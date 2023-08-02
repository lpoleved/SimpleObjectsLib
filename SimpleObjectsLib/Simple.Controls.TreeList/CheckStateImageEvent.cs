using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace DevExpress.XtraTreeList
{
    // Summary:
    //     Represents a method that will handle the DevExpress.XtraTreeList.TreeList.GetCheckStateImage
    //     event of the DevExpress.XtraTreeList.TreeList class.
    public delegate void GetCheckStateImageEventHandler(object sender, GetCheckStateImageEventArgs e);

    // Summary:
    //     Provides data for the DevExpress.XtraTreeList.TreeList.GetCheckStateImage event.
    public class GetCheckStateImageEventArgs : GetStateImageEventArgs
    {
        // Summary:
        //     Initializes a new DevExpress.XtraTreeList.GetCheckStateImageEventArgs class instance.
        //
        // Parameters:
        //   node:
        //     A tree list node.
        //
        //   nodeImageIndex:
        //     The image index.
        public GetCheckStateImageEventArgs(TreeListNode node, int nodeImageIndex)
            : base(node, nodeImageIndex)
        { 
        }
    }
}
