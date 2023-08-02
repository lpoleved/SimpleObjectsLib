using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ProtoBase;
using Simple.Serialization;

namespace Simple.SocketEngine
{
	public class PackageDecoder : IPackageDecoder<PackageReader>
	{

		private PackageArgsFactory packageArgsFactory;
		private bool createDataCopy;
		private byte[] data = new byte[0];

		public PackageDecoder(PackageArgsFactory packageArgsFactory, bool createDataCopy = false)
        {
			this.packageArgsFactory = packageArgsFactory;
			this.createDataCopy = createDataCopy;   
        }

        public PackageReader Decode(ref ReadOnlySequence<byte> buffer, object context)
		{
			PackageReader packageInfo = new PackageReader(this.packageArgsFactory, this.createDataCopy);
			
			if (this.createDataCopy)
				packageInfo.Buffer = buffer.ToArray();

			if (context is ISimpleSession session)
			{
				//SerializationReader reader = new SerializationReader(ref buffer);
				SequenceReader<byte> sr = new SequenceReader<byte>(buffer);
				SequenceReader reader = new SequenceReader(ref sr);

				//reader.AdvancePosition(this.PackageLengthSize); // Skip reading package data length again

				packageInfo.HeaderInfo = new HeaderInfo(reader.ReadUInt64Optimized());
				packageInfo.Key = reader.ReadInt32Optimized(); // package is response => package.Key is RequestId

				if (packageInfo.HeaderInfo.PackageType == PackageType.Request)
				{
					//packageInfo.Token = reader.ReadInt32Optimized();
					packageInfo.PackageArgs = (packageInfo.HeaderInfo.IsSystem) ? this.packageArgsFactory.CreateSystemRequestArgs(packageInfo.Key) :
																				  this.packageArgsFactory.CreateRequestArgs(packageInfo.Key);
				}
				else if (packageInfo.HeaderInfo.PackageType == PackageType.Response)
				{
					//packageInfo.Token = reader.ReadInt32Optimized();
					packageInfo.PackageArgs = (packageInfo.HeaderInfo.IsSystem) ? this.packageArgsFactory.CreateSystemResponseArgs(packageInfo.Key) :
																				  this.packageArgsFactory.CreateResponseArgs(packageInfo.Key);
				}
				else if (packageInfo.HeaderInfo.PackageType == PackageType.Message)
				{
					packageInfo.PackageArgs = (packageInfo.HeaderInfo.IsSystem) ? this.packageArgsFactory.CreateSystemMessageArgs(packageInfo.Key) :
																				  this.packageArgsFactory.CreateMessageArgs(packageInfo.Key);
				}

				if (packageInfo.PackageArgs != null)
				{
					try
					{
						packageInfo.PackageArgs.ReadFrom(ref reader, session);
					}
					catch (Exception ex)
					{
						packageInfo.PackageArgs.Status = PackageStatus.ExceptionIsCaughtOnArgsSerialization;
						packageInfo.PackageArgs.ErrorMessage = $"Error in args serialization, PackageType={packageInfo.HeaderInfo.PackageType.ToString()}, RequestId={packageInfo.Key}, " +
														$"IsSystem={packageInfo.HeaderInfo.IsSystem.ToString()}: {ExceptionHelper.GetFullErrorMessage(ex)}";
					}
				}
			}

			return packageInfo;
		}
	}
}
