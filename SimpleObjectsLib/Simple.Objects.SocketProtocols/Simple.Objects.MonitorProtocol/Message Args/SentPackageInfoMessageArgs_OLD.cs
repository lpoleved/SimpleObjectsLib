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
//	public class SentPackageInfoMessageArgs_OLD : MessageArgs, ISequenceSerializable
//	{
//		private PackageWriter? sentPackageInfo { get; set; }

//		public SentPackageInfoMessageArgs_OLD()
//		{
//		}

//		public SentPackageInfoMessageArgs_OLD(PackageWriter sentPackageInfo)
//		{
//			this.sentPackageInfo = sentPackageInfo;
//		}

//		public PackageInfo? PackageInfo { get; set; }


//		public override void WriteTo(ref SequenceWriter writer, object context)
//		{
//			base.WriteTo(ref writer, context);

//			if (this.sentPackageInfo != null && this.sentPackageInfo!.Buffer != null)
//				writer.Write(this.sentPackageInfo!.Buffer);
//		}

//		public override void ReadFrom(ref SequenceReader reader, object context)
//		{
//			base.ReadFrom(ref reader, context);
			
//			this.PackageInfo = new PackageReader();
//		}
//	}
//}
