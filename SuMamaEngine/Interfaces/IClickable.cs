using Microsoft.Xna.Framework;

using System;

namespace SuMamaEngine
{
	public interface IClickable
	{
		public event Action StartClicking;
		public event Action Clicking;
		public event Action Clicked;
	}
}
