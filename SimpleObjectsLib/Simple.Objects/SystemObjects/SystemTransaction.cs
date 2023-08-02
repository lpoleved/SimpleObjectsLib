using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Collections;
using Simple.Serialization;
using Simple.Modeling;
using Simple.Compression;

namespace Simple.Objects
{
	public sealed class SystemTransaction : SystemObject<long, SystemTransaction>
    {
		static SystemTransaction()
		{
			Model.TableInfo = SystemTables.SystemTransactions;
			Model.AutoGenerateKey = true;
			Model.ReuseObjectKeys = false;
		}

		public SystemTransaction()
		{
		}

		public SystemTransaction(SimpleObjectManager objectManager, ref SystemObjectCollectionByObjectKey<long, SystemTransaction> dictionaryCollection, 
								 long userId, Encoding characterEncoding, long clientId, int serverId, IEnumerable<TransactionActionInfo> rollbackTransactionActions, TransactionStatus status)
			: base(objectManager, ref dictionaryCollection, 
				  (item) =>
				  {
					  item.UserId = userId;
					  item.CodePage = characterEncoding.CodePage;
					  item.ClientId = clientId;
					  item.ServerId = serverId;
					  item.CreationTime = DateTime.Now;
					  item.RollbackTransactionActions = rollbackTransactionActions;
					  item.Status = status;
					  item.RollbackActionData = null;
					  //item.TransactionActionsBySimpleObject = new Dictionary<SimpleObject, DatastoreTransactionAction>();
				  })
		{
			this.IsRollbackActionDataCompressed = this.ObjectManager?.UseCompressionForTransactionLogActionData ?? false;
			this.RollbackActionData = this.CreateRollbackActionData(rollbackTransactionActions);
		}

		protected override void OnLoad()
		{
			base.OnLoad();
			this.RollbackTransactionActions = this.CreateRollbackTransactionActionsFromActionData();
		}

		[ObjectKey]
		public long TransactionId { get; private set; }

		public long UserId { get; private set; }
		public long ClientId { get; private set; }
		public int ServerId { get; private set; }
		public TransactionStatus Status { get; internal set; }
		public DateTime CreationTime { get; private set; }

		/// <summary>
		/// Used for serialized TransactionActionLogs.
		/// TODO: Check if this data is for rollback action: Change the property name to RollbackActionData
		/// </summary>
		public byte[]? RollbackActionData { get; private set; }
		
		public bool IsRollbackActionDataCompressed { get; private set; }

		public int CodePage { get; private set; }

		[NonStorable]
		public IEnumerable<TransactionActionInfo>? RollbackTransactionActions { get; private set; }

		[NonStorable]
		public float CompressionRatio { get; private set; }


		public static void WriteTo(ref SequenceWriter writer, IEnumerable<TransactionActionInfo> transactionActions, Func<int, ServerObjectModelInfo?> getServerObjectModel)
		{
			//writer.WriteInt32Optimized(transactionActions.Count());

			//foreach (TransactionActionInfo transactionActionInfo in transactionActions)
			//{
			//	int tableId = transactionActionInfo.TableId;
			//	long objectId = transactionActionInfo.ObjectId;
			//	ServerObjectModelInfo objectModel = getServerObjectModel(tableId);

			//	if (transactionActionInfo.ActionType == TransactionActionType.Insert)
			//	{
			//		writer.WriteBoolean(true); // Insert
			//	}
			//	else
			//	{
			//		writer.WriteBoolean(false); // NOT Insert
			//		writer.WriteBoolean(transactionActionInfo.ActionType == TransactionActionType.Update); // true for Update, false for Delete
			//	}

			//	writer.WriteInt32Optimized(tableId);
			//	writer.WriteInt64Optimized(objectId);

			//	SystemTransactionAction.WriteTo(ref writer, transactionActionInfo.ActionType, transactionActionInfo.PropertyIndexValues, objectModel);
			//}

			//return;

			// TODO: Is this a same thing.....
			// Add this as extenson method for IEnumerable<TransactionActionInfo> (WriteTo and ReadFrom)
			//
			writer.WriteInt32Optimized(transactionActions.Count());

			foreach (TransactionActionInfo item in transactionActions)
				item.WriteTo(ref writer, getServerObjectModel(item.TableId)!);
		}

