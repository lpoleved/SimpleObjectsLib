using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.ComponentModel;
using System.Reflection;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Simple;
using Simple.Collections;
using Simple.Serialization;
using Simple.Network;
using Simple.Objects;
using Simple.SocketEngine;
using Simple.Objects.MonitorProtocol;
using Simple.Controls;
using Simple.Objects.Controls;
using DevExpress.LookAndFeel;
//using DevExpress.Tutorials.Controls;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.Gallery;
using DevExpress.Tutorials.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.Dialogs;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data.Extensions;
using System.Text;

namespace Simple.Objects.ServerMonitor
{
    public partial class FormMain_OLD : SimpleRibbonFormBase //DevExpress.XtraBars.Ribbon.RibbonForm 
    {
		#region Private Members

		private const int MaxBestFitNodeCount = 100;
		//private int userSessionColumnIndexSessionId, userSessionColumnIndexServerAddress, userSessionColumnIndexClientAddress, userSessionColumnIndexStatus;
		private Dictionary<object, TreeListNode> transactionNodesByObjects = new Dictionary<object, TreeListNode>();
		private SimpleDictionary<string, int> userSessionKeysBySessionID = new SimpleDictionary<string, int>();
		private SimpleDictionary<long, string> userSessionIndexesBySessionKey = new SimpleDictionary<long, string>();
		private BindingList<SessionInfoRow> dataSourceUserSessions = new BindingList<SessionInfoRow>();
		private BindingList<PackageInfoRow> dataSourceRequestsResponses = new BindingList<PackageInfoRow>();
		private BindingList<TransactionInfoRow> dataSourceDatastoreTransactions = new BindingList<TransactionInfoRow>();
		private EditPanelFactory editPanelFactory = new EditPanelFactory();
		private int documentIndex = 0;
		private ColorPopup cp;
		private FormFind dlgFind = null;
		//private readonly object lockObject = new object();

		//frmReplace dlgReplace = null;
		private GalleryItem fCurrentFontItem, fCurrentColorItem;
		private string DocumentName { get { return string.Format("New Document {0}", documentIndex); } }

		private static SemaphoreSlim lockSesion = new SemaphoreSlim(1, 1);
		private static SemaphoreSlim lockRequestsResponses = new SemaphoreSlim(1, 1);
		private static SemaphoreSlim lockTransaction = new SemaphoreSlim(1, 1);

		//int splitControlTransactionLogWidth = 0;

		#endregion Private Members

		#region Constructor Initialization

		public FormMain_OLD()
		{
			InitializeComponent();
			
			this.AppContext = MonitorAppContext.Instance;
			//this.BarSubItemChangeSkin = this.barSubItemChangeSkin;

			//this.Ribbon.RibbonStyle = (RibbonControlStyle)MonitorAppContext.Instance.UserSettings.RibbonStyle;
			//this.Ribbon.ColorScheme = (RibbonControlColorScheme)MonitorAppContext.Instance.UserSettings.RibbonColorScheme;
			this.Ribbon.ItemPanelStyle = RibbonItemPanelStyle.Skin;
			this.Ribbon.ShowApplicationButton = DefaultBoolean.True; // Prevent locking int macOS ribbon style
			this.SetSkinStyle();
			base.Initialize();

			CreateColorPopup(popupControlContainer1);
			InitSkinGallery();
			InitFontGallery();
			InitColorGallery();
			InitEditors();
			InitSchemeCombo();

			this.siUser.Caption = MonitorAppContext.Instance.UserSettings.LastUsername;
			this.siServer.Caption = MonitorAppContext.Instance.UserSettings.Server;
			//this.barEditItemMonitorServer.EditValue = MonitorAppContext.Instance.UserSettings.GetValue<string>(SettingLastServer);
			//this.barEditItemMonitorServicePort.EditValue = MonitorAppContext.Instance.UserSettings.GetValue<int>(SettingServerPort, DefaultMonitorPort);


			this.iPaintStyle.Enabled = !this.AppContext.UserSettings.DarkMode; // Change Skin button

			UserLookAndFeel.Default.StyleChanged += new EventHandler(OnLookAndFeelStyleChanged);

			//UserLookAndFeel.Default.SetSkinStyle("Office 2010 Blue");

			this.tabControl.Dock = DockStyle.Fill;
			this.tabControl.Visible = true;
			this.tabControl.BringToFront();

			GridHelper.SetViewOnlyMode(this.gridViewUserSessions);
			this.gridControlUserSessions.DataSource = this.dataSourceUserSessions;
			this.gridViewUserSessions.RowStyle += GridViewUserSessions_RowStyle;

			GridHelper.SetSelectOnlyMode(this.gridViewRequestsResponses);
			this.gridControlRequestsResponses.DataSource = this.dataSourceRequestsResponses;
			this.gridViewRequestsResponses.FocusedRowChanged += GridViewRequestsResponses_FocusedRowChanged;
			this.groupPropertyPanelRequestsResponse.GetEditPanelType += this.editPanelFactory.GetPackageInfoRowEditPanelType;
			//this.groupPropertyPanelRequestsResponse.SetPanelObjectDefinition(typeof(PackageInfoRow), typeof(EditPanelSessionPackageJobAction));

			GridHelper.SetSelectOnlyMode(this.gridViewDatastoreTransactions);
			this.gridControlDatastoreTransactions.DataSource = this.dataSourceDatastoreTransactions;
			this.gridViewDatastoreTransactions.FocusedRowChanged += GridViewDatastoreTransactions_FocusedRowChanged;
			this.groupPropertyPanelTransactionLog.SetPanelObjectDefinition(typeof(TransactionInfoRow), typeof(EditPanelTransactionLog));


			// User Sessions
			//GridColumn gridColumn;

			//gridColumn = this.gridViewUserSessions.Columns.Add();
			//gridColumn.Name = gridColumn.FieldName = "SessionId";
			//gridColumn.Caption = "Session ID";
			//this.userSessionColumnIndexSessionId = gridColumn.AbsoluteIndex;
			//gridColumn.OptionsColumn.AllowEdit = false;
			//gridColumn.UnboundType = DevExpress.Data.UnboundColumnType.String;
			//gridColumn.VisibleIndex = this.gridViewUserSessions.Columns.Count;
			//gridColumn.Visible = true;

			//gridColumn = this.gridViewUserSessions.Columns.Add();
			//gridColumn.Name = gridColumn.FieldName = "ServerAddress";
			//gridColumn.Caption = "Server Address";
			//this.userSessionColumnIndexServerAddress = gridColumn.AbsoluteIndex;
			//gridColumn.OptionsColumn.AllowEdit = false;
			//gridColumn.UnboundType = DevExpress.Data.UnboundColumnType.String;
			//gridColumn.VisibleIndex = this.gridViewUserSessions.Columns.Count;
			//gridColumn.Visible = true;

			//gridColumn = this.gridViewUserSessions.Columns.Add();
			//gridColumn.Name = gridColumn.FieldName = "ClientAddress";
			//gridColumn.Caption = "Client Address";
			//this.userSessionColumnIndexClientAddress = gridColumn.AbsoluteIndex;
			//gridColumn.OptionsColumn.AllowEdit = false;
			//gridColumn.UnboundType = DevExpress.Data.UnboundColumnType.String;
			//gridColumn.VisibleIndex = this.gridViewUserSessions.Columns.Count;
			//gridColumn.Visible = true;

			//gridColumn = this.gridViewUserSessions.Columns.Add();
			//gridColumn.Name = gridColumn.FieldName = gridColumn.Caption = "Status";
			//this.userSessionColumnIndexStatus = gridColumn.AbsoluteIndex;
			//gridColumn.OptionsColumn.AllowEdit = false;
			//gridColumn.UnboundType = DevExpress.Data.UnboundColumnType.String;
			//gridColumn.VisibleIndex = this.gridViewUserSessions.Columns.Count;
			//gridColumn.Visible = true;

			//// Datastore Transactions
			//TreeListColumn treeColumn;

			//treeColumn = this.treeListDatastoreTransaction.Columns.Add();
			//treeColumn.Name = treeColumn.FieldName = treeColumn.Caption = "Name";
			//this.transactionColumnIndexName = treeColumn.AbsoluteIndex;
			//treeColumn.Visible = true;

			//treeColumn = this.treeListDatastoreTransaction.Columns.Add();
			//treeColumn.Name = treeColumn.FieldName = treeColumn.Caption = "Status";
			//this.transactionColumnIndexStatus = treeColumn.AbsoluteIndex;
			//treeColumn.Visible = true;

			//treeColumn = this.treeListDatastoreTransaction.Columns.Add();
			//treeColumn.Name = treeColumn.FieldName = treeColumn.Caption = "Properties";
			//this.transactionColumnIndexProperties = treeColumn.AbsoluteIndex;
			//treeColumn.Visible = true;

			//treeColumn = this.treeListDatastoreTransaction.Columns.Add();
			//treeColumn.Name = treeColumn.FieldName = "OldProperties";
			//treeColumn.Caption = "Old Properties";
			//this.transactionColumnIndexOldProperties = treeColumn.AbsoluteIndex;
			//treeColumn.Visible = true;

			//AdministratorGroupModel.Instance.GetHashCode();

			//string assemblyList = String.Empty;

			Program.MonitorClient.Closed += ClientServiceMonitor_Closed;
			//Program.MonitorClient.SessionAuthorized += AppClient_SessionAuthorized;
			//Program.MonitorClient.NewSessionConnected += ClientServiceMonitor_NewSessionConnected;
			//Program.MonitorClient.SessionClosed += ClientServiceMonitor_SessionClosed;
			//Program.MonitorClient.SessionAuthorized += this.AppClientMonitor_SessionAuthorized;
			//Program.MonitorClient.SessionPackageJobAction += ClientServiceMonitor_SessionPackageJobAction;
			//Program.MonitorClient.TransactionProcessed += ClientServiceMonitor_TransactionProcessed;

			this.InitializeAppClient();
			this.InitializeServiceControls(Program.MonitorClient.IsConnected);

			WindowsFormsSettings.CompactUIMode = (this.AppContext.UserSettings.CompactUI) ? DefaultBoolean.True : DefaultBoolean.False;


			//Task.Run(() =>
			//{
			//	if (Program.TryConnectToServerAndAuthorize(Program.MonitorClient).GetAwaiter().GetResult())
			//		this.LoadUserSessions();
			//});


			//Program.AppClient.Connect(Program.AppClient.RemoteEndPoint);

			//this.InitializeServiceControls(Program.AppClient.IsConnected);
		}

