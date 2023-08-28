using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SuperSocket;
using SuperSocket.ProtoBase;
using SuperSocket.Client;
using SuperSocket.Channel;
using Simple.Serialization;

namespace Simple.SocketEngine
{
	public abstract class ClientBase : PackageEngine, ISimpleSession //, IAppSession //: EasyClient<PackageReader, PackageWriter>
	{
		#region |   Private Members   |

		private IPackageEncoder<PackageWriter> packageEncoder;
		private EndPoint? remoteEndPoint = null;
		private bool useChannelEncription = false;
		private bool useChannelCompression = false;
		private IEasyClient<PackageReader>? provider = null;
		private bool isConnected = false;
		private bool isAuthenticated = false;
		//private long sessionKey = 0;
		//private long userId;
		//private string? username = null;
		private bool startReceiveOnConnect = true;
		private Encoding characterEncoding = new UTF8Encoding(false); //Encoding.UTF8;
		private ManualResetEvent requestResetEvent = new ManualResetEvent(false);
		private PackageReader? response = null;
		private readonly object lockRequest = new object();

        //private int numOfRetry = 1;

        #endregion |   Private Members   |

        #region |   Constructors and Initialization   |

        public ClientBase()
			: this(new PackageEncoder())
        {
        }

        public ClientBase(IPackageEncoder<PackageWriter> packageEncoder)
		{
			this.packageEncoder = packageEncoder;
		}

		#endregion |   Constructors and Initialization   |

		#region |   Events   |

		public event AsyncEventHandler? Connected;
		public event EventHandler? Closed;

		#endregion |   Events   |

		#region |   Protected Properties   |

		protected internal IEasyClient<PackageReader>? Provider
		{
			get => this.provider;
			set
			{
				if (this.provider != null)
				{
					this.provider.PackageHandler -= this.Client_PackageHandler;
					this.provider.Closed -= this.Client_Closed;
				}

				this.provider = value;

				if (this.provider != null)
				{
					this.provider.PackageHandler += this.Client_PackageHandler;
					this.provider.Closed += this.Client_Closed;
				}
			}
		}

		#endregion |   Protected Properties   |

		#region |   Public Properties   |


		public bool IsConnected => this.isConnected;
		public bool IsAuthenticated { get => this.isAuthenticated; protected set => this.isAuthenticated = value; }
		//public long SessionKey => this.sessionKey;
		//public long UserId { get => this.userId; protected set => this.userId = value; }

		public Encoding CharacterEncoding { get => this.characterEncoding; set => this.characterEncoding = value; }

		public EndPoint? RemoteEndPoint => this.remoteEndPoint;
		public EndPoint? LocalEndPoint => this.Provider?.LocalEndPoint;

		///// <summary>
		///// Request-Response wait timeout in miliseconds.
		///// </summary>
		//public int Timeout { get => this.PackageEngine.Timeout; set => this.PackageEngine.Timeout = value; }
		//public int NumOfRetry { get => this.PackageEngine.NumOfRetry; set => this.PackageEngine.NumOfRetry= value; }

		public bool UseChannelEncryption { get => this.useChannelEncription; set => this.useChannelEncription = value; }
		public bool UseChannelCompression { get => this.useChannelCompression; set => this.useChannelCompression = value; }
		public bool StartReceiveOnConnect { get => this.startReceiveOnConnect; set => this.startReceiveOnConnect = value; }

		#endregion |   Public Properties   |

		#region |   Protected Virtual Methods   |

		protected abstract object GetCommandOwnerInstance();

		//protected override IPipelineFilter<PackageInfo> CreatePipelineFilter(object session) => new PipelineFilter(this.PackageArgsFactory, createPackageDataCopy: false);

		protected virtual object CreatePipelineContext() => this as ISimpleSession;

		protected virtual ChannelOptions CreateCannelOptions() => new ChannelOptions 
		{ 
			Logger = GetLoggerFactory().CreateLogger(nameof(ClientBase)), 
			ReceiveBufferSize = this.ReceiveBufferSize, 
			SendBufferSize = this.SendBufferSize 
		};

		protected virtual ILoggerFactory GetLoggerFactory() => LoggerFactory.Create(builder => builder.AddDebug());

