using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Objects.SocketProtocol;

namespace Simple.Objects.ServerMonitor
{
	public interface IMonitorSessionContext
	{
		ISimpleObjectSession? GetSimpleObjectSession(long sessionKey);

		string GetGraphName(int graphKey);

		string GetRelationName(int relationKey);

		string GetObjectName(int tableId, long objectId);

		ServerObjectModelInfo? GetServerObjectModel(int tableId);
	}
}
