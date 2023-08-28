//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
//using Simple;
//using Simple.Serialization;
//using Simple.SocketEngine;
//using SuperSocket.ProtoBase;

//namespace Simple.Objects.MonitorProtocol
//{
//	public class SessionPackageJobActionMessageArgs_OLD : SessionMessageArgs
//	{
//		//byte[] receivedData = null;
//		PackageInfo? receivedPackage = null;
//		PackageInfo? sentPackage = null;
//		//SerializationReader receivedDataReader = null;
//		int? packageKey = null;
//		int? packageToken = null;

//		public SessionPackageJobActionMessageArgs_OLD()
//		{
//		}

//		//public SessionPackageJobActionMessageArgs(string sessionId, JobActionType jobActionType, PackageInfo receivedPackage, byte[] sentData)
//		//	: base(sessionId)
//		//{
//		//	this.ActionType = jobActionType;
//		//	this.receivedPackage = receivedPackage;
//		//	this.SentData = sentData;
//		//}

//		public SessionPackageJobActionMessageArgs_OLD(long sessionKey, PackageProcessingActionType actionType, PackageInfo receivedPackage, ArraySegment<byte> sentPackageLength, ArraySegment<byte> sentPackageHeaderAnyBody)
//			: base(sessionKey)
//		{
//			this.JobActionType = actionType;
//			this.receivedPackage = receivedPackage;
//			this.SentPackageLength = sentPackageLength;
//			this.SentPackageHeaderAnyBody = sentPackageHeaderAnyBody;
//		}

//		//private byte[] SentData { get; set; }

//		public PackageProcessingActionType JobActionType { get; private set; }

//		public ArraySegment<byte> SentPackageLength { get; private set; }
//		public ArraySegment<byte> SentPackageHeaderAnyBody { get; private set; }

//		public int PackageKey
//		{
//			get
//			{
//				if (this.packageKey == null)
//					this.SetKeyAndToken();

//				return (int)this.packageKey!;
//			}
//		}

//		public int PackageToken
//		{
//			get
//			{
//				if (this.packageToken == null)
//					this.SetKeyAndToken();

//				return (int)this.packageToken!;
//			}
//		}

//		public PackageInfo? ReceivedPackage
//		{
//			get
//			{
//				//if (this.receivedPackage == null && this.receivedData != null)
//				//{
//				//	MemoryStream receivedPackageStream = new MemoryStream(this.receivedData, 0, this.receivedData.Length, writable: false, publiclyVisible: true);
//				//	this.receivedPackage = PackageManager.CreatePackageOnReceive(receivedPackageStream, packageLengthIncluded: true);
//				//}

//				return this.receivedPackage;
//			}
//		}

//		public PackageInfo? SentPackage
//		{
//			get
//			{
//				//if (this.sentPackage == null)
//				//{
//				//	if (this.receivedDataReader != null)
//				//	{
//				//		this.sentPackage = PackageManager.CreatePackageOnReceive(this.receivedDataReader, packageLengthIncluded: true);
//				//	}
//				//	else
//				//	{
//				//		BufferStream sentPackageStream = new BufferStream();
//				//		sentPackageStream.Initialize(new ArraySegment<byte>[] { this.SentPackageLength, this.SentPackageHeaderAnyBody });
//				//		this.sentPackage = PackageManager.CreatePackageOnReceive(sentPackageStream as IBufferStream, packageLengthIncluded: false);
//				//	}
//				//}

//				return this.sentPackage;
//			}
//		}

//		public override int GetBufferCapacity()
//		{
//			return base.GetBufferCapacity() + 4 + this.ReceivedPackage?.PackageLengthSize ?? 1; // + this.SentPackageHeaderAnyBody.Count;
//		}

//		public override void WriteTo(ref SequenceWriter writer, object context)
//		{
//			base.WriteTo(ref writer, context);

//			writer.WriteInt32Optimized((int)this.JobActionType);

//			switch (this.JobActionType)
//			{
//				case PackageProcessingActionType.RequestReceivedResponseSent:
//				case PackageProcessingActionType.ResponseReceived:

//					//writer.WriteByteArray(this.ReceivedPackage.ArgsReader .GetBuffer(), 0, (int)this.ReceivedPackage.Reader.BaseStream.Length);
//					//writer.WriteBytesDirect(this.SentPackageLength.Array, 0, this.SentPackageLength.Count);
//					//writer.WriteBytesDirect(this.SentPackageHeaderAnyBody.Array, 0, this.SentPackageHeaderAnyBody.Count);

//					break;

//				case PackageProcessingActionType.MessageSent:

//					//writer.WriteBytesDirect(this.SentPackageLength.Array, 0, this.SentPackageLength.Count);
//					//writer.WriteBytesDirect(this.SentPackageHeaderAnyBody.Array, 0, this.SentPackageHeaderAnyBody.Count);

//					break;

//				case PackageProcessingActionType.MessageReceived:

//					//writer.WriteBytesDirect(this.ReceivedPackage.Reader.BaseStream.GetBuffer(), 0, (int)this.ReceivedPackage.Reader.BaseStream.Length);

//					break;
//			}

//			//writer.WriteBytesDirect(this.ReceivedPackage.Reader.BaseStream.GetBuffer(), 0, (int)this.ReceivedPackage.Reader.BaseStream.Length);
//			//writer.WriteInt32Optimized(this.SentData.Length); // Package Length
//			//writer.WriteBytesDirect(this.SentData, 0, this.SentData.Length); // Package Data

