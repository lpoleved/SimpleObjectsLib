using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
using Microsoft.Win32;
using Microsoft.SqlServer.Management;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
//using Microsoft.SqlServer.Management.Smo.RegisteredServers;
using DevExpress.XtraEditors;
using Simple.Datastore.SqlServer;

namespace Simple.Datastore.Controls
{
    public partial class ConnectToSqlServerForm : XtraForm
    {
        private bool databaseListRequireRefresh = true;
        private bool isSqlServerListPopulated = false;
        
        public ConnectToSqlServerForm()
        {
            InitializeComponent();

			this.comboBoxEditSqlServer.Text = ".\\SQLEXPRESS";
			this.radioButtonWindowsAuthentication.Checked = true;
			this.sqlServerAuthentication_CheckedChanged(this, EventArgs.Empty);
        }

		//protected override void OnLoad(EventArgs e)
		//{
		//	base.OnLoad(e);
		//	this.radioButtonWindowsAuthentication.Checked = true;
		//}

		public ComboBoxEdit ComboBoxEditSqlServer 
        { 
            get { return this.comboBoxEditSqlServer; }
        }

        public RadioButton RadioButtonSqlServerAuthentication
        {
            get { return this.radioButtonSqlServerAuthentication; }
        }

        public RadioButton RadioButtonWindowsAuthentication
        {
            get { return this.radioButtonWindowsAuthentication; }
        }

        public TextEdit TextEditUsername
        {
            get { return this.textEditUsername; }
        }

        public TextEdit TextEditPassword
        {
            get { return this.textEditPassword; }
        }

        public ComboBoxEdit ComboBoxEditDatabase
        {
            get { return this.comboBoxEditDatabase; }
        }

        public SimpleButton ButtonTestConnection
        {
            get { return this.simpleButtonTestConnection; }
        }

        public SimpleButton ButtonClose
        {
            get { return this.simpleButtonClose; }
        }

        protected ServerConnection CreateServerConnection(bool includeDatabase)
        {
            string connectionString = this.GetConnectionString(includeDatabase);
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            return new ServerConnection(sqlConnection);
        }
        /// <summary>
        /// Gets a connection string using the information currently provided on the form.
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString(bool includeDatabase)
        {
            string connectionString;
            string databaseName = (includeDatabase && !string.IsNullOrEmpty(this.ComboBoxEditDatabase.Text)) ? this.ComboBoxEditDatabase.Text : String.Empty;
            
            if (this.radioButtonWindowsAuthentication.Checked)
                connectionString = SqlServerHelper.GetConnectionString(this.ComboBoxEditSqlServer.Text, databaseName);
            else
                connectionString = SqlServerHelper.GetConnectionString(this.ComboBoxEditSqlServer.Text, databaseName, this.TextEditUsername.Text, this.TextEditPassword.Text);

            //if (this.radioButtonWindowsAuthentication.Checked)
            //    connectionString = string.Format("Server={0}; Integrated Security=SSPI; TrustServerCertificate=true;", this.ComboBoxEditSqlServer.Text); // Integrated Security=SSPI
            //else
            //    connectionString = string.Format("Server={0}; User ID={1}; Password={2};", this.ComboBoxEditSqlServer.Text, this.TextEditUsername.Text, this.TextEditPassword.Text);

            //if (includeDatabase && !string.IsNullOrEmpty(this.ComboBoxEditDatabase.Text))
            //    connectionString += string.Format(" Database={0};", this.ComboBoxEditDatabase.Text);

            return connectionString;
        }

        /// <summary>
        /// Occurs when the authentication type radio buttons 'sqlServerAuthentication' and 'windowsAuthentication' are changed.
        /// </summary>
        private void sqlServerAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            bool usernameAndPasswordEnabled = this.radioButtonSqlServerAuthentication.Checked;

			this.labelControlUsername.Enabled = usernameAndPasswordEnabled;
            this.TextEditUsername.Enabled = usernameAndPasswordEnabled;
            this.labelControlPassword.Enabled = usernameAndPasswordEnabled;
            this.TextEditPassword.Enabled = usernameAndPasswordEnabled;
        }

        private void PopulateServerNames(ComboBoxEdit comboBoxSqlServerName)
        {
            IEnumerable<string> serverNames;
            string currentSqlServerName = comboBoxSqlServerName.Text;

            try
            {
                serverNames = SqlServerHelper.GetRegisteredServerNames();

                comboBoxSqlServerName.Properties.Items.Clear();

                foreach (string serverName in serverNames)
                    comboBoxSqlServerName.Properties.Items.Add(serverName);

                // Default to default instance on this machine 
                //comboBoxServerNames.SelectedIndex = comboBoxServerNames.FindStringExact(System.Environment.MachineName);
                comboBoxSqlServerName.SelectedIndex = comboBoxSqlServerName.Properties.Items.IndexOf(System.Environment.MachineName);

                //// If this machine is not a SQL server 
                //// then select the first server in the list
                //if (comboBoxSqlServerName.SelectedIndex < 0)
                //    comboBoxSqlServerName.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                this.DisplayErrorMessage(ex);
            }
            finally
            {
                comboBoxSqlServerName.Text = currentSqlServerName;
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

        private void comboBoxEditSqlServer_MouseDown(object sender, MouseEventArgs e)
        {
            if (isSqlServerListPopulated)
                return;

            //if (this.ComboBoxEditSqlServer.Properties.Items.Count > 0)
            //    return;

            Cursor? curentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            this.PopulateServerNames(this.ComboBoxEditSqlServer);

            Cursor.Current = curentCursor;

            this.isSqlServerListPopulated = true;
        }

        private void comboBoxEditSqlServer_TextChanged(object sender, EventArgs e)
        {
            this.databaseListRequireRefresh = true;
        }

        private void comboBoxEditDatabase_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.databaseListRequireRefresh)
                return;
            
            Cursor? curentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.GetConnectionString(false)))
                {
                    sqlConnection.Open();
                    DataTable databaseList = sqlConnection.GetSchema("Databases");
                    sqlConnection.Close();

                    this.ComboBoxEditDatabase.Properties.Items.Clear();

                    foreach (DataRow row in databaseList.Rows)
                        this.ComboBoxEditDatabase.Properties.Items.Add(row["database_name"]);
                }
            }
            catch (SqlException)
            {
                this.ComboBoxEditDatabase.Properties.Items.Clear();
            }
            finally
            {
                this.databaseListRequireRefresh = false;
                
                Cursor.Current = curentCursor;
            }
        }

        private void simpleButtonTestConnection_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ComboBoxEditSqlServer.Text))
            {
                XtraMessageBox.Show("An SQL server must be specified.", "Connection Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (this.radioButtonSqlServerAuthentication.Checked && string.IsNullOrEmpty(this.TextEditUsername.Text))
            {
                XtraMessageBox.Show("If SQL server authentication is used, a username must be provided.", "Connection Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Cursor? curentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.GetConnectionString(includeDatabase: false)))
                {
                    sqlConnection.Open();
                    sqlConnection.Close();
                    Cursor.Current = curentCursor;
                    XtraMessageBox.Show("Connection successful!", "Connection Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                Cursor.Current = curentCursor;
                XtraMessageBox.Show("Connection failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
