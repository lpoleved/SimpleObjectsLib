using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Class,  AllowMultiple = true)]
    public class IncludePropertyAttribute : Attribute
    {
		public IncludePropertyAttribute(params string[] propertyNames)
        {
			this.PropertyNames = propertyNames;
        }

		public string[] PropertyNames { get; private set; }
	}
}
