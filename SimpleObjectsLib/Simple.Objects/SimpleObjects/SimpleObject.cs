using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Collections;
using Simple.Modeling;
using static System.Net.Mime.MediaTypeNames;

namespace Simple.Objects
{
	public abstract partial class SimpleObject : IBindingSimpleObject, IPropertyValue, IEqualityComparer, IDisposable
	{
		#region |   Private Members   |

		//private ISimpleObjectModel model = null;
		//private bool isStorable = true;
		private int status = 0;
		private bool isPropertyInitialization = true;
		private SimpleObjectInternalState internalState = SimpleObjectInternalState.Initialization;
		private ChangeContainer? changeContainer, defaultChangeContainer;
		private ObjectActionContext context, defaultContext;
		private object? requester = null, defaultRequester = null;
		//private SimpleObjectCollection<GraphElement> graphElements = null;
		//private Hashtable manyToManyCollectionsByRelatedObjectType = null;
		//private IObjectRelationModel objectRelationModel = null;
		//private SimpleObjectRelationCache objectRelationCache = null;
		//private SimpleDictionary<int, SimpleObject> oneToOnePrimaryObjectsByRelationKey = null;    // Key is one to one relation model key as int, Value is SimpleObject
		//private Dictionary<int, SimpleObjectKey>? oneToOneForeignSimpleObjectsByRelationKey = null;    // Key is one to one relation model key as int, Value is SimpleObject
		private Dictionary<int, SimpleObject?>? oneToOneForeignSimpleObjectsByRelationKey = null;    // Key is one to one relation model key as int, Value is SimpleObject
																									//private SimpleDictionary<int, SimpleObject> oneToManyPrimaryObjectsByRelationKey = null;    // Key is one to many relation model key as int, Value is SimpleObjectCollection
		private Dictionary<int, SimpleObjectCollection>? oneToManyForeignCollectionsByRelationKey = null;    // Key is one to many relation model key as int, Value is SimpleObjectCollection
		//private SimpleDictionary<int, ISimpleObjectCollection_OLD> manyToManyCollectionsByRelationKey = null;  // Key is many to many relation model key as int, Value is SimpleObjectCollection
		private Dictionary<int, SimpleObjectCollection>? groupMembershipCollectionsByRelationKey = null;  // Key is many to many relation model key as int, Value is SimpleObjectCollection

		//private ImageInfo imageInfo = null;

		//private bool isServerTransactionInProgress = false;
		//private SimpleDictionary<SimpleObject, TransactionRequestAction> relatedTransactionRequests = null;
		//private delegate void RelationObjectCollectionActionDelegate(SimpleObjectCollection simpleObjectCollection);
		//private List<BusinessCollection> relatedCollections = null;
		//protected const string strName = "Name";
		//protected const string strDescription = "Description";


		#endregion |   Private Members   |

		#region |   Protected Members   |

		protected long id, oldId;

		//protected Guid guid, oldGuid;
		//protected string name, oldName;
		//protected string description, oldDescription;
		//protected int objectSubType, oldObjectSubType;

		#endregion |   Protected Members   |

		#region |   Public Static Members   |


		//public static readonly object Empty = new object();
		//public static SimpleObject Empty = new EmptyObject();
		public const string StringPropertyId = "Id";
		//public const string StringPropertyGuid = "Guid";
		public const string StringPropertyName = "Name";
		public const string StringPropertyDescription = "Description";
		public const string StringPropertyObjectSubType = "ObjectSubType";
		//public const string StringPropertyPreviousId = "PreviousId";
		//public const string StringPropertyPreviousGuid = "PreviousGuid";
		//public const string StringPropertyOrderIndex = "OrderIndex";

		public const int IndexPropertyId = 0;

		//public const int IndexPropertyGuid = 1;
		//public const int IndexPropertyName = 2;
		//public const int IndexPropertyDescription = 3;
		//public const int IndexPropertyObjectSubType = 4;
		//public const int IndexPropertyPreviousId = 5;
		//public const int IndexPropertyPreviousGuid = 6;
		//public const int IndexPropertyOrderIndex = 7;

		#endregion |   Public Static Members   |

		#region |   Constructors and Initialization   |

		//public SimpleObject()
		//          : base(SimpleObjectManager.Instance, false)
		//      {
		//      }

		protected SimpleObject()
			: this(SimpleObjectManager.Instance!) 
		{
		}

		public SimpleObject(SimpleObjectManager manager)
			: this(manager, manager.DefaultChangeContainer)
		{
		}

		public SimpleObject(SimpleObjectManager manager, ChangeContainer? changeContainer, ObjectActionContext context = ObjectActionContext.Unspecified, object? requester = null)
		//: this(objectManager, true)
		{
			this.manager = manager;
			this.changeContainer = changeContainer;
			this.defaultChangeContainer = changeContainer; // manager.DefaultChangeContainer;

			if (context == ObjectActionContext.Unspecified)
				this.defaultContext = (manager.WorkingMode == ObjectManagerWorkingMode.Server) ? ObjectActionContext.ServerTransaction : ObjectActionContext.Client;
			else
				this.defaultContext = context;

			this.context = defaultContext;
			this.requester = requester;
			this.defaultRequester = requester;
			this.isStorable = this.GetModel().IsStorable;
			//this.isPropertyInitialization = true;
			this.OnInitialize();
			this.isPropertyInitialization = false;
		}

		#endregion |   Constructors and Initialization   |

		#region |   Abstract Members & Methods   |



		#endregion |   Abstract Members & Methods   |

		#region |   Public Properties   |

		public SimpleObjectManager Manager => this.manager; 

		public long Id
		{
			get
			{
				if (this.id == 0 && this.isNew)
				{
					this.id = this.Manager.AddNewObjectInCache(simpleObject: this, this.ChangeContainer, this.Context, this.Requester);
					//this.changedPropertyIndexes.Add(SimpleObject.IndexPropertyId);
					//this.changedSaveablePropertyIndexes.Add(SimpleObject.IndexPropertyId);
				}

				return this.id;
			}

			//internal set { this.id = value; }
		}

		public ChangeContainer? ChangeContainer
		{
			get => this.changeContainer;

			set
			{
				//if (value is null)
				//	throw new ArgumentNullException(nameof(value) + " isn't allowed to be null");

				this.changeContainer = value;
			}

		}

		public ChangeContainer? DefaultChangeContainer
		{
			get => this.changeContainer;

			set
			{
				//if (value is null)
				//	throw new ArgumentNullException(nameof(value) + " isn't allowed to be null");

				this.defaultChangeContainer = value;
			}

		}

		public ObjectActionContext Context { get => this.context; set => this.context = value; }

		public ObjectActionContext DefaultContext { get => this.context; set => this.context = value; }

		public object? Requester { get => this.requester; set => this.requester = value; }
		public object? DefaultRequester { get => this.defaultRequester; set => this.defaultRequester = value; }

		//public void SetChangeMember(ChangeContainer changeContainer, object requester)
		//{
		//	this.ChangeContainer = changeContainer;
		//	this.Requester = requester;
		//}

		//public Guid Guid
		//{
		//	get
		//	{
		//		if (this.guid == default(Guid))
		//		{
		//			ISimpleObjectModel objectModel = this.GetModel();
		//			//long objectId = this.GetPropertyValue<long>(SimpleObjectModel.PropertyModel.ObjectId, 0);

		//			//if (objectId == 0)
		//			//{
		//			TempKeysHolder keysHolder = this.ObjectManager.AddNewObject(this, objectModel.TableInfo.TableId, this.ObjectManager.ClientId); //, currentObjectKey);

		//			this.id = keysHolder.Id;
		//			this.guid = keysHolder.Guid;

		//			//this.SetPropertyValueInternal(SimpleObjectModel.PropertyModel.ObjectId.Name, key.ObjectId, false, true, this);
		//			//this.SetPropertyValueInternal(SimpleObjectModel.PropertyModel.CreatorServerId.Name, key.CreatorServerId, false, true, this);

		//			this.ObjectIsCreated();
		//			//}
		//			//else
		//			//{
		//			//	int creatorServerId = this.GetPropertyValue<int>(SimpleObjectModel.PropertyModel.CreatorServerId);
		//			//	this.key = new SimpleObjectKey(objectModel.TableInfo.TableId, objectId, creatorServerId);
		//			//}
		//		}

		//		return this.guid;
		//	}

		//	//internal set
		//	//{
		//	//	//this.key = value;
		//	//	this.SetPropertyValue<Guid>(IndexPropertyKey, value, ref this.key, ref this.oldKey);

		//	//	//this.SetPropertyValueInternal(SimpleObjectModel.PropertyModel.ObjectId.Name, key.ObjectId, false, true, this);
		//	//	//this.SetPropertyValueInternal(SimpleObjectModel.PropertyModel.CreatorServerId.Name, key.CreatorServerId, false, true, this);
		//	//}
		//}

		internal void SetId(long objectId)
		{
			this.SetFieldValue(this.GetModel().IdPropertyModel.PropertyIndex, objectId);
			this.id = objectId;
		}

		public bool IsNew => this.isNew;

		//internal void SetKey(Guid objectKey)
		//{
		//	this.guid = objectKey;
		//}

		public int Status
		{
			get { return this.status; }
			set
			{
				if (this.status != value)
				{
					int oldStatus = this.status;
					this.status = value;
					//this.RecalcImageName();
					this.Manager.StatusIsChanged(this, this.status, oldStatus);
				}
			}
		}

		public virtual string? GetName()
		{
			string? name = null;

			if (this.GetModel().NamePropertyModel != null)
				name = this.GetPropertyValue<string>(this.GetModel().NamePropertyModel); // ?? this.ToString();
			//else
			//	name = this.ToString();

			return name;
		}

		#endregion |   Public Properties   |

		#region |   Protected Internal Properties   |

		////protected internal List<SimpleObjectTransactionRequest> RelatedTransactionRequests
		//public IEnumerable<TransactionRequest> GetRelatedTransactionRequests()
		//{
		//	return this.RelatedTransactionRequests.AsReadOnly();
		//}

		// internal 

		//private void RelatedTransactionRequests_AfterInsert(object sender, CollectionActionEventArgs<TransactionRequest> e)
		//{
		//	this.RemoveUnnecessaryRelatedTransactionRequests(e);
		//}

		//private void RelatedTransactionRequests_AfterSet(object sender, CollectionActionOldValueEventArgs<TransactionRequest> e)
		//{
		//	this.RemoveUnnecessaryRelatedTransactionRequests(e);
		//}

		//private void RelatedTransactionRequests_AfterRemove(object sender, CollectionActionEventArgs<TransactionRequest> e)
		//{
		//	this.RemoveUnnecessaryRelatedTransactionRequests(e);
		//}

		//private void RemoveUnnecessaryRelatedTransactionRequests(CollectionActionEventArgs<TransactionRequest> initiator)
		//{
		//	foreach (var item in this.RelatedTransactionRequests.ToArray())
		//	{
		//		//if (i == initiator.Index)
		//		//	continue;

		//		SimpleObject simpleObject = item.Key;
		//		TransactionRequestAction itemRequestAction = item.Value;

		//		if (itemRequestAction == TransactionRequestAction.Save)
		//		{
		//			if (!simpleObject.RequireSaving())
		//				this.RelatedTransactionRequests.Remove(simpleObject);
		//		}
		//		else if (itemRequestAction == TransactionRequestAction.Delete)
		//		{
		//			if (simpleObject.IsDeleteStarted)
		//				this.RelatedTransactionRequests.Remove(simpleObject);
		//		}
		//	}
		//}

		//private int ObjectId
		//{
		//    get
		//    {
		//        int objectId = 0;
		//        object objectIdValue = this.GetPropertyValue(SimpleObjectModel.PropertyModel.ObjectId);

		//        if (objectIdValue != null)
		//        {
		//            objectId = (int)objectIdValue;
		//        }

		//        if (objectId == 0)
		//        {
		//            ISimpleObjectModel objectModel = this.GetObjectModel();
		//            SimpleObjectKey currentObjectKey = SimpleObjectKey.GetEmptyKey(objectModel.TableInfo.TableId);

		//            SimpleObjectKey objectKey = this.Manager.ObjectCache.AddObject(this); //, currentObjectKey);

		//            this.SetPropertyValueInternal(SimpleObjectModel.PropertyModel.ObjectId.Name, objectKey.ObjectId, false, true, this);
		//            this.SetPropertyValueInternal(SimpleObjectModel.PropertyModel.CreatorServerId.Name, objectKey.CreatorServerId, false, true, this);

		//            objectId = objectKey.ObjectId;

		//            this.ObjectIsCreated();
		//        }

		//        return objectId;
		//    }
		//}

		//private Hashtable ManyToManyCollectionsByRelatedObjectType
		//{
		//    get
		//    {
		//        if (this.manyToManyCollectionsByRelatedObjectType != null)
		//        {
		//            this.manyToManyCollectionsByRelatedObjectType = new Hashtable();
		//        }

		//        return this.manyToManyCollectionsByRelatedObjectType;
		//    }
		//}

		#endregion |   Protected Internal Properties   |

		#region |   Internal Properties   |

		//internal bool IsServerTransactionInProgress
		//{
		//	get { return this.isServerTransactionInProgress; }

		//	set
		//	{
		//		lock (this.lockObject)
		//		{
		//			this.isServerTransactionInProgress = value;
		//		}
		//	}
		//}

		#endregion |   Internal Properties   |

		#region |   Private Properties   |

		//private SimpleDictionary<int, SimpleObject> OneToOnePrimaryObjectsByRelationKey
		//{
		//	get
		//	{
		//		if (this.oneToOnePrimaryObjectsByRelationKey == null)
		//			this.oneToOnePrimaryObjectsByRelationKey = new SimpleDictionary<int, SimpleObject>();

		//		return this.oneToOnePrimaryObjectsByRelationKey;
		//	}
		//}

		private Dictionary<int, SimpleObject?> OneToOneForeignSimpleObjectsByRelationKey => this.oneToOneForeignSimpleObjectsByRelationKey ??= new Dictionary<int, SimpleObject?>();

		//private SimpleDictionary<int, SimpleObject> OneToManyPrimaryObjectsByRelationKey
		//{
		//	get
		//	{
		//		if (this.oneToManyPrimaryObjectsByRelationKey == null)
		//			this.oneToManyPrimaryObjectsByRelationKey = new SimpleDictionary<int, SimpleObject>();

		//		return this.oneToManyPrimaryObjectsByRelationKey;
		//	}
		//}

		protected Dictionary<int, SimpleObjectCollection> OneToManyForeignCollectionsByRelationKey => this.oneToManyForeignCollectionsByRelationKey ??= new Dictionary<int, SimpleObjectCollection>();

		//private SimpleDictionary<int, ISimpleObjectCollection_OLD> ManyToManyObjectCollectionsByRelationKey
		//{
		//	get
		//	{
		//		if (this.manyToManyCollectionsByRelationKey == null)
		//			this.manyToManyCollectionsByRelationKey = new SimpleDictionary<int, ISimpleObjectCollection_OLD>();

		//		return this.manyToManyCollectionsByRelationKey;
		//	}
		//}

		private Dictionary<int, SimpleObjectCollection> GroupMembershipCollectionsByRelationKey => this.groupMembershipCollectionsByRelationKey ??= new Dictionary<int, SimpleObjectCollection>();

		#endregion |   Private Properties   |

		#region |   Public Static Methods   |

		///// <summary>
		///// Creates object unique Guid key that consist from tableId, clientId and objectId.
		///// </summary>
		///// <param name="tableId">Unique object type identifier that is truncated and stored as Int16 value.</param>
		///// <param name="clientId">The client identifier.</param>
		///// <param name="objectId">The object identifier.</param>
		//public static Guid CreateGuid(int tableId, long clientId, long objectId)
		//{
		//	byte[] guidBytes = new byte[16];
		//	byte[] tableIdBytes = BitConverter.GetBytes(tableId);
		//	byte[] clientIdBytes = BitConverter.GetBytes(clientId);
		//	byte[] objectIdBytes = BitConverter.GetBytes(objectId);

		//	//if (BitConverter.IsLittleEndian)
		//	//{
		//	//	Array.Reverse(tableIdBytes);
		//	//	Array.Reverse(clientIdBytes);
		//	//	Array.Reverse(objectIdBytes);
		//	//}

		//	//randomBytes = new byte[16];
		//	//SequentialGuid.RandomGenerator.GetBytes(randomBytes);

		//	Buffer.BlockCopy(tableIdBytes, 0, guidBytes, 0, 2);
		//	Buffer.BlockCopy(clientIdBytes, 0, guidBytes, 2, 6);
		//	Buffer.BlockCopy(objectIdBytes, 0, guidBytes, 8, 8);

		//	return new Guid(guidBytes);
		//}

		///// <summary>
		///// Creates unique object key Guid that consist from tableId, serverId, clientId, objectId and isClientDynamic values.
		///// Dynamic client has a dinamicaly created clientId from server has no local datastore. 
		///// Non dynamic client is registered client with ClientId from SystemClients table.
		///// </summary>
		///// <param name="tableId">The <see cref="Int32"></see> table identifier.</param>
		///// <param name="objectId">The <see cref="Int64"></see> client identifier.</param>
		///// <param name="serverId">The <see cref="Int32"></see> server identifier.</param>
		///// <param name="clientId">The <see cref="Int64"></see> client identifier.</param>
		///// <param name="isClientDynamic">If True clientId is dynamicly generated; if False clientId is registered client from SystemClients</param>
		///// <returns>Unique object key <see cref="System.Guid"></see> identifier.</returns>
		//public static Guid CreateGuid2(int tableId, long objectId, int serverId, long clientId, bool isClientDynamic)
		//{
		//	byte[] buffer = new byte[16];

		//	uint unsignedTableId   = ((unchecked((uint)tableId)) << 1) | Convert.ToByte(isClientDynamic);
		//	uint unsignedServerId  = unchecked((uint)serverId);
		//	ulong unsignedClientId = unchecked((ulong)clientId);
		//	ulong unsignedObjectId = unchecked((ulong)objectId);

