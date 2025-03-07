using Microsoft.Xna.Framework;

namespace SuMamaLib
{
	public class UiHotbar : UiComponent
	{
		protected UiComponent[] _items;
		public int ItemsQuantity { get; protected set; }
		public IUiLayout HotbarLayout;
		public UiComponent ActiveItem;
		public Sprite Sprite;

		public UiHotbar(int quant, int w, int h, Color color) : base()
		{
			ItemsQuantity = quant;
			_items = new UiComponent[quant];
			HotbarLayout = new UiLinearLayout(this, Vector2.Zero);
			Width = w;
			Height = h;
			Color = color;
		}

		public UiHotbar(Transform trans, int quant, int w, int h, Color color) : base(trans)
		{
			ItemsQuantity = quant;
			_items = new UiComponent[quant];
			HotbarLayout = new UiLinearLayout(this, Vector2.Zero);
			Width = w;
			Height = h;
			Color = color;
		}

		public UiComponent GetHotbarItem(int index)
		{
			return _items[index];
		}

		public void SetHotbarItem(int index, UiComponent comp)
		{
			_items[index] = comp;
			comp.Parent = this;
			comp.Transform.Parent = Transform;
		}

		public void RemoveHotbarItem(int index)
		{
			_items[index].Parent = null;
			_items[index] = null;
			_items[index].Transform.Parent = null;
		}

		public void OrganizeItems()
		{
			HotbarLayout.ProcessLayout(_items);
		}

		public override void Start()
		{
			foreach(UiComponent item in _items)
			{
				item.Start();
			}

			base.Start();
		}

		public override void Update()
		{
			foreach(UiComponent item in _items)
			{
				item.Update();
			}

			base.Update();
		}

		public override void Draw()
		{
			if(!Float)
			{
				Globals.SpriteBatch.Draw(Sprite.Texture, GlobalPosition, Sprite.Bounds, Color, Transform.Rotation, Origin, Transform.Scale, Flip, Depth);
			}
			else
			{
				Drawer.DrawFillRectangle(Position, Width, Height, Color);
			}

			foreach(UiComponent item in _items)
			{
				item.Draw();
			}

			base.Draw();
		}
	}
}
