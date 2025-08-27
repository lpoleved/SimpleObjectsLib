//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using Simple;
//using Simple.Modeling;

//namespace Simple.Objects
//{
//	public partial class GraphElement : SortableSimpleObject<GraphElement>
//    {
//        //private Graph graph = null;
//		//private List<GraphElement> graphElementsThatRequireSaving = null;
//        //private int treeDepth = 0;
//		//private bool isOrderIndexSet = false;
//		//private GraphElement previous = null;
//		//private bool isPreviousSet = false;
//		//private GraphElement firstChild = null;
//		//private bool isFirstChildSet = false;
//        private const string defaultPathNameSplitter = "\\";
//        private const string defaultReversePathNameSplitter = "/";

//		//protected int simpleObjectTableId, oldSimpleObjectTableId;

//		//protected int graphKey, oldGraphKey;
//		//protected Guid parent, oldParent;
//		//protected int simpleObjectTableId, oldSimpleObjectTableId;
//		//protected Guid simpleObjectGuid, oldSimpleObjectGuid;

//		internal GraphElement(SimpleObjectManager objectManager, ChangeContainer changeContainer)
//			: base(objectManager, changeContainer)
//		{
//		}

//		//internal GraphElement(SimpleObjectKey objectKey, IDictionary<int, object> propertyDataByIndex)
//		//{
//		//	this.Key = objectKey;
//		//	this.IsNew = false;
//		//	this.Load(null, propertyDataByIndex, acceptChanges: true);
//		//}

//		//internal GraphElement(SimpleObjectKey objectKey, IDictionary<string, object> propertyDataByName)
//		//{
//		//	this.Key = objectKey;
//		//	this.IsNew = false;
//		//	this.Load(null, propertyDataByName, acceptChanges: true);
//		//}

//		//public GraphElement(Graph graph, GraphElement parent, SimpleObject simpleObject)
//		//	: this(SimpleObjectManager.Instance, graph, parent, simpleObject, null)
//		//      {
//		//      }
//		public GraphElement(SimpleObjectManager objectManager, int graphKey, SimpleObject simpleObject, GraphElement parent)
//			: this(objectManager, graphKey, simpleObject, parent, objectManager.DefaultChangeContainer)
//		{
//		}

//		public GraphElement(SimpleObjectManager objectManager, int graphKey, SimpleObject simpleObject, GraphElement parent, ChangeContainer changeContainer)
//			: this(objectManager, graphKey, simpleObject, parent, changeContainer, requester: null)
//        {
//        }


//		//public GraphElement(Graph graph, GraphElement parent, SimpleObject simpleObject, object requester)
//		//	: this(SimpleObjectManager.Instance, graph, parent, simpleObject, requester)
//  //      {
//  //      }

//		public GraphElement(SimpleObjectManager objectManager, int graphKey, SimpleObject simpleObject, GraphElement parent, ChangeContainer changeContainer, object requester)
//            : base(objectManager, changeContainer)
//        {
//			this.Requester = requester;
//			//if (graph == null)
//			//	throw new ArgumentNullException("Graph cannot be null");

//			if (parent != null && parent.GraphKey != graphKey)
//				throw new ArgumentOutOfRangeException("Parent graph element does not belong to specified Graph");

//			// enforce simpleObject key creation if new and not created already
//			long simpleObjectId = simpleObject.Id;
//			//Guid simpleObjectKey = simpleObject.Guid;

//			// Force key creation first.
//			long id = this.Id;
//			//Guid key = this.Guid;

//            this.internalState = SimpleObjectInternalState.Initialization;

// 			//this.graph = graph;
//			this.SimpleObject = simpleObject;
//			this.GraphKey = graphKey;
//			//this.SimpleObjectTableId = simpleObject.Key.TableId;
//			//this.SimpleObjectGuid = simpleObject.Key.Guid;
//			//this.Graph = (parent != null) ? parent.Graph : graph;

//			this.Parent = parent;
//			//this.SetParent(parent, isCheckCanGraphElementChangeParentMode: true, addOrRemoveInChangedPropertyNames: true, raiseEvents: true); //, setSorting: this.IsNew);

//			//// If parent == null no sorting is invoked by GraphElement-ParentGraphElement relation since parent is null.
//			//if (parent == null)
//			//	this.SetOrderingAfterInsertingIntoCollection(this, this.Graph.rootGraphElements);

//            this.internalState = SimpleObjectInternalState.Normal;
//            this.Manager.NewGraphElementIsCreated(this, changeContainer, this.Requester);

//			if (this.SimpleObject.GraphElements.Count == 0)
//				throw new InvalidProgramException("GraphElement.SimpleObject.GraphElements.Count = 0");
//        }

//		//public int GraphKey
//		//{
//		//	get { return this.graphKey; }
//		//	private set { this.SetPropertyValueInternal(GraphElementModel.PropertyModel.GraphKey.Index, value, true, true, this); }
//		//}

