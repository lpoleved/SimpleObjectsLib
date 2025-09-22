using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;

namespace Simple.Objects
{
    public interface IBindingEasyObject : IBindingObject
    {
        IEasyObjectModel GetModel();
    }
}
