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
	[SystemResponseArgs((int)SystemRequest.DoesGraphElementHaveChildren)]
	public class TrueOrFalseResponseArgs : ResponseArgs
    {
        public TrueOrFalseResponseArgs()
        {
        }

		public TrueOrFalseResponseArgs(bool isTrue)
        {
            this.IsTrue = isTrue;
        }

        public bool IsTrue { get; private set; }

        public override int GetBufferCapacity()
        {
            return 1;
        }

        public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
        {
			base.WriteTo(ref writer, session);

			writer.WriteBoolean(this.IsTrue);
        }

        public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
        {
            base.ReadFrom(ref reader, session);
            
            this.IsTrue = reader.ReadBoolean();
        }
    }
}
