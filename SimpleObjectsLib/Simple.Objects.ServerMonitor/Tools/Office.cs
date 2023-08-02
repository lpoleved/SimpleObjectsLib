using System.Drawing;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Utils;

namespace Simple.Objects.ServerMonitor
{
    public class Office2007PopupControlContainer : PopupControlContainer, ITransparentBackgroundManager {
        Color ITransparentBackgroundManager.GetForeColor(object childObject) {
            return GetForeColorCore();
        }
        Color ITransparentBackgroundManager.GetForeColor(Control childControl) {
            return GetForeColorCore();
        }
        protected Color GetForeColorCore() {
            return BarUtilites.GetAppMenuLabelForeColor(LookAndFeel.ActiveLookAndFeel);
        }
    }
}
