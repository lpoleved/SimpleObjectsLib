using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Security.Cryptography;
using Simple;
using Simple.Collections;
using Simple.Modeling;
using Simple.Security;
using System.Runtime.CompilerServices;

namespace Simple.Objects
{
    public class EasyObjectManager
    {
        #region |   Private Members   |

        //private static EasyObjectManager instance = null;
        protected object lockObject = new object();

		private bool enforcePropertyExistanceInModel = true;
		private IList<Assembly> assemblies = null;
        //private object lockObjectAction = new object();
        ////private SimpleDataActionOption dataActionOption = SimpleDataActionOption.GetLocalDataOnly; // TODO: This processing feature is not implemente, yet.
        //private int binding = 0;    // 0 - binding enablebled

        #endregion |   Private Members   |

		#region |   Protected Members   |

		//protected List<Type> allObjectModelTypes = null;
		//protected List<Type> allObjectModelDefinitionTypes = null;
		protected SimpleDictionary<Type, IEasyObjectModel> objectModelsByObjectType = new SimpleDictionary<Type, IEasyObjectModel>();   // Key is ObjectType as Type, Value is model of the object, tipically as EasyObjectModel (but is not necessary).
		protected SimpleDictionary<Type, IEasyObjectModel> objectModelsByObjectModelDefinitionType = new SimpleDictionary<Type, IEasyObjectModel>();
		protected List<Type> notIncludedObjectModelDefinitionTypes = new List<Type>();
		protected List<EasyObjectDependencyAction> objectDependencyActions = new List<EasyObjectDependencyAction>();

		#endregion |   Protected Members   |

		//#region |   Protected Members   |

		//protected object lockObject = new object();
		

		//#endregion |   Protected Members   |

		#region |   Constructors and Initialization   |

		public EasyObjectManager(IList<Assembly> assemblies)
        {
			this.assemblies = assemblies;
			////// Field based model definition
			////IDictionary<string, IEasyObjectModel> models = ReflectionHelper.GetFieldsByReflection<IEasyObjectModel>(this);
			////this.SetObjectModelDefinition(models.Values);

			//// Discovery assembly model definitions
			////List<Type> reversedObjectModels = new List<Type>();

			////// Some computer Net.Framework assemblies has an problem when calling assembly.GetTypes() that result in app crash at start.
			////// To awoid app chrash, we need alternative method to get all IEasyObjectModel object models class types.
			//////
			////var objectModels = from assembly in AppDomain.CurrentDomain.GetAssemblies()
			////				   from type in assembly.GetTypes()
			////				   where typeof(IEasyObjectModel).IsAssignableFrom(type) && !type.IsInterface && !type.IsGenericType && !type.IsAbstract
			////				   select type;

			////// The problem is here....
			////foreach (Type type in objectModels)
			////	reversedObjectModels.Add(type);


			//List<Type> objectModelTypes = ReflectionHelper.GetInheritedTypesInAssembly(typeof(IEasyObjectModel));

			////List<Type> reversedObjectModels = new List<Type>();
			////List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

			////foreach (Assembly assembly in assemblies)
			////{
			////	Type[] types = null;

			////	try
			////	{
			////		types = assembly.GetTypes();
			////	}
			////	catch
			////	{
			////		continue;
			////	}

			////	if (types != null)
			////		foreach (Type type in types)
			////			if (type != null && typeof(IEasyObjectModel).IsAssignableFrom(type) && !type.IsInterface && !type.IsGenericType && !type.IsAbstract)
			////				reversedObjectModels.Add(type);
			////}

			//for (int i = objectModelTypes.Count - 1; i >= 0; i--)
			//{
			//	IEasyObjectModel model = Activator.CreateInstance(objectModelTypes[i]) as IEasyObjectModel;
			//	this.SetObjectModelDefinition(model, model.ObjectType);
			//}
		}

