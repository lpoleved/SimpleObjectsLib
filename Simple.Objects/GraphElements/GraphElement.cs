using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Simple;
using Simple.Modeling;
using Simple.Collections;

namespace Simple.Objects
{
	public partial class GraphElement : SortableSimpleObject
	{
		private bool isAnchor = false;
		private const string defaultPathNameSplitter = "\\";
		private const string defaultReversePathNameSplitter = "/";
		private bool canRaiseParentChangeEvent = true;
		private bool canRiseNewGraphElementCreatedEvent = false;
		private bool isNewGraphElementCreatedEventRaised = false;
		private bool isChildrenLoadedInClientMode = false;
		private bool hasChildrenInClientModeWhenNotLoaded = false;
		private bool isHasChildrenInClientModeWhenNotLoadedSet = false;

		public GraphElement()
		{
		}

		public GraphElement(SimpleObjectManager objectManager)
			: base(objectManager)
		{
		}

		public GraphElement(SimpleObjectManager objectManager, int graphKey, SimpleObject simpleObject, GraphElement? parent, ObjectActionContext context, object? requester = null)
			: this(objectManager, graphKey, simpleObject, parent, isAnchor: false, context, requester)
		{
		}

		public GraphElement(SimpleObjectManager objectManager, int graphKey, SimpleObject simpleObject, GraphElement? parent, bool isAnchor, ObjectActionContext context, object? requester = null)
			: this(objectManager, graphKey, simpleObject, parent, isAnchor, objectManager.DefaultChangeContainer, context, requester)
		{
		}

		public GraphElement(SimpleObjectManager objectManager, int graphKey, SimpleObject simpleObject, GraphElement? parent, ChangeContainer? changeContainer, ObjectActionContext context, object? requester = null)
			: this(objectManager, graphKey, simpleObject, parent, isAnchor: false, changeContainer, context, requester)
		{
		}

		public GraphElement(SimpleObjectManager objectManager, int graphKey, SimpleObject simpleObject, GraphElement? parent, bool isAnchor, ChangeContainer? changeContainer, ObjectActionContext context, object? requester = null)
			: base(objectManager, changeContainer, context, requester)
		{
			this.canRaiseParentChangeEvent = false;
			this.canRiseNewGraphElementCreatedEvent = false;
			
			this.IsAnchor = isAnchor;
			this.Requester = requester;
			this.IsStorable = simpleObject.IsStorable;
			this.GraphKey = graphKey;
			this.Parent = parent;
			_ = this.Id; // Enforce new Id creation, if needed
			this.SimpleObject = simpleObject;
			
			this.canRaiseParentChangeEvent = true;
			this.canRiseNewGraphElementCreatedEvent = true;
			this.Manager.NewGraphElementIsCreated(this, changeContainer, context, requester);
			
			if (parent != null)
				this.Manager.GraphElementParentIsChanged(this, oldParent: null, changeContainer, context, requester);
		}

		protected override SimpleObjectCollection GetSortingCollection()
		{
			return this.GetNeighbours();
		}

		internal SystemGraph Graph
		{
			get { return this.Manager.GetSystemGraph(this.GraphKey); }
		}

		internal bool CanRiseNewGraphElementCreatedEvent
		{
			get => this.canRiseNewGraphElementCreatedEvent;
			set => this.canRiseNewGraphElementCreatedEvent = value;
		}

		internal bool IsNewGraphElementCreatedRised
		{
			get => this.isNewGraphElementCreatedEventRaised;
			set => this.isNewGraphElementCreatedEventRaised = value;
		}

		internal bool CanRaiseParentChangeEvent
		{
			get => this.canRaiseParentChangeEvent;
			set => this.canRaiseParentChangeEvent = value;
		}


		internal bool IsChildrenLoadedInClientMode 
		{ 
			get => this.isChildrenLoadedInClientMode; 
			set => this.isChildrenLoadedInClientMode = value; 
		}
		
		internal bool HasChildrenInClientModeWhenNotLoaded 
		{ 
			get => this.hasChildrenInClientModeWhenNotLoaded;
			set
			{
				this.hasChildrenInClientModeWhenNotLoaded = value;
				this.isHasChildrenInClientModeWhenNotLoadedSet = true;
			}
		}

		/// <summary>
		/// Determine if a node has any child node.
		/// </summary>
		public bool HasChildren
		{
			get 
			{
				if (this.Manager.WorkingMode == ObjectManagerWorkingMode.Client && !this.IsChildrenLoadedInClientMode && this.isHasChildrenInClientModeWhenNotLoadedSet) // <- remove this
					return this.HasChildrenInClientModeWhenNotLoaded;
				else
					return this.GraphElements.Count > 0; 
			}
		}

