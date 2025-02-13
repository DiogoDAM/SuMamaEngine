using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SuMamaLib
{
	public abstract class UiComponent
	{
		protected List<UiComponent> _children;
		public UiComponent Parent;
		public IUiLayout Layout { protected get; set; }

		public Vector2 LayoutOffset;
		public Vector2 Margin;
		public bool Infinite;
		public bool Float;

		public Transform Transform;
		public Color Color;
		public float Depth;

		public Vector2 GlobalPosition { get { return Transform.GlobalPosition + LayoutOffset; } }
		public Vector2 Position { get { return Transform.GlobalPosition + Margin; } }
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

	}
}
