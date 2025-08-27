using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Simple;
using Simple.Datastore;
using Simple.AppContext;
//using Simple.Collections;
using DevExpress.XtraEditors;

namespace Simple.Datastore.Controls
{
	public partial class UserControlDatastoreSettings : XtraUserControl
	{
		private Dictionary<DatastoreProviderType, DatastoreInfo> datastoreInfosByDatastoreProviderTypes = new Dictionary<DatastoreProviderType, DatastoreInfo>();
		private ServerSystemSettings? serverSystemSettings = null;


		public UserControlDatastoreSettings()
			: this(DatastoreProviderType.SqlServer, DatastoreProviderType.OfficeAccess) //, DatastoreProviderType.XmlFileSystem)
		{
		}

		public UserControlDatastoreSettings(params DatastoreProviderType[] datastoreProviderTypes)
		{
			this.InitializeComponent();
			this.SetAvailableProviderTypes(datastoreProviderTypes);
		}

		public ServerSystemSettings? ServerSystemSettings
		{
			get { return this.serverSystemSettings; }
			set { this.serverSystemSettings = value; }
		}

		public DatastoreProviderType DatastoreType
		{
			get 
			{
				if (this.comboBoxDatastoreType is not null && this.comboBoxDatastoreType.SelectedItem is DatastoreInfo datastoreInfo)
					return datastoreInfo.ProviderType;

				return DatastoreProviderType.SqlServer;
			}

			set { this.comboBoxDatastoreType.SelectedItem = this.datastoreInfosByDatastoreProviderTypes[value]; }
			
		}

		public string SqlServer
		{
			get { return this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.ComboBoxServer.Text; }
			set { this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.ComboBoxServer.Text = value; }
		}

		public string SqlDatabase
		{
			get { return this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.ComboBoxDatabase.Text; }
			set { this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.ComboBoxDatabase.Text = value; }
		}

		public bool SqlNetworkConnection
		{
			get { return this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.CheckButtonNetworkConnection.Checked; }
			set { this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.CheckButtonNetworkConnection.Checked = value; }
		}

		public int SqlNetworkPort
		{
			get { return Conversion.TryChangeType<int>(this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.TextEditNetworkPort.Text); }
			set { this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.TextEditNetworkPort.Text = value.ToString(); }
		}

		public bool SqlConnectByConnectionString
		{
			get { return this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.CheckButtonConnectByConnectionString.Checked; }
			set { this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.CheckButtonConnectByConnectionString.Checked = value; }
		}

		public string SqlConnectionString
		{
			get { return this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.TextEditConnectionString.Text; }
			set { this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.TextEditConnectionString.Text = value; }
		}

		public string OfficeAccessFilePath
		{
			get { return this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionOfficeAccess.TextEditOfficeAccessFile.Text; }
			set { this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionOfficeAccess.TextEditOfficeAccessFile.Text = value; }
		}

		//public string XmlDatastoreFolder
		//{
		//	get { return this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionXmlDatastore.TextBoxDatastoreFolder.Text; }
		//	set { this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionXmlDatastore.TextBoxDatastoreFolder.Text = value; }
		//}

		public void SetAvailableProviderTypes(DatastoreProviderType[] datastoreProviderTypes)
		{
			this.datastoreInfosByDatastoreProviderTypes.Clear();
			this.comboBoxDatastoreType.Properties.Items.Clear();

			foreach (DatastoreProviderType datastoreProviderType in datastoreProviderTypes)
			{
				switch (datastoreProviderType)
				{
					case DatastoreProviderType.SqlServer:
						this.datastoreInfosByDatastoreProviderTypes.Add(datastoreProviderType, new DatastoreInfo(DatastoreProviderType.SqlServer, "SQL Server"));
						break;

					case DatastoreProviderType.OfficeAccess:
						this.datastoreInfosByDatastoreProviderTypes.Add(datastoreProviderType, new DatastoreInfo(DatastoreProviderType.OfficeAccess, "Office Access"));
						break;

					//case DatastoreProviderType.XmlFileSystem:
					//	this.datastoreInfosByDatastoreProviderTypes.Add(datastoreProviderType, new DatastoreInfo(DatastoreProviderType.XmlFileSystem, "XML File System"));
					//	break;

					default:
						throw new ArgumentOutOfRangeException("The Datastore Provider Type " + datastoreProviderTypes.ToString() + " is not specified.");
				}
			}

			foreach (DatastoreInfo datastoreInfo in this.datastoreInfosByDatastoreProviderTypes.Values)
				this.comboBoxDatastoreType.Properties.Items.Add(datastoreInfo);
		}

		public void LoadSettings()
		{
			if (this.serverSystemSettings != null)
			{
				this.DatastoreType = (DatastoreProviderType)this.serverSystemSettings.DatastoreType;

				this.SqlServer = this.serverSystemSettings.DatastoreSqlServer;
				this.SqlDatabase = this.serverSystemSettings.DatastoreSqlDatabase;
				this.SqlNetworkConnection = this.serverSystemSettings.DatastoreSqlNetworkConnection;
				this.SqlNetworkPort = this.serverSystemSettings.DatastoreSqlNetworkPort;
				this.SqlConnectByConnectionString = this.serverSystemSettings.DatastoreSqlConnectByConnectionString;
				this.SqlConnectionString = this.serverSystemSettings.DatastoreSqlConnectionString;
				this.OfficeAccessFilePath = this.serverSystemSettings.DatastoreOfficeAccessFilePath;
				//this.XmlDatastoreFolder = this.serverSystemSettings.DatastoreXmlDataSourceFolder;
			}
		}

