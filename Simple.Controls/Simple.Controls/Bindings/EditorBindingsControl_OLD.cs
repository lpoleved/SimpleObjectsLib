//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.ComponentModel;
//using System.Windows.Forms;
//using DevExpress.XtraEditors;
//using DevExpress.XtraTab;
//using DevExpress.XtraEditors.Repository;
//using DevExpress.XtraEditors.Controls;
//using DevExpress.XtraEditors.DXErrorProvider;
//using Simple.Reflection;
//using Simple.Modelling;
//using Simple.Objects;
//using Simple.Objects;

//namespace Simple.Controls
//{
//    public class EditorBindingsControl_OLD : Component, IComponent, IDisposable
//    {
//        #region |   Private Members   |

//        private IChangableBindingObjectControl changableBindingObjectControl = null;
//        private Hashtable editPanelPolicyListByPropertyName = new Hashtable();
//        private Hashtable editPanelPolicyByBindingEditorControl = new Hashtable();
//        private int bind = 1; // < 1 - binding is stopped

//        #endregion |   Private Members   |

//        #region |   Public Properties   |

//        [Category("General"), DefaultValue(null)]
//        public XtraTabControl TabControl { get; set; }

//        [Category("General"), DefaultValue(null)]
//        public IChangableBindingObjectControl ChangableBindingObjectControl
//        {
//            get { return this.changableBindingObjectControl; }
//            set
//            {
//                if (this.changableBindingObjectControl != null)
//                {
//                    this.changableBindingObjectControl.BindingObjectChange -= new BindingObjectEventHandler(changableBindingObjectControl_BindingObjectChange);
//                    this.changableBindingObjectControl.BindingObjectPropertyValueChange -= new ChangePropertyValueRequesterBindingObjectEventHandler(changableBindingObjectControl_BindingObjectPropertyValueChange);
//                    this.changableBindingObjectControl.BindingObjectOnValidation -= new ValidationResultBindingObjectEventHandler(changableBindingObjectControl_OnValidation);
//                    this.changableBindingObjectControl.BindingObjectStoreData -= new BindingObjectEventHandler(changableBindingObjectControl_StoreData);
//                }

//                this.changableBindingObjectControl = value;

//                if (this.changableBindingObjectControl != null)
//                {
//                    this.changableBindingObjectControl.BindingObjectChange += new BindingObjectEventHandler(changableBindingObjectControl_BindingObjectChange);
//                    this.changableBindingObjectControl.BindingObjectPropertyValueChange += new ChangePropertyValueRequesterBindingObjectEventHandler(changableBindingObjectControl_BindingObjectPropertyValueChange);
//                    this.changableBindingObjectControl.BindingObjectOnValidation += new ValidationResultBindingObjectEventHandler(changableBindingObjectControl_OnValidation);
//                    this.changableBindingObjectControl.BindingObjectStoreData += new BindingObjectEventHandler(changableBindingObjectControl_StoreData);
//                }
//            }
//        }

//        [Category("Simple"), DefaultValue(null), Browsable(false)]
//        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//        public IBindingObject BindingObject { get; private set; }

//        [Category("General"), DefaultValue(null)]
//        public DXErrorProvider ErrorProvider { get; set; }

//        #endregion |   Public Properties   |

//        #region |   Public Methods   |

//        public void StartBinding()
//        {
//            this.bind++;
//        }

//        public void StopBinding()
//        {
//            this.bind--;
//        }

//        public void Register(BaseEdit bindingEditorControl, IPropertyModel propertyModel)
//        {
//            this.Register(bindingEditorControl, propertyModel, null, null);
//        }

//        public void Register(CheckEdit checkBox, IPropertyModel propertyModel, object checkedPropertyValue, object uncheckedPropertyValue)
//        {
//            this.Register(checkBox, propertyModel,
//                controlValue => Converter.TryChangeType<bool>(controlValue) ? checkedPropertyValue : uncheckedPropertyValue,
//                propertyValue => propertyValue.Equals(checkedPropertyValue));
//        }

//        public void Register(ComboBoxEdit comboBox, IPropertyModel propertyModel)
//        {
//            this.Register(comboBox, propertyModel, new Dictionary<int, IModel>());
//        }

