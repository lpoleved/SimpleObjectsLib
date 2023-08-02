using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Simple.Objects;

namespace Simple.Objects.Controls
{
	[EditPanelInfo(typeof(Folder))]
	public partial class EditPanelFolder : EditPanelNameDescription 
    {
        public EditPanelFolder()
        {
            InitializeComponent();
        }
    }
}
