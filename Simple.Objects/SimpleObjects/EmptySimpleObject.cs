using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects
{
    public class EmptySimpleObject : SimpleObject
    {
        public EmptySimpleObject()
            : base(manager: null)
        {
        }

		public override ISimpleObjectModel GetModel()
		{
			throw new NotImplementedException();
		}

		protected override object GetFieldValue(int propertyIndex)
        {
            throw new NotImplementedException();
        }

        protected override object GetOldFieldValue(int propertyIndex)
        {
            throw new NotImplementedException();
        }

        protected override void SetFieldValue(int propertyIndex, object value)
        {
            throw new NotImplementedException();
        }

        protected override void SetOldFieldValue(int propertyIndex, object value)
        {
            throw new NotImplementedException();
        }

		public override SimpleObjectCollection GetOneToManyForeignObjectCollection(int relationKey)
		{
			throw new NotImplementedException();
		}
	}
}