		protected override void OnStyleChanged(EventArgs e)
		{
			base.OnStyleChanged(e);

			this.Ribbon.ApplicationIcon = (this.Ribbon.RibbonStyle == RibbonControlStyle.Office2007) ? global::Simple.Objects.ServerMonitor.Properties.Resources.SimpleObjects_Large : null;
		}

		private void InitializeAppClient()
		{
			this.barEditItemMonitorServer.EditValue = IpHelper.GetIpAddressOrHostname(Program.MonitorClient.RemoteEndPoint);
			this.barEditItemMonitorServerPort.EditValue = IpHelper.GetPort(Program.MonitorClient.RemoteEndPoint);
			//this.InitializeServiceControls(Program.MonitorClient.IsConnected);

			//var getSessionsResult = Program.AppClient.GetSessions();

			//foreach (SessionInfo session in getSessionsResult.ResultValue)
			//	if (!this.userSessionIndexesBySessionId.ContainsKey(session.SessionId))
			//		this.ClientServiceMonitor_NewSessionConnected(session.SessionId, session.SessionKey, session.ServerAddress, session.ServerPort,
			//																							 session.ClientAddress, session.ClientPort, session.Username, connected: true);
			//this.gridViewUserSessions.BestFitColumns();
		}


	#endregion Constructor Initialization

		#region Init

	private void InitEditors()
		{
			riicStyle.Items.Add(new ImageComboBoxItem("Office 2007", RibbonControlStyle.Office2007, -1));
			riicStyle.Items.Add(new ImageComboBoxItem("Office 2010", RibbonControlStyle.Office2010, -1));
			riicStyle.Items.Add(new ImageComboBoxItem("Office 2013", RibbonControlStyle.Office2013, -1));
			riicStyle.Items.Add(new ImageComboBoxItem("Office Universal", RibbonControlStyle.OfficeUniversal, -1));
			riicStyle.Items.Add(new ImageComboBoxItem("Tablet Office", RibbonControlStyle.TabletOffice, -1));
			riicStyle.Items.Add(new ImageComboBoxItem("MacOffice", RibbonControlStyle.MacOffice, -1));
			biStyle.EditValue = ribbonControl1.RibbonStyle;
		}

		private void InitializeServiceControls(bool connected)
		{
			this.barEditItemMonitorServer.EditValue = MonitorAppContext.Instance.UserSettings.Server;
			this.barEditItemMonitorServerPort.EditValue = MonitorAppContext.Instance.UserSettings.Port.ToString();

			this.barButtonItemServerConnect.Enabled = !connected;
			this.barEditItemMonitorServer.Enabled = !connected;
			this.barEditItemMonitorServerPort.Enabled = !connected;
			this.barButtonItemServerDisconnect.Enabled = connected;

			this.barButtonItemServerStart.Enabled = connected;
			this.barButtonItemServerStop.Enabled = connected;
			this.barButtonItemServerRestart.Enabled = connected;

			this.siServer.Enabled = connected;
			//this.siServerStatus.Caption = (connected) ? "Connected to " + this.barEditItemMonitorServer.EditValue.ToString() + ":" + this.barEditItemMonitorServicePort.EditValue : "Disconnected";
			this.siServerStatus.Caption = (connected) ? "Connected" : "Disconnected";
		}

		private void frmMain_Activated(object sender, System.EventArgs e) {
            InitPaste();
		}

		public void UpdateText() {
            ribbonControl1.ApplicationCaption = "Provider Code Generator";
            //ribbonControl1.ApplicationDocumentCaption = CurrentDocName + (CurrentModified ? "*" : "");
            //siDocName.Caption = string.Format("  {0}", CurrentDocName);
        }
        void ChangeActiveForm() {
            UpdateText();
            //InitCurrentDocument(CurrentRichTextBox);
            //rtPad_SelectionChanged(CurrentRichTextBox, EventArgs.Empty);
            CloseFind();
        }
        private void xtraTabbedMdiManager1_FloatMDIChildActivated(object sender, EventArgs e) {
            ChangeActiveForm();
        }
        private void xtraTabbedMdiManager1_FloatMDIChildDeactivated(object sender, EventArgs e) {
            BeginInvoke(new MethodInvoker(ChangeActiveForm));
        }
        private void frmMain_MdiChildActivate(object sender, System.EventArgs e) {
            ChangeActiveForm();
        }
        private void rtPad_SelectionChanged(object sender, System.EventArgs e) {
            ShowHideFormatCategory();
            RichTextBox rtPad = sender as RichTextBox;
            InitFormat();
            int line = 0, col = 0;

            if(rtPad != null) {
                InitEdit(rtPad.SelectionLength > 0);
                line = rtPad.GetLineFromCharIndex(rtPad.SelectionStart) + 1;
                col = rtPad.SelectionStart + 1;
            }
            else {
                InitEdit(false);
            }
            siUser.Caption = string.Format("   Line: {0}  Position: {1}   ", line, col);
            CurrentFontChanged();
        }



		public void ShowHideFormatCategory()
		{
			RibbonPageCategory selectionCategory = Ribbon.PageCategories[0] as RibbonPageCategory;
			if (selectionCategory == null)
				return;
			//if(CurrentRichTextBox == null)
			//    selectionCategory.Visible = false;
			//else
			//    selectionCategory.Visible = CurrentRichTextBox.SelectionLength != 0;
			if (selectionCategory.Visible)
				Ribbon.SelectedPage = selectionCategory.Pages[0];
		}

		protected virtual void ShowSelectionMiniToolbar() {
            Point pt = Control.MousePosition;
            pt.Offset(0, -11);
            selectionMiniToolbar.Alignment = ContentAlignment.TopRight;
            selectionMiniToolbar.PopupMenu = null;
            selectionMiniToolbar.Show(pt);
        }
        void rtPad_TextChanged(object sender, System.EventArgs e) {
            //if(CurrentForm == null) return;
            //CurrentForm.Modified = true;
            //InitCurrentDocument(CurrentRichTextBox);
        }

        protected void InitFormat() {
            iBold.Enabled = SelectFont != null;
            iItalic.Enabled = SelectFont != null;
            iUnderline.Enabled = SelectFont != null;
            iFont.Enabled = SelectFont != null;
            iFontColor.Enabled = SelectFont != null;
            if(SelectFont != null) {
                iBold.Down = SelectFont.Bold;
                iItalic.Down = SelectFont.Italic;
                iUnderline.Down = SelectFont.Underline;
            }
			bool enabled = true;		//	CurrentRichTextBox != null;
            iProtected.Enabled = enabled;
            iBullets.Enabled = enabled;
            iAlignLeft.Enabled = enabled;
            iAlignRight.Enabled = enabled;
            iCenter.Enabled = enabled;
            rgbiFont.Enabled = enabled;
            rgbiFontColor.Enabled = enabled;
            ribbonPageGroup9.ShowCaptionButton = enabled;
            rpgFont.ShowCaptionButton = enabled;
            rpgFontColor.ShowCaptionButton = enabled;
            if(!enabled) ClearFormats();

			//if (CurrentRichTextBox != null)
			//{
   //             iProtected.Down = CurrentRichTextBox.SelectionProtected;
   //             iBullets.Down = CurrentRichTextBox.SelectionBullet;
   //             switch(CurrentRichTextBox.SelectionAlignment) {
   //                 case HorizontalAlignment.Left:
   //                     iAlignLeft.Down = true;
   //                     break;
   //                 case HorizontalAlignment.Center:
   //                     iCenter.Down = true;
   //                     break;
   //                 case HorizontalAlignment.Right:
   //                     iAlignRight.Down = true;
   //                     break;
   //             }
   //         }
        }

        void ClearFormats() {
            iBold.Down = false;
            iItalic.Down = false;
            iUnderline.Down = false;
            iProtected.Down = false;
            iBullets.Down = false;
            iAlignLeft.Down = false;
            iAlignRight.Down = false;
            iCenter.Down = false;
        }

