using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CommonLibs.Web.LongPolling.Utils;

namespace WebTest01
{
	public partial class Test01Crash : System.Web.UI.Page
	{
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			// Some parameters to pass to JavaScript
			var parameters = ((SiteMaster)Master).PageParameters;
			parameters[ "pHelloMessage" ]	= pHelloMessage.ClientID;
			parameters[ "helloMsg" ]		= "Hello world";
		}

		/// <summary>
		/// Message handler that throws an exception
		/// </summary>
		public static void TestCrashHandler(CommonLibs.Web.LongPolling.Message requestMessage)
		{
			int a = 0;
			int b = 1;
			a = b/a;
		}
	}
}
