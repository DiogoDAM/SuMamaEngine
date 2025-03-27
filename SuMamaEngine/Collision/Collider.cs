using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public enum CollisionSide
	{
		None,
		Left,
		Right,
		Top,
		Bottom
	}

	public abstract class Collider : IDisposable
	{
		public abstract AABB Bounds { get; }

        public bool Disposed { get; protected set; }

		public Transform Transform;

		public Action<CollisionInfo> Collides;

		public GameObject Object;

		public CollisionTag Tags;

		public Collider()
		{
			// _currColliders = new();
			Tags = new CollisionTag();
		}

		public abstract bool CollidesWith(Collider col);
		public abstract bool CollidesWith(RectCollider col);
		public abstract bool CollidesWith(CircleCollider col);
		public abstract bool CollidesWith(GridCollider col);

		public void CheckCollisionEvent(ColliderContainer container)
		{
			foreach(var col in container.Colliders)
			{
				if(this.Equals(col)) continue;
				if(!Tags.CheckTags(col)) continue;
				if(!CollidesWith(col)) continue;

				Collides?.Invoke(new CollisionInfo(this, col));
			}	
		}

		public static bool CircleIntersectsRectangle(Vector2 cCenter, int radius, Rectangle rect)
		{
			Point closestPoint = new Point(Math.Clamp((int)cCenter.X, rect.Left, rect.Right), Math.Clamp((int)cCenter.Y, rect.Top, rect.Bottom));

			Point distance = cCenter.ToPoint() - closestPoint;

			return distance.X * distance.X + distance.Y * distance.Y <= radius * radius;
		}

		public static bool CircleIntersectsCircle(Vector2 cCenter1, int radius1, Vector2 cCenter2, int radius2)
		{
			Point distance = cCenter1.ToPoint() - cCenter2.ToPoint();

			int radiusSum = radius1 + radius2;

			return distance.X * distance.X + distance.Y * distance.Y <= radiusSum * radiusSum;
		}

		public static (CollisionSide, CollisionSide) GetCollisionDirection(Collider col1, Collider col2)
		{
			float leftOverlap = Math.Abs(col1.Bounds.Max.X - col2.Bounds.Min.X);
			float rightOverlap = Math.Abs(col2.Bounds.Max.X - col1.Bounds.Min.X);
			float topOverlap = Math.Abs(col1.Bounds.Max.Y - col2.Bounds.Min.Y);
			float bottomOverlap = Math.Abs(col2.Bounds.Max.Y - col1.Bounds.Min.Y);

			float minOverlap = Math.Min(Math.Min(leftOverlap, rightOverlap), Math.Min(topOverlap, bottomOverlap));

			if(minOverlap == leftOverlap) return (CollisionSide.Right, CollisionSide.Left);
			if(minOverlap == rightOverlap) return (CollisionSide.Left, CollisionSide.Right);
			if(minOverlap == topOverlap) return (CollisionSide.Bottom, CollisionSide.Top);
			if(minOverlap == bottomOverlap) return (CollisionSide.Top, CollisionSide.Bottom);
			return (CollisionSide.None, CollisionSide.None);
		}

		public static void ResolveCollision(Collider col1, Collider col2)
		{
			Rectangle overlap = Rectangle.Intersect(col1.Bounds.ToRectangle(), col2.Bounds.ToRectangle());

			if(overlap.Width < overlap.Height)
			{
				if(col1.Transform.Position.X < col2.Transform.Position.X)
				{
					col1.Transform.Position.X -= overlap.Width;
				}
				else
				{
					col1.Transform.Position.X += overlap.Width;
				}
			}
			else
			{
				if(col1.Transform.Position.Y < col2.Transform.Position.Y)
				{
					col1.Transform.Position.Y -= overlap.Height;
				}
				else
				{
					col1.Transform.Position.Y += overlap.Height;
				}
			}
		}

		public static void ResolveCollisionGrid(Collider col1, GridCollider grid)
		{
			Rectangle rect = grid.GetRectangleOfCell(col1);
			Rectangle overlap = Rectangle.Intersect(col1.Bounds.ToRectangle(), rect);

			if(overlap.Width < overlap.Height)
			{
				if(col1.Transform.Position.X < rect.X)
				{
					col1.Transform.Position.X -= overlap.Width;
				}
				else
				{
					col1.Transform.Position.X += overlap.Width;
				}
			}
			else
			{
				if(col1.Transform.Position.Y < rect.Y)
				{
					col1.Transform.Position.Y -= overlap.Height;
				}
				else
				{
					col1.Transform.Position.Y += overlap.Height;
				}
			}
		}

		public virtual void Draw(Color color)
		{
			if(Disposed) return;
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
					// _currColliders.Clear();
					// CollisionEnter = null;
					// CollisionStay = null;
					// CollisionExit = null;
					Collides = null;
					Disposed = true;
				}
			}
		}
    }
}
