using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Collections;
using Simple.Modeling;

namespace Simple.Objects
{
	///// <summary>
	///// Initializes a new instance of the <see cref="Simple.Objects.SortableSimpleObject{T}"/> generic class that
	///// <typeparam name="T"></typeparam>
	//public abstract class SortableSimpleObject<T> : SortableSimpleObject where T : SortableSimpleObject<T>, ISortableBindingSimpleObject, IBindingSimpleObject, IDisposable
	//{
	//	//public SortableSimpleObject()
	//	//{
	//	//}

	//	public SortableSimpleObject(SimpleObjectManager objectManager)
	//		: base(objectManager)
	//	{
	//	}

	//	public SortableSimpleObject(SimpleObjectManager objectManager, ChangeContainer changeContainer)
	//		: base(objectManager, changeContainer)
	//	{
	//	}

	//	//public SortableSimpleObject(SimpleObjectManager objectManager, bool isNew)
	//	//	: base(objectManager, isNew)
	//	//{
	//	//}

	//	public new T Previous
	//	{
	//		get { return base.Previous as T; }
	//		//protected internal set { base.Previous = value; }
	//	}

	//	public new T Next
	//	{
	//		get { return base.Next as T; }
	//		//protected internal set { base.Next = value; }
	//	}


	//	//protected abstract SimpleObjectCollection<T> GetSortingCollection();

	//	//protected internal override ISimpleObjectCollection GetSortingCollectionInternal() { return this.GetSortingCollection(); }


	//	//protected override void SetOrderIndexInternal(int orderIndex, ChangeContainer changeContainer, object requester)
	//	//{
	//	//	this.GetSortingCollection().SetOrderIndexInternal(this, orderIndex, ChangeContainer, requester);
	//	//}

	//	//protected void SetOrderingBeforeRemovingFromCollection(object requester, SimpleObjectCollection<T> parentGraphElements)
	//	//{
	//	//	base.SetOrderingBeforeRemovingFromCollection(requester, parentGraphElements);
	//	//}

	//	//protected void SetOrderingAfterInsertingIntoCollection(object requester, SimpleObjectCollection<T> parentGraphElements)
	//	//{
	//	//	base.SetOrderingAfterInsertingIntoCollection(requester, parentGraphElements);
	//	//}
	//}

	//[IncludeProperty(SortableSimpleObject.StringPropertyPreviousId)]

	/// <summary>
	/// Initializes a new instance of the <see cref="Simple.Objects.SortableSimpleObject"/> class that implement sorting capabilities.
	/// PreviousId property is storable and it is saved in database to preserve the sorting order of the SimpleObject SortingCollection.
	/// PreviousId property  is not seriazable and is not sent via client <-> Server communication.
	/// PreviousId is set on client side when inserting objects into SimpleObjectCollection
	/// OrderIndex property is not storable but is seriazable and is sent via client <-> server communication.
	/// OrderIndex can be set to change the order of the object inside sorting collection. When the OrderIndex is set, its value is set over the client <-> server when inserting or updating object.
	/// 
	/// </summary>
	public abstract partial class SortableSimpleObject : SimpleObject, ISortableBindingSimpleObject, IBindingSimpleObject, IDisposable  // SimpleObject
	{
		//protected long previousId, oldPreviousId;
		//protected int orderIndex, oldOrderIndex;
		//protected int? actionSetOrderIndex, oldActionSetOrderIndex;

		public SortableSimpleObject(SimpleObjectManager objectManager)
			: base(objectManager)
		{
		}

		public SortableSimpleObject(SimpleObjectManager objectManager, ChangeContainer changeContainer)
			: base(objectManager, changeContainer)
		{ 
		}

		//public int OrderIndex 
		//{ 
		//	get { return this.orderIndex; }
		//	set { base.SetPropertyValue<int>(this.GetModel().OrderIndexPropertyModel, value, ref this.orderIndex, this.oldPreviousId); }
		//}

		internal void SetOrderIndexInternal(int value) => this.AcceptPropertyChangeInternal(this.GetModel().OrderIndexPropertyModel.PropertyIndex, value);

		public bool IsFirst
		{
			get { return this.PreviousId == 0; }
		}

