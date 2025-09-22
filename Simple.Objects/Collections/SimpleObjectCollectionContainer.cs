using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects
{
	internal abstract class SimpleObjectCollectionContaier : IEnumerable<SimpleObject>, IEnumerable, IDisposable //, IList
	{
		public SimpleObjectCollectionContaier(SimpleObjectManager objectManager)
		{
			this.ObjectManager = objectManager;

            //if (this.ObjectManager != null)
				this.ObjectManager.ObjectIdChange += ObjectManager_ObjectIdChange;
		}

		private void ObjectManager_ObjectIdChange(object sender, ChangeObjectIdSimpleObject e)
		{
			if (e.SimpleObject is null)
				return;
			
			int tableId = e.SimpleObject.GetModel().TableInfo.TableId;
            int index = this.IndexOf(tableId, e.OldTempId);

            if (index >= 0)
                this.ReplaceId(index, e.NewId);
		}

		public SimpleObjectManager ObjectManager { get; private set; }

		public abstract SimpleObject ElementAt(int index);
		public abstract int Count { get; }
		//public abstract void Add(int tableId, long objectId);
		public abstract void Insert(int index, int tableId, long objectId);
		public abstract void RemoveAt(int index);

        public abstract int IndexOf(int tableId, long objectId);

        public abstract void Clear();

		public abstract int[] GetTableIds();
		public abstract long[] GetObjectIds();

        internal abstract void SetOrder(IList<long> newOrderedIds);
		

        protected abstract void ReplaceId(int index, long newId);

		//public abstract SimpleObjectCollection<T> AsCustom<T>() where T : SimpleObject;
		public abstract SimpleObjectCollectionContaier CreateCopy();

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>A <see cref="IEnumerator{T}"/></see> that can be used to iterate through the collection.</returns>
		public IEnumerator<SimpleObject> GetEnumerator()
		{
			for (int index = 0; index < this.Count; index++)
				yield return this.ElementAt(index);
		}

		

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>An <see cref="IEnumerator"></see> object that can be used to iterate through the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		public void Dispose()
		{
		}

		~SimpleObjectCollectionContaier()
		{
			if (this.ObjectManager != null)
				this.ObjectManager.ObjectIdChange -= ObjectManager_ObjectIdChange;
		}
	}
}
