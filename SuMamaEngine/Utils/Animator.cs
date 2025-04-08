using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public class Animator
	{
		private Dictionary<string, Animation> _animations;
		private Animation _currentAnimation;

		public int AmountOfAnimations { get { return _animations.Count; } }
		public string ObjectId;

		public Animator(string objectId)
		{
			_animations = new();
			ObjectId = objectId;
		}

		public void AddAnimation(string name, Animation anim)
		{
			if(string.IsNullOrEmpty(name)) throw new ArgumentNullException($"Animator.AddAnimation() of the {ObjectId} the name is null or empty");
			_animations.Add(name, anim);
		}

		public void RemoveAnimation(string name)
		{
		}

		public bool ContainsAnimation(string name)
		{
		}

		public void ClearAnimation()
		{
		}

		public void Update()
		{
		}

		public void Play(string name)
		{
		}

		public void Resume()
		{
		}

		public void Stop()
		{
		}

		public void Restart()
		{
		}

		public Animation GetCurrentAnimation()
		{
		}

		public AnimationFrame GetFrame()
		{
		}

		public Rectangle GetFrameBounds()
		{
		}

	}
}
