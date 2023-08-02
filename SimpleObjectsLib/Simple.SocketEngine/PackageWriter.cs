using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ProtoBase;
using Simple.Serialization;
using System.Buffers;
using System.IO.Packaging;

namespace Simple.SocketEngine
{

	public class PackageWriter : IKeyedPackageInfo<int>
    {
		//private bool isHeaderWritten = false;
		//private bool isArgsWritten = false;
		internal HeaderInfo headerInfo;

        public PackageWriter(HeaderInfo headerInfo, int key, Encoding characterEncoding, ISimpleSession session, PackageArgs? packageArgs)
        {
            this.headerInfo = headerInfo;
            this.Key = key;
            this.CharacterEncoding = characterEncoding;
			this.Session = session;
            this.PackageArgs = packageArgs;
		}

		public HeaderInfo HeaderInfo => this.headerInfo;
        
        public int Key { get; private set; }

        /// <summary>
        /// The session request-response unique identifier. 
        /// Created by requester and returned back in the respose package to indentify exact requester to whom response data needs to be given.
        /// If package type is message, Token property has no meening.
        /// </summary>
        //public int Token { get; private set; }
        
        public PackageArgs? PackageArgs { get; internal set; }
        
        public Encoding CharacterEncoding { get; private set; }

        //public SerialWriter SerializationWriter { get; set; }

		public ISimpleSession Session { get; set; }

		public byte[]? Buffer { get; internal set; } = null;

		public virtual void WriteHeader(ref SequenceWriter writer)
		{
			//if (this.isHeaderWritten)
			//	return;

			writer.WriteUInt64Optimized(this.HeaderInfo.Value);
            writer.WriteInt32Optimized(this.Key);

            //if (this.HeaderInfo.PackageType != PackageType.Message)
            //    writer.WriteInt32Optimized(this.Token);

			//this.isHeaderWritten = true;

			//if (this.HeaderInfo.PackageType == PackageType.Request && this.HeaderInfo.RecipientModel == RecipientModel.Brodcast)
			//	this.isHeaderWritten = true;

		}

  //      public virtual void WritePackageArgs(SequenceWriter writer)
  //      {
		//	//if (this.isArgsWritten)
		//	//	return;
			
		//	try
		//	{
		//		this.PackageArgs?.WriteTo(this.SerializationWriter!, this.Context);
		//	}
		//	catch (Exception ex) // SerializationWriter needs be rewrited since some data by the package.PackageArgs is written but not all (exception is catched)
		//	{
		//		if (this.SerializationWriter != null)
		//		{
		//			this.WriteHeader();

		//			this.PackageArgs = new ErrorResponseArgs(PackageStatus.ExceptionIsCaughtOnArgsWriting, ex.GetFullErrorMessage());
		//			this.PackageArgs.WriteTo(this.SerializationWriter, this.Context);
		//		}
		//	}

		//	this.isArgsWritten = true;
		//}
	}
}
