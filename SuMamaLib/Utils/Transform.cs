using Microsoft.Xna.Framework;

namespace SuMamaLib.Utils
{
    public class Transform
    {
        public Vector2 Position;
        public float Rotation;
        public Vector2 Scale;

        public Transform(Vector2 pos)
        {
            Position = pos;
            Rotation = 0f;
            Scale = Vector2.One;
        }

        public Transform(Vector2 pos, float rot)
        {
            Position = pos;
            Rotation = rot;
            Scale = Vector2.One;
        }

        public Transform(Vector2 pos, float rot, Vector2 s)
        {
            Position = pos;
            Rotation = rot;
            Scale = s;
        }

        public void Translate(Vector2 pos)
        {
            Position += pos;
        }

        public void Translate(float x, float y)
        {
            Position.X += x;
            Position.Y += y;
        }
    }
}