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

namespace Simple.Objects.ServerMonitor
{
	[SystemRequestArgs((int)SystemRequest.GetObjectPropertyValues)]
	public partial class EditPanelGetObjectPropertyValues : EditPanelSessionPackageRequestResponse
	{
		private int columnIndexIndex, columnNameIndex, columnValueIndex;
		private int tableId;

		public EditPanelGetObjectPropertyValues()
		{
			InitializeComponent();

			TreeListColumn column = this.treeListObjectPropertyValues.Columns.Add();

			this.columnIndexIndex = column.AbsoluteIndex;
			column.Name = column.Caption = "Index";
			column.Visible = true;

			column = this.treeListObjectPropertyValues.Columns.Add();
			this.columnNameIndex = column.AbsoluteIndex;
			column.Name = column.Caption = "Name";
			column.Visible = true;

			column = this.treeListObjectPropertyValues.Columns.Add();
			this.columnValueIndex = column.AbsoluteIndex;
			column.Name = column.Caption = "Value";
			column.Visible = true;

			this.treeListObjectPropertyValues.OptionsView.ShowIndicator = false;
			//this.treeListObjectPropertyValues.OptionsView.AutoWidth = false;
		}

		protected override void OnRefreshBindingObject()
		{
			base.OnRefreshBindingObject();

			ServerObjectModelInfo? objectModel = null;

			if (this.PackageInfoRow?.RequestOrMessagePackageInfo.PackageArgs is ObjectIdTableIdRequestArgs requestArgs) // && this.PackageInfoRow.ResponseArgs is PropertyIndexValuePairsResponseArgs responseArgs)
			{
				string objectName = this.GetObjectName(requestArgs.TableId, requestArgs.ObjectId);

				objectModel = this.GetServerObjectModel(requestArgs.TableId);
				this.tableId = requestArgs.TableId;
				this.editorObjectKey.Text = $"{requestArgs.TableId}.{requestArgs.ObjectId} ({objectModel?.ObjectName}.{objectName})";

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
			else
			{
				this.tableId = 0;
				this.editorObjectKey.Text = String.Empty;
			}

			//this.treeListObjectPropertyValues.BeginUnboundLoad();
			//this.treeListObjectPropertyValues.ClearNodes();

			if (this.PackageInfoRow?.ResponsePackageInfo?.PackageArgs is ObjectPropertyValuesResponseArgs responseArgs)
			{
				this.editorTableId.Text = responseArgs.TableId.ToString() + " (" + objectModel?.ObjectName + ")";
				this.treeListObjectPropertyValues.BeginUnboundLoad();
				this.treeListObjectPropertyValues.ClearNodes();

				if (responseArgs.PropertyIndexValues != null && objectModel != null)
				{
					foreach (var item in responseArgs.PropertyIndexValues!)
					{
						string propertyName = objectModel[item.PropertyIndex].PropertyName;
						string propertyValue = item.PropertyValue?.ToString() ?? "null";
						TreeListNode node = this.treeListObjectPropertyValues.AppendNode(null, null, null);

						node.SetValue(columnIndexIndex, item.PropertyIndex);
						node.SetValue(columnNameIndex, propertyName);
						node.SetValue(columnValueIndex, propertyValue);
					}
				}

				this.treeListObjectPropertyValues.Columns[this.columnIndexIndex].BestFit();
				this.treeListObjectPropertyValues.Columns[this.columnNameIndex].BestFit();
				//this.treeListObjectPropertyValues.Columns[this.columnValueIndex].BestFit();
				//this.treeListObjectPropertyValues.BestFitColumns();
				this.treeListObjectPropertyValues.EndUnboundLoad();
			}

		}


		protected override RequestArgs? CreateRequestArgs() => new ObjectIdTableIdRequestArgs();

		protected override ResponseArgs? CreateResponseArgs() => new ObjectPropertyValuesResponseArgs();
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
}
