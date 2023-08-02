using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Collections;
using Simple.Modeling;

namespace Simple.Objects
{
	public class ChangeContainer : IDisposable
	{
		//private OrderedDictionary2<SimpleObject, TransactionRequestAction> transactionRequests = new OrderedDictionary2<SimpleObject, TransactionRequestAction>();
		private OrderedDictionary<SimpleObject, TransactionRequestAction> transactionRequests;
		private ReadOnlyDictionary<SimpleObject, TransactionRequestAction>? readOnlyTransactionRequests = null;
		private Dictionary<SimpleObject, SimpleDictionary<int, SimpleObject?>> changedForeignObjectByRelationKeysBySimpleObject = new Dictionary<SimpleObject, SimpleDictionary<int, SimpleObject?>>();
		//private HashSet<SimpleObject> simpleObjectsRequreCommit = new HashSet<SimpleObject>();
		//private int requireSavingCount = 0;
		//private int requireDeleteCount = 0;
		private int requireCommitCount = 0;
		//private bool requireCommit = false;
		//internal bool commitChangeStarted = false;
		//private bool validationSeeceeded = true;
		//private SimpleObject validationErrorObject = null;
		//private SimpleObjectValidationResult validationResult = null;
		private bool lockChanges = false;
		private readonly object lockObject = new object();

		//TODO: Implement Validation !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

		public ChangeContainer()
		{
			this.transactionRequests = new OrderedDictionary<SimpleObject, TransactionRequestAction>();
			//	this.readOnlyTransactionRequests = this.transactionRequests.AsReadOnly();
			//this.objectManager = objectManager;
			//	if (this.objectManager != null)
			//	{
			//		this.objectManager.NewGraphElementCreated += ObjectManager_GraphElementCreated;
			//		this.objectManager.BeforeDeleting += ObjectManager_BeforeDeleting;
			//		this.objectManager.AfterDelete += ObjectManager_AfterDelete;
			//		this.objectManager.PropertyValueChange += ObjectManager_PropertyValueChange;
			//		this.objectManager.GraphElementParentChange += ObjectManager_GraphElementParentChange;
			//		this.objectManager.ChangedPropertiesCountChange += ObjectManager_ChangedPropertiesCountChange;
			//		this.objectManager.OrderIndexChange += ObjectManager_OrderIndexChange;
			//		this.objectManager.RequireSavingChange += ObjectManager_RequireSavingChange;
			//		this.objectManager.RelationForeignObjectSet += ObjectManager_RelationForeignObjectSet;
			//	}
		}

		public ChangeContainer(IDictionary<SimpleObject, TransactionRequestAction> transactionRequests)
		{
			this.transactionRequests = new OrderedDictionary<SimpleObject, TransactionRequestAction>(transactionRequests);

			//foreach(var item in transactionRequests)
			//{
			//	SimpleObject simpleObject = item.Key;
			//	TransactionRequestAction requestAction = item.Value;

			//	if ((requestAction == TransactionRequestAction.Save && simpleObject.RequireSaving()) || requestAction == TransactionRequestAction.Delete)
			//		simpleObjectsRequreCommit.Add(simpleObject);
			//}
		}

		public event RequestActionSimpleObjectRequesterEventHandler? AfterAdd;
		public event SimpleObjectRequesterEventHandler? AfterRemove;
		public event RequestActionSimpleObjectRequesterEventHandler? AfterSet;
		public event OldCountEventHandler? AfterClear;
		public event CountChangeEventHandler? RequestCountChange;
		public event RequireCommitChangeEventHandler? RequireCommitChange;
		//public event RequireSavingChangeSimpleObject RequireSavingCountChange

		//public SimpleObjectValidationResult ValidationResult 
		//{ 
		//	get => this.validationResult; 
		//	internal set => this.validationResult = value; 
		//}
		//public bool ValidationSeeceeded { get => this.validationSeeceeded; set => this.validationSeeceeded = value; }
		//public SimpleObject ValidationErrorObject { get => this.validationErrorObject; set => this.validationErrorObject = value; }

		//public SimpleObjectManager ObjectManager
		//{
		//	get { return this.objectManager; }
		//}

		public int RequestCount
		{
			get { return this.transactionRequests.Count; }
		}

		//public int RequireSavingCount
		//{
		//	get { return this.requireSavingCount; }
		//}

