using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperSocket;
using SuperSocket.Channel;
using SuperSocket.Client;
using SuperSocket.ProtoBase;

namespace Simple.SocketEngine
{
    public abstract class TcpHostConfigurator : IHostConfigurator
    {
		public TcpHostConfigurator() { }
        
        public abstract string WebSocketSchema { get; }
        public bool IsSecure { get; protected set; }
        public ListenOptions Listener { get; private set; }

        public int ServerPort { get; private set; }

        public virtual void Configure(ISuperSocketHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((ctx, services) =>
            {
                services.Configure<ServerOptions>((options) =>
                {
                    var listener = options.Listeners[0];
                    
                    this.Listener = listener;

                });
            });
        }
        
        public Socket CreateClient()
        {
            var serverAddress = this.GetServerEndPoint();
            var socket = new Socket(serverAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            socket.Connect(serverAddress);
            
            return socket;
        }

        public TextReader GetStreamReader(Stream stream, Encoding encoding) => new StreamReader(stream, encoding, true);
        
        public ValueTask KeepSequence() => new ValueTask();

        public virtual IEasyClient<TPackageInfo> ConfigureEasyClient<TPackageInfo>(IPipelineFilter<TPackageInfo> pipelineFilter, ChannelOptions options)
            where TPackageInfo : class
		{
            return new EasyClient<TPackageInfo>(pipelineFilter, options);
        }

        public abstract ValueTask<Stream> GetClientStream(Socket socket);

        protected virtual IPEndPoint GetServerEndPoint() => new IPEndPoint(IPAddress.Loopback, this.Listener.Port);
    }
}