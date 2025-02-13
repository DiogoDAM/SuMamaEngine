using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuMamaLib
{
	// The Base of all Objects in game
	public class GameObject : IDisposableObject
	{
        public bool Disposed { get; protected set; }

        public Transform Transform;
		public Vector2 Anchor;
		public float Depth;
		public int Width, Height;
		public Color Color;
		public SpriteEffects Flip;

		public string SceneId;
		public string Layer;
		public string Id;

		public GameObject()
		{
			Disposed = false;
			Transform = new Transform(Vector2.Zero);
			Anchor = Vector2.One;
			Depth = 0f;
			Color = Color.White;
			Flip = SpriteEffects.None;
		}

		public virtual void Start()
		{
			if(Disposed) return;
		}

		public virtual void Update()
		{
			if(Disposed) return;
		}

		public virtual void Draw()
		{
			if(Disposed) return;
		}

		// Static Methods for handle Entities in Current scene

		public static void Instantiate(GameObject e)
		{
			SceneManager.Instance.InstantiateObject(e);
		}

		public static void Instantiate(GameObject e, Transform trans)
		{
			SceneManager.Instance.InstantiateObject(e, trans);
		}

		public static void Destroy(GameObject e)
		{
			SceneManager.Instance.DestroyObject(e);
		}

		// Methods for handle the Dispose
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
