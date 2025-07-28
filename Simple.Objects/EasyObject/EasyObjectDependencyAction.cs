using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects
{
    public abstract class EasyObjectDependencyAction
    {
		public abstract bool Match(EasyObject easyObject);

        public void SaveInternal(EasyObject easyObject)
        {
            easyObject.ObjectManager.SaveInternal(this, easyObject);
        }

        public void DeleteInternal(EasyObject easyObject)
        {
            easyObject.ObjectManager.DeleteInternal(this, easyObject);
        }

        public virtual void OnObjectCreated(EasyObject easyObject)
        {
        }

        public virtual void OnSaving(EasyObject easyObject)
        {
        }

        public virtual void OnBeforeDelete(EasyObject easyObject)
        {
        }

		//public virtual void OnAfterDelete(EasyObject easyObject)
		//{
		//}
		
		public virtual void OnPropertyValueChange(EasyObject easyObject, int propertyIndex, object value, object oldValue)
        {
        }
    }
}
