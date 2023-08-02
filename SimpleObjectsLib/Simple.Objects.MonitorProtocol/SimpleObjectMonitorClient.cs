using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Simple;
using Simple.Collections;
using Simple.Serialization;
using Simple.SocketEngine;
using Simple.Datastore;
using Simple.Modeling;
using Simple.Objects;
using Simple.Objects.SocketProtocol;
using SuperSocket;
using SuperSocket.Client;
using System.Formats.Asn1;
using Simple.Security;
using System.Threading.Tasks.Dataflow;

namespace Simple.Objects.MonitorProtocol
{
	public class SimpleObjectMonitorClient : ClientBase, ISimpleObjectSession, ISimpleSession
	{
		protected override object GetCommandOwnerInstance() => this;

		private Version? protocolVersion = null;
		//private SimpleObjectModelDiscovery modelDiscovery = null;
		////private HashList<ISystemPropertySequence> systemPropertySequencesBySequenceId = null;
		private HashArray<ServerObjectModelInfo?> serverObjectModelInfosByTableId = new HashArray<ServerObjectModelInfo?>();
		private HashArray<string> graphNamesByGraphKey = new HashArray<string>();
		private HashArray<string> relationNamesByRelationKey = new HashArray<string>();
		private string username = String.Empty;
		//private Dictionary<int, IPropertySequence> storableSeriazablePropertySequencesByPropertySequenceId = new Dictionary<int, IPropertySequence>();


		private ActionBlock<SessionConnectedMessageArgs> sessionConnectedActionBlock;
		private ActionBlock<SessionClosedMessageArgs> sessionClosedActionBlock;
		private ActionBlock<SessionAuthenticatedMessageArgs> sessionAuthenticatedActionBlock;
		private ActionBlock<MessageSessionMessageArgs> messageSentActionBlock;
		private ActionBlock<MessageMessageArgs> brodcastMessageSentActionBlock;
		private ActionBlock<MessageSessionMessageArgs> messageReceivedActionBlock;
		private ActionBlock<RequestResponseSessionMessageArgs> requestReceivedActionBlock;
		private ActionBlock<RequestResponseSessionMessageArgs> requestSentActionBlock;
		private ActionBlock<SessionErrorPackageInfoMessageArgs> packageProcessingErrorActionBlock;
		private ActionBlock<TransactionMessageArgs> transactionFinishedActionBlock;

		//public event SessionEventMessageHandler NewSessionConnected;
		//public event SessionClosedMessageEventHandler SessionClosed;
		//public event SessionAuthorizedMessageEventHandler SessionAuthorized;
		////public event SessionPackageEventHandler ServicePackageReceived;
		////public event SessionPackageEventHandler ServicePackageSent;
		//public event SessionPackageJobActionMessageEventHandler SessionPackageJobAction;
		////public event TestPackageSentEventHandler ServiceTestPackageSent;
		//public event TransactionMessageEventHandler TransactionProcessed;

		//public SimpleObjectMonitorClient(IList<Assembly> assemblies)
		//	: this(new SimpleObjectModelDiscovery(assemblies))
		//{
		//}
		//public static SimpleObjectMonitorClient Empty;
		//public SimpleObjectMonitorClient(SimpleObjectModelDiscovery modelDiscovery)
		//{
		//	this.ReceiveBufferSize = 65536;
		//	this.modelDiscovery = modelDiscovery;
		//}

