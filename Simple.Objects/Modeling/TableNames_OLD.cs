//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Reflection;
//using Simple;
//using Simple.Collections;

//namespace Simple.Objects
//{
//	//public abstract class TableNames<T> : TableNames
//	//	where T : TableNames<T>, new()
//	//{
//	//	public TableNames()
//	//	{
//	//		if (instance == null)
//	//			instance = this;
//	//	}

//	//	public static T Instance
//	//	{
//	//		get { return GetInstance<T>(); }
//	//	}
//	//}
	
//	public class TableNames
//    {
//        private static object lockObjectInstance = new object();
//		protected static TableNames instance = null;
        
//        private static SimpleDictionary<int, TableInfo> tableInfosByTableId = new SimpleDictionary<int,TableInfo>();
//        private static SimpleDictionary<string, TableInfo> tableInfosByTableName = new SimpleDictionary<string,TableInfo>();

//		//// System Tables
//		//public static readonly TableInfo SystemSettings				= new TableInfo(-2);
//		//public static readonly TableInfo SystemTableNames			= new TableInfo(-3);
//		//public static readonly TableInfo SystemServers				= new TableInfo(-4);
//		//public static readonly TableInfo SystemClients				= new TableInfo(-5);
//		//public static readonly TableInfo SystemTransactions			= new TableInfo(-6);
//		//public static readonly TableInfo SystemTransactionActions	= new TableInfo(-7);

//		// User System Tables
//		public static readonly TableInfo GraphElements              = new TableInfo(1);
//		public static readonly TableInfo Folders                    = new TableInfo(2);
//		public static readonly TableInfo ManyToManyRelationElements = new TableInfo(3);
		
//		// Administration & Security Access
//		public static readonly TableInfo Users						= new TableInfo(4);
//		//public static readonly TableInfo UserGroups				= new TableInfo(5);
		
//		static TableNames()
//        {
//			List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
//			Type tableNamesMasterClass = ReflectionHelper.FindTopInheritedTypeInAssembly<TableNames>(assemblies);
//			instance = Activator.CreateInstance(tableNamesMasterClass) as TableNames;

//			//if (instance == null)
//			//{
//			//	instance = this;
//			//}
//			//else
//			//{
//			//	return;
//			//}
			
//			IDictionary<string, TableInfo> fieldsByName = ReflectionHelper.GetFieldsByName<TableInfo>(instance);
//			int tableId = 0;

//			// Create dynamic TableIds for not specified TableId's objects (non storable objects, e.g.), first
//			foreach (KeyValuePair<string, TableInfo> keyValuePair in fieldsByName)
//			{
//				if (keyValuePair.Value.TableId == -1)
//				{
//					while (fieldsByName.Values.FirstOrDefault(tableInfo => tableInfo.TableId == tableId) != null)
//						tableId++;

//					keyValuePair.Value.TableId = tableId;
//				}
//			}

//            foreach (KeyValuePair<string, TableInfo> keyValuePair in fieldsByName)
//            {
//                TableInfo tableInfo = keyValuePair.Value;

//                if (String.IsNullOrEmpty(tableInfo.TableName))
//                    tableInfo.TableName = keyValuePair.Key;

//				if (tableInfosByTableId.ContainsKey(tableInfo.TableId))
//				{
//					throw new ArgumentException("Specified TableId=" + tableInfo.TableId.ToString() + " is already specified in TableNames definition class.");
//				}
//				else
//				{
//					tableInfosByTableId.Add(tableInfo.TableId, tableInfo);
//				}

//				if (tableInfosByTableName.ContainsKey(tableInfo.TableName))
//				{
//					throw new ArgumentException("Specified table name '" + tableInfo.TableName + "'  is already specified in TableNames definition class.");
//				}
//				else
//				{
//					tableInfosByTableName.Add(tableInfo.TableName, tableInfo);
//				}
//            }
//        }

//        public static IEnumerable<TableInfo> Collection
//        {
//            get { return tableInfosByTableId.Values; }
//        }

//        public static IDictionary<int, TableInfo> TableInfosByTableId
//        {
//            get { return tableInfosByTableId.AsReadOnly(); }
//        }

//        public static IDictionary<string, TableInfo> TableInfosByTableName
//        {
//            get { return tableInfosByTableName.AsReadOnly(); }
//        }

//		//public static TableNames Instance
//		//{
//		//	get { return GetInstance<TableNames>(); }
//		//}

//		protected static T GetInstance<T>() where T : TableNames, new()
//		{
//			lock (lockObjectInstance)
//			{
//				if (instance == null)
//					instance = new T();
//			}

//			return instance as T;
//		}
//    }
//}
