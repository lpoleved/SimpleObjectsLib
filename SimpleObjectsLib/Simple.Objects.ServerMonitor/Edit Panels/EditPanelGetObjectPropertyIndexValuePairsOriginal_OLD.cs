//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using Simple.Modeling;
//using Simple.Serialization;
//using Simple.Objects;
//using Simple.Objects.SocketProtocol;
//using Simple.SocketEngine;
//using DevExpress.XtraTreeList.Columns;
//using DevExpress.XtraTreeList.Nodes;

//namespace Simple.Objects.ServerMonitor
//{
//	[SystemRequestArgs((int)SystemRequest.GetObjectPropertyIndexValuePairs)]
//	public abstract partial class EditPanelGetObjectPropertyIndexValuePairsOriginal : EditPanelSessionPackageRequestResponse
//	{
//		private int columnIndexIndex, columnNameIndex, columnValueIndex;

//		public EditPanelGetObjectPropertyIndexValuePairsOriginal()
//		{
//			InitializeComponent();

//			TreeListColumn column = this.treeListObjectPropertyValues.Columns.Add();

//			this.columnIndexIndex = column.AbsoluteIndex;
//			column.Name = column.Caption = "Index";
//			column.Visible = true;

//			column = this.treeListObjectPropertyValues.Columns.Add();
//			this.columnNameIndex = column.AbsoluteIndex;
//			column.Name = column.Caption = "Name";
//			column.Visible = true;

//			column = this.treeListObjectPropertyValues.Columns.Add();
//			this.columnValueIndex = column.AbsoluteIndex;
//			column.Name = column.Caption = "Value";
//			column.Visible = true;
//		}

//		protected override void OnRefreshBindingObject()
//		{
//			base.OnRefreshBindingObject();

//			if (this.PackageInfoRow?.RequestOrMessagePackageInfo.PackageArgs is ObjectIdTableIdRequestArgs requestArgs && this.PackageInfoRow.ResponsePackageInfo.PackageArgs is GetObjectPropertyValuesResponseArgs responseArgs)
//			{
//				this.editorObjectId.Text = String.Format("{0}.{1}  ({2})", requestArgs.TableId, requestArgs.ObjectId, responseArgs.ObjectModel.ObjectName);
//				this.treeListObjectPropertyValues.BeginUnboundLoad();
//				this.treeListObjectPropertyValues.ClearNodes();

//				foreach (var item in responseArgs.PropertyValuesByPropertyIndex)
//				{
//					int propertyIndex = item.Key;
//					string propertyName = responseArgs.ObjectModel[propertyIndex].PropertyName;
//					string propertyValue = (item.Value is null) ? "null" : item.Value.ToString() ?? "null";

//					TreeListNode node = this.treeListObjectPropertyValues.AppendNode(null, null, null);

//					node.SetValue(columnIndexIndex, propertyIndex);
//					node.SetValue(columnNameIndex, propertyName);
//					node.SetValue(columnValueIndex, propertyValue);
//				}

//				this.treeListObjectPropertyValues.Columns[this.columnIndexIndex].BestFit();
//				this.treeListObjectPropertyValues.Columns[this.columnNameIndex].BestFit();
//				//this.treeListObjectPropertyValues.Columns[this.columnValueIndex].BestFit();
//				//this.treeListObjectPropertyValues.BestFitColumns();

//				this.treeListObjectPropertyValues.EndUnboundLoad();
//			}
//		}

//		protected override RequestArgs CreateRequestArgs() => new ObjectIdTableIdRequestArgs();

//		protected override ResponseArgs CreateResponseArgs()
//		{
//			int tableId = -1;
//			if (this.PackageInfoRow?.RequestOrMessagePackageInfo.PackageArgs is ObjectIdTableIdRequestArgs requesrArgs)
//				tableId = requesrArgs.TableId;

//			return this.CreateGetObjectPropertyValuesResponseArgs(tableId);
//		}

//		protected virtual GetObjectPropertyValuesResponseArgs CreateGetObjectPropertyValuesResponseArgs(int tableId)
//		{
//			return new GetObjectPropertyValuesResponseArgs(tableId, skipReadingKey: true, serializationModel: SerializationModel.SequenceValuesOnly, defaultOptimization: false);
//		}
//	}

//	//public class GetObjectSequencePropertyValuesRequestArgs : GetObjectPropertyValuesResponseArgs
//	//{
//	//	public GetObjectSequencePropertyValuesRequestArgs(SerializationReader reader, Guid objectKey)
//	//		: base(reader, objectKey)
//	//	{
//	//	}

//	//	protected override Dictionary<int, object> CreatePropertyValuesByPropertyIndexDictionary(SerializationReader reader)
//	//	{
//	//		Dictionary<int, object> result = new Dictionary<int, object>(this.ObjectModel.SerializablePropertySequence.ModelSequence.Length);

