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
		private Dictionary<string,object>	ParametersDict				= null;

		protected override void OnInit(EventArgs e)
		{
			ParametersDict = ((SiteMaster)Master).ParametersDict;

			base.OnInit(e);
		}

		protected override void OnLoad(EventArgs e)
		{
			// Some parameters to pass to JavaScript
			ParametersDict[ "pHelloMessage" ] = pHelloMessage.ClientID;
			ParametersDict[ "helloMsg" ] = "Hello world";

			base.OnLoad(e);
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
