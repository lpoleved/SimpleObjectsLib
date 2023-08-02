//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple;
//using Simple.Serialization;
//using Simple.Modeling;

//namespace Simple.Objects
//{
//	public class TransactionActionLog_OLD : SerializableObject, ISerializable
//	{
//		protected Func<int, ISimpleObjectModel> getObjectModel { get; set; }
//		protected Func<int, IServerPropertySequence> getPropertySequence { get; set; }

//		public TransactionActionLog_OLD(SystemTransaction transaction,  Func<int, ISimpleObjectModel> getObjectModel, Func<int, IServerPropertySequence> getPropertySequence)
//		{
//			this.Transaction = transaction;
//			this.getObjectModel = getObjectModel;
//			this.getPropertySequence = getPropertySequence;
//		}

//		internal void SetActionInsert(int tableId, long objectId)
//		{
//			this.ActionType = TransactionActionType.Insert;
//			this.TableId = tableId;
//			this.ObjectId = objectId;
//			//this.PropertyValueSequence = new PropertyValueSequence();
//		}

//		internal void SetActionUpdate(int tableId, long objectId, PropertyValueSequence normalizedChangedOldPropertyValues)
//		{
//			this.ActionType = TransactionActionType.Update;
//			this.TableId = tableId;
//			this.ObjectId = objectId;
//			this.PropertyValueSequence = normalizedChangedOldPropertyValues;
//		}

//		internal void SetActionDelete(int tableId, long objectId, PropertyValueSequence normalizedPropertyValues) //, int storableSerializablePropertySequenceId)
//		{
//			this.ActionType = TransactionActionType.Delete;
//			this.TableId = tableId;
//			this.ObjectId = objectId;
//			this.PropertyValueSequence = normalizedPropertyValues;
//			this.PropertySequenceId = this.GetObjectModel().TransactionActionLogPropertySequenceId;
//		}

//		public SystemTransaction Transaction { get; private set; }
//		public TransactionActionType ActionType { get; private set; }
//		public int TableId { get; private set; }
//		public long ObjectId { get; private set; }
//		//public Guid ObjectKey { get; private set; }
//		public PropertyValueSequence PropertyValueSequence { get; private set; }
//		public int PropertySequenceId { get; private set; }
//		public object Tag { get; set; }

//		public ISimpleObjectModel GetObjectModel()
//		{
//			return this.getObjectModel(this.TableId);
//		}

//		//public ISystemPropertySequence GetPropertySequence(int propertySequenceId)
//		//{
//		//	return this.getPropertySequence(propertySequenceId);
//		//}

//		public override int GetBufferCapacity()
//		{
//			return 10;
//		}

//		public override void WriteTo(SerializationWriter writer)
//		{
//			if (this.ActionType == TransactionActionType.Insert)
//			{
//				writer.WriteBoolean(true);
//				writer.WriteInt32Optimized(this.TableId);
//				writer.WriteInt64Optimized(this.ObjectId);
//			}
//			else
//			{
//				writer.WriteBoolean(false);

//				if (this.ActionType == TransactionActionType.Update)
//				{
//					writer.WriteBoolean(true);
//					writer.WriteInt32Optimized(this.TableId);
//					writer.WriteInt64Optimized(this.ObjectId);
//					writer.WriteInt32Optimized(this.PropertyValueSequence.Length);

//					for (int i = 0; i < this.PropertyValueSequence.Length; i++)
//					{
//						IPropertyModel propertyModel = this.PropertyValueSequence.PropertyModels[i];
//						object propertyValue = this.PropertyValueSequence.PropertyValues[i];
//						int typeId = this.PropertyValueSequence.PropertyTypeIds[i];

//						writer.WriteInt32Optimized(propertyModel.Index);
//						writer.WriteInt32Optimized(typeId);

