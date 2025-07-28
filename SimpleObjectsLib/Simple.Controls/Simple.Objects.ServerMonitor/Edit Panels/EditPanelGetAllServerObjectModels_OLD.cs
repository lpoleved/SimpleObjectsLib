//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using Simple;
//using Simple.Modeling;
//using Simple.Objects;
//using Simple.Objects.SocketProtocol;
//using Simple.Controls;
//using Simple.SocketEngine;
//using DevExpress.XtraTreeList.Columns;
//using DevExpress.XtraTreeList.Nodes;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
////using DevExpress.XtraGrid.Columns;
////using DevExpress.XtraEditors.Repository;

//namespace Simple.Objects.ServerMonitor
//{
//	[SystemRequestArgs((int)SystemRequest.GetAllServerObjectModels)]
//	public partial class EditPaneGetAllServerObjectModels_OLD : EditPanelSessionPackageRequestResponse
//	{
//		private int columnIndexIndex, columnNameIndex, columnObjectTypeIdIndex, columnObjectTypeNameIndex;
//		private int columnDatastoreTypeIdIndex, columnIsRelationTableIdIndex, columnIsRelationObjectIdIndex;
//		private int columnIsSerializationOptimizableIndex, columnIsClientSeriazableIndex, columnIsServerSeriazableIndex;
//		private int columnIsStorableIndex, columnIsEncryptedIndex, columnIncludeInTransactionActionLogIndex, columnDefaultValueIndex;
//		//private GridColumn columnTableId, columnObjectName, columnPropertyIndexSequence;
//		//private int columnNoIndex, columnTableIdIndex, columnObjectNameIndex, columnPropertyIndexSequence;
//		//private BindingList<ServerPropertySequenceRow> dataSourceServerPropertySequences = new BindingList<ServerPropertySequenceRow>();
//		//private RepositoryItemMemoEdit repositoryItempServerPopertyIndexSequence = new RepositoryItemMemoEdit();
//		//private bool isFirst = true;

//		public EditPaneGetAllServerObjectModels_OLD()
//		{
//			InitializeComponent();

//			TreeListColumn column = this.treeListServerObjectModelsPropertyValues.Columns.Add();

//			// int PropertyIndex
//			this.columnIndexIndex = column.AbsoluteIndex;
//			column.Name = column.Caption = "Index";
//			column.Visible = true;

//			// string PropertyName
//			column = this.treeListServerObjectModelsPropertyValues.Columns.Add();
//			this.columnNameIndex = column.AbsoluteIndex;
//			column.Name = column.Caption = "Name";
//			column.Visible = true;

//			column = this.treeListServerObjectModelsPropertyValues.Columns.Add();
//			this.columnObjectTypeNameIndex = column.AbsoluteIndex;
//			column.Name = column.Caption = "Type";
//			column.Visible = true;

//			// int PropertyTypeId
//			column = this.treeListServerObjectModelsPropertyValues.Columns.Add();
//			this.columnObjectTypeIdIndex = column.AbsoluteIndex;
//			column.Name = column.Caption = "TypeId";
//			column.Visible = true;

//			// int DatastoreTypeId
//			column = this.treeListServerObjectModelsPropertyValues.Columns.Add();
//			this.columnDatastoreTypeIdIndex = column.AbsoluteIndex;
//			column.Name = column.Caption = "Datastore TypeId";
//			column.Visible = true;

//			// bool IsRelationTableId 
//			column = this.treeListServerObjectModelsPropertyValues.Columns.Add();
//			this.columnIsRelationTableIdIndex = column.AbsoluteIndex;
//			column.Name = column.Caption = "IsRelationTableId";
//			column.Visible = true;

//			// bool IsRelationObjectId
//			column = this.treeListServerObjectModelsPropertyValues.Columns.Add();
//			this.columnIsRelationObjectIdIndex = column.AbsoluteIndex;
//			column.Name = column.Caption = "IsRelationObjectId";
//			column.Visible = true;

//			//bool IsSerializationOptimizable
//			column = this.treeListServerObjectModelsPropertyValues.Columns.Add();
//			this.columnIsSerializationOptimizableIndex = column.AbsoluteIndex;
//			column.Name = "IsSerializationOptimizable";
//			column.Caption = "IsSerializationOptimizable";
//			column.Visible = true;

