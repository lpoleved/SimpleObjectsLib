using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using SuperSocket;
using Simple;
using Simple.Collections;
using Simple.Serialization;
using Simple.Modeling;
using Simple.SocketEngine;
using System.Runtime.CompilerServices;

//#if !NETSTANDARD
using System.Threading.Tasks.Dataflow;
using SuperSocket.ProtoBase;
using System.Reflection;
using System.Data;
//using Simple.Objects.MonitorProtocol;
using System.Transactions;
//#endif

namespace Simple.Objects.SocketProtocol
{
	public class SimpleObjectServer : ServerBase
    {
        
		#region |   Private Fields   |

        // The AppServer protocol version
        private Version protocolVersion = new Version(0, 0);
		//private const int DefaultManagementPort = 2021;
		private LocalDatastore localDatastore;
		private SimpleObjectManager objectManager;
		private AllServerObjectModelsResponseArgs allServerObjectModelsInfoResponseArgs;
		//private SimpleObjectMonitorServer? monitorServer = null;
		//private int monitorPort = DefaultMonitorPort;
		//private bool monitorEnabled = false;

		//#if !NETSTANDARD
		private ActionBlock<SessionPackageInfo> processTransactionRequest;
		private ActionBlock<SessionPackageInfo> processAllOtherPackageReceive;
		//private ActionBlock<SimpleSession> sessionAuthenticatedInformer;
//#endif

		private HashArray<ServerObjectModelResponseArgs> serverObjectPropertyInfosByTybleId = new HashArray<ServerObjectModelResponseArgs>();

		#endregion |   Private Fields   |

		#region |   Constructors and Initialization   |