		public SimpleObjectMonitorClient()
		{
			ExecutionDataflowBlockOptions dataflowOptions = new ExecutionDataflowBlockOptions();

			dataflowOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;
			//dataflowOptions.MaxMessagesPerTask = 1;

			this.sessionConnectedActionBlock = new ActionBlock<SessionConnectedMessageArgs>(args => this.SessionConnected?.Invoke(args), dataflowOptions);
			this.sessionClosedActionBlock = new ActionBlock<SessionClosedMessageArgs>(args => this.SessionClosed?.Invoke(args), dataflowOptions);
			this.sessionAuthenticatedActionBlock = new ActionBlock<SessionAuthenticatedMessageArgs>(args => this.SessionAuthenticated?.Invoke(args), dataflowOptions);
			this.messageSentActionBlock = new ActionBlock<MessageSessionMessageArgs>(args => this.MessageSent?.Invoke(args), dataflowOptions);
			this.brodcastMessageSentActionBlock = new ActionBlock<MessageMessageArgs>(args => this.BrodcastMessageSent?.Invoke(args), dataflowOptions);
			this.messageReceivedActionBlock = new ActionBlock<MessageSessionMessageArgs>(args => this.MessageReceived?.Invoke(args), dataflowOptions);
			this.requestReceivedActionBlock = new ActionBlock<RequestResponseSessionMessageArgs>(args => this.RequestReceived?.Invoke(args), dataflowOptions);
			this.requestSentActionBlock = new ActionBlock<RequestResponseSessionMessageArgs>(args => this.RequestSent?.Invoke(args), dataflowOptions);
			this.packageProcessingErrorActionBlock = new ActionBlock<SessionErrorPackageInfoMessageArgs>(args => this.PackageProcessingError?.Invoke(args), dataflowOptions);
			this.transactionFinishedActionBlock = new ActionBlock<TransactionMessageArgs>(args => this.TransactionFinished?.Invoke(args), dataflowOptions);
		}


		//protected override PackageArgsFactory CreatePackageArgsFactory() => new PackageArgsFactory(new List<Assembly>() { this.GetType().Assembly });
		//protected override PackageArgsFactory CreatePackageArgsFactory() => new PackageArgsFactory(AppDomain.CurrentDomain.GetAssemblies());


		//public SimpleObjectModelDiscovery ModelDiscovery
		//{
		//	get { return this.modelDiscovery; }
		//}

		//public event ClientAuthenticatedEventHandler Authenticated;
		public event EmptyEventHandler? ServerStarted;
		public event EmptyEventHandler? ServerStopped;
		public event SessionConnectedEventHandler? SessionConnected;
		public event SessionClosedEventHandler? SessionClosed;
		public event SessionAuthenticatedEventHandler? SessionAuthenticated;
		public event SessionMessageSentEventHandler? MessageSent;
        public event MessageEventHandler? BrodcastMessageSent;
		public event SessionMessageSentEventHandler? MessageReceived;
		public event SessionResponsePackageInfodEventHandler? RequestSent;
		public event SessionResponsePackageInfodEventHandler? RequestReceived;
		public event SessionErrorPackageInfoEventHandler? PackageProcessingError;
		public event TransactionEventHandler? TransactionFinished;

		public Version? ProtocolVersion
		{
			get
			{
				if (this.protocolVersion == null && this.IsConnected)
				{
					var response = this.GetProtocolVersion().GetAwaiter().GetResult();

					this.protocolVersion = (response.ResponseSucceeded) ? response.ProtocolVersion : null;
				}
				else if (!this.IsConnected)
				{
					this.protocolVersion = null;
				}

				return this.protocolVersion;
			}
		}

		public string Username => this.username;

		protected override PackageArgsFactory CreatePackageArgsFactory() => new MonitorPackageArgsFactory(this.GetPackageArgsAssemblies());

		//public ServerObjectModelInfo GetServerObjectModel(int tableId) => this.GetServerObjectModelFromServer(tableId).GetAwaiter().GetResult();


		//protected override IEnumerable<Assembly> GetPackageArgsAssemblies()
		//{
		//	return base.GetPackageArgsAssemblies().Append(typeof(AuthenticateSessionRequestArgs).Assembly); // Add Simple.Objects.SocketProtocol Args assembly
		//}

		//protected override List<Assembly> GetPackageArgsAssemblyList()
		//{
		//	var list = base.GetPackageArgsAssemblyList();

		//	list.Add(this.GetType().Assembly);

		//	return list;
		//}

		#region |   App Requests   |

		public async ValueTask<ProtocolVersionResponseArgs> GetProtocolVersion()
		{
			return await this.SendSystemRequest<ProtocolVersionResponseArgs>((int)MonitorSystemRequest.GetProtocolVersion);
		}

		public async ValueTask<AuthenticateSessionResponseArgs> AuthenticateSession(string username, string password)
		{
			string passwordHash = PasswordSecurity.HashPassword(password);
			var requestArgs = new AuthenticateSessionRequestArgs(username, passwordHash);
			var response = await this.SendSystemRequest<AuthenticateSessionResponseArgs>((int)MonitorSystemRequest.AuthenticateSession, requestArgs);

			this.IsAuthenticated = response.ResponseSucceeded && response.IsAuthenticated;

			if (response.IsAuthenticated)
			{
				//this.UserId = response.UserId;
				this.username = username;
			}

			return response;
		}

