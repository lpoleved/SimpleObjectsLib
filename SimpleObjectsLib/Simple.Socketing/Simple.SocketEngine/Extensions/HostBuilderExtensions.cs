using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SuperSocket;
using SuperSocket.ProtoBase;
using SuperSocket.Server;
using SuperSocket.Channel;

namespace Simple.SocketEngine
{
	public static class HostBuilderExtensions
	{
		public static ISuperSocketHostBuilder<TReceivePackage> UsePipelineFilterFactory<TReceivePackage>(this ISuperSocketHostBuilder<TReceivePackage> hostBuilder, Func<object, IPipelineFilter<TReceivePackage>> filterFactory)
		{
			hostBuilder.ConfigureServices(
				(hostCtx, services) =>
				{
					services.AddSingleton<Func<object, IPipelineFilter<TReceivePackage>>>(filterFactory);
				}
			);

			return hostBuilder.UsePipelineFilterFactory<DelegatePipelineFilterFactory<TReceivePackage>>();
		}

		public static ISuperSocketHostBuilder<TReceivePackage> UseSessionFactory<TReceivePackage, TSession>(this ISuperSocketHostBuilder<TReceivePackage> hostBuilder, Func<object, TSession> sessionFactory)
			where TSession : IAppSession
		{
			hostBuilder.ConfigureServices(
				(hostCtx, services) =>
				{
					services.AddSingleton<Func<object, TSession>>(sessionFactory);
				}
			);

			return hostBuilder.UseSessionFactory<DelegateSessionFactory<TSession>>();
		}
	}

	public class DelegateSessionFactory<TSession> : ISessionFactory //GenericSessionFactory<TSession>
		where TSession : IAppSession
	{
		private readonly Func<object, TSession> factory;

		public DelegateSessionFactory(IServiceProvider serviceProvider, Func<object, TSession> factory)
		{
			this.ServiceProvider = serviceProvider;
			this.factory = factory;
		}

		public Type SessionType => typeof(TSession);

		public IServiceProvider ServiceProvider { get; private set; }

		public IAppSession Create()
		{
			return this.factory("null"); // ActivatorUtilities.CreateInstance<TSession>(ServiceProvider);
		}
	}
}
