using System;
using Microsoft.Xna.Framework;

namespace SuMamaLib
{
	public interface IHoverable
	{
		public event Action StartHovering;
		public event Action Hovering;
		public event Action Hovered;
		public event Action OutHovering;
	}
}
