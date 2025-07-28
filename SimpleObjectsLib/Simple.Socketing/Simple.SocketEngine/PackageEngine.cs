using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Buffers;
using System.Reflection;
using System.Net.Sockets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperSocket;
using SuperSocket.ProtoBase;
using SuperSocket.Server.Abstractions;
using SuperSocket.Server.Abstractions.Host;
using SuperSocket.Server.Abstractions.Connections;
using Simple.Collections;
using Simple.Serialization;
using Simple.Threading;
using System.CodeDom;
using System.DirectoryServices;
using System.Threading;
using System.Collections;
using SuperSocket.Server.Host;

namespace Simple.SocketEngine
{
	public abstract class PackageEngine //: IPipelineFilter<PackageInfo>, IPipelineFilter, IPackageDecoder<PackageInfo> //, IPackageEncoder<PackageWriter>
	{
		#region |   Private Memebers   |

		private PackageArgsFactory packageArgsFactory;
		private CommandDiscovery commandDiscovery;
		private IPackageEncoder<PackageWriter> packageEncoder; // PackageEncoding.Encoder;
		private int timeoutInMilliseconds = 10000; // System.Threading.Timeout.Infinite; // indefinetly wait for response
		private int numOfRetry = 3;
		private TokenGenerator<int> tokenGenerator = new TokenGenerator<int>();
		//private ThreadSyncWithDataExchange<int, PackageReader> threadSync = new ThreadSyncWithDataExchange<int, PackageReader>();
		private ThreadSync<int> threadSync = new ThreadSync<int>();
		private Dictionary<int, PackageReader> responsesByToken = new Dictionary<int, PackageReader>();
		//private HashArray<PackageReader?> responsePackagesByToken = new HashArray<PackageReader?>();
		//private Dictionary<int, ResponseArgs> responseArgsByToken = new Dictionary<int, ResponseArgs>();
		//private Dictionary<int, object?> responseContextByToken = new Dictionary<int, object?>();
		private PipelineFilter? filter;
		private ILogger? logger = null;
		private bool createPackageDataCopy = false;

		#endregion |   Private Memebers   |

		#region |   Public Static Memebers   |

		public static readonly HeaderInfo MessageFlags							 = new HeaderInfo(PackageType.Message,  RecipientModel.SessionEndpoint, isSystem: false);
		public static readonly HeaderInfo MessageSystemFlags					 = new HeaderInfo(PackageType.Message,  RecipientModel.SessionEndpoint, isSystem: true);
		public static readonly HeaderInfo MessageMulticastFlags					 = new HeaderInfo(PackageType.Message,  RecipientModel.Multicast,		isSystem: false);
		public static readonly HeaderInfo MessageSystemMulticastFlags			 = new HeaderInfo(PackageType.Message,  RecipientModel.Multicast,		isSystem: true);
		public static readonly HeaderInfo MessageBrodcastFlags					 = new HeaderInfo(PackageType.Message,  RecipientModel.Brodcast,		isSystem: false);
		public static readonly HeaderInfo MessageSystemBrodcastFlags			 = new HeaderInfo(PackageType.Message,  RecipientModel.Brodcast,		isSystem: true);

		public static readonly HeaderInfo RequestFlags							 = new HeaderInfo(PackageType.Request,  RecipientModel.SessionEndpoint, isSystem: false);
		public static readonly HeaderInfo RequestSystemFlags					 = new HeaderInfo(PackageType.Request,  RecipientModel.SessionEndpoint, isSystem: true);
		public static readonly HeaderInfo RequestMulticastFlags					 = new HeaderInfo(PackageType.Request,  RecipientModel.Multicast,		isSystem: false);
		public static readonly HeaderInfo RequestSystemMulticastFlags			 = new HeaderInfo(PackageType.Request,  RecipientModel.Multicast,		isSystem: true);
		public static readonly HeaderInfo RequestBrodcastFlags					 = new HeaderInfo(PackageType.Request,  RecipientModel.Brodcast,		isSystem: false);
		public static readonly HeaderInfo RequestSystemBrodcastFlags			 = new HeaderInfo(PackageType.Request,  RecipientModel.Brodcast,		isSystem: true);

