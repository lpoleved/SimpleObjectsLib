using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Nodes.Operations;

namespace Simple.Controls.TreeList
{
    public class SimpleTreeListNode : TreeListNode
    {
        #region |   Private Members   |
        
        private int expandedImageIndex = -1;
        private int checkBoxImageIndex = -1;
        //private bool extraChecked = false;
        private bool enabled = true;
        
        #endregion |   Private Members   |

        #region |   Constructors and Initialization   |
        
        public SimpleTreeListNode(int id, TreeListNodes owner)
            : base(id, owner)
        {
        }

        public SimpleTreeListNode(int id, TreeListNodes owner, object tag)
            : base(id, owner)
        {
            this.Tag = tag;
        }

        #endregion |   Constructors and Initialization   |

        #region |   Public Properties   |

        public new SimpleTreeListNodes Nodes
        {
            get { return base.Nodes as SimpleTreeListNodes; }
        }

        
        public new SimpleTreeList TreeList
        {
            get { return this.Nodes.TreeList; } // Some how base.TreeList is null, sometimes
        }

        /// <summary>
        /// Gets or sets the index of the checkbox state image displayed within the node.
        /// </summary>
        [DefaultValue(-1)]
        [Description("Gets or sets the index for the expanded state image.")]
        public int ExpandedImageIndex
        {
            get { return this.expandedImageIndex; }
            set 
            {
                if (this.expandedImageIndex != value)
                {
                    this.expandedImageIndex = value;
                    var tl = base.TreeList;
                    this.TreeList.InvalidateNode(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets the index of the checkbox state image displayed within the node.
        /// </summary>
        [DefaultValue(-1)]
        [Description("Gets or sets the index of the checkbox state image displayed within the node.")]
        public int CheckBoxImageIndex
        {
            get { return this.checkBoxImageIndex; }
            set 
            {
                if (this.checkBoxImageIndex != value)
                {
                    this.checkBoxImageIndex = value;
                    this.TreeList.InvalidateNode(this);
                }
            }
        }

        ///// <summary>
        ///// Gets or sets the index of the checkbox state image displayed within the node.
        ///// </summary>
        //[DefaultValue(false)]
        //[Description("Gets or sets extra checked property for custom special purposes.")]
        //public bool ExtraChecked
        //{
        //    get { return this.extraChecked; }
        //    set 
        //    {
        //        if (this.extraChecked != value)
        //        {
        //            this.extraChecked = value;
        //            this.TreeList.InvalidateNode(this);
        //        }
        //    }
        //}

        /// <summary>
        /// Gets or sets enabled property.
        /// </summary>
        [DefaultValue(true)]
        [Description("Gets or sets enabled property.")]
        public bool Enabled
        {
            get { return this.enabled; }
            set 
            {
                if (this.enabled != value)
                {
                    this.enabled = value;
                    this.TreeList.InvalidateNode(this);
                }
            }
        }
        #endregion |   Public Properties   |

        #region |   Count Checked Children   |
        
        //private int checkedNodesCount = 0;

        //public int CheckedNodesCount
        //{
        //    get { return checkedNodesCount; }
        //}

        //public bool AllNodesChecked
        //{
        //    get { return this.CheckedNodesCount == this.Nodes.Count; }
        //}

        //protected internal void SetCheckedNodesCount(int checkedNodesCount)
        //{
        //    this.checkedNodesCount = checkedNodesCount;
        //}

        #endregion |   Count Checked Children   |

        #region |   Protected Method Overrides   |
        
        //#if IncludeNodesEditorCode 


        protected override TreeListNodes CreateNodes(DevExpress.XtraTreeList.TreeList treeList)
        {
            //treeList.NodesIterator.DoOperation(new TestOp());
            return new SimpleTreeListNodes(treeList, this);
        }

        //#endif

        #endregion |   Protected Method Overrides   |
    }

    //public class TestOp : TreeListOperation
    //{

    //    public override void Execute(TreeListNode node)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
