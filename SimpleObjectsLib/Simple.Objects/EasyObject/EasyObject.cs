using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Collections;
using Simple.Modeling;
//using Simple.Security;

namespace Simple.Objects
{
    public abstract class EasyObject : IBindingEasyObject, IPropertyValue, IDisposable
    {
        #region |   Internal Members   |

        //internal ImageInfo imageInfo = null;

		#endregion |   Internal Members   |

		#region |   Protected Members   |

		protected readonly object lockObject = new object();

		#endregion |   Protected Members   |
		
		#region |   Private Members   |

		private HashSet<int> changedPropertyIndexes = new HashSet<int>();
        private EasyObjectManager objectManager = null;
        private bool isNew = true;
        private bool isDeleteStarted = false;
        private bool isDeleted = false;
        private bool isReadOnly = false;
		private bool isValidationTest = false;
		private string imageName = String.Empty;
        private object tag = null;

        private const string imageNameOptionSeparator = ".";

        #endregion |   Private Members   |

		#region |   Constructors and Initialization   |

		public EasyObject(EasyObjectManager objectManager)
            : this(objectManager, true)
        {
        }

        public EasyObject(EasyObjectManager objectManager, bool fireObjectCreatedEvent)
        {
			this.objectManager = objectManager;
			
            if (fireObjectCreatedEvent)
                this.ObjectIsCreated();
        }

        #endregion |   Constructors and Initialization   |

        #region |   Events   |

        public event BeforeChangePropertyValueEasyObjectRequesterEventHandler BeforePropertyValueChange;
        public event ChangePropertyValueEasyObjectRequesterEventHandler PropertyValueChange;
        public event CountChangeEasyObject ChangedPropertiesCountChange;

        #endregion |   Events   |

        #region |   Public Properties   |

		public object this[int propertyIndex]
		{
			get { return this.GetPropertyValue(propertyIndex); }
			set { this.SetPropertyValue(propertyIndex, value); }
		}
		
		public object this[string propertyName]
        {
            get { return this.GetPropertyValue(propertyName); }
            set { this.SetPropertyValue(propertyName, value); }
        }

        public EasyObjectManager ObjectManager 
        {
            get { return this.objectManager; }
        }

        public bool IsNew
        {
            get { return this.isNew; }
			protected internal set { this.isNew = value; }
        }

        public bool IsDeleteStarted
        {
            get { return this.isDeleteStarted; }
            protected internal set { this.isDeleteStarted = value; }
        }

        public bool IsDeleted
        {
            get { return this.isDeleted; }
            protected internal set { this.isDeleted = value; }
        }

        public bool IsReadOnly 
        {
            get { return this.isReadOnly; }
            protected internal set { this.isReadOnly = value; }
        }

        //public bool IsDeletingInProgress
        //{
        //    get { return this.isDeletingInProgress; }
        //    internal set { this.isDeletingInProgress = value; }
        //}

		public int ChangedPropertiesCount
		{
			get { return this.changedPropertyIndexes.Count; }
		}
		
		public IEnumerable<int> ChangedPropertyIndexes
		{
			get { return new HashSet<int>(this.changedPropertyIndexes); }
		}

        public bool IsChanged
        {
			get { return this.changedPropertyIndexes.Count > 0; }
        }

        public object Tag
        {
            get { return this.tag; }
            set { this.tag = value; }
        }

		public bool IsValidationTest
		{
			get { return this.isValidationTest; }
			set { this.isValidationTest = value; }
		}

		#endregion |   Public Properties   |

		#region |   Private Properties   |

		//private IDictionary<string, object> PropertyValues
		//{
		//	get
		//	{
		//		if (this.propertyValues == null)
		//		{
		//			this.propertyValues = new Dictionary<string, object>();
		//		}

		//		return this.propertyValues;
		//	}

		//	set
		//	{
		//		this.propertyValues = value;
		//	}
		//}

		//private IDictionary<string, object> OldPropertyValues
		//{
		//	get
		//	{
		//		if (this.oldPropertyValues == null)
		//		{
		//			this.oldPropertyValues = new Dictionary<string, object>();
		//		}

		//		return this.oldPropertyValues;
		//	}

		//	set
		//	{
		//		this.oldPropertyValues = value;
		//	}
		//}

		//private IList<string> ChangedPropertyNamesInternal
		//{
		//	get
		//	{
		//		if (this.changedPropertyNames == null)
		//		{
		//			this.changedPropertyNames = new List<string>();
		//		}

		//		return this.changedPropertyNames;
		//	}

		//	set
		//	{
		//		this.changedPropertyNames = value;
		//	}
		//}

		//private IDictionary<string, object> PropertyValuesAsReadOnly
		//{
		//    get
		//    {
		//        if (this.propertyValuesAsReadOnly == null)
		//        {
		//            this.propertyValuesAsReadOnly = new ReadOnlyDictionary<string, object>(this.PropertyValues);
		//        }

		//        return this.propertyValuesAsReadOnly;
		//    }

		//    //set
		//    //{
		//    //    this.propertyValuesAsReadOnly = value;
		//    //}
		//}

