using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Reflection;
using Simple;
using Simple.Collections;
using Simple.Modeling;
using Simple.Datastore;

namespace Simple.Objects
{
	public abstract class SimpleObjectModel<TObject, TPropertyModel, TObjectModel> : SimpleObjectModel, ISimpleObjectModel
		where TObject : SimpleObject
		where TPropertyModel : SimpleObjectPropertyModelBase, new()
		where TObjectModel : SimpleObjectModel<TObject, TPropertyModel, TObjectModel>, new()
	{
		private static TObjectModel? instance = null;
		//private static object lockObjectInstance = new object();

		public static readonly TPropertyModel PropertyModel = new TPropertyModel();
		public static string ObjectTypeName = typeof(TObject).Name;
		//public static readonly TObjectModel Default = new TObjectModel();
		public static int TableId = 0;
		public static string? TableName = null;
		//public static readonly new ObjectImageModel ImageModel = new ObjectImageModel();

		/// <summary>
		/// Constructor for non storable objects
		/// </summary>
		public SimpleObjectModel()
			: this(default(TableInfo))
		{
		}

		public SimpleObjectModel(TableInfo tableInfo)
			: base(tableInfo, typeof(TObject), PropertyModel)
		{
			//if (!tableInfo.Equals(default(TableModel)))
			//{
			//	TableId = tableInfo.TableId;
			//	TableName = tableInfo.TableName;
			//}

			if (instance == null && this is TObjectModel objectModel)
				instance = objectModel;
		}

		public static TObjectModel Instance //{ get; private set; }
		{
			get
			{
				//lock (lockObjectInstance)
				//{
				//	if (instance == null)
				//		instance = new TObjectModel();
				//}

				return instance!;
			}
		}

		protected override void OnSetTableInfo(TableInfo tableInfo)
		{
			base.OnSetTableInfo(tableInfo);

			TableId = tableInfo.TableId;
			TableName = tableInfo.TableName;
		}

		//protected override int GetTableId()
		//{
		//	return TableId;
		//}

		//protected override PropertyModelCollection<PropertyModel> GetPropertyModelCollection()
		//{
		//	return PropertyModel.GetCollection();
		//}
	}

	public abstract class SimpleObjectModel : ModelElement, ISimpleObjectModel
	{
		//private int tableId = 0;
		//private ObjectImageModel imageModel = null;
		private TableInfo tableInfo;
		private ReadOnlyList<IValidationRule> iUpdateValidationRules = null;
		private ReadOnlyList<IValidationRule> iDeleteValidationRules = null;
		private PropertyModelCollection<IPropertyModel> iPropertyModelCollection = null;
		private int sortableOneToManyRelationKey;
		private bool mustHaveAtLeastOneGraphElement = false;
		private int mustHaveGraphElementGraphKey = 0;

		//internal SerializationModel serializationModel;
		//internal PropertySequence serializablePropertySequence = null;
		//private int[] storablePropertyIndexes = null;
		//private int storablePropertyIndexSeqienceId = -1;
		//private IPropertyModel[] storablePropertyModels = null;

		//public SimpleObjectModel(Type objectType, string name)
		//	: this(null, objectType, name)
		//{
		//}

		private static Dictionary<Type, SimpleObjectModel> objectModelsByType = new Dictionary<Type, SimpleObjectModel>();
		private static object lockObject = new object();

