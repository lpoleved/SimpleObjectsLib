using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Collections;
using Simple.Serialization;
using Simple.Modeling;
using Simple.Compression;
using System.Transactions;

namespace Simple.Objects
{
	public sealed class SystemTransaction : SystemObject<long, SystemTransaction>
    {
		static SystemTransaction()
		{
			Model.TableInfo = SystemTablesBase.SystemTransactions;
			Model.AutoGenerateKey = true;
			Model.ReuseObjectKeys = false;
		}

		public SystemTransaction()
		{
			this.CompressionAlgorithm =  (int)TransactionCompressionAlgorithm.MiniLZOSafe;
			this.RollbackActionData = new byte[0];
			this.RollbackTransactionActions = new TransactionActionInfo[0];
		}

		public SystemTransaction(SimpleObjectManager objectManager, ref SystemObjectCollectionByObjectKey<long, SystemTransaction> dictionaryCollection, 
								 long userId, Encoding characterEncoding, long clientId, int serverId, IEnumerable<TransactionActionInfo> rollbackTransactionActions, TransactionStatus status, TransactionCompressionAlgorithm compressionAlgorithm = TransactionCompressionAlgorithm.MiniLZOSafe)
			: base(objectManager, ref dictionaryCollection, 
				  (item) =>
				  {
					  item.UserId = userId;
					  item.ClientId = clientId;
					  item.ServerId = serverId;
					  item.Status = status;
					  item.CreationTime = DateTime.Now;
					  item.RollbackTransactionActions = rollbackTransactionActions;
					  item.RollbackActionData = null;
					  item.CompressionAlgorithm = compressionAlgorithm;
					  item.CodePage = characterEncoding.CodePage;

					  //item.TransactionActionsBySimpleObject = new Dictionary<SimpleObject, DatastoreTransactionAction>();
				  })
		{
			this.IsRollbackActionDataCompressed = this.ObjectManager?.UseCompressionForTransactionLogActionData ?? false;
			this.RollbackActionData = CreateRollbackActionData(rollbackTransactionActions, this.CodePage, this.IsRollbackActionDataCompressed, compressionAlgorithm, this.ObjectManager!.GetServerObjectModel, out float compresionRatio);
			this.CompressionRatio = compresionRatio;
			this.RollbackTransactionActions = new TransactionActionInfo[0];
		}

