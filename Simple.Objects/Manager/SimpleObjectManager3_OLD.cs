//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple;

//namespace Simple.Objects
//{
//    partial class SimpleObjectManager
//    {
//        [ServerResponse(RequestKey.GetObjectKeys)]
//        protected IEnumerable<Guid> GetObjectKeys(int tableId)
//        {
//            SimpleObjectKey[] objectKeys = this.ObjectCache.GetObjectKeys(tableId);
//            List<Guid> result = new List<Guid>();

//            foreach (SimpleObjectKey objectKey in objectKeys)
//                result.Add(objectKey.Guid);

//            return result;
//        }

//        //protected object[] SerializeGetObjectKeysArgs(IBufferData bufferData)
//        //{
//        //    return null;
//        //}
//    }
//}
