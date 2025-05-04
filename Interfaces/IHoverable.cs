using System;
using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public interface IHoverable
	{
		public event Action StartHovering;
		public event Action Hovering;
		public event Action Hovered;
		public event Action OutHovering;
	}
}
