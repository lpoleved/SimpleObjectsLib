using System.Windows.Forms;
using System.Drawing;

namespace Simple.Objects.ServerMonitor
{
	partial class EditPanelGetOneToOneForeignObject
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
			labelControlObjectKey = new DevExpress.XtraEditors.LabelControl();
			labelControlObjectIdInfo = new DevExpress.XtraEditors.LabelControl();
			labelControlRelationKey = new DevExpress.XtraEditors.LabelControl();
			editorRelationKey = new DevExpress.XtraEditors.TextEdit();
			editorObjectKey = new DevExpress.XtraEditors.TextEdit();
			labelControl1 = new DevExpress.XtraEditors.LabelControl();
			labelControlForeignObjectKey = new DevExpress.XtraEditors.LabelControl();
			editorForeignObjectKey = new DevExpress.XtraEditors.TextEdit();
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
			((System.ComponentModel.ISupportInitialize)editorRelationKey.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorObjectKey.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorForeignObjectKey.Properties).BeginInit();
			SuspendLayout();
			// 
			// tabControlRequestResponse
			// 
			tabControlRequestResponse.Size = new Size(633, 535);
			// 
			// tabPageRequest
			// 
			tabPageRequest.Size = new Size(631, 510);
			// 
			// tabPageResponse
			// 
			tabPageResponse.Size = new Size(631, 510);
			// 
			// editorRequestPackageValue
			// 
			errorProvider.SetIconAlignment(editorRequestPackageValue, ErrorIconAlignment.MiddleRight);
			editorRequestPackageValue.Size = new Size(577, 410);
			// 
			// packageControlRequestHeader
			// 
			packageControlRequestHeader.Size = new Size(421, 79);
			// 
			// editorRequestPackageLength
			// 
			editorRequestPackageLength.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorRequestPackageLength.Properties.Appearance.Options.UseFont = true;
			// 
			// labelControlRequestPackageLength
			// 
			labelControlRequestPackageLength.Location = new Point(8, 16);
			// 
			// editorResponsePackageValue
			// 
			errorProvider.SetIconAlignment(editorResponsePackageValue, ErrorIconAlignment.MiddleRight);
			editorResponsePackageValue.Size = new Size(577, 410);
			// 
			// packageHeaderControlResponse
			// 
			packageHeaderControlResponse.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			packageHeaderControlResponse.Size = new Size(418, 79);
			// 
			// editorResponsePackageLength
			// 
			editorResponsePackageLength.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorResponsePackageLength.Properties.Appearance.Options.UseFont = true;
			// 
			// labelControlResponsePackageLength
			// 
			labelControlResponsePackageLength.Location = new Point(8, 16);
			// 
			// tabControlResponse
			// 
			tabControlResponse.Size = new Size(625, 441);
			// 
			// tabPageResponseDetails
			// 
			tabPageResponseDetails.Controls.Add(labelControl1);
			tabPageResponseDetails.Controls.Add(labelControlForeignObjectKey);
			tabPageResponseDetails.Controls.Add(editorForeignObjectKey);
			tabPageResponseDetails.Size = new Size(623, 416);
			// 
			// tabPageResponsePackageBinaries
			// 
			tabPageResponsePackageBinaries.Size = new Size(583, 416);
			// 
			// tabControlRequest
			// 
			tabControlRequest.Size = new Size(625, 441);
			// 
			// tabPageRequestDetails
			// 
			tabPageRequestDetails.Controls.Add(labelControlRelationKey);
			tabPageRequestDetails.Controls.Add(editorRelationKey);
			tabPageRequestDetails.Controls.Add(labelControlObjectIdInfo);
			tabPageRequestDetails.Controls.Add(labelControlObjectKey);
			tabPageRequestDetails.Controls.Add(editorObjectKey);
			tabPageRequestDetails.Size = new Size(623, 416);
			// 
			// tabPageRequestPackageBinaries
			// 
			tabPageRequestPackageBinaries.Size = new Size(560, 416);
			// 
			// editorRequestPackageHeaderValue
			// 
			editorRequestPackageHeaderValue.Location = new Point(101, 39);
			editorRequestPackageHeaderValue.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorRequestPackageHeaderValue.Properties.Appearance.Options.UseFont = true;
			editorRequestPackageHeaderValue.Size = new Size(99, 20);
			// 
			// labelControlRequestHeaderInfoValue
			// 
			labelControlRequestHeaderInfoValue.Location = new Point(8, 42);
			// 
			// editorResponsePackageHeaderValue
			// 
			editorResponsePackageHeaderValue.Location = new Point(101, 39);
			editorResponsePackageHeaderValue.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorResponsePackageHeaderValue.Properties.Appearance.Options.UseFont = true;
			editorResponsePackageHeaderValue.Size = new Size(99, 20);
			// 
			// labelControlResponseHeaderInfoValue
			// 
			labelControlResponseHeaderInfoValue.Location = new Point(8, 42);
			// 
			// editorUser
			// 
			editorUser.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorUser.Properties.Appearance.Options.UseFont = true;
			editorUser.Size = new Size(199, 20);
			// 
			// editorMessageCodeOrRequestId
			// 
			editorMessageCodeOrRequestId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorMessageCodeOrRequestId.Properties.Appearance.Options.UseFont = true;
			editorMessageCodeOrRequestId.Size = new Size(199, 20);
			// 
			// editorActionType
			// 
			editorActionType.Location = new Point(76, 35);
			editorActionType.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorActionType.Properties.Appearance.Options.UseFont = true;
			editorActionType.Size = new Size(116, 20);
			// 
			// labelControlPackageJobActionType
			// 
			labelControlPackageJobActionType.Location = new Point(12, 38);
			// 
			// editorSessionKey
			// 
			editorSessionKey.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
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
			tabControl.Size = new Size(641, 628);
			// 
			// tabPageObjectName
			// 
			tabPageObjectName.Size = new Size(639, 603);
			// 
			// labelControlObjectKey
			// 
			labelControlObjectKey.Location = new Point(9, 24);
			labelControlObjectKey.Name = "labelControlObjectKey";
			labelControlObjectKey.Size = new Size(57, 13);
			labelControlObjectKey.TabIndex = 6;
			labelControlObjectKey.Text = "Object Key:";
			// 
			// labelControlObjectIdInfo
			// 
			labelControlObjectIdInfo.Location = new Point(72, 47);
			labelControlObjectIdInfo.Name = "labelControlObjectIdInfo";
			labelControlObjectIdInfo.Size = new Size(82, 13);
			labelControlObjectIdInfo.TabIndex = 8;
			labelControlObjectIdInfo.Text = "TableId.ObjectId";
			// 
			// labelControlRelationKey
			// 
			labelControlRelationKey.Location = new Point(4, 69);
			labelControlRelationKey.Name = "labelControlRelationKey";
			labelControlRelationKey.Size = new Size(64, 13);
			labelControlRelationKey.TabIndex = 10;
			labelControlRelationKey.Text = "Relation Key:";
			// 
			// editorRelationKey
			// 
			editorRelationKey.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorRelationKey.Location = new Point(72, 66);
			editorRelationKey.Name = "editorRelationKey";
			editorRelationKey.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorRelationKey.Properties.Appearance.Options.UseFont = true;
			editorRelationKey.Properties.ReadOnly = true;
			editorRelationKey.Size = new Size(541, 20);
			editorRelationKey.TabIndex = 11;
			// 
			// editorObjectKey
			// 
			editorObjectKey.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorObjectKey.Location = new Point(72, 21);
			editorObjectKey.Name = "editorObjectKey";
			editorObjectKey.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorObjectKey.Properties.Appearance.Options.UseFont = true;
			editorObjectKey.Properties.ReadOnly = true;
			editorObjectKey.Size = new Size(538, 20);
			editorObjectKey.TabIndex = 7;
			// 
			// labelControl1
			// 
			labelControl1.Location = new Point(107, 53);
			labelControl1.Name = "labelControl1";
			labelControl1.Size = new Size(82, 13);
			labelControl1.TabIndex = 11;
			labelControl1.Text = "TableId.ObjectId";
			// 
			// labelControlForeignObjectKey
			// 
			labelControlForeignObjectKey.Location = new Point(5, 30);
			labelControlForeignObjectKey.Name = "labelControlForeignObjectKey";
			labelControlForeignObjectKey.Size = new Size(96, 13);
			labelControlForeignObjectKey.TabIndex = 9;
			labelControlForeignObjectKey.Text = "Foreign Object Key:";
			// 
			// editorForeignObjectKey
			// 
			editorForeignObjectKey.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorForeignObjectKey.Location = new Point(107, 27);
			editorForeignObjectKey.Name = "editorForeignObjectKey";
			editorForeignObjectKey.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorForeignObjectKey.Properties.Appearance.Options.UseFont = true;
			editorForeignObjectKey.Properties.ReadOnly = true;
			editorForeignObjectKey.Size = new Size(493, 20);
			editorForeignObjectKey.TabIndex = 10;
			// 
			// EditPanelGetOneToOneForeignObject
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Name = "EditPanelGetOneToOneForeignObject";
			Size = new Size(647, 634);
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
			((System.ComponentModel.ISupportInitialize)editorRelationKey.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorObjectKey.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorForeignObjectKey.Properties).EndInit();
			ResumeLayout(false);
		}

		#endregion

		protected DevExpress.XtraEditors.LabelControl labelControlObjectKey;
		protected DevExpress.XtraEditors.LabelControl labelControlObjectIdInfo;
		private DevExpress.XtraEditors.LabelControl labelControlRelationKey;
		private DevExpress.XtraEditors.TextEdit editorRelationKey;
		protected DevExpress.XtraEditors.TextEdit editorObjectKey;
		protected DevExpress.XtraEditors.LabelControl labelControl1;
		protected DevExpress.XtraEditors.LabelControl labelControlForeignObjectKey;
		protected DevExpress.XtraEditors.TextEdit editorForeignObjectKey;
	}
}
