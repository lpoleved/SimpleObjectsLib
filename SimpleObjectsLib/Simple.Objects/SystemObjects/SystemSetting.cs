using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects
{
	public sealed class SystemSetting : SystemObject<string, SystemSetting>
    {
		static SystemSetting()
		{
			Model.TableInfo = SystemTablesBase.SystemSettings;
			Model.AutoGenerateKey = false;
		}

		public SystemSetting()
		{
		}

		public SystemSetting(SimpleObjectManager objectManager, ref SystemObjectCollectionByObjectKey<string, SystemSetting> dictionaryCollection, string name, object value)
			: base(objectManager, ref dictionaryCollection, 
				  (item) =>
				  {
					  item.Name = name;
					  item.Value = value;
				  })
		{
		}

		[ObjectKey]
		public string? Name { get; set; }

		public object? Value { get; set; }
    }
}
