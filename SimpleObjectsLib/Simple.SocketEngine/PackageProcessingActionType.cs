using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.SocketEngine
{
	public enum PackageProcessingActionType
	{
		RequestReceivedResponseSent = 0,
		ResponseReceived = 1,
		MessageSent = 2,
		MessageReceived = 3
	}
}
