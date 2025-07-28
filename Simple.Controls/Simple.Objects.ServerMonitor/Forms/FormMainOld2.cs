using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DevExpress;
using DevExpress.LookAndFeel;
using DevExpress.Tutorials.Controls;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.Gallery;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils.Taskbar;
using DevExpress.XtraSplashScreen;
using DevExpress.Skins;
using DevExpress.Utils.Colors;
using DevExpress.XtraEditors.ColorWheel;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraBars;
//using DevExpress.XtraBars.Ribbon;
using System.Collections.Generic;
using DevExpress.Utils.Helpers;
using DevExpress.XtraPrinting;
using DevExpress.Drawing.Printing;
using SuperSocket;
using Simple.Collections;
using Simple.Network;
using Simple.SocketEngine;
using Simple.Objects.MonitorProtocol;
using Simple.Objects.Controls;
using Simple.Controls;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using DevExpress.XtraGauges.Core.Base;
using DevExpress.XtraSpreadsheet.Model.History;
using DevExpress.Data.Extensions;
using DevExpress.Office.Utils;
//using Castle.Components.DictionaryAdapter;
using DevExpress.CodeParser;
using DevExpress.Diagram.Core.Native;
//using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.AspNetCore.Http;
using SuperSocket.Server.Abstractions;
using Simple.Objects.SocketProtocol;
using Simple.Objects.ServerMonitor.Interfaces;

namespace Simple.Objects.ServerMonitor
{
	public partial class FormMainOld2 : SimpleRibbonFormBase, IMonitorSessionContext, IFormMain
	{
		#region |   Public Static Properties   |

		public static string FileNames = "RibbonMRUFiles.ini";
		public static string FolderNames = "RibbonMRUFolders.ini";

		#endregion |   Public Static Properties   |

		#region |   Private Properties   |

		private const int MaxBestFitNodeCount = 50;

		private static SemaphoreSlim lockSesion = new SemaphoreSlim(1, 1);
		private static SemaphoreSlim lockRequestsResponses = new SemaphoreSlim(1, 1);
		private static SemaphoreSlim lockTransaction = new SemaphoreSlim(1, 1);

		//private Dictionary<long, int> userSessionIndexesBySessionKey = new Dictionary<long, int>();
		private Dictionary<long, SessionInfoRow> userSessionInfoRowsBySessionKey = new Dictionary<long, SessionInfoRow>();

		private BindingList<SessionInfoRow> dataSourceUserSessions = new BindingList<SessionInfoRow>();
		private BindingList<PackageInfoRow> dataSourceRequestResponseMessagess = new BindingList<PackageInfoRow>();
		//private BindingList<TransactionInfoRow> dataSourceTransactions = new BindingList<TransactionInfoRow>();
		private BindingList<TransactionInfoRow> dataSourceTransactions = new BindingList<TransactionInfoRow>();

		private PackageArgsFactory packageArgsFactory;
		private EditPanelFactory editPanelFactory;

		#endregion |   Private Properties   |

		#region |   Constructors and Initialization   |

		public FormMainOld2()
		{
			this.AppContext = MonitorAppContext.Instance;
			this.packageArgsFactory = this.CreatePackageArgsFactory();
			this.editPanelFactory = this.CreateEditPanelFactory();

			InitializeComponent();

			this.BarSubItemChangeSkin = this.barSubItemChangeSkin;
			this.IconOptions.Image = global::Simple.Objects.ServerMonitor.Properties.Resources.SimpleObjects;
			this.IconOptions.LargeImage = global::Simple.Objects.ServerMonitor.Properties.Resources.SimpleObjects_Large;

			//CreateColorPopup(popupControlContainer1);
			this.InitSkinGallery();
			//InitFontGallery();
			//InitColorGallery();
			this.InitEditors();
			this.InitSchemeCombo();

			this.InitPrint();
			//this.UpdateSchemeCombo();

			//this.Ribbon.RibbonStyle = (RibbonControlStyle)this.AppContext.UserSettings.RibbonStyle;
			//this.Ribbon.ColorScheme = (RibbonControlColorScheme)this.AppContext.UserSettings.RibbonColorScheme;
			//WindowsFormsSettings.CompactUIMode = (this.AppContext.UserSettings.CompactUI) ? DefaultBoolean.True : DefaultBoolean.False;
			//this.barToggleSwitchItemCompactView.Checked = this.AppContext.UserSettings.CompactUI;


			//this.ribbonControl.ApplicationButtonDropDownControl = this.backstageViewControl;
			this.Ribbon.ApplicationButtonDropDownControl = this.backstageViewControl;  // this.pmAppMain;
																					   //this.Ribbon.ColorScheme = RibbonControlColorScheme.Orange;

			//this.SetRibbonStyle(ribbonStyle);
			//this.SetSkinStyle(ribbonStyle);
			//this.OnStyleChanged(EventArgs.Empty);

			//this.UpdateLookAndFeel(ribbonStyle);
			//this.SetSkinStyle();
			//this.OnStyleChanged(EventArgs.Empty);

			base.Initialize();

			//UserLookAndFeel.Default.StyleChanged += this.OnLookAndFeelStyleChanged;
			//UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");
			//Icon = DevExpress.Utils.ResourceImageHelper.CreateIconFromResourcesEx("Simple.Objects.ServerMonitor.AppIcon.ico", typeof(frmMain).Assembly);
			Icon = Icon.FromHandle(global::Simple.Objects.ServerMonitor.Properties.Resources.SimpleObjects.GetHicon());
			//this.recentLabelItem2.Caption = "Copyright © 2025 Simple.Objects™. ALL RIGHTS RESERVED."; // AssemblyInfo.AssemblyCopyright;

			this.InitializeSkinControls();



			//RibbonControlStyle ribbonStyle = (RibbonControlStyle)this.AppContext.UserSettings.RibbonStyle;

			//this.barEditItemRibbonStyle.EditValue = (RibbonControlStyle)this.AppContext.UserSettings.RibbonStyle;
			//this.barEditItemRibbonStyle.EditValueChanged += this.BarEditItemRibbonStyle_EditValueChanged;

			//this.barEditItemColorScheme.EditValue = (RibbonControlColorScheme)this.AppContext.UserSettings.RibbonColorScheme;
			//this.barEditItemColorScheme.EditValueChanged += this.BarEditItemColorScheme_EditValueChanged;

			//this.barToggleSwitchItemDarkMode.Checked = this.AppContext.UserSettings.DarkMode;
			//this.barToggleSwitchItemDarkMode.CheckedChanged += this.BarToggleSwitchItemDarkMode_CheckedChanged;
			//this.rgbiSkins.Enabled = !this.AppContext.UserSettings.DarkMode;

			//this.barToggleSwitchItemCompactView.Checked = this.AppContext.UserSettings.CompactUI;
			//this.barToggleSwitchItemCompactView.CheckedChanged += this.BarToggleSwitchItemCompactView_CheckedChanged;

			//this.Ribbon.ItemPanelStyle = RibbonItemPanelStyle.Skin;
			//this.Ribbon.ShowApplicationButton = DefaultBoolean.True; // Prevent locking int macOS ribbon style
			//														 //this.SetSkinStyle(ribbonStyle);
			//														 //this.InitSkinMaskColor();
			//string skinName = this.AppContext.UserSettings.RibbonSkinName;

			//this.Ribbon.RibbonStyle = (ribbonStyle == RibbonControlStyle.Default) ? RibbonControlStyle.Office2007 : ribbonStyle;
			//this.Ribbon.ColorScheme = (RibbonControlColorScheme)this.AppContext.UserSettings.RibbonColorScheme;

			//if (skinName == null || skinName.Length == 0)
			//	skinName = this.AppContext.UserSettings.DefaultRibonSkinName;

			//if (this.AppContext.UserSettings.DarkMode)
			//	skinName = this.GetDarkModeSkinName();

			//UserLookAndFeel.Default.SetSkinStyle(skinName);
			//WindowsFormsSettings.CompactUIMode = (this.AppContext.UserSettings.CompactUI) ? DefaultBoolean.True : DefaultBoolean.False;

			this.tabControl.Dock = DockStyle.Fill;
			this.tabControl.Visible = true;
			this.tabControl.BringToFront();

			GridHelper.SetViewOnlyMode(this.gridViewUserSessions);
			this.gridControlUserSessions.DataSource = this.dataSourceUserSessions;
			this.gridViewUserSessions.RowStyle += GridViewUserSessions_RowStyle;

			GridHelper.SetSelectOnlyMode(this.gridViewRequestResponseMessagess);
			this.gridControlRequestResponseMessages.DataSource = this.dataSourceRequestResponseMessagess;
			this.gridViewRequestResponseMessagess.FocusedRowChanged += GridViewRequestResponseMessagess_FocusedRowChanged; ;
			this.groupPropertyPanelRequestResponseMessages.GetEditPanelType += this.EditPanelFactory.GetPackageInfoRowEditPanelType;

			GridHelper.SetSelectOnlyMode(this.gridViewTransactions);
			this.gridControlTransactions.DataSource = this.dataSourceTransactions;
			this.gridViewTransactions.FocusedRowChanged += GridViewTransactions_FocusedRowChanged;
			this.groupPropertyPanelTransactionLog.SetPanelObjectDefinition(typeof(TransactionInfoRow), typeof(EditPanelTransactionLog));

			this.InitializeAppClient();
			//this.AppClient.Connected += this.AppClient_Connected;
			this.AppClient.Closed += this.AppClient_Closed;
			//this.AppClient.Authenticated += this.AppClient_Authenticated;
			this.AppClient.ServerStarted += this.AppClient_ServerStarted;
			this.AppClient.ServerStopped += this.AppClient_ServerStopped;
			this.AppClient.SessionConnected += this.AppClient_SessionConnected;
			this.AppClient.SessionClosed += this.AppClient_SessionClosed;
			this.AppClient.SessionAuthenticated += this.AppClient_SessionAuthenticated;
			this.AppClient.MessageSent += this.AppClient_MessageSent;
			this.AppClient.BrodcastMessageSent += this.AppClient_BrodcastMessageSent;
			this.AppClient.MessageReceived += AppClient_MessageReceived;
			this.AppClient.RequestReceived += this.AppClient_RequestReceived;
			this.AppClient.RequestSent += AppClient_RequestSent;
			this.AppClient.PackageProcessingError += AppClient_PackageProcessingError;
			this.AppClient.TransactionFinished += AppClient_TransactionFinished;

			this.InitializeServiceControls(this.AppClient.IsConnected);
			this.InitializeServerControls(this.AppClient.IsConnected, isServerStarted: false);

			//WindowsFormsSettings.CompactUIMode = (this.AppContext.UserSettings.CompactUI) ? DefaultBoolean.True : DefaultBoolean.False;
			//this.barToggleSwitchItemCompactView.Checked = this.AppContext.UserSettings.CompactUI;
			//this.Ribbon.Refresh();
		}

