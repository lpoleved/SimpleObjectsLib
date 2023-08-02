//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple;
//using Simple.Collections;

//namespace Simple.Objects
//{
//	public abstract class SortableSimpleObject<T> : SortableSimpleObject where T : SortableSimpleObject<T>, ISortableBindingSimpleObject, IBindingSimpleObject, IDisposable
//	{
//		//public SortableSimpleObject()
//		//{
//		//}

//		public SortableSimpleObject(SimpleObjectManager objectManager)
//			: base(objectManager)
//		{
//		}

//		//public SortableSimpleObject(SimpleObjectManager objectManager, bool isNew)
//		//	: base(objectManager, isNew)
//		//{
//		//}

//		public new T Next
//		{
//			get { return base.Next as T; }
//			protected internal set { base.Next = value; }
//		}

//		public new T Previous
//		{
//			get { return base.Previous as T; }
//			protected internal set { base.Previous = value; }
//		}

//		//protected void SetOrderingBeforeRemovingFromCollection(object requester, SimpleObjectCollection<T> parentGraphElements)
//		//{
//		//	base.SetOrderingBeforeRemovingFromCollection(requester, parentGraphElements);
//		//}

//		//protected void SetOrderingAfterInsertingIntoCollection(object requester, SimpleObjectCollection<T> parentGraphElements)
//		//{
//		//	base.SetOrderingAfterInsertingIntoCollection(requester, parentGraphElements);
//		//}
//	}

//	[IncludeProperty(SimpleObject.StringPropertyIsFirst, SimpleObject.StringPropertyNextKey, SimpleObject.StringPropertyPreviousKey)]
//	public abstract partial class SortableSimpleObject : SimpleObject, ISortableBindingSimpleObject, IBindingSimpleObject, IDisposable  // SimpleObject
//	{
//		private SortableSimpleObject next = null;
//		private bool isNextFetched = false;
//		private SortableSimpleObject previous = null;
//		private bool isPreviousSet = false;
//		private int orderIndex = 0;

//		//protected bool isFirst, oldIsFirst;
//		//protected Guid nextGuid, oldNextGuid;

//		//public SortableSimpleObject()
//		//      {
//		//      }


//		public SortableSimpleObject(SimpleObjectManager objectManager)
//			: base(objectManager)
//		{
//		}

//		//public SortableSimpleObject(SimpleObjectManager objectManager, bool isNew)
//		//    : base(objectManager, isNew)
//		//{
//		//}
//		//public bool IsFirst
//		//{
//		//	get { return this.isFirst; }
//		//	protected internal set { this.SetPropertyValue<bool>(SimpleObject.IndexPropertyIsFirst, value, ref this.isFirst, ref this.oldIsFirst); } //, firePropertyValueChangeEvent: false, addOrRemoveInChangedProperties: true, requester: this); }
//		//}

//		//private Guid NextGuid
//		//{
//		//	get { return this.nextGuid; }
//		//	set { this.SetPropertyValue<Guid>(SimpleObject.IndexPropertyIsFirst, value, ref this.nextGuid, ref this.oldNextGuid); } //, firePropertyValueChangeEvent: false, addOrRemoveInChangedProperties: true, requester: this); }
//		//}

//		public bool IsLast
//		{
//			get { return this.Next == null; }
//		}

//		public SortableSimpleObject Next
//		{
//			get
//			{
//				lock (this.lockObject)
//				{
//					if (!this.isNextFetched)
//					{
//						//Guid nextGuid = this.GetPropertyValue<Guid>(SimpleObject.IndexPropertyNextGuid);
//						this.next = this.ObjectManager.GetSimpleObject(this.NextKey) as SortableSimpleObject;
//						this.isNextFetched = true;
//					}
//				}

//				return this.next;
//			}

//			protected internal set
//			{
//				lock (this.lockObject)
//				{
//					if (this.next != value)
//					{
//						this.next = value;
//						this.NextKey = (value != null) ? value.Key : Guid.Empty;
//						//this.SetPropertyValueInternal(SimpleObject.IndexPropertyNextGuid, nextGuid, firePropertyValueChangeEvent: false, addOrRemoveInChangedProperties: true, requester: this);
//						this.isNextFetched = true;
//					}
//				}
//			}
//		}

