using Microsoft.Xna.Framework;

namespace SuMamaLib
{
	public class RectCollider : Collider
	{
		public override AABB Bounds { get { return new AABB(Transform.Position, new Vector2(Transform.Position.X + Width, Transform.Position.Y + Height)); } }

		public Rectangle Shape { get { return new Rectangle((int)Transform.Position.X, (int)Transform.Position.Y, Width, Height); } }
		public int Width, Height;

		public RectCollider(Transform trans, int w, int h) : base()
		{
			Transform = trans;
			Width = w;
			Height = h;
		}

		public override bool CollidesWith(Collider other)
		{
			switch(other)
			{
				case RectCollider rect:
					return Shape.Intersects(rect.Shape);
				case CircleCollider circle:
					return Collider.CircleIntersectsRectangle(circle.Center, circle.Radius, Shape);
				default:
					return false;
			}
		}

		public override void Draw(Color color)
		{
			base.Draw(color);
			Drawer.DrawLineRectangle(Shape, color, 2);
		}
	}
}
