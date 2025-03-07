using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuMamaLib
{
	public abstract class UiComponent : IDisposable
	{
		protected List<UiComponent> _children;
		public UiComponent Parent;
		public IUiLayout Layout { protected get; set; }

		public bool Disposed { get; protected set; }

		public Vector2 LayoutOffset;
		public float MarginLeft, MarginRight, MarginTop, MarginBottom;
		public Vector2 Margin { get { return new Vector2(MarginLeft + MarginRight, MarginTop + MarginBottom); } }
		public bool Infinite;
		public bool Float=false;

		public Transform Transform;
		public Color Color;
		public float Depth;
		public Vector2 Origin;
		public SpriteEffects Flip;

		public Vector2 GlobalPosition { get { return Transform.GlobalPosition + LayoutOffset + new Vector2(MarginLeft, MarginTop); } }
		public Vector2 Position { get { return Transform.GlobalPosition + new Vector2(MarginLeft, MarginTop); } }
		public int Width, Height;
		public Rectangle Bounds { get { return new Rectangle(GlobalPosition.ToPoint(), new Point(Width, Height)); } }
		public Rectangle LocalBounds { get { return new Rectangle(Position.ToPoint(), new Point(Width, Height)); } }


		public UiComponent(Transform trans) : base()
		{
			_children = new();
			Transform = trans;
		}

		public UiComponent() : base()
		{
			_children = new();
			Transform = new(Vector2.Zero);
		}

		public void SetParent(UiComponent parent)
		{
			Parent = parent;
			if(parent == null)
			{
				Transform.Parent = null;
			}
			else
			{
				Transform.Parent = parent.Transform;
			}
		}

		public void CentralizeHorizontal()
		{
			if(Parent == null && !Float) return;

			Transform.Position.X += Parent.Width/2 - Width/2;
		}

		public void CentralizeVertical()
		{
			if(Parent == null && !Float) return;

			Transform.Position.Y += Parent.Height/2 - Height/2;
		}

		public virtual void Start()
		{
			StartProcess();
		}

		public virtual void Update()
		{
			UpdateProcess();
		}

		public virtual void Draw()
		{
			DrawProcess();
		}

		public void StartProcess()
		{
			foreach(UiComponent child in _children)
			{
				child.Start();
			}
		}

		public void UpdateProcess()
		{
			foreach(UiComponent child in _children)
			{
				child.Update();
			}
		}

		public void DrawProcess()
		{
			foreach(UiComponent child in _children)
			{
				child.Draw();
			}
		}

		public void Add(UiComponent child)
		{
			if(child == null) throw new ArgumentNullException("UiComponent.Add() child is null");
			child.Transform.Parent = Transform;
			child.Parent = this;
			_children.Add(child);
		}

		public void Remove(UiComponent child)
		{
			if(child == null) throw new ArgumentNullException("UiComponent.Add() child is null");
			child.Transform.Parent = null;
			child.Parent = null;
			_children.Remove(child);
		}

		public bool Contains(UiComponent child)
		{
			if(child == null) throw new ArgumentNullException("UiComponent.Add() child is null");
			return _children.Contains(child);
		}

		public UiComponent Find(UiComponent child)
		{
			if(child == null) throw new ArgumentNullException("UiComponent.Add() child is null");
			return _children.Find((c) => c.Equals(child));
		}

		public void ForEach(Action<UiComponent> action)
		{
			foreach(UiComponent child in _children)
			{
				action(child);
			}
		}

		public void OrgazineComponents()
		{
			Layout.ProcessLayout(_children);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposable)
		{
			if(disposable)
			{
				if(!Disposed)
				{
					_children.Clear();
					Disposed = true;
				}
			}
		}
	}
}
