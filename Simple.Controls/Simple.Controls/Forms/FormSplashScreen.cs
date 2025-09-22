using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace Simple.Controls
{
    public partial class FormSplashScreen : SplashScreen 
	{
        public FormSplashScreen() 
		{
            InitializeComponent();
			//pictureEdit2.Image = DevExpress.Utils.ResourceImageHelper.CreateImageFromResources("DevExpress.XtraEditors.SplashScreen.DemoSplashScreen.png", typeof(DemoSplashScreen).Assembly);
			this.labelControlCopyright.Text = "Copyright © " + DateTime.Now.Year;
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg) {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand 
		{
        }
    }
}
