//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
////using System.Runtime.Serialization;

//namespace Simple.Objects
//{
//	//[Serializable]
//	public class SimpleObjectKey //: ISerializable
//	{
//		private const string strTableID = "TableID";
//		private const string strObjectID = "ObjectID";
//		private const string strCreatorServerID = "CreatorServerID";

//		public static SimpleObjectKey Empty = new SimpleObjectKey(0, 0, 0);


//		public SimpleObjectKey(int tableID, long objectID, int creatorServerID)
//		{
//			this.TableID = tableID;
//			this.ObjectID = objectID;
//			this.CreatorServerID = creatorServerID;
//		}

//		//public BusinessObjectKey(SerializationInfo info, StreamingContext context)
//		//{
//		//    this.TableID = (int)info.GetValue(strTableID, typeof(int));
//		//    this.ObjectID = (long)info.GetValue(strObjectID, typeof(long));
//		//    this.CreatorServerID = (int)info.GetValue(strCreatorServerID, typeof(int));
//		//}

//		public int TableID { get; private set; }
//		public long ObjectID { get; private set; }
//		public int CreatorServerID { get; private set; }

//		public static SimpleObjectKey GetEmptyKey()
//		{
//			return Empty;
//		}

//		public static SimpleObjectKey GetEmptyKey(int objectTypeID)
//		{
//			return new SimpleObjectKey(objectTypeID, 0, 0);
//		}

//		public override bool Equals(object obj)
//		{
//			// If parameter is null return false.
//			if (obj == null)
//			{
//				return false;
//			}

//			// If parameter cannot be cast to Point return false.
//			SimpleObjectKey objectKeyToCompare = obj as SimpleObjectKey;
//			if ((object)objectKeyToCompare == null)
//			{
//				return false;
//			}
//			// If both are the same instance, return true.
//			if (System.Object.ReferenceEquals(this, objectKeyToCompare))
//			{
//				return true;
//			}

//			// Return true if the key fields match.
//			return this.TableID == objectKeyToCompare.TableID && this.ObjectID == objectKeyToCompare.ObjectID && this.CreatorServerID == objectKeyToCompare.CreatorServerID;
//		}

//		public bool Equals(SimpleObjectKey objectKey)
//		{
//			// If parameter is null return false:
//			if ((object)objectKey == null)
//			{
//				return false;
//			}

//			// If both are null, or both are same instance, return true.
//			if (System.Object.ReferenceEquals(this, objectKey))
//			{
//				return true;
//			}

//			// Return true if the key fields match.
//			return this.TableID == objectKey.TableID && this.ObjectID == objectKey.ObjectID && this.CreatorServerID == objectKey.CreatorServerID;
//		}

//		public override int GetHashCode()
//		{
//			return this.TableID.GetHashCode() ^ this.ObjectID.GetHashCode() ^ this.CreatorServerID.GetHashCode();
//		}

//		public static bool operator ==(SimpleObjectKey a, SimpleObjectKey b)
//		{
//			// If both are null, or both are same instance, return true.
//			if (System.Object.ReferenceEquals(a, b))
//			{
//				return true;
//			}

//			// If one is null, but not both, return false.
//			if ((object)a == null ^ (object)b == null)
//			{
//				return false;
//			}

//			// Return true if the key fields match.
//			return a.TableID == b.TableID && a.ObjectID == b.ObjectID && a.CreatorServerID == b.CreatorServerID;
//		}

//		public static bool operator !=(SimpleObjectKey a, SimpleObjectKey b)
//		{
//			return !(a == b);
//		}

//		//void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
//		//{
//		//    info.AddValue(strTableID, this.TableID);
//		//    info.AddValue(strObjectID, this.ObjectID);
//		//    info.AddValue(strCreatorServerID, this.CreatorServerID);
//		//}
//	}
//}
