using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects
{
	public class SimpleObjectKeyOLD
	{
		public static SimpleObjectKeyOLD Empty = new SimpleObjectKeyOLD(0, 0);

		public SimpleObjectKeyOLD(int tableId, long objectId)
		{
			this.TableId = tableId;
			this.ObjectId = objectId;
		}

		public int TableId { get; private set; }
		public long ObjectId { get; private set; }

		public override bool Equals(object obj)
		{
			// If parameter is null return false.
			if (obj == null)
				return false;

			// If parameter cannot be cast to Point return false.
			SimpleObjectKeyOLD? objectKey = obj as SimpleObjectKeyOLD;

			if (objectKey is null)
				return false;

			// If both are the same instance, return true.
			if (System.Object.ReferenceEquals(this, objectKey))
				return true;

			// Return true if the key fields match.
			return this.TableId == objectKey.TableId && this.ObjectId == objectKey.ObjectId;
		}

		public bool Equals(SimpleObjectKeyOLD? objectKey)
		{
			// If parameter is null return false:
			if (objectKey is null)
				return false;

			// If both are null, or both are same instance, return true.
			if (System.Object.ReferenceEquals(this, objectKey))
				return true;

			// Return true if the key fields match.
			return this.TableId == objectKey.TableId && this.ObjectId == objectKey.ObjectId;
		}

		public override int GetHashCode()
		{
			return this.TableId.GetHashCode() ^ this.ObjectId.GetHashCode();
		}

		public static bool operator ==(SimpleObjectKeyOLD? a, SimpleObjectKeyOLD? b)
		{
			// If both are null, or both are same instance, return true.
			if (System.Object.ReferenceEquals(a, b))
				return true;

			// If one is null, but not both, return false.
			if (a is null ^ b is null)
				return false;

			// Return true if the key fields match.
			return a.TableId == b.TableId && a.ObjectId == b.ObjectId;
		}

		public static bool operator !=(SimpleObjectKeyOLD? a, SimpleObjectKeyOLD? b)
		{
			return !(a == b);
		}
	}
}