		protected virtual IEasyClient<PackageReader> CreateClientProvider()
		{
			var pipelineFilter = this.CreatePipelineFilter(this);
			var channelOptions = this.CreateCannelOptions();
			var hostConfigurator = this.CreateHostConfigurator(this.UseChannelCompression, this.UseChannelEncryption);

			pipelineFilter.Context = this.CreatePipelineContext();

			if (channelOptions.Logger != null)
				this.Logger = channelOptions.Logger;

			return hostConfigurator.ConfigureEasyClient(pipelineFilter, channelOptions);
		}

		#endregion |   Protected Virtual Methods   |

		#region |   Public Methods   |

		public async ValueTask<bool> ConnectAsync(EndPoint remoteEndPoint, CancellationToken cancellationToken = default)
		{
			if (this.Provider != null)
				await this.ClientCloseAsync();

			this.remoteEndPoint = remoteEndPoint;
			this.Provider = this.CreateClientProvider();

			var task = this.Provider.ConnectAsync(remoteEndPoint, cancellationToken);
			await task;

			this.isConnected = task.Result; // IsCompleted;

			var connectedEventHandler = this.Connected;

			if (connectedEventHandler != null)
				await connectedEventHandler.Invoke(this, EventArgs.Empty);

			if (this.Connected != null && this.isConnected)
				this.Connected?.Invoke(this, EventArgs.Empty);

			if (this.isConnected && this.StartReceiveOnConnect)
				this.StartReceive();

			//if (this.isConnected)
			//	await this.OnConnect();

			return task.Result;
		}

		//protected virtual ValueTask OnConnect() => new ValueTask();

		public void StartReceive() => this.Provider?.StartReceive();

		public async ValueTask CloseAsync() => await this.ClientCloseAsync();

		public async ValueTask<bool> SendMessage(int messageCode, MessageArgs? messageArgs)
		{
			return await this.SendMessage(PackageEngine.MessageFlags, messageCode, messageArgs);
		}

		public async ValueTask<bool> SendSystemMessage(int messageCode, MessageArgs? messageArgs)
		{
			return await this.SendMessage(PackageEngine.MessageSystemFlags, messageCode, messageArgs);
		}

		public async ValueTask<TResponseArgs> SendRequest<TResponseArgs>(int requestId, RequestArgs? requestArgs = null)
			where TResponseArgs : ResponseArgs, new()
		{
			return await this.SendRequest<TResponseArgs>(PackageEngine.RequestFlags, requestId, requestArgs);
		}

		public async ValueTask<TResponseArgs> SendSystemRequest<TResponseArgs>(int requestId, RequestArgs? requestArgs = null)
			where TResponseArgs : ResponseArgs, new()
		{
			return await this.SendRequest<TResponseArgs>(PackageEngine.RequestSystemFlags, requestId, requestArgs);
		}

		#endregion |   Public Methods   |

		#region |   Protected Methods   |

		protected virtual ValueTask OnMessageSent(byte[] packageData) => new ValueTask();

		protected virtual ValueTask OnRequestSent(byte[] requestPackageData, byte[]? responsePackageData) => new ValueTask();

		#endregion |   Protected Methods   |

		#region |   Private Methods   |

		private async ValueTask<bool> SendMessage(HeaderInfo flags, int messageCode, MessageArgs? messageArgs)
		{
			PackageWriter package = new PackageWriter(flags, messageCode, this.CharacterEncoding, session: this, messageArgs);
			bool succeeded;

			try
			{
				await this.SendAsync(this.PackageEncoder, package); //.DoNotAwait(); // ConfigureAwait(false);
				
				succeeded = true;
				
				if (package.Buffer != null)
					await this.OnMessageSent(package.Buffer);
			}
			catch (Exception ex)
			{
				succeeded = false;
				this.Logger?.LogDebug(String.Format("Error in sending message, MessageId={0} ({1})", messageCode, ex.GetFullErrorMessage()));
			}

			return succeeded;
		}

