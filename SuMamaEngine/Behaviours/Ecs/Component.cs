using System;

namespace SuMamaEngine
{
	public abstract class Component : IDisposable
	{
		public bool IsActive { get; protected set; }
		public Entity Entity { get; protected set; }

		public Component(Entity entity)
		{
			Entity = entity;
		}

		//Logic Methods

		public virtual void Start()
		{
			IsActive = true;
		}

		public virtual void Update()
		{
			if(!IsActive) return;
		}

		public virtual void Draw()
		{
			if(!IsActive) return;
		}

		//Relative to Entity

		public virtual void Added()
		{
		}

		public virtual void Removed()
		{
			Entity = null;
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
				if(IsActive)
				{
					Entity = null;
					IsActive = false;
				}
			}
		}
	}
}
