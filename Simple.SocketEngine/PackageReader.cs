using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using SuperSocket.ProtoBase;
using System.Buffers;
using System.Diagnostics.Eventing.Reader;
using System.IO.Packaging;
using Simple.Serialization;

namespace Simple.SocketEngine
{
	public class PackageReader : IPackageDecoder<PackageInfo> //, IKeyedPackageInfo<int>
	{
		//private long packageDataLength = 0;
		//private int packageLengthInfoSize = 0;
		private bool packageLengthReceived = false;
		//private long totalPackageLength = 0;
		private PackageArgsFactory? packageArgsFactory = null;

		private static readonly TryGetInt64By7BitEncodingDelegate tryGetPackageLength;

		/// <summary>
		/// TryGetPackageLengthBy7BitEncoding endianness delegate
		/// </summary>
		/// <param name="reader">The reader</param>
		/// <param name="value">The length of the package.</param>
		/// <returns></returns>
		private delegate bool TryGetInt64By7BitEncodingDelegate(ref SequenceReader<byte> reader, ref int packageLengthBytesConsumed, ref long packageLength);

		static PackageReader()
		{
			if (BitConverter.IsLittleEndian)
				tryGetPackageLength = TryGetPackageLengthByLittleEndianEncoding;
			else
				tryGetPackageLength = TryGetPackageLengthByBigEndianEncoding;
		}

		public PackageReader(PackageArgsFactory packageArgsFactory, bool createDataCopy = false)
        {
			this.packageArgsFactory = packageArgsFactory;
			this.CreateDataCopy = createDataCopy;
			this.PackageInfo = new PackageInfo();
		}

		public PackageReader(byte[] buffer)
		{
			this.PackageInfo = new PackageInfo(buffer);
			//this.packageArgsFactory = PackageArgsFactory.Empty;
			this.CreateDataCopy = false;
		}

		private long packageLength;
		private int packageLengthBytesConsumed;

		public PackageInfo PackageInfo { get; private set; }

		public long PackageLength => this.packageLength;
		public int PackageLengthBytesConsumed => this.packageLengthBytesConsumed;
		public long TotalPackageLength { get; private set; }

		internal bool CreateDataCopy { get; set; }

		//public long PackageLength => this.packageDataLength;
		//public int PackageLengthInfoSize => this.packageLengthInfoSize;

		//public long TotalPackageLength => this.totalPackageLength;

		//public HeaderInfo HeaderInfo 
		//{ 
		//	get; 

		//	internal set; 
		//}
		////public int HeaderSize { get; set; }


		///// <summary>
		///// The package key.
		///// If package type is message it is message code.
		///// If package is request it is request id.
		///// </summary>
		//public int Key { get; set; }

		////public Encoding CharacterEncoding => this.Session.CharacterEncoding;

		////public int Token { get; set; }

		//public PackageArgs? PackageArgs { get; set; }

		//public byte[]? Buffer { get; set; } = null;

		public PackageInfo Decode(ref ReadOnlySequence<byte> sequence, object context)
		{
			SequenceReader<byte> reader = new SequenceReader<byte>(sequence);

			return this.Decode(ref reader, context as ISimpleSession);
		}

		public PackageInfo Decode(ref SequenceReader<byte> sequence, ISimpleSession? session)
		{
			SequenceReader reader = new SequenceReader(ref sequence);
			
			if (this.CreateDataCopy)
				this.PackageInfo.Buffer = sequence.Sequence.ToArray();

			this.DecodeHeader(ref reader);
			this.DecodePackageKey(ref reader);
			this.DecodePackageArgs(ref reader, session, this.packageArgsFactory);

			return this.PackageInfo;
		}

		protected virtual void DecodePackageLength(ref SequenceReader<byte> sequence)
		{
			this.IsPackageLengthInfoReceived(ref sequence);
		}

		protected virtual void DecodeHeader(ref SequenceReader reader)
		{
			this.PackageInfo.HeaderInfo = new HeaderInfo(reader.ReadUInt64Optimized());
		}

		protected virtual void DecodePackageKey(ref SequenceReader reader)
		{
			this.PackageInfo.Key = reader.ReadInt32Optimized(); // package is response => package.Key is RequestId
		}

