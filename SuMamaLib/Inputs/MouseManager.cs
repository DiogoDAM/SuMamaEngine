using Microsoft.Xna.Framework;


namespace SuMamaLib.Inputs.Mouse
{
using Microsoft.Xna.Framework.Input;

	public class MouseManager
	{
		private MouseState _prev = Mouse.GetState();
		private MouseState _curr = Mouse.GetState();

		public int WheelValue { get { return _curr.ScrollWheelValue / 120; } }
		public Vector2 Position { get { return _curr.Position.ToVector2(); } }

		public MouseManager() {}

		public void Update()
		{
			_prev = _curr;
			_curr = Mouse.GetState();
		}

		public bool WasMoved()
		{
			return _prev.Position - _curr.Position != new Point(0,0);
		}

		public bool LmbIsPressed()
		{
			return _curr.LeftButton == ButtonState.Pressed;
		}

		public bool LmbIsReleased()
		{
			return _curr.LeftButton == ButtonState.Released;
		}

		public bool LmbWasPressed()
		{
			return _curr.LeftButton == ButtonState.Pressed && _prev.LeftButton == ButtonState.Released;
		}

		public bool LmbWasReleased()
		{
			return _curr.LeftButton == ButtonState.Released && _prev.LeftButton == ButtonState.Pressed;
		}

		public bool RmbIsPressed()
		{
			return _curr.RightButton == ButtonState.Pressed;
		}

		public bool RmbIsReleased()
		{
			return _curr.RightButton == ButtonState.Released;
		}

		public bool RmbWasPressed()
		{
			return _curr.RightButton == ButtonState.Pressed && _prev.RightButton == ButtonState.Released;
		}

		public bool RmbWasReleased()
		{
			return _curr.RightButton == ButtonState.Released && _prev.RightButton == ButtonState.Pressed;
		}

		public bool MmbIsPressed()
		{
			return _curr.MiddleButton == ButtonState.Pressed;
		}

		public bool MmbIsReleased()
		{
			return _curr.MiddleButton == ButtonState.Released;
		}

		public bool MmbWasPressed()
		{
			return _curr.MiddleButton == ButtonState.Pressed && _prev.MiddleButton == ButtonState.Released;
		}

		public bool MmbWasReleased()
		{
			return _curr.MiddleButton == ButtonState.Released && _prev.MiddleButton == ButtonState.Pressed;
		}
	}
}

