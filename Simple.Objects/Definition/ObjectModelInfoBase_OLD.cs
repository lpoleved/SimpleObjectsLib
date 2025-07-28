//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Simple.Modeling;
//using Simple.Datastore;

//namespace Simple.Objects
//{
//	public abstract class ObjectModelInfoBase_OLD : ModelElement
//	{
//		private HashSet<int> propertyModelIndexes = new HashSet<int>();

//		public ObjectModelInfoBase_OLD()
//		{
//		}

//		public ObjectModelInfoBase_OLD(TableInfo tableInfo)
//		{
//			this.TableInfo = tableInfo;
//		}

//		public ObjectModelInfoBase_OLD(string objectName, TableInfo tableInfo)
//			: this(tableInfo)
//		{
//			this.ObjectName = objectName;
//		}

//		public void AddProperty(PM propertyModel)
//		{
//			this.PropertyModelsByPropertyName.Add(propertyModel.PropertyName, propertyModel);

//			if (this.propertyModelIndexes.Contains(propertyModel.PropertyIndex))
//				throw new ArgumentException("property index " + propertyModel.PropertyIndex + " is already specified");

//			this.propertyModelIndexes.Add(propertyModel.PropertyIndex);
//		}

//		public string ObjectName { get; set; }
//		public Dictionary<string, PM> PropertyModelsByPropertyName { get; } = new Dictionary<string, PM>();

//		public int NamePropertyIndex { get; set; }
//		public int DescriptionPropertyIndex { get; set; }
//		public int ObjectSubTypePropertyIndex { get; set; }

//		public int[] SerializablePropertyIndexes { get; private set; }
//		public int[] StorablePropertyIndexes { get; private set; }
//		public int[] TransactionActionLogPropertyIndexes { get; private set; }

//		public int TransactionActionLogPropertySequenceId { get; set; }
//		public string[] StorableFieldNameSequence { get; set; }

//		public SerializationModel SerializationModel { get; set; }
//		//IPropertyModel[] StorablePropertyModelSequence { get; }
//		//int[] StorablePropertyIndexSequence { get; }
//		//int StorablePropertySequenceId { get; }
//		//int[] PublicPropertyIndexSequence { get; }

//		//int[] StorablePropertyIndexes { get; }
//		//int StorablePropertyIndexSequenceId { get; }
//		//IPropertyModel[] StorablePropertyModels { get; }

//		//int TableId { get; }
//		//string TableName { get; }
//		public TableInfo TableInfo { get; set; }
//		public string ObjectTypeCaption { get; set; }
//		public bool IsStorable { get; set; }
//		public bool IsSortable { get; set; }
//		public bool FetchAllRecords { get; set; }
//		public bool DeleteSimpleObjectOnLastGraphElementDelete { get; set; }
//		public bool DeleteAllSimpleObjectGraphElementsOnOneGraphElementDelete { get; set; }
//		public bool MustHaveAtLeastOneGraphElement { get; set; }
//		public int MustHaveGraphElementGraphKey { get; set; }
//		public int SortableOneToManyRelationKey { get; set; }
//		public IDictionary<int, IModelElement> ObjectSubTypes { get; set; }

//	}
//}
