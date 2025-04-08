using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuMamaEngine
{
	public sealed class Sprite : IDisposable
	{
		public Texture2D Texture { get; private set; }
		public Rectangle Bounds { get { return new Rectangle((int)FramePosition.X, (int)FramePosition.Y, Width, Height);} }

		public Vector2 FramePosition;
		public int Width, Height;
		public Color Color;
		public SpriteEffects Flip;

		public bool Disposed { get; private set; }

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

		~Sprite()
		{
			Dispose(true);
		}

		public void SetTexture(Texture2D texture)
		{
			if(texture == null) throw new System.NullReferenceException("The Texture2D is null");

			Texture = texture;
		}

		public void SetBounds(Rectangle bounds)
		{
			FramePosition = new Vector2(bounds.X, bounds.Y);
			Width = bounds.Width;
			Height = bounds.Height;
		}

		public void SetBounds(Point pos, Point size)
		{
			FramePosition = pos.ToVector2();
			Width = size.X;
			Height = size.Y;
		}

		public void SetBounds(int x, int y, int w, int h)
		{
			FramePosition = new Vector2(x, y);
			Width = w;
			Height = h;
		}

		public void Dispose()
		{
			Dispose(true);
			System.GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposable)
		{
			if(disposable)
			{
				if(!Disposed)
				{
					Disposed = true;
				}
			}
		}

		public override string ToString()
		{
			return $"Sprite : (Texture: {Texture}, Bounds : {Bounds.ToString()}, Color: {Color}, Flip: {Flip})";
		}
	}
}
