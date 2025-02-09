using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SuMamaLib
{
	public abstract class Entity : IDisposableObject
	{
        public bool Disposed { get; protected set; }

        public Transform Transform;
		public Vector2 Anchor;
		public float Depth;
		public int Width, Height;

		public Scene Scene;
		public string Layer;

		public Entity Parent;
		protected List<Entity> _children;

		public Entity()
		{
			_children = new List<Entity>();

			Disposed = false;
			Transform = new Transform(Vector2.Zero);
			Anchor = Vector2.One;
			Depth = 0f;
		}

		public virtual void Start()
		{
			foreach(Entity child in _children)
			{
				child.Start();
			}
		}

		public virtual void Update()
		{
			foreach(Entity child in _children)
			{
				child.Update();
			}
		}

		public virtual void Draw()
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

		// For Spawn and Destroy Entities inside a Scene

		public static void Instantiate(Entity e)
		{
			if(e == null) throw new ArgumentNullException("FindChild() Entity is null");
		}

		public static void Instantiate(Entity e, Transform trans)
		{
			if(e == null) throw new ArgumentNullException("FindChild() Entity is null");
		}

		public static void Destroy(Entity e)
		{
			if(e == null) throw new ArgumentNullException("FindChild() Entity is null");
		}

		public static void Destroy(string layer, Entity e)
		{
			if(e == null) throw new ArgumentNullException("FindChild() Entity is null");
		}


        public void Dispose()
        {
			Dispose(true);
			GC.SuppressFinalize(this);
        }

		protected virtual void Dispose(bool dispose)
		{
			if(!dispose && Disposed) return;
		}
    }
}