		public SimpleObjectModel(TableInfo tableInfo, Type objectType, object propertyModelElementFieldHolderInstance)
		{
			//List<IPropertyModel> storablePropertyModelSequence = new List<IPropertyModel>();
			List<int> propertyIndexes = new List<int>();
			List<int> indexedPropertyIndexes = new List<int>();
			List<int> serializablePropertyIndexes = new List<int>();
			List<int> storablePropertyIndexes = new List<int>();
			//List<int> storablePropertyTypeIds = new List<int>();
			List<int> transactionActionLogPropertyIndexes = new List<int>();

			this.TableInfo = tableInfo; //(tableInfo != null) ? tableInfo : TableInfo.Empty;
			this.PropertyModels = new PropertyModelCollection<PropertyModel>(propertyModelElementFieldHolderInstance, owner: this);
			this.iPropertyModelCollection = this.PropertyModels.AsCustom<IPropertyModel>();

			foreach (PropertyModel propertyModel in this.PropertyModels)
			{
				propertyModel.PropertyInfo = objectType.GetProperties(BindingFlags.Instance).FirstOrDefault(item => item.Name == propertyModel.PropertyName);
				propertyModel.Owner = this;
				propertyIndexes.Add(propertyModel.PropertyIndex);

				if (propertyModel.IsIndexed)
					indexedPropertyIndexes.Add(propertyModel.PropertyIndex);
				
				if (propertyModel.IsClientSeriazable)
					serializablePropertyIndexes.Add(propertyModel.PropertyIndex);

				if (propertyModel.IsStorable) // Storable models must be a serializable also to be in a serialized storable sequence in transaction action data
				{
					storablePropertyIndexes.Add(propertyModel.PropertyIndex);
					//storablePropertyTypeIds.Add(PropertyTypes.GetPropertyTypeId(propertyModel.DatastoreType));
				}

				if (propertyModel.IncludeInTransactionActionLog) // Storable models must be a serializable also to be in a serialized storable sequence in transaction action data
					transactionActionLogPropertyIndexes.Add(propertyModel.PropertyIndex);

				if (propertyModel.IsKey)
					this.IdPropertyModel = propertyModel;

				if (propertyModel.PropertyName == SimpleObject.StringPropertyName)
				{
					this.NamePropertyModel = propertyModel;
				}
				else if (propertyModel.PropertyName == SimpleObject.StringPropertyDescription)
				{
					this.DescriptionPropertyModel = propertyModel;
				}
				else if (propertyModel.PropertyName == SimpleObject.StringPropertyObjectSubType)
				{
					this.ObjectSubTypePropertyModel = propertyModel;
				}

				if (propertyModel.IsPreviousId)
					this.PreviousIdPropertyModel = propertyModel;

				if (propertyModel.IsOrderIndex)
					this.OrderIndexPropertyModel = propertyModel;

				//if (propertyModel.IsActionSetOrderIndex)
				//	this.ActionSetOrderIndexPropertyModel = propertyModel;

				propertyModel.Owner = this;
			}

			//foreach (var propertyModel in this.PropertyModels)
			//	propertyModel.Owner = this;

			this.PropertyIndexes = propertyIndexes.ToArray();
			this.IndexedPropertyIndexes = indexedPropertyIndexes.ToArray();
			this.SerializablePropertyIndexes = serializablePropertyIndexes.ToArray();
			this.StorablePropertyIndexes = storablePropertyIndexes.ToArray();
			//this.StorablePropertyTypeIds = storablePropertyTypeIds.ToArray();
			this.TransactionActionLogPropertyIndexes = transactionActionLogPropertyIndexes.ToArray();

			//this.StorablePropertyModelSequence = storablePropertyModelSequence.ToArray();
			//this.StorablePropertyIndexSequence = storablePropertyIndexes.ToArray();
			//this.PublicPropertyIndexSequence = publicPropertyIndexes.ToArray();

			//int[] storablePropertyTypesIds = new int[this.StorablePropertyIndexSequence.Length];

			//for (int i = 0; i < this.StorablePropertyIndexSequence.Length; i++)
			//	storablePropertyTypesIds[i] = this.PropertyModels[this.StorablePropertyIndexSequence[i]].PropertyTypeId;

			//this.StorablePropertySequenceId = -1; 

			//this.iPropertyModelCollection = new PropertyModelCollection<IPropertyModel>(this.PropertyModels.ToDictionary<IPropertyModel>(), this.Owner) as IPropertyModelCollection<IPropertyModel>;
			//this.iPropertyModelCollection = this.PropertyModels.AsCustom<IPropertyModel>();

			// Set IsIndexed to true for object key property
			//this.PropertyModels[SimpleObject.IndexPropertyGuid].IsRelationKey = true;
			//this.PropertyModels[SimpleObject.IndexPropertyGuid].IsIndexed = true;

			//Dictionary<string, IPropertyModel> propertyModelsByName = new Dictionary<string, IPropertyModel>(this.GetPropertyModelCollection().Count);

			//foreach (var item in this.GetPropertyModelCollection())
			//	propertyModelsByName.Add(item.Name, item);

			//this.iPropertyModelCollection = new PropertyModelCollection<PropertyModel>(propertyModelsByName, this.Owner);

			//var test = this.GetPropertyModelCollection() as IPropertyModelCollection<IPropertyModel>;

			this.Name = this.GetType().Name; // objectType.Name;
			this.Caption = this.Name.InsertSpaceOnUpperChange();
			this.ObjectType = objectType;
			this.ObjectTypeCaption = this.ObjectType.Name.InsertSpaceOnUpperChange();
			//this.IsSortable = objectType.IsSubclassOf(typeof(SortableSimpleObject));
			this.IsStorable = true;
			this.FetchAllRecords = false;
			this.DeleteSimpleObjectOnLastGraphElementDelete = true;
			this.DeleteAllSimpleObjectGraphElementsOnOneGraphElementDelete = false;
			this.MustHaveAtLeastOneGraphElement = true;
			this.SortableOneToManyRelationKey = -1;
			this.ImageName = objectType.Name;
			this.RelationModel = null; // SimpleObjectManager will set it after the relation model discovery.
			this.ObjectSubTypes = new ModelDictionary<int, ModelElement>();
			//this.GraphElementCreatedActions = new SimpleList<GraphElementCreatedAction>();
			this.UpdateValidationRules = new SimpleList<ValidationRule>();
			this.DeleteValidationRules = new SimpleList<ValidationRule>();
			this.SerializationModel = SerializationModel.IndexValuePairs;

			this.Initialize();
		}

