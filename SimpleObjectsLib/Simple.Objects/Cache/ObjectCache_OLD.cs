//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Data;
//using System.Reflection;
//using System.Globalization;
//using Simple.Modeling;
////using Simple.Datastore;
//using Simple;
//using Simple.Collections;
//using Simple.Serialization;

//namespace Simple.Objects
//{
//	public class ObjectCache_OLD
//	{
//		private SimpleObjectManager objectManager;
//		private ISimpleObjectModel objectModel;

//		private UniqueKeyGenerator<long> idGenerator;
//		private Dictionary<long, SimpleObject> simpleObjectsByObjectId = new Dictionary<long, SimpleObject>();
//		private IPropertyModel[] propertyModelsByDataReaderFieldIndex = null;
//		private Dictionary<long, HashArray<object>> indexersByObjectId = new Dictionary<long, HashArray<object>>();

//		private SimpleObjectCollection? collection = null;
//		private object lockObject = new object();

//		/// <summary>
//		/// Remeber the temp oldIds - newId untill all reference of oldId will be set to new Id (allow to get the object with the old key untill all reference are sets)
//		/// </summary>
//		private Dictionary<long, long> newIdsByTempId = new Dictionary<long, long>();

//		internal ObjectCache_OLD(SimpleObjectManager objectManager, ISimpleObjectModel objectModel)
//			: this(objectManager, objectModel, new List<long>())
//		{
//		}

//		internal ObjectCache_OLD(SimpleObjectManager objectManager, ISimpleObjectModel objectModel, IList<long> objectIds)
//		{
//			this.objectManager = objectManager;
//			this.objectModel = objectModel;
//			this.idGenerator = new UniqueKeyGenerator<long>(objectIds, this.ObjectManager.MinObjectId, this.ObjectManager.ReuseObjectKeys);
//		}

//		internal ObjectCache_OLD(SimpleObjectManager objectManager, ISimpleObjectModel objectModel, IDataReader dataReader)
//		{
//			this.objectManager = objectManager;
//			this.objectModel = objectModel;

//			//this.LoadAllRecords(dataReader);
//			//HashSet<long> objectIds = new HashSet<long>();
//			List<long> objectIds = new List<long>();

//			// Load all records
//			while (dataReader.Read())
//			{
//				SimpleObject simpleObject = this.CreateObjectFromPropertyValueData((so) => so.LoadFrom(dataReader, this.GetPropertyModelsByDataReaderFieldIndex(dataReader),
//																									   readNormalizer: this.ObjectManager.NormalizeWhenReadingFromDatastore, 
//																									   loadOldValuesAlso: true), changeContainer: null, ObjectActionContext.Unspecified);
//				long objectId = simpleObject.Id;

//				objectIds.Add(objectId);
//				//this.simpleObjectsByObjectId.Add(objectId, simpleObject);
//				simpleObject.InternalState = SimpleObjectInternalState.Normal;
//				simpleObject.AfterLoad();
//			}

//			this.idGenerator = new UniqueKeyGenerator<long>(objectIds, this.ObjectManager.MinObjectId, this.ObjectManager.ReuseObjectKeys);
//		}

//		internal void SetIndexers(Dictionary<long, HashArray<object>> indexersByObjectId)
//		{
//			this.indexersByObjectId = indexersByObjectId;
//		}

//		public SimpleObjectManager ObjectManager
//		{
//			get { return this.objectManager; }
//		}

//		public ISimpleObjectModel ObjectModel
//		{
//			get { return this.objectModel; }
//		}

//		/// <summary>
//		/// Gets the Object Id values
//		/// </summary>
//		public List<long> GetObjectIds => this.idGenerator.Keys.ToList();

//		public bool ContainsId(long objectId) => this.idGenerator.ContainsKey(objectId);

//		public bool IsInCache(long objectId) => this.simpleObjectsByObjectId.ContainsKey(objectId);

//		public SimpleObjectCollection<T> GetObjectCollection<T>() where T : SimpleObject
//		{
//			if (this.collection == null)
//				this.collection = new SimpleObjectCollection<T>(this.ObjectManager, this.ObjectModel.TableInfo.TableId, new List<long>(this.idGenerator.Keys)); // The readonly keys are copiedint into new list

//			return (SimpleObjectCollection<T>)this.collection;
//		}

//		public SimpleObject? GetObject(long objectId)
//		{
//			lock (this.lockObject)
//			{
//				SimpleObject? simpleObject = this.GetObjectInternal(objectId);

