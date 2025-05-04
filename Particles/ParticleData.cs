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
		public Vector2 Anchor;

		public float LifeTime;

		public ParticleData(Sprite sprite)
		{
			Sprite = sprite;
			Position = Vector2.One;
			Speed = 0f;
			LifeTime = 1f;
			StartScale = EndScale = Vector2.One;
			StartRotation = EndRotation = 0f;
			StartColor = EndColor = Color.White;
			StartOpacity = EndOpacity = 1f;
			StartAngle = EndAngle = 0f;
			Anchor = Vector2.Zero;
		}
	}
}
