using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Modeling;

namespace Simple.Objects
{
	public class NewPropertyObjectIdNeedToBeSet
	{
		public NewPropertyObjectIdNeedToBeSet(int tableId, long objectId, int propertyIndex) //, long newObjectId)
		{
			this.TableId = tableId;
			this.ObjectId = objectId;
			this.PropertyIndex = propertyIndex;
			//this.NewObjectId = newObjectId;
		}

		public int TableId { get; private set; }
		public long ObjectId { get; private set; }
		public int PropertyIndex { get; private set; }
		//public long NewObjectId { get; set; }
	}
}
