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

namespace Simple.Objects
{
    partial class SimpleObjectManager
    {
        #region |   Private Members   |

        protected readonly object lockObject = new object();
		private bool enforcePropertyExistanceInModel = true;

        #endregion |   Private Members   |

		#region |   Protected Members   |

		//protected SimpleDictionary<Type, ISimpleObjectModel> objectModelsByObjectType = new SimpleDictionary<Type, ISimpleObjectModel>();   // Key is ObjectType as Type, Value is model of the object, tipically as SimpleObjectModel (but is not necessary).
		//protected SimpleDictionary<Type, ISimpleObjectModel> objectModelsByObjectModelDefinitionType = new SimpleDictionary<Type, ISimpleObjectModel>();
		//protected List<Type> notIncludedObjectModelDefinitionTypes = new List<Type>();
		//protected List<SimpleObjectDependencyAction> objectDependencyActions = new List<SimpleObjectDependencyAction>();

		#endregion |   Protected Members   |

		#region |   Constructors and Initialization   |

		//public SimpleObjectManager()
  //      {
		//	////// Field based model definition
		//	////IDictionary<string, ISimpleObjectModel> models = ReflectionHelper.GetFieldsByReflection<ISimpleObjectModel>(this);
		//	////this.SetObjectModelDefinition(models.Values);

		//	//// Discovery assembly model definitions
		//	////List<Type> reversedObjectModels = new List<Type>();

		//	////// Some computer Net.Framework assemblies has an problem when calling assembly.GetTypes() that result in app crash at start.
		//	////// To awoid app chrash, we need alternative method to get all ISimpleObjectModel object models class types.
		//	//////
		//	////var objectModels = from assembly in AppDomain.CurrentDomain.GetAssemblies()
		//	////				   from type in assembly.GetTypes()
		//	////				   where typeof(ISimpleObjectModel).IsAssignableFrom(type) && !type.IsInterface && !type.IsGenericType && !type.IsAbstract
		//	////				   select type;

		//	////// The problem is here....
		//	////foreach (Type type in objectModels)
		//	////	reversedObjectModels.Add(type);


		//	//List<Type> objectModelTypes = ReflectionHelper.GetInheritedTypesInAssembly(typeof(ISimpleObjectModel));

		//	////List<Type> reversedObjectModels = new List<Type>();
		//	////List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

		//	////foreach (Assembly assembly in assemblies)
		//	////{
		//	////	Type[] types = null;

		//	////	try
		//	////	{
		//	////		types = assembly.GetTypes();
		//	////	}
		//	////	catch
		//	////	{
		//	////		continue;
		//	////	}

		//	////	if (types != null)
		//	////		foreach (Type type in types)
		//	////			if (type != null && typeof(ISimpleObjectModel).IsAssignableFrom(type) && !type.IsInterface && !type.IsGenericType && !type.IsAbstract)
		//	////				reversedObjectModels.Add(type);
		//	////}

		//	//for (int i = objectModelTypes.Count - 1; i >= 0; i--)
		//	//{
		//	//	ISimpleObjectModel model = Activator.CreateInstance(objectModelTypes[i]) as ISimpleObjectModel;
		//	//	this.SetObjectModelDefinition(model, model.ObjectType);
		//	//}
		//}

		//protected void CreateObjectModels()
		//{
		//	List<Type> reducedObjectModelDefinitionTypes = new List<Type>();
		//	List<Type> systemModelDefinitionTypes = new List<Type>();
		//	List<Type> appModelDefinitionTypes = new List<Type>();
		//	List<Type> objectModelDefinitionTypes = ReflectionHelper.GetInheritedTypesInAssembly(typeof(ISimpleObjectModel));

		//	// Get only top inherited classes
		//	foreach (Type type in objectModelDefinitionTypes)
		//	{
		//		Type topInheritedType = ReflectionHelper.FindTopInheritedClass(type, objectModelDefinitionTypes);

		//		if (!reducedObjectModelDefinitionTypes.Contains(topInheritedType))
		//		{
		//			reducedObjectModelDefinitionTypes.Add(topInheritedType);
		//		}
		//		else
		//		{
		//			this.notIncludedObjectModelDefinitionTypes.Add(type);
		//		}
		//	}

		//	// Split the models into two groups: system and app
		//	foreach (Type type in reducedObjectModelDefinitionTypes)
		//	{
		//		if (this.IsObjectModelClassSystemAssembly(type))
		//		{
		//			systemModelDefinitionTypes.Add(type);
		//		}
		//		else
		//		{
		//			appModelDefinitionTypes.Add(type);
		//		}
		//	}

		//	// Set Base model definition classes first
		//	notIncludedObjectModelDefinitionTypes.Reverse();

		//	foreach (Type objectModelDefinitionType in notIncludedObjectModelDefinitionTypes)
		//		this.SetObjectModel(objectModelDefinitionType);

