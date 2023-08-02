using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.SocketEngine;

namespace Simple.Objects.ServerMonitor
{
	public struct PackageHexTextVisualInfo
	{
		public PackageHexTextVisualInfo(PackageInfo packageInfo, MemoEdit textControl)
		{
			this.PackageInfo = packageInfo;
			this.TextControl = textControl;
		}

		public PackageInfo PackageInfo { get; private set; }
		public MemoEdit TextControl { get; private set; }
	}
}
