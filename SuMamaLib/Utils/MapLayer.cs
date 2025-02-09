namespace SuMamaLib
{
	public struct MapLayer
	{
		public int Width;
		public int Height;
		public int[] Data;
		public string Name;
		public string Type;
		public bool Visible = true;
		public byte Opacity = 1;

		public MapLayer(string name, string type, int w, int h, int[] data)
		{
			Name = name;
			Type = type;
			Width = w;
			Height = h;
			Data = data;
		}
	}
}
