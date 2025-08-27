using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Simple.Modeling;
using Simple.Datastore;

namespace Simple.Objects
{
	public class LocalDatastore : DatastoreProvider, IVirtualDatastore
	{
		private readonly object lockObject = new object();

		public LocalDatastore()
		{
		}

		public ValueTask<List<long>> GetObjectIds(TableInfo tableInfo, IPropertyModel idPropertyModel)
		{
			//ISimpleObjectModel objectModel = this.ObjectModel.GetObjectModel(tableInfo.TableId);
			//int idPropertyIndex = objectModel.IdPropertyModel.PropertyIndex;
			//string idFieldName = objectModel.IdPropertyModel.DatastoreFieldName;

			var result = this.GetRecordKeys<long>(tableInfo, idPropertyModel); // SimpleObject.StringPropertyId);

			return new ValueTask<List<long>>(result);
		}

		//public Guid[] GetObjectGuids(TableInfo tableInfo)
		//{
		//	return this.GetRecordKeys<Guid>(tableInfo.TableName, SimpleObject.StringPropertyGuid);
		//}

		//public int[] GetPropertyIndexSequence(ISimpleObjectModel objectModel)
		//{
		//	string[] fieldNames = this.datastoreProvider.GetFieldNames(objectModel.TableInfo.TableName);
		//	int[] result = new int[fieldNames.Length];

		//	for (int i = 0; i < fieldNames.Length; i++)
		//		result[i] = objectModel.PropertyModels[fieldNames[i]].Index;

		//	return result;
		//}

		//public object GetObjectPropertyValueReader(ITableInfo tableInfo, Guid objectKey)
		//{
		//	//SimpleObject result = null;
		//	return this.datastoreProvider.GetRecord(tableInfo.TableName, SimpleObject.StringPropertyKey, objectKey);

		//	//if (dataReader.Read())
		//	//	result = objectCache.CreateObjectFromReader(dataReader);

		//	//dataReader.Close();

		//	//return result;
		//}

		public ValueTask LoadObjectPropertyValues(SimpleObject simpleObject)
		{
			lock (this.lockObject)
			{
				if (simpleObject.Manager.GetObjectCache(simpleObject.GetModel().TableInfo.TableId) is ServerObjectCache objectCache)
				{
					using (IDataReader reader = this.GetRecord(simpleObject.GetModel().TableInfo, simpleObject.GetModel().IdPropertyModel, simpleObject.Id))
					{
						if (reader.Read())
						{
							simpleObject.LoadFrom(reader, objectCache.GetPropertyModelsByDataReaderFieldIndex(reader), simpleObject.Manager.NormalizeWhenReadingFromDatastore, loadOldValuesAlso: true);
						}
						else
						{
							throw new FieldAccessException("The DataReader cannot be read.");
						}

						reader.Close();
					}

					simpleObject.AfterLoad();
				}
				else
				{
					throw new Exception("LoadObjectPropertyValues cann be invoked only on Server");
				}
			}

#if !NETSTANDARD
			return ValueTask.CompletedTask;
#else
			return new ValueTask();
#endif
		}

		//IList<long> IVirtualDatastore.GetObjectIds(TableInfo tableInfo)
		//{
		//	return this.GetObjectIds(tableInfo).Result;
		//}
	}
}
