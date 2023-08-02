using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Simple.Objects;
using Simple.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraTreeList;
using DevExpress.XtraBars;

namespace Simple.Objects.Controls
{
    public partial class SimpleRibbonModulePanel : XtraUserControl
    {
        #region |   Private Members   |

		private bool isActive = false;
		private bool readOnly = false;
        private bool started = false;
        private bool refreshOnSelect = true;
		private SimpleRibbonFormBase? ribbonForm = null;
		private SimpleObjectGraphController? graphController = null;
        private List<ISimpleValidation> controlsToValidate = new List<ISimpleValidation>();
		private string imageName = String.Empty;
        
        #endregion

        #region |   Constructors   |

        public SimpleRibbonModulePanel()
            : base()
        {
            InitializeComponent();
            this.tempRibbonControl.Hide();
        }

        #endregion

        #region |   Events   |

        public event EventHandler ModuleSelected;

        #endregion |   Events   |

        #region |   Public Properties   |

		[Category("Simple.Properties"), Browsable(false)]
		public bool IsActive
		{
			get { return this.isActive; }
			set
			{
				if (this.isActive != value)
				{
					this.isActive = value;
					this.OnSetIsActive();
				}
			}
		}

		[Category("Simple.Properties"), DefaultValue("")]
        public string Caption
        { 
            get { return this.ribbonPage.Text; } 
        }

        [Category("Simple.Properties"), DefaultValue(false)]
        public bool ReadOnly
        {
            get{return this.readOnly; }
            set 
            {
                if (this.readOnly != value)
                {
                    this.readOnly = value;
                    this.OnReadOnlyChanged();
                }
            }
        }

        [Category("Simple.Properties"), DefaultValue(true)]
        public bool RefreshOnSelect
        {
            get { return this.refreshOnSelect; }
            set { this.refreshOnSelect = value; }
        }

        [Category("Simple.Properties"), Browsable(false)]
        public bool Started
        {
            get { return this.started; }
        }

        [Category("Simple.Properties"), Browsable(false)]
        public RibbonControl TempRibbonControl
        {
            get { return this.tempRibbonControl; }
        }

        [Category("Simple.Properties"), Browsable(false)]
        public RibbonPage RibbonPage
        {
            get { return this.ribbonPage; }
        }

		[Category("Simple.Properties"), Browsable(false)]
		public SimpleRibbonFormBase? RibbonForm
		{
			get { return this.ribbonForm; }
			set { this.ribbonForm = value; }
		}

		[Category("Simple.Properties"), Browsable(false)]
		public SimpleObjectGraphController? GraphController
		{
			get { return this.graphController; }
			set { this.graphController = value; }
		}

		[Category("Simple.Properties"), Browsable(true)]
		public string ImageName
		{
			get { return this.imageName; }
			set { this.imageName = value; }
		}
		
		#endregion

        #region |   Public Properties   |

        protected List<ISimpleValidation> ControlsToValidate 
        {
            get { return this.controlsToValidate; }
        }
        
        #endregion |   Public Properties   |

        #region |   Public Methods   |

        /// <summary>
        /// Start - initialize module and start showing data.
        /// Internally raise implementation of OnStarted() method.
        /// </summary>
        public void Start()
        {
            this.OnStart();
            this.started = true;
        }

        /// <summary>
        /// Refreshes panel.
        /// Internally raise implementation of OnRefreshPanel() method.
        /// </summary>
        public void RefreshPanel()
        {
            this.OnRefreshPanel();
        }

        /// <summary>
        /// Validate panel ie controls on it.
        /// Internally raise implementation of OnValidatePanel() method.
        /// </summary>
        public bool ValidatePanel()
        {
            bool isValid = true;
            
            foreach (ISimpleValidation validationControl in this.ControlsToValidate)
            {
                isValid = validationControl.ValidateAndSave();

                if (isValid == false)
                {
                    break;
                }
            }

            isValid &= this.OnValidatePanel();

            return isValid;
        }

