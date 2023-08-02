using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
//using Simple;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.ViewInfo;
using DevExpress.Utils.Design;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Controls;
using DevExpress.Utils.Serializing;
using DevExpress.LookAndFeel;

namespace Simple.Controls.TreeList
{
    [Designer(typeof(System.Windows.Forms.Design.ControlDesigner))]
    public class SimpleTreeList : DevExpress.XtraTreeList.TreeList
    {
        #region |   Private Members   |

        private bool validate = true;
        private ImageList checkBoxImageList = null;
        private bool customCheckBoxDraw = false;
        //private bool showExtraChecked = false;
        private bool customNodeImagesDraw = false;
        private bool useExpandedImages = false;
		private int nodeUpdate = 0;
        private TreeListNode oldNode = null;
        private Color focusedCellForeColor;
		private bool isEditing = false;
        //private bool selectEdititEditingDisabled = false;

        private SimpleTreeListOptions optionsSimple;
        //@ denotes exactly skin name

		private string[] skinNamesRequireFocusedNodeForeColorWhite = new string[]
		{
			"Visual Studio 2013 Dark", "Office 2016 Black", "The Bezier", "Coffee", "Liquid Sky", "London Liquid Sky", "Glass Oceans", "Stardust", "Xmas 2008 Blue", "Valentine", "McSkin",
			"Summer 2008", "Springtime", "High Contrast", "The Asphalt World", "Caramel", "Money Twins", "Lilian", "iMaginary", "Black", "Office 2007 Blue", "Office 2007 Black", "Office 2007 Silver",
			"Office 2007 Green", "Office 2007 Pink", "Blue", "Metropolis", "Metropolis Dark"
			//"DevExpress Dark Style"
			//"Visual Studio 2013 Dark", "Office 2016 Dark", "Office 2016 Black", "The Bezier", "Coffee", "Liquid Sky", "London Liquid Sky", "Glass Oceans"
			//"DevExpress Style", "VS2010"
			//"DevExpress Style", "VS2010", "Seven Classic", "Office 2010", "Office 2013", "Office 2013 Dark Gray", "Office 2013 Light Gray", "Visual Studio 2013 Blue", "Visual Studio 2013 Light",
			//"Office 2016 Colorful", "Pumpkin", "Dark Side", "Foggy", "Seven", "Sharp@", "Whiteprint", "Springtime", "The Asphalt World", "Caramel", "Metropolis", "Metropolis Dark"
		};

		private string[] skinNamesRequireFocusedNodeFontInversionWhenTreeListIsNotFocused = new string[] { "The Bezier", "Simple.Objects" };
		private string[] skinNamesRequireNodeForeColorWhiteOnViewMode = new string[] 
		{ 
			"DevExpress Dark Style", "Visual Studio 2013 Dark", "Office 2016 Black", "Office 2019 Black", "Pumpkin", "Dark Side", "High Contrast", "Sharp Plus", "Darkroom", "Blueprint", "Metropolis Dark"
		};


		#endregion |   Private Members   |

		#region |   Constructors and Initialization   |

		public SimpleTreeList()
        {
            //this.CreateCustomNode += new CreateCustomNodeEventHandler(NETreeList_CreateCustomNode);
            this.BeforeCheckNode += new CheckNodeEventHandler(SimpleTreeList_BeforeCheckNode);
            this.CustomDrawNodeCheckBox += new DevExpress.XtraTreeList.CustomDrawNodeCheckBoxEventHandler(SimpleTreeList_CustomDrawNodeCheckBox);
            this.CustomDrawNodeImages += new CustomDrawNodeImagesEventHandler(SimpleTreeList_CustomDrawNodeImages);
			this.CustomDrawNodeCell += this.SimpleTreeList_CustomDrawNodeCell;
			this.NodeCellStyle += this.SimpleTreeList_NodeCellStyle;
			this.FocusedNodeChanged += SimpleTreeList_FocusedNodeChanged;
			this.FocusedColumnChanged += SimpleTreeList_FocusedColumnChanged;

            this.AfterExpand += new NodeEventHandler(SimpleTreeList_AfterExpand);
            this.AfterCollapse += new NodeEventHandler(SimpleTreeList_AfterCollapse);

			this.SetCurrentStyleSkinNodeColorSheme();
			this.optionsSimple = new SimpleTreeListOptions(this);
            this.OptionsMenu.ShowExpandCollapseItems = false;
        }

        #endregion |   Constructors and Initialization   |

        #region |   Public Events & Delegates   |

        /// <summary>
        /// Allows you to assign check box images to nodes.
        /// </summary>
        [Description("Allows you to assign check box images to nodes.")]
        [Category("Nodes")]
        public event GetCheckStateImageEventHandler GetCheckBoxImage;

