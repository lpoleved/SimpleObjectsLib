using System.Windows.Forms;
using System.Drawing;

namespace Simple.Objects.ServerMonitor
{
	partial class EditPanelTransactionLog
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			editorTransactionId = new DevExpress.XtraEditors.TextEdit();
			labelControlTransactionId = new DevExpress.XtraEditors.LabelControl();
			editorUser = new DevExpress.XtraEditors.TextEdit();
			labelControlAdmin = new DevExpress.XtraEditors.LabelControl();
			editorClientId = new DevExpress.XtraEditors.TextEdit();
			labelControlClientId = new DevExpress.XtraEditors.LabelControl();
			editorStatus = new DevExpress.XtraEditors.TextEdit();
			labelControlStatus = new DevExpress.XtraEditors.LabelControl();
			editorCreationTime = new DevExpress.XtraEditors.TextEdit();
			labelControlCreationTime = new DevExpress.XtraEditors.LabelControl();
			labelControlTransactionRequestCount = new DevExpress.XtraEditors.LabelControl();
			gridControlRollbackActions = new DevExpress.XtraGrid.GridControl();
			gridViewRollbackActions = new DevExpress.XtraGrid.Views.Grid.GridView();
			tabControlTransactionRequest = new DevExpress.XtraTab.XtraTabControl();
			tabPageRollbackActionBinaries = new DevExpress.XtraTab.XtraTabPage();
			labelControlRollbackActionDataNoOfBytes = new DevExpress.XtraEditors.LabelControl();
			labelControlRollbackActionDataLength = new DevExpress.XtraEditors.LabelControl();
			editorTransactionActionData = new DevExpress.XtraEditors.MemoEdit();
			tabPageTransactionRequests = new DevExpress.XtraTab.XtraTabPage();
			labelControlNoOfTransactionActions = new DevExpress.XtraEditors.LabelControl();
			tabPageDatastoreActions = new DevExpress.XtraTab.XtraTabPage();
			gridControlDatastoreActions = new DevExpress.XtraGrid.GridControl();
			gridViewDatastoreActions = new DevExpress.XtraGrid.Views.Grid.GridView();
			labelControlNoOfDatastoreActions = new DevExpress.XtraEditors.LabelControl();
			labelControlDatastoreActionCount = new DevExpress.XtraEditors.LabelControl();
			tabPageRollbackActions = new DevExpress.XtraTab.XtraTabPage();
			labelControlNoOfRollbackActions = new DevExpress.XtraEditors.LabelControl();
			labelControlRollbackActions = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)tabControl).BeginInit();
			tabControl.SuspendLayout();
			tabPageObjectName.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorTransactionId.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorUser.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorClientId.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorStatus.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorCreationTime.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridControlRollbackActions).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridViewRollbackActions).BeginInit();
			((System.ComponentModel.ISupportInitialize)tabControlTransactionRequest).BeginInit();
			tabControlTransactionRequest.SuspendLayout();
			tabPageRollbackActionBinaries.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)editorTransactionActionData.Properties).BeginInit();
			tabPageTransactionRequests.SuspendLayout();
			tabPageDatastoreActions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)gridControlDatastoreActions).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridViewDatastoreActions).BeginInit();
			tabPageRollbackActions.SuspendLayout();
			SuspendLayout();
			// 
			// tabPageObjectName
			// 
			tabPageObjectName.Controls.Add(tabControlTransactionRequest);
			tabPageObjectName.Controls.Add(editorCreationTime);
			tabPageObjectName.Controls.Add(labelControlCreationTime);
			tabPageObjectName.Controls.Add(editorStatus);
			tabPageObjectName.Controls.Add(labelControlStatus);
			tabPageObjectName.Controls.Add(editorClientId);
			tabPageObjectName.Controls.Add(labelControlClientId);
			tabPageObjectName.Controls.Add(editorUser);
			tabPageObjectName.Controls.Add(labelControlAdmin);
			tabPageObjectName.Controls.Add(editorTransactionId);
			tabPageObjectName.Controls.Add(labelControlTransactionId);
			tabPageObjectName.Text = "Transaction";
			// 
			// editorTransactionId
			// 
			editorTransactionId.ImeMode = ImeMode.KatakanaHalf;
			editorTransactionId.Location = new Point(95, 25);
			editorTransactionId.Name = "editorTransactionId";
			editorTransactionId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorTransactionId.Properties.Appearance.Options.UseFont = true;
			editorTransactionId.Properties.ReadOnly = true;
			editorTransactionId.Size = new Size(121, 20);
			editorTransactionId.TabIndex = 32;
			// 
			// labelControlTransactionId
			// 
			labelControlTransactionId.Location = new Point(18, 28);
			labelControlTransactionId.Name = "labelControlTransactionId";
			labelControlTransactionId.Size = new Size(70, 13);
			labelControlTransactionId.TabIndex = 31;
			labelControlTransactionId.Text = "TransactionId:";
			// 
			// editorUser
			// 
			editorUser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorUser.ImeMode = ImeMode.KatakanaHalf;
			editorUser.Location = new Point(95, 77);
			editorUser.Name = "editorUser";
			editorUser.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorUser.Properties.Appearance.Options.UseFont = true;
			editorUser.Properties.ReadOnly = true;
			editorUser.Size = new Size(461, 20);
			editorUser.TabIndex = 34;
			// 
			// labelControlAdmin
			// 
			labelControlAdmin.Location = new Point(18, 80);
			labelControlAdmin.Name = "labelControlAdmin";
			labelControlAdmin.Size = new Size(26, 13);
			labelControlAdmin.TabIndex = 33;
			labelControlAdmin.Text = "User:";
			// 
			// editorClientId
			// 
			editorClientId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorClientId.ImeMode = ImeMode.KatakanaHalf;
			editorClientId.Location = new Point(305, 25);
			editorClientId.Name = "editorClientId";
			editorClientId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorClientId.Properties.Appearance.Options.UseFont = true;
			editorClientId.Properties.ReadOnly = true;
			editorClientId.Size = new Size(251, 20);
			editorClientId.TabIndex = 36;
			// 
			// labelControlClientId
			// 
			labelControlClientId.Location = new Point(229, 28);
			labelControlClientId.Name = "labelControlClientId";
			labelControlClientId.Size = new Size(41, 13);
			labelControlClientId.TabIndex = 35;
			labelControlClientId.Text = "ClientId:";
			// 
			// editorStatus
			// 
			editorStatus.ImeMode = ImeMode.KatakanaHalf;
			editorStatus.Location = new Point(95, 51);
			editorStatus.Name = "editorStatus";
			editorStatus.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorStatus.Properties.Appearance.Options.UseFont = true;
			editorStatus.Properties.ReadOnly = true;
			editorStatus.Size = new Size(121, 20);
			editorStatus.TabIndex = 38;
			// 
			// labelControlStatus
			// 
			labelControlStatus.Location = new Point(18, 54);
			labelControlStatus.Name = "labelControlStatus";
			labelControlStatus.Size = new Size(35, 13);
			labelControlStatus.TabIndex = 37;
			labelControlStatus.Text = "Status:";
			// 
			// editorCreationTime
			// 
			editorCreationTime.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorCreationTime.ImeMode = ImeMode.KatakanaHalf;
			editorCreationTime.Location = new Point(305, 51);
			editorCreationTime.Name = "editorCreationTime";
			editorCreationTime.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorCreationTime.Properties.Appearance.Options.UseFont = true;
			editorCreationTime.Properties.ReadOnly = true;
			editorCreationTime.Size = new Size(251, 20);
			editorCreationTime.TabIndex = 40;
			// 
			// labelControlCreationTime
			// 
			labelControlCreationTime.Location = new Point(229, 54);
			labelControlCreationTime.Name = "labelControlCreationTime";
			labelControlCreationTime.Size = new Size(70, 13);
			labelControlCreationTime.TabIndex = 39;
			labelControlCreationTime.Text = "Creation Time:";
			// 
			// labelControlTransactionRequestCount
			// 
			labelControlTransactionRequestCount.Appearance.Options.UseTextOptions = true;
			labelControlTransactionRequestCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelControlTransactionRequestCount.Location = new Point(16, 15);
			labelControlTransactionRequestCount.Name = "labelControlTransactionRequestCount";
			labelControlTransactionRequestCount.Size = new Size(135, 13);
			labelControlTransactionRequestCount.TabIndex = 42;
			labelControlTransactionRequestCount.Text = "Transaction Request Count:";
			// 
			// gridControlRollbackActions
			// 
			gridControlRollbackActions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			gridControlRollbackActions.Location = new Point(3, 34);
			gridControlRollbackActions.MainView = gridViewRollbackActions;
			gridControlRollbackActions.Name = "gridControlRollbackActions";
			gridControlRollbackActions.Size = new Size(562, 419);
			gridControlRollbackActions.TabIndex = 47;
			gridControlRollbackActions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewRollbackActions });
			// 
			// gridViewRollbackActions
			// 
			gridViewRollbackActions.GridControl = gridControlRollbackActions;
			gridViewRollbackActions.Name = "gridViewRollbackActions";
			// 
			// tabControlTransactionRequest
			// 
			tabControlTransactionRequest.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			tabControlTransactionRequest.Location = new Point(3, 118);
			tabControlTransactionRequest.Name = "tabControlTransactionRequest";
			tabControlTransactionRequest.SelectedTabPage = tabPageRollbackActionBinaries;
			tabControlTransactionRequest.Size = new Size(570, 481);
			tabControlTransactionRequest.TabIndex = 43;
			tabControlTransactionRequest.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { tabPageTransactionRequests, tabPageDatastoreActions, tabPageRollbackActions, tabPageRollbackActionBinaries });
			// 
			// tabPageRollbackActionBinaries
			// 
			tabPageRollbackActionBinaries.Controls.Add(labelControlRollbackActionDataNoOfBytes);
			tabPageRollbackActionBinaries.Controls.Add(labelControlRollbackActionDataLength);
			tabPageRollbackActionBinaries.Controls.Add(editorTransactionActionData);
			tabPageRollbackActionBinaries.Name = "tabPageRollbackActionBinaries";
			tabPageRollbackActionBinaries.Size = new Size(568, 456);
			tabPageRollbackActionBinaries.Text = "Rollback Action Binaries";
			// 
			// labelControlRollbackActionDataNoOfBytes
			// 
			labelControlRollbackActionDataNoOfBytes.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			labelControlRollbackActionDataNoOfBytes.Appearance.Options.UseFont = true;
			labelControlRollbackActionDataNoOfBytes.Appearance.Options.UseTextOptions = true;
			labelControlRollbackActionDataNoOfBytes.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelControlRollbackActionDataNoOfBytes.Location = new Point(148, 15);
			labelControlRollbackActionDataNoOfBytes.Name = "labelControlRollbackActionDataNoOfBytes";
			labelControlRollbackActionDataNoOfBytes.Size = new Size(76, 13);
			labelControlRollbackActionDataNoOfBytes.TabIndex = 46;
			labelControlRollbackActionDataNoOfBytes.Text = "(No. of Bytes)";
			// 
			// labelControlRollbackActionDataLength
			// 
			labelControlRollbackActionDataLength.Appearance.Options.UseTextOptions = true;
			labelControlRollbackActionDataLength.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelControlRollbackActionDataLength.Location = new Point(5, 15);
			labelControlRollbackActionDataLength.Name = "labelControlRollbackActionDataLength";
			labelControlRollbackActionDataLength.Size = new Size(138, 13);
			labelControlRollbackActionDataLength.TabIndex = 44;
			labelControlRollbackActionDataLength.Text = "Rollback Action Data Length:";
			// 
			// editorTransactionActionData
			// 
			editorTransactionActionData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			errorProvider.SetIconAlignment(editorTransactionActionData, ErrorIconAlignment.MiddleRight);
			editorTransactionActionData.Location = new Point(3, 34);
			editorTransactionActionData.Name = "editorTransactionActionData";
			editorTransactionActionData.Properties.MaxLength = 2000;
			editorTransactionActionData.Properties.ReadOnly = true;
			editorTransactionActionData.Size = new Size(562, 419);
			editorTransactionActionData.TabIndex = 31;
			// 
			// tabPageTransactionRequests
			// 
			tabPageTransactionRequests.Controls.Add(labelControlNoOfTransactionActions);
			tabPageTransactionRequests.Controls.Add(labelControlTransactionRequestCount);
			tabPageTransactionRequests.Name = "tabPageTransactionRequests";
			tabPageTransactionRequests.Size = new Size(568, 456);
			tabPageTransactionRequests.Text = "Transaction Requests";
			// 
			// labelControlNoOfTransactionActions
			// 
			labelControlNoOfTransactionActions.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			labelControlNoOfTransactionActions.Appearance.Options.UseFont = true;
			labelControlNoOfTransactionActions.Appearance.Options.UseTextOptions = true;
			labelControlNoOfTransactionActions.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelControlNoOfTransactionActions.Location = new Point(155, 15);
			labelControlNoOfTransactionActions.Name = "labelControlNoOfTransactionActions";
			labelControlNoOfTransactionActions.Size = new Size(89, 13);
			labelControlNoOfTransactionActions.TabIndex = 43;
			labelControlNoOfTransactionActions.Text = "(No. of Actions):";
			// 
			// tabPageDatastoreActions
			// 
			tabPageDatastoreActions.Controls.Add(gridControlDatastoreActions);
			tabPageDatastoreActions.Controls.Add(labelControlNoOfDatastoreActions);
			tabPageDatastoreActions.Controls.Add(labelControlDatastoreActionCount);
			tabPageDatastoreActions.Name = "tabPageDatastoreActions";
			tabPageDatastoreActions.Size = new Size(568, 456);
			tabPageDatastoreActions.Text = "Datastore Actions";
			// 
			// gridControlDatastoreActions
			// 
			gridControlDatastoreActions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			gridControlDatastoreActions.Location = new Point(3, 34);
			gridControlDatastoreActions.MainView = gridViewDatastoreActions;
			gridControlDatastoreActions.Name = "gridControlDatastoreActions";
			gridControlDatastoreActions.Size = new Size(562, 419);
			gridControlDatastoreActions.TabIndex = 46;
			gridControlDatastoreActions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewDatastoreActions });
			// 
			// gridViewDatastoreActions
			// 
			gridViewDatastoreActions.GridControl = gridControlDatastoreActions;
			gridViewDatastoreActions.Name = "gridViewDatastoreActions";
			// 
			// labelControlNoOfDatastoreActions
			// 
			labelControlNoOfDatastoreActions.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			labelControlNoOfDatastoreActions.Appearance.Options.UseFont = true;
			labelControlNoOfDatastoreActions.Appearance.Options.UseTextOptions = true;
			labelControlNoOfDatastoreActions.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelControlNoOfDatastoreActions.Location = new Point(137, 15);
			labelControlNoOfDatastoreActions.Name = "labelControlNoOfDatastoreActions";
			labelControlNoOfDatastoreActions.Size = new Size(89, 13);
			labelControlNoOfDatastoreActions.TabIndex = 45;
			labelControlNoOfDatastoreActions.Text = "(No. of Actions):";
			// 
			// labelControlDatastoreActionCount
			// 
			labelControlDatastoreActionCount.AllowHtmlString = true;
			labelControlDatastoreActionCount.Appearance.Options.UseTextOptions = true;
			labelControlDatastoreActionCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelControlDatastoreActionCount.Location = new Point(17, 15);
			labelControlDatastoreActionCount.Name = "labelControlDatastoreActionCount";
			labelControlDatastoreActionCount.Size = new Size(117, 13);
			labelControlDatastoreActionCount.TabIndex = 44;
			labelControlDatastoreActionCount.Text = "Datastore Action Count:";
			// 
			// tabPageRollbackActions
			// 
			tabPageRollbackActions.Controls.Add(gridControlRollbackActions);
			tabPageRollbackActions.Controls.Add(labelControlNoOfRollbackActions);
			tabPageRollbackActions.Controls.Add(labelControlRollbackActions);
			tabPageRollbackActions.Name = "tabPageRollbackActions";
			tabPageRollbackActions.Size = new Size(568, 456);
			tabPageRollbackActions.Text = "Rollback Actions";
			// 
			// labelControlNoOfRollbackActions
			// 
			labelControlNoOfRollbackActions.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			labelControlNoOfRollbackActions.Appearance.Options.UseFont = true;
			labelControlNoOfRollbackActions.Appearance.Options.UseTextOptions = true;
			labelControlNoOfRollbackActions.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelControlNoOfRollbackActions.Location = new Point(132, 15);
			labelControlNoOfRollbackActions.Name = "labelControlNoOfRollbackActions";
			labelControlNoOfRollbackActions.Size = new Size(89, 13);
			labelControlNoOfRollbackActions.TabIndex = 45;
			labelControlNoOfRollbackActions.Text = "(No. of Actions):";
			// 
			// labelControlRollbackActions
			// 
			labelControlRollbackActions.Appearance.Options.UseTextOptions = true;
			labelControlRollbackActions.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelControlRollbackActions.Location = new Point(15, 15);
			labelControlRollbackActions.Name = "labelControlRollbackActions";
			labelControlRollbackActions.Size = new Size(113, 13);
			labelControlRollbackActions.TabIndex = 44;
			labelControlRollbackActions.Text = "Rollback Actions Count:";
			// 
			// EditPanelTransactionLog
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Name = "EditPanelTransactionLog";
			((System.ComponentModel.ISupportInitialize)tabControl).EndInit();
			tabControl.ResumeLayout(false);
			tabPageObjectName.ResumeLayout(false);
			tabPageObjectName.PerformLayout();
			((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
			((System.ComponentModel.ISupportInitialize)editorTransactionId.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorUser.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorClientId.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorStatus.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorCreationTime.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)gridControlRollbackActions).EndInit();
			((System.ComponentModel.ISupportInitialize)gridViewRollbackActions).EndInit();
			((System.ComponentModel.ISupportInitialize)tabControlTransactionRequest).EndInit();
			tabControlTransactionRequest.ResumeLayout(false);
			tabPageRollbackActionBinaries.ResumeLayout(false);
			tabPageRollbackActionBinaries.PerformLayout();
			((System.ComponentModel.ISupportInitialize)editorTransactionActionData.Properties).EndInit();
			tabPageTransactionRequests.ResumeLayout(false);
			tabPageTransactionRequests.PerformLayout();
			tabPageDatastoreActions.ResumeLayout(false);
			tabPageDatastoreActions.PerformLayout();
			((System.ComponentModel.ISupportInitialize)gridControlDatastoreActions).EndInit();
			((System.ComponentModel.ISupportInitialize)gridViewDatastoreActions).EndInit();
			tabPageRollbackActions.ResumeLayout(false);
			tabPageRollbackActions.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		protected DevExpress.XtraEditors.TextEdit editorTransactionId;
		protected DevExpress.XtraEditors.LabelControl labelControlTransactionId;
		protected DevExpress.XtraEditors.TextEdit editorUser;
		protected DevExpress.XtraEditors.LabelControl labelControlAdmin;
		protected DevExpress.XtraEditors.TextEdit editorClientId;
		protected DevExpress.XtraEditors.LabelControl labelControlClientId;
		protected DevExpress.XtraEditors.TextEdit editorCreationTime;
		protected DevExpress.XtraEditors.LabelControl labelControlCreationTime;
		protected DevExpress.XtraEditors.TextEdit editorStatus;
		protected DevExpress.XtraEditors.LabelControl labelControlStatus;
		protected DevExpress.XtraEditors.LabelControl labelControlTransactionRequestCount;
		protected DevExpress.XtraGrid.GridControl gridControlRollbackActions;
		protected DevExpress.XtraGrid.Views.Grid.GridView gridViewRollbackActions;
		protected DevExpress.XtraTab.XtraTabControl tabControlTransactionRequest;
		protected DevExpress.XtraTab.XtraTabPage tabPageTransactionRequests;
		protected DevExpress.XtraTab.XtraTabPage tabPageRollbackActionBinaries;
		protected DevExpress.XtraEditors.LabelControl labelControlNoOfRollbackActions;
		protected DevExpress.XtraEditors.LabelControl labelControlRollbackActions;
		protected DevExpress.XtraEditors.MemoEdit editorTransactionActionData;
		protected DevExpress.XtraEditors.LabelControl labelControlRollbackActionDataLength;
		protected DevExpress.XtraEditors.LabelControl labelControlRollbackActionDataNoOfBytes;
		protected DevExpress.XtraEditors.LabelControl labelControlNoOfTransactionActions;
		protected DevExpress.XtraTab.XtraTabPage tabPageDatastoreActions;
		protected DevExpress.XtraGrid.GridControl gridControlDatastoreActions;
		protected DevExpress.XtraGrid.Views.Grid.GridView gridViewDatastoreActions;
		protected DevExpress.XtraEditors.LabelControl labelControlNoOfDatastoreActions;
		protected DevExpress.XtraEditors.LabelControl labelControlDatastoreActionCount;
		protected DevExpress.XtraTab.XtraTabPage tabPageTransactionActions;
		protected DevExpress.XtraTab.XtraTabPage tabPageRollbackActions;
		protected DevExpress.XtraTab.XtraTabPage tabtabPageRollbackActionBinaries;
	}
}
