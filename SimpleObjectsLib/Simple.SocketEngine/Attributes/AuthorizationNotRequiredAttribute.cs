using System;

namespace Simple.SocketEngine
{
    /// <summary>
    /// Indicates that a method does not require session to be authorized previously. 
	/// This is typically protocol version or authorization request as part of session initialization. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class AuthorizationNotRequiredAttribute : Attribute
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="AuthorizationAttribute"/> class.
		/// </summary>
		public AuthorizationNotRequiredAttribute()
		{
		}
    }
}
