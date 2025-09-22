using System;
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
using Simple.Objects;

namespace Simple.Objects.Controls
{
	public partial class GroupPropertyPanel : UserControl
	{
		#region |   Private Members   |

		private object? bindingObject = null;
		private Dictionary<Type, SystemEditPanel> editPanelsByBindingObjectType = new Dictionary<Type, SystemEditPanel>();
		private Dictionary<Type, SystemEditPanel> editPanelsByType = new Dictionary<Type, SystemEditPanel>();
		private Dictionary<Type, Type> editPanelTypeDefinitionsByBindingObjectType = new Dictionary<Type, Type>();
		private SystemEditPanel? activePropertyPanel = null;
		private Type? activePropertyPanelObjectType = null;

		#endregion |   Private Members   |

		#region |   Constructors and Initialization   |

		public GroupPropertyPanel()
		{
			InitializeComponent();
			this.labelControlInfo.Visible = false;
			this.SetActivePropertyPanel(typeof(EmptySimpleObject), this.ParentForm);
		}

		#endregion |   Constructors and Initialization   |

		#region |   Events   |

		public event GetEditPanelTypeEventHandler GetEditPanelType;

		#endregion |   Events   |

		#region |   Public Properties   |

		#endregion |   Public Properties   |

		#region |   Public Methods   |

		public void SetPanelObjectDefinition(Type objectType, Type propertyPanelType)
		{
			if (!propertyPanelType.IsSubclassOf(typeof(SystemEditPanel)))
				throw new ArgumentException("Edit panel type must be inherited from PropertyPanel class.");

			this.editPanelTypeDefinitionsByBindingObjectType.Add(objectType, propertyPanelType);
		}

		//public void SetBindingObject(object? bindingObject)
		//{
		//	this.SetBindingObject(bindingObject, context: null);
		//}

		public void SetBindingObject(object? bindingObject, object? context)
		{
			if (this.bindingObject == bindingObject)
				return;

			this.bindingObject = bindingObject;

			this.SetActivePropertyPanel(bindingObject, context);

			if (this.activePropertyPanel is IBindingControl bindingControl)
				bindingControl.SetBindingObject(bindingObject);
		}

		public void RefreshBindingObject(object? requester)
		{
			if (this.activePropertyPanel is IBindingControl bindingControl)
				bindingControl.RefreshBindingObject(requester);
		}

		#endregion |   Public Methods   |

		#region |   Private Methods   |

		private void SetActivePropertyPanel(object? bindingObject)
		{
			this.SetActivePropertyPanel(bindingObject, context: null);
		}

		private void SetActivePropertyPanel(object? bindingObject, object? context)
		{
			SystemEditPanel? editPanel = null;
			Type? editPanelType = null;

			if (this.GetEditPanelType != null)
			{
				editPanelType = this.GetEditPanelType(bindingObject);

				if (editPanelType == null)
					editPanelType = typeof(SystemEditPanelEmpty);

				if (!this.editPanelsByType.TryGetValue(editPanelType, out editPanel))
				{
					editPanel = Activator.CreateInstance(editPanelType) as SystemEditPanel;

					if (editPanel != null)
					{
						this.editPanelsByType.Add(editPanelType, editPanel);
						editPanel.Parent = this;
						//(editPanel as SystemEditPanel)!.Context = context;
					}
				}
			}
			else
			{
				Type objectType = (bindingObject != null) ? bindingObject.GetType() : typeof(EmptySimpleObject);

				if (objectType != this.activePropertyPanelObjectType)
				{
					if (!this.editPanelsByBindingObjectType.TryGetValue(objectType, out editPanel))
					{
						Type? propertyPanelControlType;

						if (this.editPanelTypeDefinitionsByBindingObjectType.TryGetValue(objectType, out propertyPanelControlType))
							editPanel = Activator.CreateInstance(propertyPanelControlType) as SystemEditPanel;

						if (editPanel != null)
						{
							this.editPanelsByBindingObjectType.Add(objectType, editPanel);
							editPanel.Parent = this;
							//editPanel.Context = context;
						}

						//editPanel.Dock = DockStyle.None;
						//editPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
					}

					//editPanel.BringToFront();
					//editPanel.Dock = DockStyle.Fill;
					//editPanel.Visible = true;

					//if (this.activePropertyPanel != null)
					//{
					//	this.activePropertyPanel.Visible = false;
					//	this.activePropertyPanel.Dock = DockStyle.None;
					//}

					//this.activePropertyPanel = editPanel;
					//this.activePropertyPanelObjectType = objectType;
				}
			}

			if (editPanel is null)
			{
				editPanel = new SystemEditPanelEmpty();
				//(editPanel as SystemEditPanelEmpty)!.Context = context;
			}

			editPanel.Context = context;

			if (editPanel != this.activePropertyPanel)
			{
				editPanel.BringToFront();
				editPanel.Dock = DockStyle.Fill;
				editPanel.Visible = true;

				if (this.activePropertyPanel != null)
				{
					this.activePropertyPanel.Visible = false;
					this.activePropertyPanel.Dock = DockStyle.None;
				}

				this.activePropertyPanel = editPanel;
				this.activePropertyPanelObjectType = editPanel.GetType();
			}

			//if (editPanel is SystemEditPanel systemEditPanel)
			//	systemEditPanel.Context = context;

			//this.activePropertyPanelObjectType = null;
		}

