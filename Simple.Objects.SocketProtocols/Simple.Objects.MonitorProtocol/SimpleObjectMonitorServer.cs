using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using SuperSocket.ProtoBase;
using SuperSocket.Connection;
using SuperSocket.Server.Abstractions.Session;
using Simple;
using Simple.Collections;
using Simple.Modeling;
using Simple.Serialization;
using Simple.SocketEngine;
using Simple.Objects;
using Simple.Objects.SocketProtocol;
using SuperSocket;
using System.Reflection;
using Simple.Security;
using System.IO.Packaging;

namespace Simple.Objects.MonitorProtocol
{
	public class SimpleObjectMonitorServer : ServerBase
	{
		#region |   Public Const Fields   |

		public const int DefaultMonitorPort = 5051;

		#endregion |   Public Const Fields   |

		#region |   Private Fields   |

		//		// The AppServer protocol version
		private Version protocolVersion = new Version(0, 0);
		//private string clientUsername = "Monitor Admin";
		//private string clientPasswordHash;
		//		private const int DefaultManagementPort = 2021;
		private SimpleObjectServer appServer;
		private HashArray<ServerObjectModelResponseArgs> serverObjectPropertyInfosByTybleId = new HashArray<ServerObjectModelResponseArgs>();

		#endregion |   Private Fields   |

		#region |   Constructors and Initialization   |

		public SimpleObjectMonitorServer(SimpleObjectServer appServer) //, string) clientPassword = "manager")
		{
            this.AppServer = appServer;
			this.appServer = appServer;
			//this.clientPasswordHash = PasswordSecurity.HashPassword(clientPassword);
			this.Port = DefaultMonitorPort;
        }

		#endregion |   Constructors and Initialization   |

		#region |   Protected Override Methods   |

		protected override string GetServerName() => "SimpleObjectMonitorServer";

		protected override object GetCommandOwnerInstance() => this;

		protected override PackageArgsFactory CreatePackageArgsFactory() => new MonitorPackageArgsFactory(Assembly.GetExecutingAssembly());

		protected override SimpleSession CreateSession(object session) => new SimpleObjectSession(this.AppServer.ObjectManager);  // In order to use Simple.Objects.SocketProtocol args SimpleObjectSession is given via contex when serializing

		//protected override List<Assembly> GetPackageArgsAssemblies()
		//{
		//	var result = base.GetPackageArgsAssemblies();

		//	//result.Add(Assembly.GetExecutingAssembly());

		//	return result;
		//}

		#endregion |   Protected Override Methods   |

		#region |   Public Properties   |

		public Version ProtocolVersion => this.protocolVersion;

