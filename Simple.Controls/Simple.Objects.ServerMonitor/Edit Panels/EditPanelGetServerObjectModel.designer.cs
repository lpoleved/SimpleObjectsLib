using System.Windows.Forms;
using System.Drawing;

namespace Simple.Objects.ServerMonitor
{
	partial class EditPanelGetServerObjectModel
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
			labelControlTabeId = new DevExpress.XtraEditors.LabelControl();
			editorTableId = new DevExpress.XtraEditors.TextEdit();
			treeListObjectPropertyValues = new Simple.Controls.TreeList.SimpleTreeList();
			labelControlServerPropertyInfoCount = new DevExpress.XtraEditors.LabelControl();
			labelControlServerPropertyInfoCountCaption = new DevExpress.XtraEditors.LabelControl();
			labelControlObjectName = new DevExpress.XtraEditors.LabelControl();
			editorObjectName = new DevExpress.XtraEditors.TextEdit();
			labelControlTableName = new DevExpress.XtraEditors.LabelControl();
			editorTableName = new DevExpress.XtraEditors.TextEdit();
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
			((System.ComponentModel.ISupportInitialize)editorTableId.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)treeListObjectPropertyValues).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorObjectName.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorTableName.Properties).BeginInit();
			SuspendLayout();
			// 
			// tabControlRequestResponse
			// 
			tabControlRequestResponse.Size = new Size(596, 535);
			// 
			// tabPageRequest
			// 
			tabPageRequest.Size = new Size(594, 510);
			// 
			// tabPageResponse
			// 
			tabPageResponse.Size = new Size(594, 510);
			// 
			// editorRequestPackageValue
			// 
			errorProvider.SetIconAlignment(editorRequestPackageValue, ErrorIconAlignment.MiddleRight);
			editorRequestPackageValue.Size = new Size(590, 410);
			// 
			// packageControlRequestHeader
			// 
			packageControlRequestHeader.Size = new Size(394, 79);
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
			editorResponsePackageValue.Size = new Size(590, 410);
			// 
			// packageHeaderControlResponse
			// 
			packageHeaderControlResponse.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			packageHeaderControlResponse.Size = new Size(384, 79);
			// 
			// editorResponsePackageLength
			// 
			editorResponsePackageLength.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorResponsePackageLength.Properties.Appearance.Options.UseFont = true;
			editorResponsePackageLength.Size = new Size(103, 20);
			// 
			// tabControlResponse
			// 
			tabControlResponse.Size = new Size(588, 441);
			// 
			// tabPageResponseDetails
			// 
			tabPageResponseDetails.Controls.Add(labelControlTableName);
			tabPageResponseDetails.Controls.Add(editorTableName);
			tabPageResponseDetails.Controls.Add(labelControlObjectName);
			tabPageResponseDetails.Controls.Add(editorObjectName);
			tabPageResponseDetails.Controls.Add(labelControlServerPropertyInfoCount);
			tabPageResponseDetails.Controls.Add(labelControlServerPropertyInfoCountCaption);
			tabPageResponseDetails.Controls.Add(treeListObjectPropertyValues);
			tabPageResponseDetails.Size = new Size(586, 416);
			// 
			// tabPageResponsePackageBinaries
			// 
			tabPageResponsePackageBinaries.Size = new Size(596, 416);
			// 
			// tabControlRequest
			// 
			tabControlRequest.Size = new Size(588, 441);
			// 
			// tabPageRequestDetails
			// 
			tabPageRequestDetails.Controls.Add(labelControlTabeId);
			tabPageRequestDetails.Controls.Add(editorTableId);
			tabPageRequestDetails.Size = new Size(586, 416);
			// 
			// tabPageRequestPackageBinaries
			// 
			tabPageRequestPackageBinaries.Size = new Size(596, 416);
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
			editorUser.Size = new Size(310, 20);
			// 
			// editorMessageCodeOrRequestId
			// 
			editorMessageCodeOrRequestId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorMessageCodeOrRequestId.Properties.Appearance.Options.UseFont = true;
			editorMessageCodeOrRequestId.Size = new Size(310, 20);
			// 
			// editorActionType
			// 
			editorActionType.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorActionType.Properties.Appearance.Options.UseFont = true;
			// 
			// editorSessionKey
			// 
			editorSessionKey.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorSessionKey.Properties.Appearance.Options.UseFont = true;
			// 
			// labelControlMessageCodeOrRequestId
			// 
			labelControlMessageCodeOrRequestId.Appearance.Options.UseTextOptions = true;
			labelControlMessageCodeOrRequestId.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			// 
			// tabControl
			// 
			tabControl.Size = new Size(614, 628);
			// 
			// tabPageObjectName
			// 
			tabPageObjectName.Size = new Size(612, 603);
			// 
			// labelControlTabeId
			// 
			labelControlTabeId.Location = new Point(9, 24);
			labelControlTabeId.Name = "labelControlTabeId";
			labelControlTabeId.Size = new Size(40, 13);
			labelControlTabeId.TabIndex = 6;
			labelControlTabeId.Text = "TableId:";
			// 
			// editorTableId
			// 
			editorTableId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorTableId.Location = new Point(55, 21);
			editorTableId.Name = "editorTableId";
			editorTableId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorTableId.Properties.Appearance.Options.UseFont = true;
			editorTableId.Properties.ReadOnly = true;
			editorTableId.Size = new Size(521, 20);
			editorTableId.TabIndex = 7;
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
			treeListObjectPropertyValues.Location = new Point(3, 60);
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
			treeListObjectPropertyValues.Size = new Size(580, 353);
			treeListObjectPropertyValues.TabIndex = 3;
			// 
			// labelControlServerPropertyInfoCount
			// 
			labelControlServerPropertyInfoCount.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			labelControlServerPropertyInfoCount.Appearance.Options.UseFont = true;
			labelControlServerPropertyInfoCount.Appearance.Options.UseTextOptions = true;
			labelControlServerPropertyInfoCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelControlServerPropertyInfoCount.Location = new Point(213, 38);
			labelControlServerPropertyInfoCount.Name = "labelControlServerPropertyInfoCount";
			labelControlServerPropertyInfoCount.Size = new Size(98, 13);
			labelControlServerPropertyInfoCount.TabIndex = 47;
			labelControlServerPropertyInfoCount.Text = "(CollectionCount)";
			// 
			// labelControlServerPropertyInfoCountCaption
			// 
			labelControlServerPropertyInfoCountCaption.Appearance.Options.UseTextOptions = true;
			labelControlServerPropertyInfoCountCaption.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelControlServerPropertyInfoCountCaption.Location = new Point(6, 38);
			labelControlServerPropertyInfoCountCaption.Name = "labelControlServerPropertyInfoCountCaption";
			labelControlServerPropertyInfoCountCaption.Size = new Size(202, 13);
			labelControlServerPropertyInfoCountCaption.TabIndex = 46;
			labelControlServerPropertyInfoCountCaption.Text = "Server Object Model Property Info Count:";
			// 
			// labelControlObjectName
			// 
			labelControlObjectName.Location = new Point(5, 15);
			labelControlObjectName.Name = "labelControlObjectName";
			labelControlObjectName.Size = new Size(66, 13);
			labelControlObjectName.TabIndex = 49;
			labelControlObjectName.Text = "Object Name:";
			// 
			// editorObjectName
			// 
			editorObjectName.Location = new Point(77, 12);
			editorObjectName.Name = "editorObjectName";
			editorObjectName.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorObjectName.Properties.Appearance.Options.UseFont = true;
			editorObjectName.Properties.ReadOnly = true;
			editorObjectName.Size = new Size(196, 20);
			editorObjectName.TabIndex = 50;
			// 
			// labelControlTableName
			// 
			labelControlTableName.Location = new Point(283, 15);
			labelControlTableName.Name = "labelControlTableName";
			labelControlTableName.Size = new Size(60, 13);
			labelControlTableName.TabIndex = 51;
			labelControlTableName.Text = "Table Name:";
			// 
			// editorTableName
			// 
			editorTableName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorTableName.Location = new Point(355, 12);
			editorTableName.Name = "editorTableName";
			editorTableName.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			editorTableName.Properties.Appearance.Options.UseFont = true;
			editorTableName.Properties.ReadOnly = true;
			editorTableName.Size = new Size(221, 20);
			editorTableName.TabIndex = 52;
			// 
			// EditPanelGetServerObjectModel
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Name = "EditPanelGetServerObjectModel";
			Size = new Size(620, 634);
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
			((System.ComponentModel.ISupportInitialize)editorTableId.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)treeListObjectPropertyValues).EndInit();
			((System.ComponentModel.ISupportInitialize)editorObjectName.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorTableName.Properties).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private DevExpress.XtraEditors.LabelControl labelControlTabeId;
		private DevExpress.XtraEditors.TextEdit editorTableId;
		private Simple.Controls.TreeList.SimpleTreeList treeListObjectPropertyValues;
		protected DevExpress.XtraEditors.LabelControl labelControlServerPropertyInfoCount;
		protected DevExpress.XtraEditors.LabelControl labelControlServerPropertyInfoCountCaption;
		private DevExpress.XtraEditors.LabelControl labelControlTableName;
		private DevExpress.XtraEditors.TextEdit editorTableName;
		private DevExpress.XtraEditors.LabelControl labelControlObjectName;
		private DevExpress.XtraEditors.TextEdit editorObjectName;
	}
}
