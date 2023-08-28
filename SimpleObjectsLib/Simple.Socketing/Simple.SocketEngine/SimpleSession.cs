using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
//using System.Threading.Tasks.Dataflow;
using SuperSocket;
using SuperSocket.ProtoBase;
using SuperSocket.Server;
//using Simple.Objects;

namespace Simple.SocketEngine
{
	// TODO: Add Encoding property to it
	public class SimpleSession : AppSession, ISimpleSession, IAppSession, ILogger, ILoggerAccessor // AppSession<AppSession, PackageInfo, int>, IAppSession<AppSession, PackageInfo>, IAppSession, ISimpleAppSession, IThreadExecutingContext
	{
		#region |   Private Members   |

		private IPackageEncoder<PackageWriter> packageEncoder;
		private bool[] isObjectModelFetchedByTableId = new bool[0];
		private ManualResetEvent requestResetEvent = new ManualResetEvent(false);
		private PackageReader? response = null;
		private readonly object lockRequest = new object();
		public int numOfRetry = 3;

        //private ActionBlock<PackageInfo> processOnPackageReceiveAction = null;
        //private ActionBlock<PackageInfo> processPackageTransactionRequest;
        //private ActionBlock<PackageInfo> processOnOtherPackageReceived;


        #endregion |   Private Members   |

        #region |   Public Static Members   |

        //public static SimpleSession Broadcast = new SimpleSession() { SessionKey = 0 };

        #endregion |   Public Static Members   |

        #region |   Constructors and Initialization   |

        public SimpleSession()
			: this(new PackageEncoder())
        {
        }

        public SimpleSession(IPackageEncoder<PackageWriter> packageEncoder)
		{
			this.packageEncoder = packageEncoder;
		}

		#endregion |   Constructors and Initialization   |

		#region |   Proptected Properties   |

		protected IPackageEncoder<PackageWriter> PackageEncoder => this.packageEncoder;

		#endregion |   Proptected Properties   |


		#region |   Public Properties   |

		//public PackageEngine PackageEngine { get; private set; }


		/// <summary>
		/// Get the endpoint authenticated user name
		/// </summary>             
		/// 
		public string Username { get; internal set; } = String.Empty;

		/// <summary>
		/// Gets the endpoint User unique identificator
		/// </summary>
		public long UserId { get; internal set; }

		/// <summary>
		/// Gets the endpoint ClientId.
		/// </summary>
		public long ClientId { get; internal set; }

		/// <summary>
		/// Gets the unique session identifier created by the server.
		/// </summary>
		public long SessionKey { get; internal set; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="SuperSocket.SocketBase.IAppSession"/> is authenticated.
		/// </summary>
		/// <value>
		///   <c>true</c> if authenticated; otherwise, <c>false</c>.
		/// </value>
		public bool IsAuthenticated { get; internal set; }
		public bool IsConnected => this.State == SessionState.Connected;

		public Encoding CharacterEncoding { get; set; } = new UTF8Encoding(false);

		public bool[] IsObjectModelFetchedByTableId => this.isObjectModelFetchedByTableId;

		#endregion |   Public Properties   |

		#region |   Public Methods   |

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

		internal void SetIsObjectModelFetchedByTableIdArray(bool[] isObjectModelFetchedByTableIdArray) => this.isObjectModelFetchedByTableId = isObjectModelFetchedByTableIdArray;


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
					this.requestResetEvent.Reset();
					request = new PackageWriter(headerInfo, key: requestId, this.CharacterEncoding, session: this, requestArgs);

					int retry = 0;
					bool isEnd = false;
					CancellationToken cancellationToken = CancellationToken.None;

					while (!isEnd)
					{
						// Ofter timeout session should be susspended

						try
						{
							this.SendAsync(this.PackageEncoder, request).ConfigureAwait(false); // .DoNotAwait();
							this.requestResetEvent.WaitOne(millisecondsTimeout: Int32.MaxValue);

							response = this.response;
							isEnd = true;
						}
						catch (TimeoutException ex)
						{
							retry++;

							if (retry > this.numOfRetry)
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

		#endregion |   Private Methods   |

		#region |   Interfaces Implementation   |

		ValueTask ISimpleSession.SendAsync(ReadOnlyMemory<byte> data) => this.Channel.SendAsync(data);

		ValueTask ISimpleSession.SendAsync<TPackage>(IPackageEncoder<TPackage> packageEncoder, TPackage package) => this.Channel.SendAsync(packageEncoder, package);

		void ISimpleSession.ResponseIsReceived(PackageReader response)
		{
			this.response = response;
			this.requestResetEvent.Set();
		}

		#endregion |   Interfaces Implementation   |
	}
}
