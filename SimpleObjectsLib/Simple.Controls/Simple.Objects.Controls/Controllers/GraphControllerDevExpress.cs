using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using Simple;
using Simple.Collections;
using Simple.Controls;
using Simple.Controls.TreeList;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System.Diagnostics;
using DevExpress.XtraSpreadsheet.Model;

namespace Simple.Objects.Controls
{
	public class GraphControllerDevExpress : SimpleObjectGraphController, IChangableBindingObjectControl, ISimpleValidation, ITextFinder, IGraphController, IComponent, IDisposable
	{
        #region |   Private Members   |

        private PopupMenu? popupMenu = null;
        //private RibbonControl ribbonControl = null;
        private SimpleRibbonModulePanel? ribbonModulePanel = null;
        private Component? buttonAddFolder = null;
        private BarItem? buttonAddFolderPopup = null;
        private Component? buttonAddSubFolder = null;
        private BarItem? buttonAddSubFolderPopup = null;
		private Component? buttonMoveUp = null;
		private BarItem? buttonMoveUpPopup = null;
		private Component? buttonMoveDown = null;
		private BarItem? buttonMoveDownPopup = null;
		private Component? buttonRemoveColumnSorting = null;
		private BarItem? buttonRemoveColumnSortingPopup = null;
		private Component? buttonSave = null;
        private BarItem? buttonSavePopup = null;
		private Component? buttonRejectChanges = null;
		private BarItem? buttonRejectChangesPopup = null;
		private Component? buttonRemove = null;
        private BarItem? buttonRemovePopup = null;
        private BarManager? barManager = null;
		private BarSubItem? subButtonGoToGraph = null;
		private BarSubItem? subButtonGoToGraphPopup = null;
		private BarSubItem? subButtonChangeTo = null;
		private BarSubItem? subButtonChangeToPopup = null;
		private List<BarSubItem> barSubItemList = new List<BarSubItem>();
		private Dictionary<int, BarItem> goToGraphBarButtonItemsByGraphKey = new Dictionary<int, BarItem>();
		private object lockLastGraphEditor = new object();
		private int graphUpdate = 0;
		//private int largeGraphUpdate = 0;
		//private BaseEdit lastActiveEditor = null;
		//private int lastActiveEditorSelectionStart = 0;
		//private int lastActiveEditorSelectionLength = 0;
		//private bool lastActiveEditorIsPopupOpen = false;

		private FieldInfo? imageIndexFieldInfo = typeof(TreeListNode).GetField("imageIndex", BindingFlags.Instance | BindingFlags.NonPublic);
		private FieldInfo? selectImageIndexFieldInfo = typeof(TreeListNode).GetField("selectImageIndex", BindingFlags.Instance | BindingFlags.NonPublic);
		private FieldInfo? stateImageIndexFieldInfo = typeof(TreeListNode).GetField("stateImageIndex", BindingFlags.Instance | BindingFlags.NonPublic);

        #endregion |   Private Members   |

        #region |   Constructors and Initialization   |

        public GraphControllerDevExpress()
        {
        }

        public GraphControllerDevExpress(IContainer container)
            : base(container)
        {
		}

        #endregion |   Constructors and Initialization   |

        #region |   Public Properties   |

        //[Category("Controls"), DefaultValue(null)]
        //public RibbonControl RibbonControl
        //{
        //    get { return this.ribbonControl; }
        //    set 
        //    {
        //        if (this.ribbonControl != null)
        //        {
        //            //this.RibbonControl.SelectedPageChanging -= new RibbonPageChangingEventHandler(RibbonControl_SelectedPageChanging);
        //            //this.ribbonControl.SelectedPageChanged -= new EventHandler(ribbonControl_SelectedPageChanged);
        //        }

        //        this.ribbonControl = value;

        //        if (this.ribbonControl != null)
        //        {
        //            //this.RibbonControl.SelectedPageChanging += new RibbonPageChangingEventHandler(RibbonControl_SelectedPageChanging);
        //            //this.ribbonControl.SelectedPageChanged += new EventHandler(ribbonControl_SelectedPageChanged);
        //        }
        //    }
        //}

        [Category("Controls"), DefaultValue(null)]
        public SimpleRibbonModulePanel RibbonModulePanel
        {
            get { return this.ribbonModulePanel; }

            set
            {
                if (this.ribbonModulePanel != null)
                {
                    this.ribbonModulePanel.ModuleSelected -= new EventHandler(simpleRibbonModulePanel_ModuleSelected);
					this.ribbonModulePanel.GraphController = null;
                }
                
                this.ribbonModulePanel = value;

                if (this.ribbonModulePanel != null)
                {
                    this.ribbonModulePanel.ModuleSelected += new EventHandler(simpleRibbonModulePanel_ModuleSelected);

					if (!this.IsCheckBoxMode)
						this.ribbonModulePanel.GraphController = this;
                }
            }
        }

        //[Category("Controls"), DefaultValue(null)]
        //public RibbonPage RibbonPage
        //{
        //    get
        //    {
        //        return this.ribbonPage;
        //    }
        //    set
        //    {
        //        this.ribbonPage = value;

        //        if (this.ribbonPage != null)
        //        {
        //            this.ribbonPage.Ribbon.SelectedPageChanged += new EventHandler(Ribbon_SelectedPageChanged);
        //        }
        //    }
        //}

        //void Ribbon_SelectedPageChanged(object sender, EventArgs e)
        //{
        //    RibbonPage selectedPage = this.RibbonControl.SelectedPage;
            
        //    //throw new NotImplementedException();
        //}


//        void RibbonControl_SelectedPageChanging(object sender, RibbonPageChangingEventArgs e)
//        {
////            throw new NotImplementedException();
//        }

//        private void ribbonControl_SelectedPageChanged(object sender, EventArgs e)
//        {
//             RibbonPage rp = this.RibbonControl.SelectedPage;

//        }

        [Category("Controls"), DefaultValue(null)]
        public SimpleTreeList? TreeList
        {
            get { return this.GraphControl as SimpleTreeList; }
            set
            {
                if (this.TreeList != null)
                {
                    this.TreeList.BeforeFocusNode -= new BeforeFocusNodeEventHandler(treeList_BeforeFocusNode);
                    this.TreeList.FocusedNodeChanged -= new FocusedNodeChangedEventHandler(treeList_FocusedNodeChanged);
                    this.TreeList.CellValueChanging -= new CellValueChangedEventHandler(treeList_CellValueChanging);
                    //this.treeList.ValidateNode -= new ValidateNodeEventHandler(treeList_ValidateNode);
                    this.TreeList.BeforeExpand -= new BeforeExpandEventHandler(TreeList_BeforeExpand);
                    this.TreeList.AfterExpand -= new NodeEventHandler(treeList_AfterExpand);
                    this.TreeList.BeforeCollapse -= new BeforeCollapseEventHandler(treeList_BeforeCollapse);
                    this.TreeList.AfterCollapse -= new NodeEventHandler(treeList_AfterCollapse);
                    this.TreeList.BeforeCheckNode -= new CheckNodeEventHandler(treeList_BeforeCheckNode);
                    this.TreeList.KeyUp -= new KeyEventHandler(treeList_KeyUp);
                    this.TreeList.KeyDown -= new KeyEventHandler(this.OnKeyDown);
                    this.TreeList.MouseDown -= new MouseEventHandler(treeList_MouseDown);
                    this.TreeList.BeforeDragNode -= new BeforeDragNodeEventHandler(treeList_BeforeDragNode);
                    this.TreeList.DragOver -= new DragEventHandler(treeList_DragOver);
                    this.TreeList.DragDrop -= new DragEventHandler(treeList_DragDrop);
                    this.TreeList.GiveFeedback -= new GiveFeedbackEventHandler(treeList_GiveFeedback);
                    this.TreeList.AfterDropNode -= new AfterDropNodeEventHandler(treeList_AfterDropNode);
                    this.TreeList.CompareNodeValues -= new CompareNodeValuesEventHandler(treeList_CompareNodeValues);
					this.TreeList.EndSorting -= new EventHandler(treeList_EndSorting);
				}

                this.GraphControl = value;

                if (this.TreeList != null)
                {
                    this.TreeList.BeforeFocusNode += new BeforeFocusNodeEventHandler(treeList_BeforeFocusNode);
                    this.TreeList.FocusedNodeChanged += new FocusedNodeChangedEventHandler(treeList_FocusedNodeChanged);
                    this.TreeList.CellValueChanging += new CellValueChangedEventHandler(treeList_CellValueChanging);
					//this.treeList.ValidateNode += new ValidateNodeEventHandler(treeList_ValidateNode);
					this.TreeList.BeforeExpand += new BeforeExpandEventHandler(TreeList_BeforeExpand);
                    this.TreeList.AfterExpand += new NodeEventHandler(treeList_AfterExpand);
                    this.TreeList.BeforeCollapse += new BeforeCollapseEventHandler(treeList_BeforeCollapse);
                    this.TreeList.AfterCollapse += new NodeEventHandler(treeList_AfterCollapse);
                    this.TreeList.BeforeCheckNode += new CheckNodeEventHandler(treeList_BeforeCheckNode);
                    this.TreeList.KeyUp += new KeyEventHandler(treeList_KeyUp);
                    this.TreeList.KeyDown += new KeyEventHandler(this.OnKeyDown);
                    this.TreeList.MouseDown += new MouseEventHandler(treeList_MouseDown);
                    this.TreeList.BeforeDragNode += new BeforeDragNodeEventHandler(treeList_BeforeDragNode);
					this.TreeList.AfterDragNode += TreeList_AfterDragNode;
                    this.TreeList.DragOver += new DragEventHandler(treeList_DragOver);
                    this.TreeList.DragDrop += new DragEventHandler(treeList_DragDrop);
                    this.TreeList.GiveFeedback += new GiveFeedbackEventHandler(treeList_GiveFeedback);
					this.TreeList.AfterDropNode += new AfterDropNodeEventHandler(treeList_AfterDropNode);
                    this.TreeList.CompareNodeValues += new CompareNodeValuesEventHandler(treeList_CompareNodeValues);
					this.TreeList.EndSorting += new EventHandler(treeList_EndSorting);

                    //this.GraphControlGraphControlIsInitializing();
                    //this.treeList.CustomNodeImagesDraw = true;
                    //this.treeList.UseExpandedImages = true;
                    //this.TreeList.OptionsBehavior.CloseEditorOnLostFocus = false;
                }
            }
        }

