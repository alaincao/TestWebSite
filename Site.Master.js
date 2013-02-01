
function master_init()
{
	// Init long-polling message handler
	message_handler	.bind( message_handler.internalErrorEvent, function(event, msg)
						{
							// There is an error with the message handler itself (JSON error, etc...)
							master_showMessage( msg, /*title*/'Long-polling client internal error' );
						} )
					.bind( message_handler.messageHandlerFailedEvent, function(event, err)
						{
							// This is a handler for JavaScript errors
							var msg = err.message;
							if( err.stack != null )
								var msg = err.message + '\n'
										+ err.stack;
							var $msg = $('<pre/>').text( msg );
							master_showMessage( $msg, /*title*/'JavaScript error' );
						} )
					.bind( 'exception', function(event, msg)
						{
							// This is a general handler for messages of type 'exception'
							// which are sent when the handler (server-side) of a message without 'reply_to_type' defined throws an exception

							// Simulate the reception of a message containing an exception
							master_checkMessageIsOk( { 'exception': msg } );
						} );
	message_handler.start();
}

function master_showMessage(msg, title)
{
	var $root = $('<div/>').attr( 'title', title );
	if( msg instanceof jQuery )
		$root.append( msg );
	else
		$root.text( msg );
	$(document.body).append( $root );
	$root.dialog();
}

function master_checkMessageIsOk(message)
{
	var isOk = true;

	var exception = message['exception'];
	if( exception != null )
	{
		isOk = false;

		var $message = $('<div/>')	.append( $('<h2/>').text('Server exception:') )
									.append( $('<h3/>').text(exception['message']) )
									.append( $('<pre/>').text(exception['stack']) );
		master_showMessage( $message, /*title*/'Server sent an exception' );
	}

	return isOk;
}
