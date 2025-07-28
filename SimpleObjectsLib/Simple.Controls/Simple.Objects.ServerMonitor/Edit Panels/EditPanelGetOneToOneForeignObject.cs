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
	[SystemRequestArgs((int)SystemRequest.GetOneToOneForeignObject)]
	public partial class EditPanelGetOneToOneForeignObject : EditPanelSessionPackageRequestResponse
	{
		private int columnIndexIndex, columnNameIndex, columnValueIndex;
		private int tableId;

		public EditPanelGetOneToOneForeignObject()
		{
			InitializeComponent();
		}

		protected override void OnRefreshBindingObject()
		{
			base.OnRefreshBindingObject();

			ServerObjectModelInfo? objectModel = null;

			if (this.PackageInfoRow?.RequestOrMessagePackageInfo.PackageArgs is RelationKeyTableIdObjectIdRequestArgs requestArgs) // && this.PackageInfoRow.ResponseArgs is PropertyIndexValuePairsResponseArgs responseArgs)
			{
				string objectName = this.Context?.GetObjectName(requestArgs.TableId, requestArgs.ObjectId) ?? "Unknown";
				var relationName = this.Context?.GetRelationName(requestArgs.RealationKey);

				objectModel = this.Context?.GetServerObjectModel(requestArgs.TableId);
				this.tableId = requestArgs.TableId;
				this.editorObjectKey.Text = $"{requestArgs.TableId}.{requestArgs.ObjectId} ({objectModel?.ObjectName}.{objectName})";
				this.editorRelationKey.Text = $"{requestArgs.RealationKey.ToString()} ({relationName})";
			}
			else
			{
				this.tableId = 0;
				this.editorObjectKey.Text = String.Empty;
			}

			if (this.PackageInfoRow?.ResponsePackageInfo?.PackageArgs is TableIdObjectIdResponseArgs responseArgs)
			{
				string objectName = this.Context?.GetObjectName(responseArgs.TableId, responseArgs.ObjectId) ?? "Unknown";

				objectModel = this.Context?.GetServerObjectModel(responseArgs.TableId);
				this.editorForeignObjectKey.Text = $"{responseArgs.TableId}.{responseArgs.ObjectId}";

				if (objectModel != null)
					this.editorForeignObjectKey.Text += $" ({objectModel.ObjectName}.{objectName})";
				else
					this.editorForeignObjectKey.Text += $" ({objectName})";
			}
		}

		protected override RequestArgs? CreateRequestArgs() => new RelationKeyTableIdObjectIdRequestArgs();

		protected override ResponseArgs? CreateResponseArgs() => new TableIdObjectIdResponseArgs();
	}
}
