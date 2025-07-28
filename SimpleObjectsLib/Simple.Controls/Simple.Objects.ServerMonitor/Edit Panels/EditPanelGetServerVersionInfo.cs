using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simple.Objects.SocketProtocol;
using Simple.SocketEngine;

namespace Simple.Objects.ServerMonitor
{
	[SystemRequestArgs((int)SystemRequest.GetServerVersionInfo)]
	public partial class EditPanelGetServerVersionInfo : EditPanelSessionPackageRequestResponse
	{
		public EditPanelGetServerVersionInfo()
		{
			InitializeComponent();
		}

		protected override void OnRefreshBindingObject()
		{
			base.OnRefreshBindingObject();

			this.editorSystemServerVersion.Text = (this.PackageInfoRow?.ResponsePackageInfo?.PackageArgs as ServerVersionInfoResponseArgs)?.SystemServerVersion?.ToString() ?? String.Empty;
			this.editorAppServerVersion.Text = (this.PackageInfoRow?.ResponsePackageInfo?.PackageArgs as ServerVersionInfoResponseArgs)?.AppServerVersion?.ToString() ?? String.Empty;
		}

		protected override ResponseArgs CreateResponseArgs() => new ServerVersionInfoResponseArgs();
	}
}
