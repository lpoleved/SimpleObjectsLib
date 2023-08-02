//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Simple.Serialization;
//using Simple.SocketEngine;
//using Simple.Objects;

//namespace Simple.Objects.SocketProtocol
//{
//	[SystemRequestArgs((int)SystemRequest.GetOneToOneForeignObject)]

//	public class GetOneToOneForeignObjectRequestArgs : RequestArgs
//	{
//		public GetOneToOneForeignObjectRequestArgs()
//		{
//		}

//		public GetOneToOneForeignObjectRequestArgs(int objectCacheTableId, int objectIdPropertyIndex, long objectId)
//		{
//			this.ObjectCacheTableId = objectCacheTableId;
//			this.ObjectIdPropertyIndex = objectIdPropertyIndex;
//			this.ObjectId = objectId;
//		}

//		public int ObjectCacheTableId { get; private set; }
//		public int ObjectIdPropertyIndex { get; private set; }
//		public long ObjectId { get; private set; }

//		public override int GetBufferCapacity() => base.GetBufferCapacity() + 4;

//		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
//		{
//			base.WriteTo(ref writer, session);
			
//			writer.WriteInt32Optimized(this.ObjectCacheTableId);
//			writer.WriteInt32Optimized(this.ObjectIdPropertyIndex);
//			writer.WriteInt64Optimized(this.ObjectId);
//		}

//		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
//		{
//			base.ReadFrom(ref reader, session);

//			this.ObjectCacheTableId = reader.ReadInt32Optimized();
//			this.ObjectIdPropertyIndex = reader.ReadInt32Optimized();
//			this.ObjectId = reader.ReadInt64Optimized();
//		}
//	}
//}
