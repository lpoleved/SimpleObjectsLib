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
		public ServerPropertyInfo(IPropertyModel propertyModel)
			: this(propertyModel.PropertyIndex, propertyModel.PropertyName, propertyModel.PropertyTypeId, propertyModel.DatastoreTypeId, propertyModel.IsRelationTableId, propertyModel.IsRelationObjectId, propertyModel.IsStorable,
				   propertyModel.IsSerializationOptimizable, propertyModel.IsClientToServerSeriazable, propertyModel.IsServerToClientSeriazable, propertyModel.IsServerToClientTransactionInfoSeriazable,
				   propertyModel.IsEncrypted, propertyModel.IncludeInTransactionActionLog, propertyModel.DefaultValue)
		{
		}

		public ServerPropertyInfo(int propertyIndex, string propertyName, int propertyTypeId, int datastoreTypeId, bool isRelationTableId, bool isRelationObjectId, bool isStorable,
								  bool isSerializationOptimizable, bool isMemberOfClientToServerSerializationSequence, bool isMemberOfServerToClientSerializationSequence, bool isMemberServerToClientTransactionInfoSerializationSequence, 
								  bool isEncrypted, bool includeInTransactionActionLogData, object? defaultValue)
		{
			this.PropertyIndex = propertyIndex;
			this.PropertyName = propertyName;
			this.PropertyTypeId = propertyTypeId;
			this.DatastoreTypeId = datastoreTypeId;
			this.IsRelationTableId = isRelationTableId;
			this.IsRelationObjectId = isRelationObjectId;
			this.IsStorable = isStorable;
			this.IsSerializationOptimizable = isSerializationOptimizable;
			this.IsClientToServerSeriazable = isMemberOfClientToServerSerializationSequence;
			this.IsServerToClientSeriazable = isMemberOfServerToClientSerializationSequence;
			this.IsServerToClientTransactionInfoSeriazable = isMemberServerToClientTransactionInfoSerializationSequence;
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
		public bool IsClientToServerSeriazable { get; private set; }
		public bool IsServerToClientSeriazable { get; private set; }
		public bool IsServerToClientTransactionInfoSeriazable { get; private set; }
		public bool IsStorable { get; private set; }
		public bool IsEncrypted { get; private set; }
		public bool IncludeInTransactionActionLog { get; private set; }
		public object? DefaultValue { get; private set; }
	}
}
