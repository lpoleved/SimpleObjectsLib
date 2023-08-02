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
		public TransactionResult(bool transactionSucceeded)
			: this(transactionSucceeded, infoMessage: null)
		{
		}

		public TransactionResult(bool transactionSucceeded, long transactionId)
			: this(transactionSucceeded, transactionId, infoMessage: null)
		{
		}

		public TransactionResult(bool transactionSucceeded, string? infoMessage)
			: this(transactionSucceeded, default(long), infoMessage)
		{
		}

		public TransactionResult(bool transactionSucceeded, long transactionId, string? infoMessage)
		{
			this.TransactionSucceeded = transactionSucceeded;
			this.TransactionId = transactionId;
			this.InfoMessage = infoMessage;
		}

		public bool TransactionSucceeded { get; private set; }
		public long TransactionId { get; private set; }
		//public IEnumerable<TransactionActionInfo>? TransactionRequestActions { get; private set; }

		//public IDictionary<SimpleObject, TransactionRequestAction> TransactionRequests { get; private set; }

		public string? InfoMessage { get; private set; }
		public SimpleObjectValidationResult? ValidationResult { get; set; }

		public string? Message => (!this.InfoMessage.IsNullOrEmpty()) ? this.InfoMessage : this.ValidationResult?.Message;
	}
}
