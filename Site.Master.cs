using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTest01
{
	public partial class SiteMaster : System.Web.UI.MasterPage
	{
		protected string			JQueryUrl						{ get { return Page.ResolveUrl("~/jquery-1.8.2.js"); } }
		protected string			LongPollingClientUrl			{ get { return Page.ResolveUrl("~/CommonLibs/Web/LongPolling/JSClient/LongPollingClient.js"); } }
		protected string			LongPollingHandlerUrl			{ get { return "'" + Page.ResolveUrl(LongPolling.HttpHandler.BASE_URL) + "'"; } }
		protected string			LongPollingSyncedHandlerUrl		{ get { return "'" + "NONE YET" + "'"; } }
		protected string			LogoutUrl						{ get { return "'" + Page.ResolveUrl("~/") + "'"; } }
	}
}