		public SimpleObjectServer AppServer
        {
            get { return this.appServer; }

            private set
            {
                if (this.appServer != null)
                {
					this.appServer.CreatePackageDataCopy = false;
                   
					this.appServer.Started -= this.AppServer_Started;
                    this.appServer.Stopped -= this.AppServer_Stoped;
                    this.appServer.SessionConnected -= this.AppServer_SessionConnected;
                    this.appServer.SessionClosed -= this.AppServer_SessionClosed;
                    this.appServer.SessionAuthenticated -= AppServer_SessionAuthenticated;
                    //this.appServer.MessageSent -= AppServer_MessageSent;
                    this.appServer.BrotcastMessageSent -= AppServer_BrotcastMessageSent;
					this.appServer.MessageReceived -= this.AppServer_MessageReceived;
					this.appServer.RequestReceived -= this.AppServer_RequestReceived;
					//this.appServer.RequestSent -= this.AppServer_RequestSent;
					this.appServer.PackageProcessingError -= this.AppServer_PackageProcessingError;
					this.appServer.ObjectManager.TransactionFinished -= ObjectManager_TransactionFinished;

					//this.appServer.SessionPackageAction -= AppServer_SessionPackageAction;
					//this.appServer.ObjectManager.TransactionFinished -= this.ObjectManager_TransactionFinished;

					//this.objectManager.ObjectCreated -= ObjectManager_ObjectCreated;
					//this.objectManager.AfterSave -= ObjectManager_AfterSave;
					//this.objectManager.AfterDelete -= ObjectManager_AfterDelete;
					//this.objectManager.PropertyValueChange -= ObjectManager_PropertyValueChange;
					//this.objectManager.GraphElementParentChange -= ObjectManager_GraphElementParentChange;
					//this.objectManager.OrderIndexChange -= ObjectManager_OrderIndexChange;
				}

				this.appServer = value;

                if (this.appServer != null)
                {
					this.appServer.CreatePackageDataCopy = true;
					
					this.appServer.Started += this.AppServer_Started;
					this.appServer.Stopped += this.AppServer_Stoped;
					this.appServer.SessionConnected += this.AppServer_SessionConnected;
					this.appServer.SessionClosed += this.AppServer_SessionClosed;
					this.appServer.SessionAuthenticated += this.AppServer_SessionAuthenticated;
                    //this.appServer.MessageSent += AppServer_MessageSent;
                    this.appServer.BrotcastMessageSent += AppServer_BrotcastMessageSent;
					this.appServer.MessageReceived += this.AppServer_MessageReceived;
					this.appServer.RequestReceived += this.AppServer_RequestReceived;
					//this.appServer.RequestSent += this.AppServer_RequestSent;
					this.appServer.PackageProcessingError += this.AppServer_PackageProcessingError;
					this.appServer.ObjectManager.TransactionFinished += ObjectManager_TransactionFinished;
					//               this.appServer.SessionPackageAction += AppServer_SessionPackageAction;
					//this.appServer.ObjectManager.TransactionFinished += this.ObjectManager_TransactionFinished;

					//this.objectManager.ObjectCreated += ObjectManager_ObjectCreated;
					//this.objectManager.AfterSave += ObjectManager_AfterSave;
					//this.objectManager.AfterDelete += ObjectManager_AfterDelete;
					//this.objectManager.PropertyValueChange += ObjectManager_PropertyValueChange;
					//this.objectManager.GraphElementParentChange += ObjectManager_GraphElementParentChange;
					//this.objectManager.OrderIndexChange += ObjectManager_OrderIndexChange;
				}
			}
        }

		#endregion |   Public Properties   |

		#region |   Private Methods   |

		private async ValueTask AppServer_Started(object sender, EventArgs e) => await this.SendBroadcastSystemMessage((int)MonitorSystemMessage.ServerSrarted);

        private async ValueTask AppServer_Stoped(object sender, EventArgs e) => await this.SendBroadcastSystemMessage((int)MonitorSystemMessage.ServerStopped);

        private async ValueTask AppServer_SessionConnected(SimpleSession session) => await this.SendBroadcastSystemMessage((int)MonitorSystemMessage.SessionConnected, new SessionConnectedMessageArgs(session));

        private async ValueTask AppServer_SessionClosed(SimpleSession session, CloseReason closeReason) => await this.SendBroadcastMessage((int)MonitorSystemMessage.SessionClosed, new SessionClosedMessageArgs(session, closeReason));

		private async ValueTask AppServer_SessionAuthenticated(SimpleSession session)
		{ 
			await this.SendBroadcastSystemMessage((int)MonitorSystemMessage.SessionAuthenticated, new SessionAuthenticatedMessageArgs(session));
		}

        private async ValueTask AppServer_MessageSent(SimpleSession session, byte[] packageData)
        {
            var messageArgs = new MessageSessionMessageArgs(session.SessionKey, packageData);

            await this.SendBroadcastSystemMessage((int)MonitorSystemMessage.MessageSent, messageArgs);
		}

        private async ValueTask AppServer_BrotcastMessageSent(byte[] packageData, Encoding packageEncoding)
        {
			var messageArgs = new BrodcastMessageMessageArgs(packageData); // TODO: EncodingTypeId needed

			await this.SendBroadcastSystemMessage((int)MonitorSystemMessage.BrodcastMessageSent, messageArgs);
		}

