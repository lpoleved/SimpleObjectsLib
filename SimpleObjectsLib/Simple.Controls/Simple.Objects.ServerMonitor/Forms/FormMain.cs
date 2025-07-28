using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using DevExpress.Data.Utils;
using DevExpress.Drawing;
using DevExpress.Drawing.Printing;
using DevExpress.LookAndFeel;
using DevExpress.Printing;
using DevExpress.Tutorials.Controls;
using DevExpress.Utils;
using DevExpress.Utils.Taskbar;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.Gallery;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ColorWheel;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using SuperSocket.Server.Abstractions;
using Simple.AppContext;
using Simple.Network;
using Simple.SocketEngine;
using Simple.Controls;
using Simple.Objects.Controls;
using Simple.Objects.SocketProtocol;
using Simple.Objects.MonitorProtocol;
using DevExpress.Skins;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraScheduler.Commands;
using Simple.Objects.ServerMonitor.Interfaces;
using System.Drawing.Text;

namespace Simple.Objects.ServerMonitor
{
	public partial class FormMain : RibbonForm, IMonitorSessionContext, IFormMain
	{
		#region |   Private Members   |

		private const int MaxBestFitNodeCount = 200;

		private static SemaphoreSlim lockSesion = new SemaphoreSlim(1, 1);
		private static SemaphoreSlim lockRequestsResponses = new SemaphoreSlim(1, 1);
		private static SemaphoreSlim lockTransaction = new SemaphoreSlim(1, 1);

		private Dictionary<long, SessionInfoRow> userSessionInfoRowsBySessionKey = new Dictionary<long, SessionInfoRow>();

		private BindingList<SessionInfoRow> dataSourceUserSessions = new BindingList<SessionInfoRow>();
		private BindingList<PackageInfoRow> dataSourceRequestResponseMessagess = new BindingList<PackageInfoRow>();
		private BindingList<TransactionInfoRow> dataSourceTransactions = new BindingList<TransactionInfoRow>();

		//private PackageArgsFactory packageArgsFactory;
		private EditPanelFactory editPanelFactory;

		#endregion |   Private Members   |

		#region |   Public Static Members   |

		public static string FileNames = "RibbonMRUFiles.ini";
		public static string FolderNames = "RibbonMRUFolders.ini";

		#endregion |   Public Static Members   |

		#region |   Constructors and Initialization   |

		public FormMain()
		{
			InitializeComponent();

			this.AppContext = MonitorAppContext.Instance;
			this.CreateColorPopup(popupControlContainer1);
			this.InitSkinGallery();
			this.InitSkinMaskColor();
			this.InitFontGallery();
			this.InitColorGallery();
			this.InitEditors();
			this.InitColorSchemeCombo();
			this.InitPrint();
			this.InitializeAdvancedLookAndFeel();

			this.barSubItemChangeSkin2.Popup += this.BarSubItemChangeSkin_Popup;


			//Icon = DevExpress.Utils.ResourceImageHelper.CreateIconFromResourcesEx("DevExpress.XtraBars.Demos.RibbonSimplePad.AppIcon.ico", typeof(FormMainNew2).Assembly);
			Icon = Icon.FromHandle(global::Simple.Objects.ServerMonitor.Properties.Resources.SimpleObjects.GetHicon());

			this.SetSkinStyle();

			this.InitDarkModeControl();
			this.InitCompactViewControl();

			//this.SetEnableForSkinControls();

			//UserLookAndFeel.Default.SetSkinStyle("WXI");

			//this.packageArgsFactory = this.CreatePackageArgsFactory();
			this.editPanelFactory = this.CreateEditPanelFactory();

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
			this.InitializeServiceControls(this.AppClient.IsConnected);
			this.InitializeServerControls(this.AppClient.IsConnected, isServerStarted: false);

			this.SetEnableForSkinControls();

			// Set app name and copyright in about info
			string appName = String.Empty;
			string appVersion = String.Empty;
			string targetFramework = String.Empty;
			string copyright = String.Empty;
			object[] attribs = this.GetType().Assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyProductAttribute), true);

			if (attribs.Length > 0)
				appName = ((System.Reflection.AssemblyProductAttribute)attribs[0]).Product;