        protected void InitPaste() {
            //bool enabledPase = CurrentRichTextBox != null && CurrentRichTextBox.CanPaste(DataFormats.GetFormat(0));
            //iPaste.Enabled = enabledPase;
            //sbiPaste.Enabled = enabledPase;
        }

        void InitUndo() {
            //iUndo.Enabled = CurrentRichTextBox != null ? CurrentRichTextBox.CanUndo : false;
            iLargeUndo.Enabled = iUndo.Enabled;
        }
        protected void InitEdit(bool enabled) {
            iCut.Enabled = enabled;
            iCopy.Enabled = enabled;
            iClear.Enabled = enabled;
            //iSelectAll.Enabled = CurrentRichTextBox != null ? CurrentRichTextBox.CanSelect : false;
            InitUndo();
        }

        void InitNewDocument(RichTextBox rtbControl) {
            rtbControl.SelectionChanged += new System.EventHandler(this.rtPad_SelectionChanged);
            rtbControl.TextChanged += new System.EventHandler(this.rtPad_TextChanged);
        }

        void InitCurrentDocument(RichTextBox rtbControl) {
            bool enabled = rtbControl != null;
            iSaveAs.Enabled = enabled;
            iClose.Enabled = enabled;
            iPrint.Enabled = enabled;
            sbiSave.Enabled = enabled;
            sbiFind.Enabled = enabled;
            iFind.Enabled = enabled;
            iReplace.Enabled = enabled;
            //iSave.Enabled = CurrentModified;
            SetModifiedCaption();
            InitPaste();
            InitFormat();
        }

        void SetModifiedCaption() {
            //if(CurrentForm == null) {
            //    siModified.Caption = "";
            //    return;
            //}
            //siModified.Caption = CurrentModified ? "   Modified   " : "";
        }
		#endregion

		#region Properties

		//frmPad CurrentForm {
		//    get {
		//        if(this.ActiveMdiChild == null) return null;
		//        if(xtraTabbedMdiManager1.ActiveFloatForm != null)
		//            return xtraTabbedMdiManager1.ActiveFloatForm as frmPad;
		//        return this.ActiveMdiChild as frmPad;
		//    }
		//}

		//public RichTextBox CurrentRichTextBox {
		//    get {
		//        if(CurrentForm == null) return null;
		//        return CurrentForm.RTBMain;
		//    }
		//}

		//string CurrentDocName {
		//    get {
		//        if(CurrentForm == null) return "";
		//        return CurrentForm.DocName;
		//    }
		//}

		//bool CurrentModified {
		//    get {
		//        if(CurrentForm == null) return false;
		//        return CurrentForm.Modified;
		//    }
		//}

		FormPad CurrentForm
		{
			get
			{
				if (this.ActiveMdiChild == null)
					return null;
				if (xtraTabbedMdiManager1.ActiveFloatForm != null)
					return xtraTabbedMdiManager1.ActiveFloatForm as FormPad;
				return this.ActiveMdiChild as FormPad;
			}
		}

		public RichTextBox CurrentRichTextBox
		{
			get
			{
				if (CurrentForm == null)
					return null;
				return CurrentForm.RTBMain;
			}
		}

		#endregion

		#region File


		//void CreateNewDocument() {
		//    CreateNewDocument(null);
		//}

		//void CreateNewDocument(string fileName) {
		//    documentIndex++;
		//    frmPad pad = new frmPad();
		//    if(fileName != null)
		//        pad.LoadDocument(fileName);
		//    else
		//        pad.DocName = DocumentName;
		//    pad.MdiParent = this;
		//    pad.Closed += new EventHandler(Pad_Closed);
		//    pad.ShowPopupMenu += new EventHandler(Pad_ShowPopupMenu);
		//    pad.ShowMiniToolbar += new EventHandler(pad_ShowMiniToolbar);
		//    pad.Show();
		//    InitNewDocument(pad.RTBMain);
		//}

		//void idNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
		//    CreateNewDocument();
		//}

		//void iClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
		//    if(CurrentForm != null) CurrentForm.Close();
		//}

		//void OpenFile() {
		//    OpenFileDialog dlg = new OpenFileDialog();
		//    dlg.Filter = "Rich Text Files (*.rtf)|*.rtf";
		//    dlg.Title = "Open";
		//    if(dlg.ShowDialog() == DialogResult.OK) {
		//        OpenFile(dlg.FileName);
		//    }
		//}

		//public void OpenFile(string name) {
		//    CreateNewDocument(name);
		//    AddToMostRecentFiles(name, arMRUList);
		//    AddToMostRecentFiles(name, recentItemsControl1.MRUFileList);
		//    AddToMostRecentFolders(name, recentItemsControl1.MRUFolderList);
		//}
		//private void AddToMostRecentFiles(string name, MRUArrayList arMRUList) {
		//    arMRUList.InsertElement(name);
		//}
		//private void AddToMostRecentFolders(string name, MRUArrayList arMRUList) {
		//    name = Path.GetFullPath(name);
		//    arMRUList.InsertElement(Path.GetDirectoryName(name));
		//}

		//void iOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
		//    OpenFile();
		//}

		//private void iPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
		//    XtraMessageBox.Show(this, "Note that you can use the XtraPrinting Library to print the contents of the standard RichTextBox control.\r\nFor more information, see the main XtraPrinting demo.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
		//}

		//void iSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
		//    Save();
		//}
		//void iSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
		//    SaveAs();
		//}
		//void Save() {
		//    if(CurrentForm == null) return;
		//    if(CurrentForm.NewDocument) {
		//        SaveAs();
		//    }
		//    else {
		//        CurrentRichTextBox.SaveFile(CurrentDocName, RichTextBoxStreamType.RichText);
		//        CurrentForm.Modified = false;
		//    }
		//    SetModifiedCaption();
		//}
		//void SaveAs() {
		//    if(CurrentForm != null) {
		//        string s = CurrentForm.SaveAs();
		//        if(s != string.Empty) {
		//            AddToMostRecentFiles(s, arMRUList);
		//            AddToMostRecentFiles(s, recentItemsControl1.MRUFileList);
		//            AddToMostRecentFolders(s, recentItemsControl1.MRUFolderList);
		//        }
		//        UpdateText();
		//    }
		//}

		private void iExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Close();
		}

		private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{

		}

