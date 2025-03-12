using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public struct ParticleData
	{
		public Sprite Sprite;

		public Vector2 Position;
		public float Speed;

		public Vector2 StartScale;
		public Vector2 EndScale;
		public float StartRotation;
		public float EndRotation;
		public Color StartColor;
		public Color EndColor;
		public float StartOpacity;
		public float EndOpacity;
		public float StartAngle;
		public float EndAngle;

		public float LifeTime;
	}
}
