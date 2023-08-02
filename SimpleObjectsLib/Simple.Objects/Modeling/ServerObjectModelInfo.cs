using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple;
using Simple.Collections;
using Simple.Objects;
using Simple.Serialization;
using Simple.Modeling;

namespace Simple.Objects
{
	public class ServerObjectModelInfo
	{
		private HashArray<ServerPropertyInfo> serverPropertyInfosByPropertyIndex = new HashArray<ServerPropertyInfo>();
		private List<int> serializablePropertyIndexes = new List<int>();
		//private List<int> serializablePropertyTypeIds = new List<int>();
		private List<int> storablePropertyIndexes = new List<int>();
		//private List<int> storablePropertyTypeIds = new List<int>();
		private List<int> transactionActionLogPropertyIndexes = new List<int>();
		//private List<int> transactionActionLogPropertyTypeIds = new List<int>();

		public ServerObjectModelInfo(int tableId, string objectName, string tableName, ServerPropertyInfo[] serverPropertyInfos)
		{
			this.TableId = tableId;
			this.ObjectName = objectName;
			this.TableName = tableName;
			this.ServerPropertyInfos = serverPropertyInfos;
			
			this.Initialize();
			//this.SerializablePropertyIndexes = this.serializablePropertyIndexes.ToArray();
			//this.StorablePropertyIndexes = this.storablePropertyIndexes.ToArray();
			//this.TransactionActionLogPropertyIndexes = this.transactionActionLogPropertyIndexes.ToArray();
		}

		public ServerObjectModelInfo(ref SequenceReader reader)
        {
			//this.ServerPropertyInfos = new ServerPropertyInfo[0] ;
			//this.ObjectName = String.Empty;
			//this.TableName = String.Empty;
			
			this.ReadFrom(ref reader);
			this.Initialize();
			//this.SerializablePropertyIndexes = this.serializablePropertyIndexes.ToArray();
			//this.StorablePropertyIndexes = this.storablePropertyIndexes.ToArray();
			//this.TransactionActionLogPropertyIndexes = this.transactionActionLogPropertyIndexes.ToArray();
		}

		private void Initialize()
		{
			foreach (ServerPropertyInfo serverPropertyInfo in this.ServerPropertyInfos)
			{
				this.serverPropertyInfosByPropertyIndex[serverPropertyInfo.PropertyIndex] = serverPropertyInfo;

				if (serverPropertyInfo.IsClientSeriazable)
				{
					this.serializablePropertyIndexes.Add(serverPropertyInfo.PropertyIndex);
					//this.serializablePropertyTypeIds.Add(serverPropertyInfo.PropertyTypeId);
				}

				if (serverPropertyInfo.IsStorable)
				{
					this.storablePropertyIndexes.Add(serverPropertyInfo.PropertyIndex);
					//this.storablePropertyTypeIds.Add(serverPropertyInfo.DatastoreTypeId);
				}

				if (serverPropertyInfo.IncludeInTransactionActionLog)
				{
					this.transactionActionLogPropertyIndexes.Add(serverPropertyInfo.PropertyIndex);
					//this.transactionActionLogPropertyTypeIds.Add(serverPropertyInfo.PropertyTypeId);
				}
			}

			this.SerializablePropertyIndexes = this.serializablePropertyIndexes.ToArray();
			this.StorablePropertyIndexes = this.storablePropertyIndexes.ToArray();
			this.TransactionActionLogPropertyIndexes = this.transactionActionLogPropertyIndexes.ToArray();
		}

		public string ObjectName { get; private set; }
		public int TableId { get; private set; }
		public string TableName { get; private set; }

		public ServerPropertyInfo[] ServerPropertyInfos { get; private set; }

		public int[] SerializablePropertyIndexes { get; private set; }
		public int[] StorablePropertyIndexes { get; private set; }
		public int[] TransactionActionLogPropertyIndexes { get; private set; }

		//public int Count
		//{
		//	get { return this.ServerPropertyInfos.Count(); }
		//}

		public ServerPropertyInfo this[int propertyIndex]
		{
			get { return this.serverPropertyInfosByPropertyIndex[propertyIndex]; }
		}