		//public int RequireDeleteCount
		//{
		//	get { return this.requireDeleteCount; }
		//}

		public bool RequireCommit
		{
			get { return this.requireCommitCount > 0; }
		}

		public ReadOnlyDictionary<SimpleObject, TransactionRequestAction> TransactionRequests
		{
			get
			{
				if (this.readOnlyTransactionRequests == null)
					this.readOnlyTransactionRequests = new ReadOnlyDictionary<SimpleObject, TransactionRequestAction>(this.transactionRequests);

				return this.readOnlyTransactionRequests;
			}
		}

		public bool Contains(SimpleObject simpleObject)
		{
			return this.transactionRequests.ContainsKey(simpleObject);
		}

		public void CancelDeleteRequests() => this.CancelDeleteRequests(requester: null);

		public void CancelDeleteRequests(object? requester)
		{
			if (this.lockChanges)
				throw new AccessViolationException("Attempting to SET change action while ChangeContainter is locked.");

			lock (this.lockObject)
			{
				foreach (var item in this.transactionRequests.ToArray())
				{
					SimpleObject simpleObject = item.Key;
					TransactionRequestAction transactionRequest = item.Value;

					if (transactionRequest == TransactionRequestAction.Delete)
						simpleObject.CancelDeleteRequest(this, requester);
				}
			}
		}

		internal void CancelDeleteRequested(SimpleObject simpleObject, object? requester)
		{
			if (this.lockChanges)
				throw new AccessViolationException("Attempting to CancelDeleteRequest action while ChangeContainter is locked.");

			TransactionRequestAction existingItemAction;

			lock (this.lockObject)
			{
				if (this.transactionRequests.TryGetValue(simpleObject, out existingItemAction))
				{
					if (existingItemAction == TransactionRequestAction.Delete)
						this.RemoveInternal(simpleObject, requester);
				}
			}
		}

		internal void Set(SimpleObject simpleObject, TransactionRequestAction requestAction, object? requester)
		{
			TransactionRequestAction existingItemAction;
			RaiseRequireCommitChangeEventArgs? setResult = null;
			bool callOnAdd = false;
			bool callOnSet = false;

			if (this.lockChanges)
				throw new AccessViolationException("Attempting to SET change action while Change Containter is locked.");

			lock (this.lockObject)
			{
				if (this.transactionRequests.TryGetValue(simpleObject, out existingItemAction))
				{
					if (existingItemAction == TransactionRequestAction.Save && requestAction == TransactionRequestAction.Delete)
					{
						setResult = this.SetInternal(simpleObject, TransactionRequestAction.Delete, requester);
						callOnSet = true;
					}
				}
				else
				{
					setResult = this.AddInternal(simpleObject, requestAction, requester);
					callOnAdd = true;
				}
			}

			if (callOnAdd)
			{
				this.OnAdd(simpleObject, requestAction, setResult.RaiseRequireCommitChangeEvent, requester);
			}
			else if (callOnSet)
			{
				this.OnSet(simpleObject, requestAction, setResult.RaiseRequireCommitChangeEvent, requester);
			}
		}

		internal void Unset(SimpleObject simpleObject, TransactionRequestAction requestAction, object? requester)
		{
			TransactionRequestAction existingItemAction;
			RaiseRequireCommitChangeEventArgs? setResult = null;
			RemoveRaiseRequireCommitChangeEventArgs? removeResult = null;

			if (this.lockChanges)
				throw new AccessViolationException("Attempting to SET change action while Change Containter is locked.");

			lock (this.lockObject)
			{
				if (requestAction == TransactionRequestAction.Save)
				{
					if (this.transactionRequests.TryGetValue(simpleObject, out existingItemAction))
						if (existingItemAction == TransactionRequestAction.Save) // if Delete is existing action object remains
							removeResult = this.RemoveInternal(simpleObject, requester);

				}
				else // requestAction == TransactionRequestAction.Delete
				{
					if (simpleObject.IsChanged)
					{
						setResult = this.SetInternal(simpleObject, TransactionRequestAction.Save, requester);
					}
					else
					{
						removeResult = this.RemoveInternal(simpleObject, requester);
					}
				}
			}

			if (removeResult != null)
			{
				if (removeResult.IsRemoved)
					this.OnRemove(simpleObject, requestAction, removeResult.RaiseRequireCommitChangeEvent, requester);
			}
			else if (setResult != null)
			{
				this.OnSet(simpleObject, requestAction, setResult.RaiseRequireCommitChangeEvent, requester);
			}
		}

