using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects
{
	public struct SimpleObjectKey
	{
		public static SimpleObjectKey Empty = new SimpleObjectKey(0, 0);

		public SimpleObjectKey(int tableId, long objectId)
		{
			this.TableId = tableId;
			this.ObjectId = objectId;
		}

		public int TableId { get; private set; }
		public long ObjectId { get; private set; }

		public override bool Equals(object obj) => obj is SimpleObjectKey other && this.Equals(other);

		public bool Equals(SimpleObjectKey key) => this.TableId == key.TableId && this.ObjectId == key.ObjectId;

		public override int GetHashCode() => (this.TableId, this.ObjectId).GetHashCode();

		public static bool operator ==(SimpleObjectKey a, SimpleObjectKey b) => a.Equals(b);

		public static bool operator !=(SimpleObjectKey a, SimpleObjectKey b) => !(a == b);

		//public override bool Equals(object obj)
		//{
		//	// If parameter is null return false.
		//	if (obj == null)
		//		return false;

		//	// If parameter cannot be cast to Point return false.
		//	SimpleObjectKey objectKey = obj as SimpleObjectKey;

		//	if ((object)objectKey == null)
		//		return false;

		//	// If both are the same instance, return true.
		//	if (System.Object.ReferenceEquals(this, objectKey))
		//		return true;

		//	// Return true if the key fields match.
		//	return this.TableId == objectKey.TableId && this.ObjectId == objectKey.ObjectId;
		//}

		//public bool Equals(SimpleObjectKey objectKey)
		//{
		//	// If parameter is null return false:
		//	if ((object)objectKey == null)
		//		return false;

		//	// If both are null, or both are same instance, return true.
		//	if (System.Object.ReferenceEquals(this, objectKey))
		//		return true;

		//	// Return true if the key fields match.
		//	return this.TableId == objectKey.TableId && this.ObjectId == objectKey.ObjectId;
		//}

		//public override int GetHashCode()
		//{
		//	return this.TableId.GetHashCode() ^ this.ObjectId.GetHashCode();
		//}

		//public static bool operator ==(SimpleObjectKey a, SimpleObjectKey b)
		//{
		//	// If both are null, or both are same instance, return true.
		//	if (System.Object.ReferenceEquals(a, b))
		//		return true;

		//	// If one is null, but not both, return false.
		//	if ((object)a == null ^ (object)b == null)
		//		return false;

		//	// Return true if the key fields match.
		//	return a.TableId == b.TableId && a.ObjectId == b.ObjectId;
		//}

		//public static bool operator !=(SimpleObjectKey a, SimpleObjectKey b)
		//{
		//	return !(a == b);
		//}
	}
}
