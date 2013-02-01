using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CommonLibs.Web.LongPolling.Utils;

namespace WebTest01
{
	public partial class SiteMaster : System.Web.UI.MasterPage
	{
		internal Dictionary<string,object>	ParametersDict				= new Dictionary<string,object>();
		protected string					Parameters					{ get { return ParametersDict.ToJSON(); } }

		private string						JQueryUICSSUrl				{ get { return Page.ResolveUrl("~/css/flick/jquery-ui-1.9.1.custom.css"); } }
		protected string					JQueryUrl					{ get { return Page.ResolveUrl("~/jquery-1.8.2.js"); } }
		protected string					JQueryUIUrl					{ get { return Page.ResolveUrl("~/jquery-ui-1.9.1.custom.js"); } }
		protected string					LongPollingBlock;
		protected string					MasterJsUrl					{ get { return Page.ResolveUrl("~/Site.Master.js"); } }

		protected override void OnLoad(EventArgs e)
		{
			cssJQueryUI.Href = JQueryUICSSUrl;

			// Include LongPolling JavaScript client in page's <head> tag
			var jsObjectName = "message_handler";
			var longPollingHandlerUrl = Page.ResolveUrl( LongPolling.HttpHandler.BASE_URL );
			var longPollingSyncedHandlerUrl = "NONE YET";
			var logoutUrl = Page.ResolveUrl("~/");  // None yet
			LongPollingBlock = CommonLibs.Web.LongPolling.JSClient.CreateJSClientInitializationBlock( Page, jsObjectName, longPollingHandlerUrl, longPollingSyncedHandlerUrl, logoutUrl, startDirectly:false );

			base.OnLoad(e);
		}

		/// <summary>
		/// Simple message handler that sends the original message back to sender without any other processing
		/// </summary>
		public static void PingMessageHandler(CommonLibs.Web.LongPolling.Message requestMessage)
		{
			var responseMessage = CommonLibs.Web.LongPolling.Message.CreateResponseMessage( requestMessage );
			LongPolling.HttpHandler.MessageHandlerStatic.SendMessageToConnection( requestMessage.SenderConnectionID, responseMessage );
		}
	}
}