		private void TreeList_AfterDragNode(object sender, AfterDragNodeEventArgs e)
		{
		}

		[Category("Controls"), DefaultValue(null)]
        public PopupMenu PopupMenu
        {
            get { return this.popupMenu; }
            set { this.popupMenu = value; }
        }

        [Category("Controls"), DefaultValue(null)]
        public BarManager BarManager
        {
            get { return this.barManager; }
            set { this.barManager = value; }
        }

        [Category("Buttons"), DefaultValue(null)]
        public Component ButtonAddFolder
        {
            get { return this.buttonAddFolder; }
            set
            {
                if (this.buttonAddFolder != null)
                    this.RemoveAddButtonPolicy(this.buttonAddFolder);

                this.buttonAddFolder = value;

                if (this.buttonAddFolder != null)
                    this.SetAddButtonPolicy(new AddButtonPolicy<GraphElement>(typeof(Folder)) { AddButtonPolicyOption = AddButtonPolicyOption.AddAsChildToParent }, this.buttonAddFolder);
            }
        }

        [Category("Buttons"), DefaultValue(null)]
        public BarItem ButtonAddFolderPopup
        {
            get { return this.buttonAddFolderPopup; }
            set
            {
                if (this.buttonAddFolderPopup != null)
                    this.RemoveAddButtonPolicy(this.buttonAddFolderPopup);

                this.buttonAddFolderPopup = value;

                if (this.buttonAddFolderPopup != null)
                    this.SetAddButtonPolicy(new AddButtonPolicy<GraphElement>(typeof(Folder)) { AddButtonPolicyOption = AddButtonPolicyOption.AddAsChildToParent }, this.buttonAddFolderPopup);
            }
        }

        [Category("Buttons"), DefaultValue(null)]
        public Component ButtonAddSubFolder
        {
            get { return this.buttonAddSubFolder; }
            set
            {
                if (this.buttonAddSubFolder != null)
                    this.RemoveAddButtonPolicy(this.buttonAddSubFolder);

                this.buttonAddSubFolder = value;

                if (this.buttonAddSubFolder != null)
                    this.SetAddButtonPolicy(new AddButtonPolicy<GraphElement>(typeof(Folder)) { AddButtonPolicyOption = AddButtonPolicyOption.AddAsChild, IsSubButton = true }, this.buttonAddSubFolder);
            }
        }

        [Category("Buttons"), DefaultValue(null)]
        public BarItem ButtonAddSubFolderPopup
        {
            get { return this.buttonAddSubFolderPopup; }
            set
            {
                if (this.buttonAddSubFolderPopup != null)
                    this.RemoveAddButtonPolicy(this.buttonAddSubFolderPopup);

                this.buttonAddSubFolderPopup = value;

                if (this.buttonAddSubFolderPopup != null)
                    this.SetAddButtonPolicy(new AddButtonPolicy<GraphElement>(typeof(Folder)) { AddButtonPolicyOption = AddButtonPolicyOption.AddAsChild, IsSubButton = true }, this.buttonAddSubFolderPopup);
            }
        }

		[Category("Buttons"), DefaultValue(null)]
		public Component ButtonMoveUp
		{
			get { return this.buttonMoveUp; }
			set
			{
				if (this.buttonMoveUp != null)
				{
					if (this.buttonMoveUp is BarItem barItem)
					{
						barItem.ItemClick -= new ItemClickEventHandler(MoveUpButton_BarItemClick);
					}
					else if (this.buttonMoveUp is Control control)
					{
						control.Click -= new EventHandler(MoveUpButton_ControlClick);
					}

					this.ButtonMoveUpList.Remove(this.buttonMoveUp);
				}

				this.buttonMoveUp = value;

				if (this.buttonMoveUp != null)
				{
					if (this.buttonMoveUp is BarItem barItem)
					{
						barItem.ItemClick += new ItemClickEventHandler(MoveUpButton_BarItemClick);
					}
					else if (this.buttonMoveUp is Control control)
					{
						control.Click += new EventHandler(MoveUpButton_ControlClick);
					}
					else
					{
						throw new ArgumentException("Button type " + this.buttonMoveUp.GetType() + " is not supported.");
					}

					this.ButtonMoveUpList.Add(this.buttonMoveUp);
				}
			}
		}

		[Category("Buttons"), DefaultValue(null)]
		public BarItem ButtonMoveUpPopup
		{
			get { return this.buttonMoveUpPopup; }
			set
			{
				if (this.buttonMoveUpPopup != null)
				{
					this.buttonMoveUpPopup.ItemClick -= new ItemClickEventHandler(MoveUpButton_BarItemClick);
					this.ButtonMoveUpList.Remove(this.buttonMoveUpPopup);
				}

				this.buttonMoveUpPopup = value;

				if (this.buttonMoveUpPopup != null)
				{
					this.buttonMoveUpPopup.ItemClick += new ItemClickEventHandler(MoveUpButton_BarItemClick);
					this.ButtonMoveUpList.Add(this.buttonMoveUpPopup);
				}
			}
		}

		[Category("Buttons"), DefaultValue(null)]
		public Component ButtonMoveDown
		{
			get { return this.buttonMoveDown; }
			set
			{
				if (this.buttonMoveDown != null)
				{
					if (this.buttonMoveDown is BarItem barItem)
					{
						barItem.ItemClick -= new ItemClickEventHandler(MoveDownButton_BarItemClick);
					}
					else if (this.buttonMoveDown is Control control)
					{
						control.Click -= new EventHandler(MoveDownButton_ControlClick);
					}

					this.ButtonMoveDownList.Remove(this.buttonMoveDown);
				}

				this.buttonMoveDown = value;

				if (this.buttonMoveDown != null)
				{
					if (this.buttonMoveDown is BarItem barItem)
					{
						barItem.ItemClick += new ItemClickEventHandler(MoveDownButton_BarItemClick);
					}
					else if (this.buttonMoveDown is Control control)
					{
						control.Click += new EventHandler(MoveDownButton_ControlClick);
					}
					else
					{
						throw new ArgumentException("Button type " + this.buttonMoveDown.GetType() + " is not supported.");
					}

					this.ButtonMoveDownList.Add(this.buttonMoveDown);
				}
			}
		}

		[Category("Buttons"), DefaultValue(null)]
		public BarItem ButtonMoveDownPopup
		{
			get { return this.buttonMoveDownPopup; }
			set
			{
				if (this.buttonMoveDownPopup != null)
				{
					this.buttonMoveDownPopup.ItemClick -= new ItemClickEventHandler(MoveDownButton_BarItemClick);
					this.ButtonMoveDownList.Remove(this.buttonMoveDownPopup);
				}

				this.buttonMoveDownPopup = value;

				if (this.buttonMoveDownPopup != null)
				{
					this.buttonMoveDownPopup.ItemClick += new ItemClickEventHandler(MoveDownButton_BarItemClick);
					this.ButtonMoveDownList.Add(this.buttonMoveDownPopup);
				}
			}
		}

		[Category("Buttons"), DefaultValue(null)]
		public Component ButtonRemoveColumnSorting
		{
			get { return this.buttonRemoveColumnSorting; }
			set
			{
				if (this.buttonRemoveColumnSorting != null)
				{
					if (this.buttonRemoveColumnSorting is BarItem barItem)
					{
						barItem.ItemClick -= new ItemClickEventHandler(RemoveColumnSortingButton_BarItemClick);
					}
					else if (this.buttonRemoveColumnSorting is Control control)
					{
						control.Click -= new EventHandler(RemoveColumnSortingButton_ControlClick);
					}

					this.ButtonRemoveColumnSortingList.Remove(this.buttonRemoveColumnSorting);
				}

				this.buttonRemoveColumnSorting = value;

				if (this.buttonRemoveColumnSorting != null)
				{
					if (this.buttonRemoveColumnSorting is BarItem barItem)
					{
						barItem.ItemClick += new ItemClickEventHandler(RemoveColumnSortingButton_BarItemClick);
					}
					else if (this.buttonRemoveColumnSorting is Control control)
					{
						control.Click += new EventHandler(RemoveColumnSortingButton_ControlClick);
					}
					else
					{
						throw new ArgumentException("Button type " + this.buttonRemoveColumnSorting.GetType() + " is not supported.");
					}

					this.ButtonRemoveColumnSortingList.Add(this.buttonRemoveColumnSorting);
				}
			}
		}

