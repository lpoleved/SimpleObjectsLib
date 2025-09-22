using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Serialization;
using Simple.SocketEngine;

namespace Simple.Objects.MonitorProtocol
{
	[SystemRequestArgs((int)MonitorSystemRequest.GetRelationName)]

	public class RelationKeyRequestArgs : RequestArgs
	{
		public RelationKeyRequestArgs()
		{
		}

		/// <summary>
		/// Create instace for GraphKeyRequestArgs request args. 
		/// </summary>
		/// <param name="graphKey">The graph Key</param>
		public RelationKeyRequestArgs(int relationKey)
		{
			this.RelationKey = relationKey;
		}

		public int RelationKey { get; private set; }

		public override int GetBufferCapacity() => base.GetBufferCapacity() + 6;

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);

			writer.WriteInt32Optimized(this.RelationKey);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);
			
			this.RelationKey = reader.ReadInt32Optimized();
		}
	}
}
