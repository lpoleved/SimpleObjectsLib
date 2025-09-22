//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple.Modeling;

//namespace Simple.Objects
//{
//	public abstract class SortableObjectSubTypeNameDescriptionPropertyModel<TObjectSubType> : SortableObjectSubTypeNameDescriptionPropertyModel
//	{
//		protected override Type GetObjectSubTypePropertyType()
//		{
//			return typeof(TObjectSubType);
//		}
//	}

//	public abstract class SortableObjectSubTypeNameDescriptionPropertyModel : ObjectSubTypeNameDescriptionPropertyModel
//	{
//		public readonly PropertyModel PreviousId = GraphElementModel.PropertyModel.PreviousId;
//		public readonly PropertyModel OrderIndex = GraphElementModel.PropertyModel.OrderIndex;
//	}
//}
