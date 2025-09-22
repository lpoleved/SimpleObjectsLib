namespace Simple.Datastore.Controls
{
    partial class UserControlDatastoreConnectionPanel
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
			this.labelDatastoreConnectionPanel = new DevExpress.XtraEditors.LabelControl();
			this.SuspendLayout();
			// 
			// labelDatastoreConnectionPanel
			// 
			this.labelDatastoreConnectionPanel.Location = new System.Drawing.Point(86, 9);
			this.labelDatastoreConnectionPanel.Name = "labelDatastoreConnectionPanel";
			this.labelDatastoreConnectionPanel.Size = new System.Drawing.Size(134, 13);
			this.labelDatastoreConnectionPanel.TabIndex = 0;
			this.labelDatastoreConnectionPanel.Text = "Datastore Connection Panel";
			// 
			// UserControlDatastoreConnectionPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.labelDatastoreConnectionPanel);
			this.Name = "UserControlDatastoreConnectionPanel";
			this.Size = new System.Drawing.Size(338, 279);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelDatastoreConnectionPanel;
    }
}
