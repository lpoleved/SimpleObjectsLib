using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using SuperSocket.Connection;
using SuperSocket.Server;
using SuperSocket.Server.Abstractions;
using SuperSocket.Server.Abstractions.Session;
using SuperSocket;
using System.Threading;

namespace Simple.SocketEngine
{
    public class SimplePackageHandlingScheduler : PackageHandlingSchedulerBase<PackageInfo>, IPackageHandlingScheduler<PackageInfo>
    {
		public SimplePackageHandlingScheduler()	{ }

        public override async ValueTask HandlePackage(IAppSession session, PackageInfo package, CancellationToken cancellationToken) => await base.HandlePackageInternal(session, package, cancellationToken);
    }
}
