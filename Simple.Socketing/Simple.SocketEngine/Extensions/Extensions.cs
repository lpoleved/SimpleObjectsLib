using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Simple.SocketEngine
{
	public static class Extensions
	{
		internal static IPEndPoint GetServerEndPoint(this IHostConfigurator hostConfigurator)
		{
			return new IPEndPoint(IPAddress.Loopback, hostConfigurator.Listener.Port);
		}
	}
}