		public static readonly HeaderInfo ResponseFlags							 = new HeaderInfo(PackageType.Response, RecipientModel.SessionEndpoint, isSystem: false, responseSucceed: true);
		public static readonly HeaderInfo ResponseNotSucceedFlags				 = new HeaderInfo(PackageType.Response, RecipientModel.SessionEndpoint, isSystem: false, responseSucceed: false);
		public static readonly HeaderInfo ResponseSystemFlags					 = new HeaderInfo(PackageType.Response, RecipientModel.SessionEndpoint, isSystem: true,  responseSucceed: true);
		public static readonly HeaderInfo ResponseSystemNotSucceedFlags			 = new HeaderInfo(PackageType.Response, RecipientModel.SessionEndpoint, isSystem: true,  responseSucceed: false);
		public static readonly HeaderInfo ResponseMulticastFlags				 = new HeaderInfo(PackageType.Response, RecipientModel.Multicast,		isSystem: false, responseSucceed: true);
		public static readonly HeaderInfo ResponseMulticastNotSucceedFlags		 = new HeaderInfo(PackageType.Response, RecipientModel.Multicast,		isSystem: false, responseSucceed: false);
		public static readonly HeaderInfo ResponseSystemMulticastFlags			 = new HeaderInfo(PackageType.Response, RecipientModel.Multicast,		isSystem: true,  responseSucceed: true);
		public static readonly HeaderInfo ResponseSystemMulticastNotSucceedFlags = new HeaderInfo(PackageType.Response, RecipientModel.Multicast,		isSystem: true,  responseSucceed: false);
		public static readonly HeaderInfo ResponseBrodcastFlags					 = new HeaderInfo(PackageType.Response, RecipientModel.Brodcast,		isSystem: false, responseSucceed: true);
		public static readonly HeaderInfo ResponseBrodcastNotSucceedFlags		 = new HeaderInfo(PackageType.Response, RecipientModel.Brodcast,		isSystem: false, responseSucceed: false);
		public static readonly HeaderInfo ResponseSystemBrodcastFlags			 = new HeaderInfo(PackageType.Response, RecipientModel.Brodcast,		isSystem: true,  responseSucceed: true);
		public static readonly HeaderInfo ResponseSystemBrodcastNotSucceedFlags  = new HeaderInfo(PackageType.Response, RecipientModel.Brodcast,		isSystem: true,  responseSucceed: false);

		#endregion |   Public Static Memebers   |

		#region |   Constructor Initialization   |

		public PackageEngine()
		{
			this.packageArgsFactory = this.CreatePackageArgsFactory();
			this.commandDiscovery = this.CreateComandDiscovery();
			this.packageEncoder = this.CreatePackageEncoder();
			//this.packageInfo = new PackageInfo(this.PackageArgsFactory);
			//this.packageEncoding = new PackageEncoding(this.CommandDiscovery);
		}

		#endregion |   Constructor Initialization   |

		#region |   Protected Properties   |

		protected IPackageEncoder<PackageWriter> PackageEncoder => this.packageEncoder;

		#endregion |   Protected Properties   |

		#region |   Public Properties   |

		/// <summary>
		/// Timeout in millisoeconds.
		/// </summary>
		public int Timeout { get => this.timeoutInMilliseconds; set => this.timeoutInMilliseconds = value; }
		public int NumOfRetry { get => this.numOfRetry; set => this.numOfRetry = value; }
		public int ReceiveBufferSize { get; set; } = 1024 * 12; // 12k
		public int SendBufferSize { get; set; } = 1024 * 12; // 12k
		public ILogger? Logger { get => this.logger; set => this.logger = value; }
		public bool CreatePackageDataCopy
		{
			get => this.createPackageDataCopy;
			set
			{
				this.createPackageDataCopy = value;

				if (this.filter != null)
					this.filter.CreatePackageDataCopy = value;

				if (this.packageEncoder is PackageEncoder packageEncoder)
					packageEncoder.CreateDataCopy = value;
			}
		}

		#endregion |   Public Properties   |

		#region |   Host Configurator   |

