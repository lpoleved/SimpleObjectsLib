//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Simple;
//using Simple.Collections;
//using Simple.Modeling;

//namespace Simple.Objects
//{
//	public class ServerSerializablePropertyModelInfo
//	{
//		private HashArray<ServerPropertyModelInfo> propertyModelInfos = new HashArray<ServerPropertyModelInfo>();

//		public ServerSerializablePropertyModelInfo(int tableId, IServerPropertySequence serverPropertySequence, bool[] serverIsSerializationOptimazableSequence, SimpleObjectModel clientObjectModel)
//		{
//			this.TableId = tableId;
//			this.PropertyModelInfoSequence = new ServerPropertyModelInfo[serverPropertySequence.PropertyIndexes.Length];

//			for (int i = 0; i < serverPropertySequence.PropertyIndexes.Length; i++)
//			{
//				int propertyIndex = serverPropertySequence.PropertyIndexes[i];
//				int propertyTypeId = serverPropertySequence.PropertyTypeIds[i];
//				bool isSerializationOptimazable = serverIsSerializationOptimazableSequence[i];

//				//IPropertyModel serverPropertyModel = serverPropertySequence.PropertyModels[i];
//				PropertyModel clientPropertyModel = clientObjectModel.PropertyModels.GetPropertyModel(propertyIndex);
//				//if (clientObjectModel.SerializablePropertySequence.PropertyIndexes.Contains(propertyIndex))
//				//	clientPropertyModel = clientObjectModel.PropertyModels[propertyIndex];
//				ServerPropertyModelInfo serverPropertyModelInfo = new ServerPropertyModelInfo(clientPropertyModel, propertyIndex, propertyTypeId, isSerializationOptimazable);

//				this.PropertyModelInfoSequence[i] = serverPropertyModelInfo;
//				this.propertyModelInfos[propertyIndex] = serverPropertyModelInfo;
//			}
//		}

//		public int TableId { get; private set; }

//		public ServerPropertyModelInfo[] PropertyModelInfoSequence { get; private set; }

//		public ServerPropertyModelInfo GetServerPropertyModelInfo(int propertyIndex)
//		{
//			return this.propertyModelInfos[propertyIndex];
//		}
//	}
//}
