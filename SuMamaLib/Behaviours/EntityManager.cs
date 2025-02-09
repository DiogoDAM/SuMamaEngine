using System;
using System.Collections.Generic;

namespace SuMamaLib
{
	public struct EntityManager
	{
		private List<Entity> _entities;
		public int Count { get { return _entities.Count; } }
		public string Layer;
		public Scene Scene;

		public EntityManager(string layer, Scene scene)
		{
			_entities = new List<Entity>();
			Layer = layer;
			Scene = scene;
		}

		public void Start()
		{
			foreach(var e in _entities)
			{
				e.Start();
			}
		}

		public void Update()
		{
			foreach(var e in _entities)
			{
				e.Update();
			}
		}

		public void Draw()
		{
			foreach(var e in _entities)
			{
				e.Draw();
			}
		}

		public void Add(Entity e)
		{
			if(e == null) throw new ArgumentNullException("EntityManager.Add() Entity is null");
			_entities.Add(e);
		}

		public void Remove(Entity e)
		{
			if(e == null) throw new ArgumentNullException("EntityManager.Remove() Entity is null");
			_entities.Remove(e);
		}
		
		public bool Contains(Entity e)
		{
			if(e == null) throw new ArgumentNullException("EntityManager.Contains() Entity is null");
			return _entities.Contains(e);
		}

		public Entity Find(Entity e)
		{
			if(e == null) throw new ArgumentNullException("EntityManager.Find() Entity is null");
			return _entities.Find((en) => en.Equals(e));
		}
	}
}
