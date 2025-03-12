using System;
using System.Collections.Generic;

namespace SuMamaEngine
{
	public struct ObjectManager
	{
		private List<GameObject> _objs;
		public int Count { get { return _objs.Count; } }
		public string Layer;
		public Scene Scene;

		public ObjectManager(string layer, Scene scene)
		{
			_objs = new List<GameObject>();
			Layer = layer;
			Scene = scene;
		}

		public void Start()
		{
			foreach(var e in _objs)
			{
				e.Start();
			}
		}

		public void Update()
		{
			foreach(var e in _objs)
			{
				e.Update();
			}
		}

		public void Draw()
		{
			foreach(var e in _objs)
			{
				e.Draw();
			}
		}

		public void Add(GameObject obj)
		{
			if(obj == null) throw new ArgumentNullException("EntityManager.Add() GameObject is null");
			_objs.Add(obj);
		}

		public void Remove(GameObject obj)
		{
			if(obj == null) throw new ArgumentNullException("EntityManager.Remove() GameObject is null");
			_objs.Remove(obj);
		}
		
		public bool Contains(GameObject obj)
		{
			if(obj == null) throw new ArgumentNullException("EntityManager.Contains() GameObject is null");
			return _objs.Contains(obj);
		}

		public GameObject Find(GameObject obj)
		{
			if(obj == null) throw new ArgumentNullException("EntityManager.Find() GameObject is null");
			return _objs.Find((it) => it.Equals(obj));
		}

		public T GetEntityType<T>() where T : GameObject
		{
			return (T)_objs.Find((e) => e is T);
		}


		public List<GameObject> GetAllEntities()
		{
			return _objs;
		}

		public void Clear()
		{
			_objs.Clear();
		}
	}
}
