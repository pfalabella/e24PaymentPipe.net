using System;
using System.Text;

namespace e24PaymentPipe
{
	/// <summary>
	/// 1 if the url is on https, 0 otherwise
	/// </summary>
	public enum UseSSL {
		/// <summary>
		/// http will be used
		/// </summary>
		off=0,
		
		/// <summary>
		/// secure (https) connection with the server will be used
		/// </summary>
		on=1
	}
	/// <summary>
	/// Identifies the server that is going to process the payment info.
	/// <example>
	/// <code>
	/// var paymentServer = new PaymentServerUrlBuilder("bankserver.example.com","/context", UseSSL.on, 443);
	/// Uri paymentServerUrl = paymentServer.ToUrl();
	/// </code>
	/// </example>
	/// </summary>
	public class PaymentServerUrlBuilder
	{
		/// <summary>
		/// UseSSL.on if the url is on https, UseSSL.off otherwise
		/// </summary>
		public UseSSL SSL {get; private set;}
		
		/// <summary>
		/// url given by the payment provider for the server that processes the payment requests
		/// </summary>
		public string WebAddress {get; private set;}
		
		/// <summary>
		/// port number (nullable)
		/// </summary>
		public int? Port {get; private set;}

		/// <summary>
		/// context string given by the payment provider. 
		/// It gets included in the url to be called.
		/// </summary>
		public string Context {get; private set;}

		/// <summary>
		/// default constructor. It initializes all the values.
		/// </summary>
		/// <param name="webAddress"><see cref="WebAddress"/></param>
		/// <param name="context"><see cref="Context"/></param>
		/// <param name="ssl">optional parameter, defaults to off
		/// 	<see cref="UseSSL" /></param>
		/// <param name="port">optional parameter, defaults to null (default port)
		/// <see cref="Port"/></param>
		public PaymentServerUrlBuilder(string webAddress, string context="", UseSSL ssl=UseSSL.off, int? port=null)
		{
			if(String.IsNullOrWhiteSpace(webAddress)) throw new ArgumentNullException(webAddress);
			
			this.SSL = ssl;
			this.Port = port;
			this.WebAddress = webAddress;
			this.Context=context;
		}
		
		/// <summary>
		/// builds the url to the payment server
		/// </summary>
		/// <returns>Uri for the payment server</returns>
		public Uri ToUrl()
		{
			var urlBuf = new StringBuilder();
			if (this.SSL == UseSSL.on)
			{
				urlBuf.Append("https://");
			}
			else
			{
				urlBuf.Append("http://");
			}

			if (String.IsNullOrWhiteSpace(this.WebAddress))
			{
				throw new ArgumentNullException(this.WebAddress );
			}
			else
			{
				urlBuf.Append(this.WebAddress);
			}
			
			
			if (this.Port.HasValue && this.Port.Value>0)
			{
				urlBuf.Append(":");
				urlBuf.Append(this.Port);
			}
			
			if (!String.IsNullOrWhiteSpace(this.Context))
			{
				if (!this.Context.StartsWith("/")) urlBuf.Append("/");
				urlBuf.Append(this.Context);
				if (!this.Context.EndsWith("/")) urlBuf.Append("/");
			}
			else
			{
				urlBuf.Append("/");
			}
			
			return new Uri(urlBuf.ToString());
		}
		
		/// <summary>
		/// overridden ToString that returns the url in string format
		/// </summary>
		/// <returns>the url as a string</returns>
		public override string ToString()
		{
			return this.ToUrl().ToString();
		}

	}

}