//		public SortableSimpleObject Previous
//		{
//			get
//			{
//				if (!this.isPreviousSet)
//				{
//					IList<SortableSimpleObject> sortingCollection = this.GetSortingCollection() as IList<SortableSimpleObject>;

//					if (sortingCollection != null)
//					{
//						this.previous = sortingCollection.FirstOrDefault(item => item.Next == this);
//						this.isPreviousSet = true;
//					}
//				}

//				return this.previous;
//			}

//			protected internal set
//			{
//				this.previous = value;
//				this.isPreviousSet = true;
//			}
//		}

//		public int OrderIndex
//		{
//			get { return this.orderIndex; }
//			protected internal set { this.orderIndex = value; }
//		}

//		/// <summary>
//		/// When override, give the unsorted collection as related main sorting collection for this sorting object.
//		/// </summary>
//		/// <returns></returns>
//		protected internal virtual IList<SortableSimpleObject> GetSortingCollection()
//		{
//			IList<SortableSimpleObject> value = null;
//			int oneToManyRelationKey = this.GetModel().SortableOneToManyRelationKey;

//			if (oneToManyRelationKey > -1)
//			{
//				SimpleObject parentCollectionOwner = this.GetOneToManyRelationForeignObject<SimpleObject>(oneToManyRelationKey);

//				if (parentCollectionOwner != null)
//					value = parentCollectionOwner.GetOneToManyRelationKeyHolderObjectCollection<SortableSimpleObject>(this.GetModel().SortableOneToManyRelationKey, sortIfSortable: false);
//			}

//			return value;
//		}

//		public void SetOrderIndex(int orderIndex)
//		{
//			this.SetOrderIndex(orderIndex, requester: this);
//		}

//		public void SetOrderIndex(int orderIndex, object requester)
//		{
//			lock (this.lockObject)
//			{
//				if (orderIndex < 0)
//					return;

//				if (orderIndex == this.OrderIndex)
//					return;

//				IEnumerable<SortableSimpleObject> collection = this.GetSortingCollection();
//				//collection.SortByDefaultDatastoreSortingIfSortable();

//				if (orderIndex >= collection.Count())
//					return;

//				int currentOrderIndex = this.OrderIndex;
//				List<SortableSimpleObject> relatedTransactionItems = new List<SortableSimpleObject>();
//				SortableSimpleObject itemWithSpecifiedOrderIndex = collection.FirstOrDefault(sortedSimpleObject => sortedSimpleObject.OrderIndex == orderIndex);

//				// Remove from current position
//				SortableSimpleObject oldNext = this.Next;

//				if (oldNext != null && this.IsFirst)
//				{
//					oldNext.IsFirst = true;     //oldNext.SetPropertyValueInternal(GraphElementModel.PropertyModel.IsFirst.Name, true, firePropertyValueChangeEvent: true, addOrRemoveInChangedPropertyNames: true, requester: this);
//					this.IsFirst = false;       //this.SetPropertyValueInternal(GraphElementModel.PropertyModel.IsFirst.Name, false, firePropertyValueChangeEvent: true, addOrRemoveInChangedPropertyNames: true, requester: this);
//					oldNext.Previous = null;
//					relatedTransactionItems.Add(oldNext);
//				}
//				else
//				{
//					if (this.Previous != null)
//					{
//						this.Previous.Next = oldNext;

//						if (oldNext != null)
//							oldNext.Previous = this.Previous;

//						//if (parentGraphElements.Count == 2)
//						//	previous.SetPropertyValueInternal(GraphElementModel.PropertyModel.IsFirst.Name, true, firePropertyValueChangeEvent: true, addOrRemoveInChangedPropertyNames: true, requester: this);

//						relatedTransactionItems.Add(this.Previous);
//					}
//				}

//				// Insert into new position specified by orderIndex
//				if (orderIndex == 0)
//				{
//					if (itemWithSpecifiedOrderIndex != null)
//					{
//						itemWithSpecifiedOrderIndex.IsFirst = false;        //graphElementWithOrderIndex.SetPropertyValueInternal(GraphElementModel.PropertyModel.IsFirst.Name, false, firePropertyValueChangeEvent: true, addOrRemoveInChangedPropertyNames: true, requester: this);
//						itemWithSpecifiedOrderIndex.Previous = this;
//						relatedTransactionItems.Add(itemWithSpecifiedOrderIndex);
//					}

