using System;
using System.Globalization;
using System.Text;

namespace e24PaymentPipe
{
	/// <summary>
	/// Required action for the payment gateway
	/// </summary>
	public enum RequiredAction {
		/// <summary>
		/// Action "purchase"
		/// </summary>
		Purchase=1,
		
		/// <summary>
		/// action to credit back the user with the amount
		/// </summary>
		Credit=2,
		
		/// <summary>
		/// action to request an authorization without taking the money
		/// from the customer's account
		/// </summary>
		Authorization=4,
		
		/// <summary>
		/// money will be captured but not taken from the customer's account
		/// </summary>
		Capture=5,
		
		/// <summary>
		/// void a transaction
		/// </summary>
		Void=9
	}

	/// <summary>
	/// Languages required for the payment page
	/// (note: not all the payment gateways may support the same languages)
	/// </summary>
	public enum RequiredLanguage {
		/// <summary>
		/// Italian
		/// </summary>
		ITA,
		
		/// <summary>
		/// US English
		/// </summary>
		USA,
		
		/// <summary>
		/// French
		/// </summary>
		FRA,
		
		/// <summary>
		/// German
		/// </summary>
		DEU,
		
		/// <summary>
		/// Spanish
		/// </summary>
		ESP,
		
		/// <summary>
		/// Slovenian
		/// </summary>
		SLO
	}

	/// <summary>
	/// Initialization message for payment. The class is immutable.
	/// </summary>
	/// <example>
	///<code>
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
	/// </code>
	///</example>
	public sealed class PaymentInitMessage
	{
		/// <summary>
		/// Id for the payment gateway
		/// </summary>
		public string Id {get; private set;}
		
		/// <summary>
		/// password for the payment gateway
		/// </summary>
		public string Password {get; private set;}
		
		/// <summary>
		/// Action required
		/// </summary>
		public RequiredAction RequiredAction{get; private set;}
		
		/// <summary>
		/// Amount of the transaction in the format ###.##
		/// </summary>
		/// <seealso cref="Currency"></seealso>
		public double Amount{get; private set;}
		
		/// <summary>
		/// url where the user will be redirected in case of succesfull
		/// transaction
		/// </summary>
		public Uri ResponseURL{get; private set;}
		
		/// <summary>
		/// trackId
		/// </summary>
		public string TrackId{get; private set;}
		
		/// <summary>
		/// User defined field. See the specifications from your payment gateway
		/// to see if this is used and how
		/// </summary>
		public string Udf1{get; private set;}

		/// <summary>
		/// User defined field. See the specifications from your payment gateway
		/// to see if this is used and how
		/// </summary>
		public string Udf2{get; private set;}
		
		/// <summary>
		/// User defined field. See the specifications from your payment gateway
		/// to see if this is used and how
		/// </summary>
		public string Udf3{get; private set;}
		
		/// <summary>
		/// User defined field. See the specifications from your payment gateway
		/// to see if this is used and how
		/// </summary>
		public string Udf4{get; private set;}
		
		/// <summary>
		/// User defined field. See the specifications from your payment gateway
		/// to see if this is used and how
		/// </summary>
		public string Udf5{get; private set;}
		
		/// <summary>
		/// ISO-4127 currency numeric code
		/// </summary>
		/// <seealso cref="Amount"></seealso>
		public int Currency {get; private set;}
		
		/// <summary>
		/// url where the user will be redirected in case of unsuccesfull
		/// transaction
		/// </summary>
		public Uri ErrorURL {get; private set;}
		
		/// <summary>
		/// Language required for the payment pages (not all providers may support
		/// the same languages, so you may need tweaking the RequiredLanguage enum
		/// to add different languages)
		/// </summary>
		/// <seealso cref="e24PaymentPipe.RequiredLanguage"></seealso>
		public RequiredLanguage Language {get; private set;}
		
		/// <summary>
		/// default constructor for the class, initializes the required properties
		/// </summary>
		/// <param name="id"><see cref="Id"></see></param>
		/// <param name="password"><see cref="Password"></see></param>
		/// <param name="action"><see cref="Action"></see></param>
		/// <param name="amount"><see cref="Amount"></see></param>
		/// <param name="language"><see cref="Language"></see></param>
		/// <param name="responseURL"><see cref="ResponseURL" /></param>
		/// <param name="errorURL"><see cref="ErrorURL" /></param>
		/// <param name="trackId"><see cref="TrackId" /></param>
		/// <param name="currency"><see cref="Currency" /></param>
		/// <param name="udf1"><see cref="Udf1" /></param>
		/// <param name="udf2"><see cref="Udf2" /></param>
		/// <param name="udf3"><see cref="Udf3" /></param>
		/// <param name="udf4"><see cref="Udf4" /></param>
		/// <param name="udf5"><see cref="Udf5" /></param>
		public PaymentInitMessage(
			string id,
			string password,
			RequiredAction action,
			double amount,
			RequiredLanguage language,
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
		
		/// <summary>
		/// builds the url parameters to be sent via POST to the payment gateway
		/// </summary>
		/// <returns>list of parameters concatenated by &amp;</returns>
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
