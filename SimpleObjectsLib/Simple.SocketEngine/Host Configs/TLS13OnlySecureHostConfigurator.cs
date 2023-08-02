using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
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
    public class TLS13OnlySecureHostConfigurator : SecureHostConfigurator
    {
        protected override SslProtocols GetServerEnabledSslProtocols()
        {
#if NETSTANDARD
            return SslProtocols.Tls12;
#else
            return SslProtocols.Tls13;
#endif
        }

        protected override SslProtocols GetClientEnabledSslProtocols()
        {
#if NETSTANDARD
            return SslProtocols.Tls12;
#else
            return SslProtocols.Tls13;
#endif
        }
    }
}
