using Simple.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects.ServerMonitor
{
	public static class HexView
	{
		public static string To7BitEncodedHexString(this int value) => To7BitEncodedHexString(value, out int size);
		public static string To7BitEncodedHexString(this int value, out int size) => To7BitEncodedHexString((long)value, out size);

		public static string To7BitEncodedHexString(this long value) => To7BitEncodedHexString(value, out int size);
		public static string To7BitEncodedHexString(this ulong value) => To7BitEncodedHexString((long)unchecked(value), out int size);
		public static string To7BitEncodedHexString(this ulong value, out int size) => To7BitEncodedHexString((long)unchecked(value), out size);

		public static string To7BitEncodedHexString(this long value, out int size) 
		{
			var result = BitConverter.ToString(PackageEngine.Create7BitEncodedInt64(value, out int count), 0, count);

			size = count;

			return result;
		}
	}
}
