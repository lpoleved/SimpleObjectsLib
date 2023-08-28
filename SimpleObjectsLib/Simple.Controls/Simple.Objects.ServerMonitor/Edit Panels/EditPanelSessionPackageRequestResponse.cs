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

		protected override void OnRefreshBindingObject()
		{
			base.OnRefreshBindingObject();

			if (this.PackageInfoRow != null)
			{
				ISimpleObjectSession? session = this.Context?.GetSessionContext(this.PackageInfoRow.SessionKey);
				PackageInfo request = this.PackageInfoRow.RequestOrMessagePackageInfo;
				PackageInfo response = this.PackageInfoRow.ResponsePackageInfo!;
				int packageKey = request.Key;
				string keyAppInfo = (request.HeaderInfo.IsSystem) ? "System" : "App";
				string keyDescription = packageKey.ToString();

				if (request.HeaderInfo.IsSystem)
					keyDescription = ((SystemRequest)packageKey).ToString();

				this.tabPageObjectName.Text = String.Format("Socket Package Request  -  {0}.{1} ({2})", keyAppInfo, keyDescription, packageKey);
				this.editorRequestPackageLength.Text = request.PackageLength.ToString() + " Bytes"; //?? String.Empty;
				this.editorRequestPackageHeaderValue.Text = "0x" + request.HeaderInfo.Value.ToString("X4");
				this.packageControlRequestHeader.SetPackageHeaderValues(request.HeaderInfo);
				this.editorMessageCodeOrRequestId.Text += $"  ({keyAppInfo}.{keyDescription})";
				//this.editorToken.Text = this.PackageInfoRow.Token.ToString();

				if (request.Buffer.Length < EditPanelSessionPackageMessage.LargePackageBufferSize)
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

				if (request.PackageArgs is null)
				{
					RequestArgs? requestArgs = this.CreateRequestArgs();

					if (requestArgs != null)
					{
						SequenceReader reader = new SequenceReader(request.Buffer);

						try
						{
							reader.AdvancePosition(request.PackageLengthSize + request.HeaderSize);
							request.ReadArgs(requestArgs, ref reader, session: session!);
						}
						catch (Exception ex)
						{
							Debug.WriteLine($"Error reading request args: {ex} (RequestId={request.Key})");
						}
					}
				}


				this.editorResponsePackageLength.Text = response.PackageLength.ToString() + " Bytes"; // .BaseStream.Length.ToString() + " Bytes";
				this.editorResponsePackageHeaderValue.Text = "0x" + response.HeaderInfo.Value.ToString("X4");
				this.packageHeaderControlResponse.SetPackageHeaderValues(response.HeaderInfo);

				if (response.Buffer.Length < EditPanelSessionPackageMessage.LargePackageBufferSize)
				{
					this.editorResponsePackageValue.Text = this.CreatePackageText(response);
				}
				else
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

				if (response.PackageArgs is null)
				{
					ResponseArgs? responseArgs = this.CreateResponseArgs();

					if (responseArgs != null)
					{
						SequenceReader reader = new SequenceReader(response.Buffer);

						try
						{
							reader.AdvancePosition(response.PackageLengthSize + response.HeaderSize);
							response.ReadArgs(responseArgs, ref reader, session: session!);
						}
						catch (Exception ex)
						{
							Debug.WriteLine($"Error reading response args: {ex} (RequestId={response.Key})");
						}
					}
				}
			}
		}

		protected virtual RequestArgs? CreateRequestArgs() => default;

		protected virtual ResponseArgs? CreateResponseArgs() => default;
	}
}