		protected void CreateObjectModels()
		{
			List<Type> reducedObjectModelDefinitionTypes = new List<Type>();
			List<Type> systemModelDefinitionTypes = new List<Type>();
			List<Type> appModelDefinitionTypes = new List<Type>();
			IEnumerable<Type> objectModelDefinitionTypes = ReflectionHelper.SelectInheritedAssemblyTypes(this.assemblies, typeof(IEasyObjectModel));

			// Get only top inherited classes
			foreach (Type type in objectModelDefinitionTypes)
			{
				Type topInheritedType = ReflectionHelper.FindTopInheritedClass(type, objectModelDefinitionTypes);

				if (!reducedObjectModelDefinitionTypes.Contains(topInheritedType))
				{
					reducedObjectModelDefinitionTypes.Add(topInheritedType);
				}
				else
				{
					this.notIncludedObjectModelDefinitionTypes.Add(type);
				}
			}

			// Split the models into two groups: system and app
			foreach (Type type in reducedObjectModelDefinitionTypes)
			{
				if (this.IsObjectModelClassSystemAssembly(type))
				{
					systemModelDefinitionTypes.Add(type);
				}
				else
				{
					appModelDefinitionTypes.Add(type);
				}
			}

			// Set Base model definition classes first
			notIncludedObjectModelDefinitionTypes.Reverse();

			foreach (Type objectModelDefinitionType in notIncludedObjectModelDefinitionTypes)
				this.SetObjectModel(objectModelDefinitionType);

			// Than add system object models, 
			systemModelDefinitionTypes.Reverse();

			foreach (Type objectModelDefinitionType in systemModelDefinitionTypes)
				this.SetObjectModel(objectModelDefinitionType);

			// Add an application models at the end to be the last
			appModelDefinitionTypes.Reverse();

			foreach (Type objectModelDefinitionType in appModelDefinitionTypes)
				this.SetObjectModel(objectModelDefinitionType);
		}

		//protected virtual void ProcessNotIncludedObjectModelTypes(List<Type> notIncludedObjectModelTypes)
		//{
		//}

		protected virtual bool IsObjectModelClassSystemAssembly(Type objectModelType)
		{
			return objectModelType.Assembly == Assembly.GetExecutingAssembly();
		}

		//public static EasyObjectManager Instance
		//{
		//	get { return GetInstance<EasyObjectManager>(); }
		//}
		
		//protected static T GetInstance<T>() where T : EasyObjectManager, new()
  //      {
  //          lock (lockStaticObject)
  //          {
  //              if (instance == null)
  //                  instance = new T();
  //          }

  //          return instance as T;
  //      }


        #endregion |   Constructors and Initialization   |

        #region |   Events   |

        //public event ActionRequesterEasyObjectEventHandler ObjectAction;

        public event EasyObjectRequesterEventHandler ObjectCreated;

        public event EasyObjectRequesterEventHandler BeforeSaving;
        public event EasyObjectRequesterEventHandler Saving;
        public event AfterSaveEasyObjectRequesterEventHandler AfterSave;

		//public event EasyObjectRequesterEventHandler BeforeLoading;
		//public event EasyObjectRequesterEventHandler AfterLoad;

        public event EasyObjectRequesterEventHandler BeforeDeleting;
        public event EasyObjectRequesterEventHandler AfterDelete;

        public event BeforeChangePropertyValueEasyObjectRequesterEventHandler BeforePropertyValueChange;
        public event ChangePropertyValueEasyObjectRequesterEventHandler PropertyValueChange;

        public event CountChangeEasyObject ChangedPropertiesCountChange;
        public event RequireSavingChangeEasyObject RequireSavingChange;
        
        public event ImageNameChangeEasyObject ImageNameChange;
		public event MultipleImageNameChangeEasyObject MultipleImageNameChange;

        #endregion |   Events   |

        #region |   Public Properties   |

        public ICryptoTransform Encryptor { get; protected set; }
        public ICryptoTransform Decryptor { get; protected set; }
        public int CryptoBlockSize { get; protected set; }
        
        public bool EnforcePropertyExistanceInModel
        {
            get { return this.enforcePropertyExistanceInModel; }
            set { this.enforcePropertyExistanceInModel = value; }
        }

        #endregion |   Public Properties   |

        #region |   Protected Properties   |

        protected List<EasyObjectDependencyAction> ObjectDependencyActions
        {
            get { return this.objectDependencyActions; }
        }

        #endregion |   Protected Properties   |

        #region |   Public Methods   |

		//public void Load(object requester, EasyObject easyObject)
		//{
		//	//lock (this.lockObjectAction)
		//	//{
		//	easyObject.ActionBeforeLoading(requester);
		//	this.OnBeforeLoading(requester, easyObject);
		//	this.RaiseBeforeLoading(requester, easyObject);
		//	//this.RaiseObjectAction(requester, easyObject, EasyObjectAction.BeforeLoading);

		//	this.OnLoad(requester, easyObject);

		//	easyObject.ActionAfterLoad(requester);
		//	this.OnAfterLoad(requester, easyObject);
		//	this.RaiseAfterLoad(requester, easyObject);
		//	//this.RaiseObjectAction(requester, easyObject, EasyObjectAction.AfterLoad);
		//	//}
		//}

