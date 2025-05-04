using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public class RectangleEmitter : IEmitter
	{
		public Vector2 EmitterPosition { get { return new Vector2(Utilities.RandomFloat(Position.X, Position.X + Width), Utilities.RandomFloat(Position.Y, Position.Y + Height)); } }

		public Vector2 Position;
		public int Width, Height;

		public RectangleEmitter(Vector2 pos, int w, int h)
		{
			Position = pos;
			Width = w;
			Height = h;
		}
    }
}
