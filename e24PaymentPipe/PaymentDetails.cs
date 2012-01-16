/*
 * Created by SharpDevelop.
 * User: paolo
 * Date: 10/12/2011
 * Time: 10:50
 */
using System;

namespace e24PaymentPipe
{
	/// <summary>
	/// Description of PaymentDetails.
	/// </summary>
	public sealed class PaymentDetails
	{
		public string paymentId {get; private set;}
	    public string paymentPage{get; private set;}
	    
		public PaymentDetails(string paymentId, string paymentPage)
		{
			this.paymentId=paymentId;
			this.paymentPage=paymentPage;
		}
	}
}
