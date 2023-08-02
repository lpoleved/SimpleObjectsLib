using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;

namespace Simple.Datastore.Controls
{
    public partial class SelectDatastoreConnectionForm : XtraForm
    {
        public SelectDatastoreConnectionForm()
        {
            InitializeComponent();

            this.comboBoxDatastoreType.Properties.Items.Add(new DatastoreInfo(DatastoreProviderType.OfficeAccess, "Office Access"));
            this.comboBoxDatastoreType.Properties.Items.Add(new DatastoreInfo(DatastoreProviderType.SqlServer, "SQL Server"));
            //this.comboBoxDatastoreType.Properties.Items.Add(new DatastoreInfo(DatastoreProviderType.XmlFileSystem, "XML Datastore"));

            this.comboBoxDatastoreType.SelectedIndex = 0;
        }

        private void comboBoxDatastoreType_SelectedIndexChanged(object sender, EventArgs e)
        {
			DatastoreProviderType providerType = (this.comboBoxDatastoreType.SelectedItem as DatastoreInfo).ProviderType;

			this.userControlDatastoreConnectionPanel.SelectDatastoreControl(providerType);
        }
    }
}