		public ServerPropertyInfo GetPropertyInfo(int propertyIndex)
		{
			return this.serverPropertyInfosByPropertyIndex.GetValue(propertyIndex);
		}

		public void WriteTo(ref SequenceWriter writer)
		{
			writer.WriteInt32Optimized(this.TableId);
			writer.WriteString(this.ObjectName);
			writer.WriteString(this.TableName);
			writer.WriteInt32Optimized(this.ServerPropertyInfos.Length);

			for (int i = 0; i < this.ServerPropertyInfos.Length; i++)
			{
				ServerPropertyInfo serverPropertyInfo = this.ServerPropertyInfos[i];

				writer.WriteInt32Optimized(serverPropertyInfo.PropertyIndex);
				writer.WriteString(serverPropertyInfo.PropertyName);
				writer.WriteInt32Optimized(serverPropertyInfo.PropertyTypeId);
				writer.WriteInt32Optimized(serverPropertyInfo.DatastoreTypeId);
				writer.WriteBoolean(serverPropertyInfo.IsRelationTableId);
				writer.WriteBoolean(serverPropertyInfo.IsRelationObjectId);
				writer.WriteBoolean(serverPropertyInfo.IsSerializationOptimizable);
				writer.WriteBoolean(serverPropertyInfo.IsClientSeriazable);
				writer.WriteBoolean(serverPropertyInfo.IsStorable);
				writer.WriteBoolean(serverPropertyInfo.IsEncrypted);
				writer.WriteBoolean(serverPropertyInfo.IncludeInTransactionActionLog);

				if (serverPropertyInfo.IsSerializationOptimizable)
					writer.WriteOptimized(serverPropertyInfo.PropertyTypeId, serverPropertyInfo.DefaultValue);
				else
					writer.Write(serverPropertyInfo.PropertyTypeId, serverPropertyInfo.DefaultValue);
			}
		}

		private void ReadFrom(ref SequenceReader reader)
		{
			this.TableId = reader.ReadInt32Optimized();
			this.ObjectName = reader.ReadString();
			this.TableName = reader.ReadString();
			this.ServerPropertyInfos = new ServerPropertyInfo[reader.ReadInt32Optimized()];

			for (int i = 0; i < this.ServerPropertyInfos.Length; i++)
			{
				int propertyIndex = reader.ReadInt32Optimized();
				string propertyName = reader.ReadString();
				int propertyTypeId = reader.ReadInt32Optimized();
				int datastoreTypeId = reader.ReadInt32Optimized();
				bool isRelationTableId = reader.ReadBoolean();
				bool isRelationObjectId = reader.ReadBoolean();
				bool isSerializationOptimizable = reader.ReadBoolean();
				bool isMemberOfSerializationSequence = reader.ReadBoolean();
				bool isStorable = reader.ReadBoolean();
				bool isEncrypted = reader.ReadBoolean();
				bool includeInTransactionActionLog = reader.ReadBoolean();
				object? defaultValue = (isSerializationOptimizable) ? reader.ReadOptimized(propertyTypeId) : reader.Read(propertyTypeId);

				this.ServerPropertyInfos[i] = new ServerPropertyInfo(propertyIndex, propertyName, propertyTypeId, datastoreTypeId, isRelationTableId, isRelationObjectId, isSerializationOptimizable,
																	 isMemberOfSerializationSequence, isStorable, isEncrypted, includeInTransactionActionLog, defaultValue);
			}
		}
	}

	//public class SystemPropertySequenceHolder : IServerPropertySequence
	//{
	//	public SystemPropertySequenceHolder(int[] propertyIndexes, int[] propertyTypeIds)
	//	{
	//		this.PropertyIndexes = propertyIndexes;
	//		this.PropertyTypeIds = propertyTypeIds;
	//	}

	//	public int[] PropertyIndexes { get; private set; }
	//	public int[] PropertyTypeIds { get; private set; }

	//	public int Length
	//	{
	//		get { return this.PropertyIndexes.Length; }
	//	}
	//}
}
