using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple;

namespace Simple.Objects
{
	/// <summary>
	/// Generic SimpleObject collection holder class. Only object Ids or object keys (pair of tableId and objectId) are stored within the class. SimpleObject are feched from the ObjectCache.
	/// </summary>
	/// <typeparam name="T">SimpleObject type</typeparam>
	public class SimpleObjectCollection<T> : SimpleObjectCollection, IEnumerable<T>, IEnumerable, IDisposable
		where T : SimpleObject
	{
		// TODO: Check if this ctor require (tableId is specified by the T type).
		public SimpleObjectCollection(SimpleObjectManager objectManager, int tableId, IList<long> objectIds)
			: base(objectManager, tableId, objectIds)
		{
		}

		public SimpleObjectCollection(SimpleObjectManager objectManager, IList<int> tableIds, IList<long> objectIds)
			: base(objectManager, tableIds, objectIds)
		{
		}

		internal SimpleObjectCollection(int tableId, SimpleObjectCollectionContaier collectionContainer)
			: base(tableId, collectionContainer)
		{
		}

		public new T this[int index] => (T)base[index];
		public new T ElementAt(int index) => (T)base.ElementAt(index);

		public override Type GetElementType() => typeof(T);
		public new SimpleObjectCollection<T> Copy() => (base.Copy() as SimpleObjectCollection<T>)!;

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.</returns>
		public new IEnumerator<T> GetEnumerator()
		{
			for (int index = 0; index < this.Count; index++)
				yield return this[index];
		}

		protected override SimpleObjectCollection CreateCopy() => new SimpleObjectCollection<T>(this.tableId, this.collectionContainer.CreateCopy());
	}

	/// <summary>
	/// SimpleObject collection holder class. Only object Ids or object keys (pair of tableId and objectId) are stored within the class. SimpleObject are feched from the ObjectCache.
	/// </summary>
	public class SimpleObjectCollection : IEnumerable, IDisposable //, IEnumerable<SimpleObject>
	{
		private SimpleObjectCollection<GroupMembership>? groupMembershipCollection = null;
		private SortableSimpleObject? lastLoaddedClientSortableObject = null;
		internal SimpleObjectCollectionContaier collectionContainer;
		//private int update = 0;

		internal int tableId = 0;
		//protected  IList<int> tableIds = null;
		//protected IList<long> objectIds = null;
		protected readonly object lockObject = new object();

		//private SimpleObjectManager objectManager = null;
		//private int tableId = 0;
		//private ObjectCache objectCache = null;
		//protected IList<long> objectIds = null;
		//protected IList<SimpleObjectKeyNew> objectKeys = null;

		/// <summary>
		/// SimpleObject collection holder class. Only object Ids or object keys (pair of tableId and objectId) are stored within the class. SimpleObject are feched from the ObjectCache.
		/// </summary>
		/// <param name="objectManager">The ObjectManager</param>
		/// <param name="objectModel">The SimpleObject model</param>
		/// <param name="objectIds">Object Id's collection to wrap</param>
		public SimpleObjectCollection(SimpleObjectManager objectManager, int tableId, IList<long> objectIds)
		{
			this.tableId = tableId;
			this.collectionContainer = new SimpleSingleTypeObjectCollectionContainer(objectManager, tableId, objectIds);

			//this.objectIds = objectIds;
		}

		public SimpleObjectCollection(SimpleObjectManager objectManager, IList<int> tableIds, IList<long> objectIds)
		{
			this.collectionContainer = new SimpleMultyTypeObjectCollectionContainer(objectManager, tableIds, objectIds);
			//this.tableIds = tableIds;
			//this.objectIds = objectIds;
		}

		internal SimpleObjectCollection(int tableId, SimpleObjectCollectionContaier collectionContainer)
		{
			this.tableId = tableId;
			this.collectionContainer = collectionContainer;
		}

