using System;

using Microsoft.Xna.Framework;

using SuMamaLib.Utils;

namespace SuMamaLib.Utils.Sprites
{
	public class Animation
	{
		private bool _running = false;
		private bool _wasEnded = false;
		private int _currentFrame = 0;
		private float _currentFrameTime;
		public float FrameTime;
		public int FramesCount;
		public Point StartPos;
		public Point Size;

		public bool IsActive { get { return _running; }}
		public bool WasEnded { get { return _wasEnded; }}

		public int CurrentFrame { get { return _currentFrame; }}

		public Animation(Point pos, Point size, int quant, float time)
		{
			StartPos = pos;
			Size = size;
			FramesCount = quant;
			FrameTime = time;
			_currentFrameTime = FrameTime;
		}

		public Animation(Point pos, int w, int h, int quant, float time)
		{
			StartPos = pos;
			Size = new Point(w,h);
			FramesCount = quant;
			FrameTime = time;
			_currentFrameTime = FrameTime;
		}

		public void Stop()
		{
			_running = false;
		}

		public void Start()
		{
			_running = true;
			_currentFrame = 0;
		}

		public void Resume()
		{
			_running = true;
		}

		public void Restart()
		{
			_currentFrame = 0;
		}

		public void SetFrame(int index)
		{
			if(index > FramesCount){ throw new Exception("The index value is out of Frames Count");}
			_currentFrame = index;
		}

		public void Update()
		{
			if(_running)
			{
				_currentFrameTime -= Globals.DeltaTime;

				if(_currentFrameTime <= 0)
				{
					_currentFrame++;
					if(_currentFrame == FramesCount) {_wasEnded = true;}
					else {_wasEnded = false; }
					_currentFrame = (int) _currentFrame % FramesCount;
					_currentFrameTime = FrameTime;
				}
			}	
		}

		public Rectangle GetFrame(bool direction=true)
		{
			if(direction) return new Rectangle(StartPos.X + (_currentFrame * Size.X), StartPos.Y, Size.X, Size.Y);
			else return new Rectangle(StartPos.X, StartPos.Y + (_currentFrame * Size.Y), Size.X, Size.Y);
		}

	}
}