		protected static readonly Encoding DefaultEncoding = new UTF8Encoding();
		protected static ILoggerFactory DefaultLoggerFactory { get; } = LoggerFactory.Create(builder => { builder.AddConsole(); });

		protected PackageArgsFactory PackageArgsFactory => this.packageArgsFactory;
		protected CommandDiscovery CommandDiscovery => this.commandDiscovery;

		//protected virtual PackageArgsFactory CreatePackageArgsFactory() => new PackageArgsFactory();

		protected abstract PackageArgsFactory CreatePackageArgsFactory();

		//protected virtual List<Assembly> GetPackageArgsAssemblies()
		//{
		//	var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
		//	var baseType = this.GetType();
		//	IEnumerable<Type> types = ReflectionHelper.SelectAssemblyTypes(allAssemblies, type => type == baseType || baseType.IsSubclassOf(type) && !type.IsAbstract &&
		//																						  !type.IsInterface && !type.IsGenericType && type != typeof(object));
		//	var assemblies = from type in types
		//					 select type.Assembly;

		//	return assemblies.ToList();
		//}
		//protected virtual PackageArgsFactory CreatePackageArgsFactory() => new PackageArgsFactory(this.GetPackageArgsAssemblyList());
		//protected virtual PackageArgsFactory CreatePackageArgsFactory() => new PackageArgsFactory(AppDomain.CurrentDomain.GetAssemblies());
		//protected virtual object? GetPackageArgsSerializationContext(PackageType packageType, int requestIdOrMessageCode, bool isSystem) => null;

		protected virtual IPipelineFilter<PackageReader> CreatePipelineFilter(object session) 
		{
			this.filter = new PipelineFilter(this.PackageArgsFactory, this.CreatePackageDataCopy); // this.CreatePipelineFilter(session);
			this.filter.CreatePackageDataCopy = this.createPackageDataCopy;
			//filter.Decoder = this.packageEncoding;

			return this.filter;
		}

		protected virtual CommandDiscovery CreateComandDiscovery() => new CommandDiscovery(this.GetType());

		protected virtual IPackageEncoder<PackageWriter> CreatePackageEncoder() => new PackageEncoder();
		protected virtual int GetServerListeningPort() => 0; // => new IPEndPoint(IPAddress.Loopback, DefaultServerPort);

		protected virtual void ConfigureServices(HostBuilderContext context, IServiceCollection services) { }

		protected virtual IHostConfigurator CreateHostConfigurator(bool useChannelCompression, bool useChannelEncryption)
		{
			IHostConfigurator hostConfigurator;

			if (useChannelCompression)
			{
				if (useChannelEncryption)
					hostConfigurator = new GzipSecureHostConfigurator();
				else
					hostConfigurator = new GzipHostConfigurator();
			}
			else
			{
				if (useChannelEncryption)
					hostConfigurator = new SecureHostConfigurator();
				else
					hostConfigurator = new RegularHostConfigurator();
			}

			return hostConfigurator;
		}

		protected SuperSocketHostBuilder<TPackageInfo> CreateSocketServerBuilder<TPackageInfo>(IHostConfigurator configurator = null)
			where TPackageInfo : class
		{
			var hostBuilder = SuperSocketHostBuilder.Create<TPackageInfo>();

			return (this.Configure(hostBuilder, configurator) as SuperSocketHostBuilder<TPackageInfo>)!;
		}

		protected SuperSocketHostBuilder<TPackageInfo> CreateSocketServerBuilder<TPackageInfo>(Func<IPipelineFilter<TPackageInfo>> filterFactory, IHostConfigurator configurator = null)
			where TPackageInfo : class
		{
			var hostBuilder = SuperSocketHostBuilder.Create<TPackageInfo>();

			hostBuilder.UsePipelineFilterFactory(filterFactory);

			return (this.Configure(hostBuilder, configurator) as SuperSocketHostBuilder<TPackageInfo>)!;
		}

