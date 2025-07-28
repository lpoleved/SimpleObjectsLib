using Simple.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects
{
    public class RelationListInfo
    {
		private List<RelationKeyTableIdObjectIdPair> list;

		public RelationListInfo()
			: this(capacity: 6)
		{
		}

		public RelationListInfo(int capacity)
		{
			this.list = new List<RelationKeyTableIdObjectIdPair>(capacity);				
		}

		public RelationKeyTableIdObjectIdPair this[int index] => this.list[index];

		public RelationKeyTableIdObjectIdPair ElementAt(int index) => this[index];

		public bool TryGetRelationInfo(int relationKey, out RelationKeyTableIdObjectIdPair? relationInfo)
		{
			int index = this.IndexOf(relationKey);

			if (index >= 0)
			{
				relationInfo = this[index];

				return true;
			}

			relationInfo = default;

			return false;
		}

		public void Set(int relationKey, int tableId, long objectId)
		{
			int index = this.IndexOf(relationKey);

			if (index < 0)
			{
				this.list.Add(new RelationKeyTableIdObjectIdPair(relationKey, tableId, objectId));
			}
			else
			{
				this.list[relationKey].TableId = tableId;
				this.list[relationKey].ObjectId = objectId;
			}
		}

		public void Set(int relationKey, int tableId)
		{
			int index = this.IndexOf(relationKey);

			if (index < 0)
				this.list.Add(new RelationKeyTableIdObjectIdPair(relationKey, tableId, objectId: 0));
			else
				this.list[index].TableId = tableId;
		}

		public void Set(int relationKey, long objectId)
		{
			int index = this.IndexOf(relationKey);

			if (index < 0)
				this.list.Add(new RelationKeyTableIdObjectIdPair(relationKey, tableId: 0, objectId));
			else
				this.list[index].ObjectId = objectId;
		}

		public bool ContainsRelationKey(int relationKey) => this.IndexOf(relationKey) >= 0;

		public int IndexOf(int relationKey)
		{
			for (int i = 0; i < this.list.Count; i++)
				if (relationKey == this.list[i].RelationKey)
					return i;

			return -1;
		}

		public int Count => this.list.Count;
	}
}
