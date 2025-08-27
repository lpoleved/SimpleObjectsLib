//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Simple.Serialization;
//using Simple.SocketEngine;

//namespace Simple.Objects.MonitorProtocol
//{
//	public class SessionPackageInfoMessageArgs : SessionPackageReader, ISerializable
//	{
//		public SessionPackageInfoMessageArgs()
//		{
//		}

//		public SessionPackageInfoMessageArgs(SimpleSession session, PackageWriter packageInfo)
//			: base(packageInfo)
//		{
//			this.SessionKey = session.SessionKey;
//		}

//        public long SessionKey { get; set; }

//		//public override int GetBufferCapacity() => 4;

//		public override void WriteTo(SerializationWriter writer)
//		{
//			base.WriteTo(writer);

//			writer.WriteInt64Optimized(this.SessionKey);
//		}

//		public override void ReadFrom(SerializationReader reader)
//		{
//			base.ReadFrom(reader);

//			this.SessionKey = reader.ReadInt64Optimized();
//		}
//	}
//}
