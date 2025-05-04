using System;
using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public class TransitionFade : Transition
	{
		private Rectangle _bounds;
		private Color _color;
		private Color _start;
		private Color _end;

		public TransitionFade(float duration, Color start, Color end, Rectangle bounds) : base(duration)
		{
			_start = start;
			_end = end;
			_bounds = bounds;
			_color = start;
		}

		public override void Update()
		{
			base.Update();

			float amount = _currentTime * 1f / _duration;

			_color = Color.Lerp(_end, _start, amount);
		}

		public override void Draw()
		{
			Drawer.DrawFillRectangle(_bounds, _color);
		}
	}
}
