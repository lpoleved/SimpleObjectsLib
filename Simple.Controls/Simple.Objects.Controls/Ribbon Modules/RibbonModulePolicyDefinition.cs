using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects.Controls
{
    public class RibbonModulePolicyDefinitionTemplate<T> : RibbonModulePolicyDefinitionTemplate
        where T : RibbonModulePolicyDefinitionTemplate<T>, new()
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
    
    public class RibbonModulePolicyDefinitionTemplate
    {
        private List<RibbonModuleModel> ribbonModuleModelList = new List<RibbonModuleModel>();
        private List<RibbonModuleGroupModel> ribbonModuleModelGroupList = new List<RibbonModuleGroupModel>();

        public List<RibbonModuleModel> RibbonModuleModels
        {
            get { return this.ribbonModuleModelList; }
        }

        public List<RibbonModuleGroupModel> RibbonModuleGroupModels
        {
            get { return this.ribbonModuleModelGroupList; }
        }

        public void AddRibbonModuleModel(RibbonModuleModel ribbonModuleModel)
        {
            this.RibbonModuleModels.Add(ribbonModuleModel);
        }

        public void AddRibbonModuleGroupModel(RibbonModuleGroupModel ribbonModuleGroupModel)
        {
            this.RibbonModuleGroupModels.Add(ribbonModuleGroupModel);
        }
    }
}
