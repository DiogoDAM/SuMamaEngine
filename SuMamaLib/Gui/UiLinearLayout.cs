using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SuMamaLib
{
    public sealed class UiLinearLayout : IUiLayout
    {
		public Vector2 ComponentsMargin { get; set; }
		private UiComponent _component;

		public UiLinearLayout(UiComponent parent, Vector2 margin)
		{
			ComponentsMargin = margin;
			_component = parent;
		}

        public void ProcessLayout(IEnumerable<UiComponent> components)
        {
			int count = 0;
			foreach(UiComponent component in components)
			{
				if(!component.Float)
				{
					float x = (ComponentsMargin.X * count) + (component.Width * count) + (component.Margin.X * count);
					component.LayoutOffset.X = x;
					count++;
				}
			}
        }
    }
}
