using DevExpress.Utils;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.ViewInfo;
using System;

namespace Simple.Controls.TreeList
{
    public class MergeRowCellsEventArgs : MergeEventArgs
    {

        public MergeRowCellsEventArgs(RowInfo rowInfo, int currCellIndex, int prevCellIndex)
        {
            this.Node = rowInfo.Node;
            this.CurrentColumn = rowInfo.Cells[currCellIndex].Column;
            this.PrevColumn = rowInfo.Cells[prevCellIndex].Column;
            this.CurrentCellDisplayText = this.Node.GetDisplayText(this.CurrentColumn);
            this.PrevCellDisplayText = this.Node.GetDisplayText(this.PrevColumn);
            this.Merge = DefaultBoolean.Default;
        }

        public string CurrentCellDisplayText { get; set; }
        public TreeListColumn CurrentColumn { get; private set; }
        public TreeListNode Node { get; private set; }
        public string PrevCellDisplayText { get; private set; }
        public TreeListColumn PrevColumn { get; private set; }
    }

    public class MergeColumnCellsEventArgs : MergeEventArgs
    {
        public MergeColumnCellsEventArgs(RowInfo rowInfo, int cellIndex)
        {
            this.CurrentNode = rowInfo.Node;
            this.PrevNode = rowInfo.Node.PrevNode;
            this.Column = rowInfo.Cells[cellIndex].Column;
            this.CurrCellVisibleText = this.CurrentNode.GetDisplayText(this.Column);
            this.PrevCellVisibleText = this.PrevNode.GetDisplayText(this.Column);
            this.Merge = DefaultBoolean.Default;
        }

        public TreeListColumn Column { get; private set; }
        public string CurrCellVisibleText { get; private set; }
        public TreeListNode CurrentNode { get; private set; }
        public string PrevCellVisibleText { get; private set; }
        public TreeListNode PrevNode { get; private set; }
    }

    public class MergeEventArgs : EventArgs
    {
        public DefaultBoolean Merge { get; set; }
    }
}
