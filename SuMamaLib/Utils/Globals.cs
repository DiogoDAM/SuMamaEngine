using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SuMamaLib.Utils
{
	public static class Globals
	{
		public static SpriteBatch SpriteBatch;
		public static ContentManager Content;
		public static float DeltaTime;

		public static void Initialize(GraphicsDevice graphics, ContentManager content)
		{
			SpriteBatch = new SpriteBatch(graphics);
			Content = content;
		}

		public static void Update(GameTime gameTime)
		{
			DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
		}
	}
}

