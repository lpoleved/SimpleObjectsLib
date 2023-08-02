namespace Simple.Objects.Controls
{
    partial class EditPanelFolder
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
			this.tabPageDescription.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorDescription.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlGeneral)).BeginInit();
			this.groupControlGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
			this.tabControl.SuspendLayout();
			this.tabPageObjectName.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// tabPageDescription
			// 
			this.tabPageDescription.Size = new System.Drawing.Size(488, 637);
			// 
			// editorDescription
			// 
			this.errorProvider.SetError(this.editorDescription, "OK");
			this.errorProvider.SetErrorType(this.editorDescription, DevExpress.XtraEditors.DXErrorProvider.ErrorType.Default);
			this.errorProvider.SetIconAlignment(this.editorDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.editorDescription.Size = new System.Drawing.Size(482, 630);
			// 
			// groupControlGeneral
			// 
			this.groupControlGeneral.Size = new System.Drawing.Size(482, 630);
			// 
			// editorName
			// 
			this.errorProvider.SetError(this.editorName, "OK");
			this.errorProvider.SetErrorType(this.editorName, DevExpress.XtraEditors.DXErrorProvider.ErrorType.Default);
			this.errorProvider.SetIconAlignment(this.editorName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.editorName.Size = new System.Drawing.Size(412, 20);
			// 
			// tabControl
			// 
			this.tabControl.SelectedTabPage = this.tabPageObjectName;
			this.tabControl.Size = new System.Drawing.Size(490, 662);
			// 
			// tabPageObjectName
			// 
			this.tabPageObjectName.Size = new System.Drawing.Size(488, 637);
			this.tabPageObjectName.Text = "Folder";
			// 
			// EditPanelFolder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Name = "EditPanelFolder";
			this.tabPageDescription.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.editorDescription.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlGeneral)).EndInit();
			this.groupControlGeneral.ResumeLayout(false);
			this.groupControlGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.tabPageObjectName.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

    }
}
