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
	public class PackageDecoder : IPackageDecoder<PackageInfo>
	{

		private PackageArgsFactory packageArgsFactory;
		private bool createDataCopy;

		public PackageDecoder(PackageArgsFactory packageArgsFactory, bool createDataCopy = false)
        {
			this.packageArgsFactory = packageArgsFactory;
			this.createDataCopy = createDataCopy;   
        }

        public PackageInfo Decode(ref ReadOnlySequence<byte> buffer, object context) => new PackageReader(this.packageArgsFactory, this.createDataCopy).Decode(ref buffer, context);
	}
}
