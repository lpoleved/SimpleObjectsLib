using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Serialization;
using Simple.SocketEngine;

namespace Simple.Objects.MonitorProtocol
{
	public class SessionMessageArgs : MessageArgs
	{
		public SessionMessageArgs()
		{
		}

		public SessionMessageArgs(long sessionKey)
		{
			this.SessionKey = sessionKey;
		}

		public long SessionKey { get; set; }

		public override int GetBufferCapacity() => 4;

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);
			
			writer.WriteInt64Optimized(this.SessionKey);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);
			
			this.SessionKey = reader.ReadInt64Optimized();
		}
	}
}
