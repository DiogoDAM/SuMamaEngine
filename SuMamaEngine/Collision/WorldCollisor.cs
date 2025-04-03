using System;
using System.Collections.Generic;

namespace SuMamaEngine
{
	public sealed class WorldCollisor : IDisposable
	{
		public ColliderContainer Colliders;

		private List<Collider> _dynamics;

		public int Count { get { return Colliders.Count; } }
		public int DynamicsCount { get { return _dynamics.Count; } }

		public bool Disposed { get; private set; }

		public WorldCollisor()
		{
			Colliders = new();
			_dynamics = new();
		}

		public void AddCollider(Collider col, bool isDynanic=false)
		{
			if(col == null) throw new System.ArgumentNullException("Collider col is null");
			if(!Colliders.Contains(col)) Colliders.Add(col);
			if(isDynanic) _dynamics.Add(col);
		}

		public void RemoveCollider(Collider col)
		{
			if(col == null) throw new System.ArgumentNullException("Collider col is null");
			if(Colliders.Contains(col)) Colliders.Remove(col);
			if(_dynamics.Contains(col)) _dynamics.Remove(col);

		}

		public bool ContainsCollider(Collider col)
		{
			if(col == null) throw new System.ArgumentNullException("Collider col is null");
			return Colliders.Contains(col);
		}

		public void CheckCollisions()
		{
			MoveDynamics();

			foreach(var col in _dynamics)
			{
				col.CheckCollisionEvent(Colliders);
			}
		}

		public void CheckCollisions(WorldCollisor world)
		{
			foreach(var col in world._dynamics)
			{
				col.CheckCollisionEvent(Colliders);
			}
		}

		private void MoveDynamics()
		{
			foreach(Collider d in _dynamics)
			{
				Colliders.Remove(d);
				Colliders.Add(d);
			}
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
					_dynamics.Clear();
					Colliders.Dispose();
					Disposed = true;
				}
			}
		}
	}
}