//        public void Register(ComboBoxEdit comboBox, IPropertyModel propertyModel, IDictionary<int, IModel> modelDictionary)
//        {
//            this.Register(comboBox, propertyModel, modelDictionary, model => model.Name);
//        }

//        public void Register(ComboBoxEdit comboBox, IPropertyModel propertyModel, IDictionary<int, IModel> modelDictionary, Func<IModel, string> getName)
//        {
//            this.Register(comboBox, propertyModel, modelDictionary,
//                                 controlValue => EditControlHelper.GetPropertyValueFromControlValue<int, IModel>(controlValue, modelDictionary, getName),
//                                 propertyValue => EditControlHelper.GetControlValueFromPropertyValue<int, IModel>(propertyValue, modelDictionary, getName),
//                                 getName);
//        }

//        public void Register(ComboBoxEdit comboBox, IPropertyModel propertyModel, Func<object, object> getPropertyValueFromControlValue, Func<object, object> getControlValueFromPropertyValue)
//        {
//            this.Register(comboBox, propertyModel, new Dictionary<int, IModel>(), getPropertyValueFromControlValue, getControlValueFromPropertyValue, model => model.Name);
//        }

//        public void Register(ComboBoxEdit comboBox, IPropertyModel propertyModel, IDictionary<object, object> dictionary)
//        {
//            Dictionary<object, object> keysByValue = new Dictionary<object, object>();

//            comboBox.Properties.Items.BeginUpdate();

//            try
//            {
//                foreach (var dictionaryItem in dictionary)
//                {
//                    keysByValue.Add(dictionaryItem.Value, dictionaryItem.Key);
//                    comboBox.Properties.Items.Add(dictionaryItem.Value);
//                }
//            }
//            finally
//            {
//                comboBox.Properties.Items.EndUpdate();
//            }

//            this.Register(comboBox as BaseEdit, propertyModel, controlValue => keysByValue[controlValue], propertyValue => dictionary[propertyValue]);
//        }

//        public void Register(RepositoryItemComboBox comboBox, IPropertyModel propertyModel, IDictionary<object, object> dictionary)
//        {
//            Dictionary<object, object> keysByValue = new Dictionary<object, object>();

//            comboBox.Items.BeginUpdate();

//            try
//            {
//                foreach (var dictionaryItem in dictionary)
//                {
//                    keysByValue.Add(dictionaryItem.Value, dictionaryItem.Key);
//                    comboBox.Items.Add(dictionaryItem.Value);
//                }
//            }
//            finally
//            {
//                comboBox.Items.EndUpdate();
//            }

//            comboBox.EditValueChanging += new ChangingEventHandler(bindingEditControl_EditValueChanging);
//            this.Register(comboBox as Component, propertyModel, controlValue => keysByValue[controlValue], propertyValue => dictionary[propertyValue]);
//        }

//        public void Register(ComboBoxEdit comboBox, IPropertyModel propertyModel, IDictionary<int, IModel> modelDictionary, Func<object, object> getPropertyValueFromControlValue, Func<object, object> getControlValueFromPropertyValue, Func<IModel, string> getName)
//        {
//            comboBox.Properties.Items.BeginUpdate();

//            try
//            {
//                foreach (IModel model in modelDictionary.Values)
//                {
//                    comboBox.Properties.Items.Add(getName(model));
//                }
//            }
//            finally
//            {
//                comboBox.Properties.Items.EndUpdate();
//            }

//            this.Register(comboBox as BaseEdit, propertyModel, getPropertyValueFromControlValue, getControlValueFromPropertyValue);
//        }

//        public void Register(BaseEdit bindingEditorControl, IPropertyModel propertyModel, Func<object, object> getPropertyValueFromControlValue, Func<object, object> getControlValueFromPropertyValue)
//        {
//            bindingEditorControl.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(bindingEditControl_EditValueChanging);
//            this.Register(bindingEditorControl as Component, propertyModel, getPropertyValueFromControlValue, getControlValueFromPropertyValue);
//        }

//        #endregion |   Public Methods   |