//				if (simpleObject == null) // objectId is oldId (old temp Id) -> try to get object by old temp Id.
//				{
//					long newId;

//					if (this.newIdsByTempId.TryGetValue(objectId, out newId))
//						simpleObject = this.GetObjectInternal(newId);
//				}

//				return simpleObject;
//			}
//		}

//		private SimpleObject? GetObjectInternal(long objectId)
//		{
//			SimpleObject? simpleObject;

//			if (!this.simpleObjectsByObjectId.TryGetValue(objectId, out simpleObject)) // <- IsInChache
//			{
//				// Object is not in cache. Lets check if object key exists in key list
//				if (this.ContainsId(objectId))
//				{
//					// object is not in cache -> go to local or remote datastore (remote object server), create and load the object.
//					simpleObject = this.CreateAndLoadObject(objectId, (simpleObject) => this.ObjectManager.Datastore.LoadObjectPropertyValues(simpleObject).GetAwaiter().GetResult(), changeContainer: null, ObjectActionContext.Unspecified);
//				}
//				else
//				{
//					simpleObject = null;
//				}
//			}

//			return simpleObject;
//		}


//		public SimpleObjectCollection<T> Select<T>(Predicate<T> selector, bool sortCollection = false) where T : SimpleObject
//        {
//			List<long> objectIds = new List<long>();
//			SimpleObjectCollection<T> result; // new SimpleObjectCollection<T>(this.ObjectManager, this.ObjectModel.TableInfo.TableId,  isSortable);

//			//lock (this.lockObject)
//			//{
//			//result.BeginLoad();
//			if (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Client)
//				sortCollection = sortCollection;


//				foreach (long id in this.GetObjectIds.ToArray())
//					if (this.GetObject(id) is T t && selector(t)) // // The object is deleted or simply not exists, is null or is not custable to T, for any reason
//						objectIds.Add(id);

//				//result.EndLoad();

//				result = new SimpleObjectCollection<T>(this.ObjectManager, this.ObjectModel.TableInfo.TableId, objectIds);
				
//				if (sortCollection) //(result.IsSortable)
//					result.Sort(setPrevious: true);
//			//}

//			return result;
//        }

//		//public SimpleObjectCollection_OLD<T> Select_OLD<T>(Predicate<T> selector, bool isSortable = false) where T : SimpleObject
//		//{
//		//	List<long> objectIds = new List<long>();
//		//	SimpleObjectCollection_OLD<T> result; // new SimpleObjectCollection<T>(this.ObjectManager, this.ObjectModel.TableInfo.TableId,  isSortable);

//		//	lock (this.lockObject)
//		//	{
//		//		//result.BeginLoad();

//		//		foreach (long id in this.GetObjectIds)
//		//		{
//		//			T simpleObject = this.GetObject(id) as T;

//		//			if (selector(simpleObject))
//		//				objectIds.Add(id);
//		//		}

//		//		//result.EndLoad();

//		//		result = new SimpleObjectCollection_OLD<T>(this.ObjectManager, this.ObjectModel.TableInfo.TableId, objectIds, isSortable: this.ObjectModel.SortingModel == SortingModel.BySingleObjectTypeCollection);

//		//		if (isSortable) //(result.IsSortable)
//		//			result.Sort(setPrevious: true, saveObjects: false);
//		//	}

//		//	return result;
//		//}


//		// TODO: Redesign SelectAll...

//		public SimpleObjectCollection<T> SelectAll<T>() where T : SimpleObject
//		{
//			bool isSortable = this.ObjectModel.SortingModel == SortingModel.BySingleObjectTypeCollection;
			
//			return this.Select<T>(item => true, isSortable);
//		}

//		//public SimpleObjectCollection_OLD<T> SelectAllOLD<T>() where T : SimpleObject
//		//{
//		//	bool isSortable = this.ObjectModel.SortingModel == SortingModel.BySingleObjectTypeCollection;

//		//	return this.Select_OLD<T>(item => true, isSortable);
//		//}


//		public SimpleObject? FindFirst(Predicate<SimpleObject> criteria) => this.FindFirst<SimpleObject>(criteria);

//		public T? FindFirst<T>(Predicate<T> crieria) where T : SimpleObject
//		{
//			lock (this.lockObject)
//			{
//				foreach (long id in this.GetObjectIds)
//					if (this.GetObject(id) is T so && crieria(so))
//						return so;
//			}

//			return default;
//		}

