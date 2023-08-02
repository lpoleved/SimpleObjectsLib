using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects
{
	public interface IUser
	{
		long Id { get; }
		
		string Username { get; }
		string PasswordHash { get; }

		Encoding CharacterEncoding { get; }

		DefaultSecurityPolicy SecurityPolicy { get; }
	}
}
