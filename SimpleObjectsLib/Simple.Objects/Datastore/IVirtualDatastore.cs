using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Modeling;
using Simple.Datastore;

namespace Simple.Objects
{
	public interface IVirtualDatastore
	{
		//ValueTask<IList<long>> GetObjectIds(TableInfo tableInfo);
		//Guid[] GetObjectGuids(TableInfo tableInfo);
		//int[] GetPropertyIndexSequence(ISimpleObjectModel objectModel);
		//Guid[] GetRecordKeysFromDatastore(ITableInfo tableInfo, IEnumerable<WhereCriteriaElement> whereCriteria)
		//object GetObjectPropertyValueReader(ITableInfo tableInfo, Guid objectKey);

		ValueTask LoadObjectPropertyValues(SimpleObject simpleObject);

	}
}