		public SimpleObjectValidationResult Validate()
		{
			SimpleObjectValidationResult validationResult = SimpleObjectValidationResult.DefaultSuccessResult;

			lock (this.lockObject)
			{
				foreach (var item in this.TransactionRequests.ToArray())
				{
					SimpleObject simpleObject = item.Key;
					TransactionRequestAction transactionAction = item.Value;

					if (transactionAction == TransactionRequestAction.Save)
					{
						validationResult = simpleObject.Manager.ValidateSave(simpleObject, this.TransactionRequests);

						if (!validationResult.Passed)
							break;
					}
					else // if (transactionAction == TransactionRequestAction.Delete)
					{
						validationResult = simpleObject.Manager.ValidateDelete(simpleObject, this.TransactionRequests);

						if (!validationResult.Passed)
							break;
					}
				}

				return validationResult;
			}
		}

		public void Sort()
		{
			lock (this.lockObject)
			{
				KeyValuePair<SimpleObject, TransactionRequestAction>[] itemList = null;
				bool somthingIsAdded;

				OrderedDictionary<SimpleObject, TransactionRequestAction> insertTransactionRequests = new OrderedDictionary<SimpleObject, TransactionRequestAction>();
				//OrderedDictionary<SimpleObject, TransactionRequestAction> newInsertTransactionRequests = new OrderedDictionary<SimpleObject, TransactionRequestAction>();
				OrderedDictionary<SimpleObject, TransactionRequestAction> updateTransactionRequests = new OrderedDictionary<SimpleObject, TransactionRequestAction>();
				OrderedDictionary<SimpleObject, TransactionRequestAction> deleteTransactionRequests = new OrderedDictionary<SimpleObject, TransactionRequestAction>();
				//OrderedDictionary<SimpleObject, TransactionRequestAction> newDeleteTransactionRequests = new OrderedDictionary<SimpleObject, TransactionRequestAction>();
				//Dictionary<SimpleObject, TransactionRequestAction> additionalExtraUpdateTransactionRequests = new Dictionary<SimpleObject, TransactionRequestAction>();

				// First Insert objects (requestAction == TransactionRequestAction.Save && SimpleObject.IsNew == true)
				foreach (var item in this.transactionRequests)
				{
					if (item.Value == TransactionRequestAction.Save)
					{
						if (item.Key.IsNew)
						{
							insertTransactionRequests.Add(item.Key, item.Value);
						}
						else
						{
							updateTransactionRequests.Add(item.Key, item.Value);
						}
					}
					else
					{
						deleteTransactionRequests.Add(item.Key, item.Value);
					}
				}

				// Sort Insert -> first must be elements without any related foreign object to new objects below list that is not yet inserted into database

				itemList = insertTransactionRequests.ToArray();
				insertTransactionRequests.Clear();
				
				do
				{
					somthingIsAdded = false;

					for (int i = 0; i < itemList.Count(); i++)
					{
						var item = itemList.ElementAt(i);

						if (item.Equals(default(KeyValuePair<SimpleObject, TransactionRequestAction>)))
							continue;

						SimpleObject simpleObject = item.Key;
						TransactionRequestAction requestAction = item.Value;

						if (this.HasNoAnyRelatedForeignObject(itemList, i, simpleObject.GetChangedPrimaryObjects()))
						{
							insertTransactionRequests.Add(simpleObject, requestAction);
							itemList.SetValue(default(KeyValuePair<SimpleObject, TransactionRequestAction>), i); // element is removed -> do not check it any more
							somthingIsAdded = true;
						}
					}
				}
				while (somthingIsAdded);

				foreach (var item in itemList)
					if (!item.Equals(default(KeyValuePair<SimpleObject, TransactionRequestAction>)))
						insertTransactionRequests.Add(item.Key, item.Value);

				insertTransactionRequests.SortKeys((s1, s2) => (s1 is GroupMembership).CompareTo(s2 is GroupMembership)); // Insert many to many at last
				// Update : No soting needed since all elements are saved in database

				//Dictionary<SimpleObject, TransactionRequestAction> newUpdateTransactionRequests = new Dictionary<SimpleObject, TransactionRequestAction>();
				//Dictionary<SimpleObject, TransactionRequestAction> additionalUpdateTransactionRequests = new Dictionary<SimpleObject, TransactionRequestAction>();

				itemList = updateTransactionRequests.ToArray();
				updateTransactionRequests.Clear();

				do
				{
					somthingIsAdded = false;

					for (int i = 0; i < itemList.Count(); i++)
					{
						var item = itemList.ElementAt(i);

						if (item.Equals(default(KeyValuePair<SimpleObject, TransactionRequestAction>)))
							continue;
						
						SimpleObject simpleObject = item.Key;
						TransactionRequestAction requestAction = item.Value;

						if (this.HasNoAnyRelatedForeignObject(itemList, i, simpleObject.GetChangedPrimaryObjects()))
						{
							updateTransactionRequests.Add(simpleObject, requestAction);
							itemList.SetValue(default(KeyValuePair<SimpleObject, TransactionRequestAction>), i);
							somthingIsAdded = true;
						}
					}
				}
				while (somthingIsAdded);

				foreach (var item in itemList)
					if (!item.Equals(default(KeyValuePair<SimpleObject, TransactionRequestAction>)))
						updateTransactionRequests.Add(item.Key, item.Value);

				// Delete Sort: first must be elements that is not part of any relation foreign object of the objects below that will be deleted after
				itemList = deleteTransactionRequests.ToArray();
				deleteTransactionRequests.Clear();

				do
				{
					somthingIsAdded = false;

					for (int i = 0; i < itemList.Count(); i++)
					{
						var item = itemList.ElementAt(i);

						if (item.Equals(default(KeyValuePair<SimpleObject, TransactionRequestAction>)))
							continue;

						SimpleObject simpleObject = item.Key;
						TransactionRequestAction requestAction = item.Value;

						if (this.HasNoAnyRelatedForeignObject(itemList, i, simpleObject.GetAllForeignObjects()))
						{
							deleteTransactionRequests.Add(simpleObject, requestAction);
							itemList.SetValue(default(KeyValuePair<SimpleObject, TransactionRequestAction>), i); // element is removed -> do not check it any more
							somthingIsAdded = true;
						}
					}
				}
				while (somthingIsAdded);

				foreach (var item in itemList)
					if (!item.Equals(default(KeyValuePair<SimpleObject, TransactionRequestAction>)))
						deleteTransactionRequests.Add(item.Key, item.Value);

				deleteTransactionRequests.SortKeys((s1, s2) => (s1 is GroupMembership).CompareTo(s2 is GroupMembership)); // ManyToManyElement needs to be deleted first

				//deleteTransactionRequests.Reverse(); // Oposite as insert => objects with foreign keys at the begining


				// Merge Insert, Update and Delete transaction requests
				this.transactionRequests = new OrderedDictionary<SimpleObject, TransactionRequestAction>();

				foreach (var item in insertTransactionRequests)
					this.transactionRequests.Add(item.Key, item.Value);

				foreach (var item in updateTransactionRequests)
					this.transactionRequests.Add(item.Key, item.Value);

				foreach (var item in deleteTransactionRequests.Reverse()) // Oposite as insert => objects with foreign keys at the begining
					this.transactionRequests.Add(item.Key, item.Value);

				this.readOnlyTransactionRequests = null;
			}
		}

