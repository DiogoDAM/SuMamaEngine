namespace SuMamaEngine
{
	public sealed class CollisionInfo 
	{
		public Collider ColliderA;
		public Collider ColliderB;

		public CollisionInfo(Collider a, Collider b)
		{
			ColliderA = a;
			ColliderB = b;
		}
	}
}
