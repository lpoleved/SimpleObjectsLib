using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple;

namespace Simple.Objects
{
	public class TransactionResult
	{
		//public TransactionResult(bool transactionSucceeded)
		//	: this(transactionSucceeded, message: null)
		//{
		//}

		//public TransactionResult(bool transactionSucceeded, long transactionId, TransactionActionInfo[] transactionActionInfos)
		//	: this(transactionSucceeded, transactionId, transactionActionInfos, message: null)
		//{
		//}

		//public TransactionResult(bool transactionSucceeded, string? message)
		//	: this(transactionSucceeded, transactionId: -1, new TransactionActionInfo[0], message)
		//{
		//}

		public TransactionResult(bool transactionSucceeded, long transactionId, long userId, long clientId, long serverId, int codePage, IList<TransactionActionInfo> transactionServerActionInfos, SimpleObjectValidationResult? validationErrorResult, string? message)
		{
			this.TransactionSucceeded = transactionSucceeded;
			this.TransactionId = transactionId;
			this.TransactionServerActionInfos = transactionServerActionInfos;
			this.Message = message;
		}

		public bool TransactionSucceeded { get; private set; }
		public long TransactionId { get; private set; }
		public long UserId { get; private set; }
		public long ClientId { get; private set; }
		public int ServerId { get; private set; }
		public int CodePage { get; private set; }
		public DateTime CreationTime { get; private set; }
		public IList<TransactionActionInfo> TransactionServerActionInfos { get; private set; }

		//public IEnumerable<TransactionActionInfo>? TransactionRequestActions { get; private set; }

		//public IDictionary<SimpleObject, TransactionRequestAction> TransactionRequests { get; private set; }

		public string? Message { get; private set; }
		public SimpleObjectValidationResult? ValidationErrorResult { get; set; }

		public string? FullMessage => (!this.Message.IsNullOrEmpty()) ? this.Message : this.ValidationErrorResult?.Message;
	}
}