        /// <summary>
        /// Allows you to merge cells in a row.
        /// </summary>
        [Description("Allows you to merge cells in a row.")]
        [Category("Nodes")]
        public event MergeRowCellsEventHandler MergeRowCells;

        public delegate void MergeColumnCellsEventHandler(object sender, MergeColumnCellsEventArgs e);
        public delegate void MergeRowCellsEventHandler(object sender, MergeRowCellsEventArgs e);

        #endregion |   Public Events & Delegates   |

        #region |   Internal Properties   |

        internal bool DoesCurrentStyleSkinRequireFocusedNodeForeColorWhite { get; set; }
		internal bool DoesCurrentStyleSkinRequireFocusedNodeFontInversionWhenTreeListIsNotFocused { get; set; }
		internal bool DoesCurrentStyleSkinRequireFocusedNodeForeColorWhiteOnViewMode { get; set; }

        #endregion |   Internal Properties   |

        #region |   Public Properties   |

        /// <summary>
        /// Gets or sets the source of the images that indicate a node's check state.
        /// </summary>
        [Category("Extras"), DefaultValue(null)]
        [TypeConverter(typeof(ImageCollectionImagesConverter))]
        [Description("Gets or sets the source of the images that indicate a node's check state.")]
        public ImageList CheckBoxImageList
        {
            get { return this.checkBoxImageList; }
            set
            {
                this.checkBoxImageList = value;
                this.CustomCheckBoxDraw = value != null;
                this.OptionsView.ShowCheckBoxes = value != null;
            }
        }

        /// <summary>
        /// Gets or sets a value specifying if to custom draw node's checkbox. If set, than CheckBoxImageList is used for drawing
        /// </summary>
        [Category("Extras"), DefaultValue(false)]
        [Description("Gets or sets value if custom checkbox drawing is used.")]
        public bool CustomCheckBoxDraw
        {
            get { return this.customCheckBoxDraw; }
            set
            {
                if (this.customCheckBoxDraw != value)
                {
                    this.customCheckBoxDraw = value;
                    this.Invalidate();
                }
            }
        }

        //[Category("Appearance"), DefaultValue(false)]
        //public bool ShowExtraChecked
        //{
        //    get { return this.showExtraChecked; }
        //    set
        //    {
        //        if (this.showExtraChecked != value)
        //        {
        //            showExtraChecked = value;
        //            if (showExtraChecked)
        //                this.customCheckBoxDraw = true;

        //            this.Invalidate();
        //        }
        //    }
        //}