		protected SuperSocketHostBuilder<TPackageInfo> CreateSocketServerBuilder<TPackageInfo, TPipelineFilter>(IHostConfigurator configurator = null)
			where TPackageInfo : class
			where TPipelineFilter : class, IPipelineFilter<TPackageInfo>
		{
			var hostBuilder = SuperSocketHostBuilder.Create<TPackageInfo>();

			hostBuilder.UsePipelineFilter<TPipelineFilter>();

			return (this.Configure(hostBuilder, configurator) as SuperSocketHostBuilder<TPackageInfo>)!;
		}

		protected T CreateObject<T>(Type type) => (T)ActivatorUtilities.CreateFactory(type, new Type[0]).Invoke(null, null);

		protected Socket CreateClient(IHostConfigurator hostConfigurator) => hostConfigurator.CreateClient();

		protected ISuperSocketHostBuilder Configure(ISuperSocketHostBuilder hostBuilder, IHostConfigurator? configurator = null)
		{
			var builder = hostBuilder.ConfigureAppConfiguration((hostCtx, configApp) =>
			{
				configApp.AddInMemoryCollection(new Dictionary<string, string>
					{
						{ "serverOptions:name", "SimpleServer" },
						{ "serverOptions:listeners:0:ip", "Any" },
						{ "serverOptions:listeners:0:backLog", "100" },
						{ "serverOptions:listeners:0:port", this.GetServerListeningPort().ToString() }
					});
			})
			.ConfigureLogging((hostCtx, loggingBuilder) =>
			{
				loggingBuilder.AddConsole();
				loggingBuilder.SetMinimumLevel(LogLevel.Debug);
				loggingBuilder.AddDebug();
			})
			.ConfigureServices((hostCtx, services) =>
			{
				ConfigureServices(hostCtx, services);
			})
			as ISuperSocketHostBuilder;

			configurator?.Configure(builder!);

			return builder!;
		}

		#endregion |   Host Configurator   |

		#region |   Package Sending, Receiving, Processing   |

		//protected async ValueTask<bool> SendMessage(ISimpleSession session, int messageCode, MessageArgs? messageArgs)
		//{
		//	return await this.SendMessage(session, MessageFlags, messageCode, messageArgs);
		//}

		//protected async ValueTask<bool> SendSystemMessage(ISimpleSession session, int messageCode, MessageArgs? messageArgs)
		//{
		//	return await this.SendMessage(session, MessageSystemFlags, messageCode, messageArgs);
		//}

		//protected async ValueTask<bool> SendMessage(ISimpleSession session, HeaderInfo flags, int messageCode, MessageArgs? messageArgs)
		//{
		//	PackageWriter package = new PackageWriter(flags, messageCode, session.CharacterEncoding, session, messageArgs);
		//	bool succeeded;

		//	try
		//	{
		//		//package.WriteHeader();
		//		//package.WritePackageArgs();

		//		await session.SendAsync(this.PackageEncoder, package); //.DoNotAwait(); // ConfigureAwait(false);
		//		await this.OnMessageSent(session, package.Buffer!);
				
		//		succeeded = true; 

		//	}
		//	catch (Exception ex)
		//	{
		//		succeeded = false;
		//		this.Logger?.LogDebug(String.Format("Error in sending message, MessageId={0} ({1})", messageCode, ex.GetFullErrorMessage()));
		//	}

		//	return succeeded;
		//}

		//protected async ValueTask<TResponseArgs> SendRequest<TResponseArgs>(ISimpleSession session, int requestId, RequestArgs? requestArgs = null)
		//	where TResponseArgs : ResponseArgs, new()
		//{
		//	return await this.SendRequest<TResponseArgs>(session, RequestFlags, requestId, requestArgs);
		//}

		//protected async ValueTask<TResponseArgs> SendSystemRequest<TResponseArgs>(ISimpleSession session, int requestId, RequestArgs? requestArgs = null)
		//	where TResponseArgs : ResponseArgs, new()
		//{
		//	return await this.SendRequest<TResponseArgs>(session, RequestSystemFlags, requestId, requestArgs);
		//}


		//int tokenCount = 0;
		////int requestNumber = 0;
		//private ManualResetEvent requestResetEvent = new ManualResetEvent(false);
		//private PackageReader? response = null;