		//public void Load(object requester, EasyObject easyObject, IDictionary<string, object> propertyDictionary, bool isNew)
		//{
		//	//lock (this.lockObjectAction)
		//	//{
		//	easyObject.ActionBeforeLoading(requester);
		//	this.OnBeforeLoading(requester, easyObject);
		//	this.RaiseBeforeLoading(requester, easyObject);
		//	//this.RaiseObjectAction(requester, easyObject, EasyObjectAction.BeforeLoading);

		//	easyObject.SetPropertyDictionary(propertyDictionary);
		//	easyObject.IsNew = isNew;
		//	easyObject.AcceptChanges(requester);

		//	this.OnLoad(requester, easyObject);

		//	easyObject.ActionAfterLoad(requester);
		//	this.OnAfterLoad(requester, easyObject);
		//	this.RaiseAfterLoad(requester, easyObject);
		//	//this.RaiseObjectAction(requester, easyObject, EasyObjectAction.AfterLoad);
		//	//}
		//}

        public virtual bool Save(object requester, EasyObject easyObject)
        {
            return this.SaveInternal(requester, easyObject);
        }

        public virtual bool SaveInternal(object requester, EasyObject easyObject)
        {
            if (easyObject.ChangedPropertiesCount > 0)
            {
                //lock (this.lockObjectAction)
                //{
                    easyObject.ActionBeforeSaving(requester);
                    this.OnBeforeSaving(requester, easyObject);
                    this.RaiseBeforeSaving(requester, easyObject);

                    bool isNewBeforeSaving = easyObject.IsNew;
                    
                    easyObject.ActionOnSaving(requester);
                    this.OnSaving(requester, easyObject);
                    this.RaiseSaving(requester, easyObject);

                    if (easyObject.IsNew)
                        easyObject.IsNew = false;

                    easyObject.AcceptChanges(requester);

                    easyObject.ActionAfterSave(requester, isNewBeforeSaving);
                    this.OnAfterSave(requester, easyObject, isNewBeforeSaving);
                    this.RaiseAfterSave(requester, easyObject, isNewBeforeSaving);
                //}
            }

            return true;
        }

        public virtual bool CanDelete(EasyObject easyObject)
        {
            return true;
        }

        public virtual bool Delete(object requester, EasyObject easyObject)
        {
            bool isDeleted;

            if (isDeleted = this.CanDelete(easyObject))
            {
                this.DeleteInternal(requester, easyObject);
            }

            return isDeleted;
        }

        public virtual void DeleteInternal(object requester, EasyObject easyObject)
        {
            if (easyObject.IsDeleteStarted)
                return;

            //lock (this.lockObjectAction)
            //{
            easyObject.IsDeleteStarted = true;
            easyObject.ActionBeforeDeleting(requester);
            this.RaiseBeforeDeleting(requester, easyObject);
            this.OnBeforeDeleting(requester, easyObject);

            this.OnDeleting(requester, easyObject);

            easyObject.IsDeleted = true;
            this.OnAfterDelete(requester, easyObject);
            this.RaiseAfterDelete(requester, easyObject);

            (easyObject as IDisposable).Dispose();
            //System.GC.SuppressFinalize(easyObject);
            easyObject = null;
            //}
        }

        public virtual EasyObjectValidationResult Validate(EasyObject easyObject)
        {
            return null;
        }

        public void AcceptChanges(object requester, EasyObject easyObject)
        {
            easyObject.AcceptChanges(requester);
        }

        public void RejectChanges(object requester, EasyObject easyObject)
        {
            easyObject.RejectChanges(requester);
        }

        public IEasyObjectModel GetObjectModel(Type objectType)
        {
            return this.objectModelsByObjectType[objectType] as IEasyObjectModel;
        }

        public virtual string GetImageName(EasyObject easyObject)
        {
			string imageName = String.Empty;
			
			if (easyObject != null)
            {
                //try
                //{
                    string oldImageName = easyObject.GetImageNameInternal();
                    imageName = this.GenerateImageName(easyObject);

                if (imageName == null)
                    imageName = null;
                
                if (!Equals(imageName, oldImageName))
                    this.SetImageName(easyObject, imageName, oldImageName);
                //}
                //catch
                //{
                //}
            }

            return imageName;
        }

