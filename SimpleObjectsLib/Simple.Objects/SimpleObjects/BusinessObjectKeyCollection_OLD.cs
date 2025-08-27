//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple;
//using Simple.Collections;
//using Simple.Datastore;

//namespace Simple.Objects
//{
//    public class SimpleObjectKeyCollection : SimpleList<SimpleObjectKey>
//    {
//        public SimpleObjectKeyCollection()
//        {
//            //this.recordKeysByTableId = new Hashtable();
//        }

//        public SimpleObjectKeyCollection(int capacity)
//            : base(capacity)
//        {
//        }

//        public SimpleObjectKeyCollection(IList<SimpleObjectKey> objectKeys)
//            : base(objectKeys)
//        {
//        }

//        //public bool ContainsKey(SimpleObjectKey objectKey)
//        //{
//        //    return base.Contains(objectKey);
//        //    //List<RecordKey> recordKeys = this.recordKeysByTableId[objectKey.TableId] as List<RecordKey>;

//        //    //if (recordKeys != null)
//        //    //{
//        //    //    RecordKey recordKey = new RecordKey(objectKey.ObjectId, objectKey.CreatorServerId);
//        //    //    return recordKeys.Contains(recordKey);
//        //    //}
//        //    //else
//        //    //{
//        //    //    return false;
//        //    //}
//        //}

//        //public IList<RecordKey> GetRecordKeys(int tableId)
//        //{
//        //    List<RecordKey> recordKeys = this.recordKeysByTableId[tableId] as List<RecordKey>;

//        //    if (recordKeys == null)
//        //    {
//        //        recordKeys = new List<RecordKey>();
//        //    }

//        //    return recordKeys;
//        //}

//        //protected override void OnBeforeInsert(int index, SimpleObjectKey value)
//        //{
//        //    base.OnBeforeInsert(index, value);
//        //    this.AddToHash(value);
//        //}

//        //protected override void OnBeforeRemove(int index, SimpleObjectKey value)
//        //{
//        //    base.OnBeforeRemove(index, value);
//        //    this.RemoveFromHash(value);
//        //}

//        //protected override void OnBeforeSet(int index, SimpleObjectKey oldValue, SimpleObjectKey newValue)
//        //{
//        //    base.OnBeforeSet(index, oldValue, newValue);

//        //    this.RemoveFromHash(oldValue);
//        //    this.AddToHash(newValue);
//        //}

//        //protected override void OnBeforeClear()
//        //{
//        //    base.OnBeforeClear();
//        //    this.recordKeysByTableId.Clear();
//        //}

//        //private void AddToHash(SimpleObjectKey objectKey)
//        //{
//        //    RecordKey recordKey = new RecordKey(objectKey.ObjectId, objectKey.CreatorServerId);
//        //    List<RecordKey> recordKeys = this.recordKeysByTableId[objectKey.TableId] as List<RecordKey>;

//        //    if (recordKeys == null)
//        //    {
//        //        recordKeys = new List<RecordKey>();
//        //        this.recordKeysByTableId.Add(objectKey.TableId, recordKeys);
//        //    }
//        //    else
//        //    {
//        //        if (recordKeys.Contains(recordKey))
//        //        {
//        //            throw new ArgumentException("The key is already exists in the SimpleObjectKeyCollection; ObjectTypeID = " + objectKey.TableId + ", ObjectId = " + objectKey.ObjectId + ".");
//        //        }
//        //    }

//        //    recordKeys.Add(recordKey);
//        //}

//        //private void RemoveFromHash(SimpleObjectKey objectKey)
//        //{
//        //    List<RecordKey> recordKeys = this.recordKeysByTableId[objectKey.TableId] as List<RecordKey>;

//        //    if (recordKeys != null)
//        //    {
//        //        RecordKey recordKey = new RecordKey(objectKey.ObjectId, objectKey.CreatorServerId);

//        //        recordKeys.Remove(recordKey);

//        //        if (recordKeys.Count == 0)
//        //        {
//        //            this.recordKeysByTableId.Remove(objectKey.TableId);
//        //        }
//        //    }
//        //}
//    }
//}
