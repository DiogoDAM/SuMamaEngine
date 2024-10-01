using SuMamaLib.Inputs.Keyboard;
using SuMamaLib.Inputs.Mouse;

namespace SuMamaLib.Inputs
{
	public static class Input
	{
		public static KeyboardManager Keyboard = new KeyboardManager();
		public static MouseManager Mouse = new MouseManager();

		public static void Update()
		{
			Keyboard.Update();
			Mouse.Update();
		}
	}
}
