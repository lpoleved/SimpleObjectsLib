using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Serialization;

namespace Simple.SocketEngine
{
	internal class ErrorMessageArgs : MessageArgs
	{

		public ErrorMessageArgs(PackageStatus resultInfo, string errorMessage)
		{
			this.Status = resultInfo;
			this.ErrorMessage = errorMessage;
		}


		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		{
			this.WriteErrorInfo(writer);
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		{
			throw new NotImplementedException("This class is only for error respond serialization writing");
		}
	}
}
