using Microsoft.Xna.Framework;

namespace SuMamaLib
{
	public class UiLabel : UiComponent
	{
		public Font Font { get { return SpriteText.Font; } set { SpriteText.SetFont(value); } }
		public SpriteText SpriteText { get; protected set; }
		public string Text { get { return SpriteText.Text; } set { SpriteText.SetText(value); } }

		public UiLabel(Transform trans, Font font, string text) : base()
		{
			Font = font;
			Text = text;
			Transform = trans;
		}

		public UiLabel(Transform trans, Font font, string text, Color color) : base()
		{
			Font = font;
			Text = text;
			Transform = trans;
			Color = color;
		}

		public override void Draw()
		{
			Globals.SpriteBatch.DrawString(Font.SpriteFont, Text, Transform.GlobalPosition, Color);

			base.Draw();
			DrawProcess();
		}
	}
}
