using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Simple.Modeling;

namespace Simple.Objects
{
	public struct GraphElementObjectPair
	{
        public GraphElementObjectPair(long graphElementId, int simpleObjectTableId, long simpleObjectId, bool hasChildren, IEnumerable<PropertyIndexValuePair> simpleObjectPropertyIndexValues)
        {
			this.GraphElementId = graphElementId;
			//this.GraphKey = graphKey;
			//this.ParentId = parentId;
			this.SimpleObjectTableId = simpleObjectTableId;
			this.SimpleObjectId = simpleObjectId;
			this.HasChildren = hasChildren;
			this.SimpleObjectPropertyIndexValues = simpleObjectPropertyIndexValues;
        }

  //      public GraphElementObjectPair(IEnumerable<PropertyIndexValuePair> graphElementPropertyIndexValues, bool hasChildren, int simpleObjectTableId, IEnumerable<PropertyIndexValuePair> simpleObjectPropertyIndexValues)
		//{
		//	this.GraphElementPropertyIndexValues = graphElementPropertyIndexValues;
		//	this.HasChildren = hasChildren;
		//	this.SimpleObjectTableId = simpleObjectTableId;
		//	this.SimpleObjectPropertyIndexValues = simpleObjectPropertyIndexValues;
		//}

		public long GraphElementId { get; private set; }
		//public int GraphKey { get; private set; }
		//public long ParentId { get; private set; }
		public int SimpleObjectTableId { get; private set; }
		public long SimpleObjectId { get; private set; }

		//public IEnumerable<PropertyIndexValuePair> GraphElementPropertyIndexValues { get; private set; }
		public bool HasChildren { get; private set; }

		//public int SimpleObjectTableId { get; private set; }
		public IEnumerable<PropertyIndexValuePair> SimpleObjectPropertyIndexValues { get; private set; }
	}
}
