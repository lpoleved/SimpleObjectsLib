using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Collections;
using Simple.Modeling;

namespace Simple.Objects
{
	public class ServerObjectCache : ObjectCache
	{
		private UniqueKeyGenerator<long> idGenerator;
		private IPropertyModel[]? propertyModelsByDataReaderFieldIndex = null;
		private SimpleObjectCollection? collection = null;

		public ServerObjectCache(SimpleObjectManager objectManager, ISimpleObjectModel objectModel) 
			: this(objectManager, objectModel, new List<long>())
		{
		}

		public ServerObjectCache(SimpleObjectManager objectManager, ISimpleObjectModel objectModel, IList<long> objectIds)
			: base (objectManager, objectModel)
		{
			this.idGenerator = new UniqueKeyGenerator<long>(objectIds, this.ObjectManager.MinObjectId, this.ObjectManager.ReuseObjectKeys);
		}

		public ServerObjectCache(SimpleObjectManager objectManager, ISimpleObjectModel objectModel, IDataReader dataReader)
			: base(objectManager, objectModel)
		{
			//this.LoadAllRecords(dataReader);
			//HashSet<long> objectIds = new HashSet<long>();
			List<long> objectIds = new List<long>();

			// Load all records
			while (dataReader.Read())
			{
				SimpleObject simpleObject = this.CreateAndLoadObjectInternal(objectId: 0, 
																			 loadAction: (so) => so.LoadFrom(dataReader, this.GetPropertyModelsByDataReaderFieldIndex(dataReader), this.ObjectManager.NormalizeWhenReadingFromDatastore, loadOldValuesAlso: true), 
																			 this.ObjectManager.DefaultChangeContainer, ObjectActionContext.ServerTransaction);
				long objectId = simpleObject.Id;

				objectIds.Add(objectId);
				//this.simpleObjectsByObjectId.Add(objectId, simpleObject);
				simpleObject.InternalState = SimpleObjectInternalState.Normal;
				simpleObject.AfterLoad();
			}

			this.idGenerator = new UniqueKeyGenerator<long>(objectIds, this.ObjectManager.MinObjectId, this.ObjectManager.ReuseObjectKeys);
		}

		/// <summary>
		/// Gets the Object Id values
		/// </summary>
		public List<long> GetObjectIds => this.idGenerator.Keys.ToList();

		public override bool ContainsId(long objectId) => this.idGenerator.ContainsKey(objectId);

		//public override SimpleObject? GetObject(long objectId)
		//{
		//	lock (this.lockObject)
		//	{
		//		SimpleObject? simpleObject = this.GetObjectInternal(objectId);

		//		if (simpleObject == null) // objectId is oldId (old temp Id) -> try to get object by old temp Id.
		//		{
		//			long newId;

		//			if (this.newIdsByTempId.TryGetValue(objectId, out newId))
		//				simpleObject = this.GetObjectInternal(newId);
		//		}

		//		return simpleObject;
		//	}
		//}

		public override SimpleObject? GetObject(long objectId)
		{
			SimpleObject? simpleObject;

			lock (this.lockObject)
			{
				if (!this.simpleObjectsByObjectId.TryGetValue(objectId, out simpleObject)) // <- IsInChache
				{
					// Object is not in cache. Lets check if object key exists in key list
					if (this.ContainsId(objectId))
					{
						// object is not in cache -> go to local or remote datastore (remote object server), create and load the object.
						simpleObject = this.CreateAndLoadObjectInternal(objectId, (simpleObject) => this.ObjectManager.Datastore.LoadObjectPropertyValues(simpleObject).GetAwaiter().GetResult(), this.ObjectManager.DefaultChangeContainer, ObjectActionContext.ServerTransaction);
					}
					else
					{
						simpleObject = null;
					}
				}
			}

			return simpleObject;
		}

		public override SimpleObjectCollection<T> GetObjectCollection<T>()
		{
			if (this.collection == null)
				this.collection = new SimpleObjectCollection<T>(this.ObjectManager, this.ObjectModel.TableInfo.TableId, new List<long>(this.idGenerator.Keys)); // The readonly keys are copied into new list

			return (SimpleObjectCollection<T>)this.collection;
		}

		public override List<long> GetOneToManyForeignObjectIds(int primaryTableId, long primaryObjectId, IOneToManyRelationModel oneToManyRelationModel)
		{
			List<long> objectIds;

			if (oneToManyRelationModel.PrimaryTableIdPropertyModel == null)
			{
				objectIds = this.Select(so => so.GetPropertyValue(oneToManyRelationModel.PrimaryObjectIdPropertyModel.PropertyIndex)!.Equals(primaryObjectId));
			}
			else
			{
#if DEBUG
				if (oneToManyRelationModel.PrimaryObjectTableId > 0 && primaryTableId != oneToManyRelationModel.PrimaryObjectTableId)
					throw new ArgumentException(String.Format("GetOneToManyCollection error: oneToManyRelationModel.primaryTableId is not the same as T object TableId"));
#endif
				objectIds = this.Select(so => so.GetPropertyValue(oneToManyRelationModel.PrimaryTableIdPropertyModel.PropertyIndex)!.Equals(primaryTableId) && 
											  so.GetPropertyValue(oneToManyRelationModel.PrimaryObjectIdPropertyModel.PropertyIndex)!.Equals(primaryObjectId));
			}

			return objectIds;
		}

