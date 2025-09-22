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
//    internal class ObjectTypeModel : BusinessObjectModel<ObjectType, ObjectTypeModel>, IBusinessObjectModel
//    {
//        public static readonly ObjectTypePropertyModel PropertyModel = new ObjectTypePropertyModel();
        
//        public ObjectTypeModel()
//        {
//            this.TableName = Names.TableObjectTypes;
//            this.Properties = this.CreateModelCollection<PropertyModel>(PropertyModel);
//        }
//    }

//    internal class ObjectTypePropertyModel
//    {
//        public readonly PropertyModel TableName = new PropertyModel() { PropertyType = typeof(string) };
//    }
//}
