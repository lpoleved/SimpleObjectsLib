//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple;

//namespace Simple.Objects
//{
//    public class SimpleObjectHashtable : IEnumerable<SimpleObject>, IEnumerable, IDisposable
//    {
//        private List<SimpleObjectKey> objectKeys = new List<SimpleObjectKey>();
//        private Hashtable hastableSimpleObjectByObjectIdByObjectTypeID = new Hashtable();

//        public SimpleObjectHashtable()
//        {
//        }

//        public int Count
//        {
//            get { return this.objectKeys.Count; }
//        }

//        public SimpleObject this[int index]
//        {
//            get
//            {
//                if (index < 0 || index >= this.Count)
//                {
//                    throw new ArgumentException("The index is out of range.");
//                }

//                SimpleObjectKey objectKey = this.objectKeys[index];
//                SimpleObject value = this[objectKey];

//                return value;
//            }
//        }
        
//        public SimpleObject this[SimpleObjectKey objectKey]
//        {
//            get
//            {
//                SimpleObject value = null;
                
//                Hashtable hastableSimpleObjectByObjectId = this.hastableSimpleObjectByObjectIdByObjectTypeID[objectKey.TableId] as Hashtable;

//                if (hastableSimpleObjectByObjectId != null)
//                {
//                    value = hastableSimpleObjectByObjectId[objectKey.ObjectId] as SimpleObject;
//                }

//                return value;
//            }
//        }

//        public void Add(SimpleObject simpleObject)
//        {
//            Hashtable hastableSimpleObjectByObjectId = this.hastableSimpleObjectByObjectIdByObjectTypeID[simpleObject.Key.TableId] as Hashtable;

//            if (hastableSimpleObjectByObjectId == null)
//            {
//                hastableSimpleObjectByObjectId = new Hashtable();
//                this.hastableSimpleObjectByObjectIdByObjectTypeID.Add(simpleObject.Key.TableId, hastableSimpleObjectByObjectId);
//            }

//            hastableSimpleObjectByObjectId.Add(simpleObject.Key.ObjectId, simpleObject);
            
//            this.objectKeys.Add(simpleObject.Key);
//        }

//        public bool Remove(SimpleObject simpleObject)
//        {
//            bool isRemoved = false;
            
//            SimpleObjectKey objectKeyInList = this.FindObjectKey(simpleObject.Key.TableId, simpleObject.Key.ObjectId);

//            if (objectKeyInList != null)
//            {
//                this.objectKeys.Remove(objectKeyInList);

//                Hashtable hastableSimpleObjectByObjectId = this.hastableSimpleObjectByObjectIdByObjectTypeID[simpleObject.Key.TableId] as Hashtable;

//                if (hastableSimpleObjectByObjectId != null)
//                {
//                    hastableSimpleObjectByObjectId.Remove(simpleObject.Key.ObjectId);
//                }

//                if (hastableSimpleObjectByObjectId.Count == 0)
//                {
//                    this.hastableSimpleObjectByObjectIdByObjectTypeID.Remove(simpleObject.Key.TableId);
//                }

//                isRemoved = true;
//            }

//            return isRemoved;
//        }

//        public bool ContainsKey(SimpleObjectKey objectKey)
//        {
//            SimpleObjectKey foundedObjectKey = this.FindObjectKey(objectKey.TableId, objectKey.ObjectId);
//            bool result = foundedObjectKey != null;

//            return result;
//        }

//        public void Clear()
//        {
//            this.hastableSimpleObjectByObjectIdByObjectTypeID.Clear();
//            this.objectKeys.Clear();
//        }

//        public void Dispose()
//        {
//            this.Clear();

//            this.objectKeys = null;
//            this.hastableSimpleObjectByObjectIdByObjectTypeID = null;

//        }

//        private SimpleObjectKey FindObjectKey(int objectTypeID, long objectId)
//        {
//            SimpleObjectKey result = this.objectKeys.Find(o => o.TableId == objectTypeID && o.ObjectId == objectId);
//            return result;
//        }

//        /// <summary>
//        /// Returns an enumerator that iterates through the collection.
//        /// </summary>
//        /// <returns> A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection. </returns>
//        IEnumerator<SimpleObject> IEnumerable<SimpleObject>.GetEnumerator()
//        {
//            foreach (SimpleObjectKey objectKey in this.objectKeys)
//            {
//                yield return this[objectKey];
//            }
//        }

//        /// <summary>
//        /// Returns an enumerator that iterates through a collection.
//        /// </summary>
//        /// <returns> An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate through the collection. </returns>
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            foreach (SimpleObjectKey objectKey in this.objectKeys)
//            {
//                yield return this[objectKey];
//            }
//        }

//    }
//}
