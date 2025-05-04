using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public struct AnimationFrame
	{
		public Rectangle Bounds { get; private set; }
		public float Duration { get; private set; }

		public AnimationFrame(Rectangle bounds, float duration)
		{
			Bounds = bounds;
			Duration = duration;
		}

		public override string ToString()
		{
			return $"AnimationFrame: (Bounds: {Bounds.ToString()}, Duration: {Duration})";
		}
	}
}
