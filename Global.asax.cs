using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace WebTest01
{
	public class Global : System.Web.HttpApplication
	{
		void Application_Start(object sender, EventArgs e)
		{
			// Set the log handler to discard all messages comming from "CommonLibs.*"
			var originalHandler = CommonLibs.Utils.Debug.LogHandler;
			CommonLibs.Utils.Debug.LogHandler = (type,message)=>
				{
					if( (""+type).StartsWith("CommonLibs.") )
						return;
					originalHandler( type, message );
				};
			LongPolling.HttpHandler.Initialize();
		}

		void Application_End(object sender, EventArgs e)
		{
		}

		void Application_Error(object sender, EventArgs e)
		{
		}

		void Session_Start(object sender, EventArgs e)
		{
		}

		void Session_End(object sender, EventArgs e)
		{
			LongPolling.HttpHandler.ConnectionListStatic.SessionEnded( Session.SessionID );
		}
	}
}