		[Category("Buttons"), DefaultValue(null)]
		public BarItem ButtonRemoveColumnSortingPopup
		{
			get { return this.buttonRemoveColumnSortingPopup; }
			set
			{
				if (this.buttonRemoveColumnSortingPopup != null)
				{
					this.buttonRemoveColumnSortingPopup.ItemClick -= new ItemClickEventHandler(RemoveColumnSortingButton_BarItemClick);
					this.ButtonRemoveColumnSortingList.Remove(this.buttonRemoveColumnSortingPopup);
				}

				this.buttonRemoveColumnSortingPopup = value;

				if (this.buttonRemoveColumnSortingPopup != null)
				{
					this.buttonRemoveColumnSortingPopup.ItemClick += new ItemClickEventHandler(RemoveColumnSortingButton_BarItemClick);
					this.ButtonRemoveColumnSortingList.Add(this.buttonRemoveColumnSortingPopup);
				}
			}
		}

		[Category("Buttons"), DefaultValue(null)]
        public Component ButtonSave
        {
            get { return this.buttonSave; }
            set
            {
                if (this.buttonSave != null)
                {
                    if (this.buttonSave is BarItem barItem)
                    {
                        barItem.ItemClick -= new ItemClickEventHandler(SaveButton_BarItemClick);
                    }
                    else if (this.buttonSave is Control control)
                    {
                        control.Click -= new EventHandler(SaveButton_ControlClick);
                    }

                    this.ButtonSaveList.Remove(this.buttonSave);
                }

                this.buttonSave = value;

                if (this.buttonSave != null)
                {
                    if (this.buttonSave is BarItem barItem)
                    {
                        barItem.ItemClick += new ItemClickEventHandler(SaveButton_BarItemClick);
                    }
                    else if (this.buttonSave is Control control)
                    {
                        control.Click += new EventHandler(SaveButton_ControlClick);
                    }
                    else
                    {
                        throw new ArgumentException("Button type " + this.buttonSave.GetType() + " is not supported.");
                    }

                    this.ButtonSaveList.Add(this.buttonSave);
                }
            }
        }

        [Category("Buttons"), DefaultValue(null)]
        public BarItem ButtonSavePopup
        {
            get { return this.buttonSavePopup; }
            set
            {
                if (this.buttonSavePopup != null)
                {
                    this.buttonSavePopup.ItemClick -= new ItemClickEventHandler(SaveButton_BarItemClick);
                    this.ButtonSaveList.Remove(this.buttonSavePopup);
                }

                this.buttonSavePopup = value;

                if (this.buttonSavePopup != null)
                {
                    this.buttonSavePopup.ItemClick += new ItemClickEventHandler(SaveButton_BarItemClick);
                    this.ButtonSaveList.Add(this.buttonSavePopup);
                }
            }
        }

		[Category("Buttons"), DefaultValue(null)]
		public Component ButtonRejectChanges
		{
			get { return this.buttonRejectChanges; }
			set
			{
				if (this.buttonRejectChanges != null)
				{
					if (this.buttonRejectChanges is BarItem barItem)
					{
						barItem.ItemClick -= new ItemClickEventHandler(RejectChangesButton_BarItemClick);
					}
					else if (this.buttonRejectChanges is Control control)
					{
						control.Click -= new EventHandler(RejectChangesButton_ControlClick);
					}

					this.ButtonRejectChangesList.Remove(this.buttonRejectChanges);
				}

				this.buttonRejectChanges = value;

				if (this.buttonRejectChanges != null)
				{
					if (this.buttonRejectChanges is BarItem barItem)
					{
						barItem.ItemClick += new ItemClickEventHandler(RejectChangesButton_BarItemClick);
					}
					else if (this.buttonRejectChanges is Control control)
					{
						control.Click += new EventHandler(RejectChangesButton_ControlClick);
					}
					else
					{
						throw new ArgumentException("Button type " + this.buttonRejectChanges.GetType() + " is not supported.");
					}

					this.ButtonRejectChangesList.Add(this.buttonRejectChanges);
				}
			}
		}

		[Category("Buttons"), DefaultValue(null)]
		public BarItem ButtonRejectChangesPopup
		{
			get { return this.buttonRejectChangesPopup; }
			set
			{
				if (this.buttonRejectChangesPopup != null)
				{
					this.buttonRejectChangesPopup.ItemClick -= new ItemClickEventHandler(RejectChangesButton_BarItemClick);
					this.ButtonRejectChangesList.Remove(this.buttonRejectChangesPopup);
				}

				this.buttonRejectChangesPopup = value;

				if (this.buttonRejectChangesPopup != null)
				{
					this.buttonRejectChangesPopup.ItemClick += new ItemClickEventHandler(RejectChangesButton_BarItemClick);
					this.ButtonRejectChangesList.Add(this.buttonRejectChangesPopup);
				}
			}
		}

		[Category("Buttons"), DefaultValue(null)]
        public Component ButtonRemove
        {
            get { return this.buttonRemove; }
            set
            {
                if (this.buttonRemove != null)
                {
                    if (this.buttonRemove is BarItem barItem)
                    {
                        barItem.ItemClick -= new ItemClickEventHandler(RemoveButton_BarItemClick);
                    }
                    else if (this.buttonRemove is Control control)
                    {
                        control.Click -= new EventHandler(RemoveButton_ControlClick);
                    }

                    this.ButtonRemoveList.Remove(this.buttonRemove);
                }

                this.buttonRemove = value;

                if (this.buttonRemove != null)
                {
                    if (this.buttonRemove is BarItem barItem)
                    {
                        barItem.ItemClick += new ItemClickEventHandler(RemoveButton_BarItemClick);
                    }
                    else if (this.buttonRemove is Control control)
                    {
                        control.Click += new EventHandler(RemoveButton_ControlClick);
                    }
                    else
                    {
                        throw new ArgumentException("Button type " + this.buttonRemove.GetType() + " is not supported.");
                    }

                    this.ButtonRemoveList.Add(this.buttonRemove);
                }
            }
        }

        [Category("Buttons"), DefaultValue(null)]
        public BarItem ButtonRemovePopup
        {
            get { return this.buttonRemovePopup; }
            set
            {
                if (this.buttonRemovePopup != null)
                {
                    this.buttonRemovePopup.ItemClick -= new ItemClickEventHandler(RemoveButton_BarItemClick);
                    this.ButtonRemoveList.Remove(this.buttonRemovePopup);
                }

                this.buttonRemovePopup = value;

                if (this.buttonRemovePopup != null)
                {
                    this.buttonRemovePopup.ItemClick += new ItemClickEventHandler(RemoveButton_BarItemClick);
                    this.ButtonRemoveList.Add(this.buttonRemovePopup);
                }
            }
        }

		[Category("Buttons"), DefaultValue(null)]
		public BarSubItem SubButtonGoToGraph
		{
			get { return this.subButtonGoToGraph; }
			set
			{
				if (this.subButtonGoToGraph != null)
					this.subButtonGoToGraph.GetItemData -= new EventHandler(SubButtonGoToGraph_GetItemData);

				this.subButtonGoToGraph = value;

				if (this.subButtonGoToGraph != null)
					this.subButtonGoToGraph.GetItemData += new EventHandler(SubButtonGoToGraph_GetItemData);
			}
		}

		[Category("Buttons"), DefaultValue(null)]
		public BarSubItem SubButtonGoToGraphPopup
		{
			get { return this.subButtonGoToGraphPopup; }
			set
			{
				if (this.subButtonGoToGraphPopup != null)
					this.subButtonGoToGraphPopup.GetItemData -= new EventHandler(SubButtonGoToGraph_GetItemData);

				this.subButtonGoToGraphPopup = value;

				if (this.subButtonGoToGraphPopup != null)
					this.subButtonGoToGraphPopup.GetItemData += new EventHandler(SubButtonGoToGraph_GetItemData);
			}
		}

		[Category("Buttons"), DefaultValue(null)]
		public BarSubItem SubButtonChangeTo
		{
			get { return this.subButtonChangeTo; }
			set
			{
				if (this.subButtonChangeTo != null)
					this.subButtonChangeTo.GetItemData -= new EventHandler(SubButtonChangeTo_GetItemData);

				this.subButtonChangeTo = value;

				if (this.subButtonChangeTo != null)
					this.subButtonChangeTo.GetItemData += new EventHandler(SubButtonChangeTo_GetItemData);
			}
		}

		[Category("Buttons"), DefaultValue(null)]
		public BarSubItem SubButtonChangeToPopup
		{
			get { return this.subButtonChangeToPopup; }
			set
			{
				if (this.subButtonChangeToPopup != null)
					this.subButtonChangeToPopup.GetItemData -= new EventHandler(SubButtonChangeTo_GetItemData);

				this.subButtonChangeToPopup = value;

				if (this.subButtonChangeToPopup != null)
					this.subButtonChangeToPopup.GetItemData += new EventHandler(SubButtonChangeTo_GetItemData);
			}
		}
		
		#endregion |   Public Properties   |

        #region |   Public Methods   |

        public object? GetNodeTag(TreeListNode node)
        {
            GraphElementNodeTag? nodeTag = node.Tag as GraphElementNodeTag;
            
