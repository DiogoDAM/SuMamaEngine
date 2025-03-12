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

		public override bool CollidesWith(Collider other)
		{
			switch(other)
			{
				case RectCollider rect:
					return Collider.CircleIntersectsRectangle(Transform.Position + new Vector2(Radius, Radius), Radius, rect.Shape);
				case CircleCollider circle:
					return Collider.CircleIntersectsCircle(Center, Radius, circle.Center, circle.Radius);
				default:
					return false;
			}
		}

		public override void Draw(Color color)
		{
			base.Draw(color);
			Drawer.DrawLineCircle(Center, Radius, 12, color, 2);
		}
	}
}
