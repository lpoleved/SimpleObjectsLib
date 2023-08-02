//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Diagnostics;
//using Simple;
//using Simple.Serialization;
//using Simple.Modeling;
//using Simple.Objects;
//using Simple.SocketEngine;

//namespace Simple.Objects.SocketProtocol
//{
//	[SystemRequestArgs((int)SystemRequest.TransactionRequest)]

//	public class ProcessTransactionRequestArgs : RequestArgs, ISerializable
//	{
//		public ProcessTransactionRequestArgs(Func<int, ServerObjectModelInfo> getServerObjectPropertyInfoByTableId)
//		{
//			this.GetServerObjectPropertyInfoByTableId = getServerObjectPropertyInfoByTableId;
//		}

//		/// <summary>
//		/// Initialize ProcessTransactionRequestArgs class. Intended to be called from server.
//		/// </summary>
//		public ProcessTransactionRequestArgs(Func<int, ISimpleObjectModel> getClientObjectModelByTableId, Func<int, ServerObjectModelInfo> getServerObjectPropertyInfoByTableId)
//		{
//			this.GetClientObjectModelByTableId = getClientObjectModelByTableId;
//			this.GetServerObjectPropertyInfoByTableId = getServerObjectPropertyInfoByTableId;
//		}

//		/// <summary>
//		/// Initialize ProcessTransactionRequestArgs class. Intended to be called from client.
//		/// </summary>
//		/// <param name="transactionActionDataInfoList">TransactionActionInfoList that should be geven by ChangeContainer in sorted order by Insert, Update and Delete.</param>
//		/// <param name="getServerObjectPropertyInfoByTableId">GetServerObjectPropertyInfoByTableId param</param>
//		public ProcessTransactionRequestArgs(IEnumerable<TransactionActionInfo> transactionActionDataInfoList, Func<int, ISimpleObjectModel> getClientObjectModelByTableId, Func<int, ServerObjectModelInfo> getServerObjectPropertyInfoByTableId)
//		{
//			this.TransactionActionDataInfoList = transactionActionDataInfoList;
//			this.GetClientObjectModelByTableId = getClientObjectModelByTableId;
//			this.GetServerObjectPropertyInfoByTableId = getServerObjectPropertyInfoByTableId;
//		}

//		// Client managed properties
//		public IEnumerable<TransactionActionInfo>? TransactionActionDataInfoList { get; private set; }
//		public Func<int, ISimpleObjectModel>? GetClientObjectModelByTableId { get; private set; }
//		public Func<int, ServerObjectModelInfo> GetServerObjectPropertyInfoByTableId { get; private set; }
//		public List<long> TempClientObjectIds { get; private set; }
//		public int NewObjectCount { get; private set; }
//		public List<ObjectPropertyNewObjectIdToSetArgs> SimpleObjectPropertyWithTempObjectIdsNeedToBeChangedToChange { get; private set; }

//		public string InfoMessage { get; private set; }
		
//		//// Server managed properties
//		//public bool RevokeTransaction { get; set; }
//		//public string RevokeReasonDescription { get; set; }
//		//public long[] NewObjectIds { get; private set; }


//		public override void WriteTo(SerializationWriter writer, object? context)
//		{
//			base.WriteTo(writer, context);
			
//			this.TempClientObjectIds = new List<long>();
//			this.SimpleObjectPropertyWithTempObjectIdsNeedToBeChangedToChange = new List<ObjectPropertyNewObjectIdToSetArgs>();
//			writer.WriteInt32Optimized(this.TransactionActionDataInfoList.Count());

//			foreach (TransactionActionInfo transactionActionInfo in this.TransactionActionDataInfoList)
//			{
//				int tableId = transactionActionInfo.TableId;
//				long objectId = transactionActionInfo.ObjectId;

//				if (transactionActionInfo.ActionType == TransactionActionType.Insert)
//				{
//					writer.WriteBoolean(true); // Insert
//					this.TempClientObjectIds.Add(objectId);
//					objectId = -objectId; // The new object has negative Id, and we are writing its value as positive number.
//				}
//				else if (transactionActionInfo.ActionType == TransactionActionType.Update)
//				{
//					writer.WriteBoolean(false); // NOT Insert
//					writer.WriteBoolean(true); // Update
//				}
//				else // (transactionActionInfo.TransactionAction == TransactionActionType.Delete)
//				{
//					writer.WriteBoolean(false); // NOT Insert
//					writer.WriteBoolean(false); // NOT Update => Delete)
//				}

