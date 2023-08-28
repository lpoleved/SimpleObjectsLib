using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Serialization;
using Simple.SocketEngine;
using Simple.Objects;

namespace Simple.Objects.SocketProtocol
{
	[SystemRequestArgs((int)SystemRequest.GetOneToManyForeignObjectCollection)]
	[SystemRequestArgs((int)SystemRequest.GetOneToOneForeignObject)]

	public class RelationKeyTableIdObjectIdRequestArgs : ObjectIdTableIdRequestArgs
	{
		public RelationKeyTableIdObjectIdRequestArgs()
		{
		}

		public RelationKeyTableIdObjectIdRequestArgs(int tableId, long objectId, int relationKey)
			: base(tableId, objectId)
		{
			this.RealationKey = relationKey;
		}

		public int RealationKey { get; private set; }

		public override int GetBufferCapacity() => base.GetBufferCapacity() + 1;

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);
			
			writer.WriteInt32Optimized(this.RealationKey);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);
			
			this.RealationKey = reader.ReadInt32Optimized();
		}
	}
}