		public static IEnumerable<TransactionActionInfo> ReadFrom(ref SequenceReader reader, Func<int, ServerObjectModelInfo?> getServerObjectModel) //Func<int, ISimpleObjectModel> getObjectModel)
		{
			//int transactionActionCount = reader.ReadInt32Optimized();
			//TransactionActionInfo[] transactionActions = new TransactionActionInfo[transactionActionCount];

			//for (int i = 0; i < transactionActionCount; i++)
			//{
			//	TransactionActionType actionType = TransactionActionType.Delete;

			//	if (reader.ReadBoolean()) // is Insert?
			//		actionType = TransactionActionType.Insert;
			//	else if (reader.ReadBoolean()) // is Update?
			//		actionType = TransactionActionType.Update;

			//	int tableId = reader.ReadInt32Optimized();
			//	long objectId = reader.ReadInt64Optimized();
			//	ServerObjectModelInfo? objectModel = getServerObjectModel(tableId);
			//	IEnumerable<PropertyIndexValuePair>? propertyIndexValues = SystemTransactionAction.ReadFrom(ref reader, actionType, objectModel);

			//	transactionActions[i] = new TransactionActionInfo(tableId, objectId, actionType, propertyIndexValues);
			//}

			//return transactionActions;


			// TODO: Is this a same thing.....
			int count = reader.ReadInt32Optimized();
			TransactionActionInfo[] result = new TransactionActionInfo[count];

			for (int i = 0; i < count; i++)
			{
				TransactionActionInfo item = new TransactionActionInfo();

				item.ReadFrom(ref reader, getServerObjectModel);
				result[i] = item;
			}

			return result;
		}


		public static byte[] Compress(byte[] actionData) => MiniLZOSafe.Compress(actionData);

		public static byte[] Decompress(byte[] actionData) => MiniLZOSafe.Decompress(actionData);

		//private void WritePropertyValues(SerializationWriter writer, TransactionActionWithDataInfo transactionActionInfo, ISimpleObjectModel objectModel)
		//{
		//	int propertyCount = transactionActionInfo.PropertyIndexValues.Length;

		//	writer.WriteInt32Optimized(propertyCount);

		//	for (int i = 0; i < propertyCount; i++)
		//	{
		//		int propertyIndex = transactionActionInfo.PropertyIndexValues.PropertyIndexes[i];
		//		object propertyValue = transactionActionInfo.PropertyIndexValues.PropertyValues[i];
		//		IPropertyModel propertyModel = objectModel.PropertyModels.GetPropertyModel(propertyIndex);

		//		writer.WriteInt32Optimized(propertyIndex);

		//		if (propertyModel.IsSerializationOptimizable)
		//		{
		//			writer.WriteOptimized(propertyModel.PropertyTypeId, propertyValue);
		//		}
		//		else
		//		{
		//			writer.Write(propertyModel.PropertyTypeId, propertyValue);
		//		}
		//	}
		//}

		private byte[] CreateRollbackActionData(IEnumerable<TransactionActionInfo> rollbackTransactionActions)
		{
			//BufferSequenceWriter bufferSequenceWriter = new BufferSequenceWriter();
			Span<byte> span = stackalloc byte[256];
			SequenceWriter writer = new SequenceWriter(span, Encoding.GetEncoding(this.CodePage));

			WriteTo(ref writer, rollbackTransactionActions, this.ObjectManager!.GetServerObjectModel!);

			var actionData = writer.ToArray();

			this.IsRollbackActionDataCompressed = this.ObjectManager.UseCompressionForTransactionLogActionData;

			if (this.IsRollbackActionDataCompressed)
			{
				int uncompressedLength = actionData.Length;

				actionData = Compress(actionData);
				this.CompressionRatio = 100 - ((float)actionData.Length / (float)uncompressedLength) * 100;
			}
			else
			{
				this.CompressionRatio = 0;
			}

			return actionData;
		}


		private IEnumerable<TransactionActionInfo> CreateRollbackTransactionActionsFromActionData()
		{
			IEnumerable<TransactionActionInfo> result;

			if (this.RollbackActionData != null && this.ObjectManager != null)
			{
				if (this.IsRollbackActionDataCompressed && this.RollbackActionData != null)
					this.RollbackActionData = Decompress(this.RollbackActionData);

				SequenceReader reader = new SequenceReader(this.RollbackActionData!);

				result = ReadFrom(ref reader, this.ObjectManager.GetServerObjectModel);
			}
			else
			{
				result = new TransactionActionInfo[0];
			}

			return result;
		}


