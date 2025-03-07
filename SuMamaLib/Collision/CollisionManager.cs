using System;
using Microsoft.Xna.Framework;

namespace SuMamaLib
{
	public sealed class CollisionManager : IDisposable
	{
		private Bvh _bvh;
		private SpatialGrid _grid;

		public bool Disposed { get; private set; }

		public Vector2 Position;
		public int Width, Height;

		private CollisionManager(int Width, int Height)
		{
			_bvh = new();
			_grid = new(20, Width/20, Height/20);
		}

		public RectCollider CreateRectCollider(Transform trans, int w, int h, bool isSolid=true)
		{
			RectCollider col = new RectCollider(trans, w, h, isSolid);
			_bvh.Insert(col);
			return col;
		}

		public CircleCollider CreateCircleCollider(Transform trans, int radius, bool isSolid=true)
		{
			CircleCollider col = new CircleCollider(trans, radius, isSolid);
			_bvh.Insert(col);
			return col;
		}

		public void AddCollider(Collider col)
		{
			if(col == null) throw new System.ArgumentNullException("CollisionManager.AddColider() col is null");
			_bvh.Insert(col);
		}

		public void SetColliderGrid(int x, int y, bool value)
		{
			_grid.SetCollider(x, y, value);
		}

		public void RemoveCollider(Collider collider)
		{
			_bvh.Remove(collider);
		}

		public bool ContainsCollider(Collider collider)
		{
			return _bvh.Contains(collider);
		}

		public void Update()
		{
			if(Disposed) return;
			_bvh.Update();

		}

		public void Draw()
		{
			if(Disposed) return;
			foreach(var col in _bvh.Colliders)
			{
				switch(col)
				{
					case RectCollider rect:
						Drawer.DrawLineRectangle(rect.Shape, Color.Red);
						break;
					case CircleCollider circle:
						Drawer.DrawLineCircle(circle.Center, circle.Radius, 12, Color.Red);
						break;
					default: break;
				}
			}
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
					_bvh.Dispose();
					_grid = null;
					Disposed = true;
				}
			}
		}
	}
}
