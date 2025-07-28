using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.AppContext;

namespace Simple.Datastore.Restore
{
	public class AppContext : ClientAppContextBase
	{
		static AppContext()
		{
			Instance = new AppContext();
		}

		public AppContext()
		{
			this.AppName = "Backup Database";
			this.Copyright = "Copyright © 2025 Simple.Objects";
		}

		public static AppContext Instance { get; private set; }
	}
}
