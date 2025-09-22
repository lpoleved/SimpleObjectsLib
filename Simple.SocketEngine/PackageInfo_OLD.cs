//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Simple.Serialization;
//using SuperSocket.ProtoBase;

//namespace Simple.SocketEngine
//{
//	public class PackageInfo_OLD : IKeyedPackageInfo<int>
//	{
//		//private int start = 0;
//		private int position = 0;
//		private int bytesConsumed = 0;

//		private static readonly ReadUInt64By7BitEncodingDelegate readUInt64By7BitEncoding;

//		/// <summary>
//		/// TryGetPackageLengthBy7BitEncoding endianness delegate
//		/// </summary>
//		/// <param name="reader">The reader</param>
//		/// <param name="value">The length of the package.</param>
//		/// <returns></returns>
//		private delegate ulong ReadUInt64By7BitEncodingDelegate(byte[] buffer, ref int position, ref int bytesConsumed);

//		static PackageInfo_OLD()
//		{
//			if (BitConverter.IsLittleEndian)
//				readUInt64By7BitEncoding = ReadUInt64By7BitLittleEndianEncoding;
//			else
//				readUInt64By7BitEncoding = ReadUInt64By7BitBigEndianEncoding;
//		}

//		public PackageInfo_OLD(byte[] buffer, int start = 0) //, Encoder characterEncoder)
//		{
//			this.Buffer = buffer;
//			this.position = start;
//			//this.start = start;
//			//this.CharacterEncoder = characterEncoder;
//			//this.SerializationReader = serializationReader;
//		}

//		public byte[] Buffer { get; private set; }

//		//public int Position => this.position;

//		public int BytesConsumed => this.bytesConsumed;

//		//public Encoder CharacterEncoder { get; private set; }

//		public long PackageLength { get; private set; }
//		public int PackageLengthInfoSize { get; private set; }

//		public HeaderInfo? HeaderInfo { get; private set; } = null;
//		public int HeaderSize { get; private set; }

//		/// <summary>
//		/// The package key.
//		/// If package type is message it is message code.
//		/// If package is request or response it is request id.
//		/// </summary>
//		public int Key { get; private set; }

//		//public int Token { get; private set; }

//		public PackageArgs? PackageArgs { get; private set; } = null;
//		//public SerializationReader SerializationReader { get; private set; }

//		public void ReadPackageLength()
//		{
//			this.PackageLength = this.ReadInt64Optized();
//			this.PackageLengthInfoSize = this.BytesConsumed;
//		}

//		public void ReadHeader()
//		{
//			this.HeaderInfo = new HeaderInfo(this.ReadUInt64Optized());
//			this.Key = this.ReadInt32Optimized();

//			//if (this.HeaderInfo.PackageType != PackageType.Message)
//			//	this.Token = this.ReadInt32Optimized();

//			this.HeaderSize = this.BytesConsumed - this.PackageLengthInfoSize;
//		}

//		public void ReadArgs<T>(ref SequenceReader reader, ISimpleSession session) 
//			where T : PackageArgs, new()
//		{
//			this.ReadArgs(new T(), ref reader, session);
//		}

//		public void ReadArgs(PackageArgs args, ref SequenceReader reader, ISimpleSession session)
//		{
//			this.PackageArgs = args;

//			if (this.HeaderInfo.PackageType == PackageType.Response && !this.HeaderInfo.ResponseSucceed)
//				this.PackageArgs.ReadErrorInfo(ref reader);
//			else
//				this.PackageArgs.ReadFrom(ref reader, session);
//		}

//		/// <summary>
//		/// Reads a 64-bit unsigned value into the stream using 7-bit endianness encoding.
//		/// 
//		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
//		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
//		/// </summary>
//		public static ulong ReadUInt64By7BitEncoding(byte[] buffer, ref int position, ref int bytesConsumed) => readUInt64By7BitEncoding(buffer, ref position, ref bytesConsumed);

//		/// <summary>
//		/// reads a 64-bit unsigned value into the stream using 7-bit little endian encoding.
//		/// 
//		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
//		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
//		/// </summary>
//		/// <param name="reader"></param>
//		/// <param name="value"></param>
//		/// <returns></returns>
//		public static ulong ReadUInt64By7BitLittleEndianEncoding(byte[] buffer, ref int position, ref int bytesConsumed)
//		{
//			int bitShift = 0;
//			ulong value = 0;
			
//			while (true)
//			{
//				byte nextByte = buffer[position++];

//				bytesConsumed++;

//				value |= ((ulong)nextByte & 0x7f) << bitShift;
//				bitShift += 7;

//				if ((nextByte & 0x80) == 0)
//					return value;
//			}
//		}

//		/// <summary>
//		/// reads a 64-bit unsigned value into the stream using 7-bit big endian encoding.
//		/// 
//		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
//		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
//		/// </summary>
//		/// <param name="reader"></param>
//		/// <param name="value"></param>
//		/// <returns></returns>
//		public static ulong ReadUInt64By7BitBigEndianEncoding(byte[] buffer, ref int position, ref int bytesConsumed)
//		{
//			ulong value = 0;
			
//			for (int i = 0; i < 9; i++)
//			{
//				byte nextByte = buffer[position++];

//				bytesConsumed++;

//				//if (unchecked((long)nextByte) == -1)
//				//	throw new Exception("End of Stream Exception");

//				value = (value << 7) | ((ulong)nextByte & 0x7f);

//				if ((nextByte & 0x80) == 0)
//					return value;
//			}

//			throw new Exception("Invalid 7-bit encoded integer in stream."); // <- Still haven't seen a byte with the high bit unset? Dodgy data.
//		}

//		private int ReadInt32Optimized() => (int)this.ReadUInt64Optized();
//		private long ReadInt64Optized() => (long)this.ReadUInt64Optized();
//		private ulong ReadUInt64Optized() => ReadUInt64By7BitEncoding(this.Buffer, ref this.position, ref this.bytesConsumed);
//	}
//}