		internal void LockChanges() { this.lockChanges = true; }
		internal void UnlockChanges() { this.lockChanges = false; }

		private bool HasNoAnyRelatedForeignObject(ICollection<KeyValuePair<SimpleObject, TransactionRequestAction>> simpleObjectRequestActions, int simpleObjectIndex, IEnumerable<SimpleObject> simpleObjectForeignObjects)
		{
			if (simpleObjectForeignObjects.Count() == 0)
				return true;

			for (int i = 0; i < simpleObjectRequestActions.Count; i++)
			{
				if (i == simpleObjectIndex)
					continue;

				var item = simpleObjectRequestActions.ElementAt(i);

				if (item.Equals(default(KeyValuePair<SimpleObject, TransactionRequestAction>)))
					continue;

				SimpleObject element = simpleObjectRequestActions.ElementAt(i).Key;

				if (simpleObjectForeignObjects.Contains(element))
					return false;
			}

			return true;
		}

		//private bool HasNoAnyRelatedObjects(SimpleObject simpleObject, ICollection<KeyValuePair<SimpleObject, TransactionRequestAction>> simpleObjectRequestActions)
		//{
		//	var foregnObjects = simpleObject.GetChangedForeignObjects();

		//	if (foregnObjects.Count() == 0)
		//		return false;

