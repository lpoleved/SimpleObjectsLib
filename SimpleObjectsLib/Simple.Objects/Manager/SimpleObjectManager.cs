using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Security.Cryptography;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using Simple;
using Simple.Datastore;
using Simple.Collections;
using Simple.Modeling;
using Simple.Serialization;
using Simple.Compression;
using Simple.Security;
//using Simple.SocketProtocol;

namespace Simple.Objects
{
	public abstract partial class SimpleObjectManager
	{
		#region |   Private Members   |

		private bool isLocalDatastoreInitialized = false;
		//private bool localDatastoreInitializationInProgress = false;
		//private IDictionary<string, short> requestKeysByPropertyName;
		//private RelationPolicyModelBase relationPolicyModel = null;
		//private ReadOnlyDictionary<Type, SimpleObjectModel> objectModelsByOriginObjectType;
		//protected List<Type> notIncludedObjectModelDefinitionTypes = new List<Type>();
		//protected SimpleDictionary<Type, ISimpleObjectModel> objectModelsByObjectModelDefinitionType = new SimpleDictionary<Type, ISimpleObjectModel>();
		//protected SimpleDictionary<Type, ISimpleObjectModel> objectModelsByObjectType = new SimpleDictionary<Type, ISimpleObjectModel>();   // Key is ObjectType as Type, Value is model of the object, tipically as SimpleObjectModel (but is not necessary).
		//private SimpleDictionary<int, ISimpleObjectModel> objectModelsByTableId = new SimpleDictionary<int, ISimpleObjectModel>();
		//private SimpleDictionary<string, ISimpleObjectModel> objectModelsByTableName = new SimpleDictionary<string, ISimpleObjectModel>();
		//private ISimpleObjectModel[] objectModelsByTableIdArray;
		//private ModelDictionary<int, SingleGraphPolicyModel> graphPolicyModelsByGraphKey = null;

		private SimpleObjectModelDiscovery modelDiscovery = null;
		private long clientId = 1;
		private int serverId = 1;
		private ObjectManagerWorkingMode workingMode = ObjectManagerWorkingMode.Server;
		//private SimpleObjectKeyMode simpleObjectKeyMode = SimpleObjectKeyMode.WithoutCreatorServerId;
		private bool deleteTransactionLogIfTransactionSucceeded = true;
		private long minObjectId = 1;
		private bool reuseObjectKeys = true;
		private bool useCompressionForTransactionLogActionData = false;
		private LocalDatastore? localDatastore = null;
		private IVirtualDatastore datastore;
		private IRemoteDatastore? remoteDatastore = null;
		//private ObjectCache objectCache = null;
		//private UserCollection users = null;
		private HashArray<SystemGraph> systemGraphsByGraphKey = new HashArray<SystemGraph>();
		//private HashArray<Graph> graphsByGraphKey = new HashArray<Graph>();
		//private HashArray<Graph> graphsByGraphId = new HashArray<Graph>();
		//private HashArray<SystemRelation> systemRelationKeysByRelationKey = new HashArray<SystemRelation>();
		//private HashArray<ManyToManyRelation> manyToManyRelationsByManyToManyRelationKey = null;
		private HashArray<ObjectCache> objectCachesByTableId = null;
		//private Dictionary<int, SimpleObjectCollection> simpleObjectCollectionsByTableId = new Dictionary<int, SimpleObjectCollection>();
		private ObjectCache? graphElementObjectCache = null;
		//private ObjectCache manyToManyRelationElementCache = null;
		private UniqueKeyGenerator<long> clientTempObjectIdGenerator = new UniqueKeyGenerator<long>(1);
		private ChangeContainer defaultChangeContainer = null;
		private IUser? systemAdmin = null;

		//private ReadOnlyDictionary<Type, SimpleObjectModel> objectModelsByOriginObjectType;
		//private SimpleDictionary<int, ISimpleObjectModel> objectModelsByTableId = new SimpleDictionary<int, ISimpleObjectModel>();
		//private ISimpleObjectModel[] objectModelsByTableIdArray;
		//private SimpleDictionary<string, ISimpleObjectModel> objectModelsByTableName = new SimpleDictionary<string, ISimpleObjectModel>();

		private SystemObjectCollectionByObjectKey<int, SystemGraph> systemGraphs = new SystemObjectCollectionByObjectKey<int, SystemGraph>();
		private SystemObjectCollectionByObjectKey<string, SystemSetting> systemSettings = new SystemObjectCollectionByObjectKey<string, SystemSetting>();
		private SystemObjectCollectionByObjectKey<int, SystemTableInfos> systemTables = new SystemObjectCollectionByObjectKey<int, SystemTableInfos>();
		private SystemObjectCollectionByObjectKey<int, SystemProperty> systemProperties = new SystemObjectCollectionByObjectKey<int, SystemProperty>();
		private SystemObjectCollectionByObjectKey<int, SystemRelation> systemRelations = new SystemObjectCollectionByObjectKey<int, SystemRelation>();

		//private SystemObjectCollectionByObjectKey<int, Graph> graphs = new SystemObjectCollectionByObjectKey<int, Graph>();
		private SystemObjectCollectionByObjectKey<long, SystemServer> systemServers = new SystemObjectCollectionByObjectKey<long, SystemServer>();
		private SystemObjectCollectionByObjectKey<long, SystemClient> systemClients = new SystemObjectCollectionByObjectKey<long, SystemClient>();
		//private SystemObjectCollectionByObjectKey<int, SystemPropertySequence> systemPropertySequences = new SystemObjectCollectionByObjectKey<int, SystemPropertySequence>();
		private SystemObjectCollectionByObjectKey<long, SystemTransaction> systemTransactions = new SystemObjectCollectionByObjectKey<long, SystemTransaction>();
		//private SystemObjectCollectionByObjectKey<long, SystemTransactionAction> systemTransactionActions = new SystemObjectCollectionByObjectKey<long, SystemTransactionAction>();

		private readonly object lockTransaction = new object();
		private readonly object lockRollbackTransaction = new object();
		private readonly object lockObjectCache = new object();
		private readonly object lockGetObjectCollection = new object();
		private readonly object lockMultipleStatusChange = new object();
		private readonly object lockSystemPropertySequences = new object();
		//private readonly object commitChangesRequester = new object();

		private HashSet<Transaction> activeTransactions = new HashSet<Transaction>();

		//private ModelDictionary<int, SingleGraphPolicyModel> graphPolicyModelsByGraphKey = null;
		//private RelationPolicyModelBase relationPolicyModel = null;
		//private Type graphKeyEnumType = null;
		//private Type manyToManyRelationKeyEnumType = null;
		//private bool initializationInProgress = true;
		private int multipleStatusChangesInProgress = 0;
		private List<ImageNameChangeSimpleObjectEventArgs> multipleImageNameChangeSimpleObjectEventArgs = new List<ImageNameChangeSimpleObjectEventArgs>();
		//private HashArray<IPropertySequence> currentServerSerializablePropertySequencesByTableId = new HashArray<IPropertySequence>();
		//private Dictionary<int, IPropertySequence> currentServerSerializablePropertySequencesByTableId = null;
		private Dictionary<byte[], SystemPropertySequence> systemPropertySequencesByValueData = new Dictionary<byte[], SystemPropertySequence>();
		private HashArray<SystemPropertySequence> systemPropertySequencesByPropertySequenceId = new HashArray<SystemPropertySequence>(); // Require
		private HashList<ServerObjectModelInfo> serverObjectPropertyInfoByTableId = new HashList<ServerObjectModelInfo>();

		//private HashArray<SystemPropertySequence> systemPropertySequencesByTypeId = new HashArray<SystemPropertySequence>();

		private SystemTransaction activeTransaction = null;
		private SystemTransaction lastTransaction = null;
		private ForeignClientRequester foreignClientRequester = new ForeignClientRequester();

		//private Dictionary<SimpleObject, TransactionAction> transactionsActionsBySimpleObject = new Dictionary<SimpleObject, TransactionAction>();
		//private Dictionary<SimpleObject, TransactionAction> oldTransactionsActionsBySimpleObject = new Dictionary<SimpleObject, TransactionAction>();

		////private IDictionary<string, short> requestKeysByPropertyName;
		//public static int ObjectKeyGuidTypeId, ObjectKeyNullableGuidTypeId;


		#endregion |   Private Members   |

		#region |   Public Members   |

		public const string DatastoreSettingAppName = "AppName";
		public const string DatastoreSettingDatastoreVersion = "DatastoreVersion";
		public const string DatastoreSettingServerId = "ServerId";

		#endregion |   Public Members   |

		#region |   Constructors and Initialization   |

		//static SimpleObjectManager()
		//{
		//	// Add custom Write/Read object key actions
		//	ObjectKeyGuidTypeId = SerializationWriter.SetNewWriteAction((writer, propertyValue) => writer.WriteGuid((Guid)propertyValue), WriteObjectKeyGuidOptimized, WriteObjectKeyGuidOptimized);
		//	int ObjectKeyGuidTypeId2 = SerializationReader.SetNewReadAction((reader) => reader.ReadGuid(), ReadObjectKeyGuidOptimized, ReadObjectKeyGuidOptimized);

		//	ObjectKeyNullableGuidTypeId = SerializationWriter.SetNewWriteAction((writer, propertyValue) => writer.WriteNullableGuid((Guid?)propertyValue), WriteNullableObjectKeyGuidOptimized, WriteNullableObjectKeyGuidOptimized);
		//	int objectKeyNullableGuidTypeId = SerializationReader.SetNewReadAction((reader) => reader.ReadGuid(), ReadNullableObjectKeyGuidOptimized, ReadNullableObjectKeyGuidOptimized);
		//}

		public SimpleObjectManager(SimpleObjectModelDiscovery modelDiscovery)
		{
			this.modelDiscovery = modelDiscovery;
			this.objectCachesByTableId = new HashArray<ObjectCache>(modelDiscovery.GetObjectModelCollection().Max(item => item.TableInfo.TableId) + 1);
			//this.objectCachesByTableId = new HashArray<ObjectCache>(modelDiscovery.GetObjectModelCollection().Max(item => item.TableInfo.TableId) + 1);
			this.defaultChangeContainer = new ChangeContainer();
			this.defaultChangeContainer.RequireCommitChange += DefaultChangeContainer_RequireCommitChange;
			//this.defaultChangeContainer.CountChange += this.DefaultChangeContainer_TransactionCountChange;
		}

		public SimpleObjectManager(SimpleObjectModelDiscovery modelDiscovery, byte[] cryptoKey, byte[] cryptoIV, PaddingMode paddingModel = PaddingMode.PKCS7)
			: this(modelDiscovery)
		{
			Aes aes = Aes.Create();

			//aes.GenerateKey();
			//aes.GenerateIV();

			//string newKey = Convert.ToBase64String(aes.Key);
			//string newIV = Convert.ToBase64String (aes.IV);

			aes.Key = cryptoKey; // Encoding.UTF8.GetBytes(key);
			aes.IV = cryptoIV;
			aes.Padding = paddingModel;

			this.Encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
			this.Decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

			//RijndaelManaged crypto = new RijndaelManaged();
			////AesManaged crypto = new AesManaged(); 
			////Aes crypto = Aes.Create();

			//crypto.Key = cryptoKey;
			//crypto.IV = initializationVector;
			//this.CryptoBlockSize = crypto.BlockSize;
			//crypto.Padding = paddingModel;

			//this.Encryptor = crypto.CreateEncryptor(cryptoKey, initializationVector);
			//this.Decryptor = crypto.CreateDecryptor(cryptoKey, initializationVector);
		}

		public ServerObjectModelInfo[] AllServerObjectModels => this.ModelDiscovery.AllServerObjectModels;


		//public static void WriteNullableObjectKeyGuidOptimized(ISerializationWriter writer, object propertyValue)
		//{
		//	if (propertyValue == null)
		//	{
		//		writer.WriteBoolean(true);
		//	}
		//	else
		//	{
		//		writer.WriteBoolean(false);
		//		WriteObjectKeyGuidOptimized(writer, propertyValue);
		//	}
		//}

		//public static void WriteObjectKeyGuidOptimized(ISerializationWriter writer, object propertyValue)
		//{
		//	writer.WriteInt32Optimized(((Guid)propertyValue).GetTableId());
		//	writer.WriteInt64Optimized(((Guid)propertyValue).GetClientId());
		//	writer.WriteInt64Optimized(((Guid)propertyValue).GetObjectId());
		//}

		//public static object ReadNullableObjectKeyGuidOptimized(ISerializationReader reader)
		//{
		//	return (reader.ReadBoolean()) ? null : ReadObjectKeyGuidOptimized(reader);
		//}

		//public static object ReadObjectKeyGuidOptimized(ISerializationReader reader)
		//{
		//	return SimpleObject.CreateGuid(reader.ReadInt32Optimized(), reader.ReadInt64Optimized(), reader.ReadInt64Optimized());
		//}


		//public SimpleObjectManager(SimpleObjectModelDiscovery modelDiscovery, IRemoteDatastore remoteDatastore)
		//{
		//	this.workingMode = ObjectManagerWorkingMode.Client;

		//	this.modelDiscovery = modelDiscovery;
		//	this.remoteDatastore = remoteDatastore;
		//	this.datastore = remoteDatastore;
		//	this.localDatastore = null;
		//	this.InitializeGraphs(updateToLocalDatastore: false);

		//	this.InitializeObjectCache(modelDiscovery);
		//}

		//public SimpleObjectManager(SimpleObjectModelDiscovery modelDiscovery, LocalDatastore localDatastore)
		//      {
		//	this.workingMode = ObjectManagerWorkingMode.Server;

		//	this.modelDiscovery = modelDiscovery;
		//          this.localDatastore = localDatastore;
		//	this.datastore = this.localDatastore;
		//	this.remoteDatastore = null;

		//	if (this.localDatastore != null && this.localDatastore.Connected)
		//		this.InitializeLocalDatastore(); // InitializeGraphs included

		//	this.InitializeObjectCache(modelDiscovery);
		//}



		//protected override void ProcessNotIncludedObjectModelTypes(List<Type> notIncludedObjectModelTypes)
		//{
		//	base.ProcessNotIncludedObjectModelTypes(notIncludedObjectModelTypes);

		//	foreach (Type type in notIncludedObjectModelTypes)
		//	{
		//		ISimpleObjectModel model = Activator.CreateInstance(type) as ISimpleObjectModel;

		//		if (!this.objectModelsByObjectType.ContainsKey(model.ObjectType))
		//		{
		//			if (!this.objectModelsByTableId.ContainsKey(model.TableInfo.TableId))
		//				model = this.objectModelsByTableId[model.TableInfo.TableId];

		//			this.objectModelsByObjectType.Add(model.ObjectType, model);
		//		}
		//	}
		//}

		//public void InitializeDatastore()
		//{
		//	switch (this.WorkingMode)
		//	{
		//		case ManagerWorkingMode.Server:
		//			this.InitializeLocalDatastore();
		//			break;

		//		case ManagerWorkingMode.Client:
		//			this.Datastore.in
		//	}
		//}

		public virtual void InitializeLocalDatastore()
		{
			if (this.isLocalDatastoreInitialized)
				return;

			//this.localDatastoreInitializationInProgress = true;

			//// Enforce SystemObject initialization in object cache
			//bool isNull = (this.objectCache == null);

			//this.systemSettings = new SystemSimpleObjectCache<SystemSetting>(this);
			//this.systemTableNames = new SystemSimpleObjectCache<SystemTableName>(this);
			//this.systemServers = new SystemSimpleObjectCache<SystemServer>(this);
			//this.systemClients = new SystemSimpleObjectCache<SystemClient>(this);
			//this.systemTransactions = new SystemSimpleObjectCache<SystemTransaction>(this);

			this.systemSettings.Load(this);
			this.systemTables.Load(this);
			this.systemProperties.Load(this);
			this.systemRelations.Load(this);
			this.systemServers.Load(this);
			this.systemClients.Load(this);
			//this.systemPropertySequences.Load(this);
			this.systemTransactions.Load(this);
			this.systemGraphs.Load(this);

			// Remove non existing models in datastore
			IEnumerable<SystemTableInfos> systemTableNamesForDelete = from systemTableName in this.SystemTables.Values
																 where this.GetObjectModelSafely(systemTableName.TableId) == null
																 select systemTableName;



			//foreach (SystemTable systemTableName in new List<SystemTable>(systemTableNamesForDelete)) <- AVOID DELETING RECORDS FOR NOW
			//	systemTableName.Delete();



			// Insert new SystemTableNames & update existing TableName fields in datastore
			foreach (ISimpleObjectModel objectModel in this.ModelDiscovery.GetObjectModelCollection())
			{
				if (!objectModel.IsStorable)
					continue;

				SystemTableInfos systemTableName;

				if (!this.SystemTables.TryGetValue(objectModel.TableInfo.TableId, out systemTableName))
				{
					systemTableName = new SystemTableInfos(this, ref this.systemTables, objectModel.TableInfo.TableId, objectModel.TableInfo.TableName);
					systemTableName.Save();
				}
				else if (systemTableName.TableName != objectModel.TableInfo.TableName)
				{
					systemTableName.TableName = objectModel.TableInfo.TableName;
					systemTableName.Save();
				}
			}

			// Check and set object property TypeId's
			foreach (ISimpleObjectModel objectModel in this.ModelDiscovery.GetObjectModelCollection())
			{
				if (!objectModel.IsStorable)
					continue;

				foreach (IPropertyModel propertyModel in objectModel.PropertyModels)
				{
					var systemProperties = from systemProperty in this.systemProperties.Values
										   where systemProperty.TableId == objectModel.TableInfo.TableId && systemProperty.PropertyIndex == propertyModel.PropertyIndex
										   orderby systemProperty.CreationTime ascending
										   select systemProperty;

					if (systemProperties.Count() == 0 || systemProperties.Last().PropertyTypeId != propertyModel.PropertyTypeId)
					{
						SystemProperty systemProperty = new SystemProperty(this, ref this.systemProperties, objectModel.TableInfo.TableId, DateTime.Now, propertyModel.PropertyTypeId, propertyModel.PropertyIndex, propertyModel.PropertyName);
						systemProperty.Save();
					}
					else if (systemProperties.Count() > 0 && systemProperties.Last().PropertyName != propertyModel.PropertyName)
					{
						SystemProperty systemProperty = systemProperties.Last();

						systemProperty.PropertyName = propertyModel.PropertyName;
						systemProperty.Save(); // Update property name.
					}
				}
			}

			//// Update Default Client in database
			//SystemClient systemClient;

			//if (!this.SystemClients.TryGetValue(this.clientId, out systemClient))
			//{
			//	systemClient = new SystemClient(this, ref this.systemClients, this.clientId, "Default", "Local");
			//	systemClient.Save();
			//}

			// Create Graph Collection: Insert new Graphs & update existing Graphs fields in datastore
			this.InitializeSystemGraphs(updateToLocalDatastore: true);
			//this.InitializeGraphs(updateToLocalDatastore: true);

			// Enforce Graph RootGraphElements collection creation to auto correct sorting if any irregularity
			//this.InitializeRootGraphElementCollection();

			// Enforce GraphElement fetching from datastore to object cache and Graph.GraphElements creation
			this.graphElementObjectCache ??= this.GetObjectCache<GraphElement>();

			//// Create RootGraphElementCollections
			//foreach (Graph graph in this.graphs.Values)
			//	graph.CreateRootGraphElementCollection();

			// Enforce ManyToManyRelationElement fetching from datastore to object cache and ManyToManyRelation.ManyToManyRelationElement creation
			//this.CreateManyToManyHashArray();
			this.InitializeSystemRelations(updateToLocalDatastore: true); // Currently only many to many relation keys are added using GetManyToManyRelationKeyEnumType to get enum type values 
																		  //this.manyToManyRelationElementCache = this.GetObjectCache<GroupMembershipElement>();

			////
			//// Populate systemPropertySequencesByValueData dictionary
			////
			//foreach (SystemPropertySequence systemPropertySequence in this.SystemPropertySequences.Values)
			//{
			//	this.systemPropertySequencesByValueData.Add(systemPropertySequence.ValueData, systemPropertySequence);
			//	this.systemPropertySequencesByPropertySequenceId[systemPropertySequence.PropertySequenceId] = systemPropertySequence;
			//}

			//// Set current Property Index Sequence in property models
			////this.currentServerSerializablePropertySequencesByTableId = new HashArray<IPropertySequence>();

			//foreach (SimpleObjectModel objectModel in this.ModelDiscovery.ObjectModels)
			//{
			//	//SystemPropertySequence serializablePropertySequence = this.GetSystemPropertySequence(objectModel.SerializablePropertySequence.PropertyIndexes,
			//	//																				   objectModel.SerializablePropertySequence.PropertyTypeIds);
			//	//SystemPropertySequence storablePropertySequence	  = this.GetSystemPropertySequence(objectModel.StorablePropertySequence.PropertyIndexes,
			//	//													  							   objectModel.StorablePropertySequence.PropertyTypeIds);
			//	int[] transactionActionLogPropertyTypeIds = new int[objectModel.TransactionActionLogPropertyIndexes.Length];

			//	for (int i = 0; i < objectModel.TransactionActionLogPropertyIndexes.Length; i++)
			//	{
			//		int propertyIndex = objectModel.TransactionActionLogPropertyIndexes[i];
			//		IPropertyModel propertyModel = objectModel.PropertyModels[propertyIndex];
			//		int propertyTypeId = propertyModel.PropertyTypeId;

			//		transactionActionLogPropertyTypeIds[i] = propertyTypeId;
			//	}

			//	SystemPropertySequence storableSerializablePropertySequence = this.GetSystemPropertySequence(objectModel.TransactionActionLogPropertyIndexes, transactionActionLogPropertyTypeIds);

			//	//objectModel.SerializablePropertySequence.PropertySequenceId = serializablePropertySequence.PropertySequenceId;
			//	//objectModel.StorablePropertySequence.PropertySequenceId = storablePropertySequence.PropertySequenceId;
			//	objectModel.TransactionActionLogPropertySequenceId = storableSerializablePropertySequence.PropertySequenceId;
			//	//this.erverSerializablePropertySequencesByTableId[objectModel.TableInfo.TableId] = objectModel.ServerSerializablePropertySequence);
			//}

			////
			//// End SystemPropertySequences handling
			////

			//// Sets the object model default property values
			//foreach (SimpleObjectModel objectModel in this.ModelDiscovery.ObjectModels)
			//{
			//	if (objectModel.ObjectType.IsAbstract)
			//		continue;

			//	SimpleObject simpleObjectInstance = this.CreateNewEmptyObject(objectModel.ObjectType, isNew: false);

			//	foreach (var propertyModel in objectModel.PropertyModels)
			//		propertyModel.DefaultValue = simpleObjectInstance.GetPropertyValue(propertyModel);

			//	simpleObjectInstance.Dispose();
			//	simpleObjectInstance = null;
			//}

			//// Calculate maxSystemTransactionId
			//if (SystemTransaction.Collection.Count() > 0)
			//	this.maxSystemTransactionId = this.systemTransactions.Collection.Max(st => st.Id);

			//foreach (SimpleObjectKey objectKey in manyToManyRelationElementCache.SimpleObjectKeys)
			//{
			//    GraphElement graphElement = manyToManyRelationElementCache.GetSimpleObject(objectKey) as GraphElement;
			//}

			//// Find not completed Transactions and undo

			//SimpleObjectTypeCache transactionCache = this.ObjectCache.GetObjectTypeCache<Transaction>();
			//IList<SimpleObjectKey> notCompletedTransactionObjectKeys = transactionCache.CacheRecords(new WhereCriteriaElement(TransactionModel.PropertyModel.Status.Name, (int)TransactionStatus.Completed, WhereComparator.NotEqual));



			//// Rollback from last one not finished transaction
			//for (int i = notCompletedTransactionObjectKeys.Count - 1; i >= 0; i--)
			//{
			//    SimpleObjectKey objectKey = notCompletedTransactionObjectKeys[i];
			//    Transaction transaction = transactionCache.GetSimpleObject(objectKey) as Transaction;

			// Rollback uncompleted transactions - from last not finished to the firs
			//MessageBox.Show("for (int i = this.Transactions.Count - 1; i >= 0; i--)", "InitializeDatastore Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
			SystemTransaction[] transactions = this.SystemTransactions.Values.ToArray();

			for (int i = transactions.Length - 1; i >= 0; i--)
			{
				SystemTransaction transaction = transactions[i];

				if (transaction.IsDeleted)
					continue;

				if (transaction.Status == TransactionStatus.Started) // Transaction is started but not completed -> rollback transaction
				{
					this.RollbackTransaction(transaction, requester: this);
					transaction.Save();

					// No delete for transaction record, until Manager server start again
				}
				else if (transaction.Status == TransactionStatus.Rollbacked)
				{
					transaction.Delete();
				}
			}

			//this.localDatastoreInitializationInProgress = false;
			this.isLocalDatastoreInitialized = true;
		}

		private IEnumerable<SystemProperty> GetObjectPropertiesByPropertyIndex(int tableId, int propertyIndex)
		{
			return from systemProperty in this.systemProperties.Values
				   where systemProperty.TableId == tableId && systemProperty.PropertyIndex == propertyIndex
				   orderby systemProperty.CreationTime descending
				   select systemProperty;
		}

		// Required for Transaction Rollback when deserializing data
		private int GetPropertyTypeId(int tableId, int propertyIndex, DateTime time)
		{
			IEnumerable<SystemProperty> properties = from systemProperty in this.systemProperties.Values
													 where systemProperty.TableId == tableId && systemProperty.PropertyIndex == propertyIndex
													 orderby systemProperty.CreationTime ascending
													 select systemProperty;

			SystemProperty property = properties.First();

			foreach (var item in properties)
			{
				if (item.CreationTime > time) // Newer
					break;

				property = item;
			}

			return property.PropertyTypeId;
		}

		private void InitializeSystemGraphs(bool updateToLocalDatastore)
		{
			// Create Graph Collection: Insert new Graphs & update existing Graphs fields in datastore
			if (this.GetGraphKeyEnumType() != null)
			{
				Array graphKeyEnumValues = Enum.GetValues(this.GetGraphKeyEnumType());
				int maxGraphKey = 0;

				foreach (var enumValue in graphKeyEnumValues)
					maxGraphKey = Math2.Max(maxGraphKey, (int)enumValue);

				this.systemGraphsByGraphKey = new HashArray<SystemGraph>(maxGraphKey + 1);

				foreach (object enumValue in graphKeyEnumValues)
				{
					int graphKey = Conversion.TryChangeType<int>(enumValue);
					string? name = Enum.GetName(this.GetGraphKeyEnumType(), graphKey);

					SystemGraph? systemGraph;

					if (!this.SystemGraphs.TryGetValue(graphKey, out systemGraph))
					{
						systemGraph = new SystemGraph(this, ref this.systemGraphs, graphKey, name);

						if (updateToLocalDatastore)
							systemGraph.Save();
					}
					else if (systemGraph.Name != name)
					{
						systemGraph.Name = name;

						if (updateToLocalDatastore)
							systemGraph.Save();
					}

					this.systemGraphsByGraphKey[graphKey] = systemGraph;

					if (!this.systemGraphs.ContainsKey(graphKey))
						this.systemGraphs.Add(graphKey, systemGraph);
				}
			}
		}

		private void InitializeSystemRelations(bool updateToLocalDatastore)
		{
			Dictionary<int, string> relationNamesByRelationKey = new Dictionary<int, string>();

			if (!this.ModelDiscovery.RelationPolicyModel.Relations.ContainsKey(0))
				relationNamesByRelationKey.Add(0, "Empty");

			foreach (RelationModel relationModel in this.ModelDiscovery.RelationPolicyModel.Relations.Values)
			{
				int relationKey = relationModel.RelationKey;
				string relationName = relationModel.Name;

				relationNamesByRelationKey.Add(relationKey, relationName);

				SystemRelation element = this.systemRelations.Values.FirstOrDefault(rk => rk.RelationKey == relationKey);

				if (element == null) // Insert new one
				{
					element = new SystemRelation(this, ref this.systemRelations, relationKey, relationName);

					if (updateToLocalDatastore)
						element.Save();
				}
				else if (element.Name != relationName) // Only update existing
				{
					element.Name = relationName;

					if (updateToLocalDatastore)
						element.Save();
				}

				//this.systemRelationKeysByRelationKey[relationKey] = element;
			}

			// Delete existing graphs in database, not exists in enum definition
			IEnumerable<SystemRelation> relationKeysForDelete = from relationKey in this.SystemRelations.Values
																where !relationNamesByRelationKey.Keys.Contains(relationKey.RelationKey)
																select relationKey;

			foreach (SystemRelation item in relationKeysForDelete) //new List<SystemTable>(graphsForDelete))
				item.Delete();
		}

		//private void CreateManyToManyHashArray()
		//{
		//	if (this.GetManyToManyRelationKeyEnumType() == null)
		//		return;

		//	Array enumValues = Enum.GetValues(this.GetManyToManyRelationKeyEnumType());
		//	int maxEnumValue = 0;

		//	foreach (var enumValue in enumValues)
		//		maxEnumValue = Math.Max(maxEnumValue, Conversion.TryChangeType<int>(enumValue));

		//	this.manyToManyRelationsByManyToManyRelationKey = new HashArray<ManyToManyRelation>(maxEnumValue + 1);

		//	foreach (object enumValue in Enum.GetValues(this.GetManyToManyRelationKeyEnumType()))
		//	{
		//		int key = Conversion.TryChangeType<int>(enumValue);
		//		string name = Enum.GetName(this.GetManyToManyRelationKeyEnumType(), key);

		//		if (key > 0)
		//		{
		//			ManyToManyRelation manyToManyRelation = new ManyToManyRelation(this, key, name);
		//			this.manyToManyRelationsByManyToManyRelationKey[key] = manyToManyRelation;
		//		}
		//	}
		//}

		//private void InitializeGraphs(bool updateToLocalDatastore)
		//{
		//	Type graphKeyEnumType = this.GetGraphKeyEnumType();

		//	// Create Graph Collection: Insert new Graphs & update existing Graphs fields in datastore
		//	if (graphKeyEnumType != null)
		//	{
		//		Dictionary<int, string> graphNamesByGraphKey = new Dictionary<int, string>();
		//		Array graphKeyEnumValues = Enum.GetValues(graphKeyEnumType);
		//		int maxGraphKey = 0;

		//		foreach (var enumValue in graphKeyEnumValues)
		//		{
		//			int graphKey = Conversion.TryChangeType<int>(enumValue);
		//			string graphName = Enum.GetName(graphKeyEnumType, graphKey);

		//			graphNamesByGraphKey.Add(graphKey, graphName);
		//			maxGraphKey = Math.Max(graphKey, maxGraphKey);
		//		}

		//		// Delete existing graphs in database, not exists in enum definition
		//		IEnumerable<Graph> graphsForDelete = from graph in this.Graphs
		//											 where !graphNamesByGraphKey.Keys.Contains(graph.GraphKey)
		//											 select graph;

		//		foreach (Graph graphItem in graphsForDelete) //new List<SystemTable>(graphsForDelete))
		//		{
		//			graphItem.RequestDelete();

		//			var result = this.CommitChanges();

		//			if (!result.TransactionSucceeded || !result.ErrorDescription.IsNullOrEmpty())
		//				Debug.WriteLine("SimpleObjectManager.InitializeGraphs: " + result.ErrorDescription + " (Name=" + graphItem.Name + ")");

		//			// If graph cannot be deleted, due to existing GraphElements, cancel deleting
		//			if (!result.TransactionSucceeded)
		//				graphItem.CancelDeleteRequest();
		//		}

		//		this.graphsByGraphKey = new HashArray<Graph>(maxGraphKey + 1);

		//		foreach (var graphItem in graphNamesByGraphKey)
		//		{
		//			int graphKey = graphItem.Key;
		//			string graphName = graphItem.Value;

		//			Graph graph = this.Graphs.FirstOrDefault(item => item.GraphKey == graphKey);

		//			//if (graph == null && graphKey == 0)
		//			//	graph = Graph.Empty;

		//			if (graph == null) // Insert new one
		//			{
		//				graph = new Graph(this);
		//				graph.GraphKey = graphKey;
		//				graph.Name = graphName;

		//				if (updateToLocalDatastore)
		//				{
		//					var result = this.CommitChanges();

		//					if (!result.TransactionSucceeded || !result.ErrorDescription.IsNullOrEmpty())
		//						Debug.WriteLine("SimpleObjectManager.InitializeGraphs: " + result.ErrorDescription + " (Name=" + graph.Name + ")");
		//				}
		//			}
		//			else if (graph.Name != graphName) // Only update existing
		//			{
		//				graph.Name = graphName;

		//				if (updateToLocalDatastore)
		//				{
		//					var result = this.CommitChanges();

		//					if (!result.TransactionSucceeded || !result.ErrorDescription.IsNullOrEmpty())
		//						Debug.WriteLine("SimpleObjectManager.InitializeGraphs: " + result.ErrorDescription + " (Name=" + graph.Name + ")");
		//				}
		//			}

		//			graph.AcceptChanges();

		//			this.graphsByGraphKey[graphKey] = graph;

		//			//if (this.Graphs.FirstOrDefault(item => item.GraphKey == graphKey) == null)
		//			//	this.graphs.Add(graphKey, graph);
		//		}
		//	}
		//}


		private void InitializeRelations_OLD() // Only for ManyToMany relations based on manyToManyRelationEnumType enum (this.GetManyToManyRelationKeyEnumType())
		{
			// The relation Keys are currently populated with the many to many relation enum type. Required for ManyToManyElement collection tha has field RelationKey
			Type manyToManyRelationEnumType = this.GetManyToManyRelationKeyEnumType();

			// Create Graph Collection: Insert new Graphs & update existing Graphs fields in datastore
			if (manyToManyRelationEnumType != null)
			{
				Dictionary<int, string> relationNamesByRelationKey = new Dictionary<int, string>();
				Array manyToManyRelationEnumValues = Enum.GetValues(manyToManyRelationEnumType);
				int maxRelationKey = 0;

				foreach (var enumValue in manyToManyRelationEnumValues)
				{
					int relationKey = Conversion.TryChangeType<int>(enumValue);
					string name = Enum.GetName(manyToManyRelationEnumType, relationKey);

					relationNamesByRelationKey.Add(relationKey, name);
					maxRelationKey = Math.Max(relationKey, maxRelationKey);
				}

				// Delete existing graphs in database, not exists in enum definition
				IEnumerable<SystemRelation> relationKeysForDelete = from relationKey in this.SystemRelations.Values
																	where !relationNamesByRelationKey.Keys.Contains(relationKey.RelationKey)
																	select relationKey;

				foreach (SystemRelation item in relationKeysForDelete) //new List<SystemTable>(graphsForDelete))
					item.Delete();

				foreach (var item in relationNamesByRelationKey)
				{
					int relationKey = item.Key;
					string relationName = item.Value;

					SystemRelation element = this.systemRelations.Values.FirstOrDefault(rk => rk.RelationKey == relationKey);

					//if (graph == null && graphKey == 0)
					//	graph = Graph.Empty;

					if (element == null) // Insert new one
					{
						element = new SystemRelation(this, ref this.systemRelations, relationKey, relationName);
						element.Save();
					}
					else if (element.Name != relationName) // Only update existing
					{
						element.Name = relationName;
						element.Save();
					}

					//this.systemRelationKeysByRelationKey[relationKey] = element;

					//if (this.Graphs.FirstOrDefault(item => item.GraphKey == graphKey) == null)
					//	this.graphs.Add(graphKey, graph);
				}
			}
		}


		///// <summary>
		///// Enforce Graph RootGraphElements collection creation that auto correct sorting if any irregularity
		///// </summary>
		//private void InitializeRootGraphElementCollection()
		//{
		//	foreach (Graph graph in this.Graphs)
		//	{
		//		var rootGraphElementsToInitialize = graph.GraphElements;
		//	}
		//}

		//private void InitializeObjectCache(SimpleObjectModelDiscovery modelDiscovery)
		//{
		//	this.objectCachesByTableId = new HashArray<ObjectCache>(modelDiscovery.GetObjectModelCollection().Max(item => item.TableInfo.TableId) + 1);
		//}

		#endregion |   Constructors and Initialization   |

		#region |   Events   |

		public event GroupMembershipRequesterEventHandler? GroupMembershipJoined;
		public event GroupMembershipRequesterEventHandler? GroupMembershipDisjoined;
		public event GraphElementRequesterEventHandler? NewGraphElementCreated;
		public event BeforeChangeParentGraphElementRequesterEventHandler? BeforeGraphElementParentChange;
		public event ChangeParentGraphElementRequesterEventHandler? GraphElementParentChange;
		public event RelationForeignObjectSetEventHandler? RelationForeignObjectSet;
		public event ChangeOrderIndexSimpleObjectRequesterEventHandler? OrderIndexChange;
		public event ValidationInfoEventHandler? ValidationInfo;
		//public event ValidationEventHandler DeleteValidation;
		public event TransactionDatastoreActionRequesterEventHandler? TransactionStarted;
		public event TransactionDatastoreActionRequesterEventHandler? TransactionFinished;
		//public event TransactionInfoEventHandler? TransactionSucceeded;
		public event EventHandler? UpdateStarted;
		public event EventHandler? UpdateEnded;
		public event ChangeObjectIdSimpleObjectEventHandler? ObjectIdChange;
		public event SimpleObjectEventHandler? ActiveEditorsPushObjectData;

		#endregion |   Events   |

		#region |   Public Properties   |

		//     public int ServerId
		//     {
		//         get { return this.serverID; }
		//set { this.serverID = value; }
		//     }
		public SimpleObjectModelDiscovery ModelDiscovery
		{
			get { return this.modelDiscovery; }
		}

		public int ServerId
		{
			get
			{
				return this.serverId;
				//SystemSetting serverIdSystemSettings = this.GetDatastoreSystemSetting(DatastoreSettingNameServerId);

				//if (serverIdSystemSettings != null && serverIdSystemSettings.Value != null)
				//{
				//	return Conversion.TryChangeType<int>(serverIdSystemSettings.Value);
				//}
				//else
				//{
				//	return 0;
				//}
			}
			set
			{
				this.serverId = value;
				//this.SetDatastoreSystemSetting(DatastoreSettingNameServerId, value);
			}
		}

		public long ClientId
		{
			get { return this.clientId; }
			//protected set { this.clientId = value; }
		}

		public ObjectManagerWorkingMode WorkingMode
		{
			get { return this.workingMode; }
			//protected set { this.workingMode = value; }
		}

		//public Dictionary<int, IPropertySequence> CurrentServerPropertySequencesByTableId
		//{
		//	get { return this.currentServerSerializablePropertySequencesByTableId; }
		//}

		//public SimpleObjectKeyMode SimpleObjectKeyMode
		//{
		//	get { return this.simpleObjectKeyMode; }
		//	protected set { this.simpleObjectKeyMode = value; }
		//}


		public LocalDatastore? LocalDatastore
		{
			get { return this.localDatastore; }
		}

		public IVirtualDatastore Datastore
		{
			get { return this.datastore; }
		}

		public IRemoteDatastore? RemoteDatastore
		{
			get { return this.remoteDatastore; }
		}


		//public ObjectCache ObjectCache
		//{
		//    get { return this.objectCache; }
		//}

		//public UserCollection Users
		//      {
		//          get 
		//          {
		//              if (this.users == null)
		//                  this.users = new UserCollection(this, SystemAdminUsername);

		//              return this.users; 
		//          }
		//      }

		public ChangeContainer DefaultChangeContainer
		{
			get
			{
				lock (this.lockTransaction)
				{
					return this.defaultChangeContainer;
				}
			}
		}

		//public IEnumerable<Graph> Graphs
		//{
		//    get { return this.graphsByGraphKey.Collection; }
		//}

		//public IEnumerable<ManyToManyRelation> ManyToManyRelations
		//{
		//    get { return this.manyToManyRelationsByManyToManyRelationKey.Collection; }
		//}



		public string DatastoreVersion
		{
			get
			{
				string result = String.Empty;

				if (this.WorkingMode != ObjectManagerWorkingMode.Client)
				{
					SystemSetting datastoreVersionSystemSettings = this.GetDatastoreSystemSetting(DatastoreSettingDatastoreVersion);

					result = datastoreVersionSystemSettings?.Value?.ToString() ?? String.Empty;
				}

				return result;
			}
		}


		public long MinObjectId
		{
			get { return this.minObjectId; }
			set { this.minObjectId = value; }
		}

		public bool ReuseObjectKeys
		{
			get { return this.reuseObjectKeys; }
			set { this.reuseObjectKeys = value; }
		}

		public bool DeleteTransactionLogIfTransactionSucceeded
		{
			get { return this.deleteTransactionLogIfTransactionSucceeded; }
			set { this.deleteTransactionLogIfTransactionSucceeded = value; }
		}

		public bool UseCompressionForTransactionLogActionData
		{
			get { return this.useCompressionForTransactionLogActionData; }
			set { this.useCompressionForTransactionLogActionData = value; }
		}

		//public SimpleObjectCollection<Graph> Graphs
		//{
		//	get { return this.GetObjectCache(GraphModel.TableId).SelectAll<Graph>(); }
		//}

		public IUser? SystemAdmin
		{
			get
			{
				if (this.systemAdmin == null)
				{
					int tableId = this.GetUserModelTableId();
					this.systemAdmin = this.GetObjectCache(tableId)?.GetObject(objectId: 1) as IUser;
					//this.systemAdmin = this.Users.FirstOrDefault(item => item.Username == SystemAdminUsername);
				}

				return this.systemAdmin;
			}
		}

		protected IUser? GetUser(string username)
		{
			int tableId = this.GetUserModelTableId();

			if (this.GetObjectCache(tableId) is ServerObjectCache serverObjectCache)
				return serverObjectCache.FindFirst(so => (so as IUser)?.Username == username) as IUser;

			return default;
		}

		//public new static SimpleObjectManager Instance
		//      {
		//          get { return GetInstance<SimpleObjectManager>(); }
		//      }

		#endregion |   Public Properties   |

		#region |   Protected Properties   |

		//protected Dictionary<Type, EditPanelPolicyModel> EditPanelPolicyModelsByObjectType
		//{
		//	get { return this.editPanelPolicyModelsByObjectType; }
		//	set { this.editPanelPolicyModelsByObjectType = value; }
		//}

		//protected virtual object CreateAdministratorCollection()
		//{
		//    return new AdministratorCollection<SimpleObjectManager, Administrator>(this, SystemAdminUsername);
		//}
		protected IDictionary<string, SystemSetting> SystemSettings
		{
			get { return this.systemSettings.AsReadOnly(); }
		}

		protected IDictionary<int, SystemTableInfos> SystemTables
		{
			get { return this.systemTables.AsReadOnly(); }
		}

		protected IDictionary<int, SystemRelation> SystemRelations
		{
			get { return this.systemRelations.AsReadOnly(); }
		}

		protected IDictionary<long, SystemServer> SystemServers
		{
			get { return this.systemServers.AsReadOnly(); }
		}

		protected IDictionary<long, SystemClient> SystemClients
		{
			get { return this.systemClients.AsReadOnly(); }
		}

		//protected IDictionary<int, SystemPropertySequence> SystemPropertySequences
		//{
		//	get { return this.systemPropertySequences.AsReadOnly(); }
		//}

		protected IDictionary<long, SystemTransaction> SystemTransactions
		{
			get { return this.systemTransactions.AsReadOnly(); }
		}

		protected IDictionary<int, SystemGraph> SystemGraphs
		{
			get { return this.systemGraphs.AsReadOnly(); }
		}

		public SystemTransaction ActiveTransaction
		{
			get { return this.activeTransaction; }
			private set { this.activeTransaction = value; }
		}


		//protected List<SystemTransactionAction> CurrentTransactionActions
		//{
		//	get { return this.curentTransactionActions; }
		//	set { this.curentTransactionActions = value; }
		//}

		//protected SimpleObjectCollection<SystemTransaction> Transactions
		//{
		//	get
		//	{
		//		if (this.transactions == null)
		//			this.transactions = this.ObjectCache.GetObjectCollection<SystemTransaction>();

		//		return this.transactions;
		//	}
		//}


		#endregion |   Protected Properties   |

		#region |   Internal Properties   |

		internal UniqueKeyGenerator<long> ClientTempObjectIdGenerator
		{
			get { return this.clientTempObjectIdGenerator; }
		}

		#endregion |   Internal Properties   |

		#region |   Private Properties   |

		private ObjectCache GraphElementObjectCache => this.graphElementObjectCache ??= this.GetObjectCache(GraphElementModel.TableId);

		#endregion |   Private Properties   |

		#region |   Private Methods   |

		//private SystemPropertySequence GetSystemPropertySequence(IPropertyModel[] propertyModelSequence)
		//{
		//	int[] propertyIndexSequence = new int[propertyModelSequence.Length];
		//	int[] propertyTypeIdSequence = new int[propertyModelSequence.Length];

		//	for (int i = 0; i < propertyModelSequence.Length; i++)
		//	{
		//		propertyIndexSequence[i] = propertyModelSequence[i].Index;
		//		propertyTypeIdSequence[i] = propertyModelSequence[i].PropertyTypeId;
		//	}

		//	return this.GetSystemPropertySequence(propertyIndexSequence, propertyTypeIdSequence);
		//}
		private SimpleObjectModel GetObjectModelSafely(int tableId)
		{
			SimpleObjectModel result;

			try
			{
				result = this.ModelDiscovery.GetObjectModel(tableId);
			}
			catch
			{
				result = null;
			}

			return result;
		}

		//private SystemPropertySequence GetSystemPropertySequence(int[] propertyIndexSequence, int[] propertyTypeIdSequence)
		//{
		//	SystemPropertySequence result = null;
		//	byte[] valueData = SystemPropertySequence.ToValueData(propertyIndexSequence, propertyTypeIdSequence);

		//	lock (this.lockSystemPropertySequences)
		//	{
		//		foreach (SystemPropertySequence item in this.systemPropertySequences.Values)
		//		{
		//			if (Comparison.EqualBytesLongUnrolled(item.ValueData, valueData)) // Comparison.IsEqual(item.ValueData, valueData)
		//			{
		//				result = item;

		//				break;
		//			}
		//		}
		//	}

		//	if (result == null)
		//	{
		//		result = new SystemPropertySequence(this, ref this.systemPropertySequences, propertyIndexSequence, propertyTypeIdSequence, valueData);
		//		result.Save();

		//		this.systemPropertySequencesByValueData.Add(valueData, result);
		//		this.systemPropertySequencesByPropertySequenceId[result.PropertySequenceId] = result;
		//	}

		//	return result;
		//}

		#endregion |   Private Methods   |

		#region |   Public Methods   |

		public void SetClientWorkingMode(IRemoteDatastore remoteDatastore)
		{
			lock (this.lockObjectCache)
			{
				this.workingMode = ObjectManagerWorkingMode.Client;
				this.localDatastore = null;
				this.datastore = remoteDatastore as IVirtualDatastore;
				this.remoteDatastore = remoteDatastore;
				this.InitializeSystemRelations(updateToLocalDatastore: false);
				this.InitializeSystemGraphs(updateToLocalDatastore: false);
				//this.CreateManyToManyHashArray();
			}
			//this.InitializeObjectCache(this.ModelDiscovery);

			//foreach (SimpleObjectModel objectModel in this.ModelDiscovery.ObjectModels)
			//{
			//	if (objectModel.IsSortable) // if (objectModel.ObjectType.IsSubclassOf(typeof(SortableSimpleObject)))
			//	{
			//		objectModel.PropertyModels[SortableSimpleObject.IndexPropertyPreviousId].AddOrRemoveInChangedProperties = false;
			//		objectModel.PropertyModels[SortableSimpleObject.IndexPropertyOrderIndex].AddOrRemoveInChangedProperties = true;
			//	}
			//}
		}

		public void SetServerWorkingMode(LocalDatastore localDatastore)
		{
			this.workingMode = ObjectManagerWorkingMode.Server;
			this.SetServerBasedWorkingMode(localDatastore);
		}

		public void SetClientWithLocalDatastoreWorkingMode(LocalDatastore localDatastore)
		{
			this.workingMode = ObjectManagerWorkingMode.ClientWithLocalDatastore;
			this.SetServerBasedWorkingMode(localDatastore);
		}

		public bool AuthenticateSession(string username, string passwordHash, out long userId)
		{
			userId = 0;

			if (this.WorkingMode != ObjectManagerWorkingMode.Client)
			{
				IUser user = this.GetUser(username);

				if (user != null && user.PasswordHash == passwordHash)
				{
					userId = user.Id;

					return true;
				}

				return false;
			}
			else // this.WorkingMode == ObjectManagerWorkingMode.Client
			{
				throw new Exception("ObjectManager can authorize session only in Server working mode");
			}
		}

		//public SimpleObject GetSimpleObject(Guid objectKey)
		//      {
		//          return this.GetObject(objectKey);
		//      }

		//public SimpleObjectCollection<GraphElement> GetGraphElements(int graphKey, long parentGraphElementId)
		//{
		//	return this.GetGraphElements(graphKey, parentGraphElementId, out bool[] hasChildrenInfo);
		//}


		public SimpleObjectCollection<GraphElement> GetGraphElements(int graphKey, long parentGraphElementId) //, out bool[] hasChildrenInfo)
		{
			SimpleObjectCollection<GraphElement> result;

			if (this.WorkingMode == ObjectManagerWorkingMode.Client)
			{
				if (parentGraphElementId == 0)
				{
					return this.GetSystemGraph(graphKey).GetRootGraphElements(); //out hasChildrenInfo);
				}
				else
				{
					// Now determine if the first or any of child graph elements are in the cache. If so, probably the all other requred objects are in the cache
					ObjectCache? graphElementObjectCache = this.GetObjectCache(GraphElementModel.TableId);
					GraphElement? parent = graphElementObjectCache?.GetObject(parentGraphElementId) as GraphElement;

					if (!parent!.IsChildrenLoadedInClientMode)
					{
						this.CacheGraphElementsWithObjectsFromServer(graphKey, parentGraphElementId, out List<long> graphElementObjectIds); //, out hasChildrenInfo);

						var graphElements = new SimpleObjectCollection<GraphElement>(this, GraphElementModel.TableId, graphElementObjectIds);

						parent.SetGraphElementCollectionInternal(graphElements);
						parent!.IsChildrenLoadedInClientMode = true;

						return graphElements;
					}
					else
					{
						result = parent.GraphElements;
					}
				}
			}
			else
			{
				result = this.GetLocalGraphElements(graphKey, parentGraphElementId);
			}

			//hasChildrenInfo = new bool[result.Count];

			//for (int i = 0; i < result.Count; i++)
			//	hasChildrenInfo[i] = result[i].HasChildren;

			return result;
		}

		public SimpleObjectCollection<GraphElement> GetLocalGraphElements(int graphKey, long parentGraphElementId)
		{
			GraphElement? parent = this.GetObject(GraphElementModel.TableId, parentGraphElementId) as GraphElement;
			SimpleObjectCollection<GraphElement> graphElements = (parent != null) ? parent.GraphElements : this.GetRootGraphElements(graphKey); //, out bool[] hasChildrenInfo);

			return graphElements;
		}

		public SimpleObjectCollection<GraphElement> GetRootGraphElements(int graphKey) //, out bool[] hasChildrenInfo)
		{
			SystemGraph graph = this.GetSystemGraph(graphKey);

			return graph.GetRootGraphElements(); //out hasChildrenInfo);
		}


		//public Graph GetGraph(int graphKey)
		//{
		//	Graph graph = this.graphsByGraphKey.GetValue(graphKey);

		//	if (graph == null)
		//	{
		//		graph = this.Graphs.FirstOrDefault(item => item.GraphKey == graphKey);

		//		if (graph != null)
		//			this.graphsByGraphKey[graphKey] = graph;
		//	}

		//	return graph;
		//}

		//public Graph GetGraph(long graphId)
		//{
		//	Graph graph = this.graphsByGraphId.GetValue((int)graphId);

		//	if (graph == null)
		//	{
		//		graph = this.Graphs.FirstOrDefault(item => item.Id == graphId);

		//		if (graph != null)
		//			this.graphsByGraphId[(int)graphId] = graph;
		//	}

		//	return graph;
		//}


		public IRelationModel? GetRelationModel(int relationKey)
		{
			return this.ModelDiscovery.GetRelationModel(relationKey);
		}

		//public ManyToManyRelation GetManyToManyRelation(int manyToManyRelationKey)
		//{
		//	return this.manyToManyRelationsByManyToManyRelationKey[manyToManyRelationKey];
		//}

		public SimpleObjectModel? GetObjectModel(int tableId)
		{
			return this.ModelDiscovery.GetObjectModel(tableId);
		}

		public SimpleObjectModel? GetObjectModel(string tableName)
		{
			return this.ModelDiscovery.GetObjectModel(tableName);
		}

		public SimpleObjectModel? GetObjectModel(Type objectType)
		{
			return this.ModelDiscovery.GetObjectModel(objectType);
		}

		public ServerObjectModelInfo? GetServerObjectModel(int tableId)
		{
			return this.ModelDiscovery.GetServerObjectModel(tableId);
		}

		//// TODO: Move this to ObjectCache.SelectAll
		//public SimpleObjectCollection<T> GetSingleObjectCollection<T>(int tableId) where T : SimpleObject
		//{
		//	SimpleObjectCollection<T> result;
		//	SimpleObjectCollection? value;

		//	lock (this.lockGetObjectCollection)
		//	{
		//		if (this.simpleObjectCollectionsByTableId.TryGetValue(tableId, out value))
		//		{
		//			result = (SimpleObjectCollection<T>)value;
		//		}
		//		else
		//		{
		//			ObjectCache objectCache = this.GetObjectCache(tableId);

		//			result = objectCache.SelectAll<T>();
		//			this.simpleObjectCollectionsByTableId.Add(tableId, result);
		//		}
		//	}

		//	return result;
		//}

		//      public IDictionary<int, ISimpleObjectModel> GetObjectModelsByTableId()
		//      {
		//          return this.objectModelsByTableId.AsReadOnly();
		//      }

		//      public IEnumerable<ISimpleObjectModel> GetObjectModels()
		//{
		//	return this.objectModelsByObjectType.AsCustom<ISimpleObjectModel>().Values;
		//}

		//public IDictionary<Type, ISimpleObjectModel> GetObjectModelsByObjectType()
		//{
		//	return this.objectModelsByObjectType.AsCustom<ISimpleObjectModel>().AsReadOnly();
		//}

		//public IDictionary<Type, ISimpleObjectModel> GetObjectModelsByObjectModelDefinitionType()
		//      {
		//	return this.objectModelsByObjectModelDefinitionType.AsCustom<ISimpleObjectModel>().AsReadOnly();
		//      }

		//public IDictionary<Type, ISimpleObjectModel> GetObjectModelsByOriginObjectType()
		//{
		//	return new CustomDictionary<Type, ISimpleObjectModel>(this.objectModelsByOriginObjectType);
		//}

		//public IObjectRelationModel GetObjectRelationModel(Type objectType)
		//{
		//	return this.GetObjectRelationModelDefinition(objectType);
		//}

		//      public IOneToOneRelationModel GetOneToOneRelationModel(int oneToOneRelationKey)
		//      {
		//          OneToOneRelationModel value;
		//          this.relationPolicyModel.OneToOneRelations.TryGetValue(oneToOneRelationKey, out value);

		//          return value;
		//      }

		//      public IOneToManyRelationModel GetOneToManyRelationModel(int oneToManyRelationKey)
		//      {
		//          OneToManyRelationModel value;
		//          this.relationPolicyModel.OneToManyRelations.TryGetValue(oneToManyRelationKey, out value);

		//          return value;
		//      }

		//      public IManyToManyRelationModel GetManyToManyRelationModel(int manyToManyRelationKey)
		//      {
		//          ManyToManyRelationModel value;
		//          this.relationPolicyModel.ManyToManyRelations.TryGetValue(manyToManyRelationKey, out value);

		//          return value;
		//      }

		//public IGraphPolicyModel GetGraphPolicyModel(Graph graph)
		//      {
		//          return this.GetGraphPolicyModel(graph.GraphKey);
		//      }

		//public IGraphPolicyModel GetGraphPolicyModel(int graphKey)
		//      {
		//	SingleGraphPolicyModel result = null;
		//	this.GraphPolicyModelsByGraphKey.TryGetValue(graphKey, out result);

		//	return result;
		//      }

		//public IEditPanelPolicyModel GetEditPanelPolicyModel(Type objectType)
		//{
		//	EditPanelPolicyModel result = null;
		//	this.EditPanelPolicyModelsByObjectType.TryGetValue(objectType, out result);

		//	return result;
		//}


		//public bool PrepeareDatastoreConnection()
		//{
		//	int DatasroreNumOfConnectionRetry = 10;

		//	for (int i = 0; i < DatasroreNumOfConnectionRetry; i++)
		//	{
		//		if (this.LocalDatastore.Connected)
		//			return true;

		//		this.LocalDatastore.Connect();
		//	}

		//	return this.LocalDatastore.Connected;
		//}

		//public SimpleObjectKey[] GetObjectKeys(int tableId)
		//{
		//    return this.ObjectCache.GetObjectKeys(tableId);
		//}

		public SystemSetting GetDatastoreSystemSetting(string settingName)
		{
			SystemSetting systemSetting = this.SystemSettings[settingName];

			//if (systemSetting == null)
			//{
			//	systemSetting = new SystemSetting();
			//	systemSetting.Name = settingName;
			//	this.SystemSettings.Add(systemSetting);
			//}

			return systemSetting;
		}

		//public void SetDatastoreSystemSetting(string settingName, object value)
		//{
		//	SystemSetting systemSetting = this.GetDatastoreSystemSetting(settingName);

		//	if (systemSetting == null)
		//	{
		//		systemSetting = new SystemSetting();
		//		systemSetting.Name = settingName;
		//	}

		//	systemSetting.Value = value;
		//	systemSetting.Save();
		//}



		//public long GetDatastoreMaxObjectId(int tableId)
		//{
		//	SystemMaxObjectId systemMaxObjectId = this.SystemTableNames.FirstOrDefault(maxObjectId => maxObjectId.ObjectTableId == tableId);

		//	if (systemMaxObjectId != null)
		//		return systemMaxObjectId.MaxObjectId;

		//	return 0;
		//}

		public byte[] CompressData(byte[] inputData)
		{
			byte[] result = MiniLZOSafe.Compress(inputData);
			return result;
		}

		public byte[] DecompressData(byte[] compressedData)
		{
			//byte[] result = new byte[] { };

			byte[] result = MiniLZOSafe.Decompress(compressedData);
			return result;
		}

		//internal IEnumerable<SystemTransactionAction> GetTransactionActions(SystemTransaction transaction)
		//{
		//    byte[] uncompressedActionData = this.DecompressData(transaction.ActionData);
		//    List<SystemTransactionAction> transactionActions = this.DeserializeTransactionActions(transaction, uncompressedActionData);

		//    return transactionActions;
		//}

		public void SetMultipleStatusChange(IDictionary<SimpleObject, int> newStatusesBySimpleObject)
		{
			if (newStatusesBySimpleObject == null)
				return;

			//lock (this.lockMultipleStatusChange)
			//{
				this.multipleStatusChangesInProgress++;
			//}

			foreach (var item in newStatusesBySimpleObject)
				item.Key.Status = item.Value; // Key is SimpleObject, Value is Status

			lock (this.lockMultipleStatusChange)
			{
				this.multipleStatusChangesInProgress--;

				if (this.multipleStatusChangesInProgress == 0 && this.multipleImageNameChangeSimpleObjectEventArgs.Count > 0)
				{
					this.RaiseMultipleImageNameChange(new List<ImageNameChangeSimpleObjectEventArgs>(this.multipleImageNameChangeSimpleObjectEventArgs));
					this.multipleImageNameChangeSimpleObjectEventArgs.Clear();
				}
			}
		}

		//public SystemPropertySequence GetSystemPropertySequence(int propertySequenceId)
		//{
		//	return this.systemPropertySequences[propertySequenceId];
		//}


		//public void LoadFieldValues(SimpleObject simpleObject, IPropertyModel[] propertyModelSequence, object[] propertyValues, Func<IPropertyModel, object, object> readNormalizer, bool loadOldValuesAlso)
		//{
		//	simpleObject.Load(propertyModelSequence, propertyValues, readNormalizer, loadOldValuesAlso);
		//}

		//public ISystemPropertySequence[] GetSystemPropertySequences()
		//{
		//	return this.systemPropertySequences.Values.ToArray();
		//}

		//public Dictionary<int, IPropertySequence> GetServerPropertySequencesByTableId()
		//{
		//	return this.serverPropertySequencesByTableId;
		//}

		public void BeginLargeUpdate()
		{
			this.RaiseLargeUpdateStarted();
		}

		public void EndLargeUpdate()
		{
			this.RaiseLargeUpdateEnded();
		}

		public void UndoTransaction(SystemTransaction transaction, object requester)
		{
			this.RollbackTransactionInternal(transaction, requester);
		}

		public override string ToString()
		{
			return base.ToString() + " (" + this.WorkingMode + ")";
		}

		#endregion |   Public Methods   |

		#region |   Get Record(s) From Datastore   |

		// TODO: Da li se ovo može izbaciti? - DA - izbačeno

		//public Guid[] GetRecordKeysFromDatastore(ITableInfo tableInfo)
		//{
		//	return this.Datastore.GetObjectKeys(tableInfo);
		//	//return this.LocalDatastore.GetRecordKeys<Guid>(tableInfo.TableName, SimpleObject.StringPropertyKey);
		//}

		////public Guid[] GetRecordKeysFromDatastore(ITableInfo tableInfo, IEnumerable<WhereCriteriaElement> whereCriteria)
		////{
		////	return this.LocalDatastore.GetRecordKeys<Guid>(tableInfo.TableName, SimpleObject.StringPropertyKey, whereCriteria);
		////}

		//public IDataReader GetRecordFromDatastore(ITableInfo tableInfo, Guid objectKey)
		//{
		//	return this.LocalDatastore.GetRecord(tableInfo.TableName, SimpleObject.StringPropertyKey, objectKey);
		//}

		//public IDataReader GetRecordsFromDatastore(ITableInfo tableInfo, bool includeRecordCountField, params string[] fieldNames)
		//{
		//	return this.GetRecordsFromDatastore(tableInfo, whereCriteria: null, includeRecordCountField: includeRecordCountField, fieldNames: fieldNames);
		//}

		//public IDataReader GetRecordsFromDatastore(ITableInfo tableInfo, IEnumerable<WhereCriteriaElement> whereCriteria, bool includeRecordCountField, params string[] fieldNames)
		//{
		//	return this.LocalDatastore.GetRecords(tableInfo.TableName, whereCriteria, includeRecordCountField, fieldNames);
		//}

		#endregion |   Get Record(s) From Datastore   |

		#region |   Object Cache   |

		public ObjectCache GetObjectCache<T>()
			where T : SimpleObject
		{
			return this.GetObjectCache(typeof(T));
		}

		public ObjectCache GetObjectCache(Type objectType)
		{
			ISimpleObjectModel objectModel = this.GetObjectModel(objectType);

			return this.GetObjectCache(objectModel.TableInfo.TableId);
		}

		//public ObjectCache GetObjectCache(ISimpleObjectModel objectModel)
		//{
		//	return this.GetObjectCache(objectModel.TableInfo.TableId);
		//}

		public ObjectCache? GetObjectCache(int tableId)
		{
			ObjectCache? objectCache = null;

			//if (tableId == 1)
			//	objectCache = null;

			//if (this.WorkingMode == ObjectManagerWorkingMode.Client && tableId == 1)
			//	objectCache = null;

			lock (this.lockObjectCache)
			{
				objectCache = this.objectCachesByTableId[tableId];

				if (objectCache == null)
				{
					ISimpleObjectModel? objectModel = this.GetObjectModel(tableId);

					if (objectModel == null)
						return null;

					objectCache = this.CreateObjectCache(objectModel);
					
					this.objectCachesByTableId[tableId] = objectCache;
				}
			}

			return objectCache;
		}

		protected virtual ObjectCache CreateObjectCache(ISimpleObjectModel objectModel)
		{
			ObjectCache objectCache;

			if (this.workingMode == ObjectManagerWorkingMode.Client)
			{
				objectCache = new ClientObjectCache(this, objectModel);
			}
			else
			{ 
				if (objectModel.IsStorable)
				{
					if (objectModel.FetchAllRecords && this.WorkingMode == ObjectManagerWorkingMode.Server)
					{
						using (IDataReader dataReader = this.LocalDatastore.GetRecords(objectModel.TableInfo))
						{
							objectCache = new ServerObjectCache(this, objectModel, dataReader);
							dataReader.Close();
						}
					}
					else
					{
						List<long> objectKeys = this.GetDatastoreObjectKeys(objectModel.TableInfo).GetAwaiter().GetResult();

						objectCache = new ServerObjectCache(this, objectModel, objectKeys);
					}
				}
				else
				{
					objectCache = new ServerObjectCache(this, objectModel);
				}
			}

			return objectCache;
		}

		private async ValueTask<List<long>> GetDatastoreObjectKeys(TableInfo tableInfo)
		{
			List<long> result; // = null;

			//do
			//{
				result = await this.LocalDatastore!.GetObjectIds(tableInfo);
			//}
			//while (result == null);

			return result;
		}

		//private Dictionary<long, HashArray<object>> GetObjectIndexeedValuesFromDatastore(ISimpleObjectModel objectModel)
		//{

		//	List<WhereCriteriaElement> whereCriteria = new List<WhereCriteriaElement>();

		//	foreach (int propertyIndex in objectModel.IndexedPropertyIndexes)
		//	{
		//		IPropertyModel propertyModel = objectModel.PropertyModels[propertyIndex];
		//		string fieldName = propertyModel.DatastoreFieldName;

		//		whereCriteria.Add(new WhereCriteriaElement(fieldName, ))
		//	}

		//	new List<WhereCriteriaElement>()
		//{
		//	new WhereCriteriaElement(GraphElementModel.PropertyModel.GraphKey.Name, (int)GraphKey.Users, WhereComparator.NotEqual),
		//	new WhereCriteriaElement(GraphElementModel.PropertyModel.GraphKey.Name, (int)GraphKey.UserGroups, WhereComparator.NotEqual),

		//}


		//public SimpleObject this[Guid objectKey]
		//{
		//	get { return this.GetObject(objectKey); }
		//}

		//public SimpleObject GetObject(Guid? objectKey)
		//{
		//	return (objectKey == null) ? null : this.GetObject(objectKey.Value, default(long));
		//}

		//public SimpleObject GetObject(Guid objectKey, long objectId)
		//{
		//	if (objectKey == Guid.Empty)
		//		return null;

		//	ObjectCache objectCache = this.GetObjectCache(objectKey.GetTableId());
		//	SimpleObject value = objectCache.GetObject(objectKey, objectId);

		//	return value;
		//}
		//public SimpleObject GetSimpleObject(object tableId, object objectId)
		//{
		//	if (tableId == null || objectId == null)
		//		return null;

		//	return this.GetSimpleObject((int)tableId, (long)objectId);
		//}

		//public SimpleObject GetSimpleObject(int? tableId, long objectId)
		//{
		//	if (tableId == null)
		//		return null;

		//	return this.GetSimpleObject((int)tableId, objectId);
		//}

		//public SimpleObject GetSimpleObject(int tableId, long? objectId)
		//{
		//	if (objectId == null)
		//		return null;

		//	return this.GetSimpleObject(tableId, (long)objectId);
		//}

		//public SimpleObject GetSimpleObject(int? tableId, long? objectId)
		//{
		//	if (tableId == null || objectId == null)
		//		return null;

		//	return this.GetSimpleObject((int)tableId, (long)objectId);
		//}
		public GroupMembership? GetGroupMembership(int membershipKey, SimpleObject object1, SimpleObject object2) => this.GetGroupMembership(membershipKey, object1.GetModel().TableInfo.TableId, object1.Id, object2.GetModel().TableInfo.TableId, object2.Id);

		public GroupMembership? GetGroupMembership(int membershipKey, int object1TableId, long object1Id, int object2TableId, long object2Id)
		{
			GroupMembership? result;
			ObjectCache? objectCache = this.GetObjectCache(GroupMembershipModel.TableId);

			if (objectCache is ServerObjectCache serverObjectCache)
			{

				result = serverObjectCache.FindFirst<GroupMembership>(gm => gm.RelationKey == membershipKey &&
																		    ((gm.Object1TableId == object1TableId && gm.Object1Id == object1Id && gm.Object2TableId == object2TableId && gm.Object2Id == object2Id) ||
																			 (gm.Object1TableId == object2TableId && gm.Object1Id == object2Id && gm.Object2TableId == object1TableId && gm.Object2Id == object1Id)));
			}
			else if (objectCache is ClientObjectCache clientObjectCache)
			{
				
				
				result = null;
			}
			else
			{
				result = default;
			}

			return result;
		}

		public SimpleObject? GetObject(SimpleObjectKey objectKey) => this.GetObject(objectKey.TableId, objectKey.ObjectId);

		public T? GetObject<T>(int tableId, long objectId)
			where T : SimpleObject
		{
			return this.GetObject(tableId, objectId) as T;
		}

		public SimpleObject? GetObject(int tableId, long objectId)
		{
			if (objectId == 0) // || tableId == 0 
				return null;

			ObjectCache? objectCache = this.GetObjectCache(tableId);
			SimpleObject? value = objectCache?.GetObject(objectId);

			return value;
		}

		public bool ContainsObject(int tableId, long objectId)
		{
			ObjectCache objectCache = this.GetObjectCache(tableId);

			if (objectCache != null)
				return objectCache.ContainsId(objectId);

			return false;
		}

		public bool IsObjectInCache(int tableId, long objectId)
		{
			ObjectCache objectCache = this.GetObjectCache(tableId);

			if (objectCache != null)
				return objectCache.IsInCache(objectId);

			return false;
		}


		//public bool IsObjectInCache(Guid? objectKey)
		//{
		//	return (objectKey == null) ? false : this.IsObjectInCache(objectKey.Value);
		//}

		//public bool IsObjectInCache(Guid objectKey)
		//{
		//	if (objectKey == Guid.Empty)
		//		return false;

		//	ObjectCache objectCache = this.GetObjectCache(objectKey.GetTableId());
		//	bool value = objectCache.Contains(objectKey);

		//	return value;
		//}

		//public SimpleObjectCollection<T> GetObjectCollection<T>() where T : SimpleObject
		//{
		//	lock (lockGetObjectCollection)
		//	{
		//		ObjectCache objectCache = this.GetObjectCache<T>();
		//		SimpleObjectCollection<T> value = new SimpleObjectCollection<T>(this, objectCache.ObjectModel.TableInfo.TableId, objectCache.Keys);

		//		return value;
		//	}
		//}

		//public SimpleObjectCollection<T> GetObjectCollection<T>(Predicate<T> selector) where T : SimpleObject
		//{
		//	return this.GetObjectCache<T>().Select<T>(selector); ;

		//	////SimpleObjectCollection<T> allObjects = this.GetObjectCollection<T>();
		//	////SimpleObjectCollection<T> result = new SimpleObjectCollection<T>(this);

		//	////lock (lockGetObjectCollection)
		//	////{
		//	////	foreach (var item in allObjects)
		//	////		if (selector(item))
		//	////			result.Add(item);

		//	////	return result;
		//	////}
		//}

		//public List<SimpleObject> GetObjectCollection(int tableId)
		//{
		//	List<SimpleObject> result = null;

		//	lock (lockGetObjectCollection)
		//	{
		//		ObjectCache objectCache = this.GetObjectCache(tableId);

		//		if (objectCache != null)
		//		{
		//			long[] objectIds = objectCache.Keys;
		//			result = new List<SimpleObject>(objectIds.Length);

		//			for (int i = 0; i < objectIds.Count(); i++)
		//			{
		//				long objectId = objectIds.ElementAt(i);
		//				//Guid objectKey = objectKeys.ElementAt(i);

		//				SimpleObject simpleObject = this.GetSimpleObject(tableId, objectId);

		//				if (simpleObject != null)
		//					result.Add(simpleObject);
		//			}

		//		}

		//		return result;
		//	}
		//}

		//public long[] GetObjectIds(int tableId)
		//{
		//	ObjectCache objectCache = this.GetObjectCache(tableId);

		//	return objectCache.Keys;
		//}


		//public virtual SimpleObjectCollection<GraphElement> GetRootGraphElements(int graphKey)
		//{
		//	return this.GetObjectCollection<GraphElement>(ge => ge.Graph.GraphKey == graphKey && ge.ParentId == 0);
		//}

		//public virtual SimpleObjectCollection<GraphElement> GetGraphElements(GraphElement parent)
		//{
		//	return parent.GraphElements;
		//}

		//public Guid[] GetObjectKeys(int tableId)
		//{
		//	ObjectCache objectCache = this.GetObjectCache(tableId);
		//	return objectCache.ObjectKeys;
		//}

		//public Guid[] GetObjectKeys<T>() where T : SimpleObject
		//{
		//	ObjectCache objectCache = this.GetObjectCache<T>();
		//	return objectCache.ObjectKeys;
		//}

		//public Guid[] GetObjectKeys<T>(Predicate<T> match) where T : SimpleObject
		//{
		//	lock (lockGetObjectKeys)
		//	{
		//		//IEnumerable<SimpleObjectKey> objectKeys = null;

		//		//lock (lockObject)
		//		//{
		//		//SimpleObjectCollection<T> simpleObjectCollection = this.GetObjectCollection<T>();

		//		//List<SimpleObjectKey> objectKeys = new List<SimpleObjectKey>();

		//		//foreach (T simpleObject in simpleObjectCollection)
		//		//{
		//		//    if (match(simpleObject))
		//		//    {
		//		//        objectKeys.Add(simpleObject.Key);
		//		//    }
		//		//}

		//		var objectKeys = from simpleObject in this.GetObjectCollection<T>()
		//						 where match(simpleObject)
		//						 select simpleObject.Guid;

		//		return objectKeys.ToArray();
		//	}
		//}


		//public SimpleObject CreateNewEmptyObject(Type objectType, long objectId)
		//{
		//	SimpleObject simpleObject = this.CreateNewEmptyObject(objectType);
		//	simpleObject.SetId(objectId);
		//	//simpleObject.Id = (this.WorkingMode == ObjectManagerWorkingMode.Server) ? this.ClientObjectIdGenerator.CreateKey() : - this.ClientObjectIdGenerator.CreateKey();

		//	//simpleObject.SetPropertyValueInternal(SimpleObject.IndexPropertyKey, objectKey, addOrRemoveInChangedProperties: false, firePropertyValueChangeEvent: false, requester: this);

		//	return simpleObject;
		//}

		//public SimpleObject CreateNewEmptyObject(Type objectType, Guid objectKey)
		//{
		//	SimpleObject simpleObject = this.CreateNewEmptyObject(objectType);
		//	simpleObject.SetKey(objectKey);
		//	//simpleObject.Id = (this.WorkingMode == ObjectManagerWorkingMode.Server) ? this.ClientObjectIdGenerator.CreateKey() : - this.ClientObjectIdGenerator.CreateKey();

		//	//simpleObject.SetPropertyValueInternal(SimpleObject.IndexPropertyKey, objectKey, addOrRemoveInChangedProperties: false, firePropertyValueChangeEvent: false, requester: this);

		//	return simpleObject;
		//}

		//public SimpleObject CreateNewEmptyObject(Type objectType)
		//{
		//	return this.CreateNewEmptyObject(objectType, requester: null);
		//}

		//public SimpleObject CreateNewEmptyObject(Type objectType, object requester)
		//{
		//	return this.CreateNewEmptyObject(objectType, isNew: true, objectId: 0, requester);
		//}

		//public SimpleObject CreateNewEmptyObject(Type objectType, bool isNew, requester)
		//{
		//	return this.CreateNewEmptyObject(objectType, isNew, objectId: 0, requester);
		//}
		//public SimpleObject CreateNewEmptyObject(Type objectType)
		//{
		//	return this.CreateNewEmptyObject(objectType, requester: null);
		//}

		//public SimpleObject CreateNewEmptyObject(Type objectType, object requester)
		//{
		//	return this.CreateNewEmptyObject(objectType, isNew: true, requester);
		//}

		//public SimpleObject CreateNewEmptyObject(Type objectType, bool isNew)
		//{
		//	return this.CreateNewEmptyObject(objectType, isNew, requester: null);
		//}

		//public SimpleObject CreateNewEmptyObject(Type objectType, bool isNew, object requester)
		//{
		//	return this.CreateNewEmptyObject(objectType, isNew, objectId: 0, requester);
		//}

		//public SimpleObject CreateNewEmptyObject(Type objectType, bool isNew, long objectId)
		//{
		//	return this.CreateNewEmptyObject(objectType, isNew, objectId, requester: null);
		//}

		//public SimpleObject CreateNewEmptyObject(Type objectType, bool isNew, long objectId, object requester)
		//{
		//	return this.CreateNewEmptyObject(objectType, isNew, objectId, this.DefaultChangeContainer, requester);
		//}

		public SimpleObject CreateNewEmptyObject(Type objectType, bool isNew, long objectId)
		{
			return this.CreateNewEmptyObject(objectType, isNew, objectId, requester: null);
		}

		public SimpleObject CreateNewEmptyObject(Type objectType, bool isNew, long objectId, object? requester)
		{
			return this.CreateNewEmptyObject(objectType, isNew, objectId, this.DefaultChangeContainer, requester);
		}

		public SimpleObject CreateNewEmptyObject(Type objectType, bool isNew, long objectId, ChangeContainer? changeContainer, object? requester)
		{
			//SimpleObject simpleObject = (objectType == typeof(GraphElement)) ? new GraphElement(this, changeContainer) : Activator.CreateInstance(objectType, this) as SimpleObject;
			if (Activator.CreateInstance(objectType, this) is SimpleObject simpleObject)
			{
				simpleObject.ChangeContainer = changeContainer;
				simpleObject.Requester = requester;
				simpleObject.SetIsNew(isNew);

				if (objectId > 0)
				{
					simpleObject.SetId(objectId);
				}
				else
				{
					// Enforce new Id creation;
					_ = simpleObject.Id;
				}

				if (changeContainer != null)
					changeContainer.Set(simpleObject, TransactionRequestAction.Save, requester);

				return simpleObject;
			}
			else
			{
				throw new ArgumentException("The object type " + objectType.Name + " is not based on SimpleObject");
			}
		}

		//public GraphElement CreateGraphElement(SimpleObject simpleObject, int graphKey, GraphElement parentGraphElement)
		//{
		//	GraphElement graphElement = new GraphElement(this, graphKey, parentGraphElement, simpleObject);

		//	graphElement.SetIsNew(true);

		//	if (!simpleObject.IsNew)
		//		graphElement.Save();

		//	return graphElement;
		//}


		//public void AddNewObjectInCache(SimpleObject simpleObject)
		//{
		//	ObjectCache objectCache = this.GetObjectCache(simpleObject.GetModel().TableInfo.TableId);

		//	if (objectCache.IsObjectInCache(simpleObject.Id))
		//		return;

		//	objectCache.AddNewObject(simpleObject);
		//	//objectCache.AddNewObjectWithKey(simpleObject);
		//}

		internal long AddNewObjectInCache(SimpleObject simpleObject, ChangeContainer changeContainer, object requester)
		{
			ObjectCache objectCache = this.GetObjectCache(simpleObject.GetModel().TableInfo.TableId);
			long id = objectCache.AddNewObject(simpleObject);

			simpleObject.InternalState = SimpleObjectInternalState.Normal;

			if (simpleObject.IsNew && this.WorkingMode != ObjectManagerWorkingMode.Server)
			{
				this.NewObjectIsCreated(simpleObject, requester);

				ChangeContainer cc = changeContainer ?? this.DefaultChangeContainer;
				
				cc.Set(simpleObject, TransactionRequestAction.Save, requester);
			}

			return id;
		}


		internal bool RemoveObjectFromCache(int tableId, long objectId)
		{
			ObjectCache objectCache = this.GetObjectCache(tableId);

			return objectCache.RemoveObject(objectId);
		}

		//internal SimpleObject CreateObject(ObjectCache objectCache, Guid objectKey)
		//{
		//	return this.Datastore.CreateObject(objectCache, objectKey);
		//}




		//int TODO; // Is this necesery???
		//internal int[] GetStorablePropertyIndexes(ISimpleObjectModel objectModel)
		//{
		//	return this.Datastore.GetPropertyIndexSequence(objectModel);
		//}




		//protected virtual ObjectCache CreateObjectCacheWithLoadedObjects(ISimpleObjectModel objectModel)
		//{
		//	ObjectCache value;

		//	IDataReader dataReader = this.GetRecordsFromDatastore(objectModel.TableInfo, includeRecordCountField: false);
		//	value = new ObjectCache(this, objectModel, dataReader);

		//	dataReader.Close();

		//	return value;
		//}


		// TODO:
		//internal SimpleObject CreateObject(ITableInfo tableInfo, Guid objectKey)
		//{
		//	SimpleObject value = null;
		//	IDataReader dataReader = this.GetRecordFromDatastore(tableInfo, objectKey);

		//	if (dataReader.Read())
		//		value = this.CreateObjectFromPropertyValueData((simpleObject) => simpleObject.Load(this, dataReader, this.GetPropertyModelsByDataReaderFieldIndex(dataReader),
		//																						   normalizer: this.ObjectManager.GetNormalizedPropertyValueFromDatastoreField, loadOldValuesAlso: true));
		//	dataReader.Close();

		//	return value;
		//}



		#endregion |   Object Cache   |

		#region |   Can & Validation   |

		public virtual bool CanAddGraphElement(int graphKey, Type objectType, GraphElement? parentGraphElement)
		{
			return this.ValidateGraphPolicy(graphKey, objectType, parentGraphElement).Passed;
		}

		private bool CanAddGraphElement_OLD(int graphKey, Type objectType, GraphElement? parentGraphElement)
		{
			bool result = false;
			Type? parentGraphElementObjectType = null;
			IGraphPolicyModel? graphPolicyModel = this.ModelDiscovery.GetGraphPolicyModel(graphKey);

			if (parentGraphElement != null)
			{
				if (parentGraphElement.GraphKey != graphKey || parentGraphElement.SimpleObject == null)
				{
					return false;
					//throw new ArgumentException("graphElement argument must have Graph property same as graph argument");
				}

				parentGraphElementObjectType = parentGraphElement.SimpleObject.GetType();
			}

			if (graphPolicyModel != null)
			{
				IGraphPolicyModelElement? objectTypeGraphPolicyElementModel = graphPolicyModel.GetGraphPolicyModelElement(objectType);
				IGraphPolicyModelElement? graphElementGraphPolicyElementModel = (parentGraphElementObjectType != null) ? graphPolicyModel.GetGraphPolicyModelElement(parentGraphElementObjectType) : null;

				if (objectTypeGraphPolicyElementModel != null)
				{
					// First check if child has higher (lower number) priority than object type -> result = false
					bool doesAnyChildHasHigherPriority = false;

					if (objectType != typeof(Folder))   // Allow Folders to be in group with different object types
					{
						lock (this.lockObject)
						{
							SimpleObjectCollection<GraphElement> childGraphElements = (parentGraphElement != null) ? parentGraphElement.GraphElements : this.GetSystemGraph(graphKey).GetRootGraphElements();

							foreach (GraphElement childGraphElement in childGraphElements)
							{
								if (childGraphElement.SimpleObject is Folder) // == FolderModel.TableId) //.SimpleObject is Folder) // Allow Folder to be in group with any objects.
									continue;

								
								graphPolicyModel.PolicyElements.Values.FirstOrDefault(policyElementModel =>
								{
									//ISimpleObjectModel simpleObjectModel = this.GetObjectModel(childGraphElement.SimpleObjectTableId);

									//if (childGraphElement.SimpleObjectKey != null)
									//{
									return policyElementModel.ObjectType == childGraphElement.SimpleObject!.GetType(); //simpleObjectModel.ObjectType;
																													  //}
																													  //else
																													  //{
																													  //	return false;
																													  //}
								});

								IGraphPolicyModelElement? childGraphElementGraphPolicyElementModel = graphPolicyModel.GetGraphPolicyModelElement(childGraphElement.SimpleObject!.GetType());

								if (childGraphElementGraphPolicyElementModel != null && childGraphElementGraphPolicyElementModel.Priority < objectTypeGraphPolicyElementModel.Priority)
								{
									doesAnyChildHasHigherPriority = true;

									break;
								}
							}
						}
					}

					if (doesAnyChildHasHigherPriority)
					{
						result = false;
					}
					else
					{
						if (graphElementGraphPolicyElementModel != null)
						{
							if (graphElementGraphPolicyElementModel.ObjectType == objectTypeGraphPolicyElementModel.ObjectType)
							{
								result = objectTypeGraphPolicyElementModel.SubTreePolicy == SubTreePolicy.AllowSubTree;
							}
							else if (objectTypeGraphPolicyElementModel.ParentAcceptablePriorities.Count() > 0)
							{
								result = false;

								foreach (int acceptedParentPriority in objectTypeGraphPolicyElementModel.ParentAcceptablePriorities)
								{
									if (graphElementGraphPolicyElementModel.Priority == acceptedParentPriority)
									{
										result = true;
										break;
									}
								}
							}
							else
							{
								result = objectTypeGraphPolicyElementModel.Priority > graphElementGraphPolicyElementModel.Priority;
							}
						}
						else
						{
							result = objectTypeGraphPolicyElementModel.CanBeInRoot;
						}
					}
				}
			}

			return result;

		}

		protected internal ValidationResult ValidateGraphPolicy(GraphElement graphElement)
		{
			return this.ValidateGraphPolicy(graphElement, graphElement.Parent);
		}

		private ValidationResult ValidateGraphPolicy(GraphElement graphElement, GraphElement parentGraphElement)
		{
			if (graphElement.SimpleObject == null)
				return new ValidationResult(false, "GraphElement SimpleObject is null");

			return this.ValidateGraphPolicy(graphElement.GraphKey, graphElement.SimpleObject.GetType(), parentGraphElement);
		}

		protected virtual ValidationResult ValidateGraphPolicy(int graphKey, Type simpleObjectType, GraphElement? parentGraphElement)
		{
			lock (this.lockObject)
			{
				SystemGraph graph = this.GetSystemGraph(graphKey);
				IGraphPolicyModel? graphPolicyModel = this.ModelDiscovery.GetGraphPolicyModel(graphKey);
				GraphElement? realParentGraphElement = (graphPolicyModel != null && parentGraphElement != null && parentGraphElement.IsAnchor) ? null : parentGraphElement;

				if (graphPolicyModel == null)
					return new ValidationResult(false, String.Format("Graph Policy Model cannot be found for the specified Graph '{0}'", graph.Name));

				if (realParentGraphElement != null && realParentGraphElement.SimpleObject == null)
					return new ValidationResult(false, String.Format("Graph Policy Model Validation error: GraphElemen.SimpleObject is null, GraphElement Id = {0}", realParentGraphElement.Id));

				IGraphPolicyModelElement? objectGraphPolicyModel = graphPolicyModel.GetGraphPolicyModelElement(simpleObjectType);

				if (simpleObjectType == typeof(Folder))
				{
					if (realParentGraphElement != null && realParentGraphElement.SimpleObject!.GetType() != typeof(Folder))
						return new ValidationResult(false, String.Format("Parent graph element of {0} cannot be Folder", simpleObjectType.Name));
				}
				else if (objectGraphPolicyModel != null && realParentGraphElement != null && realParentGraphElement.SimpleObject!.GetType() == typeof(Folder)) // Allow Folder to be in group with different object types
				{
					IGraphPolicyModelElement? parentGraphPolicyModel = graphPolicyModel.GetGraphPolicyModelElement(typeof(Folder));

					if (parentGraphPolicyModel != null)
					{
						if (objectGraphPolicyModel.Priority <= parentGraphPolicyModel.Priority)
						{
							return new ValidationResult(true);
						}
						else if (objectGraphPolicyModel.ParentAcceptablePriorities.Count() > 0 || objectGraphPolicyModel.ParentAcceptableTypes.Count() > 0)
						{
							if (objectGraphPolicyModel.ParentAcceptablePriorities.Contains(parentGraphPolicyModel.Priority) || objectGraphPolicyModel.ParentAcceptableTypes.Contains(typeof(Folder)))
								return new ValidationResult(true);
						}
					}
				}
				else
				{
					//SimpleObjectCollection<GraphElement> neighborGraphElements = (parentGraphElement != null) ? parentGraphElement.GraphElements : graph.RootGraphElements;

					//// check if neighbor graph elements can be accepted by graph policy
					//foreach (GraphElement neighborGraphElement in neighborGraphElements)
					//{
					//	Type neighborSimpleObjectType = neighborGraphElement.SimpleObject.GetType();

					//	if (neighborSimpleObjectType == simpleObjectType)
					//		continue;

					//	IGraphPolicyModelElement neighborGraphPolicyModelElement = graphPolicyModel.GetGraphPolicyModelElement(neighborGraphElement.SimpleObject.GetType());

					//	//if (neighborGraphPolicyModelElement.Priority < objectGraphPolicyModel.Priority)
					//	//	return new ValidationResult(false, "Some root graph elements has lower graph priority.");

					//	if (objectGraphPolicyModel.ParentTypeAcceptanceList.Count() > 0 || objectGraphPolicyModel.ParentPriorityAcceptanceList.Count() > 0)
					//	{
					//		if (objectGraphPolicyModel.ParentTypeAcceptanceList.Contains(neighborGraphPolicyModelElement.ObjectType))
					//			continue;

					//		if (objectGraphPolicyModel.ParentPriorityAcceptanceList.Contains(neighborGraphPolicyModelElement.Priority))
					//			continue;

					//		// if any of this two roules not match parent, validation fails
					//		if (neighborGraphPolicyModelElement.Priority < objectGraphPolicyModel.Priority)
					//			return new ValidationResult(false, String.Format("Neighbor graph element {0} ({1}) is not in acceptance list in model definition of {2} object type.", neighborGraphElement.SimpleObject.GetName(), neighborGraphElement.SimpleObject.GetType().Name, simpleObjectType.Name));
					//	}
					//	else if (neighborGraphPolicyModelElement.Priority < objectGraphPolicyModel.Priority)
					//	{
					//		return new ValidationResult(false, String.Format("Neighbor graph element {0} ({1}) has lower graph priority than {2} object type.", neighborGraphElement.SimpleObject.GetName(), neighborGraphElement.SimpleObject.GetType().Name, simpleObjectType.Name));
					//	}
					//}
				}

				if (realParentGraphElement == null)
				{
					if (objectGraphPolicyModel != null && !objectGraphPolicyModel.CanBeInRoot)
						return new ValidationResult(false, simpleObjectType.GetType() + " cannot be in root as specified by graph policy model");

					// Check if neighbor policy is accepted
					long parentGraphElementId = parentGraphElement?.Id ?? 0;
					SimpleObjectCollection<GraphElement>? neighborGraphElements = (graphPolicyModel.IsAnchorMode) ? this.GetGraphElements(graphKey, parentGraphElement?.Id ?? 0) :
																											        this.GetSystemGraph(graphKey).GetRootGraphElements();
					if (neighborGraphElements != null)
					{
						foreach (GraphElement neighborGraphElement in neighborGraphElements)
						{
							if (neighborGraphElement.SimpleObject is Folder) // SimpleObjectTableId == FolderModel.TableId) //.SimpleObject is Folder) // Allow Folder to be in group with any objects.
								continue;

							if (objectGraphPolicyModel != null && objectGraphPolicyModel.CanBeInRoot) // parentGraphElement == null
								continue;

							IGraphPolicyModelElement? neighborObjectGraphPolicyModel = graphPolicyModel.GetGraphPolicyModelElement(neighborGraphElement.SimpleObject!.GetType());

							if (neighborObjectGraphPolicyModel != null && objectGraphPolicyModel != null && neighborObjectGraphPolicyModel.Priority < objectGraphPolicyModel.Priority)
							{
								// Check if parent object is acepted in ParentPriorityAcceptanceList or ParentTypeAcceptanceList definitions
								if (!objectGraphPolicyModel.ParentAcceptablePriorities.Contains(neighborObjectGraphPolicyModel.Priority) || !objectGraphPolicyModel.ParentAcceptableTypes.Contains(neighborObjectGraphPolicyModel.ObjectType))
									return new ValidationResult(false, "Some child GraphElement has a higher graph priority.");
							}
						}
					}
				}
				else // parentGraphElement != null
				{
					if (realParentGraphElement.GraphKey != graphKey)
						return new ValidationResult(false, String.Format("Parent GraphElement Graph {0} does not belong to the given Graph {1}.", nameof(realParentGraphElement.Graph), graph.Name));

					if (realParentGraphElement.SimpleObject == null)
						return new ValidationResult(false, "Parent GraphElement SimpleObject is null.");

					IGraphPolicyModelElement? parentObjectGraphPolicyModel = graphPolicyModel.GetGraphPolicyModelElement(realParentGraphElement.SimpleObject.GetType());

					// First check if child has higher (lower number) priority than object type -> result = false
					if (objectGraphPolicyModel != null && simpleObjectType != typeof(Folder))   // Allow Folder to be in group with different object types
					{
						if (parentObjectGraphPolicyModel == null)
							return new ValidationResult(false, "Graph policy does not allow sub tree.");

						if (objectGraphPolicyModel.ParentAcceptablePriorities.Count() > 0 || objectGraphPolicyModel.ParentAcceptableTypes.Count() > 0)
						{
							if (!objectGraphPolicyModel.ParentAcceptablePriorities.Contains(parentObjectGraphPolicyModel.Priority) && !objectGraphPolicyModel.ParentAcceptableTypes.Contains(parentObjectGraphPolicyModel.ObjectType))
								return new ValidationResult(false, "Acceptance list is specified but parent is not matched.");
						}
						else if (objectGraphPolicyModel.Priority < parentObjectGraphPolicyModel.Priority)
						{
							return new ValidationResult(false, "Parent GraphElement has a higher graph priority.");
						}

						if (parentObjectGraphPolicyModel.ObjectType == objectGraphPolicyModel.ObjectType)
							if (objectGraphPolicyModel.SubTreePolicy == SubTreePolicy.DoNotAllowSubTree)
								return new ValidationResult(false, "Graph policy does not allow sub tree.");
					}

					// If any acceptance list exists it must be satisfied
					if (objectGraphPolicyModel != null && (objectGraphPolicyModel.ParentAcceptablePriorities.Count() > 0 || objectGraphPolicyModel.ParentAcceptableTypes.Count() > 0))
					{
						if (parentObjectGraphPolicyModel != null && objectGraphPolicyModel.ParentAcceptablePriorities.Contains(parentObjectGraphPolicyModel.Priority)) //objectGraphPolicyModel.ParentPriorityAcceptanceList.Count() > 0)
							return new ValidationResult(true);

						// new ValidationResult(false, "The specified graph policy parent priority acceptance list is not satisfied.");
						//foreach (int acceptedParentPriority in objectGraphPolicyModel.ParentPriorityAcceptanceList)
						//	if (parentObjectGraphPolicyModel.Priority == acceptedParentPriority)
						//		return new ValidationResult(true);

						//return new ValidationResult(false, "The specified graph policy parent priority acceptance list is not satisfied.");
						if (parentObjectGraphPolicyModel != null && objectGraphPolicyModel.ParentAcceptableTypes.FirstOrDefault(type => parentObjectGraphPolicyModel.ObjectType == type || parentObjectGraphPolicyModel.ObjectType.IsSubclassOf(type)) != null)
							return new ValidationResult(true);

						return new ValidationResult(false, "The specified graph policy parent is not accepted.");
					}
					else
					{
						// Check if neighbor policy is accepted
						SimpleObjectCollection<GraphElement> neighbourGraphElements = (realParentGraphElement != null) ? this.GetGraphElements(graphKey, realParentGraphElement.Id) 
																													   : graph.GetRootGraphElements();

						//if (neighbourGraphElements.Count < 5)
						//{
							foreach (GraphElement neighborGraphElement in neighbourGraphElements)
							{
								if (neighborGraphElement.SimpleObject is Folder) // SimpleObjectTableId == FolderModel.TableId) //.SimpleObject is Folder) // Allow Folder to be in group with any objects.
									continue;

								IGraphPolicyModelElement? neighborObjectGraphPolicyModel = graphPolicyModel.GetGraphPolicyModelElement(neighborGraphElement.SimpleObject!.GetType());

								if (neighborObjectGraphPolicyModel != null && objectGraphPolicyModel != null && neighborObjectGraphPolicyModel.Priority < objectGraphPolicyModel.Priority)
								{
									// Check if parent object is acepted in ParentPriorityAcceptanceList or ParentTypeAcceptanceList definitions
									if (!objectGraphPolicyModel.ParentAcceptablePriorities.Contains(neighborObjectGraphPolicyModel.Priority) || !objectGraphPolicyModel.ParentAcceptableTypes.Contains(neighborObjectGraphPolicyModel.ObjectType))
										return new ValidationResult(false, "Some child GraphElement has a lower graph priority.");
								}
							}
						//}
					}

					if (parentObjectGraphPolicyModel != null && objectGraphPolicyModel != null && objectGraphPolicyModel.Priority < parentObjectGraphPolicyModel.Priority)
						return new ValidationResult(false, "Some child graph element(s) has higer graph priority");
				}
			}

			return new ValidationResult(true);
		}

		protected internal virtual ValidationResult ValidateGraphNormalization(GraphElement graphElement)
		{
			GraphElement? parent = graphElement.Parent;

			// If new graph element has model priority greater than its neighbors (has same parent), replace neighbors to be its children.
			IGraphPolicyModel? graphPolicyModel = this.ModelDiscovery.GetGraphPolicyModel(graphElement.GraphKey);
			
			if (graphPolicyModel is null)
				return new ValidationResult(false, "GraphElement policy model cannot be found: GraphKey=" + graphElement.GraphKey);

			IGraphPolicyModelElement? objectGraphPolicyModel = graphPolicyModel.GetGraphPolicyModelElement(graphElement.SimpleObject!.GetType());

			lock (this.lockObject)
			{
				if (objectGraphPolicyModel != null && parent == null && !objectGraphPolicyModel.CanBeInRoot)
					return new ValidationResult(false, "Element cannot be in root.");

				// ...This is already checked in ValidateGraphPolicy...
				//if (objectGraphPolicyModel.ParentPriorityAcceptanceList.Count() > 0)
				//{
				//	IGraphPolicyModelElement parentObjectGraphPolicyModel = graphPolicyModel.GetGraphPolicyModelElement(parent.SimpleObject.GetType());

				//	if (!objectGraphPolicyModel.ParentPriorityAcceptanceList.Contains(parentObjectGraphPolicyModel.Priority))
				//		return new ValidationResult(false, "The specified graph policy parent priority acceptance list is not satisfied.");
				//}

				foreach (GraphElement neighborGraphElement in graphElement.GetNeighbours())
				{
					if (neighborGraphElement == graphElement)
						continue;

					if (objectGraphPolicyModel != null)
					{
						IGraphPolicyModelElement? neighborObjectGraphPolicyModel = graphPolicyModel.GetGraphPolicyModelElement(neighborGraphElement.SimpleObject!.GetType());

						if (neighborObjectGraphPolicyModel != null && objectGraphPolicyModel.Priority != neighborObjectGraphPolicyModel.Priority) // Normalized graph: accept only elements with the same priority to be a neighbor
							return new ValidationResult(false, "Neighbor graph elements must be with the same graph priority");
					}
				}

				return new ValidationResult(true);
			}
		}

		public virtual bool CanGraphElementChangeParent(GraphElement graphElement, GraphElement? newParentGraphElement) //, bool enforceSetParentAndValidate)
		{
			lock (lockObject)
			{
				bool isValidationTest = graphElement.SimpleObject?.IsValidationTest ?? false;
				
				if (graphElement.SimpleObject != null)
					graphElement.SimpleObject.IsValidationTest = true;

				bool result = this.CanAddGraphElement(graphElement.GraphKey, graphElement.SimpleObject.GetType(), newParentGraphElement);

				if (result == true)
				{
					if (newParentGraphElement != null)
					{
						// Inserting parent into child is not allowed.
						if (newParentGraphElement.HasAsParent(graphElement))
						{
							graphElement.SimpleObject.IsValidationTest = isValidationTest;

							return false;
						}

						//GraphElement parent = newParentGraphElement.Parent;

						//while (parent != null)
						//{
						//	if (parent == graphElement)
						//	{
						//		graphElement.SimpleObject.IsValidationTest = isValidationTest;

						//		return false;
						//	}

						//	parent = parent.Parent;
						//}
					}

					result = this.ValidateGraphPolicy(graphElement, newParentGraphElement).Passed;
					//result = this.IsGraphElementNormalizedByModelIfChangeParent(graphElement, newParentGraphElement);

					//if (enforceSetParentAndValidate && result == true)
					//{
					//	GraphElement originalParentGraphElement = graphElement.Parent;
					//	int orderIndex = graphElement.OrderIndex;

					//	//graphElement.Parent = newParentGraphElement;
					//	graphElement.SetParentWithoutCheckingCanGraphElementChangeParentOrRaisingEvents(newParentGraphElement);

					//	try
					//	{
					//                       SimpleObjectValidationResult validationResult = graphElement.SimpleObject.Validate();
					//		result = validationResult.Passed;
					//	}
					//	catch
					//	{
					//		result = false;
					//	}
					//	finally
					//	{
					//		//graphElement.Parent = originalParentGraphElement;
					//		graphElement.SetParentWithoutCheckingCanGraphElementChangeParentOrRaisingEvents(originalParentGraphElement);
					//		graphElement.SetOrderIndex(orderIndex, requester: this);
					//	}
					//}
				}

				//foreach (SimpleObjectDependencyAction objectDependencyAction in this.ObjectDependencyActions)
				//{
				//    if (objectDependencyAction.Match(graphElement.SimpleObject))
				//    {
				//        result &= objectDependencyAction.CanGraphElementChangeParent(graphElement, newParentGraphElement);
				//    }
				//}

				graphElement.SimpleObject.IsValidationTest = isValidationTest;

				return result;
			}
		}

		//public SimpleObjectValidationResult CanSave(SimpleObject simpleObject)
		//{
		//	// Prevent saving if:
		//	// simpleObject is GraphElement and has a Parent with IsNew == true
		//	// simpleObject is not GraphElement: if any simpleObject.GraphElements has a Parent with IsNew == true
		//	ISimpleObjectModel objectModel = simpleObject.GetModel();

		//	if (simpleObject is GraphElement)
		//	{
		//		// This should be handled by the relation validation in ValidateSave method
		//		GraphElement graphElement = simpleObject as GraphElement;

		//		if (graphElement.Parent != null && graphElement.Parent.IsNew && !graphElement.RelatedTransactionRequests.ContainsKey(graphElement.Parent))
		//			return new SimpleObjectValidationResult(simpleObject, false, "GraphElement Parent is not saved yet.", null);
		//	}
		//	else
		//	{
		//		// Prevent saving SimpleObject if model require having graph element
		//		if (objectModel.MustHaveAtLeastOneGraphElement)
		//		{
		//			if (simpleObject.GraphElements.Count == 0)
		//				return new SimpleObjectValidationResult(simpleObject, false, "SimpleObject must have at least one GraphElement.", null);

		//			if (objectModel.MustHaveGraphElementGraphKey > 0 && simpleObject.GetGraphElement(objectModel.MustHaveGraphElementGraphKey) == null)
		//				return new SimpleObjectValidationResult(simpleObject, false, "SimpleObject must have GraphElement with GraphKey=" + objectModel.MustHaveGraphElementGraphKey + ".", null);
		//		}

		//		// Prevent saving SimpleObject if GraphElement's parent is not saved
		//		foreach (GraphElement graphElement in simpleObject.GraphElements)
		//		{
		//			if (graphElement.Parent != null && graphElement.Parent.IsNew && !graphElement.RelatedTransactionRequests.ContainsKey(graphElement.Parent))
		//				return new SimpleObjectValidationResult(simpleObject, false, "SimpleObject's GraphElement Parent is not saved yet.", new PropertyValidationResult(false, "Parent GraphElement is not saved.", GraphElementModel.PropertyModel.ParentId));
		//		}
		//	}

		//	// Check if there is foreign keys that came from new objects that are not saved yet.
		//	//IObjectRelationModel objectRelationModel = this.GetObjectRelationModel(simpleObject.GetType());

		//	// OneToOne relations
		//	foreach (IOneToOneRelationModel oneToOneRelationModel in simpleObject.GetModel().RelationModel.OneToOneForeignObjects) // objectRelationModel.OneToOneRelationForeignKeyObjectDictionary)
		//	{
		//		//int relationKey = relationItem.Key;
		//		//IOneToOneRelationModel relationPolicyModel = relationItem.Value;
		//		SimpleObject foregnObject = simpleObject.GetOneToOneRelationForeignObject<SimpleObject>(oneToOneRelationModel);

		//		if (foregnObject == null)
		//		{
		//			if (!oneToOneRelationModel.CanBeNull)
		//				return new SimpleObjectValidationResult(simpleObject, false, null, new PropertyValidationResult(false, "Related foreign object " + oneToOneRelationModel.ForeignObjectType.ToString() + " cannot be null.", oneToOneRelationModel.ForeignIdPropertyModel));
		//		}
		//		//else if (foregnObject.IsNew)
		//		//{
		//		//	return new SimpleObjectValidationResult(simpleObject, false, null, new ValidationRuleResult(false, "Related foreign object " + oneToOneRelationModel.ForeignObjectType.ToString() + " is not saved.", oneToOneRelationModel.ForeignKeyPropertyModel));
		//		//}
		//	}

		//	// OneToMany relations
		//	foreach (IOneToManyRelationModel oneToManyRelationModel in simpleObject.GetModel().RelationModel.OneToManyForeignObjects) // objectRelationModel.OneToManyRelationForeignKeyObjectDictionary)
		//	{
		//		//int relationKey = relationItem.Key;
		//		//IOneToManyRelationModel relationPolicyModel = relationItem.Value;
		//		SimpleObject foregnObject = simpleObject.GetOneToManyRelationForeignObject<SimpleObject>(oneToManyRelationModel);

		//		if (foregnObject == null)
		//		{
		//			if (!oneToManyRelationModel.CanBeNull)
		//				return new SimpleObjectValidationResult(simpleObject, false, null, new PropertyValidationResult(false, "Related foreign object " + oneToManyRelationModel.ForeignObjectType.ToString() + " cannot be null.", oneToManyRelationModel.ForeignIdPropertyModel));
		//		}
		//		//else if (foregnObject.IsNew)
		//		//{
		//		//	return new SimpleObjectValidationResult(simpleObject, false, null, new ValidationRuleResult(false, "Related foreign object " + oneToManyRelationModel.ForeignObjectType.ToString() + " is not saved.", oneToManyRelationModel.ForeignKeyPropertyModel));
		//		//}
		//	}

		//	// ManyToMany relations - not yet implemented

		//	return new SimpleObjectValidationResult(simpleObject, true, String.Empty, null);
		//}

		public virtual bool CanDelete(SimpleObject simpleObject) //, bool checkSimpleObjectGraphElements = true)
		{
			// TODO: Add all Graph roules here, e.g. DeleteAllSimpleObjectGraphElementsOnOneGraphElementDelete, DeleteSimpleObjectOnLastGraphElementDelete etc. (see GetRelatedObjectsForDelete method)

			//if (!this.ValidateDelete(simpleObject).Passed)
			//	return false;

			lock (this.lockObject)
			{
				bool canDelete = true;

				if (simpleObject is GraphElement) // & checkSimpleObjectGraphElements)
				{
					GraphElement graphElement = simpleObject as GraphElement;

					//if (graphElement.ParentGraphElements.Count == 1) // no neighbors, childs will change parent on delete request
					//{
					//	canDelete = true;
					//}
					//else
					//{
					canDelete &= graphElement.GraphElements.Count == 0;   //this.CanDeleteGraphElement(simpleObject as GraphElement);
																		  //}
				}

				return canDelete;
			}
		}

		//public virtual SimpleObjectValidationResult ValidateDelete(SimpleObject simpleObject)
		//{
		//	return this.ValidateDelete(simpleObject, this.DefaultChangeContainer.TransactionRequests);
		//}

		public virtual SimpleObjectValidationResult ValidateDelete(SimpleObject simpleObject, IDictionary<SimpleObject, TransactionRequestAction> transactionRequests)
		{
			lock (this.lockObject)
			{
				ISimpleObjectModel objectModel = this.GetObjectModel(simpleObject.GetType());

				foreach (ValidationRule validationRule in objectModel.DeleteValidationRules)
				{
					ValidationResult validationResult = validationRule.Validate(simpleObject, transactionRequests);

					if (!validationResult.Passed)
						return new SimpleObjectValidationResult(simpleObject, false, validationResult.Message, validationResult, TransactionRequestAction.Save);
				}

				if (simpleObject is GraphElement)
				{
					bool canDelete = (simpleObject as GraphElement).GraphElements.Count == 0;   //this.CanDeleteGraphElement(simpleObject as GraphElement);

					if (!canDelete)
						foreach (GraphElement graphElement in (simpleObject as GraphElement).GraphElements)
							if (!transactionRequests.Keys.Contains(graphElement))
								return new SimpleObjectValidationResult(simpleObject, false, String.Format("{0} ({1}) delete validation failed: Child graph elements will not be saved.", simpleObject.GetName(), simpleObject.GetType().Name), null, TransactionRequestAction.Delete);
				}

				// We need to check if any of foreign key exists in any related objects and is not in transaction requests for delete
				// one to one
				foreach (IOneToOneRelationModel oneToOneRelationModel in simpleObject.GetModel().RelationModel.AsPrimaryObjectInOneToOneRelations)
				{
					SimpleObject foreignObject = simpleObject.GetOneToOneForeignObject(oneToOneRelationModel.RelationKey);

					if (foreignObject != null && !(transactionRequests.ContainsKey(foreignObject) && transactionRequests[foreignObject] == TransactionRequestAction.Delete))
						return new SimpleObjectValidationResult(simpleObject, false, String.Format("{0} ({1}) delete validation failed: Key holder object from {2} relation will not be deleted.", simpleObject.GetName(), simpleObject.GetType().Name, oneToOneRelationModel.Name), null, TransactionRequestAction.Delete);
				}

				// one to many
				foreach (IOneToManyRelationModel oneToManyRelationModel in simpleObject.GetModel().RelationModel.AsPrimaryObjectInOneToManyRelations)
				{
					SimpleObjectCollection foreignCollection = simpleObject.GetOneToManyForeignObjectCollection(oneToManyRelationModel.RelationKey);

					foreach (SimpleObject foreignObject in foreignCollection)
						if (foreignObject != null && !(transactionRequests.ContainsKey(foreignObject) && transactionRequests[foreignObject] == TransactionRequestAction.Delete))
							return new SimpleObjectValidationResult(simpleObject, false, String.Format("{0} ({1}) delete validation failed: Key holder object from {2} relation will not be deleted.", simpleObject.GetName(), simpleObject.GetType().Name, oneToManyRelationModel.Name), null, TransactionRequestAction.Delete);
				}

				// many to many
				foreach (IManyToManyRelationModel relationModel in simpleObject.GetModel().RelationModel.AsObjectInGroupMembership)
				{
					SimpleObjectCollection groupMemberCollection = simpleObject.GetGroupMemberCollection(relationModel.RelationKey);

					foreach (GroupMembership groupMembership in simpleObject.GetGroupMemberCollection(relationModel.RelationKey).GetGroupMembershipCollection())
						if (groupMembership != null && !(transactionRequests.ContainsKey(groupMembership) && transactionRequests[groupMembership] == TransactionRequestAction.Delete))
							return new SimpleObjectValidationResult(simpleObject, false, String.Format("{0} ({1}) delete validation failed: Group membership relation {2} contains this object and will not be deleted.", simpleObject.GetName(), simpleObject.GetType().Name, relationModel.Name), null, TransactionRequestAction.Delete);
				}

				return SimpleObjectValidationResult.DefaultSuccessResult;
			}
		}

		//public virtual bool CanDeleteGraphElement(GraphElement graphElement, bool checkSimpleObjectGraphElements)
		//{
		//	lock (lockObject)
		//	{
		//		if (checkSimpleObjectGraphElements)
		//			return graphElement.GraphElements.Count == 0;

		//		return true;
		//	}
		//}

		//public virtual bool CanDeleteGraphElement(GraphElement graphElement)
		//{
		//    lock (lockObject)
		//    {
		//        bool canDelete = graphElement.GraphElements.Count == 0;

		//        //foreach (SimpleObjectDependencyAction objectDependencyAction in this.ObjectDependencyActions)
		//        //{
		//        //    if (objectDependencyAction.Match(graphElement.SimpleObject))
		//        //    {
		//        //        canDelete &= objectDependencyAction.CanDeleteGraphElement(graphElement);
		//        //    }
		//        //}

		//        return canDelete;
		//    }
		//}

		// TODO: Remove this method
		//public virtual SimpleObjectValidationResult Validate_OLD(SimpleObject simpleObject)
		//      {
		//          ISimpleObjectModel objectModel = this.GetObjectModel(simpleObject.GetType());
		//          SimpleObjectValidationResult validationResult = null;

		//          lock (this.lockObject)
		//          {
		//              foreach (ValidationRule validationRoule in objectModel.ValidationRules)
		//              {
		//                  ValidationResult validationRuleResult = validationRoule.Validate(simpleObject, null);

		//                  if (!validationRuleResult.Passed)
		//                  {
		//                      validationResult = new SimpleObjectValidationResult(simpleObject, false, validationRuleResult.Message, validationRuleResult);
		//                      break;
		//                  }
		//              }
		//	}

		//          if (validationResult == null)
		//              validationResult = new SimpleObjectValidationResult(simpleObject, true, "", null);

		//          return validationResult;
		//      }

		//public virtual SimpleObjectValidationResult ValidateSave(SimpleObject simpleObject)
		//{
		//	return this.ValidateSave(simpleObject, this.DefaultChangeContainer.TransactionRequests);
		//}

		public virtual SimpleObjectValidationResult ValidateSave(SimpleObject simpleObject, IDictionary<SimpleObject, TransactionRequestAction> transactionRequests)
		{
			lock (this.lockObject)
			{
				ISimpleObjectModel objectModel = this.GetObjectModel(simpleObject.GetType());

				foreach (ValidationRule validationRule in objectModel.UpdateValidationRules)
				{
					ValidationResult validationResult = validationRule.Validate(simpleObject, transactionRequests);

					if (!validationResult.Passed)
						return new SimpleObjectValidationResult(simpleObject, false, validationResult.Message, validationResult, TransactionRequestAction.Save);
				}

				// OneToOne relations
				foreach (IOneToOneRelationModel oneToOneRelationModel in simpleObject.GetModel().RelationModel.AsForeignObjectInOneToOneRelations) // objectRelationModel.OneToOneRelationForeignKeyObjectDictionary)
				{
					SimpleObject foregnObject = simpleObject.GetOneToOnePrimaryObject(oneToOneRelationModel.RelationKey);

					if (foregnObject == null)
					{
						if (!oneToOneRelationModel.CanBeNull)
							return new SimpleObjectValidationResult(simpleObject, false, null, new PropertyValidationResult(false, "Related foreign object " + oneToOneRelationModel.PrimaryObjectType.ToString() + " cannot be null.", oneToOneRelationModel.PrimaryObjectIdPropertyModel), TransactionRequestAction.Save);
					}
					else
					{
						TransactionRequestAction requestAction;

						if (transactionRequests.TryGetValue(foregnObject, out requestAction))
						{
							if (requestAction != TransactionRequestAction.Save)
								return new SimpleObjectValidationResult(simpleObject, false, null, new PropertyValidationResult(false, "Related foreign object " + oneToOneRelationModel.PrimaryObjectType.ToString() + " will be deleted by current transaction.", oneToOneRelationModel.PrimaryObjectIdPropertyModel), TransactionRequestAction.Save);
						}
						else if (foregnObject.IsNew) // is new but not included in transaction action
						{
							return new SimpleObjectValidationResult(simpleObject, false, null, new PropertyValidationResult(false, "Related foreign object " + oneToOneRelationModel.PrimaryObjectType.ToString() + " must be saved also.", oneToOneRelationModel.PrimaryObjectIdPropertyModel), TransactionRequestAction.Save);
						}


						if (!foregnObject.IsStorable)
							return new SimpleObjectValidationResult(simpleObject, false, null, new PropertyValidationResult(false, "Related foreign object " + oneToOneRelationModel.PrimaryObjectType.ToString() + " is not storable", oneToOneRelationModel.PrimaryObjectIdPropertyModel), TransactionRequestAction.Save);
					}
				}

				// OneToMany relations
				foreach (IOneToManyRelationModel oneToManyRelationModel in simpleObject.GetModel().RelationModel.AsForeignObjectInOneToManyRelations) // objectRelationModel.OneToManyRelationForeignKeyObjectDictionary)
				{
					SimpleObject foregnObject = simpleObject.GetOneToManyPrimaryObject(oneToManyRelationModel.RelationKey);

					if (foregnObject == null)
					{
						if (!oneToManyRelationModel.CanBeNull)
							return new SimpleObjectValidationResult(simpleObject, false, null, new PropertyValidationResult(false, "Related foreign object " + oneToManyRelationModel.PrimaryObjectType.ToString() + " cannot be null.", oneToManyRelationModel.PrimaryObjectIdPropertyModel), TransactionRequestAction.Save);
					}
					else
					{
						TransactionRequestAction requestAction;

						if (transactionRequests.TryGetValue(foregnObject, out requestAction))
						{
							if (requestAction != TransactionRequestAction.Save)
								return new SimpleObjectValidationResult(simpleObject, false, null, new PropertyValidationResult(false, "Related foreign object " + oneToManyRelationModel.PrimaryObjectType.ToString() + " will be deleted by current transaction.", oneToManyRelationModel.PrimaryObjectIdPropertyModel), TransactionRequestAction.Save);
						}
						else if (foregnObject.IsNew) // foregnObject is new but not included in transaction action
						{
							return new SimpleObjectValidationResult(simpleObject, false, null, new PropertyValidationResult(false, "Related foreign object " + oneToManyRelationModel.PrimaryObjectType.ToString() + " must be saved also.", oneToManyRelationModel.PrimaryObjectIdPropertyModel), TransactionRequestAction.Save);
						}

						if (!foregnObject.IsStorable)
							return new SimpleObjectValidationResult(simpleObject, false, null, new PropertyValidationResult(false, "Related foreign object " + oneToManyRelationModel.PrimaryObjectType.ToString() + " is not storable", oneToManyRelationModel.PrimaryObjectIdPropertyModel), TransactionRequestAction.Save);
					}
				}

				// Prevent saving SimpleObject if model require having graph element
				if (objectModel.MustHaveAtLeastOneGraphElement)
				{
					if (simpleObject.GraphElements.Count == 0)
						return new SimpleObjectValidationResult(simpleObject, false, "SimpleObject must have at least one GraphElement.", null, TransactionRequestAction.Save);

					if (objectModel.MustHaveGraphElementGraphKey > 0 && simpleObject.GetGraphElement(objectModel.MustHaveGraphElementGraphKey) == null)
						return new SimpleObjectValidationResult(simpleObject, false, "SimpleObject must have GraphElement with GraphKey=" + objectModel.MustHaveGraphElementGraphKey + ".", null, TransactionRequestAction.Save);
				}

				//if (simpleObject is GraphElement)
				//{
				//	GraphElement graphElement = simpleObject as GraphElement;
				//	string name = graphElement.GetName();

				//	if (graphElement.GetNeighbours().Where(ge => ge.GetName() == name).Count() > 1)
				//		return new SimpleObjectValidationResult(graphElement.SimpleObject, false, null, new PropertyValidationResult(false, "The name within graph elements must be unique.", graphElement.SimpleObject.GetModel().NamePropertyModel), TransactionRequestAction.Save);

				//}

				return SimpleObjectValidationResult.DefaultSuccessResult;
			}
		}

		private bool IsGraphElementNormalizedByModelIfChangeParent(GraphElement graphElement, GraphElement newParentGraphElement)
		{
			if (graphElement != null && newParentGraphElement != null && graphElement.Graph.GraphKey != newParentGraphElement.Graph.GraphKey)
				return false;
			//if (graphElement.SimpleObject is Folder) // Folder can be in the same level with other objects
			//    return;

			// If new graph element has model priority greater than its neighbors (has same parent), replace neighbors to be its child.
			IGraphPolicyModel? graphPolicyModel = this.ModelDiscovery.GetGraphPolicyModel(graphElement.GraphKey);

			if (graphPolicyModel is null)
				return false;

			IGraphPolicyModelElement? graphElementPolicyModel = graphPolicyModel.GetGraphPolicyModelElement(graphElement.SimpleObject.GetType());

			lock (this.lockObject)
			{
				//bool isValidationTest = graphElement.SimpleObject.IsValidationTest;
				//graphElement.SimpleObject.IsValidationTest = true;

				//List<GraphElement> neighborGraphElements = new List<GraphElement>();

				//foreach (GraphElement neighborGraphElement in graphElement.ParentGraphElements)
				//{
				//	if (neighborGraphElement != graphElement)
				//	{
				//		neighborGraphElements.Add(neighborGraphElement);
				//	}
				//}

				if (newParentGraphElement == null && graphElementPolicyModel != null && !graphElementPolicyModel.CanBeInRoot)
					return false;

				if (graphElementPolicyModel != null && (graphElementPolicyModel.ParentAcceptablePriorities.Count() > 0 || graphElementPolicyModel.ParentAcceptableTypes.Count() > 0))
				{
					if (newParentGraphElement == null)
						return false;

					Type newParentObjectType = newParentGraphElement.SimpleObject!.GetType();
					IGraphPolicyModelElement? newParentGraphElementPolicyModel = graphPolicyModel.GetGraphPolicyModelElement(newParentObjectType);

					if (newParentGraphElementPolicyModel != null && !graphElementPolicyModel.ParentAcceptablePriorities.Contains(newParentGraphElementPolicyModel.Priority) &&
						graphElementPolicyModel.ParentAcceptableTypes.FirstOrDefault(type => newParentObjectType == type || newParentObjectType.IsSubclassOf(type)) == null)
						return false;
				}

				SimpleObjectCollection<GraphElement> neighbourGraphElements = (newParentGraphElement != null) ? newParentGraphElement.GraphElements : graphElement.Graph.GetRootGraphElements();

				foreach (GraphElement neighborGraphElement in neighbourGraphElements)
				{
					if (neighborGraphElement == graphElement)
						continue;

					if (graphElementPolicyModel != null)
					{
						IGraphPolicyModelElement? neighborGraphPolicyModelElement = graphPolicyModel.GetGraphPolicyModelElement(neighborGraphElement.SimpleObject!.GetType());

						if (neighborGraphPolicyModelElement != null && graphElementPolicyModel.Priority != neighborGraphPolicyModelElement.Priority) // Normalized graph required - only elements with the same priority are accepted in an graph collection
							return false;
					}

					// TODO: Check whole graph policy model to enforce graph normalization: Parent acceptance priority list
				}

				//graphElement.SimpleObject.IsValidationTest = isValidationTest;

				return true;
			}
		}

		#endregion |   Can & Validation   |

		#region |   Save   |

		//public bool Save(SimpleObject simpleObject, object requester)
		//      {
		//	//bool isSaved = false;

		//	if (simpleObject == null)
		//		return false;

		//	this.DefaultChangeContainer.Set(simpleObject, TransactionRequestAction.Save);

		//	User transactionUser = (requester is User) ? requester as User : this.Users.SystemAdmin;
		//	TransactionResult result = this.CommitChanges(this.DefaultChangeContainer, transactionUser.Id, requester);

		//	return result.TransactionSucceeded;

		//	//lock (lockObject)
		//	//{
		//	//	//if (this.CurrentTransaction != null)
		//	//	//	return false;

		//	//	if (!simpleObject.IsStorable || !simpleObject.GetModel().IsStorable)
		//	//		return false;

		//	//	if (this.ActiveTransaction == null)
		//	//	{
		//	//		lock (this.lockObject)
		//	//		{
		//	//			if (!this.RequireSaving(simpleObject))
		//	//				return false;

		//	//			bool canBeSaved = this.Validate_OLD(simpleObject).Passed && this.CanSave(simpleObject as SimpleObject).Passed;

		//	//			if (!canBeSaved)
		//	//				return false;

		//	//			//if (!(simpleObject is SystemSimpleObject))

		//	//			transactionUser = (requester is User) ? requester as User : this.Users.SystemAdmin;

		//	//			this.BeginTransaction(transactionUser);

		//	//			isSaved = this.SaveInternal(simpleObject, validateObject: false, requester: requester);
		//	//		}

		//	//		if (isSaved)
		//	//		{
		//	//			//if (!(simpleObject is SystemSimpleObject))
		//	//			this.EndTransaction(requester);
		//	//		}
		//	//		else
		//	//		{
		//	//			lock (this.lockObject)
		//	//			{
		//	//				this.lastTransaction = this.ActiveTransaction;
		//	//				this.ActiveTransaction = null;
		//	//				//this.oldTransactionsActionsBySimpleObject = this.transactionsActionsBySimpleObject;
		//	//				//this.transactionsActionsBySimpleObject.Clear();
		//	//			}
		//	//		}
		//	//	}
		//	//	else
		//	//	{
		//	//		isSaved = this.SaveInternal(simpleObject, requester);
		//	//	}
		//	//}

		//	//return isSaved;
		//      }

		public bool RequireSaving(SimpleObject simpleObject)
		{
			bool result = simpleObject.RequireSaving();

			if (!result)
			{
				if (simpleObject is GraphElement graphElement)
				{
					if (graphElement.SimpleObject is null)
						return false;
					
					result = graphElement.SimpleObject.RequireSaving();

					if (!result)
						foreach (GraphElement item in graphElement.SimpleObject.GraphElements)
							if (item.RequireSaving())
								return true;
				}
				else
				{
					foreach (GraphElement item in simpleObject.GraphElements)
						if (item.RequireSaving())
							return true;
				}
			}

			return result;
		}

		//      public bool SaveInternal(SimpleObject simpleObject, object requester)
		//      {
		//          return this.SaveInternal(simpleObject, validateObject: true, requester: requester);
		//      }

		//protected virtual bool SaveInternal(SimpleObject simpleObject, bool validateObject, object requester)
		//      {

		//	bool isSaved = false;

		//	//lock (this.lockObject)
		//	//{
		//		if (this.ActiveTransaction.TransactionRequestsBySimpleObject.ContainsKey(simpleObject))
		//			return true;

		//		//if (!simpleObject.IsStorable)
		//		//	return false;

		//		//try
		//		//{
		//		if (simpleObject.IsNew)
		//		{
		//			// Enforce Key creation if not created...
		//			// Guid objectKey = simpleObject.Guid;
		//			long objectId = simpleObject.Id;
		//		}

		//	//if (simpleObject is SystemSimpleObject)
		//	//{
		//	//	//lock (lockObject)
		//	//	//{
		//	//	isSaved = base.SaveInternal(requester, simpleObject);
		//	//	//}
		//	//}
		//	//else
		//	//{
		//	if (!simpleObject.IsStorable || !simpleObject.GetModel().IsStorable)
		//		return true;

		//	bool validationPassed = (validateObject) ? this.Validate_OLD(simpleObject).Passed : true;

		//	if (validationPassed)
		//	{
		//		bool canBeSaved = true;

		//		if (validateObject)
		//		{
		//			SimpleObjectValidationResult validetionResult = this.CanSave(simpleObject);

		//			canBeSaved = validetionResult.Passed;
		//		}

		//		if (canBeSaved)
		//		{
		//			if (simpleObject is GraphElement)
		//			{
		//				GraphElement graphElement = simpleObject as GraphElement;

		//				if (graphElement.Parent != null && !graphElement.Parent.IsNew)
		//				{
		//					// Enforce saving GraphElement BusinessObject to be consistent with the current validation which includes both GraphElement and BusinessObject. 
		//					//Validation is not required for BusinessObject while it is done by GraphElement validation.
		//					if (graphElement.SimpleObject.IsStorable && graphElement.SimpleObject.GetModel().IsStorable && graphElement.SimpleObject.RequireSaving())
		//						isSaved = this.SaveInternal(graphElement.SimpleObject, requester);
		//				}

		//				isSaved |= this.SaveInternalBase(simpleObject, requester);
		//			}
		//			else
		//			{
		//				isSaved = this.SaveInternalBase(simpleObject, requester);

		//				foreach (GraphElement graphElement in simpleObject.GraphElements)
		//					isSaved |= this.SaveInternal(graphElement, validateObject, requester: requester);
		//			}

		//			//	//if (graphElement.Parent != null && !graphElement.Parent.IsNew)
		//			//	//{
		//			//	// Enforce saving GraphElement SimpleObject to be consistent with the current validation whitch includes both GraphElement and SimpleObject. 
		//			//	// Validation is not required for SimpleObject while it is done by GraphElement validation.
		//			//	if (graphElement.SimpleObject.GetModel().IsStorable && graphElement.SimpleObject.RequireSaving()) // (graphElement.SimpleObject.IsNew || graphElement.SimpleObject.IsChanged))
		//			//	{
		//			//		bool graphElementSimpleObjectValidation = (validateObject) ? this.Validate(graphElement.SimpleObject).Passed : true;

		//			//		if (graphElementSimpleObjectValidation)
		//			//		{
		//			//			bool graphElementSimpleObjectCanBeSaved =  true;

		//			//			if (validateObject)
		//			//				graphElementSimpleObjectCanBeSaved = this.CanSave(graphElement.SimpleObject).Passed;

		//			//			if (graphElementSimpleObjectCanBeSaved)
		//			//			{
		//			//				isSaved = this.SaveInternalBase(requester, graphElement.SimpleObject);
		//			//			}
		//			//			else
		//			//			{
		//			//				isSaved = false;
		//			//			}
		//			//		}
		//			//		else
		//			//		{
		//			//			isSaved = false;
		//			//		}
		//			//	}
		//			//	else
		//			//	{
		//			//		isSaved = true;
		//			//	}

		//			//	if (isSaved)	
		//			//		isSaved = this.SaveInternalBase(requester, simpleObject);
		//			//	//}
		//			//}
		//			//else
		//			//{
		//			//	if (simpleObject.GetModel().IsStorable && simpleObject.RequireSaving())
		//			//		isSaved = this.SaveInternalBase(requester, simpleObject);

		//			//	foreach (GraphElement graphElement in simpleObject.GraphElements)
		//			//		if (graphElement.RequireSaving())
		//			//			isSaved |= this.SaveInternalBase(requester, graphElement);
		//			//}

		//		}
		//		else
		//		{
		//			isSaved = false;
		//		}
		//	}

		//	//}
		//          //}
		//          //catch (Exception ex)
		//          //{
		//          //    string errorMessage = Simple.ExceptionHelper.GetFullErrorMessage(ex);
		//          //    System.Windows.Forms.XtraMessageBox.Show(errorMessage, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
		//          //}

		//	//if (isSaved)
		//	//{
		//	//	foreach (TransactionActionRequest transactionActionRequest in simpleObject.RelatedTransactionRequests)
		//	//	{
		//	//		if (this.transactionsActionsBySimpleObject.ContainsKey(transactionActionRequest.SimpleObject))
		//	//			continue;

		//	//		switch (transactionActionRequest.TransactionActionRequestType)
		//	//		{
		//	//			case TransactionActionRequestType.Save:
		//	//				this.SaveInternal(requester, transactionActionRequest.SimpleObject);
		//	//				break;

		//	//			case TransactionActionRequestType.Delete:
		//	//				this.DeleteInternal(requester, transactionActionRequest.SimpleObject);
		//	//				break;
		//	//		}
		//	//	}

		//	//	simpleObject.RelatedTransactionRequests.Clear();
		//	//}

		//	//if (isSaved)
		//	//{

		//	//	foreach (var item in simpleObject.RelatedTransactionRequests)
		//	//	{
		//	//		SimpleObject relatedSimpleObject = item.Key;
		//	//		TransactionRequestAction requestAction = item.Value;

		//	//		if (requestAction == TransactionRequestAction.Save)
		//	//		{
		//	//			isSaved |= this.SaveInternal(relatedSimpleObject, validateObject: true, requester: requester);
		//	//		}
		//	//		else if (requestAction == TransactionRequestAction.Delete)
		//	//		{
		//	//			this.DeleteInternal(relatedSimpleObject, requester);
		//	//		}
		//	//	}

		//	//	simpleObject.ClearRelatedTransactionRequests();
		//	//}

		//	return isSaved;
		//      }

		//     protected void OnSaving(SimpleObject simpleObject, object requester)
		//     {
		//         ISimpleObjectModel objectModel = simpleObject.GetModel();

		//         if (objectModel.IsStorable)
		//         {
		//	//bool createTransactionAction = !this.localDatastoreInitializationInProgress; //&& !(simpleObject is SystemSimpleObject);

		// //            if (createTransactionAction)
		// //            {
		//		//KeyValuePair<int, object>[] changedPropertyValues = simpleObject.GetChangedProperties(this.NormalizeWhenReadingByPropertyType);

		//		//int[] propertyIndexes = simpleObject.GetChangedPropertyIndexes();
		//		//object[] propertyValues = simpleObject.GetChangedPropertyValues(); //this.CreateFieldDataBasedOnObjectModelProperties(simpleObject, (simpleObject as IBindingObject).GetPropertyValueByNameDictionary(), includeObjectKeyGuid: true);

		//		if (simpleObject.IsNew) // Process Insert Transaction
		//                 {
		//                     if (simpleObject.RequireSaving()) // propertyValues.Count > 0)
		//                         this.ProcessTransactionUpdateRequest(simpleObject);
		//                 }
		//                 else // Process Update Transaction
		//                 {
		//		////IDictionary<string, object> fieldData = simpleObject.GetChangedPropertyValueByNameDictionary(); //this.CreateFieldDataBasedOnObjectModelProperties(simpleObject, (simpleObject as IBindingObject).GetChangedPropertyValueByNameDictionary(), includeObjectKeyGuid: false);
		//		//PropertyValueSequence nolrmalizedChangedOldPropertyValues = simpleObject.GetChangedOldPropertyValues(this.NormalizeWhenReadingByPropertyType); //this.CreateFieldDataBasedOnObjectModelProperties(simpleObject, (simpleObject as IBindingObject).GetChangedOldPropertyValueByNameDictionary(), includeObjectKeyGuid: false);

		//		//if (nolrmalizedChangedOldPropertyValues.Count > 0)
		//			this.ProcessTransactionUpdateRequest(simpleObject); //, nolrmalizedChangedOldPropertyValues);
		//                 }


		//	//// handle related transaction requests
		//	//foreach (var item in simpleObject.RelatedTransactionRequests.ToArray())
		//	//{
		//	//	SimpleObject relatedSimpleObject = item.Key;
		//	//	TransactionRequestAction relatedRequestAction = item.Value;

		//	//	if (relatedSimpleObject == null || relatedSimpleObject.IsDeleted || this.ActiveTransaction.TransactionRequestsBySimpleObject.ContainsKey(relatedSimpleObject))
		//	//		continue;

		//	//	switch (relatedRequestAction)
		//	//	{
		//	//		case TransactionRequestAction.Save:

		//	//			if (this.Validate_OLD(relatedSimpleObject).Passed)
		//	//				this.SaveInternal(relatedSimpleObject, validateObject: false, requester: requester);

		//	//			break;

		//	//		case TransactionRequestAction.Delete:

		//	//			if (this.CanDelete(relatedSimpleObject))
		//	//				this.DeleteInternal(relatedSimpleObject, requester);

		//	//			break;
		//	//	}
		//	//}

		//             //}
		//   //          else // Insert Object into Datastore
		//   //          {
		//   //              if (simpleObject.IsNew)
		//   //              {
		//			////IDictionary<string, object> fieldData = simpleObject.GetPropertyValuesByName(this.NormalizeForWritingByDatastoreType); //this.CreateFieldDataBasedOnObjectModelProperties(simpleObject, (simpleObject as IBindingObject).GetPropertyValueByNameDictionary(), includeObjectKeyGuid: true);
		//			//object[] fieldValues = simpleObject.GetStorablePropertyValues(this.NormalizeForWritingByDatastoreType); //this.CreateFieldDataBasedOnObjectModelProperties(simpleObject, (simpleObject as IBindingObject).GetPropertyValueByNameDictionary(), includeObjectKeyGuid: true);
		//			//this.LocalDatastore.InsertRecord(objectModel.TableInfo.TableName, objectModel.StorablePropertySequence.ModelSequence, fieldValues);
		//   //              }
		//   //              else // Update Object in Datastore
		//   //              {
		//			//PropertyValuePairs changedPropertyValues = simpleObject.GetChangedPropertyValues(this.NormalizeForWritingByDatastoreType); //this.CreateFieldDataBasedOnObjectModelProperties(simpleObject, (simpleObject as IBindingObject).GetChangedPropertyValueByNameDictionary(), includeObjectKeyGuid: false);
		//   //                  this.LocalDatastore.UpdateRecord(objectModel.TableInfo.TableName, SimpleObject.StringPropertyKey, simpleObject.Key, changedPropertyValues.PropertyModelSequence, changedPropertyValues.PropertyValues);
		//   //              }
		//   //          }
		//         }

		//         //foreach (SimpleObjectDependencyAction objectDependencyAction in this.ModelDiscovery.ObjectDependencyActions)
		//         //    if (objectDependencyAction.Match(simpleObject))
		//         //        objectDependencyAction.OnSaving(simpleObject);
		//     }

		//     protected virtual void OnAfterSave(SimpleObject simpleObject, ChangeContainer changeContainer, bool isNewBeforeSaving, object requester)
		//     {
		//         // Cascade Save BusinesObject GraphElements if required.
		//         if (isNewBeforeSaving)
		//         {
		//             if (simpleObject is GraphElement)
		//             {
		//                 // Provjeriti da li ovo uopće kad treba!!!                    
		//                 GraphElement graphElement = simpleObject as GraphElement;

		//                 foreach (GraphElement childGraphElemnt in graphElement.GraphElements)
		//                 {
		//                     if (childGraphElemnt.IsNew)
		//                     {
		//                         bool isSimpleObjectSaved = !childGraphElemnt.SimpleObject.IsNew;

		//                         if (childGraphElemnt.SimpleObject.IsNew)
		//                             isSimpleObjectSaved = this.SaveInternal(childGraphElemnt.SimpleObject, requester);

		//                         if (isSimpleObjectSaved)
		//                             this.SaveInternal(childGraphElemnt, requester);
		//                     }
		//                 }
		//             }
		//             else //if (!(simpleObject is SystemSimpleObject))
		//             {
		//                 foreach (GraphElement graphElement in simpleObject.GraphElements)
		//                 {
		//                     bool isSimpleObjectSaved = !graphElement.SimpleObject.IsNew;

		//                     if (graphElement.SimpleObject.IsNew)
		//                         isSimpleObjectSaved = this.SaveInternal(graphElement.SimpleObject, requester);

		//                     if (isSimpleObjectSaved)
		//                         this.SaveInternal(graphElement, requester);
		//                 }
		//             }

		//	simpleObject.SaveManyToManyRelatedObjectsIfCan();
		//         }
		//         else // if(!(simpleObject is GraphElement)) //if (!(simpleObject is SystemSimpleObject))
		//         {
		//             foreach (GraphElement graphElement in simpleObject.GraphElements)
		//                 if (graphElement.RequireSaving()) // .IsNew)
		//                     this.SaveInternal(graphElement, requester); // Try change to SaveInternal(this, graphElement);
		//}

		//if (simpleObject is GraphElement)
		//{
		//	(simpleObject as GraphElement).SimpleObject.AfterGraphElementIsSaved(simpleObject as GraphElement, requester);
		//	this.OnAfterGraphElementSave(simpleObject as GraphElement, requester);
		//}
		//     }

		//protected virtual void OnAfterGraphElementSave(GraphElement graphElement, object requester)
		//{
		//	//foreach (SimpleObjectDependencyAction objectDependencyAction in this.ModelDiscovery.ObjectDependencyActions)
		//	//	if (objectDependencyAction.Match(graphElement.SimpleObject))
		//	//		objectDependencyAction.OnAfterGraphElementSave(graphElement);
		//}

		/// <summary>
		/// Do action before object is saved. This method is invoked only in non server working mode.
		/// </summary>
		/// <param name="simpleObject"></param>
		/// <param name="changeContainer"></param>
		/// <param name="requester"></param>
		protected virtual void OnBeforeSave(SimpleObject simpleObject, ChangeContainer changeContainer, object? requester)
		{
		}

		internal void ActionBeforeSaving(SimpleObject simpleObject, ChangeContainer changeContainer, object? requester)
		{
			if (this.WorkingMode != ObjectManagerWorkingMode.Server)
			{
				simpleObject.BeforeSave(changeContainer, requester);
				this.OnBeforeSave(simpleObject, changeContainer, requester);
			}
		}

		#endregion |   Save   |

		#region |   Delete   |

		public void RequestDelete(SimpleObject simpleObject)
		{
			this.RequestDelete(simpleObject, requester: simpleObject);
		}

		public void RequestDelete(SimpleObject simpleObject, object requester)
		{
			this.RequestDelete(simpleObject, this.DefaultChangeContainer, requester);
		}

		public void RequestDelete(SimpleObject simpleObject, ChangeContainer changeContainer, object? requester)
		{
			this.RequestDelete(simpleObject, changeContainer, findRelatedObjectsForDelete: true, requester);
		}

		internal void RequestDelete(SimpleObject simpleObject, ChangeContainer changeContainer, bool findRelatedObjectsForDelete, object? requester)
		{
			//lock (this.lockObject)
			//{
			if (simpleObject.DeleteStarted || simpleObject.DeleteRequested)
				return;

			if (changeContainer == null)
				changeContainer = this.DefaultChangeContainer;

			changeContainer.Set(simpleObject, TransactionRequestAction.Delete, requester);
			simpleObject.DeleteIsRequested(changeContainer, requester); // new changes are not validated, so in order to rollback or restore object old saved (and thus validated) data must be used.

			if (simpleObject is GraphElement graphElement)
				graphElement.SimpleObject?.GraphElementDeleteIsRequested(graphElement, changeContainer, requester);

			if (this.WorkingMode != ObjectManagerWorkingMode.Server)
			{
				HashSet<SimpleObject> relatedObjectsForDelete = new HashSet<SimpleObject>();
				bool checkSimpleObjectGraphElements = true;
				//bool checkCanDelete = false;


				//if (!(simpleObject as SimpleObject).IsStorable)
				//	return false;

				//if (this.CurrentTransaction != null)
				//	return false;

				//if (checkCanDelete && !this.CanDelete(simpleObject))
				//	return;

				//if (simpleObject is GraphElement)
				//{
				//	if (!this.CanDelete((simpleObject as GraphElement), checkSimpleObjectGraphElements))
				//		return false;
				//}

				//if (simpleObject is SystemSimpleObject)
				//	throw new ArgumentException("For deleting SystemSimpleObject call DeleteInternal method not Delete");

				//if (!simpleObject.IsNew && this.ActiveTransaction == null)
				//{

				//ChangeContainer changeContainer = new ChangeContainer();
				//User transactionUser = (requester is User) ? requester as User : this.Users.SystemAdmin;

				//changeContainer.Set(simpleObject, TransactionRequestAction.Delete, requester);
				//	this.BeginTransaction(transactionUser);
				//	currentTransactionStarted = true;
				//	this.OnAfterStartDeleteTransaction(simpleObject, requester);

				//	//this.DeleteInternal(requester, simpleObject);
				//}
				//else
				//{
				//	this.DeleteInternal(requester, simpleObject);
				//}

				//this.StratNewTransaction();
				//this.OnAfterStartNewDeleteTransaction(simpleObject);

				this.OnRequestDelete(simpleObject, changeContainer, requester);

				if (findRelatedObjectsForDelete)
				{
					this.FindRelatedObjectsForDelete(simpleObject, ref relatedObjectsForDelete, checkSimpleObjectGraphElements);

					foreach (SimpleObject relatedSimpleObject in relatedObjectsForDelete)
						relatedSimpleObject.RequestDelete(changeContainer, requester);
				}





				//// If not contained in relatedObjectsForDelete sets all OneToOne realtion foreign objects value of simpleObject to null;
				//foreach (IOneToOneRelationModel oneToOneRelationModel in simpleObject.GetModel().RelationModel.AsForeignObjectInOneToOneRelations)
				//{
				//	SimpleObject primaryObject = simpleObject.GetOneToOnePrimaryObject(oneToOneRelationModel.RelationKey);

				//	if (primaryObject != null && !relatedObjectsForDelete.Contains(primaryObject))
				//		simpleObject.SetOneToOnePrimaryObject(primaryObject: null, oneToOneRelationModel.RelationKey, changeContainer, requester);
				//}

				//// If not contained in relatedObjectsForDelete sets all OneToMany relation foreign objects value of simpleObject to null;
				//foreach (IOneToManyRelationModel oneToManyRelationModel in simpleObject.GetModel().RelationModel.AsForeignObjectInOneToManyRelations)
				//{
				//	SimpleObject primaryObject = simpleObject.GetOneToManyPrimaryObject(oneToManyRelationModel.RelationKey);

				//	if (!relatedObjectsForDelete.Contains(primaryObject))
				//		simpleObject.SetOneToManyPrimaryObject(primaryObject: null, oneToManyRelationModel.RelationKey, changeContainer, requester);

				//	//ISimpleObjectCollection foreignCollection = simpleObject.GetOneToManyForeignObjectCollection(oneToManyRelationModel.RelationKey);

				//	//foreach (SimpleObject keyHolderObject in foreignCollection)
				//	//	if (keyHolderObject != null && !relatedObjectsForDelete.Contains(keyHolderObject))
				//	//		keyHolderObject.SetOneToManyPrimaryObject(primaryObject: null, oneToManyRelationModel.RelationKey, requester);
				//}


				//if (!simpleObject.DeleteRequested)
				//{
				//	changeContainer.Set(simpleObject, TransactionRequestAction.Delete, requester);
				//	this.OnRequestDelete(simpleObject, changeContainer, requester);
				//	this.RaiseDeleteRequested(simpleObject, changeContainer, requester);
				//}
			}
			//else
			//{
			//	if (!simpleObject.DeleteRequested)
			//	{
			//		changeContainer.Set(simpleObject, TransactionRequestAction.Delete, requester);
			//	}
			//}

			this.RaiseDeleteRequested(simpleObject, changeContainer, requester);


			//}
		}


		//public void RequestDelete(SimpleObject simpleObject, bool checkCanDelete, bool checkSimpleObjectGraphElements, ChangeContainer changeContainer, object requester)
		//{
		//	if (simpleObject == null)
		//		return;

		//	changeContainer.Set(simpleObject, TransactionRequestAction.Delete, requester);

		//	return;

		//	////User transactionUser = (requester is User) ? requester as User : this.Users.SystemAdmin;
		//	////TransactionResult result = this.CommitChanges(this.DefaultChangeContainer, transactionUser.Id, requester);

		//	////return result.TransactionSucceeded;

		//	//bool isSaved = false;
		//	//bool currentTransactionStarted = false;

		//	//lock (lockObject)
		//	//{
		//	//	if (simpleObject.IsDeleteStarted)
		//	//		return false;

		//	//	//if (!(simpleObject as SimpleObject).IsStorable)
		//	//	//	return false;

		//	//	//if (this.CurrentTransaction != null)
		//	//	//	return false;

		//	//	if (checkCanDelete && !this.CanDelete(simpleObject))
		//	//		return false;

		//	//	//if (simpleObject is GraphElement)
		//	//	//{
		//	//	//	if (!this.CanDelete((simpleObject as GraphElement), checkSimpleObjectGraphElements))
		//	//	//		return false;
		//	//	//}

		//	//	//if (simpleObject is SystemSimpleObject)
		//	//	//	throw new ArgumentException("For deleting SystemSimpleObject call DeleteInternal method not Delete");

		//	//	//if (!simpleObject.IsNew && this.ActiveTransaction == null)
		//	//	//{

		//	//	//ChangeContainer changeContainer = new ChangeContainer();
		//	//	List<SimpleObject> relatedObjectsForDelete = new List<SimpleObject>();
		//	//	//User transactionUser = (requester is User) ? requester as User : this.Users.SystemAdmin;

		//	//	changeContainer.Set(simpleObject, TransactionRequestAction.Delete, requester);
		//	//	//	this.BeginTransaction(transactionUser);
		//	//	//	currentTransactionStarted = true;
		//	//	//	this.OnAfterStartDeleteTransaction(simpleObject, requester);

		//	//	//	//this.DeleteInternal(requester, simpleObject);
		//	//	//}
		//	//	//else
		//	//	//{
		//	//	//	this.DeleteInternal(requester, simpleObject);
		//	//	//}

		//	//	//this.StratNewTransaction();
		//	//	//this.OnAfterStartNewDeleteTransaction(simpleObject);

		//	//	this.GetRelatedObjectsForDelete(simpleObject, ref relatedObjectsForDelete, checkSimpleObjectGraphElements);

		//	//	// We need to check if any of these related graph elements is SimpleObject that will be deleted also
		//	//	if (!(simpleObject is GraphElement))
		//	//	{
		//	//		List<SimpleObject> additionalObjectsForDelete = new List<SimpleObject>();

		//	//		foreach (SimpleObject relatedObject in relatedObjectsForDelete)
		//	//		{
		//	//			if (relatedObject is GraphElement)
		//	//			{
		//	//				SimpleObject relatedSimpleObject = (relatedObject as GraphElement).SimpleObject;

		//	//				if (relatedSimpleObject != null && relatedSimpleObject != (simpleObject as SimpleObject) && !additionalObjectsForDelete.Contains(relatedSimpleObject) && 
		//	//					relatedSimpleObject.GetModel().DeleteSimpleObjectOnLastGraphElementDelete)
		//	//				{
		//	//					// Check if all graph elements are ready to be deleted
		//	//					bool areAllGraphElementsReadyForDelete = true;

		//	//					foreach (GraphElement relatedSimpleObjectGraphElement in relatedSimpleObject.GraphElements)
		//	//					{
		//	//						if (relatedObjectsForDelete.Contains(relatedSimpleObjectGraphElement))
		//	//						{
		//	//							continue;
		//	//						}
		//	//						else
		//	//						{
		//	//							areAllGraphElementsReadyForDelete = false;
		//	//							break;
		//	//						}
		//	//					}

		//	//					if (areAllGraphElementsReadyForDelete)
		//	//						additionalObjectsForDelete.Add(relatedSimpleObject);
		//	//				}
		//	//			}
		//	//		}

		//	//		relatedObjectsForDelete.AddRange(additionalObjectsForDelete);
		//	//	}

		//	//	for (int i = relatedObjectsForDelete.Count - 1; i >= 0; i--)
		//	//	{
		// //                 SimpleObject relatedSimpleObject = relatedObjectsForDelete[i];

		//	//		changeContainer.Set(relatedSimpleObject, TransactionRequestAction.Delete, requester);

		//	//		//if (relatedSimpleObject != null && !relatedSimpleObject.IsDeleteStarted && !relatedSimpleObject.IsDeleted)
		//	//		//	this.DeleteInternal(relatedSimpleObject, requester);
		//	//	}

		//	//	//this.DeleteInternal(simpleObject, requester);

		//	//	//if (currentTransactionStarted)
		//	//	//	this.EndTransaction(requester);


		//	//	result = this.CommitChanges(changeContainer, transactionUser.Id, requester);

		//	//	return true;
		//	//}
		//}

		// Transaction must be in progress when calling this method

		// TODO: Provjeriti ovo
		//      public void DeleteInternal(SimpleObject simpleObject, object requester)
		//      {
		//	throw new NotSupportedException("Not supported method any more. Call CommitChanges instead.");

		// //         if (simpleObject == null)
		// //             return;

		// //         if (simpleObject.IsDeleteStarted)
		// //             return;

		//	////if (this.simpleObjectReadyToBeDeletedInTransaction.Contains(simpleObject))
		// //         //    return;

		//	////lock (this.lockObjectAction)
		//	////{
		//	//simpleObject.IsDeleteStarted = true;
		//	//simpleObject.ActionBeforeDeleting(requester);
		//	//this.RaiseBeforeDeleting(simpleObject, requester);
		//	//this.OnBeforeDeleting(simpleObject, requester);
		//	////this.OnDeleting(simpleObject, requester);

		//	////simpleObject.IsDeleted = true;
		//	////this.OnAfterDelete(requester, simpleObject);
		//	////this.RaiseAfterDelete(requester, simpleObject);

		//	////(simpleObject as IDisposable).Dispose();
		//	//////System.GC.SuppressFinalize(simpleObject);
		//	////simpleObject = null;
		//	////}
		//}
		//this.simpleObjectReadyToBeDeletedInTransaction.Add(simpleObject);


		protected virtual void OnRequestDelete(SimpleObject simpleObject, ChangeContainer changeContainer, object? requester)
		{
			if (simpleObject is GraphElement)
			{
				GraphElement graphElement = simpleObject as GraphElement;

				if (graphElement.Parent != null)
				{
					int status = 0;
					int oldStatus = graphElement.Status;
					this.OnChildGraphElementStatusChange(graphElement.Parent, graphElement, status, oldStatus);
				}
			}
		}

		public void CancelDeleteRequest(SimpleObject simpleObject, ChangeContainer changeContainer, object? requester)
		{
			if (simpleObject.DeleteRequested)
			{
				simpleObject.DeleteRequestIsCancelled(changeContainer, requester);
				changeContainer.CancelDeleteRequested(simpleObject, requester);
				this.OnCancelDeleteRequest(simpleObject, changeContainer, requester);
				this.RaiseDeleteRequestCancelled(simpleObject, changeContainer, requester);
			}
		}

		//internal void DeleteRequestedIsCancelled(SimpleObject simpleObject, ChangeContainer changeContainer, object requester)
		//{
		//	if (simpleObject.DeleteRequested)
		//	{
		//		simpleObject.DeleteRequestIsCancelled(changeContainer, requester);
		//		this.OnDeleteRequestCancel(simpleObject, changeContainer, requester);
		//		this.RaiseDeleteRequestCancelled(simpleObject, changeContainer, requester);
		//	}
		//}

		protected void OnCancelDeleteRequest(SimpleObject simpleObject, ChangeContainer changeContainer, object? requester)
		{
			if (simpleObject is GraphElement)
			{
				GraphElement graphElement = simpleObject as GraphElement;

				if (graphElement.Parent != null)
				{
					int status = graphElement.Status;
					int oldStatus = 0;
					this.OnChildGraphElementStatusChange(graphElement.Parent, graphElement, status, oldStatus);
				}
			}

		}

		//internal void ActionBeforeDeleting(SimpleObject simpleObject, ChangeContainer changeContainer, object requester)
		//{
		//	if (this.WorkingMode != ObjectManagerWorkingMode.Server)
		//	{
		//		simpleObject.BeforeDelete(changeContainer, requester);
		//		this.OnBeforeDelete(simpleObject, changeContainer, requester);
		//	}
		//}

		//private void HandleRelatedTransactionRequests(SimpleObject simpleObject, object requester)
		//{
		//	foreach (var item in simpleObject.RelatedTransactionRequests.ToArray())
		//	{
		//		SimpleObject relatedSimpleObject = item.Key;
		//		TransactionRequestAction relatedRequestAction = item.Value;

		//		if (relatedSimpleObject == null || relatedSimpleObject.IsDeleted || this.ActiveTransaction.TransactionRequestsBySimpleObject.ContainsKey(simpleObject))
		//			continue;

		//		switch (relatedRequestAction)
		//		{
		//			case TransactionRequestAction.Save:

		//				//if (transactionActionRequest.SimpleObject != null && transactionActionRequest.SimpleObject.ChangedPropertyNames.Count > 0)
		//				this.SaveInternal(relatedSimpleObject, validateObject: false, requester: requester);
		//				break;

		//			case TransactionRequestAction.Delete:

		//				this.DeleteInternal(relatedSimpleObject, requester);
		//				break;
		//		}
		//	}
		//}

		//protected virtual void OnBeforeGraphElementDeleted(GraphElement graphElement, object requester)
		//      {
		//          //foreach (SimpleObjectDependencyAction objectDependencyAction in this.ModelDiscovery.ObjectDependencyActions)
		//          //    if (objectDependencyAction.Match(graphElement.SimpleObject))
		//          //        objectDependencyAction.OnBeforeGraphElementDelete(graphElement);
		//      }

		/// <summary>
		/// Do action before object is deleted. This method is invoked only in non server working mode.
		/// </summary>
		/// <param name="simpleObject"></param>
		/// <param name="changeContainer"></param>
		/// <param name="requester"></param>
		protected virtual void OnBeforeDelete(SimpleObject simpleObject, ChangeContainer changeContainer, object? requester)
		{
			////simpleObject.RemoveAllRelatedObjectsFromAllRelatedObjectCaches(changeContainer, requester);
			//bool createTransactionAction = true; // !this.isLocalDatastoreInitialized;
			//									 //bool isGraphElement = simpleObject is GraphElement;
			//ISimpleObjectModel objectModel = simpleObject.GetModel();
			////SimpleObject graphElementSimpleObject = (isGraphElement) ? (simpleObject as GraphElement).SimpleObject : null;
			////IDictionary<int, object> fieldData = null;

			//if (simpleObject is GraphElement)
			//{
			//	(simpleObject as GraphElement).SimpleObject.BeforeGraphElementIsDeleted(simpleObject as GraphElement, requester);
			//	this.OnBeforeGraphElementDeleted(simpleObject as GraphElement, requester);
			//}
			//else
			//{
			//	// Force propagate Unknown status throughout GraphElement objects
			//	simpleObject.Status = 0;
			//}

			////        if (!simpleObject.IsNew && objectModel.IsStorable)
			////if (!this.localDatastoreInitializationInProgress)
			////	fieldData = simpleObject.GetPropertyValuesByIndex(); //this.CreateFieldDataBasedOnObjectModelProperties(simpleObject, (simpleObject as IBindingObject).GetPropertyValueByNameDictionary(), includeObjectKeyGuid: false); // get the field data before removing relations

			//simpleObject.RemoveAllRelatedObjectsFromAllRelatedObjectCaches(changeContainer, requester);

			//if (!simpleObject.IsNew && simpleObject.IsStorable && objectModel.IsStorable)
			//{
			//	if (createTransactionAction) // Process Delete Transaction
			//	{
			//		this.HandleRelatedTransactionRequests(simpleObject, requester);
			//		this.ProcessTransactionDeleteRequest(simpleObject); //, simpleObject.GetStorableSerializablePropertyValues(this.NormalizeForWritingByPropertyType));
			//		simpleObject.ClearRelatedTransactionRequests();

			//	}
			//	else // Delete Object from Datastore
			//	{
			//		this.LocalDatastore.DeleteRecord(objectModel.TableInfo.TableName, SimpleObject.StringPropertyId, simpleObject.Id);
			//	}
			//}

			//bool isRemoved = this.RemoveObject(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);

			////foreach (SimpleObjectDependencyAction objectDependencyAction in this.ModelDiscovery.ObjectDependencyActions)
			////    if (objectDependencyAction.Match(simpleObject))
			////        objectDependencyAction.OnBeforeDelete(simpleObject);
		}

		protected virtual void OnAfterDelete(SimpleObject simpleObject, ChangeContainer changeContainer, object? requester) { }

		protected virtual HashSet<SimpleObject> GetRelatedObjectsForDelete(SimpleObject simpleObject)
		{
			HashSet<SimpleObject> relatedObjectsForDelete = new HashSet<SimpleObject>();

			this.FindRelatedObjectsForDelete(simpleObject, ref relatedObjectsForDelete);

			return relatedObjectsForDelete;
		}

		private void FindRelatedObjectsForDelete(SimpleObject simpleObject, ref HashSet<SimpleObject> relatedObjectsForDelete)
		{
			this.FindRelatedObjectsForDelete(simpleObject, ref relatedObjectsForDelete, checkSimpleObjectGraphElements: true);
		}

		private void FindRelatedObjectsForDelete(SimpleObject simpleObject, ref HashSet<SimpleObject> relatedObjectsForDelete, bool checkSimpleObjectGraphElements)
		{
			// We need to check if any of foreign key exists in any related objects and is not in transaction requests for delete
			// one to one
			foreach (IOneToOneRelationModel oneToOneRelationModel in simpleObject.GetModel().RelationModel.AsPrimaryObjectInOneToOneRelations)
			{
				if (oneToOneRelationModel.CascadeDelete)
				{
					SimpleObject? foreignObject = simpleObject.GetOneToOneForeignObject(oneToOneRelationModel.RelationKey);

					if (foreignObject != null)
						relatedObjectsForDelete.Add(foreignObject);
				}
			}

			// one to many
			foreach (IOneToManyRelationModel oneToManyRelationModel in simpleObject.GetModel().RelationModel.AsPrimaryObjectInOneToManyRelations)
			{
				if (oneToManyRelationModel.CascadeDelete)
				{
					SimpleObjectCollection? foreignCollection = simpleObject.GetOneToManyForeignObjectCollection(oneToManyRelationModel.RelationKey);

					if (foreignCollection != null)
						foreach (SimpleObject foreignObject in foreignCollection)
							if (foreignObject != null)
								relatedObjectsForDelete.Add(foreignObject);
				}
			}

			// many to many
			//var objectModalsAsFirst = simpleObject.GetModel().RelationModel.AsFirstObjectInManyToManyRelations;
			//var objectModalsAsSecond = simpleObject.GetModel().RelationModel.AsSecondObjectInManyToManyRelations;

			foreach (IManyToManyRelationModel manyToManyRelationModel in simpleObject.GetModel().RelationModel.AsObjectInGroupMembership) // objectModalsAsFirst.Union(objectModalsAsSecond))
			{
				SimpleObjectCollection? groupMemberCollection = simpleObject.GetGroupMemberCollection(manyToManyRelationModel.RelationKey);
				var groupMembershipCollection = groupMemberCollection?.GetGroupMembershipCollection();

				if (groupMembershipCollection != null)
					foreach (GroupMembership groupMembership in groupMembershipCollection)
						if (groupMembership != null)
							relatedObjectsForDelete.Add(groupMembership);
			}

			if (simpleObject is GraphElement graphElement)
			{
				if (graphElement.SimpleObject == null && !relatedObjectsForDelete.Contains(graphElement))
				{
					relatedObjectsForDelete.Add(graphElement);
					
					return;
				}


				if (checkSimpleObjectGraphElements && graphElement.SimpleObject != null)
				{
					ISimpleObjectModel graphElementSimpleObjectModel = graphElement.SimpleObject.GetModel();
					//if (graphElement.SimpleObject.GraphElements.Count == 1) // There is only one graph element
					//{
					//    if (graphElementSimpleObjectModel.DeleteSimpleObjectOnLastGraphElementDelete)
					//    {
					//        if (!relatedObjectsForDelete.Contains(graphElement.SimpleObject))
					//            relatedObjectsForDelete.Add(graphElement.SimpleObject);
					//    }
					//}
					//else if (graphElementSimpleObjectModel.DeleteAllSimpleObjectGraphElementsOnOneGraphElementDelete) // There is many graph elements
					//{
					//    if (!relatedObjectsForDelete.Contains(graphElement.SimpleObject))
					//        relatedObjectsForDelete.Add(graphElement.SimpleObject);

					//    foreach (GraphElement simpleObjectGraphElement in graphElement.SimpleObject.GraphElements)
					//    {
					//        if (simpleObjectGraphElement == graphElement)
					//            continue;

					//        if (!relatedObjectsForDelete.Contains(simpleObjectGraphElement))
					//            relatedObjectsForDelete.Add(simpleObjectGraphElement);

					//        this.GetRelatedObjectsForDelete(simpleObjectGraphElement, ref relatedObjectsForDelete, checkSimpleObjectGraphElements: false);
					//    }
					//}

					// There is many graph elements or if deleting MustHave graph element -> delete SimpleObject and its GraphElements
					if (graphElementSimpleObjectModel.DeleteAllSimpleObjectGraphElementsOnOneGraphElementDelete || graphElementSimpleObjectModel.MustHaveGraphElementGraphKey == graphElement.GraphKey) 
					{
						if (!relatedObjectsForDelete.Contains(graphElement.SimpleObject))
							relatedObjectsForDelete.Add(graphElement.SimpleObject);

						foreach (GraphElement simpleObjectGraphElement in graphElement.SimpleObject.GraphElements)
						{
							if (simpleObjectGraphElement == graphElement)
								continue;

							if (!relatedObjectsForDelete.Contains(simpleObjectGraphElement))
								relatedObjectsForDelete.Add(simpleObjectGraphElement);

							this.FindRelatedObjectsForDelete(simpleObjectGraphElement, ref relatedObjectsForDelete, checkSimpleObjectGraphElements: false);
						}
					}

					if (graphElementSimpleObjectModel.DeleteSimpleObjectOnLastGraphElementDelete && graphElement.SimpleObject.GraphElements.Count == 1) // There is only one graph element
						if (!relatedObjectsForDelete.Contains(graphElement.SimpleObject))
							relatedObjectsForDelete.Add(graphElement.SimpleObject);
				}

				foreach (GraphElement childGraphElement in graphElement.GraphElements)
				{
					this.FindRelatedObjectsForDelete(childGraphElement, ref relatedObjectsForDelete, checkSimpleObjectGraphElements: true);

					if (!relatedObjectsForDelete.Contains(childGraphElement))
						relatedObjectsForDelete.Add(childGraphElement);
				}
			}
			else
			{
				// TODO: Chech if this is already done by one to many relations
				foreach (GraphElement childGraphElement in simpleObject.GraphElements)
				{
					this.FindRelatedObjectsForDelete(childGraphElement, ref relatedObjectsForDelete, checkSimpleObjectGraphElements); //: false);

					if (!relatedObjectsForDelete.Contains(childGraphElement))
						relatedObjectsForDelete.Add(childGraphElement);
				}
			}

			// We need to check if any of these related graph elements is SimpleObject that will be deleted also
			if (!(simpleObject is GraphElement))
			{
				List<SimpleObject> additionalObjectsForDelete = new List<SimpleObject>();

				foreach (SimpleObject relatedObject in relatedObjectsForDelete)
				{
					if (relatedObject is GraphElement)
					{
						SimpleObject relatedSimpleObject = (relatedObject as GraphElement).SimpleObject;

						if (relatedSimpleObject != null && relatedSimpleObject != (simpleObject as SimpleObject) && !additionalObjectsForDelete.Contains(relatedSimpleObject) &&
							relatedSimpleObject.GetModel().DeleteSimpleObjectOnLastGraphElementDelete)
						{
							// Check if all graph elements are ready to be deleted
							bool areAllGraphElementsReadyForDelete = true;

							foreach (GraphElement relatedSimpleObjectGraphElement in relatedSimpleObject.GraphElements)
							{
								if (relatedObjectsForDelete.Contains(relatedSimpleObjectGraphElement))
								{
									continue;
								}
								else
								{
									areAllGraphElementsReadyForDelete = false;
									
									break;
								}
							}

							if (areAllGraphElementsReadyForDelete)
								additionalObjectsForDelete.Add(relatedSimpleObject);
						}
					}
				}

				foreach (var item in additionalObjectsForDelete)
					relatedObjectsForDelete.Add(item);
			}
		}

		#endregion |   Delete   |

		#region |   Transaction Handling   |

		//public bool ProcessTransaction(SerializationReader reader)
		//{
		//	TransactionAction[] transactionActions = DeserializeTransactionActions(this, reader);

		//	this.BeginTransaction();

		//	foreach (TransactionAction transactionAction in transactionActions)
		//	{
		//		switch (transactionAction.ActionType)
		//		{
		//			case TransactionActionType.Insert:

		//				transactionAction.

		//				this.LocalDatastore.InsertRecord(objectModel.TableInfo.TableName, objectModel.StorablePropertySequence.ModelSequence, simpleObject.GetStorablePropertyValues(this.NormalizeForWritingByDatastoreType));
		//				break;

		//			case TransactionActionType.Update:

		//				PropertyValuePairs changedPropertyValues = simpleObject.GetChangedPropertyValues(this.NormalizeForWritingByDatastoreType);
		//				this.LocalDatastore.UpdateRecord(objectModel.TableInfo.TableName, SimpleObject.StringPropertyKey, transactionAction.ObjectKey, changedPropertyValues.PropertyModelSequence, changedPropertyValues.PropertyValues);
		//				break;

		//			case TransactionActionType.Delete:

		//				this.LocalDatastore.DeleteRecord(objectModel.TableInfo.TableName, SimpleObject.StringPropertyKey, transactionAction.ObjectKey);
		//				break;
		//		}

		//	}

		//	this.ActiveTransaction.tr

		//}

		//protected void ProcessTransactionActionInsert(SimpleObject simpleObject)
		//{
		//	if (!this.ActiveTransaction.TransactionRequestsBySimpleObject.ContainsKey(simpleObject))
		//	{
		//		DatastoreTransactionAction transactionAction = new DatastoreTransactionAction(this.GetObjectModel, this.GetSystemPropertySequence);
		//		PropertyValueSequence nolrmalizedStorableSerializablePropertyValues = simpleObject.GetSerializablePropertyValues(this.NormalizeWhenReadingByPropertyType);

		//		transactionAction.SetActionInsert(simpleObject.Key, nolrmalizedStorableSerializablePropertyValues);

		//		this.ActiveTransaction.TransactionActionsBySimpleObject.Add(simpleObject, transactionAction); //simpleObject.GetModel(), simpleObject.Key));
		//	}


		//	//ISimpleObjectModel objectModel = simpleObject.GetObjectModel();
		//	//TransactionElement transactionElement = null;

		//	//if (createTransactionLog)
		//	//{
		//	//    transactionElement = new TransactionElement();
		//	//    transactionElement.Status = TransactionElementStatus.Processing;
		//	//    transactionElement.Transaction = this.CurrentTransaction;
		//	//    transactionElement.DataActionType = TransactionElementDataActionType.Insert;
		//	//    transactionElement.SimpleObjectTableId = simpleObject.Key.TableId;
		//	//    transactionElement.SimpleObjectObjectId = simpleObject.Key.ObjectId;
		//	//    transactionElement.SimpleObjectCreatorServerId = simpleObject.Key.CreatorServerId;
		//	//    transactionElement.Save();

		//	//    this.CreateTransactionElementPropertyValuesFromFieldData(transactionElement, fieldData, () => new TransactionElementPropertyValue());
		//	//}

		//	//this.Datastore.InsertRecord(objectModel.TableInfo.TableName, simpleObject.Key.ObjectId, simpleObject.Key.CreatorServerId, fieldData);

		//	//if (createTransactionLog)
		//	//{
		//	//    transactionElement.Status = TransactionElementStatus.Applied;
		//	//    transactionElement.Save();
		//	//}
		//}

		//protected void ProcessTransactionUpdateRequest(SimpleObject simpleObject) // int[] propertyIndexes, object[] normalizedPropertyValues) // IDictionary<int, object> fieldData, IDictionary<int, object> oldFieldData)
		//{
		//	if (!this.ActiveTransaction.TransactionActions.ContainsKey(simpleObject))
		//	{
		//		//DatastoreTransactionAction transactionAction = new DatastoreTransactionAction(this.GetObjectModel, this.GetSystemPropertySequence);
		//		//PropertyValueSequence normalizedChangedOldPropertyValues = simpleObject.GetChangedOldPropertyValues(this.NormalizeWhenReadingByPropertyType);

		//		//transactionAction.SetActionUpdate(simpleObject.Key, normalizedChangedOldPropertyValues);

		//		//this.ActiveTransaction.TransactionActionsBySimpleObject.Add(simpleObject, transactionAction);
		//		this.ActiveTransaction.TransactionRequestsBySimpleObject.Add(simpleObject, TransactionRequestAction.Save);
		//	}

		//	//ISimpleObjectModel objectModel = simpleObject.GetObjectModel();
		//	//TransactionElement transactionElement = null;

		//	//if (createTransactionLog)
		//	//{
		//	//    Dictionary<string, object> oldFieldData = new Dictionary<string, object>();

		//	//    foreach (var dictionaryItem in fieldData)
		//	//    {
		//	//        string propertyName = dictionaryItem.Key;
		//	//        object oldValue = simpleObject.GetOldPropertyValue(propertyName);
		//	//        oldFieldData.Add(propertyName, oldValue);
		//	//    }

		//	//    transactionElement = new TransactionElement();
		//	//    transactionElement.Status = TransactionElementStatus.Processing;
		//	//    transactionElement.Transaction = this.CurrentTransaction;
		//	//    transactionElement.DataActionType = TransactionElementDataActionType.Update;
		//	//    transactionElement.SimpleObjectTableId = simpleObject.Key.TableId;
		//	//    transactionElement.SimpleObjectObjectId = simpleObject.Key.ObjectId;
		//	//    transactionElement.SimpleObjectCreatorServerId = simpleObject.Key.CreatorServerId;

		//	//    this.CreateTransactionElementPropertyValuesFromFieldData(transactionElement, fieldData, () => new TransactionElementPropertyValue());
		//	//    this.CreateTransactionElementPropertyValuesFromFieldData(transactionElement, oldFieldData, () => new TransactionElementOldPropertyValue());

		//	//    transactionElement.Save();
		//	//}

		//	//this.Datastore.UpdateRecord(objectModel.TableInfo.TableName, SimpleObjectModel.PropertyModel.ObjectId.Name, SimpleObjectModel.PropertyModel.CreatorServerId.Name,
		//	//                            simpleObject.Key.ObjectId, simpleObject.Key.CreatorServerId, fieldData);

		//	//if (createTransactionLog)
		//	//{
		//	//    transactionElement.Status = TransactionElementStatus.Applied;
		//	//    transactionElement.Save();
		//	//}
		//}

		//protected void ProcessTransactionDeleteRequest(SimpleObject simpleObject) //IDictionary<int, object> fieldData)
		//{
		//	if (simpleObject.IsDeleted)
		//		return;

		//	if (!this.ActiveTransaction.TransactionRequestsBySimpleObject.ContainsKey(simpleObject))
		//	{
		//		//DatastoreTransactionAction transactionAction = new DatastoreTransactionAction(this.GetObjectModel, this.GetSystemPropertySequence);
		//		//PropertyValueSequence nolrmalizedStorableSerializablePropertyValues = simpleObject.GetStorableSerializablePropertyValues(this.NormalizeWhenReadingByPropertyType);
		//		//transactionAction.SetActionDelete(simpleObject.Key, nolrmalizedStorableSerializablePropertyValues);

		//		//this.ActiveTransaction.TransactionActionsBySimpleObject.Add(simpleObject, transactionAction);


		//		this.ActiveTransaction.TransactionRequestsBySimpleObject.Add(simpleObject, TransactionRequestAction.Delete);
		//	}
		//	else
		//	{
		//		this.ActiveTransaction.TransactionRequestsBySimpleObject[simpleObject] = TransactionRequestAction.Delete;
		//	}

		//	if (!simpleObject.IsDeleteStarted)
		//		simpleObject.Delete();

		//	//ISimpleObjectModel objectModel = simpleObject.GetObjectModel();
		//	//TransactionElement transactionElement = null;

		//	//if (createTransactionLog)
		//	//{
		//	//    IDictionary<string, object> propertyValueDictionary = simpleObject.GetOldPropertyValueDictionary();

		//	//    transactionElement = new TransactionElement();
		//	//    transactionElement.Status = TransactionElementStatus.Processing;
		//	//    transactionElement.Transaction = this.CurrentTransaction;
		//	//    transactionElement.SimpleObjectTableId = simpleObject.Key.TableId;
		//	//    transactionElement.SimpleObjectObjectId = simpleObject.Key.ObjectId;
		//	//    transactionElement.SimpleObjectCreatorServerId = simpleObject.Key.CreatorServerId;
		//	//    transactionElement.DataActionType = TransactionElementDataActionType.Delete;

		//	//    // PropertyValueData contains property value dictionary in order to repair previus state if transaction rollback is needed.
		//	//    this.CreateTransactionElementPropertyValuesFromFieldData(transactionElement, propertyValueDictionary, () => new TransactionElementPropertyValue());

		//	//    transactionElement.Save();
		//	//}

		//	//this.Datastore.DeleteRecord(objectModel.TableInfo.TableName, SimpleObjectModel.PropertyModel.ObjectId.Name, SimpleObjectModel.PropertyModel.CreatorServerId.Name, 
		//	//                            simpleObject.Key.ObjectId, simpleObject.Key.CreatorServerId);

		//	//if (createTransactionLog)
		//	//{
		//	//    transactionElement.Status = TransactionElementStatus.Applied;
		//	//    transactionElement.Save();
		//	//}
		//}

		//protected virtual void OnAfterStartDeleteTransaction(SimpleObject simpleObject, object requester)
		//{
		//}

		//public TransactionResult ProcessTransactionRequests(User user)
		//{
		//	TransactionResult result = null;
		//	long transactionId = 0;

		//	if (this.lastTransaction != null)
		//		transactionId = this.lastTransaction.TransactionId;

		//	if (this.lastTransaction != null && this.lastTransaction.Status == TransactionStatus.Completed) // && this.LastTransaction != null)
		//	{
		//		result = new TransactionResult(transactionSucceeded: true, transactionId: transactionId); // this.LastTransaction.TransactionId);
		//	}
		//	else
		//	{
		//		result = new TransactionResult(transactionSucceeded: false, transactionId: transactionId, errorDescription: String.Format("Transaction Error - Rollbacked"));
		//	}

		//	return result;
		//}

		//public TransactionResult ProcessTransactionRequests_OLD(Dictionary<SimpleObject, TransactionActionType> transactionRequestsBySimpleObject, User user)
		//{
		//	TransactionResult result = null;
		//	int initialRequestCount = transactionRequestsBySimpleObject.Count;
		//	SimpleObject masterSimpleObject = null;
		//	TransactionRequestAction masterRequestAction = TransactionRequestAction.Save;
		//	long transactionId = 0;

		//	for (int i = 0; i < transactionRequestsBySimpleObject.Count; i++)
		//	{
		//		var keyValuePair = transactionRequestsBySimpleObject.ElementAt(i);
		//		SimpleObject simpleObject = keyValuePair.Key;
		//		TransactionRequestAction requestAction = (keyValuePair.Value == TransactionActionType.Delete) ? TransactionRequestAction.Delete : TransactionRequestAction.Save;

		//		if (i == 0)
		//		{
		//			//this.BeginTransaction_OLD(user);
		//			masterSimpleObject = simpleObject;
		//			masterRequestAction = requestAction;
		//		}
		//		else
		//		{
		//			masterSimpleObject.SetRelatedTransactionRequest(simpleObject, requestAction);
		//		}
		//	}

		//	if (masterSimpleObject != null)
		//	{
		//		if (masterRequestAction == TransactionRequestAction.Save)
		//		{
		//			masterSimpleObject.Save(user);
		//		}
		//		else
		//		{
		//			masterSimpleObject.Delete(user);
		//		}
		//	}

		//	if (this.lastTransaction != null)
		//		transactionId = this.lastTransaction.TransactionId;

		//	if (this.lastTransaction != null && this.lastTransaction.Status == TransactionStatus.Completed) // && this.LastTransaction != null)
		//	{
		//		result = new TransactionResult(transactionSucceeded: true, transactionId: transactionId); // this.LastTransaction.TransactionId);

		//		// Sets the transaction objects internal state to normal
		//		for (int i = 0; i < transactionRequestsBySimpleObject.Count; i++)
		//			transactionRequestsBySimpleObject.ElementAt(i).Key.SetInternalState(SimpleObjectInternalState.Normal);
		//	}
		//	else
		//	{
		//		//// Rollback TransactionActionsResponse affected objects to its original state
		//		//// TODO: Rollback should be same as update but with complete reverse order to back to previous state.
		//		//// !!! There is no active transaction started (BeginTransaction) while rollback is in progress !!!

		//		//for (int i = transactionRequestsBySimpleObject.Count - 1; i >= 0; i--)
		//		//{
		//		//	var keyValuePair = transactionRequestsBySimpleObject.ElementAt(i);
		//		//	SimpleObject simpleObject = keyValuePair.Key;
		//		//	TransactionActionType actionType = keyValuePair.Value;

		//		//	if (simpleObject != null)
		//		//	{
		//		//		switch (actionType)
		//		//		{
		//		//			case TransactionActionType.Insert:

		//		//				if (!simpleObject.IsDeleteStarted)
		//		//					simpleObject.Delete();

		//		//				break;

		//		//			case TransactionActionType.Update:

		//		//				simpleObject.RejectChanges();
		//		//				break;

		//		//			case TransactionActionType.Delete:

		//		//				break;
		//		//		}
		//		//	}
		//		//}

		//		result = new TransactionResult(transactionSucceeded: false, transactionId: transactionId, errorDescription: String.Format("Transaction Error - Rollbacked"));
		//	}

		//	return result;
		//}



		//public TransactionResult ProcessTransactionRequests2(Dictionary<SimpleObject, TransactionActionType> transactionRequestsBySimpleObject, User requester)
		//{
		//	TransactionResult result = null;
		//	long transactionId = default(long);
		//	int initialRequestCount = transactionRequestsBySimpleObject.Count;
		//	int i = 0;

		//	if (transactionRequestsBySimpleObject.Count == 0)
		//	{
		//		int TODO = 0;
		//	}

		//	while (i < transactionRequestsBySimpleObject.Count)
		//	{
		//		var keyValuePair = transactionRequestsBySimpleObject.ElementAt(i);
		//		SimpleObject simpleObject = keyValuePair.Key;
		//		TransactionActionType actionType = keyValuePair.Value;

		//		if (i == 0)
		//			this.BeginTransaction(requester);

		//		if (actionType == TransactionActionType.Delete)
		//		{
		//			if (simpleObject != null && !simpleObject.IsDeleteStarted)
		//			{
		//				if (this.CanDelete(simpleObject))
		//				{
		//					//if (simpleObject is GraphElement && !this.CanDeleteGraphElement((simpleObject as GraphElement), checkSimpleObjectGraphElements: true))
		//					//{
		//					//	result = new TransactionResult(false, String.Format("GraphElement policy prevents deleting, GraphElement Id=" + simpleObject.Id));

		//					//	break;
		//					//}
		//					//else
		//					//{
		//					//  simpleObject.Delete();
		//					//}
		//					this.DeleteInternal(requester, simpleObject);
		//				}
		//				else
		//				{
		//					result = new TransactionResult(false, String.Format("SimpleObject cannot be deleted, SimpleObject Id=" + simpleObject.Id));

		//					break;
		//				}
		//			}
		//		}
		//		else if (simpleObject == null)
		//		{
		//			result = new TransactionResult(false, String.Format("SimpleObject is null"));

		//			break;
		//		}
		//		else if (!simpleObject.IsDeleted)
		//		{
		//			SimpleObjectValidationResult validationResult = simpleObject.Validate();

		//			if (!validationResult.Passed)
		//			{
		//				result = new TransactionResult(false, String.Format("Validation error for SimpleObject Id={0}: {1}", simpleObject.Id, validationResult.Message));

		//				break;
		//			}

		//			validationResult = this.CanSave(simpleObject);

		//			if (!validationResult.Passed)
		//			{
		//				result = new TransactionResult(false, String.Format("CanSave check error for SimpleObject Id={0}: {1}", simpleObject.Id, validationResult.Message));

		//				break;
		//			}

		//			this.SaveInternal(simpleObject, validateObject: false, requester: requester);


		//			//if (simpleObject is GraphElement)
		//			//{
		//			//	GraphElement graphElement = simpleObject as GraphElement;

		//			//	if (graphElement.SimpleObject == null)
		//			//	{
		//			//		result = new TransactionResult(false, String.Format("GraphElement SimpleObject is null. GraphElement ObjectKey={0}: {1}", simpleObject.Key.ToObjectKeyString(), validationResult.Message));
		//			//		break;
		//			//	}

		//			//	if (graphElement.IsNew)
		//			//	{
		//			//		if (!this.CanAddGraphElement(graphElement.Graph, graphElement.SimpleObject.GetType(), graphElement.Parent))
		//			//		{
		//			//			result = new TransactionResult(false, String.Format("GraphElement SimpleObject is null. GraphElement ObjectKey={0}: {1}", simpleObject.Key.ToObjectKeyString(), validationResult.Message));
		//			//			break;
		//			//		}
		//			//	}
		//			//	else if (graphElement.GetChangedPropertyIndexes().Contains(GraphElementModel.PropertyModel.ParentKey.Index))
		//			//	{
		//			//		GraphElement newParent = graphElement.Parent;
		//			//		Guid? oldParentKey = graphElement.GetOldPropertyValue(GraphElementModel.PropertyModel.ParentKey.Index) as Guid?;
		//			//		GraphElement oldParent = (oldParentKey != null) ? this.GetObject(oldParentKey) as GraphElement : null;

		//			//		graphElement.Parent = oldParent;

		//			//		if (!this.CanGraphElementChangeParent(graphElement, newParent, enforceSetParentAndValidate: true))
		//			//		{
		//			//			graphElement.Parent = newParent; // set original value

		//			//			result = new TransactionResult(false, String.Format("GraphElement SimpleObject is null. GraphElement ObjectKey={0}: {1}", simpleObject.Key.ToObjectKeyString(), validationResult.Message));
		//			//			break;
		//			//		}

		//			//		graphElement.Parent = newParent; // set original value
		//			//	}
		//		}
		//		else
		//		{
		//			result = new TransactionResult(false, String.Format("SimpleObject is deleted"));

		//			break;
		//		}

		//		// TODO: Instead finding related transaction request, rollback should be same as update but with complete reverse order to back to previous state.


		//		// Include all related transaction requests also
		//		foreach (var item in simpleObject.RelatedTransactionRequests.ToArray())
		//		{
		//			SimpleObject relatedSimpleObject = item.Key;
		//			TransactionRequestAction relatedRequestAction = item.Value;

		//			TransactionActionType datastoreAction = (relatedRequestAction == TransactionRequestAction.Delete) ? TransactionActionType.Delete :
		//																				  (relatedSimpleObject.IsNew) ? TransactionActionType.Insert : 
		//																												TransactionActionType.Update;
		//			if (transactionRequestsBySimpleObject.ContainsKey(relatedSimpleObject))
		//			{
		//				if (datastoreAction == TransactionActionType.Delete)
		//					transactionRequestsBySimpleObject[relatedSimpleObject] = TransactionActionType.Delete; // if existing request is update change it to delete
		//			}
		//			else
		//			{
		//				transactionRequestsBySimpleObject.Add(relatedSimpleObject, datastoreAction);
		//			}
		//		}

		//		i++;
		//	}

		//	if (transactionRequestsBySimpleObject.Count > 0)
		//	{
		//		transactionId = this.lastTransaction.TransactionId;
		//		this.EndTransaction(requester);
		//	}

		//	if (result != null)
		//	{
		//		// Rollback TransactionActionsResponse affected objects to its original state
		//		// TODO: Rollback should be same as update but with complete reverse order to back to previous state.


		//		// !!! There is no active transaction started (BeginTransaction) while rollback is in progress !!!

		//		for (i = transactionRequestsBySimpleObject.Count - 1; i >= 0; i--)
		//		{
		//			var keyValuePair = transactionRequestsBySimpleObject.ElementAt(i);
		//			SimpleObject simpleObject = keyValuePair.Key;
		//			TransactionActionType actionType = keyValuePair.Value;

		//			if (simpleObject != null)
		//			{
		//				switch (actionType)
		//				{
		//					case TransactionActionType.Insert:

		//						if (!simpleObject.IsDeleteStarted)
		//							simpleObject.Delete();

		//						break;

		//					case TransactionActionType.Update:

		//						simpleObject.RejectChanges();
		//						break;

		//					case TransactionActionType.Delete:

		//						break;
		//				}
		//			}
		//		}
		//	}
		//	else
		//	{
		//		//try
		//		//{
		//		//	this.BeginTransaction(this.Users.SystemAdmin);

		//		//	for (i = 0; i < transactionRequestsBySimpleObject.Count; i++)
		//		//	{
		//		//		var keyValuePair = transactionRequestsBySimpleObject.ElementAt(i);
		//		//		SimpleObject simpleObject = keyValuePair.Key;
		//		//		TransactionActionType actionType = keyValuePair.Value;

		//		//		switch (actionType)
		//		//		{
		//		//			case TransactionActionType.Insert:

		//		//				//if (simpleObject.IsNew)
		//		//				this.ProcessTransactionUpdateRequest(simpleObject);

		//		//				break;

		//		//			case TransactionActionType.Update:

		//		//				//PropertyValueSequence normalizedChangerOldPropertyValues = simpleObject.GetChangedOldPropertyValues(this.NormalizeWhenReadingByPropertyType); //this.CreateFieldDataBasedOnObjectModelProperties(simpleObject, (simpleObject as IBindingObject).GetChangedOldPropertyValueByNameDictionary(), includeObjectKeyGuid: false);

		//		//				//if (normalizedChangerOldPropertyValues.Count > 0)
		//		//				this.ProcessTransactionUpdateRequest(simpleObject);

		//		//				break;

		//		//			case TransactionActionType.Delete:

		//		//				if (!simpleObject.IsDeleted)
		//		//				{
		//		//					//PropertyValueSequence normalizedPropertyValues = simpleObject.GetStorableSerializablePropertyValues(this.NormalizeForWritingByPropertyType);

		//		//					//simpleObject.Delete(requester: this);
		//		//					this.ProcessTransactionDeleteRequest(simpleObject);
		//		//				}

		//		//				break;
		//		//		}
		//		//	}
		//		//}
		//		//catch (Exception ex)
		//		//{
		//		//	result = new TransactionResult(false, transactionId, String.Format("Exception is caught while processing transaction: " + ExceptionHelper.GetFullErrorMessage(ex)));
		//		//}
		//		//finally
		//		//{
		//		//	transactionId = this.ActiveTransaction.TransactionId;
		//		//	this.EndTransaction();
		//		//}

		//		//if (result == null)
		//			result = new TransactionResult(transactionSucceeded: true, transactionId: transactionId);
		//	}

		//	// Sets the transaction objects internal state to normal
		//	for (i = 0; i < transactionRequestsBySimpleObject.Count; i++)
		//		transactionRequestsBySimpleObject.ElementAt(i).Key.SetInternalState(SimpleObjectInternalState.Normal);

		//	return result;
		//}

		public TransactionResult CommitChanges()
		{
			return this.CommitChanges(requester: null);
		}

		public TransactionResult CommitChanges(ChangeContainer changeContainer)
		{
			return this.CommitChanges(changeContainer, requester: null);
		}

		public TransactionResult CommitChanges(object? requester)
		{
			return this.CommitChanges(this.DefaultChangeContainer, requester);
		}

		public TransactionResult CommitChanges(ChangeContainer changeContainer, object? requester)
		{
			return this.CommitChanges(changeContainer, saveTransaction: true, requester);
		}

		public TransactionResult CommitChanges(ChangeContainer changeContainer, bool saveTransaction, object? requester)
		{
			IUser transactionUser = (requester is IUser) ? requester as IUser : this.SystemAdmin;

			return this.CommitChanges(changeContainer, transactionUser.Id, transactionUser.CharacterEncoding, saveTransaction, requester);
		}

		public TransactionResult CommitChanges(long userId, Encoding characterEncoding, object? requester)
		{
			return this.CommitChanges(this.DefaultChangeContainer, userId, characterEncoding, requester);
		}

		public TransactionResult CommitChanges(ChangeContainer changeContainer, long userId, Encoding characterEncoding, object? requester)
		{
			return this.CommitChanges(changeContainer, userId, characterEncoding, saveTransaction: true, requester);
		}

		public TransactionResult CommitChanges(ChangeContainer changeContainer, long userId, Encoding characterEncoding, bool saveTransaction, object? requester)
		{
			//if (this.WorkingMode == ObjectManagerWorkingMode.Server)
			//	throw new Exception("You cannot commit changes in server mode!");

			if (changeContainer == null || !changeContainer.RequireCommit)
				return new TransactionResult(transactionSucceeded: true);

			lock (this.lockTransaction)
			{
				TransactionResult result;
				string errorDescription = null;
				SimpleObjectValidationResult validationResult = SimpleObjectValidationResult.DefaultSuccessResult;
				bool transactionSucceed = false;
				bool onlyDeleteNewObjects = false;
				//Dictionary<SimpleObject, TransactionRequestAction> transactionActions = new Dictionary<SimpleObject, TransactionRequestAction>();
				//KeyValuePair<SimpleObject, TransactionRequestAction>[] transactionActions = null;
				SystemTransaction? systemTransaction = null;
				List<DatastoreActionInfo>? datastoreActions = null;

				//if (changeContainer.RequestCount == 0)
				//	return new TransactionResult(transactionSucceeded: true);

				//// Try to remove this to the ChangeContainer class.....
				if (this.WorkingMode != ObjectManagerWorkingMode.Server)
				{
					// Collect all related objects that requires changes, update or delete.

					foreach (SimpleObject simpleObject in changeContainer.TransactionRequests.Keys)
						this.RaiseActiveEditorsPushData(simpleObject);

					//for (int i = 0; i < changeContainer.TransactionRequests.Count; i++)
					foreach (var item in changeContainer.TransactionRequests.ToArray())
					{
						//var item = changeContainer.TransactionRequests.ElementAt(i);
						SimpleObject simpleObject = item.Key;
						TransactionRequestAction transactionAction = item.Value;

						if (transactionAction == TransactionRequestAction.Save)
						{
							if (!simpleObject.RequireSaving())
								continue;

							this.OnBeforeSave(simpleObject, changeContainer, requester);
							simpleObject.BeforeSave(changeContainer, requester);
						}
						else // if (transactionAction == TransactionRequestAction.Delete)
						{
							this.OnBeforeDelete(simpleObject, changeContainer, requester);
							simpleObject.BeforeDelete(changeContainer, requester);

							if (simpleObject is GraphElement graphElement && graphElement.SimpleObject != null)
								graphElement.SimpleObject.BeforeGraphElementIsDeleted(graphElement, changeContainer, requester);

							HashSet<SimpleObject> relatedSimpleObjectsForDelete = this.GetRelatedObjectsForDelete(simpleObject);

							foreach (SimpleObject so in relatedSimpleObjectsForDelete)
								changeContainer.Set(so, TransactionRequestAction.Delete, requester);
						}

						//if (this.WorkingMode != ObjectManagerWorkingMode.Client)
						//transactionActions.Add(simpleObject, transactionAction);
					}

					//// Check if there is delete request and find related objects for delete also.
					//foreach (var item in changeContainer.TransactionRequests.ToArray())
					//	if (item.Value == TransactionRequestAction.Delete)
					//		foreach (SimpleObject so in this.GetRelatedObjectsForDelete(item.Key))
					//			changeContainer.SetTransactionRequest(so, TransactionRequestAction.Delete, requester);
				}


				foreach (var item in changeContainer.TransactionRequests.ToArray())
				{
					SimpleObject simpleObject = item.Key;
					TransactionRequestAction transactionAction = item.Value;

					if (transactionAction == TransactionRequestAction.Delete)
					{
						simpleObject.DeleteStarted = true;

						//if (requester != this) // Requester is this when transaction is rollback
						//{
						//simpleObject.ActionBeforeDeleting(requester);

						if (!(simpleObject is GraphElement))
							simpleObject.Status = 0; // Force propagate Unknown status throughout GraphElement objects

						//else
						//{
						//	GraphElement graphElement = simpleObject as GraphElement;

						//	graphElement.SimpleObject.BeforeGraphElementIsDeleted(graphElement, requester);
						//	this.OnBeforeGraphElementDeleted(graphElement, requester);
						//}

						this.RaiseBeforeDelete(simpleObject, requester);
						//simpleObject.RemoveAllRelatedObjectsFromAllRelatedObjectCaches(changeContainer, requester);
						//}
					}
				}


				// TODO: for deleting objects create Dictionary with non default property values needed for transaction log
				// SimpleObject, propertyIndex, old (last saved) PropertyValue

				//// TODO: Move this before locking ChangeContainer
				//foreach (var item in changeContainer.TransactionRequests.ToArray())
				//	item.Key.RemoveAllRelatedObjectsFromAllRelatedObjectCaches(changeContainer, requester);

				//int requestCount = changeContainer.RequestCount;

				changeContainer.LockChanges();
				changeContainer.Sort();
				//transactionActions = changeContainer.TransactionRequests.ToArray();

				validationResult = changeContainer.Validate();

				//if (changeContainer.RequestCount != requestCount) // If Validation from few lines up activates new property changes
				//	validationResult = changeContainer.Validate();

				if (!validationResult.Passed)
				{
					this.RaiseValidationInfo(validationResult); // new SimpleObjectValidationResult(changeContainer.ValidationErrorObject, passed: true, message: null, failedRuleResult: null));
					changeContainer.UnlockChanges();

					return new TransactionResult(false, -1, validationResult.Message) { ValidationResult = validationResult };
				}

				//changeContainer.UnlockChanges();

				//foreach (var item in changeContainer.TransactionRequests.ToArray())
				//{
				//	SimpleObject simpleObject = item.Key;
				//	TransactionRequestAction transactionAction = item.Value;

				//	if (transactionAction == TransactionRequestAction.Delete)
				//		simpleObject.RemoveAllRelatedObjectsFromAllRelatedObjectCaches(changeContainer, requester); // Remove all relations values, may
				//}

				//changeContainer.LockChanges();
				//changeContainer.Sort();

				// Check if only delete new objects transactionRequests contains in non server mode, then
				if (this.WorkingMode != ObjectManagerWorkingMode.Server && changeContainer.TransactionRequests.Count > 0)
					onlyDeleteNewObjects = changeContainer.TransactionRequests.All(item => item.Key.IsNew && item.Value == TransactionRequestAction.Delete);

				if (onlyDeleteNewObjects)
				{
					transactionSucceed = true; // No need for transaction, only delete new objects from cache
				}
				else
				{
					var rollbackTransactionActions = this.CreateRolbackTransactionActionsFromTransactionRequests(changeContainer.TransactionRequests);
					
					systemTransaction = new SystemTransaction(this, ref this.systemTransactions, userId, characterEncoding, this.ClientId, this.ServerId, rollbackTransactionActions, TransactionStatus.Started);

					if (this.WorkingMode != ObjectManagerWorkingMode.Client)
						datastoreActions = this.CreateDatastoreActionsFromTransactionRequests(changeContainer.TransactionRequests);

					this.RaiseTransactionStarted(systemTransaction, datastoreActions, requester);

					foreach (SimpleObject simpleObject in changeContainer.TransactionRequests.Keys)
						simpleObject.InternalState = SimpleObjectInternalState.TransactionRequestProcessing;

					if (this.WorkingMode == ObjectManagerWorkingMode.Client)
					{
						List<TransactionActionInfo> transactionActionData = this.CreateClientTransactionActionDataInfoListForSendingToServer(changeContainer.TransactionRequests);
						ClientTransactionResult clientTransactionResult = this.RemoteDatastore!.ProcessTransactionRequest(transactionActionData, this.GetObjectModel, this.GetServerObjectModel).GetAwaiter().GetResult();

						transactionSucceed = clientTransactionResult.TransactionSucceeded; // && clientTransactionResult.TransactionResult.TransactionSucceeded;

						if (transactionSucceed)
						{
							List<ClientObjectCache> cacheshesWithTempIds = new List<ClientObjectCache>();

							//result = clientTransactionResult.TransactionResult;

							// Sets the new object Ids values through the simpleObject properties with temp object Id values.
							foreach (var item in clientTransactionResult.SimpleObjectPropertiesWithTempClientObjectIdNeedsToChange!)
							{
								SimpleObject simpleObject = this.GetObject(item.TableId, item.ObjectId)!;
								IPropertyModel propertyModel = simpleObject.GetModel().PropertyModels[item.PropertyIndex];
								long temptClientPropertyObjectId = (long)simpleObject.GetPropertyValue(item.PropertyIndex)!;
								long newPropertyObjectId = clientTransactionResult.NewObjectIdsByTempClientObjectId![temptClientPropertyObjectId];

								simpleObject.SetPropertyValue(propertyModel, newPropertyObjectId, changeContainer: null, requester);
							}

							// Replace temp client Id with the new one
							foreach (var transactionRequest in changeContainer.TransactionRequests)
							{
								SimpleObject simpleObject = transactionRequest.Key;

								if (simpleObject.Id < 0)
								{
									long tempId = simpleObject.Id;
									long newId = clientTransactionResult.NewObjectIdsByTempClientObjectId![tempId];
									ClientObjectCache objectCache = (this.GetObjectCache(simpleObject.GetModel().TableInfo.TableId) as ClientObjectCache)!;

									objectCache.ReplaceTempId(simpleObject, newId);
									this.ObjectIdIsChanged(simpleObject, tempId, newId);
									this.ClientTempObjectIdGenerator.RemoveKey(-tempId);
									cacheshesWithTempIds.Add(objectCache);
								}
							}

							foreach (ClientObjectCache objectCache in cacheshesWithTempIds)
								objectCache.ClearTempIds();
						}
						//else
						//{
						//	// TODO: Inform client what is the reason why transaction is not succeeded: Inform client about the problem and give them a choice to try repeat request to server or rollback or reject changes.
						//	int TEST = 0;
						//}

						////if (this.TransactionFinished != null)
						//	systemTransaction.CreateTransactionActionLogsFromTransactionRequests();
					}
					else // if (this.WorkingMode == ObjectManagerWorkingMode.Server || this.WorkingMode == ObjectManagerWorkingMode.ClientWithLocalDatastore)
					{
						//systemTransaction.CreateTransactionActionLogsFromTransactionRequests();
						//systemTransaction.CreateActionDataFromTransactionActionLogs();
						//systemTransaction.CreateDatastoreActionsFromTransactionRequests(this.GetObjectModel, this.GetServerObjectPropertyInfo, this.NormalizeForWritingToDatastore);

						////byte[] uncompressedActionData = SimpleObjectManager.SerializeTransactionActions(this.ActiveTransaction.TransactionActionLogs);
						////this.ActiveTransaction.ActionData = uncompressedActionData; // this.CompressData(uncompressedActionData);

						systemTransaction.Save();

						// Checking if serialization/deserialization is working
						//byte[] compressedActionData = this.CompressData(uncompressedActionData);
						//byte[] uncompressedActionData2 = this.DecompressData(compressedActionData);

						//List<SystemTransactionAction> currentTransaction2 = this.DeserializeTransactionActions(this.CurrentTransaction, uncompressedActionData);

						transactionSucceed = true;

						try
						{
							foreach (DatastoreActionInfo datastoreAction in datastoreActions!)
							{
								ISimpleObjectModel? objectModel = this.GetObjectModel(datastoreAction.TableId);

								if (objectModel != null)
								{
									switch (datastoreAction.ActionType)
									{
										case TransactionActionType.Insert:

											this.LocalDatastore!.InsertRecord(objectModel.TableInfo, datastoreAction.PropertyIndexValues, objectModel.PropertyModels.GetPropertyModel);
											datastoreAction.DatastoreStatus = DatastoreActionStatus.Inserted;

											break;

										case TransactionActionType.Update:

											this.LocalDatastore!.UpdateRecord(objectModel.TableInfo, objectModel.IdPropertyModel.PropertyIndex, datastoreAction.ObjectId, datastoreAction.PropertyIndexValues, objectModel.PropertyModels.GetPropertyModel);
											datastoreAction.DatastoreStatus = DatastoreActionStatus.Updated;

											break;

										case TransactionActionType.Delete:

											this.LocalDatastore!.DeleteRecord(objectModel.TableInfo, objectModel.IdPropertyModel.PropertyIndex, objectModel.IdPropertyModel.DatastoreFieldName!, datastoreAction.ObjectId);
											datastoreAction.DatastoreStatus = DatastoreActionStatus.Deleted;

											break;
									}
								}
								else
								{
									// TODO: Throw something and inform that object model is unknown
								}
							}
						}
						catch (Exception ex)
						{
							transactionSucceed = false;
							errorDescription = ex.GetFullErrorMessage();
							Debug.WriteLine("CommitChanges Datastore Exception:" + errorDescription);
						}

						//if (datastoreActions.Count == 0)
						//{
						//	int TODO = 0;
						//}
					}
				}

				
				// Acknowledge or Rollback transaction
				if (transactionSucceed)
				{
					var transactionRequests = changeContainer.TransactionRequests.ToArray();

					changeContainer.UnlockChanges();

					// Remove all relations values, without track changes for deleted objects
					foreach (var item in transactionRequests)
						if (item.Value == TransactionRequestAction.Delete)
							item.Key.RemoveAllRelatedObjectsFromAllRelatedObjectCaches(changeContainer, requester); 

					// Acknowledge transaction by accepting changes of the envolved SimpleObjects
					foreach (KeyValuePair<SimpleObject, TransactionRequestAction> item in transactionRequests)
					{
						SimpleObject simpleObject = item.Key;
						TransactionRequestAction transactionAction = item.Value;

						//simpleObject.Requester = null;

						if (transactionAction == TransactionRequestAction.Save)
						{
							//if (simpleObject.IsNew && this.WorkingMode == ObjectManagerWorkingMode.Server && requester != this) // Requester is this when transaction is rollback
							//{
							//	this.RaiseNewObjectCreated(simpleObject, requester);
							//	this.OnNewObjectCreated(simpleObject, requester);

							//	if (simpleObject is GraphElement)
							//	{
							//		this.RaiseNewGraphElementCreated(simpleObject as GraphElement, requester);
							//		this.OnNewGraphElementCreated(simpleObject as GraphElement, requester);
							//		simpleObject.GraphElementIsCreated(simpleObject as GraphElement, requester);
							//	}
							//}

							//this.RaiseNewObjectCreated(simpleObject, requester);
							simpleObject.AcceptChanges(changeContainer: null, requester: this);
							simpleObject.SetIsNew(false);
						}
						else //if (transactionAction == TransactionRequestAction.Delete)
						{
							this.RemoveObjectFromCache(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);

							simpleObject.IsDeleted = true;

							//if (requester != this) // Requester is this when transaction rollback is in progress.
							//{
							this.OnAfterDelete(simpleObject, changeContainer, requester);
							this.RaiseAfterDelete(simpleObject, requester);
							//}

							simpleObject.Dispose();
							//System.GC.SuppressFinalize(simpleObject);
							//simpleObject = null;
						}
					}

					if (systemTransaction != null)
						systemTransaction.Status = TransactionStatus.Completed;

					
					//if (transactionRequestActionInfos != null)
					//	this.RaiseTransactionSucceeded(transactionRequestActionInfos);
				}
				else if (systemTransaction != null)
				{
					// TODO: Inform client what is the reason why transaction is not succeeded: Inform client about the problem and give them a choice to try fix error and problem and 
					// repeat request to server or rollback/reject changes.

					if (this.WorkingMode != ObjectManagerWorkingMode.Client)
					{
						this.RollbackTransaction(systemTransaction, requester: this);
						systemTransaction.Status = TransactionStatus.Rollbacked;
					}

					changeContainer.UnlockChanges();
				}

				foreach (SimpleObject simpleObject in changeContainer.TransactionRequests.Keys)
					simpleObject.InternalState = SimpleObjectInternalState.Normal;

				if (systemTransaction != null)
				{
					if (this.WorkingMode == ObjectManagerWorkingMode.Server && saveTransaction)
						systemTransaction.Save();

					this.RaiseTransactionFinished(systemTransaction, datastoreActions!, requester); //, this.transactionsActionsBySimpleObject.Values);

					result = new TransactionResult(systemTransaction.Status == TransactionStatus.Completed, systemTransaction.TransactionId, errorDescription);

					if (this.WorkingMode == ObjectManagerWorkingMode.Client || this.DeleteTransactionLogIfTransactionSucceeded)
						systemTransaction.Delete();

					this.lastTransaction = systemTransaction;
				}
				else
				{
					result = new TransactionResult(transactionSucceeded: true, errorDescription);
				}

				changeContainer.Clear();
				changeContainer.UnlockChanges();
				result.ValidationResult = validationResult;

				return result;
			}

			//this.ActiveTransaction = null;
			//this.oldTransactionsActionsBySimpleObject = new Dictionary<SimpleObject,TransactionAction>(this.transactionsActionsBySimpleObject);
			//this.transactionsActionsBySimpleObject.Clear();

			//foreach (SimpleObject simpleObject in changeContainer.TransactionRequests.Keys)
			//	simpleObject.SetInternalState(SimpleObjectInternalState.TransactionRequestProcessing);

		}

		public List<TransactionActionInfo> CreateClientSeriazableTransactionActionsFromTransactionRequests(IDictionary<SimpleObject, TransactionRequestAction> transactionRequests)
		{
			List<TransactionActionInfo> result = new List<TransactionActionInfo>(transactionRequests.Count);

			for (int i = 0; i < transactionRequests.Count; i++)
			{
				var item = transactionRequests.ElementAt(i);
				SimpleObject simpleObject = item.Key;
				TransactionRequestAction requestAction = item.Value;
				int tableId = simpleObject.GetModel().TableInfo.TableId;
				long objectId = simpleObject.Id;
				TransactionActionType actionType;
				List<PropertyIndexValuePair>? propertyIndexValues;

				if (requestAction == TransactionRequestAction.Save )
				{
					actionType = (simpleObject.IsNew) ? TransactionActionType.Insert : TransactionActionType.Update;

					propertyIndexValues = simpleObject.GetChangedClientSeriazablePropertyIndexValuePairs();
				}
				else
				{
					actionType = TransactionActionType.Delete;
					propertyIndexValues = null;
				}

				result.Add(new TransactionActionInfo(tableId, objectId, actionType, propertyIndexValues)); ; //, this.GetServerObjectModelInfo));
			}

			return result;
		}

		private List<TransactionActionInfo> CreateRolbackTransactionActionsFromTransactionRequests(IDictionary<SimpleObject, TransactionRequestAction> transactionRequests)
		{
			List<TransactionActionInfo> result = new List<TransactionActionInfo>(transactionRequests.Count);

			for (int i = 0; i < transactionRequests.Count; i++)
			{
				var item = transactionRequests.ElementAt(i);
				SimpleObject simpleObject = item.Key;
				TransactionRequestAction requestAction = item.Value;
				int tableId = simpleObject.GetModel().TableInfo.TableId;
				long objectId = simpleObject.Id;
				TransactionActionType actionType;
				List<PropertyIndexValuePair>? propertyIndexValues;

				if (requestAction == TransactionRequestAction.Save && simpleObject.IsNew)
				{
					actionType = TransactionActionType.Insert;
					propertyIndexValues = null;
				}
				else if (requestAction == TransactionRequestAction.Save)
				{
					propertyIndexValues = simpleObject.GetChangedSaveableOldPropertyIndexValues(propertySelector: propertyModel => propertyModel.IncludeInTransactionActionLog);

					if (propertyIndexValues.Count == 0)
						continue;

					actionType = TransactionActionType.Update;
				}
				else
				{
					actionType = TransactionActionType.Delete;
					propertyIndexValues = simpleObject.GetNonDefaultPropertyIndexValuesInternal(propertySelector: propertyModel => propertyModel.IncludeInTransactionActionLog, getFieldValue: propertyIndex => simpleObject.GetOldPropertyValue(propertyIndex)); ;
				}

				result.Add(new TransactionActionInfo(tableId, objectId, actionType, propertyIndexValues)); ; //, this.GetServerObjectModelInfo));
			}

			return result;
		}

		private List<TransactionActionInfo> CreateClientTransactionActionDataInfoListForSendingToServer(IDictionary<SimpleObject, TransactionRequestAction> transactionRequests)
		{
			List<TransactionActionInfo> result = new List<TransactionActionInfo>(transactionRequests.Count);

			foreach (var item in transactionRequests)
			{
				SimpleObject simpleObject = item.Key;
				TransactionRequestAction transactionRequestAction = item.Value;
				int tableId = simpleObject.GetModel().TableInfo.TableId;
				long objectId = simpleObject.Id;
				TransactionActionType transactionAction;
				PropertyIndexValuePair[]? propertyIndexValues = null;

				if (transactionRequestAction == TransactionRequestAction.Save)
				{
					transactionAction = (simpleObject.IsNew) ? TransactionActionType.Insert : TransactionActionType.Update;
					propertyIndexValues = simpleObject.GetChangedSaveablePropertyIndexValuePairs((propertyModel, propertyValue) =>
										  (propertyValue != null && propertyModel.IsEncrypted) ? this.EncryptProperty(propertyValue) : propertyValue);
				}
				else
				{
					transactionAction = TransactionActionType.Delete;
				}

				result.Add(new TransactionActionInfo(tableId, objectId, transactionAction, propertyIndexValues));
			}

			return result;
		}

		public List<DatastoreActionInfo> CreateDatastoreActionsFromTransactionRequests(IDictionary<SimpleObject, TransactionRequestAction> transactionRequests)
		{
			List<DatastoreActionInfo> result = new List<DatastoreActionInfo>(transactionRequests.Count);

			foreach (var item in transactionRequests)
			{
				SimpleObject simpleObject = item.Key;
				TransactionRequestAction transactionAction = item.Value;
				ISimpleObjectModel objectModel = simpleObject.GetModel();
				int tableId = objectModel.TableInfo.TableId;
				//DatastoreAction datastoreAction;
				DatastoreActionInfo? datastoreAction = null;

				//if (!simpleObject.GetModel().IsStorable || (simpleObject is GraphElement && !(simpleObject as GraphElement).SimpleObject.GetModel().IsStorable))
				if (!simpleObject.IsStorable) // || (simpleObject is GraphElement && !(simpleObject as GraphElement).SimpleObject.GetModel().IsStorable))
					continue;

				switch (transactionAction)
				{
					case TransactionRequestAction.Save:

						PropertyIndexValuePair[] propertyIndexValues;

						if (simpleObject.IsNew)
						{
							propertyIndexValues = simpleObject.GetStorablePropertyIndexValuePairs(this.NormalizeForWritingByDatastoreType);

							datastoreAction = new DatastoreActionInfo(tableId, simpleObject.Id, TransactionActionType.Insert, propertyIndexValues);
							//datastoreAction.SetActionInsert(objectModel.TableInfo.TableId, simpleObject.Id, propertyValues);
						}
						else // Update
						{
							propertyIndexValues = simpleObject.GetChangedStorablePropertyIndexValuePairs(this.NormalizeForWritingByDatastoreType);

							if (propertyIndexValues.Length > 0)
								datastoreAction = new DatastoreActionInfo(tableId, simpleObject.Id, TransactionActionType.Update, propertyIndexValues);
							//datastoreAction.SetActionUpdate(objectModel.TableInfo.TableId, simpleObject.Id, propertyValues);
						}

						break;

					case TransactionRequestAction.Delete:

						datastoreAction = new DatastoreActionInfo(tableId, simpleObject.Id, TransactionActionType.Delete, propertyIndexValues: null);
						//datastoreAction.SetActionDelete(objectModel.TableInfo.TableId, simpleObject.Id);

						break;
				}

				if (datastoreAction != null)
					result.Add(datastoreAction);
			}

			return result;
		}

		public ProcessTransactionRequestResult ProcessTransactionRequestFromClient(IEnumerable<TransactionActionInfo> transactionRequestWithDataInfo)
		{
			ChangeContainer changeContainer = new ChangeContainer();
			object requester = this;
			List<SimpleObjectPropertyRelationArgs> objectRelationPropertiesToSet = new List<SimpleObjectPropertyRelationArgs>();
			//List<SimpleObjectKeyArgs> objectsWithTempClientKeys = new List<SimpleObjectKeyArgs>();
			//Dictionary<long, int> newObjectTableIdsByTempClientObjectId = new Dictionary<long, int>();
			Dictionary<long, long> newObjectIdsByTempClientObjectId = new Dictionary<long, long>();
			//Dictionary<long, object[]> propertyValuesByTempClientId = new Dictionary<long, object[]>();
			//Dictionary<long, Dictionary<int, object>> propertyValuesByIndexByTempClientObjectId = new Dictionary<long, Dictionary<int, object>>();
			//HashSet<SimpleObject> transactionSimpleObjectsList = new HashSet<SimpleObject>();
			List<SimpleObject> simpleObjectList = new List<SimpleObject>(transactionRequestWithDataInfo.Count());

			// Can we change this with a list???, why HashSet needed???


			//int foreignTableId = 0;
			//int graphElementTableId = this.GetObjectModel(typeof(GraphElement)).TableInfo.TableId; // GraphElementModel.TableId;

			bool revokeTransaction = false;
			string infoMessage = String.Empty;
			//long[] newObjectIds = null;

			// Insert new non GraphElements first <- Not need any more
			foreach (TransactionActionInfo item in transactionRequestWithDataInfo) // First pass, just create new object without loadind data, but collecting new ObjectId's
			{
				ISimpleObjectModel? objectModel = this.GetObjectModel(item.TableId);
				SimpleObject? simpleObject;

				if (objectModel == null)
				{
					revokeTransaction = true;
					infoMessage.WriteLine("SimpleObject server model doesn't exists: TableId=" + item.TableId);
					Debug.WriteLine("ProcessTransactionRequestArgs: Server has no object info of TableId: " + item.TableId);

					break;
					//return new ProcessTransactionRequestResult(revokeTransaction, infoMessage, newObjectIds);
				}

				if (item.ActionType == TransactionActionType.Insert) // && tableId != graphElementTableId) // Insert non GraphElement first
				{
					long tempClientObjectId = item.ObjectId;
					simpleObject = this.CreateNewEmptyObject(objectModel.ObjectType, isNew: true, objectId: 0, changeContainer, requester: this);
					simpleObject.InternalState = SimpleObjectInternalState.TransactionRequestProcessing;
					long newObjectId = simpleObject.Id; // enforce ObjectId creation and adding object into object cache.
					newObjectIdsByTempClientObjectId.Add(tempClientObjectId, newObjectId);
					item.ObjectId = newObjectId;
					//objectsWithTempClientKeys.Add(new SimpleObjectKeyArgs(simpleObject, tableId, tempClientObjectId)); // Check if this is necessary
				}
				else 
				{
					simpleObject = this.GetObject(item.TableId, item.ObjectId);

					if (simpleObject is null)
					{
						revokeTransaction = true;
						infoMessage.WriteLine("SimpleObject does not exists: TableId=" + item.TableId + ", ObjectId=" + item.ObjectId);
						Debug.WriteLine("ProcessTransactionRequestArgs: Server has no object of TableId=" + item.TableId + " and ObjectId=" + item.ObjectId);

						break;
					}

					simpleObject.InternalState = SimpleObjectInternalState.TransactionRequestProcessing;

					if (item.ActionType == TransactionActionType.Delete)
						simpleObject.RequestDelete(changeContainer, requester: this);
				}

				if (item.PropertyIndexValues != null)
				{
					simpleObject.LoadPropertyValuesWithoutRelations(item.PropertyIndexValues, changeContainer, ref objectRelationPropertiesToSet, out string info, requester: this);

					if (!info.IsNullOrEmpty())
						infoMessage.WriteLine(info);
				}

				simpleObjectList.Add(simpleObject);
			}


			//if (!revokeTransaction)
			//{
			//// Second, insert GraphElements
			//foreach (TransactionActionInfo transactionActionWithDataInfo in transactionActionWithDataInfoList)
			//{
			//	int tableId = transactionActionWithDataInfo.TableId;

			//	if (transactionActionWithDataInfo.ActionType == TransactionActionType.Insert && tableId == graphElementTableId) // Second, insert GraphElements
			//	{
			//		long tempClientObjectId = transactionActionWithDataInfo.ObjectId;
			//		ISimpleObjectModel objectModel = this.GetObjectModel(tableId);
			//		Dictionary<int, object> propertyValuesByPropertyIndex = new Dictionary<int, object>(transactionActionWithDataInfo.PropertyIndexes.Count());
			//		long newObjectId;


			//		for (int i = 0; i < transactionActionWithDataInfo.PropertyIndexes.Count(); i++)
			//		{
			//			int propertyIndex = transactionActionWithDataInfo.PropertyIndexes.ElementAt(i);
			//			object propertyValue = transactionActionWithDataInfo.PropertyValues.ElementAt(i);

			//			propertyValuesByPropertyIndex.Add(propertyIndex, propertyValue);
			//		}


			//		int graphKey = (int)propertyValuesByPropertyIndex[GraphElementModel.PropertyModel.GraphKey.PropertyIndex];
			//		int simpleObjectTableId = (int)propertyValuesByPropertyIndex[GraphElementModel.PropertyModel.ObjectTableId.PropertyIndex];
			//		long simpleObjectId = (long)propertyValuesByPropertyIndex[GraphElementModel.PropertyModel.ObjectId.PropertyIndex];

			//		if (simpleObjectId < 0)
			//			simpleObjectId = newObjectIdsByTempClientObjectId[simpleObjectId];

			//		SimpleObject simpleObject = this.GetObject(simpleObjectTableId, simpleObjectId);

			//		if (simpleObject == null)
			//		{
			//			revokeTransaction = true;
			//			infoMessage.WriteLine("GraphElement SimpleObject doesn't exists: GraphElement Id=" + tempClientObjectId);
			//			Debug.WriteLine("ProcessTransactionRequestArgs: GraphElement SimpleObject doesn't exists: GraphElement Id=" + tempClientObjectId);

			//			break;
			//		}

			//		//SystemGraph graph = this.GetSystemGraph(graphKey);
			//		GraphElement graphElement = new GraphElement(this, graphKey, simpleObject, parent: null, changeContainer); // parent will be set by setting relation property

			//		graphElement.InternalState = SimpleObjectInternalState.TransactionRequestProcessing;
			//		newObjectId = graphElement.Id; // enforce ObjectId creation and adding object in object cache.

			//		newObjectIdsByTempClientObjectId.Add(tempClientObjectId, newObjectId);
			//		objectsWithTempClientKeys.Add(new SimpleObjectKeyArgs(graphElement, tableId, tempClientObjectId));
			//		transactionSimpleObjectsList.Add(graphElement);

			//		//objectsWithNewKeys.Add(new SimpleObjectKeyArgs(graphElement, tableId, newObjectId));

			//		foreach (var propertyItem in propertyValuesByPropertyIndex)
			//		{
			//			int propertyIndex = propertyItem.Key;
			//			object propertyValue = propertyItem.Value;
			//			IPropertyModel propertyModel = objectModel.PropertyModels[propertyIndex];

			//			if (propertyModel.PropertyIndex == GraphElementModel.PropertyModel.GraphKey.PropertyIndex || propertyModel.PropertyIndex == GraphElementModel.PropertyModel.ObjectTableId.PropertyIndex
			//																									  || propertyModel.PropertyIndex == GraphElementModel.PropertyModel.ObjectId.PropertyIndex)
			//				continue; // This is already has been set;

			//			if (propertyModel.IsRelationTableId)
			//			{
			//				foreignTableId = (int)propertyValue;
			//			}
			//			else if (propertyModel.IsRelationObjectId)
			//			{
			//				long foreignObjectId = (long)propertyValue;
			//				IRelationModel relationModel = this.GetRelationModel(propertyModel.RelationKey);

			//				if (relationModel is IOneToOneOrManyRelationModel)
			//				{
			//					if (foreignTableId == 0)
			//						foreignTableId = (relationModel as IOneToOneOrManyRelationModel).PrimaryObjectTableId; // If foreign TableId is not specified it is fixed and is determined by the relation model definition

			//					objectRelationPropertiesToSet.Add(new SimpleObjectPropertyRelationArgs(graphElement, foreignTableId, foreignObjectId, relationModel as IOneToOneOrManyRelationModel));
			//				}
			//				else
			//				{
			//					simpleObject.SetPropertyValue(propertyModel, propertyValue, changeContainer, requester: this);
			//				}

			//				foreignTableId = 0;
			//			}
			//			else
			//			{
			//				//propertyValue = this.NormalizeWhenReadingByPropertyType(propertyModel, propertyValue);

			//				if (propertyModel.IsEncrypted)
			//					propertyValue = this.DecryptProperty(propertyValue);

			//				graphElement.SetPropertyValue(propertyModel, propertyValue, changeContainer, requester: this);
			//			}
			//		}
			//	}
			//}

			if (!revokeTransaction)
			{
				//// Process Update and Delete
				//foreach (TransactionActionInfo transactionActionWithDataInfo in transactionRequestWithDataInfo)
				//{
				//	if (transactionActionWithDataInfo.ActionType == TransactionActionType.Insert) // This has already been done.
				//		continue;

				//	int tableId = transactionActionWithDataInfo.TableId;
				//	long objectId = transactionActionWithDataInfo.ObjectId;
				//	ISimpleObjectModel? objectModel = this.GetObjectModel(tableId);
				//	SimpleObject? simpleObject = this.GetObject(tableId, objectId);

				//	//if (objectModel is null || simpleObject is null)
				//	//{
				//	//	revokeTransaction = true;
				//	//	infoMessage.WriteLine("SimpleObject server model doesn't exists: TableId=" + tableId);
				//	//	Debug.WriteLine("ProcessTransactionRequestArgs: Server has no object info of TableId: " + tableId);

				//	//	break;
				//	//}
				//	//else
				//	//{
				//		transactionSimpleObjectsList.Add(simpleObject);
				//	//}

				//	if (transactionActionWithDataInfo.ActionType == TransactionActionType.Update) // Second, insert GraphElements
				//	{
				//		simpleObject.InternalState = SimpleObjectInternalState.TransactionRequestProcessing;
				//		simpleObject.LoadObjectPropertyValuesWithoutRelations(transactionActionWithDataInfo.PropertyIndexValues!, changeContainer, ref objectRelationPropertiesToSet, out string info, requester: this);

				//		if (!info.IsNullOrEmpty())
				//			infoMessage.WriteLine(info);

				//		//for (int i = 0; i < transactionActionWithDataInfo.PropertyIndexValues.Count(); i++)
				//		//{
				//		//	var item = transactionActionWithDataInfo.PropertyIndexValues.ElementAt(i);
				//		//	IPropertyModel propertyModel = objectModel.PropertyModels[item.PropertyIndex];

				//		//	if (propertyModel != null)
				//		//	{
				//		//		if (propertyModel.IsRelationTableId)
				//		//		{
				//		//			foreignTableId = (int)item.PropertyValue!;
				//		//		}
				//		//		else if (propertyModel.IsRelationObjectId)
				//		//		{
				//		//			long foreignObjectId = (long)item.PropertyValue!;
				//		//			IRelationModel? relationModel = this.GetRelationModel(propertyModel.RelationKey);

				//		//			if (relationModel is IOneToOneOrManyRelationModel oneToOneOrManyRelationModel)
				//		//			{
				//		//				if (foreignTableId == 0)
				//		//				{
				//		//					if (oneToOneOrManyRelationModel.PrimaryObjectTableId <= 0) // SimpleObject has ForeignObjectTableId property and since it is not changed (foreignTableId == 0) read its value from the object
				//		//						foreignTableId = simpleObject.GetPropertyValue<int>(propertyModel.PropertyIndex);
				//		//					else
				//		//						foreignTableId = oneToOneOrManyRelationModel.PrimaryObjectTableId; // SimpleObject foreign TableId doesn't exists as a property and it is fixed and determined by the relation model definition.
				//		//				}

				//		//				objectRelationPropertiesToSet.Add(new SimpleObjectPropertyRelationArgs(simpleObject, foreignTableId, foreignObjectId, oneToOneOrManyRelationModel));
				//		//			}
				//		//			else
				//		//			{
				//		//				simpleObject.SetPropertyValue(propertyModel, item.PropertyValue, changeContainer, requester: this);
				//		//			}

				//		//			foreignTableId = 0;
				//		//		}
				//		//		else
				//		//		{
				//		//			//propertyValue = this.NormalizeWhenReadingByPropertyType(propertyModel, propertyValue);

				//		//			object? propertyValue = item.PropertyValue;

				//		//			if (propertyModel.IsEncrypted && propertyValue != null)
				//		//				propertyValue = this.DecryptProperty(propertyValue);

				//		//			simpleObject.SetPropertyValue(propertyModel, propertyValue, changeContainer, requester: this);
				//		//		}
				//		//	}
				//		//	else
				//		//	{
				//		//		string info = String.Format("Server has no property model, TableId:{0}, PropertyIndex:{2}, PropertyName:{3}", tableId, item.PropertyIndex); //, propertyModel.PropertyName);

				//		//		infoMessage.WriteLine(info);
				//		//		Debug.WriteLine("ProcessTransactionRequestArgs: " + info);
				//		//	}
				//		//}
				//	}
				//	else // transactionActionWithDataInfo.TransactionAction == TransactionActionType.Delete
				//	{
				//		simpleObject.InternalState = SimpleObjectInternalState.TransactionRequestProcessing;
				//		simpleObject.RequestDelete(changeContainer, requester: this);
				//	}
				//}

				// Replace client temp negative SimpleObject Id's with realone in input variable transactionRequestWithDataInfo to be used for sending transaction info to the other clients with correct server SimpleObject Id's for new objects.
				foreach (TransactionActionInfo item in transactionRequestWithDataInfo)
				{
					if (item.propertyIndexValueArray != null)
					{
						ISimpleObjectModel objectModel = this.GetObjectModel(item.TableId)!;

						for (int i = 0; i < item.propertyIndexValueArray!.Length; i++)
						{
							var propertyModel = objectModel.PropertyModels[item.propertyIndexValueArray[i].PropertyIndex];

							if (propertyModel.IsRelationObjectId)
							{
								long foreignObjectId = (long)item.propertyIndexValueArray[i].PropertyValue!;

								if (foreignObjectId < 0)
									item.propertyIndexValueArray[i].PropertyValue = newObjectIdsByTempClientObjectId[foreignObjectId];
							}
						}
					}
				}

				// Sets relation foreign objects
				// TODO: What about Many to Many relations and its negative Id's values for new ones!!!
				foreach (var item in objectRelationPropertiesToSet)
				{
					//foreignTableId = item.ForeignTableId; ;
					long foreignObjectId = (item.ForeignObjectId < 0) ? newObjectIdsByTempClientObjectId[item.ForeignObjectId] : item.ForeignObjectId;

					//foreignTableId = 0;
					//long foreignObjectId = 0;

					//// Try to find foreign object within old object keys (if was deleted and now is recreated with new Id)
					//SimpleObjectKeyArgs oldKeyArgs = objectsWithTempClientKeys.FirstOrDefault(x => x.TableId == item.ForeignTableId && x.ObjectId == item.ForeignObjectId);

					//if (oldKeyArgs != null)
					//{
					//	if (oldKeyArgs.SimpleObject != null)
					//	{
					//		foreignTableId = oldKeyArgs.TableId;
					//		foreignObjectId = oldKeyArgs.SimpleObject.Id;
					//	}
					//}
					//else
					//{
					//	foreignTableId = item.ForeignTableId;
					//	foreignObjectId = item.ForeignObjectId;
					//}

					item.SimpleObject.SetRelationPrimaryObject(item.ForeignTableId, foreignObjectId, item.RelationModel, changeContainer, requester: this);
				}
			}
			//}

			foreach (SimpleObject simpleObject in simpleObjectList)
				simpleObject.InternalState = SimpleObjectInternalState.Normal;

			if (revokeTransaction)
			{
				var transactionRequests = changeContainer.TransactionRequests;

				foreach (var item in transactionRequests)
				{
					TransactionRequestAction requestAction = item.Value;
					SimpleObject simpleObject = item.Key;

					if (requestAction == TransactionRequestAction.Save)
					{
						if (simpleObject.IsNew)
							simpleObject.RequestDelete(changeContainer, requester);
						else
							simpleObject.RejectChanges(changeContainer, requester);
					}
					else
					{
						simpleObject.CancelDeleteRequest(changeContainer, requester);
					}
				}
			}

			TransactionResult transactionResult = this.CommitChanges(changeContainer, requester);

			revokeTransaction &= transactionResult.TransactionSucceeded;
			
			if (!transactionResult.InfoMessage.IsNullOrEmpty())
				infoMessage.WriteLine(transactionResult.InfoMessage!); // If any

			return new ProcessTransactionRequestResult(revokeTransaction, transactionResult, newObjectIdsByTempClientObjectId.Values, infoMessage);
		}

		public void OnForeignTransactionCompleted(IEnumerable<TransactionActionInfo> transactionActions)
		{
			List<SimpleObjectPropertyRelationArgs> objectRelationPropertiesToSet = new List<SimpleObjectPropertyRelationArgs>();
			List<SimpleObject> simpleObjectsAddedList = new List<SimpleObject>();
			List<SimpleObject> simpleObjectsUpdatedList = new List<SimpleObject>();
			List<SimpleObject> simpleObjectsUpdateWithoutAcceptingChangesList = new List<SimpleObject>();
			List<SimpleObject> simpleObjectsForDeleteList = new List<SimpleObject>();
			object requester = this.foreignClientRequester;

			using (ChangeContainer changeContainer = new ChangeContainer())
			{
				foreach (TransactionActionInfo transactionActionWithDataInfo in transactionActions) // First pass, Insert new objects and load with only non relation data
				{
					if (this.GetObjectModel(transactionActionWithDataInfo.TableId) is ISimpleObjectModel objectModel)
					{
						if (transactionActionWithDataInfo.ActionType == TransactionActionType.Insert) // && tableId != graphElementTableId) // Insert non GraphElement first
						{
							SimpleObject simpleObject = this.CreateNewEmptyObject(objectModel.ObjectType, isNew: true, transactionActionWithDataInfo.ObjectId, changeContainer, requester);

							simpleObject.InternalState = SimpleObjectInternalState.TransactionRequestProcessing;

							if (transactionActionWithDataInfo.PropertyIndexValues != null)
								simpleObject.LoadFieldValuesWithoutRelations(transactionActionWithDataInfo.PropertyIndexValues, changeContainer, ref objectRelationPropertiesToSet, out string info, requester);
							
							simpleObjectsAddedList.Add(simpleObject);

						}
						else if (transactionActionWithDataInfo.ActionType == TransactionActionType.Update && this.IsObjectInCache(transactionActionWithDataInfo.TableId, transactionActionWithDataInfo.ObjectId) && this.GetObject(transactionActionWithDataInfo.TableId, transactionActionWithDataInfo.ObjectId) is SimpleObject simpleObject)
						{
							simpleObject.InternalState = SimpleObjectInternalState.TransactionRequestProcessing;

							if (simpleObject.IsChanged)
								simpleObjectsUpdateWithoutAcceptingChangesList.Add(simpleObject);
							else
								simpleObjectsUpdatedList.Add(simpleObject);

							if (transactionActionWithDataInfo.PropertyIndexValues != null)
								simpleObject.LoadPropertyValuesWithoutRelations(transactionActionWithDataInfo.PropertyIndexValues, changeContainer, ref objectRelationPropertiesToSet, out string info, requester);
						}
						else if (this.IsObjectInCache(transactionActionWithDataInfo.TableId, transactionActionWithDataInfo.ObjectId)) // transactionActionWithDataInfo.ActionType == TransactionActionType.Delete)
						{
							simpleObject = this.GetObject(transactionActionWithDataInfo.TableId, transactionActionWithDataInfo.ObjectId)!;

							simpleObject.InternalState = SimpleObjectInternalState.TransactionRequestProcessing;
							simpleObject.RequestDelete(changeContainer, findRelatedObjectsForDelete: false, requester: this);
							simpleObject.RemoveAllRelatedObjectsFromAllRelatedObjectCaches(changeContainer, requester);
							this.RemoveObjectFromCache(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);

							simpleObject.IsDeleted = true;
							simpleObjectsForDeleteList.Add(simpleObject);

							//this.OnAfterDelete(simpleObject, changeContainer, requester);
							//this.RaiseAfterDelete(simpleObject, requester);
							//simpleObject.Dispose();
						}
					}
				}

				foreach (var item in objectRelationPropertiesToSet) // Sets relation foreign objects
					item.SimpleObject.SetRelationPrimaryObject(item.ForeignTableId, item.ForeignObjectId, item.RelationModel, changeContainer, requester);

				foreach (SimpleObject simpleObject in simpleObjectsAddedList) 
				{
					simpleObject.AcceptChanges(changeContainer: null, requester: this);
					simpleObject.SetIsNew(false);
					simpleObject.InternalState = SimpleObjectInternalState.Normal;

					if (simpleObject is GraphElement graphElement)
						this.NewGraphElementIsCreated(graphElement, changeContainer, requester);
					else
						this.NewObjectIsCreated(simpleObject, requester);
				}

				foreach (SimpleObject simpleObject in simpleObjectsUpdatedList) 
				{
					simpleObject.AcceptChanges(changeContainer: null, requester: this);
					simpleObject.InternalState = SimpleObjectInternalState.Normal;
				}

				foreach (SimpleObject simpleObject in simpleObjectsUpdateWithoutAcceptingChangesList)
					simpleObject.InternalState = SimpleObjectInternalState.Normal;

				foreach (SimpleObject simpleObject in simpleObjectsForDeleteList)
				{
					simpleObject.InternalState = SimpleObjectInternalState.Normal;
					this.OnAfterDelete(simpleObject, changeContainer, requester);
					this.RaiseAfterDelete(simpleObject, requester);
					simpleObject.Dispose();
				}
			}
		}

		//public Transaction BeginTransaction_New(User user)
		//{
		//	lock (this.lockTransaction)
		//	{
		//		SystemTransaction systemTransaction = new SystemTransaction(this, ref this.systemTransactions, user.Id, null, TransactionStatus.Started);
		//		Transaction transaction = new Transaction(systemTransaction, user);

		//		this.activeTransactions.Add(transaction);

		//		// TODO: Change TransactionStarted with Transaction object argument 
		//		this.RaiseTransactionStarted(this.ActiveTransaction, requester: user);

		//		return transaction;
		//	}
		//}

		//public bool EndTransaction_New(Transaction transaction, object requester)
		//{
		//	// Validate transaction requests first
		//	transaction.State = TransactionState.ValidationInProgress;

		//	foreach (var item in transaction.SystemTransaction.TransactionRequestsBySimpleObject)
		//	{
		//		SimpleObject simpleObject = item.Key;
		//		TransactionRequestAction requestAction = item.Value;
		//		ValidationResult validationResult = (requestAction == TransactionRequestAction.Save) ? this.ValidateSave(simpleObject, transaction.SystemTransaction.TransactionRequestsBySimpleObject) 
		//																							 : this.ValidateDelete(simpleObject, transaction.SystemTransaction.TransactionRequestsBySimpleObject);

		//		if (!validationResult.Passed)
		//		{
		//			transaction.State = TransactionState.ValidationFails;
		//			transaction.ValidationResult = new SimpleObjectValidationResult(simpleObject, false, validationResult.Message, validationResult);

		//			return false;
		//		}
		//	}

		//	transaction.State = TransactionState.ValidatedSuccessfully;
		//	transaction.ValidationResult = SimpleObjectValidationResult.DefaultSuccessResult;

		//	// TODO: ......







		//	if (this.WorkingMode == ObjectManagerWorkingMode.Client)
		//	{
		//		RequestResult<ClientTransactionResult> requestResult = this.RemoteDatastore.ProcessTransactionRequest(transaction.SystemTransaction.TransactionRequestsBySimpleObject);
		//		ClientTransactionResult transactionResult = requestResult.ResultValue;

		//		bool transactionSucceed = requestResult.Succeeded && transactionResult.TransactionResult.TransactionSucceeded;

		//		if (transactionSucceed)
		//		{
		//			// Sets the new object Ids values through the simpleObject properties with temp object Id values.
		//			foreach (var item in transactionResult.SimpleObjectPropertiesWithTempClientObjectIdToChange)
		//			{
		//				long newObjectId = transactionResult.NewObjectIdsByTempClientObjectId[item.ObjectId];

		//				item.SimpleObject.SetPropertyValueInternal(item.PropertyModel, newObjectId, addOrRemoveInChangedProperties: false, firePropertyValueChangeEvent: false, requester: this);
		//			}

		//			foreach (var item in this.ActiveTransaction.TransactionRequestsBySimpleObject)
		//			{
		//				SimpleObject simpleObject = item.Key;

		//				if (simpleObject.Id < 0)
		//				{
		//					long tempId = simpleObject.Id;
		//					long newId = transactionResult.NewObjectIdsByTempClientObjectId[tempId];
		//					ObjectCache objectCache = this.GetObjectCache(simpleObject.GetModel().TableInfo.TableId);

		//					objectCache.ReplaceTempId(simpleObject, newId);
		//					this.ClientTempObjectIdGenerator.RemoveKey(-tempId);

		//					this.RaiseObjectIdChange(simpleObject, tempId, newId);
		//				}
		//			}
		//		}
		//		else
		//		{
		//			// TODO: If transaction not succeeded Rollback for already deleted object should be modified. 
		//			// Furthermore, transaction should repeat request to server or inform client about the problem and give them a choice to try repeat request to server or rollback/reject changes.
		//			int TEST = 0;
		//		}
		//	}
		//	else // if (this.WorkingMode == ObjectManagerWorkingMode.Server)
		//	{

		//	}



		//		//TODO:

		//	return transaction.Status == TransactionStatus.Completed;
		//}

		//public SystemTransaction BeginTransaction(User user)
		//{
		//	lock (this.lockTransaction)
		//	{
		//		//SimpleObjectKey sysAdminKey = new SimpleObjectKey(Tables.Instance.Administrators.TableId, 1, 1);

		//		if (this.ActiveTransaction == null)
		//		{
		//			this.ActiveTransaction = new SystemTransaction(this, ref this.systemTransactions, user.Id, null, TransactionStatus.Started);
		//		}
		//		else
		//		{
		//			throw new Exception("You cannot begin transaction while current active transaction is in progress.");
		//		}
		//		//this.CurrentTransaction = new SystemTransaction(this.maxSystemTransactionId++, this.SystemAdmin.Key.Guid, this.clientId, TransactionStatus.Started, null);


		//		//this.transactionsActionsBySimpleObject.Clear();


		//		//this.CurrentTransaction.AdministratorGuid = this.SystemAdmin.Key.Guid;
		//		//this.CurrentTransaction.TransactionStatus = TransactionStatus.Started;

		//		this.RaiseTransactionStarted(this.ActiveTransaction, requester: user);

		//		return this.ActiveTransaction;
		//	}
		//}

		//public void EndTransaction(object requester)
		//{
		//	if (this.WorkingMode != ObjectManagerWorkingMode.Server) // this.WorkingMode == ObjectManagerWorkingMode.Client || this.WorkingMode == ObjectManagerWorkingMode.ClientWithLocalDatastore)
		//	{
		//		//// reverse transaction request SimpleObject order
		//		//this.ActiveTransaction.TransactionRequestsBySimpleObject = this.ActiveTransaction.TransactionRequestsBySimpleObject.Reverse().ToDictionary(x => x.Key, x => x.Value);

		//		// Ensure to include all related objects into transaction first
		//		var currentTransactionRequests = this.ActiveTransaction.TransactionRequestsBySimpleObject.ToArray();

		//		for (int i = 0; i < currentTransactionRequests.Count(); i++)
		//		{
		//			SimpleObject simpleObject = currentTransactionRequests[i].Key;

		//			foreach (var item in simpleObject.RelatedTransactionRequests.ToArray())
		//			{
		//				SimpleObject relatedSimpleObject = item.Key;
		//				TransactionRequestAction relatedRequestAction = item.Value;

		//				switch (relatedRequestAction)
		//				{
		//					case TransactionRequestAction.Save:

		//						//if (transactionRequest.SimpleObject.IsNew)
		//						//{
		//						//	this.ProcessTransactionUpdateActionInsert(transactionRequest.SimpleObject);
		//						//}
		//						//else
		//						//{
		//						//	PropertyValueSequence nolrmalizedChangerOldPropertyValues = transactionRequest.SimpleObject.GetChangedOldPropertyValues(this.NormalizeWhenReadingByPropertyType);

		//						//	if (nolrmalizedChangerOldPropertyValues.Count > 0)
		//						//		this.ProcessTransactionActionUpdate(transactionRequest.SimpleObject, nolrmalizedChangerOldPropertyValues);
		//						//}

		//						this.ProcessTransactionUpdateRequest(relatedSimpleObject);

		//						break;

		//					case TransactionRequestAction.Delete:

		//						//if (!transactionRequest.SimpleObject.IsDeleted)
		//						//{
		//						//	//object[] normalizedPropertyValues = null; //transactionRequest.SimpleObject.GetStorablePropertyValues(this.NormalizeForWritingByPropertyType);
		//						//	//int propertySequenceId = 0; //transactionRequest.SimpleObject.GetModel().StorablePropertySequence.PropertySequenceId;

		//						//	PropertyValueSequence nolrmalizedPropertyValues = transactionRequest.SimpleObject.GetStorableSerializablePropertyValues(this.NormalizeForWritingByPropertyType);

		//						//	transactionRequest.SimpleObject.Delete(requester: this);
		//						//	this.ProcessTransactionActionDelete(transactionRequest.SimpleObject, nolrmalizedPropertyValues);
		//						//}

		//						this.ProcessTransactionDeleteRequest(relatedSimpleObject);

		//						break;
		//				}
		//			}

		//			simpleObject.ClearRelatedTransactionRequests();
		//		}
		//	}


		//	//byte[] actionData = this.SerializeTransactionActions(this.CurrentTransactionActions);

		//	//Stopwatch sw = new Stopwatch();

		//	//sw.Start();
		//	//byte[] actionDataCompression1 = MiniLZO.Compress(actionData); // Very good!!!
		//	//sw.Stop();
		//	//string time1 = sw.GetElapsedTimeInMicroseconds().ToString();

		//	//sw.Start();
		//	//byte[] actionDataCompression2;
		//	//MiniLZO2.Compress(actionData, out actionDataCompression2); // Very good!!! <- Pobjednik - Best 
		//	//sw.Stop();
		//	//string time2 = sw.GetElapsedTimeInMicroseconds().ToString();

		//	//sw.Start();
		//	//byte[] actionDataCompression3 = MiniLZOSafe.Compress(actionData); // Very good!!! - Best Safe
		//	//sw.Stop();
		//	//string time3 = sw.GetElapsedTimeInMicroseconds().ToString();

		//	//sw.Start();
		//	//byte[] actionDataCompression4 = QuickLZ.Compress(actionData, 3); // Very good!!! - zna nekad podbaciti pa nema kompresije
		//	//sw.Stop();
		//	//string time4 = sw.GetElapsedTimeInMicroseconds().ToString();

		//	//sw.Start();
		//	//byte[] actionDataCompression5 = SevenZipHelper.Compress(actionData, 1 << 12); // Very good, but too slow!!!
		//	//sw.Stop();
		//	//string time5 = sw.GetElapsedTimeInMicroseconds().ToString();


		//	//byte[] actionDataCompression6 = GZip.Compress(actionData);
		//	////byte[] actionDataCompression7 = new LZOCompressor().Compress(actionData);
		//	//byte[] actionDataCompression8 = QuickLZ.Compress(actionData, 1);
		//	//byte[] actionDataCompression9 = new LZF().Compress(actionData);
		//	//lock (lockObject)
		//	//{

		//	//if (this.transactionsActionsBySimpleObject.Count == 0)
		//	//	return;

		//	lock (this.lockTransaction)
		//	{
		//		if (this.ActiveTransaction != null && this.ActiveTransaction.TransactionRequestsBySimpleObject.Count > 0)
		//		{
		//			bool transactionSucceed = false;

		//			this.lastTransaction = null;

		//			if (this.WorkingMode == ObjectManagerWorkingMode.Client)
		//			{
		//				if (this.ActiveTransaction.TransactionRequestsBySimpleObject.Count == 0)
		//				{
		//					int TODO = 0;

		//					TODO = 1;
		//				}

		//				//transactionSucceed = this.RemoteDatastore.ProcessTransactionRequest(this.ActiveTransaction);

		//				if (this.TransactionFinished != null)
		//					this.ActiveTransaction.CreateTransactionActionLogsFromTransactionRequests();

		//				RequestResult<ClientTransactionResult> requestResult = this.RemoteDatastore.ProcessTransactionRequest(this.ActiveTransaction.TransactionRequestsBySimpleObject);
		//				ClientTransactionResult clientTransactionResult = requestResult.ResultValue;

		//				transactionSucceed = requestResult.Succeeded && clientTransactionResult.TransactionResult.TransactionSucceeded;

		//				if (transactionSucceed)
		//				{
		//					// Sets the new object Ids values through the simpleObject properties with temp object Id values.
		//					foreach (var item in clientTransactionResult.SimpleObjectPropertiesWithTempClientObjectIdToChange)
		//					{
		//						long newObjectId = clientTransactionResult.NewObjectIdsByTempClientObjectId[item.ObjectId];

		//						item.SimpleObject.SetPropertyValueInternal(item.PropertyModel, newObjectId, addOrRemoveInChangedProperties: false, firePropertyValueChangeEvent: false, requester: this);
		//					}

		//					foreach (var item in this.ActiveTransaction.TransactionRequestsBySimpleObject)
		//					{
		//						SimpleObject simpleObject = item.Key;

		//						if (simpleObject.Id < 0)
		//						{
		//							long tempId = simpleObject.Id;
		//							long newId = clientTransactionResult.NewObjectIdsByTempClientObjectId[tempId];
		//							ObjectCache objectCache = this.GetObjectCache(simpleObject.GetModel().TableInfo.TableId);

		//							objectCache.ReplaceTempId(simpleObject, newId);
		//							this.ClientTempObjectIdGenerator.RemoveKey(-tempId);

		//							this.RaiseObjectIdChange(simpleObject, tempId, newId);
		//						}
		//					}
		//				}
		//				else
		//				{
		//					// TODO: If transaction not succeeded Rollback for already deleted object should be modified. 
		//					// Furthermore, transaction should repeat request to server or inform client about the problem and give them a choice to try repeat request to server or rollback/reject changes.
		//					int TEST = 0;
		//				}
		//			}
		//			else // (this.WorkingMode == ObjectManagerWorkingMode.Server || this.WorkingMode == ObjectManagerWorkingMode.ClientWithLocalDatastore)
		//			{
		//				this.ActiveTransaction.CreateTransactionActionLogsFromTransactionRequests();
		//				this.ActiveTransaction.CreateActionDataFromTransactionActionLogs();
		//				this.ActiveTransaction.CreateDatastoreActionsFromTransactionRequests(this.GetObjectModel, this.GetServerObjectPropertyInfo, this.NormalizeForWritingByDatastoreType);

		//				//byte[] uncompressedActionData = SimpleObjectManager.SerializeTransactionActions(this.ActiveTransaction.TransactionActionLogs);
		//				//this.ActiveTransaction.ActionData = uncompressedActionData; // this.CompressData(uncompressedActionData);

		//				this.ActiveTransaction.Save();

		//				// Checking if serialization/deserialization is working
		//				//byte[] compressedActionData = this.CompressData(uncompressedActionData);
		//				//byte[] uncompressedActionData2 = this.DecompressData(compressedActionData);

		//				//List<SystemTransactionAction> currentTransaction2 = this.DeserializeTransactionActions(this.CurrentTransaction, uncompressedActionData);

		//				foreach (DatastoreAction datastoreAction in this.ActiveTransaction.DatastoreActions)
		//				{
		//					switch (datastoreAction.ActionType)
		//					{
		//						case TransactionActionType.Insert:

		//							this.LocalDatastore.InsertRecord(datastoreAction.GetObjectModel().TableInfo.TableName, datastoreAction.PropertyValueSequence.PropertyModels, datastoreAction.PropertyValueSequence.PropertyValues);
		//							break;

		//						case TransactionActionType.Update:

		//							this.LocalDatastore.UpdateRecord(datastoreAction.GetObjectModel().TableInfo.TableName, SimpleObject.StringPropertyId, datastoreAction.ObjectId, datastoreAction.PropertyValueSequence.PropertyModels, datastoreAction.PropertyValueSequence.PropertyValues);
		//							break;

		//						case TransactionActionType.Delete:

		//							this.LocalDatastore.DeleteRecord(datastoreAction.GetObjectModel().TableInfo.TableName, SimpleObject.StringPropertyId, datastoreAction.ObjectId);
		//							break;
		//					}
		//				}

		//				if (this.ActiveTransaction.DatastoreActions.Count() == 0)
		//				{
		//					int TODO = 0;
		//				}



		//				//foreach (KeyValuePair<SimpleObject, TransactionRequestAction> item in this.ActiveTransaction.TransactionRequestsBySimpleObject)
		//				//{
		//				//	SimpleObject simpleObject = item.Key;
		//				//	ISimpleObjectModel objectModel = simpleObject.GetModel();
		//				//	TransactionRequestAction transactionAction = item.Value;

		//				//	switch (transactionAction)
		//				//	{
		//				//		case TransactionRequestAction.Save:

		//				//			if (simpleObject.IsNew)
		//				//			{
		//				//				PropertyValueSequence propertyValueSequence = simpleObject.GetStorablePropertyValues(this.NormalizeForWritingByDatastoreType);
		//				//				this.LocalDatastore.InsertRecord(objectModel.TableInfo.TableName, propertyValueSequence.PropertyModels, propertyValueSequence.PropertyValues);
		//				//			}
		//				//			else
		//				//			{
		//				//				PropertyValueSequence changedPropertyValues = simpleObject.GetChangedPropertyValues(this.NormalizeForWritingByDatastoreType);

		//				//				if (changedPropertyValues.Length > 0)
		//				//					this.LocalDatastore.UpdateRecord(objectModel.TableInfo.TableName, SimpleObject.StringPropertyKey, simpleObject.Key, changedPropertyValues.PropertyModels, changedPropertyValues.PropertyValues);
		//				//			}

		//				//			break;

		//				//		case TransactionRequestAction.Delete:

		//				//			this.LocalDatastore.DeleteRecord(objectModel.TableInfo.TableName, SimpleObject.StringPropertyKey, simpleObject.Key);
		//				//			break;
		//				//	}
		//				//}

		//				//uncompressedActionData = this.SerializeTransactionActions(this.CurrentTransactionActions);
		//				//this.CurrentTransactionLog.ActionData = this.CompressData(uncompressedActionData);

		//				transactionSucceed = true;

		//				// AcceptChanges
		//			}

		//			// Acknowledge or Rollback transaction
		//			if (transactionSucceed)
		//			{
		//				// Acknowledge transaction by accepting changes of the evolved SimpleObjects
		//				foreach (KeyValuePair<SimpleObject, TransactionRequestAction> item in this.ActiveTransaction.TransactionRequestsBySimpleObject)
		//				{
		//					TransactionRequestAction transactionAction = item.Value;
		//					SimpleObject simpleObject = item.Key;

		//					if (transactionAction == TransactionRequestAction.Save)
		//					{
		//						if (simpleObject.IsNew && this.WorkingMode == ObjectManagerWorkingMode.Server && requester != this) // Requester is this when transaction is rollback
		//						{
		//							this.RaiseNewObjectCreated(simpleObject, requester);
		//							this.OnNewObjectCreated(simpleObject, requester);

		//							if (simpleObject is GraphElement)
		//							{
		//								this.RaiseNewGraphElementCreated(simpleObject as GraphElement, requester);
		//								this.OnNewGraphElementCreated(simpleObject as GraphElement, requester);
		//								simpleObject.GraphElementIsCreated(simpleObject as GraphElement, requester);
		//							}
		//						}

		//						this.RaiseNewObjectCreated(simpleObject, requester);

		//						simpleObject.AcceptChanges();
		//						simpleObject.SetIsNew(false);
		//					}
		//					else //if (transactionAction == TransactionRequestAction.Delete)
		//					{
		//						simpleObject.IsDeleted = true;

		//						if (requester != this) // Requester is this when transaction is rollback
		//						{
		//							this.OnAfterDelete(simpleObject, changeContainer, requester);
		//							this.RaiseAfterDelete(simpleObject, requester);
		//						}

		//						(simpleObject as IDisposable).Dispose();
		//						//System.GC.SuppressFinalize(simpleObject);
		//						simpleObject = null;
		//					}
		//				}

		//				this.ActiveTransaction.Status = TransactionStatus.Completed;
		//			}
		//			else
		//			{
		//				this.RollbackTransaction(this.ActiveTransaction, requester: this);
		//				this.ActiveTransaction.Status = TransactionStatus.Rollbacked;
		//			}

		//			if (this.WorkingMode == ObjectManagerWorkingMode.Server)
		//				this.ActiveTransaction.Save();

		//			this.RaiseTransactionFinished(this.ActiveTransaction, requester); //, this.transactionsActionsBySimpleObject.Values);

		//			if (this.WorkingMode == ObjectManagerWorkingMode.Client || this.DeleteTransactionLogIfTransactionSucceeded)
		//				this.ActiveTransaction.Delete();

		//			this.lastTransaction = this.ActiveTransaction;
		//		}

		//		this.ActiveTransaction = null;
		//		//this.oldTransactionsActionsBySimpleObject = new Dictionary<SimpleObject,TransactionAction>(this.transactionsActionsBySimpleObject);
		//		//this.transactionsActionsBySimpleObject.Clear();
		//	}
		//}
		public void RollbackTransaction(SystemTransaction transaction, object requester)
		{
			if (transaction.Status != TransactionStatus.Completed)
			{
				//if (this.WorkingMode != ObjectManagerWorkingMode.Client)
				//	transaction.CreateTransactionActionLogsFromActionData(this.GetObjectModel, this.GetSystemPropertySequence);

				this.RollbackTransactionInternal(transaction, requester);
				transaction.Status = TransactionStatus.Rollbacked;
			}
		}

		internal void RollbackTransactionInternal(SystemTransaction transaction, object requester)
		{
			lock (this.lockRollbackTransaction)
			{
				//byte[] uncompressedActionData = transaction.ActionData;
				//byte[] uncompressedActionData = this.DecompressData(transaction.ActionData);
				//IEnumerable<TransactionActionLog> transactionActions;

				//if (transaction.TransactionActions == null)
				//{
				//	if (this.WorkingMode != ObjectManagerWorkingMode.Client)
				//	{
				//		transaction.CreateTransactionActionLogsFromActionData(this.GetObjectModel, this.GetSystemPropertySequence);
				//	}
				//	else if (transaction.TransactionRequestsBySimpleObject != null)
				//	{
				//		transaction.CreateTransactionActionLogsFromTransactionRequests();
				//	}
				//	else
				//	{
				//		transaction.CreateTransactionActionLogsFromActionData(this.GetObjectModel, this.GetSystemPropertySequence);
				//	}
				//}

				//SimpleObject masterSimpleObject = null;
				//TransactionRequestAction masterRequestAction = default(TransactionRequestAction);
				List<SimpleObjectPropertyRelationArgs> objectRelationPropertiesToSet = new List<SimpleObjectPropertyRelationArgs>();
				List<SimpleObjectKeyArgs> objectsByOldTransactionKeys = new List<SimpleObjectKeyArgs>();
				List<SimpleObject> saveRequests = new List<SimpleObject>();
				List<SimpleObject> deleteRequests = new List<SimpleObject>();
				ChangeContainer nullChangeContainer = new ChangeContainer();
				Dictionary<SimpleObject, ChangeContainer?> changeContainersBySimpleObjects = new Dictionary<SimpleObject, ChangeContainer?>();

				foreach (TransactionActionInfo transactionActionInfo in transaction.RollbackTransactionActions)
				{
					SimpleObject? simpleObject = null;

					if (transactionActionInfo.ActionType == TransactionActionType.Insert)
					{
						simpleObject = this.GetObject(transactionActionInfo.TableId, transactionActionInfo.ObjectId);

						if (simpleObject == null)
							continue;

						simpleObject.ChangeContainer = nullChangeContainer;
						deleteRequests.Add(simpleObject);
					}
					else
					{
						int foreignTableId = 0;
						ISimpleObjectModel objectModel = (this.GetObjectModel(transactionActionInfo.TableId))!;

						if (transactionActionInfo.ActionType == TransactionActionType.Update)
						{
							simpleObject = this.GetObject(transactionActionInfo.TableId, transactionActionInfo.ObjectId);

							if (simpleObject == null)
								continue;

							changeContainersBySimpleObjects.Add(simpleObject, simpleObject.ChangeContainer);
							simpleObject.ChangeContainer = nullChangeContainer;
						}
						else // if (transactionAction.ActionType == TransactionActionType.Delete)
						{
							simpleObject = this.CreateNewEmptyObject(objectModel.ObjectType, isNew: true, objectId: 0, nullChangeContainer, requester: this);
							objectsByOldTransactionKeys.Add(new SimpleObjectKeyArgs(simpleObject, transactionActionInfo.TableId, transactionActionInfo.ObjectId));
							changeContainersBySimpleObjects.Add(simpleObject, null);
							simpleObject.ChangeContainer = nullChangeContainer;
						}

						if (transactionActionInfo.PropertyIndexValues != null)
						{
							for (int i = 0; i < transactionActionInfo.PropertyIndexValues.Count(); i++)
							{
								var item = transactionActionInfo.PropertyIndexValues.ElementAt(i);
								IPropertyModel propertyModel = objectModel.PropertyModels[item.PropertyIndex];

								if (propertyModel.IsKey)
									continue;

								object? propertyValue = item.PropertyValue;

								if (propertyModel.IsRelationTableId)
								{
									foreignTableId = (int)propertyValue!;
								}
								else if (propertyModel.IsRelationObjectId)
								{
									long foreignObjectId = (long)propertyValue!;
									IRelationModel? relationModel = this.GetRelationModel(propertyModel.RelationKey);

									if (relationModel is IOneToOneOrManyRelationModel oneToOneOrManyRelationModel)
										objectRelationPropertiesToSet.Add(new SimpleObjectPropertyRelationArgs(simpleObject, foreignTableId, foreignObjectId, oneToOneOrManyRelationModel));

									foreignTableId = 0;
								}
								else
								{
									propertyValue = this.NormalizeWhenReadingByPropertyType(propertyModel, propertyValue);
									simpleObject.SetPropertyValue(propertyModel, propertyValue, nullChangeContainer, requester: requester);
								}
							}
						}

						if (transactionActionInfo.ActionType == TransactionActionType.Delete) // The new object is created, raise events
							_ = simpleObject.Id; // Enforce new object Id creation and fire NewObjectCreated event.

						saveRequests.Add(simpleObject);
					}
				}

				// Sets relation foreign objects
				foreach (var item in objectRelationPropertiesToSet)
				{
					int foreignTableId = 0;
					long foreignObjectId = 0;

					// Try to find foreign object within old object keys (if was deleted and now is recreated with new Id)
					SimpleObjectKeyArgs oldKeyArgs = objectsByOldTransactionKeys.FirstOrDefault(x => x.TableId == item.ForeignTableId && x.ObjectId == item.ForeignObjectId);

					if (oldKeyArgs != null)
					{
						if (oldKeyArgs.SimpleObject != null)
						{
							foreignTableId = oldKeyArgs.TableId;
							foreignObjectId = oldKeyArgs.SimpleObject.Id;
						}
					}
					else
					{
						foreignTableId = item.ForeignTableId;
						foreignObjectId = item.ForeignObjectId;
					}

					SimpleObject? relatedSimpleObject = this.GetObject(foreignTableId, foreignObjectId);

					item.SimpleObject.SetRelationPrimaryObject(relatedSimpleObject, item.RelationModel, nullChangeContainer, requester);
				}

				//// Raise NewObjectCreated and GraphElementCreated events, if needed
				//foreach (var item in objectsByOldTransactionKeys)
				//{
				//	this.NewObjectIsCreated(item.SimpleObject, requester);

				//	if (item.SimpleObject is GraphElement)
				//		this.NewGraphElementIsCreated(item.SimpleObject as GraphElement, changeContainer, requester);
				//}

				saveRequests.Reverse();

				foreach (SimpleObject simpleObject in saveRequests)
				{
					nullChangeContainer.Set(simpleObject, TransactionRequestAction.Save, requester);

					//if (masterSimpleObject == null)
					//{
					//	masterSimpleObject = simpleObject;
					//	masterRequestAction = TransactionRequestAction.Save;
					//}
					//else
					//{
					//	masterSimpleObject.SetRelatedTransactionRequest(simpleObject, TransactionRequestAction.Save);
					//}
				}

				foreach (SimpleObject simpleObject in deleteRequests)
				{
					nullChangeContainer.Set(simpleObject, TransactionRequestAction.Delete, requester);

					//if (masterSimpleObject == null)
					//{
					//	masterSimpleObject = simpleObject;
					//	masterRequestAction = TransactionRequestAction.Delete;
					//}
					//else
					//{
					//	masterSimpleObject.SetRelatedTransactionRequest(simpleObject, TransactionRequestAction.Delete);
					//}
				}



				//if (masterSimpleObject != null)
				//{
				//	if (masterRequestAction == TransactionRequestAction.Save)
				//	{
				//		this.Save(masterSimpleObject, requester);
				//	}
				//	else //if (masterRequestAction == TransactionRequestAction.Delete)
				//	{
				//		this.Delete(masterSimpleObject, checkCanDelete: false, checkSimpleObjectGraphElements: true, requester: requester);
				//	}
				//}


				// TODO: commit changes without creating new transaction and saving it to the database
				// TODO: Validate SimpleObjects

				TransactionResult result = this.CommitChanges(nullChangeContainer, saveTransaction: false, requester);

				// Lets set original object's ChangeContainers
				foreach (var item in changeContainersBySimpleObjects)
				{
					SimpleObject simpleObject = item.Key;
					ChangeContainer? changeContainer = item.Value;

					simpleObject.ChangeContainer = changeContainer;
				}

				////uncompressedActionData = SerializeTransactionActions(transactionActions);
				////transaction.ActionData = uncompressedActionData; 
				//////transaction.ActionData = this.CompressData(uncompressedActionData);
				transaction.Status = TransactionStatus.Rollbacked;
			}
		}

		//private SimpleObject PrepeareTransactionActionForRollback(TransactionActionLog transactionAction)
		//{
		//	//int tableId = transactionAction.ObjectTableId;
		//	//Guid guid = transactionAction.ObjectGuid;

		//	SimpleObject simpleObject = null;

		//	//if (transactionAction.ActionType == TransactionActionType.Insert || transactionAction.ActionType == TransactionActionType.Delete)
		//	//{
		//	//	object objectIdObject;
		//	//	transactionAction.FieldData.TryGetValue(SimpleObjectModel.PropertyModel.ObjectId.Name, out objectIdObject);
		//	//	objectId = (long)objectIdObject;

		//	//	object creatorServerIdObject;
		//	//	transactionAction.FieldData.TryGetValue(SimpleObjectModel.PropertyModel.CreatorServerId.Name, out creatorServerIdObject);
		//	//	creatorServerId = (int)creatorServerIdObject;
		//	//}

		//	switch (transactionAction.ActionType)
		//	{
		//		case TransactionActionType.Insert:

		//			simpleObject = this.GetObject(transactionAction.TableId, transactionAction.ObjectId);

		//			//if (simpleObject != null)
		//			//	simpleObject.Delete();

		//			break;

		//		case TransactionActionType.Update:

		//			simpleObject = this.GetObject(transactionAction.TableId, transactionAction.ObjectId);

		//			if (simpleObject != null)
		//			{
		//				simpleObject.Load(transactionAction.PropertyValueSequence, readNormalizer: this.NormalizeWhenReadingByPropertyType, loadOldValuesAlso: false, reverseOrder: true);
		//				//simpleObject.Save();
		//			}

		//			break;

		//		case TransactionActionType.Delete:

		//			//ObjectCache objectCache = this.GetObjectCache(transactionAction.ObjectKey.GetTableId());
		//			//TODO: Create new SimpleObject

		//			ISimpleObjectModel objectModel = this.GetObjectModel(transactionAction.TableId);
		//			//ISystemPropertySequence systemPropertyIndexSequence = this.GetSystemPropertySequence(transactionAction.PropertySequenceId);
		//			simpleObject = this.CreateNewEmptyObject(objectModel.ObjectType, isNew: true);
		//			//simpleObject.Load(systemPropertyIndexSequence.PropertyIndexes, transactionAction.PropertyValueSequence.PropertyValues, readNormalizer: this.NormalizeWhenReadingByPropertyType, loadOldValuesAlso: false);
		//			simpleObject.Load(transactionAction.PropertyValueSequence, readNormalizer: this.NormalizeWhenReadingByPropertyType, loadOldValuesAlso: false, reverseOrder: true);
		//			//simpleObject.Save();
		//			//simpleObject = objectCache.GetObject(transactionAction.ObjectKey);

		//			//if (simpleObject == null)
		//			//{

		//			//	simpleObject = objectCache.CreateObjectFromPropertyValueData(systemPropertyIndexSequence.PropertyIndexSequence, transactionAction.PropertyValues, getValueData: this.NormalizeWhenReadingByDatastoreType, loadOldValuesAlso: false);
		//			//	simpleObject.Save();
		//			//}

		//			break;

		//		default:
		//			throw new NotSupportedException("TransactionActionType " + transactionAction.ActionType.ToString() + " is not supported in method RollbackTransactionAction");
		//	}

		//	return simpleObject;
		//}


		//private void CreateTransactionElementPropertyValuesFromFieldData(TransactionElement transactionElement, IDictionary<string, object> fieldData, Func<TransactionElementPropertyValue> createNewTransactionElementPropertyValue)
		//{
		//    foreach (var dictionaryItem in fieldData)
		//    {
		//        TransactionElementPropertyValue transactionElementPropertyValue = createNewTransactionElementPropertyValue();
		//        transactionElementPropertyValue.TransactionElement = transactionElement;
		//        transactionElementPropertyValue.PropertyName = dictionaryItem.Key;
		//        transactionElementPropertyValue.PropertyValue = dictionaryItem.Value;
		//        transactionElementPropertyValue.Save();
		//    }
		//}

		//private IDictionary<string, object> CreateFieldDataFromTransactionElementObjectPropertyValues(TransactionElement transactionElement)
		//{
		//    Dictionary<string, object> result = new Dictionary<string,object>();

		//    foreach (TransactionElementPropertyValue transactionElementPropertyValue in transactionElement.ObjectPropertyValues)
		//    {
		//        result.Add(transactionElementPropertyValue.PropertyName, transactionElementPropertyValue.PropertyValue);
		//    }

		//    return result;
		//}

		//private IDictionary<string, object> CreateFieldDataFromTransactionElementObjectOldPropertyValues(TransactionElement transactionElement)
		//{
		//    Dictionary<string, object> result = new Dictionary<string, object>();

		//    foreach (TransactionElementOldPropertyValue transactionElementOldPropertyValue in transactionElement.ObjectOldPropertyValues)
		//    {
		//        result.Add(transactionElementOldPropertyValue.PropertyName, transactionElementOldPropertyValue.PropertyValue);
		//    }

		//    return result;
		//}

		//private string SerializeDictionaryDataToXml(IDictionary<string, object> propertyValueData)
		//{
		//    string result = XmlHelpers.SerializeObject<SimpleDictionary<string, object>>(new SimpleDictionary<string, object>(propertyValueData));
		//    return result;
		//}

		//private IDictionary<string, object> DeserializeDictionaryDataFromXml(string xmlString)
		//{
		//    SimpleDictionary<string, object> result = XmlHelpers.DeserializeObject<SimpleDictionary<string, object>>(xmlString);
		//    return result;
		//}


#endregion |   Transaction Handling   |

		#region |   Object Property Value Serialization   |

		public static void WriteObjectPropertyValue(ref SequenceWriter writer, IServerPropertyInfo propertyModel, object? propertyValue, bool ifDefaultWriteOnlyBoolean = false)
		{
			WriteObjectPropertyValue(ref writer, propertyModel, propertyValue, propertyModel.PropertyTypeId, ifDefaultWriteOnlyBoolean);
		}

		public static void WriteObjectPropertyValue(ref SequenceWriter writer, IServerPropertyInfo propertyModel, object? propertyValue, int propertyTypeId, bool ifDefaultWriteOnlyBoolean)
		{
			WriteObjectPropertyValue(ref writer, propertyValue, propertyTypeId, propertyModel.IsSerializationOptimizable, propertyModel.DefaultValue, ifDefaultWriteOnlyBoolean);
		}

		public static void WriteObjectPropertyValue(ref SequenceWriter writer, object? propertyValue, int propertyTypeId, bool isSerializationOptimizable, object? defaultValue, bool ifDefaultWriteOnlyBoolean)
		{
			//if (isRelationKey)
			//{
			//	if (propertyTypeId == (int)ObjectTypes.TypeId.NullableGuid)
			//	{
			//		WriteNullableObjectKeyGuidOptimized(writer, propertyValue);

			//		return;
			//	}
			//	else if (propertyTypeId == (int)ObjectTypes.TypeId.Guid)
			//	{
			//		WriteObjectKeyGuidOptimized(writer, propertyValue);

			//		return;
			//	}
			//}

			if (ifDefaultWriteOnlyBoolean)
			{
				bool isValueDefault = (propertyValue == defaultValue); // if (propertyModel.PropertyType.IsValueType && propertyValue == propertyModel.DefaultValue)

				writer.WriteBoolean(isValueDefault);

				if (isValueDefault)
					return;
			}
			
			if (isSerializationOptimizable)
			{
				writer.WriteOptimized(propertyTypeId, propertyValue);
			}
			else
			{
				writer.Write(propertyTypeId, propertyValue);
			}

			//if (propertyModel.IsSerializationOptimizable)
			//{
			//	if ((propertyModel.IsKey || propertyModel.IsRelationKey) && (propertyModel.PropertyTypeId == ObjectTypes.TypeIdNullableGuid || 
			//																 propertyModel.PropertyTypeId == ObjectTypes.TypeIdGuid))
			//	{
			//		if (!propertyModel.PropertyType.IsValueType) // IsNullable
			//		{
			//			if (propertyValue == null)
			//			{
			//				writer.WriteBits(1, 1);
			//			}
			//			else
			//			{
			//				writer.WriteBits(0, 1);
			//			}
			//		}

			//		writer.WriteInt32Optimized(((Guid)propertyValue).GetTableId());
			//		writer.WriteInt64Optimized(((Guid)propertyValue).GetClientId());
			//		writer.WriteInt64Optimized(((Guid)propertyValue).GetObjectId());
			//	}
			//	else if (Comparison.IsEqual(propertyValue, propertyModel.DefaultValue))
			//	{
			//		writer.WriteBits(1, 1);
			//	}
			//	else
			//	{
			//		writer.WriteBits(0, 1);
			//		writer.WriteByTypeOptimized(propertyModel.PropertyTypeId, propertyValue);
			//	}
			//}
			//else
			//{
			//	writer.WriteByType(propertyModel.PropertyTypeId, propertyValue);
			//}
		}

		private void WriteObjectPropertyValues(ref SequenceWriter writer, ISimpleObjectModel objectModel, IDictionary<int, object> propertyValues, bool includePropertyIndex, bool ifDefaultWriteOnlyBoolean)
		{
			//int propertyCount = (propertyValues != null) ? propertyValues.Count : 0;
			//writer.WriteInt32Optimized(propertyCount);

			//if (propertyValues.Count > 0)
			//{
			foreach (var fieldDataItem in propertyValues)
			{
				int propertyIndex = fieldDataItem.Key;
				object? fieldValue = fieldDataItem.Value;
				IPropertyModel propertyModel = objectModel.PropertyModels[propertyIndex];

				if (includePropertyIndex)
					writer.WriteInt32Optimized(propertyIndex);

				object? propertyValue = NormalizeForWritingByPropertyType(propertyModel, fieldValue);

				//if (defaultOptimization)
				//{
				//	bool isDefault = (propertyValue == propertyModel.DefaultValue); // && propertyModel.PropertyType.IsValueType

				//	writer.WriteBoolean(isDefault);

				//	if (isDefault)
				//		continue;
				//}

				WriteObjectPropertyValue(ref writer, propertyModel, propertyValue, ifDefaultWriteOnlyBoolean);
			}
			//}
		}

		//public static object ReadObjectPropertyValue(SerializationReader reader, IPropertyModel propertyModel)
		//{
		//	return ReadObjectPropertyValue(reader, propertyModel, defaultOptimization: false);
		//}

		public static object? ReadObjectPropertyValue(ref SequenceReader reader, IServerPropertyInfo propertyModel, bool checkIfPropertyValueIsDefault = false)
		{
			return ReadObjectPropertyValue(ref reader, propertyModel, propertyModel.PropertyTypeId, checkIfPropertyValueIsDefault);
		}

		public static object? ReadObjectPropertyValue(ref SequenceReader reader, IServerPropertyInfo propertyModel, int writtenPropertyTypeId, bool checkIfPropertyValueIsDefault)
		{
			return ReadObjectPropertyValue(ref reader, propertyModel.PropertyIndex, writtenPropertyTypeId, propertyModel.IsSerializationOptimizable, propertyModel.DefaultValue, checkIfPropertyValueIsDefault);
		}

		public static object? ReadObjectPropertyValue(ref SequenceReader reader, int propertyIndex, int writtenPropertyTypeId, bool isSerializationOptimizable, object? defaultValue, bool checkIfPropertyValueIsDefault)
		{
			object? result = null;

			//if (writtenPropertyTypeId == (int)ObjectTypes.TypeId.NullableGuid && (isRelationKey || propertyIndex == SimpleObject.IndexPropertyGuid))
			//{
			//	result = ReadNullableObjectKeyGuidOptimized(reader);
			//}
			//else if (writtenPropertyTypeId == (int)ObjectTypes.TypeId.Guid && (isRelationKey || propertyIndex == SimpleObject.IndexPropertyGuid))
			//{
			//	result = ReadObjectKeyGuidOptimized(reader);
			//}

			if (checkIfPropertyValueIsDefault && reader.ReadBoolean()) // propertyModel.PropertyType.IsValueType
			{
				result = defaultValue;
			}
			else if (isSerializationOptimizable)
			{
				result = reader.ReadOptimized(writtenPropertyTypeId);
			}
			else
			{
				result = reader.Read(writtenPropertyTypeId);
			}

			//if (propertyModel.PropertyTypeId != writtenPropertyTypeId)
			//	result = Conversion.TryChangeType(result, propertyModel.PropertyType);

			return result;
		}

		//private IDictionary<int, object> ReadObjectPropertyValues(SerializationReader reader, ISimpleObjectModel objectModel)
		//{
		//	int count = reader.ReadInt32Optimized();
		//	Dictionary<int, object> result = new Dictionary<int, object>(count);

		//	for (int i = 0; i < count; i++)
		//	{
		//		int propertyIndex = reader.ReadInt32Optimized();  // reader.ReadString();
		//		IPropertyModel propertyModel = objectModel.PropertyModels[propertyIndex];
		//		object propertyValue = ReadObjectPropertyValue(reader, propertyModel);

		//		propertyValue = NormalizeWhenReadingByPropertyType(propertyModel, propertyValue);

		//		result.Add(propertyIndex, propertyValue);
		//	}

		//	return result;
		//}


		#endregion |   Object Property Value Serialization   |

		#region |   Object Property Value Normalization   |

		public object? NormalizeForClientServerSending(object? value, Type expectedType, bool encrypt)
		{
			object? result = value;

			if (value == null)
			{
				result = expectedType.GetDefaultValue();
			}
			else
			{
				if (encrypt)
				{
					result = PasswordSecurity.Encrypt(value.ToString(), this.Encryptor); //, this.CryptoBlockSize);

					if (result == null)
						return expectedType.GetDefaultValue();
				}

				//if (expectedType == typeof(Guid?))
				//{
				//	result = (Guid?)value;
				//}
				//else 
				if (value.GetType() != expectedType)
				{
					////Type nullableType = Nullable.GetUnderlyingType(result.GetType());
					//if (expectedType.IsGenericType && expectedType.GetGenericTypeDefinition() == typeof(Nullable<>)) // IsNullable
					//{
					//	var result2 = Activator.CreateInstance(expectedType, result);
					//}
					//else
					//{
					result = Conversion.TryChangeType(value, expectedType);
					//}
				}
			}

			return result;

		}

		public object? NormalizeWhenReadingByPropertyType(IServerPropertyInfo propertyModel, object? propertyValue)
		{
			return NormalizeWhenReading(propertyModel, propertyValue, propertyModel.PropertyTypeId);
		}

		public object? NormalizeForWritingByPropertyType(IServerPropertyInfo propertyModel, object? propertyValue)
		{
			return NormalizeForWriting(propertyModel, propertyValue, propertyModel.PropertyTypeId);
		}

		public object? NormalizeWhenReadingFromDatastore(IServerPropertyInfo propertyModel, object? propertyValue)
		{
			if (propertyModel.IsRelationTableId && (propertyValue == null || Convert.IsDBNull(propertyValue)))
				return default(int);

			if (propertyModel.IsRelationObjectId && (propertyValue == null || Convert.IsDBNull(propertyValue)))
				return default(long);

			return NormalizeWhenReadingByPropertyType(propertyModel, propertyValue);
		}

		//protected internal object NormalizeWhenReadingByDatastoreType(IPropertyModel propertyModel, object datastoreFieldValue)
		//{
		//	return NormalizeFromReader(propertyModel, datastoreFieldValue, propertyModel.DatastoreType);
		//}

		protected internal object? NormalizeForWritingToDatastore(IServerPropertyInfo propertyModel, object? propertyValue)
		{
			//if ((propertyModel.IsRelationTableId) && (int)propertyValue == 0)
			//	return null;                                  //< -This will be set at SqlProviderBase

			//if (propertyModel.IsRelationObjectId && (long)propertyValue == 0)
			//	return null;                                  //< -This will be set at SqlProviderBase

			return NormalizeForWritingByDatastoreType(propertyModel, propertyValue);
		}

		protected internal object? NormalizeForWritingByDatastoreType(IServerPropertyInfo propertyModel, object? propertyValue)
		{
			return NormalizeForWriting(propertyModel, propertyValue, propertyModel.DatastoreTypeId);
		}

		private object? NormalizeWhenReading(IServerPropertyInfo propertyModel, object? readerValue, int expectedPropertyTypeId)
		{
			return GetNormalizedValue(propertyModel, readerValue, expectedPropertyTypeId, (encryptedText) => PasswordSecurity.Decrypt(encryptedText, this.Decryptor)); //, this.CryptoBlockSize));
		}

		public static object? NormalizeWhenReadingByPropertyTypeWithoutDecryption(IServerPropertyInfo propertyModel, object? readerValue)
		{
			return GetNormalizedValue(propertyModel, readerValue, propertyModel.PropertyTypeId, (encryptedText) => encryptedText);
		}

		private object? NormalizeForWriting(IServerPropertyInfo propertyModel, object? value, int expectedPropertyTypeId)
		{
			return GetNormalizedValue(propertyModel, value, expectedPropertyTypeId, (clearText) => PasswordSecurity.Encrypt(clearText, this.Encryptor)); //, this.CryptoBlockSize));
		}

		private static object? GetNormalizedValue(IServerPropertyInfo propertyModel, object? value, int expectedPropertyTypeId, Func<string, string> encryptMethod)
		{
			object? result = value;
			Type expectedType = PropertyTypes.GetPropertyType(expectedPropertyTypeId);

			if (value == DBNull.Value || value == null)
			{
				result = expectedType.GetDefaultValue();
			}
			else
			{
				//Type valueType = value.GetType();
				if (propertyModel.IsEncrypted)
				{
					result = encryptMethod(value.ToString());

					//if (result == null)
					//	return expectedType.GetDefaultValue();
				}

				//if (expectedType == typeof(Guid?))
				//{
				//	result = (Guid?)value;
				//}
				//else 
				if (value.GetType() != expectedType)
				{
					////Type nullableType = Nullable.GetUnderlyingType(result.GetType());
					//if (expectedType.IsGenericType && expectedType.GetGenericTypeDefinition() == typeof(Nullable<>)) // IsNullable
					//{
					//	var result2 = Activator.CreateInstance(expectedType, result);
					//}
					//else
					//{
					result = Conversion.TryChangeType(value, expectedType);
					//}
				}
			}

			return result;
		}

		#endregion |   Object Property Value Normalization   |

		#region |   Protected Internal Methods   |

		//protected internal RelationPolicyModelBase GetRelationPolicyModel()
		//{
		//	return this.relationPolicyModel;
		//}

		protected internal virtual void GroupMembershipIsJoined(GroupMembership groupMembership, ChangeContainer changeContainer, object? requester) 
		{
			this.RaiseGroupMembershipJoined(groupMembership, requester);
			this.OnGroupMembershipJoin(groupMembership, changeContainer, requester);
		}

		protected internal virtual void GroupMembershipIsDisjoined(GroupMembership groupMembership, ChangeContainer changeContainer, object? requester)
		{
			this.RaiseGroupMembershipDisjoined(groupMembership, requester);
			this.OnGroupMembershipDisjoin(groupMembership, changeContainer, requester);
		}

		protected internal virtual void NewGraphElementIsCreated(GraphElement graphElement, ChangeContainer changeContainer, object? requester)
		{
			GraphElement parent = graphElement.Parent;

			// Check if needed to add in null collection only for manualy creating GraphElement, not using GraphElement constructor that take parent value and not setting Parent property manualy
			// (e.g. var ge = new GraphElement(objectManager)
			//       ge.GraphKey = 5
			//       ge.SimpleObject = simpleObject
			//       //ge.Parent = null; <- if you not set this, the GraphElement wouldn't be added to the Graph.RootGraphElements null collection!!! So This code correct such a cases.
			//       ...
			if (parent == null)
			{
				SimpleObjectCollection nullCollection = graphElement.GetOneToManyForeignNullCollectionInternal(RelationPolicyModelBase.OneToManyGraphElementToParentGraphElement.RelationKey);

				if (nullCollection != null && !nullCollection.Contains(graphElement)) // If collection initializes, its already contains this
					nullCollection.Add(graphElement);
			}

			//if (changeContainer != null)
			//	changeContainer.Set(graphElement,TransactionRequestAction.Save, requester);

			//if (this.WorkingMode != ObjectManagerWorkingMode.Server)
			//{
			graphElement.SimpleObject.NewGraphElementIsCreated(graphElement, changeContainer, requester);
			
			this.RaiseNewGraphElementCreated(graphElement, requester);
			this.OnNewGraphElementCreated(graphElement, changeContainer, requester);

			graphElement.NewGraphElementCreatedEventRised = true;


			if (this.WorkingMode != ObjectManagerWorkingMode.Server)
				this.MoveGraphElementOnGraphElementCreationIfRequired(graphElement);

			if (graphElement.Parent != null)
				this.GraphElementParentIsChanged(graphElement, oldParent: null, changeContainer, requester);
			
			// TODO: Check this, why is this require????
			//if (parent == graphElement.Parent && parent != null) // If parent is not changed by this.MoveGraphElementOnGraphElementCreationIfRequired raise event GraphElementParentChange
			//	this.GraphElementParentIsChanged(graphElement, graphElement.Parent, parent, changeContainer, requester);

			//this.ActivateGraphObjectActions(graphElement);

			//}

			//if (graphElement.ParentId == 0)
			//	graphElement.Graph.RootGraphElementIsCreated(graphElement);
		}

		protected virtual void MoveGraphElementOnGraphElementCreationIfRequired(GraphElement graphElement)
		{
			//if (this.ValidateGraphPolicy(graphElement).Passed)
			//	return;

			//if (graphElement.SimpleObject is Folder) // Folder can be in the same level with other objects
			//{
			//	if (graphElement.Parent != null && graphElement.Parent.SimpleObject.GetType() != typeof(Folder))
			//	{
			//		GraphElement parentNonFolderGraphElement = graphElement.Parent;
			//		graphElement.Parent = parentNonFolderGraphElement.Parent;
			//		parentNonFolderGraphElement.Parent = graphElement;

			//		return;
			//	}
			//}

			// If new graph element has model priority greather than its neighbors (has same parent), replace neighbors to be its chailds.
			IGraphPolicyModel? graphPolicyModel = this.ModelDiscovery.GetGraphPolicyModel(graphElement.GraphKey);

			if (graphPolicyModel == null)
				return;

			IGraphPolicyModelElement? graphElementPolicyModel = graphPolicyModel.GetGraphPolicyModelElement(graphElement.SimpleObject!.GetType());

			if (graphElementPolicyModel == null)
				return;

			lock (this.lockObject)
			{
				//List<GraphElement> neighborGraphElements = new List<GraphElement>();

				//foreach (GraphElement neighborGraphElement in graphElement.ParentGraphElements)
				//{
				//	if (neighborGraphElement != graphElement)
				//	{
				//		neighborGraphElements.Add(neighborGraphElement);
				//	}
				//}

				foreach (GraphElement neighbourGraphElement in graphElement.GetNeighbours().ToArray())
				{
					if (neighbourGraphElement == graphElement)
						continue;

					IGraphPolicyModelElement? neighborGraphPolicyModelElement = graphPolicyModel.GetGraphPolicyModelElement(neighbourGraphElement.SimpleObject!.GetType());

					if (neighborGraphPolicyModelElement != null)
					{
						if (neighborGraphPolicyModelElement.ParentAcceptableTypes.Count() > 0 || neighborGraphPolicyModelElement.ParentAcceptablePriorities.Count() > 0)
						{
							bool parentTypeAccepted = false;
							bool parentPriorityAccepted = false;

							if (neighborGraphPolicyModelElement.ParentAcceptableTypes.Contains(graphElementPolicyModel.ObjectType))
								parentTypeAccepted = true;

							if (neighborGraphPolicyModelElement.ParentAcceptablePriorities.Contains(graphElementPolicyModel.Priority))
								parentPriorityAccepted = true;

							if (!(parentTypeAccepted || parentPriorityAccepted)) // if any of this two roules allows parent, do not move
								continue;
						}
						else if (neighborGraphPolicyModelElement.Priority <= graphElementPolicyModel.Priority)
						{
							continue;
						}
					}

					neighbourGraphElement.Parent = graphElement;
				}
			}
		}

		protected internal virtual void BeforeGraphElementParentIsChanged(GraphElement graphElement, GraphElement newParent, ref bool cancel, object requester)
		{
			//if (graphElement.internalState != SimpleObjectInternalState.Normal)
			//	return;

			this.OnBeforeGraphElementParentChange(graphElement, newParent, ref cancel, requester);

			if (cancel)
				return;

			// TODO: Send parent chnge notification for Graph check if new orl old parent is null to maintain Graph.GraphElements collection

			this.RaiseBeforeGraphElementParentChange(graphElement, newParent, ref cancel, requester);
		}

		protected internal virtual void GraphElementParentIsChanged(GraphElement graphElement, GraphElement? oldParent, ChangeContainer changeContainer, object? requester)
		{
			//if (graphElement.internalState != SimpleObjectInternalState.Normal)
			//	return;

			this.OnGraphElementParentChange(graphElement, oldParent, requester);
			//graphElement.Graph.GraphElementParentIsChanged(graphElement, oldParent, newParent, requester);

			if (graphElement.SimpleObject != null)
				graphElement.SimpleObject.GraphElementParentIsChanged(graphElement, oldParent, changeContainer, requester);
			//this.ActivateGraphObjectActions(graphElement);

			this.RaiseGraphElementParentChange(graphElement, oldParent, requester);
		}

		protected internal virtual void StatusIsChanged(SimpleObject simpleObject, int status, int oldStatus)
		{
			simpleObject.RecalcImageName();
			this.OnStatusChange(simpleObject, status, oldStatus);
		}

		protected internal virtual void OrderIndexIsChanged(SortableSimpleObject sortableSimpleObject, int orderIndex, int oldOrderIndex, object? requester)
		{
			this.OnOrderIndexChange(sortableSimpleObject, orderIndex, oldOrderIndex, requester);
			this.RaiseOrderIndexChange(sortableSimpleObject, orderIndex, oldOrderIndex, requester);
		}

		protected internal virtual void RelationForeignObjectIsSet(SimpleObject simpleObject, SimpleObject? foreignSimpleObject, SimpleObject? oldForeignSimpleObject, IOneToOneOrManyRelationModel objectRelationModel, ChangeContainer? changeContainer, object requester)
		{
			//if (simpleObject is GraphElement || foreignSimpleObject is GraphElement)
			//	return;

			if (changeContainer != null)
				changeContainer.RelationForeignObjectIsSet(simpleObject, foreignSimpleObject, objectRelationModel);

			this.RaiseRelationForeignObjectSet(simpleObject, foreignSimpleObject, oldForeignSimpleObject, objectRelationModel, requester);

			//simpleObject.RelationForeignObjectIsSet(foreignSimpleObject, oldForeignSimpleObject, objectRelationModel, objectRelationType, requester);

			//foreach (SimpleObjectDependencyAction objectDependencyAction in this.ModelDiscovery.ObjectDependencyActions)
			//	if (objectDependencyAction.Match(simpleObject))
			//		objectDependencyAction.OnRelationForeignObjectSet(simpleObject, foreignSimpleObject, oldForeignSimpleObject, objectRelationModel, objectRelationType);
		}

		//protected internal new void SetObjectModelDefinition(ISimpleObjectModel objectModel, Type objectType)
		//{
		//	base.SetObjectModelDefinition(objectModel, objectType);
		//}

		#endregion |   Protected Internal Methods   |

		#region |   Protected Abstract Methods   |

		protected abstract Type GetGraphKeyEnumType();
		protected abstract Type GetManyToManyRelationKeyEnumType();
		protected abstract int GetUserModelTableId();
		//protected abstract IUser GetUser(string username);
		//protected abstract IUser GetSystemAdmin();

		#endregion |   Protected Abstract Methods   |

		#region |   Protected Methods   |

		protected virtual void OnPropertyValueChange(SimpleObject simpleObject, IPropertyModel propertyModel, object value, object oldValue, bool isChanged, bool isSaveable, ChangeContainer? changeContainer, object? requester)
		{
			//foreach (SimpleObjectDependencyAction objectDependencyAction in this.ModelDiscovery.ObjectDependencyActions)
			//    if (objectDependencyAction.Match(simpleObject))
			//        objectDependencyAction.OnPropertyValueChange(simpleObject, propertyIndex, value, oldValue);
		}

		protected virtual void OnSaveablePropertyValueChange(SimpleObject simpleObject, IPropertyModel propertyModel, object value, object oldValue, bool isChanged, ChangeContainer? changeContainer, object? requester) { }


		protected virtual string? GetImageName(SimpleObject simpleObject)
		{
			string? imageName = null;

			if (simpleObject is GraphElement graphElement)
			{
				if (graphElement.SimpleObject != null)
					imageName = graphElement.SimpleObject.GetImageName();
			}
			else
			{
				ISimpleObjectModel objectModel = simpleObject.GetModel();

				imageName = objectModel.ImageName;

				if (objectModel.ObjectSubTypePropertyModel != null) // ObjectSubTypes.Count > 0)
				{
					int objectSubType = simpleObject.GetPropertyValue<int>(objectModel.ObjectSubTypePropertyModel);
					IModelElement subTypeModel;

					if (objectModel.ObjectSubTypes.TryGetValue(objectSubType, out subTypeModel))
						imageName = subTypeModel.ImageName;
				}
			}

			return imageName;
		}

		//protected virtual void RecalculateImageName(SimpleObject simpleObject)
		//{
		//	this.RecalculateImageName(simpleObject, recalculateGraphNames: true);
		//}

		//protected virtual void RecalculateImageName(SimpleObject simpleObject, bool recalculateGraphNames)
		//{
		//	this.RecalcImageName(simpleObject);

		//	if (recalculateGraphNames && !(simpleObject is GraphElement))
		//		foreach (GraphElement graphElement in simpleObject.GraphElements)
		//			this.RecalcImageName(graphElement); // Enforce re-calculation of the image name, fire event and and save it, if require.
		//}

		//protected void CreteSimpleObjectRelationModel(BusinessApplicationRelationModel businessApplicationRelationModel)
		//{
		//    this.businessApplicationRelationModel = businessApplicationRelationModel;
		//    this.simpleObjectRelationModelByObjectType.Clear();

		//    foreach (OneToOneRelationModel oneToOneRelationModel in this.businessApplicationRelationModel.OneToOneRelations)
		//    {
		//        SimpleObjectRelationModel keyHolderObjectTypeModel = this.GetObjectRelationModelDefinition(oneToOneRelationModel.KeyHolderObjectType);
		//        keyHolderObjectTypeModel.OneToOneRelationsWithKeyHolderObject.Add(oneToOneRelationModel.Key, oneToOneRelationModel);

		//        SimpleObjectRelationModel relatedObjectTypeModel = this.GetObjectRelationModelDefinition(oneToOneRelationModel.RelatedObjectType);
		//        relatedObjectTypeModel.OneToOneRelationsWithRelatedObject.Add(oneToOneRelationModel.Key, oneToOneRelationModel);
		//    }

		//    foreach (OneToManyRelationModel oneToManyRelationModel in this.businessApplicationRelationModel.OneToManyRelations)
		//    {
		//        SimpleObjectRelationModel keyHolderOneObjectTypeModel = this.GetObjectRelationModelDefinition(oneToManyRelationModel.KeyHolderOneObjectType);
		//        keyHolderOneObjectTypeModel.OneToManyRelationsWithKeyHolderOneObject.Add(oneToManyRelationModel.Key, oneToManyRelationModel);

		//        SimpleObjectRelationModel relatedManyObjectTypeModel = this.GetObjectRelationModelDefinition(oneToManyRelationModel.RelatedManyObjectType);
		//        relatedManyObjectTypeModel.OneToManyRelationsWithRelatedManyObject.Add(oneToManyRelationModel.Key, oneToManyRelationModel);
		//    }

		//    foreach (ManyToManyRelationModel manyToManyRelationModel in this.businessApplicationRelationModel.ManyToManyRelations)
		//    {
		//        SimpleObjectRelationModel firstObjectTypeModel = this.GetObjectRelationModelDefinition(manyToManyRelationModel.FirstObjectType);
		//        firstObjectTypeModel.ManyToManyRelationsWithFirstObjectType.Add(manyToManyRelationModel.Key, manyToManyRelationModel);

		//        SimpleObjectRelationModel secondObjectTypeModel = this.GetObjectRelationModelDefinition(manyToManyRelationModel.SecondObjectType);
		//        secondObjectTypeModel.ManyToManyRelationsWithSecondObjectType.Add(manyToManyRelationModel.Key, manyToManyRelationModel);
		//    }
		//}


		//protected internal ObjectRelationModel GetObjectRelationModelDefinition(Type objectType)
		//{
		//	ObjectRelationModel value = this.simpleObjectRelationModelsByObjectType[objectType] as ObjectRelationModel;

		//	if (value == null)
		//	{
		//		value = this.CreateSimpleObjectRelationModel(this.RelationPolicyModel, objectType);
		//		this.simpleObjectRelationModelsByObjectType.Add(objectType, value);
		//	}

		//	return value;
		//}

		//protected ObjectRelationModel CreateSimpleObjectRelationModel(RelationPolicyModelBase applicationRelationModel, Type objectType)
		//{
		//	ObjectRelationModel value = new ObjectRelationModel(objectType);

		//	foreach (OneToOneRelationModel oneToOneRelationModel in applicationRelationModel.OneToOneRelations.Values)
		//	{
		//		if (oneToOneRelationModel.ObjectType == objectType || objectType.IsSubclassOf(oneToOneRelationModel.ObjectType))
		//		{
		//			value.OneToOneRelationsForeignKeyObject.Add(oneToOneRelationModel.RelationKey, oneToOneRelationModel);
		//		}

		//		if (oneToOneRelationModel.ForeignObjectType == objectType || objectType.IsSubclassOf(oneToOneRelationModel.ForeignObjectType))
		//		{
		//			value.OneToOneRelationsKeyHolderObject.Add(oneToOneRelationModel.RelationKey, oneToOneRelationModel);
		//		}
		//	}

		//	foreach (OneToManyRelationModel oneToManyRelationModel in applicationRelationModel.OneToManyRelations.Values)
		//	{
		//		if (oneToManyRelationModel.ObjectType == objectType || objectType.IsSubclassOf(oneToManyRelationModel.ObjectType))
		//		{
		//			value.OneToManyRelationsForeignKeyObject.Add(oneToManyRelationModel.RelationKey, oneToManyRelationModel);
		//		}

		//		if (oneToManyRelationModel.ForeignObjectType == objectType || objectType.IsSubclassOf(oneToManyRelationModel.ForeignObjectType))
		//		{
		//			value.OneToManyRelationsKeyHolderObject.Add(oneToManyRelationModel.RelationKey, oneToManyRelationModel);
		//		}
		//	}

		//	foreach (ManyToManyRelationModel manyToManyRelationModel in applicationRelationModel.ManyToManyRelations.Values)
		//	{
		//		if (manyToManyRelationModel.FirstObjectType == objectType || objectType.IsSubclassOf(manyToManyRelationModel.FirstObjectType))
		//		{
		//			value.ManyToManyRelationsFirstObjectType.Add(manyToManyRelationModel.RelationKey, manyToManyRelationModel);
		//		}

		//		if (manyToManyRelationModel.SecondObjectType == objectType || objectType.IsSubclassOf(manyToManyRelationModel.SecondObjectType))
		//		{
		//			value.ManyToManyRelationsSecondObjectType.Add(manyToManyRelationModel.RelationKey, manyToManyRelationModel);
		//		}
		//	}

		//	return value;
		//}

		//protected SingleObjectTypeCache GetSingleObjectTypeCache(int tableId)
		//{
		//    return this.ObjectCache.GetSingleObjectTypeCache(tableId);
		//}

		//protected override void OnObjectCreated(object requester, SimpleObject simpleObject)
		//{
		//    ObjectTypeCache objectTypeCache = this.GetObjectTypeCache(simpleObject as SimpleObject);
		//    objectTypeCache.ObjectRequireNegativeID(simpleObject as SimpleObject);

		//    base.OnObjectCreated(requester, simpleObject);
		//}

		//protected override void OnLoad(object requester, SimpleObject simpleObject)
		//{
		//    base.OnLoad(requester, simpleObject);
		//}

		protected virtual void OnGroupMembershipJoin(GroupMembership groupMembership, ChangeContainer changeContainer, object? requester) { }
		protected virtual void OnGroupMembershipDisjoin(GroupMembership groupMembership, ChangeContainer changeContainer, object? requester) { }

		protected virtual void OnNewGraphElementCreated(GraphElement graphElement, ChangeContainer changeContainer, object? requester) 
		{
			if (graphElement.SimpleObject != null)
				graphElement.Status = graphElement.SimpleObject.Status;
		}
		//{
		//    //foreach (SimpleObjectDependencyAction objectDependencyAction in this.ModelDiscovery.ObjectDependencyActions)
		//    //    if (objectDependencyAction.Match(graphElement.SimpleObject))
		//    //        objectDependencyAction.OnGraphElementCreated(graphElement);
		//}

		protected virtual void OnBeforeGraphElementParentChange(GraphElement graphElement, GraphElement? newParent, ref bool cancel, object? requester)
        {
            //foreach (SimpleObjectDependencyAction objectDependencyAction in this.ModelDiscovery.ObjectDependencyActions)
            //    if (objectDependencyAction.Match(graphElement.SimpleObject))
            //        objectDependencyAction.OnBeforeGraphElementParentChange(graphElement, oldParent, newParent, ref cancel);
        }

        protected virtual void OnGraphElementParentChange(GraphElement graphElement, GraphElement? oldParent, object? requester)
        {
            // Recheck Status value for old and new parents;
            if (oldParent != null && oldParent.SimpleObject != null)
                oldParent.Status = Math.Max(oldParent.SimpleObject.Status, this.GetMaxChildGraphElementStatus(oldParent));

			if (graphElement.Parent != null)
                graphElement.Parent.Status = Math.Max((graphElement.Parent.SimpleObject) != null ? graphElement.Parent.SimpleObject.Status : 0, 
													  this.GetMaxChildGraphElementStatus(graphElement.Parent));
            
            //foreach (SimpleObjectDependencyAction objectDependencyAction in this.ModelDiscovery.ObjectDependencyActions)
            //    if (objectDependencyAction.Match(graphElement.SimpleObject))
            //        objectDependencyAction.OnGraphElementParentChange(graphElement, oldParent, newParent);
        }

		protected virtual void OnStatusChange(SimpleObject simpleObject, int status, int oldStatus)
		{
			if (simpleObject is GraphElement graphElement)
			{
				if (graphElement.Parent != null)
					this.OnChildGraphElementStatusChange(graphElement.Parent, graphElement, status, oldStatus);
			}
			else //if (!(simpleObject is SystemSimpleObject)) // SimpleObject is SimpleObject and is not SystemObject
			{
				// Propagate Status through the its GraphElements
				foreach (GraphElement item in simpleObject.GraphElements) //simpleObject.GraphElements.ForEach(ge => ge.Status = status);
				{
					int maxChildStatus = this.GetMaxChildGraphElementStatus(item);

					item.Status = Math.Max(status, maxChildStatus);
				}
			}

			//if (this.multipleStatusChangesInProgress <= 0)
		}

		protected virtual void OnChildGraphElementStatusChange(GraphElement parentGraphElement, GraphElement childGraphElement, int childGraphElementStatus, int childGraphElementOldStatus)
		{
			if (childGraphElementStatus > childGraphElementOldStatus)
			{
				parentGraphElement.Status = Math.Max(parentGraphElement.Status, childGraphElementStatus);
			}
			else if (parentGraphElement.SimpleObject != null) // status is lowered and check if parent needs to lower status also, or it has another node with higher status.
			{
				parentGraphElement.Status = Math.Max(parentGraphElement.SimpleObject.Status, this.GetMaxChildGraphElementStatus(parentGraphElement));
			}
		}

		protected virtual int GetMaxChildGraphElementStatus(GraphElement graphElement) //=> graphElement.GraphElements.Max(element => element.Status);
		{
			int maxChildStatus = 0;

			foreach (GraphElement childGraphElement in graphElement.GraphElements)
			{
				if (!childGraphElement.DeleteRequested)
				{
					int childElementStatus = childGraphElement.Status;

					if (childElementStatus > maxChildStatus)
						maxChildStatus = childElementStatus;
				}
			}

			return maxChildStatus;
		}

		protected virtual void OnObjectIdChange(SimpleObject simpleObject, long oldTempId, long newId) { }

		//protected override void OnPropertyValueChange(object requester, SimpleObject simpleObject, string propertyName, object value, object oldValue)
		//{
		//	// Propagate Status throguht object related graphs and graph's parents.
		//	if (propertyName == SimpleObjectModel.PropertyModel.Status.Name)
		//	{
		//		SimpleObject simpleObject = simpleObject as SimpleObject;
		//		int oldStatus = Conversion.TryChangeType<int>(value);

		//		this.OnStatusChange(simpleObject, simpleObject.Status, oldStatus);
		//	}
		//	else
		//	{
		//		base.OnPropertyValueChange(requester, simpleObject, propertyName, value, oldValue);
		//	}
		//}

		protected virtual void OnOrderIndexChange(SortableSimpleObject sortedSimpleObject, int orderIndex, int oldOrderIndex, object? requester)
		{
		}

		//protected virtual void OnAfterGraphElementDeleted(object requester, SimpleObject simpleObject)
		//{
		//    ISimpleObjectModel objectModel = simpleObject.GetModel();

		//    if (simpleObject.GraphElements.Count == 0)
		//    {
		//        if (objectModel.DeleteSimpleObjectOnLastGraphElementDelete)
		//        {
		//            simpleObject.Delete();
		//        }
		//    }
		//    else if (objectModel.DeleteAllGraphElementsOnOneGraphElementDelete)
		//    {
		//        while (simpleObject.GraphElements.Count > 0)
		//        {
		//            bool isLastOne = (simpleObject.GraphElements.Count == 1);

		//            this.DeleteInternal(requester, simpleObject.GraphElements[0]);

		//            if (isLastOne)
		//                break;
		//        }
		//    }
		//}


		#endregion |   Protected Methods   |

		#region |   Protected Raise Event Methods   |

		protected virtual void RaiseGroupMembershipJoined(GroupMembership groupMembership, object? requester) => this.GroupMembershipJoined?.Invoke(this, new GroupMembershipRequesterEventArgs(groupMembership, requester));
		protected virtual void RaiseGroupMembershipDisjoined(GroupMembership groupMembership, object? requester) => this.GroupMembershipDisjoined?.Invoke(this, new GroupMembershipRequesterEventArgs(groupMembership, requester));

		protected virtual void RaiseNewGraphElementCreated(GraphElement graphElement, object? requester)
        {
            this.NewGraphElementCreated?.Invoke(this, new GraphElementRequesterEventArgs(graphElement, requester));
        }

        protected virtual void RaiseBeforeGraphElementParentChange(GraphElement graphElement, GraphElement newParent, ref bool cancel, object? requester)
        {
			if (this.BeforeGraphElementParentChange != null)
			{
				BeforeChangeParentGraphElementRequesterEventArgs args = new BeforeChangeParentGraphElementRequesterEventArgs(graphElement, newParent, cancel, requester);
				this.BeforeGraphElementParentChange(this, args);

				cancel = args.Cancel;
			}
        }

        protected virtual void RaiseGraphElementParentChange(GraphElement graphElement, GraphElement? oldParent, object? requester)
        {
            this.GraphElementParentChange?.Invoke(this, new OldParentGraphElementRequesterEventArgs(graphElement, oldParent, requester));
        }

		protected virtual void RaiseRelationForeignObjectSet(SimpleObject simpleObject, SimpleObject? foreignSimpleObject, SimpleObject? oldForeignSimpleObject, IOneToOneOrManyRelationModel objectRelationPolicyModel, object? requester)
        {
            this.RelationForeignObjectSet?.Invoke(this, new RelationForeignObjectSetRequesterEventArgs(simpleObject, foreignSimpleObject, oldForeignSimpleObject, objectRelationPolicyModel, requester));
        }

		protected virtual void RaiseOrderIndexChange(SortableSimpleObject sortedSimpleObject, int orderIndex, int oldOrderIndex, object? requester) 
		{
			this.OrderIndexChange?.Invoke(this, new ChangeSortedSimpleObjectRequesterEventArgs(sortedSimpleObject, orderIndex, oldOrderIndex, requester));
		}

		protected virtual void RaiseTransactionStarted(SystemTransaction transaction, IEnumerable<DatastoreActionInfo>? datastoreActions, object? requester)
        {
			this.TransactionStarted?.Invoke(this, new TransactionDatastoreActionRequesterEventArgs(transaction, datastoreActions, requester));
        }

		protected virtual void RaiseTransactionFinished(SystemTransaction transaction, IEnumerable<DatastoreActionInfo> datastoreActions, object? requester) //, IEnumerable<TransactionAction> transactionActions)
        {
			this.TransactionFinished?.Invoke(this, new TransactionDatastoreActionRequesterEventArgs(transaction, datastoreActions, requester)); //, transactionActions));
        }

		//protected virtual void RaiseTransactionSucceeded(IEnumerable<TransactionActionInfo> transactionActions)
		//{
		//	this.TransactionSucceeded?.Invoke(this, new TransactionInfoEventArgs(transactionActions));
		//}

        protected virtual void RaiseLargeUpdateStarted()
        {
            this.UpdateStarted?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void RaiseLargeUpdateEnded()
        {
            this.UpdateEnded?.Invoke(this, EventArgs.Empty);
        }

		protected virtual void RaiseObjectIdChange(SimpleObject simpleObject, long oldTempId, long newId)
		{
			this.ObjectIdChange?.Invoke(this, new ChangeObjectIdSimpleObject(simpleObject, oldTempId, newId));
		}

		protected virtual void RaiseActiveEditorsPushData(SimpleObject simpleObject)
		{
			this.ActiveEditorsPushObjectData?.Invoke(this, new SimpleObjectEventArgs(simpleObject));
		}

		protected virtual void RaiseValidationInfo(SimpleObjectValidationResult validationResult)
		{
			this.ValidationInfo?.Invoke(this, new ValidationInfoEventArgs(validationResult));
		}

		#endregion |   Protected Raise Event Methods   |

		#region |   Protected Overriden Event Methods   |

		//protected override void RaiseObjectCreated(object requester, SimpleObject simpleObject)
		//{
		//	//if (simpleObject as SimpleObject is SystemSimpleObject)
		//	//	return;

		//	base.RaiseObjectCreated(requester, simpleObject);
		//}

		//protected override void RaiseBeforeLoading(object requester, SimpleObject simpleObject)
		//{
		//	if (simpleObject as SimpleObject is SystemSimpleObject)
		//		return;

		//	base.RaiseBeforeLoading(requester, simpleObject);
		//}

		//protected override void RaiseAfterLoad(object requester, SimpleObject simpleObject)
		//{
		//	if (simpleObject as SimpleObject is SystemSimpleObject)
		//		return;

		//	base.RaiseAfterLoad(requester, simpleObject);
		//}

		//protected override void RaiseBeforeSaving(object requester, SimpleObject simpleObject)
		//{
		//	if (simpleObject as SimpleObject is SystemSimpleObject)
		//		return;

		//	base.RaiseBeforeSaving(requester, simpleObject);
		//}

		//protected override void RaiseSaving(object requester, SimpleObject simpleObject)
		//{
		//	if (simpleObject as SimpleObject is SystemSimpleObject)
		//		return;

		//	base.RaiseSaving(requester, simpleObject);
		//}

		//protected override void RaiseAfterSave(object requester, SimpleObject simpleObject, bool isNewBeforeSaving)
		//{
		//	if (simpleObject as SimpleObject is SystemSimpleObject)
		//		return;

		//	base.RaiseAfterSave(requester, simpleObject, isNewBeforeSaving);
		//}

		//protected override void RaiseBeforeDeleting(object requester, SimpleObject simpleObject)
		//{
		//	if (simpleObject as SimpleObject is SystemSimpleObject)
		//		return;

		//	base.RaiseBeforeDeleting(requester, simpleObject);
		//}

		//protected override void RaiseAfterDelete(object requester, SimpleObject simpleObject)
		//{
		//	if (simpleObject as SimpleObject is SystemSimpleObject)
		//		return;

		//	base.RaiseAfterDelete(requester, simpleObject);
		//}

		//protected override void RaiseBeforePropertyValueChange(object requester, SimpleObject simpleObject, int propertyIndex, object value, object newValue)
		//{
		//	if (simpleObject as SimpleObject is SystemSimpleObject)
		//		return;

		//	base.RaiseBeforePropertyValueChange(requester, simpleObject, propertyIndex, value, newValue);
		//}

		//protected override void RaisePropertyValueChange(object requester, SimpleObject simpleObject, int propertyIndex, object value, object oldValue)
		//{
		//	if (simpleObject as SimpleObject is SystemSimpleObject)
		//		return;

		//	base.RaisePropertyValueChange(requester, simpleObject, propertyIndex, value, oldValue);
		//}

		//protected override void RaiseChangedPropertiesCountChange(SimpleObject simpleObject, int propertyNamesCount, int oldPropertyNamesCount)
		//{
		//	if (simpleObject as SimpleObject is SystemSimpleObject)
		//		return;

		//	base.RaiseChangedPropertiesCountChange(simpleObject, propertyNamesCount, oldPropertyNamesCount);
		//}

		//protected override void RaiseRequireSavingChange(SimpleObject simpleObject, bool requireSaving)
		//{
		//	if (simpleObject as SimpleObject is SystemSimpleObject)
		//		return;

		//	base.RaiseRequireSavingChange(simpleObject, requireSaving);
		//}

		#endregion |   Protected Overriden Event Methods   |

		#region |   Internal Methods   |

		internal void CacheGraphElementsWithObjectsFromServer(int graphKey, long parentGraphElementId, out List<long> graphElementIds) //, out bool[] hasChildrenInfo)
		{
			var graphElementsWithObjects = this.RemoteDatastore!.GetGraphElementsWithObjects(graphKey, parentGraphElementId).GetAwaiter().GetResult();
			long previousId = 0;
			//int orderIndex = 0;
			graphElementIds = new List<long>(graphElementsWithObjects.Length);
			//hasChildrenInfo = new bool[graphElementsWithObjects.Length];

			for (int i = 0; i < graphElementsWithObjects.Length; i++) // GraphElementObjectPair item in graphElementsWithObjects!)
			{
				GraphElementObjectPair item = graphElementsWithObjects[i];
				ObjectCache? simpleObjectCache = this.GetObjectCache(item.SimpleObjectTableId);
				//long graphElementId = ((long)item.GraphElementPropertyIndexValues.ElementAt(0).PropertyValue!)!;
				//long simpleObjectId = (item.SimpleObjectPropertyIndexValues.Count() > 0) ? ((long)item.SimpleObjectPropertyIndexValues.ElementAt(0).PropertyValue!)! : 0;
				GraphElement graphElement;

				if (!simpleObjectCache!.IsInCache(item.SimpleObjectId))
					_ = simpleObjectCache!.CreateAndLoadObject(item.SimpleObjectId, item.SimpleObjectPropertyIndexValues);

				if (!this.GraphElementObjectCache.IsInCache(item.GraphElementId))
				{
					graphElement = (this.GraphElementObjectCache.CreateAndLoadObject(item.GraphElementId, loadAction: ge =>	(ge as GraphElement)!.Load(previousId, parentGraphElementId, graphKey, item.SimpleObjectTableId, item.SimpleObjectId, orderIndex: i)) as GraphElement)!; // orderIndex++);


					//graphElement = (this.GraphElementObjectCache.CreateAndLoadObject(item.GraphElementPropertyIndexValues) as GraphElement)!; // real objectId will be loaded
					graphElement.HasChildrenInClientModeWhenNotLoaded = item.HasChildren;
				}

				previousId = item.GraphElementId;
				graphElementIds.Add(item.GraphElementId);
				//hasChildrenInfo[i] = item.HasChildren;
			}
		}

		//internal SimpleObjectCollection<T> GetOneToManyForeignObjectCollectionFromServer<T>(SimpleObject simpleObject, int relationKey)
		//	where T : SimpleObject
		//{
		//	var relationModel = this.ModelDiscovery.GetOneToManyRelationModel(relationKey);
		//	var objectIds = this.RemoteDatastore.GetOneToManyForeignObjectCollection(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id, relationKey).GetAwaiter().GetResult();

		//	return new SimpleObjectCollection<T>(this, relationModel!.ForeignObjectTableId, objectIds);
		//}

		internal protected GraphElement? GetSimpleObjectGraphElementByGraphKey(SimpleObject simpleObject, int graphKey)
		{
			if (this.WorkingMode == ObjectManagerWorkingMode.Client)
			{
				long graphElementId = this.RemoteDatastore.GetSimpleObjectGraphElementIdByGraphKey(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id, graphKey).GetAwaiter().GetResult();
				GraphElement? graphElement = this.GetObject(GraphElementModel.TableId, graphElementId) as GraphElement;

				return graphElement;
			}
			else
			{
				return simpleObject.GraphElements.FirstOrDefault(graphElement => graphElement.GraphKey == graphKey);
			}
		}

		internal string GetImageNameInternal(SimpleObject simpleObject) => this.GetImageName(simpleObject);

		internal IEnumerable<SystemTransaction> GetSystemTransactionsByUser(long userId)
		{
			return from SystemTransaction systemTransaction in this.systemTransactions
				   where systemTransaction.UserId == userId
				   select systemTransaction;
		}

		internal SystemGraph GetSystemGraph(int graphKey)
		{
			return this.systemGraphsByGraphKey[graphKey];
		}

		#endregion |   Internal Methods   |

		#region |   Private Methods   |

		private void SetServerBasedWorkingMode(LocalDatastore localDatastore)
		{

			this.localDatastore = localDatastore;
			this.datastore = this.localDatastore;
			this.remoteDatastore = null;

			if (this.localDatastore != null && this.localDatastore.Connected)
				this.InitializeLocalDatastore(); // InitializeGraphs and InitializeServerModelPropertySequences is included

			//this.InitializeObjectCache(this.ModelDiscovery);
		}

		private void ObjectIdIsChanged(SimpleObject simpleObject, long oldTempId, long newId) 
		{
			this.OnObjectIdChange(simpleObject, oldTempId, newId);
			simpleObject.ObjectIdIsChanged(oldTempId, newId);
			this.RaiseObjectIdChange(simpleObject, oldTempId, newId);
		}

		//private IDictionary<string, object> CreateFieldDataBasedOnObjectModelProperties(SimpleObject simpleObject, IDictionary<string, object> propertyValues, bool includeObjectKeyGuid)
		//{
		//	if (includeObjectKeyGuid)
		//		propertyValues.Add(SimpleObject.StringFieldGuid, simpleObject.Key.Guid);

		//	return propertyValues;

		//	Dictionary<string, object> result = new Dictionary<string, object>();
		//	ISimpleObjectModel objectModel = simpleObject.GetModel();

		//	foreach (var propertyValueItem in propertyValues)
		//	{
		//		string propertyName = propertyValueItem.Key;
		//		IPropertyModel propertyModel = objectModel.GetPropertyModel(propertyName);

		//		if (propertyModel != null && propertyModel.IsStorable)
		//		{
		//			object value;

		//			if (propertyValues.TryGetValue(propertyName, out value))
		//			{
		//				if (value != null && value.GetType() != propertyModel.PropertyType)
		//					value = Conversion.TryChangeType(value, propertyModel.PropertyType);
		//			}
		//			else
		//			{
		//				value = null;
		//			}

		//			result.Add(propertyName, value);
		//		}
		//		else if (includeObjectKeyGuid && (propertyName == SimpleObjectModel.PropertyModel.TableId.Name || propertyName == SimpleObjectModel.PropertyModel.ObjectId.Name || propertyName == SimpleObjectModel.PropertyModel.CreatorServerId.Name))
		//		{
		//			object value;

		//			if (!propertyValues.TryGetValue(propertyName, out value))
		//			{
		//				value = null;
		//			}

		//			result.Add(propertyName, value);
		//		}
		//	}

		//	return result;
		//}

		//private void ActivateGraphObjectActions(GraphElement graphElement)
		//{
		//	if (graphElement != null && graphElement.SimpleObject != null && !graphElement.SimpleObject.IsDeleteStarted)
		//	{
		//		foreach (GraphElementCreatedAction graphObjectAction in graphElement.SimpleObject.GetModel().GraphElementCreatedActions)
		//		{
		//			try
		//			{
		//				if (graphElement.GraphKey == graphObjectAction.GraphKey)
		//					graphObjectAction.Action(graphElement);
		//			}
		//			catch
		//			{
		//				// TODO: Create Manager error message event informer and send them message
		//			}
		//		}
		//	}
		//}


		//private void DefaultChangeContainer_TransactionCountChange(object sender, CountChangeEventArgs e)
		//{
		//	if (e.OldCount == 0)
		//	{
		//		this.RaiseRequireCommitChange(requireCommit: true);
		//	}
		//	else if (e.Count == 0)
		//	{
		//		this.RaiseRequireCommitChange(requireCommit: false);
		//	}
		//}

		#endregion |   Private Methods   |
	}

	#region |   Delegates   |

	public delegate void GroupMembershipRequesterEventHandler(object sender, GroupMembershipRequesterEventArgs e);
	public delegate void GraphElementEventHandler(object sender, GraphElementEventArgs e);
	public delegate void GraphElementRequesterEventHandler(object sender, GraphElementRequesterEventArgs e);
	public delegate void GraphElementChangePropertyValueSimpleObjectRequesterEventHandler(object sender, GraphElementChangePropertyValueSimpleObjectRequesterEventArgs e);
	public delegate void BeforeChangeParentGraphElementRequesterEventHandler(object sender, BeforeChangeParentGraphElementRequesterEventArgs e);
	public delegate void ChangeParentGraphElementRequesterEventHandler(object sender, OldParentGraphElementRequesterEventArgs e);

    //public delegate void SimpleObjectEventHandler(object sender, SimpleObjectEventArgs e);
	//public delegate void RequesterBindingObjectEventHandler(object sender, BindingObjectRequesterEventArgs e);
    public delegate void GraphValidationResultEventHandler(object sender, GraphValidationResultEventArgs e);
    public delegate void ChangePropertyValueRequesterBindingObjectEventHandler(object sender, ChangePropertyValueBindingObjectRequesterEventArgs e);
    public delegate void ImageNameChangeBindingObjectEventHandler(object sender, ImageNameChangeBindingObjectEventArgs e);
    public delegate void RelationForeignObjectSetEventHandler(object sender, RelationForeignObjectSetRequesterEventArgs e);
    public delegate void BindingObjectRelationForeignObjectSetEventHandler(object sender, BindingObjectRelationForeignObjectSetRequesterEventArgs e);
	public delegate void ChangeOrderIndexSimpleObjectRequesterEventHandler(object sender, ChangeSortedSimpleObjectRequesterEventArgs e);

    public delegate void TransactionRequesterEventHandler(object sender, TransactionRequesterEventArgs e);
	public delegate void TransactionDatastoreActionRequesterEventHandler(object sender, TransactionDatastoreActionRequesterEventArgs e);
	//public delegate void TransactionInfoEventHandler(object sender, TransactionInfoEventArgs e);
	public delegate void ValidationInfoEventHandler(object sender, ValidationInfoEventArgs e);
	public delegate void ChangeObjectIdSimpleObjectEventHandler(object sender, ChangeObjectIdSimpleObject e);
	//public delegate void TransactionActionsEventHandler(object sender, TransactionActionsEventArgs e);

	#endregion |   Delegates   |

	#region |   EventArgs Classes   |

	public class GroupMembershipRequesterEventArgs : RequesterEventArgs
	{
		public GroupMembershipRequesterEventArgs(GroupMembership groupMembership, object? requester)
			: base(requester)
		{
			this.GroupMembership = groupMembership;
		}

		public GroupMembership GroupMembership { get; private set; }
	}

	public class GraphElementEventArgs : EventArgs
	{
		public GraphElementEventArgs(GraphElement? graphElement)
		{
			this.GraphElement = graphElement;
		}

		public GraphElement? GraphElement { get; private set; }
	}

	public class NodeEventArgs : EventArgs
	{
		public NodeEventArgs(object node)
		{
			this.Node = node;
		}

		public object Node { get; private set; }
	}

	public class GraphElementNodeEventArgs : NodeEventArgs
	{
		public GraphElementNodeEventArgs(GraphElement graphElement, object node)
			: base(node)
		{
			this.GraphElement = graphElement;
		}

		public GraphElement GraphElement { get; private set; }
	}


	public class GraphElementRequesterEventArgs : RequesterEventArgs
    {
        public GraphElementRequesterEventArgs(GraphElement graphElement, object? requester)
            : base(requester)
        {
            this.GraphElement = graphElement;
        }

        public GraphElement GraphElement { get; private set; }
    }

	public class GraphElementChangePropertyValueSimpleObjectRequesterEventArgs : ChangePropertyValueSimpleObjectRequesterEventArgs
	{
		public GraphElementChangePropertyValueSimpleObjectRequesterEventArgs(GraphElement graphElement, object node, SimpleObject simpleObject, IPropertyModel? propertyModel, object? value, object? oldValue, bool isChanged, bool isSaveable, object? requester)
			: base(simpleObject, propertyModel, value, oldValue, isChanged, isSaveable, requester)
		{
			this.GraphElement = graphElement;
			this.Node = node;
		}

		public GraphElement GraphElement { get; private set; }
		public object Node { get; private set; }
    }

	public class BeforeChangeParentGraphElementRequesterEventArgs : NewParentGraphElementRequesterEventArgs
	{
		public BeforeChangeParentGraphElementRequesterEventArgs(GraphElement graphElement, GraphElement? newParent, bool cancel, object? requester)
			: base(graphElement, newParent, requester)
		{
			this.Cancel = cancel;
		}

		public bool Cancel { get; private set; }
	}

	public class NewParentGraphElementRequesterEventArgs : GraphElementRequesterEventArgs
	{
		public NewParentGraphElementRequesterEventArgs(GraphElement graphElement, GraphElement? newParent, object? requester)
			: base(graphElement, requester)
		{
			this.NewParent = newParent;
		}

		public GraphElement? NewParent { get; private set; }
	}

	public class OldParentGraphElementRequesterEventArgs : GraphElementRequesterEventArgs
    {
        public OldParentGraphElementRequesterEventArgs(GraphElement graphElement, GraphElement? oldParent, object? requester)
            : base(graphElement, requester)
        {
            this.OldParent = oldParent;
        }

        public GraphElement? OldParent { get; private set; }
    }

	public class SortableSimpleObjectRequesterEventArgs : RequesterEventArgs
	{
		public SortableSimpleObjectRequesterEventArgs(SortableSimpleObject sortableSimpleObject, object? requester)
			: base(requester)
		{
			this.SortableSimpleObject = sortableSimpleObject;
		}

		public SortableSimpleObject SortableSimpleObject { get; private set; }
	}

	//public class SimpleObjectEventArgs : EventArgs
	//{
	//	public SimpleObjectEventArgs(SimpleObject simpleObject)
	//	{
	//		this.SimpleObject = simpleObject;
	//	}

	//	public SimpleObject SimpleObject { get; private set; }
	//}

	//public class BindingObjectRequesterEventArgs : RequesterEventArgs
	//{
	//	public BindingObjectRequesterEventArgs(object requester, IBindingObject bindingObject)
	//		: base(requester)
	//	{
	//		this.BindingObject = bindingObject;
	//	}

	//	public IBindingObject BindingObject { get; private set; }
	//}

	public class ChangePropertyValueBindingObjectRequesterEventArgs : EventArgs
    {
        private ChangePropertyValueSimpleObjectRequesterEventArgs changedPropertyValueRequesterSimpleObjectEventArgs;

        public ChangePropertyValueBindingObjectRequesterEventArgs(ChangePropertyValueSimpleObjectRequesterEventArgs changedPropertyValueRequesterSimpleObjectEventArgs)
            //: base(requester, changedPropertyValueRequesterSimpleObjectEventArgs.SimpleObject) 
        {
            this.changedPropertyValueRequesterSimpleObjectEventArgs = changedPropertyValueRequesterSimpleObjectEventArgs;
        }

		public ChangePropertyValueBindingObjectRequesterEventArgs(SimpleObject bindingObject, IPropertyModel? propertyModel, object? value, object? oldValue, bool isChanged, bool isSeaveable, object? requester)
		{
			this.changedPropertyValueRequesterSimpleObjectEventArgs = new ChangePropertyValueSimpleObjectRequesterEventArgs(bindingObject, propertyModel, value, oldValue, isChanged, isSeaveable, requester);
		}

        public SimpleObject BindingObject
        {
            get { return this.changedPropertyValueRequesterSimpleObjectEventArgs.SimpleObject as SimpleObject; }
        }

        public IPropertyModel? PropertyModel
        {
            get { return this.changedPropertyValueRequesterSimpleObjectEventArgs.PropertyModel; }
        }

        public object? Value 
        {
            get { return this.changedPropertyValueRequesterSimpleObjectEventArgs.Value; }
        }
        
        public object? OldValue 
        {
            get { return this.changedPropertyValueRequesterSimpleObjectEventArgs.OldValue; }
        }

		public bool IsChanged
		{
			get { return this.changedPropertyValueRequesterSimpleObjectEventArgs.IsChanged; }
		}

		public bool IsSeavable
		{
			get { return this.changedPropertyValueRequesterSimpleObjectEventArgs.IsSaveable; }
		}

		public object? Requester
		{
			get { return this.changedPropertyValueRequesterSimpleObjectEventArgs.Requester; }
		}
	}

	public class ChangeSortedSimpleObjectRequesterEventArgs : SortableSimpleObjectRequesterEventArgs
	{
		public ChangeSortedSimpleObjectRequesterEventArgs(SortableSimpleObject sortedSimpleObject, int orderIndex, int oldOrderIndex, object? requester)
			: base(sortedSimpleObject, requester)
		{
			this.OrderIndex = orderIndex;
			this.OldOrderIndex = oldOrderIndex;
		}

		public int OldOrderIndex { get; private set; }
		public int OrderIndex { get; private set; }
	}

    public class ImageNameChangeBindingObjectEventArgs : EventArgs
    {
        private ImageNameChangeSimpleObjectEventArgs iconNameChangeSimpleObjectEventArgs;

        public ImageNameChangeBindingObjectEventArgs(ImageNameChangeSimpleObjectEventArgs iconNameChangeSimpleObjectEventArgs)
        {
            this.iconNameChangeSimpleObjectEventArgs = iconNameChangeSimpleObjectEventArgs;
        }

        public IBindingSimpleObject BindingObject
        {
            get { return this.iconNameChangeSimpleObjectEventArgs.SimpleObject!; }
        }

        public string? ImageName
        {
            get { return this.iconNameChangeSimpleObjectEventArgs.ImageName; }
        }
    }

    public class GraphValidationResultEventArgs : EventArgs
    {
		public GraphValidationResultEventArgs(GraphValidationResult graphValidationResult)
        {
            this.GraphValidationResult = graphValidationResult;
        }

        public GraphValidationResult GraphValidationResult { get; private set; }
    }

    public class RelationForeignObjectSetRequesterEventArgs : RequesterEventArgs
    {
        public RelationForeignObjectSetRequesterEventArgs(SimpleObject simpleObject, SimpleObject? foreignSimpleObject, SimpleObject? oldForeignSimpleObject, IOneToOneOrManyRelationModel relationModel, object? requester)
			: base(requester)
		{
            this.SimpleObject = simpleObject;
            this.ForeignSimpleObject = foreignSimpleObject;
            this.OldForeignSimpleObject = oldForeignSimpleObject;
            this.RelationModel = relationModel;
	    }

        public SimpleObject SimpleObject { get; private set; }
        public SimpleObject? ForeignSimpleObject { get; private set; }
        public SimpleObject? OldForeignSimpleObject { get; private set; }
        public IOneToOneOrManyRelationModel RelationModel { get; private set; }
    }

    public class BindingObjectRelationForeignObjectSetRequesterEventArgs : RequesterEventArgs
    {
        RelationForeignObjectSetRequesterEventArgs relationForeignObjectSetEventArgs = null;

        public BindingObjectRelationForeignObjectSetRequesterEventArgs(RelationForeignObjectSetRequesterEventArgs relationForeignObjectSetEventArgs)
			: base(relationForeignObjectSetEventArgs.Requester)
        {
            this.relationForeignObjectSetEventArgs = relationForeignObjectSetEventArgs;
        }

        public IBindingSimpleObject BindingObject { get { return this.relationForeignObjectSetEventArgs.SimpleObject; } }
        public IBindingSimpleObject ForeignBindingObject { get { return this.relationForeignObjectSetEventArgs.ForeignSimpleObject; } }
        public IBindingSimpleObject OldForeignBindingObject { get { return this.OldForeignBindingObject; } }
        public IOneToOneOrManyRelationModel RelationModel { get { return this.relationForeignObjectSetEventArgs.RelationModel; } }
    }

	//public class TransactionActionsEventArgs : TransactionEventArgs
	//{
	//	public TransactionActionsEventArgs(SystemTransaction transaction, IEnumerable<TransactionAction> transactionActions)
	//		: base(transaction)
	//	{
	//		this.TransactionActions = transactionActions;
	//	}

	//	public IEnumerable<TransactionAction> TransactionActions { get; private set; }
	//}
	
	//public class TransactionInfoEventArgs
	//{
 //       public TransactionInfoEventArgs(IEnumerable<TransactionActionInfo> transactionActions)
 //       {
	//		this.TransactionActions = transactionActions;	
 //       }

	//	public IEnumerable<TransactionActionInfo> TransactionActions { get; private set; }
	//}

	public class TransactionDatastoreActionRequesterEventArgs : TransactionRequesterEventArgs
	{
		public TransactionDatastoreActionRequesterEventArgs(SystemTransaction transaction, IEnumerable<DatastoreActionInfo>? datastoreActions, object? requester)
			: base(transaction, requester)
		{
			this.DatastoreActions = datastoreActions;
		}

		public IEnumerable<DatastoreActionInfo>? DatastoreActions { get; private set; }
	}

	public class TransactionRequesterEventArgs : RequesterEventArgs
    {
        public TransactionRequesterEventArgs(SystemTransaction transaction, object? requester)
			: base(requester)
        {
            this.Transaction = transaction;
        }

        public SystemTransaction Transaction { get; private set; }
    }

	public class ValidationInfoEventArgs : EventArgs
	{
		public ValidationInfoEventArgs(SimpleObjectValidationResult validationResult)
		{
			this.ValidationResult = validationResult;
		}

		public SimpleObjectValidationResult ValidationResult { get; set; }
	}

	public class ChangeObjectIdSimpleObject : SimpleObjectEventArgs
	{
		public ChangeObjectIdSimpleObject(SimpleObject simpleObject, long oldTempId, long newId)
			: base(simpleObject)
		{
			this.OldTempId = oldTempId;
			this.NewId = newId;
		}

		public long OldTempId { get; private set; }
		public long NewId { get; private set; }
	}

	#endregion |   EventArgs Classes   |

	#region |   Helper Classes   |

	public class ProcessTransactionRequestResult
	{
		public ProcessTransactionRequestResult(bool revokeTransaction, TransactionResult transactionResult, IEnumerable<long> newObjectIds, string infoMessage)
		{
			this.RevokeTransaction = revokeTransaction;
			this.TransactionResult = transactionResult;
			this.NewObjectIds = newObjectIds;
			this.InfoMessage = infoMessage;
		}

		public bool RevokeTransaction { get; private set; }
		public TransactionResult TransactionResult { get; private set; }
		public string InfoMessage { get; private set; }
		public IEnumerable<long> NewObjectIds { get; private set; }
	}

	#endregion |   Helper Classes   |

	#region |   Interfaces   |

	#endregion |   Interfaces   |

	#region |   Enums   |

	public enum ObjectManagerWorkingMode
	{
		Server,
		Client,
		ClientWithLocalDatastore
	}

	public enum SimpleObjectKeyMode
	{
		UseCreatorServerId,
		WithoutCreatorServerId
	}
	
	public enum SubTreePolicy
    {
        AllowSubTree,
        DoNotAllowSubTree
    }

    public enum ObjectRelationType
    {
        OneToOne,
        OneToMany
    }

	#endregion |   Enums   |
}