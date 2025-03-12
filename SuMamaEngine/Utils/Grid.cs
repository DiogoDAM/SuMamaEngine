using Microsoft.Xna.Framework;

using System;

namespace SuMamaEngine
{
	public struct Grid<T>
	{
		public int CellWidth { get; private set; }
		public int CellHeight { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }

		private T[] _cells;

		public Grid(int cellw, int cellh, int w, int h)
		{
			CellWidth = cellw;
			CellHeight = cellh;
			Width = w;
			Height = h;

			_cells = new T[Width * Height];
		}

		public void Set(int indexx, int indexy, T value)
		{
			if(!IndexIsValid(indexx, indexy)) throw new Exception("Grid.Set() the index is out of bound");
			if(value == null) throw new ArgumentNullException("Grid.Set() value is null");
			_cells[indexy * Width + indexx] = value;
		}

		public void Set(Point index, T value)
		{
			if(!IndexIsValid(index)) throw new Exception("Grid.Set() the index is out of bound");
			if(value == null) throw new ArgumentNullException("Grid.Set() value is null");
			_cells[index.Y * Width + index.X] = value;
		}

		public T Get(int indexx, int indexy)
		{
			if(!IndexIsValid(indexx, indexy)) throw new Exception("Grid.Get() the index is out of bound");
			return _cells[indexy * Width + indexx];
		}

		public T Get(Point index)
		{
			if(!IndexIsValid(index)) throw new Exception("Grid.Get() the index is out of bound");
			return _cells[index.Y * Width + index.X];
		}

		public void Move(int originx, int originy, int targetx, int targety)
		{
			if(!IndexIsValid(originx, originy)) throw new Exception("Grid.Move() the origin index is out of bound");
			if(!IndexIsValid(targetx, targety)) throw new Exception("Grid.Move() the target index is out of bound");

			T value = Get(originx, originy);
			if(value != null)
			{
				Set(targetx, targety, value);
			}
		}
		public void Move(Point origin, Point target)
		{
			if(!IndexIsValid(origin)) throw new Exception("Grid.Move() the origin index is out of bound");
			if(!IndexIsValid(target)) throw new Exception("Grid.Move() the target index is out of bound");

			T value = Get(origin.X, origin.Y);
			if(value != null)
			{
				Set(target.X, target.Y, value);
			}
		}

		public void Fill(T value)
		{
			for(int i=0; i<Height; i++)
			{
				for(int j=0; j<Width; j++)
				{
					_cells[i * Width + j] = value;
				}
			}
		}

		public void ForEach(Action<T> action)
		{
			foreach(var item in _cells)
			{
				if(item != null) action(item);
			}
		}

		public bool IndexIsValid(int indexx, int indexy)
		{
			return (indexx >= 0 && indexx < Width) && (indexy >= 0 && indexy < Height);
		}

		public bool IndexIsValid(Point index)
		{
			return (index.X >= 0 && index.X < Width) && (index.Y >= 0 && index.Y < Height);
		}
	}
}
