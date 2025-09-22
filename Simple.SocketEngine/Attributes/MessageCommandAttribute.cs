using System;

namespace Simple.SocketEngine
{

    /// <summary>
    /// Indicates that a method represents socket message method. This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class MessageCommandAttribute : Attribute
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="MessageCommandAttribute"/> class.
		/// </summary>
		/// <param name="messageCode">>The unique message code identifier.</param>
		public MessageCommandAttribute(int messageCode)
			: this(messageCode, String.Empty)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MessageCommandAttribute"/> class.
		/// </summary>
		/// <param name="name">The message name.</param>
		/// <param name="messageCode">The unique message code identifier.</param>
		public MessageCommandAttribute(int messageCode, string name)
        {
            this.Name = name;
            this.MessageCode = messageCode;
        }

        /// <summary>
        /// Gets or sets message name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets message code (Package Key).
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public int MessageCode { get; set; }
    }
}
