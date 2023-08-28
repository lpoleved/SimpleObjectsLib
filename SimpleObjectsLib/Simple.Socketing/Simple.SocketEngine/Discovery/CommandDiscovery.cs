using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Simple;
using Simple.Collections;

namespace Simple.SocketEngine
{
	public class CommandDiscovery
	{
		public HashArray<MethodInfo> SystemMessageMethodsByMessageCode = new HashArray<MethodInfo>();
		public HashArray<MethodInfo> SystemRequestMethodsByRequestId = new HashArray<MethodInfo>();
		public HashArray<MethodInfo> MessageMethodsByMessageCode = new HashArray<MethodInfo>();
		public HashArray<MethodInfo> RequestMethodsByRequestId = new HashArray<MethodInfo>();
		public HashSet<int> SystemMessageCodesNotRequireAuthorization = new HashSet<int>();
		public HashSet<int> MessageCodesNotRequireAuthorization = new HashSet<int>();
		public HashSet<int> SystemRequestIdsNotRequireAuthorization = new HashSet<int>();
		public HashSet<int> RequestIdsNotRequireAuthorization = new HashSet<int>();

		public CommandDiscovery(Type methodHolderType)
		{
			MethodInfo[] methods = methodHolderType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			foreach (MethodInfo methodInfo in methods)
			{
				bool authorisationNotRequired = methodInfo.GetCustomAttributes(typeof(AuthorizationNotRequiredAttribute), inherit: true).Length > 0;

				// System Messages discovery
				foreach (SystemMessageCommandAttribute systemMessage in methodInfo.GetCustomAttributes(typeof(SystemMessageCommandAttribute), inherit: false))
				{
					if (systemMessage.Name.IsNullOrEmpty())
						systemMessage.Name = methodInfo.Name;

					if (this.SystemMessageMethodsByMessageCode.GetValue(systemMessage.MessageCode) != null)
						throw new DuplicateMessageException(String.Format("Duplicate System Message specification: Class={0}, MessageCode={1}, MethodName={2}.", methodHolderType.Name, systemMessage.MessageCode, methodInfo.Name));

					this.SystemMessageMethodsByMessageCode[systemMessage.MessageCode] = methodInfo;

					if (authorisationNotRequired)
						this.SystemMessageCodesNotRequireAuthorization.Add(systemMessage.MessageCode);
				}

				// System Requests discovery
				foreach (SystemRequestCommandAttribute systemRequest in methodInfo.GetCustomAttributes(typeof(SystemRequestCommandAttribute), inherit: false))
				{
					if (systemRequest.Name.IsNullOrEmpty())
						systemRequest.Name = methodInfo.Name;

					if (this.SystemRequestMethodsByRequestId.GetValue(systemRequest.RequestId) != null)
						throw new DuplicateRequestException(String.Format("Duplicate System Request specification: Class={0}, RequestId={1}, MethodName={2}.", methodHolderType.Name, systemRequest.RequestId, methodInfo.Name));

					this.SystemRequestMethodsByRequestId[systemRequest.RequestId] = methodInfo;

					if (authorisationNotRequired)
						this.SystemRequestIdsNotRequireAuthorization.Add(systemRequest.RequestId);
				}

				// Messages discovery
				foreach (MessageCommandAttribute message in methodInfo.GetCustomAttributes(typeof(MessageCommandAttribute), inherit: false))
				{
					if (message.Name.IsNullOrEmpty())
						message.Name = methodInfo.Name;

					if (this.MessageMethodsByMessageCode.GetValue(message.MessageCode) != null)
						throw new DuplicateMessageException(String.Format("Duplicate Message specification: Class={0}, MessageCode={1}, MethodName={2}.", methodHolderType.Name, message.MessageCode, methodInfo.Name));

					this.MessageMethodsByMessageCode[message.MessageCode] = methodInfo;

					if (authorisationNotRequired)
						this.MessageCodesNotRequireAuthorization.Add(message.MessageCode);
				}

				// Requests discovery
				foreach (RequestCommandAttribute request in methodInfo.GetCustomAttributes(typeof(RequestCommandAttribute), inherit: false))
				{
					if (request.Name.IsNullOrEmpty())
						request.Name = methodInfo.Name;

					if (this.RequestMethodsByRequestId.GetValue(request.RequestId) != null)
						throw new DuplicateRequestException(String.Format("Duplicate Request specification: Class={0}, RequestId={1}, MethodName={2}.", methodHolderType.Name, request.RequestId, methodInfo.Name));

					this.RequestMethodsByRequestId[request.RequestId] = methodInfo;

					if (authorisationNotRequired)
						this.RequestIdsNotRequireAuthorization.Add(request.RequestId);
				}
			}
		}
	}
}
