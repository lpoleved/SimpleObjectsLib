using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Simple.Datastore.Controls
{
    public partial class UserControlDatastoreConnectionXmlDatastore : XtraUserControl
    {
        public UserControlDatastoreConnectionXmlDatastore()
        {
            InitializeComponent();
        }

        private void buttonSelectDatastoreFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Open a folder which contains xml output";
            dialog.ShowNewFolderButton = false;
            
            if (String.IsNullOrEmpty(this.TextBoxDatastoreFolder.Text))
            {
                dialog.RootFolder = Environment.SpecialFolder.MyComputer;
            }
            else
            {
                dialog.SelectedPath = this.TextBoxDatastoreFolder.Text;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
                this.TextBoxDatastoreFolder.Text = dialog.SelectedPath;
        }
    }
}
