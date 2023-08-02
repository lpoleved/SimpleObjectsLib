using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Collections;

namespace Simple.Objects
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Simple.Objects.SimpleObjectCollection_OLD&lt;T&gt;"/> class that contains elements copied from the specified collection 
	/// and has sufficient capacity to accommodate the number of elements copied.
	/// </summary>
	/// <typeparam name="T">SimpleObject type.</typeparam>
	public class SimpleObjectCollection_OLD<T> : SimpleList<T>, IList<T>, ICollection<T>, IEnumerable<T>, ISimpleObjectCollection_OLD, IList, ICollection, IEnumerable
		where T : SimpleObject
	{
		private SimpleObjectManager objectManager = null;
		private int update = 0;
		private bool isSortable = false;
		private SortableSimpleObject lastLoaddedClientSortableObject = null;

		#region |   Constructor(s) and Initialization   |

		public SimpleObjectCollection_OLD(SimpleObjectManager objectManager, bool isSortable = false)
		{
			this.objectManager = objectManager;
			this.isSortable = isSortable;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Simple.Objects.SimpleObjectCollection_OLD{T}"/> class that contains elements are created from the specified collection keys.
		/// </summary>
		/// <param name="objectManager">The business object manager instance.</param>
		/// <param name="objectIds">The <see cref="IEnumerable{long}"/> object Id's collection.</param>
		public SimpleObjectCollection_OLD(SimpleObjectManager objectManager, int tableId, IEnumerable<long> objectIds, bool isSortable = false)
			: this(objectManager, isSortable)
		{
			this.InnerList = new List<T>(objectIds.Count());
			//this.GetSimpleObjectDelegate = getSimpleObject;

			for (int i = 0; i < objectIds.Count(); i++)
			{
				long objectId = objectIds.ElementAt(i);
				//Guid objectKey = objectKeys.ElementAt(i);

				SimpleObject simpleObject = this.ObjectManager.GetObject(tableId, objectId);

				if (simpleObject != null && simpleObject is T)
					this.InnerList.Add(simpleObject as T); // this.ObjectManager.GetSimpleObject(tableId, objectId) as T);
			}

			//foreach (Guid objectKey in objectKeys)
			//	this.InnerList.Add(this.ObjectManager.GetObject(objectKey) as T);
		}

		//public SimpleObjectCollection(SimpleObjectManager objectManager, IEnumerable<SimpleObjectKey> objectKeys, Func<SimpleObject, T> getSimpleObject)
		//      {
		//	this.ObjectManager = objectManager;
		//          //this.GetSimpleObjectDelegate = getSimpleObject;

		//	foreach (SimpleObjectKey objectKey in objectKeys)
		//          {
		//              SimpleObject simpleObject = this.ObjectManager.GetSimpleObject(objectKey);
		//              T targeSimpleObject = (getSimpleObject != null) ? getSimpleObject(simpleObject) : simpleObject as T;

		//		this.InnerList.Add(targeSimpleObject);
		//          }
		//}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleObjectCollection{T}"/> class that wrap around the specified non-generic list.
		/// </summary>
		/// <param name="objectManager">The business object manager instance.</param>
		/// <param name="collectionToWrap">The object collection to wrap.</param>
		/// <exception cref="T:System.ArgumentNullException">The business object collection is null.</exception>
		public SimpleObjectCollection_OLD(SimpleObjectManager objectManager, ICollection<T> collectionToWrap, bool isSortable = false)
			: base(collectionToWrap)
		{
			this.objectManager = objectManager;
			this.isSortable = isSortable;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleObjectCollection_OLD{T}"/> class that wrap around the specified non-generic list.
		/// </summary>
		/// <param name="objectManager">The business object manager instance.</param>
		/// <param name="arrayToWrap">The array to wrap.</param>
		/// <exception cref="T:System.ArgumentNullException">The business object collection is null.</exception>
		public SimpleObjectCollection_OLD(SimpleObjectManager objectManager, T[] arrayToWrap, bool isSortable = false)
			: base(arrayToWrap as ICollection<T>)
		{
			this.objectManager = objectManager;
			this.isSortable = isSortable;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleObjectCollection_OLD{T}"/> class that wrap around the specified non-generic list.
		/// </summary>
		/// <param name="objectManager">The business object manager instance.</param>
		/// <param name="collectionToWrap">The object collection to wrap.</param>
		/// <exception cref="T:System.ArgumentNullException">The business object collection is null.</exception>
		public SimpleObjectCollection_OLD(SimpleObjectManager objectManager, ICollection collectionToWrap, bool isSortable = false)
			: base(collectionToWrap)
		{
			this.objectManager = objectManager;
			this.isSortable = isSortable;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Simple.Objects.SimpleObjectCollection_OLD{T}"/> class that contains elements copied from the specified collection 
		/// and has sufficient capacity to accommodate the number of elements copied.
		/// </summary>
		/// <param name="objectManager">The business object manager instance.</param>
		/// <param name="simpleObjectCollection">The business object collection whose elements are copied to the new business object collection.</param>
		/// <param name="getSimpleObject">The delegate to get the related business object based on business object from the base list.</param>
		///// <exception cref="T:System.ArgumentNullException">The simpleObjectCollection argument is null.</exception>
		///// <exception cref="T:System.ArgumentNullException">The getSimpleObject argument is null.</exception>
		public SimpleObjectCollection_OLD(SimpleObjectManager objectManager, IList simpleObjectCollection, Func<SimpleObject, SimpleObject> getSimpleObject, bool isSortable = false)
		{
			if (getSimpleObject == null)
				throw new ArgumentNullException("The getSimpleObject argument is null.");

			this.objectManager = objectManager;
			this.isSortable = isSortable;
			this.InnerList = new List<T>(simpleObjectCollection.Count);

			foreach (var element in simpleObjectCollection)
			{
				if (element != null && element is SimpleObject)
				{
					SimpleObject simpleObject = getSimpleObject(element as SimpleObject);

					if (simpleObject is T)
						this.InnerList.Add(simpleObject as T);
				}
			}
		}

		#endregion |   Constructor(s) and Initialization   |

		#region |   Properties   |

		public SimpleObjectManager ObjectManager => this.objectManager;
		public bool IsSortable => this.isSortable;
		//private Func<SimpleObject, SimpleObject> GetSimpleObjectDelegate { get; set; }

		#endregion |   Properties   |

		#region |   Public Methods   |

		public Type GetElementType()
		{
			return typeof(T);
		}

		public void BeginLoad()
		{
			this.update++;
		}

		public void EndLoad()
		{
			this.update--;

			if (this.update == 0)
			{
				this.lastLoaddedClientSortableObject = null;

				//if (this.IsSortable)
				//	this.Sort();
			}
		}

		public void Sort(bool setPrevious = false, bool saveObjects = false)
		{
			lock (this.lockObject)
			{
				if (this.Count > 0) // && this.All(item => item.GetType().IsSubclassOf(typeof(SortableSimpleObject))))
				{
					if (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Client)
					{
						//this.Sort((s1, s2) => (s1 as SortableSimpleObject).OrderIndex.CompareTo((s2 as SortableSimpleObject).OrderIndex));
						this.SetOrderIndexesAsIs(setPrevious: false, saveObjects: false);
					}
					else // if (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Server || this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.ClientWithLocalDatastore)
					{
						SortableSimpleObject item = this.FirstOrDefault(element => (element as SortableSimpleObject).IsFirst) as SortableSimpleObject;

						if (item == null)
						{
							this.SetOrderIndexesAsIs(setPrevious, saveObjects);
						}
						else // if (this.Count > 1)
						{
							int orderIndex = 0;
							SortableSimpleObject previous = item;

							item.OrderIndex = orderIndex++;

							do
							{
								item = this.FirstOrDefault(element => (element as SortableSimpleObject).PreviousId == previous.Id) as SortableSimpleObject; // Find Next item

								if (item != null)
								{
									item.OrderIndex = orderIndex++;
									previous.NextId = item.Id;
									previous = item;
								}

							}
							while (item != null && orderIndex <= this.Count);

							previous.NextId = 0;

							if (orderIndex != this.Count)
								this.SetOrderIndexesAsIs(setPrevious, saveObjects);

							//SortableSimpleObject last = this.Last() as SortableSimpleObject;

							//if (last.IsNew)
							//{
							//	last.OrderIndex = this.Count; // Prevent sorting AsItIs if new element is inserted -> Set OrderIndex of the new element only
							//	//last.oldOrderIndex = last.orderIndex;
							//}
						}
						//else
						//{
						//	item.orderIndex = 0;
						//	item.oldOrderIndex = item.orderIndex;
						//}

						this.Sort((s1, s2) => (s1 as SortableSimpleObject).OrderIndex.CompareTo((s2 as SortableSimpleObject).OrderIndex));
					}

					//int TODO = 0; // Remove this check!!!
					//if (this.Count > 1)
					//{
					//	// Check if sorting was seccessuful
					//	SortableSimpleObject itemTest = this.FirstOrDefault(element => (element as SortableSimpleObject).IsFirst) as SortableSimpleObject;
					//	int orderIndexTest = 0;

					//	while (itemTest != null && orderIndexTest <= this.Count)
					//	{
					//		itemTest = this.FirstOrDefault(element => (element as SortableSimpleObject).PreviousKey == itemTest.Key) as SortableSimpleObject; // Next item
					//		orderIndexTest++;
					//	}

					//	if (orderIndexTest != this.Count)
					//		orderIndexTest = this.Count;
					//		//throw new Exception("Somting is wrong with sorting.");
					//}
				}
			}
		}

		public void SortIfSortable()
		{
			return;

			//lock (this.lockObject)
			//{
			//	if (this.Count > 0 && this.All(item => item.GetType().IsSubclassOf(typeof(SortableSimpleObject))))
			//	{
			//		if (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Client)
			//		{
			//			this.Sort((s1, s2) => (s1 as SortableSimpleObject).OrderIndex.CompareTo((s2 as SortableSimpleObject).OrderIndex));
			//			this.SetOrderIndexesAsIs(saveObjects: false);
			//		}
			//		else // if (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Server || this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.ClientWithLocalDatastore)
			//		{
			//			SortableSimpleObject item = this.FirstOrDefault(element => (element as SortableSimpleObject).IsFirst) as SortableSimpleObject;

			//			if (item == null)
			//			{
			//				this.SetOrderIndexesAsIs(saveObjects: true);
			//			}
			//			else // if (this.Count > 1)
			//			{
			//				int orderIndex = 0;

			//				do
			//				{
			//					item.orderIndex = orderIndex++;
			//					item.oldOrderIndex = item.orderIndex;
			//					item = this.FirstOrDefault(element => (element as SortableSimpleObject).PreviousId == item.Id) as SortableSimpleObject; // Next item
			//				}
			//				while (item != null && orderIndex <= this.Count);

			//				if (orderIndex != this.Count)
			//					this.SetOrderIndexesAsIs(saveObjects: true);

			//				SortableSimpleObject last = this.Last() as SortableSimpleObject;

			//				if (last.IsNew)
			//				{
			//					last.orderIndex = this.Count; // Prevent sorting AsItIs if new element is inserted -> Set OrderIndex of the new element only
			//					last.oldOrderIndex = last.orderIndex;
			//				}
			//			}
			//			//else
			//			//{
			//			//	item.orderIndex = 0;
			//			//	item.oldOrderIndex = item.orderIndex;
			//			//}

			//			this.Sort((s1, s2) => (s1 as SortableSimpleObject).OrderIndex.CompareTo((s2 as SortableSimpleObject).OrderIndex));
			//		}

			//		//int TODO = 0; // Remove this check!!!
			//		//if (this.Count > 1)
			//		//{
			//		//	// Check if sorting was seccessuful
			//		//	SortableSimpleObject itemTest = this.FirstOrDefault(element => (element as SortableSimpleObject).IsFirst) as SortableSimpleObject;
			//		//	int orderIndexTest = 0;

			//		//	while (itemTest != null && orderIndexTest <= this.Count)
			//		//	{
			//		//		itemTest = this.FirstOrDefault(element => (element as SortableSimpleObject).PreviousKey == itemTest.Key) as SortableSimpleObject; // Next item
			//		//		orderIndexTest++;
			//		//	}

			//		//	if (orderIndexTest != this.Count)
			//		//		orderIndexTest = this.Count;
			//		//		//throw new Exception("Somting is wrong with sorting.");
			//		//}
			//	}
			//}
		}

		//public void SortIfSortable2()
		//{
		//	return;

		//	//if (this.Count > 0 && typeof(T).IsSubclassOf(typeof(SortableSimpleObject)))
		//	//{
		//	//	lock (this.lockObject)
		//	//	{
		//	//		if (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Client)
		//	//		{
		//	//			this.Sort((s1, s2) => (s1 as SortableSimpleObject).OrderIndex.CompareTo((s2 as SortableSimpleObject).OrderIndex));
		//	//			this.SetOrderIndexesAsIs(saveObjects: false);
		//	//		}
		//	//		else // if (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Server || this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.ClientWithLocalDatastore)
		//	//		{
		//	//			SortableSimpleObject item = this.FirstOrDefault(element => (element as SortableSimpleObject).IsLast) as SortableSimpleObject;

		//	//			if (item == null)
		//	//			{
		//	//				this.SetOrderIndexesAsIs(saveObjects: true);
		//	//			}
		//	//			else
		//	//			{
		//	//				int orderIndex = this.Count;

		//	//				while (item != null && orderIndex >= 0)
		//	//				{
		//	//					item.orderIndex = --orderIndex;
		//	//					item.oldOrderIndex = item.orderIndex;
		//	//					item = item.Previous;
		//	//				}

		//	//				if (orderIndex != 0)
		//	//					this.SetOrderIndexesAsIs(saveObjects: true);

		//	//				if (this.Last().IsNew)
		//	//				{
		//	//					// Prevent sorting AsItIs if new element is inserted -> Set OrderIndex of the new element only
		//	//					SortableSimpleObject last = this.Last() as SortableSimpleObject;
		//	//					last.orderIndex = this.Count;
		//	//					last.oldOrderIndex = last.orderIndex;
		//	//				}
		//	//			}

		//	//			this.Sort((s1, s2) => (s1 as SortableSimpleObject).OrderIndex.CompareTo((s2 as SortableSimpleObject).OrderIndex));
		//	//		}

		//	//		////int orderIndex = 0;
		//	//		//SortableSimpleObject previous = null;
		//	//		//IEnumerable<SortableSimpleObject> firstItems = from element in this
		//	//		//											   where (element as SortableSimpleObject).IsFirst
		//	//		//											   select element as SortableSimpleObject;
		//	//		//if (firstItems.Count() == 1)
		//	//		//{
		//	//		//	this.SetOrdering(updateAndSaveObjects: false);
		//	//		//}
		//	//		//else
		//	//		//{
		//	//		//	this.SetOrdering(updateAndSaveObjects: true);
		//	//		//}

		//	//		////if (firstItems == null || firstItems.Count() != 1)
		//	//		////{
		//	//		////	SortableSimpleObject item = firstItems.ElementAt(0);

		//	//		////	while (item != null)
		//	//		////	{
		//	//		////		item.OrderIndex = orderIndex++;
		//	//		////		item.Previous = previous;
		//	//		////		previous = item;
		//	//		////		item = item.Next;

		//	//		////		if (orderIndex > this.Count)
		//	//		////			break;
		//	//		////	}

		//	//		////	if (orderIndex != this.Count())
		//	//		////	{
		//	//		////		if (orderIndex == this.Count() - 1)
		//	//		////		{
		//	//		////			if (this.Last().IsNew)
		//	//		////			{
		//	//		////				// Prevent sorting AsItIs if new element is inserted -> Set OrderIndex of the new element only
		//	//		////				(this.Last() as SortableSimpleObject).OrderIndex = orderIndex++;
		//	//		////			}
		//	//		////			else if (!this.Last().IsDeleteStarted)
		//	//		////			{
		//	//		////				this.SetOrdering(updateAndSaveObjects: false);
		//	//		////			}
		//	//		////		}
		//	//		////		else
		//	//		////		{
		//	//		////			this.SetOrdering(updateAndSaveObjects: false);
		//	//		////		}
		//	//		////	}
		//	//		////}
		//	//		////else
		//	//		////{
		//	//		////	this.SetOrdering(updateAndSaveObjects: false);
		//	//		////}

		//	//		//this.OrderBy(element => (element as SortableSimpleObject).OrderIndex);
		//	//	}
		//	//}
		//}

		#endregion |   Public Methods   |

		#region |   Internal Protected Methods   |

		internal protected new void ListAdd(T value)
		{
			base.ListAdd(value);
		}

		internal protected new void ListRemove(T item)
		{
			base.ListRemove(item);
		}

		#endregion |   Internal Protected Methods   |

		#region |   Protected Override Methods   |

		protected override T ListGet(int index)
		{
			return base.ListGet(index);
		}

		protected override void ListSet(int index, T item)
		{
			base.ListSet(index, item);
		}

		protected override int InternalListAdd(T value)
		{
			return base.InternalListAdd(value);
		}

		protected override bool InternalListRemoveAt(int index)
		{
			return base.InternalListRemoveAt(index);
		}

		protected override void InternalListClear()
		{
			base.InternalListClear();
		}

		protected override void OnBeforeInsert(int index, T value)
		{
			base.OnBeforeInsert(index, value);

			if (this.IsSortable && this.update == 0)
			{
				//if (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Client)
				//{
				// When inserting the new object in the collection, setup PreviosId and NextId of the affected SortableSimpleObject elements in normal working mode (loadind data is not in progress).
				lock (this.lockObject)
				{
					SortableSimpleObject current = (value as SortableSimpleObject);

					if (this.Count == 0)
					{
						current.PreviousId = 0; // No need for this if not prev and next is not compromised
						current.NextId = 0;
					}
					else if (index == 0) // Insert at the top
					{
						SortableSimpleObject next = this.ListGet(0) as SortableSimpleObject;

						current.PreviousId = 0;
						current.NextId = next.Id;
						next.PreviousId = current.Id;
					}
					else if (index == this.Count) // Insert at the bottom
					{
						SortableSimpleObject previous = this.ListGet(index - 1) as SortableSimpleObject;

						previous.NextId = current.Id;
						current.PreviousId = previous.Id;
						current.NextId = 0;
					}
					else // Insert in a middle
					{
						SortableSimpleObject previous = this.ListGet(index - 1) as SortableSimpleObject;
						SortableSimpleObject next = this.ListGet(index) as SortableSimpleObject;

						previous.NextId = current.Id;
						current.PreviousId = previous.Id;
						current.NextId = next.Id;
						next.PreviousId = current.Id;
					}

					current.OrderIndex = index;
					//}
				}

				for (int i = index; i <= this.Count - 1; i++)
					(this[i] as SortableSimpleObject).OrderIndex = i + 1;
			}
		}

		protected override void OnAfterInsert(int index, T value)
		{
			base.OnAfterInsert(index, value);

			if (this.IsSortable && this.update > 0)
			{
				// Loading is in progress and the objects are sortable => set PreviousId, NextId and OrderIndex on the client side.
				// It is assumed that this is insertion is done by Add method and the elements is added at the end of the collection.

				SortableSimpleObject current = (value as SortableSimpleObject);

				if (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Client && this.lastLoaddedClientSortableObject != null)
				{
					this.lastLoaddedClientSortableObject.NextId = current.Id;
					current.PreviousId = this.lastLoaddedClientSortableObject.Id; // Loading is in progress -> no tracking changess
																				  //current.previousId = this.lastLoaddedClientSortableObject.Id; // Loading is in progress -> no tracking changess
																				  //current.oldPreviousId = current.previousId;
					this.lastLoaddedClientSortableObject = current;
				}

				current.OrderIndex = index;
			}
		}

		protected override void OnBeforeRemove(int index, T value)
		{
			base.OnBeforeRemove(index, value);

			if (this.IsSortable && update == 0)
			{
				SortableSimpleObject current = this.ListGet(index) as SortableSimpleObject;

				if (index == 0) // Removing first one
				{
					current.PreviousId = 0;
					current.NextId = 0;
				}
				else if (index == this.Count - 1) // Removing last one
				{
					(this.ListGet(index - 1) as SortableSimpleObject).NextId = 0;
					current.PreviousId = 0;
				}
				else // if (index < this.Count - 1) // Removing in the middle
				{
					SortableSimpleObject previous = this.ListGet(index - 1) as SortableSimpleObject;
					SortableSimpleObject next = this.ListGet(index + 1) as SortableSimpleObject;

					previous.NextId = next.Id;
					next.PreviousId = previous.Id;

					current.PreviousId = 0;
					current.NextId = 0;
				}

				current.OrderIndex = 0;
			}
		}

		protected override void OnAfterRemove(int index, T value)
		{
			base.OnAfterRemove(index, value);

			if (this.Count == 0)
				this.lastLoaddedClientSortableObject = null;
		}

		//protected override void OnCountChange(int count, int oldCount)
		//{
		//	base.OnCountChange(count, oldCount);

		//	if (count == 0)
		//		this.lastLoaddedClientSortableObject = null;
		//}

		#endregion |   Protected Override Methods   |

		#region |   ISimpleObjectCollection implementation   |

		SimpleObject ISimpleObjectCollection_OLD.ElementAt(int index)
		{
			return this.ElementAt(index);
		}

		void ISimpleObjectCollection_OLD.Add(SimpleObject item)
		{
			this.Add(item as T);
		}

		void ISimpleObjectCollection_OLD.Remove(SimpleObject item)
		{
			this.Remove(item as T);
		}

		void ISimpleObjectCollection_OLD.Insert(int index, SimpleObject item)
		{
			this.Insert(index, item as T);
		}

		void ISimpleObjectCollection_OLD.SetOrderIndex(SortableSimpleObject sortableSimpleObject, int orderIndex, ChangeContainer changeContainer, object requester)
		{
			lock (this.lockObject)
			{
				int minPos;
				int maxPos;
				int oldOrderIndex = sortableSimpleObject.OrderIndex;
				SortableSimpleObject itemWithSpecifiedOrderIndex = this.ElementAt(orderIndex) as SortableSimpleObject; // .FirstOrDefault(sortedSimpleObject => sortedSimpleObject.OrderIndex == orderIndex);

				if (oldOrderIndex < orderIndex)
				{
					minPos = oldOrderIndex;
					maxPos = orderIndex;
				}
				else
				{
					minPos = orderIndex;
					maxPos = oldOrderIndex;
				}

				//this.BeginLoad();
				this.InnerList.RemoveAt(oldOrderIndex);

				// Remove from current position
				if (sortableSimpleObject.PreviousId != 0)
					sortableSimpleObject.Previous.NextId = sortableSimpleObject.NextId;

				if (sortableSimpleObject.NextId != 0)
					sortableSimpleObject.Next.PreviousId = sortableSimpleObject.PreviousId;

				// Insert into new position specified by orderIndex
				if (orderIndex < oldOrderIndex)
				{
					// Move upper
					sortableSimpleObject.PreviousId = itemWithSpecifiedOrderIndex.PreviousId;

					if (sortableSimpleObject.Previous != null)
						sortableSimpleObject.Previous.NextId = sortableSimpleObject.Id;

					sortableSimpleObject.NextId = itemWithSpecifiedOrderIndex.Id;
					itemWithSpecifiedOrderIndex.PreviousId = sortableSimpleObject.Id;
				}
				else
				{
					// Move under
					sortableSimpleObject.NextId = itemWithSpecifiedOrderIndex.NextId;

					if (sortableSimpleObject.NextId != 0)
						sortableSimpleObject.Next.PreviousId = sortableSimpleObject.Id;

					sortableSimpleObject.PreviousId = itemWithSpecifiedOrderIndex.Id;
					itemWithSpecifiedOrderIndex.NextId = sortableSimpleObject.Id;
				}

				this.InnerList.Insert(orderIndex, sortableSimpleObject as T);
				//this.EndLoad();

				for (int i = minPos; i <= maxPos; i++)
					(this[i] as SortableSimpleObject).OrderIndex = i;
			}
		}

		#endregion |   ISimpleObjectCollection implementation   |

		#region |   Private Methods   |

		private void SetOrderIndexesAsIs(bool setPrevious, bool saveObjects)
		{
			SortableSimpleObject previous = null;

			for (int i = 0; i < this.Count; i++)
			{
				SortableSimpleObject item = this[i] as SortableSimpleObject;

				if (setPrevious)
					item.PreviousId = (previous != null) ? previous.Id : 0;

				item.NextId = 0;
				item.OrderIndex = i;

				if (previous != null)
					previous.NextId = item.Id;

				previous = item;
			}

			if (saveObjects && this.ObjectManager.DefaultChangeContainer.RequireCommit)
				this.ObjectManager.CommitChanges();
		}

		//private void SetOrdering2(bool updateAndSaveObjects)
		//{
		//	SortableSimpleObject firstItem = null;
		//	SortableSimpleObject previous = null;

		//	for (int i = 0; i < this.Count; i++)
		//	{
		//		SortableSimpleObject item = this[i] as SortableSimpleObject;

		//		if (updateAndSaveObjects)
		//		{
		//			item.Previous = previous;

		//			if (i == 0)
		//			{
		//				firstItem = item;
		//			}
		//			else
		//			{
		//				firstItem.RelatedTransactionRequests.Add(new SimpleObjectTransactionRequest(item, TransactionRequest.Save));
		//			}

		//			previous = item;
		//		}

		//		item.OrderIndex = i;
		//	}

		//	if (updateAndSaveObjects && firstItem != null)
		//		firstItem.Save();
		//}

		#endregion |   Private Methods   |
	}

	public interface ISimpleObjectCollection_OLD : IList, ICollection
	{
		Type GetElementType();

		SimpleObject ElementAt(int index);
		void Add(SimpleObject item);
		void Remove(SimpleObject item);
		void Insert(int index, SimpleObject item);

		void BeginLoad();
		void EndLoad();

		bool IsSortable { get; }
		void SetOrderIndex(SortableSimpleObject sortableSimpleObject, int orderIndex, ChangeContainer changeContainer, object requester);
		void Sort(bool setPrevious, bool saveObjects);
	}
}