		//private IDictionary<string, object> OldPropertyValuesAsReadOnly
		//{
		//    get
		//    {
		//        if (this.oldPropertyValuesAsReadOnly == null)
		//        {
		//            this.oldPropertyValuesAsReadOnly = new ReadOnlyDictionary<string, object>(this.OldPropertyValues);
		//        }

		//        return this.oldPropertyValuesAsReadOnly;
		//    }

		//    //set
		//    //{
		//    //    this.oldPropertyValuesAsReadOnly = value;
		//    //}
		//}

		//private IList<string> ChangedPropertyNamesAsReadOnly
		//{
		//    get
		//    {
		//        if (this.changedProperyNamesAsReadOnly == null)
		//        {
		//            this.changedProperyNamesAsReadOnly = new ReadOnlyList<string>(this.ChangedPropertyNamesInternal);
		//        }

		//        return this.changedProperyNamesAsReadOnly;
		//    }
		//}

		#endregion |   Private Properties   |

		#region |   Public Methods   |

		public int[] GetChangedPropertyIndexes()
		{
			return this.changedPropertyIndexes.ToArray();
		}

		public T GetPropertyValue<T>(IPropertyModel propertyModel)
        {
            return this.GetPropertyValue<T>(propertyModel.PropertyName);
        }
        
        public T GetPropertyValue<T>(string propertyName)
        {
            return this.GetPropertyValue<T>(propertyName, default(T));
        }

        public T GetPropertyValue<T>(IPropertyModel propertyModel, T defaultValue)
        {
            return this.GetPropertyValue<T>(propertyModel.PropertyName, defaultValue);
        }
        
        public T GetPropertyValue<T>(string propertyName, T defaultValue)
        {
            object value = this.GetPropertyValue(propertyName);

            if (value == null)
            {
                return defaultValue;
            }
            else if (value.GetType() != typeof(T))
            {
                return Conversion.TryChangeType<T>(value, defaultValue);
            }
            else
            {
                return (T)value;
            }
        }

        public object GetPropertyValue(IPropertyModel propertyModel)
        {
            return this.GetPropertyValue(propertyModel.PropertyName);
        }

        public object GetPropertyValue(string propertyName, bool normalizeValue)
        {
           return (normalizeValue) ? this.GetPropertyValue(propertyName) : this.GetFieldValue(this.GetModel().PropertyModels[propertyName].PropertyIndex);
        }

        public virtual object GetPropertyValue(string propertyName)
        {
			return this.GetPropertyValue(this.GetModel().PropertyModels[propertyName].PropertyIndex);
        }
		
		public T GetPropertyValue<T>(int propertyIndex)
		{
			return (T)this.GetFieldValue(propertyIndex);
			//T value = (T)this.GetFieldValue(propertyIndex);

			//if (value != null)
			//{
			//	IObjectProperty propertyModel = this.GetModel().ObjectPropertys[propertyIndex];

			//	if (propertyModel != null && propertyModel.IsEncrypted)
			//	{
			//		value =  Conversion.TryChangeType<T>(PasswordSecurity.EncryptPassword(value.ToString(), this.Manager.Encryptor, this.Manager.CryptoBlockSize));
			//	}
			//	else
			//	{
			//		value = (T)this.GetNormalizedPropertyValueFromDatastoreData(propertyIndex, value);
			//	}
			//}

			//return value;
		}

		//public T GetOldPropertyValue<T>(int propertyIndex)
		//{
		//	T value = (T)this.GetInternalOldPropertyValue(propertyIndex);
		//	value = (T)this.GetNormalizedPropertyValue(propertyIndex, value);

		//	return value;
		//}
		
		//public object GetOldPropertyValue(string propertyName, bool normalizeValue)
		//{
		//	return (normalizeValue) ? this.GetOldPropertyValue(propertyName) : this.GetOldPropertyValueInternal(propertyName);
		//}

		public virtual object GetPropertyValue(int propertyIndex)
		{
			return this.GetFieldValue(propertyIndex);
			//object value = this.GetFieldValue(propertyIndex);

			//if (value != null)
			//{
			//	IObjectProperty propertyModel = this.GetModel().ObjectPropertys[propertyIndex];

			//	if (propertyModel != null && propertyModel.IsEncrypted)
			//	{
			//		value = PasswordSecurity.EncryptPassword(value.ToString(), this.Manager.Encryptor, this.Manager.CryptoBlockSize);
			//	}
			//	else
			//	{
			//		value = this.GetNormalizedPropertyValueFromDatastoreData(propertyIndex, value);
			//	}
			//}

			//return value;
		}

		//public object GetOldPropertyValue(int propertyIndex)
		//{
		//	object value = this.GetInternalOldPropertyValue(propertyIndex);
		//	value = this.GetNormalizedPropertyValue(propertyIndex, value);

		//	return value;
		//}

		public void SetPropertyValue(string propertyName, object value)
		{
			IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyName];
			this.SetPropertyValue(propertyModel.PropertyIndex, value, null);
		}

		public void SetPropertyValue(IPropertyModel propertyModel, object value)
        {
            this.SetPropertyValue(propertyModel, value, null);
        }

