using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using DevExpress.XtraEditors;
using Simple.Datastore.SqlServer;

namespace Simple.Datastore.Controls
{
    public partial class BackupDatabaseForm : RestoreOrBackupDatabaseFormBase
    {
        public BackupDatabaseForm()
        {
            InitializeComponent();
        }

        protected override void OnButtonBrowseFilePathClick()
        {
            DialogResult dialogResult = this.saveBakFile.ShowDialog();

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
                this.FilePath.Text = this.saveBakFile.FileName;
        }

        private void simpleButtonBackup_Click(object sender, EventArgs e)
        {
            Cursor? curentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                ServerConnection serverConnection = this.CreateServerConnection(includeDatabase: false);
                Server sqlServer = new Server(serverConnection);

                SqlServerHelper.SqlBackupDatabase backupDatabase = new SqlServerHelper.SqlBackupDatabase();

                backupDatabase.Complete += BackupDatabase_Complete;
                backupDatabase.Backup(sqlServer, this.comboBoxEditDatabase.Text, this.FilePath.Text);

                //XtraMessageBox.Show("Backup completed.", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Close();
            }
            catch (Exception ex)
            {
                Cursor.Current = curentCursor;
                
                this.DisplayErrorMessage(ex);
            }
            finally
            {
                Cursor.Current = curentCursor;
            }
        }

		private void BackupDatabase_Complete(object? sender, ServerMessageEventArgs e)
		{
            XtraMessageBox.Show(e.Error.Message, "Backup Database Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
