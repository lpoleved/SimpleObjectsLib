using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simple.Modeling;
using Simple.Serialization;
using Simple.Objects;
using Simple.Objects.SocketProtocol;
using Simple.SocketEngine;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraSpreadsheet.Utils.Trees;
using DevExpress.XtraGrid.Columns;
using System.Windows.Controls;
using DevExpress.XtraEditors.Repository;
using Simple.Controls;
using DevExpress.Mvvm.POCO;

namespace Simple.Objects.ServerMonitor
{
	[SystemRequestArgs((int)SystemRequest.GetGraphElementsWithObjects)]
	public partial class EditPanelGetGraphElementsWithObjects : EditPanelSessionPackageRequestResponse
	{
		private int columnIndexIndex, columnNameIndex, columnObjectTypeIdIndex, columnObjectTypeNameIndex;
		private int columnDatastoreTypeIdIndex, columnIsRelationTableIdIndex, columnIsRelationObjectIdIndex;
		private int columnIsSerializationOptimizableIndex, columnIsMemberOfSerializationSequenceIndex;
		private int columnIsStorableIndex, columnIsEncryptedIndex, columnIncludeInTransactionActionLogIndex, columnDefaultValueIndex;
		private GridColumn columnNo, columnGraphElementId, columnGraphKey, columnParentId, columnObjectTableId, columnObjectId, columnHasChildren, columnSimpleObjectPropertyIndexValues;
		//private int graphKey;
		//private long parentGraphElementId;
		private BindingList<GraphElementObjectPropertyInfoRow> dataSourceGraphElementsWithObjects = new BindingList<GraphElementObjectPropertyInfoRow>();
		private RepositoryItemMemoEdit repositoryItemSimpleObjectPropertyIndexValues = new RepositoryItemMemoEdit();
		private string tabNewObjectModelsName, tabGraphElementsWithObjectsName;


		public EditPanelGetGraphElementsWithObjects()
		{
			InitializeComponent();

			this.tabNewObjectModelsName = this.tabPageResponseNewServerObjectPropertyModels.Text;
			this.tabGraphElementsWithObjectsName = this.tabPageResponseGraphElementsWithObjects.Text;

			//
			// New Server Object Models
			//
			TreeListColumn column = this.treeListNewObjectModels.Columns.Add();

			// int PropertyIndex
			this.columnIndexIndex = column.AbsoluteIndex;
			column.Name = "Index";
			column.Caption = "TableId/PropertyIndex";
			column.Visible = true;

			// string PropertyName
			column = this.treeListNewObjectModels.Columns.Add();
			this.columnNameIndex = column.AbsoluteIndex;
			column.Name = column.Caption = "Name";
			column.Visible = true;

			column = this.treeListNewObjectModels.Columns.Add();
			this.columnObjectTypeNameIndex = column.AbsoluteIndex;
			column.Name = column.Caption = "Type";
			column.Visible = true;

			// int PropertyTypeId
			column = this.treeListNewObjectModels.Columns.Add();
			this.columnObjectTypeIdIndex = column.AbsoluteIndex;
			column.Name = column.Caption = "TypeId";
			column.Visible = true;

			// int DatastoreTypeId
			column = this.treeListNewObjectModels.Columns.Add();
			this.columnDatastoreTypeIdIndex = column.AbsoluteIndex;
			column.Name = column.Caption = "Datastore TypeId";
			column.Visible = true;

			// bool IsRelationTableId 
			column = this.treeListNewObjectModels.Columns.Add();
			this.columnIsRelationTableIdIndex = column.AbsoluteIndex;
			column.Name = column.Caption = "IsRelationTableId";
			column.Visible = true;

			// bool IsRelationObjectId
			column = this.treeListNewObjectModels.Columns.Add();
			this.columnIsRelationObjectIdIndex = column.AbsoluteIndex;
			column.Name = column.Caption = "IsRelationObjectId";
			column.Visible = true;

			//bool IsSerializationOptimizable
			column = this.treeListNewObjectModels.Columns.Add();
			this.columnIsSerializationOptimizableIndex = column.AbsoluteIndex;
			column.Name = "IsSerializationOptimizable";
			column.Caption = "IsSerializationOptimizable";
			column.Visible = true;

			// bool IsMemberOfSerializationSequence
			column = this.treeListNewObjectModels.Columns.Add();
			this.columnIsMemberOfSerializationSequenceIndex = column.AbsoluteIndex;
			column.Name = "IsMemberOfSerializationSequence";
			column.Caption = "IsMemberOfSerializationSequence";
			column.Visible = true;

			// bool IsStorable 
			column = this.treeListNewObjectModels.Columns.Add();
			this.columnIsStorableIndex = column.AbsoluteIndex;
			column.Name = column.Caption = "IsStorable";
			column.Visible = true;

			// bool IsEncrypted
			column = this.treeListNewObjectModels.Columns.Add();
			this.columnIsEncryptedIndex = column.AbsoluteIndex;
			column.Name = column.Caption = "IsEncrypted";
			column.Visible = true;

			// bool IncludeInTransactionActionLog
			column = this.treeListNewObjectModels.Columns.Add();
			this.columnIncludeInTransactionActionLogIndex = column.AbsoluteIndex;
			column.Name = column.Caption = "IncludeInTransactionActionLog";
			column.Visible = true;

			// object? DefaultValue
			column = this.treeListNewObjectModels.Columns.Add();
			this.columnDefaultValueIndex = column.AbsoluteIndex;
			column.Name = column.Caption = "DefaultValue";
			column.Visible = true;

			this.treeListNewObjectModels.OptionsView.ShowIndicator = false;
			this.treeListNewObjectModels.OptionsView.AutoWidth = false;

			//
			// GraphElements with Objects 
			//
			this.gridViewGraphElementsWithObjects.Columns.Add(this.CreateGridColumn("No"));
			this.gridViewGraphElementsWithObjects.Columns.Add(this.CreateGridColumn("GraphElementId", "GraphElement Id"));
			this.gridViewGraphElementsWithObjects.Columns.Add(this.CreateGridColumn("ObjectTableId"));
			this.gridViewGraphElementsWithObjects.Columns.Add(this.CreateGridColumn("ObjectId"));
			this.gridViewGraphElementsWithObjects.Columns.Add(this.CreateGridColumn("HasChildren"));
			this.gridViewGraphElementsWithObjects.Columns.Add(this.columnSimpleObjectPropertyIndexValues = this.CreateGridColumn("SimpleObjectPropertyIndexValues", "SimpleObject Property Index Values"));
			this.columnSimpleObjectPropertyIndexValues.ColumnEdit = this.repositoryItemSimpleObjectPropertyIndexValues;
			this.gridControlGraphElementsWithObjects.RepositoryItems.Add(this.repositoryItemSimpleObjectPropertyIndexValues);

			this.gridViewGraphElementsWithObjects.OptionsBehavior.AutoPopulateColumns = false;
			this.gridViewGraphElementsWithObjects.OptionsView.ShowGroupPanel = false;
			this.gridViewGraphElementsWithObjects.OptionsView.ColumnAutoWidth = true;
			this.gridViewGraphElementsWithObjects.OptionsView.RowAutoHeight = true;
			GridHelper.SetViewOnlyMode(this.gridViewGraphElementsWithObjects);

			this.gridControlGraphElementsWithObjects.DataSource = this.dataSourceGraphElementsWithObjects;
		}


