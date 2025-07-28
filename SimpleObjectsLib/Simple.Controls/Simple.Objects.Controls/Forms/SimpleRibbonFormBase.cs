using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Simple.Objects;
using Simple.Controls;
using Simple.AppContext;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.Skins;
using DevExpress.Skins.XtraForm;
using DevExpress.LookAndFeel;

namespace Simple.Objects.Controls
{
	public class SimpleRibbonFormBase : RibbonForm, ISimpleForm
	{
		#region |   Private Members   |

		//initializing form
		private bool initializing = false;
		private Dictionary<int, SimpleRibbonModulePanel> ribbonModulePanelsByGraphKey = new Dictionary<int, SimpleRibbonModulePanel>();
		private BarSubItem? barSubItemChangeSkin;

		#endregion |   Private Members   |

		#region |   Public Members   |

		//public const string SettingSkinName = "SkinName";

		#endregion |   Public Members   |

		#region |   Constructors and Initialization   |

		public SimpleRibbonFormBase()
		{
			if (this.AppContext != null)
				this.Text = this.AppContext.AppName;
		}

		#endregion |   Constructors and Initialization   | zation   |

		#region |   Public Properties   |

		public virtual IClientAppContext AppContext { get; protected set; }

		public Panel Panel { get; protected set; }
        public List<RibbonModuleModel> RibbonModuleModels { get; protected set; }
        public List<RibbonModuleGroupModel> RibbonModuleGroupModels { get; protected set; }
        
        public BarSubItem? BarSubItemChangeSkin 
        {
            get { return this.barSubItemChangeSkin; }

            protected set
            {
                if (this.barSubItemChangeSkin != null)
                    this.barSubItemChangeSkin.Popup -= new EventHandler(barSubItemChangeSkin_Popup);

                this.barSubItemChangeSkin = value;

                if (this.barSubItemChangeSkin != null)
                    this.barSubItemChangeSkin.Popup += new EventHandler(barSubItemChangeSkin_Popup);
            }
        }

        public new RibbonControl Ribbon
        {
            get { return base.Ribbon; }
            
            set
            {
                if (this.Ribbon != null)
                {
                    this.Ribbon.SelectedPageChanging -= new RibbonPageChangingEventHandler(Ribbon_SelectedPageChanging);
                    this.Ribbon.SelectedPageChanged -= new EventHandler(SelectedPageChanged);

                }

                base.Ribbon = value;

                if (this.Ribbon != null)
                {
                    this.Ribbon.SelectedPageChanging += new RibbonPageChangingEventHandler(Ribbon_SelectedPageChanging);
                    this.Ribbon.SelectedPageChanged += new EventHandler(SelectedPageChanged);
                }
            }
        }

		public FormFind? FormFind { get; set; }

        #endregion |   Public Properties   |

		#region |   Public Methods   |

		public void GoToGraphElement(GraphElement graphElement)
		{
			if (graphElement == null)
				return;

			SimpleRibbonModulePanel ribbonModulePanel;

			if (this.ribbonModulePanelsByGraphKey.TryGetValue(graphElement.GraphKey, out ribbonModulePanel))
			{
				RibbonPage? pageGroup = this.GetRibbonPageGroup(ribbonModulePanel.RibbonPage);

				if (pageGroup != null)
					this.Ribbon.SelectedPage = pageGroup;

				
				this.Ribbon.SelectedPage = ribbonModulePanel.RibbonPage;

				if (this.Ribbon.SelectedPage == ribbonModulePanel.RibbonPage)
					ribbonModulePanel.GoToGraphElement(graphElement);
			}
		}

        #endregion |   Public Methods   |

        #region |   Protected Methods   |

        //protected virtual IClientAppContext GetAppContext() { return null; }

        protected SimpleRibbonModulePanel GetModule(RibbonPage page)
        {
            if (page == null)
            {
                return null;
            }
            else
            {
                return page.Tag as SimpleRibbonModulePanel;
            }
        }

