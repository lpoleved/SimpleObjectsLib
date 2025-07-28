using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars.Ribbon;

namespace Simple.Objects.ServerMonitor.Interfaces
{
	public interface IFormMain
	{
		RibbonControl Ribbon { get; }

		BarToggleSwitchItem BarToggleSwitchItemDarkMode { get; }

		BarToggleSwitchItem BarToggleSwitchItemCompactView { get; }
	}
}