		public bool IsAnchor { get => this.isAnchor; set => this.isAnchor = value; }

		public int GetTreeDepth()
		{
			int result = 0;
			GraphElement? parent = this.Parent;

			while (parent != null)
			{
				result++;
				parent = parent.Parent;
			}

			return result;
		}

		/// <summary>
		/// Determines whether the current GraphElement has a <see cref="Simple.Objects.GraphElement"/> instance as a parent.
		/// </summary>
		/// <param name="graphElement">The <see cref="Simple.Objects.GraphElement"/> instance to check.</param>
		/// <returns>true if the current node has a <see cref="Simple.Objects.GraphElement"/> instance as a parent; otherwise, false.</returns>
		public bool HasAsParent(GraphElement graphElement)
		{
			GraphElement? parent = this.Parent;

			while (parent != null)
			{
				if (parent == graphElement)
					return true;

				parent = parent.Parent;
			}

			return false;
		}

		public GraphElement GetRoot()
		{
			GraphElement result = this;

			while (result.Parent != null)
				result = result.Parent;

			return result;
		}

		/// <summary>
		/// Geth the parent GraphElement neighbors
		/// </summary>
		/// <returns></returns>
		public SimpleObjectCollection<GraphElement> GetNeighbours() => this.Parent?.GraphElements ?? this.Graph.RootGraphElements;

		public string GetPathName()
		{
			return this.GetPathName(defaultPathNameSplitter);
		}

		public string GetPathName(string splitter)
		{
			string value = String.Empty;
			GraphElement? graphElement = this;
			bool isFirst = true;
			
			while (graphElement != null)
			{
				if (isFirst)
				{
					isFirst = false;
				}
				else if (graphElement.SimpleObject != null)
				{
					if (value == String.Empty)
					{
						string? name = graphElement.SimpleObject.GetName();

						if (name != null)
							value = name;
					}
					else
					{
						value = graphElement.SimpleObject.GetName() + splitter + value;
					}
				}

				graphElement = graphElement.Parent;
			}

			return value;
		}

		public string GetFullPathName()
		{
			return this.GetFullPathName(defaultPathNameSplitter); ;
		}

		public string GetFullPathName(string splitter)
		{
			string pathName = this.GetPathName(splitter);
			string name = this.SimpleObject?.GetName() ?? String.Empty;

			return pathName.Length > 0 ? pathName + splitter + name : name;
		}

		public string GetReversePathName()
		{
			return this.GetReversePathName(defaultReversePathNameSplitter);
		}

		public string GetReversePathName(string splitter)
		{
			string value = String.Empty;
			GraphElement? graphElement = this;
			bool isFirst = true;

			while (graphElement != null)
			{
				if (isFirst)
				{
					isFirst = false;
				}
				else if (graphElement.SimpleObject != null)
				{
					if (value == String.Empty)
					{
						string? name = graphElement.SimpleObject.GetName();

						if (name != null)
							value = name;
					}
					else
					{
						value += splitter + graphElement.SimpleObject.GetName();
					}
				}

				graphElement = graphElement.Parent;
			}

			return value;
		}

		public string GetFullReversePathName()
		{
			return this.GetFullReversePathName(defaultReversePathNameSplitter);
		}

		public string GetFullReversePathName(string splitter)
		{
			string reversePathName = this.GetReversePathName(splitter);
			string name = this.SimpleObject?.GetName() ?? String.Empty;

			return reversePathName.Length > 0 ? name + splitter + reversePathName : name;
		}

		public SimpleObjectCollection<T> GetChildGraphElementSimpleObjectCollection<T>() where T : SimpleObject
		{
			SimpleObjectModel? model = this.Manager.GetObjectModel(typeof(T));

			if (model == null)
				return new SimpleObjectCollection<T>(this.Manager, -1, new List<long>(0));

			int tableId = model.TableInfo.TableId;
			List<long> objectIds = new List<long>(this.GraphElements.Count);
			SimpleObjectCollection<T> result;

			foreach (GraphElement graphElement in this.GraphElements)
				if (graphElement.SimpleObject != null)
					objectIds.Add(graphElement.SimpleObject.Id);

			result = new SimpleObjectCollection<T>(this.Manager, tableId, objectIds);

			return result;
		}

		public override string GetName()
		{
			return String.Format("{0} (GE)", (this.SimpleObject != null) ? this.SimpleObject.GetName() ?? "Empty" : "Missing Simple.Object");
		}

		public override string GetDescription()
		{
			return String.Format("{0} (GE)", (this.SimpleObject != null) ? this.SimpleObject.GetDescription() ?? "Empty" : "Missing Simple.Object");
		}

		public override string? ToString() => this.GetName();

