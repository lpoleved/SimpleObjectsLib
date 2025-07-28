using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects
{
	public class ClientTransactionResult // struct
	{
		public ClientTransactionResult(string messageInfo)
		{
			this.TransactionSucceeded = false;
			this.MessageInfo = messageInfo;
			this.NewObjectIdsByTempClientObjectId = null;
			this.SimpleObjectPropertiesWithTempClientObjectIdNeedsToChange = null;
		}
		
		public ClientTransactionResult(IEnumerable<long> tempClientObjectIds,  IEnumerable<long> newObjectIds, List<NewPropertyObjectIdNeedToBeSet> simpleObjectPropertiesWithTempClientObjectIdToChange)
		{
			this.TransactionSucceeded = true;
			this.MessageInfo = String.Empty;

			this.NewObjectIdsByTempClientObjectId = new Dictionary<long, long>(tempClientObjectIds.Count());

			for (int i = 0; i < tempClientObjectIds.Count(); i++)
			{
				long tempClientObjectId = tempClientObjectIds.ElementAt(i);
				long newObjectId = newObjectIds.ElementAt(i);

				this.NewObjectIdsByTempClientObjectId.Add(tempClientObjectId, newObjectId);
			}
			
			this.SimpleObjectPropertiesWithTempClientObjectIdNeedsToChange = simpleObjectPropertiesWithTempClientObjectIdToChange;
		}

		public bool TransactionSucceeded { get; private set; }
		public Dictionary<long, long>? NewObjectIdsByTempClientObjectId { get; private set; }
		public List<NewPropertyObjectIdNeedToBeSet>? SimpleObjectPropertiesWithTempClientObjectIdNeedsToChange { get; set; }
		public string MessageInfo { get; set; }
	}
}
