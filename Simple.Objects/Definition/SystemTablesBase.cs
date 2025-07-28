using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Simple;
using Simple.Collections;
using Simple.Datastore;

namespace Simple.Objects
{
	//public class Tables_SAMPLE2 : Tables_SAMPLE
	//{ 
	//	public static readonly TableInfo SAMPLE2             = new TableInfo(10);
	//}

	//public class Tables_SAMPLE : TableBase
	//{
	//	public static readonly new TableInfo GraphElements   = new TableInfo(1);
	//	public static readonly new TableInfo GroupMembership = new TableInfo(2);
	//	public static readonly new TableInfo Folders         = new TableInfo(3);

	//	// Administration & Security Access
	//	public static readonly TableInfo Users				 = new TableInfo(4);
	//	public static readonly TableInfo UserGroups			 = new TableInfo(5);
	//	public static readonly TableInfo UserPolicy			 = new TableInfo(5);

	//	// App specific objects
	//	public static readonly TableInfo SAMPLE             = new TableInfo(10);
	//}


	/// <summary>
	/// Table base class. TableId must greater than zero.
	/// </summary>
	public abstract class SystemTablesBase //: ISystemTableInfo
	{
		//private static SimpleDictionary<int, TableInfo> tableInfosByTableId = new SimpleDictionary<int, TableInfo>();

		//static SimpleObjectTables()
		//{
		//	Type tableNamesMasterClass = ReflectionHelper.FindTopInheritedTypeInAssembly<SimpleObjectTables>();
		//	SimpleObjectTables tableNamesMaster = Activator.CreateInstance(tableNamesMasterClass) as SimpleObjectTables;
		//	List<FieldInfo> tableNamesMasterFields = ReflectionHelper.GetFieldInfos(tableNamesMaster, orderFromBaseType: false);

		//	foreach (FieldInfo fieldInfo in tableNamesMasterFields)
		//	{
		//		object fieldValue = ReflectionHelper.GetFieldObject(fieldInfo, tableNamesMaster);

		//		if (fieldValue != null && fieldValue is TableInfo)
		//		{
		//			string tableName = fieldInfo.Name;
		//			TableInfo tableInfo = (TableInfo)fieldValue;
		//			TableInfo tableInfoWithSameTableName = tableInfosByTableId.Values.FirstOrDefault(item => item.TableName == tableName);

		//			if (tableInfoWithSameTableName.Equals(default(TableInfo)))
		//			{
		//				if (tableInfo.TableId <= 0)
		//					throw new Exception(string.Format("Table definition error: TableName = {0}", tableName));

		//				if (tableInfosByTableId.ContainsKey(tableInfo.TableId))
		//					throw new Exception(string.Format("Table definition error: Table is already defined, TableName = {0}", tableName));

		//				tableInfo.TableName = tableName;
		//				tableInfosByTableId.Add(tableInfo.TableId, tableInfo);
		//			}
		//			else // The field is duplicate (overriden) and require only update of TableId and TableName field values
		//			{
		//				tableInfo.TableId = tableInfoWithSameTableName.TableId;
		//				tableInfo.TableName = tableInfoWithSameTableName.TableName;
		//			}

		//			fieldInfo.SetValue(tableNamesMaster, tableInfo);
		//		}
		//	}
		//}

		// SimpleObject Tables
		public static TableInfo GraphElements;
		public static TableInfo GroupMembership;
		public static TableInfo Folders;

		// SystemObject Tables
		public static TableInfo SystemTables			 = new TableInfo( 1, isSystemTable: true); // {  TableName = "SystemTables" };
		public static TableInfo SystemGraphs			 = new TableInfo( 2, isSystemTable: true);
		public static TableInfo SystemTransactions		 = new TableInfo( 3, isSystemTable: true);
		public static TableInfo SystemTransactionActions = new TableInfo( 4, isSystemTable: true);
		public static TableInfo SystemProperties		 = new TableInfo( 5, isSystemTable: true);
		public static TableInfo SystemGlobalObjectIds	 = new TableInfo( 6, isSystemTable: true);
		public static TableInfo SystemRelations		     = new TableInfo( 7, isSystemTable: true);
		public static TableInfo SystemClients			 = new TableInfo( 8, isSystemTable: true);
		public static TableInfo SystemServers			 = new TableInfo( 9, isSystemTable: true);
		public static TableInfo SystemSettings			 = new TableInfo(10, isSystemTable: true);
		public static TableInfo SystemPropertySequences  = new TableInfo(11, isSystemTable: true);




		//public static TableModel Users; 
		//public static TableModel UserGroups;
		//public static TableModel UserPolicy;

		//// Administration & Security Access
		//public static TableModel Users;
	}

	//public interface ISystemTableInfo
	//{
	//	TableModel GraphElements { get; }
	//	TableModel GroupMembership { get; }
	//	TableModel Folders { get; }

	//	// System Tables
	//	TableModel SystemTables { get; }
	//	TableModel SystemGraphs { get; }
	//	TableModel SystemTransactions { get; }
	//	TableModel SystemTransactionActions { get; }
	//	TableModel SystemProperties { get; }
	//	TableModel SystemGlobalObjectIds { get; }
	//	TableModel SystemRelations { get; }
	//	TableModel SystemClients { get; }
	//	TableModel SystemServers { get; }
	//	TableModel SystemSettings { get; }
	//	TableModel SystemPropertySequences { get; }
	//}
}
