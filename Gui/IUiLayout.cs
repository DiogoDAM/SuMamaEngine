using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public interface IUiLayout
	{
		public Vector2 ComponentsMargin { get; set; }

		public void ProcessLayout(IEnumerable<UiComponent> components);
	}
}
