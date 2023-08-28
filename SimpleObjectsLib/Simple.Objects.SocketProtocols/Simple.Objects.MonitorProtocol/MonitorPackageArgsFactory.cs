using Simple.Objects.SocketProtocol;
using Simple.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects.MonitorProtocol
{
	public class MonitorPackageArgsFactory : PackageArgsFactory
	{

		public MonitorPackageArgsFactory()
			: this(new Assembly[0])
		{
		}

		public MonitorPackageArgsFactory(IEnumerable<Assembly> argsAssemblies)
			: base(argsAssemblies)
		{
			// Here put your custom package args policy not specified in current assembly but axists in Simple.Objects.SocketProtocol

			this.SystemRequestArgsByRequestId.SetValue((int)MonitorSystemRequest.GetServerObjectModel, typeof(TableIdRequestArgs));
			this.SystemResponseArgsByRequestId.SetValue((int)MonitorSystemRequest.GetServerObjectModel, typeof(ServerObjectModelResponseArgs));

			this.SystemRequestArgsByRequestId.SetValue((int)MonitorSystemRequest.AuthenticateSession, typeof(AuthenticateSessionRequestArgs));
			this.SystemResponseArgsByRequestId.SetValue((int)MonitorSystemRequest.AuthenticateSession, typeof(AuthenticateSessionResponseArgs));

			this.SystemRequestArgsByRequestId.SetValue((int)MonitorSystemRequest.GetServerObjectModel, typeof(TableIdRequestArgs));
			this.SystemResponseArgsByRequestId.SetValue((int)MonitorSystemRequest.GetServerObjectModel, typeof(ServerObjectModelResponseArgs));

			this.SystemRequestArgsByRequestId.SetValue((int)MonitorSystemRequest.GetObjectName, typeof(ObjectIdTableIdRequestArgs));
			//this.SystemResponseArgsByRequestId.SetValue((int)MonitorSystemRequest.GetServerObjectModel, typeof(NameResponseArgs));
		}
	}
}
