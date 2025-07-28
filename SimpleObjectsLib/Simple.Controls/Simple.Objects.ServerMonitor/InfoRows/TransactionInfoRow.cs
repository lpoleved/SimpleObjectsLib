using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Objects;
using Simple.Serialization;
using Simple.Objects.SocketProtocol;
using Simple.Objects.MonitorProtocol;

namespace Simple.Objects.ServerMonitor
{

	// TODO: Check if this TransactionMessageArgs can handle this functionality

	public class TransactionInfoRow
	{
		//private IEnumerable<TransactionActionInfo>? rollbackActions = null;
		//private IEnumerable<DatastoreActionInfo>? datastoreActions = null;
		//private object context;

		public TransactionInfoRow(int rowIndex, TransactionMessageArgs transactionMessageArgs)
		{

			this.RowIndex = rowIndex;
			this.TransactionId = transactionMessageArgs.TransactionId;
			this.UserId = transactionMessageArgs.UserId;
			this.Status = transactionMessageArgs.TransactionStatus.ToString();
			this.CreationTime = transactionMessageArgs.CreationTime;
			this.CharacterEncoding = Encoding.GetEncoding(transactionMessageArgs.CodePage).ToString() ?? String.Empty;
			
			// imternal properies
			this.CodePage = transactionMessageArgs.CodePage;
			this.IsRollbackActionDataCompressed = transactionMessageArgs.IsRollbackActionDataCompressed;
			this.TransactionRequestData = transactionMessageArgs.TransactionRequestsData!;
			this.DatastoreActionsData = transactionMessageArgs.DatastoreActionsData!;
			this.RollbackActionData = transactionMessageArgs.RollbackActionData!;

			//this.context = context;
		}

		//public DatastoreTransactionInfoRow(long transactionId, Guid administratorKey, long clientId, TransactionStatus status, DateTime creationTime, ArraySegment<byte> actionData, DatastoreTransactionAction[] transactionActions)
		//{
		//	this.TransactionId = transactionId;
		//	this.AdminKey = String.Format("{0}.{1}.{2}", administratorKey.GetTableId(), administratorKey.GetClientId(), administratorKey.GetObjectId());
		//	this.ClientId = clientId;
		//	this.Status = status.ToString();
		//	this.CreationTime = creationTime;
		//	this.ActionData = actionData;
		//	this.TransactionActions = transactionActions;
		//}

		//public long TransactionId { get; private set; }
		//public string AdminKey { get; private set; }
		//public long ClientId { get; private set; }
		//public string Status { get; set; }
		//public DateTime CreationTime { get; private set; }
		//public ArraySegment<byte> ActionData { get; private set; }

		//internal DatastoreTransactionAction[] TransactionActions { get; private set; }
		public int RowIndex { get; private set; }
		public long TransactionId { get; private set; }
		public long UserId { get; private set; }
		public string Status { get; private set; }
		public DateTime CreationTime { get; private set; }
		public string CharacterEncoding { get; private set; }
	
		internal int CodePage { get; private set; }
		internal bool IsRollbackActionDataCompressed { get; set; }
		internal TransactionCompressionAlgorithm CompressionAlgorithm { get; set; }
		internal float CompressionRatio { get; private set; }

		internal byte[] TransactionRequestData { get; set; }
		internal TransactionActionInfo[] TransactionRequests { get; set; }

		internal byte[] DatastoreActionsData { get; set; }
		internal DatastoreActionInfo[] DatastoreActions { get; set; }

		internal byte[] RollbackActionData { get; set; }
		internal TransactionActionInfo[] RollbackTransactionActions { get; set; }

		//internal IEnumerable<DatastoreActionInfo>? DatastoreActions { get; set; } = null;
		//{
		//	get 
		//	{
		//		if (this.datastoreActions is null)
		//			this.datastoreActions = this.ReadDatastoreActions();

		//		return this.datastoreActions; 
		//	}
		//}
		
		//internal IEnumerable<TransactionActionInfo>? RollbackActions { get; set; } = null;
		//{
		//	get 
		//	{
		//		if (this.rollbackActions is null)
		//			this.rollbackActions = this.ReadRollbackActions();

		//		return this.rollbackActions;
		//	}
		//}

		//internal IEnumerable<TransactionActionInfo> TransactionRequests
		//{
		//	get 
		//	{
		//		if (this.transactionMessageArgs.TransactionRequests is null)
		//			this.transactionMessageArgs.ReadTransactionActions(this.getServerObjectModel);

		//		return this.transactionMessageArgs.TransactionRequests!; 
		//	}
		//}



	}
}
