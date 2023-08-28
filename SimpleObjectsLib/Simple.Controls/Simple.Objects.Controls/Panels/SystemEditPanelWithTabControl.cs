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
	public partial class SystemEditPanelWithTabControl : SystemEditPanel
	{
		public SystemEditPanelWithTabControl()
		{
			InitializeComponent();
		}

		//protected override void OnInitializeEditors()
		//{
		//	this.tabControl.Location = new System.Drawing.Point(0, 0);
		//	this.tabControl.Size = new System.Drawing.Size(this.Size.Width + 1, this.Size.Height + 1);
		//}
	}
}
