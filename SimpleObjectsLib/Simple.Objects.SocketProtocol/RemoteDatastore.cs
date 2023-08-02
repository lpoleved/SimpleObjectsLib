using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Collections;
using Simple;
using Simple.Modeling;
using Simple.Serialization;
using Simple.Datastore;
using Simple.Objects;
using Simple.SocketEngine;

namespace Simple.Objects.SocketProtocol
{
	public class RemoteDatastore : IRemoteDatastore, IVirtualDatastore
	{
		public RemoteDatastore(SimpleObjectClient appClient)
		{
			this.AppClient = appClient;
		}

		private SimpleObjectClient AppClient { get; set; }

		public async ValueTask<IList<long>> GetObjectIdsTEMP(int tableId)
		{
			var response = await this.AppClient.GetObjectIdsTEMP(tableId);

			return response.ObjectIds!;
		}

		public async ValueTask<IEnumerable<PropertyIndexValuePair>?> GetObjectPropertyValues(int tableId, long objectId)
		{
			var response = await this.AppClient.GetObjectPropertyValues(tableId, objectId);

			return response.PropertyIndexValues;
		}


		//private async ValueTask<ServerObjectModelInfo?> GetServerObjectModel(int tableId) => await this.AppClient.GetServerObjectModel(tableId);


		public async ValueTask LoadObjectPropertyValues(SimpleObject simpleObject)
		{
			var response = await this.AppClient.GetObjectPropertyValues(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);

			simpleObject.LoadFromServer(response.PropertyIndexValues!);

			//return;

			//int tableId = simpleObject.GetModel().TableInfo.TableId;
			//PropertyIndexValuesResponseArgs responseArgs = await this.AppClient.GetObjectPropertyValuesFromServer(tableId, simpleObject.Id);

			//simpleObject.LoadFromServer(responseArgs.PropertyIndexes, responseArgs.PropertyValues, responseArgs.ServerObjectPropertyInfo);
		}

		public async ValueTask<GraphElementObjectPair[]> GetGraphElementsWithObjects(int graphKey, long parentGraphElementId)
		{
			var response = await this.AppClient.GetGraphElementsWithObjects(graphKey, parentGraphElementId);

			return response.GraphElementWithObjects!;
		}

		public async ValueTask<IList<long>> GetOneToManyForeignObjectCollection(int tableId, long objectId, int relationKey)
		{
			var response = await this.AppClient.GetOneToManyForeignObjectCollection(tableId, objectId, relationKey);

			return response.ObjectIds!;
		}

		public async ValueTask<SimpleObject?> GetOneToOneForeignObject(SimpleObject simpleObject, int relationKey)
		{
			var response = await this.AppClient.GetOneToForeignObject(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id, relationKey);
			var result = simpleObject.Manager.GetObject(response.TableId, response.ObjectId);

			return result;
		}

		public async ValueTask<IList<long>> GetGroupMembershipCollection(SimpleObject simpleObject, int relationKey)
		{
			var response = await this.AppClient.GetGroupMembershipCollection(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id, relationKey);

			return response.ObjectIds!;
		}

		public async ValueTask<long> GetSimpleObjectGraphElementIdByGraphKey(int tableId, long objectId, int graphKey)
		{
			var result = await this.AppClient.GetSimpleObjectGraphElementIdByGraphKey(tableId, objectId, graphKey);

			return result.ObjectId;
		}

		public async ValueTask<ClientTransactionResult> ProcessTransactionRequest(IEnumerable<TransactionActionInfo> transactionActionInfoList, Func<int, ISimpleObjectModel> getClientObjectModelByTableId, Func<int, ServerObjectModelInfo> getServerObjectPropertyInfoByTableId)
		{
			return await this.AppClient.SendTransactionRequest(transactionActionInfoList, getClientObjectModelByTableId, getServerObjectPropertyInfoByTableId);
		}
	}
}
