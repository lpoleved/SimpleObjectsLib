using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using Simple;

namespace Simple.Controls
{
    public partial class FormLogin : XtraForm
    {
		private string appName = String.Empty;

        public FormLogin()
			: this(String.Empty)
        {
   //         InitializeComponent();
			//this.ControlBox = false; // Hide close button, placed at up right corner of the form


			//UserLookAndFeel.Default.SetSkinStyle(ClientAppContext.Instance.UserSettings.RibonSkinName);


			//this.editorUsername.Text = this.lastUsername;
			//this.editorPassword.Text = String.Empty;
		}

		public FormLogin(string appName)
			: this(appName, String.Empty, String.Empty)
		{
		}

		public FormLogin(string appName, string server, string username)
		{
			InitializeComponent();
			this.ControlBox = false; // Hide close button, placed at up right corner of the form

			this.Text = appName + " Authorization";
			this.Server = server;
			this.Username = username;
		}

		public string AppName
		{
			get { return this.Text; }
			set
			{
				this.appName = value;
				this.Text = value + " Login";
			}
		}
		
		public string Server
		{
			get { return this.editorServer.Text; }
			set { this.editorServer.Text = value; }
		}

		public string Username
		{
			get { return this.editorUsername.Text; }
			set { this.editorUsername.Text = value; }
		}

		public string Password
		{
			get { return this.editorPassword.Text; }
			set { this.editorServer.Text = value; }
		}

        private void buttonOK_Click(object sender, EventArgs e)
        {

			this.DialogResult = DialogResult.OK;


			return;

			//if (ClientObjectManager.Instance.AuthorizeSession(this.editorUsername.Text, this.editorPassword.Text))
   //         {
			//	//try
			//	//{
			//	//}
			//	//catch (Exception exception)
			//	//{
			//	//	MessageBox.Show(exception.Message, AppContext.Instance.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//	//}

   //             this.DialogResult = DialogResult.OK;
   //             this.Close();
   //         }
   //         else
   //         {
   //             XtraMessageBox.Show("Invalid username or password.", ClientAppContext.Instance.AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
   //             this.editorPassword.Text = String.Empty;
   //             this.editorPassword.Focus();
   //             this.DialogResult = DialogResult.None;
   //         }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
			//try
			//{

			//	//if (String.IsNullOrEmpty(this.lastUsername))
			//	//    this.lastUsername = ClientAppContext.Instance.UsernameAdministrator;
			//}
			//catch (Exception exception)
			//{
			//	MessageBox.Show(exception.Message, this.appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//}

			if (this.editorServer.Text.IsNullOrEmpty())
			{
				this.editorServer.Focus();
			}
			else if (this.editorUsername.Text.IsNullOrEmpty())
			{
				this.editorUsername.Focus();
			}
		}

		private void FormLogin_Shown(object sender, EventArgs e)
		{
			//this.Focus();
		}
	}
}
