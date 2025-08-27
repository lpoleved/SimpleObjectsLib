using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;

namespace Simple.Objects.ServerMonitor
{
	partial class FormMainOld2
	{

		#region Designer generated code
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainOld2));
			GalleryItemGroup galleryItemGroup1 = new GalleryItemGroup();
			GalleryItem galleryItem1 = new GalleryItem();
			GalleryItem galleryItem2 = new GalleryItem();
			GalleryItem galleryItem3 = new GalleryItem();
			GalleryItem galleryItem4 = new GalleryItem();
			GalleryItem galleryItem5 = new GalleryItem();
			GalleryItem galleryItem6 = new GalleryItem();
			GalleryItem galleryItem7 = new GalleryItem();
			GalleryItem galleryItem8 = new GalleryItem();
			GalleryItem galleryItem9 = new GalleryItem();
			DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
			iWeb = new BarButtonItem();
			iAbout = new BarButtonItem();
			iPrint = new BarButtonItem();
			iClose = new BarButtonItem();
			repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			popupControlContainer1 = new PopupControlContainer(components);
			ribbonControl = new RibbonControl();
			pmAppMain = new ApplicationMenu(components);
			pccBottom = new PopupControlContainer(components);
			sbExit = new SimpleButton();
			imageCollection2 = new DevExpress.Utils.ImageCollection(components);
			buttonSettings = new BarButtonItem();
			buttonHelp = new BarSubItem();
			biGettingStarted = new BarButtonItem();
			biOnlineHelp = new BarButtonItem();
			iProtected = new BarButtonItem();
			sbiSave = new BarSubItem();
			barButtonItemInfo = new BarButtonItem();
			siDocName = new BarStaticItem();
			rgbiSkins = new RibbonGalleryBarItem();
			barEditItemRibbonStyle = new BarEditItem();
			riicStyle = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
			editButtonGroup = new BarButtonGroup();
			barEditItemColorScheme = new BarEditItem();
			repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			bbColorMix = new BarButtonItem();
			barButtonItem1 = new BarButtonItem();
			biContact = new BarButtonItem();
			rgbiColorScheme = new RibbonGalleryBarItem();
			barSubItemChangeSkin = new BarSubItem();
			barButtonItemConnect = new BarButtonItem();
			barEditItemMonitorServer = new BarEditItem();
			repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			barEditItemMonitorServerPort = new BarEditItem();
			repositoryItemSpinEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			barButtonItemDisconnect = new BarButtonItem();
			barEditItem2 = new BarEditItem();
			barButtonItemServerStart = new BarButtonItem();
			barButtonItemServerStop = new BarButtonItem();
			barButtonItemServerRestart = new BarButtonItem();
			barStaticItemUser = new BarStaticItem();
			barStaticItemMonitorServer = new BarStaticItem();
			barToggleSwitchItemDarkMode = new BarToggleSwitchItem();
			barToggleSwitchItemCompactView = new BarToggleSwitchItem();
			barStaticItemPoweredByInfo = new BarStaticItem();
			imageCollection1 = new DevExpress.Utils.ImageCollection(components);
			selectionMiniToolbar = new RibbonMiniToolbar(components);
			ribbonPageHome = new RibbonPage();
			ribbonPageGroupMonitorService = new RibbonPageGroup();
			ribbonPageGroupSimpleObjectsServer = new RibbonPageGroup();
			ribbonPageGroupSkins = new RibbonPageGroup();
			rpHelp = new RibbonPage();
			rpgHelp = new RibbonPageGroup();
			repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			repositoryItemTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemTrackBar();
			repositoryItemColorPickEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit();
			repositoryItemComboBox3 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			ribbonStatusBar1 = new RibbonStatusBar();
			pcAppMenuFileLabels = new PanelControl();
			pmNew = new PopupMenu(components);
			defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(components);
			pmMain = new PopupMenu(components);
			imageCollection3 = new DevExpress.Utils.ImageCollection(components);
			backstageViewControl = new BackstageViewControl();
			backstageViewClientControl2 = new BackstageViewClientControl();
			recentItemControl1 = new RecentItemControl();
			recentStackPanel12 = new RecentStackPanel();
			recentLabelItem1 = new RecentLabelItem();
			recentHyperlinkItem1 = new RecentHyperlinkItem();
			recentLabelItem2 = new RecentLabelItem();
			recentStackPanel13 = new RecentStackPanel();
			recentPinItem2 = new RecentPinItem();
			recentPinItem3 = new RecentPinItem();
			recentPinItem4 = new RecentPinItem();
			backstageViewClientControl8 = new BackstageViewClientControl();
			recentSaveAs = new RecentItemControl();
			recentStackPanel5 = new RecentStackPanel();
			recentStackPanel6 = new RecentStackPanel();
			recentTabItem3 = new RecentTabItem();
			recentStackPanel7 = new RecentStackPanel();
			backstageViewClientControl11 = new BackstageViewClientControl();
			recentControlPrint = new RecentItemControl();
			recentPrintOptionsContainer = new RecentControlItemControlContainer();
			layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			ddbDuplex = new DropDownButton();
			ddbOrientation = new DropDownButton();
			ddbPaperSize = new DropDownButton();
			ddbMargins = new DropDownButton();
			ddbCollate = new DropDownButton();
			ddbPrinter = new DropDownButton();
			printButton = new SimpleButton();
			copySpinEdit = new SpinEdit();
			layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			lciCopiesSpinEdit = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
			recentPrintPreviewContainer = new RecentControlItemControlContainer();
			panelControl2 = new PanelControl();
			printControl2 = new DevExpress.XtraPrinting.Control.PrintControl();
			panelControl3 = new PanelControl();
			stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
			zoomTextEdit = new TextEdit();
			panel2 = new Panel();
			pageButtonEdit = new ButtonEdit();
			zoomTrackBarControl1 = new ZoomTrackBarControl();
			recentStackPanel8 = new RecentStackPanel();
			recentPrintPreview = new RecentControlContainerItem();
			recentStackPanel9 = new RecentStackPanel();
			recentPrintOptions = new RecentControlContainerItem();
			backstageViewClientControl10 = new BackstageViewClientControl();
			recentControlExport = new RecentItemControl();
			recentStackPanel14 = new RecentStackPanel();
			recentStackPanel10 = new RecentStackPanel();
			recentTabItem4 = new RecentTabItem();
			recentStackPanel11 = new RecentStackPanel();
			recentControlRecentItem1 = new RecentPinItem();
			recentControlRecentItem2 = new RecentPinItem();
			recentControlRecentItem4 = new RecentPinItem();
			recentControlRecentItem5 = new RecentPinItem();
			recentControlRecentItem6 = new RecentPinItem();
			recentControlRecentItem7 = new RecentPinItem();
			recentControlRecentItem8 = new RecentPinItem();
			recentControlRecentItem9 = new RecentPinItem();
			backstageViewButtonItemSettings = new BackstageViewButtonItem();
			backstageViewTabItem1 = new BackstageViewTabItem();
			printTabItem = new BackstageViewTabItem();
			backstageViewTabItem4 = new BackstageViewTabItem();
			backstageViewTabItem6 = new BackstageViewTabItem();
			bvItemClose = new BackstageViewButtonItem();
			bvItemExit = new BackstageViewButtonItem();
			tabControl = new DevExpress.XtraTab.XtraTabControl();
			tabPageLog = new DevExpress.XtraTab.XtraTabPage();
			tabPageUserSessions = new DevExpress.XtraTab.XtraTabPage();
			gridControlUserSessions = new DevExpress.XtraGrid.GridControl();
			gridViewUserSessions = new DevExpress.XtraGrid.Views.Grid.GridView();
			tabSocketPAckageCommunication = new DevExpress.XtraTab.XtraTabPage();
			splitContainerControlSocketPackageCommunication = new SplitContainerControl();
			gridControlRequestResponseMessages = new DevExpress.XtraGrid.GridControl();
			gridViewRequestResponseMessagess = new DevExpress.XtraGrid.Views.Grid.GridView();
			groupPropertyPanelRequestResponseMessages = new Controls.GroupPropertyPanel();
			tabPageTransactions = new DevExpress.XtraTab.XtraTabPage();
			splitContainerControlTransactions = new SplitContainerControl();
			gridControlTransactions = new DevExpress.XtraGrid.GridControl();
			gridViewTransactions = new DevExpress.XtraGrid.Views.Grid.GridView();
			groupPropertyPanelTransactionLog = new Controls.GroupPropertyPanel();
			tabPageErrors = new DevExpress.XtraTab.XtraTabPage();
			tabPageStatistics = new DevExpress.XtraTab.XtraTabPage();
			printControl1 = new DevExpress.XtraPrinting.Control.PrintControl();
			recentControlRecentItem10 = new RecentPinItem();
			recentControlButtonItem1 = new RecentButtonItem();
			backstageViewClientControl7 = new BackstageViewClientControl();
			backstageViewClientControl1 = new BackstageViewClientControl();
			backstageViewClientControl3 = new BackstageViewClientControl();
			backstageViewTabItem2 = new BackstageViewTabItem();
			bvTabPrint = new BackstageViewTabItem();
			backstageViewClientControl4 = new BackstageViewClientControl();
			((System.ComponentModel.ISupportInitialize)repositoryItemSpinEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)popupControlContainer1).BeginInit();
			((System.ComponentModel.ISupportInitialize)ribbonControl).BeginInit();
			((System.ComponentModel.ISupportInitialize)pmAppMain).BeginInit();
			((System.ComponentModel.ISupportInitialize)pccBottom).BeginInit();
			pccBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)imageCollection2).BeginInit();
			((System.ComponentModel.ISupportInitialize)riicStyle).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemComboBox2).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemSpinEdit2).BeginInit();
			((System.ComponentModel.ISupportInitialize)imageCollection1).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemPictureEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemTrackBar1).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemColorPickEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemComboBox3).BeginInit();
			((System.ComponentModel.ISupportInitialize)pcAppMenuFileLabels).BeginInit();
			((System.ComponentModel.ISupportInitialize)pmNew).BeginInit();
			((System.ComponentModel.ISupportInitialize)pmMain).BeginInit();
			((System.ComponentModel.ISupportInitialize)imageCollection3).BeginInit();
			((System.ComponentModel.ISupportInitialize)backstageViewControl).BeginInit();
			backstageViewControl.SuspendLayout();
			backstageViewClientControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)recentItemControl1).BeginInit();
			backstageViewClientControl8.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)recentSaveAs).BeginInit();
			backstageViewClientControl11.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)recentControlPrint).BeginInit();
			recentControlPrint.SuspendLayout();
			recentPrintOptionsContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
			layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)copySpinEdit.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)lciCopiesSpinEdit).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem6).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem7).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem8).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem9).BeginInit();
			recentPrintPreviewContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)panelControl2).BeginInit();
			panelControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)panelControl3).BeginInit();
			panelControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)stackPanel1).BeginInit();
			stackPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)zoomTextEdit.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)pageButtonEdit.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)zoomTrackBarControl1).BeginInit();
			((System.ComponentModel.ISupportInitialize)zoomTrackBarControl1.Properties).BeginInit();
			backstageViewClientControl10.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)recentControlExport).BeginInit();
			((System.ComponentModel.ISupportInitialize)tabControl).BeginInit();
			tabControl.SuspendLayout();
			tabPageUserSessions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)gridControlUserSessions).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridViewUserSessions).BeginInit();
			tabSocketPAckageCommunication.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainerControlSocketPackageCommunication).BeginInit();
			((System.ComponentModel.ISupportInitialize)splitContainerControlSocketPackageCommunication.Panel1).BeginInit();
			splitContainerControlSocketPackageCommunication.Panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainerControlSocketPackageCommunication.Panel2).BeginInit();
			splitContainerControlSocketPackageCommunication.Panel2.SuspendLayout();
			splitContainerControlSocketPackageCommunication.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)gridControlRequestResponseMessages).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridViewRequestResponseMessagess).BeginInit();
			tabPageTransactions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainerControlTransactions).BeginInit();
			((System.ComponentModel.ISupportInitialize)splitContainerControlTransactions.Panel1).BeginInit();
			splitContainerControlTransactions.Panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainerControlTransactions.Panel2).BeginInit();
			splitContainerControlTransactions.Panel2.SuspendLayout();
			splitContainerControlTransactions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)gridControlTransactions).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridViewTransactions).BeginInit();
			backstageViewClientControl4.SuspendLayout();
			SuspendLayout();
			// 
			// iWeb
			// 
			iWeb.Caption = "&Simple.Objects™  on the Web";
			iWeb.CategoryGuid = new Guid("e07a4c24-66ac-4de6-bbcb-c0b6cfa7798b");
			iWeb.Description = "Opens the web page.";
			iWeb.Hint = "DevExpress on the Web";
			iWeb.Id = 21;
			iWeb.ImageOptions.ImageIndex = 24;
			iWeb.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iWeb.ImageOptions.SvgImage");
			iWeb.Name = "iWeb";
			iWeb.ItemClick += iWeb_ItemClick;
			// 
			// iAbout
			// 
			iAbout.Alignment = BarItemLinkAlignment.Right;
			iAbout.Caption = "&About";
			iAbout.CategoryGuid = new Guid("e07a4c24-66ac-4de6-bbcb-c0b6cfa7798b");
			iAbout.Description = "Displays the description of this program.";
			iAbout.Hint = "Displays the About dialog";
			iAbout.Id = 22;
			iAbout.ImageOptions.ImageIndex = 28;
			iAbout.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iAbout.ImageOptions.SvgImage");
			iAbout.Name = "iAbout";
			iAbout.Visibility = BarItemVisibility.Never;
			iAbout.ItemClick += iAbout_ItemClick;
			// 
			// iPrint
			// 
			iPrint.Caption = "&Print";
			iPrint.CategoryGuid = new Guid("4b511317-d784-42ba-b4ed-0d2a746d6c1f");
			iPrint.Description = "Prints the active document.";
			iPrint.Hint = "Prints the active document";
			iPrint.Id = 5;
			iPrint.ImageOptions.ImageIndex = 9;
			iPrint.ImageOptions.LargeImageIndex = 6;
			iPrint.Name = "iPrint";
			iPrint.RibbonStyle = RibbonItemStyles.SmallWithText | RibbonItemStyles.SmallWithoutText;
			iPrint.ItemClick += iPrint_ItemClick;
			// 
			// iClose
			// 
			iClose.Caption = "&Close";
			iClose.CategoryGuid = new Guid("4b511317-d784-42ba-b4ed-0d2a746d6c1f");
			iClose.Description = "Close the application menu";
			iClose.Hint = "Back to HOME";
			iClose.Id = 2;
			iClose.ImageOptions.ImageIndex = 12;
			iClose.ImageOptions.LargeImageIndex = 8;
			iClose.Name = "iClose";
			iClose.RibbonStyle = RibbonItemStyles.SmallWithText | RibbonItemStyles.SmallWithoutText;
			iClose.ItemClick += iClose_ItemClick;
			// 
			// repositoryItemSpinEdit1
			// 
			repositoryItemSpinEdit1.AutoHeight = false;
			repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
			repositoryItemSpinEdit1.MaxValue = new decimal(new int[] { 50, 0, 0, 0 });
			repositoryItemSpinEdit1.MinValue = new decimal(new int[] { 6, 0, 0, 0 });
			repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
			// 
			// popupControlContainer1
			// 
			popupControlContainer1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			popupControlContainer1.Location = new Point(0, 0);
			popupControlContainer1.Name = "popupControlContainer1";
			popupControlContainer1.Size = new Size(0, 0);
			popupControlContainer1.TabIndex = 6;
			popupControlContainer1.Visible = false;
			// 
			// ribbonControl
			// 
			ribbonControl.AllowCustomization = true;
			ribbonControl.ApplicationButtonDropDownControl = pmAppMain;
			ribbonControl.ApplicationButtonImageOptions.Image = Properties.Resources.SimpleObjects_Large;
			ribbonControl.ApplicationButtonText = "APPLICATION";
			ribbonControl.AutoSizeItems = true;
			ribbonControl.Categories.AddRange(new BarManagerCategory[] { new BarManagerCategory("File", new Guid("4b511317-d784-42ba-b4ed-0d2a746d6c1f")), new BarManagerCategory("Edit", new Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1")), new BarManagerCategory("Format", new Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258")), new BarManagerCategory("Help", new Guid("e07a4c24-66ac-4de6-bbcb-c0b6cfa7798b")), new BarManagerCategory("Status", new Guid("77795bb7-9bc5-4dd2-a297-cc758682e23d")) });
			ribbonControl.ExpandCollapseItem.Id = 0;
			ribbonControl.Images = imageCollection2;
			ribbonControl.Items.AddRange(new BarItem[] { ribbonControl.ExpandCollapseItem, iProtected, iWeb, iAbout, sbiSave, barButtonItemInfo, siDocName, rgbiSkins, barEditItemRibbonStyle, editButtonGroup, barEditItemColorScheme, bbColorMix, barButtonItem1, biOnlineHelp, biGettingStarted, iPrint, iClose, biContact, rgbiColorScheme, barSubItemChangeSkin, barButtonItemConnect, barEditItemMonitorServer, barEditItemMonitorServerPort, barButtonItemDisconnect, barEditItem2, barButtonItemServerStart, barButtonItemServerStop, barButtonItemServerRestart, barStaticItemUser, barStaticItemMonitorServer, barToggleSwitchItemDarkMode, barToggleSwitchItemCompactView, buttonSettings, buttonHelp, barStaticItemPoweredByInfo });
			ribbonControl.LargeImages = imageCollection1;
			ribbonControl.Location = new Point(0, 0);
			ribbonControl.MaxItemId = 497;
			ribbonControl.MiniToolbars.Add(selectionMiniToolbar);
			ribbonControl.Name = "ribbonControl";
			ribbonControl.OptionsTouch.ShowTouchUISelectorInQAT = true;
			ribbonControl.OptionsTouch.ShowTouchUISelectorVisibilityItemInQATMenu = true;
			ribbonControl.PageHeaderItemLinks.Add(barEditItemRibbonStyle);
			ribbonControl.PageHeaderItemLinks.Add(barButtonItem1);
			ribbonControl.Pages.AddRange(new RibbonPage[] { ribbonPageHome, rpHelp });
			ribbonControl.QuickToolbarItemLinks.Add(barSubItemChangeSkin);
			ribbonControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemSpinEdit1, repositoryItemPictureEdit1, riicStyle, repositoryItemComboBox1, repositoryItemTrackBar1, repositoryItemColorPickEdit1, repositoryItemComboBox2, repositoryItemSpinEdit2, repositoryItemComboBox3 });
			ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.True;
			ribbonControl.ShowItemCaptionsInPageHeader = true;
			ribbonControl.Size = new Size(1762, 158);
			ribbonControl.StatusBar = ribbonStatusBar1;
			ribbonControl.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.True;
			ribbonControl.BeforeApplicationButtonContentControlShow += ribbonControl1_BeforeApplicationButtonContentControlShow;
			ribbonControl.ApplicationButtonDoubleClick += ribbonControl1_ApplicationButtonDoubleClick;
			ribbonControl.ResetLayout += ribbonControl1_ResetLayout;
			// 
			// pmAppMain
			// 
			pmAppMain.BottomPaneControlContainer = pccBottom;
			pmAppMain.ItemLinks.Add(buttonSettings);
			pmAppMain.ItemLinks.Add(iPrint);
			pmAppMain.ItemLinks.Add(buttonHelp);
			pmAppMain.ItemLinks.Add(iAbout, false, "", "", true);
			pmAppMain.Name = "pmAppMain";
			pmAppMain.Ribbon = ribbonControl;
			pmAppMain.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			pmAppMain.ShowRightPane = true;
			// 
			// pccBottom
			// 
			pccBottom.Appearance.BackColor = Color.Transparent;
			pccBottom.Appearance.Options.UseBackColor = true;
			pccBottom.AutoSize = true;
			pccBottom.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			pccBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			pccBottom.Controls.Add(sbExit);
			pccBottom.Location = new Point(2115, 620);
			pccBottom.Name = "pccBottom";
			pccBottom.Ribbon = ribbonControl;
			pccBottom.Size = new Size(114, 30);
			pccBottom.TabIndex = 6;
			pccBottom.Visible = false;
			// 
			// sbExit
			// 
			sbExit.AllowFocus = false;
			sbExit.AutoSize = true;
			sbExit.ImageOptions.ImageIndex = 13;
			sbExit.ImageOptions.ImageList = imageCollection2;
			sbExit.Location = new Point(5, 5);
			sbExit.Name = "sbExit";
			sbExit.Size = new Size(106, 22);
			sbExit.TabIndex = 0;
			sbExit.Text = "E&xit Application";
			sbExit.Click += sbExit_Click;
			// 
			// imageCollection2
			// 
			imageCollection2.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("imageCollection2.ImageStream");
			imageCollection2.Images.SetKeyName(29, "SaveAs_16x16.png");
			// 
			// buttonSettings
			// 
			buttonSettings.Caption = "Settings";
			buttonSettings.Id = 492;
			buttonSettings.ImageOptions.Image = (Image)resources.GetObject("buttonSettings.ImageOptions.Image");
			buttonSettings.ImageOptions.LargeImage = (Image)resources.GetObject("buttonSettings.ImageOptions.LargeImage");
			buttonSettings.Name = "buttonSettings";
			buttonSettings.ItemClick += buttonSettings_ItemClick;
			// 
			// buttonHelp
			// 
			buttonHelp.Caption = "Help";
			buttonHelp.Id = 494;
			buttonHelp.ImageOptions.Image = (Image)resources.GetObject("buttonHelp.ImageOptions.Image");
			buttonHelp.ImageOptions.LargeImage = (Image)resources.GetObject("buttonHelp.ImageOptions.LargeImage");
			buttonHelp.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(biGettingStarted), new LinkPersistInfo(biOnlineHelp), new LinkPersistInfo(iWeb), new LinkPersistInfo(iAbout) });
			buttonHelp.Name = "buttonHelp";
			// 
			// biGettingStarted
			// 
			biGettingStarted.Caption = "Getting Started";
			biGettingStarted.Id = 424;
			biGettingStarted.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("biGettingStarted.ImageOptions.SvgImage");
			biGettingStarted.Name = "biGettingStarted";
			// 
			// biOnlineHelp
			// 
			biOnlineHelp.Caption = "Online Help";
			biOnlineHelp.Id = 423;
			biOnlineHelp.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("biOnlineHelp.ImageOptions.SvgImage");
			biOnlineHelp.Name = "biOnlineHelp";
			// 
			// iProtected
			// 
			iProtected.ButtonStyle = BarButtonStyle.Check;
			iProtected.Caption = "P&rotected";
			iProtected.CategoryGuid = new Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258");
			iProtected.Description = "Protects the selected text.";
			iProtected.Hint = "Protects the selected text";
			iProtected.Id = 19;
			iProtected.Name = "iProtected";
			// 
			// sbiSave
			// 
			sbiSave.Caption = "Save";
			sbiSave.CategoryGuid = new Guid("4b511317-d784-42ba-b4ed-0d2a746d6c1f");
			sbiSave.Description = "Saves the active document";
			sbiSave.Hint = "Saves the active document";
			sbiSave.Id = 0;
			sbiSave.ImageOptions.ImageIndex = 10;
			sbiSave.ImageOptions.LargeImageIndex = 2;
			sbiSave.MenuCaption = "Save";
			sbiSave.Name = "sbiSave";
			sbiSave.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			// 
			// barButtonItemInfo
			// 
			barButtonItemInfo.CategoryGuid = new Guid("77795bb7-9bc5-4dd2-a297-cc758682e23d");
			barButtonItemInfo.Id = 1;
			barButtonItemInfo.ImageOptions.ImageIndex = 27;
			barButtonItemInfo.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItemInfo.ImageOptions.SvgImage");
			barButtonItemInfo.Name = "barButtonItemInfo";
			// 
			// siDocName
			// 
			siDocName.CategoryGuid = new Guid("77795bb7-9bc5-4dd2-a297-cc758682e23d");
			siDocName.Id = 2;
			siDocName.Name = "siDocName";
			// 
			// rgbiSkins
			// 
			rgbiSkins.Caption = "Skin Galery";
			// 
			// 
			// 
			rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Hovered.Options.UseFont = true;
			rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Hovered.Options.UseTextOptions = true;
			rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Hovered.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseFont = true;
			rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseTextOptions = true;
			rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Pressed.Options.UseFont = true;
			rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Pressed.Options.UseTextOptions = true;
			rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Pressed.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			rgbiSkins.Id = 13;
			rgbiSkins.Name = "rgbiSkins";
			// 
			// barEditItemRibbonStyle
			// 
			barEditItemRibbonStyle.Caption = "Ribbon Style: ";
			barEditItemRibbonStyle.Edit = riicStyle;
			barEditItemRibbonStyle.EditWidth = 85;
			barEditItemRibbonStyle.Hint = "Ribbon Style";
			barEditItemRibbonStyle.Id = 106;
			barEditItemRibbonStyle.Name = "barEditItemRibbonStyle";
			barEditItemRibbonStyle.VisibleInSearchMenu = false;
			// 
			// riicStyle
			// 
			riicStyle.Appearance.Font = new Font("Tahoma", 6.75F);
			riicStyle.Appearance.Options.UseFont = true;
			riicStyle.AutoHeight = false;
			riicStyle.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
			riicStyle.Name = "riicStyle";
			// 
			// editButtonGroup
			// 
			editButtonGroup.Id = 145;
			editButtonGroup.Name = "editButtonGroup";
			editButtonGroup.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			// 
			// barEditItemColorScheme
			// 
			barEditItemColorScheme.Caption = "Color Scheme: ";
			barEditItemColorScheme.Edit = repositoryItemComboBox1;
			barEditItemColorScheme.EditWidth = 75;
			barEditItemColorScheme.Id = 188;
			barEditItemColorScheme.Name = "barEditItemColorScheme";
			// 
			// repositoryItemComboBox1
			// 
			repositoryItemComboBox1.AutoHeight = false;
			repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
			repositoryItemComboBox1.Name = "repositoryItemComboBox1";
			repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			// 
			// bbColorMix
			// 
			bbColorMix.Caption = "&Color Mix";
			bbColorMix.Id = 238;
			bbColorMix.ImageOptions.Image = (Image)resources.GetObject("bbColorMix.ImageOptions.Image");
			bbColorMix.ImageOptions.LargeImage = (Image)resources.GetObject("bbColorMix.ImageOptions.LargeImage");
			bbColorMix.ImageOptions.LargeImageIndex = 0;
			bbColorMix.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("bbColorMix.ImageOptions.SvgImage");
			bbColorMix.Name = "bbColorMix";
			bbColorMix.ItemClick += bbColorMix_ItemClick;
			// 
			// barButtonItem1
			// 
			barButtonItem1.Caption = "Share";
			barButtonItem1.Id = 420;
			barButtonItem1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem1.ImageOptions.SvgImage");
			barButtonItem1.Name = "barButtonItem1";
			barButtonItem1.VisibleInSearchMenu = false;
			// 
			// biContact
			// 
			biContact.Caption = "Contact Us";
			biContact.Id = 425;
			biContact.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("biContact.ImageOptions.SvgImage");
			biContact.Name = "biContact";
			// 
			// rgbiColorScheme
			// 
			rgbiColorScheme.Caption = "Color Scheme";
			// 
			// 
			// 
			galleryItemGroup1.Caption = "Schemes";
			galleryItem1.Caption = "Yellow";
			galleryItem1.ImageOptions.Image = Properties.Resources.Scheme;
			galleryItem1.Visible = false;
			galleryItem2.AppearanceCaption.Normal.ForeColor = Color.FromArgb(0, 114, 198);
			galleryItem2.AppearanceCaption.Normal.Options.UseForeColor = true;
			galleryItem2.Caption = "Blue";
			galleryItem3.AppearanceCaption.Normal.ForeColor = Color.FromArgb(0, 135, 82);
			galleryItem3.AppearanceCaption.Normal.Options.UseForeColor = true;
			galleryItem3.Caption = "Green";
			galleryItem4.AppearanceCaption.Normal.ForeColor = Color.FromArgb(197, 68, 24);
			galleryItem4.AppearanceCaption.Normal.Options.UseForeColor = true;
			galleryItem4.Caption = "Orange";
			galleryItem4.Checked = true;
			galleryItem5.AppearanceCaption.Normal.ForeColor = Color.FromArgb(179, 75, 223);
			galleryItem5.AppearanceCaption.Normal.Options.UseForeColor = true;
			galleryItem5.Caption = "Purple";
			galleryItem6.AppearanceCaption.Normal.ForeColor = Color.FromArgb(0, 114, 198);
			galleryItem6.AppearanceCaption.Normal.Options.UseForeColor = true;
			galleryItem6.Caption = "Default";
			galleryItem7.AppearanceCaption.Normal.ForeColor = Color.FromArgb(0, 128, 121);
			galleryItem7.AppearanceCaption.Normal.Options.UseForeColor = true;
			galleryItem7.Caption = "Teal";
			galleryItem8.AppearanceCaption.Normal.ForeColor = Color.FromArgb(224, 61, 82);
			galleryItem8.AppearanceCaption.Normal.Options.UseForeColor = true;
			galleryItem8.Caption = "Red";
			galleryItem9.AppearanceCaption.Normal.ForeColor = Color.FromArgb(0, 114, 198);
			galleryItem9.AppearanceCaption.Normal.Options.UseForeColor = true;
			galleryItem9.Caption = "Dark Blue";
			galleryItemGroup1.Items.AddRange(new GalleryItem[] { galleryItem1, galleryItem2, galleryItem3, galleryItem4, galleryItem5, galleryItem6, galleryItem7, galleryItem8, galleryItem9 });
			rgbiColorScheme.Gallery.Groups.AddRange(new GalleryItemGroup[] { galleryItemGroup1 });
			rgbiColorScheme.Gallery.ImageSize = new Size(32, 32);
			rgbiColorScheme.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.SingleRadio;
			rgbiColorScheme.Gallery.InitDropDownGallery += rgbiColorScheme_Gallery_InitDropDownGallery;
			rgbiColorScheme.Gallery.ItemCheckedChanged += rgbiColorScheme_Gallery_ItemCheckedChanged;
			rgbiColorScheme.Id = 426;
			rgbiColorScheme.Name = "rgbiColorScheme";
			// 
			// barSubItemChangeSkin
			// 
			barSubItemChangeSkin.Caption = "Paint style";
			barSubItemChangeSkin.Description = "Select a paint style scheme";
			barSubItemChangeSkin.Hint = "Select a paint style scheme";
			barSubItemChangeSkin.Id = 7;
			barSubItemChangeSkin.ImageOptions.ImageIndex = 26;
			barSubItemChangeSkin.Name = "barSubItemChangeSkin";
			// 
			// barButtonItemConnect
			// 
			barButtonItemConnect.Caption = "Connect";
			barButtonItemConnect.Id = 454;
			barButtonItemConnect.ImageOptions.Image = Properties.Resources.Server_Connect;
			barButtonItemConnect.ImageOptions.LargeImage = Properties.Resources.Server_Connect_Large;
			barButtonItemConnect.Name = "barButtonItemConnect";
			barButtonItemConnect.ItemClick += barButtonItemConnect_ItemClick;
			// 
			// barEditItemMonitorServer
			// 
			barEditItemMonitorServer.Caption = "Server";
			barEditItemMonitorServer.Edit = repositoryItemComboBox2;
			barEditItemMonitorServer.EditWidth = 140;
			barEditItemMonitorServer.Id = 456;
			barEditItemMonitorServer.Name = "barEditItemMonitorServer";
			// 
			// repositoryItemComboBox2
			// 
			repositoryItemComboBox2.AutoHeight = false;
			repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
			repositoryItemComboBox2.Name = "repositoryItemComboBox2";
			// 
			// barEditItemMonitorServerPort
			// 
			barEditItemMonitorServerPort.Caption = "Server Monitor Port";
			barEditItemMonitorServerPort.Edit = repositoryItemSpinEdit2;
			barEditItemMonitorServerPort.EditWidth = 70;
			barEditItemMonitorServerPort.Id = 457;
			barEditItemMonitorServerPort.Name = "barEditItemMonitorServerPort";
			// 
			// repositoryItemSpinEdit2
			// 
			repositoryItemSpinEdit2.AutoHeight = false;
			repositoryItemSpinEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
			repositoryItemSpinEdit2.MaskSettings.Set("mask", "d");
			repositoryItemSpinEdit2.MaxValue = new decimal(new int[] { 65535, 0, 0, 0 });
			repositoryItemSpinEdit2.Name = "repositoryItemSpinEdit2";
			// 
			// barButtonItemDisconnect
			// 
			barButtonItemDisconnect.Caption = "Disconnect";
			barButtonItemDisconnect.Id = 458;
			barButtonItemDisconnect.ImageOptions.Image = Properties.Resources.Server_Disconnect;
			barButtonItemDisconnect.ImageOptions.LargeImage = Properties.Resources.Server_Disconnect_Large;
			barButtonItemDisconnect.Name = "barButtonItemDisconnect";
			barButtonItemDisconnect.ItemClick += barButtonItemDisconnect_ItemClick;
			// 
			// barEditItem2
			// 
			barEditItem2.Caption = "barEditItem2";
			barEditItem2.Edit = repositoryItemComboBox2;
			barEditItem2.Id = 460;
			barEditItem2.Name = "barEditItem2";
			// 
			// barButtonItemServerStart
			// 
			barButtonItemServerStart.Caption = "Start";
			barButtonItemServerStart.Id = 465;
			barButtonItemServerStart.ImageOptions.Image = Properties.Resources.Play_Green;
			barButtonItemServerStart.ImageOptions.LargeImage = Properties.Resources.Play_Green_Large;
			barButtonItemServerStart.Name = "barButtonItemServerStart";
			barButtonItemServerStart.ItemClick += barButtonItemServerStart_ItemClick;
			// 
			// barButtonItemServerStop
			// 
			barButtonItemServerStop.Caption = "Stop";
			barButtonItemServerStop.Id = 466;
			barButtonItemServerStop.ImageOptions.Image = Properties.Resources.Stop_Red;
			barButtonItemServerStop.ImageOptions.LargeImage = Properties.Resources.Stop_Red_Large;
			barButtonItemServerStop.Name = "barButtonItemServerStop";
			barButtonItemServerStop.ItemClick += barButtonItemServerStop_ItemClick;
			// 
			// barButtonItemServerRestart
			// 
			barButtonItemServerRestart.Caption = "Restart";
			barButtonItemServerRestart.Id = 467;
			barButtonItemServerRestart.ImageOptions.Image = Properties.Resources.Reload;
			barButtonItemServerRestart.ImageOptions.LargeImage = Properties.Resources.Reload_Large;
			barButtonItemServerRestart.Name = "barButtonItemServerRestart";
			barButtonItemServerRestart.ItemClick += barButtonItemServerRestart_ItemClick;
			// 
			// barStaticItemUser
			// 
			barStaticItemUser.Caption = "Monitor Admin";
			barStaticItemUser.Id = 477;
			barStaticItemUser.ImageOptions.Image = Properties.Resources.Administrator;
			barStaticItemUser.Name = "barStaticItemUser";
			barStaticItemUser.PaintStyle = BarItemPaintStyle.CaptionGlyph;
			// 
			// barStaticItemMonitorServer
			// 
			barStaticItemMonitorServer.Caption = "Monitor Server";
			barStaticItemMonitorServer.Id = 479;
			barStaticItemMonitorServer.ImageOptions.Image = Properties.Resources.Server;
			barStaticItemMonitorServer.Name = "barStaticItemMonitorServer";
			barStaticItemMonitorServer.PaintStyle = BarItemPaintStyle.CaptionGlyph;
			// 
			// barToggleSwitchItemDarkMode
			// 
			barToggleSwitchItemDarkMode.Caption = "Dark Mode";
			barToggleSwitchItemDarkMode.Id = 489;
			barToggleSwitchItemDarkMode.Name = "barToggleSwitchItemDarkMode";
			// 
			// barToggleSwitchItemCompactView
			// 
			barToggleSwitchItemCompactView.Caption = "Compact View";
			barToggleSwitchItemCompactView.Id = 490;
			barToggleSwitchItemCompactView.Name = "barToggleSwitchItemCompactView";
			// 
			// barStaticItemPoweredByInfo
			// 
			barStaticItemPoweredByInfo.Alignment = BarItemLinkAlignment.Right;
			barStaticItemPoweredByInfo.Caption = "Powered by Simple.Objects™";
			barStaticItemPoweredByInfo.Id = 496;
			barStaticItemPoweredByInfo.Name = "barStaticItemPoweredByInfo";
			// 
			// imageCollection1
			// 
			imageCollection1.ImageSize = new Size(32, 32);
			imageCollection1.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("imageCollection1.ImageStream");
			// 
			// selectionMiniToolbar
			// 
			selectionMiniToolbar.Alignment = ContentAlignment.TopRight;
			selectionMiniToolbar.ItemLinks.Add(editButtonGroup);
			selectionMiniToolbar.ParentControl = this;
			// 
			// ribbonPageHome
			// 
			ribbonPageHome.Groups.AddRange(new RibbonPageGroup[] { ribbonPageGroupMonitorService, ribbonPageGroupSimpleObjectsServer, ribbonPageGroupSkins });
			ribbonPageHome.Name = "ribbonPageHome";
			ribbonPageHome.Text = "HOME";
			// 
			// ribbonPageGroupMonitorService
			// 
			ribbonPageGroupMonitorService.ItemLinks.Add(barButtonItemConnect);
			ribbonPageGroupMonitorService.ItemLinks.Add(barEditItemMonitorServer, true);
			ribbonPageGroupMonitorService.ItemLinks.Add(barEditItemMonitorServerPort);
			ribbonPageGroupMonitorService.ItemLinks.Add(barButtonItemDisconnect, true);
			ribbonPageGroupMonitorService.Name = "ribbonPageGroupMonitorService";
			ribbonPageGroupMonitorService.Text = "Monitor Service";
			// 
			// ribbonPageGroupSimpleObjectsServer
			// 
			ribbonPageGroupSimpleObjectsServer.ItemLinks.Add(barButtonItemServerStart);
			ribbonPageGroupSimpleObjectsServer.ItemLinks.Add(barButtonItemServerStop);
			ribbonPageGroupSimpleObjectsServer.ItemLinks.Add(barButtonItemServerRestart);
			ribbonPageGroupSimpleObjectsServer.Name = "ribbonPageGroupSimpleObjectsServer";
			ribbonPageGroupSimpleObjectsServer.Text = "Simple.Objects™ Server";
			// 
			// ribbonPageGroupSkins
			// 
			ribbonPageGroupSkins.ItemLinks.Add(rgbiSkins);
			ribbonPageGroupSkins.ItemLinks.Add(rgbiColorScheme);
			ribbonPageGroupSkins.ItemLinks.Add(bbColorMix);
			ribbonPageGroupSkins.ItemLinks.Add(barToggleSwitchItemDarkMode);
			ribbonPageGroupSkins.ItemLinks.Add(barToggleSwitchItemCompactView);
			ribbonPageGroupSkins.Name = "ribbonPageGroupSkins";
			ribbonPageGroupSkins.Text = "Skins";
			// 
			// rpHelp
			// 
			rpHelp.Groups.AddRange(new RibbonPageGroup[] { rpgHelp });
			rpHelp.Name = "rpHelp";
			rpHelp.Text = "HELP";
			// 
			// rpgHelp
			// 
			rpgHelp.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("rpgHelp.ImageOptions.SvgImage");
			rpgHelp.ItemLinks.Add(iWeb);
			rpgHelp.ItemLinks.Add(biOnlineHelp);
			rpgHelp.ItemLinks.Add(biGettingStarted);
			rpgHelp.ItemLinks.Add(biContact);
			rpgHelp.ItemLinks.Add(iAbout);
			rpgHelp.Name = "rpgHelp";
			rpgHelp.Text = "Help";
			// 
			// repositoryItemPictureEdit1
			// 
			repositoryItemPictureEdit1.AllowFocused = false;
			repositoryItemPictureEdit1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
			// 
			// repositoryItemTrackBar1
			// 
			repositoryItemTrackBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			repositoryItemTrackBar1.LabelAppearance.Options.UseTextOptions = true;
			repositoryItemTrackBar1.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			repositoryItemTrackBar1.Maximum = 1000;
			repositoryItemTrackBar1.Name = "repositoryItemTrackBar1";
			// 
			// repositoryItemColorPickEdit1
			// 
			repositoryItemColorPickEdit1.AutoHeight = false;
			repositoryItemColorPickEdit1.AutomaticColor = Color.Black;
			repositoryItemColorPickEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
			repositoryItemColorPickEdit1.Name = "repositoryItemColorPickEdit1";
			// 
			// repositoryItemComboBox3
			// 
			repositoryItemComboBox3.AutoHeight = false;
			repositoryItemComboBox3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
			repositoryItemComboBox3.Name = "repositoryItemComboBox3";
			// 
			// ribbonStatusBar1
			// 
			ribbonStatusBar1.ItemLinks.Add(siDocName, true);
			ribbonStatusBar1.ItemLinks.Add(barStaticItemMonitorServer);
			ribbonStatusBar1.ItemLinks.Add(barStaticItemUser, true);
			ribbonStatusBar1.ItemLinks.Add(barButtonItemInfo);
			ribbonStatusBar1.ItemLinks.Add(barStaticItemPoweredByInfo);
			ribbonStatusBar1.Location = new Point(0, 845);
			ribbonStatusBar1.Name = "ribbonStatusBar1";
			ribbonStatusBar1.Ribbon = ribbonControl;
			ribbonStatusBar1.Size = new Size(1762, 24);
			// 
			// pcAppMenuFileLabels
			// 
			pcAppMenuFileLabels.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			pcAppMenuFileLabels.Dock = DockStyle.Fill;
			pcAppMenuFileLabels.Location = new Point(0, 0);
			pcAppMenuFileLabels.Name = "pcAppMenuFileLabels";
			pcAppMenuFileLabels.Size = new Size(496, 232);
			pcAppMenuFileLabels.TabIndex = 2;
			// 
			// pmNew
			// 
			pmNew.MenuCaption = "New";
			pmNew.Name = "pmNew";
			pmNew.Ribbon = ribbonControl;
			pmNew.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			// 
			// pmMain
			// 
			pmMain.MenuCaption = "Edit Menu";
			pmMain.MultiColumn = DevExpress.Utils.DefaultBoolean.True;
			pmMain.Name = "pmMain";
			pmMain.OptionsMultiColumn.ItemDisplayMode = DevExpress.Utils.Menu.MultiColumnItemDisplayMode.Both;
			pmMain.OptionsMultiColumn.ShowItemText = DevExpress.Utils.DefaultBoolean.True;
			pmMain.Ribbon = ribbonControl;
			pmMain.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.False;
			// 
			// imageCollection3
			// 
			imageCollection3.ImageSize = new Size(15, 15);
			imageCollection3.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("imageCollection3.ImageStream");
			// 
			// backstageViewControl
			// 
			backstageViewControl.BackstageViewShowRibbonItems = BackstageViewShowRibbonItems.FormButtons | BackstageViewShowRibbonItems.Title;
			backstageViewControl.Controls.Add(backstageViewClientControl2);
			backstageViewControl.Controls.Add(backstageViewClientControl8);
			backstageViewControl.Controls.Add(backstageViewClientControl11);
			backstageViewControl.Controls.Add(backstageViewClientControl10);
			backstageViewControl.Images = imageCollection2;
			backstageViewControl.Items.Add(backstageViewButtonItemSettings);
			backstageViewControl.Items.Add(backstageViewTabItem1);
			backstageViewControl.Items.Add(printTabItem);
			backstageViewControl.Items.Add(backstageViewTabItem4);
			backstageViewControl.Items.Add(backstageViewTabItem6);
			backstageViewControl.Items.Add(bvItemClose);
			backstageViewControl.Items.Add(bvItemExit);
			backstageViewControl.Location = new Point(5, 165);
			backstageViewControl.Name = "backstageViewControl";
			backstageViewControl.Office2013StyleOptions.RightPaneContentVerticalOffset = 70;
			backstageViewControl.OwnerControl = ribbonControl;
			backstageViewControl.SelectedTab = backstageViewTabItem6;
			backstageViewControl.SelectedTabIndex = 4;
			backstageViewControl.Size = new Size(1100, 572);
			backstageViewControl.TabIndex = 9;
			backstageViewControl.Text = "backstageViewControl1";
			// 
			// backstageViewClientControl2
			// 
			backstageViewClientControl2.Controls.Add(recentItemControl1);
			backstageViewClientControl2.Location = new Point(132, 71);
			backstageViewClientControl2.Name = "backstageViewClientControl2";
			backstageViewClientControl2.Size = new Size(967, 500);
			backstageViewClientControl2.TabIndex = 10;
			// 
			// recentItemControl1
			// 
			recentItemControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			recentItemControl1.ContentPanelMinWidth = 460;
			recentItemControl1.DefaultContentPanel = recentStackPanel12;
			recentItemControl1.Dock = DockStyle.Fill;
			recentItemControl1.Location = new Point(0, 0);
			recentItemControl1.MainPanel = recentStackPanel13;
			recentItemControl1.MainPanelMinWidth = 180;
			recentItemControl1.Name = "recentItemControl1";
			recentItemControl1.ShowTitle = false;
			recentItemControl1.Size = new Size(967, 500);
			recentItemControl1.SplitterPosition = 507;
			recentItemControl1.TabIndex = 0;
			recentItemControl1.Title = "Title";
			// 
			// recentStackPanel12
			// 
			recentStackPanel12.Caption = "About";
			recentStackPanel12.CaptionToContentIndent = 46;
			recentStackPanel12.Items.AddRange(new RecentItemBase[] { recentLabelItem1, recentHyperlinkItem1, recentLabelItem2 });
			recentStackPanel12.Name = "recentStackPanel12";
			// 
			// recentLabelItem1
			// 
			recentLabelItem1.Caption = null;
			recentLabelItem1.ImageOptions.ItemNormal.Image = (Image)resources.GetObject("recentLabelItem1.ImageOptions.ItemNormal.Image");
			recentLabelItem1.Name = "recentLabelItem1";
			// 
			// recentHyperlinkItem1
			// 
			recentHyperlinkItem1.AllowSelect = DevExpress.Utils.DefaultBoolean.False;
			recentHyperlinkItem1.Caption = "www.simple-objects.com";
			recentHyperlinkItem1.Name = "recentHyperlinkItem1";
			// 
			// recentLabelItem2
			// 
			recentLabelItem2.AllowSelect = DevExpress.Utils.DefaultBoolean.False;
			recentLabelItem2.Caption = "Copyright © 2025 Simple.Objects™. ALL RIGHTS RESERVED.";
			recentLabelItem2.Name = "recentLabelItem2";
			// 
			// recentStackPanel13
			// 
			recentStackPanel13.Caption = "Support";
			recentStackPanel13.CaptionToContentIndent = 46;
			recentStackPanel13.Items.AddRange(new RecentItemBase[] { recentPinItem2, recentPinItem3, recentPinItem4 });
			recentStackPanel13.Name = "recentStackPanel13";
			recentStackPanel13.PanelPadding = new Padding(20, 20, 5, 20);
			// 
			// recentPinItem2
			// 
			recentPinItem2.Caption = "Simple.Objects™ Online Help";
			recentPinItem2.Description = "Get help using DevExpress components";
			recentPinItem2.GlyphAlignment = RecentPinItemGlyphAlignment.Center;
			recentPinItem2.ImageOptions.ItemNormal.SvgImage = Properties.Resources.DevExpressOnlineHelp;
			recentPinItem2.Name = "recentPinItem2";
			recentPinItem2.PinButtonVisibility = RecentPinButtonVisibility.Never;
			// 
			// recentPinItem3
			// 
			recentPinItem3.Caption = "Getting Started";
			recentPinItem3.Description = "See what's new and find resources to help you learn the basics quickly.";
			recentPinItem3.GlyphAlignment = RecentPinItemGlyphAlignment.Center;
			recentPinItem3.ImageOptions.ItemNormal.SvgImage = Properties.Resources.GettingStarted;
			recentPinItem3.Name = "recentPinItem3";
			recentPinItem3.PinButtonVisibility = RecentPinButtonVisibility.Never;
			// 
			// recentPinItem4
			// 
			recentPinItem4.Caption = "Contact Us";
			recentPinItem4.Description = "Let us know if you need help or how we can make our components better";
			recentPinItem4.GlyphAlignment = RecentPinItemGlyphAlignment.Center;
			recentPinItem4.ImageOptions.ItemNormal.SvgImage = Properties.Resources.ContactUs;
			recentPinItem4.Name = "recentPinItem4";
			recentPinItem4.PinButtonVisibility = RecentPinButtonVisibility.Never;
			// 
			// backstageViewClientControl8
			// 
			backstageViewClientControl8.Controls.Add(recentSaveAs);
			backstageViewClientControl8.Location = new Point(132, 71);
			backstageViewClientControl8.Name = "backstageViewClientControl8";
			backstageViewClientControl8.Size = new Size(967, 500);
			backstageViewClientControl8.TabIndex = 6;
			// 
			// recentSaveAs
			// 
			recentSaveAs.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			recentSaveAs.ContentPanelMinWidth = 100;
			recentSaveAs.DefaultContentPanel = recentStackPanel5;
			recentSaveAs.Dock = DockStyle.Fill;
			recentSaveAs.Location = new Point(0, 0);
			recentSaveAs.MainPanel = recentStackPanel6;
			recentSaveAs.MainPanelMinWidth = 100;
			recentSaveAs.MinimumSize = new Size(400, 200);
			recentSaveAs.Name = "recentSaveAs";
			recentSaveAs.SelectedTab = recentTabItem3;
			recentSaveAs.ShowTitle = false;
			recentSaveAs.Size = new Size(967, 500);
			recentSaveAs.TabIndex = 0;
			recentSaveAs.Title = "Save As";
			// 
			// recentStackPanel5
			// 
			recentStackPanel5.Caption = "Computer";
			recentStackPanel5.CaptionToContentIndent = 46;
			recentStackPanel5.ImageOptions.SvgImage = Properties.Resources.Electronics_DesktopMac;
			recentStackPanel5.Name = "recentStackPanel5";
			// 
			// recentStackPanel6
			// 
			recentStackPanel6.Caption = "Save As";
			recentStackPanel6.CaptionToContentIndent = 46;
			recentStackPanel6.Items.AddRange(new RecentItemBase[] { recentTabItem3 });
			recentStackPanel6.Name = "recentStackPanel6";
			recentStackPanel6.SelectedItem = recentTabItem3;
			// 
			// recentTabItem3
			// 
			recentTabItem3.Caption = "Computer";
			recentTabItem3.ImageOptions.ItemNormal.SvgImage = Properties.Resources.Electronics_DesktopMac;
			recentTabItem3.Name = "recentTabItem3";
			recentTabItem3.TabPanel = recentStackPanel7;
			// 
			// recentStackPanel7
			// 
			recentStackPanel7.Caption = "Computer";
			recentStackPanel7.CaptionToContentIndent = 46;
			recentStackPanel7.ImageOptions.SvgImage = Properties.Resources.Electronics_DesktopMac;
			recentStackPanel7.Name = "recentStackPanel7";
			// 
			// backstageViewClientControl11
			// 
			backstageViewClientControl11.Controls.Add(recentControlPrint);
			backstageViewClientControl11.Location = new Point(132, 71);
			backstageViewClientControl11.Name = "backstageViewClientControl11";
			backstageViewClientControl11.Size = new Size(967, 500);
			backstageViewClientControl11.TabIndex = 9;
			// 
			// recentControlPrint
			// 
			recentControlPrint.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			recentControlPrint.ContentPanelMinWidth = 100;
			recentControlPrint.Controls.Add(recentPrintOptionsContainer);
			recentControlPrint.Controls.Add(recentPrintPreviewContainer);
			recentControlPrint.DefaultContentPanel = recentStackPanel8;
			recentControlPrint.Dock = DockStyle.Fill;
			recentControlPrint.Location = new Point(0, 0);
			recentControlPrint.MainPanel = recentStackPanel9;
			recentControlPrint.MainPanelMinWidth = 100;
			recentControlPrint.Name = "recentControlPrint";
			recentControlPrint.ShowTitle = false;
			recentControlPrint.Size = new Size(967, 500);
			recentControlPrint.TabIndex = 0;
			recentControlPrint.Title = "Print";
			// 
			// recentPrintOptionsContainer
			// 
			recentPrintOptionsContainer.Appearance.BackColor = SystemColors.Control;
			recentPrintOptionsContainer.Appearance.Options.UseBackColor = true;
			recentPrintOptionsContainer.Controls.Add(layoutControl1);
			recentPrintOptionsContainer.Name = "recentPrintOptionsContainer";
			recentPrintOptionsContainer.Size = new Size(268, 408);
			recentPrintOptionsContainer.TabIndex = 1;
			// 
			// layoutControl1
			// 
			layoutControl1.Controls.Add(ddbDuplex);
			layoutControl1.Controls.Add(ddbOrientation);
			layoutControl1.Controls.Add(ddbPaperSize);
			layoutControl1.Controls.Add(ddbMargins);
			layoutControl1.Controls.Add(ddbCollate);
			layoutControl1.Controls.Add(ddbPrinter);
			layoutControl1.Controls.Add(printButton);
			layoutControl1.Controls.Add(copySpinEdit);
			layoutControl1.Dock = DockStyle.Fill;
			layoutControl1.Location = new Point(0, 0);
			layoutControl1.Name = "layoutControl1";
			layoutControl1.Root = layoutControlGroup1;
			layoutControl1.Size = new Size(268, 408);
			layoutControl1.TabIndex = 0;
			layoutControl1.Text = "layoutControl1";
			// 
			// ddbDuplex
			// 
			ddbDuplex.Appearance.Options.UseTextOptions = true;
			ddbDuplex.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			ddbDuplex.Location = new Point(2, 186);
			ddbDuplex.Name = "ddbDuplex";
			ddbDuplex.Size = new Size(264, 52);
			ddbDuplex.StyleController = layoutControl1;
			ddbDuplex.TabIndex = 17;
			ddbDuplex.Text = "Print OneSided";
			// 
			// ddbOrientation
			// 
			ddbOrientation.Appearance.Options.UseTextOptions = true;
			ddbOrientation.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			ddbOrientation.Location = new Point(2, 242);
			ddbOrientation.Name = "ddbOrientation";
			ddbOrientation.Size = new Size(264, 52);
			ddbOrientation.StyleController = layoutControl1;
			ddbOrientation.TabIndex = 13;
			ddbOrientation.Text = "Orientation";
			// 
			// ddbPaperSize
			// 
			ddbPaperSize.Appearance.Options.UseTextOptions = true;
			ddbPaperSize.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			ddbPaperSize.Location = new Point(2, 298);
			ddbPaperSize.Name = "ddbPaperSize";
			ddbPaperSize.Size = new Size(264, 52);
			ddbPaperSize.StyleController = layoutControl1;
			ddbPaperSize.TabIndex = 15;
			ddbPaperSize.Text = "Paper Size";
			// 
			// ddbMargins
			// 
			ddbMargins.Appearance.Options.UseTextOptions = true;
			ddbMargins.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			ddbMargins.Location = new Point(2, 354);
			ddbMargins.Name = "ddbMargins";
			ddbMargins.Size = new Size(264, 52);
			ddbMargins.StyleController = layoutControl1;
			ddbMargins.TabIndex = 14;
			ddbMargins.Text = "Margins";
			// 
			// ddbCollate
			// 
			ddbCollate.Appearance.Options.UseTextOptions = true;
			ddbCollate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			ddbCollate.Location = new Point(2, 410);
			ddbCollate.Name = "ddbCollate";
			ddbCollate.Size = new Size(264, 52);
			ddbCollate.StyleController = layoutControl1;
			ddbCollate.TabIndex = 16;
			ddbCollate.Text = "Collated";
			// 
			// ddbPrinter
			// 
			ddbPrinter.Appearance.Options.UseTextOptions = true;
			ddbPrinter.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			ddbPrinter.AutoWidthInLayoutControl = true;
			ddbPrinter.Location = new Point(0, 104);
			ddbPrinter.Name = "ddbPrinter";
			ddbPrinter.Size = new Size(268, 56);
			ddbPrinter.StyleController = layoutControl1;
			ddbPrinter.TabIndex = 11;
			ddbPrinter.Text = "Printer";
			// 
			// printButton
			// 
			printButton.ImageOptions.Location = ImageLocation.TopCenter;
			printButton.ImageOptions.SvgImage = Properties.Resources.Print;
			printButton.Location = new Point(2, 2);
			printButton.Name = "printButton";
			printButton.Size = new Size(80, 76);
			printButton.StyleController = layoutControl1;
			printButton.TabIndex = 5;
			printButton.Text = "Print";
			printButton.Click += printButton_Click;
			// 
			// copySpinEdit
			// 
			copySpinEdit.EditValue = new decimal(new int[] { 1, 0, 0, 0 });
			copySpinEdit.Location = new Point(152, 2);
			copySpinEdit.Name = "copySpinEdit";
			copySpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
			copySpinEdit.Properties.IsFloatValue = false;
			copySpinEdit.Properties.Mask.EditMask = "N00";
			copySpinEdit.Properties.MaxValue = new decimal(new int[] { 30, 0, 0, 0 });
			copySpinEdit.Properties.MinValue = new decimal(new int[] { 1, 0, 0, 0 });
			copySpinEdit.Size = new Size(92, 20);
			copySpinEdit.StyleController = layoutControl1;
			copySpinEdit.TabIndex = 6;
			// 
			// layoutControlGroup1
			// 
			layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			layoutControlGroup1.GroupBordersVisible = false;
			layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, lciCopiesSpinEdit, layoutControlItem3, layoutControlItem2, layoutControlItem4, layoutControlItem5, layoutControlItem6, layoutControlItem7, layoutControlItem8, layoutControlItem9 });
			layoutControlGroup1.Name = "layoutControlGroup1";
			layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			layoutControlGroup1.Size = new Size(268, 464);
			layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			layoutControlItem1.Control = printButton;
			layoutControlItem1.Location = new Point(0, 0);
			layoutControlItem1.MaxSize = new Size(84, 80);
			layoutControlItem1.MinSize = new Size(84, 80);
			layoutControlItem1.Name = "layoutControlItem1";
			layoutControlItem1.Size = new Size(84, 80);
			layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem1.TextSize = new Size(0, 0);
			layoutControlItem1.TextVisible = false;
			// 
			// lciCopiesSpinEdit
			// 
			lciCopiesSpinEdit.Control = copySpinEdit;
			lciCopiesSpinEdit.CustomizationFormText = "Copies:";
			lciCopiesSpinEdit.Location = new Point(84, 0);
			lciCopiesSpinEdit.MaxSize = new Size(180, 24);
			lciCopiesSpinEdit.MinSize = new Size(180, 24);
			lciCopiesSpinEdit.Name = "lciCopiesSpinEdit";
			lciCopiesSpinEdit.Padding = new DevExpress.XtraLayout.Utils.Padding(20, 20, 2, 2);
			lciCopiesSpinEdit.Size = new Size(184, 80);
			lciCopiesSpinEdit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			lciCopiesSpinEdit.Text = "Copies:";
			// 
			// layoutControlItem3
			// 
			layoutControlItem3.Location = new Point(0, 80);
			layoutControlItem3.Name = "layoutControlItem3";
			layoutControlItem3.Size = new Size(268, 24);
			layoutControlItem3.TextSize = new Size(0, 0);
			layoutControlItem3.TextVisible = false;
			// 
			// layoutControlItem2
			// 
			layoutControlItem2.Control = ddbPrinter;
			layoutControlItem2.Location = new Point(0, 104);
			layoutControlItem2.MaxSize = new Size(0, 56);
			layoutControlItem2.MinSize = new Size(100, 56);
			layoutControlItem2.Name = "layoutControlItem2";
			layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			layoutControlItem2.Size = new Size(268, 56);
			layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem2.TextSize = new Size(0, 0);
			layoutControlItem2.TextVisible = false;
			// 
			// layoutControlItem4
			// 
			layoutControlItem4.Location = new Point(0, 160);
			layoutControlItem4.Name = "layoutControlItem4";
			layoutControlItem4.Size = new Size(268, 24);
			layoutControlItem4.TextSize = new Size(0, 0);
			layoutControlItem4.TextVisible = false;
			// 
			// layoutControlItem5
			// 
			layoutControlItem5.Control = ddbDuplex;
			layoutControlItem5.Location = new Point(0, 184);
			layoutControlItem5.MaxSize = new Size(0, 56);
			layoutControlItem5.MinSize = new Size(100, 56);
			layoutControlItem5.Name = "layoutControlItem5";
			layoutControlItem5.Size = new Size(268, 56);
			layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem5.TextSize = new Size(0, 0);
			layoutControlItem5.TextVisible = false;
			// 
			// layoutControlItem6
			// 
			layoutControlItem6.Control = ddbOrientation;
			layoutControlItem6.Location = new Point(0, 240);
			layoutControlItem6.MaxSize = new Size(0, 56);
			layoutControlItem6.MinSize = new Size(83, 56);
			layoutControlItem6.Name = "layoutControlItem6";
			layoutControlItem6.Size = new Size(268, 56);
			layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem6.TextSize = new Size(0, 0);
			layoutControlItem6.TextVisible = false;
			// 
			// layoutControlItem7
			// 
			layoutControlItem7.Control = ddbPaperSize;
			layoutControlItem7.Location = new Point(0, 296);
			layoutControlItem7.MaxSize = new Size(0, 56);
			layoutControlItem7.MinSize = new Size(79, 56);
			layoutControlItem7.Name = "layoutControlItem7";
			layoutControlItem7.Size = new Size(268, 56);
			layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem7.TextSize = new Size(0, 0);
			layoutControlItem7.TextVisible = false;
			// 
			// layoutControlItem8
			// 
			layoutControlItem8.Control = ddbMargins;
			layoutControlItem8.Location = new Point(0, 352);
			layoutControlItem8.MaxSize = new Size(0, 56);
			layoutControlItem8.MinSize = new Size(66, 56);
			layoutControlItem8.Name = "layoutControlItem8";
			layoutControlItem8.Size = new Size(268, 56);
			layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem8.TextSize = new Size(0, 0);
			layoutControlItem8.TextVisible = false;
			// 
			// layoutControlItem9
			// 
			layoutControlItem9.Control = ddbCollate;
			layoutControlItem9.Location = new Point(0, 408);
			layoutControlItem9.MaxSize = new Size(0, 56);
			layoutControlItem9.MinSize = new Size(68, 56);
			layoutControlItem9.Name = "layoutControlItem9";
			layoutControlItem9.Size = new Size(268, 56);
			layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem9.TextSize = new Size(0, 0);
			layoutControlItem9.TextVisible = false;
			// 
			// recentPrintPreviewContainer
			// 
			recentPrintPreviewContainer.Appearance.BackColor = SystemColors.Control;
			recentPrintPreviewContainer.Appearance.Options.UseBackColor = true;
			recentPrintPreviewContainer.Controls.Add(panelControl2);
			recentPrintPreviewContainer.Name = "recentPrintPreviewContainer";
			recentPrintPreviewContainer.Size = new Size(634, 488);
			recentPrintPreviewContainer.TabIndex = 2;
			// 
			// panelControl2
			// 
			panelControl2.Controls.Add(printControl2);
			panelControl2.Controls.Add(panelControl3);
			panelControl2.Dock = DockStyle.Fill;
			panelControl2.Location = new Point(0, 0);
			panelControl2.Name = "panelControl2";
			panelControl2.Size = new Size(634, 488);
			panelControl2.TabIndex = 0;
			// 
			// printControl2
			// 
			printControl2.Dock = DockStyle.Fill;
			printControl2.IsMetric = true;
			printControl2.Location = new Point(2, 2);
			printControl2.Name = "printControl2";
			printControl2.Size = new Size(630, 444);
			printControl2.TabIndex = 3;
			printControl2.SelectedPageChanged += printControl2_SelectedPageChanged;
			// 
			// panelControl3
			// 
			panelControl3.Controls.Add(stackPanel1);
			panelControl3.Controls.Add(panel2);
			panelControl3.Controls.Add(pageButtonEdit);
			panelControl3.Controls.Add(zoomTrackBarControl1);
			panelControl3.Dock = DockStyle.Bottom;
			panelControl3.Location = new Point(2, 446);
			panelControl3.Name = "panelControl3";
			panelControl3.Size = new Size(630, 40);
			panelControl3.TabIndex = 2;
			// 
			// stackPanel1
			// 
			stackPanel1.Controls.Add(zoomTextEdit);
			stackPanel1.Dock = DockStyle.Right;
			stackPanel1.LayoutDirection = DevExpress.Utils.Layout.StackPanelLayoutDirection.RightToLeft;
			stackPanel1.Location = new Point(-70, 2);
			stackPanel1.Name = "stackPanel1";
			stackPanel1.Size = new Size(340, 36);
			stackPanel1.TabIndex = 7;
			// 
			// zoomTextEdit
			// 
			zoomTextEdit.Dock = DockStyle.Right;
			zoomTextEdit.EditValue = (short)100;
			zoomTextEdit.Location = new Point(220, 8);
			zoomTextEdit.Name = "zoomTextEdit";
			zoomTextEdit.Properties.DisplayFormat.FormatString = "{0}%";
			zoomTextEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			zoomTextEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			zoomTextEdit.Properties.Mask.EditMask = "n0";
			zoomTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
			zoomTextEdit.Size = new Size(117, 20);
			zoomTextEdit.TabIndex = 6;
			zoomTextEdit.EditValueChanged += zoomTextEdit_EditValueChanged;
			// 
			// panel2
			// 
			panel2.Dock = DockStyle.Right;
			panel2.Location = new Point(270, 2);
			panel2.Name = "panel2";
			panel2.Size = new Size(16, 36);
			panel2.TabIndex = 5;
			// 
			// pageButtonEdit
			// 
			pageButtonEdit.Dock = DockStyle.Left;
			pageButtonEdit.EditValue = "1";
			pageButtonEdit.Location = new Point(2, 2);
			pageButtonEdit.Name = "pageButtonEdit";
			pageButtonEdit.Properties.Appearance.Options.UseTextOptions = true;
			pageButtonEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			pageButtonEdit.Properties.AutoHeight = false;
			pageButtonEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			pageButtonEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Left, "", -1, true, true, true, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default), new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Right) });
			pageButtonEdit.Properties.DisplayFormat.FormatString = "Page {0} of 1";
			pageButtonEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			pageButtonEdit.Size = new Size(188, 36);
			pageButtonEdit.TabIndex = 4;
			pageButtonEdit.ButtonClick += pageButtonEdit_ButtonClick;
			pageButtonEdit.EditValueChanged += pageButtonEdit_EditValueChanged;
			pageButtonEdit.EditValueChanging += pageButtonEdit_EditValueChanging;
			// 
			// zoomTrackBarControl1
			// 
			zoomTrackBarControl1.Dock = DockStyle.Right;
			zoomTrackBarControl1.EditValue = 40;
			zoomTrackBarControl1.Location = new Point(286, 2);
			zoomTrackBarControl1.MenuManager = ribbonControl;
			zoomTrackBarControl1.Name = "zoomTrackBarControl1";
			zoomTrackBarControl1.Properties.Maximum = 80;
			zoomTrackBarControl1.Properties.Middle = 40;
			zoomTrackBarControl1.Properties.SmallChange = 2;
			zoomTrackBarControl1.Size = new Size(342, 36);
			zoomTrackBarControl1.TabIndex = 0;
			zoomTrackBarControl1.Value = 40;
			zoomTrackBarControl1.EditValueChanged += zoomTrackBarControl1_EditValueChanged;
			// 
			// recentStackPanel8
			// 
			recentStackPanel8.Items.AddRange(new RecentItemBase[] { recentPrintPreview });
			recentStackPanel8.Name = "recentStackPanel8";
			recentStackPanel8.PanelPadding = new Padding(1);
			// 
			// recentPrintPreview
			// 
			recentPrintPreview.ClientHeight = 576;
			recentPrintPreview.ControlContainer = recentPrintPreviewContainer;
			recentPrintPreview.FillSpace = true;
			recentPrintPreview.Name = "recentPrintPreview";
			// 
			// recentStackPanel9
			// 
			recentStackPanel9.Caption = "Print";
			recentStackPanel9.Items.AddRange(new RecentItemBase[] { recentPrintOptions });
			recentStackPanel9.Name = "recentStackPanel9";
			// 
			// recentPrintOptions
			// 
			recentPrintOptions.ClientHeight = 477;
			recentPrintOptions.ControlContainer = recentPrintOptionsContainer;
			recentPrintOptions.FillSpace = true;
			recentPrintOptions.Name = "recentPrintOptions";
			// 
			// backstageViewClientControl10
			// 
			backstageViewClientControl10.Controls.Add(recentControlExport);
			backstageViewClientControl10.Location = new Point(132, 71);
			backstageViewClientControl10.Name = "backstageViewClientControl10";
			backstageViewClientControl10.Size = new Size(967, 500);
			backstageViewClientControl10.TabIndex = 8;
			// 
			// recentControlExport
			// 
			recentControlExport.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			recentControlExport.ContentPanelMinWidth = 100;
			recentControlExport.DefaultContentPanel = recentStackPanel14;
			recentControlExport.Dock = DockStyle.Fill;
			recentControlExport.Location = new Point(0, 0);
			recentControlExport.MainPanel = recentStackPanel10;
			recentControlExport.MainPanelMinWidth = 100;
			recentControlExport.MinimumSize = new Size(400, 100);
			recentControlExport.Name = "recentControlExport";
			recentControlExport.SelectedTab = recentTabItem4;
			recentControlExport.ShowTitle = false;
			recentControlExport.Size = new Size(967, 500);
			recentControlExport.TabIndex = 0;
			recentControlExport.Title = "Title";
			// 
			// recentStackPanel14
			// 
			recentStackPanel14.Name = "recentStackPanel14";
			// 
			// recentStackPanel10
			// 
			recentStackPanel10.Caption = "Export";
			recentStackPanel10.CaptionToContentIndent = 46;
			recentStackPanel10.Items.AddRange(new RecentItemBase[] { recentTabItem4 });
			recentStackPanel10.Name = "recentStackPanel10";
			recentStackPanel10.SelectedItem = recentTabItem4;
			// 
			// recentTabItem4
			// 
			recentTabItem4.Caption = "Export To";
			recentTabItem4.ImageOptions.ItemNormal.SvgImage = Properties.Resources.ExportTo;
			recentTabItem4.Name = "recentTabItem4";
			recentTabItem4.TabPanel = recentStackPanel11;
			// 
			// recentStackPanel11
			// 
			recentStackPanel11.Caption = "Export To";
			recentStackPanel11.CaptionToContentIndent = 46;
			recentStackPanel11.ImageOptions.SvgImage = Properties.Resources.ExportTo;
			recentStackPanel11.Items.AddRange(new RecentItemBase[] { recentControlRecentItem1, recentControlRecentItem2, recentControlRecentItem4, recentControlRecentItem5, recentControlRecentItem6, recentControlRecentItem7, recentControlRecentItem8, recentControlRecentItem9 });
			recentStackPanel11.Name = "recentStackPanel11";
			// 
			// recentControlRecentItem1
			// 
			recentControlRecentItem1.Caption = "PDF  File";
			recentControlRecentItem1.Description = "Adobe Portable Document Format";
			recentControlRecentItem1.GlyphAlignment = RecentPinItemGlyphAlignment.Center;
			recentControlRecentItem1.ImageOptions.ItemNormal.SvgImage = Properties.Resources.ExportToPDF;
			recentControlRecentItem1.Name = "recentControlRecentItem1";
			recentControlRecentItem1.PinButtonVisibility = RecentPinButtonVisibility.Never;
			// 
			// recentControlRecentItem2
			// 
			recentControlRecentItem2.Caption = "HTML File";
			recentControlRecentItem2.Description = "Web Page";
			recentControlRecentItem2.GlyphAlignment = RecentPinItemGlyphAlignment.Center;
			recentControlRecentItem2.ImageOptions.ItemNormal.SvgImage = Properties.Resources.Action_Export_ToHTML;
			recentControlRecentItem2.Name = "recentControlRecentItem2";
			recentControlRecentItem2.PinButtonVisibility = RecentPinButtonVisibility.Never;
			// 
			// recentControlRecentItem4
			// 
			recentControlRecentItem4.Caption = "MHT File";
			recentControlRecentItem4.Description = "Single File Web Page";
			recentControlRecentItem4.GlyphAlignment = RecentPinItemGlyphAlignment.Center;
			recentControlRecentItem4.ImageOptions.ItemNormal.SvgImage = Properties.Resources.ExportToMHT;
			recentControlRecentItem4.Name = "recentControlRecentItem4";
			recentControlRecentItem4.PinButtonVisibility = RecentPinButtonVisibility.Never;
			// 
			// recentControlRecentItem5
			// 
			recentControlRecentItem5.Caption = "RTF File";
			recentControlRecentItem5.Description = "Rich Text Format";
			recentControlRecentItem5.GlyphAlignment = RecentPinItemGlyphAlignment.Center;
			recentControlRecentItem5.ImageOptions.ItemNormal.SvgImage = Properties.Resources.ExportToRTF;
			recentControlRecentItem5.Name = "recentControlRecentItem5";
			recentControlRecentItem5.PinButtonVisibility = RecentPinButtonVisibility.Never;
			// 
			// recentControlRecentItem6
			// 
			recentControlRecentItem6.Caption = "XLS File";
			recentControlRecentItem6.Description = "Microsoft Excel 2000-2003 Workbook";
			recentControlRecentItem6.GlyphAlignment = RecentPinItemGlyphAlignment.Center;
			recentControlRecentItem6.ImageOptions.ItemNormal.SvgImage = Properties.Resources.Action_Export_ToXls;
			recentControlRecentItem6.Name = "recentControlRecentItem6";
			recentControlRecentItem6.PinButtonVisibility = RecentPinButtonVisibility.Never;
			// 
			// recentControlRecentItem7
			// 
			recentControlRecentItem7.Caption = "XLSX File";
			recentControlRecentItem7.Description = "Microsoft Excel 2007-2016 Workbook";
			recentControlRecentItem7.GlyphAlignment = RecentPinItemGlyphAlignment.Center;
			recentControlRecentItem7.ImageOptions.ItemNormal.SvgImage = Properties.Resources.Action_Export_ToXLSX;
			recentControlRecentItem7.Name = "recentControlRecentItem7";
			recentControlRecentItem7.PinButtonVisibility = RecentPinButtonVisibility.Never;
			// 
			// recentControlRecentItem8
			// 
			recentControlRecentItem8.Caption = "CSV File";
			recentControlRecentItem8.Description = "Comma-Separated Values Text";
			recentControlRecentItem8.GlyphAlignment = RecentPinItemGlyphAlignment.Center;
			recentControlRecentItem8.ImageOptions.ItemNormal.SvgImage = Properties.Resources.Action_Export_ToCSV;
			recentControlRecentItem8.Name = "recentControlRecentItem8";
			recentControlRecentItem8.PinButtonVisibility = RecentPinButtonVisibility.Never;
			// 
			// recentControlRecentItem9
			// 
			recentControlRecentItem9.Caption = "Text File";
			recentControlRecentItem9.Description = "Plain Text";
			recentControlRecentItem9.GlyphAlignment = RecentPinItemGlyphAlignment.Center;
			recentControlRecentItem9.ImageOptions.ItemNormal.SvgImage = Properties.Resources.Action_Export_ToText;
			recentControlRecentItem9.Name = "recentControlRecentItem9";
			recentControlRecentItem9.PinButtonVisibility = RecentPinButtonVisibility.Never;
			// 
			// backstageViewButtonItemSettings
			// 
			backstageViewButtonItemSettings.Caption = "Properties";
			backstageViewButtonItemSettings.Name = "backstageViewButtonItemSettings";
			backstageViewButtonItemSettings.ItemClick += backstageViewButtonItemSettings_ItemClick;
			// 
			// backstageViewTabItem1
			// 
			backstageViewTabItem1.Caption = "Save As";
			backstageViewTabItem1.ContentControl = backstageViewClientControl8;
			backstageViewTabItem1.Name = "backstageViewTabItem1";
			// 
			// printTabItem
			// 
			printTabItem.Caption = "Print";
			printTabItem.ContentControl = backstageViewClientControl11;
			printTabItem.Name = "printTabItem";
			printTabItem.SelectedChanged += onTabPrint_SelectedChanged;
			// 
			// backstageViewTabItem4
			// 
			backstageViewTabItem4.Caption = "Export";
			backstageViewTabItem4.ContentControl = backstageViewClientControl10;
			backstageViewTabItem4.Name = "backstageViewTabItem4";
			// 
			// backstageViewTabItem6
			// 
			backstageViewTabItem6.Caption = "Help";
			backstageViewTabItem6.ContentControl = backstageViewClientControl2;
			backstageViewTabItem6.Name = "backstageViewTabItem6";
			backstageViewTabItem6.Selected = true;
			// 
			// bvItemClose
			// 
			bvItemClose.Caption = "Close";
			bvItemClose.Name = "bvItemClose";
			bvItemClose.ItemClick += bvItemClose_ItemClick;
			// 
			// bvItemExit
			// 
			bvItemExit.Caption = "Exit";
			bvItemExit.Name = "bvItemExit";
			bvItemExit.ItemClick += bvItemExit_ItemClick;
			// 
			// tabControl
			// 
			tabControl.Location = new Point(1143, 541);
			tabControl.Name = "tabControl";
			tabControl.SelectedTabPage = tabPageLog;
			tabControl.Size = new Size(496, 152);
			tabControl.TabIndex = 13;
			tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { tabPageLog, tabPageUserSessions, tabSocketPAckageCommunication, tabPageTransactions, tabPageErrors, tabPageStatistics });
			// 
			// tabPageLog
			// 
			tabPageLog.Name = "tabPageLog";
			tabPageLog.Size = new Size(494, 127);
			tabPageLog.Text = "Log";
			// 
			// tabPageUserSessions
			// 
			tabPageUserSessions.Controls.Add(gridControlUserSessions);
			tabPageUserSessions.Name = "tabPageUserSessions";
			tabPageUserSessions.Size = new Size(494, 127);
			tabPageUserSessions.Text = "User Sessions";
			// 
			// gridControlUserSessions
			// 
			gridControlUserSessions.Dock = DockStyle.Fill;
			gridControlUserSessions.Location = new Point(0, 0);
			gridControlUserSessions.MainView = gridViewUserSessions;
			gridControlUserSessions.MenuManager = ribbonControl;
			gridControlUserSessions.Name = "gridControlUserSessions";
			gridControlUserSessions.Size = new Size(494, 127);
			gridControlUserSessions.TabIndex = 0;
			gridControlUserSessions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewUserSessions });
			// 
			// gridViewUserSessions
			// 
			gridViewUserSessions.GridControl = gridControlUserSessions;
			gridViewUserSessions.Name = "gridViewUserSessions";
			// 
			// tabSocketPAckageCommunication
			// 
			tabSocketPAckageCommunication.Controls.Add(splitContainerControlSocketPackageCommunication);
			tabSocketPAckageCommunication.Name = "tabSocketPAckageCommunication";
			tabSocketPAckageCommunication.Size = new Size(494, 127);
			tabSocketPAckageCommunication.Text = "Socket Package Communication";
			// 
			// splitContainerControlSocketPackageCommunication
			// 
			splitContainerControlSocketPackageCommunication.Dock = DockStyle.Fill;
			splitContainerControlSocketPackageCommunication.Location = new Point(0, 0);
			splitContainerControlSocketPackageCommunication.Name = "splitContainerControlSocketPackageCommunication";
			// 
			// splitContainerControlSocketPackageCommunication.Panel1
			// 
			splitContainerControlSocketPackageCommunication.Panel1.Controls.Add(gridControlRequestResponseMessages);
			splitContainerControlSocketPackageCommunication.Panel1.Text = "Panel1";
			// 
			// splitContainerControlSocketPackageCommunication.Panel2
			// 
			splitContainerControlSocketPackageCommunication.Panel2.Controls.Add(groupPropertyPanelRequestResponseMessages);
			splitContainerControlSocketPackageCommunication.Panel2.Text = "Panel2";
			splitContainerControlSocketPackageCommunication.Size = new Size(494, 127);
			splitContainerControlSocketPackageCommunication.SplitterPosition = 818;
			splitContainerControlSocketPackageCommunication.TabIndex = 4;
			// 
			// gridControlRequestResponseMessages
			// 
			gridControlRequestResponseMessages.Dock = DockStyle.Fill;
			gridControlRequestResponseMessages.Location = new Point(0, 0);
			gridControlRequestResponseMessages.MainView = gridViewRequestResponseMessagess;
			gridControlRequestResponseMessages.MenuManager = ribbonControl;
			gridControlRequestResponseMessages.Name = "gridControlRequestResponseMessages";
			gridControlRequestResponseMessages.Size = new Size(484, 127);
			gridControlRequestResponseMessages.TabIndex = 0;
			gridControlRequestResponseMessages.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewRequestResponseMessagess });
			// 
			// gridViewRequestResponseMessagess
			// 
			gridViewRequestResponseMessagess.GridControl = gridControlRequestResponseMessages;
			gridViewRequestResponseMessagess.Name = "gridViewRequestResponseMessagess";
			// 
			// groupPropertyPanelRequestResponseMessages
			// 
			groupPropertyPanelRequestResponseMessages.Dock = DockStyle.Fill;
			groupPropertyPanelRequestResponseMessages.Location = new Point(0, 0);
			groupPropertyPanelRequestResponseMessages.Margin = new Padding(4, 3, 4, 3);
			groupPropertyPanelRequestResponseMessages.Name = "groupPropertyPanelRequestResponseMessages";
			groupPropertyPanelRequestResponseMessages.Size = new Size(0, 0);
			groupPropertyPanelRequestResponseMessages.TabIndex = 0;
			// 
			// tabPageTransactions
			// 
			tabPageTransactions.Controls.Add(splitContainerControlTransactions);
			tabPageTransactions.Name = "tabPageTransactions";
			tabPageTransactions.Size = new Size(494, 127);
			tabPageTransactions.Text = "Transactions";
			// 
			// splitContainerControlTransactions
			// 
			splitContainerControlTransactions.Dock = DockStyle.Fill;
			splitContainerControlTransactions.Location = new Point(0, 0);
			splitContainerControlTransactions.Name = "splitContainerControlTransactions";
			// 
			// splitContainerControlTransactions.Panel1
			// 
			splitContainerControlTransactions.Panel1.Controls.Add(gridControlTransactions);
			splitContainerControlTransactions.Panel1.Text = "Panel1";
			// 
			// splitContainerControlTransactions.Panel2
			// 
			splitContainerControlTransactions.Panel2.Controls.Add(groupPropertyPanelTransactionLog);
			splitContainerControlTransactions.Panel2.Text = "Panel2";
			splitContainerControlTransactions.Size = new Size(494, 127);
			splitContainerControlTransactions.SplitterPosition = 814;
			splitContainerControlTransactions.TabIndex = 0;
			// 
			// gridControlTransactions
			// 
			gridControlTransactions.Dock = DockStyle.Fill;
			gridControlTransactions.Location = new Point(0, 0);
			gridControlTransactions.MainView = gridViewTransactions;
			gridControlTransactions.MenuManager = ribbonControl;
			gridControlTransactions.Name = "gridControlTransactions";
			gridControlTransactions.Size = new Size(484, 127);
			gridControlTransactions.TabIndex = 0;
			gridControlTransactions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewTransactions });
			// 
			// gridViewTransactions
			// 
			gridViewTransactions.GridControl = gridControlTransactions;
			gridViewTransactions.Name = "gridViewTransactions";
			// 
			// groupPropertyPanelTransactionLog
			// 
			groupPropertyPanelTransactionLog.Dock = DockStyle.Fill;
			groupPropertyPanelTransactionLog.Location = new Point(0, 0);
			groupPropertyPanelTransactionLog.Margin = new Padding(4, 3, 4, 3);
			groupPropertyPanelTransactionLog.Name = "groupPropertyPanelTransactionLog";
			groupPropertyPanelTransactionLog.Size = new Size(0, 0);
			groupPropertyPanelTransactionLog.TabIndex = 0;
			// 
			// tabPageErrors
			// 
			tabPageErrors.Name = "tabPageErrors";
			tabPageErrors.Size = new Size(494, 127);
			tabPageErrors.Text = "Errors";
			// 
			// tabPageStatistics
			// 
			tabPageStatistics.Name = "tabPageStatistics";
			tabPageStatistics.Size = new Size(494, 127);
			tabPageStatistics.Text = "Statistics";
			// 
			// printControl1
			// 
			printControl1.Dock = DockStyle.Fill;
			printControl1.IsMetric = true;
			printControl1.Location = new Point(0, 0);
			printControl1.Name = "printControl1";
			printControl1.Size = new Size(639, 539);
			printControl1.TabIndex = 3;
			// 
			// recentControlRecentItem10
			// 
			recentControlRecentItem10.Caption = "Image File";
			recentControlRecentItem10.Description = "BMP, GIF, JPEG, PNG, TIFF, EMF, WMF";
			recentControlRecentItem10.GlyphAlignment = RecentPinItemGlyphAlignment.Center;
			recentControlRecentItem10.ImageOptions.ItemNormal.Image = (Image)resources.GetObject("recentControlRecentItem10.ImageOptions.ItemNormal.Image");
			recentControlRecentItem10.Name = "recentControlRecentItem10";
			recentControlRecentItem10.PinButtonVisibility = RecentPinButtonVisibility.Never;
			// 
			// recentControlButtonItem1
			// 
			recentControlButtonItem1.AutoSize = false;
			recentControlButtonItem1.Caption = "Save As";
			recentControlButtonItem1.Name = "recentControlButtonItem1";
			recentControlButtonItem1.Orientation = Orientation.Vertical;
			recentControlButtonItem1.Size = new Size(82, 86);
			// 
			// backstageViewClientControl7
			// 
			backstageViewClientControl7.Location = new Point(0, 0);
			backstageViewClientControl7.Name = "backstageViewClientControl7";
			backstageViewClientControl7.Size = new Size(150, 150);
			backstageViewClientControl7.TabIndex = 6;
			// 
			// backstageViewClientControl1
			// 
			backstageViewClientControl1.Location = new Point(0, 0);
			backstageViewClientControl1.Name = "backstageViewClientControl1";
			backstageViewClientControl1.Size = new Size(150, 150);
			backstageViewClientControl1.TabIndex = 0;
			// 
			// backstageViewClientControl3
			// 
			backstageViewClientControl3.Location = new Point(0, 0);
			backstageViewClientControl3.Name = "backstageViewClientControl3";
			backstageViewClientControl3.Size = new Size(150, 150);
			backstageViewClientControl3.TabIndex = 2;
			// 
			// backstageViewTabItem2
			// 
			backstageViewTabItem2.Caption = "Open";
			backstageViewTabItem2.ContentControl = backstageViewClientControl8;
			backstageViewTabItem2.Name = "backstageViewTabItem2";
			// 
			// bvTabPrint
			// 
			bvTabPrint.Caption = "Print";
			bvTabPrint.Name = "bvTabPrint";
			bvTabPrint.SelectedChanged += onTabPrint_SelectedChanged;
			// 
			// backstageViewClientControl4
			// 
			backstageViewClientControl4.Controls.Add(printControl1);
			backstageViewClientControl4.Location = new Point(133, 71);
			backstageViewClientControl4.Name = "backstageViewClientControl4";
			backstageViewClientControl4.Size = new Size(639, 539);
			backstageViewClientControl4.TabIndex = 3;
			// 
			// FormMain
			// 
			AllowFormGlass = DevExpress.Utils.DefaultBoolean.False;
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1762, 869);
			Controls.Add(tabControl);
			Controls.Add(backstageViewControl);
			Controls.Add(pccBottom);
			Controls.Add(ribbonStatusBar1);
			Controls.Add(ribbonControl);
			IsMdiContainer = true;
			MinimumSize = new Size(700, 380);
			Name = "FormMain";
			Ribbon = ribbonControl;
			StartPosition = FormStartPosition.CenterScreen;
			StatusBar = ribbonStatusBar1;
			Text = "Simple.Objects™ Server Monitor";
			Load += frmMain_Load;
			((System.ComponentModel.ISupportInitialize)repositoryItemSpinEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)popupControlContainer1).EndInit();
			((System.ComponentModel.ISupportInitialize)ribbonControl).EndInit();
			((System.ComponentModel.ISupportInitialize)pmAppMain).EndInit();
			((System.ComponentModel.ISupportInitialize)pccBottom).EndInit();
			pccBottom.ResumeLayout(false);
			pccBottom.PerformLayout();
			((System.ComponentModel.ISupportInitialize)imageCollection2).EndInit();
			((System.ComponentModel.ISupportInitialize)riicStyle).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemComboBox2).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemSpinEdit2).EndInit();
			((System.ComponentModel.ISupportInitialize)imageCollection1).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemPictureEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemTrackBar1).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemColorPickEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemComboBox3).EndInit();
			((System.ComponentModel.ISupportInitialize)pcAppMenuFileLabels).EndInit();
			((System.ComponentModel.ISupportInitialize)pmNew).EndInit();
			((System.ComponentModel.ISupportInitialize)pmMain).EndInit();
			((System.ComponentModel.ISupportInitialize)imageCollection3).EndInit();
			((System.ComponentModel.ISupportInitialize)backstageViewControl).EndInit();
			backstageViewControl.ResumeLayout(false);
			backstageViewClientControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)recentItemControl1).EndInit();
			backstageViewClientControl8.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)recentSaveAs).EndInit();
			backstageViewClientControl11.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)recentControlPrint).EndInit();
			recentControlPrint.ResumeLayout(false);
			recentPrintOptionsContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
			layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)copySpinEdit.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)lciCopiesSpinEdit).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem6).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem7).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem8).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem9).EndInit();
			recentPrintPreviewContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)panelControl2).EndInit();
			panelControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)panelControl3).EndInit();
			panelControl3.ResumeLayout(false);
			panelControl3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)stackPanel1).EndInit();
			stackPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)zoomTextEdit.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)pageButtonEdit.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)zoomTrackBarControl1.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)zoomTrackBarControl1).EndInit();
			backstageViewClientControl10.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)recentControlExport).EndInit();
			((System.ComponentModel.ISupportInitialize)tabControl).EndInit();
			tabControl.ResumeLayout(false);
			tabPageUserSessions.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)gridControlUserSessions).EndInit();
			((System.ComponentModel.ISupportInitialize)gridViewUserSessions).EndInit();
			tabSocketPAckageCommunication.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainerControlSocketPackageCommunication.Panel1).EndInit();
			splitContainerControlSocketPackageCommunication.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainerControlSocketPackageCommunication.Panel2).EndInit();
			splitContainerControlSocketPackageCommunication.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainerControlSocketPackageCommunication).EndInit();
			splitContainerControlSocketPackageCommunication.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)gridControlRequestResponseMessages).EndInit();
			((System.ComponentModel.ISupportInitialize)gridViewRequestResponseMessagess).EndInit();
			tabPageTransactions.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainerControlTransactions.Panel1).EndInit();
			splitContainerControlTransactions.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainerControlTransactions.Panel2).EndInit();
			splitContainerControlTransactions.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainerControlTransactions).EndInit();
			splitContainerControlTransactions.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)gridControlTransactions).EndInit();
			((System.ComponentModel.ISupportInitialize)gridViewTransactions).EndInit();
			backstageViewClientControl4.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private DevExpress.XtraBars.BarButtonItem iProtected;
		private DevExpress.XtraBars.BarButtonItem iWeb;
		private DevExpress.XtraBars.BarButtonItem iPrint;
		private DevExpress.XtraBars.BarButtonItem iClose;
		private DevExpress.XtraBars.BarButtonItem barButtonItemInfo;
		private DevExpress.XtraBars.BarStaticItem siDocName;
		private DevExpress.XtraBars.PopupControlContainer popupControlContainer1;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
		private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
		private DevExpress.Utils.ImageCollection imageCollection1;
		private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
		private DevExpress.XtraBars.PopupMenu pmNew;
		private DevExpress.XtraBars.PopupMenu pmMain;
		private DevExpress.XtraBars.BarSubItem barSubItemChangeSkin;
		private DevExpress.XtraBars.RibbonGalleryBarItem rgbiSkins;
		private DevExpress.XtraBars.Ribbon.ApplicationMenu pmAppMain;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
		private DevExpress.XtraBars.BarButtonItem iAbout;
		private DevExpress.Utils.ImageCollection imageCollection2;
		private Office2007PopupControlContainer pccAppMenu;
		//private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.PanelControl pcAppMenuFileLabels;
		private DevExpress.Utils.ImageCollection imageCollection3;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
		private BarEditItem barEditItemRibbonStyle;
		private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riicStyle;
		private PopupControlContainer pccBottom;
		private DevExpress.XtraEditors.SimpleButton sbExit;
		private DevExpress.XtraBars.Ribbon.RibbonMiniToolbar selectionMiniToolbar;
		private BarButtonGroup editButtonGroup;
		private DevExpress.XtraBars.Ribbon.BackstageViewControl backstageViewControl;
		private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl1;
		private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl3;
		private DevExpress.XtraBars.Ribbon.BackstageViewButtonItem backstageViewButtonItemSettings;
		private DevExpress.XtraBars.Ribbon.BackstageViewButtonItem bvItemClose;
		private DevExpress.XtraBars.Ribbon.BackstageViewButtonItem bvItemExit;
		private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl7;
		private BarEditItem barEditItemColorScheme;
		private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
		//private DevExpress.Utils.Taskbar.TaskbarAssistant taskbarAssistant1;
		//private DevExpress.Utils.Taskbar.ThumbnailButton thumbButtonNewDoc;
		//private DevExpress.Utils.Taskbar.ThumbnailButton thumbButtonNext;
		//private DevExpress.Utils.Taskbar.ThumbnailButton thumbButtonExit;
		//private DevExpress.Utils.Taskbar.ThumbnailButton thumbButtonPrev;
		private DevExpress.XtraEditors.Repository.RepositoryItemTrackBar repositoryItemTrackBar1;
		private BarButtonItem bbColorMix;
		private System.ComponentModel.IContainer components;
		private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl8;
		private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItem1;
		private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItem2;
		private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl10;
		private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItem4;
		private DevExpress.XtraBars.Ribbon.RecentPinItem recentControlRecentItem1;
		private DevExpress.XtraBars.Ribbon.RecentPinItem recentControlRecentItem2;
		private DevExpress.XtraBars.Ribbon.RecentPinItem recentControlRecentItem4;
		private DevExpress.XtraBars.Ribbon.RecentPinItem recentControlRecentItem5;
		private DevExpress.XtraBars.Ribbon.RecentPinItem recentControlRecentItem6;
		private DevExpress.XtraBars.Ribbon.RecentPinItem recentControlRecentItem7;
		private DevExpress.XtraBars.Ribbon.RecentPinItem recentControlRecentItem8;
		private DevExpress.XtraBars.Ribbon.RecentPinItem recentControlRecentItem9;
		private DevExpress.XtraBars.Ribbon.RecentPinItem recentControlRecentItem10;
		private DevExpress.XtraBars.Ribbon.RecentButtonItem recentControlButtonItem1;
		private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl11;
		private DevExpress.XtraBars.Ribbon.RecentItemControl recentControlPrint;
		private DevExpress.XtraBars.Ribbon.RecentControlItemControlContainer recentPrintOptionsContainer;
		private DevExpress.XtraBars.Ribbon.RecentControlItemControlContainer recentPrintPreviewContainer;
		private DevExpress.XtraBars.Ribbon.RecentControlContainerItem recentPrintOptions;
		private DevExpress.XtraBars.Ribbon.RecentControlContainerItem recentPrintPreview;
		private DevExpress.XtraBars.Ribbon.BackstageViewTabItem printTabItem;
		private DevExpress.XtraBars.Ribbon.BackstageViewTabItem bvTabPrint;
		//private DevExpress.XtraPrinting.Control.PrintControl printControl1;
		private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl4;
		private DevExpress.XtraEditors.PanelControl panelControl2;
		private DevExpress.XtraEditors.PanelControl panelControl3;
		private DevExpress.XtraPrinting.Control.PrintControl printControl1;
		private DevExpress.XtraEditors.ZoomTrackBarControl zoomTrackBarControl1;
		private DevExpress.XtraEditors.ButtonEdit pageButtonEdit;
		private System.Windows.Forms.Panel panel2;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraEditors.SimpleButton printButton;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraEditors.SpinEdit copySpinEdit;
		private DevExpress.XtraLayout.LayoutControlItem lciCopiesSpinEdit;
		private BackstageViewLabel printerLabel;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private DevExpress.XtraEditors.TextEdit zoomTextEdit;
		private DevExpress.XtraEditors.DropDownButton ddbPrinter;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraBars.Ribbon.RecentItemControl recentSaveAs;
		private DevExpress.XtraBars.Ribbon.RecentTabItem recentTabItem3;
		private DevExpress.XtraBars.Ribbon.RecentItemControl recentControlExport;
		private DevExpress.XtraBars.Ribbon.RecentTabItem recentTabItem4;
		private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl2;
		private DevExpress.XtraBars.Ribbon.RecentItemControl recentItemControl1;
		private DevExpress.XtraBars.Ribbon.RecentPinItem recentPinItem2;
		private DevExpress.XtraBars.Ribbon.RecentPinItem recentPinItem3;
		private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItem6;
		private DevExpress.XtraBars.Ribbon.RecentPinItem recentPinItem4;
		private DevExpress.XtraBars.Ribbon.RecentLabelItem recentLabelItem1;
		private DevExpress.XtraBars.Ribbon.RecentHyperlinkItem recentHyperlinkItem1;
		private DevExpress.XtraBars.Ribbon.RecentLabelItem recentLabelItem2;
		private DevExpress.XtraEditors.DropDownButton ddbDuplex;
		private DevExpress.XtraEditors.DropDownButton ddbOrientation;
		private DevExpress.XtraEditors.DropDownButton ddbPaperSize;
		private DevExpress.XtraEditors.DropDownButton ddbMargins;
		private DevExpress.XtraEditors.DropDownButton ddbCollate;
		private BackstageViewLabel backstageViewLabel2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
		private DevExpress.XtraPrinting.Control.PrintControl printControl2;
		private RecentStackPanel recentStackPanel5;
		private RecentStackPanel recentStackPanel6;
		private RecentStackPanel recentStackPanel7;
		private RecentStackPanel recentStackPanel8;
		private RecentStackPanel recentStackPanel9;
		private RecentStackPanel recentStackPanel10;
		private RecentStackPanel recentStackPanel11;
		private RecentStackPanel recentStackPanel12;
		private RecentStackPanel recentStackPanel13;
		private RecentStackPanel recentStackPanel14;
		private BarButtonItem barButtonItem1;
		private BarButtonItem biOnlineHelp;
		private BarButtonItem biGettingStarted;
		private BarButtonItem biContact;
		private RibbonPage rpHelp;
		private RibbonPageGroup rpgHelp;
		private RibbonGalleryBarItem rgbiColorScheme;
		private DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit repositoryItemColorPickEdit1;
		private DevExpress.Utils.Layout.StackPanel stackPanel1;
		private RibbonPage ribbonPageHome;
		private BarButtonItem barButtonItemConnect;
		private RibbonPageGroup ribbonPageGroupMonitorService;
		private BarEditItem barEditItemMonitorServer;
		private BarEditItem barEditItemMonitorServerPort;
		private BarButtonItem barButtonItemDisconnect;
		private BarEditItem barEditItem2;
		private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxServer;
		private RibbonPageGroup ribbonPageGroupSimpleObjectsServer;
		private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditServerMonitorPort;
		private BarButtonItem barButtonItemServerStart;
		private BarButtonItem barButtonItemServerStop;
		private BarButtonItem barButtonItemServerRestart;
		private BarStaticItem barStaticItemUser;
		private BarStaticItem barStaticItemMonitorServer;
		private RibbonPageGroup ribbonPageGroupSkins;
		private BarToggleSwitchItem barToggleSwitchItemDarkMode;
		private BarToggleSwitchItem barToggleSwitchItemCompactView;
		private DevExpress.XtraBars.BarSubItem sbiSave;
		private DevExpress.XtraBars.BarSubItem buttonHelp;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit2;
		private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox3;
		private BarButtonItem buttonSettings;
		private BarStaticItem barStaticItemPoweredByInfo;
		private DevExpress.XtraTab.XtraTabControl tabControl;
		private DevExpress.XtraTab.XtraTabPage tabPageLog;
		private DevExpress.XtraTab.XtraTabPage tabPageUserSessions;
		private DevExpress.XtraGrid.GridControl gridControlUserSessions;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewUserSessions;
		private DevExpress.XtraTab.XtraTabPage tabSocketPAckageCommunication;
		private DevExpress.XtraTab.XtraTabPage tabPageTransactions;
		private SplitContainerControl splitContainerControlSocketPackageCommunication;
		private DevExpress.XtraGrid.GridControl gridControlRequestResponseMessages;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewRequestResponseMessagess;
		private Controls.GroupPropertyPanel groupPropertyPanelRequestResponseMessages;
		private SplitContainerControl splitContainerControlTransactions;
		private Controls.GroupPropertyPanel groupPropertyPanelTransactionLog;
		private DevExpress.XtraGrid.GridControl gridControlTransactions;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewTransactions;
		private DevExpress.XtraTab.XtraTabPage tabPageErrors;
		private DevExpress.XtraTab.XtraTabPage tabPageStatistics;
	}
}
