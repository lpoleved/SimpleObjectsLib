﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple
{
	public interface IGraphElement : IPropertyValue
	{
		int Id { get; }
	}
}