		public SimpleObjectServer(SimpleObjectManager objectManager)
		{
			//this.ObjectManager = objectManager;
			this.objectManager = objectManager; // Just to suppress CS8618
			this.localDatastore = new LocalDatastore();
			this.ObjectManager.SetServerWorkingMode(this.LocalDatastore);
			this.allServerObjectModelsInfoResponseArgs = new AllServerObjectModelsResponseArgs(this.ObjectManager.AllServerObjectModels);

			//#if !NETSTANDARD
			// Put transaction request to be single thread handled - one transaction at the time.
			this.processTransactionRequest     = new ActionBlock<SessionPackageInfo>(args => base.OnPackageReceived(this.GetCommandOwnerInstance(), args.Session, args.PackageInfo, this.CommandDiscovery).DoNotAwait(),
																													new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 1, MaxMessagesPerTask = 1 });
			this.processAllOtherPackageReceive = new ActionBlock<SessionPackageInfo>(args => base.OnPackageReceived(this.GetCommandOwnerInstance(), args.Session, args.PackageInfo, this.CommandDiscovery).DoNotAwait(),
													  																new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 1, MaxMessagesPerTask = 1 }); // MaxDegreeOfParallelism = Environment.ProcessorCount
//#endif
//this.sessionAuthenticatedInformer = new ActionBlock<SimpleSession>((session, userid, username) => this.SessionAuthenticated?.Invoke(session, userId, username)); 
		}

		~SimpleObjectServer()
		{
			//this.MonitorServer?.StopAsync().GetAwaiter().GetResult();
		}


		#endregion |   Constructors and Initialization   |

		#region |   Events   |

		public event SessionAsyncEventHandler? SessionAuthenticated;

		#endregion |   Events   |

		#region |   Public Properties   |

		public Version ProtocolVersion => this.protocolVersion;
		public LocalDatastore LocalDatastore => this.localDatastore;

		//public SimpleObjectMonitorServer MonitorServer => this.monitorServer;

		public SimpleObjectManager ObjectManager
		{
			get { return this.objectManager; }

			//private set
			//{
			//	if (this.objectManager != null)
			//	{
			//		//	this.objectManager.NewObjectCreated -= this.ObjectManager_NewObjectCreated;
			//		//	this.objectManager.AfterSave -= this.ObjectManager_AfterSave;
			//		//	this.objectManager.AfterDelete -= this.ObjectManager_AfterDelete;
			//		//	this.objectManager.PropertyValueChange -= this.ObjectManager_PropertyValueChange;
			//		//	this.objectManager.GraphElementParentChange -= this.ObjectManager_GraphElementParentChange;
			//		//	this.objectManager.OrderIndexChange -= this.ObjectManager_OrderIndexChange;
			//		//  this.objectManager.TransactionFinished -= ObjectManager_TransactionFinished;
			//		//this.objectManager.TransactionSucceeded += ObjectManager_TransactionSucceeded;
			//	}

			//	this.objectManager = value;

			//	if (this.objectManager != null)
			//	{
			//		//	this.objectManager.NewObjectCreated += this.ObjectManager_NewObjectCreated;
			//		//	this.objectManager.AfterSave += this.ObjectManager_AfterSave;
			//		//	this.objectManager.AfterDelete += this.ObjectManager_AfterDelete;
			//		//	this.objectManager.PropertyValueChange += this.ObjectManager_PropertyValueChange;
			//		//	this.objectManager.GraphElementParentChange += this.ObjectManager_GraphElementParentChange;
			//		//	this.objectManager.OrderIndexChange += this.ObjectManager_OrderIndexChange;
			//		//  this.objectManager.TransactionFinished += ObjectManager_TransactionFinished;
			//		//this.objectManager.TransactionSucceeded += ObjectManager_TransactionSucceeded;
			//	}
			//}
		}

		//private void ObjectManager_TransactionFinished(object sender, TransactionDatastoreActionRequesterEventArgs e)
		//{
		//	if (e.Transaction.Status == TransactionStatus.Completed && this.IsAnyAuthenticatedClient().GetAwaiter().GetResult())
		//	{
		//		var messageArgs = new TransactionSucceededMessageArgs(e.Transaction, e.DatastoreActions);

		//		this.SendBroadcastSystemMessage((int)SystemMessage.TransactionSucceeded, messageArgs).GetAwaiter().GetResult(); // .DoNotAwait(); //.ConfigureAwait(false); //.DoNotAwait();
		//	}
		//}

		//public int MonitorPort { get => this.monitorPort; set => this.monitorPort = value; }

		#endregion |   Public Properties   |

		#region |   Protected Override Methods   |

		protected override PackageArgsFactory CreatePackageArgsFactory()
		{
			return base.CreatePackageArgsFactory();
		}

		protected override ValueTask OnPackageReceive(IAppSession session, PackageReader packageInfo)
		{
			return base.OnPackageReceive(session, packageInfo);

#if NETSTANDARD
			return base.OnPackageReceived(this.GetCommandOwnerInstance(), session as ISimpleSession, packageInfo, this.CommandDiscovery);
#else
			SessionPackageInfo sessionPackageInfo = new SessionPackageInfo((session as ISimpleSession)!, packageInfo);

			if (packageInfo.Key == (int)SystemRequest.TransactionRequest && packageInfo.HeaderInfo.IsSystem)
				this.processTransactionRequest.Post(sessionPackageInfo); // Separate transaction requests in a single queue.
			else
				return base.OnPackageReceive(session, packageInfo); //  this.processAllOtherPackageReceive.Post(sessionPackageInfo);
#endif

			return new ValueTask();
		}


		protected override string GetServerName() => "SimpleObjectServer";

		protected override object GetCommandOwnerInstance() => this;

		protected override IPipelineFilter<PackageReader> CreatePipelineFilter(object session)
		{
			return base.CreatePipelineFilter(session);
		}

		protected override SimpleSession CreateSession(object session)
		{
			return new SimpleObjectSession(this.ObjectManager);
		}

		#endregion |   Protected Override Methods   |

		#region |   Public Methods   |

		///// <summary>
		///// Async starts the monitor server instance.
		///// </summary>
		///// <returns>true if serfver is started successful, otherwise false.</returns>
		//public async Task<bool> StartMonitorServerAsync()
		//{
		//	if (this.monitorServer is null)
		//		this.monitorServer = new SimpleObjectMonitorServer(this);

		//	this.monitorServer.Port = this.MonitorPort;

		//	return await this.monitorServer.StartAsync();
		//}

		///// <summary>
		///// Async stops the monitor service, if started.
		///// </summary>
		//public async Task StopMonitorServerAsync()
		//{
		//	if (this.monitorServer != null)
		//		await this.monitorServer.StopAsync();
		//}

		public async ValueTask<IEnumerable<IAppSession>> GetAllSessions() => await this.Server.GetAsyncSessionContainer().GetSessionsAsync();

		public ServerObjectModelInfo? GetServerObjectModel(int tableId) => this.ObjectManager.GetServerObjectModel(tableId);

		public string GetGraphName(int graphKey) => this.ObjectManager.ModelDiscovery.GetGraphPolicyModel(graphKey)?.Name ?? String.Empty;

		public string GetRelationName(int relationKey) => this.ObjectManager.GetRelationModel(relationKey)?.Name ?? String.Empty;

		public string GetObjectName(int tableId, long objectId) => this.ObjectManager.GetObject(tableId, objectId)?.GetName() ?? String.Empty;


		#endregion |   Public Methods   |

		#region |   System Request Responses   |

		[AuthorizationNotRequired]
		[SystemRequestCommand((int)SystemRequest.GetProtocolVersion)]
		public ResponseArgs GetProtocolVersion(ISimpleSession session, PackageReader package) => new ProtocolVersionResponseArgs(this.ProtocolVersion);


		[AuthorizationNotRequired]
		[SystemRequestCommand((int)SystemRequest.AuthenticateSession)]
		public ResponseArgs AuthenticateSession(SimpleSession session, PackageReader package)
		{
			if (session.IsAuthenticated)
				return new AuthenticateSessionResponseArgs(true); //, session.UserId, session.Username);

			AuthenticateSessionRequestArgs requestArgs = (package.PackageArgs as AuthenticateSessionRequestArgs)!;
			bool isAuthenticated = this.ObjectManager.AuthenticateSession(requestArgs.Username, requestArgs.PasswordHash, out long userId);
			string username = requestArgs.Username;

			if (isAuthenticated)
				base.SessionIsAuthenticated(session as SimpleSession, userId, username);

			return new AuthenticateSessionResponseArgs(isAuthenticated);
		}

		protected override void OnSessionAuthenticated(SimpleSession session)
		{
			base.OnSessionAuthenticated(session);

			int maxTableId = this.ObjectManager.AllServerObjectModels.Max(item => item.TableId);
			
			this.SetIsObjectModelFetchedByTableIdArray(session, new bool[maxTableId + 1]); 
			
			var sessionAuthenticatedEventHandler = this.SessionAuthenticated;

			sessionAuthenticatedEventHandler?.Invoke(session);
			//this.sessionAuthenticatedInformer.Post(session);
		}


		[SystemRequestCommand((int)SystemRequest.GetServerObjectModel)]
		public ResponseArgs GetServerObjectModel(SimpleSession session, PackageReader package)
		{
			TableIdRequestArgs requestArgs = (package.PackageArgs as TableIdRequestArgs)!;
			ServerObjectModelInfo? serverObjectPropertyInfo = this.ObjectManager.GetServerObjectModel(requestArgs.TableId);
			ServerObjectModelResponseArgs result;

			if (serverObjectPropertyInfo != null)
			{
				result = new ServerObjectModelResponseArgs(serverObjectPropertyInfo);
				session.IsObjectModelFetchedByTableId[requestArgs.TableId] = true;
			}
			else
			{
				result = new ServerObjectModelResponseArgs();
				result.Status = PackageStatus.NoSuchData;
				result.ErrorMessage = $"The ServerObjectModelInfo cannot be found, TableId={requestArgs.TableId}";
			}

			return result;
		}


		[SystemRequestCommand((int)SystemRequest.GetAllServerObjectModels)]
		public ResponseArgs GetAllServerObjectModels(SimpleSession session, PackageReader package)
		{
			return this.allServerObjectModelsInfoResponseArgs;
		}


		[SystemRequestCommand((int)SystemRequest.GetObjectIdsTEMP)]
		public ResponseArgs GetObjectIdsTEMP(ISimpleSession session, PackageReader package)
		{
			TableIdRequestArgs requestArgs = (package.PackageArgs as TableIdRequestArgs)!;
			ServerObjectCache objectCache = (this.ObjectManager.GetObjectCache(requestArgs.TableId) as ServerObjectCache)!;
			List<long> objectIds = objectCache.GetObjectIds;

			return new ObjectIdsResponseArgs(objectIds);
		}


		[SystemRequestCommand((int)SystemRequest.GetObjectPropertyValues)]
		public ResponseArgs GetObjectPropertyValues(SimpleSession session, PackageReader package)
		{
			ObjectIdTableIdRequestArgs requestArgs = (package.PackageArgs as ObjectIdTableIdRequestArgs)!;
			SimpleObject? simpleObject = this.ObjectManager.GetObject(requestArgs.TableId, requestArgs.ObjectId)!;
			IEnumerable<PropertyIndexValuePair>? oldPropertyIndexValuePairs;
			
			if (simpleObject != null)
				oldPropertyIndexValuePairs = simpleObject.GetNonDefaultOldPropertyIndexValuePairs(propertySelector: pm => pm.PropertyIndex != 0 && pm.IsClientSeriazable); // Id is not needed
			else
				oldPropertyIndexValuePairs = null;

            return new ObjectPropertyValuesResponseArgs(requestArgs.TableId, oldPropertyIndexValuePairs);
		}


		[SystemRequestCommand((int)SystemRequest.GetGraphElementsWithObjects)]
		public ResponseArgs GetGraphElementsWithObjects(SimpleSession session, PackageReader package)
		{
			ParentGraphElementIdGraphKeyRequestArgs requestArgs = (package.PackageArgs as ParentGraphElementIdGraphKeyRequestArgs)!;
			SimpleObjectCollection<GraphElement> graphElements = this.ObjectManager.GetLocalGraphElements(requestArgs.GraphKey, requestArgs.ParentGraphElementId);
			GraphElementObjectPair[] graphElementsWithObjects = new GraphElementObjectPair[graphElements.Count()];
			List<ServerObjectModelInfo>? newServerObjectModels = null;
			//ServerObjectModelInfo[]? serverObjectModelInfos = null;

			for (int i = 0; i < graphElementsWithObjects.Length; i++)
			{
				GraphElement graphElement = graphElements[i];
				//IEnumerable<PropertyIndexValuePair> graphElementPropertyIndexValues = graphElement.GetNonDefaultOldPropertyIndexValuePairs(propertySelector: pm => pm.IsClientSeriazable);
				IEnumerable<PropertyIndexValuePair> simpleObjectPropertyIndexValues = (graphElement.SimpleObject != null) ? graphElement.SimpleObject!.GetNonDefaultOldPropertyIndexValuePairs(propertySelector: pm => pm.PropertyIndex != 0 && pm.IsClientSeriazable)
																														  : new PropertyIndexValuePair[0];
				// graphElement.SimpleObject.GetModel().TableInfo.TableId
				int tableId = (int)graphElement.GetPropertyValue(GraphElementModel.PropertyModel.ObjectTableId.PropertyIndex)!;
				long objectId = (long)graphElement.GetPropertyValue(GraphElementModel.PropertyModel.ObjectId.PropertyIndex)!;

				graphElementsWithObjects[i] = new GraphElementObjectPair(graphElement.Id, tableId, objectId, graphElement.HasChildren, simpleObjectPropertyIndexValues);

				if (!session.IsObjectModelFetchedByTableId[tableId])
				{
					newServerObjectModels ??= new List<ServerObjectModelInfo>();

					ServerObjectModelInfo? serverObjectModelInfo = this.ObjectManager.GetServerObjectModel(tableId);

					if (!newServerObjectModels.Contains(serverObjectModelInfo!))
					{
						newServerObjectModels.Add(serverObjectModelInfo!);
						session.IsObjectModelFetchedByTableId[tableId] = true;
					}
				}
			}

			return new GraphElementsWithObjectsResponseArgs(graphElementsWithObjects, newServerObjectModels);
		}


		[SystemRequestCommand((int)SystemRequest.GetOneToManyForeignObjectCollection)]
		public ResponseArgs GetOneToManyForeignObjectCollection(SimpleSession session, PackageReader package)
		{
			RelationKeyTableIdObjectIdRequestArgs requestArgs = (package.PackageArgs as RelationKeyTableIdObjectIdRequestArgs)!;
			SimpleObject? simpleObject = this.ObjectManager.GetObject(requestArgs.TableId, requestArgs.ObjectId)!;
			var collection = simpleObject?.GetOneToManyForeignObjectCollection(requestArgs.RealationKey);
			int count = collection?.Count ?? 0;
			List<long> objectIds = new List<long>(count);

			for (int i = 0; i < count; i++)
				objectIds.Add(collection![i].Id);

			return new ObjectIdsResponseArgs(objectIds);
		}


		[SystemRequestCommand((int)SystemRequest.GetSimpleObjectGraphElementByGraphKey)]
		public ResponseArgs GetSimpleObjectGraphElementByGraphKey(SimpleSession session, PackageReader package)
		{
			GraphKeyTableIdObjectIdRequestArgs requestArgs = (package.PackageArgs as GraphKeyTableIdObjectIdRequestArgs)!;
			SimpleObject? simpleObject = this.ObjectManager.GetObject(requestArgs.TableId, requestArgs.ObjectId);
			GraphElement? graphElement = simpleObject?.GetGraphElement(requestArgs.GraphKey);
			long graphElementId = graphElement?.Id ?? 0;
			
			return new ObjectIdResponseArgs(graphElementId);
		}


		[SystemRequestCommand((int)SystemRequest.GetOneToOneForeignObject)]
		public ResponseArgs GetOneToOneForeignObject(SimpleSession session, PackageReader package)
		{
			RelationKeyTableIdObjectIdRequestArgs requestArgs = (package.PackageArgs as RelationKeyTableIdObjectIdRequestArgs)!;
			SimpleObject? simpleObject = this.ObjectManager.GetObject(requestArgs.TableId, requestArgs.ObjectId);
			SimpleObject? result = simpleObject?.GetOneToOneForeignObject(requestArgs.RealationKey);
			
			return new TableIdObjectIdResponseArgs(result?.GetModel().TableInfo.TableId ?? 0, result?.Id ?? 0);
		}


		[SystemRequestCommand((int)SystemRequest.GetGroupMembershipCollection)]
		public ResponseArgs GetGroupMembershipCollection(SimpleSession session, PackageReader package)
		{
			RelationKeyTableIdObjectIdRequestArgs requestArgs = (package.PackageArgs as RelationKeyTableIdObjectIdRequestArgs)!;
			SimpleObject? simpleObject = this.ObjectManager.GetObject(requestArgs.TableId, requestArgs.ObjectId);
			var groupMembershipCollection = simpleObject?.GetGroupMemberCollection(requestArgs.RealationKey)?.GetGroupMembershipCollection();
			IList<long> objectIds = groupMembershipCollection?.GetObjectIds() ?? new long[0];

			return new ObjectIdsResponseArgs(objectIds); // new ReadOnlyList<long(objectIds)); // ReadOnlyList for awoiding unnessesery copying an objectIds array to new list
		}


		//[SystemRequestCommand((int)SystemRequest.GetObjectCacheRelationCollectionWithTableId)]
		//public ResponseArgs GetObjectCacheRelationCollectionWithTableId(SimpleSession session, PackageReader package)
		//{
		//	GetCacheRelationCollectionWithTableIdRequestArgs requestArgs = (package.PackageArgs as GetCacheRelationCollectionWithTableIdRequestArgs)!;
		//	ObjectCache objectCache = this.ObjectManager.GetObjectCache(requestArgs.ObjectCacheTableId)!;
		//	List<long> objectIds = objectCache.GetRelationCollectionObjectIds(requestArgs.TableIdPropertyIndex, requestArgs.TableId, requestArgs.ObjectIdPropertyIndex, requestArgs.ObjectId);

		//	return new ObjectIdsResponseArgs(objectIds);
		//}


		//[SystemRequestCommand((int)SystemRequest.GetObjectPropertyValues)]
		//public ResponseArgs GetObjectPropertyValues(SimpleSession session, PackageReader package)
		//{
		//	ObjectIdTableIdRequestArgs requestArgs = (package.PackageArgs as ObjectIdTableIdRequestArgs)!;
		//	SimpleObject? simpleObject = this.ObjectManager.GetObject(requestArgs.TableId, requestArgs.ObjectId);
		//	ServerObjectModelInfo? serverObjectModelInfo = this.ObjectManager.GetServerObjectModelInfo(requestArgs.TableId);
		//	object?[] oldPropertyValues = simpleObject.GetOldPropertyValues(serverObjectModelInfo.SerializablePropertyIndexes);

		//	return new PropertyIndexValuesResponseArgs(requestArgs.TableId, serverObjectModelInfo.SerializablePropertyIndexes, oldPropertyValues);
		//}


		//[SystemRequestCommand((int)SystemRequest.GetAndLoadPorpertyValues)]
		//public ResponseArgs GetAndLoadPorpertyValues(SimpleSession session, PackageInfo package)
		//{
		//	ObjectIdTableIdRequestArgs requestArgs = (package.PackageArgs as ObjectIdTableIdRequestArgs)!;
		//	SimpleObject? simpleObject = this.ObjectManager.GetObject(requestArgs.TableId, requestArgs.ObjectId);
		//	ServerObjectModelInfo? serverObjectModelInfo = this.ObjectManager.GetServerObjectModelInfo(requestArgs.TableId);

		//	return new GetAndLoadPropertyValuesResponseArgs(simpleObject, serverObjectModelInfo);
		//}


		[SystemRequestCommand((int)SystemRequest.TransactionRequest)]
		public ResponseArgs ProcessTransactionRequest(SimpleSession session, PackageReader package)
		{
			ProcessTransactionRequestArgs requestArgs = (package.PackageArgs as ProcessTransactionRequestArgs)!;
			ProcessTransactionRequestResult transactionInfo = this.ObjectManager.ProcessTransactionRequestFromClient(requestArgs.TransactionActionInfoList!);
			ProcessTransactionResponseArgs responseArgs;

			if (transactionInfo.TransactionResult.TransactionSucceeded)
			{
				responseArgs = new ProcessTransactionResponseArgs(transactionInfo.TransactionResult.TransactionId, transactionInfo.NewObjectIds);

				if (requestArgs.TransactionActionInfoList!.Count() > 0 && this.IsAnyAuthenticatedClient(exceptionSessionKey: session.SessionKey).GetAwaiter().GetResult())
				{
					//var transactionActions = this.ObjectManager.CreateClientSeriazableTransactionActionsFromTransactionRequests(transactionInfo.TransactionResult.TransactionRequests);
					var messageArgs = new TransactionCompletedMessageArgs(requestArgs.TransactionActionInfoList!);
					
					this.SendBroadcastSystemMessage((int)SystemMessage.TransactionCompleted, messageArgs, exceptionSessionKey: session.SessionKey).GetAwaiter().GetResult(); // .DoNotAwait(); //.ConfigureAwait(false); //.DoNotAwait();
					//this.SendBroadcastSystemMessage((int)SystemMessage.TransactionCompleted, messageArgs).GetAwaiter().GetResult(); // .DoNotAwait(); //.ConfigureAwait(false); //.DoNotAwait();
				}
			}
			else
			{
				responseArgs = new ProcessTransactionResponseArgs();
				requestArgs.Status = PackageStatus.Error;
				requestArgs.ErrorMessage = transactionInfo.InfoMessage;
			}

			return responseArgs;
		}

		#endregion |   System Request Responses   |

		#region |   System Messages   |

		//private void ObjectManager_TransactionSucceeded(object sender, TransactionInfoEventArgs e)
		//{
		//	if (this.IsAnyAuthenticatedClient(exceptionSessionKey: e).GetAwaiter().GetResult())
		//	{
		//		var messageArgs = new TransactionMessageArgs(e.Transaction, e.DatastoreActions);

		//		this.SendBroadcastSystemMessage((int)MonitorSystemMessage.TransactionFinished, messageArgs).GetAwaiter().GetResult(); // .DoNotAwait(); //.ConfigureAwait(false); //.DoNotAwait();
		//	}
		//}


		#endregion |   System Messages   |

		#region |   Protected Methods   |

		//protected override async ValueTask OnStart()
		//{
		//	//await this.StartMonitorServerAsync(); // will start monitor if required
		//	await base.OnStart();
		//}


		//protected override List<Assembly> GetPackageArgsAssemblyList()
		//{
		//	var list = base.GetPackageArgsAssemblyList();

		//	list.Add(Assembly.GetExecutingAssembly());

		//	return list;
		//}

		protected string GetTableName(int tableId) => this.ObjectManager.GetObjectModel(tableId)?.TableInfo.TableName ?? String.Empty;


		//protected override IServer CreateServer(IHostConfigurator hostConfigurator)
		//{
		//	return base.CreateServerInternal<SimpleObjectSession>(hostConfigurator);
		//}
		//protected override void OnNewSessionConnected(AppSession session)
		//{
		//	base.OnNewSessionConnected(session);
		//	this.MonitorServer.OnAppServer_NewSessionConnected(session);
		//}

		//protected override void OnSessionClosed(AppSession session, CloseReason reason)
		//{
		//	base.OnSessionClosed(session, reason);
		//	this.MonitorServer.OnAppServer_SessionClosed(session, reason);
		//}

		//protected override void OnSesionPackageJobAction(AppSession session, JobActionType jobActionType, PackageInfo receivedPackage, byte[] sentData)
		//{
		//	base.OnSesionPackageJobAction(session, jobActionType, receivedPackage, sentData);

		//	//System.Threading.Thread.Sleep(100);
		//	this.MonitorServer.OnAppServer_SessionPackageJobAction(session.SessionID, jobActionType, receivedPackage, sentData);
		//}

		//protected override void OnSesionPackageJobAction(AppSession session, JobActionType jobActionType, PackageInfo receivedPackage, ArraySegment<byte> sentPackageLength, ArraySegment<byte> sentPackageHeaderAnyBody)
		//{
		//	base.OnSesionPackageJobAction(session, jobActionType, receivedPackage, sentPackageLength, sentPackageHeaderAnyBody);
		//	this.RaisePackageJobAction(session != null ? session.SessionID : null, jobActionType, receivedPackage, sentPackageLength, sentPackageHeaderAnyBody);
		//}


		#endregion |   Protected Methods   |

		#region |   Private Methods   |


		#endregion |   Private Methods   |
	}
}