		public int Count => this.collectionContainer.Count;
		protected SimpleObjectManager ObjectManager => this.collectionContainer.ObjectManager;

		public SimpleObject this[int index] => this.ElementAt(index);

		public SimpleObject ElementAt(int index) => this.collectionContainer.ElementAt(index);

		//public int TableIdAt(int index) => (this.tableId > 0) ? this.tableId : this.tableIds[index];

		//public long ObjectIdAt(int index) => this.objectIds[index];
		//public SimpleObjectKey KeyAt(int index) => new SimpleObjectKey(this.TableIdAt(index), this.ObjectIdAt(index));

		public int IndexOf(int tableId, long objectId) => this.collectionContainer.IndexOf(tableId, objectId);
		public int IndexOf(long objectId) => this.collectionContainer.IndexOf(this.tableId, objectId);

		public virtual Type? GetElementType() => (this.Count > 0) ? this.ElementAt(0).GetType() : null;
		public SimpleObjectCollection<T> AsCustom<T>() where T : SimpleObject => new SimpleObjectCollection<T>(tableId, this.collectionContainer);

		internal void Add(SimpleObject simpleObject) => this.Add(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);
		//internal void AddInternal(SimpleObject simpleObject) => this.collectionContainer.Insert(this.Count, simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);
		internal void Add(long objectId) => this.Add(this.tableId, objectId);
		internal void Add(int tableId, long objectId) => this.Insert(this.Count, tableId, objectId);

		internal void Insert(SimpleObject simpleObject) => this.Insert(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);
		internal void Insert(int index, long objectId) => this.Insert(index, this.tableId, objectId);
		internal void Insert(int index, int tableId, long objectId)
		{
			lock (this.lockObject)
			{
				if (this.Contains(tableId, objectId))
					throw new DuplicateObjectException("SimpleObject already added to the collection");

				this.OnBeforeInsert(index, tableId, objectId);
				this.collectionContainer.Insert(index, tableId, objectId);
				this.OnAfterInsert(index, tableId, objectId);
			}
		}

		internal bool Remove(SimpleObject simpleObject) => this.Remove(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);
		//internal bool RemoveInternal(SimpleObject simpleObject) => this.RemoveInternal(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);
		internal bool Remove(long objectId) => this.Remove(this.tableId, objectId);
		internal bool Remove(int tableId, long objectId)
		{
			lock (this.lockObject)
			{
				int index = this.IndexOf(tableId, objectId);

				if (index >= 0)
				{
					this.RemoveAt(index);

					return true;
				}

				return false;
			}
		}

		internal bool RemoveInternal(int tableId, long objectId)
		{
			lock (this.lockObject)
			{
				int index = this.IndexOf(tableId, objectId);

				if (index >= 0)
				{
					this.collectionContainer.RemoveAt(index);

					return true;
				}

				return false;
			}
		}


		internal void RemoveAt(int index)
		{
			lock (this.lockObject)
			{
				this.OnBeforeRemove(index);
				this.collectionContainer.RemoveAt(index);
				this.OnAfterRemove(index);
			}
		}

		//public void BeginLoad() => this.update++;

		//public void EndLoad()
		//{
		//	this.update--;

		//	if (this.update == 0)
		//		this.lastLoaddedClientSortableObject = null;
		//}

		public SimpleObjectCollection<GroupMembership>? GetGroupMembershipCollection() => this.groupMembershipCollection;

		internal void SetGroupMembershipCollection(SimpleObjectCollection<GroupMembership> value) => this.groupMembershipCollection = value;

		internal void AddGroupMembership(long groupMembershipId) => this.groupMembershipCollection?.Add(groupMembershipId);
		internal void RemoveGroupMembership(long groupMembershipId) => this.groupMembershipCollection?.Remove(groupMembershipId);

		internal void Clear() => this.collectionContainer.Clear();