		//	// Than add system object models, 
		//	systemModelDefinitionTypes.Reverse();

		//	foreach (Type objectModelDefinitionType in systemModelDefinitionTypes)
		//		this.SetObjectModel(objectModelDefinitionType);

		//	// Add an application models at the end to be the last
		//	appModelDefinitionTypes.Reverse();

		//	foreach (Type objectModelDefinitionType in appModelDefinitionTypes)
		//		this.SetObjectModel(objectModelDefinitionType);
		//}

		//protected virtual void ProcessNotIncludedObjectModelTypes(List<Type> notIncludedObjectModelTypes)
		//{
		//}

		//protected virtual bool IsObjectModelClassSystemAssembly(Type objectModelType)
		//{
		//	return objectModelType.Assembly == Assembly.GetExecutingAssembly();
		//}

		//public static SimpleObjectManager Instance
		//{
		//	get { return GetInstance<SimpleObjectManager>(); }
		//}
		
		//protected static T GetInstance<T>() where T : SimpleObjectManager, new()
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

        //public event ActionRequesterSimpleObjectEventHandler ObjectAction;

        public event SimpleObjectRequesterEventHandler NewObjectCreated;

        public event SimpleObjectRequesterEventHandler BeforeSave;
        //public event SimpleObjectRequesterEventHandler Saving;
        public event AfterSaveSimpleObjectRequesterEventHandler AfterSave;

		//public event SimpleObjectRequesterEventHandler BeforeLoading;
		//public event SimpleObjectRequesterEventHandler AfterLoad;

        public event SimpleObjectChangeContainerRequesterEventHandler DeleteRequested;
        public event SimpleObjectChangeContainerRequesterEventHandler DeleteRequestCancelled;


        public event SimpleObjectRequesterEventHandler BeforeDelete;
        public event SimpleObjectRequesterEventHandler AfterDelete;

        public event BeforeChangePropertyValueSimpleObjectRequesterEventHandler BeforePropertyValueChange;
        public event ChangePropertyValueSimpleObjectRequesterEventHandler PropertyValueChange;
        public event ChangePropertyValueSimpleObjectRequesterEventHandler SaveablePropertyValueChange;

        public event CountChangeSimpleObjectEventHandler ChangedPropertiesCountChange;
        public event CountChangeSimpleObjectEventHandler ChangedSaveablePropertiesCountChange;
        public event RequireSavingChangeSimpleObjectEventHandler RequireSavingChange;
        public event RequireCommitChangeEventHandler RequireCommitChange;

        //public event RequireCommitChangeEventHandler RequireCommitChange;

        public event ImageNameChangeSimpleObjectEventHandler ImageNameChange;
		public event MultipleImageNameChangeSimpleObjectEventHandler MultipleImageNameChange;

        #endregion |   Events   |

        #region |   Public Properties   |

        public ICryptoTransform Encryptor { get; protected set; }
        public ICryptoTransform Decryptor { get; protected set; }
        //public int CryptoBlockSize { get; protected set; }
        
        public bool EnforcePropertyExistanceInModel
        {
            get { return this.enforcePropertyExistanceInModel; }
            set { this.enforcePropertyExistanceInModel = value; }
        }

        #endregion |   Public Properties   |

        #region |   Property Encryption/Decryption   |

        protected internal object? EncryptProperty(object? propertyValue)
        {
            if (propertyValue == null)
                return null;

            return PasswordSecurity.Encrypt(propertyValue.ToString(), this.Encryptor); //, this.CryptoBlockSize);
        }

        protected internal object? DecryptProperty(object propertyValue)
        {
			if (propertyValue == null)
				return null;

            return PasswordSecurity.Decrypt(propertyValue.ToString(), this.Decryptor); //, this.CryptoBlockSize);
        }

        #endregion |   Property Encryption/Decryption   |

        #region |   Protected Properties   |

        #endregion |   Protected Properties   |

        #region |   Public Methods   |

        //public void Load(object requester, SimpleObject simpleObject)
        //{
        //	//lock (this.lockObjectAction)
        //	//{
        //	simpleObject.ActionBeforeLoading(requester);
        //	this.OnBeforeLoading(requester, simpleObject);
        //	this.RaiseBeforeLoading(requester, simpleObject);
        //	//this.RaiseObjectAction(requester, simpleObject, SimpleObjectAction.BeforeLoading);

        //	this.OnLoad(requester, simpleObject);

        //	simpleObject.ActionAfterLoad(requester);
        //	this.OnAfterLoad(requester, simpleObject);
        //	this.RaiseAfterLoad(requester, simpleObject);
        //	//this.RaiseObjectAction(requester, simpleObject, SimpleObjectAction.AfterLoad);
        //	//}
        //}

