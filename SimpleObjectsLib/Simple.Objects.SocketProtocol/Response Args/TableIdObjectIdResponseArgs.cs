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
	[SystemResponseArgs((int)SystemRequest.GetOneToOneForeignObject)]

	public class TableIdObjectIdResponseArgs : ObjectIdResponseArgs
	{
		public TableIdObjectIdResponseArgs()
		{
		}

		public TableIdObjectIdResponseArgs(int tableId, long objectId)
			: base(objectId)
		{
			this.TableId = tableId;
		}

		public int TableId { get; private set; }

		public override int GetBufferCapacity() => base.GetBufferCapacity() + 4;

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);
			
			writer.WriteInt32Optimized(this.TableId);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);
			
			this.TableId = reader.ReadInt32Optimized();
		}
	}
}
