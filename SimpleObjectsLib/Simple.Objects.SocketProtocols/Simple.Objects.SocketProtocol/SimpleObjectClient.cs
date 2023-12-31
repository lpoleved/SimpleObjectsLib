﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Simple;
using Simple.Collections;
using Simple.Serialization;
using Simple.Modeling;
using Simple.SocketEngine;
using Simple.Datastore;
using Simple.Objects;
using Simple.Security;
using System.Diagnostics;
using System.Threading.Tasks.Dataflow;

namespace Simple.Objects.SocketProtocol
{
	public class SimpleObjectClient : ClientBase, ISimpleObjectSession, ISimpleSession
	{
		private Version? protocolVersion;
		private HashArray<ServerObjectModelInfo?> serverObjectModelInfosByTableId = new HashArray<ServerObjectModelInfo?>();
		private ServerObjectModelInfo[]? serverObjectModelsArray = null;
		private ActionBlock<TransactionCompletedMessageArgs> onForeignTransactionCompletedActionBlock;
		private string username = String.Empty;

		//public static SimpleObjectClient Empty;

		public SimpleObjectClient(SimpleObjectManager objectManager)
		{
			ExecutionDataflowBlockOptions dataflowOptions = new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };

			this.ObjectManager = objectManager;
			this.RemoteDatastore = new RemoteDatastore(this);
			this.ObjectManager.SetClientWorkingMode(this.RemoteDatastore);
			this.onForeignTransactionCompletedActionBlock = new ActionBlock<TransactionCompletedMessageArgs>(args => this.ObjectManager!.OnForeignTransactionCompleted(args.TransactionActions!), dataflowOptions);
		}

		//public event ClientAuthenticatedEventHandler Authenticated;

		public SimpleObjectManager ObjectManager { get; private set; }
		public RemoteDatastore RemoteDatastore { get; private set; }

		public Version? ProtocolVersion
		{
			get
			{
				if (this.protocolVersion == null && this.IsConnected)
					this.protocolVersion = this.GetProtocolVersion().GetAwaiter().GetResult().ProtocolVersion;

				return this.protocolVersion;
			}
		}

		//protected override async ValueTask OnConnect()
		//{
		//	await base.OnConnect();
		//}

		//protected override List<Assembly> GetPackageArgsAssemblyList()
		//{
		//	var list = base.GetPackageArgsAssemblyList();

		//	list.Add(Assembly.GetExecutingAssembly());

		//	return list;
		//}


		//public ServerObjectPropertyInfo GetServerObjectPropertyInfo2(int tableId)
		//{
		//	ServerObjectPropertyInfo result = this.serverObjectPropertyModelInfosByTableId.GetValue(tableId);

		//	if (result == null)
		//	{
		//		RequestResult<ServerSeriazablePropertyModelInfo> requestResult = this.GetSeriazablePropertySequence(tableId);

		//		if (requestResult.Succeed)
		//		{
		//			ISystemPropertySequence serverPropertySequence = requestResult.ResultValue;
		//			SimpleObjectModel clientObjectModel = this.ObjectManager.GetObjectModel(tableId);

		//			result = new ServerSeriazablePropertyModelInfo(tableId, serverPropertySequence, clientObjectModel);
		//			this.serverObjectPropertyModelInfosByTableId[tableId] = result;
		//		}
		//	}

		//	return result;
		//}

		//public HashArray<IPropertySequence> ServerPropertySequencesByTableId
		//{
		//	get
		//	{
		//		if (this.serverPropertySequencesByTableId == null)
		//		{
		//			Dictionary<int, IPropertySequence> serverPropertySequencesByTableIdDictionary = this.GetServerPropertySequencesByTableId().ResultValue; // this.AppClient.GetServerPropertySequencesByTableId().ResultValue;

		//			this.serverPropertySequencesByTableId = new HashArray<IPropertySequence>(serverPropertySequencesByTableIdDictionary.Keys.Max() + 1);

		//			foreach (KeyValuePair<int, IPropertySequence> item in serverPropertySequencesByTableIdDictionary)
		//			{
		//				int tableId = item.Key;
		//				IPropertySequence propertySequence = item.Value;

		//				this.serverPropertySequencesByTableId[tableId] = propertySequence;

		//				// There is no need for PropertySequenceIds throught object models while client model can differ from server object model.
		//				// Thus client object models has a PropertySequenceId set to -1;
		//			}
		//		}

		//		return this.serverPropertySequencesByTableId;
		//	}
		//}

