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
    public class RegularHostConfigurator : TcpHostConfigurator
    {
		public RegularHostConfigurator() { }

		public override string WebSocketSchema => "ws";

		public override IEasyClient<TPackageInfo> ConfigureEasyClient<TPackageInfo>(IPipelineFilter<TPackageInfo> pipelineFilter, ChannelOptions options) where TPackageInfo : class
        {
            return new EasyClient<TPackageInfo>(pipelineFilter, options);
        }

		public override ValueTask<Stream> GetClientStream(Socket socket) => new ValueTask<Stream>(new NetworkStream(socket, false));
	}
}