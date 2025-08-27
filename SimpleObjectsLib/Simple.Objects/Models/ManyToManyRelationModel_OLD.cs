//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple.Modeling;
//using Simple.SimpleObjects;

//namespace Simple.BusinessObjects
//{
//    public class ManyToManyRelationModel : BusinessObjectModel<ManyToManyRelation, ManyToManyRelationModel>
//    {
//        public static readonly ManyToManyRelationPropertyModel PropertyModel = new ManyToManyRelationPropertyModel();

//        public ManyToManyRelationModel()
//        {
//            this.IsStorable = false;
//            this.DatastoreObjectInfo = DatastoreObjects.Instance.ManyToManyRelation;
//            this.Properties = this.CreateModelCollection<PropertyModel>(PropertyModel);
//        }
//    }

//    public class ManyToManyRelationPropertyModel
//    {
//        public readonly PropertyModel Name = new PropertyModel() { PropertyType = typeof(string) };
//    }
//}
