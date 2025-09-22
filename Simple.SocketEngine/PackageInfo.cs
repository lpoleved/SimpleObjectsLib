using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Serialization;
using SuperSocket.ProtoBase;

namespace Simple.SocketEngine
{
	public class PackageInfo : IKeyedPackageInfo<int>
	{
		//private int start = 0;
		//private int position = 0;
		//private int bytesConsumed = 0;

		////private static readonly ReadUInt64By7BitEncodingDelegate readUInt64By7BitEncoding;

		/////// <summary>
		/////// TryGetPackageLengthBy7BitEncoding endianness delegate
		/////// </summary>
		/////// <param name="reader">The reader</param>
		/////// <param name="value">The length of the package.</param>
		/////// <returns></returns>
		////private delegate ulong ReadUInt64By7BitEncodingDelegate(byte[] buffer, ref int position, ref int bytesConsumed);

		////static PackageInfoNew()
		////{
		////	if (BitConverter.IsLittleEndian)
		////		readUInt64By7BitEncoding = ReadUInt64By7BitLittleEndianEncoding;
		////	else
		////		readUInt64By7BitEncoding = ReadUInt64By7BitBigEndianEncoding;
		////}

		public PackageInfo(byte[]? buffer = null)
		{
			this.HeaderInfo = HeaderInfo.Empty;
			this.Buffer = buffer;
		}

		///// <summary>
		///// PackageInfo constructor
		///// </summary>
		///// <param name="packageLength"></param>
		///// <param name="packageLengthInfoSize"></param>
		///// <param name="totalPackageLength"></param>
		///// <param name="headerInfo"></param>
		///// <param name="key"></param>
		///// <param name="characterEncoding"></param>
		///// <param name="packageArgs"></param>
		///// <param name="buffer"></param>
		//public PackageInfo(long packageLength, int packageLengthInfoSize, long totalPackageLength, HeaderInfo headerInfo,  int key, Encoding characterEncoding, PackageArgs? packageArgs = null, byte[]? buffer = null) //, )
		//{
		//	this.PackageLength = packageLength;
		//	this.PackageLengthBytesConsumed = packageLengthInfoSize;
		//	this.TotalPackageLength = totalPackageLength;
		//	this.HeaderInfo = headerInfo;
		//	this.Key = key;
		//	this.CharacterEncoding = characterEncoding;
		//	this.PackageArgs = packageArgs;
		//	this.Buffer = buffer;
		//}

		public HeaderInfo HeaderInfo { get; internal set; }
		//public int HeaderSize { get; set; }

		/// <summary>
		/// The package key.
		/// If package type is message it is message code.
		/// If package is request it is request id.
		/// </summary>
		public int Key { get; internal set; }

		//public Encoding CharacterEncoding { get; set; } = UTF8Encoding.Default; // Encoding.UTF8;

		//public int Token { get; set; }

		public PackageArgs? PackageArgs { get; set; }

		public byte[]? Buffer { get; set; } = null;
	}
}
