using System;
using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public struct Animation
	{
		private AnimationFrame[] _frames;
		private int _frameWidth;
		private int _frameHeight;
		private int _framesCount;

		private float _currentFrameTime;

		public AnimationFrame CurrentFrame { get; private set; }
		public int AmountOfFrames { get; private set; }
		public AnimationState State { get; private set; }
		public bool Looping;

		public Animation(Sprite sprite, int amountOfFrames, float timeBetweenFrames, Point startPos, bool looping=false, bool isVertical=false)
		{
			_frameWidth = sprite.Width;
			_frameHeight = sprite.Height;
			_framesCount = 0;
			_currentFrameTime = 0f;
			AmountOfFrames = amountOfFrames;
			Looping = looping;
			State = AnimationState.Stopped;

			_frames = new AnimationFrame[AmountOfFrames];

			if(!isVertical)
			{
				for(int i=0; i<AmountOfFrames; i++)
				{
					_frames[i] = new AnimationFrame(new Rectangle(startPos.X + i * _frameWidth, startPos.Y, _frameWidth, _frameHeight), timeBetweenFrames );
				}
			}
			else
			{
				for(int i=0; i<AmountOfFrames; i++)
				{
					_frames[i] = new AnimationFrame(new Rectangle(startPos.X, startPos.Y + i * _frameHeight, _frameWidth, _frameHeight), timeBetweenFrames );
				}
			}
			CurrentFrame = _frames[_framesCount];
		}

		public Animation(Sprite sprite, int amountOfFrames, float[] timesPerFrame, Point startPos, bool looping=false, bool isVertical=false)
		{
			if(sprite == null) throw new ArgumentNullException("Animation() sprite is null");
			_frameWidth = sprite.Width;
			_frameHeight = sprite.Height;
			_framesCount = 0;
			_currentFrameTime = 0f;
			Looping = looping;
			AmountOfFrames = amountOfFrames;
			State = AnimationState.Stopped;

			_frames = new AnimationFrame[AmountOfFrames];

			if(!isVertical)
			{
				for(int i=0; i<AmountOfFrames; i++)
				{
					_frames[i] = new AnimationFrame(new Rectangle(startPos.X + i * _frameWidth, startPos.Y, _frameWidth, _frameHeight), timesPerFrame[i] );
				}
			}
			else
			{
				for(int i=0; i<AmountOfFrames; i++)
				{
					_frames[i] = new AnimationFrame(new Rectangle(startPos.X, startPos.Y + i * _frameHeight, _frameWidth, _frameHeight), timesPerFrame[i] );
				}
			}
			CurrentFrame = _frames[_framesCount];
		}

		public void Update()
		{
			if(State == AnimationState.Running)
			{
				_currentFrameTime += Globals.DeltaTime;

				if(_currentFrameTime >= CurrentFrame.Duration)
				{
					_currentFrameTime = 0f;
					_framesCount++;
					if(_framesCount == AmountOfFrames)
					{
						_framesCount = 0;
						if(!Looping) State = AnimationState.IsOver;
					}
					CurrentFrame = _frames[_framesCount];
				}
			}
		}

		public void Stop()
		{
			State = AnimationState.Stopped;
		}

		public void Resume()
		{
			State = AnimationState.Running;
		}

		public void Restart()
		{
			_framesCount = 0;
		}
	}
}
