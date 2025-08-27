//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
//using Simple;
//using Simple.Serialization;
//using Simple.SocketEngine;

//namespace Simple.Objects.MonitorProtocol
//{
//	public class TestPackageSentMessageArgs_OLD : MessageArgs
//	{
//		public TestPackageSentMessageArgs_OLD()
//		{
//			this.SessionID = String.Empty;
//		}

//		public TestPackageSentMessageArgs_OLD(string sessionID, int token)
//		{
//			this.SessionID = sessionID;
//			this.Token = token;
//		}

//		public string SessionID { get; private set; }
//		public int Token { get; private set; }

//		public override int GetBufferCapacity()
//		{
//			return base.GetBufferCapacity();
//		}

//		public override void WriteTo(ref SequenceWriter writer, object context)
//		{
//			base.WriteTo(ref writer, context);

//			writer.WriteString(this.SessionID);
//			writer.WriteInt32Optimized(this.Token);
//		}

//		public override void ReadFrom(ref SequenceReader reader, object context)
//		{
//			this.ReadFrom(ref reader, context);
			
//			this.SessionID = reader.ReadString();
//			this.Token = reader.ReadInt32Optimized();
//		}
//	}
//}
