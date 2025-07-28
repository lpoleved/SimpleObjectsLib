using System;
using System.Drawing;
//using System.Windows.Controls.Ribbon;
using DevExpress;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraSplashScreen;
using Simple.Objects.ServerMonitor;

namespace Simple.Objects.ServerMonitor
{
	partial class FormMainOld
	{

		#region Designer generated code
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainOld));
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
			iPaste = new BarButtonItem();
			beiFontSize = new BarEditItem();
			repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			bbiFontColorPopup = new BarButtonItem();
			popupControlContainer1 = new PopupControlContainer(components);
			ribbonStatusBar1 = new RibbonStatusBar();
			siPosition = new BarButtonItem();
			siDocName = new BarStaticItem();
			barStaticItemMonitorServer = new BarStaticItem();
			barStaticItemUser = new BarStaticItem();
			barStaticItemInfo = new BarStaticItem();
			ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
			pmAppMain = new ApplicationMenu(components);
			pccBottom = new PopupControlContainer(components);
			simpleButtonExit = new SimpleButton();
			imageCollection2 = new DevExpress.Utils.ImageCollection(components);
			iSave = new BarButtonItem();
			iReplace = new BarButtonItem();
			iSaveAs = new BarButtonItem();
			iFind = new BarButtonItem();
			bgFontStyle = new BarButtonGroup();
			bgAlign = new BarButtonGroup();
			bgFont = new BarButtonGroup();
			bgBullets = new BarButtonGroup();
			iPasteSpecial = new BarButtonItem();
			iNew = new BarButtonItem();
			iLargeUndo = new BarLargeButtonItem();
			iTemplate = new BarButtonItem();
			barSubItemChangeSkin = new BarSubItem();
			rgbiSkins = new RibbonGalleryBarItem();
			barEditItem1 = new BarEditItem();
			repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			biStyle = new BarEditItem();
			riicStyle = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
			editButtonGroup = new BarButtonGroup();
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
			barButtonItemColorMix = new BarButtonItem();
			barToggleSwitchItemDarkMode = new BarToggleSwitchItem();
			barToggleSwitchItemCompactView = new BarToggleSwitchItem();
			barButtonItemSimpleObjectsOnTheWeb = new BarButtonItem();
			barButtonItemOnlineHelp = new BarButtonItem();
			barButtonItemGettingStarted = new BarButtonItem();
			barButtonItemConntactUs = new BarButtonItem();
			barButtonItemAbout = new BarButtonItem();
			imageCollection1 = new DevExpress.Utils.ImageCollection(components);
			selectionMiniToolbar = new RibbonMiniToolbar(components);
			ribbonPageHome = new RibbonPage();
			ribbonPageGroupMonitorServer = new RibbonPageGroup();
			ribbonPageGroupSimpleObjectsServer = new RibbonPageGroup();
			ribbonPageGroupLookAndFeel = new RibbonPageGroup();
			ribbonPageGroupHelp = new RibbonPageGroup();
			rpHelp = new RibbonPage();
			rpgHelp = new RibbonPageGroup();
			ribbonPage1 = new RibbonPage();
			ribbonPage3 = new RibbonPage();
			rpThemes = new RibbonPage();
			ribbonPageGroup10 = new RibbonPageGroup();
			repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			repositoryItemTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemTrackBar();
			repositoryItemColorPickEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit();
			repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			pmNew = new PopupMenu(components);
			pcAppMenuFileLabels = new PanelControl();
			labelControl1 = new LabelControl();
			defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(components);
			pmMain = new PopupMenu(components);
			imageCollection3 = new DevExpress.Utils.ImageCollection(components);
			backstageViewControl = new BackstageViewControl();
			backstageViewClientControl9 = new BackstageViewClientControl();
			backstageViewClientControl8 = new BackstageViewClientControl();
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
			backstageViewTabItem3 = new BackstageViewTabItem();
			bvItemSettings = new BackstageViewButtonItem();
			backstageViewTabItem1 = new BackstageViewTabItem();
			printTabItem = new BackstageViewTabItem();
			backstageViewTabItem4 = new BackstageViewTabItem();
			backstageViewTabItem6 = new BackstageViewTabItem();
			backstageViewItemSeparator1 = new BackstageViewItemSeparator();
			bvItemExit = new BackstageViewButtonItem();
			recentStackPanel1 = new RecentStackPanel();
			recentStackPanel2 = new RecentStackPanel();
			recentTabItem1 = new RecentTabItem();
			recentStackPanel3 = new RecentStackPanel();
			recentTabItem2 = new RecentTabItem();
			recentStackPanel4 = new RecentStackPanel();
			recentStackPanel5 = new RecentStackPanel();
			recentStackPanel6 = new RecentStackPanel();
			recentTabItem3 = new RecentTabItem();
			recentStackPanel7 = new RecentStackPanel();
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
			taskbarAssistant1 = new DevExpress.Utils.Taskbar.TaskbarAssistant();
			backstageViewTabItem2 = new BackstageViewTabItem();
			bvTabPrint = new BackstageViewTabItem();
			backstageViewClientControl4 = new BackstageViewClientControl();
			emptySpacePanel = new PanelControl();
			recentLabelItem3 = new RecentLabelItem();
			recentLabelItem4 = new RecentLabelItem();
			recentLabelItem5 = new RecentLabelItem();
			recentLabelItem6 = new RecentLabelItem();
			backstageViewButtonItem1 = new BackstageViewButtonItem();
			barEditItemMonitorServerPort = new BarEditItem();
			barEditItem2 = new BarEditItem();
			((System.ComponentModel.ISupportInitialize)repositoryItemSpinEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)popupControlContainer1).BeginInit();
			((System.ComponentModel.ISupportInitialize)ribbonControl).BeginInit();
			((System.ComponentModel.ISupportInitialize)pmAppMain).BeginInit();
			((System.ComponentModel.ISupportInitialize)pccBottom).BeginInit();
			pccBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)imageCollection2).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemPictureEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)riicStyle).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)imageCollection1).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemTrackBar1).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemColorPickEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit2).BeginInit();
			((System.ComponentModel.ISupportInitialize)pmNew).BeginInit();
			((System.ComponentModel.ISupportInitialize)pcAppMenuFileLabels).BeginInit();
			((System.ComponentModel.ISupportInitialize)pmMain).BeginInit();
			((System.ComponentModel.ISupportInitialize)imageCollection3).BeginInit();
			((System.ComponentModel.ISupportInitialize)backstageViewControl).BeginInit();
			backstageViewControl.SuspendLayout();
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
			backstageViewClientControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)recentItemControl1).BeginInit();
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
			((System.ComponentModel.ISupportInitialize)emptySpacePanel).BeginInit();
			SuspendLayout();
			// 
			// iWeb
			// 
			iWeb.Caption = "&Simple.Objects™ on the Web";
			iWeb.CategoryGuid = new Guid("e07a4c24-66ac-4de6-bbcb-c0b6cfa7798b");
			iWeb.Description = "Opens the web page.";
			iWeb.Hint = "DevExpress on the Web";
			iWeb.Id = 21;
			iWeb.ImageOptions.Image = (Image)resources.GetObject("iWeb.ImageOptions.Image");
			iWeb.ImageOptions.ImageIndex = 24;
			iWeb.ImageOptions.LargeImage = (Image)resources.GetObject("iWeb.ImageOptions.LargeImage");
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
			iAbout.ImageOptions.LargeImage = (Image)resources.GetObject("iAbout.ImageOptions.LargeImage");
			iAbout.Name = "iAbout";
			iAbout.Visibility = BarItemVisibility.Never;
			iAbout.ItemClick += iAbout_ItemClick;
			// 
			// iPaste
			// 
			iPaste.Caption = "&Paste";
			iPaste.CategoryGuid = new Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1");
			iPaste.Description = "Inserts the contents of the Clipboard at the insertion point, and replaces any selection. This command is available only if you have cut or copied a text.";
			iPaste.Hint = "Paste";
			iPaste.Id = 11;
			iPaste.ImageOptions.ImageIndex = 8;
			iPaste.ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V);
			iPaste.Name = "iPaste";
			iPaste.ItemClick += iPaste_ItemClick;
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
			popupControlContainer1.Location = new Point(0, 0);
			popupControlContainer1.Name = "popupControlContainer1";
			popupControlContainer1.Size = new Size(0, 0);
			popupControlContainer1.TabIndex = 6;
			popupControlContainer1.Visible = false;
			// 
			// ribbonStatusBar1
			// 
			ribbonStatusBar1.ItemLinks.Add(siPosition);
			ribbonStatusBar1.ItemLinks.Add(siDocName, true);
			ribbonStatusBar1.ItemLinks.Add(barStaticItemMonitorServer);
			ribbonStatusBar1.ItemLinks.Add(barStaticItemUser, true);
			ribbonStatusBar1.ItemLinks.Add(barStaticItemInfo);
			ribbonStatusBar1.Location = new Point(0, 746);
			ribbonStatusBar1.Name = "ribbonStatusBar1";
			ribbonStatusBar1.Size = new Size(1577, 20);
			// 
			// siPosition
			// 
			siPosition.CategoryGuid = new Guid("77795bb7-9bc5-4dd2-a297-cc758682e23d");
			siPosition.Id = 0;
			siPosition.Name = "siPosition";
			siPosition.VisibleInSearchMenu = false;
			// 
			// siDocName
			// 
			siDocName.CategoryGuid = new Guid("77795bb7-9bc5-4dd2-a297-cc758682e23d");
			siDocName.Id = 2;
			siDocName.Name = "siDocName";
			// 
			// barStaticItemMonitorServer
			// 
			barStaticItemMonitorServer.Caption = "Monitor Server";
			barStaticItemMonitorServer.Id = 485;
			barStaticItemMonitorServer.ImageOptions.Image = Properties.Resources.Server;
			barStaticItemMonitorServer.Name = "barStaticItemMonitorServer";
			barStaticItemMonitorServer.PaintStyle = BarItemPaintStyle.CaptionGlyph;
			// 
			// barStaticItemUser
			// 
			barStaticItemUser.Caption = "Monitor Admin";
			barStaticItemUser.Id = 486;
			barStaticItemUser.ImageOptions.Image = Properties.Resources.Administrator;
			barStaticItemUser.Name = "barStaticItemUser";
			barStaticItemUser.PaintStyle = BarItemPaintStyle.CaptionGlyph;
			// 
			// barStaticItemInfo
			// 
			barStaticItemInfo.Id = 487;
			barStaticItemInfo.ImageOptions.Image = (Image)resources.GetObject("barStaticItemInfo.ImageOptions.Image");
			barStaticItemInfo.ImageOptions.ImageIndex = 27;
			barStaticItemInfo.Name = "barStaticItemInfo";
			barStaticItemInfo.PaintStyle = BarItemPaintStyle.CaptionGlyph;
			// 
			// ribbonControl
			// 
			ribbonControl.AllowCustomization = true;
			ribbonControl.ApplicationButtonDropDownControl = pmAppMain;
			ribbonControl.ApplicationButtonText = "APPLICATION";
			ribbonControl.AutoSizeItems = true;
			ribbonControl.Categories.AddRange(new BarManagerCategory[] { new BarManagerCategory("APPLICATION", new Guid("4b511317-d784-42ba-b4ed-0d2a746d6c1f")), new BarManagerCategory("Edit", new Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1")), new BarManagerCategory("Format", new Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258")), new BarManagerCategory("HELP", new Guid("e07a4c24-66ac-4de6-bbcb-c0b6cfa7798b")), new BarManagerCategory("Status", new Guid("77795bb7-9bc5-4dd2-a297-cc758682e23d")) });
			ribbonControl.ExpandCollapseItem.Id = 0;
			ribbonControl.Images = imageCollection2;
			ribbonControl.Items.AddRange(new BarItem[] { ribbonControl.ExpandCollapseItem, iSave, iReplace, iSaveAs, iPaste, iFind, iWeb, iAbout, siPosition, siDocName, bgFontStyle, bgAlign, bgFont, bgBullets, iPasteSpecial, iNew, iLargeUndo, iTemplate, barSubItemChangeSkin, rgbiSkins, beiFontSize, bbiFontColorPopup, barEditItem1, biStyle, editButtonGroup, bbColorMix, barButtonItem1, biOnlineHelp, biGettingStarted, biContact, rgbiColorScheme, biPageColor, biPageBorders, skinPaletteRibbonGalleryBarItem1, barButtonItemConnect, barEditItemMonitorServer, barEditItemServerMonitorPort, barButtonItemDisconnect, barButtonItemServerStart, barButtonItemServerRestart, barButtonItemServerStop, barButtonItemColorMix, barToggleSwitchItemDarkMode, barToggleSwitchItemCompactView, barStaticItemMonitorServer, barStaticItemUser, barStaticItemInfo, barButtonItemSimpleObjectsOnTheWeb, barButtonItemOnlineHelp, barButtonItemGettingStarted, barButtonItemConntactUs, barButtonItemAbout });
			ribbonControl.LargeImages = imageCollection1;
			ribbonControl.Location = new Point(0, 0);
			ribbonControl.MaxItemId = 497;
			ribbonControl.MiniToolbars.Add(selectionMiniToolbar);
			ribbonControl.Name = "ribbonControl";
			ribbonControl.OptionsCustomizationForm.AllowToolbarCustomization = true;
			ribbonControl.OptionsExpandCollapseMenu.ShowRibbonLayoutGroup = DevExpress.Utils.DefaultBoolean.True;
			ribbonControl.OptionsTouch.ShowTouchUISelectorInQAT = true;
			ribbonControl.OptionsTouch.ShowTouchUISelectorVisibilityItemInQATMenu = true;
			ribbonControl.PageHeaderItemLinks.Add(biStyle);
			ribbonControl.PageHeaderItemLinks.Add(barButtonItem1);
			ribbonControl.Pages.AddRange(new RibbonPage[] { ribbonPageHome, rpHelp, ribbonPage1, ribbonPage3, rpThemes });
			ribbonControl.QuickToolbarItemLinks.Add(barButtonItemConnect);
			ribbonControl.QuickToolbarItemLinks.Add(barButtonItemDisconnect);
			ribbonControl.QuickToolbarItemLinks.Add(barButtonItemServerStart, true);
			ribbonControl.QuickToolbarItemLinks.Add(barButtonItemServerRestart);
			ribbonControl.QuickToolbarItemLinks.Add(barButtonItemServerStop);
			ribbonControl.QuickToolbarItemLinks.Add(barSubItemChangeSkin, true);
			ribbonControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemSpinEdit1, repositoryItemPictureEdit1, riicStyle, repositoryItemComboBox1, repositoryItemTrackBar1, repositoryItemColorPickEdit1, repositoryItemButtonEdit1, repositoryItemButtonEdit2 });
			ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.True;
			ribbonControl.ShowItemCaptionsInPageHeader = true;
			ribbonControl.Size = new Size(1577, 158);
			ribbonControl.StatusBar = ribbonStatusBar1;
			ribbonControl.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.True;
			ribbonControl.BeforeApplicationButtonContentControlShow += ribbonControl1_BeforeApplicationButtonContentControlShow;
			ribbonControl.ApplicationButtonDoubleClick += ribbonControl1_ApplicationButtonDoubleClick;
			ribbonControl.ResetLayout += ribbonControl1_ResetLayout;
			// 
			// pmAppMain
			// 
			pmAppMain.BottomPaneControlContainer = pccBottom;
			pmAppMain.Name = "pmAppMain";
			pmAppMain.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			pmAppMain.ShowRightPane = true;
			// 
			// pccBottom
			// 
			pccBottom.Appearance.BackColor = Color.Transparent;
			pccBottom.Appearance.Options.UseBackColor = true;
			pccBottom.AutoSize = true;
			pccBottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			pccBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			pccBottom.Controls.Add(simpleButtonExit);
			pccBottom.Location = new Point(2115, 620);
			pccBottom.Name = "pccBottom";
			pccBottom.Size = new Size(114, 30);
			pccBottom.TabIndex = 6;
			pccBottom.Visible = false;
			// 
			// simpleButtonExit
			// 
			simpleButtonExit.AllowFocus = false;
			simpleButtonExit.AutoSize = true;
			simpleButtonExit.ImageOptions.ImageIndex = 13;
			simpleButtonExit.ImageOptions.ImageList = imageCollection2;
			simpleButtonExit.Location = new Point(5, 5);
			simpleButtonExit.Name = "simpleButtonExit";
			simpleButtonExit.Size = new Size(106, 22);
			simpleButtonExit.TabIndex = 0;
			simpleButtonExit.Text = "E&xit Application";
			simpleButtonExit.Click += simpleButtonExit_Click;
			// 
			// imageCollection2
			// 
			imageCollection2.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("imageCollection2.ImageStream");
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
			iSave.Name = "iSave";
			iSave.RibbonStyle = RibbonItemStyles.SmallWithText;
			iSave.Visibility = BarItemVisibility.Never;
			iSave.ItemClick += iSave_ItemClick;
			// 
			// iReplace
			// 
			iReplace.Caption = "R&eplace...";
			iReplace.CategoryGuid = new Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1");
			iReplace.Description = "Searches for and replaces the specified text.";
			iReplace.Hint = "Replace";
			iReplace.Id = 15;
			iReplace.ImageOptions.ImageIndex = 14;
			iReplace.ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H);
			iReplace.Name = "iReplace";
			iReplace.Visibility = BarItemVisibility.Never;
			iReplace.ItemClick += iReplace_ItemClick;
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
			iSaveAs.Name = "iSaveAs";
			iSaveAs.ItemClick += iSaveAs_ItemClick;
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
			// bgFontStyle
			// 
			bgFontStyle.Caption = "FontStyle";
			bgFontStyle.Id = 0;
			bgFontStyle.Name = "bgFontStyle";
			bgFontStyle.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			// 
			// bgAlign
			// 
			bgAlign.Caption = "Align";
			bgAlign.Id = 0;
			bgAlign.Name = "bgAlign";
			bgAlign.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			// 
			// bgFont
			// 
			bgFont.Caption = "Font";
			bgFont.Id = 0;
			bgFont.Name = "bgFont";
			bgFont.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			// 
			// bgBullets
			// 
			bgBullets.Caption = "Bullets";
			bgBullets.Id = 1;
			bgBullets.Name = "bgBullets";
			bgBullets.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
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
			iPasteSpecial.Name = "iPasteSpecial";
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
			iNew.Name = "iNew";
			iNew.ItemClick += idNew_ItemClick;
			// 
			// iLargeUndo
			// 
			iLargeUndo.Caption = "&Undo";
			iLargeUndo.CategoryGuid = new Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1");
			iLargeUndo.Hint = "Undo";
			iLargeUndo.Id = 0;
			iLargeUndo.ImageOptions.ImageIndex = 11;
			iLargeUndo.ImageOptions.LargeImageIndex = 5;
			iLargeUndo.Name = "iLargeUndo";
			iLargeUndo.ItemClick += iUndo_ItemClick;
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
			iTemplate.Name = "iTemplate";
			// 
			// barSubItemChangeSkin
			// 
			barSubItemChangeSkin.Caption = "Paint style";
			barSubItemChangeSkin.Description = "Select a paint scheme";
			barSubItemChangeSkin.Hint = "Select a paint scheme";
			barSubItemChangeSkin.Id = 7;
			barSubItemChangeSkin.ImageOptions.ImageIndex = 26;
			barSubItemChangeSkin.Name = "barSubItemChangeSkin";
			barSubItemChangeSkin.VisibleInSearchMenu = false;
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
			riicStyle.Appearance.Font = new Font("Tahoma", 6.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
			riicStyle.Appearance.Options.UseFont = true;
			riicStyle.AutoHeight = false;
			riicStyle.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
			riicStyle.Name = "riicStyle";
			// 
			// editButtonGroup
			// 
			editButtonGroup.Id = 145;
			editButtonGroup.ItemLinks.Add(iPaste);
			editButtonGroup.Name = "editButtonGroup";
			editButtonGroup.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			// 
			// bbColorMix
			// 
			bbColorMix.Caption = "&Color Mix";
			bbColorMix.Id = 238;
			bbColorMix.ImageOptions.LargeImageIndex = 0;
			bbColorMix.Name = "bbColorMix";
			bbColorMix.ItemClick += bbColorMix_ItemClick;
			// 
			// barButtonItem1
			// 
			barButtonItem1.Caption = "Share";
			barButtonItem1.Id = 420;
			barButtonItem1.ItemAppearance.Normal.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
			barButtonItem1.ItemAppearance.Normal.Options.UseBackColor = true;
			barButtonItem1.Name = "barButtonItem1";
			barButtonItem1.VisibleInSearchMenu = false;
			// 
			// biOnlineHelp
			// 
			biOnlineHelp.Caption = "Online Help";
			biOnlineHelp.Id = 423;
			biOnlineHelp.ImageOptions.LargeImage = (Image)resources.GetObject("biOnlineHelp.ImageOptions.LargeImage");
			biOnlineHelp.Name = "biOnlineHelp";
			// 
			// biGettingStarted
			// 
			biGettingStarted.Caption = "Getting Started";
			biGettingStarted.Id = 424;
			biGettingStarted.ImageOptions.LargeImage = (Image)resources.GetObject("biGettingStarted.ImageOptions.LargeImage");
			biGettingStarted.Name = "biGettingStarted";
			// 
			// biContact
			// 
			biContact.Caption = "Contact Us";
			biContact.Id = 425;
			biContact.ImageOptions.LargeImage = (Image)resources.GetObject("biContact.ImageOptions.LargeImage");
			biContact.Name = "biContact";
			// 
			// rgbiColorScheme
			// 
			rgbiColorScheme.Caption = "ribbonGalleryBarItem1";
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
			galleryItem9.Checked = true;
			galleryItemGroup1.Items.AddRange(new GalleryItem[] { galleryItem1, galleryItem2, galleryItem3, galleryItem4, galleryItem5, galleryItem6, galleryItem7, galleryItem8, galleryItem9 });
			rgbiColorScheme.Gallery.Groups.AddRange(new GalleryItemGroup[] { galleryItemGroup1 });
			rgbiColorScheme.Gallery.ImageSize = new Size(32, 32);
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
			biPageColor.Name = "biPageColor";
			// 
			// biPageBorders
			// 
			biPageBorders.Caption = "Page Borders";
			biPageBorders.Id = 439;
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
			barEditItemServerMonitorPort.Id = 469;
			barEditItemServerMonitorPort.Name = "barEditItemServerMonitorPort";
			// 
			// barButtonItemDisconnect
			// 
			barButtonItemDisconnect.Caption = "Disconnect";
			barButtonItemDisconnect.Id = 470;
			barButtonItemDisconnect.ImageOptions.Image = Properties.Resources.Server_Disconnect;
			barButtonItemDisconnect.ImageOptions.LargeImage = Properties.Resources.Server_Disconnect_Large;
			barButtonItemDisconnect.Name = "barButtonItemDisconnect";
			// 
			// barButtonItemServerStart
			// 
			barButtonItemServerStart.Caption = "Start";
			barButtonItemServerStart.Id = 473;
			barButtonItemServerStart.ImageOptions.Image = Properties.Resources.Play_Green;
			barButtonItemServerStart.ImageOptions.LargeImage = Properties.Resources.Play_Green_Large;
			barButtonItemServerStart.Name = "barButtonItemServerStart";
			// 
			// barButtonItemServerRestart
			// 
			barButtonItemServerRestart.Caption = "Restart";
			barButtonItemServerRestart.Id = 474;
			barButtonItemServerRestart.ImageOptions.Image = Properties.Resources.Reload;
			barButtonItemServerRestart.ImageOptions.LargeImage = Properties.Resources.Reload_Large;
			barButtonItemServerRestart.Name = "barButtonItemServerRestart";
			// 
			// barButtonItemServerStop
			// 
			barButtonItemServerStop.Caption = "Stop";
			barButtonItemServerStop.Id = 475;
			barButtonItemServerStop.ImageOptions.Image = Properties.Resources.Stop_Red;
			barButtonItemServerStop.ImageOptions.LargeImage = Properties.Resources.Stop_Red_Large;
			barButtonItemServerStop.Name = "barButtonItemServerStop";
			// 
			// barButtonItemColorMix
			// 
			barButtonItemColorMix.Caption = "&Color Mix";
			barButtonItemColorMix.Id = 481;
			barButtonItemColorMix.ImageOptions.Image = Properties.Resources.ColorMixer_16x16;
			barButtonItemColorMix.ImageOptions.LargeImage = Properties.Resources.ColorMixer_32x32;
			barButtonItemColorMix.Name = "barButtonItemColorMix";
			barButtonItemColorMix.ItemClick += barButtonItemColorMix_ItemClick;
			// 
			// barToggleSwitchItemDarkMode
			// 
			barToggleSwitchItemDarkMode.Caption = "Dark Mode";
			barToggleSwitchItemDarkMode.Id = 482;
			barToggleSwitchItemDarkMode.Name = "barToggleSwitchItemDarkMode";
			// 
			// barToggleSwitchItemCompactView
			// 
			barToggleSwitchItemCompactView.Caption = "Compact View";
			barToggleSwitchItemCompactView.Id = 483;
			barToggleSwitchItemCompactView.Name = "barToggleSwitchItemCompactView";
			// 
			// barButtonItemSimpleObjectsOnTheWeb
			// 
			barButtonItemSimpleObjectsOnTheWeb.Caption = "Simple.Objects™ on the Web";
			barButtonItemSimpleObjectsOnTheWeb.Id = 491;
			barButtonItemSimpleObjectsOnTheWeb.ImageOptions.Image = (Image)resources.GetObject("barButtonItemSimpleObjectsOnTheWeb.ImageOptions.Image");
			barButtonItemSimpleObjectsOnTheWeb.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItemSimpleObjectsOnTheWeb.ImageOptions.LargeImage");
			barButtonItemSimpleObjectsOnTheWeb.Name = "barButtonItemSimpleObjectsOnTheWeb";
			// 
			// barButtonItemOnlineHelp
			// 
			barButtonItemOnlineHelp.Caption = "Online Help";
			barButtonItemOnlineHelp.Id = 492;
			barButtonItemOnlineHelp.ImageOptions.Image = (Image)resources.GetObject("barButtonItemOnlineHelp.ImageOptions.Image");
			barButtonItemOnlineHelp.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItemOnlineHelp.ImageOptions.LargeImage");
			barButtonItemOnlineHelp.Name = "barButtonItemOnlineHelp";
			// 
			// barButtonItemGettingStarted
			// 
			barButtonItemGettingStarted.Caption = "Getting Started";
			barButtonItemGettingStarted.Id = 493;
			barButtonItemGettingStarted.ImageOptions.Image = (Image)resources.GetObject("barButtonItemGettingStarted.ImageOptions.Image");
			barButtonItemGettingStarted.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItemGettingStarted.ImageOptions.LargeImage");
			barButtonItemGettingStarted.Name = "barButtonItemGettingStarted";
			// 
			// barButtonItemConntactUs
			// 
			barButtonItemConntactUs.Caption = "Conntact Us";
			barButtonItemConntactUs.Id = 494;
			barButtonItemConntactUs.ImageOptions.Image = (Image)resources.GetObject("barButtonItemConntactUs.ImageOptions.Image");
			barButtonItemConntactUs.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItemConntactUs.ImageOptions.LargeImage");
			barButtonItemConntactUs.Name = "barButtonItemConntactUs";
			// 
			// barButtonItemAbout
			// 
			barButtonItemAbout.Caption = "About";
			barButtonItemAbout.Id = 495;
			barButtonItemAbout.ImageOptions.Image = (Image)resources.GetObject("barButtonItemAbout.ImageOptions.Image");
			barButtonItemAbout.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItemAbout.ImageOptions.LargeImage");
			barButtonItemAbout.Name = "barButtonItemAbout";
			// 
			// imageCollection1
			// 
			imageCollection1.ImageSize = new Size(32, 32);
			imageCollection1.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("imageCollection1.ImageStream");
			// 
			// selectionMiniToolbar
			// 
			selectionMiniToolbar.Alignment = ContentAlignment.TopRight;
			selectionMiniToolbar.ItemLinks.Add(bgFont);
			selectionMiniToolbar.ItemLinks.Add(bgFontStyle);
			selectionMiniToolbar.ItemLinks.Add(bgAlign);
			selectionMiniToolbar.ItemLinks.Add(editButtonGroup);
			selectionMiniToolbar.ParentControl = this;
			// 
			// ribbonPageHome
			// 
			ribbonPageHome.Groups.AddRange(new RibbonPageGroup[] { ribbonPageGroupMonitorServer, ribbonPageGroupSimpleObjectsServer, ribbonPageGroupLookAndFeel, ribbonPageGroupHelp });
			ribbonPageHome.Name = "ribbonPageHome";
			ribbonPageHome.Text = "HOME";
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
			ribbonPageGroupLookAndFeel.ItemLinks.Add(rgbiSkins);
			ribbonPageGroupLookAndFeel.ItemLinks.Add(skinPaletteRibbonGalleryBarItem1);
			ribbonPageGroupLookAndFeel.ItemLinks.Add(barButtonItemColorMix);
			ribbonPageGroupLookAndFeel.ItemLinks.Add(barToggleSwitchItemDarkMode, true);
			ribbonPageGroupLookAndFeel.ItemLinks.Add(barToggleSwitchItemCompactView);
			ribbonPageGroupLookAndFeel.Name = "ribbonPageGroupLookAndFeel";
			ribbonPageGroupLookAndFeel.Text = "Look and Feel";
			// 
			// ribbonPageGroupHelp
			// 
			ribbonPageGroupHelp.ItemLinks.Add(barButtonItemSimpleObjectsOnTheWeb);
			ribbonPageGroupHelp.ItemLinks.Add(barButtonItemOnlineHelp);
			ribbonPageGroupHelp.ItemLinks.Add(barButtonItemGettingStarted);
			ribbonPageGroupHelp.ItemLinks.Add(barButtonItemConntactUs);
			ribbonPageGroupHelp.ItemLinks.Add(barButtonItemAbout);
			ribbonPageGroupHelp.Name = "ribbonPageGroupHelp";
			ribbonPageGroupHelp.Text = "Help";
			// 
			// rpHelp
			// 
			rpHelp.Groups.AddRange(new RibbonPageGroup[] { rpgHelp });
			rpHelp.Name = "rpHelp";
			rpHelp.Text = "HELP";
			// 
			// rpgHelp
			// 
			rpgHelp.ItemLinks.Add(iWeb);
			rpgHelp.ItemLinks.Add(biOnlineHelp);
			rpgHelp.ItemLinks.Add(biGettingStarted);
			rpgHelp.ItemLinks.Add(biContact);
			rpgHelp.ItemLinks.Add(iAbout);
			rpgHelp.Name = "rpgHelp";
			rpgHelp.Text = "Help";
			// 
			// ribbonPage1
			// 
			ribbonPage1.Name = "ribbonPage1";
			ribbonPage1.Text = "Home Old";
			ribbonPage1.Visible = false;
			// 
			// ribbonPage3
			// 
			ribbonPage3.Name = "ribbonPage3";
			ribbonPage3.Text = "Design";
			ribbonPage3.Visible = false;
			// 
			// rpThemes
			// 
			rpThemes.Groups.AddRange(new RibbonPageGroup[] { ribbonPageGroup10 });
			rpThemes.Name = "rpThemes";
			rpThemes.Text = "Themes";
			rpThemes.Visible = false;
			// 
			// ribbonPageGroup10
			// 
			ribbonPageGroup10.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
			ribbonPageGroup10.ItemLinks.Add(rgbiSkins);
			ribbonPageGroup10.ItemLinks.Add(skinPaletteRibbonGalleryBarItem1);
			ribbonPageGroup10.Name = "ribbonPageGroup10";
			ribbonPageGroup10.Text = "Skins";
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
			repositoryItemColorPickEdit1.AutomaticColor = Color.Black;
			repositoryItemColorPickEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
			repositoryItemColorPickEdit1.Name = "repositoryItemColorPickEdit1";
			// 
			// repositoryItemButtonEdit2
			// 
			repositoryItemButtonEdit2.AutoHeight = false;
			repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
			repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
			// 
			// pmNew
			// 
			pmNew.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(iNew), new LinkPersistInfo(iTemplate) });
			pmNew.MenuCaption = "New";
			pmNew.Name = "pmNew";
			pmNew.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
			// 
			// pcAppMenuFileLabels
			// 
			pcAppMenuFileLabels.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			pcAppMenuFileLabels.Dock = System.Windows.Forms.DockStyle.Fill;
			pcAppMenuFileLabels.Location = new Point(0, 27);
			pcAppMenuFileLabels.Name = "pcAppMenuFileLabels";
			pcAppMenuFileLabels.Size = new Size(496, 205);
			pcAppMenuFileLabels.TabIndex = 2;
			// 
			// labelControl1
			// 
			labelControl1.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
			labelControl1.Appearance.Options.UseFont = true;
			labelControl1.AutoSizeMode = LabelAutoSizeMode.None;
			labelControl1.Dock = System.Windows.Forms.DockStyle.Top;
			labelControl1.LineLocation = LineLocation.Bottom;
			labelControl1.LineVisible = true;
			labelControl1.Location = new Point(0, 0);
			labelControl1.Name = "labelControl1";
			labelControl1.Size = new Size(496, 27);
			labelControl1.TabIndex = 0;
			labelControl1.Text = "Recent Documents:";
			// 
			// pmMain
			// 
			pmMain.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(iPaste) });
			pmMain.MenuCaption = "Edit Menu";
			pmMain.MultiColumn = DevExpress.Utils.DefaultBoolean.True;
			pmMain.Name = "pmMain";
			pmMain.OptionsMultiColumn.ItemDisplayMode = DevExpress.Utils.Menu.MultiColumnItemDisplayMode.Both;
			pmMain.OptionsMultiColumn.ShowItemText = DevExpress.Utils.DefaultBoolean.True;
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
			backstageViewControl.Controls.Add(backstageViewClientControl9);
			backstageViewControl.Controls.Add(backstageViewClientControl8);
			backstageViewControl.Controls.Add(backstageViewClientControl11);
			backstageViewControl.Controls.Add(backstageViewClientControl10);
			backstageViewControl.Controls.Add(backstageViewClientControl2);
			backstageViewControl.Images = imageCollection2;
			backstageViewControl.Items.Add(backstageViewTabItem3);
			backstageViewControl.Items.Add(bvItemSettings);
			backstageViewControl.Items.Add(backstageViewTabItem1);
			backstageViewControl.Items.Add(printTabItem);
			backstageViewControl.Items.Add(backstageViewTabItem4);
			backstageViewControl.Items.Add(backstageViewTabItem6);
			backstageViewControl.Items.Add(backstageViewItemSeparator1);
			backstageViewControl.Items.Add(bvItemExit);
			backstageViewControl.Location = new Point(12, 251);
			backstageViewControl.Name = "backstageViewControl";
			backstageViewControl.Office2013StyleOptions.RightPaneContentVerticalOffset = 70;
			backstageViewControl.SelectedTab = printTabItem;
			backstageViewControl.SelectedTabIndex = 3;
			backstageViewControl.Size = new Size(651, 358);
			backstageViewControl.TabIndex = 9;
			backstageViewControl.Text = "backstageViewControl1";
			// 
			// backstageViewClientControl9
			// 
			backstageViewClientControl9.Location = new Point(132, 71);
			backstageViewClientControl9.Name = "backstageViewClientControl9";
			backstageViewClientControl9.Size = new Size(1494, 616);
			backstageViewClientControl9.TabIndex = 7;
			// 
			// backstageViewClientControl8
			// 
			backstageViewClientControl8.Location = new Point(132, 71);
			backstageViewClientControl8.Name = "backstageViewClientControl8";
			backstageViewClientControl8.Size = new Size(709, 432);
			backstageViewClientControl8.TabIndex = 6;
			// 
			// backstageViewClientControl11
			// 
			backstageViewClientControl11.Controls.Add(recentControlPrint);
			backstageViewClientControl11.Location = new Point(132, 0);
			backstageViewClientControl11.Name = "backstageViewClientControl11";
			backstageViewClientControl11.Size = new Size(519, 358);
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
			recentControlPrint.Location = new Point(0, 0);
			recentControlPrint.MainPanel = recentStackPanel9;
			recentControlPrint.MainPanelMinWidth = 100;
			recentControlPrint.Name = "recentControlPrint";
			recentControlPrint.ShowTitle = false;
			recentControlPrint.Size = new Size(519, 358);
			recentControlPrint.TabIndex = 0;
			recentControlPrint.Title = "Print";
			// 
			// recentPrintOptionsContainer
			// 
			recentPrintOptionsContainer.Appearance.BackColor = SystemColors.Control;
			recentPrintOptionsContainer.Appearance.Options.UseBackColor = true;
			recentPrintOptionsContainer.Controls.Add(layoutControl1);
			recentPrintOptionsContainer.Name = "recentPrintOptionsContainer";
			recentPrintOptionsContainer.Size = new Size(268, 266);
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
			layoutControl1.Location = new Point(0, 0);
			layoutControl1.Name = "layoutControl1";
			layoutControl1.Root = layoutControlGroup1;
			layoutControl1.Size = new Size(268, 266);
			layoutControl1.TabIndex = 0;
			layoutControl1.Text = "layoutControl1";
			// 
			// ddbDuplex
			// 
			ddbDuplex.Appearance.Options.UseTextOptions = true;
			ddbDuplex.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			ddbDuplex.Location = new Point(2, 186);
			ddbDuplex.Name = "ddbDuplex";
			ddbDuplex.Size = new Size(260, 52);
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
			ddbOrientation.Size = new Size(260, 52);
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
			ddbPaperSize.Size = new Size(260, 52);
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
			ddbMargins.Size = new Size(260, 52);
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
			ddbCollate.Size = new Size(260, 52);
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
			ddbPrinter.Size = new Size(264, 56);
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
			layoutControlGroup1.Items.AddRange(new BaseLayoutItem[] { layoutControlItem1, lciCopiesSpinEdit, layoutControlItem3, layoutControlItem2, layoutControlItem4, layoutControlItem5, layoutControlItem6, layoutControlItem7, layoutControlItem8, layoutControlItem9 });
			layoutControlGroup1.Name = "layoutControlGroup1";
			layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			layoutControlGroup1.Size = new Size(264, 464);
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
			layoutControlItem1.SizeConstraintsType = SizeConstraintsType.Custom;
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
			lciCopiesSpinEdit.Size = new Size(180, 80);
			lciCopiesSpinEdit.SizeConstraintsType = SizeConstraintsType.Custom;
			lciCopiesSpinEdit.Text = "Copies:";
			// 
			// layoutControlItem3
			// 
			layoutControlItem3.Location = new Point(0, 80);
			layoutControlItem3.Name = "layoutControlItem3";
			layoutControlItem3.Size = new Size(264, 24);
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
			layoutControlItem2.Size = new Size(264, 56);
			layoutControlItem2.SizeConstraintsType = SizeConstraintsType.Custom;
			layoutControlItem2.TextSize = new Size(0, 0);
			layoutControlItem2.TextVisible = false;
			// 
			// layoutControlItem4
			// 
			layoutControlItem4.Location = new Point(0, 160);
			layoutControlItem4.Name = "layoutControlItem4";
			layoutControlItem4.Size = new Size(264, 24);
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
			layoutControlItem5.Size = new Size(264, 56);
			layoutControlItem5.SizeConstraintsType = SizeConstraintsType.Custom;
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
			layoutControlItem6.Size = new Size(264, 56);
			layoutControlItem6.SizeConstraintsType = SizeConstraintsType.Custom;
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
			layoutControlItem7.Size = new Size(264, 56);
			layoutControlItem7.SizeConstraintsType = SizeConstraintsType.Custom;
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
			layoutControlItem8.Size = new Size(264, 56);
			layoutControlItem8.SizeConstraintsType = SizeConstraintsType.Custom;
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
			layoutControlItem9.Size = new Size(264, 56);
			layoutControlItem9.SizeConstraintsType = SizeConstraintsType.Custom;
			layoutControlItem9.TextSize = new Size(0, 0);
			layoutControlItem9.TextVisible = false;
			// 
			// recentPrintPreviewContainer
			// 
			recentPrintPreviewContainer.Appearance.BackColor = SystemColors.Control;
			recentPrintPreviewContainer.Appearance.Options.UseBackColor = true;
			recentPrintPreviewContainer.Controls.Add(panelControl2);
			recentPrintPreviewContainer.Name = "recentPrintPreviewContainer";
			recentPrintPreviewContainer.Size = new Size(186, 346);
			recentPrintPreviewContainer.TabIndex = 2;
			// 
			// panelControl2
			// 
			panelControl2.Controls.Add(printControl2);
			panelControl2.Controls.Add(panelControl3);
			panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			panelControl2.Location = new Point(0, 0);
			panelControl2.Name = "panelControl2";
			panelControl2.Size = new Size(186, 346);
			panelControl2.TabIndex = 0;
			// 
			// printControl2
			// 
			printControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			printControl2.IsMetric = true;
			printControl2.Location = new Point(2, 2);
			printControl2.Name = "printControl2";
			printControl2.Size = new Size(182, 302);
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
			panelControl3.Location = new Point(2, 304);
			panelControl3.Name = "panelControl3";
			panelControl3.Size = new Size(182, 40);
			panelControl3.TabIndex = 2;
			// 
			// stackPanel1
			// 
			stackPanel1.Controls.Add(zoomTextEdit);
			stackPanel1.Dock = System.Windows.Forms.DockStyle.Right;
			stackPanel1.LayoutDirection = DevExpress.Utils.Layout.StackPanelLayoutDirection.RightToLeft;
			stackPanel1.Location = new Point(-518, 2);
			stackPanel1.Name = "stackPanel1";
			stackPanel1.Size = new Size(340, 36);
			stackPanel1.TabIndex = 7;
			// 
			// zoomTextEdit
			// 
			zoomTextEdit.Dock = System.Windows.Forms.DockStyle.Right;
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
			panel2.Dock = System.Windows.Forms.DockStyle.Right;
			panel2.Location = new Point(-178, 2);
			panel2.Name = "panel2";
			panel2.Size = new Size(16, 36);
			panel2.TabIndex = 5;
			// 
			// pageButtonEdit
			// 
			pageButtonEdit.Dock = System.Windows.Forms.DockStyle.Left;
			pageButtonEdit.EditValue = "1";
			pageButtonEdit.Location = new Point(2, 2);
			pageButtonEdit.Name = "pageButtonEdit";
			pageButtonEdit.Properties.Appearance.Options.UseTextOptions = true;
			pageButtonEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			pageButtonEdit.Properties.AutoHeight = false;
			pageButtonEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			pageButtonEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Left, "", -1, true, true, true, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default), new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Right) });
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
			zoomTrackBarControl1.Dock = System.Windows.Forms.DockStyle.Right;
			zoomTrackBarControl1.EditValue = 40;
			zoomTrackBarControl1.Location = new Point(-162, 2);
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
			backstageViewClientControl10.Location = new Point(132, 71);
			backstageViewClientControl10.Name = "backstageViewClientControl10";
			backstageViewClientControl10.Size = new Size(1494, 616);
			backstageViewClientControl10.TabIndex = 8;
			// 
			// recentControlExport
			// 
			recentControlExport.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			recentControlExport.ContentPanelMinWidth = 100;
			recentControlExport.DefaultContentPanel = recentStackPanel14;
			recentControlExport.Dock = System.Windows.Forms.DockStyle.Fill;
			recentControlExport.Location = new Point(0, 0);
			recentControlExport.MainPanel = recentStackPanel10;
			recentControlExport.MainPanelMinWidth = 100;
			recentControlExport.MinimumSize = new Size(400, 100);
			recentControlExport.Name = "recentControlExport";
			recentControlExport.SelectedTab = recentTabItem4;
			recentControlExport.ShowTitle = false;
			recentControlExport.Size = new Size(1494, 616);
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
			// backstageViewClientControl2
			// 
			backstageViewClientControl2.Controls.Add(recentItemControl1);
			backstageViewClientControl2.Location = new Point(132, 71);
			backstageViewClientControl2.Name = "backstageViewClientControl2";
			backstageViewClientControl2.Size = new Size(1494, 616);
			backstageViewClientControl2.TabIndex = 10;
			// 
			// recentItemControl1
			// 
			recentItemControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			recentItemControl1.ContentPanelMinWidth = 460;
			recentItemControl1.DefaultContentPanel = recentStackPanel12;
			recentItemControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			recentItemControl1.Location = new Point(0, 0);
			recentItemControl1.MainPanel = recentStackPanel13;
			recentItemControl1.MainPanelMinWidth = 180;
			recentItemControl1.Name = "recentItemControl1";
			recentItemControl1.ShowTitle = false;
			recentItemControl1.Size = new Size(1494, 616);
			recentItemControl1.SplitterPosition = 750;
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
			recentHyperlinkItem1.Caption = "www.netakod-community.com";
			recentHyperlinkItem1.Name = "recentHyperlinkItem1";
			// 
			// recentLabelItem2
			// 
			recentLabelItem2.AllowSelect = DevExpress.Utils.DefaultBoolean.False;
			recentLabelItem2.Caption = "Copyright (c) 2025 Netakod Community Ltd. ALL RIGHTS RESERVED.";
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
			recentPinItem2.Caption = "Simple.Objects™ Online Help";
			recentPinItem2.Description = "Get help using Simple.Objects™ components";
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
			recentPinItem4.Description = "Let us know if you need help or how we can make our solution better";
			recentPinItem4.GlyphAlignment = RecentPinItemGlyphAlignment.Center;
			recentPinItem4.ImageOptions.ItemNormal.SvgImage = Properties.Resources.ContactUs;
			recentPinItem4.Name = "recentPinItem4";
			recentPinItem4.PinButtonVisibility = RecentPinButtonVisibility.Never;
			// 
			// backstageViewTabItem3
			// 
			backstageViewTabItem3.Caption = "Open";
			backstageViewTabItem3.CaptionHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			backstageViewTabItem3.ContentControl = backstageViewClientControl9;
			backstageViewTabItem3.GlyphHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			backstageViewTabItem3.GlyphLocation = DevExpress.Utils.Drawing.ItemLocation.Top;
			backstageViewTabItem3.Name = "backstageViewTabItem3";
			backstageViewTabItem3.Visible = false;
			// 
			// bvItemSettings
			// 
			bvItemSettings.Caption = "SETTINGS";
			bvItemSettings.CaptionHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			bvItemSettings.GlyphHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			bvItemSettings.GlyphLocation = DevExpress.Utils.Drawing.ItemLocation.Top;
			bvItemSettings.Name = "bvItemSettings";
			bvItemSettings.ItemClick += BvItemSettings_ItemClick;
			// 
			// backstageViewTabItem1
			// 
			backstageViewTabItem1.Caption = "Save As";
			backstageViewTabItem1.CaptionHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			backstageViewTabItem1.ContentControl = backstageViewClientControl8;
			backstageViewTabItem1.GlyphHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			backstageViewTabItem1.GlyphLocation = DevExpress.Utils.Drawing.ItemLocation.Top;
			backstageViewTabItem1.Name = "backstageViewTabItem1";
			backstageViewTabItem1.Visible = false;
			// 
			// printTabItem
			// 
			printTabItem.Caption = "PRINT";
			printTabItem.CaptionHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			printTabItem.ContentControl = backstageViewClientControl11;
			printTabItem.GlyphHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			printTabItem.GlyphLocation = DevExpress.Utils.Drawing.ItemLocation.Top;
			printTabItem.ImageOptions.ItemNormal.SvgImage = Properties.Resources.Print;
			printTabItem.Name = "printTabItem";
			printTabItem.Selected = true;
			printTabItem.SelectedChanged += onTabPrint_SelectedChanged;
			// 
			// backstageViewTabItem4
			// 
			backstageViewTabItem4.Caption = "EXPORT";
			backstageViewTabItem4.CaptionHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			backstageViewTabItem4.ContentControl = backstageViewClientControl10;
			backstageViewTabItem4.GlyphHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			backstageViewTabItem4.GlyphLocation = DevExpress.Utils.Drawing.ItemLocation.Top;
			backstageViewTabItem4.ImageOptions.ItemNormal.SvgImage = Properties.Resources.ExportTo;
			backstageViewTabItem4.ImageOptions.ItemNormal.SvgImageSize = new Size(32, 32);
			backstageViewTabItem4.Name = "backstageViewTabItem4";
			backstageViewTabItem4.Visible = false;
			// 
			// backstageViewTabItem6
			// 
			backstageViewTabItem6.Caption = "HELP";
			backstageViewTabItem6.CaptionHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			backstageViewTabItem6.ContentControl = backstageViewClientControl2;
			backstageViewTabItem6.GlyphHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			backstageViewTabItem6.GlyphLocation = DevExpress.Utils.Drawing.ItemLocation.Top;
			backstageViewTabItem6.ImageOptions.ItemNormal.SvgImage = Properties.Resources.DevExpressOnlineHelp;
			backstageViewTabItem6.Name = "backstageViewTabItem6";
			// 
			// backstageViewItemSeparator1
			// 
			backstageViewItemSeparator1.Alignment = BackstageViewItemAlignment.Bottom;
			backstageViewItemSeparator1.Name = "backstageViewItemSeparator1";
			// 
			// bvItemExit
			// 
			bvItemExit.Alignment = BackstageViewItemAlignment.Bottom;
			bvItemExit.Caption = "EXIT";
			bvItemExit.Name = "bvItemExit";
			bvItemExit.ItemClick += bvItemExit_ItemClick;
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
			// tabControl
			// 
			tabControl.Location = new Point(668, 273);
			tabControl.Name = "tabControl";
			tabControl.SelectedTabPage = tabPageLog;
			tabControl.Size = new Size(718, 299);
			tabControl.TabIndex = 15;
			tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { tabPageLog, tabPageUserSessions, tabSocketPAckageCommunication, tabPageTransactions, tabPageErrors, tabPageStatistics });
			// 
			// tabPageLog
			// 
			tabPageLog.Name = "tabPageLog";
			tabPageLog.Size = new Size(716, 274);
			tabPageLog.Text = "Log";
			// 
			// tabPageUserSessions
			// 
			tabPageUserSessions.Controls.Add(gridControlUserSessions);
			tabPageUserSessions.Name = "tabPageUserSessions";
			tabPageUserSessions.Size = new Size(716, 274);
			tabPageUserSessions.Text = "User Sessions";
			// 
			// gridControlUserSessions
			// 
			gridControlUserSessions.Dock = System.Windows.Forms.DockStyle.Fill;
			gridControlUserSessions.Location = new Point(0, 0);
			gridControlUserSessions.MainView = gridViewUserSessions;
			gridControlUserSessions.Name = "gridControlUserSessions";
			gridControlUserSessions.Size = new Size(716, 274);
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
			tabSocketPAckageCommunication.Size = new Size(716, 274);
			tabSocketPAckageCommunication.Text = "Socket Package Communication";
			// 
			// splitContainerControlSocketPackageCommunication
			// 
			splitContainerControlSocketPackageCommunication.Dock = System.Windows.Forms.DockStyle.Fill;
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
			splitContainerControlSocketPackageCommunication.Size = new Size(716, 274);
			splitContainerControlSocketPackageCommunication.SplitterPosition = 818;
			splitContainerControlSocketPackageCommunication.TabIndex = 4;
			// 
			// gridControlRequestResponseMessages
			// 
			gridControlRequestResponseMessages.Dock = System.Windows.Forms.DockStyle.Fill;
			gridControlRequestResponseMessages.Location = new Point(0, 0);
			gridControlRequestResponseMessages.MainView = gridViewRequestResponseMessagess;
			gridControlRequestResponseMessages.Name = "gridControlRequestResponseMessages";
			gridControlRequestResponseMessages.Size = new Size(706, 274);
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
			groupPropertyPanelRequestResponseMessages.Location = new Point(0, 0);
			groupPropertyPanelRequestResponseMessages.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			groupPropertyPanelRequestResponseMessages.Name = "groupPropertyPanelRequestResponseMessages";
			groupPropertyPanelRequestResponseMessages.Size = new Size(0, 0);
			groupPropertyPanelRequestResponseMessages.TabIndex = 0;
			// 
			// tabPageTransactions
			// 
			tabPageTransactions.Controls.Add(splitContainerControlTransactions);
			tabPageTransactions.Name = "tabPageTransactions";
			tabPageTransactions.Size = new Size(716, 274);
			tabPageTransactions.Text = "Transactions";
			// 
			// splitContainerControlTransactions
			// 
			splitContainerControlTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
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
			splitContainerControlTransactions.Size = new Size(716, 274);
			splitContainerControlTransactions.SplitterPosition = 814;
			splitContainerControlTransactions.TabIndex = 0;
			// 
			// gridControlTransactions
			// 
			gridControlTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
			gridControlTransactions.Location = new Point(0, 0);
			gridControlTransactions.MainView = gridViewTransactions;
			gridControlTransactions.Name = "gridControlTransactions";
			gridControlTransactions.Size = new Size(706, 274);
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
			groupPropertyPanelTransactionLog.Location = new Point(0, 0);
			groupPropertyPanelTransactionLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			groupPropertyPanelTransactionLog.Name = "groupPropertyPanelTransactionLog";
			groupPropertyPanelTransactionLog.Size = new Size(0, 0);
			groupPropertyPanelTransactionLog.TabIndex = 0;
			// 
			// tabPageErrors
			// 
			tabPageErrors.Name = "tabPageErrors";
			tabPageErrors.Size = new Size(716, 274);
			tabPageErrors.Text = "Errors";
			// 
			// tabPageStatistics
			// 
			tabPageStatistics.Name = "tabPageStatistics";
			tabPageStatistics.Size = new Size(716, 274);
			tabPageStatistics.Text = "Statistics";
			// 
			// printControl1
			// 
			printControl1.Dock = System.Windows.Forms.DockStyle.Fill;
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
			recentControlRecentItem10.Name = "recentControlRecentItem10";
			recentControlRecentItem10.PinButtonVisibility = RecentPinButtonVisibility.Never;
			// 
			// recentControlButtonItem1
			// 
			recentControlButtonItem1.AutoSize = false;
			recentControlButtonItem1.Caption = "Save As";
			recentControlButtonItem1.Name = "recentControlButtonItem1";
			recentControlButtonItem1.Orientation = System.Windows.Forms.Orientation.Vertical;
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
			// taskbarAssistant1
			// 
			taskbarAssistant1.ParentControl = this;
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
			// emptySpacePanel
			// 
			emptySpacePanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			emptySpacePanel.Dock = System.Windows.Forms.DockStyle.Top;
			emptySpacePanel.Location = new Point(0, 158);
			emptySpacePanel.Name = "emptySpacePanel";
			emptySpacePanel.Size = new Size(1577, 23);
			emptySpacePanel.TabIndex = 12;
			// 
			// recentLabelItem3
			// 
			recentLabelItem3.Caption = null;
			recentLabelItem3.Name = "recentLabelItem3";
			// 
			// recentLabelItem4
			// 
			recentLabelItem4.Caption = null;
			recentLabelItem4.Name = "recentLabelItem4";
			// 
			// recentLabelItem5
			// 
			recentLabelItem5.Caption = null;
			recentLabelItem5.Name = "recentLabelItem5";
			// 
			// recentLabelItem6
			// 
			recentLabelItem6.Caption = null;
			recentLabelItem6.Name = "recentLabelItem6";
			// 
			// backstageViewButtonItem1
			// 
			backstageViewButtonItem1.Caption = "HELP";
			backstageViewButtonItem1.CaptionHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			backstageViewButtonItem1.GlyphHorizontalAlignment = DevExpress.Utils.Drawing.ItemHorizontalAlignment.Center;
			backstageViewButtonItem1.GlyphLocation = DevExpress.Utils.Drawing.ItemLocation.Top;
			backstageViewButtonItem1.Name = "backstageViewButtonItem1";
			// 
			// barEditItemMonitorServerPort
			// 
			barEditItemMonitorServerPort.Caption = "Server Monitor Port";
			barEditItemMonitorServerPort.Edit = null;
			barEditItemMonitorServerPort.EditWidth = 70;
			barEditItemMonitorServerPort.Id = 457;
			barEditItemMonitorServerPort.Name = "barEditItemMonitorServerPort";
			// 
			// barEditItem2
			// 
			barEditItem2.Caption = "Server Monitor Port";
			barEditItem2.Edit = null;
			barEditItem2.EditWidth = 70;
			barEditItem2.Id = 457;
			barEditItem2.Name = "barEditItem2";
			// 
			// FormMainNew
			// 
			AllowFormGlass = DevExpress.Utils.DefaultBoolean.False;
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new Size(1577, 766);
			Controls.Add(tabControl);
			Controls.Add(emptySpacePanel);
			Controls.Add(backstageViewControl);
			Controls.Add(pccBottom);
			Controls.Add(ribbonStatusBar1);
			Controls.Add(ribbonControl);
			IsMdiContainer = true;
			MinimumSize = new Size(700, 380);
			Name = "FormMainNew";
			Ribbon = ribbonControl;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			StatusBar = ribbonStatusBar1;
			Text = "Simple.Objects™ Server Monitor";
			Activated += frmMain_Activated;
			Closing += frmMain_Closing;
			FormClosing += frmMain_FormClosing;
			Load += frmMain_Load;
			((System.ComponentModel.ISupportInitialize)repositoryItemSpinEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)popupControlContainer1).EndInit();
			((System.ComponentModel.ISupportInitialize)ribbonControl).EndInit();
			((System.ComponentModel.ISupportInitialize)pmAppMain).EndInit();
			((System.ComponentModel.ISupportInitialize)pccBottom).EndInit();
			pccBottom.ResumeLayout(false);
			pccBottom.PerformLayout();
			((System.ComponentModel.ISupportInitialize)imageCollection2).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemPictureEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)riicStyle).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)imageCollection1).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemTrackBar1).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemColorPickEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit2).EndInit();
			((System.ComponentModel.ISupportInitialize)pmNew).EndInit();
			((System.ComponentModel.ISupportInitialize)pcAppMenuFileLabels).EndInit();
			((System.ComponentModel.ISupportInitialize)pmMain).EndInit();
			((System.ComponentModel.ISupportInitialize)imageCollection3).EndInit();
			((System.ComponentModel.ISupportInitialize)backstageViewControl).EndInit();
			backstageViewControl.ResumeLayout(false);
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
			backstageViewClientControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)recentItemControl1).EndInit();
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
			((System.ComponentModel.ISupportInitialize)emptySpacePanel).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private DevExpress.XtraBars.BarButtonItem iSave;
		private DevExpress.XtraBars.BarButtonItem iSaveAs;
		private DevExpress.XtraBars.BarButtonItem iPaste;
		private DevExpress.XtraBars.BarButtonItem iFind;
		private DevExpress.XtraBars.BarButtonItem iReplace;
		private DevExpress.XtraBars.BarButtonItem iWeb;
		private DevExpress.XtraBars.BarButtonItem siPosition;
		private DevExpress.XtraBars.BarStaticItem siDocName;
		private DevExpress.XtraBars.PopupControlContainer popupControlContainer1;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
		private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
		private DevExpress.Utils.ImageCollection imageCollection1;
		private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
		private DevExpress.XtraBars.BarButtonGroup bgAlign;
		private DevExpress.XtraBars.BarButtonGroup bgFontStyle;
		private DevExpress.XtraBars.BarButtonGroup bgFont;
		private DevExpress.XtraBars.BarButtonGroup bgBullets;
		private DevExpress.XtraBars.BarButtonItem iPasteSpecial;
		private DevExpress.XtraBars.BarButtonItem iNew;
		private DevExpress.XtraBars.BarLargeButtonItem iLargeUndo;
		private DevExpress.XtraBars.BarButtonItem iTemplate;
		private DevExpress.XtraBars.PopupMenu pmNew;
		private DevExpress.XtraBars.PopupMenu pmMain;
		private DevExpress.XtraBars.BarSubItem barSubItemChangeSkin;
		private DevExpress.XtraBars.RibbonGalleryBarItem rgbiSkins;
		private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup10;
		private DevExpress.XtraBars.Ribbon.ApplicationMenu pmAppMain;
		private DevExpress.XtraBars.BarEditItem beiFontSize;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
		private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage3;
		private DevExpress.XtraBars.BarButtonItem bbiFontColorPopup;
		private DevExpress.XtraBars.BarButtonItem iAbout;
		private DevExpress.Utils.ImageCollection imageCollection2;
		private Office2007PopupControlContainer pccAppMenu;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.PanelControl pcAppMenuFileLabels;
		private DevExpress.Utils.ImageCollection imageCollection3;
		private BarEditItem barEditItem1;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
		private BarEditItem biStyle;
		private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riicStyle;
		private PopupControlContainer pccBottom;
		private DevExpress.XtraEditors.SimpleButton simpleButtonExit;
		private DevExpress.XtraBars.Ribbon.RibbonMiniToolbar selectionMiniToolbar;
		private BarButtonGroup editButtonGroup;
		private DevExpress.XtraBars.Ribbon.BackstageViewControl backstageViewControl;
		private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl1;
		private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl3;
		private DevExpress.XtraBars.Ribbon.BackstageViewButtonItem bvItemSettings;
		//private DevExpress.XtraBars.Ribbon.BackstageViewButtonItem bvItemClose;
		private DevExpress.XtraBars.Ribbon.BackstageViewButtonItem bvItemExit;
		private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl7;
		private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
		private DevExpress.Utils.Taskbar.TaskbarAssistant taskbarAssistant1;
		//private DevExpress.Utils.Taskbar.ThumbnailButton thumbButtonNewDoc;
		private DevExpress.XtraEditors.Repository.RepositoryItemTrackBar repositoryItemTrackBar1;
		private BarButtonItem bbColorMix;
		private System.ComponentModel.IContainer components;
		private BackstageViewClientControl backstageViewClientControl8;
		private BackstageViewTabItem backstageViewTabItem1;
		private BackstageViewTabItem backstageViewTabItem2;
		private BackstageViewClientControl backstageViewClientControl9;
		private BackstageViewTabItem backstageViewTabItem3;
		private BackstageViewClientControl backstageViewClientControl10;
		private BackstageViewTabItem backstageViewTabItem4;
		private RecentPinItem recentControlRecentItem1;
		private RecentPinItem recentControlRecentItem2;
		private RecentPinItem recentControlRecentItem4;
		private RecentPinItem recentControlRecentItem5;
		private RecentPinItem recentControlRecentItem6;
		private RecentPinItem recentControlRecentItem7;
		private RecentPinItem recentControlRecentItem8;
		private RecentPinItem recentControlRecentItem9;
		private RecentPinItem recentControlRecentItem10;
		private RecentButtonItem recentControlButtonItem1;
		private BackstageViewClientControl backstageViewClientControl11;
		private RecentItemControl recentControlPrint;
		private RecentControlItemControlContainer recentPrintOptionsContainer;
		private RecentControlItemControlContainer recentPrintPreviewContainer;
		private RecentControlContainerItem recentPrintOptions;
		private RecentControlContainerItem recentPrintPreview;
		private BackstageViewTabItem printTabItem;
		private BackstageViewTabItem bvTabPrint;
		//private DevExpress.XtraPrinting.Control.PrintControl printControl1;
		private BackstageViewClientControl backstageViewClientControl4;
		private PanelControl panelControl2;
		private PanelControl panelControl3;
		private DevExpress.XtraPrinting.Control.PrintControl printControl1;
		private ZoomTrackBarControl zoomTrackBarControl1;
		private ButtonEdit pageButtonEdit;
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
		//private RecentItemControl recentOpen;
		private RecentTabItem recentTabItem1;
		private RecentTabItem recentTabItem2;
		//private RecentItemControl recentSaveAs;
		private RecentTabItem recentTabItem3;
		private RecentItemControl recentControlExport;
		private RecentTabItem recentTabItem4;
		private BackstageViewClientControl backstageViewClientControl2;
		private RecentItemControl recentItemControl1;
		private RecentPinItem recentPinItem2;
		private RecentPinItem recentPinItem3;
		private BackstageViewTabItem backstageViewTabItem6;
		private RecentPinItem recentPinItem4;
		private RecentLabelItem recentLabelItem1;
		private RecentHyperlinkItem recentHyperlinkItem1;
		private RecentLabelItem recentLabelItem2;
		private DropDownButton ddbDuplex;
		private DropDownButton ddbOrientation;
		private DropDownButton ddbPaperSize;
		private DropDownButton ddbMargins;
		private DropDownButton ddbCollate;
		private BackstageViewLabel backstageViewLabel2;
		private LayoutControlItem layoutControlItem4;
		private LayoutControlItem layoutControlItem5;
		private LayoutControlItem layoutControlItem6;
		private LayoutControlItem layoutControlItem7;
		private LayoutControlItem layoutControlItem8;
		private LayoutControlItem layoutControlItem9;
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
		private DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit repositoryItemColorPickEdit1;
		private DevExpress.Utils.Layout.StackPanel stackPanel1;
		private PanelControl emptySpacePanel;
		private BackstageViewItemSeparator backstageViewItemSeparator1;
		private SkinPaletteRibbonGalleryBarItem skinPaletteRibbonGalleryBarItem1;
		private RecentLabelItem recentLabelItem3;
		private RecentLabelItem recentLabelItem4;
		private RecentLabelItem recentLabelItem5;
		private RecentLabelItem recentLabelItem6;
		private BackstageViewButtonItem backstageViewButtonItem1;
		private RibbonPage ribbonPageHome;
		private BarButtonItem barButtonItemConnect;
		private RibbonPageGroup ribbonPageGroupMonitorServer;
		private BarEditItem barEditItemMonitorServer;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
		private BarEditItem barEditItemServerMonitorPort;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit2;
		private BarEditItem barEditItemMonitorServerPort;
		private BarEditItem barEditItem2;
		private BarButtonItem barButtonItemDisconnect;
		private RibbonPageGroup ribbonPageGroupSimpleObjectsServer;
		private BarButtonItem barButtonItemServerStart;
		private BarButtonItem barButtonItemServerRestart;
		private BarButtonItem barButtonItemServerStop;
		private RibbonPageGroup ribbonPageGroupLookAndFeel;
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
		private BarButtonItem barButtonItemColorMix;
		private BarToggleSwitchItem barToggleSwitchItemDarkMode;
		private BarToggleSwitchItem barToggleSwitchItemCompactView;
		private BarStaticItem barStaticItemMonitorServer;
		private BarStaticItem barStaticItemUser;
		private BarStaticItem barStaticItemInfo;
		private RibbonPageGroup ribbonPageGroupHelp;
		private BarButtonItem barButtonItemSimpleObjectsOnTheWeb;
		private BarButtonItem barButtonItemOnlineHelp;
		private BarButtonItem barButtonItemGettingStarted;
		private BarButtonItem barButtonItemConntactUs;
		private BarButtonItem barButtonItemAbout;
		//private BarToggleSwitchItem barToggleSwitchItemAutoSave;
		private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
	}
}
