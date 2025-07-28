//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Simple.SocketEngine
//{
//	public struct RequestResult<T> where T : ResponseArgs
//	{
//        public RequestResult(T? resultValue)
//			: this(resultValue, RequestInfo.RequestSucceeded)
//        {
//        }

//        public RequestResult(T? resultValue, RequestInfo requestInfo)
//			: this(resultValue, requestInfo, String.Empty)
//        {
//        }

//        public RequestResult(T? resultValue, RequestInfo requestInfo, string message)
//        {
//			this.ResultValue = resultValue;
//			this.ResultInfo = requestInfo;
//			this.Message = message;
//        }

//        public T? ResultValue { get; set; }
//		public RequestInfo ResultInfo { get; set; }
//		public string Message { get; set; }

//		public bool Succeeded => this.ResultInfo == 0;

//		public override string? ToString()
//		{
//			if (this.Succeeded)
//				return this.ResultValue?.ToString() ?? "null"; //  base.ToString();
//			else
//				return this.Message;
//		}

//	}
//}
