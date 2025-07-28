using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Buffers;
using Simple.Serialization;
using Simple.SocketEngine;
using Simple.Objects.SocketProtocol;
using Simple.Objects.MonitorProtocol;
using DevExpress.CodeParser;
using DevExpress.XtraScheduler.Commands;
using DevExpress.XtraGauges.Core.Drawing;

namespace Simple.Objects.ServerMonitor
{
	public class PackageInfoRow
	{
		private PackageInfo packageInfo;
		private PackageInfo? responsePackageInfo = null;


		/// <summary>
		/// Collect summary of message information. If sessionKey is zero, it is brodcast message.
		/// Package args are not readed!
		/// </summary>
		/// <param name="rowIndex"></param>
		/// <param name="sessionKey"></param>
		/// <param name="messageBuffer"></param>
		/// <param name="username"></param>
		public PackageInfoRow(int rowIndex, long sessionKey, byte[] messageBuffer, string username, PackageAction action)
        {
			this.RowIndex = rowIndex;
			this.No = rowIndex;
			this.SessionKey = sessionKey;
			this.Username = username;
			this.Type = "Message";
			this.Event = action.ToString();
			
			this.packageInfo = new PackageInfo(messageBuffer);
			this.packageInfo.ReadPackageLength();
			this.packageInfo.ReadHeader();

			this.PackageKey = this.packageInfo.Key.ToString();
			//this.Token = this.packageInfo.Token;
			this.Length = messageBuffer.Length.ToString() + " Bytes"; // (responseRequestMessagePackageInfo.PackageLengthSize + responseRequestMessagePackageInfo.HeaderSize + this.PackageDetails.ArgsBuffer.Length).ToString();

			string keyAppInfo = (this.packageInfo.HeaderInfo.IsSystem) ? "System" : "App";
			string keyDescription = this.packageInfo.Key.ToString();

			if (this.packageInfo.HeaderInfo.IsSystem)
			{
				if (packageInfo.HeaderInfo.PackageType == PackageType.Message)
					keyDescription = ((SystemMessage)this.packageInfo.Key).ToString();
				else
					keyDescription = ((SystemRequest)this.packageInfo.Key).ToString();
			}

			this.PackageKey += $"  ({keyAppInfo}.{keyDescription})";
		}

		/// <summary>
		/// Collect summary of session request - response information.
		/// Package args are not readed!
		/// </summary>
		/// <param name="rowIndex">Grid view posiotion</param>
		/// <param name="sessionKey">Session key that uniqely identify the ssesion.</param>
		/// <param name="requestBuffer">The request package buffer data.</param>
		/// <param name="responseBuffer">The response package buffer data.</param>
		/// <param name="username">Session username (if authenticated)</param>
		/// <param name="action">The package action type</param>
		public PackageInfoRow(int rowIndex, long sessionKey, byte[] requestBuffer, byte[] responseBuffer, string username, PackageAction action)
			: this(rowIndex, sessionKey, requestBuffer, username, action)
		{
			this.Type = "Request";

			this.responsePackageInfo = new PackageInfo(responseBuffer);
			this.responsePackageInfo.ReadPackageLength();
			this.responsePackageInfo.ReadHeader();

			this.Length = this.packageInfo.PackageLength + this.packageInfo.PackageLengthSize + " + " +
						  (this.responsePackageInfo.PackageLength + this.responsePackageInfo.PackageLengthSize); // (responseRequestMessagePackageInfo.PackageLengthSize + responseRequestMessagePackageInfo.HeaderSize + this.PackageDetails.ArgsBuffer.Length).ToString();
			this.Length += " Bytes";
		}

		public int No { get; private set; }
		public string Type { get; private set; } = String.Empty;
		public string Event { get; private set; } = String.Empty;
		public long SessionKey { get; private set; } // SessionId for single session, SessionId list form multicast and the Brodcast keyword for brodcast
		public string Username { get; private set; }
		public string PackageKey { get; private set; } = "0"; // MessageCode or RequestId
		//public int Token { get; private set; } // Only for request - response
		public string Length { get; private set; } = "0";

		//
		// Internal properties are not visible in grid control row
		//
		internal int RowIndex { get; private set; }
		internal PackageInfo RequestOrMessagePackageInfo => this.packageInfo;
		internal PackageInfo? ResponsePackageInfo => this.responsePackageInfo;


		//internal MessageArgs? MessageArgs { get; set; }
		//internal RequestArgs? RequestArgs { get; set; }
		//internal ResponseArgs? ResponseArgs { get; set; }

	}
}