        public void SetPropertyValue(IPropertyModel propertyModel, object value, object requester)
        {
            this.SetPropertyValue(propertyModel.PropertyIndex, value, requester);
        }

        public void SetPropertyValue(int propertyIndex, object value)
        {
            this.SetPropertyValue(propertyIndex, value, null); 
        }

		public virtual void SetPropertyValue(int propertyIndex, object value, object requester)
        {
			this.SetPropertyValueInternal(propertyIndex, value, false, false, decideActionFromModel: true, enforceAccessModifier: true, requester: requester);
        }

        public void AcceptChanges()
        {
            this.AcceptChanges(null);
        }

        public void AcceptChanges(object requester)
        {
            lock (lockObject)
            {
                foreach (IPropertyModel propertyModel in this.GetModel().PropertyModels)
				{
					object value = this.GetFieldValue(propertyModel.PropertyIndex);
					this.SetOldFieldValue(propertyModel.PropertyIndex, value);
				}

                this.RunChangedPropertyNamesAction(() => this.changedPropertyIndexes.Clear());
            }
        }

        public void RejectChanges()
        {
            this.RejectChanges(this);
        }

		/// <summary>
		/// Reject object property changes. Properties with IsSystem = true specified by object property model definition are not changed.
		/// </summary>
		/// <param name="requester">The initial action caller.</param>
		public void RejectChanges(object requester)
        {
            lock (lockObject)
            {
				foreach (IPropertyModel propertyModel in this.GetModel().PropertyModels)
				{
					if (propertyModel.AvoidRejectChanges)
						continue;

					object value = null;
					object oldValue = this.GetOldFieldValue(propertyModel.PropertyIndex);
					bool callPropertyValueChange = this.changedPropertyIndexes.Contains(propertyModel.PropertyIndex);

					if (callPropertyValueChange)
						value = this.GetFieldValue(propertyModel.PropertyIndex);

					this.SetFieldValue(propertyModel.PropertyIndex, oldValue);

					if (callPropertyValueChange)
						this.PropertyValueIsChanged(requester, propertyModel.PropertyIndex, oldValue, value);
				}

				this.RunChangedPropertyNamesAction(() => this.changedPropertyIndexes.Clear());
            }
        }

        public virtual IEasyObjectModel GetModel()
        {
			return this.GetCustomObjectModel();
        }

		protected virtual IEasyObjectModel GetCustomObjectModel()
		{
			return this.ObjectManager.GetObjectModel(this.GetType());
		}

        public string GetImageName()
        {
            if (this.imageName.IsNullOrEmpty())
                this.imageName = this.ObjectManager.GetImageName(this);

            return this.imageName;
        }

        public void AddImageNameOption(string imageOption)
        {
            if (imageOption != null && this.GetImageName() != null && this.GetImageName().Trim() != "")
            {
                string[] imageBaseNameAndOptions = this.GetImageName().Split(new string[] { imageNameOptionSeparator }, StringSplitOptions.RemoveEmptyEntries);
                string imageBaseName = imageBaseNameAndOptions[0];
                List<string> currentImageOptions = imageBaseNameAndOptions.ToList();
                currentImageOptions.RemoveAt(0);

                if (!currentImageOptions.Contains(imageOption))
                {
                    currentImageOptions.Add(imageOption);
                }

                string newImageOptions = String.Join(imageNameOptionSeparator, currentImageOptions.ToArray());
                
                this.ObjectManager.SetImageName(this, (newImageOptions == "") ? imageBaseName : imageBaseName + imageNameOptionSeparator + newImageOptions, this.imageName);
            }
            else
            {
                throw new ArgumentException("You try to describe image option and the image name is empty.");
            }
        }

        public void RemoveImageNameOption(string imageOption)
        {
            if (imageOption != null && this.GetImageName() != null && this.GetImageName().Trim() != "")
            {
                string[] imageBaseNameAndOptions = this.imageName.Split(new string[] { imageNameOptionSeparator }, StringSplitOptions.RemoveEmptyEntries);
                string imageBaseName = imageBaseNameAndOptions[0];
                List<string> currentImageOptions = imageBaseNameAndOptions.ToList();
                currentImageOptions.RemoveAt(0);

                if (currentImageOptions.Contains(imageOption))
                {
                    currentImageOptions.Remove(imageOption);
                }

                string newImageOptions = String.Join(imageNameOptionSeparator, currentImageOptions.ToArray());

                this.ObjectManager.SetImageName(this, (newImageOptions == "") ? imageBaseName : imageBaseName + imageNameOptionSeparator + newImageOptions, this.imageName);
            }
            else
            {
                throw new ArgumentException("You try to describe image option but the image name is empty.");
            }
        }

		//public IDictionary<string, object> GetPropertyValueDictionary()
		//{
		//	//lock(lockObject)
		//	//{
		//		return new Dictionary<string, object>(this.PropertyValues);
		//	//}
		//}

		//public IDictionary<string, object> GetOldPropertyValueDictionary()
		//{
		//	//lock (lockObject)
		//	//{
		//		return new Dictionary<string, object>(this.OldPropertyValues);
		//	//}
		//}

		//public IDictionary<string, object> GetChangedPropertyValueDictionary()
		//{
		//	Dictionary<string, object> result = new Dictionary<string, object>();

