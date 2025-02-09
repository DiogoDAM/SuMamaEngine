using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

namespace SuMamaLib
{
	public abstract class Scene : IDisposableObject
	{
		protected Dictionary<string, EntityManager> _layers;
		protected Camera _camera;

		public bool Disposed { get; protected set; }

		public Scene()
		{
			_layers = new Dictionary<string, EntityManager>();
			Disposed = false;
		}

		public void AddLayers(string[] layers)
		{
			foreach(string layer in layers)
			{
				_layers.Add(layer, new EntityManager(layer, this));
			}
		}

		public virtual void Start()
		{
			foreach(var layer in _layers)
			{
				layer.Value.Start();
			}
		}

		public virtual void Update()
		{
			foreach(var layer in _layers)
			{
				layer.Value.Update();
			}
		}

		public virtual void Draw()
		{
			foreach(var layer in _layers)
			{
				layer.Value.Draw();
			}
		}

		public void AddEntity(string layer, Entity e)
		{
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("Scene.AddEnttiy() layer is null or empty");
			if(e == null) throw new ArgumentNullException("Scene.AddEntity() entity is null");
			_layers[layer].Add(e);
		}

		public void AddEntity(Entity e)
		{
			if(e == null) throw new ArgumentNullException("Scene.AddEntity() entity is null");
			_layers[e.Layer].Add(e);
		}

		public void RemoveEntity(string layer, Entity e)
		{
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("Scene.RemoveEnttiy() layer is null or empty");
			if(e == null) throw new ArgumentNullException("Scene.RemoveEntity() entity is null");
			_layers[layer].Remove(e);
		}

		public bool ContainsEntity(Entity e)
		{
			if(e == null) throw new ArgumentNullException("Scene.ContainsEntity() entity is null");
			foreach(var layer in _layers)
			{
				if(layer.Value.Contains(e)) return true;
			}

			return false;
		}

		public bool ContainsEntity(string layer, Entity e)
		{
			if(string.IsNullOrEmpty(layer)) throw new ArgumentNullException("Scene.AddEnttiy() layer is null or empty");
			if(e == null) throw new ArgumentNullException("Scene.AddEntity() entity is null");

			return _layers[layer].Contains(e);
		}

		public Entity FindEntity(Entity e)
		{
			if(e == null) throw new ArgumentNullException("Scene.FindEntity() entity is null");

			foreach(var layer in _layers)
			{
				Entity en = layer.Value.Find(e);
				if(en != null) return en;
			}

			return null;
		}

		public Entity FindEntity(string layer, Entity e)
		{
			if(e == null) throw new ArgumentNullException("Scene.FindEntity() entity is null");	
			return _layers[layer].Find(e);
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

		protected virtual void Dispose(bool dispose)
		{
			if(dispose && !Disposed) return;
		}
	}
}