//			// bool IsMemberOfClientSerializationSequence
//			column = this.treeListServerObjectModelsPropertyValues.Columns.Add();
//			this.columnIsClientSeriazableIndex = column.AbsoluteIndex;
//			column.Name = "IsClientSeriazable";
//			column.Caption = "IsClientSeriazable";
//			column.Visible = true;

//			// bool IsMemberOfServerSerializationSequence
//			column = this.treeListServerObjectModelsPropertyValues.Columns.Add();
//			this.columnIsServerSeriazableIndex = column.AbsoluteIndex;
//			column.Name = "IsServerSeriazable";
//			column.Caption = "IsServerSeriazable";
//			column.Visible = true;

//			// bool IsStorable 
//			column = this.treeListServerObjectModelsPropertyValues.Columns.Add();
//			this.columnIsStorableIndex = column.AbsoluteIndex;
//			column.Name = column.Caption = "IsStorable";
//			column.Visible = true;

//			// bool IsEncrypted
//			column = this.treeListServerObjectModelsPropertyValues.Columns.Add();
//			this.columnIsEncryptedIndex = column.AbsoluteIndex;
//			column.Name = column.Caption = "IsEncrypted";
//			column.Visible = true;

//			// bool IncludeInTransactionActionLog
//			column = this.treeListServerObjectModelsPropertyValues.Columns.Add();
//			this.columnIncludeInTransactionActionLogIndex = column.AbsoluteIndex;
//			column.Name = column.Caption = "IncludeInTransactionActionLog";
//			column.Visible = true;

//			// object? DefaultValue
//			column = this.treeListServerObjectModelsPropertyValues.Columns.Add();
//			this.columnDefaultValueIndex = column.AbsoluteIndex;
//			column.Name = column.Caption = "DefaultValue";
//			column.Visible = true;

//			this.treeListServerObjectModelsPropertyValues.OptionsView.ShowIndicator = false;
//			this.treeListServerObjectModelsPropertyValues.OptionsView.AutoWidth = false;
//		}

//		protected override void OnRefreshBindingObject()
//		{
//			base.OnRefreshBindingObject();

//			AllServerObjectModelsResponseArgs_OLD? responseArgs = this.PackageInfoRow?.ResponsePackageInfo?.PackageArgs as AllServerObjectModelsResponseArgs_OLD;

//			//int? tableId = requestArgs?.TableId;
//			//ISimpleObjectModel objectModel = Program.ModelDiscovery.GetObjectModel(tableId); // <- avoid directly using model discovery
//			//this.editorTableId.Text = String.Format("{0} ({1})", tableId, responseArgs?.ServerObjectModelInfo?.ObjectName);
//			//this.editorObjectName.Text = responseArgs?.ServerObjectModelInfo?.ObjectName;
//			//this.editorTableName.Text = responseArgs?.ServerObjectModelInfo?.TableName;
//			this.labelControlServerObjectModelCount.Text = responseArgs?.ServerObjectModels?.Length.ToString();

//			this.treeListServerObjectModelsPropertyValues.BeginUnboundLoad();
//			this.treeListServerObjectModelsPropertyValues.ClearNodes();

//			if (responseArgs?.ServerObjectModels != null)
//			{
//				foreach (ServerObjectModelInfo serverObjectModel in responseArgs.ServerObjectModels)
//				{
//					TreeListNode root = this.treeListServerObjectModelsPropertyValues.AppendNode(nodeData: null, parentNode: null, tag: null);

//					root.SetValue(this.columnIndexIndex, serverObjectModel.TableId);
//					root.SetValue(this.columnNameIndex, serverObjectModel.ObjectName); // $"{serverObjectModel.ObjectName}, TableName={serverObjectModel.TableName}");

//					for (int i = 0; i < serverObjectModel.ServerPropertyInfos.Count(); i++)
//					{
//						ServerPropertyInfo serverPropertyInfo = serverObjectModel.ServerPropertyInfos.ElementAt(i);
//						Type propertyObjectType = PropertyTypes.GetPropertyType(serverPropertyInfo.PropertyTypeId);
//						TreeListNode node = this.treeListServerObjectModelsPropertyValues.AppendNode(nodeData: null, parentNode: root, tag: null);

