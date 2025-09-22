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
	public class ServerObjectModelInfo : ISequenceSerializable
	{
		private HashArray<ServerPropertyInfo> serverPropertyInfosByPropertyIndex = new HashArray<ServerPropertyInfo>();
		private List<int> cliaentSerializablePropertyIndexes = new List<int>();
		private List<int> serverSerializablePropertyIndexes = new List<int>();
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

		public ServerObjectModelInfo(ref SequenceReader reader, object? context)
        {
			//this.ServerPropertyInfos = new ServerPropertyInfo[0] ;
			//this.ObjectName = String.Empty;
			//this.TableName = String.Empty;
			
			this.ReadFrom(ref reader, context);
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

				if (serverPropertyInfo.IsClientToServerSeriazable)
					this.cliaentSerializablePropertyIndexes.Add(serverPropertyInfo.PropertyIndex);

				if (serverPropertyInfo.IsServerToClientSeriazable)
					this.serverSerializablePropertyIndexes.Add(serverPropertyInfo.PropertyIndex);

				if (serverPropertyInfo.IsStorable)
					this.storablePropertyIndexes.Add(serverPropertyInfo.PropertyIndex);

				if (serverPropertyInfo.IncludeInTransactionActionLog)
					this.transactionActionLogPropertyIndexes.Add(serverPropertyInfo.PropertyIndex);
			}

			this.ClientSerializablePropertyIndexes = this.cliaentSerializablePropertyIndexes.ToArray();
			this.ServerSerializablePropertyIndexes = this.serverSerializablePropertyIndexes.ToArray();
			this.StorablePropertyIndexes = this.storablePropertyIndexes.ToArray();
			this.TransactionActionLogPropertyIndexes = this.transactionActionLogPropertyIndexes.ToArray();
		}

		public string ObjectName { get; private set; } = string.Empty;
		public int TableId { get; private set; }
		public string TableName { get; private set; } = string.Empty;

		public ServerPropertyInfo[] ServerPropertyInfos { get; private set; } = new ServerPropertyInfo[0];

		public int[] ClientSerializablePropertyIndexes { get; private set; } = new int[0];
		public int[] ServerSerializablePropertyIndexes { get; private set; } = new int[0];
		public int[] StorablePropertyIndexes { get; private set; } = new int[0];
		public int[] TransactionActionLogPropertyIndexes { get; private set; } = new int[0];

		//public int Count
		//{
		//	get { return this.ServerPropertyInfos.Count(); }
		//}

		public ServerPropertyInfo this[int propertyIndex] => this.serverPropertyInfosByPropertyIndex[propertyIndex];

		public ServerPropertyInfo GetPropertyInfo(int propertyIndex) => this.serverPropertyInfosByPropertyIndex.GetValue(propertyIndex);

		public int GetBufferCapacity() => this.ObjectName.Length + this.TableName.Length + 10;


		public void WriteTo(ref SequenceWriter writer, object? context)
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
				writer.WriteBoolean(serverPropertyInfo.IsRelationZeroValueDatastoreDBNull);
				writer.WriteBoolean(serverPropertyInfo.IsStorable);
				writer.WriteBoolean(serverPropertyInfo.IsSerializationOptimizable);
				writer.WriteBoolean(serverPropertyInfo.IsClientToServerSeriazable);
				writer.WriteBoolean(serverPropertyInfo.IsServerToClientSeriazable);
				writer.WriteBoolean(serverPropertyInfo.IsServerToClientTransactionInfoSeriazable);
				writer.WriteBoolean(serverPropertyInfo.IsEncrypted);
				writer.WriteBoolean(serverPropertyInfo.IncludeInTransactionActionLog);

				if (serverPropertyInfo.IsSerializationOptimizable)
					writer.WriteOptimized(serverPropertyInfo.PropertyTypeId, serverPropertyInfo.DefaultValue);
				else
					writer.Write(serverPropertyInfo.PropertyTypeId, serverPropertyInfo.DefaultValue);
			}
		}

		public void ReadFrom(ref SequenceReader reader, object? context)
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
				bool isRelationZeroValueDatastoreDBNull = reader.ReadBoolean();
				bool isStorable = reader.ReadBoolean();
				bool isSerializationOptimizable = reader.ReadBoolean();
				bool isMemberOfClientToServerSerializationSequence = reader.ReadBoolean();
				bool isMemberOfServerToClientSerializationSequence = reader.ReadBoolean();
				bool isMemberOfClientToServerToClientSerializationSequence = reader.ReadBoolean();
				bool isEncrypted = reader.ReadBoolean();
				bool includeInTransactionActionLog = reader.ReadBoolean();
				object? defaultValue = (isSerializationOptimizable) ? reader.ReadOptimized(propertyTypeId) : reader.Read(propertyTypeId);

				this.ServerPropertyInfos[i] = new ServerPropertyInfo(propertyIndex, propertyName, propertyTypeId, datastoreTypeId, isRelationTableId, isRelationObjectId, isRelationZeroValueDatastoreDBNull, isStorable,
																	 isSerializationOptimizable, isMemberOfClientToServerSerializationSequence, isMemberOfServerToClientSerializationSequence, isMemberOfClientToServerToClientSerializationSequence, 
																	 isEncrypted, includeInTransactionActionLog, defaultValue);
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
