using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public class MouseEmitter : IEmitter
	{
		public Vector2 EmitterPosition { get { return Input.Mouse.Position; } }
	}
}