        //public void Load(object requester, SimpleObject simpleObject, IDictionary<string, object> propertyDictionary, bool isNew)
        //{
        //	//lock (this.lockObjectAction)
        //	//{
        //	simpleObject.ActionBeforeLoading(requester);
        //	this.OnBeforeLoading(requester, simpleObject);
        //	this.RaiseBeforeLoading(requester, simpleObject);
        //	//this.RaiseObjectAction(requester, simpleObject, SimpleObjectAction.BeforeLoading);

        //	simpleObject.SetPropertyDictionary(propertyDictionary);
        //	simpleObject.IsNew = isNew;
        //	simpleObject.AcceptChanges(requester);

        //	this.OnLoad(requester, simpleObject);

        //	simpleObject.ActionAfterLoad(requester);
        //	this.OnAfterLoad(requester, simpleObject);
        //	this.RaiseAfterLoad(requester, simpleObject);
        //	//this.RaiseObjectAction(requester, simpleObject, SimpleObjectAction.AfterLoad);
        //	//}
        //}

        //public virtual bool Save(object requester, SimpleObject simpleObject)
        //{
        //    return this.SaveInternal(requester, simpleObject);
        //}

        //     protected virtual bool SaveInternalBase(SimpleObject simpleObject, object requester)
        //     {
        //         if (simpleObject.ChangedPropertiesCount > 0)
        //         {
        //             //lock (this.lockObjectAction)
        //             //{
        //                 simpleObject.ActionBeforeSaving(requester);
        //                 this.OnBeforeSaving(simpleObject, requester);
        //                 this.RaiseBeforeSaving(simpleObject, requester);

        //                 bool isNewBeforeSaving = simpleObject.IsNew;

        //                 simpleObject.ActionOnSaving(requester);

        //		this.OnSaving(simpleObject, requester);
        //                 this.RaiseSaving(simpleObject, requester);

        //                 //if (simpleObject.IsNew)
        //                 //    simpleObject.IsNew = false;

        //                 //simpleObject.AcceptChanges(requester); -> AcceptChanges will be applied on EndTransaction

        //                 simpleObject.ActionAfterSave(isNewBeforeSaving, requester);
        //                 this.OnAfterSave(simpleObject, isNewBeforeSaving, requester);
        //                 this.RaiseAfterSave(simpleObject, isNewBeforeSaving, requester);
        //             //}
        //         }

        //this.HandleRelatedTransactionRequests(simpleObject, requester);
        //simpleObject.ClearRelatedTransactionRequests();

        //return true;
        //     }

        //public virtual bool CanDelete(SimpleObject simpleObject)
        //{
        //    return true;
        //}

        //public virtual bool Delete(object requester, SimpleObject simpleObject)
        //{
        //    bool isDeleted;

        //    if (isDeleted = this.CanDelete(simpleObject))
        //    {
        //        this.DeleteInternal(requester, simpleObject);
        //    }

        //    return isDeleted;
        //}


        //public virtual SimpleObjectValidationResult Validate(SimpleObject simpleObject)
        //{
        //    return null;
        //}

        public void AcceptChanges(SimpleObject simpleObject, ChangeContainer changeContainer, object requester)
        {
            simpleObject.AcceptChanges(changeContainer, requester);
        }

        public void RejectChanges(SimpleObject simpleObject, ChangeContainer changeContainer, object requester)
        {
            simpleObject.RejectChanges(changeContainer, requester);
        }

        //public ISimpleObjectModel GetObjectModel(Type objectType)
        //{
        //    return this.objectModelsByObjectType[objectType] as ISimpleObjectModel;
        //}

   //     public virtual string RecalcImageName(SimpleObject simpleObject)
   //     {
			//string imageName = String.Empty;
			
			//if (simpleObject != null)
   //         {
   //             string oldImageName = simpleObject.GetImageNameInternal();
                    
   //             imageName = (simpleObject is GraphElement) ? (simpleObject as GraphElement).SimpleObject?.GetImageName() : this.GenerateImageName(simpleObject);
   //             this.SetImageName(simpleObject, imageName, oldImageName);
   //         }

   //         return imageName;
   //     }


        //public virtual int GetImageIndex(SimpleObject simpleObject)
        //{
        //    if (simpleObject.imageInfo == null)
        //        simpleObject.imageInfo = this.CreateImageInfo();

        //    return simpleObject.imageInfo.ImageIndex;
        //}

        #endregion |   Public Methods   |

        #region |   Protected Internal Methods   |

        protected internal void ImageNameIsChanged(SimpleObject simpleObject, string? imageName, string? oldImageName)
        {
            if (!oldImageName.IsNullOrEmpty()) // String.Empty is default value, and noo need to fire event when first time gets the image name
            {
                this.RaiseImageNameChange(simpleObject, imageName, oldImageName);
                this.OnImageNameChange(simpleObject, imageName, oldImageName);
            }
        }

