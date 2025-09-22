namespace Simple.Objects.Controls
{
    partial class EditPanelNameDescription
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
			this.tabPageDescription = new DevExpress.XtraTab.XtraTabPage();
			this.editorDescription = new DevExpress.XtraEditors.MemoEdit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlGeneral)).BeginInit();
			this.groupControlGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
			this.tabControl.SuspendLayout();
			this.tabPageObjectName.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.tabPageDescription.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorDescription.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// groupControlGeneral
			// 
			this.groupControlGeneral.Size = new System.Drawing.Size(482, 630);
			// 
			// labelName
			// 
			this.labelName.Location = new System.Drawing.Point(16, 37);
			// 
			// editorName
			// 
			this.errorProvider.SetError(this.editorName, "OK");
			this.errorProvider.SetErrorType(this.editorName, DevExpress.XtraEditors.DXErrorProvider.ErrorType.Default);
			this.errorProvider.SetIconAlignment(this.editorName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.editorName.Location = new System.Drawing.Point(53, 34);
			this.editorName.Size = new System.Drawing.Size(408, 20);
			// 
			// tabControl
			// 
			this.tabControl.Size = new System.Drawing.Size(490, 662);
			this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageDescription});
			this.tabControl.Controls.SetChildIndex(this.tabPageDescription, 0);
			this.tabControl.Controls.SetChildIndex(this.tabPageObjectName, 0);
			// 
			// tabPageObjectName
			// 
			this.tabPageObjectName.Size = new System.Drawing.Size(488, 637);
			// 
			// tabPageDescription
			// 
			this.tabPageDescription.Controls.Add(this.editorDescription);
			this.tabPageDescription.Name = "tabPageDescription";
			this.tabPageDescription.Size = new System.Drawing.Size(488, 637);
			this.tabPageDescription.Text = "Description";
			// 
			// editorDescription
			// 
			this.editorDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.errorProvider.SetError(this.editorDescription, "OK");
			this.errorProvider.SetErrorType(this.editorDescription, DevExpress.XtraEditors.DXErrorProvider.ErrorType.Default);
			this.errorProvider.SetIconAlignment(this.editorDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.editorDescription.Location = new System.Drawing.Point(3, 4);
			this.editorDescription.Name = "editorDescription";
			this.editorDescription.Properties.MaxLength = 2000;
			this.editorDescription.Size = new System.Drawing.Size(482, 631);
			this.editorDescription.TabIndex = 2;
			// 
			// EditPanelNameDescription
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "EditPanelNameDescription";
			((System.ComponentModel.ISupportInitialize)(this.groupControlGeneral)).EndInit();
			this.groupControlGeneral.ResumeLayout(false);
			this.groupControlGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.tabPageObjectName.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.tabPageDescription.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.editorDescription.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraTab.XtraTabPage tabPageDescription;
        protected DevExpress.XtraEditors.MemoEdit editorDescription;
    }
}
