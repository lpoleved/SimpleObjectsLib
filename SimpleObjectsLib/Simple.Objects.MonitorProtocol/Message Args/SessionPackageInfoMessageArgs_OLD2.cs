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
//	public class SessionPackageInfoMessageArgs : PackageInfoMessageArgs_OLD
//	{
//		public SessionPackageInfoMessageArgs(PackageArgsFactory packageArgsFactory)
//			: base(packageArgsFactory)
//		{
//		}

//		public SessionPackageInfoMessageArgs(long sessionKey, PackageReader packageInfo)
//			: base(packageInfo)
//		{
//			this.SessionKey = sessionKey;
//		}

//		public long SessionKey { get; set; }

//		public override void WriteTo(ref SequenceWriter writer, object context)
//		{
//			writer.WriteInt64Optimized(this.SessionKey); // Write session key first, than the package info
			
//			base.WriteTo(ref writer, context);
//		}

//		public override void ReadFrom(ref SequenceReader reader, object context)
//		{
//			this.SessionKey = reader.ReadInt64Optimized(); // Read session key first, than read the package info

//			base.ReadFrom(ref reader, context);
//		}
//	}
//}
