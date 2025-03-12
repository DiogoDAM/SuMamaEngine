
using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public class UiPanel : UiComponent
	{
		public Sprite Sprite { get; protected set; }

		public UiPanel(int w, int h, Sprite sprite) : base()
		{
			Width = w;
			Height = h;
			Sprite = sprite;
		}

		public UiPanel(int w, int h) : base()
		{
			Width = w;
			Height = h;
		}

		public UiPanel(Transform trans, int w, int h, Sprite sprite) : base(trans)
		{
			Width = w;
			Height = h;
			Sprite = sprite;
		}

		public UiPanel(Transform trans, int w, int h) : base(trans)
		{
			Width = w;
			Height = h;
		}

		public override void Draw()
		{
			if(!Float)
			{
				Globals.SpriteBatch.Draw(Sprite.Texture, GlobalPosition, Sprite.Bounds, Color.White, Transform.Rotation, Origin, Transform.Scale, Flip, Depth);
			}
			else
			{
				Globals.SpriteBatch.Draw(Sprite.Texture, Position, Sprite.Bounds, Color.White, Transform.Rotation, Origin, Transform.Scale, Flip, Depth);
			}

			base.Draw();
		}

		protected override void Dispose(bool disposable)
		{
			if(disposable)
			{
				if(!Disposed)
				{
					Sprite.Dispose();

					Disposed = true;
				}
			}
		}
	}
}
