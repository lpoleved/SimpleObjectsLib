using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Simple;
using Simple.Serialization;
using Simple.Modeling;
using Simple.Objects;
using Simple.SocketEngine;

namespace Simple.Objects.SocketProtocol
{
	[SystemRequestArgs((int)SystemRequest.TransactionRequest)]

	public class ProcessTransactionRequestArgs : RequestArgs
	{
        public ProcessTransactionRequestArgs()
        {
        }

        //public ProcessTransactionRequestArgs(Func<int, ServerObjectModelInfo> getServerObjectPropertyInfoByTableId)
        //{
        //	this.GetServerObjectPropertyInfoByTableId = getServerObjectPropertyInfoByTableId;
        //}

        ///// <summary>
        ///// Initialize ProcessTransactionRequestArgs class. Intended to be called from server.
        ///// </summary>
        //public ProcessTransactionRequestArgs(Func<int, ISimpleObjectModel> getClientObjectModelByTableId, Func<int, ServerObjectModelInfo> getServerObjectPropertyInfoByTableId)
        //{
        //	this.GetClientObjectModelByTableId = getClientObjectModelByTableId;
        //	this.GetServerObjectPropertyInfoByTableId = getServerObjectPropertyInfoByTableId;
        //}

        /// <summary>
        /// Initialize ProcessTransactionRequestArgs class. Intended to be called from client.
        /// </summary>
        /// <param name="transactionActionInfoList">TransactionActionInfoList that should be geven by ChangeContainer in sorted order by Insert, Update and Delete.</param>
        /// <param name="getServerObjectPropertyInfoByTableId">GetServerObjectPropertyInfoByTableId param</param>
        public ProcessTransactionRequestArgs(IEnumerable<TransactionActionInfo> transactionActionInfoList) //, Func<int, ISimpleObjectModel> getClientObjectModelByTableId, Func<int, ServerObjectModelInfo> getServerObjectPropertyInfoByTableId)
		{
			this.TransactionActionInfoList = transactionActionInfoList;
			//this.GetClientObjectModelByTableId = getClientObjectModelByTableId;
			//this.GetServerObjectPropertyInfoByTableId = getServerObjectPropertyInfoByTableId;
		}

		// Client managed properties
		public IEnumerable<TransactionActionInfo>? TransactionActionInfoList { get; private set; }
		//public Func<int, ISimpleObjectModel>? GetClientObjectModelByTableId { get; private set; }
		//public Func<int, ServerObjectModelInfo> GetServerObjectPropertyInfoByTableId { get; private set; }
		
		/// <summary>
		/// When writing all client negative new object id's are collected in a list.
		/// </summary>
		public List<long>? TempClientObjectIds { get; private set; }
		
		/// <summary>
		/// When writing all object property as relation keys that has temp negative values that need to be replaced with permanent positive values geven from the server is collected.
		/// </summary>
		public List<NewPropertyObjectIdNeedToBeSet>? SimpleObjectPropertyWithTempObjectIdsNeedsToBeChangedToPositives { get; private set; }
		
		/// <summary>
		/// When reading, this is count of inserted new objecta needs to change temp negative Id's to permamnet positive value given from the server.
		/// </summary>
		public int NewObjectCount { get; private set; }

		//// Server managed properties
		//public bool RevokeTransaction { get; set; }
		//public string RevokeReasonDescription { get; set; }
		//public long[] NewObjectIds { get; private set; }

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);