		//	for (int i = 0; i < simpleObjectRequestActions.Count - 1; i++)
		//	{
		//		if (i == simpleObjectIndex)
		//			continue;

		//		SimpleObject element = simpleObjectRequestActions.ElementAt(i).Key;

		//		if (foregnObjects.Contains(element))
		//			return true;
		//	}

		//	return false;
		//}

		internal void RelationForeignObjectIsSet(SimpleObject simpleObject, SimpleObject? foreignSimpleObject, IOneToOneOrManyRelationModel objectRelationModel)
		{
			lock (this.lockObject)
			{
				SimpleDictionary<int, SimpleObject?> changedForeignObjectByRelationKeys;

				if (!this.changedForeignObjectByRelationKeysBySimpleObject.TryGetValue(simpleObject, out changedForeignObjectByRelationKeys))
					this.changedForeignObjectByRelationKeysBySimpleObject.Add(simpleObject, changedForeignObjectByRelationKeys = new SimpleDictionary<int, SimpleObject?>());

				changedForeignObjectByRelationKeys[objectRelationModel.RelationKey] = foreignSimpleObject;
			}
		}


		internal void Clear()
		{
			lock (this.lockObject)
			{
				int oldCount = this.RequestCount;
				bool oldRequireCommit = this.RequireCommit;

				this.transactionRequests.Clear();
				this.changedForeignObjectByRelationKeysBySimpleObject.Clear();
				this.requireCommitCount = 0;
				this.RaiseAfterClear(oldCount);
				
				if (oldCount > 0)
					this.RaiseCountChange(oldCount);

				if (oldRequireCommit)
					this.RaiseRequireCommitChange(this.RequireCommit);

				this.lockChanges = false;
			}
		}

		//internal void RequireSavingChangeCount(SimpleObject simpleObject, bool requireSaving)
		//{
		//	TransactionRequestAction requestAction;

		//	if (this.transactionRequests.TryGetValue(simpleObject, out requestAction))
		//	{
		//		if (requestAction == TransactionRequestAction.Save)
		//		{
		//			if (requireSaving)
		//			{

		//			}
		//		}
		//	}
			
		//	if (requireSaving)
		//	{
		//		this.requireSavingCount++;

		//		if (this.requireSavingCount == 1 && this.requireDeleteCount == 0)
		//			this.SetRequireCommit(true);
		//	}
		//	else
		//	{
		//		this.requireSavingCount--;

		//		if (this.requireSavingCount == 0 && this.requireDeleteCount == 0)
		//			this.SetRequireCommit(false);
		//	}

		//	if (this.requireSavingCount + this.requireDeleteCount > this.Count)
		//	{
		//		throw new Exception("Check RequireSavingChangeCount or RequireDeleteChangeCount method");
		//	}
		//}

		//private void RequireDeleteChangeCount(bool requireDelete)
		//{
		//	if (requireDelete)
		//	{
		//		this.requireDeleteCount++;

		//		if (this.requireDeleteCount == 1 && this.RequireSavingCount == 0)
		//			this.SetRequireCommit(true);
		//	}
		//	else
		//	{
		//		this.requireDeleteCount--;

		//		if (this.requireDeleteCount == 0 && this.RequireSavingCount == 0)
		//			this.SetRequireCommit(false);
		//	}

		//	if (this.requireSavingCount + this.requireDeleteCount > this.Count)
		//	{
		//		throw new Exception("Check RequireSavingChangeCount or RequireDeleteChangeCount method");
		//	}
		//}


		private RaiseRequireCommitChangeEventArgs AddInternal(SimpleObject simpleObject, TransactionRequestAction requestAction, object? requester)
		{
			bool raiseRequireCommitChangeEvent = false;
			
			this.transactionRequests.Add(simpleObject, requestAction);
			this.SetChangedForeignObjectsDictionary(simpleObject);

			if ((requestAction == TransactionRequestAction.Save && simpleObject.RequireSaving()) || requestAction == TransactionRequestAction.Delete)
				if (++this.requireCommitCount == 1)
					raiseRequireCommitChangeEvent = true;

			return new RaiseRequireCommitChangeEventArgs(raiseRequireCommitChangeEvent);
		}

