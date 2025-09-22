using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static SuperSocket.Extensions;
using SuperSocket.Connection;
using SuperSocket.ProtoBase;
using SuperSocket.Server;
using SuperSocket.Server.Abstractions;
using SuperSocket.Server.Abstractions.Connections;
using SuperSocket.Server.Abstractions.Session;
using SuperSocket.Server.Abstractions.Middleware;
using SuperSocket.Server.Connection;
using SuperSocket.ProtoBase.ProxyProtocol;

namespace Simple.SocketEngine
{
	/// <summary>
	/// Represents a SimpleSocket service that handles connections and sessions.
	/// </summary>
	/// <typeparam name="PackageReader">The type of the package information received.</typeparam>
	public class SimpleSocketService : SuperSocketService<PackageInfo>, ISuperSocketHostedService
    {
		private PackageArgsFactory packageArgsFactory;

		public SimpleSocketService(IServiceProvider serviceProvider, IOptions<ServerOptions> serverOptions, PackageArgsFactory packageArgsFactory)
			: base(serviceProvider, serverOptions)
		{
			this.packageArgsFactory = packageArgsFactory;
		}

		protected override IPipelineFilterFactory<PackageInfo> GetPipelineFilterFactory()
		{
			//return base.GetPipelineFilterFactory();

			IPipelineFilterFactory<PackageInfo> filterFactory = new PipelineFilterFactory(this.ServiceProvider, this.packageArgsFactory);

			if (Options.EnableProxyProtocol)
				filterFactory = new ProxyProtocolPipelineFilterFactory<PackageInfo>(filterFactory);

			return filterFactory;
		}
	}
}
