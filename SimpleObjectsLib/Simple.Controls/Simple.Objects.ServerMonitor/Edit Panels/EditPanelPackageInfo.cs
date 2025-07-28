using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks.Dataflow;
using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using Simple;
using Simple.Modeling;
using Simple.Objects;
using Simple.SocketEngine;
using Simple.Objects.SocketProtocol;
using Simple.Controls;
using Simple.Objects.Controls;
using DevExpress.XtraRichEdit.Import.OpenXml;

namespace Simple.Objects.ServerMonitor
{
	public partial class EditPanelPackageInfo : EditPanelServerMonitorBase
	{
		private string tabPageText;
		private ActionBlock<PackageHexTextVisualInfo>? largePackageHexTextVisualizer = null;

		public EditPanelPackageInfo()
		{
			InitializeComponent();
			this.tabPageText = this.tabPageObjectName.Text;
		}

		protected ActionBlock<PackageHexTextVisualInfo> LargePackageHexTextVisualizer => this.largePackageHexTextVisualizer ??= new ActionBlock<PackageHexTextVisualInfo>(arg =>
		{
			this.SetPackageTextToControl(arg.TextControl, this.CreatePackageText(arg.PackageInfo));
		});

		public PackageInfoRow? PackageInfoRow => base.BindingObject as PackageInfoRow;


		//protected PackageArgsFactory? PackageArgsFactory => base.Context as PackageArgsFactory;

		//protected override void OnBindingObjectChange(object? oldBindingObject, object? bindingObject)
		//{
		//	base.OnBindingObjectChange(oldBindingObject, bindingObject);
		//	this.OnRefreshBindingObject();
		//}

		protected override void OnRefreshBindingObject()
		{
			base.OnRefreshBindingObject();

			if (this.PackageInfoRow is null)
				return;

			//HeaderInfo headerInfo = this.PackageInfoRow.RequestOrMessagePackageInfo.HeaderInfo; // (MessagePackage != null) ? this.PackageInfoRow.MessagePackage.Flags : this.PackageInfoRow.RequestPackage.Flags;
			int keyValue = this.PackageInfoRow.RequestOrMessagePackageInfo.Key;

			this.tabPageObjectName.Text = this.tabPageText;
			//this.editorSessionId.Text = this.PackageInfoRow.SessionId;
			this.editorSessionKey.Text = this.PackageInfoRow.SessionKey.ToString();
			this.editorActionType.Text = this.PackageInfoRow.Type.InsertSpaceOnUpperChange().Replace("_", " -> ");
			this.editorUser.Text = this.PackageInfoRow.Username; //  this.Context?.GetSessionInfoRow(this.PackageInfoRow.SessionKey)?.Username;

			//switch (packageInfoRow.SessionPackageJobAction.JobActionType)
			//{
			//	case Simple.SocketEngine.JobActionType.RequestReceived_ResponseSent:
			//		this.

			//		break;

			//	case Simple.SocketEngine.JobActionType.MessageSent:
			//		break;

			//	case Simple.SocketEngine.JobActionType.RequestSent_ResponseReceived:
			//		break;

			//	case Simple.SocketEngine.JobActionType.MessageReceived:
			//		break;
			//}

			//if (packageInfoRow.SessionPackageJobAction.JobActionType == Simple.SocketEngine.JobActionType.RequestReceived_ResponseSent || 
			//	packageInfoRow.SessionPackageJobAction.JobActionType == Simple.SocketEngine.JobActionType.RequestSent_ResponseReceived)
			//if (header.IsSystem)
			//	keyDescription = ((SystemMessage)keyValue).ToString();

			this.editorMessageCodeOrRequestId.Text = keyValue.ToString(); // String.Format("{0}  ({1}.{2})", keyValue, keyAppInfo, keyDescription);
																		  //this.editorToken.Text = this.PackageInfoRow.Token.ToString();
																		  //if (packageInfoRow.SessionPackageJobAction.MessageRequestType == Simple.SocketEngine.MessageRequestType.RequestRecievedResponseSent)
																		  //{
																		  //	this.tabPageMessage.PageVisible = false;
																		  //	this.tabPageRequest.PageVisible = true;
																		  //	this.tabPageResponse.PageVisible = true;

			//	this.editorRequestFlags.Text = packageInfoRow.SessionPackageJobAction.ReceivedPackage.Flags.Value.ToString("X4");
			//	this.editorRequestLength.Text = packageInfoRow.SessionPackageJobAction.ReceivedPackage.Reader.BaseStream.Length.ToString() + " byte(s)";

			//	this.editorResponseFlags.Text = packageInfoRow.SessionPackageJobAction.SentPackage.Flags.Value.ToString("X4");
			//	this.editorResponseLength.Text = packageInfoRow.SessionPackageJobAction.SentPackage.Reader.BaseStream.Length.ToString() + " byte(s)";
			//}
			//else if (packageInfoRow.SessionPackageJobAction.MessageRequestType == Simple.SocketEngine.MessageRequestType.MessageSent)
			//{
			//	this.tabPageMessage.PageVisible = true;
			//	this.tabPageRequest.PageVisible = false;
			//	this.tabPageResponse.PageVisible = false;

			//	this.editorMessageFlags.Text = packageInfoRow.SessionPackageJobAction.SentPackage.Flags.Value.ToString("X4");
			//	this.editorMessageLength.Text = packageInfoRow.SessionPackageJobAction.SentPackage.Reader.BaseStream.Length.ToString() + " byte(s)";
			//}
			//else if (packageInfoRow.SessionPackageJobAction.MessageRequestType == Simple.SocketEngine.MessageRequestType.MessageReceived)
			//{
			//	this.tabPageMessage.PageVisible = true;
			//	this.tabPageRequest.PageVisible = false;
			//	this.tabPageResponse.PageVisible = false;

			//	this.editorMessageFlags.Text = packageInfoRow.SessionPackageJobAction.ReceivedPackage.Flags.Value.ToString("X4");
			//	this.editorMessageLength.Text = packageInfoRow.SessionPackageJobAction.ReceivedPackage.Reader.BaseStream.Length.ToString() + " byte(s)";
			//}
			//else if (packageInfoRow.SessionPackageJobAction.MessageRequestType == Simple.SocketEngine.MessageRequestType.RequestSentResponseReceived)
			//{
			//	this.tabPageMessage.PageVisible = false;
			//	this.tabPageRequest.PageVisible = true;
			//	this.tabPageResponse.PageVisible = true;

			//	this.editorRequestFlags.Text = packageInfoRow.SessionPackageJobAction.SentPackage.Flags.Value.ToString("X4");
			//	this.editorRequestLength.Text = packageInfoRow.SessionPackageJobAction.SentPackage.Reader.BaseStream.Length.ToString() + " byte(s)";

			//	this.editorResponseFlags.Text = packageInfoRow.SessionPackageJobAction.ReceivedPackage.Flags.Value.ToString("X4");
			//	this.editorResponseLength.Text = packageInfoRow.SessionPackageJobAction.ReceivedPackage.Reader.BaseStream.Length.ToString() + " byte(s)";
			//}
		}

