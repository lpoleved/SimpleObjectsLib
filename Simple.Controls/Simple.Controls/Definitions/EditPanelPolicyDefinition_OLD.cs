//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple;
//using Simple.Objects;
//using Simple.Objects;

//namespace Simple.Controls
//{
//	public class EditPanelPolicyDefinition
//	{
//		public static Dictionary<Type, EditPanelPolicyModel> GetEditPanelPolicyModelsByObjectType()
//		{
//			Dictionary<Type, EditPanelPolicyModel> editPanelPolicyModelsByObjectType = new Dictionary<Type, EditPanelPolicyModel>();
//			List<Type> editPanelTypes = ReflectionHelper.GetInheritedTypesInAssembly(typeof(ISimpleEditPanel), type => typeof(ISimpleEditPanel).IsAssignableFrom(type) && !type.IsGenericType && !type.IsAbstract);

//			foreach (Type editPanelType in editPanelTypes)
//			{
//				object[] editPanelObjectDefinitionAttribute = editPanelType.GetCustomAttributes(typeof(EditPanelObjectTypeAttribute), false);
//				if (editPanelObjectDefinitionAttribute.Length > 0)
//				{
//					EditPanelObjectTypeAttribute editPanelObjectDefinition = editPanelObjectDefinitionAttribute[0] as EditPanelObjectTypeAttribute;
//					EditPanelPolicyModel editPanelPolicyModel = new EditPanelPolicyModel(editPanelObjectDefinition.ObjectType.Name)
//					{
//						ObjectType = editPanelObjectDefinition.ObjectType,
//						EditPanelType = editPanelType
//					};

//					editPanelPolicyModelsByObjectType.Add(editPanelObjectDefinition.ObjectType, editPanelPolicyModel);
//				}
//			}

//			return editPanelPolicyModelsByObjectType;
//		}
//	}


//	public class EditPanelPolicyDefinitionTemplate<T> : EditPanelPolicyDefinitionTemplate
//		where T : EditPanelPolicyDefinitionTemplate<T>, new()
//	{
//		private static object instance = null;
//		private static object lockObjectInstance = new object();

//		public static T Instance
//		{
//			get { return GetInstance<T>(); }
//		}

//		protected static U GetInstance<U>() where U : new()
//		{
//			lock (lockObjectInstance)
//			{
//				if (instance == null)
//				{
//					instance = new U();
//				}
//			}

//			return (U)instance;
//		}
//	}

//	public class EditPanelPolicyDefinitionTemplate
//	{
//		private Hashtable editPanelPolicyModelsByObjectType = new Hashtable();

//		public EditPanelPolicyDefinitionTemplate()
//		{
//			this.SetPolicyModel<EmptyObject, EditPanelEmpty>();
//			this.SetPolicyModel<Folder, EditPanelFolder>();
//		}

//		public Hashtable EditPanelPolicyModelsByObjectType
//		{
//			get { return this.editPanelPolicyModelsByObjectType; }
//		}

//		public void SetPolicyModel<T, U>()
//			where U : SimpleEditPanel
//		{
//			this.SetPolicyModel(typeof(T), typeof(U));
//		}

//		public void SetPolicyModel(Type objectType, Type editPanelType)
//		{
//			string modelName = objectType.Name;

//			EditPanelPolicyModel editPanelPolicyModel = new EditPanelPolicyModel(modelName)
//			{
//				ObjectType = objectType,
//				EditPanelType = editPanelType
//			};

//			this.editPanelPolicyModelsByObjectType.Add(objectType, editPanelPolicyModel);
//		}
//	}
//}
