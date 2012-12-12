<%@ Page	Title="Test 01: Crash tests"
			Language="C#"
			MasterPageFile="~/Site.Master"
			AutoEventWireup="false"
			EnableViewState="false"
			CodeBehind="Test01Crash.aspx.cs"
			Inherits="WebTest01.Test01Crash" %>

<asp:Content ID="head" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">

	<p runat="server" ID="pHelloMessage"><%-- Hello world message goes here --%></p>

	<input type="button" id="btnCrash1" value="Test server-side exception without reply_to"/><br />
	<input type="button" id="btnCrash2" value="Test server-side exception with reply_to"/><br />
	<input type="button" id="btnCrash3" value="Test client-side exception in message handler"/>

<script type="text/javascript">
function test01_init()
{
	window.test01 = {};

	test01.$pHelloMessage					= $('#'+pageParameters['pHelloMessage']);
	test01.helloMessage						= pageParameters['helloMsg'];

	test01.$btnServerCrashWithoutReplyTo	= $('#btnCrash1');
	test01.$btnServerCrashWithReplyTo		= $('#btnCrash2');
	test01.$btnClientCrash					= $('#btnCrash3');

	test01.lastMessage						= null;  // Set by message handlers

	// Set 'Hello world' message
	test01.$pHelloMessage.text( test01.helloMessage );

	// Watch for button clicks
	test01.$btnServerCrashWithoutReplyTo.click( function()
		{
			// Send a message without a 'reply_to_type' field that will crash server-side
			message_handler.sendMessage( {	'type'			: 'Test01_TestCrash' } );  // The server-side message handler type

			// NB:
			// In this case, the server will respond with a message of type 'exception'.
			// This message contains the exception details ('class', 'message' and 'type')
			// This type has been registered by the MasterPage (see 'Site.Master.js')
		} );
	test01.$btnServerCrashWithReplyTo.click( function()
		{
			// Send a message with a 'reply_to_type' field that will crash server-side
			message_handler.sendMessage( {	'type'			: 'Test01_TestCrash',  // The server-side message handler type
											'reply_to_type'	: 'test01_ResponseHandler' } );  // When the server sends its response message, it will be send to the using this handler type (i.e. handled by this script)

			// NB:
			// In this case, the server will respond with a message of the specified type 'foo_ResponseHandler'.
			// This message will contain a parameter 'exception' that contains the exception details ('class', 'message' and 'type')
		} );
	test01.$btnClientCrash.click( function()
		{
			message_handler.sendMessage( {	'type'			: 'Master_Ping',  // The server-side message handler type
											'reply_to_type'	: 'test01_ResponseHandlerBuggy' } );  // When the server sends its response message, it will be send to the using this handler type (i.e. handled by this script)
		} );

	// Bind client-side messages handlers
	message_handler.bind( 'test01_ResponseHandler', test01_responseHanler );  // Will receive the response messages of 'test01.$btnServerCrashWithReplyTo'
	message_handler.bind( 'test01_ResponseHandlerBuggy', test01_responseHanlerBuggy );  // Will receive the response message of 'test01.$btnClientCrash'
}

// Generic message handler
function test01_responseHanler(e,message)
{
	test01.lastMessage = message;  // Save this message so it can be examined (e.g. using Firefox's FireBug)

	var isOk = master_checkMessageIsOk( message );
	if(! isOk )
	{
		// Server sent an exception
	}
	else
	{
		// Processed the message without exception
	}
}

// Message handler that trows a JavaScript exception
function test01_responseHanlerBuggy(e,message)
{
	test01.lastMessage = message;  // Save this message so it can be examined (e.g. using Firefox's FireBug)
	kaboum();  // And crash...
}

test01_init();
</script>

</asp:Content>
