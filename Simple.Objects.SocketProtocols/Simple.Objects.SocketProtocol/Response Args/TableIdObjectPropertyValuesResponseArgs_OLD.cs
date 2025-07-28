//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple.Collections;
//using Simple.Modeling;
//using Simple.Objects;
//using Simple.Serialization;
//using Simple.SocketEngine;

//namespace Simple.Objects.SocketProtocol
//{
//	[SystemResponseArgs((int)SystemRequest.GetObjectPropertyValues)]
//	public class TableIdObjectPropertyValuesResponseArgs_OLD : ObjectPropertyValuesResponseArgs
//	{
//		public TableIdObjectPropertyValuesResponseArgs_OLD()
//		{
//		}

//		public TableIdObjectPropertyValuesResponseArgs_OLD(int tableId, IEnumerable<PropertyIndexValuePair> propertyIndexValues)
//			: base(tableId, propertyIndexValues)
//		{
//		}

//		public int TableId => this.tableId;

//		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
//		{
//			base.WriteTo(ref writer, session);

//			writer.WriteInt32Optimized(this.TableId);
//		}

//		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
//		{
//			base.ReadFrom(ref reader, session);

//			this.tableId = reader.ReadInt32Optimized();
//		}
//	}
//}
