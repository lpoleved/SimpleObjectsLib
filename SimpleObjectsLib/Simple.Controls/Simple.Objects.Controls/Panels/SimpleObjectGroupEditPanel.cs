﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using DevExpress.XtraEditors;
//using DevExpress.XtraBars.Ribbon;
using Simple;
using Simple.Controls;
using Simple.Modeling;

namespace Simple.Objects.Controls
{
    public partial class SimpleObjectGroupEditPanel : UserControl
    {
        #region |   Private Members   |
        
        private IChangableBindingObjectControl? changableBindingObjectControl = null;
        private IBindingSimpleObject? bindingObject = null;
        private Hashtable editPanelsByEditPanelPolicyModel = new Hashtable();
        private SimpleObjectEditPanel? activeEditPanel = null;
        private Type? activeEditPanelObjectType = null;
		private int[] activeEditPanelObjectSubTypes = new int[] { };

		#endregion |   Private Members   |

		#region |   Constructors and Initialization   |

		public SimpleObjectGroupEditPanel()
        {
            InitializeComponent();

			//SimpleObjectManager manager = null;

			//try
			//{
			//	manager = SimpleObjectManager.Instance;
			//}
			//catch
			//{
			//	manager = null;
			//}
			//finally
			//{
			//	this.Manager = manager;
			//}

        }

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			this.labelControl1.Visible = false;
		}

		#endregion |   Constructors and Initialization   |

		//#region |   Events   |

		//public event BindingObjectEventHandler BindingObjectChange;
		//public event ValidationResultBindingObjectEventHandler OnValidation;
		//public event BindingObjectEventHandler StoreData;
		//public event ChangedPropertyValueRequesterBindingObjectEventHandler PropertyValueChange;

		//#endregion |   Events   |

		#region |   Public Properties   |

		//[Browsable(false)]
		//[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		//public SimpleObjectManager Manager { get; set; }

		[Category("Simple"), DefaultValue(null)]
        public IChangableBindingObjectControl? ChangableBindingObjectControl
        {
            get { return this.changableBindingObjectControl; }
            set
            {
                if (this.changableBindingObjectControl != null)
                {
                    this.changableBindingObjectControl.BindingObjectChange -= new SimpleObjectEventHandler(changableBindingObjectControl_BindingObjectChange);
                    this.changableBindingObjectControl.BindingObjectPropertyValueChange -= new ChangePropertyValueRequesterBindingObjectEventHandler(changableBindingObjectControl_BindingObjectPropertyValueChange);
                    this.changableBindingObjectControl.BindingObjectRelationForeignObjectSet += new BindingObjectRelationForeignObjectSetEventHandler(changableBindingObjectControl_BindingObjectRelatedForeignObjectSet);
                    this.changableBindingObjectControl.BindingObjectOnValidation -= new GraphValidationResultEventHandler(changableBindingObjectControl_OnValidation);
                    this.changableBindingObjectControl.BindingObjectPushData -= new SimpleObjectEventHandler(changableBindingObjectControl_StoreData);
                    this.changableBindingObjectControl.BindingObjectRefreshContext -= new ChangePropertyValueRequesterBindingObjectEventHandler(changableBindingObjectControl_BindingObjectRefreshContext);
                }
                
                this.changableBindingObjectControl = value;

                if (this.changableBindingObjectControl != null)
                {
                    this.changableBindingObjectControl.BindingObjectChange += new SimpleObjectEventHandler(changableBindingObjectControl_BindingObjectChange);
                    this.changableBindingObjectControl.BindingObjectPropertyValueChange += new ChangePropertyValueRequesterBindingObjectEventHandler(changableBindingObjectControl_BindingObjectPropertyValueChange);
                    this.changableBindingObjectControl.BindingObjectRelationForeignObjectSet += new BindingObjectRelationForeignObjectSetEventHandler(changableBindingObjectControl_BindingObjectRelatedForeignObjectSet);
                    this.changableBindingObjectControl.BindingObjectParentGraphElementChange += new ChangeParentGraphElementRequesterEventHandler(changableBindingObjectControl_ParentGraphElementChange);
                    this.changableBindingObjectControl.BindingObjectOnValidation += new GraphValidationResultEventHandler(changableBindingObjectControl_OnValidation);
                    this.changableBindingObjectControl.BindingObjectPushData += new SimpleObjectEventHandler(changableBindingObjectControl_StoreData);
                    this.changableBindingObjectControl.BindingObjectRefreshContext += new ChangePropertyValueRequesterBindingObjectEventHandler(changableBindingObjectControl_BindingObjectRefreshContext);
                }
            }
        }

