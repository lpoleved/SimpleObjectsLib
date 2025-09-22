using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Simple;
using Simple.Collections;
using Simple.SocketEngine;
//using Simple.Objects.Services;
using Simple.Objects.SocketProtocol;
//using DevExpress.CodeParser;

namespace Simple.Objects.ServerMonitor
{
	public class EditPanelFactory
	{
        public HashArray<Type?> EditPanelTypeByMessageCode = new HashArray<Type?>();
        public HashArray<Type?> EditPanelTypeBySystemMessageCode = new HashArray<Type?>();
        public HashArray<Type?> EditPanelTypeByRequestId = new HashArray<Type?>();
        public HashArray<Type?> EditPanelTypeBySystemRequestId = new HashArray<Type?>();

		public EditPanelFactory()
			: this(AppDomain.CurrentDomain.GetAssemblies())
		{
		}

		public EditPanelFactory(IEnumerable<Assembly> editPanelAssemblies)
		{
			var objectTypes = ReflectionHelper.SelectInheritedAssemblyTypes(editPanelAssemblies, typeof(EditPanelPackageInfo));

			foreach (Type objectType in objectTypes)
			{
				foreach (MessageArgsAttribute attribute in objectType.GetCustomAttributes(typeof(MessageArgsAttribute), inherit: false))
					if (this.EditPanelTypeByMessageCode.GetValue(attribute.MessageCode) is null)
						this.EditPanelTypeByMessageCode[attribute.MessageCode] = objectType;
					else
						throw new DuplicateMessageException($"Duplicate Message Code Args: Class={objectType}, MessageCode={attribute.MessageCode}");

				foreach (SystemMessageArgsAttribute attribute in objectType.GetCustomAttributes(typeof(SystemMessageArgsAttribute), inherit: false))
					if (this.EditPanelTypeBySystemMessageCode.GetValue(attribute.MessageCode) is null)
						this.EditPanelTypeBySystemMessageCode[attribute.MessageCode] = objectType;
					else
						throw new DuplicateMessageException($"Duplicate Message Code Args: Class={objectType}, MessageCode={attribute.MessageCode}");

				foreach (RequestArgsAttribute attribute in objectType.GetCustomAttributes(typeof(RequestArgsAttribute), inherit: false))
					if (this.EditPanelTypeByRequestId.GetValue(attribute.RequestId) is null)
						this.EditPanelTypeByRequestId[attribute.RequestId] = objectType;
					else
						throw new DuplicateMessageException($"Duplicate Message Code Args: Class={objectType}, RequestId={attribute.RequestId}");

				foreach (SystemRequestArgsAttribute attribute in objectType.GetCustomAttributes(typeof(SystemRequestArgsAttribute), inherit: false))
					if (this.EditPanelTypeBySystemRequestId.GetValue(attribute.RequestId) is null)
						this.EditPanelTypeBySystemRequestId[attribute.RequestId] = objectType;
					else
						throw new DuplicateMessageException($"Duplicate Message Code Args: Class={objectType}, RequestId={attribute.RequestId}");
			}
		}

		public EditPanelPackageInfo? CreateEditPanelByMessageCode(int messageCode) => this.CreateEditPanelInstance<EditPanelPackageInfo>(this.EditPanelTypeByMessageCode, messageCode);
		public EditPanelPackageInfo? CreateEditPanelBySystemMessageCode(int messageCode) => this.CreateEditPanelInstance<EditPanelPackageInfo>(this.EditPanelTypeBySystemMessageCode, messageCode);

		public EditPanelPackageInfo? CreateEditPanelByRequestId(int messageCode) => this.CreateEditPanelInstance<EditPanelPackageInfo>(this.EditPanelTypeByRequestId, messageCode);
		public EditPanelPackageInfo? CreateEditPanelBySystemRequestId(int messageCode) => this.CreateEditPanelInstance<EditPanelPackageInfo>(this.EditPanelTypeBySystemRequestId, messageCode);


		private T? CreateEditPanelInstance<T>(HashArray<Type?> hashArray, int messageCodeOrRequestId)
		{
			Type? classType = hashArray[messageCodeOrRequestId];

			if (classType != null)
				return (T?)Activator.CreateInstance(classType);
			else
				return default;
		}


		public Type? TryGetSystemMessageEditPanelType(SystemMessage systemMessageCode) => this.EditPanelTypeBySystemMessageCode.TryGetValue((int)systemMessageCode);

