//using System.Windows.Forms;
//using System.Drawing;

//namespace Simple.Objects.ServerMonitor
//{
//	partial class EditPanelGetObjectPropertyIndexValuePairsOriginal
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
//			this.labelControlObjectId = new DevExpress.XtraEditors.LabelControl();
//			this.editorObjectId = new DevExpress.XtraEditors.TextEdit();
//			this.treeListObjectPropertyValues = new Simple.Controls.TreeList.SimpleTreeList();
//			this.labelControlObjectIdInfo = new DevExpress.XtraEditors.LabelControl();
//			((System.ComponentModel.ISupportInitialize)(this.tabControlRequestResponse)).BeginInit();
//			this.tabControlRequestResponse.SuspendLayout();
//			this.tabPageRequest.SuspendLayout();
//			this.tabPageResponse.SuspendLayout();
//			((System.ComponentModel.ISupportInitialize)(this.editorRequestPackageValue.Properties)).BeginInit();
//			((System.ComponentModel.ISupportInitialize)(this.editorRequestPackageLength.Properties)).BeginInit();
//			((System.ComponentModel.ISupportInitialize)(this.editorResponsePackageValue.Properties)).BeginInit();
//			((System.ComponentModel.ISupportInitialize)(this.editorResponsePackageLength.Properties)).BeginInit();
//			((System.ComponentModel.ISupportInitialize)(this.tabControlResponse)).BeginInit();
//			this.tabControlResponse.SuspendLayout();
//			this.tabPageResponseDetails.SuspendLayout();
//			this.tabPageResponsePackageBinaries.SuspendLayout();
//			((System.ComponentModel.ISupportInitialize)(this.tabControlRequest)).BeginInit();
//			this.tabControlRequest.SuspendLayout();
//			this.tabPageRequestDetails.SuspendLayout();
//			this.tabPageRequestPackageBinaries.SuspendLayout();
//			((System.ComponentModel.ISupportInitialize)(this.editorRequestPackageHeaderValue.Properties)).BeginInit();
//			((System.ComponentModel.ISupportInitialize)(this.editorResponsePackageHeaderValue.Properties)).BeginInit();
//			((System.ComponentModel.ISupportInitialize)(this.editorUser.Properties)).BeginInit();
//			((System.ComponentModel.ISupportInitialize)(this.editorMessageCodeOrRequestId.Properties)).BeginInit();
//			((System.ComponentModel.ISupportInitialize)(this.editorActionType.Properties)).BeginInit();
//			((System.ComponentModel.ISupportInitialize)(this.editorSessionKey.Properties)).BeginInit();
//			((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
//			this.tabControl.SuspendLayout();
//			this.tabPageObjectName.SuspendLayout();
//			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
//			((System.ComponentModel.ISupportInitialize)(this.editorObjectId.Properties)).BeginInit();
//			((System.ComponentModel.ISupportInitialize)(this.treeListObjectPropertyValues)).BeginInit();
//			this.SuspendLayout();
//			// 
//			// tabControlRequestResponse
//			// 
//			this.tabControlRequestResponse.Size = new System.Drawing.Size(577, 533);
//			// 
//			// tabPageRequest
//			// 
//			this.tabPageRequest.Size = new System.Drawing.Size(575, 508);
//			// 
//			// tabPageResponse
//			// 
//			this.tabPageResponse.Size = new System.Drawing.Size(575, 508);
//			// 
//			// editorRequestPackageValue
//			// 
//			this.errorProvider.SetIconAlignment(this.editorRequestPackageValue, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
//			this.editorRequestPackageValue.Size = new System.Drawing.Size(552, 402);
//			// 
//			// packageFlagControlRequest
//			// 
//			this.packageControlRequestHeader.Location = new System.Drawing.Point(206, 4);
//			this.packageControlRequestHeader.Size = new System.Drawing.Size(385, 79);
//			// 
//			// editorRequestPackageLength
//			// 
//			// 
//			// editorResponsePackageValue
//			// 
//			this.errorProvider.SetIconAlignment(this.editorResponsePackageValue, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
//			this.editorResponsePackageValue.Size = new System.Drawing.Size(531, 424);
//			// 
//			// packageFlagsControlResponse
//			// 
//			this.packageHeaderControlResponse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
//            | System.Windows.Forms.AnchorStyles.Right)));
//			this.packageHeaderControlResponse.Size = new System.Drawing.Size(366, 79);
//			// 
//			// editorResponsePackageLength
//			// 
//			this.editorResponsePackageLength.Size = new System.Drawing.Size(103, 20);
//			// 
//			// tabControlResponse
//			// 
//			this.tabControlResponse.Size = new System.Drawing.Size(569, 439);
//			// 
//			// tabPageResponseDetails
//			// 
//			this.tabPageResponseDetails.Controls.Add(this.treeListObjectPropertyValues);
//			this.tabPageResponseDetails.Size = new System.Drawing.Size(567, 414);
//			// 
//			// tabPageResponsePackageBinaries
//			// 
//			this.tabPageResponsePackageBinaries.Size = new System.Drawing.Size(567, 414);
//			// 
//			// tabControlRequest
//			// 
//			this.tabControlRequest.Size = new System.Drawing.Size(568, 439);
//			// 
//			// tabPageRequestDetails
//			// 
//			this.tabPageRequestDetails.Controls.Add(this.labelControlObjectIdInfo);
//			this.tabPageRequestDetails.Controls.Add(this.labelControlObjectId);
//			this.tabPageRequestDetails.Controls.Add(this.editorObjectId);
//			this.tabPageRequestDetails.Size = new System.Drawing.Size(566, 414);
//			// 
//			// tabPageRequestPackageBinaries
//			// 
//			this.tabPageRequestPackageBinaries.Size = new System.Drawing.Size(585, 414);
//			// 
//			// editorRequestPackageFlagsValue
//			// 
//			// 
//			// editorResponsePackageFlagsValue
//			// 
//			this.editorResponsePackageHeaderValue.Size = new System.Drawing.Size(103, 20);
//			// 
//			// editorUser
//			// 
//			this.editorUser.Properties.Appearance.Options.UseFont = true;
//			this.editorUser.Size = new System.Drawing.Size(327, 20);
//			// 
//			// editorMessageCodeOrRequestId
//			// 
//			this.editorMessageCodeOrRequestId.Properties.Appearance.Options.UseFont = true;
//			this.editorMessageCodeOrRequestId.Size = new System.Drawing.Size(327, 20);
//			// 
//			// editorActionType
//			// 
//			this.editorActionType.Properties.Appearance.Options.UseFont = true;
//			this.editorActionType.Size = new System.Drawing.Size(327, 20);
//			// 
//			// editorSessionKey
//			// 
//			this.editorSessionKey.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
//			this.editorSessionKey.Properties.Appearance.Options.UseFont = true;
//			// 
//			// labelControlMessageCode
//			// 
//			this.labelControlMessageCodeOrRequestId.Appearance.Options.UseTextOptions = true;
//			this.labelControlMessageCodeOrRequestId.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
//			// 
//			// tabControl
//			// 
//			this.tabControl.Size = new System.Drawing.Size(605, 628);
//			// 
//			// tabPageObjectName
//			// 
//			this.tabPageObjectName.Size = new System.Drawing.Size(603, 603);
//			// 
//			// labelControlObjectId
//			// 
//			this.labelControlObjectId.Location = new System.Drawing.Point(9, 24);
//			this.labelControlObjectId.Name = "labelControlObjectId";
//			this.labelControlObjectId.Size = new System.Drawing.Size(57, 13);
//			this.labelControlObjectId.TabIndex = 6;
//			this.labelControlObjectId.Text = "Object Key:";
//			// 
//			// editorObjectId
//			// 
//			this.editorObjectId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
//            | System.Windows.Forms.AnchorStyles.Right)));
//			this.editorObjectId.Location = new System.Drawing.Point(72, 21);
//			this.editorObjectId.Name = "editorObjectId";
//			this.editorObjectId.Properties.ReadOnly = true;
//			this.editorObjectId.Size = new System.Drawing.Size(483, 20);
//			this.editorObjectId.TabIndex = 7;
//			// 
//			// treeListObjectPropertyValues
//			// 
//			this.treeListObjectPropertyValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
//            | System.Windows.Forms.AnchorStyles.Left) 
//            | System.Windows.Forms.AnchorStyles.Right)));
//			this.treeListObjectPropertyValues.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
//			this.treeListObjectPropertyValues.Appearance.FocusedCell.ForeColor = System.Drawing.SystemColors.ControlText;
//			this.treeListObjectPropertyValues.Appearance.FocusedRow.ForeColor = System.Drawing.SystemColors.ControlText;
//			this.treeListObjectPropertyValues.Appearance.FocusedRow.Options.UseBackColor = true;
//			this.treeListObjectPropertyValues.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
//			this.treeListObjectPropertyValues.Appearance.HideSelectionRow.Options.UseBackColor = true;
//			this.treeListObjectPropertyValues.Appearance.SelectedRow.Options.UseForeColor = true;
//			this.treeListObjectPropertyValues.Location = new System.Drawing.Point(3, 14);
//			this.treeListObjectPropertyValues.Name = "treeListObjectPropertyValues";
//			this.treeListObjectPropertyValues.OptionsBehavior.Editable = false;
//			this.treeListObjectPropertyValues.OptionsDragAndDrop.DropNodesMode = DevExpress.XtraTreeList.DropNodesMode.Advanced;
//			this.treeListObjectPropertyValues.OptionsSelection.EnableAppearanceFocusedCell = false;
//			this.treeListObjectPropertyValues.OptionsSelection.EnableAppearanceFocusedRow = false;
//			this.treeListObjectPropertyValues.OptionsSimple.EditMode = Simple.Controls.TreeList.SimpleTreeEditMode.ViewOnly;
//			this.treeListObjectPropertyValues.OptionsSimple.LookAndFeelStyle = Simple.Controls.TreeList.SimpleTreeLookAndFeelStyle.ExcelStyle;
//			this.treeListObjectPropertyValues.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
//			this.treeListObjectPropertyValues.OptionsView.ShowVertLines = false;
//			this.treeListObjectPropertyValues.OptionsView.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None;
//			this.treeListObjectPropertyValues.Size = new System.Drawing.Size(558, 396);
//			this.treeListObjectPropertyValues.TabIndex = 2;
//			// 
//			// labelControlObjectIdInfo
//			// 
//			this.labelControlObjectIdInfo.Location = new System.Drawing.Point(72, 47);
//			this.labelControlObjectIdInfo.Name = "labelControlObjectIdInfo";
//			this.labelControlObjectIdInfo.Size = new System.Drawing.Size(82, 13);
//			this.labelControlObjectIdInfo.TabIndex = 8;
//			this.labelControlObjectIdInfo.Text = "TableId.ObjectId";
//			// 
//			// EditPanelGetObjectSequencePropertyValues
//			// 
//			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//			this.Name = "EditPanelGetObjectSequencePropertyValues";
//			this.Size = new System.Drawing.Size(607, 634);
//			((System.ComponentModel.ISupportInitialize)(this.tabControlRequestResponse)).EndInit();
//			this.tabControlRequestResponse.ResumeLayout(false);
//			this.tabPageRequest.ResumeLayout(false);
//			this.tabPageRequest.PerformLayout();
//			this.tabPageResponse.ResumeLayout(false);
//			this.tabPageResponse.PerformLayout();
//			((System.ComponentModel.ISupportInitialize)(this.editorRequestPackageValue.Properties)).EndInit();
//			((System.ComponentModel.ISupportInitialize)(this.editorRequestPackageLength.Properties)).EndInit();
//			((System.ComponentModel.ISupportInitialize)(this.editorResponsePackageValue.Properties)).EndInit();
//			((System.ComponentModel.ISupportInitialize)(this.editorResponsePackageLength.Properties)).EndInit();
//			((System.ComponentModel.ISupportInitialize)(this.tabControlResponse)).EndInit();
//			this.tabControlResponse.ResumeLayout(false);
//			this.tabPageResponseDetails.ResumeLayout(false);
//			this.tabPageResponsePackageBinaries.ResumeLayout(false);
//			((System.ComponentModel.ISupportInitialize)(this.tabControlRequest)).EndInit();
//			this.tabControlRequest.ResumeLayout(false);
//			this.tabPageRequestDetails.ResumeLayout(false);
//			this.tabPageRequestDetails.PerformLayout();
//			this.tabPageRequestPackageBinaries.ResumeLayout(false);
//			((System.ComponentModel.ISupportInitialize)(this.editorRequestPackageHeaderValue.Properties)).EndInit();
//			((System.ComponentModel.ISupportInitialize)(this.editorResponsePackageHeaderValue.Properties)).EndInit();
//			((System.ComponentModel.ISupportInitialize)(this.editorUser.Properties)).EndInit();
//			((System.ComponentModel.ISupportInitialize)(this.editorMessageCodeOrRequestId.Properties)).EndInit();
//			((System.ComponentModel.ISupportInitialize)(this.editorActionType.Properties)).EndInit();
//			((System.ComponentModel.ISupportInitialize)(this.editorSessionKey.Properties)).EndInit();
//			((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
//			this.tabControl.ResumeLayout(false);
//			this.tabPageObjectName.ResumeLayout(false);
//			this.tabPageObjectName.PerformLayout();
//			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
//			((System.ComponentModel.ISupportInitialize)(this.editorObjectId.Properties)).EndInit();
//			((System.ComponentModel.ISupportInitialize)(this.treeListObjectPropertyValues)).EndInit();
//			this.ResumeLayout(false);

//		}

//		#endregion

//		protected DevExpress.XtraEditors.LabelControl labelControlObjectId;
//		protected DevExpress.XtraEditors.TextEdit editorObjectId;
//		protected Simple.Controls.TreeList.SimpleTreeList treeListObjectPropertyValues;
//		protected DevExpress.XtraEditors.LabelControl labelControlObjectIdInfo;
//	}
//}
