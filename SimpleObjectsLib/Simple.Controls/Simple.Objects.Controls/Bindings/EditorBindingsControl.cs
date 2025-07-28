using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraTab;
using Simple.Controls;
using Simple.Collections;
using Simple.Modeling;

namespace Simple.Objects.Controls
{
    public class EditorBindingsControl : Component, IComponent, IDisposable
    {
        #region |   Private Members   |

        private IChangableBindingObjectControl? changableBindingObjectControl = null;
        private EditorBindingPolicyDictionary editorBindingPolicyDictionary = new EditorBindingPolicyDictionary();
		private Dictionary<int, IPropertyModel> oneToOneRelationPropertyModelsByRelationKey = new Dictionary<int, IPropertyModel>();
		private Dictionary<int, IPropertyModel> oneToManyRelationPropertyModelsByRelationKey = new Dictionary<int, IPropertyModel>();
		private SimpleObjectManager? objectManager = null;

		//private Dictionary<string, List<EditorBindingPolicy>> editPanelPolicyListByObjectPropertyName = new Dictionary<string, List<EditorBindingPolicy>>();
		//private Dictionary<int, EditorBindingPolicy> editPanelPoliciesByRepositoryItemKey = new Dictionary<string, EditorBindingPolicy>();  // Key is stored in RepositoryItem Name property! - Editor's use copy of the RepositoryItem, 
		//private Dictionary<int, RepositoryItem> repositoryItemsByRepositoryItemKey = new Dictionary<string, RepositoryItem>();   // so storing instance and metching by editors evnet given RepositoryItem is not usefull.
		//private Dictionary<int, Component> componentsByRepositoryItemKey = new Dictionary<int, Component>();
		//private UniqueKeyGenerator repositoryItemKeyGenerator = new UniqueKeyGenerator();
		private int binding = 1; // < 1 - binding is stopped

		#endregion |   Private Members   |

        #region |   Public Properties   |

        [Category("General"), DefaultValue(null)]
        public XtraTabControl? TabControl { get; set; }

        [Category("General"), DefaultValue(null)]
        public IChangableBindingObjectControl? ChangableBindingObjectControl
        {
            get { return this.changableBindingObjectControl; }
            set
            {
                if (this.changableBindingObjectControl != null)
                {
                    this.changableBindingObjectControl.BindingObjectChange -= new BindingObjectEventHandler(changableBindingObjectControl_BindingObjectChange);
                    this.changableBindingObjectControl.BindingObjectPropertyValueChange -= new ChangePropertyValueBindingObjectRequesterEventHandler(changableBindingObjectControl_BindingObjectPropertyValueChange);
                    this.changableBindingObjectControl.BindingObjectOnValidation -= new GraphValidationResultEventHandler(changableBindingObjectControl_OnValidation);
                    this.changableBindingObjectControl.BindingObjectPushData -= new BindingObjectEventHandler(changableBindingObjectControl_StoreData);
					this.changableBindingObjectControl.BindingObjectRelationForeignObjectSet -= changableBindingObjectControl_BindingObjectRelationForeignObjectSet;
				}

                this.changableBindingObjectControl = value;

                if (this.changableBindingObjectControl != null)
                {
                    this.changableBindingObjectControl.BindingObjectChange += new BindingObjectEventHandler(changableBindingObjectControl_BindingObjectChange);
                    this.changableBindingObjectControl.BindingObjectPropertyValueChange += new ChangePropertyValueBindingObjectRequesterEventHandler(changableBindingObjectControl_BindingObjectPropertyValueChange);
                    this.changableBindingObjectControl.BindingObjectOnValidation += new GraphValidationResultEventHandler(changableBindingObjectControl_OnValidation);
                    this.changableBindingObjectControl.BindingObjectPushData += new BindingObjectEventHandler(changableBindingObjectControl_StoreData);
					this.changableBindingObjectControl.BindingObjectRelationForeignObjectSet += changableBindingObjectControl_BindingObjectRelationForeignObjectSet;
                }
            }
        }

