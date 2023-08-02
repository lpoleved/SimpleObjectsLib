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
		private bool newGraphElementCreatedEventRised = false;
		private bool isChildrenLoadedInClientMode = false;
		private bool hasChildrenInClientModeWhenNotLoaded = false;
		private bool isHasChildrenInClientModeWhenNotLoadedSet = false;

		public GraphElement(SimpleObjectManager objectManager)
			: base(objectManager)
		{
		}

		public GraphElement(SimpleObjectManager objectManager, int graphKey, SimpleObject simpleObject, GraphElement? parent, object? requester = null)
			: this(objectManager, graphKey, simpleObject, parent, objectManager.DefaultChangeContainer, requester)
		{
		}

		public GraphElement(SimpleObjectManager objectManager, int graphKey, SimpleObject simpleObject, GraphElement? parent, ChangeContainer changeContainer)
			: this(objectManager, graphKey, simpleObject, parent, changeContainer, requester: null)
		{
		}

		public GraphElement(SimpleObjectManager objectManager, int graphKey, SimpleObject simpleObject, GraphElement? parent, ChangeContainer changeContainer, object? requester)
			: base(objectManager, changeContainer)
		{
			//if (changeContainer != this.Manager.DefaultChangeContainer)
			//	this.ChangeContainer = changeContainer;
			//this.initialization = true;
			
			//this.ChangeContainer = changeContainer;
			this.Requester = requester;
			this.IsStorable = simpleObject.IsStorable;
			this.GraphKey = graphKey;
			this.SimpleObject = simpleObject;
			this.Parent = parent;
			_ = this.Id; // Enforce new Id creation
			this.Manager.NewGraphElementIsCreated(this, changeContainer, requester);
			this.newGraphElementCreatedEventRised = true;

			//this.initialization = false;
		}

		protected override SimpleObjectCollection GetSortingCollection()
		{
			return this.GetNeighbours();
		}

		internal SystemGraph Graph
		{
			get { return this.Manager.GetSystemGraph(this.GraphKey); }
		}

		internal bool NewGraphElementCreatedEventRised { get => this.newGraphElementCreatedEventRised; set => this.newGraphElementCreatedEventRised = false; }


		internal bool IsChildrenLoadedInClientMode { get => this.isChildrenLoadedInClientMode; set => this.isChildrenLoadedInClientMode = value; }
		
		internal bool HasChildrenInClientModeWhenNotLoaded 
		{ 
			get => this.hasChildrenInClientModeWhenNotLoaded;
			set
			{
				this.hasChildrenInClientModeWhenNotLoaded = value;
				this.isHasChildrenInClientModeWhenNotLoadedSet = true;
			}
		}

		//internal bool IsHasChildrenInClientModeWhenNotLoadedSet { get => this.isHasChildrenInClientModeWhenNotLoadedSet; set => this.isHasChildrenInClientModeWhenNotLoadedSet = value; }


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

		//public SimpleObjectCollection<GraphElement> GetGraphElements(out bool[] hasChildrenInfo)
		//{
		//	return this.Manager.GetGraphElements(this.GraphKey, this.parentId, out hasChildrenInfo);
		//}

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
		public SimpleObjectCollection<GraphElement> GetNeighbours()
		{
			return (this.Parent != null) ? this.Parent.GraphElements : this.Graph.GetRootGraphElements();
		}

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
			return String.Format("{0} (GE)", (this.SimpleObject != null) ? this.SimpleObject.GetName() ?? String.Empty : "null");
		}

		public override string GetDescription()
		{
			return String.Format("{0} (GE)", (this.SimpleObject != null) ? this.SimpleObject.GetDescription() ?? String.Empty : "null");
		}

		public override string? ToString()
		{
			if (this.SimpleObject != null)
				return this.SimpleObject.ToString() + " (GE)";

			return base.ToString();
		}

		protected override void OnBeforeRelationPrimaryObjectSet(SimpleObject? primarySimpleObject, SimpleObject? oldPrimarySimpleObject, IOneToOneOrManyRelationModel objectRelationPolicyModel, ref bool cancel, ChangeContainer changeContainer, object? requester)
		{
			base.OnBeforeRelationPrimaryObjectSet(primarySimpleObject, oldPrimarySimpleObject, objectRelationPolicyModel, ref cancel, changeContainer, requester);

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

						this.Manager.BeforeGraphElementParentIsChanged(this, newParent, ref cancel, requester);
					}
				}
			}
		}

		protected override void OnRelationForeignObjectSet(SimpleObject? foreignSimpleObject, SimpleObject? oldForeignSimpleObject, IOneToOneOrManyRelationModel objectRelationPolicyModel, ChangeContainer changeContainer, object? requester)
		{
			base.OnRelationForeignObjectSet(foreignSimpleObject, oldForeignSimpleObject, objectRelationPolicyModel, changeContainer, requester);

			if (this.DeleteStarted)
				return;

			// requester == GraphElement.Empty handles SetParent method
			//if (requester != null && objectRelationPolicyModel.RelationType == ObjectRelationType.OneToMany && objectRelationPolicyModel.RelationKey == RelationPolicyModelBase.OneToManyGraphElementToParentGraphElement.RelationKey)
			if (objectRelationPolicyModel.RelationKey == RelationPolicyModelBase.OneToManyGraphElementToParentGraphElement.RelationKey && 
				foreignSimpleObject != oldForeignSimpleObject && this.newGraphElementCreatedEventRised)
			{
				//GraphElement parent = foreignSimpleObject as GraphElement;
				//GraphElement oldParent = oldForeignSimpleObject as GraphElement;

				//this.TreeDepth = (parent == null) ? 0 : parent.TreeDepth + 1;
				//this.SetChildrenTreeDepth(this);
				this.Manager.GraphElementParentIsChanged(this, oldForeignSimpleObject as GraphElement, changeContainer, requester);
			}
		}

		protected override void OnAfterLoad()
		{
			base.OnAfterLoad();

			this.newGraphElementCreatedEventRised = true;
		}

		protected override SimpleObjectCollection GetOneToManyForeignNullCollection(int oneToManyRelationKey)
		{
			if (oneToManyRelationKey == RelationPolicyModelBase.OneToManyGraphElementToParentGraphElement.RelationKey)
				return this.Graph.GetRootGraphElements(); //  rootSortingGraphElementsCollection;

			return base.GetOneToManyForeignNullCollection(oneToManyRelationKey);
		}

		//protected override void OnPropertyValueChange(IPropertyModel propertyModel, object value, object oldValue, bool isChanged, bool isSaveable, ChangeContainer changeContainer, object requester)
		//{
		//	base.OnPropertyValueChange(propertyModel, value, oldValue, isChanged, isSaveable, changeContainer, requester);

		//	if (!this.initialization && !this.newGraphElementCreatedEventRised && this.IsNew && this.GraphKey > 0 && this.Manager.ContainsObject(this.ObjectTableId, this.ObjectId))
		//	{
		//		_ = this.Id; // Enforce new Id creation
		//		this.IsStorable = this.SimpleObject.IsStorable;
		//		this.Manager.NewGraphElementIsCreated(this, changeContainer, requester);
		//		this.newGraphElementCreatedEventRised = true;
		//	}
		//}

		protected override void OnOrderIndexChange(int orderIndex, int oldOrderIndex, ChangeContainer changeContainer, object? requester)
		{
			base.OnOrderIndexChange(orderIndex, oldOrderIndex, changeContainer, requester);
			
			this.SimpleObject?.GraphElementOrderIndexIsChanged(this, orderIndex, oldOrderIndex, changeContainer, requester);
		}

		internal void SetParentWithoutCheckingCanGraphElementChangeParentOrRaisingEvents(GraphElement parent, ChangeContainer changeContainer, object requester)
		{
			this.SetParent(parent, checkCanGraphElementChangeParentMode: false, addOrRemoveInChangedPropertyNames: false, firePropertyValueChangeEvent: false, raiseForeignObjectSetEvent: false, changeContainer, requester: requester); //, setSorting: false);
		}

		internal void SetGraphElementCollectionInternal(SimpleObjectCollection<GraphElement> graphElements)
		{
			// TODO: Check if this sorting is needed?
			if (!this.OneToManyForeignCollectionsByRelationKey.ContainsKey(RelationPolicyModelBase.OneToManyGraphElementToParentGraphElement.RelationKey))
			{
				graphElements.Sort(setPrevious: true, saveObjects: false); //  GraphElementModel
				this.OneToManyForeignCollectionsByRelationKey.Add(RelationPolicyModelBase.OneToManyGraphElementToParentGraphElement.RelationKey, graphElements);
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
		private void SetParent(GraphElement parent, bool checkCanGraphElementChangeParentMode, bool addOrRemoveInChangedPropertyNames, bool firePropertyValueChangeEvent, bool raiseForeignObjectSetEvent, ChangeContainer changeContainer, object requester) //, bool setSorting)
		{
			if (this.InternalState != SimpleObjectInternalState.Initialization && checkCanGraphElementChangeParentMode && !this.Manager.CanGraphElementChangeParent(this, parent)) //, enforceSetParentAndValidate: false))
				return;

			GraphElement? oldParent = null;

			//lock (this.lockObject)
			//{
			oldParent = this.Parent;

			if (oldParent == parent && this.InternalState != SimpleObjectInternalState.Initialization)
				return;

			this.SetOneToManyPrimaryObject(parent, RelationPolicyModelBase.OneToManyGraphElementToParentGraphElement, addOrRemoveInChangedPropertyNames, 
										   firePropertyValueChangeEvent, raiseForeignObjectSetEvent, changeContainer, requester);
		}
	}
}
