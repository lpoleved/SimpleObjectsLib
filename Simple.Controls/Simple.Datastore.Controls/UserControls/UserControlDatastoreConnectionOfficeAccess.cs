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
    public partial class UserControlDatastoreConnectionOfficeAccess : XtraUserControl
    {
        public UserControlDatastoreConnectionOfficeAccess()
        {
            InitializeComponent();
        }

        private void buttonSelectOfficeAccessFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Office Access files (*.mdb;*.accdb)|*.mdb;*.accdb|All files (*.*)|*.*";
            dialog.InitialDirectory = this.TextEditOfficeAccessFile.Text;
            dialog.Title = "Select a Office Access file";

            DialogResult dialogResult = dialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
                this.TextEditOfficeAccessFile.Text = dialog.FileName;
        }
    }
}