//					this.IsFirst = true;        //this.SetPropertyValueInternal(GraphElementModel.PropertyModel.IsFirst.Name, true, firePropertyValueChangeEvent: true, addOrRemoveInChangedPropertyNames: true, requester: this);
//					this.Previous = null;
//					this.Next = itemWithSpecifiedOrderIndex;
//				}
//				else
//				{
//					if (currentOrderIndex < orderIndex)
//					{
//						if (itemWithSpecifiedOrderIndex != null)
//						{
//							oldNext = itemWithSpecifiedOrderIndex.Next;
//							itemWithSpecifiedOrderIndex.Next = this;
//							relatedTransactionItems.Add(itemWithSpecifiedOrderIndex);
//						}

//						this.Previous = itemWithSpecifiedOrderIndex;
//						this.Next = oldNext;

//						if (oldNext != null)
//							oldNext.Previous = this;
//					}
//					else
//					{
//						if (itemWithSpecifiedOrderIndex != null)
//						{
//							this.Previous = itemWithSpecifiedOrderIndex.Previous;

//							if (this.Previous != null)
//							{
//								this.Previous.Next = this;
//								relatedTransactionItems.Add(this.Previous);
//							}

//							itemWithSpecifiedOrderIndex.Previous = this;

//						}

//						this.Next = itemWithSpecifiedOrderIndex;

//					}
//				}

//				foreach (SortableSimpleObject relatedTransactionItem in relatedTransactionItems)
//					this.RelatedTransactionRequests.Add(new TransactionActionRequest(relatedTransactionItem, TransactionActionRequestType.Save));

//				int index = 0;
//				SortableSimpleObject item = collection.FirstOrDefault(sortedSimpleObject => sortedSimpleObject.IsFirst == true);

//				while (item != null)
//				{
//					item.OrderIndex = index++;
//					item = item.Next;
//				}

//				this.ObjectManager.OrderIndexIsChanged(requester, this, currentOrderIndex, orderIndex);
//			}
//		}

//		protected internal void SetOrderingBeforeRemovingFromCollection(object requester, IEnumerable<SortableSimpleObject> collection)
//		{
//			SortableSimpleObject next = this.Next;

//			if (next != null && this.IsFirst)
//			{
//				next.IsFirst = true;    //this.SetPropertyValueInternal(GraphElementModel.PropertyModel.IsFirst.Name, true, firePropertyValueChangeEvent: true, addOrRemoveInChangedPropertyNames: true, requester: requester);
//				next.Previous = null;
//				this.RelatedTransactionRequests.Add(new TransactionActionRequest(next, TransactionActionRequestType.Save));
//			}
//			else
//			{
//				if (this.Previous != null)
//				{
//					this.Previous.Next = next;

//					if (next != null)
//						next.Previous = this.Previous;

//					if (collection.Count() == 2)
//						this.Previous.IsFirst = true;       //previous.SetPropertyValueInternal(GraphElementModel.PropertyModel.IsFirst.Name, true, firePropertyValueChangeEvent: true, addOrRemoveInChangedPropertyNames: true, requester: requester);

//					this.RelatedTransactionRequests.Add(new TransactionActionRequest(this.Previous, TransactionActionRequestType.Save));
//				}
//			}

//			while (next != null)
//			{
//				next.OrderIndex--;
//				next = next.Next;
//			}
//		}

//		protected internal void SetOrderingAfterInsertingIntoCollection(object requester, IList<SortableSimpleObject> collection)
//		{
//			//if (this.isPreviousSet && this.previous != null && this.previous.ChangedPropertyNames.Count > 0 && this.RelatedTransactionRequests.FirstOrDefault(item => item.SimpleObject == this.previous) == null)
//			//{
//			//	var a = this.RelatedTransactionRequests.FirstOrDefault(item => item.SimpleObject == this.previous);

