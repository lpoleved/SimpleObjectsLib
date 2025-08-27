using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Objects;
using Simple.Collections;
using Simple.Serialization;
using Simple.SocketEngine;

namespace Simple.Objects.SocketProtocol
{
	[SystemResponseArgs((int)SystemRequest.AuthenticateSession)]

	public class AuthenticateSessionResponseArgs : ResponseArgs
    {
        public AuthenticateSessionResponseArgs()
        {
        }

		public AuthenticateSessionResponseArgs(bool isAuthenticated, long userId) //, long userId, string username)
        {
            this.IsAuthenticated = isAuthenticated;
            this.UserId = userId;
            //this.Username = username;
        }

        public bool IsAuthenticated { get; private set; }
        public long UserId { get; private set; }
        //public string Username { get; private set; } = String.Empty;


        public override int GetBufferCapacity()
        {
            return base.GetBufferCapacity() + 1; // + this.Username?.Length ?? 1;
        }

        public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
        {
			base.WriteTo(ref writer, session);
			
            writer.WriteBoolean(this.IsAuthenticated);

            if (this.IsAuthenticated)
            {
                writer.WriteInt64Optimized(this.UserId);
            //    writer.WriteString(this.Username);
            }
        }

        public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
        {
			base.ReadFrom(ref reader, session);
			
            this.IsAuthenticated = reader.ReadBoolean();

            if (this.IsAuthenticated)
            {
                this.UserId = reader.ReadInt64Optimized();
            //    this.Username = reader.ReadString();
            }
        }
    }
}
