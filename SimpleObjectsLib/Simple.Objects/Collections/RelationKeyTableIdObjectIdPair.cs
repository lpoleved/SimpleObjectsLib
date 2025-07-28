using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Modeling
{
	public class RelationKeyTableIdObjectIdPair
	{
		public RelationKeyTableIdObjectIdPair(int relationKey, int tableId,long objectId)
		{
			this.RelationKey = relationKey;
			this.TableId = tableId;
			this.ObjectId = objectId;
		}
		
		public int RelationKey { get; private set; }
		public int TableId { get; set; }
		public long ObjectId { get; set; }
	}
}
