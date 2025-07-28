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
        private List<TSimpleObject>? genericList = null;

        public GenericObjectCollectionBase(TObjectManager objectManager)
            : this(objectManager, ObjectActionContext.Unspecified)
        {
            this.ObjectManager = objectManager;
        }

		protected GenericObjectCollectionBase(TObjectManager objectManager, ObjectActionContext context)
		{
			this.ObjectManager = objectManager;
            this.Context = context;

            if (context == ObjectActionContext.Unspecified)
                this.Context = (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Server) ? ObjectActionContext.ServerTransaction : ObjectActionContext.Client;
		}

		public TObjectManager ObjectManager { get; private set; }

        public ObjectActionContext Context { get; set; }

        public SimpleObjectCollection<TSimpleObject>? Collection => (this.ObjectManager.GetObjectCache<TSimpleObject>())?.GetObjectCollection<TSimpleObject>();

        public List<TSimpleObject> GenericList => this.genericList ??= this.CreateGenericList();

        protected abstract List<TSimpleObject> CreateGenericList();
    }
}
