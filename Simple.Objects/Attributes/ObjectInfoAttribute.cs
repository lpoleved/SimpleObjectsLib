using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Class,  AllowMultiple = false)]
    public class ObjectInfoAttribute : Attribute
    {
		public ObjectInfoAttribute(string objectName)
        {
			this.ObjectName = objectName;
        }

		public string ObjectName { get; private set; }
	}
}
