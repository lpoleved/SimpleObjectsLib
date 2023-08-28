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
    public class SecureHostConfigurator : TcpHostConfigurator
    {
        public SecureHostConfigurator() { IsSecure = true; }

        public override string WebSocketSchema => "wss";

        public override void Configure(ISuperSocketHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((ctx, services) =>
            {
                services.Configure<ServerOptions>((options) =>
                {
                    var listener = options.Listeners[0];

                    if (listener.Security == SslProtocols.None)
                        listener.Security = GetServerEnabledSslProtocols();
                    
                    listener.CertificateOptions = new CertificateOptions
                    {
                        FilePath = "supersocket.pfx",
                        Password = "supersocket"
                    };
                });
            });

            base.Configure(hostBuilder);
        }
        public override async ValueTask<Stream> GetClientStream(Socket socket)
        {
            var stream = new SslStream(new NetworkStream(socket), false);
            var options = new SslClientAuthenticationOptions();
 
            options.TargetHost = "supersocket";
            options.EnabledSslProtocols = GetClientEnabledSslProtocols();
            options.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

#if NETSTANDARD
            await stream.AuthenticateAsClientAsync(options.ToString());
#else
            await stream.AuthenticateAsClientAsync(options);
#endif

            return stream;
        }

        protected virtual SslProtocols GetServerEnabledSslProtocols()
        {
#if NETSTANDARD
            return SslProtocols.Tls12 | SslProtocols.Tls11;
#else
            return SslProtocols.Tls13 | SslProtocols.Tls12 | SslProtocols.Tls11;
#endif
        }

        protected virtual SslProtocols GetClientEnabledSslProtocols()
        {
#if NETSTANDARD
            return SslProtocols.Tls12 | SslProtocols.Tls11;
#else
            return SslProtocols.Tls13 | SslProtocols.Tls12 | SslProtocols.Tls11;
#endif
        }

        public override IEasyClient<TPackageInfo> ConfigureEasyClient<TPackageInfo>(IPipelineFilter<TPackageInfo> pipelineFilter, ChannelOptions options) where TPackageInfo : class
        {
            var client =  new EasyClient<TPackageInfo>(pipelineFilter, options);
            client.Security = new SecurityOptions
            {
                TargetHost = "supersocket",
                EnabledSslProtocols = GetClientEnabledSslProtocols(),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            return client;
        }
    }
}