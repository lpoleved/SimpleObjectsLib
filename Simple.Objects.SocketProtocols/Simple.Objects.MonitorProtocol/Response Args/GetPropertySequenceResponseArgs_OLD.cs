//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Simple.Core;
//using Simple.Modeling;
//using Simple.Serialization;
//using Simple.SocketEngine;
//using SuperSocket.ProtoBase;

//namespace Simple.Objects.MonitorProtocol
//{
//	public class GetPropertySequenceResponseArgs : ResponseArgs
//	{
//		public GetPropertySequenceResponseArgs()
//		{
//		}

//		public GetPropertySequenceResponseArgs(SerializationReader reader)
//			: base(reader)
//		{
//		}

//		public GetPropertySequenceResponseArgs(int propertySequenceId, ISystemPropertySequence propertySequence)
//		{
//			this.PropertySequenceId = propertySequenceId;
//			this.PropertySequence = propertySequence;
//		}

//		public int PropertySequenceId { get; set; }
//		public ISystemPropertySequence PropertySequence { get; set; }

//		public override int GetBufferCapacity()
//		{
//			return PropertySequence.Length * 2 + 4;
//		}

//		public override void WriteTo(SerializationWriter writer)
//		{
//			base.WriteTo(writer);

//			writer.WriteInt32Optimized(this.PropertySequenceId);
//			writer.WriteInt32Optimized(this.PropertySequence.Length);

//			for (int i = 0; i < this.PropertySequence.Length; i++)
//			{
//				int propertyIndex = this.PropertySequence.PropertyIndexes[i];
//				int propertyTypeId = this.PropertySequence.PropertyTypeIds[i];

//				writer.WriteInt32Optimized(propertyIndex);
//				writer.WriteInt32Optimized(propertyTypeId);
//			}
//		}

//		public override void ReadFrom(SerializationReader reader)
//		{
//			base.ReadFrom(reader);

//			this.PropertySequenceId = reader.ReadInt32Optimized();
//			int propertySequenceLength = reader.ReadInt32Optimized();

//			int[] indexSequence = new int[propertySequenceLength];
//			int[] typeIdSequence = new int[propertySequenceLength];

//			for (int i = 0; i < propertySequenceLength; i++)
//			{
//				indexSequence[i] = reader.ReadInt32Optimized();
//				typeIdSequence[i] = reader.ReadInt32Optimized();
//			}

//			this.PropertySequence = new SystemPropertySequenceHolder(indexSequence, typeIdSequence);
//		}

//		class SystemPropertySequenceHolder : ISystemPropertySequence
//		{
//			public SystemPropertySequenceHolder(int[] propertyIndexes, int[] propertyTypeIds)
//			{
//				this.PropertyIndexes = propertyIndexes;
//				this.PropertyTypeIds = propertyTypeIds;
//			}

//			public int[] PropertyIndexes { get; private set; }
//			public int[] PropertyTypeIds { get; private set; }

//			public int Length
//			{
//				get { return this.PropertyIndexes.Length; }
//			}
//		}
//	}
//}
