using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Serialization;

namespace Simple.SocketEngine
{
    public abstract class ResponseArgs : PackageArgs
	{
        public bool ResponseSucceeded => this.Status == PackageStatus.OK;

  //      public override void WriteTo(ref SequenceWriter writer, ISimpleSession session) { }
		//public override void ReadFrom(ref SequenceReader reader, ISimpleSession session) { }
    }
}
