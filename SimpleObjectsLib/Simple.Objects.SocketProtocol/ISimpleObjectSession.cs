using Simple.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects.SocketProtocol
{
	public interface ISimpleObjectSession : ISimpleSession
	{
		ServerObjectModelInfo? GetServerObjectModel(int tableId);
		void SetServerObjectModel(int tableId, ServerObjectModelInfo? serverObjectModelInfo);
	}
}
