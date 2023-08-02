using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Serialization;
using Simple.SocketEngine;

namespace Simple.Objects.MonitorProtocol
{
	public abstract class SessionKeyUserIdMessageArgs : SessionMessageArgs
	{
		public SessionKeyUserIdMessageArgs()
		{
		}

		public SessionKeyUserIdMessageArgs(long sessionKey, long userId)
			: base(sessionKey)
		{
			this.UserId = userId;
		}

		public long UserId { get; set; }

		public override int GetBufferCapacity()
		{
			return base.GetBufferCapacity() + 4;
		}

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);
			
			writer.WriteInt64Optimized(this.UserId);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);
			
			this.UserId = reader.ReadInt64Optimized();
		}
	}
}
