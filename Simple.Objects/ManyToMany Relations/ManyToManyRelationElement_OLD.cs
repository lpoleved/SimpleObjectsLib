//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple;

//namespace Simple.Objects
//{
//    public class ManyToManyRelationElement_OLD : SimpleObject
//    {
//        private ManyToManyRelation manyToManyRelation = null;
//        private SimpleObjectKey objectKey1 = null, objectKey2 = null;

//        public ManyToManyRelationElement_OLD()
//        {
//        }

//        public ManyToManyRelationElement_OLD(ManyToManyRelation manyToManyRelation, SimpleObjectKey objectKey1, SimpleObjectKey objectKey2)
//        {
//            // Force key creation first.
//            SimpleObjectKey key = this.Key;

//            this.ManyToManyRelation = manyToManyRelation;
//            this.Object1TableId = objectKey1.TableId;
//            this.Object1ObjectId = objectKey1.ObjectId;
//            this.Object1CreatorServerId = objectKey1.CreatorServerId;
//            this.Object2TableId = objectKey2.TableId;
//            this.Object2ObjectId = objectKey2.ObjectId;
//            this.Object2CreatorServerId = objectKey2.CreatorServerId;
//        }

//        internal IManyToManySimpleObjectCollectionControl ManyToManySimpleObjectCollection1 { get; set; }
//        internal IManyToManySimpleObjectCollectionControl ManyToManySimpleObjectCollection2 { get; set; }

//        public ManyToManyRelation ManyToManyRelation
//        {
//            get { return this.manyToManyRelation; }
//            set
//            {
//                if (this.IsNew)
//                {
//                    if (this.ManyToManyRelation != null)
//                    {
//                        this.ManyToManyRelation.ManyToManyRelationElements.ListRemove(this.Key);
//                    }

//                    this.manyToManyRelation = value;
//                    this.ManyToManyRelationKey = this.manyToManyRelation.RelationKey;

//                    if (this.ManyToManyRelation != null && !this.ManyToManyRelation.ManyToManyRelationElements.ContainsKey(this.Key))
//                    {
//                        this.ManyToManyRelation.ManyToManyRelationElements.ListAdd(this.Key);
//                    }
//                }
//                else
//                {
//                    throw new ArgumentException("You can set ManyToManyRelationElement's ManyToManyRelation property only when IsNew = true.");
//                }
//            }
//       }

//        public SimpleObject SimpleObject1
//        {
//            get { return this.Manager.ObjectCache.GetObject(this.ObjectKey1); }
//        }

//        public SimpleObject SimpleObject2
//        {
//            get { return this.Manager.ObjectCache.GetObject(this.ObjectKey2); }
//        }

//        public SimpleObjectKey ObjectKey1
//        {
//            get 
//            {
//                if (this.objectKey1 == null)
//                {
//                    this.objectKey1 = new SimpleObjectKey(this.Object1TableId, this.Object1ObjectId, this.Object1CreatorServerId);
//                }

//                return this.objectKey1;
//            }
//        }

//        public SimpleObjectKey ObjectKey2
//        {
//            get 
//            {
//                if (this.objectKey2 == null)
//                {
//                    this.objectKey2 = new SimpleObjectKey(this.Object2TableId, this.Object2ObjectId, this.Object2CreatorServerId);
//                }

//                return this.objectKey2;
//            }
//        }

//        internal int ManyToManyRelationKey
//        {
//            get { return this.GetPropertyValue<int>(ManyToManyRelationElementModel.PropertyModel.ManyToManyRelationKey, 0); }
//            set { this.SetPropertyValueInternal(ManyToManyRelationElementModel.PropertyModel.ManyToManyRelationKey.Name, value, false, true, this); }
//        }
        
//        internal int Object1TableId
//        {
//            get { return this.GetPropertyValue<int>(ManyToManyRelationElementModel.PropertyModel.Object1TableId, 0); }
//            private set { this.SetPropertyValueInternal(ManyToManyRelationElementModel.PropertyModel.Object1TableId.Name, value, false, true, this); }
//        }

//        internal long Object1ObjectId
//        {
//            get { return this.GetPropertyValue<long>(ManyToManyRelationElementModel.PropertyModel.Object1ObjectId, 0); }
//            private set { this.SetPropertyValueInternal(ManyToManyRelationElementModel.PropertyModel.Object1ObjectId.Name, value, false, true, this); }
//        }

//        internal int Object1CreatorServerId
//        {
//            get { return this.GetPropertyValue<int>(ManyToManyRelationElementModel.PropertyModel.Object1CreatorServerId, 0); }
//            private set { this.SetPropertyValueInternal(ManyToManyRelationElementModel.PropertyModel.Object1CreatorServerId.Name, value, false, true, this); }
//        }

//        internal int Object2TableId
//        {
//            get { return this.GetPropertyValue<int>(ManyToManyRelationElementModel.PropertyModel.Object2TableId, 0); }
//            private set { this.SetPropertyValueInternal(ManyToManyRelationElementModel.PropertyModel.Object2TableId.Name, value, false, true, this); }
//        }

//        internal long Object2ObjectId
//        {
//            get { return this.GetPropertyValue<long>(ManyToManyRelationElementModel.PropertyModel.Object2ObjectId, 0); }
//            private set { this.SetPropertyValueInternal(ManyToManyRelationElementModel.PropertyModel.Object2ObjectId.Name, value, false, true, this); }
//        }

//        internal int Object2CreatorServerId
//        {
//            get { return this.GetPropertyValue<int>(ManyToManyRelationElementModel.PropertyModel.Object2CreatorServerId, 0); }
//            private set { this.SetPropertyValueInternal(ManyToManyRelationElementModel.PropertyModel.Object2CreatorServerId.Name, value, false, true, this); }
//        }

//        public void SaveIfCan()
//        {
//            if (this.IsNew) 
//            {
//                bool canSave = this.Manager.ObjectCache.IsObjectInCache(this.ObjectKey1) ? !this.SimpleObject1.IsNew : true;
//                canSave &= this.Manager.ObjectCache.IsObjectInCache(this.ObjectKey2) ? !this.SimpleObject2.IsNew : true;

//                if (canSave)
//                {
//                    this.Save();
//                }
//            }
//        }

//        protected override void OnBeforeDeleting(object requester)
//        {
//            if (this.ManyToManyRelation != null)
//            {
//                this.ManyToManyRelation.ManyToManyRelationElements.ListRemove(this.Key);
//            }
            
//            if (this.ManyToManySimpleObjectCollection1 != null)
//            {
//                this.ManyToManySimpleObjectCollection1.RemoveGroupMembershipElement(this);
//            }

//            if (this.ManyToManySimpleObjectCollection2 != null)
//            {
//                this.ManyToManySimpleObjectCollection2.RemoveGroupMembershipElement(this);
//            }

//            base.OnBeforeDeleting(requester);
//        }
//    }
//}