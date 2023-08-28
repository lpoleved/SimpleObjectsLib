using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using Simple;
using Simple.Modeling;
using Simple.Controls;
using Simple.Collections;
using DevExpress.XtraEditors;

namespace Simple.Objects.Controls
{
	public abstract class SimpleObjectGraphController : Component, IChangableBindingObjectControl, ISimpleValidation, ITextFinder, IGraphController, IComponent, IDisposable
    {
		#region |   Protected Members   |

		protected string columnNameOrderIndexHelper = "OrderIndexHelper";

		#endregion |   Protected Members   |

		#region |   Private Members   |

		private const int defaultGraphKey = 0;
        private const int defaultManyToManyRelationKey = 0;

        private IContainer components;
        private EditorBindingsControl editorBindings;
        private SimpleObjectManager? objectManager = null;
        private Control? graphControl = null;
        private Dictionary<GraphElement, object> nodesByGraphElement = new Dictionary<GraphElement, object>();
        private GraphColumnCollection columns;
		private GraphColumn? columnOrderIndexHelper;
        private GraphElement? anchorGraphElement = null;
        private IChangableBindingObjectControl? changableBindingObjectControl = null;
        private bool isGraphErrorMessageSet = false;
        //private object lastValidationNode = null;
        private object? lastValidationFailedNode = null;
        private GraphValidationResult lastValidationNodeResult = GraphValidationResult.DefaultSuccessResult;
        private GraphEditMode editMode = GraphEditMode.SelectEdit;
        private GraphLookAndFeelStyle lookAndFeelStyle = GraphLookAndFeelStyle.ExplorerStyle;
        private ImageList? imageList = null;
        private ImageList? stateImageList = null;
        private ImageList? checkBoxImageList = null;
        private int checkBoxCheckedImageIndex = -1;
        private int checkBoxCheckedChildCheckedImageIndex = -1;
        private int checkBoxUncheckedImageIndex = -1;
        private int checkBoxUncheckedChildCheckedImageIndex = -1;
        private int checkBoxCheckedGreyedImageIndex = -1;
        private int checkBoxCheckedChildCheckedGreyedImageIndex = -1;
        private int checkBoxUncheckedGreyedImageIndex = -1;
        private int checkBoxUncheckedChildCheckedGreyedImageIndex = -1;
        private bool isLoadingInProgress = true;
        private bool isImagesLoaded = false;
        private bool isStateImagesLoaded = false;
        private bool isCheckBoxImagesLoaded = false;
        private List<object>? checkedNodeList = null;
        private HashArray<HashArray<GraphColumnBindingPolicy>>? columnBindingPolicyByColumnIndexByTableId = null;
		private HashArray<HashArray<GraphColumnBindingPolicy>>? columnBindingPolicyByPropertyIndexByTableId = null;
		//private int maxPropertyIndex = 0;
		//private Dictionary<Type, Dictionary<GraphColumn, List<ColumnBindingPolicy>>> columnBindingPolicyListByGraphColumnByObjectType = new Dictionary<Type, Dictionary<GraphColumn, List<ColumnBindingPolicy>>>();
		private HashSet<int>? columnNamesWithDisabledBinding = null;
        //private Dictionary<Component, AddButtonPolicy> buttonAddObjectPolicyDictionaryByComponent = new Dictionary<Component, AddButtonPolicy>();
        private List<AddButtonPolicy<GraphElement>> addButtonPolicyList = new List<AddButtonPolicy<GraphElement>>();
        //private List<Component> buttonAddFolderList = new List<Component>();
        //private List<Component> buttonAddSubFolderList = new List<Component>();
        private List<Component> buttonInsertObjectList = new List<Component>();
		private List<Component> buttonMoveUpList = new List<Component>();
		private List<Component> buttonMoveDownList = new List<Component>();
		private List<Component> buttonRemoveColumnSortingList = new List<Component>();
		private List<Component> buttonSaveList = new List<Component>();
		private List<Component> buttonRejectChangesList = new List<Component>();
		private List<Component> buttonRemoveList = new List<Component>();
        private bool canDragAndDrop = false;
        private bool refreshGraphNodeOnEverySimpleObjectPropertyValueChange = true;
        private int graphUpdate = 0;
		//private int largeGraphUpdate = 0;
		private bool findStartFromBegining = false;
		private int findColumnIndex = 0;
		private int findColumnPosition = 0;
		private bool isInitialized = false;
        private bool isBinded = false;
        private bool isDisposed = false;
        private SimpleObject? bindingObject = null;
        private SimpleObject? changableBindingObjectControlFocusedSimpleObject = null;
        private object lockChildNodeLoading = new object();

        #endregion |   Private Members   |

        #region |   Constructors and Initialization   |

        public SimpleObjectGraphController()
        {
            this.components = new Container();

            //this.AppName = string.Empty;
			//this.GraphKey = defaultGraphKey;
            //this.ManyToManyRelationKey = defaultManyToManyRelationKey;
			//this.IsActive = true;
            //this.CanDragAndDrop = this.canDragAndDrop;
            //this.EditMode = GraphEditMode.SelectEdit;
            //this.LookAndFeelStyle = GraphLookAndFeelStyle.ExplorerStyle;
            //this.LoadAllNodes = false;
            //this.BindOnlyToSpecifiedGraphKey = true;
            //this.CommitChangeOnFocusedNodeChange = true;
            //this.CommitChangeOnDeleteRequest = true;

            //this.ClearOnBind = true;
            //this.DeleteAnchorGraphElementOnLastGraphElementDelete = true;


            this.columns = new GraphColumnCollection(this);
            this.editorBindings = new EditorBindingsControl();
            this.editorBindings.ChangableBindingObjectControl = this;

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
            //    this.ObjectManager = manager;
            //}

            this.OnInitializeComponent();
            GraphManager.SubscribeMe(this);
        }

        ~SimpleObjectGraphController()
        {
            GraphManager.UnsubscribeMe(this);
        }

        public SimpleObjectGraphController(IContainer container)
            : this()
        {
            container.Add(this);
        }

        #endregion |   Constructors and Initialization   |

        #region |   Events   |

        public event SimpleObjectEventHandler? BindingObjectChange;
        public event ChangePropertyValueRequesterBindingObjectEventHandler? BindingObjectPropertyValueChange;
        public event GraphElementChangePropertyValueSimpleObjectRequesterEventHandler? SimpleObjectPropertyValueChange;
        public event BindingObjectRelationForeignObjectSetEventHandler? BindingObjectRelationForeignObjectSet;
        public event ChangePropertyValueRequesterBindingObjectEventHandler? BindingObjectRefreshContext; // This is request for refresh context by the any other object property value change or action that require context refresh.
        //public event IconNameChangeBindingObjectEventHandler BindingObjectIconNameChange;
        public event GraphValidationResultEventHandler? BindingObjectOnValidation;
        public event SimpleObjectEventHandler? BindingObjectPushData;
        public event GraphElementRequesterEventHandler? GraphElementCreated;
        public event GraphElementChangeContainerRequesterEventHandler? GraphElementDeleteRequested; 
        public event GraphElementRequesterEventHandler? BeforeGraphElementDeleted;
        public event ChangeParentGraphElementRequesterEventHandler? BindingObjectParentGraphElementChange;
        public event GraphElementEventHandler? AfterSetColumnsEnableProperty;
        //public event AnchorGraphElementEventHandler GetAnchorGraphElement;
        //public event EventHandler CreateAnchorGraphElement;
        public event EventHandler? AfterSetButtonsEnableProperty;
		public event ComponentEventHandler? OnSetButtonEnableProperty;
        public event EditValueGraphElementEventHandler? GetCustomCellEditValue;
        public event ReloadGraphElementNodeEventHandler? ReloadGraphElementNode;
        public event GraphElementNodeEventHandler? AfterGraphElementNodeReload;
        public event EventHandler? AfterBestFitGraphColumns;
        public event CheckGraphElementEventHandler? BeforeCheckGraphElement;
        public event CompareColumnNodeValues? CompareColumnNodeValues;
		public event AskBeforeDeleteGraphElementEventHandler? AskBeforeDelete;
		public event AllowAddButtonPolicy? ButtonAddClicked;

        #endregion |   Events   |

        #region |   Public Properties   |

        [Category("General"), DefaultValue("")]
        public string AppName { get; set; } = String.Empty;

        [Category("Graph"), DefaultValue(defaultGraphKey)]
        public int GraphKey { get; set; } = defaultGraphKey;

        [Category("Group Membership"), DefaultValue(defaultManyToManyRelationKey)]
        public int ManyToManyRelationKey { get; set; } = defaultManyToManyRelationKey;

        public bool IsActive { get; set; } = true;
		