//	//		foreach (IPropertyModel propertyModel in this.ObjectModel.SerializablePropertySequence.ModelSequence)
//	//		{
//	//			if (propertyModel.Index == SimpleObject.IndexPropertyKey)
//	//				continue;

//	//			object propertyValue = this.ReadObjectPropertyValue(reader, propertyModel, SimpleObjectManager.NormalizeWhenReadingByPropertyTypeWithoutDecription, defaultOptimization: false);
//	//			propertyValue = this.GetCustomizedPropertyValue(propertyModel, propertyValue);

//	//			result.Add(propertyModel.Index, propertyValue);
//	//		}

//	//		return result;
//	//	}
//	//}

//	public class GetObjectPropertyValuesResponseArgs : ResponseArgs
//	{
//		private int tableId;
//		private bool skipReadingKey;
//		private SerializationModel serializationModel;
//		private bool defaultOptimization;

//		public GetObjectPropertyValuesResponseArgs(int tableId, bool skipReadingKey, SerializationModel serializationModel, bool defaultOptimization)
//		{
//			this.tableId = tableId;
//			this.skipReadingKey = skipReadingKey;
//			this.serializationModel = serializationModel;
//			this.defaultOptimization = defaultOptimization;

//			this.ObjectModel = Program.MonitorClient.GetServerObjectModelInfo(tableId).GetAwaiter().GetResult();
//		}

//		public ServerObjectModelInfo? ObjectModel { get; private set; }
//		public Dictionary<int, object>? PropertyValuesByPropertyIndex { get; private set; }

//		public override void ReadFrom(ref SequenceReader reader, object context)
//		{
//			base.ReadFrom(ref reader, context);
			
//			int propertyCount;
//			bool checkIfPropertyValueIsDefault = false; // if the SerializationModel.SequenceValuesOnly sequence reader reads only reads values

//			if (serializationModel == SerializationModel.IndexValuePairs)
//			{
//				propertyCount = reader.ReadInt32Optimized();
//				checkIfPropertyValueIsDefault = false; // prevent reading single property value optimized in index/value pairs serialization mode.
//			}
//			else
//			{
//				propertyCount = this.ObjectModel!.SerializablePropertyIndexes.Length; // This should be the same value with this.GetModel().SerializablePropertySequence.Length;
//				checkIfPropertyValueIsDefault = this.defaultOptimization;
//			}

//			//this.ObjectModel = Program.AppClient.GetServerObjectModelInfoFromCache(this.tableId); //Program.ModelDiscovery.GetObjectModel(tableId);
//			//this.ObjectModel = Program.ModelDiscovery.GetObjectModel(tableId);
//			this.PropertyValuesByPropertyIndex = new Dictionary<int, object>(propertyCount);

//			for (int i = 0; i < propertyCount; i++)
//			{
//				int propertyIndex = (serializationModel == SerializationModel.IndexValuePairs) ? reader.ReadInt32Optimized() : this.ObjectModel!.SerializablePropertyIndexes[i];
//				var propertyModel = this.ObjectModel![propertyIndex];

//				if (skipReadingKey && propertyModel.PropertyIndex == SimpleObject.IndexPropertyId && serializationModel == SerializationModel.SequenceValuesOnly)
//					continue;

//				object? propertyValue = this.ReadObjectPropertyValue(ref reader, propertyModel, SimpleObjectManager.NormalizeWhenReadingByPropertyTypeWithoutDecryption, checkIfPropertyValueIsDefault);

//				propertyValue = this.GetCustomizedPropertyValue(propertyModel, propertyValue);
//				this.PropertyValuesByPropertyIndex.Add(propertyModel.PropertyIndex, propertyValue);
//			}
//		}

//		protected object? ReadObjectPropertyValue(ref SequenceReader reader, ServerPropertyInfo propertyModel, Func<IServerPropertyInfo, object?, object?> readNormalizer, bool checkIfPropertyValueIsDefault)
//		{
//			object? result = SimpleObjectManager.ReadObjectPropertyValue(ref reader, propertyModel, checkIfPropertyValueIsDefault);

//			result = readNormalizer(propertyModel, result);

//			return result;
//		}

//		protected object? GetCustomizedPropertyValue(IServerPropertyInfo propertyModel, object? propertyValue)
//		{
//			//if (propertyValue != null && (propertyModel.PropertyType == typeof(Guid) || propertyModel.PropertyType == typeof(Guid?)) && (propertyModel.IsKey || propertyModel.IsRelationKey))
//			//	propertyValue = String.Format("{0} ({1})", propertyValue.ToString(), ((Guid)propertyValue).ToObjectKeyString());

//			return propertyValue;
//		}

//		public override void WriteTo(ref SequenceWriter writer, object context)
//		{
//			throw new NotImplementedException();
//		}
//	}
//}
