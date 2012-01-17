using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

using e24PaymentPipe.Exceptions;

namespace e24PaymentPipe
{
	/// <summary>
	/// Handles the communication with the payment gateway
	/// <example>
	/// <code>
	/// var paymentServer = new PaymentServerUrlBuilder("bankserver.example.com","/context", UseSSL.on, 443);
	///Uri paymentServerUrl = paymentServer.ToUrl();
	///
	///var init=new PaymentInitMessage(
	///    id: "yourId",
	///    password: "yourPassword",
	///    action: RequiredAction.Authorization,
	///    amount: 5.30,
	///    language: AcceptedLanguage.ITA,
	///    responseURL: new Uri("http://www.example.com/TransactionOK.htm"),
	///    errorURL: new Uri("http://www.example.com/TransactionKO.htm"),
	///    trackId: new Guid().ToString(),
	///    currency: 978
	///    );
	///
	///var pipe = new PaymentPipe(paymentServerUrl);
	///PaymentDetails paymentdetails=null;
	///try
	///{
	///   paymentdetails = pipe.performPaymentInitialization(init);
	///}
	///catch(BadResponseFromWebServiceException ex)
	///{
	///   Console.WriteLine("Error! AttemptedUrl:", ex.AttemptedUrl);
	///    Console.WriteLine("Error! AttemptedParameters:", ex.AttemptedParams);
	///}
	///
	///Console.WriteLine("paymentID: {0}", paymentdetails.paymentId);
	///Console.WriteLine("paymentpage: {0}", paymentdetails.paymentPage);
	/// </code>
	/// </example>
	/// </summary>
	public sealed class PaymentPipe
	{
		/// <summary>
		/// Url for the payment gateway (should be given by the provider)
		/// </summary>
		public Uri PaymentServerUrl {get; private set;}
		
		/// <summary>
		/// default constructor
		/// </summary>
		/// <param name="paymentServerUrl"><see cref="PaymentServerUrl" /></param>
		/// <exception cref="ArgumentNullException">thrown if a null paymentServerUrl is
		/// provided</exception>
		public PaymentPipe(Uri paymentServerUrl)
		{
			if(paymentServerUrl==null) throw new ArgumentNullException("paymentServerUrl");
			this.PaymentServerUrl=paymentServerUrl;
		}
		

		/// <summary>
		/// sends a payment initialization message to the payment gateway
		/// </summary>
		/// <param name="initMessage">instance of PaymentInitMessage that wraps the
		/// details for the requested operation</param>
		/// <param name="timeout">After this time (in milliseconds), the method will stop waiting for an
		/// answer from the payment gateway. It's an optional parameter.Default 5000 milliseconds.</param>
		/// <returns>PaymentDetails for the requested operation</returns>
		/// <exception cref="e24PaymentPipe.Exceptions.BadResponseFromWebServiceException"></exception>
		public PaymentDetails PerformPaymentInitialization(PaymentInitMessage initMessage, int timeout=5000)
		{
			if(initMessage==null) throw new ArgumentNullException("initMessage");

			Uri url=BuildUrl("PaymentInitHTTPServlet");
			
			string response = sendMessage(url,initMessage.ToUrlParameters(),timeout);
			
			if(String.IsNullOrEmpty(response) || response.StartsWith("!ERROR!") || !response.Contains(":"))
			{
				throw new BadResponseFromWebServiceException(response, url.ToString(),initMessage.ToUrlParameters());
			}

			var parsedrsp=ParseResponse(response);
			
			return new PaymentDetails(parsedrsp[0], string.Format("{0}:{1}?PaymentID={2}",parsedrsp[1], parsedrsp[2], parsedrsp[0]));
		}

		private Uri BuildUrl(string servletName)
		{
			var sb = new StringBuilder(this.PaymentServerUrl.AbsoluteUri);
			sb.Append("servlet/");
			sb.Append(servletName);
			return new Uri(sb.ToString());
		}
		
		private string sendMessage(Uri url, string data, int timeout=5000)
		{
			WebRequest webRequest = WebRequest.Create(url);
			webRequest.Method = "POST";
			webRequest.Timeout = timeout; // wait for 5 seconds and then timeout
			
			webRequest.ContentType = "application/x-www-form-urlencoded";
			
			using(Stream reqStream = webRequest.GetRequestStream())
			{
				byte[] postArray = Encoding.ASCII.GetBytes(data);
				reqStream.Write(postArray, 0, postArray.Length);
				reqStream.Close();
			}
			using(StreamReader sr = new StreamReader(webRequest.GetResponse().GetResponseStream())){
				string resp = sr.ReadToEnd();
				return resp;
			}

		}

		private string[] ParseResponse(string response)
		{
			return response.Split(':');
		}
	}
}