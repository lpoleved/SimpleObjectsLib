//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Simple.Objects
//{
//    public class ObjectCollectionBase<TObjectManager, TSimpleObject> : SimpleObjectCollection<TSimpleObject>
//        where TObjectManager : SimpleObjectManager
//        where TSimpleObject : SimpleObject 
//    {
//        private TObjectManager objectManager = null;
//        private SimpleObjectCollection<TSimpleObject> collection = null;

//        public ObjectCollectionBase(TObjectManager objectManager)
//            : base(objectManager)
//        {
//            this.objectManager = objectManager;
//        }

//        public SimpleObjectCollection<TSimpleObject> Collection
//        { 
//            get
//            {
//                if (this.collection == null)
//                    this.collection = this.objectManager.ObjectCache.GetObjectCollection<TSimpleObject>();

//                return this.collection;
//            }
//        }
//    }
//}
