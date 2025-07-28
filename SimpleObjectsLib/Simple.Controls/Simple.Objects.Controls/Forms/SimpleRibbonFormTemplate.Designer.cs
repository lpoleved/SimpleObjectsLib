namespace Simple.Objects.Controls
{
    partial class SimpleRibbonFormTemplate
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleRibbonFormTemplate));
			this.ribbonMain = new DevExpress.XtraBars.Ribbon.RibbonControl();
			this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
			this.menuItemHelp = new DevExpress.XtraBars.BarButtonItem();
			this.menuItemAbout = new DevExpress.XtraBars.BarButtonItem();
			this.menuItemVersionHistory = new DevExpress.XtraBars.BarButtonItem();
			this.menuHelp = new DevExpress.XtraBars.BarSubItem();
			this.menuItemChangeSkin = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonAbout = new DevExpress.XtraBars.BarButtonItem();
			this.barSubItemChangeSkin = new DevExpress.XtraBars.BarSubItem();
			this.barButtonHelp = new DevExpress.XtraBars.BarButtonItem();
			this.barStaticItemUser = new DevExpress.XtraBars.BarStaticItem();
			this.barStaticItemServer = new DevExpress.XtraBars.BarStaticItem();
			this.barStaticItemInfo = new DevExpress.XtraBars.BarStaticItem();
			this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.panelMain = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.ribbonMain)).BeginInit();
			this.SuspendLayout();
			// 
			// ribbonMain
			// 
			this.ribbonMain.ApplicationButtonText = null;
			this.ribbonMain.AutoSizeItems = true;
			// 
			// 
			// 
			this.ribbonMain.ExpandCollapseItem.Id = 0;
			this.ribbonMain.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonMain.ExpandCollapseItem,
            this.barButtonItem5,
            this.barButtonItem6,
            this.menuItemHelp,
            this.menuItemAbout,
            this.menuItemVersionHistory,
            this.menuHelp,
            this.menuItemChangeSkin,
            this.barButtonAbout,
            this.barSubItemChangeSkin,
            this.barButtonHelp,
            this.barStaticItemUser,
            this.barStaticItemServer,
            this.barStaticItemInfo,
            this.ribbonMain.SearchEditItem});
			this.ribbonMain.Location = new System.Drawing.Point(0, 0);
			this.ribbonMain.MaxItemId = 36;
			this.ribbonMain.Name = "ribbonMain";
			this.ribbonMain.Size = new System.Drawing.Size(1092, 53);
			this.ribbonMain.StatusBar = this.ribbonStatusBar;
			// 
			// barButtonItem5
			// 
			this.barButtonItem5.Caption = "Undo";
			this.barButtonItem5.Id = 8;
			this.barButtonItem5.Name = "barButtonItem5";
			// 
			// barButtonItem6
			// 
			this.barButtonItem6.Caption = "Delete";
			this.barButtonItem6.Id = 9;
			this.barButtonItem6.Name = "barButtonItem6";
			// 
			// menuItemHelp
			// 
			this.menuItemHelp.Caption = "Help";
			this.menuItemHelp.Enabled = false;
			this.menuItemHelp.Id = 11;
			this.menuItemHelp.Name = "menuItemHelp";
			this.menuItemHelp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuItemHelp_ItemClick);
			// 
			// menuItemAbout
			// 
			this.menuItemAbout.Caption = "About";
			this.menuItemAbout.Id = 12;
			this.menuItemAbout.Name = "menuItemAbout";
			this.menuItemAbout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuItemAbout_ItemClick);
			// 
			// menuItemVersionHistory
			// 
			this.menuItemVersionHistory.Caption = "Version history";
			this.menuItemVersionHistory.Enabled = false;
			this.menuItemVersionHistory.Id = 13;
			this.menuItemVersionHistory.Name = "menuItemVersionHistory";
			this.menuItemVersionHistory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuItemVersionHistory_ItemClick);
			// 
			// menuHelp
			// 
			this.menuHelp.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.menuHelp.Caption = "Help";
			this.menuHelp.Id = 16;
			this.menuHelp.MenuCaption = "Help";
			this.menuHelp.Name = "menuHelp";
			this.menuHelp.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
			// 
			// menuItemChangeSkin
			// 
			this.menuItemChangeSkin.Caption = "Change Skin";
			this.menuItemChangeSkin.Id = 20;
			this.menuItemChangeSkin.Name = "menuItemChangeSkin";
			// 
			// barButtonAbout
			// 
			this.barButtonAbout.Caption = "About";
			this.barButtonAbout.Id = 22;
			this.barButtonAbout.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonAbout.ImageOptions.Image")));
			this.barButtonAbout.Name = "barButtonAbout";
			this.barButtonAbout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonAbout_ItemClick);
			// 
			// barSubItemChangeSkin
			// 
			this.barSubItemChangeSkin.Caption = "Change Skin";
			this.barSubItemChangeSkin.Id = 23;
			this.barSubItemChangeSkin.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barSubItemChangeSkin.ImageOptions.Image")));
			this.barSubItemChangeSkin.Name = "barSubItemChangeSkin";
			// 
			// barButtonHelp
			// 
			this.barButtonHelp.Caption = "Help";
			this.barButtonHelp.Id = 24;
			this.barButtonHelp.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonHelp.ImageOptions.Image")));
			this.barButtonHelp.Name = "barButtonHelp";
			this.barButtonHelp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonHelp_ItemClick);
			// 
			// barStaticItemUser
			// 
			this.barStaticItemUser.Caption = "User";
			this.barStaticItemUser.Id = 28;
			this.barStaticItemUser.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barStaticItemUser.ImageOptions.Image")));
			this.barStaticItemUser.Name = "barStaticItemUser";
			this.barStaticItemUser.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			// 
			// barStaticItemServer
			// 
			this.barStaticItemServer.Caption = "Server";
			this.barStaticItemServer.Id = 30;
			this.barStaticItemServer.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barStaticItemServer.ImageOptions.Image")));
			this.barStaticItemServer.Name = "barStaticItemServer";
			this.barStaticItemServer.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			// 
			// barStaticItemInfo
			// 
			this.barStaticItemInfo.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.barStaticItemInfo.Caption = "Powered by Simple.Objects™";
			this.barStaticItemInfo.Id = 31;
			this.barStaticItemInfo.Name = "barStaticItemInfo";
			// 
			// ribbonStatusBar
			// 
			this.ribbonStatusBar.Location = new System.Drawing.Point(0, 656);
			this.ribbonStatusBar.Name = "ribbonStatusBar";
			this.ribbonStatusBar.Ribbon = this.ribbonMain;
			this.ribbonStatusBar.Size = new System.Drawing.Size(1092, 27);
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Black";
			// 
			// panelMain
			// 
			this.panelMain.BackColor = System.Drawing.Color.Transparent;
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 53);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(1092, 603);
			this.panelMain.TabIndex = 3;
			// 
			// SimpleRibbonFormTemplate
			// 
			this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.Appearance.Options.UseBackColor = true;
			this.Appearance.Options.UseFont = true;
			this.ClientSize = new System.Drawing.Size(1092, 683);
			this.Controls.Add(this.panelMain);
			this.Controls.Add(this.ribbonStatusBar);
			this.Controls.Add(this.ribbonMain);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
			this.Name = "SimpleRibbonFormTemplate";
			this.Ribbon = this.ribbonMain;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.StatusBar = this.ribbonStatusBar;
			this.Text = "Mobiteli Studio - Enterprise Edition";
			((System.ComponentModel.ISupportInitialize)(this.ribbonMain)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        protected DevExpress.XtraBars.Ribbon.RibbonControl ribbonMain;
        protected DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        protected DevExpress.XtraBars.BarButtonItem barButtonItem5;
        protected DevExpress.XtraBars.BarButtonItem barButtonItem6;
        protected DevExpress.XtraBars.BarButtonItem menuItemHelp;
        protected DevExpress.XtraBars.BarButtonItem menuItemVersionHistory;
        protected DevExpress.XtraBars.BarButtonItem menuItemAbout;
        protected DevExpress.XtraBars.BarSubItem menuHelp;
        protected DevExpress.XtraBars.BarButtonItem menuItemChangeSkin;
        protected DevExpress.XtraBars.BarButtonItem barButtonAbout;
        protected DevExpress.XtraBars.BarSubItem barSubItemChangeSkin;
        protected DevExpress.XtraBars.BarButtonItem barButtonHelp;
        protected DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        protected DevExpress.XtraBars.BarStaticItem barStaticItemUser;
        protected DevExpress.XtraBars.BarStaticItem barStaticItemServer;
        protected DevExpress.XtraBars.BarStaticItem barStaticItemInfo;
        protected System.Windows.Forms.Panel panelMain;
    }
}