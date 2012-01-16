/*
 * Created by SharpDevelop.
 * User: paolo
 * Date: 13/01/2012
 * Time: 21:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using NUnit.Framework;
using e24PaymentPipe;

namespace e24PaymentPipeTests
{
	[TestFixture]
	public class ServerUrlBuilderTest
	{
		[Test]
		public void ServerBuildUrl()
		{
			var si = new PaymentServerUrlBuilder(
				WebAddress: "test4.constriv.com",
				Context: 	"/cg301",
				SSL:		UseSSL.off,
				Port:		80);
			
			var url=si.ToUrl();
			Console.Error.WriteLine(url);
			
			Assert.AreEqual(new Uri("http://test4.constriv.com:80/cg301/"),url);
			Assert.AreEqual("http://test4.constriv.com/cg301/",si.ToString());
			
		}
	}
}
