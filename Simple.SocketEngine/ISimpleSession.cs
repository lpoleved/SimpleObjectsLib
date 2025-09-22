using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket;
using SuperSocket.ProtoBase;

namespace Simple.SocketEngine
{
	public interface ISimpleSession //: IAppSession
	{
		//long SessionKey { get; }
		Encoding CharacterEncoding { get; }
		bool IsConnected { get; }
		bool IsAuthenticated { get; }
		//long UserId { get; }
		//string Username { get; }
		ValueTask SendAsync(ReadOnlyMemory<byte> data);

		ValueTask SendAsync<TPackage>(IPackageEncoder<TPackage> packageEncoder, TPackage package);


		void ResponseIsReceived(PackageInfo response);

		//ValueTask SendAsync(byte[] data);
		//ValueTask SendAsync(ReadOnlyMemory<byte> segment);
		//ValueTask SendPackageAsync(ArraySegment<byte> segment);
		//ValueTask SendPackageAsync(IList<ArraySegment<byte>> segments);
		//ValueTask SendPackageAsync(IList<ReadOnlyMemory<byte>> segments);
	}
}
