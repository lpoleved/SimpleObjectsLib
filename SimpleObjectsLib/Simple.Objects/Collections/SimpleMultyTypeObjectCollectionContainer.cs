using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects
{
	internal class SimpleMultyTypeObjectCollectionContainer : SimpleObjectCollectionContaier
	{
		protected IList<int> tableIds;
		protected IList<long> objectIds;

		public SimpleMultyTypeObjectCollectionContainer(SimpleObjectManager objectManager, IList<int> tableIds, IList<long> objectIds)
			: base(objectManager)
		{
			this.tableIds = tableIds;
			this.objectIds = objectIds;
		}

		public override SimpleObject ElementAt(int index) => this.ObjectManager.GetObject(this.tableIds[index], this.objectIds[index])!;
		//public override void Add(int tableId, long objectId) { this.tableIds.Add(tableId); this.objectIds.Add(objectId); }
		public override void Insert(int index, int tableId, long objectId) { this.tableIds.Insert(index, tableId); this.objectIds.Insert(index, objectId); }
		public override void RemoveAt(int index) { this.tableIds.RemoveAt(index); this.objectIds.RemoveAt(index); }
		public override void Clear() { this.tableIds.Clear(); this.objectIds.Clear(); }

		public override int Count => this.objectIds.Count;

		public override int IndexOf(int tableId, long objectId)
		{
			for (int i = 0; i < this.Count; i++)
				if (objectIds[i] == objectId && tableIds[i] == tableId)
					return i;

			return -1;
		}

		//public override SimpleObjectCollection<T> AsCustom<T>() => new SimpleObjectCollection<T>(tableId: 0, this);
		public override SimpleObjectCollectionContaier CreateCopy() => new SimpleMultyTypeObjectCollectionContainer(this.ObjectManager, this.tableIds, this.objectIds.ToArray());

		public override int[] GetTableIds() => this.tableIds.ToArray();

		public override long[] GetObjectIds() => this.objectIds.ToArray();

		internal override void SetOrder(IList<long> newOrderedIds) => throw new NotSupportedException("The Multy Type Object collection cannot be ordered with only objectId's");

		//internal override void SetOrder(List<SortableSimpleObject> newOrderedList)
		//{
		//	List<int> newTableIds = new List<int>(newOrderedList.Count);
		//	List<long> newObjectIds = new List<long>(newOrderedList.Count);
			
		//	foreach (SortableSimpleObject sortableSimpleObject in newOrderedList)
		//	{
		//		newTableIds.Add(sortableSimpleObject.GetModel().TableInfo.TableId);
		//		newObjectIds.Add(sortableSimpleObject.Id);
		//	}

		//	this.tableIds = newTableIds;
		//	this.objectIds = newObjectIds;
		//}

		protected override void ReplaceId(int index, long newId) => this.objectIds[index] = newId;
	}
}
