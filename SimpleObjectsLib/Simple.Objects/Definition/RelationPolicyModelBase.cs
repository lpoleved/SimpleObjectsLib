using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Simple;
using Simple.Modeling;

namespace Simple.Objects
{
	//public class RelationPolicyModelBase<T> : RelationPolicyModelBase, IRelationPolicyModel, IModelElement 
	//	where T : RelationPolicyModelBase<T>, new()
	//{
	//	//public RelationPolicyModelBase()
	//	//{
	//	//	if (instance == null)
	//	//		instance = this;
	//	//}

	//	//public static new T Instance
	//	//{
	//	//	get { return GetInstance<T>(); }
	//	//}
	//}

	public class RelationPolicyModelBase : ModelElement, IRelationPolicyModel, IModelElement
    {
		//private Dictionary<string, FieldInfo> fieldInfosByName = null;
		private ModelDictionary<int, OneToOneRelationModel> oneToOneRelations = new ModelDictionary<int, OneToOneRelationModel>();
		private ModelDictionary<int, OneToManyRelationModel> oneToManyRelations = new ModelDictionary<int, OneToManyRelationModel>();
		private ModelDictionary<int, ManyToManyRelationModel> manyToManyRelations = new ModelDictionary<int, ManyToManyRelationModel>();
		private SortedDictionary<int, RelationModel> relations = new SortedDictionary<int, RelationModel>();

		//private static object lockObjectInstance = new object();
		//      protected static RelationPolicyModelBase instance = null;

		//protected static readonly UniqueKeyGenerator RelationModelKeys = new UniqueKeyGenerator();
		private static UniqueKeyGenerator<int> RelationKeyGenerator = null;
		private static IList<int> predefinedRelationKeys = null;

		//protected static readonly UniqueKeyGenerator<int> OneToOneRelationModelKeys = new UniqueKeyGenerator<int>();
		//protected static readonly UniqueKeyGenerator<int> OneToManyRelationModelKeys = new UniqueKeyGenerator<int>();
		//protected static readonly UniqueKeyGenerator<int> ManyToManyRelationKeys = new UniqueKeyGenerator<int>();

		// Each relation type has its own unique key. No unique key for all relations!
		public RelationPolicyModelBase()
		{
			//if (instance == null)
			//{
			//	instance = this;
			//}
			//else
			//{
			//	return;
			//}
			//this.fieldInfosByName = ReflectionHelper.GetFieldInfosByName(this);

			if (predefinedRelationKeys == null)
			{
				IDictionary<string, RelationModel> relationModelFieldsByName = ReflectionHelper.GetFieldsByName<RelationModel>(this);
				List<int> relationsWithPredefinedRelationKey = new List<int>();

				foreach (var item in relationModelFieldsByName)
				{
					string fieldName = item.Key;
					RelationModel model = item.Value;

					if (model.RelationKey > 0)
					{
						if (relationsWithPredefinedRelationKey.Contains(model.RelationKey))
							throw new Exception("RelationKey must be unique, ObjectType:" + this.GetType().ToString());

						relationsWithPredefinedRelationKey.Add(model.RelationKey);
					}
				}

				relationsWithPredefinedRelationKey.Sort();
				predefinedRelationKeys = relationsWithPredefinedRelationKey;
			}

			RelationKeyGenerator = new UniqueKeyGenerator<int>(this.PredefinedRelationKeys, minKey: 1, reuseKeys: true);

			this.OneToOneRelations	 = this.CreateModelDictionary<int, OneToOneRelationModel>(this,	  rpm => rpm.RelationKey > 0 ? rpm.RelationKey : rpm.RelationKey = RelationKeyGenerator.CreateKey());
			this.OneToManyRelations	 = this.CreateModelDictionary<int, OneToManyRelationModel>(this,  rpm => rpm.RelationKey > 0 ? rpm.RelationKey : rpm.RelationKey = RelationKeyGenerator.CreateKey());
			this.ManyToManyRelations = this.CreateModelDictionary<int, ManyToManyRelationModel>(this, rpm => rpm.RelationKey > 0 ? rpm.RelationKey : rpm.RelationKey = RelationKeyGenerator.CreateKey());
			//this.ManyToManyRelations = this.CreateModelDictionary<int, ManyToManyRelationModel>(this, rpm => rpm.RelationKey != -1 ? rpm.RelationKey : rpm.RelationKey = ManyToManyRelationKeys.CreateKey());

			foreach (OneToOneRelationModel relationModel in this.OneToOneRelations.Values)
			{
				relationModel.Index = relationModel.RelationKey;
				relationModel.PrimaryObjectIdPropertyModel.RelationKey = relationModel.RelationKey;

				if (relationModel.PrimaryTableIdPropertyModel != null)
					relationModel.PrimaryTableIdPropertyModel.RelationKey = relationModel.RelationKey;

				this.Relations.Add(relationModel.RelationKey, relationModel);
			}

			foreach (OneToManyRelationModel relationModel in this.OneToManyRelations.Values)
			{
				relationModel.Index = relationModel.RelationKey;
				relationModel.PrimaryObjectIdPropertyModel.RelationKey = relationModel.RelationKey;

				if (relationModel.PrimaryTableIdPropertyModel != null)
					relationModel.PrimaryTableIdPropertyModel.RelationKey = relationModel.RelationKey;

				this.Relations.Add(relationModel.RelationKey, relationModel);
			}

			foreach (ManyToManyRelationModel relationModel in this.ManyToManyRelations.Values)
			{
				relationModel.Index = relationModel.RelationKey;

				this.Relations.Add(relationModel.RelationKey, relationModel);
			}
		}

