//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Simple;
//using Simple.Modeling;

//namespace Simple.Objects
//{
//	public class ServerObjectModelInfo
//	{
//		public ServerObjectModelInfo(IPropertySequence serverPropertySequence, bool[] serverIsSerializationOptimazableSequence, SimpleObjectModel clientObjectModel)
//		{
//			this.PropertySequence = new ServerPropertyModelInfo[serverPropertySequence.PropertyIndexes.Length];

//			for (int i = 0; i < serverPropertySequence.PropertyIndexes.Length; i++)
//			{
//				int propertyIndex = serverPropertySequence.PropertyIndexes[i];
//				int propertyTypeId = serverPropertySequence.PropertyTypeIds[i];
//				bool isSerializationOptimazable = serverIsSerializationOptimazableSequence[i];

//				PropertyModel clientPropertyModel = null;

//				if (clientObjectModel.SerializablePropertySequence.PropertyIndexes.Contains(propertyIndex))
//					clientPropertyModel = clientObjectModel.PropertyModels[propertyIndex];

//				this.PropertySequence[i] = new ServerPropertyModelInfo(clientPropertyModel, propertyIndex, propertyTypeId, isSerializationOptimazable);
//			}
//		}

//		public ServerPropertyModelInfo[] PropertySequence { get; private set; }
//	}
//}
