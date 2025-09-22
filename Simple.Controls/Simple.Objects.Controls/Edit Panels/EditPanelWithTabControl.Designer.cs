namespace Simple.Objects.Controls
{
    partial class EditPanelWithTabControl
    {
        ///// <summary> 
        ///// Required designer variable.
        ///// </summary>
        //private System.ComponentModel.IContainer components = null;

        ///// <summary> 
        ///// Clean up any resources being used.
        ///// </summary>
        ///// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.tabControl = new DevExpress.XtraTab.XtraTabControl();
			this.tabPageObjectName = new DevExpress.XtraTab.XtraTabPage();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
			this.tabControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Location = new System.Drawing.Point(3, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedTabPage = this.tabPageObjectName;
			this.tabControl.Size = new System.Drawing.Size(490, 662);
			this.tabControl.TabIndex = 0;
			this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageObjectName});
			// 
			// tabPageObjectName
			// 
			this.tabPageObjectName.Name = "tabPageObjectName";
			this.tabPageObjectName.Size = new System.Drawing.Size(488, 637);
			this.tabPageObjectName.Text = "ObjectName";
			// 
			// EditPanelWithTabControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl);
			this.Name = "EditPanelWithTabControl";
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraTab.XtraTabControl tabControl;
        protected DevExpress.XtraTab.XtraTabPage tabPageObjectName;
    }
}