//		//public T FindFirst<T>(Predicate<T> match) where T : SimpleObject
//		//{
//		//	return this.FindFirst(match) as T;
//		//}

//		public List<long> GetOneToManyForeignObjectIds(IOneToManyRelationModel oneToManyRelationModel, int primaryTableId, long primaryObjectId)
//		{
//			List<long> collectionIds;

//			if (oneToManyRelationModel.PrimaryTableIdPropertyModel == null)
//			{
//				collectionIds = this.GetRelationCollectionObjectIds(oneToManyRelationModel.PrimaryObjectIdPropertyModel.PropertyIndex, primaryObjectId);
//			}
//			else
//			{
//#if DEBUG
//				if (oneToManyRelationModel.PrimaryObjectTableId > 0 && primaryTableId != oneToManyRelationModel.PrimaryObjectTableId)
//					throw new ArgumentException(String.Format("GetOneToManyCollection error: oneToManyRelationModel.primaryTableId is not the same as T object TableId"));
//#endif
//				collectionIds = this.GetRelationCollectionObjectIds(oneToManyRelationModel.PrimaryTableIdPropertyModel.PropertyIndex, primaryTableId, oneToManyRelationModel.PrimaryObjectIdPropertyModel.PropertyIndex, primaryObjectId);
//			}

//			return collectionIds;
//		}


//		public List<long> GetRelationCollectionObjectIds(int objectIdPropertyIndex, long objectId)
//		{
//			return this.Select(so => so.GetPropertyValue(objectIdPropertyIndex)!.Equals(objectId));
//		}

//		public List<long> GetRelationCollectionObjectIds(int tableIdPropertyIndex, int tableId, int objectIdPropertyIndex, long objectId)
//		{
//			return this.Select(so => so.GetPropertyValue(tableIdPropertyIndex)!.Equals(tableId) && so.GetPropertyValue(objectIdPropertyIndex)!.Equals(objectId));
//		}


//		protected List<long> Select(Predicate<SimpleObject> selector)
//		{
//			List<long> objectIds = new List<long>();
//			//SimpleObjectCollection<T> result; // new SimpleObjectCollection<T>(this.ObjectManager, this.ObjectModel.TableInfo.TableId,  isSortable);

//			//lock (this.lockObject)
//			//{
//			//result.BeginLoad();

//			foreach (long id in this.idGenerator.Keys.ToArray())
//				if (this.GetObject(id) is SimpleObject so && selector(so)) // // The object is deleted or simply not exists, is null or is not custable to T, for any reason
//					objectIds.Add(id);

//			//result.EndLoad();

//			//result = new SimpleObjectCollection<T>(this.ObjectManager, this.ObjectModel.TableInfo.TableId, objectIds);

//			//if (sortCollection) //(result.IsSortable)
//			//	result.Sort(setPrevious: true, saveObjects: false);
//			//}

//			return objectIds;
//		}


//		//public List<SimpleObject> Select(Predicate<SimpleObject> selector)
//  //      {
//  //          List<SimpleObject> result = new List<SimpleObject>();

//  //          lock (this.lockObject)
//  //          {
//		//		foreach (long id in this.GetObjectIds)
//		//		{
//		//			SimpleObject? simpleObject = this.GetObject(id);

//		//			if (simpleObject != null && selector(simpleObject))
//		//				result.Add(simpleObject);
//		//		}
//		//	}

//		//	return result;
//  //      }


//		/// <summary>
//		/// Add the <see cref="SimpleObject"/> in the cache. If currentObjectId has zero value and an AutoGenerateKey is set to true object key is generated, otherwise currentObjectId is used as object key.
//		/// </summary>
//		/// <param name="simpleObject">The <see cref="SimpleObject"/> that is about to be set in the object cache.</param>
//		/// <returns>An object key that unique present object within the sam object type.</returns>
//		/// <exception cref="T:System.ArgumentNullException">The <see cref="SimpleObject"/> is null.</exception>
//		/// <exception cref="T:System.ArgumentException">The <see cref="SimpleObject"/> is not the same type as declared.</exception>
//		/// <exception cref="T:System.ArgumentException">The <see cref="SimpleObject"/> IsNew property must be true.</exception>
//		internal long AddNewObject(SimpleObject simpleObject, ChangeContainer? changeContainer, object? requester)
//		{
//			if (simpleObject == null)
//				throw new ArgumentNullException("The simpleObject argument is null.");

//			if (simpleObject.GetType() != this.ObjectModel.ObjectType)
//				throw new ArgumentException("The simpleObject argument is not the same type as declared.");