		//public void CreateTransactionActionLogsFromTransactionRequests()
		//{
		//	List<TransactionActionLog> result = new List<TransactionActionLog>(this.TransactionRequestsBySimpleObject.Count);

		//	foreach (var item in this.TransactionRequestsBySimpleObject)
		//	{
		//		SimpleObject simpleObject = item.Key;
		//		TransactionRequestAction requestAction = item.Value;
		//		TransactionActionLog transactionActionLog;

		//		////if (!simpleObject.GetModel().IsStorable || (simpleObject is GraphElement && !(simpleObject as GraphElement).SimpleObject.GetModel().IsStorable))
		//		//if (!simpleObject.IsStorable) // || (simpleObject is GraphElement && !(simpleObject as GraphElement).SimpleObject.GetModel().IsStorable))
		//		//	continue;

		//		if (requestAction == TransactionRequestAction.Save)
		//		{
		//			if (simpleObject.IsNew) // Insert
		//			{
		//				transactionActionLog = new TransactionActionLog(this, this.ObjectManager.GetObjectModel, this.ObjectManager.GetSystemPropertySequence);
		//				transactionActionLog.SetActionInsert(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);
		//				result.Add(transactionActionLog);
		//			}
		//			else if (simpleObject.RequireSaving()) // Update
		//			{
		//				PropertyValueSequence oldPropertyValueSequence = simpleObject.GetChangedStorableActionLogOldPropertyValueSequence(this.ObjectManager.NormalizeForWritingByPropertyType);

		//				if (oldPropertyValueSequence.Length > 0)
		//				{
		//					transactionActionLog = new TransactionActionLog(this, this.ObjectManager.GetObjectModel, this.ObjectManager.GetSystemPropertySequence);
		//					transactionActionLog.SetActionUpdate(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id, oldPropertyValueSequence);
		//					result.Add(transactionActionLog);
		//				}
		//			}
		//		}
		//		else if (!simpleObject.IsNew) // && !simpleObject.DeleteStarted) // Delete
		//		{
		//			PropertyValueSequence propertyValueSequence = simpleObject.GetStorableActionLogPropertyValueSequence(this.ObjectManager.NormalizeForWritingByPropertyType);
		//			transactionActionLog = new TransactionActionLog(this, this.ObjectManager.GetObjectModel, this.ObjectManager.GetSystemPropertySequence);

		//			transactionActionLog.SetActionDelete(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id, propertyValueSequence);
		//			result.Add(transactionActionLog);
		//		}
		//	}

		//	this.TransactionActionLogs = result;
		//}

		//public void CreateActionDataFromTransactionActionLogs()
		//{
		//	byte[] uncompressedActionData = SerializeTransactionActionLogs(this.TransactionActionLogs);

		//	if (this.ObjectManager.UseCompressionForTransactionLogActionData)
		//	{
		//		this.ActionData = MiniLZOSafe.Compress(uncompressedActionData);
		//		this.CompressionRatio = 100 - ((float)this.ActionData.Length / (float)uncompressedActionData.Length) * 100;
		//	}
		//	else
		//	{
		//		this.ActionData = uncompressedActionData;
		//		this.CompressionRatio = 0;
		//	}
		//}

		//public void CreateTransactionActionLogsFromActionData(Func<int, ISimpleObjectModel> getObjectModelByTableId, Func<int, IServerPropertySequence> getPropertySequenceBySequenceId)
		//{
		//	byte[] decompressedActionData = (this.ObjectManager.UseCompressionForTransactionLogActionData) ? MiniLZOSafe.Decompress(this.ActionData) : this.ActionData;
		//	SerializationReader reader = new SerializationReader(decompressedActionData);

		//	this.TransactionActionLogs = DeserializeTransactionActionLogs(reader, getObjectModelByTableId, getPropertySequenceBySequenceId);
		//}

		//public void CreateDatastoreActionsFromTransactionRequests(Func<int, ISimpleObjectModel> getObjectModelByTableId, Func<int, ServerObjectModelInfo> getServerObjectPropertyInfoByTableId, Func<IPropertyModel, object, object> propertyValueNormalizer)
		//{
		//	this.DatastoreActions = new List<DatastoreAction>(this.TransactionRequestsBySimpleObject.Count);

