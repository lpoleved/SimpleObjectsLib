using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Reflection;
using System.IO;
using System.IO.Packaging;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Security.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SuperSocket;
using SuperSocket.ProtoBase;
using SuperSocket.Channel;
using SuperSocket.Server;
//using SuperSocket.SocketBase.Config;
//using SuperSocket.SocketEngine;
//using SuperSocket.ProtoBase;
using Simple;
//using Simple.Threading;
using Simple.Serialization;
using Simple.SocketEngine;
//using Simple.Objects;

namespace Simple.SocketEngine
{
	public abstract class ServerBase : PackageEngine //: AppServer<AppSession, PackageInfo, int>
	{
        #region |   Public Const Fields   |

        public const int DefaultPort = 5050;
        public const int MaxConnections = 0;

        #endregion |   Public Const Fields   |

        #region |   Private Members   |

        //private long sessionKey = 1;
		private Dictionary<long, SimpleSession> sessionsBySessionKey = new Dictionary<long, SimpleSession>();
		private UniqueKeyGenerator<long> sessionKeyGenerator = new UniqueKeyGenerator<long>();
		private bool useChannelEncription = false;
		private bool useChannelCompression = false;
		private IServer server = null;
		private ILogger logger = null;
		private int port = DefaultPort;
		//private bool createPackageDataCopy = false;
		//private bool useProxy = false;
		//private IPAddress proxy = null;
		//private int proxyPort = 8080;

		//private IHostConfigurator hostConfigurator = null;
		//private IServer server = null;

		//private ActionBlock<SessionPackageInfo> processOnPackageReceiveAction = null;
		//private int systemTransactionRequestPackageKey;

		//private object lockObject = new object();

		#endregion |   Private Members   |

		#region |   Constructors and Initialization   |

		public ServerBase()
		{
			this.sessionKeyGenerator.ReuseKeys = true;
			//this.packageEngine = this.CreatePackageEngine();

			//this.NewRequestReceived += AppServer_NewRequestReceived;
			//this.executeCommendAction = new ActionBlock<SessionPackageInfo>(args => base.ExecuteCommand(args.Session, args.Package));
			//base.NewSessionConnected += AppServer_NewSessionConnected;
			//this.NewRequestReceived += this.AppServer_NewRequestReceived;
			//this.NewSessionConnected += AppServer_NewSessionConnected;
			//if (this.NewRequestReceived != null)
			//	this.NewRequestReceived(null, null);
		}

		#endregion |   Constructors and Initialization   |

		#region |   Events   |

		//[Obsolete("This event is internally handled.", true)]
		//public new event RequestHandler<AppSession, PackageInfo> NewRequestReceived;
		public event AsyncEventHandler? Started;
		public event AsyncEventHandler? Stopped;
		public event SessionAsyncEventHandler? SessionConnected;
		public event SessionCloseAsyncEventHandler? SessionClosed;
		//public event SessionAuthenticatedAsyncEventHandler SessionAuthenticated;
		//public event SessionPackageDataAsyncEventHandler? MessageSent;
		public event PackageDataEncodingEventAsyncHandler? BrotcastMessageSent;
		public event SessionPackageDataAsyncEventHandler? MessageReceived;
		//public event SessionRequestResponsePackageInfoAsyncEventHandler? RequestSent;
		public event SessionRequestResponsePackageInfoAsyncEventHandler? RequestReceived;
		public event SessionErrorMessagePackageDataAsyncEventHandler? PackageProcessingError;

		// TODO:
		//public event SessionPackageActionEventHandler SessionPackageAction;

		#endregion |   Events   |

		#region |   Protected Properties   |

		protected IServer Server => this.server;

		#endregion |   Protected Properties   |

		#region |   Public Properties   |
		
		//public Version ProtocolVersion { get => this.protocolVersion; protected set => this.protocolVersion = value; }

		public bool UseChannelEncryption { get => this.useChannelEncription; set => this.useChannelEncription = value; }
		public bool UseChannelCompression { get => this.useChannelCompression; set => this.useChannelCompression = value; }

		public int Port { get => this.port; set => this.port = value; }

		//public bool CreatePackageDataCopy { get => this.createPackageDataCopy; set => this.createPackageDataCopy = value; }

		//public bool UseProxy { get => this.useProxy; set => this.useProxy = value; }
		//public IPAddress Proxy { get => this.proxy; set => this.proxy = value; }
		//public int ProxyPort { get => this.proxyPort; set => this.proxyPort = value; }

		public ServerState State => (this.Server != null) ? this.Server.State : ServerState.Failed;

		#endregion |   Public Properties   |

