using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public class HorizontalLineEmitter : IEmitter
	{
		public Vector2 EmitterPosition { get { return new Vector2(Utilities.RandomFloat(StartX, EndX), Y); } }

		public float Y;
		public float StartX, EndX;

		public HorizontalLineEmitter(float start, float end, float y)
		{
			StartX = start;
			EndX = end;
			Y = y;
		}
	}
}
