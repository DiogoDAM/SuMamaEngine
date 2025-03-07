using System;
using Microsoft.Xna.Framework;

namespace SuMamaLib
{
	public static class Utilities
	{
		public static Vector2 CarToIsoScreen(int mapx, int mapy, int tileWidth, int tileHeight)
		{
			return new Vector2((mapx - mapy) * (tileWidth/2), (mapx + mapy) * (tileHeight/2));
		}

		public static Vector2 CarToIsoScreen(Point map, int tileWidth, int tileHeight)
		{
			return new Vector2((map.X - map.Y) * (tileWidth/2)-16, (map.X + map.Y) * (tileHeight/2)-20);
		}

		public static Vector2 CarToIsoMap(Vector2 worldPos, int tileWidth, int tileHeight)
		{
			float mapX = (worldPos.X / (tileWidth/2) + worldPos.Y / (tileHeight/2) ) / 2;
			float mapY = (worldPos.Y / (tileHeight/2) - worldPos.X / (tileWidth/2) ) / 2;

			return new Vector2((float)Math.Floor(mapX + .5f), (float)Math.Floor(mapY + .5f));
		}

		public static Rectangle GetOverlap(Rectangle rect1, Rectangle rect2)
		{
			int x = Math.Max(rect1.Left, rect2.Right);
			int y = Math.Max(rect1.Top, rect2.Bottom);
			int w = Math.Min(rect1.Right, rect2.Left) - x;
			int h = Math.Min(rect1.Bottom, rect2.Top) - y;

			return new Rectangle(x, y, w, h);
		}

		public static AABB GetOverlap(AABB a, AABB b)
		{
			int x = Math.Max((int)a.Min.X, (int)b.Max.X);
			int y = Math.Max((int)a.Min.Y, (int)b.Max.Y);
			int w = Math.Min((int)a.Max.X, (int)b.Min.X);
			int h = Math.Min((int)a.Max.Y, (int)b.Min.Y);
			return new AABB(new Vector2(x, y), new Vector2(w, h));
		}
	}
}