		#region |   Protected Overrides Methods   |

		protected override int GetServerListeningPort() => this.Port;

		//protected override IPipelineFilter<PackageInfo> CreatePipelineFilter(object session) => new PipelineFilter(this.PackageArgsFactory, this.CreatePackageDataCopy); // this.CreatePipelineFilter(session);

		//protected override async ValueTask OnMessageSent(ISimpleSession session, byte[] packageData)
		//{
		//	if (this.MessageSent != null)
		//		await this.MessageSent((session as SimpleSession)!, packageData);
		//}

		protected override async ValueTask OnMessageReceived(ISimpleSession session, byte[] packageData)
		{
			if (this.MessageReceived != null)
				await this.MessageReceived((session as SimpleSession)!, packageData);
		}

		#endregion |   Protected Overrides Methods   |

		#region |   Protected Virtual Methods   |

		public class SessionContainerDependentService
		{
			public IAsyncSessionContainer AsyncSessionContainer { get; private set; }
			public ISessionContainer SessionContainer { get; private set; }
			public SessionContainerDependentService(ISessionContainer sessionContainer, IAsyncSessionContainer asyncSessionContainer)
			{
				SessionContainer = sessionContainer;
				AsyncSessionContainer = asyncSessionContainer;
			}
		}

		protected virtual string GetServerName() => "SimpleServer";
		protected abstract object GetCommandOwnerInstance();

		protected virtual IServer CreateServer(IHostConfigurator hostConfigurator)
		{
			return this.CreateServerInternal<SimpleSession>(hostConfigurator);
		}
		
		protected IServer CreateServerInternal<TSession>(IHostConfigurator hostConfigurator)
			where TSession : SimpleSession
		{
			//string className = this.GetType().Name;
			
			
			// This works file
			//IServer server = CreateSocketServerBuilder<PackageInfo, SimplePipelineFilter>(hostConfigurator) // this.CreatePipelineFilter
			//				 .UsePipelineFilterFactory<SimplePipelineFilterFactory>()
			//				 .UseSession<SimpleSession>()
			//				 .UseSessionHandler(this.OnSessionConnected, this.OnSessionClosed)
			//				 .UsePackageHandler(this.OnPackageReceive, this.OnPackageProcessingError)
			//				 .ConfigureSuperSocket(serverOptions =>
			//				 {
			//					 serverOptions.Name = this.GetServerName();
			//					 serverOptions.Listeners.FirstOrDefault()!.Port = this.Port;
			//					 serverOptions.Logger = DefaultLoggerFactory.CreateLogger(this.GetType().Name);
			//				 })
			//				 .UseInProcSessionContainer()
			//				 .ConfigureServices((ctx, services) => services.AddSingleton<SessionContainerDependentService>())
			//				 .BuildAsServer();


			IServer server = CreateSocketServerBuilder<PackageReader>(hostConfigurator) // this.CreatePipelineFilter
							 //.UsePipelineFilter<SimplePipelineFilter>()
							 //.UsePipelineFilterFactory<PipelineFilterFactory>()
							 //.UseSession<TSession>(this.CreateSession)
							 //.UseSessionFactory<SimpleSession>(this.CreateSession)
							 .UsePipelineFilterFactory(this.CreatePipelineFilter)
							 .UseSessionFactory(this.CreateSession)
							 .UseSessionHandler(this.OnSessionConnected, this.OnSessionClosed)
							 .UsePackageHandler(this.OnPackageReceive, this.OnPackageProcessingError)
							 .ConfigureSuperSocket(serverOptions =>
							 {
								 serverOptions.Name = this.GetServerName();
								 serverOptions.Listeners.FirstOrDefault()!.Port = this.Port;
								 serverOptions.Logger = DefaultLoggerFactory.CreateLogger(this.GetType().Name);
								 serverOptions.ReceiveBufferSize = this.ReceiveBufferSize;
								 serverOptions.SendBufferSize = this.SendBufferSize;
							 })
							 .UseInProcSessionContainer()
							 .ConfigureServices((ctx, services) => services.AddSingleton<SessionContainerDependentService>())
							 .BuildAsServer();
			return server;
		}

		protected virtual SimpleSession CreateSession(object session)
		{
			return new SimpleSession();
		}

		#endregion |   Protected Virtual Methods   |

		#region |   Public Methods   |

		public async Task<bool> StartAsync()
		{
			IHostConfigurator hostConfigurator = this.CreateHostConfigurator(this.UseChannelCompression, UseChannelEncryption);
			
			this.server = this.CreateServer(hostConfigurator);
			
			if (this.server.Options.Logger != null)
				this.logger = this.server.Options.Logger;
			
			bool result = await this.server.StartAsync();

			await this.OnStart();

			return result;
		}

