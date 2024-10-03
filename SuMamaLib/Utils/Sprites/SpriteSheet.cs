using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuMamaLib.Utils.Sprites
{
    public class SpriteSheet : Sprite
    {
		private Rectangle[] _sprites;

		public SpriteSheet(Texture2D texture, Rectangle srcRect, uint quant, bool hdirection=true)
		:base(texture, srcRect)
		{
			_sprites = new Rectangle[quant];
			for(int i=0; i<quant; i++)
			{
				if(hdirection) _sprites[i] = new Rectangle(srcRect.X + (i * srcRect.Width), srcRect.Y, srcRect.Width, srcRect.Height);
				else new Rectangle(srcRect.X, srcRect.Y + (i * srcRect.Height), srcRect.Width, srcRect.Height);
			}
		}

		public SpriteSheet(Texture2D texture, Point startPos, Point size, uint quant, bool hdirection=true)
		:base(texture, startPos, size)
		{
			_sprites = new Rectangle[quant];
			for(int i=0; i<quant; i++)
			{
				if(hdirection) _sprites[i] = new Rectangle(startPos.X + (i * size.X), startPos.Y, size.X, size.Y);
				else new Rectangle(startPos.X, startPos.Y + (i * size.Y), size.X, size.Y);
			}
		}

		public Rectangle GetBounds(int index)
		{
			return _sprites[index];
		}
    }
}
