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
			editorTransactionId = new DevExpress.XtraEditors.TextEdit();
			labelControlTransactionId = new DevExpress.XtraEditors.LabelControl();
			editorStatus = new DevExpress.XtraEditors.TextEdit();
			labelControlStatus = new DevExpress.XtraEditors.LabelControl();
			editorTime = new DevExpress.XtraEditors.TextEdit();
			labelControlTime = new DevExpress.XtraEditors.LabelControl();
			editorRequestCount = new DevExpress.XtraEditors.TextEdit();
			labelControlRequestCount = new DevExpress.XtraEditors.LabelControl();
			editorErrorDescription = new DevExpress.XtraEditors.TextEdit();
			labelControlErrorDescription = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)tabControl).BeginInit();
			tabControl.SuspendLayout();
			tabPageObjectName.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorTransactionId.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorStatus.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorTime.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorRequestCount.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)editorErrorDescription.Properties).BeginInit();
			SuspendLayout();
			// 
			// tabPageObjectName
			// 
			tabPageObjectName.Controls.Add(editorErrorDescription);
			tabPageObjectName.Controls.Add(labelControlErrorDescription);
			tabPageObjectName.Controls.Add(editorRequestCount);
			tabPageObjectName.Controls.Add(labelControlRequestCount);
			tabPageObjectName.Controls.Add(editorTime);
			tabPageObjectName.Controls.Add(labelControlTime);
			tabPageObjectName.Controls.Add(editorStatus);
			tabPageObjectName.Controls.Add(labelControlStatus);
			tabPageObjectName.Controls.Add(editorTransactionId);
			tabPageObjectName.Controls.Add(labelControlTransactionId);
			tabPageObjectName.Text = "Transaction";
			// 
			// editorTransactionId
			// 
			editorTransactionId.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			editorTransactionId.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
			editorTransactionId.Location = new System.Drawing.Point(90, 28);
			editorTransactionId.Name = "editorTransactionId";
			editorTransactionId.Properties.ReadOnly = true;
			editorTransactionId.Size = new System.Drawing.Size(387, 20);
			editorTransactionId.TabIndex = 20;
			// 
			// labelControlTransactionId
			// 
			labelControlTransactionId.Location = new System.Drawing.Point(11, 31);
			labelControlTransactionId.Name = "labelControlTransactionId";
			labelControlTransactionId.Size = new System.Drawing.Size(73, 13);
			labelControlTransactionId.TabIndex = 19;
			labelControlTransactionId.Text = "Transaction Id:";
			// 
			// editorStatus
			// 
			editorStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			editorStatus.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
			editorStatus.Location = new System.Drawing.Point(90, 106);
			editorStatus.Name = "editorStatus";
			editorStatus.Properties.ReadOnly = true;
			editorStatus.Size = new System.Drawing.Size(387, 20);
			editorStatus.TabIndex = 22;
			// 
			// labelControlStatus
			// 
			labelControlStatus.Location = new System.Drawing.Point(11, 109);
			labelControlStatus.Name = "labelControlStatus";
			labelControlStatus.Size = new System.Drawing.Size(35, 13);
			labelControlStatus.TabIndex = 21;
			labelControlStatus.Text = "Status:";
			// 
			// editorTime
			// 
			editorTime.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			editorTime.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
			editorTime.Location = new System.Drawing.Point(90, 54);
			editorTime.Name = "editorTime";
			editorTime.Properties.ReadOnly = true;
			editorTime.Size = new System.Drawing.Size(387, 20);
			editorTime.TabIndex = 24;
			// 
			// labelControlTime
			// 
			labelControlTime.Location = new System.Drawing.Point(11, 57);
			labelControlTime.Name = "labelControlTime";
			labelControlTime.Size = new System.Drawing.Size(26, 13);
			labelControlTime.TabIndex = 23;
			labelControlTime.Text = "Time:";
			// 
			// editorRequestCount
			// 
			editorRequestCount.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			editorRequestCount.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
			editorRequestCount.Location = new System.Drawing.Point(90, 80);
			editorRequestCount.Name = "editorRequestCount";
			editorRequestCount.Properties.ReadOnly = true;
			editorRequestCount.Size = new System.Drawing.Size(387, 20);
			editorRequestCount.TabIndex = 26;
			// 
			// labelControlRequestCount
			// 
			labelControlRequestCount.Location = new System.Drawing.Point(11, 83);
			labelControlRequestCount.Name = "labelControlRequestCount";
			labelControlRequestCount.Size = new System.Drawing.Size(76, 13);
			labelControlRequestCount.TabIndex = 25;
			labelControlRequestCount.Text = "Request Count:";
			// 
			// editorErrorDescription
			// 
			editorErrorDescription.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			editorErrorDescription.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
			editorErrorDescription.Location = new System.Drawing.Point(90, 132);
			editorErrorDescription.Name = "editorErrorDescription";
			editorErrorDescription.Properties.ReadOnly = true;
			editorErrorDescription.Size = new System.Drawing.Size(387, 20);
			editorErrorDescription.TabIndex = 28;
			// 
			// labelControlErrorDescription
			// 
			labelControlErrorDescription.Location = new System.Drawing.Point(11, 135);
			labelControlErrorDescription.Name = "labelControlErrorDescription";
			labelControlErrorDescription.Size = new System.Drawing.Size(35, 13);
			labelControlErrorDescription.TabIndex = 27;
			labelControlErrorDescription.Text = "Status:";
			// 
			// SystemEditPanelTransaction
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			Name = "SystemEditPanelTransaction";
			((System.ComponentModel.ISupportInitialize)tabControl).EndInit();
			tabControl.ResumeLayout(false);
			tabPageObjectName.ResumeLayout(false);
			tabPageObjectName.PerformLayout();
			((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
			((System.ComponentModel.ISupportInitialize)editorTransactionId.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorStatus.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorTime.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorRequestCount.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)editorErrorDescription.Properties).EndInit();
			ResumeLayout(false);
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
		private DevExpress.XtraEditors.TextEdit editorErrorDescription;
		private DevExpress.XtraEditors.LabelControl labelControlErrorDescription;
	}
}
