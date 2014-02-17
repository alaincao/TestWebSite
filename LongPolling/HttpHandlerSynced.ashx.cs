using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CommonLibs.Web.LongPolling;
using CommonLibs.Web.LongPolling.Utils;

namespace WebTest01.LongPolling
{
	internal class HttpHandlerSynced : SyncedHttpHandler
	{
		internal const string					BASE_URL				= "~/LongPolling/HttpHandlerSynced.ashx";

		public override ConnectionList			ConnectionList			{ get { return HttpHandler.ConnectionListStatic; } }

		public override ISyncedRequestHandler GetHandler(HttpContext context, string handlerType, string sessionID, string connectionID)
		{
			switch( handlerType )
			{
				case GenericPageFile.HandlerType: {
					var instance = GenericPageFile.GenericSyncedHandler( HttpHandler.ConnectionListStatic, context.Request, connectionID, ()=>{ return new GenericPageFile(HttpHandler.MessageHandlerStatic, connectionID); } );
					return instance; }

				default:
					throw new NotImplementedException("Unknown handler type '" + handlerType + "'");
			}
		}
	}
}
