using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Simple;
using Simple.Datastore;
using Simple.AppContext;

namespace Simple.Datastore.Controls
{
    public partial class DatastoreConnectionForm : XtraForm
    {
		public DatastoreConnectionForm(ServerSystemSettings serverSystemSettings)
        {
            InitializeComponent();

            //this.Name = AppContext.Instance.AppName + " Datastore Connection";

            this.DatastoreSettings.ServerSystemSettings = serverSystemSettings;
            this.DatastoreSettings.LoadSettings();
            this.buttonConnect.Focus();
        }

		//public void SetAvailableProviderTypes(params DatastoreProviderType[] datastoreProviderTypes)
		//{
		//	this.DatastoreSettings.SetAvailableProviderTypes(datastoreProviderTypes);
		//}

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            this.DatastoreSettings.UpdateSettings();
            this.DatastoreSettings.SaveSettings();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
