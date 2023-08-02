using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple;
using Simple.Serialization;
using Simple.Modeling;
using Simple.SocketEngine;
using Simple.Objects;
using System.Buffers;
using Simple.Objects.SocketProtocol;

namespace Simple.Objects.MonitorProtocol
{
	/// <summary>
	/// Serialize Simple.Object Transaction and use it as TransactionInfoRow at a same time.
	/// </summary>
	[SystemMessageArgs((int)MonitorSystemMessage.TransactionFinished)]
	public class TransactionMessageArgs : MessageArgs
	{
		//private Func<int, ISimpleObjectModel> getObjectModelByTableId = null;
		//private Func<int, IPropertySequence> getPropertySequenceBySequenceId = null;
		//private Func<int, ServerObjectModelInfo> getServerObjectPropertyInfoByTableId = null;
		//private ArraySegment<byte> actionData;
		//private IEnumerable<TransactionActionInfo> transactionActionLogs = null;
		////private bool isTransactionActionsSerialized = false;
		//private SerializationReader reader = null;

		public TransactionMessageArgs()
        {
        }

  //      public TransactionMessageArgs(SerializationReader reader, Func<int, ISimpleObjectModel> getObjectModelByTableId, Func<int, IPropertySequence> getPropertySequenceBySequenceId, Func<int, ServerObjectModelInfo> getServerObjectPropertyInfoByTableId)
		//{
		//	this.getObjectModelByTableId = getObjectModelByTableId;
		//	this.getPropertySequenceBySequenceId = getPropertySequenceBySequenceId;
		//	this.getServerObjectPropertyInfoByTableId = getServerObjectPropertyInfoByTableId;

		//	this.ReadFrom(reader, null); //??????
		//}

		public TransactionMessageArgs(SystemTransaction transaction, IEnumerable<DatastoreActionInfo> datastoreActions)
		{
			this.TransactionId = transaction.TransactionId;
			this.UserId = transaction.UserId;
			this.CodePage = transaction.CodePage;
			this.TransactionStatus = transaction.Status;
			this.CreationTime = transaction.CreationTime;
			this.RollbackActionData = transaction.RollbackActionData;
			this.IsRollbackActionDataCompressed = transaction.IsRollbackActionDataCompressed;
			//this.TransactionRequests = transactionRequests;
			this.DatastoreActions = datastoreActions;
		}

		public long TransactionId { get; private set; }
		public long UserId { get; private set; }
		public int CodePage { get; private set; }
		public TransactionStatus TransactionStatus { get; set; }
		public DateTime CreationTime { get; private set; }
		public byte[]? RollbackActionData { get; private set; }
		public bool IsRollbackActionDataCompressed { get; private set; }
		public float CompressionRatio { get; private set; }
		internal IEnumerable<TransactionActionInfo>? RollbackActions { get; private set; }
		//public IEnumerable<TransactionActionInfo>? TransactionRequests { get; private set; }
		//public byte[]? TransactionActionsData { get; private set; }

		public byte[]? DatastoreActionsData { get; private set; }
		internal IEnumerable<DatastoreActionInfo>? DatastoreActions { get; private set; }
		//{
		//	get
		//	{
		//		if (this.transactionActionLogs == null)
		//		{
		//			BufferStream transactionActionsStream = new BufferStream();
		//			transactionActionsStream.Initialize(new ArraySegment<byte>[] { this.ActionData });
		//			SerializationReader reader = new SerializationReader(transactionActionsStream);

		//			this.transactionActionLogs = SystemTransaction.DeserializeTransactionActionLogs(reader, this.getObjectModelByTableId, this.getPropertySequenceBySequenceId);
		//		}

		//		return this.transactionActionLogs;
		//	}
		//}

		//public override int GetBufferCapacity()
		//{
		//	return 0;
		//}


		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);

