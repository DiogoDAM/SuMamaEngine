namespace SuMamaEngine
{
	public sealed class SpatialGrid
	{
		public int CellSize { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }

		public bool Disposed { get; private set; }

		private bool[] _cells;

		public SpatialGrid(int cellSize, int w, int h)
		{
			CellSize = cellSize;
			Width = w;
			Height = h;
			_cells = new bool[w * h];
		}

		public void SetCollider(int x, int y, bool value)
		{
			if(IndexIsValid(x, y))
			{
				_cells[y * Width + x] = value;
			}
		}

		public bool IsColliding(int x, int y)
		{
			if(IndexIsValid(x, y)) return _cells[y * Width + x] == true;

			return false;
		}

		public bool CheckCollision(AABB shape)
		{
			int startX = (int) shape.Min.X/CellSize;
			int startY = (int) shape.Min.Y/CellSize;
			int endX = (int) shape.Max.X/CellSize;
			int endY = (int) shape.Max.Y/CellSize;

			for(int y = startY; y<=endY; y++)
			{
				for(int x = startX; x<=endX; x++)
				{
					if(IsColliding(x, y)) return true;
				}
			}

			return false;
		}

		public bool IndexIsValid(int x, int y)
		{
			return (x >= 0 && x < Width) && (y >= 0 && y < Height);
		}

	}
}
