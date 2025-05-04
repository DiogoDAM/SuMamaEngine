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
			if(_animations.ContainsKey(name)) _animations.Remove(name);
		}

		public bool ContainsAnimation(string name)
		{
			return _animations.ContainsKey(name);
		}

		public void ClearAnimation()
		{
			_animations.Clear();
		}

		public void Update()
		{
			_currentAnimation.Update();
		}

		public void Play(string name)
		{
			if(!_animations.ContainsKey(name)) throw new Exception($"Animator.Play() of the {ObjectId} don't have the {name} animation");
			_currentAnimation = _animations[name];
		}

		public void Resume()
		{
			_currentAnimation.Resume();
		}

		public void Stop()
		{
			_currentAnimation.Stop();
		}

		public void Restart()
		{
			_currentAnimation.Restart();
		}

		public Animation GetCurrentAnimation()
		{
			return _currentAnimation;
		}

		public AnimationFrame GetFrame()
		{
			return _currentAnimation.CurrentFrame;
		}

		public Rectangle GetFrameBounds()
		{
			return _currentAnimation.CurrentFrame.Bounds;
		}

	}
}
