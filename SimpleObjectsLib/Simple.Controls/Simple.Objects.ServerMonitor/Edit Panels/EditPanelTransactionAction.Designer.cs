using System.Windows.Forms;
using System.Drawing;

namespace Simple.Objects.ServerMonitor
{
	partial class EditPanelTransactionAction
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
			labelControlTransactionSucceed = new DevExpress.XtraEditors.LabelControl();
			editorTransactionSucceed = new DevExpress.XtraEditors.TextEdit();
			labelControlInfoMessage = new DevExpress.XtraEditors.LabelControl();
			labelControlTransactionId = new DevExpress.XtraEditors.LabelControl();
			editorTransactionId = new DevExpress.XtraEditors.TextEdit();
			gridControlGetRequestsDetails = new DevExpress.XtraGrid.GridControl();
			gridViewGetRequestDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
			labelControlTransactionRequestActions = new DevExpress.XtraEditors.LabelControl();
			labelControlTransactionRequestNumOfActions = new DevExpress.XtraEditors.LabelControl();
			editorNewIds = new DevExpress.XtraEditors.TextEdit();
			editorInfoMessage = new DevExpress.XtraEditors.MemoEdit();
			labelControlNewIds = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)tabControlRequestResponse).BeginInit();
			tabControlRequestResponse.SuspendLayout();
			tabPageRequest.SuspendLayout();
			tabPageResponse.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)editorRequestPackageValue.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorRequestPackageLength.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorResponsePackageValue.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorResponsePackageLength.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)tabControlResponse).BeginInit();
			tabControlResponse.SuspendLayout();
			tabPageResponseDetails.SuspendLayout();
			tabPageResponsePackageBinaries.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)tabControlRequest).BeginInit();
			tabControlRequest.SuspendLayout();
			tabPageRequestDetails.SuspendLayout();
			tabPageRequestPackageBinaries.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)editorRequestPackageHeaderValue.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorResponsePackageHeaderValue.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorUser.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorMessageCodeOrRequestId.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorActionType.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorSessionKey.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)tabControl).BeginInit();
			tabControl.SuspendLayout();
			tabPageObjectName.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorTransactionSucceed.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorTransactionId.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridControlGetRequestsDetails).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridViewGetRequestDetails).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorNewIds.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorInfoMessage.Properties).BeginInit();
			SuspendLayout();
			// 
			// tabControlRequestResponse
			// 
			tabControlRequestResponse.Size = new Size(624, 535);
			// 
			// tabPageRequest
			// 
			tabPageRequest.Size = new Size(622, 510);
			// 
			// tabPageResponse
			// 
			tabPageResponse.Size = new Size(622, 510);
			// 
			// editorRequestPackageValue
			// 
			errorProvider.SetIconAlignment(editorRequestPackageValue, ErrorIconAlignment.MiddleRight);
			editorRequestPackageValue.Size = new Size(604, 410);
			// 
			// packageControlRequestHeader
			// 
			packageControlRequestHeader.Location = new Point(210, 5);
			packageControlRequestHeader.Size = new Size(412, 79);
			// 
			// editorRequestPackageLength
			// 
			// 
			// editorResponsePackageValue
			// 
			errorProvider.SetIconAlignment(editorResponsePackageValue, ErrorIconAlignment.MiddleRight);
			editorResponsePackageValue.Size = new Size(604, 410);
			// 
			// packageHeaderControlResponse
			// 
			packageHeaderControlResponse.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			packageHeaderControlResponse.Location = new Point(210, 5);
			packageHeaderControlResponse.Size = new Size(409, 79);
			// 
			// editorResponsePackageLength
			// 
			// 
			// tabControlResponse
			// 
			tabControlResponse.Size = new Size(616, 441);
			// 
			// tabPageResponseDetails
			// 
			tabPageResponseDetails.Controls.Add(labelControlNewIds);
			tabPageResponseDetails.Controls.Add(editorInfoMessage);
			tabPageResponseDetails.Controls.Add(editorNewIds);
			tabPageResponseDetails.Controls.Add(labelControlTransactionId);
			tabPageResponseDetails.Controls.Add(editorTransactionId);
			tabPageResponseDetails.Controls.Add(labelControlInfoMessage);
			tabPageResponseDetails.Controls.Add(labelControlTransactionSucceed);
			tabPageResponseDetails.Controls.Add(editorTransactionSucceed);
			tabPageResponseDetails.Size = new Size(614, 416);
			// 
			// tabPageResponsePackageBinaries
			// 
			tabPageResponsePackageBinaries.Size = new Size(610, 416);
			// 
			// tabControlRequest
			// 
			tabControlRequest.Size = new Size(616, 441);
			// 
			// tabPageRequestDetails
			// 
			tabPageRequestDetails.Controls.Add(labelControlTransactionRequestNumOfActions);
			tabPageRequestDetails.Controls.Add(labelControlTransactionRequestActions);
			tabPageRequestDetails.Controls.Add(gridControlGetRequestsDetails);
			tabPageRequestDetails.Size = new Size(614, 416);
			// 
			// tabPageRequestPackageBinaries
			// 
			tabPageRequestPackageBinaries.Size = new Size(610, 416);
			// 
			// editorRequestPackageHeaderValue
			// 
			// 
			// editorResponsePackageHeaderValue
			// 
			editorResponsePackageHeaderValue.Location = new Point(103, 39);
			editorResponsePackageHeaderValue.Size = new Size(97, 20);
			// 
			// editorUser
			// 
			editorUser.Location = new Point(258, 12);
			editorUser.Properties.Appearance.Options.UseFont = true;
			editorUser.Size = new Size(350, 20);
			// 
			// labelControlUser
			// 
			labelControlUser.Location = new Point(226, 15);
			// 
			// editorMessageCodeOrRequestId
			// 
			editorMessageCodeOrRequestId.Location = new Point(258, 36);
			editorMessageCodeOrRequestId.Properties.Appearance.Options.UseFont = true;
			editorMessageCodeOrRequestId.Size = new Size(350, 20);
			// 
			// editorActionType
			// 
			editorActionType.Location = new Point(76, 36);
			editorActionType.Properties.Appearance.Options.UseFont = true;
			editorActionType.Size = new Size(116, 20);
			// 
			// labelControlPackageJobActionType
			// 
			labelControlPackageJobActionType.Location = new Point(9, 39);
			// 
			// editorSessionKey
			// 
			editorSessionKey.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorSessionKey.Properties.Appearance.Options.UseFont = true;
			editorSessionKey.Size = new Size(116, 20);
			// 
			// labelControlMessageCodeOrRequestId
			// 
			labelControlMessageCodeOrRequestId.Appearance.Options.UseTextOptions = true;
			labelControlMessageCodeOrRequestId.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			// 
			// tabControl
			// 
			tabControl.Size = new Size(628, 628);
			// 
			// tabPageObjectName
			// 
			tabPageObjectName.Size = new Size(626, 603);
			// 
			// labelControlTransactionSucceed
			// 
			labelControlTransactionSucceed.Location = new Point(7, 34);
			labelControlTransactionSucceed.Name = "labelControlTransactionSucceed";
			labelControlTransactionSucceed.Size = new Size(103, 13);
			labelControlTransactionSucceed.TabIndex = 8;
			labelControlTransactionSucceed.Text = "Transaction Succeed:";
			// 
			// editorTransactionSucceed
			// 
			editorTransactionSucceed.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorTransactionSucceed.Location = new Point(116, 31);
			editorTransactionSucceed.Name = "editorTransactionSucceed";
			editorTransactionSucceed.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorTransactionSucceed.Properties.Appearance.Options.UseFont = true;
			editorTransactionSucceed.Properties.ReadOnly = true;
			editorTransactionSucceed.Size = new Size(484, 20);
			editorTransactionSucceed.TabIndex = 9;
			// 
			// labelControlInfoMessage
			// 
			labelControlInfoMessage.Appearance.Options.UseTextOptions = true;
			labelControlInfoMessage.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelControlInfoMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			labelControlInfoMessage.Location = new Point(7, 142);
			labelControlInfoMessage.Name = "labelControlInfoMessage";
			labelControlInfoMessage.Size = new Size(103, 25);
			labelControlInfoMessage.TabIndex = 10;
			labelControlInfoMessage.Text = "Info/Error Message:";
			// 
			// labelControlTransactionId
			// 
			labelControlTransactionId.Location = new Point(37, 60);
			labelControlTransactionId.Name = "labelControlTransactionId";
			labelControlTransactionId.Size = new Size(73, 13);
			labelControlTransactionId.TabIndex = 12;
			labelControlTransactionId.Text = "Transaction Id:";
			// 
			// editorTransactionId
			// 
			editorTransactionId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorTransactionId.Location = new Point(116, 57);
			editorTransactionId.Name = "editorTransactionId";
			editorTransactionId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorTransactionId.Properties.Appearance.Options.UseFont = true;
			editorTransactionId.Properties.ReadOnly = true;
			editorTransactionId.Size = new Size(484, 20);
			editorTransactionId.TabIndex = 13;
			// 
			// gridControlGetRequestsDetails
			// 
			gridControlGetRequestsDetails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			gridControlGetRequestsDetails.Location = new Point(3, 34);
			gridControlGetRequestsDetails.MainView = gridViewGetRequestDetails;
			gridControlGetRequestsDetails.Name = "gridControlGetRequestsDetails";
			gridControlGetRequestsDetails.Size = new Size(608, 379);
			gridControlGetRequestsDetails.TabIndex = 20;
			gridControlGetRequestsDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewGetRequestDetails });
			// 
			// gridViewGetRequestDetails
			// 
			gridViewGetRequestDetails.GridControl = gridControlGetRequestsDetails;
			gridViewGetRequestDetails.Name = "gridViewGetRequestDetails";
			// 
			// labelControlTransactionRequestActions
			// 
			labelControlTransactionRequestActions.Appearance.Options.UseTextOptions = true;
			labelControlTransactionRequestActions.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelControlTransactionRequestActions.Location = new Point(6, 15);
			labelControlTransactionRequestActions.Name = "labelControlTransactionRequestActions";
			labelControlTransactionRequestActions.Size = new Size(134, 13);
			labelControlTransactionRequestActions.TabIndex = 32;
			labelControlTransactionRequestActions.Text = "Transaction Request Details";
			// 
			// labelControlTransactionRequestNumOfActions
			// 
			labelControlTransactionRequestNumOfActions.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			labelControlTransactionRequestNumOfActions.Appearance.Options.UseFont = true;
			labelControlTransactionRequestNumOfActions.Appearance.Options.UseTextOptions = true;
			labelControlTransactionRequestNumOfActions.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelControlTransactionRequestNumOfActions.Location = new Point(142, 15);
			labelControlTransactionRequestNumOfActions.Name = "labelControlTransactionRequestNumOfActions";
			labelControlTransactionRequestNumOfActions.Size = new Size(89, 13);
			labelControlTransactionRequestNumOfActions.TabIndex = 44;
			labelControlTransactionRequestNumOfActions.Text = "(No. of Actions):";
			// 
			// editorNewIds
			// 
			editorNewIds.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorNewIds.Location = new Point(116, 83);
			editorNewIds.Name = "editorNewIds";
			editorNewIds.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorNewIds.Properties.Appearance.Options.UseFont = true;
			editorNewIds.Properties.ReadOnly = true;
			editorNewIds.Size = new Size(484, 20);
			editorNewIds.TabIndex = 14;
			// 
			// editorInfoMessage
			// 
			editorInfoMessage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorInfoMessage.Location = new Point(116, 109);
			editorInfoMessage.Name = "editorInfoMessage";
			editorInfoMessage.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorInfoMessage.Properties.Appearance.Options.UseFont = true;
			editorInfoMessage.Size = new Size(484, 86);
			editorInfoMessage.TabIndex = 15;
			// 
			// labelControlNewIds
			// 
			labelControlNewIds.Location = new Point(67, 86);
			labelControlNewIds.Name = "labelControlNewIds";
			labelControlNewIds.Size = new Size(43, 13);
			labelControlNewIds.TabIndex = 16;
			labelControlNewIds.Text = "New Ids:";
			// 
			// EditPanelTransactionAction
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Name = "EditPanelTransactionAction";
			Size = new Size(634, 634);
			((System.ComponentModel.ISupportInitialize)tabControlRequestResponse).EndInit();
			tabControlRequestResponse.ResumeLayout(false);
			tabPageRequest.ResumeLayout(false);
			tabPageRequest.PerformLayout();
			tabPageResponse.ResumeLayout(false);
			tabPageResponse.PerformLayout();
			((System.ComponentModel.ISupportInitialize)editorRequestPackageValue.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorRequestPackageLength.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorResponsePackageValue.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorResponsePackageLength.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)tabControlResponse).EndInit();
			tabControlResponse.ResumeLayout(false);
			tabPageResponseDetails.ResumeLayout(false);
			tabPageResponseDetails.PerformLayout();
			tabPageResponsePackageBinaries.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)tabControlRequest).EndInit();
			tabControlRequest.ResumeLayout(false);
			tabPageRequestDetails.ResumeLayout(false);
			tabPageRequestDetails.PerformLayout();
			tabPageRequestPackageBinaries.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)editorRequestPackageHeaderValue.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorResponsePackageHeaderValue.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorUser.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorMessageCodeOrRequestId.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorActionType.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorSessionKey.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)tabControl).EndInit();
			tabControl.ResumeLayout(false);
			tabPageObjectName.ResumeLayout(false);
			tabPageObjectName.PerformLayout();
			((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
			((System.ComponentModel.ISupportInitialize)editorTransactionSucceed.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorTransactionId.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)gridControlGetRequestsDetails).EndInit();
			((System.ComponentModel.ISupportInitialize)gridViewGetRequestDetails).EndInit();
			((System.ComponentModel.ISupportInitialize)editorNewIds.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorInfoMessage.Properties).EndInit();
			ResumeLayout(false);
		}

		#endregion
		private DevExpress.XtraEditors.LabelControl labelControlTransactionSucceed;
		private DevExpress.XtraEditors.TextEdit editorTransactionSucceed;
		private DevExpress.XtraEditors.LabelControl labelControlInfoMessage;
		private DevExpress.XtraEditors.LabelControl labelControlTransactionId;
		private DevExpress.XtraEditors.TextEdit editorTransactionId;
		private DevExpress.XtraGrid.GridControl gridControlGetRequestsDetails;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewGetRequestDetails;
		protected DevExpress.XtraEditors.LabelControl labelControlTransactionRequestActions;
		protected DevExpress.XtraEditors.LabelControl labelControlTransactionRequestNumOfActions;
		private DevExpress.XtraEditors.TextEdit editorNewIds;
		private DevExpress.XtraEditors.LabelControl labelControlNewIds;
		private DevExpress.XtraEditors.MemoEdit editorInfoMessage;
	}
}