		private void OnAdd(SimpleObject simpleObject, TransactionRequestAction requestAction, bool raiseRequireCommitChangeEvent, object? requester)
		{
			this.RaiseAfterAdd(simpleObject, requestAction, requester);
			this.RaiseCountChange(this.RequestCount - 1);

			if (raiseRequireCommitChangeEvent)
				this.RaiseRequireCommitChange(this.RequireCommit);
		}

		private RaiseRequireCommitChangeEventArgs SetInternal(SimpleObject simpleObject, TransactionRequestAction requestAction, object? requester)
		{
			bool raiseRequireCommitChangeEvent = false;

			this.transactionRequests[simpleObject] = requestAction;

			if (simpleObject.RequireSaving() && requestAction == TransactionRequestAction.Delete)
				if (++this.requireCommitCount == 1)
					raiseRequireCommitChangeEvent = true;

			return new RaiseRequireCommitChangeEventArgs(raiseRequireCommitChangeEvent);
		}

		private void OnSet(SimpleObject simpleObject, TransactionRequestAction requestAction, bool raiseRequireCommitChangeEvent, object? requester)
		{
			this.RaiseAfterSet(simpleObject, requestAction, requester);

			if (raiseRequireCommitChangeEvent)
				this.RaiseRequireCommitChange(this.RequireCommit);
		}

		private RemoveRaiseRequireCommitChangeEventArgs RemoveInternal(SimpleObject simpleObject, object? requester)
		{
			TransactionRequestAction requestAction;
			bool raiseRequireCommitChangeEvent = false;
			bool isRemoved = false;

			if (this.transactionRequests.TryGetValue(simpleObject, out requestAction))
			{
				this.transactionRequests.Remove(simpleObject);
				this.changedForeignObjectByRelationKeysBySimpleObject.Remove(simpleObject);
				isRemoved = true;

				if ((requestAction == TransactionRequestAction.Save && !simpleObject.RequireSaving()) || requestAction == TransactionRequestAction.Delete)
					if (--this.requireCommitCount == 0)
						raiseRequireCommitChangeEvent = true;
			}

			return new RemoveRaiseRequireCommitChangeEventArgs(isRemoved, raiseRequireCommitChangeEvent);
		}

		private void OnRemove(SimpleObject simpleObject, TransactionRequestAction requestAction, bool raiseRequireCommitChangeEvent, object? requester)
		{
			this.RaiseAfterRemove(simpleObject, requestAction, requester);
			this.RaiseCountChange(this.RequestCount + 1);

			if (raiseRequireCommitChangeEvent)
				this.RaiseRequireCommitChange(this.RequireCommit);
		}

		//private void SetRequireCommit(bool requireCommit)
		//{
		//	if (this.requireCommit != requireCommit)
		//	{
		//		this.requireCommit = requireCommit;
		//		this.RaiseRequireCommitChange(requireCommit);
		//	}
		//}

		private void SetChangedForeignObjectsDictionary(SimpleObject simpleObject)
		{
			lock (this.lockObject)
			{
				SimpleDictionary<int, SimpleObject>? changedForeignObjectByRelationKeys;

				if (!this.changedForeignObjectByRelationKeysBySimpleObject.TryGetValue(simpleObject, out changedForeignObjectByRelationKeys))
					this.changedForeignObjectByRelationKeysBySimpleObject.Add(simpleObject, changedForeignObjectByRelationKeys = new SimpleDictionary<int, SimpleObject>());

				if (changedForeignObjectByRelationKeys != null)
				{
					foreach (int relationKey in simpleObject.GetChangedPrimaryObjectRelationKeys())
					{
						SimpleObject foreignObject = simpleObject.GetRelationPrimaryObject(relationKey);

						changedForeignObjectByRelationKeys[relationKey] = foreignObject;
					}
				}
			}
		}

		private void RaiseAfterAdd(SimpleObject simpleObject, TransactionRequestAction requestAction, object? requester)
		{
			this.AfterAdd?.Invoke(this, new RequestActionSimpleObjectRequesterEventArgs(simpleObject, requestAction, requester));
		}

		private void RaiseAfterSet(SimpleObject simpleObject, TransactionRequestAction requestAction, object? requester)
		{
			this.AfterSet?.Invoke(this, new RequestActionSimpleObjectRequesterEventArgs(simpleObject, requestAction, requester));
		}