		protected override void OnLoad()
		{
			base.OnLoad();
			this.RollbackTransactionActions = GetRollbackTransactionActions(this.RollbackActionData, this.CodePage, this.IsRollbackActionDataCompressed, (TransactionCompressionAlgorithm)this.CompressionAlgorithm, this.ObjectManager!.GetServerObjectModel);
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
		public byte[] RollbackActionData { get; private set; }
		
		public bool IsRollbackActionDataCompressed { get; private set; }

		public TransactionCompressionAlgorithm CompressionAlgorithm { get; private set; }

		public int CodePage { get; private set; }
		
		public string? ErrorDescription { get; set; }

		[NonStorable]
		public IEnumerable<TransactionActionInfo> RollbackTransactionActions { get; private set; }

		[NonStorable]
		public float CompressionRatio { get; private set; }


		//public static void WriteTo(ref SequenceWriter writer, IEnumerable<TransactionActionInfo> transactionActions, Func<int, ServerObjectModelInfo?> getServerObjectModel)
		//{
		//	//writer.WriteInt32Optimized(transactionActions.Count());

		//	//foreach (TransactionActionInfo transactionActionInfo in transactionActions)
		//	//{
		//	//	int tableId = transactionActionInfo.TableId;
		//	//	long objectId = transactionActionInfo.ObjectId;
		//	//	ServerObjectModelInfo objectModel = getServerObjectModel(tableId);

		//	//	if (transactionActionInfo.ActionType == TransactionActionType.Insert)
		//	//	{
		//	//		writer.WriteBoolean(true); // Insert
		//	//	}
		//	//	else
		//	//	{
		//	//		writer.WriteBoolean(false); // NOT Insert
		//	//		writer.WriteBoolean(transactionActionInfo.ActionType == TransactionActionType.Update); // true for Update, false for Delete
		//	//	}

		//	//	writer.WriteInt32Optimized(tableId);
		//	//	writer.WriteInt64Optimized(objectId);

		//	//	SystemTransactionAction.WriteTo(ref writer, transactionActionInfo.ActionType, transactionActionInfo.PropertyIndexValues, objectModel);
		//	//}

		//	//return;

		//	// TODO: Is this a same thing.....
		//	// Add this as extenson method for IEnumerable<TransactionActionInfo> (WriteTo and ReadFrom)
		//	//
		//	writer.WriteInt32Optimized(transactionActions.Count());

		//	foreach (TransactionActionInfo item in transactionActions)
		//		item.WriteTo(ref writer, getServerObjectModel(item.TableId)!);
		//}

		//public static IEnumerable<TransactionActionInfo> ReadFrom(ref SequenceReader reader, Func<int, ServerObjectModelInfo?> getServerObjectModelByTableId) //Func<int, ISimpleObjectModel> getObjectModel)
		//{
		//	//int transactionActionCount = reader.ReadInt32Optimized();
		//	//TransactionActionInfo[] transactionActions = new TransactionActionInfo[transactionActionCount];

		//	//for (int i = 0; i < transactionActionCount; i++)
		//	//{
		//	//	TransactionActionType actionType = TransactionActionType.Delete;

		//	//	if (reader.ReadBoolean()) // is Insert?
		//	//		actionType = TransactionActionType.Insert;
		//	//	else if (reader.ReadBoolean()) // is Update?
		//	//		actionType = TransactionActionType.Update;

		//	//	int tableId = reader.ReadInt32Optimized();
		//	//	long objectId = reader.ReadInt64Optimized();
		//	//	ServerObjectModelInfo? objectModel = getServerObjectModel(tableId);
		//	//	IEnumerable<PropertyIndexValuePair>? propertyIndexValues = SystemTransactionAction.ReadFrom(ref reader, actionType, objectModel);

		//	//	transactionActions[i] = new TransactionActionInfo(tableId, objectId, actionType, propertyIndexValues);
		//	//}

		//	//return transactionActions;

		//	// TODO: Is this a same thing.....
		//	int count = reader.ReadInt32Optimized();
		//	TransactionActionInfo[] result = new TransactionActionInfo[count];

		//	for (int i = 0; i < count; i++)
		//	{
		//		TransactionActionInfo item = new TransactionActionInfo();

		//		item.ReadFrom(ref reader, getServerObjectModelByTableId);
		//		result[i] = item;
		//	}

		//	return result;
		//}

#if DEBUG
		//public new void Save()
		//{
		//	//lock (this.lockObject)
		//	//{
		//		this.OnBeforeSave();

		//		int[] propertyIndexes = this.GetModel().StorablePropertyIndexes;

		//		propertyIndexes = new int[] { 0, 1, 2, 3 };

		//		var propertyIndexValues = this.GetPropertyIndexValuePairs(propertyIndexes);

		//		if (this.IsNew) // -> Insert
		//		{
		//			this.ObjectManager?.LocalDatastore?.InsertRecord(this.GetModel().TableInfo, propertyIndexValues, this.GetModel().GetPropertyModel);
		//			this.IsNew = false;
		//		}
		//		else // -> Update
		//		{
		//			long key = this.GetKeyValue();

		//			this.ObjectManager?.LocalDatastore?.UpdateRecord(this.GetModel().TableInfo, this.GetModel().ObjectKeyPropertyModel.PropertyIndex, key, propertyIndexValues, this.GetModel().GetPropertyModel);
		//		}

		//		this.OnAfterSave();
		//	//}
		//}
#endif

		public static byte[] Compress(byte[] actionData, TransactionCompressionAlgorithm compressionAlgorithm)
		{
			if (compressionAlgorithm == TransactionCompressionAlgorithm.MiniLZOSafe)
				return MiniLZOSafe.Compress(actionData);

			throw new ArgumentException("CompressionAlgorithm is not defined");
		}

		public static byte[] Decompress(byte[] actionData, TransactionCompressionAlgorithm compressionAlgorithm)
		{
			if (compressionAlgorithm == TransactionCompressionAlgorithm.MiniLZOSafe)
				return MiniLZOSafe.Decompress(actionData);

			throw new ArgumentException("CompressionAlgorithm is not defined");
		}

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

		public static byte[] CreateRollbackActionData(IEnumerable<TransactionActionInfo> rollbackTransactionActions, int codePage, bool compressRollbackActionData, TransactionCompressionAlgorithm compressionAlgorithm, Func<int, ServerObjectModelInfo?> getServerObjectModelByTableId, out float compresioRation)
		{
			//BufferSequenceWriter bufferSequenceWriter = new BufferSequenceWriter();
			Span<byte> span = stackalloc byte[256];
			SequenceWriter writer = new SequenceWriter(span, Encoding.GetEncoding(codePage));

			writer.WriteInt32Optimized(rollbackTransactionActions.Count());

			foreach (TransactionActionInfo item in rollbackTransactionActions)
				item.WriteTo(ref writer, getServerObjectModelByTableId(item.TableId)!);


			byte[] data = writer.ToArray();

			//this.IsRollbackActionDataCompressed = this.ObjectManager.UseCompressionForTransactionLogActionData;

			if (compressRollbackActionData)
			{
				int uncompressedLength = data.Length;

				data = Compress(data, compressionAlgorithm);
				compresioRation = 100 - ((float)data.Length / (float)uncompressedLength) * 100;
			}
			else
			{
				compresioRation = 0;
			}

			return data;
		}


		public static TransactionActionInfo[] GetRollbackTransactionActions(byte[] rollbackActionData, int codePage, bool isRollbackActionDataCompressed, TransactionCompressionAlgorithm compressionAlgorithm, Func<int, ServerObjectModelInfo?> getServerObjectModelByTableId)
		{
			byte[] data = rollbackActionData;

			if (isRollbackActionDataCompressed)
				data = Decompress(rollbackActionData, compressionAlgorithm);

			SequenceReader reader = new SequenceReader(data, Encoding.GetEncoding(codePage));
			TransactionActionInfo[] result = new TransactionActionInfo[reader.ReadInt32Optimized()];

			for (int i = 0; i < result.Count(); i++)
			{
				TransactionActionInfo item = new TransactionActionInfo();

				item.ReadFrom(ref reader, getServerObjectModelByTableId);
				result[i] = item;
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

	public enum TransactionCompressionAlgorithm
	{
		MiniLZOSafe = 0,
	}
}