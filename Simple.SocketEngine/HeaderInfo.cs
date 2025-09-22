using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;

namespace Simple.SocketEngine
{
	/// <summary>
	/// The PackageFlags struct where package information flags are specified.
	/// </summary>
	public class HeaderInfo
	{
		private int bytesConsumed = 0;

		private const ulong PackageTypeMask = 0x0003;  // First two bits (0000 0011)
		private const ulong RecipientModelMask = 0x000C;  // Third and fourth bits (0000 1100)
		private const ulong IsSystemMask = 0x0010;  // Fifth bit (0001 0000)
		private const ulong ResponseSucceededMask = 0x0020;  // Sixt bit (0010 0000)
		private const ulong ResponseSucceededInverseMask = 0xDF;  // Sixt bit (1101 1111)

		//private const uint HasBodyMask = 0x0004;  // Third bit
		//private const uint IsBrodcastMask = 0x0020;  // Sixt bit
		//private const uint IsMulticastMask = 0x0040;  // Seventh bit
		//private const uint IsExtendedMask = 0x0080;  // Eigth bit

		public static HeaderInfo Empty = new HeaderInfo(0);

		/// <summary>
		/// Initializes a new instance of the <see cref="HeaderInfo"/> struct.
		/// First three bits are PackageType, forth bit is IsSystem (1 if true, 0 if false), fifth bit is IsBroadcast, sixth bit is ResponseSucceed.
		/// </summary>
		/// <param name="packageType">The package type.</param>
		/// <param name="recipientModel">The ricipients sending model.</param>
		/// <param name="isSystem">If true, this is System package communication, otherwise is User Application package.</param>
		/// <param name="responseSucceed">If package type is response, determine if request job was succeesufuly done.</param>
		public HeaderInfo(PackageType packageType, RecipientModel recipientModel, bool isSystem, bool responseSucceed = false)
		{
			this.Value = unchecked((ulong)packageType & PackageTypeMask); // first two bits for PackageType
			this.Value |= unchecked(((ulong)recipientModel << 2) & RecipientModelMask); // third and fourth bits for RecipientModel

			if (isSystem)
				this.Value |= IsSystemMask;

			if (responseSucceed)
				this.Value |= ResponseSucceededMask;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HeaderInfo"/> struct.
		/// </summary>
		/// <param name="value">The <see cref="HeaderInfo"/> <see cref="UInt64"/> value.</param>
		public HeaderInfo(ulong value) => this.Value = value;

		/// <summary>
		/// The <see cref="HeaderInfo"/> <see cref="UInt64"/> value. 
		/// </summary>
		public ulong Value { get; private set; }

		/// <summary>
		/// The package type.
		/// </summary>
		public PackageType PackageType => (PackageType)(this.Value & PackageTypeMask);

		/// <summary>
		/// The recipient model.
		/// </summary>
		public RecipientModel RecipientModel => (RecipientModel)((this.Value & RecipientModelMask) >> 2);

		/// <summary>
		/// The IsSystem flag. If true, this is system package communication, otherwise is user application specific.
		/// </summary>
		public bool IsSystem => (this.Value & IsSystemMask) > 0;

		/// <summary>
		/// The ResponseSucceed flag. If package type is response, determine if request job was done succeesufuly or not.
		/// </summary>
		public bool ResponseSucceed
		{
					 get => (this.Value & ResponseSucceededMask) > 0;
			internal set => this.Value = (value) ? this.Value |= ResponseSucceededMask :
												   this.Value &= ResponseSucceededInverseMask;
		}

		///// <summary>
		///// The Header size in bytes required for serialization, from one to four
		///// </summary>
		//public int BytesConsumed 
		//{ 
		//	get
		//	{
		//		if (this.bytesConsumed == 0)
		//		{
		//			ulong value = this.Value;

		//			while (value > 0)
		//			{
		//				value >>= 7;
		//				this.bytesConsumed++;
		//			}
		//		}

		//		return this.bytesConsumed;

		//	}

		//	internal set => this.bytesConsumed = value;
		//}


		public override string ToString() => this.Value.ToString();
	}
}