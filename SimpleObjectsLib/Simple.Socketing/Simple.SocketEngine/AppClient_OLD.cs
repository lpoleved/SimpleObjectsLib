//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple;
//using System.Reflection;
//using System.IO;
//using System.Net;
//using System.Threading;
//using System.Threading.Tasks;
////using System.Threading.Tasks.Dataflow;
//using Simple.Collections;
////using Simple.Threading;
//using SuperSocket.ProtoBase;
//using SuperSocket.ClientEngine;

//namespace Simple.SocketEngine
//{
//	public class AppClient : EasyClientBase, ISimpleAppSession
//	{
//		private MessageRequestDiscoveryManager discoveryManager = null;
//		private PackageEngine packageEngine = null;
//		private bool isAuthorized = false;
//		private EndPoint remoteEndPoint = null;

//		private const int ConnectSleepInterval = 200;
//		//private ActionBlock<PackageInfo> processOnPackageReceiveAction = null;
//		//protected ThreadSyncToken threadSyncToken = new ThreadSyncToken("AppClient");

//		public AppClient()
//		{
//			this.discoveryManager = new MessageRequestDiscoveryManager(this.GetType());
//			this.packageEngine = new PackageEngine(socketEngine: this, session: this, discoveryManager: this.discoveryManager, logger: null, onSessionPackageJobActionCallback: null);

//			//this.PipeLineProcessor = new DefaultPipelineProcessor<PackageInfo>(new PackageHandlerWrap<PackageInfo>(this.OnPackageReceive), new DynamicHeaderReceiveFilter());
//			//this.PipeLineProcessor = new SimplePipelineProcessor(new DynamicHeaderReceiveFilter(), package => this.OnPackageReceive(package));

//			//this.PipeLineProcessor = new SimplePipelineProcessor(this.PackageEngine);

//			//If using SimplePipelineProcessor2 you need to remove line "this.OnPackageReceive(package as PackageInfo);" from HandlePackage method!
//			//this.PipeLineProcessor = new SimplePipelineProcessor2(new DynamicHeaderReceiveFilter(), this.PackageEngine);

//			this.PipeLineProcessor = new SimplePipelineProcessor3(this.OnPackageReceive);

//			//this.PipeLineProcessor = new DefaultPipelineProcessor<PackageInfo>(new DynamicHeaderReceiveFilter());

//			this.Connected += this.AppClient_Connected;

//			//this.processOnPackageReceiveAction = new ActionBlock<PackageInfo>(package => this.PackageEngine.OnPackageReceived(package));
//		}

//		//public event SessionPackageEventHandler PackageReceived;
//		//public event SessionPackageEventHandler PackageSent;
//		//public event SessionPackageJobActionEventHandler PackageJobAction;


//		protected PackageEngine PackageEngine
//		{
//			get { return this.packageEngine; }
//		}

//		/// <summary>
//		/// Timeout in seconds.
//		/// </summary>
//		public int Timeout
//		{
//			get { return this.PackageEngine.Timeout; }
//			set { this.PackageEngine.Timeout = value; }
//		}

//		public int NumOfRetry
//		{
//			get { return this.PackageEngine.NumOfRetry; }
//			set { this.PackageEngine.NumOfRetry = value; }
//		}

//		public EndPoint RemoteEndPoint
//		{
//			get { return this.remoteEndPoint; }
//		}

//		public bool IsAuthorized
//		{
//			get { return this.isAuthorized; }
//			protected set { this.isAuthorized = value; }
//		}

//		protected virtual void OnPackageReceive(PackageInfo package)
//		{
//			this.PackageEngine.OnPackageReceived(package);
//			////await Task.Run(() => this.PackageManager.OnPackageReceived(null, package));
//			////this.PackageManager.OnPackageReceived(null, package);
//			//this.processOnPackageReceiveAction.SendAsync(package);
//			////this.processOnPackageReceiveAction.Post(package);
//			////this.processOnPackageReceiveAction.Complete();
//		}

//		protected override void HandlePackage(IPackageInfo package)
//		{
//			this.OnPackageReceive(package as PackageInfo);
//		}


//		//protected override async void HandlePackage(IPackageInfo package) // OnPackageReceive
//		//{
//		//	await this.processOnPackageReceiveAction.SendAsync(package as PackageInfo);
//		//	//this.PackageManager.OnPackageReceived(null, package as PackageInfo);

//		//	//await this.PackageManager.OnPackageReceived(null, package as PackageInfo);

//		//	////this.processPackageOnReceiveAction.SendAsync(package);
//		//	//this.processOnPackageReceiveAction.Post(package as PackageInfo);
//		//	//this.processOnPackageReceiveAction.Complete();
//		//}

//		class PackageHandlerWrap<PackageInfo> : IPackageHandler<PackageInfo>
//			where PackageInfo : IPackageInfo
//		{
//			private Action<PackageInfo> handler;