//        #region |   Protected Methods   |

//        protected override void Dispose(bool disposing)
//        {
//            this.editPanelPolicyListByPropertyName = null;
//            this.editPanelPolicyByBindingEditorControl = null;
//            this.ChangableBindingObjectControl = null;
//            this.BindingObject = null;
            
//            base.Dispose(disposing);
//        }

//        #endregion |   Protected Methods   |

//        #region |   Private Methods   |

//        private void changableBindingObjectControl_BindingObjectChange(object sender, BindingObjectEventArgs e)
//        {
//            if (this.BindingObject != e.BindingObject)
//            {
//                this.BindingObject = e.BindingObject;
//                this.ReloadData();
//            }
//        }

//        private void changableBindingObjectControl_BindingObjectPropertyValueChange(object sender, ChangePropertyValueBindingObjectRequesterEventArgs e)
//        {
//            if (e.Requester != this && e.BindingObject == this.BindingObject)
//            {
//                this.ReloadData(e.PropertyName, e.Value);
//            }
//        }

//        private void changableBindingObjectControl_OnValidation(object sender, ValidationResultBindingObjectEventArgs e)
//        {
//            if (e.BindingObject == this.BindingObject)
//            {
//                if (!e.ValidationResult.Passed)
//                {
//                    List<EditPanelPolicy> editPanelPolicyList = this.editPanelPolicyListByPropertyName[e.ValidationResult.ErrorPropertyName] as List<EditPanelPolicy>;

//                    if (editPanelPolicyList != null)
//                    {
//                        foreach (EditPanelPolicy editPanelPolicy in editPanelPolicyList)
//                        {
//                            if (editPanelPolicy.BindingEditorControl is Control)
//                            {
//                                this.ErrorProvider.SetError(editPanelPolicy.BindingEditorControl as Control, e.ValidationResult.Message);

//                                if (editPanelPolicy.TabPage != null)
//                                {
//                                    editPanelPolicy.TabPage.Image = global::Simple.Controls.Properties.Resources.Error;
//                                    editPanelPolicy.TabPage.TooltipIconType = DevExpress.Utils.ToolTipIconType.Error;
//                                    editPanelPolicy.TabPage.Tooltip = e.ValidationResult.Message;

//                                    editPanelPolicy.TabPage.TabControl.SelectedTabPage = editPanelPolicy.TabPage;
//                                    // TODO: Remember editPanelPolicy.TabPage.Image
//                                }
//                            }
//                        }
//                    }
//                }
//                else
//                {
//                    foreach (EditPanelPolicy editPanelPolicy in this.editPanelPolicyByBindingEditorControl.Values)
//                    {
//                        if (editPanelPolicy.BindingEditorControl is Control)
//                        {
//                            this.ErrorProvider.SetError(editPanelPolicy.BindingEditorControl as Control, null);

//                            if (editPanelPolicy.TabPage != null)
//                            {
//                                editPanelPolicy.TabPage.Image = null;
//                                editPanelPolicy.TabPage.TooltipIconType = DevExpress.Utils.ToolTipIconType.None;
//                                editPanelPolicy.TabPage.Tooltip = "";
//                                // TODO: restor editPanelPolicy.TabPage.Image
//                            }
//                        }
//                    }
//                }
//            }
//        }

//        private void changableBindingObjectControl_StoreData(object sender, BindingObjectEventArgs e)
//        {
//            if (e.BindingObject == this.BindingObject)
//            {
//                foreach (BaseEdit bindingEditorControl in this.editPanelPolicyByBindingEditorControl.Keys)
//                {
//                    bindingEditorControl.DoValidate();
//                }
//            }
//        }
        
//        private XtraTabPage GetControlTabPabe(Control control)
//        {
//            XtraTabPage result = null;

//            if (this.TabControl != null)
//            {
//                foreach (XtraTabPage tabPage in this.TabControl.TabPages)
//                {
//                    if (tabPage.Contains(control))
//                    {
//                        result = tabPage;
//                        break;
//                    }
//                }
//            }

//            return result;
//        }

