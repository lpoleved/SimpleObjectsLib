using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Modeling;

namespace Simple.Objects
{
	public class GraphPolicyModelElement : ModelElement, IGraphPolicyModelElement
	{
		public GraphPolicyModelElement(Type objectType)
		{
			this.ObjectType = objectType;
			this.Priority = -1;
			this.SubTreePolicy = SubTreePolicy.DoNotAllowSubTree;
			this.CanBeInRoot = true;
			this.ParentAcceptablePriorities = new List<int>();
			this.ParentAcceptableTypes = new List<Type>(); 
		}

		public Type ObjectType { get; set; }
		public int Priority { get; set; }
		public bool IsAnchor { get; set; }

		public SubTreePolicy SubTreePolicy { get; set; }
		public bool CanBeInRoot { get; set; }

		public IEnumerable<int> ParentAcceptablePriorities { get; set; }
		public IEnumerable<Type> ParentAcceptableTypes { get; set; }


	}

	public interface IGraphPolicyModelElement : IModelElement
	{
		Type ObjectType { get; }
		int Priority { get; }
		bool IsAnchor { get; }
		SubTreePolicy SubTreePolicy { get; }
		bool CanBeInRoot { get; }
		IEnumerable<int> ParentAcceptablePriorities { get; }
		IEnumerable<Type> ParentAcceptableTypes { get; }
	}
}
