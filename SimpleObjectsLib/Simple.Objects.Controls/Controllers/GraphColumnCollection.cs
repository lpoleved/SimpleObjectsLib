using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Collections;

namespace Simple.Objects.Controls
{
    public class GraphColumnCollection : IEnumerable<GraphColumn>, IEnumerable
    {
		private HashArray<GraphColumn?> array = new HashArray<GraphColumn?>();
		private List<GraphColumn> list = new List<GraphColumn>();
        private IGraphController graphController;

		public GraphColumnCollection(IGraphController graphController)
        {
            this.graphController = graphController;
        }

        public GraphColumn? this[int index]
        {
            get { return this.array[index]; }
        }

		//public GraphColumn this[string name]
		//{
		//	get
		//	{
		//		return this.FirstOrDefault(item => item.Name == name);
		//	}
		//}
        
        public int Count
        {
            get { return this.list.Count; }
        }

        public void Load<TEnum>()
        {
            if (typeof(TEnum).IsEnum)
            {
				string[] columnNames = Enum.GetNames(typeof(TEnum));

				this.array = new HashArray<GraphColumn?>(columnNames.Length + 1);

				foreach (string columnName in columnNames)
                {
                    GraphColumn graphColumn = new GraphColumn(columnName);
                    this.Add(graphColumn);
                }
	        }
        }
        public int Add(GraphColumn graphColumn)
        {
			graphColumn.Index = this.Count;
			graphColumn.IsNew = false;
            graphColumn.GraphController = this.graphController;

			this.array[graphColumn.Index] = graphColumn;
			this.list.Add(graphColumn);

			return graphColumn.Index;
        }

        public bool Remove(GraphColumn graphColumn)
        {
			bool isRemoved = this.list.Remove(graphColumn);

			if (isRemoved)
				this.array[graphColumn.Index] = null;

            return isRemoved;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns> A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection. </returns>
        IEnumerator<GraphColumn> IEnumerable<GraphColumn>.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }
    }
}