		//	lock (lockObject)
		//	{
		//		foreach (string propertyName in this.ChangedPropertyIndexes)
		//		{
		//			result.Add(propertyName, this.GetPropertyValueInternal(propertyName));
		//		}
		//	}

		//	return result;
		//}

		//public IDictionary<string, object> GetChangedOldPropertyValueDictionary()
		//{
		//	Dictionary<string, object> result = new Dictionary<string, object>();

		//	lock (lockObject)
		//	{
		//		foreach (string propertyName in this.ChangedPropertyIndexes)
		//		{
		//			result.Add(propertyName, this.GetOldPropertyValueInternal(propertyName));
		//		}
		//	}

		//	return result;
		//}

		public bool RequireSaving()
		{
			return !this.IsDeleteStarted && (this.IsNew || this.IsChanged);
		}

		#endregion |   Public Methods   |

		#region |   Internal Protected Methods   |

		//internal protected void SetPropertyDictionary(IDictionary<string, object> propertyValueData)
		//{
		//	lock (lockObject)
		//	{
		//		this.PropertyValues = propertyValueData;
		//		this.OldPropertyValues = null;
		//		this.ChangedPropertyNamesInternal = null;
		//	}
		//}

		protected internal void SetPropertyValueInternal(int propertyIndex, object value,  bool addOrRemoveInChangedProperties, bool firePropertyValueChangeEvent, object requester)
		{
			this.SetPropertyValueInternal(propertyIndex, value, addOrRemoveInChangedProperties, firePropertyValueChangeEvent, decideActionFromModel: false, enforceAccessModifier: false, requester: requester);
		}

        //protected internal void SetPropertyValueInternal(int propertyIndex, object value, bool firePropertyValueChangeEvent, bool addOrRemoveInChangedProperties, object requester)
        //{
        //	object currentValue = this.GetInternalPropertyValue(propertyIndex);
        //	object oldValue = this.GetInternalOldPropertyValue(propertyIndex);

        //	bool isDifferentThanCurrent = !Comparison.IsEqual(value, currentValue);
        //	bool isDifferentThanOld = !Comparison.IsEqual(value, oldValue);

        //	if (isDifferentThanCurrent)
        //	{
        //		if (firePropertyValueChangeEvent)
        //		{
        //			this.BeforePropertyValueIsChanged(requester, propertyIndex, currentValue, value);
        //		}

        //		this.SetInternalPropertyValue(propertyIndex, value);

        //		if (firePropertyValueChangeEvent)
        //		{
        //			this.PropertyValueIsChanged(requester, propertyIndex, value, currentValue);
        //		}
        //	}

        //	if (addOrRemoveInChangedProperties)
        //	{
        //		if (isDifferentThanOld)
        //		{
        //			if (!this.changedPropertyIndexes.Contains(propertyIndex))
        //			{
        //				this.RunChangedPropertyNamesAction(() => this.changedPropertyIndexes.Add(propertyIndex));
        //			}
        //		}
        //		else
        //		{
        //			if (this.changedPropertyIndexes.Contains(propertyIndex))
        //			{
        //				this.RunChangedPropertyNamesAction(() => this.changedPropertyIndexes.Remove(propertyIndex));
        //			}
        //		}
        //	}
        //}

        //protected internal virtual void DeleteInternal(object requester)
        //{
        //    lock (lockObject)
        //    {
        //        this.Manager.DeleteInternal(requester, this);
        //    }
        //}


        /// <summary>
        /// Gets the property values dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with property index as a key.
        /// </summary>
        /// <returns>The dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with field values by index.</returns>
        protected internal IDictionary<int, object> GetPropertyValuesByIndex()
        {
            return this.GetPropertyValuesByIndex(null);
        }

        /// <summary>
        /// Gets the datastore field values dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with property index as a key.
        /// Each returned property value is calculated by getValueIterator method given. If <param name="normalizer"> is null returned result is unchanged property value.
        /// </summary>
        /// <param name="normalizer">The method to calculate returned item value.</param>
        /// <returns>The dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with property values by index.</returns>
        protected internal IDictionary<int, object> GetPropertyValuesByIndex(Func<IPropertyModel, object, object> normalizer)
		{
			lock (this.lockObject)
			{
				Dictionary<int, object> result = new Dictionary<int, object>();

				foreach (IPropertyModel propertyModel in this.GetModel().PropertyModels)
				{
					if (!propertyModel.IsStorable)
						continue;

					object value = this.GetFieldValue(propertyModel.PropertyIndex);

                    if (normalizer != null)
                        value = normalizer(propertyModel, value);

                    if (value != propertyModel.DatastoreType.GetDefaultValue())
						result.Add(propertyModel.PropertyIndex, value);
				}

				return result;
			}
		}

        /// <summary>
        /// Gets the property values dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with property name as a key.
        /// </summary>
        /// <returns>The dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with field values by proeprty name.</returns>
        protected internal IDictionary<string, object> GetPropertyValuesByName()
        {
            return this.GetPropertyValuesByName(null);
        }

