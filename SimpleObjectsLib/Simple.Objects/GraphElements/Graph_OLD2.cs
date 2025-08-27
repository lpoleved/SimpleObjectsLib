//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Simple.Collections;

//namespace Simple.Objects
//{
//	public sealed class Graph : SystemObject<int, Graph>
//	{
//		private IList<SortableSimpleObject> sortingCollection = null;
//		internal SimpleObjectCollection<GraphElement> rootGraphElements = null;

//		static Graph()
//		{
//			Model.AutoGenerateKey = false;
//		}

//		public Graph()
//		{
//		}

//		public Graph(SimpleObjectManager objectManager, ref SystemObjectCollectionByObjectKey<int, Graph> dictionaryCollection, int graphKey, string name)
//			: base(objectManager, ref dictionaryCollection,
//				  (item) =>
//				  {
//					  item.GraphKey = graphKey;
//					  item.Name = name;
//				  })
//		{
//			//                    this.rootGraphElements = this.Manager.ObjectCache.GetObjectCollection<GraphElement>(ge => ge.GraphID == this.Key.ObjectId && ge.Parent == null); // GraphElementCache.GetGraphElementCollection(this.Key.ObjectId, 0);
//			//this.CreateRootGraphElementCollection();
//		}

//		[ObjectKey]
//		[DatastoreType(typeof(short))]
//		public int GraphKey { get; set; }

//		public string Name { get; set; }

//		[NonStorable]
//		public SimpleObjectCollection<GraphElement> RootGraphElements
//		{
//			get
//			{
//				if (this.rootGraphElements == null)
//					this.CreateRootGraphElements();

//				return new SimpleObjectCollection<GraphElement>(this.ObjectManager, this.rootGraphElements.ToArray());
//			}
//		}

//		[NonStorable]
//		public int RootGraphElementsCount
//		{
//			get
//			{
//				if (this.rootGraphElements == null)
//					this.CreateRootGraphElements();

//				return this.rootGraphElements.Count;
//			}
//		}

//		public GraphElement GetRootGraphElement(int index)
//		{
//			if (this.rootGraphElements == null)
//				this.CreateRootGraphElements();

//			return this.rootGraphElements.ElementAt(index);
//		}

//		internal IList<SortableSimpleObject> GetRootSortingCollection()
//		{
//			if (this.sortingCollection == null)
//			{
//				if (this.rootGraphElements == null)
//					this.CreateRootGraphElements();

//				this.sortingCollection = this.rootGraphElements.AsCustom<SortableSimpleObject>();
//			}

//			return this.sortingCollection;
//		}

//		private void CreateRootGraphElements()
//		{
//			this.rootGraphElements = this.ObjectManager.GetRootGraphElements(this.GraphKey);
//			this.rootGraphElements.SortIfSortable();
//		}
//	}
//}
