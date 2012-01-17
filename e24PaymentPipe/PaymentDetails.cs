using System;

namespace e24PaymentPipe
{
	/// <summary>
	/// PaymentDetails immutable class, wraps the PaymentId and the Payment page
	/// returned by the payment gateway
	/// </summary>
	public sealed class PaymentDetails
	{
		/// <summary>
		/// Identifier for the requested payment
		/// </summary>
		public string PaymentId {get; private set;}
		
		/// <summary>
		/// identifier for the payment page
		/// </summary>
	    public string PaymentPage{get; private set;}
	    
	    /// <summary>
	    /// default constructor for the class
	    /// </summary>
	    /// <param name="paymentId"><see cref="PaymentId" /></param>
	    /// <param name="paymentPage"><see cref="PaymentPage" /></param>
		public PaymentDetails(string paymentId, string paymentPage)
		{
			this.PaymentId=paymentId;
			this.PaymentPage=paymentPage;
		}
	}
}
