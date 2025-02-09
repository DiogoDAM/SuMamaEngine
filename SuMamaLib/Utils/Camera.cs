using Microsoft.Xna.Framework;

namespace SuMamaLib
{
	public sealed class Camera
	{
		public Transform Transform;

		public Vector2 Position { get { return Transform.Position; } set { Transform.Position = value; } }
		public Vector2 Scale { get { return Transform.Scale; } set { Transform.Scale = value; } }
		public float Rotation { get { return Transform.Rotation; } set { Transform.Rotation = value; } }

		public Camera()
		{
			Transform = new Transform(Vector2.Zero);
		}

		public Camera(Transform trans)
		{
			Transform = trans;
		}

		public Camera(Vector2 pos)
		{
			Transform = new Transform(pos);
		}

		public Matrix GetMatrix()
		{
			return Matrix.CreateScale(Scale.X, Scale.Y, 1f) *
				Matrix.CreateRotationZ(Rotation) *
				Matrix.CreateTranslation(-Position.X, -Position.Y, 0f);
		}
	}
}
