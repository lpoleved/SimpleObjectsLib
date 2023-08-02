//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple.Modelling;

//namespace Simple.BusinessObjects
//{
//    public class VirtualGrapElementModel : SimpleModelElement
//    {
//        public VirtualGrapElementModel()
//        {
//            this.GetParentVirtualGraphElement = businessObject => null;
//        }
        
//        public int GraphID { get; set; }
//        public Func<BusinessObject, GraphElement> GetParentVirtualGraphElement { get; set; }
//    }

//    public interface IVirtualGrapElementModel : IModelElement
//    {
//        int GraphID { get; }
//        Func<BusinessObject, GraphElement> GetParentVirtualGraphElement { get; }
//    }
//}
