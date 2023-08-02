using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Modeling;

namespace Simple.Objects
{
	public class ClientObjectCache : ObjectCache
	{
		/// <summary>
		/// Remeber the temp oldIds - newId untill all reference of oldId will be set to new Id (allow to get the object with the old key untill all reference are sets)
		/// </summary>
		private Dictionary<long, long> newIdsByTempId = new Dictionary<long, long>(); // Is this necesery ??????

		public ClientObjectCache(SimpleObjectManager objectManager, ISimpleObjectModel objectModel)
			: base(objectManager, objectModel)
		{
		}

		public override SimpleObject? GetObject(long objectId)
		{
			lock (this.lockObject)
			{
				SimpleObject? simpleObject;

				if (!this.simpleObjectsByObjectId.TryGetValue(objectId, out simpleObject)) // <- IsInChache
				{
					// TODO: Check is this needed since old temp id's (id < 0) are update via ReplaceTempId method
					if (this.newIdsByTempId.TryGetValue(objectId, out long newId)) // try to get object by old temp Id
						this.simpleObjectsByObjectId.TryGetValue(newId, out simpleObject);

					if (simpleObject is null) // Stil nothing => Ask server
					{
						IEnumerable<PropertyIndexValuePair>? propertyIndexValues = this.ObjectManager.RemoteDatastore!.GetObjectPropertyValues(this.ObjectModel.TableInfo.TableId, objectId).GetAwaiter().GetResult();

						if (propertyIndexValues != null)
							simpleObject = this.CreateAndLoadObject(objectId, propertyIndexValues);
						else
							simpleObject = null;
					}
				}

				return simpleObject;
			}
		}

		public override bool ContainsId(long objectId) => this.GetObject(objectId) != null;

		public override List<long> GetOneToManyForeignObjectIds(int primaryTableId, long primaryObjectId, IOneToManyRelationModel oneToManyRelationModel)
		{
			lock (this.lockObject)
			{
				List<long> objectIds = new List<long>();

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

				if (primaryObjectId > 0) // Ask server is any other elements, since client doesn't know how many elements are. If primary ObjectId < 0, it is new object and not exists on server
				{
					IList<long> serverObjectIds = this.ObjectManager.RemoteDatastore!.GetOneToManyForeignObjectCollection(primaryTableId, primaryObjectId, oneToManyRelationModel.RelationKey).GetAwaiter().GetResult();

					foreach (long serverObjectId in serverObjectIds) // Merge with the local object Id's
						if (!objectIds.Contains(serverObjectId))
							objectIds.Add(serverObjectId);
				}

				return objectIds;
			}
		}

		//public override T? FindFirst<T>(Predicate<T> criteria)
		//	where T : class
		//{
		//	lock (this.lockObject)
		//	{
		//		foreach (SimpleObject so in this.simpleObjectsByObjectId.Values)
		//			if (so is T t && criteria(t))
		//				return t;

		//		// TODO: Ask server... but how to send them criteria
		//	}

		//	return default;
		//}


		protected override long CreateNewId() => -this.ObjectManager.ClientTempObjectIdGenerator.CreateKey();

		protected List<long> Select(Predicate<SimpleObject> selector)
		{
			lock (this.lockObject)
			{
				List<long> objectIds = new List<long>();

				for (int i = 0; i < this.simpleObjectsByObjectId.Count; i++)
				{
					SimpleObject item = this.simpleObjectsByObjectId.ElementAt(i).Value;

					if (selector(item))
						objectIds.Add(item.Id);
				}

				return objectIds;
			}

			////
			//// Alternative withot locking, but copying SimpleObjects to an array
			////
			//List<long> objectIds = new List<long>();

			//foreach (var item in this.simpleObjectsByObjectId.Values.ToArray())
			//	if (selector(item)) // The object is deleted or simply not exists, is null or is not custable to T, for any reason
			//		objectIds.Add(item.Id);

			//return objectIds;
		}

		internal protected override bool RemoveObject(long objectId)
		{
			bool isRemoved;

			lock (this.lockObject)
			{
				isRemoved = this.simpleObjectsByObjectId.Remove(objectId);

				if (objectId < 0)
					this.ObjectManager.ClientTempObjectIdGenerator.RemoveKey(-objectId);
			}

			return isRemoved;
		}



		/// <summary>
		/// Replaces the one (temp) object Id with another (new).
		/// Call this method only from Client (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Client).
		/// </summary>
		/// <param name="simpleObject">The <see cref="SimpleObject"/> with an old Id that is about to be changed with a new ones.</param>
		/// <param name="newId">The new object Id.</param>
		internal void ReplaceTempId(SimpleObject simpleObject, long newId)
		{
			lock (this.lockObject)
			{
				long oldId = simpleObject.Id;

				this.simpleObjectsByObjectId.Remove(oldId);
				this.simpleObjectsByObjectId.Add(newId, simpleObject);
				this.newIdsByTempId.Add(oldId, newId);

				simpleObject.SetId(newId);

			}
		}

		internal void ClearTempIds()
		{
			this.newIdsByTempId.Clear();
		}
	}
}
