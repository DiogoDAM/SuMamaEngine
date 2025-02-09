using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuMamaLib
{
	public static class Drawer
	{
		private static Texture2D _pixelTexture;

		public static void Initialize(GraphicsDevice graphics)
		{
			_pixelTexture = new Texture2D(graphics, 1, 1);
			_pixelTexture.SetData<Color>(new Color[] {Color.White});
		}

		public static void DrawStraightLine(Vector2 pos, int length, Color color, int thicknes=1)
		{
			Globals.SpriteBatch.Draw(_pixelTexture, new Rectangle((int)pos.X, (int)pos.Y, length, thicknes), color);
		}

		public static void DrawLine(Vector2 start, Vector2 end, Color color, int thicknes=1, float depth=1f)
		{
			float distance = Vector2.Distance(start, end);
			float angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);

			Globals.SpriteBatch.Draw(_pixelTexture, start, null, color, angle, Vector2.Zero, new Vector2(distance, thicknes), SpriteEffects.None, depth);
		}

		public static void DrawLineRectangle(Vector2 pos, int w, int h, Color color, int thicknes=1, float depth=1f)
		{
			Vector2 topLeft = pos;
			Vector2 topRight = pos + new Vector2(w,0);
			Vector2 bottomLeft = pos + new Vector2(0, h);
			Vector2 bottomRight = pos + new Vector2(w,h);

			DrawLine(topLeft, topRight, color, thicknes, depth);
			DrawLine(topRight, bottomRight, color, thicknes, depth);
			DrawLine(bottomRight, bottomLeft, color, thicknes, depth);
			DrawLine(bottomLeft, topLeft, color, thicknes, depth);
		}

		public static void DrawLineRectangle(Rectangle rect, Color color, int thicknes=1, float depth=1f)
		{
			Vector2 topLeft = new Vector2(rect.Left, rect.Top);
			Vector2 topRight = new Vector2(rect.Right, rect.Top);
			Vector2 bottomLeft = new Vector2(rect.Left, rect.Bottom);
			Vector2 bottomRight = new Vector2(rect.Right, rect.Bottom);

			DrawLine(topLeft, topRight, color, thicknes, depth);
			DrawLine(topRight, bottomRight, color, thicknes, depth);
			DrawLine(bottomRight, bottomLeft, color, thicknes, depth);
			DrawLine(bottomLeft, topLeft, color, thicknes, depth);
		}

		public static void DrawFillRectangle(Vector2 pos, int w, int h, Color color, float rot=0f, float depth=1f)
		{
			Globals.SpriteBatch.Draw(_pixelTexture, pos, null, color, rot, Vector2.Zero, new Vector2(w,h), SpriteEffects.None, depth);
		}

		public static void DrawFillRectangle(Rectangle rect, Color color, float rot=0f, float depth=1f)
		{
			Globals.SpriteBatch.Draw(_pixelTexture, rect, null, color, rot, Vector2.Zero, SpriteEffects.None, depth);
		}

		public static void DrawLinePolygon(Vector2[] vertices, Color color, int thicknes=1, float depth=1f)
		{
			for(int i=0; i<vertices.Length; i++)
			{
				Vector2 start = vertices[i];
				Vector2 end = vertices[(i + 1) % vertices.Length];

				DrawLine(start, end, color, thicknes, depth);
			}
		}

		public static void DrawLineCircle(Vector2 center, int radius, int lines, Color color, int thicknes=1, float depth=1)
		{
			Vector2[] vertices = new Vector2[lines];
			float angleStep = MathHelper.TwoPi / lines;

			for(int i=0; i<lines; i++)
			{
				float angle = i * angleStep;
				float x = center.X + radius * (float)Math.Cos(angle);
				float y = center.Y + radius * (float)Math.Sin(angle);
				vertices[i] = new Vector2(x, y);
			}

			for(int i=0; i<vertices.Length; i++)
			{
				Vector2 start = vertices[i];
				Vector2 end = vertices[(i + 1) % vertices.Length];

				DrawLine(start, end, color, thicknes, depth);
			}
		}
	}
}
