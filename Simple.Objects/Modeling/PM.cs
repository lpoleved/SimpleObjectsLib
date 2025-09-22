using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Modeling;

namespace Simple.Objects
{
	public class PM<T> : PM
	{
		public PM()
			: base(typeof(T))
		{
		}

		public PM(int index)
			: base(index, typeof(T))
		{
		}

		public PM(int index, string propertyName)
			: base(index, propertyName, typeof(T))
		{
		}

		public new T DefaultValue
		{
			get { return (T)base.DefaultValue!; }
			set { base.DefaultValue = value; }
		}
	}

	public class PM : PropertyModel
	{
		internal PM(Type propertyType)
			: base(propertyType)
		{
		}

		internal PM(int index, Type propertyType)
			: base(index, propertyType)
		{
		}

		internal PM(int index, string propertyName, Type propertyType)
			: base(index, propertyName, propertyType)
		{
		}
	}
}