		private async ValueTask AppServer_MessageReceived(SimpleSession session, byte[] packageData)
		{
			var messageArgs = new MessageSessionMessageArgs(session.SessionKey, packageData);

			await this.SendBroadcastSystemMessage((int)MonitorSystemMessage.MessageReceived, messageArgs);
		}

		private async ValueTask AppServer_RequestReceived(SimpleSession session, byte[] requestPackageData, byte[] responsePackageData)
		{
			var messageArgs = new RequestResponseSessionMessageArgs(session.SessionKey, requestPackageData, responsePackageData);

			await this.SendBroadcastSystemMessage((int)MonitorSystemMessage.RequestReceived, messageArgs);
		}

		private async ValueTask AppServer_RequestSent(SimpleSession session, byte[] requestPackageData, byte[] responsePackageData)
		{
			var messageArgs = new RequestResponseSessionMessageArgs(session.SessionKey, requestPackageData, responsePackageData);

			await this.SendBroadcastSystemMessage((int)MonitorSystemMessage.RequestSent, messageArgs);
		}

		private async ValueTask AppServer_PackageProcessingError(SimpleSession session, string errorMessage, string errorDescription, byte[] packageData)
		{
			var messageArgs = new SessionErrorPackageInfoMessageArgs(session.SessionKey, errorMessage, errorDescription, packageData);

			await this.SendBroadcastSystemMessage((int)MonitorSystemMessage.PackageProcessingError, messageArgs);
		}

		private void ObjectManager_TransactionFinished(object sender, TransactionDatastoreActionRequesterEventArgs e)
		{
			if (this.IsAnyClientAuthenticated().GetAwaiter().GetResult())
			{
				var messageArgs = new TransactionMessageArgs(e.TransactionRequests, e.Transaction, e.DatastoreActions!, this.AppServer.GetServerObjectModel);

				this.SendBroadcastSystemMessage((int)MonitorSystemMessage.TransactionFinished, messageArgs).GetAwaiter().GetResult(); // .DoNotAwait(); //.ConfigureAwait(false); //.DoNotAwait();
			}
		}

		#endregion |   Private Methods   |

		//private void AppServer_SessionPackageAction(SimpleSession session, PackageProcessingActionType jobActionType, PackageInfo receivedPackage, ArraySegment<byte> sentPackageLength, ArraySegment<byte> sentPackageHeaderAnyBody)
		//      {
		//	this.SendBroadcastMessage((int)MonitorMessage.PackageJobAction, new SessionPackageJobActionMessageArgs(session.SessionKey, jobActionType, receivedPackage, sentPackageLength, sentPackageHeaderAnyBody)).GetAwaiter().GetResult();
		//}

		//      private void ObjectManager_TransactionFinished(object sender, TransactionDatastoreActionRequesterEventArgs e)
		//{
		//	this.SendBroadcastMessage((int)MonitorMessage.TransactionProcessed, new TransactionMessageArgs(e.Transaction, e.DatastoreActions)).GetAwaiter();
		//}






		//		protected override object GetCommandOwnerInstance() => this;

		//		private void ObjectAppServer_NewSessionConnected(SimpleSession session)
		//		{
		//			this.SendBroadcastMessage((int)ServiceMonitorMessage.NewSessionConnected, () => new NewSessionConnectedMessageArgs(session));
		//		}

		//		private void ObjectAppServer_SessionClosed(AppSession session, CloseReason value)
		//		{
		//			this.SendBroadcastMessage((int)ServiceMonitorMessage.SessionClosed, () => new SessionClossedMessageArgs(session, value));
		//		}

		//		private void ObjectAppServer_SessionAuthorized(AppSession session)
		//		{
		//			this.SendBroadcastMessage((int)ServiceMonitorMessage.SessionAuthorized, () => new SessionAuthorizedMessageArgs(session));
		//		}

