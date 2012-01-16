e24PaymentPipe
==============

A .net implementation of the ACI Worldwide Payment Gateway that is used by many payment gateway providers.

    Quick start
    ------------
        ```
        var paymentServer = new PaymentServerUrlBuilder("bankserver.example.com","/context", UseSSL.on, 443);
        paymentServerUrl = paymentServer.ToUrl();

        var init=new PaymentInitMessage(
            id: "yourId",
            password: "yourPassword",
            action: RequiredAction.Authorization,
            amount: 5.30,
            language: AcceptedLanguage.ITA,
            responseURL: new Uri("http://www.example.com/TransactionOK.htm"),
            errorURL: new Uri("http://www.example.com/TransactionKO.htm"),
            trackId: new Guid().ToString(),
            currency: 978     
            );

        var pipe = new PaymentPipe(paymentServerUrl);
        PaymentDetails paymentdetails=null;
        try 
        {
            paymentdetails = pipe.performPaymentInitialization(init);
        }
        catch(BadResponseFromWebServiceException ex)
        {
            Console.WriteLine("Error! AttemptedUrl:", ex.AttemptedUrl);
            Console.WriteLine("Error! AttemptedParameters:", ex.AttemptedParams);				
        }

        Console.WriteLine("paymentID: {0}", paymentdetails.paymentId);
        Console.WriteLine("paymentpage: {0}", paymentdetails.paymentPage);
        ```
