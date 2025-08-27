using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Simple;
using Simple.Collections;
using Simple.Modeling;
using Simple.Datastore;

namespace Simple.Objects
{
	public class SimpleObjectModelDiscovery<TablesInfo> : SimpleObjectModelDiscovery
	{
		public SimpleObjectModelDiscovery(IList<Assembly> assemblies)
			: base(assemblies, typeof(TablesInfo))
		{
		}
	}

	public class SimpleObjectModelDiscovery
	{
		//private IDictionary<string, short> requestKeysByPropertyName;
		//private HashArray<SimpleProperty> propertyRepositoryByPropertyKey = new HashArray<SimpleProperty>();
		//private IDictionary<string, SimpleProperty> propertyRepositoryByPropertyName = null;
		private RelationPolicyModelBase relationPolicyModel;
        private SimpleDictionary<Type, SimpleObjectModel> objectModelsByOriginObjectType;
        private List<Type> notIncludedObjectModelDefinitionTypes = new List<Type>();
        private SimpleDictionary<Type, SimpleObjectModel> objectModelsByObjectModelDefinitionType = new SimpleDictionary<Type, SimpleObjectModel>();
        private SimpleDictionary<Type, SimpleObjectModel> objectModelsByObjectType = new SimpleDictionary<Type, SimpleObjectModel>();   // Key is ObjectType as Type, Value is model of the object, tipically as SimpleObjectModel (but is not necessary).
        private SimpleDictionary<int, SimpleObjectModel> objectModelsByTableId = new SimpleDictionary<int, SimpleObjectModel>();
		private SimpleDictionary<string, SimpleObjectModel> objectModelsByTableName = new SimpleDictionary<string, SimpleObjectModel>();
        private SimpleObjectModel[] objectModelsByTableIdArray;
		private ServerObjectModelInfo[] serverObjectModelsByTableIdArray;
		private SimpleDictionary<int, TableInfo> tableInfosByTableId = new SimpleDictionary<int, TableInfo>();
		//private string[] tableNamesByTableIdArray = null;
		private ModelDictionary<int, SingleGraphPolicyModel> graphPolicyModelsByGraphKey = new ModelDictionary<int, SingleGraphPolicyModel>();
        //private List<SimpleObjectDependencyAction> objectDependencyActions = new List<SimpleObjectDependencyAction>();
        private ICollection<ISimpleObjectModel> objectModelCollection;
		private ServerObjectModelInfo[] allServerObjectModels;
		private static object lockObject = new object();

		public SimpleObjectModelDiscovery(IList<Assembly> assemblies)
			: this(assemblies, ReflectionHelper.FindTopInheritedTypeInAssembly<SystemTablesBase>(assemblies))
		{
		}