		protected override void OnCreateDictionaryElement<TKey, TFieldType>(FieldInfo fieldInfo, string fieldName, TKey key, TFieldType model, IDictionary<TKey, TFieldType> modelDictionary)
		{
			base.OnCreateDictionaryElement<TKey, TFieldType>(fieldInfo, fieldName, key, model, modelDictionary);

			//FieldInfo fieldInfo = this.fieldInfosByName[fieldName];

			if (model is RelationModel relationModel)
			{
				relationModel.DefinitionFieldName = fieldName;

				if (fieldInfo.ReflectedType != null)
					relationModel.DefinitionObjectClassType = fieldInfo.ReflectedType;
			}
		}

		public IList<int> PredefinedRelationKeys 
		{
			get { return predefinedRelationKeys; }
		}

		//public static readonly OneToOneRelationPolicyModel OneToOneRelationModelUncompletedTransactionToTransaction = new OneToOneRelationPolicyModel()
		//{
		//    ObjectType = typeof(UncompletedTransaction),
		//    ForeignObjectIdPropertyName = UncompletedTransactionModel.PropertyModel.TransactionObjectId.Name,
		//    ForeignObjectCreatorServerIdPropertyName = UncompletedTransactionModel.PropertyModel.TransactionCreatorServerId.Name,
		//    ForeignObjectType = typeof(TransactionLog)
		//};

		//public static readonly OneToOneRelationPolicyModel OneToOneRelationModelGraphElementToNextGraphElement = new OneToOneRelationPolicyModel()
		//{
		//	ObjectType = typeof(GraphElement),
		//	ForeignObjectIdPropertyName = GraphElementModel.PropertyModel.NextObjectId.Name,
		//	ForeignObjectCreatorServerIdPropertyName = GraphElementModel.PropertyModel.NextCreatorServerId.Name,
		//	ForeignObjectType = typeof(GraphElement)
		//};

		//public static readonly OneToManyRelationPolicyModel OneToManyRelationModelGraphElementToGraph = new OneToManyRelationPolicyModel()
		//{
		//    ObjectType = typeof(GraphElement),
		//    ForeignObjectIdPropertyName = GraphElementModel.PropertyModel.GraphID.Name,
		//    ForeignObjectCreatorServerIdPropertyName  = GraphElementModel.PropertyModel.GraphCreatorServerId.Name,
		//    ForeignObjectType = typeof(Graph)
		//};

		//public static readonly OneToManyRelationModel OneToManyRelationModelSortableObjectToPreviousSortableObject = new OneToManyRelationModel()
		//{
		//	KeyHolderObjectType = typeof(SortableSimpleObject),
		//	KeyHolderAccessModifier = AccessModifier.Public,
		//          ForeignObjectType = typeof(SortableSimpleObject),
		//	ForeignIdPropertyModel = GraphElementModel.PropertyModel.PreviousId,
		//	ForeignKeyPropertyModel = GraphElementModel.PropertyModel.PreviousGuid,
		//	ForeignObjectName = "Previous",
		//	CascadeDelete = false,
		//	CanBeNull = true
		//};

		//public static readonly OneToManyRelationModel OneToManyGraphToGraphElement = new OneToManyRelationModel()
		//{
		//	PrimaryObjectType = typeof(Graph),
		//	ForeignObjectType = typeof(GraphElement),
		//	ForeignCollectionName = "AllGraphElements",
		//	//KeyHolderAccessModifier = AccessModifier.Public | AccessModifier.Override, // Hide SimpleObject's GraphElements property that derives from OneToManyRelationModelGraphElementToSimpleObject and has no meaning for GraphElement
		//	ForeignObjectIdPropertyModel = GraphElementModel.PropertyModel.GraphId,
		//	CascadeDelete = true,
		//	CanBeNull = false,
		//	//GetForeignCollectionSelectorByPrimaryObject = (graph) => new Predicate<SimpleObject>(graphElement => !graphElement.DeleteStarted && (graphElement as GraphElement).GraphId == (graph as Graph).Id && (graphElement as GraphElement).ParentId == 0)
		//};