        protected virtual string GetDarkModeSkinName() 
        { 
            return this.AppContext.UserSettings.DarkModeSkinName; 
        }

		protected void Initialize()
        {
            this.initializing = true;

			this.Load += new EventHandler(SimpleRibbonForm_Load);
            this.FormClosing += new FormClosingEventHandler(SimpleRibbonForm_FormClosing);

			this.LoadModulePanels();
			this.LoadLocationAndSizeSettings();
			
            try
			{
				this.SetStartPage(0);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.GetFullErrorMessage());
			}
			
            this.OnInitilaze();
            this.initializing = false;
        }

        protected virtual void OnInitilaze() { }
        //protected virtual void OnBarButtonChangeSkin(string skinName) { }

        protected virtual void LoadModulePanels()
        {
            //int moduleCount = 0;
            //foreach (string moduleKey in SecurityManager.CurrentUserModules())
            //{
            //Type panelType = AddInManager.GetPanelType(moduleKey);

			if (this.RibbonModuleModels == null)
				return;
			
			foreach (RibbonModuleModel ribbonModuleModel in this.RibbonModuleModels)
            {
                if (Activator.CreateInstance(ribbonModuleModel.RibbonModuleType) is SimpleRibbonModulePanel ribbonModulePanel)
                {
                    this.LoadModulePanel(ribbonModuleModel.GraphKey, ribbonModulePanel);
                    ribbonModulePanel.Name = ribbonModuleModel.Name;
                    this.OnLoadModulePanel(ribbonModulePanel);

                    if (ribbonModuleModel.RibbonModuleGroups.Count > 0)
                        ribbonModulePanel.RibbonPage.Visible = false;
                }
            }

            //foreach (RibbonModuleGroupModel ribbonModuleGroupModel in this.RibbonModuleGroupModels)
            //{
            //    SimpleRibbonModulePanel ribbonModuleGroupPanel = Activator.CreateInstance(typeof(SimpleRibbonModulePanel)) as SimpleRibbonModulePanel;
            //    ribbonModuleGroupPanel.RibbonPage.Text = ribbonModuleGroupModel.Caption;
            //    this.LoadModule(ribbonModuleGroupPanel);
            //}

            //moduleCount++;
            //}
        }

		protected virtual void LoadModulePanel(int graphKey, SimpleRibbonModulePanel ribbonModulePanel)
		{
			this.LoadModulePanel(graphKey, ribbonModulePanel, String.Empty);
		}

		protected virtual void LoadModulePanel(int graphKey, SimpleRibbonModulePanel ribbonModulePanel, string imageName)
        {
			if (ribbonModulePanel == null)
				return;

			ribbonModulePanel.ImageName = imageName;

			ribbonModulePanel.RibbonPage.Tag = ribbonModulePanel;
			//ribbonModulePanel.RibbonPage.Text = ribbonModulePanel.RibbonPage.Text.ToUpper();
			//ribbonModulePanel.RibbonPage.Appearance.Font = new System.Drawing.Font("Arial Narrow", 10.0f, FontStyle.Regular);
			ribbonModulePanel.RibbonForm = this;
			ribbonModulePanel.IsActive = false;
			
            //ribbonModulePanel.Ribbon = this.Ribbon;
			int pageIndex = this.Ribbon.Pages.Add(ribbonModulePanel.RibbonPage);
			
			if (graphKey >= 0)
				this.ribbonModulePanelsByGraphKey.Add(graphKey, ribbonModulePanel);
            //ribbonModulePanel.TempRibbonControl.Hide();
            //ribbonModulePanel.Start();
			this.OnLoadModulePanel(ribbonModulePanel);
        }


        protected void UnloadModulePanel(SimpleRibbonModulePanel ribbonModulePanel)
        {
            int graphKey = -1;

            foreach (var item in this.ribbonModulePanelsByGraphKey)
            {
                if (item.Value == ribbonModulePanel)
                {
                    graphKey = item.Key;
                    
                    break;
                }
            }

            if (graphKey >= 0)
                this.ribbonModulePanelsByGraphKey.Remove(graphKey);

            this.Ribbon.Pages.Remove(ribbonModulePanel.RibbonPage);
        }

