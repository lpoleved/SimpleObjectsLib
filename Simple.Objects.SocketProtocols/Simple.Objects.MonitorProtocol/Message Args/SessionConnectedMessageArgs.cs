using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using SuperSocket;
using SuperSocket.Server;
using Simple.Serialization;
using Simple.SocketEngine;

namespace Simple.Objects.MonitorProtocol
{
	[SystemMessageArgs((int)MonitorSystemMessage.SessionConnected)]

	public class SessionConnectedMessageArgs : SessionMessageArgs
    {
		public SessionConnectedMessageArgs()
		{
		}

		public SessionConnectedMessageArgs(SimpleSession session)
			: base(session.SessionKey)
		{
			this.SessionKey = session.SessionKey;

			if (session.LocalEndPoint is IPEndPoint localPEndPoint)
			{
				this.ServerAddress = localPEndPoint.Address.ToString();
				this.ServerPort = localPEndPoint.Port;
			}

			if (session.RemoteEndPoint is IPEndPoint remoteEndPoint)
			{
				this.ClientAddress = remoteEndPoint.Address.ToString();
				this.ClientPort = remoteEndPoint.Port;
			}

			this.CharacterEncoding = session.CharacterEncoding;
		}

		public string? ServerAddress { get; set; }
		public int ServerPort { get; set; }

		public string? ClientAddress { get; set; }
		public int ClientPort { get; set; }

		public Encoding? CharacterEncoding { get; set; }

		public override int GetBufferCapacity()
		{
			return base.GetBufferCapacity() + this.ClientAddress?.Length ?? 1 + 6;
		}

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);

			writer.WriteInt64Optimized(this.SessionKey);
			writer.WriteString(this.ServerAddress);
			writer.WriteInt32Optimized(this.ServerPort);
			writer.WriteString(this.ClientAddress);
			writer.WriteInt32(this.ClientPort);
			writer.WriteInt32(this.CharacterEncoding.CodePage);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);

			this.SessionKey = reader.ReadInt64Optimized();
			this.ServerAddress = reader.ReadString();
			this.ServerPort = reader.ReadInt32Optimized();
			this.ClientAddress = reader.ReadString();
			this.ClientPort = reader.ReadInt32();
			this.CharacterEncoding = Encoding.GetEncoding(reader.ReadInt32()); 
		}
	}
}