		public static readonly OneToManyRelationModel OneToManyGraphElementToParentGraphElement = new OneToManyRelationModel()
        {
            PrimaryObjectType = typeof(GraphElement),
			PrimaryObjectName = "Parent",
			ForeignObjectType = typeof(GraphElement),
			PrimaryAccessModifier = AccessModifier.Public | AccessModifier.Override, // Hide SimpleObject's GraphElements property that derives from OneToManyRelationModelGraphElementToSimpleObject and has no meaning for GraphElement
			PrimaryObjectIdPropertyModel = GraphElementModel.PropertyModel.ParentId,
            CascadeDelete = true,
			CanBeNull = true
        };

		public static readonly OneToManyRelationModel OneToManyGraphElementToSimpleObject = new OneToManyRelationModel()
		{
			PrimaryObjectType = typeof(SimpleObject),
			PrimaryObjectExcludedTypes = new Type[] { typeof(GraphElement) },
            ForeignObjectType = typeof(GraphElement),
 			PrimaryAccessModifier = AccessModifier.Public | AccessModifier.Virtual,
			PrimaryTableIdPropertyModel = GraphElementModel.PropertyModel.ObjectTableId,
			PrimaryObjectIdPropertyModel = GraphElementModel.PropertyModel.ObjectId,
			//dodati ForeignModifier = "private"
			//dodati i ForeignKeyModifier po 
            CascadeDelete = true,
			CanBeNull = false,
        };

		//public static readonly OneToManyRelationModel OneToManyGraphElementToUser = new OneToManyRelationModel()
		//{
		//	PrimaryObjectType = typeof(IUser),
		//	PrimaryObjectExcludedTypes = new Type[] { typeof(GraphElement) },
		//	ForeignObjectType = typeof(GraphElement),
		//	PrimaryAccessModifier = AccessModifier.Public | AccessModifier.Virtual,
		//	//PrimaryTableIdPropertyModel = GraphElementModel.PropertyModel.ObjectTableId,
		//	PrimaryObjectIdPropertyModel = GraphElementModel.PropertyModel.UserId,
		//	//dodati ForeignModifier = "private"
		//	//dodati i ForeignKeyModifier po 
		//	CascadeDelete = true,
		//	CanBeNull = false,
		//};

		//public static readonly OneToOneRelationModel OneToOneRelationModelGraphElementToPreviousGraphElement = new OneToOneRelationModel()
		//{
		//	KeyHolderObjectType = typeof(GraphElement),
		//	KeyHolderObjectName = "Next",
		//	ForeignObjectType = typeof(GraphElement), // ForeignObjectType is NetworkNode or Vlan
		//	ForeignObjectIdPropertyModel = GraphElementModel.PropertyModel.PreviousId,
		//	ForeignObjectName = "Previous",
		//	CascadeDelete = false, 
		//	CanBeNull = true
		//};


		//public static readonly OneToManyRelationPolicyModel OneToManyRelationModelTransactionElementToTransaction = new OneToManyRelationPolicyModel()
		//{
		//    ObjectType = typeof(TransactionElement),
		//    ForeignObjectIdPropertyName = TransactionElementModel.PropertyModel.TransactionObjectId.Name,
		//    ForeignObjectCreatorServerIdPropertyName = TransactionElementModel.PropertyModel.TransactionCreatorServerId.Name,
		//    ForeignObjectType = typeof(TransactionLog)
		//};

		//public static readonly OneToManyRelationPolicyModel OneToManyRelationModelTransactionLogToAdministrator = new OneToManyRelationPolicyModel()
		//{
		//	ObjectType = typeof(SystemTransaction),
		//	ForeignGuidPropertyIndex = SystemTransactionModel.PropertyModel.AdministratorGuid.Index,
		//	//ForeignObjectCreatorServerIdPropertyName = SystemTransactionModel.PropertyModel.AdministratorCreatorServerId.Name,
		//	ForeignObjectType = typeof(Administrator),
		//	CascadeDelete = true,
		//	CanBeNull = false
		//};

