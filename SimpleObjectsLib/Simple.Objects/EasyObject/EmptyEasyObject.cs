using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects
{
    public class EmptyEasyObject : EasyObject
    {
        public EmptyEasyObject()
            : base(null)
        {
        }

		protected override object GetFieldValue(int propertyIndex)
		{
			return null;
		}

		protected override object GetOldFieldValue(int propertyIndex)
		{
			return null;
		}

		protected override void SetFieldValue(int propertyIndex, object value)
		{
		}

		protected override void SetOldFieldValue(int propertyIndex, object value)
		{
		}
    }
}
