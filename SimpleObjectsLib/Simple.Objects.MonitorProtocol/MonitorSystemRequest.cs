using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects.MonitorProtocol
{
	public enum MonitorSystemRequest
	{
		GetProtocolVersion = 0,
		AuthenticateSession = 1,
		GetServerState = 2,
		StopServer = 3,
		StartServer = 4,
		GetSessionInfos = 5,
		GetServerObjectModel = 6,
		GetGraphName = 7,
		GetRelationName = 8,
		GetObjectName = 9,
		//GetTransactionActionPropertySequence = 7,
	}
}
