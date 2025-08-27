using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Data;
using Simple;
using Simple.Collections;
using Simple.Modeling;
using Simple.Serialization;
using Simple.Security;

namespace Simple.Objects
{
	partial class SimpleObject : IBindingSimpleObject, IPropertyValue, IEqualityComparer , IDisposable
	{
		#region |   Protected Members   |

		protected readonly object lockObject = new object();

		#endregion |   Protected Members   |

		#region |   Private Members   |

		private SimpleObjectManager manager;
		private bool isNew = true;
		//private bool isDeleteStarted = false;
		private bool deleteRequested = false;
		private bool deleteStarted = false;
		private bool isDeleted = false;
		private bool isReadOnly = false;
		private bool isStorable = false;
		////private bool isValidationTest = false;
		//private HashSet<IPropertyModel> changedPropertyModels = new HashSet<IPropertyModel>();
		private SortedSet<int> changedPropertyIndexes = new SortedSet<int>();
		//private HashSet<int> changedSaveablePropertyIndexes = new HashSet<int>();
		//private SortedSet<int> changedSaveablePropertyIndexes = new SortedSet<int>();
		private static Dictionary<long, long> EmptyNewObjectIdsByTempClientObjectIdDictionary = new Dictionary<long, long>();

		private string? imageName = null;
		private object? tag = null;

		private const string imageNameOptionSeparator = ".";

		#endregion |   Private Members   |

		#region |   Constructors and Initialization   |

		//public SimpleObject(EasyObjectManager objectManager)
		//          : this(objectManager, true)
		//      {
		//      }

		//      public SimpleObject(EasyObjectManager objectManager, bool fireObjectCreatedEvent)
		//      {
		//	this.objectManager = objectManager;

		//          if (fireObjectCreatedEvent)
		//              this.ObjectIsCreated();
		//      }

		#endregion |   Constructors and Initialization   |

		#region |   Events   |

		public event BeforeChangePropertyValuePropertyModelSimpleObjectChangeContainerContextRequesterEventHandler? BeforePropertyValueChange;
		public event ChangePropertyValuePropertyModelSimpleObjectChangeContainerRequesterEventHandler? PropertyValueChange;
		//public event ChangePropertyValueSimpleObjectRequesterEventHandler? SaveablePropertyValueChange;
		public event CountChangeSimpleObjectEventHandler? ChangedPropertiesCountChange;
		//public event CountChangeSimpleObjectEventHandler? ChangedSaveablePropertiesCountChange;

		#endregion |   Events   |

		#region |   Public Properties   |

		public object? this[int propertyIndex]
		{
			get { return this.GetPropertyValue(propertyIndex); }
			set { this.SetPropertyValue(propertyIndex, value); }
		}

		public object? this[string propertyName]
		{
			get { return this.GetPropertyValue(propertyName); }
			set { this.SetPropertyValue(propertyName, value); }
		}

		public bool DeleteStarted
		{
			get { return this.deleteStarted; }
			protected internal set { this.deleteStarted = value; }
		}

		public bool DeleteRequested
		{
			get { return this.deleteRequested; }
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

		public bool IsStorable
		{
			get { return this.isStorable; }
			set { this.isStorable = value; }
		}

		public int ChangedPropertiesCount
		{
			get { return this.changedPropertyIndexes.Count; }
		}

		//public int ChangedSaveablePropertiesCount
		//{
		//	get { return this.changedSaveablePropertyIndexes.Count; }
		//}

		public int[] GetChangedPropertyIndexes()
		{
			return (!this.IsDeleted) ? this.changedPropertyIndexes.ToArray() : new int[] { };
		}

		//public int[] GetChangedSaveablePropertyIndexes()
		//{
		//	return (!this.IsDeleted) ? this.changedSaveablePropertyIndexes.ToArray() : new int[] { };
		//}

		public List<int> GetChangedPrimaryObjectRelationKeys()
		{
			List<int> result = new List<int>();
			int[] changedIndexes = this.GetChangedPropertyIndexes();

			foreach (IOneToOneOrManyRelationModel relationModel in this.GetModel().RelationModel.AsForeignObjectInRelarions)
				if (changedIndexes.Contains(relationModel.PrimaryObjectIdPropertyModel.PropertyIndex) || (relationModel.PrimaryTableIdPropertyModel != null && changedIndexes.Contains(relationModel.PrimaryTableIdPropertyModel.PropertyIndex)))
					result.Add(relationModel.RelationKey);

			return result;
		}

		public IEnumerable<SimpleObject> GetChangedPrimaryObjects()
		{
			return from relationKey in this.GetChangedPrimaryObjectRelationKeys()
				   select this.GetRelationPrimaryObject(relationKey);
		}

		public IEnumerable<SimpleObject> GetAllForeignObjects()
		{
			return from relationKey in this.GetModel().RelationModel.ForeignObjectKeys
				   select this.GetRelationPrimaryObject(relationKey);
		}

		//public IPropertyModel[] GetChangedPropertyModels()
		//{
		//	//lock (this.lockObject)
		//	//{
		//	//	HashSet<int> result = new HashSet<int>();

		//	//	foreach (int propertyIndex in this.changedPropertyIndexes)
		//	//	{
		//	//		IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

		//	//		if (!propertyModel.IsStorable)
		//	//			continue;

		//	//		result.Add(propertyIndex);
		//	//	}

		//	//	return result.ToArray();
		//	//}
		//	return (!this.IsDeleted) ? this.changedPropertyModels.ToArray() : new PropertyModel[] { };
		//}


		//public bool IsChanged => this.changedSaveablePropertyIndexes.Count > 0;
		public bool IsChanged => this.changedPropertyIndexes.Count > 0;

		public object? Tag
		{
			get { return this.tag; }
			set { this.tag = value; }
		}

		////public bool IsValidationTest
		////{
		////	get { return this.isValidationTest; }
		////	set { this.isValidationTest = value; }
		////}

		#endregion |   Public Properties   |

		#region |   Private Properties   |

		#endregion |   Private Properties   |

		#region |   Public Methods   |

		public virtual string? GetDescription()
		{
			if (this.GetModel().DescriptionPropertyModel != null)
				return this.GetPropertyValue<string>(this.GetModel().DescriptionPropertyModel);

			return null;
		}

		public void SetName(string name)
		{
			if (this.GetModel().NamePropertyModel != null)
				this.SetPropertyValue(this.GetModel().NamePropertyModel, name);
		}

		public void SetDescription(string description)
		{
			if (this.GetModel().DescriptionPropertyModel != null)
				this.SetPropertyValue(this.GetModel().DescriptionPropertyModel, description);
		}

		public GraphElement CreateGraphElement(int graphKey, GraphElement? parent, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			return this.GetOrCreateGraphElement(graphKey, simpleObject: this, parent, changeContainer, context, requester);
		}

		public T GetPropertyValue<T>(IPropertyModel propertyModel)
		{
			return this.GetPropertyValue<T>(propertyModel.PropertyIndex);
		}

		public T GetPropertyValue<T>(IPropertyModel propertyModel, T defaultValue)
		{
			return this.GetPropertyValue<T>(propertyModel.PropertyIndex, defaultValue);
		}

		public T GetPropertyValue<T>(string propertyName)
		{
			return this.GetPropertyValue<T>(this.GetModel().PropertyModels[propertyName].PropertyIndex, default);
		}

		public T GetPropertyValue<T>(int propertyIndex)
		{
			return (T)this.GetFieldValue(propertyIndex)!;
		}

		public T GetPropertyValue<T>(int propertyIndex, T defaultValue)
		{
			object? value = this.GetPropertyValue(propertyIndex);

			if (value == null)
				return defaultValue;
			else if (value.GetType() != typeof(T))
				return Conversion.TryChangeType<T>(value, defaultValue);
			else
				return (T)value;
		}

		public object? GetPropertyValue(IPropertyModel propertyModel)
		{
			return this.GetPropertyValue(propertyModel.PropertyIndex);
		}

		//public object GetPropertyValue(string propertyName, bool normalizeValue)
		//{
		//	int propertyIndex = this.GetModel().PropertyModels[propertyName].Index;
		//	return (normalizeValue) ? this.GetPropertyValue(propertyIndex) : this.GetFieldValue(propertyIndex);
		//}


		public virtual object? GetPropertyValue(string propertyName)
		{
			IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyName];
			object? result = null;

			if (propertyModel != null)
				result = this.GetPropertyValue(propertyModel.PropertyIndex);

			return result;
		}

		public virtual object? GetPropertyValue(int propertyIndex)
		{
			return this.GetFieldValue(propertyIndex);
		}

		public virtual object? GetOldPropertyValue(int propertyIndex)
		{
			return this.GetOldFieldValue(propertyIndex);
		}


		public void SetPropertyValue(string propertyName, object? value)
		{
			this.SetPropertyValue(propertyName, value, context: ObjectActionContext.Unspecified);
		}

		public void SetPropertyValue(string propertyName, object? value, ObjectActionContext context)
		{
			this.SetPropertyValue(propertyName, value, context, this.Requester);
		}

		public void SetPropertyValue(string propertyName, object? value, object? requester)
		{
			this.SetPropertyValue(propertyName, value, context: ObjectActionContext.Unspecified, requester);
		}

		public void SetPropertyValue(string propertyName, object? value, ObjectActionContext context, object? requester)
		{
			this.SetPropertyValue(propertyName, value, this.ChangeContainer, context, requester);
		}

		public void SetPropertyValue(string propertyName, object? value, ChangeContainer? changeContainer, ObjectActionContext context)
		{
			this.SetPropertyValue(propertyName, value, changeContainer, context, this.Requester);
		}

		public void SetPropertyValue(string propertyName, object? value, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyName];

			this.SetPropertyValue(propertyModel, value, changeContainer, context, requester);
		}

		public void SetPropertyValue(int propertyIndex, object? value)
		{
			this.SetPropertyValue(propertyIndex, value, this.Requester);
		}

		public void SetPropertyValue(int propertyIndex, object? value, object? requester)
		{
			this.SetPropertyValue(propertyIndex, value, ObjectActionContext.Unspecified, requester);
		}

		public void SetPropertyValue(int propertyIndex, object? value, ObjectActionContext context)
		{
			this.SetPropertyValue(propertyIndex, value,  context, this.Requester);
		}

		public void SetPropertyValue(int propertyIndex, object? value, ObjectActionContext context, object? requester)
		{
			this.SetPropertyValue(propertyIndex, value, this.ChangeContainer, context, requester);
		}

		public void SetPropertyValue(int propertyIndex, object? value, ChangeContainer? changeContainer, ObjectActionContext context)
		{
			this.SetPropertyValue(propertyIndex, value, changeContainer, context, this.Requester);
		}

		public void SetPropertyValue(int propertyIndex, object? value, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

			this.SetPropertyValue(propertyModel, value, changeContainer, context, requester);
		}

		public void SetPropertyValue(IPropertyModel propertyModel, object? value)
		{
			this.SetPropertyValue(propertyModel, value, this.Requester);
		}

		public void SetPropertyValue(IPropertyModel propertyModel, object? value, object? requester)
		{
			this.SetPropertyValue(propertyModel, value, this.Context, requester);
		}

		public void SetPropertyValue(IPropertyModel propertyModel, object? value, ObjectActionContext context)
		{
			this.SetPropertyValue(propertyModel, value, context, this.Requester);
		}

		public void SetPropertyValue(IPropertyModel propertyModel, object? value, ObjectActionContext context, object? requester)
		{
			this.SetPropertyValue(propertyModel, value, this.ChangeContainer, context, requester);
		}

		public void SetPropertyValue(IPropertyModel propertyModel, object? value, ChangeContainer? changeContainer, ObjectActionContext context)
		{
			this.SetPropertyValue(propertyModel, value, changeContainer, context, this.Requester);
		}

		public void SetPropertyValue(IPropertyModel propertyModel, object? value, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			this.SetPropertyValue(propertyModel, value, propertyModel.TrimStringBeforeComparison, changeContainer, context, requester);
		}

		public void SetPropertyValue(IPropertyModel propertyModel, object? value, bool trimStringBeforeComparison, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			if (propertyModel.AccessPolicy == PropertyAccessPolicy.ReadOnly && (!this.IsNew)) // && !propertyModel.CanSetOnClientUpdate))
				throw new NotSupportedException("The property is about to be set and the property model access policy is read-only: Object Type = " + this.GetType().Name + ", PropertyIndex=" + propertyModel.PropertyIndex + " (" + propertyModel.PropertyName + ")");

			this.SetPropertyValuePrivate(propertyModel, value, trimStringBeforeComparison, changeContainer, context, requester);
		}

		public void AcceptChanges()
		{
			this.AcceptChanges(this.Requester);
		}

		public void AcceptChanges(object? requester)
		{
			this.AcceptChanges(this.ChangeContainer, requester);
		}

		public void AcceptChanges(ChangeContainer? changeContainer)
		{
			this.AcceptChanges(changeContainer, this.Requester);
		}

		public void AcceptChanges(ChangeContainer? changeContainer, object? requester)
		{
			this.AcceptChangesInternal(changeContainer, requester);
		}

		internal void AcceptChangesInternal(ChangeContainer? changeContainer, object? requester)
		{
			lock (this.lockObject)
			{
				this.OnBeforeAcceptChanges();

				foreach (IPropertyModel propertyModel in this.GetModel().PropertyModels)
				{
					object? value = this.GetFieldValue(propertyModel.PropertyIndex);
					
					this.SetOldFieldValue(propertyModel.PropertyIndex, value!);
				}

				this.RunChangedPropertyCountAction(() => this.changedPropertyIndexes.Clear(), changeContainer, requester); //this.changedSaveablePropertyIndexes.Clear();

				changeContainer?.Unset(this, TransactionRequestAction.Save, requester);

				this.changeContainer = this.defaultChangeContainer;
				this.context = this.defaultContext;
				this.requester = this.defaultRequester;
				this.OnAcceptChanges();
			}
		}

		public void RejectChanges()
		{
			this.RejectChanges(this.Requester);
		}

		public void RejectChanges(object? requester)
		{
			this.RejectChanges(this.ChangeContainer, requester);
		}

		public void RejectChanges(ChangeContainer? changeContainer, object? requester)
		{
			this.RejectChanges(changeContainer, ObjectActionContext.Unspecified, requester);
		}

		public void RejectChanges(ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			this.RejectChangesIntrenal(changeContainer, context, requester);
		}

		/// <summary>
		/// Reject object property changes. Properties with IsSystem = true specified by object property model definition are not changed.
		/// </summary>
		/// <param name="requester">The initial action caller.</param>
		internal void RejectChangesIntrenal(ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			if (this.IsNew) // Rejecting changes on new object is not allowed
				return;

			if (context == ObjectActionContext.Unspecified)
				context = (this.Manager.WorkingMode == ObjectManagerWorkingMode.Server) ? ObjectActionContext.ServerTransaction : ObjectActionContext.Client;

			lock (this.lockObject)
			{
				List<int> processedPropertyIndexes = new List<int>();

				// Goes throw the realation object first and revert changes, first

				foreach (var relationModel in this.GetModel().RelationModel.AsForeignObjectInRelarions.ToArray())
				{
					SimpleObject? oldForeignObject = this.GetRelationOldPrimaryObject(relationModel);

					this.SetRelationPrimaryObject(oldForeignObject, relationModel, changeContainer, context, requester);
				}

				//// OneToOne relations
				//foreach (IOneToOneRelationModel oneToOneRelationModel in this.GetModel().RelationModel.OneToOneForeignObjectModels) //  ObjectRelationModel.OneToOneRelationKeyHolderObjectDictionary)
				//{
				//	SimpleObject oldForeignObject = this.GetOneToOneRelationForeignObject<SimpleObject>(oneToOneRelationModel); //OneToOneForeignObjectsByRelationModelKey[oneToOneRelationModel.Key] as SimpleObject;

				//	if (oneToOneRelationModel.ForeignTableIdPropertyModel != null)
				//	{
				//		this.RejectPropertyValueChange(oneToOneRelationModel.ForeignTableIdPropertyModel, callOnPropertyValueChange: true, changeContainer, requester);
				//		processedPropertyIndexes.Add(oneToOneRelationModel.ForeignTableIdPropertyModel.Index);
				//	}

				//	this.RejectPropertyValueChange(oneToOneRelationModel.ForeignObjectIdPropertyModel, callOnPropertyValueChange: true, changeContainer, requester);
				//	processedPropertyIndexes.Add(oneToOneRelationModel.ForeignObjectIdPropertyModel.Index);
				//}

				//// OneToMany relations
				//foreach (IOneToManyRelationModel oneToManyRelationModel in this.GetModel().RelationModel.OneToManyForeignObjectModels) //  ObjectRelationModel.OneToOneRelationKeyHolderObjectDictionary)
				//{
				//	SimpleObject oldForeignObject = this.GetOneToManyRelationForeignObject<SimpleObject>(oneToManyRelationModel); //OneToOneForeignObjectsByRelationModelKey[oneToOneRelationModel.Key] as SimpleObject;

				//	if (oneToManyRelationModel.ForeignTableIdPropertyModel != null)
				//	{
				//		this.RejectPropertyValueChange(oneToManyRelationModel.ForeignTableIdPropertyModel, callOnPropertyValueChange: true, changeContainer, requester);
				//		processedPropertyIndexes.Add(oneToManyRelationModel.ForeignTableIdPropertyModel.Index);
				//	}

				//	this.RejectPropertyValueChange(oneToManyRelationModel.ForeignObjectIdPropertyModel, callOnPropertyValueChange: true, changeContainer, requester);
				//	processedPropertyIndexes.Add(oneToManyRelationModel.ForeignObjectIdPropertyModel.Index);
				//}

				// ManyToMany relations are to be found in RelatedTransactionObjects 

				// First set SerializationSequence memebers properties e.g. OrderIndex insted of PreviousId 
				foreach (int index in this.GetChangedPropertyIndexes())
				{
					//IPropertyModel propertyModel = this.GetModel().PropertyModels[index];

					//if (propertyModel.IsClientSeriazable)
					//{
						object? oldValue = this.GetOldPropertyValue(index);
						this.SetPropertyValue(index, oldValue, changeContainer, context, requester);
					//}
				}

				//// than reject saveable property changes
				//foreach (int index in this.GetChangedSaveablePropertyIndexes())
				//{
				//	object? oldValue = this.GetOldPropertyValue(index);
				//	this.SetPropertyValue(index, oldValue);
				//}

				//// Do the rest, if any
				//foreach (int index in this.GetChangedPropertyIndexes())
				//{
				//	object? oldValue = this.GetOldPropertyValue(index);
				//	this.SetPropertyValue(index, oldValue);
				//}

				//// Reject changes in rest properties
				//foreach (IPropertyModel propertyModel in this.GetModel().PropertyModels)
				//{
				//	if (propertyModel.Index == SimpleObject.IndexPropertyId || propertyModel.AvoidRejectChanges || processedPropertyIndexes.Contains(propertyModel.Index))
				//		continue;

				//	this.RejectPropertyValueChange(propertyModel, callOnPropertyValueChange: true, changeContainer, requester);
				//}

				if (this.changedPropertyIndexes.Count > 0)
				{
					//int TODO = 0; // this.changedPropertyIndexes should be empty and the following line will only clear changedPropertyIndexes list but the difference between value and oldValue will remain.
					this.RunChangedPropertyCountAction(() => this.changedPropertyIndexes.Clear(), changeContainer, requester);
				}
			}

			this.OnRejectChanges();
		}

		protected virtual void OnRejectChanges() { }

		/// <summary>
		/// Rejects property value change and return True if any value change was made; otherwise False.
		/// </summary>
		/// <param name="requester">The object requester</param>
		/// <param name="propertyModel">The property model</param>
		/// <param name="callOnPropertyValueChange">If True and if property value is changed, OnPropertyValueChange method will be invoked</param>
		/// <returns>Returns True if any value change was made; otherwise False.</returns>
		protected virtual bool RejectPropertyValueChange(IPropertyModel propertyModel, bool callOnPropertyValueChange, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			bool result = false;
			//object value = null;
			//callOnPropertyValueChange &= this.changedPropertyIndexes.Contains(propertyModel.PropertyIndex);

			object? value = this.GetFieldValue(propertyModel.PropertyIndex);
			object? oldValue = this.GetOldFieldValue(propertyModel.PropertyIndex);

			if (!Comparison.IsEqual(value, oldValue))
			{
				this.SetPropertyValue(propertyModel, oldValue, changeContainer, context, requester);
				result = true;
			}

			return result;
		}



		public string? GetImageName()
		{
			if (this.imageName == null)
				this.RecalcImageName();

			return this.imageName;
		}

