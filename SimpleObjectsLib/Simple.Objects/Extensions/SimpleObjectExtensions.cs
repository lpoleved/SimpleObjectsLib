using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Modeling;
using Simple.Serialization;

namespace Simple.Objects
{
	public static class SimpleObjectExtensions
	{
		public static TransactionActionInfo CreateFromReader(this TransactionActionInfo transactionActionInfo, ref SequenceReader reader, Func<int, ServerObjectModelInfo?> getServerObjectModel)
		{
			transactionActionInfo.ReadFrom(ref reader, getServerObjectModel);

			return transactionActionInfo;
		}
	}
}
