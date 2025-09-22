using System;

namespace Simple.SocketEngine
{

	/// <summary>
	/// Indicates that a method represents socket request and receive method. This class cannot be inherited.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class RequestArgsAttribute : Attribute
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="RequestCommandAttribute"/> class.
		/// </summary>
		/// <param name="requestId">The unique request identifier.</param>
		public RequestArgsAttribute(int requestId)
        {
            this.RequestId = requestId;
        }

        /// <summary>
        /// Gets or sets RequestId (Package Key).
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public int RequestId { get; set; }
    }
}
