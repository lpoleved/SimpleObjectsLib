using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Simple;
using Simple.Modeling;
using Simple.Objects;
using DevExpress.XtraLayout.Resizing;

namespace Simple.Objects.Controls
{
    public partial class SimpleObjectEditPanel : XtraUserControl, ISimpleEditPanel, IBindingSimpleEditPanel, IBindingControl, IChangableBindingObjectControl
	{
		#region |   Private Memebers   |

		private SimpleObjectManager? objectManager = null;
		private bool isEditorsInitialized = false;
		//private bool refreshContextOnEveryBindingObjectPropertyValueChange = true;

		#endregion |   Private Memebers   |

		#region |   Constructors and Initialization   |

		public SimpleObjectEditPanel()
        {
            InitializeComponent();

            if (this.EditorBindings != null)
                this.EditorBindings.ChangableBindingObjectControl = this;
        }

        #endregion |   Constructors and Initialization   |

        #region |   Events   |

        public event BindingObjectEventHandler? BindingObjectChange;
        public event ChangePropertyValueBindingObjectRequesterEventHandler? BindingObjectPropertyValueChange;
        public event BindingObjectRelationForeignObjectSetEventHandler? BindingObjectRelationForeignObjectSet;
        public event OldParentGraphElemenChangeContainertContextRequesterEventHandler? BindingObjectParentGraphElementChange;
        public event GraphValidationResultEventHandler? BindingObjectOnValidation;
        public event BindingObjectEventHandler? BindingObjectPushData;
        public event ChangePropertyValueBindingObjectRequesterEventHandler? BindingObjectRefreshContext;
		public event ChangePropertyValueBindingObjectRequesterEventHandler? BindingObjectReloadData;

		//private BindingObjectEventHandler? bindingObjectPushData;
		//event BindingObjectEventHandler IBindingObjectControl.BindingObjectPushData
		//{
		//	add
		//	{
		//		this.bindingObjectPushData += value;
		//	}
		//	remove
		//	{
		//		this.bindingObjectPushData -= value;
		//	}
		//}

		//private BindingObjectEventHandler? bindingObjectChange;
		//event BindingObjectEventHandler IChangableBindingObjectControl.BindingObjectChange
		//{
		//	add
		//	{
		//		this.bindingObjectChange += value;
		//	}
		//	remove
		//	{
		//		this.bindingObjectChange -= value;
		//	}
		//}

		#endregion |   Events   |

		#region |   Public Properties   |

		[Category("Simple"), DefaultValue(null)]
		public IChangableBindingObjectControl? ChangableBindingObjectControl { get; private set; } = null;
        //{
        //    get { return this.changableBindingObjectControl; }
        //    set 
        //    {
        //        if (this.changableBindingObjectControl != null)
        //        {
        //            this.changableBindingObjectControl.BindingObjectChange -= new BindingObjectEventHandler(changableBindingObjectControl_BindingObjectChange);
        //            this.changableBindingObjectControl.PropertyValueChange -= new ChangedPropertyValueRequesterBindingObjectEventHandler(changableBindingObjectControl_PropertyValueChange);
        //            this.changableBindingObjectControl.OnValidation -= new ValidationResultBindingObjectEventHandler(changableBindingObjectControl_OnValidation);
        //            this.changableBindingObjectControl.StoreData -= new BindingObjectEventHandler(changableBindingObjectControl_StoreData);
        //        }

        //        this.changableBindingObjectControl = value;

        //        if (this.changableBindingObjectControl != null)
        //        {
        //            this.changableBindingObjectControl.BindingObjectChange += new BindingObjectEventHandler(changableBindingObjectControl_BindingObjectChange);
        //            this.changableBindingObjectControl.PropertyValueChange += new ChangedPropertyValueRequesterBindingObjectEventHandler(changableBindingObjectControl_PropertyValueChange);
        //            this.changableBindingObjectControl.OnValidation += new ValidationResultBindingObjectEventHandler(changableBindingObjectControl_OnValidation);
        //            this.changableBindingObjectControl.StoreData += new BindingObjectEventHandler(changableBindingObjectControl_StoreData);
        //        }
        //    }
        //}

        //[Category("Simple"), DefaultValue(true)]
        //public bool RefreshContextOnEveryBindingObjectPropertyValueChange
        //{
        //	get { return this.refreshContextOnEveryBindingObjectPropertyValueChange; }
        //	set { this.refreshContextOnEveryBindingObjectPropertyValueChange = value; }
        //}