		protected void SetPackageTextToControl(MemoEdit textControl, string text)
		{
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(() => this.OnSetPackageText(textControl, text)));
			else
				this.OnSetPackageText(textControl, text);
		}

		protected void OnSetPackageText(MemoEdit textControl, string text) => textControl.Text = text;


		protected string CreatePackageText(PackageInfo packageInfo)
		{
			string text;
			string requestIdOrMessageCodeText = (packageInfo.HeaderInfo.PackageType == PackageType.Message) ? "Message Code" : "Request Id";

			text = packageInfo.PackageLength.To7BitEncodedHexString() + "   <- Package length, " + this.GetByteText(packageInfo.PackageLengthSize) + "\r\n";
			text += $"  ==== Header, {this.GetByteText(packageInfo.HeaderSize)} ====r\n";
			text += packageInfo.HeaderInfo.Value.To7BitEncodedHexString(out int headerInfoSize) + "   <- Header Info, " + this.GetByteText(headerInfoSize) + "\r\n";
			text += packageInfo.Key.To7BitEncodedHexString(out int requestIdSize) + $"   <- {requestIdOrMessageCodeText}, " + this.GetByteText(requestIdSize) + "\r\n";

			//if (packageInfo.HeaderInfo.PackageType != PackageType.Message)
			//	text += packageInfo.Token.To7BitEncodedHexString(out int tokenSize) + "   <- Token, " + this.GetByteText(tokenSize) + "\r\n";
			long bodyLength = packageInfo.PackageLength - packageInfo.HeaderSize;

			text += $"  ==== Package Args Body, {this.GetByteText(bodyLength)} ====\r\n";

			if (bodyLength > 0)
				text += BitConverter.ToString(packageInfo.Buffer, startIndex: packageInfo.PackageLengthSize + packageInfo.HeaderSize);

			return text;
		}

		protected string GetByteText(int value, string keyword = " Byte") => this.GetByteText((long)value);

		protected string GetByteText(long value, string keyword = " Byte")
		{
			return value.ToString() + keyword + ((value != 1) ? "s" : "");
		}
	}
}
