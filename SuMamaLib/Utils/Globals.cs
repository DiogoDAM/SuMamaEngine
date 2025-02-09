using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SuMamaLib
{
	public static class Globals
	{
		public static SpriteBatch SpriteBatch;
		public static ContentManager Content;
		public static float DeltaTime;
		public static int WindowWidth, WindowHeight;
		public static Font DefaultFont;

		private static GraphicsDeviceManager _graphics;

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
		}

	}
}
