using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects
{
	public class SimpleObjectKey2
	{
		public static SimpleObjectKey2 Empty = new SimpleObjectKey2(0, 0, 0, 0, false);

		public SimpleObjectKey2(int serverId, int tableId, long clientId, long objectId, bool isClientDynamic)
		{
			byte[] buffer = new byte[16];
			uint unsignedServerId = unchecked((uint)serverId);
			uint unsignedTableId = ((unchecked((uint)tableId)) << 1) | Convert.ToByte(isClientDynamic);
			ulong unsignedClientId = unchecked((ulong)clientId);
			ulong unsignedObjectId = unchecked((ulong)objectId);

			this.ServerId = serverId;
			this.TableId = tableId;
			this.ObjectId = objectId;
			this.IsClientDynamic = isClientDynamic;

			buffer[0] = (byte)unsignedServerId;

			buffer[1] = (byte)unsignedTableId;
			buffer[2] = (byte)(unsignedTableId >>= 8);

			buffer[3] = (byte)unsignedClientId; unsignedClientId >>= 8;
			buffer[4] = (byte)unsignedClientId; unsignedClientId >>= 8;
			buffer[5] = (byte)unsignedClientId; unsignedClientId >>= 8;
			buffer[6] = (byte)unsignedClientId; unsignedClientId >>= 8;
			buffer[7] = (byte)unsignedClientId;

			buffer[8]  = (byte)unsignedObjectId; unsignedObjectId >>= 8;
			buffer[9]  = (byte)unsignedObjectId; unsignedObjectId >>= 8;
			buffer[10] = (byte)unsignedObjectId; unsignedObjectId >>= 8;
			buffer[11] = (byte)unsignedObjectId; unsignedObjectId >>= 8;
			buffer[12] = (byte)unsignedObjectId; unsignedObjectId >>= 8;
			buffer[13] = (byte)unsignedObjectId; unsignedObjectId >>= 8;
			buffer[14] = (byte)unsignedObjectId; unsignedObjectId >>= 8;
			buffer[15] = (byte)unsignedObjectId;

			//Buffer.BlockCopy(BitConverter.GetBytes(unsignedTableId), 0, buffer, 0, 2);
			//Buffer.BlockCopy(BitConverter.GetBytes(serverId), 0, buffer, 3, 1);
			//Buffer.BlockCopy(BitConverter.GetBytes(clientId), 0, buffer, 4, 5);
			//Buffer.BlockCopy(BitConverter.GetBytes(objectId), 0, buffer, 8, 8);

			this.Value = new Guid(buffer);
		}

		public SimpleObjectKey2(Guid value)
		{
			this.Value = value;
			this.Initialize(value);
		}

		public SimpleObjectKey2(Guid? value)
		{
			this.Value = value;
			if (value != null)
				this.Initialize((Guid)value);
		}

		private void Initialize(Guid value)
		{
			byte[] buffer = value.ToByteArray();

			this.TableId = BitConverter.ToUInt16(buffer, 1) >> 1;
			this.IsClientDynamic = Convert.ToBoolean(buffer[1]);
			this.ServerId = (int)buffer[2];
			this.ClientId = BitConverter.ToInt64(buffer, 3);
			this.ObjectId = BitConverter.ToInt64(buffer, 8);
		}

		//public BusinessObjectKey(SerializationInfo info, StreamingContext context)
		//{
		//    this.TableID = (int)info.GetValue(strTableID, typeof(int));
		//    this.ObjectID = (long)info.GetValue(strObjectID, typeof(long));
		//    this.CreatorServerID = (int)info.GetValue(strCreatorServerID, typeof(int));
		//}

		public int ServerId { get; private set; }
		public int TableId { get; private set; }
		public long ClientId { get; private set; }
		public long ObjectId { get; private set; }
		public bool IsClientDynamic { get; private set; }

		public Guid? Value { get; private set; }

		public override bool Equals(object obj)
		{
			// If parameter is null return false.
			if (obj == null)
				return false;

			// If parameter cannot be cast to Point return false.
			SimpleObjectKey2 objectKey = obj as SimpleObjectKey2;

			if ((object)objectKey == null)
				return false;

			// If both are the same instance, return true.

			if (System.Object.ReferenceEquals(this, objectKey))
				return true;

			// Return true if the key fields match.
			return this.Value == objectKey.Value;
		}

		public bool Equals(SimpleObjectKey2 objectKey)
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

		public static bool operator ==(SimpleObjectKey2 a, SimpleObjectKey2 b)
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

		public static bool operator !=(SimpleObjectKey2 a, SimpleObjectKey2 b)
		{
			return !(a == b);
		}
	}
}