			if (session is ISimpleObjectSession simpleObjectSession)
			{
				Func<int, ServerObjectModelInfo?> getServerObjectModel = (int tableId) => simpleObjectSession.GetServerObjectModel(tableId); // this.getServerObjectPropertyInfoByTableId); // this.getObjectModelByTableId);

				writer.WriteInt64Optimized(this.TransactionId);
				writer.WriteInt64Optimized(this.UserId);
				writer.WriteInt32(this.CodePage);
				writer.WriteBits((byte)this.TransactionStatus, 2);
				writer.WriteDateTimeOptimized(this.CreationTime);
				writer.WriteBoolean(this.IsRollbackActionDataCompressed);

				//
				// Rolback Action Data
				//
				int rollbackActionDataLength = this.RollbackActionData?.Length ?? 0;
				
				writer.WriteInt32Optimized(rollbackActionDataLength);

				if (rollbackActionDataLength > 0)
					writer.WriteBinary(this.RollbackActionData!);

				////
				//// Transaction Requests
				////
				//Span<byte> transactionActionsWriterSpan = stackalloc byte[this.TransactionRequests!.Count() * 30];
				//SequenceWriter transactionActionsWriter = new SequenceWriter(transactionActionsWriterSpan, Encoding.GetEncoding(this.CodePage));
				////SystemTransaction.WriteTo(ref transactionActionsWriter, this.TransactionActions!, getServerObjectPropertyInfoByTableId);
				//transactionActionsWriter.WriteInt32Optimized(this.TransactionRequests!.Count());

				//foreach (TransactionActionInfo item in this.TransactionRequests!)
				//	item.WriteTo(ref transactionActionsWriter, getServerObjectModel);

				//this.TransactionActionsData = transactionActionsWriter.ToArray();
				//writer.WriteInt32Optimized(this.TransactionActionsData.Length);
				//writer.WriteSequence(this.TransactionActionsData);

				//
				// Datastore Actions
				//
				Span<byte> datastoreActionsWriterSpan = stackalloc byte[this.DatastoreActions!.Count() * 30];
				SequenceWriter datastoreActionsWriter = new SequenceWriter(datastoreActionsWriterSpan, Encoding.GetEncoding(this.CodePage));

				datastoreActionsWriter.WriteInt32Optimized(this.DatastoreActions!.Count());

				foreach (DatastoreActionInfo item in this.DatastoreActions!)
				{
					var objectModel = simpleObjectSession.GetServerObjectModel(item.TableId);

					item.WriteTo(ref datastoreActionsWriter, objectModel);
				}

				this.DatastoreActionsData = datastoreActionsWriter.ToArray();
				writer.WriteInt32Optimized(this.DatastoreActionsData.Length);
				writer.WriteBinary(this.DatastoreActionsData);
			}
		}

		/// <summary>
		/// Reads only RollbackActionData
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="session"></param>
		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);

			if (session is ISimpleObjectSession simpleObjectSession)
			{
				// The problem here is that we are in package decoding while need to send request. It must be doing on a different way.
				
				
				//Func<int, ServerObjectModelInfo?> getServerObjectModel = (int tableId) => simpleObjectSession.GetServerObjectModel(tableId).GetAwaiter().GetResult(); // this.getServerObjectPropertyInfoByTableId); // this.getObjectModelByTableId);

				this.TransactionId = reader.ReadInt64Optimized();
				this.UserId = reader.ReadInt64Optimized();
				this.CodePage = reader.ReadInt32();
				this.TransactionStatus = (TransactionStatus)reader.ReadBits(2);
				this.CreationTime = reader.ReadDateTimeOptimized();
				this.IsRollbackActionDataCompressed = reader.ReadBoolean();

				int rollbackActionDataLength = reader.ReadInt32Optimized();

				if (rollbackActionDataLength > 0)
					this.RollbackActionData = reader.ReadBinary(rollbackActionDataLength).ToArray(); // new byte[actionDataLength];

				if (this.IsRollbackActionDataCompressed)
					this.RollbackActionData = SystemTransaction.Decompress(this.RollbackActionData!.ToArray());

				int datastoreActionCount = reader.ReadInt32Optimized();

				this.DatastoreActionsData = reader.ReadBinary(datastoreActionCount).ToArray();
			}
		}

		//public void ReadTransactionActions(Func<int, ServerObjectModelInfo?> getServerObjectModel)
		//{
		//	//this.TransactionActions = SystemTransaction.ReadFrom(ref reader, getServerObjectModel); // this.getServerObjectPropertyInfoByTableId); // this.getObjectModelByTableId);
		//	SequenceReader reader = new SequenceReader(this.TransactionActionsData!, Encoding.GetEncoding(this.CodePage));
		//	int transactionActionCount = reader.ReadInt32Optimized();
		//	TransactionActionInfo[] transactionActions = new TransactionActionInfo[transactionActionCount];

		//	for (int i = 0; i < transactionActionCount; i++)
		//	{
		//		TransactionActionInfo transactionActionInfo = new TransactionActionInfo();

		//		transactionActionInfo.ReadFrom(ref reader, getServerObjectModel);
		//		transactionActions[i] = transactionActionInfo;
		//	}

		//	this.TransactionRequests = transactionActions;
		//}


		//private void ReadTransactionActions(SerializationReader reader)
		//{
		//	int actionDataPosition = (int)reader.BaseStream.Position;

		//	this.transactionActions = SimpleObjectManager.DeserializeTransactionActions(reader, this.getObjectModelByTableId, this.getPropertySequenceBySequenceId);
		//	this.actionData = new ArraySegment<byte>(reader.BaseStream.GetBuffer(), actionDataPosition, (int)reader.BaseStream.Length - actionDataPosition);
		//	this.isTransactionActionsSerialized = true;
		//}
	}
}