//						node.SetValue(this.columnIndexIndex, serverPropertyInfo.PropertyIndex);
//						node.SetValue(this.columnNameIndex, serverPropertyInfo.PropertyName);
//						node.SetValue(this.columnObjectTypeNameIndex, (propertyObjectType != null) ? ReflectionHelper.GetTypeName(propertyObjectType) : String.Empty);
//						node.SetValue(this.columnObjectTypeIdIndex, serverPropertyInfo.PropertyTypeId);
//						node.SetValue(this.columnDatastoreTypeIdIndex, serverPropertyInfo.DatastoreTypeId.ToString());
//						node.SetValue(this.columnIsRelationTableIdIndex, serverPropertyInfo.IsRelationTableId.ToString());
//						node.SetValue(this.columnIsRelationObjectIdIndex, serverPropertyInfo.IsRelationObjectId.ToString());
//						node.SetValue(this.columnIsSerializationOptimizableIndex, serverPropertyInfo.IsSerializationOptimizable.ToString());
//						node.SetValue(this.columnIsClientSeriazableIndex, serverPropertyInfo.IsClientToServerSeriazable.ToString());
//						node.SetValue(this.columnIsServerSeriazableIndex, serverPropertyInfo.IsServerToClientSeriazable.ToString());
//						node.SetValue(this.columnIsStorableIndex, serverPropertyInfo.IsStorable.ToString());
//						node.SetValue(this.columnIsEncryptedIndex, serverPropertyInfo.IsEncrypted.ToString());
//						node.SetValue(this.columnIncludeInTransactionActionLogIndex, serverPropertyInfo.IncludeInTransactionActionLog.ToString());
//						node.SetValue(this.columnDefaultValueIndex, serverPropertyInfo.DefaultValue?.ValueToString());

//					}
//				}
//			}

//			this.treeListServerObjectModelsPropertyValues.EndUnboundLoad();

//			this.treeListServerObjectModelsPropertyValues.Columns[this.columnIndexIndex].BestFit();
//			this.treeListServerObjectModelsPropertyValues.Columns[this.columnNameIndex].BestFit();
//			this.treeListServerObjectModelsPropertyValues.Columns[this.columnObjectTypeIdIndex].BestFit();
//			this.treeListServerObjectModelsPropertyValues.Columns[this.columnObjectTypeNameIndex].BestFit();
//			//this.treeListObjectPropertyValues.Columns[this.columnDatastoreTypeIdIndex].BestFit();
//			//this.treeListObjectPropertyValues.Columns[this.columnIsRelationTableIdIndex].BestFit();
//			//this.treeListObjectPropertyValues.Columns[this.columnIsRelationObjectIdIndex].BestFit();
//			//this.treeListObjectPropertyValues.Columns[this.columnIsSerializationOptimizableIndex].BestFit();
//			//this.treeListObjectPropertyValues.Columns[this.columnIsMemberOfSerializationSequenceIndex].BestFit();
//			//this.treeListObjectPropertyValues.Columns[this.columnIsStorableIndex].BestFit();
//			//this.treeListObjectPropertyValues.Columns[this.columnIsEncryptedIndex].BestFit();
//			//this.treeListObjectPropertyValues.Columns[this.columnIncludeInTransactionActionLogIndex].BestFit();
//			//this.treeListObjectPropertyValues.Columns[this.columnDefaultValueIndex].BestFit();

//			this.treeListServerObjectModelsPropertyValues.BestFitColumns();
//		}

//		protected override ResponseArgs CreateResponseArgs() => new AllServerObjectModelsResponseArgs_OLD();

//		private string CreatePropertyIndexSequenceText(ISimpleObjectModel objectModel, int[] propertyIndexes, Func<int, IPropertyModel> getPropertyModel)
//		{
//			string result = String.Empty;

//			for (int i = 0; i < propertyIndexes.Length; i++)
//			{
//				int propertyIndex = propertyIndexes[i];
//				IPropertyModel propertyModel = getPropertyModel(i);

//				result += String.Format("{0} ({1})", propertyModel.PropertyIndex, propertyModel.PropertyName);

//				if (i < propertyIndexes.Length - 1)
//					result += ", ";
//			}

//			return result;
//		}
//	}
//}
