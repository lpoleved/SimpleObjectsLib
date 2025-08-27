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
		private SimpleListWithEvents<Assembly> assemblies = new SimpleListWithEvents<Assembly>();
		IEnumerable<Type>? objectTypes = null;
		private HashArray<Type?>? messageArgsByMessageCode = null;
		private HashArray<Type?>? systemMessageArgsByMessageCode = null;
		private HashArray<Type?>? requestArgsByRequestId = null;
		private HashArray<Type?>? systemRequestArgsByRequestId = null;
		private HashArray<Type?>? responseArgsByRequestId = null;
		private HashArray<Type?>? systemResponseArgsByRequestId = null;

		//public PackageArgsFactory()
		//	: this(new List<Assembly>())
		//{
		//}

		public PackageArgsFactory(Assembly assembly)
			: this(new List<Assembly> { assembly })
		{
		}

		public PackageArgsFactory(IEnumerable<Assembly> assemblies)
		{
			this.assemblies.AddRange(assemblies);
			
			this.assemblies.AfterClear += Assemblies_AfterClear;
			this.assemblies.AfterInsert += Assemblies_AfterInsert;
			this.assemblies.AfterRemove += Assemblies_AfterRemove;
			
			this.RecalculateAdditionalDefinition();

			////assemblies.Add(this.GetType().Assembly);
			//var protocolAssembly = ReflectionHelper.GetAssemblyByName("Simple.Objects.SocketProtocol");
			
			//if (!assemblies.Contains(protocolAssembly))
			//	assemblies.Add(protocolAssembly);

			//if (assemblies.Count == 0)
			//	assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());

			//var objectTypes = ReflectionHelper.SelectInheritedAssemblyTypes(assemblies, typeof(PackageArgs));

			//foreach (Type objectType in objectTypes)
			//{
			//	foreach (MessageArgsAttribute attribute in objectType.GetCustomAttributes(typeof(MessageArgsAttribute), inherit: false))
			//		this.MessageArgsByMessageCode.SetValue((int)attribute.MessageCode, objectType);

			//	foreach (SystemMessageArgsAttribute attribute in objectType.GetCustomAttributes(typeof(SystemMessageArgsAttribute), inherit: false))
			//		this.SystemMessageArgsByMessageCode.SetValue((int)attribute.MessageCode, objectType);

			//	foreach (RequestArgsAttribute attribute in objectType.GetCustomAttributes(typeof(RequestArgsAttribute), inherit: false))
			//		this.RequestArgsByRequestId.SetValue((int)attribute.RequestId, objectType);

   //             foreach (SystemRequestArgsAttribute attribute in objectType.GetCustomAttributes(typeof(SystemRequestArgsAttribute), inherit: false))
   //                 this.SystemRequestArgsByRequestId.SetValue((int)attribute.RequestId, objectType);

   //             foreach (ResponseArgsAttribute attribute in objectType.GetCustomAttributes(typeof(ResponseArgsAttribute), inherit: false))
   //                 this.ResponseArgsByRequestId.SetValue((int)attribute.RequestId, objectType);

   //             foreach (SystemResponseArgsAttribute attribute in objectType.GetCustomAttributes(typeof(SystemResponseArgsAttribute), inherit: false))
   //                 this.SystemResponseArgsByRequestId.SetValue((int)attribute.RequestId, objectType);
   //         }
		}

		private IEnumerable<Type> ObjectTypes => this.objectTypes ??= ReflectionHelper.SelectInheritedAssemblyTypes(assemblies, typeof(PackageArgs));

		public SimpleListWithEvents<Assembly> Assemblies => this.assemblies;

		public HashArray<Type?> MessageArgsByMessageCode 
		{ 
			get
			{
				if (this.messageArgsByMessageCode == null)
				{
					this.messageArgsByMessageCode = new HashArray<Type?>();

					foreach (Type objectType in this.ObjectTypes)
						foreach (MessageArgsAttribute attribute in objectType.GetCustomAttributes(typeof(MessageArgsAttribute), inherit: false))
							this.messageArgsByMessageCode.SetValue((int)attribute.MessageCode, objectType);
				}

				return this.messageArgsByMessageCode;
			}
		}

		public HashArray<Type?> SystemMessageArgsByMessageCode
		{
			get
			{
				if (this.systemMessageArgsByMessageCode == null)
				{
					this.systemMessageArgsByMessageCode = new HashArray<Type?>();

					foreach (Type objectType in this.ObjectTypes)
						foreach (SystemMessageArgsAttribute attribute in objectType.GetCustomAttributes(typeof(SystemMessageArgsAttribute), inherit: false))
							this.systemMessageArgsByMessageCode.SetValue((int)attribute.MessageCode, objectType);
				}

				return this.systemMessageArgsByMessageCode;
			}
		}

		public HashArray<Type?> RequestArgsByRequestId
		{
			get
			{
				if (this.requestArgsByRequestId == null)
				{
					this.requestArgsByRequestId = new HashArray<Type?>();

					foreach (Type objectType in this.ObjectTypes)
						foreach (RequestArgsAttribute attribute in objectType.GetCustomAttributes(typeof(RequestArgsAttribute), inherit: false))
							this.requestArgsByRequestId.SetValue((int)attribute.RequestId, objectType);
				}

				return this.requestArgsByRequestId;
			}
		}

		public HashArray<Type?> SystemRequestArgsByRequestId
		{
			get
			{
				if (this.systemRequestArgsByRequestId == null)
				{
					this.systemRequestArgsByRequestId = new HashArray<Type?>();

					foreach (Type objectType in this.ObjectTypes)
						foreach (SystemRequestArgsAttribute attribute in objectType.GetCustomAttributes(typeof(SystemRequestArgsAttribute), inherit: false))
							this.systemRequestArgsByRequestId.SetValue((int)attribute.RequestId, objectType);
				}

				return this.systemRequestArgsByRequestId;
			}
		}

		public HashArray<Type?> ResponseArgsByRequestId
		{
			get
			{
				if (this.responseArgsByRequestId == null)
				{
					this.responseArgsByRequestId = new HashArray<Type?>();

					foreach (Type objectType in this.ObjectTypes)
						foreach (ResponseArgsAttribute attribute in objectType.GetCustomAttributes(typeof(ResponseArgsAttribute), inherit: false))
							this.responseArgsByRequestId.SetValue((int)attribute.RequestId, objectType);
				}

				return this.responseArgsByRequestId;
			}
		}

		public HashArray<Type?> SystemResponseArgsByRequestId
		{
			get
			{
				if (this.systemResponseArgsByRequestId == null)
				{
					this.systemResponseArgsByRequestId = new HashArray<Type?>();

					foreach (Type objectType in this.ObjectTypes)
						foreach (SystemResponseArgsAttribute attribute in objectType.GetCustomAttributes(typeof(SystemResponseArgsAttribute), inherit: false))
							this.systemResponseArgsByRequestId.SetValue((int)attribute.RequestId, objectType);
				}

				return this.systemResponseArgsByRequestId;
			}
		}

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

		
		protected virtual void RecalculateAdditionalDefinition() { }
		
		private void Assemblies_AfterRemove(object sender, CollectionActionEventArgs<Assembly> e) => this.OnAssemblyListChange();

		private void Assemblies_AfterInsert(object sender, CollectionActionEventArgs<Assembly> e) => this.OnAssemblyListChange();

		private void Assemblies_AfterClear(object? sender, EventArgs e) => this.OnAssemblyListChange();

		private T? CreateArgsInstance<T>(HashArray<Type?> hashArray, int messageCodeOrRequestId) where T : PackageArgs
		{
            Type? classType = hashArray.GetValue(messageCodeOrRequestId);

            if (classType != null)
                return (T?)Activator.CreateInstance(classType);
            else
                return default;
        }

		private void OnAssemblyListChange()
		{
			this.objectTypes = null;
			this.messageArgsByMessageCode = null;
			this.systemMessageArgsByMessageCode = null;
			this.requestArgsByRequestId = null;
			this.systemRequestArgsByRequestId = null;
			this.responseArgsByRequestId = null;
			this.systemResponseArgsByRequestId = null;

			this.RecalculateAdditionalDefinition();
		}
	}
}
