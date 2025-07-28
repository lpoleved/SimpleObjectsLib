using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects
{
	public sealed class SystemClient : SystemObject<long, SystemClient>
    {
		static SystemClient()
		{
			Model.TableInfo = SystemTablesBase.SystemClients;
			Model.AutoGenerateKey = false;
		}

		public SystemClient()
		{
		}

		public SystemClient(SimpleObjectManager objectManager, ref SystemObjectCollectionByObjectKey<long, SystemClient> dictionaryCollection, long clientId, string name, string description, string location)
			: base(objectManager, ref dictionaryCollection, 
				  (item) =>
				  {
					  item.ClientId = clientId;
					  item.Name = name;
					  item.Description = description;
					  item.Location = location;
				  })
		{
		}

		[ObjectKey]
		public long ClientId { get; private set; }

		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Location { get; set; }
	}
}