		//	buffer[0]  = (byte)unsignedTableId;
		//	buffer[1]  = (byte)(unsignedTableId >>= 8);

		//	buffer[2]  = (byte)unsignedServerId;

		//	buffer[3]  = (byte)unsignedClientId; unsignedClientId >>= 8;
		//	buffer[4]  = (byte)unsignedClientId; unsignedClientId >>= 8;
		//	buffer[5]  = (byte)unsignedClientId; unsignedClientId >>= 8;
		//	buffer[6]  = (byte)unsignedClientId; unsignedClientId >>= 8;
		//	buffer[7]  = (byte)unsignedClientId;

		//	buffer[8]  = (byte)unsignedObjectId; unsignedObjectId >>= 8;
		//	buffer[9]  = (byte)unsignedObjectId; unsignedObjectId >>= 8;
		//	buffer[10] = (byte)unsignedObjectId; unsignedObjectId >>= 8;
		//	buffer[11] = (byte)unsignedObjectId; unsignedObjectId >>= 8;
		//	buffer[12] = (byte)unsignedObjectId; unsignedObjectId >>= 8;
		//	buffer[13] = (byte)unsignedObjectId; unsignedObjectId >>= 8;
		//	buffer[14] = (byte)unsignedObjectId; unsignedObjectId >>= 8;
		//	buffer[15] = (byte)unsignedObjectId;

		//	//Buffer.BlockCopy(BitConverter.GetBytes(tableId), 0, buffer, 0, 2);
		//	//Buffer.BlockCopy(BitConverter.GetBytes(serverId), 0, buffer, 3, 1);
		//	//Buffer.BlockCopy(BitConverter.GetBytes(unsignedClientId), 0, buffer, 4, 5);
		//	//Buffer.BlockCopy(BitConverter.GetBytes(objectId), 0, buffer, 8, 8);

		//	return new Guid(buffer);
		//}

		//// TODO: Remove this and leave only Guid extensions

		//public static Guid CreateKey3(int serverId, int tableId, bool isClientDynamic, long clientId, long objectId)
		//{
		//	return (Guid) new SimpleObjectKey3(serverId, tableId, isClientDynamic, clientId, objectId).Value;
		//}

		//public static int GetTableId(Guid objectKey)
		//{
		//	return BitConverter.ToUInt16(objectKey.ToByteArray(), 0);
		//}

		//public static int GetTableId(Guid? objectKey)
		//{
		//	return (objectKey == null) ? 0 : GetTableId(objectKey.Value);
		//}

		//public static long GetClientId(Guid objectKey)
		//{
		//	return BitConverter.ToInt64(objectKey.ToByteArray(), 0) >> 16;
		//}

		//public static long GetClientId(Guid? objectKey)
		//{
		//	return (objectKey == null) ? 0 : GetClientId(objectKey.Value);
		//}

		//public static long GetObjectId(Guid objectKey)
		//{
		//	return BitConverter.ToInt64(objectKey.ToByteArray(), 8);
		//}

		//public static long GetObjectId(Guid? objectKey)
		//{
		//	return (objectKey == null) ? 0 : GetObjectId(objectKey.Value);
		//}



		//// TODO: Remove this and leave only Guid extensions
		//// New Key format

		//public int GetTableId()
		//{
		//	return this.Guid.GetTableId2();
		//}

		//public long GetObjectId()
		//{
		//	return this.Guid.GetObjectId2();
		//}

		//public int GetServerId()
		//{
		//	return this.Guid.GetServerId2();
		//}

		//public long GetClientId()
		//{
		//	return this.Guid.GetClientId2();
		//}

		//public bool GetIsClientDynamic()
		//{
		//	return this.Guid.GetIsClientDynamic2();
		//}


		//public static string GetPropertyValueString(IPropertyModel propertyModel, object propertyValue)
		//{
		//	if (propertyValue != null)
		//	{
		//		return propertyValue.ToString();
		//	}
		//	else
		//	{
		//		return "null";
		//	}
		//	//else if (propertyModel.IsRelationKey && (propertyModel.PropertyTypeId == (int)ObjectTypes.TypeId.Guid || propertyModel.PropertyTypeId == (int)ObjectTypes.TypeId.NullableGuid)) //else if (propertyValue.GetType() == typeof(Guid))
		//	//{
		//	//	return ((Guid)propertyValue).ToObjectKeyString();
		//	//}
		//}

		#endregion |   Public Static Methods   |

		#region |   Public Abstract Methods   |

		public abstract ISimpleObjectModel GetModel();

		#endregion |   Public Abstract Methods   |

		#region |   Public Methods   |

		//// TODO: Remove this after testing feature and make it internal
		//internal protected void SetInternalState(SimpleObjectInternalState internalState)
		//{
		//	this.internalState = internalState;
		//}


		//public virtual ISimpleObjectModel GetModel()
		//      {
		//          if (this.model == null)
		//              this.model = this.ObjectManager.GetObjectModel(this.Guid.GetTableId());

		//          return this.model;
		//      }

		//public GraphElement GetGraphElement(Graph graph)
		//      {
		//	return this.GetGraphElement(graph.GraphKey); // this.GraphElements.FirstOrDefault(ge => ge.Graph == graph);
		//      }

		public GraphElement? GetGraphElement(int graphKey)
        {
			// TODO: Move this functionality to ObjectManager

			//return this.Manager.GetSimpleObjectGraphElementByGraphKey(this, graphKey);
			//var graphElements = this.GraphElements;

			//for (int i = 0; i < graphElements.Count; i++)
			//{
			//	GraphElement graphElement = graphElements[i];
				
			//	if (graphElement.GraphKey == graphKey)
			//		return graphElement;
			//}

			////foreach (GraphElement graphElement in graphElements)
			////	if (graphElement.GraphKey == graphKey) 
			////		return graphElement;

			//return null;

			return this.GraphElements.FirstOrDefault(graphElement => graphElement.GraphKey == graphKey);


			//foreach (GraphElement graphElement in (this.GraphElements as IEnumerable<GraphElement>).fi)
			//	if (graphElement.GraphKey == graphKey)
			//		return graphElement;

			//return null;
        }

        //public GraphElement GetParentGraphElement(Graph graph)
        //{
        //    return this.GetParentGraphElement(graph.GraphKey);
        //}
        
        public GraphElement? GetParentGraphElement(int graphKey)
        {
            GraphElement? graphElement = this.GetGraphElement(graphKey);
            
			return graphElement?.Parent;
        }

        //public SimpleObject GetParentGraphElementSimpleObject(Graph graph)
        //{
        //    return this.GetParentGraphElementSimpleObject(graph.GraphKey);
        //}
        
        public SimpleObject? GetParentGraphElementSimpleObject(int graphKey)
        {
            GraphElement? parentGraphElement = this.GetParentGraphElement(graphKey);
            
			return parentGraphElement?.SimpleObject;
        }

        //public SimpleObjectCollection<T> GetChildGraphElementSimpleObjectCollection<T>(Graph graph) where T : SimpleObject
        //{
        //    return this.GetChildGraphElementSimpleObjectCollection<T>(graph.GraphKey);
        //}

        public SimpleObjectCollection<T> GetChildGraphElementSimpleObjectCollection<T>(int graphKey) where T : SimpleObject
        {
            GraphElement? graphElement = this.GetGraphElement(graphKey);

            if (graphElement != null)
                return graphElement.GetChildGraphElementSimpleObjectCollection<T>();
            else
                return new SimpleObjectCollection<T>(this.Manager, this.GetModel().TableInfo.TableId, new long[0]);
        }

        //public string GetPathName(Graph graph)
        //{
        //    return this.GetPathName(graph.GraphKey);
        //}

        //public string GetPathName(Graph graph, string splitter)
        //{
        //    return this.GetPathName(graph.GraphKey, splitter);
        //}

        public string GetPathName(int graphKey)
        {
            GraphElement? graphElement = this.GetGraphElement(graphKey);
            
			return graphElement?.GetPathName() ?? String.Empty;
        }

        public string GetPathName(int graphKey, string splitter)
        {
            GraphElement? graphElement = this.GetGraphElement(graphKey);
           
			return graphElement?.GetPathName(splitter) ?? String.Empty;
        }

        public string GetFullPathName(int graphKey, string splitter)
        {
            GraphElement? graphElement = this.GetGraphElement(graphKey);
            
			return graphElement?.GetFullPathName(splitter) ?? String.Empty;
        }

        public string GetFullPathName(int graphKey)
        {
            GraphElement? graphElement = this.GetGraphElement(graphKey);
            
			return graphElement?.GetFullPathName() ?? String.Empty;
        }

        public string GetReverseGraphPathName(int graphKey)
        {
            GraphElement? graphElement = this.GetGraphElement(graphKey);
            
			return graphElement?.GetReversePathName() ?? String.Empty;
        }

        public string GetReverseGraphPathName(int graphKey, string splitter)
        {
            GraphElement? graphElement = this.GetGraphElement(graphKey);
            
			return graphElement?.GetReversePathName(splitter) ?? String.Empty;
        }

        public string GetFullReversePathName(int graphKey)
        {
            GraphElement? graphElement = this.GetGraphElement(graphKey);
            
			return graphElement?.GetFullReversePathName() ?? String.Empty;
        }

        public string GetFullReversePathName(int graphKey, string splitter)
        {
            GraphElement? graphElement = this.GetGraphElement(graphKey);
            
			return graphElement?.GetFullReversePathName(splitter) ?? String.Empty;
        }

		//protected ManyToManyCollection<T> GetManyToManyCollection<T>() where T : SimpleObject
		//{
		//    ManyToManyCollection<T> result = this.ManyToManyCollectionsByRelatedObjectType[typeof(T)] as ManyToManyCollection<T>;

		//    if (result == null)
		//    {
		//        result = this.Manager.ObjectCache.GetManyToManyCollection<T>(this);
		//        this.ManyToManyCollectionsByRelatedObjectType.Add(typeof(T), result);
		//    }

		//    return result;
		//}

		//public new void SetPropertyValue(string propertyName, object value)
		//{
		//    if (propertyName == SimpleObjectPropertyModelCollection.ID.Name)
		//    {
		//        throw new System.ArgumentException("You cannot set object key ID.");
		//    }
		//    else
		//    {
		//        base.SetPropertyValue(propertyName, value);
		//    }
		//}



        //public IObjectRelationModel GetObjectRelationModel()
        //{
        //	if (this.objectRelationModel == null)
        //	{
        //		this.objectRelationModel = this.Manager.GetObjectRelationModel(this.GetType());
        //	}

        //	return this.objectRelationModel;
        //}



   //     public bool Save()
   //     {
			//return this.Save(requester: this);
   //     }

   //     public bool Save(object requester)
   //     {
   //         return this.Manager.Save(this, requester);
   //     }

        public void RequestDelete()
        {
            this.RequestDelete(this.Requester);
        }

		public void RequestDelete(object? requester)
		{
			this.RequestDelete(this.ChangeContainer, requester);
		}

		public void RequestDelete(ObjectActionContext context)
		{
			this.RequestDelete(context, this.Requester);
		}

		public void RequestDelete(ObjectActionContext context, object? requester)
		{
			this.RequestDelete(this.ChangeContainer, context, requester);
		}

		public void RequestDelete(ChangeContainer? changeContainer)
		{
			this.RequestDelete(changeContainer, this.Requester);
		}

		public void RequestDelete(ChangeContainer? changeContainer, object? requester)
        {
			this.RequestDelete(changeContainer, this.Context, requester);
        }

		public void RequestDelete(ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			this.RequestDelete(findRelatedObjectsForDelete: true, changeContainer, context, requester);
		}

		public void RequestDelete(bool findRelatedObjectsForDelete, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			if (!this.DeleteRequested)
				this.Manager.RequestDeleteInternal(this, findRelatedObjectsForDelete, changeContainer, context, requester);
		}


		public void CancelDeleteRequest()
		{
			this.CancelDeleteRequest(this.Requester);
		}

		public void CancelDeleteRequest(object? requester)
		{
			this.CancelDeleteRequest(this.ChangeContainer, context: ObjectActionContext.Unspecified);
		}

		public void CancelDeleteRequest(ChangeContainer? changeContainer, ObjectActionContext context)
		{
			this.CancelDeleteRequest(changeContainer, context, this.Requester);
		}

		public void CancelDeleteRequest(ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			if (this.DeleteRequested)
				this.Manager.CancelDeleteRequest(this, changeContainer, context, requester);
		}

		//public void Save()
		//{
		//	this.Manager.CommitChanges();
		//}

		//public void Save(object requester)
		//{
		//	this.Manager.CommitChanges(requester);
		//}

		//public void Save(ChangeContainer changeContainer, object requester)
		//{
		//	this.Manager.CommitChanges(changeContainer, requester);
		//}

		//public void Delete()
		//{
		//	this.RequestDelete();
		//	this.Manager.CommitChanges();
		//}

		//public void Delete(object requester)
		//{
		//	this.RequestDelete(requester);
		//	this.Manager.CommitChanges(requester);
		//}

		//public void Delete(ChangeContainer changeContainer, object requester)
		//{
		//	this.RequestDelete(changeContainer, requester);
		//	this.Manager.CommitChanges(changeContainer, requester);
		//}

		public virtual SimpleObjectValidationResult ValidateSave() => this.Manager.ValidateSave(this, this.ChangeContainer?.TransactionRequests);

		public virtual SimpleObjectValidationResult ValidateDelete() => this.Manager.ValidateDelete(this, this.ChangeContainer?.TransactionRequests);

		//public new bool Save()
		//{
		//    return this.ObjectManager.Save(null, this);
		//}

		//public new void Delete()
		//{
		//    base.Delete();
		//}

		//public new SimpleObjectValidationResult Validate()
		//{
		//    return base.Validate();
		//}


		//public void SetRelatedTransactionRequest(IDictionary<SimpleObject, TransactionRequestAction> relatedTransactionRequests)
		//{
		//	foreach (var item in relatedTransactionRequests)
		//		this.SetRelatedTransactionRequest(item.Key, item.Value);
		//}

		//public void SetRelatedTransactionRequest(SimpleObject relatedSimpleObject, TransactionRequestAction requestAction)
		//{
		//	bool allowAdd = true;
		//	//TransactionRequest[] transactionRequests = this.RelatedTransactionRequests.ToArray();

		//	foreach (var item in this.RelatedTransactionRequests.ToArray())
		//	{
		//		SimpleObject itemSimpleObject = item.Key;
		//		TransactionRequestAction itemRequestAction = item.Value;

		//		if (itemSimpleObject == relatedSimpleObject)
		//		{
		//			if (itemRequestAction == TransactionRequestAction.Save && requestAction == TransactionRequestAction.Delete && !relatedSimpleObject.IsNew)
		//			{
		//				this.relatedTransactionRequests[itemSimpleObject] = TransactionRequestAction.Delete;
		//				allowAdd = false; // Prevent saving object if already deletion requested.

		//				continue;
		//			}

		//			allowAdd = false;
		//		}

		//		// remove unnecessary transaction requests
		//		if (itemRequestAction == TransactionRequestAction.Save)
		//		{
		//			if (!itemSimpleObject.RequireSaving())
		//				this.relatedTransactionRequests.Remove(itemSimpleObject);
		//		}
		//		else if (itemRequestAction == TransactionRequestAction.Delete)
		//		{
		//			if (itemSimpleObject.IsNew || itemSimpleObject.IsDeleteStarted)
		//				this.relatedTransactionRequests.Remove(itemSimpleObject);
		//		}
		//	}

		//	if (allowAdd)
		//	{
		//		if (requestAction == TransactionRequestAction.Save)
		//		{
		//			if (!relatedSimpleObject.RequireSaving())
		//				allowAdd = false;
		//		}
		//		else if (requestAction == TransactionRequestAction.Delete)
		//		{
		//			if (relatedSimpleObject.IsDeleteStarted)
		//				allowAdd = false;
		//		}
		//	}

		//	if (allowAdd)
		//	{
		//		if (relatedSimpleObject.IsNew && requestAction == TransactionRequestAction.Delete)
		//		{
		//			this.relatedTransactionRequests.Remove(relatedSimpleObject);
		//			this.Manager.DeleteInternal(relatedSimpleObject, requester: this);
		//		}
		//		else
		//		{
		//			this.relatedTransactionRequests.Add(relatedSimpleObject, requestAction);
		//		}
		//	}
		//}

		//public void SetRelatedTransactionRequest_OLD(SimpleObject relatedSimpleObject, TransactionRequestAction requestAction)
		//{
		//	bool allowAdd = true;
		//	//TransactionRequest[] transactionRequests = this.RelatedTransactionRequests.ToArray();

		//	foreach (var item in this.RelatedTransactionRequests.ToArray())
		//	{
		//		SimpleObject simpleObject = item.Key;
		//		TransactionRequestAction itemRequestAction = item.Value;

		//		if (simpleObject == relatedSimpleObject)
		//		{
		//			if (itemRequestAction == TransactionRequestAction.Save && requestAction == TransactionRequestAction.Delete)
		//			{
		//				this.RelatedTransactionRequests.Remove(simpleObject);
		//				allowAdd = true; // Prevent saving object if already is requested for delete.

		//				continue;
		//			}
		//			else
		//			{
		//				allowAdd = false;
		//			}
		//		}

		//		// remove unnecessary transaction requests
		//		if (itemRequestAction == TransactionRequestAction.Save)
		//		{
		//			if (!simpleObject.RequireSaving())
		//				this.RelatedTransactionRequests.Remove(simpleObject);
		//		}
		//		else if (itemRequestAction == TransactionRequestAction.Delete)
		//		{
		//			if (simpleObject.IsDeleteStarted)
		//				this.RelatedTransactionRequests.Remove(simpleObject);
		//		}
		//	}

		//	if (allowAdd)
		//	{
		//		if (requestAction == TransactionRequestAction.Save)
		//		{
		//			if (!relatedSimpleObject.RequireSaving())
		//				allowAdd = false;
		//		}
		//		else if (requestAction == TransactionRequestAction.Delete)
		//		{
		//			if (relatedSimpleObject.IsDeleteStarted)
		//				allowAdd = false;
		//		}
		//	}

