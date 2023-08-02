using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Simple.Datastore.SqlServer;

namespace Simple.Datastore.Controls
{
    public partial class UserControlDatastoreConnectionSqlServer : XtraUserControl
    {
        public UserControlDatastoreConnectionSqlServer()
        {
            InitializeComponent();

            this.CheckButtonConnectByConnectionString.Checked = false;
            this.TextEditNetworkPort.Text = "1433";
            this.SetControlsEnableProperty();
        }

        private void checkBoxConnectByConnectionString_CheckedChanged(object sender, EventArgs e)
        {
            this.SetControlsEnableProperty();
        }

        private void CheckBoxNetworkConnection_CheckedChanged(object sender, EventArgs e)
        {
            this.SetControlsEnablePropertyByNetworkConnection();
        }

        private void SetControlsEnableProperty()
        {
			this.labelServer.Enabled = !this.CheckButtonConnectByConnectionString.Checked;
			this.ComboBoxServer.Enabled = !this.CheckButtonConnectByConnectionString.Checked;
			this.labelDatabase.Enabled = !this.CheckButtonConnectByConnectionString.Checked;
			this.ComboBoxDatabase.Enabled = !this.CheckButtonConnectByConnectionString.Checked;
            this.CheckButtonNetworkConnection.Enabled = !this.CheckButtonConnectByConnectionString.Checked;
			this.TextEditConnectionString.Enabled = this.CheckButtonConnectByConnectionString.Checked;

            this.SetControlsEnablePropertyByNetworkConnection();
        }

        private void SetControlsEnablePropertyByNetworkConnection()
        {
			this.TextEditNetworkPort.Enabled = this.CheckButtonNetworkConnection.Checked && !this.CheckButtonConnectByConnectionString.Checked;
			this.labelPort.Enabled = this.TextEditNetworkPort.Enabled;
        }

		private void ComboBoxSqlServer_MouseDown(object sender, MouseEventArgs e)
		{
			if (this.ComboBoxServer.Properties.Items.Count > 0)
				return;

			Cursor curentCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;

			this.PopulateServersComboBox(ref this.ComboBoxServer);

			Cursor.Current = curentCursor;
		}

		private void ComboBoxDatabase_MouseDown(object sender, MouseEventArgs e)
		{
			//if (this.ComboBoxDatabase.Items.Count > 0)
			//    return;

			//if (this.ComboBoxSqlServer.SelectedIndex == 0)
			//    return;

			if (this.ComboBoxServer.Text == null || this.ComboBoxServer.Text.Trim().Length == 0)
				return;

			Cursor? curentCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;

            string sqlConnectionString = String.Format("Data Source={0}; Integrated Security=SSPI; TrustServerCertificate=true;", this.ComboBoxServer.Text); // Integrated Security=True;

            this.PopulateDatabasesComboBox(sqlConnectionString, ref this.ComboBoxDatabase);

			Cursor.Current = curentCursor;
		}

        private void PopulateServersComboBox(ref ComboBoxEdit comboBoxServers)
        {
            IEnumerable<string> serverNames;

            try
            {
                comboBoxServers.Properties.Items.Clear();

                serverNames = SqlServerHelper.GetRegisteredServerNames();

                foreach (string serverName in serverNames)
                    comboBoxServers.Properties.Items.Add(serverName);

                // Default to default instance on this machine 
                comboBoxServers.SelectedIndex = comboBoxServers.Properties.Items.IndexOf(System.Environment.MachineName);

                // If this machine is not a SQL server 
                // then select the first server in the list
                if (comboBoxServers.SelectedIndex < 0)
                    comboBoxServers.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                this.DisplayErrorMessage(ex);
            }
        }

        private void PopulateDatabasesComboBox(string sqlConnectionString, ref ComboBoxEdit comboBoxDatabases)
        {
            IEnumerable<string> databaseNames;

            try
            {
                //string comboBoxDatabasesText = comboBoxDatabases.Text; 
                comboBoxDatabases.Properties.Items.Clear();

                databaseNames = SqlServerHelper.GetDatabaseNames(sqlConnectionString);

                foreach (string databaseName in databaseNames)
                    comboBoxDatabases.Properties.Items.Add(databaseName);

                //// Default to default instance on this machine 
                //comboBoxServers.SelectedIndex = comboBoxServers.FindStringExact(System.Environment.MachineName);

                //// If not selected, select the first database in the list
                //if (comboBoxDatabases.SelectedIndex < 0)
                //    comboBoxDatabases.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                this.DisplayErrorMessage(ex);
            }
        }

        // generic error display
        protected void DisplayErrorMessage(Exception ex)
        {
            string errorMessage;

            errorMessage = ex.Message;
            while (ex.InnerException != null)
            {
                errorMessage += Environment.NewLine + ex.InnerException.Message;
                ex = ex.InnerException;
            }

            XtraMessageBox.Show(this, errorMessage, this.Text + @" - Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }
	}
}
