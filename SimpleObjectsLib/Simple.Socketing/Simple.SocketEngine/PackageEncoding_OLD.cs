//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Buffers;
//using SuperSocket.ProtoBase;
//using Simple.Serialization;
//using System.Diagnostics.Eventing.Reader;

//namespace Simple.SocketEngine
//{
//	public class PackageEncoding : IPackageDecoder<PackageInfo>, IPackageEncoder<PackageWriter>
//	{
//        //      private static readonly PackageEncoding Default = new PackageEncoding();

//        //public static readonly IPackageDecoder<PackageReader> Decoder = Default;
//        //      public static readonly IPackageEncoder<PackageWriter> Encoder = Default;

//        public PackageEncoding(CommandDiscovery commandDiscovery)
//        {
//            this.CommandDiscovery = commandDiscovery;
//        }

//        public CommandDiscovery CommandDiscovery { get; private set; }

//		public PackageInfo Decode(ref ReadOnlySequence<byte> buffer, object context)
//        {
//            if (context is ISimpleSession session)
//            {
//                //SequenceReader<byte> sequenceReader = new SequenceReader<byte>(buffer);
//                SerializationReader reader; // = new SerializationReader(sequenceReader); // = new SerializationReader(ref buffer, session.CharacterEncoding);
//                PackageReader packageReader;
//                PackageInfo result;

//                //if (buffer.IsSingleSegment)
//                    // ((reader = new SerializationReader(buffer.ToArray());
//				//else
				  
//                var arraySegmentList = buffer.ToArraySegmentList();

//				reader = new SerializationReader(arraySegmentList);



//				//            var arraySegmentList = buffer.ToArraySegmentList();
//				//            var first = arraySegmentList[0];

//				//if (first.Offset > 0)
//				//            {
//				//	var packageLengthData = new ArraySegment<byte>(first.Array, 0, first.Offset);

//				//	arraySegmentList.Insert(0, packageLengthData); // Put it at the top
//				//}

//				//            reader = new SerializationReader(arraySegmentList, session.CharacterEncoding);
//				packageReader = new PackageReader(reader);

//				packageReader.ReadPackageLength();
//				packageReader.ReadHeader();

//                result = new PackageInfo();

//                return result;
//            }
//            else
//            {
//				throw new ArgumentException("Package decode: Context object is not ISimpleSession type object.");
//				//return new PackageInfo(0, new PackageFlags(0), 0, 0, new SerializationReader(buffer));
//            }
//        }

//        public int Encode(IBufferWriter<byte> writer, PackageWriter package)
//        {
//            package.WriteHeader();
//            package.WritePackageArgs();
            
//            var packageLengthData = PackageEngine.Create7BitEncodedInt64Span(package.SerializationWriter!.BytesWritten);

//            writer.Write(packageLengthData);
//            package.SerializationWriter.WriteDataTo(writer);

//            int bytesWriten = packageLengthData.Length + (int)package.SerializationWriter.BytesWritten;

//            return bytesWriten;
//        }
//    }
//}
