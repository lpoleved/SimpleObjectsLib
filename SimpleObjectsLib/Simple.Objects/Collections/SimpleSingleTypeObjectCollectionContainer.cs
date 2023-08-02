using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects
{
	internal class SimpleSingleTypeObjectCollectionContainer : SimpleObjectCollectionContaier
	{
		private int tableId = 0;
		private ObjectCache objectCache;
		protected IList<long> objectIds;

		public SimpleSingleTypeObjectCollectionContainer(SimpleObjectManager objectManager, int tableId, IList<long> objectIds)
			: base(objectManager)
		{
			this.tableId = tableId;
			this.objectIds = objectIds;
			this.objectCache = objectManager.GetObjectCache(tableId)!;
		}

		public override SimpleObject ElementAt(int index) => this.objectCache.GetObject(this.objectIds[index])!;

		//public override void Add(int tableId, long objectId) { this.CheckTableId(tableId); this.objectIds.Add(objectId); }

		public override void Insert(int index, int tableId, long objectId)
		{
			this.CheckTableId(tableId);
			this.objectIds.Insert(index, objectId);
		}

		public override void RemoveAt(int index) => this.objectIds.RemoveAt(index);
		public override void Clear() => this.objectIds.Clear();

		public override int Count => this.objectIds.Count;

		public override int IndexOf(int tableId, long objectId) => (tableId != this.tableId) ? -1 : this.objectIds.IndexOf(objectId);

		//public override SimpleObjectCollection<T> AsCustom<T>() => new SimpleObjectCollection<T>(this.tableId, this);
		public override SimpleObjectCollectionContaier CreateCopy() => new SimpleSingleTypeObjectCollectionContainer(this.ObjectManager, this.tableId, this.objectIds.ToArray());

		public override int[] GetTableIds()
		{
			int[] tableIds = new int[this.Count];

			Array.Fill(tableIds, this.tableId);

			return tableIds;
		}

		public override long[] GetObjectIds() => this.objectIds.ToArray();

		internal override void SetOrder(IList<long> newOrderedIds) => this.objectIds = newOrderedIds;

		protected override void ReplaceId(int index, long newId) => this.objectIds[index] = newId;


		private void CheckTableId(int tableId)
		{
			if (tableId != this.tableId)
				throw new ArgumentException("SimpleSingleTypeObjectCollectionContainer: tableId's do not match");
		}
	}
}
