namespace SuMamaLib
{
	public interface IDisposableObject
	{
		public bool Disposed { get; }

		public void Dispose();
	}
}
