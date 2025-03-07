using Microsoft.Xna.Framework;

namespace SuMamaLib
{
	public class UiSimplePanel : UiComponent
	{
		public UiSimplePanel(int w, int h, Color color) : base()
		{
			Width = w;
			Height = h;
			Color = color;
		}

		public UiSimplePanel(Transform trans, int w, int h, Color color) : base(trans)
		{
			Width = w;
			Height = h;
			Color = color;
		}

		public override void Draw()
		{
			if(!Float)
			{
				Drawer.DrawFillRectangle(GlobalPosition, Width, Height, Color);
			}
			else
			{
				Drawer.DrawFillRectangle(Position, Width, Height, Color);
			}

			base.Draw();
		}
	}
}