            return nodeTag?.Tag;
        }

        public void SetNodeTag(TreeListNode node, object tag)
        {
            GraphElementNodeTag? nodeTag = node.Tag as GraphElementNodeTag;
            
            if (nodeTag is not null)
                nodeTag.Tag = tag;
        }

        public void SetAddButtonPolicy(Type objectType, params Component[] buttons)
        {
            this.SetAddButtonPolicy(new AddButtonPolicy<GraphElement>(objectType), buttons);
        }

        public void SetAddButtonPolicy(Type objectType, Action<GraphElement> objectAction, params Component[] buttons)
        {
            this.SetAddButtonPolicy(new AddButtonPolicy<GraphElement>(objectType, objectAction), buttons);
        }

        public void SetAddButtonPolicy(AddButtonPolicy<GraphElement> addButtonPolicy, params Component[] buttons)
        {
            foreach (Component button in buttons)
            {
                if (button is BarItem barItem)
                {
                    barItem.ItemClick += new ItemClickEventHandler(AddButton_BarItemClick);
                }
                else if (button is Control control)
                {
                    control.Click += new EventHandler(AddButton_ControlClick);
                }
                else
                {
                    throw new ArgumentException("Button type " + button.GetType() + " is not supported.");
                }

                addButtonPolicy.Buttons.Add(button);
            }

            this.AddButtonPolicyList.Add(addButtonPolicy);
        }

        public void RemoveAddButtonPolicy(Component button)
        {
            if (button is BarItem barItem)
            {
                barItem.ItemClick -= new ItemClickEventHandler(AddButton_BarItemClick);
            }
            else if (button is Control control)
            {
                control.Click -= new EventHandler(AddButton_ControlClick);
            }
            else
            {
                throw new ArgumentException("Button type " + button.GetType() + " is not supported.");
            }

            this.RemoveAddButtonPolicyByButton(button);
        }

		public void SetGoToGraphButton(int graphKey, BarItem button)
 		{
			this.goToGraphBarButtonItemsByGraphKey.Add(graphKey, button);
			button.ItemClick += goToButton_ItemClick;
		}

		//public void CreateInsertObjectSubTypes(BarSubItem barSubItem, int objectModelTableId)
		//{
		//	ISimpleObjectModel simpleObjectModel = this.ObjectManager.GetObjectModel(objectModelTableId);

		//	this.CreateInsertObjectSubTypes(barSubItem, simpleObjectModel);
		//}

		public void CreateAndInsertObjectSubTypes(ISimpleObjectModel simpleObjectModel, params BarSubItem[] barSubItems)
		{
			foreach (BarSubItem barSubItem in barSubItems)
			{
				barSubItem.ItemLinks.Clear();

				foreach (var objectTypeModelItem in simpleObjectModel.ObjectSubTypes)
				{
					BarButtonItem barButtonItem = new BarButtonItem();

					barButtonItem.Caption = objectTypeModelItem.Value.Caption;
					barButtonItem.Tag = objectTypeModelItem.Key;
					barButtonItem.Glyph = ImageControl.SmallImageCollection.Images[objectTypeModelItem.Value.ImageName + ImageControl.ImageOptionSeparator + ImageControl.ImageOptionAdd];
					barButtonItem.LargeGlyph = ImageControl.LargeImageCollection.Images[objectTypeModelItem.Value.ImageName + ImageControl.ImageOptionSeparator + ImageControl.ImageOptionAdd];

					barSubItem.ItemLinks.Add(barButtonItem, objectTypeModelItem.Value.BeginGroup);

                    this.SetAddButtonPolicy(simpleObjectModel.ObjectType, item => (item as GraphElement).SimpleObject.SetPropertyValue((item as GraphElement).SimpleObject.GetModel().ObjectSubTypePropertyModel, objectTypeModelItem.Key), barButtonItem);
                }

                this.barSubItemList.Add(barSubItem);
			}
		}

		#endregion |   Public Methods   |

		#region |   Protected Overrided Abstract Methods   |

		protected override void GraphControlSetEditMode(GraphEditMode editMode)
        {
            if (this.TreeList != null)
            {
                switch (editMode)
                {
                    case GraphEditMode.Select:

						this.TreeList.OptionsSimple.EditMode = SimpleTreeEditMode.Select;
                        
                        break;

                    case GraphEditMode.SelectEdit:

						this.TreeList.OptionsSimple.EditMode = SimpleTreeEditMode.SelectEdit;
                        
                        break;

                    case GraphEditMode.ViewOnly:

						this.TreeList.OptionsSimple.EditMode = SimpleTreeEditMode.ViewOnly;
                        
                        break;

                    default:

						this.TreeList.OptionsSimple.EditMode = SimpleTreeEditMode.Standard;
                        
                        break;
                }
            }
        }

        protected override void GraphControlSetLookAndFeelStyle(GraphLookAndFeelStyle lookAndFeelStyle)
        {
            if (this.TreeList != null)
            {
                switch (lookAndFeelStyle)
                {
                    case GraphLookAndFeelStyle.ExcelStyle:
                        
                        this.TreeList.OptionsSimple.LookAndFeelStyle = SimpleTreeLookAndFeelStyle.ExcelStyle;
                        
                        break;

                    case GraphLookAndFeelStyle.ExplorerStyle:
                        
                        this.TreeList.OptionsSimple.LookAndFeelStyle = SimpleTreeLookAndFeelStyle.ExplorerStyle;
                        
                        break;

                    default:
                        
                        this.TreeList.OptionsSimple.LookAndFeelStyle = SimpleTreeLookAndFeelStyle.Standard;
                        
                        break;
                }
            }
        }

        protected override void GraphControlSetCanDragAndDrop(bool canDragAndDrop)
        {
            if (this.TreeList != null)
            {
				//this.TreeList.OptionsBehavior.DragNodes = canDragAndDrop;
				this.TreeList.AllowDrop = canDragAndDrop;
				this.TreeList.OptionsDragAndDrop.DragNodesMode = (canDragAndDrop) ? DragNodesMode.Single : DragNodesMode.None;
                this.TreeList.OptionsDragAndDrop.DropNodesMode = DropNodesMode.Advanced;
				this.TreeList.OptionsDragAndDrop.CanCloneNodesOnDrop = true;
                //this.TreeList.DragNodesMode = TreeListDragNodesMode.Advanced;
            }
        }

        protected override void GraphControlSetImageList(ImageList imageList)
        {
            if (this.TreeList != null)
                this.TreeList.SelectImageList = imageList;
        }

        protected override void GraphControlSetStateImageList(ImageList stateImageList)
        {
            if (this.TreeList != null)
                this.TreeList.StateImageList = stateImageList;
        }

        protected override void GraphControlSetCheckBoxImageList(ImageList checkBoxImageList)
        {
            if (this.TreeList != null)
                this.TreeList.CheckBoxImageList = checkBoxImageList;
        }

        //protected virtual void SetPopupMenu()
        //{
        //    if (this.TreeList != null && this.PopupMenu != null)
        //    {
        //        if (this.RibbonControl != null)
        //        {
        //            //this.RibbonControl.SetPopupContextMenu(this.TreeList, this.PopupMenu);
        //            //this.popupMenu = new PopupMenu();
        //            //this.PopupMenu.Ribbon = this.RibbonControl;
        //        }
        //        else if (this.BarManager != null)
        //        {
        //            //this.BarManager.SetPopupContextMenu(this.TreeList, this.PopupMenu);
        //            //this.popupMenu = new PopupMenu(this.BarManager);
        //        }
        //    }
        //}

		protected override void GraphControlBeginUpdate()
        {
			//lock (this.lockLastGraphEditor)
			//{
			//	if (this.graphUpdate == 0 && this.largeGraphUpdate == 0)
			//		this.SaveLastEditorState();

			this.graphUpdate++;
			//}
            
            this.TreeList?.BeginUpdate();
        }

        protected override void GraphControlEndUpdate()
        {
            this.TreeList?.EndUpdate();

			//lock (lockLastGraphEditor)
			//{
			this.graphUpdate--;

			//	if (this.graphUpdate == 0 && this.largeGraphUpdate == 0)
			//		this.RestoreLastEditorState();
			//}
        }

		//protected override void GraphControlBeginLargeUpdate()
		//{
		//	//lock (this.lockLastGraphEditor)
		//	//{
		//	//	if (this.graphUpdate == 0 && this.largeGraphUpdate == 0)
		//	//		this.SaveLastEditorState();

		//	this.largeGraphUpdate++;
		//	//}

		//	this.TreeList.BeginUnboundLoad();
		//}

		//protected override void GraphControlEndLargeUpdate()
		//{
		//	this.TreeList.EndUnboundLoad();

		//	//lock (lockLastGraphEditor)
		//	//{
		//	this.largeGraphUpdate--;
				
		//	//	if (this.graphUpdate == 0 && this.largeGraphUpdate == 0)
		//	//	{
		//	//		this.RestoreLastEditorState();
		//	//	}
		//	//}
  //      }

