using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Collections;

namespace Simple.Objects
{
	public sealed class SystemGraph : SystemObject<int, SystemGraph>
	{
		private SimpleObjectCollection<GraphElement>? rootGraphElements = null;

		//public static SystemGraph? Empty;

		static SystemGraph()
		{
			Model.TableInfo = SystemTables.SystemGraphs;
			Model.AutoGenerateKey = false;
		}

		public SystemGraph()
		{
		}

		public SystemGraph(SimpleObjectManager objectManager, ref SystemObjectCollectionByObjectKey<int, SystemGraph> dictionaryCollection, int graphKey, string name)
			: base(objectManager, ref dictionaryCollection,
				  (item) =>
				  {
					  item.GraphKey = graphKey;
					  item.Name = name;
				  })
		{
			//this.rootGraphElements = this.Manager.ObjectCache.GetObjectCollection<GraphElement>(ge => ge.GraphID == this.Key.ObjectId && ge.Parent == null); // GraphElementCache.GetGraphElementCollection(this.Key.ObjectId, 0);
			//this.CreateRootGraphElementCollection();
		}

		[ObjectKey]
		[DatastoreType(typeof(short))]
		public int GraphKey { get; set; }

		public string? Name { get; set; }

		//[NonStorable]

		public SimpleObjectCollection<GraphElement> GetRootGraphElements()
		{
			if (this.rootGraphElements == null)
			{
				if (this.ObjectManager is null)
					throw new Exception("The ObjectManager cannot be null");

				if (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Client)
				{
					this.ObjectManager.CacheGraphElementsWithObjectsFromServer(this.GraphKey, parentGraphElementId: 0, out List<long> graphElementIds); //, out bool[] hasChildrenInfo);
					this.rootGraphElements = new SimpleObjectCollection<GraphElement>(this.ObjectManager, GraphElementModel.TableId, graphElementIds);

					return this.rootGraphElements;
				}
				else
				{
					this.rootGraphElements = (this.ObjectManager.GetObjectCache(GraphElementModel.TableId) as ServerObjectCache)!
															    .Select<GraphElement>(graphElement => graphElement.GraphKey == this.GraphKey &&
																									  graphElement.ParentId == 0, sortCollection: true);
				}
			}

			return this.rootGraphElements;
		}

		//public SimpleObjectCollection<GraphElement> GetRootGraphElements(out bool[] hasChildrenInfo)
		//{
		//	if (this.rootGraphElements == null)
		//	{
		//		if (this.ObjectManager is null)
		//			throw new Exception("The ObjectManager cannot be null");

		//		if (this.ObjectManager.WorkingMode == ObjectManagerWorkingMode.Client)
		//		{
		//			this.ObjectManager.CacheGraphElementsWithObjectsFromServer(this.GraphKey, parentGraphElementId: 0, out List<long> graphElementIds, out hasChildrenInfo);
		//			this.rootGraphElements = new SimpleObjectCollection<GraphElement>(this.ObjectManager, GraphElementModel.TableId, graphElementIds);
					
		//			return this.rootGraphElements;
		//		}
		//		else
		//		{
		//			this.rootGraphElements = (this.ObjectManager.GetObjectCache(GraphElementModel.TableId) as ServerObjectCache)!
		//													    .Select<GraphElement>(graphElement => graphElement.GraphKey == this.GraphKey &&
		//																							  graphElement.ParentId == 0, sortCollection: true);
		//		}
		//	}

		//	hasChildrenInfo = new bool[this.rootGraphElements.Count];

		//	for (int i = 0; i < this.rootGraphElements.Count; i++)
		//		hasChildrenInfo[i] = this.rootGraphElements[i].HasChildren;
			
		//	return this.rootGraphElements;
		//}


		//protected override void OnLoad()
		//{
		//	base.OnLoad();

		//	if (Empty == null && !this.IsNew && this.GraphKey == 0)
		//		Empty = this;
		//}
	}
}
