using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple;
using Simple.Modeling;
using Simple.Serialization;

namespace Simple.Objects
{
	public class ServerPropertyInfo : IServerPropertyInfo
	{
		public ServerPropertyInfo(IPropertyModel	propertyModel)
			: this(propertyModel.PropertyIndex,     propertyModel.PropertyName,		  propertyModel.PropertyTypeId,				   propertyModel.DatastoreTypeId, 
				   propertyModel.IsRelationTableId, propertyModel.IsRelationObjectId, propertyModel.IsSerializationOptimizable,	   propertyModel.IsClientSeriazable, 
				   propertyModel.IsStorable,		propertyModel.IsEncrypted,		  propertyModel.IncludeInTransactionActionLog, propertyModel.DefaultValue)
		{
		}
		
		public ServerPropertyInfo(int propertyIndex,      string propertyName,        int propertyTypeId,						   int datastoreTypeId, 
								  bool isRelationTableId, bool isRelationObjectId,    bool isSerializationOptimizable,            bool isMemberOfSerializationSequence, 
								  bool isStorable,        bool isEncrypted,		      bool includeInTransactionActionLogData,  object? defaultValue)
		{
			this.PropertyIndex = propertyIndex;
			this.PropertyName = propertyName;
			this.PropertyTypeId = propertyTypeId;
			this.DatastoreTypeId = datastoreTypeId;
			this.IsRelationTableId = isRelationTableId;
			this.IsRelationObjectId = isRelationObjectId;
			this.IsSerializationOptimizable = isSerializationOptimizable;
			this.IsClientSeriazable = isMemberOfSerializationSequence;
			this.IsStorable = isStorable;
			this.IsEncrypted = isEncrypted;
			this.IncludeInTransactionActionLog = includeInTransactionActionLogData;
			this.DefaultValue = defaultValue;
		}

		public int PropertyIndex { get; private set; }
		public string PropertyName { get; private set; }
		public int PropertyTypeId { get; private set; }
		public int DatastoreTypeId { get; private set; }
		public bool IsRelationTableId { get; private set; }
		public bool IsRelationObjectId { get; private set; }
		public bool IsSerializationOptimizable { get; private set; }
		public bool IsClientSeriazable { get; private set; }
		public bool IsStorable { get; private set; }
		public bool IsEncrypted { get; private set; }
		public bool IncludeInTransactionActionLog { get; private set; }
		public object? DefaultValue { get; private set; }
	}
}
