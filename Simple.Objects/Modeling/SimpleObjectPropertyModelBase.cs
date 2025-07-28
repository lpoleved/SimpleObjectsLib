using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Modeling;

namespace Simple.Objects
{
	public abstract class SimpleObjectPropertyModelBase : PropertyModelBase
	{
		public SimpleObjectPropertyModelBase()
		{
		}

		//public SimpleObjectPropertyModel(Comparison<PropertyModel> sortingComparison)
		//	: base(sortingComparison)
		//{
		//}

		public PM Id = new PM<long>(SimpleObject.IndexPropertyId)
		{
			AccessPolicy = PropertyAccessPolicy.ReadOnly,
			GetAccessModifier = AccessModifier.Public,
			SetAccessModifier = AccessModifier.Private,
			IsId = true,
			IsIndexed = true,
			IsClientToServerSeriazable = false, // key is not general property, it is unique object identificator and it is not
			IncludeInTransactionActionLog = false // It is written as ObjectId together with the TableId
		};
	}
}