//			public PackageHandlerWrap(Action<PackageInfo> handler)
//			{
//				this.handler = handler;
//			}

//			public void Handle(PackageInfo package)
//			{
//				this.handler(package);
//			}
//		}

//		public bool Connect(EndPoint endPoint)
//		{
//			int repeat = this.Timeout * 1000 / ConnectSleepInterval;
//			Task<bool> result = this.ConnectAsync(endPoint);

//			for (int i = 0; i < repeat; i++)
//			{
//				if (result.IsCompleted)
//				{
//					//connected = true;
//					break;
//				}
//				else
//				{
//					Thread.Sleep(ConnectSleepInterval);
//				}
//			}

//			return result.IsCompleted && this.IsConnected;
//		}

//		public override async Task<bool> ConnectAsync(EndPoint endPoint)
//		{
//			this.remoteEndPoint = endPoint;

//			return await base.ConnectAsync(endPoint);
//		}

//		public bool Disconnect()
//		{
//			var d = this.DisconnectAsync();

//			return d.Result;

//			//int repeat = this.Timeout * 1000 / ConnectSleepInterval;

//			//Task<bool> result = this.DisconnectAsync();

//			//for (int i = 0; i < repeat; i++)
//			//{
//			//	if (result.IsCompleted)
//			//	{
//			//		Thread.Sleep(ConnectSleepInterval);
//			//	}
//			//	else
//			//	{
//			//		break;
//			//	}
//			//}

//			//return result.IsCompleted && this.IsConnected;
//		}

//		//public Task<bool> Disconnect()
//		//{
//		//	return this.Close();
//		//}

//		public async Task<bool> DisconnectAsync()
//		{
//			return await this.Close();
//		}

//		//public virtual void InitializeProtocol()
//		//{
//		//}

//		protected virtual void OnConnected()
//		{
//		}

//		//public new Task<bool> Close()
//		//{
//		//	var result = base.Close();

//		//	this.ProtocolVersion = null;

//		//	return result;
//		//}

//		protected bool SendMessage(int messageCode)
//		{
//			return this.SendMessage<MessageArgs>(messageCode, null);
//		}

//		protected bool SendMessage<TMessageArgs>(int messageCode, Func<TMessageArgs> getRequestArgs)
//			where TMessageArgs : MessageArgs
//		{
//			return this.PackageEngine.SendMessageAsync(messageCode, getRequestArgs, isSystem: false, isBroadcast: false, isMulticast: false);
//		}

//		protected IRequestResult SendRequest<TRequestArgs>(int requestId)
//			where TRequestArgs : RequestArgs
//		{
//			return this.SendRequest<TRequestArgs>(requestId, null);
//		}

//		protected IRequestResult SendRequest<TRequestArgs>(int requestId, Func<TRequestArgs> getRequestArgs)
//			where TRequestArgs : RequestArgs
//		{
//			return this.PackageEngine.SendRequest<TRequestArgs, ResponseArgs>(requestId, getRequestArgs, null, isSystem: false, isBroadcast: false, isMulticast: false);
//		}

//		protected RequestResult<TResult> SendRequest<TResponseArgs, TResult>(int requestId, Func<TResponseArgs, TResult> getResulValue)
//			where TResponseArgs : ResponseArgs
//		{
//			return this.SendRequest<RequestArgs, TResponseArgs, TResult>(requestId, null, getResulValue);
//		}

//		protected RequestResult<TResult> SendRequest<TRequestArgs, TResponseArgs, TResult>(int requestId, Func<TRequestArgs> getRequestArgs, Func<TResponseArgs, TResult> getResulValue)
//			where TRequestArgs : RequestArgs
//			where TResponseArgs : ResponseArgs
//		{
//			return this.SendRequest(requestId, getRequestArgs, () => Activator.CreateInstance(typeof(TResponseArgs)) as TResponseArgs, getResulValue);
//		}

//		protected RequestResult<TResult> SendRequest<TRequestArgs, TResponseArgs, TResult>(int requestId, Func<TRequestArgs> getRequestArgs, Func<TResponseArgs> getResponseArgs, Func<TResponseArgs, TResult> getResulValue)
//			where TRequestArgs : RequestArgs
//			where TResponseArgs : ResponseArgs
//		{
//			return this.PackageEngine.SendRequest(requestId, getRequestArgs, getResponseArgs, getResulValue, isSystem: false, isBroadcast: false, isMulticast: false);
//		}


//		protected bool SendSystemMessage(int messageCode)
//		{
//			return this.SendSystemMessage<MessageArgs>(messageCode, null);
//		}

//		protected bool SendSystemMessage<TMessageArgs>(int messageCode, Func<TMessageArgs> getRequestArgs)
//			where TMessageArgs : MessageArgs
//		{
//			return this.PackageEngine.SendMessageAsync(messageCode, getRequestArgs, isSystem: true, isBroadcast: false, isMulticast: false);
//		}