		public void UpdateSettings()
		{
			if (this.serverSystemSettings != null)
			{
				this.serverSystemSettings.DatastoreType = (int)this.DatastoreType;

				if (this.DatastoreType == DatastoreProviderType.SqlServer)
				{
					this.serverSystemSettings.DatastoreSqlServer = this.SqlServer;
					this.serverSystemSettings.DatastoreSqlDatabase = this.SqlDatabase;
					this.serverSystemSettings.DatastoreSqlNetworkConnection = this.SqlNetworkConnection;
					this.serverSystemSettings.DatastoreSqlNetworkPort = this.SqlNetworkPort;
					this.serverSystemSettings.DatastoreSqlConnectByConnectionString = this.SqlConnectByConnectionString;
					this.serverSystemSettings.DatastoreSqlConnectionString = this.SqlConnectionString;
				}
				else if (this.DatastoreType == DatastoreProviderType.OfficeAccess)
				{
					this.serverSystemSettings.DatastoreOfficeAccessFilePath = this.OfficeAccessFilePath;
				}
				//else if (this.DatastoreType == DatastoreProviderType.XmlFileSystem)
				//{
				//	this.serverSystemSettings.DatastoreXmlDataSourceFolder = this.XmlDatastoreFolder;
				//}

				//this.serverSystemSettings.Save();
			}
		}

		public void SaveSettings() => this.serverSystemSettings?.Save();

		//public void LoadSettings()
		//      {
		//          try
		//          {
		//              this.comboBoxDatastoreType.SelectedItem = this.datastoreInfosByDatastoreProviderTypes[(DatastoreProviderType)AppContext.AppContext.Instance.UserSettings.DatastoreType];

		//		//this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionOfficeAccess.TextBoxOfficeAccessFilePath.Text = AppContext.AppContext.Instance.UserSettings.DatastoreOfficeAccesFilePath;

		//		//this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionXmlDatastore.TextBoxDatastoreSourceFolder.Text = AppContext.AppContext.Instance.UserSettings.DatastoreXmlDataSourceFolder;

		//		this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.ComboBoxSqlServer.Text = AppContext.AppContext.Instance.UserSettings.DatastoreSqlServer;
		//              this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.ComboBoxDatabase.Text = AppContext.AppContext.Instance.UserSettings.DatastoreSqlDatabase;
		//              this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.checkButtonNetworkConnection.Checked = AppContext.AppContext.Instance.UserSettings.DatastoreSqlNetworkConnection;
		//              this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.TextEditNetworkPort.Text = AppContext.AppContext.Instance.UserSettings.DatastoreSqlNetworkPort;
		//              this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.CheckButtonConnectByConnectionString.Checked = AppContext.AppContext.Instance.UserSettings.DatastoreSqlConnectByConnectionString;
		//              this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.TextEditConnectionString.Text = AppContext.AppContext.Instance.UserSettings.DatastoreSqlConnectionString;
		//          }
		//          catch (Exception exception)
		//          {
		//              MessageBox.Show(exception.Message, AppContext.AppContext.Instance.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		//          }
		//      }

		//      public void SaveSettings()
		//      {
		//          try
		//          {
		//              AppContext.AppContext.Instance.UserSettings.DatastoreType = (this.comboBoxDatastoreType.SelectedItem as DatastoreInfo).ProviderType;

		//		//AppContext.AppContext.Instance.UserSettings.DatastoreOfficeAccesFilePath = this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionOfficeAccess.TextBoxOfficeAccessFilePath.Text;

		//		//AppContext.AppContext.Instance.UserSettings.DatastoreXmlDataSourceFolder = this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionXmlDatastore.TextBoxDatastoreSourceFolder.Text;

		//		AppContext.AppContext.Instance.UserSettings.DatastoreSqlServer = this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.ComboBoxSqlServer.Text;
		//              AppContext.AppContext.Instance.UserSettings.DatastoreSqlDatabase = this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.ComboBoxDatabase.Text;
		//              AppContext.AppContext.Instance.UserSettings.DatastoreSqlNetworkConnection = this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.checkButtonNetworkConnection.Checked;
		//              AppContext.AppContext.Instance.UserSettings.DatastoreSqlNetworkPort = this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.TextEditNetworkPort.Text;
		//              AppContext.AppContext.Instance.UserSettings.DatastoreSqlConnectByConnectionString = this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.CheckButtonConnectByConnectionString.Checked;
		//              AppContext.AppContext.Instance.UserSettings.DatastoreSqlConnectionString = this.userControlDatastoreConnectionPanel.UserControlDatastoreConnectionSqlServer.TextEditConnectionString.Text;

		//              AppContext.AppContext.Instance.UserSettings.Save();
		//          }
		//          catch (Exception exception)
		//          {
		//              MessageBox.Show(exception.Message, AppContext.AppContext.Instance.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		//          }
		//      }

		private void comboBoxDatastoreType_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			if (this.comboBoxDatastoreType.SelectedItem is DatastoreInfo datastoreInfo)
				this.userControlDatastoreConnectionPanel.SelectDatastoreControl(datastoreInfo.ProviderType);
		}
	}
}
