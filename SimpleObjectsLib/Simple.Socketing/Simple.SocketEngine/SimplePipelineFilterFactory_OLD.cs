//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using SuperSocket.ProtoBase;

//namespace Simple.SocketEngine
//{
//	public class SimplePipelineFilterFactory_OLD : DefaultPipelineFilterFactory<PackageReader, SimplePipelineFilter>, IPipelineFilterFactory<PackageReader>
//	{
//		public SimplePipelineFilterFactory_OLD(IServiceProvider serviceProvider)
//			: base(serviceProvider)
//		{
//			// TODO: Check if this needed !!!
//			//this.PackageDecoder = new PackageDecoder();
//		}

//		protected override IPipelineFilter<PackageReader> CreateCore(object client)
//		{
//			return base.CreateCore(client);
//		}

//		public override IPipelineFilter<PackageReader> Create(object client)
//		{
//			var result = base.Create(client); // base class sets Decoder to null

//			//result.Decoder = new PackageEncoding(); // PackageEncoding.Decoder; 

//			return result;
//		}
//	}
//}
