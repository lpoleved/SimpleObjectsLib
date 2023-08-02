using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects
{
	public class Transaction
	{
		//public Transaction(SimpleObjectManager objectManager, User user)
		//{
		//	//objectManager.BeginTransaction_New(user);
		//}

		internal Transaction(SystemTransaction systemTransaction, IUser user)
		{
			this.SystemTransaction = systemTransaction;
			this.User = user;
			this.State = TransactionState.Started;
		}

		internal SystemTransaction SystemTransaction { get; private set; }

		public IUser User { get; private set; }
		public TransactionState State { get; internal set; }

		public long TransactionId
		{
			get { return this.SystemTransaction.TransactionId; }
		}

		public TransactionStatus Status
		{
			get { return this.SystemTransaction.Status; }
		}

		public DateTime CreationTime
		{
			get { return this.SystemTransaction.CreationTime; }
		}

		public SimpleObjectValidationResult ValidationResult { get; internal set; }

		public bool Contains(SimpleObject simpleObject)
		{
			return this.SystemTransaction.RollbackTransactionActions.FirstOrDefault(item => item.TableId == simpleObject.GetModel().TableInfo.TableId && item.ObjectId == simpleObject.Id) != null;
		}

		//public bool TryGetRequest(SimpleObject simpleObject, out TransactionRequestAction requestAction)
		//{
		//	TransactionActionInfo transactionActionInfo = this.SystemTransaction.TransactionActions.FirstOrDefault(item => item.TableId == simpleObject.GetModel().TableInfo.TableId && item.ObjectId == simpleObject.Id);

		//	if (transactionActionInfo != null)
		//	{
		//		requestAction = transactionActionInfo.ActionType == TransactionActionType.;

		//		return true;
		//	}

		//	return false;
			
		//	//return this.SystemTransaction.TransactionActions.TryGetValue(simpleObject, out requestAction);
		//}

		//public void AddRequest(SimpleObject simpleObject, TransactionRequestAction requestAction)
		//{
		//	if (this.State != TransactionState.Started)
		//		throw new Exception("You cannot add new transaction request when transaction is processing.");

		//	this.SystemTransaction.TransactionRequestsBySimpleObject.Add(simpleObject, requestAction);
		//}

		//public void SetRequest(SimpleObject simpleObject, TransactionRequestAction requestAction)
		//{
		//	if (this.State != TransactionState.Started)
		//		throw new Exception("You cannot change transaction request when transaction is processing.");

		//	// this.SystemTransaction.TransactionRequestsBySimpleObject[simpleObject] = requestAction;

		//	bool allowAdd = true;
		//	//TransactionRequest[] transactionRequests = this.RelatedTransactionRequests.ToArray();

		//	foreach (var item in this.SystemTransaction.TransactionRequestsBySimpleObject.ToArray())
		//	{
		//		SimpleObject itemSimpleObject = item.Key;
		//		TransactionRequestAction itemRequestAction = item.Value;

		//		if (itemSimpleObject == simpleObject)
		//		{
		//			if (itemRequestAction == TransactionRequestAction.Save && requestAction == TransactionRequestAction.Delete && !simpleObject.IsNew)
		//			{
		//				this.SystemTransaction.TransactionRequestsBySimpleObject[itemSimpleObject] = TransactionRequestAction.Delete;
		//				allowAdd = false; // Prevent saving object if already deletion requested.

		//				continue;
		//			}

		//			allowAdd = false;
		//		}

		//		// remove unnecessary transaction requests
		//		if (itemRequestAction == TransactionRequestAction.Save)
		//		{
		//			if (!itemSimpleObject.RequireSaving())
		//				this.SystemTransaction.TransactionRequestsBySimpleObject.Remove(itemSimpleObject);
		//		}
		//		else if (itemRequestAction == TransactionRequestAction.Delete)
		//		{
		//			if (itemSimpleObject.IsNew || itemSimpleObject.DeleteStarted)
		//				this.SystemTransaction.TransactionRequestsBySimpleObject.Remove(itemSimpleObject);
		//		}
		//	}

		//	if (allowAdd)
		//	{
		//		if (requestAction == TransactionRequestAction.Save)
		//		{
		//			if (!simpleObject.RequireSaving())
		//				allowAdd = false;
		//		}
		//		else if (requestAction == TransactionRequestAction.Delete)
		//		{
		//			if (simpleObject.DeleteStarted)
		//				allowAdd = false;
		//		}
		//	}

		//	if (allowAdd)
		//		this.SystemTransaction.TransactionRequestsBySimpleObject.Add(simpleObject, requestAction);
		//}
	}
	public enum TransactionState
	{
		Started = 0,
		ValidationInProgress = 1,
		ValidationFails = 2,
		ValidatedSuccessfully = 3,
		Processing = 4,
		Finished = 5
	}
}