		public bool IsLast
		{
			get { return this.NextId == 0; }
		}

		///// <summary>
		/////  Gets or sets PreviousId property value.
		///// </summary>
		//public long PreviousId
		//{
		//	get { return this.previousId; }
		//	internal set { base.SetPropertyValue<long>(this.GetModel().PreviousIdPropertyModel, value, ref this.previousId, this.oldPreviousId); }
		//}

		public SortableSimpleObject Previous
		{
			get { return this.Manager.GetObject(this.GetModel().TableInfo.TableId, this.PreviousId) as SortableSimpleObject; }
		}

		public long NextId { get; internal set; }

		public SortableSimpleObject Next
		{
			get { return this.Manager.GetObject(this.GetModel().TableInfo.TableId, this.NextId) as SortableSimpleObject; }
		}
		//public int? ActionSetOrderIndex
		//{
		//	get { return this.actionSetOrderIndex; }
		//	internal set { base.SetPropertyValue<int?>(this.GetModel().ActionSetOrderIndexPropertyModel, value, ref this.actionSetOrderIndex, this.oldActionSetOrderIndex); }
		//}

		//public void SetOrderIndex(int orderIndex)
		//{
		//	this.SetOrderIndex(orderIndex, this.Requester);
		//}

		//public void SetOrderIndex(int orderIndex, object requester)
		//{
		//	this.SetOrderIndex(orderIndex, this.Manager.DefaultChangeContainer, requester);
		//}

		//public void SetOrderIndex(int orderIndex, ChangeContainer changeContainer, object requester)
		//{
		//	if (orderIndex == this.OrderIndex)
		//		return;

		//	this.ChangeContainer = changeContainer;
		//	this.Requester = requester;
		//	this.ActionSetOrderIndex = orderIndex; // Set SetOrderIndexAction to the required OrderIndex and activate SetOrderIndexInternal method to accomplish action task
		//}

		//private void SetOrderIndexInternal(int orderIndex, ChangeContainer changeContainer, object requester)
		//{
		//	int oldOrderIndex = this.OrderIndex;
			
		//	this.GetSortingCollection().SetOrderIndex(this, orderIndex, changeContainer, requester);
		//	this.Manager.OrderIndexIsChanged(this, orderIndex, oldOrderIndex, requester);
		//}

		//protected internal void SetOrderIndexInternal(int orderIndex)
		//{
		//	this.OrderIndex = orderIndex;
		//}

		protected abstract SimpleObjectCollection? GetSortingCollection();

		internal SimpleObjectCollection? GetSortingCollectionInternal() => this.GetSortingCollection();

		//protected virtual ISimpleObjectCollection GetSortingCollectionNew()
		//{
		//	ISimpleObjectCollection result;

		//	switch (this.GetModel().SortingModel)
		//	{
		//		case SortingModel.ByOneToManyRelationKey:

		//			result = this.GetOneToManyForeignObjectCollection(this.GetModel().SortableOneToManyRelationKey);
		//			break;

		//		case SortingModel.BySingleObjectTypeCollection:

		//			result = this.Manager.GetSingleObjectTypeCollection(this.GetModel().TableInfo.TableId);
		//			break;

		//		default: 

		//			result = null;
		//			break;
		//	}

		//	return result;
		//}

		public void MoveUp()
		{
			//this.ActionSetOrderIndex = this.OrderIndex - 1;
			this.OrderIndex--;
		}

		public void MoveDown()
		{
			//this.ActionSetOrderIndex = this.OrderIndex + 1;
			this.OrderIndex++;
		}


		//protected override void OnAcceptChanges()
		//{
		//	base.OnAcceptChanges();

		//	this.actionSetOrderIndex = null; // Reset values to null since this is not property but action request
		//	this.oldActionSetOrderIndex = null;
		//}

		//protected override void OnRejectChanges()
		//{
		//	base.OnRejectChanges();

		//	this.actionSetOrderIndex = null; // Reset values to null since this is not property but action request
		//	this.oldActionSetOrderIndex = null;
		//}

		//protected override void OnBeforePropertyValueChange(IPropertyModel propertyModel, object value, object newValue, bool willBeChanged, object requester)
		//{
		//	base.OnBeforePropertyValueChange(propertyModel, value, newValue, willBeChanged, requester);

