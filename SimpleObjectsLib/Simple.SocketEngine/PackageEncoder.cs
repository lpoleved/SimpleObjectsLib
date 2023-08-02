using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Buffers;
using SuperSocket.ProtoBase;
using Simple.Serialization;
using System.Diagnostics.Eventing.Reader;

namespace Simple.SocketEngine
{
	public class PackageEncoder : IPackageEncoder<PackageWriter>
	{
		private bool createDataCopy;

		public PackageEncoder(bool createDataCopy = false)
        {
			this.createDataCopy = createDataCopy;
        }

		public int MaxStackSize = 256;

		internal bool CreateDataCopy {  get => this.createDataCopy; set => this.createDataCopy = value; }

        public int Encode(IBufferWriter<byte> writer, PackageWriter package)
        {
			int bufferCapacity = (package.PackageArgs != null) ? package.PackageArgs.GetBufferCapacity() + 4 : 8;
			Span<byte> span = stackalloc byte[Math.Min(bufferCapacity, MaxStackSize)];
            SequenceWriter binaryWriter = new SequenceWriter(span, package.CharacterEncoding);

			if (package.HeaderInfo.PackageType == PackageType.Response)
				package.headerInfo.ResponseSucceed = true; // Maybe already set to true

			package.WriteHeader(ref binaryWriter);
			
			try
			{
				package.PackageArgs?.WriteTo(ref binaryWriter, package.Session);
			}
			catch (Exception ex) // Write new error inform data since exception is cought.
			{
				binaryWriter = new SequenceWriter(span, package.CharacterEncoding);

				if (package.HeaderInfo.PackageType == PackageType.Response)
					package.headerInfo.ResponseSucceed = false; 

				package.WriteHeader(ref binaryWriter);
				package.PackageArgs = new ErrorResponseArgs(PackageStatus.ExceptionIsCaughtOnArgsWriting, ex.GetFullErrorMessage());
				package.PackageArgs?.WriteTo(ref binaryWriter, package.Session);
			}

            var packageLengthData = PackageEngine.Create7BitEncodedInt64Span(binaryWriter.BytesWritten);

            writer.Write(packageLengthData);
            binaryWriter.WriteDataTo(writer);

			if (this.createDataCopy)
			{
				// The resulting buffer contains package length data + package header + package args
				byte[] buffer = new byte[binaryWriter.BytesWritten + packageLengthData.Length];

				packageLengthData.CopyTo(buffer);
				package.Buffer = binaryWriter.ToArray(buffer, start: packageLengthData.Length);
			}

			return packageLengthData.Length + binaryWriter.BytesWritten;
        }
    }
}
