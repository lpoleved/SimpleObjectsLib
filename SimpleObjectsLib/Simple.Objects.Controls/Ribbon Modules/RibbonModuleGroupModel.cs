using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects.Controls
{
    public class RibbonModuleGroupModel
    {
        public RibbonModuleGroupModel(string name)
            : this(name, name)
        {
        }

        public RibbonModuleGroupModel(string name, string caption)
        {
			this.Name = name;
			this.Caption = caption;
            this.RibbonModuleMembers = new List<RibbonModuleModel>();
        }
        
        public string Name { get; private set; }
        public string Caption { get; private set; }
        public List<RibbonModuleModel> RibbonModuleMembers { get; private set; }
        SimpleRibbonModulePanel RibbonModulePanel { get; set; }

        public void AddRibbonModuleModelMembers(params RibbonModuleModel[] ribbonModuleModelMembers)
        {
            foreach(RibbonModuleModel ribbonModuleModel in ribbonModuleModelMembers)
            {
                this.RibbonModuleMembers.Add(ribbonModuleModel);
                ribbonModuleModel.RibbonModuleGroups.Add(this);

            }
        }
    }
}
