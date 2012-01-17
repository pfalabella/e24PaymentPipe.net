using System;
using e24PaymentPipe.Exceptions;
using NUnit.Framework;
using e24PaymentPipe;

namespace e24PaymentPipeTests
{
	[TestFixture]
	public class ConnectionTests
	{
		Uri paymentServerUrl;
		
		[SetUp]
        public void SetUp()
        {
        	var paymentServer = new PaymentServerUrlBuilder("bankserver.example.com","/context", UseSSL.on, 443);
        	paymentServerUrl = paymentServer.ToUrl();
        }
        
		[Test, Explicit("This test actually connects to the bank to initiate a transaction!")]
		public void PaymentInitializationTest() 
		{
			var sku = "SKU";
			var size="medium";
			var desc = "a product";
			string details = sku + "#" + desc + "#" + size;

//			// build Payment Init message
			var init=new PaymentInitMessage(
			 	id: "11111111",
			 	password: "test",
			 	action: RequiredAction.Authorization,
			 	amount: 5.30,
			 	language: RequiredLanguage.ITA,
			 	responseURL: new Uri("http://www.example.com/TransactionOK.htm"),
			 	errorURL: new Uri("http://www.example.com/TransactionKO.htm"),
			 	trackId: new Guid().ToString(),
			 	currency: 978,
			 	udf1:details
			 );
			
			var pipe = new PaymentPipe(paymentServerUrl);
			PaymentDetails paymentdetails=null;
			try 
			{
				paymentdetails = pipe.PerformPaymentInitialization(init);
			}
			catch(BadResponseFromWebServiceException ex)
			{
				Console.Error.WriteLine("attemptedUrl:", ex.AttemptedUrl);
				Console.Error.WriteLine("attemptedParameters:", ex.AttemptedParams);				
			}
			
			Assert.IsNotNull(paymentdetails);
			Console.Error.WriteLine("paymentID: {0}", paymentdetails.PaymentId);
			Console.Error.WriteLine("paymentpage: {0}", paymentdetails.PaymentPage);
		}
	}
}
