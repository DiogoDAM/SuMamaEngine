namespace SuMamaEngine
{
	public interface IDraw
	{
		public string Layer { get; }
		public float DepthPoint { get; }
		public void Draw();
	}
}