		private void RaiseAfterRemove(SimpleObject simpleObject, TransactionRequestAction requestAction, object? requester)
		{
			this.AfterRemove?.Invoke(this, new RequestActionSimpleObjectRequesterEventArgs(simpleObject, requestAction, requester));
		}

		private void RaiseAfterClear(int oldCount)
		{
			this.AfterClear?.Invoke(this, new OldCountEventArgs(oldCount));
		}

		private void RaiseCountChange(int oldCount)
		{
			this.RequestCountChange?.Invoke(this, new CountChangeEventArgs(this.transactionRequests.Count, oldCount));
		}

		private void RaiseRequireCommitChange(bool requireCommit)
		{
			this.RequireCommitChange?.Invoke(this, new RequireCommitChangeEventArgs(requireCommit));
		}

		public void Dispose()
		{
			this.transactionRequests.Clear();
			this.readOnlyTransactionRequests = null;
			this.changedForeignObjectByRelationKeysBySimpleObject.Clear();
		}

	//private void ObjectManager_RelationForeignObjectSet(object sender, RelationForeignObjectSetRequesterEventArgs e)
	//{
	//	throw new NotImplementedException();
	//}

	//private void ObjectManager_RequireSavingChange(object sender, RequireSavingChangeSimpleObjectEventArgs e)
	//{
	//	throw new NotImplementedException();
	//}

	//private void ObjectManager_OrderIndexChange(object sender, ChangeSortedSimpleObjectRequesterEventArgs e)
	//{
	//	throw new NotImplementedException();
	//}

	//private void ObjectManager_ChangedPropertiesCountChange(object sender, CountChangeSimpleObjectEventArgs e)
	//{
	//	throw new NotImplementedException();
	//}

	//private void ObjectManager_GraphElementParentChange(object sender, ChangeParentGraphElementRequesterEventArgs e)
	//{
	//	throw new NotImplementedException();
	//}

	//private void ObjectManager_PropertyValueChange(object sender, ChangePropertyValueSimpleObjectRequesterEventArgs e)
	//{
	//	throw new NotImplementedException();
	//}

	//private void ObjectManager_AfterDelete(object sender, SimpleObjectRequesterEventArgs e)
	//{
	//	throw new NotImplementedException();
	//}

	//private void ObjectManager_BeforeDeleting(object sender, SimpleObjectRequesterEventArgs e)
	//{
	//	throw new NotImplementedException();
	//}

	//private void ObjectManager_GraphElementCreated(object sender, GraphElementRequesterEventArgs e)
	//{
	//	throw new NotImplementedException();
	//}

	private class RaiseRequireCommitChangeEventArgs
		{
			public RaiseRequireCommitChangeEventArgs(bool raiseRequireCommitChangeEvent)
			{
				this.RaiseRequireCommitChangeEvent = raiseRequireCommitChangeEvent;
			}

			public bool RaiseRequireCommitChangeEvent { get; private set; }
		}

		private class RemoveRaiseRequireCommitChangeEventArgs : RaiseRequireCommitChangeEventArgs
		{
			public RemoveRaiseRequireCommitChangeEventArgs(bool isRemoved, bool raiseRequireCommitChangeEvent)
				: base(raiseRequireCommitChangeEvent)
			{
				this.IsRemoved = isRemoved;
			}

			public bool IsRemoved { get; private set; }
		}
	}

	public delegate void RequestActionSimpleObjectRequesterEventHandler(object sender, RequestActionSimpleObjectRequesterEventArgs e);

	public class RequestActionSimpleObjectRequesterEventArgs : SimpleObjectRequesterEventArgs
	{
		public RequestActionSimpleObjectRequesterEventArgs(SimpleObject simpleObject, TransactionRequestAction requestAction, object? requester)
			: base(simpleObject, requester)
		{
			this.RequestAction = requestAction;
		}

		public TransactionRequestAction RequestAction { get; private set; }
	}

	//public class ChangeItemInfo
	//{
	//	public ChangeItemInfo(SimpleObject SimpleObject, TransactionRequestAction requestAction)
	//	{
	//		this.sim
	//	}

	//	public SimpleObject SimpleObject { get; private set; }
	//	public TransactionRequestAction RequestAction { get; private set; }
	//	public Dictionary<int, SimpleObject> ForeignObjectsByRelationKey { get; private set; }
	//}
}
