using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects.Controls
{
    public class RibbonModuleModel
    {
        public RibbonModuleModel(Type ribbonModuleType, int graphKey, string name)
			: this(ribbonModuleType, graphKey, name, name)
        {
        }

        public RibbonModuleModel(Type ribbonModuleType, int graphKey, string name, string caption)
        {
            this.RibbonModuleType = ribbonModuleType;
            this.GraphKey = graphKey;
            this.Name = name;
			this.Caption = caption;

            this.RibbonModuleGroups = new List<RibbonModuleGroupModel>();
        }
        
        public Type RibbonModuleType { get; private set; }
        public string Name { get; private set; }
        public string Caption { get; private set; }

        public List<RibbonModuleGroupModel> RibbonModuleGroups { get; set; }
		public int GraphKey { get; set; }
		
		private SimpleRibbonModulePanel RibbonModulePanel { get; set; }
    }
}
