using System.Windows.Forms;
using System.Drawing;

namespace Simple.Objects.ServerMonitor
{
	partial class EditPanelSessionPackageRequestResponse
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
			tabControlRequestResponse = new DevExpress.XtraTab.XtraTabControl();
			tabPageRequest = new DevExpress.XtraTab.XtraTabPage();
			packageControlRequestHeader = new PackageHeaderInfoControl();
			editorRequestPackageHeaderValue = new DevExpress.XtraEditors.TextEdit();
			labelControlRequestHeaderInfoValue = new DevExpress.XtraEditors.LabelControl();
			tabControlRequest = new DevExpress.XtraTab.XtraTabControl();
			tabPageRequestDetails = new DevExpress.XtraTab.XtraTabPage();
			tabPageRequestPackageBinaries = new DevExpress.XtraTab.XtraTabPage();
			editorRequestPackageValue = new DevExpress.XtraEditors.MemoEdit();
			editorRequestPackageLength = new DevExpress.XtraEditors.TextEdit();
			labelControlRequestPackageLength = new DevExpress.XtraEditors.LabelControl();
			tabPageResponse = new DevExpress.XtraTab.XtraTabPage();
			packageHeaderControlResponse = new PackageHeaderInfoControl();
			editorResponsePackageHeaderValue = new DevExpress.XtraEditors.TextEdit();
			labelControlResponseHeaderInfoValue = new DevExpress.XtraEditors.LabelControl();
			tabControlResponse = new DevExpress.XtraTab.XtraTabControl();
			tabPageResponseDetails = new DevExpress.XtraTab.XtraTabPage();
			tabPageResponsePackageBinaries = new DevExpress.XtraTab.XtraTabPage();
			editorResponsePackageValue = new DevExpress.XtraEditors.MemoEdit();
			editorResponsePackageLength = new DevExpress.XtraEditors.TextEdit();
			labelControlResponsePackageLength = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)editorUser.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorMessageCodeOrRequestId.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorActionType.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorSessionKey.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)tabControl).BeginInit();
			tabControl.SuspendLayout();
			tabPageObjectName.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
			((System.ComponentModel.ISupportInitialize)tabControlRequestResponse).BeginInit();
			tabControlRequestResponse.SuspendLayout();
			tabPageRequest.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)editorRequestPackageHeaderValue.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)tabControlRequest).BeginInit();
			tabControlRequest.SuspendLayout();
			tabPageRequestPackageBinaries.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)editorRequestPackageValue.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorRequestPackageLength.Properties).BeginInit();
			tabPageResponse.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)editorResponsePackageHeaderValue.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)tabControlResponse).BeginInit();
			tabControlResponse.SuspendLayout();
			tabPageResponsePackageBinaries.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)editorResponsePackageValue.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorResponsePackageLength.Properties).BeginInit();
			SuspendLayout();
			// 
			// editorUser
			// 
			editorUser.Location = new Point(254, 12);
			editorUser.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorUser.Properties.Appearance.Options.UseFont = true;
			editorUser.Size = new Size(498, 20);
			// 
			// labelControlUser
			// 
			labelControlUser.Location = new Point(224, 15);
			// 
			// editorMessageCodeOrRequestId
			// 
			editorMessageCodeOrRequestId.Location = new Point(254, 36);
			editorMessageCodeOrRequestId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorMessageCodeOrRequestId.Properties.Appearance.Options.UseFont = true;
			editorMessageCodeOrRequestId.Size = new Size(498, 20);
			// 
			// editorActionType
			// 
			editorActionType.Location = new Point(76, 34);
			editorActionType.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorActionType.Properties.Appearance.Options.UseFont = true;
			// 
			// labelControlPackageJobActionType
			// 
			labelControlPackageJobActionType.Location = new Point(12, 37);
			// 
			// editorSessionKey
			// 
			editorSessionKey.Location = new Point(76, 12);
			editorSessionKey.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorSessionKey.Properties.Appearance.Options.UseFont = true;
			// 
			// labelControlMessageCodeOrRequestId
			// 
			labelControlMessageCodeOrRequestId.Appearance.Options.UseTextOptions = true;
			labelControlMessageCodeOrRequestId.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelControlMessageCodeOrRequestId.Location = new Point(198, 39);
			labelControlMessageCodeOrRequestId.Size = new Size(57, 13);
			labelControlMessageCodeOrRequestId.Text = "Request Id:";
			// 
			// tabControl
			// 
			tabControl.Size = new Size(624, 628);
			// 
			// tabPageObjectName
			// 
			tabPageObjectName.Controls.Add(tabControlRequestResponse);
			tabPageObjectName.Size = new Size(622, 603);
			tabPageObjectName.Text = "Socket Package Request";
			tabPageObjectName.Controls.SetChildIndex(labelControlMessageCodeOrRequestId, 0);
			tabPageObjectName.Controls.SetChildIndex(labelControlSessionKey, 0);
			tabPageObjectName.Controls.SetChildIndex(editorSessionKey, 0);
			tabPageObjectName.Controls.SetChildIndex(labelControlUser, 0);
			tabPageObjectName.Controls.SetChildIndex(editorUser, 0);
			tabPageObjectName.Controls.SetChildIndex(labelControlPackageJobActionType, 0);
			tabPageObjectName.Controls.SetChildIndex(editorActionType, 0);
			tabPageObjectName.Controls.SetChildIndex(tabControlRequestResponse, 0);
			tabPageObjectName.Controls.SetChildIndex(editorMessageCodeOrRequestId, 0);
			// 
			// tabControlRequestResponse
			// 
			tabControlRequestResponse.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			tabControlRequestResponse.Location = new Point(3, 65);
			tabControlRequestResponse.Name = "tabControlRequestResponse";
			tabControlRequestResponse.SelectedTabPage = tabPageRequest;
			tabControlRequestResponse.Size = new Size(616, 535);
			tabControlRequestResponse.TabIndex = 29;
			tabControlRequestResponse.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { tabPageRequest, tabPageResponse });
			// 
			// tabPageRequest
			// 
			tabPageRequest.Controls.Add(packageControlRequestHeader);
			tabPageRequest.Controls.Add(editorRequestPackageHeaderValue);
			tabPageRequest.Controls.Add(labelControlRequestHeaderInfoValue);
			tabPageRequest.Controls.Add(tabControlRequest);
			tabPageRequest.Controls.Add(editorRequestPackageLength);
			tabPageRequest.Controls.Add(labelControlRequestPackageLength);
			tabPageRequest.Name = "tabPageRequest";
			tabPageRequest.Size = new Size(614, 510);
			tabPageRequest.Text = "Request";
			// 
			// packageControlRequestHeader
			// 
			packageControlRequestHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			packageControlRequestHeader.Location = new Point(206, 5);
			packageControlRequestHeader.Name = "packageControlRequestHeader";
			packageControlRequestHeader.Size = new Size(404, 79);
			packageControlRequestHeader.TabIndex = 34;
			// 
			// editorRequestPackageHeaderValue
			// 
			editorRequestPackageHeaderValue.ImeMode = ImeMode.KatakanaHalf;
			editorRequestPackageHeaderValue.Location = new Point(104, 39);
			editorRequestPackageHeaderValue.Name = "editorRequestPackageHeaderValue";
			editorRequestPackageHeaderValue.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorRequestPackageHeaderValue.Properties.Appearance.Options.UseFont = true;
			editorRequestPackageHeaderValue.Properties.ReadOnly = true;
			editorRequestPackageHeaderValue.Size = new Size(96, 20);
			editorRequestPackageHeaderValue.TabIndex = 40;
			// 
			// labelControlRequestHeaderInfoValue
			// 
			labelControlRequestHeaderInfoValue.Location = new Point(10, 42);
			labelControlRequestHeaderInfoValue.Name = "labelControlRequestHeaderInfoValue";
			labelControlRequestHeaderInfoValue.Size = new Size(91, 13);
			labelControlRequestHeaderInfoValue.TabIndex = 39;
			labelControlRequestHeaderInfoValue.Text = "Header Info Value:";
			// 
			// tabControlRequest
			// 
			tabControlRequest.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			tabControlRequest.Location = new Point(3, 66);
			tabControlRequest.Name = "tabControlRequest";
			tabControlRequest.SelectedTabPage = tabPageRequestDetails;
			tabControlRequest.Size = new Size(608, 442);
			tabControlRequest.TabIndex = 38;
			tabControlRequest.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { tabPageRequestDetails, tabPageRequestPackageBinaries });
			// 
			// tabPageRequestDetails
			// 
			tabPageRequestDetails.Name = "tabPageRequestDetails";
			tabPageRequestDetails.Size = new Size(606, 417);
			tabPageRequestDetails.Text = "Body Details";
			// 
			// tabPageRequestPackageBinaries
			// 
			tabPageRequestPackageBinaries.Controls.Add(editorRequestPackageValue);
			tabPageRequestPackageBinaries.Name = "tabPageRequestPackageBinaries";
			tabPageRequestPackageBinaries.Size = new Size(606, 417);
			tabPageRequestPackageBinaries.Text = "Package Binaries";
			// 
			// editorRequestPackageValue
			// 
			editorRequestPackageValue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			errorProvider.SetIconAlignment(editorRequestPackageValue, ErrorIconAlignment.MiddleRight);
			editorRequestPackageValue.Location = new Point(3, 3);
			editorRequestPackageValue.Name = "editorRequestPackageValue";
			editorRequestPackageValue.Properties.MaxLength = 2000;
			editorRequestPackageValue.Properties.ReadOnly = true;
			editorRequestPackageValue.Size = new Size(600, 410);
			editorRequestPackageValue.TabIndex = 36;
			// 
			// editorRequestPackageLength
			// 
			editorRequestPackageLength.ImeMode = ImeMode.KatakanaHalf;
			editorRequestPackageLength.Location = new Point(92, 13);
			editorRequestPackageLength.Name = "editorRequestPackageLength";
			editorRequestPackageLength.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorRequestPackageLength.Properties.Appearance.Options.UseFont = true;
			editorRequestPackageLength.Properties.ReadOnly = true;
			editorRequestPackageLength.Size = new Size(108, 20);
			editorRequestPackageLength.TabIndex = 33;
			// 
			// labelControlRequestPackageLength
			// 
			labelControlRequestPackageLength.Location = new Point(10, 16);
			labelControlRequestPackageLength.Name = "labelControlRequestPackageLength";
			labelControlRequestPackageLength.Size = new Size(80, 13);
			labelControlRequestPackageLength.TabIndex = 32;
			labelControlRequestPackageLength.Text = "Package Length:";
			// 
			// tabPageResponse
			// 
			tabPageResponse.Controls.Add(packageHeaderControlResponse);
			tabPageResponse.Controls.Add(editorResponsePackageHeaderValue);
			tabPageResponse.Controls.Add(labelControlResponseHeaderInfoValue);
			tabPageResponse.Controls.Add(tabControlResponse);
			tabPageResponse.Controls.Add(editorResponsePackageLength);
			tabPageResponse.Controls.Add(labelControlResponsePackageLength);
			tabPageResponse.Name = "tabPageResponse";
			tabPageResponse.Size = new Size(614, 510);
			tabPageResponse.Text = "Response";
			// 
			// packageHeaderControlResponse
			// 
			packageHeaderControlResponse.Location = new Point(206, 5);
			packageHeaderControlResponse.Name = "packageHeaderControlResponse";
			packageHeaderControlResponse.Size = new Size(404, 79);
			packageHeaderControlResponse.TabIndex = 34;
			// 
			// editorResponsePackageHeaderValue
			// 
			editorResponsePackageHeaderValue.ImeMode = ImeMode.KatakanaHalf;
			editorResponsePackageHeaderValue.Location = new Point(104, 39);
			editorResponsePackageHeaderValue.Name = "editorResponsePackageHeaderValue";
			editorResponsePackageHeaderValue.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorResponsePackageHeaderValue.Properties.Appearance.Options.UseFont = true;
			editorResponsePackageHeaderValue.Properties.ReadOnly = true;
			editorResponsePackageHeaderValue.Size = new Size(96, 20);
			editorResponsePackageHeaderValue.TabIndex = 42;
			// 
			// labelControlResponseHeaderInfoValue
			// 
			labelControlResponseHeaderInfoValue.Location = new Point(10, 42);
			labelControlResponseHeaderInfoValue.Name = "labelControlResponseHeaderInfoValue";
			labelControlResponseHeaderInfoValue.Size = new Size(91, 13);
			labelControlResponseHeaderInfoValue.TabIndex = 41;
			labelControlResponseHeaderInfoValue.Text = "Header Info Value:";
			// 
			// tabControlResponse
			// 
			tabControlResponse.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			tabControlResponse.Location = new Point(3, 66);
			tabControlResponse.Name = "tabControlResponse";
			tabControlResponse.SelectedTabPage = tabPageResponseDetails;
			tabControlResponse.Size = new Size(608, 441);
			tabControlResponse.TabIndex = 37;
			tabControlResponse.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { tabPageResponseDetails, tabPageResponsePackageBinaries });
			// 
			// tabPageResponseDetails
			// 
			tabPageResponseDetails.Name = "tabPageResponseDetails";
			tabPageResponseDetails.Size = new Size(606, 416);
			tabPageResponseDetails.Text = "Body Details";
			// 
			// tabPageResponsePackageBinaries
			// 
			tabPageResponsePackageBinaries.Controls.Add(editorResponsePackageValue);
			tabPageResponsePackageBinaries.Name = "tabPageResponsePackageBinaries";
			tabPageResponsePackageBinaries.Size = new Size(606, 416);
			tabPageResponsePackageBinaries.Text = "Package Binaries";
			// 
			// editorResponsePackageValue
			// 
			editorResponsePackageValue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			errorProvider.SetIconAlignment(editorResponsePackageValue, ErrorIconAlignment.MiddleRight);
			editorResponsePackageValue.Location = new Point(3, 3);
			editorResponsePackageValue.Name = "editorResponsePackageValue";
			editorResponsePackageValue.Properties.MaxLength = 2000;
			editorResponsePackageValue.Properties.ReadOnly = true;
			editorResponsePackageValue.Size = new Size(600, 410);
			editorResponsePackageValue.TabIndex = 36;
			// 
			// editorResponsePackageLength
			// 
			editorResponsePackageLength.ImeMode = ImeMode.KatakanaHalf;
			editorResponsePackageLength.Location = new Point(92, 13);
			editorResponsePackageLength.Name = "editorResponsePackageLength";
			editorResponsePackageLength.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorResponsePackageLength.Properties.Appearance.Options.UseFont = true;
			editorResponsePackageLength.Properties.ReadOnly = true;
			editorResponsePackageLength.Size = new Size(108, 20);
			editorResponsePackageLength.TabIndex = 33;
			// 
			// labelControlResponsePackageLength
			// 
			labelControlResponsePackageLength.Location = new Point(10, 16);
			labelControlResponsePackageLength.Name = "labelControlResponsePackageLength";
			labelControlResponsePackageLength.Size = new Size(80, 13);
			labelControlResponsePackageLength.TabIndex = 32;
			labelControlResponsePackageLength.Text = "Package Length:";
			// 
			// EditPanelSessionPackageRequestResponse
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			Name = "EditPanelSessionPackageRequestResponse";
			Size = new Size(630, 634);
			((System.ComponentModel.ISupportInitialize)editorUser.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorMessageCodeOrRequestId.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorActionType.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorSessionKey.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)tabControl).EndInit();
			tabControl.ResumeLayout(false);
			tabPageObjectName.ResumeLayout(false);
			tabPageObjectName.PerformLayout();
			((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
			((System.ComponentModel.ISupportInitialize)tabControlRequestResponse).EndInit();
			tabControlRequestResponse.ResumeLayout(false);
			tabPageRequest.ResumeLayout(false);
			tabPageRequest.PerformLayout();
			((System.ComponentModel.ISupportInitialize)editorRequestPackageHeaderValue.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)tabControlRequest).EndInit();
			tabControlRequest.ResumeLayout(false);
			tabPageRequestPackageBinaries.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)editorRequestPackageValue.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorRequestPackageLength.Properties).EndInit();
			tabPageResponse.ResumeLayout(false);
			tabPageResponse.PerformLayout();
			((System.ComponentModel.ISupportInitialize)editorResponsePackageHeaderValue.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)tabControlResponse).EndInit();
			tabControlResponse.ResumeLayout(false);
			tabPageResponsePackageBinaries.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)editorResponsePackageValue.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorResponsePackageLength.Properties).EndInit();
			ResumeLayout(false);
		}

		#endregion

		protected DevExpress.XtraTab.XtraTabControl tabControlRequestResponse;
		protected DevExpress.XtraTab.XtraTabPage tabPageRequest;
		protected DevExpress.XtraTab.XtraTabPage tabPageResponse;
		protected DevExpress.XtraEditors.MemoEdit editorRequestPackageValue;
		protected PackageHeaderInfoControl packageControlRequestHeader;
		protected DevExpress.XtraEditors.TextEdit editorRequestPackageLength;
		protected DevExpress.XtraEditors.LabelControl labelControlRequestPackageLength;
		protected DevExpress.XtraEditors.MemoEdit editorResponsePackageValue;
		protected PackageHeaderInfoControl packageHeaderControlResponse;
		protected DevExpress.XtraEditors.TextEdit editorResponsePackageLength;
		protected DevExpress.XtraEditors.LabelControl labelControlResponsePackageLength;
		protected DevExpress.XtraTab.XtraTabControl tabControlResponse;
		protected DevExpress.XtraTab.XtraTabPage tabPageResponseDetails;
		protected DevExpress.XtraTab.XtraTabPage tabPageResponsePackageBinaries;
		protected DevExpress.XtraTab.XtraTabControl tabControlRequest;
		protected DevExpress.XtraTab.XtraTabPage tabPageRequestDetails;
		protected DevExpress.XtraTab.XtraTabPage tabPageRequestPackageBinaries;
		protected DevExpress.XtraEditors.TextEdit editorRequestPackageHeaderValue;
		protected DevExpress.XtraEditors.LabelControl labelControlRequestHeaderInfoValue;
		protected DevExpress.XtraEditors.TextEdit editorResponsePackageHeaderValue;
		protected DevExpress.XtraEditors.LabelControl labelControlResponseHeaderInfoValue;
		//protected DevExpress.XtraEditors.TextEdit editorToken;
		//protected DevExpress.XtraEditors.LabelControl labelControlToken;
	}
}