//				writer.WriteInt32Optimized(tableId);
//				writer.WriteInt64Optimized(objectId);

//				if (transactionActionInfo.ActionType != TransactionActionType.Delete)
//					this.WritePropertyValues(writer, transactionActionInfo);
//			}
//		}

//		public override void ReadFrom(SerializationReader reader, object? context)
//		{
//			base.ReadFrom(reader, context);
			
//			int transactionActionCount = reader.ReadInt32Optimized();
//			TransactionActionInfo[] transactionActionInfoList = new TransactionActionInfo[transactionActionCount];
			
//			this.NewObjectCount = 0;

//			for (int i = 0; i < transactionActionCount; i++)
//			{
//				PropertyIndexValues? propertyIndexValues = null;
//				TransactionActionType transactionAction = TransactionActionType.Delete;

//				if (reader.ReadBoolean()) // is Insert?
//				{
//					transactionAction = TransactionActionType.Insert;
//					this.NewObjectCount++;
//				}
//				else if (reader.ReadBoolean()) // Is Update?
//				{
//					transactionAction = TransactionActionType.Update;
//				}

//				int tableId = reader.ReadInt32Optimized();
//				long objectId = reader.ReadInt64Optimized();

//				if (transactionAction == TransactionActionType.Insert)
//					objectId = -objectId;

//				if (transactionAction != TransactionActionType.Delete)
//				{
//					// Read property index values
//					ServerObjectModelInfo objectModel = this.GetServerObjectPropertyInfoByTableId(tableId);
					
//					propertyIndexValues = this.ReadPropertyIndexValues(reader, objectModel);
//				}

//				transactionActionInfoList[i] = new TransactionActionInfo(tableId, objectId, transactionAction, propertyIndexValues, this.GetServerObjectPropertyInfoByTableId);
//			}

//			this.TransactionActionDataInfoList = transactionActionInfoList;
//		}

//		private void WritePropertyValues(SerializationWriter writer, TransactionActionInfo transactionActionInfo)
//		{
//			//ISimpleObjectModel clientObjectModel = this.GetClientObjectModelByTableId(transactionActionInfo.TableId);
//			this.SetCurrentObjectModel(transactionActionInfo.TableId);


//			ServerObjectModelInfo serverObjectModelInfo = this.GetServerObjectPropertyInfoByTableId(transactionActionInfo.TableId);
//			List<int> propertyIndexes = new List<int>();
//			List<object?> propertyValues = new List<object?>();
//			List<ServerPropertyInfo> serverPropertyInfos = new List<ServerPropertyInfo>();

//			if (serverObjectModelInfo != null)
//			{
//				for (int i = 0; i < transactionActionInfo.PropertyIndexes.Count(); i++)
//				{
//					int propertyIndex = transactionActionInfo.PropertyIndexes.ElementAt(i);
//					//IPropertyModel clientPropertyModel = clientObjectModel.GetPropertyModel(propertyIndex);
//					ServerPropertyInfo serverPropertyInfo = serverObjectModelInfo.GetPropertyInfo(propertyIndex);

//					if (serverPropertyInfo != null)
//					{
//						object? propertyValue = transactionActionInfo.PropertyValues.ElementAt(i);
//						int serverPropertyTypeId = serverPropertyInfo.PropertyTypeId;

//						propertyValue = this.GetServerNormalizedPropertyValue(propertyValue, propertyIndex, serverPropertyTypeId);
						
//						//if (serverPropertyTypeId != clientPropertyModel.PropertyTypeId)
//						//	propertyValue = Conversion.TryChangeType(propertyValue, serverPropertyTypeId); //Type serverPropertyType = PropertyTypes.GetPropertyType(serverPropertyTypeId);

//						propertyIndexes.Add(propertyIndex);
//						propertyValues.Add(propertyValue);
//						serverPropertyInfos.Add(serverPropertyInfo);
//					}
//					else
//					{
//						string info = String.Format("Server has no property model, TableId:{0}, ObjectId:{1}, PropertyIndex:{2}, PropertyName:{3}", transactionActionInfo.TableId,
//																																				    transactionActionInfo.ObjectId,
//																																				    propertyIndex, serverPropertyInfo!.PropertyName);
//						this.InfoMessage.WriteLine(info);
//						Debug.WriteLine("ProcessTransactionRequestArgs: " + info);
//					}
//				}
//			}
//			else
//			{
//				string info = String.Format("Server has no object info of TableId:{0}", transactionActionInfo.TableId);

//				this.InfoMessage.WriteLine(info);
//				Debug.WriteLine("ProcessTransactionRequestArgs: " + info);
//			}

