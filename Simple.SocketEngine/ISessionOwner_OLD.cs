//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Logging;

//namespace Simple.SocketEngine
//{
//	public interface ISessionOwner
//	{
//		ILogger Logger { get; }

//		void OnRequestProcessed(ISimpleSession session, PackageReader requestPackage, PackageWriter responsePackage);
//		void OnResponseReceived(ISimpleSession session, PackageWriter requestPackage, PackageReader responsePackage);
//		void OnMessageSent(ISimpleSession session, PackageWriter package);
//		void OnMessageReceived(ISimpleSession session, PackageReader package);
//	}
//}
