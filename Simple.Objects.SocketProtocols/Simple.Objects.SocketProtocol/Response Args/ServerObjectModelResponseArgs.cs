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
	[SystemResponseArgs((int)SystemRequest.GetServerObjectModel)]
	public class ServerObjectModelResponseArgs : ResponseArgs
	{
		public ServerObjectModelResponseArgs()
		{
		}

		public ServerObjectModelResponseArgs(ServerObjectModelInfo serverObjectModelInfo)
		{
			this.ServerObjectModelInfo = serverObjectModelInfo;
		}

		public ServerObjectModelInfo? ServerObjectModelInfo { get; private set; } = null;

		public override int GetBufferCapacity()
		{
			return this.ServerObjectModelInfo?.ClientSerializablePropertyIndexes.Length + this.ServerObjectModelInfo?.ServerSerializablePropertyIndexes.Length + this.ServerObjectModelInfo?.StorablePropertyIndexes.Length ?? 20 * 40 + 5;
		}

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);

			this.ServerObjectModelInfo!.WriteTo(ref writer, session);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);

			this.ServerObjectModelInfo = new ServerObjectModelInfo(ref reader, session);
		}
	}
}