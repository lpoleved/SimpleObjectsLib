using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simple.Objects;
using Simple.SocketEngine;
using Simple.Objects.SocketProtocol;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;

namespace Simple.Objects.ServerMonitor
{
	[SystemRequestArgs((int)SystemRequest.GetOneToManyForeignObjectCollection)]
	public partial class EditPanelGetOneToManyForeignObjectCollection : EditPanelSessionPackageRequestResponse
	{
		private int columnObjectKey;

		public EditPanelGetOneToManyForeignObjectCollection()
		{
			InitializeComponent();

			TreeListColumn column = this.treeListObjectIds.Columns.Add();

			columnObjectKey = column.AbsoluteIndex;
			column.Name = "ObjectIds";
			column.Caption = "Object Ids";
			column.Visible = true;

			this.treeListObjectIds.OptionsView.ShowIndicator = false;
			this.treeListObjectIds.OptionsView.AutoWidth = false;
		}

		protected override void OnRefreshBindingObject()
		{
			base.OnRefreshBindingObject();

			try
			{
				if (this.PackageInfoRow?.RequestOrMessagePackageInfo.PackageArgs is RelationKeyTableIdObjectIdRequestArgs requestArgs)
				{
					//int tableId = requestArgs.TableId;
					//var objectModel = Program.MonitorClient.GetServerObjectModelInfoFromCache(tableId);
					var objectModel = this.GetServerObjectModel(requestArgs.TableId);
					string graphName = this.GetRelationName(requestArgs.RealationKey);
					string objectName = this.GetObjectName(requestArgs.TableId, requestArgs.ObjectId);

					//if (objectModel != null)
					this.editorTableId.Text = String.Format("{0} ({1})", requestArgs.TableId, objectModel?.ObjectName);
					this.editorObjectId.Text = $"{requestArgs.ObjectId.ToString()} ({objectName})";
					this.editorRelationKey.Text = $"{requestArgs.RealationKey.ToString()} ({graphName})";

					//else
					//	Task.Run(async () =>
					//	{
					//		var objectModel = await Program.MonitorClient.GetServerObjectModelInfo(tableId);

					//		this.editorTableId.Text = String.Format("{0} ({1})", tableId, objectModel?.ObjectName);
					//	});
				}

				//var responseArgs = new GetObjectKeysResponseArgs();
				//responseArgs.ReadFrom(this.ResponsePackage.Reader);

				if (this.PackageInfoRow?.ResponsePackageInfo?.PackageArgs is ObjectIdsResponseArgs responeArgs)
				{
					this.treeListObjectIds.BeginUnboundLoad();
					this.treeListObjectIds.ClearNodes();

					if (responeArgs.ObjectIds != null)
						foreach (long objectId in responeArgs.ObjectIds)
							this.treeListObjectIds.AppendNode(null, null, null).SetValue(columnObjectKey, objectId);

					this.treeListObjectIds.EndUnboundLoad();
				}
			}
			catch (Exception ex)
			{
				return;
			}
		}

		protected override RequestArgs CreateRequestArgs() => new RelationKeyTableIdObjectIdRequestArgs();

		protected override ResponseArgs CreateResponseArgs() => new ObjectIdsResponseArgs();
	}
}