		#endregion |   Private Methods   |

		#region |   Private ChangableBindingObjectControl Event Methods   |

		private void changableBindingObjectControl_BindingObjectChange(object sender, SimpleObjectEventArgs e)
		{
			ISimpleObjectModel? objectModel = (e.SimpleObject != null) ? e.SimpleObject.GetModel() : null;
			Type objectType = (objectModel != null) ? objectModel.ObjectType : typeof(EmptySimpleObject);

			this.bindingObject = e.SimpleObject;

			this.SetActivePropertyPanel(objectType);
			(this.activePropertyPanel as IBindingSimpleEditPanel)?.SetBindingObject(e.SimpleObject);
		}

		private void changableBindingObjectControl_BindingObjectPropertyValueChange(object sender, ChangePropertyValueBindingObjectEventArgs e)
		{
			(this.activePropertyPanel as IBindingSimpleEditPanel)?.PropertyValueIsChanged(e);
		}

		private void changableBindingObjectControl_BindingObjectRelatedForeignObjectSet(object sender, BindingObjectRelationForeignObjectSetEventArgs e)
		{
			(this.activePropertyPanel as IBindingSimpleEditPanel)?.BindingObjectRelationForeignObjectIsSet(e);
		}

		private void changableBindingObjectControl_ParentGraphElementChange(object sender, OldParentGraphElemenChangeContainertContextRequesterEventArgs e)
		{
			(this.activePropertyPanel as IBindingSimpleEditPanel)?.ParentGraphElementIsChanged(e);
		}

		private void changableBindingObjectControl_OnValidation(object sender, GraphValidationResultEventArgs e)
		{
			(this.activePropertyPanel as IBindingSimpleEditPanel)?.OnValidation(e);
		}

		private void changableBindingObjectControl_StoreData(object sender, SimpleObjectEventArgs e)
		{
			(this.activePropertyPanel as IBindingSimpleEditPanel)?.StoreData(e);
		}

		private void changableBindingObjectControl_BindingObjectRefreshContext(object sender, ChangePropertyValueBindingObjectEventArgs e)
		{
			(this.activePropertyPanel as IBindingSimpleEditPanel)?.RefreshContext(e);
		}

		#endregion |   Private ChangableBindingObjectControl Event Methods   |
	}

	#region |   EventArgs Classes   |

	public class GetPropertyPanelEventArgs : EventArgs
	{
		public GetPropertyPanelEventArgs(object bindingObject)
		{
			this.BindingObject = bindingObject;
		}

		public object BindingObject { get; private set; }
	}

	#endregion |   EventArgs Classes   |

	#region |   Delegates   |

	public delegate Type? GetEditPanelTypeEventHandler(object? bindingObject);

	#endregion |   Delegates   |
}
