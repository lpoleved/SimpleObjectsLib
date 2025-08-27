//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Simple.Objects
//{
//    public abstract class SimpleObjectDependencyAction
//    {
//        public abstract bool Match(SimpleObject simpleObject);

//        public GraphElement CreateGraphElement(SimpleObject simpleObject, int graphKey)
//        {
//            return this.CreateGraphElement(simpleObject, graphKey, null);
//        }

//        public GraphElement CreateGraphElement(SimpleObject simpleObject, int graphKey, GraphElement parentGraphElement)
//        {
//            GraphElement graphElement = new GraphElement(simpleObject.ObjectManager, graphKey, parentGraphElement, simpleObject);

//			if (!simpleObject.IsNew)
//				graphElement.Save();

//            return graphElement;
//        }

//		public bool DeleteGraphElement(GraphElement graphElement, bool checkSimpleObjectGraphElements)
//        {
//			graphElement.ObjectManager.Delete(this, graphElement, checkCanDelete: false, checkSimpleObjectGraphElements: checkSimpleObjectGraphElements);
//            return true;
//        }

//		public bool DeleteGraphElementBySimpleObjectAndGraphKey(SimpleObject simpleObject, int graphKey, bool checkSimpleObjectGraphElements)
//        {
//            bool isRemoved = false;
//            GraphElement graphElementToRemove = simpleObject.GetGraphElement(graphKey);

//            if (graphElementToRemove != null)
//            {
//				graphElementToRemove.ObjectManager.Delete(this, graphElementToRemove, checkCanDelete: false, checkSimpleObjectGraphElements: checkSimpleObjectGraphElements);
//                isRemoved = true;
//            }

//            return isRemoved;
//        }

//        //public virtual bool CanDeleteGraphElement(GraphElement graphElement)
//        //{
//        //    return graphElement.GraphElements.Count == 0;
//        //}

//        //public virtual bool CanGraphElementChangeParent(GraphElement graphElement, GraphElement newParentGraphElement)
//        //{
//        //    return false;
//        //}

//        public void SaveInternal(SimpleObject simpleObject)
//        {
//            simpleObject.ObjectManager.SaveInternal(this, simpleObject);
//        }

//        public void DeleteInternal(SimpleObject simpleObject)
//        {
//            simpleObject.ObjectManager.DeleteInternal(this, simpleObject);
//        }

//        public virtual void OnNewObjectCreated(SimpleObject simpleObject)
//        {
//        }

//        public virtual void OnSaving(SimpleObject simpleObject)
//        {
//        }

//        public virtual void OnBeforeDelete(SimpleObject simpleObject)
//        {
//        }

//        //public virtual void OnAfterDelete(SimpleObject simpleObject)
//        //{
//        //}

//        public virtual void OnPropertyValueChange(SimpleObject simpleObject, int propertyIndex, object value, object oldValue)
//        {
//        }


//        public virtual void OnGraphElementCreated(GraphElement graphElement)
//        {
//        }

//		public virtual void OnAfterGraphElementSave(GraphElement graphElement)
//		{
//		}
		
//		public virtual void OnBeforeGraphElementDelete(GraphElement graphElement)
//        {
//        }

//        public virtual void OnBeforeGraphElementParentChange(GraphElement graphElement, GraphElement oldParent, GraphElement newParent, ref bool cancel)
//        {
//        }

//        public virtual void OnGraphElementParentChange(GraphElement graphElement, GraphElement oldParent, GraphElement newParent)
//        {
//        }

//        public virtual void OnRelationForeignObjectSet(SimpleObject simpleObject, SimpleObject foreignSimpleObject, SimpleObject oldForeignSimpleObject, IOneToOneOrManyRelationModel objectRelationModel, ObjectRelationType objectRelationType)
//        {
//        }
        
//        //public override bool Match(SimpleObject simpleObject)
//        //{
//        //    return base.Match(simpleObject);
//        //}

//        //public override void OnCreation(SimpleObject simpleObject)
//        //{
//        //    base.OnCreation(simpleObject);
//        //}

//        //public override void OnBeforeDelete(SimpleObject simpleObject)
//        //{
//        //    base.OnBeforeDelete(simpleObject);
//        //}

//        //public override void OnPropertyValueChange(SimpleObject simpleObject, string propertyName, object newValue, object oldValue)
//        //{
//        //    base.OnPropertyValueChange(simpleObject, propertyName, newValue, oldValue);
//        //}
//    }
//}