//        private void ReloadData()
//        {
//            if (this.BindingObject != null)
//            {
//                ISimpleObjectModel objectModel = this.BindingObject.GetObjectModel();

//                foreach (IPropertyModel propertyModel in objectModel.Properties)
//                {
//                    object propertyValue = this.BindingObject[propertyModel.Name];
//                    this.ReloadData(propertyModel.Name, propertyValue);
//                }
//            }
//        }

//        private void ReloadData(string propertyName, object propertyValue)
//        {
//            List<EditPanelPolicy> editPanelPolicyList = this.editPanelPolicyListByPropertyName[propertyName] as List<EditPanelPolicy>;

//            if (editPanelPolicyList != null)
//            {
//                this.StopBinding();

//                foreach (EditPanelPolicy editPanelPolicy in editPanelPolicyList)
//                {
//                    object controlValue = editPanelPolicy.GetControlValueFromPropertyValue != null ? editPanelPolicy.GetControlValueFromPropertyValue(propertyValue) : propertyValue;
//                    string controlValueText = controlValue != null ? controlValue.ToString() : String.Empty;

//                    if (editPanelPolicy.BindingEditorControl is CheckEdit)
//                    {
//                        bool checkedValue = Converter.TryChangeType<bool>(controlValue);
//                        (editPanelPolicy.BindingEditorControl as CheckEdit).Checked = checkedValue;
//                    }
//                    else if (editPanelPolicy.BindingEditorControl is Control)
//                    {
//                        (editPanelPolicy.BindingEditorControl as Control).Text = controlValueText;
//                    }
//                    //else if (editPanelPolicy.BindingEditorControl is RepositoryItemComboBox)
//                    //{
//                    //    (editPanelPolicy.BindingEditorControl as RepositoryItemComboBox).Properties.Sele
//                    //}
//                }

//                this.StartBinding();
//            }
//        }

//        private void Register(Component bindingEditorControl, IPropertyModel propertyModel, Func<object, object> getPropertyValueFromControlValue, Func<object, object> getControlValueFromPropertyValue)
//        {
//            List<EditPanelPolicy> editPanelPolicyList = null;
//            XtraTabPage tabPage = bindingEditorControl is Control ? this.GetControlTabPabe(bindingEditorControl as Control) : null;

//            EditPanelPolicy editPanelPolicy = new EditPanelPolicy()
//            {
//                BindingEditorControl = bindingEditorControl,
//                PropertyName = propertyModel.Name,
//                TabPage = tabPage,
//                GetPropertyValueFromControlValue = getPropertyValueFromControlValue,
//                GetControlValueFromPropertyValue = getControlValueFromPropertyValue
//            };

//            editPanelPolicyList = this.editPanelPolicyListByPropertyName[propertyModel.Name] as List<EditPanelPolicy>;

//            if (editPanelPolicyList == null)
//            {
//                editPanelPolicyList = new List<EditPanelPolicy>();
//                this.editPanelPolicyListByPropertyName.Add(propertyModel.Name, editPanelPolicyList);
//            }

//            editPanelPolicyList.Add(editPanelPolicy);
//            this.editPanelPolicyByBindingEditorControl.Add(bindingEditorControl, editPanelPolicy);

//            if (this.ErrorProvider != null && bindingEditorControl is Control)
//            {
//                this.ErrorProvider.SetError(bindingEditorControl as Control, null);
//            }
//        }

//        private void bindingEditControl_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
//        {
//            if (this.bind > 0 && this.BindingObject != null)
//            {
//                EditPanelPolicy editPanelPolicy = this.editPanelPolicyByBindingEditorControl[sender as BaseEdit] as EditPanelPolicy;

//                if (editPanelPolicy == null)
//                {
//                    throw new ArgumentException("The control is not registered as binding control " + sender.ToString());
//                }

//                object propertyValue = editPanelPolicy.GetPropertyValueFromControlValue != null ? editPanelPolicy.GetPropertyValueFromControlValue(e.NewValue) : e.NewValue;
//                this.BindingObject.SetPropertyValue(editPanelPolicy.PropertyName, propertyValue, this);
//            }
//        }

//        #endregion |   Private Methods   |
//    }
//}
