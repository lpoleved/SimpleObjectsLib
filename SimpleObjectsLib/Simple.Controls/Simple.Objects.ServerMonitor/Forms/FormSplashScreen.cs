using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace Simple.Objects.ServerMonitor
{
    public partial class FormSplashScreen : Simple.Controls.FormSplashScreen
	{
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// FormSplashScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(283, 367);
			this.Name = "FormSplashScreen";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
	}
}
