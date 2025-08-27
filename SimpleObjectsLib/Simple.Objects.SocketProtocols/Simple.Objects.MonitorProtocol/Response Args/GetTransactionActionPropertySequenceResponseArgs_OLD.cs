//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Simple;
//using Simple.Modeling;
//using Simple.Serialization;
//using Simple.Objects;
//using Simple.SocketEngine;
//using SuperSocket.ProtoBase;

//namespace Simple.Objects.MonitorProtocol
//{
//	[SystemResponseArgs((int)MonitorSystemRequest.GetTransactionActionPropertySequence)]

//	public class GetTransactionActionPropertySequenceResponseArgs_OLD : ResponseArgs
//	{
//		public GetTransactionActionPropertySequenceResponseArgs_OLD()
//		{
//		}

//		public GetTransactionActionPropertySequenceResponseArgs_OLD(IPropertySequence propertySequence)
//		{
//			this.PropertySequence = propertySequence;
//		}

//		public IPropertySequence? PropertySequence { get; set; }

//		public override int GetBufferCapacity()
//		{
//			return this.PropertySequence!.Length * 2 + 3;
//		}

//		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
//		{
//			base.WriteTo(ref writer, session);
			
//			writer.WriteInt32Optimized(this.PropertySequence!.Length);

//			for (int i = 0; i < this.PropertySequence.Length; i++)
//			{
//				int propertyIndex = this.PropertySequence.PropertyIndexes[i];
//				int propertyTypeId = this.PropertySequence.PropertyTypeIds[i];

//				writer.WriteInt32Optimized(propertyIndex);
//				writer.WriteInt32Optimized(propertyTypeId);
//			}
//		}

//		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
//		{
//			base.ReadFrom(ref reader, session);
			
//			int length = reader.ReadInt32Optimized();
//			int[] propertyIndexes = new int[length];
//			int[] propertyTypeIds = new int[length];

//			for (int i = 0; i < length; i++)
//			{
//				propertyIndexes[i] = reader.ReadInt32Optimized();
//				propertyTypeIds[i] = reader.ReadInt32Optimized();
//			}

//			this.PropertySequence = new PropertySequence(propertyIndexes, propertyTypeIds);
//		}
//	}
//}
