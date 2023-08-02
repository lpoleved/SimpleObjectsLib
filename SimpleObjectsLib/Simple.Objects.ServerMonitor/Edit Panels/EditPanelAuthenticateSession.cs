using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Simple.Objects.Services;
using Simple.Objects.SocketProtocol;
using Simple.SocketEngine;

namespace Simple.Objects.ServerMonitor
{
	[SystemRequestArgs((int)SystemRequest.AuthenticateSession)]
	public partial class EditPanelAuthenticateSession : EditPanelSessionPackageRequestResponse
	{
		public EditPanelAuthenticateSession()
		{
			InitializeComponent();
		}

		protected override void OnRefreshBindingObject()
		{
			base.OnRefreshBindingObject();

			PackageInfo? request = this.PackageInfoRow?.RequestOrMessagePackageInfo;
			PackageInfo? response = this.PackageInfoRow?.ResponsePackageInfo;
			bool? isAuthenticatedNullable = (response?.PackageArgs as AuthenticateSessionResponseArgs)?.IsAuthenticated;
			bool isAuthenticated = isAuthenticatedNullable ?? false;


			this.labelControlUserId.Visible = isAuthenticated;
			this.editorUserId.Visible = isAuthenticated;
			this.labelControlUsername2.Visible = isAuthenticated;
			this.editorUsername2.Visible = isAuthenticated;

			this.editorUsername.Text = (request?.PackageArgs as AuthenticateSessionRequestArgs)?.Username ?? String.Empty;
			this.editorPassword.Text = (request?.PackageArgs as AuthenticateSessionRequestArgs)?.PasswordHash ?? String.Empty;


			this.editorIsAuthenticated.Text = isAuthenticated.ToString();

			//if (isAuthenticated)
			//{
			//	this.editorUsername2.Text = this.Context?.GetSessionInfoRow(this.PackageInfoRow.SessionKey).Username; // ResponseArgs as AuthenticateSessionResponseArgs)?.Username ?? String.Empty;
			//	this.editorUserId.Text = (this.PackageInfoRow?.ResponseArgs as AuthenticateSessionResponseArgs)?.UserId.ToString();
			//}
		}

		protected override RequestArgs CreateRequestArgs() => new AuthenticateSessionRequestArgs();

		protected override ResponseArgs CreateResponseArgs() => new AuthenticateSessionResponseArgs();
	}
}
