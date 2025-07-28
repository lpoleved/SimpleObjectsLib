//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using Simple;

//namespace Simple.Objects
//{
//    public class Graph
//    {
//        //private SimpleObjectCollection<GraphElement> rootGraphElements = null;

//        //public Graph()
//        //{
//        //}

//        //public Graph(SimpleObjectManager manager)
//        //    : base(manager)
//        //{
//        //}
//		private SimpleObjectManager objecManager = null;
//		private object lockObject = new object();
//		//internal HashSet<GraphElement> rootGraphElements = new HashSet<GraphElement>();
//		private SimpleObjectCollection<GraphElement> rootGraphElements = null;
//		private bool requireSorting = false;
		
//		//private GraphElement firstChild = null;
//		//private bool isFirstChildSet = false;


//        public Graph(SimpleObjectManager objectManager, int graphKey, string name)
//        {
//			this.objecManager = objectManager;
//			this.rootGraphElements = new SimpleObjectCollection<GraphElement>(this.objecManager);
//			this.GraphKey = graphKey;
//            this.Name = name;
////            this.RootGraphElements = new SimpleObjectCollection<GraphElement>(SimpleObjectManager.Instance, );
//        }

//        public int GraphKey { get; private set; }
//        public string Name { get; private set; }
        
//        ///// <summary>
//        ///// Gets only GraphElements that belongs to this graph and has Parent property set to null.
//        ///// </summary>
//        public SimpleObjectCollection<GraphElement> RootGraphElements 
//        {
//			get
//			{
//				if (this.requireSorting)
//				{
//					this.rootGraphElements.SortIfSortable();
//					this.requireSorting = false;
//				}
				
//				return new SimpleObjectCollection<GraphElement>(this.objecManager, this.rootGraphElements.ToArray());
//			}
			
//			//get 
//			//{
//			//	lock (this.lockObject)
//			//	{
//			//		SimpleObjectCollection<GraphElement> graphElements = new SimpleObjectCollection<GraphElement>(this.manager, this.rootGraphElements);
//			//		graphElements.SortIfSortable();

//			//		return graphElements;
//			//	}
//			//}
//        }

//		public int RootGraphElementsCount
//		{
//			get { return this.rootGraphElements.Count; }
//		}

//		public GraphElement GetRootGraphElement(int index)
//		{
//			return this.rootGraphElements.ElementAt(index);
//		}

//		internal bool RootGraphElementsRequireSorting
//		{
//			get { return this.requireSorting; }
//			set { this.requireSorting = value; }
//		}

//		//public GraphElement FirstChild
//		//{
//		//	get
//		//	{
//		//		if (!this.isFirstChildSet)
//		//		{
//		//			this.firstChild = this.rootGraphElements.Find(graphElement => graphElement.IsFirst == true);
//		//			this.isFirstChildSet = true;
//		//		}

//		//		return this.firstChild;
//		//	}

//		//	internal set
//		//	{
//		//		this.firstChild = value;
//		//		this.isFirstChildSet = true;
//		//	}
//		//}

////        {
////            get
////            {
//////                if (this.rootGraphElements == null)
//////                {
////////                    this.rootGraphElements = this.Manager.ObjectCache.GetObjectCollection<GraphElement>(ge => ge.GraphID == this.Key.ObjectId && ge.Parent == null); // GraphElementCache.GetGraphElementCollection(this.Key.ObjectId, 0);
//////                    this.rootGraphElements = this.Manager.ObjectCache.GetObjectCollection<GraphElement>(ge => ge.Graph == this && ge.Parent == null); // GraphElementCache.GetGraphElementCollection(this.Key.ObjectId, 0);
//////                }

//////                return this.rootGraphElements;
////            }
////        }

//        //public int ID
//        //{
//        //    get { return this.Key.ObjectId; }
//        //    private set { this.SetPropertyValueInternal(Names.FieldID, value, false, false, this); }
//        //}

//        ///// <summary>
//        ///// Gets the all GraphElements that belongs to this graph.
//        ///// </summary>
//        //public new SimpleObjectCollection<GraphElement> GraphElements
//        //{
//        //    get { return this.GetOneToManyRelationForeignKeyHolderObjectCollection<GraphElement>(BusinessApplicationObjectRelationModel.OneToManyRelationModelGraphElementToGraph); }
//        //}

//    }
//}