		protected virtual void DecodePackageArgs(ref SequenceReader reader, ISimpleSession? session, PackageArgsFactory? packageArgsFactory = null)
		{
			if (this.PackageInfo.PackageArgs is null && packageArgsFactory != null)
			{
				if (this.PackageInfo.HeaderInfo.PackageType == PackageType.Request)
				{
					this.PackageInfo.PackageArgs = (this.PackageInfo.HeaderInfo.IsSystem) ? packageArgsFactory.CreateSystemRequestArgs(this.PackageInfo.Key) :
																							packageArgsFactory.CreateRequestArgs(this.PackageInfo.Key);
				}
				else if (this.PackageInfo.HeaderInfo.PackageType == PackageType.Response)
				{
					this.PackageInfo.PackageArgs = (this.PackageInfo.HeaderInfo.IsSystem) ? packageArgsFactory.CreateSystemResponseArgs(this.PackageInfo.Key) :
																							packageArgsFactory.CreateResponseArgs(this.PackageInfo.Key);
				}
				else if (this.PackageInfo.HeaderInfo.PackageType == PackageType.Message)
				{
					this.PackageInfo.PackageArgs = (this.PackageInfo.HeaderInfo.IsSystem) ? packageArgsFactory.CreateSystemMessageArgs(this.PackageInfo.Key) :
																							packageArgsFactory.CreateMessageArgs(this.PackageInfo.Key);
				}
			}

			if (this.PackageInfo.PackageArgs != null)
			{
				if (session != null)
				{
					try
					{
						this.PackageInfo.PackageArgs.ReadFrom(ref reader, session);
					}
					catch (Exception ex)
					{
						this.PackageInfo.PackageArgs.Status = PackageStatus.ExceptionIsCaughtOnArgsSerialization;
						this.PackageInfo.PackageArgs.ErrorMessage = $"Error in args serialization, PackageType={this.PackageInfo.HeaderInfo.PackageType.ToString()}, RequestId={this.PackageInfo.Key}, " +
																	$"IsSystem={this.PackageInfo.HeaderInfo.IsSystem.ToString()}: {ExceptionHelper.GetFullErrorMessage(ex)}";
					}
				}
			}
		}

		internal bool IsPackageLengthInfoReceived(ref SequenceReader<byte> reader) //ref SequenceReader<byte> buffer)
		{
			if (!this.packageLengthReceived)
			{
				this.packageLengthReceived = TryGetPackageLength(ref reader, ref this.packageLengthBytesConsumed, ref this.packageLength);

				if (this.packageLengthReceived)
					this.TotalPackageLength = this.PackageLengthBytesConsumed + this.PackageLength;
			}

			return this.packageLengthReceived;
		}

		/// <summary>
		/// Reads a 64-bit unsigned value into the stream using 7-bit endianness encoding.
		/// 
		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
		/// </summary>
		private static bool TryGetPackageLength(ref SequenceReader<byte> reader, ref int packageLengthBytesConsumed, ref long packageLength) => tryGetPackageLength(ref reader, ref packageLengthBytesConsumed, ref packageLength);

		/// <summary>
		/// reads a 64-bit unsigned value into the stream using 7-bit little endian encoding.
		/// 
		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		private static bool TryGetPackageLengthByLittleEndianEncoding(ref SequenceReader<byte> reader, ref int packageLengthBytesConsumed, ref long packageLength)
		{
			int bitShift = 0;

			//byte? first = null;
			//byte? second = null;
			//bool thatsIt = false;

			while (true)
			{
				if (!reader.TryRead(out byte nextByte))
					return false; // no enought data

				packageLengthBytesConsumed++;
				packageLength |= ((long)nextByte & 0x7f) << bitShift;
				bitShift += 7;

				if ((nextByte & 0x80) == 0)
					return true;
			}
		}

		/// <summary>
		/// reads a 64-bit unsigned value into the stream using 7-bit big endian encoding.
		/// 
		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		private static bool TryGetPackageLengthByBigEndianEncoding(ref SequenceReader<byte> reader, ref int packageLengthBytesConsumed, ref long packageLength)
		{
			for (int i = 0; i < 9; i++)
			{
				if (!reader.TryRead(out byte nextByte))
					return false; // no enought data

				packageLengthBytesConsumed++;

				if (unchecked((long)nextByte) == -1)
					throw new Exception("End of Stream Exception");

				packageLength = (packageLength << 7) | ((long)nextByte & 0x7f);

				if ((nextByte & 0x80) == 0)
					return true;
			}

			throw new Exception("Invalid 7-bit encoded integer in stream."); // <- Still haven't seen a byte with the high bit unset? Dodgy data.
		}
	}
}
