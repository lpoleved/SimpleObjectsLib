namespace Simple.Datastore.Controls
{
    partial class ConnectToSqlServerForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectToSqlServerForm));
			this.radioButtonSqlServerAuthentication = new System.Windows.Forms.RadioButton();
			this.radioButtonWindowsAuthentication = new System.Windows.Forms.RadioButton();
			this.groupControlSqlServer = new DevExpress.XtraEditors.GroupControl();
			this.comboBoxEditSqlServer = new DevExpress.XtraEditors.ComboBoxEdit();
			this.groupControlAuthentication = new DevExpress.XtraEditors.GroupControl();
			this.simpleButtonTestConnection = new DevExpress.XtraEditors.SimpleButton();
			this.textEditPassword = new DevExpress.XtraEditors.TextEdit();
			this.labelControlPassword = new DevExpress.XtraEditors.LabelControl();
			this.textEditUsername = new DevExpress.XtraEditors.TextEdit();
			this.labelControlUsername = new DevExpress.XtraEditors.LabelControl();
			this.groupControlDatabase = new DevExpress.XtraEditors.GroupControl();
			this.comboBoxEditDatabase = new DevExpress.XtraEditors.ComboBoxEdit();
			this.simpleButtonClose = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.groupControlSqlServer)).BeginInit();
			this.groupControlSqlServer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSqlServer.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlAuthentication)).BeginInit();
			this.groupControlAuthentication.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditUsername.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlDatabase)).BeginInit();
			this.groupControlDatabase.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDatabase.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// radioButtonSqlServerAuthentication
			// 
			this.radioButtonSqlServerAuthentication.AutoSize = true;
			this.radioButtonSqlServerAuthentication.Location = new System.Drawing.Point(10, 50);
			this.radioButtonSqlServerAuthentication.Name = "radioButtonSqlServerAuthentication";
			this.radioButtonSqlServerAuthentication.Size = new System.Drawing.Size(173, 17);
			this.radioButtonSqlServerAuthentication.TabIndex = 3;
			this.radioButtonSqlServerAuthentication.Text = "Use SQL Server Authentication";
			this.radioButtonSqlServerAuthentication.UseVisualStyleBackColor = true;
			this.radioButtonSqlServerAuthentication.CheckedChanged += new System.EventHandler(this.sqlServerAuthentication_CheckedChanged);
			// 
			// radioButtonWindowsAuthentication
			// 
			this.radioButtonWindowsAuthentication.AutoSize = true;
			this.radioButtonWindowsAuthentication.Checked = true;
			this.radioButtonWindowsAuthentication.Location = new System.Drawing.Point(10, 30);
			this.radioButtonWindowsAuthentication.Name = "radioButtonWindowsAuthentication";
			this.radioButtonWindowsAuthentication.Size = new System.Drawing.Size(162, 17);
			this.radioButtonWindowsAuthentication.TabIndex = 2;
			this.radioButtonWindowsAuthentication.TabStop = true;
			this.radioButtonWindowsAuthentication.Text = "Use Windows Authentication";
			this.radioButtonWindowsAuthentication.UseVisualStyleBackColor = true;
			// 
			// groupControlSqlServer
			// 
			this.groupControlSqlServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupControlSqlServer.Controls.Add(this.comboBoxEditSqlServer);
			this.groupControlSqlServer.Location = new System.Drawing.Point(8, 12);
			this.groupControlSqlServer.Name = "groupControlSqlServer";
			this.groupControlSqlServer.Size = new System.Drawing.Size(269, 61);
			this.groupControlSqlServer.TabIndex = 20;
			this.groupControlSqlServer.Text = "SQL Server/Express/LocalDB Name";
			// 
			// comboBoxEditSqlServer
			// 
			this.comboBoxEditSqlServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditSqlServer.Location = new System.Drawing.Point(10, 31);
			this.comboBoxEditSqlServer.Name = "comboBoxEditSqlServer";
			this.comboBoxEditSqlServer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditSqlServer.Size = new System.Drawing.Size(248, 20);
			this.comboBoxEditSqlServer.TabIndex = 1;
			this.comboBoxEditSqlServer.TextChanged += new System.EventHandler(this.comboBoxEditSqlServer_TextChanged);
			this.comboBoxEditSqlServer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.comboBoxEditSqlServer_MouseDown);
			// 
			// groupControlAuthentication
			// 
			this.groupControlAuthentication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupControlAuthentication.Controls.Add(this.simpleButtonTestConnection);
			this.groupControlAuthentication.Controls.Add(this.textEditPassword);
			this.groupControlAuthentication.Controls.Add(this.labelControlPassword);
			this.groupControlAuthentication.Controls.Add(this.textEditUsername);
			this.groupControlAuthentication.Controls.Add(this.labelControlUsername);
			this.groupControlAuthentication.Controls.Add(this.radioButtonWindowsAuthentication);
			this.groupControlAuthentication.Controls.Add(this.radioButtonSqlServerAuthentication);
			this.groupControlAuthentication.Location = new System.Drawing.Point(8, 78);
			this.groupControlAuthentication.Name = "groupControlAuthentication";
			this.groupControlAuthentication.Size = new System.Drawing.Size(269, 156);
			this.groupControlAuthentication.TabIndex = 21;
			this.groupControlAuthentication.Text = "Authentication";
			// 
			// simpleButtonTestConnection
			// 
			this.simpleButtonTestConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonTestConnection.Location = new System.Drawing.Point(150, 125);
			this.simpleButtonTestConnection.Name = "simpleButtonTestConnection";
			this.simpleButtonTestConnection.Size = new System.Drawing.Size(108, 23);
			this.simpleButtonTestConnection.TabIndex = 6;
			this.simpleButtonTestConnection.Text = "Test Connection";
			this.simpleButtonTestConnection.Click += new System.EventHandler(this.simpleButtonTestConnection_Click);
			// 
			// textEditPassword
			// 
			this.textEditPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditPassword.Location = new System.Drawing.Point(88, 99);
			this.textEditPassword.Name = "textEditPassword";
			this.textEditPassword.Size = new System.Drawing.Size(170, 20);
			this.textEditPassword.TabIndex = 5;
			// 
			// labelControlPassword
			// 
			this.labelControlPassword.Location = new System.Drawing.Point(28, 102);
			this.labelControlPassword.Name = "labelControlPassword";
			this.labelControlPassword.Size = new System.Drawing.Size(50, 13);
			this.labelControlPassword.TabIndex = 12;
			this.labelControlPassword.Text = "Password:";
			// 
			// textEditUsername
			// 
			this.textEditUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditUsername.Location = new System.Drawing.Point(88, 76);
			this.textEditUsername.Name = "textEditUsername";
			this.textEditUsername.Size = new System.Drawing.Size(170, 20);
			this.textEditUsername.TabIndex = 4;
			// 
			// labelControlUsername
			// 
			this.labelControlUsername.Location = new System.Drawing.Point(28, 79);
			this.labelControlUsername.Name = "labelControlUsername";
			this.labelControlUsername.Size = new System.Drawing.Size(52, 13);
			this.labelControlUsername.TabIndex = 10;
			this.labelControlUsername.Text = "Username:";
			// 
			// groupControlDatabase
			// 
			this.groupControlDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupControlDatabase.Controls.Add(this.comboBoxEditDatabase);
			this.groupControlDatabase.Location = new System.Drawing.Point(8, 239);
			this.groupControlDatabase.Name = "groupControlDatabase";
			this.groupControlDatabase.Size = new System.Drawing.Size(269, 65);
			this.groupControlDatabase.TabIndex = 22;
			this.groupControlDatabase.Text = "Database";
			// 
			// comboBoxEditDatabase
			// 
			this.comboBoxEditDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditDatabase.Location = new System.Drawing.Point(10, 31);
			this.comboBoxEditDatabase.Name = "comboBoxEditDatabase";
			this.comboBoxEditDatabase.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditDatabase.Size = new System.Drawing.Size(248, 20);
			this.comboBoxEditDatabase.TabIndex = 7;
			this.comboBoxEditDatabase.MouseDown += new System.Windows.Forms.MouseEventHandler(this.comboBoxEditDatabase_MouseDown);
			// 
			// simpleButtonClose
			// 
			this.simpleButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.simpleButtonClose.Location = new System.Drawing.Point(188, 314);
			this.simpleButtonClose.Name = "simpleButtonClose";
			this.simpleButtonClose.Size = new System.Drawing.Size(89, 23);
			this.simpleButtonClose.TabIndex = 8;
			this.simpleButtonClose.Text = "Close";
			this.simpleButtonClose.Click += new System.EventHandler(this.simpleButtonClose_Click);
			// 
			// ConnectToSqlServerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.simpleButtonClose;
			this.ClientSize = new System.Drawing.Size(285, 349);
			this.Controls.Add(this.simpleButtonClose);
			this.Controls.Add(this.groupControlDatabase);
			this.Controls.Add(this.groupControlAuthentication);
			this.Controls.Add(this.groupControlSqlServer);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("ConnectToSqlServerForm.IconOptions.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConnectToSqlServerForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Connect to SQL Server";
			((System.ComponentModel.ISupportInitialize)(this.groupControlSqlServer)).EndInit();
			this.groupControlSqlServer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSqlServer.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlAuthentication)).EndInit();
			this.groupControlAuthentication.ResumeLayout(false);
			this.groupControlAuthentication.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditUsername.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlDatabase)).EndInit();
			this.groupControlDatabase.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDatabase.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.RadioButton radioButtonSqlServerAuthentication;
        protected System.Windows.Forms.RadioButton radioButtonWindowsAuthentication;
        protected DevExpress.XtraEditors.GroupControl groupControlSqlServer;
        protected DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSqlServer;
        protected DevExpress.XtraEditors.GroupControl groupControlAuthentication;
        protected DevExpress.XtraEditors.TextEdit textEditPassword;
        protected DevExpress.XtraEditors.LabelControl labelControlPassword;
        protected DevExpress.XtraEditors.TextEdit textEditUsername;
        protected DevExpress.XtraEditors.LabelControl labelControlUsername;
        protected DevExpress.XtraEditors.GroupControl groupControlDatabase;
        protected DevExpress.XtraEditors.ComboBoxEdit comboBoxEditDatabase;
        protected DevExpress.XtraEditors.SimpleButton simpleButtonClose;
        protected DevExpress.XtraEditors.SimpleButton simpleButtonTestConnection;
    }
}