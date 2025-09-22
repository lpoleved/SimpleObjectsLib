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
using Simple.Objects.Graph.Manager;

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

        public event SimpleObjectChangeContainerContextRequesterEventHandler NewObjectCreated;

        public event SimpleObjectContextRequesterEventHandler BeforeSave;
        //public event SimpleObjectRequesterEventHandler Saving;
        public event AfterSaveSimpleObjectRequesterEventHandler AfterSave;

		//public event SimpleObjectRequesterEventHandler BeforeLoading;
		//public event SimpleObjectRequesterEventHandler AfterLoad;

        public event SimpleObjectChangeContainerContextRequesterEventHandler DeleteRequested;
        public event SimpleObjectChangeContainerContextRequesterEventHandler DeleteRequestCancelled;

        public event SimpleObjectChangeContainerContextRequesterEventHandler BeforeDelete;
        public event SimpleObjectChangeContainerContextRequesterEventHandler AfterDelete;

        public event BeforeChangePropertyValuePropertyModelSimpleObjectChangeContainerContextRequesterEventHandler BeforePropertyValueChange;
        public event ChangePropertyValuePropertyModelSimpleObjectChangeContainerRequesterEventHandler PropertyValueChange;
        //public event ChangePropertyValueSimpleObjectRequesterEventHandler SaveablePropertyValueChange;

        public event CountChangeSimpleObjectEventHandler ChangedPropertiesCountChange;
        //public event CountChangeSimpleObjectEventHandler ChangedSaveablePropertiesCountChange;
        public event RequireSavingSimpleObjectEventHandler RequireSavingChange;
        public event RequireCommitChangeContainerEventHandler RequireCommitChange;

        //public event RequireCommitChangeEventHandler RequireCommitChange;

        public event ImageNameChangeSimpleObjectEventHandler ImageNameChange;
		public event MultipleImageNameChangeSimpleObjectEventHandler MultipleImageNameChange;

        #endregion |   Events   |

        #region |   Public Properties   |

        public ICryptoTransform Encryptor { get; private set; }
        public ICryptoTransform Decryptor { get; private set; }
		//public int CryptoBlockSize { get; protected set; }

		public static readonly ForeignClientRequester ForeignClientRequester = new ForeignClientRequester();

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

        public void AcceptChanges(SimpleObject simpleObject, ChangeContainer? changeContainer, object? requester)
        {
            simpleObject.AcceptChangesInternal(changeContainer, requester);
        }

        public void RejectChanges(SimpleObject simpleObject, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
        {
            simpleObject.RejectChangesIntrenal(changeContainer, context, requester);
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
            if (oldImageName != String.Empty) // String.Empty is default value, and noo need to fire event when first time gets the image name
            {
                this.RaiseImageNameChange(simpleObject, imageName, oldImageName);
                this.OnImageNameChange(simpleObject, imageName, oldImageName);

                if (!(simpleObject is GraphElement)) // if SimpleObject image name is changed, propagate change it to it's GraphElements
                    foreach (GraphElement graphElement in simpleObject.GraphElements)
                        graphElement.RecalcImageName();
            }
        }

        //protected internal virtual void NewClientObjectIsCreated(SimpleObject simpleObject, ChangeContainer? changeContainer, object? requester)
        //{
        //    //if (this.WorkingMode != ObjectManagerWorkingMode.Server)
        //    //{
        //    simpleObject.NewClientObjectIsCreated(changeContainer, requester);
        //    this.RaiseNewClientObjectCreated(simpleObject, changeContainer, requester);
        //    this.OnNewObjectCreated(simpleObject, changeContainer, requester);
        //    //}
        //}

		//protected internal virtual void NewGraphElementIsCreated(GraphElement graphElement, ChangeContainer? changeContainer, object? requester)
		//{
		//	GraphElement? parent = graphElement.Parent;

		//	// Check if needed to add in null collection only for manualy creating GraphElement, not using GraphElement constructor that take parent value and not setting Parent property manualy
		//	// (e.g. var ge = new GraphElement(objectManager)
		//	//       ge.GraphKey = 5
		//	//       ge.SimpleObject = simpleObject
		//	//       //ge.Parent = null; <- if you not set this, the GraphElement wouldn't be added to the Graph.RootGraphElements null collection!!! So This code correct such a cases.
		//	//       ...
		//	if (parent == null)
		//	{
		//		SimpleObjectCollection? nullCollection = graphElement.GetOneToManyForeignNullCollectionInternal(RelationPolicyModelBase.OneToManyGraphElementToParentGraphElement.RelationKey);

		//		if (nullCollection != null && !nullCollection.Contains(graphElement)) // If collection initializes, its already contains this
		//			nullCollection.Add(graphElement, changeContainer, requester);
		//	}

		//	//if (changeContainer != null)
		//	//	changeContainer.Set(graphElement,TransactionRequestAction.Save, requester);

		//	//if (this.WorkingMode != ObjectManagerWorkingMode.Server)
		//	//{

		//	if (graphElement.CanRiseNewGraphElementCreatedEvent && !graphElement.IsNewGraphElementCreatedRised)
		//	{
		//		graphElement.SimpleObject.NewGraphElementIsCreated(graphElement, changeContainer, requester);

		//		this.RaiseNewGraphElementCreated(graphElement, requester);
		//		//graphElement.CanRiseNewGraphElementCreatedEvent = false; // Prevent second call if occur
		//		graphElement.IsNewGraphElementCreatedRised = true;
		//		this.OnNewGraphElementCreated(graphElement, changeContainer, requester);

		//		//if (graphElement.Parent != null)
		//		//	this.GraphElementParentIsChanged(graphElement, oldParent: null, changeContainer, requester);

		//		if (this.WorkingMode != ObjectManagerWorkingMode.Server)
		//			this.MoveNeighborGraphElementsOnGraphElementCreationOrParentChangeIfRequired(graphElement);

		//		//graphElement.IsNewGraphElementCreatedEventRised = true;
		//	}
		//	else
		//	{
		//		throw new Exception("You cannot raise NewGraphElementCreated event twice or more times");

		//		graphElement.CanRiseNewGraphElementCreatedEvent = false;
		//	}

		//	// TODO: Check this, why is this require????
		//	//if (parent == graphElement.Parent && parent != null) // If parent is not changed by this.MoveGraphElementOnGraphElementCreationIfRequired raise event GraphElementParentChange
		//	//	this.GraphElementParentIsChanged(graphElement, graphElement.Parent, parent, changeContainer, requester);

		//	//this.ActivateGraphObjectActions(graphElement);

		//	//}

		//	//if (graphElement.ParentId == 0)
		//	//	graphElement.Graph.RootGraphElementIsCreated(graphElement);
		//}

		// TODO: Create method NewObjectIsCreated and than switch as needed (NewClientObjectCreated, OnOBjectCreated and NewGraphElementCreated)

		protected internal virtual void NewObjectIsCreated(SimpleObject simpleObject, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
        {
            changeContainer?.Set(simpleObject, TransactionRequestAction.Save, requester);

			this.OnNewObjectCreated(simpleObject, changeContainer, context, requester);

            if (simpleObject is GraphElement graphElement)
            {
                this.NewGraphElementIsCreated(graphElement, changeContainer, context, requester);
            }
            else //if (this.WorkingMode != ObjectManagerWorkingMode.Server)
            {
                simpleObject.NewObjectIsCreated(changeContainer, context, requester);
                this.RaiseNewClientObjectCreated(simpleObject, changeContainer, context, requester);
            }
		}

        protected internal void NewGraphElementIsCreated(GraphElement graphElement, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
        {
			if (graphElement.CanRiseNewGraphElementCreatedEvent && !graphElement.IsNewGraphElementCreatedRised)
            {
			    GraphElement? parentGraphElement = graphElement.Parent;

				if (this.WorkingMode == ObjectManagerWorkingMode.Client)
				{
					graphElement.IsChildrenLoadedInClientMode = true;

					if (parentGraphElement != null)
						parentGraphElement.IsChildrenLoadedInClientMode = true;
				}

				//
				// Is this nessesary ?????? It is already present in SetOneToManyPrimaryObject for Parent
				//
				//if (parentGraphElement == null)
				//{
				//    SimpleObjectCollection? nullCollection = graphElement.GetOneToManyForeignNullCollectionInternal(RelationPolicyModelBase.OneToManyGraphElementToParentGraphElement.RelationKey);

				//    if (nullCollection != null && !nullCollection.Contains(graphElement)) // If collection initializes, its already contains this
				//        nullCollection.Add(graphElement, changeContainer, requester);
				//}

				graphElement.SimpleObject.NewGraphElementIsCreated(graphElement, changeContainer, context, requester);

                this.RaiseNewGraphElementCreated(graphElement, changeContainer, context, requester);
                graphElement.IsNewGraphElementCreatedRised = true;
                this.OnNewGraphElementCreated(graphElement, changeContainer, requester);

                if (this.WorkingMode != ObjectManagerWorkingMode.Server)
                    this.MoveNeighborGraphElementsOnGraphElementCreationOrParentChangeIfRequired(graphElement);
            }
        }

		protected internal virtual void BeforePropertyValueIsChanged(SimpleObject simpleObject, IPropertyModel propertyModel, object? value, object? newValue, bool willBeChanged, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
        {
            if (this.WorkingMode != ObjectManagerWorkingMode.Server)
            {
                this.RaiseBeforePropertyValueChange(simpleObject, propertyModel, value, newValue, willBeChanged, changeContainer, context, requester);
                this.OnBeforePropertyValueChange(simpleObject, propertyModel, value, newValue, willBeChanged, changeContainer, context, requester);
            }
        }

        protected internal virtual void PropertyValueIsChanged(SimpleObject simpleObject, IPropertyModel propertyModel, object? value, object? oldValue, bool isChanged, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
        {
            if (this.WorkingMode != ObjectManagerWorkingMode.Server)
            {
                this.RaisePropertyValueChange(simpleObject, propertyModel, value, oldValue, isChanged, changeContainer, context, requester);
                this.OnPropertyValueChange(simpleObject, propertyModel, value, oldValue, isChanged, changeContainer, context, requester);
            }
        }

        //protected internal virtual void SaveablePropertyValueIsChanged(SimpleObject simpleObject, IPropertyModel propertyModel, object value, object oldValue, bool isChanged, ChangeContainer? changeContainer, object? requester)
        //{
        //    if (this.WorkingMode != ObjectManagerWorkingMode.Server)
        //    {
        //        this.RaiseSaveablePropertyValueChange(simpleObject, propertyModel, value, oldValue, isChanged, requester);
        //        this.OnSaveablePropertyValueChange(simpleObject, propertyModel, value, oldValue, isChanged, changeContainer, requester);
        //    }
        //}

        protected internal virtual void ChangedPropertiesCountIsChanged(SimpleObject simpleObject, int changedPropertiesCount, int oldChangedPropertiesCount, ChangeContainer? changeContainer, object? requester)
        {
            if (this.WorkingMode != ObjectManagerWorkingMode.Server)
            {
                this.RaiseChangedPropertiesCountChange(simpleObject, changedPropertiesCount, oldChangedPropertiesCount);
                this.OnChangedPropertiesCountChange(simpleObject, changedPropertiesCount, oldChangedPropertiesCount);
            }

			if (oldChangedPropertiesCount == 0) // && simpleObject.RelatedTransactionRequests.Count == 0)
			{
				changeContainer?.Set(simpleObject, TransactionRequestAction.Save, requester);
				//if (changeContainer != null)
				//    changeContainer.RequireSavingChangeCount(requireSaving: true);

				//if (this.WorkingMode != ObjectManagerWorkingMode.Server)
				this.RaiseRequireSavingChange(simpleObject, requireSaving: true);
			}
			else if (changedPropertiesCount == 0) // && simpleObject.RelatedTransactionRequests.Count == 0)
			{
				if (!simpleObject.IsNew)
					changeContainer?.Unset(simpleObject, TransactionRequestAction.Save, requester);
				//if (changeContainer != null)
				//    changeContainer.RequireSavingChangeCount(requireSaving: false);

				//TransactionRequestAction requstAction;

				//if (changeContainer != null && changeContainer.TransactionRequests.TryGetValue(simpleObject, out requstAction))
				//    if (requstAction == TransactionRequestAction.Save)
				//        changeContainer.Remove(simpleObject);

				//if (this.WorkingMode != ObjectManagerWorkingMode.Server)
				this.RaiseRequireSavingChange(simpleObject, requireSaving: false);
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

		//protected internal virtual void ChangedSaveablePropertiesCountIsChanged(SimpleObject simpleObject, int changedSaveablePropertiesCount, int oldChangedSaveablePropertiesCount, ChangeContainer? changeContainer, object? requester)
  //      {
  //          //if (this.WorkingMode != ObjectManagerWorkingMode.Server)
  //          //{
  //              this.RaiseChangedSaveablePropertiesCountChange(simpleObject, changedSaveablePropertiesCount, oldChangedSaveablePropertiesCount);
  //              this.OnChangedSaveablePropertiesCountChange(simpleObject, changedSaveablePropertiesCount, oldChangedSaveablePropertiesCount);
  //          //}

  //      }

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

        protected virtual void OnNewObjectCreated(SimpleObject simpleObject, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
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

        protected virtual void OnBeforePropertyValueChange(SimpleObject simpleObject, IPropertyModel propertyModel, object? propertyValue, object? newPropertyValue, bool willBeChanged, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
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

        //protected virtual void OnChangedSaveablePropertiesCountChange(SimpleObject simpleObject, int changedPropertyCount, int oldChangedPropertyCount)
        //{
        //}

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

        protected virtual void RaiseNewClientObjectCreated(SimpleObject simpleObject, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
        {
			this.NewObjectCreated?.Invoke(this, new SimpleObjectChangeContainerContextRequesterEventArgs(simpleObject, changeContainer, context, requester));
        }

        protected virtual void RaiseBeforeSave(SimpleObject simpleObject, ObjectActionContext context, object? requester)
        {
            this.BeforeSave?.Invoke(this, new SimpleObjectContextRequesterEventArgs(simpleObject, context, requester));
        }

   //     protected virtual void RaiseSaving(SimpleObject simpleObject, object requester)
   //     {
			//this.Saving?.Invoke(this, new SimpleObjectRequesterEventArgs(simpleObject, requester));
   //     }

        protected virtual void RaiseAfterSave(SimpleObject simpleObject, bool isNewBeforeSaving, ObjectActionContext context, object? requester)
        {
            this.AfterSave?.Invoke(this, new AfterSaveSimpleObjectContextRequesterEventArgs(simpleObject, isNewBeforeSaving, context, requester));
        }

        protected virtual void RaiseDeleteRequested(SimpleObject simpleObject, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
        {
            this.DeleteRequested?.Invoke(this, new SimpleObjectChangeContainerContextRequesterEventArgs(simpleObject, changeContainer, context, requester));
        }

        protected virtual void RaiseDeleteRequestCancelled(SimpleObject simpleObject, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
        {
            this.DeleteRequestCancelled?.Invoke(this, new SimpleObjectChangeContainerContextRequesterEventArgs(simpleObject, changeContainer, context, requester));
        }

        protected virtual void RaiseBeforeDelete(SimpleObject simpleObject, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
        {
            this.BeforeDelete?.Invoke(this, new SimpleObjectChangeContainerContextRequesterEventArgs(simpleObject, changeContainer, context, requester));
        }

        protected internal virtual void RaiseAfterDelete(SimpleObject simpleObject, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
        {
            this.AfterDelete?.Invoke(this, new SimpleObjectChangeContainerContextRequesterEventArgs(simpleObject, changeContainer, context, requester));
        }

        protected virtual void RaiseBeforePropertyValueChange(SimpleObject simpleObject, IPropertyModel propertyModel, object? value, object? newValue, bool willBeChanged, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
        {
            this.BeforePropertyValueChange?.Invoke(this, new BeforeChangePropertyValuePropertyModelSimpleObjectChangeContainerContextRequesterEventArgs(simpleObject, propertyModel, value, newValue, willBeChanged, changeContainer, context, requester));
        }

        protected virtual void RaisePropertyValueChange(SimpleObject simpleObject, IPropertyModel propertyModel, object? value, object? oldValue, bool isChanged, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
        {
            this.PropertyValueChange?.Invoke(this, new ChangePropertyValuePertyModelSimpleObjectChangeContainerContextRequesterEventArgs(simpleObject, propertyModel, value, oldValue, isChanged, changeContainer, context, requester));
        }

        //protected virtual void RaiseSaveablePropertyValueChange(SimpleObject simpleObject, IPropertyModel propertyModel, object value, object oldValue, bool isChanged, object? requester)
        //{
        //    this.SaveablePropertyValueChange?.Invoke(this, new ChangePropertyValueSimpleObjectRequesterEventArgs(simpleObject, propertyModel, value, oldValue, isChanged, isSaveable: true, requester));
        //}

        protected virtual void RaiseChangedPropertiesCountChange(SimpleObject simpleObject, int changedPropertiesCount, int oldChangedPropertiesCount)
        {
            this.ChangedPropertiesCountChange?.Invoke(this, new CountChangeSimpleObjectEventArgs(simpleObject, changedPropertiesCount, oldChangedPropertiesCount));
        }

        //protected virtual void RaiseChangedSaveablePropertiesCountChange(SimpleObject simpleObject, int changedPropertiesCount, int oldChangedPropertiesCount)
        //{
        //    this.ChangedSaveablePropertiesCountChange?.Invoke(this, new CountChangeSimpleObjectEventArgs(simpleObject, changedPropertiesCount, oldChangedPropertiesCount));
        //}

        protected virtual void RaiseRequireSavingChange(SimpleObject simpleObject, bool requireSaving)
        {
            this.RequireSavingChange?.Invoke(this, new RequireSavingSimpleObjectEventArgs(simpleObject, requireSaving));
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

        private void DefaultChangeContainer_RequireCommitChange(object sender, RequireCommiChangeContainertEventArgs e)
        {
            this.RequireCommitChange?.Invoke(this, e);
        }

        #endregion |   Private Methods   |
    }

    #region |   Delegates   |

    public delegate void SimpleObjectEventHandler(object sender, SimpleObjectEventArgs e);
	public delegate void NodeGraphElementSimpleObjectEventHandler(object sender, NodeGraphElementSimpleObjectEventArgs e);
	public delegate void SimpleObjectRequesterEventHandler(object sender, SimpleObjectRequesterEventArgs e);
	public delegate void SimpleObjectContextRequesterEventHandler(object sender, SimpleObjectContextRequesterEventArgs e);
	public delegate void AfterSaveSimpleObjectRequesterEventHandler(object sender, AfterSaveSimpleObjectContextRequesterEventArgs e);
    public delegate void PropertyValuePropertyModelSimpleObjectChangeContainerContextRequesterEventHandler(object sender, PropertyValuePropertyModelSimpleObjectChangeContainerContextRequesterEventArgs e);
    public delegate void ChangePropertyValuePropertyModelSimpleObjectChangeContainerRequesterEventHandler(object sender, ChangePropertyValuePertyModelSimpleObjectChangeContainerContextRequesterEventArgs e);
    public delegate void BeforeChangePropertyValuePropertyModelSimpleObjectChangeContainerContextRequesterEventHandler(object sender, BeforeChangePropertyValuePropertyModelSimpleObjectChangeContainerContextRequesterEventArgs e);
    public delegate void CountChangeSimpleObjectEventHandler(object sender, CountChangeSimpleObjectEventArgs e);
    public delegate void SimpleObjectChangeContainerContextRequesterEventHandler(object sender, SimpleObjectChangeContainerContextRequesterEventArgs e);
    public delegate void RequireSavingSimpleObjectEventHandler(object sender, RequireSavingSimpleObjectEventArgs e);
    public delegate void RequireCommitChangeContainerEventHandler(object sender, RequireCommiChangeContainertEventArgs e);
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

	public class GraphElementSimpleObjectEventArgs : SimpleObjectEventArgs
	{
		public GraphElementSimpleObjectEventArgs(GraphElement? graphElement, SimpleObject? simpleObject)
            : base(simpleObject)
		{
			this.GraphElement = graphElement;
		}

		public GraphElement? GraphElement { get; private set; }
	}

	public class NodeGraphElementSimpleObjectEventArgs : GraphElementSimpleObjectEventArgs
	{
        public NodeGraphElementSimpleObjectEventArgs(object? node, GraphElement? graphElement, SimpleObject? simpleObject)
            : base(graphElement, simpleObject)
        {
            this.Node = node;
        }

        public object? Node { get; private set; }
	}

	public class SimpleObjectChangeContainerContextRequesterEventArgs : ChangeContainerContextRequesterEventArgs
	{
		public SimpleObjectChangeContainerContextRequesterEventArgs(SimpleObject simpleObject, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
			: base(changeContainer, context, requester)
		{
			this.SimpleObject = simpleObject;
		}

		public SimpleObject SimpleObject { get; private set; }
	}

	public class SimpleObjectContextRequesterEventArgs : ContextRequesterEventArgs
	{
        public SimpleObjectContextRequesterEventArgs(SimpleObject simpleObject, ObjectActionContext context, object? requester)
            : base(context, requester)
        {
            this.SimpleObject = simpleObject;
        }

        public SimpleObject SimpleObject { get; private set; }
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


	public class ChangeContainerContextRequesterEventArgs : ContextRequesterEventArgs
	{
		public ChangeContainerContextRequesterEventArgs(ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
            : base(context, requester)
		{
			this.ChangeContainer = changeContainer;
		}

        public ChangeContainer? ChangeContainer { get; private set; }
	}

    public class ContextRequesterEventArgs : RequesterEventArgs
    {
		public ContextRequesterEventArgs(ObjectActionContext context, object? requester)
            : base(requester)
		{
            this.Context = context;    
		}

		public ObjectActionContext Context { get; private set; }
	}


	public class AfterSaveSimpleObjectContextRequesterEventArgs : SimpleObjectContextRequesterEventArgs
    {
        public AfterSaveSimpleObjectContextRequesterEventArgs(SimpleObject simpleObject, bool isNewBeforeSaving, ObjectActionContext context, object? requester)
            : base(simpleObject, context, requester)
	    {
            this.IsNewBeforeSaving = isNewBeforeSaving;
	    }

        public bool IsNewBeforeSaving { get; private set; }
    }

    public class PropertyModelSimpleObjectChangeContainercContextRequesterEventArgs : SimpleObjectChangeContainerContextRequesterEventArgs
    {
        public PropertyModelSimpleObjectChangeContainercContextRequesterEventArgs(SimpleObject simpleObject, IPropertyModel? propertyModel, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
            : base(simpleObject, changeContainer, context, requester)
        {
            this.PropertyModel = propertyModel;
        }

        public IPropertyModel? PropertyModel { get; private set; }
    }

    public class PropertyValuePropertyModelSimpleObjectChangeContainerContextRequesterEventArgs : PropertyModelSimpleObjectChangeContainercContextRequesterEventArgs
    {
        public PropertyValuePropertyModelSimpleObjectChangeContainerContextRequesterEventArgs(SimpleObject simpleObject, IPropertyModel? propertyModel, object? propertyValue, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
            : base(simpleObject, propertyModel, changeContainer, context, requester)
        {
            this.PropertyValue = propertyValue;
        }

        public object? PropertyValue { get; private set; }
    }
    
    public class ChangePropertyValuePertyModelSimpleObjectChangeContainerContextRequesterEventArgs : PropertyValuePropertyModelSimpleObjectChangeContainerContextRequesterEventArgs
    {
        public ChangePropertyValuePertyModelSimpleObjectChangeContainerContextRequesterEventArgs(SimpleObject simpleObject, IPropertyModel? propertyModel, object? propertyValue, object? oldPropertyValue, bool isChanged, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
            : base(simpleObject, propertyModel, propertyValue, changeContainer, context, requester)
        {
			this.OldPropertyValue = oldPropertyValue;
            this.IsChanged = isChanged;
        }

		public object? OldPropertyValue { get; }
        public bool IsChanged { get; private set; }
    }

    public class BeforeChangePropertyValuePropertyModelSimpleObjectChangeContainerContextRequesterEventArgs : PropertyValuePropertyModelSimpleObjectChangeContainerContextRequesterEventArgs
    {
		private object? newPropertyValue;

		public BeforeChangePropertyValuePropertyModelSimpleObjectChangeContainerContextRequesterEventArgs(SimpleObject simpleObject, IPropertyModel? propertyModel, object? propertyValue, object? newPropertyValue, bool willBeChanged, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
            : base(simpleObject, propertyModel, propertyValue, changeContainer, context, requester)
        {
			this.NewPropertyValue = newPropertyValue;
            this.WillBeChanged = willBeChanged;
        }

		public object? NewPropertyValue { get => newPropertyValue; private set => newPropertyValue = value; }
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

    public class RequireSavingSimpleObjectEventArgs : SimpleObjectEventArgs
    {
        public RequireSavingSimpleObjectEventArgs(SimpleObject simpleObject, bool requireSaving)
            : base(simpleObject)
        {
            this.RequireSaving = requireSaving;
        }

        public bool RequireSaving { get; private set; }
    }

    public class RequireCommiChangeContainertEventArgs : EventArgs
    {
        public RequireCommiChangeContainertEventArgs(bool requireCommit, ChangeContainer changeContainer)
        {
            this.RequireCommit = requireCommit;
            this.ChangeContainer = changeContainer;
        }

        public bool RequireCommit { get; private set; }
        public ChangeContainer ChangeContainer { get; private set; }
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