		//		private void ObjectAppServer_SessionPackageJobAction(AppSession session, JobActionType actionType, PackageInfo receivedPackage, ArraySegment<byte> sentPackageLength, ArraySegment<byte> sentHeaderAnyBody)
		//		{
		//			//	throw new NotImplementedException();
		//			//}

		//			//private void AppServer_PackageJobAction(object sender, SessionPackageJobActionMessageArgs e)
		//			//{
		//			//	this.SendBroadcastMessage((int)ServiceMonitorMessage.ServicePackageJobAction, () => new SessionConnectedMessageArgs(session));
		//			//}
		//			//internal void OnAppServer_SessionPackageJobAction(string sessionId, JobActionType jobActionType, PackageInfo receivedPackage, byte[] sentData)
		//			//{
		//			//this.SendBroadcastMessage((int)ServiceMonitorMessage.ServicePackageJobAction, () => new SessionPackageJobActionMessageArgs(sessionId, jobActionType, receivedPackage, sentData));

		//			//bool useSessionPackageJobActionMessageArgsClass = false;

		//			IEnumerable<AppSession> sessions = this.GetAllSessions();

		//			if (sessions != null && sessions.Count() > 0)
		//			{
		//				//TODO:
		//				int checkWhySleepGivesBestResultOfContinuousReceivingOnJobPackageAction = 0;

		//				//System.Threading.Thread.Sleep(20);

		//#if ProcessJobActionNonOptimized
		//					SerializationWriter newHeaderWriter = PackageEngine.CreatePackageWriterForSending((int)ServiceMonitorMessage.PackageJobAction, PackageEngine.MessageWithBodyNonSystemBroadcastPackageFlags, 0, null);

		//					newHeaderWriter.WriteInt64Optimized(session.SessionKey);
		//					newHeaderWriter.WriteInt32Optimized((int)actionType);

		//					ArraySegment<byte> newPackageHeader = newHeaderWriter.BaseStream.ToArraySegment();
		//					ArraySegment<byte> newPackageLength = PackageEngine.CreatePackageLengthBy7BitEncoding(newPackageHeader.Count + (int)receivedPackage.Reader.BaseStream.Length + sentPackageLength.Count + sentHeaderAnyBody.Count);


		//				List<ArraySegment<byte>> dataToSend = new List<ArraySegment<byte>>(6) { newPackageLength, newPackageHeader };

		//				foreach (var item in receivedPackage.Reader.BaseStream.GetArraySegments())
		//					dataToSend.Add(item);

		//				dataToSend.Add(sentPackageLength);
		//				dataToSend.Add(sentHeaderAnyBody);
		//				//IList<ArraySegment<byte>> dataToSend = new ArraySegment<byte>[] { newPackageLength, newPackageHeader, receivedPackage.Reader.BaseStream.GetArraySegment(), sentPackageLength, sentHeaderAnyBody };


		//				foreach (AppSession monitorSession in sessions)
		//					try
		//					{
		//						if (monitorSession != null && monitorSession.Connected && monitorSession.Authorized)
		//							monitorSession.Send(dataToSend);
		//					}
		//					catch (Exception ex)
		//					{
		//						Logger.Debug(ex);
		//					}

		//#else
		//				this.SendBroadcastMessage((int)ServiceMonitorMessage.PackageJobAction, () => new SessionPackageJobActionMessageArgs(session.SessionKey, actionType, receivedPackage, sentPackageLength, sentHeaderAnyBody));
		//#endif

		//				//BufferStream testStream = new BufferStream();
		//				//testStream.Initialize(new ArraySegment<byte>[] { packageLength, newHeader, headerAnyBodyData });

		//				//PackageInfo test = PackageManager.CreatePackageWriterForSending(testStream as IBufferStream);
		//			}
		//		}


		//		//private void AppServer_PackageReceived(string sessionId, PackageInfo package)
		//		//{
		//		//	this.SendBroadcastMessage((int)ServiceMonitorMessage.PackageReceived, () => new SessionPackageMessageArgs(sessionId, package));

