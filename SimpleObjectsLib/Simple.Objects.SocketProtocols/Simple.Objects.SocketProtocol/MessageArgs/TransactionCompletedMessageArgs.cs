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
using System.Threading;

namespace Simple.Objects.SocketProtocol
{
	[SystemMessageArgs((int)SystemMessage.TransactionCompleted)]
	public class TransactionCompletedMessageArgs : MessageArgs
	{
		public TransactionCompletedMessageArgs()
        {
        }

		public TransactionCompletedMessageArgs(long transactionId, long userId, long clientId, int serverId, int codePage, DateTime creationTime, IEnumerable<TransactionActionInfo> transactionServerActionInfos)
		{
			this.TransactionId = transactionId;
			this.UserId = userId;
			this.ClientId = clientId;
			this.ServerId = serverId;
			this.CodePage = codePage;
			this.CreationTime = creationTime;
			this.TransactionServerActionInfos = transactionServerActionInfos;
		}

		public long TransactionId { get; private set; }
		public long UserId { get; private set; }
		public long ClientId { get; private set; }
		public int ServerId { get; private set; }
		public int CodePage { get; private set; }
		public DateTime CreationTime { get; private set; }
		public IEnumerable<TransactionActionInfo>? TransactionServerActionInfos { get; private set; }

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);

			if (session is ISimpleObjectSession simpleObjectSession)
			{
				writer.CharacterEncoding = Encoding.GetEncoding(this.CodePage); // char encoding is per user specofic

				writer.WriteInt64Optimized(this.TransactionId);
				writer.WriteInt64Optimized(this.UserId);
				writer.WriteInt64Optimized(this.ClientId);
				writer.WriteInt32Optimized(this.ServerId);
				writer.WriteInt64Optimized(this.CodePage);
				writer.WriteDateTimeOptimized(this.CreationTime);
				writer.WriteInt32Optimized(this.TransactionServerActionInfos!.Count());

				foreach (var transactionAction in this.TransactionServerActionInfos!)
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
				this.TransactionId = reader.ReadInt64Optimized();
				this.UserId = reader.ReadInt64Optimized();
				this.ClientId = reader.ReadInt64Optimized();
				this.ServerId = reader.ReadInt32Optimized();
				this.CodePage = reader.ReadInt32Optimized();
				this.CreationTime = reader.ReadDateTimeOptimized();

				reader.CharacterEncoding = Encoding.GetEncoding(this.CodePage); // char encoding is per user specific
				TransactionActionInfo[] transactionActions = new TransactionActionInfo[reader.ReadInt32Optimized()];

				for (int i = 0; i < transactionActions.Length; i++)
					transactionActions[i] = new TransactionActionInfo().CreateFromReader(ref reader, simpleObjectSession.GetServerObjectModel);

				this.TransactionServerActionInfos = transactionActions;
			}
		}
	}
}
