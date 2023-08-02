using System.Windows.Forms;
using System.Drawing;

namespace Simple.Objects.ServerMonitor
{
	partial class EditPanelGetGraphElementsWithObjects
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
			labelControlGraphKey = new DevExpress.XtraEditors.LabelControl();
			editorGraphKey = new DevExpress.XtraEditors.TextEdit();
			labelControlParentGraphElementId = new DevExpress.XtraEditors.LabelControl();
			editorParentGraphElementId = new DevExpress.XtraEditors.TextEdit();
			tabControlResponseDetails = new DevExpress.XtraTab.XtraTabControl();
			tabPageResponseNewServerObjectPropertyModels = new DevExpress.XtraTab.XtraTabPage();
			treeListNewObjectModels = new Simple.Controls.TreeList.SimpleTreeList();
			tabPageResponseGraphElementsWithObjects = new DevExpress.XtraTab.XtraTabPage();
			gridControlGraphElementsWithObjects = new DevExpress.XtraGrid.GridControl();
			gridViewGraphElementsWithObjects = new DevExpress.XtraGrid.Views.Grid.GridView();
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
			((System.ComponentModel.ISupportInitialize)editorGraphKey.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorParentGraphElementId.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)tabControlResponseDetails).BeginInit();
			tabControlResponseDetails.SuspendLayout();
			tabPageResponseNewServerObjectPropertyModels.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)treeListNewObjectModels).BeginInit();
			tabPageResponseGraphElementsWithObjects.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)gridControlGraphElementsWithObjects).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridViewGraphElementsWithObjects).BeginInit();
			SuspendLayout();
			// 
			// tabControlRequestResponse
			// 
			tabControlRequestResponse.Size = new Size(570, 535);
			// 
			// tabPageRequest
			// 
			tabPageRequest.Size = new Size(568, 510);
			// 
			// tabPageResponse
			// 
			tabPageResponse.Size = new Size(568, 510);
			// 
			// editorRequestPackageValue
			// 
			errorProvider.SetIconAlignment(editorRequestPackageValue, ErrorIconAlignment.MiddleRight);
			editorRequestPackageValue.Size = new Size(577, 410);
			// 
			// packageControlRequestHeader
			// 
			packageControlRequestHeader.Size = new Size(358, 79);
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
			packageHeaderControlResponse.Size = new Size(358, 79);
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
			tabControlResponse.Size = new Size(562, 441);
			// 
			// tabPageResponseDetails
			// 
			tabPageResponseDetails.Controls.Add(tabControlResponseDetails);
			tabPageResponseDetails.Size = new Size(560, 416);
			// 
			// tabPageResponsePackageBinaries
			// 
			tabPageResponsePackageBinaries.Size = new Size(583, 416);
			// 
			// tabControlRequest
			// 
			tabControlRequest.Size = new Size(562, 441);
			// 
			// tabPageRequestDetails
			// 
			tabPageRequestDetails.Controls.Add(labelControlParentGraphElementId);
			tabPageRequestDetails.Controls.Add(editorParentGraphElementId);
			tabPageRequestDetails.Controls.Add(labelControlGraphKey);
			tabPageRequestDetails.Controls.Add(editorGraphKey);
			tabPageRequestDetails.Size = new Size(560, 416);
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
			editorUser.Size = new Size(261, 20);
			// 
			// editorMessageCodeOrRequestId
			// 
			editorMessageCodeOrRequestId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorMessageCodeOrRequestId.Properties.Appearance.Options.UseFont = true;
			editorMessageCodeOrRequestId.Size = new Size(261, 20);
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
			// labelControlGraphKey
			// 
			labelControlGraphKey.Location = new Point(74, 24);
			labelControlGraphKey.Name = "labelControlGraphKey";
			labelControlGraphKey.Size = new Size(54, 13);
			labelControlGraphKey.TabIndex = 6;
			labelControlGraphKey.Text = "Graph Key:";
			// 
			// editorGraphKey
			// 
			editorGraphKey.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorGraphKey.Location = new Point(134, 21);
			editorGraphKey.Name = "editorGraphKey";
			editorGraphKey.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorGraphKey.Properties.Appearance.Options.UseFont = true;
			editorGraphKey.Properties.ReadOnly = true;
			editorGraphKey.Size = new Size(413, 20);
			editorGraphKey.TabIndex = 7;
			// 
			// labelControlParentGraphElementId
			// 
			labelControlParentGraphElementId.Location = new Point(9, 50);
			labelControlParentGraphElementId.Name = "labelControlParentGraphElementId";
			labelControlParentGraphElementId.Size = new Size(119, 13);
			labelControlParentGraphElementId.TabIndex = 9;
			labelControlParentGraphElementId.Text = "Parent GraphElement Id:";
			// 
			// editorParentGraphElementId
			// 
			editorParentGraphElementId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			editorParentGraphElementId.Location = new Point(134, 47);
			editorParentGraphElementId.Name = "editorParentGraphElementId";
			editorParentGraphElementId.Properties.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
			editorParentGraphElementId.Properties.Appearance.Options.UseFont = true;
			editorParentGraphElementId.Properties.ReadOnly = true;
			editorParentGraphElementId.Size = new Size(413, 20);
			editorParentGraphElementId.TabIndex = 10;
			// 
			// tabControlResponseDetails
			// 
			tabControlResponseDetails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			tabControlResponseDetails.Location = new Point(4, 8);
			tabControlResponseDetails.Name = "tabControlResponseDetails";
			tabControlResponseDetails.SelectedTabPage = tabPageResponseNewServerObjectPropertyModels;
			tabControlResponseDetails.Size = new Size(553, 405);
			tabControlResponseDetails.TabIndex = 1;
			tabControlResponseDetails.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { tabPageResponseNewServerObjectPropertyModels, tabPageResponseGraphElementsWithObjects });
			// 
			// tabPageResponseNewServerObjectPropertyModels
			// 
			tabPageResponseNewServerObjectPropertyModels.Controls.Add(treeListNewObjectModels);
			tabPageResponseNewServerObjectPropertyModels.Name = "tabPageResponseNewServerObjectPropertyModels";
			tabPageResponseNewServerObjectPropertyModels.Size = new Size(551, 380);
			tabPageResponseNewServerObjectPropertyModels.Text = "New Server Object Models";
			// 
			// treeListNewObjectModels
			// 
			treeListNewObjectModels.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			treeListNewObjectModels.Appearance.FocusedCell.ForeColor = Color.Black;
			treeListNewObjectModels.Appearance.FocusedRow.ForeColor = Color.Black;
			treeListNewObjectModels.Appearance.HideSelectionRow.ForeColor = Color.Black;
			treeListNewObjectModels.Location = new Point(3, 3);
			treeListNewObjectModels.Name = "treeListNewObjectModels";
			treeListNewObjectModels.OptionsBehavior.Editable = false;
			treeListNewObjectModels.OptionsMenu.ShowExpandCollapseItems = false;
			treeListNewObjectModels.OptionsSelection.EnableAppearanceFocusedCell = false;
			treeListNewObjectModels.OptionsSelection.EnableAppearanceFocusedRow = false;
			treeListNewObjectModels.OptionsSimple.EditMode = Simple.Controls.TreeList.SimpleTreeEditMode.ViewOnly;
			treeListNewObjectModels.OptionsSimple.LookAndFeelStyle = Simple.Controls.TreeList.SimpleTreeLookAndFeelStyle.ExcelStyle;
			treeListNewObjectModels.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
			treeListNewObjectModels.OptionsView.ShowHorzLines = false;
			treeListNewObjectModels.OptionsView.ShowIndicator = false;
			treeListNewObjectModels.OptionsView.ShowVertLines = false;
			treeListNewObjectModels.OptionsView.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None;
			treeListNewObjectModels.Size = new Size(545, 374);
			treeListNewObjectModels.TabIndex = 0;
			// 
			// tabPageResponseGraphElementsWithObjects
			// 
			tabPageResponseGraphElementsWithObjects.Controls.Add(gridControlGraphElementsWithObjects);
			tabPageResponseGraphElementsWithObjects.Name = "tabPageResponseGraphElementsWithObjects";
			tabPageResponseGraphElementsWithObjects.Size = new Size(574, 380);
			tabPageResponseGraphElementsWithObjects.Text = "GraphElements with Objects";
			// 
			// gridControlGraphElementsWithObjects
			// 
			gridControlGraphElementsWithObjects.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			gridControlGraphElementsWithObjects.Location = new Point(3, 3);
			gridControlGraphElementsWithObjects.MainView = gridViewGraphElementsWithObjects;
			gridControlGraphElementsWithObjects.Name = "gridControlGraphElementsWithObjects";
			gridControlGraphElementsWithObjects.Size = new Size(568, 374);
			gridControlGraphElementsWithObjects.TabIndex = 1;
			gridControlGraphElementsWithObjects.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewGraphElementsWithObjects });
			// 
			// gridViewGraphElementsWithObjects
			// 
			gridViewGraphElementsWithObjects.GridControl = gridControlGraphElementsWithObjects;
			gridViewGraphElementsWithObjects.Name = "gridViewGraphElementsWithObjects";
			// 
			// EditPanelGetGraphElementsWithObjects
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Name = "EditPanelGetGraphElementsWithObjects";
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
			((System.ComponentModel.ISupportInitialize)editorGraphKey.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorParentGraphElementId.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)tabControlResponseDetails).EndInit();
			tabControlResponseDetails.ResumeLayout(false);
			tabPageResponseNewServerObjectPropertyModels.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)treeListNewObjectModels).EndInit();
			tabPageResponseGraphElementsWithObjects.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)gridControlGraphElementsWithObjects).EndInit();
			((System.ComponentModel.ISupportInitialize)gridViewGraphElementsWithObjects).EndInit();
			ResumeLayout(false);
		}

		#endregion

		protected DevExpress.XtraEditors.LabelControl labelControlGraphKey;
		protected DevExpress.XtraEditors.TextEdit editorGraphKey;
		protected DevExpress.XtraEditors.LabelControl labelControlParentGraphElementId;
		protected DevExpress.XtraEditors.TextEdit editorParentGraphElementId;
		private DevExpress.XtraTab.XtraTabControl tabControlResponseDetails;
		private DevExpress.XtraTab.XtraTabPage tabPageResponseNewServerObjectPropertyModels;
		private DevExpress.XtraTab.XtraTabPage tabPageResponseGraphElementsWithObjects;
		private Simple.Controls.TreeList.SimpleTreeList treeListNewObjectModels;
		private DevExpress.XtraGrid.GridControl gridControlGraphElementsWithObjects;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewGraphElementsWithObjects;
		//private DevExpress.XtraTab.XtraTabPage tabPageResponseGraphElementsWithObjects;
	}
}