		public SimpleObjectMonitorClient AppClient => Program.MonitorClient;

		private void InitializeSkinControls()
		{
			RibbonControlStyle ribbonStyle = (RibbonControlStyle)this.AppContext.UserSettings.RibbonStyle;

			this.barEditItemRibbonStyle.EditValue = (RibbonControlStyle)this.AppContext.UserSettings.RibbonStyle;
			this.barEditItemRibbonStyle.EditValueChanged += this.BarEditItemRibbonStyle_EditValueChanged;

			//this.barSubItemChangeSkin.


			this.barEditItemColorScheme.EditValue = (RibbonControlColorScheme)this.AppContext.UserSettings.RibbonColorScheme;
			this.barEditItemColorScheme.EditValueChanged += this.BarEditItemColorScheme_EditValueChanged;

			this.barToggleSwitchItemDarkMode.Checked = this.AppContext.UserSettings.DarkMode;
			this.barToggleSwitchItemDarkMode.CheckedChanged += this.BarToggleSwitchItemDarkMode_CheckedChanged;
			this.rgbiSkins.Enabled = !this.AppContext.UserSettings.DarkMode;

			this.barToggleSwitchItemCompactView.Checked = this.AppContext.UserSettings.CompactUI;
			this.barToggleSwitchItemCompactView.CheckedChanged += this.BarToggleSwitchItemCompactView_CheckedChanged;
			//this.rgbiSkins.


			this.Ribbon.ItemPanelStyle = RibbonItemPanelStyle.Skin;
			this.Ribbon.ShowApplicationButton = DefaultBoolean.True; // Prevent locking int macOS ribbon style

			this.InitSkinMaskColor();
			this.SetSkinStyle(ribbonStyle);
			//UserLookAndFeel.Default.SetSkinStyle(this.AppContext.UserSettings.RibbonSkinName);

			//WindowsFormsSettings.CompactUIMode = (this.AppContext.UserSettings.CompactUI) ? DefaultBoolean.True : DefaultBoolean.False;
			//this.OnStyleChanged(EventArgs.Empty);

			//         if (this.AppContext.UserSettings.CompactUI) // Force refresh ribbon if in commpact view
			//         {
			//	this.barToggleSwitchItemCompactView.Checked = false;
			//             this.barToggleSwitchItemCompactView.Checked = true;
			//}
		}

		private void InitSkinMaskColor()
		{
			if (this.AppContext.UserSettings.RibbonSkinMaskColor != this.AppContext.UserSettings.DefaultRibbonSkinMaskColor)
				UserLookAndFeel.Default.SkinMaskColor = Color.FromArgb(this.AppContext.UserSettings.RibbonSkinMaskColor);

			if (this.AppContext.UserSettings.RibbonSkinMaskColor2 != this.AppContext.UserSettings.DefaultRibbonSkinMaskColor2)
				UserLookAndFeel.Default.SkinMaskColor2 = Color.FromArgb(this.AppContext.UserSettings.RibbonSkinMaskColor2);
		}

		private void InitSchemeCombo()
		{
			foreach (RibbonControlColorScheme value in Enum.GetValues(typeof(RibbonControlColorScheme)))
			{
				if (value == RibbonControlColorScheme.Yellow)
					continue;

				Bitmap bmp = new Bitmap(ScaleUtils.ScaleValue(32), ScaleUtils.ScaleValue(32));

				using (Graphics g = Graphics.FromImage(bmp))
				{
					Rectangle rect = new Rectangle(Point.Empty, bmp.Size);

					rect.Inflate(-1, -1);

					using (SolidBrush b = new SolidBrush(this.rgbiColorScheme.Gallery.Groups[0].Items[(int)value].AppearanceCaption.Normal.ForeColor))
						g.FillRectangle(b, rect);
				}

				this.rgbiColorScheme.Gallery.Groups[0].Items[(int)value].Value = value;
				this.rgbiColorScheme.Gallery.Groups[0].Items[(int)value].ImageOptions.Image = bmp;
			}

			this.rgbiColorScheme.Gallery.SetItemCheck(this.rgbiColorScheme.Gallery.Groups[0].Items[this.AppContext.UserSettings.RibbonColorScheme], true);
			this.ribbonControl.GetController().Changed += OnRibbonControllerChanged;
		}

		private void OnRibbonControllerChanged(object? sender, EventArgs e)
		{
			if (true) // this.Ribbon.RibbonStyle == RibbonControlStyle.Office2010 || this.Ribbon.GetController().LookAndFeel.ActiveSkinName == "Office 2016 Colorful")
				this.rgbiColorScheme.Visibility = BarItemVisibility.Always;
			else
				this.rgbiColorScheme.Visibility = BarItemVisibility.Never;

			//         this.AppContext.UserSettings.RibbonColorScheme = (int)this.Ribbon.ColorScheme;
			//this.AppContext.UserSettings.RibbonSkinName = UserLookAndFeel.Default.ActiveSkinName;

			//this.AppContext.UserSettings.Save();
		}

		private void InitEditors()
		{
			riicStyle.Items.Add(new ImageComboBoxItem("Default", RibbonControlStyle.Default, -1));
			riicStyle.Items.Add(new ImageComboBoxItem("Office 2007", RibbonControlStyle.Office2007, -1));
			riicStyle.Items.Add(new ImageComboBoxItem("Office 2010", RibbonControlStyle.Office2010, -1));
			riicStyle.Items.Add(new ImageComboBoxItem("Office 2013", RibbonControlStyle.Office2013, -1));
			riicStyle.Items.Add(new ImageComboBoxItem("Office 2019", RibbonControlStyle.Office2019, -1));
			riicStyle.Items.Add(new ImageComboBoxItem("Office 365", RibbonControlStyle.Office365, -1));
			riicStyle.Items.Add(new ImageComboBoxItem("MacOffice", RibbonControlStyle.MacOffice, -1));
			riicStyle.Items.Add(new ImageComboBoxItem("TabletOffice", RibbonControlStyle.TabletOffice, -1));
			riicStyle.Items.Add(new ImageComboBoxItem("OfficeUniversal", RibbonControlStyle.OfficeUniversal, -1));

			//biStyle.EditValue = ribbonControl.RibbonStyle;
		}

		#endregion |   Constructors and Initialization   |

		#region |   Public Properties   |

		public PackageArgsFactory PackageArgsFactory => this.packageArgsFactory;
		public EditPanelFactory EditPanelFactory => this.editPanelFactory;

		#endregion |   Public Properties   |

		#region |   Init Methods   |

		private void InitializeAppClient()
		{
			this.barEditItemMonitorServer.EditValue = IpHelper.GetIpAddressOrHostname(this.AppClient.RemoteEndPoint);
			this.barEditItemMonitorServerPort.EditValue = IpHelper.GetPort(this.AppClient.RemoteEndPoint);

			//var getSessionsResult = Program.AppClient.GetSessions();

			//foreach (SessionInfo session in getSessionsResult.ResultValue)
			//	if (!this.userSessionIndexesBySessionId.ContainsKey(session.SessionId))
			//		this.ClientServiceMonitor_NewSessionConnected(session.SessionId, session.SessionKey, session.ServerAddress, session.ServerPort,
			//																							 session.ClientAddress, session.ClientPort, session.Username, connected: true);
			//this.gridViewUserSessions.BestFitColumns();
		}


		private void InitializeServiceControls(bool connected)
		{
			this.barEditItemMonitorServer.EditValue = this.AppContext.UserSettings.Server;
			this.barEditItemMonitorServerPort.EditValue = this.AppContext.UserSettings.Port.ToString();

			if (!connected)
				this.barStaticItemUser.Caption = this.AppContext.UserSettings.LastUsername + " - not athorized";

			this.barButtonItemConnect.Enabled = !connected;
			this.barEditItemMonitorServer.Enabled = !connected;
			this.barEditItemMonitorServerPort.Enabled = !connected;
			this.barButtonItemDisconnect.Enabled = connected;

			this.barStaticItemUser.Enabled = connected;
			this.barButtonItemInfo.Visibility = (connected) ? BarItemVisibility.Never : BarItemVisibility.Always;

			this.barStaticItemMonitorServer.Enabled = connected;
			this.barStaticItemMonitorServer.Caption = (connected) ? "Connected to " + this.barEditItemMonitorServer.EditValue.ToString() + ":" + this.barEditItemMonitorServerPort.EditValue : "Disconnected";
		}

		private void InitializeServerControls(bool connected, bool isServerStarted)
		{
			this.barButtonItemServerStart.Enabled = connected && !isServerStarted;
			this.barButtonItemServerStop.Enabled = connected && isServerStarted;
			this.barButtonItemServerRestart.Enabled = connected && isServerStarted;
		}

		//private void frmMain_Activated(object sender, System.EventArgs e) {
		//          InitPaste();
		//      }
		//public void UpdateText() {
		//    ribbonControl.ApplicationCaption = "Ribbon Simple Pad";
		//    ribbonControl.ApplicationDocumentCaption = CurrentDocName + (CurrentModified ? "*" : "");
		//    //Text = string.Format("Ribbon Simple Pad ({0})", CurrentDocName);
		//    siDocName.Caption = string.Format("  {0}", CurrentDocName);
		//}
		//void ChangeActiveForm() {
		//    UpdateText();
		//    InitCurrentDocument(CurrentRichTextBox);
		//    rtPad_SelectionChanged(CurrentRichTextBox, EventArgs.Empty);
		//    CloseFind();
		//}
		//private void xtraTabbedMdiManager1_FloatMDIChildActivated(object sender, EventArgs e) {
		//    ChangeActiveForm();
		//}
		//private void xtraTabbedMdiManager1_FloatMDIChildDeactivated(object sender, EventArgs e) {
		//    BeginInvoke(new MethodInvoker(ChangeActiveForm));
		//}
		//private void frmMain_MdiChildActivate(object sender, System.EventArgs e) {
		//    ChangeActiveForm();
		//}
		//void rtPad_SelectionChanged(object sender, System.EventArgs e) {
		//    ShowHideFormatCategory();
		//    RichTextBox rtPad = sender as RichTextBox;
		//    InitFormat();
		//    int line = 0, col = 0;

