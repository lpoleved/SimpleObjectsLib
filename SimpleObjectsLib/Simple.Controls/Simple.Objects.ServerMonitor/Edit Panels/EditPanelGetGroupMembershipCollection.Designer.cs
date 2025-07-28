using System.Windows.Forms;
using System.Drawing;

namespace Simple.Objects.ServerMonitor
{
	partial class EditPanelGetGroupMembershipCollection
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
			editorTableId = new DevExpress.XtraEditors.TextEdit();
			labelControlTabeId = new DevExpress.XtraEditors.LabelControl();
			treeListObjectIds = new Simple.Controls.TreeList.SimpleTreeList();
			labelControlObjectId = new DevExpress.XtraEditors.LabelControl();
			editorObjectId = new DevExpress.XtraEditors.TextEdit();
			labelControlRelationKey = new DevExpress.XtraEditors.LabelControl();
			editorRelationKey = new DevExpress.XtraEditors.TextEdit();
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
			((System.ComponentModel.ISupportInitialize)treeListObjectIds).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorObjectId.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorRelationKey.Properties).BeginInit();
			SuspendLayout();
			// 
			// tabControlRequestResponse
			// 
			tabControlRequestResponse.Size = new Size(595, 535);
			// 
			// tabPageRequest
			// 
			tabPageRequest.Size = new Size(593, 510);
			// 
			// tabPageResponse
			// 
			tabPageResponse.Size = new Size(593, 510);
			// 
			// editorRequestPackageValue
			// 
			errorProvider.SetIconAlignment(editorRequestPackageValue, ErrorIconAlignment.MiddleRight);
			editorRequestPackageValue.Size = new Size(578, 403);
			// 
			// packageControlRequestHeader
			// 
			packageControlRequestHeader.Size = new Size(447, 79);
			// 
			// editorRequestPackageLength
			// 
			editorRequestPackageLength.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorRequestPackageLength.Properties.Appearance.Options.UseFont = true;
			// 
			// editorResponsePackageValue
			// 
			errorProvider.SetIconAlignment(editorResponsePackageValue, ErrorIconAlignment.MiddleRight);
			editorResponsePackageValue.Size = new Size(578, 410);
			// 
			// packageHeaderControlResponse
			// 
			packageHeaderControlResponse.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			packageHeaderControlResponse.Size = new Size(383, 79);
			// 
			// editorResponsePackageLength
			// 
			// 
			// tabControlResponse
			// 
			tabControlResponse.Size = new Size(587, 441);
			// 
			// tabPageResponseDetails
			// 
			tabPageResponseDetails.Controls.Add(treeListObjectIds);
			tabPageResponseDetails.Size = new Size(585, 416);
			// 
			// tabPageResponsePackageBinaries
			// 
			tabPageResponsePackageBinaries.Size = new Size(564, 416);
			// 
			// tabControlRequest
			// 
			tabControlRequest.Size = new Size(587, 441);
			// 
			// tabPageRequestDetails
			// 
			tabPageRequestDetails.Controls.Add(labelControlRelationKey);
			tabPageRequestDetails.Controls.Add(editorRelationKey);
			tabPageRequestDetails.Controls.Add(labelControlObjectId);
			tabPageRequestDetails.Controls.Add(editorObjectId);
			tabPageRequestDetails.Controls.Add(labelControlTabeId);
			tabPageRequestDetails.Controls.Add(editorTableId);
			tabPageRequestDetails.Size = new Size(585, 416);
			// 
			// tabPageRequestPackageBinaries
			// 
			tabPageRequestPackageBinaries.Size = new Size(564, 416);
			// 
			// editorRequestPackageHeaderValue
			// 
			editorRequestPackageHeaderValue.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorRequestPackageHeaderValue.Properties.Appearance.Options.UseFont = true;
			// 
			// editorResponsePackageHeaderValue
			// 
			// 
			// editorUser
			// 
			editorUser.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorUser.Properties.Appearance.Options.UseFont = true;
			editorUser.Size = new Size(267, 20);
			// 
			// editorMessageCodeOrRequestId
			// 
			editorMessageCodeOrRequestId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorMessageCodeOrRequestId.Properties.Appearance.Options.UseFont = true;
			editorMessageCodeOrRequestId.Size = new Size(267, 20);
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
			tabControl.Size = new Size(603, 628);
			// 
			// tabPageObjectName
			// 
			tabPageObjectName.Size = new Size(601, 603);
			// 
			// editorTableId
			// 
			editorTableId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorTableId.Location = new Point(77, 21);
			editorTableId.Name = "editorTableId";
			editorTableId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorTableId.Properties.Appearance.Options.UseFont = true;
			editorTableId.Properties.ReadOnly = true;
			editorTableId.Size = new Size(499, 20);
			editorTableId.TabIndex = 5;
			// 
			// labelControlTabeId
			// 
			labelControlTabeId.Location = new Point(9, 24);
			labelControlTabeId.Name = "labelControlTabeId";
			labelControlTabeId.Size = new Size(40, 13);
			labelControlTabeId.TabIndex = 4;
			labelControlTabeId.Text = "TableId:";
			// 
			// treeListObjectIds
			// 
			treeListObjectIds.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			treeListObjectIds.Appearance.FocusedCell.BackColor = Color.FromArgb(255, 255, 255);
			treeListObjectIds.Appearance.FocusedCell.ForeColor = SystemColors.ControlText;
			treeListObjectIds.Appearance.FocusedRow.ForeColor = SystemColors.ControlText;
			treeListObjectIds.Appearance.FocusedRow.Options.UseBackColor = true;
			treeListObjectIds.Appearance.HideSelectionRow.ForeColor = Color.Black;
			treeListObjectIds.Appearance.HideSelectionRow.Options.UseBackColor = true;
			treeListObjectIds.Appearance.SelectedRow.Options.UseForeColor = true;
			treeListObjectIds.Location = new Point(3, 6);
			treeListObjectIds.Name = "treeListObjectIds";
			treeListObjectIds.OptionsBehavior.Editable = false;
			treeListObjectIds.OptionsDragAndDrop.DropNodesMode = DevExpress.XtraTreeList.DropNodesMode.Advanced;
			treeListObjectIds.OptionsSelection.EnableAppearanceFocusedCell = false;
			treeListObjectIds.OptionsSelection.EnableAppearanceFocusedRow = false;
			treeListObjectIds.OptionsSimple.EditMode = Simple.Controls.TreeList.SimpleTreeEditMode.ViewOnly;
			treeListObjectIds.OptionsSimple.LookAndFeelStyle = Simple.Controls.TreeList.SimpleTreeLookAndFeelStyle.ExcelStyle;
			treeListObjectIds.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
			treeListObjectIds.OptionsView.ShowVertLines = false;
			treeListObjectIds.OptionsView.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None;
			treeListObjectIds.Size = new Size(579, 407);
			treeListObjectIds.TabIndex = 4;
			// 
			// labelControlObjectId
			// 
			labelControlObjectId.Location = new Point(9, 50);
			labelControlObjectId.Name = "labelControlObjectId";
			labelControlObjectId.Size = new Size(46, 13);
			labelControlObjectId.TabIndex = 6;
			labelControlObjectId.Text = "ObjectId:";
			// 
			// editorObjectId
			// 
			editorObjectId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorObjectId.Location = new Point(77, 47);
			editorObjectId.Name = "editorObjectId";
			editorObjectId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorObjectId.Properties.Appearance.Options.UseFont = true;
			editorObjectId.Properties.ReadOnly = true;
			editorObjectId.Size = new Size(499, 20);
			editorObjectId.TabIndex = 7;
			// 
			// labelControlRelationKey
			// 
			labelControlRelationKey.Location = new Point(9, 76);
			labelControlRelationKey.Name = "labelControlRelationKey";
			labelControlRelationKey.Size = new Size(64, 13);
			labelControlRelationKey.TabIndex = 8;
			labelControlRelationKey.Text = "Relation Key:";
			// 
			// editorRelationKey
			// 
			editorRelationKey.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorRelationKey.Location = new Point(77, 73);
			editorRelationKey.Name = "editorRelationKey";
			editorRelationKey.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorRelationKey.Properties.Appearance.Options.UseFont = true;
			editorRelationKey.Properties.ReadOnly = true;
			editorRelationKey.Size = new Size(499, 20);
			editorRelationKey.TabIndex = 9;
			// 
			// EditPanelGetOneToManyForeignObjectCollection
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Name = "EditPanelGetOneToManyForeignObjectCollection";
			Size = new Size(609, 634);
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
			((System.ComponentModel.ISupportInitialize)treeListObjectIds).EndInit();
			((System.ComponentModel.ISupportInitialize)editorObjectId.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorRelationKey.Properties).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private DevExpress.XtraEditors.LabelControl labelControlTabeId;
		private DevExpress.XtraEditors.TextEdit editorTableId;
		private Simple.Controls.TreeList.SimpleTreeList treeListObjectIds;
		private DevExpress.XtraEditors.LabelControl labelControlRelationKey;
		private DevExpress.XtraEditors.TextEdit editorRelationKey;
		private DevExpress.XtraEditors.LabelControl labelControlObjectId;
		private DevExpress.XtraEditors.TextEdit editorObjectId;
	}
}
