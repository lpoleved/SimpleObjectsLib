using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple;
using static System.Net.Mime.MediaTypeNames;

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

		//public new SimpleObjectCollection<T> Sort(bool setPrevious = false, bool saveObjects = false) => (base.Sort(setPrevious, saveObjects) as SimpleObjectCollection<T>)!;

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
		bool isSorted = false;
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
			//this.Sort(); // if sortable

			//this.objectIds = objectIds;
		}

		public SimpleObjectCollection(SimpleObjectManager objectManager, IList<int> tableIds, IList<long> objectIds)
		{
			this.collectionContainer = new SimpleMultyTypeObjectCollectionContainer(objectManager, tableIds, objectIds);
			//this.tableIds = tableIds;
			//this.objectIds = objectIds;
			//this.Sort(); // if sortable
		}

		internal SimpleObjectCollection(int tableId, SimpleObjectCollectionContaier collectionContainer)
		{
			this.tableId = tableId;
			this.collectionContainer = collectionContainer;
			//this.Sort(); // if sortable
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

		internal bool Add(SimpleObject simpleObject, ChangeContainer? changeContainer, object? requester) => this.Add(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id, changeContainer, requester);
		//internal void AddInternal(SimpleObject simpleObject) => this.collectionContainer.Insert(this.Count, simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);
		internal bool Add(long objectId, ChangeContainer? changeContainer, object? requester) => this.Add(this.tableId, objectId, changeContainer, requester);
		internal bool Add(int tableId, long objectId, ChangeContainer? changeContainer, object? requester) => this.Insert(this.Count, tableId, objectId, changeContainer, requester);

		internal bool Insert(SimpleObject simpleObject, ChangeContainer? changeContainer, object? requester) => this.Insert(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id, changeContainer, requester);
		internal bool Insert(int index, long objectId, ChangeContainer? changeContainer, object? requester) => this.Insert(index, this.tableId, objectId, changeContainer, requester);
		internal bool Insert(int index, int tableId, long objectId, ChangeContainer? changeContainer, object? requester)
		{
			lock (this.lockObject)
			{
				if (this.Contains(tableId, objectId))
					return false; // throw new DuplicateObjectException("SimpleObject already added to the collection");

				this.OnBeforeInsert(index, tableId, objectId, changeContainer, requester);
				this.collectionContainer.Insert(index, tableId, objectId);
				this.OnAfterInsert(index, tableId, objectId, changeContainer, requester);

				return true;
			}
		}

		internal bool Remove(SimpleObject simpleObject, ChangeContainer? changeContainer, object? requester) => this.Remove(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id, changeContainer, requester);
		//internal bool RemoveInternal(SimpleObject simpleObject) => this.RemoveInternal(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);
		internal bool Remove(long objectId, ChangeContainer? changeContainer, object? requester) => this.Remove(this.tableId, objectId, changeContainer, requester);
		internal bool Remove(int tableId, long objectId, ChangeContainer? changeContainer, object? requester)
		{
			lock (this.lockObject)
			{
				int index = this.IndexOf(tableId, objectId);

				if (index >= 0)
				{
					this.RemoveAt(index, changeContainer, requester);

					return true;
				}

				return false;
			}
		}

		//internal bool RemoveInternal(int tableId, long objectId)
		//{
		//	lock (this.lockObject)
		//	{
		//		int index = this.IndexOf(tableId, objectId);

		//		if (index >= 0)

		//		{
		//			this.collectionContainer.RemoveAt(index);

		//			return true;
		//		}

		//		return false;
		//	}
		//}


		internal void RemoveAt(int index, ChangeContainer? changeContainer, object? requester)
		{
			lock (this.lockObject)
			{
				this.OnBeforeRemove(index, changeContainer, requester);
				this.collectionContainer.RemoveAt(index);
				this.OnAfterRemove(index, changeContainer, requester);
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

		internal void AddGroupMembership(long groupMembershipId, ChangeContainer? changeContainer, object? requester) => this.groupMembershipCollection?.Add(groupMembershipId, changeContainer, requester);
		internal void RemoveGroupMembership(long groupMembershipId, ChangeContainer? changeContainer, object? requester) => this.groupMembershipCollection?.Remove(groupMembershipId, changeContainer, requester);

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

		public void Sort(bool setPrevious = false, object? requester = null)
		{
			this.Sort(setPrevious, this.ObjectManager.DefaultChangeContainer, requester);
		}

		public void Sort(bool setPrevious, ChangeContainer? changeContainer, object? requester = null) //, bool saveObjects = false)
		{
			if (!this.isSorted && this.Count > 0) // && this.All(item => item.GetType().IsSubclassOf(typeof(SortableSimpleObject))))
			{
				if (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Client)
				{
					//this.Sort((s1, s2) => (s1 as SortableSimpleObject).OrderIndex.CompareTo((s2 as SortableSimpleObject).OrderIndex));
					this.SetOrderIndexesAsIs(setPrevious: true, changeContainer: null, requester);
				}
				else // if (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Server || this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.ClientWithLocalDatastore)
				{
					SortableSimpleObject? previous = this.ElementAt(0) as SortableSimpleObject;

					if (previous is null)
						return; // this is not sortable collection

					SortableSimpleObject? item = null;
					//this.FirstOrDefault(element =>
					//											{
					//												if (element is SortableSimpleObject sortableSimpleObject)
					//													return sortableSimpleObject.IsFirst;

					//												return false;

					//											}) as SortableSimpleObject;

					foreach (var element in this)
					{
						if (element is SortableSimpleObject sortableSimpleObject && sortableSimpleObject.PreviousId == 0)
						{
							item = sortableSimpleObject;

							break;
						}
					}
						

					if (item == null)
					{
						this.SetOrderIndexesAsIs(setPrevious, changeContainer, requester);
					}
					else // if (this.Count > 1)
					{
						int orderIndex = 0;
						List<long> newOrderedIds = new List<long>(this.Count);
						
						item.SetOrderIndexInternal(orderIndex++);
						newOrderedIds.Add(item!.Id);
						previous = item;

						do
						{
							item = null;

							foreach (var element in this)
							{
								if (element is SortableSimpleObject sortableSimpleObject && sortableSimpleObject.PreviousId == previous.Id)
								{
									item = sortableSimpleObject;

									break;
								}
							}

							//item = this.FirstOrDefault(element =>
							//						  {
							//							   if (element is SortableSimpleObject sortableSimpleObject)
							//								   return sortableSimpleObject.PreviousId == previous.Id;

							//							   return false;

							//						  }) as SortableSimpleObject; // Find Next item

							if (item != null)
							{
								//item.SetPropertyValueInternal(item.GetModel().PreviousIdPropertyModel, previous.Id, changeContainer, requester);
								previous.NextId = item.Id;
								item.SetOrderIndexInternal(orderIndex++);
								previous = item;
								newOrderedIds.Add(item.Id);
							}
						}
						while (item != null && orderIndex <= this.Count);

						previous.NextId = 0;

						if (orderIndex == this.Count)
							this.collectionContainer.SetOrder(newOrderedIds);
						else
							this.SetOrderIndexesAsIs(setPrevious, changeContainer, requester);
					}

					//this.Sort<SortableSimpleObject>((s1, s2) => s1.OrderIndex.CompareTo(s2.OrderIndex));
				}
			}

			this.isSorted = true;

			//return this;
		}

		public void Sort(IList<long> newOrderedIds)
		{
			this.Sort(newOrderedIds, this.ObjectManager.DefaultChangeContainer);
		}

		public void Sort(IList<long> newOrderedIds, ChangeContainer? changeContainer, object? requester = null)
		{
			this.collectionContainer.SetOrder(newOrderedIds);
			this.SetOrderIndexesAsIs(setPrevious: true, changeContainer, requester);
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
		
		internal void SetOrderIndex(int newOrderIndex, int oldOrderIndex, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
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

				// Remove from current position
				var previous = simpleObject.Previous;

				if (previous != null)
					previous.NextId = simpleObject.NextId;

				var next = simpleObject.Next;

				if (next != null)
					next.SetPropertyValueInternal(next.GetModel().PreviousIdPropertyModel, simpleObject.PreviousId, changeContainer, context, requester);

				SortableSimpleObject simpleObjectAtNewPosition = (SortableSimpleObject)this.ElementAt(newOrderIndex);
				
				// Insert into new position specified by newOrderIndex
				if (newOrderIndex < oldOrderIndex)
				{
					// Move above
					simpleObject.SetPropertyValueInternal(simpleObject.GetModel().PreviousIdPropertyModel, simpleObjectAtNewPosition.PreviousId, changeContainer, context, requester);

					previous = simpleObject.Previous;

					if (previous != null)
						previous.NextId = simpleObject.Id;

					simpleObject.NextId = simpleObjectAtNewPosition.Id;
					simpleObjectAtNewPosition.SetPropertyValueInternal(simpleObjectAtNewPosition.GetModel().PreviousIdPropertyModel, simpleObject.Id, changeContainer, context, requester);
				}
				else
				{
					// Move under
					simpleObject.NextId = simpleObjectAtNewPosition.NextId;
					next = simpleObject.Next;

					if (next != null)
						next.SetPropertyValueInternal(next.GetModel().PreviousIdPropertyModel, simpleObject.Id, changeContainer, context, requester);

					simpleObject.SetPropertyValueInternal(simpleObject.GetModel().PreviousIdPropertyModel, simpleObjectAtNewPosition.Id, changeContainer, context, requester);
					simpleObjectAtNewPosition.NextId = simpleObject.Id;
				}

				this.collectionContainer.RemoveAt(oldOrderIndex);
				this.collectionContainer.Insert(newOrderIndex, simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);

				for (int i = minPos; i <= maxPos; i++)
					if (this[i] is SortableSimpleObject sortableSimpleObject)
						sortableSimpleObject.SetOrderIndexInternal(i);
			}
		}

		//internal void SetOrderIndexOld(int newOrderIndex, int oldOrderIndex, ChangeContainer changeContainer, object? requester)
		//{
		//	lock (this.lockObject)
		//	{
		//		int minPos;
		//		int maxPos;

		//		if (newOrderIndex == oldOrderIndex)
		//			return;

		//		if (oldOrderIndex < newOrderIndex)
		//		{
		//			minPos = oldOrderIndex;
		//			maxPos = newOrderIndex - 1;
		//		}
		//		else
		//		{
		//			minPos = newOrderIndex + 1;
		//			maxPos = oldOrderIndex;
		//		}

		//		SortableSimpleObject simpleObject = (SortableSimpleObject)this.ElementAt(oldOrderIndex);

		//		//this.BeginLoad();

		//		// Remove from current position
		//		if (simpleObject.PreviousId != 0)
		//			simpleObject.Previous!.NextId = simpleObject.NextId;

		//		if (simpleObject.NextId != 0)
		//			simpleObject.Next!.PreviousId = simpleObject.PreviousId;

		//		SortableSimpleObject simpleObjectAtNewPosition = (SortableSimpleObject)this.ElementAt(newOrderIndex);
				
		//		// Insert into new position specified by orderIndex
		//		if (newOrderIndex < oldOrderIndex)
		//		{
		//			// Move upper
		//			simpleObject.PreviousId = simpleObjectAtNewPosition.PreviousId;

		//			if (simpleObject.Previous != null)
		//				simpleObject.Previous.NextId = simpleObject.Id;

		//			simpleObject.NextId = simpleObjectAtNewPosition.Id;
		//			simpleObjectAtNewPosition.PreviousId = simpleObject.Id;
		//		}
		//		else
		//		{
		//			// Move under
		//			simpleObject.NextId = simpleObjectAtNewPosition.NextId;

		//			if (simpleObject.NextId != 0)
		//				simpleObject.Next!.PreviousId = simpleObject.Id;

		//			simpleObject.PreviousId = simpleObjectAtNewPosition.Id;
		//			simpleObjectAtNewPosition.NextId = simpleObject.Id;
		//		}

		//		this.collectionContainer.RemoveAt(oldOrderIndex);
		//		this.collectionContainer.Insert(newOrderIndex, simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);
		//		//this.EndLoad();

		//		for (int i = minPos; i <= maxPos; i++)
		//			if (this[i] is SortableSimpleObject sortableSimpleObject)
		//				sortableSimpleObject.SetOrderIndexInternal(i);
		//	}
		//}


		//internal void SetOrderIndex(int newOrderIndex, int oldOrderIndex, ChangeContainer changeContainer, object? requester)
		//{
		//	lock (this.lockObject)
		//	{
		//		int minPos;
		//		int maxPos;

		//		if (newOrderIndex == oldOrderIndex)
		//			return;

		//		if (oldOrderIndex < newOrderIndex)
		//		{
		//			minPos = oldOrderIndex;
		//			maxPos = newOrderIndex - 1;
		//		}
		//		else
		//		{
		//			minPos = newOrderIndex + 1;
		//			maxPos = oldOrderIndex;
		//		}

		//		var old = (SortableSimpleObject)this.ElementAt(oldOrderIndex);
		//		long oldPrevId = old.PreviousId;
		//		long oldNextId = old.NextId;

		//		var @new = (SortableSimpleObject)this.ElementAt(newOrderIndex);
		//		long newPrevId = @new.PreviousId;
		//		long newNextId = @new.NextId;

		//		if (oldPrevId != 0)
		//			old.Previous!.NextId = old.NextId;

		//		if (oldNextId != 0)
		//			old.Next!.PreviousId = oldPrevId;


		//		old.PreviousId = newPrevId;
		//		old.NextId = newNextId;

		//		if (newNextId != 0)
		//			old.Next!.PreviousId = old.Id;

		//		if (newPrevId != 0)
		//			@new.Previous!.NextId = old.Id;

		//		@new.PreviousId = oldPrevId;
		//		@new.NextId = oldNextId;

		//		if (oldNextId != 0)
		//			@new.Next!.PreviousId = @new.Id;

		//		this.collectionContainer.RemoveAt(oldOrderIndex);
		//		this.collectionContainer.Insert(newOrderIndex, old.GetModel().TableInfo.TableId, old.Id);

		//		for (int i = minPos; i <= maxPos; i++)
		//			if (this[i] is SortableSimpleObject sortableSimpleObject)
		//				sortableSimpleObject.SetOrderIndexInternal(i);
		//	}
		//}



		protected virtual void OnBeforeInsert(int index, int tableId, long objectId, ChangeContainer? changeContainer, object? requester)
		{
		}

		protected virtual void OnAfterInsert(int index, int tableId, long objectId, ChangeContainer? changeContainer, object? requester)
		{
			if (this.ElementAt(index) is SortableSimpleObject current)
			{
				SortableSimpleObject? prev = (index > 0) ? this.ElementAt(index - 1) as SortableSimpleObject : null;
				SortableSimpleObject? next = (index < this.Count - 1) ? this.ElementAt(index + 1) as SortableSimpleObject : null;

				if (prev != null)
				{
					prev.NextId = current.Id;
					current.SetPropertyValueInternal(current.GetModel().PreviousIdPropertyModel, prev.Id, changeContainer, requester);
				}

				if (next != null)
				{
					current.NextId = next.Id;
					next.SetPropertyValueInternal(current.GetModel().PreviousIdPropertyModel, current.Id, changeContainer, requester);
				}

				if (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Client && this.lastLoaddedClientSortableObject != null) // && this.update > 0)
				{
					// Loading is in progress and the objects are sortable => set PreviousId, NextId and OrderIndex on the client side.
					// It is assumed that this is insertion is done by Add method and the elements is added at the end of the collection.
					this.lastLoaddedClientSortableObject.NextId = current.Id;
					current.SetPropertyValueInternal(current.GetModel().PreviousIdPropertyModel, this.lastLoaddedClientSortableObject.Id, changeContainer, requester); // Loading is in progress -> no tracking changess

					//current.previousId = this.lastLoaddedClientSortableObject.Id; // Loading is in progress -> no tracking changess
					//current.oldPreviousId = current.previousId;
					this.lastLoaddedClientSortableObject = current;
				}
			}

			for (int i = index; i < this.Count; i++)
				if (this[i] is SortableSimpleObject sortableSimpleObject && sortableSimpleObject.GetSortingCollectionInternal() == this)
					sortableSimpleObject.SetOrderIndexInternal(i);

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
					if (this[i] is SortableSimpleObject sortableSimpleObject && sortableSimpleObject.GetSortingCollectionInternal() == this)
						sortableSimpleObject.SetOrderIndexInternal(i + 1);
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
					if (this[i] is SortableSimpleObject sortableSimpleObject && sortableSimpleObject.GetSortingCollectionInternal() == this)
						sortableSimpleObject.SetOrderIndexInternal(i + 1);
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

		protected virtual void OnBeforeRemove(int index, ChangeContainer? changeContainer, object? requester) 
		{
			if (this.ElementAt(index) is SortableSimpleObject current)  // && update == 0)
			{
				current.SetPropertyValueInternal(current.GetModel().PreviousIdPropertyModel, (long)0, changeContainer, requester);
				current.NextId = 0;
				current.SetOrderIndexInternal(0);

				SortableSimpleObject? prev = (index > 0) ? this.ElementAt(index - 1) as SortableSimpleObject : null;
				SortableSimpleObject? next = (index < this.Count - 1) ? this.ElementAt(index + 1) as SortableSimpleObject : null;

				if (prev != null)
					prev.NextId = (next != null) ? next.Id : 0;

				if (next != null)
					next.SetPropertyValueInternal(next.GetModel().PreviousIdPropertyModel, (prev != null) ? prev.Id : 0, changeContainer, requester);
			}
		}

		protected virtual void OnAfterRemove(int index, ChangeContainer? changeContainer, object? requester)
		{
			if (this.Count > 0)
			{
				for (int i = index; i < this.Count; i++)
					if (this[i] is SortableSimpleObject sortableSimpleObject && sortableSimpleObject.GetSortingCollectionInternal() == this)
						sortableSimpleObject.SetOrderIndexInternal(i);
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
						if (this[i] is SortableSimpleObject sortableSimpleObject && sortableSimpleObject.GetSortingCollectionInternal() == this)
							sortableSimpleObject.SetOrderIndexInternal(i);
				}
			}
			else if (this.Count == 0)
			{
				this.lastLoaddedClientSortableObject = null;
			}
		}

		protected virtual void OnClear() => this.lastLoaddedClientSortableObject = null;

		private void SetOrderIndexesAsIs(bool setPrevious, ChangeContainer? changeContainer, object ? requester) //, bool saveObjects)
		{
			SortableSimpleObject? previous = null;
			ChangeContainer? cc = (setPrevious) ? changeContainer : null;

			for (int i = 0; i < this.Count; i++)
			{
				SortableSimpleObject? item = this[i] as SortableSimpleObject;

				if (item == null)
					return;

				//if (setPrevious)
					item.SetPropertyValueInternal(item.GetModel().PreviousIdPropertyModel, previous?.Id ?? 0, changeContainer: cc, requester);

				item.NextId = 0;
				item.SetOrderIndexInternal(i);

				if (previous != null)
					previous.NextId = item.Id;

				previous = item;
			}

			// CommitChanges is not good at this point since it could be dead lock since CommitChanges method takes all related objects for saveing and delete including related collectoions
			//if (saveObjects && this.ObjectManager.DefaultChangeContainer.RequireCommit) // save
			//	this.ObjectManager.CommitChanges();
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
