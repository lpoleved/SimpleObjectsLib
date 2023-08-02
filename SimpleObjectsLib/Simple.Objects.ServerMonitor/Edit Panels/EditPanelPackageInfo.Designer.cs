using System.Windows.Forms;
using System.Drawing;

namespace Simple.Objects.ServerMonitor
{
	partial class EditPanelPackageInfo
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
			editorUser = new DevExpress.XtraEditors.TextEdit();
			labelControlUser = new DevExpress.XtraEditors.LabelControl();
			editorMessageCodeOrRequestId = new DevExpress.XtraEditors.TextEdit();
			editorActionType = new DevExpress.XtraEditors.TextEdit();
			labelControlPackageJobActionType = new DevExpress.XtraEditors.LabelControl();
			labelControlSessionKey = new DevExpress.XtraEditors.LabelControl();
			editorSessionKey = new DevExpress.XtraEditors.TextEdit();
			labelControlMessageCodeOrRequestId = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)tabControl).BeginInit();
			tabControl.SuspendLayout();
			tabPageObjectName.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorUser.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorMessageCodeOrRequestId.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorActionType.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorSessionKey.Properties).BeginInit();
			SuspendLayout();
			// 
			// tabPageObjectName
			// 
			tabPageObjectName.Controls.Add(labelControlMessageCodeOrRequestId);
			tabPageObjectName.Controls.Add(editorSessionKey);
			tabPageObjectName.Controls.Add(labelControlSessionKey);
			tabPageObjectName.Controls.Add(editorActionType);
			tabPageObjectName.Controls.Add(labelControlPackageJobActionType);
			tabPageObjectName.Controls.Add(editorMessageCodeOrRequestId);
			tabPageObjectName.Controls.Add(editorUser);
			tabPageObjectName.Controls.Add(labelControlUser);
			// 
			// editorUser
			// 
			editorUser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorUser.ImeMode = ImeMode.KatakanaHalf;
			editorUser.Location = new Point(263, 12);
			editorUser.Name = "editorUser";
			editorUser.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorUser.Properties.Appearance.Options.UseFont = true;
			editorUser.Properties.ReadOnly = true;
			editorUser.Size = new Size(303, 20);
			editorUser.TabIndex = 20;
			// 
			// labelControlUser
			// 
			labelControlUser.Location = new Point(231, 15);
			labelControlUser.Name = "labelControlUser";
			labelControlUser.Size = new Size(26, 13);
			labelControlUser.TabIndex = 19;
			labelControlUser.Text = "User:";
			// 
			// editorMessageCodeOrRequestId
			// 
			editorMessageCodeOrRequestId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorMessageCodeOrRequestId.ImeMode = ImeMode.KatakanaHalf;
			editorMessageCodeOrRequestId.Location = new Point(263, 36);
			editorMessageCodeOrRequestId.Name = "editorMessageCodeOrRequestId";
			editorMessageCodeOrRequestId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorMessageCodeOrRequestId.Properties.Appearance.Options.UseFont = true;
			editorMessageCodeOrRequestId.Properties.ReadOnly = true;
			editorMessageCodeOrRequestId.Size = new Size(303, 20);
			editorMessageCodeOrRequestId.TabIndex = 24;
			// 
			// editorActionType
			// 
			editorActionType.ImeMode = ImeMode.KatakanaHalf;
			editorActionType.Location = new Point(77, 36);
			editorActionType.Name = "editorActionType";
			editorActionType.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorActionType.Properties.Appearance.Options.UseFont = true;
			editorActionType.Properties.ReadOnly = true;
			editorActionType.Size = new Size(101, 20);
			editorActionType.TabIndex = 28;
			// 
			// labelControlPackageJobActionType
			// 
			labelControlPackageJobActionType.Location = new Point(11, 39);
			labelControlPackageJobActionType.Name = "labelControlPackageJobActionType";
			labelControlPackageJobActionType.Size = new Size(61, 13);
			labelControlPackageJobActionType.TabIndex = 27;
			labelControlPackageJobActionType.Text = "Action Type:";
			// 
			// labelControlSessionKey
			// 
			labelControlSessionKey.Location = new Point(11, 15);
			labelControlSessionKey.Name = "labelControlSessionKey";
			labelControlSessionKey.Size = new Size(61, 13);
			labelControlSessionKey.TabIndex = 29;
			labelControlSessionKey.Text = "Session Key:";
			// 
			// editorSessionKey
			// 
			editorSessionKey.ImeMode = ImeMode.KatakanaHalf;
			editorSessionKey.Location = new Point(77, 12);
			editorSessionKey.Name = "editorSessionKey";
			editorSessionKey.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorSessionKey.Properties.Appearance.Options.UseFont = true;
			editorSessionKey.Properties.ReadOnly = true;
			editorSessionKey.Size = new Size(101, 20);
			editorSessionKey.TabIndex = 30;
			// 
			// labelControlMessageCodeOrRequestId
			// 
			labelControlMessageCodeOrRequestId.Appearance.Options.UseTextOptions = true;
			labelControlMessageCodeOrRequestId.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelControlMessageCodeOrRequestId.Location = new Point(185, 41);
			labelControlMessageCodeOrRequestId.Name = "labelControlMessageCodeOrRequestId";
			labelControlMessageCodeOrRequestId.Size = new Size(74, 13);
			labelControlMessageCodeOrRequestId.TabIndex = 31;
			labelControlMessageCodeOrRequestId.Text = "Message Code:";
			// 
			// EditPanelPackageInfo
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			Name = "EditPanelPackageInfo";
			((System.ComponentModel.ISupportInitialize)tabControl).EndInit();
			tabControl.ResumeLayout(false);
			tabPageObjectName.ResumeLayout(false);
			tabPageObjectName.PerformLayout();
			((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
			((System.ComponentModel.ISupportInitialize)editorUser.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorMessageCodeOrRequestId.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorActionType.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorSessionKey.Properties).EndInit();
			ResumeLayout(false);
		}

		#endregion

		protected DevExpress.XtraEditors.TextEdit editorUser;
		protected DevExpress.XtraEditors.LabelControl labelControlUser;
		protected DevExpress.XtraEditors.TextEdit editorMessageCodeOrRequestId;
		protected DevExpress.XtraEditors.TextEdit editorActionType;
		protected DevExpress.XtraEditors.LabelControl labelControlPackageJobActionType;
		protected DevExpress.XtraEditors.TextEdit editorSessionKey;
		protected DevExpress.XtraEditors.LabelControl labelControlSessionKey;
		protected DevExpress.XtraEditors.LabelControl labelControlMessageCodeOrRequestId;
	}
}
