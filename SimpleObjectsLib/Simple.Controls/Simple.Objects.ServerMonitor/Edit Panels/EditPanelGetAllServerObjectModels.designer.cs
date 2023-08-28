using System.Windows.Forms;
using System.Drawing;

namespace Simple.Objects.ServerMonitor
{
	partial class EditPaneGetAllServerObjectModels
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
			treeListServerObjectModelsPropertyValues = new Simple.Controls.TreeList.SimpleTreeList();
			labelControlServerObjectModelCount = new DevExpress.XtraEditors.LabelControl();
			labelControlServerObjectModelCountCaption = new DevExpress.XtraEditors.LabelControl();
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
			((System.ComponentModel.ISupportInitialize)treeListServerObjectModelsPropertyValues).BeginInit();
			SuspendLayout();
			// 
			// tabControlRequestResponse
			// 
			tabControlRequestResponse.Size = new Size(586, 535);
			// 
			// tabPageRequest
			// 
			tabPageRequest.Size = new Size(584, 510);
			// 
			// tabPageResponse
			// 
			tabPageResponse.Size = new Size(584, 510);
			// 
			// editorRequestPackageValue
			// 
			errorProvider.SetIconAlignment(editorRequestPackageValue, ErrorIconAlignment.MiddleRight);
			editorRequestPackageValue.Size = new Size(590, 410);
			// 
			// packageControlRequestHeader
			// 
			packageControlRequestHeader.Size = new Size(395, 79);
			// 
			// editorRequestPackageLength
			// 
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
			editorResponsePackageLength.Size = new Size(103, 20);
			// 
			// tabControlResponse
			// 
			tabControlResponse.Size = new Size(588, 441);
			// 
			// tabPageResponseDetails
			// 
			tabPageResponseDetails.Controls.Add(labelControlServerObjectModelCount);
			tabPageResponseDetails.Controls.Add(labelControlServerObjectModelCountCaption);
			tabPageResponseDetails.Controls.Add(treeListServerObjectModelsPropertyValues);
			tabPageResponseDetails.Size = new Size(586, 416);
			// 
			// tabPageResponsePackageBinaries
			// 
			tabPageResponsePackageBinaries.Size = new Size(596, 416);
			// 
			// tabControlRequest
			// 
			tabControlRequest.Size = new Size(578, 441);
			// 
			// tabPageRequestDetails
			// 
			tabPageRequestDetails.Size = new Size(576, 416);
			// 
			// tabPageRequestPackageBinaries
			// 
			tabPageRequestPackageBinaries.Size = new Size(586, 416);
			// 
			// editorRequestPackageHeaderValue
			// 
			editorRequestPackageHeaderValue.Size = new Size(91, 20);
			// 
			// editorResponsePackageHeaderValue
			// 
			editorResponsePackageHeaderValue.Size = new Size(91, 20);
			// 
			// editorUser
			// 
			editorUser.Properties.Appearance.Options.UseFont = true;
			editorUser.Size = new Size(300, 20);
			// 
			// editorMessageCodeOrRequestId
			// 
			editorMessageCodeOrRequestId.Properties.Appearance.Options.UseFont = true;
			editorMessageCodeOrRequestId.Size = new Size(300, 20);
			// 
			// editorActionType
			// 
			editorActionType.Properties.Appearance.Options.UseFont = true;
			// 
			// editorSessionKey
			// 
			editorSessionKey.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
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
			// treeListServerObjectModelsPropertyValues
			// 
			treeListServerObjectModelsPropertyValues.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			treeListServerObjectModelsPropertyValues.Appearance.FocusedCell.BackColor = Color.FromArgb(255, 255, 255);
			treeListServerObjectModelsPropertyValues.Appearance.FocusedCell.ForeColor = SystemColors.ControlText;
			treeListServerObjectModelsPropertyValues.Appearance.FocusedRow.ForeColor = SystemColors.ControlText;
			treeListServerObjectModelsPropertyValues.Appearance.FocusedRow.Options.UseBackColor = true;
			treeListServerObjectModelsPropertyValues.Appearance.HideSelectionRow.ForeColor = Color.Black;
			treeListServerObjectModelsPropertyValues.Appearance.HideSelectionRow.Options.UseBackColor = true;
			treeListServerObjectModelsPropertyValues.Appearance.SelectedRow.Options.UseForeColor = true;
			treeListServerObjectModelsPropertyValues.Location = new Point(3, 31);
			treeListServerObjectModelsPropertyValues.Name = "treeListServerObjectModelsPropertyValues";
			treeListServerObjectModelsPropertyValues.OptionsBehavior.Editable = false;
			treeListServerObjectModelsPropertyValues.OptionsDragAndDrop.DropNodesMode = DevExpress.XtraTreeList.DropNodesMode.Advanced;
			treeListServerObjectModelsPropertyValues.OptionsSelection.EnableAppearanceFocusedCell = false;
			treeListServerObjectModelsPropertyValues.OptionsSelection.EnableAppearanceFocusedRow = false;
			treeListServerObjectModelsPropertyValues.OptionsSimple.EditMode = Simple.Controls.TreeList.SimpleTreeEditMode.ViewOnly;
			treeListServerObjectModelsPropertyValues.OptionsSimple.LookAndFeelStyle = Simple.Controls.TreeList.SimpleTreeLookAndFeelStyle.ExcelStyle;
			treeListServerObjectModelsPropertyValues.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
			treeListServerObjectModelsPropertyValues.OptionsView.ShowVertLines = false;
			treeListServerObjectModelsPropertyValues.OptionsView.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None;
			treeListServerObjectModelsPropertyValues.Size = new Size(580, 382);
			treeListServerObjectModelsPropertyValues.TabIndex = 3;
			// 
			// labelControlServerObjectModelCount
			// 
			labelControlServerObjectModelCount.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			labelControlServerObjectModelCount.Appearance.Options.UseFont = true;
			labelControlServerObjectModelCount.Appearance.Options.UseTextOptions = true;
			labelControlServerObjectModelCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelControlServerObjectModelCount.Location = new Point(148, 12);
			labelControlServerObjectModelCount.Name = "labelControlServerObjectModelCount";
			labelControlServerObjectModelCount.Size = new Size(83, 13);
			labelControlServerObjectModelCount.TabIndex = 47;
			labelControlServerObjectModelCount.Text = "(RecordCount)";
			// 
			// labelControlServerObjectModelCountCaption
			// 
			labelControlServerObjectModelCountCaption.Appearance.Options.UseTextOptions = true;
			labelControlServerObjectModelCountCaption.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelControlServerObjectModelCountCaption.Location = new Point(6, 12);
			labelControlServerObjectModelCountCaption.Name = "labelControlServerObjectModelCountCaption";
			labelControlServerObjectModelCountCaption.Size = new Size(134, 13);
			labelControlServerObjectModelCountCaption.TabIndex = 46;
			labelControlServerObjectModelCountCaption.Text = "Server Object Model Count:";
			// 
			// EditPaneGetAllServerObjectModels
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Name = "EditPaneGetAllServerObjectModels";
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
			((System.ComponentModel.ISupportInitialize)treeListServerObjectModelsPropertyValues).EndInit();
			ResumeLayout(false);
		}

		#endregion
		private Simple.Controls.TreeList.SimpleTreeList treeListServerObjectModelsPropertyValues;
		protected DevExpress.XtraEditors.LabelControl labelControlServerObjectModelCount;
		protected DevExpress.XtraEditors.LabelControl labelControlServerObjectModelCountCaption;
	}
}