        protected internal virtual void NewObjectIsCreated(SimpleObject simpleObject, object? requester)
        {
            //if (this.WorkingMode != ObjectManagerWorkingMode.Server)
            //{
            simpleObject.NewObjectIsCreated(requester);
            this.RaiseNewObjectCreated(simpleObject, requester);
            this.OnNewObjectCreated(simpleObject, requester);
            //}
        }

        protected internal virtual void BeforePropertyValueIsChanged(SimpleObject simpleObject, IPropertyModel propertyModel, object value, object newValue, bool willBeChanged, object? requester)
        {
            if (this.WorkingMode != ObjectManagerWorkingMode.Server)
            {
                this.RaiseBeforePropertyValueChange(simpleObject, propertyModel, value, newValue, willBeChanged, requester);
                this.OnBeforePropertyValueChange(simpleObject, propertyModel, value, newValue, willBeChanged, requester);
            }
        }

        protected internal virtual void PropertyValueIsChanged(SimpleObject simpleObject, IPropertyModel propertyModel, object value, object oldValue, bool isChanged, bool isSaveable, ChangeContainer? changeContainer, object? requester)
        {
            if (this.WorkingMode != ObjectManagerWorkingMode.Server)
            {
                this.RaisePropertyValueChange(simpleObject, propertyModel, value, oldValue, isChanged, isSaveable, requester);
                this.OnPropertyValueChange(simpleObject, propertyModel, value, oldValue, isChanged, isSaveable, changeContainer, requester);
            }
        }

        protected internal virtual void SaveablePropertyValueIsChanged(SimpleObject simpleObject, IPropertyModel propertyModel, object value, object oldValue, bool isChanged, ChangeContainer? changeContainer, object? requester)
        {
            if (this.WorkingMode != ObjectManagerWorkingMode.Server)
            {
                this.RaiseSaveablePropertyValueChange(simpleObject, propertyModel, value, oldValue, isChanged, requester);
                this.OnSaveablePropertyValueChange(simpleObject, propertyModel, value, oldValue, isChanged, changeContainer, requester);
            }
        }

        protected internal virtual void ChangedPropertiesCountIsChanged(SimpleObject simpleObject, int changedPropertiesCount, int oldChangedPropertiesCount, ChangeContainer? changeContainer, object? requester)
        {
            if (this.WorkingMode != ObjectManagerWorkingMode.Server)
            {
                this.RaiseChangedPropertiesCountChange(simpleObject, changedPropertiesCount, oldChangedPropertiesCount);
                this.OnChangedPropertiesCountChange(simpleObject, changedPropertiesCount, oldChangedPropertiesCount);
            }

    //        if (oldChangedPropertiesCount == 0) // && simpleObject.RelatedTransactionRequests.Count == 0)
    //        {
				////if (changeContainer != null)
    ////                changeContainer.Set(simpleObject, TransactionRequestAction.Save, requester);

    //            //if (this.WorkingMode != ObjectManagerWorkingMode.Server)
    //            //    this.RaiseRequireSavingChange(simpleObject, requireSaving: true);
    //        }
    //        else if (changedPropertiesCount == 0) // && simpleObject.RelatedTransactionRequests.Count == 0)
    //        {
    //            //if (changeContainer != null)
    //            //    changeContainer.Unset(simpleObject, TransactionRequestAction.Save, requester);

    //            //if (this.WorkingMode != ObjectManagerWorkingMode.Server)
    //            //    this.RaiseRequireSavingChange(simpleObject, requireSaving: false);
    //        }
        }

        protected internal virtual void ChangedSaveablePropertiesCountIsChanged(SimpleObject simpleObject, int changedSaveablePropertiesCount, int oldChangedSaveablePropertiesCount, ChangeContainer? changeContainer, object? requester)
        {
            //if (this.WorkingMode != ObjectManagerWorkingMode.Server)
            //{
                this.RaiseChangedSaveablePropertiesCountChange(simpleObject, changedSaveablePropertiesCount, oldChangedSaveablePropertiesCount);
                this.OnChangedSaveablePropertiesCountChange(simpleObject, changedSaveablePropertiesCount, oldChangedSaveablePropertiesCount);
            //}

            if (oldChangedSaveablePropertiesCount == 0) // && simpleObject.RelatedTransactionRequests.Count == 0)
            {
                if (changeContainer != null)
                    changeContainer.Set(simpleObject, TransactionRequestAction.Save, requester);
                //if (changeContainer != null)
                //    changeContainer.RequireSavingChangeCount(requireSaving: true);

                //if (this.WorkingMode != ObjectManagerWorkingMode.Server)
                this.RaiseRequireSavingChange(simpleObject, requireSaving: true);
            }
            else if (changedSaveablePropertiesCount == 0) // && simpleObject.RelatedTransactionRequests.Count == 0)
            {
                if (changeContainer != null && !simpleObject.IsNew)
                    changeContainer.Unset(simpleObject, TransactionRequestAction.Save, requester);
                //if (changeContainer != null)
                //    changeContainer.RequireSavingChangeCount(requireSaving: false);

                //TransactionRequestAction requstAction;

                //if (changeContainer != null && changeContainer.TransactionRequests.TryGetValue(simpleObject, out requstAction))
                //    if (requstAction == TransactionRequestAction.Save)
                //        changeContainer.Remove(simpleObject);

                //if (this.WorkingMode != ObjectManagerWorkingMode.Server)
                this.RaiseRequireSavingChange(simpleObject, requireSaving: false);
            }
        }

