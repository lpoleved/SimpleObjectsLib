using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using Simple;
using Simple.Controls;

namespace Simple.Objects.Controls
{
	public class ControlManager
	{
		private static ControlManager? instance = null;
		//private Dictionary<Type, EditPanelPolicyModel> editPanelPolicyModelsByObjectType = new Dictionary<Type, EditPanelPolicyModel>();
		private HashSet<EditPanelPolicyModel> editPanelPolicyModels = new HashSet<EditPanelPolicyModel>();

		protected static object lockObject = new object();

		public ControlManager()
		{
			// Create editPanelPolicyModelsByObjectType dictionary edit panel object type definition
			IEnumerable<Type> editPanelTypes = ReflectionHelper.SelectInheritedAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies().ToList(), typeof(ISimpleEditPanel)); //, type => typeof(ISimpleEditPanel).IsAssignableFrom(type) && !type.IsGenericType && !type.IsAbstract);

			foreach (Type editPanelType in editPanelTypes)
			{
				//MessageBox.Show("object[] editPanelObjectDefinitionAttribute = ReflectionHelper.GetCustomAttribute(editPanelType, typeof(EditPanelObjectTypeAttribute)); - " + editPanelType.Name);
				//object[] editPanelObjectDefinitionAttribute = ReflectionHelper.GetCustomAttribute(editPanelType, typeof(EditPanelObjectTypeAttribute));
				object[] editPanelObjectDefinitionAttribute = editPanelType.GetCustomAttributes(typeof(EditPanelInfoAttribute), false);

				//object[] editPanelObjectDefinitionAttribute = new object[] { };
				//editPanelObjectDefinitionAttribute = Simple.Objects.SimpleObjectManager.Instance.GetCustomAttribute(editPanelType, typeof(EditPanelObjectTypeAttribute));

				//MessageBox.Show("if (editPanelObjectDefinitionAttribute.Length > 0);  - " + editPanelObjectDefinitionAttribute.Length);
				if (editPanelObjectDefinitionAttribute.Length > 0 && editPanelObjectDefinitionAttribute[0] is EditPanelInfoAttribute editPanelObjectDefinition)
				{
					EditPanelPolicyModel editPanelPolicyModel = new EditPanelPolicyModel(editPanelObjectDefinition.ObjectType, editPanelType, editPanelObjectDefinition.ObjectSubTypes);

					//this.editPanelPolicyModelsByObjectType.Add(editPanelObjectDefinition.ObjectType, editPanelPolicyModel);
					this.editPanelPolicyModels.Add(editPanelPolicyModel);
				}
			}
		}
		public IEditPanelPolicyModel? GetEditPanelPolicyModel(SimpleObject simpleObject)
		{
			IEditPanelPolicyModel? result = null;

			if (simpleObject == null)
				return result;

			if (simpleObject.GetModel().SubTypePropertyModel != null)
			{
				int objectSubType = (int)simpleObject.GetPropertyValue(simpleObject.GetModel().SubTypePropertyModel);

				result = this.editPanelPolicyModels.FirstOrDefault(item => item.ObjectType == simpleObject.GetType() && item.ObjectSubTypes.Contains(objectSubType));

				if (result != null)
					return result;
			}

			return this.editPanelPolicyModels.FirstOrDefault(item => item.ObjectType == simpleObject.GetType());
		}

		public IEditPanelPolicyModel? GetEditPanelPolicyModel(Type objectType)
		{
			return this.editPanelPolicyModels.FirstOrDefault(item => item.ObjectType == objectType);
		}

		public static ControlManager Instance
		{
			get 
			{
				lock (lockObject)
				{
					if (instance == null)
						instance = new ControlManager();
				}

				return instance;
			}
		}

		//protected static T? GetInstance<T>() where T : ControlManager, new()
		//{
		//	lock (lockObject)
		//	{
		//		if (instance == null)
		//			instance = new T();
		//	}

		//	return instance as T;
		//}
	}
}