		public async ValueTask<ServerState> GetServerState()
		{
			var response = await this.SendSystemRequest<ServerStateResponseArgs>((int)MonitorSystemRequest.GetServerState);

			return (response.ResponseSucceeded) ? response.ServerState : ServerState.Failed;
		}

		public async ValueTask<ServerState> StartServer()
		{ 
			var response = await this.SendSystemRequest<ServerStateResponseArgs>((int)MonitorSystemRequest.StartServer);

			return (response.ResponseSucceeded) ? response.ServerState : ServerState.Failed;
		}

		public async ValueTask<ServerState> StopServer()
		{
			var response = await this.SendSystemRequest<ServerStateResponseArgs>((int)MonitorSystemRequest.StopServer);

			return (response.ResponseSucceeded) ? response.ServerState : ServerState.Failed;
		}

		public async ValueTask<SessionInfosResponseArgs> GetSessionInfos()
		{
			return await this.SendSystemRequest<SessionInfosResponseArgs>((int)MonitorSystemRequest.GetSessionInfos);
		}

		public async ValueTask<ServerObjectModelInfo?> GetServerObjectModel(int tableId)
		{
			ServerObjectModelInfo? serverObjectPropertyInfo = this.serverObjectModelInfosByTableId.GetValue(tableId);

			if (serverObjectPropertyInfo == null)
			{
				var request = new TableIdRequestArgs(tableId);
				var response = await this.SendSystemRequest<ServerObjectModelResponseArgs>((int)MonitorSystemRequest.GetServerObjectModel, request);

				if (!response.ResponseSucceeded)
					return null;
					
				serverObjectPropertyInfo = response.ServerObjectModelInfo;
				this.serverObjectModelInfosByTableId[tableId] = serverObjectPropertyInfo;
			}

			return serverObjectPropertyInfo;
		}

		public async ValueTask<string> GetGraphName(int graphKey)
		{
			string? graphName = this.graphNamesByGraphKey.GetValue(graphKey);

			if (graphName is null)
			{
				var request = new GraphKeyRequestArgs(graphKey);
				var response = await this.SendSystemRequest<NameResponseArgs>((int)MonitorSystemRequest.GetGraphName, request);

				if (!response.ResponseSucceeded)
					return String.Empty;

				graphName = response.Name;
				this.graphNamesByGraphKey.SetValue(graphKey, graphName);
			}

			return graphName;
		}

		public async ValueTask<string> GetRelationName(int relationKey)
		{
			string? relationName = this.relationNamesByRelationKey.GetValue(relationKey);

			if (relationName is null)
			{
				var request = new RelationKeyRequestArgs(relationKey);
				var response = await this.SendSystemRequest<NameResponseArgs>((int)MonitorSystemRequest.GetRelationName, request);

				if (!response.ResponseSucceeded)
					return String.Empty;

				relationName = response.Name;
				this.relationNamesByRelationKey.SetValue(relationKey, relationName);
			}

			return relationName;
		}

		public async ValueTask<string> GetObjectName(int tableId, long objectName)
		{
			var request = new ObjectIdTableIdRequestArgs(tableId, objectName);
			var response = await this.SendSystemRequest<NameResponseArgs>((int)MonitorSystemRequest.GetObjectName, request);

			if (!response.ResponseSucceeded)
				return String.Empty;

			return response.Name;
		}


		//[AuthorizationNotRequired]
		//public RequestResult<bool> Authorize(string username, string password)
		//{
		//	return this.SendRequest<AuthorizeSessionRequestArgs, AuthorizeSessionResponseArgs, bool>((int)ServiceMonitorRequest.Authorize,
		//																		 () => new AuthorizeSessionRequestArgs(username, password),
		//																		 args => args.IsAuthorized);
		//}

		//public RequestResult<SessionInfo[]> GetSessions()
		//{
		//	return this.SendRequest<GetSessionsResponseArgs, SessionInfo[]>((int)ServiceMonitorRequest.GetSessions, args => args.Sessions);
		//}