		//    if(rtPad != null) {
		//        InitEdit(rtPad.SelectionLength > 0);
		//        line = rtPad.GetLineFromCharIndex(rtPad.SelectionStart) + 1;
		//        col = rtPad.SelectionStart + 1;
		//    }
		//    else {
		//        InitEdit(false);
		//    }
		//    siPosition.Caption = string.Format("   Line: {0}  Position: {1}   ", line, col);
		//    CurrentFontChanged();
		//}

		//protected virtual void ShowSelectionMiniToolbar() {
		//          Point pt = Control.MousePosition;
		//          pt.Offset(0, -11);
		//          selectionMiniToolbar.Alignment = ContentAlignment.TopRight;
		//          selectionMiniToolbar.PopupMenu = null;
		//          selectionMiniToolbar.Show(pt);
		//      }
		//void rtPad_TextChanged(object sender, System.EventArgs e) {
		//    if(CurrentForm == null) return;
		//    CurrentForm.Modified = true;
		//    InitCurrentDocument(CurrentRichTextBox);
		//}

		//protected void InitFormat() {
		//    iBold.Enabled = SelectFont != null;
		//    iItalic.Enabled = SelectFont != null;
		//    iUnderline.Enabled = SelectFont != null;
		//    iFont.Enabled = SelectFont != null;
		//    iFontColor.Enabled = SelectFont != null;
		//    if(SelectFont != null) {
		//        iBold.Down = SelectFont.Bold;
		//        iItalic.Down = SelectFont.Italic;
		//        iUnderline.Down = SelectFont.Underline;
		//    }
		//    bool _enabled = CurrentRichTextBox != null;
		//    iProtected.Enabled = _enabled;
		//    iBullets.Enabled = _enabled;
		//    iAlignLeft.Enabled = _enabled;
		//    iAlignRight.Enabled = _enabled;
		//    iCenter.Enabled = _enabled;
		//    rgbiFont.Enabled = _enabled;
		//    rgbiFontColor.Enabled = _enabled;
		//    ribbonPageGroup9.ShowCaptionButton = _enabled;
		//    rpgFont.ShowCaptionButton = _enabled;
		//    rpgFontColor.ShowCaptionButton = _enabled;
		//    if(!_enabled) ClearFormats();
		//    if(CurrentRichTextBox != null) {
		//        iProtected.Down = CurrentRichTextBox.SelectionProtected;
		//        iBullets.Down = CurrentRichTextBox.SelectionBullet;
		//        switch(CurrentRichTextBox.SelectionAlignment) {
		//            case HorizontalAlignment.Left:
		//                iAlignLeft.Down = true;
		//                break;
		//            case HorizontalAlignment.Center:
		//                iCenter.Down = true;
		//                break;
		//            case HorizontalAlignment.Right:
		//                iAlignRight.Down = true;
		//                break;
		//        }
		//    }
		//}

		//void ClearFormats() {
		//    iBold.Down = false;
		//    iItalic.Down = false;
		//    iUnderline.Down = false;
		//    iProtected.Down = false;
		//    iBullets.Down = false;
		//    iAlignLeft.Down = false;
		//    iAlignRight.Down = false;
		//    iCenter.Down = false;
		//}

		//protected void InitPaste() {
		//    bool enabledPase = CurrentRichTextBox != null && CurrentRichTextBox.CanPaste(DataFormats.GetFormat(0));
		//    iPaste.Enabled = enabledPase;
		//    sbiPaste.Enabled = enabledPase;
		//}

		//void InitUndo() {
		//    iUndo.Enabled = CurrentRichTextBox != null ? CurrentRichTextBox.CanUndo : false;
		//    iLargeUndo.Enabled = iUndo.Enabled;
		//}
		//protected void InitEdit(bool enabled) {
		//    iCut.Enabled = enabled;
		//    iCopy.Enabled = enabled;
		//    iClear.Enabled = enabled;
		//    iSelectAll.Enabled = CurrentRichTextBox != null ? CurrentRichTextBox.CanSelect : false;
		//    InitUndo();
		//}

		//void InitNewDocument(RichTextBox rtbControl) {
		//    rtbControl.SelectionChanged += new System.EventHandler(this.rtPad_SelectionChanged);
		//    rtbControl.TextChanged += new System.EventHandler(this.rtPad_TextChanged);
		//}

		//void InitCurrentDocument(RichTextBox rtbControl) {
		//    bool _enabled = rtbControl != null;
		//    iSaveAs.Enabled = _enabled;
		//    iClose.Enabled = _enabled;
		//    iPrint.Enabled = _enabled;
		//    sbiSave.Enabled = _enabled;
		//    sbiFind.Enabled = _enabled;
		//    iFind.Enabled = _enabled;
		//    iReplace.Enabled = _enabled;
		//    iSave.Enabled = CurrentModified;
		//    SetModifiedCaption();
		//    InitPaste();
		//    InitFormat();
		//}

		//void SetModifiedCaption() {
		//    if(CurrentForm == null) {
		//        siModified.Caption = "";
		//        return;
		//    }
		//    siModified.Caption = CurrentModified ? "   Modified   " : "";
		//}

		private void InitSkinGallery()
		{
			DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(this.rgbiSkins, true);
		}

		#endregion |   Init Methods   |

		#region |   Event Handling   |