        public SimpleObjectModelDiscovery(IList<Assembly> assemblies, Type tablesInfoType)
        {
			lock (lockObject)
			{
				//// -1. Collect PropertyRepository and set global property keys
				//Type propertyRepositoryType = ReflectionHelper.FindTopInheritedTypeInAssembly<SimplePropertyRepository>(assemblies);
				//SimplePropertyRepository propertyRepository = Activator.CreateInstance(propertyRepositoryType) as SimplePropertyRepository;
				//Dictionary<string, FieldInfo> propertyRepositoryFieldInfosByName = ReflectionHelper.GetFieldInfosByName(propertyRepository);
				//int propertyKey = 0;
				//this.propertyRepositoryByPropertyName = new Dictionary<string, SimpleProperty>();

				//foreach (var item in propertyRepositoryFieldInfosByName)
				//{
				//	FieldInfo fieldInfo = item.Value;
				//	object fieldValue = ReflectionHelper.GetFieldObject(fieldInfo, propertyRepository);

				//	if (fieldValue != null && fieldValue is SimpleProperty)
				//	{
				//		string propertyName = item.Key;
				//		SimpleProperty property = (SimpleProperty)fieldValue;

				//		property.Key = propertyKey;
				//		property.Name = propertyName;
				//		fieldInfo.SetValue(propertyRepository, property);

				//		this.propertyRepositoryByPropertyKey[propertyKey++] = property;
				//		this.propertyRepositoryByPropertyName.Add(propertyName, property);
				//	}
				//}

				// 0. Populate all classes that inherit abstract class SimpleObjectTables
				//IEnumerable<Type> tablesInfoTypes = ReflectionHelper.SelectInheritedClasses<SimpleObjectTables>(assemblies);

				//foreach (Type tablesInfoType in tablesInfoTypes)
				//{
				// Type tableNamesMasterClass = ReflectionHelper.FindTopInheritedTypeInAssembly<SimpleObjectTables>(assemblies);

				if (tablesInfoType == null || tablesInfoType.IsAbstract)
					return;

				SystemTablesBase? tablesInfoClass = Activator.CreateInstance(tablesInfoType) as SystemTablesBase;
				//TableBase tablesInfoClass = Activator.CreateInstance(tablesInfoType) as TableBase;

				if (tablesInfoClass is null)
					throw new ArgumentNullException("tablesInfoType mush inherit TableBase class");

				//SimpleObjectTables tableNamesMaster = Activator.CreateInstance(tableNamesMasterClass) as SimpleObjectTables;
				List<FieldInfo> tableInfoClassFields = ReflectionHelper.GetFieldInfos(tablesInfoClass, orderFromBaseType: false);

				foreach (FieldInfo fieldInfo in tableInfoClassFields)
				{
					object fieldValue = ReflectionHelper.GetFieldObject(fieldInfo, tablesInfoClass);

					if (fieldValue != null && fieldValue is TableInfo)
					{
						string tableName = fieldInfo.Name;
						TableInfo tableInfo = (TableInfo)fieldValue;
						TableInfo tableInfoWithSameTableName = this.tableInfosByTableId.Values.FirstOrDefault(item => item.TableName == tableName);

						tableInfo.TableName = tableName;

						if (!tableInfo.IsSystemTable)
						{
							if (tableInfoWithSameTableName.Equals(default(TableInfo)))
							{
								if (tableInfo.TableId <= 0)
									throw new Exception(string.Format("Table definition error: TableId cannot be <= 0, TableName = {0}", tableName));

								if (this.tableInfosByTableId.ContainsKey(tableInfo.TableId))
									throw new Exception(string.Format("Table definition error: Table is already defined, TableId={0}, TableName={1}", tableInfo.TableId, tableName));

								this.tableInfosByTableId.Add(tableInfo.TableId, tableInfo);
							}
							else // The field is duplicate (overridden) and require only update of TableId and TableName field values
							{
								tableInfo.TableId = tableInfoWithSameTableName.TableId;
								tableInfo.TableName = tableInfoWithSameTableName.TableName;
							}
						}

						fieldInfo.SetValue(tablesInfoClass, tableInfo);
					}
				}
				//}

				// 1. Create Object Relation Policy Model Class -> set this.relationPolicyModel
				Type objectRelationModelType = ReflectionHelper.FindTopInheritedTypeInAssembly<RelationPolicyModelBase>(assemblies);
				RelationPolicyModelBase? relationPolicyModelObject = Activator.CreateInstance(objectRelationModelType) as RelationPolicyModelBase;

				if (relationPolicyModelObject is null)
					throw new ArgumentNullException("objectRelationModelType mus inherit RelationPolicyModelBase class");

				this.relationPolicyModel = relationPolicyModelObject;

				//Type objectRelationModelType = null;
				//List<Type> businessApplicationObjectRelationModelTypes = ReflectionHelper.GetInheritedTypesInAssembly(typeof(RelationPolicyModelBase));
				//businessApplicationObjectRelationModelTypes.Remove(typeof(RelationPolicyModelBase));

				//if (businessApplicationObjectRelationModelTypes.Count == 0)
				//{
				//	throw new Exception("Object Relation Model is not found.");
				//}
				////else if (businessApplicationObjectRelationModelTypes.Count == 1)
				////{
				////	objectRelationModelType = businessApplicationObjectRelationModelTypes[0];
				////}
				//else // more than two BusinessApplicationObjectRelationModel classes exists - find the top subclass
				//{
				//	foreach (Type type in businessApplicationObjectRelationModelTypes)
				//	{
				//		bool isTopClass = true;

				//		foreach (Type item in businessApplicationObjectRelationModelTypes)
				//		{
				//			if (item == type)
				//				continue;

				//			if (!type.IsSubclassOf(item))
				//			{
				//				isTopClass = false;
				//				break;
				//			}
				//		}

				//		if (isTopClass)
				//			objectRelationModelType = type;
				//	}
				//}

				//if (objectRelationModelType == null)
				//{
				//	throw new Exception("Object Relation Model is not found.");
				//}
				//else
				//{
				//	this.relationPolicyModel = Activator.CreateInstance(objectRelationModelType) as RelationPolicyModelBase;
				//	//RelationPolicyModelBase.SetInstance(this.RelationPolicyModel);

				//	//PropertyInfo propertyInfo = objectRelationModelType.GetProperty(ReflectionHelper.StrInstance, BindingFlags.Static | BindingFlags.Instance);

				//	//if (propertyInfo != null)
				//	//{
				//	//	object instance = propertyInfo.GetValue(objectRelationModelType, null);

				//	//	if (instance != null)
				//	//	{
				//	//		this.RelationPolicyModel = instance as RelationPolicyModelBase;
				//	//	}
				//	//	else
				//	//	{
				//	//		throw new Exception("Class {0} with Object Reletion Model definition has no static Instance property");
				//	//	}
				//	//}
				//}


				// 2. Create Object Models
				List<Type> reducedObjectModelDefinitionTypes = new List<Type>();
				List<Type> systemModelDefinitionTypes = new List<Type>();
				List<Type> appModelDefinitionTypes = new List<Type>();
				IEnumerable<Type> objectModelDefinitionTypes = ReflectionHelper.SelectInheritedAssemblyTypes(assemblies, typeof(SimpleObjectModel));

				// Get only top inherited classes
				foreach (Type type in objectModelDefinitionTypes)
				{
					Type topInheritedType = ReflectionHelper.FindTopInheritedClass(type, objectModelDefinitionTypes);

					if (!reducedObjectModelDefinitionTypes.Contains(topInheritedType))
						reducedObjectModelDefinitionTypes.Add(topInheritedType);
					else
						this.notIncludedObjectModelDefinitionTypes.Add(type);
				}

				// Split the models into two groups: system and app
				foreach (Type type in reducedObjectModelDefinitionTypes)
				{
					if (this.IsObjectModelClassSystemAssembly(type))
						systemModelDefinitionTypes.Add(type);
					else
						appModelDefinitionTypes.Add(type);
				}

				// Set Base model definition classes first
				this.notIncludedObjectModelDefinitionTypes.Reverse();

				foreach (Type objectModelDefinitionType in notIncludedObjectModelDefinitionTypes)
					this.CreateAndSetObjectModel(objectModelDefinitionType);

				// Than add system object models, 
				systemModelDefinitionTypes.Reverse();

				foreach (Type objectModelDefinitionType in systemModelDefinitionTypes)
					this.CreateAndSetObjectModel(objectModelDefinitionType);

				//// Add an application models at the end to be the last
				//appModelDefinitionTypes.Reverse();

				foreach (Type objectModelDefinitionType in appModelDefinitionTypes)
					this.CreateAndSetObjectModel(objectModelDefinitionType);


				// 2.1 Save original object models by object type
				this.objectModelsByOriginObjectType = this.objectModelsByObjectType.Clone(); // (this.objectModelsByObjectType.AsCustom<SimpleObjectModel>().Clone() as CustomDictionary<Type, SimpleObjectModel>).AsReadOnly();

				// 2.2 Create unique TableId. If not exists tableId is dynamically created
				IEnumerable<int> specifiedTableIds = from objectModel in this.objectModelsByOriginObjectType.Values
													 where objectModel.TableInfo.TableId > 0 //!= TableInfo.Empty.TableId
													 select objectModel.TableInfo.TableId;

				List<int> usedTableIds = new List<int>(specifiedTableIds);
				int tableIdHelper = usedTableIds.Max() / 100 * 100 + 100; // The first number rounded to 100

				foreach (SimpleObjectModel objectModel in this.objectModelsByOriginObjectType.Values)
				{
					if (objectModel.TableInfo.TableId == 0) //.TableId == TableInfo.Empty.TableId)
					{
						while (usedTableIds.Contains(tableIdHelper))
							tableIdHelper++;

						objectModel.TableInfo = new TableInfo(tableIdHelper, objectModel.ObjectType.Name);
						usedTableIds.Add(tableIdHelper);

						this.objectModelsByTableId.Add(tableIdHelper, objectModel);

						if (objectModel.IsStorable)
						{
							//if (objectModel.TableInfo.TableName.IsNullOrEmpty())
								throw new Exception(objectModel.GetType().Name + " has property IsStorable set to True but the TableName property is not specified.");
						}

						this.objectModelsByTableName.Add(objectModel.ObjectType.Name, objectModel);
					}
				}

				// 2.3 Sort objectModelsByTableId by tableId
				List<int> sortedTableIds = this.objectModelsByTableId.Keys.ToList();
				sortedTableIds.Sort();

				SimpleDictionary<int, SimpleObjectModel> sortedObjectModelsByTableId = new SimpleDictionary<int, SimpleObjectModel>(sortedTableIds.Count);

				foreach (int tableIdValue in sortedTableIds)
					sortedObjectModelsByTableId.Add(tableIdValue, this.objectModelsByTableId[tableIdValue]);

				this.objectModelsByTableId = sortedObjectModelsByTableId;

				// 2.4 Sets ObjectRelationModel in SimpleObjectModel definitions
				foreach (SimpleObjectModel objectModel in this.objectModelsByOriginObjectType.Values)
					objectModel.RelationModel = new ObjectRelationModel(objectModel, this.relationPolicyModel);

				// 2.5 Normalize objectModels by TableName & TableId - Set the same model for all object types with the same TableId
				Type[] objectTypeArray = new Type[this.objectModelsByObjectType.Keys.Count];
				this.objectModelsByObjectType.Keys.CopyTo(objectTypeArray, 0);

				foreach (Type objectType in objectTypeArray)
				{
					SimpleObjectModel objectModel = this.objectModelsByObjectType[objectType];

					//if (objectModel is ISimpleObjectModel)
					//{
					//ISimpleObjectModel simpleObjectModel = objectModel as ISimpleObjectModel;

					if (!objectModel.TableInfo.TableName.IsNullOrEmpty())
					{
						SimpleObjectModel modelByTableId = this.objectModelsByTableId[objectModel.TableInfo.TableId];

						if (objectModel != modelByTableId)
							this.objectModelsByObjectType[objectType] = modelByTableId;
					}
					//}
				}

				this.objectModelsByTableIdArray = new SimpleObjectModel[this.objectModelsByTableId.Max(item => item.Key) + 1];
				this.serverObjectModelsByTableIdArray = new ServerObjectModelInfo[this.objectModelsByTableIdArray.Length];
				this.allServerObjectModels = new ServerObjectModelInfo[this.objectModelsByTableId.Count];
				//this.tableNamesByTableIdArray = new string[this.objectModelsByTableIdArray.Length];

				for (int i = 0; i < this.objectModelsByTableId.Count; i++)
				{
					var item = this.objectModelsByTableId.ElementAt(i);
					int tableId = item.Key;
					SimpleObjectModel objectModel = item.Value;
					ServerObjectModelInfo serverObjectModel = this.CreateServerObjectModel(objectModel);

					this.objectModelsByTableIdArray[tableId] = objectModel;
					this.serverObjectModelsByTableIdArray[tableId] = serverObjectModel;
					this.allServerObjectModels[i] = serverObjectModel;
				}


				//foreach (var item in this.objectModelsByTableName)
				//{
				//	if (item.Value is ISimpleObjectModel)
				//	{
				//		ISimpleObjectModel model = this.objectModelsByTableId[(item.Value as ISimpleObjectModel).TableInfo.TableId];

				//		if (item.Value != model)
				//			this.objectModelsByTableName[item.Key] = model;
				//	}
				//}

				//// Include all object model object types in this.objectModelsByObjectType & this.objectModelsByObjectModelDefinitionType
				//foreach (Type type in this.allObjectModelDefinitionTypes)
				//{
				//	ISimpleObjectModel model = this.objectModelsByObjectModelDefinitionType.ContainsKey(type);

				//	if (!this.objectModelsByObjectModelDefinitionType.ContainsKey(type))
				//	{
				//		model = Activator.CreateInstance(type) as ISimpleObjectModel;

				//		if (!this.objectModelsByObjectType.ContainsKey(model.ObjectType))
				//		{
				//			if (!this.objectModelsByTableId.ContainsKey(model.TableInfo.TableId))
				//				model = this.objectModelsByTableId[model.TableInfo.TableId];

				//			this.objectModelsByObjectType.Add(model.ObjectType, model);
				//		}

				//		this.objectModelsByObjectModelDefinitionType.Add(type, model);
				//	}
				//}


				// 3. Collect Graph Policy Models
				IEnumerable<Type> graphPolicyModelTypes = ReflectionHelper.SelectInheritedAssemblyTypes(assemblies, typeof(SingleGraphPolicyModel));

				foreach (Type type in graphPolicyModelTypes)
				{
					if (type != typeof(SingleGraphPolicyModel))
					{
						SingleGraphPolicyModel? graphPolicyModel = Activator.CreateInstance(type) as SingleGraphPolicyModel;

						if (graphPolicyModel is null)
							continue;

						if (this.GraphPolicyModelsByGraphKey.ContainsKey(graphPolicyModel.GraphKey))
							throw new Exception(string.Format("Graph policy model already defined: GraphKey = {0}, Graph Name = {1}", graphPolicyModel.GraphKey.ToString(), graphPolicyModel.Name));

						this.GraphPolicyModelsByGraphKey.Add(graphPolicyModel.GraphKey, graphPolicyModel);
					}
				}

				// 4. Set relation models TableId's
				foreach (SimpleObjectModel objectModel in this.ObjectModels)
				{
					if (objectModel.Owner == null)
						objectModel.Owner = this;

					if (objectModel.ObjectType.Name == "GraphElement")
						objectModel.Owner = objectModel.Owner;

					ObjectRelationModel relationModel = objectModel.RelationModel;

					foreach (var item in relationModel.AsPrimaryObjectInOneToOneRelations)
					{
						item.ForeignObjectTableId = this.GetObjectTableId(item.ForeignObjectType);
						item.PrimaryObjectTableId = this.GetObjectTableId(item.PrimaryObjectType);

						if (relationModel.IsObjectTypeSameOrSubclassOf(objectModel.ObjectType, item.ForeignObjectType))
							relationModel.SetForegnKeyRelationModelsByPropertyIndex(item);
					}

					foreach (var item in relationModel.AsForeignObjectInOneToOneRelations)
					{
						item.ForeignObjectTableId = this.GetObjectTableId(item.ForeignObjectType);
						item.PrimaryObjectTableId = this.GetObjectTableId(item.PrimaryObjectType);
					}

					foreach (var item in relationModel.AsPrimaryObjectInOneToManyRelations)
					{
						item.ForeignObjectTableId = this.GetObjectTableId(item.ForeignObjectType);
						item.PrimaryObjectTableId = this.GetObjectTableId(item.PrimaryObjectType);
					}

					foreach (var item in relationModel.AsForeignObjectInOneToManyRelations)
					{
						item.ForeignObjectTableId = this.GetObjectTableId(item.ForeignObjectType);
						item.PrimaryObjectTableId = this.GetObjectTableId(item.PrimaryObjectType);

						if (relationModel.IsObjectTypeSameOrSubclassOf(objectModel.ObjectType, item.ForeignObjectType))
							relationModel.SetForegnKeyRelationModelsByPropertyIndex(item);
					}

					//// Set customized Guid object keys serialization methods
					//foreach (PropertyModel propertyModel in objectModel.PropertyModels)
					//{
					//	if (propertyModel.IsKey || propertyModel.IsRelationKey)
					//	{
					//		if (propertyModel.PropertyType == typeof(Guid))
					//		{
					//			propertyModel.PropertyTypeId = ObjectKeyGuidTypeId;
					//		}
					//		else if (propertyModel.PropertyType == typeof(Guid?))
					//		{
					//			propertyModel.PropertyTypeId = ObjectKeyNullableGuidTypeId;
					//		}
					//	}
					//}
				}

				//// 5. Set the object property models as require
				//foreach (SimpleObjectModel objectModel in this.ObjectModels)
				//{
				//	foreach (var propertyModel in objectModel.PropertyModels)
				//	{
				//		//propertyModel.PropertyName = propertyModel.PropertyName;
				//		propertyModel.DatastoreFieldName = propertyModel.PropertyName;
				//	}
				//}


				//// Some computer Net.Framework assemblies has an problem when calling assembly.GetTypes() that result in app crash at start.
				//// To avoid app crash, we need alternative method to get all ISimpleObjectModel object models class types.
				////
				//// Discovery object dependency action definitions
				//var objectDependencies = from assembly in AppDomain.CurrentDomain.GetAssemblies()
				//						   from type in assembly.GetTypes()
				//						   where typeof(SimpleObjectDependencyAction).IsAssignableFrom(type) && !type.IsInterface && !type.IsGenericType && !type.IsAbstract
				//						   select type;


					//// 4. Collect Object Dependencies
					//IEnumerable<Type> objectDependencyTypes = ReflectionHelper.SelectInheritedClasses(assemblies, typeof(SimpleObjectDependencyAction));

					////List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
					////List<Type> objectDependencyTypes = new List<Type>();

					////foreach (Assembly assembly in assemblies)
					////{
					////	Type[] types = null;

					////	try
					////	{
					////		types = assembly.GetTypes();
					////	}
					////	catch
					////	{
					////		continue;
					////	}

					////	if (types != null)
					////		foreach (Type type in types)
					////			if (type != null && typeof(SimpleObjectDependencyAction).IsAssignableFrom(type) && !type.IsInterface && !type.IsGenericType && !type.IsAbstract)
					////				objectDependencyTypes.Add(type);
					////}


					//foreach (Type type in objectDependencyTypes)
					//{
					//	if (type != typeof(SimpleObjectDependencyAction))
					//	{
					//		SimpleObjectDependencyAction objectDependencyAction = Activator.CreateInstance(type) as SimpleObjectDependencyAction;
					//		this.objectDependencyActions.Add(objectDependencyAction);
					//	}
					//}


					//foreach (SimpleObjectKey objectKey in this.objectKeys)
					//{
					//	// Add only id's who belongs to this server - for new key generation
					//	if (objectKey.GetClientId() == this.Manager.ClientId && objectKey.GetObjectId() > maxObjectId2)
					//	{
					//		maxObjectId2 = objectKey.GetObjectId();
					//	}
					//}

					//if (maxObjectId != maxObjectId2)
					//	throw new Exception("Alternative in CalculateMaxObjectId function has failed");

					//if (!this.ObjectModel.IsSystemObject)
					//{
					//	//long lastUsedMaxObjectIdByTableId = Conversion.TryChangeType<int>(this.SystemMaxObjectId.MaxObjectId);
					//	this.maxObjectId = Math.Max(this.maxObjectId, this.SystemTableName.MaxObjectId);
					//}
			}
		}

