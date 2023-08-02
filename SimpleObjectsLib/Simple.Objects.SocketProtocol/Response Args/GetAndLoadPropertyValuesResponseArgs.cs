using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Modeling;
using Simple.Objects;
using Simple.Serialization;
using Simple.SocketEngine;

namespace Simple.Objects.SocketProtocol
{
	//[SystemResponseArgs((int)SystemRequest.GetAndLoadPorpertyValues)]

	public class GetAndLoadPropertyValuesResponseArgs : ResponseArgs
	{
        public GetAndLoadPropertyValuesResponseArgs()
        {
            
        }

        public GetAndLoadPropertyValuesResponseArgs(SimpleObject simpleObject, ServerObjectModelInfo serverObjectPropertyInfo)
		{
			this.SimpleObject = simpleObject;
			this.ServerObjectPropertyInfo = serverObjectPropertyInfo;
        }

		public SimpleObject? SimpleObject { get; private set; }
		public ServerObjectModelInfo? ServerObjectPropertyInfo { get; private set; }


		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
        {
			base.WriteTo(ref writer, session);
			
			if (this.ServerObjectPropertyInfo != null)
				this.SimpleObject?.WriteTo(ref writer, this.ServerObjectPropertyInfo.SerializablePropertyIndexes, this.ServerObjectPropertyInfo);
        }

        public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
        {
			base.ReadFrom(ref reader, session);

			if (this.ServerObjectPropertyInfo != null)
				this.SimpleObject?.LoadFrom(ref reader, this.ServerObjectPropertyInfo);
		}
	}
}
