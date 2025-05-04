using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

namespace SuMamaEngine
{
	public abstract class Scene : IDisposable
	{
		protected Dictionary<string, SceneLayer> _layers;
		protected List<IUpdate> _updateables;

		protected Dictionary<string, object> _taggedObjs;

		protected WorldCollisor _world;

		protected Camera _camera;

		private Queue<object> _poolForAdd;
		private Queue<(bool, object)> _poolForRemove;

		public string Id;

		public int ObjectCount { get { int count = 0; foreach(var layer in _layers) count += layer.Value.Count; return count; } }

		public bool Disposed { get; protected set; }
		public bool IsActive { get; protected set; }

		public Scene()
		{
			_layers = new Dictionary<string, SceneLayer>();
			_updateables = new List<IUpdate>();
			_taggedObjs = new();
			_world = new();
			_poolForAdd = new();
			_poolForRemove = new();
			Disposed = false;
		}

		public void CreateLayers(string[] layers)
		{
			foreach(string layer in layers)
			{
				_layers.Add(layer, new SceneLayer(layer, this));
			}
		}

		public void CreateLayer(string layer)
		{
			_layers.Add(layer, new SceneLayer(layer, this));
		}

		public virtual void Start()
		{
			if(Disposed) return;
			foreach(var obj in _updateables)
			{
				obj.Start();
			}
		}

		public virtual void Update()
		{
			if(Disposed) return;

			foreach(var obj in _updateables)
			{
				obj.Update();
			}

			_world.CheckCollisions();

			UpdatePools();
		}

		private void UpdatePools()
		{
			while(_poolForAdd.Count > 0)
			{
				var obj = _poolForAdd.Dequeue();
				if(obj is IDraw)
				{
					var drawable = obj as IDraw;
					if(string.IsNullOrEmpty(drawable.Layer)) throw new ArgumentNullException("Scene.AddObject() obj.Layer is null or empty");
					_layers[drawable.Layer].Add(drawable);
				}

				if(obj is IUpdate)
				{
					var updateable = obj as IUpdate;
					_updateables.Add(updateable);
					updateable.Start();
				}

				if(obj is ISceneObject)
				{
					var sceneObject = obj as ISceneObject;
					sceneObject.Added();
				}
			}

			while(_poolForRemove.Count > 0)
			{
				var item = _poolForRemove.Dequeue();
				var disposable = item.Item1;
				var obj = item.Item2;
				if(obj is IDraw)
				{
					var drawable = obj as IDraw;
					_layers[drawable.Layer].Remove(drawable);
				}

				if(obj is IUpdate)
				{
					var updateable = obj as IUpdate;
					_updateables.Remove(updateable);
				}

				if(obj is ISceneObject)
				{
					var sceneObject = obj as ISceneObject;
					sceneObject.Removed();
				}

				if(disposable)
				{
					var dis = obj as IDisposable;
					dis.Dispose();
				}
			}
		}

		public virtual void Draw()
		{
			if(Disposed) return;
			foreach(var layer in _layers)
			{
				layer.Value.Draw();
			}
		}

		public virtual void DrawUi()
		{
			if(Disposed) return;
		}

		public virtual void Enter()
		{
			IsActive = true;
			if(Disposed) return;
		}

		public virtual void Exit()
		{
			IsActive = false;
			_world.ClearAndDispose();
			ClearObjects();
			return;
		}

		//Tag Handlers

		public void AddTag(string tag)
		{
			if(String.IsNullOrEmpty(tag)) throw new ArgumentNullException("Scene.AddTag() tag is null or empty");

			if(!_taggedObjs.ContainsKey(tag)) _taggedObjs.Add(tag, null);
		}
		
		public void AddTag(string tag, object obj)
		{
			if(obj == null) throw new ArgumentNullException("Scene.AddTag() obj is null");
			if(String.IsNullOrEmpty(tag)) throw new ArgumentNullException("Scene.AddTag() tag is null or empty");

			if(!_taggedObjs.ContainsKey(tag)) _taggedObjs.Add(tag, obj);
		}

		public void RemoveTag(string tag)
		{
			if(String.IsNullOrEmpty(tag)) throw new ArgumentNullException("Scene.RemoveTag() tag is null or empty");

			if(_taggedObjs.ContainsKey(tag)) _taggedObjs.Remove(tag);
		}

		public object GetObjectFromTag(string tag)
		{
			if(String.IsNullOrEmpty(tag)) throw new ArgumentNullException("Scene.GetObjectFromTag() tag is null or empty");

			if(_taggedObjs.ContainsKey(tag)) return _taggedObjs[tag];
			return null;
		}

