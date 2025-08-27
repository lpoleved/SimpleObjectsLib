using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Serialization;

namespace Simple.SocketEngine
{
    public abstract class RequestArgs : PackageArgs
	{
        public override void WriteTo(ref SequenceWriter writer, ISimpleSession session) { }
        public override void ReadFrom(ref SequenceReader reader, ISimpleSession session) { }
    }
}
