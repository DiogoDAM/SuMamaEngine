using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuMamaLib
{
	public sealed class SpriteAtlas : IDisposable
	{
		public Texture2D Texture { get; private set; }
		public Rectangle[] Quads { get; private set; }

		public Point SrcPosition { get; private set; }
		public int CellWidth { get; private set; }
		public int CellHeight { get; private set; }
		public int Width { get { return Texture.Width; } }
		public int Height { get { return Texture.Height; } }
		public Color Color;
		public SpriteEffects Flip;

		public bool Disposed { get; private set; }

		public SpriteAtlas(Texture2D texture, Point pos, int w, int h, int quantity=0)
		{
			SetTexture(texture);
			SrcPosition = pos;
			CellWidth = w;
			CellHeight = h;
			Color = Color.White;
			Flip = SpriteEffects.None;

			if(quantity == 0) quantity = Width/CellWidth * Height/CellHeight;

			Quads = new Rectangle[quantity];

			for(int i=0; i<quantity; i++)
			{
				Quads[i] = new Rectangle(SrcPosition.X + ((CellWidth * i) % Width),
						SrcPosition.Y + (CellHeight * (int)(i / (Width/CellWidth))),
						CellWidth,
						CellHeight
				);

			}

		}

		~SpriteAtlas()
		{
			Dispose(true);
		}

		public void SetTexture(Texture2D texture)
		{
			if(texture == null) throw new System.ArgumentNullException("texture is null");
			Texture = texture;
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
					Texture.Dispose();
					Disposed = true;
				}
			}
		}
	}
}