		//public RequestResult<IPropertySequence> GetTransactionActionPropertySequence(int propertySequenceId)
		//{
		//	return this.SendRequest<GetTransactionActionPropertySequenceRequestArgs, GetTransactionActionPropertySequenceResponseArgs, IPropertySequence>((int)ServiceMonitorRequest.GetTransactionActionPropertySequence,
		//																																						() => new GetTransactionActionPropertySequenceRequestArgs(propertySequenceId),
		//																																						args => args.PropertySequence);
		//}

		//public RequestResult<ServerObjectModelInfo> GetServerObjectModelInfo(int tableId)
		//{
		//	return this.SendRequest<GetServerObjectModelInfoRequestArgs, GetServerObjectModelInfoResponseArgs, ServerObjectModelInfo>((int)ServiceMonitorRequest.GetServerObjectModelInfo,
		//																															  () => new GetServerObjectModelInfoRequestArgs(tableId),
		//																															  args => args.ServerObjectModelInfo);
		//}


		////private RequestResult<ISystemPropertySequence[]> GetSystemPropertySequences()
		////{
		////	return this.SendRequest<GetSystemPropertySequencesResponseArgs, ISystemPropertySequence[]>((int)ServiceMonitorRequest.GetSystemPropertySequences, args => args.SystemPropertySequences);
		////}


		#endregion |   App Requests   |

		#region |   Message Receives   |

		[SystemMessageCommand((int)MonitorSystemMessage.ServerSrarted)]
		protected void MessageReceive_ServerStarted(ISimpleSession session, PackageReader packageInfo) => this.ServerStarted?.Invoke();

		[SystemMessageCommand((int)MonitorSystemMessage.ServerStopped)]
		protected void MessageReceive_ServerStopped(ISimpleSession session, PackageReader packageInfo) => this.ServerStopped?.Invoke();

		[SystemMessageCommand((int)MonitorSystemMessage.SessionConnected)]
		protected void MessageReceive_SessionConnected(ISimpleSession session, PackageReader packageInfo)
		{
			if (this.SessionConnected != null && packageInfo.PackageArgs is SessionConnectedMessageArgs args)
				this.sessionConnectedActionBlock.Post(args);
		}

		[SystemMessageCommand((int)MonitorSystemMessage.SessionClosed)]
		protected void MessageReceive_SessionClosed(ISimpleSession session, PackageReader packageInfo)
		{
			if (this.SessionClosed != null && packageInfo.PackageArgs is SessionClosedMessageArgs args)
				this.sessionClosedActionBlock.Post(args);
		}

		[SystemMessageCommand((int)MonitorSystemMessage.SessionAuthenticated)]
		protected void MessageReceive_SessionAuthenticated(ISimpleSession session, PackageReader packageInfo)
		{
			if (this.SessionAuthenticated != null && packageInfo.PackageArgs is SessionAuthenticatedMessageArgs args)
				this.sessionAuthenticatedActionBlock.Post(args);
		}

		[SystemMessageCommand((int)MonitorSystemMessage.MessageSent)]
		protected void MessageReceive_MessageSent(ISimpleSession session, PackageReader packageInfo)
		{
			if (this.MessageSent != null && packageInfo.PackageArgs is MessageSessionMessageArgs args)
				this.messageSentActionBlock.Post(args);
		}

		[SystemMessageCommand((int)MonitorSystemMessage.BrodcastMessageSent)]
		protected void MessageReceive_BrodcastMessageSent(ISimpleSession session, PackageReader packageInfo)
		{
			if (this.BrodcastMessageSent != null && packageInfo.PackageArgs is MessageMessageArgs args)
				this.brodcastMessageSentActionBlock.Post(args);
		}

		[SystemMessageCommand((int)MonitorSystemMessage.MessageReceived)]
		protected void MessageReceive_MessageReceived(ISimpleSession session, PackageReader packageInfo)
		{
			if (this.MessageReceived != null && packageInfo.PackageArgs is MessageSessionMessageArgs args)
				this.messageReceivedActionBlock.Post(args);
		}

		[SystemMessageCommand((int)MonitorSystemMessage.RequestReceived)]
		protected void MessageReceive_RequestReceived(ISimpleSession session, PackageReader packageInfo)
		{
			if (this.RequestReceived != null && packageInfo.PackageArgs is RequestResponseSessionMessageArgs args)
				this.requestReceivedActionBlock.Post(args);
		}

