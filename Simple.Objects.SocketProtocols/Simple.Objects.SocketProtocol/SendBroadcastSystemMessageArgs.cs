using Simple.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects.SocketProtocol
{
	public class SendBroadcastSystemMessageArgs : SendSystemMessageArgs
	{
		public SendBroadcastSystemMessageArgs(int messageCode, MessageArgs? messageArgs = null, SimpleSession? exceptionSession = null)
			: base(messageCode, messageArgs)
		{
			this.ExceptionSession = exceptionSession;
		}

		public SimpleSession? ExceptionSession { get; private set; }
	}
}
