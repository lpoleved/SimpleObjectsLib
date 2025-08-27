using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using Simple.Controls.TreeList;
using Simple.Objects;
using DevExpress.XtraBars;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;

namespace Simple.Objects.Controls
{
    public class CollectionControllerDevExpress : SimpleObjectCollectionController
    {
        #region |   Private Members   |

        private IGraphControl? graphControl = null;
        private SimpleTreeList? treeList = null;
		private PopupMenu? popupMenu = null;
		private Component? buttonMoveUp = null;
		private BarItem? buttonMoveUpPopup = null;
		private Component? buttonMoveDown = null;
		private BarItem? buttonMoveDownPopup = null;
		private Component? buttonRemoveColumnSorting = null;
		private BarItem? buttonRemoveColumnSortingPopup = null;
		private Component? buttonSave = null;
		private BarItem? buttonSavePopup = null;
		private Component? buttonRemove = null;
		private BarItem? buttonRemovePopup = null;
		private XtraTabControl? tabControl = null;

        #endregion |   Private Members   |

        #region |   Constructors and Initialization   |

        public CollectionControllerDevExpress()
        {
        }

        public CollectionControllerDevExpress(IContainer container)
            : base(container)
        {
		}

        #endregion |   Constructors and Initialization   |

        #region |   Public Properties   |

		[Category("General"), DefaultValue(null)]
		public XtraTabControl? TabControl 
		{
			get => this.tabControl;
			set
			{
				this.tabControl = value;

				if (this.EditorBindings != null && this.EditorBindings.TabControl == null)
					this.EditorBindings.TabControl = this.tabControl;
			}
		}

		[Category("Controls"), DefaultValue(null)]
        public SimpleTreeList? TreeList
        {
            get => this.treeList;
            set
            {
                if (this.treeList != null)
                {
					//this.TreeList.BeforeFocusNode -= new BeforeFocusNodeEventHandler(treeList_BeforeFocusNode);
					//this.TreeList.FocusedNodeChanged -= new FocusedNodeChangedEventHandler(treeList_FocusedNodeChanged);
					//this.TreeList.CellValueChanging -= new CellValueChangedEventHandler(treeList_CellValueChanging);
					////this.treeList.ValidateNode -= new ValidateNodeEventHandler(treeList_ValidateNode);
					//this.TreeList.BeforeExpand -= new BeforeExpandEventHandler(TreeList_BeforeExpand);
					//this.TreeList.AfterExpand -= new NodeEventHandler(treeList_AfterExpand);
					//this.TreeList.BeforeCollapse -= new BeforeCollapseEventHandler(treeList_BeforeCollapse);
					//this.TreeList.AfterCollapse -= new NodeEventHandler(treeList_AfterCollapse);
					//this.TreeList.BeforeCheckNode -= new CheckNodeEventHandler(treeList_BeforeCheckNode);
					//this.TreeList.KeyUp -= new KeyEventHandler(treeList_KeyUp);
					//this.TreeList.KeyDown -= new KeyEventHandler(this.ControlOnKeyDown);
					this.treeList.MouseDown -= TreeList_MouseDown;
					//this.TreeList.BeforeDragNode -= new BeforeDragNodeEventHandler(treeList_BeforeDragNode);
					//this.TreeList.DragOver -= new DragEventHandler(treeList_DragOver);
					//this.TreeList.DragDrop -= new DragEventHandler(treeList_DragDrop);
					//this.TreeList.GiveFeedback -= new GiveFeedbackEventHandler(treeList_GiveFeedback);
				}

				this.treeList = value;

                if (this.GraphControl is GraphControlTreeListDevExpress graphControlTreeListDevExpress)
					graphControlTreeListDevExpress.TreeList = this.treeList;

                if (this.treeList != null)
                {
					//this.TreeList.BeforeFocusNode += new BeforeFocusNodeEventHandler(treeList_BeforeFocusNode);
					//this.TreeList.FocusedNodeChanged += new FocusedNodeChangedEventHandler(treeList_FocusedNodeChanged);
					//this.TreeList.CellValueChanging += new CellValueChangedEventHandler(treeList_CellValueChanging);
					////this.treeList.ValidateNode += new ValidateNodeEventHandler(treeList_ValidateNode);
					//this.TreeList.BeforeExpand += new BeforeExpandEventHandler(TreeList_BeforeExpand);
					//this.TreeList.AfterExpand += new NodeEventHandler(treeList_AfterExpand);
					//this.TreeList.BeforeCollapse += new BeforeCollapseEventHandler(treeList_BeforeCollapse);
					//this.TreeList.AfterCollapse += new NodeEventHandler(treeList_AfterCollapse);
					//this.TreeList.BeforeCheckNode += new CheckNodeEventHandler(treeList_BeforeCheckNode);
					//this.TreeList.KeyUp += new KeyEventHandler(treeList_KeyUp);
					//this.TreeList.KeyDown += new KeyEventHandler(this.ControlOnKeyDown);
					this.treeList.MouseDown += this.TreeList_MouseDown;
					//this.TreeList.BeforeDragNode += new BeforeDragNodeEventHandler(treeList_BeforeDragNode);
					//this.TreeList.DragOver += new DragEventHandler(treeList_DragOver);
					//this.TreeList.DragDrop += new DragEventHandler(treeList_DragDrop);
					//this.TreeList.GiveFeedback += new GiveFeedbackEventHandler(treeList_GiveFeedback);

					//this.ControlGraphControlIsInitializing();
					//this.treeList.CustomNodeImagesDraw = true;
					//this.treeList.UseExpandedImages = true;
				}
			}
        }