			if (session is ISimpleObjectSession simpleObjectSession)
			{
				this.TempClientObjectIds = new List<long>();
				this.SimpleObjectPropertyWithTempObjectIdsNeedsToBeChangedToPositives = new List<NewPropertyObjectIdNeedToBeSet>();
				writer.WriteInt32Optimized(this.TransactionActionInfoList!.Count());

				foreach (TransactionActionInfo transactionActionInfo in this.TransactionActionInfoList!)
				{
					int tableId = transactionActionInfo.TableId;
					long objectId = transactionActionInfo.ObjectId;

					if (transactionActionInfo.ActionType == TransactionActionType.Insert)
					{
						writer.WriteBoolean(true); // Insert
						this.TempClientObjectIds.Add(objectId);
						objectId = -objectId; // The new object has negative Id, and we are writing its value as positive number.
					}
					else if (transactionActionInfo.ActionType == TransactionActionType.Update)
					{
						writer.WriteBoolean(false); // NOT Insert
						writer.WriteBoolean(true); // Update
					}
					else // (transactionActionInfo.TransactionAction == TransactionActionType.Delete)
					{
						writer.WriteBoolean(false); // NOT Insert
						writer.WriteBoolean(false); // NOT Update => Delete)
					}

					writer.WriteInt32Optimized(tableId);
					writer.WriteInt64Optimized(objectId);

					if (transactionActionInfo.ActionType != TransactionActionType.Delete)
						this.WritePropertyIndexValues(ref writer, transactionActionInfo, simpleObjectSession);
				}
			}
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);

			if (session is ISimpleObjectSession simpleObjectSession)
			{
				int transactionActionCount = reader.ReadInt32Optimized();
				TransactionActionInfo[] transactionActionInfoList = new TransactionActionInfo[transactionActionCount];

				this.NewObjectCount = 0;

				for (int i = 0; i < transactionActionCount; i++)
				{
					PropertyIndexValuePair[]? propertyIndexValues = null;
					TransactionActionType transactionAction = TransactionActionType.Delete;

					if (reader.ReadBoolean()) // is Insert?
					{
						transactionAction = TransactionActionType.Insert;
						this.NewObjectCount++;
					}
					else if (reader.ReadBoolean()) // Is Update?
					{
						transactionAction = TransactionActionType.Update;
					}

					int tableId = reader.ReadInt32Optimized();
					long objectId = reader.ReadInt64Optimized();

					if (transactionAction == TransactionActionType.Insert)
						objectId = -objectId;

					if (transactionAction != TransactionActionType.Delete)
					{
						ServerObjectModelInfo? serverPropertyInfo = simpleObjectSession.GetServerObjectModel(tableId);

						if (serverPropertyInfo != null)
						{
							propertyIndexValues = this.ReadPropertyIndexValues(ref reader, serverPropertyInfo);
						}
						else
						{
							string info = $"Server has no property model, TableId:{tableId}, ObjectId:{objectId}";

							this.ErrorMessage.WriteLine(info);
							Debug.WriteLine("ProcessTransactionRequestArgs: " + info);
						}
					}

					transactionActionInfoList[i] = new TransactionActionInfo(tableId, objectId, transactionAction, propertyIndexValues);
				}

				this.TransactionActionInfoList = transactionActionInfoList;
			}
		}

		private void WritePropertyIndexValues(ref SequenceWriter writer, TransactionActionInfo transactionActionInfo, ISimpleObjectSession session)
		{
			//ISimpleObjectModel clientObjectModel = this.GetClientObjectModelByTableId(transactionActionInfo.TableId);
			//this.SetCurrentObjectModel(transactionActionInfo.TableId);

			ServerObjectModelInfo? serverObjectModelInfo = session.GetServerObjectModel(transactionActionInfo.TableId);

			if (serverObjectModelInfo != null && transactionActionInfo.PropertyIndexValues != null)
			{
				int propertyCount = transactionActionInfo.PropertyIndexValues.Count();

				writer.WriteInt32Optimized(propertyCount);

				for (int i = 0; i < propertyCount; i++)
				{
					var item = transactionActionInfo.PropertyIndexValues.ElementAt(i);
					//IPropertyModel clientPropertyModel = clientObjectModel.GetPropertyModel(propertyIndex);
					ServerPropertyInfo serverPropertyInfo = serverObjectModelInfo.GetPropertyInfo(item.PropertyIndex);

					if (serverPropertyInfo != null)
					{
						//object? propertyValue = transactionActionInfo.PropertyIndexValues.ElementAt(i);
						//int serverPropertyTypeId = serverPropertyInfo.PropertyTypeId;
						//propertyValue = this.GetServerNormalizedPropertyValue(propertyValue, item.PropertyIndex, serverPropertyTypeId);

						writer.WriteInt32Optimized(item.PropertyIndex);
						this.WritePropertyValue(ref writer, transactionActionInfo.TableId, transactionActionInfo.ObjectId, serverPropertyInfo.PropertyIndex, item.PropertyValue,
															serverPropertyInfo.PropertyTypeId, serverPropertyInfo.IsRelationObjectId, serverPropertyInfo.IsSerializationOptimizable);
					}
					else
					{
						string info = $"Server has no property model, TableId:{transactionActionInfo.TableId}, ObjectId:{transactionActionInfo.ObjectId}, " +
									  $"PropertyIndex:{item.PropertyIndex}, PropertyName:{serverPropertyInfo?.PropertyName}";
						
						this.ErrorMessage.WriteLine(info);
						Debug.WriteLine("ProcessTransactionRequestArgs: " + info);
					}
				}
			}
			else
			{
				string info = String.Format("Server has no object info of TableId:{0}", transactionActionInfo.TableId);

				this.ErrorMessage.WriteLine(info);
				Debug.WriteLine("ProcessTransactionRequestArgs: " + info);
			}
		}

		//ISimpleObjectModel? currentObjectModel = null;

		//protected virtual void SetCurrentObjectModel(int tableId)
		//{
		//	if (this.GetClientObjectModelByTableId != null)
		//		this.currentObjectModel = this.GetClientObjectModelByTableId(tableId);
		//}

		//protected virtual object? GetServerNormalizedPropertyValue(object? value, in int propertyIndex, in int serverPropertyTypeId)
		//{
		//	if (this.currentObjectModel is null)
		//		return value;
			
		//	IPropertyModel clientPropertyModel = this.currentObjectModel.GetPropertyModel(propertyIndex);
		//	object? result = value;

		//	if (serverPropertyTypeId != clientPropertyModel.PropertyTypeId)
		//		result = Conversion.TryChangeType(value, serverPropertyTypeId); //Type serverPropertyType = PropertyTypes.GetPropertyType(serverPropertyTypeId);

		//	return result;
		//}

		private PropertyIndexValuePair[] ReadPropertyIndexValues(ref SequenceReader reader, ServerObjectModelInfo serverObjectModel)
		{
			int propertyCount = reader.ReadInt32Optimized();
			PropertyIndexValuePair[] propertyIndexValues = new PropertyIndexValuePair[propertyCount];

			for (int i = 0; i < propertyCount; i++)
			{
				int propertyIndex = reader.ReadInt32Optimized();
				ServerPropertyInfo serverPropertyInfo = serverObjectModel.GetPropertyInfo(propertyIndex);
				object? propertyValue = this.ReadPropertyValue(ref reader, serverPropertyInfo.PropertyTypeId, serverPropertyInfo.IsRelationObjectId, serverPropertyInfo.IsSerializationOptimizable);

				propertyIndexValues[i] = new PropertyIndexValuePair(propertyIndex, propertyValue); 
			}

			return propertyIndexValues;
		}


		private void WritePropertyValue(ref SequenceWriter writer, int tableId, long objectId, int propertyIndex, object? propertyValue, int propertyTypeId,
																   bool isRelationObjectId, bool isSerializactionOptimizable)
		{
			if (isRelationObjectId)
			{
				long relationKeyValue = (long)propertyValue!;
				bool isNegative = (relationKeyValue < 0);

				if (isNegative)
				{
					this.SimpleObjectPropertyWithTempObjectIdsNeedsToBeChangedToPositives?.Add(new NewPropertyObjectIdNeedToBeSet(tableId, objectId, propertyIndex)); //, relationKeyValue));
					relationKeyValue = -relationKeyValue;
				}

				writer.WriteInt64Optimized(relationKeyValue);
				writer.WriteBoolean(isNegative);
			}
			else
			{
				if (isSerializactionOptimizable)
					writer.WriteOptimized(propertyTypeId, propertyValue);
				else
					writer.Write(propertyTypeId, propertyValue);
			}
		}

		private object? ReadPropertyValue(ref SequenceReader reader, int propertyTypeId, bool isRelationObjectId, bool isSerializationOptimizable)
		{ 
			object? propertyValue;

			if (isRelationObjectId) // New objects have negative Id
			{
				propertyValue = reader.ReadInt64Optimized();

				if (reader.ReadBoolean()) // Is negative
					propertyValue = -(long)propertyValue; // this.NewObjectIdsByTableId[-(long)propertyValue];
			}
			else
			{
				if (isSerializationOptimizable)
					propertyValue = reader.ReadOptimized(propertyTypeId);
				else
					propertyValue = reader.Read(propertyTypeId);
			}

			return propertyValue;
		}
	}
}
