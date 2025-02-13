using Microsoft.Xna.Framework;

namespace SuMamaLib
{
	public class UiSimpleProgressBar : UiComponent
	{
		public Color BarColor; 
		public float StartValue;
		public float EndValue;
		public float CurrentValue;
		public float StepValue;

		public UiSimpleProgressBar(int w, int h, Color color, Color barColor) :base()
		{
			Width = w;
			Height = h;
			Color = color;
			BarColor = barColor;
			StartValue = 0f;
			CurrentValue = 0f;
			StepValue = 0.1f;
			EndValue = 100f;
		}

		public UiSimpleProgressBar(Transform trans, int w, int h, Color color, Color barColor): base(trans)
		{
			Width = w;
			Height = h;
			Color = color;
			BarColor = barColor;
			StartValue = 0f;
			CurrentValue = 0f;
			StepValue = 0.1f;
			EndValue = 100f;
		}

		public void SetValues(float startValue, float endValue, float stepValue)
		{
			StartValue = startValue;
			CurrentValue = startValue;
			EndValue = endValue;
			StepValue = stepValue;
		}

		public override void Update()
		{
			if(CurrentValue < EndValue)
			{
				CurrentValue += StepValue;
			}

			base.Update();
		}

		public override void Draw()
		{
			Drawer.DrawFillRectangle(GlobalPosition, Width, Height, Color);
			Drawer.DrawFillRectangle(GlobalPosition, (int)(CurrentValue * 100 / Width), Height, BarColor);

			base.Draw();
		}
	}
}