		//	if (propertyModel.IsActionSetOrderIndex && value != null) // && requester != SetOrderIndexRequester)
		//	{
		//		this.SetOrderIndexInternal((int)value, changeContainer, requester);

		//		willBeChanged = false;
		//	}                                                                                                   // == ObjectManagerWorkingMode.Server
		//}

		protected override void OnNewObjectCreated(object? requester)
		{
			base.OnNewObjectCreated(requester);

			if (this.GetModel().SortingModel == SortingModel.BySingleObjectTypeCollection)
			{
				SimpleObjectCollection? sortingCollection = this.GetSortingCollection();

				if (sortingCollection != null && !sortingCollection.Contains(this))
					sortingCollection.Add(this);
			}
		}

		protected override void OnObjectIdChange(long oldTempId, long newId)
		{
			base.OnObjectIdChange(oldTempId, newId);

			if (this.PreviousId != 0)
				this.Previous.NextId = this.Id;
			
			if (this.NextId != 0)
				this.Next.PreviousId = this.Id;
		}

		protected override void OnPropertyValueChange(IPropertyModel propertyModel, object? value, object? oldValue, bool isChanged, bool isSaveable, ChangeContainer changeContainer, object? requester)
		{
			base.OnPropertyValueChange(propertyModel, value, oldValue, isChanged, isSaveable, changeContainer, requester);

			//if (propertyModel.IsActionSetOrderIndex && value != null) // && requester != SetOrderIndexRequester)
			//{
			//	bool isReset = false;

			//	if (this.oldActionSetOrderIndex == null)
			//	{
			//		this.oldActionSetOrderIndex = this.OrderIndex; // Save value of OrderIndex before first set
			//	}
			//	else if ((int?)value == this.oldActionSetOrderIndex) // value is the same as it is first time set => set actionSetOrderIndex and oldActionSetOrderIndex to its initial state (no OrderIndex change is requested)
			//	{
			//		isReset = true;
			//	}

			//	int newOrderIndex = (int)value;
			//	int oldOrderIndex = this.OrderIndex;
			//	SimpleObjectCollection sortingCollection = this.GetSortingCollection();

			//	sortingCollection.SetOrderIndex(this.OrderIndex, newOrderIndex, changeContainer, requester);
			//	this.Manager.OrderIndexIsChanged(this, newOrderIndex, oldOrderIndex, requester);

			//	if (isReset)
			//	{
			//		this.actionSetOrderIndex = null;
			//		this.oldActionSetOrderIndex = null;
			//	}

			//	//this.SetOrderIndexInternal((int)value, changeContainer, requester);

			//	//if (this.oldSetOrderIndexAction == null)
			//	//	this.oldSetOrderIndexAction = this.OrderIndex;
			//}
			if (propertyModel.IsOrderIndex)
			{
				int orderIndex = (int)value;
				int oldOrderIndex = (int)oldValue!;

				this.GetSortingCollection()?.SetOrderIndex(orderIndex, oldOrderIndex);
				this.OnOrderIndexChange(orderIndex, oldOrderIndex, changeContainer, requester);
				this.Manager.OrderIndexIsChanged(this, (int)value, (int)oldValue, requester);
			}
		}

		protected override void OnGraphElementOrderIndexChange(GraphElement graphElement, int orderIndex, int oldOrderIndex, ChangeContainer changeContainer, object? requester)
		{
			base.OnGraphElementOrderIndexChange(graphElement, orderIndex, oldOrderIndex, changeContainer, requester);

			_ = this.GetSortingCollection(); // Initialize sorting collection, if not, to get the correct OrderIndexes of its elements.
		}

		protected virtual void OnOrderIndexChange(int orderIndex, int oldOrderIndex, ChangeContainer changeContainer, object? requester) { }
	}

	public interface ISortableBindingSimpleObject : IBindingSimpleObject
	{
		bool IsFirst { get; }
		bool IsLast { get; }
		int OrderIndex { get; }
		SortableSimpleObject Next { get; }
		SortableSimpleObject Previous { get; }
		//void SetOrderIndex(int orderIndex);
		//void SetOrderIndex(int orderIndex, ChangeContainer changeContainer, object requester);
	}
}
