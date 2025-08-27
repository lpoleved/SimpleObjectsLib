namespace Simple.Datastore.Controls
{
    partial class RestoreOrBackupDatabaseFormBase
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestoreOrBackupDatabaseFormBase));
			this.groupControlFile = new DevExpress.XtraEditors.GroupControl();
			this.textEditFilePath = new DevExpress.XtraEditors.TextEdit();
			this.simpleButtonBrowseFilePath = new DevExpress.XtraEditors.SimpleButton();
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
			((System.ComponentModel.ISupportInitialize)(this.groupControlFile)).BeginInit();
			this.groupControlFile.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.textEditFilePath.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// radioButtonWindowsAuthentication
			// 
			this.radioButtonWindowsAuthentication.TabIndex = 100;
			// 
			// groupControlSqlServer
			// 
			this.groupControlSqlServer.Size = new System.Drawing.Size(348, 60);
			// 
			// comboBoxEditSqlServer
			// 
			this.comboBoxEditSqlServer.Size = new System.Drawing.Size(327, 20);
			// 
			// groupControlAuthentication
			// 
			this.groupControlAuthentication.Location = new System.Drawing.Point(8, 77);
			this.groupControlAuthentication.Size = new System.Drawing.Size(348, 156);
			// 
			// textEditPassword
			// 
			this.textEditPassword.Size = new System.Drawing.Size(249, 20);
			// 
			// textEditUsername
			// 
			this.textEditUsername.Size = new System.Drawing.Size(249, 20);
			// 
			// groupControlDatabase
			// 
			this.groupControlDatabase.Location = new System.Drawing.Point(8, 238);
			this.groupControlDatabase.Size = new System.Drawing.Size(348, 61);
			// 
			// comboBoxEditDatabase
			// 
			this.comboBoxEditDatabase.Size = new System.Drawing.Size(327, 20);
			// 
			// simpleButtonClose
			// 
			this.simpleButtonClose.Location = new System.Drawing.Point(267, 410);
			this.simpleButtonClose.TabIndex = 10;
			// 
			// simpleButtonTestConnection
			// 
			this.simpleButtonTestConnection.Location = new System.Drawing.Point(229, 125);
			// 
			// groupControlFile
			// 
			this.groupControlFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupControlFile.Controls.Add(this.textEditFilePath);
			this.groupControlFile.Controls.Add(this.simpleButtonBrowseFilePath);
			this.groupControlFile.Location = new System.Drawing.Point(8, 304);
			this.groupControlFile.Name = "groupControlFile";
			this.groupControlFile.Size = new System.Drawing.Size(348, 86);
			this.groupControlFile.TabIndex = 24;
			this.groupControlFile.Text = "File";
			// 
			// textEditFilePath
			// 
			this.textEditFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditFilePath.Location = new System.Drawing.Point(10, 29);
			this.textEditFilePath.Name = "textEditFilePath";
			this.textEditFilePath.Size = new System.Drawing.Size(328, 20);
			this.textEditFilePath.TabIndex = 8;
			// 
			// simpleButtonBrowseFilePath
			// 
			this.simpleButtonBrowseFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonBrowseFilePath.Location = new System.Drawing.Point(271, 55);
			this.simpleButtonBrowseFilePath.Name = "simpleButtonBrowseFilePath";
			this.simpleButtonBrowseFilePath.Size = new System.Drawing.Size(66, 19);
			this.simpleButtonBrowseFilePath.TabIndex = 9;
			this.simpleButtonBrowseFilePath.Text = "Browse...";
			this.simpleButtonBrowseFilePath.Click += new System.EventHandler(this.simpleButtonBrowseFilePath_Click);
			// 
			// RestoreOrBackupDatabaseFormBase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(364, 445);
			this.Controls.Add(this.groupControlFile);
			this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("RestoreOrBackupDatabaseFormBase.IconOptions.Icon")));
			this.Name = "RestoreOrBackupDatabaseFormBase";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Restore or Backup Datastore";
			this.Controls.SetChildIndex(this.simpleButtonClose, 0);
			this.Controls.SetChildIndex(this.groupControlAuthentication, 0);
			this.Controls.SetChildIndex(this.groupControlDatabase, 0);
			this.Controls.SetChildIndex(this.groupControlSqlServer, 0);
			this.Controls.SetChildIndex(this.groupControlFile, 0);
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
			((System.ComponentModel.ISupportInitialize)(this.groupControlFile)).EndInit();
			this.groupControlFile.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.textEditFilePath.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraEditors.GroupControl groupControlFile;
        protected DevExpress.XtraEditors.SimpleButton simpleButtonBrowseFilePath;
        protected DevExpress.XtraEditors.TextEdit textEditFilePath;
    }
}