        protected void UnloadModulePanel(int graphKey)
        {
            var modulePanel = this.ribbonModulePanelsByGraphKey[graphKey];

            this.Ribbon.Pages.Remove(modulePanel.RibbonPage);
            this.ribbonModulePanelsByGraphKey.Remove(graphKey);
        }

        protected virtual void OnLoadModulePanel(SimpleRibbonModulePanel ribbonModulePanel)
        {

            //test code
            //module.Parent = this.panelMain;
            //this.ribbonMain.MergeRibbon(module.RibbonControl);
        }

        protected virtual void SelectedPageChanged(object? sender, EventArgs e)
        {
            if (!this.initializing)
            {
                SimpleRibbonModulePanel selectedModule = this.GetModule(this.Ribbon.SelectedPage);

                if (selectedModule != null)
                {
                    if (selectedModule.RefreshOnSelect)
                        selectedModule.RefreshPanel();

                    selectedModule.ModuleIsSelected();
                }
            }
        }

        //protected virtual bool OnRibbonPageChanging(RibbonPage currentPage, RibbonPage newPage)
        //{
        //	bool canChange = false;

        //	SimpleRibbonModulePanel currentModule = null;

        //	if (currentPage != null)
        //	{
        //		currentModule = this.GetModule(currentPage);

        //		if (!currentModule.CanLeave())
        //		{
        //			canChange = false;
        //			return canChange;
        //		}
        //	}

        //	SimpleRibbonModulePanel newModule = this.GetModule(newPage);

        //	this.OnBeforeRibbonPageChange(currentPage, newPage, currentModule, newModule);

        //	if (newModule != null)
        //	{
        //		newModule.Parent = this.Panel;
        //		newModule.Dock = DockStyle.Fill;
        //		newModule.BringToFront();
        //		newModule.IsActive = true;

        //		if (!newModule.Started)
        //		{
        //			Cursor currentCursor = Cursor.Current;
        //			Cursor.Current = Cursors.WaitCursor;

        //			newModule.Start();

        //			Cursor.Current = currentCursor;
        //		}

        //		if (this.FormFind != null && !this.FormFind.IsDisposed && newModule.GraphController != null && newModule.GraphController is GraphControllerDevExpress)
        //		{
        //			this.FormFind.TextFinder = newModule.GraphController;
        //		}

        //		if (currentModule != null)
        //			currentModule.IsActive = false;
        //	}

        //	canChange = true;

        //	return canChange;
        //}

        protected virtual bool OnFormClosing()
        {
            try
            {
                //try
                //{
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
                //}
                //catch (Exception exception)
                //{
                //    MessageBox.Show(exception.Message, this.AppContext.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}

                SimpleRibbonModulePanel selectedModulePanel = this.GetModule(this.Ribbon.SelectedPage);
                    
                if (selectedModulePanel != null && !(selectedModulePanel.CanExit()))
                {
                    DialogResult result = DialogResult.Abort;
                        
                    while (result != DialogResult.OK && result != DialogResult.Cancel)
                    {
                        if (result == DialogResult.Retry)
                        {
                            result = selectedModulePanel.PrepareForExit();
                        }
                        else if (result == DialogResult.Abort)
                        {
                            string message = string.Format("Can't close module '{0}'.\r\nDo you wish to abort exiting, retry close module or\r\nignore this module and continue exiting?", selectedModulePanel.Caption);
                            
                            result = XtraMessageBox.Show(message, String.Format("Closing {0} application", this.AppContext.AppName), MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Question);

                            if (result == DialogResult.Abort)
                            {
                                result = DialogResult.Cancel;
                            }
                            else if (result == DialogResult.Ignore)
                            {
                                result = DialogResult.OK;
                            }
                        }
                    }

                    if (result == DialogResult.Cancel)
                        return false;
                }

                return true;
            }
            catch 
            {
				throw;
			}
        }

