//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
//using Simple.Core;
//using Simple.Collections;
//using Simple.Modeling;
//using Simple.Objects;
//using Simple.Serialization;
//using Simple.SocketEngine;

//namespace Simple.Objects.MonitorProtocol
//{
//	public class GetObjectModelResponseArgs : ResponseArgs
//	{
//		public GetObjectModelResponseArgs()
//		{
//		}

//		public GetObjectModelResponseArgs(SerializationReader reader)
//			: base(reader)
//		{
//		}

//		public GetObjectModelResponseArgs(SimpleDictionary<int, SimpleObjectModel> objectModelsByTableId)
//		{
//			this.ObjectModelsByTableId = objectModelsByTableId;
//		}

//		public SimpleDictionary<int, SimpleObjectModel> ObjectModelsByTableId { get; private set; }

//		public override int GetBufferCapacity()
//		{
//			return 0;
//		}

//		public override void WriteTo(SerializationWriter writer)
//		{
//			base.WriteTo(writer);

//			writer.WriteInt32Optimized(this.ObjectModelsByTableId.Count);

//			foreach (var item in this.ObjectModelsByTableId)
//			{

//			}

//			int[] tableIds = this.ServerPropertySequencesByTableId.Keys.ToArray();

//			for (int i = 0; i < tableIds.Length; i++) //KeyValuePair<int, PropertySequence> item in this.ServerPublicPropertySequencesByTableId)
//			{
//				int tableId = tableIds[i];
//				IPropertySequence propertySequence = this.ServerPropertySequencesByTableId[tableId];

//				writer.WriteInt32Optimized(tableId); // Key is TableId
//				writer.WriteInt32Optimized(propertySequence.ModelSequence.Length);

//				for (int j = 0; j < propertySequence.ModelSequence.Length; j++)
//				{
//					IPropertyModel propertyModel = propertySequence.ModelSequence[j];

//					writer.WriteInt32Optimized(propertyModel.Index);
//					writer.WriteInt32Optimized(propertyModel.PropertyTypeId);

//					//if (propertyModel.PropertyTypeId == -1)
//					//{
//					//	int stop = 0;
//					//}
//				}
//			}
//		}

//		public override void ReadFrom(SerializationReader reader)
//		{
//			base.ReadFrom(reader);

//			int modelCount = reader.ReadInt32Optimized();
//			this.ServerPropertySequencesByTableId = new Dictionary<int, IPropertySequence>(modelCount);

//			for (int i = 0; i < modelCount; i++)
//			{
//				int tableId = reader.ReadInt32Optimized();
//				int propertyCount = reader.ReadInt32Optimized();

//				PropertyModel[] propertySequence = new PropertyModel[propertyCount];

//				for (int j = 0; j < propertyCount; j++)
//				{
//					int propertyIndex = reader.ReadInt32Optimized();
//					int propertyTypeId = reader.ReadInt32Optimized();
//					Type propertyType = ObjectTypes.GetType(propertyTypeId);

//					propertySequence[j] = new PropertyModel(propertyIndex, propertyType);
//				}

//				this.ServerPropertySequencesByTableId.Add(tableId, new PropertySequence(propertySequence));
//			}

//			//int tt = 0;

//		}
//	}
//}
