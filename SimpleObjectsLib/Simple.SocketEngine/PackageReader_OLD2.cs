//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.IO;
//using System.Threading;
//using Simple;
//using Simple.Serialization;
//using SuperSocket.ProtoBase;
//using System.Buffers;

//namespace Simple.SocketEngine
//{
//	/// <summary>
//	/// The basic package info interface with key
//	/// </summary>
//	public class PackageReader_OLD2 : IKeyedPackageInfo<int> //, IPackageEncoder<SimplePackageWriter> //, IRequestInfo //, IBufferedPackageInfo
//	{
//		//private BufferList data = null;
//		//private long packageDataLength = 0;
//		//private bool packageDataLengthReceived = false;
//		//private long totalPackageLength = 0;
//		//private int bytesConsumed = 0;

//		///// <summary>
//		///// Call empty constructor when readind data.
//		///// </summary>
//		//public PackageReader()
//		//{
//		//}

//		//public PackageReader(PackageFlags flags, int key, SerializationReader reader) 
//		//	: this(flags, key, token: 0, reader) { }

//		/// <summary>
//		/// Call this constructor when decoding the data.
//		/// </summary>
//		/// <param name="key">The package key. The response has no key.</param>
//		/// <param name="flags">The package flags</param>
//		/// <param name="reader"></param>
//		public PackageReader_OLD2(PackageFlags flags, int key, int token, SerializationReader reader)
//		{
//			this.Key = key;
//			this.Flags = flags;
//			this.Token = token;
//			this.Reader = reader;
//		}

//		// The unique package type identifier: MessageId or RequestId. If the PackageFlags.PackageType equals PackageType.Message it is MessageId.
//		// If The PackageType is PackageType.Request or PackageType.Response it is ReqiestId.
//		public PackageFlags Flags { get; private set; }
//		public int Key { get; private set; }
//		public int Token { get; private set; }
		
//		public SerializationReader Reader { get; private set; }

//		//public bool IsPackageReadyForEncode(ref ReadOnlySequence<byte> buffer)
//		//{
//		//	if (!this.packageDataLengthReceived)
//		//	{
//		//		if (!PackageEngine.TryGetInt64By7BitEncoding(ref reader, ref this.packageDataLength, ref bytesConsumed))
//		//			return default(PackageReader); // not enough data for package length. Wait for more...

//		//		this.packageDataLengthReceived = true;
//		//		this.totalPackageLength = this.bytesConsumed + this.packageDataLength; //  reader.Consumed = reader.Position.GetInteger()
//		//	}

//		//	long rest = this.totalPackageLength - reader.Length;

//		//	if (rest > 0) // Haven't received a full request package -> there is more data...
//		//		return default(PackageReader);

//		//	var sequence = reader.Sequence;

//		//	try
//		//	{
//		//		return this.DecodePackage(ref sequence);
//		//	}
//		//	finally
//		//	{
//		//		reader.Advance(reader.Consumed);
//		//	}

//		//}

//		//public int Encode(IBufferWriter<byte> writer, SimplePackageWriter package)
//		//{

//		//	SerializationWriter packageWriter = new SerializationWriter() { Encoding = package.Encoding };

//		//	packageWriter.WriteUInt64Optimized(package.Flags.Value);
//		//	packageWriter.WriteInt32Optimized(package.Key);
//		//	package.PackageArgs?.WriteTo(packageWriter);

//		//	var packageLengthData = PackageEngine.CreatePackageLengthSpanBy7BitEncoding(packageWriter.Length);

//		//	writer.Write(packageLengthData);
//		//	writer.Write(packageWriter.ToArray());

//		//	return (int)(packageLengthData.Length + packageWriter.Length);


//		//	//SerializationWriter serWriter = new SerializationWriter(writer);

//		//	//serWriter.WriteInt32Optimized(packageKey);
//		//	//serWriter.WriteUInt64Optimized(packageFlags.Value);

//		//	//if (packageFlags.PackageType != PackageType.Message)
//		//	//    writer.WriteInt32Optimized(token);

//		//	////if (packageArgsBody != null)
//		//	////{
//		//	////	packageArgsBody.WriteTo(writer);
//		//	////	//writer.Update();
//		//	////}

//		//	//return writer;


//		//	// TODO:

//		//	//ArraySegment<byte> responseHeaderaAndBody = this.CreateResponseHeaderAndBodyDataToSend(requestId, package.Token, responseSucceeded, package.Flags.IsSystem, isBroadcast: false, isMulticast: false, packageArgsBody: requestResultArgs);
//		//	//ArraySegment<byte> responsePackageLength = PackageEngine.CreatePackageLengthBy7BitEncoding(responseHeaderaAndBody.Count);

//		//	//this.Session.SendPackageAsync(new ArraySegment<byte>[] { responsePackageLength, responseHeaderaAndBody });
//		//	//writer.Write<byte>(package.re)



//		//	//return writer.Write(pack, this.encoding);
//		//}


//		//public void WriteTo(SerializationWriter writer)
//		//{
//		//	writer.WriteByteArray(this.Reader.ToArray());
//		//	//writer.WriteBytesDirect(this.Reader.BaseStream.GetBuffer(),)

//		//	//Stream baseStream = this.Reader.BaseStream;

//		//	//if (baseStream is BufferStream)
//		//	//{
//		//	//	var buffers = (baseStream as BufferStream).Buffers;

//		//	//	foreach (ArraySegment<byte> buffer in buffers)
//		//	//		writer.WriteBytesDirect(buffer.Array, buffer.Offset, buffer.Count);
//		//	//}
//		//	//else
//		//	//{
//		//	//	byte[] packageBytes = baseStream.GetBuffer();
//		//	//	writer.WriteBytesDirect(packageBytes, 0, packageBytes.Length);
//		//	//}
//		//}

//		//string IRequestInfo.Key
//		//{
//		//	get { return this.Key.ToString(); }
//		//}

//		//public IList<ArraySegment<byte>> GetArraySegments()
//		//{
//		//	Stream baseStream = this.Reader.BaseStream;

//		//	if (baseStream is BufferStream)
//		//		return (baseStream as BufferStream).Buffers;

//		//	return new ArraySegment<byte>[] { baseStream.GetArraySegment() };
//		//}

//		//public BufferList Data
//		//{
//		//	get
//		//	{
//		//		if (this.data == null && this.Reader.BaseStream is BufferStream)
//		//		{
//		//			this.data = new BufferList();

//		//			foreach (var buffer in (this.Reader.BaseStream as BufferStream).Buffers)
//		//				this.data.Add(buffer); //, new BufferState());
//		//		}

//		//		return this.data;
//		//	}
//		//}

//		//IList<ArraySegment<byte>> IBufferedPackageInfo.Data
//		//{
//		//	get { return (IList<ArraySegment<byte>>)this.Data; }
//		//}

//		//class BufferState : IBufferState
//		//{
//		//	private int m_Reference;

//		//	/// <summary>
//		//	/// Decreases the reference count of this buffer state
//		//	/// </summary>
//		//	/// <returns></returns>
//		//	public int DecreaseReference()
//		//	{
//		//		return Interlocked.Decrement(ref m_Reference) - 1;
//		//	}

//		//	/// <summary>
//		//	/// Increases the reference count of this buffer state
//		//	/// </summary>
//		//	public void IncreaseReference()
//		//	{
//		//		Interlocked.Increment(ref m_Reference);
//		//	}
//		//}
//	}
//}
