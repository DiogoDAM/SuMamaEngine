using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
    public class GridCollider : Collider
    {
        public override AABB Bounds => throw new System.NotImplementedException();

		private bool[] _grid;

		public int CellSize { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }

		public GridCollider(int w, int h, int cellSize)
		{
			Width = w;
			Height = h;
			CellSize = cellSize;

			_grid = new bool[w*h];

			for(int y=0; y<Height; y++)
			{
				for(int x=0; x<Width; x++)
				{
					_grid[y * Width + x] = false;
				}
			}
		}

		public void Set(int x, int y, bool value)
		{
			_grid[y * Width + x] = value;
		}

		public bool Get(int x, int y)
		{
			return _grid[y * Width + x];
		}

		public void Set(Point index, bool value)
		{
			_grid[index.Y * Width + index.Y] = value;
		}

		public void Set(int cols, int rows, bool[] values)
		{
			if(!HasIndex(cols, rows)) return;
			for(int y=0; y<rows; y++)
			{
				for(int x=0; x<cols; x++)
				{
					_grid[y * cols + x] = values[y * cols + x];
				}
			}
		}

		public bool Get(Point index)
		{
			return _grid[index.Y * Width + index.Y];
		}

        public override bool CollidesWith(Collider col)
        {
			switch(col)
			{
				case RectCollider rect: return CollidesWith(rect);
				case CircleCollider circle: return CollidesWith(circle);
				default: return false;
			}
        }

        public override bool CollidesWith(RectCollider col)
        {
			(int minx, int maxx) = GetHorizontalIndex(col.Bounds);
			(int miny, int maxy) = GetVerticalIndex(col.Bounds);

			for(int y=miny; y<=maxy; y++)
			{
				for(int x=minx; x<=maxx; x++)
				{
					if(_grid[y * Width + x]) return true;
				}
			}
			return false;
        }

        public override bool CollidesWith(CircleCollider col)
        {
			(int minx, int maxx) = GetHorizontalIndex(col.Bounds);
			(int miny, int maxy) = GetVerticalIndex(col.Bounds);

			for(int y=miny; y<=maxy; y++)
			{
				for(int x=minx; x<=maxx; x++)
				{
					if(_grid[y * Width + x]) return true;
				}
			}
			return false;
        }

        public override bool CollidesWith(GridCollider col)
        {
            throw new System.NotImplementedException();
        }

        public Rectangle GetRectangleOfCell(Collider col)
		{
			(int minx, int maxx) = GetHorizontalIndex(col.Bounds);
			(int miny, int maxy) = GetVerticalIndex(col.Bounds);

			for(int y=miny; y<=maxy; y++)
			{
				for(int x=minx; x<=maxx; x++)
				{
					if(_grid[y * Width + x]) return new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize);
				}
			}
			return Rectangle.Empty;
		}

		public bool HasIndex(int x, int y)
		{
			return (x > 0 && x < Width) && (y > 0 && y < Height);
		}

		public bool HasIndex(Point index)
		{
			return (index.X > 0 && index.X < Width) && (index.Y > 0 && index.Y < Height);
		}

		public override void Draw(Color color)
		{
			for(int y=0; y<Height; y++)
			{
				for(int x=0; x<Width; x++)
				{
					if(_grid[y * Width + x]) Drawer.DrawLineRectangle(new Vector2(x*CellSize, y*CellSize), CellSize, CellSize, color);
				}
			}
		}

		private (int, int) GetHorizontalIndex(AABB shape)
		{
			int minx = (int)shape.TopLeft.X / CellSize;
			int maxx = (int)shape.TopRight.X / CellSize;
			return ((minx < 0) ? 0 : minx, (maxx > Width) ? Width : maxx);
		}

		private (int, int) GetVerticalIndex(AABB shape)
		{
			int miny = (int)shape.TopLeft.Y / CellSize;
			int maxy = (int)shape.BottomLeft.Y / CellSize;
			return ((miny < 0) ? 0 : miny, (maxy > Height) ? Height : maxy);
		}
    }
}
