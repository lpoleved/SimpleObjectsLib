using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Simple.Collections;
using Simple.Modeling;

namespace Simple.Objects
{
	public class GraphElementModel : SimpleObjectModel<GraphElement, GraphElementPropertyModel, GraphElementModel>, ISimpleObjectModel
    {
		//public static new GraphElementPropertyModel PropertyModel = new GraphElementPropertyModel();
		public GraphElementModel() : base(SystemTables.GraphElements)
        {
			this.FetchAllRecords = true;
            // To speed up saving and processing validation is avoided.
//            //this.AddRelatedObjectCollectionModel(this.GetRootGraphElementCollection, GraphElementModel.PropertyModel.GraphID.Name, GraphElementModel.PropertyModel.ParentID.Name);
//            //this.RelatedCollectionDelegates.Add(this.GetParentGraphElementCollection);
//            //this.RelatedCollectionDelegates.Add(this.GetSimpleObjectGraphElementCollection);

            this.UpdateValidationRules.Add(new ValidationRuleExistance(PropertyModel.GraphKey, (simpleObject, transaction) => (simpleObject as GraphElement).Graph != null, "GraphElement must have Graph specified."));
			this.UpdateValidationRules.Add(new ValidationRuleExistance(PropertyModel.ObjectId, (simpleObject, transaction) => (simpleObject as GraphElement).SimpleObject != null, "GraphElement must have SimpleObject specified."));
			//            this.ValidationRules.Add(new ValidationRuleIsInDatastore("Graph", ge => (ge as GraphElement).Graph));
			//this.ValidationRules.Add(new ValidationRuleExistence(PropertyModel.ObjectId, (simpleObject, transaction) => (simpleObject as GraphElement).SimpleObject != null));
			//this.ValidationRules.Add(new ValidationRuleCustom(PropertyModel.ObjectId, (simpleObject, transaction) => (simpleObject as GraphElement).SimpleObject.IsNew != true, "SimpleObject must be saved before saving GraphElement."));
			////            this.ValidationRoules.Add(new ValidationRuleIsInDatastore("SimpleObject", ge => (ge as GraphElement).SimpleObject));
			//this.ValidationRules.Add(new ValidationRuleIsInDatastore("Parent", simpleObject => (simpleObject as GraphElement).Parent));

			this.UpdateValidationRules.Add(new ValidationRule("Graph Policy Validation", (simpleObject, transaction) => simpleObject.Manager.ValidateGraphPolicy(simpleObject as GraphElement)));
			//this.ValidationRules.Add(new ValidationRule("Graph Policy Normalization", simpleObject => simpleObject.ObjectManager.ValidateGraphNormalization(simpleObject as GraphElement)));

			// -> TODO: Add GraphPolicyValidation

			this.MustHaveAtLeastOneGraphElement = false;
			this.SortableOneToManyRelationKey = RelationPolicyModelBase.OneToManyGraphElementToParentGraphElement.RelationKey;
        }
		
		//public GraphElementPropertyModel GraphElementPropertyModel
		//{
		//	get { return PropertyModel; }
		//}

        //public SimpleObjectCollection GetParentGraphElementCollection(SimpleObject simpleObject)
        //{
        //    GraphElement graphElement = simpleObject as GraphElement;
        //    return graphElement.Parent == null ? graphElement.Graph.GraphElements : graphElement.Parent.GraphElements;
        //}

        //public SimpleObjectCollection GetSimpleObjectGraphElementCollection(SimpleObject simpleObject)
        //{
        //    GraphElement graphElement = simpleObject as GraphElement;
        //    return graphElement.SimpleObject.GraphElements;
        //}
        //public SimpleObjectCollection GetRootGraphElementCollection(SimpleObject simpleObject)
        //{
        //    GraphElement graphElement = simpleObject as GraphElement;
        //    return graphElement.Parent == null ? graphElement.Graph.RootGraphElements : null;
        //}
    }

	//public static bool ValidateMacAddressText(SimpleObject simpleObject, IDictionary<SimpleObject, TransactionRequestAction> transactionRequests)
	//{
	//	InterfaceBase interfaceObject = simpleObject as InterfaceBase;

	//	if (interfaceObject.MacAddressText == null || interfaceObject.MacAddressText.Trim().Length == 0)
	//		return interfaceObject.MacAddress == null;

	//	return MacAddressHelper.ValidateMacAddress(interfaceObject.MacAddressText);
	//}


	//public class GraphElementPropertyModel_OLD : SortableObjectPropertyModel_OLD
	//   {
	//	public readonly PropertyModel GraphKey            = new PropertyModel<int>(11)   { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Public,   SetAccessModifier = AccessModifier.Private, FirePropertyValueChangeEvent = false, DatastoreType = typeof(short), IsIndexed = true, CanSetOnClientUpdate = false }; // This property is not part of the relation thus require manual sets IsIdexed and IsSerializationOptimizable.
	//	public readonly PropertyModel ParentId            = new PropertyModel<long>(12)  { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, SetAccessModifier = AccessModifier.Private, IsRelationKey = true };
	//	public readonly PropertyModel ParentGuid          = new PropertyModel<Guid?>(13) { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, SetAccessModifier = AccessModifier.Private, IsRelationKey = true };
	//	public readonly PropertyModel SimpleObjectTableId = new PropertyModel<int>(14)   { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, IsRelationTableId = true, DatastoreType = typeof(short) };
	//	public readonly PropertyModel SimpleObjectId      = new PropertyModel<long>(15)  { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, IsRelationKey = true, };
	//	public readonly PropertyModel SimpleObjectGuid    = new PropertyModel<Guid>(16)  { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, IsRelationKey = true, };
	//   }

	public class GraphElementPropertyModel : SimpleObjectPropertyModelBase // SortableObjectPropertyModel
	{
		//public GraphElementPropertyModel()
		//	: base((s1, s2) => (s1.Index == GraphElementModel.PropertyModel.OrderIndex.Index) ? 1 : s1.Index.CompareTo(s2.Index)) 
		//{
		//}

		public PM PreviousId = new PrevIdPM<long>(1);
		public PM GraphKey		    = new PM<int>(2) { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Public, SetAccessModifier = AccessModifier.Internal, DatastoreType = typeof(short), IsIndexed = true }; // FirePropertyValueChangeEvent = false, CanSetOnClientUpdate = false }; // This property is not part of the relation thus require manual sets IsIdexed and IsSerializationOptimizable.
		public PM ParentId         = new PM<long>(3) { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, IsRelationObjectId = true }; // SetAccessModifier = AccessModifier.Private,
		public PM ObjectTableId	    = new PM<int>(4) { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, DatastoreType = typeof(short) };
		public PM ObjectId		   = new PM<long>(5) { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal };

		public PM OrderIndex = new OrderIndexPM();
		//public PM ActionSetOrderIndex = new ActionSetOrderIndexPM();
	}
}
