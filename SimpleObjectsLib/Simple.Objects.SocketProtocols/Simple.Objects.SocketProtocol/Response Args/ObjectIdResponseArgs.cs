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
	[SystemResponseArgs((int)SystemRequest.GetSimpleObjectGraphElementByGraphKey)]

	public class ObjectIdResponseArgs : ResponseArgs
	{
		public ObjectIdResponseArgs()
		{
		}

		public ObjectIdResponseArgs(long objectId)
		{
			this.ObjectId = objectId;
		}

		public long ObjectId { get; private set; }

		public override int GetBufferCapacity() => base.GetBufferCapacity() + 4;

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);
			
			writer.WriteInt64Optimized(this.ObjectId);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);
			
			this.ObjectId = reader.ReadInt64Optimized();
		}
	}
}
