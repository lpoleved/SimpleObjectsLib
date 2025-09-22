//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Simple.Serialization;
//using Simple.SocketEngine;

//namespace Simple.Objects.MonitorProtocol
//{
//	public abstract class PackageInfoMessageArgs_OLD : MessageArgs, ISequenceSerializable
//	{
//		private PackageArgsFactory? packageArgsFactory;

//		public PackageInfoMessageArgs_OLD(PackageArgsFactory packageArgsFactory)
//		{
//			this.packageArgsFactory = packageArgsFactory;
//		}

//		public PackageInfoMessageArgs_OLD(PackageReader packageInfo)
//		{
//			this.PackageInfo = packageInfo;
//		}

//		public PackageReader? PackageInfo { get; set; }

//		//public PackageWriter? PackageWriter { get; set; }

//		//protected ArraySegmentListWriter SegmentListWriter { get; private set; } = new ArraySegmentListWriter();


//		// Ensure to have SegmentList writer
//		//public override SerializationWriter CreateWriter() => new SerializationWriter(this.SegmentListWriter); // // this.PackageWritter!.SerializationWriter.ToArraySegmentList());

//		public override void WriteTo(ref SequenceWriter writer, object context)
//		{
//			base.WriteTo(ref writer, context);

//			if (this.PackageInfo?.Buffer != null)
//				writer.Write(this.PackageInfo.Buffer);
//		}

//		public override void ReadFrom(ref SequenceReader reader, object context)
//		{
//			base.ReadFrom(ref reader, context);

//			this.PackageInfo = new PackageReader(this.packageArgsFactory!);
//			//this.PackageInfo.ReadPackageLength();
//            //this.PackageInfo.ReadHeader();
//            // Read Args also nedd to be done!!!!
//        }
//	}
//}