        public virtual void SetImageName(EasyObject easyObject, string imageName, string oldImageName)
        {
            if (imageName != oldImageName)
            {
                bool fireOnImageNameChange = !oldImageName.IsNullOrEmpty() && !Equals(imageName, oldImageName);

                easyObject.SetImageNameInternal(imageName, oldImageName, fireOnImageNameChange);

                if (fireOnImageNameChange)
                {
                    this.RaiseImageNameChange(easyObject, imageName, oldImageName);
                    this.OnImageNameChange(easyObject, imageName, oldImageName);
                }
            }
        }

        //public virtual int GetImageIndex(EasyObject easyObject)
        //{
        //    if (easyObject.imageInfo == null)
        //        easyObject.imageInfo = this.CreateImageInfo();

        //    return easyObject.imageInfo.ImageIndex;
        //}

        #endregion |   Public Methods   |

        #region |   Protected Internal Methods   |

        protected internal virtual void ObjectIsCreated(object requester, EasyObject easyObject)
        {
            this.RaiseObjectCreated(requester, easyObject);
            this.OnObjectCreated(requester, easyObject);
        }

        protected internal virtual void BeforePropertyValueIsChanged(object requester, EasyObject easyObject, int propertyIndex, object value, object newValue)
        {
			this.RaiseBeforePropertyValueChange(requester, easyObject, propertyIndex, value, newValue);
            this.OnBeforePropertyValueChange(requester, easyObject, propertyIndex, value, newValue);
        }

        protected internal virtual void PropertyValueIsChanged(object requester, EasyObject easyObject, int propertyIndex, object value, object oldValue)
        {
            this.RaisePropertyValueChange(requester, easyObject, propertyIndex, value, oldValue);
            this.OnPropertyValueChange(requester, easyObject, propertyIndex, value, oldValue);
        }

        protected internal virtual void ChangedPropertiesCountIsChanged(EasyObject easyObject, int changedPropertiesCount, int oldChangedPropertiesCount)
        {
            this.RaiseChangedPropertiesCountChange(easyObject, changedPropertiesCount, oldChangedPropertiesCount);
            this.OnChangedPropertyNamesCountChange(easyObject, changedPropertiesCount, oldChangedPropertiesCount);

            if (oldChangedPropertiesCount == 0)
            {
                this.RaiseRequireSavingChange(easyObject, requireSaving: true);
            }
            else if (changedPropertiesCount == 0)
            {
                this.RaiseRequireSavingChange(easyObject, requireSaving: false);
            }
        }

        #endregion |   Protected Internal Methods   |

        #region |   Protected Methods   |

        //protected T GetObjectModelDefinition<T>(Type objectType) where T : IEasyObjectModel
        //{
        //    T value = default(T);

        //    if (this.objectModelHashtable.ContainsKey(objectType))
        //    {
        //        value = (T)this.objectModelHashtable[objectType];
        //    }

        //    return value;
        //}

        //protected void SetObjectModelDefinition(IEnumerable<IEasyObjectModel> modelList)
        //{
        //    foreach (IEasyObjectModel model in modelList)
        //    {
        //        this.SetObjectModelDefinition(model);
        //    }
        //}

        protected virtual string GenerateImageName(EasyObject easyObject)
        {
            return this.GetObjectModel(easyObject.GetType()).ImageName;
        }

        protected virtual void RecalculateImageName(EasyObject easyObject)
        {
            this.GetImageName(easyObject);
        }

        //protected virtual ImageInfo CreateImageInfo()
        //{
        //    return new ImageInfo();
        //}

		private void SetObjectModel(Type objectModelDefinitionType)
		{
			if (objectModelDefinitionType == null)
				throw new ArgumentNullException("modelClassType is null.");
			
			if (!typeof(IEasyObjectModel).IsAssignableFrom(objectModelDefinitionType))
				throw new Exception("Method SetObjectModel: modelClassType must have IEasyObjectModel interface implemented.");

			if (this.objectModelsByObjectModelDefinitionType.ContainsKey(objectModelDefinitionType))
				return;
			
			IEasyObjectModel objectModel = Activator.CreateInstance(objectModelDefinitionType) as IEasyObjectModel;

			//if (objectModelDefinitionType == typeof(EasyObjectModel))
			//	(objectModel as EasyObjectModel).SetInstanceInternal(objectModel as EasyObjectModel);
			
			//if (!(model is EmptyObjectModel) && model.ObjectPropertys != null)


			//PropertyInfo propertyInfo = objectModelDefinitionType.GetProperty(ReflectionHelper.StrInstance);

			//if (propertyInfo != null)
			//{
			//	object instance = propertyInfo.GetValue(objectModelDefinitionType, null);

			//	if (instance != null)
			//	{
			//		objectModel = instance as IEasyObjectModel;
			//	}
			//	else
			//	{
			//		throw new Exception(String.Format("Class {0} with Object Reletion Model definition has no static Instance property", objectModelDefinitionType.Name));
			//	}
			//}

			foreach (var propertyModel in objectModel.PropertyModels)
				if (propertyModel is PropertyModel)
					(propertyModel as PropertyModel).Owner = objectModel;

		//	this.SetObjectModelDefinition(objectModel, objectModel.ObjectType);
		//}

		//protected internal void SetObjectModelDefinition(IEasyObjectModel model, Type objectType)
		//{
			this.objectModelsByObjectModelDefinitionType[objectModel.GetType()] = objectModel;
			this.objectModelsByObjectType[objectModel.ObjectType] = objectModel;
			this.OnSetObjectModel(objectModel, objectModel.ObjectType);
		}

