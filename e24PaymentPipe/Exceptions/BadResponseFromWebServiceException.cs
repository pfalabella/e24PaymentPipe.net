/*
 * Created by SharpDevelop.
 * User: paolo
 * Date: 12/01/2012
 * Time: 15:26
 */
using System;

namespace e24PaymentPipe.Exceptions
{
	/// <summary>
	/// Error coming from web service.
	/// </summary>
	[Serializable]
	public class BadResponseFromWebServiceException : Exception
	{
		public string AttemptedUrl{get; private set;}
		public string AttemptedParams{get; private set;}
		
		public BadResponseFromWebServiceException(): base() {}
	
		public BadResponseFromWebServiceException(string msg, string AttemptedUrl, string AttemptedParams) : base(msg) 
		{
			this.AttemptedUrl=AttemptedUrl;		
			this.AttemptedParams = AttemptedParams;
		}
		
		public BadResponseFromWebServiceException(string msg, string AttemptedUrl, Exception inner) : base(msg,inner) 
		{
			this.AttemptedUrl=AttemptedUrl;		
			this.AttemptedParams = AttemptedParams;
		}		
	}
}
