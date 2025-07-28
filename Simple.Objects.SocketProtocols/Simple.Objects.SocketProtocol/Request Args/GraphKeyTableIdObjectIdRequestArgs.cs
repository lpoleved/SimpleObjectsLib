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
	[SystemRequestArgs((int)SystemRequest.GetSimpleObjectGraphElementByGraphKey)]

	public class GraphKeyTableIdObjectIdRequestArgs : ObjectIdTableIdRequestArgs
	{
		public GraphKeyTableIdObjectIdRequestArgs()
		{
		}

		public GraphKeyTableIdObjectIdRequestArgs(int tableId, long objectId, int graphKey)
			: base(tableId, objectId)
		{
			this.GraphKey = graphKey;
		}

		public int GraphKey { get; private set; }

		public override int GetBufferCapacity() => base.GetBufferCapacity() + 1;

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);
			
			writer.WriteInt32Optimized(this.GraphKey);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);
			
			this.GraphKey = reader.ReadInt32Optimized();
		}
	}
}
