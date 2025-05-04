using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public class QuadEmitter : IEmitter
	{
		public Vector2 EmitterPosition { get { return new Vector2(Utilities.Random.Next(200, 300), Utilities.Random.Next(200, 300)); } }
    }
}
