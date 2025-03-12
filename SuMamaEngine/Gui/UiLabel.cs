using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public class UiLabel : UiComponent
	{
		public SpriteText SpriteText { get; protected set; }
		public string Text { get { return SpriteText.Text; } set { SpriteText.SetText(value); } }
		public Font Font { get { return SpriteText.Font; } set { SpriteText.SetFont(value); } }
		public Vector2 Spacing;
		public int MaxLetterPerLine = 20;

		public UiLabel(Font font, string text) : base()
		{
			SpriteText = new SpriteText(font, text);
		}

		public UiLabel(Transform trans, Font font, string text, Color color) : base()
		{
			SpriteText = new SpriteText(font, text);
			Transform = trans;
			Color = color;
		}

		public void SetHalignToLeft()
		{
			Spacing = new Vector2(0, 0);
		}

		public void SetHalignToCenter()
		{
			int aux = SpriteText.Text.Length >= MaxLetterPerLine ? MaxLetterPerLine : SpriteText.Text.Length;
			Spacing = new Vector2(aux*0.5f * SpriteText.Font.Size, 0);
		}

		public void SetHalignToRight()
		{
			int aux = SpriteText.Text.Length >= MaxLetterPerLine ? MaxLetterPerLine : SpriteText.Text.Length;
			Spacing = new Vector2(aux * SpriteText.Font.Size, 0);
		}

		public override void Draw()
		{
			if(!Float)
			{
				Globals.SpriteBatch.DrawString(SpriteText.Font.SpriteFont, SpriteText.Text, GlobalPosition, Color, Transform.Rotation, Spacing, Transform.Scale, SpriteText.Flip, 1f);
			}
			else
			{
				Globals.SpriteBatch.DrawString(SpriteText.Font.SpriteFont, SpriteText.Text, Position, Color);
			}

			base.Draw();
			DrawProcess();
		}
	}
}
