using System;
using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public static class Util
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
	}
}
