using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CommonLibs.Web.LongPolling;
using CommonLibs.Web.LongPolling.Utils;

namespace WebTest01
{
	public partial class Test02Upload : System.Web.UI.Page
	{
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			LongPolling.Uploader.RegisterJS( this );

			var pageParameters = ((SiteMaster)Master).PageParameters;
			pageParameters[ "UploadQueryParameters" ] = LongPolling.Uploader.GetQueryParameters( receiverType:typeof(Test02Upload), methodName:"OnUploadCreated" );
		}

		public static void OnUploadCreated(ConnectionList connectionList, string connectionID, GenericPageFile pageFile, HttpRequest request)
		{
			var message = Message.CreateEmtpyMessage( "ServerMessage" );
			message[ "Message" ] = ""+DateTime.Now + " OnUploadCreated";
			LongPolling.HttpHandler.MessageHandlerStatic.SendMessageToConnection( connectionID, message );

			pageFile.OnUploadStarted = (rqst,uploadFileName)=>{ OnUploadStarted(pageFile, uploadFileName); };
			pageFile.OnUploadTerminated = (rqst,fileName,filePath,succes,size)=>{ OnUploadTerminated(pageFile, fileName, filePath, succes, size); };
		}

		public static void OnUploadStarted(GenericPageFile pageFile, string uploadFileName)
		{
			var message = Message.CreateEmtpyMessage( "ServerMessage" );
			message[ "Message" ] = ""+DateTime.Now + " OnUploadStarted ; " + uploadFileName;
			LongPolling.HttpHandler.MessageHandlerStatic.SendMessageToConnection( pageFile.ConnectionID, message );
		}

		public static void OnUploadTerminated(GenericPageFile pageFile, string fileName, string filePath, bool succes, long size)
		{
			var message = Message.CreateEmtpyMessage( "ServerMessage" );
			message[ "Message" ] = ""+DateTime.Now + " OnUploadTerminated " + succes + " ; " + fileName + " ; " + size + " ; " + filePath;
			LongPolling.HttpHandler.MessageHandlerStatic.SendMessageToConnection( pageFile.ConnectionID, message );
		}
	}
}
