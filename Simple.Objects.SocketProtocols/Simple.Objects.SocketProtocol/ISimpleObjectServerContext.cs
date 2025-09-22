using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects.SocketProtocol
{
	public interface ISimpleObjectServerContext : IServerContext
	{
		ServerObjectModelInfo? GetServerObjectModel(int tableId);
	}
}