		//protected abstract int GetTableId();

		//public int TableId => GetTableId();
		//public string TableName { get; private set; }
		public TableInfo TableInfo
		{
			get { return this.tableInfo; }

			set
			{
				this.tableInfo = value;
				this.OnSetTableInfo(value);
			}
		}

		public Type ObjectType { get; set; }
		public string ObjectTypeCaption { get; set; }
		public SerializationModel SerializationModel { get; set; }
		public bool IsStorable { get; set; }
		//public bool IsSortable { get; set; }
		public SortingModel SortingModel { get; set; }

		public bool FetchAllRecords { get; set; }
		public bool DeleteSimpleObjectOnLastGraphElementDelete { get; set; }
		public bool DeleteAllSimpleObjectGraphElementsOnOneGraphElementDelete { get; set; }

		public bool MustHaveAtLeastOneGraphElement { get => this.mustHaveAtLeastOneGraphElement; set => this.mustHaveAtLeastOneGraphElement = value; }

		public int MustHaveGraphElementGraphKey 
		{
			get => this.mustHaveGraphElementGraphKey;
			set
			{
				this.mustHaveGraphElementGraphKey = value;

				if (value > 0)
					this.MustHaveAtLeastOneGraphElement = true;
			}
		}

		public int SortableOneToManyRelationKey 
		{
			get { return this.sortableOneToManyRelationKey; }
			
			set
			{
				this.sortableOneToManyRelationKey = value;

				if (value > 0)
					this.SortingModel = SortingModel.ByOneToManyRelationKey;
			}
		}

		public IPropertyModel IdPropertyModel { get; set; }
		public IPropertyModel NamePropertyModel { get; set; }
		public IPropertyModel DescriptionPropertyModel { get; set; }
		public IPropertyModel ObjectSubTypePropertyModel { get; set; }
		public IPropertyModel PreviousIdPropertyModel { get; set; }
		public IPropertyModel OrderIndexPropertyModel { get; set; }
		//public IPropertyModel ActionSetOrderIndexPropertyModel { get; set; }


		/// <summary>
		/// Gets or Sets object relation model. SimpleObjectManager should be responsible for setting this value after relation object model discovery.
		/// </summary>
		public ObjectRelationModel RelationModel { get; set; }
		public ModelDictionary<int, ModelElement> ObjectSubTypes { get; set; }
		//public SimpleList<GraphElementCreatedAction> GraphElementCreatedActions { get; set; }
		public SimpleList<ValidationRule> UpdateValidationRules { get; set; }
		public SimpleList<ValidationRule> DeleteValidationRules { get; set; }

		public int[] PropertyIndexes { get; set; }
		public int[] IndexedPropertyIndexes { get; set; }
		public string[] StorableFieldNameIndexes { get; private set; }
		public int[] StorablePropertyIndexes{ get; private set; }
		//public int[] StorablePropertyTypeIds { get; private set; }

