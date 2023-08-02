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
    public partial class FormLoginLocalDatastore : XtraForm
    {
		private string appName = String.Empty;

        public FormLoginLocalDatastore()
			: this(String.Empty)
        {
   //         InitializeComponent();
			//this.ControlBox = false; // Hide close button, placed at up right corner of the form


			//UserLookAndFeel.Default.SetSkinStyle(ClientAppContext.Instance.UserSettings.RibonSkinName);


			//this.editorUsername.Text = this.lastUsername;
			//this.editorPassword.Text = String.Empty;
		}

		public FormLoginLocalDatastore(string appName)
			: this(appName, String.Empty)
		{
		}

		public FormLoginLocalDatastore(string appName, string username)
		{
			InitializeComponent();
			this.ControlBox = false; // Hide close button, placed at up right corner of the form

			this.AppName = appName;
			this.Username = username.IsNullOrEmpty() ? "System Admin" : username;
		}

		public string AppName
		{
			get { return this.Text; }
			set
			{
				this.appName = value;
				this.Text = value + " Authorization";
			}
		}

		public string Username
		{
			get { return this.editorUsername.Text; }
			set { this.editorUsername.Text = value; }
		}

		public string Password
		{
			get { return this.editorPassword.Text; }
			set { this.editorPassword.Text = value; }
		}

        private void buttonOK_Click(object sender, EventArgs e)
        {
			this.DialogResult = DialogResult.OK;
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

			if (this.editorUsername.Text.IsNullOrEmpty())
				this.editorUsername.Focus();
		}

		private void FormLogin_Shown(object sender, EventArgs e)
		{
			//this.Focus();
		}
	}
}
