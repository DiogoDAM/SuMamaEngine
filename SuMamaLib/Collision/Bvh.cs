using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SuMamaLib
{
	public sealed class Bvh : IDisposable
	{
		private BvhNode _root;
		public List<Collider> Colliders;
		private Dictionary<Collider, HashSet<Collider>> _currentColliders;

		public bool Disposed { get; private set; }

		public int Count { get { return Colliders.Count; } }

		public Bvh()
		{
			Colliders = new();
			_currentColliders = new();
		}

		public void Update()
		{
			Rebuild();
			CheckCollisions();
		}

		private void CheckCollisions()
		{
			_currentColliders.Clear();
			CheckCollision(_root);

			foreach(Collider col in Colliders)
			{
				col.CheckCollisionEvent(_currentColliders.ContainsKey(col) ? _currentColliders[col] : new HashSet<Collider>());
			}
		}

		public void Insert(Collider col)
		{
			Colliders.Add(col);
			Rebuild();
		}

		public void Remove(Collider col)
		{
			Colliders.Remove(col);
			Rebuild();
		}

		public bool Contains(Collider col)
		{
			return Colliders.Contains(col);
		}

		public List<Collider> Query(Collider col)
		{
			List<Collider> colliders = new();
			QueryNode(_root, col, colliders);
			return colliders;
		}

		private void QueryNode(BvhNode node, Collider col, List<Collider> colliders)
		{
			if(node == null || node.Bounds.Intersects(col.Bounds)) return;

			if(node.IsLeaf && node.Collider != null) colliders.Add(node.Collider);

			QueryNode(node.Left, col, colliders);
			QueryNode(node.Right, col, colliders);
		}

		private void Rebuild()
		{
			_root = Build(Colliders);
		}

		private BvhNode Build(List<Collider> colliders)
		{
			if(colliders.Count == 0) return null;
			if(colliders.Count == 1) return new BvhNode(colliders[0].Bounds, colliders[0]);

			colliders.Sort((a, b) => a.Bounds.Min.X.CompareTo(b.Bounds.Min.X));
			int mid = colliders.Count/2;

			var left = Build(colliders.GetRange(0, mid));
			var right = Build(colliders.GetRange(mid, colliders.Count - mid));

			AABB combindedBound = AABB.Union(left.Bounds, right.Bounds);

			return new BvhNode(combindedBound, null, left, right);
		}

		private void CheckCollision(BvhNode node)
		{
			if(node == null || node.IsLeaf) return;

			CheckCollision(node.Left);
			CheckCollision(node.Right);
			CheckCollisions(node.Left, node.Right);
		}

		private void CheckCollisions(BvhNode a, BvhNode b)
		{
			if(a == null || b == null || !a.Bounds.Intersects(b.Bounds)) return;

			if(a.IsLeaf && b.IsLeaf)
			{
				if(!_currentColliders.ContainsKey(a.Collider))
				{
					_currentColliders[a.Collider] = new HashSet<Collider>();
				}
				if(!_currentColliders.ContainsKey(b.Collider))
				{
					_currentColliders[b.Collider] = new HashSet<Collider>();
				}

				_currentColliders[a.Collider].Add(b.Collider);
				_currentColliders[b.Collider].Add(a.Collider);
			}
			else
			{
			    CheckCollisions(a.Left, b);
			    CheckCollisions(a.Right, b);
				CheckCollisions(a, b.Left);
				CheckCollisions(a, b.Right);
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
					Colliders.Clear();
					_currentColliders.Clear();
					Disposed = true;
				}
			}
		}
	}
}