        protected internal override int GraphControlAddColumn(string columnName, string caption, BindingDataType dataType, BindingEditorType editorType)
        {
            if (this.TreeList is null)
                return -1;
            
            TreeListColumn column = this.TreeList.Columns.Add();
            
            column.Name = columnName;
            column.FieldName = columnName;
            column.Caption = caption;
            column.VisibleIndex = this.TreeList.Columns.Count - 1;
            column.Format.FormatType = DevExpress.Utils.FormatType.Custom;
            column.Format.FormatString = "\\";

			//if (column.Name == this.columnNameOrderIndexHelper)
			//	column.OptionsColumn.ShowInCustomizationForm = false;

			// TODO: Uncomment this in 13.2 version and higher
			column.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;

			// TODO: editor type implementation based on BindingDataType dataType and BindingEditorType editorType

			return column.AbsoluteIndex; // column.AbsoluteIndex
        }

		protected internal override void GraphControlSetColumnCaption(int columnIndex, string caption)
        {
			TreeListColumn? column = this.TreeList?.Columns[columnIndex];
            
            if (column is not null)
                column.Caption = caption;
        }

        protected internal override void GraphControlSetColumnDataType(int columnName, BindingDataType dataType)
        {
            TreeListColumn? column = this.TreeList?.Columns[columnName];
            // TODO: 
        }

		protected internal override void GraphControlSetColumnEditorType(int columnIndex, BindingEditorType editorType)
        {
			TreeListColumn? column = this.TreeList?.Columns[columnIndex];
            // TODO: 
        }

		protected internal override int GraphControlGetColumnWidth(int columnIndex)
        {
            if (this.TreeList is null)
                return -1;
            
            return this.TreeList.Columns[columnIndex].Width;
        }

		protected internal override void GraphControlSetColumnWidth(int columnIndex, int width)
        {
            if (this.TreeList is null)
                return;

            this.TreeList.Columns[columnIndex].Width = width;
        }

		protected internal override void GraphControlRemoveColumn(int columnIndex)
        {
            if (this.TreeList is null)
                return;
            
            TreeListColumn column = this.TreeList.Columns[columnIndex];
            
            this.TreeList.Columns.Remove(column);
        }

		protected override void GraphControlSetColumnError(int columnIndex, string errorMessage)
        {
            if (this.TreeList is null)
                return;
            
            TreeListColumn column = this.TreeList.Columns[columnIndex];
            
            this.TreeList.SetColumnError(column, errorMessage, DevExpress.XtraEditors.DXErrorProvider.ErrorType.Default);
        }

        protected override void GraphControlResetColumnErrors()
        {
            if (this.TreeList is null)
                return;

            foreach (TreeListColumn column in this.TreeList.Columns)
                this.TreeList.SetColumnError(column, "", DevExpress.XtraEditors.DXErrorProvider.ErrorType.None);
        }

        protected override void GraphControlBestFitColumns(bool applyAutoWidth)
        {
            if (this.TreeList is null)
                return;

            this.TreeList.OptionsView.AutoWidth = applyAutoWidth;
            this.TreeList.BestFitColumns(applyAutoWidth);
        }

		protected override void GraphControlRemoveColumnSorting()
		{
			this.TreeList?.ClearSorting();
		}

		protected override int GraphControlGetSortedColumnCount()
		{
            if (this.TreeList is null)
                return -1;

            int sortedColumnCount = this.TreeList.SortedColumnCount;
            TreeListColumn column = this.TreeList.Columns.ColumnByName(this.columnNameOrderIndexHelper);
            
            if (column != null && column.SortOrder != SortOrder.None)
				sortedColumnCount--;

			return sortedColumnCount;
		}

        protected override object GraphControlAddNode(object? parentNode, object nodeTag)
        {
            TreeListNode? parent = parentNode as TreeListNode;
            TreeListNode node = this.AppendTreeListNode(parent, nodeTag)!;
            
            return node;
        }

        //protected override GraphElementNodeTag GraphControlCreateNodeTag(GraphElement graphElement)
        //{
        //    GraphElementNodeTag nodeTag = new GraphElementNodeTag(graphElement);
            
        //    return nodeTag;
        //}

        protected override GraphElementNodeTag GraphControlGetGraphNodeTag(object node)
        {
            return (node as TreeListNode).Tag as GraphElementNodeTag;
        }

        protected override void GraphControlSetNodeHasChildrenProperty(object node, bool value)
        {
            (node as TreeListNode).HasChildren = value;
        }

        protected override void GraphControlSetNodeCheckedProperty(object node, bool value)
        {
            (node as TreeListNode).Checked = value;
        }

        protected override void GraphControlDeleteNode(object node)
        {
            this.TreeList.DeleteNode(node as TreeListNode);
            //TreeListNode treeListNode = node as TreeListNode;
            //GraphNodeTag nodeTag = treeListNode.Tag as GraphNodeTag;
            //nodeTag.GraphElement = null;
            //treeListNode.TreeList.DeleteNode(treeListNode);
        }

		protected override object GraphControlGetNodeCellEditValue(object node, int columnIndex)
		{
			TreeListNode treeListNode = node as TreeListNode;
			
            return treeListNode.GetValue(columnIndex);
		}

        protected override void GraphControlSetNodeCellEditValue(object node, int columnIndex, object? editValue)
        {
            //TreeListColumn column = this.TreeList.Columns[columnIndex];

            //if (requester != null && column.ColumnEdit != null && column.ColumnEdit == requester)
            //    return;

            if (node is TreeListNode treeListNode)
                treeListNode.SetValue(columnIndex, editValue);
        }

        protected override void GraphControlMoveNode(object sourceNode, object? destinationNode)
        {
            TreeListNode? sn = sourceNode as TreeListNode;
            TreeListNode? dn = destinationNode as TreeListNode;

            //if (sourceNode is TreeListNode sn && destinationNode is TreeListNode dn)
                this.TreeList?.MoveNode(sn, dn, modifySource: false);
        }

        protected override void GraphControlExpandNode(object node)
        {
            if (node is TreeListNode treeListNode)
                treeListNode.Expanded = true;
        }

        protected override void GraphControlCollapseNode(object node)
        {
            if (node is TreeListNode treeListNode)
                treeListNode.Expanded = false;
        }

        protected override void GraphControlExpandAll()
        {
            this.TreeList?.ExpandAll();
        }

        protected override void GraphControlCollapseAll()
        {
            this.TreeList?.CollapseAll();
        }

        protected override object? GraphControlGetParentNode(object node)
        {
            if (node is TreeListNode treeListNode)
                return treeListNode.ParentNode;

            return default;
        }

        protected override object? GraphControlGetFocusedNode()
        {
            return this.TreeList?.FocusedNode;
        }

		protected override int GraphControlGetFocusedColumnIndex()
		{
			return (this.TreeList?.FocusedColumn != null) ? this.TreeList.FocusedColumn.AbsoluteIndex : -1;
		}

		protected override void GraphControlSetFocusedNode(object node)
        {
            if (this.TreeList is not null)
                this.TreeList.FocusedNode = node as TreeListNode;
        }

		protected override void GraphControlSetFocusedColumn(int columnIndex)
        {
            if (this.TreeList is null)
                return;
            
            TreeListColumn column = this.TreeList.Columns[columnIndex];
            
            this.TreeList.FocusedColumn = column;
        }

        protected override void GraphControlFocus()
        {
            this.TreeList?.Focus();
        }

        protected override void GraphControlCloseEditor()
        {
            this.TreeList?.CloseEditor();
            //this.TreeList.EndCurrentEdit();
        }

        protected override void GraphControlShowEditor()
        {
            if (this.TreeList is null)
                return;

            this.TreeList.OptionsBehavior.Editable = true;
            this.TreeList.ShowEditor();
        }

		protected override void GraphControlSetNodeOrderIndex(object node, int orderIndex)
		{
            if (this.TreeList is null)
                return;

            TreeListNode? treeListNode = node as TreeListNode;
            int nodeIndex = this.TreeList.GetNodeIndex(treeListNode);

            this.TreeList.SetNodeIndex(treeListNode, orderIndex);
		}

		protected override void GraphControlSetNodeImageIndex(object node, int imageIndex)
        {
            TreeListNode? treeListNode = node as TreeListNode;

			this.imageIndexFieldInfo?.SetValue(treeListNode, imageIndex);
			this.selectImageIndexFieldInfo?.SetValue(treeListNode, imageIndex);
			this.TreeList?.RefreshNode(treeListNode);
			
			//treeListNode.ImageIndex = imageIndex;
			//treeListNode.SelectImageIndex = imageIndex;
        }

        protected override void GraphControlSetNodeExpandedImageIndex(object node, int imageIndex)
        {
            if (node is SimpleTreeListNode treeListNode)
                treeListNode.ExpandedImageIndex = imageIndex;
        }

        protected override void GraphControlSetNodeStateImageIndex(object node, int imageIndex)
        {
            TreeListNode? treeListNode = node as TreeListNode;

			this.stateImageIndexFieldInfo?.SetValue(treeListNode, imageIndex);
			this.TreeList?.RefreshNode(treeListNode);

//			treeListNode.StateImageIndex = imageIndex;
        }

        protected override void GraphControlSetNodeCheckBoxImageIndex(object node, int imageIndex)
        {
            if (node is SimpleTreeListNode treeListNode)
                treeListNode.CheckBoxImageIndex = imageIndex;
        }

        protected override void GraphControlSetCheckBoxMode(bool checkBoxMode)
        {
            if (this.TreeList is null)
                return;
            
            this.TreeList.OptionsView.ShowCheckBoxes = checkBoxMode;
            this.TreeList.CustomNodeImagesDraw = checkBoxMode;
            this.TreeList.CustomCheckBoxDraw = checkBoxMode;
        }

