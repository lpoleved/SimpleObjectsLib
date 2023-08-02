namespace Simple.Objects.Controls
{
    partial class EditPanelName
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
			this.groupControlGeneral = new DevExpress.XtraEditors.GroupControl();
			this.labelName = new DevExpress.XtraEditors.LabelControl();
			this.editorName = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
			this.tabControl.SuspendLayout();
			this.tabPageObjectName.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlGeneral)).BeginInit();
			this.groupControlGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorName.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Size = new System.Drawing.Size(490, 662);
			// 
			// tabPageObjectName
			// 
			this.tabPageObjectName.Controls.Add(this.groupControlGeneral);
			this.tabPageObjectName.Size = new System.Drawing.Size(488, 637);
			// 
			// groupControlGeneral
			// 
			this.groupControlGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupControlGeneral.Controls.Add(this.labelName);
			this.groupControlGeneral.Controls.Add(this.editorName);
			this.groupControlGeneral.Location = new System.Drawing.Point(3, 4);
			this.groupControlGeneral.Name = "groupControlGeneral";
			this.groupControlGeneral.Size = new System.Drawing.Size(482, 630);
			this.groupControlGeneral.TabIndex = 1;
			this.groupControlGeneral.Text = "General";
			// 
			// labelName
			// 
			this.labelName.Location = new System.Drawing.Point(16, 38);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(31, 13);
			this.labelName.TabIndex = 2;
			this.labelName.Text = "Name:";
			// 
			// editorName
			// 
			this.editorName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.errorProvider.SetError(this.editorName, "OK");
			this.errorProvider.SetErrorType(this.editorName, DevExpress.XtraEditors.DXErrorProvider.ErrorType.Default);
			this.errorProvider.SetIconAlignment(this.editorName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.editorName.Location = new System.Drawing.Point(53, 35);
			this.editorName.Name = "editorName";
			this.editorName.Size = new System.Drawing.Size(412, 20);
			this.editorName.TabIndex = 3;
			// 
			// EditPanelName
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "EditPanelName";
			((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.tabPageObjectName.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlGeneral)).EndInit();
			this.groupControlGeneral.ResumeLayout(false);
			this.groupControlGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorName.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraEditors.GroupControl groupControlGeneral;
        protected DevExpress.XtraEditors.LabelControl labelName;
        protected DevExpress.XtraEditors.TextEdit editorName;
    }
}
