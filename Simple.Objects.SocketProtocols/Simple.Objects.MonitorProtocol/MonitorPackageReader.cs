using Simple.Serialization;
using Simple.SocketEngine;
using SuperSocket.ProtoBase;
using System;
using System.Buffers;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects.MonitorProtocol
{
	public class MonitorPackageReader : PackageReader // IPackageDecoder<PackageInfo> //, IKeyedPackageInfo<int>
	{
		public MonitorPackageReader(byte[] buffer)
			: base(buffer)
        {
		}

		public int HeaderInfoBytesConsumed { get; private set; }
		public int KeyBytesConsumed { get; internal set; }
		public long PackageArgsBytesConsumed { get; internal set; }

		internal long BytesConsumed { get; set; }

		public new void DecodePackageLength(ref SequenceReader<byte> buffer)
		{
			base.DecodePackageLength(ref buffer);
			this.BytesConsumed = this.PackageLengthBytesConsumed;
		}

		public new void DecodeHeader(ref SequenceReader reader)
		{
			long bytesConsumed = reader.BytesConsumed;

			base.DecodeHeader(ref reader);
			this.HeaderInfoBytesConsumed = (int)(reader.BytesConsumed - bytesConsumed);
			this.BytesConsumed += this.HeaderInfoBytesConsumed;
		}

		public new void DecodePackageKey(ref SequenceReader reader)
		{
			long bytesConsumed = reader.BytesConsumed;

			base.DecodePackageKey(ref reader);
			this.KeyBytesConsumed = (int)(reader.BytesConsumed - bytesConsumed);
			this.BytesConsumed += this.KeyBytesConsumed;
		}

		public void DecodePackageArgs(ISimpleSession? session)
		{
			if (this.PackageInfo.Buffer != null)
			{
				SequenceReader reader = new SequenceReader(this.PackageInfo.Buffer);

				reader.AdvancePosition(this.BytesConsumed);
				this.DecodePackageArgs(ref reader, session);
			}
		}

		public void DecodePackageLengthHeaderAndKey()
		{
			if (this.PackageInfo.Buffer != null)
			{
				var readerSequence = new SequenceReader<byte>(new ReadOnlySequence<byte>(this.PackageInfo.Buffer));

				this.DecodePackageLengthHeaderAndKey(ref readerSequence);
			}
			else
			{
				throw new ArgumentNullException("The PackageInfo.Buffer is null");
			}
		}

		public void DecodePackageLengthHeaderAndKey(ref SequenceReader<byte> sequence)
		{
			this.DecodePackageLength(ref sequence);

			var reader = new SequenceReader(ref sequence);

			this.DecodeHeader(ref reader);
			this.DecodePackageKey(ref reader);
		}

		public new void DecodePackageArgs(ref SequenceReader reader, ISimpleSession? session, PackageArgsFactory? packageArgsFactory = null)
		{
			long bytesConsumed = reader.BytesConsumed;

			base.DecodePackageArgs(ref reader, session);
			
			this.PackageArgsBytesConsumed = reader.BytesConsumed - bytesConsumed;
			this.BytesConsumed += this.PackageArgsBytesConsumed;
		}
	}
}