		public void SetObjectInTag(string tag, object obj)
		{
			if(obj == null) throw new ArgumentNullException("Scene.SetObjectInTag() obj is null");
			if(String.IsNullOrEmpty(tag)) throw new ArgumentNullException("Scene.SetObjectInTag() tag is null or empty");

			if(_taggedObjs.ContainsKey(tag)) _taggedObjs[tag] = obj;
		}

		// object Handlers

		public void AddObject(object o)
		{
			if(o == null) throw new ArgumentNullException("Scene.AddObject() object is null");
			_poolForAdd.Enqueue(o);
		}

		public void AddObject(object o, string layer)
		{
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("Scene.AddEnttiy() layer is null or empty");
			if(o == null) throw new ArgumentNullException("Scene.AddObject() object is null");
			_poolForAdd.Enqueue(o);
		}

		public void RemoveObject(object o, string layer, bool disposable=false)
		{
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("Scene.RemoveEnttiy() layer is null or empty");
			if(o == null) throw new ArgumentNullException("Scene.RemoveObject() Object is null");
			_poolForRemove.Enqueue((disposable, o));
		}


		public void RemoveObject(object o, bool disposable=false)
		{
			if(o == null) throw new ArgumentNullException("Scene.RemoveObject() Object is null");
			_poolForRemove.Enqueue((disposable, o));
		}

		public bool ContainsObject(object o)
		{
			if(o == null) throw new ArgumentNullException("Scene.ContainsObject() Object is null");
			if(o is IUpdate)
			{
				return _updateables.Contains((IUpdate) o);
			}

			return false;
		}

		public bool ContainsObject(object o, string layer)
		{
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("Scene.ContainsObject() layer is null or empty");
			if(o == null) throw new ArgumentNullException("Scene.ContainsObject() Object is null");

			if(o is IDraw)
			{
				return _layers[layer].Contains((IDraw) o);
			}

			return false;
		}

		public object FindObject(object o)
		{
			if(o == null) throw new ArgumentNullException("Scene.FindObject() object is null");

			if(o is IUpdate)
			{
				return _updateables.Find(obj => obj.Equals(o));
			}

			return null;
		}

		public object FindObject(object o, string layer)
		{
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("Scene.FindObject() layer is null or empty");
			if(o == null) throw new ArgumentNullException("Scene.FindObject() object is null");	

			if(o is IDraw)
			{
				return _layers[layer].Find((IDraw) o);
			}

			return null;
		}

		public List<IDraw> GetAllEntitiesFromLayer(string layer)
		{
			return _layers[layer].GetAllEntities();
		}

		public T GetEntityTypeFromLayer<T>(string layer) where T : IDraw
		{
			return (T)_layers[layer].GetEntityType<T>();
		}

		public List<IUpdate> GetAllEntities()
		{
			return _updateables;
		}

		public T GetEntityType<T>() where T : IUpdate
		{
			return (T)_updateables.Find(obj => obj is T);
		}

		public void ClearObjectsFromLayers()
		{
			_layers.Clear();
		}

		public void ClearObjectsFromLayer(string layer)
		{
			_layers[layer].Clear();
		}

		public void ClearObjects()
		{
			_updateables.Clear();
		}

		public void Clear()
		{
			_layers.Clear();
			_updateables.Clear();
		}

		public Camera GetCamera()
		{
			return _camera;
		}

		public Matrix GetCameraMatrix()
		{
			return _camera.GetMatrix();
		}

		public void SetCamera(Camera camera)
		{
			_camera = camera;
		}

		public RectCollider CreateRectCollider(Transform trans, int w, int h, object obj=null, bool isDynamic=false)
		{
			RectCollider rect = new(trans, w, h, obj);
			_world.AddCollider(rect, isDynamic);
			return rect;
		}


		public CircleCollider CreateCircleCollider(Transform trans, int radius, object obj=null, bool isDynamic=false)
		{
			CircleCollider circle = new(trans, radius, obj);
			_world.AddCollider(circle, isDynamic);
			return circle;
		}

		public void AddCollider(Collider col, bool isDynamic=false)
		{
			if(col == null) throw new ArgumentNullException("Scene.AddCollider() col is null");
			_world.AddCollider(col, isDynamic);
		}

		public void RemoveCollider(Collider col)
		{
			if(col == null) throw new ArgumentNullException("Scene.RemoveCollider() col is null");
			_world.RemoveCollider(col);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposable)
		{
			if(disposable)
			{
				if(!Disposed)
				{
					_layers.Clear();
					_poolForAdd.Clear();
					_poolForRemove.Clear();
					_camera.Dispose();
					_world.Dispose();
					Disposed = true;
				}
			}
		}
	}
}