		//		//	//IList<ArraySegment<byte>> packageData = package.GetArraySegments();

		//		//	//foreach (AppSession monitorSession in this.GetSessions(s => true))
		//		//	//{
		//		//	//	SerializationWriter newHeaderWriter = this.PackageManager.CreatePackageWriterForSending((int)ServiceMonitorMessage.PackageSent, PackageManager.MessageWithBodyNonSystemBroadcastPackageFlags, 0, null);
		//		//	//	newHeaderWriter.WriteString(sessionId);

		//		//	//	ArraySegment<byte> newHeader = this.PackageManager.ToArraySegment(newHeaderWriter.BaseStream);
		//		//	//	ArraySegment<byte> newPackageLength = this.PackageManager.CreatePackageLengthBy7BitEncodedSignedInt32ArraySegment(newHeader.Count + packageLength.Count + headerAnyBody.Count);

		//		//	//	monitorSession.Send(new ArraySegment<byte>[] { newPackageLength, newHeader, packageLength, headerAnyBody });

		//		//	//	//BufferStream testStream = new BufferStream();
		//		//	//	//testStream.Initialize(new ArraySegment<byte>[] { packageLength, newHeader, headerAnyBodyData });

		//		//	//	//PackageInfo test = PackageManager.CreatePackage(testStream as IBufferStream);
		//		//	//}
		//		//}

		//		//private void AppServer_PackageSent(string sessionId, ArraySegment<byte> packageLength, ArraySegment<byte> headerAnyBody)
		//		//{
		//		//	//System.Threading.Thread.Sleep(20);

		//		//	//this.SendBroadcastMessage((int)ServiceMonitorMessage.PackageSent, () => new SessionPackageMessageArgs(sessionId, this.token++));



		//		//	////this.SendBroadcastMessage((int)ServiceMonitorMessage.PackageSent, () => new SessionPackageMessageArgs(session.SessionID, package));

		//		//	//BufferStream packageStream = new BufferStream();
		//		//	//packageStream.Initialize(new ArraySegment<byte>[] { packageLength, headerAnyBody });
		//		//	//PackageInfo package = PackageManager.CreatePackage(packageStream as IBufferStream);

		//		//	//this.SendBroadcastMessage((int)ServiceMonitorMessage.PackageSent, () => new SessionPackageMessageArgs(sessionId, package));


		//		//	IEnumerable<AppSession> sesions = this.GetSessions(s => true);

		//		//	if (sesions.Count() > 0)
		//		//	{
		//		//		SerializationWriter newHeaderWriter = this.PackageManager.CreatePackageWriterForSending((int)ServiceMonitorMessage.PackageSent, PackageManager.MessageWithBodyNonSystemBroadcastPackageFlags, 0, null);
		//		//		newHeaderWriter.WriteString(sessionId);
		//		//		ArraySegment<byte> newHeader = newHeaderWriter.BaseStream.GetArraySegment();
		//		//		ArraySegment<byte> newPackageLength = PackageManager.CreatePackageLengthBy7BitEncodedSignedInt32ArraySegment(newHeader.Count + packageLength.Count + headerAnyBody.Count);

		//		//		IList<ArraySegment<byte>> dataToSend = new ArraySegment<byte>[] { newPackageLength, newHeader, packageLength, headerAnyBody };

		//		//		foreach (AppSession monitorSession in sesions)
		//		//			monitorSession.Send(dataToSend);

		//		//		//BufferStream testStream = new BufferStream();
		//		//		//testStream.Initialize(new ArraySegment<byte>[] { packageLength, newHeader, headerAnyBodyData });

		//		//		//PackageInfo test = PackageManager.CreatePackageWriterForSending(testStream as IBufferStream);
		//		//	}

		//		//	//foreach (AppSession monitorSession in this.GetSessions(s => true))
		//		//	//{
		//		//	//	SerializationWriter newHeaderWriter = this.PackageManager.CreatePackageWriterForSending((int)ServiceMonitorMessage.PackageSent, PackageManager.MessageWithBodyNonSystemBroadcastPackageFlags, 0, null);

