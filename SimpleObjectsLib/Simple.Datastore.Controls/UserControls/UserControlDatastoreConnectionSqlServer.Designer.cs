namespace Simple.Datastore.Controls
{
    partial class UserControlDatastoreConnectionSqlServer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.labelServer = new DevExpress.XtraEditors.LabelControl();
			this.labelDatabase = new DevExpress.XtraEditors.LabelControl();
			this.CheckButtonNetworkConnection = new DevExpress.XtraEditors.CheckEdit();
			this.TextEditConnectionString = new DevExpress.XtraEditors.TextEdit();
			this.CheckButtonConnectByConnectionString = new DevExpress.XtraEditors.CheckEdit();
			this.TextEditNetworkPort = new DevExpress.XtraEditors.TextEdit();
			this.labelPort = new DevExpress.XtraEditors.LabelControl();
			this.ComboBoxServer = new DevExpress.XtraEditors.ComboBoxEdit();
			this.ComboBoxDatabase = new DevExpress.XtraEditors.ComboBoxEdit();
			((System.ComponentModel.ISupportInitialize)(this.CheckButtonNetworkConnection.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TextEditConnectionString.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CheckButtonConnectByConnectionString.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TextEditNetworkPort.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ComboBoxServer.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ComboBoxDatabase.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// labelServer
			// 
			this.labelServer.Location = new System.Drawing.Point(12, 8);
			this.labelServer.Name = "labelServer";
			this.labelServer.Size = new System.Drawing.Size(58, 13);
			this.labelServer.TabIndex = 0;
			this.labelServer.Text = "SQL Server:";
			// 
			// labelDatabase
			// 
			this.labelDatabase.Location = new System.Drawing.Point(12, 54);
			this.labelDatabase.Name = "labelDatabase";
			this.labelDatabase.Size = new System.Drawing.Size(50, 13);
			this.labelDatabase.TabIndex = 2;
			this.labelDatabase.Text = "Database:";
			// 
			// CheckButtonNetworkConnection
			// 
			this.CheckButtonNetworkConnection.Location = new System.Drawing.Point(15, 107);
			this.CheckButtonNetworkConnection.Name = "CheckButtonNetworkConnection";
			this.CheckButtonNetworkConnection.Properties.Caption = "Network Connection";
			this.CheckButtonNetworkConnection.Size = new System.Drawing.Size(156, 20);
			this.CheckButtonNetworkConnection.TabIndex = 4;
			this.CheckButtonNetworkConnection.CheckedChanged += new System.EventHandler(this.CheckBoxNetworkConnection_CheckedChanged);
			// 
			// TextEditConnectionString
			// 
			this.TextEditConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextEditConnectionString.Location = new System.Drawing.Point(15, 205);
			this.TextEditConnectionString.Name = "TextEditConnectionString";
			this.TextEditConnectionString.Size = new System.Drawing.Size(311, 20);
			this.TextEditConnectionString.TabIndex = 8;
			// 
			// CheckButtonConnectByConnectionString
			// 
			this.CheckButtonConnectByConnectionString.Location = new System.Drawing.Point(15, 182);
			this.CheckButtonConnectByConnectionString.Name = "CheckButtonConnectByConnectionString";
			this.CheckButtonConnectByConnectionString.Properties.Caption = "Connect by Connection String:";
			this.CheckButtonConnectByConnectionString.Size = new System.Drawing.Size(234, 20);
			this.CheckButtonConnectByConnectionString.TabIndex = 7;
			this.CheckButtonConnectByConnectionString.CheckedChanged += new System.EventHandler(this.checkBoxConnectByConnectionString_CheckedChanged);
			// 
			// TextEditNetworkPort
			// 
			this.TextEditNetworkPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextEditNetworkPort.Location = new System.Drawing.Point(68, 130);
			this.TextEditNetworkPort.Name = "TextEditNetworkPort";
			this.TextEditNetworkPort.Size = new System.Drawing.Size(43, 20);
			this.TextEditNetworkPort.TabIndex = 6;
			// 
			// labelPort
			// 
			this.labelPort.Location = new System.Drawing.Point(33, 133);
			this.labelPort.Name = "labelPort";
			this.labelPort.Size = new System.Drawing.Size(24, 13);
			this.labelPort.TabIndex = 5;
			this.labelPort.Text = "Port:";
			// 
			// ComboBoxServer
			// 
			this.ComboBoxServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ComboBoxServer.Location = new System.Drawing.Point(12, 27);
			this.ComboBoxServer.Name = "ComboBoxServer";
			this.ComboBoxServer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.ComboBoxServer.Size = new System.Drawing.Size(314, 20);
			this.ComboBoxServer.TabIndex = 1;
			this.ComboBoxServer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ComboBoxSqlServer_MouseDown);
			// 
			// ComboBoxDatabase
			// 
			this.ComboBoxDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ComboBoxDatabase.Location = new System.Drawing.Point(12, 73);
			this.ComboBoxDatabase.Name = "ComboBoxDatabase";
			this.ComboBoxDatabase.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.ComboBoxDatabase.Size = new System.Drawing.Size(314, 20);
			this.ComboBoxDatabase.TabIndex = 3;
			this.ComboBoxDatabase.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ComboBoxDatabase_MouseDown);
			// 
			// UserControlDatastoreConnectionSqlServer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ComboBoxDatabase);
			this.Controls.Add(this.ComboBoxServer);
			this.Controls.Add(this.labelPort);
			this.Controls.Add(this.TextEditNetworkPort);
			this.Controls.Add(this.CheckButtonConnectByConnectionString);
			this.Controls.Add(this.TextEditConnectionString);
			this.Controls.Add(this.CheckButtonNetworkConnection);
			this.Controls.Add(this.labelDatabase);
			this.Controls.Add(this.labelServer);
			this.Name = "UserControlDatastoreConnectionSqlServer";
			this.Size = new System.Drawing.Size(337, 267);
			((System.ComponentModel.ISupportInitialize)(this.CheckButtonNetworkConnection.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TextEditConnectionString.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CheckButtonConnectByConnectionString.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TextEditNetworkPort.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ComboBoxServer.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ComboBoxDatabase.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelServer;
        private DevExpress.XtraEditors.LabelControl labelDatabase;
        private DevExpress.XtraEditors.LabelControl labelPort;

		public DevExpress.XtraEditors.CheckEdit CheckButtonNetworkConnection;
		public DevExpress.XtraEditors.TextEdit TextEditConnectionString;
        public DevExpress.XtraEditors.CheckEdit CheckButtonConnectByConnectionString;
        public DevExpress.XtraEditors.TextEdit TextEditNetworkPort;
		public DevExpress.XtraEditors.ComboBoxEdit ComboBoxServer;
		public DevExpress.XtraEditors.ComboBoxEdit ComboBoxDatabase;
	}
}
