//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Simple.Serialization;
//using Simple.SocketEngine;

//namespace Simple.Objects.MonitorProtocol
//{
//	public class ResponseRequestSessionPackageInfoMessageArgs : SessionPackageInfoMessageArgs
//	{
//		private PackageWriter? responseWriterInfo { get; set; }

//		public ResponseRequestSessionPackageInfoMessageArgs(PackageArgsFactory packageArgsFactory)
//			: base(packageArgsFactory)
//		{
//		}

//		public ResponseRequestSessionPackageInfoMessageArgs(SimpleSession session, PackageInfo requestReaderInfo, PackageWriter responseWritterInfo)
//			: base (session.SessionKey, packageInfo: requestReaderInfo)
//		{
//			this.responseWriterInfo = responseWritterInfo;
//		}


//		public override void WriteTo(ref SequenceWriter writer, object context)
//		{
//			base.WriteTo(ref writer, context);

//			// Writer has no package length written since package encoder writes it after flags and args is created
//			// Does we need to writer response package length first
//			writer.WriteInt64Optimized(this.responseWriterInfo!.Buffer!.Length);
//			writer.Write(this.responseWriterInfo!.Buffer!.ToArray());
//		}

//		public override void ReadFrom(ref SequenceReader reader, object context)
//		{
//			base.ReadFrom(ref reader, context);
//		}
//	}
//}