		//		//	//	newHeaderWriter.WriteString(sessionId);
		//		//	//	ArraySegment<byte> newHeader = newHeaderWriter.BaseStream.GetArraySegment();
		//		//	//	ArraySegment<byte> newPackageLength = this.PackageManager.CreatePackageLengthBy7BitEncodedSignedInt32ArraySegment(newHeader.Count + packageLength.Count + headerAnyBody.Count);

		//		//	//	IList<ArraySegment<byte>> dataToSend = new ArraySegment<byte>[] { newPackageLength, newHeader, packageLength, headerAnyBody };

		//		//	//	monitorSession.Send(dataToSend);

		//		//	//	//BufferStream testStream = new BufferStream();
		//		//	//	//testStream.Initialize(new ArraySegment<byte>[] { packageLength, newHeader, headerAnyBodyData });

		//		//	//	//PackageInfo test = PackageManager.CreatePackage(testStream as IBufferStream);
		//		//	//}
		//		//}


		//		//private void ObjectManager_TransactionStarted(object sender, TransactionEventArgs e)
		//		//{
		//		//}

		//		private void ObjectManager_TransactionFinished(object sender, TransactionDatastoreActionRequesterEventArgs e)
		//		{
		//			this.SendBroadcastMessage((int)ServiceMonitorMessage.TransactionProcessed, () => new TransactionMessageArgs(e.Transaction, e.DatastoreActions));
		//		}

		//		//public int MonitorPort { get; set; }

		//		#region |   Public Methods   |


		//		//public void Stop()
		//		//{
		//		//	if (this.appServer != null)
		//		//	{
		//		//		this.appServer.Stop();
		//		//		this.appServer.NewRequestReceived -= AppServer_NewRequestReceived;
		//		//		this.appServer.NewSessionConnected -= AppServer_NewSessionConnected;
		//		//		this.appServer.SessionClosed -= AppServer_SessionClosed;
		//		//	}
		//		//}

		//		#endregion |   Public Methods   |

		#region |   Request & Responses   |

		//[AuthorizationNotRequired]
		//[SystemRequestCommand((int)SystemRequest.GetProtocolVersion)]
		//public ResponseArgs GetProtocolVersion(ISimpleSession session, PackageReader package) => new ProtocolVersionResponseArgs(this.ProtocolVersion);


		[AuthorizationNotRequired]
		[SystemRequestCommand((int)MonitorSystemRequest.AuthenticateSession)]
		public ResponseArgs AuthenticateSession(SimpleSession session, PackageInfo packageInfo)
		{
			string username = String.Empty;

			if (session.IsAuthenticated)
			{
				//username = session.Username;

				return new AuthenticateSessionResponseArgs(isAuthenticated: true, session.UserId); //, username);
			}

			AuthenticateSessionRequestArgs requestArgs = (packageInfo.PackageArgs as AuthenticateSessionRequestArgs)!;
			bool isAuthenticated = this.AppServer.ObjectManager.AuthenticateMonitorSession(requestArgs.Username, requestArgs.PasswordHash, out long userId);
			//bool isAuthenticated = (this.clientUsername == requestArgs.Username && this.clientPasswordHash == requestArgs.PasswordHash);

			if (isAuthenticated)
				base.SessionIsAuthenticated(session as SimpleSession, userId, requestArgs.Username);

			return new AuthenticateSessionResponseArgs(isAuthenticated, userId);
		}


		[SystemRequestCommand((int)MonitorSystemRequest.GetServerState)]
		public ResponseArgs GetServerState(SimpleSession session, PackageInfo packageInfo)
		{
			return new ServerStateResponseArgs(this.AppServer.State);
		}


		[SystemRequestCommand((int)MonitorSystemRequest.StartServer)]
		public ResponseArgs StartServer(SimpleSession session, PackageInfo packageInfo)
		{
			this.AppServer.StartAsync().GetAwaiter().GetResult();

			return new ServerStateResponseArgs(this.AppServer.State);
		}