        #endregion |   Public Properties   |

        #region |   Public Methods   |

        public void RefreshContext()
        {
            if (this.activeEditPanel != null)
                (this.activeEditPanel as IBindingSimpleEditPanel).RefreshContext(new ChangePropertyValueBindingObjectRequesterEventArgs(this.bindingObject as SimpleObject, null, null, null, false, false, requester: this));
        }

		public void ReloadData()
		{
            if (this.activeEditPanel != null)
                (this.activeEditPanel as IBindingSimpleEditPanel).ReloadData(new ChangePropertyValueBindingObjectRequesterEventArgs(this.bindingObject as SimpleObject, null, null, null, false, false, requester: this));
		}

		//public void SetBindingObject(IBindingSimpleObject bindingObject)
		//{
		//	if (this.bindingObject != bindingObject)
		//		this.changableBindingObjectControl_BindingObjectChange(this, new SimpleObjectObjectEventArgs(bindingObject));
		//}

        #endregion |   Public Methods   |

        #region |   Private ChangableBindingObjectControl Event Methods   |

        private void changableBindingObjectControl_BindingObjectChange(object sender, SimpleObjectEventArgs e)
        {
			ISimpleObjectModel? objectModel = (e.SimpleObject != null) ? e.SimpleObject.GetModel() : null;
            Type objectType = (objectModel != null) ? objectModel.ObjectType : typeof(EmptySimpleObject);

            this.bindingObject = e.SimpleObject;
            this.SetActiveEditPanel(objectType, e.SimpleObject);
        }

        private void changableBindingObjectControl_BindingObjectPropertyValueChange(object sender, ChangePropertyValueBindingObjectRequesterEventArgs e)
        {
            IPropertyModel objectSubTypePropertyModel = e.BindingObject.GetModel().ObjectSubTypePropertyModel;

            if (objectSubTypePropertyModel != null && e.PropertyModel.PropertyIndex == objectSubTypePropertyModel.PropertyIndex)
                this.SetActiveEditPanel(e.BindingObject.GetType(), e.BindingObject);

            if (this.activeEditPanel is IBindingSimpleEditPanel bindingSimpleEditPanel)
                bindingSimpleEditPanel.PropertyValueIsChanged(e);
        }

        private void changableBindingObjectControl_BindingObjectRelatedForeignObjectSet(object sender, BindingObjectRelationForeignObjectSetRequesterEventArgs e)
        {
            if (this.activeEditPanel is IBindingSimpleEditPanel bindingSimpleEditPanel)
                bindingSimpleEditPanel.BindingObjectRelationForeignObjectIsSet(e);
        }

        private void changableBindingObjectControl_ParentGraphElementChange(object sender, OldParentGraphElementRequesterEventArgs e)
        {
            if (this.activeEditPanel is IBindingSimpleEditPanel bindingSimpleEditPanel)
                bindingSimpleEditPanel.ParentGraphElementIsChanged(e);
        }

        private void changableBindingObjectControl_OnValidation(object sender, GraphValidationResultEventArgs e)
        {
            if (this.activeEditPanel is IBindingSimpleEditPanel bindingSimpleEditPanel)
                bindingSimpleEditPanel.OnValidation(e);
        }

        private void changableBindingObjectControl_StoreData(object sender, SimpleObjectEventArgs e)
        {
            if (this.activeEditPanel is IBindingSimpleEditPanel bindingSimpleEditPanel)
                bindingSimpleEditPanel.StoreData(e);
        }

        private void changableBindingObjectControl_BindingObjectRefreshContext(object sender, ChangePropertyValueBindingObjectRequesterEventArgs e)
        {
            if (this.activeEditPanel is IBindingSimpleEditPanel bindingSimpleEditPanel)
                bindingSimpleEditPanel.RefreshContext(e);
        }

        #endregion |   Private ChangableBindingObjectControl Event Methods   |

        //#region |   Private Raise Event Methods   |

