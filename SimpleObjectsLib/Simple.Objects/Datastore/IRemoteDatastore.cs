using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple;
using Simple.Modeling;

namespace Simple.Objects
{
	public interface IRemoteDatastore : IVirtualDatastore
	{
		ValueTask<IList<long>> GetObjectIdsTEMP(int tableId);

		ValueTask<IEnumerable<PropertyIndexValuePair>?> GetObjectPropertyValues(int tableId, long objectId);
		ValueTask<GraphElementObjectPair[]> GetGraphElementsWithObjects(int graphKey, long parentGraphElementId);
		ValueTask<IList<long>> GetOneToManyForeignObjectCollection(int tableId, long objectId, int relationKey);
		ValueTask<SimpleObject?> GetOneToOneForeignObject(SimpleObject simpleObject, int relationKey);
		ValueTask<IList<long>> GetGroupMembershipCollection(SimpleObject simpleObject, int relationKey);
		ValueTask<long> GetSimpleObjectGraphElementIdByGraphKey(int tableId, long objectId, int graphKey);
		ValueTask<ClientTransactionResult> ProcessTransactionRequest(IEnumerable<TransactionActionInfo> transactionActionInfoList, 
																	 Func<int, ISimpleObjectModel> getClientObjectModelByTableId, 
																	 Func<int, ServerObjectModelInfo> getServerObjectModelInfoByTableId);
	}
}
