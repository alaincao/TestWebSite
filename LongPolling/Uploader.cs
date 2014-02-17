using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CommonLibs.Utils;
using CommonLibs.Web.LongPolling;
using CommonLibs.Web.LongPolling.Utils;

namespace WebTest01.LongPolling
{
	public static class Uploader
	{
		private const string			JSUrl							= "~/LongPolling/Uploader.js";

		public static void RegisterJS(System.Web.UI.Page page)
		{
			CommonLibs.Utils.Debug.ASSERT( page != null, System.Reflection.MethodInfo.GetCurrentMethod(), "Missing parameter 'page'" );

			if( page.ClientScript.IsClientScriptBlockRegistered(JSUrl) )
				// Already registered
				return;

			// Register the 2 JS includes
			var scriptBlock = CommonLibs.Web.LongPolling.JSClient.CreateJSUploaderBlock( page ) + "\n" +
								string.Format( "<script type='text/javascript' src='{0}'></script>", page.ResolveClientUrl(JSUrl).EscapeQuotes() );
			page.ClientScript.RegisterClientScriptBlock( typeof(Uploader), key:JSUrl, script:scriptBlock );
		}

		public static Dictionary<string,string> GetQueryParameters(Type receiverType, string methodName="OnUploadPageFileCreated")
		{
			CommonLibs.Utils.Debug.ASSERT( receiverType != null, System.Reflection.MethodInfo.GetCurrentMethod(), "Missing parameter 'receiverType'" );

			return GenericPageFile.CreateUploaderQueryParameters( receiverType:receiverType, methodName:methodName );
		}
	}
}