		public async Task StopAsync()
		{
			if (this.Server != null && this.State != ServerState.Stopped)
			{
				await this.Server.StopAsync();
				await this.OnStop();
			}
		}

		public async Task<bool> RestartAsync()
		{
			await this.StopAsync();

			return await this.StartAsync();
		}

		protected async ValueTask SendBroadcastMessage(int messageCode, MessageArgs? messageArgs = null, long exceptionSessionKey = 0)
		{
			await this.SendBroadcastMessage(messageCode, this.PackageEncoder, messageArgs, exceptionSessionKey);
		}

		protected async ValueTask SendBroadcastMessage(int messageCode, IPackageEncoder<PackageWriter> packageEncoder, MessageArgs? messageArgs = null, long exceptionSessionKey = 0)
		{
			await this.SendBroadcastMessage(messageCode, MessageBrodcastFlags, packageEncoder, messageArgs, exceptionSessionKey);
		}

		protected async ValueTask SendBroadcastSystemMessage(int messageCode, MessageArgs? messageArgs = null, long exceptionSessionKey = 0)
		{
			await this.SendBroadcastSystemMessage(messageCode, this.PackageEncoder, messageArgs, exceptionSessionKey);
		}

		protected async ValueTask SendBroadcastSystemMessage(int messageCode, IPackageEncoder<PackageWriter> packageEncoder, MessageArgs? messageArgs = null, long exceptionSessionKey = 0)
		{
			await this.SendBroadcastMessage(messageCode, MessageSystemBrodcastFlags, packageEncoder, messageArgs, exceptionSessionKey);
		}

		private async ValueTask SendBroadcastMessage(int messageCode, HeaderInfo headerInfo, IPackageEncoder<PackageWriter> packageEncoder, MessageArgs? messageArgs, long exceptionSessionKey = 0)
        {
			Dictionary<Encoding, ReadOnlyMemory<byte>> packageDataByEncoding = new Dictionary<Encoding, ReadOnlyMemory<byte>>();
			var sessionContainer = this.Server.GetAsyncSessionContainer();
			IEnumerable<IAppSession> sessions = await sessionContainer.GetSessionsAsync();
			bool isOnBrodcastMessageSentInvoked = false;

			foreach (SimpleSession session in sessions)
			{
				if (session != null && session.IsConnected && session.IsAuthenticated && session.SessionKey != exceptionSessionKey)
				{
					try
					{
						ReadOnlyMemory<byte> packageData;

						if (packageDataByEncoding.TryGetValue(session.CharacterEncoding, out packageData))
						{
							await session.SendAsync(packageData); //.DoNotAwait(); // ConfigureAwait(false);
						}
						else
						{
							PackageWriter package = new PackageWriter(headerInfo, messageCode, session.CharacterEncoding, session, messageArgs); // Each endpoint user differ only in character encoding
							
							await session.SendAsync(packageEncoder, package); //.DoNotAwait(); // ConfigureAwait(false);

							packageData = new ReadOnlyMemory<byte>(package.Buffer);
							packageDataByEncoding.Add(session.CharacterEncoding, packageData);

							if (!isOnBrodcastMessageSentInvoked)
							{
								await this.OnBrodcastMessageSent(package.Buffer!, session.CharacterEncoding); //
								isOnBrodcastMessageSentInvoked = true;
							}
						}
					}
					catch (Exception ex)
					{
						this.Logger?.LogDebug(String.Format("Error in sending message, MessageId={0} ({1})", messageCode, ex.GetFullErrorMessage()));
					}
				}
			}
        }

		protected async ValueTask<bool> IsAnyAuthenticatedClient(long exceptionSessionKey = 0)
		{
			var sessions = await this.Server.GetAsyncSessionContainer().GetSessionsAsync();

			foreach (SimpleSession session in sessions)
				if (session.SessionKey != exceptionSessionKey &&  session.IsAuthenticated)
					return true;

			return false;
		}

		#endregion |   Public Methods   |

		#region |   Protected Methods   |

		protected virtual async ValueTask OnStart() 
		{ 
			if (this.Started != null) 
				await this.Started.Invoke(this, EventArgs.Empty); 
		}

		protected virtual async ValueTask OnStop()
		{
			if (this.Stopped != null)
				await this.Stopped.Invoke(this, EventArgs.Empty);
		}

		protected virtual async ValueTask OnSessionConnected(IAppSession session)
		{
			if (session is SimpleSession simpleSession)
				simpleSession.SessionKey = this.sessionKeyGenerator.CreateKey();
			
			if (this.SessionConnected != null)
				await this.SessionConnected((session as SimpleSession)!);
		}

