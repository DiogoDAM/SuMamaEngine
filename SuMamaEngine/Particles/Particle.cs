using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuMamaEngine
{
	public class Particle : IDisposable
	{
		private Sprite _sprite;
		private Color _color;
		private Vector2 _pos;
		private Vector2 _dir;
		private float _speed;

		private Vector2 _origin;
		private Vector2 _scale;
		private float _rot;
		private float _angle;
		private float _opacity;
		private float _depth;
		private float _amount;

		public ParticleData Data { get; private set; }

		public float LifeTime;
		public float LifeTimeLeft;

		public bool IsFinished => LifeTimeLeft <= 0;

		public Particle(ParticleData data, float depth=1f)
		{
			Data = data;
			LifeTime = Data.LifeTime;
			LifeTimeLeft = LifeTime;
			_sprite = Data.Sprite;
			_color = Data.StartColor;
			_pos = Data.Position;
			_speed = Data.Speed;
			_origin = Vector2.Zero;
			_scale = Data.StartScale;
			_rot = Data.StartRotation;
			_opacity = Data.StartOpacity;
			_depth = depth;

		}


		public void Update()
		{
			LifeTimeLeft -= Globals.DeltaTime;

			_amount = (float) LifeTimeLeft * 1f / LifeTime;

			_color = Color.Lerp(Data.EndColor, Data.StartColor, _amount);
			_opacity = MathHelper.Clamp(MathHelper.Lerp(Data.EndOpacity, Data.StartOpacity, _amount), 0, 1);
			_scale.X = MathHelper.Lerp(Data.EndScale.X, Data.StartScale.X, _amount);
			_scale.Y = MathHelper.Lerp(Data.EndScale.Y, Data.StartScale.Y, _amount);
			_angle = MathHelper.ToRadians(MathHelper.Lerp(Data.EndAngle, Data.StartAngle, _amount));
			_dir = new Vector2((float)Math.Cos(_angle), (float)Math.Sin(_angle));
			_pos += _dir * _speed * Globals.DeltaTime;

		}

		public void Draw()
		{
			Globals.SpriteBatch.Draw(_sprite.Texture, _pos, _sprite.Bounds, _color * _opacity, _rot, _origin, _scale, SpriteEffects.None, _depth);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposable)
		{
			if(disposable)
			{
				_sprite.Dispose();
			}
		}

	}
}