		//	if (allowAdd)
		//		this.RelatedTransactionRequests.Add(relatedSimpleObject, requestAction);
		//}

		//internal void RemoveRelatedTransactionRequest(SimpleObject relatedSimpleObject)
		//{
		//	this.RelatedTransactionRequests.Remove(relatedSimpleObject);

		//	//this.RelatedTransactionRequests.RemoveAll(item => item.SimpleObject == relatedSimpleObject);

		//	// variant 1
		//	//List<TransactionRequest> requestForRemove = this.relatedTransactionRequests.FindAll(item => item.SimpleObject == relatedSimpleObject);

		//	//foreach (TransactionRequest transactionRequest in requestForRemove)
		//	//	this.relatedTransactionRequests.Remove(transactionRequest);


		//	// variant 2
		//	//TransactionRequest[] transactionRequests = this.relatedTransactionRequests.ToArray();

		//	//for (int i = 0; i < transactionRequests.Length; i++)
		//	//{
		//	//	TransactionRequest transactionRequest = transactionRequests[i];

		//	//	if (transactionRequest.SimpleObject == relatedSimpleObject)
		//	//		this.relatedTransactionRequests.Remove(transactionRequest);
		//	//}
		//}

		//public void ClearRelatedTransactionRequests()
		//{
		//	if (this.relatedTransactionRequests != null)
		//		this.relatedTransactionRequests.Clear();
		//}

		public override bool Equals(object? obj)
        {
            //// If parameter is null return false.
            //if (obj == null)
            //    return false;

			if (obj is SimpleObject simpleObject)
				return this.Equals(simpleObject);

			return false;
            
			//// If parameter cannot be cast to SimpleObject return false.
   //         SimpleObject simpleObjectToCompare = obj as SimpleObject;



			//if ((object)simpleObjectToCompare == null)
   //             return false;

   //         // If both are the same instance, return true.
   //         if (System.Object.ReferenceEquals(this, simpleObjectToCompare))
   //             return true;

			//// Return true if the keys are equal and id IsDeleted match.
			//if (this.Id == simpleObjectToCompare.Id && this.GetModel().TableInfo.TableId == simpleObjectToCompare.GetModel().TableInfo.TableId)
			//	return this.IsDeleted == simpleObjectToCompare.IsDeleted;

			//return false;
		}

		public bool Equals(SimpleObject simpleObject)
        {
			return this == simpleObject;
			
			//// If parameter is null return false.
   //         if ((object)simpleObject == null)
   //             return false;

   //         // If both are null, or both are same instance, return true.
   //         if (System.Object.ReferenceEquals(this, simpleObject))
   //             return true;

			//// Return true if the keys are equal and id IsDeleted match.
			//if (this.Id == simpleObject.Id && this.GetModel().TableInfo.TableId == simpleObject.GetModel().TableInfo.TableId)
			//	return this.IsDeleted == simpleObject.IsDeleted;

			//return false;
        }

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			//return base.GetHashCode();
			//int result2 = this.GetModel().TableInfo.TableId.GetHashCode() ^ this.Id.GetHashCode();

			return result;
		}


		public static bool operator ==(SimpleObject? a, SimpleObject? b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
                return true;

            // If one is null, but not both, return false.
            if (a is null ^ b is null)
                return false;

			if (a is null || b is null)
				return false;

			// Return true if the key fields match.
            if (a.Id == b.Id && a.GetModel().TableInfo.TableId == b.GetModel().TableInfo.TableId)
				return a.IsDeleted == b.IsDeleted;

			return false;
		}

		public static bool operator !=(SimpleObject? a, SimpleObject? b) => !(a == b);

		public bool EqualsByProperties(SimpleObject? other)
		{
			if (other is null) 
				return false;

			if (other.GetModel().TableInfo.TableId != this.GetModel().TableInfo.TableId) 
				return false;

			foreach (var propertyModel in this.GetModel().PropertyModels)
				if (!Comparison.IsEqual(this.GetPropertyValue(propertyModel.PropertyIndex), other.GetPropertyValue(propertyModel.PropertyIndex)))
					return false;

			return true;
		}

        public override string? ToString()
        {
			if (this.GetModel().NamePropertyModel != null)
				return this.GetPropertyValue<string>(this.GetModel().NamePropertyModel);

			return base.ToString();
        }

		#endregion |   Public Methods   |

		#region |   Public One-To-One Relation Methods   |

		// TODO: Create abstract method
		public SimpleObject? GetOneToOnePrimaryObject(int oneToOneRelationKey)
		{
			//OneToOneRelationModel oneToOneRelationModel;
			//RelationPolicyModelBase.Instance.OneToOneRelations.TryGetValue(oneToOneRelationKey, out oneToOneRelationModel);

			IOneToOneRelationModel? oneToOneRelationModel = this.GetModel().RelationModel.AsForeignObjectInOneToOneRelations[oneToOneRelationKey];

			if (oneToOneRelationModel == null)
			{
#if DEBUG
				throw new ArgumentException("GetOneToOnePrimaryObject: There is no relation model definition, RelationKey=" + oneToOneRelationKey);
#else
				return null;
#endif
			}

			return this.GetOneToOnePrimaryObject(oneToOneRelationModel);
		}

		protected T? GetOneToOnePrimaryObject<T>(IOneToOneRelationModel oneToOneRelationModel) where T : SimpleObject => this.GetOneToOnePrimaryObject(oneToOneRelationModel) as T;

		protected SimpleObject? GetOneToOnePrimaryObject(IOneToOneRelationModel oneToOneRelationModel)
		{
			//return this.ObjectRelationCache.GetOneToOneRelationForeignObject<T>(oneToOneRelationModel);
			SimpleObject? value;

			//if (!this.OneToOnePrimaryObjectsByRelationKey.TryGetValue(oneToOneRelationModel.RelationKey, out value))
			//{
				//if (this.GetModel().RelationModel.AsForeignObjectInOneToOneRelations.ContainsKey(oneToOneRelationModel.RelationKey)) // ObjectRelationModel.OneToOneRelationForeignKeyObjectDictionary.ContainsKey(oneToOneRelationModel.RelationKey))
				//{
					//int foreignObjectTableId;
					//Guid foreignObjectGuid;
					////int foreignCreatorServerId;
					//SimpleObjectKey foreignObjectKey;

					//if (oneToOneRelationModel.ForeignTableIdPropertyModel != null)
					//{
					//	foreignObjectTableId = this.GetPropertyValue<int>(oneToOneRelationModel.ForeignTableIdPropertyModel.Index);
					//}
					//else
					//{
					//	ISimpleObjectModel foreignObjectModel = this.Manager.GetObjectModel(oneToOneRelationModel.ForeignObjectType);
					//	foreignObjectTableId = foreignObjectModel.TableId;
					//}

					//Guid foreignObjectKey = this.GetPropertyValue<Guid>(oneToOneRelationModel.ForeignKeyPropertyModel.Index);
					int primaryTableId = (oneToOneRelationModel.PrimaryTableIdPropertyModel != null) ? this.GetPropertyValue<int>(oneToOneRelationModel.PrimaryTableIdPropertyModel.PropertyIndex) : oneToOneRelationModel.PrimaryObjectTableId;
					long primaryObjectId = this.GetPropertyValue<long>(oneToOneRelationModel.PrimaryObjectIdPropertyModel);
					//foreignCreatorServerId = this.Owner.GetPropertyValue<int>(oneToOneRelationModel.ForeignObjectCreatorServerIdPropertyName);

					value = this.Manager.GetObject(primaryTableId, primaryObjectId);
					//value = this.ObjectManager.GetObject(foreignObjectKey) as T;

					//if (value != null)
					//{
					//	//lock (lockObject)
					//	//{
					//	this.OneToOnePrimaryObjectsByRelationKey[oneToOneRelationModel.RelationKey] = value;
					//	//}
					//}
				//}
				//else
				//{
				//	return null;
				//	//throw new ArgumentException("GetOneToOneRelationForeignObject: There is no relation model definition.");
				//}
			//}

			return value;
		}

		// TODO: Create abstract method
		public SimpleObject? GetOneToOneForeignObject(int oneToOneRelationKey)
		{
			IOneToOneRelationModel? oneToOneRelationModel = this.GetModel().RelationModel.AsPrimaryObjectInOneToOneRelations[oneToOneRelationKey];

			if (oneToOneRelationModel == null)
			{
#if DEBUG
				throw new ArgumentException("GetOneToOneForeignObject: There is no relation model definition, RelationKey=" + oneToOneRelationKey);
#else
				return null;
#endif
			}

			return this.GetOneToOneForeignObject(oneToOneRelationModel);
		}


		protected T? GetOneToOneForeignObject<T>(IOneToOneRelationModel oneToOneRelationModel) where T : SimpleObject => this.GetOneToOneForeignObject(oneToOneRelationModel) as T;


		// TODO: foreignObjectCache.FindFirst method need to search throght the idexed values

		protected SimpleObject? GetOneToOneForeignObject(IOneToOneRelationModel oneToOneRelationModel)
		{
			// return this.ObjectRelationCache.GetOneToOneRelationKeyHolderObject<T>(oneToOneRelationModel);
			//return this.GetOneToOneRelationKeyHolderObject<T>(oneToOneRelationModel);

			//SimpleObjectKey valueKey;
			SimpleObject? value;

			//lock (this.lockObject)
			//{
				if (!this.OneToOneForeignSimpleObjectsByRelationKey.TryGetValue(oneToOneRelationModel.RelationKey, out value))
				{
					//if (this.GetModel().RelationModel.AsPrimaryObjectInOneToOneRelations.ContainsKey(oneToOneRelationModel.RelationKey)) // .ObjectRelationModel.OneToOneRelationKeyHolderObjectDictionary.ContainsKey(oneToOneRelationModel.RelationKey))
					//{
					//int foreignObjectTableId;
					//int foreignObjectId;
					//int foreignCreatorServerId;
					//SimpleObjectKey foreignObjectKey;

					//if (!String.IsNullOrEmpty(oneToOneRelationModel.ForeignObjectTableIdPropertyName))
					//{
					//    foreignObjectTableId = this.Owner.GetPropertyValue<int>(oneToOneRelationModel.ForeignObjectTableIdPropertyName);
					//}
					//else
					//{
					//    ISimpleObjectModel relatedObjectModel = this.Owner.Manager.GetObjectModel(oneToOneRelationModel.ForeignObjectType);
					//    foreignObjectTableId = relatedObjectModel.TableInfo.TableId;
					//}

					//foreignObjectId = this.Owner.GetPropertyValue<int>(oneToOneRelationModel.ForeignObjectIdPropertyName);
					//foreignCreatorServerId = this.Owner.GetPropertyValue<int>(oneToOneRelationModel.ForeignObjectCreatorServerIdPropertyName);
					//foreignObjectKey = new SimpleObjectKey(foreignObjectTableId, foreignObjectId, foreignCreatorServerId);

					//value = this.Owner.Manager.GetSimpleObject(foreignObjectKey) as T;
					////this.Owner.Manager.ObjectCache.get

					//SimpleObjectCollection<T> relatedObjects;

					//if (oneToOneRelationModel.ForeignTableIdPropertyModel != null)
					//{
					//	//match = new Predicate<SimpleObject>(o => o.GetPropertyValue<int>(oneToOneRelationModel.ForeignObjectTableIdPropertyName) == this.Owner.Key.TableId &&
					//	//                                           o.GetPropertyValue<int>(oneToOneRelationModel.ForeignObjectCreatorServerIdPropertyName) == this.Owner.Key.CreatorServerId &&
					//	//                                           o.GetPropertyValue<long>(oneToOneRelationModel.ForeignObjectIdPropertyName) == this.Owner.Key.ObjectId);
					//	match = new Predicate<SimpleObject>(bo => !bo.IsDeleteStarted && this.Key.GetTableId().Equals(bo.GetPropertyValue(oneToOneRelationModel.ForeignTableIdPropertyModel.Index)) &&
					//		//this.Owner.Key.CreatorServerId.Equals(o.GetPropertyValue(oneToOneRelationModel.ForeignObjectCreatorServerIdPropertyName)) &&
					//												this.Key.Guid.Equals(bo.GetPropertyValue(oneToOneRelationModel.ForeignGuidPropertyModel.Index)));
					//}
					//else
					//{
					//this.Owner.Key.CreatorServerId.Equals(o.GetPropertyValue(oneToOneRelationModel.ForeignObjectCreatorServerIdPropertyName)) &&

					//}


					if (this.Manager.WorkingMode == ObjectManagerWorkingMode.Client)
					{
						value = this.Manager.RemoteDatastore?.GetOneToOneForeignObject(this, oneToOneRelationModel.RelationKey).GetAwaiter().GetResult();
					}
					else
					{
						ServerObjectCache? foreignObjectCache = this.Manager.GetObjectCache(oneToOneRelationModel.ForeignObjectTableId) as ServerObjectCache;
						//Predicate<SimpleObject> match = new Predicate<SimpleObject>(simpleObject => !simpleObject.DeleteStarted && this.Id.Equals(simpleObject.GetPropertyValue(oneToOneRelationModel.PrimaryObjectIdPropertyModel.PropertyIndex)));

						value = foreignObjectCache?.FindFirst(simpleObject => !simpleObject.DeleteStarted && this.Id.Equals(simpleObject.GetPropertyValue(oneToOneRelationModel.PrimaryObjectIdPropertyModel.PropertyIndex)));

						//if (relatedObjects.Count > 0)
						//{
						//    value = relatedObjects[0];
						//}

						//if (value != null)
						//{
						//lock (lockObject)
						//{
						//}
						//}
						//				}
						//				else
						//				{
						//					return null;

						//#if DEBUG
						//					throw new ArgumentException("GetOneToOneRelationKeyHolderObject: There is no relation model definition.");
						//#endif
						//				}
					}

					//SimpleObjectKey foreignObjectKey = (value != null) ? new SimpleObjectKey(value.GetModel().TableInfo.TableId, value.Id) : new SimpleObjectKey(0, 0);

					this.SetOneToOneForeignObjectInternal(oneToOneRelationModel.RelationKey, value);
				}
				//else
				//{
				//	value = this.Manager.GetObject(valueKey);
				//}
			//}
			return value;
		}

		public bool SetOneToOnePrimaryObject(SimpleObject? primaryObject, int oneToOneRelationKey)
		{
			return this.SetOneToOnePrimaryObject(primaryObject, oneToOneRelationKey, this.Requester);
		}

		public bool SetOneToOnePrimaryObject(SimpleObject? primaryObject, int oneToOneRelationKey, object? requester)
		{
			return this.SetOneToOnePrimaryObject(primaryObject, oneToOneRelationKey, this.ChangeContainer, this.Context, requester);
		}

		//internal void RemoveOneToOneRelationPrimaryObjectFromCache(SimpleObject? primaryObject, int oneToOneRelationKey)
		//{
		//	if (primaryObject != null)
		//		primaryObject.RemoveOneToOneRelationForeignObjectFromCache(oneToOneRelationKey);

		//	this.OneToOneForeignObjectKeysByRelationKey.Remove(oneToOneRelationKey);
		//}

		public bool SetOneToOnePrimaryObject(SimpleObject? primaryObject, int oneToOneRelationKey, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			//OneToOneRelationModel oneToOneRelationModel;
			//RelationPolicyModelBase.Instance.OneToOneRelations.TryGetValue(oneToOneRelationKey, out oneToOneRelationModel);

			IOneToOneRelationModel oneToOneRelationModel = this.GetModel().RelationModel.AsForeignObjectInOneToOneRelations[oneToOneRelationKey];

			if (oneToOneRelationModel == null)
			{
#if DEBUG
				throw new ArgumentException("GetOneToOneForeignObject: There is no relation model definition, RelationKey=" + oneToOneRelationKey);
#else
				return false;
#endif
			}

			return this.SetOneToOnePrimaryObject(primaryObject, oneToOneRelationModel, changeContainer, context, requester);
		}

		protected bool SetOneToOnePrimaryObject(SimpleObject? primaryObject, IOneToOneRelationModel oneToOneRelationModel)
		{
			return this.SetOneToOnePrimaryObject(primaryObject, oneToOneRelationModel, requester: this.Requester);
		}

		protected bool SetOneToOnePrimaryObject(SimpleObject? primaryObject, IOneToOneRelationModel oneToOneRelationModel, object? requester)
		{
			return this.SetOneToOnePrimaryObject(primaryObject, oneToOneRelationModel, this.ChangeContainer, this.Context, requester);
		}