		public bool Contains(SimpleObject simpleObject) => this.Contains(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);
		public bool Contains(long objectId) => this.IndexOf(objectId) >= 0;
		public bool Contains(int tableId, long objectId) => this.IndexOf(tableId, objectId) >= 0;
		//public bool Contains(SimpleObject simpleObject) => this.Contains(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);

		public SimpleObjectCollection Copy() => this.CreateCopy();

		public bool IsSortable
		{
			get
			{
				Type? elementType = this.GetElementType();

				return elementType != null && elementType.IsSubclassOf(typeof(SortableSimpleObject));
				//this.Count > 0 && this.ElementAt(0).GetType().IsSubclassOf(typeof(SortableSimpleObject));
				//this.All(item => item.GetType().IsSubclassOf(typeof(SortableSimpleObject))))
			}
		}

		public void Sort(bool setPrevious = false, bool saveObjects = false)
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
					SortableSimpleObject? item = this.FirstOrDefault(element =>
																	{
																		if (element is SortableSimpleObject sortableSimpleObject)
																			return sortableSimpleObject.IsFirst;

																		return false;

																	}) as SortableSimpleObject;

					if (item == null)
					{
						this.SetOrderIndexesAsIs(setPrevious, saveObjects);
					}
					else // if (this.Count > 1)
					{
						int orderIndex = 0;
						SortableSimpleObject previous = item;
						List<long> newOrderedIds = new List<long>(this.Count);

						item.SetOrderIndexInternal(orderIndex++);
						newOrderedIds.Add(item.Id);

						do
						{
							item = this.FirstOrDefault(element =>
														{
															if (element is SortableSimpleObject sortableSimpleObject)
																return sortableSimpleObject.PreviousId == previous.Id;

															return false;

														}) as SortableSimpleObject; // Find Next item

							if (item != null)
							{
								item.SetOrderIndexInternal(orderIndex++);
								previous.NextId = item.Id;
								previous = item;
								newOrderedIds.Add(item.Id);
							}
						}
						while (item != null && orderIndex <= this.Count);

						previous.NextId = 0;

						if (orderIndex == this.Count)
						{
							this.collectionContainer.SetOrder(newOrderedIds);
						}
						else
						{
							this.SetOrderIndexesAsIs(setPrevious, saveObjects);
						}
					}

