//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.IO;
//using System.Xml;
//using System.Xml.Serialization;
//using Simple;
//using Simple.Collections;
//using Simple.Datastore;

//namespace Simple.BusinessObjects
//{
//    public class BusinessObjectDatastore
//    {
//        private SimpleDatastore simpleDatastore = new SimpleDatastore();

//        public DatastoreProviderType DatastoreProviderType
//        {
//            get { return this.simpleDatastore.DatastoreProviderType; }
//            set { this.simpleDatastore.DatastoreProviderType = value; }
//        }

//        public string DataSource
//        {
//            get { return this.simpleDatastore.DataSource; }
//            set { this.simpleDatastore.DataSource = value; }
//        }

//        public string Username
//        {
//            get { return this.simpleDatastore.Username; }
//            set { this.simpleDatastore.Username = value; }
//        }

//        public string Password
//        {
//            get { return this.simpleDatastore.Password; }
//            set { this.simpleDatastore.Password = value; }
//        }

//        public bool IsConnected
//        {
//            get { return this.simpleDatastore.IsConnected; }
//        }

//        public void Connect()
//        {
//            this.simpleDatastore.Connect();
//        }

//        public void Disconnect()
//        {
//            this.simpleDatastore.Disconnect();
//        }

//        public IEnumerable<string> GetTableNames()
//        {
//            return this.simpleDatastore.GetTableNames();
//        }

//        public IEnumerable<RecordKey> GetRecordKeys(string tableName, string objectIDFieldName, string creatorServerIDFieldName)
//        {
//            return this.simpleDatastore.GetRecordKeys(tableName, objectIDFieldName, creatorServerIDFieldName);
//        }

//        public IDictionary<string, object> GetRecord(string tableName, string objectIDFieldName, string creatorServerIDFieldName, int objectID, int creatorServerID)
//        {
//            return this.simpleDatastore.GetRecord(tableName, objectIDFieldName, creatorServerIDFieldName, objectID, creatorServerID);
//        }

//        public IDictionary<string, object> GetRecord(string tableName, string objectIDFieldName, string creatorServerIDFieldName, int objectID, int creatorServerID, string[] fieldNames)
//        {
//            return this.simpleDatastore.GetRecord(tableName, objectIDFieldName, creatorServerIDFieldName, objectID, creatorServerID, fieldNames);
//        }

//        public IEnumerable<IDictionary<string, object>> GetAllRecords(string tableName)
//        {
//            return this.simpleDatastore.GetAllRecords(tableName);
//        }

//        public IEnumerable<IDictionary<string, object>> GetAllRecords(string tableName, IEnumerable<string> fieldNames)
//        {
//            return this.simpleDatastore.GetAllRecords(tableName, fieldNames);
//        }

//        public void InsertRecord(BusinessObject businessObject, IDictionary<string, object> fieldData, bool createTransactionLog, Transaction currentTransaction)
//        {
//            IBusinessObjectModel objectModel = businessObject.GetObjectModel();
//            TransactionElement transactionElement = null;
            
//            if (createTransactionLog)
//            {
//                transactionElement = new TransactionElement();
//                transactionElement.Transaction = currentTransaction;
//                transactionElement.DataActionType = TransactionElementDataActionType.Insert;
//                transactionElement.BusinessObjectTableID = businessObject.Key.TableID;
//                transactionElement.BusinessObjectObjectID = businessObject.Key.ObjectID;
//                transactionElement.BusinessObjectCreatorServerID = businessObject.Key.CreatorServerID;
//                transactionElement.PropertyValueData = this.SerializeFieldDataToXml(fieldData);
//                transactionElement.Status = TransactionElementStatus.Processing;
//                transactionElement.Save();
//            }

//            this.simpleDatastore.InsertRecord(objectModel.TableInfo.TableName, fieldData);

//            if (createTransactionLog)
//            {
//                transactionElement.Status = TransactionElementStatus.Applied;
//                transactionElement.Save();
//            }
//        }

//        public void UpdateRecord(BusinessObject businessObject, IDictionary<string, object> fieldData, bool createTransactionLog, Transaction currentTransaction)
//        {
//            IBusinessObjectModel objectModel = businessObject.GetObjectModel();
//            TransactionElement transactionElement = null;

//            if (createTransactionLog)
//            {
//                transactionElement = new TransactionElement();
//                transactionElement.Transaction = currentTransaction;
//                transactionElement.DataActionType = TransactionElementDataActionType.Update;
//                transactionElement.BusinessObjectTableID = businessObject.Key.TableID;
//                transactionElement.BusinessObjectObjectID = businessObject.Key.ObjectID;
//                transactionElement.BusinessObjectCreatorServerID = businessObject.Key.CreatorServerID;
//                transactionElement.PropertyValueData = this.SerializeFieldDataToXml(fieldData);
//                transactionElement.Status = TransactionElementStatus.Processing;
//                transactionElement.Save();
//            }

//            this.simpleDatastore.UpdateRecord(objectModel.TableInfo.TableName, BusinessObjectModel.PropertyModel.ObjectID.Name, BusinessObjectModel.PropertyModel.CreatorServerID.Name,
//                                              businessObject.Key.ObjectID, businessObject.Key.CreatorServerID, fieldData);

//            if (createTransactionLog)
//            {
//                transactionElement.Status = TransactionElementStatus.Applied;
//                transactionElement.Save();
//            }
//        }

//        public void DeleteRecord(BusinessObject businessObject, bool createTransactionLog, Transaction currentTransaction)
//        {
//            IBusinessObjectModel objectModel = businessObject.GetObjectModel();
//            TransactionElement transactionElement = null;

//            if (createTransactionLog)
//            {
//                transactionElement = new TransactionElement();
//                transactionElement.Transaction = currentTransaction;
//                transactionElement.BusinessObjectTableID = businessObject.Key.TableID;
//                transactionElement.BusinessObjectObjectID = businessObject.Key.ObjectID;
//                transactionElement.BusinessObjectCreatorServerID = businessObject.Key.CreatorServerID;
//                transactionElement.DataActionType = TransactionElementDataActionType.Delete;
//                transactionElement.Status = TransactionElementStatus.Processing;
//                transactionElement.Save();
//            }

//            this.simpleDatastore.DeleteRecord(objectModel.TableInfo.TableName, BusinessObjectModel.PropertyModel.ObjectID.Name, BusinessObjectModel.PropertyModel.CreatorServerID.Name,
//                                              businessObject.Key.ObjectID, businessObject.Key.CreatorServerID);
        
//            if (createTransactionLog)
//            {
//                transactionElement.Status = TransactionElementStatus.Applied;
//                transactionElement.Save();
//            }
//        }

//        public string SerializeFieldDataToXml(IDictionary<string, object> fieldData)
//        {
//            //string result = XmlHelpers.SerializeObject<SimpleDictionary<string, string>>(new SimpleDictionary<string, string>(new SimpleDictionary<string, object>(fieldData).AsCustom<string>()));
//            string result = XmlHelpers.SerializeObject<SimpleDictionary<string, object>>(new SimpleDictionary<string, object>(fieldData));

//            //IDictionary<string, object> fieldData2 = this.DeserializeDataFromXml(result);

//            return result;
//        }

//        public IDictionary<string, object> DeserializeFieldDataFromXml(string xmlString)
//        {
//            SimpleDictionary<string, object> result = XmlHelpers.DeserializeObject<SimpleDictionary<string, object>>(xmlString);

//            return result;
//        }

//        public void Dispose()
//        {
//            this.simpleDatastore.Dispose();
//            this.simpleDatastore = null;
//        }
//    }
//}
