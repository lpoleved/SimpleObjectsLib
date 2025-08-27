//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Simple.Modeling;

//namespace Simple.Objects
//{
//	public abstract class SimpleObjectPropertyModel_OLD : ObjectPropertyModelBase<PropertyModel>
//	{
//		public SimpleObjectPropertyModel_OLD()
//		{
//		}

//		public SimpleObjectPropertyModel_OLD(Comparison<PropertyModel> sortingComparison)
//			: base(sortingComparison)
//		{
//		}

//		public readonly PropertyModel Id = new PropertyModel<long>(SimpleObject.IndexPropertyId)
//		{
//			Name = SimpleObject.StringPropertyId,
//			AccessPolicy = PropertyAccessPolicy.ReadOnly,
//			GetAccessModifier = AccessModifier.Public,
//			SetAccessModifier = AccessModifier.Private,
////			IsKey = true,
//			//IsRelationKey = true,
//			IsIndexed = true,
//		};

//		//public readonly PropertyModel Guid = new PropertyModel<Guid>(SimpleObject.IndexPropertyGuid)
//		//{
//		//	Name = SimpleObject.StringPropertyGuid,
//		//	AccessPolicy = PropertyAccessPolicy.ReadOnly,
//		//	GetAccessModifier = AccessModifier.Public,
//		//	SetAccessModifier = AccessModifier.Private,
//		//	IsKey = true,
//		//	IsRelationKey = true,
//		//	IsIndexed = true
//		//};
//	}
//}
