using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects
{
	public class SimpleObjectKeyArgs
	{
		public SimpleObjectKeyArgs(SimpleObject simpleObject, int tableId, long objectId)
		{
			this.SimpleObject = simpleObject;
			this.TableId = tableId;
			this.ObjectId = objectId;
		}

		public SimpleObject SimpleObject { get; private set; }
		public int TableId { get; private set; }
		public long ObjectId { get; private set; }
	}
}