		//	foreach (KeyValuePair<SimpleObject, TransactionRequestAction> item in this.TransactionRequestsBySimpleObject)
		//	{
		//		SimpleObject simpleObject = item.Key;
		//		TransactionRequestAction transactionAction = item.Value;
		//		ISimpleObjectModel objectModel = simpleObject.GetModel();
		//		DatastoreAction datastoreAction;

		//		//if (!simpleObject.GetModel().IsStorable || (simpleObject is GraphElement && !(simpleObject as GraphElement).SimpleObject.GetModel().IsStorable))
		//		if (!simpleObject.IsStorable) // || (simpleObject is GraphElement && !(simpleObject as GraphElement).SimpleObject.GetModel().IsStorable))
		//			continue;

		//		datastoreAction = new DatastoreAction(getObjectModelByTableId, getServerObjectPropertyInfoByTableId);
				
		//		switch (transactionAction)
		//		{
		//			case TransactionRequestAction.Save:
						
		//				if (simpleObject.IsNew)
		//				{
		//					PropertyIndexValues propertyValues = simpleObject.GetStorablePropertyIndexValues(propertyValueNormalizer);
							
		//					datastoreAction.SetActionInsert(objectModel.TableInfo.TableId, simpleObject.Id, propertyValues);
		//				}
		//				else // Update
		//				{
		//					PropertyIndexValues propertyValues = simpleObject.GetChangedSaveablePropertyIndexValues(propertyValueNormalizer);
							
		//					if (propertyValues.Length > 0)
		//						datastoreAction.SetActionUpdate(objectModel.TableInfo.TableId, simpleObject.Id, propertyValues);
		//				}

		//				break;

		//			case TransactionRequestAction.Delete:

		//				datastoreAction.SetActionDelete(objectModel.TableInfo.TableId, simpleObject.Id);

		//				break;
		//		}

		//		this.DatastoreActions.Add(datastoreAction);
		//	}
		//}

		//public static byte[] SerializeTransactionActionLogs(IEnumerable<TransactionActionLog> transactionActionLogs)
		//{
		//	SerializationWriter writer = new SerializationWriter();

		//	SerializeTransactionActionLogs(writer, transactionActionLogs);

		//	return writer.ToArray();
		//}

		//public static void SerializeTransactionActionLogs(SerializationWriter writer, IEnumerable<TransactionActionLog> transactionActionLogs)
		//{
		//	SimpleWriter.WriteNBitEncodedSignedInt32(writer, 3, transactionActionLogs.Count());

		//	foreach (TransactionActionLog transactionActionLog in transactionActionLogs)
		//		transactionActionLog.WriteTo(writer);
		//}

		//public static TransactionActionLog[] DeserializeTransactionActionLogs(SerializationReader reader, Func<int, ISimpleObjectModel> getObjectModelByTableId, Func<int, IServerPropertySequence> getPropertySequenceBySequenceId)
		//{
		//	int count = SimpleReader.ReadNBitEncodedSignedInt32(reader, 3);
		//	TransactionActionLog[] result = new TransactionActionLog[count];

		//	for (int i = 0; i < count; i++)
		//	{
		//		TransactionActionLog transactionActionLog = new TransactionActionLog(null, getObjectModelByTableId, getPropertySequenceBySequenceId);

		//		transactionActionLog.ReadFrom(reader);
		//		result[i] = transactionActionLog;
		//	}

		//	return result;
		//}

		//public static void SerializeDatastoreActions(SerializationWriter writer, IEnumerable<DatastoreAction> datastoreActions)
		//{
		//	writer.WriteInt32Optimized(datastoreActions.Count());

		//	foreach (DatastoreAction datastoreAction in datastoreActions)
		//		datastoreAction.WriteTo(writer);
		//}

		//public static IEnumerable<DatastoreAction> DeserializeDatastoreActions(SerializationReader reader, Func<int, ISimpleObjectModel> getObjectModelByTableId, Func<int, ServerObjectModelInfo> getServerObjectModelInfoByTableId)
		//{
		//	int count = reader.ReadInt32Optimized();
		//	DatastoreAction[] result = new DatastoreAction[count];

		//	for (int i = 0; i < count; i++)
		//	{
		//		DatastoreAction datastoreAction = new DatastoreAction(getObjectModelByTableId, getServerObjectModelInfoByTableId);

		//		datastoreAction.ReadFrom(reader);
		//		result[i] = datastoreAction;
		//	}

		//	return result;
		//}
	}

	public enum TransactionStatus
	{
		Started = 0,
		Completed = 1,
		Rollbacked = 2
	}
}
