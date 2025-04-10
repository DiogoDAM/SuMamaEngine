using System;
using System.Collections;
using System.Collections.Generic;

namespace SuMamaEngine
{
	public sealed class ComponentContainer : IDisposable, IEnumerable
	{
		private List<Component> _components;

		public Entity Entity { get; private set; }

		public ComponentContainer(Entity entity)
		{
			Entity = entity;

			_components = new();
		}

		public void Add(Component c) 
		{
			if(_components.Contains(c)) return;
			_components.Add(c);
			c.Added();
		}

		public void Remove(Component c) 
		{
			if(!_components.Contains(c)) return;
			_components.Remove(c);
			c.Removed();
		}

		public bool Contains(Component c) 
		{
			return _components.Contains(c);
		}

		public Component GetComponent(Component c) 
		{
			return _components.Find(com => com.Equals(c));
		}

		public T GetComponentType<T>() where T : Component
		{
			return (T)_components.Find(com => com is T);
		}

		public void Clear()
		{
			_components.Clear();
		}

		public void Dispose()
		{
			Clear();
			Entity = null;

			GC.SuppressFinalize(this);
		}

        public IEnumerator GetEnumerator()
        {
			return _components.GetEnumerator();
        }
    }
}