		//public void SendAsyncTest(int numOfRepeat, int sendIntervalInMiliseconds = 0)
		//{
		//	for (int i = 0; i < numOfRepeat; i++)
		//	{
		//		this.PackageEngine.SendRequestAsync<RequestArgs, GetProtocolVersionResponseArgs, Version>(
		//											(int)SystemRequest.GetProtocolVersion, null, 
		//											() => Activator.CreateInstance(typeof(GetProtocolVersionResponseArgs)) as GetProtocolVersionResponseArgs,
		//											args => args.ProtocolVersion, isSystem: true, isBroadcast: false, isMulticast: false);

		//		Thread.Sleep(sendIntervalInMiliseconds);
		//		//Task.Delay(sendIntervalInMiliseconds);
		//	}
		//}

		#region |   System Requests   |

		public async ValueTask<ProtocolVersionResponseArgs> GetProtocolVersion()
		{
			return await this.SendSystemRequest<ProtocolVersionResponseArgs>((int)SystemRequest.GetProtocolVersion);
		}

		public async ValueTask<AuthenticateSessionResponseArgs> AuthenticateSession(string username, string password)
		{
			if (this.IsAuthenticated)
				throw new Exception("Session already authenticated");

			string passwordHash = PasswordSecurity.HashPassword(password);
			var request = new AuthenticateSessionRequestArgs(username, passwordHash);
			var response = await this.SendSystemRequest<AuthenticateSessionResponseArgs>((int)SystemRequest.AuthenticateSession, request);

			this.IsAuthenticated = response.ResponseSucceeded && response.IsAuthenticated;

			if (this.IsAuthenticated)
			{
				//this.UserId = response.UserId;
				this.username = username;
				//await this.GetAllServerObjectModels();
				await GetServerObjectModel(GraphElementModel.TableId); // Cache GraphElement model 
			}

			return response;
		}

		//public ServerObjectModelInfo GetServerObjectModelByGetAll(int tableId) => this.serverObjectModelsArray![tableId];

		//public ServerObjectModelInfo GetServerObjectModelFast(int tableId) => this.serverObjectModelInfosByTableId[tableId]!;

		public async ValueTask<ServerObjectModelInfo?> GetServerObjectModel(int tableId)
		{
			ServerObjectModelInfo? serverObjectModelInfo = this.serverObjectModelInfosByTableId.GetValue(tableId);

			if (serverObjectModelInfo == null)
			{
				var request = new TableIdRequestArgs(tableId);
				var response = await this.SendSystemRequest<ServerObjectModelResponseArgs>((int)SystemRequest.GetServerObjectModel, request);

				if (!response.ResponseSucceeded)
					return null;

				serverObjectModelInfo = response.ServerObjectModelInfo;
				this.serverObjectModelInfosByTableId.SetValue(tableId, serverObjectModelInfo);
			}

			return serverObjectModelInfo;
		}


		public async ValueTask<AllServerObjectModelsResponseArgs> GetAllServerObjectModels()
		{
			var response = await this.SendSystemRequest<AllServerObjectModelsResponseArgs>((int)SystemRequest.GetAllServerObjectModels);

			this.serverObjectModelsArray = new ServerObjectModelInfo[response.ServerObjectModels!.Max(item => item.TableId) + 1];

			foreach (var item in response.ServerObjectModels!)
				this.serverObjectModelsArray[item.TableId] = item;

			return response;
		}

		public async ValueTask<ObjectIdsResponseArgs> GetObjectIdsTEMP(int tableId)
		{
			var request = new TableIdRequestArgs(tableId);
			var response = await this.SendSystemRequest<ObjectIdsResponseArgs>((int)SystemRequest.GetObjectIdsTEMP, request);

			return response;
		}

		public async ValueTask<ObjectPropertyValuesResponseArgs> GetObjectPropertyValues(int tableId, long objectId)
		{
			await this.GetServerObjectModel(tableId); // We must ensure that the object model is already fetched first

			var request = new ObjectIdTableIdRequestArgs(tableId, objectId);
			var response = await this.SendSystemRequest<ObjectPropertyValuesResponseArgs>((int)SystemRequest.GetObjectPropertyValues, request);

			return response;
		}

		public async ValueTask<GraphElementsWithObjectsResponseArgs> GetGraphElementsWithObjects(int graphKey, long parentGraphElementId)
		{
			var request = new ParentGraphElementIdGraphKeyRequestArgs(graphKey, parentGraphElementId);
			var response = await this.SendSystemRequest<GraphElementsWithObjectsResponseArgs>((int)SystemRequest.GetGraphElementsWithObjects, request);

			return response;
		}


