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
//	[SystemRequestArgs((int)SystemRequest.GetObjectCacheRelationCollectionWithTableId)]

//	public class GetCacheRelationCollectionWithTableIdRequestArgs_OLD : GetOneToOneForeignObjectRequestArgs
//	{
//		public GetCacheRelationCollectionWithTableIdRequestArgs_OLD()
//		{
//		}

//		public GetCacheRelationCollectionWithTableIdRequestArgs_OLD(int cacheTableId, int tableIdPropertyIndex, int tableId, int objectIdPropertyIndex, long objectId)
//			: base(cacheTableId, objectIdPropertyIndex, objectId)
//		{
//			this.TableIdPropertyIndex = tableIdPropertyIndex;
//			this.TableId = tableId;
//		}

//		public int TableIdPropertyIndex { get; private set; }
//		public int TableId { get; private set; }

//		public override int GetBufferCapacity() => base.GetBufferCapacity() + 4;

//		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
//		{
//			base.WriteTo(ref writer, session);
			
//			writer.WriteInt32Optimized(this.TableIdPropertyIndex);
//			writer.WriteInt32Optimized(this.TableId);
//		}

//		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
//		{
//			base.ReadFrom(ref reader, session);
			
//			this.TableIdPropertyIndex = reader.ReadInt32Optimized();
//			this.TableId = reader.ReadInt32Optimized();
//		}
//	}
//}
