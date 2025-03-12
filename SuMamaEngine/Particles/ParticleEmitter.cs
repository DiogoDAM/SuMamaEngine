using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public class ParticleEmitter
	{
		private IEmitter _emitter;
		private ParticleEmitterData _data;
		private float _intervalLeft;
		private float _depth;

		public ParticleEmitter(IEmitter emitter, ParticleEmitterData data, float depth)
		{
			_emitter = emitter;
			_data = data;
			_intervalLeft = _data.Interval;
			_depth = depth;
		}

		public void Update()
		{
			_intervalLeft -= Globals.DeltaTime;
			while(_intervalLeft <= 0f)
			{
				_intervalLeft += _data.Interval;
				Vector2 pos = _emitter.EmitterPosition; 
				for(uint i=0; i<_data.EmitCount; i++)
				{
					Emit(pos);
				}
			}
		}

		private void Emit(Vector2 pos)
		{
			ParticleData pd = _data.ParticleData;
			pd.Position = pos;
			pd.LifeTime = Utilities.RandomFloat(_data.LifeSpanMin, _data.LifeSpanMax);
			pd.Speed = Utilities.RandomFloat(_data.SpeedMin, _data.SpeedMax);
			float r = (float) Utilities.Random.NextDouble() * 2 - 1;
			pd.StartAngle += _data.AngleVariance * r;

			Particle p = new(pd, _depth);
			ParticleManager.Instance.AddParticle(p);
		}
	}
}