		protected bool SetOneToOnePrimaryObject(SimpleObject? primaryObject, IOneToOneRelationModel oneToOneRelationModel, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			//this.ObjectRelationCache.SetOneToOneRelationForeignObject(requester, oneToOneRelationModel, foreignObject);
		
            //if (this.GetModel().RelationModel.AsForeignObjectInOneToOneRelations.ContainsKey(oneToOneRelationModel.RelationKey)) // ObjectRelationModel.OneToOneRelationForeignKeyObjectDictionary.ContainsKey(oneToOneRelationModel.RelationKey))
            //{
			bool cancel = false;
            SimpleObject? oldPrimaryObject = this.GetOneToOnePrimaryObject(oneToOneRelationModel.RelationKey); //OneToOneForeignObjectsByRelationModelKey[oneToOneRelationModel.Key] as SimpleObject;

			if (primaryObject == oldPrimaryObject)
				return true;

			this.OnBeforeRelationPrimaryObjectSet(primaryObject, oldPrimaryObject, oneToOneRelationModel, ref cancel, changeContainer, context, requester);

			if (cancel)
				return false;

			long primaryObjectId = (primaryObject != null) ? primaryObject.Id : 0; // oneToOneRelationModel.ForeignIdPropertyModel.PropertyType.GetDefaultValue();
			SimpleObject? oldPrimaryObjectRelatedForeignObject = (primaryObject != null) ? primaryObject.GetOneToOneForeignObject(oneToOneRelationModel.RelationKey) : null;

			// Set Owner's property value(s) (foreign key)
			if (oneToOneRelationModel.PrimaryTableIdPropertyModel != null)
			{
				int primaryObjectTableId = (primaryObject != null) ? primaryObject.GetModel().TableInfo.TableId : 0; // oneToOneRelationModel.ForeignTableIdPropertyModel.PropertyType.GetDefaultValue(); // oneToOneRelationModel.ForeignObjectTableId;
				this.SetPropertyValueInternal(oneToOneRelationModel.PrimaryTableIdPropertyModel, primaryObjectTableId, changeContainer, context, requester);
				// this.SetPropertyValueInternal(oneToOneRelationModel.ForeignTableIdPropertyModel, relatedObjectKey.GetTableId(), true, false, this);
			}

            // Using SetPropertyValueInternal to avoid read only and similar checkig and throwing an exception, for standard SetPropertyValue method.
			//this.Owner.SetPropertyValueInternal(oneToOneRelationModel.ForeignObjectCreatorServerIdPropertyName, relatedObjectKey.CreatorServerId, false, true, this.Owner);
            //this.SetPropertyValueInternal(oneToOneRelationModel.ForeignKeyPropertyModel, relatedObjectKey, true, false, this);
			this.SetPropertyValueInternal(oneToOneRelationModel.PrimaryObjectIdPropertyModel, primaryObjectId, changeContainer, requester);

			// Set related object relation value change and add value to the local hash if required or remove from hash if is null.
			if (primaryObject != null)
				primaryObject.SetOneToOneForeignObjectInternal(oneToOneRelationModel.RelationKey, this);
				////this.OneToOnePrimaryObjectsByRelationKey[oneToOneRelationModel.RelationKey] = primaryObject;
    ////           }
    ////           else
    ////           {
    ////               this.OneToOnePrimaryObjectsByRelationKey.Remove(oneToOneRelationModel.RelationKey);
    ////           }

            // Reset old related object relation value
            if (oldPrimaryObject != null)
                oldPrimaryObject.SetOneToOneForeignObjectInternal(oneToOneRelationModel.RelationKey, null);

            // Remove oldForeignSimpleObject relation cache
            if (oldPrimaryObjectRelatedForeignObject != null)
            {
                oldPrimaryObjectRelatedForeignObject.SetOneToOnePrimaryObject(primaryObject: null, oneToOneRelationModel, changeContainer, context, requester);
					
				//if (changeContainer != null)
				//	changeContainer.Set(oldForeignObjectRelatedKeyHolder, TransactionRequestAction.Save, requester);
				////oldForeignSimpleObjectRelatedKeyHolder.Save();
            }

			this.OnRelationForeignObjectSet(primaryObject, oldPrimaryObject, oneToOneRelationModel, changeContainer, context, requester);

			if (!this.isPropertyInitialization)
				this.Manager.RelationForeignObjectIsSet(this, primaryObject, oldPrimaryObject, oneToOneRelationModel, changeContainer, context, requester);
            //}
            //else
            //{
            //    throw new ArgumentException("SetOneToOneRelationForeignObject: There is no relation model definition.");
            //}

			return true;
		}

		public bool SetOneToOneForeignObject(SimpleObject? foreignObject, int oneToOneRelationKey)
		{
			return this.SetOneToOneForeignObject(foreignObject, oneToOneRelationKey, this.Requester);
		}

		public bool SetOneToOneForeignObject(SimpleObject? foreignObject, int oneToOneRelationKey, object? requester)
		{
			//OneToOneRelationModel oneToOneRelationModel;
			//RelationPolicyModelBase.Instance.OneToOneRelations.TryGetValue(oneToOneRelationKey, out oneToOneRelationModel);

			IOneToOneRelationModel oneToOneRelationModel = this.GetModel().RelationModel.AsPrimaryObjectInOneToOneRelations[oneToOneRelationKey];

			if (oneToOneRelationModel == null)
			{
#if DEBUG
				throw new ArgumentException("SetOneToOneForeignObject: There is no relation model definition, RelationKey=" + oneToOneRelationKey);
#else
				return false;
#endif
			}

			return this.SetOneToOneForeignObject(foreignObject, oneToOneRelationModel, this.Context, requester);
		}

		protected bool SetOneToOneForeignObject(SimpleObject? foreignObject, IOneToOneRelationModel oneToOneRelationModel)
		{
			return this.SetOneToOneForeignObject(foreignObject, oneToOneRelationModel, this.Requester);
		}

		protected bool SetOneToOneForeignObject(SimpleObject? foreignObject, IOneToOneRelationModel oneToOneRelationModel, object? requestert)
		{
			return this.SetOneToOneForeignObject(foreignObject, oneToOneRelationModel, this.Context, requester);
		}

		protected bool SetOneToOneForeignObject(SimpleObject? foreignObject, IOneToOneRelationModel oneToOneRelationModel, ObjectActionContext context)
		{
			return this.SetOneToOneForeignObject(foreignObject, oneToOneRelationModel, context, this.Requester);
		}

		protected bool SetOneToOneForeignObject(SimpleObject? foreignObject, IOneToOneRelationModel oneToOneRelationModel, ObjectActionContext context, object? requester)
		{
			return this.SetOneToOneForeignObject(foreignObject, oneToOneRelationModel, this.ChangeContainer, context, requester);
		}

		protected bool SetOneToOneForeignObject(SimpleObject? foreignObject, IOneToOneRelationModel oneToOneRelationModel, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			//this.ObjectRelationCache.SetOneToOneRelationKeyHolderObject(requester, oneToOneRelationModel, keyHolderObject);
			bool succeed = false;
			SimpleObject? oldForeignObject = this.GetOneToOneForeignObject(oneToOneRelationModel.RelationKey);

			if (foreignObject == oldForeignObject)
				return false;

			if (oldForeignObject != null)
			{
				succeed = oldForeignObject.SetOneToOnePrimaryObject(primaryObject: null, oneToOneRelationModel, changeContainer, context, requester);

				//if (succeed && changeContainer != null)
				//	changeContainer.Set(oldForeignKeyHolderSimpleObject, TransactionRequestAction.Save, requester);

				//oldForeignKeyHolderSimpleObject.Save();
			}

			//if (succeed && keyHolderObject != null)
			if (foreignObject != null)
			{
				succeed = foreignObject.SetOneToOnePrimaryObject(primaryObject: this, oneToOneRelationModel, changeContainer, context, requester);

				if (!succeed && oldForeignObject != null)
					oldForeignObject.SetOneToOnePrimaryObject(primaryObject: this, oneToOneRelationModel, changeContainer, context, requester);
			}

			return succeed;
		}

		#endregion |   Public One-To-One Relation Methods   |

		#region |   Public One-To-Many Relation Methods   |

		public SimpleObject? GetOneToManyPrimaryObject(int oneToManyRelationKey)
		{
			//SimpleObject result = null;
			//OneToManyRelationModel oneToManyRelationModel;
			//RelationPolicyModelBase.Instance.OneToManyRelations.TryGetValue(oneToManyRelationKey, out oneToManyRelationModel);

			IOneToManyRelationModel oneToManyRelationModel = this.GetModel().RelationModel.AsForeignObjectInOneToManyRelations[oneToManyRelationKey];

			if (oneToManyRelationModel == null)
			{
#if DEBUG
				throw new ArgumentException("GetOneToManyPrimaryObject: There is no relation model definition, RelationKey=" + oneToManyRelationKey);
#else
				return null;
#endif
			}

			return this.GetOneToManyPrimaryObject(oneToManyRelationModel);
		}

		protected SimpleObject? GetOneToManyPrimaryObject(IOneToManyRelationModel oneToManyRelationModel)
		{
			//return this.ObjectRelationCache.GetOneToManyRelationForeignObject<T>(oneToManyRelationModel);

            SimpleObject? value = null;

            //if (!this.OneToManyPrimaryObjectsByRelationKey.TryGetValue(oneToManyRelationModel.RelationKey, out value))
            //{
                //if (this.GetModel().RelationModel.AsForeignObjectInOneToManyRelations.ContainsKey(oneToManyRelationModel.RelationKey)) // ObjectRelationModel.OneToManyRelationForeignKeyObjectDictionary.ContainsKey(oneToManyRelationModel.RelationKey))
                //{
                    //int foreignObjectTableId;
                    //Guid foreignObjectGuid;
                    //SimpleObjectKey foreignObjectKey;

                    //if (oneToManyRelationModel.ForeignTableIdPropertyModel != null)
                    //{
                    //    foreignObjectTableId = this.GetPropertyValue<int>(oneToManyRelationModel.ForeignTableIdPropertyModel.Index);
                    //}
                    //else
                    //{
                    //    ISimpleObjectModel relatedObjectModel = this.Manager.GetObjectModel(oneToManyRelationModel.ForeignObjectType);
                    //    foreignObjectTableId = relatedObjectModel.TableId;
                    //}

                    //Guid? foreignObjectKey = this.GetPropertyValue<Guid?>(oneToManyRelationModel.ForeignKeyPropertyModel.Index);
					int primaryTableId = (oneToManyRelationModel.PrimaryTableIdPropertyModel != null) ? this.GetPropertyValue<int>(oneToManyRelationModel.PrimaryTableIdPropertyModel.PropertyIndex) : oneToManyRelationModel.PrimaryObjectTableId;
					long? primaryObjectId = this.GetPropertyValue<long?>(oneToManyRelationModel.PrimaryObjectIdPropertyModel);

					//value = this.ObjectManager.GetObject(foreignObjectKey);
					
					if (primaryObjectId != null)		
						value = this.Manager.GetObject(primaryTableId, (long)primaryObjectId);

                    //if (value != null)
                    //{
                        //lock (lockObject)
                        //{
                        //this.OneToManyPrimaryObjectsByRelationKey[oneToManyRelationModel.RelationKey] = value;
                        //}
                    //}
     //           }
     //           else
     //           {
					//return null;
					////throw new ArgumentException("GetOneToManyRelationForeignObject: There is no relation model definition.");
     //           }
            //}

            return value;
		}

		protected T? GetOneToManyPrimaryObject<T>(IOneToManyRelationModel oneToManyRelationModel) where T : SimpleObject //, IUser
		{
			return (T?)this.GetOneToManyPrimaryObject(oneToManyRelationModel);
		}


		public bool SetOneToManyPrimaryObject(SimpleObject? primaryObject, int oneToManyRelationKey)
		{
			return this.SetOneToManyPrimaryObject(primaryObject, oneToManyRelationKey, this.Requester);
		}

		public bool SetOneToManyPrimaryObject(SimpleObject? primaryObject, int oneToManyRelationKey, object? requester)
		{
			return this.SetOneToManyPrimaryObject(primaryObject, oneToManyRelationKey, this.Context, requester);
		}

		public bool SetOneToManyPrimaryObject(SimpleObject? primaryObject, int oneToManyRelationKey, ObjectActionContext context)
		{
			return this.SetOneToManyPrimaryObject(primaryObject, oneToManyRelationKey, context, requester: this.Requester);
		}

		public bool SetOneToManyPrimaryObject(SimpleObject? primaryObject, int oneToManyRelationKey, ObjectActionContext context, object? requester)
		{
			return this.SetOneToManyPrimaryObject(primaryObject, oneToManyRelationKey, this.ChangeContainer, context, requester);
		}

		public bool SetOneToManyPrimaryObject(SimpleObject? primaryObject, int oneToManyRelationKey, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			//OneToManyRelationModel oneToManyRelationModel;
			//RelationPolicyModelBase.Instance.OneToManyRelations.TryGetValue(oneToManyRelationKey, out oneToManyRelationModel);

			IOneToManyRelationModel oneToManyRelationModel = this.GetModel().RelationModel.AsForeignObjectInOneToManyRelations[oneToManyRelationKey];

			if (oneToManyRelationModel == null)
			{
#if DEBUG
				throw new ArgumentException("SetOneToManyRelationPrimaryObject: There is no relation model definition, RelationKey=" + oneToManyRelationKey);
#else
				return false;
#endif
			}

			return this.SetOneToManyPrimaryObject(primaryObject, oneToManyRelationModel, changeContainer, context, requester);
		}

		protected bool SetOneToManyPrimaryObject(SimpleObject? primaryObject, IOneToManyRelationModel oneToManyRelationModel)
		{
			return this.SetOneToManyPrimaryObject(primaryObject, oneToManyRelationModel, this.Requester);
		}

		protected bool SetOneToManyPrimaryObject(SimpleObject? primaryObject, IOneToManyRelationModel oneToManyRelationModel, object? requester)
		{
			return this.SetOneToManyPrimaryObject(primaryObject, oneToManyRelationModel, this.Context, requester);
		}

		protected bool SetOneToManyPrimaryObject(SimpleObject? primaryObject, IOneToManyRelationModel oneToManyRelationModel, ObjectActionContext context)
		{
			return this.SetOneToManyPrimaryObject(primaryObject, oneToManyRelationModel, context, this.Requester);
		}

		protected bool SetOneToManyPrimaryObject(SimpleObject? primaryObject, IOneToManyRelationModel oneToManyRelationModel, ObjectActionContext context, object? requester)
		{
			return this.SetOneToManyPrimaryObject(primaryObject, oneToManyRelationModel, this.ChangeContainer, context, requester);
		}

		protected bool SetOneToManyPrimaryObject(SimpleObject? primaryObject, IOneToManyRelationModel oneToManyRelationModel, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			//return this.SetOneToManyPrimaryObject(primaryObject, oneToManyRelationModel, addOrRemoveInChangedPropertyNames: true, firePropertyValueChangeEvent: true, raiseForeignObjectSetEvent: true, changeContainer, context, requester);
			return this.SetOneToManyPrimaryObject(primaryObject, oneToManyRelationModel, raiseForeignObjectSetEvent: true, changeContainer, context, requester);
		}


		/// <summary>
		/// Gets the Null Collection with elements whan primary object is null when foreign key allows to be null (0). For e.g. whent Parent GraphElement is null, this is null collection is root GraphElement collection where Parent is null (ParentId = 0)
		/// </summary>
		/// <param name="relationKey">Specify relation by relation key</param>
		/// <returns>Collection of null SimpleObjects</returns>
		protected virtual SimpleObjectCollection? GetOneToManyForeignNullCollection(int relationKey) => null;

		//protected bool SetOneToManyPrimaryObject(SimpleObject? primaryObject, IOneToManyRelationModel oneToManyRelationModel, bool addOrRemoveInChangedPropertyNames, bool firePropertyValueChangeEvent, bool raiseForeignObjectSetEvent, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		protected bool SetOneToManyPrimaryObject(SimpleObject? primaryObject, IOneToManyRelationModel oneToManyRelationModel, bool raiseForeignObjectSetEvent, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			//if (this.GetModel().RelationModel.AsForeignObjectInOneToManyRelations.ContainsKey(oneToManyRelationModel.RelationKey)) //  ObjectRelationModel.OneToManyRelationForeignKeyObjectDictionary.ContainsKey(oneToManyRelationModel.RelationKey))
			//{
			bool cancel = false;
			SimpleObject? oldPrimaryObject = this.GetOneToManyPrimaryObject(oneToManyRelationModel);  //OneToManyForeignObjectsByRelationModelKey[oneToManyRelationModel.Key] as SimpleObject;
																									 //bool isSortableCollection = true; // (this.GetModel().SortableOneToManyRelationKey == oneToManyRelationModel.RelationKey && this.GetModel().IsSortable);
			//
			// Is this needed any more ???????????????????????????
			//
			if (primaryObject == oldPrimaryObject)
			{
				// if object is new, check if needed to add it to the null collection 
				if (primaryObject == null && !this.DeleteStarted)
				{
					this.GetOneToManyForeignNullCollection(oneToManyRelationModel.RelationKey)?.Add(this, changeContainer, requester);

					//SimpleObjectCollection? oneToManyForeignNullCollection = this.GetOneToManyForeignNullCollection(oneToManyRelationModel.RelationKey);

					//if (oneToManyForeignNullCollection != null && !oneToManyForeignNullCollection.Contains(this))
					//	oneToManyForeignNullCollection.Add(this, changeContainer, requester);
				}

				return true;
			}

			//if (!this.IsNew && foreignObject == oldForeignObject)
				//{
				//	if (this.deleteStarted && foreignObject == null) // Remove from Null collection on delete
				//	{
				//		ISimpleObjectCollection nullOneToManyCollection = this.GetOneToManyNullCollection(oneToManyRelationModel.RelationKey) as ISimpleObjectCollection;

				//		if (nullOneToManyCollection != null)
				//			nullOneToManyCollection.ListRemove(this);
				//	}

				//	return false;
				//}

			this.OnBeforeRelationPrimaryObjectSet(primaryObject, oldPrimaryObject, oneToManyRelationModel, ref cancel, changeContainer, context, requester);

			if (cancel)
				return false;

			// Remove from old related object collection
			if (oldPrimaryObject != null)
			{
				// First get the sorting collection and removr from if, otherwise if oldPrimaryObject.RemoveFromOneToManyForeignCollectionIfInCache(oneToManyRelationModel.RelationKey, this);
				// is called first, if collection is not cached from foreign object, sortableSimpleObject.GetSortingCollectionInternal method will collect this object that will be removed and becom null.
				//
				if (this is SortableSimpleObject sortableSimpleObject && this.GetModel().SortableOneToManyRelationKey == oneToManyRelationModel.RelationKey) 
				{
					sortableSimpleObject.GetSortingCollectionInternal()?.Remove(this, changeContainer, requester);

					//SimpleObjectCollection ? sortableCollection = sortableSimpleObject.GetSortingCollectionInternal();

					//if (sortableCollection != null && this.GetModel().SortableOneToManyRelationKey == oneToManyRelationModel.RelationKey && 
					//								  sortableCollection.Contains(sortableSimpleObject) && !this.DeleteStarted)
					//	sortableCollection.Remove(this, changeContainer, requester);
				}

				oldPrimaryObject.RemoveOneToManyForeignCollectionIfInCache(oneToManyRelationModel.RelationKey, this, changeContainer, requester);
			}
			else //if (!this.DeleteStarted) // && !this.IsNew) // RemoveAllRelatedObjectsFromAllRelatedObjectCaches handling null collection removal
			{
				this.GetOneToManyForeignNullCollection(oneToManyRelationModel.RelationKey)?.Remove(this, changeContainer, requester);

				//SimpleObjectCollection? oneToManyForeignNullCollection = this.GetOneToManyForeignNullCollection(oneToManyRelationModel.RelationKey);

				//if (oneToManyForeignNullCollection != null)
				//{
				//	int index = oneToManyForeignNullCollection.IndexOf(this.GetModel().TableInfo.TableId, this.Id);
					
				//	if (index >= 0)
				//		oneToManyForeignNullCollection.RemoveAt(index, changeContainer, requester);
				//}
			}