		protected override void OnRefreshBindingObject()
		{
			base.OnRefreshBindingObject();

			if (this.PackageInfoRow?.RequestOrMessagePackageInfo.PackageArgs is ParentGraphElementIdGraphKeyRequestArgs requestArgs) // && this.PackageInfoRow.ResponseArgs is PropertyIndexValuePairsResponseArgs responseArgs)
			{
				string graphName = this.GetGraphName(requestArgs.GraphKey);
				string parentGrapElementName = this.GetObjectName(GraphElementModel.TableId, requestArgs.ParentGraphElementId);

				this.editorGraphKey.Text = $"{requestArgs.GraphKey} ({graphName})"; ;
				this.editorParentGraphElementId.Text = requestArgs.ParentGraphElementId.ToString();

				if (requestArgs.ParentGraphElementId != 0)
					this.editorParentGraphElementId.Text += " (" + parentGrapElementName + ")";

				//ServerObjectModelInfo? serverObjectModelInfo = Program.MonitorClient.GetServerObjectModelInfo(requestArgs.TableId).Result;



				//ServerObjectModelInfo? serverObjectModelInfo = Program.MonitorClient.GetServerObjectModelInfoFromCache(requestArgs.TableId);

				//if (serverObjectModelInfo != null)
				//{
				//	this.PackageInfoRow.ResponseArgs ??= new PropertyIndexValuePairsResponseArgs(serverObjectModelInfo).CreateFromReader(this.PackageInfoRow?.ResponseArgsReader!);
				//	this.RefreshResponse(this.PackageInfoRow.ResponseArgs as PropertyIndexValuePairsResponseArgs);
				//}
				//else
				//{
				//	Task.Run(async () =>
				//	{
				//		ServerObjectModelInfo? serverObjectModelInfo = await Program.MonitorClient.GetServerObjectModelInfo(this.tableId);

				//		if (serverObjectModelInfo != null)
				//		{
				//			this.PackageInfoRow.ResponseArgs ??= new PropertyIndexValuePairsResponseArgs(serverObjectModelInfo).CreateFromReader(this.PackageInfoRow?.ResponseArgsReader!);
				//			this.RefreshResponse(this.PackageInfoRow.ResponseArgs as PropertyIndexValuePairsResponseArgs);
				//		}
				//		else
				//			this.treeListObjectPropertyValues.ClearNodes();

				//	});
				//}
			}

			if (this.PackageInfoRow?.ResponsePackageInfo?.PackageArgs is GraphElementsWithObjectsResponseArgs responseArgs)
			{
				this.tabPageResponseNewServerObjectPropertyModels.Text = $"{this.tabNewObjectModelsName} ({responseArgs.NewServerObjectModels?.Count().ToString() ?? "0"})";
				this.tabPageResponseGraphElementsWithObjects.Text = this.tabGraphElementsWithObjectsName + " (" + responseArgs?.GraphElementWithObjects!.Count() + ")";

				// New Object Models
				this.treeListNewObjectModels.BeginUnboundLoad();
				this.treeListNewObjectModels.ClearNodes();

				if (responseArgs?.NewServerObjectModels != null)
				{
					foreach (ServerObjectModelInfo serverObjectModel in responseArgs.NewServerObjectModels)
					{
						TreeListNode root = this.treeListNewObjectModels.AppendNode(nodeData: null, parentNode: null, tag: null);

						root.SetValue(this.columnIndexIndex, serverObjectModel.TableId);
						root.SetValue(this.columnNameIndex, serverObjectModel.ObjectName); // $"{serverObjectModel.ObjectName}, TableName={serverObjectModel.TableName}");

						for (int i = 0; i < serverObjectModel.ServerPropertyInfos.Count(); i++)
						{
							ServerPropertyInfo serverPropertyInfo = serverObjectModel.ServerPropertyInfos.ElementAt(i);
							Type propertyObjectType = PropertyTypes.GetPropertyType(serverPropertyInfo.PropertyTypeId);
							TreeListNode node = this.treeListNewObjectModels.AppendNode(nodeData: null, parentNode: root, tag: null);

							node.SetValue(this.columnIndexIndex, serverPropertyInfo.PropertyIndex);
							node.SetValue(this.columnNameIndex, serverPropertyInfo.PropertyName);
							node.SetValue(this.columnObjectTypeNameIndex, (propertyObjectType != null) ? ReflectionHelper.GetTypeName(propertyObjectType) : String.Empty);
							node.SetValue(this.columnObjectTypeIdIndex, serverPropertyInfo.PropertyTypeId);
							node.SetValue(this.columnDatastoreTypeIdIndex, serverPropertyInfo.DatastoreTypeId.ToString());
							node.SetValue(this.columnIsRelationTableIdIndex, serverPropertyInfo.IsRelationTableId.ToString());
							node.SetValue(this.columnIsRelationObjectIdIndex, serverPropertyInfo.IsRelationObjectId.ToString());
							node.SetValue(this.columnIsSerializationOptimizableIndex, serverPropertyInfo.IsSerializationOptimizable.ToString());
							node.SetValue(this.columnIsMemberOfSerializationSequenceIndex, serverPropertyInfo.IsClientSeriazable.ToString());
							node.SetValue(this.columnIsStorableIndex, serverPropertyInfo.IsStorable.ToString());
							node.SetValue(this.columnIsEncryptedIndex, serverPropertyInfo.IsEncrypted.ToString());
							node.SetValue(this.columnIncludeInTransactionActionLogIndex, serverPropertyInfo.IncludeInTransactionActionLog.ToString());
							node.SetValue(this.columnDefaultValueIndex, serverPropertyInfo.DefaultValue?.ValueToString());

						}
					}
				}

				this.treeListNewObjectModels.EndUnboundLoad();

				// GraphElements with Objects
				this.gridViewGraphElementsWithObjects.BeginUpdate();
				this.dataSourceGraphElementsWithObjects.Clear();

				for (int i = 0; i < responseArgs?.GraphElementWithObjects!.Length; i++)
					this.AppendGraphElementObjectPropertyInfoRow(i, ref responseArgs.GraphElementWithObjects![i]);

				for (int i = 0; i < 5; i++)
					this.gridViewGraphElementsWithObjects.Columns[i].BestFit();

				this.gridViewGraphElementsWithObjects.EndUpdate();
			}
			else
			{
				this.treeListNewObjectModels.ClearNodes();
				this.dataSourceGraphElementsWithObjects.Clear();
				this.tabPageResponseNewServerObjectPropertyModels.Text = this.tabNewObjectModelsName;
				this.tabPageResponseGraphElementsWithObjects.Text = this.tabGraphElementsWithObjectsName;
			}
		}

