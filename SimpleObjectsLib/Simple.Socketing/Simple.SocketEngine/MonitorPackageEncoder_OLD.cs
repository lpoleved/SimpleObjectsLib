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
//	public class MonitorPackageEncoder : IPackageEncoder<PackageWriter>
//	{
//        //public PackageInfo Decode(ref ReadOnlySequence<byte> buffer, object context)
//        //{
//        //    ISimpleSession session = context as ISimpleSession;
//        //    SerializationReader reader = new SerializationReader(buffer, session.CharacterEncoding);

//        //    long packageLength = reader.ReadInt64Optimized();

//        //    PackageFlags flags = new PackageFlags(reader.ReadUInt64Optimized());
//        //    int packageKey = reader.ReadInt32Optimized();
//        //    int token = (flags.PackageType != PackageType.Message) ? reader.ReadInt32Optimized() : 0;  

//        //    return new PackageInfo(packageLength, flags, packageKey, token, argsReader: reader);
//        //}

//        public int Encode(IBufferWriter<byte> writer, PackageWriter package)
//        {
//            var bufferWritter = new BufferSequenceWriter();
//            var serializationWriter = new SerializationWriter(bufferWritter, package.CharacterEncoding);
//            int bytesWriten;

//            serializationWriter.WriteUInt64Optimized(package.Flags.Value);
//            serializationWriter.WriteInt32Optimized(package.Key);

//            if (package.Flags.PackageType != PackageType.Message)
//                serializationWriter.WriteInt32Optimized(package.Token);

//            // TODO: try catch if WritingTo failed
//            try
//            {
//                package.PackageArgs?.WriteTo(serializationWriter);

//            }
//            catch (Exception ex)
//            {
//                // serializationWriter needs be rewrited since some data by the package.PackageArgs is written but not all
//                bufferWritter = new BufferSequenceWriter();
//                serializationWriter = new SerializationWriter(bufferWritter, package.CharacterEncoding);
//                var flags = package.Flags;
                
//                flags.ResponseSucceed = false;
//				serializationWriter.WriteUInt64Optimized(package.Flags.Value);
//                serializationWriter.WriteInt32Optimized(package.Key);

//                if (package.Flags.PackageType != PackageType.Message)
//                    serializationWriter.WriteInt32Optimized(package.Token);

//                var packageArgs = new ErrorResponseArgs(TaskResultInfo.ExceptionIsCaughtOnArgsWriting, ex.GetFullErrorMessage());

//                packageArgs.WriteTo(serializationWriter);
//            }
//            finally
//            {
//                var packageLengthData = PackageEngine.Create7BitEncodedInt64Span(bufferWritter.BytesWritten);

//                writer.Write(packageLengthData);
//                bufferWritter.WriteTo(writer);

//                bytesWriten = packageLengthData.Length + (int)bufferWritter.BytesWritten;

//                // TODO: Check if this needed or will be handled by SuperSocket with return value -> No need, when this method ends, all written data is flashed to pipeline stream.
//                //writer.Advance(bytesWriten);
//            }

//            return bytesWriten;
//        }
//    }
//}
