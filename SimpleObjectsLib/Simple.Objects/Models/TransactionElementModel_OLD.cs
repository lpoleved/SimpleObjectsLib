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
//    public class TransactionElementModel : BusinessObjectModel<TransactionElement, TransactionElementModel>, IBusinessObjectModel
//    {
//        public static readonly TransactionElementPropertyModel PropertyModel = new TransactionElementPropertyModel();

//        public TransactionElementModel()
//        {
//            this.TableInfo = Tables.Instance.TransactionElements;
//            this.Properties = this.CreateModelDictionary<string, PropertyModel>(PropertyModel);
//            this.IsSystemObject = true;

//            this.ValidationRules.Add(new ValidationRuleExistance("Transaction", busienssObject => (busienssObject as TransactionElement).Transaction != null));
//        }
//    }

//    public class TransactionElementPropertyModel
//    {
//        public readonly PropertyModel TransactionObjectID           = new PropertyModel() { PropertyType = typeof(long), AccessPolicy = PropertyAccessPolicy.ReadOnly };
//        public readonly PropertyModel TransactionCreatorServerID    = new PropertyModel() { PropertyType = typeof(int),  AccessPolicy = PropertyAccessPolicy.ReadOnly };
//        public readonly PropertyModel DataActionType                = new PropertyModel() { PropertyType = typeof(int) };
//        public readonly PropertyModel BusinessObjectTableID         = new PropertyModel() { PropertyType = typeof(int), };
//        public readonly PropertyModel BusinessObjectObjectID        = new PropertyModel() { PropertyType = typeof(long), };
//        public readonly PropertyModel BusinessObjectCreatorServerID = new PropertyModel() { PropertyType = typeof(int), };
//        //public readonly PropertyModel PropertyValueData             = new PropertyModel() { PropertyType = typeof(object) };
//        //public readonly PropertyModel OldPropertyValueData          = new PropertyModel() { PropertyType = typeof(object) };
//        public readonly PropertyModel Status                        = new PropertyModel() { PropertyType = typeof(int) };
//    }

//    public enum TransactionElementDataActionType
//    {
//        Insert = 0,
//        Update = 1,
//        Delete = 2
//    }

//    public enum TransactionElementStatus
//    {
//        Processing = 0,
//        Applied = 1
//    }
//}
