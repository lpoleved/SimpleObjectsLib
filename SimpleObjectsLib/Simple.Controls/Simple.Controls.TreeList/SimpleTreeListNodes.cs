//#if IncludeNodesEditorCode 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design.Serialization;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Simple.Controls.TreeList.Designer.Serializers;

namespace Simple.Controls.TreeList
{
    /// <summary>
    /// Represents a collection of DevExpress.XtraTreeList.Nodes.TreeListNode objects
    /// in the DevExpress.XtraTreeList.TreeList component.
    /// </summary>
    [DesignerSerializer(typeof(SimpleTreeListNodesCodeDomSerializer), typeof(CodeDomSerializer))]
     public class SimpleTreeListNodes : TreeListNodes
    {
        /// <summary>
        /// Creates a collection of nodes at the root level of the tree list.
        /// </summary>
        /// <param name="treeList">A DevExpress.XtraTreeList.TreeList instance to which the node collection belongs.</param>
        public SimpleTreeListNodes(DevExpress.XtraTreeList.TreeList treeList)
            : base (treeList)
        {
        }

        /// <summary>
        /// Creates a collection of child nodes for a specific parent node.
        /// </summary>
        /// <param name="treeList">A DevExpress.XtraTreeList.TreeList class instance to which the collection belongs.</param>
        /// <param name="parentNode">The node for which to create a collection of child nodes.</param>
        public SimpleTreeListNodes(DevExpress.XtraTreeList.TreeList treeList, TreeListNode parentNode)
            : base(treeList, parentNode)
        { 
        }

        public new SimpleTreeList TreeList
        {
            get { return base.TreeList as SimpleTreeList; }
        }
    }
}
//#endif