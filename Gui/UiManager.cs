using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public abstract class UiManager : UiComponent
	{
		public UiManager(Transform trans) : base(trans)
		{
		}

		public UiManager() : base()
		{
			Width = Engine.WindowWidth;
			Height = Engine.WindowHeight;
		}

		public bool CursorCrossesAnElement()
		{
			Rectangle cursorBound = new Rectangle(Input.Mouse.Position.ToPoint(), new Point(1,1));
			foreach(UiComponent child in _children)
			{
				if(child.Bounds.Intersects(cursorBound) || child.LocalBounds.Intersects(cursorBound)) return true;
			}

			return false;
		}
	}
}