        /// <summary>
        /// Gets or sets a value specifying if to use custom draw for node's images. 
        /// If SelectedImageIndex not set, than is used ExpandedImagIndex if node is expanded, and if not then ImageIndex.
        /// </summary>
        [Category("Extras"), DefaultValue(false)]
        [Description("Gets or sets value if custom checkbox drawing is used.")]
        public bool CustomNodeImagesDraw
        {
            get { return this.customNodeImagesDraw; }
            set
            {
                if (customNodeImagesDraw != value)
                {
                    this.customNodeImagesDraw = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets value if ExpandedImageList should be used for drawing node's image. 
        /// If set, than ExpandedImageList is used for drawing expanded nodes that are not selected.
        /// </summary>
        [Category("Extras"), DefaultValue(false)]
        [Description("Gets or sets value if ExpandedImageList should be used for drawing node's image.")]
        public bool UseExpandedImages
        {
            get { return this.useExpandedImages; }
            set
            {
                if (this.useExpandedImages != value)
                {
                    this.useExpandedImages = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets an OptionsSimple class that controls some extra functional and visibility features.
        /// </summary>
        //[Category("Simple")]
        //[Category("Options"), Browsable(true)]
        //[Description("Gets the OptionsSimple class that control some extra features.")]
        //[XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]


        //        [XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue)]
        //[DevExpressXtraTreeListLocalizedDescription("TreeListOptionsView")]
        [Category("Options"), Browsable(true)]
        [Description("Gets an OptionsSimple class that controls some extra functionality and visibility features.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue)]
        public SimpleTreeListOptions OptionsSimple
        {
            get { return this.optionsSimple; }
            set { this.optionsSimple = value; }
        }

        //#if IncludeNodesEditorCode 

        /// <summary>
        /// Obtains the collection of nodes within the XtraTreeList.
        /// </summary>
        [Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(Simple.Controls.TreeList.Designer.NodesEditor), typeof(UITypeEditor))]
        [Browsable(true)]
        [Description("Obtains the collection of nodes within the XtraTreeList.")]
        public new SimpleTreeListNodes Nodes
        {
            get { return base.Nodes as SimpleTreeListNodes; }
        }

        [Description("Provides access to the tree list's display options."), Category("Options"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
                     XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue)]
        public new SimpleTreeListOptionsView OptionsView { get { return base.OptionsView as SimpleTreeListOptionsView; } }

        public new SimpleTreeListViewInfo ViewInfo { get { return (SimpleTreeListViewInfo)base.ViewInfo; } }


        #endregion |   Public Properties   |

        #region |   Public Methods   |

        ////
        //// Summary:
        ////     Appends a new node containing the specified values to the specified node's
        ////     child collection.
        ////
        //// Parameters:
        ////   nodeData:
        ////     An array of values or a System.Data.DataRow object, used to initialize the
        ////     created node's cells.
        ////
        ////   parentNodeId:
        ////     An integer value specifying the parent node's identifier.
        ////
        //// Returns:
        ////     A DevExpress.XtraTreeList.Nodes.TreeListNode object or descendant representing
        ////     the added node.
        //public new NETreeListNode AppendNode(object nodeData, int parentNodeId)
        //{
        //    return base.AppendNode(nodeData, parentNodeId) as NETreeListNode;
        //}

        ////
        //// Summary:
        ////     Adds a new DevExpress.XtraTreeList.Nodes.TreeListNode containing the specified
        ////     values to the XtraTreeList.
        ////
        //// Parameters:
        ////   nodeData:
        ////     An array of values or a System.Data.DataRow object, used to initialize the
        ////     created node's cells.
        ////
        ////   parentNode:
        ////     A parent node of the added one.
        ////
        //// Returns:
        ////     A DevExpress.XtraTreeList.Nodes.TreeListNode object representing the added
        ////     node.
        //public new NETreeListNode AppendNode(object nodeData, NETreeListNode parentNode)
        //{
        //        return base.AppendNode(nodeData, parentNode) as NETreeListNode;
        //}

        ////
        //// Summary:
        ////     Appends a new node with the specified settings.
        ////
        //// Parameters:
        ////   nodeData:
        ////     An array of values or a System.Data.DataRow object, used to initialize the
        ////     created node's cells.
        ////
        ////   parentNodeId:
        ////     An integer value that identifies the parent node.
        ////
        ////   checkState:
        ////     The node's check state.
        ////
        //// Returns:
        ////     A DevExpress.XtraTreeList.Nodes.TreeListNode object that represents the added
        ////     node.
        //public new NETreeListNode AppendNode(object nodeData, int parentNodeId, CheckState checkState)
        //{
        //    return base.AppendNode(nodeData, parentNodeId, checkState) as NETreeListNode;
        //}

        ////
        //// Summary:
        ////     Adds a DevExpress.XtraTreeList.Nodes.TreeListNode containing the specified
        ////     values to the XtraTreeList.
        ////
        //// Parameters:
        ////   nodeData:
        ////     An array of values or a System.Data.DataRow object, used to initialize the
        ////     created node's cells.
        ////
        ////   parentNodeId:
        ////     An integer value specifying the identifier of the parent node.
        ////
        ////   tag:
        ////     An object that contains information associated with the tree list node. This
        ////     value is assigned to the DevExpress.XtraTreeList.Nodes.TreeListNode.Tag property.
        ////
        //// Returns:
        ////     A DevExpress.XtraTreeList.Nodes.TreeListNode object or descendant representing
        ////     the added node.
        //public new NETreeListNode AppendNode(object nodeData, int parentNodeId, object tag)
        //{
        //    return base.AppendNode(nodeData, parentNodeId, tag) as NETreeListNode;
        //}

        ////
        //// Summary:
        ////     Appends a new node with the specified settings.
        ////
        //// Parameters:
        ////   nodeData:
        ////     An array of values or a System.Data.DataRow object, used to initialize the
        ////     created node's cells.
        ////
        ////   parentNode:
        ////     A DevExpress.XtraTreeList.Nodes.TreeListNode object that represents the parent
        ////     node.
        ////
        ////   checkState:
        ////     The node's check state.
        ////
        //// Returns:
        ////     A DevExpress.XtraTreeList.Nodes.TreeListNode object that represents the added
        ////     node.
        //public new NETreeListNode AppendNode(object nodeData, NETreeListNode parentNode, CheckState checkState)
        //{
        //    return base.AppendNode(nodeData, parentNode, checkState) as NETreeListNode;
        //}

        ////
        //// Summary:
        ////     Adds a DevExpress.XtraTreeList.Nodes.TreeListNode containing the specified
        ////     values to the XtraTreeList.
        ////
        //// Parameters:
        ////   nodeData:
        ////     An array of values or a System.Data.DataRow object, used to initialize the
        ////     created node's cells.
        ////
        ////   parentNode:
        ////     A parent node for the added one.
        ////
        ////   tag:
        ////     An object that contains information associated with the tree list node. This
        ////     value is assigned to the DevExpress.XtraTreeList.Nodes.TreeListNode.Tag property.
        ////
        //// Returns:
        ////     A DevExpress.XtraTreeList.Nodes.TreeListNode object or descendant representing
        ////     the added node.
        //public new NETreeListNode AppendNode(object nodeData, NETreeListNode parentNode, object tag)
        //{
        //    return base.AppendNode(nodeData, parentNode, tag) as NETreeListNode;
        //}

        ////
        //// Summary:
        ////     Appends a new node containing the specified values to the specified node's
        ////     child collection.
        ////
        //// Parameters:
        ////   nodeData:
        ////     An array of values or a System.Data.DataRow object, used to initialize the
        ////     created node's cells.
        ////
        ////   parentNodeId:
        ////     An integer value specifying the identifier of the parent node.
        ////
        ////   imageIndex:
        ////     An integer value specifying the image displayed within the node when it is
        ////     not focused. This value is assigned to the DevExpress.XtraTreeList.Nodes.TreeListNode.ImageIndex
        ////     property of the added node.
        ////
        ////   selectImageIndex:
        ////     An integer value specifying the image displayed within the node when it is
        ////     focused. This value is assigned to the DevExpress.XtraTreeList.Nodes.TreeListNode.SelectImageIndex
        ////     property of the added node.
        ////
        ////   stateImageIndex:
        ////     An integer value specifying the index of the state image displayed within
        ////     the node. This value is assigned to the DevExpress.XtraTreeList.Nodes.TreeListNode.StateImageIndex
        ////     property of the added node.
        ////
        //// Returns:
        ////     A DevExpress.XtraTreeList.Nodes.TreeListNode object or descendant representing
        ////     the added node.
        //public new NETreeListNode AppendNode(object nodeData, int parentNodeId, int imageIndex, int selectImageIndex, int stateImageIndex)
        //{
        //    return base.AppendNode(nodeData, parentNodeId, imageIndex, selectImageIndex, stateImageIndex) as NETreeListNode;
        //}

        ////
        //// Summary:
        ////     Appends a new node with the specified settings.
        ////
        //// Parameters:
        ////   nodeData:
        ////     An array of values or a System.Data.DataRow object, used to initialize the
        ////     created node's cells.
        ////
        ////   parentNodeId:
        ////     An integer value that identifies the parent node.
        ////
        ////   imageIndex:
        ////     A zero-based index of the image displayed within the node.
        ////
        ////   selectImageIndex:
        ////     A zero-based index of the image displayed within the node when it is focused
        ////     or selected.
        ////
        ////   stateImageIndex:
        ////     An integer value that specifies the index of the node's state image.
        ////
        ////   checkState:
        ////     The node's check state.
        ////
        //// Returns:
        ////     A DevExpress.XtraTreeList.Nodes.TreeListNode object that represents the added
        ////     node.
        //public new NETreeListNode AppendNode(object nodeData, int parentNodeId, int imageIndex, int selectImageIndex, int stateImageIndex, CheckState checkState)
        //{
        //    return base.AppendNode(nodeData, parentNodeId, imageIndex, selectImageIndex, stateImageIndex, checkState) as NETreeListNode;
        //}

        public SimpleTreeListNode AppendNode(object nodeData, int parentNodeId, int imageIndex, int selectImageIndex, int stateImageIndex, int expandedImageIndex) //, bool extraChecked)
        {
            SimpleTreeListNode newNode = base.AppendNode(nodeData, parentNodeId, imageIndex, selectImageIndex, stateImageIndex) as SimpleTreeListNode;
            newNode.ExpandedImageIndex = expandedImageIndex;
            //newNode.ExtraChecked = extraChecked;
            return newNode;
        }

        public SimpleTreeListNode AppendNode(object nodeData, int parentNodeId, int imageIndex, int selectImageIndex, int stateImageIndex, int expandedImageIndex, CheckState checkState)
        {
            SimpleTreeListNode newNode = base.AppendNode(nodeData, parentNodeId, imageIndex, selectImageIndex, stateImageIndex, checkState) as SimpleTreeListNode;
            newNode.ExpandedImageIndex = expandedImageIndex;
            //newNode.ExtraChecked = extraChecked;
            return newNode;
        }
		//#endif

		public override void BeginUpdate()
		{
            this.nodeUpdate++;
            base.BeginUpdate();
		}

		public override void EndUpdate()
		{
			base.EndUpdate();
            this.nodeUpdate--;
        }

        //public override void BeginUnboundLoad()
        //{
        //	base.BeginUnboundLoad();
        //}

        //public override void EndUnboundLoad()
        //{
        //	base.EndUnboundLoad();
        //          //this.RaiseFocusedNodeChangedIfRequire();
        //      }

        #endregion |   Public Methods   |

        #region |   Public Static Methods   |

        public static int AddColumn(DevExpress.XtraTreeList.TreeList treeList, string name)
        {
            return AddColumn(treeList, name, name);
        }

        public static int AddColumn(DevExpress.XtraTreeList.TreeList treeList, string name, string caption)
        {
            DevExpress.XtraTreeList.Columns.TreeListColumn column = treeList.Columns.Add();
            column.Name = name;
            column.FieldName = name;
            column.Caption = caption;
            column.VisibleIndex = treeList.Columns.Count - 1;
            column.Format.FormatType = DevExpress.Utils.FormatType.Custom;
            column.Format.FormatString = "\\";
            column.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;

            return column.AbsoluteIndex;
        }

        #endregion |   Public Static Methods   |

        #region |   Protected Method Overrides   |

        protected override TreeListNode CreateNode(int nodeID, TreeListNodes owner, object tag)
        {
            //NETreeListNode newNode = new NETreeListNode(nodeID, owner, tag);
            //newNode.ExpandedImageIndex = 9;
            //return newNode;
            SimpleTreeListNode node = new SimpleTreeListNode(nodeID, owner, tag);
            return node;
        }

        //#if IncludeNodesEditorCode 
        protected override TreeListNodes CreateNodes()
        {
            return new SimpleTreeListNodes(this);
        }

        protected override TreeListOptionsView CreateOptionsView()
        {
            return new SimpleTreeListOptionsView(this);
        }

        protected override TreeListViewInfo CreateViewInfo()
        {
            return new SimpleTreeListViewInfo(this);
        }

        protected override void RaiseFocusedNodeChanged(TreeListNode oldNode, TreeListNode newNode)
		{
            //if (this.nodeUpdate != 0)
            //	return;

            if (this.oldNode != newNode)
            {
                base.RaiseFocusedNodeChanged(oldNode, newNode);
                this.oldNode = newNode;
            }

            if (this.OptionsSimple.EditMode == SimpleTreeEditMode.SelectEdit)
                this.OptionsBehavior.Editable = (oldNode == null);

            if (this.FocusedRow != null)
            {
                this.FocusedRow.Appearance.ForeColor = this.Appearance.FocusedRow.ForeColor;

                if (this.OptionsSimple.EditMode == SimpleTreeEditMode.SelectEdit)
                {
                    this.OptionsSelection.EnableAppearanceFocusedCell = false;
                    //this.selectEdititEditingDisabled = true;
                }
                else
				{
                    //this.selectEdititEditingDisabled = false;
                }
            }

        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (this.OptionsSimple.EditMode == SimpleTreeEditMode.SelectEdit)
            {
				if (this.OptionsBehavior.Editable)
					this.OptionsSelection.EnableAppearanceFocusedCell = true;

                //this.OptionsBehavior.Editable = !this.selectEdititEditingDisabled; // first click -> no editing
                this.OptionsBehavior.Editable = true;


                //this.selectEdititEditingDisabled = false;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (this.OptionsSimple.EditMode == SimpleTreeEditMode.SelectEdit)
            {
				if (this.OptionsBehavior.Editable)
					this.OptionsSelection.EnableAppearanceFocusedCell = true;

				this.OptionsBehavior.Editable = true;
            }
        }

        protected override void RaiseShowingEditor(ref bool cancel)
        {
			this.isEditing = true;
			
            base.RaiseShowingEditor(ref cancel);

			if (this.OptionsSimple.EditMode == SimpleTreeEditMode.SelectEdit)
			{
                this.OptionsSelection.EnableAppearanceFocusedCell = true;

                if (this.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhite)
                {
                    this.focusedCellForeColor = this.Appearance.FocusedCell.ForeColor;
                    this.Appearance.FocusedCell.ForeColor = Color.Black;
                }
            }
        }

        protected override void RaiseHiddenEditor()
        {
			this.isEditing = false;

			if (this.OptionsSimple.EditMode == SimpleTreeEditMode.SelectEdit && this.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhite)
			{
				this.Appearance.FocusedCell.ForeColor = this.focusedCellForeColor; // Color.WhiteSmoke; // this.focusedCellForeColor;
                this.OptionsSelection.EnableAppearanceFocusedCell = false;

                if (this.FocusedRow != null)
                {
                    this.FocusedRow.Appearance.ForeColor = this.Appearance.FocusedRow.ForeColor;
                    //this.Appearance.FocusedCell.BackColor = this.FocusedRow.Appearance.BackColor;
                }
            }

			base.RaiseHiddenEditor();
		}

		#endregion |   Protected Method Overrides   |

		#region |   Protected Virtual Methods   |

		protected virtual void RaiseGetCheckStateImage(TreeListNode node, ref int nodeImageIndex)
        {
            if (this.customCheckBoxDraw)
            {
                if (this.GetCheckBoxImage != null)
                {
                    GetCheckStateImageEventArgs args = new GetCheckStateImageEventArgs(node, nodeImageIndex);
                    this.GetCheckBoxImage(this, args);

                    (node as SimpleTreeListNode).CheckBoxImageIndex = args.NodeImageIndex;
                    nodeImageIndex = args.NodeImageIndex;
                }
            }
            else
            {
                throw new InvalidOperationException("Can't raise GetCheckStateImage when CustomCheckBoxDraw is set to false.");
            }
        }


        private string activeSkinName = null;

		protected override void OnLookAndFeelChanged(LookAndFeelChangedEventArgs args)
		{
			base.OnLookAndFeelChanged(args);

            if (args.Reason == LookAndFeelChangeReason.StyleChanged && this.LookAndFeel.ActiveSkinName != this.activeSkinName)
            {
                this.activeSkinName = this.LookAndFeel.ActiveSkinName;
                this.SetCurrentStyleSkinNodeColorSheme();
                this.OptionsSimple.RefreshEditMode();
                this.OptionsSimple.RefreshLookAndFeel();
            }
        }

        #endregion |   Protected Virtual Methods   |

        #region |   Public & Protected Methods - Validation   |

        public void EnableValidation()
        {
            validate = true;
        }

        public void DisableValidation()
        {
            validate = false;
        }

        public void Validate()
        {
            this.EndEdit(false);
        }

        protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
        {
            if (validate)
            {
                base.OnValidating(e);
            }
            else
            {
                e.Cancel = false;
            }
        }

        protected override void OnValidated(EventArgs e)
        {
            base.OnValidated(e);
        }

        #endregion |   Public & Protected Methods - Validation   |

        #region |   Raise Event Methods   |

        protected internal void RaiseMergeRowCells(MergeRowCellsEventArgs args)
        {
            this.MergeRowCells?.Invoke(this, args);
        }

        #endregion |   Raise Event Methods   |

        #region |   Method Overrides   |

        public override void ShowEditor()
        {
            if (this.OptionsView.AllowHorizontalMerge)
            {
                RowInfo ri = this.FocusedRow;
                CellInfo cell = ri[this.FocusedColumn];

                if (cell == null)
                {
                    if (this.FocusedColumn.VisibleIndex == 0)
                        return;

                    this.FocusedColumn = this.VisibleColumns[this.FocusedColumn.VisibleIndex - 1];
                }
            }

            base.ShowEditor();
        }

        //public override bool CanShowEditor { get { return base.CanShowEditor; } }


        #endregion |   Method Overrides   |

        #region |   Private Methods   |

        private void SetCurrentStyleSkinNodeColorSheme()
		{
			this.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhite = this.skinNamesRequireFocusedNodeForeColorWhite.Contains(this.LookAndFeel.ActiveSkinName);
				//this.LookAndFeel.ActiveSkinName.ToLower().CompareWithAny(this.skinNamesRequireFocusedNodeForeColorWhite, trim: true, ignoreCase: true, comparer: this.StartsWithAnySpecialComparer);
			this.DoesCurrentStyleSkinRequireFocusedNodeFontInversionWhenTreeListIsNotFocused = this.skinNamesRequireFocusedNodeFontInversionWhenTreeListIsNotFocused.Contains(this.LookAndFeel.ActiveSkinName);
			this.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhiteOnViewMode = this.skinNamesRequireNodeForeColorWhiteOnViewMode.Contains(this.LookAndFeel.ActiveSkinName);
		}

		private bool StartsWithAnySpecialComparer(string x, string y)
        {
            if (y.Length > 1 && y.EndsWith("@"))
            {
                string newY = y.Substring(0, y.Length - 1);
                return newY == x;
            }
            else
            {
                return x.StartsWith(y);
            }
        }

		private void SimpleTreeList_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
		{
			if (e.Node == this.FocusedNode && !this.Focused && !this.isEditing)
				e.Appearance.ForeColor = this.Appearance.HideSelectionRow.ForeColor;


			//if (e.Node == this.FocusedNode) // && this.DoesCurrentStyleSkinRequireFocusedNodeFontInversionWhenTreeListIsNotFocused)
			//{
			//	if (this.Focused)
			//	{
			//		if (this.isEditing)
			//		{
			//			e.Appearance.ForeColor = this.Appearance.FocusedCell.ForeColor; // Color.WhiteSmoke;
			//		}
			//		else
			//		{
			//			e.Appearance.ForeColor = (this.DoesCurrentStyleSkinRequireFocusedNodeFontInversion) ? Color.Black : Color.WhiteSmoke; // this.Appearance.FocusedCell.ForeColor;
			//		}
			//	}
			//	else
			//	{
			//		e.Appearance.ForeColor = this.Appearance.HideSelectionRow.ForeColor;
			//	}

				//	//e.Appearance.ForeColor = (this.Focused) ? this.Appearance.FocusedRow.ForeColor : this.Appearance.HideSelectionRow.ForeColor;

				//	//if (this.Focused && this.DoesCurrentStyleSkinRequireFocusedNodeFontInversion && !this.isEditing)
				//	//	e.Appearance.ForeColor = Color.WhiteSmoke;
				//}
		}

		private void SimpleTreeList_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
		{
			//if (this.DoesCurrentStyleSkinRequireFocusedNodeFontInversionWhenTreeListIsNotFocused && e.Node == this.FocusedNode)
			//{
			//	e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
			//	Rectangle rect = new Rectangle(e.EditViewInfo.ContentRect.Left, e.EditViewInfo.ContentRect.Top, 
			//								   Convert.ToInt32(e.Graphics.MeasureString(e.CellText, this.Font).Width + 1),
			//								   Convert.ToInt32(e.Graphics.MeasureString(e.CellText, this.Font).Height));

			//	if (this.Focused)
			//		e.Graphics.FillRectangle(SystemBrushes.Highlight, rect);
			//	else
			//		e.Graphics.FillRectangle(SystemBrushes.InactiveCaption, rect);

			//	e.Graphics.DrawString(e.CellText, this.Font, SystemBrushes.HighlightText, rect);
			//	e.Handled = true;
			//}
		}

		private void SimpleTreeList_GotFocus(object sender, EventArgs e)
		{
			if (this.OptionsSimple.EditMode == SimpleTreeEditMode.SelectEdit && this.DoesCurrentStyleSkinRequireFocusedNodeFontInversionWhenTreeListIsNotFocused)
				this.OptionsSimple.RefreshEditMode();
		}

		private void SimpleTreeList_LostFocus(object sender, EventArgs e)
		{
			if (this.OptionsSimple.EditMode == SimpleTreeEditMode.SelectEdit)
			{
				if (this.DoesCurrentStyleSkinRequireFocusedNodeFontInversionWhenTreeListIsNotFocused)
				{
					Color foreColor = (this.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhite) ? Color.Black : Color.WhiteSmoke;

					this.Appearance.FocusedRow.ForeColor = foreColor;
					this.Appearance.FocusedCell.ForeColor = foreColor;
					this.Appearance.HideSelectionRow.ForeColor = foreColor;
				}

				//this.Appearance.HideSelectionRow.ForeColor = Color.Black;
			}
		}

        private void JumpIfInMergedCell(FocusedColumnChangedEventArgs e, int step)
        {
            TreeListColumn nextColumn = GetColumnByVisibleIndex(e.Column.VisibleIndex - 1);

            if (Equals(this.FocusedNode.GetValue(e.Column), this.FocusedNode.GetValue(nextColumn)))
                this.FocusedColumn = this.GetColumnByVisibleIndex(FocusedColumn.VisibleIndex + step);
        }

        private void MovementUpDownInMergeColumn(FocusedNodeChangedEventArgs e)
        {
            if (e.OldNode != null && (this.FocusedColumn.VisibleIndex - 1) > 0)
                if (Equals(e.Node.GetValue(this.GetColumnByVisibleIndex(this.FocusedColumn.VisibleIndex)), e.Node.GetValue(this.GetColumnByVisibleIndex(this.FocusedColumn.VisibleIndex - 1))))
                    this.FocusedColumn = this.GetColumnByVisibleIndex(FocusedColumn.VisibleIndex - 1);
        }

        private void ReturnIfBeyondRightBorder(FocusedColumnChangedEventArgs e)
        {
            //if (this.FocusedNode.GetValue(e.Column).ToString() == this.FocusedNode.GetValue(e.OldColumn).ToString() && this.Columns[e.Column.VisibleIndex + 1] == null)
            if (Equals(this.FocusedNode.GetValue(e.Column), this.FocusedNode.GetValue(e.OldColumn)) && this.Columns[e.Column.VisibleIndex + 1] == null)
                this.FocusedColumn = e.OldColumn;
        }

        #endregion |   Private Methods   |

        #region |   Private Events Implementation   |

        //private void SimpleTreeList_CreateCustomNode(object sender, CreateCustomNodeEventArgs e)
        //{
        //    e.Node = new NETreeListNode(e.NodeID, e.Owner);
        //}

        private void SimpleTreeList_BeforeCheckNode(object sender, CheckNodeEventArgs e)
        {
            SimpleTreeListNode node = e.Node as SimpleTreeListNode;
            
            e.CanCheck = node.Enabled;
        }

        private void SimpleTreeList_CustomDrawNodeCheckBox(object sender, DevExpress.XtraTreeList.CustomDrawNodeCheckBoxEventArgs e)
        {
            if (this.customCheckBoxDraw)
            {
                SimpleTreeListNode node = e.Node as SimpleTreeListNode;
                int nodeImageIndex = node.CheckBoxImageIndex;

                if (this.GetCheckBoxImage != null)
                {
                    RaiseGetCheckStateImage(e.Node, ref nodeImageIndex);
                }
                else
                {
                    //// TODO: Dodati image za AllowIndeterminateCheckState i u skladu s tim izabrati sliku
                    ////if ((sender as NETreeList).OptionsBehavior.AllowIndeterminateCheckState)
                    //if (this.showExtraChecked)
                    //{
                    //    nodeImageIndex = (node.Checked ? 4 : 2) + (node.ExtraChecked ? 1 : 0);
                    //}
                    //else
                    //{
                    //    nodeImageIndex = node.Checked ? 1 : 0;
                    //}
                    node.CheckBoxImageIndex = nodeImageIndex;
                }

                if (nodeImageIndex >= 0 && checkBoxImageList.Images.Count > nodeImageIndex)
                {
                    if (e.ObjectArgs.State == ObjectState.Disabled || !node.Enabled)
                    {
                        ControlPaint.DrawImageDisabled(e.Graphics, this.checkBoxImageList.Images[nodeImageIndex], e.Bounds.Location.X, e.Bounds.Location.Y, this.BackColor);
                    }
                    else
                    {
                        e.Graphics.DrawImage(this.checkBoxImageList.Images[nodeImageIndex], e.Bounds.Location.X - 3, e.Bounds.Y + 1);
                    }

                    e.Handled = true;
                }
            }
        }

        private void SimpleTreeList_CustomDrawNodeImages(object sender, CustomDrawNodeImagesEventArgs e)
        {
            if (!this.customNodeImagesDraw || this.SelectImageList == null)
                return;

            SimpleTreeListNode node = e.Node as SimpleTreeListNode;

            int imageIndex = -1;
            
            if (node.Selected && node.SelectImageIndex > -1)
            {
                imageIndex = node.SelectImageIndex;
            }
            else if (this.UseExpandedImages && node.Expanded && node.ExpandedImageIndex > -1)
            {
                imageIndex = node.ExpandedImageIndex;
            }
            else
            {
                imageIndex = node.ImageIndex;
            }

            bool handled = false;
            ImageList imageList = this.SelectImageList as ImageList;

            if (imageIndex > -1 && imageList.Images.Count > imageIndex) //Count check because sharedimagelist does not deserialize correct in inherited controls in designer
            {
                e.Graphics.DrawImage(imageList.Images[imageIndex], e.SelectRect.Location.X, e.SelectRect.Location.Y);
                handled = true;
            }

            if (e.StateImageIndex > -1 && this.StateImageList != null)
            {
                ImageList stateImageList = this.StateImageList as ImageList;
                
                if (stateImageList.Images.Count > e.StateImageIndex) //Count check because sharedimagelist does not deserialize correct in inherited controls in designer
                {
                    e.Graphics.DrawImage(stateImageList.Images[e.StateImageIndex], e.StateImageLocation);
                    handled = true;
                }
            }

            e.Handled = handled;
        }

        private void SimpleTreeList_AfterCollapse(object sender, NodeEventArgs e)
        {
            this.InvalidateNode(e.Node);
        }

        private void SimpleTreeList_AfterExpand(object sender, NodeEventArgs e)
        {
            this.InvalidateNode(e.Node);
        }

        private void SimpleTreeList_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            if (!this.OptionsView.AllowHorizontalMerge)
                return;
            
            if (e.OldColumn == null || e.Column == null) return;
                this.ReturnIfBeyondRightBorder(e);

            if (e.OldColumn.VisibleIndex < e.Column.VisibleIndex && this.Columns[e.Column.VisibleIndex + 1] != null)
                this.JumpIfInMergedCell(e, 1);
            
            if (e.OldColumn.VisibleIndex > e.Column.VisibleIndex && this.Columns[e.Column.VisibleIndex - 1] != null)
                this.JumpIfInMergedCell(e, -1);
        }

        private void SimpleTreeList_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (this.OptionsView.AllowHorizontalMerge)
                this.MovementUpDownInMergeColumn(e);

            this.CloseEditor();
        }

        #endregion |   Private Events Implementation   |
    }
}