        #endregion |   Protected Internal Methods   |

        #region |   Protected Methods   |

        //protected T GetObjectModelDefinition<T>(Type objectType) where T : ISimpleObjectModel
        //{
        //    T value = default(T);

        //    if (this.objectModelHashtable.ContainsKey(objectType))
        //    {
        //        value = (T)this.objectModelHashtable[objectType];
        //    }

        //    return value;
        //}

        //protected void SetObjectModelDefinition(IEnumerable<ISimpleObjectModel> modelList)
        //{
        //    foreach (ISimpleObjectModel model in modelList)
        //    {
        //        this.SetObjectModelDefinition(model);
        //    }
        //}

        //protected virtual string GenerateImageName(SimpleObject simpleObject)
        //{
        //    return this.GetObjectModel(simpleObject.GetType()).ImageModel.ImageName;
        //}

        //protected virtual void RecalculateImageName(SimpleObject simpleObject)
        //{
        //    this.GetImageName(simpleObject);
        //}

        //protected virtual ImageInfo CreateImageInfo()
        //{
        //    return new ImageInfo();
        //}


		//protected void SetPropertyDictionary(SimpleObject simpleObject, IDictionary<string, object> propertyValueData)
		//{
		//	simpleObject.SetPropertyDictionary(propertyValueData);
		//}

		//protected void SetPropertyValueInternal(SimpleObject simpleObject, string propertyName, object value, bool firePropertyValueChangeEvent, bool addOrRemoveInChangedPropertyNames, object requester)
		//{
		//	simpleObject.SetPropertyValueInternal(propertyName, value, firePropertyValueChangeEvent, addOrRemoveInChangedPropertyNames, requester);
		//}

        //protected virtual void OnObjectAction(object requester, SimpleObject simpleObject, SimpleObjectAction action)
        //{
        //}

        protected virtual void OnNewObjectCreated(SimpleObject simpleObject, object requester)
        {
            //foreach (SimpleObjectDependencyAction objectDependencyAction in this.ModelDiscovery.ObjectDependencyActions)
            //    if (objectDependencyAction.Match(simpleObject))
            //        objectDependencyAction.OnNewObjectCreated(simpleObject); 
        }

		//protected virtual void OnLoad(object requester, SimpleObject simpleObject)
		//{
		//}

		//protected virtual void OnBeforeLoading(object requester, SimpleObject simpleObject)
		//{
		//}

		//protected virtual void OnAfterLoad(object requester, SimpleObject simpleObject)
		//{
		//}

        //protected virtual void OnSaving(object requester, SimpleObject simpleObject)
        //{
        //    foreach (SimpleObjectDependencyAction objectDependencyAction in this.ObjectDependencyActions)
        //        if (objectDependencyAction.Match(simpleObject))
        //            objectDependencyAction.OnSaving(simpleObject);
        //}

        //protected virtual void OnAfterSave(object requester, SimpleObject simpleObject, bool isNewBeforeSaving)
        //{
        //}

        //protected virtual void OnDeleting(SimpleObject simpleObject, object requester)
        //{
        //}

        //protected virtual void OnBeforeDeleting(object requester, SimpleObject simpleObject)
        //{
        //    foreach (SimpleObjectDependencyAction objectDependencyAction in this.ObjectDependencyActions)
        //        if (objectDependencyAction.Match(simpleObject))
        //            objectDependencyAction.OnBeforeDelete(simpleObject);
        //}

        protected virtual void OnBeforePropertyValueChange(SimpleObject simpleObject, IPropertyModel propertyModel, object value, object newValue, bool willBeChanged, object requester)
        {
        }

    //    protected virtual void OnPropertyValueChange(object requester, SimpleObject simpleObject, int propertyIndex, object value, object oldValue)
    //    {
    //        foreach (SimpleObjectDependencyAction objectDependencyAction in this.ObjectDependencyActions)
    //        {
				////for (int i = 0; i < 2; i++)
				////{
    //                if (objectDependencyAction.Match(simpleObject))
    //                    objectDependencyAction.OnPropertyValueChange(simpleObject, propertyIndex, value, oldValue);
				////}
    //        }
    //    }

        protected virtual void OnChangedPropertiesCountChange(SimpleObject simpleObject, int changedPropertyCount, int oldChangedPropertyCount)
        {
        }

        protected virtual void OnChangedSaveablePropertiesCountChange(SimpleObject simpleObject, int changedPropertyCount, int oldChangedPropertyCount)
        {
        }

