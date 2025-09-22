using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects
{
	/// <summary>
	/// Use it for local user property.
	/// </summary>
	public enum DefaultSecurityPolicy
	{
		SystemDefault = 0,
		PermitOnlyListed = 1, // All other is forbidden
		DenyAllListed = 2     // All other is permited
	}

	
	/// <summary>
	/// Use it for global app settings.
	/// </summary>
	public enum SecurityPolicy // This is for global settings
	{
		PermitOnlyListed = 0, // All other is forbidden
		DenyAllListed = 1     // All other is permited
	}
}