			attribs = this.GetType().Assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyFileVersionAttribute), true);

			if (attribs.Length > 0)
				appVersion = ((System.Reflection.AssemblyFileVersionAttribute)attribs[0]).Version;

			attribs = this.GetType().Assembly.GetCustomAttributes(typeof(System.Runtime.Versioning.TargetFrameworkAttribute), true);

			if (attribs.Length > 0)
				targetFramework = ((System.Runtime.Versioning.TargetFrameworkAttribute)attribs[0]).FrameworkDisplayName ?? "Unknown";

			attribs = this.GetType().Assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyCopyrightAttribute), true);

			if (attribs.Length > 0)
				copyright = ((System.Reflection.AssemblyCopyrightAttribute)attribs[0]).Copyright;

			this.recentLabelItemAppNameAndVersion.Caption = $"{appName}, Version {appVersion}, Target Framework {targetFramework}";
			this.recentLabelItemCopyrigjht.Caption = copyright; //  AssemblyInfo.AssemblyCopyright;
		}

		#endregion |   Constructors and Initialization   |

		#region |   Public Properties   |

		public virtual IClientAppContext AppContext { get; protected set; }

		public SimpleObjectMonitorClient AppClient => Program.MonitorClient;

		//public PackageArgsFactory PackageArgsFactory => this.packageArgsFactory;

		public EditPanelFactory EditPanelFactory => this.editPanelFactory;

		#endregion |   Public Properties   |

		#region |   Protected Methods   |

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			SplashScreenManager.CloseForm(false);
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			//Ribbon.MessageClosed += OnRibbonMessageClosed;
			//ShowMessage(WhatsNewMessage, 0);
			//ShowMessage(DocumentationMessage, 5000);
		}

		protected void SetSkinStyle() => this.SetSkinStyle((RibbonControlStyle)this.AppContext.UserSettings.RibbonStyle);

		protected void SetSkinStyle(RibbonControlStyle ribbonStyle)
		{
			string skinName = this.AppContext.UserSettings.RibbonSkinName;

			this.Ribbon.RibbonStyle = (ribbonStyle == RibbonControlStyle.Default) ? RibbonControlStyle.Office2019 : ribbonStyle;
			this.Ribbon.ColorScheme = Conversion.TryChangeType<RibbonControlColorScheme>(this.AppContext.UserSettings.RibbonColorScheme, defaultValue: RibbonControlColorScheme.Default);
			WindowsFormsSettings.CompactUIMode = (this.AppContext.UserSettings.CompactUI) ? DefaultBoolean.True : DefaultBoolean.False;

			if (skinName == null || skinName.Length == 0)
				skinName = this.AppContext.UserSettings.DefaultRibonSkinName;

			if (this.AppContext.UserSettings.DarkMode)
				skinName = this.GetDarkModeSkinName();

			if (skinName != UserLookAndFeel.Default.ActiveSkinName)
			{
				string oldSkinName = UserLookAndFeel.Default.ActiveSkinName;

				if (!this.AppContext.UserSettings.DarkMode)
				{
					this.AppContext.UserSettings.RibbonSkinName = skinName;
					this.AppContext.UserSettings.Save();
				}

				UserLookAndFeel.Default.SetSkinStyle(skinName);
				this.SetEnableForSkinControls();
			}
		}

		protected virtual string GetDarkModeSkinName()
		{
			return this.AppContext.UserSettings.DarkModeSkinName;
		}

		// TODO: Check why both skinPaletteRibbonGalleryBarItem's needed

		protected virtual void OnRibbonSkinNameChange(string skinName, string oldSkinName)
		{
			this.SetEnableForSkinControls();
			this.AppContext.UserSettings.RibbonSkinName = skinName;
			this.AppContext.UserSettings.RibbonSkinPaletteName = UserLookAndFeel.Default.ActiveSvgPaletteName;
			this.AppContext.UserSettings.Save();
		}

		private void SetEnableForSkinControls()
		{
			bool enabled = !this.AppContext.UserSettings.DarkMode;

			this.barSubItemChangeSkin.Enabled = enabled;
			this.barSubItemChangeSkin2.Enabled = enabled;

			this.rgbiSkins.Enabled = enabled;
			this.rgbiSkins2.Enabled = enabled;
			this.biStyle.Enabled = enabled;

			if (this.skinPaletteRibbonGalleryBarItem2.Enabled)
				this.skinPaletteRibbonGalleryBarItem2.Visibility = BarItemVisibility.Always;
			else
				this.skinPaletteRibbonGalleryBarItem2.Visibility = BarItemVisibility.Never;

			string skinName = this.ribbonControl.GetController().LookAndFeel.ActiveSkinName;

			if (skinName.Contains("Office 2010") || skinName.Contains("Office 2013") || skinName.Contains("Office 2016"))
				this.rgbiColorScheme.Visibility = BarItemVisibility.Always;
			else
				this.rgbiColorScheme.Visibility = BarItemVisibility.Never;
		}

		#endregion |   Protected Methods   |

		#region RibbonSimplePad

		void OnRibbonMessageClosed(object sender, RibbonMessageClosedArgs e)
		{
			if (e.Message == WhatsNewMessage && e.Result == DialogResult.OK)
				DevExpress.Data.Utils.SafeProcess.Start("https://www.devexpress.com/new#winforms");
			if (e.Message == DocumentationMessage && e.Result == DialogResult.Yes)
				DevExpress.Data.Utils.SafeProcess.Start("https://docs.devexpress.com/WindowsForms/DevExpress.XtraBars.Ribbon.RibbonControl.ShowMessage(DevExpress.XtraBars.Ribbon.RibbonMessageArgs)");
		}

		void ShowMessage(RibbonMessageArgs message, int delay)
		{
			if (delay == 0)
			{
				Ribbon.ShowMessage(message);
				return;
			}

			var timer = new System.Windows.Forms.Timer();

			timer.Interval = delay;
			timer.Tick += delegate
			{
				Ribbon.ShowMessage(message);
				timer.Stop();
				timer.Dispose();
			};
			timer.Start();
		}

		RibbonMessageArgs wnMessage;
		RibbonMessageArgs WhatsNewMessage
		{
			get
			{
				if (wnMessage == null)
				{
					wnMessage = new RibbonMessageArgs();
					wnMessage.Caption = "What's New";
					wnMessage.Text = "Explore new WinForms-related features that we introduced in our recent major update.";
					wnMessage.ImageOptions.SvgImage = barButtonItemInfo.ImageOptions.SvgImage;
					wnMessage.Buttons = new DialogResult[] { DialogResult.OK };
					wnMessage.Showing += delegate (object sender, RibbonMessageShowingArgs e)
					{
						BarButtonItem button = e?.Buttons[DialogResult.OK];
						if (button == null) return;
						button.Caption = "Explore Roadmap";
					};
				}
				return wnMessage;
			}
		}

		RibbonMessageArgs docMessage;
		RibbonMessageArgs DocumentationMessage
		{
			get
			{
				if (docMessage == null)
				{
					docMessage = new RibbonMessageArgs();
					docMessage.Text = "Want to know how to display alerts within the Ribbon Message Bar?";
					docMessage.Icon = MessageBoxIcon.Question;
					docMessage.ImageOptions.SvgImage = recentPinItem2.ImageOptions.ItemNormal.SvgImage;
					docMessage.Buttons = new DialogResult[] { DialogResult.Yes, DialogResult.No };
					docMessage.Showing += delegate (object sender, RibbonMessageShowingArgs e)
					{
						Ribbon.Messages[docMessage].ShowCloseItem = false;
					};
				}
				return docMessage;
			}
		}

		private void InitColorSchemeCombo()
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
					{
						g.FillRectangle(b, rect);
					}
				}

				this.rgbiColorScheme.Gallery.Groups[0].Items[(int)value].Value = value;
				this.rgbiColorScheme.Gallery.Groups[0].Items[(int)value].ImageOptions.Image = bmp;
			}

			this.rgbiColorScheme.Gallery.SetItemCheck(this.rgbiColorScheme.Gallery.Groups[0].Items[this.AppContext.UserSettings.RibbonColorScheme], true);
			this.ribbonControl.GetController().Changed += OnRibbonControllerChanged;
		}

		private void InitializeAdvancedLookAndFeel()
		{
			// Initializes corresponding skin settings/selectors https://docs.devexpress.com/WindowsForms/117178/build-an-application/application-skins/add-and-customize-the-ribbon-gallery-skin-selector#display-advanced-skin-options

			SkinHelper.InitTrackWindowsAppMode(bciTrackWindowsAppMode);
			SkinHelper.InitResetToOriginalPalette(bciResetToOriginalPalette);
			SkinHelper.InitTrackWindowsAccentColor(bciTrackWindowsAccentColor);
			SkinHelper.InitCustomAccentColor(Ribbon.Manager, bbiSystemAccentColor);
			SkinHelper.InitCustomAccentColor2(Ribbon.Manager, bbiAccentCustomColor2);

			this.bciTrackWindowsAppMode.Checked = this.AppContext.UserSettings.TrackWindowsAppMode;
			this.bciResetToOriginalPalette.Checked = this.AppContext.UserSettings.ResetToOriginalPalette;
			this.bciTrackWindowsAccentColor.Checked = this.AppContext.UserSettings.TrackWindowsAccentColor;

			if (this.AppContext.UserSettings.SystemAccentColor != this.AppContext.UserSettings.DefaultSystemAccentColor)
				DevExpress.XtraEditors.WindowsFormsSettings.SetAccentColor(Color.FromArgb(this.AppContext.UserSettings.SystemAccentColor));

			if (this.AppContext.UserSettings.SystemAccentColor2 != this.AppContext.UserSettings.DefaultSystemAccentColor2)
				DevExpress.XtraEditors.WindowsFormsSettings.SetAccentColor2(Color.FromArgb(this.AppContext.UserSettings.SystemAccentColor2));

			this.bciTrackWindowsAppMode.CheckedChanged += BciTrackWindowsAppMode_CheckedChanged; ;
			this.bciResetToOriginalPalette.CheckedChanged += BciResetToOriginalPalette_CheckedChanged;
			this.bciTrackWindowsAccentColor.CheckedChanged += BciTrackWindowsAccentColor_CheckedChanged;
		}

		private void BciTrackWindowsAppMode_CheckedChanged(object sender, ItemClickEventArgs e)
		{
			this.AppContext.UserSettings.TrackWindowsAppMode = this.bciTrackWindowsAppMode.Checked;
			this.AppContext.UserSettings.Save();
		}

		private void BciResetToOriginalPalette_CheckedChanged(object sender, ItemClickEventArgs e)
		{
			this.AppContext.UserSettings.ResetToOriginalPalette = this.bciResetToOriginalPalette.Checked;
			this.AppContext.UserSettings.Save();
		}

		private void BciTrackWindowsAccentColor_CheckedChanged(object sender, ItemClickEventArgs e)
		{
			this.AppContext.UserSettings.TrackWindowsAccentColor = this.bciTrackWindowsAccentColor.Checked;
			this.AppContext.UserSettings.Save();
		}

		private void SetSkinPalette(string paletteName)
		{
			var skin = CommonSkins.GetSkin(UserLookAndFeel.Default);
			DevExpress.Utils.Svg.SvgPalette? palette = skin.CustomSvgPalettes[paletteName];

			if (palette != null)
			{
				skin.SvgPalettes[Skin.DefaultSkinPaletteName].SetCustomPalette(palette);
				LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
			}
		}

		private void InitDarkModeControl()
		{
			this.barToggleSwitchItemDarkMode.Checked = this.AppContext.UserSettings.DarkMode;
		}

		private void InitCompactViewControl()
		{
			this.barToggleSwitchItemCompactView.Checked = this.AppContext.UserSettings.CompactUI;
		}

		private void OnRibbonControllerChanged(object sender, EventArgs e)
		{
			bool enable = this.skinPaletteRibbonGalleryBarItem2.Enabled;


			//if (this.Ribbon.RibbonStyle == RibbonControlStyle.Office2010 || this.ribbonControl.GetController().LookAndFeel.ActiveSkinName.Contains("Office 2010") || this.ribbonControl.GetController().LookAndFeel.ActiveSkinName == "Office 2016 Colorful")
			//	this.rgbiColorScheme.Visibility = BarItemVisibility.Always;
			//else
			//this.rgbiColorScheme.Visibility = BarItemVisibility.Never;
		}

		private void rgbiColorScheme_GalleryItemClick(object sender, GalleryItemClickEventArgs e)
		{
			RibbonControlColorScheme ribbonControlColorScheme = Conversion.TryChangeType<RibbonControlColorScheme>(e.Item.Value, defaultValue: RibbonControlColorScheme.Default);

			this.AppContext.UserSettings.RibbonColorScheme = (int)ribbonControlColorScheme;
			this.AppContext.UserSettings.Save();
		}

		int documentIndex = 0;
		PopupColorNew2 pc;
		frmFind dlgFind = null;
		frmReplace dlgReplace = null;
		GalleryItem fCurrentFontItem, fCurrentColorItem;
		string DocumentName { get { return string.Format("New Document {0}", documentIndex); } }

		void CreateNewDocument()
		{
			CreateNewDocument(null);
		}
		void InitEditors()
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
			biStyle.EditValue = ribbonControl.RibbonStyle;
		}
		public void ShowHideFormatCategory()
		{
			RibbonPageCategory selectionCategory = Ribbon.PageCategories[0] as RibbonPageCategory;
			if (selectionCategory == null) return;
			if (CurrentRichTextBox == null)
				selectionCategory.Visible = false;
			else
				selectionCategory.Visible = CurrentRichTextBox.SelectionLength != 0;
			if (selectionCategory.Visible) Ribbon.SelectedPage = selectionCategory.Pages[0];
		}
		void CreateNewDocument(string fileName)
		{
			documentIndex++;
			frmPad pad = new frmPad();
			if (fileName != null)
				pad.LoadDocument(fileName);
			else
				pad.DocName = DocumentName;
			pad.MdiParent = this;
			pad.Closed += new EventHandler(Pad_Closed);
			pad.ShowPopupMenu += new EventHandler(Pad_ShowPopupMenu);
			pad.ShowMiniToolbar += new EventHandler(pad_ShowMiniToolbar);
			pad.Show();
			InitNewDocument(pad.RTBMain);
		}

		void pad_ShowMiniToolbar(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(((RichTextBox)sender).SelectedText))
				return;
			ShowSelectionMiniToolbar();
		}

		void Pad_Closed(object sender, EventArgs e)
		{
			CloseFind();
		}
		void Pad_ShowPopupMenu(object sender, EventArgs e)
		{
			pmMain.RibbonToolbar = selectionMiniToolbar;
			pmMain.ShowPopup(Control.MousePosition);
		}
		void CloseFind()
		{
			if (dlgFind != null && dlgFind.RichText != CurrentRichTextBox)
			{
				dlgFind.Close();
				dlgFind = null;
			}
			if (dlgReplace != null && dlgReplace.RichText != CurrentRichTextBox)
			{
				dlgReplace.Close();
				dlgReplace = null;
			}
		}

		private void CreateColorPopup(PopupControlContainer container)
		{
			pc = new PopupColorNew2(container, this);
		}

		#endregion RibbonSimplePad

		#region Init

		private void frmMain_Activated(object sender, System.EventArgs e)
		{
			InitPaste();
		}
		public void UpdateText()
		{
			ribbonControl.ApplicationCaption = "Ribbon Simple Pad";
			ribbonControl.ApplicationDocumentCaption = CurrentDocName + (CurrentModified ? "*" : "");
			//Text = string.Format("Ribbon Simple Pad ({0})", CurrentDocName);
			siDocName.Caption = string.Format("  {0}", CurrentDocName);
		}
		void ChangeActiveForm()
		{
			UpdateText();
			InitCurrentDocument(CurrentRichTextBox);
			rtPad_SelectionChanged(CurrentRichTextBox, EventArgs.Empty);
			CloseFind();
		}
		private void xtraTabbedMdiManager1_FloatMDIChildActivated(object sender, EventArgs e)
		{
			ChangeActiveForm();
		}
		private void xtraTabbedMdiManager1_FloatMDIChildDeactivated(object sender, EventArgs e)
		{
			BeginInvoke(new MethodInvoker(ChangeActiveForm));
		}
		private void frmMain_MdiChildActivate(object sender, System.EventArgs e)
		{
			ChangeActiveForm();
		}
		void rtPad_SelectionChanged(object sender, System.EventArgs e)
		{
			ShowHideFormatCategory();
			RichTextBox rtPad = sender as RichTextBox;
			InitFormat();
			int line = 0, col = 0;

			if (rtPad != null)
			{
				InitEdit(rtPad.SelectionLength > 0);
				line = rtPad.GetLineFromCharIndex(rtPad.SelectionStart) + 1;
				col = rtPad.SelectionStart + 1;
			}
			else
			{
				InitEdit(false);
			}
			siPosition.Caption = string.Format("   Line: {0}  Position: {1}   ", line, col);
			CurrentFontChanged();
		}

		protected virtual void ShowSelectionMiniToolbar()
		{
			Point pt = Control.MousePosition;
			pt.Offset(0, -11);
			selectionMiniToolbar.Alignment = ContentAlignment.TopRight;
			selectionMiniToolbar.PopupMenu = null;
			selectionMiniToolbar.Show(pt);
		}
		void rtPad_TextChanged(object sender, System.EventArgs e)
		{
			if (CurrentForm == null) return;
			CurrentForm.Modified = true;
			InitCurrentDocument(CurrentRichTextBox);
		}

		protected void InitFormat()
		{
			iBold.Enabled = SelectFont != null;
			iItalic.Enabled = SelectFont != null;
			iUnderline.Enabled = SelectFont != null;
			iFont.Enabled = SelectFont != null;
			iFontColor.Enabled = SelectFont != null;
			if (SelectFont != null)
			{
				iBold.Down = SelectFont.Bold;
				iItalic.Down = SelectFont.Italic;
				iUnderline.Down = SelectFont.Underline;
			}
			bool _enabled = CurrentRichTextBox != null;
			iProtected.Enabled = _enabled;
			iBullets.Enabled = _enabled;
			iAlignLeft.Enabled = _enabled;
			iAlignRight.Enabled = _enabled;
			iCenter.Enabled = _enabled;
			rgbiFont.Enabled = _enabled;
			rgbiFontColor.Enabled = _enabled;
			ribbonPageGroup9.ShowCaptionButton = _enabled;
			rpgFont.ShowCaptionButton = _enabled;
			rpgFontColor.ShowCaptionButton = _enabled;
			if (!_enabled) ClearFormats();
			if (CurrentRichTextBox != null)
			{
				iProtected.Down = CurrentRichTextBox.SelectionProtected;
				iBullets.Down = CurrentRichTextBox.SelectionBullet;
				switch (CurrentRichTextBox.SelectionAlignment)
				{
					case HorizontalAlignment.Left:
						iAlignLeft.Down = true;
						break;
					case HorizontalAlignment.Center:
						iCenter.Down = true;
						break;
					case HorizontalAlignment.Right:
						iAlignRight.Down = true;
						break;
				}
			}
		}

		void ClearFormats()
		{
			iBold.Down = false;
			iItalic.Down = false;
			iUnderline.Down = false;
			iProtected.Down = false;
			iBullets.Down = false;
			iAlignLeft.Down = false;
			iAlignRight.Down = false;
			iCenter.Down = false;
		}

		protected void InitPaste()
		{
			bool enabledPase = CurrentRichTextBox != null && CurrentRichTextBox.CanPaste(DataFormats.GetFormat(0));
			iPaste.Enabled = enabledPase;
			sbiPaste.Enabled = enabledPase;
		}

		void InitUndo()
		{
			iUndo.Enabled = CurrentRichTextBox != null ? CurrentRichTextBox.CanUndo : false;
			iLargeUndo.Enabled = iUndo.Enabled;
		}
		protected void InitEdit(bool enabled)
		{
			iCut.Enabled = enabled;
			iCopy.Enabled = enabled;
			iClear.Enabled = enabled;
			iSelectAll.Enabled = CurrentRichTextBox != null ? CurrentRichTextBox.CanSelect : false;
			InitUndo();
		}

		void InitNewDocument(RichTextBox rtbControl)
		{
			rtbControl.SelectionChanged += new System.EventHandler(this.rtPad_SelectionChanged);
			rtbControl.TextChanged += new System.EventHandler(this.rtPad_TextChanged);
		}

		void InitCurrentDocument(RichTextBox rtbControl)
		{
			bool _enabled = rtbControl != null;
			iSaveAs.Enabled = _enabled;
			iClose.Enabled = _enabled;
			iPrint.Enabled = _enabled;
			sbiSave.Enabled = _enabled;
			sbiFind.Enabled = _enabled;
			iFind.Enabled = _enabled;
			iReplace.Enabled = _enabled;
			iSave.Enabled = CurrentModified;
			SetModifiedCaption();
			InitPaste();
			InitFormat();
		}

		void SetModifiedCaption()
		{
			if (CurrentForm == null)
			{
				barButtonItemInfo.Caption = "";
				return;
			}
			barButtonItemInfo.Caption = CurrentModified ? "   Modified   " : "";
		}
		#endregion

		#region Properties
		frmPad CurrentForm
		{
			get
			{
				if (this.ActiveMdiChild == null) return null;
				if (xtraTabbedMdiManager1.ActiveFloatForm != null)
					return xtraTabbedMdiManager1.ActiveFloatForm as frmPad;
				return this.ActiveMdiChild as frmPad;
			}
		}

		public RichTextBox? CurrentRichTextBox
		{
			get
			{
				if (CurrentForm == null) return null;
				return CurrentForm.RTBMain;
			}
		}

		string CurrentDocName
		{
			get
			{
				if (CurrentForm == null) return "";
				return CurrentForm.DocName;
			}
		}

		bool CurrentModified
		{
			get
			{
				if (CurrentForm == null) return false;
				return CurrentForm.Modified;
			}
		}
		#endregion

		#region File
		void idNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			CreateNewDocument();
		}

		void iClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (CurrentForm != null) CurrentForm.Close();
		}

		void OpenFile()
		{
			OpenFileFolder(string.Empty);
		}

		public void OpenFile(string name)
		{
			CreateNewDocument(name);
			AddToMostRecentFiles(name, arMRUList);
			AddToMostRecentFiles(name, null);
			AddToMostRecentFolders(name, null);
		}
		private void AddToMostRecentFiles(string name, MRUArrayList arMRUList)
		{
			if (arMRUList != null) arMRUList.InsertElement(name);
			RecentPinItem pinItem = new RecentPinItem() { Caption = GetFileName(name), Description = name, PinButtonChecked = false };
			if (CheckForOverlap(pinItem, recentTabItem1.TabPanel.Items)) return;
			recentTabItem1.TabPanel.Items.Insert(FindFirstUnCheckedIntemIndex(recentTabItem1.TabPanel), pinItem);
		}

		bool CheckForOverlap(RecentPinItem pinItem, RecentItemCollection recentItemCollection)
		{
			foreach (RecentItemBase item in recentItemCollection)
			{
				RecentPinItem pItem = item as RecentPinItem;
				if (pItem != null && pinItem.Caption == pItem.Caption && pinItem.Description == pItem.Description)
					return true;
			}
			return false;
		}
		private void AddToMostRecentFolders(string name, MRUArrayList arMRUList)
		{
			if (arMRUList != null)
			{
				name = Path.GetFullPath(name);
				arMRUList.InsertElement(Path.GetDirectoryName(name));
			}
			name = Path.GetDirectoryName(Path.GetFullPath(name));
			RecentPinItem pinItem = new RecentPinItem() { Caption = GetFileName(name), Description = name, PinButtonChecked = false };
			if (CheckForOverlap(pinItem, recentTabItem2.TabPanel.Items)) return;
			RecentPinItem pinItem_ = new RecentPinItem() { Caption = GetFileName(name), Description = name, PinButtonChecked = false };
			recentTabItem2.TabPanel.Items.Insert(FindFirstUnCheckedIntemIndex(recentTabItem2.TabPanel), pinItem);
			recentTabItem3.TabPanel.Items.Insert(FindFirstUnCheckedIntemIndex(recentTabItem3.TabPanel), pinItem_);
		}

		void iOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			OpenFile();
		}

		private void iPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			XtraMessageBox.Show(this, "Note that you can use the XtraPrinting Library to print the contents of the standard RichTextBox control.\r\nFor more information, see the main XtraPrinting demo.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		void iSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Save();
		}
		void iSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SaveAs();
		}
		void Save()
		{
			if (CurrentForm == null) return;
			if (CurrentForm.NewDocument)
			{
				SaveAs();
			}
			else
			{
				CurrentRichTextBox.SaveFile(CurrentDocName, RichTextBoxStreamType.RichText);
				CurrentForm.Modified = false;
			}
			SetModifiedCaption();
		}
		void SaveAs()
		{
			SaveAs(string.Empty);
		}
		void SaveAs(string path)
		{
			if (CurrentForm != null)
			{
				string s = CurrentForm.SaveAs(path);
				if (s != string.Empty)
				{
					AddToMostRecentFiles(s, arMRUList);
					AddToMostRecentFiles(s, null);
					AddToMostRecentFolders(s, null);
				}
				UpdateText();
			}
		}
		private void iExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Close();
		}
		private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Save advanced ribbon skin settings
			this.AppContext.UserSettings.TrackWindowsAppMode = this.bciTrackWindowsAppMode.Checked;
			this.AppContext.UserSettings.ResetToOriginalPalette = this.bciResetToOriginalPalette.Checked;
			this.AppContext.UserSettings.TrackWindowsAccentColor = this.bciTrackWindowsAccentColor.Checked;
			this.AppContext.UserSettings.SystemAccentColor = DevExpress.XtraEditors.WindowsFormsSettings.GetAccentColor().ToArgb();
			this.AppContext.UserSettings.SystemAccentColor2 = DevExpress.XtraEditors.WindowsFormsSettings.GetAccentColor2().ToArgb();
			this.AppContext.UserSettings.Save();
		}


		private void ribbonPageGroup1_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e)
		{
			OpenFile();
		}

		private void ribbonPageGroup9_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e)
		{
			SaveAs();
		}
		#endregion

		#region Format
		private FontStyle rtPadFontStyle()
		{
			FontStyle fs = new FontStyle();
			if (iBold.Down) fs |= FontStyle.Bold;
			if (iItalic.Down) fs |= FontStyle.Italic;
			if (iUnderline.Down) fs |= FontStyle.Underline;
			return fs;
		}

		private void iBullets_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (CurrentRichTextBox == null) return;
			CurrentRichTextBox.SelectionBullet = iBullets.Down;
			InitUndo();
		}

		private void iFontStyle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (CurrentRichTextBox == null) return;
			CurrentRichTextBox.SelectionFont = new Font(SelectFont, rtPadFontStyle());
		}

		private void iProtected_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (CurrentRichTextBox == null) return;
			CurrentRichTextBox.SelectionProtected = iProtected.Down;
		}

		private void iAlign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (CurrentRichTextBox == null) return;
			if (iAlignLeft.Down)
				CurrentRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
			if (iCenter.Down)
				CurrentRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
			if (iAlignRight.Down)
				CurrentRichTextBox.SelectionAlignment = HorizontalAlignment.Right;
			InitUndo();
		}


		protected Font SelectFont
		{
			get
			{
				if (CurrentRichTextBox != null)
					return CurrentRichTextBox.SelectionFont;
				return null;
			}
		}
		void ShowFontDialog()
		{
			if (CurrentRichTextBox == null) return;
			Font dialogFont = null;
			if (SelectFont != null)
				dialogFont = (Font)SelectFont.Clone();
			else dialogFont = CurrentRichTextBox.Font;
			XtraFontDialog dlg = new XtraFontDialog(dialogFont);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				CurrentRichTextBox.SelectionFont = dlg.ResultFont;
				beiFontSize.EditValue = dlg.ResultFont.Size;
			}
		}
		private void iFont_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			ShowFontDialog();
		}
		private void iFontColor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (CurrentRichTextBox == null) return;
			CurrentRichTextBox.SelectionColor = ((IPopupColorPickEdit)pc).Color;
		}
		#endregion

		#region Edit
		private void iUndo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (CurrentRichTextBox == null) return;
			CurrentRichTextBox.Undo();
			CurrentForm.Modified = CurrentRichTextBox.CanUndo;
			SetModifiedCaption();
			InitUndo();
			InitFormat();
		}

		private void iCut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (CurrentRichTextBox == null) return;
			CurrentRichTextBox.Cut();
			InitPaste();
		}

		private void iCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (CurrentRichTextBox == null) return;
			CurrentRichTextBox.Copy();
			InitPaste();
		}

		private void iPaste_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (CurrentRichTextBox == null) return;
			CurrentRichTextBox.Paste();
		}

		private void iClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (CurrentRichTextBox == null) return;
			CurrentRichTextBox.SelectedRtf = "";
		}

		private void iSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (CurrentRichTextBox == null) return;
			CurrentRichTextBox.SelectAll();
		}
		private void ribbonPageGroup2_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e)
		{
			pmMain.ShowPopup(ribbonControl.Manager, MousePosition);
		}
		#endregion

		#region SkinGallery

		private void InitSkinGallery()
		{
			DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(rgbiSkins, true);
			DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(rgbiSkins2, true);
		}

		private void InitSkinMaskColor()
		{
			if (this.AppContext.UserSettings.RibbonSkinMaskColor != this.AppContext.UserSettings.DefaultRibbonSkinMaskColor)
				UserLookAndFeel.Default.SkinMaskColor = Color.FromArgb(this.AppContext.UserSettings.RibbonSkinMaskColor);

			if (this.AppContext.UserSettings.RibbonSkinMaskColor2 != this.AppContext.UserSettings.DefaultRibbonSkinMaskColor2)
				UserLookAndFeel.Default.SkinMaskColor2 = Color.FromArgb(this.AppContext.UserSettings.RibbonSkinMaskColor2);
		}

		#endregion

		#region FontGallery
		Image GetFontImage(int width, int height, string fontName, int fontSize)
		{
			Rectangle rect = new Rectangle(0, 0, width, height);
			Image fontImage = new Bitmap(width, height);
			try
			{
				using (Font fontSample = new Font(fontName, fontSize))
				{
					Graphics g = Graphics.FromImage(fontImage);
					g.FillRectangle(Brushes.White, rect);
					using (StringFormat fs = new StringFormat())
					{
						fs.Alignment = StringAlignment.Center;
						fs.LineAlignment = StringAlignment.Center;
						//g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
						g.DrawString("Aa", fontSample, Brushes.Black, rect, fs);
						g.Dispose();
					}
				}
			}
			catch { }
			return fontImage;
		}
		void InitFont(GalleryItemGroup groupDropDown, GalleryItemGroup galleryGroup)
		{
			FontFamily[] fonts = FontFamily.Families;
			for (int i = 0; i < fonts.Length; i++)
			{
				if (!FontFamily.Families[i].IsStyleAvailable(FontStyle.Regular)) continue;
				string fontName = fonts[i].Name;
				GalleryItem item = new GalleryItem();
				item.Caption = fontName;
				item.Image = GetFontImage(40, 40, fontName, 16);
				item.HoverImage = item.Image;
				item.Description = fontName;
				item.Hint = fontName;
				try
				{
					item.Tag = new Font(fontName, 9);
					if (DevExpress.Utils.ControlUtils.IsSymbolFont((Font)item.Tag))
					{
						item.Tag = new Font(DevExpress.Utils.AppearanceObject.DefaultFont.FontFamily, 9);
						item.Description += " (Symbol Font)";
					}
				}
				catch
				{
					continue;
				}
				groupDropDown.Items.Add(item);
				galleryGroup.Items.Add(item);
			}
		}
		void InitFontGallery()
		{
			gddFont.Gallery.BeginUpdate();
			rgbiFont.Gallery.BeginUpdate();
			try
			{
				InitFont(gddFont.Gallery.Groups[0], rgbiFont.Gallery.Groups[0]);
			}
			finally
			{
				gddFont.Gallery.EndUpdate();
				rgbiFont.Gallery.EndUpdate();
			}
			beiFontSize.EditValue = 8;
		}
		void SetFont(string fontName, GalleryItem item)
		{
			if (CurrentRichTextBox == null) return;
			CurrentRichTextBox.SelectionFont = new Font(fontName, Convert.ToInt32(beiFontSize.EditValue), rtPadFontStyle());
			if (item != null) CurrentFontItem = item;
		}
		private void gddFont_Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
		{
			SetFont(e.Item.Caption, e.Item);
		}
		private void rpgFont_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e)
		{
			ShowFontDialog();
		}
		private void rgbiFont_Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
		{
			SetFont(e.Item.Caption, e.Item);
		}
		private void gddFont_Gallery_CustomDrawItemText(object sender, GalleryItemCustomDrawEventArgs e)
		{
			DevExpress.XtraBars.Ribbon.ViewInfo.GalleryItemViewInfo itemInfo = e.ItemInfo as DevExpress.XtraBars.Ribbon.ViewInfo.GalleryItemViewInfo;
			itemInfo.PaintAppearance.ItemDescriptionAppearance.Normal.DrawString(e.Cache, e.Item.Description, itemInfo.DescriptionBounds);
			AppearanceObject app = itemInfo.PaintAppearance.ItemCaptionAppearance.Normal.Clone() as AppearanceObject;
			app.Font = (Font)e.Item.Tag;
			try
			{
				e.Cache.Graphics.DrawString(e.Item.Caption, app.Font, app.GetForeBrush(e.Cache), itemInfo.CaptionBounds);
			}
			catch { }
			e.Handled = true;
		}
		#endregion

		#region ColorGallery
		void InitColorGallery()
		{
			gddFontColor.BeginUpdate();
			foreach (Color color in DevExpress.XtraEditors.Popup.ColorListBoxViewInfo.WebColors)
			{
				if (color == Color.Transparent) continue;
				GalleryItem item = new GalleryItem();
				item.Caption = color.Name;
				item.Tag = color;
				item.Hint = color.Name;
				gddFontColor.Gallery.Groups[0].Items.Add(item);
				rgbiFontColor.Gallery.Groups[0].Items.Add(item);
			}
			foreach (Color color in DevExpress.XtraEditors.Popup.ColorListBoxViewInfo.SystemColors)
			{
				GalleryItem item = new GalleryItem();
				item.Caption = color.Name;
				item.Tag = color;
				gddFontColor.Gallery.Groups[1].Items.Add(item);
			}
			gddFontColor.EndUpdate();
		}
		private void gddFontColor_Gallery_CustomDrawItemImage(object sender, GalleryItemCustomDrawEventArgs e)
		{
			Color clr = (Color)e.Item.Tag;
			using (Brush brush = new SolidBrush(clr))
			{
				e.Cache.FillRectangle(brush, e.Bounds);
				e.Handled = true;
			}
		}
		void SetResultColor(Color color, GalleryItem item)
		{
			if (CurrentRichTextBox == null) return;
			CurrentRichTextBox.SelectionColor = color;
			if (item != null) CurrentColorItem = item;
		}
		private void gddFontColor_Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
		{
			SetResultColor((Color)e.Item.Tag, e.Item);
		}
		private void rpgFontColor_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e)
		{
			if (CurrentRichTextBox == null) return;
			if (pc == null)
				CreateColorPopup(popupControlContainer1);
			popupControlContainer1.ShowPopup(ribbonControl.Manager, MousePosition);
		}

		private void rgbiFontColor_Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
		{
			SetResultColor((Color)e.Item.Tag, e.Item);
		}
		#endregion

		#region |   AppClient Handling   |

		public ISimpleObjectSession? GetSessionContext(long sessionKey)
		{
			SessionInfoRow? sessionInfoRow = this.GetSessionInfoRow(sessionKey);

			if (sessionInfoRow != null)
				return sessionInfoRow;
			else if (sessionKey == 0)
				return this.AppClient;

			return null;
		}

		public SessionInfoRow? GetSessionInfoRow(long sessionKey)
		{
			if (this.userSessionInfoRowsBySessionKey.TryGetValue(sessionKey, out SessionInfoRow? value))
				return value;

			return default;
		}

		public ServerObjectModelInfo? GetServerObjectModel(int tableId) => this.AppClient.GetServerObjectModel(tableId).GetAwaiter().GetResult();
		public string GetGraphName(int graphKey) => this.AppClient.GetGraphName(graphKey).GetAwaiter().GetResult();

		public string GetRelationName(int relationKey) => this.AppClient.GetRelationName(relationKey).GetAwaiter().GetResult();

		public string GetObjectName(int tableId, long objectId) => this.AppClient.GetObjectName(tableId, objectId).GetAwaiter().GetResult();

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

		#endregion |   AppClient Handling   |

		#region |   Protected Methods   |

		//protected virtual PackageArgsFactory CreatePackageArgsFactory() => new PackageArgsFactory(new List<System.Reflection.Assembly>() { this.GetType().Assembly, System.Reflection.Assembly.Load("Simple.Objects.ServerMonitor") });
		//protected virtual PackageArgsFactory CreatePackageArgsFactory() => new PackageArgsFactory(this.GetType().Assembly);
		protected virtual EditPanelFactory CreateEditPanelFactory() => new EditPanelFactory();

		#endregion |   Protected Methods   |

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

		#endregion |   Private Methods   |

		#region RibbonSimplePad 2

		private void iFind_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (CurrentRichTextBox == null) return;
			if (dlgReplace != null) dlgReplace.Close();
			if (dlgFind != null) dlgFind.Close();
			dlgFind = new frmFind(CurrentRichTextBox, Bounds);
			AddOwnedForm(dlgFind);
			dlgFind.Show();
		}

		private void iReplace_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (CurrentRichTextBox == null) return;
			if (dlgReplace != null) dlgReplace.Close();
			if (dlgFind != null) dlgFind.Close();
			dlgReplace = new frmReplace(CurrentRichTextBox, Bounds);
			AddOwnedForm(dlgReplace);
			dlgReplace.Show();
		}

		string TextByCaption(string caption)
		{
			return caption.Replace("&", "");
		}

		private void frmMain_Load(object sender, System.EventArgs e)
		{
			this.LoadLocationAndSizeSettings();

			InitMostRecentFiles();
			arMRUList = new MRUArrayList(pcAppMenuFileLabels, imageCollection3.Images[0], imageCollection3.Images[1]);
			arMRUList.LabelClicked += new EventHandler(OnMRUFileLabelClicked);
			InitMostRecentFiles(arMRUList);
			ribbonControl.ForceInitialize();
			GalleryDropDown skins = new GalleryDropDown();
			skins.Ribbon = ribbonControl;
			DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGalleryDropDown(skins);
			barSubItemChangeSkin.DropDownControl = skins;
			skins.GalleryItemClick += Skins_GalleryItemClick;
			this.InitBarItemSkins();
			//CreateNewDocument();

			barEditItem1.EditValue = (Bitmap)DevExpress.Utils.ResourceImageHelper.CreateImageFromResources("DevExpress.XtraBars.Demos.RibbonSimplePad.online.gif", typeof(FormMain).Assembly);
		}

		private void InitBarItemSkins()
		{
			foreach (SkinContainer skin in SkinManager.Default.Skins)
			{
				BarCheckItem item = this.Ribbon.Items.CreateCheckItem(skin.SkinName, false);

				item.Tag = skin.SkinName;
				item.ItemClick += BarButtonChangeSkinClick;
				item.ImageOptions.Image = DevExpress.Skins.SkinCollectionHelper.GetSkinIcon(skin.SkinName, DevExpress.Skins.SkinIconsSize.Small);

				if (this.barSubItemChangeSkin2 != null)
					this.barSubItemChangeSkin2.ItemLinks.Add(item);
			}
		}

		private void BarButtonChangeSkinClick(object sender, ItemClickEventArgs e)
		{
			string oldSkinName = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName;
			string? skinName = e.Item.Tag.ToString();

			this.AppContext.UserSettings.RibbonSkinName = skinName;
			this.SetSkinStyle();
			this.OnRibbonSkinNameChange(skinName!, oldSkinName);
		}

		private void Skins_GalleryItemClick(object sender, GalleryItemClickEventArgs e)
		{
			string oldSkinName = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName;
			string? skinName = e.Item.Tag.ToString();

			this.OnRibbonSkinNameChange(skinName!, oldSkinName);
		}

		private void rgbiSkins2_GalleryItemClick(object sender, GalleryItemClickEventArgs e)
		{
			string oldSkinName = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName;
			string? skinName = e.Item.Tag.ToString();

			this.OnRibbonSkinNameChange(skinName!, oldSkinName);
		}

		private void rgbiSkins_GalleryItemClick(object sender, GalleryItemClickEventArgs e)
		{
			string oldSkinName = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName;
			string? skinName = e.Item.Tag.ToString();

			this.OnRibbonSkinNameChange(skinName!, oldSkinName);
		}

		private void skinPaletteRibbonGalleryBarItem2_GalleryItemCheckedChanged(object sender, GalleryItemEventArgs e)
		{
			this.AppContext.UserSettings.RibbonSkinPaletteName = (string)e.Item.Value;
			this.AppContext.UserSettings.Save();
		}

		private void InitMostRecentFiles(MRUArrayList arMRUList)
		{
			string fileName = Application.StartupPath + "\\" + MRUArrayList.MRUFileName;
			arMRUList.Init(fileName, "Document1.rtf");
		}

		#endregion RibbonSimplePad 2

		#region GalleryItemsChecked

		GalleryItem GetColorItemByColor(Color color, BaseGallery gallery)
		{
			foreach (GalleryItemGroup galleryGroup in gallery.Groups)
				foreach (GalleryItem item in galleryGroup.Items)
					if (item.Caption == color.Name)
						return item;
			return null;
		}
		GalleryItem GetFontItemByFont(string fontName, BaseGallery gallery)
		{
			foreach (GalleryItemGroup galleryGroup in gallery.Groups)
				foreach (GalleryItem item in galleryGroup.Items)
					if (item.Caption == fontName)
						return item;
			return null;
		}
		GalleryItem CurrentFontItem
		{
			get { return fCurrentFontItem; }
			set
			{
				if (fCurrentFontItem == value) return;
				if (fCurrentFontItem != null) fCurrentFontItem.Checked = false;
				fCurrentFontItem = value;
				if (fCurrentFontItem != null)
				{
					fCurrentFontItem.Checked = true;
					MakeFontVisible(fCurrentFontItem);
				}
			}
		}
		void MakeFontVisible(GalleryItem item)
		{
			gddFont.Gallery.MakeVisible(fCurrentFontItem);
			rgbiFont.Gallery.MakeVisible(fCurrentFontItem);
		}
		GalleryItem CurrentColorItem
		{
			get { return fCurrentColorItem; }
			set
			{
				if (fCurrentColorItem == value) return;
				if (fCurrentColorItem != null) fCurrentColorItem.Checked = false;
				fCurrentColorItem = value;
				if (fCurrentColorItem != null)
				{
					fCurrentColorItem.Checked = true;
					MakeColorVisible(fCurrentColorItem);
				}
			}
		}
		void MakeColorVisible(GalleryItem item)
		{
			gddFontColor.Gallery.MakeVisible(fCurrentColorItem);
			rgbiFontColor.Gallery.MakeVisible(fCurrentColorItem);
		}
		void CurrentFontChanged()
		{
			if (CurrentRichTextBox == null || CurrentRichTextBox.SelectionFont == null) return;
			CurrentFontItem = GetFontItemByFont(CurrentRichTextBox.SelectionFont.Name, rgbiFont.Gallery);
			CurrentColorItem = GetColorItemByColor(CurrentRichTextBox.SelectionColor, rgbiFontColor.Gallery);
		}
		private void gddFont_Popup(object sender, System.EventArgs e)
		{
			MakeFontVisible(CurrentFontItem);
			if (CurrentRichTextBox == null || CurrentRichTextBox.SelectionFont == null) return;
			beiFontSize.EditValue = CurrentRichTextBox.SelectionFont.Size;
		}

		private void gddFontColor_Popup(object sender, System.EventArgs e)
		{
			MakeColorVisible(CurrentColorItem);
		}
		#endregion

		#region MostRecentFiles

		private void LoadLocationAndSizeSettings()
		{
			try
			{
				// Set location.																				      // Divide the screen in half, and find the center of the form to center it
				int x = this.AppContext.UserSettings.GetValue<int>(UserSettings.SettingWindowLocationX, defaultValue: (Screen.PrimaryScreen!.Bounds.Width - this.Width) / 2); // this.AppContext.UserSettings.WindowLocationX
				int y = this.AppContext.UserSettings.GetValue<int>(UserSettings.SettingWindowLocationY, defaultValue: (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2); // this.AppContext.UserSettings.WindowLocationY

				this.Location = new Point(x, y);

				// Set size.
				int width = this.AppContext.UserSettings.GetValue<int>(UserSettings.SettingWindowWidth, defaultValue: this.Size.Width); // this.AppContext.UserSettings.WindowWidth
				int height = this.AppContext.UserSettings.GetValue<int>(UserSettings.SettingWindowHeight, defaultValue: this.Size.Height); // this.AppContext.UserSettings.WindowHeight

				this.Size = new Size(width, height);

				// Set state.
				this.WindowState = Conversion.TryChangeType<FormWindowState>(this.AppContext.UserSettings.WindowState, ((int)FormWindowState.Normal)); //  GetValue<int>(UserSettings.SettingWindowState
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, this.AppContext.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		MRUArrayList arMRUList = null;

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveMostRecentFiles(recentTabItem1.TabPanel.Items, Application.StartupPath + "\\" + FileNames);
			SaveMostRecentFiles(recentTabItem2.TabPanel.Items, Application.StartupPath + "\\" + FolderNames);

			try
			{
				if (this.WindowState == FormWindowState.Normal)
				{
					this.AppContext.UserSettings.WindowWidth = this.Size.Width;
					this.AppContext.UserSettings.WindowHeight = this.Size.Height;
					this.AppContext.UserSettings.WindowLocationX = this.Location.X;
					this.AppContext.UserSettings.WindowLocationY = this.Location.Y;
				}

				this.AppContext.UserSettings.WindowState = (int)this.WindowState;
				this.AppContext.UserSettings.Save();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, this.AppContext.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		void InitMostRecentFiles()
		{
			string fileName = Application.StartupPath + "\\" + MRUArrayList.MRUFileName;
			string folderName = Application.StartupPath + "\\" + MRUArrayList.MRUFolderName;
			InitFiles(fileName, "Document1.rtf", true);
			InitFiles(folderName, Application.StartupPath, false);
		}

		public void InitFiles(string fileName, string defaultItem, bool isFile)
		{
			if (!System.IO.File.Exists(fileName))
			{
				StartInitFiles(isFile);
				EndInitFiles(isFile);
				//InsertElement(defaultItem, isFile);
				return;
			}
			System.IO.StreamReader sr = System.IO.File.OpenText(fileName);
			List<string> list = new List<string>();
			for (string s = sr.ReadLine(); s != null; s = sr.ReadLine())
				list.Add(s);
			for (int i = 0; i < list.Count; i++)
			{
				InsertElement(list[i], isFile);
			}
			sr.Close();
			if (!isFile) CreateButtonBrowse();
		}

		int FindFirstUnCheckedIntemIndex(RecentPanelBase recentPanelBase)
		{
			for (int i = 0; i < recentPanelBase.Items.Count; i++)
			{
				RecentPinItem pinItem = recentPanelBase.Items[i] as RecentPinItem;
				if (pinItem == null) continue;
				if (!pinItem.PinButtonChecked) return i;
			}
			return 0;
		}
		void InsertElement(object obj, bool isFile)
		{
			string[] names = obj.ToString().Split(',');
			string _name = names[0];
			bool checkedLabel = false;
			if (names.Length > 1) checkedLabel = names[1].ToLower().Equals("true");
			if (isFile)
			{
				RecentPinItem pinItem = new RecentPinItem() { Caption = GetFileName(_name), Description = _name, PinButtonChecked = checkedLabel };
				recentTabItem1.TabPanel.Items.Add(pinItem);
			}
			else
			{
				RecentPinItem pinItem = new RecentPinItem() { Caption = GetFileName(_name), Description = _name, PinButtonChecked = checkedLabel };
				RecentPinItem pinItem_ = new RecentPinItem() { Caption = GetFileName(_name), Description = _name, PinButtonChecked = checkedLabel };
				recentTabItem2.TabPanel.Items.Add(pinItem);
				recentTabItem3.TabPanel.Items.Add(pinItem_);
			}
		}
		void CreateButtonBrowse()
		{
			RecentHyperlinkItem hyperlinkBrowse = new RecentHyperlinkItem() { Caption = "Browse..." };
			recentTabItem2.TabPanel.Items.Add(hyperlinkBrowse);
			hyperlinkBrowse.ItemClick += hyperlinkBrowse_ItemClick;
			RecentHyperlinkItem hyperlinkBrowse1 = new RecentHyperlinkItem() { Caption = "Browse..." };
			recentTabItem3.TabPanel.Items.Add(hyperlinkBrowse1);
			hyperlinkBrowse1.ItemClick += hyperlinkBrowse_ItemClick;
		}

		void hyperlinkBrowse_ItemClick(object sender, RecentItemEventArgs e)
		{
			SaveAs();
		}
		private void EndInitFiles(bool isFile)
		{
			if (isFile) InitDefaultFiles();
			else InitDefaultFolders();
			if (!isFile) CreateButtonBrowse();
		}
		private void StartInitFiles(bool isFile)
		{
			if (!isFile) InitStartDefaultFolders();
		}
		private void InitStartDefaultFolders()
		{
			RecentPinItem desktop = new RecentPinItem() { Caption = "Desktop", Description = SafeEnvironment.Desktop, PinButtonChecked = true, ShowDescription = false };
			RecentPinItem desktop_ = new RecentPinItem() { Caption = "Desktop", Description = SafeEnvironment.Desktop, PinButtonChecked = true, ShowDescription = false };
			recentTabItem2.TabPanel.Items.Add(desktop);
			recentTabItem3.TabPanel.Items.Add(desktop_);
			RecentSeparatorItem separator = new RecentSeparatorItem();
			RecentSeparatorItem separator_ = new RecentSeparatorItem();
			recentTabItem2.TabPanel.Items.Add(separator);
			recentTabItem3.TabPanel.Items.Add(separator_);
		}
		private void InitDefaultFolders()
		{
			RecentPinItem item1 = new RecentPinItem() { Caption = "MyDocuments", Description = SafeEnvironment.MyDocuments };
			RecentPinItem item1_ = new RecentPinItem() { Caption = "MyDocuments", Description = SafeEnvironment.MyDocuments };
			recentTabItem2.TabPanel.Items.Add(item1);
			recentTabItem3.TabPanel.Items.Add(item1_);

			RecentPinItem item7 = new RecentPinItem() { Caption = "Saved HTML Articles", Description = "D:\\Personal\\Saved HTML Articles" };
			RecentPinItem item7_ = new RecentPinItem() { Caption = "Saved HTML Articles", Description = "D:\\Personal\\Saved HTML Articles" };
			recentTabItem2.TabPanel.Items.Add(item7);
			recentTabItem3.TabPanel.Items.Add(item7_);

			RecentPinItem item3 = new RecentPinItem() { Caption = "Tutorials", Description = "C:\\Program Files x86\\DevExpress 15.2\\Tutorials" };
			RecentPinItem item3_ = new RecentPinItem() { Caption = "Tutorials", Description = "C:\\Program Files x86\\DevExpress 15.2\\Tutorials" };
			recentTabItem2.TabPanel.Items.Add(item3);
			recentTabItem3.TabPanel.Items.Add(item3_);

			RecentPinItem item2 = new RecentPinItem() { Caption = "Products", Description = "C:\\DevExpress\\Products" };
			RecentPinItem item2_ = new RecentPinItem() { Caption = "Products", Description = "C:\\DevExpress\\Products" };
			recentTabItem2.TabPanel.Items.Add(item2);
			recentTabItem3.TabPanel.Items.Add(item2_);

			RecentPinItem item8 = new RecentPinItem() { Caption = "Win Forms", Description = "C:\\DevExpress\\Products\\Win Forms" };
			RecentPinItem item8_ = new RecentPinItem() { Caption = "Win Forms", Description = "C:\\DevExpress\\Products\\Win Forms" };
			recentTabItem2.TabPanel.Items.Add(item8);
			recentTabItem3.TabPanel.Items.Add(item8_);

			RecentPinItem item9 = new RecentPinItem() { Caption = "Controls", Description = "C:\\DevExpress\\Products\\Win Forms\\Controls" };
			RecentPinItem item9_ = new RecentPinItem() { Caption = "Controls", Description = "C:\\DevExpress\\Products\\Win Forms\\Controls" };
			recentTabItem2.TabPanel.Items.Add(item9);
			recentTabItem3.TabPanel.Items.Add(item9_);
		}
		private void InitDefaultFiles()
		{
			RecentPinItem item1 = new RecentPinItem() { Caption = "Windows 10 GuideLines.doc", Description = "D:\\Personal\\Saved HTML Articles" };
			recentTabItem1.TabPanel.Items.Add(item1);
			RecentPinItem item2 = new RecentPinItem() { Caption = "Web Site Usability.doc", Description = "C:\\Users\\Default\\Documents" };
			recentTabItem1.TabPanel.Items.Add(item2);
			RecentPinItem item3 = new RecentPinItem() { Caption = "Getting Started.pdf", Description = "C:\\Program Files x86\\DevExpress 15.2\\Tutorials" };
			recentTabItem1.TabPanel.Items.Add(item3);
			RecentPinItem item4 = new RecentPinItem() { Caption = "Office 2013 features.docx", Description = "C:\\DevExpress\\Products\\Win Forms" };
			recentTabItem1.TabPanel.Items.Add(item4);
			RecentPinItem item5 = new RecentPinItem() { Caption = "WinForms Webinar.pdf", Description = "C:\\DevExpress\\Webinars" };
			recentTabItem1.TabPanel.Items.Add(item5);
		}
		string GetFileName(object obj)
		{
			FileInfo fi = new FileInfo(obj.ToString());
			return fi.Name;
		}
		void recentControlOpen_ItemClick(object sender, RecentItemEventArgs e)
		{
			RecentPinItem recentItem = e.Item as RecentPinItem;
			if (recentItem != null) ribbonControl.HideApplicationButtonContentControl();
			if (recentTabItem1.TabPanel.Items.Contains(e.Item) && recentItem != null)
				OpenFileCore(recentItem.Description);
			else if (recentTabItem2.TabPanel.Items.Contains(e.Item))
			{
				if (recentItem != null)
					OpenFileFolder(recentItem.Description);
			}
		}
		private void OpenFileFolder(string p)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			if (p != string.Empty) dlg.InitialDirectory = p;
			dlg.Filter = "Rich Text Files (*.rtf)|*.rtf";
			dlg.Title = "Open";
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				OpenFile(dlg.FileName);
			}
		}
		private void recentControlSave_ItemClick(object sender, RecentItemEventArgs e)
		{
			ribbonControl.HideApplicationButtonContentControl();
			RecentPinItem recentItem = e.Item as RecentPinItem;
			if (recentTabItem3.TabPanel.Items.Contains(e.Item))
			{
				if (recentItem != null)
					SaveAs(recentItem.Description);
			}
		}
		private void recentControlExport_ItemClick(object sender, DevExpress.XtraBars.Ribbon.RecentItemEventArgs e)
		{
			if (CurrentForm == null)
			{
				Ribbon.HideApplicationButtonContentControl();
				return;
			}
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Title = "Export";
			if (!(e.Item is RecentPinItem)) return;
			string caption = (e.Item as RecentPinItem).Caption;
			if (caption.Contains("PDF"))
				saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
			else if (caption.Contains("HTML"))
				saveFileDialog.Filter = "HTML files (*.html)|*.html";
			else if (caption.Contains("MHT"))
				saveFileDialog.Filter = "MHT files (*.mht)|*.mht";
			else if (caption.Contains("RTF"))
				saveFileDialog.Filter = "RTF files (*.rtf)|*.rtf";
			else if (caption.Contains("XLS"))
				saveFileDialog.Filter = "XLS files (*.xls)|*.xls";
			else if (caption.Contains("XLSX"))
				saveFileDialog.Filter = "XLSX files (*.xls)|*.xls";
			else if (caption.Contains("CSV"))
				saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
			else if (caption.Contains("Text File"))
				saveFileDialog.Filter = "Text files (*.txt)|*.txt";
			else if (caption.Contains("Image"))
				saveFileDialog.Filter = "BMP files (*.bmp)|*.bmp|Gif files (*.gif)|*.gif|Jpeg files (*.jpeg)|*.jpeg|PNG files (*.png)|*.png|Tiff files (*.tiff)|*.tiff|EMF files (*.emf)|.emf|WMF files (*.wmf)|*.wmf";
			saveFileDialog.Filter += "|All files (*.*)|*.*";
			saveFileDialog.FilterIndex = 0;
			if (saveFileDialog.ShowDialog() != DialogResult.OK)
				return;

			XtraReport report = new XtraReport();
			report.Bands.Add(new DetailBand()
			{
				Controls = { new XRRichText() {
					BoundsF = new RectangleF(0, 0, 650, 0),
					Rtf = CurrentRichTextBox.Rtf,
				} }
			});
			report.CreateDocument();

			if (caption.Contains("PDF"))
				report.ExportToPdf(saveFileDialog.FileName);
			else if (caption.Contains("HTML"))
				report.ExportToHtml(saveFileDialog.FileName);
			else if (caption.Contains("MHT"))
				report.ExportToMht(saveFileDialog.FileName);
			else if (caption.Contains("RTF"))
				report.ExportToRtf(saveFileDialog.FileName);
			else if (caption.Contains("XLS"))
				report.ExportToXls(saveFileDialog.FileName);
			else if (caption.Contains("XLSX"))
				report.ExportToXlsx(saveFileDialog.FileName);
			else if (caption.Contains("CSV"))
				report.ExportToCsv(saveFileDialog.FileName);
			else if (caption.Contains("Text File"))
				report.ExportToText(saveFileDialog.FileName);
			else if (caption.Contains("Image"))
			{
				DXImageFormat fmt = DXImageFormat.Bmp;
				switch (saveFileDialog.FilterIndex)
				{
					case 0: fmt = DXImageFormat.Bmp; break;
					case 1: fmt = DXImageFormat.Gif; break;
					case 2: fmt = DXImageFormat.Jpeg; break;
					case 3: fmt = DXImageFormat.Png; break;
					case 4: fmt = DXImageFormat.Tiff; break;
					case 5: fmt = DXImageFormat.Emf; break;
					case 6: fmt = DXImageFormat.Wmf; break;
				}
				report.ExportToImage(saveFileDialog.FileName, fmt);
			}
		}
		void OpenFileCore(string path)
		{
			ribbonControl.DeactivateKeyboardNavigation();
			pmAppMain.HidePopup();
			this.Refresh();
			if (System.IO.File.Exists(path))
			{
				OpenFile(path);
				backstageViewControl1.Ribbon.HideApplicationButtonContentControl();
			}
			else DevExpress.XtraEditors.XtraMessageBox.Show(string.Format("ItemClick {0}", path.ToString()));
		}
		void SaveMostRecentFiles(RecentItemCollection files, string fileName)
		{
			try
			{
				System.IO.StreamWriter sw = System.IO.File.CreateText(fileName);
				for (int i = 0; i < files.Count; i++)
				{
					RecentPinItem pinItem = files[i] as RecentPinItem;
					if (pinItem == null) continue;
					sw.WriteLine(string.Format("{0},{1}", pinItem.Description != string.Empty ? pinItem.Description : pinItem.Caption, pinItem.PinButtonChecked.ToString()));
				}
				sw.Close();
			}
			catch { }
		}
		void OnMRUFileLabelClicked(object sender, EventArgs e)
		{
			ribbonControl.DeactivateKeyboardNavigation();
			pmAppMain.HidePopup();
			this.Refresh();
			OpenFile(sender.ToString());
		}

		#endregion

		#region RibbonSimplePad 3

		private void ribbonControl1_ApplicationButtonDoubleClick(object sender, EventArgs e)
		{
			if (ribbonControl.RibbonStyle == RibbonControlStyle.Office2007)
				this.Close();
		}

		private void barEditItem1_ItemPress(object sender, ItemClickEventArgs e)
		{
			DevExpress.Data.Utils.SafeProcess.Start("http://www.netakod-community.com");
		}
		bool IsOffice365StyleOrDefault => Ribbon.RibbonStyle == RibbonControlStyle.Office365 || Ribbon.RibbonStyle == RibbonControlStyle.Default;
		void UpdateEmpaSpacePanelVisibility()
		{
			emptySpacePanel.Visible = IsOffice365StyleOrDefault;
		}
		void UpdateSearchBoxPosition()
		{
			Ribbon.SearchItemPosition = IsOffice365StyleOrDefault ? SearchItemPosition.Caption : SearchItemPosition.PageHeader;
		}
		void UpdateLookAndFeel()
		{
			string skinName;
			RibbonControlStyle style = ribbonControl.RibbonStyle;
			switch (style)
			{
				case RibbonControlStyle.Office365:
				case RibbonControlStyle.Default:
					skinName = "WXI";
					break;
				case RibbonControlStyle.Office2019:
					skinName = "Office 2019 Colorful";
					break;
				case RibbonControlStyle.Office2007:
					skinName = "Office 2007 Blue";
					break;
				case RibbonControlStyle.Office2013:
				case RibbonControlStyle.TabletOffice:
				case RibbonControlStyle.OfficeUniversal:
					skinName = "Office 2013";
					break;
				case RibbonControlStyle.Office2010:
				case RibbonControlStyle.MacOffice:
				default:
					skinName = "Office 2010 Blue";
					break;
			}
			UserLookAndFeel.Default.SetSkinStyle(skinName);
		}
		private void sbExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void beiFontSize_EditValueChanged(object sender, EventArgs e)
		{
			if (CurrentRichTextBox == null) return;
			Font _font = CurrentRichTextBox.SelectionFont;
			if (_font == null)
			{
				_font = AppearanceObject.DefaultFont;
			}
			CurrentRichTextBox.SelectionFont = new Font(_font.FontFamily, Convert.ToSingle(beiFontSize.EditValue), _font.Style);
		}

		void onTabPrint_SelectedChanged(object sender, BackstageViewItemEventArgs e)
		{
			InitPrintingSystem();
		}

		void ribbonControl1_BeforeApplicationButtonContentControlShow(object sender, EventArgs e)
		{
			if (backstageViewControl1.SelectedTab == printTabItem)
				InitPrintingSystem();
		}

		private void bvItemSettings_ItemClick(object sender, BackstageViewItemEventArgs e)
		{
			//Save();
			this.backstageViewControl1.Close();
			this.ShowFormSettings();
		}

		private void barButtonItemSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.ShowFormSettings();
		}

		private void bvItemSaveAs_ItemClick(object sender, BackstageViewItemEventArgs e)
		{
			SaveAs();
		}

		private void bvItemOpen_ItemClick(object sender, BackstageViewItemEventArgs e)
		{
			OpenFile();
		}

		private void bvItemClose_ItemClick(object sender, BackstageViewItemEventArgs e)
		{
			if (xtraTabbedMdiManager1.SelectedPage != null)
				xtraTabbedMdiManager1.SelectedPage.MdiChild.Close();
		}

		private void bvItemExit_ItemClick(object sender, BackstageViewItemEventArgs e)
		{
			Close();
		}
		void ribbonControl1_ResetLayout(object sender, ResetLayoutEventArgs e)
		{
			ShowHideFormatCategory();
		}
		void OnNewDocThumbButtonClick(object sender, ThumbButtonClickEventArgs e)
		{
			CreateNewDocument();
		}
		void OnPrevThumbButtonClick(object sender, ThumbButtonClickEventArgs e)
		{
			Form mdiChild = GetPrevMdiChild();
			if (mdiChild != null)
				ActivateMdiChild(mdiChild);
		}
		void OnNextDocThumbButtonClick(object sender, ThumbButtonClickEventArgs e)
		{
			Form mdiChild = GetNextMdiChild();
			if (mdiChild != null)
				ActivateMdiChild(mdiChild);
		}
		void OnExitThumbButtonClick(object sender, ThumbButtonClickEventArgs e)
		{
			Close();
		}
		Form GetNextMdiChild()
		{
			if (ActiveMdiChild == null || MdiChildren.Length < 2)
				return null;
			int pos = Array.IndexOf(MdiChildren, ActiveMdiChild);
			return pos == MdiChildren.Length - 1 ? MdiChildren[0] : MdiChildren[pos + 1];
		}
		Form GetPrevMdiChild()
		{
			if (ActiveMdiChild == null || MdiChildren.Length < 2)
				return null;
			int pos = Array.IndexOf(MdiChildren, ActiveMdiChild);
			return pos == 0 ? MdiChildren[MdiChildren.Length - 1] : MdiChildren[pos - 1];
		}
		void OnTabbedMdiManagerPageCollectionChanged(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
		{
			UpdateThumbnailButtons();
		}
		void UpdateThumbnailButtons()
		{
			thumbButtonNext.Enabled = thumbButtonPrev.Enabled = MdiChildren.Length > 1;
		}

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

		#endregion RibbonSimplePad 3

		#region Print processing
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

			XtraReport report = new XtraReport();
			report.Bands.Add(new DetailBand()
			{
				Controls = { new XRRichText() {
					BoundsF = new RectangleF(0, 0, 650, 0),
					Rtf = CurrentRichTextBox?.Rtf ?? String.Empty,
				} }
			});
			report.AfterPrint += Report_AfterPrint;

			PrintingSystemBase ps = report.PrintingSystem;
			ps.StartPrint += new PrintDocumentEventHandler(OnStartPrint);

			this.printControl2.DocumentSource = report;
			this.printControl2.InitiateDocumentCreation();

			this.printButton.Enabled = false;
			this.pageButtonEdit.Enabled = false;
			this.pageButtonEdit.EditValue = null;
			this.pageButtonEdit.Properties.DisplayFormat.FormatString = "";

			UpdatePrintPageSettings();
		}

		private void Report_AfterPrint(object sender, EventArgs e)
		{
			var pages = ((XtraReport)sender).Pages;
			this.printButton.Enabled = pages.Count > 0;
			this.pageButtonEdit.Enabled = pages.Count > 0;
			this.pageButtonEdit.Properties.DisplayFormat.FormatString = "Page {0} of " + pages.Count;
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
		void InitPrint()
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
			portraitItem.ImageOptions.SvgImage = Properties.Resources.PageOrientationPortrait1;
			portraitItem.Caption = "Portrait Orientation";
			GalleryItem landscapeItem = new GalleryItem();
			landscapeItem.ImageOptions.SvgImage = Properties.Resources.PageOrientationLandscape1;
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
			normal.ImageOptions.SvgImage = Properties.Resources.PageMarginsNormal1;
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
			letter.Tag = DXPaperKind.Letter;
			GalleryItem tabloid = new GalleryItem();
			tabloid.ImageOptions.SvgImage = Properties.Resources.PaperKind_Tabloid1;
			tabloid.Caption = "Tabloid";
			tabloid.Description = "279 mm x 431 mm";
			tabloid.Tag = DXPaperKind.Tabloid;
			GalleryItem legal = new GalleryItem();
			legal.ImageOptions.SvgImage = Properties.Resources.PaperKind_Legal1;
			legal.Caption = "Legal";
			legal.Description = "215 mm x 355 mm";
			legal.Tag = DXPaperKind.Legal;
			GalleryItem executive = new GalleryItem();
			executive.ImageOptions.SvgImage = Properties.Resources.PaperKind_Executive1;
			executive.Caption = "Executive";
			executive.Description = "184 mm x 266 mm";
			executive.Tag = DXPaperKind.Executive;
			GalleryItem a3 = new GalleryItem();
			a3.ImageOptions.SvgImage = Properties.Resources.PaperKind_A31;
			a3.Caption = "A3";
			a3.Description = "296 mm x 420 mm";
			a3.Tag = DXPaperKind.A3;
			GalleryItem a4 = new GalleryItem();
			a4.ImageOptions.SvgImage = Properties.Resources.PaperKind_A41;
			a4.Caption = "A4";
			a4.Description = "210 mm x 296 mm";
			a4.Tag = DXPaperKind.A4;
			GalleryItem a5 = new GalleryItem();
			a5.ImageOptions.SvgImage = Properties.Resources.PaperKind_A51;
			a5.Caption = "A5";
			a5.Description = "148 mm x 210 mm";
			a5.Tag = DXPaperKind.A5;
			GalleryItem a6 = new GalleryItem();
			a6.ImageOptions.SvgImage = Properties.Resources.PaperKind_A61;
			a6.Caption = "A6";
			a6.Description = "105 mm x 148 mm";
			a6.Tag = DXPaperKind.A6;
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
			collated.ImageOptions.SvgImage = Properties.Resources.Page_;
			collated.Caption = "Collated";
			collated.Description = "1,2,3   1,2,3  1,2,3";
			collated.Tag = true;
			GalleryItem uncollated = new GalleryItem();
			uncollated.ImageOptions.SvgImage = Properties.Resources.Page_;
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
			oneSided.ImageOptions.SvgImage = Properties.Resources.Page_;
			oneSided.Caption = "Print One Sided";
			oneSided.Description = "Only print on one side of the page";
			oneSided.Tag = false;
			GalleryItem twoSided = new GalleryItem();
			twoSided.ImageOptions.SvgImage = Properties.Resources.Page_;
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

			PrinterItemContainer printerItemContainer = new PrinterItemContainer();
			var defaultPrinterName = printerItemContainer.DefaultPrinterName;

			GalleryItem defaultPrinter = null;
			try
			{
				foreach (var printerItem in printerItemContainer.Items)
				{
					GalleryItem item = new GalleryItem();
					item.ImageOptions.SvgImage = this.printButton.ImageOptions.SvgImage;
					item.Caption = printerItem.FullName;
					res.Gallery.Groups[0].Items.Add(item);
					if (printerItem.FullName == defaultPrinterName)
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
				if (e.NewValue != null)
				{
					int pageIndex = Int32.Parse(e.NewValue.ToString());
					if (pageIndex < 1)
						pageIndex = 1;
					else if (pageIndex > this.printControl2.PrintingSystem.Pages.Count)
						pageIndex = this.printControl2.PrintingSystem.Pages.Count;
					e.NewValue = pageIndex;
				}
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

		//private void rgbiColorScheme_Gallery_ItemCheckedChanged(object sender, GalleryItemEventArgs e)
		//{
		//	ribbonControl.ColorScheme = ((RibbonControlColorScheme)e.Item.Value);
		//}

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
		#endregion

		#region |   Init Methods   |

		private void InitializeAppClient()
		{
			this.barEditItemMonitorServer.EditValue = IpHelper.GetIpAddressOrHostname(this.AppClient.RemoteEndPoint);
			this.barEditItemServerMonitorPort.EditValue = IpHelper.GetPort(this.AppClient.RemoteEndPoint);

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
		}


		private void InitializeServiceControls(bool connected)
		{
			this.barEditItemMonitorServer.EditValue = this.AppContext.UserSettings.Server;
			this.barEditItemServerMonitorPort.EditValue = this.AppContext.UserSettings.Port.ToString();

			if (!connected)
				this.barStaticItemUser.Caption = this.AppContext.UserSettings.LastUsername + " - not athorized";

			this.barButtonItemConnect.Enabled = !connected;
			this.barEditItemMonitorServer.Enabled = !connected;
			this.barEditItemServerMonitorPort.Enabled = !connected;
			this.barButtonItemDisconnect.Enabled = connected;

			this.barStaticItemUser.Enabled = connected;
			this.barButtonItemInfo.Visibility = (connected) ? BarItemVisibility.Never : BarItemVisibility.Always;

			this.barStaticItemMonitorServer.Enabled = connected;
			this.barStaticItemMonitorServer.Caption = (connected) ? "Connected to " + this.barEditItemMonitorServer.EditValue.ToString() + ":" + this.barEditItemServerMonitorPort.EditValue : "Disconnected";
		}

		private void InitializeServerControls(bool connected, bool isServerStarted)
		{
			this.barButtonItemServerStart.Enabled = connected && !isServerStarted;
			this.barButtonItemServerStop.Enabled = connected && isServerStarted;
			this.barButtonItemServerRestart.Enabled = connected && isServerStarted;
		}

		#endregion |   Init Methods   |

		#region |    Button Action Handling   |

		private void barButtonItemConnect_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.AppContext.UserSettings.Server = (string)this.barEditItemMonitorServer.EditValue;
			this.AppContext.UserSettings.Port = Conversion.TryChangeType<int>(this.barEditItemServerMonitorPort.EditValue);
			this.AppContext.UserSettings.Save();

			this.barButtonItemConnect.Enabled = false;
			this.barEditItemMonitorServer.Enabled = false;
			this.barEditItemServerMonitorPort.Enabled = false;
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

			this.ribbonControl.ColorScheme = colorScheme;
			//this.AppContext.UserSettings.RibbonColorScheme = (int)colorScheme;
			//this.AppContext.UserSettings.Save();
		}

		//private void BarEditItemColorScheme_EditValueChanged(object? sender, EventArgs e)
		//{
		//	this.Ribbon.ColorScheme = ((RibbonControlColorScheme)this.rgbiColorScheme. barEditItemColorScheme.EditValue);
		//	this.OnStyleChanged(EventArgs.Empty);
		//}

		private void BarToggleSwitchItemDarkMode_CheckedChanged(object sender, ItemClickEventArgs e)
		{
			this.AppContext.UserSettings.DarkMode = this.barToggleSwitchItemDarkMode.Checked;
			this.AppContext.UserSettings.Save();
			this.SetEnableForSkinControls();
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
			RibbonControlStyle ribbonStyle = (RibbonControlStyle)this.biStyle.EditValue;

			this.AppContext.UserSettings.RibbonStyle = (int)ribbonStyle;
			this.AppContext.UserSettings.Save();

			this.SetRibbonStyle(ribbonStyle);
		}

		private void biStyle_EditValueChanged(object sender, EventArgs e)
		{
			RibbonControlStyle ribbonStyle = (RibbonControlStyle)biStyle.EditValue;

			this.SetRibbonStyle(ribbonStyle);
		}

		#endregion |    Button Action Handling   |

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

		#region |   Private Methods   |

		protected void SetRibbonStyle(RibbonControlStyle ribbonStyle)
		{
			this.Ribbon.RibbonStyle = ribbonStyle;
			this.Ribbon.ApplicationButtonDropDownControl = ribbonStyle == RibbonControlStyle.Office2007 ? (object)pmAppMain : this.backstageViewControl1;

			if (ribbonStyle == RibbonControlStyle.TabletOffice || ribbonStyle == RibbonControlStyle.OfficeUniversal)
				this.barToggleSwitchItem1.Visibility = BarItemVisibility.Always;
			else
				this.barToggleSwitchItem1.Visibility = BarItemVisibility.Never;

			//this.UpdateLookAndFeel();
			this.UpdateSearchBoxPosition();
			this.UpdateEmpaSpacePanelVisibility();

			////this.UpdateSchemeCombo(ribbonStyle);
			//this.UpdateLookAndFeel(ribbonStyle);
			//this.SetSkinStyle(ribbonStyle);
			//this.OnStyleChanged(EventArgs.Empty);
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

		private void ShowFormSettings()
		{
			var ribbonStyle = MonitorAppContext.Instance.UserSettings.RibbonStyle;
			var ribbonColorScheme = MonitorAppContext.Instance.UserSettings.RibbonColorScheme;
			var ribbonSkinName = MonitorAppContext.Instance.UserSettings.RibbonSkinName;
			var ribbonSkinPaletteName = MonitorAppContext.Instance.UserSettings.RibbonSkinPaletteName;
			var darkMode = MonitorAppContext.Instance.UserSettings.DarkMode;
			var compactUI = MonitorAppContext.Instance.UserSettings.CompactUI;


			FormSettings formSettings = new FormSettings(this);
			DialogResult formSettingsDialogResult = formSettings.ShowDialog();

			if (formSettingsDialogResult != System.Windows.Forms.DialogResult.OK)
			{
				// Revert styling to its previuous state

				Cursor? currentCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;

				MonitorAppContext.Instance.UserSettings.RibbonStyle = ribbonStyle;
				MonitorAppContext.Instance.UserSettings.RibbonColorScheme = ribbonColorScheme;
				MonitorAppContext.Instance.UserSettings.RibbonSkinName = ribbonSkinName;
				MonitorAppContext.Instance.UserSettings.RibbonSkinPaletteName = ribbonSkinPaletteName;
				MonitorAppContext.Instance.UserSettings.DarkMode = darkMode;
				MonitorAppContext.Instance.UserSettings.CompactUI = compactUI;

				this.barToggleSwitchItemDarkMode.Checked = darkMode;
				this.barToggleSwitchItemCompactView.Checked = compactUI;

				//this.Ribbon.RibbonStyle = (RibbonControlStyle)this.AppContext.UserSettings.RibbonStyle;
				//this.Ribbon.ColorScheme = (RibbonControlColorScheme)this.AppContext.UserSettings.RibbonColorScheme;

				this.SetSkinStyle();
				this.OnStyleChanged(EventArgs.Empty);

				this.rgbiSkins.Enabled = !darkMode;

				Cursor.Current = currentCursor;
			}
		}

		private async ValueTask<bool> TryConnectToServerAndAuthorize()
		{
			bool connected = false;
			bool authorized = false;
			bool escape = false;
			int retry = 0;
			int numOfRetry = 10;
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
						var response = await this.AppClient.AuthenticateSession(username, Program.DefaultPassword);

						authorized = response.ResponseSucceeded && response.IsAuthenticated;
					}

					escape = !(connected && authorized);
#else
					formLogin ??= new FormLogin(MonitorAppContext.Instance.AppName, server, username);
					formLogin.Focus();

					DialogResult loginDialogResult = formLogin!.ShowDialog();

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
#endif
					if (escape)
						break;

					if (!connected)
					{
						string serverInfo = formLogin?.Server ?? server;

						XtraMessageBox.Show("Could not connect to remote server " + serverInfo, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (!authorized)
					{
						XtraMessageBox.Show("Wrong username or password.", "Authorization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}

					Thread.Sleep(100);
				}
			}
			catch (Exception ex)
			{
				XtraMessageBox.Show(ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

				formLogin?.Close();
			}

			return connected && authorized;
		}

		private void barSubItemChangeSkin_ItemPress(object sender, ItemClickEventArgs e)
		{

		}

		private void barSubItemChangeSkin_ItemClick(object sender, ItemClickEventArgs e)
		{

		}

		private void BarSubItemChangeSkin_Popup(object? sender, EventArgs e)
		{
			foreach (BarItemLink link in this.barSubItemChangeSkin2.ItemLinks)
				((BarCheckItem)link.Item).Checked = (link.Item.Caption == UserLookAndFeel.Default.ActiveLookAndFeel.SkinName);
		}

		private void FormMainNew2_Shown(object sender, EventArgs e)
		{
			if (this.skinPaletteRibbonGalleryBarItem2.Enabled)
				this.skinPaletteRibbonGalleryBarItem2.Visibility = BarItemVisibility.Always;
			else
				this.skinPaletteRibbonGalleryBarItem2.Visibility = BarItemVisibility.Never;
		}

		private void iWeb_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			string fileName = "https://www.netakod-community.com";

			DevExpress.Data.Utils.SafeProcess.Open(fileName);
		}

		private void biOnlineHelp_ItemClick(object sender, ItemClickEventArgs e)
		{
			string fileName = "https://www.netakod-community.com/downloads/";

			DevExpress.Data.Utils.SafeProcess.Open(fileName);
		}

		private void biGettingStarted_ItemClick(object sender, ItemClickEventArgs e)
		{
			string fileName = "https://www.netakod-community.com/products";

			DevExpress.Data.Utils.SafeProcess.Open(fileName);
		}

		private void biContact_ItemClick(object sender, ItemClickEventArgs e)
		{
			string fileName = "https://www.netakod-community.com/about";

			DevExpress.Data.Utils.SafeProcess.Open(fileName);
		}

		private void iAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			string fileName = "https://www.netakod-community.com/about";

			DevExpress.Data.Utils.SafeProcess.Open(fileName);
		}

		private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
		{
			DevExpress.Data.Utils.SafeProcess.Open("https://github.com/lpoleved/SimpleObjectsLib");
			//System.Diagnostics.Process.Start("cmd", "/C start " + "https://github.com/lpoleved/SimpleObjectsLib");
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

	public class RibbonSimplePadSplashScreen : DemoSplashScreen
	{
		public RibbonSimplePadSplashScreen()
		{
			DemoText = "RibbonSimplePad";
			ProductText = "The XtraBars Suite";
		}
	}
}