		public ServerObjectModelInfo[] AllServerObjectModels => this.allServerObjectModels;

		internal ICollection<SimpleObjectModel> ObjectModels
		{
			get { return this.objectModelsByTableId.Values; }
		}

		internal RelationPolicyModelBase RelationPolicyModel
		{
			get { return this.relationPolicyModel; }
		}

		//public IList<SimpleObjectDependencyAction> ObjectDependencyActions
  //      {
  //          get { return this.objectDependencyActions.AsReadOnly() ; }
  //      }

		//public SimpleProperty GetGlobalPropertyByPropertyKey(int propertyKey)
		//{
		//	return this.propertyRepositoryByPropertyKey[propertyKey];
		//}

		//public SimpleProperty GetGlobalPropertyByPropertyName(string propertyName)
		//{
		//	return this.propertyRepositoryByPropertyName[propertyName];
		//}

		public SimpleObjectModel? GetObjectModel(int tableId)
        {
			return this.objectModelsByTableIdArray[tableId];

			//if (tableId >= 0 && tableId < this.objectModelsByTableIdArray.Length)
   //           return this.objectModelsByTableIdArray[tableId];

   //         return null;
        }

        public SimpleObjectModel? GetObjectModel(string tableName)
        {
            SimpleObjectModel? result;

            if (this.objectModelsByTableName.TryGetValue(tableName, out result))
                return result;

            return null;
        }

