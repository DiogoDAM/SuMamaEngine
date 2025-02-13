using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuMamaLib.Utils.Sprites
{
	public class NineSlice
	{
		public Texture2D Texture { get; private set; }
		//Bounds of Texture
		public Rectangle Bounds { get => new Rectangle(0, 0, Width, Height); }

		private Rectangle _topLeft, _topMiddle, _topRight;
		private Rectangle _midLeft, _midMiddle, _midRight;
		private Rectangle _bottomLeft, _bottomMiddle, _bottomRight;

		public int Width, Height;

		public NineSlice(Texture2D texture, Rectangle bounds)
		{
			if(texture == null) throw new NullReferenceException();
			Texture = texture;

			Width = bounds.Width;
			Height = bounds.Height;

			int x = bounds.Location.X;
			int y = bounds.Location.Y;
			int width = Width/3;
			int height = Height/3;

			_topLeft = new(x, y, width, height);
			_topMiddle = new(x + width, y, width, height);
			_topRight = new(x + width * 2, y, width, height);

			_midLeft = new(x, y + height, width, height);
			_midMiddle = new(x + width, y + height, width, height);
			_midRight = new(x + width * 2, y + height, width, height);

			_bottomLeft = new(x, y + height * 2, width, height);
			_bottomMiddle = new(x + width, y + height * 2, width, height);
			_bottomRight = new(x + width * 2, y + height * 2, width, height);
		}

		public void DrawWithNone(Vector2 pos, Color color, float rot, Vector2 origin, Vector2 scale, SpriteEffects se, float depth)
		{
			Globals.SpriteBatch.Draw(Texture, pos, Bounds, color, rot, origin, scale, se, depth);
		}

		public void DrawWithRepeat(int repX, int repY, Vector2 pos, Color color, float rot, Vector2 origin, Vector2 scale, SpriteEffects se, float depth)
		{
			int width = Width/3;
			int height = Height/3;
			//Top
			Globals.SpriteBatch.Draw(Texture, pos, _topLeft, color, rot, origin, scale, se, depth);
			for(int i=1; i<repX; i++)
			{
				Globals.SpriteBatch.Draw(Texture, new Vector2((int)pos.X + (width * i), (int)pos.Y), _topMiddle, color, rot, origin, scale, se, depth);
			}
			Globals.SpriteBatch.Draw(Texture, new Vector2((int)pos.X + (width * repX), (int)pos.Y), _topRight, color, rot, origin, scale, se, depth);

			//Middle
			for(int y=1; y<repY; y++)
			{
				Globals.SpriteBatch.Draw(Texture, new Vector2((int)pos.X, (int)pos.Y + (height * y)), _midLeft, color, rot, origin, scale, se, depth);
				for(int x=1; x<repX; x++)
				{
					Globals.SpriteBatch.Draw(Texture, new Vector2((int)pos.X + (width * x), (int)pos.Y + (height * y)), _midMiddle, color, rot, origin, scale, se, depth);
				}
				Globals.SpriteBatch.Draw(Texture, new Vector2((int)pos.X + (width * repX), (int)pos.Y + (height * y)), _midRight, color, rot, origin, scale, se, depth);
			}

			//Bottom
			Globals.SpriteBatch.Draw(Texture, new Vector2((int)pos.X, (int)pos.Y + (height * repY)), _bottomLeft, color, rot, origin, scale, se, depth);
			for(int i=1; i<repX; i++)
			{
				Globals.SpriteBatch.Draw(Texture, new Vector2((int)pos.X + (width * i), (int)pos.Y + (height * repY)), _bottomMiddle, color, rot, origin, scale, se, depth);
			}
			Globals.SpriteBatch.Draw(Texture, new Vector2((int)pos.X + (width * repX), (int)pos.Y + (height * repY)), _bottomRight, color, rot, origin, scale, se, depth);
		}

		public void DrawWithStretch(int widthStretch, int heightStretch, Vector2 pos, Color color, float rot, Vector2 origin, Vector2 scale, SpriteEffects se, float depth)
		{
			float width = Width/3;
			float height = Height/3;
			float ws = scale.X * widthStretch;
			float hs = scale.Y * heightStretch;

			//Top
			Globals.SpriteBatch.Draw(Texture, pos, _topLeft, color, rot, origin, scale, se, depth);
			Globals.SpriteBatch.Draw(Texture, new Vector2(pos.X + width, pos.Y), _topMiddle, color, rot, origin, new Vector2(ws, scale.Y), se, depth);
			Globals.SpriteBatch.Draw(Texture, new Vector2(pos.X + width + ws * width, pos.Y), _topRight, color, rot, origin, scale, se, depth);

			//Middle
			Globals.SpriteBatch.Draw(Texture, new Vector2(pos.X, pos.Y + height), _midLeft, color, rot, origin, new Vector2(scale.X, hs), se, depth);
			Globals.SpriteBatch.Draw(Texture, new Vector2(pos.X + width, pos.Y + height), _midMiddle, color, rot, origin, new Vector2(ws, hs), se, depth);
			Globals.SpriteBatch.Draw(Texture, new Vector2(pos.X + width + ws * width, pos.Y + height), _midRight, color, rot, origin, new Vector2(scale.X, hs), se, depth);

			//Bottom
			Globals.SpriteBatch.Draw(Texture, new Vector2(pos.X, pos.Y + height + hs * height), _bottomLeft, color, rot, origin, scale, se, depth);
			Globals.SpriteBatch.Draw(Texture, new Vector2(pos.X + width, pos.Y + height + hs * height), _bottomMiddle, color, rot, origin, new Vector2(ws, scale.Y), se, depth);
			Globals.SpriteBatch.Draw(Texture, new Vector2(pos.X + width + ws * width, pos.Y + height + hs * height), _bottomRight, color, rot, origin, scale, se, depth);
		}
	}
}