        /// <summary>
        /// Gets the datastore field values dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with property name as a key.
        /// Each returned property value is calculated by <param name="normalizer"> method given. If <param name="normalizer"> is null returned result is unchanged property value.
        /// </summary>
        /// <param name="normalizer">The method to calculate returned item value.</param>
        /// <returns></returns>
        protected internal IDictionary<string, object> GetPropertyValuesByName(Func<IPropertyModel, object, object> normalizer)
		{
			lock (this.lockObject)
			{
				Dictionary<string, object> result = new Dictionary<string, object>();

				foreach (IPropertyModel propertyModel in this.GetModel().PropertyModels)
				{
					if (!propertyModel.IsStorable)
						continue;

					object value = this.GetFieldValue(propertyModel.PropertyIndex);

                    if (normalizer != null)
                        value = normalizer(propertyModel, value);

                    if (value != propertyModel.DatastoreType.GetDefaultValue())
						result.Add(propertyModel.PropertyName, value);
				}

				return result;
			}
		}

        protected internal IDictionary<int, object> GetOldPropertyValuesByIndex()
        {
            return this.GetOldPropertyValuesByIndex(null);
        }

        protected internal IDictionary<int, object> GetOldPropertyValuesByIndex(Func<IPropertyModel, object, object> normalizer)
		{
			lock (this.lockObject)
			{
				Dictionary<int, object> result = new Dictionary<int, object>();

				foreach (IPropertyModel propertyModel in this.GetModel().PropertyModels)
				{
					if (!propertyModel.IsStorable)
						continue;

					object value = this.GetOldFieldValue(propertyModel.PropertyIndex);

                    if (normalizer != null)
                        value = normalizer(propertyModel, value);

                    if (value != propertyModel.DatastoreType.GetDefaultValue())
						result.Add(propertyModel.PropertyIndex, value);
				}

				return result;
			}
		}


        protected internal IDictionary<string, object> GetOldPropertyValuesByName()
        {
            return this.GetOldPropertyValuesByName(null);
        }

        protected internal IDictionary<string, object> GetOldPropertyValuesByName(Func<IPropertyModel, object, object> normalizer)
		{
			lock (this.lockObject)
			{
				Dictionary<string, object> result = new Dictionary<string, object>();

				foreach (IPropertyModel propertyModel in this.GetModel().PropertyModels)
				{
					if (!propertyModel.IsStorable)
						continue;

					object value = this.GetOldFieldValue(propertyModel.PropertyIndex);

                    if (normalizer != null)
                        value = normalizer(propertyModel, value);

                    if (value != propertyModel.DatastoreType.GetDefaultValue())
						result.Add(propertyModel.PropertyName, value);
				}

				return result;
			}
		}

        protected internal IDictionary<int, object> GetChangedPropertyValuesByIndex()
        {
            return this.GetChangedPropertyValuesByIndex(null);
        }

        protected internal IDictionary<int, object> GetChangedPropertyValuesByIndex(Func<IPropertyModel, object, object> normalizer)
		{
			lock (this.lockObject)
			{
				Dictionary<int, object> result = new Dictionary<int, object>();

				foreach (int propertyIndex in this.changedPropertyIndexes)
				{
					IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

					if (!propertyModel.IsStorable)
						continue;

					object value = this.GetFieldValue(propertyIndex);

                    if (normalizer != null)
                        value = normalizer(propertyModel, value);

                    result.Add(propertyModel.PropertyIndex, value);
				}

				return result;
			}
		}

        protected internal IDictionary<string, object> GetChangedPropertyValuesByName()
        {
            return this.GetChangedPropertyValuesByName(null);
        }

        protected internal IDictionary<string, object> GetChangedPropertyValuesByName(Func<IPropertyModel, object, object> normalizer)
		{
			lock (this.lockObject)
			{
				Dictionary<string, object> result = new Dictionary<string, object>();

				foreach (int propertyIndex in this.changedPropertyIndexes)
				{
					IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

					if (!propertyModel.IsStorable)
						continue;

					object value = this.GetFieldValue(propertyModel.PropertyIndex);

                    if (normalizer != null)
                        value = normalizer(propertyModel, value);

                    result.Add(propertyModel.PropertyName, value);
				}

				return result;
			}
		}

        protected internal IDictionary<int, object> GetChangedOldPropertyValuesByIndex()
        {
            return this.GetChangedOldPropertyValuesByIndex(null);
        }

        protected internal IDictionary<int, object> GetChangedOldPropertyValuesByIndex(Func<IPropertyModel, object, object> normalizer)
		{
			lock (this.lockObject)
			{
				Dictionary<int, object> result = new Dictionary<int, object>();

				foreach (int propertyIndex in this.changedPropertyIndexes)
				{
					IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

					if (!propertyModel.IsStorable)
						continue;

					object value = this.GetOldFieldValue(propertyModel.PropertyIndex);

                    if (normalizer != null)
                        value = normalizer(propertyModel, value);

                    result.Add(propertyModel.PropertyIndex, value);
				}

				return result;
			}
		}

        protected internal IDictionary<string, object> GetChangedOldPropertyValuesByName()
        {
            return this.GetChangedOldPropertyValuesByName(null);
        }