		public Type? GetPackageInfoRowEditPanelType(object? bindingObject)
		{
			Type? result;
			
			if (bindingObject != null && bindingObject is PackageInfoRow packageInfoRow)
			{
				if (packageInfoRow.RequestOrMessagePackageReader.PackageInfo.HeaderInfo.PackageType == PackageType.Message)
				{
                    if (packageInfoRow.RequestOrMessagePackageReader.PackageInfo.HeaderInfo.IsSystem) 
						if (this.EditPanelTypeBySystemMessageCode.TryGetValue(packageInfoRow.RequestOrMessagePackageReader.PackageInfo.Key, out result))
							return result;
					else if (this.EditPanelTypeByMessageCode.TryGetValue(packageInfoRow.RequestOrMessagePackageReader.PackageInfo.Key, out result))
						return result;
				}
				else if (packageInfoRow.RequestOrMessagePackageReader.PackageInfo.HeaderInfo.PackageType == PackageType.Request || packageInfoRow.RequestOrMessagePackageReader.PackageInfo.HeaderInfo.PackageType == PackageType.Response)
				{
					if (packageInfoRow.RequestOrMessagePackageReader.PackageInfo.HeaderInfo.IsSystem)
						if (this.EditPanelTypeBySystemRequestId.TryGetValue(packageInfoRow.RequestOrMessagePackageReader.PackageInfo.Key, out result))
							return result;
					if (this.EditPanelTypeByRequestId.TryGetValue(packageInfoRow.RequestOrMessagePackageReader.PackageInfo.Key, out result))
						return result;
				}

				return typeof(EditPanelSessionPackageRequestResponse);
			}

			return default;
        }
	}


 //   public class PackageInfoRowEditPanelDiscovery
	//{
	//	private Dictionary<SystemRequest, Type?> EditPanelTypeBySystemRequest = new Dictionary<SystemRequest, Type?>();

	//	public PackageInfoRowEditPanelDiscovery(IEnumerable<Assembly> editPanelAssemblies)
	//	{
	//		var objectTypes = ReflectionHelper.SelectInheritedAssemblyTypes(editPanelAssemblies, typeof(EditPanelSessionPackageAction));

	//		foreach (Type objectType in objectTypes)
	//			foreach (SystemRequestKeyAttribute includePropertyAttribute in objectType.GetCustomAttributes(typeof(SystemRequestKeyAttribute), inherit: false))
	//				if (includePropertyAttribute.IsSystemRequest)
	//					this.EditPanelTypeBySystemRequest[includePropertyAttribute.RequestKey] = objectType;
	//	}

	//	public Type? GetPackageInfoRowEditPanelType(object? bindingObject)
	//	{
	//		if (bindingObject != null && bindingObject is PackageInfoRow packageInfoRow)
	//		{
	//			if (packageInfoRow.PackageType == PackageType.Request || packageInfoRow.PackageType == PackageType.Response)
	//			{
	//				if (packageInfoRow.Flags.IsSystem && this.EditPanelTypeBySystemRequest.TryGetValue((SystemRequest)packageInfoRow.KeyValue, out Type? result))
	//					return result;

	//				return typeof(EditPanelSessionPackageJobActionRequestResponse);

	//				//switch (packageInfoRow.KeyValue)
	//				//{
	//				//	case (int)SystemRequest.GetProtocolVersion:
	//				//		return typeof(EditPanelGetProtocolVersion);

	//				//	case (int)SystemRequest.GetServerObjectPropertyInfo:
	//				//		return typeof(EditPanelGetServerObjectPropertyInfo);

	//				//	case (int)SystemRequest.GetObjectIds:
	//				//		return typeof(EditPanelRequestGetObjectIds);

	//				//	case (int)SystemRequest.GetObjectKeys:
	//				//		return typeof(EditPanelRequestGetObjectKeys);

	//				//	case (int)SystemRequest.GetObjectSequencePropertyValues:
	//				//		return typeof(EditPanelGetObjectSequencePropertyValues);

	//				//	case (int)SystemRequest.GetObjectSequenceDefaultOptimizedPropertyValues:
	//				//		return typeof(EditPanelGetObjectSequenceDefaultOptimizedPropertyValues);

	//				//	case (int)SystemRequest.GetObjectIndexedPropertyValues:
	//				//		return typeof(EditPanelGetObjectIndexedPropertyValues);

	//				//	case (int)SystemRequest.GetObjectIndexedDefaultOptimizedPropertyValues:
	//				//		return typeof(EditPanelGetObjectIndexedDefaultOptimizedPropertyValues);

	//				//	case (int)SystemRequest.ProcessTransactionRequest:
	//				//		return typeof(EditPanelProcessTransactionRequest);

	//				//	default:
	//				//		return typeof(EditPanelSessionPackageJobActionRequestResponse);
	//				//}
	//			}
	//			else
	//			{
	//				return typeof(EditPanelSessionPackageJobActionMessage);
	//			}
	//		}

	//		return null; //return typeof(SystemEditPanelEmpty);
	//	}
	//}
}
