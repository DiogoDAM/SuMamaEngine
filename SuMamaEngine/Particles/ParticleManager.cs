using System.Collections.Generic;

namespace SuMamaEngine
{
	public class ParticleManager
	{
		public static ParticleManager Instance { get { if(_instance == null) _instance = new(); return _instance; } }
		private static ParticleManager _instance;

		private List<Particle> _particles;
		private List<ParticleEmitter> _emitters;

		public int ParticleCount { get { return _particles.Count; } }
		public int ParticleEmitter { get { return _emitters.Count; } }

		private ParticleManager()
		{
			_particles = new();
			_emitters = new();
		}

		public void AddParticle(Particle p)
		{
			_particles.Add(p);
		}

		public void AddEmitter(ParticleEmitter emitter)
		{
			_emitters.Add(emitter);
		}

		public void Update()
		{
			UpdateParticles();
			UpdateEmitters();
		}

		public void Draw()
		{
			foreach(Particle p in _particles)
			{
				p.Draw();
			}
		}

		private void UpdateParticles()
		{
			foreach(Particle p in _particles)
			{
				p.Update();
			}

			_particles.RemoveAll((p) => p.IsFinished == true);
		}

		private void UpdateEmitters()
		{
			foreach(ParticleEmitter e in _emitters)
			{
				e.Update();
			}
		}
	}
}
