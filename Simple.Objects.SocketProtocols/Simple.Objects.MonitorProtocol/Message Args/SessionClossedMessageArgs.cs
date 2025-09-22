using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Serialization;
using Simple.SocketEngine;
using SuperSocket.Connection;

namespace Simple.Objects.MonitorProtocol
{
	[SystemMessageArgs((int)MonitorSystemMessage.SessionClosed)]

	public class SessionClosedMessageArgs : SessionMessageArgs
	{
		public SessionClosedMessageArgs()
		{
		}

		public SessionClosedMessageArgs(SimpleSession session, CloseReason closeReason)
			: base(session.SessionKey)
		{
			this.CloseReason = closeReason;
		}

		public CloseReason CloseReason { get; set; }

		public override int GetBufferCapacity()
		{
			return base.GetBufferCapacity() + 1;
		}

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);
			
			writer.WriteByte((byte)this.CloseReason);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);
			
			this.CloseReason = (CloseReason)reader.ReadByte();
		}
	}
}