		protected void SetRibbonStyle(RibbonControlStyle ribbonStyle)
		{
			//this.UpdateSchemeCombo(ribbonStyle);
			this.UpdateLookAndFeel(ribbonStyle);
			this.SetSkinStyle(ribbonStyle);
			this.OnStyleChanged(EventArgs.Empty);
		}

		protected void UpdateLookAndFeel(RibbonControlStyle ribbonStyle)
		{
			string skinName;
			bool compactUI = false;

			//RibbonControlStyle style = (RibbonControlStyle)this.AppContext.UserSettings.RibbonStyle; //this.ribbonControl.RibbonStyle;

			switch (ribbonStyle)
			{
				case RibbonControlStyle.Default:

                    skinName = "Black"; // "WXI"; //"Office 2016 Colorful";
                    compactUI = false; // true;

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

                    skinName = "Office 2016 Colorful"; // "Office 2013";

					break;

				case RibbonControlStyle.Office2010:
				case RibbonControlStyle.MacOffice:

				default:

					skinName = "Office 2010 Blue";

					break;
			}

			this.AppContext.UserSettings.RibbonSkinName = skinName;
			this.AppContext.UserSettings.CompactUI = compactUI;
            this.AppContext.UserSettings.Save();
        }

        protected void SetSkinStyle() => this.SetSkinStyle((RibbonControlStyle)this.AppContext.UserSettings.RibbonStyle);

        protected void SetSkinStyle(RibbonControlStyle ribbonStyle)
        {
            string skinName = this.GetSkinName();

            this.Ribbon.RibbonStyle = (ribbonStyle == RibbonControlStyle.Default) ? RibbonControlStyle.Office2019 : ribbonStyle;
            this.Ribbon.ColorScheme = (RibbonControlColorScheme)this.AppContext.UserSettings.RibbonColorScheme;
            WindowsFormsSettings.CompactUIMode = (this.AppContext.UserSettings.CompactUI) ? DefaultBoolean.True : DefaultBoolean.False;

            if (skinName != UserLookAndFeel.Default.ActiveSkinName)
            {
                string oldSkinName = UserLookAndFeel.Default.ActiveSkinName;

                if (!this.AppContext.UserSettings.DarkMode)
                {
                    this.AppContext.UserSettings.RibbonSkinName = skinName;
                    this.AppContext.UserSettings.Save();
                }

                UserLookAndFeel.Default.SetSkinStyle(skinName);
                this.OnSkinNameChange(skinName, oldSkinName);
            }
        }

        private string GetSkinName()
        {
			string skinName = this.AppContext.UserSettings.RibbonSkinName;

			if (skinName == null || skinName.Length == 0)
				skinName = this.AppContext.UserSettings.DefaultRibonSkinName;

			if (this.AppContext.UserSettings.DarkMode)
				skinName = this.GetDarkModeSkinName();

            return skinName;
		}


		protected virtual void OnSkinNameChange(string skinName, string oldSkinName) 
        {
			if (this.BarSubItemChangeSkin != null)
                this.BarSubItemChangeSkin.Enabled = !this.AppContext.UserSettings.DarkMode;
		}

		protected virtual void OnBeforeRibbonPageChange(RibbonPage currentPage, RibbonPage newPage, SimpleRibbonModulePanel currentModule, SimpleRibbonModulePanel newModule) { }
		protected virtual void OnRibbonPageChange(RibbonPage oldPage, RibbonPage page, SimpleRibbonModulePanel oldModule, SimpleRibbonModulePanel module) { }
		protected virtual RibbonPage? GetRibbonPageGroup(RibbonPage page) => null;

        #endregion |   Protected Methods   |

        #region |   Private Methods   |

        private void SetStartPage(int pageIndex)
        {
            if (pageIndex >= 0 && pageIndex < this.Ribbon.Pages.Count)
            {
                RibbonPage ribonPage = this.Ribbon.Pages[pageIndex];
                
                this.SetStartPage(ribonPage);
            }
        }