		public virtual string? GetDefaultImageName()
		{
			string? result = null;
			
			if (this is GraphElement graphElement)
			{
				if (graphElement.SimpleObject != null)
					result = graphElement.SimpleObject.GetDefaultImageName();
			}
			else
			{
				ISimpleObjectModel objectModel = this.GetModel();

				result = objectModel.ImageName;

				if (objectModel.ObjectSubTypePropertyModel != null) // ObjectSubTypes.Count > 0)
				{
					int objectSubType = this.GetPropertyValue<int>(objectModel.ObjectSubTypePropertyModel);
					IModelElement? subTypeModel;

					if (objectModel.ObjectSubTypes.TryGetValue(objectSubType, out subTypeModel))
						result = subTypeModel.ImageName;
				}
			}

			return result;
		}

		public void SetImageName(string imageName)
		{
			this.imageName = imageName;
			this.RecalcImageName();
		}

		public void RecalcImageName()
		{
			string? newImageName = this.Manager.GetImageNameInternal(this);
			
			if (newImageName != this.imageName)
			{
				string? oldImageName = this.imageName;

				this.imageName = newImageName;
				this.Manager.ImageNameIsChanged(this, this.imageName, oldImageName);
			}
			else
			{
				this.imageName = newImageName;
			}
		}

		public void AddImageNameOption(string imageOption)
		{
			string? imageName = this.GetImageName();
			
			if (imageOption != null && imageName != null && imageName.Trim() != "")
			{
				string[] imageBaseNameAndOptions = imageName.Split(new string[] { imageNameOptionSeparator }, StringSplitOptions.RemoveEmptyEntries);
				string imageBaseName = imageBaseNameAndOptions[0];
				List<string> currentImageOptions = imageBaseNameAndOptions.ToList();
				currentImageOptions.RemoveAt(0);

				if (!currentImageOptions.Contains(imageOption))
					currentImageOptions.Add(imageOption);

				string newImageOptions = String.Join(imageNameOptionSeparator, currentImageOptions.ToArray());

				this.imageName = (newImageOptions == "") ? imageBaseName : imageBaseName + imageNameOptionSeparator + newImageOptions;
			}
			else
			{
				throw new ArgumentException("You try to describe image option and the image name is empty.");
			}
		}

		public void RemoveImageNameOption(string imageOption)
		{
			string? imageName = this.GetImageName();

			if (imageOption != null && imageName != null && imageName.Trim() != "")
			{
				string[] imageBaseNameAndOptions = this.imageName!.Split(new string[] { imageNameOptionSeparator }, StringSplitOptions.RemoveEmptyEntries);
				string imageBaseName = imageBaseNameAndOptions[0];
				List<string> currentImageOptions = imageBaseNameAndOptions.ToList();
				currentImageOptions.RemoveAt(0);

				if (currentImageOptions.Contains(imageOption))
					currentImageOptions.Remove(imageOption);

				string newImageOptions = String.Join(imageNameOptionSeparator, currentImageOptions.ToArray());

				this.imageName = (newImageOptions == "") ? imageBaseName : imageBaseName + imageNameOptionSeparator + newImageOptions;
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

		public virtual bool RequireSaving()
		{
			return !this.DeleteStarted && (this.IsNew || this.IsChanged);
		}

		//public void BeginTransaction()
		//{
		//	lock (this.lockObject)
		//	{
		//		this.isServerTransactionInProgress = true;
		//	}
		//}

		//public void EndTransaction()
		//{
		//	lock (this.lockObject)
		//	{
		//		this.isServerTransactionInProgress = false;
		//	}
		//}

		//protected internal void LoadFrom(IDataReader dataReader, IPropertyModel[] propertyModelsByDataReaderFieldIndex, Func<IPropertyModel, object, object> readNormalizer, bool loadOldValuesAlso)
		/// <summary>
		/// Write object propery values to the serialization stream. 
		/// If writeOnlyPropertyValues is true, only property values are writen, otherwise item length, each pair of property index and property value are written into the stream.
		/// </summary>
		/// <param name="writer">The <see cref="SequenceWriter"></see> to write to.</param>
		/// <param name="propertyIndexes">The property model sequence.</param>
		/// <param name="writeNormalizer">The property value writeing normalizer.</param>
		/// <param name="skipWritingKey">If true, key is not writen, otherwise it does.</param>
		/// <param name="serializationModel">The writing <see cref="SerializationModel"></see>.</param>
		/// <param name="defaultValueOptimization">If True the default optimization during serialization will take place.</param>
		public void WriteTo(ref SequenceWriter writer, IEnumerable<int> propertyIndexes, ServerObjectModelInfo serverObjectPropertyInfo, Func<IPropertyModel, object?, object?> writeNormalizer,
													   SerializationModel serializationModel, bool skipWritingKey, bool defaultValueOptimization)
		{
			lock (this.lockObject)
			{
				if (serializationModel == SerializationModel.SequenceValuesOnly)
				{
					for (int i = 0; i < propertyIndexes.Count(); i++)
					{
						int propertyIndex = propertyIndexes.ElementAt(i);

						if (skipWritingKey && propertyIndex == SimpleObject.IndexPropertyId)
							continue;

						ServerPropertyInfo serverPropertyInfo = serverObjectPropertyInfo.GetPropertyInfo(propertyIndex);
						IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];
						object? propertyValue = this.GetOldPropertyValue(propertyIndex);
						
						propertyValue = writeNormalizer(propertyModel, propertyValue);

						if ((propertyModel.IsId || propertyModel.IsRelationObjectId) && this.Manager.WorkingMode == ObjectManagerWorkingMode.Client && (propertyModel.PropertyTypeId == (int)PropertyTypeId.Int64))
						{
							writer.WriteBoolean((long)propertyValue! < 0); // IsNegative
							propertyValue = Math.Abs((long)propertyValue);
						}

						SimpleObjectManager.WriteObjectPropertyValue(ref writer, propertyModel, propertyValue, serverPropertyInfo.PropertyTypeId, ifDefaultWriteOnlyBoolean: defaultValueOptimization);
					}
				}
				else //if (serializationModel == SerializationModel.IndexValuePairs)
				{
					int propertyCount = 0;
					KeyValuePair<IPropertyModel, object?>[] valuesByIndex = new KeyValuePair<IPropertyModel, object?>[propertyIndexes.Count()];

					for (int i = 0; i < propertyIndexes.Count(); i++)
					{
						int propertyIndex = propertyIndexes.ElementAt(i);
						
						if (skipWritingKey && propertyIndex == SimpleObject.IndexPropertyId)
							continue;

						ServerPropertyInfo serverPropertyInfo = serverObjectPropertyInfo.GetPropertyInfo(propertyIndex);
						IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];
						object? propertyValue = this.GetOldPropertyValue(propertyIndex);
						
						propertyValue = writeNormalizer(propertyModel, propertyValue);

						if (defaultValueOptimization && Comparison.IsEqual(propertyValue, propertyModel.DefaultValue)) // && propertyModel.PropertyType.IsValueType
							continue;

						valuesByIndex[propertyCount++] = new KeyValuePair<IPropertyModel, object?>(propertyModel, propertyValue);
					}

					writer.WriteInt32Optimized(propertyCount);

					for (int i = 0; i < propertyCount; i++)
					{
						IPropertyModel propertyModel = valuesByIndex[i].Key;
						object? propertyValue = valuesByIndex[i].Value;

						writer.WriteInt32Optimized(propertyModel.PropertyIndex);

						if ((propertyModel.IsId || propertyModel.IsRelationObjectId) && this.Manager.WorkingMode == ObjectManagerWorkingMode.Client && (propertyModel.PropertyTypeId == (int)PropertyTypeId.Int64))
						{
							writer.WriteBoolean((long)propertyValue! < 0); // IsNegative
							propertyValue = Math.Abs((long)propertyValue);
						}

						SimpleObjectManager.WriteObjectPropertyValue(ref writer, propertyModel, propertyValue, ifDefaultWriteOnlyBoolean: false);
					}
				}
			}
		}

		/// <summary>
		/// Write object propery values to the serialization stream. 
		/// If writeOnlyPropertyValues is true, only property values are writen, otherwise item length, each pair of property index and property value are written into the stream.
		/// </summary>
		/// <param name="writer">The <see cref="SequenceWriter"></see> to write to.</param>
		/// <param name="propertyIndexes">The property model sequence.</param>
		/// <param name="writeNormalizer">The property value writeing normalizer.</param>
		public void WriteTo(ref SequenceWriter writer, IEnumerable<int> propertyIndexes, ServerObjectModelInfo serverObjectPropertyInfo, Func<IServerPropertyInfo, object?, object?>? writeNormalizer = null)
		{
			lock (this.lockObject)
			{
				int propertyCount = 0;
				KeyValuePair<IServerPropertyInfo, object?>[] valuesByServerPropertyModelInfo = new KeyValuePair<IServerPropertyInfo, object?>[propertyIndexes.Count()];

				for (int i = 0; i < propertyIndexes.Count(); i++)
				{
					int propertyIndex = propertyIndexes.ElementAt(i);
					ServerPropertyInfo serverPropertyInfo = serverObjectPropertyInfo.GetPropertyInfo(propertyIndex);

					if (serverPropertyInfo == null)
						continue; // Property doesnot exists on server, skiped

					object? propertyValue = this.GetOldPropertyValue(propertyIndex);

					if (writeNormalizer != null)
						propertyValue = writeNormalizer(serverPropertyInfo, propertyValue); // normalizer will also change value type if different from server property def

					if (Comparison.IsEqual(propertyValue, serverPropertyInfo.DefaultValue)) // && propertyModel.PropertyType.IsValueType
						continue;

					valuesByServerPropertyModelInfo[propertyCount++] = new KeyValuePair<IServerPropertyInfo, object?>(serverPropertyInfo, propertyValue);
				}

				writer.WriteInt32Optimized(propertyCount);

				for (int i = 0; i < propertyCount; i++)
				{
					IServerPropertyInfo propertyModel = valuesByServerPropertyModelInfo[i].Key;
					object? propertyValue = valuesByServerPropertyModelInfo[i].Value;

					writer.WriteInt32Optimized(propertyModel.PropertyIndex);

					if ((propertyModel.IsRelationObjectId) && this.Manager.WorkingMode == ObjectManagerWorkingMode.Client) //&& (propertyModel.PropertyTypeId == (int)PropertyTypeId.Int64))
					{
						writer.WriteBoolean((long)propertyValue! < 0); // IsNegative
						propertyValue = Math.Abs((long)propertyValue);
					}

					SimpleObjectManager.WriteObjectPropertyValue(ref writer, propertyModel, propertyValue);
				}
			}
		}

		/// <summary>
		/// Write object propery values to the serialization stream. 
		/// If writeOnlyPropertyValues is true, only property values are writen, otherwise item length, each pair of property index and property value are written into the stream.
		/// </summary>
		/// <param name="writer">The <see cref="SequenceWriter"></see> to write to.</param>
		/// <param name="propertyIndexes">The property model sequence.</param>
		public void WriteToWithDefaultValues(ref SequenceWriter writer, int[] propertyIndexes, ServerObjectModelInfo serverObjectPropertyInfo)
		{
			lock (this.lockObject)
			{
				writer.WriteInt32Optimized(propertyIndexes.Length);

				for (int i = 0; i < propertyIndexes.Length; i++)
				{
					int propertyIndex = propertyIndexes[i];
					ServerPropertyInfo serverPropertyInfo = serverObjectPropertyInfo.GetPropertyInfo(propertyIndex);
					IPropertyModel localPropertyModel = this.GetModel().PropertyModels.GetPropertyModel(propertyIndex);
					object? propertyValue;
					
					if (localPropertyModel != null)
					{
						propertyValue = this.GetOldPropertyValue(propertyIndex);

						if (serverPropertyInfo.PropertyTypeId != localPropertyModel.PropertyTypeId) // && propertyModel.PropertyType.IsValueType
							propertyValue = Conversion.TryChangeType(propertyValue, serverPropertyInfo.PropertyTypeId);
					}
					else
					{
						propertyValue = serverPropertyInfo.DefaultValue;
					}

					writer.WriteInt32Optimized(propertyIndex);

					if (serverPropertyInfo.IsSerializationOptimizable)
						writer.WriteOptimized(serverPropertyInfo.PropertyTypeId, propertyValue);
					else
						writer.Write(serverPropertyInfo.PropertyTypeId, propertyValue);
				}
			}
		}


		//public enum SetPropertiesOption
		//{
		//	SetAll,
		//	AvoidSetsRelations,
		//	SetOnlyRelations
		//}

		/////// <summary>
		/////// Gets the values of object array ordered by storable property indexes specified by object model and marked as public.
		/////// </summary>
		/////// <returns>The object property value array.</returns>
		////public object[] GetSerializablePropertyValues()
		////{
		////	return this.GetPropertyValues(this.GetModel().SerializablePropertySequence.IndexSequence, (propertyIndex) => this.GetFieldValue(propertyIndex));
		////}

		///// <summary>
		///// Gets the values of object array ordered by serializable property indexes and specified by object model and marked as public.
		///// Each returned property value is calculated by <param name="normalizer"> method given. If <param name="normalizer"> is null, returned result is unchanged property value.
		///// </summary>
		///// <param name="normalizer">The method to calculate return value.</param>
		///// <returns>The object property value array.</returns>
		//public PropertyValueSequence GetSerializablePropertyValueSequence(Func<IPropertyModel, object, object> normalizer)
		//{
		//	return this.GetPropertyValues(this.GetModel().SerializablePropertySequence, (propertyIndex) => this.GetFieldValue(propertyIndex), normalizer);
		//}

		/////// <summary>
		/////// Gets the old values of object array ordered by storable property indexes specified by object model and marked as public.
		/////// </summary>
		/////// <returns>The object property value array.</returns>
		////public object[] GetSerializableOldPropertyValues()
		////{
		////	return this.GetPropertyValues(this.GetModel().SerializablePropertySequence.IndexSequence, (propertyIndex) => this.GetOldFieldValue(propertyIndex));
		////}

		///// <summary>
		///// Gets the old values of object array ordered by seriazable property indexes and specified by object model and marked as public.
		///// Each returned property value is calculated by <param name="normalizer"> method given. If <param name="normalizer"> is null, returned result is unchanged property value.
		///// </summary>
		///// <param name="normalizer">The method to calculate return value.</param>
		///// <returns>The object property value array.</returns>
		//public PropertyValueSequence GetSerializableOldPropertyValueSequence(Func<IPropertyModel, object, object> normalizer)
		//{
		//	return this.GetPropertyValues(this.GetModel().SerializablePropertySequence, (propertyIndex) => this.GetOldFieldValue(propertyIndex), normalizer);
		//}

		///// <summary>
		///// Gets the property values object array ordered by storable property indexes specified by object model.
		///// </summary>
		///// <returns>The object property value array.</returns>
		//public object[] GetStorablePropertyValues()
		//{
		//	return this.GetPropertyValues(this.GetModel().StorablePropertySequence.IndexSequence, (propertyIndex) => this.GetFieldValue(propertyIndex));
		//}

		///// <summary>
		///// Gets the property values object array ordered by storable property indexes and specified by object model.
		///// Each returned property value is calculated by <param name="normalizer"> method given. If <param name="normalizer"> is null, returned result is unchanged property value.
		///// </summary>
		///// <param name="normalizer">The method to calculate return value.</param>
		///// <returns>The object property value array.</returns>
		//public PropertyValueSequence GetStorablePropertyValueSequence(Func<IPropertyModel, object, object> normalizer)
		//{
		//	return this.GetPropertyValues(this.GetModel().StorablePropertySequence, (propertyIndex) => this.GetFieldValue(propertyIndex), normalizer);
		//}

		//public PropertyValueSequence GetChangedStorablePropertyValueSequence(Func<IPropertyModel, object, object> normalizer)
		//{
		//	lock (this.lockObject)
		//	{
		//		IPropertyModel[] changedPropertyModels = this.GetChangedPropertyIndexes().ToModelSequence(this.GetModel().PropertyModels);
		//		List<IPropertyModel> propertyModels = new List<IPropertyModel>();
		//		List<object> propertyValues = new List<object>();

		//		for (int i = 0; i < changedPropertyModels.Length; i++)
		//		{
		//			IPropertyModel propertyModel = changedPropertyModels[i];

		//			if (!propertyModel.IsStorable)
		//				continue;

		//			propertyModels.Add(propertyModel);

		//			object propertyValue = this.GetFieldValue(propertyModel.Index);
		//			propertyValue = normalizer(propertyModel, propertyValue);

		//			propertyValues.Add(propertyValue);
		//		}

		//		return new PropertyValueSequence(propertyModels.ToArray(), propertyValues.ToArray());
		//	}
		//}

		//public PropertyIndexValues GetChangedSaveablePropertyValues()
		//{
		//	return this.GetChangedSaveablePropertyValues(normalizer: (propertyModel, propertyValue) => propertyValue);
		//}

		public IEnumerable<PropertyIndexValuePair> GetNonDefaultOldPropertyIndexValuePairs(Predicate<IPropertyModel>? propertySelector = null)
		{
			return this.GetNonDefaultPropertyIndexValuePairsInternal(propertyIndex => this.GetOldFieldValue(propertyIndex), propertySelector);
		}

		public IEnumerable<PropertyIndexValuePair> GetNonDefaultPropertyIndexValuePairs(Predicate<IPropertyModel>? propertySelector = null)
		{
			return this.GetNonDefaultPropertyIndexValuePairsInternal(propertyIndex => this.GetFieldValue(propertyIndex), propertySelector);
		}

		internal IEnumerable<PropertyIndexValuePair> GetNonDefaultPropertyIndexValuePairsInternal(Func<int, object?> getFieldValue, Predicate<IPropertyModel>? propertySelector = null) //, Func<IPropertyModel, object, object> normalizer)
		{
			List<PropertyIndexValuePair> result = new List<PropertyIndexValuePair>(this.GetModel().PropertyModels.Count);

			//lock (this.lockObject)
			//{
				foreach (IPropertyModel propertyModel in this.GetModel().PropertyModels)
				{
					if (propertySelector != null && !propertySelector(propertyModel))
						continue;

					object? propertyValue = getFieldValue(propertyModel.PropertyIndex); 
					bool isDefault = Comparison.Equals(propertyValue, propertyModel.DefaultValue);

					if (!isDefault)
					{
						if (propertyModel.IsEncrypted)
							propertyValue = PasswordSecurity.Encrypt(propertyValue?.ToString(), this.Manager.Encryptor);

						result.Add(new PropertyIndexValuePair(propertyModel.PropertyIndex, propertyValue));
					}
				//}
			}

			//string test = "Ovo je test string. X";
			//string enkryptedText = PasswordSecurity.EncryptPassword(test,		   this.Manager.Encryptor, this.Manager.CryptoBlockSize);
			//string decryptedText = PasswordSecurity.DecryptPassword(enkryptedText, this.Manager.Decryptor, this.Manager.CryptoBlockSize);

			return result;
		}


		// TODO: Add normalizer instead of directly encripting

		//internal List<PropertyIndexValuePair> GetChangedSaveableOldPropertyIndexValues(Predicate<IPropertyModel> propertySelector)
		//{
		//	List<PropertyIndexValuePair> result;
		//	//Dictionary<int, object> result = new Dictionary<int, object>();

		//	lock (this.lockObject)
		//	{
		//		result = new List<PropertyIndexValuePair>(this.changedSaveablePropertyIndexes.Count);

		//		for (int i = 0; i < this.changedSaveablePropertyIndexes.Count; i++)
		//		{
		//			int propertyIndex = this.changedSaveablePropertyIndexes.ElementAt(i);
		//			IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

		//			if (propertySelector(propertyModel))
		//			{
		//				object? propertyValue = this.GetOldPropertyValue(propertyIndex);

		//				if (propertyValue != null && propertyModel.IsEncrypted)
		//					propertyValue = PasswordSecurity.Encrypt(propertyValue.ToString(), this.Manager.Encryptor);


		//				result.Add(new PropertyIndexValuePair(propertyIndex, propertyValue));
		//			}
		//		}
				
		//		//this.OnGetChangedSaveableOldPropertyValuesByPropertyIndex(propertySelector, ref result);
		//	}

		//	return result;
		//}

		internal List<PropertyIndexValuePair> GetChangedPropertyIndexValues() => this.GetChangedPropertyIndexValues(propertySelector: propertyModel => true); //  propertyModel => propertyModel.PropertyIndex != SimpleObject.IndexPropertyId);

		internal List<PropertyIndexValuePair> GetChangedPropertyIndexValues(Predicate<IPropertyModel> propertySelector) => this.GetChangedPropertyIndexValues(propertySelector, getPropertyValue: index => this.GetPropertyValue(index));


		internal List<PropertyIndexValuePair> GetChangedOldPropertyIndexValues() => this.GetChangedOldPropertyIndexValues(propertySelector: propertyModel => true); //  propertyModel => propertyModel.PropertyIndex != SimpleObject.IndexPropertyId);

