namespace Simple.Datastore.Controls
{
    partial class UserControlDatastoreSettings
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
			this.labelDatastoreType = new DevExpress.XtraEditors.LabelControl();
			this.comboBoxDatastoreType = new DevExpress.XtraEditors.ComboBoxEdit();
			this.userControlDatastoreConnectionPanel = new Simple.Datastore.Controls.UserControlDatastoreConnectionPanel();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxDatastoreType.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// labelDatastoreType
			// 
			this.labelDatastoreType.Location = new System.Drawing.Point(14, 21);
			this.labelDatastoreType.Name = "labelDatastoreType";
			this.labelDatastoreType.Size = new System.Drawing.Size(79, 13);
			this.labelDatastoreType.TabIndex = 0;
			this.labelDatastoreType.Text = "Datastore Type:";
			// 
			// comboBoxDatastoreType
			// 
			this.comboBoxDatastoreType.Location = new System.Drawing.Point(98, 18);
			this.comboBoxDatastoreType.Name = "comboBoxDatastoreType";
			this.comboBoxDatastoreType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxDatastoreType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxDatastoreType.Size = new System.Drawing.Size(145, 20);
			this.comboBoxDatastoreType.TabIndex = 1;
			this.comboBoxDatastoreType.SelectedIndexChanged += new System.EventHandler(this.comboBoxDatastoreType_SelectedIndexChanged_1);
			// 
			// userControlDatastoreConnectionPanel
			// 
			this.userControlDatastoreConnectionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.userControlDatastoreConnectionPanel.Location = new System.Drawing.Point(0, 46);
			this.userControlDatastoreConnectionPanel.Name = "userControlDatastoreConnectionPanel";
			this.userControlDatastoreConnectionPanel.Size = new System.Drawing.Size(389, 286);
			this.userControlDatastoreConnectionPanel.TabIndex = 2;
			// 
			// UserControlDatastoreSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.comboBoxDatastoreType);
			this.Controls.Add(this.userControlDatastoreConnectionPanel);
			this.Controls.Add(this.labelDatastoreType);
			this.Name = "UserControlDatastoreSettings";
			this.Size = new System.Drawing.Size(389, 332);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxDatastoreType.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		private Simple.Datastore.Controls.UserControlDatastoreConnectionPanel userControlDatastoreConnectionPanel;
        private DevExpress.XtraEditors.LabelControl labelDatastoreType;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxDatastoreType;
	}
}
