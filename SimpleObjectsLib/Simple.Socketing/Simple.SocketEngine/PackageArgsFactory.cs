using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Simple;
using Simple.Collections;
using Simple.Serialization;

namespace Simple.SocketEngine
{
	public class PackageArgsFactory
	{
		public PackageArgsFactory()
			: this(new Assembly[0])
		{
		}
		
		public PackageArgsFactory(IEnumerable<Assembly> argsAssemblies)
		{
			List<Assembly> assemblies = new List<Assembly>();

			assemblies.AddRange(argsAssemblies);
			
			if (assemblies.Count == 0)
				assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());

			var objectTypes = ReflectionHelper.SelectInheritedAssemblyTypes(assemblies, typeof(PackageArgs));

			foreach (Type objectType in objectTypes)
			{
				foreach (MessageArgsAttribute attribute in objectType.GetCustomAttributes(typeof(MessageArgsAttribute), inherit: false))
					this.MessageArgsByMessageCode.SetValue((int)attribute.MessageCode, objectType);

				foreach (SystemMessageArgsAttribute attribute in objectType.GetCustomAttributes(typeof(SystemMessageArgsAttribute), inherit: false))
					this.SystemMessageArgsByMessageCode.SetValue((int)attribute.MessageCode, objectType);

				foreach (RequestArgsAttribute attribute in objectType.GetCustomAttributes(typeof(RequestArgsAttribute), inherit: false))
					this.RequestArgsByRequestId.SetValue((int)attribute.RequestId, objectType);

                foreach (SystemRequestArgsAttribute attribute in objectType.GetCustomAttributes(typeof(SystemRequestArgsAttribute), inherit: false))
                    this.SystemRequestArgsByRequestId.SetValue((int)attribute.RequestId, objectType);

                foreach (ResponseArgsAttribute attribute in objectType.GetCustomAttributes(typeof(ResponseArgsAttribute), inherit: false))
                    this.ResponseArgsByRequestId.SetValue((int)attribute.RequestId, objectType);

                foreach (SystemResponseArgsAttribute attribute in objectType.GetCustomAttributes(typeof(SystemResponseArgsAttribute), inherit: false))
                    this.SystemResponseArgsByRequestId.SetValue((int)attribute.RequestId, objectType);
            }
		}

		public HashArray<Type?> MessageArgsByMessageCode { get; private set; } = new HashArray<Type?>();
		public HashArray<Type?> RequestArgsByRequestId { get; private set; } = new HashArray<Type?>();
		public HashArray<Type?> ResponseArgsByRequestId { get; private set; } = new HashArray<Type?>();
		public HashArray<Type?> SystemMessageArgsByMessageCode { get; private set; }  = new HashArray<Type?>();
		public HashArray<Type?> SystemRequestArgsByRequestId { get; private set; } = new HashArray<Type?>();
		public HashArray<Type?> SystemResponseArgsByRequestId { get; private set; } = new HashArray<Type?>();

		public PackageArgs? CreatePackageArgs(PackageType packageType, int requestIdOrMessageCode, bool isSystem)
		{
			if (packageType == PackageType.Request)
			{
				if (isSystem)
					return this.CreateSystemRequestArgs(requestIdOrMessageCode);
				else
					return this.CreateRequestArgs(requestIdOrMessageCode);
			}
			else if (packageType == PackageType.Response)
			{
				if (isSystem)
					return this.CreateSystemResponseArgs(requestIdOrMessageCode);
				else
					return this.CreateResponseArgs(requestIdOrMessageCode);
			}
			else if (packageType == PackageType.Message)
			{
				if (isSystem)
					return this.CreateSystemMessageArgs(requestIdOrMessageCode);
				else
					return this.CreateMessageArgs(requestIdOrMessageCode);
			}

			return null;
		}

		public MessageArgs? CreateMessageArgs(int messageCode) => this.CreateArgsInstance<MessageArgs>(this.MessageArgsByMessageCode, messageCode);
        public MessageArgs? CreateSystemMessageArgs(int messageCode) => this.CreateArgsInstance<MessageArgs>(this.SystemMessageArgsByMessageCode, messageCode);

        public RequestArgs? CreateRequestArgs(int requestId) => this.CreateArgsInstance<RequestArgs>(this.RequestArgsByRequestId, requestId);
        public RequestArgs? CreateSystemRequestArgs(int requestId) => this.CreateArgsInstance<RequestArgs>(this.SystemRequestArgsByRequestId, requestId);

        public ResponseArgs? CreateResponseArgs(int requestId) => this.CreateArgsInstance<ResponseArgs>(this.ResponseArgsByRequestId, requestId);
        public ResponseArgs? CreateSystemResponseArgs(int requestId) => this.CreateArgsInstance<ResponseArgs>(this.SystemResponseArgsByRequestId, requestId);

		private T? CreateArgsInstance<T>(HashArray<Type?> hashArray, int messageCodeOrRequestId) where T : PackageArgs
		{
            Type? classType = hashArray.GetValue(messageCodeOrRequestId);

            if (classType != null)
                return (T?)Activator.CreateInstance(classType);
            else
                return default;
        }
    }
}