					//this.Sort<SortableSimpleObject>((s1, s2) => s1.OrderIndex.CompareTo(s2.OrderIndex));
				}
			}
		}

		public int[] GetTableIds() => this.collectionContainer.GetTableIds();
		public long[] GetObjectIds() => this.collectionContainer.GetObjectIds();

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.</returns>
		public IEnumerator<SimpleObject> GetEnumerator()
		{
			lock (this.lockObject)
			{
				for (int index = 0; index < this.Count; index++)
					yield return this[index];
				//return this.collectionContainer.GetEnumerator();
			}
		}

		protected virtual SimpleObjectCollection CreateCopy() => new SimpleObjectCollection(this.tableId, this.collectionContainer.CreateCopy());
		
		internal void SetOrderIndex(int newOrderIndex, int oldOrderIndex) //, ChangeContainer changeContainer, object? requester)
		{
			lock (this.lockObject)
			{
				int minPos;
				int maxPos;

				if (newOrderIndex == oldOrderIndex)
					return;

				if (oldOrderIndex < newOrderIndex)
				{
					minPos = oldOrderIndex;
					maxPos = newOrderIndex - 1;
				}
				else
				{
					minPos = newOrderIndex + 1;
					maxPos = oldOrderIndex;
				}

				SortableSimpleObject simpleObject = (SortableSimpleObject)this.ElementAt(oldOrderIndex);

				//this.BeginLoad();

				// Remove from current position
				if (simpleObject.PreviousId != 0)
					simpleObject.Previous.NextId = simpleObject.NextId;

				if (simpleObject.NextId != 0)
					simpleObject.Next.PreviousId = simpleObject.PreviousId;

				SortableSimpleObject simpleObjectAtNewPosition = (SortableSimpleObject)this.ElementAt(newOrderIndex);
				// Insert into new position specified by orderIndex
				if (newOrderIndex < oldOrderIndex)
				{
					// Move upper
					simpleObject.PreviousId = simpleObjectAtNewPosition.PreviousId;

					if (simpleObject.Previous != null)
						simpleObject.Previous.NextId = simpleObject.Id;

					simpleObject.NextId = simpleObjectAtNewPosition.Id;
					simpleObjectAtNewPosition.PreviousId = simpleObject.Id;
				}
				else
				{
					// Move under
					simpleObject.NextId = simpleObjectAtNewPosition.NextId;

					if (simpleObject.NextId != 0)
						simpleObject.Next.PreviousId = simpleObject.Id;

					simpleObject.PreviousId = simpleObjectAtNewPosition.Id;
					simpleObjectAtNewPosition.NextId = simpleObject.Id;
				}

				this.collectionContainer.RemoveAt(oldOrderIndex);
				this.collectionContainer.Insert(newOrderIndex, simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);
				//this.EndLoad();

				for (int i = minPos; i <= maxPos; i++)
					if (this[i] is SortableSimpleObject sortableSimpleObject)
						sortableSimpleObject.SetOrderIndexInternal(i);
			}
		}

		protected virtual void OnBeforeInsert(int index, int tableId, long objectId)
		{
		}

		protected virtual void OnAfterInsert(int index, int tableId, long objectId)
		{
			SortableSimpleObject? current = this.ElementAt(index) as SortableSimpleObject;

			if (current != null)
			{
				SortableSimpleObject? prev = (index > 0) ? this.ElementAt(index - 1) as SortableSimpleObject : null;
				SortableSimpleObject? next = (index < this.Count - 1) ? this.ElementAt(index + 1) as SortableSimpleObject : null;

				if (prev != null)
				{
					prev.NextId = current.Id;
					current.PreviousId = prev.Id;
				}

				if (next != null)
				{
					current.NextId = next.Id;
					next.PreviousId = current.Id;
				}

				if (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Client && this.lastLoaddedClientSortableObject != null) // && this.update > 0)
				{
					// Loading is in progress and the objects are sortable => set PreviousId, NextId and OrderIndex on the client side.
					// It is assumed that this is insertion is done by Add method and the elements is added at the end of the collection.
					this.lastLoaddedClientSortableObject.NextId = current.Id;
					current.PreviousId = this.lastLoaddedClientSortableObject.Id; // Loading is in progress -> no tracking changess
																					//current.previousId = this.lastLoaddedClientSortableObject.Id; // Loading is in progress -> no tracking changess
																					//current.oldPreviousId = current.previousId;
					this.lastLoaddedClientSortableObject = current;
				}
			}

			for (int i = index; i < this.Count; i++)
				(this[i] as SortableSimpleObject)?.SetOrderIndexInternal(i);

			//current.SetOrderIndexInternal(index);
		}

		protected virtual void OnBeforeInsert2(int index, int tableId, long objectId) 
		{
			if (this.Contains(tableId, objectId))
				throw new DuplicateObjectException("SimpleObject already added to the collection.");

			SortableSimpleObject? current = this.ObjectManager.GetObject(tableId, objectId) as SortableSimpleObject;

			if (current != null) // && this.update > 0)
			{
				// When inserting the new object in the collection, setup PreviosId and NextId of the affected SortableSimpleObject elements in normal working mode (loadind data is not in progress).

				if (this.Count == 0)
				{
					current.PreviousId = 0; // No need for this if not prev and next is not compromised
					current.NextId = 0;
				}
				else if (index == 0) // Insert at the top
				{
					SortableSimpleObject next = (SortableSimpleObject)this.ElementAt(0);

					current.PreviousId = 0;
					current.NextId = next.Id;
					next.PreviousId = current.Id;
				}
				else if (index == this.Count) // Insert at the bottom
				{
					SortableSimpleObject previous = (SortableSimpleObject)this.ElementAt(index - 1);

					previous.NextId = current.Id;
					current.PreviousId = previous.Id;
					current.NextId = 0;
				}
				else // Insert in a middle
				{
					SortableSimpleObject previous = (SortableSimpleObject)this.ElementAt(index - 1);
					SortableSimpleObject next = (SortableSimpleObject)this.ElementAt(index);

					previous.NextId = current.Id;
					current.PreviousId = previous.Id;
					current.NextId = next.Id;
					next.PreviousId = current.Id;
				}

				current.SetOrderIndexInternal(index);

				for (int i = index; i < this.Count; i++)
					((SortableSimpleObject)(this[i])).SetOrderIndexInternal(i + 1);
			}
		}

		protected virtual void OnBeforeInsert3(int index, int tableId, long objectId)
		{
			if (this.Contains(tableId, objectId))
				throw new DuplicateObjectException("SimpleObject already added to the collection.");

			SortableSimpleObject? current = this.ObjectManager.GetObject(tableId, objectId) as SortableSimpleObject;

			if (current != null) // && this.update > 0)
			{
				// When inserting the new object in the collection, setup PreviosId and NextId of the affected SortableSimpleObject elements in normal working mode (loadind data is not in progress).

				if (this.Count == 0)
				{
					current.PreviousId = 0; // No need for this if not prev and next is not compromised
					current.NextId = 0;
				}
				else if (index == 0) // Insert at the top
				{
					SortableSimpleObject next = (SortableSimpleObject)this.ElementAt(0);

					current.PreviousId = 0;
					current.NextId = next.Id;
					next.PreviousId = current.Id;
				}
				else if (index == this.Count) // Insert at the bottom
				{
					SortableSimpleObject previous = (SortableSimpleObject)this.ElementAt(index - 1);

					previous.NextId = current.Id;
					current.PreviousId = previous.Id;
					current.NextId = 0;
				}
				else // Insert in a middle
				{
					SortableSimpleObject previous = (SortableSimpleObject)this.ElementAt(index - 1);
					SortableSimpleObject next = (SortableSimpleObject)this.ElementAt(index);

					previous.NextId = current.Id;
					current.PreviousId = previous.Id;
					current.NextId = next.Id;
					next.PreviousId = current.Id;
				}

				current.SetOrderIndexInternal(index);

				for (int i = index; i <= this.Count - 1; i++)
					((SortableSimpleObject)this[i]).SetOrderIndexInternal(i + 1);
			}
		}



		//protected virtual void OnAfterInsert(int index, int tableId, long objectId) 
		//{
		//	if (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Client && this.lastLoaddedClientSortableObject != null) // && this.update > 0)
		//	{
		//		// Loading is in progress and the objects are sortable => set PreviousId, NextId and OrderIndex on the client side.
		//		// It is assumed that this is insertion is done by Add method and the elements is added at the end of the collection.

		//		SortableSimpleObject current = this.ObjectManager.GetObject(tableId, objectId) as SortableSimpleObject;

		//		if (current != null)
		//		{
		//			this.lastLoaddedClientSortableObject.NextId = current.Id;
		//			current.PreviousId = this.lastLoaddedClientSortableObject.Id; // Loading is in progress -> no tracking changess
		//																		  //current.previousId = this.lastLoaddedClientSortableObject.Id; // Loading is in progress -> no tracking changess
		//																		  //current.oldPreviousId = current.previousId;
		//			this.lastLoaddedClientSortableObject = current;
		//		}
		//	}

		//	//current.SetOrderIndexInternal(index);
		//}

		protected virtual void OnBeforeRemove(int index) 
		{
			if (this.ElementAt(index) is SortableSimpleObject current)  // && update == 0)
			{
				current.PreviousId = 0;
				current.NextId = 0;
				current.SetOrderIndexInternal(0);

				SortableSimpleObject? prev = (index > 0) ? this.ElementAt(index - 1) as SortableSimpleObject : null;
				SortableSimpleObject? next = (index < this.Count - 1) ? this.ElementAt(index + 1) as SortableSimpleObject : null;

				if (prev != null)
					prev.NextId = (next != null) ? next.Id : 0;

				if (next != null)
					next.PreviousId = (prev != null) ? prev.Id : 0;
			}
		}

		protected virtual void OnAfterRemove(int index)
		{
			if (this.Count > 0)
			{
				for (int i = index; i < this.Count; i++)
					(this[i] as SortableSimpleObject)?.SetOrderIndexInternal(i);
			}
			else //if (this.Count == 0)
			{
				this.lastLoaddedClientSortableObject = null;
			}
		}

		protected virtual void OnAfterRemove2(int index) 
		{
			if (this.Count > 0)
			{
				SortableSimpleObject? current = this.ElementAt(index) as SortableSimpleObject;

				if (current != null) // && update == 0)
				{
					//current.ChangeContainer = this.ObjectManager.DefaultChangeContainer;

					if (this.Count == 1) // only one left 
					{
						current.PreviousId = 0;
						current.NextId = 0;
					}
					else if (index == 0) // first is removed
					{
						current.PreviousId = 0;
					}
					else if (index == this.Count) // last is removed
					{
						//(this.ElementAt(index - 1) as SortableSimpleObject).NextId = 0;
						//current.PreviousId = 0;
						current.NextId = 0;
					}
					else // if (index < this.Count - 1) // Removing in the middle
					{
						SortableSimpleObject previous = (SortableSimpleObject)this.ElementAt(index - 1);
						SortableSimpleObject next = (SortableSimpleObject)this.ElementAt(index + 1);

						previous.NextId = next.Id;
						next.PreviousId = previous.Id;

						current.PreviousId = 0;
						current.NextId = 0;
					}

					for (int i = index; i < this.Count; i++)
						(this[i] as SortableSimpleObject)?.SetOrderIndexInternal(i);
				}
			}
			else if (this.Count == 0)
			{
				this.lastLoaddedClientSortableObject = null;
			}
		}

		protected virtual void OnClear() => this.lastLoaddedClientSortableObject = null;

		private void SetOrderIndexesAsIs(bool setPrevious, bool saveObjects)
		{
			SortableSimpleObject? previous = null;

			for (int i = 0; i < this.Count; i++)
			{
				SortableSimpleObject item = (SortableSimpleObject)this[i];

				if (setPrevious)
				{
					item.ChangeContainer = this.ObjectManager.DefaultChangeContainer;
					item.PreviousId = (previous != null) ? previous.Id : 0;
				}

				item.NextId = 0;
				item.SetOrderIndexInternal(i);

				if (previous != null)
					previous.NextId = item.Id;

				previous = item;
			}

			if (saveObjects && this.ObjectManager.DefaultChangeContainer.RequireCommit)
				this.ObjectManager.CommitChanges();
		}

		///// <summary>
		///// Sort a generic collection in a place. 
		///// </summary>
		///// <typeparam name="T">The type of elements in the list.</typeparam>
		///// <param name="list">The list to sort.</param>
		///// <param name="comparison">The comparison method.</param>
		//private void Sort<T>(Comparison<T> comparison) => ArrayList.Adapter(this.collectionContainer as IList).Sort(new ComparisonComparer<T>(comparison));

		private SimpleObject? FirstOrDefault(Func<SimpleObject, bool> predicate)
		{
			for (int i = 0; i < this.Count; i++)
			{
				SimpleObject element = this[i];

				if (predicate(element))
					return element;
			}

			return null;
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
			//for (int index = 0; index < this.Count; index++)
			//	yield return this[index];
		}

		public void Dispose()
		{
			this.groupMembershipCollection = null;
			this.lastLoaddedClientSortableObject = null;
			this.collectionContainer.Dispose();
		}
	}
}
