using System.Windows.Forms;
using System.Drawing;

namespace Simple.Objects.ServerMonitor
{
	partial class EditPanelGetServerVersionInfo
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
			editorSystemServerVersion = new DevExpress.XtraEditors.TextEdit();
			labelControlSystemServerVersion = new DevExpress.XtraEditors.LabelControl();
			editorAppServerVersion = new DevExpress.XtraEditors.TextEdit();
			labelControlAppServerVersion = new DevExpress.XtraEditors.LabelControl();
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
			((System.ComponentModel.ISupportInitialize)editorSystemServerVersion.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorAppServerVersion.Properties).BeginInit();
			SuspendLayout();
			// 
			// tabControlRequestResponse
			// 
			tabControlRequestResponse.Size = new Size(597, 535);
			// 
			// tabPageRequest
			// 
			tabPageRequest.Size = new Size(595, 510);
			// 
			// tabPageResponse
			// 
			tabPageResponse.Size = new Size(595, 510);
			// 
			// editorRequestPackageValue
			// 
			errorProvider.SetIconAlignment(editorRequestPackageValue, ErrorIconAlignment.MiddleRight);
			editorRequestPackageValue.Size = new Size(581, 410);
			// 
			// packageControlRequestHeader
			// 
			packageControlRequestHeader.Size = new Size(385, 79);
			// 
			// editorRequestPackageLength
			// 
			editorRequestPackageLength.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorRequestPackageLength.Properties.Appearance.Options.UseFont = true;
			editorRequestPackageLength.Size = new Size(103, 20);
			// 
			// editorResponsePackageValue
			// 
			errorProvider.SetIconAlignment(editorResponsePackageValue, ErrorIconAlignment.MiddleRight);
			editorResponsePackageValue.Size = new Size(581, 410);
			// 
			// packageHeaderControlResponse
			// 
			packageHeaderControlResponse.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			packageHeaderControlResponse.Size = new Size(385, 79);
			// 
			// editorResponsePackageLength
			// 
			editorResponsePackageLength.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorResponsePackageLength.Properties.Appearance.Options.UseFont = true;
			editorResponsePackageLength.Size = new Size(103, 20);
			// 
			// tabControlResponse
			// 
			tabControlResponse.Size = new Size(589, 441);
			// 
			// tabPageResponseDetails
			// 
			tabPageResponseDetails.Controls.Add(editorAppServerVersion);
			tabPageResponseDetails.Controls.Add(labelControlAppServerVersion);
			tabPageResponseDetails.Controls.Add(editorSystemServerVersion);
			tabPageResponseDetails.Controls.Add(labelControlSystemServerVersion);
			tabPageResponseDetails.Size = new Size(587, 416);
			// 
			// tabPageResponsePackageBinaries
			// 
			tabPageResponsePackageBinaries.Size = new Size(587, 416);
			// 
			// tabControlRequest
			// 
			tabControlRequest.Size = new Size(589, 441);
			// 
			// tabPageRequestDetails
			// 
			tabPageRequestDetails.Size = new Size(587, 416);
			// 
			// tabPageRequestPackageBinaries
			// 
			tabPageRequestPackageBinaries.Size = new Size(587, 416);
			// 
			// editorRequestPackageHeaderValue
			// 
			editorRequestPackageHeaderValue.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorRequestPackageHeaderValue.Properties.Appearance.Options.UseFont = true;
			editorRequestPackageHeaderValue.Size = new Size(91, 20);
			// 
			// editorResponsePackageHeaderValue
			// 
			editorResponsePackageHeaderValue.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorResponsePackageHeaderValue.Properties.Appearance.Options.UseFont = true;
			editorResponsePackageHeaderValue.Size = new Size(91, 20);
			// 
			// editorUser
			// 
			editorUser.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorUser.Properties.Appearance.Options.UseFont = true;
			editorUser.Size = new Size(256, 20);
			// 
			// editorMessageCodeOrRequestId
			// 
			editorMessageCodeOrRequestId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorMessageCodeOrRequestId.Properties.Appearance.Options.UseFont = true;
			editorMessageCodeOrRequestId.Size = new Size(256, 20);
			// 
			// editorActionType
			// 
			editorActionType.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorActionType.Properties.Appearance.Options.UseFont = true;
			editorActionType.Size = new Size(107, 20);
			// 
			// editorSessionKey
			// 
			editorSessionKey.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorSessionKey.Properties.Appearance.Options.UseFont = true;
			editorSessionKey.Size = new Size(107, 20);
			// 
			// labelControlMessageCodeOrRequestId
			// 
			labelControlMessageCodeOrRequestId.Appearance.Options.UseTextOptions = true;
			labelControlMessageCodeOrRequestId.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			// 
			// tabControl
			// 
			tabControl.Size = new Size(605, 628);
			// 
			// tabPageObjectName
			// 
			tabPageObjectName.Size = new Size(603, 603);
			// 
			// editorSystemServerVersion
			// 
			editorSystemServerVersion.Enabled = false;
			editorSystemServerVersion.Location = new Point(144, 23);
			editorSystemServerVersion.Name = "editorSystemServerVersion";
			editorSystemServerVersion.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorSystemServerVersion.Properties.Appearance.Options.UseFont = true;
			editorSystemServerVersion.Properties.ReadOnly = true;
			editorSystemServerVersion.Size = new Size(98, 20);
			editorSystemServerVersion.TabIndex = 3;
			// 
			// labelControlSystemServerVersion
			// 
			labelControlSystemServerVersion.Location = new Point(22, 26);
			labelControlSystemServerVersion.Name = "labelControlSystemServerVersion";
			labelControlSystemServerVersion.Size = new Size(112, 13);
			labelControlSystemServerVersion.TabIndex = 2;
			labelControlSystemServerVersion.Text = "System Server Version:";
			// 
			// editorAppServerVersion
			// 
			editorAppServerVersion.Enabled = false;
			editorAppServerVersion.Location = new Point(144, 49);
			editorAppServerVersion.Name = "editorAppServerVersion";
			editorAppServerVersion.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorAppServerVersion.Properties.Appearance.Options.UseFont = true;
			editorAppServerVersion.Properties.ReadOnly = true;
			editorAppServerVersion.Size = new Size(98, 20);
			editorAppServerVersion.TabIndex = 5;
			// 
			// labelControlAppServerVersion
			// 
			labelControlAppServerVersion.Location = new Point(22, 52);
			labelControlAppServerVersion.Name = "labelControlAppServerVersion";
			labelControlAppServerVersion.Size = new Size(99, 13);
			labelControlAppServerVersion.TabIndex = 4;
			labelControlAppServerVersion.Text = "App Server Version :";
			// 
			// EditPanelGetServerVersionInfo
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Name = "EditPanelGetServerVersionInfo";
			Size = new Size(611, 634);
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
			((System.ComponentModel.ISupportInitialize)editorSystemServerVersion.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorAppServerVersion.Properties).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private DevExpress.XtraEditors.TextEdit editorSystemServerVersion;
		private DevExpress.XtraEditors.LabelControl labelControlSystemServerVersion;
		private DevExpress.XtraEditors.TextEdit editorAppServerVersion;
		private DevExpress.XtraEditors.LabelControl labelControlAppServerVersion;
	}
}
