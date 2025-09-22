using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Objects;
using Simple.Serialization;
using Simple.SocketEngine;

namespace Simple.Objects.SocketProtocol
{
	[SystemResponseArgs((int)SystemRequest.TransactionRequest)]
	public class ProcessTransactionResponseArgs : ResponseArgs
	{
        public ProcessTransactionResponseArgs()
        {
        }

		public ProcessTransactionResponseArgs(long transactionId, IEnumerable<long>? newObjectIds, string infoMessage) // IEnumerable<long> newObjectIds)
		{
			this.TransactionSucceeded = transactionId > 0;
			this.TransactionId = transactionId;
			this.NewObjectIds = newObjectIds;
			this.InfoMessage = infoMessage;
		}

		//private int NewObjectsCount { get; set; }
		public bool TransactionSucceeded { get; private set; }
		public long TransactionId { get; private set; }
		public IEnumerable<long>? NewObjectIds { get; private set; }
		public string InfoMessage { get; private set; } = String.Empty;

		public override int GetBufferCapacity() => base.GetBufferCapacity() + 15;

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			base.WriteTo(ref writer, session);

			writer.WriteBoolean(this.TransactionSucceeded);

			if (this.TransactionSucceeded)
			{
				writer.WriteInt64Optimized(this.TransactionId);
				writer.WriteInt32Optimized(this.NewObjectIds!.Count());

				foreach (long newObjectId in this.NewObjectIds!)
					writer.WriteInt64Optimized(newObjectId); // Writes new object Id 
			}
			//else
			//{
				writer.WriteString(this.InfoMessage);
			//}
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			base.ReadFrom(ref reader, session);
			
			this.TransactionSucceeded = reader.ReadBoolean();

			if (this.TransactionSucceeded)
			{
				this.TransactionId = reader.ReadInt64Optimized();
				var newObjectIds = new long[reader.ReadInt32Optimized()];

				for (int i = 0; i < newObjectIds.Length; i++)
					newObjectIds[i] = reader.ReadInt64Optimized();

				this.NewObjectIds = newObjectIds;
			}
			//else
			//{
				this.InfoMessage = reader.ReadString();
			//}
		}
	}
}
