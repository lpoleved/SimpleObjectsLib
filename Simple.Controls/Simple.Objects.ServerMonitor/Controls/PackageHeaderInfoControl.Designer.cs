using System.Windows.Forms;
using System.Drawing;

namespace Simple.Objects.ServerMonitor
{
	partial class PackageHeaderInfoControl
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
			groupControl = new DevExpress.XtraEditors.GroupControl();
			editorValue = new DevExpress.XtraEditors.TextEdit();
			labelValue = new DevExpress.XtraEditors.LabelControl();
			editorRecipientModel = new DevExpress.XtraEditors.TextEdit();
			labelRecipientModel = new DevExpress.XtraEditors.LabelControl();
			editorIsSystem = new DevExpress.XtraEditors.TextEdit();
			editorPackageType = new DevExpress.XtraEditors.TextEdit();
			labelResponseSucceeded = new DevExpress.XtraEditors.LabelControl();
			editorResponseSucceeded = new DevExpress.XtraEditors.TextEdit();
			labelControlIsSystem = new DevExpress.XtraEditors.LabelControl();
			labelPackageType = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)groupControl).BeginInit();
			groupControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)editorValue.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorRecipientModel.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorIsSystem.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorPackageType.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorResponseSucceeded.Properties).BeginInit();
			SuspendLayout();
			// 
			// groupControl
			// 
			groupControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			groupControl.Controls.Add(editorValue);
			groupControl.Controls.Add(labelValue);
			groupControl.Controls.Add(editorRecipientModel);
			groupControl.Controls.Add(labelRecipientModel);
			groupControl.Controls.Add(editorIsSystem);
			groupControl.Controls.Add(editorPackageType);
			groupControl.Controls.Add(labelResponseSucceeded);
			groupControl.Controls.Add(editorResponseSucceeded);
			groupControl.Controls.Add(labelControlIsSystem);
			groupControl.Controls.Add(labelPackageType);
			groupControl.Location = new Point(0, 0);
			groupControl.Name = "groupControl";
			groupControl.Size = new Size(349, 77);
			groupControl.TabIndex = 2;
			groupControl.Text = "Package Header Info";
			// 
			// editorValue
			// 
			editorValue.Enabled = false;
			editorValue.Location = new Point(294, 28);
			editorValue.Name = "editorValue";
			editorValue.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorValue.Properties.Appearance.Options.UseFont = true;
			editorValue.Size = new Size(48, 20);
			editorValue.TabIndex = 19;
			// 
			// labelValue
			// 
			labelValue.Location = new Point(263, 32);
			labelValue.Name = "labelValue";
			labelValue.Size = new Size(30, 13);
			labelValue.TabIndex = 18;
			labelValue.Text = "Value:";
			// 
			// editorRecipientModel
			// 
			editorRecipientModel.Enabled = false;
			editorRecipientModel.Location = new Point(86, 51);
			editorRecipientModel.Name = "editorRecipientModel";
			editorRecipientModel.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorRecipientModel.Properties.Appearance.Options.UseFont = true;
			editorRecipientModel.Size = new Size(108, 20);
			editorRecipientModel.TabIndex = 17;
			// 
			// labelRecipientModel
			// 
			labelRecipientModel.Location = new Point(6, 54);
			labelRecipientModel.Name = "labelRecipientModel";
			labelRecipientModel.Size = new Size(79, 13);
			labelRecipientModel.TabIndex = 16;
			labelRecipientModel.Text = "Recipient Model:";
			// 
			// editorIsSystem
			// 
			editorIsSystem.Enabled = false;
			editorIsSystem.Location = new Point(211, 27);
			editorIsSystem.Name = "editorIsSystem";
			editorIsSystem.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorIsSystem.Properties.Appearance.Options.UseFont = true;
			editorIsSystem.Size = new Size(42, 20);
			editorIsSystem.TabIndex = 15;
			// 
			// editorPackageType
			// 
			editorPackageType.Enabled = false;
			editorPackageType.Location = new Point(86, 27);
			editorPackageType.Name = "editorPackageType";
			editorPackageType.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorPackageType.Properties.Appearance.Options.UseFont = true;
			editorPackageType.Size = new Size(65, 20);
			editorPackageType.TabIndex = 14;
			// 
			// labelResponseSucceeded
			// 
			labelResponseSucceeded.Location = new Point(199, 54);
			labelResponseSucceeded.Name = "labelResponseSucceeded";
			labelResponseSucceeded.Size = new Size(94, 13);
			labelResponseSucceeded.TabIndex = 10;
			labelResponseSucceeded.Text = "Response Succeed:";
			// 
			// editorResponseSucceeded
			// 
			editorResponseSucceeded.Enabled = false;
			editorResponseSucceeded.Location = new Point(294, 51);
			editorResponseSucceeded.Name = "editorResponseSucceeded";
			editorResponseSucceeded.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorResponseSucceeded.Properties.Appearance.Options.UseFont = true;
			editorResponseSucceeded.Size = new Size(48, 20);
			editorResponseSucceeded.TabIndex = 11;
			// 
			// labelControlIsSystem
			// 
			labelControlIsSystem.Location = new Point(157, 32);
			labelControlIsSystem.Name = "labelControlIsSystem";
			labelControlIsSystem.Size = new Size(51, 13);
			labelControlIsSystem.TabIndex = 6;
			labelControlIsSystem.Text = "Is System:";
			// 
			// labelPackageType
			// 
			labelPackageType.Location = new Point(6, 32);
			labelPackageType.Name = "labelPackageType";
			labelPackageType.Size = new Size(71, 13);
			labelPackageType.TabIndex = 2;
			labelPackageType.Text = "Package Type:";
			// 
			// PackageHeaderInfoControl
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(groupControl);
			Name = "PackageHeaderInfoControl";
			Size = new Size(349, 77);
			((System.ComponentModel.ISupportInitialize)groupControl).EndInit();
			groupControl.ResumeLayout(false);
			groupControl.PerformLayout();
			((System.ComponentModel.ISupportInitialize)editorValue.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorRecipientModel.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorIsSystem.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorPackageType.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorResponseSucceeded.Properties).EndInit();
			ResumeLayout(false);
		}

		#endregion

		protected DevExpress.XtraEditors.GroupControl groupControl;
		protected DevExpress.XtraEditors.LabelControl labelPackageType;
		protected DevExpress.XtraEditors.LabelControl labelResponseSucceeded;
		protected DevExpress.XtraEditors.TextEdit editorResponseSucceeded;
		protected DevExpress.XtraEditors.LabelControl labelControlIsSystem;
		protected DevExpress.XtraEditors.TextEdit editorPackageType;
		protected DevExpress.XtraEditors.TextEdit editorIsSystem;
		protected DevExpress.XtraEditors.TextEdit editorRecipientModel;
		protected DevExpress.XtraEditors.LabelControl labelRecipientModel;
		protected DevExpress.XtraEditors.TextEdit editorValue;
		protected DevExpress.XtraEditors.LabelControl labelValue;
	}
}
