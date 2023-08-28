using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.AppContext;

namespace Simple.Datastore.Backup
{
	public class AppContext : ClientAppContextBase
	{
		static AppContext()
		{
			Instance = new AppContext();
		}

		public AppContext()
		{
			this.AppName = "Simple.Objects™ Backup Database";
			this.Copyright = "Copyright © 2023 Simple.Objects™";
		}

		public static AppContext Instance { get; private set; }
	}
}
