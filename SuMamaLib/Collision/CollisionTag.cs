using System.Collections.Generic;

namespace SuMamaLib
{
	public struct CollisionTag
	{
		public List<int> Tags;
		public List<int> CollisionTags;

		public CollisionTag()
		{
			Tags = new();
			CollisionTags = new();
		}

		public bool CheckTags(Collider col)
		{
			foreach(int tag in col.Tags.Tags)
			{
				if(CollisionTags.Contains(tag)) return true;
			}

			return false;
		}
	}
}
