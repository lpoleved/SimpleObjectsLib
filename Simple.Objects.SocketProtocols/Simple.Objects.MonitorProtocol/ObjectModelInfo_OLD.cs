//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Simple.Serialization;
//using Simple.Modeling;
//using Simple.Objects;

//namespace Simple.Objects.MonitorProtocol
//{
//	public class ObjectModelInfo : SerializableObject, ISerialization
//	{
//		public ObjectModelInfo(SerializationReader reader)
//			: base(reader)
//		{
//		}

//		public ObjectModelInfo(SimpleObjectModel objectModel)
//									 //int tableId, string tableName, Type objectType, string objectTypeCaption, bool isStorable, bool IsSortable, bool FetchAllRecords, 
//									 //bool deleteSimpleObjectOnLastGraphElementDelete, bool deleteAllSimpleObjectGraphElementsOnOneGraphElementDelete,
//									 //bool mustHaveAtLeastOneGraphElement, bool mustHaveGraphElementGraphKey, bool sortableOneToManyRelationKey, 
//									 //IEnumerable<PropertyModelInfo> propertyModelInfoCollection, int[] serializablePropertyIndexSequence, int[] storablePropertyIndexSequence)
//		{
//			this.TableId = objectModel.TableInfo.TableId;
//			this.TableName = objectModel.TableInfo.TableName;
//			this.ObjectType = objectModel.ObjectType;
//			this.ObjectTypeCaption = objectModel.ObjectTypeCaption;
//			this.IsStorable = objectModel.IsStorable;
//			this.IsSortable = objectModel.IsSortable;
//			this.FetchAllRecords = objectModel.FetchAllRecords;
//			this.DeleteSimpleObjectOnLastGraphElementDelete = objectModel.DeleteSimpleObjectOnLastGraphElementDelete;
//			this.DeleteAllSimpleObjectGraphElementsOnOneGraphElementDelete = objectModel.DeleteAllSimpleObjectGraphElementsOnOneGraphElementDelete;
//			this.MustHaveAtLeastOneGraphElement = objectModel.MustHaveAtLeastOneGraphElement;
//			this.MustHaveGraphElementGraphKey = objectModel.MustHaveGraphElementGraphKey;
//			this.SortableOneToManyRelationKey = objectModel.SortableOneToManyRelationKey;
//			this.PropertyModels = objectModel.PropertyModels;
//			this.SerializablePropertySequence = objectModel.SerializablePropertySequence;
//			this.StorablePropertySequence = objectModel.StorablePropertySequence;
//		}

//		public int TableId { get; private set; }
//		public string TableName { get; private set; }
//		public Type ObjectType { get; private set; }
//		public string ObjectTypeCaption { get; private set; }
//		public bool IsStorable { get; private set; }
//		public bool IsSortable { get; private set; }
//		public bool FetchAllRecords { get; private set; }
//		public bool DeleteSimpleObjectOnLastGraphElementDelete { get; private set; }
//		public bool DeleteAllSimpleObjectGraphElementsOnOneGraphElementDelete { get; private set; }
//		public bool MustHaveAtLeastOneGraphElement { get; private set; }
//		public int MustHaveGraphElementGraphKey { get; private set; }
//		public int SortableOneToManyRelationKey { get; private set; }

//		public PropertyModelCollection<PropertyModel> PropertyModels { get; private set; }
//		public PropertySequence SerializablePropertySequence { get; private set; }
//		public PropertySequence StorablePropertySequence { get; private set; }
//	}
//}