		//public static readonly OneToManyRelationPolicyModel OneToManyRelationModelTransactionElementPropertyValueToTransactionElement = new OneToManyRelationPolicyModel()
		//{
		//    ObjectType = typeof(TransactionElementPropertyValue),
		//    ForeignObjectIdPropertyName = TransactionElementPropertyValueModel.PropertyModel.TransactionElementObjectId.Name,
		//    ForeignObjectCreatorServerIdPropertyName = TransactionElementPropertyValueModel.PropertyModel.TransactionElementCreatorServerId.Name,
		//    ForeignObjectType = typeof(TransactionElement)
		//};

		//public static readonly OneToManyRelationPolicyModel OneToManyRelationModelTransactionElementOldPropertyValueToTransactionElement = new OneToManyRelationPolicyModel()
		//{
		//    ObjectType = typeof(TransactionElementOldPropertyValue),
		//    ForeignObjectIdPropertyName = TransactionElementOldPropertyValueModel.PropertyModel.TransactionElementObjectId.Name,
		//    ForeignObjectCreatorServerIdPropertyName = TransactionElementOldPropertyValueModel.PropertyModel.TransactionElementCreatorServerId.Name,
		//    ForeignObjectType = typeof(TransactionElement)
		//};

		//public static readonly OneToManyRelationPolicyModel OneToManyRelationModelManyToManyRelationElementToManyToManyRelation = new OneToManyRelationPolicyModel()
		//{
		//    ObjectType = typeof(ManyToManyRelationElement),
		//    ForeignObjectTypeIDPropertyName = ManyToManyRelationElementModel.PropertyModel.ManyToManyRelationID.Name,
		//    ForeignObjectCreatorServerIdPropertyName = ManyToManyRelationElementModel.PropertyModel.ManyToManyRelationCreatorServerId.Name,
		//    ForeignObjectType = typeof(ManyToManyRelation)
		//};

		public ModelDictionary<int, OneToOneRelationModel> OneToOneRelations
        {

			get { return this.oneToOneRelations; }
			protected set { this.oneToOneRelations = value; }
			
			//get { return this.GetModelDictionary<int, OneToOneRelationModel>(); }
            //protected set { this.SetModelDictionary<int, OneToOneRelationModel>(value); }
        }

        public ModelDictionary<int, OneToManyRelationModel> OneToManyRelations
        {
			get { return this.oneToManyRelations; }
			protected set { this.oneToManyRelations = value; }

			//get { return this.GetModelDictionary<int, OneToManyRelationModel>(); }
            //protected set { this.SetModelDictionary<int, OneToManyRelationModel>(value); }
        }

		public SortedDictionary<int, RelationModel> Relations
		{
			get { return this.relations; }
			protected set { this.relations = value; }
		}

		public ModelDictionary<int, ManyToManyRelationModel> ManyToManyRelations
        {
			get { return this.manyToManyRelations; }
			protected set { this.manyToManyRelations = value; }

			//get { return this.GetModelDictionary<int, ManyToManyRelationModel>(); }
            //protected set { this.SetModelDictionary<int, ManyToManyRelationModel>(value); }
        }

        //public static RelationPolicyModelBase Instance
        //{
        //    get { return GetInstance<RelationPolicyModelBase>(); }
        //}

        //protected static T GetInstance<T>() where T : RelationPolicyModelBase, new()
        //{
        //    lock (lockObjectInstance)
        //    {
        //        if (instance == null)
        //            instance = new T();
        //    }

        //    return instance as T;
        //}

		//internal static void SetInstance(RelationPolicyModelBase relationPolicyModelBaseInstance)
		//{
		//	instance = relationPolicyModelBaseInstance;
		//}

        IList<IOneToOneRelationModel> IRelationPolicyModel.OneToOneRelations
        {
            get { return this.GetModelCollection<OneToOneRelationModel>().AsCustom<IOneToOneRelationModel>().AsReadOnly(); }
        }

        IList<IOneToManyRelationModel> IRelationPolicyModel.OneToManyRelations
        {
            get { return this.GetModelCollection<OneToManyRelationModel>().AsCustom<IOneToManyRelationModel>().AsReadOnly(); }
        }

        IList<IManyToManyRelationModel> IRelationPolicyModel.ManyToManyRelations
        {
            get { return this.GetModelCollection<ManyToManyRelationModel>().AsCustom<IManyToManyRelationModel>().AsReadOnly(); }
        }
    }

    public interface IRelationPolicyModel : IModelElement
    {
        IList<IOneToOneRelationModel> OneToOneRelations { get; }
        IList<IOneToManyRelationModel> OneToManyRelations { get; }
        IList<IManyToManyRelationModel> ManyToManyRelations { get; }
    }
}
