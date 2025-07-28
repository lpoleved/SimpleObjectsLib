using System;
using DevExpress;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraLayout;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraSplashScreen;

namespace Simple.Objects.ServerMonitor
{ 
	partial class frmMain
	{
		#region Designer generated code
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			GalleryItemGroup galleryItemGroup1 = new GalleryItemGroup();
			GalleryItemGroup galleryItemGroup2 = new GalleryItemGroup();
			GalleryItemGroup galleryItemGroup3 = new GalleryItemGroup();
			GalleryItemGroup galleryItemGroup4 = new GalleryItemGroup();
			GalleryItemGroup galleryItemGroup5 = new GalleryItemGroup();
			GalleryItemGroup galleryItemGroup6 = new GalleryItemGroup();
			GalleryItem galleryItem1 = new GalleryItem();
			GalleryItem galleryItem2 = new GalleryItem();
			GalleryItem galleryItem3 = new GalleryItem();
			GalleryItem galleryItem4 = new GalleryItem();
			GalleryItem galleryItem5 = new GalleryItem();
			GalleryItem galleryItem6 = new GalleryItem();
			GalleryItem galleryItem7 = new GalleryItem();
			GalleryItem galleryItem8 = new GalleryItem();
			GalleryItem galleryItem9 = new GalleryItem();
			DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.ToolTipSeparatorItem toolTipSeparatorItem1 = new DevExpress.Utils.ToolTipSeparatorItem();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem5 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip5 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem6 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem5 = new DevExpress.Utils.ToolTipItem();
			DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
			iWeb = new BarButtonItem();
			iAbout = new BarButtonItem();
			iCenter = new BarButtonItem();
			iSelectAll = new BarButtonItem();
			iCopy = new BarButtonItem();
			iCut = new BarButtonItem();
			iPaste = new BarButtonItem();
			iClear = new BarButtonItem();
			iFont = new BarButtonItem();
			gddFont = new GalleryDropDown(components);
			beiFontSize = new BarEditItem();
			repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			bbiFontColorPopup = new BarButtonItem();
			popupControlContainer1 = new PopupControlContainer(components);
			ribbonControl = new RibbonControl();
			pmAppMain = new ApplicationMenu(components);
			pccBottom = new PopupControlContainer(components);
			sbExit = new SimpleButton();
			imageCollection2 = new DevExpress.Utils.ImageCollection(components);
			idNew = new BarButtonItem();
			pmNew = new PopupMenu(components);
			iNew = new BarButtonItem();
			iTemplate = new BarButtonItem();
			iOpen = new BarButtonItem();
			sbiSave = new BarSubItem();
			iSave = new BarButtonItem();
			iSaveAs = new BarButtonItem();
			iPrint = new BarButtonItem();
			iClose = new BarButtonItem();
			iUndo = new BarButtonItem();
			iReplace = new BarButtonItem();
			iExit = new BarButtonItem();
			iFind = new BarButtonItem();
			iBullets = new BarButtonItem();
			iProtected = new BarCheckItem();
			iBold = new BarButtonItem();
			iItalic = new BarButtonItem();
			iUnderline = new BarButtonItem();
			iAlignLeft = new BarButtonItem();
			iAlignRight = new BarButtonItem();
			iFontColor = new BarButtonItem();
			gddFontColor = new GalleryDropDown(components);
			siPosition = new BarButtonItem();
			barButtonItemInfo = new BarButtonItem();
			siDocName = new BarStaticItem();
			bgFontStyle = new BarButtonGroup();
			bgAlign = new BarButtonGroup();
			bgFont = new BarButtonGroup();
			bgBullets = new BarButtonGroup();
			sbiPaste = new BarSubItem();
			iPasteSpecial = new BarButtonItem();
			sbiFind = new BarSubItem();
			iLargeUndo = new BarLargeButtonItem();
			iPaintStyle = new BarButtonItem();
			rgbiSkins = new RibbonGalleryBarItem();
			rgbiFont = new RibbonGalleryBarItem();
			rgbiFontColor = new RibbonGalleryBarItem();
			barEditItem1 = new BarEditItem();
			repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			biStyle = new BarEditItem();
			riicStyle = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
			editButtonGroup = new BarButtonGroup();
			barToggleSwitchItem1 = new BarToggleSwitchItem();
			bbColorMix = new BarButtonItem();
			barButtonItem1 = new BarButtonItem();
			biOnlineHelp = new BarButtonItem();
			biGettingStarted = new BarButtonItem();
			biContact = new BarButtonItem();
			rgbiColorScheme = new RibbonGalleryBarItem();
			biPageColor = new BarButtonItem();
			biPageBorders = new BarButtonItem();
			skinPaletteRibbonGalleryBarItem1 = new SkinPaletteRibbonGalleryBarItem();
			barButtonItemConnect = new BarButtonItem();
			barEditItemMonitorServer = new BarEditItem();
			repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			barEditItemServerMonitorPort = new BarEditItem();
			barButtonItemDisconnect = new BarButtonItem();
			barButtonItemServerStart = new BarButtonItem();
			barButtonItemServerRestart = new BarButtonItem();
			barButtonItemServerStop = new BarButtonItem();
			rgbiSkins2 = new RibbonGalleryBarItem();
			skinPaletteRibbonGalleryBarItem2 = new SkinPaletteRibbonGalleryBarItem();
			barButtonItemColorMix = new BarButtonItem();
			barToggleSwitchItemDarkMode = new BarToggleSwitchItem();
			barToggleSwitchItemCompactView = new BarToggleSwitchItem();
			barStaticItemMonitorServer = new BarStaticItem();
			barStaticItemUser = new BarStaticItem();
			imageCollection1 = new DevExpress.Utils.ImageCollection(components);
			selectionMiniToolbar = new RibbonMiniToolbar(components);
			ribbonPageCategory1 = new RibbonPageCategory();
			ribbonPage4 = new RibbonPage();
			ribbonPageGroup12 = new RibbonPageGroup();
			ribbonPageGroup13 = new RibbonPageGroup();
			ribbonPage2 = new RibbonPage();
			ribbonPageGroupMonitorServer = new RibbonPageGroup();
			ribbonPageGroupSimpleObjectsServer = new RibbonPageGroup();
			ribbonPageGroupLookAndFeel = new RibbonPageGroup();
			ribbonPage1 = new RibbonPage();
			ribbonPageGroup1 = new RibbonPageGroup();
			ribbonPageGroup2 = new RibbonPageGroup();
			ribbonPageGroup3 = new RibbonPageGroup();
			ribbonPageGroup4 = new RibbonPageGroup();
			ribbonPageGroup8 = new RibbonPageGroup();
			ribbonPageGroup9 = new RibbonPageGroup();
			ribbonPage3 = new RibbonPage();
			rpgFont = new RibbonPageGroup();
			rpgFontColor = new RibbonPageGroup();
			ribbonPageGroup5 = new RibbonPageGroup();
			rpThemes = new RibbonPage();
			ribbonPageGroup10 = new RibbonPageGroup();
			rpHelp = new RibbonPage();
			rpgHelp = new RibbonPageGroup();
			repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			repositoryItemTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemTrackBar();
			repositoryItemColorPickEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit();
			repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			ribbonStatusBar1 = new RibbonStatusBar();
			pcAppMenuFileLabels = new PanelControl();
			labelControl1 = new LabelControl();
			defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(components);
			xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(components);
			pmMain = new PopupMenu(components);
			imageCollection3 = new DevExpress.Utils.ImageCollection(components);
			backstageViewControl1 = new BackstageViewControl();
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
			backstageViewClientControl9 = new BackstageViewClientControl();
			recentOpen = new RecentItemControl();
			recentStackPanel1 = new RecentStackPanel();
			recentStackPanel2 = new RecentStackPanel();
			recentTabItem1 = new RecentTabItem();
			recentStackPanel3 = new RecentStackPanel();
			recentTabItem2 = new RecentTabItem();
			recentStackPanel4 = new RecentStackPanel();
			backstageViewClientControl8 = new BackstageViewClientControl();
			recentSaveAs = new RecentItemControl();
			recentStackPanel5 = new RecentStackPanel();
			recentStackPanel6 = new RecentStackPanel();
			recentTabItem3 = new RecentTabItem();
			recentStackPanel7 = new RecentStackPanel();
			backstageViewClientControl11 = new BackstageViewClientControl();
			recentControlPrint = new RecentItemControl();
			recentPrintOptionsContainer = new RecentControlItemControlContainer();
			layoutControl1 = new LayoutControl();
			ddbDuplex = new DropDownButton();
			ddbOrientation = new DropDownButton();
			ddbPaperSize = new DropDownButton();
			ddbMargins = new DropDownButton();
			ddbCollate = new DropDownButton();
			ddbPrinter = new DropDownButton();
			printButton = new SimpleButton();
			copySpinEdit = new SpinEdit();
			layoutControlGroup1 = new LayoutControlGroup();
			layoutControlItem1 = new LayoutControlItem();
			lciCopiesSpinEdit = new LayoutControlItem();
			layoutControlItem3 = new LayoutControlItem();
			layoutControlItem2 = new LayoutControlItem();
			layoutControlItem4 = new LayoutControlItem();
			layoutControlItem5 = new LayoutControlItem();
			layoutControlItem6 = new LayoutControlItem();
			layoutControlItem7 = new LayoutControlItem();
			layoutControlItem8 = new LayoutControlItem();
			layoutControlItem9 = new LayoutControlItem();
			recentPrintPreviewContainer = new RecentControlItemControlContainer();
			panelControl2 = new PanelControl();
			printControl2 = new DocumentViewer();
			panelControl3 = new PanelControl();
			stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
			zoomTextEdit = new TextEdit();
			panel2 = new System.Windows.Forms.Panel();
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
			backstageViewTabItem3 = new BackstageViewTabItem();
			bvItemSave = new BackstageViewButtonItem();
			backstageViewTabItem1 = new BackstageViewTabItem();
			printTabItem = new BackstageViewTabItem();
			backstageViewTabItem4 = new BackstageViewTabItem();
			backstageViewTabItem6 = new BackstageViewTabItem();
			backstageViewItemSeparator1 = new BackstageViewItemSeparator();
			bvItemClose = new BackstageViewButtonItem();
			bvItemExit = new BackstageViewButtonItem();
			printControl1 = new DevExpress.XtraPrinting.Control.PrintControl();
			recentControlRecentItem10 = new RecentPinItem();
			recentControlButtonItem1 = new RecentButtonItem();
			backstageViewClientControl7 = new BackstageViewClientControl();
			backstageViewClientControl1 = new BackstageViewClientControl();
			backstageViewClientControl3 = new BackstageViewClientControl();
			taskbarAssistant1 = new DevExpress.Utils.Taskbar.TaskbarAssistant();
			thumbButtonNewDoc = new DevExpress.Utils.Taskbar.ThumbnailButton();
			thumbButtonPrev = new DevExpress.Utils.Taskbar.ThumbnailButton();
			thumbButtonNext = new DevExpress.Utils.Taskbar.ThumbnailButton();
			thumbButtonExit = new DevExpress.Utils.Taskbar.ThumbnailButton();
			backstageViewTabItem2 = new BackstageViewTabItem();
			bvTabPrint = new BackstageViewTabItem();
			backstageViewClientControl4 = new BackstageViewClientControl();
			emptySpacePanel = new PanelControl();
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
			ribbonGalleryBarItem1 = new RibbonGalleryBarItem();
			((System.ComponentModel.ISupportInitialize)gddFont).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemSpinEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)popupControlContainer1).BeginInit();
			((System.ComponentModel.ISupportInitialize)ribbonControl).BeginInit();
			((System.ComponentModel.ISupportInitialize)pmAppMain).BeginInit();
			((System.ComponentModel.ISupportInitialize)pccBottom).BeginInit();
			pccBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)imageCollection2).BeginInit();
			((System.ComponentModel.ISupportInitialize)pmNew).BeginInit();
			((System.ComponentModel.ISupportInitialize)gddFontColor).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemPictureEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)riicStyle).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)imageCollection1).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemTrackBar1).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemColorPickEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit2).BeginInit();
			((System.ComponentModel.ISupportInitialize)pcAppMenuFileLabels).BeginInit();
			((System.ComponentModel.ISupportInitialize)xtraTabbedMdiManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)pmMain).BeginInit();
			((System.ComponentModel.ISupportInitialize)imageCollection3).BeginInit();
			((System.ComponentModel.ISupportInitialize)backstageViewControl1).BeginInit();
			backstageViewControl1.SuspendLayout();
			backstageViewClientControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)recentItemControl1).BeginInit();
			backstageViewClientControl9.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)recentOpen).BeginInit();
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
			backstageViewClientControl4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)emptySpacePanel).BeginInit();
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
			SuspendLayout();
			// 
			// iWeb
			// 
			iWeb.Caption = "&DevExpress on the Web";
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
			// iCenter
			// 
			iCenter.ButtonStyle = BarButtonStyle.Check;
			iCenter.Caption = "&Center";
			iCenter.CategoryGuid = new Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258");
			iCenter.Description = "Centers the selected text.";
			iCenter.GroupIndex = 1;
			iCenter.Hint = "Center";
			iCenter.Id = 28;
			iCenter.ImageOptions.ImageIndex = 19;
			iCenter.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iCenter.ImageOptions.SvgImage");
			iCenter.ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E);
			iCenter.Name = "iCenter";
			iCenter.ItemClick += iAlign_ItemClick;
			// 
			// iSelectAll
			// 
			iSelectAll.Caption = "Select A&ll";
			iSelectAll.CategoryGuid = new Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1");
			iSelectAll.Description = "Selects all text in the active document.";
			iSelectAll.Hint = "Selects all text in the active document.";
			iSelectAll.Id = 13;
			iSelectAll.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iSelectAll.ImageOptions.SvgImage");
			iSelectAll.ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A);
			iSelectAll.Name = "iSelectAll";
			iSelectAll.ItemClick += iSelectAll_ItemClick;
			// 
			// iCopy
			// 
			iCopy.Caption = "&Copy";
			iCopy.CategoryGuid = new Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1");
			iCopy.Description = "Copies the selection to the Clipboard.";
			iCopy.Hint = "Copy";
			iCopy.Id = 10;
			iCopy.ImageOptions.ImageIndex = 1;
			iCopy.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iCopy.ImageOptions.SvgImage");
			iCopy.ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C);
			iCopy.Name = "iCopy";
			iCopy.RibbonStyle = RibbonItemStyles.SmallWithText | RibbonItemStyles.SmallWithoutText;
			iCopy.ItemClick += iCopy_ItemClick;
			// 
			// iCut
			// 
			iCut.Caption = "Cu&t";
			iCut.CategoryGuid = new Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1");
			iCut.Description = "Removes the selection from the active document and places it on the Clipboard.";
			iCut.Hint = "Cut";
			iCut.Id = 9;
			iCut.ImageOptions.ImageIndex = 2;
			iCut.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iCut.ImageOptions.SvgImage");
			iCut.ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X);
			iCut.Name = "iCut";
			iCut.RibbonStyle = RibbonItemStyles.SmallWithText | RibbonItemStyles.SmallWithoutText;
			iCut.ItemClick += iCut_ItemClick;
			// 
			// iPaste
			// 
			iPaste.Caption = "&Paste";
			iPaste.CategoryGuid = new Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1");
			iPaste.Description = "Inserts the contents of the Clipboard at the insertion point, and replaces any selection. This command is available only if you have cut or copied a text.";
			iPaste.Hint = "Paste";
			iPaste.Id = 11;
			iPaste.ImageOptions.ImageIndex = 8;
			iPaste.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iPaste.ImageOptions.SvgImage");
			iPaste.ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V);
			iPaste.Name = "iPaste";
			iPaste.ItemClick += iPaste_ItemClick;
			// 
			// iClear
			// 
			iClear.Caption = "Cle&ar";
			iClear.CategoryGuid = new Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1");
			iClear.Description = "Deletes the selected text without putting it on the Clipboard. This command is available only if a text is selected. ";
			iClear.Hint = "Clear";
			iClear.Id = 12;
			iClear.ImageOptions.ImageIndex = 13;
			iClear.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iClear.ImageOptions.SvgImage");
			iClear.Name = "iClear";
			iClear.RibbonStyle = RibbonItemStyles.SmallWithText | RibbonItemStyles.SmallWithoutText;
			iClear.ItemClick += iClear_ItemClick;
			// 
			// iFont
			// 
			iFont.ButtonStyle = BarButtonStyle.DropDown;
			iFont.Caption = "&Font...";
			iFont.CategoryGuid = new Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258");
			iFont.Description = "Changes the font and character spacing formats of the selected text.";
			iFont.DropDownControl = gddFont;
			iFont.Hint = "Font Dialog";
			iFont.Id = 17;
			iFont.ImageOptions.ImageIndex = 4;
			iFont.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iFont.ImageOptions.SvgImage");
			iFont.Name = "iFont";
			iFont.RibbonStyle = RibbonItemStyles.SmallWithText | RibbonItemStyles.SmallWithoutText;
			iFont.ItemClick += iFont_ItemClick;
			// 
			// gddFont
			// 
			// 
			// 
			// 
			gddFont.Gallery.Appearance.ItemCaptionAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 9.75F);
			gddFont.Gallery.Appearance.ItemCaptionAppearance.Hovered.Options.UseFont = true;
			gddFont.Gallery.Appearance.ItemCaptionAppearance.Hovered.Options.UseTextOptions = true;
			gddFont.Gallery.Appearance.ItemCaptionAppearance.Hovered.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			gddFont.Gallery.Appearance.ItemCaptionAppearance.Hovered.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			gddFont.Gallery.Appearance.ItemCaptionAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F);
			gddFont.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseFont = true;
			gddFont.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseTextOptions = true;
			gddFont.Gallery.Appearance.ItemCaptionAppearance.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			gddFont.Gallery.Appearance.ItemCaptionAppearance.Normal.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			gddFont.Gallery.Appearance.ItemCaptionAppearance.Pressed.Font = new System.Drawing.Font("Tahoma", 9.75F);
			gddFont.Gallery.Appearance.ItemCaptionAppearance.Pressed.Options.UseFont = true;
			gddFont.Gallery.Appearance.ItemCaptionAppearance.Pressed.Options.UseTextOptions = true;
			gddFont.Gallery.Appearance.ItemCaptionAppearance.Pressed.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			gddFont.Gallery.Appearance.ItemCaptionAppearance.Pressed.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			gddFont.Gallery.Appearance.ItemDescriptionAppearance.Hovered.Options.UseTextOptions = true;
			gddFont.Gallery.Appearance.ItemDescriptionAppearance.Hovered.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			gddFont.Gallery.Appearance.ItemDescriptionAppearance.Hovered.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			gddFont.Gallery.Appearance.ItemDescriptionAppearance.Normal.Options.UseTextOptions = true;
			gddFont.Gallery.Appearance.ItemDescriptionAppearance.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			gddFont.Gallery.Appearance.ItemDescriptionAppearance.Normal.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			gddFont.Gallery.Appearance.ItemDescriptionAppearance.Pressed.Options.UseTextOptions = true;
			gddFont.Gallery.Appearance.ItemDescriptionAppearance.Pressed.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			gddFont.Gallery.Appearance.ItemDescriptionAppearance.Pressed.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			gddFont.Gallery.ColumnCount = 1;
			gddFont.Gallery.FixedImageSize = false;
			galleryItemGroup1.Caption = "Main";
			gddFont.Gallery.Groups.AddRange(new GalleryItemGroup[] { galleryItemGroup1 });
			gddFont.Gallery.ItemImageLocation = DevExpress.Utils.Locations.Left;
			gddFont.Gallery.RowCount = 6;
			gddFont.Gallery.ShowGroupCaption = false;
			gddFont.Gallery.ShowItemText = true;
			gddFont.Gallery.SizeMode = GallerySizeMode.Vertical;
			gddFont.ItemLinks.Add(beiFontSize);
			gddFont.ItemLinks.Add(bbiFontColorPopup);
			gddFont.MenuCaption = "Fonts";
			gddFont.Name = "gddFont";
			gddFont.Ribbon = ribbonControl;
			gddFont.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			gddFont.GalleryItemClick += gddFont_Gallery_ItemClick;
			gddFont.GalleryCustomDrawItemText += gddFont_Gallery_CustomDrawItemText;
			gddFont.Popup += gddFont_Popup;
			// 
			// beiFontSize
			// 
			beiFontSize.Caption = "Font Size";
			beiFontSize.Edit = repositoryItemSpinEdit1;
			beiFontSize.Hint = "Font Size";
			beiFontSize.Id = 27;
			beiFontSize.Name = "beiFontSize";
			beiFontSize.EditValueChanged += beiFontSize_EditValueChanged;
			// 
			// repositoryItemSpinEdit1
			// 
			repositoryItemSpinEdit1.AutoHeight = false;
			repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
			repositoryItemSpinEdit1.MaxValue = new decimal(new int[] { 50, 0, 0, 0 });
			repositoryItemSpinEdit1.MinValue = new decimal(new int[] { 6, 0, 0, 0 });
			repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
			// 
			// bbiFontColorPopup
			// 
			bbiFontColorPopup.ActAsDropDown = true;
			bbiFontColorPopup.ButtonStyle = BarButtonStyle.DropDown;
			bbiFontColorPopup.Caption = "Font Color";
			bbiFontColorPopup.Description = "Formats the selected text with the color you click";
			bbiFontColorPopup.DropDownControl = popupControlContainer1;
			bbiFontColorPopup.Hint = "Formats the selected text with the color you click";
			bbiFontColorPopup.Id = 36;
			bbiFontColorPopup.Name = "bbiFontColorPopup";
			// 
			// popupControlContainer1
			// 
			popupControlContainer1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			popupControlContainer1.Location = new System.Drawing.Point(0, 0);
			popupControlContainer1.Name = "popupControlContainer1";
			popupControlContainer1.Size = new System.Drawing.Size(0, 0);
			popupControlContainer1.TabIndex = 6;
			popupControlContainer1.Visible = false;
			// 
			// ribbonControl
			// 
			ribbonControl.AllowCustomization = true;
			ribbonControl.ApplicationButtonDropDownControl = pmAppMain;
			ribbonControl.ApplicationButtonText = "App";
			ribbonControl.AutoSizeItems = true;
			ribbonControl.Categories.AddRange(new BarManagerCategory[] { new BarManagerCategory("File", new Guid("4b511317-d784-42ba-b4ed-0d2a746d6c1f")), new BarManagerCategory("Edit", new Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1")), new BarManagerCategory("Format", new Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258")), new BarManagerCategory("Help", new Guid("e07a4c24-66ac-4de6-bbcb-c0b6cfa7798b")), new BarManagerCategory("Status", new Guid("77795bb7-9bc5-4dd2-a297-cc758682e23d")) });
			ribbonControl.ExpandCollapseItem.Id = 0;
			ribbonControl.Images = imageCollection2;
			ribbonControl.Items.AddRange(new BarItem[] { ribbonControl.ExpandCollapseItem, iOpen, iSave, iUndo, iReplace, idNew, iClose, iSaveAs, iPrint, iExit, iCut, iCopy, iPaste, iClear, iSelectAll, iFind, iFont, iBullets, iProtected, iWeb, iAbout, iBold, iItalic, iUnderline, iAlignLeft, iCenter, iAlignRight, iFontColor, siPosition, barButtonItemInfo, siDocName, bgFontStyle, bgAlign, bgFont, bgBullets, sbiSave, sbiPaste, sbiFind, iPasteSpecial, iNew, iLargeUndo, iTemplate, iPaintStyle, rgbiSkins, beiFontSize, rgbiFont, bbiFontColorPopup, rgbiFontColor, barEditItem1, biStyle, editButtonGroup, barToggleSwitchItem1, bbColorMix, barButtonItem1, biOnlineHelp, biGettingStarted, biContact, rgbiColorScheme, biPageColor, biPageBorders, skinPaletteRibbonGalleryBarItem1, barButtonItemConnect, barEditItemMonitorServer, barEditItemServerMonitorPort, barButtonItemDisconnect, barButtonItemServerStart, barButtonItemServerRestart, barButtonItemServerStop, rgbiSkins2, skinPaletteRibbonGalleryBarItem2, barButtonItemColorMix, barToggleSwitchItemDarkMode, barToggleSwitchItemCompactView, barStaticItemMonitorServer, barStaticItemUser });
			ribbonControl.LargeImages = imageCollection1;
			ribbonControl.Location = new System.Drawing.Point(0, 0);
			ribbonControl.MaxItemId = 486;
			ribbonControl.MiniToolbars.Add(selectionMiniToolbar);
			ribbonControl.Name = "ribbonControl";
			ribbonControl.OptionsCustomizationForm.AllowToolbarCustomization = true;
			ribbonControl.OptionsExpandCollapseMenu.ShowRibbonLayoutGroup = DevExpress.Utils.DefaultBoolean.True;
			ribbonControl.OptionsTouch.ShowTouchUISelectorInQAT = true;
			ribbonControl.OptionsTouch.ShowTouchUISelectorVisibilityItemInQATMenu = true;
			ribbonControl.PageCategories.AddRange(new RibbonPageCategory[] { ribbonPageCategory1 });
			ribbonControl.PageHeaderItemLinks.Add(biStyle);
			ribbonControl.PageHeaderItemLinks.Add(barButtonItem1);
			ribbonControl.Pages.AddRange(new RibbonPage[] { ribbonPage2, ribbonPage1, ribbonPage3, rpThemes, rpHelp });
			ribbonControl.QuickToolbarItemLinks.Add(iOpen);
			ribbonControl.QuickToolbarItemLinks.Add(iSave);
			ribbonControl.QuickToolbarItemLinks.Add(iUndo);
			ribbonControl.QuickToolbarItemLinks.Add(iReplace, true);
			ribbonControl.QuickToolbarItemLinks.Add(iPaintStyle);
			ribbonControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemSpinEdit1, repositoryItemPictureEdit1, riicStyle, repositoryItemComboBox1, repositoryItemTrackBar1, repositoryItemColorPickEdit1, repositoryItemButtonEdit1, repositoryItemButtonEdit2 });
			ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.True;
			ribbonControl.ShowItemCaptionsInPageHeader = true;
			ribbonControl.Size = new System.Drawing.Size(1672, 158);
			ribbonControl.StatusBar = ribbonStatusBar1;
			ribbonControl.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.True;
			ribbonControl.BeforeApplicationButtonContentControlShow += ribbonControl1_BeforeApplicationButtonContentControlShow;
			ribbonControl.ApplicationButtonDoubleClick += ribbonControl1_ApplicationButtonDoubleClick;
			ribbonControl.ResetLayout += ribbonControl1_ResetLayout;
			// 
			// pmAppMain
			// 
			pmAppMain.BottomPaneControlContainer = pccBottom;
			pmAppMain.ItemLinks.Add(idNew);
			pmAppMain.ItemLinks.Add(iOpen);
			pmAppMain.ItemLinks.Add(sbiSave, true);
			pmAppMain.ItemLinks.Add(iPrint);
			pmAppMain.ItemLinks.Add(iClose, true);
			pmAppMain.Name = "pmAppMain";
			pmAppMain.Ribbon = ribbonControl;
			pmAppMain.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			pmAppMain.ShowRightPane = true;
			// 
			// pccBottom
			// 
			pccBottom.Appearance.BackColor = System.Drawing.Color.Transparent;
			pccBottom.Appearance.Options.UseBackColor = true;
			pccBottom.AutoSize = true;
			pccBottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			pccBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			pccBottom.Controls.Add(sbExit);
			pccBottom.Location = new System.Drawing.Point(2115, 620);
			pccBottom.Name = "pccBottom";
			pccBottom.Ribbon = ribbonControl;
			pccBottom.Size = new System.Drawing.Size(114, 30);
			pccBottom.TabIndex = 6;
			pccBottom.Visible = false;
			// 
			// sbExit
			// 
			sbExit.AllowFocus = false;
			sbExit.AutoSize = true;
			sbExit.ImageOptions.ImageIndex = 13;
			sbExit.ImageOptions.ImageList = imageCollection2;
			sbExit.Location = new System.Drawing.Point(5, 5);
			sbExit.Name = "sbExit";
			sbExit.Size = new System.Drawing.Size(106, 22);
			sbExit.TabIndex = 0;
			sbExit.Text = "E&xit Application";
			sbExit.Click += sbExit_Click;
			// 
			// imageCollection2
			// 
			imageCollection2.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("imageCollection2.ImageStream");
			imageCollection2.Images.SetKeyName(29, "SaveAs_16x16.png");
			// 
			// idNew
			// 
			idNew.ButtonStyle = BarButtonStyle.DropDown;
			idNew.Caption = "New";
			idNew.CategoryGuid = new Guid("4b511317-d784-42ba-b4ed-0d2a746d6c1f");
			idNew.Description = "Creates a new, blank file.";
			idNew.DropDownControl = pmNew;
			idNew.Hint = "Creates a new, blank file";
			idNew.Id = 0;
			idNew.ImageOptions.ImageIndex = 6;
			idNew.ImageOptions.LargeImageIndex = 0;
			idNew.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("idNew.ImageOptions.SvgImage");
			idNew.Name = "idNew";
			idNew.ItemClick += idNew_ItemClick;
			// 
			// pmNew
			// 
			pmNew.ItemLinks.Add(iNew);
			pmNew.ItemLinks.Add(iTemplate);
			pmNew.MenuCaption = "New";
			pmNew.Name = "pmNew";
			pmNew.Ribbon = ribbonControl;
			pmNew.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			// 
			// iNew
			// 
			iNew.Caption = "&New";
			iNew.CategoryGuid = new Guid("4b511317-d784-42ba-b4ed-0d2a746d6c1f");
			iNew.Description = "Creates a new, blank file.";
			iNew.Hint = "Creates a new, blank file";
			iNew.Id = 0;
			iNew.ImageOptions.ImageIndex = 6;
			iNew.ImageOptions.LargeImageIndex = 0;
			iNew.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iNew.ImageOptions.SvgImage");
			iNew.Name = "iNew";
			iNew.ItemClick += idNew_ItemClick;
			// 
			// iTemplate
			// 
			iTemplate.Caption = "Template...";
			iTemplate.CategoryGuid = new Guid("4b511317-d784-42ba-b4ed-0d2a746d6c1f");
			iTemplate.Description = "Creates a new template";
			iTemplate.Enabled = false;
			iTemplate.Hint = "Creates a new template";
			iTemplate.Id = 1;
			iTemplate.ImageOptions.ImageIndex = 6;
			iTemplate.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iTemplate.ImageOptions.SvgImage");
			iTemplate.Name = "iTemplate";
			// 
			// iOpen
			// 
			iOpen.Caption = "&Open...";
			iOpen.CategoryGuid = new Guid("4b511317-d784-42ba-b4ed-0d2a746d6c1f");
			iOpen.Description = "Opens a file.";
			iOpen.Hint = "Open a file";
			iOpen.Id = 1;
			iOpen.ImageOptions.ImageIndex = 7;
			iOpen.ImageOptions.LargeImageIndex = 9;
			iOpen.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iOpen.ImageOptions.SvgImage");
			iOpen.ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O);
			iOpen.Name = "iOpen";
			iOpen.RibbonStyle = RibbonItemStyles.SmallWithText | RibbonItemStyles.SmallWithoutText;
			iOpen.ItemClick += iOpen_ItemClick;
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
			sbiSave.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("sbiSave.ImageOptions.SvgImage");
			sbiSave.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(iSave), new LinkPersistInfo(iSaveAs) });
			sbiSave.MenuCaption = "Save";
			sbiSave.Name = "sbiSave";
			sbiSave.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			// 
			// iSave
			// 
			iSave.Caption = "&Save";
			iSave.CategoryGuid = new Guid("4b511317-d784-42ba-b4ed-0d2a746d6c1f");
			iSave.Description = "Saves the active document with its current file name.";
			iSave.Hint = "Saves the active document with its current file name";
			iSave.Id = 3;
			iSave.ImageOptions.ImageIndex = 10;
			iSave.ImageOptions.LargeImageIndex = 7;
			iSave.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iSave.ImageOptions.SvgImage");
			iSave.Name = "iSave";
			iSave.RibbonStyle = RibbonItemStyles.SmallWithText;
			iSave.ItemClick += iSave_ItemClick;
			// 
			// iSaveAs
			// 
			iSaveAs.Caption = "Save &As...";
			iSaveAs.CategoryGuid = new Guid("4b511317-d784-42ba-b4ed-0d2a746d6c1f");
			iSaveAs.Description = "Saves the active document with a different file name.";
			iSaveAs.Hint = "Saves the active document with a different file name";
			iSaveAs.Id = 4;
			iSaveAs.ImageOptions.ImageIndex = 21;
			iSaveAs.ImageOptions.LargeImageIndex = 2;
			iSaveAs.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iSaveAs.ImageOptions.SvgImage");
			iSaveAs.Name = "iSaveAs";
			iSaveAs.ItemClick += iSaveAs_ItemClick;
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
			iPrint.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iPrint.ImageOptions.SvgImage");
			iPrint.Name = "iPrint";
			iPrint.RibbonStyle = RibbonItemStyles.SmallWithText | RibbonItemStyles.SmallWithoutText;
			iPrint.ItemClick += iPrint_ItemClick;
			// 
			// iClose
			// 
			iClose.Caption = "&Close";
			iClose.CategoryGuid = new Guid("4b511317-d784-42ba-b4ed-0d2a746d6c1f");
			iClose.Description = "Closes the active document.";
			iClose.Hint = "Closes the active document";
			iClose.Id = 2;
			iClose.ImageOptions.ImageIndex = 12;
			iClose.ImageOptions.LargeImageIndex = 8;
			iClose.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iClose.ImageOptions.SvgImage");
			iClose.Name = "iClose";
			iClose.RibbonStyle = RibbonItemStyles.SmallWithText | RibbonItemStyles.SmallWithoutText;
			iClose.ItemClick += iClose_ItemClick;
			// 
			// iUndo
			// 
			iUndo.Caption = "&Undo";
			iUndo.CategoryGuid = new Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1");
			iUndo.Description = "Reverses the last command or deletes the last entry you typed.";
			iUndo.Hint = "Undo";
			iUndo.Id = 8;
			iUndo.ImageOptions.ImageIndex = 11;
			iUndo.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iUndo.ImageOptions.SvgImage");
			iUndo.ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z);
			iUndo.Name = "iUndo";
			iUndo.ItemClick += iUndo_ItemClick;
			// 
			// iReplace
			// 
			iReplace.Caption = "R&eplace...";
			iReplace.CategoryGuid = new Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1");
			iReplace.Description = "Searches for and replaces the specified text.";
			iReplace.Hint = "Replace";
			iReplace.Id = 15;
			iReplace.ImageOptions.ImageIndex = 14;
			iReplace.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iReplace.ImageOptions.SvgImage");
			iReplace.ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H);
			iReplace.Name = "iReplace";
			iReplace.ItemClick += iReplace_ItemClick;
			// 
			// iExit
			// 
			iExit.Caption = "E&xit";
			iExit.CategoryGuid = new Guid("4b511317-d784-42ba-b4ed-0d2a746d6c1f");
			iExit.Description = "Closes this program after prompting you to save unsaved document.";
			iExit.Hint = "Closes this program after prompting you to save unsaved document.";
			iExit.Id = 6;
			iExit.ImageOptions.ImageIndex = 22;
			iExit.ImageOptions.LargeImageIndex = 1;
			iExit.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iExit.ImageOptions.SvgImage");
			iExit.Name = "iExit";
			iExit.ItemClick += iExit_ItemClick;
			// 
			// iFind
			// 
			iFind.Caption = "&Find...";
			iFind.CategoryGuid = new Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1");
			iFind.Description = "Searches for the specified text.";
			iFind.Hint = "Find";
			iFind.Id = 14;
			iFind.ImageOptions.ImageIndex = 3;
			iFind.ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F);
			iFind.Name = "iFind";
			iFind.ItemClick += iFind_ItemClick;
			// 
			// iBullets
			// 
			iBullets.ButtonStyle = BarButtonStyle.Check;
			iBullets.Caption = "&Bullets";
			iBullets.CategoryGuid = new Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258");
			iBullets.Description = "Adds bullets to or removes bullets from selected paragraphs.";
			iBullets.Hint = "Bullets";
			iBullets.Id = 18;
			iBullets.ImageOptions.ImageIndex = 0;
			iBullets.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iBullets.ImageOptions.SvgImage");
			iBullets.Name = "iBullets";
			iBullets.ItemClick += iBullets_ItemClick;
			// 
			// iProtected
			// 
			iProtected.Caption = "P&rotected";
			iProtected.CategoryGuid = new Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258");
			iProtected.Description = "Protects the selected text.";
			iProtected.Hint = "Protects the selected text";
			iProtected.Id = 19;
			iProtected.Name = "iProtected";
			iProtected.ItemClick += iProtected_ItemClick;
			// 
			// iBold
			// 
			iBold.ButtonStyle = BarButtonStyle.Check;
			iBold.Caption = "&Bold";
			iBold.CategoryGuid = new Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258");
			iBold.Description = "Makes selected text and numbers bold. If the selection is already bold, clicking button removes bold formatting.";
			iBold.Hint = "Bold";
			iBold.Id = 24;
			iBold.ImageOptions.ImageIndex = 15;
			iBold.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iBold.ImageOptions.SvgImage");
			iBold.ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B);
			iBold.Name = "iBold";
			iBold.ItemClick += iFontStyle_ItemClick;
			// 
			// iItalic
			// 
			iItalic.ButtonStyle = BarButtonStyle.Check;
			iItalic.Caption = "&Italic";
			iItalic.CategoryGuid = new Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258");
			iItalic.Description = "Makes selected text and numbers italic. If the selection is already italic, clicking button removes italic formatting.";
			iItalic.Hint = "Italic";
			iItalic.Id = 25;
			iItalic.ImageOptions.ImageIndex = 16;
			iItalic.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iItalic.ImageOptions.SvgImage");
			iItalic.ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I);
			iItalic.Name = "iItalic";
			iItalic.ItemClick += iFontStyle_ItemClick;
			// 
			// iUnderline
			// 
			iUnderline.ButtonStyle = BarButtonStyle.Check;
			iUnderline.Caption = "&Underline";
			iUnderline.CategoryGuid = new Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258");
			iUnderline.Description = "Underlines selected text and numbers. If the selection is already underlined, clicking button removes underlining.";
			iUnderline.Hint = "Underline";
			iUnderline.Id = 26;
			iUnderline.ImageOptions.ImageIndex = 17;
			iUnderline.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iUnderline.ImageOptions.SvgImage");
			iUnderline.ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U);
			iUnderline.Name = "iUnderline";
			iUnderline.ItemClick += iFontStyle_ItemClick;
			// 
			// iAlignLeft
			// 
			iAlignLeft.ButtonStyle = BarButtonStyle.Check;
			iAlignLeft.Caption = "Align &Left";
			iAlignLeft.CategoryGuid = new Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258");
			iAlignLeft.Description = "Aligns the selected text to the left.";
			iAlignLeft.GroupIndex = 1;
			iAlignLeft.Hint = "Align Left";
			iAlignLeft.Id = 27;
			iAlignLeft.ImageOptions.ImageIndex = 18;
			iAlignLeft.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iAlignLeft.ImageOptions.SvgImage");
			iAlignLeft.ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L);
			iAlignLeft.Name = "iAlignLeft";
			iAlignLeft.ItemClick += iAlign_ItemClick;
			// 
			// iAlignRight
			// 
			iAlignRight.ButtonStyle = BarButtonStyle.Check;
			iAlignRight.Caption = "Align &Right";
			iAlignRight.CategoryGuid = new Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258");
			iAlignRight.Description = "Aligns the selected text to the right.";
			iAlignRight.GroupIndex = 1;
			iAlignRight.Hint = "Align Right";
			iAlignRight.Id = 29;
			iAlignRight.ImageOptions.ImageIndex = 20;
			iAlignRight.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iAlignRight.ImageOptions.SvgImage");
			iAlignRight.ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R);
			iAlignRight.Name = "iAlignRight";
			iAlignRight.ItemClick += iAlign_ItemClick;
			// 
			// iFontColor
			// 
			iFontColor.ButtonStyle = BarButtonStyle.DropDown;
			iFontColor.Caption = "Font C&olor";
			iFontColor.CategoryGuid = new Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258");
			iFontColor.Description = "Formats the selected text with the color you click.";
			iFontColor.DropDownControl = gddFontColor;
			iFontColor.Hint = "Font Color";
			iFontColor.Id = 30;
			iFontColor.ImageOptions.ImageIndex = 5;
			iFontColor.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iFontColor.ImageOptions.SvgImage");
			iFontColor.Name = "iFontColor";
			iFontColor.RibbonStyle = RibbonItemStyles.SmallWithText | RibbonItemStyles.SmallWithoutText;
			iFontColor.ItemClick += iFontColor_ItemClick;
			// 
			// gddFontColor
			// 
			// 
			// 
			// 
			gddFontColor.Gallery.Appearance.ItemCaptionAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 6.75F);
			gddFontColor.Gallery.Appearance.ItemCaptionAppearance.Hovered.Options.UseFont = true;
			gddFontColor.Gallery.Appearance.ItemCaptionAppearance.Hovered.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			gddFontColor.Gallery.Appearance.ItemCaptionAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 6.75F);
			gddFontColor.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseFont = true;
			gddFontColor.Gallery.Appearance.ItemCaptionAppearance.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			gddFontColor.Gallery.Appearance.ItemCaptionAppearance.Pressed.Font = new System.Drawing.Font("Tahoma", 6.75F);
			gddFontColor.Gallery.Appearance.ItemCaptionAppearance.Pressed.Options.UseFont = true;
			gddFontColor.Gallery.Appearance.ItemCaptionAppearance.Pressed.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			gddFontColor.Gallery.FilterCaption = "All Colors";
			gddFontColor.Gallery.FixedImageSize = false;
			galleryItemGroup2.Caption = "Web Colors";
			galleryItemGroup3.Caption = "System Colors";
			gddFontColor.Gallery.Groups.AddRange(new GalleryItemGroup[] { galleryItemGroup2, galleryItemGroup3 });
			gddFontColor.Gallery.ImageSize = new System.Drawing.Size(48, 16);
			gddFontColor.Gallery.ItemImageLocation = DevExpress.Utils.Locations.Top;
			gddFontColor.Gallery.RowCount = 5;
			gddFontColor.Gallery.ShowItemText = true;
			gddFontColor.Gallery.SizeMode = GallerySizeMode.Both;
			gddFontColor.MenuCaption = "Font Colors";
			gddFontColor.Name = "gddFontColor";
			gddFontColor.Ribbon = ribbonControl;
			gddFontColor.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			gddFontColor.GalleryItemClick += gddFontColor_Gallery_ItemClick;
			gddFontColor.GalleryCustomDrawItemImage += gddFontColor_Gallery_CustomDrawItemImage;
			gddFontColor.Popup += gddFontColor_Popup;
			// 
			// siPosition
			// 
			siPosition.CategoryGuid = new Guid("77795bb7-9bc5-4dd2-a297-cc758682e23d");
			siPosition.Id = 0;
			siPosition.Name = "siPosition";
			siPosition.VisibleInSearchMenu = false;
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
			// bgFontStyle
			// 
			bgFontStyle.Caption = "FontStyle";
			bgFontStyle.Id = 0;
			bgFontStyle.ItemLinks.Add(iBold);
			bgFontStyle.ItemLinks.Add(iItalic);
			bgFontStyle.ItemLinks.Add(iUnderline);
			bgFontStyle.Name = "bgFontStyle";
			bgFontStyle.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			// 
			// bgAlign
			// 
			bgAlign.Caption = "Align";
			bgAlign.Id = 0;
			bgAlign.ItemLinks.Add(iAlignLeft);
			bgAlign.ItemLinks.Add(iCenter);
			bgAlign.ItemLinks.Add(iAlignRight);
			bgAlign.Name = "bgAlign";
			bgAlign.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			// 
			// bgFont
			// 
			bgFont.Caption = "Font";
			bgFont.Id = 0;
			bgFont.ItemLinks.Add(iFont);
			bgFont.ItemLinks.Add(iFontColor);
			bgFont.Name = "bgFont";
			bgFont.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			// 
			// bgBullets
			// 
			bgBullets.Caption = "Bullets";
			bgBullets.Id = 1;
			bgBullets.ItemLinks.Add(iBullets);
			bgBullets.Name = "bgBullets";
			bgBullets.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			// 
			// sbiPaste
			// 
			sbiPaste.Caption = "Paste";
			sbiPaste.CategoryGuid = new Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1");
			sbiPaste.Description = "Inserts the contents of the Clipboard at the insertion point";
			sbiPaste.Hint = "Inserts the contents of the Clipboard at the insertion point";
			sbiPaste.Id = 1;
			sbiPaste.ImageOptions.ImageIndex = 8;
			sbiPaste.ImageOptions.LargeImageIndex = 3;
			sbiPaste.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("sbiPaste.ImageOptions.SvgImage");
			sbiPaste.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(iPaste), new LinkPersistInfo(iPasteSpecial) });
			sbiPaste.MenuCaption = "Paste";
			sbiPaste.Name = "sbiPaste";
			sbiPaste.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			// 
			// iPasteSpecial
			// 
			iPasteSpecial.Caption = "Paste &Special...";
			iPasteSpecial.CategoryGuid = new Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1");
			iPasteSpecial.Description = "Opens the Paste Special dialog";
			iPasteSpecial.Enabled = false;
			iPasteSpecial.Hint = "Opens the Paste Special dialog";
			iPasteSpecial.Id = 3;
			iPasteSpecial.ImageOptions.ImageIndex = 8;
			iPasteSpecial.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iPasteSpecial.ImageOptions.SvgImage");
			iPasteSpecial.Name = "iPasteSpecial";
			// 
			// sbiFind
			// 
			sbiFind.Caption = "Find";
			sbiFind.CategoryGuid = new Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1");
			sbiFind.Description = "Searches for the specified text";
			sbiFind.Hint = "Searches for the specified text";
			sbiFind.Id = 2;
			sbiFind.ImageOptions.ImageIndex = 3;
			sbiFind.ImageOptions.LargeImageIndex = 4;
			sbiFind.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("sbiFind.ImageOptions.SvgImage");
			sbiFind.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(iFind), new LinkPersistInfo(iReplace) });
			sbiFind.MenuCaption = "Find and Replace";
			sbiFind.Name = "sbiFind";
			sbiFind.RibbonStyle = RibbonItemStyles.Large | RibbonItemStyles.SmallWithText | RibbonItemStyles.SmallWithoutText;
			sbiFind.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			// 
			// iLargeUndo
			// 
			iLargeUndo.Caption = "&Undo";
			iLargeUndo.CategoryGuid = new Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1");
			iLargeUndo.Hint = "Undo";
			iLargeUndo.Id = 0;
			iLargeUndo.ImageOptions.ImageIndex = 11;
			iLargeUndo.ImageOptions.LargeImageIndex = 5;
			iLargeUndo.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iLargeUndo.ImageOptions.SvgImage");
			iLargeUndo.Name = "iLargeUndo";
			iLargeUndo.ItemClick += iUndo_ItemClick;
			// 
			// iPaintStyle
			// 
			iPaintStyle.ActAsDropDown = true;
			iPaintStyle.ButtonStyle = BarButtonStyle.DropDown;
			iPaintStyle.Caption = "Paint style";
			iPaintStyle.Description = "Select a paint scheme";
			iPaintStyle.Hint = "Select a paint scheme";
			iPaintStyle.Id = 7;
			iPaintStyle.ImageOptions.ImageIndex = 26;
			iPaintStyle.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("iPaintStyle.ImageOptions.SvgImage");
			iPaintStyle.Name = "iPaintStyle";
			iPaintStyle.VisibleInSearchMenu = false;
			// 
			// rgbiSkins
			// 
			rgbiSkins.Caption = "Skins";
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
			// rgbiFont
			// 
			rgbiFont.Caption = "Font";
			// 
			// 
			// 
			galleryItemGroup4.Caption = "Main";
			rgbiFont.Gallery.Groups.AddRange(new GalleryItemGroup[] { galleryItemGroup4 });
			rgbiFont.Gallery.ImageSize = new System.Drawing.Size(40, 40);
			rgbiFont.Gallery.ItemClick += rgbiFont_Gallery_ItemClick;
			rgbiFont.GalleryDropDown = gddFont;
			rgbiFont.Id = 29;
			rgbiFont.Name = "rgbiFont";
			// 
			// rgbiFontColor
			// 
			rgbiFontColor.Caption = "Color";
			// 
			// 
			// 
			rgbiFontColor.Gallery.ColumnCount = 10;
			galleryItemGroup5.Caption = "Main";
			rgbiFontColor.Gallery.Groups.AddRange(new GalleryItemGroup[] { galleryItemGroup5 });
			rgbiFontColor.Gallery.ImageSize = new System.Drawing.Size(20, 14);
			rgbiFontColor.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.Stretch;
			rgbiFontColor.Gallery.ItemClick += rgbiFontColor_Gallery_ItemClick;
			rgbiFontColor.Gallery.CustomDrawItemImage += gddFontColor_Gallery_CustomDrawItemImage;
			rgbiFontColor.GalleryDropDown = gddFontColor;
			rgbiFontColor.Id = 37;
			rgbiFontColor.Name = "rgbiFontColor";
			// 
			// barEditItem1
			// 
			barEditItem1.Alignment = BarItemLinkAlignment.Right;
			barEditItem1.CanOpenEdit = false;
			barEditItem1.Edit = repositoryItemPictureEdit1;
			barEditItem1.EditWidth = 130;
			barEditItem1.Id = 94;
			barEditItem1.Name = "barEditItem1";
			barEditItem1.ItemPress += barEditItem1_ItemPress;
			// 
			// repositoryItemPictureEdit1
			// 
			repositoryItemPictureEdit1.AllowFocused = false;
			repositoryItemPictureEdit1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
			// 
			// biStyle
			// 
			biStyle.Caption = "Ribbon Style: ";
			biStyle.Edit = riicStyle;
			biStyle.EditWidth = 85;
			biStyle.Hint = "Ribbon Style";
			biStyle.Id = 106;
			biStyle.Name = "biStyle";
			biStyle.VisibleInSearchMenu = false;
			biStyle.EditValueChanged += biStyle_EditValueChanged;
			// 
			// riicStyle
			// 
			riicStyle.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
			riicStyle.Appearance.Options.UseFont = true;
			riicStyle.AutoHeight = false;
			riicStyle.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
			riicStyle.Name = "riicStyle";
			// 
			// editButtonGroup
			// 
			editButtonGroup.Id = 145;
			editButtonGroup.ItemLinks.Add(iCut);
			editButtonGroup.ItemLinks.Add(iCopy);
			editButtonGroup.ItemLinks.Add(iPaste);
			editButtonGroup.ItemLinks.Add(iClear);
			editButtonGroup.Name = "editButtonGroup";
			editButtonGroup.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			// 
			// barToggleSwitchItem1
			// 
			barToggleSwitchItem1.Caption = "Auto Save";
			barToggleSwitchItem1.Id = 213;
			barToggleSwitchItem1.Name = "barToggleSwitchItem1";
			barToggleSwitchItem1.Visibility = BarItemVisibility.Never;
			// 
			// bbColorMix
			// 
			bbColorMix.Caption = "&Color Mix";
			bbColorMix.Id = 238;
			bbColorMix.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("bbColorMix.ImageOptions.Image");
			bbColorMix.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("bbColorMix.ImageOptions.LargeImage");
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
			barButtonItem1.ItemAppearance.Normal.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
			barButtonItem1.ItemAppearance.Normal.Options.UseBackColor = true;
			barButtonItem1.Name = "barButtonItem1";
			barButtonItem1.VisibleInSearchMenu = false;
			// 
			// biOnlineHelp
			// 
			biOnlineHelp.Caption = "Online Help";
			biOnlineHelp.Id = 423;
			biOnlineHelp.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("biOnlineHelp.ImageOptions.SvgImage");
			biOnlineHelp.Name = "biOnlineHelp";
			// 
			// biGettingStarted
			// 
			biGettingStarted.Caption = "Getting Started";
			biGettingStarted.Id = 424;
			biGettingStarted.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("biGettingStarted.ImageOptions.SvgImage");
			biGettingStarted.Name = "biGettingStarted";
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
			rgbiColorScheme.Caption = "ribbonGalleryBarItem1";
			// 
			// 
			// 
			galleryItemGroup6.Caption = "Schemes";
			galleryItem1.Caption = "Yellow";
			galleryItem1.ImageOptions.Image = Properties.Resources.Scheme;
			galleryItem1.Visible = false;
			galleryItem2.AppearanceCaption.Normal.ForeColor = System.Drawing.Color.FromArgb(0, 114, 198);
			galleryItem2.AppearanceCaption.Normal.Options.UseForeColor = true;
			galleryItem2.Caption = "Blue";
			galleryItem3.AppearanceCaption.Normal.ForeColor = System.Drawing.Color.FromArgb(0, 135, 82);
			galleryItem3.AppearanceCaption.Normal.Options.UseForeColor = true;
			galleryItem3.Caption = "Green";
			galleryItem4.AppearanceCaption.Normal.ForeColor = System.Drawing.Color.FromArgb(197, 68, 24);
			galleryItem4.AppearanceCaption.Normal.Options.UseForeColor = true;
			galleryItem4.Caption = "Orange";
			galleryItem5.AppearanceCaption.Normal.ForeColor = System.Drawing.Color.FromArgb(179, 75, 223);
			galleryItem5.AppearanceCaption.Normal.Options.UseForeColor = true;
			galleryItem5.Caption = "Purple";
			galleryItem6.AppearanceCaption.Normal.ForeColor = System.Drawing.Color.FromArgb(0, 114, 198);
			galleryItem6.AppearanceCaption.Normal.Options.UseForeColor = true;
			galleryItem6.Caption = "Default";
			galleryItem7.AppearanceCaption.Normal.ForeColor = System.Drawing.Color.FromArgb(0, 128, 121);
			galleryItem7.AppearanceCaption.Normal.Options.UseForeColor = true;
			galleryItem7.Caption = "Teal";
			galleryItem8.AppearanceCaption.Normal.ForeColor = System.Drawing.Color.FromArgb(224, 61, 82);
			galleryItem8.AppearanceCaption.Normal.Options.UseForeColor = true;
			galleryItem8.Caption = "Red";
			galleryItem9.AppearanceCaption.Normal.ForeColor = System.Drawing.Color.FromArgb(0, 114, 198);
			galleryItem9.AppearanceCaption.Normal.Options.UseForeColor = true;
			galleryItem9.Caption = "Dark Blue";
			galleryItem9.Checked = true;
			galleryItemGroup6.Items.AddRange(new GalleryItem[] { galleryItem1, galleryItem2, galleryItem3, galleryItem4, galleryItem5, galleryItem6, galleryItem7, galleryItem8, galleryItem9 });
			rgbiColorScheme.Gallery.Groups.AddRange(new GalleryItemGroup[] { galleryItemGroup6 });
			rgbiColorScheme.Gallery.ImageSize = new System.Drawing.Size(32, 32);
			rgbiColorScheme.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.SingleRadio;
			rgbiColorScheme.Gallery.InitDropDownGallery += rgbiColorScheme_Gallery_InitDropDownGallery;
			rgbiColorScheme.Gallery.ItemCheckedChanged += rgbiColorScheme_Gallery_ItemCheckedChanged;
			rgbiColorScheme.Id = 426;
			rgbiColorScheme.Name = "rgbiColorScheme";
			// 
			// biPageColor
			// 
			biPageColor.Caption = "Page Color";
			biPageColor.Id = 438;
			biPageColor.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("biPageColor.ImageOptions.SvgImage");
			biPageColor.Name = "biPageColor";
			// 
			// biPageBorders
			// 
			biPageBorders.Caption = "Page Borders";
			biPageBorders.Id = 439;
			biPageBorders.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("biPageBorders.ImageOptions.SvgImage");
			biPageBorders.Name = "biPageBorders";
			biPageBorders.ItemClick += barButtonItem2_ItemClick;
			// 
			// skinPaletteRibbonGalleryBarItem1
			// 
			skinPaletteRibbonGalleryBarItem1.Caption = "skinPaletteRibbonGalleryBarItem1";
			skinPaletteRibbonGalleryBarItem1.Id = 453;
			skinPaletteRibbonGalleryBarItem1.Name = "skinPaletteRibbonGalleryBarItem1";
			// 
			// barButtonItemConnect
			// 
			barButtonItemConnect.Caption = "Connect";
			barButtonItemConnect.Id = 466;
			barButtonItemConnect.ImageOptions.Image = Properties.Resources.Server_Connect;
			barButtonItemConnect.ImageOptions.LargeImage = Properties.Resources.Server_Connect_Large;
			barButtonItemConnect.Name = "barButtonItemConnect";
			barButtonItemConnect.ItemClick += barButtonItemConnect_ItemClick;
			// 
			// barEditItemMonitorServer
			// 
			barEditItemMonitorServer.Caption = "Server";
			barEditItemMonitorServer.Edit = repositoryItemButtonEdit1;
			barEditItemMonitorServer.EditWidth = 115;
			barEditItemMonitorServer.Id = 467;
			barEditItemMonitorServer.Name = "barEditItemMonitorServer";
			// 
			// repositoryItemButtonEdit1
			// 
			repositoryItemButtonEdit1.AutoHeight = false;
			repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
			repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
			// 
			// barEditItemServerMonitorPort
			// 
			barEditItemServerMonitorPort.Caption = "Server Monitor Port";
			barEditItemServerMonitorPort.Edit = repositoryItemSpinEdit1;
			barEditItemServerMonitorPort.Id = 468;
			barEditItemServerMonitorPort.Name = "barEditItemServerMonitorPort";
			// 
			// barButtonItemDisconnect
			// 
			barButtonItemDisconnect.Caption = "Disconnect";
			barButtonItemDisconnect.Id = 469;
			barButtonItemDisconnect.ImageOptions.Image = Properties.Resources.Server_Disconnect;
			barButtonItemDisconnect.ImageOptions.LargeImage = Properties.Resources.Server_Disconnect_Large;
			barButtonItemDisconnect.Name = "barButtonItemDisconnect";
			// 
			// barButtonItemServerStart
			// 
			barButtonItemServerStart.Caption = "Start";
			barButtonItemServerStart.Id = 470;
			barButtonItemServerStart.ImageOptions.Image = Properties.Resources.Play_Green;
			barButtonItemServerStart.ImageOptions.LargeImage = Properties.Resources.Play_Green_Large;
			barButtonItemServerStart.Name = "barButtonItemServerStart";
			// 
			// barButtonItemServerRestart
			// 
			barButtonItemServerRestart.Caption = "Restart";
			barButtonItemServerRestart.Id = 471;
			barButtonItemServerRestart.ImageOptions.Image = Properties.Resources.Reload;
			barButtonItemServerRestart.ImageOptions.LargeImage = Properties.Resources.Reload_Large;
			barButtonItemServerRestart.Name = "barButtonItemServerRestart";
			// 
			// barButtonItemServerStop
			// 
			barButtonItemServerStop.Caption = "Stop";
			barButtonItemServerStop.Id = 472;
			barButtonItemServerStop.ImageOptions.Image = Properties.Resources.Stop_Red;
			barButtonItemServerStop.ImageOptions.LargeImage = Properties.Resources.Stop_Red_Large;
			barButtonItemServerStop.Name = "barButtonItemServerStop";
			// 
			// rgbiSkins2
			// 
			rgbiSkins2.Caption = "ribbonGalleryBarItem2";
			rgbiSkins2.Id = 474;
			rgbiSkins2.Name = "rgbiSkins2";
			// 
			// skinPaletteRibbonGalleryBarItem2
			// 
			skinPaletteRibbonGalleryBarItem2.Caption = "skinPaletteRibbonGalleryBarItem2";
			skinPaletteRibbonGalleryBarItem2.Id = 475;
			skinPaletteRibbonGalleryBarItem2.Name = "skinPaletteRibbonGalleryBarItem2";
			// 
			// barButtonItemColorMix
			// 
			barButtonItemColorMix.Caption = "Color Mix";
			barButtonItemColorMix.Id = 476;
			barButtonItemColorMix.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItemColorMix.ImageOptions.Image");
			barButtonItemColorMix.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barButtonItemColorMix.ImageOptions.LargeImage");
			barButtonItemColorMix.Name = "barButtonItemColorMix";
			// 
			// barToggleSwitchItemDarkMode
			// 
			barToggleSwitchItemDarkMode.Caption = "Dark Mode";
			barToggleSwitchItemDarkMode.Id = 477;
			barToggleSwitchItemDarkMode.Name = "barToggleSwitchItemDarkMode";
			// 
			// barToggleSwitchItemCompactView
			// 
			barToggleSwitchItemCompactView.Caption = "Compact View";
			barToggleSwitchItemCompactView.Id = 479;
			barToggleSwitchItemCompactView.Name = "barToggleSwitchItemCompactView";
			// 
			// barStaticItemMonitorServer
			// 
			barStaticItemMonitorServer.Caption = "Monitor Server";
			barStaticItemMonitorServer.Id = 480;
			barStaticItemMonitorServer.ImageOptions.Image = Properties.Resources.Server;
			barStaticItemMonitorServer.Name = "barStaticItemMonitorServer";
			barStaticItemMonitorServer.PaintStyle = BarItemPaintStyle.CaptionGlyph;
			// 
			// barStaticItemUser
			// 
			barStaticItemUser.Caption = "Monitor Admin";
			barStaticItemUser.Id = 481;
			barStaticItemUser.ImageOptions.Image = Properties.Resources.Administrator;
			barStaticItemUser.Name = "barStaticItemUser";
			barStaticItemUser.PaintStyle = BarItemPaintStyle.CaptionGlyph;
			// 
			// imageCollection1
			// 
			imageCollection1.ImageSize = new System.Drawing.Size(32, 32);
			imageCollection1.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("imageCollection1.ImageStream");
			// 
			// selectionMiniToolbar
			// 
			selectionMiniToolbar.Alignment = System.Drawing.ContentAlignment.TopRight;
			selectionMiniToolbar.ItemLinks.Add(bgFont);
			selectionMiniToolbar.ItemLinks.Add(bgFontStyle);
			selectionMiniToolbar.ItemLinks.Add(bgAlign);
			selectionMiniToolbar.ItemLinks.Add(editButtonGroup);
			selectionMiniToolbar.ParentControl = this;
			// 
			// ribbonPageCategory1
			// 
			ribbonPageCategory1.Name = "ribbonPageCategory1";
			ribbonPageCategory1.Pages.AddRange(new RibbonPage[] { ribbonPage4 });
			ribbonPageCategory1.Text = "Selection";
			// 
			// ribbonPage4
			// 
			ribbonPage4.Groups.AddRange(new RibbonPageGroup[] { ribbonPageGroup12, ribbonPageGroup13 });
			ribbonPage4.Name = "ribbonPage4";
			ribbonPage4.Text = "Selection";
			// 
			// ribbonPageGroup12
			// 
			ribbonPageGroup12.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
			ribbonPageGroup12.ItemLinks.Add(sbiPaste);
			ribbonPageGroup12.ItemLinks.Add(iCut, true);
			ribbonPageGroup12.ItemLinks.Add(iCopy);
			ribbonPageGroup12.ItemLinks.Add(iClear);
			ribbonPageGroup12.Name = "ribbonPageGroup12";
			ribbonPageGroup12.Text = "Edit";
			// 
			// ribbonPageGroup13
			// 
			ribbonPageGroup13.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
			ribbonPageGroup13.ItemLinks.Add(iBold);
			ribbonPageGroup13.ItemLinks.Add(iItalic);
			ribbonPageGroup13.ItemLinks.Add(iUnderline);
			ribbonPageGroup13.ItemLinks.Add(rgbiFont);
			ribbonPageGroup13.ItemLinks.Add(rgbiFontColor);
			ribbonPageGroup13.Name = "ribbonPageGroup13";
			ribbonPageGroup13.Text = "Format";
			// 
			// ribbonPage2
			// 
			ribbonPage2.Groups.AddRange(new RibbonPageGroup[] { ribbonPageGroupMonitorServer, ribbonPageGroupSimpleObjectsServer, ribbonPageGroupLookAndFeel });
			ribbonPage2.Name = "ribbonPage2";
			ribbonPage2.Text = "Home";
			// 
			// ribbonPageGroupMonitorServer
			// 
			ribbonPageGroupMonitorServer.ItemLinks.Add(barButtonItemConnect);
			ribbonPageGroupMonitorServer.ItemLinks.Add(barEditItemMonitorServer, true);
			ribbonPageGroupMonitorServer.ItemLinks.Add(barEditItemServerMonitorPort);
			ribbonPageGroupMonitorServer.ItemLinks.Add(barButtonItemDisconnect, true);
			ribbonPageGroupMonitorServer.Name = "ribbonPageGroupMonitorServer";
			ribbonPageGroupMonitorServer.Text = "Simple.Objects™ Server Monitor Connection";
			// 
			// ribbonPageGroupSimpleObjectsServer
			// 
			ribbonPageGroupSimpleObjectsServer.ItemLinks.Add(barButtonItemServerStart);
			ribbonPageGroupSimpleObjectsServer.ItemLinks.Add(barButtonItemServerRestart);
			ribbonPageGroupSimpleObjectsServer.ItemLinks.Add(barButtonItemServerStop);
			ribbonPageGroupSimpleObjectsServer.Name = "ribbonPageGroupSimpleObjectsServer";
			ribbonPageGroupSimpleObjectsServer.Text = "Simple.Objects™ Server";
			// 
			// ribbonPageGroupLookAndFeel
			// 
			ribbonPageGroupLookAndFeel.ItemLinks.Add(rgbiSkins2);
			ribbonPageGroupLookAndFeel.ItemLinks.Add(skinPaletteRibbonGalleryBarItem2);
			ribbonPageGroupLookAndFeel.ItemLinks.Add(barButtonItemColorMix);
			ribbonPageGroupLookAndFeel.ItemLinks.Add(barToggleSwitchItemDarkMode);
			ribbonPageGroupLookAndFeel.ItemLinks.Add(barToggleSwitchItemCompactView);
			ribbonPageGroupLookAndFeel.Name = "ribbonPageGroupLookAndFeel";
			ribbonPageGroupLookAndFeel.Text = "Look and Feel";
			// 
			// ribbonPage1
			// 
			ribbonPage1.Groups.AddRange(new RibbonPageGroup[] { ribbonPageGroup1, ribbonPageGroup2, ribbonPageGroup3, ribbonPageGroup4, ribbonPageGroup8, ribbonPageGroup9 });
			ribbonPage1.Name = "ribbonPage1";
			ribbonPage1.Text = "Document";
			// 
			// ribbonPageGroup1
			// 
			ribbonPageGroup1.ImageOptions.ImageIndex = 1;
			ribbonPageGroup1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ribbonPageGroup1.ImageOptions.SvgImage");
			ribbonPageGroup1.ItemLinks.Add(idNew);
			ribbonPageGroup1.ItemLinks.Add(iOpen);
			ribbonPageGroup1.ItemLinks.Add(iClose);
			ribbonPageGroup1.ItemLinks.Add(iPrint);
			ribbonPageGroup1.ItemLinks.Add(barToggleSwitchItem1);
			ribbonPageGroup1.ItemLinks.Add(sbiSave, true);
			ribbonPageGroup1.Name = "ribbonPageGroup1";
			toolTipTitleItem1.Text = "Open File Dialog";
			toolTipItem1.Appearance.Image = (System.Drawing.Image)resources.GetObject("resource.Image");
			toolTipItem1.Appearance.Options.UseImage = true;
			toolTipItem1.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("resource.Image1");
			toolTipItem1.Text = "Show the Open file dialog box";
			toolTipTitleItem2.Appearance.Image = (System.Drawing.Image)resources.GetObject("resource.Image2");
			toolTipTitleItem2.Appearance.Options.UseImage = true;
			toolTipTitleItem2.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("resource.Image3");
			toolTipTitleItem2.Text = "For more information see help";
			superToolTip1.Items.Add(toolTipTitleItem1);
			superToolTip1.Items.Add(toolTipItem1);
			superToolTip1.Items.Add(toolTipSeparatorItem1);
			superToolTip1.Items.Add(toolTipTitleItem2);
			ribbonPageGroup1.SuperTip = superToolTip1;
			ribbonPageGroup1.Text = "File";
			ribbonPageGroup1.CaptionButtonClick += ribbonPageGroup1_CaptionButtonClick;
			// 
			// ribbonPageGroup2
			// 
			ribbonPageGroup2.ImageOptions.ImageIndex = 2;
			ribbonPageGroup2.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ribbonPageGroup2.ImageOptions.SvgImage");
			ribbonPageGroup2.ItemLinks.Add(sbiPaste);
			ribbonPageGroup2.ItemLinks.Add(iCut);
			ribbonPageGroup2.ItemLinks.Add(iCopy);
			ribbonPageGroup2.ItemLinks.Add(iClear);
			ribbonPageGroup2.ItemLinks.Add(iUndo, true);
			ribbonPageGroup2.ItemLinks.Add(iSelectAll);
			ribbonPageGroup2.Name = "ribbonPageGroup2";
			toolTipTitleItem3.Text = "Edit Popup Menu";
			toolTipItem2.Appearance.Image = (System.Drawing.Image)resources.GetObject("resource.Image4");
			toolTipItem2.Appearance.Options.UseImage = true;
			toolTipItem2.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("resource.Image5");
			toolTipItem2.Text = "Show the Edit popup menu";
			superToolTip2.Items.Add(toolTipTitleItem3);
			superToolTip2.Items.Add(toolTipItem2);
			ribbonPageGroup2.SuperTip = superToolTip2;
			ribbonPageGroup2.Text = "Edit";
			ribbonPageGroup2.CaptionButtonClick += ribbonPageGroup2_CaptionButtonClick;
			// 
			// ribbonPageGroup3
			// 
			ribbonPageGroup3.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
			ribbonPageGroup3.ImageOptions.ImageIndex = 26;
			ribbonPageGroup3.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ribbonPageGroup3.ImageOptions.SvgImage");
			ribbonPageGroup3.ItemLinks.Add(bgFontStyle);
			ribbonPageGroup3.ItemLinks.Add(bgFont);
			ribbonPageGroup3.ItemLinks.Add(bgBullets);
			ribbonPageGroup3.ItemLinks.Add(bgAlign);
			ribbonPageGroup3.Name = "ribbonPageGroup3";
			ribbonPageGroup3.Text = "Format";
			// 
			// ribbonPageGroup4
			// 
			ribbonPageGroup4.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
			ribbonPageGroup4.ImageOptions.ImageIndex = 3;
			ribbonPageGroup4.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ribbonPageGroup4.ImageOptions.SvgImage");
			ribbonPageGroup4.ItemLinks.Add(sbiFind);
			ribbonPageGroup4.Name = "ribbonPageGroup4";
			ribbonPageGroup4.Text = "Find";
			// 
			// ribbonPageGroup8
			// 
			ribbonPageGroup8.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
			ribbonPageGroup8.ItemLinks.Add(iProtected);
			ribbonPageGroup8.Name = "ribbonPageGroup8";
			ribbonPageGroup8.Text = "Protected";
			// 
			// ribbonPageGroup9
			// 
			ribbonPageGroup9.Alignment = RibbonPageGroupAlignment.Far;
			ribbonPageGroup9.AllowTextClipping = false;
			ribbonPageGroup9.ImageOptions.ImageIndex = 22;
			ribbonPageGroup9.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ribbonPageGroup9.ImageOptions.SvgImage");
			ribbonPageGroup9.ItemLinks.Add(iExit);
			ribbonPageGroup9.Name = "ribbonPageGroup9";
			toolTipTitleItem4.Text = "Save File Dialog";
			toolTipItem3.Appearance.Image = (System.Drawing.Image)resources.GetObject("resource.Image6");
			toolTipItem3.Appearance.Options.UseImage = true;
			toolTipItem3.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("resource.Image7");
			toolTipItem3.Text = "Show the Save file dialog box";
			superToolTip3.Items.Add(toolTipTitleItem4);
			superToolTip3.Items.Add(toolTipItem3);
			ribbonPageGroup9.SuperTip = superToolTip3;
			ribbonPageGroup9.Text = "Exit";
			ribbonPageGroup9.CaptionButtonClick += ribbonPageGroup9_CaptionButtonClick;
			// 
			// ribbonPage3
			// 
			ribbonPage3.Groups.AddRange(new RibbonPageGroup[] { rpgFont, rpgFontColor, ribbonPageGroup5 });
			ribbonPage3.Name = "ribbonPage3";
			ribbonPage3.Text = "Design";
			// 
			// rpgFont
			// 
			rpgFont.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("rpgFont.ImageOptions.SvgImage");
			rpgFont.ItemLinks.Add(rgbiFont);
			rpgFont.Name = "rpgFont";
			toolTipTitleItem5.Text = "Font Dialog";
			toolTipItem4.Appearance.Image = (System.Drawing.Image)resources.GetObject("resource.Image8");
			toolTipItem4.Appearance.Options.UseImage = true;
			toolTipItem4.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("resource.Image9");
			toolTipItem4.Text = "Show the Font dialog box";
			superToolTip4.Items.Add(toolTipTitleItem5);
			superToolTip4.Items.Add(toolTipItem4);
			rpgFont.SuperTip = superToolTip4;
			rpgFont.Text = "Font";
			rpgFont.CaptionButtonClick += rpgFont_CaptionButtonClick;
			// 
			// rpgFontColor
			// 
			rpgFontColor.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("rpgFontColor.ImageOptions.SvgImage");
			rpgFontColor.ItemLinks.Add(rgbiFontColor);
			rpgFontColor.Name = "rpgFontColor";
			toolTipTitleItem6.Text = "Color Edit";
			toolTipItem5.Appearance.Image = (System.Drawing.Image)resources.GetObject("resource.Image10");
			toolTipItem5.Appearance.Options.UseImage = true;
			toolTipItem5.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("resource.Image11");
			toolTipItem5.Text = "Show the Color edit popup";
			superToolTip5.Items.Add(toolTipTitleItem6);
			superToolTip5.Items.Add(toolTipItem5);
			rpgFontColor.SuperTip = superToolTip5;
			rpgFontColor.Text = "Font Color";
			rpgFontColor.CaptionButtonClick += rpgFontColor_CaptionButtonClick;
			// 
			// ribbonPageGroup5
			// 
			ribbonPageGroup5.ItemLinks.Add(biPageColor);
			ribbonPageGroup5.ItemLinks.Add(biPageBorders);
			ribbonPageGroup5.Name = "ribbonPageGroup5";
			ribbonPageGroup5.Text = "Page";
			// 
			// rpThemes
			// 
			rpThemes.Groups.AddRange(new RibbonPageGroup[] { ribbonPageGroup10 });
			rpThemes.Name = "rpThemes";
			rpThemes.Text = "Themes";
			// 
			// ribbonPageGroup10
			// 
			ribbonPageGroup10.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
			ribbonPageGroup10.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ribbonPageGroup10.ImageOptions.SvgImage");
			ribbonPageGroup10.ItemLinks.Add(rgbiSkins);
			ribbonPageGroup10.ItemLinks.Add(skinPaletteRibbonGalleryBarItem1);
			ribbonPageGroup10.Name = "ribbonPageGroup10";
			ribbonPageGroup10.Text = "Skins";
			// 
			// rpHelp
			// 
			rpHelp.Groups.AddRange(new RibbonPageGroup[] { rpgHelp });
			rpHelp.Name = "rpHelp";
			rpHelp.Text = "Help";
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
			// repositoryItemComboBox1
			// 
			repositoryItemComboBox1.AutoHeight = false;
			repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
			repositoryItemComboBox1.Name = "repositoryItemComboBox1";
			repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
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
			repositoryItemColorPickEdit1.AutomaticColor = System.Drawing.Color.Black;
			repositoryItemColorPickEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
			repositoryItemColorPickEdit1.Name = "repositoryItemColorPickEdit1";
			// 
			// repositoryItemButtonEdit2
			// 
			repositoryItemButtonEdit2.AutoHeight = false;
			repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
			repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
			// 
			// ribbonStatusBar1
			// 
			ribbonStatusBar1.ItemLinks.Add(barStaticItemMonitorServer);
			ribbonStatusBar1.ItemLinks.Add(barStaticItemUser, true);
			ribbonStatusBar1.ItemLinks.Add(barButtonItemInfo);
			ribbonStatusBar1.ItemLinks.Add(siPosition, true);
			ribbonStatusBar1.ItemLinks.Add(siDocName);
			ribbonStatusBar1.Location = new System.Drawing.Point(0, 824);
			ribbonStatusBar1.Name = "ribbonStatusBar1";
			ribbonStatusBar1.Ribbon = ribbonControl;
			ribbonStatusBar1.Size = new System.Drawing.Size(1672, 24);
			// 
			// pcAppMenuFileLabels
			// 
			pcAppMenuFileLabels.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			pcAppMenuFileLabels.Dock = System.Windows.Forms.DockStyle.Fill;
			pcAppMenuFileLabels.Location = new System.Drawing.Point(0, 27);
			pcAppMenuFileLabels.Name = "pcAppMenuFileLabels";
			pcAppMenuFileLabels.Size = new System.Drawing.Size(496, 205);
			pcAppMenuFileLabels.TabIndex = 2;
			// 
			// labelControl1
			// 
			labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
			labelControl1.Appearance.Options.UseFont = true;
			labelControl1.AutoSizeMode = LabelAutoSizeMode.None;
			labelControl1.Dock = System.Windows.Forms.DockStyle.Top;
			labelControl1.LineLocation = LineLocation.Bottom;
			labelControl1.LineVisible = true;
			labelControl1.Location = new System.Drawing.Point(0, 0);
			labelControl1.Name = "labelControl1";
			labelControl1.Size = new System.Drawing.Size(496, 27);
			labelControl1.TabIndex = 0;
			labelControl1.Text = "Recent Documents:";
			// 
			// xtraTabbedMdiManager1
			// 
			xtraTabbedMdiManager1.FloatOnDoubleClick = DevExpress.Utils.DefaultBoolean.True;
			xtraTabbedMdiManager1.FloatOnDrag = DevExpress.Utils.DefaultBoolean.True;
			xtraTabbedMdiManager1.MdiParent = this;
			xtraTabbedMdiManager1.PageAdded += OnTabbedMdiManagerPageCollectionChanged;
			xtraTabbedMdiManager1.PageRemoved += OnTabbedMdiManagerPageCollectionChanged;
			xtraTabbedMdiManager1.FloatMDIChildActivated += xtraTabbedMdiManager1_FloatMDIChildActivated;
			xtraTabbedMdiManager1.FloatMDIChildDeactivated += xtraTabbedMdiManager1_FloatMDIChildDeactivated;
			// 
			// pmMain
			// 
			pmMain.ItemLinks.Add(iUndo);
			pmMain.ItemLinks.Add(iCopy, true);
			pmMain.ItemLinks.Add(iCut);
			pmMain.ItemLinks.Add(iPaste);
			pmMain.ItemLinks.Add(iClear);
			pmMain.ItemLinks.Add(iSelectAll);
			pmMain.ItemLinks.Add(iFont, true);
			pmMain.ItemLinks.Add(iBullets);
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
			imageCollection3.ImageSize = new System.Drawing.Size(15, 15);
			imageCollection3.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("imageCollection3.ImageStream");
			// 
			// backstageViewControl1
			// 
			backstageViewControl1.BackstageViewShowRibbonItems = BackstageViewShowRibbonItems.FormButtons | BackstageViewShowRibbonItems.Title;
			backstageViewControl1.Controls.Add(backstageViewClientControl2);
			backstageViewControl1.Controls.Add(backstageViewClientControl9);
			backstageViewControl1.Controls.Add(backstageViewClientControl8);
			backstageViewControl1.Controls.Add(backstageViewClientControl11);
			backstageViewControl1.Controls.Add(backstageViewClientControl10);
			backstageViewControl1.Images = imageCollection2;
			backstageViewControl1.Items.Add(backstageViewTabItem3);
			backstageViewControl1.Items.Add(bvItemSave);
			backstageViewControl1.Items.Add(backstageViewTabItem1);
			backstageViewControl1.Items.Add(printTabItem);
			backstageViewControl1.Items.Add(backstageViewTabItem4);
			backstageViewControl1.Items.Add(backstageViewTabItem6);
			backstageViewControl1.Items.Add(backstageViewItemSeparator1);
			backstageViewControl1.Items.Add(bvItemClose);
			backstageViewControl1.Items.Add(bvItemExit);
			backstageViewControl1.Location = new System.Drawing.Point(12, 210);
			backstageViewControl1.Name = "backstageViewControl1";
			backstageViewControl1.Office2013StyleOptions.RightPaneContentVerticalOffset = 70;
			backstageViewControl1.OwnerControl = ribbonControl;
			backstageViewControl1.SelectedTab = backstageViewTabItem6;
			backstageViewControl1.SelectedTabIndex = 5;
			backstageViewControl1.Size = new System.Drawing.Size(769, 437);
			backstageViewControl1.TabIndex = 9;
			backstageViewControl1.Text = "backstageViewControl1";
			// 
			// backstageViewClientControl2
			// 
			backstageViewClientControl2.Controls.Add(recentItemControl1);
			backstageViewClientControl2.Location = new System.Drawing.Point(132, 71);
			backstageViewClientControl2.Name = "backstageViewClientControl2";
			backstageViewClientControl2.Size = new System.Drawing.Size(636, 365);
			backstageViewClientControl2.TabIndex = 10;
			// 
			// recentItemControl1
			// 
			recentItemControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			recentItemControl1.ContentPanelMinWidth = 460;
			recentItemControl1.DefaultContentPanel = recentStackPanel12;
			recentItemControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			recentItemControl1.Location = new System.Drawing.Point(0, 0);
			recentItemControl1.MainPanel = recentStackPanel13;
			recentItemControl1.MainPanelMinWidth = 180;
			recentItemControl1.Name = "recentItemControl1";
			recentItemControl1.ShowTitle = false;
			recentItemControl1.Size = new System.Drawing.Size(636, 365);
			recentItemControl1.SplitterPosition = 180;
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
			recentLabelItem1.ImageOptions.ItemNormal.Image = Properties.Resources.XtraBars;
			recentLabelItem1.Name = "recentLabelItem1";
			// 
			// recentHyperlinkItem1
			// 
			recentHyperlinkItem1.AllowSelect = DevExpress.Utils.DefaultBoolean.False;
			recentHyperlinkItem1.Caption = "www.devexpress.com";
			recentHyperlinkItem1.Name = "recentHyperlinkItem1";
			// 
			// recentLabelItem2
			// 
			recentLabelItem2.AllowSelect = DevExpress.Utils.DefaultBoolean.False;
			recentLabelItem2.Caption = "Copyright (c) 2000-2013 DevExpress inc. ALL RIGHTS RESERVED.";
			recentLabelItem2.Name = "recentLabelItem2";
			// 
			// recentStackPanel13
			// 
			recentStackPanel13.Caption = "Support";
			recentStackPanel13.CaptionToContentIndent = 46;
			recentStackPanel13.Items.AddRange(new RecentItemBase[] { recentPinItem2, recentPinItem3, recentPinItem4 });
			recentStackPanel13.Name = "recentStackPanel13";
			recentStackPanel13.PanelPadding = new System.Windows.Forms.Padding(20, 20, 5, 20);
			// 
			// recentPinItem2
			// 
			recentPinItem2.Caption = "DevExpress Online Help";
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
			// backstageViewClientControl9
			// 
			backstageViewClientControl9.Controls.Add(recentOpen);
			backstageViewClientControl9.Location = new System.Drawing.Point(132, 70);
			backstageViewClientControl9.Name = "backstageViewClientControl9";
			backstageViewClientControl9.Size = new System.Drawing.Size(1828, 557);
			backstageViewClientControl9.TabIndex = 7;
			// 
			// recentOpen
			// 
			recentOpen.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			recentOpen.ContentPanelMinWidth = 100;
			recentOpen.DefaultContentPanel = recentStackPanel1;
			recentOpen.Dock = System.Windows.Forms.DockStyle.Fill;
			recentOpen.Location = new System.Drawing.Point(0, 0);
			recentOpen.MainPanel = recentStackPanel2;
			recentOpen.MainPanelMinWidth = 100;
			recentOpen.MinimumSize = new System.Drawing.Size(400, 200);
			recentOpen.Name = "recentOpen";
			recentOpen.SelectedTab = recentTabItem2;
			recentOpen.ShowTitle = false;
			recentOpen.Size = new System.Drawing.Size(1828, 557);
			recentOpen.TabIndex = 0;
			recentOpen.Title = "Open";
			recentOpen.ItemClick += recentControlOpen_ItemClick;
			// 
			// recentStackPanel1
			// 
			recentStackPanel1.Caption = "Computer";
			recentStackPanel1.CaptionToContentIndent = 46;
			recentStackPanel1.ImageOptions.SvgImage = Properties.Resources.Electronics_DesktopMac;
			recentStackPanel1.Name = "recentStackPanel1";
			// 
			// recentStackPanel2
			// 
			recentStackPanel2.Caption = "Open";
			recentStackPanel2.CaptionToContentIndent = 46;
			recentStackPanel2.Items.AddRange(new RecentItemBase[] { recentTabItem1, recentTabItem2 });
			recentStackPanel2.Name = "recentStackPanel2";
			recentStackPanel2.SelectedItem = recentTabItem2;
			// 
			// recentTabItem1
			// 
			recentTabItem1.Caption = "Recent Documents";
			recentTabItem1.ImageOptions.ItemNormal.SvgImage = Properties.Resources.Recent_Documents;
			recentTabItem1.Name = "recentTabItem1";
			recentTabItem1.TabPanel = recentStackPanel3;
			// 
			// recentStackPanel3
			// 
			recentStackPanel3.Caption = "Recent Documents";
			recentStackPanel3.CaptionToContentIndent = 46;
			recentStackPanel3.ImageOptions.SvgImage = Properties.Resources.Recent_Documents;
			recentStackPanel3.Name = "recentStackPanel3";
			// 
			// recentTabItem2
			// 
			recentTabItem2.Caption = "Computer";
			recentTabItem2.ImageOptions.ItemNormal.SvgImage = Properties.Resources.Electronics_DesktopMac;
			recentTabItem2.Name = "recentTabItem2";
			recentTabItem2.TabPanel = recentStackPanel4;
			// 
			// recentStackPanel4
			// 
			recentStackPanel4.Caption = "Computer";
			recentStackPanel4.CaptionToContentIndent = 46;
			recentStackPanel4.ImageOptions.SvgImage = Properties.Resources.Electronics_DesktopMac;
			recentStackPanel4.Name = "recentStackPanel4";
			// 
			// backstageViewClientControl8
			// 
			backstageViewClientControl8.Controls.Add(recentSaveAs);
			backstageViewClientControl8.Location = new System.Drawing.Point(132, 70);
			backstageViewClientControl8.Name = "backstageViewClientControl8";
			backstageViewClientControl8.Size = new System.Drawing.Size(1828, 557);
			backstageViewClientControl8.TabIndex = 6;
			// 
			// recentSaveAs
			// 
			recentSaveAs.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			recentSaveAs.ContentPanelMinWidth = 100;
			recentSaveAs.DefaultContentPanel = recentStackPanel5;
			recentSaveAs.Dock = System.Windows.Forms.DockStyle.Fill;
			recentSaveAs.Location = new System.Drawing.Point(0, 0);
			recentSaveAs.MainPanel = recentStackPanel6;
			recentSaveAs.MainPanelMinWidth = 100;
			recentSaveAs.MinimumSize = new System.Drawing.Size(400, 200);
			recentSaveAs.Name = "recentSaveAs";
			recentSaveAs.SelectedTab = recentTabItem3;
			recentSaveAs.ShowTitle = false;
			recentSaveAs.Size = new System.Drawing.Size(1828, 557);
			recentSaveAs.TabIndex = 0;
			recentSaveAs.Title = "Save As";
			recentSaveAs.ItemClick += recentControlSave_ItemClick;
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
			backstageViewClientControl11.Location = new System.Drawing.Point(132, 70);
			backstageViewClientControl11.Name = "backstageViewClientControl11";
			backstageViewClientControl11.Size = new System.Drawing.Size(1828, 557);
			backstageViewClientControl11.TabIndex = 9;
			// 
			// recentControlPrint
			// 
			recentControlPrint.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			recentControlPrint.ContentPanelMinWidth = 100;
			recentControlPrint.Controls.Add(recentPrintOptionsContainer);
			recentControlPrint.Controls.Add(recentPrintPreviewContainer);
			recentControlPrint.DefaultContentPanel = recentStackPanel8;
			recentControlPrint.Dock = System.Windows.Forms.DockStyle.Fill;
			recentControlPrint.Location = new System.Drawing.Point(0, 0);
			recentControlPrint.MainPanel = recentStackPanel9;
			recentControlPrint.MainPanelMinWidth = 100;
			recentControlPrint.Name = "recentControlPrint";
			recentControlPrint.ShowTitle = false;
			recentControlPrint.Size = new System.Drawing.Size(1828, 557);
			recentControlPrint.TabIndex = 0;
			recentControlPrint.Title = "Print";
			// 
			// recentPrintOptionsContainer
			// 
			recentPrintOptionsContainer.Appearance.BackColor = System.Drawing.SystemColors.Control;
			recentPrintOptionsContainer.Appearance.Options.UseBackColor = true;
			recentPrintOptionsContainer.Controls.Add(layoutControl1);
			recentPrintOptionsContainer.Name = "recentPrintOptionsContainer";
			recentPrintOptionsContainer.Size = new System.Drawing.Size(244, 377);
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
			layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			layoutControl1.Location = new System.Drawing.Point(0, 0);
			layoutControl1.Name = "layoutControl1";
			layoutControl1.Root = layoutControlGroup1;
			layoutControl1.Size = new System.Drawing.Size(244, 377);
			layoutControl1.TabIndex = 0;
			layoutControl1.Text = "layoutControl1";
			// 
			// ddbDuplex
			// 
			ddbDuplex.Appearance.Options.UseTextOptions = true;
			ddbDuplex.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			ddbDuplex.Location = new System.Drawing.Point(2, 186);
			ddbDuplex.Name = "ddbDuplex";
			ddbDuplex.Size = new System.Drawing.Size(260, 52);
			ddbDuplex.StyleController = layoutControl1;
			ddbDuplex.TabIndex = 17;
			ddbDuplex.Text = "Print OneSided";
			// 
			// ddbOrientation
			// 
			ddbOrientation.Appearance.Options.UseTextOptions = true;
			ddbOrientation.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			ddbOrientation.Location = new System.Drawing.Point(2, 242);
			ddbOrientation.Name = "ddbOrientation";
			ddbOrientation.Size = new System.Drawing.Size(260, 52);
			ddbOrientation.StyleController = layoutControl1;
			ddbOrientation.TabIndex = 13;
			ddbOrientation.Text = "Orientation";
			// 
			// ddbPaperSize
			// 
			ddbPaperSize.Appearance.Options.UseTextOptions = true;
			ddbPaperSize.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			ddbPaperSize.Location = new System.Drawing.Point(2, 298);
			ddbPaperSize.Name = "ddbPaperSize";
			ddbPaperSize.Size = new System.Drawing.Size(260, 52);
			ddbPaperSize.StyleController = layoutControl1;
			ddbPaperSize.TabIndex = 15;
			ddbPaperSize.Text = "Paper Size";
			// 
			// ddbMargins
			// 
			ddbMargins.Appearance.Options.UseTextOptions = true;
			ddbMargins.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			ddbMargins.Location = new System.Drawing.Point(2, 354);
			ddbMargins.Name = "ddbMargins";
			ddbMargins.Size = new System.Drawing.Size(260, 52);
			ddbMargins.StyleController = layoutControl1;
			ddbMargins.TabIndex = 14;
			ddbMargins.Text = "Margins";
			// 
			// ddbCollate
			// 
			ddbCollate.Appearance.Options.UseTextOptions = true;
			ddbCollate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			ddbCollate.Location = new System.Drawing.Point(2, 410);
			ddbCollate.Name = "ddbCollate";
			ddbCollate.Size = new System.Drawing.Size(260, 52);
			ddbCollate.StyleController = layoutControl1;
			ddbCollate.TabIndex = 16;
			ddbCollate.Text = "Collated";
			// 
			// ddbPrinter
			// 
			ddbPrinter.Appearance.Options.UseTextOptions = true;
			ddbPrinter.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			ddbPrinter.AutoWidthInLayoutControl = true;
			ddbPrinter.Location = new System.Drawing.Point(0, 104);
			ddbPrinter.Name = "ddbPrinter";
			ddbPrinter.Size = new System.Drawing.Size(264, 56);
			ddbPrinter.StyleController = layoutControl1;
			ddbPrinter.TabIndex = 11;
			ddbPrinter.Text = "Printer";
			// 
			// printButton
			// 
			printButton.ImageOptions.Location = ImageLocation.TopCenter;
			printButton.ImageOptions.SvgImage = Properties.Resources.Print;
			printButton.Location = new System.Drawing.Point(2, 2);
			printButton.Name = "printButton";
			printButton.Size = new System.Drawing.Size(80, 76);
			printButton.StyleController = layoutControl1;
			printButton.TabIndex = 5;
			printButton.Text = "Print";
			printButton.Click += printButton_Click;
			// 
			// copySpinEdit
			// 
			copySpinEdit.EditValue = new decimal(new int[] { 1, 0, 0, 0 });
			copySpinEdit.Location = new System.Drawing.Point(152, 2);
			copySpinEdit.Name = "copySpinEdit";
			copySpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
			copySpinEdit.Properties.IsFloatValue = false;
			copySpinEdit.Properties.Mask.EditMask = "N00";
			copySpinEdit.Properties.MaxValue = new decimal(new int[] { 30, 0, 0, 0 });
			copySpinEdit.Properties.MinValue = new decimal(new int[] { 1, 0, 0, 0 });
			copySpinEdit.Size = new System.Drawing.Size(92, 20);
			copySpinEdit.StyleController = layoutControl1;
			copySpinEdit.TabIndex = 6;
			// 
			// layoutControlGroup1
			// 
			layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			layoutControlGroup1.GroupBordersVisible = false;
			layoutControlGroup1.Items.AddRange(new BaseLayoutItem[] { layoutControlItem1, lciCopiesSpinEdit, layoutControlItem3, layoutControlItem2, layoutControlItem4, layoutControlItem5, layoutControlItem6, layoutControlItem7, layoutControlItem8, layoutControlItem9 });
			layoutControlGroup1.Name = "layoutControlGroup1";
			layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			layoutControlGroup1.Size = new System.Drawing.Size(264, 464);
			layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			layoutControlItem1.Control = printButton;
			layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			layoutControlItem1.MaxSize = new System.Drawing.Size(84, 80);
			layoutControlItem1.MinSize = new System.Drawing.Size(84, 80);
			layoutControlItem1.Name = "layoutControlItem1";
			layoutControlItem1.Size = new System.Drawing.Size(84, 80);
			layoutControlItem1.SizeConstraintsType = SizeConstraintsType.Custom;
			layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem1.TextVisible = false;
			// 
			// lciCopiesSpinEdit
			// 
			lciCopiesSpinEdit.Control = copySpinEdit;
			lciCopiesSpinEdit.CustomizationFormText = "Copies:";
			lciCopiesSpinEdit.Location = new System.Drawing.Point(84, 0);
			lciCopiesSpinEdit.MaxSize = new System.Drawing.Size(180, 24);
			lciCopiesSpinEdit.MinSize = new System.Drawing.Size(180, 24);
			lciCopiesSpinEdit.Name = "lciCopiesSpinEdit";
			lciCopiesSpinEdit.Padding = new DevExpress.XtraLayout.Utils.Padding(20, 20, 2, 2);
			lciCopiesSpinEdit.Size = new System.Drawing.Size(180, 80);
			lciCopiesSpinEdit.SizeConstraintsType = SizeConstraintsType.Custom;
			lciCopiesSpinEdit.Text = "Copies:";
			// 
			// layoutControlItem3
			// 
			layoutControlItem3.Location = new System.Drawing.Point(0, 80);
			layoutControlItem3.Name = "layoutControlItem3";
			layoutControlItem3.Size = new System.Drawing.Size(264, 24);
			layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem3.TextVisible = false;
			// 
			// layoutControlItem2
			// 
			layoutControlItem2.Control = ddbPrinter;
			layoutControlItem2.Location = new System.Drawing.Point(0, 104);
			layoutControlItem2.MaxSize = new System.Drawing.Size(0, 56);
			layoutControlItem2.MinSize = new System.Drawing.Size(100, 56);
			layoutControlItem2.Name = "layoutControlItem2";
			layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			layoutControlItem2.Size = new System.Drawing.Size(264, 56);
			layoutControlItem2.SizeConstraintsType = SizeConstraintsType.Custom;
			layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem2.TextVisible = false;
			// 
			// layoutControlItem4
			// 
			layoutControlItem4.Location = new System.Drawing.Point(0, 160);
			layoutControlItem4.Name = "layoutControlItem4";
			layoutControlItem4.Size = new System.Drawing.Size(264, 24);
			layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem4.TextVisible = false;
			// 
			// layoutControlItem5
			// 
			layoutControlItem5.Control = ddbDuplex;
			layoutControlItem5.Location = new System.Drawing.Point(0, 184);
			layoutControlItem5.MaxSize = new System.Drawing.Size(0, 56);
			layoutControlItem5.MinSize = new System.Drawing.Size(100, 56);
			layoutControlItem5.Name = "layoutControlItem5";
			layoutControlItem5.Size = new System.Drawing.Size(264, 56);
			layoutControlItem5.SizeConstraintsType = SizeConstraintsType.Custom;
			layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem5.TextVisible = false;
			// 
			// layoutControlItem6
			// 
			layoutControlItem6.Control = ddbOrientation;
			layoutControlItem6.Location = new System.Drawing.Point(0, 240);
			layoutControlItem6.MaxSize = new System.Drawing.Size(0, 56);
			layoutControlItem6.MinSize = new System.Drawing.Size(83, 56);
			layoutControlItem6.Name = "layoutControlItem6";
			layoutControlItem6.Size = new System.Drawing.Size(264, 56);
			layoutControlItem6.SizeConstraintsType = SizeConstraintsType.Custom;
			layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem6.TextVisible = false;
			// 
			// layoutControlItem7
			// 
			layoutControlItem7.Control = ddbPaperSize;
			layoutControlItem7.Location = new System.Drawing.Point(0, 296);
			layoutControlItem7.MaxSize = new System.Drawing.Size(0, 56);
			layoutControlItem7.MinSize = new System.Drawing.Size(79, 56);
			layoutControlItem7.Name = "layoutControlItem7";
			layoutControlItem7.Size = new System.Drawing.Size(264, 56);
			layoutControlItem7.SizeConstraintsType = SizeConstraintsType.Custom;
			layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem7.TextVisible = false;
			// 
			// layoutControlItem8
			// 
			layoutControlItem8.Control = ddbMargins;
			layoutControlItem8.Location = new System.Drawing.Point(0, 352);
			layoutControlItem8.MaxSize = new System.Drawing.Size(0, 56);
			layoutControlItem8.MinSize = new System.Drawing.Size(66, 56);
			layoutControlItem8.Name = "layoutControlItem8";
			layoutControlItem8.Size = new System.Drawing.Size(264, 56);
			layoutControlItem8.SizeConstraintsType = SizeConstraintsType.Custom;
			layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem8.TextVisible = false;
			// 
			// layoutControlItem9
			// 
			layoutControlItem9.Control = ddbCollate;
			layoutControlItem9.Location = new System.Drawing.Point(0, 408);
			layoutControlItem9.MaxSize = new System.Drawing.Size(0, 56);
			layoutControlItem9.MinSize = new System.Drawing.Size(68, 56);
			layoutControlItem9.Name = "layoutControlItem9";
			layoutControlItem9.Size = new System.Drawing.Size(264, 56);
			layoutControlItem9.SizeConstraintsType = SizeConstraintsType.Custom;
			layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem9.TextVisible = false;
			// 
			// recentPrintPreviewContainer
			// 
			recentPrintPreviewContainer.Appearance.BackColor = System.Drawing.SystemColors.Control;
			recentPrintPreviewContainer.Appearance.Options.UseBackColor = true;
			recentPrintPreviewContainer.Controls.Add(panelControl2);
			recentPrintPreviewContainer.Name = "recentPrintPreviewContainer";
			recentPrintPreviewContainer.Size = new System.Drawing.Size(285, 504);
			recentPrintPreviewContainer.TabIndex = 2;
			// 
			// panelControl2
			// 
			panelControl2.Controls.Add(printControl2);
			panelControl2.Controls.Add(panelControl3);
			panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			panelControl2.Location = new System.Drawing.Point(0, 0);
			panelControl2.Name = "panelControl2";
			panelControl2.Size = new System.Drawing.Size(285, 504);
			panelControl2.TabIndex = 0;
			// 
			// printControl2
			// 
			printControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			printControl2.IsMetric = true;
			printControl2.Location = new System.Drawing.Point(2, 2);
			printControl2.Name = "printControl2";
			printControl2.Size = new System.Drawing.Size(281, 460);
			printControl2.TabIndex = 3;
			printControl2.SelectedPageChanged += printControl2_SelectedPageChanged;
			// 
			// panelControl3
			// 
			panelControl3.Controls.Add(stackPanel1);
			panelControl3.Controls.Add(panel2);
			panelControl3.Controls.Add(pageButtonEdit);
			panelControl3.Controls.Add(zoomTrackBarControl1);
			panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelControl3.Location = new System.Drawing.Point(2, 462);
			panelControl3.Name = "panelControl3";
			panelControl3.Size = new System.Drawing.Size(281, 40);
			panelControl3.TabIndex = 2;
			// 
			// stackPanel1
			// 
			stackPanel1.Controls.Add(zoomTextEdit);
			stackPanel1.Dock = System.Windows.Forms.DockStyle.Right;
			stackPanel1.LayoutDirection = DevExpress.Utils.Layout.StackPanelLayoutDirection.RightToLeft;
			stackPanel1.Location = new System.Drawing.Point(-419, 2);
			stackPanel1.Name = "stackPanel1";
			stackPanel1.Size = new System.Drawing.Size(340, 36);
			stackPanel1.TabIndex = 7;
			// 
			// zoomTextEdit
			// 
			zoomTextEdit.Dock = System.Windows.Forms.DockStyle.Right;
			zoomTextEdit.EditValue = (short)100;
			zoomTextEdit.Location = new System.Drawing.Point(220, 8);
			zoomTextEdit.Name = "zoomTextEdit";
			zoomTextEdit.Properties.DisplayFormat.FormatString = "{0}%";
			zoomTextEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			zoomTextEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			zoomTextEdit.Properties.Mask.EditMask = "n0";
			zoomTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
			zoomTextEdit.Size = new System.Drawing.Size(117, 20);
			zoomTextEdit.TabIndex = 6;
			zoomTextEdit.EditValueChanged += zoomTextEdit_EditValueChanged;
			// 
			// panel2
			// 
			panel2.Dock = System.Windows.Forms.DockStyle.Right;
			panel2.Location = new System.Drawing.Point(-79, 2);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(16, 36);
			panel2.TabIndex = 5;
			// 
			// pageButtonEdit
			// 
			pageButtonEdit.Dock = System.Windows.Forms.DockStyle.Left;
			pageButtonEdit.EditValue = "1";
			pageButtonEdit.Location = new System.Drawing.Point(2, 2);
			pageButtonEdit.Name = "pageButtonEdit";
			pageButtonEdit.Properties.Appearance.Options.UseTextOptions = true;
			pageButtonEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			pageButtonEdit.Properties.AutoHeight = false;
			pageButtonEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			pageButtonEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Left, "", -1, true, true, true, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default), new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Right) });
			pageButtonEdit.Properties.DisplayFormat.FormatString = "Page {0} of 1";
			pageButtonEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			pageButtonEdit.Size = new System.Drawing.Size(188, 36);
			pageButtonEdit.TabIndex = 4;
			pageButtonEdit.ButtonClick += pageButtonEdit_ButtonClick;
			pageButtonEdit.EditValueChanged += pageButtonEdit_EditValueChanged;
			pageButtonEdit.EditValueChanging += pageButtonEdit_EditValueChanging;
			// 
			// zoomTrackBarControl1
			// 
			zoomTrackBarControl1.Dock = System.Windows.Forms.DockStyle.Right;
			zoomTrackBarControl1.EditValue = 40;
			zoomTrackBarControl1.Location = new System.Drawing.Point(-63, 2);
			zoomTrackBarControl1.MenuManager = ribbonControl;
			zoomTrackBarControl1.Name = "zoomTrackBarControl1";
			zoomTrackBarControl1.Properties.Maximum = 80;
			zoomTrackBarControl1.Properties.Middle = 40;
			zoomTrackBarControl1.Properties.SmallChange = 2;
			zoomTrackBarControl1.Size = new System.Drawing.Size(342, 36);
			zoomTrackBarControl1.TabIndex = 0;
			zoomTrackBarControl1.Value = 40;
			zoomTrackBarControl1.EditValueChanged += zoomTrackBarControl1_EditValueChanged;
			// 
			// recentStackPanel8
			// 
			recentStackPanel8.Items.AddRange(new RecentItemBase[] { recentPrintPreview });
			recentStackPanel8.Name = "recentStackPanel8";
			recentStackPanel8.PanelPadding = new System.Windows.Forms.Padding(1);
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
			backstageViewClientControl10.Location = new System.Drawing.Point(132, 70);
			backstageViewClientControl10.Name = "backstageViewClientControl10";
			backstageViewClientControl10.Size = new System.Drawing.Size(1828, 557);
			backstageViewClientControl10.TabIndex = 8;
			// 
			// recentControlExport
			// 
			recentControlExport.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			recentControlExport.ContentPanelMinWidth = 100;
			recentControlExport.DefaultContentPanel = recentStackPanel14;
			recentControlExport.Dock = System.Windows.Forms.DockStyle.Fill;
			recentControlExport.Location = new System.Drawing.Point(0, 0);
			recentControlExport.MainPanel = recentStackPanel10;
			recentControlExport.MainPanelMinWidth = 100;
			recentControlExport.MinimumSize = new System.Drawing.Size(400, 100);
			recentControlExport.Name = "recentControlExport";
			recentControlExport.SelectedTab = recentTabItem4;
			recentControlExport.ShowTitle = false;
			recentControlExport.Size = new System.Drawing.Size(1828, 557);
			recentControlExport.TabIndex = 0;
			recentControlExport.Title = "Title";
			recentControlExport.ItemClick += recentControlExport_ItemClick;
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
			// backstageViewTabItem3
			// 
			backstageViewTabItem3.Caption = "Open";
			backstageViewTabItem3.CaptionHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			backstageViewTabItem3.ContentControl = backstageViewClientControl9;
			backstageViewTabItem3.GlyphHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			backstageViewTabItem3.GlyphLocation = DevExpress.Utils.Drawing.ItemLocation.Top;
			backstageViewTabItem3.Name = "backstageViewTabItem3";
			// 
			// bvItemSave
			// 
			bvItemSave.Caption = "Save";
			bvItemSave.CaptionHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			bvItemSave.GlyphHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			bvItemSave.GlyphLocation = DevExpress.Utils.Drawing.ItemLocation.Top;
			bvItemSave.Name = "bvItemSave";
			bvItemSave.ItemClick += bvItemSave_ItemClick;
			// 
			// backstageViewTabItem1
			// 
			backstageViewTabItem1.Caption = "Save As";
			backstageViewTabItem1.CaptionHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			backstageViewTabItem1.ContentControl = backstageViewClientControl8;
			backstageViewTabItem1.GlyphHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			backstageViewTabItem1.GlyphLocation = DevExpress.Utils.Drawing.ItemLocation.Top;
			backstageViewTabItem1.Name = "backstageViewTabItem1";
			// 
			// printTabItem
			// 
			printTabItem.Caption = "Print";
			printTabItem.CaptionHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			printTabItem.ContentControl = backstageViewClientControl11;
			printTabItem.GlyphHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			printTabItem.GlyphLocation = DevExpress.Utils.Drawing.ItemLocation.Top;
			printTabItem.ImageOptions.ItemNormal.SvgImage = Properties.Resources.Print;
			printTabItem.Name = "printTabItem";
			printTabItem.SelectedChanged += onTabPrint_SelectedChanged;
			// 
			// backstageViewTabItem4
			// 
			backstageViewTabItem4.Caption = "Export";
			backstageViewTabItem4.CaptionHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			backstageViewTabItem4.ContentControl = backstageViewClientControl10;
			backstageViewTabItem4.GlyphHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			backstageViewTabItem4.GlyphLocation = DevExpress.Utils.Drawing.ItemLocation.Top;
			backstageViewTabItem4.ImageOptions.ItemNormal.SvgImage = Properties.Resources.ExportTo;
			backstageViewTabItem4.ImageOptions.ItemNormal.SvgImageSize = new System.Drawing.Size(32, 32);
			backstageViewTabItem4.Name = "backstageViewTabItem4";
			// 
			// backstageViewTabItem6
			// 
			backstageViewTabItem6.Caption = "Help";
			backstageViewTabItem6.CaptionHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			backstageViewTabItem6.ContentControl = backstageViewClientControl2;
			backstageViewTabItem6.GlyphHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			backstageViewTabItem6.GlyphLocation = DevExpress.Utils.Drawing.ItemLocation.Top;
			backstageViewTabItem6.Name = "backstageViewTabItem6";
			backstageViewTabItem6.Selected = true;
			// 
			// backstageViewItemSeparator1
			// 
			backstageViewItemSeparator1.Alignment = BackstageViewItemAlignment.Bottom;
			backstageViewItemSeparator1.Name = "backstageViewItemSeparator1";
			// 
			// bvItemClose
			// 
			bvItemClose.Alignment = BackstageViewItemAlignment.Bottom;
			bvItemClose.Caption = "Close";
			bvItemClose.Name = "bvItemClose";
			bvItemClose.ItemClick += bvItemClose_ItemClick;
			// 
			// bvItemExit
			// 
			bvItemExit.Alignment = BackstageViewItemAlignment.Bottom;
			bvItemExit.Caption = "Exit";
			bvItemExit.Name = "bvItemExit";
			bvItemExit.ItemClick += bvItemExit_ItemClick;
			// 
			// printControl1
			// 
			printControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			printControl1.IsMetric = true;
			printControl1.Location = new System.Drawing.Point(0, 0);
			printControl1.Name = "printControl1";
			printControl1.Size = new System.Drawing.Size(639, 539);
			printControl1.TabIndex = 3;
			// 
			// recentControlRecentItem10
			// 
			recentControlRecentItem10.Caption = "Image File";
			recentControlRecentItem10.Description = "BMP, GIF, JPEG, PNG, TIFF, EMF, WMF";
			recentControlRecentItem10.GlyphAlignment = RecentPinItemGlyphAlignment.Center;
			recentControlRecentItem10.ImageOptions.ItemNormal.Image = (System.Drawing.Image)resources.GetObject("recentControlRecentItem10.ImageOptions.ItemNormal.Image");
			recentControlRecentItem10.Name = "recentControlRecentItem10";
			recentControlRecentItem10.PinButtonVisibility = RecentPinButtonVisibility.Never;
			// 
			// recentControlButtonItem1
			// 
			recentControlButtonItem1.AutoSize = false;
			recentControlButtonItem1.Caption = "Save As";
			recentControlButtonItem1.Name = "recentControlButtonItem1";
			recentControlButtonItem1.Orientation = System.Windows.Forms.Orientation.Vertical;
			recentControlButtonItem1.Size = new System.Drawing.Size(82, 86);
			// 
			// backstageViewClientControl7
			// 
			backstageViewClientControl7.Location = new System.Drawing.Point(0, 0);
			backstageViewClientControl7.Name = "backstageViewClientControl7";
			backstageViewClientControl7.Size = new System.Drawing.Size(150, 150);
			backstageViewClientControl7.TabIndex = 6;
			// 
			// backstageViewClientControl1
			// 
			backstageViewClientControl1.Location = new System.Drawing.Point(0, 0);
			backstageViewClientControl1.Name = "backstageViewClientControl1";
			backstageViewClientControl1.Size = new System.Drawing.Size(150, 150);
			backstageViewClientControl1.TabIndex = 0;
			// 
			// backstageViewClientControl3
			// 
			backstageViewClientControl3.Location = new System.Drawing.Point(0, 0);
			backstageViewClientControl3.Name = "backstageViewClientControl3";
			backstageViewClientControl3.Size = new System.Drawing.Size(150, 150);
			backstageViewClientControl3.TabIndex = 2;
			// 
			// taskbarAssistant1
			// 
			taskbarAssistant1.ParentControl = this;
			taskbarAssistant1.ThumbnailButtons.Add(thumbButtonNewDoc);
			taskbarAssistant1.ThumbnailButtons.Add(thumbButtonPrev);
			taskbarAssistant1.ThumbnailButtons.Add(thumbButtonNext);
			taskbarAssistant1.ThumbnailButtons.Add(thumbButtonExit);
			// 
			// thumbButtonNewDoc
			// 
			thumbButtonNewDoc.Image = (System.Drawing.Bitmap)resources.GetObject("thumbButtonNewDoc.Image");
			thumbButtonNewDoc.Tooltip = "Create New Document";
			thumbButtonNewDoc.Click += OnNewDocThumbButtonClick;
			// 
			// thumbButtonPrev
			// 
			thumbButtonPrev.Image = (System.Drawing.Bitmap)resources.GetObject("thumbButtonPrev.Image");
			thumbButtonPrev.Tooltip = "Previous Document";
			thumbButtonPrev.Click += OnPrevThumbButtonClick;
			// 
			// thumbButtonNext
			// 
			thumbButtonNext.Image = (System.Drawing.Bitmap)resources.GetObject("thumbButtonNext.Image");
			thumbButtonNext.Tooltip = "Next Document";
			thumbButtonNext.Click += OnNextDocThumbButtonClick;
			// 
			// thumbButtonExit
			// 
			thumbButtonExit.Image = (System.Drawing.Bitmap)resources.GetObject("thumbButtonExit.Image");
			thumbButtonExit.Tooltip = "Exit";
			thumbButtonExit.Click += OnExitThumbButtonClick;
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
			backstageViewClientControl4.Location = new System.Drawing.Point(133, 71);
			backstageViewClientControl4.Name = "backstageViewClientControl4";
			backstageViewClientControl4.Size = new System.Drawing.Size(639, 539);
			backstageViewClientControl4.TabIndex = 3;
			// 
			// emptySpacePanel
			// 
			emptySpacePanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			emptySpacePanel.Dock = System.Windows.Forms.DockStyle.Top;
			emptySpacePanel.Location = new System.Drawing.Point(0, 158);
			emptySpacePanel.Name = "emptySpacePanel";
			emptySpacePanel.Size = new System.Drawing.Size(1672, 23);
			emptySpacePanel.TabIndex = 12;
			// 
			// tabControl
			// 
			tabControl.Location = new System.Drawing.Point(798, 234);
			tabControl.Name = "tabControl";
			tabControl.SelectedTabPage = tabPageLog;
			tabControl.Size = new System.Drawing.Size(757, 346);
			tabControl.TabIndex = 16;
			tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { tabPageLog, tabPageUserSessions, tabSocketPAckageCommunication, tabPageTransactions, tabPageErrors, tabPageStatistics });
			// 
			// tabPageLog
			// 
			tabPageLog.Name = "tabPageLog";
			tabPageLog.Size = new System.Drawing.Size(755, 321);
			tabPageLog.Text = "Log";
			// 
			// tabPageUserSessions
			// 
			tabPageUserSessions.Controls.Add(gridControlUserSessions);
			tabPageUserSessions.Name = "tabPageUserSessions";
			tabPageUserSessions.Size = new System.Drawing.Size(755, 321);
			tabPageUserSessions.Text = "User Sessions";
			// 
			// gridControlUserSessions
			// 
			gridControlUserSessions.Dock = System.Windows.Forms.DockStyle.Fill;
			gridControlUserSessions.Location = new System.Drawing.Point(0, 0);
			gridControlUserSessions.MainView = gridViewUserSessions;
			gridControlUserSessions.Name = "gridControlUserSessions";
			gridControlUserSessions.Size = new System.Drawing.Size(755, 321);
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
			tabSocketPAckageCommunication.Size = new System.Drawing.Size(755, 321);
			tabSocketPAckageCommunication.Text = "Socket Package Communication";
			// 
			// splitContainerControlSocketPackageCommunication
			// 
			splitContainerControlSocketPackageCommunication.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainerControlSocketPackageCommunication.Location = new System.Drawing.Point(0, 0);
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
			splitContainerControlSocketPackageCommunication.Size = new System.Drawing.Size(755, 321);
			splitContainerControlSocketPackageCommunication.SplitterPosition = 818;
			splitContainerControlSocketPackageCommunication.TabIndex = 4;
			// 
			// gridControlRequestResponseMessages
			// 
			gridControlRequestResponseMessages.Dock = System.Windows.Forms.DockStyle.Fill;
			gridControlRequestResponseMessages.Location = new System.Drawing.Point(0, 0);
			gridControlRequestResponseMessages.MainView = gridViewRequestResponseMessagess;
			gridControlRequestResponseMessages.Name = "gridControlRequestResponseMessages";
			gridControlRequestResponseMessages.Size = new System.Drawing.Size(745, 321);
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
			groupPropertyPanelRequestResponseMessages.Dock = System.Windows.Forms.DockStyle.Fill;
			groupPropertyPanelRequestResponseMessages.Location = new System.Drawing.Point(0, 0);
			groupPropertyPanelRequestResponseMessages.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			groupPropertyPanelRequestResponseMessages.Name = "groupPropertyPanelRequestResponseMessages";
			groupPropertyPanelRequestResponseMessages.Size = new System.Drawing.Size(0, 0);
			groupPropertyPanelRequestResponseMessages.TabIndex = 0;
			// 
			// tabPageTransactions
			// 
			tabPageTransactions.Controls.Add(splitContainerControlTransactions);
			tabPageTransactions.Name = "tabPageTransactions";
			tabPageTransactions.Size = new System.Drawing.Size(755, 321);
			tabPageTransactions.Text = "Transactions";
			// 
			// splitContainerControlTransactions
			// 
			splitContainerControlTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainerControlTransactions.Location = new System.Drawing.Point(0, 0);
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
			splitContainerControlTransactions.Size = new System.Drawing.Size(755, 321);
			splitContainerControlTransactions.SplitterPosition = 814;
			splitContainerControlTransactions.TabIndex = 0;
			// 
			// gridControlTransactions
			// 
			gridControlTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
			gridControlTransactions.Location = new System.Drawing.Point(0, 0);
			gridControlTransactions.MainView = gridViewTransactions;
			gridControlTransactions.Name = "gridControlTransactions";
			gridControlTransactions.Size = new System.Drawing.Size(745, 321);
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
			groupPropertyPanelTransactionLog.Dock = System.Windows.Forms.DockStyle.Fill;
			groupPropertyPanelTransactionLog.Location = new System.Drawing.Point(0, 0);
			groupPropertyPanelTransactionLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			groupPropertyPanelTransactionLog.Name = "groupPropertyPanelTransactionLog";
			groupPropertyPanelTransactionLog.Size = new System.Drawing.Size(0, 0);
			groupPropertyPanelTransactionLog.TabIndex = 0;
			// 
			// tabPageErrors
			// 
			tabPageErrors.Name = "tabPageErrors";
			tabPageErrors.Size = new System.Drawing.Size(755, 321);
			tabPageErrors.Text = "Errors";
			// 
			// tabPageStatistics
			// 
			tabPageStatistics.Name = "tabPageStatistics";
			tabPageStatistics.Size = new System.Drawing.Size(755, 321);
			tabPageStatistics.Text = "Statistics";
			// 
			// ribbonGalleryBarItem1
			// 
			ribbonGalleryBarItem1.Caption = "Skins";
			// 
			// 
			// 
			ribbonGalleryBarItem1.Gallery.Appearance.ItemCaptionAppearance.Hovered.Options.UseFont = true;
			ribbonGalleryBarItem1.Gallery.Appearance.ItemCaptionAppearance.Hovered.Options.UseTextOptions = true;
			ribbonGalleryBarItem1.Gallery.Appearance.ItemCaptionAppearance.Hovered.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			ribbonGalleryBarItem1.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseFont = true;
			ribbonGalleryBarItem1.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseTextOptions = true;
			ribbonGalleryBarItem1.Gallery.Appearance.ItemCaptionAppearance.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			ribbonGalleryBarItem1.Gallery.Appearance.ItemCaptionAppearance.Pressed.Options.UseFont = true;
			ribbonGalleryBarItem1.Gallery.Appearance.ItemCaptionAppearance.Pressed.Options.UseTextOptions = true;
			ribbonGalleryBarItem1.Gallery.Appearance.ItemCaptionAppearance.Pressed.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			ribbonGalleryBarItem1.Id = 13;
			ribbonGalleryBarItem1.Name = "ribbonGalleryBarItem1";
			// 
			// frmMain
			// 
			AllowFormGlass = DevExpress.Utils.DefaultBoolean.False;
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(1672, 848);
			Controls.Add(tabControl);
			Controls.Add(emptySpacePanel);
			Controls.Add(backstageViewControl1);
			Controls.Add(pccBottom);
			Controls.Add(ribbonStatusBar1);
			Controls.Add(ribbonControl);
			IsMdiContainer = true;
			MinimumSize = new System.Drawing.Size(700, 380);
			Name = "frmMain";
			Ribbon = ribbonControl;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			StatusBar = ribbonStatusBar1;
			Text = "SimplePad (C# Demo)";
			Activated += frmMain_Activated;
			Closing += frmMain_Closing;
			FormClosing += frmMain_FormClosing;
			Load += frmMain_Load;
			MdiChildActivate += frmMain_MdiChildActivate;
			((System.ComponentModel.ISupportInitialize)gddFont).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemSpinEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)popupControlContainer1).EndInit();
			((System.ComponentModel.ISupportInitialize)ribbonControl).EndInit();
			((System.ComponentModel.ISupportInitialize)pmAppMain).EndInit();
			((System.ComponentModel.ISupportInitialize)pccBottom).EndInit();
			pccBottom.ResumeLayout(false);
			pccBottom.PerformLayout();
			((System.ComponentModel.ISupportInitialize)imageCollection2).EndInit();
			((System.ComponentModel.ISupportInitialize)pmNew).EndInit();
			((System.ComponentModel.ISupportInitialize)gddFontColor).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemPictureEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)riicStyle).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)imageCollection1).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemTrackBar1).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemColorPickEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit2).EndInit();
			((System.ComponentModel.ISupportInitialize)pcAppMenuFileLabels).EndInit();
			((System.ComponentModel.ISupportInitialize)xtraTabbedMdiManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)pmMain).EndInit();
			((System.ComponentModel.ISupportInitialize)imageCollection3).EndInit();
			((System.ComponentModel.ISupportInitialize)backstageViewControl1).EndInit();
			backstageViewControl1.ResumeLayout(false);
			backstageViewClientControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)recentItemControl1).EndInit();
			backstageViewClientControl9.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)recentOpen).EndInit();
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
			backstageViewClientControl4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)emptySpacePanel).EndInit();
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
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private DevExpress.XtraBars.BarButtonItem iClose;
        private DevExpress.XtraBars.BarButtonItem iSave;
        private DevExpress.XtraBars.BarButtonItem iOpen;
        private DevExpress.XtraBars.BarButtonItem iSaveAs;
        private DevExpress.XtraBars.BarButtonItem idNew;
        private DevExpress.XtraBars.BarButtonItem iExit;
        private DevExpress.XtraBars.BarButtonItem iPrint;
        private DevExpress.XtraBars.BarButtonItem iClear;
        private DevExpress.XtraBars.BarButtonItem iPaste;
        private DevExpress.XtraBars.BarButtonItem iFind;
        private DevExpress.XtraBars.BarButtonItem iCut;
        private DevExpress.XtraBars.BarButtonItem iCopy;
        private DevExpress.XtraBars.BarButtonItem iUndo;
        private DevExpress.XtraBars.BarButtonItem iReplace;
        private DevExpress.XtraBars.BarButtonItem iSelectAll;
        private DevExpress.XtraBars.BarButtonItem iBold;
        private DevExpress.XtraBars.BarButtonItem iAlignRight;
        private DevExpress.XtraBars.BarButtonItem iCenter;
        private DevExpress.XtraBars.BarButtonItem iUnderline;
        private DevExpress.XtraBars.BarButtonItem iAlignLeft;
        private DevExpress.XtraBars.BarButtonItem iItalic;
        private DevExpress.XtraBars.BarButtonItem iFont;
        private DevExpress.XtraBars.BarButtonItem iBullets;
        private DevExpress.XtraBars.BarCheckItem iProtected;
        private DevExpress.XtraBars.BarButtonItem iFontColor;
        private DevExpress.XtraBars.BarButtonItem iWeb;
        private DevExpress.XtraBars.BarButtonItem siPosition;
        private DevExpress.XtraBars.BarButtonItem barButtonItemInfo;
        private DevExpress.XtraBars.BarStaticItem siDocName;
        private DevExpress.XtraBars.PopupControlContainer popupControlContainer1;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup8;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup9;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.BarButtonGroup bgAlign;
        private DevExpress.XtraBars.BarButtonGroup bgFontStyle;
        private DevExpress.XtraBars.BarButtonGroup bgFont;
        private DevExpress.XtraBars.BarButtonGroup bgBullets;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        private DevExpress.XtraBars.BarSubItem sbiSave;
        private DevExpress.XtraBars.BarSubItem sbiPaste;
        private DevExpress.XtraBars.BarSubItem sbiFind;
        private DevExpress.XtraBars.BarButtonItem iPasteSpecial;
        private DevExpress.XtraBars.BarButtonItem iNew;
        private DevExpress.XtraBars.BarLargeButtonItem iLargeUndo;
        private DevExpress.XtraBars.BarButtonItem iTemplate;
        private DevExpress.XtraBars.PopupMenu pmNew;
        private DevExpress.XtraBars.PopupMenu pmMain;
        private DevExpress.XtraBars.BarButtonItem iPaintStyle;
        private DevExpress.XtraBars.RibbonGalleryBarItem rgbiSkins;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup10;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu pmAppMain;
        private DevExpress.XtraBars.Ribbon.GalleryDropDown gddFont;
        private DevExpress.XtraBars.BarEditItem beiFontSize;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage3;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgFont;
        private DevExpress.XtraBars.RibbonGalleryBarItem rgbiFont;
        private DevExpress.XtraBars.Ribbon.GalleryDropDown gddFontColor;
        private DevExpress.XtraBars.BarButtonItem bbiFontColorPopup;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgFontColor;
        private DevExpress.XtraBars.RibbonGalleryBarItem rgbiFontColor;
        private DevExpress.XtraBars.BarButtonItem iAbout;
        private DevExpress.Utils.ImageCollection imageCollection2;
        private DevExpress.XtraBars.Ribbon.RibbonPageCategory ribbonPageCategory1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage4;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup12;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup13;
        private Office2007PopupControlContainer pccAppMenu;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl pcAppMenuFileLabels;
        private DevExpress.Utils.ImageCollection imageCollection3;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private BarEditItem biStyle;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riicStyle;
        private PopupControlContainer pccBottom;
        private DevExpress.XtraEditors.SimpleButton sbExit;
        private DevExpress.XtraBars.Ribbon.RibbonMiniToolbar selectionMiniToolbar;
        private BarButtonGroup editButtonGroup;
        private DevExpress.XtraBars.Ribbon.BackstageViewControl backstageViewControl1;
        private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl1;
        private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl3;
        private DevExpress.XtraBars.Ribbon.BackstageViewButtonItem bvItemSave;
        private DevExpress.XtraBars.Ribbon.BackstageViewButtonItem bvItemClose;
        private DevExpress.XtraBars.Ribbon.BackstageViewButtonItem bvItemExit;
        private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl7;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.Utils.Taskbar.TaskbarAssistant taskbarAssistant1;
        private DevExpress.Utils.Taskbar.ThumbnailButton thumbButtonNewDoc;
        private DevExpress.Utils.Taskbar.ThumbnailButton thumbButtonNext;
        private DevExpress.Utils.Taskbar.ThumbnailButton thumbButtonExit;
        private DevExpress.Utils.Taskbar.ThumbnailButton thumbButtonPrev;
        private BarToggleSwitchItem barToggleSwitchItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTrackBar repositoryItemTrackBar1;
        private BarButtonItem bbColorMix;
        private System.ComponentModel.IContainer components;
        private BackstageViewClientControl backstageViewClientControl8;
        private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItem1;
        private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItem2;
        private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl9;
        private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItem3;
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
        private LayoutControl layoutControl1;
        private LayoutControlGroup layoutControlGroup1;
        private SimpleButton printButton;
        private LayoutControlItem layoutControlItem1;
        private SpinEdit copySpinEdit;
        private LayoutControlItem lciCopiesSpinEdit;
        private BackstageViewLabel printerLabel;
        private LayoutControlItem layoutControlItem3;
        private TextEdit zoomTextEdit;
        private DropDownButton ddbPrinter;
        private LayoutControlItem layoutControlItem2;
        private DevExpress.XtraBars.Ribbon.RecentItemControl recentOpen;
        private DevExpress.XtraBars.Ribbon.RecentTabItem recentTabItem1;
        private DevExpress.XtraBars.Ribbon.RecentTabItem recentTabItem2;
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
        private DocumentViewer printControl2;
        private RecentStackPanel recentStackPanel1;
        private RecentStackPanel recentStackPanel2;
        private RecentStackPanel recentStackPanel3;
        private RecentStackPanel recentStackPanel4;
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
        private RibbonPage rpThemes;
        private BarButtonItem biPageColor;
        private BarButtonItem biPageBorders;
        private RibbonPageGroup ribbonPageGroup5;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit repositoryItemColorPickEdit1;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        private DevExpress.XtraEditors.PanelControl emptySpacePanel;
        private BackstageViewItemSeparator backstageViewItemSeparator1;
        private SkinPaletteRibbonGalleryBarItem skinPaletteRibbonGalleryBarItem1;
		private DevExpress.XtraTab.XtraTabControl tabControl;
		private DevExpress.XtraTab.XtraTabPage tabPageLog;
		private DevExpress.XtraTab.XtraTabPage tabPageUserSessions;
		private DevExpress.XtraGrid.GridControl gridControlUserSessions;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewUserSessions;
		private DevExpress.XtraTab.XtraTabPage tabSocketPAckageCommunication;
		private SplitContainerControl splitContainerControlSocketPackageCommunication;
		private DevExpress.XtraGrid.GridControl gridControlRequestResponseMessages;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewRequestResponseMessagess;
		private Controls.GroupPropertyPanel groupPropertyPanelRequestResponseMessages;
		private DevExpress.XtraTab.XtraTabPage tabPageTransactions;
		private SplitContainerControl splitContainerControlTransactions;
		private DevExpress.XtraGrid.GridControl gridControlTransactions;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewTransactions;
		private Controls.GroupPropertyPanel groupPropertyPanelTransactionLog;
		private DevExpress.XtraTab.XtraTabPage tabPageErrors;
		private DevExpress.XtraTab.XtraTabPage tabPageStatistics;
		private RibbonPage ribbonPage2;
		private BarButtonItem barButtonItemConnect;
		private RibbonPageGroup ribbonPageGroupMonitorServer;
		private BarEditItem barEditItemMonitorServer;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
		private BarEditItem barEditItemServerMonitorPort;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit2;
		private BarButtonItem barButtonItemDisconnect;
		private RibbonPageGroup ribbonPageGroupSimpleObjectsServer;
		private BarButtonItem barButtonItemServerStart;
		private BarButtonItem barButtonItemServerRestart;
		private BarButtonItem barButtonItemServerStop;
		private RibbonPageGroup ribbonPageGroupLookAndFeel;
		private RibbonGalleryBarItem ribbonGalleryBarItem1;
		private RibbonGalleryBarItem rgbiSkins2;
		private SkinPaletteRibbonGalleryBarItem skinPaletteRibbonGalleryBarItem2;
		private BarButtonItem barButtonItemColorMix;
		private BarToggleSwitchItem barToggleSwitchItemDarkMode;
		private BarToggleSwitchItem barToggleSwitchItemCompactView;
		private BarStaticItem barStaticItemMonitorServer;
		private BarStaticItem barStaticItemUser;
	}
}