		protected virtual void OnSetObjectModel(IEasyObjectModel model, Type objectType)
		{
		}

		//protected void SetPropertyDictionary(EasyObject easyObject, IDictionary<string, object> propertyValueData)
		//{
		//	easyObject.SetPropertyDictionary(propertyValueData);
		//}

		//protected void SetPropertyValueInternal(EasyObject easyObject, string propertyName, object value, bool firePropertyValueChangeEvent, bool addOrRemoveInChangedPropertyNames, object requester)
		//{
		//	easyObject.SetPropertyValueInternal(propertyName, value, firePropertyValueChangeEvent, addOrRemoveInChangedPropertyNames, requester);
		//}

        //protected virtual void OnObjectAction(object requester, EasyObject easyObject, EasyObjectAction action)
        //{
        //}

        protected virtual void OnObjectCreated(object requester, EasyObject easyObject)
        {
            foreach (EasyObjectDependencyAction objectDependencyAction in this.ObjectDependencyActions)
            {
                if (objectDependencyAction.Match(easyObject))
                {
                    objectDependencyAction.OnObjectCreated(easyObject); 
                }
            }
        }

		//protected virtual void OnLoad(object requester, EasyObject easyObject)
		//{
		//}

		//protected virtual void OnBeforeLoading(object requester, EasyObject easyObject)
		//{
		//}

		//protected virtual void OnAfterLoad(object requester, EasyObject easyObject)
		//{
		//}

        protected virtual void OnBeforeSaving(object requester, EasyObject easyObject)
        {
        }

        protected virtual void OnSaving(object requester, EasyObject easyObject)
        {
            foreach (EasyObjectDependencyAction objectDependencyAction in this.ObjectDependencyActions)
            {
                if (objectDependencyAction.Match(easyObject))
                {
                    objectDependencyAction.OnSaving(easyObject);
                }
            }
        }

        protected virtual void OnAfterSave(object requester, EasyObject easyObject, bool isNewBeforeSaving)
        {
        }

        protected virtual void OnDeleting(object requester, EasyObject easyObject)
        {
        }

        protected virtual void OnBeforeDeleting(object requester, EasyObject easyObject)
        {
            foreach (EasyObjectDependencyAction objectDependencyAction in this.ObjectDependencyActions)
            {
                if (objectDependencyAction.Match(easyObject))
                {
                    objectDependencyAction.OnBeforeDelete(easyObject);
                }
            }
        }

        protected virtual void OnAfterDelete(object requester, EasyObject easyObject)
        {
			//foreach (EasyObjectDependencyAction objectDependencyAction in this.ObjectDependencyActions)
			//{
			//	if (objectDependencyAction.Match(easyObject))
			//	{
			//		objectDependencyAction.OnAfterDelete(easyObject);
			//	}
			//}
		}

        protected virtual void OnBeforePropertyValueChange(object requester, EasyObject easyObject, int propertyIndex, object value, object newValue)
        {
        }

        protected virtual void OnPropertyValueChange(object requester, EasyObject easyObject, int propertyIndex, object value, object oldValue)
        {
            foreach (EasyObjectDependencyAction objectDependencyAction in this.ObjectDependencyActions)
            {
				//for (int i = 0; i < 2; i++)
				//{
                    if (objectDependencyAction.Match(easyObject))
                        objectDependencyAction.OnPropertyValueChange(easyObject, propertyIndex, value, oldValue);
				//}
            }
        }

        protected virtual void OnChangedPropertyNamesCountChange(EasyObject easyObject, int propertyNamesCount, int oldPropertyNamesCount)
        {
        }