		//public override List<long> GetRelationCollectionObjectIds(int objectIdPropertyIndex, long objectId)
		//{
		//	return this.Select(so => so.GetPropertyValue(objectIdPropertyIndex)!.Equals(objectId));
		//}

		//public override List<long> GetRelationCollectionObjectIds(int tableIdPropertyIndex, int tableId, int objectIdPropertyIndex, long objectId)
		//{
		//	return this.Select(so => so.GetPropertyValue(tableIdPropertyIndex)!.Equals(tableId) && so.GetPropertyValue(objectIdPropertyIndex)!.Equals(objectId));
		//}

		public SimpleObjectCollection<T> SelectAll<T>() where T : SimpleObject
		{
			List<long> objectIds = this.Select(item => true);
			SimpleObjectCollection<T> result = new SimpleObjectCollection<T>(this.ObjectManager, this.ObjectModel.TableInfo.TableId, objectIds);
			bool sortCollection = this.ObjectModel.SortingModel == SortingModel.BySingleObjectTypeCollection;

			if (this.ObjectModel.SortingModel == SortingModel.BySingleObjectTypeCollection)
				result.Sort(setPrevious: true);

			return result;
		}

		//public override T? FindFirst<T>(Predicate<SimpleObject> criteria) where T : default
		//{
		//	throw new NotImplementedException();
		//}

		public SimpleObject? FindFirst(Predicate<SimpleObject> criteria) => this.FindFirst<SimpleObject>(criteria);

		public T? FindFirst<T>(Predicate<T> criteria) 
			where T : class
		{
			lock (this.lockObject)
			{
				foreach (long id in this.GetObjectIds)
					if (this.GetObject(id) is T so && criteria(so))
						return so;
			}

			return default;
		}

		//public override T? FindFirst<T>(Predicate<SimpleObject> criteria) where T : default
		//{
		//	lock (this.lockObject)
		//	{
		//		foreach (long id in this.GetObjectIds)
		//			if (this.GetObject(id) is T so && crieria(so))
		//				return so;
		//	}

		//	return default;
		//}

		//public override SimpleObject? FindFirst<T>(Predicate<T> crieria) 
		//	where T : SimpleObject
		//{
		//	lock (this.lockObject)
		//	{
		//		foreach (long id in this.GetObjectIds)
		//			if (this.GetObject(id) is T so && crieria(so))
		//				return so;
		//	}

		//	return default;
		//}

		public SimpleObjectCollection<T> Select<T>(Predicate<T> selector, bool sortCollection = false) where T : SimpleObject
		{
			List<long> objectIds = new List<long>();
			SimpleObjectCollection<T> result; // new SimpleObjectCollection<T>(this.ObjectManager, this.ObjectModel.TableInfo.TableId,  isSortable);

			foreach (long id in this.GetObjectIds.ToArray())
				if (this.GetObject(id) is T t && selector(t)) // // The object is deleted or simply not exists, is null or is not custable to T, for any reason
					objectIds.Add(id);

			result = new SimpleObjectCollection<T>(this.ObjectManager, this.ObjectModel.TableInfo.TableId, objectIds);

			if (sortCollection) //(result.IsSortable)
				result.Sort(setPrevious: true);

			return result;
		}


		private List<long> Select(Predicate<SimpleObject> selector) 
		{
			List<long> objectIds = new List<long>();

			foreach (long id in this.idGenerator.Keys.ToArray())
				if (this.GetObject(id) is SimpleObject so && selector(so)) // // The object is deleted or simply not exists, is null or is not custable to T, for any reason
					objectIds.Add(id);

			return objectIds;
		}

		protected override long CreateNewId()
		{
			return this.idGenerator.CreateKey();
		}

		protected override void OnNewObjectAdded(SimpleObject simpleObject, ChangeContainer? changeContainer, object? requester)
		{
			this.collection?.Add(simpleObject.Id, changeContainer, requester);
		}

		internal protected override bool RemoveObjectInternal(long objectId, ChangeContainer? changeContainer, object? requester)
		{
			bool isRemoved;

			if (isRemoved = this.idGenerator.ContainsKey(objectId))
			{
				lock (this.lockObject)
				{
					this.simpleObjectsByObjectId.Remove(objectId);
					this.idGenerator.RemoveKey(objectId);

					this.collection?.Remove(objectId, changeContainer, requester);
				}
			}

			return isRemoved;
		}


		internal IPropertyModel[] GetPropertyModelsByDataReaderFieldIndex(IDataReader dataReader)
		{
			if (this.propertyModelsByDataReaderFieldIndex == null)
			{
				this.propertyModelsByDataReaderFieldIndex = new IPropertyModel[dataReader.FieldCount];

				for (int i = 0; i < this.propertyModelsByDataReaderFieldIndex.Length; i++)
					this.propertyModelsByDataReaderFieldIndex[i] = this.ObjectModel.PropertyModels[dataReader.GetName(i)];
			}

			return this.propertyModelsByDataReaderFieldIndex;
		}
	}
}