		//public static bool operator ==(GraphElement? a, GraphElement? b)
		//{
		//	// If both are null, or both are same instance, return true.
		//	if (System.Object.ReferenceEquals(a, b))
		//		return true;

		//	// If one is null, but not both, return false.
		//	if (a is null ^ b is null)
		//		return false;

		//	if (a is null || b is null)
		//		return false;

		//	// Return true if the key fields match.
		//	if (a.Id == b.Id && a.GetModel().TableInfo.TableId == b.GetModel().TableInfo.TableId)
		//		return a.IsDeleted == b.IsDeleted;

		//	return false;
		//}

		//public static bool operator !=(GraphElement? a, GraphElement? b) => !(a == b);

		protected override void OnBeforeRelationPrimaryObjectSet(SimpleObject? primarySimpleObject, SimpleObject? oldPrimarySimpleObject, IOneToOneOrManyRelationModel objectRelationPolicyModel, ref bool cancel, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			base.OnBeforeRelationPrimaryObjectSet(primarySimpleObject, oldPrimarySimpleObject, objectRelationPolicyModel, ref cancel, changeContainer, context, requester);

			if (this.DeleteStarted)
				return;

			// Prevent changing SimpleObject when object is stored or saved.
			if (objectRelationPolicyModel.RelationType == ObjectRelationType.OneToMany)
			{
				if (objectRelationPolicyModel.RelationKey == RelationPolicyModelBase.OneToManyGraphElementToSimpleObject.RelationKey)
				{
					if (!this.IsNew && !this.DeleteStarted && !this.DeleteRequested)
					{
						cancel = true;
						throw new ArgumentException("You can set GraphElement's SimpleObject property only when IsNew = true.");
					}
				}
				else if (objectRelationPolicyModel.RelationKey == RelationPolicyModelBase.OneToManyGraphElementToParentGraphElement.RelationKey)
				{
					if (requester != null) // requester == GraphElement.Empty handles SetParent method
					{
						GraphElement? newParent = primarySimpleObject as GraphElement;

						this.Manager.BeforeGraphElementParentIsChanged(this, newParent, ref cancel, changeContainer, context, requester);
					}
				}
			}
		}

		protected override void OnRelationForeignObjectSet(SimpleObject? foreignSimpleObject, SimpleObject? oldForeignSimpleObject, IOneToOneOrManyRelationModel objectRelationPolicyModel, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			base.OnRelationForeignObjectSet(foreignSimpleObject, oldForeignSimpleObject, objectRelationPolicyModel, changeContainer, context, requester);

			if (this.DeleteStarted)
				return;

			if (foreignSimpleObject != oldForeignSimpleObject && objectRelationPolicyModel.RelationKey == RelationPolicyModelBase.OneToManyGraphElementToParentGraphElement.RelationKey) // && !this.CanRaiseNewGraphElementCreated) // if canRaiseNewGraphElementCreated is false does this event is not rised and we can call and rise parent chane event
				this.Manager.GraphElementParentIsChanged(this, oldForeignSimpleObject as GraphElement, changeContainer, context, requester);
		}

		protected override void OnAfterLoad()
		{
			base.OnAfterLoad();

			this.CanRiseNewGraphElementCreatedEvent = false; // Prevent rising NewGraphElementCreated event after Load object properties
		}

		protected override SimpleObjectCollection? GetOneToManyForeignNullCollection(int oneToManyRelationKey)
		{
			if (oneToManyRelationKey == RelationPolicyModelBase.OneToManyGraphElementToParentGraphElement.RelationKey)
				return this.Graph.RootGraphElements; //  rootSortingGraphElementsCollection;

			return base.GetOneToManyForeignNullCollection(oneToManyRelationKey);
		}

		protected override void OnOrderIndexChange(int orderIndex, int oldOrderIndex, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			base.OnOrderIndexChange(orderIndex, oldOrderIndex, changeContainer, context, requester);
			
			this.SimpleObject?.GraphElementOrderIndexIsChanged(this, orderIndex, oldOrderIndex, changeContainer, context, requester);
		}

