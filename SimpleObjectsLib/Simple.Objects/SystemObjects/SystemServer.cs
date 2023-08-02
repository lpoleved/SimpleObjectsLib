using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects
{
	public sealed class SystemServer : SystemObject<long, SystemServer>
    {
		static SystemServer()
		{
			Model.TableInfo = SystemTables.SystemServers;
			Model.AutoGenerateKey = false;
		}

		public SystemServer()
		{
		}

		public SystemServer(SimpleObjectManager objectManager, ref SystemObjectCollectionByObjectKey<long, SystemServer> dictionaryCollection, long serverId, string description, string location)
			: base(objectManager, ref dictionaryCollection, 
				  (item) =>
				  {
					  item.ServerId = serverId;
					  item.Description = description;
					  item.Location = location;
				  })
		{
		}

		[ObjectKey]
		public long ServerId { get; private set; }

		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Location { get; set; }
    }
}
