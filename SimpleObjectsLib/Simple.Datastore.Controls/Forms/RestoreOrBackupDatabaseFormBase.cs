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
    public partial class RestoreOrBackupDatabaseFormBase : ConnectToSqlServerForm
    {
        public RestoreOrBackupDatabaseFormBase()
        {
            InitializeComponent();
        }

		public TextEdit FilePath
        {
            get { return this.textEditFilePath; }
        }

        protected virtual void OnButtonBrowseFilePathClick()
        {
        }

        private void simpleButtonBrowseFilePath_Click(object sender, EventArgs e)
        {
            this.OnButtonBrowseFilePathClick();
        }
    }
}
