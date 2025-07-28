namespace Simple.Objects.Controls
{
	partial class SystemEditPanelTransactionActionInfo
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
			this.editorObjectTableId = new DevExpress.XtraEditors.TextEdit();
			this.labelControlObjectTableId = new DevExpress.XtraEditors.LabelControl();
			this.labelControlPropertyValues = new DevExpress.XtraEditors.LabelControl();
			this.treeListPropertyValues = new Simple.Controls.TreeList.SimpleTreeList();
			this.editorActionType = new DevExpress.XtraEditors.TextEdit();
			this.labelControlActionType = new DevExpress.XtraEditors.LabelControl();
			this.editorObjectId = new DevExpress.XtraEditors.TextEdit();
			this.labelControlObjectId = new DevExpress.XtraEditors.LabelControl();
			this.editorStatus = new DevExpress.XtraEditors.TextEdit();
			this.labelControlStatus = new DevExpress.XtraEditors.LabelControl();
			this.tabPageDatastoreAction = new DevExpress.XtraTab.XtraTabPage();
			this.editorDatastoreStatus = new DevExpress.XtraEditors.TextEdit();
			this.labelDatastoreStatus = new DevExpress.XtraEditors.LabelControl();
			this.editorDatastoreObjectId = new DevExpress.XtraEditors.TextEdit();
			this.labelDatastoreObjectId = new DevExpress.XtraEditors.LabelControl();
			this.treeListDatastorePropertyValues = new Simple.Controls.TreeList.SimpleTreeList();
			this.editorDatastoreActionType = new DevExpress.XtraEditors.TextEdit();
			this.labelControlDatastoreActionType = new DevExpress.XtraEditors.LabelControl();
			this.labelDatastorePropertyValues = new DevExpress.XtraEditors.LabelControl();
			this.editorDatastoreTableId = new DevExpress.XtraEditors.TextEdit();
			this.labelDatastoreTableId = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
			this.tabControl.SuspendLayout();
			this.tabPageObjectName.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorObjectTableId.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeListPropertyValues)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorActionType.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorObjectId.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorStatus.Properties)).BeginInit();
			this.tabPageDatastoreAction.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorDatastoreStatus.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorDatastoreObjectId.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeListDatastorePropertyValues)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorDatastoreActionType.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorDatastoreTableId.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Size = new System.Drawing.Size(490, 659);
			this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageDatastoreAction});
			this.tabControl.Controls.SetChildIndex(this.tabPageDatastoreAction, 0);
			this.tabControl.Controls.SetChildIndex(this.tabPageObjectName, 0);
			// 
			// tabPageObjectName
			// 
			this.tabPageObjectName.Controls.Add(this.editorStatus);
			this.tabPageObjectName.Controls.Add(this.labelControlStatus);
			this.tabPageObjectName.Controls.Add(this.editorObjectId);
			this.tabPageObjectName.Controls.Add(this.labelControlObjectId);
			this.tabPageObjectName.Controls.Add(this.treeListPropertyValues);
			this.tabPageObjectName.Controls.Add(this.editorActionType);
			this.tabPageObjectName.Controls.Add(this.labelControlActionType);
			this.tabPageObjectName.Controls.Add(this.labelControlPropertyValues);
			this.tabPageObjectName.Controls.Add(this.editorObjectTableId);
			this.tabPageObjectName.Controls.Add(this.labelControlObjectTableId);
			this.tabPageObjectName.Size = new System.Drawing.Size(488, 634);
			this.tabPageObjectName.Text = "Transaction Action Log";
			// 
			// editorObjectTableId
			// 
			this.editorObjectTableId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.editorObjectTableId.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
			this.editorObjectTableId.Location = new System.Drawing.Point(90, 54);
			this.editorObjectTableId.Name = "editorObjectTableId";
			this.editorObjectTableId.Properties.ReadOnly = true;
			this.editorObjectTableId.Size = new System.Drawing.Size(384, 20);
			this.editorObjectTableId.TabIndex = 18;
			// 
			// labelControlObjectTableId
			// 
			this.labelControlObjectTableId.Location = new System.Drawing.Point(9, 57);
			this.labelControlObjectTableId.Name = "labelControlObjectTableId";
			this.labelControlObjectTableId.Size = new System.Drawing.Size(75, 13);
			this.labelControlObjectTableId.TabIndex = 17;
			this.labelControlObjectTableId.Text = "Object TableId:";
			// 
			// labelControlPropertyValues
			// 
			this.labelControlPropertyValues.Location = new System.Drawing.Point(9, 145);
			this.labelControlPropertyValues.Name = "labelControlPropertyValues";
			this.labelControlPropertyValues.Size = new System.Drawing.Size(213, 13);
			this.labelControlPropertyValues.TabIndex = 25;
			this.labelControlPropertyValues.Text = "Property Values (* property is not storable):";
			// 
			// treeListPropertyValues
			// 
			this.treeListPropertyValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeListPropertyValues.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.treeListPropertyValues.Appearance.FocusedCell.ForeColor = System.Drawing.SystemColors.ControlText;
			this.treeListPropertyValues.Appearance.FocusedRow.ForeColor = System.Drawing.SystemColors.ControlText;
			this.treeListPropertyValues.Appearance.FocusedRow.Options.UseBackColor = true;
			this.treeListPropertyValues.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
			this.treeListPropertyValues.Appearance.HideSelectionRow.Options.UseBackColor = true;
			this.treeListPropertyValues.Appearance.SelectedRow.BackColor = System.Drawing.Color.White;
			this.treeListPropertyValues.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Tomato;
			this.treeListPropertyValues.Appearance.SelectedRow.Options.UseForeColor = true;
			this.treeListPropertyValues.Location = new System.Drawing.Point(3, 164);
			this.treeListPropertyValues.Name = "treeListPropertyValues";
			this.treeListPropertyValues.OptionsBehavior.Editable = false;
			this.treeListPropertyValues.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.treeListPropertyValues.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.treeListPropertyValues.OptionsSimple.AutoDisableEditingColumns = false;
			this.treeListPropertyValues.OptionsSimple.EditMode = Simple.Controls.TreeList.SimpleTreeEditMode.ViewOnly;
			this.treeListPropertyValues.OptionsSimple.LookAndFeelStyle = Simple.Controls.TreeList.SimpleTreeLookAndFeelStyle.ExcelStyle;
			this.treeListPropertyValues.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
			this.treeListPropertyValues.OptionsView.ShowRoot = false;
			this.treeListPropertyValues.OptionsView.ShowVertLines = false;
			this.treeListPropertyValues.OptionsView.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None;
			this.treeListPropertyValues.Size = new System.Drawing.Size(482, 467);
			this.treeListPropertyValues.TabIndex = 1;
			// 
			// editorActionType
			// 
			this.editorActionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.editorActionType.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
			this.editorActionType.Location = new System.Drawing.Point(90, 28);
			this.editorActionType.Name = "editorActionType";
			this.editorActionType.Properties.ReadOnly = true;
			this.editorActionType.Size = new System.Drawing.Size(384, 20);
			this.editorActionType.TabIndex = 29;
			// 
			// labelControlActionType
			// 
			this.labelControlActionType.Location = new System.Drawing.Point(9, 31);
			this.labelControlActionType.Name = "labelControlActionType";
			this.labelControlActionType.Size = new System.Drawing.Size(61, 13);
			this.labelControlActionType.TabIndex = 28;
			this.labelControlActionType.Text = "Action Type:";
			// 
			// editorObjectId
			// 
			this.editorObjectId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.editorObjectId.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
			this.editorObjectId.Location = new System.Drawing.Point(90, 80);
			this.editorObjectId.Name = "editorObjectId";
			this.editorObjectId.Properties.ReadOnly = true;
			this.editorObjectId.Size = new System.Drawing.Size(384, 20);
			this.editorObjectId.TabIndex = 31;
			// 
			// labelControlObjectId
			// 
			this.labelControlObjectId.Location = new System.Drawing.Point(9, 83);
			this.labelControlObjectId.Name = "labelControlObjectId";
			this.labelControlObjectId.Size = new System.Drawing.Size(49, 13);
			this.labelControlObjectId.TabIndex = 30;
			this.labelControlObjectId.Text = "Object Id:";
			// 
			// editorStatus
			// 
			this.editorStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.editorStatus.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
			this.editorStatus.Location = new System.Drawing.Point(90, 106);
			this.editorStatus.Name = "editorStatus";
			this.editorStatus.Properties.ReadOnly = true;
			this.editorStatus.Size = new System.Drawing.Size(384, 20);
			this.editorStatus.TabIndex = 35;
			// 
			// labelControlStatus
			// 
			this.labelControlStatus.Location = new System.Drawing.Point(9, 109);
			this.labelControlStatus.Name = "labelControlStatus";
			this.labelControlStatus.Size = new System.Drawing.Size(35, 13);
			this.labelControlStatus.TabIndex = 34;
			this.labelControlStatus.Text = "Status:";
			// 
			// tabPageDatastoreAction
			// 
			this.tabPageDatastoreAction.Controls.Add(this.editorDatastoreStatus);
			this.tabPageDatastoreAction.Controls.Add(this.labelDatastoreStatus);
			this.tabPageDatastoreAction.Controls.Add(this.editorDatastoreObjectId);
			this.tabPageDatastoreAction.Controls.Add(this.labelDatastoreObjectId);
			this.tabPageDatastoreAction.Controls.Add(this.treeListDatastorePropertyValues);
			this.tabPageDatastoreAction.Controls.Add(this.editorDatastoreActionType);
			this.tabPageDatastoreAction.Controls.Add(this.labelControlDatastoreActionType);
			this.tabPageDatastoreAction.Controls.Add(this.labelDatastorePropertyValues);
			this.tabPageDatastoreAction.Controls.Add(this.editorDatastoreTableId);
			this.tabPageDatastoreAction.Controls.Add(this.labelDatastoreTableId);
			this.tabPageDatastoreAction.Name = "tabPageDatastoreAction";
			this.tabPageDatastoreAction.Size = new System.Drawing.Size(488, 634);
			this.tabPageDatastoreAction.Text = "Datastore Action";
			// 
			// editorDatastoreStatus
			// 
			this.editorDatastoreStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.editorDatastoreStatus.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
			this.editorDatastoreStatus.Location = new System.Drawing.Point(90, 106);
			this.editorDatastoreStatus.Name = "editorDatastoreStatus";
			this.editorDatastoreStatus.Properties.ReadOnly = true;
			this.editorDatastoreStatus.Size = new System.Drawing.Size(387, 20);
			this.editorDatastoreStatus.TabIndex = 45;
			// 
			// labelDatastoreStatus
			// 
			this.labelDatastoreStatus.Location = new System.Drawing.Point(9, 109);
			this.labelDatastoreStatus.Name = "labelDatastoreStatus";
			this.labelDatastoreStatus.Size = new System.Drawing.Size(35, 13);
			this.labelDatastoreStatus.TabIndex = 44;
			this.labelDatastoreStatus.Text = "Status:";
			// 
			// editorDatastoreObjectId
			// 
			this.editorDatastoreObjectId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.editorDatastoreObjectId.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
			this.editorDatastoreObjectId.Location = new System.Drawing.Point(90, 80);
			this.editorDatastoreObjectId.Name = "editorDatastoreObjectId";
			this.editorDatastoreObjectId.Properties.ReadOnly = true;
			this.editorDatastoreObjectId.Size = new System.Drawing.Size(387, 20);
			this.editorDatastoreObjectId.TabIndex = 43;
			// 
			// labelDatastoreObjectId
			// 
			this.labelDatastoreObjectId.Location = new System.Drawing.Point(9, 83);
			this.labelDatastoreObjectId.Name = "labelDatastoreObjectId";
			this.labelDatastoreObjectId.Size = new System.Drawing.Size(49, 13);
			this.labelDatastoreObjectId.TabIndex = 42;
			this.labelDatastoreObjectId.Text = "Object Id:";
			// 
			// treeListDatastorePropertyValues
			// 
			this.treeListDatastorePropertyValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeListDatastorePropertyValues.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.treeListDatastorePropertyValues.Appearance.FocusedCell.ForeColor = System.Drawing.SystemColors.ControlText;
			this.treeListDatastorePropertyValues.Appearance.FocusedRow.ForeColor = System.Drawing.SystemColors.ControlText;
			this.treeListDatastorePropertyValues.Appearance.FocusedRow.Options.UseBackColor = true;
			this.treeListDatastorePropertyValues.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
			this.treeListDatastorePropertyValues.Appearance.HideSelectionRow.Options.UseBackColor = true;
			this.treeListDatastorePropertyValues.Appearance.SelectedRow.BackColor = System.Drawing.Color.White;
			this.treeListDatastorePropertyValues.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Tomato;
			this.treeListDatastorePropertyValues.Appearance.SelectedRow.Options.UseForeColor = true;
			this.treeListDatastorePropertyValues.Location = new System.Drawing.Point(3, 164);
			this.treeListDatastorePropertyValues.Name = "treeListDatastorePropertyValues";
			this.treeListDatastorePropertyValues.OptionsBehavior.Editable = false;
			this.treeListDatastorePropertyValues.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.treeListDatastorePropertyValues.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.treeListDatastorePropertyValues.OptionsSimple.AutoDisableEditingColumns = false;
			this.treeListDatastorePropertyValues.OptionsSimple.EditMode = Simple.Controls.TreeList.SimpleTreeEditMode.ViewOnly;
			this.treeListDatastorePropertyValues.OptionsSimple.LookAndFeelStyle = Simple.Controls.TreeList.SimpleTreeLookAndFeelStyle.ExcelStyle;
			this.treeListDatastorePropertyValues.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
			this.treeListDatastorePropertyValues.OptionsView.ShowRoot = false;
			this.treeListDatastorePropertyValues.OptionsView.ShowVertLines = false;
			this.treeListDatastorePropertyValues.OptionsView.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None;
			this.treeListDatastorePropertyValues.Size = new System.Drawing.Size(482, 467);
			this.treeListDatastorePropertyValues.TabIndex = 36;
			// 
			// editorDatastoreActionType
			// 
			this.editorDatastoreActionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.editorDatastoreActionType.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
			this.editorDatastoreActionType.Location = new System.Drawing.Point(90, 28);
			this.editorDatastoreActionType.Name = "editorDatastoreActionType";
			this.editorDatastoreActionType.Properties.ReadOnly = true;
			this.editorDatastoreActionType.Size = new System.Drawing.Size(387, 20);
			this.editorDatastoreActionType.TabIndex = 41;
			// 
			// labelControlDatastoreActionType
			// 
			this.labelControlDatastoreActionType.Location = new System.Drawing.Point(9, 31);
			this.labelControlDatastoreActionType.Name = "labelControlDatastoreActionType";
			this.labelControlDatastoreActionType.Size = new System.Drawing.Size(61, 13);
			this.labelControlDatastoreActionType.TabIndex = 40;
			this.labelControlDatastoreActionType.Text = "Action Type:";
			// 
			// labelDatastorePropertyValues
			// 
			this.labelDatastorePropertyValues.Location = new System.Drawing.Point(9, 145);
			this.labelDatastorePropertyValues.Name = "labelDatastorePropertyValues";
			this.labelDatastorePropertyValues.Size = new System.Drawing.Size(80, 13);
			this.labelDatastorePropertyValues.TabIndex = 39;
			this.labelDatastorePropertyValues.Text = "Property Values:";
			// 
			// editorDatastoreTableId
			// 
			this.editorDatastoreTableId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.editorDatastoreTableId.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
			this.editorDatastoreTableId.Location = new System.Drawing.Point(90, 54);
			this.editorDatastoreTableId.Name = "editorDatastoreTableId";
			this.editorDatastoreTableId.Properties.ReadOnly = true;
			this.editorDatastoreTableId.Size = new System.Drawing.Size(387, 20);
			this.editorDatastoreTableId.TabIndex = 38;
			// 
			// labelDatastoreTableId
			// 
			this.labelDatastoreTableId.Location = new System.Drawing.Point(9, 57);
			this.labelDatastoreTableId.Name = "labelDatastoreTableId";
			this.labelDatastoreTableId.Size = new System.Drawing.Size(75, 13);
			this.labelDatastoreTableId.TabIndex = 37;
			this.labelDatastoreTableId.Text = "Object TableId:";
			// 
			// SystemEditPanelTransactionActionInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Name = "SystemEditPanelTransactionActionInfo";
			((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.tabPageObjectName.ResumeLayout(false);
			this.tabPageObjectName.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorObjectTableId.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeListPropertyValues)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorActionType.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorObjectId.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorStatus.Properties)).EndInit();
			this.tabPageDatastoreAction.ResumeLayout(false);
			this.tabPageDatastoreAction.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorDatastoreStatus.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorDatastoreObjectId.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeListDatastorePropertyValues)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorDatastoreActionType.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorDatastoreTableId.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraEditors.TextEdit editorObjectTableId;
		private DevExpress.XtraEditors.LabelControl labelControlObjectTableId;
		private DevExpress.XtraEditors.LabelControl labelControlPropertyValues;
		private DevExpress.XtraEditors.TextEdit editorActionType;
		private DevExpress.XtraEditors.LabelControl labelControlActionType;
		private Simple.Controls.TreeList.SimpleTreeList treeListPropertyValues;
		private DevExpress.XtraEditors.TextEdit editorObjectId;
		private DevExpress.XtraEditors.LabelControl labelControlObjectId;
		private DevExpress.XtraEditors.TextEdit editorStatus;
		private DevExpress.XtraEditors.LabelControl labelControlStatus;
		private DevExpress.XtraTab.XtraTabPage tabPageDatastoreAction;
		private DevExpress.XtraEditors.TextEdit editorDatastoreStatus;
		private DevExpress.XtraEditors.LabelControl labelDatastoreStatus;
		private DevExpress.XtraEditors.TextEdit editorDatastoreObjectId;
		private DevExpress.XtraEditors.LabelControl labelDatastoreObjectId;
		private Simple.Controls.TreeList.SimpleTreeList treeListDatastorePropertyValues;
		private DevExpress.XtraEditors.TextEdit editorDatastoreActionType;
		private DevExpress.XtraEditors.LabelControl labelControlDatastoreActionType;
		private DevExpress.XtraEditors.LabelControl labelDatastorePropertyValues;
		private DevExpress.XtraEditors.TextEdit editorDatastoreTableId;
		private DevExpress.XtraEditors.LabelControl labelDatastoreTableId;
	}
}
