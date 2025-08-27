using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.Versioning;
using System.Drawing;
using Simple;
using Simple.Modeling;
using Simple.Controls;
using Simple.Collections;
using DevExpress.XtraEditors;
using System.Reflection.Metadata.Ecma335;

namespace Simple.Objects.Controls
{
    // TODO: Check why some events are SimpleObject based?. Generic graph controller should be independent of SimpleObjects. It is generic object graph controller
    // TODO: Than chenge all BindingObject events to generic BindingObject arguments to not depend on SimpleObject
 
    public abstract class GenericGraphControllerBase<TGraphElement> : Component, IGraphController, IChangableBindingObjectControl, ISimpleValidation, IComponent, IDisposable
        where TGraphElement : IPropertyValue
    {
        #region |   Private Members   |

        private const string strExpanded = "Expanded";
        //private const int defaultGraphKey = 0;
        //private const int defaultManyToManyRelationKey = 0;
        //private static object lockObject = new object();

        private IContainer components;
        //private SimpleObjectManager manager = null;
        //private Control graphControl = null;
        private EditorBindingsControl editorBindings;
        private SimpleDictionary<TGraphElement, object> nodesByGraphElement = new SimpleDictionary<TGraphElement, object>();
        private List<TGraphElement> rootGraphElements = new List<TGraphElement>();
        private GraphColumnCollection columns;
        private TGraphElement? anchorGraphElement = default(TGraphElement);
        private IChangableBindingObjectControl? changableBindingObjectControl = null;
        private SimpleObject? changableBindingObjectControlFocusedBindingObject = null;
        //private object lastValidationNode = null;
        private object? lastValidationFailedNode = null;
        private GraphValidationResult lastValidationNodeResult = GraphValidationResult.DefaultSuccessResult;
        private GraphEditMode editMode = GraphEditMode.SelectEdit;
        private GraphLookAndFeelStyle lookAndFeelStyle = GraphLookAndFeelStyle.ExplorerStyle;
        private ImageList? imageList = null;
        private ImageList? stateImageList = null;
        private ImageList? checkBoxImageList = null;
		private bool isImagesLoaded = false;
        private bool isStateImagesLoaded = false;
        private bool isCheckBoxImagesLoaded = false;
        private int checkBoxCheckedImageIndex = -1;
        private int checkBoxCheckedChildCheckedImageIndex = -1;
        private int checkBoxUncheckedImageIndex = -1;
        private int checkBoxUncheckedChildCheckedImageIndex = -1;
        private int checkBoxCheckedGreyedImageIndex = -1;
        private int checkBoxCheckedChildCheckedGreyedImageIndex = -1;
        private int checkBoxUncheckedGreyedImageIndex = -1;
        private int checkBoxUncheckedChildCheckedGreyedImageIndex = -1;
        private bool showCheckBoxes = false;
        private bool allowRecursiveNodeChecking = false;
        //private bool isImagesLoaded = false;
        //private bool isStateImagesLoaded = false;
        private bool isLoadingInProgress = false;
		private List<object>? checkedNodes = null;
        private SimpleDictionary<Type, HashArray<GraphColumnBindingPolicy<TGraphElement>>> columnBindingPolicyByColumnIndexByObjectType = new SimpleDictionary<Type, HashArray<GraphColumnBindingPolicy<TGraphElement>>>();
        private SimpleDictionary<Type, HashArray<GraphColumnBindingPolicy<TGraphElement>>> columnBindingPolicyByPropertyIndexByObjectType = new SimpleDictionary<Type, HashArray<GraphColumnBindingPolicy<TGraphElement>>>();
        private List<string>? columnNamesWithDisabledBinding = null;
        //private Dictionary<Component, AddButtonPolicy> buttonAddObjectPolicyDictionaryByComponent = new Dictionary<Component, AddButtonPolicy>();
        private List<AddButtonPolicy<TGraphElement>> addButtonPolicyList = new List<AddButtonPolicy<TGraphElement>>();
        //private List<Component> buttonAddFolderList = new List<Component>();
        //private List<Component> buttonAddSubFolderList = new List<Component>();
        private List<Component> buttonAddObjectList = new List<Component>();
        private List<Component> buttonMoveUpList = new List<Component>();
        private List<Component> buttonMoveDownList = new List<Component>();
        private List<Component> buttonRemoveColumnSortingList = new List<Component>();
        private List<Component> buttonSaveList = new List<Component>();
        private List<Component> buttonRemoveList = new List<Component>();
        private bool canDragAndDrop = false;
        private bool refreshGraphNodeOnEveryBindigObjectPropertyValueChange = true;
        private bool isInitialized = false;
        private bool isBinded = false;
        private bool isDisposed = false;

        #endregion |   Private Members   |

        #region |   Constructors and Initialization   |

        public GenericGraphControllerBase()
        {
            this.components = new Container();

			//this.GraphKey = defaultGraphKey;
			//this.ManyToManyRelationKey = defaultManyToManyRelationKey;
			//this.IsActive = true;
            //this.EditMode = GraphEditMode.SelectEdit;
            //this.LookAndFeelStyle = GraphLookAndFeelStyle.ExplorerStyle;
            //this.CanDragAndDrop = this.canDragAndDrop;
            //this.LoadAllNodes = false;
            //this.CommitChangeOnFocusedNodeChange = true;
            //this.CommitChangeOnDeleteRequest = true;
            //this.BindOnlyToSpecifiedGraphKey = true;

            this.columns = new GraphColumnCollection(this);
            this.editorBindings = new EditorBindingsControl();
            this.editorBindings.ChangableBindingObjectControl = this;

			//this.ImageList = ImageControl.SmallImageCollection;
			//this.StateImageList = ImageControl.StateImageCollection;
			//this.CheckBoxImageList = ImageControl.CheckBoxImageCollection;

			//SimpleObjectManager manager = null;

			//try
			//{
			//    manager = SimpleObjectManager.Instance;
			//}
			//catch
			//{
			//    manager = null;
			//}
			//finally
			//{
			//    this.Manager = manager;
			//}

			this.OnInitializeComponent();

            GraphManager.SubscribeMe(this);
        }

        ~GenericGraphControllerBase()
        {
            GraphManager.UnsubscribeMe(this);
        }

        public GenericGraphControllerBase(IContainer container)
            : this()
        {
            container.Add(this);
        }

        #endregion |   Constructors and Initialization   |

        #region |   Events   |

        public event BindingObjectEventHandler? BindingObjectChange;
		//public event ChangePropertyValueRequesterBindingObjectEventHandler BindingObjectPropertyValueChange;
		//public event IconNameChangeBindingObjectEventHandler BindingObjectIconNameChange;
		//public event ValidationResultBindingObjectEventHandler BindingObjectOnValidation;
		//public event BindingObjectEventHandler BindingObjectStoreData;
		//public event BindingObjectEventHandler BindingObjectRefreshContext;
		public event EventHandler? AfterInitializeControl;

		public event GraphElementEventHandler<TGraphElement>? GraphElementCreated;
        public event GraphElementEventHandler<TGraphElement>? BeforeDeleteGraphElement;
        //public event GraphElementChangePropertyValueEventHandler<TGraphElement> BindingObjectPropertyValueChange;
        //public event ChangeParentGraphElementEventHandler<TGraphElement> ParentGraphElementChange;
        public event CellEditValueGraphElementEventHandler<TGraphElement>? GetCustomCellEditValue;
        public event ReloadGraphElementNodeEventHandler<TGraphElement>? ReloadNode;
        public event GraphElementNodeEventHandler<TGraphElement>? AfterNodeReload;
        public event BeforeCheckGraphElementEventHandler<TGraphElement>? BeforeCheckGraphElement;
		public event CheckGraphElementEventHandler<TGraphElement>? AfterCheckGraphElement;

		public event EventHandler? AfterBestFitGraphColumns;
        public event GraphElementEventHandler<TGraphElement>? AfterSetGraphColumnsEnableProperty;
        public event ComponentEventHandler? OnSetButtonEnableProperty;

        public event ChangePropertyValueBindingObjectRequesterEventHandler? BindingObjectPropertyValueChange;
        public event ChangePropertyValueBindingObjectRequesterEventHandler? BindingObjectRefreshContext; // This is request for refresh context by the any other object property value change or action that require context refresh.
        public event BindingObjectEventHandler? BindingObjectPushData;
        public event GraphValidationResultEventHandler? BindingObjectOnValidation;
        public event OldParentGraphElemenChangeContainertContextRequesterEventHandler? BindingObjectParentGraphElementChange;
        public event BindingObjectRelationForeignObjectSetEventHandler? BindingObjectRelationForeignObjectSet;

        #endregion |   Events   |

        #region |   Public Properties   |

        [Category("General"), DefaultValue(true)]
        public bool IsActive { get; set; } = true;

        [Category("General"), Browsable(true)]
        public IChangableBindingObjectControl? ChangableBindingObjectControl
        {
            get { return this.changableBindingObjectControl; }
            set
            {
                if (this.changableBindingObjectControl != null)
                {
                    this.changableBindingObjectControl.BindingObjectChange -= new BindingObjectEventHandler(changableBindingObjectControl_BindingObjectChange);
                    this.changableBindingObjectControl.BindingObjectRelationForeignObjectSet -= new BindingObjectRelationForeignObjectSetEventHandler(changableBindingObjectControl_BindingObjectRelationForeignObjectSet);
                    this.changableBindingObjectControl.BindingObjectRefreshContext -= new ChangePropertyValueBindingObjectRequesterEventHandler(changableBindingObjectControl_BindingObjectRefreshContext);
                }

                this.changableBindingObjectControl = value;

                if (this.changableBindingObjectControl != null)
                {
                    this.changableBindingObjectControl.BindingObjectChange += new BindingObjectEventHandler(changableBindingObjectControl_BindingObjectChange);
                    this.changableBindingObjectControl.BindingObjectRelationForeignObjectSet += new BindingObjectRelationForeignObjectSetEventHandler(changableBindingObjectControl_BindingObjectRelationForeignObjectSet);
                    this.changableBindingObjectControl.BindingObjectRefreshContext += new ChangePropertyValueBindingObjectRequesterEventHandler(changableBindingObjectControl_BindingObjectRefreshContext);
                }

                //this.editorBindings.ChangableBindingObjectControl = value;

                this.OnSetChangableBindingObjectControl();
            }
        }

        [Category("General"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SimpleObject? ChangableBindingObjectControlFocusedBindingObject
        { 
            get { return this.changableBindingObjectControlFocusedBindingObject; }
        }

        [Category("General"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EditorBindingsControl EditorBindings
        {
            get { return this.editorBindings; }
        }

        [Category("Appearancee"), DefaultValue(GraphEditMode.SelectEdit)]
        public GraphEditMode EditMode
        {
            get { return this.editMode; }
            set
            {
                this.editMode = value;
                this.GraphControl?.SetEditMode(this.editMode);
            }
        }

        [Category("Appearancee"), DefaultValue(GraphLookAndFeelStyle.ExplorerStyle)]
        public GraphLookAndFeelStyle LookAndFeelStyle
        {
            get { return this.lookAndFeelStyle; }
            set
            {
                this.lookAndFeelStyle = value;
                this.GraphControl?.SetLookAndFeelStyle(this.lookAndFeelStyle);
            }
        }

        [Category("Images"), DefaultValue(null)]
        public ImageList? ImageList
        {
            get { return this.imageList; }
            set
            {
                //if (value != this.imageList)
                //{
                //    this.isImagesLoaded = false;
                //}

                this.imageList = value;

				//if (this.imageList != null && this.imageList.Images.Count == 0)
				//	this.imageList = ImageControl.SmallImageCollection;

                if (this.imageList is not null)
				    this.GraphControl?.SetImageList(this.imageList);
            }
        }

        [Category("Images"), DefaultValue(null)]
        public ImageList? StateImageList
        {
            get { return this.stateImageList; }
            set
            {
                //if (value != this.stateImageList)
                //{
                //    this.isStateImagesLoaded = false;
                //}

                this.stateImageList = value;

                //if (this.stateImageList != null && this.stateImageList.Images.Count == 0)
                //	this.stateImageList = ImageControl.StateImageCollection;

                if (this.stateImageList is not null)
                    this.GraphControl?.SetStateImageList(this.stateImageList);
            }
        }

        [Category("Images"), DefaultValue(null)]
        public ImageList? CheckBoxImageList
        {
            get { return this.checkBoxImageList; }
            set
            {
                //if (value != this.checkBoxImageList)
                //{
                //    this.isCheckBoxImagesLoaded = false;
                //}

                this.checkBoxImageList = value;

				//if (this.checkBoxImageList != null && this.checkBoxImageList.Images.Count > 0)
				//	this.checkBoxImageList = ImageControl.CheckBoxImageCollection;

                if (this.checkBoxImageList is not null)
                    this.GraphControl?.SetCheckBoxImageList(this.checkBoxImageList);
                
                this.ShowCheckBoxes = (value != null);

                this.checkBoxCheckedImageIndex = -1;
                this.checkBoxUncheckedImageIndex = -1;
                this.checkBoxCheckedChildCheckedImageIndex = -1;
                this.checkBoxUncheckedChildCheckedImageIndex = -1;

                this.checkBoxCheckedGreyedImageIndex = -1;
                this.checkBoxUncheckedGreyedImageIndex = -1;
                this.checkBoxCheckedChildCheckedGreyedImageIndex = -1;
                this.checkBoxUncheckedChildCheckedGreyedImageIndex = -1;
            }
        }

        [Category("Options"), Browsable(true), DefaultValue(false)]
        [Description("Gets or sets whether child nodes are automatically checked/unchecked when a parent node is checked/unchecked and vice versa.")]
        public bool AllowRecursiveNodeChecking
        {
            get { return this.allowRecursiveNodeChecking; }
            set 
            { 
                this.allowRecursiveNodeChecking = value;
                this.GraphControl?.SetRecursiveNodeChecking(value);
            }
        }

        [Category("Graph"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GraphColumnCollection Columns
        {
            get { return this.columns; }
        }

        [Category("General"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowCheckBoxes
        {
            get { return this.showCheckBoxes; }
            set
            {
                this.showCheckBoxes = value;
                this.GraphControl?.SetCheckBoxMode(this.showCheckBoxes);
            }
        }

        //[Category("Simple"), Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public Graph Graph
        //{
        //    get { return this.Manager.GetGraph(this.GraphKey); }
        //}

        [Category("Simple"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object? FocusedNode
        {
            get { return this.GraphControl?.GetFocusedNode(); }
        }

        [Category("Simple"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TGraphElement? FocusedGraphElement
        {
            get 
            { 
                if (this.FocusedNode is not null)
                    return this.GetGraphElement(this.FocusedNode);

                return default;
            }
        }

        [Category("Simple"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TGraphElement? AnchorGraphElement
        {
            get { return this.anchorGraphElement; }
            set 
            {
                if (this.anchorGraphElement is GraphElement graphElement)
                    graphElement.IsAnchor = false;

                this.anchorGraphElement = value;  

                if (this.anchorGraphElement is GraphElement graphElement2)
                    graphElement2.IsAnchor = true;
            }
        }

        [Category("General"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<string> ColumnNamesWithDisabledBinding
        {
            get
            {
                if (this.columnNamesWithDisabledBinding == null)
                    this.columnNamesWithDisabledBinding = new List<string>();

                return this.columnNamesWithDisabledBinding;
            }
        }

        [Category("General"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<TGraphElement> RootGraphElements
        {
            get { return this.rootGraphElements; }
        }


        [Category("Graph"), DefaultValue(false)]
        public bool CanDragAndDrop
        {
            get { return this.canDragAndDrop; }
            set
            {
                this.canDragAndDrop = value;
                this.GraphControl?.SetCanDragAndDrop(value);
            }
        }

        [Category("Simple"), DefaultValue(true)]
        public bool RefreshGraphNodeOnEveryBindingObjectPropertyValueChange
        {
            get { return this.refreshGraphNodeOnEveryBindigObjectPropertyValueChange; }
            set { this.refreshGraphNodeOnEveryBindigObjectPropertyValueChange = value; }
        }

        [Category("Simple"), DefaultValue(false)]
        public bool LoadAllNodes { get; set; }

        [Category("Simple"), DefaultValue(true)]
        public bool CommitChangeOnFocusedNodeChange { get; set; } = true;

        [Category("Simple"), DefaultValue(true)]
        public bool CommitChangeOnDeleteRequest { get; set; } = true;

        [Category("Simple"), DefaultValue(SaveButtonOption.CommitChanges)]
        public SaveButtonOption SaveButonOption { get; set; } = SaveButtonOption.CommitChanges;

        #endregion |   Public Properties   |

        #region |   Protected Properties   |

        protected bool isGraphErrorMessageSet = false;

        protected IGraphControl? GraphControl
        {
            get { return this.GetGraphControl(); }
        }

        protected bool IsLoadingInProgress
        {
            get { return this.isLoadingInProgress; }
        }

        protected bool IsBinded
        {
            get { return this.isBinded; }
        }

        protected bool IsDisposed
        {
            get { return this.isDisposed; }
        }

        //protected List<Component> ButtonAddFolderList
        //{
        //    get { return this.buttonAddFolderList; }
        //}

        //protected List<Component> ButtonAddSubFolderList
        //{
        //    get { return this.buttonAddSubFolderList; }
        //}

        protected List<Component> ButtonAddObjectList
        {
            get { return this.buttonAddObjectList; }
        }

        protected List<Component> ButtonMoveUpList
        {
            get { return this.buttonMoveUpList; }
        }

        protected List<Component> ButtonMoveDownList
        {
            get { return this.buttonMoveDownList; }
        }

        protected List<Component> ButtonRemoveColumnSortingList
        {
            get { return this.buttonRemoveColumnSortingList; }
        }

        protected List<Component> ButtonSaveList
        {
            get { return this.buttonSaveList; }
        }

        protected List<Component> ButtonRemoveList
        {
            get { return this.buttonRemoveList; }
        }

        protected List<AddButtonPolicy<TGraphElement>> AddButtonPolicyList
        {
            get { return this.addButtonPolicyList; }
        }

        #endregion |   Protected Properties   |

        #region |   Private Properties   |

        private int CheckBoxCheckedImageIndex
        {
            get
            {
                if (this.checkBoxCheckedImageIndex == -1 && this.CheckBoxImageList != null)
                    this.checkBoxCheckedImageIndex = this.CheckBoxImageList.Images.IndexOfKey(ImageControl.ImageNameCheckBoxCkecked);

                return this.checkBoxCheckedImageIndex;
            }
        }

        private int CheckBoxCheckedChildCheckedImageIndex
        {
            get
            {
                if (this.checkBoxCheckedChildCheckedImageIndex == -1 && this.CheckBoxImageList != null)
                    this.checkBoxCheckedChildCheckedImageIndex = this.CheckBoxImageList.Images.IndexOfKey(ImageControl.ImageNameCheckBoxCheckedChildChecked);

                return this.checkBoxCheckedChildCheckedImageIndex;
            }
        }

        private int CheckBoxUncheckedImageIndex
        {
            get
            {
                if (this.checkBoxUncheckedImageIndex == -1 && this.CheckBoxImageList != null)
                    this.checkBoxUncheckedImageIndex = this.CheckBoxImageList.Images.IndexOfKey(ImageControl.ImageNameCheckBoxUnckecked);

                return this.checkBoxUncheckedImageIndex;
            }
        }

        private int CheckBoxUncheckedChildCheckedImageIndex
        {
            get
            {
                if (this.checkBoxUncheckedChildCheckedImageIndex == -1 && this.CheckBoxImageList != null)
                    this.checkBoxUncheckedChildCheckedImageIndex = this.CheckBoxImageList.Images.IndexOfKey(ImageControl.ImageNameCheckBoxUncheckedChildChecked);

                return this.checkBoxUncheckedChildCheckedImageIndex;
            }
        }

        private int CheckBoxCheckedGreyedImageIndex
        {
            get
            {
                if (this.checkBoxCheckedGreyedImageIndex == -1 && this.CheckBoxImageList != null)
                    this.checkBoxCheckedGreyedImageIndex = this.CheckBoxImageList.Images.IndexOfKey(ImageControl.ImageNameCheckBoxCkeckedGreyed);

                return this.checkBoxCheckedGreyedImageIndex;
            }
        }

        private int CheckBoxCheckedChildCheckedGreyedImageIndex
        {
            get
            {
                if (this.checkBoxCheckedChildCheckedGreyedImageIndex == -1 && this.CheckBoxImageList != null)
                    this.checkBoxCheckedChildCheckedGreyedImageIndex = this.CheckBoxImageList.Images.IndexOfKey(ImageControl.ImageNameCheckBoxCheckedChildCheckedGreyed);

                return this.checkBoxCheckedChildCheckedGreyedImageIndex;
            }
        }

        private int CheckBoxUncheckedGreyedImageIndex
        {
            get
            {
                if (this.checkBoxUncheckedGreyedImageIndex == -1 && this.CheckBoxImageList != null)
                    this.checkBoxUncheckedGreyedImageIndex = this.CheckBoxImageList.Images.IndexOfKey(ImageControl.ImageNameCheckBoxUnckeckedGreyed);

                return this.checkBoxUncheckedGreyedImageIndex;
            }
        }

        private int CheckBoxUncheckedChildCheckedGreyedImageIndex
        {
            get
            {
                if (this.checkBoxUncheckedChildCheckedGreyedImageIndex == -1 && this.CheckBoxImageList != null)
                    this.checkBoxUncheckedChildCheckedGreyedImageIndex = this.CheckBoxImageList.Images.IndexOfKey(ImageControl.ImageNameCheckBoxUncheckedChildCheckedGreyed);

                return this.checkBoxUncheckedChildCheckedGreyedImageIndex;
            }
        }

        private List<object> CheckedNodes
        {
            get
            {
                if (this.checkedNodes == null)
                    this.checkedNodes = new List<object>();

                return this.checkedNodes;
            }
        }

        #endregion |   Private Properties   |

        #region |   Public Methods   |

        public void Bind()
        {
            TGraphElement? graphElement = default(TGraphElement);

            this.Bind(graphElement);
        }

        public void Bind(TGraphElement? graphElement)
        {
            IEnumerable<TGraphElement> rootGraphElements;
                
            if (graphElement != null)
			{
                rootGraphElements = new TGraphElement[] { graphElement };
            }
            else if (this.anchorGraphElement != null)
            {
                rootGraphElements = this.GetChildGraphElements(this.anchorGraphElement);
            }
            else
            {
                rootGraphElements = new TGraphElement[] { };
            }

            this.Bind(rootGraphElements);
        }

        public void Bind(IEnumerable<TGraphElement> rootGraphElements)
        {
            if (!this.isInitialized)
                this.Initialize();

            this.SetImagesIfIsNotLoaded();

            if (rootGraphElements == null)
            {
                this.isBinded = true;
                
                return;
            }

            TGraphElement? parentGraphElement = default(TGraphElement);
            object? oldFocusedNode = this.GraphControl?.GetFocusedNode();

            if (this.anchorGraphElement != null && this.nodesByGraphElement.ContainsKey(this.anchorGraphElement))
                parentGraphElement = anchorGraphElement;

            this.GraphControl?.BeginUpdate();
            this.isLoadingInProgress = true;

            foreach (TGraphElement graphElement in rootGraphElements)
                this.Load(graphElement, parentGraphElement);

            this.isLoadingInProgress = false;
            this.GraphControl?.EndUpdate();

            this.isBinded = true;

            // Refresh focused node dependencies
            object focusedNode = this.GraphControl?.GetFocusedNode();

            if (focusedNode != oldFocusedNode)
                (this as IGraphController).FocusedNodeIsChanged(focusedNode, oldFocusedNode);
        }

        public void ClearNodes()
        {
            this.nodesByGraphElement.Clear();
            this.GraphControl?.ClearNodes();
        }

        public void BeginUpdate()
        {
            this.GraphControl?.BeginUpdate();
        }

        public void EndUpdate()
        {
            this.GraphControl?.EndUpdate();
        }

        /// <summary>
        /// Gets a GraphElement that is binded with a node.
        /// </summary>
        /// <param name="node">Control's graph node.</param>
        /// <returns>GraphElement binded with the node.</returns>
        public TGraphElement? GetGraphElement(object? node)
        {
            if (node != null)
            {
                object? tag = this.GraphControl?.GetGraphNodeTag(node);

                if (tag is GraphNodeTag<TGraphElement> graphNodeTag)
                    return graphNodeTag.GraphElement;
            }

            return default;
        }

        public IEnumerable<TGraphElement> GetAllGraphElements()
        {
            return this.nodesByGraphElement.Keys.ToArray();
        }

        public GraphNodeTag<TGraphElement>? GetGraphNodeTag(TGraphElement graphElement)
        {
            object? node = this.GetNode(graphElement);
            object? nodeTag = this.GraphControl?.GetGraphNodeTag(node);

            return nodeTag as GraphNodeTag<TGraphElement>;
        }

        public GraphColumnBindingPolicy<TGraphElement>? GetGraphColumnBindingPolicyByColumnIndex(Type objectType, int columnIndex)
        {
            GraphColumnBindingPolicy<TGraphElement>? result = null;
            HashArray<GraphColumnBindingPolicy<TGraphElement>>? columnBindingPolicyByColumnIndex = this.columnBindingPolicyByColumnIndexByObjectType[objectType];

            if (columnBindingPolicyByColumnIndex != null)
            {
				result = columnBindingPolicyByColumnIndex[columnIndex];

                if (result == null)
                {
                    result = this.CreateDefaultGraphColumnBindingPolicyByColumnIndex(objectType, columnIndex);

                    if (result != null)
                    {
                        columnBindingPolicyByColumnIndex[columnIndex] = result;
                        this.columnBindingPolicyByPropertyIndexByObjectType[objectType][result.PropertyModel.PropertyIndex] = result;
                    }
                }
            }
            else
            {
                result = this.CreateDefaultGraphColumnBindingPolicyByColumnIndex(objectType, columnIndex);

                if (result != null)
                {
                    columnBindingPolicyByColumnIndex = new HashArray<GraphColumnBindingPolicy<TGraphElement>>(this.Columns.Max(item => item.Index) + 1);
                    columnBindingPolicyByColumnIndex[columnIndex] = result;
                    this.columnBindingPolicyByColumnIndexByObjectType[objectType] = columnBindingPolicyByColumnIndex;

                    HashArray<GraphColumnBindingPolicy<TGraphElement>> columnBindingPolicyByPropertyIndex = new HashArray<GraphColumnBindingPolicy<TGraphElement>>();
                    columnBindingPolicyByPropertyIndex[result.PropertyModel.PropertyIndex] = result;
                    this.columnBindingPolicyByPropertyIndexByObjectType[objectType] = columnBindingPolicyByPropertyIndex;
                }
            }

            return result;
        }

        public GraphColumnBindingPolicy<TGraphElement>? GetGraphColumnBindingPolicyByObjectPropertyIndex(Type objectType, int propertyIndex)
        {
            GraphColumnBindingPolicy<TGraphElement>? result = null;
            HashArray<GraphColumnBindingPolicy<TGraphElement>>? columnBindingPolicyByPropertyIndex = this.columnBindingPolicyByPropertyIndexByObjectType[objectType];

            if (columnBindingPolicyByPropertyIndex != null)
            {
                result = columnBindingPolicyByPropertyIndex.GetValue(propertyIndex);

                if (result == null)
                {
                    result = this.CreateDefaultGraphColumnBindingPolicyByPropertyIndex(objectType, propertyIndex);

                    if (result != null)
                    {
                        columnBindingPolicyByPropertyIndex[propertyIndex] = result;
                        this.columnBindingPolicyByPropertyIndexByObjectType[objectType][result.GraphColumn.Index] = result;
                    }
                }
            }
            else
            {
                result = this.CreateDefaultGraphColumnBindingPolicyByPropertyIndex(objectType, propertyIndex);

                if (result != null)
                {
                    columnBindingPolicyByPropertyIndex = new HashArray<GraphColumnBindingPolicy<TGraphElement>>();
                    columnBindingPolicyByPropertyIndex[propertyIndex] = result;
                    this.columnBindingPolicyByPropertyIndexByObjectType[objectType] = columnBindingPolicyByPropertyIndex;

                    HashArray<GraphColumnBindingPolicy<TGraphElement>> columnBindingPolicyByColumnIndex = new HashArray<GraphColumnBindingPolicy<TGraphElement>>(this.Columns.Max(item => item.Index) + 1);
                    columnBindingPolicyByColumnIndex[result.GraphColumn.Index] = result;
                    this.columnBindingPolicyByColumnIndexByObjectType[objectType] = columnBindingPolicyByColumnIndex;
                }
            }

            return result;
        }


        //public IEnumerable<ColumnBindingPolicy<TGraphElement>> GetColumnBindingPolicyList(Type objectType, GraphColumn graphColumn)
        //      {
        //          List<ColumnBindingPolicy<TGraphElement>> result = new List<ColumnBindingPolicy<TGraphElement>>();

        //          foreach (var columnBindingPolicyListByGraphColumnItem in this.columnBindingPolicyByColumnIndexByObjectType)
        //              if (columnBindingPolicyListByGraphColumnItem.Key == objectType) // || ReflectionHelper.CompareTypes(columnBindingPolicyListByGraphColumnItem.Key, objectType, TypeComparisonCriteria.Subclass))
        //                  foreach (var columnBindingPolicyListItem in columnBindingPolicyListByGraphColumnItem.Value.Collection)
        //                      if (graphColumn == columnBindingPolicyListItem.Key)
        //                          result.AddRange(columnBindingPolicyListItem.Value);

        //          if (result.Count == 0)
        //          {
        //              result = new List<ColumnBindingPolicy<TGraphElement>>();
        //              IEnumerable<IPropertyModel> propertyModels = this.GetBindingObjectPropertyModels(objectType);
        //              IPropertyModel propertyModel = propertyModels.FirstOrDefault(pm => pm.Name == graphColumn.Name);

        //              if (propertyModel != null)
        //              {
        //                  ColumnBindingPolicy<TGraphElement> columnBindingPolicy = this.SetGraphColumnBindingPolicy(graphColumn.Name, propertyModel);
        //                  result.Add(columnBindingPolicy);
        //              }
        //          }

        //          return result;
        //      }

        //      public GraphColumn GetGraphColumnByBindingPolicy(Type objectType, int propertyIndex, string columnOrPropertyName)
        //      {
        //          IEnumerable<GraphColumn> graphColumns = this.GetGraphColumnsByBindingPolicy(objectType, propertyIndex, columnOrPropertyName);
        //          return graphColumns.Count() > 0 ? graphColumns.ElementAt(0) : null;
        //      }

        //public IEnumerable<GraphColumn> GetGraphColumnsByBindingPolicy(Type objectType, int propertyIndex, string columnOrPropertyName)
        //      {
        //          List<GraphColumn> result = new List<GraphColumn>();
        //          GraphColumn graphColumn;

        //          foreach (var columnBindingPolicyListByGraphColumnItem in this.columnBindingPolicies)
        //          {
        //              if (columnBindingPolicyListByGraphColumnItem.Key == objectType || Comparison.CompareTypes(columnBindingPolicyListByGraphColumnItem.Key, objectType, TypeComparisonCriteria.Subclass))
        //              {
        //                  foreach (var columnBindingPolicyListItem in columnBindingPolicyListByGraphColumnItem.Value)
        //                  {
        //                      graphColumn = columnBindingPolicyListItem.Key;

        //                      if (columnBindingPolicyListItem.Value.Find(columnBindingPolicy => columnBindingPolicy.PropertyModel.Index == propertyIndex) != null)
        //                      {
        //                          if (!result.Contains(graphColumn))
        //                          {
        //                              result.Add(graphColumn);
        //                          }
        //                      }
        //                  }
        //              }
        //          }

        //          if (result.Count == 0)
        //          {
        //		graphColumn = this.GetGraphColumn(columnOrPropertyName);

        //              if (graphColumn != null)
        //                  result.Add(graphColumn);
        //          }

        //          return result;
        //      }

        public GraphColumn? GetGraphColumn(string columnOrPropertyName)
        {
            return this.Columns.FirstOrDefault(c => c.Name == columnOrPropertyName);
        }

        public GraphColumnBindingPolicy<TGraphElement>? SetGraphColumnBindingPolicy(int columnIndex, IPropertyModel propertyModel)
        {
            return this.SetGraphColumnBindingPolicy(columnIndex, propertyModel, BindingOption.EnableEditing);
        }

        public GraphColumnBindingPolicy<TGraphElement>? SetGraphColumnBindingPolicy(int columnIndex, IPropertyModel propertyModel, BindingOption bindingOption)
        {
            if (propertyModel.Owner is ISimpleObjectModel simpleObjectModel)
			{
                Type objectType = simpleObjectModel.ObjectType;

                return this.SetGraphColumnBindingPolicy(columnIndex, objectType, propertyModel, bindingOption);
            }

            return null;
        }

        public GraphColumnBindingPolicy<TGraphElement>? SetGraphColumnBindingPolicy(int columnIndex, Type objectType, IPropertyModel propertyModel)
        {
            return this.SetGraphColumnBindingPolicy(columnIndex, objectType, propertyModel, BindingOption.EnableEditing);
        }

        public GraphColumnBindingPolicy<TGraphElement>? SetGraphColumnBindingPolicy(int columnIndex, Type objectType, IPropertyModel propertyModel, BindingOption bindingOption)
        {
            return this.SetGraphColumnBindingPolicy(columnIndex, objectType, propertyModel, null, bindingOption);
        }

        public GraphColumnBindingPolicy<TGraphElement>? SetGraphColumnBindingPolicy(int columnIndex, Type objectType, IPropertyModel propertyModel, Func<TGraphElement, object> getEditValue)
        {
            return this.SetGraphColumnBindingPolicy(columnIndex, objectType, propertyModel, getEditValue, BindingOption.EnableEditing);
        }

        public GraphColumnBindingPolicy<TGraphElement>? SetGraphColumnBindingPolicy(int columnIndex, Type objectType, IPropertyModel propertyModel, Func<TGraphElement, object>? getEditValue, BindingOption bindingOption)
        {
            GraphColumnBindingPolicy<TGraphElement>? result = null;
            GraphColumn? graphColumn = this.Columns[columnIndex];

            if (graphColumn is null)
                return null;

            HashArray<GraphColumnBindingPolicy<TGraphElement>> columnBindingPolicyByColumnIndex = this.columnBindingPolicyByColumnIndexByObjectType[objectType];

            if (columnBindingPolicyByColumnIndex != null)
            {
                result = columnBindingPolicyByColumnIndex[columnIndex];

                if (result == null)
                {
                    result = new GraphColumnBindingPolicy<TGraphElement>(propertyModel, graphColumn, getEditValue, bindingOption);

                    // Ovveride existing policy if any
                    columnBindingPolicyByColumnIndex[columnIndex] = result;
                    this.columnBindingPolicyByPropertyIndexByObjectType[objectType][propertyModel.PropertyIndex] = result;
                }
            }
            else
            {
                result = new GraphColumnBindingPolicy<TGraphElement>(propertyModel, graphColumn, getEditValue, bindingOption);

                columnBindingPolicyByColumnIndex = new HashArray<GraphColumnBindingPolicy<TGraphElement>>(this.Columns.Max(item => item.Index) + 1);
                columnBindingPolicyByColumnIndex[columnIndex] = result;
                this.columnBindingPolicyByColumnIndexByObjectType[objectType] = columnBindingPolicyByColumnIndex;

                HashArray<GraphColumnBindingPolicy<TGraphElement>> columnBindingPolicyByPropertyIndex = new HashArray<GraphColumnBindingPolicy<TGraphElement>>();
                
                columnBindingPolicyByPropertyIndex[result.PropertyModel.PropertyIndex] = result;
                this.columnBindingPolicyByPropertyIndexByObjectType[objectType] = columnBindingPolicyByPropertyIndex;
            }

            return result;


            //         GraphColumn graphColumn = this.Columns[graphColumnName];
            //GraphColumnBindingPolicy<TGraphElement> columnBindingPolicy = null;
            //         Dictionary<GraphColumn, List<ColumnBindingPolicy<TGraphElement>>> graphColumnBindingPolicies;
            //         List<GraphColumnBindingPolicy<TGraphElement>> columnBindingPolicyList;

            //         if (!this.columnBindingPolicies.TryGetValue(objectType, out graphColumnBindingPolicies))
            //         {
            //             graphColumnBindingPolicies = new Dictionary<GraphColumn, List<ColumnBindingPolicy<TGraphElement>>>();
            //             this.columnBindingPolicies.Add(objectType, graphColumnBindingPolicies);
            //         }

            //         if (!graphColumnBindingPolicies.TryGetValue(graphColumn, out columnBindingPolicyList))
            //         {
            //             columnBindingPolicyList = new List<ColumnBindingPolicy<TGraphElement>>();
            //             graphColumnBindingPolicies.Add(graphColumn, columnBindingPolicyList);
            //         }

            //         columnBindingPolicy = new ColumnBindingPolicy<TGraphElement>(propertyModel, getEditValue, bindingOption);
            //         columnBindingPolicyList.Add(columnBindingPolicy);

            //         return columnBindingPolicy;
        }

        public bool GetGraphColumnEnableProperty(GraphColumn graphColumn)
        {
            if (this.GraphControl is not null)
                return this.GraphControl.GetColumnEnableProperty(graphColumn.Index);

            return false;
        }

        public void SetGraphColumnEnableProperty(GraphColumn graphColumn, bool enabled)
        {
            this.GraphControl?.SetColumnEnableProperty(graphColumn.Index, enabled);
        }

        public bool GetGraphColumnVisibleProperty(GraphColumn graphColumn)
        {
            if (this.GraphControl is not null)
                return this.GraphControl.GetColumnVisibleProperty(graphColumn.Index);

            return false;
        }

        public void SetGraphColumnVisibleProperty(GraphColumn graphColumn, bool enabled)
        {
            this.GraphControl?.SetColumnVisibleProperty(graphColumn.Index, enabled);
        }

        public void BestFitColumns()
        {
            this.BestFitColumns(false);
        }

        public void BestFitColumns(bool applyAutoWidth)
        {
            if (this.nodesByGraphElement.Count > 0)
            {
                this.GraphControl?.BestFitColumns(applyAutoWidth);
                this.RaiseAfterBestFitGraphColumns();
            }
        }

        public void RemoveColumnSorting()
        {
            this.GraphControl?.RemoveColumnSorting();
            this.SetButtonsSortingEnableProperty();
        }

        public bool Validate()
        {
            bool isValid = false;

            switch (this.SaveButonOption)
            {
                case SaveButtonOption.CommitChanges:

                    if (this.EditorBindings.ObjectManager is not null)
                        isValid = this.EditorBindings.ObjectManager.DefaultChangeContainer.Validate().Passed;
                    
                    break;

                case SaveButtonOption.SaveFocusedNode:

                    if (this.FocusedNode is not null)
                        isValid = this.ValidateNode(this.FocusedNode).Passed;
                    
                    break;
            }

            return isValid;
        }

        bool ISimpleValidation.ValidateAndSave()
        {
            bool isValid = false;

            if (this.CommitChangeOnFocusedNodeChange)
            {
                switch (this.SaveButonOption)
                {
                    case SaveButtonOption.CommitChanges:

                        if (this.EditorBindings.ObjectManager is not null)
                            isValid = this.EditorBindings.ObjectManager.CommitChanges().TransactionSucceeded;
                        
                        break;

                    case SaveButtonOption.SaveFocusedNode:

                        if (this.FocusedGraphElement is not null)
                            isValid = this.ValidateAndTrySaveNodeIfRequired(this.FocusedGraphElement);
                        
                        break;
                }
            }

            return isValid;
        }

        //public bool ValidateAndTrySaveNodeIfRequired()
        //{
        //    if (this.FocusedNode is not null)
        //        return this.ValidateAndTrySaveNodeIfRequired(this.FocusedNode);

        //    return true;
        //}

        //public bool ValidateAndTrySaveNodeIfRequired(object node) => this.ValidateAndTrySaveNodeIfRequired(node, requester: this);

        public bool ValidateAndTrySaveNodeIfRequired(object node)
        {
            if (node == null)
                return true;

            this.GraphControl?.CloseEditor();

            TGraphElement? graphElement = this.GetGraphElement(node);
            //GraphNodeTag<TGraphElement> nodeTag = this.GraphControl.GetGraphNodeTag(node) as GraphNodeTag<TGraphElement>;

            // if there is no property changes on the object retrun last validation result if exists.
            //if (graphElement.SimpleObject.ChangedPropertyNames.Count == 0) // && this.lastValidationNode == node && this.lastValidationNodeResult != null)
            //{
            //    if (!this.lastValidationNodeResult.IsValid)
            //    {
            //        this.SetGraphValidationError(this.lastValidationNodeResult);
            //    }

            //    return this.lastValidationNodeResult.IsValid;
            //}

            // First store data on attached events that bind focused object 
            if (graphElement is SimpleObject simpleObject)
            {
                if (simpleObject.IsDeleted) // Due to dev express TreeList bug vhen deleting nodes
                    return true;

                this.RaiseBindingObjectStoreData(simpleObject);

                if (simpleObject.ChangedPropertiesCount == 0)
                {
                    this.RaiseBindingObjectOnValidation(new GraphValidationResult(new SimpleObjectValidationResult(graphElement as SimpleObject, true, String.Empty, null, TransactionRequestAction.Save), graphNode: null, errorColumnIndex: -1));
                    
                    return true;
                }
            }

            if (graphElement is not null && !this.DoesGraphElementRequireSaving(graphElement))
                return true;

            if (!this.CommitChangeOnFocusedNodeChange)
                return true;

            GraphValidationResult graphValidationResult = this.ValidateNode(node);
            bool isValid = graphValidationResult.Passed;

            if (isValid)
            {
                bool isSaved = this.SaveNode(node);

                if (!isSaved)
                    graphValidationResult = new GraphValidationResult(graphValidationResult.SimpleObjectValidationResult, graphNode: null, errorColumnIndex: -1, message: "Validation passed, Saving failed!");
            }

            if (graphElement is SimpleObject)
                this.RaiseBindingObjectOnValidation(graphValidationResult);

            if (!isValid && graphValidationResult.ErrorGraphColumnIndex >= 0)
            {
                this.SetGraphValidationError(graphValidationResult);
            }
            else if (this.isGraphErrorMessageSet)
            {
                this.GraphControl?.ResetColumnErrors();
                this.isGraphErrorMessageSet = false;
            }

            //this.RaiseOnValidation(graphElement.SimpleObject, graphValidationResult);

            //this.lastValidationNode = node;
            this.lastValidationNodeResult = graphValidationResult;

            if (!graphValidationResult.Passed)
                this.lastValidationFailedNode = node;

            return isValid;
        }

        public void SetFocusedGraphElement(TGraphElement graphElement)
        {
            object? node = this.GetNode(graphElement);

            if (node != null)
                this.GraphControl?.SetFocusedNode(node);
        }

        public void ExpandGraphElement(TGraphElement graphElement)
        {
            object? node = this.GetNode(graphElement);
            
            if (node != null)
                this.GraphControl?.ExpandNode(node);
        }

        public void CollapseGraphElement(TGraphElement graphElement)
        {
            object? node = this.GetNode(graphElement);
            
            if (node != null)
                this.GraphControl?.CollapseNode(node);
        }

        public void ExpandAll()
        {
            this.GraphControl?.ExpandAll();
        }

        public void CollapseAll()
        {
            this.GraphControl?.CollapseAll();
        }

        public void RefreshGraphElement(TGraphElement graphElement)
        {
            object? node = this.GetNode(graphElement);
            
            if (node != null)
                this.RefreshNode(graphElement, node);
        }

        public void RefreshAll()
        {
            foreach (var nodeGraphElementItem in this.nodesByGraphElement)
                this.RefreshNode(nodeGraphElementItem.Key, nodeGraphElementItem.Value);
        }

        public void SetGraphElementImage(TGraphElement graphElement, string? imageName)
        {
            if (imageName is null)
                return;

            object node = this.nodesByGraphElement[graphElement];

            if (node == null || this.ImageList is null || this.GraphControl is null)
                return;
            
            int imageIndex = this.ImageList.Images.IndexOfKey(imageName);

            if (imageIndex < 0)
                return;

            GraphNodeTag<TGraphElement>? nodeTag = this.GetGraphNodeTag(node);

            if (nodeTag is not null)
            {
                nodeTag.ImageIndex = imageIndex;

                this.BeginUpdate();
                this.GraphControl.SetNodeImageIndex(node, imageIndex);
                this.EndUpdate();
            }
        }

        public void SetGraphElementStateImage(TGraphElement graphElement, string stateImageName)
        {
            object node = this.nodesByGraphElement[graphElement];
            GraphNodeTag<TGraphElement>? nodeTag = this.GetGraphNodeTag(node);

            if (nodeTag is not null && this.StateImageList is not null && this.GraphControl is not null)
            {
                nodeTag.StateImageIndex = this.StateImageList.Images.IndexOfKey(stateImageName);

                this.BeginUpdate();
                this.GraphControl.SetNodeStateImageIndex(node, nodeTag.StateImageIndex);
                this.EndUpdate();
            }
        }

        public void SetGraphElementChecked(TGraphElement graphElement, bool value)
        {
            object? node = this.GetNode(graphElement);

            if (node is not null)
            {
                this.BeginUpdate();
                this.SetGraphNodeChecked(node, value);
                this.EndUpdate();
            }
        }

        public void SetGraphNodeCheckDisabled(TGraphElement graphElement, bool value)
        {
            object? node = this.GetNode(graphElement);
            
            if (node is not null)
                this.SetGraphNodeCheckDisabled(node, value);
        }

        public object AddGraphElement(TGraphElement graphElement, TGraphElement? parentGraphElement)
        {
            object node = this.AddNodeInternal(graphElement, parentGraphElement);
            GraphNodeTag<TGraphElement>? graphNodeTag = this.GetGraphNodeTag(node);
            //IEnumerable<TGraphElement> childGraphElements = this.GetChildGraphElements(graphElement);

            this.UpdateNodeInternal(graphElement, node);
            this.RaiseGraphElementCreated(graphElement);

            //this.GraphControl.SetNodeHasChildrenProperty(node, childGraphElements.Count() > 0);
            if (graphNodeTag is not null)
                graphNodeTag.ChildrenNodesLoaded = true;

            return node;
        }

        public bool RemoveGraphElement(TGraphElement graphElement)
        {
            bool isRemoved = false;

            if (this.ContainsGraphElement(graphElement))
            {
                this.RemoveNodeInternal(graphElement);
                isRemoved = true;
            }

            return isRemoved;
        }

        public object? GetCellEditValue(TGraphElement graphElement, int columnIndex)
        {
            object? node = this.GetNode(graphElement);

            if (node is not null && this.GraphControl is not null)
                return this.GraphControl.GetCellEditValue(node, columnIndex);

            return null;
        }

        public void SetCellEditValue(TGraphElement graphElement, int columnIndex, object editValue)
        {
            object? node = this.GetNode(graphElement);
            
            if (node is not null && this.GraphControl is not null)
                this.GraphControl.SetCellEditValue(node, columnIndex, editValue);
        }

        public object? GetFocusedNode()
        {
            return this.FocusedNode;
        }

        public object? GetNode(TGraphElement? graphElement)
        {
            if (graphElement == null)
                return null;

            return this.nodesByGraphElement[graphElement];
        }

        public object? GetParentNode(TGraphElement graphElement)
        {
            object? node = this.GetNode(graphElement);
            
            if (node is not null)
                return this.GetParentNode(node);

            return null;
        }

        public object? GetParentNode(object node)
        {
            return this.GraphControl?.GetParentNode(node);
        }

        public ICollection? GetChildNodes(TGraphElement graphElement)
        {
            object? node = this.GetNode(graphElement);
            
            if (node is not null)
                return this.GetChildNodes(node);

            return null;
        }

        public ICollection? GetChildNodes(object node)
        {
            if (this.GraphControl is not null)
               return this.GraphControl.GetChildNodes(node);

            return null;
        }

        public int GetChildNodesCount(TGraphElement graphElement)
        {
            object? node = this.GetNode(graphElement);
            
            if (node is not null)
                return this.GetChildNodesCount(node);

            return -1;
        }

        public int GetChildNodesCount(object node)
        {
            if (this.GraphControl is not null)
                return this.GraphControl.GetChildNodesCount(node);

            return -1;
        }


        public bool ContainsGraphElement(TGraphElement graphElement)
        {
            return this.nodesByGraphElement.ContainsKey(graphElement);
        }

		public void SetImageLists()
		{
			this.isImagesLoaded = false;
			this.isStateImagesLoaded = false;
			this.isCheckBoxImagesLoaded = false;

			this.SetImagesIfIsNotLoaded();
		}

		#endregion |   Public Methods   |

		#region |   Protected Abstract Methods   |

		protected abstract IGraphControl? GetGraphControl();
        protected abstract object? GetGraphElementPropertyValue(TGraphElement graphElement, int propertyIndex);
        protected abstract void SetGraphElementPropertyValue(TGraphElement graphElement, int propertyIndex, object? value, object? requester);
        protected abstract string GetGraphElementName(TGraphElement graphElement);
        protected abstract string GetGraphElementImageName(TGraphElement graphElement);
        protected abstract TGraphElement? GetParentGraphElement(TGraphElement graphElement);
        protected abstract void SetParentGraphElement(TGraphElement graphElement, TGraphElement? parentGraphElement);
        protected abstract void SetOrderIndex(TGraphElement? graphElement, int orderIndex);
        protected abstract IEnumerable<TGraphElement> GetChildGraphElements(TGraphElement graphElement);
        protected abstract bool CanGraphElementChangeParent(TGraphElement graphElement, TGraphElement? newParentGraphElement);
        protected abstract bool DoesGraphElementRequireSaving(TGraphElement graphElement);

        // TODO: Načiniti ISimpleBindigObject interface koje vraća ova metoda ili posojeće IBindingObject načiniti generičkim i staviti ga u Simple
        protected abstract object? GetBindingObject(TGraphElement? graphElement);
        protected abstract bool CanAddGraphElement(Type objectType, TGraphElement? parentGraphElement);
        protected abstract TGraphElement? CreateNewGraphElement(Type objectType, TGraphElement? parentGraphElement, object? requester);
        protected abstract bool CanDeleteGraphElement(TGraphElement graphElement);
        protected abstract bool DeleteGraphElement(TGraphElement graphElement);
        protected abstract bool SaveGraphElement(TGraphElement graphElement);
        protected abstract ValidationResult ValidateGraphElement(TGraphElement graphElement);
        protected abstract IPropertyModel? GetBindingObjectPropertyModel(Type bindingObjectType, int propertyIndex);
        protected abstract IPropertyModel? GetBindingObjectPropertyModel(Type bindingObjectType, string propertyName);

        //protected abstract void SetEditMode(GraphEditMode editMode);
        //protected abstract void SetLookAndFeelStyle(GraphLookAndFeelStyle lookAndFeelStyle);
        //protected abstract void SetCanDragAndDrop(bool canDragAndDrop);

        //protected abstract void SetImageList(ImageList imageList);
        //protected abstract void SetStateImageList(ImageList stateImageList);
        //protected abstract void SetCheckBoxImageList(ImageList checkBoxImageList);

        //protected internal abstract void AddColumn(string columnName, string caption, BindingDataType dataType, BindingEditorType editorType);
        //protected internal abstract void SetColumnCaption(string columnName, string caption);
        //protected internal abstract void SetColumnDataType(string columnName, BindingDataType dataType);
        //protected internal abstract void SetColumnEditorType(string columnName, BindingEditorType editorType);
        //protected internal abstract int GetColumnWidth(string columnName);
        //protected internal abstract void SetColumnWidth(string columnName, int width);
        //protected internal abstract void RemoveColumn(string columnName);
        //protected abstract bool GetColumnEnableProperty(string columnName);
        //protected abstract void SetColumnEnableProperty(string columnName, bool enabled);
        //protected abstract void SetColumnError(string columnName, string errorMessage);
        //protected abstract void ResetColumnErrors();
        //protected abstract void BestFitColumns(bool applyAutoWidth);

        //protected abstract void BeginUpdate();
        //protected abstract void EndUpdate();

        //protected abstract object AddNode(GraphElement graphElement, object parentNode, object nodeTag);
        //protected abstract GraphNodeTag CreateNodeTag(GraphElement graphElement);
        //protected abstract GraphNodeTag GetGraphNodeTag(object node);
        //protected abstract void SetNodeCheckedProperty(object node, bool value);
        //protected abstract void SetNodeHasChildrenProperty(object node, bool value);
        //protected abstract void RemoveNode(object node);
        //protected abstract void UpdateCellEditValue(object node, string propertyName, object editValue);
        //protected abstract void MoveNode(object sourceNode, object destinationNode);
        //protected abstract void ExpandNode(object node);
        //protected abstract void CollapseNode(object node);
        //protected abstract void ExpandAll();
        //protected abstract void CollapseAll();

        //protected abstract object GetParentNode(object node);
        //protected abstract object GetFocusedNode();
        //protected abstract void SetFocusedNode(object node);
        //protected abstract void SetFocusedColumn(string columnName);
        //protected abstract void SetNodeImageIndex(object node, int imageIndex);
        //protected abstract void SetNodeExpandedImageIndex(object node, int imageIndex);
        //protected abstract void SetNodeStateImageIndex(object node, int imageIndex);
        //protected abstract void SetNodeCheckBoxImageIndex(object node, int imageIndex);

        //protected abstract void SetCheckBoxMode(bool isCheckBoxMode);

        //protected abstract void CloseEditor();
        //protected abstract void ShowEditor();

        //protected abstract void SetButtonEnableProperty(Component button, bool enabled);

        ///// <summary>
        ///// Gets a GraphElement that is binded with a node.
        ///// </summary>
        ///// <param name="node">Control's graph node.</param>
        ///// <returns>GraphElement binded with the node.</returns>
        //protected abstract GraphElement GetGraphElementByNode(object node);

        //protected abstract Component GetEditorComponent(string columnName);
        //protected abstract void ClearNodes();

        #endregion |   Protected Abstract Methods   |

        #region |   Protected GraphControl Methods   |

        protected void ControlButtonAddFolderIsClicked(Component button)
        {
            TGraphElement? parentGraphElement = (this.FocusedGraphElement is not null) ? this.GetParentGraphElement(this.FocusedGraphElement) 
                                                                                       : default(TGraphElement);
            
            this.ControlTryAddNewObjectByButtonClick(button, requester: parentGraphElement);
        }

        protected void ControlButtonAddSubFolderIsClicked(Component button, object requester)
        {
            this.ControlTryAddNewObjectByButtonClick(button, requester);
        }

        protected void ControlButtonAddIsClicked(Component button, object requester)
        {
            this.ControlTryAddNewObjectByButtonClick(button, requester);
        }

        protected virtual bool ControlTryAddNewObjectByButtonClick(Component button, object requester)
        {
            return this.ControlTryAddNewObjectByButtonClick(button, this.FocusedGraphElement, requester);
        }

        protected virtual bool ControlTryAddNewObjectByButtonClick(Component button, TGraphElement parentGraphElement, object requester)
        {
            bool success = false;
            AddButtonPolicy<TGraphElement> addButtonPolicy = this.AddButtonPolicyList.FirstOrDefault(a => a.Buttons.Contains(button));

            if (addButtonPolicy != null)
            {
                Cursor currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;

                TGraphElement realParentGraphElement = this.GetParentGraphElementForNewGraphInsertion(parentGraphElement, addButtonPolicy);
                success = this.ControlTryAddNewObject(addButtonPolicy, realParentGraphElement, requester);

                Cursor.Current = currentCursor;
            }
            else
            {
                success = false;
                throw new ArgumentException("Object type is not specified for button " + button.ToString());
            }

            return success;
        }

        protected virtual bool ControlTryAddNewObject(AddButtonPolicy<TGraphElement> addButtonPolicy, TGraphElement parentGraphElement, object requester)
        {
            bool success = (this.FocusedNode is not null) ? this.ValidateAndTrySaveNodeIfRequired(this.FocusedNode)
                                                          : true;

            if (success)
                success = this.AddNewObject(addButtonPolicy, parentGraphElement, requester);

            return success;
        }

        protected void ControlButtonMoveUpIsClicked(Component button)
        {
            if (!this.IsActive)
                return;

            object? node = this.GetFocusedNode();

            if (node is not null)
            {
                TGraphElement? graphElement = this.GetGraphElement(node);

                if (graphElement != null && graphElement is SortableSimpleObject sortableSimpleObject)
                {
                    this.GraphControl?.CloseEditor();

                    if (sortableSimpleObject.IsNew && !sortableSimpleObject.ValidateSave().Passed)
                    {
                        if (this.FocusedNode is not null)
                            this.ValidateAndTrySaveNodeIfRequired(this.FocusedNode);
                        
                        return;
                    }

                    this.BeginUpdate();

                    //sortableSimpleObject.SetOrderIndex(sortableSimpleObject.OrderIndex - 1, requester: this);
                    sortableSimpleObject.MoveUp();

                    //this.SaveGraphElement(graphElement);

                    this.EndUpdate();
                }
            }
        }

        protected void ControlButtonMoveDownIsClicked(Component button)
        {
            if (!this.IsActive)
                return;

            object? node = this.GetFocusedNode();

            if (node is not null)
            {
                TGraphElement? graphElement = this.GetGraphElement(node);

                if (graphElement != null && graphElement is SortableSimpleObject sortableSimpleObject)
                {
                    this.GraphControl?.CloseEditor();

                    if (sortableSimpleObject.IsNew && !sortableSimpleObject.ValidateSave().Passed)
                    {
                        if (this.FocusedNode is not null)
                            this.ValidateAndTrySaveNodeIfRequired(this.FocusedNode);
                        
                        return;
                    }

                    this.BeginUpdate();

                    //sortableSimpleObject.SetOrderIndex(sortableSimpleObject.OrderIndex + 1, requester: this);
                    sortableSimpleObject.MoveDown();

                    //this.SaveGraphElement(graphElement);

                    this.EndUpdate();
                }
            }
        }

        protected virtual void ControlButtonRemoveColumnSortingIsClicked(Component button)
        {
            Cursor? currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            this.RemoveColumnSorting();

            Cursor.Current = currentCursor;
        }

        protected virtual void ControlButtonSaveIsClicked(Component button)
        {
            Cursor? currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            if (this.SaveButonOption == SaveButtonOption.CommitChanges)
            {
                //if (this.EditorBindings != null && this.EditorBindings.ObjectManager != null)
                    this.EditorBindings?.ObjectManager?.CommitChanges();
            }
            else if (this.SaveButonOption == SaveButtonOption.SaveFocusedNode)
            {
                if (this.FocusedNode is not null)
                    this.ValidateAndTrySaveNodeIfRequired(this.FocusedNode);
            }

            Cursor.Current = currentCursor;
        }

        protected virtual void ControlButtonRemoveIsClicked(Component button)
        {
            TGraphElement? graphElement = this.FocusedGraphElement;

            if (graphElement != null)
            {
                bool canDelete = true;

                if (graphElement is SimpleObject simpleObject)
                {
                    canDelete = simpleObject.Manager.CanDelete(simpleObject);

                    if (canDelete)
                    {
                        ISimpleObjectModel simpleObjectModel = simpleObject.GetModel();
                        canDelete = XtraMessageBox.Show(String.Format("Do you want to delete {0} {1}?", simpleObjectModel.Caption, simpleObject.GetName()),
                                                        "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes;
                    }
                }

                if (canDelete)
                {
                    if (this.DeleteGraphElement(graphElement))
                        this.RemoveNodeInternal(graphElement);
                }
            }
        }

        #endregion |   Protected GraphControl Methods   |

        #region |   Protected Methods   |

        protected GraphNodeTag<TGraphElement>? GetGraphNodeTag(object node)
        {
            if (this.GraphControl is not null && this.GraphControl.GetGraphNodeTag(node) is GraphNodeTag<TGraphElement> graphNodeTag)
                return graphNodeTag;
            
            return null;
        }

        protected void RemoveAddButtonPolicyByButton(Component button)
        {
            var addButtonPoliciesToRemove = from addButtonPolicy in this.AddButtonPolicyList
                                            where addButtonPolicy.Buttons.Contains(button)
                                            select addButtonPolicy;

            foreach (AddButtonPolicy<TGraphElement> addButtonPolicy in addButtonPoliciesToRemove)
                this.AddButtonPolicyList.Remove(addButtonPolicy);
        }

        protected bool CanNodeChangeParent(object node, object? newParentNode)
        {
            TGraphElement? graphElement = this.GetGraphElement(node);

            if (graphElement != null)
            {
                TGraphElement? newParentGraphElement = this.GetGraphElement(newParentNode);
                TGraphElement? parentGraphElement = (newParentGraphElement is not null) ? this.GetParentGraphElement(graphElement)
                                                                                        : default;
                if (Comparison.Equals(parentGraphElement, newParentGraphElement))
                {
                    return false;
                }
                else
                {
                    return this.CanGraphElementChangeParent(graphElement, newParentGraphElement);
                }
            }

            return false;
        }

        protected Form? FindGraphControlForm()
        {
            Form? result = null;

            if (this.GraphControl != null)
                result = this.GraphControl.FindForm();

            return result;
        }

        protected void SetButtonsSaveProperty(bool enabled)
        {
            foreach (Component button in this.ButtonSaveList)
                this.SetButtonEnablePropertyInternal(button, enabled);
        }

        protected void SetButtonsMoveUpEnableProperty(TGraphElement? graphElement)
        {
            bool enabled = false;

            if (this.GraphControl?.GetSortedColumnCount() == 0 && graphElement is SortableSimpleObject sortableSimpleObject)
                enabled = !sortableSimpleObject.IsFirst;

            foreach (Component buttonMoveUp in this.ButtonMoveUpList)
                this.SetButtonEnablePropertyInternal(buttonMoveUp, enabled);
        }

        protected void SetButtonsMoveDownEnableProperty(TGraphElement? graphElement)
        {
            bool enabled = false;

            if (this.GraphControl?.GetSortedColumnCount() == 0 && graphElement is SortableSimpleObject sortableSimpleObject)
                enabled = !sortableSimpleObject.IsLast;

            foreach (Component buttonMoveDown in this.ButtonMoveDownList)
                this.SetButtonEnablePropertyInternal(buttonMoveDown, enabled);
        }

        protected void SetButtonsSortingEnableProperty()
        {
            bool enabled = (this.GraphControl.GetSortedColumnCount() > 0);

            foreach (Component button in this.ButtonRemoveColumnSortingList)
                this.SetButtonEnablePropertyInternal(button, enabled);

            this.SetButtonsMoveUpEnableProperty(this.FocusedGraphElement);
            this.SetButtonsMoveDownEnableProperty(this.FocusedGraphElement);
        }

        protected virtual void OnInitializeComponent() { }

        protected virtual void OnSetChangableBindingObjectControl() { }

        protected virtual void OnDispose() { }

        #endregion |   Protected Methods   |

        #region |   Protected Object Manager Based Methods   |

        //private void manager_GraphElementCreated(object sender, GraphElementRequesterEventArgs e)
        //{
        //    if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
        //        return;

        //    Form form = this.FindGraphControlForm();

        //    if (form != null)
        //    {
        //        form.Invoke(new MethodInvoker(() => OnGraphElementCreated(sender, e)));
        //    }
        //    else
        //    {
        //        this.OnGraphElementCreated(sender, e);
        //    }
        //}

        protected void ManagerOnGraphElementCreated(TGraphElement graphElement)
        {
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            //if (this.BindOnlyToSpecifiedGraphKey && e.GraphElement.Graph != this.Graph)
            //    return;
            TGraphElement? parent = this.GetParentGraphElement(graphElement);

            if ((parent == null && this.anchorGraphElement == null) || (parent != null && (this.nodesByGraphElement.ContainsKey(parent) || parent.Equals(this.anchorGraphElement))))
            {
                //this.BeginUpdate();

                object node = this.AddNodeInternal(graphElement, parent);
                this.UpdateNodeInternal(graphElement, node);

                //this.EndUpdate();

                this.RaiseGraphElementCreated(graphElement);
            }
        }

        //private void manager_BeforeDeleting(object sender, SimpleObjectRequesterEventArgs e)
        //{
        //    if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
        //        return;

        //    Form form = this.FindGraphControlForm();

        //    if (form != null)
        //    {
        //        form.Invoke(new MethodInvoker(() => OnBeforeDeleting(sender, e)));
        //    }
        //    else
        //    {
        //        this.OnBeforeDeleting(sender, e);
        //    }
        //}

        protected void ManagerOnBeforeDeleting(TGraphElement graphElement)
        {
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            if (this.nodesByGraphElement.ContainsKey(graphElement))
            {
                this.RemoveNodeInternal(graphElement);
                this.RaiseBeforeDeleteGraphElement(graphElement);
            }
        }

        //private void manager_AfterDelete(object sender, SimpleObjectRequesterEventArgs e)
        //{
        //    if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
        //        return;

        //    Form form = this.FindGraphControlForm();

        //    if (form != null)
        //    {
        //        form.Invoke(new MethodInvoker(() => OnAfterDelete(sender, e)));
        //    }
        //    else
        //    {
        //        this.OnAfterDelete(sender, e);
        //    }
        //}

        private void ManagerOnAfterDelete(TGraphElement graphElement)
        {
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            // Refresh buttons while BeforeDeleting still have GraphElement references in collctions that compute e.g CanDeleteGraphElemnt
            this.SetButtonsEnableProperty(this.FocusedNode);
        }

        //private void manager_PropertyValueChange(object sender, ChangePropertyValueSimpleObjectRequesterEventArgs e)
        //{
        //    if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
        //        return;

        //    Form form = this.FindGraphControlForm();

        //    if (form != null)
        //    {
        //        form.Invoke(new MethodInvoker(() => OnPropertyValueChange(sender, e)));
        //    }
        //    else
        //    {
        //        this.OnPropertyValueChange(sender, e);
        //    }
        //}

        private void ManagerOnPropertyValueChange(TGraphElement graphElement, IPropertyModel? propertyModel, object? value, object? oldValue, ChangeContainer? changeContainer,  bool isChanged)
        {
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            object? node = this.GetNode(graphElement);

            if (this.RefreshGraphNodeOnEveryBindingObjectPropertyValueChange)
            {
                this.RefreshNode(graphElement, node);
            }
            else
            {
                object? bindingObject = this.GetBindingObject(graphElement);

                if (bindingObject != null && propertyModel != null)
                {
                    GraphColumnBindingPolicy<TGraphElement>? columnBindingPolicy = this.GetGraphColumnBindingPolicyByObjectPropertyIndex(bindingObject.GetType(), propertyModel.PropertyIndex);

                    if (columnBindingPolicy != null && node != null)
                        this.UpdateNodeColumnValueInternal(graphElement, node, bindingObject.GetType(), columnBindingPolicy.GraphColumn, propertyModel.PropertyIndex);
                }
            }

            this.RaiseBindingObjectPropertyValueChange(graphElement, propertyModel, value, oldValue, isChanged, changeContainer, requester: this);
        }

        //private void manager_GraphElementParentChange(object sender, ChangeParentGraphElementRequesterEventArgs e)
        //{
        //    if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
        //        return;

        //    Form form = this.FindGraphControlForm();

        //    if (form != null)
        //    {
        //        form.Invoke(new MethodInvoker(() => OnGraphElementParentChange(sender, e)));
        //    }
        //    else
        //    {
        //        this.OnGraphElementParentChange(sender, e);
        //    }
        //}

        private void ManagerOnGraphElementParentChange(TGraphElement graphElement, TGraphElement oldParent, object requester)
        {
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            object? sourceNode = this.GetNode(graphElement);

            if (sourceNode == null)
                return;

            TGraphElement? parentGraphElement = this.GetParentGraphElement(graphElement);
			object? destinationNode = this.GetNode(parentGraphElement);

            this.GraphControl?.MoveNode(sourceNode, destinationNode);

            if (destinationNode != null)
                this.GraphControl?.ExpandNode(destinationNode);

            this.RaiseParentGraphElementChange(graphElement, oldParent, changeContainer: null, requester);
        }

        //private void manager_ChangedPropertyNamesCountChange(object sender, CountChangeSimpleObjectEventArgs e)
        //{
        //    if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
        //        return;

        //    Form form = this.FindGraphControlForm();

        //    if (form != null)
        //    {
        //        form.Invoke(new MethodInvoker(() => OnChangedPropertyNamesCountChange(sender, e)));
        //    }
        //    else
        //    {
        //        this.OnChangedPropertyNamesCountChange(sender, e);
        //    }
        //}

        private void ManagerOnChangedPropertyNamesCountChange(TGraphElement graphElement, int oldChangedPropertyNamesCount, int changedPropertyNamesCount)
        {
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            if ((changedPropertyNamesCount == 0 || oldChangedPropertyNamesCount == 0))
            {
                if (this.FocusedNode != null && graphElement.Equals(this.FocusedGraphElement))
                {
                    this.SetBattonsSaveEnableProperty(this.FocusedGraphElement);
                }
            }
        }

        //private void manager_RelationForeignObjectSet(object sender, RelationForeignObjectSetEventArgs e)
        //{
        //    if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
        //        return;

        //    Form form = this.FindGraphControlForm();

        //    if (form != null)
        //    {
        //        form.Invoke(new MethodInvoker(() => OnRelationForeignObjectSet(sender, e)));
        //    }
        //    else
        //    {
        //        this.OnRelationForeignObjectSet(sender, e);
        //    }
        //}

        //private void ManagerOnRelationForeignObjectSet(object sender, RelationForeignObjectSetEventArgs e)
        //{
        //	if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
        //		return;

        //	//this.RefreshNodeBySimpleObject(e.SimpleObject);
        //	//this.RefreshNodeBySimpleObject(e.ForeignSimpleObject);
        //	//this.RefreshNodeBySimpleObject(e.OldForeignSimpleObject);

        //	this.RaiseBindingObjectRelationForeignObjectSet(new BindingObjectRelationForeignObjectSetEventArgs(e));
        //}

        //private void manager_ImageNameChange(object sender, ImageNameChangeSimpleObjectEventArgs e)
        //{
        //    if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
        //        return;

        //    Form form = this.FindGraphControlForm();

        //    if (form != null)
        //    {
        //        form.Invoke(new MethodInvoker(() => OnImageNameChange(sender, e)));
        //    }
        //    else
        //    {
        //        this.OnImageNameChange(sender, e);
        //    }
        //}

        private void OnImageNameChange(TGraphElement graphElement, string oldImageName, string newImageName)
        {
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            if (this.nodesByGraphElement.ContainsKey(graphElement))
            {
                object node = this.nodesByGraphElement[graphElement];
                GraphNodeTag<TGraphElement>? nodeTag = this.GetGraphNodeTag(node);

                if (nodeTag != null && this.ImageList != null)
                {
                    nodeTag.ImageIndex = this.ImageList.Images.IndexOfKey(newImageName);

                    this.BeginUpdate();
                    this.GraphControl?.SetNodeImageIndex(node, nodeTag.ImageIndex);
                    this.EndUpdate();
                }
            }
        }

        #endregion |   Protected Object Manager Based Methods   |

        #region |   Protected Raise Event Methods   |

        protected void RaiseGraphElementCreated(TGraphElement graphElement)
        {
            this.GraphElementCreated?.Invoke(this, new GraphElementEventArgs<TGraphElement>(graphElement));
        }

        protected void RaiseBeforeDeleteGraphElement(TGraphElement graphElement)
        {
            this.BeforeDeleteGraphElement?.Invoke(this, new GraphElementEventArgs<TGraphElement>(graphElement));
        }

        protected void RaiseBindingObjectChange(object? bindingObject)
        {
            this.BindingObjectChange?.Invoke(this, new BindingObjectEventArgs(bindingObject));
        }

        protected void RaiseBindingObjectPropertyValueChange(TGraphElement graphElement, IPropertyModel? propertyModel, object? propertyValue, object? oldPropertyValue, bool isChanged, ChangeContainer? changeContainer, object? requester)
        {
            if (this.GetBindingObject(graphElement) is SimpleObject simpleObject)
                this.BindingObjectPropertyValueChange?.Invoke(this, new ChangePropertyValueBindingObjectEventArgs(simpleObject, propertyModel, propertyValue, oldPropertyValue, isChanged, changeContainer, ObjectActionContext.Unspecified, requester));
        }

        protected void RaiseParentGraphElementChange(TGraphElement graphElement, TGraphElement oldParent, ChangeContainer? changeContainer, object? requester)
        {
            if (this.GetBindingObject(graphElement) is GraphElement ge)
            this.BindingObjectParentGraphElementChange?.Invoke(this, new OldParentGraphElemenChangeContainertContextRequesterEventArgs(ge, this.GetBindingObject(oldParent) as GraphElement, changeContainer, ObjectActionContext.Unspecified, requester));
        }

        protected void RaiseBindingObjectRelationForeignObjectSet(BindingObjectRelationForeignObjectSetEventArgs bindingObjectRelationForeignObjectSetEventArgs)
        {
            this.BindingObjectRelationForeignObjectSet?.Invoke(this, bindingObjectRelationForeignObjectSetEventArgs);
        }

        protected void RaiseAfterSetGraphColumnsEnableProperty(TGraphElement graphElement)
        {
            this.AfterSetGraphColumnsEnableProperty?.Invoke(this, new GraphElementEventArgs<TGraphElement>(graphElement));
        }

        protected void RaiseOnSetButtonEnableProperty(Component button)
        {
            this.OnSetButtonEnableProperty?.Invoke(this, new ButtonEventArgs(button));
        }

        protected object? GetEditValueByEvent(TGraphElement graphElement, object node, GraphColumn graphColumn, int bindingObjectPropertyIndex, object? value)
        {
            if (this.GetCustomCellEditValue != null)
            {
                EditValueGraphElementEventArgs<TGraphElement> args = new EditValueGraphElementEventArgs<TGraphElement>(graphElement, node, graphColumn, bindingObjectPropertyIndex, value);
                
                this.GetCustomCellEditValue(this, args);

                return args.EditValue;
            }
            else
            {
                return value;
            }
        }

        protected void RaiseAfterReloadNode(TGraphElement graphElement, object node)
        {
            this.AfterNodeReload?.Invoke(this, new GraphElementNodeEventArgs<TGraphElement>(graphElement, node));
        }

        protected bool ReloadNodeByEvent(TGraphElement graphElement, object node)
        {
            if (this.ReloadNode != null)
            {
                ReloadGraphElementNodeEventArgs<TGraphElement> args = new ReloadGraphElementNodeEventArgs<TGraphElement>(graphElement, node, false);
                
                this.ReloadNode(this, args);

                return args.IsReloaded;
            }
            else
            {
                return false;
            }
        }

        protected void RaiseAfterBestFitGraphColumns()
        {
            this.AfterBestFitGraphColumns?.Invoke(this, new EventArgs());
        }

        protected BeforeCheckGraphElementEventArgs<TGraphElement>? RaiseBeforeCheckGraphElementEvent(TGraphElement graphElement, bool checkValue, bool canCheck)
        {
            if (this.BeforeCheckGraphElement != null)
            {
                BeforeCheckGraphElementEventArgs<TGraphElement> checkGraphElementArgs = new BeforeCheckGraphElementEventArgs<TGraphElement>(graphElement, checkValue, canCheck);
                
                this.BeforeCheckGraphElement(this, checkGraphElementArgs);

                return checkGraphElementArgs;
            }
            else
            {
                return null;
            }
        }

		protected void RaiseAfterCheckGraphElementEvent(TGraphElement graphElement, bool checkValue)
		{
			this.AfterCheckGraphElement?.Invoke(this, new CheckGraphElementEventArgs<TGraphElement>(graphElement, checkValue));
		}

		#endregion |   Private Raise Event Methods   |

		#region |   Private Methods   |

		private void Initialize()
        {
            if (!this.isInitialized)
            {
                if (this.GraphControl is not null)
                {
                    if (this.ImageList is not null)
                        this.GraphControl.SetImageList(this.ImageList);

                    if (this.StateImageList is not null)
                        this.GraphControl.SetStateImageList(this.StateImageList);

                    if (this.CheckBoxImageList is not null)
                        this.GraphControl.SetCheckBoxImageList(this.CheckBoxImageList);

                    this.GraphControl.SetEditMode(this.EditMode);
                    this.GraphControl.SetLookAndFeelStyle(this.LookAndFeelStyle);
                    this.GraphControl.SetCheckBoxMode(this.ShowCheckBoxes);
                    this.GraphControl.SetCanDragAndDrop(this.CanDragAndDrop);
                    this.GraphControl.SetRecursiveNodeChecking(this.allowRecursiveNodeChecking);
                }

                this.SetButtonsSortingEnableProperty();
                this.isInitialized = true;
				this.RaiseAfterinitializeControl();
            }
        }

        private void SetButtonsEnableProperty(object? node)
        {
            bool enabled;
            TGraphElement? graphElement = (node is not null) ? this.GetGraphElement(node) : default;

            //GraphElement parentGraphElement = graphElement == null ? null : graphElement.Parent;

            //// Add Folder buttons
            //enabled = this.Manager.CanAddChildGraphElement(this.Graph, typeof(Folder), graphElement);
            //foreach (Component buttonAddFolder in this.ButtonAddFolderList)
            //{
            //    this.SetButtonEnableProperty(buttonAddFolder, enabled);
            //}

            //// Add Sub Folder buttons
            //enabled = graphElement != null ? this.Manager.CanAddChildGraphElement(this.Graph, typeof(Folder), graphElement.Parent) : false;
            //foreach (Component buttonAddSubFolder in this.ButtonAddSubFolderList)
            //{
            //    this.SetButtonEnableProperty(buttonAddSubFolder, enabled);
            //}

            // Add buttons
            foreach (AddButtonPolicy<TGraphElement> addButtonPolicy in this.AddButtonPolicyList)
            {
                TGraphElement? parentGraphElementForInsertion = this.GetParentGraphElementForNewGraphInsertion(graphElement, addButtonPolicy);

                if (parentGraphElementForInsertion != null && parentGraphElementForInsertion.Equals(this.anchorGraphElement) && addButtonPolicy.IsSubButton)
                    enabled = false;
                else
                    enabled = this.CanAddGraphElement(addButtonPolicy.ObjectType, parentGraphElementForInsertion);

                foreach (Component button in addButtonPolicy.Buttons)
                    this.SetButtonEnablePropertyInternal(button, enabled);
            }

            // MoveUp/Down buttons
            this.SetButtonsMoveUpEnableProperty(graphElement);
            this.SetButtonsMoveDownEnableProperty(graphElement);

            // Save buttons
            this.SetBattonsSaveEnableProperty(graphElement);

            // Delete buttons
            enabled = graphElement != null && this.CanDeleteGraphElement(graphElement);

            foreach (Component buttonDelete in this.ButtonRemoveList)
                this.SetButtonEnablePropertyInternal(buttonDelete, enabled);
        }

		private void SetImagesIfIsNotLoaded()
		{
			if (!this.isImagesLoaded && this.ImageList != null)
			{
				if (this.ImageList.Images.Count == 0)
					this.ImageList = ImageControl.SmallImageCollection;

				//ImageControl.LoadImages(this.ImageList, ImageControl.SmallImageCollection);
				this.isImagesLoaded = true;
			}

			if (!this.isStateImagesLoaded && this.StateImageList != null)
			{
				if (this.StateImageList.Images.Count == 0)
					this.StateImageList = ImageControl.StateImageCollection;

				//ImageControl.LoadImages(this.StateImageList, ImageControl.StateImageCollection);
				this.isStateImagesLoaded = true;
			}

			if (!this.isCheckBoxImagesLoaded && this.CheckBoxImageList != null)
			{
				if (this.CheckBoxImageList.Images.Count == 0)
					this.CheckBoxImageList = ImageControl.CheckBoxImageCollection;

				//ImageControl.LoadImages(this.CheckBoxImageList, ImageControl.CheckBoxImageCollection);
				this.isCheckBoxImagesLoaded = true;
			}
		}

		private void Load(TGraphElement graphElement, TGraphElement? parentGraphElement)
        {
            object node = this.AddNodeInternal(graphElement, parentGraphElement);
            GraphNodeTag<TGraphElement>? graphNodeTag = this.GetGraphNodeTag(node);
            IEnumerable<TGraphElement> childGraphElements = this.GetChildGraphElements(graphElement);

            this.UpdateNodeInternal(graphElement, node);
            this.RaiseGraphElementCreated(graphElement);

            if (this.LoadAllNodes)
            {
                foreach (TGraphElement childGraphElement in childGraphElements)
                    this.Load(childGraphElement, graphElement);

                if (graphNodeTag != null)
                    graphNodeTag.ChildrenNodesLoaded = true;
            }
            else
            {
                bool nodeHasChildren = childGraphElements != null && childGraphElements.Count() > 0;

                this.GraphControl?.SetNodeHasChildrenProperty(node, nodeHasChildren);
                
                if (graphNodeTag != null)
                    graphNodeTag.ChildrenNodesLoaded = !nodeHasChildren;
            }
        }

        private bool AddNewObject(AddButtonPolicy<TGraphElement> addButtonPolicy, TGraphElement parentGraphElement, object requester)
        {
            return this.AddNewObject(addButtonPolicy, parentGraphElement, this.Columns[0], requester);
        }

        private bool AddNewObject(AddButtonPolicy<TGraphElement> addButtonPolicy, TGraphElement parentGraphElement, GraphColumn focusedColumn, object requester)
        {
            bool canAdd = this.CanAddGraphElement(addButtonPolicy.ObjectType, parentGraphElement);

            if (canAdd)
            {
                TGraphElement? newGraphElement = this.CreateNewGraphElement(addButtonPolicy.ObjectType, parentGraphElement, requester);
                object? newNode = null;

                this.BeginUpdate();

                if (newGraphElement != null)
                {
                    if (addButtonPolicy.ObjectAction != null)
                    {
                        addButtonPolicy.ObjectAction(newGraphElement);
                        newNode = this.GetNode(newGraphElement);
                    }

                    if (newNode == null)
                        newNode = this.AddNodeInternal(newGraphElement, parentGraphElement);

                    this.UpdateNodeInternal(newGraphElement, newNode);
                    this.RaiseGraphElementCreated(newGraphElement);
                }

                this.EndUpdate();

                if (newNode != null)
                {
                    GraphNodeTag<TGraphElement>? newGraphNodeTag = this.GetGraphNodeTag(newNode);

                    if (newGraphNodeTag != null)
                        newGraphNodeTag.ChildrenNodesLoaded = true;

                    this.GraphControl?.SetFocusedNode(newNode);
                }

                this.GraphControl?.SetFocusedColumn(focusedColumn.Index);
                this.GraphControl?.ShowEditor();
            }

            return canAdd;
        }

        private GraphValidationResult ValidateNode(object node)
        {
            GraphValidationResult result;
            GraphColumn errorColumn = null;
            int errorColumnIndex = -1;
            TGraphElement graphElement = this.GetGraphElement(node);
			ValidationResult objectValidationResult = this.ValidateGraphElement(graphElement);
            SimpleObjectValidationResult simpleObjectValidationResult = null;

            if (objectValidationResult is SimpleObjectValidationResult)
            {
                simpleObjectValidationResult = objectValidationResult as SimpleObjectValidationResult;
            }
            else
            {
                simpleObjectValidationResult = new SimpleObjectValidationResult(graphElement as SimpleObject, objectValidationResult.Passed, objectValidationResult.Message,
                                                                                new ValidationResult(objectValidationResult.Passed, objectValidationResult.Message), TransactionRequestAction.Save);
            }

            if (!simpleObjectValidationResult.Passed)
            {
                errorColumn = null;

                if (simpleObjectValidationResult.FailedRuleResult != null && simpleObjectValidationResult.FailedRuleResult is PropertyValidationResult)
                {
					IPropertyModel errorPropertyModel = (simpleObjectValidationResult.FailedRuleResult as PropertyValidationResult).ErrorPropertyModel;
                    object bindingObject = this.GetBindingObject(graphElement);
                    GraphColumnBindingPolicy<TGraphElement> graphColumnBindingPolicy = this.GetGraphColumnBindingPolicyByObjectPropertyIndex(bindingObject.GetType(), errorPropertyModel.PropertyIndex);

                    if (graphColumnBindingPolicy != null)
                        errorColumn = graphColumnBindingPolicy.GraphColumn;
                }

                if (errorColumn != null)
                    errorColumnIndex = errorColumn.Index;
            }

            result = new GraphValidationResult(simpleObjectValidationResult, node, errorColumnIndex);

            return result;
        }

        protected void SetValidation(GraphValidationResult graphValidationResult)
        {
            if (!graphValidationResult.Passed && graphValidationResult.ErrorGraphColumnIndex >= 0)
            {
                this.SetGraphValidationError(graphValidationResult);
            }
            else if (this.isGraphErrorMessageSet)
            {
                this.GraphControl.ResetColumnErrors();
                this.isGraphErrorMessageSet = false;
            }

            this.RaiseBindingObjectOnValidation(graphValidationResult);

            if (!graphValidationResult.Passed && graphValidationResult.ErrorGraphColumnIndex >= 0 && !graphValidationResult.Message.IsNullOrEmpty() && !graphValidationResult.IsValidationErrorFormShown)
            {
                XtraMessageBox.Show(graphValidationResult.Message, "Validation Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                graphValidationResult.IsValidationErrorFormShown = true;

                bool columnEnabled = this.GraphControl.GetColumnEnableProperty(graphValidationResult.ErrorGraphColumnIndex);

                if (columnEnabled)
                {
                    this.GraphControl.Focus();
                    this.GraphControl.ShowEditor();
                }

                //return;
            }

            if (!graphValidationResult.Passed)
                this.lastValidationFailedNode = graphValidationResult.GraphNode;

            this.lastValidationNodeResult = graphValidationResult;
        }


        private bool SaveNode(object node)
        {
            TGraphElement? graphElement = this.GetGraphElement(node);
            bool isSaved = false;
            
            if (graphElement is not null)
                isSaved = this.SaveGraphElement(graphElement);
            //isSaved &= this.Manager.Save(requester, graphElement);

            if (isSaved)
                this.SetButtonsSaveEnableProperty(false);

            return isSaved;
        }

        //private void DeleteNode(object node)
        //{
        //    TGraphElement graphElement = this.GetGraphElement(node);
        //    this.DeleteGraphElement(graphElement);
        //}

        private object AddNodeInternal(TGraphElement graphElement, TGraphElement? parentGraphElement)
        {
            object parentNode = null;

            if (parentGraphElement != null)
                parentNode = this.GetNode(parentGraphElement);

            GraphNodeTag<TGraphElement> nodeTag = new GraphNodeTag<TGraphElement>(graphElement);
            object node = this.GraphControl.AddNode(parentNode, nodeTag);
            this.nodesByGraphElement.Add(graphElement, node);

            if (this.GraphControl.GetParentNode(node) == null)
                this.rootGraphElements.Add(graphElement);

            if (this.ImageList != null)
            {
                string imageName = this.GetGraphElementImageName(graphElement);
                int imageIndex = this.ImageList.Images.IndexOfKey(imageName);
                nodeTag.ImageIndex = imageIndex;
                this.GraphControl.SetNodeImageIndex(node, imageIndex);

                string expandedImageName = imageName + "." + strExpanded;
                int expandedImageIndex = this.ImageList.Images.IndexOfKey(expandedImageName);
                nodeTag.ExpandedImageIndex = expandedImageIndex;
                this.GraphControl.SetNodeExpandedImageIndex(node, expandedImageIndex);
            }

            if (this.ShowCheckBoxes)
                this.GraphControl.SetNodeCheckBoxImageIndex(node, this.CheckBoxUncheckedImageIndex);

            return node;
        }

        private void RemoveNodeInternal(object node)
        {
            TGraphElement graphElement = this.GetGraphElement(node);
            this.RemoveNodeInternal(graphElement, node);
        }

        private void RemoveNodeInternal(TGraphElement graphElement)
        {
            object node = this.GetNode(graphElement);
            this.RemoveNodeInternal(graphElement, node);
        }

        private void RemoveNodeInternal(TGraphElement graphElement, object node)
        {
            //GraphNodeTag<TGraphElement> nodeTag = this.GetGraphNodeTag(node);

            this.nodesByGraphElement.Remove(graphElement);

            if (node != null)
            {
                object parentNode = this.GraphControl.GetParentNode(node);

                if (parentNode == null)
                    this.rootGraphElements.Remove(graphElement);
            }

            if (node != null)
                this.GraphControl.RemoveNode(node);
            //nodeTag.GraphElement = default(TGraphElement);
        }

        protected void UpdateNodeInternal(TGraphElement graphElement, object node)
        {
            object bindingObject = this.GetBindingObject(graphElement);
            bool isReloaded = this.ReloadNodeByEvent(graphElement, node);

            if (!isReloaded)
                foreach (GraphColumn graphColumn in this.Columns)
                    this.UpdateNodeColumnValueInternal(graphElement, node, bindingObject.GetType(), graphColumn, propertyIndex: -1);

            this.RaiseAfterReloadNode(graphElement, node);
        }

        //     private void UpdateNodeColumnValueInternal(object node, TGraphElement graphElement, int propertyIndex, string columnOrPropertyName)
        //     {
        //         object bindingObject = this.GetBindingObject(graphElement);

        //if (bindingObject != null)
        //{
        //	Type objectType = bindingObject.GetType();
        //	IEnumerable<GraphColumn> graphColumns = this.GetColumnBindingPolicyByObjectPropertyIndex(objectType, propertyIndex, columnOrPropertyName);

        //	foreach (GraphColumn graphColumn in graphColumns)
        //		this.UpdateNodeColumnValueInternal(node, graphElement, objectType, graphColumn, propertyIndex);
        //}
        //     }

        private void UpdateNodeColumnValueInternal(TGraphElement graphElement, object node, Type objectType, GraphColumn graphColumn, int propertyIndex)
        {
            object? editValue = null;

            GraphColumnBindingPolicy<TGraphElement>? graphColumnBindingPolicy = this.GetGraphColumnBindingPolicyByColumnIndex(objectType, graphColumn.Index);

            if (graphColumnBindingPolicy != null && graphColumnBindingPolicy.BindingOption == BindingOption.EnableEditing && !this.ColumnNamesWithDisabledBinding.Contains(graphColumn.Name) && this.GraphControl is not null)
            {
                object? propertyValue = null;

                if (graphElement is IBindingSimpleObject bindingSimpleObject)
                    propertyValue = this.EditorBindings.GetPropertyValue(bindingSimpleObject, graphColumnBindingPolicy.PropertyModel);

                if (propertyValue == null)
                    propertyValue = this.GetGraphElementPropertyValue(graphElement, graphColumnBindingPolicy.PropertyModel.PropertyIndex);

                Component? editorComponent = this.GraphControl?.GetEditorComponent(graphColumn.Index);

                if (!this.EditorBindings.TryGetControlValueFromPropertyValue(objectType, graphColumnBindingPolicy.PropertyModel, propertyValue, editorComponent, out editValue))
                {
                    if (graphColumnBindingPolicy.GetEditValue != null)
                    {
                        editValue = graphColumnBindingPolicy.GetEditValue(graphElement);
                    }
                    else
                    {
                        editValue = propertyValue;
                    }
                }
            }
            else if (propertyIndex >= 0)
            {
                //if (graphElement is IBindingSimpleObject)
                //{
                //	IPropertyModel propertyModel = (graphElement as IBindingSimpleObject).GetModel().PropertyModel.GetModel(propertyIndex);

                //	if (propertyModel != null)
                //		editValue = this.EditorBindings.GetPropertyValue(graphElement as IBindingSimpleObject, propertyModel.Index);
                //}
                //else
                //{
                //	IPropertyModel propertyModel = graphElement.PropertyModel.GetModel(propertyIndex);

                //	if (propertyModel != null)

                editValue = graphElement.GetPropertyValue(propertyIndex);
                //}
            }
            else
			{
                editValue = String.Empty; // .GetPropertyValue(graphColumn.Name);

            }

            editValue = this.GetEditValueByEvent(graphElement, node, graphColumn, propertyIndex, editValue);

            this.GraphControl?.CloseEditor();
            this.GraphControl?.SetCellEditValue(node, graphColumn.Index, editValue);
        }

        private void RefreshNode(TGraphElement graphElement, object node)
        {
            if (node != null)
            {
                this.BeginUpdate();
                this.UpdateNodeInternal(graphElement, node);
                this.EndUpdate();

                //this.RaiseBindingObjectRefreshContext(graphElement.SimpleObject);
            }
        }

        private void SetColumnsEnableProperty(object node)
        {
            TGraphElement? graphElement = default;

            if (node != null)
            {
                graphElement = this.GetGraphElement(node);

                if (!Comparer.Equals(default(TGraphElement), graphElement) && graphElement is not null && this.Columns is not null)
                {
                    object? bindingObject = this.GetBindingObject(graphElement);
                    Type? objectType = bindingObject?.GetType();

                    foreach (GraphColumn graphColumn in this.Columns)
                    {
                        bool columnEnabled = (objectType != null) ? this.GetColumnEnableProperty(objectType, graphColumn) : false;
                        this.GraphControl?.SetColumnEnableProperty(graphColumn.Index, columnEnabled);
                    }
                }
            }
            else if (this.Columns is not null)
            {
				foreach (GraphColumn graphColumn in this.Columns)
					this.GraphControl?.SetColumnEnableProperty(graphColumn.Index, false);
			}

            if (!Comparer.Equals(default(TGraphElement), graphElement) && graphElement is not null)
                this.RaiseAfterSetGraphColumnsEnableProperty(graphElement);
        }

        private bool GetColumnEnableProperty(Type objectType, GraphColumn graphColumn)
        {
            bool columnEnabled = false;
            GraphColumnBindingPolicy<TGraphElement>? graphColumnBindingPolicy = this.GetGraphColumnBindingPolicyByColumnIndex(objectType, graphColumn.Index);

            if (graphColumnBindingPolicy != null && graphColumnBindingPolicy.BindingOption == BindingOption.EnableEditing && graphColumnBindingPolicy.PropertyModel.AccessPolicy != PropertyAccessPolicy.ReadOnly)
                columnEnabled = true;

            return columnEnabled;
        }

        private TGraphElement? GetParentGraphElementForNewGraphInsertion(TGraphElement? graphElement, AddButtonPolicy<TGraphElement> addButtonPolicy)
        {
            TGraphElement? anchorGraphElement = graphElement != null ? graphElement : this.anchorGraphElement;
            TGraphElement? result = anchorGraphElement;

            // TODO: Provjeriti zašto je ponekad anchorGraphElement != null a anchorGraphElement.SimpleObject == null !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (anchorGraphElement != null && this.GetBindingObject(anchorGraphElement) != null)
            {
                TGraphElement? parentGraphElement = graphElement != null ? this.GetParentGraphElement(graphElement) : this.anchorGraphElement;
                Type bindingObjectType = this.GetBindingObject(anchorGraphElement).GetType();

                if (addButtonPolicy.AddButtonPolicyOption == AddButtonPolicyOption.AddAsNeighbor)
                    result = parentGraphElement;

                // TODO:

                //// If focused graph element and button are the same type -> set parent graph element as anchor if graph policy model do not allow sub tree creation for same type
                //else if (bindingObjectType == addButtonPolicy.ObjectType)
                //{
                //    // Set parent graph element as anchor if 
                //    IGraphPolicyModel graphPolicyModel = this.Manager.GetGraphPolicyModel(this.GraphKey);
                //    IGraphPolicyModel graphPolicyModelAddButtonObjectType = graphPolicyModel.PolicyElements.FirstOrDefault(pe => pe.ObjectType == addButtonPolicy.ObjectType);

                //    if (graphPolicyModelAddButtonObjectType != null && graphPolicyModelAddButtonObjectType.SubTreePolicy == SubTreePolicy.DoNotAllowSubTree)
                //    {
                //        result = parentGraphElement;
                //    }
                //}
                //else // check are the focused graph element and button has the same declared priority
                //{
                //    IGraphPolicyModel graphPolicyModel = this.Manager.GetGraphPolicyModel(this.GraphKey);
                //    IGraphPolicyModel graphPolicyModelGraphElement = graphPolicyModel.PolicyElements.FirstOrDefault(pe => pe.ObjectType == graphElementSimpleObjectType);
                //    IGraphPolicyModel graphPolicyModelAddButtonObjectType = graphPolicyModel.PolicyElements.FirstOrDefault(pe => pe.ObjectType == addButtonPolicy.ObjectType);

                //    // If button and graph elemnt priority are the same -> Set parent graph element as anchor
                //    if (graphPolicyModelGraphElement.Priority == graphPolicyModelAddButtonObjectType.Priority)
                //    {
                //        result = parentGraphElement;
                //    }
                //    else if (parentGraphElement == null && graphPolicyModelAddButtonObjectType.Priority < graphPolicyModelGraphElement.Priority)
                //    {
                //        result = parentGraphElement;
                //    }
                //}
            }

            // TODO:
            //if (result != null && result.IsDeleting)
            //    result = null;

            return result;
        }

        private void SetBattonsSaveEnableProperty(TGraphElement graphElement)
        {
            bool enabled = this.DoesGraphElementRequireSaving(graphElement);
            this.SetButtonsSaveEnableProperty(enabled);
        }

        private void SetButtonsSaveEnableProperty(bool enabled)
        {
            foreach (Component buttonSave in this.buttonSaveList)
                this.SetButtonEnablePropertyInternal(buttonSave, enabled);
        }

        private void SetButtonEnablePropertyInternal(Component button, bool enabled)
        {
            this.GraphControl.SetButtonEnableProperty(button, enabled);
            this.RaiseOnSetButtonEnableProperty(button);
        }

        //private void BindingObjectHasChangeFocus(IBindingObject oldBindingObject, IBindingObject bindingObject)
        //{
        //    if (this.IsCheckBoxMode)
        //    {
        //        // First remove checkings from old binding object.
        //        while (this.CheckedNodes.Count > 0)
        //        {
        //            object node = this.CheckedNodes[0];
        //            this.SetGraphNodeCheckedProperty(node, false);
        //        }

        //        // Than set checkings for current binding object
        //        if (bindingObject != null)
        //        {
        //            IList focusedBindingObjectManyToManyObjectCollection = (bindingObject as IBindingSimpleObject).GetManyToManyObjectCollection(this.ManyToManyRelationKey);

        //            foreach (SimpleObject simpleObject in focusedBindingObjectManyToManyObjectCollection)
        //            {
        //                GraphElement graphElement = simpleObject.GetGraphElement(this.Graph);

        //                if (graphElement != null)
        //                {
        //                    object node = this.GetNode(graphElement);

        //                    this.SetGraphNodeCheckedProperty(node, true);
        //                }
        //            }
        //        }
        //    }
        //}

        private void SetGraphNodeChecked(object node, bool value)
        {
            GraphNodeTag<TGraphElement>? nodeTag = this.GetGraphNodeTag(node);

            if (nodeTag != null && !nodeTag.CheckDisabled && nodeTag.Checked != value)
            {
                nodeTag.Checked = value;
                this.GraphControl?.SetNodeCheckedProperty(node, value);

                if (nodeTag.Checked)
                    this.CheckedNodes.Add(node);
                else
                    this.CheckedNodes.Remove(node);

                this.SetNodeCheckBoxImage(node);

                object? parentNode = this.GraphControl?.GetParentNode(node);

                if (parentNode != null)
                {
                    int increasingCheckCountValue = nodeTag.Checked ? 1 : -1;
                    this.IncreaseChildNodesCheckCount(parentNode, increasingCheckCountValue);
                }

                this.RaiseAfterCheckGraphElementEvent(nodeTag.GraphElement, value);

                if (this.AllowRecursiveNodeChecking)
                {
                    ICollection? childNodes = this.GetChildNodes(node);

                    if (childNodes != null)
                        foreach (object childNode in childNodes)
                            this.SetGraphNodeChecked(childNode, value);
                }
            }
        }

		private void SetGraphNodeCheckDisabled(object node, bool value)
        {
            GraphNodeTag<TGraphElement>? nodeTag = this.GetGraphNodeTag(node);

            if (nodeTag != null && nodeTag.CheckDisabled != value)
            {
                nodeTag.CheckDisabled = value;
                this.SetNodeCheckBoxImage(node);
            }
        }

        private void IncreaseChildNodesCheckCount(object node, int increasingCheckCountValue)
        {
            GraphNodeTag<TGraphElement> nodeTag = this.GetGraphNodeTag(node);
            nodeTag.ChildrendNodesCheckCount += increasingCheckCountValue;
            this.SetNodeCheckBoxImage(node);

            object parentNode = this.GraphControl.GetParentNode(node);

            if (parentNode != null)
                this.IncreaseChildNodesCheckCount(parentNode, increasingCheckCountValue);
        }

        private void SetNodeCheckBoxImage(object node)
        {
            int imageIndex = -1;
            GraphNodeTag<TGraphElement> nodeTag = this.GetGraphNodeTag(node);

            if (nodeTag.Checked)
            {
                if (nodeTag.CheckDisabled)
                    imageIndex = nodeTag.ChildrendNodesCheckCount > 0 ? this.CheckBoxCheckedChildCheckedGreyedImageIndex : this.CheckBoxCheckedGreyedImageIndex;
                else
                    imageIndex = nodeTag.ChildrendNodesCheckCount > 0 ? this.CheckBoxCheckedChildCheckedImageIndex : this.CheckBoxCheckedImageIndex;
            }
            else
            {
                if (nodeTag.CheckDisabled)
                    imageIndex = nodeTag.ChildrendNodesCheckCount > 0 ? this.CheckBoxUncheckedChildCheckedGreyedImageIndex : this.CheckBoxUncheckedGreyedImageIndex;
                else
                    imageIndex = nodeTag.ChildrendNodesCheckCount > 0 ? this.CheckBoxUncheckedChildCheckedImageIndex : this.CheckBoxUncheckedImageIndex;
            }

            this.GraphControl.SetNodeCheckBoxImageIndex(node, imageIndex);
        }

        private void SetGraphValidationError(GraphValidationResult graphValidationResult)
        {
            this.GraphControl.SetColumnError(graphValidationResult.ErrorGraphColumnIndex, graphValidationResult.Message);

            this.isGraphErrorMessageSet = true;
            this.GraphControl.SetFocusedColumn(graphValidationResult.ErrorGraphColumnIndex);

            bool columnEnabled = this.GraphControl.GetColumnEnableProperty(graphValidationResult.ErrorGraphColumnIndex);

            if (columnEnabled)
            {
                this.GraphControl.Focus();
                this.GraphControl.ShowEditor();
            }
        }

        private bool GraphElementHasAsParent(TGraphElement graphElement, TGraphElement parentGraphElementCandidate)
        {
            if (graphElement == null)
                return false;

            bool result = false;
            TGraphElement? parent = this.GetParentGraphElement(graphElement);

            while (!result && parent != null)
            {
                result = (parent.Equals(parentGraphElementCandidate));
                parent = this.GetParentGraphElement(parent);
            }

            return result;
        }

        private GraphColumnBindingPolicy<TGraphElement>? CreateDefaultGraphColumnBindingPolicyByColumnIndex(Type objectType, int columnIndex)
        {
            GraphColumnBindingPolicy<TGraphElement>? result = null;
            GraphColumn? graphColumn = this.Columns[columnIndex];

            if (graphColumn is null)
                return null;

            IPropertyModel? propertyModel = this.GetBindingObjectPropertyModel(objectType, graphColumn.Name);

            if (propertyModel != null)
                result = new GraphColumnBindingPolicy<TGraphElement>(propertyModel, graphColumn);

            return result;
        }

        private GraphColumnBindingPolicy<TGraphElement>? CreateDefaultGraphColumnBindingPolicyByPropertyIndex(Type objectType, int propertyIndex)
        {
            GraphColumnBindingPolicy<TGraphElement>? result = null;
            IPropertyModel? propertyModel = this.GetBindingObjectPropertyModel(objectType, propertyIndex);

            if (propertyModel != null)
            {
                GraphColumn? graphColumn = this.GetGraphColumn(propertyModel.PropertyName);

                if (graphColumn != null)
                    result = new GraphColumnBindingPolicy<TGraphElement>(propertyModel, graphColumn);
            }

            return result;
        }


        //private Form FindGraphControlForm()
        //{
        //    Form result = null;

        //    if (this.GraphControl != null)
        //    {
        //        result = this.GraphControl.FindForm();
        //    }

        //    return result;
        //}

        #endregion |   Private Methods   |

        #region |   Event Calls   |

        private void changableBindingObjectControl_BindingObjectChange(object sender, BindingObjectEventArgs e)
        {
            Form? form = this.FindGraphControlForm();

            if (form != null && form.InvokeRequired)
                form.Invoke(new MethodInvoker(() => this.OnBindingObjectChange(e)));
            else
                this.OnBindingObjectChange(e);
        }

        protected virtual void OnBindingObjectChange(BindingObjectEventArgs e)
        {
            if (this.CommitChangeOnFocusedNodeChange && this.FocusedGraphElement != null && this.DoesGraphElementRequireSaving(this.FocusedGraphElement))
                this.SaveGraphElement(this.FocusedGraphElement);

            //if (this.FocusedGraphElement != null && this.FocusedGraphElement is SimpleObject)
            //    (this.FocusedGraphElement as SimpleObject).Save();

            this.BeginUpdate();
            this.ClearNodes();

            //object bindingObject = this.GetBindingObject((TGraphElement)e.SimpleObject);

            //if (bindingObject is SimpleObject)
            //{
            this.changableBindingObjectControlFocusedBindingObject = e.BindingObject as SimpleObject;
            //}
            //else
            //{
            //	this.ChangableBindingObjectControlFocusedBindingObject = null;
            //}

            if (this.ChangableBindingObjectControlFocusedBindingObject != null)
            {
                IEnumerable<TGraphElement> rootElements = this.GetChildGraphElements(default(TGraphElement));
                
                this.Bind(rootElements);
                this.BestFitColumns();
            }

            this.EndUpdate();
        }

        private void changableBindingObjectControl_BindingObjectRelationForeignObjectSet(object sender, BindingObjectRelationForeignObjectSetEventArgs e)
        {
            Form? form = this.FindGraphControlForm();

            if (form != null && form.InvokeRequired)
                form.Invoke(new MethodInvoker(() => this.OnBindingObjectRelationForeignObjectSet(e)));
            else
                this.OnBindingObjectRelationForeignObjectSet(e);
        }

        protected virtual void OnBindingObjectRelationForeignObjectSet(BindingObjectRelationForeignObjectSetEventArgs e)
        {
            if (e.Requester != this && e.Requester != this.EditorBindings)
                this.RefreshGraphElement((TGraphElement)e.BindingObject);
        }

        private void changableBindingObjectControl_BindingObjectRefreshContext(object sender, ChangePropertyValueBindingObjectEventArgs e)
        {
            Form? form = this.FindGraphControlForm();

            if (form != null && form.InvokeRequired)
                form.Invoke(new MethodInvoker(() => this.OnBindingObjectRefreshContext(e)));
            else
                this.OnBindingObjectRefreshContext(e);
        }

        protected virtual void OnBindingObjectRefreshContext(ChangePropertyValueBindingObjectEventArgs e)
        {
			// this.RefreshContext(this.ChangableBindingObjectControlFocusedBindingObject, e.BindingObject);
			this.RaiseBindingObjectRefreshContext(e);
        }

		private void RaiseAfterinitializeControl()
		{
			this.AfterInitializeControl?.Invoke(this, new EventArgs());
		}

        internal void RaiseBindingObjectRefreshContext(ChangePropertyValueBindingObjectEventArgs changePropertyValueBindingObjectRequesterEventArgs)
        {
			this.BindingObjectRefreshContext?.Invoke(this, changePropertyValueBindingObjectRequesterEventArgs);
        }

        private void RaiseBindingObjectStoreData(object? bindingObject)
        {
			this.BindingObjectPushData?.Invoke(this, new BindingObjectEventArgs(bindingObject));
        }

        private void RaiseBindingObjectOnValidation(GraphValidationResult graphValidationResult)
        {
             this.BindingObjectOnValidation?.Invoke(this, new GraphValidationResultEventArgs(graphValidationResult));
        }

        //#region |   IBindingObjectControl Interface   |

        ////bool IBindingObjectControl.SaveBindingObject(object requester, IBindingObject bindingObject)
        ////{
        ////    bool result = true;
        ////    object node = this.GetFocusedNode();
        ////    GraphElement graphElement = this.GetGraphElementByNode(node);

        ////    if (graphElement != null)
        ////    {
        ////        SimpleObject bussinessObject = graphElement.SimpleObject;

        ////        if (bussinessObject != null && bindingObject is SimpleObject && bindingObject as SimpleObject == bussinessObject)
        ////        {
        ////            result = this.ValidateAndTrySaveNodeIfRequired(requester, node);
        ////        }
        ////    }

        ////    return result;
        ////}

        ////void IBindingObjectControl.RefreshBindingObjectContext(object requester, IBindingObject bindingObject)
        ////{
        ////    GraphElement graphElement = (bindingObject as SimpleObject).GetGraphElement(this.Graph);
        ////    this.RefreshGraphElement(graphElement);

        ////    if (this.ChangableBindingObjectControl != null)
        ////    {
        ////        this.ChangableBindingObjectControl.RefreshBindingObjectContext(requester, bindingObject);
        ////    }
        ////}

        //#endregion |   IBindingObjectControl Interface   |

        #endregion |   Event Calls   |

        #region |   IGraphControllerControl Interface   |

        void IGraphController.RemoveColumn(int columnIndex)
        {
            this.GraphControl?.RemoveColumn(columnIndex);
        }

        int IGraphController.AddColumn(string columnName, string caption, BindingDataType dataType, BindingEditorType editorType)
        {
            return this.GraphControl!.AddColumn(columnName, caption, dataType, editorType);
        }

        int IGraphController.GetColumnWidth(int columnIndex)
        {
            return this.GraphControl!.GetColumnWidth(columnIndex);
        }

        void IGraphController.SetColumnWidth(int columnIndex, int width)
        {
            this.GraphControl!.SetColumnWidth(columnIndex, width);
        }

        bool IGraphController.GetColumnEnableProperty(int columnIndex)
        {
            return this.GraphControl!.GetColumnEnableProperty(columnIndex);
        }

        void IGraphController.SetColumnEnableProperty(int columnIndex, bool enable)
        {
            this.GraphControl!.SetColumnEnableProperty(columnIndex, enable);
        }

        bool IGraphController.GetColumnVisibleProperty(int columnIndex)
        {
            return this.GraphControl!.GetColumnVisibleProperty(columnIndex);
        }

        void IGraphController.SetColumnVisibleProperty(int columnIndex, bool enable)
        {
            this.GraphControl!.SetColumnVisibleProperty(columnIndex, enable);
        }

        bool IGraphController.GetColumnShowInCustomizationFormProperty(int columnIndex)
        {
            return this.GraphControl!.GetColumnShowInCustomizationFormProperty(columnIndex);
        }

        void IGraphController.SetColumnShowInCustomizationFormProperty(int columnIndex, bool enable)
        {
            this.GraphControl!.SetColumnShowInCustomizationFormProperty(columnIndex, enable);
        }

        void IGraphController.SetColumnCaption(int columnIndex, string caption)
        {
            this.GraphControl!.SetColumnCaption(columnIndex, caption);
        }

        void IGraphController.SetColumnDataType(int columnIndex, BindingDataType dataType)
        {
            this.GraphControl!.SetColumnDataType(columnIndex, dataType);
        }

        void IGraphController.SetColumnEditorType(int columnIndex, BindingEditorType editorType)
        {
            this.GraphControl!.SetColumnEditorType(columnIndex, editorType);
        }

        void IGraphController.BeforeNodeIsFocused(object node, object oldNode, ref bool canFocus)
        {
            if (this.EditMode == GraphEditMode.Select || this.EditMode == GraphEditMode.ViewOnly)
                return;

            if (oldNode != null)
            {
                GraphNodeTag<TGraphElement>? oldNodeTag = this.GraphControl?.GetGraphNodeTag(oldNode) as GraphNodeTag<TGraphElement>;

                // If deleting node is in progress, oldNodeTag.GraphElement is null.
                if (oldNodeTag != null && oldNodeTag.GraphElement != null && !(oldNodeTag.GraphElement is SimpleObject simpleObject && simpleObject.DeleteStarted))
                    canFocus = this.ValidateAndTrySaveNodeIfRequired(oldNode);

                //                this.lastValidationFailedNode = canFocus ? null : oldNode;
            }
            else
            {
                if (this.lastValidationFailedNode != null)
                {
                    this.ValidateAndTrySaveNodeIfRequired(this.lastValidationFailedNode);
                    canFocus = this.lastValidationFailedNode == node ? true : canFocus;
                }
            }
        }

        void IGraphController.FocusedNodeIsChanged(object node, object oldNode)
        {
            if (this.EditMode == GraphEditMode.Select || this.EditMode == GraphEditMode.ViewOnly)
                return;

            this.SetColumnsEnableProperty(node);
            this.SetButtonsEnableProperty(node);

            TGraphElement? graphElement = this.GetGraphElement(node);
            object? bindingObject = this.GetBindingObject(graphElement);

            this.RaiseBindingObjectChange(bindingObject);
		}

		void IGraphController.CellValueIsChanged(object node, int columnIndex, object value)
        {
            TGraphElement? graphElement = this.GetGraphElement(node);

			if (!Comparer.Equals(default(TGraphElement), graphElement) && graphElement is not null)
			{
				Type? objectType = this.GetBindingObject(graphElement)?.GetType();
                GraphColumnBindingPolicy<TGraphElement>? columnBindingPolicy = this.GetGraphColumnBindingPolicyByColumnIndex(objectType, columnIndex);

                // if no EditorBindings handle of this property than set editor property value (if EditorBindings contains property binding no any action needed)
                if (columnBindingPolicy != null && columnBindingPolicy.PropertyModel.AccessPolicy != PropertyAccessPolicy.ReadOnly &&
                    !this.EditorBindings.ContainsPropertyBinding(objectType, columnBindingPolicy.PropertyModel))
                {
                    object? propertyValue = value;
                    Component? editorComponent = this.GraphControl?.GetEditorComponent(columnIndex);

                    if (editorComponent != null)
                        this.EditorBindings.TryGetPropertyValueFromControlValue(value, editorComponent, out propertyValue);

                    object normalizedPropertyValue = Conversion.TryChangeType(propertyValue, columnBindingPolicy.PropertyModel.PropertyType);

                    this.SetGraphElementPropertyValue(graphElement, columnBindingPolicy.PropertyModel.PropertyIndex, normalizedPropertyValue, requester: this);
                }

                this.SetBattonsSaveEnableProperty(graphElement);
            }
        }

        void IGraphController.BeforeNodeIsCollapsed(object node, ref bool canCollapse)
        {
            if (this.EditMode == GraphEditMode.Select || this.EditMode == GraphEditMode.ViewOnly)
            {
                canCollapse = true;
                
                return;
            }

            canCollapse = this.lastValidationNodeResult.Passed;

            if (canCollapse)
            {
                // Make sure that node is validated.
                if (this.FocusedNode != null)
                    canCollapse = this.ValidateAndTrySaveNodeIfRequired(this.FocusedNode);
            }
            else if (this.lastValidationFailedNode is not null)
            {
                // Allow collapsing nodes but prohibit colapsing any of the parent error nodes.
                TGraphElement? failedGraphElement = this.GetGraphElement(this.lastValidationFailedNode);

                if (failedGraphElement is not null)
                {
                    TGraphElement? graphElementToBeExpanded = this.GetGraphElement(node);

                    if (graphElementToBeExpanded is not null)
                        canCollapse = !this.GraphElementHasAsParent(failedGraphElement, graphElementToBeExpanded);
                }
            }
        }

        void IGraphController.BeforeNodeIsExpanded(object node)
        {
            if (!this.LoadAllNodes)
            {
                GraphNodeTag<TGraphElement> nodeTag = this.GetGraphNodeTag(node);

                if (nodeTag == null)
                    return;

                if (!nodeTag.ChildrenNodesLoaded)
                {
                    TGraphElement graphElement = nodeTag.GraphElement;

                    Cursor? currentCursor = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;

                    this.BeginUpdate();

                    foreach (TGraphElement childGraphElement in this.GetChildGraphElements(graphElement))
                    {
                        if (!this.nodesByGraphElement.ContainsKey(childGraphElement))
                        {
                            // Check if node already exists while on move - preventing double loading same node
                            this.Load(childGraphElement, graphElement);
                        }
                    }

                    this.BestFitColumns();
                    this.EndUpdate();

                    nodeTag.ChildrenNodesLoaded = true;

                    Cursor.Current = currentCursor;
                }
            }
        }

        void IGraphController.NodeIsExpanded(object node)
        {
            GraphNodeTag<TGraphElement> nodeTag = this.GetGraphNodeTag(node);

            if (nodeTag == null)
                return;

            if (nodeTag.ExpandedImageIndex >= 0)
                this.GraphControl?.SetNodeImageIndex(node, nodeTag.ExpandedImageIndex);
		}

		void IGraphController.NodeIsCollapsed(object node)
        {
            GraphNodeTag<TGraphElement> nodeTag = this.GetGraphNodeTag(node);

            if (nodeTag == null)
                return;

            this.GraphControl.SetNodeImageIndex(node, nodeTag.ImageIndex);
        }

        void IGraphController.BeforeNodeCheckStateIsChanged(object node, bool checkValue, ref bool canCheck)
        {
            if (this.ShowCheckBoxes)
            {
                TGraphElement graphElement = this.GetGraphElement(node);
                BeforeCheckGraphElementEventArgs<TGraphElement> checkGraphElementArgs = this.RaiseBeforeCheckGraphElementEvent(graphElement, checkValue, canCheck);

                if (checkGraphElementArgs != null)
                {
                    canCheck = checkGraphElementArgs.CanCheck;

                    if (canCheck)
                    {
                        this.BeginUpdate();
                        this.SetGraphNodeChecked(node, checkValue);
                        this.EndUpdate();
                    }
                }
            }
        }

        void IGraphController.SortedColumnCountIsChanged()
        {
            this.SetButtonsSortingEnableProperty();
        }

        bool IGraphController.CanDragNode(object node)
        {
            return this.CanDragAndDrop ? node != null : false;
        }

        bool IGraphController.CanNodeChangeParent(object node, object? newParentNode) 
        {
            return this.CanNodeChangeParent(node, newParentNode);
        }

        void IGraphController.DragDrop(object node, object? newParentNode)
        {
			TGraphElement? graphElement = this.GetGraphElement(node);

			if (graphElement != null)
			{
				TGraphElement? parentGraphElement = this.GetGraphElement(newParentNode);
				bool oldRequireSaving = this.DoesGraphElementRequireSaving(graphElement);

				this.SetParentGraphElement(graphElement, parentGraphElement);

				if (!oldRequireSaving)
					this.SaveGraphElement(graphElement);
			}
		}

		void IGraphController.AfterDropNode(object node, int nodeIndex)
        {
			TGraphElement? graphElement = this.GetGraphElement(node);
			
            if (graphElement != null)
			{
				bool oldRequireSaving = this.DoesGraphElementRequireSaving(graphElement);

				this.SetOrderIndex(graphElement, nodeIndex);

				if (!oldRequireSaving)
					this.SaveGraphElement(graphElement);
			}
        }

        void IGraphController.ButtonSaveIsClicked(Component button)
        {
            if (this.GraphControl is null)
                return;
            
            object? node = this.GraphControl.GetFocusedNode();
            
            if (node is not null)
                this.ValidateAndTrySaveNodeIfRequired(node);
        }

        void IGraphController.ButtonRemoveIsClicked(Component button)
        {
            if (this.GraphControl is null)
                return;

            object? node = this.GraphControl.GetFocusedNode();
            
            if (node is not null)
                this.RemoveNodeInternal(node);
        }

        void IGraphController.OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && this.FocusedNode is not null)
            {
                TGraphElement? graphElement = this.GetGraphElement(this.FocusedNode);

                if (graphElement != null)
                {
                    bool canDelete = this.CanDeleteGraphElement(graphElement);

                    if (canDelete)
                    {
                        Type objectType = this.GetBindingObject(graphElement).GetType();
                        string graphElementName = this.GetGraphElementName(graphElement);
                        canDelete = XtraMessageBox.Show(String.Format("Do you want to delete {0} {1}?", objectType.Name, graphElementName),
                                                        "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK;
                    }

                    if (canDelete)
                        this.RemoveNodeInternal(this.FocusedNode);
                }
            }
        }

        #endregion |   IGraphControllerControl Interface   |

        #region |   IBindingObjectControl Interface   |

        bool IBindingObjectControl.SaveBindingObject(IBindingSimpleObject bindingObject)
        {
            bool result = true;
            object? node = this.GetFocusedNode();
            TGraphElement? graphElement = (node is not null) ? this.GetGraphElement(node)
                                                             : default;

            if (node is not null && graphElement is not null && graphElement is SimpleObject graphElementSimpleObject && bindingObject is SimpleObject bindingSimpleObject && graphElementSimpleObject == bindingSimpleObject)
                result = this.ValidateAndTrySaveNodeIfRequired(node);

            return result;
        }

        void IBindingObjectControl.RefreshButtonsEnableProperty()
        {
            this.SetButtonsEnableProperty(this.FocusedNode);
        }

        #endregion |   IBindingObjectControl Interface   |

        #region |   Dispose   |

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
                //this.manager = null;
                this.editorBindings.Dispose();
                //this.nodesByGraphElement = null;
                //this.columns = null;
                this.anchorGraphElement = default(TGraphElement);
                //this.changableBindingObjectControl = null;
                //this.lastValidationNodeResult = null;
                this.imageList = null;
                this.stateImageList = null;
                this.checkBoxImageList = null;
                //this.columnBindingPolicyByColumnIndexByObjectType = null;
                //this.columnNamesWithDisabledBinding = null;
                //this.addButtonPolicyList = null;
                //this.buttonAddObjectList = null;
                //this.buttonSaveList = null;
                //this.buttonRemoveList = null;

                //this.BindingObjectChange = null;
                //this.BindingObjectPropertyValueChange = null;
                //this.BindingObjectOnValidation = null;
                //this.BindingObjectStoreData = null;
                this.GraphElementCreated = null;
                this.BeforeDeleteGraphElement = null;
                this.BindingObjectPropertyValueChange = null;
                this.BindingObjectParentGraphElementChange = null;
                this.AfterSetGraphColumnsEnableProperty = null;
                this.OnSetButtonEnableProperty = null;

                this.OnDispose();
            }

            base.Dispose(disposing);
            //System.GC.SuppressFinalize(this);

            this.isDisposed = true;
        }

        #endregion |   Dispose   |
    }

    #region |   Enums   |

    //public enum AddButtonPolicyOption
    //{
    //    AddAsChild,
    //    AddAsChildToParent
    //}

    //public enum BindingOption
    //{
    //    EnableEditing,
    //    DisableEditing
    //}

    #endregion |   Enums   |

    #region |   Delegates   |

    public delegate void ComponentEventHandler(object sender, ButtonEventArgs e);
    public delegate void CellEditValueGraphElementEventHandler<TGraphElement>(object sender, EditValueGraphElementEventArgs<TGraphElement> e);
    public delegate void ReloadGraphElementNodeEventHandler<TGraphElement>(object sender, ReloadGraphElementNodeEventArgs<TGraphElement> e);
    public delegate void GraphElementNodeEventHandler<TGraphElement>(object sender, GraphElementNodeEventArgs<TGraphElement> e);
    public delegate void CheckGraphElementEventHandler<TGraphElement>(object sender, CheckGraphElementEventArgs<TGraphElement> e);
	public delegate void BeforeCheckGraphElementEventHandler<TGraphElement>(object sender, BeforeCheckGraphElementEventArgs<TGraphElement> e);

	public delegate void GraphElementEventHandler<TGraphElement>(object sender, GraphElementEventArgs<TGraphElement> e);
    public delegate void GraphElementChangePropertyValueEventHandler<TGraphElement>(object sender, GraphElementChangedPropertyValueEventArgs<TGraphElement> e);
    public delegate void ChangeParentGraphElementEventHandler<TGraphElement>(object sender, ChangeParentGraphElementEventArgs<TGraphElement> e);


    #endregion |   Delegates   |

    #region |   EventArgs Classes   |

    //public class ComponentEventArgs : EventArgs 
    //{
    //    public ComponentEventArgs(Component component)
    //    {
    //        this.Component = component;
    //    }

    //    public Component Component { get; private set; }
    //}

    public class GraphElementEventArgs<TGraphElement> : EventArgs
    {
        public GraphElementEventArgs(TGraphElement graphElement)
        {
            this.GraphElement = graphElement;
        }

        public TGraphElement GraphElement { get; private set; }
    }

    public class NodeEventArgs : EventArgs
    {
        public NodeEventArgs(object node)
        {
            this.Node = node;
        }

        public object Node { get; private set; }
    }

    public class EditValueGraphElementEventArgs<TGraphElement> : GraphElementEventArgs<TGraphElement>
    {
        public EditValueGraphElementEventArgs(TGraphElement graphElement, object node, GraphColumn graphColumn, int bindingObjectProprtyIndex, object? editValue)
            : base(graphElement)
        {
            this.Node = node;
            this.GraphColumn = graphColumn;
            this.BindingObjectPropertyIndex = bindingObjectProprtyIndex;
            this.EditValue = editValue;
        }

        public object Node { get; private set; }
        public GraphColumn GraphColumn { get; private set; }
        public int BindingObjectPropertyIndex { get; private set; }
        public object? EditValue { get; set; }
    }

    public class ReloadGraphElementNodeEventArgs<TGraphElement> : GraphElementNodeEventArgs<TGraphElement>
    {
        public ReloadGraphElementNodeEventArgs(TGraphElement graphElement, object node, bool isReloaded)
            : base(graphElement, node)
        {
            this.IsReloaded = isReloaded;
        }
        public bool IsReloaded { get; set; }
    }

    public class GraphElementNodeEventArgs<TGraphElement> : NodeEventArgs
    {
        public GraphElementNodeEventArgs(TGraphElement graphElement, object node)
            : base(node)
        {
            this.GraphElement = graphElement;
        }

        public TGraphElement GraphElement { get; private set; }
    }


	//public class CheckGraphElementArgs<TGraphElement> : CheckGraphElementArgs
	//{
	//    public CheckGraphElementArgs(TGraphElement graphElement, bool checkValue, bool canCheck)
	//        : base(graphElement, checkValue, canCheck)
	//    {
	//    }

	//    public new TGraphElement GraphElement
	//    {
	//        get { return (TGraphElement)base.GraphElement; }
	//    }
	//}

	public class BeforeCheckGraphElementEventArgs<TGraphElement> : CheckGraphElementEventArgs<TGraphElement>
	{
		public BeforeCheckGraphElementEventArgs(TGraphElement graphElement, bool checkValue, bool canCheck)
			: base(graphElement, checkValue)
		{
			this.CanCheck = canCheck;
		}

		public bool CanCheck { get; set; }
	}

	public class CheckGraphElementEventArgs<TGraphElement> : GraphElementEventArgs<TGraphElement>
    {
        public CheckGraphElementEventArgs(TGraphElement graphElement, bool checkValue)
            : base(graphElement)
        {
            this.CheckValue = checkValue;
        }

        public bool CheckValue { get; private set; }
    }

    public class GraphElementChangedPropertyValueEventArgs<TGraphElement> : GraphElementEventArgs<TGraphElement>
    {
        public GraphElementChangedPropertyValueEventArgs(TGraphElement graphElement, string propertyName, object oldValue, object value)
            : base(graphElement)
        {
            this.PropertyName = propertyName;
            this.OldValue = oldValue;
            this.Value = value;
        }

        public string PropertyName { get; private set; }
        public object Value { get; private set; }
        public object OldValue { get; private set; }
    }

    public class ChangeParentGraphElementEventArgs<TGraphElement> : GraphElementEventArgs<TGraphElement>
    {
        public ChangeParentGraphElementEventArgs(TGraphElement graphElement, TGraphElement oldParent)
            : base(graphElement)
        {
            this.OldParent = oldParent;
        }

        public TGraphElement OldParent { get; private set; }
    }

    #endregion |   EventArgs Classes   |

    #region |   Helper Classes   |

    //public class GraphNodeTag<TGraphElement> : GraphNodeTag
    //{
    //    public GraphNodeTag(TGraphElement graphElement)
    //        : base(graphElement)
    //    {
    //    }

    //    public new TGraphElement GraphElement 
    //    {
    //        get { return (TGraphElement)base.GraphElement; }
    //        set { base.GraphElement = value; } 
    //    }
    //}

    public class GraphNodeTag<TGraphElement>
    {
        public GraphNodeTag(TGraphElement graphElement)
        {
            this.GraphElement = graphElement;
            this.ImageIndex = -1;
            this.ExpandedImageIndex = -1;
            this.Expanded = false;
            this.Checked = false;
            //this.Greyed = false;
            this.CheckDisabled = false;
            this.ChildrendNodesCheckCount = 0;
            this.CanBeFocusedWithoutSavingOldNode = false;
            this.Tag = null;
        }

        public TGraphElement GraphElement { get; internal set; }
        public int ImageIndex { get; set; }
        public int StateImageIndex { get; set; }
        public int ExpandedImageIndex { get; set; }
        public bool Expanded { get; set; }
        public bool Checked { get; set; }
        public bool CheckDisabled { get; set; }
        //public bool Greyed { get; set; }
        public int ChildrendNodesCheckCount { get; set; }
        public bool ChildrenNodesLoading { get; set; }
        public bool ChildrenNodesLoaded 
        { 
            get; 
            set; 
        }
        public bool CanBeFocusedWithoutSavingOldNode { get; set; }
        public object? Tag { get; set; }
    }

    //public class AddButtonPolicy
    //{
    //    //public AddButtonPolicy()
    //    //    : this(null)
    //    //{
    //    //}

    //    public AddButtonPolicy(Type objectType)
    //    {
    //        this.ObjectType = objectType;
    //        this.AddButtonPolicyOption = AddButtonPolicyOption.AddAsChild;
    //        this.IsSubButton = false;
    //        this.Buttons = new List<Component>();
    //    }

    //    public Type ObjectType { get; set; }
    //    public AddButtonPolicyOption AddButtonPolicyOption { get; set; }
    //    public bool IsSubButton { get; set; }
    //    public List<Component> Buttons { get; private set; }
    //}


    public class GraphColumnBindingPolicy<TGraphElement>
    {
        //public ColumnBindingPolicy()
        //	: this(null)
        //{
        //}

        public GraphColumnBindingPolicy(IPropertyModel propertyModel, GraphColumn graphColumn)
            : this(propertyModel, graphColumn, null, BindingOption.EnableEditing)
        {
        }

        public GraphColumnBindingPolicy(IPropertyModel propertyModel, GraphColumn graphColumn, Func<TGraphElement, object>? getEditValue, BindingOption bindingOption)
        {
            this.PropertyModel = propertyModel;
            this.GraphColumn = graphColumn;
            this.GetEditValue = getEditValue;
            this.BindingOption = bindingOption;
        }

        public IPropertyModel PropertyModel { get; private set; }
        public GraphColumn GraphColumn { get; private set; }
        public Func<TGraphElement, object>? GetEditValue { get; private set; }
        public BindingOption BindingOption { get; private set; }
    }


    //public class ColumnBindingPolicy<TGraphElement>
    //   {
    //       public ColumnBindingPolicy()
    //       {
    //       }

    //       public ColumnBindingPolicy(IPropertyModel propertyModel)
    //           : this(propertyModel, null, BindingOption.EnableEditing)
    //       {
    //       }

    //       public ColumnBindingPolicy(IPropertyModel propertyModel, Func<TGraphElement, object> getEditValue, BindingOption bindingOption)
    //       {
    //           this.PropertyModel = propertyModel;
    //           this.GetEditValue = getEditValue;
    //           this.BindingOption = bindingOption;
    //       }

    //       public IPropertyModel PropertyModel { get; set; }
    //       public Func<TGraphElement, object> GetEditValue { get; set; }
    //       public BindingOption BindingOption { get; set; }
    //   }

    //public class ColumnBindingPolicy
    //{
    //    public ColumnBindingPolicy()
    //        : this(null)
    //    {
    //    }

    //    public ColumnBindingPolicy(IPropertyModel propertyModel)
    //        : this(propertyModel, null, BindingOption.EnableEditing)
    //    {
    //    }

    //    public ColumnBindingPolicy(IPropertyModel propertyModel, Func<object, object> getEditValue, BindingOption bindingOption)
    //    {
    //        this.PropertyModel = propertyModel;
    //        this.GetEditValue = getEditValue;
    //        this.BindingOption = bindingOption;
    //    }

    //    public IPropertyModel PropertyModel { get; set; }
    //    public Func<object, object> GetEditValue { get; set; }
    //    public BindingOption BindingOption { get; set; }
    //}

    #endregion |   Helper Classes   |

    #region |   Interfaces   |

    //public interface IChangableBindingObjectControl : IBindingObjectControl
    //{
    //    event BindingObjectEventHandler BindingObjectChange;
    //}

    //public interface IBindingObjectControl
    //{
    //    event ChangePropertyValueRequesterBindingObjectEventHandler BindingObjectPropertyValueChange;
    //    event ChangeParentGraphElementRequesterEventHandler ParentGraphElementChange;
    //    event ValidationResultBindingObjectEventHandler BindingObjectOnValidation;
    //    event BindingObjectEventHandler BindingObjectStoreData;
    //    event BindingObjectEventHandler BindingObjectRefreshContext;

    //    /// <summary>
    //    /// Tell Business Object Manager to Save binded SimpleObject
    //    /// </summary>
    //    /// <param name="requester"></param>
    //    /// <param name="bindingObject"></param>
    //    /// <returns></returns>
    //    bool SaveBindingObject(object requester, IBindingObject bindingObject);
    //}

    public interface IGraphController
    {
        int AddColumn(string columnName, string caption, BindingDataType dataType, BindingEditorType editorType);
        void RemoveColumn(int columnIndex);
        int GetColumnWidth(int columnIndex);
        void SetColumnWidth(int columnIndex, int width);
        bool GetColumnEnableProperty(int columnIndex);
        void SetColumnEnableProperty(int columnIndex, bool value);
        bool GetColumnVisibleProperty(int columnIndex);
        void SetColumnVisibleProperty(int columnIndex, bool value);
        bool GetColumnShowInCustomizationFormProperty(int columnIndex);
        void SetColumnShowInCustomizationFormProperty(int columnIndex, bool value);
        void SetColumnCaption(int columnIndex, string caption);
        void SetColumnDataType(int columnIndex, BindingDataType dataType);
        void SetColumnEditorType(int columnIndex, BindingEditorType editorType);
        void RemoveColumnSorting();
        void ExpandAll();
        void CollapseAll();

        void BeforeNodeIsFocused(object node, object oldNode, ref bool canFocus);
        void FocusedNodeIsChanged(object node, object oldNode);
        void CellValueIsChanged(object node, int columnIndex, object value);
        void BeforeNodeIsCollapsed(object node, ref bool canCollapse);
        void BeforeNodeIsExpanded(object node);
        void NodeIsExpanded(object node);
        void NodeIsCollapsed(object node);
        void BeforeNodeCheckStateIsChanged(object node, bool checkValue, ref bool canCheck);
        void SortedColumnCountIsChanged();
        bool CanDragNode(object node);
        bool CanNodeChangeParent(object node, object? newParentNodeCandidate);
        void DragDrop(object node, object? newParentNode);
        void AfterDropNode(object node, int nodeIndex);
        void ButtonSaveIsClicked(Component button);
        void ButtonRemoveIsClicked(Component button);
        void OnKeyDown(object? sender, KeyEventArgs e);
        bool CommitChangeOnFocusedNodeChange { get; set; }
        bool CommitChangeOnDeleteRequest { get; set; }
    }

    public interface IGraphControl
    {
        void SetEditMode(GraphEditMode editMode);
        void SetLookAndFeelStyle(GraphLookAndFeelStyle lookAndFeelStyle);
        void SetCanDragAndDrop(bool canDragAndDrop);

        void SetImageList(ImageList imageList);
        void SetStateImageList(ImageList stateImageList);
        void SetCheckBoxImageList(ImageList checkBoxImageList);

        int AddColumn(string columnName, string caption, BindingDataType dataType, BindingEditorType editorType);
        void SetColumnCaption(int columnIndex, string caption);
        void SetColumnDataType(int columnIndex, BindingDataType dataType);
        void SetColumnEditorType(int columnIndex, BindingEditorType editorType);
        int GetColumnWidth(int columnIndex);
        void SetColumnWidth(int columnIndex, int width);
        void RemoveColumn(int columnIndex);
        bool GetColumnEnableProperty(int columnIndex);
        void SetColumnEnableProperty(int columnIndex, bool value);
        bool GetColumnVisibleProperty(int columnIndex);
        void SetColumnVisibleProperty(int columnIndex, bool value);
        bool GetColumnShowInCustomizationFormProperty(int columnIndex);
        void SetColumnShowInCustomizationFormProperty(int columnIndex, bool value);
        void SetColumnError(int columnIndex, string errorMessage);
        void ResetColumnErrors();
        void BestFitColumns(bool applyAutoWidth);
        void RemoveColumnSorting();
        int GetSortedColumnCount();

        void BeginUpdate();
        void EndUpdate();

        object? AddNode(object parentNode, object nodeTag);
        //GraphElementNodeTag CreateNodeTag(GraphElement graphElement);
        object? GetGraphNodeTag(object node);
        void SetNodeCheckedProperty(object node, bool value);
        void SetNodeHasChildrenProperty(object node, bool value);
        void RemoveNode(object node);
        object? GetCellEditValue(object node, int columnIndex);
        void SetCellEditValue(object node, int columnIndex, object? editValue);
        void MoveNode(object sourceNode, object? destinationNode);
        void ExpandNode(object node);
        void CollapseNode(object node);
        void ExpandAll();
        void CollapseAll();

        object? GetParentNode(object node);
        ICollection? GetChildNodes(object node);
        int GetChildNodesCount(object node);
        object? GetFocusedNode();
        void SetFocusedNode(object node);
        void SetFocusedColumn(int columnIndex);
        void SetNodeImageIndex(object node, int imageIndex);
        void SetNodeExpandedImageIndex(object node, int imageIndex);
        void SetNodeStateImageIndex(object node, int imageIndex);
        void SetNodeCheckBoxImageIndex(object node, int imageIndex);
        void SetCheckBoxMode(bool isCheckBoxMode);
        void SetRecursiveNodeChecking(bool allowRecursiveNodeChecking);
        void SetOrderIndex(object node, int orderIndex);

        void Focus();
        void CloseEditor();
        void ShowEditor();

        void SetButtonEnableProperty(Component button, bool enabled);

        /// <summary>
        /// Gets a GraphElement that is binded with a node.
        /// </summary>
        /// <param name="node">Control's graph node.</param>
        /// <returns>GraphElement binded with the node.</returns>
        object? GetGraphElement(object node);

        Component? GetEditorComponent(int columnIndex);
        void ClearNodes();

        Form? FindForm();
    }

    #endregion |   Interfaces   |
}