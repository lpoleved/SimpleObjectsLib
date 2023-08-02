﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Serialization;
using Simple.SocketEngine;

namespace Simple.Objects.SocketProtocol
{
	public class GraphKeyRequestArgs : RequestArgs
	{
		public GraphKeyRequestArgs()
		{
		}

		/// <summary>
		/// Create instace for GetGraphElementsWithObject request args. 
		/// </summary>
		/// <param name="graphKey">The graph Key</param>
		/// <param name="parentGraphElementId">Parent GraphElement Id. If you want root GraphElements, set zero for parentGraphElementId.</param>
		public GraphKeyRequestArgs(int graphKey)
		{
			this.GraphKey= graphKey;
		}

		public int GraphKey { get; private set; }

		public override int GetBufferCapacity() => base.GetBufferCapacity() + 6;

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);

			writer.WriteInt32Optimized(this.GraphKey);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);
			
			this.GraphKey = reader.ReadInt32Optimized();
		}
	}
}
