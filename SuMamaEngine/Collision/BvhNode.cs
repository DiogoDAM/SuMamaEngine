namespace SuMamaEngine
{
	public sealed class BvhNode
	{
		public AABB Bounds;
		public BvhNode Left, Right;
		public Collider Collider;

		public bool IsLeaf => Collider != null;

		public BvhNode(AABB bounds, Collider col = null, BvhNode left = null, BvhNode right = null)
		{
			Bounds = bounds;
			Left = left;
			Right = right;
			Collider = col;
		}
	}
}
