<%@ Page	Title="Test 02: Upload tests"
			Language="C#"
			MasterPageFile="~/Site.Master"
			AutoEventWireup="false"
			EnableViewState="false"
			CodeBehind="Test02Upload.aspx.cs"
			Inherits="WebTest01.Test02Upload" %>

<asp:Content ID="head" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">

	Hello world

	<div id="divUpload"></div>

	<ul id="ulMessages">
		<li>Server messages:</li>
	</ul>

<script type="text/javascript">
function test02_init()
{
	window.test02 = {};
	test02.$ulMessages	= $('#ulMessages');
	test02.$divUpload	= $('#divUpload');

	// Create uploader
	test02.uploader		= LongPollingFileUploader( test02.$divUpload,
									{
										urlParameters	: pageParameters['UploadQueryParameters']
									} );

	// Watch messages from server
	message_handler.bind( 'ServerMessage', test02_serverMessage );
}

function test02_serverMessage(evt, message)
{
	var ok = master_checkMessageIsOk( message );

	var $li = $('<li/>').text( 'Message: ' + message['Message'] );
	test02.$ulMessages.append( $li );
}

test02_init();
</script>

</asp:Content>
