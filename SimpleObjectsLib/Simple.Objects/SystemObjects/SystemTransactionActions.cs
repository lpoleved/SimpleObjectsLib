using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Collections;
using Simple.Serialization;
using Simple.Modeling;

namespace Simple.Objects
{
	public sealed class SystemTransactionAction : SystemObject<long, SystemTransactionAction>
    {
		//private List<DatastoreActionInfo> datastoreActions = new List<DatastoreActionInfo>();

		static SystemTransactionAction()
		{
			Model.TableInfo = SystemTables.SystemTransactionActions;
			Model.AutoGenerateKey = true;
		}

		public SystemTransactionAction()
		{
		}

		public SystemTransactionAction(Func<int, ServerObjectModelInfo> getSimpleObjectModelByTableId)
		{
			this.GetSimpleObjectModelByTableId = getSimpleObjectModelByTableId;
		}

		public SystemTransactionAction(SimpleObjectManager objectManager, ref SystemObjectCollectionByObjectKey<long, SystemTransactionAction> dictionaryCollection, 
									   long transactionId, int simpleObjectTableId, long simpleObjectId, TransactionActionType actionType,
									   IEnumerable<PropertyIndexValuePair> propertyIndexValues, Func<int, ServerObjectModelInfo> getSimpleObjectModelByTableId)
			: base(objectManager, ref dictionaryCollection, 
				  (item) =>
				  {
					  item.TransactionId = transactionId;
					  item.SimpleObjectTableId = simpleObjectTableId;
					  item.SimpleObjectId = simpleObjectId;
					  item.ActionType = actionType;
					  item.PropertyIndexValues = propertyIndexValues;
					  item.GetSimpleObjectModelByTableId = getSimpleObjectModelByTableId;
				  })
		{
		}

		[ObjectKey]
		public long TransactionActionId { get; private set; }

		public long TransactionId { get; private set; }
		public int SimpleObjectTableId { get; private set; }
		public long SimpleObjectId { get; private set; }
		public TransactionActionType ActionType { get; private set; }
		public byte[]? PropertyValueData { get; internal set; }

		[NonStorable]
		public IEnumerable<PropertyIndexValuePair>? PropertyIndexValues { get; private set; }
		
		[NonStorable]
		public Func<int, ServerObjectModelInfo>? GetSimpleObjectModelByTableId { get; private set; }

		//public void WriteToPropertyValueData()
		//{
		//	BufferSequenceWriter bufferSequenceWriter = new BufferSequenceWriter();
		//	SerialWriter writer = new SerialWriter(bufferSequenceWriter);
		//	ServerObjectModelInfo? objectModel = (this.GetSimpleObjectModelByTableId != null) ? this.GetSimpleObjectModelByTableId(this.SimpleObjectTableId) : null;

		//	if (objectModel != null && this.PropertyIndexValues != null)
		//		WriteTo(writer, this.ActionType, this.PropertyIndexValues, objectModel);

		//	this.PropertyValueData = bufferSequenceWriter.ToArray();
		//}

		public static void WriteTo(ref SequenceWriter writer, TransactionActionType actionType, IEnumerable<PropertyIndexValuePair>? propertyIndexValues, ServerObjectModelInfo objectModel)
		{
			if (actionType != TransactionActionType.Insert)
			{
				if (propertyIndexValues != null)
				{
					writer.WriteInt32Optimized(propertyIndexValues.Count());

					for (int i = 0; i < propertyIndexValues.Count(); i++)
					{
						var item = propertyIndexValues.ElementAt(i);
						ServerPropertyInfo propertyModel = objectModel[item.PropertyIndex];

						writer.WriteInt32Optimized(propertyModel.PropertyIndex);
						//writer.WriteInt32Optimized(propertyModel.PropertyTypeId);
						//writer.WriteBoolean(propertyModel.IsSerializationOptimizable);

						if (propertyModel.IsSerializationOptimizable)
							writer.WriteOptimized(propertyModel.PropertyTypeId, item.PropertyValue);
						else
							writer.Write(propertyModel.PropertyTypeId, item.PropertyValue);
					}
				}
				else
				{
					writer.WriteInt32Optimized(0);
				}
			}
		}

		//public void ReadFromPropertyValueData()
		//{
		//	ServerObjectModelInfo? objectModel = (this.GetSimpleObjectModelByTableId != null) ? this.GetSimpleObjectModelByTableId(this.SimpleObjectTableId) : null;
		//	//var bufferSegment = new BufferSegment<byte>(this.PropertyValueData);
		//	//BufferSequenceReader bufferSequenceReader = new BufferSequenceReader(bufferSegment);

		//	if (objectModel != null && this.PropertyValueData != null)
		//		this.PropertyIndexValues = ReadFrom(new SerialReader(this.PropertyValueData), this.ActionType, objectModel);
		//}

		public static IEnumerable<PropertyIndexValuePair>? ReadFrom(ref SequenceReader reader, TransactionActionType actionType, ServerObjectModelInfo objectModel)
		{
			IEnumerable<PropertyIndexValuePair>? result = null;

			if (actionType != TransactionActionType.Insert)
			{
				int propertyCount = reader.ReadInt32Optimized();
				PropertyIndexValuePair[] propertyIndexValues = new PropertyIndexValuePair[propertyCount];

				for (int i = 0; i < propertyCount; i++)
				{
					int propertyIndex = reader.ReadInt32Optimized();
					ServerPropertyInfo propertyModel = objectModel[propertyIndex];
					//int propertyTypeId = reader.ReadInt32Optimized();
					//bool isSerializationOptimizable = reader.ReadBoolean();
					object? propertyValue = (propertyModel.IsSerializationOptimizable) ? reader.ReadOptimized(propertyModel.PropertyTypeId) : 
																						 reader.Read(propertyModel.PropertyTypeId);

					propertyIndexValues[i] = new PropertyIndexValuePair(propertyIndex, propertyValue);
				}

				result = propertyIndexValues;
			}

			return result;
		}
	}
}
