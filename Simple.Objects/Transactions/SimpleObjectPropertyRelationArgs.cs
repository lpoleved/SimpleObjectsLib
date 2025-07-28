using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Modeling;

namespace Simple.Objects
{
	public struct SimpleObjectPropertyRelationArgs
	{
		public SimpleObjectPropertyRelationArgs(SimpleObject simpleObject, int foreignTableId, long foreignObjectId, IOneToOneOrManyRelationModel relationModel) // IPropertyModel foreignObjectIdPropertyModel)
		{
			this.SimpleObject = simpleObject;
			this.ForeignTableId = foreignTableId;
			this.ForeignObjectId = foreignObjectId;
			this.RelationModel = relationModel;
			//this.ForeignObjectIdPropertyModel = foreignObjectIdPropertyModel;
		}

		public SimpleObject SimpleObject { get; private set; }
		//public IOneToOneOrManyRelationModel RelationModel { get; private set; }
		public int ForeignTableId { get; private set; }
		public long ForeignObjectId { get; private set; }
		public IOneToOneOrManyRelationModel RelationModel { get; private set; }
		//public IPropertyModel ForeignObjectIdPropertyModel { get; private set; }
	}
}
