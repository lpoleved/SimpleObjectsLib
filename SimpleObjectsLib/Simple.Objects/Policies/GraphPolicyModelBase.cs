using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Collections;
using System.Collections;
using Simple.Modeling;

namespace Simple.Objects
{
    public class GraphPolicyModelBase<T> : GraphPolicyModelBase
        where T : GraphPolicyModelBase<T>, new()
    {
        private static object instance = null;
        private static object lockObjectInstance = new object();

        public static T Instance
        {
            get { return GetInstance<T>(); }
        }

        protected static U GetInstance<U>() where U : new()
        {
            lock (lockObjectInstance)
            {
                if (instance == null)
                {
                    instance = new U();
                }
            }

            return (U)instance;
        }
    }
    
    public class GraphPolicyModelBase : ModelElement
    {
        public GraphPolicyModelBase()
        {
            this.GraphPolicyModelsByGraphKey = this.CreateModelDictionary<int, SingleGraphPolicyModel>(this, singleGraphPolicyModel => singleGraphPolicyModel.GraphKey);
        }

        public ModelDictionary<int, SingleGraphPolicyModel> GraphPolicyModelsByGraphKey
        {
            get { return this.GetModelDictionary<int, SingleGraphPolicyModel>(); }
            set { this.SetModelDictionary<int, SingleGraphPolicyModel>(value); }
        }
    }
}
