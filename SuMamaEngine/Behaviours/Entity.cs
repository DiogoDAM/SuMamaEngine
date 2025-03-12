using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SuMamaEngine
{
	// A more complex base for game objects with composite pattern
	public abstract class Entity : GameObject
	{
		public Entity Parent;
		protected List<Entity> _children;

		public Entity()
		{
			_children = new List<Entity>();
		}

		// Methods for handle the children process with basic game methods

		public virtual void StartChildren()
		{
			foreach(Entity child in _children)
			{
				child.Start();
			}
		}

		public virtual void UpdateChildren()
		{
			foreach(Entity child in _children)
			{
				child.Update();
			}
		}

		public virtual void DrawChildren()
		{
			foreach(Entity child in _children)
			{
				child.Draw();
			}
		}

		// Methods for handle the children

		public void AddChild(Entity e)
		{
			if(e == null) throw new ArgumentNullException("AddChild() Entity is null");
			_children.Add(e);
		}

		public void RemoveChild(Entity e)
		{
			if(e == null) throw new ArgumentNullException("RemoveChild() Entity is null");
			_children.Remove(e);
		}

		public Entity FindChild(Entity e)
		{
			if(e == null) throw new ArgumentNullException("FindChild() Entity is null");
			return _children.Find((en) => en.Equals(e));
		}

		public bool ContainsChild(Entity e)
		{
			if(e == null) throw new ArgumentNullException("FindChild() Entity is null");
			return _children.Contains(e);
		}

		public void ForEach(Action<Entity> action)
		{
			foreach(Entity child in _children)
			{
				action(child);
			}
		}

		protected override void Dispose(bool disposable)
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
