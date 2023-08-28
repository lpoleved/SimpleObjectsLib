using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Security.Cryptography;
using Microsoft.Win32;
using Simple;
using Simple.AppContext;

namespace Simple.Objects.ServerMonitor
{
	public class MonitorAppContext : ClientAppContextBase
	{
		static MonitorAppContext()
		{
			Instance = new MonitorAppContext();
		}

		public MonitorAppContext()
        {
			this.AppName = "Simple.Objects™ Server Monitor";
			this.Copyright = "Copyright © 2023 by Simple.Objects™";
        }

        public static MonitorAppContext Instance { get; private set; }
	}
}
