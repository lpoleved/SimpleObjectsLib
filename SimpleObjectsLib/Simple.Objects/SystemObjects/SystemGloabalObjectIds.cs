using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Collections;
using Simple.Serialization;
using Simple.Modeling;

namespace Simple.Objects
{
	public sealed class SystemGlobalObjectId : SystemObject<long, SystemGlobalObjectId>
    {
		static SystemGlobalObjectId()
		{
			Model.TableInfo = SystemTablesBase.SystemGlobalObjectIds;
			Model.AutoGenerateKey = true;
		}

		public SystemGlobalObjectId()
		{
		}

		public SystemGlobalObjectId(SimpleObjectManager objectManager, ref SystemObjectCollectionByObjectKey<long, SystemGlobalObjectId> dictionaryCollection, 
									   int serverId, int tableId, int serverObjectId, long localObjectId, DateTime creationTime)
			: base(objectManager, ref dictionaryCollection, 
				  (item) =>
				  {
					  item.ServerId = serverId;
					  item.TableId = tableId;
					  item.ServerObjectId = serverObjectId;
					  item.LocalObjectId = localObjectId;
					  item.CreationTime = creationTime;
				  })
		{
		}

		[ObjectKey]
		public long Id { get; private set; }

		/// <summary>
		/// The ServerId of server where the object is created.
		/// </summary>
		public int ServerId { get; private set; }
		public long TableId { get; private set; }
		public int ServerObjectId { get; private set; }
		public long LocalObjectId { get; private set; }
		
		/// <summary>
		/// The time when the object is created on the server.
		/// </summary>
		public DateTime CreationTime { get; private set; }
	}
}
