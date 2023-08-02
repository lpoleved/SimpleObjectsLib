//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.IO;
//using Simple;
//using Simple.Serialization;
//using SuperSocket.ProtoBase;
//using System.Buffers;

//namespace Simple.SocketEngine
//{
//	/// <summary>
//	/// The receive filter which is designed for the protocol all messages are stored in a dynamically created header
//	/// </summary>
//	/// <typeparam name="TPackageInfo">The type of the package info.</typeparam>
//	public class SimplePipelineFilter_OLD : PipelineFilterBase<PackageReader>, IPipelineFilter<PackageReader> //IReceiveFilter<PackageInfo>, IPackageResolver<PackageInfo> //, SuperSocket.SocketBase.Protocol.IReceiveFilter<PackageInfo>,
//	{
//		private long packageDataLength = 0;
//		private int bytesConsumed = 0;
//		private bool packageDataLengthReceived = false;
//		private long totalPackageLength = 0;

//		/// <summary>
//		/// Initializes a new instance of the <see cref="DynamicHeaderReceiveFilter{TPackageInfo}"/> class.
//		/// </summary>
//		/// <param name="size">The size.</param>
//		public SimplePipelineFilter_OLD() 
//		{
//			//this.Decoder = new PackageEncoding(); // PackageEncoding.Decoder; // This is for Client
//		}

//		public override PackageReader Filter(ref SequenceReader<byte> reader)
//		{
//			if (!this.packageDataLengthReceived)
//			{
//				int i = 0;

//				if (reader.CurrentSpan[0] == 10 && reader.CurrentSpan[1] == 8)
//					i = 1;
				
//				if (!PackageEngine.TryGetInt64By7BitEncoding(ref reader, ref this.packageDataLength, ref this.bytesConsumed))
//					return default!; // not enough data for package length. Wait for more...

//				this.packageDataLengthReceived = true;
//				this.totalPackageLength = this.bytesConsumed + this.packageDataLength; //  reader.Consumed = reader.Position.GetInteger()
//			}

//			var totalLength = this.totalPackageLength;

//			if (reader.Length < totalLength)  
//				return default!; // Haven't received a full request package -> there is more data...

//			//var consumed = this.bytesConsumed;
//			var packageLength = this.packageDataLength;
//			var pack = reader.Sequence.Slice(0, totalLength);

//			if (totalLength > 10000)
//				this.bytesConsumed = this.bytesConsumed;

//			// If assume that the package length is received in one piece, we can simplify by this

//			try
//			{
//				 return this.DecodePackage(ref pack);
//			}
//			finally
//			{
//				reader.Advance(packageLength);
//			}
//		}

//		/// <summary>
//		/// Resets this receive filter.
//		/// </summary>
//		public override void Reset()
//		{
//			//base.Reset() // No NextFilter
			
//			this.packageDataLength = 0;
//			this.bytesConsumed = 0;
//			this.packageDataLengthReceived = false;
//			this.totalPackageLength = 0;
//		}
//	}
//}
