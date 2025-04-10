using System;

namespace SuMamaEngine
{
	public abstract class Entity : GameObject
	{
		protected ComponentContainer _components;

		public Entity()
		{
			_components = new(this);
		}

		//Logic Methods

		public override void Start()
		{
			base.Start();

			foreach(Component components in _components)
			{
				components.Start();
			}
		}

		public override void Update()
		{
			base.Update();

			foreach(Component components in _components)
			{
				components.Update();
			}
		}

		public override void Draw()
		{
			base.Draw();

			foreach(Component components in _components)
			{
				components.Draw();
			}

		}

		//Components Handler Methods

		public void AddComponent(Component c) 
		{
			if(c == null) throw new ArgumentNullException("Entity.AddComponent() of the {ObjectId} Entity the component is null");
			_components.Add(c);
		}

		public void RemoveComponent(Component c) 
		{
			if(c == null) throw new ArgumentNullException("Entity.AddComponent() of the {ObjectId} Entity the component is null");
			_components.Remove(c);
		}

		public void LiberateComponent(Component c) 
		{
			if(c == null) throw new ArgumentNullException("Entity.AddComponent() of the {ObjectId} Entity the component is null");
			_components.Remove(c);
			c.Dispose();
		}

		public bool ContainsComponent(Component c) 
		{
			if(c == null) throw new ArgumentNullException("Entity.AddComponent() of the {ObjectId} Entity the component is null");
			return _components.Contains(c);
		}

		public Component GetComponent(Component c) 
		{
			if(c == null) throw new ArgumentNullException("Entity.AddComponent() of the {ObjectId} Entity the component is null");
			return _components.GetComponent(c);
		}

		public T GetComponentType<T>() where T : Component
		{
			return _components.GetComponentType<T>();
		}

		protected override void Dispose(bool disposable)
		{
			if(disposable)
			{
				if(!Disposed)
				{
					_components.Dispose();
					Disposed = true;
				}
			}

		}
	}
}
