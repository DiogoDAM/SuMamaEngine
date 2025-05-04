using System;
using System.Collections.Generic;

namespace SuMamaEngine
{
	public sealed class ColliderContainer : IDisposable
	{
		public List<Collider> Colliders;

		public int Count { get { return Colliders.Count; } }

		public bool Disposed { get; private set; }

		public ColliderContainer()
		{
			Colliders = new();
		}

		public ColliderContainer(List<Collider> list)
		{
			Colliders = new(list);
		}

		public void Add(Collider col)
		{
			if(!Colliders.Contains(col)) Colliders.Add(col);
		}

		public void Remove(Collider col)
		{
			Colliders.Remove(col);
		}

		public bool Contains(Collider col)
		{
			return Colliders.Contains(col);
		}

		public Collider Find(Collider col)
		{
			return Colliders.Find((c) => c.Equals(col));
		}

		public void Clear()
		{
			Colliders.Clear();
		}

		public bool Collide(Collider col)
		{
			foreach(var c in Colliders)
			{
				if(col.CollidesWith(c)) return true;
			}
			return false;
		}

		public List<Collider> GetColliders(Collider col)
		{
			List<Collider> list = new();
			foreach(var c in Colliders)
			{
				if(col.CollidesWith(c)) list.Add(c);
			}
			return list;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposable)
		{
			if(disposable)
			{
				if(!Disposed)
				{
					Colliders.Clear();
					Disposed = true;
				}
			}
		}
	}
}
