using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects
{
	public class DatastoreTypeAttribute : Attribute
	{
		public DatastoreTypeAttribute(Type datastoreType)
		{
			this.DatastoreType = datastoreType;
		}

		public Type DatastoreType { get; private set; }
	}
}