		[SystemMessageCommand((int)MonitorSystemMessage.RequestSent)]
		protected void MessageReceive_RequestSent(ISimpleSession session, PackageReader packageInfo)
		{
			if (this.RequestSent != null && packageInfo.PackageArgs is RequestResponseSessionMessageArgs args)
				this.requestSentActionBlock.Post(args);
		}

		[SystemMessageCommand((int)MonitorSystemMessage.PackageProcessingError)]
		protected void MessageReceive_PackageProcessingError(ISimpleSession session, PackageReader packageInfo)
		{
			if (this.PackageProcessingError != null && packageInfo.PackageArgs is SessionErrorPackageInfoMessageArgs args)
				this.packageProcessingErrorActionBlock.Post(args);
		}

		[SystemMessageCommand((int)MonitorSystemMessage.TransactionFinished)]
		protected void MessageReceive_TransactionFinished(ISimpleSession session, PackageReader packageInfo)
		{
			if (this.TransactionFinished != null && packageInfo.PackageArgs is TransactionMessageArgs args)
				this.transactionFinishedActionBlock.Post(args);
		}

		#endregion |   Message Receives   |

		//#region |   Public Methods   |

		//public ServerObjectModelInfo GetServerObjectModelInfoFromCache(int tableId)
		//{
		//	ServerObjectModelInfo serverObjectPropertyInfo = this.serverObjectPropertyInfosByTableId.GetValue(tableId);

		//	if (serverObjectPropertyInfo == null)
		//	{
		//		var requestResult = this.GetServerObjectModelInfo(tableId);

		//		if (requestResult.Succeeded)
		//		{
		//			serverObjectPropertyInfo = requestResult.ResultValue;
		//			this.serverObjectPropertyInfosByTableId[tableId] = serverObjectPropertyInfo;
		//		}
		//	}

		//	return serverObjectPropertyInfo;
		//}

		//public IPropertySequence GetPropertySequenceFromCache(int propertySequenceId)
		//{
		//	IPropertySequence result;

		//	if (!this.storableSeriazablePropertySequencesByPropertySequenceId.TryGetValue(propertySequenceId, out result))
		//	{
		//		result = this.GetTransactionActionPropertySequence(propertySequenceId).ResultValue;
		//		this.storableSeriazablePropertySequencesByPropertySequenceId.Add(propertySequenceId, result);
		//	}

		//	return result;
		//}

		//#endregion |   Public Methods   |


		//#region |   Internal Methods   |

		//#endregion |   Internal Methods   |

		//#region |   Protected Methods   |

		////protected override void OnConnected()
		////{
		////	base.OnConnected();

		////	//Thread thread = new Thread(() =>
		////	Task.Run(() =>
		////	{
		////		var pv = this.GetProtocolVersion();
		////		this.ProtocolVersion = pv.ResultValue; // this.GetProtocolVersion().ResultValue;

		////		//var ps = this.GetSystemPropertySequences();
		////		//ISystemPropertySequence[] systemPropertySequences = ps.ResultValue; // this.GetSystemPropertySequences().ResultValue;
		////		//this.systemPropertySequencesBySequenceId = new HashList<ISystemPropertySequence>(systemPropertySequences.Length);

		////		//foreach (ISystemPropertySequence systemPropertySequence in systemPropertySequences)
		////		//	this.systemPropertySequencesBySequenceId[systemPropertySequence.PropertySequenceId] = systemPropertySequence;
		////	});

		//////	thread.IsBackground = true;
		//////	thread.Priority = ThreadPriority.Normal;
		//////	thread.Start();
		////}

		////protected override void HandlePackage(IPackageInfo package)
		////{
		////	//var result = this.GetSystemPropertySequence(1);

		////	base.HandlePackage(package);
		////}

		//#endregion |   Protected Methods   |

		//#region |   Private Methods   |

		//private void RaiseNewSessionConnected(object sender, SerializationReader reader)
		//{
		//	if (this.NewSessionConnected != null)
		//	{
		//		var args = new NewSessionConnectedMessageArgs();
		//		args.ReadFrom(reader);

		//		this.NewSessionConnected(sender, args);
		//	}
		//}

		//private void RaiseSessionClosed(object sender, SerializationReader reader)
		//{
		//	if (this.SessionClosed != null)
		//	{
		//		var args = new SessionClossedMessageArgs();
		//		args.ReadFrom(reader);

