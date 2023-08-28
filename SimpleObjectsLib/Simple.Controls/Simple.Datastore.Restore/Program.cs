using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Simple.Datastore.Controls;

namespace Simple.Datastore.Restore
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(AppContext.Instance.UserSettings.GetRibbonSkinName());

			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RestoreDatabaseForm());
        }
    }
}
