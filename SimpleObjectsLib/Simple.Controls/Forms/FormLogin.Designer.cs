namespace Simple.Controls
{
    partial class FormLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
			this.editorUsername = new DevExpress.XtraEditors.TextEdit();
			this.labelControlUsername = new DevExpress.XtraEditors.LabelControl();
			this.editorPassword = new DevExpress.XtraEditors.TextEdit();
			this.labelControlPassword = new DevExpress.XtraEditors.LabelControl();
			this.buttonCancel = new DevExpress.XtraEditors.SimpleButton();
			this.buttonOK = new DevExpress.XtraEditors.SimpleButton();
			this.pictureBoxKeys = new System.Windows.Forms.PictureBox();
			this.labelControlServer = new DevExpress.XtraEditors.LabelControl();
			this.editorServer = new DevExpress.XtraEditors.ComboBoxEdit();
			((System.ComponentModel.ISupportInitialize)(this.editorUsername.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorPassword.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxKeys)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorServer.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// editorUsername
			// 
			resources.ApplyResources(this.editorUsername, "editorUsername");
			this.editorUsername.Name = "editorUsername";
			// 
			// labelControlUsername
			// 
			resources.ApplyResources(this.labelControlUsername, "labelControlUsername");
			this.labelControlUsername.Name = "labelControlUsername";
			// 
			// editorPassword
			// 
			resources.ApplyResources(this.editorPassword, "editorPassword");
			this.editorPassword.Name = "editorPassword";
			this.editorPassword.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("editorPassword.Properties.Appearance.Font")));
			this.editorPassword.Properties.Appearance.Options.UseFont = true;
			this.editorPassword.Properties.PasswordChar = '*';
			// 
			// labelControlPassword
			// 
			resources.ApplyResources(this.labelControlPassword, "labelControlPassword");
			this.labelControlPassword.Name = "labelControlPassword";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.buttonCancel, "buttonCancel");
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOK
			// 
			resources.ApplyResources(this.buttonOK, "buttonOK");
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// pictureBoxKeys
			// 
			this.pictureBoxKeys.Image = global::Simple.Controls.Properties.Resources.Keys_Large;
			resources.ApplyResources(this.pictureBoxKeys, "pictureBoxKeys");
			this.pictureBoxKeys.Name = "pictureBoxKeys";
			this.pictureBoxKeys.TabStop = false;
			// 
			// labelControlServer
			// 
			resources.ApplyResources(this.labelControlServer, "labelControlServer");
			this.labelControlServer.Name = "labelControlServer";
			// 
			// editorServer
			// 
			resources.ApplyResources(this.editorServer, "editorServer");
			this.editorServer.Name = "editorServer";
			this.editorServer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("editorServer.Properties.Buttons"))))});
			// 
			// FormLogin
			// 
			this.AcceptButton = this.buttonOK;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.Controls.Add(this.editorServer);
			this.Controls.Add(this.labelControlServer);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.editorPassword);
			this.Controls.Add(this.labelControlPassword);
			this.Controls.Add(this.editorUsername);
			this.Controls.Add(this.labelControlUsername);
			this.Controls.Add(this.pictureBoxKeys);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("FormLogin.IconOptions.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLogin";
			this.Load += new System.EventHandler(this.FormLogin_Load);
			this.Shown += new System.EventHandler(this.FormLogin_Shown);
			((System.ComponentModel.ISupportInitialize)(this.editorUsername.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorPassword.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxKeys)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorServer.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxKeys;
        protected DevExpress.XtraEditors.TextEdit editorUsername;
        protected DevExpress.XtraEditors.LabelControl labelControlUsername;
        protected DevExpress.XtraEditors.TextEdit editorPassword;
        protected DevExpress.XtraEditors.LabelControl labelControlPassword;
        private DevExpress.XtraEditors.SimpleButton buttonCancel;
        private DevExpress.XtraEditors.SimpleButton buttonOK;
		protected DevExpress.XtraEditors.LabelControl labelControlServer;
		private DevExpress.XtraEditors.ComboBoxEdit editorServer;
	}
}