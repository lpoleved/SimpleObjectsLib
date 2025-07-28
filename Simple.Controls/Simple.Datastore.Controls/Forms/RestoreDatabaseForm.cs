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
    public partial class RestoreDatabaseForm : RestoreOrBackupDatabaseFormBase
    {
        public RestoreDatabaseForm()
        {
            InitializeComponent();
			this.RestoreFolder = System.IO.Directory.GetCurrentDirectory() + "\\Datastore";
        }

        public string RestoreFolder { get; set; }
        
        protected override void OnButtonBrowseFilePathClick()
        {
            DialogResult dialogResult = this.openBakFile.ShowDialog();

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
                this.FilePath.Text = this.openBakFile.FileName;
        }

        private void simpleButtonRestore_Click(object sender, EventArgs e)
        {
            Cursor curentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                ServerConnection serverConnection = this.CreateServerConnection(includeDatabase: false);
                Server sqlServer = new Server(serverConnection);
                SqlServerHelper.SqlRestoreDatabase restoreDatabase = new SqlServerHelper.SqlRestoreDatabase();

				restoreDatabase.Complete += RestoreDatabase_Complete;
                restoreDatabase.Restore(sqlServer, this.comboBoxEditDatabase.Text, this.FilePath.Text, this.RestoreFolder, this.checkEditReplaceDatabase.Checked);

                //XtraMessageBox.Show("Restore completed.", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

		private void RestoreDatabase_Complete(object sender, ServerMessageEventArgs e)
		{
            XtraMessageBox.Show(e.Error.Message, "Restore Database Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