        [Category("Simple"), DefaultValue(null), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SimpleObjectManager? ObjectManager
		{
			get { return this.objectManager; }

			set
			{
				this.objectManager = value;

				if (this.editorBindings != null)
					this.editorBindings.ObjectManager = this.objectManager;
			}
		}

		[Category("Simple"), DefaultValue(null), Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SimpleObject? BindingObject { get; private set; } = null;

        [Category("Simple"), DefaultValue(null), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EditorBindingsControl EditorBindings
        {
            get { return this.editorBindings; }
        }
        
        #endregion |   Public Properties   |

		#region |   Public Methods   |

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

		public void ShowRibbonPrintPreview(DevExpress.XtraTreeList.TreeList treeList)
		{
			Cursor? currentCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;

			treeList.ShowRibbonPrintPreview();

			Cursor.Current = currentCursor;
		}

		public void PrintTreeList(DevExpress.XtraTreeList.TreeList treeList)
		{
			Cursor? currentCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;

			treeList.Print();

			Cursor.Current = currentCursor;
		}

		public void SetBindingObject(SimpleObject? simpleObject)
		{
			if (this.BindingObject != simpleObject)
			{
				this.BindingObject = simpleObject;

				if (!this.isEditorsInitialized)
					this.InitializeEditors();

				this.RaiseBindingObjectChange(new BindingObjectEventArgs(simpleObject));
				//this.ReloadData();
				//this.loadingIsInProgress = false;

				// Refresh panel while chenges on other object may influence on edit panel context.
				//if (simpleObject != null)
					this.OnRefreshContext(new ChangePropertyValueBindingObjectEventArgs(simpleObject, propertyModel: null, value: null, oldValue: null, isChanged: false, simpleObject?.ChangeContainer, simpleObject?.Context ?? ObjectActionContext.Unspecified, requester: this));
			}
		}

		#endregion |   Public Methods   |

		#region |   Protected Methods   |

		//protected void RegisterTabControl(XtraTabControl tabControl)
		//{
		//    this.tabControl = tabControl;
		//}

		//protected void RegisterBinding(BaseEdit bindingEditorControl, IPropertyModel propertyModel)
		//{
		//    this.RegisterBinding(bindingEditorControl, propertyModel, null, null);
		//}

		//protected void RegisterBinding(CheckEdit checkBox, IPropertyModel propertyModel, object checkedPropertyValue, object uncheckedPropertyValue)
		//{
		//    this.RegisterBinding(checkBox, propertyModel,
		//        controlValue => Converter.TryChangeType<bool>(controlValue) ? checkedPropertyValue : uncheckedPropertyValue,
		//        propertyValue => propertyValue.Equals(checkedPropertyValue));
		//}

		//protected void RegisterBinding(ComboBoxEdit comboBox, IPropertyModel propertyModel)
		//{
		//    this.RegisterBinding(comboBox, propertyModel, new Dictionary<int, IModel>());
		//}

		//protected void RegisterBinding(ComboBoxEdit comboBox, IPropertyModel propertyModel, IDictionary<int, IModel> modelDictionary)
		//{
		//    this.RegisterBinding(comboBox, propertyModel, modelDictionary, model => model.Name);
		//}

		//protected void RegisterBinding(ComboBoxEdit comboBox, IPropertyModel propertyModel, IDictionary<int, IModel> modelDictionary, Func<IModel, string> getName)
		//{
		//    this.RegisterBinding(comboBox, propertyModel, modelDictionary,
		//                         controlValue => EditControlHelper.GetPropertyValueFromControlValue<int, IModel>(controlValue, modelDictionary, getName),
		//                         propertyValue => EditControlHelper.GetControlValueFromPropertyValue<int, IModel>(propertyValue, modelDictionary, getName),
		//                         getName);
		//}

		//protected void RegisterBinding(ComboBoxEdit comboBox, IPropertyModel propertyModel, Func<object, object> getPropertyValueFromControlValue, Func<object, object> getControlValueFromPropertyValue)
		//{
		//    this.RegisterBinding(comboBox, propertyModel, new Dictionary<int, IModel>(), getPropertyValueFromControlValue, getControlValueFromPropertyValue, model => model.Name);
		//}

		//protected void RegisterBinding(ComboBoxEdit comboBox, IPropertyModel propertyModel, IDictionary<object, object> dictionary)
		//{
		//    Dictionary<object, object> keysByValue = new Dictionary<object,object>();

		//    comboBox.Properties.Items.BeginUpdate();

		//    try
		//    {
		//        foreach (var dictionaryItem in dictionary)
		//        {
		//            keysByValue.Add(dictionaryItem.Key, dictionaryItem.Value);
		//            comboBox.Properties.Items.Add(dictionaryItem.Value);
		//        }
		//    }
		//    finally
		//    {
		//        comboBox.Properties.Items.EndUpdate();
		//    }

		//    this.RegisterBinding(comboBox, propertyModel, controlValue => keysByValue[controlValue], propertyValue => dictionary[propertyValue]);
		//}

		//protected void RegisterBinding(ComboBoxEdit comboBox, IPropertyModel propertyModel, IDictionary<int, IModel> modelDictionary, Func<object, object> getPropertyValueFromControlValue, Func<object, object> getControlValueFromPropertyValue, Func<IModel, string> getName)
		//{
		//    comboBox.Properties.Items.BeginUpdate();

		//    try
		//    {
		//        foreach (IModel model in modelDictionary.Values)
		//        {
		//            comboBox.Properties.Items.Add(getName(model));
		//        }
		//    }
		//    finally
		//    {
		//        comboBox.Properties.Items.EndUpdate();
		//    }


		//    this.RegisterBinding(comboBox as BaseEdit, propertyModel, getPropertyValueFromControlValue, getControlValueFromPropertyValue);
		//}

		//protected void RegisterBinding(BaseEdit bindingEditorControl, IPropertyModel propertyModel, Func<object, object> getPropertyValueFromControlValue, Func<object, object> getControlValueFromPropertyValue)
		//{
		//    List<EditPanelPolicy> editPanelPolicyList = null;
		//    XtraTabPage tabPage = this.GetControlTabPabe(bindingEditorControl);

		//    EditPanelPolicy editPanelPolicy = new EditPanelPolicy()
		//    {
		//        BindingEditorControl = bindingEditorControl,
		//        PropertyName = propertyModel.Name,
		//        TabPage = tabPage,
		//        GetPropertyValueFromControlValue = getPropertyValueFromControlValue,
		//        GetControlValueFromPropertyValue = getControlValueFromPropertyValue
		//    };

		//    editPanelPolicyList = this.editPanelPolicyListByPropertyName[propertyModel.Name] as List<EditPanelPolicy>;

		//    if (editPanelPolicyList == null)
		//    {
		//        editPanelPolicyList = new List<EditPanelPolicy>();
		//        this.editPanelPolicyListByPropertyName.Add(propertyModel.Name, editPanelPolicyList);
		//    }

		//    editPanelPolicyList.Add(editPanelPolicy);

		//    this.editPanelPolicyByBindingEditorControl.Add(bindingEditorControl, editPanelPolicy);

		//    bindingEditorControl.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(bindingEditControl_EditValueChanging);
		//    this.errorProvider.SetError(bindingEditorControl, null);
		//}

		//protected void ReloadData()
		//{
		//    if (this.BindingObject != null)
		//    {
		//        ISimpleObjectModel objectModel = this.BindingObject.GetObjectModel();

		//        foreach (IPropertyModel propertyModel in objectModel.Properties)
		//        {
		//            object propertyValue = this.BindingObject[propertyModel.Name];
		//            this.ReloadData(propertyModel.Name, propertyValue);
		//        }
		//    }
		//}

		//protected void ReloadData(string propertyName, object propertyValue)
		//{
		//    List<EditPanelPolicy> editPanelPolicyList = this.editPanelPolicyListByPropertyName[propertyName] as List<EditPanelPolicy>;

		//    if (editPanelPolicyList != null)
		//    {
		//        bool currentIsLoadingInProgress = this.loadingIsInProgress;
		//        this.loadingIsInProgress = true;

		//        foreach (EditPanelPolicy editPanelPolicy in editPanelPolicyList)
		//        {
		//            object controlValue = editPanelPolicy.GetControlValueFromPropertyValue != null ? editPanelPolicy.GetControlValueFromPropertyValue(propertyValue) : propertyValue;
		//            string controlValueText = controlValue != null ? controlValue.ToString() : String.Empty;

		//            if (editPanelPolicy.BindingEditorControl is CheckEdit)
		//            {
		//                bool checkedValue = Converter.TryChangeType<bool>(controlValue);
		//                (editPanelPolicy.BindingEditorControl as CheckEdit).Checked = checkedValue;
		//            }
		//            else
		//            {
		//                editPanelPolicy.BindingEditorControl.Text = controlValueText;
		//            }
		//        }

		//        this.loadingIsInProgress = currentIsLoadingInProgress;
		//    }

		//    this.OnReloadData(propertyName, propertyValue, editPanelPolicyList);
		//}

		#endregion |   Protected Methods   |

		#region |   Protected Virtual Methods   |

		protected virtual void OnInitializeEditors()
        {
        }

        protected virtual void OnBindingObjectChange(BindingObjectEventArgs e)
        {
        }

        protected virtual void OnBindingObjectPropertyValueChange(ChangePropertyValueBindingObjectEventArgs e)
        {
        }

        protected virtual void OnBindingObjectForeignObjectSet(BindingObjectRelationForeignObjectSetEventArgs e)
        {
        }

        protected virtual void OnParentGraphElementChange(OldParentGraphElemenChangeContainertContextRequesterEventArgs e)
        {
        }

        protected virtual void OnBindingObjectValidation(GraphValidationResultEventArgs e)
        {
        }

        protected virtual void OnBindingObjectStoreData()
        {
        }

		protected virtual void OnRefreshContext(ChangePropertyValueBindingObjectEventArgs e)
        {
        }

		protected virtual void OnReloadData(ChangePropertyValueBindingObjectEventArgs e)
		{
			this.EditorBindings?.ReloadData();
		}
		
		#endregion |   Protected Virtual Methods   |

        #region |   Private Raise Event Methods   |

        private void RaiseBindingObjectChange(BindingObjectEventArgs args)
        {
			this.BindingObjectChange?.Invoke(this, args);
			this.OnBindingObjectChange(args);
        }

        private void RaiseBindingObjectPropertyValueChange(ChangePropertyValueBindingObjectEventArgs changePropertyValueBindingObjectRequesterEventArgs)
        {
            this.BindingObjectPropertyValueChange?.Invoke(this, changePropertyValueBindingObjectRequesterEventArgs);
            this.OnBindingObjectPropertyValueChange(changePropertyValueBindingObjectRequesterEventArgs);

			//if (this.RefreshContextOnEveryBindingObjectPropertyValueChange)
			//{
				//this.OnRefreshContext(changePropertyValueBindingObjectRequesterEventArgs);
			//}
        }

        private void RaiseBindingObjectRelationForeignObjectSet(BindingObjectRelationForeignObjectSetEventArgs bindingObjectRelationForeignObjectSetEventArgs)
        {
            this.BindingObjectRelationForeignObjectSet?.Invoke(this, bindingObjectRelationForeignObjectSetEventArgs);
            this.OnBindingObjectForeignObjectSet(bindingObjectRelationForeignObjectSetEventArgs);
        }

        private void RaiseParentGraphElementChange(OldParentGraphElemenChangeContainertContextRequesterEventArgs changeParentGraphElementRequesterEventArgs)
        {
            this.BindingObjectParentGraphElementChange?.Invoke(this, changeParentGraphElementRequesterEventArgs);
            this.OnParentGraphElementChange(changeParentGraphElementRequesterEventArgs);
        }

        private void RaiseOnValidation(GraphValidationResultEventArgs validationResultBindingObjectEventArgs)
        {
            this.BindingObjectOnValidation?.Invoke(this, validationResultBindingObjectEventArgs);
            this.OnBindingObjectValidation(validationResultBindingObjectEventArgs);
        }

        private void RaiseStoreData(BindingObjectEventArgs bindingObjectEventArgs)
        {
            this.BindingObjectPushData?.Invoke(this, bindingObjectEventArgs);
            this.OnBindingObjectStoreData();
        }

        private void RaiseRefreshContext(ChangePropertyValueBindingObjectEventArgs changePropertyValueBindingObjectRequesterEventArgs)
        {
            this.BindingObjectRefreshContext?.Invoke(this, changePropertyValueBindingObjectRequesterEventArgs);
            this.OnRefreshContext(changePropertyValueBindingObjectRequesterEventArgs);
        }
		
		private void RaiseReloadData(ChangePropertyValueBindingObjectEventArgs changePropertyValueBindingObjectRequesterEventArgs)
		{
			this.BindingObjectReloadData?.Invoke(this, changePropertyValueBindingObjectRequesterEventArgs);
			this.OnReloadData(changePropertyValueBindingObjectRequesterEventArgs);
		}
		
		#endregion |   Private Raise Event Methods   |

        #region |   Private Methods   |

        private void InitializeEditors()
        {
            this.OnInitializeEditors();
			this.isEditorsInitialized = true;
        }
        
        //private void bindingEditControl_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
        //    if (this.bloadingIsInProgress && this.BindingObject != null)
        //    {
        //        EditPanelPolicy editPanelPolicy = this.editPanelPolicyByBindingEditorControl[sender as BaseEdit] as EditPanelPolicy;

        //        if (editPanelPolicy == null)
        //        {
        //            throw new ArgumentException("The control is not registered as binding control " + sender.ToString());
        //        }

        //        object propertyValue = editPanelPolicy.GetPropertyValueFromControlValue != null ? editPanelPolicy.GetPropertyValueFromControlValue(e.NewValue) : e.NewValue;
        //        this.BindingObject.SetPropertyValue(editPanelPolicy.PropertyName, propertyValue, this);
        //    }
        //}

        //private XtraTabPage GetControlTabPabe(Control control)
        //{
        //    XtraTabPage result = null;

        //    if (this.tabControl != null)
        //    {
        //        foreach (XtraTabPage tabPage in this.tabControl.TabPages)
        //        {
        //            if (tabPage.Contains(control))
        //            {
        //                result = tabPage;
        //                break;
        //            }
        //        }
        //    }

        //    return result;
        //}


        #endregion |   Private Methods   |

        #region |   ISimpleEditPanel Interface   |

        void ISimpleEditPanel.InitializeEditors()
        {
            this.InitializeEditors();
        }

        #endregion |   ISimpleEditPanel Interface   |

        #region |   IBindingObjectControl Interface   |

        bool IBindingObjectControl.SaveBindingObject(IBindingSimpleObject bindingObject)
        {
            if (this.ChangableBindingObjectControl is not null)
				return this.ChangableBindingObjectControl.SaveBindingObject(bindingObject);

			return false;
        }

        void IBindingObjectControl.RefreshButtonsEnableProperty()
        {
            this.ChangableBindingObjectControl?.RefreshButtonsEnableProperty();
        }

        //void IBindingObjectControl.RefreshBindingObjectContext(object requester, IBindingObject bindingObject)
        //{
        //    this.ChangableBindingObjectControl.RefreshBindingObjectContext(requester, bindingObject);
        //}

        #endregion |   IBindingObjectControl Interface   |

        #region |   ISimpleEditPanelBinding Interface   |

        void IBindingSimpleEditPanel.SetChangableBindingObjectControl(IChangableBindingObjectControl changableBindingObjectControl)
        {
            this.ChangableBindingObjectControl = changableBindingObjectControl;
        }

		void IBindingSimpleEditPanel.SetBindingObject(SimpleObject? simpleObject)
        {
			this.SetBindingObject(simpleObject);
        }

        void IBindingSimpleEditPanel.PropertyValueIsChanged(ChangePropertyValueBindingObjectEventArgs e)
        {
            this.RaiseBindingObjectPropertyValueChange(e);
        }

        void IBindingSimpleEditPanel.BindingObjectRelationForeignObjectIsSet(BindingObjectRelationForeignObjectSetEventArgs e)
        {
            this.RaiseBindingObjectRelationForeignObjectSet(e);
        }

        void IBindingSimpleEditPanel.ParentGraphElementIsChanged(OldParentGraphElemenChangeContainertContextRequesterEventArgs e)
        {
            this.RaiseParentGraphElementChange(e);
        }

        void IBindingSimpleEditPanel.OnValidation(GraphValidationResultEventArgs e)
        {
            this.RaiseOnValidation(e);
        }

        void IBindingSimpleEditPanel.StoreData(SimpleObjectEventArgs e)
        {
            this.RaiseStoreData(new BindingObjectEventArgs(e.SimpleObject));
        }

        void IBindingSimpleEditPanel.RefreshContext(ChangePropertyValueBindingObjectEventArgs e)
        {
            this.RaiseRefreshContext(e);
        }

		void IBindingSimpleEditPanel.ReloadData(ChangePropertyValueBindingObjectEventArgs e)
		{
			this.RaiseReloadData(e);
		}

		#endregion |   ISimpleEditPanelBinding Interface   |

		#region |   IEditPanelBinding Interface   |

		void IBindingControl.SetBindingObject(object? bindingObject)
		{
			if (bindingObject == null || !(bindingObject is SimpleObject))
				this.SetBindingObject(null);
			else if (bindingObject is SimpleObject)
				this.SetBindingObject(bindingObject as SimpleObject);
		}

		void IBindingControl.RefreshBindingObject(object? requester)
		{
			this.RaiseRefreshContext(new ChangePropertyValueBindingObjectEventArgs(this.BindingObject, propertyModel: null, value: null, oldValue: null, isChanged: false, this.BindingObject?.ChangeContainer, this.BindingObject?.Context ?? ObjectActionContext.Unspecified, requester));
		}

		#endregion |   IEditPanelBinding Interface   |
	}

	#region |   Interfaces   |

	public interface ISimpleEditPanel : IBindingSimpleEditPanel
    {
        void InitializeEditors();
    }
    
    public interface IBindingSimpleEditPanel : IBindingControl
	{
        void SetChangableBindingObjectControl(IChangableBindingObjectControl changableBindingObjectControl);
        void SetBindingObject(SimpleObject? bindingObject);
        void PropertyValueIsChanged(ChangePropertyValueBindingObjectEventArgs e);
        void BindingObjectRelationForeignObjectIsSet(BindingObjectRelationForeignObjectSetEventArgs e);
        void ParentGraphElementIsChanged(OldParentGraphElemenChangeContainertContextRequesterEventArgs e);
        void OnValidation(GraphValidationResultEventArgs e);
        void StoreData(SimpleObjectEventArgs e);
        void RefreshContext(ChangePropertyValueBindingObjectEventArgs e);
		void ReloadData(ChangePropertyValueBindingObjectEventArgs e);
    }

	public interface IBindingControl
	{
		void SetBindingObject(object? bindingObject);
		void RefreshBindingObject(object? requester);
	}

	#endregion |   Interfaces   |

	#region |   Helper Classes   |

	public static class EditControlHelper
    {
		public static TKey? GetPropertyValueFromControlValue<TKey, TValue>(object controlValue, IDictionary<TKey?, TValue> modelDictionary, Func<TValue, string> getName)
		{
			return GetPropertyValueFromControlValue(controlValue, modelDictionary, getName, default);
		}
		
		public static TKey? GetPropertyValueFromControlValue<TKey, TValue>(object controlValue, IDictionary<TKey?, TValue> modelDictionary, Func<TValue, string> getName, TKey defaultValue)
        {
            var dictionaryEntity = modelDictionary.FirstOrDefault(model => getName(model.Value) == Conversion.TryChangeType<string>(controlValue));
            TKey? result = dictionaryEntity.Value != null ? dictionaryEntity.Key 
														  : Conversion.TryChangeType<TKey?>(controlValue, defaultValue);

            return result;
        }

        public static string GetControlValueFromPropertyValue<TKey, TValue>(object? propertyValue, IDictionary<TKey?, TValue> modelDictionary, Func<TValue, string> getName)
        {
			TKey customPropertyValue = Conversion.TryChangeType<TKey>(propertyValue);
			KeyValuePair<TKey?, TValue>? dictionaryEntity = modelDictionary.FirstOrDefault(model => object.Equals(model.Key, customPropertyValue));
			object? result;

			result = propertyValue;

			if (dictionaryEntity != null && dictionaryEntity.Value.Value != null)
			{
				TValue value = dictionaryEntity.Value.Value;

				result = getName(value);
			}
			else
			{
				result = Conversion.TryChangeType<string>(propertyValue);
			
				if (result is TValue value)
					result = getName(value);
			}

			//if (customPropertyValue is null) // May be the key is nullable
			//{
			//	try
			//	{
			//		dictionaryEntity = modelDictionary.FirstOrDefault(model => Equals(model.Key, customPropertyValue));
			//		result = "Any"; // (string)dictionaryEntity.Value.Value;
			//	}
			//	catch
			//	{
			//		result = Conversion.TryChangeType<string>(propertyValue);
			//	}
			//}
			//else 
			//{
			//try
			//{
			//		dictionaryEntity = modelDictionary.FirstOrDefault(model => Equals(model.Key, customPropertyValue));
					
					
			//		result = "Any"; // (string)dictionaryEntity.Value.Value;
			//	}
			//	catch
			//	{
			//		result = Conversion.TryChangeType<string>(propertyValue);
			//	}

			//	result = getName(dictionaryEntity.Value);
			//}
			//else
			//{
			//	result = Conversion.TryChangeType<string>(propertyValue);
			//}

			////var dictionaryEntity = modelDictionary.FirstOrDefault(model => model.Key is not null && model.Key.Equals(customPropertyValue));
			////string result = dictionaryEntity.Value != null ? getName(dictionaryEntity.Value) : Conversion.TryChangeType<string>(propertyValue);

            return result?.ToString() ?? "Null";
        }
    }

    #endregion |   Helper Classes   |
}