		private void AppendGraphElementObjectPropertyInfoRow(int rowIndex, ref GraphElementObjectPair graphElementWithObject)
		{
			ServerObjectModelInfo? objectModel = this.GetServerObjectModel(graphElementWithObject.SimpleObjectTableId);
			string simpleObjectPropertyIndexValuesText = this.CreatePropertyIndexValuesString(objectModel!, graphElementWithObject.SimpleObjectPropertyIndexValues);
			GraphElementObjectPropertyInfoRow row = new GraphElementObjectPropertyInfoRow(rowIndex, graphElementWithObject.GraphElementId,
																						  graphElementWithObject.SimpleObjectTableId, graphElementWithObject.SimpleObjectId,
																						  graphElementWithObject.HasChildren, simpleObjectPropertyIndexValuesText);

			this.dataSourceGraphElementsWithObjects.Add(row);
			//this.dataSourceTransactionRequestDetails.RefreshRow(rowIndex);
		}


		private void RefreshResponse(GraphElementsWithObjectsResponseArgs responseArgs)
		{
			//var objectModel = this.Context?.AppClient.GetServerObjectModel(responseArgs.TableId);

			//this.editorObjectId.Text += "  (" + objectModel?.ObjectName + ")";
			//this.editorTableId.Text = responseArgs.TableId.ToString();
			//this.treeListObjectPropertyValues.BeginUnboundLoad();
			//this.treeListObjectPropertyValues.ClearNodes();

			//if (responseArgs.PropertyIndexValues != null && objectModel != null)
			//{
			//	foreach (var item in responseArgs.PropertyIndexValues!)
			//	{
			//		string propertyName = objectModel[item.PropertyIndex].PropertyName;
			//		string propertyValue = item.PropertyValue?.ToString() ?? "null";

			//		TreeListNode node = this.treeListObjectPropertyValues.AppendNode(null, null, null);

			//		node.SetValue(columnIndexIndex, item.PropertyIndex);
			//		node.SetValue(columnNameIndex, propertyName);
			//		node.SetValue(columnValueIndex, propertyValue);
			//	}
			//}

			//this.treeListObjectPropertyValues.Columns[this.columnIndexIndex].BestFit();
			//this.treeListObjectPropertyValues.Columns[this.columnNameIndex].BestFit();
			////this.treeListObjectPropertyValues.Columns[this.columnValueIndex].BestFit();
			////this.treeListObjectPropertyValues.BestFitColumns();

			//this.treeListObjectPropertyValues.EndUnboundLoad();
		}

