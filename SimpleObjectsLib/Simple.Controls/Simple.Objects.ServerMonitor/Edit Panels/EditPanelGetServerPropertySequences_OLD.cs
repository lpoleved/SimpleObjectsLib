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
//using Simple.Objects;
//using Simple.Objects.Services;
//using Simple.Controls;
//using Simple.SocketEngine;
//using DevExpress.XtraGrid.Columns;
//using DevExpress.XtraEditors.Repository;
////using DevExpress.XtraTreeList.Columns;
////using DevExpress.XtraTreeList.Nodes;

//namespace Simple.Objects.ServerMonitor
//{
//	[RequestKey(SystemRequest.GetServerObjectPropertyInfo)]
//	public partial class EditPanelGetServerPropertySequences : EditPanelSessionPackageJobActionRequestResponse
//	{
//		private GridColumn columnTableId, columnObjectName, columnPropertyIndexSequence;
//		//private int columnNoIndex, columnTableIdIndex, columnObjectNameIndex, columnPropertyIndexSequence;
//		private BindingList<ServerPropertySequenceRow> dataSourceServerPropertySequences = new BindingList<ServerPropertySequenceRow>();
//		private RepositoryItemMemoEdit repositoryItempServerPopertyIndexSequence = new RepositoryItemMemoEdit();
//		private bool isFirst = true;

//		public EditPanelGetServerPropertySequences()
//		{
//			InitializeComponent();

//			this.columnTableId = new GridColumn();
//			this.columnTableId.Name = this.columnTableId.FieldName = this.columnTableId.Caption = "TableId";
//			this.columnTableId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
//			this.columnTableId.OptionsFilter.AllowFilter = false;
//			this.columnTableId.Visible = true;
//			this.gridViewServerPropertySequences.Columns.Add(this.columnTableId);

//			this.columnObjectName = new GridColumn();
//			this.columnObjectName.Name = this.columnObjectName.FieldName = "ObjectName";
//			this.columnObjectName.Caption = "Object Name";
//			this.columnObjectName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
//			this.columnObjectName.OptionsFilter.AllowFilter = false;
//			this.columnObjectName.Visible = true;
//			this.gridViewServerPropertySequences.Columns.Add(this.columnObjectName);

//			this.columnPropertyIndexSequence = new GridColumn();
//			this.columnPropertyIndexSequence.Name = this.columnPropertyIndexSequence.FieldName = "ServerPropertyIndexSequence";
//			this.columnPropertyIndexSequence.Caption = "Server Object Property Index Sequence";
//			this.columnPropertyIndexSequence.ColumnEdit = this.repositoryItempServerPopertyIndexSequence;
//			this.columnPropertyIndexSequence.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
//			//this.columnPropertyIndexSequence.OptionsFilter.AllowFilter = false;
//			this.columnPropertyIndexSequence.Visible = true;
//			this.gridViewServerPropertySequences.Columns.Add(this.columnPropertyIndexSequence);
//			this.gridControlServerPropertySequence.RepositoryItems.Add(this.repositoryItempServerPopertyIndexSequence);

//			this.gridViewServerPropertySequences.OptionsBehavior.AutoPopulateColumns = false;
//			this.gridViewServerPropertySequences.OptionsView.ShowGroupPanel = false;
//			this.gridViewServerPropertySequences.OptionsView.ColumnAutoWidth = true;
//			this.gridViewServerPropertySequences.OptionsView.RowAutoHeight = true;
//			GridHelper.SetViewOnlyMode(this.gridViewServerPropertySequences);

//			this.gridControlServerPropertySequence.DataSource = this.dataSourceServerPropertySequences;
//		}

//		protected override void OnRefreshBindingObject()
//		{
//			base.OnRefreshBindingObject();

//			GetSeriazablePropertySequencesResponseArgs responseArgs = this.PackageInfoRow.ResponseArgs as GetSeriazablePropertySequencesResponseArgs;

//			this.gridViewServerPropertySequences.BeginUpdate();
//			this.dataSourceServerPropertySequences.Clear();

//			foreach (var item in responseArgs.SeriazablePropertySequencesByTableId)
//			{
//				int tableId = item.Key;
//				ISimpleObjectModel objectModel = Program.ClientServiceMonitor.ModelDiscovery.GetObjectModel(tableId);
//				string serverPropertySequenceText = this.CreatePropertyIndexSequenceText(objectModel, item.Value);

//				ServerPropertySequenceRow row = new ServerPropertySequenceRow(tableId, objectModel.ObjectTypeCaption, serverPropertySequenceText);
//				//int rowIndex = this.dataSourceServerPropertgySequences.Count;
//				this.dataSourceServerPropertySequences.Add(row);
//			}

//			this.columnTableId.BestFit();
//			this.columnObjectName.BestFit();
//			//this.columnPropertyIndexSequence.BestFit();

//			this.gridViewServerPropertySequences.EndUpdate();
//			//this.gridViewServerPropertgySequences.BestFitColumns();
//			//this.gridViewServerPropertgySequences.RefreshEditor(updateEditorValue: false);

//			//this.columnTableId.BestFit();
//			//this.columnObjectName.BestFit();
//			//this.gridControlServerPropertySequence.Refresh();

//			if (this.isFirst)
//			{
//				this.isFirst = false;
//				//				this.OnRefreshBindingObject();
//				//this.gridControlServerPropertySequence.Refresh();
//			}

//			this.labelControlServerPropertySequencesCount.Text = responseArgs.SeriazablePropertySequencesByTableId.Count.ToString();
//		}

//		protected override ResponseArgs CreateResponseArgs(PackageInfo responsePackage)
//		{
//			return new GetSeriazablePropertySequencesResponseArgs(responsePackage.Reader);
//		}

//		private string CreatePropertyIndexSequenceText(ISimpleObjectModel objectModel, IPropertySequence propertySequence)
//		{
//			string result = String.Empty;

//			for (int i = 0; i < propertySequence.Length; i++)
//			{
//				IPropertyModel propertyModel = propertySequence.PropertyModels[i];

//				result += String.Format("{0} ({1})", propertyModel.Index, propertyModel.Name);

//				if (i < propertySequence.Length - 1)
//					result += ", ";
//			}

//			return result;
//		}
//	}

//	class ServerPropertySequenceRow
//	{
//		public ServerPropertySequenceRow(int tableId, string objectName, string serverPropertyIndexSequence)
//		{
//			this.TableId = tableId;
//			this.ObjectName = objectName;
//			this.ServerPropertyIndexSequence = serverPropertyIndexSequence;
//		}

//		public int TableId { get; private set; }
//		public string ObjectName { get; private set; }
//		public string ServerPropertyIndexSequence { get; private set; }
//	}
//}
