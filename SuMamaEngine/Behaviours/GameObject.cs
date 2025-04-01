using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuMamaEngine
{
	// The Base of all Objects in game
	public class GameObject : IDisposable, IPrototype
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

		public virtual void OnAdd()
		{
		}

		public virtual void OnRemoved()
		{
		}

		// Static Methods for handle Entities in Current scene
		
		public static void AddToScene(GameObject obj)
		{
			SceneManager.Instance.CurrentScene.AddObject(obj);
		}

		public static void AddToScene(string layer, GameObject obj)
		{
			SceneManager.Instance.CurrentScene.AddObject(layer, obj);
		}

		public static T Instantiate<T>(T original) where T : GameObject, new()
		{
			T newInstance = new T();

			newInstance = (T) original.DeepClone();

			SceneManager.Instance.CurrentScene.AddObject(newInstance);

			return newInstance;
		}

		public static T Instantiate<T>(string layer, T original) where T : GameObject, new()
		{
			T newInstance = new T();

			newInstance = (T) original.DeepClone();

			SceneManager.Instance.CurrentScene.AddObject(layer, newInstance);

			return newInstance;
		}

		public static T Instantiate<T>(T original, Vector2 pos) where T : GameObject, new()
		{
			T newInstance = new T();

			newInstance = (T) original.DeepClone();

			newInstance.Transform.Position = pos;
			SceneManager.Instance.CurrentScene.AddObject(newInstance);

			return newInstance;
		}

		public static T Instantiate<T>(string layer, T original, Vector2 pos) where T : GameObject, new()
		{
			T newInstance = new T();

			newInstance = (T) original.DeepClone();

			newInstance.Transform.Position = pos;
			SceneManager.Instance.CurrentScene.AddObject(layer, newInstance);

			return newInstance;
		}

		public static void RemoveToScene(GameObject e)
		{
			SceneManager.Instance.CurrentScene.RemoveObject(e);
		}

		public static void RemoveToScene(string layer, GameObject e)
		{
			SceneManager.Instance.CurrentScene.RemoveObject(layer, e);
		}

		public static void Destroy(GameObject e)
		{
			SceneManager.Instance.CurrentScene.RemoveObject(e);
			e.Dispose();
		}

		public static void Destroy(GameObject e, string layer)
		{
			SceneManager.Instance.CurrentScene.RemoveObject(layer, e);
			e.Dispose();
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

        public virtual IPrototype ShallowClone()
        {
			return (GameObject) MemberwiseClone();
        }

        public virtual IPrototype DeepClone()
        {
			GameObject obj = (GameObject) MemberwiseClone();
			obj.Transform = Transform;

			return obj;
        }
    }
}
