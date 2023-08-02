using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects
{
    public abstract class GenericObjectCollectionBase<TObjectManager, TSimpleObject>
        where TObjectManager : SimpleObjectManager
        where TSimpleObject : SimpleObject
    {
        private List<TSimpleObject> genericList = null;

        public GenericObjectCollectionBase(TObjectManager objectManager)
        {
            this.ObjectManager = objectManager;
        }

        public TObjectManager ObjectManager { get; private set; }

        public SimpleObjectCollection<TSimpleObject>? Collection
        {
            get { return (this.ObjectManager.GetObjectCache<TSimpleObject>() as ServerObjectCache)?.SelectAll<TSimpleObject>(); }
        }

        public List<TSimpleObject> GenericList
        {
            get
            {
                if (this.genericList == null)
                    this.genericList = this.CreateGenericList();

                return this.genericList;
            }
        }

        protected abstract List<TSimpleObject> CreateGenericList();
    }
}