			// Set Owner's property value(s) (foreign key)
			//Guid? foreignObjectKey = (foreignObject != null) ? (Guid?)foreignObject.Guid : null;
			long foreignObjectId = (primaryObject != null) ? primaryObject.Id : 0; // oneToManyRelationModel.ForeignIdPropertyModel.PropertyType.GetDefaultValue();

			if (oneToManyRelationModel.PrimaryTableIdPropertyModel != null) // No PropertyValueChange event on relation TableId property change, only on relation Guid
			{
				int foreignTableId = (primaryObject != null) ? primaryObject.GetModel().TableInfo.TableId : 0; // oneToManyRelationModel.ForeignTableIdPropertyModel.PropertyType.GetDefaultValue(); // oneToManyRelationModel.ForeignObjectTableId;
				this.SetPropertyValueInternal(oneToManyRelationModel.PrimaryTableIdPropertyModel, foreignTableId, changeContainer, context, requester);
			}

			//this.OneToManyPrimaryObjectsByRelationKey[oneToManyRelationModel.RelationKey] = primaryObject;
			this.SetPropertyValueInternal(oneToManyRelationModel.PrimaryObjectIdPropertyModel, foreignObjectId, changeContainer, context, requester);
				
			// Add to related object collection and to the local hash if required or remove from hash if is null.
			if (primaryObject != null)
			{
				primaryObject.AddToOneToManyForeignCollectionIfInCache(oneToManyRelationModel.RelationKey, this, changeContainer, requester);

				if (this is SortableSimpleObject sortableSimpleObject && this.GetModel().SortableOneToManyRelationKey == oneToManyRelationModel.RelationKey)
				{
					sortableSimpleObject.GetSortingCollectionInternal()?.Add(this, changeContainer, requester);

					//if (sortablegCollection != null && this.GetModel().SortableOneToManyRelationKey == oneToManyRelationModel.RelationKey && 
					//								   !sortablegCollection.Contains(sortableSimpleObject) && !this.DeleteStarted) // If delete in progress OnBeforeDeleting at SortableSimpleObject will set ordering in a collection
					//	sortablegCollection.Add(this, changeContainer, requester);
				}

				//if (isSortableCollection)
				//	(this.Owner as SortableSimpleObject).SetOrderingAfterInsertingIntoCollection(requester, (this.Owner as SortableSimpleObject).GetSortingCollection());
			}
			else if (!this.DeleteStarted) // When DeleteStarted the object is removing from any collection
			{
				this.GetOneToManyForeignNullCollection(oneToManyRelationModel.RelationKey)?.Add(this, changeContainer, requester);

				//SimpleObjectCollection? oneToManyForeignNullCollection = this.GetOneToManyForeignNullCollection(oneToManyRelationModel.RelationKey);

				//if (oneToManyForeignNullCollection != null && !oneToManyForeignNullCollection.Contains(this))
				//	oneToManyForeignNullCollection.Add(this, changeContainer, requester);

				////this.OneToManyPrimaryObjectsByRelationKey.Remove(oneToManyRelationModel.RelationKey);
			}

			//if (isSortableCollection && !this.DeleteStarted) // && foreignObject != null // If delete in progress OnBeforeDeleting at SortableSimpleObject will set ordering in a collection
			//{
			//	IList<SortableSimpleObject> sortablegCollection = (this is SortableSimpleObject) ? (this as SortableSimpleObject).GetSortingCollection(sort: true) : null;

			//	if (sortablegCollection != null && sortablegCollection.Contains(this as SortableSimpleObject))
			//	{
			//		this.OnBeforeSetOrderingAfterOneToManyRelationForeignObjectSet(foreignObject, oldForeignObject, oneToManyRelationModel, requester);
			//		(this as SortableSimpleObject).SetOrderingAfterInsertingIntoCollection(sortablegCollection, changeContainer, requester);
			//	}
			//}

			this.OnRelationForeignObjectSet(primaryObject, oldPrimaryObject, oneToManyRelationModel, changeContainer, context, requester);

			if (raiseForeignObjectSetEvent && !this.isPropertyInitialization)
				this.Manager.RelationForeignObjectIsSet(this, primaryObject, oldPrimaryObject, oneToManyRelationModel, changeContainer, context, requester);
			//}
			//else
			//{
			//	throw new ArgumentException("SetOneToManyRelationForeignObject: There is no relation model definition.");
			//}