		internal void SetParentWithoutCheckingCanGraphElementChangeParentOrRaisingEvents(GraphElement? parent, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
		{
			//this.SetParent(parent, checkCanGraphElementChangeParentMode: false, addOrRemoveInChangedPropertyNames: false, firePropertyValueChangeEvent: false, raiseForeignObjectSetEvent: false, changeContainer, context, requester); //, setSorting: false);
			this.SetParent(parent, checkCanGraphElementChangeParentMode: false, raiseForeignObjectSetEvent: false, changeContainer, context, requester); //, setSorting: false);
		}

		//
		// TODO: Check why is this needed!
		//		 THIS IS NEEDED ONLY ON CLIENT SIDE
		//
		internal void SetClientGraphElementCollectionInternal(SimpleObjectCollection<GraphElement> graphElements)
		{
			// TODO: Check if this sorting is needed?
			if (!this.OneToManyForeignCollectionsByRelationKey.ContainsKey(RelationPolicyModelBase.OneToManyGraphElementToParentGraphElement.RelationKey))
			{
				//TODO: Check if sorting is needed
				graphElements.Sort(setPrevious: true, this.ChangeContainer, this.Requester); //  GraphElementModel

				//if (graphElements.Count > 0 && graphElements[0].SimpleObject.GetModel().SortingModel == SortingModel.ByOneToManyRelationKey)
				//	this.SetPreviousIdAndNextIdOnGraphElementSimpleObjectsAsIs(graphElements);

				this.OneToManyForeignCollectionsByRelationKey.Add(RelationPolicyModelBase.OneToManyGraphElementToParentGraphElement.RelationKey, graphElements);
			}
		}

		internal void MergeClientGraphElementCollectionInternal(IEnumerable<long> graphElementObjectIds)
		{
			int relationKey = RelationPolicyModelBase.OneToManyGraphElementToParentGraphElement.RelationKey;

			// TODO: Check if this sorting is needed?
			if (this.OneToManyForeignCollectionsByRelationKey.ContainsKey(relationKey))
			{
				var current = this.OneToManyForeignCollectionsByRelationKey[relationKey];
				List<long> newObjectIds = new List<long>(current.GetObjectIds().Union(graphElementObjectIds));
				var result = new SimpleObjectCollection(this.Manager, GraphElementModel.TableId, newObjectIds);

				this.OneToManyForeignCollectionsByRelationKey[relationKey] = result;

				//TODO: Check if sorting is needed
				result.Sort(setPrevious: true, this.ChangeContainer, this.Requester); //  GraphElementModel
			}
		}

		private void SetPreviousIdAndNextIdOnGraphElementSimpleObjectsAsIs(SimpleObjectCollection<GraphElement> graphElements)
		{
			SortableSimpleObject? previousSimpleObject = null;
			int orderIndex = 0;

			foreach (GraphElement graphElement in graphElements)
			{
				if (graphElement.SimpleObject is SortableSimpleObject sortableSimpleObject)
				{
					sortableSimpleObject.SetPropertyValueInternal(graphElement.SimpleObject.GetModel().PreviousIdPropertyModel, previousSimpleObject?.Id ?? 0, changeContainer: null, requester: this);
					sortableSimpleObject.NextId = 0;
					sortableSimpleObject.SetOrderIndexInternal(orderIndex++);

					if (previousSimpleObject != null)
						previousSimpleObject.NextId = sortableSimpleObject.Id;

					previousSimpleObject = sortableSimpleObject;
				}
			}
		}

		internal void Load(long previousId, long parentId, int graphKey, int objectTableId, long objectId, int orderIndex)
		{
			this.previousId = this.oldPreviousId = previousId;
			this.parentId = this.oldParentId = parentId;
			this.graphKey = this.oldGraphKey = graphKey;
			this.objectTableId = this.oldObjectTableId = objectTableId;
			this.objectId = this.oldObjectId = objectId;
			this.orderIndex = this.oldOrderIndex = orderIndex;
		}

		/// <summary>
		/// Manualy handling of setting Parent relation foreign object with customized behaviour control.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="isCheckCanGraphElementChangeParentMode"></param>
		/// <param name="addOrRemoveInChangedPropertyNames"></param>
		/// <param name="firePropertyValueChangeEvent"></param>
		//private void SetParent(GraphElement? parent, bool checkCanGraphElementChangeParentMode, bool addOrRemoveInChangedPropertyNames, bool firePropertyValueChangeEvent, bool raiseForeignObjectSetEvent, ChangeContainer? changeContainer, ObjectActionContext context, object? requester) //, bool setSorting)
		private void SetParent(GraphElement? parent, bool checkCanGraphElementChangeParentMode, bool raiseForeignObjectSetEvent, ChangeContainer? changeContainer, ObjectActionContext context, object? requester) //, bool setSorting)
		{
			if (this.InternalState != SimpleObjectInternalState.Initialization && checkCanGraphElementChangeParentMode && !this.Manager.CanGraphElementChangeParent(this, parent)) //, enforceSetParentAndValidate: false))
				return;

			GraphElement? oldParent = null;

			oldParent = this.Parent;

			if (oldParent == parent && this.InternalState != SimpleObjectInternalState.Initialization)
				return;

			this.SetOneToManyPrimaryObject(parent, RelationPolicyModelBase.OneToManyGraphElementToParentGraphElement, raiseForeignObjectSetEvent, changeContainer, context, requester);
		}
	}
}
