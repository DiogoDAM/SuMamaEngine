using System;
using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public abstract class Transition
	{
		protected float _duration;
		protected float _currentTime;
		
		public bool IsRunning { get; protected set; }
		public bool IsCompleted { get; protected set; }

		public Transition(float duration)
		{
			_duration = duration;
			_currentTime = 0f;
			IsRunning = true;
		}

		public virtual void Update()
		{
			_currentTime += Globals.DeltaTime;

			if(_currentTime >= _duration)
			{
				IsRunning = false;
				IsCompleted = true;
			}
		}

		public virtual void Draw()
		{
		}

		public void Reset()
		{
			_currentTime = 0f;
			IsRunning = true;
			IsCompleted = false;
		}
	}
}
