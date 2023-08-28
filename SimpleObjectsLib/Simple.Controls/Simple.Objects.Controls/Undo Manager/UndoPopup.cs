using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Simple.Objects.Controls
{
	public partial class UndoPopup : XtraUserControl
	{
		private int index = -1;
		private int lastIndex = -1;

		public UndoPopup()
		{
			InitializeComponent();
		}

		public int SelectedCount
		{
			get { return this.index + 1; }
		}

		public void ResetIndexes()
		{
			this.index = -1;
			this.lastIndex = this.index;
		}
		public void BestFit()
		{
			var viewInfo = this.ListBox.GetViewInfo();
			Size listBoxMinSize;
			Size labelMinSize = this.Label.CalcBestSize();

			using (Graphics gr = this.ListBox.CreateGraphics())
				listBoxMinSize = viewInfo.CalcBestFit(gr);

			this.Width = Math.Max(listBoxMinSize.Width, labelMinSize.Width + 30);
			this.Width = Math.Min(this.Width, 5000); // Limit width to 50000
			this.Height = Math.Min(listBoxMinSize.Height + this.Label.Height + 30, 400);
			this.Label.Width = this.Width;
		}

		private void ListBox_MouseMove(object sender, MouseEventArgs e)
		{
			index = this.ListBox.IndexFromPoint(e.Location);
			//index = Math.Max(1, this.ListBox.IndexFromPoint(e.Location) + 1);

			if (e.Location.Y == 0) // || e.Location.Y < this.ListBox.Top)
				index = -1;

			this.SetSelection(e.Delta);
		}

		private void ListBox_MouseLeave(object sender, EventArgs e)
		{
			this.index = -1;
			this.SetSelection(0);
		}

		private void SetSelection(int delta)
		{
			if (index != lastIndex)
			{
				int topIndex = Math.Max(-1, Math.Min(this.ListBox.TopIndex + delta, this.ListBox.Items.Count - 1));

				this.ListBox.BeginUpdate();

				if (index > lastIndex)
				{
					for (int i = lastIndex + 1; i <= index; ++i)
						this.ListBox.SetSelected(i, true);
				}
				else
				{
					for (int i = index + 1; i <= lastIndex; ++i)
						this.ListBox.SetSelected(i, false);
				}

				this.lastIndex = index;
				this.ListBox.TopIndex = topIndex;

				this.ListBox.EndUpdate();
			}
			else if (index == -1)
			{
				this.ListBox.UnSelectAll();
			}
		}
	}
}