//		protected IRequestResult SendSystemRequest<TRequestArgs>(int requestId)
//			where TRequestArgs : RequestArgs
//		{
//			return this.SendSystemRequest<TRequestArgs>(requestId, null);
//		}

//		protected IRequestResult SendSystemRequest<TRequestArgs>(int requestId, Func<TRequestArgs> getRequestArgs)
//			where TRequestArgs : RequestArgs
//		{
//			return this.PackageEngine.SendRequest<TRequestArgs, ResponseArgs>(requestId, getRequestArgs, null, isSystem: true, isBroadcast: false, isMulticast: false);
//		}

//		protected RequestResult<TResult> SendSystemRequest<TResponseArgs, TResult>(int requestId, Func<TResponseArgs, TResult> getResulValue)
//			where TResponseArgs : ResponseArgs
//		{
//			return this.SendSystemRequest<RequestArgs, TResponseArgs, TResult>(requestId, null, getResulValue);
//		}

//		protected RequestResult<TResult> SendSystemRequest<TRequestArgs, TResponseArgs, TResult>(int requestId, Func<TRequestArgs> getRequestArgs, Func<TResponseArgs, TResult> getResulValue)
//			where TRequestArgs : RequestArgs
//			where TResponseArgs : ResponseArgs
//		{
//			return this.SendSystemRequest<TRequestArgs, TResponseArgs, TResult>(requestId, getRequestArgs, () => Activator.CreateInstance(typeof(TResponseArgs)) as TResponseArgs, getResulValue);
//		}

//		protected RequestResult<TResult> SendSystemRequest<TRequestArgs, TResponseArgs, TResult>(int requestId, Func<TRequestArgs> getRequestArgs, Func<TResponseArgs> getResponseArgs, Func<TResponseArgs, TResult> getResulValue)
//			where TRequestArgs : RequestArgs
//			where TResponseArgs : ResponseArgs
//		{
//			return this.PackageEngine.SendRequest(requestId, getRequestArgs, getResponseArgs, getResulValue, isSystem: true, isBroadcast: false, isMulticast: false);
//		}


//		//public void SendAsync(ArraySegment<byte> segment)
//		//      {
//		//          base.Send(segment);
//		//      }

//		//public void SendAsync(IList<ArraySegment<byte>> segments)
//		//{
//		//    base.Send(segments);
//		//}

//		//[Obsolete("This method is not supported in this class.", true)]
//		//public new void Send(ArraySegment<byte> segment)
//		//{
//		//	throw new NotImplementedException("Don't use this method!");
//		//}

//		//[Obsolete("This method is not supported in this class.", true)]
//		//public new void Send(IList<ArraySegment<byte>> segments)
//		//{
//		//	throw new NotImplementedException("Don't use this method!");
//		//}

//		//private void RaisePackageReceived(PackageInfo package)
//		//{
//		//	if (this.PackageReceived != null)
//		//		this.PackageReceived(null, package);
//		//}

//		//private void RaisePackageSent(IList<ArraySegment<byte>> segments)
//		//{
//		//	if (this.PackageSent != null)
//		//	{
//		//		BufferStream packageStream = new BufferStream();
//		//		packageStream.Initialize(segments);
//		//		PackageInfo package = PackageManager.CreatePackage(packageStream as IBufferStream, packageLengthIncluded: true);

//		//		this.PackageSent(null, package);
//		//	}
//		//}

//		//private void OnSesionPackageJobAction(AppSession session, JobActionType jobActionType, PackageInfo receivedPackage, ArraySegment<byte> sentPackageLength, ArraySegment<byte> sentPackageHeaderAnyBody)
//		//{
//		//	this.RaisePackageJobAction(new AppSession[] { session }, jobActionType, receivedPackage, sentData);
//		//}

//		//private void RaisePackageJobAction(AppSession session, JobActionType jobActionType, PackageInfo receivedPackage, ArraySegment<byte> sentPackageLength, ArraySegment<byte> sentPackageHeaderAnyBody)
//		//{
//		//	if (this.PackageJobAction != null)
//		//		this.PackageJobAction(session, jobActionType, receivedPackage, sentPackageLength, sentPackageHeaderAnyBody);
//		//}


//		//Type IPackageManagable.GetSocketEngineType()
//		//{
//		//	return this.GetType();
//		//}

//		private void AppClient_Connected(object sender, EventArgs e)
//		{
//			this.OnConnected();
//		}

//		void ISimpleAppSession.SendPackageAsync(byte[] data)
//		{
//			this.Send(data);
//		}

//		void ISimpleAppSession.SendPackageAsync(ArraySegment<byte> segment)
//		{
//			this.Send(segment);
//		}

//		void ISimpleAppSession.SendPackageAsync(IList<ArraySegment<byte>> segments)
//		{
//			this.Send(segments);
//		}
//	}
//}