        protected virtual void OnImageNameChange(SimpleObject simpleObject, string? imageName, string? oldImageName)
        {
        }
        
        //private object GetNormalizedPropertyValueFromDatastoreData(int propertyIndex, object datastoreData)
        //{
        //    IObjectProperty propertyModel = this.GetObjectModel(propertyIndex).ObjectPropertys[propertyIndex];
        //    return this.GetNormalizedPropertyValueFromDatastoreData(propertyModel, datastoreData);
        //}


        #endregion |   Protected Methods   |

        #region |   Internal Methods   |

        //internal ISimpleObjectModel GetObjectModelByModelDefinitionType(Type modelDefinitionType)
        //{
        //	ISimpleObjectModel result = null;

        //	this.objectModelsByObjectType.TryGetValue(modelDefinitionType, out result);
        //	return result;
        //}

        #endregion

        #region |   Protected Raise Event Methods   |

        protected virtual void RaiseNewObjectCreated(SimpleObject simpleObject, object? requester)
        {
			this.NewObjectCreated?.Invoke(this, new SimpleObjectRequesterEventArgs(simpleObject, requester));
        }

        protected virtual void RaiseBeforeSave(SimpleObject simpleObject, object? requester)
        {
            this.BeforeSave?.Invoke(this, new SimpleObjectRequesterEventArgs(simpleObject, requester));
        }

   //     protected virtual void RaiseSaving(SimpleObject simpleObject, object requester)
   //     {
			//this.Saving?.Invoke(this, new SimpleObjectRequesterEventArgs(simpleObject, requester));
   //     }

        protected virtual void RaiseAfterSave(SimpleObject simpleObject, bool isNewBeforeSaving, object? requester)
        {
            this.AfterSave?.Invoke(this, new AfterSaveSimpleObjectRequesterEventArgs(simpleObject, isNewBeforeSaving, requester));
        }

        protected virtual void RaiseDeleteRequested(SimpleObject simpleObject, ChangeContainer changeContainer, object? requester)
        {
            this.DeleteRequested?.Invoke(this, new SimpleObjectChangeContainerRequesterEventArgs(simpleObject, changeContainer, requester));
        }

        protected virtual void RaiseDeleteRequestCancelled(SimpleObject simpleObject, ChangeContainer changeContainer, object? requester)
        {
            this.DeleteRequestCancelled?.Invoke(this, new SimpleObjectChangeContainerRequesterEventArgs(simpleObject, changeContainer, requester));
        }

        protected virtual void RaiseBeforeDelete(SimpleObject simpleObject, object? requester)
        {
            this.BeforeDelete?.Invoke(this, new SimpleObjectRequesterEventArgs(simpleObject, requester));
        }

        protected virtual void RaiseAfterDelete(SimpleObject simpleObject, object? requester)
        {
            this.AfterDelete?.Invoke(this, new SimpleObjectRequesterEventArgs(simpleObject, requester));
        }

        protected virtual void RaiseBeforePropertyValueChange(SimpleObject simpleObject, IPropertyModel propertyModel, object value, object newValue, bool willBeChanged, object? requester)
        {
            this.BeforePropertyValueChange?.Invoke(this, new BeforeChangePropertyValueSimpleObjectRequesterEventArgs(simpleObject, propertyModel, value, newValue, willBeChanged, requester));
        }

        protected virtual void RaisePropertyValueChange(SimpleObject simpleObject, IPropertyModel propertyModel, object value, object oldValue, bool isChanged, bool isSaveable, object? requester)
        {
            this.PropertyValueChange?.Invoke(this, new ChangePropertyValueSimpleObjectRequesterEventArgs(simpleObject, propertyModel, value, oldValue, isChanged, isSaveable, requester));
        }

        protected virtual void RaiseSaveablePropertyValueChange(SimpleObject simpleObject, IPropertyModel propertyModel, object value, object oldValue, bool isChanged, object? requester)
        {
            this.SaveablePropertyValueChange?.Invoke(this, new ChangePropertyValueSimpleObjectRequesterEventArgs(simpleObject, propertyModel, value, oldValue, isChanged, isSaveable: true, requester));
        }

        protected virtual void RaiseChangedPropertiesCountChange(SimpleObject simpleObject, int changedPropertiesCount, int oldChangedPropertiesCount)
        {
            this.ChangedPropertiesCountChange?.Invoke(this, new CountChangeSimpleObjectEventArgs(simpleObject, changedPropertiesCount, oldChangedPropertiesCount));
        }

        protected virtual void RaiseChangedSaveablePropertiesCountChange(SimpleObject simpleObject, int changedPropertiesCount, int oldChangedPropertiesCount)
        {
            this.ChangedSaveablePropertiesCountChange?.Invoke(this, new CountChangeSimpleObjectEventArgs(simpleObject, changedPropertiesCount, oldChangedPropertiesCount));
        }

