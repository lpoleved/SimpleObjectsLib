using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Simple.Controls.TreeList
{
    public partial class SimpleTreeListWithColumnGroup : XtraUserControl
    {
        public SimpleTreeListWithColumnGroup()
        {
            InitializeComponent();
        }

        public SimpleTreeList TreeList
        {
            get { return this.treeList; }
        }

        public SimpleTreeList ColumnGroup
        {
            get { return this.columnGroup; }
        }
    }
}
