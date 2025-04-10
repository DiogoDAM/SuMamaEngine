using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public class UiProgressBar : UiComponent
	{
		public float StartValue;
		public float EndValue;
		public float CurrentValue;
		public float StepValue;
		public bool IsActive;

		public Sprite BackgroundSprite;
		public Sprite BarSprite;

		public UiProgressBar(int w, int h, Color color) :base()
		{
			Width = w;
			Height = h;
			Color = color;
			StartValue = 0f;
			CurrentValue = 0f;
			StepValue = 0.1f;
			EndValue = 100f;
		}

		public UiProgressBar(Transform trans, int w, int h, Color color): base(trans)
		{
			Width = w;
			Height = h;
			Color = color;
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

		public void SetValuePercentage(float value)
		{
			CurrentValue = EndValue * value / 100;
		}

		public void AddValuePercentage(float value)
		{
			CurrentValue += EndValue * value / 100;
			System.Console.WriteLine(CurrentValue);
		}

		public void SetValue(float value)
		{
			CurrentValue = value;
		}

		public void AddValue(float value)
		{
			CurrentValue += value;
		}

		public override void Update()
		{
			if(IsActive)
			{
				if(CurrentValue < EndValue)
				{
					CurrentValue += StepValue;
					BarSprite.Width = (int)(CurrentValue * Width / EndValue);
				}
			}


			base.Update();
		}

		public override void Draw()
		{
			if(!Float)
			{
				Engine.SpriteBatch.Draw(BackgroundSprite.Texture, GlobalPosition, BackgroundSprite.Bounds, Color, Transform.Rotation, Origin, Transform.Scale, Flip, Depth);
				Engine.SpriteBatch.Draw(BarSprite.Texture, GlobalPosition, BarSprite.Bounds, Color, Transform.Rotation, Origin, Transform.Scale, Flip, Depth);
			}
			else
			{
				Engine.SpriteBatch.Draw(BackgroundSprite.Texture, Position, BackgroundSprite.Bounds, Color, Transform.Rotation, Origin, Transform.Scale, Flip, Depth);
				Engine.SpriteBatch.Draw(BarSprite.Texture, Position, BarSprite.Bounds, Color, Transform.Rotation, Origin, Transform.Scale, Flip, Depth);
			}

			base.Draw();
		}
	}
}