		//		this.SessionClosed(sender, args);
		//	}
		//}

		//private void RaiseSessionAuthorized(object sender, SerializationReader reader)
		//{
		//	if (this.SessionAuthorized != null)
		//	{
		//		var args = new SessionAuthorizedMessageArgs();
		//		args.ReadFrom(reader);

		//		this.SessionAuthorized(sender, args);
		//	}
		//}

		////private void RaiseServicePackageReceived(object sender, SerializationReader reader)
		////{
		////	if (this.ServicePackageReceived != null)
		////		this.ServicePackageReceived(sender, new SessionPackageMessageArgs(reader));
		////}

		////private void RaiseServicePackageSent(object sender, SerializationReader reader)
		////{
		////	if (this.ServicePackageSent != null)
		////		this.ServicePackageSent(sender, new ObjectServices.SessionPackageMessageArgs(reader));
		////}

		//private void RaiseSessionPackageJobAction(object sender, SerializationReader reader)
		//{
		//	//var protocol = this.GetProtocolVersion();

		//	if (this.SessionPackageJobAction != null)
		//	{
		//		var args = new SessionPackageJobActionMessageArgs();
		//		args.ReadFrom(reader);

		//		this.SessionPackageJobAction(sender, args);
		//	}
		//}

		////private void RaiseServiceTestPackageSent(object sender, SerializationReader reader)
		////{
		////	if (this.ServiceTestPackageSent != null)
		////		this.ServiceTestPackageSent(sender, new TestPackageSentMessageArgs(reader));
		////}

		//private void RaiseTransactionProcessed(object sender, SerializationReader reader)
		//{
		//	if (this.TransactionProcessed != null)
		//	{
		//		var args = new TransactionMessageArgs(reader, this.ModelDiscovery.GetObjectModel, this.GetPropertySequenceFromCache, this.GetServerObjectModelInfoFromCache);

		//		this.TransactionProcessed(sender, args);
		//	}
		//}

		//#endregion |   Private Methods   |
		#region |   Interface ISimpleObjectSession   |

		ServerObjectModelInfo? ISimpleObjectSession.GetServerObjectModel(int tableId) => this.GetServerObjectModel(tableId).GetAwaiter().GetResult();

		void ISimpleObjectSession.SetServerObjectModel(int tableId, ServerObjectModelInfo? serverObjectModelInfo) => this.serverObjectModelInfosByTableId.SetValue(tableId, serverObjectModelInfo);

		#endregion |   Interface ISimpleObjectSession   |
	}

	#region |   Delegates   |

	public delegate void SessionConnectedEventHandler(SessionConnectedMessageArgs sessionInfo);
	public delegate void SessionClosedEventHandler(SessionClosedMessageArgs sessionClosedInfo);
	public delegate void SessionAuthenticatedEventHandler(SessionAuthenticatedMessageArgs sessionAuthenticatedInfo);
	public delegate void MessageEventHandler(MessageMessageArgs sentPackageInfo);
	public delegate void SessionMessageSentEventHandler(MessageSessionMessageArgs sessionSentPackageInfo);
	public delegate void SessionResponsePackageInfodEventHandler(RequestResponseSessionMessageArgs requestResponseSessionInfoMessageArgs);
	public delegate void SessionErrorPackageInfoEventHandler(SessionErrorPackageInfoMessageArgs sessionErrorPackageInfoMessageArgs);
	public delegate void TransactionEventHandler(TransactionMessageArgs transactionInfo);


	#endregion |   Delegates   |


	//	public delegate void SessionEventMessageHandler(object sender, SessionConnectedMessageArgs e);
	//	public delegate void SessionClosedMessageEventHandler(object sender, SessionClossedMessageArgs e);
	//	public delegate void SessionAuthorizedMessageEventHandler(object sender, SessionAuthenticatedMessageArgs_OLD e);
	//	//public delegate void SessionPackageMessageEventHandler(object sender, SessionPackageJobActionMessageArgs e);
	////	public delegate void SessionPackageJobActionMessageEventHandler(object sender, SessionPackageJobActionMessageArgs e);

	//	//public delegate void TestPackageSentEventHandler(object sender, TestPackageSentMessageArgs e);
	//	public delegate void TransactionMessageEventHandler(object sender, TransactionMessageArgs e);
}
