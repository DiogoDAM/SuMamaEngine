using Microsoft.Xna.Framework;

using System;

namespace SuMamaLib
{
	public class UiTexturedButton : UiComponent, IClickable, IHoverable
	{
		public Sprite Sprite { get; protected set; }

        public event Action StartClicking;
        public event Action Clicking;
        public event Action Clicked;
        public event Action StartHovering;
        public event Action Hovering;
        public event Action Hovered;
		public event Action OutHovered;

        protected bool _hovering;

		protected bool _borderOn;
		public int BorderThickness { get; private set; }
		public Color BorderColor { get; private set; }
		public Vector2 BorderOffset;

		public UiTexturedButton(Transform trans, int w, int h, Sprite sprite): base(trans)
		{
			Sprite = sprite;
			Width = w;
			Height = h;
		}

		public UiTexturedButton(int w, int h, Sprite sprite): base()
		{
			Sprite = sprite;
			Width = w;
			Height = h;
		}

		public UiTexturedButton(Transform trans, int w, int h): base(trans)
		{
			Width = w;
			Height = h;
		}

		public UiTexturedButton(int w, int h): base()
		{
			Width = w;
			Height = h;
		}

		public void SetBorder(int thickness, Color color)
		{
			_borderOn = true;
			BorderThickness = thickness;
			BorderColor = color;
		}

		public void DisableBorder()
		{
			_borderOn = false;
		}

		public override void Start()
		{
			base.Start();

			StartProcess();
		}

		public override void Update()
		{
			ProcessInput();

			base.Update();
			ProcessInput();
		}

		public override void Draw()
		{
			if(!Float)
			{
				Globals.SpriteBatch.Draw(Sprite.Texture, GlobalPosition, Sprite.Bounds, Color, Transform.Rotation, Origin, Transform.Scale, Flip, Depth);
				if(_borderOn)
				{
					Drawer.DrawLineRectangle(Transform.GlobalPosition + BorderOffset, Width, Height, BorderColor, BorderThickness);
				}
			}
			else
			{
				Globals.SpriteBatch.Draw(Sprite.Texture, Position, Sprite.Bounds, Color, Transform.Rotation, Origin, Transform.Scale, Flip, Depth);
				if(_borderOn)
				{
					Drawer.DrawLineRectangle(Position + BorderOffset, Width, Height, BorderColor, BorderThickness);
				}
			}
			base.Draw();
			DrawProcess();
		}

        private void ProcessInput()
        {
			// Check Hover

			if(!Bounds.Intersects(new Rectangle(Input.Mouse.Position.ToPoint(), new Point(1,1))))
			{
				OutHovered?.Invoke();
			}
			else
			{
				if(!_hovering && Bounds.Intersects(new Rectangle(Input.Mouse.Position.ToPoint(), new Point(1,1))))
				{
					_hovering = true;
					StartHovering?.Invoke();
				}

				if(_hovering)
				{
					Hovering?.Invoke();
				}
			}


			if(_hovering && !Bounds.Intersects(new Rectangle(Input.Mouse.Position.ToPoint(), new Point(1,1))))
			{
				_hovering = false;
				Hovered?.Invoke();
			}

			// Check Click

			if(_hovering && Input.Mouse.ButtonWasPressed(0))
			{
				StartClicking?.Invoke();
			}

			if(_hovering && Input.Mouse.ButtonIsPressed(0))
			{
				Clicking?.Invoke();
			}

			if(!_hovering || Input.Mouse.ButtonWasReleased(0))
			{
				Clicked?.Invoke();
			}

        }
    }
}