        private void SetStartPage(RibbonPage page)
        {
            if (this.Ribbon.SelectedPage != page)
                this.Ribbon.SelectedPage = page;
            else
				this.SelectPage(page);
        }

		public bool SelectPage(RibbonPage page)
		{
			//if (this.Ribbon.SelectedPage == page)
			//	return true;

			SimpleRibbonModulePanel currentModule = this.GetModule(this.Ribbon.SelectedPage);

			if (currentModule == null)
				return true;

			if (!currentModule.CanLeave())
				return false;

			SimpleRibbonModulePanel newModule = this.GetModule(page);

			this.OnBeforeRibbonPageChange(this.Ribbon.SelectedPage, page, currentModule, newModule);
			this.SetModuleSelected(currentModule, newModule);
			this.OnRibbonPageChange(this.Ribbon.SelectedPage, page, currentModule, newModule);

			return true;
		}

		private void SetModuleSelected(SimpleRibbonModulePanel currentModule, SimpleRibbonModulePanel newModule)
		{
			if (newModule != null)
			{
				newModule.Parent = this.Panel;
				newModule.Dock = DockStyle.Fill;
				newModule.BringToFront();
				newModule.IsActive = true;

				if (!newModule.Started)
				{
					Cursor currentCursor = Cursor.Current;
					Cursor.Current = Cursors.WaitCursor;

					newModule.Start();

					Cursor.Current = currentCursor;
				}

				if (this.FormFind != null && !this.FormFind.IsDisposed && newModule.GraphController != null && newModule.GraphController is GraphControllerDevExpress)
					this.FormFind.TextFinder = newModule.GraphController;

				if (currentModule != null)
					currentModule.IsActive = false;
			}
		}

        #endregion |   Private Methods   |

        #region |   Private Event Methods   |

        private void SimpleRibbonForm_Load(object? sender, EventArgs e)
        {
            foreach (SkinContainer skin in SkinManager.Default.Skins)
            {
                BarCheckItem item = this.Ribbon.Items.CreateCheckItem(skin.SkinName, false);
                
                item.Tag = skin.SkinName;
                item.ItemClick += new ItemClickEventHandler(BarButtonChangeSkinClick);

                if (this.BarSubItemChangeSkin != null)
                    this.BarSubItemChangeSkin.ItemLinks.Add(item);
            }
        }

        private void Ribbon_SelectedPageChanging(object? sender, RibbonPageChangingEventArgs e)
        {
            if (!this.initializing)
				e.Cancel = !this.SelectPage(e.Page);
        }

        private void SimpleRibbonForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            e.Cancel = !this.OnFormClosing();
        }

        private void BarButtonChangeSkinClick(object? sender, ItemClickEventArgs e)
        {
            string skinName = e.Item.Tag.ToString();

            this.AppContext.UserSettings.RibbonSkinName = skinName;
            this.AppContext.UserSettings.Save();
            this.SetSkinStyle();
        }

        private void barSubItemChangeSkin_Popup(object? sender, EventArgs e)
        {
            if (this.barSubItemChangeSkin != null)
                foreach (BarItemLink link in this.barSubItemChangeSkin.ItemLinks)
                    ((BarCheckItem)link.Item).Checked = (link.Item.Caption == UserLookAndFeel.Default.ActiveLookAndFeel.SkinName);
        }

        private void LoadLocationAndSizeSettings()
        {
            try
            {
				// Set location.                                                                                      // Divide the screen in half, and find the center of the form to center it
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

		#endregion |   Private Event Methods   |

		#region |   ISimpleForm Interface   |

		FormPainter ISimpleForm.GetPainter()
		{
			return this.FormPainter;
		}

		#endregion |   ISimpleForm Interface   |
	}

	#region |   Enums   |

	public enum RibbonControlPagePlacement
	{
		Static,
		StaticGrouping,
		Dynamic,
		DynamicGrouping
	}

	#endregion |   Enums   |
}
