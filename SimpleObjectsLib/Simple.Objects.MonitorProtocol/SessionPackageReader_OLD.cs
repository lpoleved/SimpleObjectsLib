using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Serialization;
using Simple.SocketEngine;

namespace Simple.Objects.MonitorProtocol
{
	public class SessionPackageReader_OLD
	{
		public SessionPackageReader_OLD(PackageInfo packageInfo)
		{
			this.PackageInfo = packageInfo;
		}

		public SessionPackageReader_OLD(long sessionKey, PackageInfo packageInfo)
		{
			this.SessionKey = sessionKey;
			this.PackageInfo = packageInfo;
		}

		public long SessionKey { get; private set; }
		public PackageInfo PackageInfo { get; private set; }

		//public void WriteDataTo(IBufferWriter<byte> writer)
		//{
		//	this.PackageInfoSerializationReader.WriteDataTo(writer);

		//	var packageLengthData = PackageEngine.Create7BitEncodedInt64Span(this.SessionKey);

		//	writer.Write(packageLengthData);
		//}

		//public void ReadData() => this.SessionKey = this.PackageInfo.SerializationReader.ReadInt64Optimized();
	}
}
