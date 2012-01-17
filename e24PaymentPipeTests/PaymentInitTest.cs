using System;
using System.Collections.Generic;
using NUnit.Framework;
using e24PaymentPipe;

namespace e24PaymentPipeTests
{
	[TestFixture]
	public class PaymentInitTest
	{
		[Test]
		public void InitMessageFields()
		{
			PaymentInitMessage pipe = new PaymentInitMessage(
			    id:"id",
 			   	password: "password",
 			    action: RequiredAction.Authorization,
 			    currency: 978,
 			   	language: RequiredLanguage.ITA,
 			   	responseURL: new Uri("http://www.mioserver.it/responseurl"),
 			    errorURL: new Uri("http://www.mioserver.it/errorurl"),
 			   	amount: 33.33, 
 			    trackId: "trackId",
 			    udf1: "details");
			
			Assert.AreEqual("password", pipe.Password);
			Assert.AreEqual("id", pipe.Id);
			Assert.AreEqual(RequiredAction.Authorization, pipe.RequiredAction);
			Assert.AreEqual(978, pipe.Currency);
			Assert.AreEqual(RequiredLanguage.ITA, pipe.Language);
			Assert.AreEqual(new Uri("http://www.mioserver.it/responseurl"), pipe.ResponseURL);
			Assert.AreEqual(new Uri("http://www.mioserver.it/errorurl"), pipe.ErrorURL);
			Assert.AreEqual(33.33, pipe.Amount);
			Assert.AreEqual("trackId", pipe.TrackId);
			Assert.AreEqual("details", pipe.Udf1);					
		}
		
		[Test]
		public void InitGetParameters()
		{
			PaymentInitMessage pipe = new PaymentInitMessage(
			    id:"89025555",
 			   	password: "test",
 			    action: RequiredAction.Authorization,
 			    currency: 978,
 			   	language: RequiredLanguage.ITA,
 			   	responseURL: new Uri("http://www.mioserver.com/Receipt.jsp"),
 			   	errorURL: new Uri("http://www.mioserver.com/Error.jsp"),
 			   	amount: 33.33, 
 			    trackId: "trackId",
 			    udf1: "udf1",
 			    udf2: "udf2",
 			    udf3: "udf3",
 			    udf4: "udf4",
 			    udf5: "udf5");
			
			var urlPars = pipe.ToUrlParameters();
			Console.Error.WriteLine(urlPars);
			Assert.AreEqual(@"id=89025555&password=test&amt=33.33&currencycode=978&action=4&langid=ITA&responseURL=http%3A%2F%2Fwww.mioserver.com%2FReceipt.jsp&errorURL=http%3A%2F%2Fwww.mioserver.com%2FError.jsp&trackid=trackId&udf1=udf1&udf2=udf2&udf3=udf3&udf4=udf4&udf5=udf5",
			                urlPars);
		}		
	}
}