		internal List<PropertyIndexValuePair> GetChangedOldPropertyIndexValues(Predicate<IPropertyModel> propertySelector) => this.GetChangedPropertyIndexValues(propertySelector, getPropertyValue: index => this.GetOldPropertyValue(index));

		
		internal List<PropertyIndexValuePair>  GetChangedPropertyIndexValues(Predicate<IPropertyModel> propertySelector, Func<int, object?> getPropertyValue)
		{
			List<PropertyIndexValuePair> result;
			int[] changedPropertyIndexes = this.GetChangedPropertyIndexes();
			//Dictionary<int, object> result = new Dictionary<int, object>();

			//lock (this.lockObject)
			//{
				result = new List<PropertyIndexValuePair>(changedPropertyIndexes.Length);

				for (int i = 0; i < changedPropertyIndexes.Length; i++)
				{
					int propertyIndex = changedPropertyIndexes.ElementAt(i);
					IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

					if (propertySelector(propertyModel))
					{
						object? propertyValue = getPropertyValue(propertyIndex);

						if (propertyValue != null && propertyModel.IsEncrypted)
							propertyValue = PasswordSecurity.Encrypt(propertyValue.ToString(), this.Manager.Encryptor);


						result.Add(new PropertyIndexValuePair(propertyIndex, propertyValue));
					}
				}

				//this.OnGetChangedSaveableOldPropertyValuesByPropertyIndex(propertySelector, ref result);
			//}

			return result;
		}



		//protected virtual void OnGetChangedSaveableOldPropertyValuesByPropertyIndex(Predicate<IPropertyModel>? propertySelector, ref List<PropertyIndexValuePair> propertyIndexValues) { }

		public List<PropertyIndexValuePair> GetNonDefaultOldPropertyIndexValues(Predicate<IPropertyModel>? propertySelector = null)
		{
			return this.GetNonDefaultPropertyIndexValuesInternal(propertyIndex => this.GetOldFieldValue(propertyIndex), propertySelector);
		}

		public List<PropertyIndexValuePair> GetNonDefaultPropertyIndexValues(Predicate<IPropertyModel>? propertySelector = null)
		{
			return this.GetNonDefaultPropertyIndexValuesInternal(propertyIndex => this.GetFieldValue(propertyIndex), propertySelector);
		}

		internal List<PropertyIndexValuePair> GetNonDefaultPropertyIndexValuesInternal(Func<int, object?> getFieldValue, Predicate<IPropertyModel>? propertySelector = null) //, Func<IPropertyModel, object, object> normalizer)
		{
			List<PropertyIndexValuePair> result;

			//lock (this.lockObject)
			//{
				result = new List<PropertyIndexValuePair>(this.GetModel().PropertyModels.Count);

				foreach (IPropertyModel propertyModel in this.GetModel().PropertyModels)
				{
					if (propertySelector != null && !propertySelector(propertyModel))
						continue;
					
					object? propertyValue = getFieldValue(propertyModel.PropertyIndex); // this.GetOldPropertyValue(propertyModel.PropertyIndex);
					//propertyValue = normalizer(propertyModel, propertyValue);

					bool isDefault = object.Equals(propertyValue, propertyModel.DefaultValue); // !Comparison.IsEqual(propertyValue, propertyModel.DefaultValue);

					if (!isDefault)
					{
						// TODO: Create ObjectManager.EncryptProperty and DecryptProperty
							
						if (propertyModel.IsEncrypted)
							propertyValue = PasswordSecurity.Encrypt(propertyValue?.ToString(), this.Manager.Encryptor);

						result.Add(new PropertyIndexValuePair(propertyModel.PropertyIndex, propertyValue));
					}
				}

				//this.OnGetDefaultChangedOldPropertyValuesByPropertyIndex(propertySelector, ref result);
			//}

			return result;
		}

		//protected virtual void OnGetDefaultChangedOldPropertyValuesByPropertyIndex(Predicate<IPropertyModel>? propertySelector, ref List<PropertyIndexValuePair> propertyIndexValues) { }

		//public PropertyIndexValues GetChangedSaveablePropertyIndexValues()
		//{
		//	return this.GetChangedSaveablePropertyIndexValues(normalizer: (propertyModel, propertyValue) => propertyValue);
		//}

		//internal PropertyIndexValues GetChangedSaveablePropertyIndexValues(Func<IPropertyModel, object?, object?> normalizer)
		//{
		//	lock (this.lockObject)
		//	{
		//		IEnumerable<int> propertyIndexes = this.GetChangedSaveablePropertyIndexes();
		//		object?[] propertyValues = this.GetPropertyValues(propertyIndexes, normalizer);

		//		return new PropertyIndexValues(propertyIndexes, propertyValues);
		//	}
		//}

		//internal PropertyIndexValuePair[] GetChangedSaveablePropertyIndexValuePairs(Func<IPropertyModel, object?, object?> normalizer)
		//{
		//	lock (this.lockObject)
		//	{
		//		var propertyIndexes = this.changedSaveablePropertyIndexes;
		//		PropertyIndexValuePair[] result = this.GetPropertyIndexValuePairs(propertyIndexes, normalizer);

		//		return result;
		//	}
		//}

		internal List<PropertyIndexValuePair> GetChangedClientSeriazablePropertyIndexValuePairs() => this.GetChangedPropertyIndexValues(propertySelector: propertyModel => propertyModel.IsClientToServerSeriazable, // propertyModel.PropertyIndex != SimpleObject.IndexPropertyId &&
																																		getPropertyValue: propertyIndex => this.GetPropertyValue(propertyIndex));
		//{
		//	lock (this.lockObject)
		//	{
		//		List<PropertyIndexValuePair> result = new List<PropertyIndexValuePair>(this.changedPropertyIndexes.Count);
		//		var propertyModels = this.GetModel().PropertyModels;

		//		foreach (int propertyIndex in this.changedPropertyIndexes)
		//		{
		//			if (propertyIndex == SimpleObject.IndexPropertyId)
		//				continue;

		//			var propertyModel = propertyModels[propertyIndex];

		//			if (propertyModel.IsClientSeriazable)
		//			{
		//				object? propertyValue = this.GetFieldValue(propertyIndex);

		//				if (propertyModel.IsEncrypted)
		//					propertyValue = PasswordSecurity.Encrypt(propertyValue?.ToString(), this.Manager.Encryptor);


		//				result.Add(new PropertyIndexValuePair(propertyIndex, propertyValue));
		//			}
		//		}

		//		return result;
		//	}
		//}

		internal List<PropertyIndexValuePair> GetChangedServerSeriazablePropertyIndexValuePairs() => this.GetChangedPropertyIndexValues(propertySelector: propertyModel => propertyModel.PropertyIndex != SimpleObject.IndexPropertyId && propertyModel.IsServerToClientSeriazable);

		//{
		//	lock (this.lockObject)
		//	{
		//		List<PropertyIndexValuePair> result = new List<PropertyIndexValuePair>(this.changedPropertyIndexes.Count);
		//		var propertyModels = this.GetModel().PropertyModels;

		//		foreach (int propertyIndex in this.changedPropertyIndexes)
		//		{
		//			if (propertyIndex == SimpleObject.IndexPropertyId)
		//				continue;

		//			var propertyModel = propertyModels[propertyIndex];

		//			if (propertyModel.IsServerSeriazable)
		//			{
		//				object? propertyValue = this.GetFieldValue(propertyIndex);

		//				if (propertyModel.IsEncrypted)
		//					propertyValue = PasswordSecurity.Encrypt(propertyValue?.ToString(), this.Manager.Encryptor);


		//				result.Add(new PropertyIndexValuePair(propertyIndex, propertyValue));
		//			}
		//		}

		//		return result;
		//	}
		//}

		//public PropertyIndexValues GetStorablePropertyIndexValues(Func<IPropertyModel, object, object> normalizer)
		//{
		//	IEnumerable<int> propertyIndexes = this.GetModel().StorablePropertySequence.PropertyIndexes;
		//	object[] propertyValues = this.GetPropertyValues(propertyIndexes, propertyIndex => this.GetFieldValue(propertyIndex), normalizer);

		//	return new PropertyIndexValues(propertyIndexes, propertyValues);
		//}

		//internal PropertyIndexValues GetChangedSaveablePropertyIndexValues(Func<IPropertyModel, object?, object?> normalizer)
		//{
		//	lock (this.lockObject)
		//	{
		//		IEnumerable<int> propertyIndexes = this.GetChangedSaveablePropertyIndexes();
		//		object?[] propertyValues = this.GetPropertyValues(propertyIndexes, normalizer);

		//		return new PropertyIndexValues(propertyIndexes, propertyValues);
		//	}
		//}

		internal List<PropertyIndexValuePair> GetChangedStorablePropertyIndexValuePairs(Func<IPropertyModel, object?, object?> normalizer)
		{ 
			return this.GetPropertyIndexValuePairs(this.changedPropertyIndexes.ToArray(), propertySelector: propertyModel => propertyModel.IsStorable, normalizer);
		}


		internal PropertyIndexValues GetStorablePropertyIndexValues(Func<IPropertyModel, object?, object?> normalizer)
		{
			//lock (this.lockObject)
			//{
				int[] propertyIndexes = this.GetModel().StorablePropertyIndexes;
				var propertyValues = this.GetPropertyValues(propertyIndexes, normalizer);

				return new PropertyIndexValues(propertyIndexes, propertyValues);
			//}
		}
		internal List<PropertyIndexValuePair> GetStorablePropertyIndexValuePairs(Func<IPropertyModel, object?, object?> normalizer)
		{
			return this.GetStorablePropertyIndexValuePairs(propertySelector: propertyModel => true, normalizer); // Id is included (!propertyModel.IsId - i f you want to not include Id
		}

		internal List<PropertyIndexValuePair> GetStorablePropertyIndexValuePairs(Predicate<IPropertyModel> propertySelector, Func<IPropertyModel, object?, object?> normalizer)
		{
			//lock (this.lockObject)
			//{
			var propertyIndexes = this.GetModel().StorablePropertyIndexes;

			return this.GetPropertyIndexValuePairs(propertyIndexes, propertySelector, normalizer); // Id is not included
		}



		//private PropertyIndexFieldNameValues GetPropertyIndexFieldNameValues(IEnumerable<int> propertyIndexes, Func<int, object> getFieldValue, Func<IPropertyModel, object, object> normalizer)
		//{
		//	object[] propertyValues = new object[propertyIndexes.Count()];
		//	string[] fieldNames = new string[propertyIndexes.Count()];
		//	_ = this.Id; // Enforce Id creation, if needed

		//	for (int i = 0; i < propertyIndexes.Count(); i++)
		//	{
		//		int propertyIndex = propertyIndexes.ElementAt(i);
		//		IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];
		//		fieldNames[i] = propertyModel.Name;
		//		object propertyValue = getFieldValue(propertyIndex); // (propertyIndex == SimpleObject.IndexPropertyId) ? this.Id : getFieldValue(propertyIndex); // Enforce Id creation, if needed

		//		propertyValue = normalizer(propertyModel, propertyValue);
		//		propertyValues[i] = propertyValue;
		//	}

		//	return new PropertyIndexFieldNameValues(propertyIndexes, fieldNames, propertyValues);
		//}

		//private PropertyIndexValues GetPropertyIndexValues(IEnumerable<int> propertyIndexes, Func<int, object> getFieldValue, Func<IPropertyModel, object, object> normalizer)
		//{
		//	object[] propertyValues = this.GetPropertyValues(propertyIndexes, getFieldValue, normalizer);

		//	return new PropertyIndexValues(propertyIndexes, propertyValues);
		//}


		//private PropertyIndexValues GetPropertyIndexValues(int[] propertyIndexes, Func<int, object> getFieldValue, Func<IPropertyModel, object, object> normalizer)
		//{
		//	lock (this.lockObject)
		//	{
		//		object[] propertyValues = new object[propertyIndexes.Length];

		//		for (int i = 0; i < propertyIndexes.Length; i++)
		//		{
		//			int propertyIndex = propertyIndexes[i];
		//			IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];
		//			object propertyValue = (propertyIndex == SimpleObject.IndexPropertyId) ? this.Id : getFieldValue(propertyIndex); // Enforce Id creation, if needed

		//			propertyValue = normalizer(propertyModel, propertyValue);
		//			propertyValues[i] = propertyValue;
		//		}

		//		return new PropertyIndexValues(propertyIndexes, propertyValues);
		//	}


		//public object[] GetPropertyValues(IEnumerable<int> propertyIndexes)
		//{
		//	object[] propertyValues = new object[propertyIndexes.Count()];
		//	//_ = this.Id; // Enforce Id creation, if needed

		//	lock (this.lockObject)
		//	{
		//		for (int i = 0; i < propertyIndexes.Count(); i++)
		//		{
		//			int propertyIndex = propertyIndexes.ElementAt(i);

		//			propertyValues[i] = this.GetFieldValue(propertyIndex);
		//		}
		//	}

		//	return propertyValues;
		//}



		//}
		//public object?[] GetPropertyValues(IEnumerable<int> propertyIndexes, Func<IPropertyModel, object?, object?> normalizer)
		//{
		//	return this.GetPropertyValues(propertyIndexes, normalizer);
		//}

		//public object?[] GetOldPropertyValues(IEnumerable<int> propertyIndexes)
		//{
		//	object?[] oldPropertyValues = new object?[propertyIndexes.Count()];
		//	_ = this.Id; // Enforce Id creation, if needed

		//	lock (this.lockObject)
		//	{
		//		for (int i = 0; i < propertyIndexes.Count(); i++)
		//		{
		//			int propertyIndex = propertyIndexes.ElementAt(i);

		//			oldPropertyValues[i] = this.GetOldFieldValue(propertyIndex);
		//		}
		//	}

		//	return oldPropertyValues;
		//}
		//}

		public List<object?> GetPropertyValues(IEnumerable<int> propertyIndexes)
		{
			return this.GetPropertyValues(propertyIndexes, normalizer: (propertyModel, propertyValue) => propertyValue);
		}

		public List<object?> GetPropertyValues(IEnumerable<int> propertyIndexes, Func<IPropertyModel, object?, object?> normalizer)
		{
			return this.GetPropertyValues(propertyIndexes, propertySelector: propertyModel => true, normalizer);
		}

		public List<object?> GetPropertyValues(IEnumerable<int> propertyIndexes, Predicate<IPropertyModel> propertySelector, Func<IPropertyModel, object?, object?> normalizer)
		{
			return this.GetPropertyValues(propertyIndexes, propertySelector, getFieldValue: propertyIndex => this.GetFieldValue(propertyIndex), normalizer);
		}


		public List<object?> GetOldPropertyValues(IEnumerable<int> propertyIndexes)
		{
			return this.GetOldPropertyValues(propertyIndexes, normalizer: (propertyModel, propertyValue) => propertyValue);
		}

		public List<object?> GetOldPropertyValues(IEnumerable<int> propertyIndexes, Func<IPropertyModel, object?, object?> normalizer)
		{
			return this.GetOldPropertyValues(propertyIndexes, propertySelector: propertyModel => true, normalizer);
		}

		public List<object?> GetOldPropertyValues(IEnumerable<int> propertyIndexes, Predicate<IPropertyModel> propertySelector, Func<IPropertyModel, object?, object?> normalizer)
		{
			return this.GetPropertyValues(propertyIndexes, propertySelector, getFieldValue: propertyIndex => this.GetOldFieldValue(propertyIndex), normalizer);
		}

		private List<object?> GetPropertyValues(IEnumerable<int> propertyIndexes, Predicate<IPropertyModel> propertySelector, Func<int, object?> getFieldValue, Func<IPropertyModel, object?, object?> normalizer)
		{
			List<object?> propertyValues = new List<object?>(propertyIndexes.Count());
			var propertyModels = this.GetModel().PropertyModels;
			_ = this.Id; // Enforce Id creation, if needed

			for (int i = 0; i < propertyIndexes.Count(); i++)
			{
				int propertyIndex = propertyIndexes.ElementAt(i);
				IPropertyModel propertyModel = propertyModels[propertyIndex];

				if (propertySelector(propertyModel))
				{
					object? propertyValue = getFieldValue(propertyIndex); // (propertyIndex == SimpleObject.IndexPropertyId) ? this.Id : getFieldValue(propertyIndex); // Enforce Id creation, if needed

					propertyValue = normalizer(propertyModel, propertyValue);
					propertyValues.Add(propertyValue);
				}
			}

			return propertyValues;
		}

		internal List<PropertyIndexValuePair> GetPropertyIndexValuePairs(IEnumerable<int> propertyIndexes, Func<IPropertyModel, object?, object?> normalizer)
		{
			return this.GetPropertyIndexValuePairs(propertyIndexes, propertySelector: propertyModel => true, normalizer);
		}

		internal List<PropertyIndexValuePair> GetPropertyIndexValuePairs(IEnumerable<int> propertyIndexes, Predicate<IPropertyModel> propertySelector, Func<IPropertyModel, object?, object?> normalizer)
		{
			return this.GetPropertyIndexValuePairs(propertyIndexes, propertySelector, (propertyIndex) => this.GetPropertyValue(propertyIndex), normalizer);
		}

		internal List<PropertyIndexValuePair> GetPropertyIndexValuePairs(IEnumerable<int> propertyIndexes, Predicate<IPropertyModel> propertySelector, Func<int, object?> getFieldValue, Func<IPropertyModel, object?, object?> normalizer)
		{
			//lock (this.lockObject)
			//{
				List<PropertyIndexValuePair> result = new List<PropertyIndexValuePair>(propertyIndexes.Count());
				var propertyModels = this.GetModel().PropertyModels;
				_ = this.Id; // Enforce Id creation, if needed

				for (int i = 0; i < propertyIndexes.Count(); i++)
				{
					int propertyIndex = propertyIndexes.ElementAt(i);
					IPropertyModel propertyModel = propertyModels[propertyIndex];

					if (propertySelector(propertyModel))
					{
						object? propertyValue = getFieldValue(propertyIndex); // (propertyIndex == SimpleObject.IndexPropertyId) ? this.Id : getFieldValue(propertyIndex); // Enforce Id creation, if needed

						propertyValue = normalizer(propertyModel, propertyValue);
						result.Add(new PropertyIndexValuePair(propertyIndex, propertyValue));
					}
				}

				return result;
			//}
		}


		//public PropertyIndexValues GetNonDefaultStorablePropertyValueSequence(Func<IPropertyModel, object, object> normalizer)
		//{
		//	lock (this.lockObject)
		//	{
		//		IPropertyModel[] changedPropertyModels = this.GetChangedPropertyIndexes().ToModelSequence(this.GetModel().PropertyModels);
		//		List<int> propertyIndexes = new List<int>();
		//		List<object> propertyValues = new List<object>();

		//		for (int i = 0; i < changedPropertyModels.Length; i++)
		//		{
		//			IPropertyModel propertyModel = changedPropertyModels[i];

		//			if (!propertyModel.IsStorable)
		//				continue;

		//			object propertyValue = this.GetFieldValue(propertyModel.Index);
		//			propertyValue = normalizer(propertyModel, propertyValue);

		//			if (!Comparison.IsEqual(propertyValue, propertyModel.DefaultValue))
		//			{
		//				propertyIndexes.Add(propertyModel.Index);
		//				propertyValues.Add(propertyValue);
		//			}
		//		}

		//		return new PropertyIndexValues(propertyIndexes, propertyValues);
		//	}
		//}

		///// <summary>
		///// Gets the property values object array ordered by storable seriazable property indexes and specified by object model.
		///// Each returned property value is calculated by <param name="normalizer"> method given. If <param name="normalizer"> is null, returned result is unchanged property value.
		///// </summary>
		///// <param name="normalizer">The method to calculate return value.</param>
		///// <returns>The object property value array.</returns>
		//public PropertyValueSequence GetStorableActionLogPropertyValueSequence(Func<IPropertyModel, object, object> normalizer)
		//{
		//	return this.GetPropertyValues(this.GetModel().TransactionActionPropertySequence, (propertyIndex) => this.GetFieldValue(propertyIndex), normalizer);
		//}

		//public PropertyValueSequence GetStorableActionLogOldPropertyValueSequence(Func<IPropertyModel, object, object> normalizer)
		//{
		//	return this.GetPropertyValues(this.GetModel().TransactionActionPropertySequence, (propertyIndex) => this.GetOldFieldValue(propertyIndex), normalizer);
		//}

		///// <summary>
		///// Gets the changed property values object array ordered by storable seriazable property indexes and specified by object model.
		///// Each returned property value is calculated by <param name="normalizer"> method given. If <param name="normalizer"> is null, returned result is unchanged property value.
		///// </summary>
		///// <param name="normalizer">The method to calculate return value.</param>
		///// <returns>The changed storable action property value array.</returns>
		//public PropertyValueSequence GetChangedStorableActionLogOldPropertyValueSequence(Func<IPropertyModel, object, object> normalizer)
		//{
		//	return this.GetPropertyValues(this.GetChangedStorableTransactionLogPropertyModels(), (propertyIndex) => this.GetOldFieldValue(propertyIndex), normalizer);
		//}

