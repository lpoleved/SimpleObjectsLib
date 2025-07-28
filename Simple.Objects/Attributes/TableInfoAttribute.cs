using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects
{
	[AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
	public class TableInfoAttribute : Attribute
	{
		public TableInfoAttribute()
		{
		}
	}
}
