using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket;
using SuperSocket.ProtoBase;
using SuperSocket.Client;
using Simple.Serialization;

namespace Simple.SocketEngine
{
	public static class SocketEngineExtensions
	{
		public static async ValueTask SendAsync(this IAppSession session, ReadOnlyMemory<byte> data)
		{
			await session.Channel.SendAsync(data);
		}

		public static async ValueTask SendAsync<TSendPackage>(this IAppSession session, IPackageEncoder<TSendPackage> packageEncoder, TSendPackage package)
		{
			await session.Channel.SendAsync<TSendPackage>(packageEncoder, package);
		}

		public static async ValueTask SendAsync<TSendPackage>(this ClientBase client, IPackageEncoder<TSendPackage> packageEncoder, TSendPackage package)
		{
			if (client.Provider != null)
				await client.Provider.SendAsync<TSendPackage>(packageEncoder, package);
		}

		public static T CreateFromReader<T>(this T serializationObject, ref SequenceReader reader, ISimpleSession session) 
			where T : PackageArgs
		{
			serializationObject.ReadFrom(ref reader, session);

			return serializationObject;
		}
	}
}