		[SystemRequestCommand((int)MonitorSystemRequest.StopServer)]
		public ResponseArgs StopServer(SimpleSession session, PackageInfo packageInfo)
		{
			this.AppServer.StopAsync().GetAwaiter().GetResult();

			return new ServerStateResponseArgs(this.AppServer.State);
		}


		[SystemRequestCommand((int)MonitorSystemRequest.GetSessionInfos)]
		public ResponseArgs GetSessionInfos(SimpleSession session, PackageInfo packageInfo)
		{
			IEnumerable<IAppSession> sessions = this.AppServer.GetAllSessions().GetAwaiter().GetResult();

			return new SessionInfosResponseArgs(sessions);
		}

		//[CommandRequest((int)MonitorServiceRequest.GetTransactionActionPropertySequence)]
		//public ResponseArgs GetTransactionActionPropertySequence(AppSession session, SerializationReader reader)
		//{
		//	GetTransactionActionPropertySequenceRequestArgs args = new GetTransactionActionPropertySequenceRequestArgs();

		//	args.ReadFrom(reader);

		//	IPropertySequence propertySequence = this.ObjectAppServer.ObjectManager.GetSystemPropertySequence(args.PropertySequenceId);

		//	return new GetTransactionActionPropertySequenceResponseArgs(propertySequence);
		//}

		[SystemRequestCommand((int)MonitorSystemRequest.GetServerObjectModel)]
		public ResponseArgs GetServerObjectModelInfo(SimpleSession session, PackageInfo packageInfo)
		{
			TableIdRequestArgs requestArgs = (packageInfo.PackageArgs as TableIdRequestArgs)!;

			//requestArgs.ReadFrom(package.SerializationReader);

			ServerObjectModelInfo? serverObjectPropertyInfo = this.AppServer.GetServerObjectModel(requestArgs.TableId);

			if (serverObjectPropertyInfo != null)
				return new ServerObjectModelResponseArgs(serverObjectPropertyInfo);
			else
				return new ServerObjectModelResponseArgs(); // throw new ArgumentNullException($"Server Object Model is not found, TableId={requestArgs.TableId}");
		}

		[SystemRequestCommand((int)MonitorSystemRequest.GetGraphName)]
		public ResponseArgs GetGraphName(SimpleSession session, PackageInfo packageInfo)
		{
			GraphKeyRequestArgs requestArgs = (packageInfo.PackageArgs as GraphKeyRequestArgs)!;
			string graphName = this.AppServer.GetGraphName(requestArgs.GraphKey);

			return new NameResponseArgs(graphName);
		}

		[SystemRequestCommand((int)MonitorSystemRequest.GetRelationName)]
		public ResponseArgs GetRelationName(SimpleSession session, PackageInfo packageInfo)
		{
			RelationKeyRequestArgs requestArgs = (packageInfo.PackageArgs as RelationKeyRequestArgs)!;
			string relationName = this.AppServer.GetRelationName(requestArgs.RelationKey);

			return new NameResponseArgs(relationName);
		}

		[SystemRequestCommand((int)MonitorSystemRequest.GetObjectName)]
		public ResponseArgs GetObjectName(SimpleSession session, PackageInfo packageInfo)
		{
			ObjectIdTableIdRequestArgs requestArgs = (packageInfo.PackageArgs as ObjectIdTableIdRequestArgs)!;
			string objectName = this.AppServer.GetObjectName(requestArgs.TableId, requestArgs.ObjectId);

			return new NameResponseArgs(objectName);
		}

		#endregion |   Request & Responses   |

		//		#region |   Protected Methods   |

		//		protected override void ProcessSessionPackageReceive(AppSession session, PackageInfo package)
		//		{
		//			session.PackageEngine.OnPackageReceived(package);
		//		}

		//		#endregion |   Protected Methods   |
	}
}
