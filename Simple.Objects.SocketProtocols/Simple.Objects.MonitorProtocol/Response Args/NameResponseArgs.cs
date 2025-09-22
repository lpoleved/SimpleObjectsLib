using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket;
using Simple.Modeling;
using Simple.Objects;
using Simple.Serialization;
using Simple.SocketEngine;

namespace Simple.Objects.MonitorProtocol
{

	[SystemResponseArgs((int)MonitorSystemRequest.GetGraphName)]
	[SystemResponseArgs((int)MonitorSystemRequest.GetRelationName)]
	[SystemResponseArgs((int)MonitorSystemRequest.GetObjectName)]
	public class NameResponseArgs : ResponseArgs
    {
        public NameResponseArgs()
        {
        }

        public NameResponseArgs(string name) 
        {
			this.Name = name;
        }

        public string Name { get; private set; } = String.Empty;

		public override int GetBufferCapacity()
        {
            return base.GetBufferCapacity() + this.Name.Length + 2;
        }

        public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
        {
            base.WriteTo(ref writer, session);
            
            writer.WriteString(this.Name);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
        {
            base.ReadFrom(ref reader, session);

            this.Name = reader.ReadString();
        }
    }
}
