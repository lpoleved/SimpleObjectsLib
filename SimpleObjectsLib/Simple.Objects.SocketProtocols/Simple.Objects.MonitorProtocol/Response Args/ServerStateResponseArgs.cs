using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.Server.Abstractions;
using Simple.Modeling;
using Simple.Objects;
using Simple.Serialization;
using Simple.SocketEngine;

namespace Simple.Objects.MonitorProtocol
{

	[SystemResponseArgs((int)MonitorSystemRequest.GetServerState)]
	public class ServerStateResponseArgs : ResponseArgs
    {
        public ServerStateResponseArgs()
        {
        }

        public ServerStateResponseArgs(ServerState serverState) 
        {
			this.ServerState = serverState;
        }

        public ServerState ServerState { get; private set; }

		public override int GetBufferCapacity()
        {
            return base.GetBufferCapacity() + 4;
        }

        public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
        {
            base.WriteTo(ref writer, session);
            
            writer.WriteInt32Optimized((int)this.ServerState);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
        {
            base.ReadFrom(ref reader, session);

            this.ServerState = (ServerState)reader.ReadInt32Optimized();
        }
    }
}
