namespace SuMamaEngine
{
	public interface IPrototype
	{
		public IPrototype ShallowClone();
		public IPrototype DeepClone();
	}
}
