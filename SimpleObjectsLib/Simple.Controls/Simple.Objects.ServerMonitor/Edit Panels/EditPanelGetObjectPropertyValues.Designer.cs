using System.Windows.Forms;
using System.Drawing;

namespace Simple.Objects.ServerMonitor
{
	partial class EditPanelGetObjectPropertyValues
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
			editorObjectKey = new DevExpress.XtraEditors.TextEdit();
			treeListObjectPropertyValues = new Simple.Controls.TreeList.SimpleTreeList();
			labelControlObjectIdInfo = new DevExpress.XtraEditors.LabelControl();
			labelControlTableId = new DevExpress.XtraEditors.LabelControl();
			editorTableId = new DevExpress.XtraEditors.TextEdit();
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
			((System.ComponentModel.ISupportInitialize)editorObjectKey.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)treeListObjectPropertyValues).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorTableId.Properties).BeginInit();
			SuspendLayout();
			// 
			// tabControlRequestResponse
			// 
			tabControlRequestResponse.Size = new Size(593, 535);
			// 
			// tabPageRequest
			// 
			tabPageRequest.Size = new Size(591, 510);
			// 
			// tabPageResponse
			// 
			tabPageResponse.Size = new Size(591, 510);
			// 
			// editorRequestPackageValue
			// 
			errorProvider.SetIconAlignment(editorRequestPackageValue, ErrorIconAlignment.MiddleRight);
			editorRequestPackageValue.Size = new Size(577, 410);
			// 
			// packageControlRequestHeader
			// 
			packageControlRequestHeader.Size = new Size(381, 79);
			// 
			// editorRequestPackageLength
			// 
			editorRequestPackageLength.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
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
			packageHeaderControlResponse.Size = new Size(381, 79);
			// 
			// editorResponsePackageLength
			// 
			// 
			// labelControlResponsePackageLength
			// 
			labelControlResponsePackageLength.Location = new Point(8, 16);
			// 
			// tabControlResponse
			// 
			tabControlResponse.Size = new Size(585, 441);
			// 
			// tabPageResponseDetails
			// 
			tabPageResponseDetails.Controls.Add(labelControlTableId);
			tabPageResponseDetails.Controls.Add(editorTableId);
			tabPageResponseDetails.Controls.Add(treeListObjectPropertyValues);
			tabPageResponseDetails.Size = new Size(583, 416);
			// 
			// tabPageResponsePackageBinaries
			// 
			tabPageResponsePackageBinaries.Size = new Size(583, 416);
			// 
			// tabControlRequest
			// 
			tabControlRequest.Size = new Size(585, 441);
			// 
			// tabPageRequestDetails
			// 
			tabPageRequestDetails.Controls.Add(labelControlObjectIdInfo);
			tabPageRequestDetails.Controls.Add(labelControlObjectKey);
			tabPageRequestDetails.Controls.Add(editorObjectKey);
			tabPageRequestDetails.Size = new Size(583, 416);
			// 
			// tabPageRequestPackageBinaries
			// 
			tabPageRequestPackageBinaries.Size = new Size(583, 416);
			// 
			// editorRequestPackageHeaderValue
			// 
			editorRequestPackageHeaderValue.Location = new Point(101, 39);
			editorRequestPackageHeaderValue.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
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
			editorResponsePackageHeaderValue.Size = new Size(99, 20);
			// 
			// labelControlResponseHeaderInfoValue
			// 
			labelControlResponseHeaderInfoValue.Location = new Point(8, 42);
			// 
			// editorUser
			// 
			editorUser.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorUser.Properties.Appearance.Options.UseFont = true;
			editorUser.Size = new Size(205, 20);
			// 
			// editorMessageCodeOrRequestId
			// 
			editorMessageCodeOrRequestId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorMessageCodeOrRequestId.Properties.Appearance.Options.UseFont = true;
			editorMessageCodeOrRequestId.Size = new Size(205, 20);
			// 
			// editorActionType
			// 
			editorActionType.Location = new Point(76, 35);
			editorActionType.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorActionType.Properties.Appearance.Options.UseFont = true;
			editorActionType.Size = new Size(116, 20);
			// 
			// labelControlPackageJobActionType
			// 
			labelControlPackageJobActionType.Location = new Point(12, 38);
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
			tabControl.Size = new Size(601, 628);
			// 
			// tabPageObjectName
			// 
			tabPageObjectName.Size = new Size(599, 603);
			// 
			// labelControlObjectKey
			// 
			labelControlObjectKey.Location = new Point(9, 24);
			labelControlObjectKey.Name = "labelControlObjectKey";
			labelControlObjectKey.Size = new Size(57, 13);
			labelControlObjectKey.TabIndex = 6;
			labelControlObjectKey.Text = "Object Key:";
			// 
			// editorObjectKey
			// 
			editorObjectKey.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorObjectKey.Location = new Point(72, 21);
			editorObjectKey.Name = "editorObjectKey";
			editorObjectKey.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorObjectKey.Properties.Appearance.Options.UseFont = true;
			editorObjectKey.Properties.ReadOnly = true;
			editorObjectKey.Size = new Size(498, 20);
			editorObjectKey.TabIndex = 7;
			// 
			// treeListObjectPropertyValues
			// 
			treeListObjectPropertyValues.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			treeListObjectPropertyValues.Appearance.FocusedCell.BackColor = Color.FromArgb(255, 255, 255);
			treeListObjectPropertyValues.Appearance.FocusedCell.ForeColor = SystemColors.ControlText;
			treeListObjectPropertyValues.Appearance.FocusedRow.ForeColor = SystemColors.ControlText;
			treeListObjectPropertyValues.Appearance.FocusedRow.Options.UseBackColor = true;
			treeListObjectPropertyValues.Appearance.HideSelectionRow.ForeColor = Color.Black;
			treeListObjectPropertyValues.Appearance.HideSelectionRow.Options.UseBackColor = true;
			treeListObjectPropertyValues.Appearance.SelectedRow.Options.UseForeColor = true;
			treeListObjectPropertyValues.Location = new Point(3, 46);
			treeListObjectPropertyValues.Name = "treeListObjectPropertyValues";
			treeListObjectPropertyValues.OptionsBehavior.Editable = false;
			treeListObjectPropertyValues.OptionsDragAndDrop.DropNodesMode = DevExpress.XtraTreeList.DropNodesMode.Advanced;
			treeListObjectPropertyValues.OptionsSelection.EnableAppearanceFocusedCell = false;
			treeListObjectPropertyValues.OptionsSelection.EnableAppearanceFocusedRow = false;
			treeListObjectPropertyValues.OptionsSimple.EditMode = Simple.Controls.TreeList.SimpleTreeEditMode.ViewOnly;
			treeListObjectPropertyValues.OptionsSimple.LookAndFeelStyle = Simple.Controls.TreeList.SimpleTreeLookAndFeelStyle.ExcelStyle;
			treeListObjectPropertyValues.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
			treeListObjectPropertyValues.OptionsView.ShowVertLines = false;
			treeListObjectPropertyValues.OptionsView.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None;
			treeListObjectPropertyValues.Size = new Size(577, 367);
			treeListObjectPropertyValues.TabIndex = 2;
			// 
			// labelControlObjectIdInfo
			// 
			labelControlObjectIdInfo.Location = new Point(72, 47);
			labelControlObjectIdInfo.Name = "labelControlObjectIdInfo";
			labelControlObjectIdInfo.Size = new Size(82, 13);
			labelControlObjectIdInfo.TabIndex = 8;
			labelControlObjectIdInfo.Text = "TableId.ObjectId";
			// 
			// labelControlTableId
			// 
			labelControlTableId.Location = new Point(4, 23);
			labelControlTableId.Name = "labelControlTableId";
			labelControlTableId.Size = new Size(40, 13);
			labelControlTableId.TabIndex = 8;
			labelControlTableId.Text = "TableId:";
			// 
			// editorTableId
			// 
			editorTableId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorTableId.Location = new Point(50, 20);
			editorTableId.Name = "editorTableId";
			editorTableId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorTableId.Properties.Appearance.Options.UseFont = true;
			editorTableId.Properties.ReadOnly = true;
			editorTableId.Size = new Size(516, 20);
			editorTableId.TabIndex = 9;
			// 
			// EditPanelGetObjectPropertyValues
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Name = "EditPanelGetObjectPropertyValues";
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
			((System.ComponentModel.ISupportInitialize)editorObjectKey.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)treeListObjectPropertyValues).EndInit();
			((System.ComponentModel.ISupportInitialize)editorTableId.Properties).EndInit();
			ResumeLayout(false);
		}

		#endregion

		protected DevExpress.XtraEditors.LabelControl labelControlObjectKey;
		protected DevExpress.XtraEditors.TextEdit editorObjectKey;
		protected Simple.Controls.TreeList.SimpleTreeList treeListObjectPropertyValues;
		protected DevExpress.XtraEditors.LabelControl labelControlObjectIdInfo;
		protected DevExpress.XtraEditors.LabelControl labelControlTableId;
		protected DevExpress.XtraEditors.TextEdit editorTableId;
	}
}
