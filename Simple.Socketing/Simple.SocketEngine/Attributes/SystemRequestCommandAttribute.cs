using System;

namespace Simple.SocketEngine
{

	/// <summary>
	/// Indicates that a method represents socket request and receive method. This class cannot be inherited.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class SystemRequestCommandAttribute : Attribute
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="RequestCommandAttribute"/> class.
		/// </summary>
		/// <param name="requestId">The unique request identifier.</param>
		public SystemRequestCommandAttribute(int requestId)
			: this(requestId, String.Empty)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RequestCommandAttribute"/> class.
		/// </summary>
		/// <param name="name">The request name.</param>
		/// <param name="requestId">The unique request identifier.</param>
		public SystemRequestCommandAttribute(int requestId, string name)
        {
            this.Name = name;
            this.RequestId = requestId;
        }

        /// <summary>
        /// Gets or sets request name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets RequestId (Package Key).
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public int RequestId { get; set; }
    }
}
