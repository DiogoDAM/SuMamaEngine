using Microsoft.Xna.Framework.Graphics;

namespace SuMamaLib
{
	public sealed class Font
	{
		public SpriteFont SpriteFont { get; private set; }
		public int Size { get; private set; }

        public Font(SpriteFont font, int size)
		{
			if(font == null) throw new System.NullReferenceException("The SpriteFont is null");
			SpriteFont = font;
			Size = size;
		}

    }
}
