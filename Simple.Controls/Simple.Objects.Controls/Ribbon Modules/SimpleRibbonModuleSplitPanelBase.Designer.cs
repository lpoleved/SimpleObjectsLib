namespace Simple.Objects.Controls
{
    partial class SimpleRibbonModuleSplitPanel
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
			this.tempRibbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
			this.ribbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
			this.splitContainerMain = new DevExpress.XtraEditors.SplitContainerControl();
			((System.ComponentModel.ISupportInitialize)(this.tempRibbonControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerMain.Panel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerMain.Panel2)).BeginInit();
			this.SuspendLayout();
			// 
			// tempRibbonControl
			// 
			this.tempRibbonControl.ApplicationButtonText = null;
			// 
			// 
			// 
			this.tempRibbonControl.ExpandCollapseItem.Id = 0;
			this.tempRibbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.tempRibbonControl.ExpandCollapseItem,
            this.tempRibbonControl.SearchEditItem});
			this.tempRibbonControl.Location = new System.Drawing.Point(0, 0);
			this.tempRibbonControl.MaxItemId = 1;
			this.tempRibbonControl.Name = "ribbonControlMain";
			this.tempRibbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage});
			this.tempRibbonControl.Size = new System.Drawing.Size(948, 0);
			this.tempRibbonControl.Visible = false;
			// 
			// ribbonPage
			// 
			this.ribbonPage.Name = "ribbonPage";
			// 
			// splitContainerMain
			// 
			this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerMain.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
			this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
			this.splitContainerMain.Name = "splitContainerMain";
			// 
			// 
			// 
			this.splitContainerMain.Panel1.Location = new System.Drawing.Point(0, 0);
			this.splitContainerMain.Panel1.Name = "";
			this.splitContainerMain.Panel1.Size = new System.Drawing.Size(558, 616);
			this.splitContainerMain.Panel1.TabIndex = 0;
			this.splitContainerMain.Panel1.Text = "Panel1";
			// 
			// 
			// 
			this.splitContainerMain.Panel2.Location = new System.Drawing.Point(568, 0);
			this.splitContainerMain.Panel2.Name = "";
			this.splitContainerMain.Panel2.Size = new System.Drawing.Size(380, 616);
			this.splitContainerMain.Panel2.TabIndex = 1;
			this.splitContainerMain.Panel2.Text = "Panel2";
			this.splitContainerMain.Size = new System.Drawing.Size(948, 616);
			this.splitContainerMain.SplitterPosition = 558;
			this.splitContainerMain.TabIndex = 3;
			this.splitContainerMain.Text = "splitContainerControl1";
			// 
			// SimpleRibbonModuleSplitPanel
			// 
			this.Controls.Add(this.splitContainerMain);
			this.Name = "SimpleRibbonModuleSplitPanel";
			this.Size = new System.Drawing.Size(948, 616);
			((System.ComponentModel.ISupportInitialize)(this.tempRibbonControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerMain.Panel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerMain.Panel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        protected DevExpress.XtraEditors.SplitContainerControl splitContainerMain;
		private DevExpress.XtraBars.Ribbon.RibbonControl tempRibbonControl;
		private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage;
	}
}
