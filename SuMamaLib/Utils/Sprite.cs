using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuMamaLib
{
	public sealed class Sprite
	{
		public Texture2D Texture { get; private set; }
		public Rectangle Bounds { get { return new Rectangle(FramePosition, Size);} }

		public Point FramePosition;
		public Point Size;
		public Color Color;
		public SpriteEffects Flip;

		public Sprite(Texture2D texture, Point pos, Point size)
		{
			SetTexture(texture);
			SetBounds(pos, size);
			Color = Color.White;
			Flip = SpriteEffects.None;
		}

		public Sprite(Texture2D texture, int x, int y, int w, int h)
		{
			SetTexture(texture);
			SetBounds(x, y, w, h);
			Color = Color.White;
			Flip = SpriteEffects.None;
		}

		public Sprite(Texture2D texture, Rectangle bounds)
		{
			SetTexture(texture);
			SetBounds(bounds);
			Color = Color.White;
			Flip = SpriteEffects.None;
		}

		public void SetTexture(Texture2D texture)
		{
			if(texture == null) throw new System.NullReferenceException("The Texture2D is null");

			Texture = texture;
		}

		public void SetBounds(Rectangle bounds)
		{
			FramePosition = new Point(bounds.X, bounds.Y);
			Size = new Point(bounds.Width, bounds.Height);
		}

		public void SetBounds(Point pos, Point size)
		{
			FramePosition = pos;
			Size = size;
		}

		public void SetBounds(int x, int y, int w, int h)
		{
			FramePosition = new Point(x, y);
			Size = new Point(w, h);
		}
	}
}