		// TODO: Consider remove it
		public int[] TransactionActionLogPropertyIndexes { get; private set; }
		
		public int[] SerializablePropertyIndexes { get; private set; }




		// TODO : remove this
		public int TransactionActionLogPropertySequenceId { get; set; }

		public PropertyModelCollection<PropertyModel> PropertyModels { get; private set; }

		public IPropertyModel GetPropertyModel(int propertyIndex)
		{
			return this.PropertyModels[propertyIndex];
		}
		
		//public IPropertyModel[] StorablePropertyModelSequence { get; set; }
		//public int[] StorablePropertyIndexSequence { get; set; }

		//public int StorablePropertySequenceId { get; set; }

		//public int[] PublicPropertyIndexSequence { get; set; }



		//public int[] StorablePropertyIndexes
		//{
		//	get
		//	{
		//		if (this.storablePropertyIndexes == null)
		//			this.storablePropertyIndexes = (this.Owner as SimpleObjectManager).GetStorablePropertyIndexes(this);

		//		return this.storablePropertyIndexes;
		//	}
		//}

		//public int StorablePropertyIndexSequenceId
		//{
		//	get
		//	{
		//		if (this.storablePropertyIndexSeqienceId < 0)
		//		{
		//			int[] storablePropertyTypesIds = new int[this.StorablePropertyIndexes.Length];

		//			for (int i = 0; i < this.StorablePropertyIndexes.Length; i++)
		//				storablePropertyTypesIds[i] = this.PropertyModels[this.StorablePropertyIndexes[i]].PropertyTypeId;

		//			this.storablePropertyIndexSeqienceId = (this.Owner as SimpleObjectManager).GetPropertyIndexSequence(this.StorablePropertyIndexes, storablePropertyTypesIds).PropertyIndexSequenceId;

		//		}

		//		return this.storablePropertyIndexSeqienceId;
		//	}
		//}

		//public IPropertyModel[] StorablePropertyModels
		//{
		//	get
		//	{
		//		if (this.storablePropertyModels == null)
		//		{
		//			this.storablePropertyModels = new IPropertyModel[this.StorablePropertyIndexes.Length];

		//			for (int i = 0; i < this.StorablePropertyIndexes.Length; i++)
		//				this.storablePropertyModels[i] = this.PropertyModels[this.StorablePropertyIndexes[i]];
		//		}

		//		return this.storablePropertyModels;
		//	}
		//}

		//public ImageModel ImageModel
  //      {
  //          get { return this.imageModel; }

  //          protected set
  //          {
  //              this.imageModel = value;

  //              if (this.imageModel != null)
  //                  this.imageModel.Owner = this;
  //          }
  //      }


		//{
		//	get { return this.GetServerSeriazablePropertySequence(); }
		//}

		//internal Func<SerializationModel> GetServerSerializationModel { get; set; }

		//internal Func<PropertySequence> GetServerSeriazablePropertySequence { get; set; }


		//protected internal abstract void SetTableId(int tableId);
		//protected abstract PropertyModelCollection<PropertyModel> GetPropertyModelCollection();

		//public int GetTableId()
		//{
		//	return this.tableId;
		//}

		//public void AddGraphElementCreatedAction(int graphKey, Action<GraphElement> action)
		//{
		//	this.GraphElementCreatedActions.Add(new GraphElementCreatedAction(graphKey, action));
		//}

		//int ISimpleObjectModel.TableId
		//{
		//	get { return this.tableId; }
		//}

		public static SimpleObjectModel GetInstance(Type objectModelType)
		{
			lock (lockObject)
			{
				SimpleObjectModel objectModel = null;

				if (!objectModelsByType.TryGetValue(objectModelType, out objectModel))
				{
					objectModel = Activator.CreateInstance(objectModelType) as SimpleObjectModel;
					objectModelsByType.Add(objectModelType, objectModel);
				}

				return objectModel;
			}
		}

		protected virtual void Initialize() { }
		protected virtual void OnSetTableInfo(TableInfo tableInfo) { }

		PropertyModelCollection<IPropertyModel> ISimpleObjectModel.PropertyModels
        {
            get { return this.iPropertyModelCollection; } // PropertyModels. as IPropertyModelCollection<PropertyModel>; }
        }

