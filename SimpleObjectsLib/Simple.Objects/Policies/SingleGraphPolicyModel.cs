using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Modeling;
using Simple.Collections;

namespace Simple.Objects
{
    public class SingleGraphPolicyModel : SingleGraphPolicyModelBase, IGraphPolicyModel
    {
        public SingleGraphPolicyModel()
        {
        }

        public SingleGraphPolicyModel(object graphKeyEnum)
            : base(graphKeyEnum)
        {
        }
    }

    public abstract class SingleGraphPolicyModelBase : ModelElement, IGraphPolicyModel
    {
        public SingleGraphPolicyModelBase()
        {
			this.PolicyElements = this.CreateModelDictionary<Type, GraphPolicyModelElement>(this, graphPolicyModelElement => graphPolicyModelElement.ObjectType);
        }

        public SingleGraphPolicyModelBase(object graphKeyEnum)
            : this()
        {
            if (graphKeyEnum != null && graphKeyEnum.GetType().IsEnum)
            {
                this.GraphKey = Conversion.TryChangeType<int>(graphKeyEnum);
                this.Name = graphKeyEnum.ToString();
            }
        }

        public int GraphKey { get; protected set; }
        public bool IsAnchorMode { get; set; }

        public ModelDictionary<Type, GraphPolicyModelElement> PolicyElements
        {
            get { return this.GetModelDictionary<Type, GraphPolicyModelElement>(); }
            protected set { this.SetModelDictionary<Type, GraphPolicyModelElement>(value); }
        }
 
		public IGraphPolicyModelElement? GetGraphPolicyModelElement(Type objectType)
		{
			GraphPolicyModelElement? result;

			if (objectType == null)
				return null;
			
			if (!this.PolicyElements.TryGetValue(objectType, out result))
				result = null;

			return result;
		}

        protected override void OnCreteModelCollectionElement<T>(string fieldName, T modelElement, ModelCollection<T> modelCollection)
        {
            base.OnCreteModelCollectionElement<T>(fieldName, modelElement, modelCollection);

            if (modelElement is GraphPolicyModelElement)
            {
                GraphPolicyModelElement graphPolicyModelElement = modelElement as GraphPolicyModelElement;

                if (graphPolicyModelElement.Priority == -1)
                {
                    int priority = modelCollection.Count > 1 ? (modelCollection[modelCollection.Count - 2] as GraphPolicyModelElement).Priority + 1 : 0;
                    graphPolicyModelElement.Priority = priority;
                }
            }
        }

		IDictionary<Type, IGraphPolicyModelElement> IGraphPolicyModel.PolicyElements
		{
			get { return this.GetModelDictionary<Type, GraphPolicyModelElement>().AsCustom<Type, IGraphPolicyModelElement>().AsReadOnly(); }
		}
    }

	public interface IGraphPolicyModel : IModelElement
	{
		int GraphKey { get; }
        bool IsAnchorMode { get; }
		IDictionary<Type, IGraphPolicyModelElement> PolicyElements { get; }
		IGraphPolicyModelElement? GetGraphPolicyModelElement(Type objectType);
	}
}