        protected override bool GraphControlGetColumnEnableProperty(int columnIndex)
        {
            if (this.TreeList is null)
                return false;

            return this.TreeList.Columns[columnIndex].OptionsColumn.AllowEdit;
        }

		protected override void GraphControlSetColumnEnableProperty(int columnIndex, bool value)
        {
            if (this.TreeList is not null)
                this.TreeList.Columns[columnIndex].OptionsColumn.AllowEdit = value;
        }

		protected override bool GraphControlGetColumnVisibleProperty(int columnIndex)
		{
            if (this.TreeList is null)
                return false;

            return this.TreeList.Columns[columnIndex].Visible;
		}

		protected override void GraphControlSetColumnVisibleProperty(int columnIndex, bool value)
		{
            if (this.TreeList is not null)
                this.TreeList.Columns[columnIndex].Visible = value;
		}

		protected override bool GraphControlGetColumnShowInCustomizationFormProperty(int columnIndex)
		{
            if (this.TreeList is null)
                return false;

            return this.TreeList.Columns[columnIndex].OptionsColumn.ShowInCustomizationForm;
		}

		protected override void GraphControlSetColumnShowInCustomizationFormProperty(int columnIndex, bool value)
		{
			if (this.TreeList is not null)
                this.TreeList.Columns[columnIndex].OptionsColumn.ShowInCustomizationForm = value;
		}

		protected override void GraphControlSetColumnSorting(int columnIndex, SortOrder sortOrder)
		{
			if (this.TreeList is not null)
                this.TreeList.Columns[columnIndex].SortOrder = sortOrder;
		}

		protected override void GraphControlSetButtonEnableProperty(Component button, bool value)
        {
            if (button is BarItem barItem)
            {
                if (barItem.Enabled != value)
				{
                    barItem.Enabled = value;
                    barItem.Refresh();
				}
            }
            else if (button is Control control)
            {
                if (control.Enabled != value)
				{
                    control.Enabled = value;
                    control.Refresh();
                }
            }
            else
            {
                throw new ArgumentException("Button type " + button.GetType() + " is not supported");
            }
        }

        protected override GraphElement? GraphControlGetGraphElementByNode(object node)
        {
            if (node is TreeListNode treeListNode)
                return this.GetGraphElement(treeListNode);
            
            return default;
        }

		protected override Component? GraphControlGetEditorComponent(int columnIndex)
        {
            if (this.TreeList is null)
                return default;

            TreeListColumn column = this.TreeList.Columns[columnIndex];
            Component editorComponent = column.ColumnEdit;

            if (editorComponent is BaseEdit baseEdit)
                editorComponent = baseEdit.Properties;

            return editorComponent;
        }

        protected override void GraphControlClearNodes()
        {
            this.TreeList?.ClearNodes();
        }

		protected override object[] GraphControlGetChildNodes(object node)
		{
			if (node is TreeListNode treeListNode)
			{
                return new SimpleCollection<object>(treeListNode.Nodes).ToArray();
			}
			else if (this.TreeList is not null)
			{
				return new SimpleCollection<object>(this.TreeList.Nodes).ToArray();
			}
            else
			{
                return new object[] { };
			}
		}

		// Before calling this method, graph controller must set this node and appropriate column to be focused.
		// TODO: remove this limitation and highlight and required node, and part of a text, without need to focus the node or column.
		protected override void GraphControlHighlightCellText(object node, int columnIndex, int startTextPosition, int textLength)
		{
			if (this.TreeList is null)
                return;

			if (this.TreeList.ActiveEditor is TextEdit textEdit)
			{
				textEdit.SelectionStart = startTextPosition;
				textEdit.SelectionLength = textLength;
			}
			else if (this.TreeList.ActiveEditor is DateEdit dateEdit)
			{
				dateEdit.SelectionStart = startTextPosition;
				dateEdit.SelectionLength = textLength;
			}
		}

        #endregion |   Protected Overrided Abstract Methods   |

        #region |   Protected Overrided Methods   |

		//protected override void OnGraphElementCreated(GraphElementRequesterEventArgs e)
		//{
		//	this.SetButtonGoToGraphEnableProperty(this.FocusedNode);
		//	base.OnGraphElementCreated(e);
		//}

		//protected override void OnAfterGraphElementDelete(object requester, GraphElement graphElement)
		//{
		//	base.OnAfterGraphElementDelete(requester, graphElement);
		//	this.SetButtonGoToGraphEnableProperty(this.FocusedNode);
		//}

		protected override void SetButtonsEnableProperty(GraphElement graphElement)
		{
			base.SetButtonsEnableProperty(graphElement);

			// barSubItems buttons
			foreach (BarSubItem barSubItem in this.barSubItemList)
			{
				bool enabled = false;

				for (int i = 0; i < barSubItem.ItemLinks.Count; i++)
				{
					BarItem barItem = barSubItem.ItemLinks[i].Item;

					if (barItem.Enabled)
					{
						enabled = true;
						
                        break;
					}
				}

				barSubItem.Enabled = enabled;
			}

			// SubButtonGoToGraph buttons
			if (this.SubButtonGoToGraph != null || this.SubButtonGoToGraphPopup != null)
			{
				//bool enabled = this.SubButtonGoToGraph.ItemLinks.Count > 0;
				
				bool enabled = false;

				// graphElement.SimpleObject.GraphElements might contain non visible (helper) graphs. So this method is not reliable.
				//if (graphElement != null && graphElement.SimpleObject != null && graphElement.SimpleObject.GraphElements.Count > 1) 
				//	enabled = true;

				if (this.FocusedSimpleObject != null)
				{
					foreach (var goToGraphItem in this.goToGraphBarButtonItemsByGraphKey)
					{
						GraphElement? graphElementToGo = this.FocusedSimpleObject?.GetGraphElement(goToGraphItem.Key);

						if (graphElementToGo != null)
						{
							enabled = true;
							
                            break;
						}
					}
				}

				if (this.SubButtonGoToGraph != null)
					this.SubButtonGoToGraph.Enabled = enabled;

				if (this.SubButtonGoToGraphPopup != null)
					this.SubButtonGoToGraphPopup.Enabled = enabled;
			}

			// SubButtonChangeTo buttons
			if (this.SubButtonChangeTo != null || this.SubButtonChangeToPopup != null)
			{
				bool enabled = false;

				if (graphElement != null && graphElement.SimpleObject != null && graphElement.SimpleObject.GetModel().ObjectSubTypes.Count > 1)
					enabled = true;

				if (this.SubButtonChangeTo != null)
					this.SubButtonChangeTo.Enabled = enabled;

				if (this.SubButtonChangeToPopup != null)
					this.SubButtonChangeToPopup.Enabled = enabled;
			}
		}

        protected override void OnDispose()
        {
            this.TreeList = null;
            base.OnDispose();
        }

        #endregion |   Protected Overrided Methods   |

        #region |   Private TreeList Events   |

        private void treeList_BeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            if (e.Node != null && e.OldNode != null && e.Node != e.OldNode)
            {
                bool canFocus = true;
                
                this.GraphControlBeforeNodeIsFocused(e.Node, e.OldNode, ref canFocus);
                e.CanFocus = canFocus;
            }
        }

