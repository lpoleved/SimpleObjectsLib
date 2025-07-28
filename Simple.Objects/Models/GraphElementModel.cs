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
		public GraphElementModel() : base(SystemTablesBase.GraphElements)
		{
			this.FetchAllRecords = true;
			// To speed up saving and processing validation is avoided.
			//            //this.AddRelatedObjectCollectionModel(this.GetRootGraphElementCollection, GraphElementModel.PropertyModel.GraphID.Name, GraphElementModel.PropertyModel.ParentID.Name);
			//            //this.RelatedCollectionDelegates.Add(this.GetParentGraphElementCollection);
			//            //this.RelatedCollectionDelegates.Add(this.GetSimpleObjectGraphElementCollection);

			this.UpdateValidationRules.Add(new ValidationRuleExistance(PropertyModel.GraphKey, (simpleObject, transaction) => (simpleObject as GraphElement)?.Graph != null, "GraphElement must have Graph specified."));
			this.UpdateValidationRules.Add(new ValidationRuleExistance(PropertyModel.ObjectId, (simpleObject, transaction) => !simpleObject.IsNew || (simpleObject as GraphElement)?.SimpleObject != null, "GraphElement must have SimpleObject specified.")); // Allow update anomaly change if existing GE has no SO
																																																															   
			//this.UpdateValidationRules.Add(new ValidationRule("Graph Policy Validation", (simpleObject, transaction) => simpleObject.Manager.ValidateGraphPolicy((simpleObject as GraphElement)!)));
			this.UpdateValidationRules.Add(new ValidationRuleCustom(ValidateHasAsParent, "Inserting parent into child GraphElement is not allowed."));
			//this.UpdateValidationRules.Add(new ValidationRuleCustom((simpleObject, transaction) => (simpleObject as GraphElement)!.Parent != null && !(simpleObject as GraphElement)!.Parent!.HasAsParent((simpleObject as GraphElement)!), "Inserting parent into child GraphElement is not allowed."));
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



		public static bool ValidateHasAsParent(SimpleObject simpleObject, IDictionary<SimpleObject, TransactionRequestAction> transactionRequests)
		{
			if (simpleObject is GraphElement graphElement && graphElement.Parent != null)
				return !graphElement.Parent.HasAsParent(graphElement);

			return true;
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
	}

	public class GraphElementPropertyModel : SimpleObjectPropertyModelBase // SortableObjectPropertyModel
	{
		//public GraphElementPropertyModel()
		//	: base((s1, s2) => (s1.Index == GraphElementModel.PropertyModel.OrderIndex.Index) ? 1 : s1.Index.CompareTo(s2.Index)) 
		//{
		//}

		public PM PreviousId = new PreviousIdPM<long>(1);
		public PM GraphKey              = new PM<int>(2) { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Public, SetAccessModifier = AccessModifier.Internal, DatastoreType = typeof(short), IsIndexed = true }; // FirePropertyValueChangeEvent = false, CanSetOnClientUpdate = false }; // This property is not part of the relation thus require manual sets IsIdexed and IsSerializationOptimizable.
		public PM ParentId			   = new PM<long>(3) { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Public, IsRelationObjectId = true }; // SetAccessModifier = AccessModifier.Private,
		public PM ObjectTableId         = new PM<int>(4) { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Public }; //, DatastoreType = typeof(short) };
		public PM ObjectId			   = new PM<long>(5) { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Public };
		public PM UserId			   = new PM<long>(6) { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Public, DefaultValue = 1 };

		public PM OrderIndex = new OrderIndexPM();
		public PM PreviousParentId = new PM<long>()		          { IsStorable = false, IsServerToClientTransactionInfoSeriazable = true, IsClientToServerSeriazable = false, IsServerToClientSeriazable = false };
		public PM DoesPreviousParentHaveChildren = new PM<bool>() { IsStorable = false, IsServerToClientTransactionInfoSeriazable = true, IsClientToServerSeriazable = false, IsServerToClientSeriazable = false } ;
		//public PM ActionSetOrderIndex = new ActionSetOrderIndexPM();
	}
}
