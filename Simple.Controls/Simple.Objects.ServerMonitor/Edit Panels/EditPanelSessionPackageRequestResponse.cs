using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Simple;
using Simple.Controls;
using Simple.Objects;
using Simple.SocketEngine;
using Simple.Objects.SocketProtocol;
using Simple.Serialization;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace Simple.Objects.ServerMonitor
{
	public partial class EditPanelSessionPackageRequestResponse : EditPanelPackageInfo
	{
		public EditPanelSessionPackageRequestResponse()
		{
			InitializeComponent();
		}

		//public PackageInfo RequestPackage { get; private set; }
		//public ResponseArgs RequestArgs { get; private set; }
		//public PackageInfo ResponsePackage { get; private set; }
		//public ResponseArgs ResponseArgs { get; private set; }

		//protected override void OnBindingObjectChange(object oldBindingObject, object bindingObject)
		//{
		//	//this.OnRefreshBindingObject();
		//	base.OnBindingObjectChange(oldBindingObject, bindingObject);
		//}

		protected override void OnRefreshBindingObject(object? requester)
		{
			base.OnRefreshBindingObject(requester);

			if (this.PackageInfoRow != null)
			{
				ISimpleObjectSession? session = this.Context?.GetSimpleObjectSession(this.PackageInfoRow.SessionKey);
				var request = this.PackageInfoRow.RequestOrMessagePackageReader;
				var response = this.PackageInfoRow.ResponsePackageReader;
				int packageKey = request.PackageInfo.Key;
				string keyAppInfo = (request.PackageInfo.HeaderInfo.IsSystem) ? "System" : "App";
				string keyDescription = packageKey.ToString();

				if (request.PackageInfo.HeaderInfo.IsSystem)
					keyDescription = ((SystemRequest)packageKey).ToString();

				this.tabPageObjectName.Text = String.Format("Socket Package Request  -  {0}.{1} ({2})", keyAppInfo, keyDescription, packageKey);
				this.editorRequestPackageLength.Text = request.PackageLength.ToString() + " Bytes"; //?? String.Empty;
				this.editorRequestPackageHeaderValue.Text = "0x" + request.PackageInfo.HeaderInfo.Value.ToString("X4");
				this.packageControlRequestHeader.SetPackageHeaderValues(request.PackageInfo.HeaderInfo);
				this.editorMessageCodeOrRequestId.Text += $"  ({keyAppInfo}.{keyDescription})";
				//this.editorToken.Text = this.PackageInfoRow.Token.ToString();

				if (request.PackageInfo.PackageArgs is null && session != null)
				{
					request.PackageInfo.PackageArgs = this.CreateRequestArgs();

					try
					{
						request.DecodePackageArgs(session);
					}
					catch (Exception ex)
					{
						Debug.WriteLine($"Error reading request args: {ex} (RequestId={request.PackageInfo.Key})");
					}
				}

				if (request.PackageInfo.Buffer?.Length < EditPanelSessionPackageMessage.LargePackageBufferSize)
				{
					this.editorRequestPackageValue.Text = this.CreatePackageText(request);
				}
				else
				{
					// this.LargePackageHexTextVisualizer.Post(new PackageHexTextVisualInfo(request, this.editorRequestPackageValue));

					Thread setPackageHexText = new Thread(() =>
					{
						string text = this.CreatePackageText(request);

						this.SetPackageTextToControl(this.editorRequestPackageValue, text);
					});

					setPackageHexText.IsBackground = true;
					setPackageHexText.Priority = ThreadPriority.Normal;
					//setPackageHexText.SetApartmentState(ApartmentState.STA);
					setPackageHexText.Start();
				}

				this.editorResponsePackageLength.Text = response?.PackageLength.ToString() + " Bytes"; // .BaseStream.Length.ToString() + " Bytes";
				this.editorResponsePackageHeaderValue.Text = "0x" + response?.PackageInfo.HeaderInfo.Value.ToString("X4");
				this.packageHeaderControlResponse.SetPackageHeaderValues(response?.PackageInfo.HeaderInfo ?? new HeaderInfo(9));

				if (response != null && response.PackageInfo.PackageArgs is null && session != null)
				{
					response.PackageInfo.PackageArgs = this.CreateResponseArgs();

					try
					{
						response.DecodePackageArgs(session);
					}
					catch (Exception ex)
					{
						Debug.WriteLine($"Error reading response args: {ex} (RequestId={response.PackageInfo.Key})");
					}
				}

				if (response?.PackageInfo.Buffer?.Length < EditPanelSessionPackageMessage.LargePackageBufferSize)
				{
					this.editorResponsePackageValue.Text = this.CreatePackageText(response);
				}
				else if (response != null)
				{
					// this.LargePackageHexTextVisualizer.Post(new PackageHexTextVisualInfo(request, this.editorRequestPackageValue));

					Thread setPackageHexText = new Thread(() =>
					{
						string text = this.CreatePackageText(response);

						this.SetPackageTextToControl(this.editorResponsePackageValue, text);
					});

					setPackageHexText.IsBackground = true;
					setPackageHexText.Priority = ThreadPriority.Normal;
					//setPackageHexText.SetApartmentState(ApartmentState.STA);
					setPackageHexText.Start();
				}
			}
		}

		protected virtual RequestArgs? CreateRequestArgs() => default;

		protected virtual ResponseArgs? CreateResponseArgs() => default;
	}
}
