//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple.SimpleObjects;

//namespace Simple.BusinessObjects
//{
//    public class RelatedObjectCollectionModel : ModelElement, IRelatedObjectCollectionModel
//    {
//        private List<string> relatedPropertyNames = new List<string>();
        
//        public RelatedObjectCollectionModel()
//        {
//        }

//        public RelatedObjectCollectionModel(Func<BusinessObject, BusinessObjectCollection> getRelatedObjectCollectionDelegate, params string[] relatedPropertyNames)
//        {
//            this.GetRelatedObjectCollectionDelegate = getRelatedObjectCollectionDelegate;
//            this.relatedPropertyNames = relatedPropertyNames.ToList();
//        }

//        public Func<BusinessObject, BusinessObjectCollection> GetRelatedObjectCollectionDelegate { get; set; }

//        public List<string> RelatedPropertyNames
//        {
//            get { return this.relatedPropertyNames; }
//        }

//        IList<string> IRelatedObjectCollectionModel.RelatedPropertyNames
//        {
//            get { return this.RelatedPropertyNames.AsReadOnly(); }
//        }
//    }

//    public interface IRelatedObjectCollectionModel : IModelElement
//    {
//        Func<BusinessObject, BusinessObjectCollection> GetRelatedObjectCollectionDelegate { get; }
//        IList<string> RelatedPropertyNames { get; }
//    }
//}