		int[] ISimpleObjectModel.SerializablePropertyIndexes
		{
			get { return this.SerializablePropertyIndexes; }
		}

		int[] ISimpleObjectModel.StorablePropertyIndexes
		{
			get { return this.StorablePropertyIndexes; }
		}

		int[] ISimpleObjectModel.TransactionActionPropertyIndexes
		{
			get { return this.TransactionActionLogPropertyIndexes; }
		}

		//ITableInfo ISimpleObjectModel.TableInfo
		//{
		//	get { return this.TableInfo; }
		//}

		//IImageModel ISimpleObjectModel.ImageModel
		//{
		//	get { return ImageModel; }
		//}

		IList<IValidationRule> ISimpleObjectModel.UpdateValidationRules
		{
			get 
			{
				if (this.iUpdateValidationRules == null)
					this.iUpdateValidationRules = this.UpdateValidationRules.AsCustom<IValidationRule>().AsReadOnly();

				return this.iUpdateValidationRules; 
			}
		}

		IList<IValidationRule> ISimpleObjectModel.DeleteValidationRules
		{
			get
			{
				if (this.iDeleteValidationRules == null)
					this.iDeleteValidationRules = this.DeleteValidationRules.AsCustom<IValidationRule>().AsReadOnly();

				return this.iDeleteValidationRules;
			}
		}

		IDictionary<int, IModelElement> ISimpleObjectModel.ObjectSubTypes
		{
			get { return this.ObjectSubTypes.AsCustom<int, IModelElement>(); }
		}

		IObjectRelationModel ISimpleObjectModel.RelationModel
		{
			get { return this.RelationModel; }
		}

		//IList<GraphElementCreatedAction> ISimpleObjectModel.GraphElementCreatedActions
		//{
		//	get { return this.GraphElementCreatedActions; }
		//}
	}


	public interface ISimpleObjectModel : IModelElement
    {
        //int TableId { get; }

        Type ObjectType { get; }
		PropertyModelCollection<IPropertyModel> PropertyModels { get; }
		IPropertyModel GetPropertyModel(int propertyIndex);

		IPropertyModel IdPropertyModel { get; }
		IPropertyModel NamePropertyModel { get; }
		IPropertyModel DescriptionPropertyModel { get; }
		IPropertyModel ObjectSubTypePropertyModel { get; }
		IPropertyModel PreviousIdPropertyModel { get; }
		IPropertyModel OrderIndexPropertyModel { get; }
		//IPropertyModel ActionSetOrderIndexPropertyModel { get; }

		int[] PropertyIndexes { get; }
		int[] IndexedPropertyIndexes { get; }
		int[] SerializablePropertyIndexes { get; }
		int[] StorablePropertyIndexes { get; }
		int[] TransactionActionPropertyIndexes { get; }
		int TransactionActionLogPropertySequenceId { get; }
		string[] StorableFieldNameIndexes { get; }

		SerializationModel SerializationModel { get; }
		//IPropertyModel[] StorablePropertyModelSequence { get; }
		//int[] StorablePropertyIndexSequence { get; }
		//int StorablePropertySequenceId { get; }
		//int[] PublicPropertyIndexSequence { get; }

		//int[] StorablePropertyIndexes { get; }
		//int StorablePropertyIndexSequenceId { get; }
		//IPropertyModel[] StorablePropertyModels { get; }

		//int TableId { get; }
		//string TableName { get; }
		TableInfo TableInfo { get; }
		string ObjectTypeCaption { get; }
        bool IsStorable { get; }
		//bool IsSortable { get; }
		SortingModel SortingModel { get; }
		bool FetchAllRecords { get; }
        IList<IValidationRule> UpdateValidationRules { get; }
		IList<IValidationRule> DeleteValidationRules { get; }
		bool DeleteSimpleObjectOnLastGraphElementDelete { get; }
        bool DeleteAllSimpleObjectGraphElementsOnOneGraphElementDelete { get; }
		bool MustHaveAtLeastOneGraphElement { get; }
		int MustHaveGraphElementGraphKey { get; }
		
		// Consider remove this
		int SortableOneToManyRelationKey { get; }
		IDictionary<int, IModelElement> ObjectSubTypes { get; }
		IObjectRelationModel RelationModel { get; }
		//IList<GraphElementCreatedAction> GraphElementCreatedActions { get; }
    }
}