//		public Graph Graph
//        {
//            get { return this.Manager.GetGraph(this.graphKey); }
            
//			//private set 
//			//{
//			//	if (value == this.graph)
//			//		return;
				
//			//	lock (this.lockObject)
//			//	{
//			//		if (this.IsNew)
//			//		{
//			//			if (this.graph != null && this.Parent == null)
//			//				this.graph.rootGraphElements.Remove(this);

//			//			this.graph = value;
//			//			this.GraphKey = this.graph.GraphKey;
//			//			//this.SetPropertyValueInternal<int>(GraphElementModel.PropertyModel.GraphKey.Index, value.GraphKey, ref this.graphKey , ref this.oldGraphKey, false, false, null);

//			//			if (this.graph != null && this.Parent == null) // && !this.Graph.GraphElements.ContainsKey(this.Key))
//			//				this.graph.rootGraphElements.Add(this);
//			//		}
//			//		else
//			//		{
//			//			throw new ArgumentException("You can set GraphElement's Graph property only when IsNew = true.");
//			//		}
//			//	}
//			//}
//        }

//		public bool IsAnchor { get; set; }

//		//public GraphElement Parent
//		//{
//		//	get { return this.GetOneToManyRelationForeignObject<GraphElement>(RelationPolicyModelBase.OneToManyRelationModelGraphElementToParentGraphElement); }
//		//	set { this.SetParent(value, isCheckCanGraphElementChangeParentMode: true, addOrRemoveInChangedPropertyNames: true, raiseEvents: true); } //, setSorting: true); }
//		//}

//		//public SimpleObject SimpleObject
//		//{
//		//	get { return this.GetOneToManyRelationForeignObject<SimpleObject>(RelationPolicyModelBase.OneToManyRelationModelGraphElementToSimpleObject); }
//		//	set 
//		//	{
//		//		if (this.IsNew)
//		//		{
//		//			this.SetOneToManyRelationForeignObject(this, RelationPolicyModelBase.OneToManyRelationModelGraphElementToSimpleObject, value);
//		//		}
//		//		else
//		//		{
//		//			throw new ArgumentException("You can set GraphElement's SimpleObject property only when IsNew = true.");
//		//		}
//		//	}
//		//}

//		//public new SimpleObjectCollection<GraphElement> GraphElements
//		//{
//		//	get { return this.GetOneToManyRelationKeyHolderObjectCollection<GraphElement>(RelationPolicyModelBase.OneToManyRelationModelGraphElementToParentGraphElement); }
//		//}

//		///// <summary>
//		///// Gets SimpleObjectTableId property value.
//		///// </summary>
//		//internal int SimpleObjectTableId
//		//{
//		//	get
//		//	{
//		//		if (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Server)
//		//		{
//		//			return this.simpleObjectTableId;
//		//		}
//		//		else
//		//		{
//		//			return this.SimpleObjectKey.GetTableId();
//		//		}
//		//	}
//		//}

//		public SimpleObjectCollection<GraphElement> ParentGraphElements
//        {
//            get { return (this.Parent != null) ? this.Parent.GraphElements : this.Graph.RootGraphElements; }
//        }

//		public bool HasChildren
//		{
//			get { return this.GraphElements.Count > 0; }
//		}

//		//public bool IsFirst
//		//{
//		//	get { return this.GetPropertyValue<bool>(GraphElementModel.PropertyModel.IsFirst); }
//		//	protected internal set { this.SetPropertyValue(GraphElementModel.PropertyModel.IsFirst, value); }
//		//}

//		//public bool IsLast
//		//{
//		//	get { return this.Next == null; }
//		//}

//		//public GraphElement FirstChild
//		//{
//		//	get 
//		//	{
//		//		if (!this.isFirstChildSet)
//		//		{
//		//			SimpleObjectCollection<GraphElement> unSortedGraphElements = this.GetOneToManyRelationForeignKeyHolderObjectCollection<GraphElement>(BusinessApplicationObjectRelationModel.OneToManyRelationModelGraphElementToParentGraphElement);
//		//			this.firstChild = unSortedGraphElements.FirstOrDefault(graphElement => graphElement.IsFirst == true);
//		//			this.isFirstChildSet = true;
//		//		}

//		//		return this.firstChild; 
//		//	}

//		//	private set 
//		//	{ 
//		//		this.firstChild = value;
//		//		this.isFirstChildSet = true;
//		//	}
//		//}

//		//public new GraphElement Next
//		//{
//		//	get { return this.GetOneToOneRelationForeignObject<GraphElement>(BusinessApplicationObjectRelationModel.OneToOneRelationModelGraphElementToNextGraphElement); }
//		//	internal set { this.SetOneToOneRelationForeignObject(this, BusinessApplicationObjectRelationModel.OneToOneRelationModelGraphElementToNextGraphElement, value); }
//		//}

