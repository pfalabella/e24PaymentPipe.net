/*
 * User: paolo
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

using e24PaymentPipe.Exceptions;

namespace e24PaymentPipe
{

	public sealed class PaymentPipe
	{
		public Uri PaymentServerUrl {get; private set;}
		
		public PaymentPipe(Uri paymentServerUrl)
		{
			if(paymentServerUrl==null) throw new ArgumentNullException("paymentServerUrl");
			this.PaymentServerUrl=paymentServerUrl;
		}
		

		public PaymentDetails performPaymentInitialization(PaymentInitMessage initMessage, int timeout=5000)
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

		public Uri BuildUrl(string servletName)
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