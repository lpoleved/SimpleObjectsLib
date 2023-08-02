using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple;
using Simple.Modeling;
using Simple.Serialization;
using Simple.SocketEngine;

namespace Simple.Objects.SocketProtocol
{
	[SystemResponseArgs((int)SystemRequest.GetAllServerObjectModels)]
	public class AllServerObjectModelsResponseArgs : ResponseArgs
	{
		private byte[]? buffer = null;

		public AllServerObjectModelsResponseArgs()
		{
		}

		public AllServerObjectModelsResponseArgs(ServerObjectModelInfo[] serverObjectModels)
		{
			this.ServerObjectModels = serverObjectModels;

			SequenceWriter writer = new SequenceWriter(UTF8Encoding.Default);

			this.WriteToBuffer(ref writer);
		}

		public ServerObjectModelInfo[]? ServerObjectModels { get; private set; } = null;

		
		private void WriteToBuffer(ref SequenceWriter writer)
		{
			writer.WriteInt32Optimized(this.ServerObjectModels!.Length);

			foreach (var model in this.ServerObjectModels)
				model.WriteTo(ref writer);
			
			this.buffer = writer.ToArray();
		}

		public override int GetBufferCapacity()
		{
			return this.ServerObjectModels!.Length * 40 + 4;
		}

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);

			writer.WriteBinary(this.buffer!);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);

			this.ServerObjectModels = new ServerObjectModelInfo[reader.ReadInt32Optimized()];

			for (int i = 0; i < this.ServerObjectModels.Length; i++)
				this.ServerObjectModels[i] = new ServerObjectModelInfo(ref reader); 
		}
	}
}