        /// <summary>
        /// Returns true if panel can lose focus.
        /// Internally raise implementation of OnCanLeave() method.
        /// </summary>
        public bool CanLeave()
        {
            bool isClosed = this.OnCloseModule(ModuleCloseContext.LeaveModule);

            if (isClosed)
            {
                return this.OnCanLeave();
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if panel can be closed (on exiting application)
        /// without any other operation.
        /// Internally raise implementation of OnCanExit() method.
        /// </summary>
        public bool CanExit()
        {
            bool isClosed = this.OnCloseModule(ModuleCloseContext.ExitApp);

            if (isClosed)
            {
                return this.OnCanExit();
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Prepares modules for exiting application. Returns false
        /// if exiting application is canceled.
        /// Internally raise implementation of OnPrepareForExit() method.
        /// </summary>
        public DialogResult PrepareForExit()
        {
            DialogResult result = this.OnPrepareForExit();

            if (result != DialogResult.OK && result != DialogResult.Cancel && result != DialogResult.Abort)
                throw new ArgumentOutOfRangeException("result", "Result can only be OK, Cancel or Abort.");

            return result;
        }

		public void GoToGraphElement(GraphElement graphElement)
		{
			this.GoToGraphElement(this.graphController, graphElement);
		}
	
		public void GoToGraphElement(SimpleObjectGraphController graphController, GraphElement graphElement)
		{
			if (graphController != null)
			{
				Cursor? currentCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;

				graphController.SetFocusedGraphElement(graphElement);

				Cursor.Current = currentCursor;
			}
		}

		public void GoToGraphElement(SimpleObjectCollectionController graphController, SimpleObject graphElement)
		{
			if (graphController != null)
			{
				Cursor? currentCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;

				graphController.SetFocusedGraphElement(graphElement);

				Cursor.Current = currentCursor;
			}
		}

		public void GraphExpandAll(IGraphController graphController)
		{
			Cursor? currentCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;

			graphController.ExpandAll();
	
			Cursor.Current = currentCursor;
		}

		public void GraphCollapseAll(IGraphController graphController)
		{
			Cursor? currentCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			
			graphController.CollapseAll();
			
			Cursor.Current = currentCursor;
		}

		public void ShowFormFind(ITextFinder textFinder)
		{
            if (this.RibbonForm == null)
                return;
            
            if (this.RibbonForm.FormFind != null && this.RibbonForm.FormFind.IsDisposed)
				this.RibbonForm.FormFind = null;

			if (this.RibbonForm.FormFind == null)
			{
				this.RibbonForm.FormFind = new FormFind();
				this.RibbonForm.FormFind.TextFinder = textFinder;
				this.RibbonForm.FormFind.Show(this);
			}

			this.RibbonForm.FormFind.Focus();
		}
		
		//// TODO:
		//public void GraphFindNext(SimpleGraphController graphController)
		//{

		//}

		public void ShowRibbonPrintPreview(DevExpress.XtraTreeList.TreeList treeList)
		{
			Cursor currentCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			
			treeList.ShowRibbonPrintPreview();
			
			Cursor.Current = currentCursor;
		}

        public void ShowRibbonPrintPreview(DevExpress.XtraGrid.GridControl gridControl)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            gridControl.ShowRibbonPrintPreview();

            Cursor.Current = currentCursor;
        }
        
        public void PrintTreeList(DevExpress.XtraTreeList.TreeList treeList)
		{
			Cursor currentCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;

			treeList.Print();

			Cursor.Current = currentCursor;
		}

        public void PrintTreeList(DevExpress.XtraGrid.GridControl gridControl)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            gridControl.Print();

            Cursor.Current = currentCursor;
        }

        #endregion

        #region |   Internal Methods   |

        internal void ModuleIsSelected()
        {
            this.OnModuleSelected();
            this.RaiseModuleSelected();
        }

        #endregion |   Internal Methods   |

        #region |   Protected Virtual Methods   |

        /// <summary>
        /// Raised after ReadOnly property is changed.
        /// </summary>
        protected virtual void OnReadOnlyChanged()
        { 
        }

        /// <summary>
        /// Raised when Start() method is called.
        /// Here is place to implement initialization of module
        /// and start showing data.
        /// </summary>
        protected virtual void OnStart()
        {
        }

        /// <summary>
        /// Raised when RefreshPanel() method is called.
        /// Here is place to implement refreshing panel data.
        /// </summary>
        protected virtual void OnRefreshPanel()
        {
        }
        /// <summary>
        /// Raised when ValidatePanel() method is called.
        /// Here is place to implement validating panel controls.
        /// </summary>
        protected virtual bool OnValidatePanel()
        {
            return true;
        }

        /// <summary>
        /// Raised when CanLeave() method is called.
        /// Return true if module can leave focus to some other module.
        /// </summary>
        protected virtual bool OnCanLeave()
        {
            return this.ValidatePanel();
        } 

        /// <summary>
        /// Raised when CanExit() method is called (on exiting application).
        /// Return true if module can exit application with any other
        /// action. On false main application will call  PrepareForExit() method.
        /// </summary>
        protected virtual bool OnCanExit()
        {
            return this.ValidatePanel();
        }

        protected virtual bool OnCloseModule(ModuleCloseContext c)
        {
            return true;
        }

        /// <summary>
        /// Raised when PrepareForExit() method is called.
        /// Here is place to implement save data and other similar operations.
        /// Returns OK if can exit, Cancel if exiting is canceled by user,
        /// or Abort if any other reasno to cancel exiting
        /// </summary>
        protected virtual DialogResult OnPrepareForExit()
        {
            return this.ValidatePanel() ? DialogResult.OK : DialogResult.Cancel;
        }


        protected virtual void OnModuleSelected()
        {
        }

		protected virtual void OnSetIsActive()
		{
		}

        #endregion |   Protected Virtual Methods   |

        #region |   Private Methods   |

        private void RaiseModuleSelected()
        {
            this.ModuleSelected?.Invoke(this, new EventArgs());
        }

        #endregion |   Private Methods   |
    }

    #region |   Enums   |

    public enum ModuleCloseContext
    {
        LeaveModule,
        ExitApp
    }

    #endregion |   Enums   |
}
