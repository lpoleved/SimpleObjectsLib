using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects.MonitorProtocol
{
	public enum MonitorSystemMessage
	{
		ServerSrarted = 0,
		ServerStopped = 1,
		SessionConnected = 2,
		SessionClosed = 3,
		SessionAuthenticated = 4,
		MessageSent = 5,
		BrodcastMessageSent = 6,
		MessageReceived = 7,
		RequestReceived = 8,
		RequestSent = 9,
		TransactionFinished = 10,
		PackageProcessingError = 11
	}
}