        public SimpleObjectModel? GetObjectModel(Type objectType)
        {
            SimpleObjectModel? result;

            if (this.objectModelsByObjectType.TryGetValue(objectType, out result))
                return result;

            return null;
        }

		public ServerObjectModelInfo GetServerObjectModel(int tableId)
		{
			return this.serverObjectModelsByTableIdArray[tableId];
		}

        //public IDictionary<int, ISimpleObjectModel> GetObjectModelsByTableId()
        //{
        //    return this.objectModelsByTableId.AsReadOnly();
        //}

        public IList<ISimpleObjectModel> GetObjectModelCollection()
        {
            if (this.objectModelCollection == null)
                this.objectModelCollection = this.objectModelsByTableId.AsCustom<int, ISimpleObjectModel>().Values;

            return new List<ISimpleObjectModel>(this.objectModelCollection);
        }



        //public IDictionary<Type, ISimpleObjectModel> GetObjectModelsByObjectType()
        //{
        //    return this.objectModelsByObjectType.AsReadOnly();
        //}

        //public IDictionary<Type, ISimpleObjectModel> GetObjectModelsByObjectModelDefinitionType()
        //{
        //    return this.objectModelsByObjectModelDefinitionType.AsReadOnly();
        //}

		//public string[] GetTableNamesByTableId()
		//{
		//	return this.tableNamesByTableIdArray;
		//}

