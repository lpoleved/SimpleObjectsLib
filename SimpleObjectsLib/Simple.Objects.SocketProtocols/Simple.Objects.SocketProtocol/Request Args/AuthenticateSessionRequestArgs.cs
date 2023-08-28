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
	[SystemRequestArgs((int)SystemRequest.AuthenticateSession)]
	public class AuthenticateSessionRequestArgs : RequestArgs
	{
		public AuthenticateSessionRequestArgs()
		{
		}

		public AuthenticateSessionRequestArgs(string username, string passwordHash)
		{
			this.Username = username;
			this.PasswordHash = passwordHash;
		}

		public string Username { get; private set; } = String.Empty;
		public string PasswordHash { get; private set; } = String.Empty;

		public override int GetBufferCapacity()
		{
			return base.GetBufferCapacity() + this.Username.Length + this.PasswordHash.Length + 4;
		}

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);
			
			writer.WriteString(this.Username);
			writer.WriteString(this.PasswordHash);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);
			
			this.Username = reader.ReadString();
			this.PasswordHash = reader.ReadString();
		}
	}
}
