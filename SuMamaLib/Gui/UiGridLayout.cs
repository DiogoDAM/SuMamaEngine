using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SuMamaLib
{
	public sealed class UiGridLayout : IUiLayout
	{
		public int Width, Height;
		public Vector2 ComponentsMargin { get; set; }
		private UiComponent _component;

		public UiGridLayout(UiComponent comp, int w, int h, Vector2 margin)
		{
			_component = comp;
			Width = w;
			Height = h;
			ComponentsMargin = margin;
		}

		public UiGridLayout(UiComponent comp, int w, int h)
		{
			_component = comp;
			Width = w;
			Height = h;
		}

		public void ProcessLayout(IEnumerable<UiComponent> components)
		{
			int countw = 0;
			int counth = 0;
			foreach(UiComponent component in components)
			{
				if(!component.Float)
				{
					float x = (component.Margin.X * countw) + (ComponentsMargin.X * countw) + (component.Width * countw);
					float y = (component.Margin.Y * counth) + (ComponentsMargin.Y * counth) + (component.Height * counth);
					component.LayoutOffset = new Vector2(x, y);

					countw++;
					if(countw >= Width) 
					{
						countw = 0;
						counth++;
						if(counth >= Height && !_component.Infinite) return;
					}
				}
			}
		}
	}
}
