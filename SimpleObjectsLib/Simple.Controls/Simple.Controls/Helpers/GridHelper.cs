using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraGrid.Views.Grid;

namespace Simple.Controls
{
	public static class GridHelper
	{
		public static void SetViewOnlyMode(GridView gridView)
		{
			gridView.OptionsBehavior.Editable = false;
			gridView.OptionsBehavior.ReadOnly = true;

			gridView.OptionsSelection.EnableAppearanceFocusedRow = true;
			gridView.OptionsSelection.EnableAppearanceFocusedCell = true;
			gridView.OptionsView.ShowIndicator = false;

			gridView.Appearance.FocusedRow.Options.UseBackColor = true;
			gridView.Appearance.FocusedCell.Options.UseBackColor = true;

			gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
		}

		public static void SetSelectOnlyMode(GridView gridView)
		{
			gridView.OptionsBehavior.Editable = false;
			gridView.OptionsBehavior.ReadOnly = true;

			gridView.OptionsSelection.EnableAppearanceFocusedRow = true;
			gridView.OptionsSelection.EnableAppearanceFocusedCell = true;
			gridView.OptionsView.ShowIndicator = true;

			gridView.Appearance.FocusedRow.Options.UseBackColor = false;
			gridView.Appearance.FocusedCell.Options.UseBackColor = true;

			//gridView.Appearance.FocusedRow.BackColor = System.Drawing.Color.Blue;

			gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
		}
	}
}
