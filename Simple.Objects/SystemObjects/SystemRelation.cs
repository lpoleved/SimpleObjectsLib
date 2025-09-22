using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Collections;

namespace Simple.Objects
{
	public sealed class SystemRelation : SystemObject<int, SystemRelation>
	{
		static SystemRelation()
		{
			Model.TableInfo = SystemTablesBase.SystemRelations;
			Model.AutoGenerateKey = false;
		}

		public SystemRelation()
		{
		}

		public SystemRelation(SimpleObjectManager objectManager, ref SystemObjectCollectionByObjectKey<int, SystemRelation> dictionaryCollection, int relationKey, string name)
			: base(objectManager, ref dictionaryCollection,
				  (item) =>
				  {
					  item.RelationKey = relationKey;
					  item.Name = name;
				  })
		{
		}

		[ObjectKey]
		[DatastoreType(typeof(int))]
		public int RelationKey { get; set; }

		public string? Name { get; set; }
	}
}