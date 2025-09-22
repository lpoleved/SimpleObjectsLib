using Simple.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects.SocketProtocol
{
	public class SendSystemMessageArgs
	{
		public SendSystemMessageArgs(int messageCode, MessageArgs? messageArgs = null)
		{
			this.MessageCode = messageCode;
			this.MessageArgs = messageArgs;
		}

		public int MessageCode { get; private set; }
		public MessageArgs? MessageArgs { get; private set; }
	}
}
