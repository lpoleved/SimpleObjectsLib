using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects
{
	public class SimpleObjectKey3
	{
		private int pos = 0;
		private byte[] buffer = new byte[16];

		public static SimpleObjectKey3 Empty = new SimpleObjectKey3(0, 0, false, 0, 0);

		public SimpleObjectKey3(int tableId, int serverId, bool isClientDynamic, long clientId, long objectId)
		{
			ulong unsignedTableId = unchecked((ulong)tableId);
			ulong unsignedServerId = unchecked((ulong)serverId);
			ulong unsignedClientId = (unchecked((ulong)clientId) << 1) | Convert.ToByte(isClientDynamic);
			ulong unsignedObjectId = unchecked((ulong)objectId);

			this.TableId = tableId;
			this.ServerId = serverId;
			this.IsClientDynamic = isClientDynamic;
			this.ClientId = clientId;
			this.ObjectId = objectId;

			Write7BitEncodedUnsignedInt64(unsignedTableId, ref pos, ref buffer);
			Write7BitEncodedUnsignedInt64(unsignedServerId, ref pos, ref buffer);
			Write7BitEncodedUnsignedInt64(unsignedClientId, ref pos, ref buffer);

			while (pos < 16)
			{
				buffer[pos++] = (byte)unsignedObjectId;
				unsignedObjectId >>= 8;

				if (unsignedObjectId == 0)
					break;
			}

			this.Value = new Guid(buffer);
		}

		public SimpleObjectKey3(Guid value)
		{
			this.Initialize(value);
		}

		public SimpleObjectKey3(Guid? value)
		{
			this.Value = value;

			if (value != null)
				this.Initialize((Guid)value);
		}

		private void Initialize(Guid value)
		{
			this.TableId = (int)Read7BitEncodedUnsignedInt64(ref pos, ref buffer);
			this.ServerId = (int)Read7BitEncodedUnsignedInt64(ref pos, ref buffer);
			this.ClientId = (long)Read7BitEncodedUnsignedInt64(ref pos, ref buffer);
			this.IsClientDynamic = Convert.ToBoolean(unchecked(this.ClientId));
			this.ClientId >>= 1;

			ulong objectId = 0;
			var bitShift = 0;

			while (pos < 16 && bitShift < 64)
			{
				objectId |= (ulong)buffer[pos++] << bitShift;
				bitShift += 8;
			}

			this.ObjectId = (long)objectId;
		}

		public int TableId { get; private set; }
		public long ObjectId { get; private set; }
		public int ServerId { get; private set; }
		public long ClientId { get; private set; }
		public bool IsClientDynamic { get; private set; }

		public Guid? Value { get; private set; }

		public override bool Equals(object obj)
		{
			// If parameter is null return false.
			if (obj == null)
				return false;

			// If parameter cannot be cast to Point return false.
			SimpleObjectKey3 objectKey = obj as SimpleObjectKey3;

			if ((object)objectKey == null)
				return false;

			// If both are the same instance, return true.

			if (System.Object.ReferenceEquals(this, objectKey))
				return true;

			// Return true if the key fields match.
			return this.Value == objectKey.Value;
		}

		public bool Equals(SimpleObjectKey3 objectKey)
		{
			// If parameter is null return false:
			if ((object)objectKey == null)
				return false;

			// If both are null, or both are same instance, return true.
			if (System.Object.ReferenceEquals(this, objectKey))
				return true;

			// Return true if the key fields match.
			return this.Value == objectKey.Value;
		}

		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		public static bool operator ==(SimpleObjectKey3 a, SimpleObjectKey3 b)
		{
			// If both are null, or both are same instance, return true.
			if (System.Object.ReferenceEquals(a, b))
				return true;

			// If one is null, but not both, return false.
			if ((object)a == null ^ (object)b == null)
				return false;

			// Return true if the key fields match.
			return a.Value == b.Value;
		}

		public static bool operator !=(SimpleObjectKey3 a, SimpleObjectKey3 b)
		{
			return !(a == b);
		}

		/// <summary>
		/// Stores a 64-bit unsigned value into the buffer using 7-bit encoding.
		/// 
		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
		/// 
		/// See Write(ULong) for details of the values that are optimizable.
		/// </summary>
		/// <param name="value">The ULong value to encode.</param>
		internal static void Write7BitEncodedUnsignedInt64(ulong value, ref int pos, ref byte[] buffer)
		{
			while (value >= 0x80)
			{
				buffer[pos++] = (byte)(value | 0x80);
				value >>= 7;
			}

			buffer[pos++] = (byte)value;
		}

		/// <summary>
		/// Returns a UInt64 value from the buffer that was stored optimized.
		/// </summary>
		/// <returns>A UInt64 value.</returns>
		internal static ulong Read7BitEncodedUnsignedInt64(ref int pos, ref byte[] buffer)
		{
			ulong result = 0;
			var bitShift = 0;

			while (true)
			{
				byte nextByte = buffer[pos++];

				result |= ((ulong)nextByte & 0x7f) << bitShift;

				if ((nextByte & 0x80) == 0)
					return result;

				bitShift += 7;
			}
		}
	}
}