        protected virtual void RaiseRequireSavingChange(SimpleObject simpleObject, bool requireSaving)
        {
            this.RequireSavingChange?.Invoke(this, new RequireSavingChangeSimpleObjectEventArgs(simpleObject, requireSaving));
        }

		//protected virtual void RaiseRequireCommitChange(bool requireCommit)
		//{
		//	this.RequireCommitChange?.Invoke(this, new RequireCommitChangeEventArgs(requireCommit));
		//}

		protected virtual void RaiseImageNameChange(SimpleObject simpleObject, string? imageName, string? oldImageName)
        {
            //if (simpleObject as SimpleObject is SystemSimpleObject)
            //	return;

            bool raiseSingleImageChange = false;

            //lock (this.lockMultipleStatusChange)
            //{
                raiseSingleImageChange = (this.multipleStatusChangesInProgress <= 0);

                if (!raiseSingleImageChange)
                    this.multipleImageNameChangeSimpleObjectEventArgs.Add(new ImageNameChangeSimpleObjectEventArgs(simpleObject, imageName, oldImageName));
            //}

            if (raiseSingleImageChange)
                this.ImageNameChange?.Invoke(this, new ImageNameChangeSimpleObjectEventArgs(simpleObject, imageName, oldImageName));
        }

		protected virtual void RaiseMultipleImageNameChange(List<ImageNameChangeSimpleObjectEventArgs> imageNameChangeSimpleObjectEventArgsList)
		{
			this.MultipleImageNameChange?.Invoke(this, imageNameChangeSimpleObjectEventArgsList);
		}

        #endregion |   Protected Raise Event Methods   |

        #region |   Private Methods   |

        private void DefaultChangeContainer_RequireCommitChange(object sender, RequireCommitChangeEventArgs e)
        {
            this.RequireCommitChange?.Invoke(this, e);
        }

        #endregion |   Private Methods   |
    }

    #region |   Delegates   |

    public delegate void SimpleObjectEventHandler(object sender, SimpleObjectEventArgs e);
	public delegate void SimpleObjectRequesterEventHandler(object sender, SimpleObjectRequesterEventArgs e);
    public delegate void AfterSaveSimpleObjectRequesterEventHandler(object sender, AfterSaveSimpleObjectRequesterEventArgs e);
    public delegate void PropertyValueRequesterSimpleObjectEventHandler(object sender, PropertyValueSimpleObjectRequesterEventArgs e);
    public delegate void ChangePropertyValueSimpleObjectRequesterEventHandler(object sender, ChangePropertyValueSimpleObjectRequesterEventArgs e);
    public delegate void BeforeChangePropertyValueSimpleObjectRequesterEventHandler(object sender, BeforeChangePropertyValueSimpleObjectRequesterEventArgs e);
    public delegate void CountChangeSimpleObjectEventHandler(object sender, CountChangeSimpleObjectEventArgs e);
    public delegate void RequireSavingChangeSimpleObjectEventHandler(object sender, RequireSavingChangeSimpleObjectEventArgs e);
    public delegate void SimpleObjectChangeContainerRequesterEventHandler(object sender, SimpleObjectChangeContainerRequesterEventArgs e);
    public delegate void RequireCommitChangeEventHandler(object sender, RequireCommitChangeEventArgs e);
	public delegate void ImageNameChangeSimpleObjectEventHandler(object sender, ImageNameChangeSimpleObjectEventArgs e);
	public delegate void MultipleImageNameChangeSimpleObjectEventHandler(object sender, List<ImageNameChangeSimpleObjectEventArgs> e);

    #endregion |   Delegates   |

    #region |   EventArgs Classes   |

    public class SimpleObjectEventArgs : EventArgs
    {
        public SimpleObjectEventArgs(SimpleObject? simpleObject)
	    {
            this.SimpleObject = simpleObject;
	    }

        public SimpleObject? SimpleObject { get; private set; }
    }

    public class SimpleObjectRequesterEventArgs : RequesterEventArgs
    {
        public SimpleObjectRequesterEventArgs(SimpleObject simpleObject, object? requester)
            : base(requester)
        {
            this.SimpleObject = simpleObject;
        }

        public SimpleObject SimpleObject { get; private set; }
    }

    public class AfterSaveSimpleObjectRequesterEventArgs : SimpleObjectRequesterEventArgs
    {
        public AfterSaveSimpleObjectRequesterEventArgs(SimpleObject simpleObject, bool isNewBeforeSaving, object? requester)
            : base(simpleObject, requester)
	    {
            this.IsNewBeforeSaving = isNewBeforeSaving;
	    }

        public bool IsNewBeforeSaving { get; private set; }
    }

    public class PropertySimpleObjectRequesterEventArgs : SimpleObjectRequesterEventArgs
    {
        public PropertySimpleObjectRequesterEventArgs(SimpleObject simpleObject, IPropertyModel? propertyModel, object? requester)
            : base(simpleObject, requester)
        {
            this.PropertyModel = propertyModel;
        }