		private void ribbonPageGroup1_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e)
		{
			//OpenFile();
		}

		//private void ribbonPageGroup9_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e) {
		//    SaveAs();
		//}
		#endregion

		#region Format

		private void GridViewUserSessions_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
		{
			if (e.RowHandle >= 0 && !(this.gridViewUserSessions.GetRow(e.RowHandle) as SessionInfoRow).Connected)
				e.Appearance.ForeColor = Color.Gray;
		}

		private FontStyle rtPadFontStyle() {
            FontStyle fs = new FontStyle();
            if(iBold.Down) fs |= FontStyle.Bold;
            if(iItalic.Down) fs |= FontStyle.Italic;
            if(iUnderline.Down) fs |= FontStyle.Underline;
            return fs;
        }

        private void iBullets_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //if(CurrentRichTextBox == null) return;
            //CurrentRichTextBox.SelectionBullet = iBullets.Down;
            InitUndo();
        }

        private void iFontStyle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //if(CurrentRichTextBox == null) return;
            //CurrentRichTextBox.SelectionFont = new Font(SelectFont, rtPadFontStyle());
        }

        private void iProtected_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //if(CurrentRichTextBox == null) return;
            //CurrentRichTextBox.SelectionProtected = iProtected.Down;
        }

        private void iAlign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //if(CurrentRichTextBox == null) return;
            //if(iAlignLeft.Down)
            //    CurrentRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
            //if(iCenter.Down)
            //    CurrentRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
            //if(iAlignRight.Down)
            //    CurrentRichTextBox.SelectionAlignment = HorizontalAlignment.Right;
            InitUndo();
        }


        protected Font SelectFont {
            get {
                //if(CurrentRichTextBox != null)
                //    return CurrentRichTextBox.SelectionFont;
                return null;
            }
        }
        void ShowFontDialog() {
            //if(CurrentRichTextBox == null) return;
            Font dialogFont = null;
            if(SelectFont != null)
                dialogFont = (Font)SelectFont.Clone();
            //else dialogFont = CurrentRichTextBox.Font;
            XtraFontDialog dlg = new XtraFontDialog(dialogFont);
            if(dlg.ShowDialog() == DialogResult.OK) {
                //CurrentRichTextBox.SelectionFont = dlg.ResultFont;
                beiFontSize.EditValue = dlg.ResultFont.Size;
            }
        }
        private void iFont_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ShowFontDialog();
        }
        private void iFontColor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //if(CurrentRichTextBox == null) return;
            //CurrentRichTextBox.SelectionColor = cp.ResultColor;
        }
        #endregion

        #region Edit
        private void iUndo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //if(CurrentRichTextBox == null) return;
            //CurrentRichTextBox.Undo();
            //CurrentForm.Modified = CurrentRichTextBox.CanUndo;
            SetModifiedCaption();
            InitUndo();
            InitFormat();
        }

        private void iCut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //if(CurrentRichTextBox == null) return;
            //CurrentRichTextBox.Cut();
            InitPaste();
        }

        private void iCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //if(CurrentRichTextBox == null) return;
            //CurrentRichTextBox.Copy();
            InitPaste();
        }

        private void iPaste_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //if(CurrentRichTextBox == null) return;
            //CurrentRichTextBox.Paste();
        }

        private void iClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //if(CurrentRichTextBox == null) return;
            //CurrentRichTextBox.SelectedRtf = "";
        }

        private void iSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //if(CurrentRichTextBox == null) return;
            //CurrentRichTextBox.SelectAll();
        }
        private void ribbonPageGroup2_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e) {
            pmMain.ShowPopup(ribbonControl1.Manager, MousePosition);
        }
        #endregion

        #region SkinGallery
        void InitSkinGallery() {
            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(rgbiSkins, true);
        }
        #endregion

        #region FontGallery
        Image GetFontImage(int width, int height, string fontName, int fontSize) {
            Rectangle rect = new Rectangle(0, 0, width, height);
            Image fontImage = new Bitmap(width, height);
            try {
                using(Font fontSample = new Font(fontName, fontSize)) {
                    Graphics g = Graphics.FromImage(fontImage);
                    g.FillRectangle(Brushes.White, rect);
                    using(StringFormat fs = new StringFormat()) {
                        fs.Alignment = StringAlignment.Center;
                        fs.LineAlignment = StringAlignment.Center;
                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        g.DrawString("Aa", fontSample, Brushes.Black, rect, fs);
                        g.Dispose();
                    }
                }
            }
            catch { }
            return fontImage;
        }
        void InitFont(GalleryItemGroup groupDropDown, GalleryItemGroup galleryGroup) {
            FontFamily[] fonts = FontFamily.Families;
            for(int i = 0; i < fonts.Length; i++) {
                if(!FontFamily.Families[i].IsStyleAvailable(FontStyle.Regular)) continue;
                string fontName = fonts[i].Name;
                GalleryItem item = new GalleryItem();
                item.Caption = fontName;
                item.Image = GetFontImage(32, 28, fontName, 12);
                item.HoverImage = item.Image;
                item.Description = fontName;
                item.Hint = fontName;
                try {
                    item.Tag = new Font(fontName, 9);
                    if(DevExpress.Utils.ControlUtils.IsSymbolFont((Font)item.Tag)) {
                        item.Tag = new Font(DevExpress.Utils.AppearanceObject.DefaultFont.FontFamily, 9);
                        item.Description += " (Symbol Font)";
                    }
                }
                catch {
                    continue;
                }
                groupDropDown.Items.Add(item);
                galleryGroup.Items.Add(item);
            }
        }
        void InitFontGallery() {
            gddFont.Gallery.BeginUpdate();
            rgbiFont.Gallery.BeginUpdate();
            try
            {
                InitFont(gddFont.Gallery.Groups[0], rgbiFont.Gallery.Groups[0]);
            }
            finally {
                gddFont.Gallery.EndUpdate();
                rgbiFont.Gallery.EndUpdate();
            }
            beiFontSize.EditValue = 8;
        }
        void SetFont(string fontName, GalleryItem item) {
            //if(CurrentRichTextBox == null) return;
            //CurrentRichTextBox.SelectionFont = new Font(fontName, Convert.ToInt32(beiFontSize.EditValue), rtPadFontStyle());
            if(item != null) CurrentFontItem = item;
        }
        private void gddFont_Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e) {
            SetFont(e.Item.Caption, e.Item);
        }
        private void rpgFont_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e) {
            ShowFontDialog();
        }
        private void rgbiFont_Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e) {
            SetFont(e.Item.Caption, e.Item);
        }
        private void gddFont_Gallery_CustomDrawItemText(object sender, GalleryItemCustomDrawEventArgs e) {
            DevExpress.XtraBars.Ribbon.ViewInfo.GalleryItemViewInfo itemInfo = e.ItemInfo as DevExpress.XtraBars.Ribbon.ViewInfo.GalleryItemViewInfo;
            itemInfo.PaintAppearance.ItemDescriptionAppearance.Normal.DrawString(e.Cache, e.Item.Description, itemInfo.DescriptionBounds);
            AppearanceObject app = itemInfo.PaintAppearance.ItemCaptionAppearance.Normal.Clone() as AppearanceObject;
            app.Font = (Font)e.Item.Tag;
            try {
                e.Cache.Graphics.DrawString(e.Item.Caption, app.Font, app.GetForeBrush(e.Cache), itemInfo.CaptionBounds);
            }
            catch { }
            e.Handled = true;
        }
        #endregion

        #region ColorGallery
        void InitColorGallery() {
            gddFontColor.BeginUpdate();
            foreach(Color color in DevExpress.XtraEditors.Popup.ColorListBoxViewInfo.WebColors) {
                if(color == Color.Transparent) continue;
                GalleryItem item = new GalleryItem();
                item.Caption = color.Name;
                item.Tag = color;
                item.Hint = color.Name;
                gddFontColor.Gallery.Groups[0].Items.Add(item);
                rgbiFontColor.Gallery.Groups[0].Items.Add(item);
            }
            foreach(Color color in DevExpress.XtraEditors.Popup.ColorListBoxViewInfo.SystemColors) {
                GalleryItem item = new GalleryItem();
                item.Caption = color.Name;
                item.Tag = color;
                gddFontColor.Gallery.Groups[1].Items.Add(item);
            }
            gddFontColor.EndUpdate();
        }
        private void gddFontColor_Gallery_CustomDrawItemImage(object sender, GalleryItemCustomDrawEventArgs e) {
            Color clr = (Color)e.Item.Tag;
            using(Brush brush = new SolidBrush(clr)) {
                e.Cache.FillRectangle(brush, e.Bounds);
                e.Handled = true;
            }
        }
        void SetResultColor(Color color, GalleryItem item) {
            //if(CurrentRichTextBox == null) return;
            cp.ResultColor = color;
            //CurrentRichTextBox.SelectionColor = cp.ResultColor;
            if(item != null) CurrentColorItem = item;
        }
        private void gddFontColor_Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e) {
            SetResultColor((Color)e.Item.Tag, e.Item);
        }
        private void rpgFontColor_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e) {
            //if(CurrentRichTextBox == null) return;
            if(cp == null)
                CreateColorPopup(popupControlContainer1);
            popupControlContainer1.ShowPopup(ribbonControl1.Manager, MousePosition);
        }

        private void rgbiFontColor_Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e) {
            SetResultColor((Color)e.Item.Tag, e.Item);
        }
		#endregion

        #region GalleryItemsChecked

        GalleryItem GetColorItemByColor(Color color, BaseGallery gallery) {
            foreach(GalleryItemGroup galleryGroup in gallery.Groups)
                foreach(GalleryItem item in galleryGroup.Items)
                    if(item.Caption == color.Name)
                        return item;
            return null;
        }
        GalleryItem GetFontItemByFont(string fontName, BaseGallery gallery) {
            foreach(GalleryItemGroup galleryGroup in gallery.Groups)
                foreach(GalleryItem item in galleryGroup.Items)
                    if(item.Caption == fontName)
                        return item;
            return null;
        }
        GalleryItem CurrentFontItem {
            get { return fCurrentFontItem; }
            set {
                if(fCurrentFontItem == value) return;
                if(fCurrentFontItem != null) fCurrentFontItem.Checked = false;
                fCurrentFontItem = value;
                if(fCurrentFontItem != null) {
                    fCurrentFontItem.Checked = true;
                    MakeFontVisible(fCurrentFontItem);
                }
            }
        }
        void MakeFontVisible(GalleryItem item) {
            gddFont.Gallery.MakeVisible(fCurrentFontItem);
            rgbiFont.Gallery.MakeVisible(fCurrentFontItem);
        }
        GalleryItem CurrentColorItem {
            get { return fCurrentColorItem; }
            set {
                if(fCurrentColorItem == value) return;
                if(fCurrentColorItem != null) fCurrentColorItem.Checked = false;
                fCurrentColorItem = value;
                if(fCurrentColorItem != null) {
                    fCurrentColorItem.Checked = true;
                    MakeColorVisible(fCurrentColorItem);
                }
            }
        }
        void MakeColorVisible(GalleryItem item) {
            gddFontColor.Gallery.MakeVisible(fCurrentColorItem);
            rgbiFontColor.Gallery.MakeVisible(fCurrentColorItem);
        }
        void CurrentFontChanged() {
            //if(CurrentRichTextBox == null || CurrentRichTextBox.SelectionFont == null) return;
            //CurrentFontItem = GetFontItemByFont(CurrentRichTextBox.SelectionFont.Name, rgbiFont.Gallery);
            //CurrentColorItem = GetColorItemByColor(CurrentRichTextBox.SelectionColor, rgbiFontColor.Gallery);
        }
        private void gddFont_Popup(object sender, System.EventArgs e) {
            MakeFontVisible(CurrentFontItem);
            //if(CurrentRichTextBox == null) return;
            //beiFontSize.EditValue = CurrentRichTextBox.SelectionFont.Size;
        }

        private void gddFontColor_Popup(object sender, System.EventArgs e) {
            MakeColorVisible(CurrentColorItem);
        }
        #endregion
       
		#region MostRecentFiles
        //MRUArrayList arMRUList = null;
        
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
            //SaveMostRecentFiles(arMRUList, Application.StartupPath + "\\" + MRUArrayList.MRUFileName);
            //SaveMostRecentFiles(recentItemsControl1.MRUFolderList, Application.StartupPath + "\\" + MRUArrayList.MRUFolderName);
        }
		//void InitMostRecentFiles(MRUArrayList arList) {
		//    string fileName = Application.StartupPath + "\\" + MRUArrayList.MRUFileName;
		//    string folderName = Application.StartupPath + "\\" + MRUArrayList.MRUFolderName;
		//    arMRUList.Init(fileName, "Document1.rtf");
		//    //recentItemsControl1.MRUFileList.Init(fileName, "Document1.rtf");
		//    //recentItemsControl1.MRUFolderList.Init(folderName, Application.StartupPath);
		//}

		//void SaveMostRecentFiles(MRUArrayList arList, string fileName) {
		//    try {
		//        System.IO.StreamWriter sw = System.IO.File.CreateText(fileName);
		//        for(int i = arList.Count - 1; i >= 0; i--) sw.WriteLine(string.Format("{0},{1}", arList[i].ToString(), arList.GetLabelChecked(arList[i].ToString())));
		//        sw.Close();
		//    }
		//    catch { }
		//}
		//void OnMRUFileLabelClicked(object sender, EventArgs e) {
		//    ribbonControl1.DeactivateKeyboardNavigation();
		//    pmAppMain.HidePopup();
		//    this.Refresh();
		//    //OpenFile(sender.ToString());
		//}

		#endregion

		#region Miscellaneous Methods

		private void OnLookAndFeelStyleChanged(object sender, EventArgs e)
		{
			UpdateSchemeCombo();
		}

		private void InitSchemeCombo()
		{
			foreach (object obj in Enum.GetValues(typeof(RibbonControlColorScheme)))
			{
				repositoryItemComboBox1.Items.Add(obj);
			}
			beScheme.EditValue = RibbonControlColorScheme.Yellow;
		}

		private void pad_ShowMiniToolbar(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(((RichTextBox)sender).SelectedText))
				return;
			ShowSelectionMiniToolbar();
		}

		private void Pad_Closed(object sender, EventArgs e)
		{
			//CloseFind();
		}

		private void Pad_ShowPopupMenu(object sender, EventArgs e)
		{
			pmMain.RibbonToolbar = selectionMiniToolbar;
			pmMain.ShowPopup(Control.MousePosition);
		}

		private void CloseFind()
		{
			if (dlgFind != null) // && dlgFind.RichText != CurrentRichTextBox)
			{
				dlgFind.Close();
				dlgFind = null;
			}
			//if (dlgReplace != null && dlgReplace.RichText != CurrentRichTextBox)
			//{
			//	dlgReplace.Close();
			//	dlgReplace = null;
			//}
		}

		private void CreateColorPopup(PopupControlContainer container)
		{
			cp = new ColorPopup(container, iFontColor, this);
		}

		private void iFind_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//if (CurrentRichTextBox == null)
			//	return;
			//if (dlgReplace != null)
			//	dlgReplace.Close();
			if (dlgFind != null)
				dlgFind.Close();
			dlgFind = new FormFind(null, Bounds); // frmFind(CurrentRichTextBox, Bounds);
			AddOwnedForm(dlgFind);
			dlgFind.Show();
		}

		//private void iReplace_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		//{
		//	if (CurrentRichTextBox == null)
		//		return;
		//	if (dlgReplace != null)
		//		dlgReplace.Close();
		//	if (dlgFind != null)
		//		dlgFind.Close();
		//	dlgReplace = new frmReplace(CurrentRichTextBox, Bounds);
		//	AddOwnedForm(dlgReplace);
		//	dlgReplace.Show();
		//}

		private void iWeb_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Process process = new Process();
			process.StartInfo.FileName = "http://www.netakod-community.com";
			process.StartInfo.Verb = "Open";
			process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
			process.Start();
		}

		private void iAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//DevExpress.Utils.About.frmAbout dlg = new DevExpress.Utils.About.frmAbout("Ribbon Demo for the XtraBars by Developer Express Inc.");
			//dlg.ShowDialog();
		}

		private string TextByCaption(string caption)
		{
			return caption.Replace("&", "");
		}

		private void frmMain_Load(object sender, System.EventArgs e)
		{
			//arMRUList = new MRUArrayList(pcAppMenuFileLabels, imageCollection3.Images[0], imageCollection3.Images[1]);
			//arMRUList.LabelClicked += new EventHandler(OnMRUFileLabelClicked);
			//InitMostRecentFiles(arMRUList);
			//InitMostRecentFiles(recentItemsControl1.MRUFileList);
			ribbonControl1.ForceInitialize();
			GalleryDropDown skins = new GalleryDropDown();
			skins.Ribbon = ribbonControl1;
			DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGalleryDropDown(skins);
			iPaintStyle.DropDownControl = skins;
			//CreateNewDocument();
		}

		private void ribbonControl1_ApplicationButtonDoubleClick(object sender, EventArgs e) {
            if(ribbonControl1.RibbonStyle == RibbonControlStyle.Office2007)
                this.Close();
        }

        private void barEditItem1_ItemPress(object sender, ItemClickEventArgs e) {
            System.Diagnostics.Process.Start("http://www.netakod-community.com");
        }

        private void biStyle_EditValueChanged(object sender, EventArgs e) {
            RibbonControlStyle style = (RibbonControlStyle)biStyle.EditValue;
            ribbonControl1.RibbonStyle = style;
            if (style == RibbonControlStyle.Office2010 ||
                style == RibbonControlStyle.MacOffice)
            {
                ribbonControl1.ApplicationButtonDropDownControl = this.backstageViewControl1;
                //this.ribbonControl1.ApplicationIcon = null;
            }
            else
            {
                ribbonControl1.ApplicationButtonDropDownControl = pmAppMain;
                //this.ribbonControl1.ApplicationIcon = global::Simple.CodeGenerator.Properties.Resources.SimpleObjects;
            }

			UpdateSchemeCombo();

			this.AppContext.UserSettings.RibbonStyle = (int)style;
			this.AppContext.UserSettings.Save();
			this.SetSkinStyle(style);
			this.OnStyleChanged(null);
		}

		private void UpdateSchemeCombo() {
            if(ribbonControl1.RibbonStyle == RibbonControlStyle.MacOffice ||
                ribbonControl1.RibbonStyle == RibbonControlStyle.Office2010) {
                beScheme.Visibility = UserLookAndFeel.Default.ActiveSkinName.Contains("Office 2010") ? BarItemVisibility.Always : BarItemVisibility.Never;
            }
            else {
                beScheme.Visibility = BarItemVisibility.Never;
            }
        }

        private void sbExit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void beiFontSize_EditValueChanged(object sender, EventArgs e) {
            //if(CurrentRichTextBox == null) return;
            //CurrentRichTextBox.SelectionFont = new Font(CurrentRichTextBox.SelectionFont.FontFamily, Convert.ToSingle(beiFontSize.EditValue), CurrentRichTextBox.SelectionFont.Style);
        }

		//private void bvTabPrint_SelectedChanged(object sender, BackstageViewItemEventArgs e) {
		//    if(e.Item == bvTabPrint) {
		//        this.printControl1.RtfText = CurrentRichTextBox != null? CurrentRichTextBox.Rtf: "";
		//    }
		//}

		//private void ribbonControl1_BeforeApplicationButtonContentControlShow(object sender, EventArgs e)
		//{
		//	this.printControl1.RtfText = CurrentRichTextBox == null ? "" : CurrentRichTextBox.Rtf;
		//	this.exportControl1.RtfText = CurrentRichTextBox == null ? "" : CurrentRichTextBox.Rtf;
		//}

		private void bvItemSave_ItemClick(object sender, BackstageViewItemEventArgs e) {
            //Save();
        }

        private void bvItemSaveAs_ItemClick(object sender, BackstageViewItemEventArgs e) {
            //SaveAs();
        }

        private void bvItemOpen_ItemClick(object sender, BackstageViewItemEventArgs e) {
            //OpenFile();
        }

        private void bvItemClose_ItemClick(object sender, BackstageViewItemEventArgs e) {
            if(xtraTabbedMdiManager1.SelectedPage != null) 
                xtraTabbedMdiManager1.SelectedPage.MdiChild.Close();
        }

        private void bvItemExit_ItemClick(object sender, BackstageViewItemEventArgs e) {
            Close();
        }

		private void beScheme_EditValueChanged(object sender, EventArgs e) {
            ribbonControl1.ColorScheme = ((RibbonControlColorScheme)beScheme.EditValue);

			this.AppContext.UserSettings.RibbonColorScheme = (int)ribbonControl1.ColorScheme;
			this.SetSkinStyle();
			this.OnStyleChanged(new EventArgs());
		}

		//private async void barButtonItemServerConnect_ItemClick(object sender, ItemClickEventArgs e)
		//{
		//	IPAddress monitorServerIpAddress = DnsHelper.GetIpAddressFromHost(this.barEditItemMonitorServer.EditValue.ToString());
		//	int monitorServicePort = Conversion.TryChangeType<int>(this.barEditItemMonitorServicePort.EditValue);

		//	await Program.ServiceMonitorClient.ConnectAsync(new IPEndPoint(monitorServerIpAddress, monitorServicePort));
		//}

		private void barButtonItemServerConnect_ItemClick(object sender, ItemClickEventArgs e)
		{
			MonitorAppContext.Instance.UserSettings.Server = (string)this.barEditItemMonitorServer.EditValue;
			MonitorAppContext.Instance.UserSettings.Port = Conversion.TryChangeType<int>(this.barEditItemMonitorServerPort.EditValue);

			Task.Run(() =>
			{
				if (Program.TryConnectToServerAndAuthorize(Program.MonitorClient).GetAwaiter().GetResult())
					this.LoadUserSessions();
			});
		}

		private void barButtonItemServerDisconnect_ItemClick(object sender, ItemClickEventArgs e)
		{
			Program.MonitorClient.CloseAsync().GetAwaiter().GetResult();
		}



		//private void GroupPropertyPanelRequestsResponse_GetEditPanel(object bindingObject, out SystemEditPanel propertyPanelType)
		//{
		//	if (bindingObject != null)
		//	{
		//		PackageInfoRow packageInfoRow = bindingObject as PackageInfoRow;

		//		if (packageInfoRow.SessionPackageJobAction.MessageRequestType == Simple.SocketEngine.MessageRequestType.RequestRecievedResponseSent)
		//		{
		//			this.tabPageMessage.PageVisible = false;
		//			this.tabPageRequest.PageVisible = true;
		//			this.tabPageResponse.PageVisible = true;

		//			this.editorRequestFlags.Text = packageInfoRow.SessionPackageJobAction.ReceivedPackage.Flags.Value.ToString("X4");
		//			this.editorRequestLength.Text = packageInfoRow.SessionPackageJobAction.ReceivedPackage.Reader.BaseStream.Length.ToString() + " byte(s)";

		//			this.editorResponseFlags.Text = packageInfoRow.SessionPackageJobAction.SentPackage.Flags.Value.ToString("X4");
		//			this.editorResponseLength.Text = packageInfoRow.SessionPackageJobAction.SentPackage.Reader.BaseStream.Length.ToString() + " byte(s)";
		//		}
		//		else if (packageInfoRow.SessionPackageJobAction.MessageRequestType == Simple.SocketEngine.MessageRequestType.MessageSent)
		//		{
		//			this.tabPageMessage.PageVisible = true;
		//			this.tabPageRequest.PageVisible = false;
		//			this.tabPageResponse.PageVisible = false;

		//			this.editorMessageFlags.Text = packageInfoRow.SessionPackageJobAction.SentPackage.Flags.Value.ToString("X4");
		//			this.editorMessageLength.Text = packageInfoRow.SessionPackageJobAction.SentPackage.Reader.BaseStream.Length.ToString() + " byte(s)";
		//		}
		//		else if (packageInfoRow.SessionPackageJobAction.MessageRequestType == Simple.SocketEngine.MessageRequestType.MessageReceived)
		//		{
		//			this.tabPageMessage.PageVisible = true;
		//			this.tabPageRequest.PageVisible = false;
		//			this.tabPageResponse.PageVisible = false;

		//			this.editorMessageFlags.Text = packageInfoRow.SessionPackageJobAction.ReceivedPackage.Flags.Value.ToString("X4");
		//			this.editorMessageLength.Text = packageInfoRow.SessionPackageJobAction.ReceivedPackage.Reader.BaseStream.Length.ToString() + " byte(s)";
		//		}
		//		else if (packageInfoRow.SessionPackageJobAction.MessageRequestType == Simple.SocketEngine.MessageRequestType.RequestSentResponseReceived)
		//		{
		//			this.tabPageMessage.PageVisible = false;
		//			this.tabPageRequest.PageVisible = true;
		//			this.tabPageResponse.PageVisible = true;

		//			this.editorRequestFlags.Text = packageInfoRow.SessionPackageJobAction.SentPackage.Flags.Value.ToString("X4");
		//			this.editorRequestLength.Text = packageInfoRow.SessionPackageJobAction.SentPackage.Reader.BaseStream.Length.ToString() + " byte(s)";

		//			this.editorResponseFlags.Text = packageInfoRow.SessionPackageJobAction.ReceivedPackage.Flags.Value.ToString("X4");
		//			this.editorResponseLength.Text = packageInfoRow.SessionPackageJobAction.ReceivedPackage.Reader.BaseStream.Length.ToString() + " byte(s)";
		//		}
		//	}
		//}

		//     private void barButtonItemGenerateProviderModelClass_ItemClick(object sender, ItemClickEventArgs e)
		//     {
		//         this.CreateNewDocument("ProviderModel.Generated.cs");
		//this.CurrentRichTextBox.Text = String.Empty;
		//Simple.Providers.ProviderModel.GenerateProviderModelClass(lineText => this.CurrentRichTextBox.AppendText(String.Format("{0}\r\n", lineText)));
		//     }
		#endregion Miscellaneous Methods

		#region |   Protected Overrides   |

		protected override void OnSkinNameChange(string skinName, string oldSkinName)
		{
			base.OnSkinNameChange(skinName, oldSkinName);

			if (!MonitorAppContext.Instance.UserSettings.DarkMode)
			{
				MonitorAppContext.Instance.UserSettings.RibbonSkinName = skinName;
				MonitorAppContext.Instance.UserSettings.Save();
			}
		}

		#endregion |   Protected Overrides   |

		#region |   Client Service Monitor Actions   |

		public ServerObjectModelInfo? GetServerObjectModelInfo(int tableId) =>  Program.MonitorClient.GetServerObjectModel(tableId).GetAwaiter().GetResult();

		private async void LoadUserSessions()
		{
			await lockSesion.WaitAsync(); //Use SemaphoreSlim to ensure loading can never occur concurrently with another load

			try
			{
				this.InitializeServiceControls(Program.MonitorClient.IsConnected);

				var data = await this.GetUserSessionInfoRowsAsync();

				this.gridControlUserSessions.BeginInvoke(new Action(() =>
				{
					this.gridControlUserSessions.DataSource = null;
					this.gridControlUserSessions.DataSource = data.ToArray();
				}));

			}
			finally
			{
				lockSesion.Release();
			}
		}

		private async ValueTask<List<SessionInfoRow>> GetUserSessionInfoRowsAsync()
		{
			return await this.CreateUserSessionInfoRows();
		}

		private async ValueTask<List<SessionInfoRow>> CreateUserSessionInfoRows()
		{
			List<SessionInfoRow> list = new List<SessionInfoRow>();
			var response = await Program.MonitorClient.GetSessionInfos();

			if (response.ResponseSucceeded && response.SessionInfos != null) // .Succeeded)
			{
				foreach (SessionInfo session in response.SessionInfos)
				{
					if (session != null && !this.userSessionKeysBySessionID.ContainsKey(session.SessionKey.ToString()))
					{
						int sessionIndex;
						//string sessionId = session.SessionId.ToString();

						if (!this.userSessionKeysBySessionID.TryGetValue(session.SessionKey.ToString(), out sessionIndex))
						{
							sessionIndex = this.dataSourceUserSessions.Count;
							SessionInfoRow sessionRow = new SessionInfoRow(sessionIndex, session.SessionKey, session.ServerAddress, session.ServerPort, session.ClientAddress, session.ClientPort, 
																		   session.Username, connected: true, session.CharacterEncoding, context: this);

							this.userSessionKeysBySessionID[session.SessionKey.ToString()] = sessionIndex;
							list.Add(sessionRow);

							//this.dataSourceUserSessions.Add(sessionRow);
							//this.gridViewUserSessions.RefreshRow(sessionIndex);

							//if (this.dataSourceUserSessions.Count <= MaxBestFitNodeCount)
							//	this.gridViewUserSessions.BestFitColumns();
						}

						if (session.SessionKey.ToString() != null)
							this.userSessionIndexesBySessionKey[session.SessionKey] = session.SessionKey.ToString();

					}
					//await this.ClientServiceMonitor_NewSessionConnected(session.SessionId, session.SessionKey, session.ServerAddress, session.ServerPort, session.ClientAddress, session.ClientPort, session.Username, connected: true);
				}
			}

			return list;
		}

		private async void AppClient_SessionAuthorized(object sender, SessionAuthenticatedMessageArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(async () => await this.OnClientServiceMonitor_SessionAuthorized(e)));
			}
			else
			{
				await this.OnClientServiceMonitor_SessionAuthorized(e);
			}
		}

		private async Task OnClientServiceMonitor_SessionAuthorized(SessionAuthenticatedMessageArgs e)
		{
			await lockSesion.WaitAsync(); //Use SemaphoreSlim to ensure loading can never occur concurrently with another load  

			int sessionIndex;

			try
			{
				if (this.TryGetSessionIndex(e.SessionKey, out sessionIndex))
				{
					this.gridControlUserSessions.BeginInvoke(new Action(() =>
					{
						this.dataSourceUserSessions[sessionIndex].Authorized = true;
						//this.gridViewUserSessions.RefreshRowCell(sessionIndex, this.gridViewUserSessions.Columns[this.userSessionColumnIndexStatus]);
						this.gridViewUserSessions.RefreshRow(sessionIndex);
					}));
				}
			}
			finally
			{

				lockSesion.Release();
			}
		}

		private void ClientServiceMonitor_Closed(object sender, EventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() => this.OnClientServiceMonitor_Closed()));
			}
			else
			{
				this.OnClientServiceMonitor_Closed();
			}
		}

		private void OnClientServiceMonitor_Closed()
		{
			//await lockSesion.WaitAsync(); //Use SemaphoreSlim to ensure loading can never occur concurrently with another load  

			try
			{
				this.InitializeServiceControls(Program.MonitorClient.IsConnected);
				this.ClearUserSessionNodes();
				//this.gridControlUserSessions.BeginInvoke(new Action(() => { this.ClearUserSessionNodes(); }));
			}
			finally
			{
				//lockSesion.Release();
			}
		}

		private async void ClientServiceMonitor_NewSessionConnected(object sender, SessionConnectedMessageArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(async () => await this.ClientServiceMonitor_NewSessionConnected(e.SessionKey, e.ServerAddress!, e.ServerPort, e.ClientAddress!, e.ClientPort, username: String.Empty, connected: true, e.CharacterEncoding)));
			}
			else
			{
				await this.ClientServiceMonitor_NewSessionConnected(e.SessionKey, e.ServerAddress!, e.ServerPort, e.ClientAddress!, e.ClientPort, username: String.Empty, connected: true, e.CharacterEncoding);
			}
		}

		private async void ClientServiceMonitor_NewSessionConnected(object sender, SessionInfo e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(async () => await this.ClientServiceMonitor_NewSessionConnected(e.SessionKey,e.ServerAddress, e.ServerPort, e.ClientAddress, e.ClientPort, e.Username, connected: true, e.CharacterEncoding)));
			}
			else
			{
				await this.ClientServiceMonitor_NewSessionConnected(e.SessionKey, e.ServerAddress, e.ServerPort, e.ClientAddress, e.ClientPort, e.Username, connected: true, e.CharacterEncoding);
			}
		}

		private async Task ClientServiceMonitor_NewSessionConnected(long sessionKey, string serverAddress, int serverPort, string clientAddress, int clientPort, string username, bool connected, Encoding characterEncoding)
		{
			await lockSesion.WaitAsync(); //Use SemaphoreSlim to ensure loading can never occur concurrently with another load  

			int sessionIndex = this.dataSourceUserSessions.Count;
			
			try
			{
				this.userSessionIndexesBySessionKey[sessionKey] = sessionKey.ToString();
				
				if (!this.userSessionKeysBySessionID.TryGetValue(sessionKey.ToString(), out sessionIndex))
				{
					SessionInfoRow sessionRow = new SessionInfoRow(sessionIndex, sessionKey, serverAddress, serverPort, clientAddress, clientPort, username, connected, characterEncoding, context: this);

					this.userSessionKeysBySessionID[sessionKey.ToString()] = sessionIndex;
					
					//this.gridControlUserSessions.BeginInvoke(new Action(() =>
					//{
					//	this.dataSourceUserSessions.Add(sessionRow);
					//}));

					this.dataSourceUserSessions.Add(sessionRow);
				}

			}
			finally
			{
				this.gridViewUserSessions.RefreshRow((int)sessionIndex);

				if (this.dataSourceUserSessions.Count <= MaxBestFitNodeCount)
					this.gridViewUserSessions.BestFitColumns();

				lockSesion.Release();
			}
		}

		//private async Task ClientServiceMonitor_NewSessionConnected(string sessionId, long sessionKey, string serverAddress, int serverPort, string clientAddress, int clientPort, string username, bool connected)
		//{
		//	await lockSesion.WaitAsync(); //Use SemaphoreSlim to ensure loading can never occur concurrently with another load  

		//	try
		//	{
		//		int sessionIndex;

		//		if (!this.userSessionIndexesBySessionId.TryGetValue(sessionId, out sessionIndex))
		//		{
		//			SessionInfoRow sessionRow = new SessionInfoRow(sessionId, sessionKey, serverAddress, serverPort, clientAddress, clientPort, username, connected);

		//			this.gridControlUserSessions.BeginInvoke(new Action(() =>
		//			{
		//				sessionIndex = this.dataSourceUserSessions.Count;

		//				this.userSessionIndexesBySessionId[sessionId] = sessionIndex;
		//				this.dataSourceUserSessions.Add(sessionRow);
		//				this.gridViewUserSessions.RefreshRow(sessionIndex);
		//			}));

		//			if (this.dataSourceUserSessions.Count <= MaxBestFitNodeCount)
		//				this.gridViewUserSessions.BestFitColumns();
		//		}

		//		this.userSessionIdsBySessionKey[sessionKey] = sessionId;
		//	}
		//	finally
		//	{
		//		lockSesion.Release();
		//	}
		//}

		private void ClientServiceMonitor_SessionClosed(object sender, SessionClosedMessageArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() => this.OnClientServiceMonitor_SessionClosed(sender, e)));
			}
			else
			{
				this.OnClientServiceMonitor_SessionClosed(sender, e);
			}
		}

		private async void OnClientServiceMonitor_SessionClosed(object sender, SessionClosedMessageArgs e)
		{
			await lockSesion.WaitAsync(); //Use SemaphoreSlim to ensure loading can never occur concurrently with another load  

			int sessionIndex;
			
			try
			{
				if (this.TryGetSessionIndex(e.SessionKey, out sessionIndex))
				{
					this.gridControlUserSessions.BeginInvoke(new Action(() =>
					{
						SessionInfoRow row;

						try
						{
							row = this.dataSourceUserSessions[sessionIndex];
						}
						catch(Exception)
						{
							row = null;
						}

						if (row != null)
						{
							row.Connected = false;
							//this.gridViewUserSessions.RefreshRowCell(sesionIndex, this.gridViewUserSessions.Columns[this.userSessionColumnIndexStatus]);
							this.gridViewUserSessions.RefreshRow(sessionIndex);
						}
					}));
				}
			}
			//catch // On form closing error may occur trying to write to the exposed this.dataSourceUserSessions
			//{
			//}
			finally
			{

				lockSesion.Release();
			}
		}

		private void AppClientMonitor_SessionAuthorized(object sender, SessionAuthenticatedMessageArgs e)
		{
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(async () => this.OnClientServiceMonitor_SessionAuthorized(sender, e)));
			else
				this.OnClientServiceMonitor_SessionAuthorized(sender, e);
		}

		private void OnClientServiceMonitor_SessionAuthorized(object sender, SessionAuthenticatedMessageArgs e)
		{
			//await lockSesion.WaitAsync(); //Use SemaphoreSlim to ensure loading can never occur concurrently with another load  

			try
			{
				int sessionIndex;

				if (this.TryGetSessionIndex(e.SessionKey, out sessionIndex))
				{
					//this.gridControlUserSessions.BeginInvoke(new Action(() =>
					//{
						this.dataSourceUserSessions[sessionIndex].Authorized = true;
						this.dataSourceUserSessions[sessionIndex].Username =  e.UserId.ToString();
						this.gridViewUserSessions.RefreshRow(sessionIndex);
						//this.gridViewUserSessions.RefreshRowCell(sesionIndex, this.gridViewUserSessions.Columns[this.userSessionColumnIndexStatus]);
					//}));
				}
			}
			finally
			{
				//lockSesion.Release();
			}
		}

		private async void ClientServiceMonitor_SessionPackageJobAction(object sender, RequestResponseSessionMessageArgs e) // long sessionKey, JobActionType jobActionType, PackageInfo receivedPackage, ArraySegment<byte> sentPackageLength, ArraySegment<byte> sentPackageHeaderAnyBody)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(async () => await this.AppendSesionPackageJobActionInfoNode(e)));
			}
			else
			{
				await this.AppendSesionPackageJobActionInfoNode(e);
			}
		}

		private async Task AppendSesionPackageJobActionInfoNode(RequestResponseSessionMessageArgs e) // long sessionKey, JobActionType jobActionType, PackageInfo receivedPackage, ArraySegment<byte> sentPackageLength, ArraySegment<byte> sentPackageHeaderAnyBody)
		{
			await lockRequestsResponses.WaitAsync(); //Use SemaphoreSlim to ensure loading can never occur concurrently with another load  
			
			try
			{
				int rowIndex = this.dataSourceRequestsResponses.Count;
				string sessionID;
				
				this.userSessionIndexesBySessionKey.TryGetValue(e.SessionKey, out sessionID);

				string username = String.Empty; // Username have to be found some how. Find UserSessionRow by SessionKey/ID

				PackageInfoRow row = new PackageInfoRow(rowIndex, e.SessionKey, e.RequestBuffer!, e.ResponseBuffer!, username, PackageAction.Recieved);

				//this.dataSourceRequestsResponses.Add(row);
				this.gridControlRequestsResponses.BeginInvoke(new Action(() =>
				{
					this.dataSourceRequestsResponses.Add(row);
				}));

			}
			finally
			{
				if (this.dataSourceRequestsResponses.Count == 1)
					this.gridViewRequestsResponses.BestFitColumns();

				lockRequestsResponses.Release();
			}
		}

		private async void ClientServiceMonitor_TransactionProcessed(object sender, TransactionMessageArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(async () => await this.AppendTransactionNode(e)));
			}
			else
			{
				await this.AppendTransactionNode(e);
			}
		}

		private async Task AppendTransactionNode(TransactionMessageArgs e)
		{
			await lockTransaction.WaitAsync(); //Use SemaphoreSlim to ensure loading can never occur concurrently with another load  

			try
			{
				int rowIndex = this.dataSourceDatastoreTransactions.Count;
				TransactionInfoRow row = new TransactionInfoRow(rowIndex, e); //, this.GetServerObjectModelInfo); // (e.TransactionId, e.AdministratorKey, e.ClientId, e.Status, e.CreationTime, e.ActionData, e.TransactionActions);
																	//int rowIndex = this.dataSourceDatastoreTransactions.Count;
				this.gridControlDatastoreTransactions.BeginInvoke(new Action(() => this.dataSourceDatastoreTransactions.Add(row)));
				//this.dataSourceDatastoreTransactions.Add(row);
			}
			finally
			{
				lockTransaction.Release();
			}
			//this.gridViewDatastoreTransactions.RefreshRow(rowIndex);
		}


		//private void ClientServiceMonitor_ServicePackageReceived(object sender, SessionPackageMessageArgs e)
		//{
		//	//if (this.InvokeRequired)
		//	//{
		//	//	this.Invoke(new MethodInvoker(() => this.AppendServerCommunicationInfoNode(e)));
		//	//}
		//	//else
		//	//{
		//	//	this.AppendServerCommunicationInfoNode(e);
		//	//}
		//}

		//private void ClientServiceMonitor_ServicePackageSent(object sender, SessionPackageMessageArgs e)
		//{
		//	//if (this.InvokeRequired)
		//	//{
		//	//	this.Invoke(new MethodInvoker(() => this.AppendServerCommunicationInfoNode(e)));
		//	//}
		//	//else
		//	//{
		//	//	this.AppendServerCommunicationInfoNode(e);
		//	//}
		//}

		//private void ClientServiceMonitor_ServiceTestPackageSent(object sender, TestPackageSentMessageArgs e)
		//{
		//	//if (this.InvokeRequired)
		//	//{
		//	//	this.Invoke(new MethodInvoker(() => this.AppendServerCommunicationInfoNode2(e)));
		//	//}
		//	//else
		//	//{
		//	//	this.AppendServerCommunicationInfoNode2(e);
		//	//}
		//}



		//private void ClientServiceMonitor_ServiceSessionPackageJobAction(string sessionId, CommunicationType type, CommunicationDirection direction, CommunicationInitiator initiator, PackageInfo receivedPackage, IList<ArraySegment<byte>> sentData)
		//{
		//	if (this.InvokeRequired)
		//	{
		//		this.Invoke(new MethodInvoker(() => this.AppendServerCommunicationInfoNode(e)));
		//	}
		//	else
		//	{
		//		this.AppendServerCommunicationInfoNode(e);
		//	}
		//}

		private void barButtonItemServerStop_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (this.gridViewUserSessions.FocusedRowHandle >= 0)
			{
				this.dataSourceUserSessions[this.gridViewUserSessions.FocusedRowHandle].Connected = false;
				this.gridViewUserSessions.RefreshRow(this.gridViewUserSessions.FocusedRowHandle);
				//this.gridViewUserSessions.RefreshRowCell(0, this.gridViewUserSessions.Columns[this.userSessionColumnIndexStatus]);
			}
		}

		private void barButtonItemServerStart_ItemClick(object sender, ItemClickEventArgs e)
		{
			//this.AppendUserSessionNode(this.dataSourceUserSessions.Count.ToString(), "Server", 2020, "Client", 555, connected: true);
		}

		private void GridViewRequestsResponses_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			PackageInfoRow? row = null;

			if (e.FocusedRowHandle >= 0)
				row = this.gridViewRequestsResponses.GetRow(e.FocusedRowHandle) as PackageInfoRow;
				
			this.groupPropertyPanelRequestsResponse.SetBindingObject(row, context: this);
		}

		private void GridViewDatastoreTransactions_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			TransactionInfoRow? row = null;

			if (e.FocusedRowHandle >=0)
				row = this.gridViewDatastoreTransactions.GetRow(e.FocusedRowHandle) as TransactionInfoRow;

			this.groupPropertyPanelTransactionLog.SetBindingObject(row, context: this);
		}


		#endregion |   Client Service Monitor Actions   |

		#region |   Node Helper Methods   |

		//private void AppendServerCommunicationInfoNode(SessionPackageMessageArgs e)
		//{
		//	PackageInfoRow row = new PackageInfoRow(e);
		//	int rowIndex = this.gridViewRequestsResponses.RowCount;

		//	this.dataSourceRequestsResponses.Add(row);
		//	this.gridViewRequestsResponses.RefreshRow(rowIndex);
		//}

		private bool TryGetSessionIndex(long sessionKey, out int sessionIndex)
		{
			string sessionId;

			if (this.userSessionIndexesBySessionKey.TryGetValue(sessionKey, out sessionId))
				if (this.userSessionKeysBySessionID.TryGetValue(sessionId, out sessionIndex))
					return true;

			sessionIndex = -1;

			return false;
		}


		//private void AppendServerCommunicationInfoNode(string sessionId, CommunicationType mesageRequestType, PackageInfo receivedPackage, IList<ArraySegment<byte>> sentData)
		//{
		//	PackageInfoRow row = new PackageInfoRow(sessionId, mesageRequestType, receivedPackage, sentData);
		//	int rowIndex = this.gridViewRequestsResponses.RowCount;

		//	this.dataSourceRequestsResponses.Add(row);
		//	this.gridViewRequestsResponses.RefreshRow(rowIndex);
		//}

		//private void AppendServerCommunicationInfoNode2(TestPackageSentMessageArgs e)
		//{
		//	//PackageInfoRow row = new PackageInfoRow(e);
		//	//int rowIndex = this.gridViewRequestsResponses.RowCount;

		//	//this.dataSourceRequestsResponses.Add(row);
		//	//this.gridViewRequestsResponses.RefreshRow(rowIndex);
		//}


		private void FormMain_Shown(object sender, EventArgs e)
		{
			//this.ConnectToServer();
		}

		private void barButtonItemSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.ShowFormSettings();
		}

		private void backstageViewButtonItemSettings_ItemClick(object sender, BackstageViewItemEventArgs e)
		{
			this.ShowFormSettings();
		}

		private void ShowFormSettings()
		{
			FormSettings formSettings = new FormSettings(this.ribbonControl1);
			DialogResult formSettingsDialogResult = formSettings.ShowDialog();

			if (formSettingsDialogResult == System.Windows.Forms.DialogResult.OK)
			{
				Cursor currentCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;

				this.Ribbon.RibbonStyle = (RibbonControlStyle)this.AppContext.UserSettings.RibbonStyle;
				this.Ribbon.ColorScheme = (RibbonControlColorScheme)this.AppContext.UserSettings.RibbonColorScheme;
				this.SetSkinStyle(this.Ribbon.RibbonStyle);

				this.iPaintStyle.Enabled = !this.AppContext.UserSettings.DarkMode; // Change Skin button

				Cursor.Current = currentCursor;
			}
		}

		//private DataGridViewRow AppendUserSessionRow(string sessionId)
		//{
		//	if (String.IsNullOrEmpty(sessionId))
		//		return null;

		//	DataGridViewRow row;

		//	if (!this.userSessionRowsBySessionId.TryGetValue(sessionId, out row))
		//	{
		//		this.gridViewUserSessions.AddNewRow();
		//		int rowHandle = row.Index;
		//		row = this.gridViewUserSessions.GetRow(this.gridViewUserSessions.RowCount - 1) as DataGridViewRow;
		//		this.userSessionRowsBySessionId.Add(sessionId, row);
		//	}

		//	return row;
		//}

		//private bool RemoveUserSessionNode(string sessionId)
		//{
		//	int sesionIndex;

		//	if (this.userSessionIndexBySessionId.TryGetValue(sessionId, out sesionIndex))
		//	{
		//		this.gridViewUserSessions.DeleteRow(sesionIndex);
		//		this.userSessionIndexBySessionId.Remove(sessionId);

		//		return true;
		//	}
		//	else
		//	{
		//		return false;
		//	}
		//}

		private void ClearUserSessionNodes()
		{
			//this.gridViewUserSessions.BeginUpdate();

			//while (this.gridViewUserSessions.RowCount > 0)
			//{
			//	this.gridViewUserSessions.SelectAll();
			//	this.gridViewUserSessions.DeleteSelectedRows();
			//}

			this.userSessionKeysBySessionID.Clear();
			this.userSessionIndexesBySessionKey.Clear();
			this.dataSourceUserSessions.Clear();

			//this.gridControlUserSessions.EndUpdate();

		}

		#endregion |   Node Helper Methods   |

		#region |   Private Helper Classes   |



		#endregion |   Private Helper Classes   |
	}
}