//			//int sentDataSegmentsCount = 0;

//			//foreach (ArraySegment<byte> segment in this.SentData)
//			//	sentDataCount += segment.Count - segment.Offset;


//			//foreach (ArraySegment<byte> segment in this.SentData)
//			//	writer.WriteBytesDirect(segment.Array, segment.Offset, segment.Count);



//			//foreach (ArraySegment<byte> segment in this.ReceivedPackage.GetArraySegments())
//			//	writer.WriteBytesDirect(segment.Array, segment.Offset, segment.Count);
//		}



//		public override void ReadFrom(ref SequenceReader reader, object context)
//		{
//			base.ReadFrom(ref reader, context);

//			this.packageKey = null;
//			this.packageToken = null;

//			this.JobActionType = (PackageProcessingActionType)reader.ReadInt32Optimized();

//			switch (this.JobActionType)
//			{
//				case PackageProcessingActionType.RequestReceivedResponseSent:

//				case PackageProcessingActionType.ResponseReceived:

//					//int receivedPackageLength = reader.ReadInt32Optimized();
//					//int receivedPackagePosition = (int)reader.BaseStream.Position;
//					//reader.BaseStream.Position += receivedPackageLength;
//					//int sentPackageLength = reader.ReadInt32Optimized();
//					//int sentPackagePosition = (int)reader.BaseStream.Position;
//					//byte[] streamBuffer = reader.BaseStream.ToArray(); // The reader is BufferStream so GetBuffer() does not helps;

//					//ArraySegment<byte> receivedPackageSegment = new ArraySegment<byte>(streamBuffer, receivedPackagePosition, receivedPackageLength);
//					//BufferStream receivedBufferStream = new BufferStream();
//					//receivedBufferStream.Initialize(new ArraySegment<byte>[] { receivedPackageSegment });
//					//this.receivedPackage = PackageEngine.CreatePackageOnReceive(receivedBufferStream as IBufferStream, packageLengthIncluded: false);

//					//ArraySegment<byte> sentPackageSegment = new ArraySegment<byte>(streamBuffer, sentPackagePosition, sentPackageLength);
//					//BufferStream sentBufferStream = new BufferStream();
//					//sentBufferStream.Initialize(new ArraySegment<byte>[] { sentPackageSegment });
//					//this.sentPackage = PackageEngine.CreatePackageOnReceive(sentBufferStream as IBufferStream, packageLengthIncluded: false);
//					////this.receivedDataReader = reader;

//					////int receivedPackageLength = reader.ReadInt32Optimized();
//					////int sentPackagePosition = (int)reader.BaseStream.Position + receivedPackageLength;
//					////ArraySegment<byte> receivedPackageSegment = new ArraySegment<byte>((reader.BaseStream.GetArraySegment  as BufferStream).Buffers[0].ToArray(), (int)reader.BaseStream.Position, receivedPackageLength);
//					////BufferStream receivedPackagBufferStream = new BufferStream();
//					////receivedPackagBufferStream.Initialize(new ArraySegment<byte>[] { receivedPackageSegment });

//					////this.receivedPackage = PackageManager.CreatePackage(receivedPackagBufferStream as IBufferStream, packageLengthIncluded: false);

//					////int sentPackageLength = (int)reader.BaseStream.Length - sentPackagePosition;
//					////ArraySegment<byte> sentPackageSegment = new ArraySegment<byte>(reader.BaseStream.GetBuffer(), sentPackagePosition, sentPackageLength);
//					////BufferStream sentPackagBufferStream = new BufferStream();
//					////sentPackagBufferStream.Initialize(new ArraySegment<byte>[] { sentPackageSegment });
//					////this.sentPackage = PackageManager.CreatePackage(sentPackagBufferStream as IBufferStream, packageLengthIncluded: true);

//					break;

//				case PackageProcessingActionType.MessageSent:

//					//this.sentPackage = PackageEngine.CreatePackageOnReceive(reader, packageLengthIncluded: true);
//					break;

//				case PackageProcessingActionType.MessageReceived:

//					//this.receivedPackage = PackageEngine.CreatePackageOnReceive(reader, packageLengthIncluded: true);
//					break;
//			}


//			//this.sentPackage = null;
//			//int sentPackageLength = reader.ReadInt32Optimized();
//			////ArraySegment<byte> sentPackageLengthSegment = PackageManager.CreatePackageLengthBy7BitEncodedSignedInt32ArraySegment(sentPackageLength);
//			//this.SentData = new byte[sentPackageLength];

//			//reader.ReadBytesDirect(this.SentData, 0, sentPackageLength);
//			////this.SentData = new ArraySegment<byte>(sentPackageDataSegment);
//			////this.SentData = new ArraySegment<byte>[] { new ArraySegment<byte>(sentData) };

//			//this.receivedPackage = null;
//			//this.receivedData = new byte[reader.BaseStream.Length - reader.BaseStream.Position];

//			//reader.ReadBytesDirect(this.receivedData, 0, receivedData.Length);

//			////this.receivedPackage = PackageManager.CreatePackage(reader);
//		}

//		private void SetKeyAndToken()
//		{
//			if (this.JobActionType == PackageProcessingActionType.MessageSent)
//			{
//				this.packageKey = this.SentPackage!.Key;
//				this.packageToken = this.SentPackage.Token;
//			}
//			else
//			{
//				this.packageKey = this.ReceivedPackage!.Key;
//				this.packageToken = this.ReceivedPackage.Token;
//			}
//		}
//	}
//}
