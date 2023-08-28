using System;

namespace Simple.SocketEngine
{

	/// <summary>
	/// Indicates that a method represents socket message method. This class cannot be inherited.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class MessageArgsAttribute : Attribute
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="MessageCommandAttribute"/> class.
		/// </summary>
		/// <param name="messageCode">The unique message code identifier.</param>
		public MessageArgsAttribute(int messageCode)
        {
            this.MessageCode = messageCode;
        }

        /// <summary>
        /// Gets or sets message code (Package Key).
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public int MessageCode { get; set; }
    }
}
