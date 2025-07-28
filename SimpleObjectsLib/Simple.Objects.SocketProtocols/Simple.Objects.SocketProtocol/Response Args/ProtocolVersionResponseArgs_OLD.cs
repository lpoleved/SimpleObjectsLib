//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple.Modeling;
//using Simple.Objects;
//using Simple.Serialization;
//using Simple.SocketEngine;

//namespace Simple.Objects.SocketProtocol
//{
//	[SystemResponseArgs((int)SystemRequest.GetProtocolVersion)]
//	public class ProtocolVersionResponseArgs : ResponseArgs
//    {
//        public ProtocolVersionResponseArgs()
//        {
//        }

//        public ProtocolVersionResponseArgs(Version protocolVersion) //, ISimpleObjectModel objectModel)
//        {
//			this.ProtocolVersion = protocolVersion;
//        }

//        public Version? ProtocolVersion { get; private set; }

//		public override int GetBufferCapacity()
//        {
//            return base.GetBufferCapacity() + 4;
//        }

//        public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
//        {
//			base.WriteTo(ref writer, session);

//			writer.WriteInt32Optimized(this.ProtocolVersion!.Major);
//			writer.WriteInt32Optimized(this.ProtocolVersion.Minor);
//		}

//		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
//        {
//			base.ReadFrom(ref reader, session);

//			int major = reader.ReadInt32Optimized();
//			int minor = reader.ReadInt32Optimized();

//			this.ProtocolVersion = new Version(major, minor);
//        }
//    }
//}