		/////// <summary>
		/////// Gets the property values object array ordered by storable property indexes specified by object model.
		/////// </summary>
		/////// <returns>The object property value array.</returns>
		////public object[] GetStorableOldPropertyValues()
		////{
		////	return this.GetPropertyValues(this.GetModel().StorablePropertySequence.IndexSequence, (propertyIndex) => this.GetOldFieldValue(propertyIndex));
		////}

		///// <summary>
		///// Gets the property values object array ordered by storable property indexes and specified by object model.
		///// Each returned property value is calculated by <param name="normalizer"> method given. If <param name="normalizer"> is null, returned result is unchanged property value.
		///// </summary>
		///// <param name="normalizer">The method to calculate return value.</param>
		///// <returns>The object property value array.</returns>
		//public PropertyValueSequence GetStorableOldPropertyValueSequence(Func<IPropertyModel, object, object> normalizer)
		//{
		//	return this.GetPropertyValues(this.GetModel().StorablePropertySequence, (propertyIndex) => this.GetOldFieldValue(propertyIndex), normalizer);
		//}

		///// <summary>
		///// Gets the property values dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with property index as a key.
		///// </summary>
		///// <returns>The dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with field values by index.</returns>
		//public IDictionary<int, object> GetPropertyValuesByIndex()
		//      {
		//          return this.GetPropertyValuesByIndex(normalizer: (propertyModel, value) => value);
		//      }

		//      /// <summary>
		//      /// Gets the property values dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with property index as a key.
		//      /// Each returned property value is calculated by <param name="normalizer"> method given. If <param name="normalizer"> is null returned result is unchanged property value.
		//      /// </summary>
		//      /// <param name="normalizer">The method to calculate returned item value.</param>
		//      /// <returns>The dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with property values by index.</returns>
		//      public IDictionary<int, object> GetPropertyValuesByIndex(Func<IPropertyModel, object, object> normalizer)
		//{
		//	lock (this.lockObject)
		//	{
		//		Dictionary<int, object> result = new Dictionary<int, object>();

		//		foreach (IPropertyModel propertyModel in this.GetModel().PropertyModels)
		//		{
		//			if (!propertyModel.IsStorable)
		//				continue;

		//			object value = this.GetFieldValue(propertyModel.Index);
		//                  value = normalizer(propertyModel, value);

		//                  if (value != propertyModel.DatastoreType.GetDefaultValue())
		//				result.Add(propertyModel.Index, value);
		//		}

		//		return result;
		//	}
		//}

		///// <summary>
		///// Gets the property values dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with property name as a key.
		///// </summary>
		///// <returns>The dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with field values by proeprty name.</returns>
		//protected internal IDictionary<string, object> GetPropertyValuesByName()
		//      {
		//          return this.GetPropertyValuesByName(null);
		//      }

		//      /// <summary>
		//      /// Gets the property values dictionary <see cref="IDictionary&lt;int, object&gt;"></see> with property name as a key.
		//      /// Each returned property value is calculated by <param name="normalizer"> method given. If <param name="normalizer"> is null returned result is unchanged property value.
		//      /// </summary>
		//      /// <param name="normalizer">The method to calculate returned item value.</param>
		//      /// <returns></returns>
		//      protected internal IDictionary<string, object> GetPropertyValuesByName(Func<IPropertyModel, object, object> normalizer)
		//{
		//	lock (this.lockObject)
		//	{
		//		Dictionary<string, object> result = new Dictionary<string, object>();

		//		foreach (IPropertyModel propertyModel in this.GetModel().PropertyModels)
		//		{
		//			if (!propertyModel.IsStorable)
		//				continue;

		//			object value = this.GetFieldValue(propertyModel.Index);

		//                  if (normalizer != null)
		//                      value = normalizer(propertyModel, value);

		//                  if (value != propertyModel.DatastoreType.GetDefaultValue())
		//				result.Add(propertyModel.Name, value);
		//		}

		//		return result;
		//	}
		//}

		//      public IDictionary<int, object> GetOldPropertyValuesByIndex()
		//      {
		//          return this.GetOldPropertyValuesByIndex(null);
		//      }

		//      public IDictionary<int, object> GetOldPropertyValuesByIndex(Func<IPropertyModel, object, object> normalizer)
		//{
		//	lock (this.lockObject)
		//	{
		//		Dictionary<int, object> result = new Dictionary<int, object>();

		//		foreach (IPropertyModel propertyModel in this.GetModel().PropertyModels)
		//		{
		//			if (!propertyModel.IsStorable)
		//				continue;

		//			object value = this.GetOldFieldValue(propertyModel.Index);

		//                  if (normalizer != null)
		//                      value = normalizer(propertyModel, value);

		//                  if (value != propertyModel.DatastoreType.GetDefaultValue())
		//				result.Add(propertyModel.Index, value);
		//		}

		//		return result;
		//	}
		//}

		public GraphElement GetOrCreateGraphElement(int graphKey, SimpleObject simpleObject, GraphElement? parent, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			GraphElement? graphElement = simpleObject.GetGraphElement(graphKey);

			if (graphElement == null)
			{
				graphElement = new GraphElement(this.Manager, graphKey, simpleObject, parent, changeContainer, context, requester);
				//graphElement.ChangeContainer = changeContainer;
				//graphElement.Requester = requester;
				//graphElement.GraphKey = graphKey;
				//graphElement.SimpleObject = simpleObject;
				//graphElement.Parent = parent;
				//this.SetRelatedTransactionRequest(graphElement, TransactionRequestAction.Save);
			}

			//if (!this.IsNew)
			//	graphElement.Save();

			return graphElement;
		}

		//public ChangeContainer GetChangeContainer() => this.ChangeContainer ?? this.Manager.DefaultChangeContainer;

		#endregion |   Public Methods   |

		#region |   Internal Methods   |

		internal protected SimpleObjectInternalState InternalState
		{
			get { return this.internalState; }
			set { this.internalState = value; }
		}

		internal void SetIsNew(bool value)
		{
			this.isNew = value;
		}

		//internal string GetImageNameInternal()
		//{
		//	return this.imageName;
		//}

		//internal void SetImageNameInternal(string imageName, string oldImageName, bool fireOnImageNameChange)
		//{
		//	this.imageName = imageName;

		//	if (fireOnImageNameChange)
		//		this.OnImageNameChange(imageName, oldImageName);
		//}

		//internal void ActionBeforeLoading(object requester)
		//{
		//	this.OnBeforeLoading(requester);
		//}

		//internal void ActionAfterLoad(object requester)
		//{
		//	this.OnAfterLoad(requester);
		//}

		internal void BeforeSave(ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			this.OnBeforeSave(changeContainer, context, requester);
		}

		//internal void Saving(object requester)
		//{
		//	this.OnSaving(requester);
		//}

		internal void AfterLoad()
		{
			this.OnAfterLoad();
		}

		internal void AfterSave(bool isNewBeforeSaving, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			this.OnAfterSave(isNewBeforeSaving, changeContainer, context, requester);
		}

		internal void DeleteIsRequested(ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			//if (!this.IsNew)
			//	this.RejectChanges();
			
			this.deleteRequested = true;
			this.OnRequestDelete(changeContainer, context, requester);
		}

		//internal void ClientDeleteIsRequested(ChangeContainer? changeContainer, object? requester)
		//{
		//	this.OnClientRequestDelete(changeContainer, requester);
		//}

		internal void GraphElementDeleteIsRequested(GraphElement graphElement, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			this.OnGraphElementDeleteRequest(graphElement, changeContainer, context, requester);
		}

		internal void DeleteRequestIsCancelled(ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			this.deleteRequested = false;

			if (!this.IsNew)
				this.RejectChanges();

			this.OnDeleteRequestCancel(changeContainer, context, requester);
		}

		internal void BeforeDelete(ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			this.OnBeforeDelete(changeContainer, context, requester);
		}

		private bool isNewClientObjectIsCreatedRaised = false;

		internal void NewObjectIsCreated(ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			//if (!(this is GraphElement))
			//	this.AddToNullCollections(this);

#if DEBUG
			if (this.isNewClientObjectIsCreatedRaised)
				throw new Exception("Second call of NewClientIsCreated. Fix this");
#endif

			this.InternalState = SimpleObjectInternalState.Normal; // Track changes from now
			this.OnNewObjectCreated(changeContainer, context, requester);
			this.isNewClientObjectIsCreatedRaised = true;
		}

		internal void NewGraphElementIsCreated(GraphElement graphElement, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			this.OnNewGraphElementCreated(graphElement, changeContainer, context, requester);
		}

		internal void GraphElementParentIsChanged(GraphElement graphElement, GraphElement? oldParent, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			this.OnGraphElementParentChange(graphElement, oldParent, changeContainer, context, requester);
		}

		internal void GraphElementOrderIndexIsChanged(GraphElement graphElement, int orderIndex, int oldOrderIndex, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			this.OnGraphElementOrderIndexChange(graphElement, orderIndex, oldOrderIndex, changeContainer, context, requester);
		}

		//internal void AfterGraphElementIsSaved(GraphElement graphElement, object requester)
		//{
		//	this.OnAfterGraphElementSave(graphElement, requester);
		//}

		internal void BeforeGraphElementIsDeleted(GraphElement graphElement, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			this.OnBeforeGraphElementDelete(graphElement, changeContainer, context, requester);
		}

		//internal virtual void RelationForeignObjectIsSet(SimpleObject foreignSimpleObject, SimpleObject oldForeignSimpleObject, IOneToOneOrManyRelationModel objectRelationModel, ChangeContainer changeContainer, object? requester)
		//{
			//this.OnRelationForeignObjectSet(foreignSimpleObject, oldForeignSimpleObject, objectRelationModel, changeContainer, requester);
		//}

		internal void ObjectIdIsChanged(long oldTempId, long newId, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			// TODO: Propagate Id change for cached one to one foreign object caches, one to many foreign collections any group membership
			// Find all object references in one-to-one relation, one-to-many collection any group membership collections
			// Similar to RemoveAllRelatedObjectsFromAllRelatedObjectCaches we need ChangeAllRelatedObjectTempKeysFromAllRelatedObjectCaches

			//lock (this.lockObject)
			//{
			//	// one-to-one foreign object cache
			//	foreach (IOneToOneRelationModel oneToOneRelationModel in this.GetModel().RelationModel.AsForeignObjectInOneToOneRelations) //    ObjectRelationModel.OneToOneRelationKeyHolderObjectDictionary)
			//	{
			//		int primaryTableId = (oneToOneRelationModel.PrimaryTableIdPropertyModel != null) ? this.GetPropertyValue<int>(oneToOneRelationModel.PrimaryTableIdPropertyModel.PropertyIndex) : oneToOneRelationModel.PrimaryObjectTableId;
			//		long primaryObjectId = this.GetPropertyValue<long>(oneToOneRelationModel.PrimaryObjectIdPropertyModel);

			//		if (this.Manager.IsObjectInCache(primaryTableId, primaryObjectId, out SimpleObject? primaryObject))
			//			primaryObject?.ChangeOneToOneForeignObjectTempIdIfInCache(this.GetModel().TableInfo.TableId, oldTempId, newId);
			//	}

				// All others related objects are in SimpleObjectCollection and its container change temp Id by catching ObjectManager_ObjectIdChange event in SimpleObjectCollectionContaier class
			//}

			this.OnObjectIdChange(oldTempId, newId, changeContainer, context, requester);
		}

		internal SimpleObjectCollection? GetOneToManyForeignNullCollectionInternal(int relationKey) => this.GetOneToManyForeignNullCollection(relationKey);


		#endregion |   Internal Methods   |

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

		//protected internal object GetOldPropertyValue(int propertyIndex)
		//{
		//	return this.GetOldFieldValue(propertyIndex);
		//}

		protected internal void SetPropertyValueInternal(IPropertyModel propertyModel, object? propertyValue, ChangeContainer? changeContainer, object? requester)
		{
			this.SetPropertyValueInternal(propertyModel, propertyValue, changeContainer, this.Context, requester);
		}

		protected internal void SetPropertyValueInternal(IPropertyModel propertyModel, object? propertyValue, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			this.SetPropertyValueInternal(propertyModel, propertyValue, propertyModel.TrimStringBeforeComparison, changeContainer, context, requester);
		}

		protected internal void SetPropertyValueInternal(IPropertyModel propertyModel, object? propertyValue, bool trimStringBeforeComparison, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			this.SetPropertyValuePrivate(propertyModel, propertyValue, trimStringBeforeComparison, changeContainer, context, requester);
		}

		//TODO: REMOVE THIS

		//protected internal void SetPropertyValueInternal(IPropertyModel propertyModel, object value, bool addOrRemoveInChangedProperties, bool firePropertyValueChangeEvent, ChangeContainer changeContainer, object requester)
		//{
		//	this.SetPropertyValueInternal(propertyModel, value, addOrRemoveInChangedProperties, firePropertyValueChangeEvent, enforceAccessModifier: false, changeContainer, requester);
		//}

