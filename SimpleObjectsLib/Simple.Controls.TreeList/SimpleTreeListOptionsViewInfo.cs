using DevExpress.Utils.Controls;
using DevExpress.XtraTreeList;
using System.ComponentModel;

namespace Simple.Controls.TreeList
{
    public class SimpleTreeListOptionsView : TreeListOptionsView
    {
        private bool allowHorzontalMerge;

        public SimpleTreeListOptionsView(SimpleTreeList treeList)
            : base(treeList)
        {
            this.allowHorzontalMerge = false;
        }

        public override void Assign(BaseOptions options)
        {
            base.Assign(options);
            
            SimpleTreeListOptionsView optView = options as SimpleTreeListOptionsView;
            
            if (optView == null)
                return;

            this.allowHorzontalMerge = optView.AllowHorizontalMerge;
        }

        [DefaultValue(false)]
        public bool AllowHorizontalMerge
        {
            get { return this.allowHorzontalMerge; }
            set { this.allowHorzontalMerge = value; }
        }
    }
}