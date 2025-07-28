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

		public MonitorPackageArgsFactory(Assembly assembly)
			: this(new List<Assembly>() { assembly })
		{
		}

		public MonitorPackageArgsFactory(List<Assembly> assemblies)
			: base(assemblies)
		{
		}

		protected override void RecalculateAdditionalDefinition()
		{
			base.RecalculateAdditionalDefinition();

			// *** Here put your custom package args policy not specified in current assembly but axists in Simple.Objects.SocketProtocol ***

			this.SystemResponseArgsByRequestId.SetValue((int)MonitorSystemRequest.GetServerVersionInfo, typeof(ServerVersionInfoResponseArgs));

			this.SystemRequestArgsByRequestId.SetValue((int)MonitorSystemRequest.AuthenticateSession, typeof(AuthenticateSessionRequestArgs));
			this.SystemResponseArgsByRequestId.SetValue((int)MonitorSystemRequest.AuthenticateSession, typeof(AuthenticateSessionResponseArgs));

			this.SystemRequestArgsByRequestId.SetValue((int)MonitorSystemRequest.GetServerObjectModel, typeof(TableIdRequestArgs));
			this.SystemResponseArgsByRequestId.SetValue((int)MonitorSystemRequest.GetServerObjectModel, typeof(ServerObjectModelResponseArgs));

			this.SystemRequestArgsByRequestId.SetValue((int)MonitorSystemRequest.GetObjectName, typeof(ObjectIdTableIdRequestArgs));
		}
	}
}
