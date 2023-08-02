using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Modeling;

namespace Simple.Objects
{
	//public class SortableObjectPropertyModel : SimpleObjectPropertyModel
	//{
	//	public SortableObjectPropertyModel()
	//		: this((s1, s2) => (s1.Index == 2) ? 1 : s1.Index.CompareTo(s2.Index)) // index of 2 is OrderIndex
	//	{
	//	}

	//	public SortableObjectPropertyModel(Comparison<PropertyModel> sortingComparison)
	//		: base(sortingComparison)
	//	{
	//	}

	//	public PM PreviousId = new PM<long>(SortableSimpleObject.IndexPropertyPreviousId)
	//	{
	//		Name = SortableSimpleObject.StringPropertyPreviousId,
	//		AccessPolicy = PropertyAccessPolicy.ReadWrite,
	//		GetAccessModifier = AccessModifier.Internal,
	//		SetAccessModifier = AccessModifier.Private,
	//		IsIndexed = true,
	//		AddOrRemoveInChangedProperties = true, // at Server side
	//		FirePropertyValueChangeEvent = false,
	//		IsMemberOfSerializationSequence = false,
	//		IsStorable = true,
	//		IncludeInTransactionActionLog = false,
	//	};

	//	public PM OrderIndex = new PM<int>(SortableSimpleObject.IndexPropertyOrderIndex)		{
	//		Name = SortableSimpleObject.StringPropertyOrderIndex,
	//		AccessPolicy = PropertyAccessPolicy.ReadWrite,
	//		GetAccessModifier = AccessModifier.Public,
	//		SetAccessModifier = AccessModifier.Public,
	//		AddOrRemoveInChangedProperties = false, // at Server side
	//		FirePropertyValueChangeEvent = false,
	//		IsMemberOfSerializationSequence = true,
	//		IsStorable = false,
	//		IncludeInTransactionActionLog = true,
	//	};
	//}

	public class PrevIdPM<T> : PM<T>
	{
		public PrevIdPM(int index)
			: base(index)
		{
			this.PropertyName = "PreviousId";
			this.AccessPolicy = PropertyAccessPolicy.ReadWrite;
			this.GetAccessModifier = AccessModifier.Protected; // | AccessModifier.Internal;
			this.SetAccessModifier = AccessModifier.Protected; // | AccessModifier.Internal;
			//this.IsRelationObjectId = true;
			this.IsIndexed = true;
			this.IsPreviousId = true;
			this.AddOrRemoveInChangedProperties = true; // at Server side
			this.FirePropertyValueChangeEvent = false;
			this.IsClientSeriazable = false;
			this.IsStorable = true;
			this.IncludeInTransactionActionLog = false;
		}
	}

	//public class ActionSetOrderIndexPM : PM<int?>
	//{
	//	public ActionSetOrderIndexPM()
	//		: base()
	//	{
	//		this.PropertyName = "ActionSetOrderIndex";
	//		this.AccessPolicy = PropertyAccessPolicy.ReadWrite;
	//		//this.GetAccessModifier = AccessModifier.Private;
	//		//this.SetAccessModifier = AccessModifier.Public;
	//		this.AddOrRemoveInChangedProperties = false; // at Server side
	//		this.FirePropertyValueChangeEvent = false;
	//		this.IsMemberOfSerializationSequence = true;
	//		this.IsStorable = false;
	//		this.IsActionSetOrderIndex = true;
	//		this.IncludeInTransactionActionLog = true;
	//	}
	//}

	public class OrderIndexPM : PM<int>
	{
		public OrderIndexPM()
			: base()
		{
			this.PropertyName = "OrderIndex";
			this.AccessPolicy = PropertyAccessPolicy.ReadWrite;
			//this.GetAccessModifier = AccessModifier.Private;
			//this.SetAccessModifier = AccessModifier.Public;
			//this.AddOrRemoveInChangedProperties = false; // at Server side
			//this.FirePropertyValueChangeEvent = false;
			this.IsClientSeriazable = true;
			this.IsStorable = false;
			this.IsOrderIndex = true;
			this.IncludeInTransactionActionLog = true;
		}
	}

	//public class SortableObjectPropertyModel_OLD : SimpleObjectPropertyModel_OLD
	//{
	//	public SortableObjectPropertyModel_OLD()
	//		: this((s1, s2) => (s1.Index == 2) ? 1 : s1.Index.CompareTo(s2.Index)) // index 2 is OrderIndex
	//	{
	//	}

	//	public SortableObjectPropertyModel_OLD(Comparison<PropertyModel> sortingComparison)
	//		: base(sortingComparison)
	//	{
	//	}

	//	public readonly PropertyModel PreviousId = new PropertyModel<long>(1)  //SimpleObject.IndexPropertyPreviousId)
	//	{
	//		Name = SortableSimpleObject.StringPropertyPreviousId,
	//		AccessPolicy = PropertyAccessPolicy.ReadWrite,
	//		GetAccessModifier = AccessModifier.Internal,
	//		SetAccessModifier = AccessModifier.Private,
	//		IsIndexed = true,
	//		AddOrRemoveInChangedProperties = true,
	//		FirePropertyValueChangeEvent = false,
	//		IsMemberOfSerializationSequence = false,
	//		IsStorable = true,
	//		IncludeInTransactionActionLog = false,
	//	};

	//	public readonly PropertyModel PreviousGuid = new PropertyModel<Guid?>(2) //SimpleObject.IndexPropertyPreviousGuid)
	//	{
	//		Name = "PreviousGuid",
	//		AccessPolicy = PropertyAccessPolicy.ReadWrite,
	//		GetAccessModifier = AccessModifier.Internal,
	//		SetAccessModifier = AccessModifier.Private,
	//		IsIndexed = true,
	//		AddOrRemoveInChangedProperties = true,
	//		FirePropertyValueChangeEvent = false,
	//		IsMemberOfSerializationSequence = false,
	//		IsStorable = true,
	//		IncludeInTransactionActionLog = false,
	//	};

	//	public readonly PropertyModel OrderIndex = new PropertyModel<int>(3) //SimpleObject.IndexPropertyOrderIndex)
	//	{
	//		Name = SortableSimpleObject.StringPropertyOrderIndex,
	//		AccessPolicy = PropertyAccessPolicy.ReadWrite,
	//		GetAccessModifier = AccessModifier.Public,
	//		SetAccessModifier = AccessModifier.Public,
	//		AddOrRemoveInChangedProperties = false,
	//		FirePropertyValueChangeEvent = false,
	//		IsMemberOfSerializationSequence = true,
	//		IsStorable = false,
	//		IncludeInTransactionActionLog = true,
	//	};
	//}
}
