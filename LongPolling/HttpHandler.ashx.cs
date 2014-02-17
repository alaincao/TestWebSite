using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CommonLibs.Utils.Tasks;
using CommonLibs.Web.LongPolling;

namespace WebTest01.LongPolling
{
	internal class HttpHandler : LongPollingHandler
	{
		internal const string					BASE_URL								= "~/LongPolling/HttpHandler.ashx";

		internal static TasksQueue				TasksQueueStatic						{ get; private set; }
		internal static MessageHandler			MessageHandlerStatic					{ get; private set; }
		internal static ConnectionList			ConnectionListStatic					{ get; private set; }

		protected override MessageHandler		MessageHandler							{ get { return MessageHandlerStatic; } }
		protected override ConnectionList		ConnectionList							{ get { return ConnectionListStatic; } }

		public HttpHandler()
		{
		}

		internal static void Initialize()
		{
			TasksQueueStatic = new CommonLibs.Utils.Tasks.TasksQueue();
			ConnectionListStatic = new CommonLibs.Web.LongPolling.ConnectionList( TasksQueueStatic );
			MessageHandlerStatic = new CommonLibs.Web.LongPolling.MessageHandler( TasksQueueStatic, ConnectionListStatic );

			#if DEBUG
				// Increase stale & disconnection timeouts during development so unwanted disconnections don't occures when using breakpoints
				ConnectionListStatic.DisconnectionSeconds = 60*60;  // 1H
				ConnectionListStatic.StaleConnectionSeconds = 60*60;  // 1H
			#endif

			// Registering message handlers
			MessageHandlerStatic.AddMessageHandler( SiteMaster.PingMessageType, SiteMaster.PingMessageHandler );
			MessageHandlerStatic.AddMessageHandler( "Test01_TestCrash", Test01Crash.TestCrashHandler );
		}
	}
}
