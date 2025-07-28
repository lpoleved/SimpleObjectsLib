//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple.Collections;
//using Simple.Reflection;

//namespace Simple.BusinessObjects
//{
//    public class StaticBusinessObjects<T> where T : BusinessObject
//    {
//        private BusinessObjectCollection<T> collection = null;
//        private SimpleDictionary<int, T> dictionaryByObjectID = new SimpleDictionary<int, T>();
//        private SimpleDictionary<string, T> dictionaryByName = new SimpleDictionary<string, T>();

//        public StaticBusinessObjects(BusinessObjectManager manager)
//        {
//            List<BusinessObjectKey> objectKeys = new List<BusinessObjectKey>();

//            IDictionary<string, T> fieldsByName = ReflectionHelper.GetFieldsByReflection<T>(this);

//            foreach (KeyValuePair<string, T> keyValuePair in fieldsByName)
//            {
//                T businessObject = keyValuePair.Value;
//                businessObject.Name = keyValuePair.Key;

//                objectKeys.Add(businessObject.Key);
//                this.dictionaryByObjectID.Add(businessObject.Key.ObjectID, businessObject);
//                this.dictionaryByName.Add(businessObject.Name, businessObject);
//            }

//            this.collection = new BusinessObjectCollection<T>(manager, objectKeys);
//        }

//        public BusinessObjectCollection<T> Collection
//        {
//            get { return this.collection; }
//        }

//        public IDictionary<int, T> DictionaryObjectID
//        {
//            get { return this.dictionaryByObjectID.AsReadOnly(); }
//        }

//        public IDictionary<string, T> DictionaryByName
//        {
//            get { return this.dictionaryByName.AsReadOnly(); }
//        }
//    }
//}
