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
	[SystemMessageArgs((int)MonitorSystemMessage.BrodcastMessageSent)]

	public class BrodcastMessageMessageArgs : MessageArgs
	{
        public BrodcastMessageMessageArgs()
        {
        }

        public BrodcastMessageMessageArgs(byte[] messageBuffer)
        {
            this.MessageBuffer = messageBuffer;
        }

        public byte[]? MessageBuffer { get; set; }

		public override int GetBufferCapacity()
		{
			return base.GetBufferCapacity() + this.MessageBuffer!.Length + 4;
		}

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);

            writer.WriteInt32Optimized(this.MessageBuffer!.Length);
            writer.WriteBinary(this.MessageBuffer!);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);

            int messageLength = reader.ReadInt32Optimized();
            
            this.MessageBuffer = reader.ReadBinary(messageLength).ToArray();
		}
	}
}