		private void TreeList_MouseDown(object? sender, MouseEventArgs e)
		{
			if (this.PopupMenu != null && e.Button == MouseButtons.Right && this.TreeList is not null)
			{
				TreeListHitInfo hitInfo = this.TreeList.CalcHitInfo(e.Location);

				if (hitInfo.HitInfoType != HitInfoType.Column && hitInfo.HitInfoType != HitInfoType.BehindColumn)
					this.PopupMenu.ShowPopup(Cursor.Position);
			}
		}

		[Category("Controls"), DefaultValue(null)]
        public PopupMenu? PopupMenu
        {
            get { return this.popupMenu; }
            set { this.popupMenu = value; }
        }

		[Category("Buttons"), DefaultValue(null)]
		public Component? ButtonMoveUp
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
		public BarItem? ButtonMoveUpPopup
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
		public Component? ButtonMoveDown
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
		public BarItem? ButtonMoveDownPopup
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
		public Component? ButtonRemoveColumnSorting
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
		public BarItem? ButtonRemoveColumnSortingPopup
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
		public Component? ButtonSave
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
		public BarItem? ButtonSavePopup
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
		public Component? ButtonRemove
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
		public BarItem? ButtonRemovePopup
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

        #endregion |   Public Properties   |

        #region |   Public Methods   |

		public void SetAddButtonPolicy(Type objectType, params Component[] buttons)
		{
			this.SetAddButtonPolicy(new AddButtonPolicy<SimpleObject>(objectType), buttons);
		}

		//public void SetAddButtonPolicy<T>(Action<T> objectAction, params Component[] buttons) where T : SimpleObject
		//{
		//	this.SetAddButtonPolicy(typeof(T), objectAction as Action<SimpleObject>, buttons);
		//}

		public void SetAddButtonPolicy(Type objectType, Action<SimpleObject> objectAction, params Component[] buttons)
		{
			this.SetAddButtonPolicy(new AddButtonPolicy<SimpleObject>(objectType, objectAction), buttons);
		}

		public void SetAddButtonPolicy(AddButtonPolicy<SimpleObject> addButtonPolicy, params Component[] buttons)
		{
			foreach (Component button in buttons)
			{
				if (button is BarItem barItem)
					barItem.ItemClick += new ItemClickEventHandler(AddButton_BarItemClick);
				else if (button is Control control)
					control.Click += new EventHandler(AddButton_ControlClick);
				else
					throw new ArgumentException("Button type " + button.GetType() + " is not supported.");

				addButtonPolicy.Buttons.Add(button);
			}

			this.AddButtonPolicyList.Add(addButtonPolicy);
		}

		public void RemoveAddButtonPolicy(Component button)
		{
			if (button is BarItem barItem)
				barItem.ItemClick -= new ItemClickEventHandler(AddButton_BarItemClick);
			else if (button is Control control)
				control.Click -= new EventHandler(AddButton_ControlClick);
			else
				throw new ArgumentException("Button type " + button.GetType() + " is not supported.");

			this.RemoveAddButtonPolicyByButton(button);
		}

		public void Focus()
		{
			if (this.TreeList != null)
				this.TreeList.Focus();
		}

		#endregion |   Public Methods   |

		#region |   Protected Methods   |

		protected override IGraphControl? GetGraphControl()
        {
            if (this.graphControl == null && this.TreeList is not null)
                this.graphControl = new GraphControlTreeListDevExpress(this);

            return this.graphControl;
        }

		protected override void OnDispose()
        {
            this.graphControl = null;

            if (this.treeList != null)
                this.treeList.Dispose();
            
			this.treeList = null;

            if (this.popupMenu != null)
                this.popupMenu.Dispose();
            this.popupMenu = null;

            base.OnDispose();
        }

        #endregion |   Protected Methods   |

        #region |   Private Methods   |

		private void AddButton_BarItemClick(object sender, ItemClickEventArgs e)
		{
			this.ControlTryAddNewObjectByButtonClick(e.Item, this);
		}

		private void AddButton_ControlClick(object? sender, EventArgs e)
		{
			if (sender is Component component)
				this.ControlTryAddNewObjectByButtonClick(component, requester: this);
		}

		private void MoveUpButton_BarItemClick(object sender, ItemClickEventArgs e)
		{
			this.ControlButtonMoveUpIsClicked(e.Item);
		}

		private void MoveUpButton_ControlClick(object? sender, EventArgs e)
		{
			if (sender is Component component)
				this.ControlButtonMoveUpIsClicked(component);
		}

		private void MoveDownButton_BarItemClick(object sender, ItemClickEventArgs e)
		{
			this.ControlButtonMoveDownIsClicked(e.Item);
		}

		private void MoveDownButton_ControlClick(object? sender, EventArgs e)
		{
			if (sender is Component component)
				this.ControlButtonMoveDownIsClicked(component);
		}

		private void RemoveColumnSortingButton_BarItemClick(object sender, ItemClickEventArgs e)
		{
			this.ControlButtonRemoveColumnSortingIsClicked(e.Item);
		}

		private void RemoveColumnSortingButton_ControlClick(object? sender, EventArgs e)
		{
			if (sender is Component component)
				this.ControlButtonRemoveColumnSortingIsClicked(component);
		}

		private void SaveButton_BarItemClick(object sender, ItemClickEventArgs e)
		{
			this.ControlButtonSaveIsClicked(e.Item);
		}

		private void SaveButton_ControlClick(object? sender, EventArgs e)
		{
			if (sender is Component component)
				this.ControlButtonSaveIsClicked(component);
		}

		private void RemoveButton_BarItemClick(object sender, ItemClickEventArgs e)
		{
			this.ControlButtonRemoveIsClicked(e.Item);
		}

		private void RemoveButton_ControlClick(object? sender, EventArgs e)
		{
			if (sender is Component component)
				this.ControlButtonRemoveIsClicked(component);
		}

		#endregion |   Private Methods   |
	}
}
