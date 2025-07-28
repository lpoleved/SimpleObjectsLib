using System.Windows.Forms;
using System.Drawing;

namespace Simple.Objects.ServerMonitor
{
	partial class EditPanelAuthenticateSession
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
			editorIsAuthenticated = new DevExpress.XtraEditors.TextEdit();
			labelControlIsAuthorized = new DevExpress.XtraEditors.LabelControl();
			labelControlUsername = new DevExpress.XtraEditors.LabelControl();
			editorUsername = new DevExpress.XtraEditors.TextEdit();
			labelControlPassword = new DevExpress.XtraEditors.LabelControl();
			editorPassword = new DevExpress.XtraEditors.TextEdit();
			editorUserId = new DevExpress.XtraEditors.TextEdit();
			labelControlUserId = new DevExpress.XtraEditors.LabelControl();
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
			((System.ComponentModel.ISupportInitialize)editorIsAuthenticated.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorUsername.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorPassword.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorUserId.Properties).BeginInit();
			SuspendLayout();
			// 
			// tabControlRequestResponse
			// 
			tabControlRequestResponse.Size = new Size(593, 535);
			// 
			// tabPageRequest
			// 
			tabPageRequest.Size = new Size(568, 510);
			// 
			// tabPageResponse
			// 
			tabPageResponse.Size = new Size(591, 510);
			// 
			// editorRequestPackageValue
			// 
			errorProvider.SetIconAlignment(editorRequestPackageValue, ErrorIconAlignment.MiddleRight);
			editorRequestPackageValue.Size = new Size(554, 410);
			// 
			// packageControlRequestHeader
			// 
			packageControlRequestHeader.Size = new Size(358, 79);
			// 
			// editorRequestPackageLength
			// 
			editorRequestPackageLength.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorRequestPackageLength.Properties.Appearance.Options.UseFont = true;
			// 
			// editorResponsePackageValue
			// 
			errorProvider.SetIconAlignment(editorResponsePackageValue, ErrorIconAlignment.MiddleRight);
			editorResponsePackageValue.Size = new Size(577, 410);
			// 
			// packageHeaderControlResponse
			// 
			packageHeaderControlResponse.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			packageHeaderControlResponse.Size = new Size(381, 79);
			// 
			// editorResponsePackageLength
			// 
			editorResponsePackageLength.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorResponsePackageLength.Properties.Appearance.Options.UseFont = true;
			// 
			// tabControlResponse
			// 
			tabControlResponse.Size = new Size(585, 441);
			// 
			// tabPageResponseDetails
			// 
			tabPageResponseDetails.Controls.Add(editorUserId);
			tabPageResponseDetails.Controls.Add(labelControlUserId);
			tabPageResponseDetails.Controls.Add(editorIsAuthenticated);
			tabPageResponseDetails.Controls.Add(labelControlIsAuthorized);
			tabPageResponseDetails.Size = new Size(583, 416);
			// 
			// tabPageResponsePackageBinaries
			// 
			tabPageResponsePackageBinaries.Size = new Size(583, 416);
			// 
			// tabControlRequest
			// 
			tabControlRequest.Size = new Size(539, 441);
			// 
			// tabPageRequestDetails
			// 
			tabPageRequestDetails.Controls.Add(labelControlPassword);
			tabPageRequestDetails.Controls.Add(editorPassword);
			tabPageRequestDetails.Controls.Add(labelControlUsername);
			tabPageRequestDetails.Controls.Add(editorUsername);
			tabPageRequestDetails.Size = new Size(537, 416);
			// 
			// tabPageRequestPackageBinaries
			// 
			tabPageRequestPackageBinaries.Size = new Size(560, 416);
			// 
			// editorRequestPackageHeaderValue
			// 
			editorRequestPackageHeaderValue.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorRequestPackageHeaderValue.Properties.Appearance.Options.UseFont = true;
			// 
			// editorResponsePackageHeaderValue
			// 
			editorResponsePackageHeaderValue.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorResponsePackageHeaderValue.Properties.Appearance.Options.UseFont = true;
			// 
			// editorUser
			// 
			editorUser.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorUser.Properties.Appearance.Options.UseFont = true;
			editorUser.Size = new Size(250, 20);
			// 
			// editorMessageCodeOrRequestId
			// 
			editorMessageCodeOrRequestId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorMessageCodeOrRequestId.Properties.Appearance.Options.UseFont = true;
			editorMessageCodeOrRequestId.Size = new Size(250, 20);
			// 
			// editorActionType
			// 
			editorActionType.Location = new Point(76, 35);
			editorActionType.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorActionType.Properties.Appearance.Options.UseFont = true;
			editorActionType.Size = new Size(108, 20);
			// 
			// labelControlPackageJobActionType
			// 
			labelControlPackageJobActionType.Location = new Point(12, 38);
			// 
			// editorSessionKey
			// 
			editorSessionKey.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorSessionKey.Properties.Appearance.Options.UseFont = true;
			editorSessionKey.Size = new Size(108, 20);
			// 
			// labelControlMessageCodeOrRequestId
			// 
			labelControlMessageCodeOrRequestId.Appearance.Options.UseTextOptions = true;
			labelControlMessageCodeOrRequestId.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			// 
			// tabControl
			// 
			tabControl.Size = new Size(601, 628);
			// 
			// tabPageObjectName
			// 
			tabPageObjectName.Size = new Size(599, 603);
			// 
			// editorIsAuthenticated
			// 
			editorIsAuthenticated.Enabled = false;
			editorIsAuthenticated.Location = new Point(84, 20);
			editorIsAuthenticated.Name = "editorIsAuthenticated";
			editorIsAuthenticated.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorIsAuthenticated.Properties.Appearance.Options.UseFont = true;
			editorIsAuthenticated.Properties.ReadOnly = true;
			editorIsAuthenticated.Size = new Size(75, 20);
			editorIsAuthenticated.TabIndex = 3;
			// 
			// labelControlIsAuthorized
			// 
			labelControlIsAuthorized.Location = new Point(12, 23);
			labelControlIsAuthorized.Name = "labelControlIsAuthorized";
			labelControlIsAuthorized.Size = new Size(68, 13);
			labelControlIsAuthorized.TabIndex = 2;
			labelControlIsAuthorized.Text = "Is Authorized:";
			// 
			// labelControlUsername
			// 
			labelControlUsername.Location = new Point(12, 23);
			labelControlUsername.Name = "labelControlUsername";
			labelControlUsername.Size = new Size(52, 13);
			labelControlUsername.TabIndex = 6;
			labelControlUsername.Text = "Username:";
			// 
			// editorUsername
			// 
			editorUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorUsername.Location = new Point(67, 20);
			editorUsername.Name = "editorUsername";
			editorUsername.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorUsername.Properties.Appearance.Options.UseFont = true;
			editorUsername.Properties.ReadOnly = true;
			editorUsername.Size = new Size(452, 20);
			editorUsername.TabIndex = 7;
			// 
			// labelControlPassword
			// 
			labelControlPassword.Location = new Point(14, 49);
			labelControlPassword.Name = "labelControlPassword";
			labelControlPassword.Size = new Size(50, 13);
			labelControlPassword.TabIndex = 8;
			labelControlPassword.Text = "Password:";
			// 
			// editorPassword
			// 
			editorPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorPassword.Location = new Point(67, 46);
			editorPassword.Name = "editorPassword";
			editorPassword.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorPassword.Properties.Appearance.Options.UseFont = true;
			editorPassword.Properties.ReadOnly = true;
			editorPassword.Size = new Size(452, 20);
			editorPassword.TabIndex = 9;
			// 
			// editorUserId
			// 
			editorUserId.Enabled = false;
			editorUserId.Location = new Point(84, 44);
			editorUserId.Name = "editorUserId";
			editorUserId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorUserId.Properties.Appearance.Options.UseFont = true;
			editorUserId.Properties.ReadOnly = true;
			editorUserId.Size = new Size(75, 20);
			editorUserId.TabIndex = 5;
			// 
			// labelControlUserId
			// 
			labelControlUserId.Location = new Point(12, 47);
			labelControlUserId.Name = "labelControlUserId";
			labelControlUserId.Size = new Size(36, 13);
			labelControlUserId.TabIndex = 4;
			labelControlUserId.Text = "UserId:";
			// 
			// EditPanelAuthenticateSession
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Name = "EditPanelAuthenticateSession";
			Size = new Size(607, 634);
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
			((System.ComponentModel.ISupportInitialize)editorIsAuthenticated.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorUsername.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorPassword.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorUserId.Properties).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private DevExpress.XtraEditors.TextEdit editorIsAuthenticated;
		private DevExpress.XtraEditors.LabelControl labelControlIsAuthorized;
		private DevExpress.XtraEditors.LabelControl labelControlUsername;
		private DevExpress.XtraEditors.TextEdit editorUsername;
		private DevExpress.XtraEditors.LabelControl labelControlPassword;
		private DevExpress.XtraEditors.TextEdit editorPassword;
		private DevExpress.XtraEditors.TextEdit editorUserId;
		private DevExpress.XtraEditors.LabelControl labelControlUserId;
	}
}
