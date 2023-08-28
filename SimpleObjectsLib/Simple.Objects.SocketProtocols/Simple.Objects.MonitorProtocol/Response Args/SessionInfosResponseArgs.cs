using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Simple.Serialization;
using Simple.SocketEngine;
using SuperSocket;
using SuperSocket.ProtoBase;

namespace Simple.Objects.MonitorProtocol
{
	[SystemResponseArgs((int)MonitorSystemRequest.GetSessionInfos)]
	public class SessionInfosResponseArgs : ResponseArgs
	{
		public SessionInfosResponseArgs()
		{
		}

		public SessionInfosResponseArgs(IEnumerable<IAppSession> sessions)
		{
			this.SessionInfos = new SessionInfo[sessions.Count()];

			for (int i = 0; i < sessions.Count(); i++) 
			{
				SimpleSession? session = sessions.ElementAt(i) as SimpleSession;

				if (session != null)
				{
					string serverEndPoint = (session.LocalEndPoint is IPEndPoint server) ? server.Address.ToString() : session.LocalEndPoint.ToString();
					int serverPort = (session.LocalEndPoint is IPEndPoint server2) ? server2.Port : ServerBase.DefaultPort;
					
					string clientEndPoint = (session.RemoteEndPoint is IPEndPoint client) ? client.Address.ToString() : session.LocalEndPoint.ToString();
					int clientPort = (session.RemoteEndPoint is IPEndPoint client2) ? client2.Port : 1024;

					this.SessionInfos[i] = new SessionInfo(session.SessionKey, serverEndPoint, serverPort, clientEndPoint, clientPort, session.CharacterEncoding, session.UserId.ToString());
				}
			}
		}

		public SessionInfo[]? SessionInfos { get; set; }

		public override int GetBufferCapacity()
		{
			return base.GetBufferCapacity() + this.SessionInfos?.Length ?? 0 * 20;
		}

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);

			writer.WriteInt32Optimized(this.SessionInfos!.Count());

			for (int i = 0; i < this.SessionInfos!.Count(); i++)
			{
				SessionInfo sessionInfo = this.SessionInfos![i];

				writer.WriteInt64Optimized(sessionInfo.SessionKey);
				writer.WriteString(sessionInfo.ServerAddress);
				writer.WriteInt32Optimized(sessionInfo.ServerPort);
				writer.WriteString(sessionInfo.ClientAddress);
				writer.WriteInt32Optimized(sessionInfo.ClientPort);
				writer.WriteInt32(sessionInfo.CharacterEncoding.CodePage);
				writer.WriteString(sessionInfo.Username);
			}
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);
			
			var sessions = new SessionInfo[reader.ReadInt32Optimized()];
			
			for (int i = 0; i < sessions.Length; i++)
				sessions[i] = new SessionInfo(reader.ReadInt64Optimized(), reader.ReadString(), reader.ReadInt32Optimized(), reader.ReadString(), reader.ReadInt32Optimized(), Encoding.GetEncoding(reader.ReadInt32()), reader.ReadString());
			
			this.SessionInfos = sessions;
		}
	}

	public class SessionInfo
	{
		public SessionInfo(long sessionKey, string serverAddress, int serverPort, string clientAddress, int clientPort, Encoding characterEncoding, string username)
		{
			this.SessionKey = sessionKey;
			this.ServerAddress = serverAddress;
			this.ServerPort = serverPort;
			this.ClientAddress = clientAddress;
			this.ClientPort = clientPort;
			this.CharacterEncoding = characterEncoding;
			this.Username = username;
		}

		public long SessionKey { get;  private set; }
		public string ServerAddress { get; private set; }
		public int ServerPort { get; private set; }
		public string ClientAddress { get; private set; }
		public int ClientPort { get; private set; }
		public Encoding CharacterEncoding { get; private set; }
		public string Username { get; private set; }
	}
}
