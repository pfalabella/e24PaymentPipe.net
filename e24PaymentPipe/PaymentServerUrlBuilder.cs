using System;
using System.Text;

namespace e24PaymentPipe
{
	public enum UseSSL {
		off=0,
		on=1
	}
	/// <summary>
	/// Identifies the server that is going to process the payment info
	/// </summary>
	public class PaymentServerUrlBuilder
	{
		public UseSSL SSL {get; private set;}
		public string WebAddress {get; private set;}
		public int? Port {get; private set;}
		public string Context {get; private set;}

		public PaymentServerUrlBuilder(string WebAddress, string Context="", UseSSL SSL=UseSSL.off, int? Port=null)
		{
			if(String.IsNullOrWhiteSpace(WebAddress)) throw new ArgumentNullException(WebAddress);
			
			this.SSL = SSL;
			this.Port = Port;
			this.WebAddress = WebAddress;
			this.Context=Context;
		}
		
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
		
		public override string ToString()
		{
			return this.ToUrl().ToString();
		}

	}

}