//			//	this.RelatedTransactionRequests.Add(new TransactionActionRequest(this.previous, TransactionActionRequestType.Save));
//			//}
//			//else
//			//{
//			SortableSimpleObject previous = null;

//			for (int i = 0; i < collection.Count(); i++)
//			{
//				if (collection[i].Next == null && collection[i] != this)
//				{
//					previous = collection[i];
//					break;
//				}
//			}

//			//SortableSimpleObject previous = collection.FirstOrDefault(sortableElement => sortableElement.Next == null && sortableElement != this);

//			this.Next = null;

//			if (previous != null)
//			{
//				this.IsFirst = false;       //this.SetPropertyValueInternal(GraphElementModel.PropertyModel.IsFirst.Name, false, firePropertyValueChangeEvent: true, addOrRemoveInChangedPropertyNames: true, requester: requester);
//											//this.Next = null; //previous.Next;
//				this.Previous = previous;
//				previous.Next = this;

//				this.RelatedTransactionRequests.Add(new TransactionActionRequest(previous, TransactionActionRequestType.Save));

//				//if (!previous.IsNew)
//				//{
//				//	if (saveInternal)
//				//	{
//				//		this.Manager.SaveInternal(requester, previous);
//				//	}
//				//	else
//				//	{
//				//		previous.Save(requester);
//				//	}
//				//}
//			}
//			else
//			{
//				this.IsFirst = (collection.Count() == 1);       //this.SetPropertyValueInternal(GraphElementModel.PropertyModel.IsFirst.Name, true, firePropertyValueChangeEvent: true, addOrRemoveInChangedPropertyNames: true, requester: requester);
//																//this.Next = null;
//				this.Previous = null;
//			}


//			this.OrderIndex = collection.Count() - 1; // this object is inserted at the end of the collection
//													  //this.OrderIndex = (collection.Contains(this)) ? collection.Count() - 1 : collection.Count();
//													  //}
//		}

//		protected override void OnBeforeDeleting(object requester)
//		{
//			lock (this.lockObject)
//			{
//				base.OnBeforeDeleting(requester);
//				this.SetOrderingBeforeRemovingFromCollection(requester, this.GetSortingCollection());
//			}
//		}

//		//public static IList<T> Sort<T>(IList<T> unsortedItems) where T : SortableSimpleObject
//		//{
//		//	List<T> sortedItems = new List<T>();
//		//	int orderIndex = 0;
//		//	T previous = null;
//		//	T firstItem = unsortedItems.FirstOrDefault(item => item.IsFirst);

//		//	if (firstItem != null)
//		//	{
//		//		T item = firstItem;

//		//		while (item != null)
//		//		{
//		//			if (sortedItems.Contains(item))
//		//			{
//		//				sortedItems.Clear();
//		//				break;
//		//			}

//		//			sortedItems.Add(item);
//		//			item.OrderIndex = orderIndex++;
//		//			item.Previous = previous;
//		//			previous = item;
//		//			item = item.Next as T;
//		//		}
//		//	}

//		//	if (sortedItems.Count != unsortedItems.Count())
//		//	{
//		//		sortedItems = new List<T>(unsortedItems);
//		//		orderIndex = 0;

//		//		foreach (T item in sortedItems)
//		//			item.OrderIndex = orderIndex++;
//		//	}

//		//	//sortedItems.GetEnumerator().Reset();

//		//	return sortedItems;
//		//}

//		//public static IList<T> Sort<T>(IList unsortedItems) where T : SortableSimpleObject
//		//{
//		//	if (unsortedItems.Count == 0)
//		//		return new List<T>();

//		//	if (!(unsortedItems[0] is SortableSimpleObject))
//		//		return new SimpleList<T>(unsortedItems);

//		//	return Sort<T>(new SimpleList<T>(unsortedItems) as IList<T>);
//		//}
//	}

//	public interface ISortableBindingSimpleObject : IBindingSimpleObject
//	{
//		bool IsFirst { get; }
//		bool IsLast { get; }
//		int OrderIndex { get; }
//		SortableSimpleObject Next { get; }
//		SortableSimpleObject Previous { get; }
//		void SetOrderIndex(int orderIndex);
//		void SetOrderIndex(int orderIndex, object requester);
//	}
//}
