using System;

namespace e24PaymentPipe.Exceptions
{
	/// <summary>
	/// Error coming from web service.
	/// </summary>
	[Serializable]
	public class BadResponseFromWebServiceException : Exception
	{
		/// <summary>
		/// Url that returned the exception
		/// </summary>
		public string AttemptedUrl{get; private set;}
		
		/// <summary>
		/// parameters that were sent in POST to the url and caused the exception
		/// </summary>
		public string AttemptedParams{get; private set;}
		
		/// <summary>
		/// default (parameterless) constructor
		/// </summary>
		public BadResponseFromWebServiceException(): base() {}
	
		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="msg">message returned by the payment provider</param>
		/// <param name="AttemptedUrl"><see cref="AttemptedUrl"></see></param>
		/// <param name="AttemptedParams"><see cref="AttemptedParams"></see></param>
		public BadResponseFromWebServiceException(string msg, string AttemptedUrl, string AttemptedParams) : base(msg) 
		{
			this.AttemptedUrl=AttemptedUrl;		
			this.AttemptedParams = AttemptedParams;
		}
		
		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="msg">message returned by the payment provider</param>
		/// <param name="AttemptedUrl"><see cref="AttemptedUrl"></see></param>
		/// <param name="AttemptedParams"><see cref="AttemptedParams"></see></param>
		/// <param name="inner">Inner exception</param>
		public BadResponseFromWebServiceException(string msg, string AttemptedUrl, string AttemptedParams, Exception inner) : base(msg,inner) 
		{
			this.AttemptedUrl=AttemptedUrl;		
			this.AttemptedParams = AttemptedParams;
		}		
	}
}
