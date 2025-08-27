namespace Simple.Datastore.Controls
{
    partial class DatastoreConnectionForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatastoreConnectionForm));
			this.panelFooter = new DevExpress.XtraEditors.PanelControl();
			this.buttonConnect = new DevExpress.XtraEditors.SimpleButton();
			this.buttonClose = new DevExpress.XtraEditors.SimpleButton();
			this.DatastoreSettings = new Simple.Datastore.Controls.UserControlDatastoreSettings();
			((System.ComponentModel.ISupportInitialize)(this.panelFooter)).BeginInit();
			this.panelFooter.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelFooter
			// 
			this.panelFooter.Controls.Add(this.buttonConnect);
			this.panelFooter.Controls.Add(this.buttonClose);
			this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelFooter.Location = new System.Drawing.Point(0, 354);
			this.panelFooter.Name = "panelFooter";
			this.panelFooter.Size = new System.Drawing.Size(409, 39);
			this.panelFooter.TabIndex = 21;
			// 
			// buttonConnect
			// 
			this.buttonConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonConnect.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonConnect.Location = new System.Drawing.Point(201, 6);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(93, 26);
			this.buttonConnect.TabIndex = 1;
			this.buttonConnect.Text = "Connect";
			this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(321, 6);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(76, 26);
			this.buttonClose.TabIndex = 2;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// DatastoreSettings
			// 
			this.DatastoreSettings.DatastoreType = Simple.Datastore.DatastoreProviderType.SqlServer;
			this.DatastoreSettings.Location = new System.Drawing.Point(13, 13);
			this.DatastoreSettings.Name = "DatastoreSettings";
			this.DatastoreSettings.OfficeAccessFilePath = "";
			this.DatastoreSettings.ServerSystemSettings = null;
			this.DatastoreSettings.Size = new System.Drawing.Size(389, 332);
			this.DatastoreSettings.SqlConnectByConnectionString = false;
			this.DatastoreSettings.SqlConnectionString = "";
			this.DatastoreSettings.SqlDatabase = "";
			this.DatastoreSettings.SqlNetworkConnection = false;
			this.DatastoreSettings.SqlNetworkPort = 1433;
			this.DatastoreSettings.SqlServer = "";
			this.DatastoreSettings.TabIndex = 0;
			// 
			// DatastoreConnectionForm
			// 
			this.AcceptButton = this.buttonConnect;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(409, 393);
			this.Controls.Add(this.panelFooter);
			this.Controls.Add(this.DatastoreSettings);
			this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("DatastoreConnectionForm.IconOptions.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DatastoreConnectionForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Connect to Datastore";
			((System.ComponentModel.ISupportInitialize)(this.panelFooter)).EndInit();
			this.panelFooter.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelFooter;
        private DevExpress.XtraEditors.SimpleButton buttonConnect;
        private DevExpress.XtraEditors.SimpleButton buttonClose;

		public UserControlDatastoreSettings DatastoreSettings;
    }
}