//		//public new GraphElement Previous
//		//{
//		//	get 
//		//	{
//		//		if (!isPreviousSet)
//		//		{
//		//			var p = this.ParentGraphElements; //Enforce getiing roout collection and setting order indexing
//		//			this.isPreviousSet = true;
//		//		}

//		//		return this.previous; 
//		//	}

//		//	internal set 
//		//	{ 
//		//		this.previous = value;
//		//		this.isPreviousSet = true;
//		//	}
//		//}

//		//public new int OrderIndex
//		//{
//		//	get 
//		//	{
//		//		if (!isOrderIndexSet)
//		//		{
//		//			var p = this.ParentGraphElements; //Enforce getiing roout collection and setting order indexing
//		//			this.isOrderIndexSet = true;
//		//		}

//		//		return base.OrderIndex; 
//		//	}

//		//	internal set 
//		//	{ 
//		//		base.OrderIndex = value;
//		//		this.isOrderIndexSet = true;
//		//	}
//		//}

//		//public virtual int TreeDepth
//		//      {
//		//          get { return this.treeDepth; }
//		//          internal set { this.treeDepth = value; }
//		//      }

//		//public override object GetPropertyValue(string propertyName)
//		//{
//		//	object value = null;

//		//	if (propertyName == GraphElementModel.PropertyModel.TreeDepth.Name)
//		//	{
//		//		value = this.TreeDepth;
//		//	}
//		//	else if (propertyName == GraphElementModel.PropertyModel.OrderIndex.Name)
//		//	{
//		//		value = this.OrderIndex;
//		//	}
//		//	else
//		//	{
//		//		value = base.GetPropertyValue(propertyName);
//		//	}

//		//	return value;
//		//}

//		public int GetTreeDepth()
//		{
//			int result = 0;
//			GraphElement parent = this.Parent;

//			while (parent != null)
//			{
//				result++;
//				parent = parent.Parent;
//			}

//			return result;
//		}

//        /// <summary>
//        /// Determines whether the current GraphElement has a <see cref="Simple.Objects.GraphElement"/> instance as a parent.
//        /// </summary>
//        /// <param name="graphElement">The <see cref="Simple.Objects.GraphElement"/> instance to check.</param>
//        /// <returns>true if the current node has a <see cref="Simple.Objects.GraphElement"/> instance as a parent; otherwise, false.</returns>
//        public bool HasAsParent(GraphElement graphElement)
//        {
//			GraphElement parent = this.Parent;

//			while (parent != null)
//			{
//				if (parent == graphElement)
//					return true;

//				parent = parent.Parent;
//			}

//			return false;
//		}

//		public GraphElement GetRoot()
//        {
//            GraphElement result = this;

//            while (result.Parent != null)
//                result = result.Parent;

//            return result;
//        }

//        public string GetPathName()
//        {
//            return this.GetPathName(defaultPathNameSplitter);
//        }

//        public string GetPathName(string splitter)
//        {
//            string value = String.Empty;
//            GraphElement graphElement = this;
//            bool isFirst = true;
//            while (graphElement != null)
//            {
//                if (isFirst)
//                {
//                    isFirst = false;
//                }
//                else
//                {
//                    if (value == String.Empty)
//                    {
//                        value = graphElement.SimpleObject.GetName();
//                    }
//                    else
//                    {
//                        value = graphElement.SimpleObject.GetName() + splitter + value;
//                    }
//                }

//                graphElement = graphElement.Parent;
//            }

//            return value;
//        }

//        public string GetFullPathName()
//        {
//            return this.GetFullPathName(defaultPathNameSplitter);;
//        }

//        public string GetFullPathName(string splitter)
//        {
//            string pathName = this.GetPathName(splitter);
//            return pathName.Length > 0 ? pathName + splitter + this.SimpleObject.GetName() : this.SimpleObject.GetName();
//        }

//        public string GetReversePathName()
//        {
//            return this.GetReversePathName(defaultReversePathNameSplitter);
//        }

//        public string GetReversePathName(string splitter)
//        {
//            string value = String.Empty;
//            GraphElement graphElement = this;
//            bool isFirst = true;

//            while (graphElement != null)
//            {
//                if (isFirst)
//                {
//                    isFirst = false;
//                }
//                else
//                {
//                    if (value == String.Empty)
//                    {
//                        value = graphElement.SimpleObject.GetName();
//                    }
//                    else
//                    {
//                        value += splitter + graphElement.SimpleObject.GetName();
//                    }
//                }

//                graphElement = graphElement.Parent;
//            }

//            return value;
//        }

//        public string GetFullReversePathName()
//        {
//            return this.GetFullReversePathName(defaultReversePathNameSplitter);
//        }

//        public string GetFullReversePathName(string splitter)
//        {
//            string reversePathName = this.GetReversePathName(splitter);
//            return reversePathName.Length > 0 ? this.SimpleObject.GetName() + splitter + reversePathName : this.SimpleObject.GetName();
//        }

