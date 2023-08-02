using Microsoft.Extensions.Logging;
using Simple.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket;

namespace Simple.Objects.SocketProtocol
{
	public class SimpleObjectSession : SimpleSession, ISimpleObjectSession, ISimpleSession, IAppSession, ILogger, ILoggerAccessor
	{
        public SimpleObjectSession(SimpleObjectManager objectManager)
        {
            this.ObjectManager = objectManager;
        }

        protected SimpleObjectManager ObjectManager { get; private set; }

        public ServerObjectModelInfo GetServerObjectModel(int tableId) => this.ObjectManager.GetServerObjectModel(tableId)!;

        void ISimpleObjectSession.SetServerObjectModel(int tableId, ServerObjectModelInfo? serverObjectModelInfo) => throw new NotImplementedException("Set ServerObjectModel is only applicable for client session");
	}
}
