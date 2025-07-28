using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Serialization;
using Simple.SocketEngine;

namespace Simple.Objects.MonitorProtocol
{
	[SystemMessageArgs((int)MonitorSystemMessage.SessionAuthenticated)]

	public class SessionAuthenticatedMessageArgs : SessionKeyUserIdMessageArgs
	{
		public SessionAuthenticatedMessageArgs()
		{
		}

		public SessionAuthenticatedMessageArgs(SimpleSession session)
			: base(session.SessionKey, session.UserId)
		{
			this.Username = session.Username;
		}

		public string Username { get; set; } = String.Empty;

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);

			writer.WriteString(this.Username);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);

			this.Username = reader.ReadString();
		}
	}
}
