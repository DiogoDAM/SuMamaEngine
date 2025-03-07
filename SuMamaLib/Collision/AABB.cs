using System;
using Microsoft.Xna.Framework;

namespace SuMamaLib
{
	public struct AABB
	{
		public Vector2 Min, Max;

		public Vector2 TopLeft { get { return Min; } }
		public Vector2 TopRight { get { return new Vector2(Max.X, Min.Y); } }
		public Vector2 BottomLeft { get { return new Vector2(Min.X, Max.Y); } }
		public Vector2 BottomRight { get { return Max; } }

		public Vector2 Center { get { return new Vector2(Min.X + (Max.X - Min.X), Min.Y + (Max.Y - Min.Y)); } }

		public static AABB Empty { get { return new AABB(Vector2.Zero, Vector2.Zero); } } 

		public AABB(Vector2 min, Vector2 max)
		{
			Min = min;
			Max = max;
		}

		public AABB(Rectangle rect)
		{
			Min = new Vector2(rect.X, rect.Y);
			Max = new Vector2(rect.X + rect.Width, rect.Y + rect.Height);
		}

		public void FromRectangle(Rectangle rect)
		{
			Min = new Vector2(rect.X, rect.Y);
			Max = new Vector2(rect.X + rect.Width, rect.Y + rect.Height);
		}

		public Rectangle ToRectangle()
		{
			return new Rectangle(Min.ToPoint(), (Max - Min).ToPoint());
		}

		public bool Intersects(AABB other)
		{
			return (Min.X <= other.Max.X && Max.X >= other.Min.X) &&
				(Min.Y <= other.Max.Y && Max.Y >= other.Min.Y);
		}

		public static AABB Union(AABB value1, AABB value2)
		{
			float minX = Math.Min(value1.Min.X, value2.Min.X);
			float minY = Math.Min(value1.Min.Y, value2.Min.Y);
			float maxX = Math.Max(value1.Max.X, value2.Max.X);
			float maxY = Math.Max(value1.Max.Y, value2.Max.Y);

			return new AABB(new Vector2(minX, minY), new Vector2(maxX, maxY));
		}

	}
}
