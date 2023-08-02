using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Datastore;

namespace Simple.Objects
{
	public abstract class TableInfoBase<T> where T : TableInfoBase<T>
	{
		public TableInfoBase()
		{
			if (Instance == null)
				Instance = this as T;
		}
		//public Dictionary<int, string> TableNamesByTableId = new Dictionary<int, string>();


		//public void Add(int tableId, string tableName)
		//{
		//	this.TableNamesByTableId.Add(tableId, tableName);
		//}

		public static T Instance { get; private set; }

		public abstract TableInfo GraphElements { get; }
		public abstract TableInfo Folders { get; }
		public abstract TableInfo ManyToManyRelationElements { get; }
	}

	//public interface ITableInfo
	//{
	//	TableModel GraphElements { get; }
	//	TableModel Folders { get; }
	//	TableModel ManyToManyRelationElements { get; }
	//	TableModel Users { get; }
	//}
}
