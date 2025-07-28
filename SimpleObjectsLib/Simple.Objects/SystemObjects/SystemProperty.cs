using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Collections;

namespace Simple.Objects
{
	public sealed class SystemProperty : SystemObject<int, SystemProperty>
	{
		static SystemProperty()
		{
			Model.TableInfo = SystemTablesBase.SystemProperties;
			Model.AutoGenerateKey = true;
		}

		public SystemProperty()
		{
		}

		public SystemProperty(SimpleObjectManager objectManager, ref SystemObjectCollectionByObjectKey<int, SystemProperty> dictionaryCollection, int tableId, DateTime creationTime, int propertyTypeId, int propertyIndex, string propertyName)
			: base(objectManager, ref dictionaryCollection, 
				  (item) =>
				  {
					  item.TableId = tableId;
					  item.CreationTime = creationTime;
					  item.PropertyTypeId = propertyTypeId;
					  item.PropertyIndex = propertyIndex;
					  item.PropertyName = propertyName;
				  })
		{
		}

		[ObjectKey]
		public int PropertyId { get; set; }

		public int PropertyIndex { get; set; }
		public int TableId { get; set; }
		public int PropertyTypeId { get; set; }
		public string? PropertyName { get; set; }
		public DateTime CreationTime { get; private set; }
	}
}