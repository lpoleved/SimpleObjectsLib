//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using Simple.Collections;
//using Simple.Modeling;

//namespace Simple.Objects
//{
//	public class GraphModel : SimpleObjectModel<Graph, GraphPropertyModel, GraphModel>, ISimpleObjectModel
//	{
//		//public static new GraphElementPropertyModel PropertyModel = new GraphElementPropertyModel();
//		public GraphModel() : base(TablesBase.Graphs)
//		{
//			this.FetchAllRecords = true;
//			// To speed up saving and processing validation is avoided.
//			//            //this.AddRelatedObjectCollectionModel(this.GetRootGraphElementCollection, GraphElementModel.PropertyModel.GraphID.Name, GraphElementModel.PropertyModel.ParentID.Name);
//			//            //this.RelatedCollectionDelegates.Add(this.GetParentGraphElementCollection);
//			//            //this.RelatedCollectionDelegates.Add(this.GetSimpleObjectGraphElementCollection);

//			//this.ValidationRules.Add(new ValidationRuleUnique(PropertyModel.GraphKey, so => so.Manager.Graphs));
//			this.ValidationRules.Add(new ValidationRuleExistance(PropertyModel.Name));
//			//this.ValidationRules.Add(new ValidationRuleUnique(PropertyModel.Name));

//			this.MustHaveAtLeastOneGraphElement = false;
//			this.SortableOneToManyRelationKey = RelationPolicyModelBase.OneToManyGraphElementToParentGraphElement.RelationKey;
//		}

//		//public GraphElementPropertyModel GraphElementPropertyModel
//		//{
//		//	get { return PropertyModel; }
//		//}

//		//public SimpleObjectCollection GetParentGraphElementCollection(SimpleObject simpleObject)
//		//{
//		//    GraphElement graphElement = simpleObject as GraphElement;
//		//    return graphElement.Parent == null ? graphElement.Graph.GraphElements : graphElement.Parent.GraphElements;
//		//}

//		//public SimpleObjectCollection GetSimpleObjectGraphElementCollection(SimpleObject simpleObject)
//		//{
//		//    GraphElement graphElement = simpleObject as GraphElement;
//		//    return graphElement.SimpleObject.GraphElements;
//		//}
//		//public SimpleObjectCollection GetRootGraphElementCollection(SimpleObject simpleObject)
//		//{
//		//    GraphElement graphElement = simpleObject as GraphElement;
//		//    return graphElement.Parent == null ? graphElement.Graph.RootGraphElements : null;
//		//}
//	}

//	//public class GraphElementPropertyModel_OLD : SortableObjectPropertyModel_OLD
//	//   {
//	//	public readonly PropertyModel GraphKey            = new PropertyModel<int>(11)   { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Public,   SetAccessModifier = AccessModifier.Private, FirePropertyValueChangeEvent = false, DatastoreType = typeof(short), IsIndexed = true, CanSetOnClientUpdate = false }; // This property is not part of the relation thus require manual sets IsIdexed and IsSerializationOptimizable.
//	//	public readonly PropertyModel ParentId            = new PropertyModel<long>(12)  { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, SetAccessModifier = AccessModifier.Private, IsRelationKey = true };
//	//	public readonly PropertyModel ParentGuid          = new PropertyModel<Guid?>(13) { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, SetAccessModifier = AccessModifier.Private, IsRelationKey = true };
//	//	public readonly PropertyModel SimpleObjectTableId = new PropertyModel<int>(14)   { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, IsRelationTableId = true, DatastoreType = typeof(short) };
//	//	public readonly PropertyModel SimpleObjectId      = new PropertyModel<long>(15)  { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, IsRelationKey = true, };
//	//	public readonly PropertyModel SimpleObjectGuid    = new PropertyModel<Guid>(16)  { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, IsRelationKey = true, };
//	//   }

//	public class GraphPropertyModel : SimpleObjectPropertyModelBase // SortableObjectPropertyModel
//	{
//		public PM GraphKey = new PM<int> (1);
//		public PM Name   = new PM<string>(2);
//	}
//}
