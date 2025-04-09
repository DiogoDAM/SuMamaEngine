using System;
using System.Collections.Generic;

namespace SuMamaEngine
{
	public struct SceneLayer
	{
		private List<IDraw> _items;
		public int Count { get { return _items.Count; } }
		public string Id;
		public Scene Scene;

		public SceneLayer(string layer, Scene scene)
		{
			_items = new List<IDraw>();
			Id = layer;
			Scene = scene;
		}

		public void Draw()
		{
			foreach(var e in _items)
			{
				e.Draw();
			}
		}

		public void Add(IDraw obj)
		{
			if(obj == null) throw new ArgumentNullException("EntityManager.Add() IDraw is null");
			_items.Add(obj);
		}

		public void Remove(IDraw obj)
		{
			if(obj == null) throw new ArgumentNullException("EntityManager.Remove() IDraw is null");
			_items.Remove(obj);
		}
		
		public bool Contains(IDraw obj)
		{
			if(obj == null) throw new ArgumentNullException("EntityManager.Contains() IDraw is null");
			return _items.Contains(obj);
		}

		public IDraw Find(IDraw obj)
		{
			if(obj == null) throw new ArgumentNullException("EntityManager.Find() IDraw is null");
			return _items.Find((it) => it.Equals(obj));
		}

		public void Sort(Comparison<IDraw> con)
		{
			_items.Sort(con);
		}

		public T GetEntityType<T>() where T : IDraw
		{
			return (T)_items.Find((e) => e is T);
		}


		public List<IDraw> GetAllEntities()
		{
			return _items;
		}

		public void Clear()
		{
			_items.Clear();
		}
	}
}
