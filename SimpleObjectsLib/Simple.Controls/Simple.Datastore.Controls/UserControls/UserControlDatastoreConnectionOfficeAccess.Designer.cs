namespace Simple.Datastore.Controls
{
    partial class UserControlDatastoreConnectionOfficeAccess
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
			this.labelFile = new DevExpress.XtraEditors.LabelControl();
			this.TextEditOfficeAccessFile = new DevExpress.XtraEditors.ComboBoxEdit();
			this.buttonSelectOfficeAccessFile = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.TextEditOfficeAccessFile.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// labelFile
			// 
			this.labelFile.Location = new System.Drawing.Point(12, 16);
			this.labelFile.Name = "labelFile";
			this.labelFile.Size = new System.Drawing.Size(88, 13);
			this.labelFile.TabIndex = 0;
			this.labelFile.Text = "Office Access File:";
			// 
			// textEditOfficeAccessFile
			// 
			this.TextEditOfficeAccessFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextEditOfficeAccessFile.Location = new System.Drawing.Point(12, 35);
			this.TextEditOfficeAccessFile.Name = "textEditOfficeAccessFile";
			this.TextEditOfficeAccessFile.Size = new System.Drawing.Size(315, 20);
			this.TextEditOfficeAccessFile.TabIndex = 1;
			// 
			// buttonSelectOfficeAccessFile
			// 
			this.buttonSelectOfficeAccessFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSelectOfficeAccessFile.Location = new System.Drawing.Point(252, 61);
			this.buttonSelectOfficeAccessFile.Name = "buttonSelectOfficeAccessFile";
			this.buttonSelectOfficeAccessFile.Size = new System.Drawing.Size(75, 23);
			this.buttonSelectOfficeAccessFile.TabIndex = 2;
			this.buttonSelectOfficeAccessFile.Text = "Select";
			this.buttonSelectOfficeAccessFile.Click += new System.EventHandler(this.buttonSelectOfficeAccessFile_Click);
			// 
			// UserControlDatastoreConnectionOfficeAccess
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.buttonSelectOfficeAccessFile);
			this.Controls.Add(this.TextEditOfficeAccessFile);
			this.Controls.Add(this.labelFile);
			this.Name = "UserControlDatastoreConnectionOfficeAccess";
			this.Size = new System.Drawing.Size(339, 138);
			((System.ComponentModel.ISupportInitialize)(this.TextEditOfficeAccessFile.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelFile;
        private DevExpress.XtraEditors.SimpleButton buttonSelectOfficeAccessFile;

		public DevExpress.XtraEditors.ComboBoxEdit TextEditOfficeAccessFile;
	}
}
