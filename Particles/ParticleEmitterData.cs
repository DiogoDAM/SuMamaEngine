namespace SuMamaEngine
{
	public struct ParticleEmitterData
	{
		public ParticleData ParticleData;
		public float AngleVariance;
		public float LifeSpanMin;
		public float LifeSpanMax;
		public float SpeedMin;
		public float SpeedMax;
		public float Interval;
		public float EmitCount;

		public ParticleEmitterData(ParticleData data, float interval, float emitCount)
		{
			ParticleData = data;
			Interval = interval;
			EmitCount = emitCount;
		}
	}
}
