using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuMamaEngine
{
	public abstract class Scene : IDisposable
	{
		protected Dictionary<string, SceneLayer> _layers;
		protected Dictionary<string, GameObject> _taggedObjs;
		protected WorldCollisor _world;
		protected Camera _camera;

		private Queue<GameObject> _poolForAdd;
		private Queue<GameObject> _poolForRemove;

		public string Id;

		public int ObjectCount { get { int count = 0; foreach(var layer in _layers) count += layer.Value.Count; return count; } }

		public bool Disposed { get; protected set; }
		public bool IsActive { get; protected set; }

		public Scene()
		{
			_layers = new Dictionary<string, SceneLayer>();
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
			foreach(var layer in _layers)
			{
				layer.Value.Start();
			}
		}

		public virtual void Update()
		{
			if(Disposed) return;


			UpdatePools();

			foreach(var layer in _layers)
			{
				layer.Value.Update();
			}

			_world.CheckCollisions();
		}

		private void UpdatePools()
		{
			while(_poolForAdd.Count > 0)
			{
				var obj = _poolForAdd.Dequeue();
				obj.OnAdd();
				_layers[obj.Layer].Add(obj);
				obj.Start();
			}

			while(_poolForRemove.Count > 0)
			{
				var obj = _poolForRemove.Dequeue();
				obj.OnRemoved();
				_layers[obj.Layer].Remove(obj);
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
			return;
		}

		//Tag Handlers

		public void AddTag(string tag)
		{
			if(String.IsNullOrEmpty(tag)) throw new ArgumentNullException("Scene.AddTag() tag is null or empty");

			if(!_taggedObjs.ContainsKey(tag)) _taggedObjs.Add(tag, null);
		}
		
		public void AddTag(string tag, GameObject obj)
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

		public GameObject GetObjectFromTag(string tag)
		{
			if(String.IsNullOrEmpty(tag)) throw new ArgumentNullException("Scene.GetObjectFromTag() tag is null or empty");

			if(_taggedObjs.ContainsKey(tag)) return _taggedObjs[tag];
			return null;
		}

		public void SetObjectInTag(string tag, GameObject obj)
		{
			if(obj == null) throw new ArgumentNullException("Scene.SetObjectInTag() obj is null");
			if(String.IsNullOrEmpty(tag)) throw new ArgumentNullException("Scene.SetObjectInTag() tag is null or empty");

			if(_taggedObjs.ContainsKey(tag)) _taggedObjs[tag] = obj;
		}

		// GameObject Handlers

		public void AddObject(string layer, GameObject e)
		{
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("Scene.AddEnttiy() layer is null or empty");
			if(e == null) throw new ArgumentNullException("Scene.AddObject() GameObject is null");
			e.Layer = layer;
			_poolForAdd.Enqueue(e);
		}

		public void AddObject(GameObject e)
		{
			if(e == null) throw new ArgumentNullException("Scene.AddObject() GameObject is null");
			_poolForAdd.Enqueue(e);
		}

		public void RemoveObject(string layer, GameObject e)
		{
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("Scene.RemoveEnttiy() layer is null or empty");
			if(e == null) throw new ArgumentNullException("Scene.RemoveObject() GameObject is null");
			e.Layer = layer;
			_poolForRemove.Enqueue(e);
		}

		public void RemoveObject(GameObject e)
		{
			if(e == null) throw new ArgumentNullException("Scene.RemoveObject() GameObject is null");
			_poolForRemove.Enqueue(e);
		}

		public bool ContainsObject(GameObject e)
		{
			if(e == null) throw new ArgumentNullException("Scene.ContainsObject() GameObject is null");
			foreach(var layer in _layers)
			{
				if(layer.Value.Contains(e)) return true;
			}

			return false;
		}

		public bool ContainsObject(string layer, GameObject e)
		{
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("Scene.ContainsObject() layer is null or empty");
			if(e == null) throw new ArgumentNullException("Scene.ContainsObject() GameObject is null");

			return _layers[layer].Contains(e);
		}

		public GameObject FindObject(GameObject e)
		{
			if(e == null) throw new ArgumentNullException("Scene.FindObject() GameObject is null");

			foreach(var layer in _layers)
			{
				GameObject en = layer.Value.Find(e);
				if(en != null) return en;
			}

			return null;
		}

		public GameObject FindObject(string layer, GameObject e)
		{
			if(e == null) throw new ArgumentNullException("Scene.FindObject() GameObject is null");	
			return _layers[layer].Find(e);
		}

		public List<GameObject> GetAllEntities(string layer)
		{
			return _layers[layer].GetAllEntities();
		}

		public T GetEntityType<T>(string layer) where T : GameObject
		{
			return (T)_layers[layer].GetEntityType<T>();
		}

		public void ClearObjects()
		{
			_layers.Clear();
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

		public RectCollider CreateRectCollider(Transform trans, int w, int h, bool isDynamic=false)
		{
			RectCollider rect = new(trans, w, h);
			_world.AddCollider(rect, isDynamic);
			return rect;
		}

		public CircleCollider CreateCircleCollider(Transform trans, int radius, bool isDynamic=false)
		{
			CircleCollider circle = new(trans, radius);
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
