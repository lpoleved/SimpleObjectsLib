//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using Simple.Collections;
//using Simple.Modeling;
//using Simple.SimpleObjects;

//namespace Simple.BusinessObjects
//{
//    public class UncompletedTransactionModel : BusinessObjectModel<UncompletedTransaction, UncompletedTransactionModel>, IBusinessObjectModel
//    {
//        public static readonly UncompletedTransactionPropertyModel PropertyModel = new UncompletedTransactionPropertyModel();

//        public UncompletedTransactionModel()
//        {
//            this.TableInfo = Tables.Instance.UncompletedTransactions;
//            this.Properties = this.CreateModelDictionary<string, PropertyModel>(PropertyModel);
//            this.IsSystemObject = true;
//        }
//    }

//    public class UncompletedTransactionPropertyModel
//    {
//        public readonly PropertyModel TransactionObjectID        = new PropertyModel() { PropertyType = typeof(long), AccessPolicy = PropertyAccessPolicy.ReadOnly };
//        public readonly PropertyModel TransactionCreatorServerID = new PropertyModel() { PropertyType = typeof(int),  AccessPolicy = PropertyAccessPolicy.ReadOnly };
//    }
//}
