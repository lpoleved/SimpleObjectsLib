using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Serialization;
using Simple.SocketEngine;

namespace Simple.Objects.SocketProtocol
{
	[SystemRequestArgs((int)SystemRequest.GetGraphElementsWithObjects)]
	[SystemRequestArgs((int)SystemRequest.GetGraphElementsWithObjectsNew)]
	public class ParentGraphElementIdGraphKeyRequestArgs : GraphKeyRequestArgs
	{
		public ParentGraphElementIdGraphKeyRequestArgs()
		{
		}

		/// <summary>
		/// Create instace for GetGraphElementsWithObject request args. 
		/// </summary>
		/// <param name="graphKey">The graph Key</param>
		/// <param name="parentGraphElementId">Parent GraphElement Id. If you want root GraphElements, set zero for parentGraphElementId.</param>
		public ParentGraphElementIdGraphKeyRequestArgs(int graphKey, long parentGraphElementId)
			: base(graphKey)
		{
			this.ParentGraphElementId = parentGraphElementId;
		}

		public long ParentGraphElementId { get; private set; }

		public override int GetBufferCapacity() => base.GetBufferCapacity() + 6;

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);

			writer.WriteInt64Optimized(this.ParentGraphElementId);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);
			
			this.ParentGraphElementId = reader.ReadInt64Optimized();
		}
	}
}
