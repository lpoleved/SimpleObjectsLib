//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Simple.Collections;

//namespace Simple.Objects
//{
//	public partial class Graph : SimpleObject //SystemObject<int, Graph>
//	{
//		//private IList<SortableSimpleObject> sortingCollection = null;
//		private SimpleObjectCollection<GraphElement> rootGraphElements = null;

//		public static Graph Empty;

//		public Graph(SimpleObjectManager objectManager)
//			: base(objectManager)
//		{
//		}

//		//static Graph()
//		//{
//		//	Model.AutoGenerateKey = false;
//		//}

//		//public Graph()
//		//{
//		//}

//		//public Graph(SimpleObjectManager objectManager, ref SystemObjectCollectionByObjectKey<int, Graph> dictionaryCollection, int graphKey, string name)
//		//	: base(objectManager, ref dictionaryCollection,
//		//		  (item) =>
//		//		  {
//		//			  item.GraphKey = graphKey;
//		//			  item.Name = name;
//		//		  })
//		//{
//		//	//                    this.rootGraphElements = this.Manager.ObjectCache.GetObjectCollection<GraphElement>(ge => ge.GraphID == this.Key.ObjectId && ge.Parent == null); // GraphElementCache.GetGraphElementCollection(this.Key.ObjectId, 0);
//		//	//this.CreateRootGraphElementCollection();
//		//}



//		protected override void OnAfterLoad()
//		{
//			base.OnAfterLoad();

//			if (Empty == null && !this.IsNew && this.Id == 0)
//				Empty = this;
//		}

//		//[NonStorable]
//		public SimpleObjectCollection<GraphElement> RootGraphElements
//		{
//			get
//			{
//				if (this.rootGraphElements == null)
//					this.rootGraphElements = this.Manager.GetObjectCache(GraphElementModel.TableId).Select<GraphElement>(graphElement => graphElement.GraphId == this.Id && graphElement.ParentId == 0, isSortable: true);
					

//				return this.rootGraphElements;
//			}
//		}

//		//internal void GraphElementParentIsChanged(GraphElement graphElement, GraphElement oldParent, GraphElement newParent, object requester)
//		//{
//		//	if (this.rootGraphElements != null)
//		//	{
//		//		if (oldParent == null)
//		//		{
//		//			this.rootGraphElements.ListRemove(graphElement);
//		//		}
//		//		else if (newParent == null)
//		//		{
//		//			this.rootGraphElements.ListAdd(newParent);
//		//		}
//		//	}
//		//}

//		//internal void RootGraphElementIsCreated(GraphElement graphElement)
//		//{
//		//	if (this.rootGraphElements != null)
//		//	{
//		//		if (!this.rootGraphElements.Contains(graphElement))
//		//		{
//		//			this.rootGraphElements.ListAdd(graphElement);
//		//		}
//		//		else
//		//		{
//		//			this.rootGraphElements = this.rootGraphElements;
//		//		}
//		//	}
//		//}

//		//internal void RootGraphElementIsDeleting(GraphElement graphElement)
//		//{
//		//	if (this.rootGraphElements != null)
//		//		this.rootGraphElements.ListRemove(graphElement);
//		//}

//		//internal IList<SortableSimpleObject> GetRootSortingCollection()
//		//{
//		//	if (this.sortingCollection == null)
//		//		this.sortingCollection = this.GraphElements.AsCustom<SortableSimpleObject>();

//		//	return this.sortingCollection;
//		//}

//		public override string ToString()
//		{
//			return "Graph " + base.ToString();
//		}
//	}
//}
