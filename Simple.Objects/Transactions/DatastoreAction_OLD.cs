//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Runtime.Serialization;
//using Simple;
//using Simple.Serialization;
//using Simple.Modeling;

//namespace Simple.Objects
//{
//	public class DatastoreAction_OLD //: SerializableObjectBase, Simple.Serialization.ISerializable
//	{
//		protected Func<int, ISimpleObjectModel> getObjectModelByTableId { get; set; }
//		protected Func<int, ServerObjectModelInfo> getServerObjectPropertyInfoByTableId { get; set; }

//		public DatastoreAction_OLD(Func<int, ISimpleObjectModel> getObjectModelByTableId, Func<int, ServerObjectModelInfo> getServerObjectPropertyInfoByTableId)
//		{
//			this.getObjectModelByTableId = getObjectModelByTableId;
//			this.getServerObjectPropertyInfoByTableId = getServerObjectPropertyInfoByTableId;
//		}

//		internal void SetActionInsert(int tableId, long objectId, PropertyIndexValues normalizedPropertyValues)
//		{
//			this.ActionType = TransactionActionType.Insert;
//			this.TableId = tableId;
//			this.ObjectId = objectId;
//			this.PropertyIndexValues = normalizedPropertyValues;
//			this.ServerObjectPropertyInfo = this.getServerObjectPropertyInfoByTableId(tableId);
//		}

//		internal void SetActionUpdate(int tableId, long objectId, PropertyIndexValues normalizedChangedPropertyValues)
//		{
//			this.ActionType = TransactionActionType.Update;
//			this.TableId = tableId;
//			this.ObjectId = objectId;
//			this.PropertyIndexValues = normalizedChangedPropertyValues;
//			this.ServerObjectPropertyInfo = this.getServerObjectPropertyInfoByTableId(tableId);
//		}

//		internal void SetActionDelete(int tableId, long objectId)
//		{
//			this.ActionType = TransactionActionType.Delete;
//			this.TableId = tableId;
//			this.ObjectId = objectId;
//			//this.PropertyValueSequence = new PropertyValueSequence();
//			this.ServerObjectPropertyInfo = this.getServerObjectPropertyInfoByTableId(tableId);
//		}

//		public SystemTransaction Transaction { get; private set; }
//		public TransactionActionType ActionType { get; private set; }
//		public DatastoreActionStatus Status { get; internal set; } 
//		public int TableId { get; private set; }
//		public long ObjectId { get; private set; }
//		public PropertyIndexValues PropertyIndexValues { get; private set; }
//		public ServerObjectModelInfo ServerObjectPropertyInfo { get; private set; }
//		//public object Tag { get; set; }

//		public ISimpleObjectModel ObjectModel { get; private set; }

//		public override int GetBufferCapacity()
//		{
//			return 10;
//		}

//		public override void WriteTo(SerializationWriter writer, object? context)
//		{
//			writer.WriteInt32Optimized(this.TableId);
//			writer.WriteInt64Optimized(this.ObjectId);

//			if (this.ActionType == TransactionActionType.Insert)
//			{
//				writer.WriteBoolean(true);
//			}
//			else
//			{
//				writer.WriteBoolean(false);
//				writer.WriteBoolean(this.ActionType == TransactionActionType.Update);
//			}

//			if (this.ActionType != TransactionActionType.Delete) // Write the property values for insert or update
//			{
//				writer.WriteInt32Optimized(this.PropertyIndexValues.Count);

//				for (int i = 0; i < this.PropertyIndexValues.Count; i++)
//				{
//					int propertyIndex = this.PropertyIndexValues.PropertyIndexes.ElementAt(i);
//					object propertyValue = this.PropertyIndexValues.PropertyValues.ElementAt(i);
//					ServerPropertyInfo serverPropertyInfo = this.ServerObjectPropertyInfo.GetPropertyInfo(propertyIndex);

//					writer.WriteInt32Optimized(propertyIndex);
//					SimpleObjectManager.WriteObjectPropertyValue(writer, propertyValue, serverPropertyInfo.DatastoreTypeId, serverPropertyInfo.IsSerializationOptimizable, serverPropertyInfo.DefaultValue, ifDefaultWriteOnlyBoolean: false);
//				}
//			}
//		}

//		public override void ReadFrom(ref BinarySequenceReader reader, object? context)
//		{
//			this.TableId = reader.ReadInt32Optimized();
//			this.ObjectId = reader.ReadInt64Optimized();

//			if (reader.ReadBoolean())
//			{
//				this.ActionType = TransactionActionType.Insert;
//			}
//			else if (reader.ReadBoolean())
//			{
//				this.ActionType = TransactionActionType.Update;
//			}
//			else
//			{
//				this.ActionType = TransactionActionType.Delete;
//				this.PropertyIndexValues = new PropertyIndexValues(new int[0], new object[0]);
//			}

//			if (this.ActionType != TransactionActionType.Delete) // Read the property values for insert or update
//			{
//				ISimpleObjectModel clientObjectModel = this.getObjectModelByTableId(this.TableId);
//				int propertyCount = reader.ReadInt32Optimized();
//				int[] propertyIndexes = new int[propertyCount];
//				object[] propertyValues = new object[propertyCount];

//				this.ServerObjectPropertyInfo = this.getServerObjectPropertyInfoByTableId(this.TableId);
				
//				for (int i = 0; i < propertyCount; i++)
//				{
//					int propertyIndex = reader.ReadInt32Optimized();
//					ServerPropertyInfo serverPropertyInfo = this.ServerObjectPropertyInfo.GetPropertyInfo(propertyIndex);

//					propertyIndexes[i] = propertyIndex;
//					propertyValues[i] = SimpleObjectManager.ReadObjectPropertyValue(ref reader, propertyIndex, serverPropertyInfo.DatastoreTypeId, serverPropertyInfo.IsSerializationOptimizable, 
//																										   serverPropertyInfo.DefaultValue, checkIfPropertyValueIsDefault: false);
//				}

//				this.PropertyIndexValues = new PropertyIndexValues(propertyIndexes, propertyValues);
//			}
//		}
//	}

//	//public enum DatastoreActionStatus
//	//{
//	//	Unfinished = 0,
//	//	Inserted = 1,
//	//	Updated = 2,
//	//	Deleted = 3
//	//}
//}