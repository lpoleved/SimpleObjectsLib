using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects
{
	public class GlobalSimpleObjectKey : SimpleObjectKeyOLD
	{
		public new static GlobalSimpleObjectKey Empty = new GlobalSimpleObjectKey(0, 0, 0);

		public GlobalSimpleObjectKey(int creatorId, int tableId, long objectId)
			: base(tableId, objectId)
		{
			this.CreatorId = creatorId;
		}

		/// <summary>
		/// The creator server or client Id. If server is creator, CreatorId = (ServerId LeftShift 1) | 1. If client is creator CreatorId = (ClientId LeftShift 1) | 0.
		/// </summary>
		public int CreatorId { get; private set; }

		public override bool Equals(object obj)
		{
			// If parameter is null return false.
			if (obj == null)
				return false;

			return this.Equals(obj as GlobalSimpleObjectKey);
		}

		public bool Equals(GlobalSimpleObjectKey objectKey)
		{
			return base.Equals(objectKey as SimpleObjectKeyOLD) && this.CreatorId == objectKey.CreatorId;
		}

		public override int GetHashCode()
		{
			return this.CreatorId.GetHashCode() ^ base.GetHashCode();
		}

		public static bool operator ==(GlobalSimpleObjectKey a, GlobalSimpleObjectKey b)
		{
			return (a as SimpleObjectKeyOLD == b as SimpleObjectKeyOLD) && a.CreatorId == b.CreatorId;
		}

		public static bool operator !=(GlobalSimpleObjectKey a, GlobalSimpleObjectKey b)
		{
			return !(a == b);
		}
	}
}