//		//public void MoveUp()
//		//{
//		//	GraphElement upperGraphElement = null;
//		//	int position = 1;
//		//	IList<GraphElement> graphElementCollection = (this.Parent != null) ? this.Parent.GraphElements : this.Graph.RootGraphElements;

//		//	foreach (GraphElement item in graphElementCollection)
//		//	{
//		//		if (item == this)
//		//			break;
				
//		//		upperGraphElement = item;
//		//		position++;
//		//	}

//		//	if (upperGraphElement != null)
//		//	{
//		//		upperGraphElement.SetPropertyValueInternal(GraphElementModel.PropertyModel.OrderIndex.Name, position, firePropertyValueChangeEvent: true, addOrRemoveInChangedPropertyNames: true, requester: this);

//		//		if (!upperGraphElement.IsNew)
//		//			upperGraphElement.Save();

//		//		this.SetPropertyValueInternal(GraphElementModel.PropertyModel.OrderIndex.Name, position - 1, firePropertyValueChangeEvent: true, addOrRemoveInChangedPropertyNames: true, requester: this);

//		//		if (!this.IsNew)
//		//			this.Save();
//		//	}
//		//}

//		//public void MoveDown()
//		//{
//		//	GraphElement lowerGraphElement = null;
//		//	int position = 1;
//		//	bool doBreak = false;
//		//	IList<GraphElement> graphElementCollection = (this.Parent != null) ? this.Parent.GraphElements : this.Graph.RootGraphElements;

//		//	foreach (GraphElement item in graphElementCollection)
//		//	{
//		//		if (doBreak)
//		//		{
//		//			lowerGraphElement = item;
//		//			break;
//		//		}

//		//		if (item == this)
//		//			doBreak = true;

//		//		position++;
//		//	}

//		//	if (lowerGraphElement != null)
//		//	{
//		//		lowerGraphElement.SetPropertyValueInternal(GraphElementModel.PropertyModel.OrderIndex.Name, position, firePropertyValueChangeEvent: true, addOrRemoveInChangedPropertyNames: true, requester: this);

//		//		if (!lowerGraphElement.IsNew)
//		//			lowerGraphElement.Save();

//		//		this.SetPropertyValueInternal(GraphElementModel.PropertyModel.OrderIndex.Name, position + 1, firePropertyValueChangeEvent: true, addOrRemoveInChangedPropertyNames: true, requester: this);

//		//		if (!this.IsNew)
//		//			this.Save();
//		//	}
//		//}
		
//		public SimpleObjectCollection<T> GetChildGraphElementSimpleObjectCollection<T>() where T : SimpleObject
//        {
//            SimpleObjectCollection<T> result = new SimpleObjectCollection<T>(this.Manager, this.GraphElements, graphElement => (graphElement as GraphElement).SimpleObject);
//            return result;
//        }

//		public override string GetName()
//		{
//			return String.Format("{0} (GE)", (this.SimpleObject != null) ? this.SimpleObject.GetName() ?? String.Empty : "null");
//		}

//		public override string GetDescription()
//		{
//			return String.Format("{0} (GE)", (this.SimpleObject != null) ? this.SimpleObject.GetDescription() ?? String.Empty : "null");
//		}

//		public override string ToString()
//		{
//			if (this.SimpleObject != null)
//				return this.SimpleObject.ToString() + " (GE)";
			
//			return base.ToString();
//		}

//		//public override bool RequireSaving()
//		//{
//		//	return base.RequireSaving() || (this.SimpleObject != null && this.SimpleObject.RequireSaving());
//		//}

//		protected override IPropertyModel GetPreviousIdPropertyModel()
//		{
//			return GraphElementModel.PropertyModel.PreviousId;
//		}

//		protected override IPropertyModel GetOrderIndexPropertyModel()
//		{
//			return GraphElementModel.PropertyModel.OrderIndex;
//		}

//		protected internal override IList<SortableSimpleObject> GetSortingCollection(bool sortIfSortable)
//		{ 
//			if (this.Parent != null)
//			{
//				return this.Parent.GetOneToManyRelationKeyHolderObjectCollection<GraphElement>(RelationPolicyModelBase.OneToManyRelationModelGraphElementToParentGraphElement, sortIfSortable, getOriginalList: true).AsCustom<SortableSimpleObject>();
//			}
//			else
//			{
//				return this.Graph.GetRootSortingCollection();
//			}

//			//return this.ParentGraphElements.AsCustom<SortableSimpleObject>();
			
//			//// Return unsorted parent parent GraphElements collection
//			//if (this.Parent != null)
//			//{
//			//	return this.Parent.GetOneToManyRelationKeyHolderObjectCollection<GraphElement>(RelationPolicyModelBase.OneToManyRelationModelGraphElementToParentGraphElement, sortIfSortable: false);
//			//}
//			//else
//			//{
//			//	return new SimpleObjectCollection<GraphElement>(this.Manager, this.Graph.rootGraphElements);
//			//}
//		}

