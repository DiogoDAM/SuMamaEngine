using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public class PointEmitter : IEmitter
	{
		public Vector2 EmitterPosition { get; }

		public PointEmitter(Vector2 pos)
		{
			EmitterPosition = pos;
		}
	}
}
