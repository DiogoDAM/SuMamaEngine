using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuMamaEngine
{
	public struct SpriteText
	{
		public Font Font { get; private set; }
		public string Text { get; private set; }

		public int Size { get { return Text.Length * Font.Size; } }

		public Color Color;
		public SpriteEffects Flip;

		public SpriteText(Font font)
		{
			SetFont(font);
			SetText("Lorem Ipsum Dolor");
		}

		public SpriteText(Font font, string text)
		{
			SetFont(font);
			SetText(text);
		}


		public void SetFont(Font font)
		{
			if(font == null) throw new System.NullReferenceException("Font is null");
			Font = font;
		}

		public void SetText(string text)
		{
			if(string.IsNullOrEmpty(text)) throw new System.NullReferenceException("The text is null or empty");
			Text = text;
		}
	}
}