		/// <summary>
		/// Sets the field and old field value to requered silentrly. No any events fired. 
		/// propertyIndex is removed from ChangedPropertyIndexes and ChangedSaveablePropertyIndexes, if in it-
		/// </summary>
		/// <param name="propertyIndex">The property index</param>
		/// <param name="value">The property value to set</param>
		protected internal void AcceptPropertyChangeInternal(int propertyIndex, object value)
		{
			lock (this.lockObject)
			{
				this.SetFieldValue(propertyIndex, value);
				this.SetOldFieldValue(propertyIndex, value);
				this.changedPropertyIndexes.Remove(propertyIndex);
				//this.changedSaveablePropertyIndexes.Remove(propertyIndex);
			}
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


		//      protected internal IDictionary<string, object> GetOldPropertyValuesByName()
		//      {
		//          return this.GetOldPropertyValuesByName(null);
		//      }

		//      protected internal IDictionary<string, object> GetOldPropertyValuesByName(Func<IPropertyModel, object, object> normalizer)
		//{
		//	lock (this.lockObject)
		//	{
		//		Dictionary<string, object> result = new Dictionary<string, object>();

		//		foreach (IPropertyModel propertyModel in this.GetModel().PropertyModels)
		//		{
		//			if (!propertyModel.IsStorable)
		//				continue;

		//			object value = this.GetOldFieldValue(propertyModel.Index);

		//                  if (normalizer != null)
		//                      value = normalizer(propertyModel, value);

		//                  if (value != propertyModel.DatastoreType.GetDefaultValue())
		//				result.Add(propertyModel.Name, value);
		//		}

		//		return result;
		//	}
		//}

		//protected internal object[] GetChangedPropertyValues()
		//{
		//	return this.GetChangedPropertyValues(null);
		//}

		//protected internal object[] GetChangedPropertyValues(Func<IPropertyModel, object, object> normalizer)
		//{
		//	lock (this.lockObject)
		//	{

		//		object[] result = new object[this.changedPropertyIndexes.Count];

		//		for (int i = 0; i < result.Length; i++)
		//		{
		//			int propertyIndex = this.changedPropertyIndexes.ElementAt(i);
		//			IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

		//			object value = this.GetFieldValue(propertyIndex);

		//			if (normalizer != null)
		//				value = normalizer(propertyModel, value);

		//			result[i] = value;
		//		}

		//		return result;
		//	}
		//}

		//protected internal PropertyValueSequence GetChangedPropertyValues()
		//{
		//	return this.GetChangedPropertyValues(normalizer: (propertyModel, value) => value);
		//}

		//protected internal PropertyValueSequence GetChangedPropertyValueSequence(Func<IPropertyModel, object, object> normalizer)
		//{
		//	return this.GetPropertyValues(this.GetChangedPropertyIndexes().ToModelSequence(this.GetModel().PropertyModels), (propertyIndex) => this.GetFieldValue(propertyIndex), normalizer);
		//}

		//protected internal PropertyValueSequence GetChangedSaveablePropertyValueSequence(Func<IPropertyModel, object, object> normalizer)
		//{
		//	return this.GetPropertyValues(this.GetChangedSaveablePropertyIndexes().ToModelSequence(this.GetModel().PropertyModels), (propertyIndex) => this.GetFieldValue(propertyIndex), normalizer);
		//}

		////protected internal PropertyValueSequence GetChangedOldPropertyValues()
		////{
		////	return this.GetChangedOldPropertyValues(normalizer: (propertyModel, value) => value);
		////}

		//protected internal PropertyValueSequence GetChangedOldPropertyValueSequence(Func<IPropertyModel, object, object> normalizer)
		//{
		//	return this.GetPropertyValues(this.GetChangedPropertyIndexes().ToModelSequence(this.GetModel().PropertyModels), (propertyIndex) => this.GetOldFieldValue(propertyIndex), normalizer);
		//}

		//protected internal IDictionary<int, object> GetChangedPropertyValuesByIndex()
		//      {
		//          return this.GetChangedPropertyValuesByIndex(normalizer: (propertyModel, value) => value);
		//      }

		//      protected internal IDictionary<int, object> GetChangedPropertyValuesByIndex(Func<IPropertyModel, object, object> normalizer)
		//{
		//	lock (this.lockObject)
		//	{
		//		Dictionary<int, object> result = new Dictionary<int, object>();

		//		foreach (int propertyIndex in this.changedPropertyModels)
		//		{
		//			IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

		//			if (!propertyModel.IsStorable)
		//				continue;

		//			object value = this.GetFieldValue(propertyIndex);
		//			value = normalizer(propertyModel, value);

		//                  result.Add(propertyModel.Index, value);
		//		}

		//		return result;
		//	}
		//}

		//      protected internal IDictionary<string, object> GetChangedPropertyValuesByName()
		//      {
		//          return this.GetChangedPropertyValuesByName(normalizer: (propertyModel, value) => value);
		//      }

		//      protected internal IDictionary<string, object> GetChangedPropertyValuesByName(Func<IPropertyModel, object, object> normalizer)
		//{
		//	lock (this.lockObject)
		//	{
		//		Dictionary<string, object> result = new Dictionary<string, object>();

		//		foreach (int propertyIndex in this.changedPropertyModels)
		//		{
		//			IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

		//			if (!propertyModel.IsStorable)
		//				continue;

		//			object value = this.GetFieldValue(propertyModel.Index);
		//                  value = normalizer(propertyModel, value);

		//                  result.Add(propertyModel.Name, value);
		//		}

		//		return result;
		//	}
		//}

		//      protected internal IDictionary<int, object> GetChangedOldPropertyValuesByIndex()
		//      {
		//          return this.GetChangedOldPropertyValuesByIndex(normalizer: (propertyModel, value) => value);
		//      }

		//      protected internal IDictionary<int, object> GetChangedOldPropertyValuesByIndex(Func<IPropertyModel, object, object> normalizer)
		//{
		//	lock (this.lockObject)
		//	{
		//		Dictionary<int, object> result = new Dictionary<int, object>();

		//		foreach (int propertyIndex in this.changedPropertyModels)
		//		{
		//			IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

		//			if (!propertyModel.IsStorable)
		//				continue;

		//			object value = this.GetOldFieldValue(propertyModel.Index);
		//                  value = normalizer(propertyModel, value);

		//                  result.Add(propertyModel.Index, value);
		//		}

		//		return result;
		//	}
		//}

		//      protected internal IDictionary<string, object> GetChangedOldPropertyValuesByName()
		//      {
		//          return this.GetChangedOldPropertyValuesByName(normalizer: (propertyModel, value) => value);
		//      }

		//      protected internal IDictionary<string, object> GetChangedOldPropertyValuesByName(Func<IPropertyModel, object, object> normalizer)
		//{
		//	lock (this.lockObject)
		//	{
		//		Dictionary<string, object> result = new Dictionary<string, object>();

		//		foreach (int propertyIndex in this.changedPropertyModels)
		//		{
		//			IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

		//			if (!propertyModel.IsStorable)
		//				continue;

		//			object value = this.GetOldFieldValue(propertyIndex);
		//                  value = normalizer(propertyModel, value);

		//                  //fieldValue = this.CreateNormalizedPropertyDataToSaveInDatastore(propertyModel, fieldValue);

		//                  result.Add(propertyModel.Name, value);
		//		}

		//		return result;
		//	}
		//}

		#endregion |   Internal Protected Methods   |

		#region |   Protected Abstract Methods   |

		protected abstract object? GetFieldValue(int propertyIndex);
		protected abstract object? GetOldFieldValue(int propertyIndex);
		protected abstract void SetFieldValue(int propertyIndex, object value);
		protected abstract void SetOldFieldValue(int propertyIndex, object value);

		#endregion |   Protected Abstract Methods   |

		#region |   Property Loads   |

		public void LoadFrom(IDataReader dataReader, IPropertyModel[] propertyModelsByDataReaderFieldIndex, Func<IPropertyModel, object?, object?> readNormalizer, bool loadOldValuesAlso)
		{
			lock (this.lockObject)
			{
				for (int fieldIndex = 0; fieldIndex < propertyModelsByDataReaderFieldIndex.Length; fieldIndex++)
				{
					IPropertyModel propertyModel = propertyModelsByDataReaderFieldIndex[fieldIndex];

					if (propertyModel == null)
						continue;

					object value = dataReader.GetValue(fieldIndex);

					this.LoadPropertyValue(propertyModel, value, readNormalizer, loadOldValuesAlso);
				}
			}
		}

		///// <summary>
		///// Load property values from the server serialization stream. It is intended to be used from the client.
		///// If SerializationModel.SequenceValuesOnly and if defaultValueOptimization is first we read boolean to see if value is default.
		///// If SerializationModel.IndexValuePairs only property Index/Pairs are read. Others has its defaultValues
		///// </summary>
		///// <param name="reader">The <see cref="SequenceReader"></see> to read from.</param>
		///// <param name="propertyIndexSequence">The property indexes array that specify order of the property values.</param>
		///// <param name="readNormalizer">The property value reading normalizer.</param>
		///// <param name="loadOldValuesAlso">If true, old field values are loaded also, otherwise not.</param>
		///// <param name="skipReadingKey">If true, key is not read, otherwise it is.</param>
		//public void LoadFrom(SequenceReader reader, Func<IPropertyModel, object?, object?> readNormalizer,
		//											SerializationModel serializationModel, bool skipReadingKey, bool loadOldValuesAlso, bool defaultValueOptimization)
		//{
		//	lock (this.lockObject)
		//	{
		//		int propertyCount;
		//		bool checkIfPropertyValueIsDefault = false; // if the SerializationModel.SequenceValuesOnly sequence reader reads only reads values

		//		if (serializationModel == SerializationModel.IndexValuePairs)
		//		{
		//			propertyCount = reader.ReadInt32Optimized();
		//			checkIfPropertyValueIsDefault = false; // prevent reading single property value default optimized in index/value pairs serialization mode.
		//		}
		//		else
		//		{
		//			propertyCount = this.GetModel().SerializablePropertyIndexes.Length;
		//			checkIfPropertyValueIsDefault = defaultValueOptimization;
		//		}

		//		for (int i = 0; i < propertyCount; i++)
		//		{
		//			int propertyIndex = (serializationModel == SerializationModel.IndexValuePairs) ?
		//										   reader.ReadInt32Optimized() :
		//										   this.GetModel().SerializablePropertyIndexes[i];
		//			IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

		//			if (skipReadingKey && propertyModel.PropertyIndex == SimpleObject.IndexPropertyId && serializationModel == SerializationModel.SequenceValuesOnly)
		//				continue;

		//			object? propertyValue = SimpleObjectManager.ReadObjectPropertyValue(ref reader, propertyModel, checkIfPropertyValueIsDefault);

		//			this.LoadPropertyValue(propertyModel, propertyValue, readNormalizer, loadOldValuesAlso);
		//		}
		//	}
		//}

		//public void SetFrom(ref SequenceReader reader, Func<IPropertyModel, object?, object?> readNormalizer, SerializationModel serializationModel, bool skipReadingKey, bool defaultValueOptimization, object requester)
		//{
		//	lock (this.lockObject)
		//	{
		//		int propertyCount;
		//		bool checkIfPropertyValueIsDefault = false; // if the SerializationModel.SequenceValuesOnly sequence reader reads only reads values

		//		if (serializationModel == SerializationModel.IndexValuePairs)
		//		{
		//			propertyCount = reader.ReadInt32Optimized();
		//			checkIfPropertyValueIsDefault = false; // prevent reading single property value optimized in index/value pairs serialization mode.
		//		}
		//		else
		//		{
		//			propertyCount = this.GetModel().SerializablePropertyIndexes.Length;
		//			checkIfPropertyValueIsDefault = defaultValueOptimization;
		//		}

		//		for (int i = 0; i < propertyCount; i++)
		//		{
		//			int propertyIndex = (serializationModel == SerializationModel.IndexValuePairs) ?
		//										   reader.ReadInt32Optimized() :
		//										   this.GetModel().SerializablePropertyIndexes[i];
		//			IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

		//			if (skipReadingKey && propertyModel.PropertyIndex == SimpleObject.IndexPropertyId && serializationModel == SerializationModel.SequenceValuesOnly)
		//				continue;

		//			object? propertyValue = SimpleObjectManager.ReadObjectPropertyValue(ref reader, propertyModel, checkIfPropertyValueIsDefault);
		//			propertyValue = readNormalizer(propertyModel, propertyValue);

		//			//this.SetPropertyValueInternal(propertyModel, propertyValue, propertyModel.AddOrRemoveInChangedProperties, propertyModel.FirePropertyValueChangeEvent,
		//			//							  decideActionFromModel: true, enforceAccessModifier: false, requester);


		//			if (propertyModel.IsRelationObjectId) // && propertyModel.Index != SimpleObject.IndexPropertyGuid) // SimpleObject.Key (propertyModel.Index == SimpleObject.IndexPropertyKey) maybe noo need for check since key will 
		//			{
		//				// -> TODO: Find relation type and manualy set relation

		//				// Temporary
		//				this.SetPropertyValue(propertyModel, propertyValue, changeContainer: null, context, requester);

		//				//this.GetModel().RelationModel.


		//				//this.SetOneToManyRelationForeignObject

		//			}
		//			else
		//			{
		//				this.SetPropertyValue(propertyModel, propertyValue, requester);
		//			}

		//			//if (propertyModel.Index == SimpleObject.IndexPropertyOrderIndex) // Left OrderIndex to be set at last
		//			//{
		//			//	orderIndexPropertyModel = propertyModel;
		//			//	orderIndex = (int)propertyValue;
		//			//}
		//			//else
		//			//{
		//			//	this.SetFromPropertyValue(propertyModel, propertyValue, requester);
		//			//}
		//		}

		//		//if (orderIndexPropertyModel != null) // Set OrderIndex at last
		//		//	this.SetPropertyValue(orderIndexPropertyModel, orderIndex, requester);
		//	}

		//	this.AfterLoad();
		//}

		/// <summary>
		/// Load property values from the server serialization stream. It is intended to be used from the client.
		/// If SerializationModel.SequenceValuesOnly and if defaultValueOptimization is first we read boolean to see if value is default.
		/// If SerializationModel.IndexValuePairs only property Index/Pairs are read. Others has its defaultValues
		/// </summary>
		/// <param name="reader">The <see cref="SequenceReader"></see> to read from.</param>
		/// <param name="propertyIndexSequence">The property indexes array that specify order of the property values.</param>
		/// <param name="readNormalizer">The property value reading normalizer.</param>
		/// <param name="loadOldValuesAlso">If true, old field values are loaded also, otherwise not.</param>
		/// <param name="skipReadingKey">If true, key is not read, otherwise it is.</param>
		public void LoadFrom(ref SequenceReader reader, ServerObjectModelInfo serverObjectPropertyInfo, Func<IPropertyModel, object?, object?> readNormalizer,
													    SerializationModel serializationModel, bool skipReadingKey, bool loadOldValuesAlso, bool defaultValueOptimization)
		{
			lock (this.lockObject)
			{
				int propertyCount;
				bool checkIfPropertyValueIsDefault = false; // if the SerializationModel.SequenceValuesOnly sequence reader reads only reads values

				if (serializationModel == SerializationModel.IndexValuePairs)
				{
					propertyCount = reader.ReadInt32Optimized();
					checkIfPropertyValueIsDefault = false; // prevent reading single property value optimized in index/value pairs serialization mode.
				}
				else
				{
					propertyCount = serverObjectPropertyInfo.ClientSerializablePropertyIndexes.Length; // This should be the same value with this.GetModel().SerializablePropertySequence.Length;
					checkIfPropertyValueIsDefault = defaultValueOptimization;
				}

				for (int i = 0; i < propertyCount; i++)
				{
					int propertyIndex = (serializationModel == SerializationModel.IndexValuePairs) ? reader.ReadInt32Optimized() : serverObjectPropertyInfo.ClientSerializablePropertyIndexes[i];
					ServerPropertyInfo serverPropertyModelInfo = serverObjectPropertyInfo.GetPropertyInfo(propertyIndex);

					if (skipReadingKey && propertyIndex == SimpleObject.IndexPropertyId && serializationModel == SerializationModel.SequenceValuesOnly)
						continue;

					IPropertyModel clientPropertyModel = this.GetModel().PropertyModels.GetPropertyModel(propertyIndex);
					object? propertyValue = null;

					if (clientPropertyModel == null)
					{
						propertyValue = (serverPropertyModelInfo.IsSerializationOptimizable) ? reader.ReadOptimized(serverPropertyModelInfo.PropertyTypeId) : reader.Read(serverPropertyModelInfo.PropertyTypeId);
						continue; // Client does not have this property - client and server property models are different
					}

					propertyValue = SimpleObjectManager.ReadObjectPropertyValue(ref reader, clientPropertyModel, serverPropertyModelInfo.PropertyTypeId, checkIfPropertyValueIsDefault);
					propertyValue = readNormalizer(clientPropertyModel, propertyValue);

					// if client property type is different from server property type -> change type
					if (serverPropertyModelInfo.PropertyTypeId != clientPropertyModel.PropertyTypeId)
						propertyValue = Conversion.TryChangeType(propertyValue, clientPropertyModel.PropertyType);

					this.SetFieldValue(propertyIndex, propertyValue!);

					if (loadOldValuesAlso)
						this.SetOldFieldValue(propertyIndex, propertyValue!);
				}
			}

			this.AfterLoad();
		}


		protected internal void Load(IPropertyModel[] propertyModelSequence, object[] propertyValues, Func<IPropertyModel, object?, object?> readNormalizer, bool loadOldValuesAlso)
		{
			lock (this.lockObject)
			{
				for (int i = 0; i < propertyModelSequence.Length; i++)
					this.LoadPropertyValue(propertyModelSequence[i], propertyValues[i], readNormalizer, loadOldValuesAlso);
			}

			this.AfterLoad();
		}

		protected internal void Load(int[] propertyIndexSequence, object[] propertyValues, Func<IPropertyModel, object?, object?> readNormalizer, bool loadOldValuesAlso)
		{
			lock (this.lockObject)
			{
				for (int i = 0; i < propertyIndexSequence.Length; i++)
					this.LoadPropertyValue(propertyIndexSequence[i], propertyValues[i], readNormalizer, loadOldValuesAlso);

				this.OnAfterLoad();
			}

			this.AfterLoad();
		}

		//protected internal void Load(KeyValuePair<IPropertyModel, object>[] propertyModelValueProperties, Func<IPropertyModel, object, object> readNormalizer, bool loadOldValuesAlso)
		//{
		//	lock (this.lockObject)
		//	{
		//		foreach (var item in propertyModelValueProperties)
		//			this.LoadPropertyValue(item.Key, item.Value, readNormalizer, loadOldValuesAlso);

		//		this.OnAfterLoad();
		//	}
		//}

		//protected internal void Load(PropertyValueSequence propertyValueSequence, Func<IPropertyModel, object, object> readNormalizer, bool loadOldValuesAlso, bool reverseOrder = false)
		//{
		//	lock (this.lockObject)
		//	{
		//		for (int i = 0; i < propertyValueSequence.Length; i++)
		//		{
		//			int position = (reverseOrder) ? propertyValueSequence.Length - 1 - i : i;
		//			this.LoadPropertyValue(propertyValueSequence.PropertyModels[position], propertyValueSequence.PropertyValues[position], readNormalizer, loadOldValuesAlso);
		//		}

		//		this.OnAfterLoad();
		//	}
		//}

		protected internal void Load(IDictionary<int, object> propertyDataByIndex, Func<IPropertyModel, object?, object?> readNormalizer, bool loadOldValuesAlso)
		{
			lock (this.lockObject)
			{
				foreach (var item in propertyDataByIndex)
					this.LoadPropertyValue(item.Key, item.Value, readNormalizer, loadOldValuesAlso);
			}

			this.AfterLoad();
		}

		protected internal void Load(IDictionary<string, object> propertyDataByName, Func<IPropertyModel, object?, object?> readNormalizer, bool loadOldValuesAlso)
		{
			lock (this.lockObject)
			{
				foreach (var item in propertyDataByName)
					this.LoadPropertyValue(item.Key, item.Value, readNormalizer, loadOldValuesAlso);
			}

			this.AfterLoad();
		}

		/// <summary>
		/// Load property values from the server serialization stream. It is intended to be used from the client.
		/// If SerializationModel.SequenceValuesOnly and if defaultValueOptimization is first we read boolean to see if value is default.
		/// If SerializationModel.IndexValuePairs only property Index/Pairs are read. Others has its defaultValues
		/// </summary>
		/// <param name="reader">The <see cref="SequenceReader"></see> to read from.</param>
		/// <param name="propertyIndexSequence">The property indexes array that specify order of the property values.</param>
		/// <param name="readNormalizer">The property value reading normalizer.</param>
		/// <param name="loadOldValuesAlso">If true, old field values are loaded also, otherwise not.</param>
		/// <param name="skipReadingKey">If true, key is not read, otherwise it is.</param>
		public void LoadFrom(ref SequenceReader reader, ServerObjectModelInfo serverObjectPropertyInfo, bool setOldValuesAlso, Func<IServerPropertyInfo, object?, object?>? readNormalizer = null)
		{
			lock (this.lockObject)
			{
				int propertyCount = reader.ReadInt32Optimized();

				for (int i = 0; i < propertyCount; i++)
				{
					int propertyIndex = reader.ReadInt32Optimized();
					ServerPropertyInfo serverPropertyModelInfo = serverObjectPropertyInfo[propertyIndex];
					object? propertyValue = SimpleObjectManager.ReadObjectPropertyValue(ref reader, serverPropertyModelInfo);
					IPropertyModel? localPropertyModel = this.GetModel().PropertyModels.GetPropertyModel(propertyIndex);

					if (localPropertyModel == null)
						continue; // local property does not exists => it is not implemented (different client and server version/model => skip loading propery value

					if (readNormalizer != null)
					{
						propertyValue = readNormalizer(serverPropertyModelInfo, propertyValue); // normalizer will also change value type if different from server property def 
					}
					else if (serverPropertyModelInfo.PropertyTypeId != localPropertyModel.PropertyTypeId) // if client property type is different from server property type -> change type
					{
						propertyValue = Conversion.TryChangeType(propertyValue, localPropertyModel.PropertyType);
					}

					this.SetFieldValue(propertyIndex, propertyValue!);

					if (setOldValuesAlso)
					{
						this.SetOldFieldValue(propertyIndex, propertyValue!);
						this.changedPropertyIndexes.Remove(propertyIndex);
						//this.changedSaveablePropertyIndexes.Remove(propertyIndex);
					}
				}
			}

			this.AfterLoad();
		}

		/// <summary>
		/// Load property values from the server serialization stream. It is intended to be used from the client.
		/// If SerializationModel.SequenceValuesOnly and if defaultValueOptimization is first we read boolean to see if value is default.
		/// If SerializationModel.IndexValuePairs only property Index/Pairs are read. Others has its defaultValues
		/// </summary>
		/// <param name="reader">The <see cref="SequenceReader"></see> to read from.</param>
		/// <param name="propertyIndexSequence">The property indexes array that specify order of the property values.</param>
		/// <param name="readNormalizer">The property value reading normalizer.</param>
		/// <param name="loadOldValuesAlso">If true, old field values are loaded also, otherwise not.</param>
		/// <param name="skipReadingKey">If true, key is not read, otherwise it is.</param>
		public void LoadFrom(ref SequenceReader reader, ServerObjectModelInfo serverObjectPropertyInfo)
		{
			lock (this.lockObject)
			{
				int propertyCount = reader.ReadInt32Optimized();

				for (int i = 0; i < propertyCount; i++)
				{
					int propertyIndex = reader.ReadInt32Optimized();
					ServerPropertyInfo serverPropertyModel = serverObjectPropertyInfo[propertyIndex];
					object? propertyValue = SimpleObjectManager.ReadObjectPropertyValue(ref reader, serverPropertyModel);
					IPropertyModel localPropertyModel = this.GetModel().PropertyModels.GetPropertyModel(propertyIndex);

					if (localPropertyModel == null)
						continue; // local property does not exists => property is not implemented (different client and server version/model) => skip loading propery value

					if (serverPropertyModel.IsEncrypted)
					{
						propertyValue = PasswordSecurity.Decrypt((string)propertyValue!, this.Manager.Decryptor);

						if (localPropertyModel.PropertyTypeId != (int)PropertyTypeId.String)
							propertyValue = Conversion.TryChangeType(propertyValue, localPropertyModel.PropertyType);
					}
					else
					{
						if (serverPropertyModel.PropertyTypeId != localPropertyModel.PropertyTypeId)
							propertyValue = Conversion.TryChangeType(propertyValue, localPropertyModel.PropertyType);
					}

					this.SetFieldValue(propertyIndex, propertyValue!);
					this.SetOldFieldValue(propertyIndex, propertyValue!);
					this.changedPropertyIndexes.Remove(propertyIndex);
					//this.changedSaveablePropertyIndexes.Remove(propertyIndex);
				}
			}

			this.AfterLoad();
		}


		public void LoadFromServer(IEnumerable<PropertyIndexValuePair> propertyIndexValuePairs)
		{
			lock (this.lockObject)
			{
				ServerObjectModelInfo serverObjectPropertyInfo = this.Manager.GetServerObjectModel(this.GetModel().TableInfo.TableId)!;

				foreach (PropertyIndexValuePair item in propertyIndexValuePairs)
				{
					IPropertyModel localPropertyModel = this.GetModel().GetPropertyModel(item.PropertyIndex);

					if (localPropertyModel != null)
					{
						IServerPropertyInfo serverPropertyModel = serverObjectPropertyInfo[item.PropertyIndex];
						object? propertyValue = item.PropertyValue;

						if (serverPropertyModel.IsEncrypted)
						{
							propertyValue = PasswordSecurity.Decrypt((string)propertyValue!, this.Manager.Decryptor);

							if (localPropertyModel.PropertyTypeId != (int)PropertyTypeId.String)
								propertyValue = Conversion.TryChangeType(propertyValue, localPropertyModel.PropertyType);
						}
						else
						{
							if (serverPropertyModel.PropertyTypeId != localPropertyModel.PropertyTypeId)
								propertyValue = Conversion.TryChangeType(propertyValue, localPropertyModel.PropertyType);
						}

						this.SetFieldValue(item.PropertyIndex, propertyValue!);
						this.SetOldFieldValue(item.PropertyIndex, propertyValue!);
						this.changedPropertyIndexes.Remove(item.PropertyIndex);
						//this.changedSaveablePropertyIndexes.Remove(item.PropertyIndex);
					}
				}
			}

			this.AfterLoad();
		}

		public void LoadFromServer(int[] propertyIndexes, object[] propertyValues, ServerObjectModelInfo serverObjectPropertyInfo)
		{
			lock (this.lockObject)
			{
				for (int i = 0; i < propertyIndexes.Length; i++)
				{
					int propertyIndex = propertyIndexes[i];
					IPropertyModel localPropertyModel = this.GetModel().GetPropertyModel(propertyIndex);

					if (localPropertyModel != null)
					{
						IServerPropertyInfo serverPropertyModel = serverObjectPropertyInfo[propertyIndex];
						object? propertyValue = propertyValues[i];

						if (serverPropertyModel.IsEncrypted)
						{
							propertyValue = PasswordSecurity.Decrypt((string)propertyValue!, this.Manager.Decryptor);

							if (localPropertyModel.PropertyTypeId != (int)PropertyTypeId.String)
								propertyValue = Conversion.TryChangeType(propertyValue, localPropertyModel.PropertyType);
						}
						else
						{
							if (serverPropertyModel.PropertyTypeId != localPropertyModel.PropertyTypeId)
								propertyValue = Conversion.TryChangeType(propertyValue, localPropertyModel.PropertyType);
						}

						this.SetFieldValue(propertyIndex, propertyValue!);
						this.SetOldFieldValue(propertyIndex, propertyValue!);
						this.changedPropertyIndexes.Remove(propertyIndex);
						//this.changedSaveablePropertyIndexes.Remove(propertyIndex);
					}
				}
			}

			this.AfterLoad();
		}


		internal void SetRelationPropertyValuesOnlyAndRemoveFromCachesInternal(IEnumerable<PropertyIndexValuePair> propertyIndexValues, IDictionary<long, long> newObjectIdsByTempClientObjectId, out string infoMessage, ChangeContainer? changeContainer, ObjectActionContext context, object? requester = null)
		{
			int primaryTableId = 0, oldPrimaryTableId = 0;

			infoMessage = string.Empty;

			lock (this.lockObject)
			{
				for (int i = 0; i < propertyIndexValues.Count(); i++)
				{
					var item = propertyIndexValues.ElementAt(i);
					IPropertyModel? propertyModel = this.GetModel().PropertyModels[item.PropertyIndex];

					if (propertyModel != null)
					{
						if (propertyModel.IsRelationTableId)
						{
							primaryTableId = (int)item.PropertyValue!;
							oldPrimaryTableId = (int)this.GetPropertyValue(propertyModel.PropertyIndex)!;
							this.SetPropertyValueInternal(propertyModel, item.PropertyValue, changeContainer, context, requester);
						}
						else if (propertyModel.IsRelationObjectId)
						{
							if ((long)item.PropertyValue! < 0)
								item.PropertyValue = newObjectIdsByTempClientObjectId[(long)item.PropertyValue];

							long primaryObjectId = (long)item.PropertyValue!;
							long oldPrimaryObjectId = (long)this.GetPropertyValue(propertyModel.PropertyIndex)!;
							IRelationModel? relationModel = this.Manager.GetRelationModel(propertyModel.RelationKey);

							if (relationModel is IOneToOneOrManyRelationModel oneToOneOrManyRelationModel)
							{
								if (primaryTableId == 0)
								{
									primaryTableId = oneToOneOrManyRelationModel.PrimaryObjectTableId; // If foreign TableId is not specified it is fixed and is determined by the relation model definition
									oldPrimaryTableId = primaryTableId;
								}

								this.RemoveRelationPrimaryObjectFromCache(oldPrimaryTableId, oldPrimaryObjectId, oneToOneOrManyRelationModel);

								if (oldPrimaryObjectId == 0)
									this.GetOneToManyForeignNullCollection(relationModel.RelationKey)?.Remove(this, changeContainer, requester);

								if (primaryObjectId == 0)
									this.GetOneToManyForeignNullCollection(relationModel.RelationKey)?.Add(this, changeContainer, requester);
							}
							//else
							//{
							// for exampele, GroupMembershipElement has no specific RelationKey for Object2Id/Object2Id, only value need to be set

							//}

							//if (foreignObjectId < 0)
							this.SetPropertyValueInternal(propertyModel, item.PropertyValue, changeContainer, context, requester);

							primaryTableId = 0;
							oldPrimaryTableId = 0;
						}
						//else if (propertyModel.IsOrderIndex)
						//{
						//	postponedPropertiesToSet.Add(new SimpleObjectPropertyIndexValuePair(this, item));

						//	continue;
						//}


						//else
						//{
						//	//propertyValue = this.NormalizeWhenReadingByPropertyType(propertyModel, propertyValue);
						//	object? propertyValue = item.PropertyValue;

						//	if (propertyModel.IsEncrypted && propertyValue != null)
						//		propertyValue = this.Manager.DecryptProperty(propertyValue);

						//	this.SetPropertyValueInternal(propertyModel, propertyValue, changeContainer, requester);
						//}
					}
					else
					{
						infoMessage = String.Format("There is no property model defined, TableId:{0}, PropertyIndex:{2}, PropertyName:{3}", this.GetModel().TableInfo.TableId, item.PropertyIndex); //, propertyModel.PropertyName);

						Debug.WriteLine("ProcessTransactionRequestArgs: " + infoMessage);
					}
				}
			}

			//this.AfterLoad();
		}

		internal void SetAllPropertyValuesInternal(IEnumerable<PropertyIndexValuePair> propertyIndexValues, IDictionary<long, long> newObjectIdsByTempClientObjectId, ChangeContainer? changeContainer, ObjectActionContext context, object? requester = null)
		{
			lock (this.lockObject)
			{
				for (int i = 0; i < propertyIndexValues.Count(); i++)
				{
					var item = propertyIndexValues.ElementAt(i);
					IPropertyModel? propertyModel = this.GetModel().PropertyModels[item.PropertyIndex];

					if (propertyModel != null)
					{
						if (propertyModel.IsRelationObjectId && (long)item.PropertyValue! < 0)
							item.PropertyValue = newObjectIdsByTempClientObjectId[(long)item.PropertyValue];

						////propertyValue = this.NormalizeWhenReadingByPropertyType(propertyModel, propertyValue);
						object? propertyValue = item.PropertyValue;

						if (propertyModel.IsEncrypted && propertyValue != null)
							propertyValue = this.Manager.DecryptProperty(propertyValue);

						this.SetPropertyValueInternal(propertyModel, propertyValue, changeContainer, context, requester);
					}
					else
					{
						string infoMessage = String.Format("There is no property model defined, TableId:{0}, PropertyIndex:{2}, PropertyName:{3}", this.GetModel().TableInfo.TableId, item.PropertyIndex); //, propertyModel.PropertyName);
						Debug.WriteLine("ProcessTransactionRequestArgs: " + infoMessage);
					}
				}
			}
		}


		internal void SetRelationPrimaryObjectsInternal(RelationListInfo relationsToSet, ChangeContainer? changeContainer, ObjectActionContext context, object? requester = null)
		{
			for (int i = 0; i < relationsToSet.Count; i++)
			{
				var item = relationsToSet[i];

				this.SetRelationPrimaryObject(item.TableId, item.ObjectId, item.RelationKey, changeContainer, context, requester);
			}
		}

		/// <summary>
		/// Sets all relations in a relationsToSet acoording to data within and sets all other relations that exists in relation model with default values stored in object.
		/// </summary>
		/// <param name="relationsToSet">Relation infos to sets</param>
		/// <param name="changeContainer">The underlying ChangeContainer</param>
		/// <param name="context">The underlying context</param>
		/// <param name="requester">The requester of the event</param>
		internal void SetAllRelationPrimaryObjectsInternal(RelationListInfo relationsToSet, ChangeContainer? changeContainer, ObjectActionContext context, object? requester = null)
		{
			foreach (IOneToOneOrManyRelationModel relationModel in this.GetModel().RelationModel.AsForeignObjectInRelarions)
			{
				if (relationsToSet.TryGetRelationInfo(relationModel.RelationKey, out RelationKeyTableIdObjectIdPair? relationInfo))
				{
					this.SetRelationPrimaryObject(relationInfo!.TableId,  relationInfo.ObjectId, relationModel.RelationKey, changeContainer, context, requester); // 
				}
				else // Sets the relation with default values stored in object
				{
					int primaryTableId = (relationModel.PrimaryTableIdPropertyModel != null) ? this.GetPropertyValue<int>(relationModel.PrimaryTableIdPropertyModel.PropertyIndex) : relationModel.PrimaryObjectTableId;
					long primaryObjectId = this.GetPropertyValue<long>(relationModel.PrimaryObjectIdPropertyModel.PropertyIndex);

					this.SetRelationPrimaryObject(primaryTableId, primaryObjectId, relationModel.RelationKey, changeContainer, context, requester);
				}
			}
		}

		internal void SetNonRelationPropertyValuesAndCollectRelationsInternal(IEnumerable<PropertyIndexValuePair> propertyIndexValues, out RelationListInfo relationsToSet, ChangeContainer? changeContainer, ObjectActionContext context, object? requester = null)
		{
			this.SetNonRelationPropertyValuesAndCollectRelationsInternal(propertyIndexValues, EmptyNewObjectIdsByTempClientObjectIdDictionary, out relationsToSet, changeContainer, context, requester);
		}

		internal void SetNonRelationPropertyValuesAndCollectRelationsInternal(IEnumerable<PropertyIndexValuePair> propertyIndexValues, IDictionary<long, long> newObjectIdsByTempClientObjectId, out RelationListInfo relationsToSet, ChangeContainer? changeContainer, ObjectActionContext context, object? requester = null)
		{
			relationsToSet = new RelationListInfo();
			
			//lock (this.lockObject)
			//{
				for (int i = 0; i < propertyIndexValues.Count(); i++)
				{
					var item = propertyIndexValues.ElementAt(i);
					IPropertyModel? propertyModel = this.GetModel().PropertyModels[item.PropertyIndex];

					if (propertyModel != null)
					{
						if (propertyModel.IsRelationTableId)
						{
							relationsToSet.Set(propertyModel.RelationKey, tableId: (int)item.PropertyValue!);

							continue;
						}
						else if (propertyModel.IsRelationObjectId)
						{
							long objectId = (long)item.PropertyValue!;

							if (objectId < 0)
								objectId = newObjectIdsByTempClientObjectId[objectId];

							item.PropertyValue = objectId;
							relationsToSet.Set(propertyModel.RelationKey, objectId);

							continue;
						}

						//propertyValue = this.NormalizeWhenReadingByPropertyType(propertyModel, propertyValue);
						object? propertyValue = item.PropertyValue;

						if (propertyModel.IsEncrypted && propertyValue != null)
							propertyValue = this.Manager.DecryptProperty(propertyValue);

						this.SetPropertyValueInternal(propertyModel, propertyValue, changeContainer, context, requester);
					}
					else
					{
						string infoMessage = String.Format("There is no property model defined, TableId:{0}, PropertyIndex:{2}, PropertyName:{3}", this.GetModel().TableInfo.TableId, item.PropertyIndex); //, propertyModel.PropertyName);
						Debug.WriteLine("ProcessTransactionRequestArgs: " + infoMessage);
					}
				}

				for (int i = 0; i < relationsToSet.Count; i++)
				{
					var item = relationsToSet[i];

					if (item.TableId == 0)
					{
						var relationModel = this.GetModel().RelationModel.AsForeignObjectInRelarions.GetRelationModel(item.RelationKey);

						if (relationModel != null)
							item.TableId = relationModel.PrimaryObjectTableId;
					}
					else if (item.ObjectId == 0)
					{
						var relationModel = this.GetModel().RelationModel.AsForeignObjectInRelarions.GetRelationModel(item.RelationKey);

						if (relationModel != null)
							item.ObjectId = this.GetPropertyValue<long>(relationModel.PrimaryObjectIdPropertyModel.PropertyIndex);
					}
				}
			//}
		}


		internal void SetAllPropertyValuesInternal(IEnumerable<PropertyIndexValuePair> propertyIndexValues, ChangeContainer? changeContainer, ObjectActionContext context, object? requester = null)
		{
			//lock (this.lockObject)
			//{
				for (int i = 0; i < propertyIndexValues.Count(); i++)
				{
					var item = propertyIndexValues.ElementAt(i);
					IPropertyModel? propertyModel = this.GetModel().PropertyModels[item.PropertyIndex];

					if (propertyModel != null)
					{
						object? propertyValue = item.PropertyValue;

						if (propertyModel.IsEncrypted && propertyValue != null)
							propertyValue = this.Manager.DecryptProperty(propertyValue);

						this.SetPropertyValueInternal(propertyModel, propertyValue, changeContainer, context, requester);
					}
					else
					{
						string infoMessage = String.Format("There is no property model defined, TableId:{0}, PropertyIndex:{2}, PropertyName:{3}", this.GetModel().TableInfo.TableId, item.PropertyIndex); //, propertyModel.PropertyName);
						Debug.WriteLine("ProcessTransactionRequestArgs: " + infoMessage);
					}
				}
			//}
		}


		// TODO: Fix bug if only TableId and ObjectId remain the same is changed for related object is changed and need to be set!!!!!
		//internal void SetRelationPrimaryObjectsInternal(IEnumerable<PropertyIndexValuePair> propertyIndexValues, IDictionary<long, long> newObjectIdsByTempClientObjectId, out string infoMessage, ChangeContainer? changeContainer, ObjectActionContext context, object? requester = null)
		//{
		//	int primaryTableId = 0;
		//	//IPropertyModel? primaryTableIdPropertyModel = null;

		//	infoMessage = string.Empty;

		//	lock (this.lockObject)
		//	{
		//		for (int i = 0; i < propertyIndexValues.Count(); i++)
		//		{
		//			var item = propertyIndexValues.ElementAt(i);
		//			IPropertyModel? propertyModel = this.GetModel().PropertyModels[item.PropertyIndex];

		//			if (propertyModel != null)
		//			{
		//				if (propertyModel.IsRelationTableId)
		//				{
		//					primaryTableId = (int)item.PropertyValue!;
		//					this.SetPropertyValueInternal(propertyModel, item.PropertyValue, changeContainer, context, requester); // For many to many relations
		//				}
		//				else if (propertyModel.IsRelationObjectId)
		//				{
		//					if ((long)item.PropertyValue! < 0)
		//						item.PropertyValue = newObjectIdsByTempClientObjectId[(long)item.PropertyValue];

		//					long primaryObjectId = (long)item.PropertyValue!;
		//					IRelationModel? relationModel = this.Manager.GetRelationModel(propertyModel.RelationKey);

		//					if (relationModel is IOneToOneOrManyRelationModel oneToOneOrManyRelationModel)
		//					{
		//						if (primaryTableId == 0)
		//							primaryTableId = oneToOneOrManyRelationModel.PrimaryObjectTableId; // If foreign TableId is not specified it is fixed and is determined by the relation model definition

		//						this.SetRelationPrimaryObject(primaryTableId, primaryObjectId, relationModel.RelationKey, changeContainer, context, requester);
		//					}
		//					else // IManyToManyRelation
		//					{
		//						// for exampele, GroupMembershipElement has no specific RelationKey for Object2Id/Object2Id, only value need to be set
		//						this.SetPropertyValueInternal(propertyModel, item.PropertyValue, changeContainer, context, requester);
		//					}

		//					primaryTableId = 0;
		//				}
		//				else
		//				{
		//					////propertyValue = this.NormalizeWhenReadingByPropertyType(propertyModel, propertyValue);
		//					//object? propertyValue = item.PropertyValue;

		//					//if (propertyModel.IsEncrypted && propertyValue != null)
		//					//	propertyValue = this.Manager.DecryptProperty(propertyValue);

		//					//this.SetPropertyValueInternal(propertyModel, propertyValue, changeContainer, context, requester);
		//				}
		//			}
		//			else
		//			{
		//				infoMessage = String.Format("There is no property model defined, TableId:{0}, PropertyIndex:{2}, PropertyName:{3}", this.GetModel().TableInfo.TableId, item.PropertyIndex); //, propertyModel.PropertyName);

		//				Debug.WriteLine("ProcessTransactionRequestArgs: " + infoMessage);
		//			}
		//		}
		//	}
		//}

		internal void SetAllRelationPrimaryObjectsInternal_OLD(IEnumerable<PropertyIndexValuePair> newPropertyIndexValues, ChangeContainer? changeContainer, ObjectActionContext context, object? requester = null)
		{
			foreach (IOneToOneOrManyRelationModel relationModel in this.GetModel().RelationModel.AsForeignObjectInRelarions)
			{
				if (!this.SetRelationPrimaryObjectInternal_OLD(relationModel, newPropertyIndexValues, changeContainer, context, requester))
				{
					int primaryTableId = (relationModel.PrimaryTableIdPropertyModel != null) ? this.GetPropertyValue<int>(relationModel.PrimaryTableIdPropertyModel.PropertyIndex) : relationModel.PrimaryObjectTableId;
					long primaryObjectId = this.GetPropertyValue<long>(relationModel.PrimaryObjectIdPropertyModel.PropertyIndex);

					this.SetRelationPrimaryObject(primaryTableId, primaryObjectId, relationModel.RelationKey, changeContainer, context, requester);
				}
			}
		}

		/// <summary>
		/// Sets the new relation property objects if ObjectzId or TableId exists in newPropertyIndexValues. 
		/// It is assumed that relation property values are already set. Only needs to set primary relation object
		/// </summary>
		/// <param name="propertyIndexValues"></param>
		/// <param name="changeContainer"></param>
		/// <param name="context"></param>
		/// <param name="requester"></param>
		internal void SetRelationPrimaryObjectsInternal_OLD(IEnumerable<PropertyIndexValuePair> propertyIndexValues, ChangeContainer? changeContainer, ObjectActionContext context, object? requester = null)
		{
			foreach (IOneToOneOrManyRelationModel relationModel in this.GetModel().RelationModel.AsForeignObjectInRelarions)
				this.SetRelationPrimaryObjectInternal_OLD(relationModel, propertyIndexValues, changeContainer, context, requester);
		}

		private bool SetRelationPrimaryObjectInternal_OLD(IOneToOneOrManyRelationModel relationModel, IEnumerable<PropertyIndexValuePair> propertyIndexValues, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			if (propertyIndexValues.TryGetPropertyValue(relationModel.PrimaryObjectIdPropertyModel.PropertyIndex, out object? primaryObjectId))
			{
				int primaryTableId = (relationModel.PrimaryTableIdPropertyModel != null) ? this.GetPropertyValue<int>(relationModel.PrimaryTableIdPropertyModel.PropertyIndex) : relationModel.PrimaryObjectTableId;

				this.SetRelationPrimaryObject(primaryTableId, (long)primaryObjectId!, relationModel.RelationKey, changeContainer, context, requester);

				return true;
			}
			else if (relationModel.PrimaryTableIdPropertyModel != null && propertyIndexValues.TryGetPropertyValue(relationModel.PrimaryTableIdPropertyModel.PropertyIndex, out object? primaryTableId))
			{
				primaryObjectId = this.GetPropertyValue<long>(relationModel.PrimaryObjectIdPropertyModel.PropertyIndex);

				this.SetRelationPrimaryObject((int)primaryTableId!, (long)primaryObjectId, relationModel.RelationKey, changeContainer, context, requester);

				return true;
			}

			return false;
		}

		internal void SetAllRelationPrimaryObjects(IEnumerable<PropertyIndexValuePair>? excludedPropertyIndexValues, ChangeContainer? changeContainer, ObjectActionContext context, object? requester = null)
		{
			foreach (IOneToOneOrManyRelationModel relationModel in this.GetModel().RelationModel.AsForeignObjectInRelarions.ToArray())
			{
				//bool isRelationAlreadySet = false;

				//if (excludedPropertyIndexValues != null)
				//{
				//	foreach (var i in excludedPropertyIndexValues)
				//	{
				//		if (i.PropertyIndex == relationModel.PrimaryObjectIdPropertyModel.PropertyIndex)
				//		{
				//			isRelationAlreadySet = true;

				//			break;
				//		}
				//	}
				//}
				
				bool isRelationAlreadySet = excludedPropertyIndexValues?.FindFirst(item => item.PropertyIndex == relationModel.PrimaryObjectIdPropertyModel.PropertyIndex) != null;
				//var excluded = excludedPropertyIndexValues.Where(item => item.PropertyIndex == relationModel.PrimaryObjectIdPropertyModel.PropertyIndex);

				if (!isRelationAlreadySet) // excluded is null
				{
					int primaryTableId = (relationModel.PrimaryTableIdPropertyModel != null) ? this.GetPropertyValue<int>(relationModel.PrimaryTableIdPropertyModel.PropertyIndex) : relationModel.PrimaryObjectTableId;
					long primaryObjectId = this.GetPropertyValue<long>(relationModel.PrimaryObjectIdPropertyModel.PropertyIndex);

					this.SetRelationPrimaryObject(primaryTableId, primaryObjectId, relationModel.RelationKey, changeContainer, context, requester);
				}
			}
		}

		internal void SetPropertyValuesWithoutRelationsInternal(IEnumerable<PropertyIndexValuePair> propertyIndexValues, ChangeContainer? changeContainer, ObjectActionContext context, object? requester = null)
		{
			this.SetPropertyValues2Internal(propertyIndexValues, modelCriteria: propertyModel => !(propertyModel.IsRelationTableId || propertyModel.IsRelationObjectId), changeContainer, context, requester);
		}

		internal void SetPropertyValues2Internal(IEnumerable<PropertyIndexValuePair> propertyIndexValues, Func<IPropertyModel, bool> modelCriteria, ChangeContainer? changeContainer, ObjectActionContext context, object? requester = null)
		{
			//lock (this.lockObject)
			//{
				for (int i = 0; i < propertyIndexValues.Count(); i++)
				{
					var item = propertyIndexValues.ElementAt(i);
					IPropertyModel? propertyModel = this.GetModel().PropertyModels[item.PropertyIndex];

					if (propertyModel != null)
					{
						if (!modelCriteria(propertyModel))
							continue;

						object? propertyValue = item.PropertyValue;

						if (propertyModel.IsEncrypted && propertyValue != null)
							propertyValue = this.Manager.DecryptProperty(propertyValue);

						this.SetPropertyValueInternal(propertyModel, item.PropertyValue, changeContainer, context, requester);
					}
				}
			//}
		}

		internal void LoadFieldValuesWithoutRelationsInternal(IEnumerable<PropertyIndexValuePair> propertyIndexValues, ref List<SimpleObjectPropertyRelationArgs> objectRelationPropertiesToSet, out string infoMessage, object? requester = null)
		{
			int foreignTableId = 0;

			infoMessage = string.Empty;

			lock (this.lockObject)
			{
				for (int i = 0; i < propertyIndexValues.Count(); i++)
				{
					var item = propertyIndexValues.ElementAt(i);
					IPropertyModel? propertyModel = this.GetModel().PropertyModels[item.PropertyIndex];

					if (propertyModel != null)
					{
						if (propertyModel.IsRelationTableId)
						{
							foreignTableId = (int)item.PropertyValue!;
							this.SetFieldValue(propertyModel.PropertyIndex, item.PropertyValue);
							this.SetOldFieldValue(propertyModel.PropertyIndex, item.PropertyValue);
						}
						else if (propertyModel.IsRelationObjectId)
						{
							long foreignObjectId = (long)item.PropertyValue!;
							IRelationModel? relationModel = this.Manager.GetRelationModel(propertyModel.RelationKey);

							if (relationModel is IOneToOneOrManyRelationModel oneToOneOrManyRelationModel)
							{
								if (foreignTableId == 0)
									foreignTableId = oneToOneOrManyRelationModel.PrimaryObjectTableId; // If foreign TableId is not specified it is fixed and is determined by the relation model definition

								objectRelationPropertiesToSet.Add(new SimpleObjectPropertyRelationArgs(this, foreignTableId, foreignObjectId, oneToOneOrManyRelationModel));
							}
							else
							{
								// for exampele, GroupMembershipElement has no specific RelationKey for Object2Id/Object2Id, only value need to be set
								this.SetFieldValue(propertyModel.PropertyIndex, item.PropertyValue);
								this.SetOldFieldValue(propertyModel.PropertyIndex, item.PropertyValue);
							}

							//if (foreignObjectId < 0)
							foreignTableId = 0;
						}
						//else if (propertyModel.IsOrderIndex)
						//{
						//	postonedPropertiesToSet.Add(item);
						//}
						else //if (propertyModel.PropertyIndex != SimpleObject.IndexPropertyId)
						{
							//propertyValue = this.NormalizeWhenReadingByPropertyType(propertyModel, propertyValue);
							object? propertyValue = item.PropertyValue;

							if (propertyModel.IsEncrypted && propertyValue != null)
								propertyValue = this.Manager.DecryptProperty(propertyValue);

							this.SetFieldValue(propertyModel.PropertyIndex, item.PropertyValue!);
							this.SetOldFieldValue(propertyModel.PropertyIndex, item.PropertyValue!);
						}
					}
					else
					{
						infoMessage = String.Format("There is no property model defined, TableId:{0}, PropertyIndex:{2}, PropertyName:{3}", this.GetModel().TableInfo.TableId, item.PropertyIndex); //, propertyModel.PropertyName);

						Debug.WriteLine("ProcessTransactionRequestArgs: " + infoMessage);
					}
				}
			}

			this.AfterLoad();
		}

		//public PropertyValueSequence GetPropertyValueSequence(IPropertySequence propertySequence)
		//{
		//	return this.GetPropertyValueSequence(propertySequence, normalizer: (propertyModel, propertyValue) => propertyValue);
		//}

		//public PropertyValueSequence GetPropertyValueSequence(IPropertySequence propertySequence, Func<IPropertyModel, object, object> normalizer)
		//{
		//	return this.GetPropertyValues(propertySequence, (propertyIndex) => this.GetFieldValue(propertyIndex), normalizer);
		//}

		//public PropertyValueSequence GetOldPropertyValueSequence(IPropertySequence propertySequence)
		//{
		//	return this.GetOldPropertyValueSequence(propertySequence, normalizer: (propertyModel, propertyValue) => propertyValue);
		//}

		//public PropertyValueSequence GetOldPropertyValueSequence(IPropertySequence propertySequence, Func<IPropertyModel, object, object> normalizer, SetPropertiesOption setPropertiesOption = SetPropertiesOption.SetAll)
		//{
		//	return this.GetPropertyValues(propertySequence, (propertyIndex) => this.GetOldFieldValue(propertyIndex), normalizer);
		//}

		//public void SetPropertyValues(IEnumerable<PropertyIndexValuePair> propertyIndexeValues, ChangeContainer changeContainer, object requester)
		//{
		//	this.SetPropertyValues(propertyIndexeValues, writeNormalizer: this.Manager.NormalizeForWritingByPropertyType, changeContainer, requester);
		//}

		public void SetPropertyValues(IEnumerable<PropertyIndexValuePair> propertyIndexeValues, ChangeContainer? changeContainer, ObjectActionContext context, object? requester = null)
		{
			this.SetPropertyValuesInternal(propertyIndexeValues, writeNormalizer: this.Manager.NormalizeForWritingByPropertyType, changeContainer, context, requester);
		}

		internal void SetPropertyValuesInternal(IEnumerable<PropertyIndexValuePair> propertyIndexeValues, ChangeContainer? changeContainer, ObjectActionContext context, object? requester = null)
		{
			this.SetPropertyValuesInternal(propertyIndexeValues, writeNormalizer: this.Manager.NormalizeForWritingByPropertyType, changeContainer, context, requester);
		}

		internal void SetPropertyValuesInternal(IEnumerable<PropertyIndexValuePair> propertyIndexeValues, Func<IPropertyModel, object?, object?> writeNormalizer, ChangeContainer? changeContainer, ObjectActionContext context, object? requester = null)
		{
			int primaryTableId = 0;

			//lock (this.lockObject)
			//{
				// Sets non relation properties, first
				for (int i = 0; i < propertyIndexeValues.Count(); i++)
				{
					var item = propertyIndexeValues.ElementAt(i);
					IPropertyModel propertyModel = this.GetModel().PropertyModels[item.PropertyIndex];

					//if (propertyModel != null)
					//{
					if (propertyModel.IsRelationTableId || propertyModel.IsRelationObjectId)
						continue;

					object? propertyValue = writeNormalizer(propertyModel, item.PropertyValue);

					//propertyValue = (propertyModel.IsEncrypted && propertyValue != null) ? this.Manager.DecryptProperty(propertyValue) : 
					//																	   writeNormalizer(propertyModel, propertyValue);

					this.SetPropertyValue(propertyModel, propertyValue, changeContainer, context, requester: this);

					//else
					//{
					//	string info = String.Format("Server has no property model, TableId:{0}, PropertyIndex:{2}, PropertyName:{3}", foreignTableId, propertyIndex, propertyModel.PropertyName);

					//	Debug.WriteLine("ProcessTransactionRequestArgs: " + info);
					//}
				}

				//this.AfterLoad();

				// Sets relation properties, second
				for (int i = 0; i < propertyIndexeValues.Count(); i++)
				{
					var item = propertyIndexeValues.ElementAt(i);
					IPropertyModel propertyModel = this.GetModel().PropertyModels[item.PropertyIndex];

					//if (setPropertiesOption == SetPropertiesOption.AvoidSetsRelations && (propertyModel.IsRelationTableId || propertyModel.IsRelationObjectId))
					//	continue;

					//if (setPropertiesOption == SetPropertiesOption.SetOnlyRelations && (!propertyModel.IsRelationTableId || !propertyModel.IsRelationObjectId))
					//	continue;

					if (propertyModel.IsRelationTableId)
					{
						primaryTableId = (int)item.PropertyValue!;
					}
					else if (propertyModel.IsRelationObjectId)
					{
						long primaryObjectId = (long)item.PropertyValue!;
						IRelationModel? relationModel = this.Manager.GetRelationModel(propertyModel.RelationKey);

						if (relationModel is IOneToOneOrManyRelationModel oneToOneOrManyRelationModel)
						{
							if (primaryTableId == 0)
								primaryTableId = oneToOneOrManyRelationModel.PrimaryObjectTableId; // If foreign TableId is not specified it is fixed and is determined by the relation model definition

							this.SetRelationPrimaryObjectInternal(primaryTableId, primaryObjectId, oneToOneOrManyRelationModel, changeContainer, context, requester);
						}

						primaryTableId = 0;
					}
				}
			//}
		}

		private void LoadPropertyValue(string propertyName, object? propertyValue, Func<IPropertyModel, object?, object?> readNormalizer, bool loadOldValueAlso)
		{
			this.LoadPropertyValue(this.GetModel().PropertyModels[propertyName]!, propertyValue, readNormalizer, loadOldValueAlso);
		}

		private void LoadPropertyValue(int propertyIndex, object? propertyValue, Func<IPropertyModel, object?, object?> readNormalizer, bool loadOldValueAlso)
		{
			this.LoadPropertyValue(this.GetModel().PropertyModels[propertyIndex], propertyValue, readNormalizer, loadOldValueAlso);
		}

		private void LoadPropertyValue(IPropertyModel propertyModel, object? propertyValue, Func<IPropertyModel, object?, object?> readNormalizer, bool loadOldValueAlso)
		{
			propertyValue = readNormalizer(propertyModel, propertyValue);

			this.SetFieldValue(propertyModel.PropertyIndex, propertyValue!);

			if (loadOldValueAlso)
				this.SetOldFieldValue(propertyModel.PropertyIndex, propertyValue!);
		}

		#endregion |   Property Loads   |

		#region |   Protected Methods   |

		protected virtual void OnBeforeAcceptChanges() { }
		protected virtual void OnAcceptChanges() { }

		protected virtual IPropertyModel[] GetChangedStorableTransactionLogPropertyModels()
		{
			return this.GetChangedPropertyIndexes().ToModelSequence(this.GetModel().PropertyModels);
		}


		//protected bool DeleteGraphElement(GraphElement graphElement, bool checkGraphElements, object requester)
		//{
		//	graphElement.SetRelatedTransactionRequest(this, TransactionRequestAction.Save);
		//	bool isRemoved = this.Manager.Delete(graphElement, checkCanDelete: false, checkSimpleObjectGraphElements: checkGraphElements, requester);

		//	//if (isRemoved)
		//	//	this.RelatedTransactionRequests.Add(new TransactionRequest(graphElement, TransactionRequestAction.Delete));

		//	return isRemoved;
		//}

		//protected bool DeleteGraphElementByGraphKey(int graphKey, bool checkGraphElements, object requester)
		//{
		//	bool isRemoved = true; //false;
		//	GraphElement graphElementToRemove = this.GetGraphElement(graphKey);

		//	if (graphElementToRemove != null)
		//		isRemoved = this.DeleteGraphElement(graphElementToRemove, checkGraphElements, requester);

		//	return isRemoved;
		//}

		//protected void SetPropertyValue<T>(IPropertyModel propertyModel, T value, ref T propertyValue, T oldPropertyValue)
		//{
		//	this.SetPropertyValue<T>(propertyModel, value, ref propertyValue, oldPropertyValue, requester: this.Requester);
		//}

		//protected void SetPropertyValue<T>(IPropertyModel propertyModel, T value, ref T propertyValue, T oldPropertyValue, object requester)
		//{
		//	this.SetPropertyValue<T>(propertyModel, value, ref propertyValue, oldPropertyValue, this.GetChangeContainter(), requester);
		//}

		//protected void SetPropertyValue<T>(IPropertyModel propertyModel, T value, ref T propertyValue, T oldPropertyValue, ChangeContainer changeContainer, object requester)
		//{
		//	this.SetPropertyValueInternal<T>(propertyModel, value, ref propertyValue, oldPropertyValue, propertyModel.AddOrRemoveInChangedProperties, propertyModel.FirePropertyValueChangeEvent, changeContainer: changeContainer, requester);
		//}

		//protected void SetPropertyValueInternal<T>(IPropertyModel propertyModel, T value, ref T propertyValue, ref T oldPropertyValue, bool firePropertyValueChangeEvent, bool addOrRemoveInChangedProperties, object requester)
		//{
		//	this.SetPropertyValueInternal<T>(propertyModel, value, ref propertyValue, oldPropertyValue, firePropertyValueChangeEvent, addOrRemoveInChangedProperties, decideActionFromModel: false, enforceAccessModifier: false, requester);
		//}
		////////

		//protected void SetPropertyValueInternal<T>(IPropertyModel propertyModel, T value, ref T propertyValue, T oldPropertyValue, ChangeContainer changeContainer,  object requester)
		//{
		//	this.SetPropertyValueInternal<T>(propertyModel, value, ref propertyValue, oldPropertyValue, propertyModel.AddOrRemoveInChangedProperties, propertyModel.FirePropertyValueChangeEvent, changeContainer, requester);
		//}

		//protected void SetPropertyValueInternal<T>(IPropertyModel propertyModel, T value, ref T propertyValue, T oldPropertyValue, bool addOrRemoveInChangedProperties, bool firePropertyValueChangeEvent, ChangeContainer changeContainer, object requester)
		//{
		//	this.SetPropertyValueInternal(propertyModel, value, addOrRemoveInChangedProperties, firePropertyValueChangeEvent, enforceAccessModifier: false, changeContainer, requester);
		//}

		/// <summary>
		/// Fires whe the new object is created and is not loaded from database.
		/// </summary>
		/// <param name="requester"></param>
		protected virtual void OnNewObjectCreated(ChangeContainer? changeContainer, ObjectActionContext context, object? requester) { }
		
		/// <summary>
		/// Set the default values of the properties or do what ever you wants when initializing the object.
		/// </summary>
		protected virtual void OnInitialize() {	}

		/// <summary>
		/// Do the action after property values are loaded from the datastore.
		/// </summary>
		protected virtual void OnAfterLoad() { }

		//protected void Save()
		//      {
		//          this.Save(null);
		//      }

		//      protected void Save(object requester)
		//      {
		//          this.ObjectManager.Save(requester, this);
		//      }

		//      protected void Delete()
		//      {
		//          this.Delete(null);
		//      }

		//      protected void Delete(object requester)
		//      {
		//          this.ObjectManager.Delete(requester, this);
		//      }

		//      protected SimpleObjectValidationResult Validate()
		//      {
		//          SimpleObjectValidationResult validationResult = this.ObjectManager.Validate(this);
		//          return validationResult;
		//      }

		protected virtual void OnImageNameChange(string imageName, string oldImageName)	{ }

		//protected virtual void OnBeforeLoading(object requester)
		//{
		//}

		//protected virtual void OnAfterLoad(object requester)
		//{
		//}

		/// <summary>
		/// Do action before object is saved. This method is invoked only in non server working mode.
		/// </summary>
		/// <param name="requester"></param>
		protected virtual void OnBeforeSave(ChangeContainer? changeContainer, ObjectActionContext context, object? requester) { }

		//protected virtual void OnSaving(object requester)
		//{
		//}

		protected virtual void OnAfterSave(bool isNewBeforeSaving, ChangeContainer? changeContainer, ObjectActionContext context, object? requester) { }

		protected virtual void OnRequestDelete(ChangeContainer? changeContainer, ObjectActionContext context, object? requester) { }

		//protected virtual void OnClientRequestDelete(ChangeContainer? changeContainer, object? requester) { }

		protected virtual void OnGraphElementDeleteRequest(GraphElement graphElement, ChangeContainer? changeContainer, ObjectActionContext context, object? requester) { }

		protected virtual void OnDeleteRequestCancel(ChangeContainer? changeContainer, ObjectActionContext context, object? requester) { }

		/// <summary>
		/// Do action before object is deleted. This method is invoked only in non server working mode.
		/// </summary>
		/// <param name="changeContainer"></param>
		/// <param name="requester"></param>
		protected virtual void OnBeforeDelete(ChangeContainer? changeContainer, ObjectActionContext context, object? requester) { }

		protected virtual void OnBeforePropertyValueChange(IPropertyModel propertyModel, object? value, object? newValue, bool willBeChanged, ChangeContainer? changeContainer, ObjectActionContext context, object? requester) { }

		protected virtual void OnPropertyValueChange(IPropertyModel propertyModel, object? value, object? oldValue, bool isChanged, ChangeContainer? changeContainer, ObjectActionContext context, object? requester) { }

		//protected virtual void OnSaveablePropertyValueChange(IPropertyModel propertyModel, object? value, object? oldValue, bool isChanged, ChangeContainer changeContainer, object? requester) { }

		protected virtual void OnChangedPropertiesCountChange(int changedPropertiesCount, int oldChangedPropertiesCount) { }

		//protected virtual void OnChangedSaveablePropertiesCountChange(int changedPropertiesCount, int oldChangedPropertiesCount) { }

		protected virtual void OnNewGraphElementCreated(GraphElement graphElement, ChangeContainer? changeContainer, ObjectActionContext context, object? requester) { }

		protected virtual void OnGraphElementParentChange(GraphElement graphElement, GraphElement? oldParent, ChangeContainer? changeContainer, ObjectActionContext context, object? requester) { }

		protected virtual void OnGraphElementOrderIndexChange(GraphElement graphElement, int orderIndex, int oldOrderIndex, ChangeContainer? changeContainer, ObjectActionContext context, object? requester) { }

		protected virtual void OnBeforeGraphElementDelete(GraphElement graphElement, ChangeContainer? changeContainer, ObjectActionContext context, object? requester) { }

		protected virtual void OnObjectIdChange(long oldTempId, long newId, ChangeContainer? changeContainer, ObjectActionContext context, object? requester) { }


		#endregion |   Protected Methods   |

		#region |   Private Raise Event Methods   |

		private void RaiseBeforePropertyValueChange(IPropertyModel propertyModel, object? value, object? newValue, bool willBeChanged, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			this.BeforePropertyValueChange?.Invoke(this, new BeforeChangePropertyValuePropertyModelSimpleObjectChangeContainerContextRequesterEventArgs(this, propertyModel, value, newValue, willBeChanged, changeContainer, context, requester));
		}

		private void RaisePropertyValueChange(IPropertyModel propertyModel, object? value, object? oldValue, bool isChanged, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			this.PropertyValueChange?.Invoke(this, new ChangePropertyValuePertyModelSimpleObjectChangeContainerContextRequesterEventArgs(this, propertyModel, value, oldValue, isChanged, changeContainer, context, requester));
		}

		//private void RaiseSaveablePropertyValueChange(IPropertyModel propertyModel, object value, object oldValue, bool isChanged, object requester)
		//{
		//	this.SaveablePropertyValueChange?.Invoke(this, new ChangePropertyValueSimpleObjectRequesterEventArgs(this, propertyModel, value, oldValue, isChanged, isSaveable: true, requester));
		//}

		private void RaiseChangedPropertiesCountChange(int changedPropertiesCount, int oldChangedPropertiesCount)
		{
			this.ChangedPropertiesCountChange?.Invoke(this, new CountChangeSimpleObjectEventArgs(this, changedPropertiesCount, oldChangedPropertiesCount));
		}

		//private void RaiseChangedSaveablePropertiesCountChange(int changedPropertiesCount, int oldChangedPropertiesCount)
		//{
		//	this.ChangedSaveablePropertiesCountChange?.Invoke(this, new CountChangeSimpleObjectEventArgs(this, changedPropertiesCount, oldChangedPropertiesCount));
		//}

		#endregion |   Private Raise Event Methods   |

		#region |   Private Methods   |

		//private List<SimpleObject> GetRelatedTransactionObjects(SimpleObject simpleObject)
		//{
		//	lock (this.lockObject)
		//	{
		//		List<SimpleObject> result = new List<SimpleObject>();

		//		this.GetRelatedTransactionRequests(this, ref result);

		//		return result;
		//	}
		//}

		//private void GetRelatedTransactionRequests(SimpleObject simpleObject, ref List<SimpleObject> relatedTransactionObjects)
		//{
		//	foreach (var item in simpleObject.RelatedTransactionRequests.ToArray())
		//	{
		//		SimpleObject relatedSimpleObject = item.Key;

		//		if (!relatedTransactionObjects.Contains(simpleObject)) // FirstOrDefault(element => element == simpleObject) == null)
		//		{
		//			relatedTransactionObjects.Add(simpleObject);
		//			this.GetRelatedTransactionRequests(relatedSimpleObject, ref relatedTransactionObjects);
		//		}
		//	}
		//}

		//private void SetPropertyValueInternal(IPropertyModel propertyModel, object value, bool addOrRemoveInChangedProperties, bool firePropertyValueChangeEvent, 
		//									  bool enforceAccessModifier, ChangeContainer changeContainer, object requester)
		//{
		//	object propertyValue = this.GetFieldValue(propertyModel.PropertyIndex);
		//	object oldPropertyValue = this.GetOldFieldValue(propertyModel.PropertyIndex);

		//	this.SetPropertyValueInternal<object>(propertyModel, value, ref propertyValue, oldPropertyValue, addOrRemoveInChangedProperties, firePropertyValueChangeEvent, 
		//										  enforceAccessModifier, setPropertyValueAlternative: () => this.SetFieldValue(propertyModel.PropertyIndex, value), changeContainer, requester);
		//}

		//private void SetPropertyValueInternal<T>(IPropertyModel propertyModel, T value, ref T propertyValue, T oldPropertyValue, bool addOrRemoveInChangedProperties, bool firePropertyValueChangeEvent, 
		//										 bool enforceAccessModifier, ChangeContainer changeContainer, object requester)
		//{
		//	this.SetPropertyValueInternal<T>(propertyModel, value, ref propertyValue, oldPropertyValue, addOrRemoveInChangedProperties, firePropertyValueChangeEvent, enforceAccessModifier, () => { }, changeContainer, requester);
		//}

		//private void AddToNullCollections(SimpleObject simpleObject)
		//{
		//	foreach (IOneToManyRelationModel oneToManyRelationModel in simpleObject.GetModel().RelationModel.AsForeignObjectInOneToManyRelations)
		//	{
		//		SimpleObjectCollection nullCollection = simpleObject.GetOneToManyForeignNullCollection(oneToManyRelationModel.RelationKey);

		//		if (nullCollection != null && !nullCollection.Contains(simpleObject)) // If collection initializes, its already contains this
		//			nullCollection.Add(simpleObject);
		//	}
		//}


		//private void SetPropertyValueInternal(IPropertyModel propertyModel, object? value, bool addOrRemoveInChangedProperties, bool firePropertyValueChangeEvent, bool enforceAccessModifier, ChangeContainer? changeContainer, object? requester)
		//{
		//	this.SetPropertyValueInternal(propertyModel, value, addOrRemoveInChangedProperties, firePropertyValueChangeEvent, enforceAccessModifier, changeContainer, trimBeforeStringComparison: true, requester);
		//}

		private void SetPropertyValuePrivate(IPropertyModel propertyModel, object? newValue, bool trimStringBeforeComparison, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			//if (this.deleteStarted || this.isDeleted || this.deleteRequested)
			//	return;

			//if (!addOrRemoveInChangedProperties)
			//	throw new NotSupportedException("Property is changed without counting it!");

			if (this.IsReadOnly)
				throw new NotSupportedException("The property is about to be set and the SimpleObject is read-only.");

			//if (this.DeleteRequested)
			//	throw new NotSupportedException("When delete is requested, no property change is allowed.");

			if (propertyModel.PropertyIndex == SimpleObject.IndexPropertyId)
				throw new NotSupportedException("You cannot change object Id.");

			if (context == ObjectActionContext.Unspecified)
				context = (this.Manager.WorkingMode == ObjectManagerWorkingMode.Server) ? ObjectActionContext.ServerTransaction : ObjectActionContext.Client;

			if (this.isPropertyInitialization)
			{
				this.SetFieldValue(propertyModel.PropertyIndex, newValue!);
				this.SetOldFieldValue(propertyModel.PropertyIndex, newValue!);
			}
			else
			{
				//IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

				//if ((propertyModel == null && this.ObjectManager.EnforcePropertyExistanceInModel) || (decideActionFromModel && propertyModel == null))
				//	throw new NotSupportedException("The property is about to be set and the property name does not exist in the model property definition: Object Type = " + this.GetType().Name + ", Property Index = " + propertyModel.Index + " (" + propertyModel.Name + ")");
				
				// enforceAccessModifier &&

				object? currentValue = this.GetFieldValue(propertyModel!.PropertyIndex);
				object? oldValue = this.GetOldFieldValue(propertyModel.PropertyIndex);
				bool isChanged;
				int oldChangedPropertiesCount = 0, newChangedPropertiesCount = 0;
				//int oldChangedSaveablePropertiesCount = 0, newChangedSaveablePropertiesCount = 0;
				//bool isSaveable;

				//if (this.isDeleteStarted || this.isDeleted)
				//	return;

				//if (this.IsReadOnly)
				//	throw new NotSupportedException("The property is about to be set and the SimpleObject is read-only.");

				//IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

				//if (propertyModel == null && this.ObjectManager.EnforcePropertyExistanceInModel)
				//	throw new NotSupportedException("The property is about to be set and does not exists in the model property definition: Object Type = " + this.GetType().Name + ", Property Index = " + propertyIndex);

				//// Allow sets property for protected internal methods
				////if (propertyModel.AccessPolicy == PropertyAccessPolicy.ReadOnly)
				////	throw new NotSupportedException("The property is about to be set and the property model access policy is read-only: Property Name = " + propertyIndex);


				//lock (this.lockObject)
				//{
					if (Comparison.IsEqual(newValue, currentValue, trimStringBeforeComparison)) 
						return; // Nothing to do

					//bool doFirePropertyValueChangeEvent = firePropertyValueChangeEvent;
					//bool doAddOrRemoveInChangedProperties = addOrRemoveInChangedProperties;

					//if (decideActionFromModel)
					//{
					//	firePropertyValueChangeEvent = propertyModel.FirePropertyValueChangeEvent;
					//	addOrRemoveInChangedProperties = propertyModel.AddOrRemoveInChangedProperties; // && propertyModel.IsStorable;
					//}
					//else //if (propertyModel != null)
					//{
					//	doAddOrRemoveInChangedProperties &= propertyModel.IsStorable;
					//}

					//bool doFirePropertyValueChangeEvent = (decideActionFromModel && propertyModel != null && propertyModel.FirePropertyValueChangeEvent) || (!decideActionFromModel && firePropertyValueChangeEvent);
					//bool doAddOrRemoveInChangedProperties = (decideActionFromModel && propertyModel != null && propertyModel.AddOrRemoveInChangedProperties) || (!decideActionFromModel && addOrRemoveInChangedProperties);
					isChanged = !Comparison.IsEqual(newValue, oldValue, trimStringBeforeComparison);

					this.BeforePropertyValueIsChanged(propertyModel, currentValue, newValue: newValue, willBeChanged: isChanged, changeContainer, context, requester);
					this.SetFieldValue(propertyModel.PropertyIndex, newValue!); // propertyValue = value;
					//setPropertyValueAlternative?.Invoke(); // setPropertyValueAlternative();

					//object previousPropertyValue = propertyValue;
					oldChangedPropertiesCount = this.changedPropertyIndexes.Count;
					//oldChangedSaveablePropertiesCount = this.changedSaveablePropertyIndexes.Count;
					//newChangedSaveablePropertiesCount = oldChangedSaveablePropertiesCount;
					//isSaveable = this.IsPropertySaveable(propertyModel);

					//if (addOrRemoveInChangedProperties)
					//{
						if (isChanged)
							this.changedPropertyIndexes.Add(propertyModel.PropertyIndex);
						else
							this.changedPropertyIndexes.Remove(propertyModel.PropertyIndex);

						newChangedPropertiesCount = this.changedPropertyIndexes.Count;

						//if (isSaveable)
						//{
						//	if (isChanged)
						//		this.changedSaveablePropertyIndexes.Add(propertyModel.PropertyIndex);
						//	else
						//		this.changedSaveablePropertyIndexes.Remove(propertyModel.PropertyIndex);

						//	newChangedSaveablePropertiesCount = this.changedSaveablePropertyIndexes.Count;
						//}
					//}
				//}

				//if (addOrRemoveInChangedProperties)
				//{
					if (oldChangedPropertiesCount != newChangedPropertiesCount)
						this.ChangedPropertiesCountIsChanged(newChangedPropertiesCount, oldChangedPropertiesCount, changeContainer, requester);

					//if (oldChangedSaveablePropertiesCount != newChangedSaveablePropertiesCount)
					//	this.ChangedSaveablePropertiesCountIsChanged(this.changedSaveablePropertyIndexes.Count, oldChangedSaveablePropertiesCount, changeContainer, requester);
				//}

				//if (firePropertyValueChangeEvent)
				//{
					//if (isSaveable)
					//	this.SaveablePropertyValueIsChanged(propertyModel, value, propertyValue, isChanged, changeContainer, requester);

					this.PropertyValueIsChanged(propertyModel, newValue, currentValue, isChanged, changeContainer, context, requester);
				//}
			}
		}

		//private bool IsPropertySaveable(IPropertyModel propertyModel)
		//{
		//	//if (this.Manager.WorkingMode == ObjectManagerWorkingMode.Client)
		//	//	return propertyModel.IsClientSeriazable || propertyModel.IsStorable;
		//	//else
		//	//	return propertyModel.IsStorable;

		//	return propertyModel.IsClientSeriazable || propertyModel.IsStorable;
		//}

		//private void SetPropertyValueInternal(IPropertyModel propertyModel, object value, bool addOrRemoveInChangedProperties, bool firePropertyValueChangeEvent, bool decideActionFromModel, bool enforceAccessModifier, object requester)
		//{
		//	this.SetPropertyValueInternal(propertyModel.Index, value, addOrRemoveInChangedProperties, firePropertyValueChangeEvent, decideActionFromModel, enforceAccessModifier, requester);
		//}

		//private void SetPropertyValueInternal(int propertyIndex, object value, bool addOrRemoveInChangedProperties, bool firePropertyValueChangeEvent, bool decideActionFromModel, bool enforceAccessModifier, object requester)
		//{
		//	this.SetPropertyValueInternal<object>(propertyIndex, value, addOrRemoveInChangedProperties, firePropertyValueChangeEvent, decideActionFromModel, enforceAccessModifier, requester);

		//	if (this.isDeleteStarted || this.isDeleted)
		//		return;

		//	if (this.IsReadOnly)
		//		throw new NotSupportedException("The property is about to be set and the SimpleObject is read-only.");

		//	if (propertyIndex == SimpleObject.IndexPropertyKey)
		//		throw new NotSupportedException("You cannot change object key.");

		//	IPropertyModel propertyModel = this.GetModel().PropertyModels[propertyIndex];

		//	if ((propertyModel == null && this.ObjectManager.EnforcePropertyExistanceInModel) || (decideActionFromModel && propertyModel == null))
		//		throw new NotSupportedException("The property is about to be set and the property name does not exist in the model property definition: Object Type = " + this.GetType().Name + ", Property Index = " + propertyIndex + " (" + propertyModel.Name + ")");

		//	if (enforceAccessModifier && propertyModel != null && propertyModel.AccessPolicy == PropertyAccessPolicy.ReadOnly)
		//		throw new NotSupportedException("The property is about to be set and the property model access policy is read-only: Object Type = " + this.GetType().Name + ", Property Index = " + propertyIndex + " (" + propertyModel.Name + ")");

		//	lock (lockObject)
		//	{
		//		object propertyValue = this.GetFieldValue(propertyIndex);

		//		if (Comparison.IsEqual(value, propertyValue))
		//			return;

		//		object oldPropertyValue = this.GetOldFieldValue(propertyIndex);
		//		object previousPropertyValue = propertyValue;

		//		bool doFirePropertyValueChangeEvent = firePropertyValueChangeEvent;
		//		bool doAddOrRemoveInChangedProperties = addOrRemoveInChangedProperties;

		//		if (decideActionFromModel)
		//		{
		//			doFirePropertyValueChangeEvent = propertyModel.FirePropertyValueChangeEvent;
		//			doAddOrRemoveInChangedProperties = propertyModel.AddOrRemoveInChangedProperties && propertyModel.IsStorable;
		//		}
		//		else if (propertyModel != null)
		//		{
		//			doAddOrRemoveInChangedProperties &= propertyModel.IsStorable;
		//		}

		//		//bool doFirePropertyValueChangeEvent = (decideActionFromModel && propertyModel != null && propertyModel.FirePropertyValueChangeEvent) || (!decideActionFromModel && firePropertyValueChangeEvent);
		//		//bool doAddOrRemoveInChangedProperties = (decideActionFromModel && propertyModel != null && propertyModel.AddOrRemoveInChangedProperties) || (!decideActionFromModel && addOrRemoveInChangedProperties);

		//		this.BeforePropertyValueIsChanged(this, propertyIndex, propertyValue, value);
		//		this.SetFieldValue(propertyIndex, value);

		//		int oldChangedPropertiesCount = this.changedPropertyIndexes.Count;

		//		if (doAddOrRemoveInChangedProperties)
		//		{
		//			if (!Comparison.IsEqual(value, oldPropertyValue))
		//			{
		//				if (propertyModel.RelationTableIdPropertyIndex > 0)
		//					this.changedPropertyIndexes.Add(propertyModel.RelationTableIdPropertyIndex);

		//				this.changedPropertyIndexes.Add(propertyIndex);
		//			}
		//			else
		//			{
		//				if (propertyModel.RelationTableIdPropertyIndex > 0)
		//					this.changedPropertyIndexes.Remove(propertyModel.RelationTableIdPropertyIndex);

		//				this.changedPropertyIndexes.Remove(propertyIndex);
		//			}
		//		}

		//		if (doFirePropertyValueChangeEvent)
		//			this.PropertyValueIsChanged(requester, propertyIndex, value, previousPropertyValue);

		//		if (doAddOrRemoveInChangedProperties && oldChangedPropertiesCount != this.ChangedPropertiesCount)
		//			this.ChangedPropertiesCountIsChanged(this.changedPropertyIndexes.Count, oldChangedPropertiesCount);
		//	}
		//}
		//private void SetPropertyValue(IPropertyModel propertyModel, object propertyValue, ref int primaryTableId, ChangeContainer changeContainer, object requester)
		//{
		//	this.SetPropertyValue(propertyModel, propertyValue, propertyModel.AddOrRemoveInChangedProperties, propertyModel.FirePropertyValueChangeEvent, ref primaryTableId, changeContainer, requester);
		//}

		//private void SetPropertyValue(IPropertyModel propertyModel, object propertyValue, bool addOrRemoveInChangedProperties, bool firePropertyValueChangeEvent, ref int primaryTableId, ChangeContainer changeContainer, object requester)
		//{
		//	if (propertyModel.IsKey)
		//	{
		//		return;
		//	}
		//	else if (propertyModel.IsRelationTableId)
		//	{
		//		primaryTableId = (int)propertyValue;
		//	}
		//	else if (propertyModel.IsRelationObjectId)
		//	{
		//		long primaryObjectId = (long)propertyValue;

		//		this.SetRelationPrimaryObject(primaryTableId, primaryObjectId, propertyModel.RelationKey, changeContainer, requester);
		//		primaryTableId = 0;
		//	}
		//	else
		//	{
		//		this.SetPropertyValueInternal(propertyModel, propertyValue, addOrRemoveInChangedProperties, firePropertyValueChangeEvent, changeContainer, requester);
		//	}
		//}

		private void RunChangedPropertyCountAction(Action changedPropertysAction, ChangeContainer? changeContainer, object? requester)
        {
            int oldChangedPropertiesCount = this.ChangedPropertiesCount;
            
			changedPropertysAction();

			if (this.ChangedPropertiesCount != oldChangedPropertiesCount)
				this.ChangedPropertiesCountIsChanged(this.ChangedPropertiesCount, oldChangedPropertiesCount, changeContainer, requester);
        }

        private void BeforePropertyValueIsChanged(IPropertyModel propertyModel, object? value, object? newValue, bool willBeChanged, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
        {
            this.RaiseBeforePropertyValueChange(propertyModel, value, newValue, willBeChanged, changeContainer, context, requester);
            this.OnBeforePropertyValueChange(propertyModel, value, newValue, willBeChanged, changeContainer, context, requester);
            this.Manager.BeforePropertyValueIsChanged(this, propertyModel, value, newValue, willBeChanged, changeContainer, context, requester);
       }

        private void PropertyValueIsChanged(IPropertyModel propertyModel, object? value, object? oldValue, bool isChanged, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
        {
			this.RaisePropertyValueChange(propertyModel, value, oldValue, isChanged, changeContainer, context, requester);
			this.OnPropertyValueChange(propertyModel, value, oldValue, isChanged, changeContainer, context, requester);
			this.Manager.PropertyValueIsChanged(this, propertyModel, value, oldValue, isChanged, changeContainer, context, requester);

			IPropertyModel objectSubTypePropertyModel = this.GetModel().ObjectSubTypePropertyModel;

			if (objectSubTypePropertyModel != null && objectSubTypePropertyModel.PropertyIndex == propertyModel.PropertyIndex)
				this.RecalcImageName();
		}

		//private void SaveablePropertyValueIsChanged(IPropertyModel propertyModel, object value, object oldValue, bool isChanged, ChangeContainer? changeContainer, object? requester)
		//{
		//	this.RaiseSaveablePropertyValueChange(propertyModel, value, oldValue, isChanged, requester);
		//	this.OnSaveablePropertyValueChange(propertyModel, value, oldValue, isChanged, changeContainer, requester);
		//	this.Manager.SaveablePropertyValueIsChanged(this, propertyModel, value, oldValue, isChanged, changeContainer, requester);
		//}

		private void ChangedPropertiesCountIsChanged(int changedPropertiesCount, int oldChangedPropertiesCount, ChangeContainer? changeContainer, object? requester)
        {
            this.RaiseChangedPropertiesCountChange(changedPropertiesCount, oldChangedPropertiesCount);
            this.OnChangedPropertiesCountChange(changedPropertiesCount, oldChangedPropertiesCount);
            this.Manager.ChangedPropertiesCountIsChanged(this, changedPropertiesCount, oldChangedPropertiesCount, changeContainer, requester);
        }

		//private void ChangedSaveablePropertiesCountIsChanged(int changedPropertiesCount, int oldChangedPropertiesCount, ChangeContainer? changeContainer, object? requester)
		//{
		//	this.RaiseChangedSaveablePropertiesCountChange(changedPropertiesCount, oldChangedPropertiesCount);
		//	this.OnChangedSaveablePropertiesCountChange(changedPropertiesCount, oldChangedPropertiesCount);
		//	this.Manager.ChangedSaveablePropertiesCountIsChanged(this, changedPropertiesCount, oldChangedPropertiesCount, changeContainer, requester);
		//}

		//private object[] GetPropertyValues(int[] propertyIndexSequence, Func<int, object> getPropertyValue)
		//{
		//	lock (this.lockObject)
		//	{
		//		object[] result = new object[propertyIndexSequence.Length];

		//		for (int i = 0; i < result.Length; i++)
		//			result[i] = getPropertyValue(propertyIndexSequence[i]);

		//		return result;
		//	}
		//}

		//private object[] GetPropertyValues(int[] propertyIndexSequence, Func<int, object> getPropertyValue, Func<IPropertyModel, object, object> normalizer)
		//{
		//	lock (this.lockObject)
		//	{
		//		object[] result = new object[propertyIndexSequence.Length];

		//		for (int i = 0; i < result.Length; i++)
		//			result[i] = normalizer(this.GetModel().PropertyModels[propertyIndexSequence[i]], getPropertyValue(propertyIndexSequence[i]));

		//		return result;
		//	}
		//}

		//private PropertyValuePairs GetChangedPropertyValues(Func<IPropertyModel, object, object> normalizer, Func<int, object> getFieldValueFunc)
		//{
		//	lock (this.lockObject)
		//	{
		//		IPropertyModel[] propertyModelSequence = this.changedPropertyModels.ToArray();
		//		object[] propertyValues = new object[propertyModelSequence.Length];

		//		for (int i = 0; i < propertyModelSequence.Length; i++)
		//		{
		//			IPropertyModel propertyModel = propertyModelSequence[i];
		//			object propertyValue = getFieldValueFunc(propertyModel.Index);

		//			propertyValue = normalizer(propertyModel, propertyValue);

		//			propertyValues[i] = propertyValue;
		//		}

		//		return new PropertyValuePairs(new PropertySequence(propertyModelSequence), propertyValues);
		//	}
		//}
		//private PropertyValueSequence GetPropertyValues(IPropertyModel[] propertyModels, Func<int, object> getFieldValueFunc, Func<IPropertyModel, object, object> normalizer)
		//{
		//	return this.GetPropertyValues(new PropertySequence(propertyModels), getFieldValueFunc, normalizer);
		//}

		//private PropertyValueSequence GetPropertyValues(IPropertySequence propertyModelSequence, Func<int, object> getFieldValueFunc, Func<IPropertyModel, object, object> normalizer)
		//{
		//	lock (this.lockObject)
		//	{
		//		object[] propertyValues = new object[propertyModelSequence.Length];

		//		for (int i = 0; i < propertyModelSequence.Length; i++)
		//		{
		//			IPropertyModel propertyModel = propertyModelSequence.PropertyModels[i];
		//			object propertyValue = getFieldValueFunc(propertyModel.Index);
		//			propertyValue = normalizer(propertyModel, propertyValue);

		//			propertyValues[i] = propertyValue;
		//		}

		//		return new PropertyValueSequence(propertyModelSequence, propertyValues);
		//	}
		//}


		//private void RelatedTransactionRequests_AfterAdd(object sender, DictionaryActionEventArgs<SimpleObject, TransactionRequestAction> e)
		//{
		//	this.Manager.RelatedTransactionRequestCountIsChanged(this, this.relatedTransactionRequests.Count - 1, this.relatedTransactionRequests.Count);
		//}

		//private void RelatedTransactionRequests_AfterRemove(object sender, DictionaryActionEventArgs<SimpleObject, TransactionRequestAction> e)
		//{
		//	this.Manager.RelatedTransactionRequestCountIsChanged(this, this.relatedTransactionRequests.Count + 1, this.relatedTransactionRequests.Count);
		//}

		//private void RelatedTransactionRequests_AfterClear(object sender, int oldCount)
		//{
		//	this.Manager.RelatedTransactionRequestCountIsChanged(this, oldCount, 0);
		//}


		#endregion |   Private Methods   |

		#region |   IBindingObject Interface   |

		//object IBindingObject.GetOldPropertyValue(string propertyName)
		//{
		//	return this.GetPropertyValue(propertyName, this.OldPropertyValues);
		//}

		//T IBindingObject.GetOldPropertyValue<T>(string propertyName)
		//{
		//	return (T)this.GetPropertyValue(propertyName, this.OldPropertyValues);
		//}



		//object IBindingObject.GetObject()
		//{
		//	return this;
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

		#endregion |   IBindingObject Interface   |

		#region |   Dispose   |

		//partial void Dispose()
		//      {
		//          this.changedPropertyIndexes = null;
		//	this.tag = null;
		//	this.objectManager = null;
		//      }

		//      void IDisposable.Dispose()
		//      {
		//          this.Dispose();
		//      }

		#endregion |   Dispose   |
	}
}