		//private Hashtable resetEvents = new Hashtable();
		//private Hashtable ResetEvents => Hashtable.Synchronized(this.resetEvents);
		//private readonly object lockRequest = new object();

		//private async ValueTask<TResponseArgs> SendRequest<TResponseArgs>(ISimpleSession session, HeaderInfo headerInfo, int requestId, RequestArgs? requestArgs) //, TResponseArgs responseArgs, object? responseContext = null)
		//	where TResponseArgs : ResponseArgs, new()
		//{
		//	PackageWriter request;
		//	PackageReader? response = null;
		//	TResponseArgs result;

		//	if (!session.IsConnected)
		//	{
		//		result = new TResponseArgs();
		//		result.Status = PackageStatus.NoSuchData;
		//		result.ErrorMessage = "Session is not connected.";
		//	}
		//	else
		//	{
		//		lock (this.lockRequest)
		//		{
		//			//ManualResetEvent resetEvent;
		//			int token = 0;

		//			//resetEvent = new ManualResetEvent(false);
		//			//token = this.tokenCount++; 
		//			//token = this.tokenGenerator.Generate();

		//			//if (this.ResetEvents.ContainsKey(token))
		//			//	this.ResetEvents[token] = resetEvent;
		//			//else
		//			//this.ResetEvents.Add(token, resetEvent); // Prepare
		//													 //this.ResetEvents[token] = resetEvent; // Prepare

		//			//this.requestNumber++;

		//			//if (this.requestNumber > 1)
		//			//	this.Logger?.LogDebug("Multiple request on same time, RequestId=" + requestId);

		//			//this.threadSync.Prepare(token);

		//			request = new PackageWriter(headerInfo, key: requestId, token, session.CharacterEncoding, session, requestArgs);
		//			int retry = 0;
		//			bool isEnd = false;
		//			CancellationToken cancellationToken = CancellationToken.None;

		//			while (!isEnd)
		//			{
		//				try
		//				{
		//					this.requestResetEvent.Reset();
							
		//					session.SendAsync(this.PackageEncoder, request).DoNotAwait();
		//					//await resetEvent.WaitOneAsync(millisecondsTimeout: Int32.MaxValue, cancellationToken); // <- TEST ONLY this.Timeout)
		//					this.requestResetEvent.WaitOne(millisecondsTimeout: Int32.MaxValue);

		//					response = this.response;
		//					//resetEvent.WaitOne(millisecondsTimeout: Int32.MaxValue); // <- TEST ONLY this.Timeout)

		//					//(this.ResetEvents[token] as ManualResetEvent)!.Close();
		//					//this.ResetEvents.Remove(token);
		//					//resetEvent.Close();

		//					//this.threadSync.WaitFor(token, millisecondsTimeout: Int32.MaxValue); // <- TEST ONLY  //Timeout.Timeout); !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

		//					//if (this.responsesByToken.TryGetValue(token, out response))
		//					//	this.responsesByToken.Remove(token);

		//					isEnd = true;
		//				}
		//				catch (TimeoutException ex)
		//				{
		//					retry++;

		//					if (retry > this.NumOfRetry)
		//					{
		//						result = new TResponseArgs();
		//						result.Status = PackageStatus.TimeOut;
		//						result.ErrorMessage = ExceptionHelper.GetFullErrorMessage(ex);
		//						this.Logger?.LogDebug("Error in waiting for request response, num of retry excceeded, RequestId=" + requestId);

		//						isEnd = true;
		//					}
		//				}
		//			}

		//			//this.ResetEvents.Remove(token);
		//			//this.tokenGenerator.Release(token);

		//			if (response is null)
		//			{
		//				result = new TResponseArgs();
		//				result.Status = PackageStatus.Error;
		//				result.ErrorMessage = $"The response reuslt cannot be custed to {typeof(TResponseArgs).Name}";
		//			}
		//			else if (response.PackageArgs is TResponseArgs responseArgs)
		//			{
		//				result = responseArgs;
		//			}
		//			else if (response.PackageArgs != null)
		//			{
		//				result = new TResponseArgs();
		//				result.Status = PackageStatus.PackageArgsTypeDoNotMach;
		//				result.ErrorMessage = $"The response reuslt package args do not mach with requested result type {typeof(TResponseArgs).Name}";
		//			}
		//			else
		//			{
		//				result = new TResponseArgs();
		//				result.Status = PackageStatus.PackageArgsIsNull;
		//				result.ErrorMessage = $"Package args are null";
		//			}
					
