using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ProtoBase;

namespace Simple.SocketEngine
{
	public class PipelineFilterFactory : IPipelineFilterFactory<PackageReader> //, DefaultPipelineFilterFactory<PackageInfo, SimplePipelineFilter>
	{
		protected IPackageDecoder<PackageReader>? PackageDecoder { get; private set; }

		public PipelineFilterFactory(IServiceProvider serviceProvider, PackageArgsFactory packageArgsFactory)
		{
			this.PackageDecoder = serviceProvider.GetService(typeof(IPackageDecoder<PackageReader>)) as IPackageDecoder<PackageReader>;

			// TODO: Check if this needed !!!
			//this.PackageDecoder = new PackageInfo(packageArgsFactory);
			this.PackageArgsFactory = packageArgsFactory;
		}

		public PackageArgsFactory PackageArgsFactory { get; private set; }

		public IPipelineFilter<PackageReader> Create()
		{
			var result = new PipelineFilter(this.PackageArgsFactory, createPackageDataCopy: false); // base class sets Decoder to null

			//result.Decoder = new PackageEncoding(this.CommandDiscovery); // PackageEncoding.Decoder; 

			return result;
		}
	}
}
