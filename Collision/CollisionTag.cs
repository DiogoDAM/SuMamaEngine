using System.Collections.Generic;

namespace SuMamaEngine
{
	public struct CollisionTag
	{
		public List<int> OwnTags;
		public List<int> CollisionTags;

		public CollisionTag()
		{
			OwnTags = new();
			CollisionTags = new();
		}

		public bool CheckTag(int t)
		{
			foreach(int tag in CollisionTags)
			{
				if(tag == t) return true;
			}

			return false;
		}

		public bool CheckTags(Collider col)
		{
			foreach(int tag in col.Tags.OwnTags)
			{
				if(CollisionTags.Contains(tag)) return true;
			}

			return false;
		}

		public bool HasTag(int t)
		{
			foreach(int tag in OwnTags)
			{
				if(tag == t) return true;
			}

			return false;
		}
	}
}
