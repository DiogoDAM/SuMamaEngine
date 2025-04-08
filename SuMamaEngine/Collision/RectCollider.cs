using Microsoft.Xna.Framework;

namespace SuMamaEngine
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

		public RectCollider(Transform trans, int w, int h, GameObject obj) : base(obj)
		{
			Transform = trans;
			Width = w;
			Height = h;
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
			return Shape.Intersects(col.Shape);
		}

		public override bool CollidesWith(CircleCollider col)
		{
			return Collider.CircleIntersectsRectangle(col.Center, col.Radius, Shape);
		}

		public override bool CollidesWith(GridCollider col)
		{
			return col.CollidesWith(this);
		}


		public override void Draw(Color color)
		{
			base.Draw(color);
			Drawer.DrawLineRectangle(Shape, color, 2);
		}
	}
}
