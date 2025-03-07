using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SuMamaLib
{
	public static class Globals
	{
		public static SpriteBatch SpriteBatch { get; private set; }
		public static ContentManager Content { get; private set; }
		public static float DeltaTime { get; private set; }
		public static float Fps { get; private set; }
		public static float FrameCount { get; private set; }
		public static Font DefaultFont;
		public static int WindowWidth, WindowHeight;

		private static GraphicsDeviceManager _graphics;
		private static float _elapsedGameTime;


		public static void Load(GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice, ContentManager content)
		{
			SpriteBatch = new SpriteBatch(graphicsDevice);
			_graphics = graphics;
			Content = content;
		}

		public static void ResizeWindow(int width, int height)
		{
			WindowWidth = width;
			WindowHeight = height;
			_graphics.PreferredBackBufferWidth = WindowWidth;
			_graphics.PreferredBackBufferHeight = WindowHeight;
			_graphics.ApplyChanges();
		}

		public static void Update(GameTime gameTime)
		{
			DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

			_elapsedGameTime += DeltaTime;
			FrameCount++;

			if(_elapsedGameTime >= 1f)
			{
				Fps = FrameCount;
				FrameCount = 0;
				_elapsedGameTime -= 1;
			}
		}

	}
}
