using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SuMamaEngine
{
	public sealed class Map
	{
		public SpriteAtlas Sprite { get; private set; }
		public Point Position;
		public int TileWidth;
		public int TileHeight;
		public int Width;
		public int Height;
		public Dictionary<string, MapLayer> Layers;

		public Map(Point pos, SpriteAtlas sprite, int tilew, int tileh, int w, int h)
		{
			Position = pos;
			Sprite = sprite;
			TileWidth = tilew;
			TileHeight = tileh;
			Width = w;
			Height = h;
			Layers = new();
		}

		public void AddLayer(string layerName, MapLayer layer)
		{
			Layers.Add(layerName, layer);
		}

		public void RemoveLayer(string layerName)
		{
			Layers.Remove(layerName);
		}

		public void SetLayerVisibily(string layerName, bool visibility)
		{
			if(!Layers.ContainsKey(layerName)) return;
			var v = Layers[layerName];
			v.Visible = visibility;
		}

		public void Draw()
		{
			foreach(var layer in Layers)
			{
				MapLayer layerValue = layer.Value;
				if(layerValue.Visible == false || layerValue.Type != "tilelayer") return;

				int[] data = layerValue.Data;
				int width = layerValue.Width;
				int height = layerValue.Height;

				for(int row=0; row<height; row++)
				{
					for(int column=0; column<width; column++)
					{
						int tile = data[row * width + column];
						if(tile != 0)
						{
							Vector2 tilePos = new Vector2(Position.X + (column * TileWidth), Position.Y + (row * TileHeight));
							Globals.SpriteBatch.Draw(Sprite.Texture,
									tilePos,
									Sprite.Quads[tile],
									Color.White
									);
						}
					}
				}
			}
		}

		public void DrawIsometric()
		{
			foreach(var layer in Layers)
			{
				MapLayer layerValue = layer.Value;
				if(layerValue.Visible == false || layerValue.Type != "tilelayer") return;

				int[] data = layerValue.Data;
				int width = layerValue.Width;
				int height = layerValue.Height;

				for(int row=0; row<height; row++)
				{
					for(int column=0; column<width; column++)
					{
						int tile = data[row * width + column];
						if(tile != 0)
						{
							Vector2 tilePos = Utilities.CarToIsoScreen(column, row, TileWidth, TileHeight);
							Globals.SpriteBatch.Draw(Sprite.Texture,
									Position.ToVector2() + tilePos,
									Sprite.Quads[tile],
									Color.White
									);
						}
					}
				}
			}
		}

	}
}
