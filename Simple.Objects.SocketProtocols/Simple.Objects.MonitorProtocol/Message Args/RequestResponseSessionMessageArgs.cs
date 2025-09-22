using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Serialization;
using Simple.SocketEngine;

namespace Simple.Objects.MonitorProtocol
{
	[SystemMessageArgs((int)MonitorSystemMessage.RequestReceived)]

	public class RequestResponseSessionMessageArgs : SessionMessageArgs
	{
        public RequestResponseSessionMessageArgs()
        {
        }

        public RequestResponseSessionMessageArgs(long sessionKey, byte[] requestBuffer, byte[] responseBuffer)
            : base(sessionKey)
        {
            this.RequestBuffer = requestBuffer;
            this.ResponseBuffer = responseBuffer;
        }

        public byte[]? RequestBuffer { get; set; }
        public byte[]? ResponseBuffer { get; set; }

		public override int GetBufferCapacity()
		{
			return base.GetBufferCapacity() + this.RequestBuffer!.Length + this.ResponseBuffer!.Length + 8;
		}

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);

            writer.WriteInt32Optimized(this.RequestBuffer!.Length);
            writer.WriteBinary(this.RequestBuffer!);
            writer.WriteInt32Optimized(this.ResponseBuffer!.Length);
            writer.WriteBinary(this.ResponseBuffer!);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);

            int requestLength = reader.ReadInt32Optimized();
            
            this.RequestBuffer = reader.ReadBinary(requestLength).ToArray();

            int responseLength = reader.ReadInt32Optimized();

            this.ResponseBuffer = reader.ReadBinary(responseLength).ToArray();
		}
	}
}