		private async ValueTask<TResponseArgs> SendRequest<TResponseArgs>(HeaderInfo headerInfo, int requestId, RequestArgs? requestArgs)
			where TResponseArgs : ResponseArgs, new()
		{
			PackageWriter request;
			PackageReader? response = null;
			TResponseArgs result;

			if (!this.IsConnected)
			{
				result = new TResponseArgs();
				result.Status = PackageStatus.NoSuchData;
				result.ErrorMessage = "Session is not connected.";
			}
			else
			{
				lock (this.lockRequest)
				{
					this.requestResetEvent.Reset(); // Prepare
					request = new PackageWriter(headerInfo, key: requestId, this.CharacterEncoding, session: this, requestArgs);

					int retry = 0;
					bool isEnd = false;
					CancellationToken cancellationToken = CancellationToken.None;

					while (!isEnd)
					{
						// If timeout occur we need to inform user or main form that we are disconnected from the server and that traying to establishing connection...
						// When connection is established, inform that connection is reestablished and everything is back to normal
						try
						{
							this.Provider!.SendAsync(this.packageEncoder, request).ConfigureAwait(false); // .GetAwaiter().GetResult(); //.DoNotAwait();
							this.requestResetEvent.WaitOne(millisecondsTimeout: Int32.MaxValue); // TEST ONLY !!!! In a production put sam value here and catch timeout 
							response = this.response;
							isEnd = true;
						}
						catch (TimeoutException ex)
						{
							retry++;

							if (retry > this.NumOfRetry)
							{
								result = new TResponseArgs();
								result.Status = PackageStatus.TimeOut;
								result.ErrorMessage = ExceptionHelper.GetFullErrorMessage(ex);
								this.Logger?.LogDebug("Error in waiting for request response, num of retry excceeded, RequestId=" + requestId);

								isEnd = true;
							}
						}
					}

					if (response is null)
					{
						result = new TResponseArgs();
						result.Status = PackageStatus.Error;
						result.ErrorMessage = $"The response reuslt cannot be custed to {typeof(TResponseArgs).Name}";
					}
					else if (response.PackageArgs is TResponseArgs responseArgs)
					{
						result = responseArgs;
					}
					else if (response.PackageArgs != null)
					{
						result = new TResponseArgs();
						result.Status = PackageStatus.PackageArgsTypeDoNotMach;
						result.ErrorMessage = $"The response reuslt package args do not mach with requested result type {typeof(TResponseArgs).Name}";
					}
					else
					{
						result = new TResponseArgs();
						result.Status = PackageStatus.PackageArgsIsNull;
						result.ErrorMessage = $"Package args are null";
					}
				}

				if (request.Buffer != null)
					await this.OnRequestSent(request.Buffer, response?.Buffer).ConfigureAwait(false);
			}

			return result;
		}

		private async ValueTask ClientCloseAsync()
		{
			if (this.Provider == null)
				return;

			try // Client Channel can be null snd will throw object null reference exception. Ignore it, not connected anyway.
			{
				await this.Provider.CloseAsync();
			}
			catch 
			{	
			}
			finally
			{
				this.isConnected = false;
				this.isAuthenticated = false;
			}
		}

		private ValueTask Client_PackageHandler(EasyClient<PackageReader> sender, PackageReader packageInfo)
		{
			return this.OnPackageReceived(commandOwner: this.GetCommandOwnerInstance(), session: this, packageInfo, this.CommandDiscovery); // .DoNotAwait();
		}

		private void Client_Closed(object? sender, EventArgs e)
		{
			this.isConnected = false;
			this.isAuthenticated = false;
			this.remoteEndPoint = null;

			var closedEventHandler = this.Closed;

			if (closedEventHandler != null)
				closedEventHandler.Invoke(this, EventArgs.Empty);
		}

		#endregion |   Private Methods   |

		#region |   ISimpleSession, IAppSession Interfaces Implementation   |

		//IChannel IAppSession.Channel => null!;

		//object IAppSession.DataContext { get => null!; set { } }

		//async ValueTask IAppSession.CloseAsync(CloseReason reason)
		//{
		//	if (this.Client != null)
		//		await this.Client.CloseAsync();
		//}

		async ValueTask ISimpleSession.SendAsync(ReadOnlyMemory<byte> data)
		{
			if (this.Provider != null)
				await this.Provider.SendAsync(data);
		}

		async ValueTask ISimpleSession.SendAsync<TPackage>(IPackageEncoder<TPackage> packageEncoder, TPackage package)
		{
			if (this.Provider != null)
				await this.Provider.SendAsync(packageEncoder, package);
		}

		void ISimpleSession.ResponseIsReceived(PackageReader response)
		{
			this.response = response;
			this.requestResetEvent.Set();
		}

		#endregion |   ISimpleSession, IAppSession Interface Implementation   |
	}
}
