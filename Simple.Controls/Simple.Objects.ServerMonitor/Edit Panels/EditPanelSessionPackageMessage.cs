using DevExpress.DataAccess.Native.Web;
using DevExpress.DataProcessing;
using Simple;
using Simple.Controls;
using Simple.Objects;
using Simple.Objects.SocketProtocol;
using Simple.Serialization;
using Simple.SocketEngine;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Simple.Objects.ServerMonitor
{
	public partial class EditPanelSessionPackageMessage : EditPanelPackageInfo
	{
		public EditPanelSessionPackageMessage()
		{
			InitializeComponent();
		}

		//public PackageInfo MessagePackage { get; private set; }
		//public MessageArgs MessageArgs { get; private set; }

		//protected override void OnBindingObjectChange(object? bindingObject, object? oldBindingObject)
		//{
		//	this.OnRefreshBindingObject();
		//	base.OnBindingObjectChange(bindingObject, oldBindingObject);
		//}

		public static int LargePackageBufferSize = 4096;

		protected override void OnRefreshBindingObject(object? requester)
		{
			base.OnRefreshBindingObject(requester);

			//this.MessagePackage = (packageInfoRow.SessionPackageJobAction.JobActionType == Simple.SocketEngine.JobActionType.MessageSent) ? packageInfoRow.SessionPackageJobAction.SentPackage :
			//																																  packageInfoRow.SessionPackageJobAction.ReceivedPackage;

			if (this.PackageInfoRow != null)
			{
				ISimpleObjectSession? session = this.Context?.GetSimpleObjectSession(this.PackageInfoRow.SessionKey);
				var message = this.PackageInfoRow.RequestOrMessagePackageReader;
				int packageKey = message.PackageInfo.Key;
				string keyAppInfo = (message.PackageInfo.HeaderInfo.IsSystem) ? "System" : "App";
				string keyDescription = String.Empty;

				if (message.PackageInfo.HeaderInfo.IsSystem)
					keyDescription = ((SystemMessage)packageKey).ToString();

				this.tabPageObjectName.Text = String.Format("Socket Package Massage  -  {0}.{1} ({2})", keyAppInfo, keyDescription, packageKey);
				this.editorMessageCodeOrRequestId.Text += $"  ({keyAppInfo}.{keyDescription})";
				this.editorMessagePackageLength.Text = message.PackageLength.ToString() + " Bytes";
				this.editorMessagePackageHeaderValue.Text = "0x" + message.PackageInfo.HeaderInfo.Value.ToString("X4");
				this.packageHeaderControlMessage.SetPackageHeaderValues(message.PackageInfo.HeaderInfo);

				if (message.PackageInfo.Buffer?.Length < LargePackageBufferSize)
				{
					this.editorMessagePackageValue.Text = this.CreatePackageText(message);
				}
				else
				{
					// this.LargePackageHexTextVisualizer.Post(new PackageHexTextVisualInfo(request, this.editorRequestPackageValue));

					Thread setPackageHexText = new Thread(() =>
					{
						string text = this.CreatePackageText(message);

						this.SetPackageTextToControl(this.editorMessagePackageValue, text);
					});

					setPackageHexText.IsBackground = true;
					setPackageHexText.Priority = ThreadPriority.Normal;
					//setPackageHexText.SetApartmentState(ApartmentState.STA);
					setPackageHexText.Start();
				}


				if (message.PackageInfo.PackageArgs is null && session != null)
				{
					message.PackageInfo.PackageArgs = this.CreateMessageArgs();

					if (message.PackageInfo.PackageArgs != null)
					{
						try
						{
							message.DecodePackageArgs(session);
						}
						catch (Exception ex)
						{
							Debug.WriteLine($"Error reading response args: {ex} (RequestId={message.PackageInfo.Key})");
						}


						//SequenceReader reader = new SequenceReader(message.Buffer);

						//try
						//{
						//	reader.AdvancePosition(message.PackageLengthInfoSize + message.HeaderSize);
						//	message.ReadArgs(messageArgs, ref reader, session: session!);
						//}
						//catch (Exception)
						//{
						//}
					}
				}

				//this.editorMessagePackageValue.Text = BitConverter.ToString(message.Buffer);
			}
		}

		protected virtual MessageArgs? CreateMessageArgs() => default;
	}
}