		//			//if (this.ResetEvents.ContainsKey(token))
		//			//{
		//			//(this.ResetEvents[token] as ManualResetEvent)!.Close();
		//			//lock (this.lockToken)
		//			//{
		//			//}

		//			//this.requestNumber--;
		//		}

		//		if (request.Buffer != null)
		//			await this.OnRequestSent(session, request.Buffer, response?.Buffer).ConfigureAwait(false);

		//	}

		//	return result;
		//}

		protected async ValueTask OnPackageReceived(object commandOwner, ISimpleSession session, PackageReader package, CommandDiscovery commandDiscovery)
		{
			HeaderInfo flags = package.HeaderInfo;
			
			if (flags.PackageType == PackageType.Request)
			{
				PackageReader requestPackage = package;
				//int token = requestPackage.Token;
				int requestId = requestPackage.Key; //  package is response => package.Key is RequestId
				bool acceptRequest = session.IsAuthenticated;
				Encoding encoding = session.CharacterEncoding;
				HeaderInfo responseFlags;
				ResponseArgs? responseArgs;

                if (!acceptRequest) // if session is not authorized, check if request require authorization
				{
					HashSet<int> requestIdsNotRequireAuthorization = (requestPackage.HeaderInfo.IsSystem) ? commandDiscovery.SystemRequestIdsNotRequireAuthorization :
																									 commandDiscovery.RequestIdsNotRequireAuthorization;
					acceptRequest = requestIdsNotRequireAuthorization.Contains(requestId);
				}

				if (!acceptRequest)
				{
					// Request require authorisation -> send back error response
					responseArgs = new ErrorResponseArgs(PackageStatus.NotAuthenticated, "Session is not authorized, RequestId=" + requestId);
					responseFlags = requestPackage.HeaderInfo.IsSystem ? ResponseSystemNotSucceedFlags : ResponseNotSucceedFlags;
					this.Logger?.LogDebug(responseArgs.ErrorMessage);
				}
				else
				{
					MethodInfo? method = null;
					HashArray<MethodInfo> methodsByRequestId;

					if (requestPackage.HeaderInfo.IsSystem)
					{
						methodsByRequestId = commandDiscovery.SystemRequestMethodsByRequestId;
						responseFlags = ResponseSystemFlags;
					}
					else
					{
						methodsByRequestId = commandDiscovery.RequestMethodsByRequestId;
						responseFlags = ResponseFlags;
					}

					try 
					{ 
						method = methodsByRequestId[requestId]; 
					}
					catch  
					{
						method = null;
					}

					if (method == null)
					{
						string errorMessage = "Unknown Request (no method to process found), RequestId=" + requestId;

						responseFlags = requestPackage.HeaderInfo.IsSystem ? ResponseSystemNotSucceedFlags : ResponseNotSucceedFlags;
						responseArgs = new ErrorResponseArgs(PackageStatus.UnknownRequest, errorMessage); 
						this.Logger?.LogDebug(errorMessage);
					}
					else
					{
						object[] methodArgs = new object[] { session, requestPackage };

						try
						{
							responseArgs = method.Invoke(commandOwner, methodArgs) as ResponseArgs;
						}
						catch (Exception ex)
						{
							responseFlags = requestPackage.HeaderInfo.IsSystem ? ResponseSystemNotSucceedFlags : ResponseNotSucceedFlags;
							responseArgs = new ErrorResponseArgs(PackageStatus.ExceptionIsCaughtOnRequestProcessing, ex.GetFullErrorMessage()); //  new RequestResult<object>(null, RequestResultInfo.ExceptionIsCaughtOnRequestProcessing, package.Token, ExceptionHelper.GetFullErrorMessage(ex));
							this.Logger?.LogDebug("ExceptionIsCaughtOnRequestProcessing: " + ex.GetFullErrorMessage());
						}
					}
				}

				PackageWriter responsePackage = new PackageWriter(responseFlags, key: requestId, encoding, session, responseArgs);

				//responsePackage.WriteHeader(); 
				//responsePackage.WritePackageArgs();
				
				await session.SendAsync(this.PackageEncoder, responsePackage);
				
				if (requestPackage.Buffer != null && responsePackage.Buffer != null)
					await this.OnRequestReceived(session, requestPackage.Buffer, responsePackage.Buffer);
			}
			else if (flags.PackageType == PackageType.Response)
			{
				PackageReader responsePackage = package;
				//int token = package.Token;
				//int requestId = package.Key;
				//ResponseArgs? responseArgs;

				//if (!this.responseArgsByToken.TryGetValue(token, out responseArgs))
				//{
				//	this.Logger?.LogDebug("ReasponseArgs is null for received request token. SendRequest must set ReasponseArgs when sending request, so nothing to do.");
				//}
				//else
				//{
				//	//if (responsePackage.HeaderInfo.ResponseSucceed)
				//	//{
				//	//	try
				//	//	{
				//	//		responseArgs.ReadFrom(responsePackage.SerializationReader);
				//	//	}
				//	//	catch (Exception ex)
				//	//	{
				//	//		responseArgs.Status = PackageStatus.ExceptionIsCaughtOnResponseProcessing;
				//	//		responseArgs.ErrorMessage = ExceptionHelper.GetFullErrorMessage(ex);
				//	//		this.Logger?.LogDebug($"Error reading response args from reader (RequestId={requestId})");
				//	//	}
				//	//}
				//	//else
				//	//{
				//	//	try
				//	//	{
				//	//		responseArgs.ReadErrorInfo(responsePackage.SerializationReader);
				//	//	}
				//	//	catch (Exception ex)
				//	//	{
				//	//		responseArgs.Status = PackageStatus.ExceptionIsCaughtOnResponseProcessing;
				//	//		responseArgs.ErrorMessage = ExceptionHelper.GetFullErrorMessage(ex);
				//	//		this.Logger?.LogDebug($"Error reading error response info from reader (RequestId={responsePackage.Key})");
				//	//	}
				//	//}
				//}

				//this.responseArgsByToken[token] = responsePackage.PackageArgs as ResponseArgs;

				//this.responsesByToken[token] = responsePackage;

				session.ResponseIsReceived(responsePackage);


				//this.response = package;
				
				//this.requestResetEvent.Set();
				
				//this.tokenGenerator.Release(token);

				//ManualResetEvent? resetEvent = this.ResetEvents[token] as ManualResetEvent;

				//if (resetEvent != null)
				//{
				//	//this.ResetEvents.Remove(token);
				//	resetEvent.Set();
				//	//resetEvent.Close();
				//}

				//this.threadSync.Release(token); //, responsePackage);
				if (responsePackage.Buffer != null)
					await this.OnResponseReceived(session, responsePackage.Buffer);
					
			}
			else if (flags.PackageType == PackageType.Message)
			{
				PackageReader messagePackage = package;
				int messageCode = package.Key; // package is message => package.Key is MessageCode
				MethodInfo? method = null;
				HashArray<MethodInfo> methodsByMessageCode = (package.HeaderInfo.IsSystem) ? commandDiscovery.SystemMessageMethodsByMessageCode : 
																							 commandDiscovery.MessageMethodsByMessageCode;
				try
				{
					method = methodsByMessageCode[messageCode];
				}
				catch
				{
					method = null;
				}

				if (method == null)
				{
					this.Logger?.LogDebug("Received unknown message, MessageCode = {0}.", messageCode);
				}
				else
				{
					object[] arguments = new object[] { session, package };

					try
					{
						method.Invoke(commandOwner, arguments);
					}
					catch (Exception ex)
					{
						this.Logger?.LogDebug("Exception is caught while executing message receive, MessageCode = " + messageCode + "\r\n" + ExceptionHelper.GetFullErrorMessage(ex));
					}
				}

				if (messagePackage.Buffer != null)
					await this.OnMessageReceived(session, messagePackage.Buffer);
			}
#if NETSTANDARD2_1_OR_GREATER
			//return new ValueTask();
#else
			//return ValueTask.CompletedTask;
#endif
		}

