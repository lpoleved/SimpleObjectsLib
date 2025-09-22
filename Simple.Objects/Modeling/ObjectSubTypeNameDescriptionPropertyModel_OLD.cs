//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple.Modeling;

//namespace Simple.Objects
//{
//	public abstract class ObjectSubTypeNameDescriptionPropertyModel<TObjectSubType> : ObjectSubTypeNameDescriptionPropertyModel
//	{
//		protected override Type GetObjectSubTypePropertyType()
//		{
//			return typeof(TObjectSubType);
//		}
//	}

//	public abstract class ObjectSubTypeNameDescriptionPropertyModel : NameDescriptionPropertyModel
//	{
//		public PropertyModel ObjectSubType = new PropertyModel<int>(SimpleObject.IndexPropertyObjectSubType) { Name = SimpleObject.StringPropertyObjectSubType, DatastoreType = typeof(short), IsIndexed = true };

//		public ObjectSubTypeNameDescriptionPropertyModel()
//		{
//			this.ObjectSubType.PropertyType = this.GetObjectSubTypePropertyType();
//		}

//		protected abstract Type GetObjectSubTypePropertyType();
//	}
//}