		public async ValueTask<ObjectIdsResponseArgs> GetOneToManyForeignObjectCollection(int tableId, long objectId, int relationKey)
		{
			var request = new RelationKeyTableIdObjectIdRequestArgs(tableId, objectId, relationKey);
			var response = await this.SendSystemRequest<ObjectIdsResponseArgs>((int)SystemRequest.GetOneToManyForeignObjectCollection, request);

			return response;
		}

		public async ValueTask<ObjectIdsResponseArgs> GetGroupMembershipCollection(int tableId, long objectId, int relationKey)
		{
			var request = new RelationKeyTableIdObjectIdRequestArgs(tableId, objectId, relationKey);
			var response = await this.SendSystemRequest<ObjectIdsResponseArgs>((int)SystemRequest.GetOneToManyForeignObjectCollection, request);

			return response;
		}


		public async ValueTask<ObjectIdResponseArgs> GetSimpleObjectGraphElementIdByGraphKey(int tableId, long objectId, int graphKey)
		{
			var request = new GraphKeyTableIdObjectIdRequestArgs(tableId, objectId, graphKey);
			var response = await this.SendSystemRequest<ObjectIdResponseArgs>((int)SystemRequest.GetSimpleObjectGraphElementByGraphKey, request);

			return response;
		}

		public async ValueTask<TableIdObjectIdResponseArgs> GetOneToForeignObject(int tableId, long objectId, int graphKey)
		{
			var request = new GraphKeyTableIdObjectIdRequestArgs(tableId, objectId, graphKey);
			var response = await this.SendSystemRequest<TableIdObjectIdResponseArgs>((int)SystemRequest.GetOneToOneForeignObject, request);

			return response;
		}


		//public async ValueTask<PropertyIndexValuesResponseArgs> GetObjectPropertyValuesFromServer(int tableId, long objectId)
		//{
		//	ObjectIdTableIdRequestArgs request = new ObjectIdTableIdRequestArgs(tableId, objectId);
		//	ServerObjectModelInfo? serverObjectModelInfo = await this.GetServerObjectModelInfo(tableId);
		//	PropertyIndexValuesResponseArgs response;

		//	if (serverObjectModelInfo != null)
		//	{
		//		response = await this.SendSystemRequest<PropertyIndexValuesResponseArgs>((int)SystemRequest.GetObjectPropertyValues, request);
		//	}
		//	else
		//	{
		//		response = new PropertyIndexValuesResponseArgs();
		//		response.Status = PackageStatus.NoSuchData;
		//		response.ErrorMessage = $"Server Object Model info cannot be found (TableId={tableId}";
		//	}

		//	return response;
		//}


		//public async ValueTask<bool> LoadPropertyValuesFromServer2(SimpleObject simpleObject)
		//{
		//	int tableId = simpleObject.GetModel().TableInfo.TableId;
		//	ObjectIdTableIdRequestArgs request = new ObjectIdTableIdRequestArgs(tableId, simpleObject.Id);
		//	ServerObjectModelInfo? serverObjectModelInfo = await this.GetServerObjectModelInfo(tableId);
		//	PropertyIndexValuesResponseArgs response;

		//	if (serverObjectModelInfo != null)
		//	{
		//		response = await this.SendSystemRequest<PropertyIndexValuesResponseArgs>((int)SystemRequest.GetObjectPropertyValues, request);

		//		if (response.ResponseSucceeded)
		//		{
		//			simpleObject.LoadFromServer(response.PropertyIndexes!, response.PropertyValues!, serverObjectModelInfo);

		//			return true;
		//		}
		//		else
		//		{
		//			return false;
		//		}
		//	}
		//	else
		//	{
		//		//response = new PropertyIndexValuesResponseArgs(RequestStatus.NoSuchData, $"Server Object Model info cannot be found (TableId={tableId}");

		//		return false;
		//	}
		//}

		//public async ValueTask<bool> LoadPropertyValuesFromServer3(SimpleObject simpleObject)
		//{
		//	int tableId = simpleObject.GetModel().TableInfo.TableId;
		//	ObjectIdTableIdRequestArgs request = new ObjectIdTableIdRequestArgs(tableId, simpleObject.Id);
		//	ServerObjectModelInfo? serverObjectModelInfo = await this.GetServerObjectModelInfo(tableId);
		//	GetAndLoadPropertyValuesResponseArgs response;

		//	if (serverObjectModelInfo != null)
		//	{
		//		response = await this.SendSystemRequest((int)SystemRequest.GetAndLoadPorpertyValues, request, new GetAndLoadPropertyValuesResponseArgs(simpleObject, serverObjectModelInfo));