        public IPropertyModel? PropertyModel { get; private set; }
    }

    public class PropertyValueSimpleObjectRequesterEventArgs : PropertySimpleObjectRequesterEventArgs
    {
        public PropertyValueSimpleObjectRequesterEventArgs(SimpleObject simpleObject, IPropertyModel? propertyModel, object? propertyValue, object? requester)
            : base(simpleObject, propertyModel, requester)
        {
            this.Value = propertyValue;
        }

        public object? Value { get; private set; }
    }

    public class ChangePropertyValueSimpleObjectRequesterEventArgs : PropertyValueSimpleObjectRequesterEventArgs
    {
        public ChangePropertyValueSimpleObjectRequesterEventArgs(SimpleObject simpleObject, IPropertyModel? propertyModel, object? value, object? oldValue, bool isChanged, bool isSaveable, object? requester)
            : base(simpleObject, propertyModel, value, requester)
        {
            this.OldValue = oldValue;
            this.IsChanged = isChanged;
            this.IsSaveable = isSaveable;
        }

        public object? OldValue { get; private set; }
        public bool IsChanged { get; private set; }
        public bool IsSaveable { get; private set; }
    }

    public class BeforeChangePropertyValueSimpleObjectRequesterEventArgs : PropertyValueSimpleObjectRequesterEventArgs
    {
        public BeforeChangePropertyValueSimpleObjectRequesterEventArgs(SimpleObject simpleObject, IPropertyModel? propertyModel, object? value, object? newValue, bool willBeChanged, object? requester)
            : base(simpleObject, propertyModel, value, requester)
        {
            this.NewValue = newValue;
            this.WillBeChanged = willBeChanged;
        }

        public object? NewValue { get; private set; }
        public object WillBeChanged { get; private set; }
    }

    public class CountChangeSimpleObjectEventArgs : SimpleObjectEventArgs
    {
        public CountChangeSimpleObjectEventArgs(SimpleObject simpleObject, int count, int oldCount)
            : base(simpleObject)
        {
            this.Count = count;
            this.OldCount = oldCount;
        }

        public int Count { get; private set; }
        public int OldCount { get; private set; }
    }

    public class RequireSavingChangeSimpleObjectEventArgs : SimpleObjectEventArgs
    {
        public RequireSavingChangeSimpleObjectEventArgs(SimpleObject simpleObject, bool requireSaving)
            : base(simpleObject)
        {
            this.RequireSaving = requireSaving;
        }

        public bool RequireSaving { get; private set; }
    }

    public class RequireCommitChangeEventArgs : EventArgs
    {
        public RequireCommitChangeEventArgs(bool requireCommit)
        {
            this.RequireCommit = requireCommit;
        }

        public bool RequireCommit { get; private set; }
    }

    public class SimpleObjectChangeContainerRequesterEventArgs : SimpleObjectRequesterEventArgs
    {
        public SimpleObjectChangeContainerRequesterEventArgs(SimpleObject simpleObject, ChangeContainer changeContainer, object? requester)
            : base(simpleObject, requester)
        {
            this.ChangeContainer = changeContainer;
        }

        public ChangeContainer ChangeContainer { get; set; }
    }

    public class ImageNameChangeSimpleObjectEventArgs : SimpleObjectEventArgs
    {
        public ImageNameChangeSimpleObjectEventArgs(SimpleObject simpleObject, string? imageName, string? oldImageName)
            : base(simpleObject)
        {
            this.ImageName = imageName;
            this.OldImageName = oldImageName;
        }
        
        public string? ImageName { get; private set; }
        public string? OldImageName { get; private set; }
    }

    #endregion |   EventArgs Classes   |

    #region |   Interfaces   |
    ////public interface ISimpleObjectManager
    ////{
    ////    ISimpleObjectModel GetModel(Type simpleObjectType);

    ////    void Load(object requester, SimpleObject simpleObject);
    ////    void Load(object requester, SimpleObject simpleObject, IDictionary<string, object> propertyDictionary, bool isNew);

    ////    bool Save(object requester, SimpleObject simpleObject);

    ////    void Delete(object requester, SimpleObject simpleObject);

    ////    event ActionRequesterSimpleObjectEventHandler ObjectAction;

    ////    event RequesterSimpleObjectEventHandler ObjectCreated;

    ////    event RequesterSimpleObjectEventHandler BeforeSaving;
    ////    event RequesterSimpleObjectEventHandler AfterSave;

    ////    event RequesterSimpleObjectEventHandler BeforeLoading;
    ////    event RequesterSimpleObjectEventHandler AfterLoad;

    ////    event RequesterSimpleObjectEventHandler BeforeDeleting;

    ////    event OldPropertyValueRequesterSimpleObjectEventHandler PropertyValueChange;
    ////}

    #endregion |   Interfaces   |
}