        public IDictionary<Type, ISimpleObjectModel> GetObjectModelsByOriginObjectType()
        {
            return new CustomDictionary<Type, ISimpleObjectModel>(this.objectModelsByOriginObjectType);
        }

        public IOneToOneRelationModel? GetOneToOneRelationModel(int relationKey)
        {
            OneToOneRelationModel? value;
            
			this.relationPolicyModel.OneToOneRelations.TryGetValue(relationKey, out value);

            return value;
        }

        public IOneToManyRelationModel? GetOneToManyRelationModel(int relationKey)
        {
            OneToManyRelationModel? value;
            
			this.relationPolicyModel.OneToManyRelations.TryGetValue(relationKey, out value);

            return value;
        }

		public IRelationModel? GetRelationModel(int relationKey)
		{
			RelationModel? value;
			
			this.relationPolicyModel.Relations.TryGetValue(relationKey, out value);

			return value;
		}

		public IManyToManyRelationModel? GetManyToManyRelationModel(int manyToManyRelationKey)
        {
            ManyToManyRelationModel? value;
            
			this.relationPolicyModel.ManyToManyRelations.TryGetValue(manyToManyRelationKey, out value);

            return value;
        }

        //public IGraphPolicyModel GetGraphPolicyModel(Graph graph)
        //{
        //    return this.GetGraphPolicyModel(graph.GraphKey);
        //}

