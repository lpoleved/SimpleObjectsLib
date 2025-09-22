using Simple.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects.SocketProtocol
{
	public class SendUnicastSystemMessageArgs : SendSystemMessageArgs
	{
		public SendUnicastSystemMessageArgs(SimpleSession session, int messageCode, MessageArgs? messageArgs = null)
			: base(messageCode, messageArgs)
		{
			this.Session = session;
		}

		public SimpleSession Session { get; private set; }
	}
}