//		//public void SetOrderIndex(int orderIndex)
//		//{
//		//	this.SetOrderIndex(orderIndex, requester: this);
//		//}

//		//public void SetOrderIndex(int orderIndex, object requester)
//		//{
//		//	lock (this.lockObject)
//		//	{
//		//		if (orderIndex < 0)
//		//			return;

//		//		int currentOrderIndex = this.OrderIndex;

//		//		if (currentOrderIndex == orderIndex)
//		//			return;

//		//		SimpleObjectCollection<GraphElement> parentGraphElements = this.ParentGraphElements;

//		//		if (orderIndex >= parentGraphElements.Count)
//		//			return;

//		//		List<GraphElement> relatedTransactionGraphElements = new List<GraphElement>();
//		//		GraphElement graphElementWithOrderIndex = parentGraphElements.FirstOrDefault(graphElement => graphElement.OrderIndex == orderIndex);

//		//		// Remove from current position
//		//		GraphElement oldNext = this.Next;

//		//		if (oldNext != null && this.IsFirst)
//		//		{
//		//			oldNext.IsFirst = true;		//oldNext.SetPropertyValueInternal(GraphElementModel.PropertyModel.IsFirst.Name, true, firePropertyValueChangeEvent: true, addOrRemoveInChangedPropertyNames: true, requester: this);
//		//			this.IsFirst = false;		//this.SetPropertyValueInternal(GraphElementModel.PropertyModel.IsFirst.Name, false, firePropertyValueChangeEvent: true, addOrRemoveInChangedPropertyNames: true, requester: this);
//		//			oldNext.Previous = null;
//		//			relatedTransactionGraphElements.Add(oldNext);
//		//		}
//		//		else
//		//		{
//		//			GraphElement previous = this.GetPrevious();

//		//			if (previous != null)
//		//			{
//		//				previous.Next = oldNext;

//		//				if (oldNext != null)
//		//					oldNext.Previous = previous;

//		//				//if (parentGraphElements.Count == 2)
//		//				//	previous.SetPropertyValueInternal(GraphElementModel.PropertyModel.IsFirst.Name, true, firePropertyValueChangeEvent: true, addOrRemoveInChangedPropertyNames: true, requester: this);

//		//				relatedTransactionGraphElements.Add(previous);
//		//			}
//		//		}

//		//		// Insert into new position specified by orderIndex
//		//		if (orderIndex == 0)
//		//		{
//		//			graphElementWithOrderIndex.IsFirst = false;		//graphElementWithOrderIndex.SetPropertyValueInternal(GraphElementModel.PropertyModel.IsFirst.Name, false, firePropertyValueChangeEvent: true, addOrRemoveInChangedPropertyNames: true, requester: this);
//		//			this.IsFirst = true;		//this.SetPropertyValueInternal(GraphElementModel.PropertyModel.IsFirst.Name, true, firePropertyValueChangeEvent: true, addOrRemoveInChangedPropertyNames: true, requester: this);
//		//			this.Previous = null;
//		//			this.Next = graphElementWithOrderIndex;
//		//			graphElementWithOrderIndex.Previous = this;

//		//			relatedTransactionGraphElements.Add(graphElementWithOrderIndex);
//		//		}
//		//		else
//		//		{
//		//			if (currentOrderIndex < orderIndex)
//		//			{
//		//				oldNext = graphElementWithOrderIndex.Next;
//		//				graphElementWithOrderIndex.Next = this;
//		//				this.Previous = graphElementWithOrderIndex;
//		//				this.Next = oldNext;

//		//				relatedTransactionGraphElements.Add(graphElementWithOrderIndex);
//		//			}
//		//			else
//		//			{
//		//				this.Previous = graphElementWithOrderIndex.Previous;
//		//				this.Previous.Next = this;
//		//				graphElementWithOrderIndex.Previous = this;
//		//				this.Next = graphElementWithOrderIndex;

//		//				relatedTransactionGraphElements.Add(this.Previous);
//		//			}
//		//		}


//		//		foreach (GraphElement relatedTransactionGraphElement in relatedTransactionGraphElements)
//		//			this.RelatedTransactionRequests.Add(new TransactionActionRequest(relatedTransactionGraphElement, TransactionActionRequestType.Save));

//		//		int index = 0;
//		//		GraphElement item = parentGraphElements.FirstOrDefault(graphElement => graphElement.IsFirst == true);

//		//		while (item != null)
//		//		{
//		//			item.OrderIndex = index++;
//		//			item = item.Next;
//		//		}

//		//		this.Manager.OrderIndexIsChanged(requester, this, currentOrderIndex, orderIndex);
//		//	}
//		//}

//		//protected override void OnAfterSave(object requester, bool isNewBeforeSaving)
//		//{
//		//	base.OnAfterSave(requester, isNewBeforeSaving);

//		//	if (this.graphElementsThatRequireSaving != null)
//		//	{
//		//		foreach (GraphElement graphElement in this.graphElementsThatRequireSaving)
//		//		{
//		//			if (graphElement != null)
//		//				graphElement.Manager.SaveInternal(requester, graphElement);
//		//		}

