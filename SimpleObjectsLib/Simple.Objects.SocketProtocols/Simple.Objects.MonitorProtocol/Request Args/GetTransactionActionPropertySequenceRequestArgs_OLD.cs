//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Simple.Serialization;
//using Simple.SocketEngine;
//using Simple.Objects;

//namespace Simple.Objects.MonitorProtocol
//{
//	public class GetTransactionActionPropertySequenceRequestArgs_OLD : RequestArgs
//	{
//		public GetTransactionActionPropertySequenceRequestArgs_OLD()
//		{
//		}

//		public GetTransactionActionPropertySequenceRequestArgs_OLD(int propertySequenceId)
//		{
//			this.PropertySequenceId = propertySequenceId;
//		}

//		public int PropertySequenceId { get; set; }

//		public override int GetBufferCapacity()
//		{
//			return base.GetBufferCapacity() + 4;
//		}

//		public override void WriteTo(ref SequenceWriter writer, object context)
//		{
//			base.WriteTo(ref writer, context);
			
//			writer.WriteInt32Optimized(this.PropertySequenceId);
//		}

//		public override void ReadFrom(ref SequenceReader reader, object context)
//		{
//			base.ReadFrom(ref reader, context);
			
//			this.PropertySequenceId = reader.ReadInt32Optimized();
//		}
//	}
//}