        protected internal IDictionary<string, object> GetChangedOldPropertyValuesByName(Func<IPropertyModel, object, object> normalizer)
		{
			lock (this.lockObject)
			{
				Dictionary<string, object> result = new Dictionary<string, object>();

				foreach (int propertyIndex in this.changedPropertyIndexes)
				{
					IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

					if (!propertyModel.IsStorable)
						continue;

					object value = this.GetOldFieldValue(propertyIndex);

                    if (normalizer != null)
                        value = normalizer(propertyModel, value);

                    //fieldValue = this.CreateNormalizedPropertyDataToSaveInDatastore(propertyModel, fieldValue);

                    result.Add(propertyModel.PropertyName, value);
				}

				return result;
			}
		}

        //TODO: Move this methods to ObjectManager or Datastore, what ever.

        #endregion |   Internal Protected Methods   |

        #region |   Internal Methods   |

        internal string GetImageNameInternal()
        {
            return this.imageName;
        }
        
        internal void SetImageNameInternal(string imageName, string oldImageName, bool fireOnImageNameChange)
        {
            this.imageName = imageName;

            if (fireOnImageNameChange)
                this.OnImageNameChange(imageName, oldImageName);
        }
        
		//internal void ActionBeforeLoading(object requester)
		//{
		//	this.OnBeforeLoading(requester);
		//}

		//internal void ActionAfterLoad(object requester)
		//{
		//	this.OnAfterLoad(requester);
		//}

        internal void ActionBeforeSaving(object requester)
        {
            this.OnBeforeSaving(requester);
        }

        internal void ActionOnSaving(object requester)
        {
            this.OnSaving(requester);
        }

        internal void ActionAfterSave(object requester, bool isNewBeforeSaving)
        {
            this.OnAfterSave(requester, isNewBeforeSaving);
        }

        internal void ActionBeforeDeleting(object requester)
        {
            this.OnBeforeDeleting(requester);
        }

        #endregion |   Internal Methods   |

		#region |   Protected Abstract Methods   |

		protected abstract object GetFieldValue(int propertyIndex);
		protected abstract object GetOldFieldValue(int propertyIndex);
		protected abstract void SetFieldValue(int propertyIndex, object value);
		protected abstract void SetOldFieldValue(int propertyIndex, object value);

		#endregion |   Protected Abstract Methods   |

		#region |   Protected Methods   |

		protected void SetPropertyValue<T>(int propertyIndex, T value, ref T propertyValue, ref T oldPropertyValue)
		{
			this.SetPropertyValueInternal<T>(propertyIndex, value, ref propertyValue, ref oldPropertyValue, false, false, decideActionFromModel: true, requester: null);
		}
		
		protected void SetPropertyValueInternal<T>(int propertyIndex, T value, ref T propertyValue, ref T oldPropertyValue, bool firePropertyValueChangeEvent, bool addOrRemoveInChangedProperties, object requester)
		{
			this.SetPropertyValueInternal<T>(propertyIndex, value, ref propertyValue, ref oldPropertyValue, firePropertyValueChangeEvent, addOrRemoveInChangedProperties, decideActionFromModel: false, requester: requester);
		}

        protected void ObjectIsCreated()
        {
            this.ObjectManager.ObjectIsCreated(null, this);
            this.OnCreated(null);
        }

		//protected void SetModel(IEasyObjectModel model)
		//{
		//	this.model = model;
		//}

		protected void Load(object requester, IDictionary<int, object> propertyDataByIndex, bool acceptChanges, Func<IPropertyModel, object, object> normalizer)
		{
            lock (this.lockObject)
            {
                foreach (var item in propertyDataByIndex)
                {
                    object fieldValue = item.Value;

                    if (normalizer != null)
                        fieldValue = normalizer(this.GetModel().PropertyModels[item.Key], fieldValue);

                    this.SetFieldValue(item.Key, fieldValue);
                }

                if (acceptChanges)
                    this.AcceptChanges(requester);

                this.OnAfterLoad();
            }
		}

		protected void Load(object requester, IDictionary<string, object> propertyDataByName, bool acceptChanges, Func<IPropertyModel, object, object> normalizer)
		{
            lock (this.lockObject)
            {
                foreach (var item in propertyDataByName)
                {
                    object fieldValue = item.Value;
                    IPropertyModel propertyModel = this.GetModel().PropertyModels[item.Key];

                    if (normalizer != null)
                        fieldValue = normalizer(propertyModel, fieldValue);

                    this.SetFieldValue(propertyModel.PropertyIndex, fieldValue);
                }

                if (acceptChanges)
                    this.AcceptChanges(requester);

                this.OnAfterLoad();
            }
		}

		protected virtual void OnAfterLoad()
		{
		}
		
		protected void Save()
        {
            this.Save(null);
        }

        protected void Save(object requester)
        {
            this.ObjectManager.Save(requester, this);
        }

        protected void Delete()
        {
            this.Delete(null);
        }

        protected void Delete(object requester)
        {
            this.ObjectManager.Delete(requester, this);
        }

        protected EasyObjectValidationResult Validate()
        {
            EasyObjectValidationResult validationResult = this.ObjectManager.Validate(this);
            return validationResult;
        }

