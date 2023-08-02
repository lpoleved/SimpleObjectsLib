using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ProtoBase;
using Simple.SocketEngine;
using Simple.Objects.SocketProtocol;

namespace Simple.Objects.ServerMonitor
{
	public class SessionInfoRow : ISimpleObjectSession
	{
		private Encoding characterEncoding;
		private object context;

		private const string strConnected = "Connected";
		private const string strDisconnected = "Disconnected";
		private const string strAuthorized = "Authorized";

		private bool connected = false;
		private bool authorized = false;

		public SessionInfoRow(int rowIndex, long sessionKey, string serverAddress, int serverPort, string clientAddress, int clientPort, string username, bool connected, Encoding characterEncoding, object context)
		{
			this.characterEncoding = characterEncoding;
			this.context = context;
			
			this.RowIndex = rowIndex;
			this.SessionKey = sessionKey;
			this.ServerAddress = serverAddress + ":" + serverPort;
			this.ClientAddress = clientAddress + ":" + clientPort;
			this.CharacterEncoding = characterEncoding.ToString() ?? UTF8Encoding.Default.ToString() ?? "Unknown";
			this.Username = username;
			this.Connected = connected;
			this.Status = String.Empty;
		}

		//public string SessionID { get; private set; }
		public int RowIndex { get; private set; }
		public long SessionKey { get; private set; }
		public string ServerAddress { get; private set; }
		public string ClientAddress { get; private set; }
		public string CharacterEncoding { get; private set; }
		public long UserId { get; set; }
		public string Username { get; set; }
		public string Status { get; set; }

		internal bool Authenticated { get; set; }

		internal bool Connected
		{
			get { return this.connected; }

			set
			{
				this.connected = value;
				this.UpdateStatus();
			}
		}

		internal bool Authorized
		{
			get { return this.authorized; }

			set
			{
				this.authorized = value;
				this.UpdateStatus();
			}

		}

		private void UpdateStatus()
		{
			this.Status = (this.Connected) ? (this.Authorized) ? strConnected + ", " + strAuthorized : strConnected 
										   : strDisconnected;
		}

		ServerObjectModelInfo? ISimpleObjectSession.GetServerObjectModel(int tableId)
		{
			if (this.context is FormMain formMain)
				return formMain.GetServerObjectModel(tableId);

			return null;
		}

		void ISimpleObjectSession.SetServerObjectModel(int tableId, ServerObjectModelInfo? serverObjectModelInfo) { } // Do nothing 

		Encoding ISimpleSession.CharacterEncoding => this.characterEncoding;
		bool ISimpleSession.IsConnected => this.connected;
		bool ISimpleSession.IsAuthenticated => this.Authenticated;
		ValueTask ISimpleSession.SendAsync(ReadOnlyMemory<byte> data) => throw new NotImplementedException("SendAsync is not supported in this context");

		ValueTask ISimpleSession.SendAsync<TPackage>(IPackageEncoder<TPackage> packageEncoder, TPackage package) => throw new NotImplementedException("SendAsync is not supported in this context");
		void ISimpleSession.ResponseIsReceived(PackageReader response) => throw new NotImplementedException("ResponseIsReceived is not used in this context");
	}
}