        public IGraphPolicyModel? GetGraphPolicyModel(int graphKey)
        {
            SingleGraphPolicyModel? result;
            
			this.GraphPolicyModelsByGraphKey.TryGetValue(graphKey, out result);

            return result;
        }

        private void CreateAndSetObjectModel(Type objectModelDefinitionType)
        {
            if (objectModelDefinitionType == null)
                throw new ArgumentNullException("modelClassType is null.");

            if (!typeof(ISimpleObjectModel).IsAssignableFrom(objectModelDefinitionType))
                throw new Exception("Method SetObjectModel: modelClassType must have ISimpleObjectModel interface implemented.");

            if (this.objectModelsByObjectModelDefinitionType.ContainsKey(objectModelDefinitionType))
                return;

			SimpleObjectModel objectModel = SimpleObjectModel.GetInstance(objectModelDefinitionType);

			//if (objectModelDefinitionType == typeof(SimpleObjectModel))
			//(objectModel as SimpleObjectModel).SetInstanceInternal(objectModel as SimpleObjectModel);

			//if (!(model is EmptyObjectModel) && model.ObjectPropertys != null)


			//PropertyInfo propertyInfo = objectModelDefinitionType.GetProperty(ReflectionHelper.StrInstance);

			//if (propertyInfo != null)
			//{
			//	object instance = propertyInfo.GetValue(objectModelDefinitionType, null);

			//	if (instance != null)
			//	{
			//		objectModel = instance as ISimpleObjectModel;
			//	}
			//	else
			//	{
			//		throw new Exception(String.Format("Class {0} with Object Reletion Model definition has no static Instance property", objectModelDefinitionType.Name));
			//	}
			//}

			//foreach (var propertyModel in objectModel.PropertyModels)
   //             if (propertyModel is PropertyModel)
   //                 (propertyModel as PropertyModel).Owner = objectModel;

            //	this.SetObjectModelDefinition(objectModel, objectModel.ObjectType);
            //}

            //protected internal void SetObjectModelDefinition(ISimpleObjectModel model, Type objectType)
            //{
            this.objectModelsByObjectModelDefinitionType[objectModel.GetType()] = objectModel;
            this.objectModelsByObjectType[objectModel.ObjectType] = objectModel;
            this.OnSetObjectModel(objectModel, objectModel.ObjectType);
        }

