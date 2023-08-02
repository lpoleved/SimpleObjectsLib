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
    public partial class UserControlDatastoreConnectionPanel : XtraUserControl
    {
        private Dictionary<DatastoreProviderType, Type> datastoreUserControlTypesByDatastoreProviderType = new Dictionary<DatastoreProviderType, Type>();
        private Dictionary<DatastoreProviderType, UserControl> datastoreControlsByDatastoreProviderType = new Dictionary<DatastoreProviderType, UserControl>();
        
        public UserControlDatastoreConnectionPanel()
        {
            InitializeComponent();

            this.datastoreUserControlTypesByDatastoreProviderType.Add(DatastoreProviderType.OfficeAccess, typeof(UserControlDatastoreConnectionOfficeAccess));
            this.datastoreUserControlTypesByDatastoreProviderType.Add(DatastoreProviderType.SqlServer, typeof(UserControlDatastoreConnectionSqlServer));
            //this.datastoreUserControlTypesByDatastoreProviderType.Add(DatastoreProviderType.XmlFileSystem, typeof(UserControlDatastoreConnectionXmlDatastore));
        }

        public UserControl SelectedControl { get; private set; }

        public UserControlDatastoreConnectionOfficeAccess UserControlDatastoreConnectionOfficeAccess
        {
            get { return this.GetDatastoreControl(DatastoreProviderType.OfficeAccess) as UserControlDatastoreConnectionOfficeAccess; }
        }

        public UserControlDatastoreConnectionSqlServer UserControlDatastoreConnectionSqlServer
        {
            get { return this.GetDatastoreControl(DatastoreProviderType.SqlServer) as UserControlDatastoreConnectionSqlServer; }
        }

        //public UserControlDatastoreConnectionXmlDatastore UserControlDatastoreConnectionXmlDatastore
        //{
        //    get { return this.GetDatastoreControl(DatastoreProviderType.XmlFileSystem) as UserControlDatastoreConnectionXmlDatastore; }
        //}
        
        public void SelectDatastoreControl(DatastoreProviderType datastoreProviderType)
        {
            this.SelectedControl = this.GetDatastoreControl(datastoreProviderType);
            this.SelectedControl.Dock = DockStyle.Fill;
            this.SelectedControl.BringToFront();
            this.labelDatastoreConnectionPanel.Visible = false;
        }
        
        private UserControl GetDatastoreControl(DatastoreProviderType datastoreProviderType)
        {
            UserControl result;

            if (!this.datastoreControlsByDatastoreProviderType.TryGetValue(datastoreProviderType, out result))
            {
                Type userControlType = this.datastoreUserControlTypesByDatastoreProviderType[datastoreProviderType];
                result = Activator.CreateInstance(userControlType) as UserControl;
                
                this.Controls.Add(result);
                this.datastoreControlsByDatastoreProviderType.Add(datastoreProviderType, result);
            }

            return result;
        }
    }
}