		//		return response.ResponseSucceeded;
		//	}
		//	else
		//	{
		//		return false;
		//	}
		//}

		public async ValueTask<ClientTransactionResult> SendTransactionRequest(IEnumerable<TransactionActionInfo> transactionActionInfoList, Func<int, ISimpleObjectModel> getClientObjectModelByTableId, Func<int, ServerObjectModelInfo> getServerObjectPropertyInfoByTableId)
		{
			ProcessTransactionRequestArgs request = new ProcessTransactionRequestArgs(transactionActionInfoList); //, getClientObjectModelByTableId, getServerObjectPropertyInfoByTableId);
			ClientTransactionResult result;

			var response = await this.SendSystemRequest<ProcessTransactionResponseArgs>((int)SystemRequest.TransactionRequest, request);

			if (response.ResponseSucceeded)
			{
				if (response.TransactionSucceeded)
					result = new ClientTransactionResult(request.TempClientObjectIds!, response.NewObjectIds!, request.SimpleObjectPropertyWithTempObjectIdsNeedsToBeChangedToPositives!);
				else
					result = new ClientTransactionResult(response.MessageInfo);
			}
			else
			{
				result = new ClientTransactionResult(response.ErrorMessage);
			}

			return result;
		}

		#endregion |   System Requests   |

		#region |   System Message Receivers   |




		[SystemMessageCommand((int)SystemMessage.TransactionCompleted)]
		protected void MessageReceive_TransactionCompleted(ISimpleSession session, PackageReader packageInfo)
		{
			if (packageInfo.PackageArgs is TransactionCompletedMessageArgs args) // && args.TransactionActions != null)
				this.onForeignTransactionCompletedActionBlock.Post(args);
				//this.ObjectManager.OnForeignTransactionCompleted(args.TransactionActions, requester: this);

				//if (!result.TransactionSucceeded)
				//	Debug.WriteLine("Transaction completed from server but not on client: " + result.InfoMessage);
		}


		//protected override void OnSesionPackageJobAction(AppSession session, JobActionType jobActionType, PackageInfo receivedPackage, byte[] sentData)
		//{
		//	base.OnSesionPackageJobAction(session, jobActionType, receivedPackage, sentData);

		//	// TODO: If needed
		//	//this.MonitorServer.OnAppServer_SessionPackageJobAction(session.SessionID, jobActionType, receivedPackage, sentData);
		//}


		//[SystemMessageReceiver((int)SystemMessage.SetPropertySequences)]
		//public void GetServerPropertySequences(SerializationReader reader)
		//{
		//	ISimpleObjectModel[] modelCollection = this.ObjectManager.ModelDiscovery.GetObjectModelCollection().ToArray();
		//	Dictionary<int, PropertySequence> publicServerPropertySequencesByTableIdDictionary = new Dictionary<int, PropertySequence>(modelCollection.Length);
		//	this.publicServerPropertySequencesByTableId = new HashList<PropertySequence>(modelCollection.Max(item => item.TableInfo.TableId));

		//	foreach (ISimpleObjectModel objectModel in modelCollection)
		//	{
		//		publicServerPropertySequencesByTableIdDictionary.Add(objectModel.TableInfo.TableId, objectModel.PublicPropertySequence);
		//		this.publicServerPropertySequencesByTableId.SetValue(objectModel.TableInfo.TableId, objectModel.PublicPropertySequence);
		//	}



		//	SetPropertySequencesMessageArgs args = new SetPropertySequencesMessageArgs(reader);

		//	//TODO: Make GetServerObjectPropertySequences by TableId and update in object model ServerPublicPropertySequences property according

		//	this.ServerPublicPropertySequencesByTableId = args.ServerPublicPropertySequencesByTableId;

		//	foreach (ObjectModelPropertyIndexTypeSequence objectModelPropertyIndexTypeSequence in args.ObjectModelPropertyIndexTypeSequences)
		//	{
		//		SimpleObjectModel objectModel = this.ObjectManager.GetObjectModel(objectModelPropertyIndexTypeSequence.TableId);
		//		int[] storablePropertyIndexSequence = new int[objectModelPropertyIndexTypeSequence.StorablePropertyIndexTypeSequence.Length];
		//		IPropertyModel[] storablePropertyModelSequence = new IPropertyModel[storablePropertyIndexSequence.Length];
		//		int[] publicPropertyIndexSequence = new int[objectModelPropertyIndexTypeSequence.PublicPropertyIndexTypeSequence.Length];

		//		objectModel.StorablePropertySequenceId = objectModelPropertyIndexTypeSequence.StorablePropertyIndexSequenceId;

