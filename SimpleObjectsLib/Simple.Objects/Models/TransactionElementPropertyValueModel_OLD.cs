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
//    public class TransactionElementPropertyValueModel : BusinessObjectModel<TransactionElementPropertyValue, TransactionElementPropertyValueModel>, IBusinessObjectModel
//    {
//        public static readonly TransactionElementPropertyValuePropertyModel PropertyModel = new TransactionElementPropertyValuePropertyModel();

//        public TransactionElementPropertyValueModel()
//        {
//            this.TableInfo = Tables.Instance.TransactionElementPropertyValues;
//            this.Properties = this.CreateModelDictionary<string, PropertyModel>(PropertyModel);
//            this.IsSystemObject = true;

//            this.ValidationRules.Add(new ValidationRuleExistance("TransactionElement", busienssObject => (busienssObject as TransactionElementPropertyValue).TransactionElement != null));
//        }
//    }

//    public class TransactionElementPropertyValuePropertyModel
//    {
//        public readonly PropertyModel TransactionElementObjectID        = new PropertyModel() { PropertyType = typeof(long), AccessPolicy = PropertyAccessPolicy.ReadOnly };
//        public readonly PropertyModel TransactionElementCreatorServerID = new PropertyModel() { PropertyType = typeof(int),  AccessPolicy = PropertyAccessPolicy.ReadOnly };
//        public readonly PropertyModel PropertyName                      = new PropertyModel() { PropertyType = typeof(int) };
//        public readonly PropertyModel PropertyValue                     = new PropertyModel() { PropertyType = typeof(int), };
//    }
//}