        protected void OnImageNameChange(string imageName, string oldImageName)
        {
        }

        protected virtual void OnCreated(object requester)
        {
        }
        
		//protected virtual void OnBeforeLoading(object requester)
		//{
		//}

		//protected virtual void OnAfterLoad(object requester)
		//{
		//}

        protected virtual void OnBeforeSaving(object requester)
        {
        }

        protected virtual void OnSaving(object requester)
        {
        }

        protected virtual void OnAfterSave(object requester, bool isNewBeforeSaving)
        {
        }

        protected virtual void OnBeforeDeleting(object requester)
        {
        }

        protected virtual void OnBeforePropertyValueChange(object requester, int propertyIndex, object value, object newValue)
        {
        }

        protected virtual void OnPropertyValueChange(object requester, int propertyIndex, object value, object oldValue)
        {
        }

        protected virtual void OnChangedPropertiesCountChange(int changedPropertiesCount, int oldChangedPropertiesCount)
        {
        }

        #endregion |   Protected Methods   |

        #region |   Private Raise Event Methods   |

        private void RaiseBeforePropertyValueChange(object requester, int propertyIndex, object value, object newValue)
        {
            if (this.BeforePropertyValueChange != null)
            {
                this.BeforePropertyValueChange(this, new BeforeChangePropertyValueEasyObjectRequesterEventArgs(requester, this, propertyIndex, value, newValue));
            }
        }

        private void RaisePropertyValueChange(object requester, int propertyIndex, object value, object oldValue)
        {
            if (this.PropertyValueChange != null)
            {
                this.PropertyValueChange(this, new ChangePropertyValueEasyObjectRequesterEventArgs(requester, this, propertyIndex, value, oldValue));
            }
        }

        private void RaiseChangedPropertiesCountChange(int changedPropertiesCount, int oldChangedPropertiesCount)
        {
            if (this.ChangedPropertiesCountChange != null)
            {
                this.ChangedPropertiesCountChange(this, new CountChangeEasyObjectEventArgs(this, changedPropertiesCount, oldChangedPropertiesCount));
            }
        }


        #endregion |   Private Raise Event Methods   |
        
        #region |   Private Methods   |


        private void SetPropertyValueInternal<T>(int propertyIndex, T value, ref T propertyValue, ref T oldPropertyValue, bool firePropertyValueChangeEvent, bool addOrRemoveInChangedProperties, bool decideActionFromModel, object requester)
		{
			if (this.IsReadOnly)
				throw new NotSupportedException("The property is about to be set and the EasyObject is read-only.");

			IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

			if (propertyModel == null && this.ObjectManager.EnforcePropertyExistanceInModel)
				throw new NotSupportedException("The property is about to be set and the property name does not exist in the model property definition: Object Type = " + this.GetType().Name + ", Property Index = " + propertyIndex);

			// Allow sets property for protected internal methods
			//if (propertyModel.AccessPolicy == PropertyAccessPolicy.ReadOnly)
			//	throw new NotSupportedException("The property is about to be set and the property model access policy is read-only: Property Name = " + propertyIndex);


			lock (lockObject)
			{
				if (Comparison.IsEqual<T>(value, propertyValue))
					return;
				
				T previousPropertyValue = propertyValue;
				bool doFirePropertyValueChangeEvent = (decideActionFromModel && propertyModel != null && propertyModel.FirePropertyValueChangeEvent) || (!decideActionFromModel && firePropertyValueChangeEvent);
				bool doAddOrRemoveInChangedProperties = (decideActionFromModel && propertyModel != null && propertyModel.AddOrRemoveInChangedProperties) || (!decideActionFromModel && addOrRemoveInChangedProperties);
				
				this.BeforePropertyValueIsChanged(this, propertyIndex, propertyValue, value);
				propertyValue = value;


				int oldChangedPropertiesCount = this.changedPropertyIndexes.Count;

				if (doAddOrRemoveInChangedProperties)
				{
					if (!Comparison.IsEqual<T>(value, oldPropertyValue))
					{
						this.changedPropertyIndexes.Add(propertyIndex);

					}
					else
					{
						this.changedPropertyIndexes.Remove(propertyIndex);
					}
				}

				if (doFirePropertyValueChangeEvent)
					this.PropertyValueIsChanged(this, propertyIndex, value, previousPropertyValue);

				if (doAddOrRemoveInChangedProperties && oldChangedPropertiesCount != this.changedPropertyIndexes.Count)
					this.ChangedPropertiesCountIsChanged(this.changedPropertyIndexes.Count, oldChangedPropertiesCount);
			}
		}