		//
		// TODO: Remove PackageReader and PaclageWriter from this methods arguments since it contains SerialitationReader thant cannot be rented outside the scope when received
		//
		//protected virtual ValueTask OnMessageSent(ISimpleSession session, byte[] packageData) => new ValueTask();
		protected virtual ValueTask OnRequestReceived(ISimpleSession session, byte[] requestPackageData, byte[] responsePackageData) => new ValueTask();
		protected virtual ValueTask OnResponseReceived(ISimpleSession session, byte[] responsePackageData) => new ValueTask();
		protected virtual ValueTask OnMessageReceived(ISimpleSession session, byte[] packageData) => new ValueTask();


		#endregion |   Package Sending, Receiving, Processing   |

		#region |   Public Static Helper Methods   |

		//Move this to PackageInfo


		/// <summary>
		/// Stores a 64-bit unsigned value into the stream using 7-bit encoding.
		/// 
		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
		/// </summary>
		/// <param name="packageLength">The length of the package.</param>
		/// <returns>The package length <see cref="ArraySegment{Byte}"/>.</returns>
		public static ArraySegment<byte> Create7BitEncodedInt64ArraySegment(long packageLength)
		{
			return new ArraySegment<byte>(Create7BitEncodedInt64(packageLength, out int count), 0, count);
		}

