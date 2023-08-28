//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Buffers;
//using SuperSocket.ProtoBase;
//using Simple.Serialization;

//namespace Simple.SocketEngine
//{
//	public class MonitorPackageEncoding_OLD : IPackageDecoder<PackageReader>, IPackageEncoder<PackageWriter>
//	{
//        private static readonly PackageEncoding Default = new PackageEncoding();

//		public static readonly IPackageDecoder<PackageReader>       Decoder = Default;
//        public static readonly IPackageEncoder<PackageWriter> Encoder = Default;

//        public PackageReader Decode(ref ReadOnlySequence<byte> buffer, object context)
//        {
//            if (context is ISimpleSession session)
//            {
//				SerializationReader reader = new SerializationReader(ref buffer, session.CharacterEncoding);

//                return new PackageReader(reader);
//            }
//            else
//            {
//				throw new ArgumentException("Package decode: Context object is not ISimpleSession type object.");
//				//return new PackageInfo(0, new PackageFlags(0), 0, 0, new SerializationReader(buffer));
//            }
//        }

//        public int Encode(IBufferWriter<byte> writer, PackageWriter package)
//        {
//            var packageLengthData = PackageEngine.Create7BitEncodedInt64Span(package.SerializationWriter.BytesWritten);

//            writer.Write(packageLengthData);
//            package.SerializationWriter.WriteDataTo(writer);

//            int bytesWriten = packageLengthData.Length + (int)package.SerializationWriter.BytesWritten;

//            return bytesWriten;
//        }
//    }
//}