//						SimpleObjectManager.WriteObjectPropertyValue(writer, propertyModel, propertyValue, typeId, defaultOptimization: false);
//					}
//				}
//				else // if (this.ActionType == DatastoreAction.Delete)
//				{
//					ISimpleObjectModel objectModel = this.GetObjectModel();

//					writer.WriteBoolean(false);
//					writer.WriteInt32Optimized(this.TableId);
//					writer.WriteInt32Optimized(this.PropertySequenceId);

//					for (int i = 0; i < this.PropertyValueSequence.Length; i++) // The Guid is already written as ObjectKey
//					{
//						IPropertyModel propertyModel = this.PropertyValueSequence.PropertyModels[i];
//						object propertyValue = this.PropertyValueSequence.PropertyValues[i];
//						int typeId = this.PropertyValueSequence.PropertyTypeIds[i];

//						SimpleObjectManager.WriteObjectPropertyValue(writer, propertyModel, propertyValue, typeId, defaultOptimization: false);
//					}
//				}
//			}
//		}

//		public override void ReadFrom(SerializationReader reader)
//		{
//			if (reader.ReadBoolean()) // Insert
//			{
//				this.ActionType = TransactionActionType.Insert;
//				this.TableId = reader.ReadInt32Optimized();
//				this.ObjectId = reader.ReadInt64Optimized();
//			}
//			else
//			{
//				if (reader.ReadBoolean()) // Update
//				{
//					this.ActionType = TransactionActionType.Update;
//					this.TableId = reader.ReadInt32Optimized();
//					this.ObjectId = reader.ReadInt64Optimized();

//					ISimpleObjectModel objectModel = this.GetObjectModel();
//					IPropertyModel[] propertyModels = new IPropertyModel[reader.ReadInt32Optimized()];
//					object[] propertyValues = new object[propertyModels.Length];

//					for (int i = 0; i < propertyModels.Length; i++) 
//					{
//						int propertyIndex = reader.ReadInt32Optimized();
//						IPropertyModel propertyModel = objectModel.PropertyModels[propertyIndex];
//						int writtenPropertyTypeId = reader.ReadInt32Optimized();

//						propertyModels[i] = propertyModel;
//						propertyValues[i] = SimpleObjectManager.ReadObjectPropertyValue(reader, propertyModel, writtenPropertyTypeId, defaultOptimization: false);
//					}

//					this.PropertyValueSequence = new PropertyValueSequence(propertyModels, propertyValues);
//				}
//				else // Delete
//				{
//					this.ActionType = TransactionActionType.Delete;
//					this.TableId = reader.ReadInt32Optimized();
//					this.PropertySequenceId = reader.ReadInt32Optimized();

//					ISimpleObjectModel objectModel = this.GetObjectModel();
//					IServerPropertySequence propertySequence = this.getPropertySequence(this.PropertySequenceId);
//					IPropertyModel[] propertyModels = new IPropertyModel[propertySequence.Length];
//					object[] propertyValues = new object[propertySequence.Length];

//					for (int i = 0; i < propertyValues.Length; i++) 
//					{
//						int propertyIndex = propertySequence.PropertyIndexes[i];
//						IPropertyModel propertyModel = objectModel.PropertyModels[propertyIndex];
//						int writtenPropertyTypeId = propertySequence.PropertyTypeIds[i];

//						propertyModels[i] = propertyModel;
//						propertyValues[i] = SimpleObjectManager.ReadObjectPropertyValue(reader, propertyModel, writtenPropertyTypeId, defaultOptimization: false);
//					}

//					this.ObjectId = (long)propertyValues[0];
//					this.PropertyValueSequence = new PropertyValueSequence(propertyModels, propertyValues);
//				}
//			}
//		}
//	}

//	//public interface ITransactionAction
//	//{
//	//	int TableId { get; }
//	//	long ObjectId { get; }
//	//	TransactionActionType ActionType { get; }
//	//	PropertyValueSequence PropertyValueSequence { get; }
//	//	//object Tag { get; set; }
//	//}
//}