        protected ModelDictionary<int, SingleGraphPolicyModel> GraphPolicyModelsByGraphKey
        {
            get { return this.graphPolicyModelsByGraphKey; }
            set { this.graphPolicyModelsByGraphKey = value; }
        }

        protected virtual void OnSetObjectModel(SimpleObjectModel objectModel, Type objectType)
        {
            //base.SetObjectModel(objectModel);

            //if (objectModel is ISimpleObjectModel)
            //{
                //ISimpleObjectModel simpleObjectModel = objectModel as ISimpleObjectModel;

                if (!objectModel.TableInfo.TableName.IsNullOrEmpty())
                {
                    // Update objectModelsByTableId
                    //if (this.objectModelsByTableId.ContainsKey(simpleObjectModel.TableInfo.TableId))
                    //{
                    //	//ISimpleObjectModel oldSimpleObjectModel = this.objectModelsByTableId[simpleObjectModel.TableInfo.TableId];
                    //	//base.SetObjectModel(simpleObjectModel);
                    //	this.objectModelsByTableId[simpleObjectModel.TableInfo.TableId] = simpleObjectModel;
                    //}
                    //else
                    //{
                    //	this.objectModelsByTableId.Add(simpleObjectModel.TableInfo.TableId, simpleObjectModel);
                    //}

                    this.objectModelsByTableId[objectModel.TableInfo.TableId] = objectModel;
                    // Update objectModelsByTableNames
                    this.objectModelsByTableName[objectModel.TableInfo.TableName] = objectModel;

                    //this.allObjectModels.Add(simpleObjectModel);
                }
            //}
        }

		private int GetObjectTableId(Type objectType)
		{
			ISimpleObjectModel? objectModel = this.GetObjectModel(objectType);

			return (objectModel != null) ? objectModel.TableInfo.TableId : 0;
		}

		private bool IsObjectModelClassSystemAssembly(Type objectModelType)
        {
			return objectModelType.Assembly == Assembly.GetExecutingAssembly(); // || objectModelType.Assembly == Assembly.GetExecutingAssembly();
        }

		private ServerObjectModelInfo CreateServerObjectModel(SimpleObjectModel objectModel)
		{
			ServerPropertyInfo[] serverPropertyInfos = new ServerPropertyInfo[objectModel.PropertyModels.Count];

			for (int i = 0; i < serverPropertyInfos.Length; i++)
				serverPropertyInfos[i] = new ServerPropertyInfo(objectModel.PropertyModels.ElementAt(i));

			return new ServerObjectModelInfo(objectModel.TableInfo.TableId, objectModel.ObjectType.Name, objectModel.TableInfo.TableName, serverPropertyInfos.ToArray());
		}
	}
}