		private void SetPropertyValueInternal(int propertyIndex, object value, bool addOrRemoveInChangedProperties, bool firePropertyValueChangeEvent, bool decideActionFromModel, bool enforceAccessModifier, object requester)
		{
			if (this.isDeleteStarted || this.isDeleted)
				return;

			if (this.IsReadOnly)
				throw new NotSupportedException("The property is about to be set and the EasyObject is read-only.");

			IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

			if ((propertyModel == null && this.ObjectManager.EnforcePropertyExistanceInModel) || (decideActionFromModel && propertyModel == null))
				throw new NotSupportedException("The property is about to be set and the property name does not exist in the model property definition: Object Type = " + this.GetType().Name + ", Property Index = " + propertyIndex);

			if (enforceAccessModifier && propertyModel != null && propertyModel.AccessPolicy == PropertyAccessPolicy.ReadOnly)
				throw new NotSupportedException("The property is about to be set and the property model access policy is read-only: Object Type = " + this.GetType().Name + ", Property Index = " + propertyIndex);


			lock (lockObject)
			{
				object propertyValue = this.GetFieldValue(propertyIndex);

				if (Comparison.IsEqual(value, propertyValue))
					return;

				object oldPropertyValue = this.GetOldFieldValue(propertyIndex);
				object previousPropertyValue = propertyValue;
				bool doFirePropertyValueChangeEvent = (decideActionFromModel && propertyModel != null && propertyModel.FirePropertyValueChangeEvent) || (!decideActionFromModel && firePropertyValueChangeEvent);
				bool doAddOrRemoveInChangedProperties = (decideActionFromModel && propertyModel != null && propertyModel.AddOrRemoveInChangedProperties) || (!decideActionFromModel && addOrRemoveInChangedProperties);

				this.BeforePropertyValueIsChanged(this, propertyIndex, propertyValue, value);
				this.SetFieldValue(propertyIndex, value);

				int oldChangedPropertiesCount = this.changedPropertyIndexes.Count;

				if (doAddOrRemoveInChangedProperties)
				{
					if (!Comparison.IsEqual(value, oldPropertyValue))
					{
						this.changedPropertyIndexes.Add(propertyIndex);
					}
					else
					{
						this.changedPropertyIndexes.Remove(propertyIndex);
					}
				}

				if (doFirePropertyValueChangeEvent)
					this.PropertyValueIsChanged(requester, propertyIndex, value, previousPropertyValue);

				if (doAddOrRemoveInChangedProperties && oldChangedPropertiesCount != this.ChangedPropertiesCount)
					this.ChangedPropertiesCountIsChanged(this.changedPropertyIndexes.Count, oldChangedPropertiesCount);
			}
		}

		private void RunChangedPropertyNamesAction(Action changedPropertyNamesAction)
        {
            int oldChangedPropertiesCount = this.ChangedPropertiesCount;
            changedPropertyNamesAction();

			if (this.ChangedPropertiesCount != oldChangedPropertiesCount)
				this.ChangedPropertiesCountIsChanged(this.ChangedPropertiesCount, oldChangedPropertiesCount);
        }

        private void BeforePropertyValueIsChanged(object requester, int propertyIndex, object value, object newValue)
        {
            this.ObjectManager.BeforePropertyValueIsChanged(requester, this, propertyIndex, value, newValue);
            this.OnBeforePropertyValueChange(requester, propertyIndex, value, newValue);
            this.RaiseBeforePropertyValueChange(requester, propertyIndex, value, newValue);
        }

        private void PropertyValueIsChanged(object requester, int propertyIndex, object value, object oldValue)
        {
			this.ObjectManager.PropertyValueIsChanged(requester, this, propertyIndex, value, oldValue);
			this.OnPropertyValueChange(requester, propertyIndex, value, oldValue);
			this.RaisePropertyValueChange(requester, propertyIndex, value, oldValue);
        }

        private void ChangedPropertiesCountIsChanged(int changedPropertiesCount, int oldChangedPropertiesCount)
        {
            this.ObjectManager.ChangedPropertiesCountIsChanged(this, changedPropertiesCount, oldChangedPropertiesCount);
            this.OnChangedPropertiesCountChange(changedPropertiesCount, oldChangedPropertiesCount);
            this.RaiseChangedPropertiesCountChange(changedPropertiesCount, oldChangedPropertiesCount);
        }

		#endregion |   Private Methods   |

		#region |   IBindingEasyObject Interface   |

		//object IBindingObject.GetOldPropertyValue(string propertyName)
		//{
		//	return this.GetPropertyValue(propertyName, this.OldPropertyValues);
		//}

		//T IBindingObject.GetOldPropertyValue<T>(string propertyName)
		//{
		//	return (T)this.GetPropertyValue(propertyName, this.OldPropertyValues);
		//}


		//object IPropertyValue.GetPropertyValue(string propertyName)
		//{
		//	IObjectProperty propertyModel = this.GetModel().ObjectPropertys.GetModel(propertyName);

		//	if (propertyModel != null)
		//		return this.GetPropertyValue(propertyModel.Index);

		//	return null; 
		//}

		//void IPropertyValue.SetPropertyValue(string propertyName, object value)
		//{
		//	IObjectProperty propertyModel = this.GetModel().ObjectPropertys.GetModel(propertyName);

		//	if (propertyModel != null)
		//		this.SetPropertyValue(propertyModel.Index, value);
		//}

		#endregion |   IBindingEasyObject Interface   |

		#region |   Dispose   |

		protected virtual void Dispose()
        {
            this.changedPropertyIndexes = null;
			this.tag = null;
			this.objectManager = null;
        }
        
        void IDisposable.Dispose()
        {
            this.Dispose();
        }

        #endregion |   Dispose   |
    }
}