//			int propertyCount = propertyValues.Count;

//			writer.WriteInt32Optimized(propertyCount);

//			for (int i = 0; i < propertyCount; i++)
//			{
//				ServerPropertyInfo serverPropertyInfo = serverPropertyInfos.ElementAt(i);
//				int propertyIndex = propertyIndexes.ElementAt(i);
//				object? propertyValue = propertyValues.ElementAt(i);

//				writer.WriteInt32Optimized(propertyIndex);
//				this.WritePropertyValue(writer, transactionActionInfo.TableId, transactionActionInfo.ObjectId, serverPropertyInfo.PropertyIndex, propertyValue,
//												serverPropertyInfo.PropertyTypeId, serverPropertyInfo.IsRelationObjectId, serverPropertyInfo.IsSerializationOptimizable);
//			}
//		}

//		ISimpleObjectModel? currentObjectModel = null;

//		protected virtual void SetCurrentObjectModel(int tableId)
//		{
//			if (this.GetClientObjectModelByTableId != null)
//				this.currentObjectModel = this.GetClientObjectModelByTableId(tableId);
//		}

//		protected virtual object? GetServerNormalizedPropertyValue(object? value, in int propertyIndex, in int serverPropertyTypeId)
//		{
//			if (this.currentObjectModel is null)
//				return value;
			
//			IPropertyModel clientPropertyModel = this.currentObjectModel.GetPropertyModel(propertyIndex);
//			object? result = value;

//			if (serverPropertyTypeId != clientPropertyModel.PropertyTypeId)
//				result = Conversion.TryChangeType(value, serverPropertyTypeId); //Type serverPropertyType = PropertyTypes.GetPropertyType(serverPropertyTypeId);

//			return result;
//		}

//		private PropertyIndexValues ReadPropertyIndexValues(SerializationReader reader, ServerObjectModelInfo objectModel)
//		{
//			int propertyCount = reader.ReadInt32Optimized();
//			int[] propertyIndexes = new int[propertyCount];
//			object?[] propertyValues = new object[propertyCount];

//			for (int i = 0; i < propertyCount; i++)
//			{
//				int propertyIndex = reader.ReadInt32Optimized();
//				ServerPropertyInfo serverPropertyInfo = objectModel.GetPropertyInfo(propertyIndex);
//				object? propertyValue = this.ReadPropertyValue(reader, serverPropertyInfo.PropertyTypeId, serverPropertyInfo.IsRelationObjectId, serverPropertyInfo.IsSerializationOptimizable);

//				propertyIndexes[i] = propertyIndex;
//				propertyValues[i] = propertyValue;
//			}

//			return new PropertyIndexValues(propertyIndexes, propertyValues);
//		}


//		private void WritePropertyValue(SerializationWriter writer, int tableId, long objectId, int propertyIndex, object? propertyValue, int propertyTypeId,
//																	bool isRelationObjectId, bool isSerializactionOptimizable)
//		{
//			if (isRelationObjectId)
//			{
//				long relationKeyValue = (long)propertyValue!;
//				bool isNegative = (relationKeyValue < 0);

//				if (isNegative)
//				{
//					this.SimpleObjectPropertyWithTempObjectIdsNeedToBeChangedToChange.Add(new ObjectPropertyNewObjectIdToSetArgs(tableId, objectId, propertyIndex)); //, relationKeyValue));
//					relationKeyValue = -relationKeyValue;
//				}

//				writer.WriteInt64Optimized(relationKeyValue);
//				writer.WriteBoolean(isNegative);
//			}
//			else
//			{
//				if (isSerializactionOptimizable)
//					writer.WriteOptimized(propertyTypeId, propertyValue);
//				else
//					writer.Write(propertyTypeId, propertyValue);
//			}
//		}

//		private object? ReadPropertyValue(SerializationReader reader, int propertyTypeId, bool isRelationObjectId, bool isSerializationOptimizable)
//		{ 
//			object? propertyValue;

//			if (isRelationObjectId) // New objects have negative Id
//			{
//				propertyValue = reader.ReadInt64Optimized();

//				if (reader.ReadBoolean()) // Is negative
//					propertyValue = -(long)propertyValue; // this.NewObjectIdsByTableId[-(long)propertyValue];
//			}
//			else
//			{
//				if (isSerializationOptimizable)
//					propertyValue = reader.ReadOptimized(propertyTypeId);
//				else
//					propertyValue = reader.Read(propertyTypeId);
//			}

//			return propertyValue;
//		}
//	}
//}