//			lock (this.lockObject)
//			{
//				long newId = this.CreateNewId();

//				simpleObject.SetId(newId);
//				this.simpleObjectsByObjectId.Add(simpleObject.Id, simpleObject);
//				this.collection?.Add(simpleObject.Id, changeContainer, requester);
//			}

//			return simpleObject.Id;
//		}

//		internal bool RemoveObject(long objectId, ChangeContainer? changeContainer, object? requester)
//		{
//			bool isRemoved;

//			if (isRemoved = this.idGenerator.ContainsKey(objectId))
//			{
//				SimpleObject simpleObject = this.GetObject(objectId);

//				lock (this.lockObject)
//				{
//					this.simpleObjectsByObjectId.Remove(objectId);
//					this.idGenerator.RemoveKey(simpleObject.Id);

//					if (objectId < 0)
//						this.ObjectManager.ClientTempObjectIdGenerator.RemoveKey(-objectId);

//					this.collection?.Remove(objectId, changeContainer, requester);
//				}
//			}

//			return isRemoved;
//		}

//		internal SimpleObject CreateAndLoadObject(IEnumerable<PropertyIndexValuePair> propertyIndexValues)
//		{
//			return this.CreateAndLoadObject(objectId: 0, propertyIndexValues); // objectId will be loaded by propertyIndexValues
//		}

//		internal SimpleObject CreateAndLoadObject(long objectId, IEnumerable<PropertyIndexValuePair> propertyIndexValues)
//		{
//			return this.CreateAndLoadObject(objectId, (simpleObject) => simpleObject.LoadFromServer(propertyIndexValues));
//		}

//		internal SimpleObject CreateObjectFromPropertyValueData(Action<SimpleObject> loadPropertyValueData, ChangeContainer? changeContainer, ObjectActionContext context)
//		{
//			return this.CreateAndLoadObject(objectId: 0, (simpleObject) => loadPropertyValueData(simpleObject), changeContainer, context);
//		}

//		internal SimpleObject CreateAndLoadObject(long objectId, Action<SimpleObject> loadAction, ChangeContainer? changeContainer, ObjectActionContext context)
//		{
//			SimpleObject simpleObject = this.ObjectManager.CreateNewEmptyObjectInternal(this.ObjectModel.ObjectType, isNew: false, objectId, changeContainer, context, requester: this);

//			loadAction(simpleObject);
//			this.simpleObjectsByObjectId.Add(simpleObject.Id, simpleObject);

//			return simpleObject;
//		}

//		internal IPropertyModel[] GetPropertyModelsByDataReaderFieldIndex(IDataReader dataReader)
//		{
//			if (this.propertyModelsByDataReaderFieldIndex == null)
//			{
//				this.propertyModelsByDataReaderFieldIndex = new IPropertyModel[dataReader.FieldCount];

//				for (int i = 0; i < this.propertyModelsByDataReaderFieldIndex.Length; i++)
//					this.propertyModelsByDataReaderFieldIndex[i] = this.objectModel.PropertyModels[dataReader.GetName(i)];
//			}

//			return this.propertyModelsByDataReaderFieldIndex;
//		}

//		/// <summary>
//		/// Replaces the one (temp) object Id with another (new).
//		/// Call this method only from Client (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Client).
//		/// </summary>
//		/// <param name="simpleObject">The <see cref="SimpleObject"/> with an old Id that is about to be changed with a new ones.</param>
//		/// <param name="newId">The new object Id.</param>
//		internal void ReplaceTempId(SimpleObject simpleObject, long newId)
//		{
//			lock (this.lockObject)
//			{
//				long oldId = simpleObject.Id;

//				this.idGenerator.RemoveKey(oldId);
//				this.simpleObjectsByObjectId.Remove(oldId);
//				this.idGenerator.AddKey(newId);
//				this.simpleObjectsByObjectId.Add(newId, simpleObject);
//				this.newIdsByTempId.Add(oldId, newId);

//				simpleObject.SetId(newId);

//			}
//		}

//		internal void ClearTempIds() => this.newIdsByTempId.Clear();

//		private long CreateNewId()
//		{
//			if (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Client)
//			{
//				long id = -this.ObjectManager.ClientTempObjectIdGenerator.CreateKey();

//				this.idGenerator.AddKey(id);

//				return id;
//			}
//			else // (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Server || this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.ClientWithLocalDatastore)
//			{
//				return this.idGenerator.CreateKey();
//			}
//		}
//	}
//}
