namespace Simple.Datastore.Controls
{
    partial class SelectDatastoreConnectionForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectDatastoreConnectionForm));
			this.labelDatastoreType = new DevExpress.XtraEditors.LabelControl();
			this.userControlDatastoreConnectionPanel = new Simple.Datastore.Controls.UserControlDatastoreConnectionPanel();
			this.comboBoxDatastoreType = new DevExpress.XtraEditors.ComboBoxEdit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxDatastoreType.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// labelDatastoreType
			// 
			this.labelDatastoreType.Location = new System.Drawing.Point(13, 26);
			this.labelDatastoreType.Name = "labelDatastoreType";
			this.labelDatastoreType.Size = new System.Drawing.Size(79, 13);
			this.labelDatastoreType.TabIndex = 1;
			this.labelDatastoreType.Text = "Datastore Type:";
			// 
			// userControlDatastoreConnectionPanel
			// 
			this.userControlDatastoreConnectionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.userControlDatastoreConnectionPanel.Location = new System.Drawing.Point(1, 65);
			this.userControlDatastoreConnectionPanel.Name = "userControlDatastoreConnectionPanel";
			this.userControlDatastoreConnectionPanel.Size = new System.Drawing.Size(337, 234);
			this.userControlDatastoreConnectionPanel.TabIndex = 2;
			// 
			// comboBoxDatastoreType
			// 
			this.comboBoxDatastoreType.Location = new System.Drawing.Point(98, 23);
			this.comboBoxDatastoreType.Name = "comboBoxDatastoreType";
			this.comboBoxDatastoreType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxDatastoreType.Size = new System.Drawing.Size(158, 20);
			this.comboBoxDatastoreType.TabIndex = 3;
			this.comboBoxDatastoreType.SelectedIndexChanged += new System.EventHandler(this.comboBoxDatastoreType_SelectedIndexChanged);
			// 
			// FormSelectDatastoreConnection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(340, 301);
			this.Controls.Add(this.comboBoxDatastoreType);
			this.Controls.Add(this.userControlDatastoreConnectionPanel);
			this.Controls.Add(this.labelDatastoreType);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSelectDatastoreConnection";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Datastore Connection";
			((System.ComponentModel.ISupportInitialize)(this.comboBoxDatastoreType.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl labelDatastoreType;
        private UserControlDatastoreConnectionPanel userControlDatastoreConnectionPanel;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxDatastoreType;
	}
}