        protected virtual void OnImageNameChange(EasyObject easyObject, string imageName, string oldImageName)
        {
        }
        
        //private object GetNormalizedPropertyValueFromDatastoreData(int propertyIndex, object datastoreData)
        //{
        //    IObjectProperty propertyModel = this.GetObjectModel(propertyIndex).ObjectPropertys[propertyIndex];
        //    return this.GetNormalizedPropertyValueFromDatastoreData(propertyModel, datastoreData);
        //}

        protected internal object? GetNormalizedPropertyValueFromDatastoreData(IPropertyModel propertyModel, object datastoreData)
        {
            //IObjectProperty propertyModel = this.GetModel().ObjectPropertys[propertyIndex];
            object? result = datastoreData;

            //lock (lockObject)
            //{
            if (result == null || result.GetType() == typeof(System.DBNull))
            {
                result = propertyModel.PropertyType.GetDefaultValue();
            }
            else if (propertyModel.PropertyType != propertyModel.DatastoreType)
            {
                if (propertyModel.IsEncrypted)
                    result = PasswordSecurity.Decrypt(datastoreData.ToString(), this.Decryptor);

                result = Conversion.TryChangeType(result, propertyModel.PropertyType);
            }
            //if (propertyModel.DatastoreFieldType != DatastoreFieldType.Default)
            //{
            //	result = Conversion.TryChangeType(result, propertyModel.PropertyType);
            //}
            //}

            return result;
        }

        protected internal object? CreateNormalizedPropertyDataToSaveInDatastore(IPropertyModel propertyModel, object propertyData)
        {
            //         //object propertyData = this.GetFieldValue(propertyModel.Index);
            object? result = propertyData;

            //if (result != null && propertyModel.IsEncrypted)
            //	result = PasswordSecurity.EncryptPassword(propertyData.ToString(), this.ObjectManager.Encryptor, this.ObjectManager.CryptoBlockSize);

            if (result != null)
            {
                if (propertyModel.PropertyType != propertyModel.DatastoreType)
                    result = Conversion.TryChangeType(result, propertyModel.DatastoreType);

                if (propertyModel.IsEncrypted)
                    result = PasswordSecurity.Encrypt(result!.ToString(), this.Encryptor); //, this.CryptoBlockSize);
            }

            return result;

            //if (propertyModel.DatastoreFieldType != DatastoreFieldType.Default)
            //{
            //	if (propertyModel.DatastoreFieldType == DatastoreFieldType.Byte)
            //	{
            //		result = Conversion.TryChangeType<byte>(result);
            //	}
            //	else if (propertyModel.DatastoreFieldType == DatastoreFieldType.Short)
            //	{
            //		result = Conversion.TryChangeType<short>(result);
            //	}
            //	else if (propertyModel.DatastoreFieldType == DatastoreFieldType.Int)
            //	{
            //		result = Conversion.TryChangeType<int>(result);
            //	}
            //	else if (propertyModel.DatastoreFieldType == DatastoreFieldType.Long)
            //	{
            //		result = Conversion.TryChangeType<long>(result);
            //	}
            //	else if (propertyModel.DatastoreFieldType == DatastoreFieldType.Guid)
            //	{
            //		result = Conversion.TryChangeType<Guid>(result);
            //	}
            //	else if (propertyModel.DatastoreFieldType == DatastoreFieldType.Decimal)
            //	{
            //		result = Conversion.TryChangeType<decimal>(result);
            //	}
            //	else if (propertyModel.DatastoreFieldType == DatastoreFieldType.Float)
            //	{
            //		result = Conversion.TryChangeType<float>(result);
            //	}
            //	else if (propertyModel.DatastoreFieldType == DatastoreFieldType.DateTime)
            //	{
            //		result = Conversion.TryChangeType<DateTime>(result);
            //	}
            //}

            //return result;
        }

        #endregion |   Protected Methods   |

        #region |   Internal Methods   |

        //internal IEasyObjectModel GetObjectModelByModelDefinitionType(Type modelDefinitionType)
        //{
        //	IEasyObjectModel result = null;

        //	this.objectModelsByObjectType.TryGetValue(modelDefinitionType, out result);
        //	return result;
        //}

        #endregion

        #region |   Protected Raise Event Methods   |

        protected virtual void RaiseObjectCreated(object requester, EasyObject easyObject)
        {
            if (this.ObjectCreated != null)
                this.ObjectCreated(this, new EasyObjectRequesterEventArgs(requester, easyObject));
        }

