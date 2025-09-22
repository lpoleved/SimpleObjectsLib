using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Simple;
using Simple.Serialization;
using SuperSocket.ProtoBase;
using System.Buffers;

namespace Simple.SocketEngine
{
	/// <summary>
	/// The receive filter which is designed for the protocol all messages are stored in a dynamically created header
	/// </summary>
	/// <typeparam name="TPackageInfo">The type of the package info.</typeparam>
	public class PipelineFilter : IPipelineFilter<PackageInfo>, IPipelineFilter
	{
		private PackageArgsFactory packageArgsFactory;
		private Encoding characterEncoding = new UTF8Encoding();
		private PackageReader packageReader;
		private bool createPackageDataCopy;

		// Constructor that requires dependency injection
		public PipelineFilter(PackageArgsFactory packageArgsFactory, bool createPackageDataCopy)
        {
			this.packageArgsFactory = packageArgsFactory;
			this.packageReader = new PackageReader(this.packageArgsFactory);
			this.createPackageDataCopy = createPackageDataCopy;
        }

        public PackageInfo Filter(ref SequenceReader<byte> reader)
		{
			if (!this.packageReader.IsPackageLengthInfoReceived(ref reader))
				return default!; // Package length is not fully received yet...

			if (reader.Length < (this.packageReader.PackageLength)) // if (reader.Length < this.packageInfo.TotalPackageLength)
				return default!; // Haven't received a full package -> there is more data...

			if (this.packageReader.TotalPackageLength != reader.Length)
				reader = reader;

			//var pack = reader.Sequence.Slice(0, this.packageInfo.TotalPackageLength);
			var pack = reader; // reader.Sequence;

			try
			{
				return this.packageReader.Decode(ref pack, (this.Context as ISimpleSession)!);
			}
			finally
			{
				reader.Advance(this.packageReader.PackageLength);
			}
		}

		public object? Context { get; set; }
		
		public bool CreatePackageDataCopy 
		{ 
			get => this.createPackageDataCopy;
			set
			{
				this.createPackageDataCopy = value;
				this.packageReader.CreateDataCopy = value;
			}
		}


		///// <summary>
		///// Get the command owner instane that hosts package processing methods on receive
		///// </summary>
		//internal object CommandOwner { get; private set; }

		//public Encoding CharacterEncoding 
		//{ 
		//	get => this.characterEncoding;
		//	set
		//	{ 
		//		this.characterEncoding = value;
		//		this.packageInfo.CharacterEncoding = value;
		//	}
		//}

		public void Reset()
		{
			this.packageReader = new PackageReader(this.packageArgsFactory, this.createPackageDataCopy);
		}

		//public IPackageDecoder<PackageReader>? Decoder { get => this.packageInfo; set { } }
		public IPackageDecoder<PackageInfo>? Decoder { get => this.packageReader; set { } }

		IPipelineFilter<PackageInfo> IPipelineFilter<PackageInfo>.NextFilter => null!;
	}
}
