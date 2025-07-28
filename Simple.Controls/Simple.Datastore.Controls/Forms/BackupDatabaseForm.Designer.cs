namespace Simple.Datastore.Controls
{
    partial class BackupDatabaseForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupDatabaseForm));
			this.saveBakFile = new System.Windows.Forms.SaveFileDialog();
			this.simpleButtonBackup = new DevExpress.XtraEditors.SimpleButton();
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
			this.SuspendLayout();
			// 
			// groupControlFile
			// 
			this.groupControlFile.Location = new System.Drawing.Point(8, 308);
			this.groupControlFile.Size = new System.Drawing.Size(377, 81);
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
			this.groupControlDatabase.Size = new System.Drawing.Size(377, 63);
			// 
			// comboBoxEditDatabase
			// 
			this.comboBoxEditDatabase.Size = new System.Drawing.Size(356, 20);
			// 
			// simpleButtonClose
			// 
			this.simpleButtonClose.Location = new System.Drawing.Point(294, 412);
			this.simpleButtonClose.TabIndex = 11;
			// 
			// simpleButtonTestConnection
			// 
			this.simpleButtonTestConnection.Location = new System.Drawing.Point(258, 125);
			// 
			// saveBakFile
			// 
			this.saveBakFile.Filter = "Backup Files (*.bak, *.trn)|*.bak;*.trn|All files (*)|*.*";
			this.saveBakFile.Title = "Select file for saving backup";
			// 
			// simpleButtonBackup
			// 
			this.simpleButtonBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonBackup.Location = new System.Drawing.Point(169, 412);
			this.simpleButtonBackup.Name = "simpleButtonBackup";
			this.simpleButtonBackup.Size = new System.Drawing.Size(102, 23);
			this.simpleButtonBackup.TabIndex = 10;
			this.simpleButtonBackup.Text = "Backup";
			this.simpleButtonBackup.Click += new System.EventHandler(this.simpleButtonBackup_Click);
			// 
			// BackupDatabaseForm
			// 
			this.AcceptButton = this.simpleButtonBackup;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(395, 447);
			this.Controls.Add(this.simpleButtonBackup);
			this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("BackupDatabaseForm.IconOptions.Icon")));
			this.Name = "BackupDatabaseForm";
			this.Text = "Backup Database";
			this.Controls.SetChildIndex(this.simpleButtonClose, 0);
			this.Controls.SetChildIndex(this.groupControlAuthentication, 0);
			this.Controls.SetChildIndex(this.groupControlDatabase, 0);
			this.Controls.SetChildIndex(this.groupControlSqlServer, 0);
			this.Controls.SetChildIndex(this.groupControlFile, 0);
			this.Controls.SetChildIndex(this.simpleButtonBackup, 0);
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
			this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.SaveFileDialog saveBakFile;
        protected DevExpress.XtraEditors.SimpleButton simpleButtonBackup;
    }
}