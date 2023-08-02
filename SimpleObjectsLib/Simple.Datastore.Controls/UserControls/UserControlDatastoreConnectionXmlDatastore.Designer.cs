namespace Simple.Datastore.Controls
{
    partial class UserControlDatastoreConnectionXmlDatastore
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
			this.buttonSelectDatastoreFolder = new DevExpress.XtraEditors.SimpleButton();
			this.TextBoxDatastoreFolder = new DevExpress.XtraEditors.TextEdit();
			this.labelDatastoreFolder = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.TextBoxDatastoreFolder.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonSelectDatastoreFolder
			// 
			this.buttonSelectDatastoreFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSelectDatastoreFolder.Location = new System.Drawing.Point(254, 61);
			this.buttonSelectDatastoreFolder.Name = "buttonSelectDatastoreFolder";
			this.buttonSelectDatastoreFolder.Size = new System.Drawing.Size(75, 23);
			this.buttonSelectDatastoreFolder.TabIndex = 5;
			this.buttonSelectDatastoreFolder.Text = "Select";
			this.buttonSelectDatastoreFolder.Click += new System.EventHandler(this.buttonSelectDatastoreFolder_Click);
			// 
			// TextBoxDatastoreFolder
			// 
			this.TextBoxDatastoreFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxDatastoreFolder.Location = new System.Drawing.Point(12, 35);
			this.TextBoxDatastoreFolder.Name = "TextBoxDatastoreFolder";
			this.TextBoxDatastoreFolder.Size = new System.Drawing.Size(317, 20);
			this.TextBoxDatastoreFolder.TabIndex = 4;
			// 
			// labelDatastoreFolder
			// 
			this.labelDatastoreFolder.Location = new System.Drawing.Point(12, 16);
			this.labelDatastoreFolder.Name = "labelDatastoreFolder";
			this.labelDatastoreFolder.Size = new System.Drawing.Size(107, 13);
			this.labelDatastoreFolder.TabIndex = 3;
			this.labelDatastoreFolder.Text = "XML Datastore Folder:";
			// 
			// UserControlDatastoreConnectionXmlDatastore
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.buttonSelectDatastoreFolder);
			this.Controls.Add(this.TextBoxDatastoreFolder);
			this.Controls.Add(this.labelDatastoreFolder);
			this.Name = "UserControlDatastoreConnectionXmlDatastore";
			this.Size = new System.Drawing.Size(339, 138);
			((System.ComponentModel.ISupportInitialize)(this.TextBoxDatastoreFolder.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton buttonSelectDatastoreFolder;
		private DevExpress.XtraEditors.LabelControl labelDatastoreFolder;

		public DevExpress.XtraEditors.TextEdit TextBoxDatastoreFolder;
    }
}