//		//		this.graphElementsThatRequireSaving = null;
//		//	}
//		//}

//		//internal static List<GraphElement> SortGraphElements(IEnumerable<GraphElement> graphElements)
//		//{
//		//	GraphElement firstGraphElement = graphElements.FirstOrDefault(graphElement => graphElement.IsFirst);
//		//	return SortGraphElements(graphElements, firstGraphElement);
//		//}

//		//internal static List<GraphElement> SortGraphElements(IEnumerable<GraphElement> unsortedGraphElements, GraphElement firstGraphElement)
//		//{
//		//	List<GraphElement> sortedGraphElements = new List<GraphElement>();
//		//	int orderIndex = 0;
//		//	GraphElement previous = null;

//		//	if (firstGraphElement != null)
//		//	{
//		//		GraphElement item = firstGraphElement;

//		//		while (item != null)
//		//		{
//		//			if (sortedGraphElements.Contains(item))
//		//			{
//		//				sortedGraphElements.Clear();
//		//				break;
//		//			}

//		//			sortedGraphElements.Add(item);
//		//			item.OrderIndex = orderIndex++;
//		//			item.Previous = previous;
//		//			previous = item;
//		//			item = item.Next;
//		//		}

//		//		if (sortedGraphElements.Count != unsortedGraphElements.Count())
//		//		{
//		//			sortedGraphElements = new List<GraphElement>(unsortedGraphElements);
//		//			orderIndex = 0;

//		//			foreach (GraphElement graphElement in sortedGraphElements)
//		//				graphElement.OrderIndex = orderIndex++;
					
//		//			//throw new Exception("Something is wrong with sorting GraphElements");
//		//		}
//		//	}
//		//	else
//		//	{
//		//		sortedGraphElements = new List<GraphElement>(unsortedGraphElements);
//		//		orderIndex = 0;

//		//		foreach (GraphElement graphElement in sortedGraphElements)
//		//			graphElement.OrderIndex = orderIndex++;
//		//	}

//		//	return sortedGraphElements;
//		//}

//		//private void SetOrderingBeforeRemovingFromParentGraphElements(object requester)
//		//{
//		//	this.SetOrderingBeforeRemovingFromParentGraphElements(requester, this.ParentGraphElements);
//		//}


//		//private GraphElement GetPrevious()
//		//{
//		//	IEnumerable<GraphElement> parentGraphElements = this.GetUnsortedParentGraphElements();
			
//		//	foreach (GraphElement item in parentGraphElements)
//		//	{
//		//		if (item.Next == this)
//		//			return item;
//		//	}

//		//	return null;
//		//}

//		//private IEnumerable<GraphElement> GetUnsortedParentGraphElements()
//		//{
//		//	if (this.Parent != null)
//		//	{
//		//		return this.Parent.GetOneToManyRelationForeignKeyHolderObjectCollection<GraphElement>(BusinessApplicationObjectRelationModel.OneToManyRelationModelGraphElementToParentGraphElement);
//		//	}
//		//	else
//		//	{
//		//		return this.Graph.rootGraphElements;
//		//	}
//		//}

//        //protected override void OnBeforeDeleting(object requester)
//        //{
            
//        //    base.OnBeforeDeleting(requester);
//        //}

//		//protected internal List<GraphElement> GraphElementsThatRequireSaving
//		//{
//		//	get
//		//	{
//		//		if (this.graphElementsThatRequireSaving == null)
//		//		{
//		//			this.graphElementsThatRequireSaving = new List<GraphElement>();
//		//		}

//		//		return this.graphElementsThatRequireSaving;
//		//	}
//		//}

//		protected override void OnBeforeRelationForeignObjectSet(SimpleObject foreignSimpleObject, SimpleObject oldForeignSimpleObject, IOneToOneOrManyRelationModel objectRelationPolicyModel, ObjectRelationType objectRelationType, ref bool cancel, ChangeContainer changeContainer, object requester)
//		{
//			base.OnBeforeRelationForeignObjectSet(foreignSimpleObject, oldForeignSimpleObject, objectRelationPolicyModel, objectRelationType, ref cancel, changeContainer, requester);

//			if (this.DeleteStarted)
//				return;
			
//			// Prevent changing SimpleObject when object is stored or saved.
//			if (objectRelationType == ObjectRelationType.OneToMany)
//			{
//				if (objectRelationPolicyModel.RelationKey == RelationPolicyModelBase.OneToManyRelationModelGraphElementToSimpleObject.RelationKey)
//				{
//					if (!this.IsNew && !this.DeleteStarted && !this.DeleteRequested)
//					{
//						cancel = true;
//						throw new ArgumentException("You can set GraphElement's SimpleObject property only when IsNew = true.");
//					}
//				}
//				else if (objectRelationPolicyModel.RelationKey == RelationPolicyModelBase.OneToManyRelationModelGraphElementToParentGraphElement.RelationKey)
//				{
//					if (requester != null) // requester == GraphElement.Empty handles SetParent method
//						this.Manager.BeforeGraphElementParentIsChanged(this, oldForeignSimpleObject as GraphElement, foreignSimpleObject as GraphElement, ref cancel, requester);
//				}
//			}
//		}


