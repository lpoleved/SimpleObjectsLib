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

namespace Simple.Objects.SocketProtocol
{
	[SystemMessageArgs((int)SystemMessage.TransactionCompleted)]
	public class TransactionCompletedMessageArgs : MessageArgs
	{
		public TransactionCompletedMessageArgs()
        {
        }

		public TransactionCompletedMessageArgs(IEnumerable<TransactionActionInfo> transactionActions)
		{
			this.TransactionActions = transactionActions;
		}

		public IEnumerable<TransactionActionInfo>? TransactionActions { get; private set; }
		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);

			if (session is ISimpleObjectSession simpleObjectSession)
			{
				writer.WriteInt32Optimized(this.TransactionActions!.Count());

				foreach (var transactionAction in this.TransactionActions!)
				{
					ServerObjectModelInfo objectModel = simpleObjectSession.GetServerObjectModel(transactionAction.TableId)!;
					
					transactionAction.WriteTo(ref writer, objectModel);
				}
			}
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);

			if (session is ISimpleObjectSession simpleObjectSession)
			{
				TransactionActionInfo[] transactionActions = new TransactionActionInfo[reader.ReadInt32Optimized()];

				for (int i = 0; i < transactionActions.Length; i++)
					transactionActions[i] = new TransactionActionInfo().CreateFromReader(ref reader, simpleObjectSession.GetServerObjectModel);

				this.TransactionActions = transactionActions;
			}
		}
	}
}
