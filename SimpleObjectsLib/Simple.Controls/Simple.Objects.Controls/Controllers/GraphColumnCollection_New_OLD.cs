//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Simple.Objects.Controls
//{
//	public class GraphColumnCollection_New : IEnumerable<GraphColumn>, IEnumerable
//	{
//		private List<GraphColumn> list = new List<GraphColumn>();
//		private IGraphControllerControl graphController = null;

//		public GraphColumnCollection_New(IGraphControllerControl graphController)
//		{
//			this.graphController = graphController;
//		}

//		public GraphColumn this[int index]
//		{
//			get 
//			{
//				GraphColumn value = this.list[index];
//				return value;
//			}
//		}

//		public GraphColumn this[string name]
//		{
//			get
//			{
//				GraphColumn value = null;

//				foreach (GraphColumn graphColumn in this)
//				{
//					if (graphColumn.Name == name)
//					{
//						value = graphColumn;
//						break;
//					}
//				}

//				return value;
//			}
//		}

//		public int Count
//		{
//			get { return this.list.Count; }
//		}

//		public void Load<TEnum>()
//		{
//			if (typeof(TEnum).IsEnum)
//			{
//				foreach (string columnName in Enum.GetNames(typeof(TEnum)))
//				{
//					GraphColumn graphColumn = new GraphColumn(columnName);
//					this.Add(graphColumn);
//				}
//			}
//		}

//		public int Add(GraphColumn graphColumn)
//		{
//			this.list.Add(graphColumn);
//			graphColumn.IsNew = false;
//			graphColumn.GraphController = this.graphController;
//		}

//		public bool Remove(GraphColumn graphColumn)
//		{
//			bool isRemoved = this.list.Remove(graphColumn);
//			return isRemoved;
//		}

//		/// <summary>
//		/// Returns an enumerator that iterates through the collection.
//		/// </summary>
//		/// <returns> A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection. </returns>
//		IEnumerator<GraphColumn> IEnumerable<GraphColumn>.GetEnumerator()
//		{
//			return this.list.GetEnumerator();
//		}

//		/// <summary>
//		/// Returns an enumerator that iterates through a collection.
//		/// </summary>
//		/// <returns>
//		/// An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
//		/// </returns>
//		IEnumerator IEnumerable.GetEnumerator()
//		{
//			return this.list.GetEnumerator();
//		}
//	}
//}
