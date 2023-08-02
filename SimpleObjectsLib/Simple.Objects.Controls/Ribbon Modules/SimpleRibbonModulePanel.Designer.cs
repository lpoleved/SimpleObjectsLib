namespace Simple.Objects.Controls
{
    partial class SimpleRibbonModulePanel
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
            ((System.ComponentModel.ISupportInitialize)(this.tempRibbonControl)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControlMain
            // 
            this.tempRibbonControl.ApplicationButtonText = null;
            // 
            // 
            // 
            this.tempRibbonControl.ExpandCollapseItem.Id = 0;
            this.tempRibbonControl.ExpandCollapseItem.Name = "";
            this.tempRibbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.tempRibbonControl.ExpandCollapseItem});
            this.tempRibbonControl.Location = new System.Drawing.Point(0, 0);
            this.tempRibbonControl.MaxItemId = 1;
            this.tempRibbonControl.Name = "ribbonControlMain";
            this.tempRibbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage});
            this.tempRibbonControl.SelectedPage = this.ribbonPage;
            this.tempRibbonControl.Size = new System.Drawing.Size(682, 146);
            // 
            // ribbonPageMain
            // 
            this.ribbonPage.Name = "ribbonPageMain";
            // 
            // SimpleRibbonModulePanel
            // 
            this.Controls.Add(this.tempRibbonControl);
            this.Name = "SimpleRibbonModulePanel";
            this.Size = new System.Drawing.Size(682, 379);
            ((System.ComponentModel.ISupportInitialize)(this.tempRibbonControl)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        protected DevExpress.XtraBars.Ribbon.RibbonControl tempRibbonControl;
        protected DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage;
    }
}
