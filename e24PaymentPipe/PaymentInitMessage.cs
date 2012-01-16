/*
 * Created by SharpDevelop.
 * User: paolo
 * Date: 11/01/2012
 * Time: 17:13
 * 
 */
using System;
using System.Globalization;
using System.Text;

namespace e24PaymentPipe
{
	public enum RequiredAction {
		Purchase=1,
		Credit=2,
		Authorization=4,
		Capture=5,
		Void=9
	}

	public enum AcceptedLanguage {
		ITA,
		USA,
		FRA,
		DEU,
		ESP,
		SLO
	}

	/// <summary>
	/// Initialization message for payment
	/// </summary>
	public sealed class PaymentInitMessage
	{
		public string Id {get; private set;}
		public string Password {get; private set;}
		public RequiredAction RequiredAction{get; private set;}
		public double Amount{get; private set;}
		public Uri ResponseURL{get; private set;}
		public string TrackId{get; private set;}
		public string Udf1{get; private set;}
		public string Udf2{get; private set;}
		public string Udf3{get; private set;}
		public string Udf4{get; private set;}
		public string Udf5{get; private set;}
		public int Currency {get; private set;}
		public Uri ErrorURL {get; private set;}
		public AcceptedLanguage Language {get; private set;}
  
		public PaymentInitMessage(
			string id,
		    string password,
		    RequiredAction action,
		    double amount,
		    AcceptedLanguage language,
		    Uri responseURL,
		    Uri errorURL,
		    string trackId,
		    int currency=978,
		    string udf1="",
		    string udf2="",
		    string udf3="",
		    string udf4="",
		    string udf5=""
		   )
		{
			this.Id = id;
			this.Password = password;
			this.RequiredAction = action;
			this.Amount = amount;
			this.ResponseURL = responseURL;
			this.TrackId = trackId;
			this.Udf1 = udf1;
			this.Udf2 = udf2;
			this.Udf3 = udf3;
			this.Udf4 = udf4;
			this.Udf5 = udf5;
			this.Currency = currency;
			this.ErrorURL = errorURL;
			this.Language = language;
		}
		
		public string ToUrlParameters() {
			StringBuilder buf = new StringBuilder();
			if (this.Id.Length > 0)
				buf.Append("id=" + Uri.EscapeDataString(this.Id) + "&");
			if (this.Password.Length > 0)
				buf.Append("password=" + Uri.EscapeDataString(this.Password) + "&");
			if (this.Amount > 0)
				buf.Append("amt=" + Uri.EscapeDataString(this.Amount.ToString("0.00",CultureInfo.InvariantCulture)) + "&");
			if (this.Currency > 0 && this.Currency<=999)
				buf.Append("currencycode=" + Uri.EscapeDataString(this.Currency.ToString("000")) + "&");
			buf.Append("action=" + Uri.EscapeDataString(this.RequiredAction.ToString("d")) + "&");
			buf.Append("langid=" + Uri.EscapeDataString(this.Language.ToString().ToUpper()) + "&");
			if (this.ResponseURL != null)
				buf.Append("responseURL=" + Uri.EscapeDataString(this.ResponseURL.AbsoluteUri) + "&");
			if (this.ErrorURL != null)
				buf.Append("errorURL=" + Uri.EscapeDataString(this.ErrorURL.AbsoluteUri) + "&");
			if (this.TrackId.Length > 0)
				buf.Append("trackid=" + Uri.EscapeDataString(this.TrackId) + "&");
			if (this.Udf1.Length > 0)
				buf.Append("udf1=" + Uri.EscapeDataString(this.Udf1) + "&");
			if (this.Udf2.Length > 0)
				buf.Append("udf2=" + Uri.EscapeDataString(this.Udf2) + "&");
			if (this.Udf3.Length > 0)
				buf.Append("udf3=" + Uri.EscapeDataString(this.Udf3) + "&");
			if (this.Udf4.Length > 0)
				buf.Append("udf4=" + Uri.EscapeDataString(this.Udf4) + "&");
			if (this.Udf5.Length > 0)
				buf.Append("udf5=" + Uri.EscapeDataString(this.Udf5) + "&");
			
			return buf.ToString().TrimEnd('&');
		}
	}
}
