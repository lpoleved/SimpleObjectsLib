using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Objects;

namespace Simple.Objects.Controls
{
	public class UndoTransaction : UndoAction
	{
		public UndoTransaction(SystemTransaction transaction)
		{
			this.Transaction = transaction;
			this.TransactionId = transaction.TransactionId;
		}

		public SystemTransaction Transaction { get; set; }
		public long TransactionId { get; private set; }

		public override string GetText(UndoActionType actionType)
		{
			string text = (actionType == UndoActionType.Undo) ? "Rollback Transaction " : "Apply Transaction ";
			text += this.TransactionId.ToString();

			return text;
		}

		public override void Undo()
		{
			this.Transaction.ObjectManager?.UndoTransaction(this.Transaction, ObjectActionContext.Unspecified, requester: this);
		}

		public override void Redo()
		{
			this.Transaction.ObjectManager?.UndoTransaction(this.Transaction, ObjectActionContext.Unspecified, requester: this);
		}
	}
}
