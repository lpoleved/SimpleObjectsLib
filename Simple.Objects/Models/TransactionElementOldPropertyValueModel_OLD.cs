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
//    public class TransactionElementOldPropertyValueModel : BusinessObjectModel<TransactionElementOldPropertyValue, TransactionElementOldPropertyValueModel>, IBusinessObjectModel
//    {
//        public static readonly TransactionElementPropertyValuePropertyModel PropertyModel = new TransactionElementPropertyValuePropertyModel();

//        public TransactionElementOldPropertyValueModel()
//        {
//            this.TableInfo = Tables.Instance.TransactionElementOldPropertyValues;
//            this.Properties = this.CreateModelDictionary<string, PropertyModel>(PropertyModel);
//            this.IsSystemObject = true;

//            this.ValidationRules.Add(new ValidationRuleExistance("TransactionElement", busienssObject => (busienssObject as TransactionElementPropertyValue).TransactionElement != null));
//        }
//    }
//}
