namespace Simple.Datastore.Controls
{
    partial class RestoreDatabaseForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestoreDatabaseForm));
			this.openBakFile = new System.Windows.Forms.OpenFileDialog();
			this.simpleButtonRestore = new DevExpress.XtraEditors.SimpleButton();
			this.checkEditReplaceDatabase = new DevExpress.XtraEditors.CheckEdit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlFile)).BeginInit();
			this.groupControlFile.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.textEditFilePath.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlSqlServer)).BeginInit();
			this.groupControlSqlServer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSqlServer.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlAuthentication)).BeginInit();
			this.groupControlAuthentication.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditUsername.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlDatabase)).BeginInit();
			this.groupControlDatabase.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDatabase.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditReplaceDatabase.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// groupControlFile
			// 
			this.groupControlFile.Location = new System.Drawing.Point(8, 302);
			this.groupControlFile.Size = new System.Drawing.Size(377, 84);
			// 
			// simpleButtonBrowseFilePath
			// 
			this.simpleButtonBrowseFilePath.Location = new System.Drawing.Point(300, 54);
			// 
			// textEditFilePath
			// 
			this.textEditFilePath.Size = new System.Drawing.Size(356, 20);
			// 
			// groupControlSqlServer
			// 
			this.groupControlSqlServer.Size = new System.Drawing.Size(377, 60);
			// 
			// comboBoxEditSqlServer
			// 
			this.comboBoxEditSqlServer.Size = new System.Drawing.Size(356, 20);
			// 
			// groupControlAuthentication
			// 
			this.groupControlAuthentication.Location = new System.Drawing.Point(8, 76);
			this.groupControlAuthentication.Size = new System.Drawing.Size(377, 156);
			// 
			// textEditPassword
			// 
			this.textEditPassword.Size = new System.Drawing.Size(278, 20);
			// 
			// textEditUsername
			// 
			this.textEditUsername.Size = new System.Drawing.Size(278, 20);
			// 
			// groupControlDatabase
			// 
			this.groupControlDatabase.Location = new System.Drawing.Point(8, 236);
			this.groupControlDatabase.Size = new System.Drawing.Size(377, 62);
			// 
			// comboBoxEditDatabase
			// 
			this.comboBoxEditDatabase.Size = new System.Drawing.Size(356, 20);
			// 
			// simpleButtonClose
			// 
			this.simpleButtonClose.Location = new System.Drawing.Point(296, 401);
			this.simpleButtonClose.TabIndex = 12;
			// 
			// simpleButtonTestConnection
			// 
			this.simpleButtonTestConnection.Location = new System.Drawing.Point(258, 125);
			// 
			// openBakFile
			// 
			this.openBakFile.Filter = "Backup Files (*.bak, *.trn)|*.bak;*.trn|All files (*)|*.*";
			this.openBakFile.Title = "Select Backup File";
			// 
			// simpleButtonRestore
			// 
			this.simpleButtonRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonRestore.Location = new System.Drawing.Point(172, 401);
			this.simpleButtonRestore.Name = "simpleButtonRestore";
			this.simpleButtonRestore.Size = new System.Drawing.Size(102, 23);
			this.simpleButtonRestore.TabIndex = 11;
			this.simpleButtonRestore.Text = "Restore";
			this.simpleButtonRestore.Click += new System.EventHandler(this.simpleButtonRestore_Click);
			// 
			// checkEditReplaceDatabase
			// 
			this.checkEditReplaceDatabase.Location = new System.Drawing.Point(18, 402);
			this.checkEditReplaceDatabase.Name = "checkEditReplaceDatabase";
			this.checkEditReplaceDatabase.Properties.AutoWidth = true;
			this.checkEditReplaceDatabase.Properties.Caption = "Replace Database";
			this.checkEditReplaceDatabase.Size = new System.Drawing.Size(110, 20);
			this.checkEditReplaceDatabase.TabIndex = 10;
			// 
			// RestoreDatabaseForm
			// 
			this.AcceptButton = this.simpleButtonRestore;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(395, 436);
			this.Controls.Add(this.checkEditReplaceDatabase);
			this.Controls.Add(this.simpleButtonRestore);
			this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("RestoreDatabaseForm.IconOptions.Icon")));
			this.Name = "RestoreDatabaseForm";
			this.Text = "Restore Database";
			this.Controls.SetChildIndex(this.simpleButtonClose, 0);
			this.Controls.SetChildIndex(this.groupControlAuthentication, 0);
			this.Controls.SetChildIndex(this.groupControlDatabase, 0);
			this.Controls.SetChildIndex(this.groupControlSqlServer, 0);
			this.Controls.SetChildIndex(this.groupControlFile, 0);
			this.Controls.SetChildIndex(this.simpleButtonRestore, 0);
			this.Controls.SetChildIndex(this.checkEditReplaceDatabase, 0);
			((System.ComponentModel.ISupportInitialize)(this.groupControlFile)).EndInit();
			this.groupControlFile.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.textEditFilePath.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlSqlServer)).EndInit();
			this.groupControlSqlServer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSqlServer.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlAuthentication)).EndInit();
			this.groupControlAuthentication.ResumeLayout(false);
			this.groupControlAuthentication.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditUsername.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlDatabase)).EndInit();
			this.groupControlDatabase.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDatabase.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditReplaceDatabase.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.OpenFileDialog openBakFile;
        protected DevExpress.XtraEditors.SimpleButton simpleButtonRestore;
        protected DevExpress.XtraEditors.CheckEdit checkEditReplaceDatabase;
    }
}