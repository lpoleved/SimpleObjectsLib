using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Simple;
using Simple.SocketEngine;

namespace Simple.Objects.ServerMonitor
{
	public partial class PackageHeaderInfoControl : XtraUserControl
	{
		public PackageHeaderInfoControl()
		{
			InitializeComponent();
		}

		public void SetPackageHeaderValues(HeaderInfo headerInfo)
		{
			this.editorPackageType.Text = headerInfo.PackageType.ToString();
			this.editorRecipientModel.Text = headerInfo.RecipientModel.ToString().InsertSpaceOnUpperChange();
			this.editorIsSystem.Text = (headerInfo.IsSystem) ? "True" : "False";
			this.editorValue.Text = headerInfo.Value.ToString();

			if (headerInfo.PackageType == PackageType.Response)
			{
				this.labelResponseSucceeded.Visible = true;
				this.editorResponseSucceeded.Visible = true;
				this.editorResponseSucceeded.Text = (headerInfo.ResponseSucceed) ? "True" : "False";
			}
			else
			{
				this.labelResponseSucceeded.Visible = false;
				this.editorResponseSucceeded.Visible = false;
			}
		}
	}
}
