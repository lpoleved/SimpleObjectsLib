using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects
{
    public enum ObjectActionContext
    {
        Unspecified             = 0,      // 0
        Client                  = 1 << 1, // 1
        ServerTransaction       = 1 << 2, // 2
        RemoteClientTransaction = 1 << 3, // 4
        RemoteServerTransaction = 1 << 4, // 8
    }
}
