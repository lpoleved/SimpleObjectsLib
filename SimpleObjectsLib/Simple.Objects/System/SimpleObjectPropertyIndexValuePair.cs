using Simple.Modeling;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects
{
	public struct SimpleObjectPropertyIndexValuePair
	{
		public SimpleObjectPropertyIndexValuePair(SimpleObject simpleObject, PropertyIndexValuePair propertyIndexValuePair)
		{
			this.SimpleObject = simpleObject;
			this.PropertyIndexValuePair = propertyIndexValuePair;
		}

		public SimpleObject SimpleObject { get; private set; }
		public PropertyIndexValuePair PropertyIndexValuePair { get; private set; }
	}
}
