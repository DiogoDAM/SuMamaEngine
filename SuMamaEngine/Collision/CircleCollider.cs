using System;
using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public sealed class CircleCollider : Collider
	{
		public override AABB Bounds { get { return new AABB(Transform.Position, new Vector2(Transform.Position.X + Radius*2, Transform.Position.Y + Radius*2)); } }

		public Vector2 Center { get { return new Vector2(Transform.Position.X + Radius, Transform.Position.Y + Radius); } }

		public int Radius;

		public CircleCollider(Transform trans, int radius) : base()
		{
			Transform = trans;
			Radius = radius;
		}

		public override bool CollidesWith(Collider col)
		{
			switch(col)
			{
				case RectCollider rect: return CollidesWith(rect);
				case CircleCollider circle: return CollidesWith(circle);
				case GridCollider grid: return CollidesWith(grid);
				default: return false;
			}
		}

		public override bool CollidesWith(RectCollider col)
		{
			return Collider.CircleIntersectsRectangle(Center, Radius, col.Shape);
		}

		public override bool CollidesWith(CircleCollider col)
		{
			return Collider.CircleIntersectsCircle(Center, Radius, col.Center, col.Radius);
		}

		public override bool CollidesWith(GridCollider col)
		{
			return col.CollidesWith(this);
		}

		public override void Draw(Color color)
		{
			base.Draw(color);
			Drawer.DrawLineCircle(Center, Radius, 12, color, 2);
		}
	}
}
