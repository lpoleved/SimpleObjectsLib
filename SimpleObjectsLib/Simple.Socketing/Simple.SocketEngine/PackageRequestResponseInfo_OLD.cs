//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Simple;

//namespace Simple.SocketEngine
//{
//	public class PackageRequestResponseInfo 
//	{
//		public PackageRequestResponseInfo(PackageReader requestPackage, Func<ResponseArgs> getResponseArgs)
//		{
//			//this.RequestPackageLength = requestPackageLength;
//			//this.RequestHeaderAndBody = requestHeaderAndBody;
//			this.RequestPackage = requestPackage;
//			this.GetResponseArgs = getResponseArgs;
//		}

//		//public ArraySegment<byte> RequestPackageLength { get;  private set; }
//		//public ArraySegment<byte> RequestHeaderAndBody { get; private set; }

//		public PackageReader RequestPackage { get; private set; }
//		public Func<ResponseArgs> GetResponseArgs { get; private set; }

//		public ResponseArgs ResponseArgs { get; set; }
//		public PackageReader ResponsePackage { get; set; }
//		public bool ResponseSucceeded { get; set; }
// 		public string ErrorMessage { get; set; }
//		public RequestResultInfo ResultInfo { get; set; }

//		//IRequestResult IRequestResponseInfo.RequestResult
//		//{
//		//	get { return this.RequestResult; }
//		//	set { this.RequestResult = value as RequestResult<TResult>; }
//		//}
//	}

//	//public interface IRequestResponseInfo<TResult> : IRequestResponseInfo
//	//{
//	//	new RequestResult<TResult> RequestResult { get; set; }
//	//}

//	//public interface IRequestResponseInfo
//	//{
//	//	ArraySegment<byte> RequestPackageLength { get; }
//	//	ArraySegment<byte> RequestHeaderAndBody { get; }
//	//	Func<ResponseArgs> GetResponseArgs { get; }

//	//	ResponseArgs ResponseArgs { get; set; }
//	//	PackageInfo ResponsePackage { get; set; }
//	//	IRequestResult RequestResult { get; set; }
//	//}

//	//public class RequestResponseInfo<TResult> : IRequestResponseInfo
//	//{
//	//	public RequestResponseInfo(Func<ResponseArgs> getResponseArgs)
//	//		: base(getResponseArgs)
//	//	{
//	//	}
//	//	public new RequestResult<TResult> RequestResult
//	//	{
//	//		get { return base.RequestResult as RequestResult<TResult>; }
//	//		set { base.RequestResult = value; }
//	//	}

//	//	public ResponseArgs ResponseArgs { get; set; }
//	//	public Func<ResponseArgs> GetResponseArgs { get; private set; }
//	//	public PackageInfo Package { get; set; }
//	//	public RequestResult<TResult> RequestResult { get; set; }
//	//}


//	//public class RequestResponseInfo
//	//{
//	//	public RequestResponseInfo(Func<ResponseArgs> getResponseArgs)
//	//	{
//	//		this.GetResponseArgs = getResponseArgs;
//	//	}

//	//	public ResponseArgs ResponseArgs { get; set; }
//	//	public Func<ResponseArgs> GetResponseArgs { get; private set; }
//	//	public PackageInfo Package { get; set; }
//	//	public IRequestResult RequestResult { get; set; }
//	//}

//}