		/// <summary>
		/// Stores a 64-bit unsigned value into the stream using 7-bit encoding.
		/// 
		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
		/// </summary>
		/// <param name="packageLength">The length of the package.</param>
		/// <returns>The package length <see cref="ReadOnlySpan{Byte}"/>.</returns>
		public static ReadOnlySpan<byte> Create7BitEncodedInt64Span(long packageLength)
		{
			return new ReadOnlySpan<byte>(Create7BitEncodedInt64(packageLength, out int count), 0, count);
		}

		/// <summary>
		/// Stores a 64-bit unsigned value into the stream using 7-bit encoding.
		/// 
		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
		/// </summary>
		/// <param name="packageLength">The length of the package.</param>
		/// <returns>The package length <see cref="ReadOnlySequence{Byte}"/>.</returns>
		public static ReadOnlySequence<byte> Create7BitEncodedInt64Sequence(long packageLength)
		{
			return new ReadOnlySequence<byte>(Create7BitEncodedInt64(packageLength, out int count), 0, count);
		}

		/// <summary>
		/// Stores a 64-bit unsigned value into the stream using 7-bit encoding.
		/// 
		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
		/// </summary>
		/// <param name="packageLength">The length of the package.</param>
		/// <returns>The package length <see cref="Memory{Byte}"/>.</returns>
		public static Memory<byte> Create7BitEncodedInt64Memory(long packageLength)
		{
			return new Memory<byte>(Create7BitEncodedInt64(packageLength, out int count), 0, count);
		}

		/// <summary>
		/// Stores a 64-bit unsigned value into the stream using 7-bit encoding.
		/// 
		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
		/// </summary>
		/// <param name="value">The length of the package.</param>
		/// <returns>The package length <see cref="byte[]"/>.</returns>
		public static byte[] Create7BitEncodedInt64(long value, out int count)
		{
			byte[] buffer = new byte[9];
			var unsignedValue = unchecked(value);

			count = 0;

			while (unsignedValue >= 0x80)
			{
				buffer[count++] = ((byte)(unsignedValue | 0x80));
				unsignedValue >>= 7;
			}

			buffer[count++] = (byte)unsignedValue;

			return buffer;
		}

		#endregion |   Public Static Helper Methods   |

		#region |   Protected Methods   |

		protected void SetIsObjectModelFetchedByTableIdArray(SimpleSession session, bool[] isObjectModelFetchecdByTableId)
		{
			session.SetIsObjectModelFetchedByTableIdArray(isObjectModelFetchecdByTableId);
		}

		#endregion |   Protected Methods   |
	}
}
