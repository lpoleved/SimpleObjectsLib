namespace Simple.Objects.Controls
{
    partial class EditPanelDescription
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
			this.tabPageDescription = new DevExpress.XtraTab.XtraTabPage();
			this.editorDescription = new DevExpress.XtraEditors.MemoEdit();
			((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
			this.tabControl.SuspendLayout();
			this.tabPageObjectName.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlGeneral)).BeginInit();
			this.tabPageDescription.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorDescription.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageDescription});
			this.tabControl.Controls.SetChildIndex(this.tabPageDescription, 0);
			this.tabControl.Controls.SetChildIndex(this.tabPageObjectName, 0);
			// 
			// tabPageObjectName
			// 
			this.tabPageObjectName.Controls.Add(this.groupControlGeneral);
			// 
			// groupControlGeneral
			// 
			this.groupControlGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupControlGeneral.Location = new System.Drawing.Point(3, 4);
			this.groupControlGeneral.Name = "groupControlGeneral";
			this.groupControlGeneral.Size = new System.Drawing.Size(482, 630);
			this.groupControlGeneral.TabIndex = 1;
			this.groupControlGeneral.Text = "General";
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
			this.editorDescription.Location = new System.Drawing.Point(3, 3);
			this.editorDescription.Name = "editorDescription";
			this.editorDescription.Properties.MaxLength = 2000;
			this.editorDescription.Size = new System.Drawing.Size(482, 631);
			this.editorDescription.TabIndex = 3;
			// 
			// EditPanelDescription
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "EditPanelDescription";
			((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.tabPageObjectName.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlGeneral)).EndInit();
			this.tabPageDescription.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.editorDescription.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraEditors.GroupControl groupControlGeneral;
		protected DevExpress.XtraTab.XtraTabPage tabPageDescription;
		protected DevExpress.XtraEditors.MemoEdit editorDescription;
	}
}