		//protected virtual void RaiseBeforeLoading(object requester, EasyObject easyObject)
		//{
		//	if (this.BeforeLoading != null)
		//	{
		//		this.BeforeLoading(this, new EasyObjectRequesterEventArgs(requester, easyObject));
		//	}
		//}

		//protected virtual void RaiseAfterLoad(object requester, EasyObject easyObject)
		//{
		//	if (this.AfterLoad != null)
		//	{
		//		this.AfterLoad(this, new EasyObjectRequesterEventArgs(requester, easyObject));
		//	}
		//}

        protected virtual void RaiseBeforeSaving(object requester, EasyObject easyObject)
        {
            if (this.BeforeSaving != null)
                this.BeforeSaving(this, new EasyObjectRequesterEventArgs(requester, easyObject));
        }

        protected virtual void RaiseSaving(object requester, EasyObject easyObject)
        {
            if (this.Saving != null)
                this.Saving(this, new EasyObjectRequesterEventArgs(requester, easyObject));
        }

        protected virtual void RaiseAfterSave(object requester, EasyObject easyObject, bool isNewBeforeSaving)
        {
            if (this.AfterSave != null)
                this.AfterSave(this, new AfterSaveEasyObjectRequesterEventArgs(requester, easyObject, isNewBeforeSaving));
        }

        protected virtual void RaiseBeforeDeleting(object requester, EasyObject easyObject)
        {
            if (this.BeforeDeleting != null)
                this.BeforeDeleting(this, new EasyObjectRequesterEventArgs(requester, easyObject));
        }

        protected virtual void RaiseAfterDelete(object requester, EasyObject easyObject)
        {
            if (this.AfterDelete != null)
                this.AfterDelete(this, new EasyObjectRequesterEventArgs(requester, easyObject));
        }

        protected virtual void RaiseBeforePropertyValueChange(object requester, EasyObject easyObject, int propertyIndex, object value, object newValue)
        {
            if (this.BeforePropertyValueChange != null)
                this.BeforePropertyValueChange(this, new BeforeChangePropertyValueEasyObjectRequesterEventArgs(requester, easyObject, propertyIndex, value, newValue));
        }

        protected virtual void RaisePropertyValueChange(object requester, EasyObject easyObject, int propertyIndex, object value, object oldValue)
        {
            if (this.PropertyValueChange != null)
                this.PropertyValueChange(this, new ChangePropertyValueEasyObjectRequesterEventArgs(requester, easyObject, propertyIndex, value, oldValue));
        }

        protected virtual void RaiseChangedPropertiesCountChange(EasyObject easyObject, int changedPropertiesCount, int oldChangedPropertiesCount)
        {
            if (this.ChangedPropertiesCountChange != null)
                this.ChangedPropertiesCountChange(this, new CountChangeEasyObjectEventArgs(easyObject, changedPropertiesCount, oldChangedPropertiesCount));
        }

        protected virtual void RaiseRequireSavingChange(EasyObject easyObject, bool requireSaving)
        {
            if (this.RequireSavingChange != null)
                this.RequireSavingChange(this, new RequireSavingChangeEasyObjectEventArgs(easyObject, requireSaving));
        }

        protected virtual void RaiseImageNameChange(EasyObject easyObject, string imageName, string oldImageName)
        {
            if (this.ImageNameChange != null)
                this.ImageNameChange(this, new ImageNameChangeEasyObjectEventArgs(easyObject, imageName, oldImageName));
        }

		protected virtual void RaiseMultipleImageNameChange(List<ImageNameChangeEasyObjectEventArgs> imageNameChangeEasyObjectEventArgsList)
		{
			if (this.MultipleImageNameChange != null)
				this.MultipleImageNameChange(this, imageNameChangeEasyObjectEventArgsList);
		}

        #endregion |   Protected Raise Event Methods   |
    }

    #region |   Delegates   |

    public delegate void EasyObjectRequesterEventHandler(object sender, EasyObjectRequesterEventArgs e);
    public delegate void AfterSaveEasyObjectRequesterEventHandler(object sender, AfterSaveEasyObjectRequesterEventArgs e);
    public delegate void PropertyValueRequesterEasyObjectEventHandler(object sender, PropertyValueEasyObjectRequsterEventArgs e);
    public delegate void ChangePropertyValueEasyObjectRequesterEventHandler(object sender, ChangePropertyValueEasyObjectRequesterEventArgs e);
    public delegate void BeforeChangePropertyValueEasyObjectRequesterEventHandler(object sender, BeforeChangePropertyValueEasyObjectRequesterEventArgs e);
    public delegate void CountChangeEasyObject(object sender, CountChangeEasyObjectEventArgs e);
    public delegate void RequireSavingChangeEasyObject(object sender, RequireSavingChangeEasyObjectEventArgs e);
    public delegate void ImageNameChangeEasyObject(object sender, ImageNameChangeEasyObjectEventArgs e);
	public delegate void MultipleImageNameChangeEasyObject(object sender, List<ImageNameChangeEasyObjectEventArgs> e);