		[Category("General"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SimpleObjectManager? ObjectManager
        {
            get { return this.objectManager; }

            set
            {
                if (this.objectManager != null)
                {
                    this.objectManager.NewGraphElementCreated -= new GraphElementRequesterEventHandler(objectManager_GraphElementCreated);
                    this.objectManager.DeleteRequested -= this.ObjectManager_DeleteRequested;
                    this.objectManager.BeforeDelete -= new SimpleObjectRequesterEventHandler(objectManager_BeforeDeleting);
					this.objectManager.AfterDelete -= new SimpleObjectRequesterEventHandler(objectManager_AfterDelete);
                    this.objectManager.PropertyValueChange -= new ChangePropertyValueSimpleObjectRequesterEventHandler(objectManager_PropertyValueChange);
                    this.objectManager.GraphElementParentChange -= new ChangeParentGraphElementRequesterEventHandler(objectManager_GraphElementParentChange);
                    //this.manager.ChangedPropertyNamesCountChange -= new CountChangeSimpleObject(manager_ChangedPropertyNamesCountChange);
					this.objectManager.OrderIndexChange -= new ChangeOrderIndexSimpleObjectRequesterEventHandler(objectManager_OrderIndexChange);
					this.objectManager.RequireSavingChange -= new RequireSavingChangeSimpleObjectEventHandler(objectManager_RequireSavingChange);
					this.objectManager.RequireCommitChange -= ObjectManager_RequireCommitChange;
					this.objectManager.ActiveEditorsPushObjectData -= this.ObjectManager_ActiveEditorsPushObjectData;
					this.objectManager.RelationForeignObjectSet -= new RelationForeignObjectSetEventHandler(objectManager_RelationForeignObjectSet);
                    this.objectManager.ImageNameChange -= new ImageNameChangeSimpleObjectEventHandler(objectManager_ImageNameChange);
					this.objectManager.MultipleImageNameChange -= new MultipleImageNameChangeSimpleObjectEventHandler(objectManager_MultipleImageNameChange);
                    this.objectManager.UpdateStarted -= new EventHandler(objectManager_UpdateStarted);
                    this.objectManager.UpdateEnded -= new EventHandler(objectManager_UpdateEnded);
					this.objectManager.ValidationInfo -= this.ObjectManager_ValidationInfo;
				}

				this.objectManager = value;

                if (this.objectManager != null)
                {
                    this.objectManager.NewGraphElementCreated += new GraphElementRequesterEventHandler(objectManager_GraphElementCreated);
                    this.objectManager.DeleteRequested += this.ObjectManager_DeleteRequested;
                    this.objectManager.BeforeDelete += new SimpleObjectRequesterEventHandler(objectManager_BeforeDeleting);
                    this.objectManager.AfterDelete += new SimpleObjectRequesterEventHandler(objectManager_AfterDelete);
                    this.objectManager.PropertyValueChange += new ChangePropertyValueSimpleObjectRequesterEventHandler(objectManager_PropertyValueChange);
                    this.objectManager.GraphElementParentChange += new ChangeParentGraphElementRequesterEventHandler(objectManager_GraphElementParentChange);
                    //this.manager.ChangedPropertyNamesCountChange += new CountChangeSimpleObject(manager_ChangedPropertyNamesCountChange);
					this.objectManager.OrderIndexChange += new ChangeOrderIndexSimpleObjectRequesterEventHandler(objectManager_OrderIndexChange);
                    this.objectManager.RequireCommitChange += ObjectManager_RequireCommitChange;
                    this.objectManager.RequireSavingChange += new RequireSavingChangeSimpleObjectEventHandler(objectManager_RequireSavingChange);
					this.objectManager.ActiveEditorsPushObjectData += this.ObjectManager_ActiveEditorsPushObjectData;
                    this.objectManager.RelationForeignObjectSet += new RelationForeignObjectSetEventHandler(objectManager_RelationForeignObjectSet);
                    this.objectManager.ImageNameChange += new ImageNameChangeSimpleObjectEventHandler(objectManager_ImageNameChange);
					this.objectManager.MultipleImageNameChange += new MultipleImageNameChangeSimpleObjectEventHandler(objectManager_MultipleImageNameChange);
                    this.objectManager.UpdateStarted += new EventHandler(objectManager_UpdateStarted);
                    this.objectManager.UpdateEnded += new EventHandler(objectManager_UpdateEnded);
					this.objectManager.ValidationInfo += this.ObjectManager_ValidationInfo;

					int maxTableId = this.objectManager.ModelDiscovery.GetObjectModelCollection().Max(item => item.TableInfo.TableId) + 1;
					//this.maxPropertyIndex = this.ObjectManager.ModelDiscovery.GetObjectModelCollection().Max(model => model.PropertyModels.Max(property => property.Index));

					this.columnBindingPolicyByColumnIndexByTableId = new HashArray<HashArray<GraphColumnBindingPolicy>>(maxTableId + 1);
					this.columnBindingPolicyByPropertyIndexByTableId = new HashArray<HashArray<GraphColumnBindingPolicy>>(maxTableId + 1);
                }

				if (this.editorBindings != null)
					this.editorBindings.ObjectManager = this.objectManager;
            }
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
                this.GraphControlSetEditMode(this.editMode);
            }
        }

        [Category("Appearancee"), DefaultValue(GraphLookAndFeelStyle.ExplorerStyle)]
        public GraphLookAndFeelStyle LookAndFeelStyle
        {
            get { return this.lookAndFeelStyle; }
            set
            {
                this.lookAndFeelStyle = value;
                this.GraphControlSetLookAndFeelStyle(this.lookAndFeelStyle);
            }
        }

        [Category("Images"), DefaultValue(null)]
        public ImageList? ImageList
        {
            get { return this.imageList; }
            set
            {
                if (value != this.imageList)
                    this.isImagesLoaded = false;

                this.imageList = value;
                
                if (this.imageList is not null)
                    this.GraphControlSetImageList(this.imageList);
            }
        }

        [Category("Images"), DefaultValue(null)]
        public ImageList? StateImageList
        {
            get { return this.stateImageList; }
            set
            {
                if (value != this.stateImageList)
                    this.isStateImagesLoaded = false;

                this.stateImageList = value;
                
                if (this.stateImageList is not null)
                    this.GraphControlSetStateImageList(this.stateImageList);
            }
        }

        [Category("Images"), DefaultValue(null)]
        public ImageList? CheckBoxImageList
        {
            get { return this.checkBoxImageList; }
            set
            {
                if (value != this.checkBoxImageList)
                    this.isCheckBoxImagesLoaded = false;

                this.checkBoxImageList = value;
                
                if (this.checkBoxImageList != null)
                    this.GraphControlSetCheckBoxImageList(this.checkBoxImageList);

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

        [Category("Graph"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GraphColumnCollection Columns
        {
            get { return this.columns; }
        }

        [Category("General"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsCheckBoxMode
        {
            get { return this.ManyToManyRelationKey > 0 || this.CheckBoxImageList != null; }
        }

        //[Category("Simple"), Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public Graph Graph
        //{
        //    get { return this.ObjectManager.GetGraph(this.GraphKey); }
        //}

        [Category("Simple"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object? FocusedNode => this.GraphControlGetFocusedNode();

        [Category("Simple"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GraphElement? FocusedGraphElement => (this.FocusedNode != null) ? this.GraphControlGetGraphElementByNode(this.FocusedNode) : default;

        [Category("Simple"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SimpleObject? FocusedSimpleObject => (this.FocusedGraphElement != null) ? this.FocusedGraphElement.SimpleObject : default;

        [Category("Group Membership"), DefaultValue(null)]
        public IChangableBindingObjectControl? ChangableBindingObjectControl
        {
            get { return this.changableBindingObjectControl; }
            set
            {
                if (this.changableBindingObjectControl != null)
                {
                    this.changableBindingObjectControl.BindingObjectChange -= new SimpleObjectEventHandler(changableBindingObjectControl_BindingObjectChange);
                    this.changableBindingObjectControl.BindingObjectRelationForeignObjectSet -= new BindingObjectRelationForeignObjectSetEventHandler(changableBindingObjectControl_BindingObjectRelationForeignObjectSet);
                    this.changableBindingObjectControl.BindingObjectRefreshContext -= new ChangePropertyValueRequesterBindingObjectEventHandler(changableBindingObjectControl_BindingObjectRefreshContext);
                }

                this.changableBindingObjectControl = value;

                if (this.changableBindingObjectControl != null)
                {
                    this.changableBindingObjectControl.BindingObjectChange += new SimpleObjectEventHandler(changableBindingObjectControl_BindingObjectChange);
                    this.changableBindingObjectControl.BindingObjectRelationForeignObjectSet += new BindingObjectRelationForeignObjectSetEventHandler(changableBindingObjectControl_BindingObjectRelationForeignObjectSet);
                    this.changableBindingObjectControl.BindingObjectRefreshContext += new ChangePropertyValueRequesterBindingObjectEventHandler(changableBindingObjectControl_BindingObjectRefreshContext);
                }

                //this.editorBindings.ChangableBindingObjectControl = value;
            }
        }

        [Category("General"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SimpleObject? ChangableBindingObjectControlFocusedSimpleObject 
        { 
            get { return this.changableBindingObjectControlFocusedSimpleObject; }
        }

        [Category("General"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public HashSet<int> ColumnIndexesWithDisabledBinding
        {
            get
            {
                if (this.columnNamesWithDisabledBinding == null)
					this.columnNamesWithDisabledBinding = new HashSet<int>();

                return this.columnNamesWithDisabledBinding;
            }
        }

        [Category("General"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<GraphElement> GetAllGraphElements()
        {
            return this.nodesByGraphElement.Keys;
        }

        [Category("General"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public KeyValuePair<GraphElement, object>[] GetAllGraphElementNodePairs()
        {
            return this.nodesByGraphElement.ToArray(); // new ReadOnlyDictionary<GraphElement, object>(this.nodesByGraphElement); } //this.nodesByGraphElement.AsReadOnly(); }
		}

		[Category("General"), Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int NodeCount
		{
			get { return this.nodesByGraphElement.Count; } 
		}

        [Category("General"), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GraphElement? AnchorGraphElement 
        {
            get { return this.anchorGraphElement; }
            set 
            { 
                this.anchorGraphElement = value;

                if (this.anchorGraphElement is not null)
                    this.anchorGraphElement.IsAnchor = true;
            }
        }

        [Category("Graph"), DefaultValue(false)]
        public bool CanDragAndDrop
        {
            get { return this.canDragAndDrop; }
            set
            {
                this.canDragAndDrop = value;
                this.GraphControlSetCanDragAndDrop(value);
            }
        }

        [Category("Simple"), DefaultValue(true)]
        public bool RefreshGraphNodeOnEverySimpleObjectPropertyValueChange
        {
            get { return this.refreshGraphNodeOnEverySimpleObjectPropertyValueChange; }
            set { this.refreshGraphNodeOnEverySimpleObjectPropertyValueChange = value; }
        }

        [Category("Simple"), DefaultValue(false)]
        public bool LoadAllNodes { get; set; }

        [Category("Simple"), DefaultValue(true)]
        public bool BindOnlyToSpecifiedGraphKey { get; set; } = true;

		[Category("Simple"), DefaultValue(false)]
		public bool AskBeforeDeleteOnButtonRemoveClick { get; set; }

        [Category("Simple"), DefaultValue(true)]
        public bool CommitChangeOnFocusedNodeChange { get; set; } = true;

        [Category("Simple"), DefaultValue(true)]
        public bool CommitChangeOnDeleteRequest { get; set; } = true;

        //[Category("Simple"), DefaultValue(true)]
        //public bool DeleteAnchorGraphElementOnLastGraphElementDelete { get; set; }

        //[Category("Simple"), DefaultValue(true)]
        //public bool ClearOnBind { get; set; }

        [Category("Simple"), DefaultValue(false)]
        public bool IsAnchorMode { get; set; }

        [Category("Simple"), DefaultValue(SaveButtonOption.CommitChanges)]
        public SaveButtonOption SaveButonOption { get; set; }

        #endregion |   Public Properties   |

        #region |   Protected Properties   |

        protected Control? GraphControl
        {
            get { return this.graphControl; }
            set { this.graphControl = value; }
        }

        //protected List<Component> ButtonAddFolderList
        //{
        //    get { return this.buttonAddFolderList; }
        //}

        //protected List<Component> ButtonAddSubFolderList
        //{
        //    get { return this.buttonAddSubFolderList; }
        //}

        protected List<Component> ButtonInsertObjectList
        {
            get { return this.buttonInsertObjectList; }
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

		protected List<Component> ButtonRejectChangesList
		{
			get { return this.buttonRejectChangesList; }
		}

		protected List<Component> ButtonRemoveList
        {
            get { return this.buttonRemoveList; }
        }

        protected List<AddButtonPolicy<GraphElement>> AddButtonPolicyList
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

        private List<object> CheckedNodeList
        {
            get
            {
                if (this.checkedNodeList == null)
                    this.checkedNodeList = new List<object>();

                return this.checkedNodeList;
            }
        }

        #endregion |   Private Properties   |

        #region |   Protected Abstract Methods   |

        protected abstract void GraphControlSetEditMode(GraphEditMode editMode);
        protected abstract void GraphControlSetLookAndFeelStyle(GraphLookAndFeelStyle lookAndFeelStyle);
        protected abstract void GraphControlSetCanDragAndDrop(bool canDragAndDrop);

        protected abstract void GraphControlSetImageList(ImageList imageList);
        protected abstract void GraphControlSetStateImageList(ImageList stateImageList);
        protected abstract void GraphControlSetCheckBoxImageList(ImageList checkBoxImageList);

        protected internal abstract int GraphControlAddColumn(string columnName, string caption, BindingDataType dataType, BindingEditorType editorType);
        protected internal abstract void GraphControlSetColumnCaption(int columnIndex, string caption);
		protected internal abstract void GraphControlSetColumnDataType(int columnIndex, BindingDataType dataType);
		protected internal abstract void GraphControlSetColumnEditorType(int columnIndex, BindingEditorType editorType);
		protected internal abstract int GraphControlGetColumnWidth(int columnIndex);
		protected internal abstract void GraphControlSetColumnWidth(int columnIndex, int width);
		protected internal abstract void GraphControlRemoveColumn(int columnIndex);
		protected abstract bool GraphControlGetColumnEnableProperty(int columnIndex);
		protected abstract void GraphControlSetColumnEnableProperty(int columnIndex, bool value);
		protected abstract bool GraphControlGetColumnVisibleProperty(int columnIndex);
		protected abstract void GraphControlSetColumnVisibleProperty(int columnIndex, bool value);
		protected abstract bool GraphControlGetColumnShowInCustomizationFormProperty(int columnIndex);
		protected abstract void GraphControlSetColumnShowInCustomizationFormProperty(int columnIndex, bool value);
		protected abstract void GraphControlSetColumnError(int columnIndex, string errorMessage);
		
        protected abstract void GraphControlResetColumnErrors();
        protected abstract void GraphControlBestFitColumns(bool applyAutoWidth);
		protected abstract void GraphControlSetColumnSorting(int columnIndex, SortOrder sortOrder);
		protected abstract void GraphControlRemoveColumnSorting();
		protected abstract int GraphControlGetSortedColumnCount();

        protected abstract void GraphControlBeginUpdate();
        protected abstract void GraphControlEndUpdate();
        //protected abstract void GraphControlBeginLargeUpdate();
        //protected abstract void GraphControlEndLargeUpdate();

        protected abstract object GraphControlAddNode(object? parentNode, object nodeTag);
        //protected abstract GraphElementNodeTag GraphControlCreateNodeTag(GraphElement graphElement);
        protected abstract GraphElementNodeTag GraphControlGetGraphNodeTag(object node);
        protected abstract void GraphControlSetNodeCheckedProperty(object node, bool value);
        protected abstract void GraphControlSetNodeHasChildrenProperty(object node, bool value);
        protected abstract void GraphControlDeleteNode(object node);
		protected abstract object GraphControlGetNodeCellEditValue(object node, int columnIndex);
		protected abstract void GraphControlSetNodeCellEditValue(object node, int columnIndex, object? editValue);
        protected abstract void GraphControlMoveNode(object sourceNode, object? destinationNode);
        protected abstract void GraphControlExpandNode(object node);
        protected abstract void GraphControlCollapseNode(object node);
        protected abstract void GraphControlExpandAll();
        protected abstract void GraphControlCollapseAll();

        protected abstract object? GraphControlGetParentNode(object node);
		protected abstract object[] GraphControlGetChildNodes(object node);
        protected abstract object? GraphControlGetFocusedNode();
		protected abstract int GraphControlGetFocusedColumnIndex();
		protected abstract void GraphControlSetFocusedNode(object node);
		protected abstract void GraphControlSetFocusedColumn(int columnIndex);
		protected abstract void GraphControlSetNodeOrderIndex(object node, int orderIndex);
        protected abstract void GraphControlSetNodeImageIndex(object node, int imageIndex);
        protected abstract void GraphControlSetNodeExpandedImageIndex(object node, int imageIndex);
        protected abstract void GraphControlSetNodeStateImageIndex(object node, int imageIndex);
        protected abstract void GraphControlSetNodeCheckBoxImageIndex(object node, int imageIndex);

		protected abstract void GraphControlHighlightCellText(object node, int columnIndex, int startTextPosition, int textLength);

        protected abstract void GraphControlSetCheckBoxMode(bool isCheckBoxMode);

        protected abstract void GraphControlFocus();
        protected abstract void GraphControlCloseEditor();
        protected abstract void GraphControlShowEditor();

        protected abstract void GraphControlSetButtonEnableProperty(Component button, bool enabled);

        /// <summary>
        /// Gets a GraphElement that is binded with a node.
        /// </summary>
        /// <param name="node">Control's graph node.</param>
        /// <returns>GraphElement binded with the node.</returns>
        protected abstract GraphElement? GraphControlGetGraphElementByNode(object node);

		protected abstract Component? GraphControlGetEditorComponent(int columnIndex);
        protected abstract void GraphControlClearNodes();

        #endregion |   Protected Abstract Methods   |

        #region |   Public Methods   |

        //public void Bind()
        //{
        //    this.Bind(null);
        //}

        //     public void Bind(GraphElement anchorGraphElement)
        //     {
        //         this.Bind(anchorGraphElement, null);
        //     }

        //     public void Bind(GraphElement anchorGraphElement, SimpleObjectCollection<GraphElement> rootGraphElements)
        //     {
        //         GraphElement parentGraphElement = null;
        //         SimpleObjectCollection<GraphElement> internalRootGraphElements = rootGraphElements;

        //         if (!this.isInitialized)
        //	this.Initialize();

        //         this.anchorGraphElement = anchorGraphElement;

        //         if (this.anchorGraphElement != null)
        //         {
        //             this.anchorGraphElement.IsAnchor = true;

        //             //if (this.nodesByGraphElement.ContainsKey(this.anchorGraphElement))
        //             //    parentGraphElement = this.anchorGraphElement;
        //         }

        //         if (internalRootGraphElements == null)
        //             internalRootGraphElements = (this.anchorGraphElement == null) ? this.Graph.RootGraphElements : this.anchorGraphElement.GraphElements;

        //         this.BeginLargeGraphUpdate();
        //         this.isLoadingInProgress = true;

        //foreach (GraphElement graphElement in internalRootGraphElements)
        //             this.Load(graphElement, parentGraphElement);

        //         this.isLoadingInProgress = false;
        //         this.isBinded = true;
        //         this.EndLargeGraphUpdate();

        //         //Thread thread = new Thread(() => this.LoadChildGraphElements(internalRootGraphElements));
        //         //thread.IsBackground = true;
        //         //thread.Priority = ThreadPriority.Lowest;
        //         //thread.Start();

        //// Refresh focused node dependencies
        //         object focusedNode = this.GraphControlGetFocusedNode();
        //         this.GraphControlFocusedNodeIsChanged(null, focusedNode);
        //     }

        //public void Bind() => this.Bind(parent: null);

        public void Bind(GraphElement? parent = null)
        {
            // Avoiding calling GetRootGraphElements in anchor mode (it will try to sort root GraphElements and will sets PreviousId unnecessary)

            if (this.IsAnchorMode || this.AnchorGraphElement != null)
            {
                this.Bind(collection: null, parent);
            }
            else
            {
                var collection = this.ObjectManager!.GetRootGraphElements(this.GraphKey); //, out bool[] hasChildrenInfo);

                this.Bind(collection, parent);
            }
        }

  //      public void Bind(IEnumerable<GraphElement>? collection, GraphElement? parent = null)
  //      {
  //          bool[] hasChildrenInfo = new bool[collection.Count()];

  //          for (int i = 0; i < hasChildrenInfo.Length; i++)
  //              hasChildrenInfo[i] = collection.ElementAt(i).HasChildren;


		//	this.Bind(collection, parent, hasChildrenInfo);
  //      }

		//public void Bind(IEnumerable<GraphElement> collection)
		//{
		//	this.Bind(collection, parent: null);
		//}

		public void Bind(IEnumerable<GraphElement>? collection, GraphElement? parent = null) //, bool[] hasChildrenInfo)
        {
            IEnumerable<GraphElement> internalGraphElements;
            object? oldFocusedNode = this.GraphControlGetFocusedNode();
            //bool[] hasChildrenInfoLocal;

			if (!this.isInitialized)
                this.Initialize();

            if (collection == null && this.IsAnchorMode)
            {
                collection = new GraphElement[0]; // { };
			}

            this.BeginGraphUpdate();
            this.isLoadingInProgress = true;

            if (this.AnchorGraphElement != null)
            {
                internalGraphElements = this.AnchorGraphElement.GraphElements; //out hasChildrenInfoLocal);
                parent = this.AnchorGraphElement;
            }
            else if (collection == null)
            {
                //internalGraphElements = (parent != null) ? parent.GraphElements : this.ObjectManager!.GetRootGraphElements(this.GraphKey);
                internalGraphElements = this.ObjectManager!.GetGraphElements(this.GraphKey, (parent != null) ? parent.Id : 0); //, out hasChildrenInfoLocal);
			}
            else
            {
                internalGraphElements = collection;
			}

            for (int i = 0; i < internalGraphElements.Count(); i++)
                this.Load(internalGraphElements.ElementAt(i), parent);

            this.isLoadingInProgress = false;
            this.isBinded = true;
            this.EndGraphUpdate();

            //Thread thread = new Thread(() => this.LoadChildGraphElements(internalRootGraphElements));
            //thread.IsBackground = true;
            //thread.Priority = ThreadPriority.Lowest;
            //thread.Start();

            // Refresh focused node dependencies
            object? focusedNode = this.GraphControlGetFocusedNode();
            GraphElement? focusedGraphElement = (focusedNode != null) ? this.GraphControlGetGraphElementByNode(focusedNode) : null;
            SimpleObject? focusedSimpleObject = (focusedGraphElement != null) ? focusedGraphElement.SimpleObject 
                                                                              : null;
            this.SetColumnsAndButtonsEnableProperty(focusedGraphElement);
            this.RaiseBindingObjectChange(focusedSimpleObject);
            
            if (focusedNode != oldFocusedNode)
                this.GraphControlFocusedNodeIsChanged(focusedNode, oldFocusedNode);
        }

        public void Clear()
        {
            this.nodesByGraphElement.Clear();
            this.GraphControlClearNodes();

            //if (this.AnchorGraphElement != null && this.DeleteAnchorGraphElementOnLastGraphElementDelete &&
            //    (this.AnchorGraphElement.GraphElements.Count == 0 || this.GetAllGraphElements().All(ge => ge.DeleteRequested)))
            //{
            //    this.AnchorGraphElement.RequestDelete(requester: this);
            //}

            //this.AnchorGraphElement = null;
        }

        public void BeginGraphUpdate()
        {
//            if (this.graphUpdate == 0)

            this.graphUpdate++;
            this.GraphControlBeginUpdate();
        }

		public void EndGraphUpdate()
		{
            //if (this.graphUpdate == 0)
            //    this.GraphControlCloseEditor();
            
            this.GraphControlEndUpdate();
			this.graphUpdate--;
		}

		//public void BeginLargeGraphUpdate()
		//{
  //          //if (this.largeGraphUpdate == 0)
  //          //    this.GraphControlCloseEditor();
            
		//	this.largeGraphUpdate++;
  //          this.GraphControlBeginLargeUpdate();
		//}
		
		//public void EndLargeGraphUpdate()
		//{
  //          //if (this.largeGraphUpdate == 0)
  //          //    this.GraphControlCloseEditor();
            
  //          this.GraphControlEndLargeUpdate();
		//	this.largeGraphUpdate--;
		//}
		/// <summary>
        /// Gets a GraphElement that is binded with a node.
        /// </summary>
        /// <param name="node">Control's graph node.</param>
        /// <returns>GraphElement binded with the node.</returns>
        public GraphElement? GetGraphElement(object? node)
        {
            return (node != null) ? this.GraphControlGetGraphElementByNode(node) : default;
        }

        public GraphElementNodeTag? GetGraphNodeTag(GraphElement graphElement)
        {
            object? node = this.GetNode(graphElement);
            
            if (node != null)
                return this.GetGraphNodeTag(node);

            return default;
        }

        public GraphElementNodeTag GetGraphNodeTag(object node)
        {
            return this.GraphControlGetGraphNodeTag(node);
        }


        public GraphColumnBindingPolicy GetGraphColumnBindingPolicyByColumnIndex(int tableId, int columnIndex)
        {
            GraphColumnBindingPolicy? result = null;
            HashArray<GraphColumnBindingPolicy>? columnBindingPolicyByColumnIndex = this.columnBindingPolicyByColumnIndexByTableId?[tableId];

			if (columnBindingPolicyByColumnIndex != null)
			{
				result = columnBindingPolicyByColumnIndex.GetValue(columnIndex);

				if (result == null)
				{
					result = this.CreateDefaultGraphColumnBindingPolicyByColumnIndex(tableId, columnIndex);

					if (result != null && this.columnBindingPolicyByPropertyIndexByTableId != null)
					{
						columnBindingPolicyByColumnIndex[columnIndex] = result;
						this.columnBindingPolicyByPropertyIndexByTableId[tableId][result.PropertyModel.PropertyIndex] = result;
					}
				}
			}
			else
			{
				result = this.CreateDefaultGraphColumnBindingPolicyByColumnIndex(tableId, columnIndex);
				
				if (result != null)
				{
					columnBindingPolicyByColumnIndex = new HashArray<GraphColumnBindingPolicy>(this.Columns.Max(item => item.Index) + 1);
					columnBindingPolicyByColumnIndex[columnIndex] = result;
					this.columnBindingPolicyByColumnIndexByTableId[tableId] = columnBindingPolicyByColumnIndex;

					int maxPropertyIndex = this.GetMaxPropertyIndex(tableId);
					HashArray<GraphColumnBindingPolicy> columnBindingPolicyByPropertyIndex = new HashArray<GraphColumnBindingPolicy>(maxPropertyIndex + 1);
					columnBindingPolicyByPropertyIndex[result.PropertyModel.PropertyIndex] = result;
					this.columnBindingPolicyByPropertyIndexByTableId[tableId] = columnBindingPolicyByPropertyIndex;
				}
			}

            return result;
        }

		public GraphColumnBindingPolicy GetGraphColumnBindingPolicyByObjectPropertyIndex(int tableId, int propertyIndex)
        {
			GraphColumnBindingPolicy result = null;
			HashArray<GraphColumnBindingPolicy> columnBindingPolicyByPropertyIndex = this.columnBindingPolicyByPropertyIndexByTableId[tableId];

			if (columnBindingPolicyByPropertyIndex != null)
			{
				result = columnBindingPolicyByPropertyIndex[propertyIndex];

				if (result == null)
				{
					result = this.CreateDefaultGraphColumnBindingPolicyByPropertyIndex(tableId, propertyIndex);

					if (result != null)
					{
						columnBindingPolicyByPropertyIndex[propertyIndex] = result;
						this.columnBindingPolicyByColumnIndexByTableId[tableId][result.GraphColumn.Index] = result;
					}
				}
			}
			else
			{
				result = this.CreateDefaultGraphColumnBindingPolicyByPropertyIndex(tableId, propertyIndex);

				if (result != null)
				{
					int maxPropertyIndex = this.GetMaxPropertyIndex(tableId);
					columnBindingPolicyByPropertyIndex = new HashArray<GraphColumnBindingPolicy>(maxPropertyIndex + 1);
					columnBindingPolicyByPropertyIndex[propertyIndex] = result;
					this.columnBindingPolicyByPropertyIndexByTableId[tableId] = columnBindingPolicyByPropertyIndex;

					HashArray<GraphColumnBindingPolicy> columnBindingPolicyByColumnIndex = new HashArray<GraphColumnBindingPolicy>(this.Columns.Max(item => item.Index) + 1);
					columnBindingPolicyByColumnIndex[result.GraphColumn.Index] = result;
					this.columnBindingPolicyByColumnIndexByTableId[tableId] = columnBindingPolicyByColumnIndex;
				}
			}

			return result;
		}

		//public IEnumerable<GraphColumn> GetGraphColumnsByBindingPolicy(Type objectType, string columnOrPropertyName)
		//{
		//	List<GraphColumn> result = new List<GraphColumn>();
		//	GraphColumn graphColumn;

		//	foreach (var columnBindingPolicyListByGraphColumnItem in this.columnBindingPolicyByColumnIndexByTableId)
		//		if (columnBindingPolicyListByGraphColumnItem.Key == objectType || Comparison.CompareTypes(columnBindingPolicyListByGraphColumnItem.Key, objectType, TypeComparisonCriteria.Subclass))
		//			foreach (var columnBindingPolicyListItem in columnBindingPolicyListByGraphColumnItem.Value)
		//			{
		//				graphColumn = columnBindingPolicyListItem.Key;

		//				if (columnBindingPolicyListItem.Value.Find(columnBindingPolicy => columnBindingPolicy.PropertyModel.Name == columnOrPropertyName) != null)
		//					if (!result.Contains(graphColumn))
		//						result.Add(graphColumn);
		//			}

		//	if (result.Count == 0)
		//	{
		//		// check if propertyName is same name as graph column name - default policy
		//		graphColumn = this.GetGraphColumn(columnOrPropertyName);

		//		if (graphColumn != null)
		//			result.Add(graphColumn);
		//	}

		//	return result;
		//}

        public GraphColumn GetGraphColumn(string columnName)
        {
			return this.Columns.FirstOrDefault(c => c.Name == columnName);
		}

        public GraphColumnBindingPolicy SetGraphColumnBindingPolicy(int columnIndex, IPropertyModel propertyModel)
        {
            return this.SetGraphColumnBindingPolicy(columnIndex, propertyModel, BindingOption.EnableEditing);
        }

        public GraphColumnBindingPolicy SetGraphColumnBindingPolicy(int columnIndex, IPropertyModel propertyModel, BindingOption bindingOption)
        {
            ISimpleObjectModel objectModel = propertyModel.Owner as ISimpleObjectModel;
			return this.SetGraphColumnBindingPolicy(columnIndex, objectModel.ObjectType, propertyModel, bindingOption);
        }

        public GraphColumnBindingPolicy SetGraphColumnBindingPolicy(int columnIndex, Type objectType, IPropertyModel propertyModel)
        {
            return this.SetGraphColumnBindingPolicy(columnIndex, objectType, propertyModel, (propertyModel.AccessPolicy == PropertyAccessPolicy.ReadOnly) ? BindingOption.DisableEditing : BindingOption.EnableEditing);
        }

        public GraphColumnBindingPolicy SetGraphColumnBindingPolicy(int columnIndex, Type objectType, IPropertyModel propertyModel, BindingOption bindingOption)
        {
            return this.SetGraphColumnBindingPolicy(columnIndex, objectType, propertyModel, null, bindingOption);
        }

        public GraphColumnBindingPolicy SetGraphColumnBindingPolicy(int columnIndex, Type objectType, IPropertyModel propertyModel, Func<GraphElement, object> getEditValue)
        {
            return this.SetGraphColumnBindingPolicy(columnIndex, objectType, propertyModel, getEditValue, BindingOption.EnableEditing);
        }

        public GraphColumnBindingPolicy SetGraphColumnBindingPolicy(int columnIndex, Type objectType, IPropertyModel propertyModel, Func<GraphElement, object> getEditValue, BindingOption bindingOption)
        {
			GraphColumnBindingPolicy result = null;
			GraphColumn graphColumn = this.Columns[columnIndex];
			int tableId = this.ObjectManager.GetObjectModel(objectType).TableInfo.TableId;
			HashArray<GraphColumnBindingPolicy> columnBindingPolicyByColumnIndex = this.columnBindingPolicyByColumnIndexByTableId[tableId];

			if (columnBindingPolicyByColumnIndex != null)
			{
				result = columnBindingPolicyByColumnIndex.GetValue(columnIndex);

				if (result == null)
				{
					result = new GraphColumnBindingPolicy(propertyModel, graphColumn, getEditValue, bindingOption);
					
					// Ovveride existing policy if any
					columnBindingPolicyByColumnIndex[columnIndex] = result;
					this.columnBindingPolicyByPropertyIndexByTableId[tableId][propertyModel.PropertyIndex] = result;
				}
			}
			else
			{
				result = new GraphColumnBindingPolicy(propertyModel, graphColumn, getEditValue, bindingOption);

				columnBindingPolicyByColumnIndex = new HashArray<GraphColumnBindingPolicy>(this.Columns.Max(item => item.Index) + 1);
				columnBindingPolicyByColumnIndex[columnIndex] = result;
				this.columnBindingPolicyByColumnIndexByTableId[tableId] = columnBindingPolicyByColumnIndex;

				int maxPropertyIndex = this.GetMaxPropertyIndex(tableId);
				HashArray<GraphColumnBindingPolicy> columnBindingPolicyByPropertyIndex = new HashArray<GraphColumnBindingPolicy>(maxPropertyIndex + 1);
				columnBindingPolicyByPropertyIndex[result.PropertyModel.PropertyIndex] = result;
				this.columnBindingPolicyByPropertyIndexByTableId[tableId] = columnBindingPolicyByPropertyIndex;
			}

			return result;
		}

		public bool GetGraphColumnEnableProperty(int columnIndex)
        {
			return this.GraphControlGetColumnEnableProperty(columnIndex);
        }

		public void SetGraphColumnEnableProperty(int columnIndex, bool value)
        {
			this.GraphControlSetColumnEnableProperty(columnIndex, value);
        }

		public bool GetGraphColumnVisibleProperty(int columnIndex)
		{
			return this.GraphControlGetColumnVisibleProperty(columnIndex);
		}

		public void SetGraphColumnVisibleProperty(int columnIndex, bool value)
		{
			this.GraphControlSetColumnVisibleProperty(columnIndex, value);
		}

		public bool GetGraphColumnShowInCustomizationFormProperty(int columnIndex)
		{
			return this.GraphControlGetColumnShowInCustomizationFormProperty(columnIndex);
		}

		public void SetGraphColumnShowInCustomizationFormProperty(int columnIndex, bool value)
		{
			this.GraphControlSetColumnShowInCustomizationFormProperty(columnIndex, value);
		}

		public void BestFitGraphColumns()
        {
            this.BestFitGraphColumns(applyAutoWidth: true);
        }

        public void BestFitGraphColumns(bool applyAutoWidth)
        {
            if (this.nodesByGraphElement.Count > 0)
            {
				this.GraphControlBestFitColumns(applyAutoWidth);
				this.RaiseAfterBestFitGraphColumns();
            }
        }

		public void SetColumnSorting(int columnIndex, SortOrder sortOrder)
		{
			this.GraphControlSetColumnSorting(columnIndex, sortOrder);
		}

		public void RemoveColumnSorting()
		{
			this.GraphControlBeginUpdate();

			this.GraphControlRemoveColumnSorting();
			this.GraphControlSetColumnSorting(this.columnOrderIndexHelper!.Index, SortOrder.Ascending);

			this.GraphControlEndUpdate();
			
			//this.GraphControlBeginUpdate();

			//this.GraphControlRemoveColumnSorting();

			//if (this.columnOrderIndexHelper != null)
			//{

			//	//foreach (KeyValuePair<GraphElement, object> item in this.NodesByGraphElement)
			//	//	this.SetCellEditValue(item.Key, item.Value, this.columnOrderIndexHelper.Name, item.Key.OrderIndex);

			//	this.GraphControlSetColumnSorting(this.columnOrderIndexHelper.Name, SortOrder.Ascending);

			//	//this.GraphControlRemoveColumnSorting();
			//}
			////else
			////{
			////	this.GraphControlRemoveColumnSorting();
			////}

			//this.GraphControlEndUpdate();

			this.SetButtonsSortingEnableProperty();
		}

        public bool ValidateAndSave()
        {
            bool isValid = true;

            if (this.CommitChangeOnFocusedNodeChange)
            {
                switch (this.SaveButonOption)
                {
                    case SaveButtonOption.CommitChanges:

                        TransactionResult transactionResult = this.ObjectManager.CommitChanges();

                        isValid = transactionResult.TransactionSucceeded;

                        break;

                    case SaveButtonOption.SaveFocusedNode:

                        isValid = this.ValidateAndTrySaveNodeIfRequired(this.FocusedGraphElement);
                        break;
                }
            }

            return isValid;
        }

        public bool ValidateAndTrySaveNodeIfRequired()
        {
            return this.ValidateAndTrySaveNodeIfRequired(this, this.FocusedNode);
        }

        public bool ValidateAndTrySaveNodeIfRequired(object requester)
        {
            return this.ValidateAndTrySaveNodeIfRequired(requester, this.FocusedNode);
        }

        public bool ValidateAndTrySaveNodeIfRequired(object requester, object node)
        {
            if (node == null)
                return true;

            this.GraphControlCloseEditor();

            GraphElement graphElement = this.GraphControlGetGraphElementByNode(node);
            GraphElementNodeTag nodeTag = this.GraphControlGetGraphNodeTag(node);

            if (graphElement.IsDeleted) // Due to dev express TreeList bug when deleting nodes
                return true;

            //if (this.anchorGraphElement != null && this.anchorGraphElement.IsNew) // "Graph Controller Anchor GraphElemnet is New and no need for saving node!
            //	return true;

            // if there is no property changes on the object retrun last validation result if exists.
            //if (graphElement.SimpleObject.ChangedPropertyNames.Count == 0) // && this.lastValidationNode == node && this.lastValidationNodeResult != null)
            //{
            //    if (!this.lastValidationNodeResult.IsValid)
            //    {
            //        this.SetGraphValidationError(this.lastValidationNodeResult);
            //    }

            //    return this.lastValidationNodeResult.IsValid;
            //}
            this.BindingObjectStoreData(graphElement);
			// First store data on attached events that binds focused object 

			//if (! graphElement.SimpleObject.RequireSaving())
			//{
			//    this.RaiseBindingObjectOnValidation(graphElement.SimpleObject, new GraphValidationResult(new SimpleObjectValidationResult(graphElement.SimpleObject, true, String.Empty, null), -1));
			//    return true;
			//}

			if (!this.ObjectManager.RequireSaving(graphElement))
				return true;

			if (!this.CommitChangeOnFocusedNodeChange)
                return true;

            bool isSaved = false;
            
            if (this.CheckUpdateValidationRules(graphElement.SimpleObject))
                isSaved = this.SaveNode(requester, graphElement);

            return isSaved;

   //         GraphValidationResult graphValidationResult = this.ValidateNode(graphElement);

   //         if (graphValidationResult.Passed)
   //         {
   //             var changeContainerValidation = this.ObjectManager.DefaultChangeContainer.Validate();

   //             if (!changeContainerValidation.Passed)
   //             {
   //                 graphValidationResult = new GraphValidationResult(changeContainerValidation, graphValidationResult.GraphNode, -1, changeContainerValidation.Message);
   //             }
   //             else
   //             {
   //                 if (!this.SaveNode(requester, graphElement))
   //                     graphValidationResult = new GraphValidationResult(graphValidationResult.SimpleObjectValidationResult, graphValidationResult.GraphNode, -1, "Validation passed, Saving failed!");
   //             }
   //         }

			//this.SetValidation(graphValidationResult);

			//return graphValidationResult.Passed;
        }

        public void BindingObjectStoreData(object node)
        {
            GraphElement graphElement = this.GetGraphElement(node);

            if (graphElement != null)
                this.BindingObjectStoreData(graphElement);
        }

        public void BindingObjectStoreData(GraphElement graphElement)
        {
            this.RaiseBindingObjectPushData(graphElement.SimpleObject);
        }

        private void SetValidation(GraphValidationResult graphValidationResult)
		{
            if (this.GetGraphElement(graphValidationResult.GraphNode) != null)
            {
                if (!graphValidationResult.Passed && graphValidationResult.ErrorGraphColumnIndex >= 0)
                {
                    this.SetGraphValidationError(graphValidationResult);
                }
                else if (this.isGraphErrorMessageSet)
                {
                    this.GraphControlResetColumnErrors();
                    this.isGraphErrorMessageSet = false;
                }

                this.RaiseBindingObjectOnValidation(graphValidationResult);

                if (!graphValidationResult.Passed && !graphValidationResult.Message.IsNullOrEmpty() && !graphValidationResult.SimpleObjectValidationResult.IsValidationErrorFormShown)
                {
                    string caption = graphValidationResult.SimpleObjectValidationResult.FailedValidationType.ToString() + " Validation Error"; // this.AppName

                    XtraMessageBox.Show(graphValidationResult.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    graphValidationResult.SimpleObjectValidationResult.IsValidationErrorFormShown = true;

                    if (graphValidationResult.ErrorGraphColumnIndex >= 0)
                    {
                        bool columnEnabled = this.GraphControlGetColumnEnableProperty(graphValidationResult.ErrorGraphColumnIndex);

                        if (columnEnabled)
                        {
                            this.GraphControlFocus();
                            this.GraphControlShowEditor();
                        }

                        return;
                    }
                }

                if (!graphValidationResult.Passed)
                    this.lastValidationFailedNode = graphValidationResult.GraphNode;

                this.lastValidationNodeResult = graphValidationResult;
            }
		}

		public void SetFocusedGraphElement(GraphElement graphElement)
        {
			object node = this.GetNode(graphElement);

			if (node == null && !this.LoadAllNodes)
			{
				// Expand selected GraphElement path nodes to force loading

				List<GraphElement> focusedGraphElementPathList = new List<GraphElement>();
				GraphElement parentGraphElement = graphElement.Parent;

				while (parentGraphElement != null)
				{
					focusedGraphElementPathList.Add(parentGraphElement);
					parentGraphElement = parentGraphElement.Parent;
				}

				for (int i = focusedGraphElementPathList.Count - 1; i >= 0; i--)
				{
					GraphElement graphElementToExpand = focusedGraphElementPathList[i];

					if (this.Contains(graphElementToExpand))
						this.ExpandGraphElement(focusedGraphElementPathList[i]);
				}

				node = this.GetNode(graphElement);
			}

            if (node != null)
                this.SetFocusedNode(node);
        }

        public void ExpandGraphElement(GraphElement graphElement)
        {
            object node = this.GetNode(graphElement);
            
            this.GraphControlExpandNode(node);
        }

        public void CollapseGraphElement(GraphElement graphElement)
        {
            object node = this.GetNode(graphElement);
            
            this.GraphControlCollapseNode(node);
        }

        public void ExpandAll()
        {
            this.GraphControlExpandAll();
        }

        public void CollapseAll()
        {
            this.GraphControlCollapseAll();
        }

        public void RefreshGraphElement(GraphElement graphElement)
        {
            object node = this.GetNode(graphElement);
            
            this.RefreshNode(graphElement, node);
        }

        //public void RefreshGraphElement(KeyValuePair<GraphElement, object> graphElementNodeKeyValuePair)
        public void RefreshGraphElement(KeyValuePair<GraphElement, object> graphElementNodeKeyValuePair)
        {
            this.RefreshNode(graphElementNodeKeyValuePair.Key, graphElementNodeKeyValuePair.Value);
        }

        public void RefreshAll()
        {
            foreach (var nodeGraphElementItem in this.nodesByGraphElement)
                this.RefreshNode(nodeGraphElementItem.Key, nodeGraphElementItem.Value);
        }

        public void SetImage(GraphElement graphElement, string imageName)
        {
            object node = this.GetNode(graphElement);

            GraphElementNodeTag nodeTag = this.GraphControlGetGraphNodeTag(node);
            nodeTag.ImageIndex = this.ImageList.Images.IndexOfKey(imageName);

            this.BeginGraphUpdate();
            this.GraphControlSetNodeImageIndex(node, nodeTag.ImageIndex);
            this.EndGraphUpdate();
        }

        public void SetStateImage(GraphElement graphElement, string stateImageName)
        {
            object node = this.GetNode(graphElement);

            GraphElementNodeTag nodeTag = this.GraphControlGetGraphNodeTag(node);
            nodeTag.StateImageIndex = this.StateImageList.Images.IndexOfKey(stateImageName);

            this.BeginGraphUpdate();
            this.GraphControlSetNodeStateImageIndex(node, nodeTag.StateImageIndex);
            this.EndGraphUpdate();
        }

        public void SetGraphElementCheckedProperty(GraphElement graphElement, bool value)
        {
            object node = this.GetNode(graphElement);
            
            this.SetGraphNodeCheckedProperty(node, value);
        }

        public void SetGraphElementGreyedProperty(GraphElement graphElement, bool value)
        {
            object node = this.GetNode(graphElement);
            
            this.SetGraphNodeCheckDisabled(node, value);
        }

		public object? GetNode(GraphElement? graphElement)
        {
            if (graphElement == null)
                return null;

			object? node = null;

			this.nodesByGraphElement.TryGetValue(graphElement, out node);

			return node;
        }

		public object GetNode(SimpleObject simpleObject)
		{
			if (simpleObject == null)
				return null;

			foreach (GraphElement graphElement in simpleObject.GraphElements)
			{
				object node = this.GetNode(graphElement);

				if (node != null)
					return node;
			}

			return null;
		}

		//public GraphElement FindGraphElement(SimpleObject simpleObject)
		//{
		//	if (simpleObject == null)
		//		return null;

		//	if (simpleObject is GraphElement)
		//	{
		//		if (this.nodesByGraphElement.ContainsKey(simpleObject as GraphElement))
		//			return simpleObject as GraphElement;

		//		return null;
		//	}

		//	foreach (GraphElement graphElement in simpleObject.GraphElements)
		//		if (this.nodesByGraphElement.ContainsKey(graphElement))
		//			return graphElement;

		//	return null;
		//}

		//public object GetFocusedNode()
		//{
		//	return this.FocusedNode;
		//}

		public object GetCellEditValue(object node, int columnIndex)
		{
			return this.GraphControlGetNodeCellEditValue(node, columnIndex);
		}
		
		public void SetCellEditValue(GraphElement graphElement, int columnIndex, object editValue)
        {
            object node = this.GetNode(graphElement);
            
            this.SetCellEditValue(node, columnIndex, editValue);
        }

        public void SetCellEditValue(object node, int columnIndex, object? editValue)
        {
            this.GraphControlSetNodeCellEditValue(node, columnIndex, editValue);
        }

        public void SetButtonsEnableProperty()
        {
            this.SetButtonsEnableProperty(this.FocusedGraphElement);
        }

        public bool Contains(GraphElement graphElement)
        {
            return this.nodesByGraphElement.ContainsKey(graphElement);
        }

        private bool Contains(SimpleObject simpleObject)
        {
            foreach (GraphElement graphElement in simpleObject.GraphElements)
                if (graphElement != null && this.Contains(graphElement))
                    return true;

            return false;
        }

        public GraphElement FindGraphElementBySimpleObject(SimpleObject simpleObject)
		{
			GraphElement graphElement = null;

			if (simpleObject is GraphElement)
			{
				if (this.Contains(simpleObject as GraphElement))
					graphElement = simpleObject as GraphElement;
			}
			else
			{
				foreach (GraphElement item in simpleObject.GraphElements)
				{
					if (this.Contains(item))
					{
						graphElement = item;
						break;
					}
				}
			}

			return graphElement;
		}

		public bool FindNextText(object startNode, string textToFind, bool matchCase)
		{
			object node = null;

			if (this.findStartFromBegining)
			{
				object[] childNodes = this.GraphControlGetChildNodes(null);

				if (childNodes.Length > 0)
				{
					node = childNodes[0];
				}

				this.findStartFromBegining = false;
			}
			else
			{
				node = startNode;
			}

			if (node == null)
				return false;

			while (node != null)
			{
				if (this.FindNextTextInAnyChildNodes(node, textToFind, matchCase))
					return true;

				node = this.GetNextNode(node);
			}

			this.findStartFromBegining = true;
			
			return false;
		}

		public void SetColumnsAndButtonsEnableProperty(GraphElement? graphElement)
		{
			if (this.EditMode != GraphEditMode.Select && this.EditMode != GraphEditMode.ViewOnly)
			{
				this.SetColumnsEnableProperty(graphElement);
				this.SetButtonsEnableProperty(graphElement);
			}
		}

		#endregion |   Public Methods   |

		#region |   Protected GraphControl Methods   |

		protected void GraphControlButtonAddIsClicked(Component button)
        {
			this.GraphControlTryAddNewObjectByButtonClick(button);
        }

		protected AddNewObjectResult GraphControlTryAddNewObjectByButtonClick(Component button)
        {
            return this.GraphControlTryAddNewObjectByButtonClick(button, this.FocusedGraphElement);
        }

        protected AddNewObjectResult GraphControlTryAddNewObjectByButtonClick(Component button, GraphElement parentGraphElement)
        {
			if (!this.IsActive)
				return new AddNewObjectResult(null, false);
			
			AddNewObjectResult result;
            AddButtonPolicy<GraphElement> addButtonPolicy = this.AddButtonPolicyList.FirstOrDefault(a => a.Buttons.Contains(button));

            if (addButtonPolicy != null)
            {
                Cursor currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;

                GraphElement realParentGraphElement = this.GetParentGraphElementForNewGraphInsertion(parentGraphElement, addButtonPolicy);
				AllowAddButtonPolicytEventArgs args = this.RaiseButtonAddClicked(button, addButtonPolicy, realParentGraphElement, allow: true);

                if (args == null || args.Allow)
				{
					result = this.GraphControlTryAddNewObject(addButtonPolicy, realParentGraphElement);
				}
				else
				{
					result = new AddNewObjectResult(null, false);
				}

                Cursor.Current = currentCursor;
            }
            else
            {
                result = new AddNewObjectResult(null, false);
                throw new ArgumentException("Object type is not specified for button " + button.ToString());
            }

            return result;
        }

		protected AddNewObjectResult GraphControlTryAddNewObject(AddButtonPolicy<GraphElement> addButtonPolicy, GraphElement parentGraphElement)
        {
			if (!this.IsActive)
				return new AddNewObjectResult(null, false);

			object node = this.GraphControlGetFocusedNode();
			bool validation = this.ValidateAndTrySaveNodeIfRequired(this, node);
			AddNewObjectResult result;

			if (validation)
			{
				result = this.AddNewObject(addButtonPolicy, parentGraphElement);
			}
			else
			{
				result = new AddNewObjectResult(null, false);
			}

			return result;
        }

        protected void GraphControlBeforeNodeIsFocused(object node, object oldNode, ref bool canFocus)
        {
			if (node == null)
				return;

			if (this.EditMode == GraphEditMode.Select || this.EditMode == GraphEditMode.ViewOnly)
                return;

            if (oldNode != null)
            {
                GraphElementNodeTag oldNodeTag = this.GraphControlGetGraphNodeTag(oldNode);

				// If deleting node is in progress, oldNodeTag.GraphElement is null.
				if (oldNodeTag.GraphElement != null && !oldNodeTag.GraphElement.DeleteStarted) // && (oldNodeTag.GraphElement.RequireSaving() || oldNodeTag.GraphElement.SimpleObject.RequireSaving()))
				{
					GraphElementNodeTag nodeTag = this.GraphControlGetGraphNodeTag(node);

					if (nodeTag.CanBeFocusedWithoutSavingOldNode)
					{
						canFocus = true;
						nodeTag.CanBeFocusedWithoutSavingOldNode = false;
					}
					else
					{
						canFocus = this.ValidateAndTrySaveNodeIfRequired(this, oldNode);
					}
				}
                //                this.lastValidationFailedNode = canFocus ? null : oldNode;
            }
            else
            {
                if (this.lastValidationFailedNode != null)
                {
                    this.ValidateAndTrySaveNodeIfRequired(null, this.lastValidationFailedNode);
                    canFocus = (this.lastValidationFailedNode == node) ? true : canFocus;
                }
            }
        }

        protected void GraphControlFocusedNodeIsChanged(object? node, object? oldNode)
        {
            GraphElement? graphElement = this.GraphControlGetGraphElementByNode(node);
			SimpleObject? simpleObject = (graphElement != null) ? graphElement.SimpleObject : null;

			if (this.bindingObject != simpleObject)
			{
				this.bindingObject = simpleObject;
                this.SetColumnsAndButtonsEnableProperty(graphElement);
                this.RaiseBindingObjectChange(simpleObject);
			}
		}

		protected void GraphControlCellValueIsChanged(object node, int columnIndex, object value)
        {
            GraphElement graphElement = this.GraphControlGetGraphElementByNode(node);
            SimpleObject simpleObject = graphElement.SimpleObject;
            GraphColumnBindingPolicy columnBindingPolicy = this.GetGraphColumnBindingPolicyByColumnIndex(simpleObject.GetModel().TableInfo.TableId, columnIndex);

            // if no EditorBindings handle of this property than set editor property value (if EditorBindings contains property binding no any action needed)
            if (columnBindingPolicy != null && columnBindingPolicy.PropertyModel.AccessPolicy != PropertyAccessPolicy.ReadOnly && 
                !this.EditorBindings.ContainsPropertyBinding(graphElement.SimpleObject.GetType(), columnBindingPolicy.PropertyModel))
			{
                object propertyValue = value;
				Component editorComponent = this.GraphControlGetEditorComponent(columnIndex);

                if (editorComponent != null)
                    if (!this.EditorBindings.TryGetPropertyValueFromControlValue(value, editorComponent, out propertyValue))
                        propertyValue = value;
                        
                object normalizedPropertyValue = this.EditorBindings.GetNormalizedPropertyValue(columnBindingPolicy.PropertyModel, propertyValue);
                
                simpleObject.SetPropertyValue(columnBindingPolicy.PropertyModel.PropertyIndex, normalizedPropertyValue, requester: this);
            }
        }

        protected void GraphControlBeforeNodeIsCollapsed(object node, ref bool canCollapse)
        {
            canCollapse = this.lastValidationNodeResult.Passed;

            if (canCollapse)
            {
                //// Make sure that node is validated.
                //object focusedNode = this.GraphControlGetFocusedNode();
                
                //if (focusedNode != null)
                //    canCollapse = this.ValidateAndTrySaveNodeIfRequired(this, focusedNode);
            }
            else
            {
                // Allow collapsing nodes but prohibit colapsing any of the parent error nodes.
                GraphElement failedGraphElement = this.GraphControlGetGraphElementByNode(this.lastValidationFailedNode);
                GraphElement graphElementToBeExpanded = this.GraphControlGetGraphElementByNode(node);

                canCollapse = !failedGraphElement.HasAsParent(graphElementToBeExpanded);
            }

            if (canCollapse)
            {
                GraphElement graphElement = this.GetGraphElement(node);
                GraphElementNodeTag nodeTag = this.GetGraphNodeTag(graphElement);

                nodeTag.Expanded = false;
            }
        }

        protected void GraphControlBeforeNodeIsExpanded(object node)
        {
			this.LoadChildNodesIfNotLoaded(node);
        }

        protected void GraphControlNodeIsExpanded(object node)
        {
            GraphElement graphElement = this.GetGraphElement(node);
            string imageName = graphElement.GetImageName();
            string expandedImageName = ImageControl.ImageNameAddOption(imageName, ImageControl.ImageOptionExpanded, insertionPosition: 1);
            int expandedImageIndex = this.ImageList.Images.IndexOfKey(expandedImageName);

            if (expandedImageIndex >= 0)
            {
                GraphElementNodeTag nodeTag = this.GraphControlGetGraphNodeTag(node);

                nodeTag.ExpandedImageIndex = expandedImageIndex;
                //this.GraphControlSetNodeExpandedImageIndex(node, expandedImageIndex);
                this.GraphControlSetNodeImageIndex(node, expandedImageIndex);
            }
        }

        protected void GraphControlNodeIsCollapsed(object node)
        {
            GraphElement graphElement = this.GetGraphElement(node);
            string imageName = graphElement.GetImageName();
            int imageIndex = this.ImageList.Images.IndexOfKey(imageName);

            GraphElementNodeTag nodeTag = this.GraphControlGetGraphNodeTag(node);
            nodeTag.ImageIndex = imageIndex;

            this.GraphControlSetNodeImageIndex(node, imageIndex);
        }

        protected void GraphControlNodeCheckStateIsChanged(object node, bool checkValue, ref bool canCheck)
        {
            GraphElement graphElement = this.GetGraphElement(node);
            CheckGraphElementEventArgs checkGraphElementArgs = this.RaiseBeforeCheckGraphElementEvent(graphElement, checkValue, canCheck);

            if (checkGraphElementArgs != null && checkGraphElementArgs.CanCheck == false)
                return;

            if (this.ManyToManyRelationKey > 0)
            {
                GraphElementNodeTag nodeTag = this.GraphControlGetGraphNodeTag(node);
				IObjectRelationModel objectRelationModel = nodeTag.GraphElement.SimpleObject.GetModel().RelationModel; // this.ObjectManager.GetObjectRelationModel(nodeTag.GraphElement.SimpleObject.GetType());
                IManyToManyRelationModel manyToManyRelationModel = null;

                if (this.ChangableBindingObjectControlFocusedSimpleObject != null)
                {
                    manyToManyRelationModel = objectRelationModel.AsFirstObjectInManyToManyRelations[this.ManyToManyRelationKey];

                    if (manyToManyRelationModel == null)
                        manyToManyRelationModel = objectRelationModel.AsSecondObjectInManyToManyRelations[this.ManyToManyRelationKey];

                    //if (objectRelationModel.ManyToManyFirstObjects.ContainsKey(this.ManyToManyRelationKey)) //   ManyToManyRelationFirstObjectTypeDictionary.ContainsKey(this.ManyToManyRelationKey))
                    //{
                    //	manyToManyRelationModel = objectRelationModel.ManyToManyFirstObjects[this.ManyToManyRelationKey];
                    //}
                    //else if (objectRelationModel.ManyToManySecondObjects.ContainsKey(this.ManyToManyRelationKey))
                    //{
                    //	manyToManyRelationModel = objectRelationModel.ManyToManySecondObjects[this.ManyToManyRelationKey];
                    //}

                    canCheck = (manyToManyRelationModel != null) ? canCheck &= true : false;

                    if (canCheck && nodeTag.Checked != checkValue)
                    {
                        GroupMembership groupMembership = null;

                        //IList focusedBindingObjectManyToManyObjectCollection = (this.ChangableBindingObjectControlFocusedSimpleObject as IBindingSimpleObject).GetGroupMemberCollection(manyToManyRelationModel.RelationKey);

                        if (checkValue)
                        {
                            groupMembership = new GroupMembership(this.ObjectManager, this.ManyToManyRelationKey, this.ChangableBindingObjectControlFocusedSimpleObject, nodeTag.GraphElement.SimpleObject, requester: this);
                            nodeTag.GroupMembership = groupMembership;
                            //focusedBindingObjectManyToManyObjectCollection.Add(nodeTag.GraphElement.SimpleObject);
                        }
                        else
                        {
                            //groupMembership = this.ObjectManager.GetObject(GroupMembershipModel.TableId, nodeTag.GroupMembershipId) as GroupMembership;
                            //groupMembership = this.ObjectManager.GetGroupMembership(this.ManyToManyRelationKey, this.ChangableBindingObjectControlFocusedSimpleObject, nodeTag.GraphElement.SimpleObject);
                            nodeTag.GroupMembership?.RequestDelete();

                            //focusedBindingObjectManyToManyObjectCollection.Remove(nodeTag.GraphElement.SimpleObject);
                        }

                        this.SetGraphNodeCheckedProperty(node, checkValue);

                        if (!this.ChangableBindingObjectControlFocusedSimpleObject.IsChanged)
                            this.ObjectManager.CommitChanges();
                    }
                }
            }
            else
            {
                this.SetGraphNodeCheckedProperty(node, checkValue);
            }
        }

		public void Initialize()
		{
			if (!this.isInitialized)
			{
                this.columnOrderIndexHelper = new GraphColumn(this.columnNameOrderIndexHelper);
                this.Columns.Add(this.columnOrderIndexHelper);
                this.columnOrderIndexHelper.Visible = false;
                this.columnOrderIndexHelper.ShowInCustomizationForm = false;

                if (this.ImageList != null)
                    this.GraphControlSetImageList(this.ImageList);

                if (this.StateImageList != null)
                    this.GraphControlSetStateImageList(this.StateImageList);
                    
                if (this.CheckBoxImageList != null)
                    this.GraphControlSetCheckBoxImageList(this.CheckBoxImageList);
				
                this.GraphControlSetEditMode(this.EditMode);
				this.GraphControlSetLookAndFeelStyle(this.LookAndFeelStyle);
				this.GraphControlSetCheckBoxMode(this.IsCheckBoxMode);
				this.GraphControlSetCanDragAndDrop(this.CanDragAndDrop);
                this.SetButtonsSortingEnableProperty();

                if (!this.isImagesLoaded && this.ImageList != null && this.ImageList.Images.Count == 0)
                {
                    this.ImageList = ImageControl.SmallImageCollection;
                    //ImageControl.LoadImages(this.ImageList, ImageControl.SmallImageCollection);
                    this.isImagesLoaded = true;
                }

                if (!this.isStateImagesLoaded && this.StateImageList != null && this.StateImageList.Images.Count == 0)
                {
                    this.StateImageList = ImageControl.StateImageCollection;
                    //ImageControl.LoadImages(this.StateImageList, ImageControl.StateImageCollection);
                    this.isStateImagesLoaded = true;
                }

                if (!this.isCheckBoxImagesLoaded && this.CheckBoxImageList != null && this.CheckBoxImageList.Images.Count == 0)
                {
                    this.CheckBoxImageList = ImageControl.CheckBoxImageCollection;
                    //ImageControl.LoadImages(this.CheckBoxImageList, ImageControl.CheckBoxImageCollection);
                    this.isCheckBoxImagesLoaded = true;
                }

                this.isInitialized = true;
			}
		}

        protected bool GraphControlCanDragNode(object node)
        {
            return (this.CanDragAndDrop) ? node != null : false;
        }

        protected DragDropEffects GraphControlGetDragDropEffect(object dragNode, object? parentNode)
        {
            if (dragNode == parentNode)
                return DragDropEffects.None;

            bool canChangeParent = false;
            
            if (parentNode is not null)
                canChangeParent = this.CanGraphNodeChangeParent(dragNode, parentNode);

            if (canChangeParent)
                return DragDropEffects.Move;
            else
                return DragDropEffects.None;
        }

        protected void GraphControlDragDrop(object draggedNode, object? parentNode)
        {
			GraphElement? dragGraphElement = this.GetGraphElement(draggedNode);
			GraphElement? parentGraphElement = (parentNode != null) ? this.GetGraphElement(parentNode) : this.anchorGraphElement;
            //bool isChangedBefore = dragGraphElement.IsChanged;

            if (dragGraphElement != null)
            {
                dragGraphElement.Requester = this;
                dragGraphElement.Parent = parentGraphElement;
            }

			if (parentGraphElement != null && this.Contains(parentGraphElement))
				this.ExpandGraphElement(parentGraphElement);

			this.SetColumnsAndButtonsEnableProperty(dragGraphElement);
			
            if (dragGraphElement != null && dragGraphElement.SimpleObject != null)
                this.RaiseBindingObjectRefreshContext(dragGraphElement.SimpleObject, null, null, null, false, false, requester: this);

			//if (!dragGraphElement.IsNew && !isChangedBefore && dragGraphElement.IsChanged) 
			//	dragGraphElement.Save();
		}

        protected void GraphControlAfterDropNode(object node, int nodeIndex)
		{
            GraphElement? graphElement = this.GetGraphElement(node);

            if (graphElement.OrderIndex != nodeIndex)
            {
                graphElement.Requester = this;
                graphElement.OrderIndex = nodeIndex;
            }
        }

        protected int GraphControlCompareColumnNodeValues(GraphColumn graphColumn, object node1, object node2, object nodeValue1, object nodeValue2, SortOrder sortOrder, int result)
        {
			if (graphColumn == this.columnOrderIndexHelper)
			{
				GraphElement? graphElement1 = this.GetGraphElement(node1);
				GraphElement? graphElement2 = this.GetGraphElement(node2);

				if (graphElement1 != null && graphElement2 != null)
                    return graphElement1.OrderIndex - graphElement2.OrderIndex;
                else
					return StringLogicalComparer.CompareObject(nodeValue1, nodeValue2);
			}
			else
			{
				CompareGraphColumnNodeValuesEventArgs? compareGraphColumnNodeValuesEventArgs = this.GetColumnNodeValuesCompareByEvent(graphColumn, node1, node2, nodeValue1, nodeValue2, sortOrder, result);

				if (compareGraphColumnNodeValuesEventArgs != null)
					return compareGraphColumnNodeValuesEventArgs.Result;
				else
					return StringLogicalComparer.CompareObject(nodeValue1, nodeValue2);
			}
        }

        protected bool CanGraphNodeChangeParent(object node, object newParentNode)
        {
            GraphElement? graphElement = this.GetGraphElement(node);

            if (graphElement != null)
            {
				GraphElement? newParentGraphElement = (newParentNode != null) ? this.GetGraphElement(newParentNode) : this.anchorGraphElement;

                if (graphElement.Parent == newParentGraphElement)
                    return false;
                else
					return this.ObjectManager!.CanGraphElementChangeParent(graphElement, newParentGraphElement);
            }

            return false;
        }

		protected virtual void SetButtonsEnableProperty(GraphElement graphElement)
		{
			bool enabled;

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

            Dictionary<GraphElement, Dictionary<Type, bool>> canAddGraphElementByParentGraphElementByObjectType = new Dictionary<GraphElement, Dictionary<Type, bool>>();
            Dictionary<Type, bool> canAddGraphElementByNullParentGraphElementByObjectType = new Dictionary<Type, bool>();

            // Add buttons
            foreach (AddButtonPolicy<GraphElement> addButtonPolicy in this.AddButtonPolicyList)
			{
				GraphElement? parentGraphElementForInsertion = this.GetParentGraphElementForNewGraphInsertion(graphElement, addButtonPolicy);

                //if (addButtonPolicy.ObjectType.Name == "SecondaryIpAddress")
                //	enabled = false;


                if (parentGraphElementForInsertion == this.anchorGraphElement && addButtonPolicy.IsSubButton)
                    enabled = false;
                else if (this.ObjectManager != null)
                    enabled = this.ObjectManager.CanAddGraphElement(this.GraphKey, addButtonPolicy.ObjectType, parentGraphElementForInsertion);
                else
                    enabled = false;

				foreach (Component button in addButtonPolicy.Buttons)
					this.SetButtonEnablePropertyInternal(button, enabled);
			}
             
			// MoveUp/Down buttons
			this.SetButtonsMoveUpEnableProperty(graphElement);
			this.SetButtonsMoveDownEnableProperty(graphElement);
			
			// Save & RejectChanges buttons
			this.SetButtonsSaveEnableProperty(graphElement);
			this.SetButtonsRejectChangesEnableProperty(graphElement);

			// Delete buttons
			enabled = graphElement != null && this.ObjectManager.CanDelete(graphElement);

			foreach (Component buttonDelete in this.ButtonRemoveList)
				this.SetButtonEnablePropertyInternal(buttonDelete, enabled);

			this.RaiseAfterSetButtonsEnableProperty();
		}

        protected virtual void OnInitializeComponent()
        {
        }

        protected virtual void OnDispose()
        {
        }

		#endregion |   Protected GraphControl Methods   |

		#region |   Private GraphControl Methods   |

		private void LoadChildNodesIfNotLoaded(object node)
		{
			if (!this.LoadAllNodes)
			{
				GraphElementNodeTag nodeTag = this.GraphControlGetGraphNodeTag(node);
				GraphElement graphElement = nodeTag.GraphElement;

				bool doLoad = false;

				lock (lockChildNodeLoading)
				{
					if (!nodeTag.ChildrenNodesLoaded && !nodeTag.ChildrenNodesLoading)
					{
						doLoad = true;
						nodeTag.ChildrenNodesLoading = true;
					}
				}

				if (doLoad)
				{
					Cursor? currentCursor = Cursor.Current;
					Cursor.Current = Cursors.WaitCursor;

                    if (graphElement.HasChildren)
                    {
                        var childGraphElements = this.ObjectManager!.GetGraphElements(this.GraphKey, parentGraphElementId: graphElement.Id);

                        this.BeginGraphUpdate();
                        this.isLoadingInProgress = true;

                        for (int i = 0; i < childGraphElements.Count(); i++)
                        {
                            GraphElement childGraphElement = childGraphElements.ElementAt(i);
							// Check if node already exists while on move - preventing double loading same node
							if (!this.nodesByGraphElement.ContainsKey(childGraphElement))
                                this.Load(childGraphElement, parentGraphElement: graphElement);

                            //// Try to speed up loading
                            //object childNode = this.GetNode(childGraphElement);
                            //GraphNodeTag childNodeTag = this.GraphControlGetGraphNodeTag(childNode);

                            //if (!childNodeTag.ChildNodesLoaded)
                            //{
                            //    foreach (GraphElement childOfChildGraphElement in childGraphElement.GraphElements)
                            //    {
                            //        if (!this.nodesByGraphElement.ContainsKey(childOfChildGraphElement))
                            //        {
                            //            this.Load(childOfChildGraphElement, childGraphElement);
                            //        }
                            //    }
                            //}

                            //childNodeTag.ChildNodesLoaded = true;
                        }

                        lock (lockChildNodeLoading)
                        {
                            nodeTag.ChildrenNodesLoading = false;
                            nodeTag.ChildrenNodesLoaded = true;
                        }

                        this.isLoadingInProgress = false;
                        this.BestFitGraphColumns();
                        this.EndGraphUpdate();

                        //Thread thread = new Thread(() => this.LoadChildGraphElements(graphElement.GraphElements));
                        //thread.IsBackground = true;
                        //thread.Priority = ThreadPriority.Lowest;
                        //thread.Start();
                    }

					Cursor.Current = currentCursor;

					// TODO: Otvoriti novi thread i loadati child of child graph elemente da je grananje brže!!!!!
				}

				nodeTag.Expanded = true;

				//Thread thread = new Thread(() => this.LoadChildGraphElements(graphElement.GraphElements, showWaitCursor: true));
				//thread.IsBackground = true;
				//thread.Priority = ThreadPriority.Lowest;
				//thread.Start();
			}
		}

		//private void LoadChildGraphElements(IEnumerable<GraphElement> graphElements)
		//{
		//	if (this.GraphControl!.InvokeRequired)
		//		this.GraphControl.FindForm().Invoke(new MethodInvoker(() => OnLoadChildGraphElements(graphElements)));
		//	else
		//		this.OnLoadChildGraphElements(graphElements);
		//}

		//private void OnLoadChildGraphElements(IEnumerable<GraphElement> graphElements)
		//{
		//	foreach (GraphElement graphElement in graphElements)
		//	{
		//		bool doLoad = false;
		//		object? childNode = this.GetNode(graphElement);
		//		GraphElementNodeTag childNodeTag = this.GraphControlGetGraphNodeTag(childNode);

		//		lock (lockChildNodeLoading)
		//		{
		//			if (!childNodeTag.ChildNodesLoaded && !childNodeTag.ChildNodesLoading)
		//			{
		//				doLoad = true;
		//				childNodeTag.ChildNodesLoading = true;
		//			}
		//		}

		//		if (doLoad)
		//		{
		//			//this.GraphControlBeginUpdate();
		//			this.isLoadingInProgress = true;

		//			foreach (GraphElement childGraphElement in graphElement.GraphElements)
		//				if (!this.nodesByGraphElement.ContainsKey(childGraphElement))
		//					this.Load(childGraphElement, graphElement);

		//			lock (lockChildNodeLoading)
		//			{
		//				childNodeTag.ChildNodesLoading = false;
		//				childNodeTag.ChildNodesLoaded = true;
		//			}

		//			this.isLoadingInProgress = false;
		//			//this.GraphControlEndUpdate();
		//		}
		//	}

		//	this.BestFitGraphColumns();
		//}

		#endregion |   Private GraphControl Methods   |

		#region |   Protected Methods   |

		protected void GraphControlButtonMoveUpIsClicked(Component button)
		{
            //if (this.FocusedGraphElement != null)
            //{
            //	this.FocusedGraphElement.MoveUp();

            //	if (!this.FocusedGraphElement.IsNew)
            //		this.FocusedGraphElement.Save();
            //}


            //Thread thread = new Thread(() => this.GraphControlButtonMoveAction(graphElement => graphElement.MoveUp()));
            //thread.IsBackground = true;
            //thread.Priority = ThreadPriority.Normal;
            //thread.Start();


            this.GraphControlButtonMoveAction(graphElement => graphElement.MoveUp(), requester: button);
		}

		protected void GraphControlButtonMoveDownIsClicked(Component button)
		{
			//if (this.FocusedGraphElement != null)
			//{ 
			//	this.FocusedGraphElement.MoveDown();

			//	if (!this.FocusedGraphElement.IsNew)
			//		this.FocusedGraphElement.Save();
			//}


			//Thread thread = new Thread(() => this.GraphControlButtonMoveAction(graphElement => graphElement.MoveDown()));
			//thread.IsBackground = true;
			//thread.Priority = ThreadPriority.Normal;
			//thread.Start();

			this.GraphControlButtonMoveAction(graphElement => graphElement.MoveDown(), requester: button);
		}

		protected void GraphControlButtonRemoveColumnSortingIsClicked(Component button)
		{
			if (!this.IsActive)
				return;

			Cursor? currentCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;

			this.RemoveColumnSorting();

			Cursor.Current = currentCursor;
		}

		protected void GraphControlButtonSaveIsClicked(Component button)
        {
			if (!this.IsActive)
				return;

			Cursor? currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            if (this.SaveButonOption == SaveButtonOption.CommitChanges)
            {
				TransactionResult transactionResult = this.ObjectManager.CommitChanges();

				if (!transactionResult.TransactionSucceeded && !transactionResult.ValidationResult.IsValidationErrorFormShown)
				{
					string caption = transactionResult.ValidationResult.FailedValidationType.ToString() + " Validation Error";

					XtraMessageBox.Show(transactionResult.ValidationResult.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					transactionResult.ValidationResult.IsValidationErrorFormShown = true;
				}

				////if (this.EditorBindings != null && this.EditorBindings.ObjectManager != null)
				//bool canCommit = true;
    //            object node = this.GraphControlGetFocusedNode();
    //            GraphElement graphElement = null; 
                
    //            if (node != null)
    //                graphElement = this.GraphControlGetGraphElementByNode(node);

    //            if (graphElement != null)
    //                canCommit = this.CheckUpdateValidationRules(graphElement.SimpleObject);

    //            if (canCommit)
    //                this.EditorBindings.ObjectManager.CommitChanges();
            }
            else if (this.SaveButonOption == SaveButtonOption.SaveFocusedNode)
            {
                object? node = this.GraphControlGetFocusedNode();

                if (node != null)
                    this.ValidateAndTrySaveNodeIfRequired(this, node);
            }

            Cursor.Current = currentCursor;
        }

		protected void GraphControlButtonRejectChangesIsClicked(Component button)
		{
			if (!this.IsActive)
				return;

			Cursor? currentCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;

			object? node = this.GraphControlGetFocusedNode();
			GraphElement? graphElement = this.GetGraphElement(node);

			if (graphElement != null && graphElement.SimpleObject != null)
			{
				graphElement.SimpleObject.RejectChanges();
				graphElement.RejectChanges();
			}

			Cursor.Current = currentCursor;
		}

		//TODO: create event to be called to determine if ask before delete is needed to customize on specific requirements, 
		//		e.g. interface are allowed to be deleted without ask but devices should be asked before delete
		protected void GraphControlButtonRemoveIsClicked(Component button)
        {
			if (!this.IsActive)
				return;

			object? node = this.GraphControlGetFocusedNode();
            GraphElement? graphElement = (node != null) ? this.GetGraphElement(node) : null;

            if (graphElement != null)
            {
				bool canDelete = true;
				// Calling ObjectManager.CanDelete is avoided to speed up procesing since remove buttons already had checked to CanDelete before can be clicked
				// bool canDelete = this.ObjectManager.CanDelete(graphElement);

				bool askBeforeDelete = true;
				
				if (this.AskBeforeDelete != null)
				{
					AskBeforeDeleteGraphElementEventArgs? askBeforeDeleteEventArgs = this.RaiseAskBeforeDeleteEvent(graphElement, InputUserSource.Button, askBeforeDelete);
					
                    askBeforeDelete = (askBeforeDeleteEventArgs != null) ? askBeforeDeleteEventArgs.AskBeforeDelete : false;
				}
				else
				{
					askBeforeDelete = this.AskBeforeDeleteOnButtonRemoveClick || (graphElement.SimpleObject != null && graphElement.SimpleObject.GraphElements.Count > 1);
				}
				
				if (askBeforeDelete)
                {
                    ISimpleObjectModel simpleObjectModel = (graphElement.SimpleObject != null) ? graphElement.SimpleObject.GetModel() : GraphElementModel.Instance;
                    string? name = (graphElement.SimpleObject != null) ? graphElement.SimpleObject?.GetName() : "GraphElement without SimpleObject";

                    canDelete = XtraMessageBox.Show(String.Format("Do you want to delete {0} '{1}'?", simpleObjectModel.ObjectTypeCaption, name),
													"Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes;
                }

                if (canDelete)
                    this.DeleteGraphElement(graphElement, requester: this);
            }

            //this.RemoveNodeInternal(node);
        }

        protected void OnKeyDown(object? sender, KeyEventArgs e)
        {
			if (!this.IsActive)
				return;

			if (e.KeyCode == Keys.Delete)
            {
                GraphElement? graphElement = this.GetGraphElement(this.FocusedNode);

                if (graphElement != null)
                {
                    bool canDelete = this.ObjectManager!.CanDelete(graphElement);

					if (canDelete)
                    {
						bool askBeforeDelete = true;

						if (this.AskBeforeDelete != null)
						{
							AskBeforeDeleteGraphElementEventArgs? askBeforeDeleteEventArgs = this.RaiseAskBeforeDeleteEvent(graphElement, InputUserSource.Keyboard, askBeforeDelete);
							
                            
                            askBeforeDelete = askBeforeDeleteEventArgs!.AskBeforeDelete;
						}

						if (askBeforeDelete)
						{
                            ISimpleObjectModel simpleObjectModel = graphElement.SimpleObject!.GetModel();
							
                            canDelete = XtraMessageBox.Show(String.Format("Do you want to delete {0} {1}?", simpleObjectModel.Caption, graphElement.SimpleObject.GetName()),
														    "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK;
						}
                    }

                    if (canDelete)
                        this.DeleteGraphElement(graphElement, requester: this);
                }
            }
        }

        protected void DeleteGraphElement(GraphElement graphElement, object requester)
        {
            if (!this.CheckDeleteValidationRules(graphElement.SimpleObject!))
                return;

            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            //if (graphElement.SimpleObject != null)
            //{
                bool changeImage = true;

                //graphElement.SimpleObject.RequestDelete();
                graphElement.RequestDelete(requester);

                if (this.CommitChangeOnDeleteRequest)
                    changeImage = !graphElement.Manager.CommitChanges(this).TransactionSucceeded;

                if (changeImage)
                {
                    string imageName = graphElement.GetImageName();
                    string newImageName = ImageControl.ImageNameAddOption(imageName, ImageControl.ImageOptionRemoveExt, insertionPosition: 1);
                    int imageIndex = this.ImageList.Images.IndexOfKey(newImageName);

                    if (imageIndex > 0)
                    {
                        object node = this.GetNode(graphElement);

                        if (node != null)
                            this.GraphControlSetNodeImageIndex(node, imageIndex);
                    }
                }
            //}

            Cursor.Current = currentCursor;
        }

        //protected void SetAddButtonPolicy(Component button, Type objectType)
        //{
        //    this.SetAddButtonPolicy(button, new AddButtonPolicy(objectType, new Component[] { button }));
        //}

        //protected void SetAddButtonPolicy(Component button, AddButtonPolicy addButtonPolicy)
        //{
        //    if (!this.AddButtonPolicyList.ContainsKey(button))
        //    {
        //        this.AddButtonPolicyList.Add(button, addButtonPolicy);
        //    }
        //    else
        //    {
        //        this.AddButtonPolicyList[button] = addButtonPolicy;
        //    }
        //}

        //protected void RemoveAddButtonPolicy(Component button)
        //{
        //    this.AddButtonPolicyList.Remove(button);
        //}

        protected void RemoveAddButtonPolicyByButton(Component button)
        {
            var addButtonPoliciesToRemove = from addButtonPolicy in this.AddButtonPolicyList
                                            where addButtonPolicy.Buttons.Contains(button)
                                            select addButtonPolicy;

            foreach (AddButtonPolicy<GraphElement> addButtonPolicy in addButtonPoliciesToRemove)
            {
                this.AddButtonPolicyList.Remove(addButtonPolicy);
            }
        }

		protected void SetButtonsSortingEnableProperty()
		{
			bool enabled = (this.GraphControlGetSortedColumnCount() > 0);

			foreach (Component button in this.buttonRemoveColumnSortingList)
				this.SetButtonEnablePropertyInternal(button, enabled);

			this.SetButtonsMoveUpEnableProperty(this.FocusedGraphElement);
			this.SetButtonsMoveDownEnableProperty(this.FocusedGraphElement);
		}

		#endregion |   Protected Methods   |

		#region |   Private - Protected Virtual Event Recieve Methods   |

		private void objectManager_GraphElementCreated(object sender, GraphElementRequesterEventArgs e)
        {
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            Form? form = this.FindGraphControlForm();

            if (form != null && form.InvokeRequired)
                form.Invoke(new MethodInvoker(() => this.OnGraphElementCreated(e)));
            else
                this.OnGraphElementCreated(e);
        }

        protected virtual void OnGraphElementCreated(GraphElementRequesterEventArgs e)
        {
			if (this.IsActive && this.FocusedSimpleObject == e.GraphElement.SimpleObject)
				this.SetButtonsEnableProperty(this.FocusedGraphElement);

			if (this.BindOnlyToSpecifiedGraphKey && e.GraphElement.GraphKey != this.GraphKey)
                return;

            if (e.Requester == this || (e.GraphElement.Parent == null && e.GraphElement.GraphKey == this.GraphKey && this.anchorGraphElement == null) ||
                (e.GraphElement.Parent != null && (this.nodesByGraphElement.ContainsKey(e.GraphElement.Parent) || e.GraphElement.Parent == this.anchorGraphElement)))
            {
                this.BeginGraphUpdate();

                object node = this.AddNodeInternal(e.GraphElement, e.GraphElement.Parent);
                
                //this.UpdateNodeInternal(e.GraphElement, node);
                this.RaiseGraphElementCreated(e);

                this.EndGraphUpdate();
            }
		}

        private void ObjectManager_DeleteRequested(object sender, SimpleObjectChangeContainerRequesterEventArgs e)
        {
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            Form? form = this.FindGraphControlForm();

            if (form != null && form.InvokeRequired)
                form.Invoke(new MethodInvoker(() => this.OnObjectManager_DeleteRequested(e)));
            else
                this.OnObjectManager_DeleteRequested(e);
        }

        private void OnObjectManager_DeleteRequested(SimpleObjectChangeContainerRequesterEventArgs e)
        {
            if (e.SimpleObject is GraphElement)
            {
                GraphElement graphElement = e.SimpleObject as GraphElement;

                if (this.Contains(graphElement))
                    this.RaiseGraphElementDeleteRequested(graphElement, e.ChangeContainer, e.Requester);
            }
        }

        private void objectManager_BeforeDeleting(object sender, SimpleObjectRequesterEventArgs e)
        {
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            Form? form = this.FindGraphControlForm();

            if (form != null && form.InvokeRequired)
                form.Invoke(new MethodInvoker(() => this.OnBeforeDeleting(e)));
            else
                this.OnBeforeDeleting(e);
        }

        protected virtual void OnBeforeDeleting(SimpleObjectRequesterEventArgs e)
        {
            //if (e.Requester != this && e.SimpleObject is GraphElement)
        }

		protected virtual void OnBeforeGraphElementDelete(GraphElement graphElement, object? requester)
		{
		}

        private void objectManager_AfterDelete(object sender, SimpleObjectRequesterEventArgs e)
        {
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            Form? form = this.FindGraphControlForm();

            if (form != null && form.InvokeRequired)
                form.Invoke(new MethodInvoker(() => this.OnAfterDelete(e)));
            else
                this.OnAfterDelete(e);
        }

        protected virtual void OnAfterDelete(SimpleObjectRequesterEventArgs e)
        {
			if (this.graphUpdate > 0)
				return;

            if (e.SimpleObject is GraphElement)
            {
                GraphElement? graphElement = e.SimpleObject as GraphElement;
                object? node = null;
                bool nodeExists = (graphElement != null) && this.nodesByGraphElement.TryGetValue(graphElement, out node);

                if (nodeExists && node != null)
                {
                    this.RemoveNodeInternal(graphElement!, node, e.Requester);

                    if (e.Requester != this && graphElement!.GraphKey == this.GraphKey)
                    {
                        this.OnAfterGraphElementDelete(graphElement!, e.Requester);

                        if (this.FocusedGraphElement != null && this.FocusedGraphElement.SimpleObject != null)
                        {
                            // TODO: Revision needed. Is this necesery 
                            // Refresh buttons while BeforeDeleting still have GraphElement references in collections that compute e.g. CanDeleteGraphElement
                            this.SetButtonsEnableProperty(this.FocusedGraphElement);
                        }
                    }
                }
            }
        }

		protected virtual void OnAfterGraphElementDelete(GraphElement graphElement, object? requester)
		{
		}

        private void objectManager_PropertyValueChange(object sender, ChangePropertyValueSimpleObjectRequesterEventArgs e)
        {
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            Form? form = this.FindGraphControlForm();

            if (form != null && form.InvokeRequired)
                form.Invoke(new MethodInvoker(() => this.OnPropertyValueChange(e)));
            else
                this.OnPropertyValueChange(e);
        }

		protected virtual void OnPropertyValueChange(ChangePropertyValueSimpleObjectRequesterEventArgs e)
		{
            if (!(e.SimpleObject is GraphElement))
			{
                if (e.Requester != this)
                {
                    SimpleObject simpleObject = e.SimpleObject as SimpleObject;
                    var graphElements = simpleObject.GraphElements;

                    if (graphElements.Count > 0 && e.PropertyModel != null && !e.PropertyModel.IsRelationTableId && !e.PropertyModel.IsRelationObjectId)
                    {
                        //object focusedNode = this.GraphControlGetFocusedNode();
                        //GraphElement focusedGraphElement = focusedNode != null ? this.GraphControlGetGraphElementByNode(focusedNode) : null;
                        //SimpleObject focusedSimpleObject = focusedGraphElement != null ? focusedGraphElement.SimpleObject : null;
                        //int focusedColumnIndex = this.GraphControlGetFocusedColumnIndex();
                        //bool isSimpleObjectFocused = e.SimpleObject.Equals(focusedSimpleObject);

                        foreach (GraphElement graphElement in graphElements)
                        {
                            object? node = this.GetNode(graphElement);

                            if (node != null) //this.nodesByGraphElement.ContainsKey(graphElement))
                            {
                                //ISimpleObjectModel objectModel = e.SimpleObject.GetModel();
                                //List<GraphColumn> processedColumns = new List<GraphColumn>();

                                //if (this.RefreshGraphNodeOnEverySimpleObjectPropertyValueChange)
                                //{
                                //	foreach (IPropertyModel propertyModel in objectModel.PropertyModels)
                                //	{
                                //		GraphColumnBindingPolicy columnBindingPolicy = this.GetGraphColumnBindingPolicyByObjectPropertyIndex(objectModel.TableInfo.TableId, propertyModel.Index);

                                //		if (columnBindingPolicy != null)
                                //		{
                                //			//if (!processedColumns.Contains(columnBindingPolicy.GraphColumn))
                                //			//	processedColumns.Add(columnBindingPolicy.GraphColumn);

                                //			if (propertyModel.Index == e.PropertyIndex && e.Requester == this)
                                //				continue;

                                //			if (columnBindingPolicy.GraphColumn.Index != focusedColumnIndex)
                                //				this.UpdateNodeColumnValueInternal(graphElement, node, objectModel.TableInfo.TableId, columnBindingPolicy.GraphColumn);
                                //		}
                                //	}

                                //	//// Update rest of the columns not processed by the object properties
                                //	//foreach (GraphColumn graphColumn in this.Columns)
                                //	//	if (!processedColumns.Contains(graphColumn) && graphColumn.Index != focusedColumnIndex)
                                //	//		this.UpdateNodeColumnValueInternal(graphElement, node, objectModel.TableInfo.TableId, graphColumn);
                                //}
                                //else if (e.Requester != this) // && !e.SimpleObject.IsNew)
                                //{
                                //if (e.Requester != this) // && !e.SimpleObject.IsNew)
                                    this.UpdateNodeColumnValueInternal(graphElement, node, e.PropertyModel.PropertyIndex);
                                //}


                                this.RaiseSimpleObjectPropertyValueChange(graphElement, node, e.SimpleObject, e.PropertyModel, e.Value, e.OldValue, e.IsChanged, e.IsSaveable, e.Requester);

                                //if (isSimpleObjectFocused)
                                //{
                                //this.RaiseBindingObjectRefreshContext(new ChangePropertyValueBindingObjectRequesterEventArgs(e));
                                
                                if (this.bindingObject == e.SimpleObject)
                                    this.RaiseBindingObjectPropertyValueChange(new ChangePropertyValueBindingObjectRequesterEventArgs(e));
                                //}

                                if (graphElement == this.FocusedGraphElement)
                                    this.SetButtonsSaveEnableProperty(graphElement);
                            }
                        }
                    }
                }
                else if (this.bindingObject == e.SimpleObject)
                {
                    this.RaiseBindingObjectPropertyValueChange(new ChangePropertyValueBindingObjectRequesterEventArgs(e));
                }
            }
        }

        private void objectManager_GraphElementParentChange(object sender, OldParentGraphElementRequesterEventArgs e)
        {
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            Form? form = this.FindGraphControlForm();

            if (form != null && form.InvokeRequired)
                form.Invoke(new MethodInvoker(() => this.OnGraphElementParentChange(e)));
            else
                this.OnGraphElementParentChange(e);
        }

        protected virtual void OnGraphElementParentChange(OldParentGraphElementRequesterEventArgs e)
        {
            if (e.Requester != this)
            {
                object? sourceNode = this.GetNode(e.GraphElement);

                if (sourceNode == null)
                    return;

                object? destinationNode = (e.GraphElement.Parent != null) ? this.GetNode(e.GraphElement.Parent) : null;

                //if (destinationNode == null)
                //    return;

                this.GraphControlMoveNode(sourceNode, destinationNode);
                
                if (destinationNode != null)
                    this.GraphControlExpandNode(destinationNode);
            }
            
            if (e.GraphElement.GraphKey == this.GraphKey)
                this.RaiseBindingObjectParentGraphElementChange(e);

			this.SetButtonsMoveUpEnableProperty(this.FocusedGraphElement);
			this.SetButtonsMoveDownEnableProperty(this.FocusedGraphElement);
		}

        //private void manager_ChangedPropertyNamesCountChange(object sender, CountChangeSimpleObjectEventArgs e)
        //{
        //    if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
        //        return;

        //    Form form = this.FindGraphControlForm();

        //    if (form != null && form.InvokeRequired)
        //    {
        //        form.Invoke(new MethodInvoker(() => OnChangedPropertyNamesCountChange(e)));
        //    }
        //    else
        //    {
        //        this.OnChangedPropertyNamesCountChange(e);
        //    }
        //}

        //protected virtual void OnChangedPropertyNamesCountChange(CountChangeSimpleObjectEventArgs e)
        //{
        //    if ((e.Count == 0 || e.OldCount == 0))
        //    {
        //        object node = this.GraphControlGetFocusedNode();

        //        if (node != null)
        //        {
        //            GraphElement focusedGraphElement = this.GraphControlGetGraphElementByNode(node);

        //            if (e.SimpleObject.Equals(focusedGraphElement.SimpleObject))
        //            {
        //                this.SetBattonsSaveEnableProperty(focusedGraphElement);
        //            }
        //        }
        //    }
        //}


		private void objectManager_OrderIndexChange(object sender, ChangeSortedSimpleObjectRequesterEventArgs e)
		{
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            Form? form = this.FindGraphControlForm();

            if (form != null && form.InvokeRequired)
				form.Invoke(new MethodInvoker(() => this.OnOrderIndexChange(e)));
            else
				this.OnOrderIndexChange(e);
		}

		protected virtual void OnOrderIndexChange(ChangeSortedSimpleObjectRequesterEventArgs e)
		{
			if (e.SortableSimpleObject is GraphElement)
			{
				GraphElement? graphElement = e.SortableSimpleObject as GraphElement;
				object? node = this.GetNode(graphElement);

				if (node != null) //this.nodesByGraphElement.ContainsKey(graphElement))
				{
					if (e.Requester != this)
                        this.GraphControlSetNodeOrderIndex(node, e.OrderIndex);

					if (this.FocusedGraphElement != null)
					{
						this.SetButtonsMoveUpEnableProperty(this.FocusedGraphElement);
						this.SetButtonsMoveDownEnableProperty(this.FocusedGraphElement);
					}
				}
			}
		}

        private void objectManager_RequireSavingChange(object sender, RequireSavingChangeSimpleObjectEventArgs e)
        {
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            Form? form = this.FindGraphControlForm();

            if (form != null && form.InvokeRequired)
                form.Invoke(new MethodInvoker(() => this.OnRequireSavingChange(e)));
            else
                this.OnRequireSavingChange(e);
        }

        protected virtual void OnRequireSavingChange(RequireSavingChangeSimpleObjectEventArgs e)
        {
            if (e.SimpleObject is GraphElement graphElement && graphElement.GraphKey != this.GraphKey)
                return;

            if (e.SimpleObject != null && this.SaveButonOption == SaveButtonOption.SaveFocusedNode && this.FocusedGraphElement != null && 
                ((this.FocusedGraphElement.SimpleObject != null && e.SimpleObject.Equals(this.FocusedGraphElement.SimpleObject)) || e.SimpleObject.Equals(this.FocusedGraphElement)))
            {
                this.SetButtonsSaveEnableProperty(e.RequireSaving);
                this.SetButtonsRejectChangesEnableProperty(this.FocusedGraphElement);
            }
        }

		private void ObjectManager_RequireCommitChange(object sender, RequireCommitChangeEventArgs e)
		{
			if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
				form.Invoke(new MethodInvoker(() => this.OnRequireCommitChange(e)));
			else
				this.OnRequireCommitChange(e);
		}

		protected virtual void OnRequireCommitChange(RequireCommitChangeEventArgs e)
		{
            if (this.SaveButonOption == SaveButtonOption.CommitChanges)
                this.SetButtonsSaveEnableProperty(e.RequireCommit);

            if (!e.RequireCommit)
            {
                this.GraphControlResetColumnErrors();
                this.isGraphErrorMessageSet = false;
            }

            if (!e.RequireCommit)
            {
                this.SetButtonsRejectChangesEnableProperty(false);
            }
            else
            {
                this.SetButtonsRejectChangesEnableProperty(this.FocusedGraphElement);
            }
        }

        private void ObjectManager_ActiveEditorsPushObjectData(object sender, SimpleObjectEventArgs e)
		{
			if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
				form.Invoke(new MethodInvoker(() => this.OnActiveEditorsPushObjectData(e)));
			else
				this.OnActiveEditorsPushObjectData(e);
		}

		protected virtual void OnActiveEditorsPushObjectData(SimpleObjectEventArgs e)
		{
			this.RaiseBindingObjectPushData(e.SimpleObject);
		}

		private void objectManager_RelationForeignObjectSet(object sender, RelationForeignObjectSetRequesterEventArgs e)
        {
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            Form? form = this.FindGraphControlForm();

            if (form != null && form.InvokeRequired)
                form.Invoke(new MethodInvoker(() => this.OnRelationForeignObjectSet(e)));
            else
                this.OnRelationForeignObjectSet(e);
        }

        protected virtual void OnRelationForeignObjectSet(RelationForeignObjectSetRequesterEventArgs e)
        {
			if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
				return;

			if (e.Requester != this && e.Requester != this.EditorBindings)
			{
				this.RefreshNodeBySimpleObject(e.SimpleObject);
				this.RefreshNodeBySimpleObject(e.ForeignSimpleObject);

				if (e.OldForeignSimpleObject != null && !e.OldForeignSimpleObject.DeleteStarted)
					this.RefreshNodeBySimpleObject(e.OldForeignSimpleObject);
			}

            if (this.bindingObject == e.SimpleObject)
            {
                this.RaiseBindingObjectRelationForeignObjectSet(new BindingObjectRelationForeignObjectSetRequesterEventArgs(e));
                this.RaiseBindingObjectRefreshContext(e.SimpleObject, e.RelationModel.PrimaryObjectIdPropertyModel, null, null, false, false, e.Requester);
            }
        }

        private void objectManager_ImageNameChange(object sender, ImageNameChangeSimpleObjectEventArgs e)
        {
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            Form? form = this.FindGraphControlForm();

            if (form != null && form.InvokeRequired)
                form.Invoke(new MethodInvoker(() => this.OnImageNameChange(e)));
            else
                this.OnImageNameChange(e);
        }

        protected virtual void OnImageNameChange(ImageNameChangeSimpleObjectEventArgs e)
        {
            //if (e.SimpleObject is GraphElement)
            //{
            //    GraphElement graphElement = e.SimpleObject as GraphElement;
            //    object node = this.nodesByGraphElement[graphElement];

            //    if (node != null)
            //    {
            //        GraphNodeTag nodeTag = this.GetGraphNodeTag(graphElement);
            //        this.GraphControlSetNodeImageIndex(node, nodeTag.ImageIndex++);
            //    }
            //}

            //return;

            //SimpleObject? simpleObject = e.SimpleObject as SimpleObject;
			GraphElement? graphElement = null;
			object? node = null;

			if (e.SimpleObject is GraphElement)
            {
				graphElement = e.SimpleObject as GraphElement;
				node = this.GetNode(graphElement);
            }
			else
			{
				foreach (var item in e.SimpleObject.GraphElements)
				{
					node = this.GetNode(item);

					if (node != null)
					{
						graphElement = item;
						break;
					}
				}
			}

			if (node != null)
			{
				string imageName = e.ImageName;
				GraphElementNodeTag nodeTag = this.GetGraphNodeTag(graphElement);

				nodeTag.ImageIndex = this.GetImageIndex(e.ImageName, nodeTag);
				this.GraphControlSetNodeImageIndex(node, nodeTag.ImageIndex);
			}

			//GraphElement focusedGraphElement = this.fo GraphControlGetGraphElementByNode(node);

			//if (e.SimpleObject.Equals(focusedGraphElement.SimpleObject))
			//{
			//    this.RaiseBindingObjectImageNameChange(e);
			//}


			//foreach (GraphElement graphElement in simpleObject.GraphElements)
			//{
			//    //if (graphElement.Graph == this.Graph)
			//    if (this.nodesByGraphElement.ContainsKey(graphElement))
			//    {
			//        object node = this.nodesByGraphElement[graphElement];

			//        GraphNodeTag nodeTag = this.GraphControlGetGraphNodeTag(node);
			//        nodeTag.ImageIndex = this.ImageList.Images.IndexOfKey(e.ImageName);

			//        this.GraphControlBeginUpdate();
			//        this.GraphControlSetNodeImageIndex(node, nodeTag.ImageIndex);
			//        this.GraphControlEndUpdate();

			//        GraphElement focusedGraphElement = this.GraphControlGetGraphElementByNode(node);

			//        if (e.SimpleObject.Equals(focusedGraphElement.SimpleObject))
			//        {
			//            this.RaiseBindingObjectImageNameChange(e);
			//        }
			//    }
			//}
		}

        //private void UpdateNodeImage(object node, string imageName)
        //{
        //    GraphNodeTag nodeTag = this.GraphControlGetGraphNodeTag(node);
        //    nodeTag.ImageIndex = this.ImageList.Images.IndexOfKey(imageName);

        //    //this.GraphControlBeginUpdate();
        //    this.GraphControlSetNodeImageIndex(node, nodeTag.ImageIndex);
        //    //this.GraphControlEndUpdate();
        //}

		private void objectManager_MultipleImageNameChange(object sender, List<ImageNameChangeSimpleObjectEventArgs> e)
		{
			if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
				form.Invoke(new MethodInvoker(() => this.OnMultipleImageNameChange(e)));
			else
				this.OnMultipleImageNameChange(e);
		}

		protected virtual void OnMultipleImageNameChange(List<ImageNameChangeSimpleObjectEventArgs> e)
		{
			Dictionary<object, int> imageIndexesByNode = new Dictionary<object, int>();

            try
            {
                foreach (ImageNameChangeSimpleObjectEventArgs imageNameChangeSimpleObjectEventArgs in e)
                {
                    if (imageNameChangeSimpleObjectEventArgs.SimpleObject is GraphElement)
                    {
                        GraphElement? graphElement = imageNameChangeSimpleObjectEventArgs.SimpleObject as GraphElement;
                        object? node = this.GetNode(graphElement);

                        if (node != null)
                        {
                            GraphElementNodeTag? nodeTag = this.GetGraphNodeTag(graphElement);

                            if (nodeTag != null && imageNameChangeSimpleObjectEventArgs.ImageName != null)
                            {
                                nodeTag.ImageIndex = this.GetImageIndex(imageNameChangeSimpleObjectEventArgs.ImageName, nodeTag);

                                if (imageIndexesByNode.ContainsKey(node))
                                    imageIndexesByNode[node] = nodeTag.ImageIndex;
                                else
                                    imageIndexesByNode.Add(node, nodeTag.ImageIndex);
                            }
                        }
                    }
                }
            }
            catch
            {
            }

			if (imageIndexesByNode.Count > 0)
			{
				this.BeginGraphUpdate();

				foreach (var item in imageIndexesByNode)
					this.GraphControlSetNodeImageIndex(item.Key, item.Value); // Key is node object, value is image index

				this.EndGraphUpdate();
			}
		}

		private int GetImageIndex(string imageName, GraphElementNodeTag nodeTag)
		{
			if (nodeTag.Expanded)
			{
				string expandedImageName = ImageControl.ImageNameAddOption(imageName, ImageControl.ImageOptionExpanded, insertionPosition: 1);

				if (this.ImageList != null && this.ImageList.Images.ContainsKey(expandedImageName))
					imageName = expandedImageName;
			}

			return this.ImageList?.Images.IndexOfKey(imageName) ?? 0;
		}

		private void objectManager_UpdateStarted(object? sender, EventArgs e)
        {
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            Form? form = this.FindGraphControlForm();

            if (form != null && form.InvokeRequired)
                form.Invoke(new MethodInvoker(() => this.OnUpdateStarted()));
            else
                this.OnUpdateStarted();
        }

        private void OnUpdateStarted()
        {
            this.BeginGraphUpdate();
        }

        private void objectManager_UpdateEnded(object? sender, EventArgs e)
        {
            if (this.isLoadingInProgress || !this.isBinded || this.isDisposed)
                return;

            Form? form = this.FindGraphControlForm();

            if (form != null && form.InvokeRequired)
                form.Invoke(new MethodInvoker(() => this.OnUpdateEnded()));
            else
                this.OnUpdateEnded();
        }

        private void OnUpdateEnded()
        {
            this.EndGraphUpdate();
        }

        private void changableBindingObjectControl_BindingObjectChange(object sender, SimpleObjectEventArgs e)
        {
            Form? form = this.FindGraphControlForm();

            if (form != null && form.InvokeRequired)
                form.Invoke(new MethodInvoker(() => this.OnBindingObjectChange(e)));
            else
                this.OnBindingObjectChange(e);
        }

        protected virtual void OnBindingObjectChange(SimpleObjectEventArgs e)
        {
            if (this.ChangableBindingObjectControlFocusedSimpleObject != e.SimpleObject)
                this.RefreshContext(this.ChangableBindingObjectControlFocusedSimpleObject, e.SimpleObject);

            this.changableBindingObjectControlFocusedSimpleObject = e.SimpleObject;
        }

        private void changableBindingObjectControl_BindingObjectRelationForeignObjectSet(object sender, BindingObjectRelationForeignObjectSetRequesterEventArgs e)
        {
            Form? form = this.FindGraphControlForm();

            if (form != null && form.InvokeRequired)
                form.Invoke(new MethodInvoker(() => this.OnBindingObjectRelationForeignObjectSet(e)));
            else
                this.OnBindingObjectRelationForeignObjectSet(e);
        }

        protected virtual void OnBindingObjectRelationForeignObjectSet(BindingObjectRelationForeignObjectSetRequesterEventArgs e)
        {
        }

        private void changableBindingObjectControl_BindingObjectRefreshContext(object sender, ChangePropertyValueBindingObjectRequesterEventArgs e)
        {
            Form? form = this.FindGraphControlForm();

            if (form != null && form.InvokeRequired)
                form.Invoke(new MethodInvoker(() => this.OnBindingObjectRefreshContext(e)));
            else
                this.OnBindingObjectRefreshContext(e);
        }

        protected virtual void OnBindingObjectRefreshContext(ChangePropertyValueBindingObjectRequesterEventArgs e)
        {
            this.RefreshContext(this.ChangableBindingObjectControlFocusedSimpleObject, e.BindingObject);
        }

		private void ObjectManager_ValidationInfo(object sender, ValidationInfoEventArgs e)
		{
			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
				form.Invoke(new MethodInvoker(() => this.OnValidationInfo(e)));
			else
				this.OnValidationInfo(e);
		}

		private void OnValidationInfo(ValidationInfoEventArgs e)
		{
			if (this.isLoadingInProgress || !this.isBinded || this.isDisposed || e.ValidationResult.Target != this.FocusedSimpleObject)
				return;

			GraphValidationResult graphValidationResult = this.ValidateNode(e.ValidationResult);

			this.SetValidation(graphValidationResult);
			this.RaiseBindingObjectOnValidation(graphValidationResult);
		}


        #endregion |   Private - Protected Virtual Event Recieve Methods   |

        #region |   Private Raise Event Methods   |

		private void RaiseBindingObjectChange(SimpleObject? bindingObject)
        {
            this.BindingObjectChange?.Invoke(this, new SimpleObjectEventArgs(bindingObject));
        }

        private void RaiseBindingObjectPropertyValueChange(ChangePropertyValueBindingObjectRequesterEventArgs changePropertyValueBindingObjectRequesterEventArgs)
        {
            this.BindingObjectPropertyValueChange?.Invoke(this, changePropertyValueBindingObjectRequesterEventArgs);
        }

        private void RaiseBindingObjectRelationForeignObjectSet(BindingObjectRelationForeignObjectSetRequesterEventArgs bindingObjectRelationForeignObjectSetEventArgs)
        {
            this.BindingObjectRelationForeignObjectSet?.Invoke(this, bindingObjectRelationForeignObjectSetEventArgs);
        }

        //private void RaiseBindingObjectImageNameChange(ImageNameChangeSimpleObjectEventArgs iconNameChangeSimpleObjectEventArgs)
        //{
        //    if (this.BindingObjectIconNameChange != null)
        //    {
        //        this.BindingObjectIconNameChange(this, new ImageNameChangeBindingObjectEventArgs(iconNameChangeSimpleObjectEventArgs));
        //    }
        //}

        private void RaiseBindingObjectOnValidation(GraphValidationResult graphValidationResult)
        {
            this.BindingObjectOnValidation?.Invoke(this, new GraphValidationResultEventArgs(graphValidationResult));
        }

        private void RaiseBindingObjectPushData(SimpleObject bindingObject)
        {
			this.BindingObjectPushData?.Invoke(this, new SimpleObjectEventArgs(bindingObject));
        }

        private void RaiseBindingObjectRefreshContext(SimpleObject bindingObject, IPropertyModel? propertyModel, object? value, object? oldValue, bool isChanged, bool isSeaveable, object requester)
        {
            this.BindingObjectRefreshContext?.Invoke(this, new ChangePropertyValueBindingObjectRequesterEventArgs(bindingObject, propertyModel, value, oldValue, isChanged, isSeaveable, requester));
        }

        private void RaiseGraphElementCreated(GraphElementRequesterEventArgs graphElementRequesterEventArgs)
        {
            this.GraphElementCreated?.Invoke(this, graphElementRequesterEventArgs);
        }

        private void RaiseGraphElementDeleteRequested(GraphElement graphElement, ChangeContainer changeContainer, object? requester)
        {
            this.GraphElementDeleteRequested?.Invoke(this, new GraphElementChangeContainerRequesterEventArgs(graphElement, changeContainer, requester));
        }

        //private void RaiseBeforeGraphElementDeleted(GraphElementRequesterEventArgs graphElementRequesterEventArgs)
        //{
        //    this.BeforeGraphElementDeleted?.Invoke(this, graphElementRequesterEventArgs);
        //}

        private void RaiseBeforeDeleteGraphElement(GraphElement graphElement, object requester)
        {
            this.BeforeGraphElementDeleted?.Invoke(this, new GraphElementRequesterEventArgs(graphElement, requester));
        }

        private void RaiseSimpleObjectPropertyValueChange(GraphElement graphElement, object node, SimpleObject simpleObject, IPropertyModel? propertyModel, object? value, object? oldValue, bool isChanged, bool isSaveable, object? requester)
        {
            this.SimpleObjectPropertyValueChange?.Invoke(this, new GraphElementChangePropertyValueSimpleObjectRequesterEventArgs(graphElement, node, simpleObject, propertyModel, value, oldValue, isChanged, isSaveable, requester));
        }

        private void RaiseBindingObjectParentGraphElementChange(OldParentGraphElementRequesterEventArgs oldParentGraphElementRequesterEventArgs)
        {
            this.BindingObjectParentGraphElementChange?.Invoke(this, oldParentGraphElementRequesterEventArgs);
        }

        private void RaiseAfterSetGraphColumnsEnableProperty(GraphElement graphElement)
        {
            this.AfterSetColumnsEnableProperty?.Invoke(this, new GraphElementEventArgs(graphElement));
        }

        private void RaiseOnSetButtonEnableProperty(Component button)
        {
            this.OnSetButtonEnableProperty?.Invoke(this, new ButtonEventArgs(button));
        }

		private void RaiseAfterSetButtonsEnableProperty()
		{
			this.AfterSetButtonsEnableProperty?.Invoke(this, new EventArgs());
		}

		private object? GetEditValueByEvent(GraphElement graphElement, object node, GraphColumn graphCpolumn, object? value)
        {
            if (this.GetCustomCellEditValue != null)
            {
                EditValueGraphElementEventArgs args = new EditValueGraphElementEventArgs(graphElement, node, graphCpolumn, value);
                
                this.GetCustomCellEditValue(this, args);

                return args.EditValue;
            }
            else
            {
                return value;
            }
        }

        private bool ReloadGraphElementNodeByEvent(GraphElement graphElement, object node)
        {
            if (this.ReloadGraphElementNode != null)
            {
                ReloadGraphElementNodeEventArgs args = new ReloadGraphElementNodeEventArgs(graphElement, node, false);
                
                this.ReloadGraphElementNode(this, args);

                return args.IsReloaded;
            }
            else
            {
                return false;
            }
        }

        private void RaiseAfterGraphElementNodeReload(GraphElement graphElement, object node)
        {
            this.AfterGraphElementNodeReload?.Invoke(this, new GraphElementNodeEventArgs(graphElement, node));
        }

        private void RaiseAfterBestFitGraphColumns()
        {
            this.AfterBestFitGraphColumns?.Invoke(this, new EventArgs());
        }

        //private void RaiseCreateAnchorGraphElement()
        //{
        //    this.CreateAnchorGraphElement?.Invoke(this, EventArgs.Empty);
        //}

        //private AnchorGraphElementEventArgs RaiseGetAnchorGraphElement()
        //{
        //    AnchorGraphElementEventArgs args = new AnchorGraphElementEventArgs();

        //    this.GetAnchorGraphElement?.Invoke(this, args);

        //    return args;
        //}

        private CheckGraphElementEventArgs? RaiseBeforeCheckGraphElementEvent(GraphElement graphElement, bool checkValue, bool canCheck)
        {
            if (this.BeforeCheckGraphElement != null)
            {
                CheckGraphElementEventArgs checkGraphElementArgs = new CheckGraphElementEventArgs(graphElement, checkValue, canCheck);
                
                this.BeforeCheckGraphElement(this, checkGraphElementArgs);

                return checkGraphElementArgs;
            }
            else
            {
                return null;
            }
        }

        private CompareGraphColumnNodeValuesEventArgs? GetColumnNodeValuesCompareByEvent(GraphColumn graphColumn, object node1, object node2, object nodeValue1, object nodeValue2, SortOrder sortOrder, int result)
        {
            if (this.CompareColumnNodeValues != null)
            {
                CompareGraphColumnNodeValuesEventArgs compareColumnNodeValuesEventArgs = new CompareGraphColumnNodeValuesEventArgs(graphColumn, node1, node2, nodeValue1, nodeValue2, sortOrder, result);
                
                this.CompareColumnNodeValues(this, compareColumnNodeValuesEventArgs);

                return compareColumnNodeValuesEventArgs;
            }
            else
            {
                return null;
            }
        }

		private AskBeforeDeleteGraphElementEventArgs? RaiseAskBeforeDeleteEvent(GraphElement graphElement, InputUserSource inputSource, bool askBeforeDelete)
		{
			if (this.AskBeforeDelete != null)
			{
				AskBeforeDeleteGraphElementEventArgs args = new AskBeforeDeleteGraphElementEventArgs(graphElement, inputSource, askBeforeDelete);
				
                this.AskBeforeDelete(this, args);

				return args;
			}
			else
			{
				return default;
			}
		}

		private AllowAddButtonPolicytEventArgs? RaiseButtonAddClicked(Component component, AddButtonPolicy<GraphElement> addButtonPolicy, GraphElement parentGraphElement, bool allow)
		{
			if (this.ButtonAddClicked != null)
			{
				AllowAddButtonPolicytEventArgs args = new AllowAddButtonPolicytEventArgs(component, addButtonPolicy, parentGraphElement, allow);
				
                this.ButtonAddClicked(this, args);

				return args;
			}
			else
			{
				return null;
			}
		}

        #endregion |   Private Raise Event Methods   |

        #region |   Private Methods   |

        private void Load(GraphElement graphElement, GraphElement? parentGraphElement) //, bool hasChildren)
        {
            if (graphElement.DeleteStarted)
                return;
            
            object node = this.AddNodeInternal(graphElement, parentGraphElement);
            GraphElementNodeTag graphNodeTag = this.GraphControlGetGraphNodeTag(node);

            //this.UpdateNodeInternal(graphElement, node);
			//this.RaiseGraphElementCreated(new GraphElementRequesterEventArgs(this, graphElement));

            if (this.LoadAllNodes)
            {
                //var childGraphElements = this.ObjectManager!.GetGraphElements(this.GraphKey, graphElement.Id);
                var childGraphElements = graphElement.GraphElements;

				foreach (GraphElement item in childGraphElements)
                    this.Load(item, graphElement);

                graphNodeTag.ChildrenNodesLoaded = true;
            }
            else
            {
				bool nodeHasChildren = graphElement.HasChildren;

				this.GraphControlSetNodeHasChildrenProperty(node, nodeHasChildren);
                graphNodeTag.ChildrenNodesLoaded = !nodeHasChildren;
            }
        }

		private AddNewObjectResult AddNewObject(AddButtonPolicy<GraphElement> addButtonPolicy, GraphElement parentGraphElement)
        {
			return this.AddNewObject(addButtonPolicy, parentGraphElement, this.Columns[0]!);
        }

		private AddNewObjectResult AddNewObject(AddButtonPolicy<GraphElement> addButtonPolicy, GraphElement parentGraphElement, GraphColumn focusedColumn)
        {
            if (this.ObjectManager is null)
                return new AddNewObjectResult(null, success: false);

            bool canAdd = this.ObjectManager.CanAddGraphElement(this.GraphKey, addButtonPolicy.ObjectType, parentGraphElement);
			GraphElement? newGraphElement = null;

            if (canAdd)
            {
                SimpleObject newSimpleObject = this.ObjectManager.CreateNewEmptyObject(addButtonPolicy.ObjectType, isNew: true, objectId: 0, requester: null); //Activator.CreateInstance(addButtonPolicy.ObjectType, this.ObjectManager) as SimpleObject;
                newGraphElement = new GraphElement(this.ObjectManager, this.GraphKey, newSimpleObject, parentGraphElement, requester: this);
                // When created new graph element ObjectManager fires event and create new node and add it to the graphElementNodeHashtable.

                if (addButtonPolicy.ObjectAction != null)
					addButtonPolicy.ObjectAction(newGraphElement);

                // Now, we gets the node from local hashtable graphElementNodeHashtable
                object? newNode = this.GetNode(newGraphElement);

				if (newNode == null)
				{
                    this.BeginGraphUpdate();

					newNode = this.AddNodeInternal(newGraphElement, parentGraphElement);

					//this.UpdateNodeInternal(newGraphElement, newNode);
					this.RaiseGraphElementCreated(new GraphElementRequesterEventArgs(newGraphElement, requester: this));

					this.EndGraphUpdate();
				}

				GraphElementNodeTag newNodeTag = this.GraphControlGetGraphNodeTag(newNode);
				
                newNodeTag.CanBeFocusedWithoutSavingOldNode = true;

				this.SetFocusedNode(newNode);
                this.GraphControlSetFocusedColumn(focusedColumn.Index);
                this.GraphControlShowEditor();
            }

            return new AddNewObjectResult(newGraphElement, canAdd);
        }

		private GraphValidationResult ValidateNode(GraphElement graphElement)
		{
			SimpleObjectValidationResult validationResult = graphElement.SimpleObject.ValidateSave();

			return this.ValidateNode(validationResult);
		}

		private GraphValidationResult ValidateNode(SimpleObjectValidationResult validationResult)
        {
            GraphColumn errorColumn = null;
			int errorColumnIndex = -1;

            if (!validationResult.Passed)
            {
				if (validationResult.FailedRuleResult != null && validationResult.FailedRuleResult is PropertyValidationResult propertyValidationResult)
				{
                    IPropertyModel errorPropertyModel = propertyValidationResult.ErrorPropertyModel;

                    if (errorPropertyModel != null)
                    {
                        GraphColumnBindingPolicy columnBindingPolicy = this.GetGraphColumnBindingPolicyByObjectPropertyIndex(validationResult.Target.GetModel().TableInfo.TableId, errorPropertyModel.PropertyIndex);

                        if (columnBindingPolicy != null)
                            errorColumn = this.Columns[columnBindingPolicy.GraphColumn.Index];
                    }
				}
            }
			//else
			//{
   //             //bool enforceParentGraphElementIsNotNew = this.anchorGraphElement == null;
   //             if (validationResult.Target.RequireSaving())
   //                 validationResult = validationResult.Target.ValidateSave(); // this.ObjectManager.CanSave(validationResult.Target);
			//}

			if (errorColumn != null)
				errorColumnIndex = errorColumn.Index;

			object node = this.GetNode(validationResult.Target);

			return new GraphValidationResult(validationResult, node, errorColumnIndex);
        }

		private bool SaveNode(object requester, GraphElement graphElement)
        {
            TransactionResult result = this.ObjectManager.CommitChanges();
            //bool isSaved = this.ObjectManager.Save(graphElement.SimpleObject, requester);

			//if (isSaved && graphElement.RequireSaving())
			//	isSaved &= this.ObjectManager.Save(requester, graphElement);

			if (result.TransactionSucceeded)
			{
				this.SetButtonsSaveEnableProperty(false);
				this.SetButtonsRejectChangesEnableProperty(false);
			}

            return result.TransactionSucceeded;
        }

        //private void DeleteNode(object node)
        //{
        //    GraphElement graphElement = this.GetGraphElementByNode(node);
        //    graphElement.Delete();
        //}

		private void SetFocusedNode(object node)
		{
			// Reset find column index and position
			this.findStartFromBegining = false;
			this.findColumnIndex = 0;
			this.findColumnPosition = 0;
			this.GraphControlSetFocusedNode(node);
		}

        private object AddNodeInternal(GraphElement graphElement, GraphElement? parentGraphElement)
        {
            object? parentNode = (parentGraphElement is not null) ? this.GetNode(parentGraphElement)
                                                                  : null;
            GraphElementNodeTag nodeTag = new GraphElementNodeTag(graphElement); // this.GraphControlCreateNodeTag(graphElement);
            object node = this.GraphControlAddNode(parentNode, nodeTag);
            
            this.nodesByGraphElement.Add(graphElement, node);
            this.UpdateNodeInternal(graphElement, node);

            if (this.ImageList != null)
            {
                try
                {
                    string? imageName = graphElement.GetImageName();
                    int imageIndex = (!imageName.IsNullOrEmpty()) ? this.ImageList.Images.IndexOfKey(imageName) : -1;
                    
                    nodeTag.ImageIndex = imageIndex;
                    this.GraphControlSetNodeImageIndex(node, imageIndex);

                    //string expandedImageName = imageName + ImageControl.ImageNameOptionSeparator + ImageControl.ImageNameOptionExpanded;
                    //int expandedImageIndex = this.ImageList.Images.IndexOfKey(expandedImageName);
                    //nodeTag.ExpandedImageIndex = expandedImageIndex;
                    //this.GraphControlSetNodeExpandedImageIndex(node, expandedImageIndex);

                    //        this.SetNodeExpandedImageIndex(node, expandedImageIndex);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, this.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            if (this.IsCheckBoxMode)
                this.GraphControlSetNodeCheckBoxImageIndex(node, this.CheckBoxUncheckedImageIndex);

            return node;
        }

		private void UpdateNodeInternal(GraphElement graphElement, object node)
		{
			//if (this.Graph.GraphKey != graphElement.GraphKey)
			//    return;

			//try
			//{
			if (graphElement.SimpleObject != null)
			{
				bool isReloaded = this.ReloadGraphElementNodeByEvent(graphElement, node);

				if (!isReloaded)
				{
					if (graphElement.SimpleObject == null)
					{
						XtraMessageBox.Show(String.Format("GraphElement SimpleObject is null (GraphElement Id={0}", graphElement.Id),
											this.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else
					{
						foreach (GraphColumn graphColumn in this.Columns)
							this.UpdateNodeColumnValueInternal(graphElement, node, graphColumn);
					}
				}

				this.RaiseAfterGraphElementNodeReload(graphElement, node);
			}
			//catch (Exception ex)
			//{
			//	XtraMessageBox.Show(ex.Message, AppContext.AppContext.Instance.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//}
		}


		private void RemoveNodeInternal(object node, object? requester)
        {
            GraphElement? graphElement = this.GetGraphElement(node);
            this.RemoveNodeInternal(graphElement, node, requester);
        }

        private void RemoveNodeInternal(GraphElement graphElement, object? requester)
        {
            object? node = this.GetNode(graphElement);
            
            if (node is not null)
                this.RemoveNodeInternal(graphElement, node, requester);
        }

        private void RemoveNodeInternal(GraphElement graphElement, object node, object? requester)
        {
            this.OnBeforeGraphElementDelete(graphElement, requester);
            this.RaiseBeforeDeleteGraphElement(graphElement, requester);
            this.GraphControlDeleteNode(node);
            this.nodesByGraphElement.Remove(graphElement);

            //if (this.NodeCount == 0 && this.AnchorGraphElement != null && this.AnchorGraphElement.GraphElements.Count == 1 && 
            //    this.AnchorGraphElement.GraphElements.Contains(graphElement) && this.DeleteAnchorGraphElementOnLastGraphElementDelete)
            //{
            //    this.AnchorGraphElement.RequestDelete();
            //    this.anchorGraphElement = null;
            //}
        }

        private bool CheckUpdateValidationRules(SimpleObject simpleObject)
        {
            if (this.ObjectManager == null)
                return true;
            
            //First check if graphElement.SimpleObject delete validation roles are satisfied
            ISimpleObjectModel simpleObjectModel = simpleObject.GetModel();
            var transactionRequests = this.ObjectManager.DefaultChangeContainer.TransactionRequests;

            foreach (ValidationRule validationRule in simpleObjectModel.UpdateValidationRules)
            {
                ValidationResult validationResult = validationRule.Validate(simpleObject, transactionRequests);

                if (!validationResult.Passed)
                {
                    XtraMessageBox.Show(validationResult.Message, "Update Validation Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return false;
                }
            }

            return true;
        }

        private bool CheckDeleteValidationRules(SimpleObject simpleObject)
        {
            if (this.ObjectManager == null || simpleObject == null)
                return true;
                
            //First check if graphElement.SimpleObject delete validation roles are satisfied
            ISimpleObjectModel simpleObjectModel = simpleObject.GetModel();
            var transactionRequests = this.ObjectManager.DefaultChangeContainer.TransactionRequests;

            foreach (ValidationRule validationRule in simpleObjectModel.DeleteValidationRules)
            {
                ValidationResult validationResult = validationRule.Validate(simpleObject, transactionRequests);

                if (!validationResult.Passed)
                {
                    XtraMessageBox.Show(validationResult.Message, "Delete Validation Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return false;
                }
            }

            return true;
        }


		private void UpdateNodeColumnValueInternal(GraphElement graphElement, object node, int simpleObjectPropertyIndex)
		{
			object? editValue = null;

            if (graphElement.SimpleObject != null)
            {
				GraphColumnBindingPolicy? columnBindingPolicy = this.GetGraphColumnBindingPolicyByObjectPropertyIndex(graphElement.SimpleObject.GetModel().TableInfo.TableId, simpleObjectPropertyIndex);

                if (columnBindingPolicy != null)
                {
                    editValue = this.GetEditValueByEvent(graphElement, node, columnBindingPolicy.GraphColumn, editValue);

                    if (editValue == null)
                        editValue = this.GetControlCellValueFromPropertyValue(graphElement, columnBindingPolicy);

                    this.GraphControlCloseEditor();
                    this.GraphControlSetNodeCellEditValue(node, columnBindingPolicy.GraphColumn.Index, editValue);
                }
            }
		}

		private void UpdateNodeColumnValueInternal(GraphElement graphElement, object node, GraphColumn graphColumn)
        {
			object? editValue = null;

			editValue = this.GetEditValueByEvent(graphElement, node, graphColumn, editValue);

            if (editValue == null)
            {
                SimpleObject? simpleObject = graphElement.SimpleObject;
				GraphColumnBindingPolicy? columnBindingPolicy = this.GetGraphColumnBindingPolicyByColumnIndex(graphElement.SimpleObject!.GetModel().TableInfo.TableId, graphColumn.Index);

                if (columnBindingPolicy != null && simpleObject != null)
                {
                    object? propertyValue = simpleObject.GetPropertyValue(columnBindingPolicy.PropertyModel.PropertyIndex);

                    if (!this.EditorBindings.TrySetPropertyValue(simpleObject.GetType(), columnBindingPolicy.PropertyModel, propertyValue))
                    {
                        editValue = this.GetControlCellValueFromPropertyValue(graphElement, columnBindingPolicy);

                        this.GraphControlSetNodeCellEditValue(node, graphColumn.Index, editValue);
                    }
                }
            }
            else
            {
                this.GraphControlSetNodeCellEditValue(node, graphColumn.Index, editValue);
            }
		}

		private object GetControlCellValueFromPropertyValue(GraphElement graphElement, GraphColumnBindingPolicy columnBindingPolicy)
		{
            object? editValue = null;

			if (columnBindingPolicy != null && columnBindingPolicy.BindingOption == BindingOption.EnableEditing && !this.ColumnIndexesWithDisabledBinding.Contains(columnBindingPolicy.GraphColumn.Index))
			{
                SimpleObject? simpleObject = graphElement.SimpleObject;

                if (simpleObject != null)
                {
                    object? propertyValue = simpleObject.GetPropertyValue(columnBindingPolicy.PropertyModel.PropertyIndex);

                    editValue = this.EditorBindings.GetControlValueFromPropertyValue(simpleObject.GetType(), columnBindingPolicy.PropertyModel, propertyValue);
                }
			}

			return editValue;
		}

		private void RefreshNode(GraphElement graphElement, object node)
        {
            if (node != null)
            {
                this.BeginGraphUpdate();
                this.UpdateNodeInternal(graphElement, node);
                this.EndGraphUpdate();
            }
        }

        private void RefreshNodeBySimpleObject(SimpleObject simpleObject)
        {
            if (simpleObject == null || simpleObject is GraphElement || simpleObject.Id < 0) // || simpleObject.IsNew)
                return;

            foreach (GraphElement graphElement in simpleObject.GraphElements)
				if (graphElement != null && this.Contains(graphElement))
					this.RefreshGraphElement(graphElement);
        }

        private void SetColumnsEnableProperty(GraphElement? graphElement)
        {
            if (graphElement != null)
            {
                SimpleObject? simpleObject = graphElement.SimpleObject;

                if (simpleObject == null)
                    return;

                int tableId = simpleObject.GetModel().TableInfo.TableId;

                foreach (GraphColumn graphColumn in this.Columns)
                {
					bool columnEnabled = this.GetColumnEnableProperty(tableId, graphColumn.Index);
                    this.GraphControlSetColumnEnableProperty(graphColumn.Index, columnEnabled);
                }

                this.RaiseAfterSetGraphColumnsEnableProperty(graphElement);
            }
            else
            {
                foreach (GraphColumn graphColumn in this.Columns)
                    this.GraphControlSetColumnEnableProperty(graphColumn.Index, false);
            }
        }

        private bool GetColumnEnableProperty(int tableId, int columnIndex)
        {
            GraphColumnBindingPolicy columnBindingPolicy = this.GetGraphColumnBindingPolicyByColumnIndex(tableId, columnIndex);

			if (columnBindingPolicy != null)
				return columnBindingPolicy.BindingOption == BindingOption.EnableEditing;

            return false;
        }

        private GraphElement? GetParentGraphElementForNewGraphInsertion(GraphElement graphElement, AddButtonPolicy<GraphElement> addButtonPolicy)
        {
            GraphElement? anchorGraphElement = (graphElement != null) ? graphElement : this.anchorGraphElement;
            GraphElement? result = anchorGraphElement;

            // TODO: Provjeriti zašto je ponekad anchorGraphElement != null a anchorGraphElement.SimpleObject == null !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (anchorGraphElement != null && anchorGraphElement.SimpleObject != null)
            {
                GraphElement? parentGraphElement = (graphElement != null) ? graphElement.Parent : this.anchorGraphElement;
                Type graphElementSimpleObjectType = anchorGraphElement.SimpleObject.GetType();

                if (addButtonPolicy.AddButtonPolicyOption == AddButtonPolicyOption.AddAsChildToParent)
                    result = parentGraphElement;

                // If focused graph element and button are the same type -> set parent graph element as anchor if graph policy model do not allow sub tree creation for same type
                else if (graphElementSimpleObjectType == addButtonPolicy.ObjectType)
                {
                    // Set parent graph element as anchor if 
                    IGraphPolicyModel? graphPolicyModel = this.ObjectManager?.ModelDiscovery.GetGraphPolicyModel(this.GraphKey);
                    IGraphPolicyModelElement? graphPolicyModelAddButtonObjectType = graphPolicyModel?.GetGraphPolicyModelElement(addButtonPolicy.ObjectType);

                    if (graphPolicyModelAddButtonObjectType != null && graphPolicyModelAddButtonObjectType.SubTreePolicy == SubTreePolicy.DoNotAllowSubTree)
                        result = parentGraphElement;
                }
                else // check are the focused graph element and button has the same declared priority
                {
					IGraphPolicyModel? graphPolicyModel = this.ObjectManager?.ModelDiscovery.GetGraphPolicyModel(this.GraphKey);
                    IGraphPolicyModelElement? graphPolicyModelGraphElement = graphPolicyModel?.GetGraphPolicyModelElement(graphElementSimpleObjectType);
                    IGraphPolicyModelElement? graphPolicyModelAddButtonObjectType = graphPolicyModel?.GetGraphPolicyModelElement(addButtonPolicy.ObjectType);

                    if (graphPolicyModelGraphElement == null && graphElement != null) // if graphElement == null, this.anchorGraphElement is an anchor and graphPolicyModelGraphElement should not exists
                    {
                        XtraMessageBox.Show("GraphController: Error in Graph Policy Specification; GraphElement.SimpleObject Type=" + graphElementSimpleObjectType.Name, this.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (graphPolicyModelGraphElement == null)
                    {
                        result = this.anchorGraphElement; // anchor graph element not need to be in a policy
                    }
                    else if (graphPolicyModelAddButtonObjectType == null)
                    {
                        XtraMessageBox.Show("GraphController: Error in Add Button Graph Policy Specification; Add Button Policy Object Type=" + addButtonPolicy.ObjectType.Name, this.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (graphPolicyModelGraphElement.Priority == graphPolicyModelAddButtonObjectType.Priority) // If button and graph element priority are the same -> Set parent graph element as anchor
                    {
                        result = parentGraphElement;
                    }
                    else if (parentGraphElement == null && graphPolicyModelAddButtonObjectType.Priority < graphPolicyModelGraphElement.Priority) // for button up in priority (left in ribbon menu)
                    {
                        result = null; // parentGraphElement;
                    }
                    else if (parentGraphElement != null && !addButtonPolicy.IsSubButton) // for button down in priority (right in ribbon menu)
                    {
                        if (graphPolicyModelAddButtonObjectType.ParentAcceptablePriorities.Count() > 0 || graphPolicyModelAddButtonObjectType.ParentAcceptableTypes.Count() > 0)
                        {
                            if (graphPolicyModelAddButtonObjectType.ParentAcceptablePriorities.Contains(graphPolicyModelGraphElement.Priority) || 
                                graphPolicyModelAddButtonObjectType.ParentAcceptableTypes.Contains(graphPolicyModelGraphElement.ObjectType))
                            {
                                result = graphElement;
                            }
                            else if (graphPolicyModelGraphElement.ParentAcceptablePriorities.Contains(graphPolicyModelAddButtonObjectType.Priority) || graphPolicyModelGraphElement.ParentAcceptableTypes.Contains(graphPolicyModelAddButtonObjectType.ObjectType))
                            {
                                result = parentGraphElement.Parent;
							}
						}
						else if (graphPolicyModelAddButtonObjectType.Priority < graphPolicyModelGraphElement.Priority) // for button down in priority (right in ribbon menu)
                        {
							result = parentGraphElement.Parent;
						}
					}
                    //else if (this.ObjectManager.CanAddGraphElement(this.GraphKey, addButtonPolicy.ObjectType, parentGraphElement))
                    //{
                    //	result = parentGraphElement;
                    //}
                }
            }

            if (result != null && result.DeleteStarted)
                result = null;

            return result;
        }

		private void SetButtonsMoveUpEnableProperty(GraphElement? graphElement)
		{
			bool enabled = (graphElement != null) && this.GraphControlGetSortedColumnCount() == 0 && !graphElement.IsFirst;

			foreach (Component buttonMoveUp in this.ButtonMoveUpList)
				this.SetButtonEnablePropertyInternal(buttonMoveUp, enabled);
		}

		private void SetButtonsMoveDownEnableProperty(GraphElement graphElement)
		{
			bool enabled = (graphElement != null) && this.GraphControlGetSortedColumnCount() == 0 && !graphElement.IsLast;

			foreach (Component buttonMoveDown in this.ButtonMoveDownList)
				this.SetButtonEnablePropertyInternal(buttonMoveDown, enabled);
		}

		private void SetButtonsSaveEnableProperty(GraphElement graphElement)
        {
            bool enabled = false;

            if (this.SaveButonOption == SaveButtonOption.SaveFocusedNode)
                enabled = graphElement != null && graphElement.SimpleObject != null && graphElement.SimpleObject.RequireSaving();
            else if (this.ObjectManager != null)
                enabled = this.ObjectManager.DefaultChangeContainer.RequireCommit;
            
            this.SetButtonsSaveEnableProperty(enabled);
        }

        private void SetButtonsSaveEnableProperty(bool enabled)
        {
            foreach (Component button in this.ButtonSaveList)
                this.SetButtonEnablePropertyInternal(button, enabled);
        }

		private void SetButtonsRejectChangesEnableProperty(GraphElement graphElement)
		{
			bool enabled = false;

			if (graphElement != null)
			{
                if (!graphElement.IsNew)
                    enabled |= graphElement.GetChangedSaveablePropertyIndexes().ToModelSequence(graphElement.GetModel().PropertyModels).FirstOrDefault(model => !model.AvoidRejectChanges) != null;

                if (graphElement.SimpleObject != null && !graphElement.SimpleObject.IsNew)
                    enabled |= graphElement.SimpleObject.GetChangedSaveablePropertyIndexes().ToModelSequence(graphElement.SimpleObject.GetModel().PropertyModels).FirstOrDefault(model => !model.AvoidRejectChanges) != null;
            }

			this.SetButtonsRejectChangesEnableProperty(enabled);
		}

		private void SetButtonsRejectChangesEnableProperty(bool enabled)
		{
			foreach (Component button in this.ButtonRejectChangesList)
				this.SetButtonEnablePropertyInternal(button, enabled);
		}

		private void SetButtonEnablePropertyInternal(Component button, bool enabled)
        {
            this.GraphControlSetButtonEnableProperty(button, enabled);
            this.RaiseOnSetButtonEnableProperty(button);
        }

        private void RefreshContext(IBindingSimpleObject oldBindingObject, IBindingSimpleObject bindingObject)
        {
            if (this.IsCheckBoxMode)
            {
                // First remove checkings from old binding object.
                while (this.CheckedNodeList.Count > 0)
                {
                    object node = this.CheckedNodeList[0];
                    
                    this.SetGraphNodeCheckedProperty(node, false);
                }

                // Than set checkings for current binding object
                if (bindingObject is SimpleObject simpleObject)
                {
                    SimpleObjectCollection? memberCollection = simpleObject.GetGroupMemberCollection(this.ManyToManyRelationKey);

                    if (memberCollection != null)
					{
                        var groupMembershipCollection = memberCollection.GetGroupMembershipCollection();

                        if (groupMembershipCollection != null)
                        {
                            for (int i = 0; i < memberCollection.Count; i++)
                            {
                                SimpleObject item = memberCollection.ElementAt(i);
                                //long groupMembershipId = groupMembershipCollection.ObjectIdAt(i);
                                GroupMembership groupMembership = groupMembershipCollection.ElementAt(i);
                                GraphElement? graphElement = item.GetGraphElement(this.GraphKey);

                                if (graphElement != null)
                                {
                                    object? node = this.GetNode(graphElement);
                                    
                                    if (node != null)
                                    {
                                        GraphElementNodeTag nodeTag = this.GetGraphNodeTag(node);
                                        
                                        nodeTag.GroupMembership = groupMembership;
                                        this.SetGraphNodeCheckedProperty(node, true);
                                    }
                                }
                            }
                        }

                        //foreach (SimpleObject simpleObject in memberCollection)
                        //{
                        //    GraphElement graphElement = simpleObject.GetGraphElement(this.GraphKey);

                        //    if (graphElement != null)
                        //    {
                        //        object node = this.GetNode(graphElement);
                                
                        //        this.SetGraphNodeCheckedProperty(node, true);
                        //    }
                        //}
                    }

                    return;

                    //IList focusedBindingObjectManyToManyObjectCollection = null;

                    //try
                    //{
                    //    focusedBindingObjectManyToManyObjectCollection = (bindingObject as IBindingSimpleObject).GetGroupMemberCollection(this.ManyToManyRelationKey);
                    //}
                    //catch
                    //{
                    //    focusedBindingObjectManyToManyObjectCollection = null;
                    //}

                    //if (focusedBindingObjectManyToManyObjectCollection != null)
                    //{
                    //    foreach (SimpleObject simpleObject in focusedBindingObjectManyToManyObjectCollection)
                    //    {
                    //        GraphElement graphElement = simpleObject.GetGraphElement(this.GraphKey);

                    //        if (graphElement != null)
                    //        {
                    //            object node = this.GetNode(graphElement);
                    //            this.SetGraphNodeCheckedProperty(node, true);
                    //        }
                    //    }
                    //}
                    //else
                    //{

                    //}
                }
            }
        }

        private void SetGraphNodeCheckedProperty(object node, bool value)
        {
            GraphElementNodeTag nodeTag = this.GraphControlGetGraphNodeTag(node);

            if (nodeTag.Checked != value)
            {
                nodeTag.Checked = value;
                this.GraphControlSetNodeCheckedProperty(node, value);

                if (nodeTag.Checked)
                {
                    this.CheckedNodeList.Add(node);
                }
                else
                {
                    this.CheckedNodeList.Remove(node);
                }

                this.SetNodeCheckBoxImage(node);

                object parentNode = this.GraphControlGetParentNode(node);

                if (parentNode != null)
                {
                    int increasingCheckCountValue = nodeTag.Checked ? 1 : -1;
                    this.IncreaseChildNodesCheckCount(parentNode, increasingCheckCountValue);
                }
            }
        }

        private void SetGraphNodeCheckDisabled(object node, bool value)
        {
            GraphElementNodeTag nodeTag = this.GraphControlGetGraphNodeTag(node);

            if (nodeTag.CheckDisabled != value)
            {
                nodeTag.CheckDisabled = value;
                this.SetNodeCheckBoxImage(node);
            }
        }

        private void IncreaseChildNodesCheckCount(object node, int increasingCheckCountValue)
        {
            GraphElementNodeTag nodeTag = this.GraphControlGetGraphNodeTag(node);
            nodeTag.ChildrendNodesCheckCount += increasingCheckCountValue;
            this.SetNodeCheckBoxImage(node);

            object parentNode = this.GraphControlGetParentNode(node);

            if (parentNode != null)
                this.IncreaseChildNodesCheckCount(parentNode, increasingCheckCountValue);
        }

        private void SetNodeCheckBoxImage(object node)
        {
            int imageIndex = -1;
            GraphElementNodeTag nodeTag = this.GraphControlGetGraphNodeTag(node);

            if (nodeTag.Checked)
            {
                if (nodeTag.CheckDisabled)
                {
                    imageIndex = nodeTag.ChildrendNodesCheckCount > 0 ? this.CheckBoxCheckedChildCheckedGreyedImageIndex : this.CheckBoxCheckedGreyedImageIndex;
                }
                else
                {
                    imageIndex = nodeTag.ChildrendNodesCheckCount > 0 ? this.CheckBoxCheckedChildCheckedImageIndex : this.CheckBoxCheckedImageIndex;
                }
            }
            else
            {
                if (nodeTag.CheckDisabled)
                {

                    imageIndex = nodeTag.ChildrendNodesCheckCount > 0 ? this.CheckBoxUncheckedChildCheckedGreyedImageIndex : this.CheckBoxUncheckedGreyedImageIndex;
                }
                else
                {
                    imageIndex = nodeTag.ChildrendNodesCheckCount > 0 ? this.CheckBoxUncheckedChildCheckedImageIndex : this.CheckBoxUncheckedImageIndex;
                }
            }

            this.GraphControlSetNodeCheckBoxImageIndex(node, imageIndex);
        }

        private void SetGraphValidationError(GraphValidationResult graphValidationResult)
        {
            this.GraphControlSetColumnError(graphValidationResult.ErrorGraphColumnIndex, graphValidationResult.Message);
            this.isGraphErrorMessageSet = true;
            this.GraphControlSetFocusedColumn(graphValidationResult.ErrorGraphColumnIndex);

            bool columnEnabled = this.GraphControlGetColumnEnableProperty(graphValidationResult.ErrorGraphColumnIndex);

            if (columnEnabled)
            {
                this.GraphControlFocus();
                this.GraphControlShowEditor();
            }
        }

        public Form? FindGraphControlForm()
        {
            Form? result = null;

            if (this.GraphControl != null)
                result = this.GraphControl.FindForm();

            return result;
        }

		private bool FindNextTextInAnyChildNodes(object startNode, string textToFind, bool matchCase)
		{
			if (this.FindNextTextInSingleNode(startNode, textToFind, matchCase))
			{
				return true;
			}
			else
			{
				this.LoadChildNodesIfNotLoaded(startNode);
				
				foreach (object childNode in this.GraphControlGetChildNodes(startNode))
					if (this.FindNextTextInAnyChildNodes(childNode, textToFind, matchCase))
						return true;
			}

			return false;
		}

		private bool FindNextTextInSingleNode(object node, string textToFind, bool matchCase)
		{
			//bool isFirstPass = true;

			while (this.findColumnIndex < this.Columns.Count)
			{
				GraphColumn? graphColumn = this.Columns[this.findColumnIndex];

                if (graphColumn is null)
                    continue;

				object cellValue = this.GetCellEditValue(node, graphColumn.Index);

				if (cellValue is string cellText && cellText is not null)
				{
					string normalizedTextToFind = textToFind;

					if (!matchCase)
					{
						cellText = cellText.ToLower();
						normalizedTextToFind = textToFind.ToLower();
					}

					int startTextPosition = (cellText.Length >= this.findColumnPosition) ? cellText.IndexOf(normalizedTextToFind, this.findColumnPosition) : -1;

					if (startTextPosition >= 0)
					{
						//if (isFirstPass)
						//	this.findColumnPosition = startTextPosition + textToFind.Length;

						this.GraphControlSetFocusedNode(node);
						this.GraphControlSetFocusedColumn(graphColumn.Index);
						this.GraphControlShowEditor();
						this.GraphControlHighlightCellText(node, graphColumn.Index, startTextPosition, textToFind.Length);
						this.findColumnPosition = startTextPosition + 1;

						return true;
					}
				}

//				isFirstPass = false;
				this.findColumnIndex++;
			}

			this.findColumnIndex = 0;
			this.findColumnPosition = 0;

			return false;
		}

		private object? GetNextNode(object node)
		{
			if (node == null)
				return null;

			object? parentNode = this.GraphControlGetParentNode(node);
            object[] childNodes = (parentNode is not null) ? this.GraphControlGetChildNodes(parentNode) 
                                                           : new object[] { };
            bool isFound = false;

			foreach (object currentNode in childNodes)
			{
				if (isFound)
					return currentNode;

				if (currentNode == node)
					isFound = true;
			}

			if (isFound && parentNode is not null)
				return this.GetNextNode(parentNode); // This node is the last node in a collection, go and find next parent node

			return null;
		}

		private GraphColumnBindingPolicy? CreateDefaultGraphColumnBindingPolicyByColumnIndex(int tableId, int columnIndex)
		{
			GraphColumnBindingPolicy? result = null;
			GraphColumn? graphColumn = this.Columns[columnIndex];

            if (graphColumn is null)
                return null;

            ISimpleObjectModel objectModel = this.ObjectManager.GetObjectModel(tableId);
			IPropertyModel propertyModel = objectModel.PropertyModels[graphColumn.Name];

			if (propertyModel != null)
				result = new GraphColumnBindingPolicy(propertyModel, graphColumn);

			return result;
		}

		private GraphColumnBindingPolicy? CreateDefaultGraphColumnBindingPolicyByPropertyIndex(int tableId, int propertyIndex)
		{
			GraphColumnBindingPolicy? result = null;
            ISimpleObjectModel objectModel = this.ObjectManager.GetObjectModel(tableId);
			IPropertyModel propertyModel = objectModel.PropertyModels[propertyIndex];
			GraphColumn graphColumn = this.GetGraphColumn(propertyModel.PropertyName);

			if (graphColumn != null)
				result = new GraphColumnBindingPolicy(propertyModel, graphColumn);

			return result;
		}

		private void GraphControlButtonMoveAction(Action<GraphElement> graphElementMoveAction, object? requester = null)
		{
			if (!this.IsActive)
				return;

			object node = this.GraphControlGetFocusedNode();
			GraphElement graphElement = this.GetGraphElement(node);

			if (graphElement != null)
			{
				this.GraphControlCloseEditor();

				////bool requireSave = graphElement.RequireSaving() || graphElement.SimpleObject.RequireSaving();

				//if (graphElement.SimpleObject.IsNew)
				//{
				//	SimpleObjectValidationResult validationResult = graphElement.SimpleObject.Validate();

				//	if (!validationResult.Passed)
				//	{
				//		this.ValidateAndTrySaveNodeIfRequired(this, node);

				//		return;
				//	}
				//}

				//Thread thread = new Thread(() =>
				//	{
				//		//this.BeginGraphUpdate();

				//		graphElementMoveAction(graphElement);
				//		graphElement.Save();

				//		//this.EndGraphUpdate();
				//	});

				//thread.IsBackground = true;
				//thread.Priority = ThreadPriority.Normal;
				//thread.Start();

				this.BeginGraphUpdate();

                graphElement.Requester = requester;
                graphElementMoveAction(graphElement);
				//graphElement.Save();

				this.EndGraphUpdate();
			}
		}

		private int GetMaxPropertyIndex(int tableId)
		{
			var objectModel = this.ObjectManager.ModelDiscovery.GetObjectModel(tableId);
			var maxPropertyIndex = objectModel.PropertyModels.Max(property => property.PropertyIndex);

			return maxPropertyIndex;
		}

		#endregion |   Private Methods   |

		#region |   IGraphControllerControl Interface   |

		int IGraphController.AddColumn(string columnName, string caption, BindingDataType dataType, BindingEditorType editorType)
		{
			return this.GraphControlAddColumn(columnName, caption, dataType, editorType);
		}

		void IGraphController.RemoveColumn(int columnIndex)
		{
			this.GraphControlRemoveColumn(columnIndex);
		}

		int IGraphController.GetColumnWidth(int columnIndex)
		{
			return this.GraphControlGetColumnWidth(columnIndex);
		}

		void IGraphController.SetColumnWidth(int columnIndex, int width)
		{
			this.GraphControlSetColumnWidth(columnIndex, width);
		}

		bool IGraphController.GetColumnEnableProperty(int columnIndex)
		{
			return this.GetGraphColumnEnableProperty(columnIndex);
		}

		void IGraphController.SetColumnEnableProperty(int columnIndex, bool value)
		{
			this.SetGraphColumnEnableProperty(columnIndex, value);
		}

		bool IGraphController.GetColumnVisibleProperty(int columnIndex)
		{
			return this.GetGraphColumnVisibleProperty(columnIndex);
		}

		void IGraphController.SetColumnVisibleProperty(int columnIndex, bool value)
		{
			this.SetGraphColumnVisibleProperty(columnIndex, value);
		}

		bool IGraphController.GetColumnShowInCustomizationFormProperty(int columnIndex)
		{
			return this.GetGraphColumnShowInCustomizationFormProperty(columnIndex);
		}

		void IGraphController.SetColumnShowInCustomizationFormProperty(int columnIndex, bool value)
		{
			this.SetGraphColumnShowInCustomizationFormProperty(columnIndex, value);
		}

		void IGraphController.SetColumnCaption(int columnIndex, string caption)
		{
			this.GraphControlSetColumnCaption(columnIndex, caption);
		}

		void IGraphController.SetColumnDataType(int columnIndex, BindingDataType dataType)
		{
			this.GraphControlSetColumnDataType(columnIndex, dataType);
		}

		void IGraphController.SetColumnEditorType(int columnIndex, BindingEditorType editorType)
		{
			this.GraphControlSetColumnEditorType(columnIndex, editorType);
		}

		void IGraphController.BeforeNodeIsFocused(object node, object oldNode, ref bool canFocus)
		{
			this.GraphControlBeforeNodeIsFocused(node, oldNode, ref canFocus);
		}
		
		void IGraphController.FocusedNodeIsChanged(object node, object oldNode)
		{
			this.GraphControlFocusedNodeIsChanged(node, oldNode);
		}
		
		void IGraphController.CellValueIsChanged(object node, int columnIndex, object value)
		{
			this.GraphControlCellValueIsChanged(node, columnIndex, value);
		}
		
		void IGraphController.BeforeNodeIsCollapsed(object node, ref bool canCollapse)
		{
			this.GraphControlBeforeNodeIsCollapsed(node, ref canCollapse);
		}
		
		void IGraphController.BeforeNodeIsExpanded(object node)
		{
			this.GraphControlBeforeNodeIsExpanded(node);
		}
		
		void IGraphController.NodeIsExpanded(object node)
		{
			this.GraphControlNodeIsExpanded(node);
		}
		
		void IGraphController.NodeIsCollapsed(object node)
		{
			this.GraphControlNodeIsCollapsed(node);
		}
		
		void IGraphController.BeforeNodeCheckStateIsChanged(object node, bool checkValue, ref bool canCheck)
		{
			this.GraphControlNodeCheckStateIsChanged(node, checkValue, ref canCheck);
		}

		void IGraphController.SortedColumnCountIsChanged()
		{
			this.SetButtonsSortingEnableProperty();
		}
		
		bool IGraphController.CanDragNode(object node)
		{
			return this.GraphControlCanDragNode(node);
		}
		
		DragDropEffects IGraphController.GetDragDropEffect(object dragNode, object targetNode)
		{
			return this.GraphControlGetDragDropEffect(dragNode, targetNode);
		}
		
		void IGraphController.DragDrop(object dragNode, object targetNode)
		{
			this.GraphControlDragDrop(dragNode, targetNode);
		}
		
		void IGraphController.ButtonSaveIsClicked(Component button)
		{
			this.GraphControlButtonSaveIsClicked(button);
		}
		
		void IGraphController.ButtonRemoveIsClicked(Component button)
		{
			this.GraphControlButtonRemoveIsClicked(button);
		}
		
		void IGraphController.OnKeyDown(object? sender, KeyEventArgs e)
		{
			this.OnKeyDown(sender, e);
		}

		#endregion |   IGraphControllerControl Interface   |

		#region |   IBindingObjectControl Interface   |

		bool IBindingObjectControl.SaveBindingObject(object requester, IBindingSimpleObject bindingObject)
        {
            bool result = true;
            object node = this.GraphControlGetFocusedNode();
            GraphElement graphElement = this.GraphControlGetGraphElementByNode(node);

            if (graphElement != null)
            {
                SimpleObject bussinessObject = graphElement.SimpleObject;

                if (bussinessObject != null && bindingObject is SimpleObject && bindingObject as SimpleObject == bussinessObject)
                    result = this.ValidateAndTrySaveNodeIfRequired(requester, node);
            }

            return result;
        }

        void IBindingObjectControl.RefreshButtonsEnableProperty()
        {
            this.SetButtonsEnableProperty();
        }

        //void IBindingObjectControl.RefreshBindingObjectContext(object requester, IBindingObject bindingObject)
        //{
        //    GraphElement graphElement = (bindingObject as SimpleObject).GetGraphElement(this.Graph);
        //    this.RefreshGraphElement(graphElement);

        //    if (this.ChangableBindingObjectControl != null)
        //    {
        //        this.ChangableBindingObjectControl.RefreshBindingObjectContext(requester, bindingObject);
        //    }
        //}

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
                this.components = null;
                this.objectManager = null;
                this.editorBindings.Dispose();
                this.editorBindings = null;
                this.nodesByGraphElement = null;
                this.columns = null;
                this.anchorGraphElement = null;
                this.changableBindingObjectControl = null;
                this.lastValidationNodeResult = null;
                this.imageList = null;
                this.stateImageList = null;
                this.checkBoxImageList = null;
                this.columnBindingPolicyByColumnIndexByTableId = null;
                this.columnNamesWithDisabledBinding = null;
                this.addButtonPolicyList = null;
                this.buttonInsertObjectList = null;
                this.buttonSaveList = null;
                this.buttonRemoveList = null;

                this.BindingObjectChange = null;
                this.BindingObjectPropertyValueChange = null;
                this.BindingObjectOnValidation = null;
                this.BindingObjectPushData = null;
                this.GraphElementCreated = null;
                this.BeforeGraphElementDeleted = null;
                this.SimpleObjectPropertyValueChange = null;
                this.BindingObjectParentGraphElementChange = null;
                this.AfterSetColumnsEnableProperty = null;
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

    public enum AddButtonPolicyOption
    {
        AddAsChild,
        AddAsChildToParent
    }

    public enum BindingOption
    {
        EnableEditing,
        DisableEditing
    }

    #endregion |   Enums   |

    #region |   Delegates   |

    //public delegate void ComponentEventHandler(object sender, ComponentEventArgs e);
    public delegate void EditValueGraphElementEventHandler(object sender, EditValueGraphElementEventArgs e);
    public delegate void GraphElementNodeEventHandler(object sender, GraphElementNodeEventArgs e);
    public delegate void ReloadGraphElementNodeEventHandler(object sender, ReloadGraphElementNodeEventArgs e);
    public delegate void CheckGraphElementEventHandler(object sender, CheckGraphElementEventArgs e);
    public delegate void CompareColumnNodeValues(object sender, CompareGraphColumnNodeValuesEventArgs e);
	public delegate void AskBeforeDeleteGraphElementEventHandler(object sender, AskBeforeDeleteGraphElementEventArgs e);
	public delegate void AllowAddButtonPolicy(object sender, AllowAddButtonPolicytEventArgs e);

    #endregion |   Delegates   |

    #region |   Event Delegates   |

    public delegate void GraphElementChangeContainerRequesterEventHandler(object sender, GraphElementChangeContainerRequesterEventArgs e);
    //public delegate void AnchorGraphElementEventHandler(object sender, AnchorGraphElementEventArgs e);

    #endregion |   Event Delegates   |

    #region |   EventArgs Classes   |

    //public class AnchorGraphElementEventArgs : EventArgs
    //{
    //    public AnchorGraphElementEventArgs()
    //    {
    //    }

    //    public GraphElement AnchorGraphElement { get; set; }
    //}

    public class AllowAddButtonPolicytEventArgs : AddButtonPolicytEventArgs
	{
		public AllowAddButtonPolicytEventArgs(Component component, AddButtonPolicy<GraphElement> addButtonPolicy, GraphElement parentGraphElement, bool allow)
			: base(component, addButtonPolicy, parentGraphElement)
		{
			this.Allow = allow;
		}

		public bool Allow { get; set; }
	}

	public class AddButtonPolicytEventArgs : ButtonEventArgs
	{
		public AddButtonPolicytEventArgs(Component button, AddButtonPolicy<GraphElement> addButtonPolicy, GraphElement parentGraphElement)
			: base(button)
		{
			this.AddButtonPolicy = addButtonPolicy;
			this.ParentGraphElement = parentGraphElement;
		}
		
		public AddButtonPolicy<GraphElement> AddButtonPolicy { get; private set; }
		public GraphElement ParentGraphElement { get; private set; }
	}
	
	public class ButtonEventArgs : EventArgs
    {
        public ButtonEventArgs(Component button)
        {
            this.Button = button;
        }

        public Component Button { get; private set; }
    }

    public class ReloadGraphElementNodeEventArgs : GraphElementNodeEventArgs
    {
        public ReloadGraphElementNodeEventArgs(GraphElement graphElement, object node, bool isReloaded)
            : base(graphElement, node)
        {
            this.IsReloaded = isReloaded;
        }

        public bool IsReloaded { get; set; }
    }

    public class EditValueGraphElementEventArgs : GraphElementEventArgs
    {
		public EditValueGraphElementEventArgs(GraphElement graphElement, object node, GraphColumn graphColumn, object? editValue)
            : base(graphElement)
        {
            this.Node = node;
            this.GraphColumn = graphColumn;
            this.EditValue = editValue;
        }

        public object Node { get; private set; }
		public GraphColumn GraphColumn { get; private set; }
        public object? EditValue { get; set; }
    }

    public class CheckGraphElementEventArgs : GraphElementEventArgs
    {
        public CheckGraphElementEventArgs(GraphElement graphElement, bool checkValue, bool canCheck)
			: base(graphElement)
        {
            this.CheckValue = checkValue;
            this.CanCheck = canCheck;
        }
        public bool CheckValue { get; private set; }
        public bool CanCheck { get; set; }
    }

	public class AskBeforeDeleteGraphElementEventArgs : GraphElementEventArgs
	{
		public AskBeforeDeleteGraphElementEventArgs(GraphElement graphElement, InputUserSource inputSource, bool askBeforeDelete)
			: base(graphElement)
		{
			this.InputSource = inputSource;
			this.AskBeforeDelete = askBeforeDelete;
		}

		public bool AskBeforeDelete { get; set; }
		public InputUserSource InputSource { get; set; }
	}

    /// <summary>
    /// Provides data for the Simple.Controls.CompareColumnNodeValues event.
    /// </summary>
    public class CompareGraphColumnNodeValuesEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a new Simple.Controls.CompareGraphColumnNodeValuesEventArgs object.
        /// </summary>
        /// <param name="node1">A object representing the node containing the first value involved in the comparison. 
        /// This value is assigned to the Simple.Controls.CompareGraphColumnNodeValuesEventArgs.Node1 property.</param>
        /// <param name="node2">A object representing the node containing the first value involved in the comparison. 
        /// This value is assigned to the Simple.Controls.CompareGraphColumnNodeValuesEventArgs.Node2 property.</param>
        /// <param name="nodeValue1">An object representing the first value involved in the comparison. This value
        /// is assigned to the Simple.Controls.CompareGraphColumnNodeValuesEventArgs.NodeValue1 property.</param>
        /// <param name="nodeValue2">An object representing the first value involved in the comparison. This value
        /// is assigned to the Simple.Controls.CompareGraphColumnNodeValuesEventArgs.NodeValue2 property.</param>
        /// <param name="graphColumn">A Simple.Controls.GraphColumn object representing the column against whose values 
        /// the data will be sorted. This value is assigned to the Simple.Controls.CompareGraphColumnNodeValuesEventArgs.Column property.</param>
        /// <param name="sortOrder">A System.Windows.Forms.SortOrder enumeration member representing the sort order to be applied. 
        /// This value is assigned to the Simple.Controls.CompareGraphColumnNodeValuesEventArgs.SortOrder property.</param>
        /// <param name="result">An integer value representing the comparison result. This value is assigned to the 
        /// Simple.Controls.CompareGraphColumnNodeValuesEventArgs.Result property</param>
        public CompareGraphColumnNodeValuesEventArgs(GraphColumn graphColumn, object node1, object node2, object nodeValue1, object nodeValue2, SortOrder sortOrder, int result)
        {
            this.GraphColumn = graphColumn;
            this.Node1 = node1;
            this.Node2 = node2;
            this.NodeValue1 = nodeValue1;
            this.NodeValue2 = nodeValue2;
            this.SortOrder = sortOrder;
            this.Result = result;
        }

        /// <summary>
        /// Gets the graph column against whose values sorting is performed.
        /// </summary>
        public GraphColumn GraphColumn { get; private set; }

        /// <summary>
        /// Gets the node containing the first value involved in comparison.
        /// </summary>
        public object Node1 { get; private set; }

        /// <summary>
        /// Gets the node containing the second value involved in comparison.
        /// </summary>
        public object Node2 { get; private set; }

        /// <summary>
        /// Gets the first value involved in comparison.
        /// </summary>
        public object NodeValue1 { get; private set; }

        /// <summary>
        /// Gets the second value involved in comparison.
        /// </summary>
        public object NodeValue2 { get; private set; }

        /// <summary>
        /// Gets or sets a comparison result.
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// Gets the sort order applied to the column whose values are going to be compared.
        /// </summary>
        public SortOrder SortOrder { get; private set; }
    }

    public class GraphElementChangeContainerRequesterEventArgs : GraphElementRequesterEventArgs
    {
        public GraphElementChangeContainerRequesterEventArgs(GraphElement graphElement, ChangeContainer changeContainer, object? requester)
            : base(graphElement, requester)
        {
            this.ChangeContainer = changeContainer;
        }

        public ChangeContainer ChangeContainer { get; set; }
    }

    #endregion |   EventArgs Classes   |

    #region |   Helper Classes   |

    public class GraphElementNodeTag : GraphNodeTag<GraphElement>
	{
        public GraphElementNodeTag(GraphElement graphElement)
			: base(graphElement)
        {
            //this.GraphElement = graphElement;
            //this.ImageIndex = -1;
            //this.ExpandedImageIndex = -1;
            //this.Expanded = false;
            //this.Checked = false;
            //this.Greyed = false;
            //this.ChildNodesCheckCount = 0;
            //this.Tag = null;
        }
        public GroupMembership GroupMembership { get; internal set; }

        //public GraphElement GraphElement { get; set; }
        //public int ImageIndex { get; set; }
        //public int StateImageIndex { get; set; }
        //public int ExpandedImageIndex { get; set; }
        //public bool Expanded { get; set; }
        //public bool Checked { get; set; }
        //public bool Greyed { get; set; }
        //public int ChildNodesCheckCount { get; set; }
        //public bool ChildNodesLoaded { get; set; }
        //public bool ChildNodesLoading { get; set; }
        //public object Tag { get; set; }
    }

    public class AddButtonPolicy<TGraphElement> where TGraphElement : IPropertyValue
    {
		public AddButtonPolicy(Type objectType)
			: this(objectType, null)
		{
		}

		public AddButtonPolicy(Type objectType, Action<TGraphElement>? objectAction)
        {
            this.ObjectType = objectType;
			this.ObjectAction = objectAction;
            this.AddButtonPolicyOption = AddButtonPolicyOption.AddAsChild;
            this.IsSubButton = false;
            this.Buttons = new List<Component>();
        }

        public Type ObjectType { get; set; }
        public AddButtonPolicyOption AddButtonPolicyOption { get; set; }
        public bool IsSubButton { get; set; }
		public Action<TGraphElement>? ObjectAction { get; set; }
        public List<Component> Buttons { get; private set; }
    }

	public class AddNewObjectResult
	{
		public AddNewObjectResult(GraphElement? graphElement, bool success)
		{
			this.GraphElement = graphElement;
			this.Success = success;
		}

		public GraphElement? GraphElement { get; set; }
		public bool Success { get; set; }
	}

    public class GraphColumnBindingPolicy : GraphColumnBindingPolicy<GraphElement>
	{
		//public ColumnBindingPolicy()
		//	: this(null)
		//{
		//}

		public GraphColumnBindingPolicy(IPropertyModel propertyModel, GraphColumn graphColumn)
			: this(propertyModel, graphColumn, null, (propertyModel.AccessPolicy == PropertyAccessPolicy.ReadOnly) ? BindingOption.DisableEditing : BindingOption.EnableEditing)
		{
		}

		public GraphColumnBindingPolicy(IPropertyModel propertyModel, GraphColumn graphColumn, Func<GraphElement, object>? getEditValue, BindingOption bindingOption)
			: base(propertyModel, graphColumn, getEditValue, bindingOption)
		{
			//this.PropertyModel = propertyModel;
			//this.GraphColumn = graphColumn;
			//this.GetEditValue = getEditValue;
			//this.BindingOption = bindingOption;
		}

		//      public IPropertyModel PropertyModel { get; private set; }
		//public GraphColumn GraphColumn { get; private set; }
		//      public Func<GraphElement, object> GetEditValue { get; private set; }
		//      public BindingOption BindingOption { get; private set; }
	}

    #endregion |   Helper Classes   |

    #region |   Interfaces   |

    public interface IChangableBindingObjectControl : IBindingObjectControl
    {
        event SimpleObjectEventHandler BindingObjectChange;
    }

    public interface IBindingObjectControl
    {
        event ChangePropertyValueRequesterBindingObjectEventHandler BindingObjectPropertyValueChange;
        event BindingObjectRelationForeignObjectSetEventHandler BindingObjectRelationForeignObjectSet;
        event ChangeParentGraphElementRequesterEventHandler BindingObjectParentGraphElementChange;
        event GraphValidationResultEventHandler BindingObjectOnValidation;
        event SimpleObjectEventHandler BindingObjectPushData;
        event ChangePropertyValueRequesterBindingObjectEventHandler BindingObjectRefreshContext;


        /// <summary>
        /// Request Object Manager to Save binded SimpleObject
        /// </summary>
        /// <param name="requester"></param>
        /// <param name="bindingObject"></param>
        /// <returns></returns>
        bool SaveBindingObject(object requester, IBindingSimpleObject bindingObject);

        void RefreshButtonsEnableProperty();
    }

    #endregion |   Interfaces   |

    #region |   Enums   |

	public enum InputUserSource
	{
		Button,
		Keyboard
	}

	#endregion |   Enums   |
}