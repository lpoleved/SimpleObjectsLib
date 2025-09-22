using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.SocketEngine;

namespace Simple.Objects.SocketProtocol
{
	public class SessionPackageInfo
	{
		public SessionPackageInfo(ISimpleSession session, PackageInfo packageInfo)
		{
			this.Session = session;
			this.PackageInfo = packageInfo;
		}

		public ISimpleSession Session { get; private set; }
		public PackageInfo PackageInfo { get; private set; }
	}
}
