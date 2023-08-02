using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Collections;

namespace Simple.Objects
{
	public sealed class SystemTableInfos : SystemObject<int, SystemTableInfos>
	{
		static SystemTableInfos()
		{
			Model.TableInfo = SystemTables.SystemTableInfos;
			Model.AutoGenerateKey = false;
		}

		public SystemTableInfos()
		{
		}

		public SystemTableInfos(SimpleObjectManager objectManager, ref SystemObjectCollectionByObjectKey<int, SystemTableInfos> dictionaryCollection, int tableId, string tableName)
			: base(objectManager, ref dictionaryCollection, 
				  (item) =>
				  {
					  item.TableId = tableId;
					  item.TableName = tableName;
				  })
		{
		}

		//[DatastoreType(typeof(short))]
		[ObjectKey]
		public int TableId { get; set; }

		public string? TableName { get; set; }

		//protected override string GetPropertyName(string originalPropertyName)
		//{
		//	return (originalPropertyName == "TableId") ? "[TableId]" : originalPropertyName;
		//}
	}
}