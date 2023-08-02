using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket;

namespace Simple.SocketEngine
{
	public class SimpleSessionFactory<TSession> : ISessionFactory
		where TSession : IAppSession
	{
		public Type SessionType => typeof(TSession);

		public IServiceProvider ServiceProvider { get; private set; }

		public SimpleSessionFactory(IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider;
		}

		public IAppSession Create()
		{
			return ActivatorUtilities.CreateInstance<TSession>(ServiceProvider);
		}
	}
}