		private void AppClient_Closed(object? sender, EventArgs e)
		{
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => this.AppClient_OnClosed()));
			else
				this.AppClient_OnClosed();
		}

		private void AppClient_OnClosed()
		{
			this.InitializeServiceControls(connected: false);
			this.InitializeServerControls(connected: false, isServerStarted: false);
		}


		private void SessionIsAuthenticated(string username)
		{
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => this.AppClient_OnSessionAuthenticated(username)));
			else
				this.AppClient_OnSessionAuthenticated(username);
		}

		private void AppClient_OnSessionAuthenticated(string username) // The ssesion is authorized -> load user sessions
		{
			ServerState servertState = this.AppClient.GetServerState().GetAwaiter().GetResult();

			this.barStaticItemUser.Enabled = true;
			this.barStaticItemUser.Caption = username;
			this.InitializeServerControls(connected: true, isServerStarted: servertState == ServerState.Started);
			this.SetServerState().DoNotAwait();
			this.LoadUserSessions().DoNotAwait();
		}


		private void AppClient_ServerStarted()
		{
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => this.AppClient_OnServerStarted()));
			else
				this.AppClient_OnServerStarted();
		}

		private void AppClient_OnServerStarted()
		{
			this.InitializeServerControls(connected: true, isServerStarted: true);
		}

		private void AppClient_ServerStopped()
		{
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => this.AppClient_OnServerStopped()));
			else
				this.AppClient_OnServerStopped();
		}

		private void AppClient_OnServerStopped()
		{
			this.InitializeServerControls(connected: true, isServerStarted: false);
			this.gridControlUserSessions.DataSource = null;
		}

		private void AppClient_SessionConnected(SessionConnectedMessageArgs e)
		{
			//         int rowIndex = this.dataSourceUserSessions.Count;
			//         SessionInfoRow row = new SessionInfoRow(rowIndex, sessionInfo.SessionKey, sessionInfo.ServerAddress, sessionInfo.ServerPort,
			//                                                                                   sessionInfo.ClientAddress, sessionInfo.ClientPort, sessionInfo.Username, connected: true);
			//         this.userSessionRowsBySessionKey.Add(sessionInfo.SessionKey, row);
			//         this.gridControlUserSessions.BeginInvoke(() => this.dataSourceUserSessions.Add(row));
			//         this.gridViewUserSessions.RefreshRow(rowIndex);
			//this.gridViewUserSessions.RefreshData();
			//if (this.dataSourceUserSessions.Count <= MaxBestFitNodeCount)
			//    this.gridViewUserSessions.BestFitColumns();


			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => this.AppClient_OnSessionConnected(e)));
			else
				this.AppClient_OnSessionConnected(e);
		}

		private void AppClient_OnSessionConnected(SessionConnectedMessageArgs e)
		{
			int rowIndex = this.dataSourceUserSessions.Count;
			SessionInfoRow row = new SessionInfoRow(rowIndex, e.SessionKey, e.ServerAddress!, e.ServerPort, e.ClientAddress!, e.ClientPort,
													userId: 0, username: String.Empty, connected: true, e.CharacterEncoding, context: this);

			this.userSessionInfoRowsBySessionKey.Add(e.SessionKey, row);
			//this.gridControlUserSessions.BeginInvoke(() => this.dataSourceUserSessions.Add(row));
			this.dataSourceUserSessions.Add(row);
			this.gridViewUserSessions.RefreshRow(rowIndex);

			if (this.dataSourceUserSessions.Count <= MaxBestFitNodeCount)
				this.gridViewUserSessions.BestFitColumns();



			//await lockSesion.WaitAsync(); //Use SemaphoreSlim to ensure loading can never occur concurrently with another load  

			//int sessionIndex = this.dataSourceUserSessions.Count;

			//try
			//{
			//	this.userSessionIndexesBySessionKey[sessionInfo.SessionKey] = sessionIndex;

			//	//if (!this.userSessionIndexesBySessionId.TryGetValue(sessionId, out sessionIndex))
			//	//{
			//		SessionInfoRow sessionRow = new SessionInfoRow(sessionInfo.SessionKey, sessionInfo.ServerAddress, sessionInfo.ServerPort, 
			//																			   sessionInfo.ClientAddress, sessionInfo.ClientPort, sessionInfo.Username, connected: true);

			//		//this.userSessionIndexesBySessionId[sessionId] = sessionIndex;

			//		//this.gridControlUserSessions.BeginInvoke(new Action(() =>
			//		//{
			//		//	this.dataSourceUserSessions.Add(sessionRow);
			//		//}));

			//		this.dataSourceUserSessions.Add(sessionRow);
			//	//}

			//}
			//finally
			//{
			//	this.gridViewUserSessions.RefreshRow(sessionIndex);

			//	if (this.dataSourceUserSessions.Count <= MaxBestFitNodeCount)
			//		this.gridViewUserSessions.BestFitColumns();

			//	lockSesion.Release();
			//}
		}

		private void AppClient_SessionClosed(SessionClosedMessageArgs e)
		{
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => this.AppClient_OnSessionClosed(e)));
			else
				this.AppClient_OnSessionClosed(e);
		}

		private void AppClient_OnSessionClosed(SessionClosedMessageArgs e)
		{
			if (this.userSessionInfoRowsBySessionKey.TryGetValue(e.SessionKey, out SessionInfoRow? row))
			{
				row.Connected = false;
				this.gridViewUserSessions.RefreshRow(row.RowIndex);
			}


			//await lockSesion.WaitAsync(); //Use SemaphoreSlim to ensure loading can never occur concurrently with another load  

			//int sessionIndex;

			//try
			//{
			//	if (this.userSessionIndexesBySessionKey.TryGetValue(sessionClosedInfo.SessionKey, out sessionIndex))
			//	{
			//		if (this.dataSourceUserSessions.TryGetValue(sessionIndex, out SessionInfoRow row))
			//		{
			//			row.Connected = false;
			//			this.gridViewUserSessions.RefreshRow(sessionIndex);
			//		}
			//	}
			//}
			////catch // On form closing error may occur trying to write to the exposed this.dataSourceUserSessions
			////{
			////}
			//finally
			//{

			//	lockSesion.Release();
			//}
		}

		//private async Task<SessionInfoRow?> GetUserSessionRow(long sessionKey)
		//{
		//	await lockSesion.WaitAsync(); //Use SemaphoreSlim to ensure loading can never occur concurrently with another load  

		//	int sessionIndex;
		//	SessionInfoRow? row = null;

		//	try
		//	{
		//		if (this.userSessionIndexesBySessionKey.TryGetValue(sessionKey, out sessionIndex))
		//		{
		//			if (this.dataSourceUserSessions.TryGetValue(sessionIndex, out row))
		//			{
		//				row.Connected = false;
		//				this.gridViewUserSessions.RefreshRow(sessionIndex);
		//			}
		//		}
		//	}
		//	//catch // On form closing error may occur trying to write to the exposed this.dataSourceUserSessions
		//	//{
		//	//}
		//	finally
		//	{

		//		lockSesion.Release();

		//	}

		//	return row;
		//}

		private void AppClient_SessionAuthenticated(SessionAuthenticatedMessageArgs e)
		{
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => this.AppClient_OnSessionAuthenticated(e)));
			else
				this.AppClient_OnSessionAuthenticated(e);
		}

		private void AppClient_OnSessionAuthenticated(SessionAuthenticatedMessageArgs e)
		{
			if (this.userSessionInfoRowsBySessionKey.TryGetValue(e.SessionKey, out SessionInfoRow? row))
			{
				row.UserId = e.UserId;
				row.Username = e.Username; // this.userSessionInfoRowsBySessionKey.GetValueOrDefault(sessionAuthenticatedInfo.SessionKey)?.Username ?? String.Empty;
				row.Authorized = true;
				this.gridViewUserSessions.RefreshRow(row.RowIndex);
			}
		}


		private void AppClient_MessageSent(MessageSessionMessageArgs e)
		{
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => this.AppClient_OnMessageSent(e)));
			else
				this.AppClient_OnMessageSent(e);
		}

		private void AppClient_OnMessageSent(MessageSessionMessageArgs e)
		{
			this.AddNewPackageInfoRow(e.SessionKey, (rowIndex, username) => new PackageInfoRow(rowIndex, e.SessionKey, e.MessageBuffer!, username, PackageAction.Sent));
		}

		private void AppClient_BrodcastMessageSent(BrodcastMessageMessageArgs e)
		{
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => this.AppClient_OnBrodcastMessageSent(e)));
			else
				this.AppClient_OnBrodcastMessageSent(e);
		}

		private void AppClient_OnBrodcastMessageSent(BrodcastMessageMessageArgs e)
		{
			this.AddNewPackageInfoRow(sessionKey: 0, (rowIndex, username) => new PackageInfoRow(rowIndex, sessionKey: 0, e.MessageBuffer!, username, PackageAction.Sent));
		}

		private void AppClient_MessageReceived(MessageSessionMessageArgs e)
		{
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => this.AppClient_OnBrodcastMessageReceived(e)));
			else
				this.AppClient_OnBrodcastMessageReceived(e);
		}

		private void AppClient_OnBrodcastMessageReceived(MessageSessionMessageArgs e)
		{
			this.AddNewPackageInfoRow(e.SessionKey, (rowIndex, username) => new PackageInfoRow(rowIndex, sessionKey: 0, e.MessageBuffer!, username, PackageAction.Recieved));
		}

		private void AppClient_RequestReceived(RequestResponseSessionMessageArgs e)
		{
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => this.AppClient_OnRequestReceived(e)));
			else
				this.AppClient_OnRequestReceived(e);
		}

		private void AppClient_OnRequestReceived(RequestResponseSessionMessageArgs e)
		{
			this.AddNewPackageInfoRow(e.SessionKey, (rowIndex, username) => new PackageInfoRow(rowIndex, e.SessionKey, e.RequestBuffer!, e.ResponseBuffer!, username, PackageAction.Recieved));
		}

		private void AppClient_RequestSent(RequestResponseSessionMessageArgs e)
		{
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => this.AppClient_OnRequestSent(e)));
			else
				this.AppClient_OnRequestSent(e);
		}

		private void AppClient_OnRequestSent(RequestResponseSessionMessageArgs e)
		{
			this.AddNewPackageInfoRow(e.SessionKey, (rowIndex, username) => new PackageInfoRow(rowIndex, e.SessionKey, e.RequestBuffer!, e.ResponseBuffer!, username, PackageAction.Sent));
		}


		private void AppClient_PackageProcessingError(SessionErrorPackageInfoMessageArgs e)
		{
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => this.AppClient_OnPackageProcessingError(e)));
			else
				this.AppClient_OnPackageProcessingError(e);
		}

		private void AppClient_OnPackageProcessingError(SessionErrorPackageInfoMessageArgs e)
		{
			// TODO:
		}

		private void AppClient_TransactionFinished(TransactionMessageArgs e)
		{
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(async () => this.AppendTransactionNode(e)));
			else
				this.AppendTransactionNode(e);
		}

		private void AppClient_OnTransactionFinished(TransactionMessageArgs e)
		{
			int rowIndex = this.dataSourceTransactions.Count;
			TransactionInfoRow row = new TransactionInfoRow(rowIndex, e);
			//int rowIndex = this.dataSourceDatastoreTransactions.Count;
			this.dataSourceTransactions.Add(row);

			if (this.dataSourceTransactions.Count < MaxBestFitNodeCount)
				this.gridViewTransactions?.BestFitColumns();
		}

		private async Task AppendTransactionNode(TransactionMessageArgs e)
		{
			await lockTransaction.WaitAsync(); //Use SemaphoreSlim to ensure loading can never occur concurrently with another load  

			try
			{
				int rowIndex = this.dataSourceTransactions.Count;
				TransactionInfoRow row = new TransactionInfoRow(rowIndex, e); //, this.GetServerObjectModelInfo); // (e.TransactionId, e.AdministratorKey, e.ClientId, e.Status, e.CreationTime, e.ActionData, e.TransactionActions);
																			  //int rowIndex = this.dataSourceDatastoreTransactions.Count;
				this.gridControlTransactions.BeginInvoke(new Action(() =>
				{
					this.dataSourceTransactions.Add(row);

					if (this.dataSourceTransactions.Count < MaxBestFitNodeCount)
						this.gridViewTransactions?.BestFitColumns();
				}));
			}
			finally
			{
				lockTransaction.Release();
			}
			//this.gridViewDatastoreTransactions.RefreshRow(rowIndex);
		}



		#endregion |   Event Handling   |

		#region |   Protected Methods   |

		protected virtual PackageArgsFactory CreatePackageArgsFactory() => new PackageArgsFactory(new List<System.Reflection.Assembly>() { this.GetType().Assembly, System.Reflection.Assembly.Load("Simple.Objects.ServerMonitor") });
		protected virtual EditPanelFactory CreateEditPanelFactory() => new EditPanelFactory();

		protected override void OnStyleChanged(EventArgs e)
		{
			base.OnStyleChanged(e);

			RibbonControlStyle ribbonStyle = this.Ribbon.RibbonStyle;
			string skinName = UserLookAndFeel.Default.SkinName;

			//this.Ribbon.ApplicationIcon = (this.Ribbon.RibbonStyle == RibbonControlStyle.Office2007) ? global::Simple.Objects.ServerMonitor.Properties.Resources.SimpleObjects_Large : null;

			this.Ribbon.ApplicationButtonDropDownControl = (ribbonStyle == RibbonControlStyle.Default || ribbonStyle == RibbonControlStyle.Office2007) ? this.pmAppMain :
																																						 this.backstageViewControl;
			//this.Ribbon.ColorScheme = RibbonControlColorScheme.Blue;
			this.UpdateSchemeCombo(ribbonStyle);

			if (UserLookAndFeel.Default.ActiveSkinName != skinName) // Skin style/name change is invoked by this.rgbiSkins control
			{
				if (!this.AppContext.UserSettings.DarkMode)
					this.AppContext.UserSettings.RibbonSkinName = UserLookAndFeel.Default.ActiveSkinName;

				this.OnSkinNameChange(UserLookAndFeel.Default.ActiveSkinName, oldSkinName: skinName);
			}

			this.barToggleSwitchItemCompactView.Checked = this.AppContext.UserSettings.CompactUI;
		}

		protected override void OnSkinNameChange(string skinName, string oldSkinName)
		{
			base.OnSkinNameChange(skinName, oldSkinName);
			//this.rgbiSkins.Enabled = !this.AppContext.UserSettings.DarkMode;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			SplashScreenManager.CloseForm(false);


			//SessionInfoRow row = new SessionInfoRow(0, 0, "ServerAddress TEST", 2020,
			//											  "ClientAddress TEST", 1024, "Username TEST", connected: true);
			//this.userSessionRowsBySessionKey.Add(0, row);
			//this.gridControlUserSessions.BeginInvoke(() => this.dataSourceUserSessions.Add(row));
			//this.gridViewUserSessions.RefreshRow(0);

		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
		}

		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			WindowsFormsSettings.CompactUIMode = (this.AppContext.UserSettings.CompactUI) ? DefaultBoolean.True : DefaultBoolean.False;
			//this.barToggleSwitchItemCompactView.Checked = this.AppContext.UserSettings.CompactUI;

		}

		#endregion |   Protected Methods   |

		#region |   Print processing   |

		void InitPrintingSystem()
		{
			RibbonForm frm = FindForm() as RibbonForm;
			BarManager manager = ribbonControl.Manager;
			((GalleryDropDown)this.ddbOrientation.DropDownControl).Manager = manager;
			((GalleryDropDown)this.ddbMargins.DropDownControl).Manager = manager;
			((GalleryDropDown)this.ddbPaperSize.DropDownControl).Manager = manager;
			((GalleryDropDown)this.ddbCollate.DropDownControl).Manager = manager;
			((GalleryDropDown)this.ddbPrinter.DropDownControl).Manager = manager;
			((GalleryDropDown)this.ddbDuplex.DropDownControl).Manager = manager;
			PrintingSystem ps = new PrintingSystem();
			this.printControl2.PrintingSystem = ps;
			ps.StartPrint += new PrintDocumentEventHandler(OnStartPrint);
			Link link = new Link(ps);
			//if(CurrentRichTextBox != null)
			//    link.RtfReportHeader = CurrentRichTextBox.Rtf;
			link.CreateDocument();
			this.printButton.Enabled = ps.Pages.Count > 0;
			this.pageButtonEdit.Enabled = ps.Pages.Count > 0;
			this.pageButtonEdit.Properties.DisplayFormat.FormatString = "Page {0} of " + ps.Pages.Count;
			this.pageButtonEdit.EditValue = 1;
			UpdatePrintPageSettings();
		}
		void printButton_Click(object sender, EventArgs e)
		{
			((PrintingSystem)this.printControl2.PrintingSystem).Print(this.ddbPrinter.Text);
		}
		void OnStartPrint(object sender, PrintDocumentEventArgs e)
		{
			e.PrintDocument.PrinterSettings.Copies = (short)this.copySpinEdit.Value;
			Padding p = (Padding)this.ddbMargins.Tag;
			this.printControl2.PrintingSystem.PageSettings.TopMargin = (int)(p.Top * 3.9);
			this.printControl2.PrintingSystem.PageSettings.BottomMargin = (int)(p.Bottom * 3.9);
			this.printControl2.PrintingSystem.PageSettings.LeftMargin = (int)(p.Left * 3.9);
			this.printControl2.PrintingSystem.PageSettings.RightMargin = (int)(p.Right * 3.9);
			e.PrintDocument.PrinterSettings.Collate = (bool)this.ddbCollate.Tag;
			e.PrintDocument.PrinterSettings.Duplex = ((bool)this.ddbDuplex.Tag) ? Duplex.Horizontal : Duplex.Simplex;
		}

		private void InitPrint()
		{
			this.ddbOrientation.DropDownControl = CreateOrientationGallery();
			this.ddbMargins.DropDownControl = CreateMarginsGallery();
			this.ddbPaperSize.DropDownControl = CreatePageSizeGallery();
			this.ddbCollate.DropDownControl = CreateCollateGallery();
			this.ddbPrinter.DropDownControl = CreatePrintersGallery();
			this.ddbDuplex.DropDownControl = CreateDuplexGallery();
		}

		GalleryDropDown CreateListBoxGallery()
		{
			GalleryDropDown res = new GalleryDropDown();
			res.Gallery.FixedImageSize = false;
			res.Gallery.ShowItemText = true;
			res.Gallery.ColumnCount = 1;
			res.Gallery.CheckDrawMode = CheckDrawMode.OnlyImage;
			res.Gallery.ShowGroupCaption = false;
			res.Gallery.AutoSize = GallerySizeMode.Vertical;
			res.Gallery.SizeMode = GallerySizeMode.None;
			res.Gallery.ShowScrollBar = ShowScrollBar.Hide;
			res.Gallery.ItemCheckMode = ItemCheckMode.SingleRadio;
			res.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseTextOptions = true;
			res.Gallery.Appearance.ItemCaptionAppearance.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			res.Gallery.Appearance.ItemCaptionAppearance.Normal.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			res.Gallery.Appearance.ItemCaptionAppearance.Hovered.Options.UseTextOptions = true;
			res.Gallery.Appearance.ItemCaptionAppearance.Hovered.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			res.Gallery.Appearance.ItemCaptionAppearance.Hovered.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			res.Gallery.Appearance.ItemCaptionAppearance.Pressed.Options.UseTextOptions = true;
			res.Gallery.Appearance.ItemCaptionAppearance.Pressed.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			res.Gallery.Appearance.ItemCaptionAppearance.Pressed.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

			res.Gallery.ItemImageLocation = DevExpress.Utils.Locations.Left;
			res.Gallery.Appearance.ItemDescriptionAppearance.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			res.Gallery.Appearance.ItemDescriptionAppearance.Normal.Options.UseTextOptions = true;
			res.Gallery.Appearance.ItemDescriptionAppearance.Hovered.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			res.Gallery.Appearance.ItemDescriptionAppearance.Hovered.Options.UseTextOptions = true;
			res.Gallery.Appearance.ItemDescriptionAppearance.Pressed.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			res.Gallery.Appearance.ItemDescriptionAppearance.Pressed.Options.UseTextOptions = true;
			res.Gallery.Groups.Add(new GalleryItemGroup());
			res.Gallery.StretchItems = true;

			return res;
		}
		GalleryDropDown CreateOrientationGallery()
		{
			GalleryDropDown res = CreateListBoxGallery();
			GalleryItem portraitItem = new GalleryItem();
			portraitItem.ImageOptions.SvgImage = global::Simple.Objects.ServerMonitor.Properties.Resources.PageOrientationPortrait1;
			portraitItem.Caption = "Portrait Orientation";
			GalleryItem landscapeItem = new GalleryItem();
			landscapeItem.ImageOptions.SvgImage = global::Simple.Objects.ServerMonitor.Properties.Resources.PageOrientationLandscape1;
			landscapeItem.Caption = "Landscape Orientation";
			res.Gallery.Groups[0].Items.Add(portraitItem);
			res.Gallery.Groups[0].Items.Add(landscapeItem);
			res.Gallery.ItemCheckedChanged += new GalleryItemEventHandler(OnOrientationGalleryItemCheckedChanged);
			portraitItem.Checked = true;
			return res;
		}
		GalleryDropDown CreateMarginsGallery()
		{
			GalleryDropDown res = CreateListBoxGallery();
			GalleryItem normal = new GalleryItem();
			normal.ImageOptions.SvgImage = global::Simple.Objects.ServerMonitor.Properties.Resources.PageMarginsNormal1;
			normal.Caption = "Normal";
			normal.Description = "Top:\t25 mm\tBottom:\t25 mm\nLeft:\t25 mm\tRight:\t25 mm";
			normal.Tag = new Padding(25, 25, 25, 25);
			GalleryItem narrow = new GalleryItem();
			narrow.ImageOptions.SvgImage = Properties.Resources.PageMarginsNarrow1;
			narrow.Caption = "Narrow";
			narrow.Description = "Top:\t12 mm\tBottom:\t12 mm\nLeft:\t12 mm\tRight:\t12 mm";
			narrow.Tag = new Padding(12, 12, 12, 12);
			GalleryItem moderate = new GalleryItem();
			moderate.ImageOptions.SvgImage = Properties.Resources.PageMarginsModerate1;
			moderate.Caption = "Moderate";
			moderate.Description = "Top:\t25 mm\tBottom:\t25 mm\nLeft:\t19 mm\tRight:\t19 mm";
			moderate.Tag = new Padding(19, 25, 19, 25);
			GalleryItem wide = new GalleryItem();
			wide.ImageOptions.SvgImage = Properties.Resources.PageMarginsWide1;
			wide.Caption = "Wide";
			wide.Description = "Top:\t25 mm\tBottom:\t25 mm\nLeft:\t50 mm\tRight:\t50 mm";
			wide.Tag = new Padding(50, 25, 50, 25);
			res.Gallery.Groups[0].Items.Add(normal);
			res.Gallery.Groups[0].Items.Add(narrow);
			res.Gallery.Groups[0].Items.Add(moderate);
			res.Gallery.Groups[0].Items.Add(wide);
			res.Gallery.ItemCheckedChanged += new GalleryItemEventHandler(OnMarginsGalleryItemCheckedChanged);
			normal.Checked = true;
			return res;
		}
		GalleryDropDown CreatePageSizeGallery()
		{
			GalleryDropDown res = CreateListBoxGallery();
			GalleryItem letter = new GalleryItem();
			letter.ImageOptions.SvgImage = Properties.Resources.PaperKind_Letter1;
			letter.Caption = "Letter";
			letter.Description = "215 mm x 279 mm";
			letter.Tag = PaperKind.Letter;
			GalleryItem tabloid = new GalleryItem();
			tabloid.ImageOptions.SvgImage = Properties.Resources.PaperKind_Tabloid1;
			tabloid.Caption = "Tabloid";
			tabloid.Description = "279 mm x 431 mm";
			tabloid.Tag = PaperKind.Tabloid;
			GalleryItem legal = new GalleryItem();
			legal.ImageOptions.SvgImage = Properties.Resources.PaperKind_Legal1;
			legal.Caption = "Legal";
			legal.Description = "215 mm x 355 mm";
			legal.Tag = PaperKind.Legal;
			GalleryItem executive = new GalleryItem();
			executive.ImageOptions.SvgImage = Properties.Resources.PaperKind_Executive1;
			executive.Caption = "Executive";
			executive.Description = "184 mm x 266 mm";
			executive.Tag = PaperKind.Executive;
			GalleryItem a3 = new GalleryItem();
			a3.ImageOptions.SvgImage = Properties.Resources.PaperKind_A31;
			a3.Caption = "A3";
			a3.Description = "296 mm x 420 mm";
			a3.Tag = PaperKind.A3;
			GalleryItem a4 = new GalleryItem();
			a4.ImageOptions.SvgImage = Properties.Resources.PaperKind_A41;
			a4.Caption = "A4";
			a4.Description = "210 mm x 296 mm";
			a4.Tag = PaperKind.A4;
			GalleryItem a5 = new GalleryItem();
			a5.ImageOptions.SvgImage = Properties.Resources.PaperKind_A51;
			a5.Caption = "A5";
			a5.Description = "148 mm x 210 mm";
			a5.Tag = PaperKind.A5;
			GalleryItem a6 = new GalleryItem();
			a6.ImageOptions.SvgImage = Properties.Resources.PaperKind_A61;
			a6.Caption = "A6";
			a6.Description = "105 mm x 148 mm";
			a6.Tag = PaperKind.A6;
			res.Gallery.Groups[0].Items.Add(letter);
			res.Gallery.Groups[0].Items.Add(tabloid);
			res.Gallery.Groups[0].Items.Add(legal);
			res.Gallery.Groups[0].Items.Add(executive);
			res.Gallery.Groups[0].Items.Add(a3);
			res.Gallery.Groups[0].Items.Add(a4);
			res.Gallery.Groups[0].Items.Add(a5);
			res.Gallery.Groups[0].Items.Add(a6);
			res.Gallery.ItemCheckedChanged += new GalleryItemEventHandler(OnPaperSizeGalleryItemCheckedChanged);
			a4.Checked = true;
			return res;
		}
		GalleryDropDown CreateCollateGallery()
		{
			GalleryDropDown res = CreateListBoxGallery();
			GalleryItem collated = new GalleryItem();
			collated.ImageOptions.SvgImage = global::Simple.Objects.ServerMonitor.Properties.Resources.Page_;
			collated.Caption = "Collated";
			collated.Description = "1,2,3   1,2,3  1,2,3";
			collated.Tag = true;
			GalleryItem uncollated = new GalleryItem();
			uncollated.ImageOptions.SvgImage = global::Simple.Objects.ServerMonitor.Properties.Resources.Page_;
			uncollated.Caption = "Uncollated";
			uncollated.Description = "1,1,1  2,2,2  3,3,3";
			uncollated.Tag = false;
			res.Gallery.Groups[0].Items.Add(collated);
			res.Gallery.Groups[0].Items.Add(uncollated);
			res.Gallery.ItemCheckedChanged += new GalleryItemEventHandler(OnCollateGalleryItemCheckedChanged);
			collated.Checked = true;
			return res;
		}
		GalleryDropDown CreateDuplexGallery()
		{
			GalleryDropDown res = CreateListBoxGallery();
			GalleryItem oneSided = new GalleryItem();
			oneSided.ImageOptions.SvgImage = global::Simple.Objects.ServerMonitor.Properties.Resources.Page_;
			oneSided.Caption = "Print One Sided";
			oneSided.Description = "Only print on one side of the page";
			oneSided.Tag = false;
			GalleryItem twoSided = new GalleryItem();
			twoSided.ImageOptions.SvgImage = global::Simple.Objects.ServerMonitor.Properties.Resources.Page_;
			twoSided.Caption = "Manually Print on Both Sides";
			twoSided.Description = "Reload paper when prompted to print the second side";
			twoSided.Tag = false;
			res.Gallery.Groups[0].Items.Add(oneSided);
			res.Gallery.Groups[0].Items.Add(twoSided);
			res.Gallery.ItemCheckedChanged += new GalleryItemEventHandler(OnDuplexGalleryItemCheckedChanged);
			oneSided.Checked = true;
			return res;
		}
		void OnDuplexGalleryItemCheckedChanged(object sender, GalleryItemEventArgs e)
		{
			UpdatePrintPageDuplex(e.Item);
		}
		GalleryDropDown CreatePrintersGallery()
		{
			GalleryDropDown res = CreateListBoxGallery();
			PrinterSettings ps = new PrinterSettings();
			GalleryItem defaultPrinter = null;
			try
			{
				foreach (string str in PrinterSettings.InstalledPrinters)
				{
					GalleryItem item = new GalleryItem();
					item.ImageOptions.SvgImage = this.printButton.ImageOptions.SvgImage;
					item.Caption = str;
					res.Gallery.Groups[0].Items.Add(item);
					ps.PrinterName = str;
					if (ps.IsDefaultPrinter)
						defaultPrinter = item;
				}
			}
			catch { }
			res.Gallery.ItemCheckedChanged += new GalleryItemEventHandler(OnPrinterGalleryItemCheckedChanged);
			if (defaultPrinter != null)
				defaultPrinter.Checked = true;
			return res;
		}
		void OnMarginsGalleryItemCheckedChanged(object sender, GalleryItemEventArgs e)
		{
			UpdatePrintPageMargins(e.Item);
		}
		void OnPrinterGalleryItemCheckedChanged(object sender, GalleryItemEventArgs e)
		{
			UpdatePrintPagePrinters(e.Item);
		}
		void OnCollateGalleryItemCheckedChanged(object sender, GalleryItemEventArgs e)
		{
			UpdatePrintPageCollate(e.Item);
		}
		void OnPaperSizeGalleryItemCheckedChanged(object sender, GalleryItemEventArgs e)
		{
			UpdatePrintPageSize(e.Item);
		}
		void OnOrientationGalleryItemCheckedChanged(object sender, GalleryItemEventArgs e)
		{
			UpdatePrintPageOrientation(e.Item);
		}
		public void UpdatePrintPageSettings()
		{
			if (ddbOrientation.DropDownControl != null)
			{
				UpdatePrintPageOrientation(((GalleryDropDown)ddbOrientation.DropDownControl).Gallery.GetCheckedItem());
			}
			if (ddbMargins.DropDownControl != null)
			{
				UpdatePrintPageMargins(((GalleryDropDown)ddbMargins.DropDownControl).Gallery.GetCheckedItem());
			}
			if (ddbPaperSize.DropDownControl != null)
			{
				UpdatePrintPageSize(((GalleryDropDown)ddbPaperSize.DropDownControl).Gallery.GetCheckedItem());
			}
			if (ddbCollate.DropDownControl != null)
			{
				UpdatePrintPageCollate(((GalleryDropDown)ddbCollate.DropDownControl).Gallery.GetCheckedItem());
			}
			if (ddbPrinter.DropDownControl != null)
			{
				UpdatePrintPagePrinters(((GalleryDropDown)ddbPrinter.DropDownControl).Gallery.GetCheckedItem());
			}
			if (ddbDuplex.DropDownControl != null)
			{
				UpdatePrintPageDuplex(((GalleryDropDown)ddbDuplex.DropDownControl).Gallery.GetCheckedItem());
			}
		}
		void UpdatePrintPageOrientation(GalleryItem item)
		{
			if (item == null) return;
			ddbOrientation.Text = item.Caption;
			ddbOrientation.ImageOptions.SvgImage = item.ImageOptions.SvgImage;
			if (ddbOrientation.DropDownControl != null)
			{
				this.printControl2.PrintingSystem.PageSettings.Landscape = ((GalleryDropDown)ddbOrientation.DropDownControl).Gallery.Groups[0].Items[1].Checked;
			}
		}
		void UpdatePrintPageMargins(GalleryItem item)
		{
			if (item == null) return;
			this.ddbMargins.ImageOptions.SvgImage = item.ImageOptions.SvgImage;
			this.ddbMargins.Text = item.Caption;
			this.ddbMargins.Tag = item.Tag;
			Padding p = (Padding)item.Tag;
			if (this.printControl2.PrintingSystem != null)
			{
				this.printControl2.PrintingSystem.PageSettings.TopMargin = (int)(p.Top * 3.9);
				this.printControl2.PrintingSystem.PageSettings.BottomMargin = (int)(p.Bottom * 3.9);
				this.printControl2.PrintingSystem.PageSettings.LeftMargin = (int)(p.Left * 3.9);
				this.printControl2.PrintingSystem.PageSettings.RightMargin = (int)(p.Right * 3.9);
			}
		}
		void UpdatePrintPageSize(GalleryItem item)
		{
			if (item == null) return;
			this.ddbPaperSize.ImageOptions.SvgImage = item.ImageOptions.SvgImage;
			this.ddbPaperSize.Text = item.Caption;
			if (this.printControl2.PrintingSystem != null)
			{
				this.printControl2.PrintingSystem.PageSettings.PaperKind = (DXPaperKind)item.Tag;
			}
		}
		void UpdatePrintPageCollate(GalleryItem item)
		{
			if (item == null) return;
			this.ddbCollate.ImageOptions.SvgImage = item.ImageOptions.SvgImage;
			this.ddbCollate.Text = item.Caption;
			this.ddbCollate.Tag = item.Tag;
		}
		void UpdatePrintPagePrinters(GalleryItem item)
		{
			if (item == null) return;
			this.ddbPrinter.Text = item.Caption;
			this.ddbPrinter.ImageOptions.SvgImage = item.ImageOptions.SvgImage;
		}

		void zoomTrackBarControl1_EditValueChanged(object sender, EventArgs e)
		{
			if (updatedZoom) return;
			updatedZoom = true;
			try
			{
				zoomTextEdit.EditValue = GetZoomValue();
			}
			finally
			{
				updatedZoom = false;
			}
		}
		int GetZoomValue()
		{
			if (zoomTrackBarControl1.Value <= 40)
				return 10 + 90 * (zoomTrackBarControl1.Value - 0) / 40;
			else
				return 100 + 400 * (zoomTrackBarControl1.Value - 40) / 40;
		}
		bool updatedZoom = false;

		void zoomTextEdit_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				int zoomValue = Int32.Parse((string)zoomTextEdit.EditValue.ToString());
				this.zoomTrackBarControl1.Value = ZoomValueToValue(zoomValue);
				this.printControl2.Zoom = 0.01f * (int)zoomValue;
			}
			catch (Exception) { }
		}
		int ZoomValueToValue(int zoomValue)
		{
			if (zoomValue < 100)
				return Math.Min(80, Math.Max(0, (zoomValue - 10) * 40 / 90));
			return Math.Min(80, Math.Max(0, (zoomValue - 100) * 40 / 400 + 40));
		}

		void pageButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			int pageIndex = (int)this.pageButtonEdit.EditValue;
			if (e.Button.Kind == ButtonPredefines.Left)
			{
				if (pageIndex > 1)
					pageIndex--;
			}
			else if (e.Button.Kind == ButtonPredefines.Right)
			{
				if (pageIndex < this.printControl2.PrintingSystem.Pages.Count)
					pageIndex++;
			}
			this.pageButtonEdit.EditValue = pageIndex;
		}

		void pageButtonEdit_EditValueChanging(object sender, ChangingEventArgs e)
		{
			try
			{
				int pageIndex = Int32.Parse(e.NewValue.ToString());
				if (pageIndex < 1)
					pageIndex = 1;
				else if (pageIndex > this.printControl2.PrintingSystem.Pages.Count)
					pageIndex = this.printControl2.PrintingSystem.Pages.Count;
				e.NewValue = pageIndex;
			}
			catch (Exception)
			{
				e.NewValue = 1;
			}
		}

		void UpdatePageButtonsEnabledState(int pageIndex)
		{
			if (pageButtonEdit.Properties.Buttons.Count == 0) return;
			this.pageButtonEdit.Properties.Buttons[0].Enabled = pageIndex != 1;
			this.pageButtonEdit.Properties.Buttons[1].Enabled = pageIndex != this.printControl2.PrintingSystem.Pages.Count;
		}

		void pageButtonEdit_EditValueChanged(object sender, EventArgs e)
		{
			int pageIndex = Convert.ToInt32(this.pageButtonEdit.EditValue);
			this.printControl2.SelectedPageIndex = pageIndex - 1;
			UpdatePageButtonsEnabledState(pageIndex);
		}

		void printControl2_SelectedPageChanged(object sender, EventArgs e)
		{
			this.pageButtonEdit.EditValue = this.printControl2.SelectedPageIndex + 1;
		}

		private void rgbiColorScheme_Gallery_InitDropDownGallery(object sender, InplaceGalleryEventArgs e)
		{
			e.PopupGallery.SynchWithInRibbonGallery = true;
			//e.PopupGallery.AllowGlyphSkinning = true;
		}

		private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
		{
			XtraMessageBox.Show("Page Borders clicked.");
		}

		void UpdatePrintPageDuplex(GalleryItem item)
		{
			if (item == null) return;
			this.ddbDuplex.Text = item.Caption;
			this.ddbDuplex.ImageOptions.SvgImage = item.ImageOptions.SvgImage;
			this.ddbDuplex.Tag = item.Tag;
		}

		#endregion |   Print processing   |

		#region |    Button Action Handling   |

		private void barButtonItemConnect_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.AppContext.UserSettings.Server = (string)this.barEditItemMonitorServer.EditValue;
			this.AppContext.UserSettings.Port = Conversion.TryChangeType<int>(this.barEditItemMonitorServerPort.EditValue);
			this.AppContext.UserSettings.Save();

			this.barButtonItemConnect.Enabled = false;
			this.barEditItemMonitorServer.Enabled = false;
			this.barEditItemMonitorServerPort.Enabled = false;
			this.barButtonItemDisconnect.Enabled = true;
			this.barStaticItemMonitorServer.Caption = "Connecting...";

			Task.Run(async () =>
			{
				bool connected = await this.TryConnectToServerAndAuthorize();

				this.InitializeServiceControls(connected);

				if (connected)
					this.SessionIsAuthenticated(AppContext.UserSettings.LastUsername);
				else
					await this.AppClient.CloseAsync();
			});
		}

		private void barButtonItemDisconnect_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.InitializeServiceControls(connected: false);
			this.InitializeServerControls(connected: false, isServerStarted: false);

			Task.Run(async () => await this.AppClient.CloseAsync());

			this.gridControlUserSessions.DataSource = null;
			this.gridControlUserSessions.Refresh();
		}

		private void barButtonItemServerStart_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.DisableServerActionButtons();

			Task.Run(async () => await this.AppClient.StartServer());

			//this.InitializeServerControls(connected: true, isServerStarted: true);
		}

		private void barButtonItemServerStop_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.DisableServerActionButtons();

			Task.Run(async () => await this.AppClient.StopServer());

			//this.InitializeServerControls(connected: true, isServerStarted: false);
		}

		private void barButtonItemServerRestart_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.DisableServerActionButtons();

			Task.Run(async () =>
			{
				await this.AppClient.StopServer();
				await this.AppClient.StartServer();
			});
			//this.InitializeServerControls(connected: true, isServerStarted: true);
		}

		private void DisableServerActionButtons()
		{
			this.barButtonItemServerStart.Enabled = false;
			this.barButtonItemServerStop.Enabled = false;
			this.barButtonItemServerRestart.Enabled = false;
		}

		private void rgbiColorScheme_Gallery_ItemCheckedChanged(object sender, GalleryItemEventArgs e)
		{
			RibbonControlColorScheme colorScheme = ((RibbonControlColorScheme)e.Item.Value);

			ribbonControl.ColorScheme = colorScheme;
			this.AppContext.UserSettings.RibbonColorScheme = (int)colorScheme;
			this.AppContext.UserSettings.Save();
		}

		private void BarEditItemColorScheme_EditValueChanged(object? sender, EventArgs e)
		{
			this.Ribbon.ColorScheme = ((RibbonControlColorScheme)barEditItemColorScheme.EditValue);
			this.OnStyleChanged(EventArgs.Empty);
		}

		private void BarToggleSwitchItemDarkMode_CheckedChanged(object sender, ItemClickEventArgs e)
		{
			this.AppContext.UserSettings.DarkMode = this.barToggleSwitchItemDarkMode.Checked;
			this.AppContext.UserSettings.Save();
			this.rgbiSkins.Enabled = !this.AppContext.UserSettings.DarkMode;

			this.SetSkinStyle();
		}

		private void BarToggleSwitchItemCompactView_CheckedChanged(object sender, ItemClickEventArgs e)
		{
			this.AppContext.UserSettings.CompactUI = this.barToggleSwitchItemCompactView.Checked;
			this.AppContext.UserSettings.Save();
			this.SetSkinStyle();
		}

		private void BarEditItemRibbonStyle_EditValueChanged(object? sender, EventArgs e)
		{
			RibbonControlStyle style = (RibbonControlStyle)barEditItemRibbonStyle.EditValue;

			this.AppContext.UserSettings.RibbonStyle = (int)style;
			this.AppContext.UserSettings.Save();

			this.SetRibbonStyle(style);
		}

		#endregion |    Button Action Handling   |

		#region |   AppClient Handling   |

		private async ValueTask<BindingList<SessionInfoRow>> CreateUserSessionInfoRowBindingList()
		{
			BindingList<SessionInfoRow> sessionInfoRowBindingList = new BindingList<SessionInfoRow>();
			var response = await this.AppClient.GetSessionInfos();

			if (response.ResponseSucceeded && response.SessionInfos != null)
			{
				for (long i = 0; i < response.SessionInfos.LongLength; i++)
				{
					SessionInfo sessionInfo = response.SessionInfos[i];
					SessionInfoRow sessionInfoRow = new SessionInfoRow((int)i, sessionInfo.SessionKey, sessionInfo.ServerAddress, sessionInfo.ServerPort, sessionInfo.ClientAddress, sessionInfo.ClientPort,
																	   sessionInfo.UserId, sessionInfo.Username, connected: true, sessionInfo.CharacterEncoding, context: this);
					sessionInfoRowBindingList.Add(sessionInfoRow);
				}
			}

			return sessionInfoRowBindingList;
		}

		public SessionInfoRow? GetSessionInfoRow(long sessionKey)
		{
			if (this.userSessionInfoRowsBySessionKey.TryGetValue(sessionKey, out SessionInfoRow? value))
				return value;

			return default;
		}


		#endregion |   AppClient Handling   |

		#region |   Internal Methods   |

		#endregion |   Internal Methods   |

		#region |   Private Methods   |

		private void AddNewPackageInfoRow(long sessionKey, Func<int, string, PackageInfoRow> createPackageInfoRow)
		{
			int rowIndex = this.dataSourceRequestResponseMessagess.Count;
			string username = this.GetSessionInfoRow(sessionKey)?.Username ?? String.Empty;
			PackageInfoRow row = createPackageInfoRow(rowIndex, username);

			this.dataSourceRequestResponseMessagess.Add(row);
			//this.gridViewRequestResponseMessagess.RefreshRow(rowIndex);

			if (this.dataSourceRequestResponseMessagess.Count < MaxBestFitNodeCount)
				this.gridViewRequestResponseMessagess.BestFitColumns();
		}

		private void GridViewUserSessions_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
		{
			if (e.RowHandle >= 0 && (this.gridViewUserSessions.GetRow(e.RowHandle) is SessionInfoRow row) && !row.Connected)
				e.Appearance.ForeColor = Color.Gray;
		}

		private void GridViewRequestResponseMessagess_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			PackageInfoRow? row = null;

			if (e.FocusedRowHandle >= 0)
				row = this.gridViewRequestResponseMessagess.GetRow(e.FocusedRowHandle) as PackageInfoRow;

			this.groupPropertyPanelRequestResponseMessages.SetBindingObject(row, context: this);
		}

		private void GridViewTransactions_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			TransactionInfoRow? row = null;

			if (e.FocusedRowHandle >= 0)
				row = this.gridViewTransactions.GetRow(e.FocusedRowHandle) as TransactionInfoRow;

			this.groupPropertyPanelTransactionLog.SetBindingObject(row, context: this);
		}

		private void UpdateSchemeCombo(RibbonControlStyle ribbonStyle)
		{
			//RibbonControlStyle style = (RibbonControlStyle)this.AppContext.UserSettings.RibbonStyle; //this.ribbonControl.RibbonStyle;

			if (ribbonStyle == RibbonControlStyle.MacOffice || ribbonStyle == RibbonControlStyle.Office2010 || ribbonStyle == RibbonControlStyle.Office2013 || ribbonStyle == RibbonControlStyle.Default)
				this.barEditItemColorScheme.Visibility = (UserLookAndFeel.Default.ActiveSkinName.Contains("Office")) ? BarItemVisibility.Always :
																													   BarItemVisibility.Never;
			else
				this.barEditItemColorScheme.Visibility = BarItemVisibility.Never;
		}


		//private void iFind_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		//{
		//	if (CurrentRichTextBox == null) return;
		//	if (dlgReplace != null) dlgReplace.Close();
		//	if (dlgFind != null) dlgFind.Close();
		//	dlgFind = new frmFind(CurrentRichTextBox, Bounds);
		//	AddOwnedForm(dlgFind);
		//	dlgFind.Show();
		//}

		//private void iReplace_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		//{
		//	if (CurrentRichTextBox == null) return;
		//	if (dlgReplace != null) dlgReplace.Close();
		//	if (dlgFind != null) dlgFind.Close();
		//	dlgReplace = new frmReplace(CurrentRichTextBox, Bounds);
		//	AddOwnedForm(dlgReplace);
		//	dlgReplace.Show();
		//}

		private void iWeb_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			string fileName = "http://www.devexpress.com";
			DevExpress.Data.Utils.SafeProcess.Open(fileName);
		}

		private void iAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			BarManager.About();
		}

		string TextByCaption(string caption)
		{
			return caption.Replace("&", "");
		}

		private void frmMain_Load(object sender, System.EventArgs e)
		{
			//InitMostRecentFiles();
			//arMRUList = new MRUArrayList(pcAppMenuFileLabels, imageCollection3.Images[0], imageCollection3.Images[1]);
			//arMRUList.LabelClicked += new EventHandler(OnMRUFileLabelClicked);
			//InitMostRecentFiles(arMRUList);
			ribbonControl.ForceInitialize();

			GalleryDropDown skins = new GalleryDropDown();

			skins.Ribbon = ribbonControl;
			DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGalleryDropDown(skins);
			//barSubItemChangeSkin.DropDownControl = skins;
		}

		private void buttonSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.ShowFormSettings();
		}

		private void backstageViewButtonItemSettings_ItemClick(object sender, BackstageViewItemEventArgs e)
		{
			this.ShowFormSettings();
		}

		private void ShowFormSettings()
		{
			FormSettings formSettings = new FormSettings(this);
			DialogResult formSettingsDialogResult = formSettings.ShowDialog();

			if (formSettingsDialogResult == System.Windows.Forms.DialogResult.OK)
			{
				Cursor? currentCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;

				this.Ribbon.RibbonStyle = (RibbonControlStyle)this.AppContext.UserSettings.RibbonStyle;
				this.Ribbon.ColorScheme = (RibbonControlColorScheme)this.AppContext.UserSettings.RibbonColorScheme;
				this.SetSkinStyle(this.Ribbon.RibbonStyle);
				this.OnStyleChanged(EventArgs.Empty);

				this.rgbiSkins.Enabled = !this.AppContext.UserSettings.DarkMode;

				Cursor.Current = currentCursor;
			}
		}

		//private void InitMostRecentFiles(MRUArrayList arMRUList)
		//{
		//	string fileName = Application.StartupPath + "\\" + MRUArrayList.MRUFileName;
		//	arMRUList.Init(fileName, "Document1.rtf");

		//}

		private void ribbonControl1_ApplicationButtonDoubleClick(object sender, EventArgs e)
		{
			//if(ribbonControl.RibbonStyle == RibbonControlStyle.Office2007)
			//    this.Close();
		}

		private void barEditItem1_ItemPress(object sender, ItemClickEventArgs e)
		{
			//DevExpress.Data.Utils.SafeProcess.Start("http://www.devexpress.com");
		}

		private void iPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			XtraMessageBox.Show(this, "Note that you can use the XtraPrinting Library to print the contents of the standard RichTextBox control.\r\nFor more information, see the main XtraPrinting demo.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		void iClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//if (CurrentForm != null) CurrentForm.Close();
		}

		private void sbExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		//private void beiFontSize_EditValueChanged(object sender, EventArgs e) {
		//    if(CurrentRichTextBox == null) return;
		//    Font _font = CurrentRichTextBox.SelectionFont;
		//    if(_font == null) {
		//        _font = AppearanceObject.DefaultFont;
		//    }
		//    CurrentRichTextBox.SelectionFont = new Font(_font.FontFamily, Convert.ToSingle(beiFontSize.EditValue), _font.Style);
		//}

		void onTabPrint_SelectedChanged(object sender, BackstageViewItemEventArgs e)
		{
			InitPrintingSystem();
		}

		void ribbonControl1_BeforeApplicationButtonContentControlShow(object sender, EventArgs e)
		{
			if (backstageViewControl.SelectedTab == printTabItem)
				InitPrintingSystem();
		}

		//private void bvItemSave_ItemClick(object sender, BackstageViewItemEventArgs e) {
		//    Save();
		//}

		//private void bvItemSaveAs_ItemClick(object sender, BackstageViewItemEventArgs e) {
		//    SaveAs();
		//}

		//private void bvItemOpen_ItemClick(object sender, BackstageViewItemEventArgs e) {
		//    OpenFile();
		//}

		private void bvItemClose_ItemClick(object sender, BackstageViewItemEventArgs e)
		{
			//if(xtraTabbedMdiManager1.SelectedPage != null)
			//    xtraTabbedMdiManager1.SelectedPage.MdiChild.Close();
		}

		private void bvItemExit_ItemClick(object sender, BackstageViewItemEventArgs e)
		{
			this.Close();
		}
		void ribbonControl1_ResetLayout(object sender, ResetLayoutEventArgs e)
		{
			//ShowHideFormatCategory();
		}
		//void OnNewDocThumbButtonClick(object sender, ThumbButtonClickEventArgs e) {
		//    //CreateNewDocument();
		//}
		//void OnPrevThumbButtonClick(object sender, ThumbButtonClickEventArgs e) {
		//    Form mdiChild = GetPrevMdiChild();
		//    if(mdiChild != null)
		//        ActivateMdiChild(mdiChild);
		//}
		//void OnNextDocThumbButtonClick(object sender, ThumbButtonClickEventArgs e) {
		//    Form mdiChild = GetNextMdiChild();
		//    if(mdiChild != null)
		//        ActivateMdiChild(mdiChild);
		//}
		//void OnExitThumbButtonClick(object sender, ThumbButtonClickEventArgs e) {
		//    Close();
		//}
		//Form GetNextMdiChild() {
		//    if(ActiveMdiChild == null || MdiChildren.Length < 2)
		//        return null;
		//    int pos = Array.IndexOf(MdiChildren, ActiveMdiChild);
		//    return pos == MdiChildren.Length - 1 ? MdiChildren[0] : MdiChildren[pos + 1];
		//}
		//Form GetPrevMdiChild() {
		//    if(ActiveMdiChild == null || MdiChildren.Length < 2)
		//        return null;
		//    int pos = Array.IndexOf(MdiChildren, ActiveMdiChild);
		//    return pos == 0 ? MdiChildren[MdiChildren.Length - 1] : MdiChildren[pos - 1];
		//}
		//void OnTabbedMdiManagerPageCollectionChanged(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e) {
		//    UpdateThumbnailButtons();
		//}
		//void UpdateThumbnailButtons() {
		//    thumbButtonNext.Enabled = thumbButtonPrev.Enabled = MdiChildren.Length > 1;
		//}

		private void bbColorMix_ItemClick(object sender, ItemClickEventArgs e)
		{
			ColorWheelForm form = new ColorWheelForm();

			form.StartPosition = FormStartPosition.CenterParent;
			form.SkinMaskColor = UserLookAndFeel.Default.SkinMaskColor;
			form.SkinMaskColor2 = UserLookAndFeel.Default.SkinMaskColor2;

			var dialogResult = form.ShowDialog(this);

			if (dialogResult == DialogResult.OK)
			{
				this.AppContext.UserSettings.RibbonSkinMaskColor = UserLookAndFeel.Default.SkinMaskColor.ToArgb();
				this.AppContext.UserSettings.RibbonSkinMaskColor2 = UserLookAndFeel.Default.SkinMaskColor2.ToArgb();
				this.AppContext.UserSettings.Save();
			}
		}

		private async Task SetServerState()
		{
			ServerState servertState = await this.AppClient.GetServerState();

			this.InitializeServerControls(connected: true, isServerStarted: servertState == ServerState.Started);
		}

		private async ValueTask LoadUserSessions()
		{
			await lockSesion.WaitAsync(); //Use SemaphoreSlim to ensure loading can never occur concurrently with another load

			try
			{
				//this.InitializeServiceControls(Program.MonitorClient.IsConnected);

				this.userSessionInfoRowsBySessionKey.Clear();
				this.dataSourceUserSessions = await this.CreateUserSessionInfoRowBindingList();

				foreach (var item in dataSourceUserSessions)
					this.userSessionInfoRowsBySessionKey.Add(item.SessionKey, item);

				this.gridControlUserSessions.BeginInvoke(new Action(() =>
				{
					this.gridControlUserSessions.DataSource = null;
					this.gridControlUserSessions.DataSource = this.dataSourceUserSessions;
				}));

			}
			finally
			{
				this.gridControlUserSessions.RefreshDataSource();

				lockSesion.Release();
			}
		}

		private async ValueTask<bool> TryConnectToServerAndAuthorize()
		{
			bool connected = false;
			bool authorized = false;
			bool escape = false;
			int retry = 0;
			int numOfRetry = 3;
			string server = AppContext.UserSettings.Server;
			int port = AppContext.UserSettings.Port;
			string username = AppContext.UserSettings.LastUsername;
			FormLogin? formLogin = null;

			try
			{
				if (this.AppClient.IsConnected)
					await this.AppClient.CloseAsync();

				while ((!connected || !authorized) && retry++ < numOfRetry)
				{
#if DEBUG
					IPEndPoint endPoint = IpHelper.ParseIpEndPoint(server, port);

					connected = (this.AppClient.IsConnected) ? true : await this.AppClient.ConnectAsync(endPoint);

					if (connected)
					{
						var response = await this.AppClient.AuthenticateSession(username, "manager");

						authorized = response.ResponseSucceeded && response.IsAuthenticated;
					}

					escape = !(connected && authorized);
#else
					formLogin = new FormLogin(MonitorAppContext.Instance.AppName, server, username);
					formLogin.Focus();
					DialogResult loginDialogResult = formLogin.ShowDialog();

					if (loginDialogResult == DialogResult.OK)
					{
						server = formLogin.Server;
						username = formLogin.Username;

						IPEndPoint endPoint = IpHelper.ParseIpEndPoint(formLogin.Server, Program.DefaultMonitorPort);

						connected = (this.AppClient.IsConnected) ? true : await this.AppClient.ConnectAsync(endPoint);

						if (connected)
						{
                            var response = await this.AppClient.AuthenticateSession(formLogin.Username, formLogin.Password);

							authorized = response.ResponseSucceeded && response.IsAuthenticated;
						}
					}
					else
					{
						escape = true;
					}

					formLogin.Close();

					if (escape)
						break;
#endif
					if (!connected)
					{
						string serverInfo = formLogin?.Server ?? server;

						XtraMessageBox.Show("Could not connect to remote server " + serverInfo, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (!authorized)
					{
						XtraMessageBox.Show("Wrong username or password.", "Authorization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}

					if (escape)
						break;

					Thread.Sleep(500);
				}
			}
			catch (Exception ex)
			{
				XtraMessageBox.Show(ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

				if (formLogin != null)
					formLogin.Close();
			}
			finally
			{
				if (connected)
				{
					AppContext.UserSettings.Server = server;
					AppContext.UserSettings.Port = port;
				}

				if (authorized)
					AppContext.UserSettings.LastUsername = username;

				if (connected || authorized)
					AppContext.UserSettings.Save();
			}

			return connected && authorized;
		}


		#endregion |   Private Methods   |

		#region |   IMonitorSessionContext Interfaces Implementation   |

		ISimpleObjectSession? IMonitorSessionContext.GetSimpleObjectSession(long sessionKey)
		{
			SessionInfoRow? sessionInfoRow = this.GetSessionInfoRow(sessionKey);

			if (sessionInfoRow != null)
				return sessionInfoRow;
			else if (sessionKey == 0)
				return this.AppClient;

			return null;
		}

		ServerObjectModelInfo? IMonitorSessionContext.GetServerObjectModel(int tableId) => this.AppClient.GetServerObjectModel(tableId).GetAwaiter().GetResult();

		string IMonitorSessionContext.GetGraphName(int graphKey) => this.AppClient.GetGraphName(graphKey).GetAwaiter().GetResult();

		string IMonitorSessionContext.GetRelationName(int relationKey) => this.AppClient.GetRelationName(relationKey).GetAwaiter().GetResult();

		string IMonitorSessionContext.GetObjectName(int tableId, long objectId) => this.AppClient.GetObjectName(tableId, objectId).GetAwaiter().GetResult();

		#endregion |   IMonitorSessionContext Interfaces Implementation   |

		#region |   IFormMain Interface Implementation   |

		RibbonControl IFormMain.Ribbon => this.ribbonControl;

		BarToggleSwitchItem IFormMain.BarToggleSwitchItemDarkMode => this.barToggleSwitchItemDarkMode;

		BarToggleSwitchItem IFormMain.BarToggleSwitchItemCompactView => this.barToggleSwitchItemCompactView;

		#endregion |   IFormMain Interface Implementation   |
	}
}