		protected virtual async ValueTask OnSessionClosed(IAppSession session, CloseEventArgs closeInfo)
		{
			if (this.SessionClosed != null)
				await this.SessionClosed((session as SimpleSession)!, closeInfo.Reason);
		}

		protected virtual ValueTask OnPackageReceive(IAppSession session, PackageReader package)
		{
			return base.OnPackageReceived(commandOwner: this.GetCommandOwnerInstance(), (session as ISimpleSession)!, package, this.CommandDiscovery);
		}

		protected override async ValueTask OnRequestReceived(ISimpleSession session, byte[] requestPackageData, byte[] responsePackageData)
		{
			await base.OnRequestReceived(session, requestPackageData, responsePackageData);

			if (this.RequestReceived != null)
				await this.RequestReceived((session as SimpleSession)!, requestPackageData, responsePackageData);
		}

		//protected override async ValueTask OnRequestSent(ISimpleSession session, byte[] requestPackageData, byte[]? responsePackageData)
		//{
		//	await base.OnRequestSent(session, requestPackageData, responsePackageData);

		//	if (this.RequestSent != null)
		//		await this.RequestSent((session as SimpleSession)!, requestPackageData, responsePackageData);

		//}

		//Func<IAppSession, PackageHandlingException<TReceivePackage>, ValueTask<bool>
		protected virtual async ValueTask<bool> OnPackageProcessingError(IAppSession session, PackageHandlingException<PackageReader> ex)
		{
			//OutputHelper.WriteLine("Server Package error hendler exception: " + ex.GetFullErrorMessage());
			//session.GetDefaultLogger()?.LogDebug("Server Package error hendler exception: " + ex.GetFullErrorMessage());
			this.Logger?.LogDebug("Server Package error hendler exception: " + ex.GetFullErrorMessage());

			if (this.PackageProcessingError != null)
				await this.PackageProcessingError((session as SimpleSession)!, ex.Message, ex.GetFullErrorMessage(), ex.Package.Buffer!);

			return true;
		}

		protected void SessionIsAuthenticated(SimpleSession session, long userId, string username)
		{
			session.UserId = userId;
			session.Username = username;
			session.IsAuthenticated = true;
            
			this.OnSessionAuthenticated(session);
        }

		protected virtual void OnSessionAuthenticated(SimpleSession session) { } // => this.SessionAuthenticated?.Invoke(session);

		protected virtual async ValueTask OnBrodcastMessageSent(byte[] packageData, Encoding characterEncoding)
		{
			if (this.BrotcastMessageSent != null)
				await this.BrotcastMessageSent(packageData, characterEncoding);
		}

		#endregion |   Protected Methods   |

		#region |   Raise Event Methods   |


		//protected internal void RaiseSessionPackageJobAction(ISimpleSession session, PackageProcessingActionType jobActionType, PackageReader receivedPackage, ArraySegment<byte> sentPackageLength, ArraySegment<byte> sentPackageHeaderAnyBody)
		//{
		//	this.SessionPackageJobAction?.Invoke(session as SimpleSession, jobActionType, receivedPackage, sentPackageLength, sentPackageHeaderAnyBody);
		//}
		//protected virtual void RaiseSessionAuthorized(SimpleSession session) => this.SessionAuthenticated?.Invoke(session);

		#endregion |   Raise Event Methods   |
	}

	#region |   Event Delegates   |

	public delegate ValueTask SessionAsyncEventHandler(SimpleSession session);
	public delegate ValueTask SessionCloseAsyncEventHandler(SimpleSession session, CloseReason closeReason);
	//public delegate void SessionPackageActionEventHandler(SimpleSession session, PackageProcessingActionType jobActionType, PackageInfo receivedPackage, ArraySegment<byte> sentPackageLength, ArraySegment<byte> sentPackageHeaderAnyBody);
	//public delegate ValueTask PackageDataEventAsyncHandler(byte[] packageData);
	public delegate ValueTask PackageDataEncodingEventAsyncHandler(byte[] packageData, Encoding characterEncoding);
	public delegate ValueTask SessionPackageDataAsyncEventHandler(SimpleSession session, byte[] packageData);
	public delegate ValueTask SessionErrorMessagePackageDataAsyncEventHandler(SimpleSession session, string errorMessage, string errorDescription, byte[] packageData);
	public delegate ValueTask SessionRequestResponsePackageInfoAsyncEventHandler(SimpleSession session, byte[] requestPackageData, byte[]? responsePackageData);

	#endregion |   Event Delegates   |
}