//		//protected override void OnBeforeSetOrderingAfterOneToManyRelationForeignObjectSet(object requester, SimpleObject simpleObject, SimpleObject foreignSimpleObject, SimpleObject oldForeignSimpleObject, IOneToOneOrManyRelationModel objectRelationPolicyModel)
//		//{
//		//	base.OnBeforeSetOrderingAfterOneToManyRelationForeignObjectSet(requester, simpleObject, foreignSimpleObject, oldForeignSimpleObject, objectRelationPolicyModel);

//		//	if (this.IsDeleteStarted)
//		//		return;

//		//	if (objectRelationPolicyModel.RelationKey == RelationPolicyModelBase.OneToManyRelationModelGraphElementToParentGraphElement.RelationKey)
//		//	{
//		//		if (foreignSimpleObject == null) // foreignSimpleObject is parent
//		//		{
//		//			this.Graph.rootGraphElements.Add(this);
//		//		}
//		//		else if (oldForeignSimpleObject == null) // oldForeignSimpleObject is oldParent
//		//		{
//		//			this.Graph.rootGraphElements.Remove(this);
//		//		}
//		//	}
//		//}

//		protected override void OnRelationForeignObjectSet(SimpleObject foreignSimpleObject, SimpleObject oldForeignSimpleObject, IOneToOneOrManyRelationModel objectRelationPolicyModel, ObjectRelationType objectRelationType, ChangeContainer changeContainer, object requester)
//		{
//			base.OnRelationForeignObjectSet(foreignSimpleObject, oldForeignSimpleObject, objectRelationPolicyModel, objectRelationType, changeContainer, requester);

//			if (this.DeleteStarted)
//				return;

//			// requester == GraphElement.Empty handles SetParent method
//			if (requester != null && objectRelationType == ObjectRelationType.OneToMany && objectRelationPolicyModel.RelationKey == RelationPolicyModelBase.OneToManyRelationModelGraphElementToParentGraphElement.RelationKey)
//			{
//				GraphElement parent = foreignSimpleObject as GraphElement;
//				GraphElement oldParent = oldForeignSimpleObject as GraphElement;

//				//this.TreeDepth = (parent == null) ? 0 : parent.TreeDepth + 1;
//				//this.SetChildrenTreeDepth(this);
//				this.Manager.GraphElementParentIsChanged(this, oldParent, parent, changeContainer, requester);
//			}
//		}

//		//protected override void OnPropertyValueChange(IPropertyModel propertyModel, object value, object oldValue, object requester)
//		//{
//		//	base.OnPropertyValueChange(propertyModel, value, oldValue, requester);

//		//	if (this.IsDeleteStarted)
//		//		return;

//		//	if (requester == SimpleObject.RejectChangesRequester && propertyModel.Index == GraphElementModel.PropertyModel.ParentId.Index)
//		//	{
//		//		long parentId = (long)value;
//		//		GraphElement parent = this.Manager.GetSimpleObject(GraphElementModel.TableId, parentId) as GraphElement;

//		//		//this.SetOneToManyRelationForeignObject(requester, RelationPolicyModelBase.OneToManyRelationModelGraphElementToParentGraphElement, parent,
//		//		//									   addOrRemoveInChangedPropertyNames: false, firePropertyValueChangeEvent: false, raiseForeignObjectSetEvent: true);

//		//		this.SetOneToManyRelationForeignObject(RelationPolicyModelBase.OneToManyRelationModelGraphElementToParentGraphElement, parent, requester); //this.Parent = parent;
//		//		//this.RejectOrderIndexChange();


//		//		//this.SetParent(parent, checkCanGraphElementChangeParentMode: false, addOrRemoveInChangedPropertyNames: false, firePropertyValueChangeEvent: false, raiseForeignObjectSetEvent: true); //, setSorting: false);
//		//		////this.ObjectManager.GraphElementParentIsChanged(requester, this, oldParent, parent);
//		//	}
//		//}

//		protected override ISimpleObjectCollection GetOneToManyNullCollection(int oneToManyRelationKey)
//		{
//			if (oneToManyRelationKey == RelationPolicyModelBase.OneToManyRelationModelGraphElementToParentGraphElement.RelationKey)
//				return this.Graph.rootGraphElements;

//			return base.GetOneToManyNullCollection(oneToManyRelationKey);
//		}