		//		for (int i = 0; i < storablePropertyModelSequence.Length; i++) // PropertyIndexTypePair item in objectModelPropertyIndexTypeSequence.PublicPropertyIndexTypeSequence)
		//		{
		//			//TODO // 1. U modelu implementirati PropertyIndexTypePair[] PublicPropertyIndexTypeSequence
		//			//	 // 2. U modelu implementirati PropertyIndexTypePair[] StorablePropertyIndexTypeSequence osim StorablePropertyIndexSequenceId

		//			PropertyIndexTypePair item = objectModelPropertyIndexTypeSequence.StorablePropertyIndexTypeSequence[i];

		//			PropertyModel propertyModel = objectModel.PropertyModels[item.PropertyIndex];

		//			propertyModel.ServerPropertyTypeId = item.PropertyTypeId;
		//			storablePropertyIndexSequence[i] = item.PropertyIndex;
		//			storablePropertyModelSequence[i] = objectModel.PropertyModels[item.PropertyIndex];
		//		}

		//		objectModel.StorablePropertyIndexSequence = storablePropertyIndexSequence;
		//		objectModel.StorablePropertyModelSequence = storablePropertyModelSequence;

		//		for (int i = 0; i < publicPropertyIndexSequence.Length; i++) // PropertyIndexTypePair item in objectModelPropertyIndexTypeSequence.PublicPropertyIndexTypeSequence)
		//		{
		//			//TODO // 1. U modelu implementirati PropertyIndexTypePair[] PublicPropertyIndexTypeSequence
		//			//	 // 2. U modelu implementirati PropertyIndexTypePair[] StorablePropertyIndexTypeSequence osim StorablePropertyIndexSequenceId

		//			PropertyIndexTypePair item = objectModelPropertyIndexTypeSequence.PublicPropertyIndexTypeSequence[i];

		//			PropertyModel propertyModel = objectModel.PropertyModels[item.PropertyIndex];

		//			propertyModel.ServerPropertyTypeId = item.PropertyTypeId;
		//			publicPropertyIndexSequence[i] = item.PropertyIndex;
		//		}

		//		objectModel.PublicPropertyIndexSequence = publicPropertyIndexSequence;
		//	}
		//}

		#endregion |   System Message Receives   |

		#region |   Public Properties   |

		//public bool Connect(EndPoint endPoint)
		//{
		//	bool connected = false;
		//	int sleepInterval = 500;
		//	int repeat = this.Timeout * 1000 / sleepInterval;

		//	this.ConnectAsync(endPoint);

		//	for (int i = 0; i < repeat; i++)
		//	{
		//		if (this.IsConnected)
		//		{
		//			connected = true;
		//			break;
		//		}
		//		else
		//		{
		//			Thread.Sleep(sleepInterval);
		//		}
		//	}

		//	if (connected)
		//		this.ProtocolVersion = this.GetProtocolVersion().ResultValue;

		//	return connected;
		//}

		//public new Task<bool> Close()
		//{
		//	var result = base.Close();

		//	this.ProtocolVersion = null;

		//	return result;
		//}

		#endregion |   Public Properties   |

		#region |   Public methods   |

		//public override void InitializeProtocol()
		//{
		//	base.InitializeProtocol();

		//	this.ProtocolVersion = this.GetProtocolVersion().ResultValue;
		//}

		#endregion |   Public methods   |

		#region |   Protected Methods   |

		protected override object GetCommandOwnerInstance() => this;

		//protected override void OnConnected()
		//{
		//	base.OnConnected();

		//	Thread thread = new Thread(() => this.ProtocolVersion = this.GetProtocolVersion().ResultValue);

		//	thread.IsBackground = true;
		//	thread.Priority = ThreadPriority.Normal;
		//	thread.Start();
		//}

		#endregion |   Protected Methods   |

		#region |   ISimpleObjectSession, Interface Implementation   |

		ServerObjectModelInfo? ISimpleObjectSession.GetServerObjectModel(int tableId)
		{
			return this.GetServerObjectModel(tableId).GetAwaiter().GetResult();
		}

		void ISimpleObjectSession.SetServerObjectModel(int tableId, ServerObjectModelInfo? serverObjectModelInfo)
		{
			this.serverObjectModelInfosByTableId.SetValue(tableId, serverObjectModelInfo);
		}

		#endregion |   ISimpleObjectSession, Interface Implementation   |
	}

	#region |   Public Delegates   |

	//public delegate void ClientAuthenticatedEventHandler(string username);

	#endregion |   Public Delegates   |
}
