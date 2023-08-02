using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects
{
	public enum SimpleObjectInternalState
	{
		Normal = 0,
		Initialization = 1,
		TransactionRequestProcessing = 2
	}
}
