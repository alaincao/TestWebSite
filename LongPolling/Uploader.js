(function()
{
	// Override LongPollingFileUploader()
	var baseLongPollingFileUploader = LongPollingFileUploader;
	LongPollingFileUploader = function($uploadControl, options)
		{
			var self = baseLongPollingFileUploader( $uploadControl, message_handler, options );

			// Layout the control
			$uploadControl	.css( 'background-color', 'Green' )
							.css( 'border', '3px solid Black' )
							.css( 'height', '50px' )
							.css( 'width', '300px' )
							.css( 'text-align', 'center' )
							.text( 'Upload' );

			// Bind to upload events
			self.bind( self.mouseInEvent, function(evt, parm)
					{
						$uploadControl.css( 'background-color', 'Red' );
					} )
				.bind( self.mouseOutEvent, function(evt, parm)
					{
						$uploadControl.css( 'background-color', 'Green' );
					} )
				.bind( self.startEvent, function(evt, message)
					{
						$uploadControl.text( '0%: ' + message['FileName'] );
					} )
				.bind( self.progressEvent, function(evt, message)
					{
						var progress = Math.round( (message['Current'] / message['Total']) * 100 );
						$uploadControl.text( '' + progress + '%: ' + message['FileName'] );
					} )
				.bind( self.terminatedEvent, function(evt, message)
					{
						var ok = master_checkMessageIsOk( message );  // Display errors/warnings/messages if any

						$uploadControl.text( message['FileName'] );
					} );

			return self;
		};
})();
