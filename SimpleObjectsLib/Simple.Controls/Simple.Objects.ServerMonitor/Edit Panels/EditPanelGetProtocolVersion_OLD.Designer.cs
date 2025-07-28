//using System.Windows.Forms;
//using System.Drawing;

//namespace Simple.Objects.ServerMonitor
//{
//	partial class EditPanelGetProtocolVersion
//	{
//		/// <summary> 
//		/// Required designer variable.
//		/// </summary>
//		private System.ComponentModel.IContainer components = null;

//		/// <summary> 
//		/// Clean up any resources being used.
//		/// </summary>
//		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//		protected override void Dispose(bool disposing)
//		{
//			if (disposing && (components != null))
//			{
//				components.Dispose();
//			}
//			base.Dispose(disposing);
//		}

//		#region Component Designer generated code

//		/// <summary> 
//		/// Required method for Designer support - do not modify 
//		/// the contents of this method with the code editor.
//		/// </summary>
//		private void InitializeComponent()
//		{
//			editorProtocolVersion = new DevExpress.XtraEditors.TextEdit();
//			labelControlProtocolVersion = new DevExpress.XtraEditors.LabelControl();
//			((System.ComponentModel.ISupportInitialize)tabControlRequestResponse).BeginInit();
//			tabControlRequestResponse.SuspendLayout();
//			tabPageRequest.SuspendLayout();
//			tabPageResponse.SuspendLayout();
//			((System.ComponentModel.ISupportInitialize)editorRequestPackageValue.Properties).BeginInit();
//			((System.ComponentModel.ISupportInitialize)editorRequestPackageLength.Properties).BeginInit();
//			((System.ComponentModel.ISupportInitialize)editorResponsePackageValue.Properties).BeginInit();
//			((System.ComponentModel.ISupportInitialize)editorResponsePackageLength.Properties).BeginInit();
//			((System.ComponentModel.ISupportInitialize)tabControlResponse).BeginInit();
//			tabControlResponse.SuspendLayout();
//			tabPageResponseDetails.SuspendLayout();
//			tabPageResponsePackageBinaries.SuspendLayout();
//			((System.ComponentModel.ISupportInitialize)tabControlRequest).BeginInit();
//			tabControlRequest.SuspendLayout();
//			tabPageRequestPackageBinaries.SuspendLayout();
//			((System.ComponentModel.ISupportInitialize)editorRequestPackageHeaderValue.Properties).BeginInit();
//			((System.ComponentModel.ISupportInitialize)editorResponsePackageHeaderValue.Properties).BeginInit();
//			((System.ComponentModel.ISupportInitialize)editorUser.Properties).BeginInit();
//			((System.ComponentModel.ISupportInitialize)editorMessageCodeOrRequestId.Properties).BeginInit();
//			((System.ComponentModel.ISupportInitialize)editorActionType.Properties).BeginInit();
//			((System.ComponentModel.ISupportInitialize)editorSessionKey.Properties).BeginInit();
//			((System.ComponentModel.ISupportInitialize)tabControl).BeginInit();
//			tabControl.SuspendLayout();
//			tabPageObjectName.SuspendLayout();
//			((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
//			((System.ComponentModel.ISupportInitialize)editorProtocolVersion.Properties).BeginInit();
//			SuspendLayout();
//			// 
//			// tabControlRequestResponse
//			// 
//			tabControlRequestResponse.Size = new Size(597, 535);
//			// 
//			// tabPageRequest
//			// 
//			tabPageRequest.Size = new Size(595, 510);
//			// 
//			// tabPageResponse
//			// 
//			tabPageResponse.Size = new Size(595, 510);
//			// 
//			// editorRequestPackageValue
//			// 
//			errorProvider.SetIconAlignment(editorRequestPackageValue, ErrorIconAlignment.MiddleRight);
//			editorRequestPackageValue.Size = new Size(581, 410);
//			// 
//			// packageControlRequestHeader
//			// 
//			packageControlRequestHeader.Size = new Size(405, 79);
//			// 
//			// editorRequestPackageLength
//			// 
//			editorRequestPackageLength.Size = new Size(103, 20);
//			// 
//			// editorResponsePackageValue
//			// 
//			errorProvider.SetIconAlignment(editorResponsePackageValue, ErrorIconAlignment.MiddleRight);
//			editorResponsePackageValue.Size = new Size(581, 410);
//			// 
//			// packageHeaderControlResponse
//			// 
//			packageHeaderControlResponse.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
//			packageHeaderControlResponse.Size = new Size(385, 79);
//			// 
//			// editorResponsePackageLength
//			// 
//			editorResponsePackageLength.Size = new Size(103, 20);
//			// 
//			// tabControlResponse
//			// 
//			tabControlResponse.Size = new Size(589, 441);
//			// 
//			// tabPageResponseDetails
//			// 
//			tabPageResponseDetails.Controls.Add(editorProtocolVersion);
//			tabPageResponseDetails.Controls.Add(labelControlProtocolVersion);
//			tabPageResponseDetails.Size = new Size(587, 416);
//			// 
//			// tabPageResponsePackageBinaries
//			// 
//			tabPageResponsePackageBinaries.Size = new Size(587, 416);
//			// 
//			// tabControlRequest
//			// 
//			tabControlRequest.Size = new Size(589, 441);
//			// 
//			// tabPageRequestDetails
//			// 
//			tabPageRequestDetails.Size = new Size(587, 416);
//			// 
//			// tabPageRequestPackageBinaries
//			// 
//			tabPageRequestPackageBinaries.Size = new Size(587, 416);
//			// 
//			// editorRequestPackageHeaderValue
//			// 
//			editorRequestPackageHeaderValue.Size = new Size(91, 20);
//			// 
//			// editorResponsePackageHeaderValue
//			// 
//			editorResponsePackageHeaderValue.Size = new Size(91, 20);
//			// 
//			// editorUser
//			// 
//			editorUser.Properties.Appearance.Options.UseFont = true;
//			editorUser.Size = new Size(313, 20);
//			// 
//			// editorMessageCodeOrRequestId
//			// 
//			editorMessageCodeOrRequestId.Properties.Appearance.Options.UseFont = true;
//			editorMessageCodeOrRequestId.Size = new Size(313, 20);
//			// 
//			// editorActionType
//			// 
//			editorActionType.Properties.Appearance.Options.UseFont = true;
//			editorActionType.Size = new Size(107, 20);
//			// 
//			// editorSessionKey
//			// 
//			editorSessionKey.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
//			editorSessionKey.Properties.Appearance.Options.UseFont = true;
//			editorSessionKey.Size = new Size(107, 20);
//			// 
//			// labelControlMessageCodeOrRequestId
//			// 
//			labelControlMessageCodeOrRequestId.Appearance.Options.UseTextOptions = true;
//			labelControlMessageCodeOrRequestId.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
//			// 
//			// tabControl
//			// 
//			tabControl.Size = new Size(605, 628);
//			// 
//			// tabPageObjectName
//			// 
//			tabPageObjectName.Size = new Size(603, 603);
//			// 
//			// editorProtocolVersion
//			// 
//			editorProtocolVersion.Enabled = false;
//			editorProtocolVersion.Location = new Point(93, 21);
//			editorProtocolVersion.Name = "editorProtocolVersion";
//			editorProtocolVersion.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
//			editorProtocolVersion.Properties.Appearance.Options.UseFont = true;
//			editorProtocolVersion.Properties.ReadOnly = true;
//			editorProtocolVersion.Size = new Size(98, 20);
//			editorProtocolVersion.TabIndex = 3;
//			// 
//			// labelControlProtocolVersion
//			// 
//			labelControlProtocolVersion.Location = new Point(9, 24);
//			labelControlProtocolVersion.Name = "labelControlProtocolVersion";
//			labelControlProtocolVersion.Size = new Size(81, 13);
//			labelControlProtocolVersion.TabIndex = 2;
//			labelControlProtocolVersion.Text = "Protocol Version:";
//			// 
//			// EditPanelGetProtocolVersion
//			// 
//			AutoScaleDimensions = new SizeF(6F, 13F);
//			AutoScaleMode = AutoScaleMode.Font;
//			Name = "EditPanelGetProtocolVersion";
//			Size = new Size(611, 634);
//			((System.ComponentModel.ISupportInitialize)tabControlRequestResponse).EndInit();
//			tabControlRequestResponse.ResumeLayout(false);
//			tabPageRequest.ResumeLayout(false);
//			tabPageRequest.PerformLayout();
//			tabPageResponse.ResumeLayout(false);
//			tabPageResponse.PerformLayout();
//			((System.ComponentModel.ISupportInitialize)editorRequestPackageValue.Properties).EndInit();
//			((System.ComponentModel.ISupportInitialize)editorRequestPackageLength.Properties).EndInit();
//			((System.ComponentModel.ISupportInitialize)editorResponsePackageValue.Properties).EndInit();
//			((System.ComponentModel.ISupportInitialize)editorResponsePackageLength.Properties).EndInit();
//			((System.ComponentModel.ISupportInitialize)tabControlResponse).EndInit();
//			tabControlResponse.ResumeLayout(false);
//			tabPageResponseDetails.ResumeLayout(false);
//			tabPageResponseDetails.PerformLayout();
//			tabPageResponsePackageBinaries.ResumeLayout(false);
//			((System.ComponentModel.ISupportInitialize)tabControlRequest).EndInit();
//			tabControlRequest.ResumeLayout(false);
//			tabPageRequestPackageBinaries.ResumeLayout(false);
//			((System.ComponentModel.ISupportInitialize)editorRequestPackageHeaderValue.Properties).EndInit();
//			((System.ComponentModel.ISupportInitialize)editorResponsePackageHeaderValue.Properties).EndInit();
//			((System.ComponentModel.ISupportInitialize)editorUser.Properties).EndInit();
//			((System.ComponentModel.ISupportInitialize)editorMessageCodeOrRequestId.Properties).EndInit();
//			((System.ComponentModel.ISupportInitialize)editorActionType.Properties).EndInit();
//			((System.ComponentModel.ISupportInitialize)editorSessionKey.Properties).EndInit();
//			((System.ComponentModel.ISupportInitialize)tabControl).EndInit();
//			tabControl.ResumeLayout(false);
//			tabPageObjectName.ResumeLayout(false);
//			tabPageObjectName.PerformLayout();
//			((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
//			((System.ComponentModel.ISupportInitialize)editorProtocolVersion.Properties).EndInit();
//			ResumeLayout(false);
//		}

//		#endregion

//		private DevExpress.XtraEditors.TextEdit editorProtocolVersion;
//		private DevExpress.XtraEditors.LabelControl labelControlProtocolVersion;
//	}
//}
