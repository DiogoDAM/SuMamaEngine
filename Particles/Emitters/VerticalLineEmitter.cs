using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public class VerticalLineEmitter : IEmitter
	{
		public Vector2 EmitterPosition { get { return new Vector2(X, Utilities.RandomFloat(StartY, EndY)); } }

		public float X;
		public float StartY, EndY;

		public VerticalLineEmitter(float start, float end, float x)
		{
			StartY = start;
			EndY = end;
			X = x;
		}
	}
}
