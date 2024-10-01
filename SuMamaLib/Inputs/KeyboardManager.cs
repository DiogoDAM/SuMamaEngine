namespace SuMamaLib.Inputs.Keyboard
{
using Microsoft.Xna.Framework.Input;

	public class KeyboardManager
	{
		private KeyboardState _prev = Keyboard.GetState();
		private KeyboardState _curr = Keyboard.GetState();

		public KeyboardManager() { }

		public void Update()
		{
			_prev = _curr;
			_curr = Keyboard.GetState();
		}

		public bool KeyIsPressed(Keys key)
		{
			return _curr.IsKeyDown(key);
		}

		public bool KeyIsReleased(Keys key)
		{
			return _curr.IsKeyUp(key);
		}

		public bool KeyWasPressed(Keys key)
		{
			return _curr.IsKeyDown(key) && _prev.IsKeyUp(key);
		}

		public bool KeyWasReleased(Keys key)
		{
			return _curr.IsKeyUp(key) && _prev.IsKeyDown(key);
		}

		public bool KeysArePressed(Keys[] keys)
		{
			foreach(Keys key in keys)
			{
				if(_curr.IsKeyUp(key)) { return false; }
			}

			return true;
		}
	}
}
