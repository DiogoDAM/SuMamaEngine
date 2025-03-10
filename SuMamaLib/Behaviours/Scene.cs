using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

namespace SuMamaLib
{
	public abstract class Scene : IDisposable
	{
		protected Dictionary<string, ObjectManager> _layers;
		protected CollisionManager _collisionManager;
		protected Camera _camera;

		public string Id;

		public bool Disposed { get; protected set; }

		public Scene()
		{
			_layers = new Dictionary<string, ObjectManager>();
			Disposed = false;
			_collisionManager = new CollisionManager();
		}

		public void AddLayers(string[] layers)
		{
			foreach(string layer in layers)
			{
				_layers.Add(layer, new ObjectManager(layer, this));
			}
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
			foreach(var layer in _layers)
			{
				layer.Value.Update();
			}
			_collisionManager.Update();
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
			if(Disposed) return;
			Start();
		}

		public virtual void Exit()
		{
			if(Disposed) return;
		}

		public void AddObject(string layer, GameObject e)
		{
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("Scene.AddEnttiy() layer is null or empty");
			if(e == null) throw new ArgumentNullException("Scene.AddObject() GameObject is null");
			e.Layer = layer;
			e.OnAdd();
			_layers[layer].Add(e);
		}

		public void AddObject(GameObject e)
		{
			if(e == null) throw new ArgumentNullException("Scene.AddObject() GameObject is null");
			e.OnAdd();
			_layers[e.Layer].Add(e);
		}

		public void RemoveObject(string layer, GameObject e)
		{
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("Scene.RemoveEnttiy() layer is null or empty");
			if(e == null) throw new ArgumentNullException("Scene.RemoveObject() GameObject is null");
			e.OnRemoved();
			_layers[layer].Remove(e);
		}

		public void RemoveObject(GameObject e)
		{
			if(e == null) throw new ArgumentNullException("Scene.RemoveObject() GameObject is null");
			e.OnRemoved();
			_layers[e.Layer].Remove(e);
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

		public RectCollider CreateRectCollider(Transform trans, int w, int h)
		{
			return _collisionManager.CreateRectCollider(trans, w, h);
		}

		public CircleCollider CreateCircleCollider(Transform trans, int radius)
		{
			return _collisionManager.CreateCircleCollider(trans, radius);
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
					_camera.Dispose();
					Disposed = true;
				}
			}
		}
	}
}
