using System.Windows.Forms;
using System.Drawing;

namespace Simple.Objects.ServerMonitor
{
	partial class EditPanelSessionPackageMessage
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
			tabControlMessage = new DevExpress.XtraTab.XtraTabControl();
			tabPageMessage = new DevExpress.XtraTab.XtraTabPage();
			packageHeaderControlMessage = new PackageHeaderInfoControl();
			editorMessagePackageHeaderValue = new DevExpress.XtraEditors.TextEdit();
			labelControlHeaderInfoValue = new DevExpress.XtraEditors.LabelControl();
			tabControlMessageDetails = new DevExpress.XtraTab.XtraTabControl();
			tabPageBodyDetails = new DevExpress.XtraTab.XtraTabPage();
			tabPageMessageBinary = new DevExpress.XtraTab.XtraTabPage();
			editorMessagePackageValue = new DevExpress.XtraEditors.MemoEdit();
			editorMessagePackageLength = new DevExpress.XtraEditors.TextEdit();
			labelControlMessagePackageLength = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)editorUser.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorMessageCodeOrRequestId.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorActionType.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorSessionKey.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)tabControl).BeginInit();
			tabControl.SuspendLayout();
			tabPageObjectName.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
			((System.ComponentModel.ISupportInitialize)tabControlMessage).BeginInit();
			tabControlMessage.SuspendLayout();
			tabPageMessage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)editorMessagePackageHeaderValue.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)tabControlMessageDetails).BeginInit();
			tabControlMessageDetails.SuspendLayout();
			tabPageMessageBinary.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)editorMessagePackageValue.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorMessagePackageLength.Properties).BeginInit();
			SuspendLayout();
			// 
			// editorUser
			// 
			editorUser.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorUser.Properties.Appearance.Options.UseFont = true;
			editorUser.Size = new Size(305, 20);
			// 
			// labelControlUser
			// 
			labelControlUser.Location = new Point(233, 15);
			// 
			// editorMessageCodeOrRequestId
			// 
			editorMessageCodeOrRequestId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorMessageCodeOrRequestId.Properties.Appearance.Options.UseFont = true;
			editorMessageCodeOrRequestId.Size = new Size(305, 20);
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
			// 
			// tabPageObjectName
			// 
			tabPageObjectName.Controls.Add(tabControlMessage);
			tabPageObjectName.Text = "Socket Package Message";
			tabPageObjectName.Controls.SetChildIndex(labelControlSessionKey, 0);
			tabPageObjectName.Controls.SetChildIndex(editorSessionKey, 0);
			tabPageObjectName.Controls.SetChildIndex(labelControlMessageCodeOrRequestId, 0);
			tabPageObjectName.Controls.SetChildIndex(labelControlUser, 0);
			tabPageObjectName.Controls.SetChildIndex(editorUser, 0);
			tabPageObjectName.Controls.SetChildIndex(labelControlPackageJobActionType, 0);
			tabPageObjectName.Controls.SetChildIndex(editorActionType, 0);
			tabPageObjectName.Controls.SetChildIndex(tabControlMessage, 0);
			tabPageObjectName.Controls.SetChildIndex(editorMessageCodeOrRequestId, 0);
			// 
			// tabControlMessage
			// 
			tabControlMessage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			tabControlMessage.Location = new Point(3, 66);
			tabControlMessage.Name = "tabControlMessage";
			tabControlMessage.SelectedTabPage = tabPageMessage;
			tabControlMessage.Size = new Size(570, 535);
			tabControlMessage.TabIndex = 29;
			tabControlMessage.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { tabPageMessage });
			// 
			// tabPageMessage
			// 
			tabPageMessage.Controls.Add(packageHeaderControlMessage);
			tabPageMessage.Controls.Add(editorMessagePackageHeaderValue);
			tabPageMessage.Controls.Add(labelControlHeaderInfoValue);
			tabPageMessage.Controls.Add(tabControlMessageDetails);
			tabPageMessage.Controls.Add(editorMessagePackageLength);
			tabPageMessage.Controls.Add(labelControlMessagePackageLength);
			tabPageMessage.Name = "tabPageMessage";
			tabPageMessage.Size = new Size(568, 510);
			tabPageMessage.Text = "Message";
			// 
			// packageHeaderControlMessage
			// 
			packageHeaderControlMessage.Location = new Point(212, 4);
			packageHeaderControlMessage.Name = "packageHeaderControlMessage";
			packageHeaderControlMessage.Size = new Size(352, 80);
			packageHeaderControlMessage.TabIndex = 29;
			// 
			// editorMessagePackageHeaderValue
			// 
			editorMessagePackageHeaderValue.ImeMode = ImeMode.KatakanaHalf;
			editorMessagePackageHeaderValue.Location = new Point(103, 39);
			editorMessagePackageHeaderValue.Name = "editorMessagePackageHeaderValue";
			editorMessagePackageHeaderValue.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorMessagePackageHeaderValue.Properties.Appearance.Options.UseFont = true;
			editorMessagePackageHeaderValue.Properties.ReadOnly = true;
			editorMessagePackageHeaderValue.Size = new Size(92, 20);
			editorMessagePackageHeaderValue.TabIndex = 42;
			// 
			// labelControlHeaderInfoValue
			// 
			labelControlHeaderInfoValue.Location = new Point(9, 42);
			labelControlHeaderInfoValue.Name = "labelControlHeaderInfoValue";
			labelControlHeaderInfoValue.Size = new Size(91, 13);
			labelControlHeaderInfoValue.TabIndex = 41;
			labelControlHeaderInfoValue.Text = "Header Info Value:";
			// 
			// tabControlMessageDetails
			// 
			tabControlMessageDetails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			tabControlMessageDetails.Location = new Point(3, 66);
			tabControlMessageDetails.Name = "tabControlMessageDetails";
			tabControlMessageDetails.SelectedTabPage = tabPageBodyDetails;
			tabControlMessageDetails.Size = new Size(562, 442);
			tabControlMessageDetails.TabIndex = 32;
			tabControlMessageDetails.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { tabPageBodyDetails, tabPageMessageBinary });
			// 
			// tabPageBodyDetails
			// 
			tabPageBodyDetails.Name = "tabPageBodyDetails";
			tabPageBodyDetails.Size = new Size(560, 417);
			tabPageBodyDetails.Text = "Body Details";
			// 
			// tabPageMessageBinary
			// 
			tabPageMessageBinary.Controls.Add(editorMessagePackageValue);
			tabPageMessageBinary.Name = "tabPageMessageBinary";
			tabPageMessageBinary.Size = new Size(560, 417);
			tabPageMessageBinary.Text = "Package Binaries";
			// 
			// editorMessagePackageValue
			// 
			editorMessagePackageValue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			errorProvider.SetIconAlignment(editorMessagePackageValue, ErrorIconAlignment.MiddleRight);
			editorMessagePackageValue.Location = new Point(3, 3);
			editorMessagePackageValue.Name = "editorMessagePackageValue";
			editorMessagePackageValue.Properties.MaxLength = 2000;
			editorMessagePackageValue.Properties.ReadOnly = true;
			editorMessagePackageValue.Size = new Size(554, 412);
			editorMessagePackageValue.TabIndex = 31;
			// 
			// editorMessagePackageLength
			// 
			editorMessagePackageLength.ImeMode = ImeMode.KatakanaHalf;
			editorMessagePackageLength.Location = new Point(92, 13);
			editorMessagePackageLength.Name = "editorMessagePackageLength";
			editorMessagePackageLength.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorMessagePackageLength.Properties.Appearance.Options.UseFont = true;
			editorMessagePackageLength.Properties.ReadOnly = true;
			editorMessagePackageLength.Size = new Size(103, 20);
			editorMessagePackageLength.TabIndex = 28;
			// 
			// labelControlMessagePackageLength
			// 
			labelControlMessagePackageLength.Location = new Point(9, 16);
			labelControlMessagePackageLength.Name = "labelControlMessagePackageLength";
			labelControlMessagePackageLength.Size = new Size(80, 13);
			labelControlMessagePackageLength.TabIndex = 27;
			labelControlMessagePackageLength.Text = "Package Length:";
			// 
			// EditPanelSessionPackageMessage
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			Name = "EditPanelSessionPackageMessage";
			((System.ComponentModel.ISupportInitialize)editorUser.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorMessageCodeOrRequestId.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorActionType.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorSessionKey.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)tabControl).EndInit();
			tabControl.ResumeLayout(false);
			tabPageObjectName.ResumeLayout(false);
			tabPageObjectName.PerformLayout();
			((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
			((System.ComponentModel.ISupportInitialize)tabControlMessage).EndInit();
			tabControlMessage.ResumeLayout(false);
			tabPageMessage.ResumeLayout(false);
			tabPageMessage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)editorMessagePackageHeaderValue.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)tabControlMessageDetails).EndInit();
			tabControlMessageDetails.ResumeLayout(false);
			tabPageMessageBinary.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)editorMessagePackageValue.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorMessagePackageLength.Properties).EndInit();
			ResumeLayout(false);
		}

		#endregion

		protected DevExpress.XtraTab.XtraTabControl tabControlMessage;
		protected DevExpress.XtraTab.XtraTabPage tabPageMessage;
		protected DevExpress.XtraEditors.TextEdit editorMessagePackageLength;
		protected DevExpress.XtraEditors.LabelControl labelControlMessagePackageLength;
		protected PackageHeaderInfoControl packageHeaderControlMessage;
		protected DevExpress.XtraEditors.MemoEdit editorMessagePackageValue;
		protected DevExpress.XtraTab.XtraTabControl tabControlMessageDetails;
		protected DevExpress.XtraTab.XtraTabPage tabPageBodyDetails;
		protected DevExpress.XtraTab.XtraTabPage tabPageMessageBinary;
		protected DevExpress.XtraEditors.TextEdit editorMessagePackageHeaderValue;
		protected DevExpress.XtraEditors.LabelControl labelControlHeaderInfoValue;
	}
}
