//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Threading.Tasks;
//using Simple.Serialization;
//using Simple.SocketEngine;

//namespace Simple.Objects.MonitorProtocol
//{
//	public class SessionSentPackageInfoMessageArgs_OLD : SessionMessageArgs
//	{
//		private PackageWriter? sentPackageInfo { get; set; }

//		public SessionSentPackageInfoMessageArgs_OLD()
//		{
//		}

//		public SessionSentPackageInfoMessageArgs_OLD(long sessionKey, PackageWriter sentPackageInfo)
//			: base(sessionKey)
//		{
//			this.sentPackageInfo = sentPackageInfo;
//		}

//		public PackageInfo? PackageInfo { get; set; }


//		public override void WriteTo(ref SequenceWriter writer, object context)
//		{
//			base.WriteTo(ref writer, context);

//			writer.Write(this.sentPackageInfo!.Buffer!);
//		}

//		public override void ReadFrom(ref SequenceReader reader, object context)
//		{
//			base.ReadFrom(ref reader, context);
			
//			this.PackageInfo = new PackageReader();
//		}
//	}
//}
