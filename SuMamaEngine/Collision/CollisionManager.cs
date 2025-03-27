using System;
using System.Collections.Generic;

namespace SuMamaEngine
{
	public sealed class CollisionManager : IDisposable
	{
		private static CollisionManager _instance;
		public static CollisionManager Instance { get { if(_instance == null) _instance = new(); return _instance; } }

		private WorldCollisor _static;
		private Dictionary<string, WorldCollisor> _scenes;

		public string CurrentScene;

		private List<Collider> _dynamicColliders;

		public bool Disposed { get; private set; }


		private CollisionManager()
		{
			_static = new();
			_scenes = new();
			_dynamicColliders = new();
		}

		public void AddScene(string scene)
		{
			if(!_scenes.ContainsKey(scene))
			{
				_scenes.Add(scene, new WorldCollisor());
			}
		}

		public void RemoveScene(string scene)
		{
			_scenes.Remove(scene);
		}

		public WorldCollisor GetWorld(string scene)
		{
			if(_scenes.ContainsKey(scene))
			{
				return _scenes[scene];
			}
			return null;
		}

		public RectCollider CreateStaticRectCollider(Transform trans, int w, int h, bool isDynamic=false)
		{
			RectCollider col = new RectCollider(trans, w, h);
			_static.AddCollider(col, isDynamic);
			if(isDynamic) _dynamicColliders.Add(col);
			return col;
		}

		public CircleCollider CreateStaticCircleCollider(Transform trans, int radius, bool isDynamic=false)
		{
			CircleCollider col = new CircleCollider(trans, radius);
			_static.AddCollider(col, isDynamic);
			if(isDynamic) _dynamicColliders.Add(col);
			return col;
		}

		public GridCollider CreateStaticCircleCollider(int cellsize, int w, int h, bool[] values)
		{
			GridCollider grid = new GridCollider(w, h, cellsize);
			grid.Set(w, h, values);
			_static.AddCollider(grid);
			return grid;
		}

		public RectCollider CreateRectCollider(string scene, Transform trans, int w, int h, bool isDynamic=false)
		{
			if(!_scenes.ContainsKey(scene)) return null;
			RectCollider col = new RectCollider(trans, w, h);
			_scenes[scene].AddCollider(col, isDynamic);
			if(isDynamic) _dynamicColliders.Add(col);
			return col;
		}

		public CircleCollider CreateCircleCollider(string scene, Transform trans, int radius, bool isDynamic=false)
		{
			if(!_scenes.ContainsKey(scene)) return null;
			CircleCollider col = new CircleCollider(trans, radius);
			_scenes[scene].AddCollider(col, isDynamic);
			if(isDynamic) _dynamicColliders.Add(col);
			return col;
		}

		public GridCollider CreateCircleCollider(string scene, int cellsize, int w, int h, bool[] values)
		{
			if(!_scenes.ContainsKey(scene)) return null;
			GridCollider grid = new GridCollider(w, h, cellsize);
			grid.Set(w, h, values);
			_scenes[scene].AddCollider(grid);
			return grid;
		}

		public void AddStaticCollider(Collider col, bool isDynamic=false)
		{
			_static.AddCollider(col, isDynamic);
			if(isDynamic) _dynamicColliders.Add(col);
		}

		public void AddCollider(string scene, Collider col, bool isDynamic=false)
		{
			_scenes[scene].AddCollider(col, isDynamic);
			if(isDynamic) _dynamicColliders.Add(col);
		}

		public void RemoveStaticCollider(Collider collider)
		{
			_static.RemoveCollider(collider);
			if(_dynamicColliders.Contains(collider)) _dynamicColliders.Remove(collider);
		}

		public void RemoveCollider(string scene, Collider collider)
		{
			_scenes[scene].RemoveCollider(collider);
			if(_dynamicColliders.Contains(collider)) _dynamicColliders.Remove(collider);
		}

		public bool ContainsStaticCollider(Collider collider)
		{
			return _static.ContainsCollider(collider);
		}

		public bool ContainsCollider(string scene, Collider collider)
		{
			if(!_scenes.ContainsKey(scene)) return false;
			return _scenes[scene].ContainsCollider(collider);
		}

		public void Update()
		{
			if(Disposed) return;

			_static.CheckCollisions();
			_scenes[CurrentScene].CheckCollisions();

			_scenes[CurrentScene].CheckCollisions(_static);
		}


		public void Dispose()
		{
			Dispose(true);
			System.GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposable)
		{
			if(disposable)
			{
				if(!Disposed)
				{
					Disposed = true;
				}
			}
		}
	}
}
