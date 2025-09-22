using DevExpress.XtraEditors;
using Simple.Objects.MonitorProtocol;
using Simple.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects.ServerMonitor
{
	public struct PackageHexTextVisualInfo
	{
		public PackageHexTextVisualInfo(MonitorPackageReader packageReader, MemoEdit textControl)
		{
			this.PackageReader = packageReader;
			this.TextControl = textControl;
		}

		public MonitorPackageReader PackageReader { get; private set; }
		public MemoEdit TextControl { get; private set; }
	}
}
