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
	[SystemRequestArgs((int)SystemRequest.GetProtocolVersion)]
	public partial class EditPanelGetProtocolVersion : EditPanelSessionPackageRequestResponse
	{
		public EditPanelGetProtocolVersion()
		{
			InitializeComponent();
		}

		protected override void OnRefreshBindingObject()
		{
			base.OnRefreshBindingObject();

			this.editorProtocolVersion.Text = (this.PackageInfoRow?.ResponsePackageInfo?.PackageArgs as ProtocolVersionResponseArgs)?.ProtocolVersion?.ToString() ?? String.Empty;
		}

		protected override ResponseArgs CreateResponseArgs() => new ProtocolVersionResponseArgs();
	}
}
