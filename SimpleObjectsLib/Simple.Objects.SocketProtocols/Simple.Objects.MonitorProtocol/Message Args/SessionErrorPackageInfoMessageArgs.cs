using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Collections;
using Simple.SocketEngine;
using Simple.Serialization;
using System.Buffers;

namespace Simple.Objects.MonitorProtocol
{
	public class SessionErrorPackageInfoMessageArgs : SessionMessageArgs
	{
		public SessionErrorPackageInfoMessageArgs()
		{

		}

		public SessionErrorPackageInfoMessageArgs(long sessionKey, string message, string description, byte[] packageBuffer)
			: base(sessionKey)
		{
			this.Message = message;
			this.Description = description;
			this.PackageBuffer = packageBuffer;
		}

		public string Message { get; private set; } = String.Empty;
		public string Description { get; private set; } = String.Empty;
		public byte[]? PackageBuffer { get; private set; }

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);

			writer.WriteString(this.Message);
			writer.WriteString(this.Description);
			writer.WriteInt32Optimized(this.PackageBuffer!.Length);
			writer.WriteBinary(this.PackageBuffer);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);

			this.Message = reader.ReadString();
			this.Description = reader.ReadString();

			var bufferLength = reader.ReadInt32Optimized();
			
			this.PackageBuffer = reader.ReadBinary(bufferLength).ToArray();
		}
	}
}
