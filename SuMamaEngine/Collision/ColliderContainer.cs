using System.Collections.Generic;

namespace SuMamaEngine
{
	public class ColliderContainer
	{
		public List<Collider> Colliders;

		public int Count { get { return Colliders.Count; } }

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

	}
}