        [Category("Simple"), DefaultValue(null), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IBindingSimpleObject? BindingObject { get; private set; }

		[Category("General"), Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SimpleObjectManager? ObjectManager
		{
			get { return this.objectManager; }
			set
			{
				if (this.objectManager != null)
					this.objectManager.DefaultChangeContainer.RequireCommitChange -= this.ObjectManager_RequireCommitChange;

				this.objectManager = value;

				if (this.objectManager != null)
					this.objectManager.DefaultChangeContainer.RequireCommitChange += this.ObjectManager_RequireCommitChange;
			}
		}

		///// <summary>
		///// Gets the related object to bind the editors. If null, binding object is determined by the changable binding object control. 
		///// Use this method to specify binding customization to other object then changable binding object.
		///// </summary>
		//[Category("Simple"), DefaultValue(null), Browsable(false)]
		//[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		////public Func<IBindingSimpleObject, IBindingSimpleObject> GetBindingObject { get; set; }

		[Category("General"), DefaultValue(null)]
        public DXErrorProvider? ErrorProvider { get; set; }

        #endregion |   Public Properties   |

        #region |   Public Methods   |

        public void EnableBinding()
        {
            this.binding++;
        }

        public void DisableBinding()
        {
            this.binding--;
        }

		public void RegisterCheckEdit(RepositoryItemCheckEdit repositoryItemCheckEdit, Type objectType, IPropertyModel propertyModel)
        {
			this.RegisterCheckEdit(repositoryItemCheckEdit, objectType, propertyModel, true, false);
        }

		public void RegisterCheckEdit(RepositoryItemCheckEdit repositoryItemCheckEdit, Type objectType, IPropertyModel propertyModel, object checkedPropertyValue, object uncheckedPropertyValue)
        {
			this.RegisterCheckEdit(repositoryItemCheckEdit, objectType, propertyModel, checkedPropertyValue, uncheckedPropertyValue, valueToSetToTheControl => (repositoryItemCheckEdit.OwnerEdit as CheckEdit).Checked = (bool)valueToSetToTheControl);
        }

		public void RegisterCheckEdit(RepositoryItemCheckEdit repositoryItemCheckEdit, Type objectType, IPropertyModel propertyModel, Action<object> setControlValue)
        {
			this.RegisterCheckEdit(repositoryItemCheckEdit, objectType, propertyModel, true, false, setControlValue);
        }

		public void RegisterCheckEdit(RepositoryItemCheckEdit repositoryItemCheckEdit, Type objectType, IPropertyModel propertyModel, object checkedPropertyValue, object uncheckedPropertyValue, Action<object> setControlValue)
        {
			this.Register(repositoryItemCheckEdit, objectType, propertyModel,
						  controlValue => Conversion.TryChangeType<bool>(controlValue) ? checkedPropertyValue : uncheckedPropertyValue,
						  propertyValue => Equals(propertyValue, checkedPropertyValue),
						  setControlValue);
        }

		public void RegisterComboBox(RepositoryItemComboBox repositoryItemComboBox, Type objectType, IPropertyModel propertyModel, IDictionary keyValueDictionary)
        {
			this.RegisterComboBox(repositoryItemComboBox, objectType, propertyModel, keyValueDictionary, null, null);
        }

		public void RegisterComboBox(RepositoryItemComboBox repositoryItemComboBox, Type objectType, IPropertyModel propertyModel, IDictionary keyValueDictionary, 
									 Func<object, object>? getName)
        {
			this.RegisterComboBox(repositoryItemComboBox, objectType, propertyModel, keyValueDictionary, getName, null);
        }

		public void RegisterComboBox(RepositoryItemComboBox repositoryItemComboBox, Type objectType, IPropertyModel propertyModel, IDictionary keyValueDictionary, 
									 Func<object, object>? getName, Action<object>? setControlValue)
		{
			this.RegisterComboBox(repositoryItemComboBox, objectType, propertyModel, keyValueDictionary, getName, setControlValue, null);
		}

		public void RegisterComboBox(RepositoryItemComboBox repositoryItemComboBox, Type objectType, IPropertyModel propertyModel, IDictionary keyValueDictionary, 
									 Func<object, object>? getName, Action<object>? setControlValue, Func<object?, object>? getImageName)
		{
			this.RegisterComboBox(repositoryItemComboBox, objectType, EditorBindingType.ObjectProperty, propertyModel, -1, keyValueDictionary, getName, setControlValue, getImageName);
		}

		public void RegisterComboBox(RepositoryItemComboBox repositoryItemComboBox, Type objectType, EditorBindingType bindingType, IPropertyModel propertyModel, int relationKey, IDictionary keyValueDictionary, 
									 Func<object, object>? getName, Action<object>? setControlValue, Func<object, object?>? getImageName)
        {
			NullableDictionary<object?, object?> keysByValue = new NullableDictionary<object?, object?>();
			NullableDictionary<object?, object?> valuesByKey = new NullableDictionary<object?, object?>();

            repositoryItemComboBox.Items.BeginUpdate();

            try
            {
				if (bindingType == EditorBindingType.OneToManyRelation)
				{
					IOneToManyRelationModel? oneToManyRelationModel = this.ObjectManager?.ModelDiscovery.GetOneToManyRelationModel(relationKey);
					//RelationPolicyModelBase.Instance.OneToManyRelations.TryGetValue(relationKey, out oneToManyRelationModel);

					if (oneToManyRelationModel != null && oneToManyRelationModel.CanBeNull)
					{
						valuesByKey.Add(String.Empty, null);
						keysByValue.Add(String.Empty, null);

                        if (repositoryItemComboBox is RepositoryItemImageComboBox repositoryItemImageComboBox)
                        {
                            repositoryItemImageComboBox.Items.Add(new ImageComboBoxItem(String.Empty, -1));
                        }
                        else
                        {
                            repositoryItemComboBox.Items.Add(String.Empty);
                        }
					}
				}
				else if (bindingType == EditorBindingType.OneToOneRelationPrimaryObject || bindingType == EditorBindingType.OneToOneRelationForeignObject)
				{
					IOneToOneRelationModel? oneToOneRelationModel = this.ObjectManager?.ModelDiscovery.GetOneToOneRelationModel(relationKey);
					//RelationPolicyModelBase.Instance.OneToOneRelations.TryGetValue(relationKey, out oneToOneRelationModel);

					if (oneToOneRelationModel != null && oneToOneRelationModel.CanBeNull)
					{
						valuesByKey.Add(String.Empty, null);
						keysByValue.Add(String.Empty, null);

                        if (repositoryItemComboBox is RepositoryItemImageComboBox repositoryItemImageComboBox)
                        {
                            repositoryItemImageComboBox.Items.Add(new ImageComboBoxItem(String.Empty, -1));
                        }
                        else
                        {
                            repositoryItemComboBox.Items.Add(String.Empty);
                        }
					}
				}

				foreach (object key in keyValueDictionary.Keys)
                {
					object? value = keyValueDictionary[key];																				 // if declared property type is enum and key is not, convert key value to enum 
					object? objectKey = (key != null && bindingType == EditorBindingType.ObjectProperty && propertyModel.PropertyType.IsEnum && !key.GetType().IsEnum) ? Conversion.TryChangeType(key, propertyModel.PropertyType) 
																																									   : key;
					object objectValue = ((getName != null && value != null) ? getName(value) : value) ?? String.Empty;
                    bool isItemAdded = false;

					if (bindingType == EditorBindingType.ObjectProperty && propertyModel.PropertyType.IsEnum && !key.GetType().IsEnum)
						objectKey = Conversion.TryChangeType(key, propertyModel.PropertyType);

					valuesByKey.Add(objectKey, objectValue);
					keysByValue.Add(objectValue, objectKey);
						
					isItemAdded = true;

					if (repositoryItemComboBox is RepositoryItemImageComboBox repositoryItemImageComboBox)
					{
						repositoryItemImageComboBox.Items.Add(new ImageComboBoxItem(objectValue, -1));

						if (key != null && getImageName is not null && repositoryItemImageComboBox.SmallImages is not null && repositoryItemImageComboBox.SmallImages is ImageList imageList)
						{
							string? imageName = getImageName(key)?.ToString();

							if (!imageName.IsNullOrEmpty())
							{
								int imageIndex = imageList.Images.IndexOfKey(imageName!);
								
								repositoryItemImageComboBox.Items.Add(new ImageComboBoxItem(objectValue, imageIndex));
								isItemAdded = true;
							}

						}
					}
					else
					{
						repositoryItemComboBox.Items.Add(objectValue);
					}

					if (!isItemAdded)
						repositoryItemComboBox.Items.Add(objectValue);
                }
            }
            finally
            {
                repositoryItemComboBox.Items.EndUpdate();
            }

			this.Register(repositoryItemComboBox, objectType, bindingType, propertyModel, relationKey,
                            controlValue => 
                            {
                                object? propertyValue = null;

                                //if (controlValue != null)
                                //{
                                    if (!keysByValue.TryGetValue(controlValue, out propertyValue))
                                        propertyValue = null;
                                //}
                                //else
                                //{
                                //    propertyValue = null;
                                //}

                                return propertyValue;
                            }, 
                            propertyValue => 
                            {
                                object? controlValue = null;

                                //if (propertyValue != null)
                                //{
                                    if (!valuesByKey.TryGetValue(propertyValue, out controlValue))
                                        controlValue = null;
                                //}
                                //else
                                //{
                                //    controlValue = null;
                                //}

                                return controlValue;
                            },
                            setControlValue);
        }

		public void Register(RepositoryItem repositoryItem, Type objectType, IPropertyModel propertyModel)
        {
			this.Register(repositoryItem, objectType, propertyModel, getPropertyValueFromControlValue: default, getControlValueFromPropertyValue: default);
        }

		public void Register(Control editorControl, Type objectType, IPropertyModel propertyModel)
        {
			this.Register(editorControl, objectType, propertyModel, getPropertyValueFromControlValue: default, getControlValueFromPropertyValue: default, setControlValue: default);
        }

		public void Register(RepositoryItem repositoryItem, Type objectType, IPropertyModel propertyModel, Func<object, object?>? getPropertyValueFromControlValue, Func<object?, object>? getControlValueFromPropertyValue)
        {
            this.Register(repositoryItem, objectType, propertyModel, getPropertyValueFromControlValue, getControlValueFromPropertyValue, valueToSetToTheControl => repositoryItem.OwnerEdit.EditValue = valueToSetToTheControl);
        }

        public void Register(Component editorComponent, Type objectType, IPropertyModel propertyModel, Func<object, object?>? getPropertyValueFromControlValue, Func<object?, object>? getControlValueFromPropertyValue, Action<object>? setControlValue)
        {
			this.Register(editorComponent, objectType, EditorBindingType.ObjectProperty, propertyModel, -1, getPropertyValueFromControlValue, getControlValueFromPropertyValue, setControlValue);
        }

		public void Register(Component editorComponent, Type objectType, EditorBindingType bindingType, IPropertyModel propertyModel, int relationKey, Func<object, object?>? getPropertyValueFromControlValue, Func<object?, object>? getControlValueFromPropertyValue, Action<object>? setControlValue)
		{
			EditorBindingPolicy editorBindingPolicy = new EditorBindingPolicy(editorComponent, objectType, bindingType, propertyModel, relationKey, tabPage: null, getPropertyValueFromControlValue, getControlValueFromPropertyValue, setControlValue);
			
			this.Register(editorBindingPolicy);
		}

		public void Register(EditorBindingPolicy editorBindingPolicy)
		{
			if (editorBindingPolicy.TabPage is null && editorBindingPolicy.EditorComponent is not null)
			{
				XtraTabPage? tabPage = default;

				if (editorBindingPolicy.EditorComponent is RepositoryItem)
				{
					tabPage = this.GetControlTabPabe((editorBindingPolicy.EditorComponent as RepositoryItem)?.OwnerEdit);
				}
				else if (editorBindingPolicy.EditorComponent is Control)
				{
					tabPage = this.GetControlTabPabe(editorBindingPolicy.EditorComponent as Control);
				}

				editorBindingPolicy.TabPage = tabPage;
			}

			if (editorBindingPolicy.EditorComponent != null)
				this.editorBindingPolicyDictionary.Set(editorBindingPolicy.EditorComponent, editorBindingPolicy.PropertyModel, editorBindingPolicy);

			if (editorBindingPolicy.BindingType == EditorBindingType.OneToOneRelationPrimaryObject || editorBindingPolicy.BindingType == EditorBindingType.OneToOneRelationForeignObject)
			{
				if (this.oneToOneRelationPropertyModelsByRelationKey.ContainsKey(editorBindingPolicy.RelationKey))
					this.oneToOneRelationPropertyModelsByRelationKey.Remove(editorBindingPolicy.RelationKey);

				this.oneToOneRelationPropertyModelsByRelationKey.Add(editorBindingPolicy.RelationKey, editorBindingPolicy.PropertyModel);
			}
			else if (editorBindingPolicy.BindingType == EditorBindingType.OneToManyRelation)
			{
				if (this.oneToManyRelationPropertyModelsByRelationKey.ContainsKey(editorBindingPolicy.RelationKey))
					this.oneToManyRelationPropertyModelsByRelationKey.Remove(editorBindingPolicy.RelationKey);
				
				this.oneToManyRelationPropertyModelsByRelationKey.Add(editorBindingPolicy.RelationKey, editorBindingPolicy.PropertyModel);
			}

			// Set control's EditValueChange event and set error handle to the ErrorProvider.
			if (editorBindingPolicy.EditorComponent is not null)
			{
				if (editorBindingPolicy.EditorComponent is BaseEdit)
				{
					((BaseEdit)editorBindingPolicy.EditorComponent).EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(EditorBindingsControl_EditValueChanging);
				}
				else if (editorBindingPolicy.EditorComponent is RepositoryItem)
				{
					((RepositoryItem)editorBindingPolicy.EditorComponent).EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(EditorBindingsControl_EditValueChanging);
				}
				else if (editorBindingPolicy.EditorComponent is Control)
				{
					((Control)editorBindingPolicy.EditorComponent).TextChanged += this.EditorBindingsControl_TextChanged;
				}

				this.SetErrorProviderError(editorBindingPolicy.EditorComponent, errorText: null); ;
			}
		}

        public bool TryGetControlValueFromPropertyValue(Type objectType, IPropertyModel propertyModel, object propertyValue, Component editorComponent, out object? controlValue)
        {
            bool result = false;
            controlValue = null;

            if (editorComponent == null)
                return result;

            EditorBindingPolicy? editorBindingPolicy = this.editorBindingPolicyDictionary.GetEditorBindingPolicyByEditorComponent(editorComponent);

			if (editorBindingPolicy != null)
			{
				if (editorBindingPolicy.GetControlValueFromPropertyValue != null && editorBindingPolicy.PropertyModel == propertyModel &&
					(editorBindingPolicy.ObjectType == objectType || editorBindingPolicy.ObjectType.IsSubclassOf(objectType)) || objectType.IsSubclassOf(editorBindingPolicy.ObjectType))
				{
					controlValue = editorBindingPolicy.GetControlValueFromPropertyValue!(propertyValue);
					result = true;
				}
			}

            return result;
        }

        public bool TryGetPropertyValueFromControlValue(object controlValue, Component editorComponent, out object? propertyValue)
        {
            propertyValue = null;

            if (editorComponent == null)
                return false;

            bool result = false;
            EditorBindingPolicy editPanelPolicy = this.editorBindingPolicyDictionary.GetEditorBindingPolicyByEditorComponent(editorComponent);

            if (editPanelPolicy != null && editPanelPolicy.GetPropertyValueFromControlValue != null)
            {
                propertyValue = editPanelPolicy.GetPropertyValueFromControlValue(controlValue);
                result = true;
            }

            return result;
        }

		public bool TrySetPropertyValue(Type objectType, IPropertyModel propertyModel, object propertyValue)
		{
			bool isAnythingSet = false;
			IList<EditorBindingPolicy>? editorBindingPolicyList = this.editorBindingPolicyDictionary.GetEditorBindingPolicyListByObjectPropertyModel(propertyModel);

			if (editorBindingPolicyList != null)
				isAnythingSet = this.ReloadData(editorBindingPolicyList, objectType, propertyValue, requester: default);

			return isAnythingSet;
		}

		public void ReloadData()
		{
			if (this.BindingObject != null)
			{
				ISimpleObjectModel objectModel = this.BindingObject.GetModel();

				foreach (IPropertyModel propertyModel in objectModel.PropertyModels)
					this.ReloadData(propertyModel);
			}
		}

		public void ReloadData(IPropertyModel propertyModel)
		{
			if (this.BindingObject != null)
			{
				IList<EditorBindingPolicy>? editorBindingPolicyList = this.editorBindingPolicyDictionary.GetEditorBindingPolicyListByObjectPropertyModel(propertyModel);

				if (editorBindingPolicyList != null && editorBindingPolicyList.Count > 0)
				{
					object? propertyValue = this.GetPropertyValue(this.BindingObject, propertyModel, editorBindingPolicyList);
					
					this.ReloadData(editorBindingPolicyList, this.BindingObject.GetType(), propertyValue, requester: null);
				}
			}
		}

		public object? GetPropertyValue(IBindingSimpleObject bindingObject, IPropertyModel propertyModel)
		{
			IList<EditorBindingPolicy>? editorBindingPolicyList = this.editorBindingPolicyDictionary.GetEditorBindingPolicyListByObjectPropertyModel(propertyModel);
			
			if (editorBindingPolicyList is not null)
				return this.GetPropertyValue(bindingObject, propertyModel, editorBindingPolicyList);

			return default;
		}

		public object? GetPropertyValue(IBindingSimpleObject bindingObject, IPropertyModel propertyModel, IList<EditorBindingPolicy> editorBindingPolicyList)
		{
			if (editorBindingPolicyList != null)
			{
				foreach (EditorBindingPolicy editorBindingPolicy in editorBindingPolicyList)
				{
					if (bindingObject.GetType() == editorBindingPolicy.ObjectType || bindingObject.GetType().IsSubclassOf(editorBindingPolicy.ObjectType))
					{
						object? propertyValue = null;

						switch (editorBindingPolicy.BindingType)
						{
							case EditorBindingType.ObjectProperty:
								
								propertyValue = bindingObject.GetPropertyValue(propertyModel.PropertyIndex);
								
								break;

							case EditorBindingType.OneToManyRelation:
								
								propertyValue = bindingObject.GetOneToManyPrimaryObject(editorBindingPolicy.RelationKey);
								
								break;

							case EditorBindingType.OneToOneRelationPrimaryObject:
								
								propertyValue = bindingObject.GetOneToOnePrimaryObject(editorBindingPolicy.RelationKey);
								
								break;

							case EditorBindingType.OneToOneRelationForeignObject:
								
								propertyValue = bindingObject.GetOneToOneForeignObject(editorBindingPolicy.RelationKey);
								
								break;

							default:
								
								propertyValue = bindingObject[propertyModel.PropertyIndex];
								
								break;
						}

						return propertyValue;
					}
				}

				return null;
			}
			else
			{
				return bindingObject[propertyModel.PropertyIndex];
			}
		}

		public object? GetNormalizedPropertyValue(IPropertyModel propertyModel, object? propertyValue)
		{
			object? normalizedPropertyValue = Conversion.TryChangeType(propertyValue, propertyModel.PropertyType); //  .PropertyTypeId);

			return normalizedPropertyValue;
		}

		public bool ContainsPropertyBinding(Type objectType, IPropertyModel propertyModel) => this.editorBindingPolicyDictionary.ContainsPropertyBinding(objectType, propertyModel);

		public static void SetTabControlErrorStatus(SimpleObjectValidationResult validationResult, XtraTabPage tabPage)
		{
			if (validationResult.Passed)
			{
				tabPage.Image = null;
				tabPage.TooltipIconType = DevExpress.Utils.ToolTipIconType.None;
				tabPage.Tooltip = "";
				// TODO: restore tabPage.Image
			}
			else
			{
				tabPage.Image = global::Simple.Objects.Controls.Properties.Resources.Error;
				tabPage.TooltipIconType = DevExpress.Utils.ToolTipIconType.Error;
				tabPage.Tooltip = validationResult.Message;

				tabPage.TabControl.SelectedTabPage = tabPage;
				// TODO: Remember editPanelPolicy.TabPage.Image
			}
		}


		#endregion |   Public Methods   |

		#region |   Protected Methods   |

		//protected override void Dispose(bool disposing)
  //      {
  //          this.editorBindingPolicyDictionary = null;
  //          this.ChangableBindingObjectControl = null;
  //          this.BindingObject = null;

  //          base.Dispose(disposing);
  //      }

        #endregion |   Protected Methods   |

        #region |   Private Methods   |

        private void changableBindingObjectControl_BindingObjectChange(object sender, BindingObjectEventArgs e)
        {
			//if (this.GetBindingObject != null)
			//{
				//if (this.BindingObject != null)
				//{
				//	this.BindingObject.PropertyValueChange -= BindingObject_PropertyValueChange;
				//	this.BindingObject.RelationForeignObjectSet -= BindingObject_RelationForeignObjectSet;
				//}

                this.BindingObject = e.BindingObject as IBindingSimpleObject;

				//if (this.BindingObject != null)
				//{
				//	this.BindingObject.PropertyValueChange += BindingObject_PropertyValueChange;
				//	this.BindingObject.RelationForeignObjectSet += BindingObject_RelationForeignObjectSet;
				//}

                this.ReloadData();
				this.ResetErrorStatus();

			//}
			//else if (this.BindingObject != e.SimpleObject)
			//{
			//	this.BindingObject = e.SimpleObject;
			//	this.ReloadData();

			if (this.TabControl != null && this.BindingObject is SimpleObject simpleObject) // Reset tab error if you delete node with error, fot example.
				SetTabControlErrorStatus(SimpleObjectValidationResult.GetDefaultPassedResult(simpleObject), this.TabControl.SelectedTabPage);
			//}
        }

        private void changableBindingObjectControl_BindingObjectPropertyValueChange(object sender, ChangePropertyValueBindingObjectEventArgs e)
        {
			if (e.BindingObject as IBindingSimpleObject == this.BindingObject && e.PropertyModel != null) // && e.Requester != this)
				this.ReloadData(e.BindingObject.GetType(), e.PropertyModel, e.Value, e.Requester);
		}

		private void changableBindingObjectControl_BindingObjectRelationForeignObjectSet(object sender, BindingObjectRelationForeignObjectSetEventArgs e)
		{
			if (e.BindingObject == this.BindingObject && e.Requester != this)
			{
				if (e.RelationModel.RelationType == ObjectRelationType.OneToOne)
				{
					IPropertyModel? propertyModel;

					if (this.oneToOneRelationPropertyModelsByRelationKey.TryGetValue(e.RelationModel.RelationKey, out propertyModel))
					{
						if (propertyModel != null)
							this.ReloadData(e.BindingObject.GetType(), propertyModel, e.ForeignBindingObject, e.Requester);
						
						this.ResetErrorStatus();
					}
				}
				else if (e.RelationModel.RelationType == ObjectRelationType.OneToMany)
				{
					IPropertyModel? propertyModel;

					if (this.oneToManyRelationPropertyModelsByRelationKey.TryGetValue(e.RelationModel.RelationKey, out propertyModel))
					{
						if (propertyModel != null)
							this.ReloadData(e.BindingObject.GetType(), propertyModel, e.ForeignBindingObject, e.Requester);
						
						this.ResetErrorStatus();
					}
				}
			}
		}

        private void changableBindingObjectControl_OnValidation(object sender, GraphValidationResultEventArgs e)
        {
            if (e.GraphValidationResult.SimpleObjectValidationResult.Target as IBindingSimpleObject == this.BindingObject)
            {
				bool isTabPageSet = false;
				
				foreach (EditorBindingPolicy editPanelPolicy in this.editorBindingPolicyDictionary.EditorBindingPolices)
                {
                    if (e.GraphValidationResult.Passed)
                    {
                        this.SetErrorProviderError(editPanelPolicy.EditorComponent, null);

                        if (editPanelPolicy.TabPage != null)
							SetTabControlErrorStatus(e.GraphValidationResult.SimpleObjectValidationResult, editPanelPolicy.TabPage);
                    }
                    else if (e.GraphValidationResult.SimpleObjectValidationResult.FailedRuleResult is PropertyValidationResult propertyValidationResult && propertyValidationResult.ErrorPropertyModel is not null)
					{
						IList<EditorBindingPolicy>? editPanelPolicyList = this.editorBindingPolicyDictionary.GetEditorBindingPolicyListByObjectPropertyModel(propertyValidationResult.ErrorPropertyModel);

                        if (editPanelPolicyList != null)
                        {
                            foreach (EditorBindingPolicy editorBindinglPolicy in editPanelPolicyList)
                            {
                                this.SetErrorProviderError(editorBindinglPolicy.EditorComponent, e.GraphValidationResult.Message);

								if (editorBindinglPolicy.TabPage != null)
								{
									SetTabControlErrorStatus(e.GraphValidationResult.SimpleObjectValidationResult, editorBindinglPolicy.TabPage);
									isTabPageSet = true;
								}
                            }
                        }
                    }
                }

				// If editor binding policy is not specified for some object properties that is binded by the tree list, for e.g., and tab control is specified, automaticaly process tab control error handling
				if (!isTabPageSet && this.TabControl != null)
					SetTabControlErrorStatus(e.GraphValidationResult.SimpleObjectValidationResult, this.TabControl.SelectedTabPage);
            }
        }

		private void changableBindingObjectControl_StoreData(object sender, BindingObjectEventArgs e)
        {
            if (e.BindingObject == this.BindingObject)
            {
                foreach (Component editorComponent in this.editorBindingPolicyDictionary.EditorComponents)
                {
                    if (editorComponent is BaseEdit baseEdit)
                    {
                        baseEdit.DoValidate();
                    }
                    else if (editorComponent is RepositoryItem repositoryItem && repositoryItem.OwnerEdit != null)
                    {
                        repositoryItem.OwnerEdit.DoValidate();
                    }
                    else if (editorComponent is Component component)
                    {
                    }
                }
            }
        }

		private void ObjectManager_RequireCommitChange(object sender, RequireCommiChangeContainertEventArgs e)
		{
			if (!e.RequireCommit)
				this.ResetErrorStatus();
		}

		private void ResetErrorStatus()
		{
			foreach (EditorBindingPolicy editPanelPolicy in this.editorBindingPolicyDictionary.EditorBindingPolices)
			{
				this.SetErrorProviderError(editPanelPolicy.EditorComponent, null);

				if (editPanelPolicy.TabPage != null)
					SetTabControlErrorStatus(new SimpleObjectValidationResult(target: null, passed: true, message: null, new ValidationResult(passed: true), TransactionRequestAction.Save), editPanelPolicy.TabPage);
			}

			// If editor binding policy is not specified for some object properties that is binded by the tree list, for e.g., and tab control is specified, automaticaly process tab control error handling
			if (this.TabControl != null)
				SetTabControlErrorStatus(new SimpleObjectValidationResult(target: null, passed: true, message: null, new ValidationResult(passed: true), TransactionRequestAction.Save), this.TabControl.SelectedTabPage);
		}

		//private void BindingObject_PropertyValueChange(object sender, ChangePropertyValueSimpleObjectRequesterEventArgs e)
		//{
		//	this.ReloadData(e.PropertyIndex, e.Value, e.Requester);
		//}

		//private void BindingObject_RelationForeignObjectSet(object sender, RelationForeignObjectSetEventArgs e)
		//{
		//	this.changableBindingObjectControl_BindingObjectRelationForeignObjectSet(sender, new BindingObjectRelationForeignObjectSetEventArgs(e));
		//}

		private XtraTabPage? GetControlTabPabe(Control? control)
        {
            XtraTabPage? result = null;

            if (this.TabControl != null)
            {
                foreach (XtraTabPage tabPage in this.TabControl.TabPages)
                {
                    if (tabPage.Contains(control))
                    {
                        result = tabPage;
                        
						break;
                    }
                }
            }

            return result;
        }

        private bool ReloadData(Type objectType, IPropertyModel propertyModel, object? propertyValue, object? requester)
        {
            IList<EditorBindingPolicy>? editorBindingPolicyList = this.editorBindingPolicyDictionary?.GetEditorBindingPolicyListByObjectPropertyModel(propertyModel);

            if (editorBindingPolicyList != null)
                return this.ReloadData(editorBindingPolicyList, objectType, propertyValue, requester);

			return false;
        }


		public object? GetControlValueFromPropertyValue(Type objectType, IPropertyModel propertyModel, object? propertyValue)
		{
			object? controlValue = null;
			IList<EditorBindingPolicy>? editorBindingPolicyList = this.editorBindingPolicyDictionary?.GetEditorBindingPolicyListByObjectPropertyModel(propertyModel);

			if (editorBindingPolicyList != null)
			{
				foreach (var item in editorBindingPolicyList)
				{
					if (item.GetControlValueFromPropertyValue != null && objectType.IsSameOrSubclassOf(item.ObjectType))
					{
						controlValue = item.GetControlValueFromPropertyValue(propertyValue);

						break;
					}
				}
			}

			if (controlValue == null)
				controlValue = propertyValue?.ToString();

			return controlValue;
		}

		// TOD: Add SimpleObject type cheching before load date to the control:  ReloadData(IList<EditorBindingPolicy> editorBindingPolicyList, Type, objectType, object propertyValue, object requester)
		private bool ReloadData(IList<EditorBindingPolicy> editorBindingPolicyList, Type objectType, object? propertyValue, object? requester)
        {
			bool isAnythingLoaded = false;

			if (editorBindingPolicyList == null)
				return isAnythingLoaded;

			this.DisableBinding();

			//if (propertyValue != null && propertyValue.GetType().IsEnum)
			//	propertyValue = (int)propertyValue;

			foreach (EditorBindingPolicy editPanelPolicy in editorBindingPolicyList)
            {
				if (editPanelPolicy.EditorComponent == requester || editPanelPolicy.ObjectType != objectType) // || objectType.IsSameOrSubclassOf(editPanelPolicy.ObjectType))
					continue;

				object? controlValue = (editPanelPolicy.GetControlValueFromPropertyValue) != null ? editPanelPolicy.GetControlValueFromPropertyValue(propertyValue) : propertyValue;

				if (editPanelPolicy.SetControlValue != null)
                {
                    editPanelPolicy.SetControlValue(controlValue);
					isAnythingLoaded = true;
				}
                else if (editPanelPolicy.EditorComponent is BaseEdit)
                {
                    BaseEdit editor = (BaseEdit)editPanelPolicy.EditorComponent;

					if (editor == null || editor.Properties == requester)
						continue;

					editor.EditValue = controlValue;
					isAnythingLoaded = true;
				}
				else if (editPanelPolicy.EditorComponent is RepositoryItem repositoryItem)
                {
                    BaseEdit editor = repositoryItem.OwnerEdit;

					if (editor == null || editor.Properties == requester)
						continue;

					editor.EditValue = controlValue;
					isAnythingLoaded = true;
				}
				else if (editPanelPolicy.EditorComponent is Control control)
                {
                    Control editor = control;

					if (editor == null || editor == requester)
						continue;

					editor.Text = controlValue != null ? controlValue.ToString() : String.Empty;
					isAnythingLoaded = true;
				}

				//if (editPanelPolicy.EditorComponent is CheckEdit)
				//{
				//	bool checkedValue = Conversion.TryChangeType<bool>(controlValue);
				//	editPanelPolicy.SetControlValue(checkedValue);
				//	(editPanelPolicy.EditorComponent as CheckEdit).Checked = checkedValue;
				//}
			}

			this.EnableBinding();

			return isAnythingLoaded;
        }

        //private string CreateRepositoryItemKey(int key)
        //{
        //    return strKey + key;
        //}

        //private int GetRepositoryItemKey(string repositoryItemName)
        //{
        //    int key = 0;
            
        //    try
        //    {
        //        key = Convert.ToInt32(repositoryItemName.Substring(strKey.Length));
        //    }
        //    catch
        //    {
        //    }

        //    return key;
        //}

        public void SetErrorProviderError(Component editorComponent, string? errorText)
        {
            this.SetErrorProviderError(editorComponent, errorText, ErrorType.Default);
        }

        public void SetErrorProviderError(Component editorComponent, string? errorText, ErrorType errorType)
        {
            if (this.ErrorProvider != null)
            {
                if (editorComponent is BaseEdit baseEdit)
                {
                    this.ErrorProvider.SetError(baseEdit, errorText, errorType);
                }
                else if (editorComponent is RepositoryItem repositoryItem)
                {
                    this.ErrorProvider.SetError(repositoryItem?.OwnerEdit, errorText, errorType);
                }
                else if (editorComponent is Control control)
                {
                    this.ErrorProvider.SetError(control, errorText, errorType);
                }
            }
        }
        
        private void EditorBindingsControl_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (this.binding > 0 && this.BindingObject is not null && sender is Component editorComponent)
            {
				if (editorComponent is BaseEdit baseEdit)
					editorComponent = baseEdit.Properties;

				if (editorComponent is not null)
					this.EditorBindingControlEditValueIsChanged(editorComponent, e.NewValue);
            }
        }

        private void EditorBindingsControl_TextChanged(object? sender, EventArgs e)
        {
            if (this.binding > 0 && this.BindingObject is not null && sender is Control editorComponent)
				this.EditorBindingControlEditValueIsChanged(editorComponent, editorComponent.Text);
        }

        private void EditorBindingControlEditValueIsChanged(Component editorComponent, object newEditValue)
        {
			if (this.BindingObject is null)
				return;
			
			EditorBindingPolicy? editPanelPolicy = this.editorBindingPolicyDictionary?.GetEditorBindingPolicyByEditorComponent(editorComponent);

            if (editPanelPolicy == null)
                throw new ArgumentException("The control is not registered as binding control of " + editorComponent.ToString());

			// This check is not needed since changable binding object can bee diferent from binded TreeList
			//if (this.BindingObject.GetModel().TableInfo.TableId != (editPanelPolicy.PropertyModel.Owner as SimpleObjectModel)?.TableInfo.TableId)
			//	return;
			
			object? propertyValue = editPanelPolicy.GetPropertyValueFromControlValue != null ? editPanelPolicy.GetPropertyValueFromControlValue(newEditValue) : newEditValue;

			//if (editPanelPolicy.SetControlValue != null)
			//{
			//	editPanelPolicy.SetControlValue(propertyValue);
			//}
			//else
			//{
			
			switch (editPanelPolicy.BindingType)
			{
				case EditorBindingType.ObjectProperty:

					IPropertyModel propertyModel = this.BindingObject.GetModel().PropertyModels[editPanelPolicy.PropertyModel.PropertyIndex];
					object? normalizedPropertyValue = this.GetNormalizedPropertyValue(propertyModel, propertyValue);

					this.BindingObject.SetPropertyValue(editPanelPolicy.PropertyModel.PropertyIndex, normalizedPropertyValue, requester: editorComponent);
					
					break;

				case EditorBindingType.OneToOneRelationPrimaryObject:

					this.BindingObject.SetOneToOnePrimaryObject((propertyValue as SimpleObject)!, editPanelPolicy.RelationKey, requester: this);
					
					break;

				case EditorBindingType.OneToOneRelationForeignObject:

					this.BindingObject.SetOneToOneForeignObject((propertyValue as SimpleObject)!, editPanelPolicy.RelationKey, requester: this);

					break;

				case EditorBindingType.OneToManyRelation:

					this.BindingObject.SetOneToManyPrimaryObject((propertyValue as SimpleObject)!, editPanelPolicy.RelationKey, requester: this);
					
					break;

				default:
					
					this.BindingObject.SetPropertyValue(editPanelPolicy.PropertyModel.PropertyIndex, propertyValue, requester: editorComponent);
					
					break;
			}

			//}
        }

        #endregion |   Private Methods   |
    }
}