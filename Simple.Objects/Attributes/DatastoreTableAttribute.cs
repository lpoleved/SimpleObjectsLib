using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class DatastoreTableAttribute : Attribute
	{
		public DatastoreTableAttribute(int tableId)
		{
			this.TableId = tableId;
		}

		public int TableId { get; private set; }
	}
}