        //private void RaiseBindingObjectChange(BindingObjectEventArgs bindingObjectEventArgs)
        //{
        //    if (this.BindingObjectChange != null)
        //    {
        //        this.BindingObjectChange(this, bindingObjectEventArgs);
        //    }
        //}

        //private void RaisePropertyValueChange(ChangedPropertyValueRequesterBindingObjectEventArgs changedPropertyValueRequesterSimpleObjectEventArgs)
        //{
        //    if (this.PropertyValueChange != null)
        //    {
        //        this.PropertyValueChange(this, changedPropertyValueRequesterSimpleObjectEventArgs);
        //    }
        //}

        //private void RaiseOnValidation(ValidationResultBindingObjectEventArgs validationResultBindingObjectEventArgs)
        //{
        //    if (this.OnValidation != null)
        //    {
        //        this.OnValidation(this, validationResultBindingObjectEventArgs);
        //    }
        //}

        //private void RaiseStoreData(BindingObjectEventArgs bindingObjectEventArgs)
        //{
        //    if (this.StoreData != null)
        //    {
        //        this.StoreData(this, bindingObjectEventArgs);
        //    }
        //}

        //#endregion |   Private Raise Event Methods   |

        #region |   Private Methods   |

        private void SetActiveEditPanel(Type objectType, SimpleObject? bindingObject)
        {
			IPropertyModel? objectSubTypePropertyModel = bindingObject?.GetModel().ObjectSubTypePropertyModel;
			IEditPanelPolicyModel? editPanelPolicyModel = null;
			SimpleObjectEditPanel? editPanel = null;

			if (objectType == null)
				objectType = typeof(EmptySimpleObject);

			if (objectType != this.activeEditPanelObjectType || (objectSubTypePropertyModel != null && !this.activeEditPanelObjectSubTypes.Contains(bindingObject.GetPropertyValue<int>(objectSubTypePropertyModel))))
            {
				editPanelPolicyModel = ControlManager.Instance.GetEditPanelPolicyModel(bindingObject);

                if (editPanelPolicyModel != null)
                {
 					editPanel = this.editPanelsByEditPanelPolicyModel[editPanelPolicyModel] as SimpleObjectEditPanel;
					
					if (editPanel == null)
                    {
						editPanel = Activator.CreateInstance(editPanelPolicyModel.EditPanelType) as SimpleObjectEditPanel;

                        if (editPanel is not null)
                        {
                            editPanel.ObjectManager = bindingObject.Manager;

                            Type[] editPanelInterfaces = editPanel.GetType().GetInterfaces();

                            if (!editPanelInterfaces.Contains(typeof(ISimpleEditPanel)))
                                throw new Exception("Specified edit panel for object type " + editPanelPolicyModel.EditPanelType.ToString() + " must implement ISimpleEditPanel interface.");

                            this.editPanelsByEditPanelPolicyModel.Add(editPanelPolicyModel, editPanel);
                        }
                    }

                    if (editPanel is not null)
                    {
                        editPanel.Parent = this;
                        (editPanel as IBindingSimpleEditPanel).SetChangableBindingObjectControl(this.ChangableBindingObjectControl);
                    }
                }
				else
				{
					editPanel = new EditPanelEmpty();
				}

				this.activeEditPanelObjectType = objectType;
				this.activeEditPanelObjectSubTypes = (editPanelPolicyModel != null) ? editPanelPolicyModel.ObjectSubTypes : new int[] { };
            }

			if (editPanel != null && editPanel != this.activeEditPanel)
			{
                editPanel.BringToFront();
                editPanel.Dock = DockStyle.Fill;
                editPanel.Visible = true;

                if (this.activeEditPanel != null)
                {
                    this.activeEditPanel.Visible = false;
                    this.activeEditPanel.Dock = DockStyle.None;
                }

                this.activeEditPanel = editPanel;
			}

            if (this.activeEditPanel is IBindingSimpleEditPanel bindingSimpleEditPanel)
                bindingSimpleEditPanel.SetBindingObject(bindingObject);
        }

        #endregion |   Private Methods   |

        //#region |   IBindingObjectControl Interface   |

        //bool IBindingObjectControl.SaveBindingObject(object requester, IBindingObject bindingObject)
        //{
        //    return this.ChangableBindingObjectControl.SaveBindingObject(requester, bindingObject);
        //}

        //#endregion |   IBindingObjectControl Interface   |
    }
}