    #endregion |   Delegates   |

    #region |   EventArgs Classes   |

	//public class RequesterEventArgs : EventArgs
 //   {
 //       public RequesterEventArgs(object? requester)
 //       {
 //           this.Requester = requester;
 //       }

 //       public object? Requester { get; private set; }
 //   }

    public class EasyObjectEventArgs : EventArgs
    {
        public EasyObjectEventArgs(EasyObject easyObject)
	    {
            this.EasyObject = easyObject;
	    }

        public EasyObject EasyObject { get; private set; }
    }

    public class EasyObjectRequesterEventArgs : EventArgs
    {
        public EasyObjectRequesterEventArgs(object requester, EasyObject easyObject)
        {
            this.Requester = requester;
            this.EasyObject = easyObject;
        }

        public object Requester { get; private set; }
        public EasyObject EasyObject { get; private set; }
    }

    public class AfterSaveEasyObjectRequesterEventArgs : EasyObjectRequesterEventArgs
    {
        public AfterSaveEasyObjectRequesterEventArgs(object requester, EasyObject easyObject, bool isNewBeforeSaving)
            : base(requester, easyObject)
	    {
            this.IsNewBeforeSaving = isNewBeforeSaving;
	    }

        public bool IsNewBeforeSaving { get; private set; }
    }

    public class PropertyEasyObjectRequsterEventArgs : EasyObjectRequesterEventArgs
    {
        public PropertyEasyObjectRequsterEventArgs(object requester, EasyObject easyObject, int propertyIndex)
            : base(requester, easyObject)
        {
            this.PropertyIndex = propertyIndex;
        }

        public int PropertyIndex { get; private set; }
    }

    public class PropertyValueEasyObjectRequsterEventArgs : PropertyEasyObjectRequsterEventArgs
    {
        public PropertyValueEasyObjectRequsterEventArgs(object requester, EasyObject easyObject, int propertyIndex, object propertyValue)
            : base(requester, easyObject, propertyIndex)
        {
            this.Value = propertyValue;
        }

        public object Value { get; private set; }
    }

    public class ChangePropertyValueEasyObjectRequesterEventArgs : PropertyValueEasyObjectRequsterEventArgs
    {
        public ChangePropertyValueEasyObjectRequesterEventArgs(object requester, EasyObject easyObject, int propertyIndex, object value, object oldValue)
            : base(requester, easyObject, propertyIndex, value)
        {
            this.OldValue = oldValue;
        }

        public object OldValue { get; private set; }
    }

    public class BeforeChangePropertyValueEasyObjectRequesterEventArgs : PropertyValueEasyObjectRequsterEventArgs
    {
        public BeforeChangePropertyValueEasyObjectRequesterEventArgs(object requester, EasyObject easyObject, int propertyIndex, object value, object newValue)
            : base(requester, easyObject, propertyIndex, value)
        {
            this.NewValue = newValue;
        }

        public object NewValue { get; private set; }
    }

    public class CountChangeEasyObjectEventArgs : EasyObjectEventArgs
    {
        public CountChangeEasyObjectEventArgs(EasyObject easyObject, int count, int oldCount)
            : base(easyObject)
        {
            this.Count = count;
            this.OldCount = oldCount;
        }

        public int Count { get; private set; }
        public int OldCount { get; private set; }
    }

    public class RequireSavingChangeEasyObjectEventArgs : EasyObjectEventArgs
    {
        public RequireSavingChangeEasyObjectEventArgs(EasyObject easyObject, bool requireSaving)
            : base(easyObject)
        {
            this.RequireSaving = requireSaving;
        }

        public bool RequireSaving { get; private set; }
    }

    public class ImageNameChangeEasyObjectEventArgs : EasyObjectEventArgs
    {
        public ImageNameChangeEasyObjectEventArgs(EasyObject easyObject, string imageName, string oldImageName)
            : base(easyObject)
        {
            this.ImageName = imageName;
            this.OldImageName = imageName;
        }
        
        public string ImageName { get; private set; }
        public string OldImageName { get; private set; }
    }

    #endregion |   EventArgs Classes   |
}