//		internal void SetParentWithoutCheckingCanGraphElementChangeParentOrRaisingEvents(GraphElement parent, ChangeContainer changeContainer, object requester)
//        {
//			this.SetParent(parent, checkCanGraphElementChangeParentMode: false, addOrRemoveInChangedPropertyNames: false, firePropertyValueChangeEvent: false, raiseForeignObjectSetEvent: false, changeContainer, requester: requester); //, setSorting: false);
//        }

//		//protected override void OnAfterLoad()
//		//{
//		//	base.OnAfterLoad();

//		//	this.graph = this.ObjectManager.GetGraph(this.GraphKey);

//		//	//if (this.Graph != null && this.GetPropertyValue<Guid>(GraphElementModel.PropertyModel.ParentGuid) == default(Guid) && this.Parent == null && !this.Graph.rootGraphElements.Contains(this))
//		//	if (this.Graph != null && this.GetPropertyValue<Guid>(GraphElementModel.PropertyModel.ParentKey) == default(Guid)) // this.Parent == null)
//		//	{
//		//		this.Graph.rootGraphElements.Add(this);
//		//		this.Graph.requireSorting = true;
//		//	}
//		//}

//		//protected override void OnBeforeDeleting(object requester)
//		//{
//		//	base.OnBeforeDeleting(requester);

//		//	if (this.Graph != null && this.Parent == null)
//		//		this.Graph.rootGraphElements.Remove(this);
//		//}

//		/// <summary>
//		/// Manualy handling of setting Parent relation foreign object with customized behaviour control.
//		/// </summary>
//		/// <param name="parent"></param>
//		/// <param name="isCheckCanGraphElementChangeParentMode"></param>
//		/// <param name="addOrRemoveInChangedPropertyNames"></param>
//		/// <param name="firePropertyValueChangeEvent"></param>
//		private void SetParent(GraphElement parent, bool checkCanGraphElementChangeParentMode, bool addOrRemoveInChangedPropertyNames, bool firePropertyValueChangeEvent, bool raiseForeignObjectSetEvent, ChangeContainer changeContainer, object requester) //, bool setSorting)
//        {
//            if (this.internalState != SimpleObjectInternalState.Initialization && checkCanGraphElementChangeParentMode && !this.Manager.CanGraphElementChangeParent(this, parent, enforceSetParentAndValidate: false))
//                return;

//			GraphElement oldParent = null;
			
//			//lock (this.lockObject)
//			//{
//				oldParent = this.Parent;

//				if (oldParent == parent && this.internalState != SimpleObjectInternalState.Initialization)
//					return;

//				//if (!this.initializationInProgress && raiseEvents)
//				//{
//				//	bool cancel = false;
//				//	this.Manager.BeforeGraphElementParentIsChanged(GraphElement.Empty, this, oldParent, parent, ref cancel);

//				//	if (cancel)
//				//		return;
//				//}

//				//if (setSorting)
//				//	this.SetOrderingBeforeRemovingFromCollection(this, this.ParentGraphElements);

//				//if (this.Graph != null && oldParent == null && parent != null)
//				//	this.SetOrderingBeforeRemovingFromCollection(this, this.Graph.rootGraphElements); raiseForeignObjectSetEvent

//				this.SetOneToManyRelationForeignObject(parent, RelationPolicyModelBase.OneToManyRelationModelGraphElementToParentGraphElement, 
//													   addOrRemoveInChangedPropertyNames, firePropertyValueChangeEvent, raiseForeignObjectSetEvent, changeContainer, requester);

//				//if (this.Graph != null)
//				//{
					
//				//if (parent == null)
//				//{
//				//	this.Graph.rootGraphElements.Add(this);
//				//}
//				//else if (oldParent == null)
//				//{
//				//	this.Graph.rootGraphElements.Remove(this);
//				//}

////				if (oldParent != null && this.Parent == null)
////					{
////						this.Graph.rootGraphElements.Add(this);
////						//this.SetOrderingAfterInsertingIntoCollection(this, this.Graph.rootGraphElements);
////					}
////					else if (oldParent == null && this.Parent != null)
////					{
//////						this.SetOrderingBeforeRemovingFromCollection(this, this.Graph.rootGraphElements);
////						this.Graph.rootGraphElements.Remove(this);
////					}
////				//}

//				//this.TreeDepth = (parent == null) ? 0 : parent.TreeDepth + 1;
//				//this.SetChildrenTreeDepth(this);

//				//if (setSorting)
//				//	this.SetOrderingAfterInsertingIntoCollection(this, this.ParentGraphElements);
//			//}

//			//if (!this.initializationInProgress && raiseEvents)
//			//	this.Manager.GraphElementParentIsChanged(GraphElement.Empty, this, oldParent, parent);
//		}

//        //private void SetChildrenTreeDepth(GraphElement graphElement)
//        //{
//        //    foreach (GraphElement childGraphElement in graphElement.GraphElements)
//        //    {
//        //        childGraphElement.TreeDepth = graphElement.TreeDepth + 1;
//        //        this.SetChildrenTreeDepth(childGraphElement);
//        //    }
//        //}
//    }
//}
