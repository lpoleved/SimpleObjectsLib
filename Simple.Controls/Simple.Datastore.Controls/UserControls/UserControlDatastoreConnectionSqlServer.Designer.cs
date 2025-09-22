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
			labelServer = new DevExpress.XtraEditors.LabelControl();
			labelDatabase = new DevExpress.XtraEditors.LabelControl();
			CheckButtonNetworkConnection = new DevExpress.XtraEditors.CheckEdit();
			TextEditConnectionString = new DevExpress.XtraEditors.TextEdit();
			CheckButtonConnectByConnectionString = new DevExpress.XtraEditors.CheckEdit();
			TextEditNetworkPort = new DevExpress.XtraEditors.TextEdit();
			labelPort = new DevExpress.XtraEditors.LabelControl();
			ComboBoxServer = new DevExpress.XtraEditors.ComboBoxEdit();
			ComboBoxDatabase = new DevExpress.XtraEditors.ComboBoxEdit();
			labelInfo = new DevExpress.XtraEditors.LabelControl();
			labelInfo2 = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)CheckButtonNetworkConnection.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)TextEditConnectionString.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)CheckButtonConnectByConnectionString.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)TextEditNetworkPort.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxServer.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxDatabase.Properties).BeginInit();
			SuspendLayout();
			// 
			// labelServer
			// 
			labelServer.Location = new System.Drawing.Point(12, 8);
			labelServer.Name = "labelServer";
			labelServer.Size = new System.Drawing.Size(58, 13);
			labelServer.TabIndex = 0;
			labelServer.Text = "SQL Server:";
			// 
			// labelDatabase
			// 
			labelDatabase.Location = new System.Drawing.Point(12, 54);
			labelDatabase.Name = "labelDatabase";
			labelDatabase.Size = new System.Drawing.Size(50, 13);
			labelDatabase.TabIndex = 2;
			labelDatabase.Text = "Database:";
			// 
			// CheckButtonNetworkConnection
			// 
			CheckButtonNetworkConnection.Location = new System.Drawing.Point(15, 108);
			CheckButtonNetworkConnection.Name = "CheckButtonNetworkConnection";
			CheckButtonNetworkConnection.Properties.Caption = "Network Connection";
			CheckButtonNetworkConnection.Size = new System.Drawing.Size(156, 20);
			CheckButtonNetworkConnection.TabIndex = 4;
			CheckButtonNetworkConnection.CheckedChanged += CheckBoxNetworkConnection_CheckedChanged;
			// 
			// TextEditConnectionString
			// 
			TextEditConnectionString.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			TextEditConnectionString.Location = new System.Drawing.Point(15, 192);
			TextEditConnectionString.Name = "TextEditConnectionString";
			TextEditConnectionString.Size = new System.Drawing.Size(311, 20);
			TextEditConnectionString.TabIndex = 8;
			// 
			// CheckButtonConnectByConnectionString
			// 
			CheckButtonConnectByConnectionString.Location = new System.Drawing.Point(15, 169);
			CheckButtonConnectByConnectionString.Name = "CheckButtonConnectByConnectionString";
			CheckButtonConnectByConnectionString.Properties.Caption = "Connect by Connection String:";
			CheckButtonConnectByConnectionString.Size = new System.Drawing.Size(234, 20);
			CheckButtonConnectByConnectionString.TabIndex = 7;
			CheckButtonConnectByConnectionString.CheckedChanged += checkBoxConnectByConnectionString_CheckedChanged;
			// 
			// TextEditNetworkPort
			// 
			TextEditNetworkPort.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			TextEditNetworkPort.Location = new System.Drawing.Point(68, 130);
			TextEditNetworkPort.Name = "TextEditNetworkPort";
			TextEditNetworkPort.Size = new System.Drawing.Size(43, 20);
			TextEditNetworkPort.TabIndex = 6;
			// 
			// labelPort
			// 
			labelPort.Location = new System.Drawing.Point(33, 133);
			labelPort.Name = "labelPort";
			labelPort.Size = new System.Drawing.Size(24, 13);
			labelPort.TabIndex = 5;
			labelPort.Text = "Port:";
			// 
			// ComboBoxServer
			// 
			ComboBoxServer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			ComboBoxServer.Location = new System.Drawing.Point(12, 27);
			ComboBoxServer.Name = "ComboBoxServer";
			ComboBoxServer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
			ComboBoxServer.Size = new System.Drawing.Size(314, 20);
			ComboBoxServer.TabIndex = 1;
			ComboBoxServer.MouseDown += ComboBoxSqlServer_MouseDown;
			// 
			// ComboBoxDatabase
			// 
			ComboBoxDatabase.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			ComboBoxDatabase.Location = new System.Drawing.Point(12, 73);
			ComboBoxDatabase.Name = "ComboBoxDatabase";
			ComboBoxDatabase.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
			ComboBoxDatabase.Size = new System.Drawing.Size(314, 20);
			ComboBoxDatabase.TabIndex = 3;
			ComboBoxDatabase.MouseDown += ComboBoxDatabase_MouseDown;
			// 
			// labelInfo
			// 
			labelInfo.Location = new System.Drawing.Point(18, 228);
			labelInfo.Name = "labelInfo";
			labelInfo.Size = new System.Drawing.Size(250, 13);
			labelInfo.TabIndex = 9;
			labelInfo.Text = "* Note that changing any of this parameters require";
			// 
			// labelInfo2
			// 
			labelInfo2.Location = new System.Drawing.Point(26, 242);
			labelInfo2.Name = "labelInfo2";
			labelInfo2.Size = new System.Drawing.Size(165, 13);
			labelInfo2.TabIndex = 10;
			labelInfo2.Text = "application restart to take effects!";
			// 
			// UserControlDatastoreConnectionSqlServer
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(labelInfo2);
			Controls.Add(labelInfo);
			Controls.Add(ComboBoxDatabase);
			Controls.Add(ComboBoxServer);
			Controls.Add(labelPort);
			Controls.Add(TextEditNetworkPort);
			Controls.Add(CheckButtonConnectByConnectionString);
			Controls.Add(TextEditConnectionString);
			Controls.Add(CheckButtonNetworkConnection);
			Controls.Add(labelDatabase);
			Controls.Add(labelServer);
			Name = "UserControlDatastoreConnectionSqlServer";
			Size = new System.Drawing.Size(337, 267);
			((System.ComponentModel.ISupportInitialize)CheckButtonNetworkConnection.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)TextEditConnectionString.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)CheckButtonConnectByConnectionString.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)TextEditNetworkPort.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxServer.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxDatabase.Properties).EndInit();
			ResumeLayout(false);
			PerformLayout();
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
		public DevExpress.XtraEditors.LabelControl labelInfo;
		public DevExpress.XtraEditors.LabelControl labelInfo2;
	}
}