        private void treeList_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            this.GraphControlFocusedNodeIsChanged(e.Node, e.OldNode);
			//this.SetButtonGoToGraphEnableProperty(this.FocusedNode);
        }

		private void treeList_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
			this.GraphControlCellValueIsChanged(e.Node, e.Column.AbsoluteIndex, e.Value);
			//Debug.WriteLine(e.Value);
		}

		private void treeList_KeyUp(object? sender, KeyEventArgs e)
        {
            // Restore old node value on ESCAPE key press
            if (e.KeyCode == Keys.Escape && this.TreeList.FocusedNode != null && this.TreeList.FocusedColumn != null)
            {
                object cellValue = this.TreeList.FocusedNode[this.TreeList.FocusedColumn];
                this.GraphControlCellValueIsChanged(this.TreeList.FocusedNode, this.TreeList.FocusedColumn.AbsoluteIndex, cellValue);
            }
        }

        private void treeList_MouseDown(object? sender, MouseEventArgs e)
        {
            if (this.PopupMenu != null && e.Button == MouseButtons.Right && this.TreeList is not null)
            {
                TreeListHitInfo hitInfo = this.TreeList.CalcHitInfo(e.Location);

                if (hitInfo.HitInfoType != HitInfoType.Column && hitInfo.HitInfoType != HitInfoType.BehindColumn)
                    this.PopupMenu.ShowPopup(Cursor.Position);
            }
        }

        private void treeList_BeforeDragNode(object sender, BeforeDragNodeEventArgs e)
        {
            e.CanDrag = this.GraphControlCanDragNode(e.Node);
        }

        //private void treeList_DragLeave(object sender, EventArgs e)
        //{
        //    //throw new NotImplementedException();
        //}

        private void treeList_DragOver(object sender, DragEventArgs e)
        {
			DXDragEventArgs args = this.TreeList.GetDXDragEventArgs(e);
            TreeListNode? parentNode = this.GetDragDropParentNode(args.TargetNode, args.DragInsertPosition);

            //if (args.DragInsertPosition != DragInsertPosition.Before && args.DragInsertPosition != DragInsertPosition.After)
                e.Effect = this.GraphControlGetDragDropEffect(args.Node, parentNode);
            
			if (e.Effect == DragDropEffects.None)
                if (args.DragInsertPosition == DragInsertPosition.Before || args.DragInsertPosition == DragInsertPosition.After)
                    if (args.TargetNode != null && args.Node.ParentNode == args.TargetNode.ParentNode)
                        e.Effect = DragDropEffects.Move;
            
            //object dragNode, targetNode;

			//         dragNode = e.Data.GetData(typeof(TreeListNode));

			//         Point p = this.TreeList.PointToClient(new Point(e.X, e.Y));
			//         targetNode = this.TreeList.CalcHitInfo(p).Node;

			//         e.Effect = this.GraphControlGetDragDropEffect(dragNode, targetNode);
		}

        private TreeListNode? GetDragDropParentNode(TreeListNode targetNode, DragInsertPosition dragInsertPosition)
		{
            TreeListNode? parentNode = targetNode;

            if (dragInsertPosition == DragInsertPosition.Before || dragInsertPosition == DragInsertPosition.After)
                parentNode = targetNode?.ParentNode;

            return parentNode;
        }

        private void treeList_DragDrop(object sender, DragEventArgs e)
        {
			DXDragEventArgs args = this.TreeList.GetDXDragEventArgs(e);
            //this.GraphControlDragDrop(args.Node, args.TargetNode);
            //TreeListNode draggedNode = args.Node;
            TreeListNode? draggedNode = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
            TreeListNode? parentNode = this.GetDragDropParentNode(args.TargetNode, args.DragInsertPosition);

            
   //         else if (insertPosition == DragInsertPosition.AsChild)
			//{
   //             parentNode = parentNode;
   //         }

            if (parentNode == draggedNode)
                return;
            
            //GraphElement draggedGraphElement = this.GetGraphElement(draggedNode);
            //GraphElement parentGraphElement = (parentNode != null) ? this.GetGraphElement(parentNode) : null;            

            //Point pt = this.TreeList.PointToClient(new Point(e.X, e.Y));
            //TreeListHitTest ht = this.TreeList.ViewInfo.GetHitTest(pt);
            //TreeListNode targetNode = ht.Node;

            //if (args.TargetNode != targetNode)



            //bool isMovedManualy = false;

            //if (!(targetNode != null && targetNode.HasChildren)) // If terget node has children no need for since destination node will be targetNode.Nodes[0] and will moved automatically
            //{
            //isMovedManualy = this.TreeList.MoveNode(draggedNode, targetNode);

            if (parentNode == null)
                this.TreeList?.MoveNode(draggedNode, parentNode);
            //if (isMovedManualy)
            //{
            //    int position = this.TreeList.GetNodeIndex(draggedNode);
            //    GraphElement graphElement = this.GetGraphElement(draggedNode);

            //    if (position != graphElement.OrderIndex)
            //        graphElement.OrderIndex = position;
            //}
            //}

            //         if (insertPosition == DragInsertPosition.AsChild)
            //{
            //             targetPosition = -1;
            //         }
            //         else if (insertPosition == DragInsertPosition.Before)
            //{
            //             targetPosition = 0;
            //}
            //         else if (insertPosition == DragInsertPosition.After)
            //{
            //             targetPosition = 1;
            //}

            if (draggedNode is not null)
                this.GraphControlDragDrop(draggedNode, parentNode);

            //int draggedNodeIndex = this.TreeList.GetNodeIndex(draggedNode);

   //         if (insertPosition == DragInsertPosition.Before)
			//{
   //             draggedNodeIndex = this.TreeList.GetNodeIndex(args.TargetNode);
   //         }
   //         else if (insertPosition == DragInsertPosition.After)
			//{
   //             draggedNodeIndex = this.TreeList.GetNodeIndex(args.TargetNode);

   //             if (draggedNode.ParentNode != args.TargetNode.ParentNode) // If in same collection, no need for +1
   //                 draggedNodeIndex++;
   //             //            int targetPosition2 = (targetNode.ParentNode != null) ? targetNode.ParentNode.Nodes.IndexOf(targetNode) : this.TreeList.Nodes.IndexOf(targetNode);
   //         }
   //         else
			//{
   //             draggedNodeIndex = this.TreeList.GetNodeIndex(draggedNode);
   //         }

   //         else if (parentNode != null)
			//{
   //             draggedNodeIndex = parentNode.Nodes.Count;
   //         }
   //         else
			//{
   //             draggedNodeIndex = this.TreeList.Nodes.Count - 1;
   //         }

            //if (draggedGraphElement.OrderIndex != draggedNodeIndex)
            //{
            //    draggedGraphElement.Requester = this;
            //    draggedGraphElement.OrderIndex = draggedNodeIndex;
            //}

            //if (targetPosition != draggedGraphElement.OrderIndex)
            //    draggedGraphElement.OrderIndex = targetPosition;


            //object dragNode, targetNode;

            //         dragNode = e.Data.GetData(typeof(TreeListNode));

            //         Point p = this.TreeList.PointToClient(new Point(e.X, e.Y));
            //         targetNode = this.TreeList.CalcHitInfo(p).Node;

            //         this.GraphControlDragDrop(dragNode, targetNode);

            //e.Effect = DragDropEffects.Move;
        }

        // Add a node to the TreeList when a grid row is dropped.
        //private void treeList_DragDrop2(object sender, DragEventArgs e)
        //{
        //    // Get extended arguments of the drag event.
        //    DXDragEventArgs args = treeList.GetDXDragEventArgs(e);

        //    // Get how a node is inserted (as a child, before or after a node, or at the end of the node collection).
        //    DragInsertPosition position = args.DragInsertPosition;
        //    Person dataRow = e.Data.GetData(typeof(DragAndDropRows.Person)) as Person;
        //    if (dataRow == null) return;
        //    int parentID = (int)treeList.RootValue;

        //    // Get the node over which the row is dropped.
        //    TreeListNode node = args.TargetNode;

        //    // Add a node at the root level.
        //    if (node == null)
        //    {
        //        treeList.AppendNode((new PersonEx(dataRow, parentID)).ToArray(), null);
        //    }
        //    else
        //    {
        //        // Add a child node to the target node.
        //        if (position == DragInsertPosition.AsChild)
        //        {
        //            parentID = Convert.ToInt32(node.GetValue("ID"));
        //            Object[] targetObject = (new PersonEx(dataRow, parentID)).ToArray();
        //            treeList.AppendNode(targetObject, node);
        //        }
        //        // Insert a node before the target node.
        //        if (position == DragInsertPosition.Before)
        //        {
        //            parentID = Convert.ToInt32(node.GetValue("ParentID"));
        //            Object[] targetObject = (new PersonEx(dataRow, parentID)).ToArray();
        //            TreeListNode newNode = treeList.AppendNode(targetObject, node.ParentNode);
        //            int targetPosition;
        //            if (node.ParentNode == null)
        //                targetPosition = treeList.Nodes.IndexOf(node);
        //            else targetPosition = node.ParentNode.Nodes.IndexOf(node);
        //            treeList.SetNodeIndex(newNode, targetPosition);
        //        }
        //        node.Expanded = true;
        //    }
        //}

        private void treeList_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            //e.UseDefaultCursors = true;
		}

        private void treeList_AfterDropNode(object sender, AfterDropNodeEventArgs e)
        {
            if (this.TreeList is null)
                return;
            
            int nodeIndex = this.TreeList.GetNodeIndex(e.Node);
            
            this.GraphControlAfterDropNode(e.Node, nodeIndex);
        }

        private void treeList_BeforeCollapse(object sender, BeforeCollapseEventArgs e)
        {
            bool canCollapse = true;
            
            this.GraphControlBeforeNodeIsCollapsed(e.Node, ref canCollapse);
            e.CanCollapse = canCollapse;

            //e.CanCollapse = this.lastValidationNodeResult.IsValid;

            //if (!e.CanCollapse)
            //{
            //    // Allow collapsing nodes but prohibit colapsing any of the parent error nodes.

            //    GraphElement failedGraphElement = this.GetGraphElement(this.lastValidationFailedNode);
            //    GraphElement graphElementToBeExpanded = this.GetGraphElement(e.Node);

            //    e.CanCollapse = !failedGraphElement.HasAsParent(graphElementToBeExpanded);
            //}
        }

        private void TreeList_BeforeExpand(object sender, BeforeExpandEventArgs e)
        {
            this.GraphControlBeforeNodeIsExpanded(e.Node);
        }

        private void treeList_AfterExpand(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            this.GraphControlNodeIsExpanded(e.Node);
        }

        private void treeList_AfterCollapse(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            this.GraphControlNodeIsCollapsed(e.Node);
        }

        private void treeList_BeforeCheckNode(object sender, CheckNodeEventArgs e)
        {
            bool canCheck = true;
            bool checkValue = e.State == CheckState.Checked;
            this.GraphControlNodeCheckStateIsChanged(e.Node, checkValue, ref canCheck);

            e.CanCheck = canCheck;
        }

        private void treeList_CompareNodeValues(object sender, CompareNodeValuesEventArgs e)
        {
            GraphColumn? graphColumn = this.Columns[e.Column.AbsoluteIndex];
            
            if (graphColumn is not null)
                e.Result = this.GraphControlCompareColumnNodeValues(graphColumn, e.Node1, e.Node2, e.NodeValue1, e.NodeValue2, e.SortOrder, e.Result);
        }

		private void treeList_EndSorting(object sender, EventArgs e)
		{
			this.SetButtonsSortingEnableProperty();
		}

        #endregion |   Private TreeList Events   |

        #region |   Private Methods   |

        private TreeListNode? AppendTreeListNode(TreeListNode? parentNode, object nodeTag)
        {
            return this.TreeList?.AppendNode(null, parentNode, nodeTag);
        }

        private GraphElement? GetGraphElement(TreeListNode node)
        {
            if (node.Tag is GraphElementNodeTag graphElementNodeTag)
                return graphElementNodeTag?.GraphElement;

            return default;
        }

        //private void AddFolderButton_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    this.GraphControlButtonAddFolderIsClicked(e.Item);
        //}

        //private void AddSubFolderButton_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    this.GraphControlButtonAddSubFolderIsClicked(e.Item);
        //}

        private void AddButton_BarItemClick(object? sender, ItemClickEventArgs e)
        {
            this.GraphControlButtonAddIsClicked(e.Item);
        }

        private void AddButton_ControlClick(object? sender, EventArgs e)
        {
            if (sender is Component component)
                this.GraphControlButtonAddIsClicked(component);
        }

		private void MoveUpButton_BarItemClick(object? sender, ItemClickEventArgs e)
		{
			this.GraphControlButtonMoveUpIsClicked(e.Item);
		}

		private void MoveUpButton_ControlClick(object? sender, EventArgs e)
		{
            if (sender is Component component)
                this.GraphControlButtonMoveUpIsClicked(component);
		}

		private void MoveDownButton_BarItemClick(object? sender, ItemClickEventArgs e)
		{
			this.GraphControlButtonMoveDownIsClicked(e.Item);
		}

		private void MoveDownButton_ControlClick(object? sender, EventArgs e)
		{
            if (sender is Component component)
                this.GraphControlButtonMoveDownIsClicked(component);
		}

		private void RemoveColumnSortingButton_BarItemClick(object? sender, ItemClickEventArgs e)
		{
			this.GraphControlButtonRemoveColumnSortingIsClicked(e.Item);
		}

		private void RemoveColumnSortingButton_ControlClick(object? sender, EventArgs e)
		{
            if (sender is Component component)
                this.GraphControlButtonRemoveColumnSortingIsClicked(component);
		}

		private void SaveButton_BarItemClick(object? sender, ItemClickEventArgs e)
        {
            this.GraphControlButtonSaveIsClicked(e.Item);
        }

        private void SaveButton_ControlClick(object? sender, EventArgs e)
        {
            if (sender is Component component)
                this.GraphControlButtonSaveIsClicked(component);
        }

		private void RejectChangesButton_BarItemClick(object? sender, ItemClickEventArgs e)
		{
			this.GraphControlButtonRejectChangesIsClicked(e.Item);
		}

		private void RejectChangesButton_ControlClick(object? sender, EventArgs e)
		{
            if (sender is Component component)
                this.GraphControlButtonRejectChangesIsClicked(component);
		}

		private void RemoveButton_BarItemClick(object? sender, ItemClickEventArgs e)
        {
            this.GraphControlButtonRemoveIsClicked(e.Item);
        }

        private void RemoveButton_ControlClick(object? sender, EventArgs e)
        {
            if (sender is Component component)
                this.GraphControlButtonRemoveIsClicked(component);
        }

		private void SubButtonGoToGraph_GetItemData(object? sender, EventArgs e)
		{
			if (this.FocusedSimpleObject is not null && sender is BarSubItem barSubItem)
			{
				barSubItem.ItemLinks.Clear();

				foreach (var goToGraphItem in this.goToGraphBarButtonItemsByGraphKey)
				{
					GraphElement? graphElementToGo = this.FocusedSimpleObject?.GetGraphElement(goToGraphItem.Key);

					if (graphElementToGo != null)
					{
						barSubItem.ItemLinks.Add(goToGraphItem.Value);
						goToGraphItem.Value.Tag = graphElementToGo;
					}
				}
			}
		}

		private void SubButtonChangeTo_GetItemData(object? sender, EventArgs e)
		{
			if (this.FocusedSimpleObject is not null && sender is BarSubItem barSubItem)
			{
				ISimpleObjectModel simpleObjectModel = this.FocusedSimpleObject.GetModel();
				barSubItem.ItemLinks.Clear();
				
				foreach (var objectTypeModelItem in simpleObjectModel.ObjectSubTypes)
				{
					if (objectTypeModelItem.Key == this.FocusedSimpleObject.GetPropertyValue<int>(simpleObjectModel.ObjectSubTypePropertyModel))
						continue;
					
					BarButtonItem barButtonItem = new BarButtonItem();
					
                    barButtonItem.Caption = objectTypeModelItem.Value.Caption;
					barButtonItem.Tag = objectTypeModelItem.Key;
					barButtonItem.Glyph = ImageControl.SmallImageCollection.Images[objectTypeModelItem.Value.ImageName];
					barButtonItem.LargeGlyph = ImageControl.LargeImageCollection.Images[objectTypeModelItem.Value.ImageName];
					barButtonItem.ItemClick += SubButtonChangeToBarButtonItem_ItemClick;
					
					barSubItem.ItemLinks.Add(barButtonItem, objectTypeModelItem.Value.BeginGroup);
				}
			}
		}

		private void SubButtonChangeToBarButtonItem_ItemClick(object? sender, ItemClickEventArgs e)
		{
			if (this.FocusedSimpleObject != null)
				this.FocusedSimpleObject.SetPropertyValue(this.FocusedSimpleObject.GetModel().ObjectSubTypePropertyModel, Conversion.TryChangeType<int>(e.Item.Tag));
		}

		private void simpleRibbonModulePanel_ModuleSelected(object? sender, EventArgs e)
        {
            if (this.FocusedGraphElement != null)
                this.RefreshGraphElement(this.FocusedGraphElement);

            if (this.TreeList != null)
                this.TreeList.Focus();
        }

		//private void SetButtonGoToGraphEnableProperty(object focusedNode)
		//{
		//	if (this.SubButtonGoToGraph != null || this.SubButtonGoToGraph2 != null)
		//	{
		//		bool enabled = false;

		//		if (focusedNode != null)
		//		{
		//			GraphElement graphElement = this.GetGraphElement(focusedNode);

		//			if (graphElement != null && graphElement.SimpleObject != null && graphElement.SimpleObject.GraphElements.Count > 1)
		//				enabled = true;
		//		}

		//		if (this.SubButtonGoToGraph != null)
		//			this.SubButtonGoToGraph.Enabled = enabled;

		//		if (this.SubButtonGoToGraph2 != null)
		//			this.SubButtonGoToGraph2.Enabled = enabled;
		//	}
		//}

		private void goToButton_ItemClick(object? sender, ItemClickEventArgs e)
		{
			if (this.RibbonModulePanel != null && e.Item.Tag is GraphElement graphElement)
				this.RibbonModulePanel.RibbonForm?.GoToGraphElement(graphElement);
		}

		//private void SaveLastEditorState()
		//{
		//	this.lastActiveEditor = this.TreeList.ActiveEditor;

		//	if (this.lastActiveEditor != null)
		//	{
		//		if (this.lastActiveEditor is TextEdit)
		//		{
		//			this.lastActiveEditorSelectionStart = (this.lastActiveEditor as TextEdit).SelectionStart;
		//			this.lastActiveEditorSelectionLength = (this.lastActiveEditor as TextEdit).SelectionLength;
		//		}
		//		else if (this.lastActiveEditor is DateEdit)
		//		{
		//			this.lastActiveEditorSelectionStart = (this.lastActiveEditor as DateEdit).SelectionStart;
		//			this.lastActiveEditorSelectionLength = (this.lastActiveEditor as DateEdit).SelectionLength;
		//		}

		//		if (this.lastActiveEditor is PopupBaseEdit)
		//			this.lastActiveEditorIsPopupOpen = (this.lastActiveEditor as PopupBaseEdit).IsPopupOpen;

		//		this.GraphControlCloseEditor();
		//	}
		//}

		//private void RestoreLastEditorState()
		//{
		//	if (this.lastActiveEditor != null)
		//	{
		//		this.GraphControlShowEditor();

		//		if (this.lastActiveEditor is TextEdit)
		//		{
		//			(this.lastActiveEditor as TextEdit).SelectionStart = this.lastActiveEditorSelectionStart;
		//			(this.lastActiveEditor as TextEdit).SelectionLength = this.lastActiveEditorSelectionLength;
		//		}
		//		else if (this.lastActiveEditor is DateEdit)
		//		{
		//			(this.lastActiveEditor as DateEdit).SelectionStart = this.lastActiveEditorSelectionStart;
		//			(this.lastActiveEditor as DateEdit).SelectionLength = this.lastActiveEditorSelectionLength;
		//		}

		//		if (this.lastActiveEditor is PopupBaseEdit && this.lastActiveEditorIsPopupOpen)
		//			(this.lastActiveEditor as PopupBaseEdit).ShowPopup();
		//	}
		//}

        #endregion |   Private Methods   |
    }
}
