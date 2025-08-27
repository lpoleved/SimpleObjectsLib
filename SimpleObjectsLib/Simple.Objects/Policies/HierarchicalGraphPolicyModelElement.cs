using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects
{
	public class HierarchicalGraphPolicyModelElement : GraphPolicyModelElement, IGraphPolicyModelElement
	{
		public HierarchicalGraphPolicyModelElement(Type objectType, int parentOneToManyRelationKey)
			: base(objectType)
		{
			this.ParentOneToManyRelationKey = parentOneToManyRelationKey;
		}

		public int ParentOneToManyRelationKey { get; private set; }
	}
}
