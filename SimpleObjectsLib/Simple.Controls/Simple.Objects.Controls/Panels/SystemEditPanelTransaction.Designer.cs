namespace Simple.Objects.Controls
{
	partial class SystemEditPanelTransaction
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
			this.editorTransactionId = new DevExpress.XtraEditors.TextEdit();
			this.labelControlTransactionId = new DevExpress.XtraEditors.LabelControl();
			this.editorStatus = new DevExpress.XtraEditors.TextEdit();
			this.labelControlStatus = new DevExpress.XtraEditors.LabelControl();
			this.editorTime = new DevExpress.XtraEditors.TextEdit();
			this.labelControlTime = new DevExpress.XtraEditors.LabelControl();
			this.editorRequestCount = new DevExpress.XtraEditors.TextEdit();
			this.labelControlRequestCount = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
			this.tabControl.SuspendLayout();
			this.tabPageObjectName.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorTransactionId.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorStatus.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorTime.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorRequestCount.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// tabPageObjectName
			// 
			this.tabPageObjectName.Controls.Add(this.editorRequestCount);
			this.tabPageObjectName.Controls.Add(this.labelControlRequestCount);
			this.tabPageObjectName.Controls.Add(this.editorTime);
			this.tabPageObjectName.Controls.Add(this.labelControlTime);
			this.tabPageObjectName.Controls.Add(this.editorStatus);
			this.tabPageObjectName.Controls.Add(this.labelControlStatus);
			this.tabPageObjectName.Controls.Add(this.editorTransactionId);
			this.tabPageObjectName.Controls.Add(this.labelControlTransactionId);
			this.tabPageObjectName.Text = "Transaction";
			// 
			// editorTransactionId
			// 
			this.editorTransactionId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.editorTransactionId.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
			this.editorTransactionId.Location = new System.Drawing.Point(90, 28);
			this.editorTransactionId.Name = "editorTransactionId";
			this.editorTransactionId.Properties.ReadOnly = true;
			this.editorTransactionId.Size = new System.Drawing.Size(387, 20);
			this.editorTransactionId.TabIndex = 20;
			// 
			// labelControlTransactionId
			// 
			this.labelControlTransactionId.Location = new System.Drawing.Point(11, 31);
			this.labelControlTransactionId.Name = "labelControlTransactionId";
			this.labelControlTransactionId.Size = new System.Drawing.Size(73, 13);
			this.labelControlTransactionId.TabIndex = 19;
			this.labelControlTransactionId.Text = "Transaction Id:";
			// 
			// editorStatus
			// 
			this.editorStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.editorStatus.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
			this.editorStatus.Location = new System.Drawing.Point(90, 106);
			this.editorStatus.Name = "editorStatus";
			this.editorStatus.Properties.ReadOnly = true;
			this.editorStatus.Size = new System.Drawing.Size(387, 20);
			this.editorStatus.TabIndex = 22;
			// 
			// labelControlStatus
			// 
			this.labelControlStatus.Location = new System.Drawing.Point(11, 109);
			this.labelControlStatus.Name = "labelControlStatus";
			this.labelControlStatus.Size = new System.Drawing.Size(35, 13);
			this.labelControlStatus.TabIndex = 21;
			this.labelControlStatus.Text = "Status:";
			// 
			// editorTime
			// 
			this.editorTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.editorTime.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
			this.editorTime.Location = new System.Drawing.Point(90, 54);
			this.editorTime.Name = "editorTime";
			this.editorTime.Properties.ReadOnly = true;
			this.editorTime.Size = new System.Drawing.Size(387, 20);
			this.editorTime.TabIndex = 24;
			// 
			// labelControlTime
			// 
			this.labelControlTime.Location = new System.Drawing.Point(11, 57);
			this.labelControlTime.Name = "labelControlTime";
			this.labelControlTime.Size = new System.Drawing.Size(26, 13);
			this.labelControlTime.TabIndex = 23;
			this.labelControlTime.Text = "Time:";
			// 
			// editorRequestCount
			// 
			this.editorRequestCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.editorRequestCount.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
			this.editorRequestCount.Location = new System.Drawing.Point(90, 80);
			this.editorRequestCount.Name = "editorRequestCount";
			this.editorRequestCount.Properties.ReadOnly = true;
			this.editorRequestCount.Size = new System.Drawing.Size(387, 20);
			this.editorRequestCount.TabIndex = 26;
			// 
			// labelControlRequestCount
			// 
			this.labelControlRequestCount.Location = new System.Drawing.Point(11, 83);
			this.labelControlRequestCount.Name = "labelControlRequestCount";
			this.labelControlRequestCount.Size = new System.Drawing.Size(76, 13);
			this.labelControlRequestCount.TabIndex = 25;
			this.labelControlRequestCount.Text = "Request Count:";
			// 
			// SystemEditPanelTransaction
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Name = "SystemEditPanelTransaction";
			((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.tabPageObjectName.ResumeLayout(false);
			this.tabPageObjectName.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorTransactionId.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorStatus.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorTime.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorRequestCount.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraEditors.TextEdit editorTransactionId;
		private DevExpress.XtraEditors.LabelControl labelControlTransactionId;
		private DevExpress.XtraEditors.TextEdit editorStatus;
		private DevExpress.XtraEditors.LabelControl labelControlStatus;
		private DevExpress.XtraEditors.TextEdit editorTime;
		private DevExpress.XtraEditors.LabelControl labelControlTime;
		private DevExpress.XtraEditors.TextEdit editorRequestCount;
		private DevExpress.XtraEditors.LabelControl labelControlRequestCount;
	}
}