		protected override RequestArgs? CreateRequestArgs() => new ParentGraphElementIdGraphKeyRequestArgs();

		protected override ResponseArgs? CreateResponseArgs() => new GraphElementsWithObjectsResponseArgs();
		//{
		//	int tableId = -1;
		//	ServerObjectModelInfo? serverObjectModelInfo = null;

		//	// TODO: Check if this needed?????
		//	// Why not simple return => new PropertyValuesResponseArgs();
		//	//
		//	try
		//	{
		//		if (this.PackageInfoRow?.RequestOrMessagePackageInfo.PackageArgs is ObjectIdTableIdRequestArgs requesrArgs)
		//			tableId = requesrArgs.TableId;

		//		serverObjectModelInfo = this.GetServerObjectModel(tableId);
		//	}
		//	catch (Exception ex)
		//	{
		//		return default;
		//	}

		//	return (serverObjectModelInfo != null) ? new ObjectPropertyValuesResponseArgs() : default;
		//}

		//protected virtual GetObjectPropertyValuesResponseArgs CreateGetObjectPropertyValuesResponseArgs(int tableId)
		//{
		//	return new GetObjectPropertyValuesResponseArgs(tableId, skipReadingKey: true, serializationModel: SerializationModel.SequenceValuesOnly, defaultOptimization: false);
		//}
	}

	class GraphElementObjectPropertyInfoRow
	{
		public GraphElementObjectPropertyInfoRow(int rowIndex, long graphElementId, int simpleObjectTableId, long simpleObjectId, bool hasChildren, string objectPropertyIndexValuesText)
		{
			this.No = rowIndex;
			this.GraphElementId = graphElementId;
			this.ObjectTableId = simpleObjectTableId;
			this.ObjectId = simpleObjectId;
			this.HasChildren = (hasChildren) ? "Yes" : "No";
			this.SimpleObjectPropertyIndexValues = objectPropertyIndexValuesText;
		}

		public int No { get; private set; }
		public long GraphElementId { get; private set; }
		public int ObjectTableId { get; private set; }
		public long ObjectId { get; private set; }
		public string HasChildren { get; private set; }
		public string SimpleObjectPropertyIndexValues { get; private set; }
	}
}
