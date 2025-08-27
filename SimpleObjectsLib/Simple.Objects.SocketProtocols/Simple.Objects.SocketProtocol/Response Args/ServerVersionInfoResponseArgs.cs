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
	[SystemResponseArgs((int)SystemRequest.GetServerVersionInfo)]
	public class ServerVersionInfoResponseArgs : ResponseArgs
    {
        public ServerVersionInfoResponseArgs()
        {
        }

        public ServerVersionInfoResponseArgs(Version systemServerVersion, Version appServerVersion) //, ISimpleObjectModel objectModel)
        {
			this.SystemServerVersion = systemServerVersion;
			this.AppServerVersion = appServerVersion;
        }

		public Version? SystemServerVersion { get; private set; }

		public Version? AppServerVersion { get; private set; }


		public override int GetBufferCapacity()
        {
            return base.GetBufferCapacity() + 4;
        }

        public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
        {
			base.WriteTo(ref writer, session);

			this.WriteVersion(this.SystemServerVersion!, ref writer);
			this.WriteVersion(this.AppServerVersion!, ref writer);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
        {
			base.ReadFrom(ref reader, session);

			this.SystemServerVersion = this.ReadVersion(ref reader);
			this.AppServerVersion = this.ReadVersion(ref reader);
        }

		private void WriteVersion(Version version, ref SequenceWriter writer)
		{
			writer.WriteInt32Optimized(this.SystemServerVersion!.Major);
			writer.WriteInt32Optimized(this.SystemServerVersion.Minor);
			
			writer.WriteBoolean(this.AppServerVersion!.Build >= 0);

			if (this.SystemServerVersion.Build >= 0)
				writer.WriteInt32Optimized(this.AppServerVersion.Build);

			if (this.SystemServerVersion.Revision >= 0)
				writer.WriteInt32Optimized(this.AppServerVersion.Revision);
		}

		private Version ReadVersion(ref SequenceReader reader)
		{
			Version version;
			int major = reader.ReadInt32Optimized();
			int minor = reader.ReadInt32Optimized();
			int build = -1;
			int revision = -1;

			if (reader.ReadBoolean())
				build = reader.ReadInt32Optimized();

			if (reader.ReadBoolean())
				revision = reader.ReadInt32Optimized();

			if (revision < 0)
			{
				if (build < 0)
					version = new Version(major, minor);
				else
					version = new Version(major, minor, build);
			}
			else
			{
				version = new Version(major, minor, build, revision);
			}

			return version;
		}
	}
}
