using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.SocketEngine
{
	public enum PackageStatus
	{
		OK = 0,
		Error = 1,
		TimeOut = 2,
		UnknownRequest = 3,
		UnknownMessage = 4,
		UnknownToken = 5,
		ConnectionError = 6,
		SocketError = 7,
		NotAuthenticated = 8,
		NoSuchData = 9,
		NoSuchModule = 10,
		NoSuchMethod = 11,
		ExceptionIsCaught = 12,
		ExceptionIsCaughtOnMessageArgsInitialization = 13,
		ExceptionIsCaughtOnMessageArgsSerialization = 14,
		ExceptionIsCaughtOnRequestArgsInitialization = 15,
		ExceptionIsCaughtOnArgsSerialization = 16,
		ExceptionIsCaughtOnRequestArgsSerialization = 17,
		ExceptionIsCaughtOnResponseArgsInitialization = 18,
		ExceptionIsCaughtOnResponseArgsSerialization = 19,
		ExceptionIsCaughtOnArgsWriting = 20,
		ExceptionIsCaughtOnRequestProcessing = 21,
		ExceptionIsCaughtOnResponseProcessing = 22,
		ExceptionIsCaughtOnMessageSending = 23,
		ExceptionIsCaughtOnMessageReceiving = 24,
		PackageArgsTypeDoNotMach = 25,
		PackageArgsIsNull = 26,
	}
}
