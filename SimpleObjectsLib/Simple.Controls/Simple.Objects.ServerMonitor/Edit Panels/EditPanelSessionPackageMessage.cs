using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Buffers;
using Simple;
using Simple.Serialization;
using Simple.Controls;
using Simple.Objects;
using Simple.SocketEngine;
using Simple.Objects.SocketProtocol;
using System.Threading;
using DevExpress.DataProcessing;

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

		protected override void OnRefreshBindingObject()
		{
			base.OnRefreshBindingObject();

			//this.MessagePackage = (packageInfoRow.SessionPackageJobAction.JobActionType == Simple.SocketEngine.JobActionType.MessageSent) ? packageInfoRow.SessionPackageJobAction.SentPackage :
			//																																  packageInfoRow.SessionPackageJobAction.ReceivedPackage;

			if (this.PackageInfoRow != null)
			{
				PackageInfo message = this.PackageInfoRow.RequestOrMessagePackageInfo;
				int packageKey = message.Key;
				string keyAppInfo = (message.HeaderInfo.IsSystem) ? "System" : "App";
				string keyDescription = String.Empty;

				if (message.HeaderInfo.IsSystem)
					keyDescription = ((SystemMessage)packageKey).ToString();

				this.tabPageObjectName.Text = String.Format("Socket Package Massage  -  {0}.{1} ({2})", keyAppInfo, keyDescription, packageKey);
				this.editorMessageCodeOrRequestId.Text += $"  ({keyAppInfo}.{keyDescription})";
				this.editorMessagePackageLength.Text = message.PackageLength.ToString() + " Bytes";
				this.editorMessagePackageHeaderValue.Text = "0x" + message.HeaderInfo.Value.ToString("X4");
				this.packageHeaderControlMessage.SetPackageHeaderValues(message.HeaderInfo);

				if (message.Buffer.Length < LargePackageBufferSize)
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

				if (message.PackageArgs is null)
				{
					MessageArgs? messageArgs = this.CreateMessageArgs();

					if (messageArgs != null)
					{
						SequenceReader reader = new SequenceReader(message.Buffer);
						ISimpleObjectSession? session = this.Context?.GetSimpleObjectSession(this.PackageInfoRow.SessionKey);

						try
						{
							reader.AdvancePosition(message.PackageLengthSize + message.HeaderSize);
							message.ReadArgs(messageArgs, ref reader, session: session!);
						}
						catch (Exception)
						{
						}
					}
				}

				//this.editorMessagePackageValue.Text = BitConverter.ToString(message.Buffer);
			}
		}

		protected virtual MessageArgs? CreateMessageArgs() => default;
	}
}
