using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using SuperSocket.Channel;
using SuperSocket.Server;
using SuperSocket;

namespace Simple.SocketEngine
{
    public class SimplePackageHandlingScheduler : PackageHandlingSchedulerBase<PackageInfo>, IPackageHandlingScheduler<PackageInfo>
    {
		public SimplePackageHandlingScheduler()	{ }

        public override async ValueTask HandlePackage(IAppSession session, PackageInfo package) => await base.HandlePackageInternal(session, package);
    }
}