			return true;
		}

		/// <summary>
		/// Override this method to specify One-To-Many foreign collection by relation key.
		/// </summary>
		/// <param name="relationKey">The relation key</param>
		/// <returns></returns>
		public virtual SimpleObjectCollection? GetOneToManyForeignObjectCollection(int relationKey) => null;
		//{
		//	var realtionModel = this.Manager.ModelDiscovery.GetOneToManyRelationModel(relationKey);

		//	return this.GetOneToManyForeignObjectCollection(realtionModel!);
		//}

		protected SimpleObjectCollection<T> GetOneToManyForeignObjectCollection<T>(IOneToManyRelationModel oneToManyRelationModel) 
			where T : SimpleObject
		{
			SimpleObjectCollection? value;
			SimpleObjectCollection<T>? result = null;

			//lock (this.lockObject)
			//{
				if (this.OneToManyForeignCollectionsByRelationKey.TryGetValue(oneToManyRelationModel.RelationKey, out value))
				{
					result = value as SimpleObjectCollection<T>;
#if DEBUG
					if (result == null)
						throw new ArgumentException(String.Format("GetOneToManyCollection error: The result object collection is not castable from {0} to {1} type (RelationKey={2}.", value.GetElementType().Name, typeof(T).Name, oneToManyRelationModel.RelationKey));
#endif
					return result;
				}

				ObjectCache foreignObjectCache = this.Manager.GetObjectCache(oneToManyRelationModel.ForeignObjectTableId)!;
				IList<long> objectIds = foreignObjectCache.GetOneToManyForeignObjectIds(primaryTableId: this.GetModel().TableInfo.TableId, primaryObjectId: this.Id, oneToManyRelationModel);
				bool sortCollection = this.Manager.GetObjectModel(oneToManyRelationModel.ForeignObjectTableId)?.SortableOneToManyRelationKey == oneToManyRelationModel.RelationKey;

//				if (this.Manager.WorkingMode == ObjectManagerWorkingMode.Client && this.id > 0)
//				{
//					objectIds = this.Manager.RemoteDatastore!.GetOneToManyForeignObjectCollection(this.GetModel().TableInfo.TableId, this.Id, oneToManyRelationModel.RelationKey).GetAwaiter().GetResult();
//					sortCollection = this.Manager.GetObjectModel(oneToManyRelationModel.ForeignObjectTableId)?.SortableOneToManyRelationKey == oneToManyRelationModel.RelationKey;
//				}
//				else
//				{
//#if DEBUG			
//					if (!this.GetModel().RelationModel.AsPrimaryObjectInOneToManyRelations.ContainsKey(oneToManyRelationModel.RelationKey))
//						throw new ArgumentException(String.Format("GetOneToManyCollection error: The relation key {0} is not specified in a relation model for object {1}, Id={2}", oneToManyRelationModel.RelationKey, this.GetType().Name, this.Id));
//#endif
//					ObjectCache foreignObjectCache = this.Manager.GetObjectCache(oneToManyRelationModel.ForeignObjectTableId)!;
					
//					sortCollection = foreignObjectCache.ObjectModel.SortableOneToManyRelationKey == oneToManyRelationModel.RelationKey;
//					//var foreignObjectModel = this.Manager.GetObjectModel(oneToManyRelationModel.ForeignObjectTableId);
//					//bool sortCollection = typeof(T).IsSubclassOf(typeof(SortableSimpleObject));
//					//(bool sortCollection = (foreignObjectModel.SortingModel == SortingModel.ByOneToManyRelationKey) ? foreignObjectModel.SortableOneToManyRelationKey == oneToManyRelationModel.RelationKey :
//					//																								 foreignObjectModel.SortingModel == SortingModel.BySingleObjectTypeCollection;

//					objectIds = foreignObjectCache.GetOneToManyForeignObjectIds(primaryTableId: this.GetModel().TableInfo.TableId, primaryObjectId: this.Id, oneToManyRelationModel);

////					//Predicate<SimpleObject> match;

////					if (oneToManyRelationModel.PrimaryTableIdPropertyModel == null)
////					{
////						//match = new Predicate<SimpleObject>(foreignObject => !foreignObject.DeleteStarted && this.Id.Equals(foreignObject.GetPropertyValue(oneToManyRelationModel.PrimaryKeyObjectIdPropertyModel.Index)));
////						//match = new Predicate<SimpleObject>(foreignObject => this.Id.Equals(foreignObject.GetPropertyValue(oneToManyRelationModel.PrimaryObjectIdPropertyModel.PropertyIndex)));
////						collectionIds = foreignObjectCache.GetRelationCollectionObjectIds(oneToManyRelationModel.PrimaryObjectIdPropertyModel.PropertyIndex, this.Id);
////					}
////					else
////					{
////						//match = new Predicate<SimpleObject>(foreignObject => !foreignObject.DeleteStarted && this.Id.Equals(foreignObject.GetPropertyValue(oneToManyRelationModel.PrimaryKeyObjectIdPropertyModel.Index)) &&
////						//																					 foreignKeyObjectTableId.Equals(foreignObject.GetPropertyValue(oneToManyRelationModel.PrimaryKeyTableIdPropertyModel.Index)));
////						//int primaryObjectTableId = this.Manager.GetObjectModel(typeof(T)).TableInfo.TableId;
////						int primaryObjectTableId = this.GetModel().TableInfo.TableId;
////#if DEBUG
////						if (oneToManyRelationModel.PrimaryObjectTableId > 0 && primaryObjectTableId != oneToManyRelationModel.PrimaryObjectTableId)
////							throw new ArgumentException(String.Format("GetOneToManyCollection error: oneToManyRelationModel.PrimaryObjectTableId is not the same as T object TableId"));
////#endif
////						//match = new Predicate<SimpleObject>(foreignObject => primaryObjectTableId.Equals(foreignObject.GetPropertyValue(oneToManyRelationModel.PrimaryTableIdPropertyModel.PropertyIndex)) &&
////						//													 this.Id.Equals(foreignObject.GetPropertyValue(oneToManyRelationModel.PrimaryObjectIdPropertyModel.PropertyIndex)));
////						collectionIds = foreignObjectCache.GetRelationCollectionObjectIds(oneToManyRelationModel.PrimaryTableIdPropertyModel.PropertyIndex, primaryObjectTableId, oneToManyRelationModel.PrimaryObjectIdPropertyModel.PropertyIndex, this.Id);
////					}

//					// TODO: Check what if different type of object may exists in a collection  
//					//result = foreignObjectCache.Select<T>(match, sortCollection);
//				}

				result = new SimpleObjectCollection<T>(this.Manager, oneToManyRelationModel.ForeignObjectTableId, objectIds);


				if (sortCollection)
				{
					bool requireSave = this.Manager.WorkingMode != ObjectManagerWorkingMode.Client && this.ChangeContainer != null && !this.ChangeContainer.RequireCommit && this.ChangeContainer.RequestCount == 0;
					
					result.Sort(setPrevious: true, this.ChangeContainer, this.Requester);
					
					
					if (requireSave && this.ChangeContainer!.RequireCommit)
						this.Manager.CommitChanges(this.ChangeContainer, this.Context, this.Requester);
				}

				this.OneToManyForeignCollectionsByRelationKey.Add(oneToManyRelationModel.RelationKey, result);


				return result;
			//}
		}

		//public SimpleObjectCollection<T> GetOneToManyRelationKeyHolderObjectCollection<T>(IOneToManyRelationModel oneToManyRelationModel) where T : SimpleObject
		//{
		//	return this.GetOneToManyRelationKeyHolderObjectCollection<T>(oneToManyRelationModel, sortIfSortable: true, getOriginalList: true);
		//}

		//public virtual SimpleObjectCollection<T> GetOneToManyRelationKeyHolderObjectCollection<T>(IOneToManyRelationModel oneToManyRelationModel, bool sortIfSortable, bool getOriginalList) where T : SimpleObject
		//{
		//	//return this.ObjectRelationCache.(<T>(oneToManyRelationModel, sortIfSortable);

		//	ISimpleObjectCollection value;
		//	SimpleObjectCollection<T> result = null;
		//	//bool isSorted = false;
		//	//bool requireSorting = false;
		//	//bool generateNewKeyArray = true;

		//	if (oneToManyRelationModel.RelationKey == 3 && (this is Graph) && (this as Graph).GraphKey == 1)
		//	{
		//		result = result;
		//		//result = null;
		//		////this.OneToManyForeignKeyHolderObjectCollectionsByRelationKey[oneToManyRelationModel.RelationKey] = result;
		//	}

		//	if (oneToManyRelationModel.RelationKey == 7)
		//	{
		//		result = result;
		//		//result = null;
		//	}

		//	result = this.GetOneToManyForeignCollection<T>(oneToManyRelationModel);

		//	return result;

		//	if (!this.OneToManyCollectionsByRelationKey.TryGetValue(oneToManyRelationModel.RelationKey, out value))
		//	{
		//		if (this.GetModel().RelationModel.OneToManyKeyHolderObjects.ContainsKey(oneToManyRelationModel.RelationKey)) // ObjectRelationModel.OneToManyRelationKeyHolderObjectDictionary.ContainsKey(oneToManyRelationModel.RelationKey))
		//		{
		//			ObjectCache simpleObjectTypeCache = null;

		//			if (oneToManyRelationModel.ForeignKeyObjectTableId > 0)
		//			{
		//				simpleObjectTypeCache = this.Manager.GetObjectCache(oneToManyRelationModel.ForeignKeyObjectTableId);
		//			}
		//			else
		//			{
		//				Type cacheType = (typeof(T).IsSubclassOf(oneToManyRelationModel.ForeignKeyObjectType)) ? typeof(T) : oneToManyRelationModel.ForeignKeyObjectType;
		//				simpleObjectTypeCache = this.Manager.GetObjectCache(cacheType);
		//			}

		//			// TODO: revidirati što činiti kada imamo u relaciji različite tipove objekata npr. NetworkObject, pa to može biti Network, NetworkNode, VLAN, itd.
		//			//       Tada ne možemo samo potegnuti GetSingleObjectTypeCache ili GetObjectCollection<T>(match)...




		//			//matchCriteria.Add(oneToManyRelationModel.ForeignObjectCreatorServerIdPropertyName, this.Owner.Key.CreatorServerId);
		//			//string foreignGuidPropertyName = this.GetModel().PropertyModels[oneToManyRelationModel.ForeignGuidPropertyIndex].Name;

		//			//if (oneToManyRelationModel.ForeignTableIdPropertyModel != null)
		//			//{
		//			//	match = new Predicate<SimpleObject>(bo => !bo.IsDeleteStarted && this.Key.Guid.Equals(bo.GetPropertyValue(oneToManyRelationModel.ForeignGuidPropertyModel.Index)) &&
		//			//												this.Key.TableId.Equals(bo.GetPropertyValue(oneToManyRelationModel.ForeignTableIdPropertyModel.Index)));
		//			//	//this.Owner.Key.CreatorServerId.Equals(o.GetPropertyValue(oneToManyRelationModel.ForeignObjectCreatorServerIdPropertyName)));
		//			//	//string foreignTableIdPropertyName = this.GetModel().PropertyModels[oneToManyRelationModel.ForeignTableIdPropertyIndex].Name;
		//			//	matchCriteria.Add(oneToManyRelationModel.ForeignTableIdPropertyModel.Name, this.Key.TableId);
		//			//}
		//			//else
		//			//{
		//			//this.Owner.Key.CreatorServerId.Equals(o.GetPropertyValue(oneToManyRelationModel.ForeignObjectCreatorServerIdPropertyName)));
		//			//}


		//			Predicate<SimpleObject> match;

		//			if (oneToManyRelationModel.PrimaryKeyTableIdPropertyModel != null)
		//			{
		//				int tableId = this.GetModel().TableInfo.TableId;

		//				match = new Predicate<SimpleObject>(keyHolderObject => !keyHolderObject.DeleteStarted && this.Id.Equals(keyHolderObject.GetPropertyValue(oneToManyRelationModel.PrimaryKeyObjectIdPropertyModel.Index)) &&
		//																										 tableId.Equals(keyHolderObject.GetPropertyValue(oneToManyRelationModel.PrimaryKeyTableIdPropertyModel.Index)));
		//			}
		//			else
		//			{
		//				match = new Predicate<SimpleObject>(keyHolderObject => !keyHolderObject.DeleteStarted && this.Id.Equals(keyHolderObject.GetPropertyValue(oneToManyRelationModel.PrimaryKeyObjectIdPropertyModel.Index)));
		//			}

		//			//Predicate<SimpleObject> match = new Predicate<SimpleObject>(so => !so.IsDeleteStarted && this.Guid.Equals(so.GetPropertyValue(oneToManyRelationModel.ForeignKeyPropertyModel.Index)));

		//			if (simpleObjectTypeCache != null)
		//			{
		//				//if (this.IsNew)
		//				//{
		//				result = simpleObjectTypeCache.Select<T>(match);
		//				//}
		//				//else
		//				//{
		//				//	var matchCriteria = new Dictionary<string, object>() { { oneToManyRelationModel.ForeignGuidPropertyModel.Name, this.Key.Guid } };
		//				//	IList<SimpleObjectKey> keyCollection = simpleObjectTypeCache.SelectKeysFromDatastore(matchCriteria);
		//				//	value = new SimpleObjectCollection<T>(this.ObjectManager, keyCollection);
		//				//}
		//			}
		//			//else
		//			//{
		//			//	result = this.Manager.GetObjectCollection<T>(match);
		//			//}

		//				//if (simpleObjectTypeCache != null)
		//				//	value = this.ObjectManager.ObjectCache.GetObjectCollection<T>(match);

		//			if (result != null)
		//			{
		//				//lock (lockObject)
		//				//{
		//				this.OneToManyCollectionsByRelationKey[oneToManyRelationModel.RelationKey] = result;


		//				//}
		//			}
		//			//generateNewKeyArray = false;
		//		}
		//		else
		//		{
		//			return null;
		//			//throw new ArgumentException("GetOneToManyRelationForeignKeyHolderObjectCollection: There is no relation model definition.");
		//		}

		//		if (sortIfSortable && result != null && result.Count > 0 && result[0] != null && result[0].GetSortableRelationKey() == oneToManyRelationModel.RelationKey)
		//			result.SortIfSortable();
		//	}
		//	else 
		//	{
		//		result = value as SimpleObjectCollection<T>;



		//		// Create copy of cache (dictionary) fetched value. No need for sorting while is sorted when added to the dictionary (see line above: result.SortIfSortable();).
		//		if (getOriginalList)
		//		{
		//			result = value as SimpleObjectCollection<T>;

		//			if (result == null) // cannot be custed => create custom wrap
		//				result = new SimpleObjectCollection<T>(this.Manager, new CustomList<T>(value as IList) as IList<T>);
		//		}
		//		else
		//		{
		//			T[] array = new T[value.Count];
		//			value.CopyTo(array, 0);

		//			result = new SimpleObjectCollection<T>(this.Manager, array);
		//		}


		//		// TODO: Check if posible to avoid sorting. When calling GetSortingCollection should return original this.OneToManyForeignKeyHolderObjectCollectionsByRelationKey collection, not a copy of it like above.
		//		// Conclusion: It will not working since this.GetSortingCollection can be overriden and thus return not origin collection but with copy of value within. Thus this result.Sort((s1, s2) => ... should be here.
		//		int check;
		//		//if (sortIfSortable && typeof(T).IsSubclassOf(typeof(SortableSimpleObject)))
		//		//	result.Sort((s1, s2) => (s1 as SortableSimpleObject).OrderIndex.CompareTo((s2 as SortableSimpleObject).OrderIndex));
		//	}

		//	////if (generateNewKeyArray && value != null)
		//	////{
		//	////    List<SimpleObjectKey> newSimpleObjectKeyList = new List<SimpleObjectKey>(value.ObjectKeys);
		//	////    value = new SimpleObjectCollection<T>(this.Owner.Manager, newSimpleObjectKeyList);
		//	////}

		//	//if (!isSorted && sortIfSortable && typeof(T).IsSubclassOf(typeof(SortableSimpleObject)))
		//	//	result.Sort((s1, s2) => (s1 as SortableSimpleObject).OrderIndex.CompareTo((s2 as SortableSimpleObject).OrderIndex));





		//	//int toDo;
		//	//// TODO: Test if new SimpleObjectCollection<T> wrapper is require or is just enough to return value as SimpleObjectCollection<T>!?

		//	//if (value == null)
		//	//	throw new Exception();

		//	//return value as SimpleObjectCollection<T>;

		//	////result = new SimpleObjectCollection<T>(this.ObjectManager, value as IEnumerable); //value as SimpleObjectCollection<T>;





		//	////if (requireSorting && sortIfSortable && result.Count > 0 && result[0] != null && result[0].GetModel().SortableOneToManyRelationKey == oneToManyRelationModel.RelationKey)
		//	////	result.SortIfSortable();

		//	if (result == null)
		//	{
		//		result = result;
		//	}

		//	return result;
		//}

		//protected virtual int GetSortableRelationKey()
		//{
		//	return this.GetModel().SortableOneToManyRelationKey;
		//}


		#endregion |   Public One-To-Many Relation Methods   |

		#region |   Public Many-To-Many Relation Methods   |

		public virtual SimpleObjectCollection? GetGroupMemberCollection(int membershipKey) => null;

		protected SimpleObjectCollection<T> GetGroupMemberCollection<T>(IManyToManyRelationModel manyToManyRelationModel) where T : SimpleObject
		{
			SimpleObjectCollection? value;
			SimpleObjectCollection<T> result;

			//lock (this.lockObject)
			//{
				if (this.GroupMembershipCollectionsByRelationKey.TryGetValue(manyToManyRelationModel.RelationKey, out value))
				{
					result = (value as SimpleObjectCollection<T>)!;
//#if DEBUG
					if (result == null)
					{
						result = value.AsCustom<T>(); // e.g. NetworkGroup has NetworkObject as element types

						if (result == null)
							throw new ArgumentException(String.Format("GetGroupMembershipCollection error: The result object collection is not castable from {0} to {1} type (RelationKey={2}.", value.GetElementType().Name, typeof(T).Name, manyToManyRelationModel.RelationKey));
					}
//#endif
				}
				else
				{
#if DEBUG
					if (!this.GetModel().RelationModel.AsFirstObjectInManyToManyRelations.ContainsKey(manyToManyRelationModel.RelationKey) &&
						!this.GetModel().RelationModel.AsSecondObjectInManyToManyRelations.ContainsKey(manyToManyRelationModel.RelationKey))
						throw new ArgumentException(String.Format("GetGroupMembershipCollection error: The relation key {0} is not specified in a relation model for object {1}, Id={2}", manyToManyRelationModel.RelationKey, this.GetType().Name, this.Id));
#endif
					SimpleObjectCollection<GroupMembership> groupMembershipElements;

					if (this.Manager.WorkingMode == ObjectManagerWorkingMode.Client)
					{
						IList<long> groupMembershipIds = this.Manager.RemoteDatastore!.GetGroupMembershipCollection(this, manyToManyRelationModel.RelationKey).GetAwaiter().GetResult();
						
						groupMembershipElements = new SimpleObjectCollection<GroupMembership>(this.Manager, GroupMembershipModel.TableId, groupMembershipIds);
					}
					else
					{
						ServerObjectCache objectCache = (this.Manager.GetObjectCache(GroupMembershipModel.TableId) as ServerObjectCache)!;

						//if (this.GetType() == manyToManyRelationModel.FirstObjectType)
						//	groupMembershipElements = objectCache.Select<GroupMembership>(gm => gm.RelationKey == manyToManyRelationModel.RelationKey && gm.Object1TableId == this.GetModel().TableInfo.TableId && gm.Object1Id == this.Id);
						//else if (this.GetType() == manyToManyRelationModel.SecondObjectType)
						//	groupMembershipElements = objectCache.Select<GroupMembership>(gm => gm.RelationKey == manyToManyRelationModel.RelationKey && gm.Object2TableId == this.GetModel().TableInfo.TableId && gm.Object2Id == this.Id);
						//else
							groupMembershipElements = objectCache.Select<GroupMembership>(gm =>   gm.RelationKey == manyToManyRelationModel.RelationKey &&
																								((gm.Object1TableId == this.GetModel().TableInfo.TableId && gm.Object1Id == this.Id) ||
																								 (gm.Object2TableId == this.GetModel().TableInfo.TableId && gm.Object2Id == this.Id)));

						//// Check if they sre the same
						//bool isTheSame = true;

						//if (groupMembershipElements.Count != groupMembershipElements2.Count)
						//	isTheSame = false;
						//else
						//	foreach (var item in groupMembershipElements)
						//		if (!groupMembershipElements2.GetObjectIds().Contains(item.id))
						//			throw new Exception("GroupMembership are not the same!");
					}

					List<int> tableIds = new List<int>(groupMembershipElements.Count); // In case that the groupMembershipElements are with different tableId's
					List<long> objectIds = new List<long>(groupMembershipElements.Count);

					foreach (GroupMembership groupMembershipElement in groupMembershipElements)
					{
						if (groupMembershipElement.Object1TableId == this.GetModel().TableInfo.TableId)
						{
							tableIds.Add(groupMembershipElement.Object2TableId);
							objectIds.Add(groupMembershipElement.Object2Id);
						}
						else
						{
							tableIds.Add(groupMembershipElement.Object1TableId);
							objectIds.Add(groupMembershipElement.Object1Id);
						}
					}

					result = new SimpleObjectCollection<T>(this.Manager, tableIds, objectIds);
					result.SetGroupMembershipCollection(groupMembershipElements);

					this.GroupMembershipCollectionsByRelationKey.Add(manyToManyRelationModel.RelationKey, result);
				}
			//}

			return result;
		}

		//// TODO: Create and Generate by the ObjectCodeGenerator GetManyToManyObjectCollection similar to GetOneToManyForeignObjectCollection in one-to-many relations.
		//// SimpleObjectCollection<T> GetManyToManyObjectCollection<T>(int manyToManyRelationKey) where T : SimpleObject
		//public virtual ISimpleObjectCollection_OLD GetManyToManyObjectCollection(int manyToManyRelationKey) { return null; }


		//public ManyToManySimpleObjectCollection<T> GetManyToManyObjectCollection<T>(int manyToManyRelationKey) where T : SimpleObject
		//{
		//	IManyToManyRelationModel manyToManyRelationModel = this.Manager.ModelDiscovery.GetManyToManyRelationModel(manyToManyRelationKey);
			
		//	return this.GetManyToManyObjectCollection<T>(manyToManyRelationModel);

		//	//return this.ObjectRelationCache.GetManyToManyObjectCollection<T>(manyToManyRelationKey);
		//}

		//public ManyToManySimpleObjectCollection<T> GetManyToManyObjectCollection<T>(IManyToManyRelationModel manyToManyRelationModel) where T : SimpleObject
		//{
		//	//return this.ObjectRelationCache.GetManyToManyObjectCollection<T>(manyToManyRelationModel);
		//	ManyToManySimpleObjectCollection<T> result = null;
		//	ISimpleObjectCollection_OLD value;
		//	bool isOwnerObjectIdFirst;
		//	ManyToManyRelation manyToManyRelation = null;
		//	//bool generateNewKeyArray = true;

		//	if (this.ManyToManyObjectCollectionsByRelationKey.TryGetValue(manyToManyRelationModel.RelationKey, out value))
		//	{
		//		result = value as ManyToManySimpleObjectCollection<T>;

		//		////if (result == null)
		//		//result = result;
		//	}
		//	else
		//	{
		//		manyToManyRelation = this.Manager.GetManyToManyRelation(manyToManyRelationModel.RelationKey);
		//		int tableId = this.GetModel().TableInfo.TableId;

		//		if (this.GetModel().RelationModel.AsFirstObjectInManyToManyRelations.ContainsKey(manyToManyRelationModel.RelationKey) && //  ObjectRelationModel.ManyToManyRelationFirstObjectTypeDictionary.ContainsKey(manyToManyRelationModel.RelationKey) &&
		//		   (manyToManyRelationModel.SecondObjectType == typeof(T) || manyToManyRelationModel.SecondObjectType.IsSubclassOf(typeof(T))))
		//		{
		//			SimpleObjectCollection_OLD<GroupMembershipElement> manyToManyRelationElements = this.Manager.GetObjectCache(GroupMembershipElementModel.TableId).Select_OLD<GroupMembershipElement>(
		//																													   gme => gme.RelationKey == manyToManyRelationModel.RelationKey &&
		//																															  gme.Object1TableId == tableId &&
		//																															  gme.Object1Id == this.Id);
		//			isOwnerObjectIdFirst = true;
		//			value = new ManyToManySimpleObjectCollection<T>(this, manyToManyRelation, manyToManyRelationElements, isOwnerObjectIdFirst);
		//		}
		//		else if (this.GetModel().RelationModel.AsSecondObjectInManyToManyRelations.ContainsKey(manyToManyRelationModel.RelationKey) && // ObjectRelationModel.ManyToManyRelationSecondObjectTypeDictionary.ContainsKey(manyToManyRelationModel.RelationKey) &&
		//				(manyToManyRelationModel.FirstObjectType == typeof(T) || manyToManyRelationModel.FirstObjectType.IsSubclassOf(typeof(T))))
		//		{
		//			SimpleObjectCollection_OLD<GroupMembershipElement> groupMembershipElements = this.Manager.GetObjectCache(GroupMembershipElementModel.TableId).Select_OLD<GroupMembershipElement>(
		//																													   gme => gme.RelationKey == manyToManyRelationModel.RelationKey &&
		//																															  gme.Object2TableId == tableId &&
		//																															  gme.Object2Id == this.Id);
		//			isOwnerObjectIdFirst = false;
		//			value = new ManyToManySimpleObjectCollection<T>(this, manyToManyRelation, groupMembershipElements, isOwnerObjectIdFirst);
		//		}
		//		else
		//		{
		//			return null;
		//			//throw new ArgumentException("GetManyToManyObjectCollection: There is no relation model definition.");
		//		}

		//		//if (value != null)
		//		//{
		//		//lock (lockObject)
		//		//{
		//		this.ManyToManyObjectCollectionsByRelationKey[manyToManyRelationModel.RelationKey] = value;
		//		//}
		//		//}

		//		//generateNewKeyArray = false;
		//	}

		//	//if (generateNewKeyArray && value != null)
		//	//{
		//	//    //value = new ManyToManySimpleObjectCollection<T>(this.Owner.Manager, manyToManyRelationModel, value, (value as ManyToManySimpleObjectCollection<T>).isOwnerObjectIdFirst);
		//	//}
		//	result = value as ManyToManySimpleObjectCollection<T>;

		//	if (result == null && value != null) // cannot be custed => create custom wrap
		//	{
		//		if (manyToManyRelation == null) 
		//			manyToManyRelation = this.Manager.GetManyToManyRelation(manyToManyRelationModel.RelationKey);
				
		//		result = new ManyToManySimpleObjectCollection<T>(this, manyToManyRelation, (value as IManyToManyCollectionControl).ManyToManyRelationElements, new CustomList<T>(value) as IList<T>, (value as IManyToManyCollectionControl).IsOwnerObjectIdFirst);
		//	}

		//	return result;
		//}

		#endregion |   Public Many-To-Many Relation Methods   |

		#region |   Relation Helper Methods   |

		//private SimpleObject GetRelationForeignObject(int foreignTableId, long foreignObjectId, IOneToOneOrManyRelationModel relationModel)
		//{
		//	if (relationModel.RelationType == ObjectRelationType.OneToOne)
		//	{
		//		return this.GetOneToOneForeignObject(relationModel.RelationKey);
		//	}
		//	else if (relationModel.RelationType == ObjectRelationType.OneToMany)
		//	{
		//		return this.GetOneToManyForeignObject(relationModel.RelationKey);
		//	}

		//	//if (foreignTableId == 0)
		//	//	foreignTableId = relationModel.ForeignObjectTableId; // If foreign TableId is not specified (0) it is determined by the relation model definition

		//	//return this.Manager.GetObject(foreignTableId, foreignObjectId);
		//}

		public SimpleObject GetRelationPrimaryObject(int relationKey)
		{
			IRelationModel relationModel = this.Manager.GetRelationModel(relationKey)!;
			SimpleObject result = this.GetRelationPrimaryObject((relationModel as IOneToOneOrManyRelationModel)!);

			return result;
		}

		public SimpleObject GetRelationPrimaryObject(IOneToOneOrManyRelationModel relationModel)
		{
			//if (relationModel.RelationType == ObjectRelationType.OneToOne)
			if (relationModel is IOneToOneRelationModel)
			{ 
				return this.GetOneToOnePrimaryObject(relationModel.RelationKey)!;
			}
			else if (relationModel is IOneToManyRelationModel) //else if (relationModel.RelationType == ObjectRelationType.OneToMany)
			{
				return this.GetOneToManyPrimaryObject(relationModel.RelationKey)!;
			}

			return null;

			//int primaryTableId = (relationModel.ForeignTableIdPropertyModel != null) ? this.GetPropertyValue<int>(relationModel.ForeignTableIdPropertyModel.Index) : relationModel.PrimaryObjectTableId;
			//long primaryObjectId = this.GetPropertyValue<long>(relationModel.ForeignObjectIdPropertyModel.Index);

			//return this.Manager.GetObject(primaryTableId, primaryObjectId);
		}

		public SimpleObject? GetRelationOldPrimaryObject(int relationKey)
		{
			IRelationModel relationModel = this.Manager.GetRelationModel(relationKey)!;
			SimpleObject? result = this.GetRelationOldPrimaryObject((relationModel as IOneToOneOrManyRelationModel)!);

			return result;
		}

		private SimpleObject? GetRelationOldPrimaryObject(IOneToOneOrManyRelationModel relationModel)
		{
			int primaryTableId = (relationModel.PrimaryTableIdPropertyModel != null) ? (int)this.GetOldPropertyValue(relationModel.PrimaryTableIdPropertyModel.PropertyIndex)! : relationModel.PrimaryObjectTableId;
			long primaryObjectId = (long)this.GetOldPropertyValue(relationModel.PrimaryObjectIdPropertyModel.PropertyIndex)!;

			return this.Manager.GetObject(primaryTableId, primaryObjectId)!;
		}


		//private void SetRelationPrimaryObject(int primaryTableId, long primaryObjectId, IOneToOneOrManyRelationModel relationModel, ChangeContainer changeContainer, object requester)
		//{
		//	SimpleObject primaryObject = this.Manager.GetObject(primaryTableId, primaryObjectId);

		//	this.SetRelationPrimaryObject(primaryObject, relationModel, changeContainer, requester);
		//}

		public void SetRelationPrimaryObject(SimpleObject? primaryObject, IOneToOneOrManyRelationModel relationModel, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			this.SetRelationPrimaryObjectInternal(primaryObject, relationModel, changeContainer, context, requester);
		}

		internal void SetRelationPrimaryObjectInternal(int primaryTableId, long primaryObjectId, IOneToOneOrManyRelationModel relationModel, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			SimpleObject primaryObject = this.Manager.GetObject(primaryTableId, primaryObjectId)!;

			this.SetRelationPrimaryObjectInternal(primaryObject, relationModel, changeContainer, context, requester);
		}

		internal void SetRelationPrimaryObjectInternal(SimpleObject? primaryObject, IOneToOneOrManyRelationModel relationModel, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			if (relationModel is IOneToOneRelationModel oneToOneRelationModel)
				this.SetOneToOnePrimaryObject(primaryObject, oneToOneRelationModel, changeContainer, context, requester);
			else if (relationModel is IOneToManyRelationModel oneToManyRelationModel)
				this.SetOneToManyPrimaryObject(primaryObject, oneToManyRelationModel, changeContainer, context, requester);
		}

		internal void RemoveRelationPrimaryObjectFromCache(int primaryObjectTableId, long primaryObjectId, IOneToOneOrManyRelationModel relationModel)
		{
			if (relationModel is IOneToOneRelationModel)
			{
				SimpleObject? primaryObject = this.Manager.GetObject(primaryObjectTableId, primaryObjectId);

				if (primaryObject != null)
					primaryObject.RemoveOneToOneForeignObjectFromCache(relationModel.RelationKey);
			}
			else if (relationModel is IOneToManyRelationModel)
			{
				this.RemoveOneToManyForeignCollectionFromCache(relationModel.RelationKey);
			}
		}

		public void SetRelationPrimaryObject(int primaryTableId, long primaryObjectId, int relationKey, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			SimpleObject primaryObject = this.Manager.GetObject(primaryTableId, primaryObjectId)!;

			this.SetRelationPrimaryObject(primaryObject, relationKey, changeContainer, context, requester);
		}

		public void SetRelationPrimaryObject(SimpleObject? foreignObject, int relationKey, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			IRelationModel relationModel = this.Manager.GetRelationModel(relationKey)!;

			this.SetRelationPrimaryObjectInternal(foreignObject, (relationModel as IOneToOneOrManyRelationModel)!, changeContainer, context, requester);
		}
		
		//public SimpleObject GetRelationForeignObjectByForeignObjectIdPropertyIndex(int foreignTableId, long foreignObjectId, int foreignObjectIdPropertyIndex)
		//{
		//	IOneToOneOrManyRelationModel relationModel = this.GetModel().RelationModel.GetForeignRelationModel(foreignObjectIdPropertyIndex, foreignTableId);

		//	return this.GetRelationForeignObject(foreignTableId, foreignObjectId, relationModel);
		//}

		//public void SetRelationForeignObjectByForeignObjectIdPropertyIndex(int foreignTableId, long foreignObjectId, int foreignObjectIdPropertyIndex, ChangeContainer changeContainer, object requester)
		//{
		//	IOneToOneOrManyRelationModel relationModel = this.GetModel().RelationModel.GetForeignRelationModel(foreignObjectIdPropertyIndex, foreignTableId);

		//	if (relationModel != null)
		//	{
		//		SimpleObject foreignObject = this.GetRelationForeignObject(foreignTableId, foreignObjectId, relationModel);

		//		this.SetRelationForeignObject(foreignObject, relationModel, changeContainer, requester);
		//	}
		//	else
		//	{
		//		throw new ArgumentNullException("SetRelationForeignObject method, relationModel is not found by foreignObjectIdPropertyIndex and foreignTableId");
		//	}
		//}

		#endregion |   Relation Helper Methods   |

		#region |   Internal Object Relation Methods   |

		//internal void SetOneToOneRelationPrimaryObjectIfInHash(int oneToOneRelationKey, SimpleObject foreignObject)
		//{
		//	//lock (lockObject)
		//	//{
		//	if (foreignObject != null)
		//	{
		//		this.OneToOnePrimaryObjectsByRelationKey[oneToOneRelationKey] = foreignObject;
		//	}
		//	else if (this.OneToOnePrimaryObjectsByRelationKey.ContainsKey(oneToOneRelationKey))
		//	{
		//		this.OneToOnePrimaryObjectsByRelationKey.Remove(oneToOneRelationKey);
		//	}
		//	//}
		//}

		internal void SetOneToOneForeignObjectInternal(int oneToOneRelationKey, SimpleObject? foreignObject)
		{
			this.OneToOneForeignSimpleObjectsByRelationKey[oneToOneRelationKey] = foreignObject;

			////lock (this.lockObject)
			////{
			//	if (foreignObject != null)
			//	{
			//		SimpleObjectKey key;
			//		int tableId = foreignObject.GetModel().TableInfo.TableId;
			//		long objectId = foreignObject.Id;

			//		if (this.OneToOneForeignSimpleObjectsByRelationKey.TryGetValue(oneToOneRelationKey, out key))
			//		{
			//			// now check if key need update?
			//			if (key.TableId != tableId || key.ObjectId != objectId)
			//				this.OneToOneForeignSimpleObjectsByRelationKey[oneToOneRelationKey] = new SimpleObjectKey(tableId, objectId);
			//		}
			//		else
			//		{
			//			this.OneToOneForeignSimpleObjectsByRelationKey.Add(oneToOneRelationKey, new SimpleObjectKey(tableId, objectId));
			//		}
			//	}
			//	else if (this.OneToOneForeignSimpleObjectsByRelationKey.ContainsKey(oneToOneRelationKey))
			//	{
			//		this.OneToOneForeignSimpleObjectsByRelationKey.Remove(oneToOneRelationKey);
			//	}
			////}
		}

		internal void RemoveOneToOneForeignObjectFromCache(int oneToOneRelationKey)
		{
			this.OneToOneForeignSimpleObjectsByRelationKey.Remove(oneToOneRelationKey);
		}

		//internal void ChangeOneToOneForeignObjectTempIdIfInCache(int tableId, long oldTempId, long newId)
		//{
		//	//lock (this.lockObject)
		//	//{
		//	//	if (this.oneToOneForeignSimpleObjectsByRelationKey != null)
		//	//	{
		//	//		foreach (var item in this.oneToOneForeignSimpleObjectsByRelationKey)
		//	//		{
		//	//			SimpleObjectKey objectKey = item.Value;

		//	//			if (objectKey.ObjectId == oldTempId && objectKey.TableId == tableId)
		//	//			{
		//	//				int relationKey = item.Key;
							
		//	//				this.OneToOneForeignSimpleObjectsByRelationKey[relationKey] = new SimpleObjectKey(tableId, newId);
		//	//			}
		//	//		}
		//	//	}
		//	//}
		//}

		internal void AddToOneToManyForeignCollectionIfInCache(int oneToManyRelationKey, SimpleObject simpleObject, ChangeContainer? changeContainer, object? requester)
		{
			//lock (this.lockObject)
			//{
				SimpleObjectCollection? relatedObjectCollection;

				if (this.OneToManyForeignCollectionsByRelationKey.TryGetValue(oneToManyRelationKey, out relatedObjectCollection))
					if (!relatedObjectCollection.Contains(simpleObject))
						relatedObjectCollection.Add(simpleObject, changeContainer, requester);
			//}
		}

		internal void RemoveOneToManyForeignCollectionIfInCache(int oneToManyRelationKey, SimpleObject simpleObject, ChangeContainer? changeContainer, object? requester)
		{
			//lock (this.lockObject)
			//{
				SimpleObjectCollection? relatedObjectCollection;

				if (this.OneToManyForeignCollectionsByRelationKey.TryGetValue(oneToManyRelationKey, out relatedObjectCollection))
					relatedObjectCollection.Remove(simpleObject, changeContainer, requester);
			//}
		}

		internal void RemoveOneToManyForeignCollectionFromCache(int oneToManyRelationKey)
		{
			this.OneToManyForeignCollectionsByRelationKey.Remove(oneToManyRelationKey);
		}

		internal void AddGroupMembershipIfInCache(GroupMembership groupMembership, ChangeContainer? changeContainer, object? requester)
		{
			lock (this.lockObject)
			{
				SimpleObjectCollection? membershipCollection;

				if (this.GroupMembershipCollectionsByRelationKey.TryGetValue(groupMembership.RelationKey, out membershipCollection))
				{
					bool isThisFirst = (groupMembership.Object1TableId == this.GetModel().TableInfo.TableId);
					int tableId = (isThisFirst) ? groupMembership.Object2TableId : groupMembership.Object1TableId;
					long objectId = (isThisFirst) ? groupMembership.Object2Id : groupMembership.Object1Id;

					membershipCollection.Add(tableId, objectId, changeContainer, requester);
					membershipCollection.AddGroupMembership(groupMembership.Id, changeContainer, requester);
				}
			}
		}

		internal void RemoveGroupMembershipFromCache(GroupMembership groupMembership, ChangeContainer? changeContainer, object? requester)
		{
			lock (this.lockObject)
			{
				SimpleObjectCollection? membershipCollection;

				if (this.GroupMembershipCollectionsByRelationKey.TryGetValue(groupMembership.RelationKey, out membershipCollection))
				{
					bool isThisFirst = (groupMembership.Object1TableId == this.GetModel().TableInfo.TableId);
					int tableId = (isThisFirst) ? groupMembership.Object2TableId : groupMembership.Object1TableId;
					long objectId = (isThisFirst) ? groupMembership.Object2Id : groupMembership.Object1Id;

					membershipCollection.Remove(tableId, objectId, changeContainer, requester);
					membershipCollection.RemoveGroupMembership(groupMembership.Id, changeContainer, requester);
				}
			}
		}

		//internal void AddToManyToManyObjectCollectionIfInCache(GroupMembershipElement manyToManyRelationElement)
		//{
		//	ISimpleObjectCollection_OLD manyToManyCollection;

		//	if (this.ManyToManyObjectCollectionsByRelationKey.TryGetValue(manyToManyRelationElement.RelationKey, out manyToManyCollection))
		//	{
		//		//lock (lockObject)
		//		//{
		//		(manyToManyCollection as IManyToManyCollectionControl).AddGroupMembershipElement(manyToManyRelationElement);
		//		//}
		//	}
		//}

		//internal void RemoveFromManyToManyObjectCollectionIfInCache(GroupMembershipElement manyToManyRelationElement)
		//{
		//	ISimpleObjectCollection_OLD manyToManyCollection;

		//	if (this.ManyToManyObjectCollectionsByRelationKey.TryGetValue(manyToManyRelationElement.RelationKey, out manyToManyCollection))
		//	{
		//		//lock (lockObject)
		//		//{
		//		(manyToManyCollection as IManyToManyCollectionControl).RemoveGroupMembershipElement(manyToManyRelationElement);
		//		//}
		//	}
		//}

		//internal void SaveManyToManyRelatedObjectsIfCan()
		//{
		//	int tableId = this.GetModel().TableInfo.TableId;
		//	SimpleObjectCollection_OLD<GroupMembershipElement> groupMembershipCollection = this.Manager.GetObjectCache(GroupMembershipElementModel.TableId).Select_OLD<GroupMembershipElement>(gme => (gme.Object1TableId == tableId && gme.Object1Id == this.Id) ||
		//																																													  (gme.Object2TableId == tableId && gme.Object2Id == this.Id));
		//	foreach (GroupMembershipElement groupMembershipElement in groupMembershipCollection)
		//		groupMembershipElement.SaveIfNeeded();
		//}

		internal void RemoveAllRelatedObjectsFromAllRelatedObjectCachesInternal(ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			//foreach (IOneToManyRelationModel oneToManyRelationModel in this.GetModel().RelationModel.AsForeignObjectInOneToManyRelations) //  ObjectRelationModel.OneToManyRelationForeignKeyObjectDictionary)
			//	this.GetOneToManyForeignNullCollection(oneToManyRelationModel.RelationKey)?.Remove(this.GetModel().TableInfo.TableId, this.Id);
			
			// OneToOne
			//foreach (IOneToOneRelationModel oneToOneRelationModel in this.GetModel().RelationModel.OneToOneKeyHolderObjectModels) //  ObjectRelationModel.OneToOneRelationKeyHolderObjectDictionary)
			//{
			//	//if (oneToOneRelationModel != null)
			//	//{
			//	//	int relationModelKey = (int)dictionaryEntry.Key;
			//	//	IOneToOneRelationModel relationPolicyModel = dictionaryEntry.Value;

				
			//	// TODO: This checking CascadeDelete should be done before calling this
			//	if (oneToOneRelationModel.CascadeDelete)
			//	{
			//		SimpleObject foreignSimpleObject = this.GetOneToOneRelationKeyHolderObject<SimpleObject>(oneToOneRelationModel);

			//		if (foreignSimpleObject != null)
			//			foreignSimpleObject.RequestDelete(changeContainer, requester);
			//			//changeContainer.Set(foreignSimpleObject, TransactionRequestAction.Delete, requester); // foreignSimpleObject);
			//	}
			//	//}
			//}

			foreach (IOneToOneRelationModel oneToOneRelationModel in this.GetModel().RelationModel.AsForeignObjectInOneToOneRelations) //    ObjectRelationModel.OneToOneRelationKeyHolderObjectDictionary)
				this.SetOneToOnePrimaryObject(primaryObject: null, oneToOneRelationModel, changeContainer, context, requester);

			foreach (IOneToOneRelationModel oneToOneRelationModel in this.GetModel().RelationModel.AsPrimaryObjectInOneToOneRelations) //    ObjectRelationModel.OneToOneRelationKeyHolderObjectDictionary)
				this.SetOneToOneForeignObject(foreignObject: null, oneToOneRelationModel, changeContainer, context, requester);

			//// Consider if this need to do since cascade delete is checked in SimpleObjectManager.GetRelatedObjectsForDelete method
			//foreach (IOneToOneRelationModel oneToOneRelationModel in this.GetModel().RelationModel.AsPrimaryObjectInOneToOneRelations) // this.ObjectRelationModel.OneToOneRelationForeignKeyObjectDictionary)
			//	this.SetOneToOneForeignObject(foreignObject: null, oneToOneRelationModel, changeContainer, requester);

			this.OneToOneForeignSimpleObjectsByRelationKey.Clear();
			//this.OneToOnePrimaryObjectsByRelationKey.Clear();


			// OneToMany

			//foreach (var dictionaryEntry in this.OneToManyForeignObjectsByRelationKey)
			//{
			//    int relationModelKey = (int)dictionaryEntry.Key;
			//    SimpleObject foreignObject = dictionaryEntry.Value;
			//    IOneToManyRelationPolicyModel onoToManyRelationPolicyModel = this.Owner.Manager.GetOneToManyRelationModel(relationModelKey);

			//    if (onoToManyRelationPolicyModel.CascadeDelete)
			//    {
			//        this.Owner.Manager.DeleteInternal(this.Owner, foreignObject);
			//    }
			//    else
			//    {
			//        foreignObject.ObjectRelationCache.RemoveFromOneToManyObjectCollectionIfInCache(relationModelKey, this.Owner.Key);
			//    }
			//}




			//foreach (IOneToManyRelationModel oneToManyRelationModel in this.GetModel().RelationModel.AsPrimaryObjectInOneToManyRelations) // ObjectRelationModel.OneToManyRelationKeyHolderObjectDictionary)
			//{
			//	//int relationModelKey = dictionaryEntry.Key;
			//	//IOneToManyRelationModel relationPolicyModel = dictionaryEntry.Value;

			//	//// If this is GraphElement and the Parent is null (root GraphElement) remove it from Graph.RootGraphElements collection;
			//	//if (this is GraphElement && (this as GraphElement).ParentId == 0)
			//	//	(this as GraphElement).Graph.RootGraphElementIsDeleting(this as GraphElement);

			//	ISimpleObjectCollection foreignObjectCollection = this.GetOneToManyForeignObjectCollection(oneToManyRelationModel.RelationKey);

			//	if (foreignObjectCollection != null) // && foreignObjectCollection.Count > 0)
			//	{
			//		//SimpleObjectCollection<SimpleObject> newForeignObjectCollection = new SimpleObjectCollection<SimpleObject>(this.Owner.Manager, foreignObjectCollection.ObjectKeys.ToArray());

			//		//if (oneToManyRelationModel.CascadeDelete)
			//		//{
			//		//	foreach (SimpleObject simpleObject in foreignObjectCollection.ToArray())
			//		//		simpleObject.RequestDelete(changeContainer, requester);
			//		//		//changeContainer.Set(simpleObject, TransactionRequestAction.Delete, requester); // foreignSimpleObject);
			//		//		//this.Manager.DeleteInternal(this, simpleObject);
			//		//}
			//		//else
			//		//{
			//		foreach (SimpleObject simpleObject in foreignObjectCollection)
			//		{
			//			if (simpleObject != null && !simpleObject.DeleteStarted)
			//			{
			//				simpleObject.SetOneToManyPrimaryObject(primaryObject: null, oneToManyRelationModel, addOrRemoveInChangedPropertyNames: true, firePropertyValueChangeEvent: true,
			//													   raiseForeignObjectSetEvent: true, changeContainer, requester);
			//				//changeContainer.Set(simpleObject, TransactionRequestAction.Save, requester); // foreignSimpleObject);
			//				//this.SetRelatedTransactionRequest(simpleObject, TransactionRequestAction.Save);
			//			}
			//		}
			//		//}
			//	}

			//	ISimpleObjectCollection foreignNullCollection = this.GetOneToManyForeignNullCollection(oneToManyRelationModel.RelationKey);

			//	if (foreignNullCollection != null)
			//		foreignNullCollection.Remove(this);

			//	//if (foreignObject != null)
			//	//{
			//	//    if (relationPolicyModel.CascadeDelete)
			//	//    {
			//	//        this.Owner.Manager.DeleteInternal(this.Owner, foreignObject);
			//	//    }
			//	//    else
			//	//    {
			//	//        foreignObject.ObjectRelationCache.RemoveFromOneToManyObjectCollectionIfInCache(relationModelKey, this.Owner.Key);
			//	//    }
			//	//}
			//}


			foreach (IOneToManyRelationModel oneToManyRelationModel in this.GetModel().RelationModel.AsForeignObjectInOneToManyRelations)
			{ //  ObjectRelationModel.OneToManyRelationForeignKeyObjectDictionary)
				//this.SetOneToManyPrimaryObject(null, oneToManyRelationModel, addOrRemoveInChangedPropertyNames: false, firePropertyValueChangeEvent: false, raiseForeignObjectSetEvent: true, changeContainer, context, requester);
				this.SetOneToManyPrimaryObject(null, oneToManyRelationModel, raiseForeignObjectSetEvent: true, changeContainer, context, requester);
				this.GetOneToManyForeignNullCollection(oneToManyRelationModel.RelationKey)?.Remove(this, changeContainer, requester);
			}

			this.OneToManyForeignCollectionsByRelationKey.Clear();
			//this.OneToManyPrimaryObjectsByRelationKey.Clear();

			//foreach (var model in this.GetModel().RelationModel.ForeignObjectModels)
			//	this.SetRelationForeignObject(foreignObject: null, model.RelationKey, changeContainer, requester);

			// ManyToMany
			//IList<SimpleObjectKey> relatedGroupMembershipElementKeys = this.ObjectManager.ObjectCache.GetObjectKeys<ManyToManyRelationElement>((gme => gme.Object1Guid == this.Key.Guid ||
			//																																	   gme.Object2Guid == this.Key.Guid));
			
			//foreach (var item in this.GetModel().RelationModel.AsFirstObjectInManyToManyRelations)
			//{

			//}

			//foreach (var item in this.GetModel().RelationModel.AsSecondObjectInManyToManyRelations)
			//{

			//}

			foreach (var item in this.GetModel().RelationModel.AsObjectInGroupMembership)
				if (this.GetGroupMemberCollection(item.RelationKey)?.GetGroupMembershipCollection() is SimpleObjectCollection<GroupMembership> groupMembershipCollection)
					foreach (var groupMembership in groupMembershipCollection)
						groupMembership.RequestDelete(changeContainer, context, requester);



			//int tableId = this.GetModel().TableInfo.TableId;
			//SimpleObjectCollection_OLD<GroupMembershipElement> groupMembershipCollection = this.Manager.GetObjectCache(GroupMembershipElementModel.TableId).Select_OLD<GroupMembershipElement>(gme => (gme.Object1TableId == tableId && gme.Object1Id == this.Id) ||
			//																																										  (gme.Object2TableId == tableId && gme.Object2Id == this.Id));
			//foreach (GroupMembershipElement groupMembershipElement in groupMembershipCollection)
			//{
			//	SimpleObject relatedBusinesObject = (groupMembershipElement.SimpleObject1 == this) ? groupMembershipElement.SimpleObject2 : groupMembershipElement.SimpleObject1;
				
			//	relatedBusinesObject.RemoveFromManyToManyObjectCollectionIfInCache(groupMembershipElement);
			//	groupMembershipElement.RequestDelete(changeContainer, requester);
			//}

			//this.ManyToManyObjectCollectionsByRelationKey.Clear();
		}

		#endregion |   Internal Object Relation Methods   |

		#region |   Protected Internal Methods   |

		//protected internal void SetPropertyValueInternal(IPropertyModel propertyModel, object? value, ChangeContainer? changeContainer, object? requester = null)
		//{
		//	this.SetPropertyValuePrivate(propertyModel, value, changeContainer, requester);
		//}

		//protected internal void SetPropertyValueInternal(IPropertyModel propertyModel, object? value, bool addOrRemoveInChangedProperties, bool firePropertyValueChangeEvent, ChangeContainer? changeContainer, object? requester)
		//{
		//	this.SetPropertyValuePrivate(propertyModel, value, addOrRemoveInChangedProperties, firePropertyValueChangeEvent, enforceAccessModifier: false, trimBeforeStringComparison: true, changeContainer, requester);
		//}



		//protected override void ObjectIsCreated()
		//{
		//    if (this.ID != 0)
		//    {
		//        base.ObjectIsCreated();
		//    }
		//}

		//protected internal SimpleObjectRelationCache ObjectRelationCache
		//{
		//	get
		//	{
		//		if (this.objectRelationCache == null)
		//		{
		//			this.objectRelationCache = new SimpleObjectRelationCache(this);
		//		}

		//		return this.objectRelationCache;
		//	}
		//}

		//protected internal void SetModel(ISimpleObjectModel model)
		//{
		//	base.SetModel(model);
		//}

		//protected internal new void Load()
		//{
		//	base.Load();
		//}

		//protected internal new void Load(object requester, IDictionary<int, object> propertyDataByIndex, bool acceptChanges, Func<IPropertyModel, object, object> normalizer)
		//      {
		//	base.Load(requester, propertyDataByIndex, acceptChanges, normalizer);
		//      }

		//protected internal new void Load(object requester, IDictionary<string, object> propertyDataByName, bool acceptChanges, Func<IPropertyModel, object, object> normalizer)
		//{
		//	base.Load(requester, propertyDataByName, acceptChanges, normalizer);
		//}

		//protected internal void SetPropertyValueInternal(int propertyIndex, object value, bool addOrRemoveInChangedProperties, bool firePropertyValueChangeEvent, object requester)
		//{
		//    this.SetPropertyValueInternal(propertyIndex, value, addOrRemoveInChangedProperties, firePropertyValueChangeEvent, requester);
		//}

		//protected internal virtual void OnRelationForeignObjectSet(object requester, SimpleObject simpleObject, SimpleObject foreignSimpleObject, SimpleObject oldForeignSimpleObject, IOneToOneOrManyRelationModel objectRelationModel, SimpleRelationType objectRelationType)
		//{
		//	this.RaiseRelationForeignObjectSet(requester, simpleObject, foreignSimpleObject, oldForeignSimpleObject, objectRelationModel, objectRelationType);
		//}

		///// <summary>
		///// Gets the property values dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with property index as a key.
		///// </summary>
		///// <returns>The dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with field values by index.</returns>
		//protected internal IDictionary<int, object> GetPropertyValuesByIndex()
		//{
		//    return this.GetPropertyValuesByIndex();
		//}

		///// <summary>
		///// Gets the property values dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with property index as a key.
		///// Each returned property value is calculated by <param name="normalizer"> method given. If <param name="normalizer"> is null returned result is unchanged property value.
		///// </summary>
		///// <param name="normalizer">The method to calculate returned item value.</param>
		///// <returns>The dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with property values by index.</returns>
		//protected internal IDictionary<int, object> GetPropertyValuesByIndex(Func<IPropertyModel, object, object> normalizer)
		//{
		//    return this.GetPropertyValuesByIndex(normalizer);
		//}

		///// <summary>
		///// Gets the property values dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with property name as a key.
		///// </summary>
		///// <returns>The dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with field values by proeprty name.</returns>
		//protected internal IDictionary<string, object> GetPropertyValuesByName()
		//{
		//    return this.GetPropertyValuesByName();
		//}

		///// <summary>
		///// Gets the property values dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with property name as a key.
		///// Each returned property value is calculated by <param name="normalizer"> method given. If <param name="normalizer"> is null returned result is unchanged property value.
		///// </summary>
		///// <param name="normalizer">The method to calculate returned item value.</param>
		///// <returns></returns>
		//protected internal new IDictionary<string, object> GetPropertyValuesByName(Func<IPropertyModel, object, object> normalizer)
		//{
		//    return base.GetPropertyValuesByName(normalizer);
		//}

		//protected internal new IDictionary<int, object> GetOldPropertyValuesByIndex()
		//{
		//    return base.GetOldPropertyValuesByIndex();
		//}

		//protected internal new IDictionary<int, object> GetOldPropertyValuesByIndex(Func<IPropertyModel, object, object> normalizer)
		//{
		//    return base.GetOldPropertyValuesByIndex(normalizer);
		//}


		//protected internal new IDictionary<string, object> GetOldPropertyValuesByName()
		//{
		//    return base.GetOldPropertyValuesByName();
		//}

		//protected internal new IDictionary<string, object> GetOldPropertyValuesByName(Func<IPropertyModel, object, object> normalizer)
		//{
		//    return base.GetOldPropertyValuesByName(normalizer);
		//}

		//protected internal new IDictionary<int, object> GetChangedPropertyValuesByIndex()
		//{
		//    return base.GetChangedPropertyValuesByIndex();
		//}

		//protected internal new IDictionary<int, object> GetChangedPropertyValuesByIndex(Func<IPropertyModel, object, object> normalizer)
		//{
		//    return base.GetChangedPropertyValuesByIndex(normalizer);
		//}

		//protected internal new IDictionary<string, object> GetChangedPropertyValuesByName()
		//{
		//    return base.GetChangedPropertyValuesByName();
		//}

		//protected internal new IDictionary<string, object> GetChangedPropertyValuesByName(Func<IPropertyModel, object, object> normalizer)
		//{
		//    return base.GetChangedPropertyValuesByName(normalizer);
		//}

		//protected internal new IDictionary<int, object> GetChangedOldPropertyValuesByIndex()
		//{
		//    return base.GetChangedOldPropertyValuesByIndex();
		//}

		//protected internal new IDictionary<int, object> GetChangedOldPropertyValuesByIndex(Func<IPropertyModel, object, object> normalizer)
		//{
		//    return base.GetChangedOldPropertyValuesByIndex(normalizer);
		//}

		//protected internal new IDictionary<string, object> GetChangedOldPropertyValuesByName()
		//{
		//    return base.GetChangedOldPropertyValuesByName();
		//}

		//protected internal new IDictionary<string, object> GetChangedOldPropertyValuesByName(Func<IPropertyModel, object, object> normalizer)
		//{
		//    return base.GetChangedOldPropertyValuesByName(normalizer);
		//}

		#endregion |   Protected Internal Methods   |

		#region |   Protected Methods   |

		protected virtual void OnBeforeRelationPrimaryObjectSet(SimpleObject? primaryObject, SimpleObject? oldPrimaryObject, IOneToOneOrManyRelationModel relationModel, ref bool cancel, ChangeContainer? changeContainer, ObjectActionContext context, object? requester) { }

		protected virtual void OnRelationForeignObjectSet(SimpleObject? foreignSimpleObject, SimpleObject? oldForeignSimpleObject, IOneToOneOrManyRelationModel relationModel, ChangeContainer? changeContainer, ObjectActionContext context, object? requester) { }


				//protected override void OnAfterSave(object requester)
				//{
				//    base.OnAfterSave(requester);

				//    if (this.IsNew)
				//    {
				//        ISimpleObjectModel model = this.GetModel();

				//        foreach (GetBusinessCollection getRelatedCollection in model.RelatedCollectionDelegates)
				//        {
				//            BusinessCollection relatedCollection = getRelatedCollection(this);

				//            if (relatedCollection != null)
				//            {
				//                this.AddToRelatedCollection(relatedCollection);
				//            }
				//        }
				//    }
				//}

				//protected override void OnAfterSave(object requester, bool isNewBeforeSaving)
				//{
				//    base.OnAfterSave(requester, isNewBeforeSaving);

				//    //if (isNewBeforeSaving)
				//    //{
				//    //    this.ObjectRealtionCache.SaveManyToManyRealtedObjectsIfCan();
				//    //}
				//}

				//protected virtual void OnBeforeDelete(object requester)
				//{
				//	//base.OnBeforeDeleting(requester);

				//	//// Remove object from related collections, first
				//	//this.ProcessRelatedCollectionAction((relatedCollection => relatedCollection.ListRemove(this.Key)));

				//	////Remove from relations
				//	//this.RemoveFromManyToManyObjectCollectionIfInCache ObjectRealtionCache.RemoveFromAllRelatedObjects();

				//	////Remove object from cache
				//	//if (!this.IsNew)
				//	//{
				//	//	if (this is GraphElement)
				//	//	{
				//	//		this.Manager.ObjectCache.GraphElementCache.RemoveGraphElement(this as GraphElement);
				//	//	}
				//	//	else
				//	//	{
				//	//		this.Manager.ObjectCache.GraphElementCache.RemoveSimpleObject(this);
				//	//	}

				//	//	this.Manager.ObjectCache.RemoveObject(this.Key);
				//	//}
				//}

				//protected override void OnBeforePropertyValueChange(object requester, string propertyName, object newValue, object currentValue)
				//{
				//    base.OnBeforePropertyValueChange(requester, propertyName, newValue, currentValue);

				//    this.ProcessRelatedCollectionAction((relatedCollection => relatedCollection.ListRemove(this.Key)), propertyName);
				//}

				//protected override void OnPropertyValueChange(object requester, string propertyName, object value, object oldValue)
				//{
				//    base.OnPropertyValueChange(requester, propertyName, value, oldValue);

				//    this.ProcessRelatedCollectionAction((relatedCollection => relatedCollection.ListAdd(this.Key)), propertyName);
				//}


				//protected override ISimpleObjectModel GetCustomObjectModel()
				//{
				//	return this.GetModel();
				//}


				//private List<BusinessCollection> RelatedCollections
				//{
				//    get
				//    {
				//        if (this.relatedCollections == null)
				//        {
				//            this.relatedCollections = new List<BusinessCollection>();
				//        }

				//        return this.relatedCollections;
				//    }
				//}

				//private int CreateObjectId()
				//{
				//    int objectId = 0;
				//    object objectIdValue = this.GetPropertyValue(SimpleObjectModel.PropertyModel.ObjectId);

				//    if (objectIdValue != null)
				//    {
				//        objectId = (int)objectIdValue;
				//    }

				//    if (objectId == 0)
				//    {
				//        ISimpleObjectModel objectModel = this.GetObjectModel();
				//        SimpleObjectKey currentObjectKey = SimpleObjectKey.GetEmptyKey(objectModel.TableInfo.TableId);

				//        SimpleObjectKey objectKey = this.Manager.ObjectCache.AddObject(this); //, currentObjectKey);

				//        this.SetPropertyValueInternal(SimpleObjectModel.PropertyModel.ObjectId.Name, objectKey.ObjectId, false, true, this);
				//        objectId = objectKey.ObjectId;
				//    }
				//    else
				//    {
				//        throw new ArgumentException("You can create ID only if current ID is not set or it has zero value.");
				//    }

				//    return objectId;
				//}

		#endregion |   Protected Methods   |

		#region |   IBindingSimpleObject Interface   |

		SimpleObject IBindingSimpleObject.GetOneToOnePrimaryObject(int oneToOneRelationKey)
		{
			return this.GetOneToOnePrimaryObject(oneToOneRelationKey);
		}

		SimpleObject IBindingSimpleObject.GetOneToOneForeignObject(int oneToOneRelationKey)
		{
			return this.GetOneToOneForeignObject(oneToOneRelationKey);
		}

		SimpleObject IBindingSimpleObject.GetOneToManyPrimaryObject(int oneToManyRelationKey)
		{
			return this.GetOneToManyPrimaryObject(oneToManyRelationKey);
		}

        SimpleObjectCollection IBindingSimpleObject.GetOneToManyForeignObjectCollection(int oneToManyRelationKey)
        {
            return this.GetOneToManyForeignObjectCollection(oneToManyRelationKey);
        }

		SimpleObjectCollection IBindingSimpleObject.GetGroupMemberCollection(int manyToManyRelationKey)
        {
            return this.GetGroupMemberCollection(manyToManyRelationKey);
        }

        //private void ProcessRelatedCollectionAction(RelationObjectCollectionActionDelegate relatedCollectionActionDelegate)
        //{
        //    this.ProcessRelatedCollectionAction(relatedCollectionActionDelegate, null);
        //}

        //private void ProcessRelatedCollectionAction(RelationObjectCollectionActionDelegate relatedCollectionActionDelegate, string propertyNameToMatch)
        //{
        //    ISimpleObjectModel objectModel = this.GetObjectModel();

        //    foreach (IRelatedObjectCollectionModel relatedObjectCollectionModel in objectModel.RelatedObjectCollections)
        //    {
        //        bool activateAction = String.IsNullOrEmpty(propertyNameToMatch) || relatedObjectCollectionModel.RelatedPropertyNames.Contains(propertyNameToMatch);

        //        if (activateAction)
        //        {
        //            SimpleObjectCollection relatedCollection = null;

        //            // Try get related collection, while it can throw an exception - related collection is null
        //            try
        //            {
        //                relatedCollection = relatedObjectCollectionModel.GetRelatedObjectCollectionDelegate(this);
        //            }
        //            catch
        //            {
        //            }

        //            if (relatedCollection != null)
        //            {
        //                relatedCollectionActionDelegate(relatedCollection);
        //            }
        //        }
        //    }
        //}

		int[] IBindingObject.GetChangedPropertyIndexes()
		{
			return this.GetChangedPropertyIndexes();
		}

		#endregion |   IBindingSimpleObject Interface   |

		#region |   IEqualityComparer Interface   |

		bool IEqualityComparer.Equals(object? x, object? y)
		{
			return x as SimpleObject == y as SimpleObject;
		}

		int IEqualityComparer.GetHashCode(object obj)
		{
			return obj as SimpleObject == null ? 0 : obj.GetHashCode();
			//return obj as SimpleObject == null ? 0 : this.GetModel().TableInfo.TableId.GetHashCode() ^ this.Id.GetHashCode(); ;
		}

		#endregion |   IEqualityComparer Interface   |

		#region |   Dispose   |

		public void Dispose()
		{
			//this.graphElements = null;
			//this.objectRelationModel = null;

			//if (this.objectRelationCache != null)
			//{
			//	this.objectRelationCache.Dispose();
			//	this.objectRelationCache = null;
			//}
			//this.changedPropertyIndexes = null;
			//this.changedSaveablePropertyIndexes = null;
			this.tag = null;

			this.oneToOneForeignSimpleObjectsByRelationKey = null;
			//this.oneToManyPrimaryObjectsByRelationKey = null;

			//this.oneToOnePrimaryObjectsByRelationKey = null;
			this.oneToManyForeignCollectionsByRelationKey = null;
			//this.manyToManyCollectionsByRelationKey = null;
			this.groupMembershipCollectionsByRelationKey = null;

			//this.manager = null;

			//base.Dispose();
		}

		void IDisposable.Dispose()
		{
			this.Dispose();
		}

#endregion |   Dispose   |
	}
}