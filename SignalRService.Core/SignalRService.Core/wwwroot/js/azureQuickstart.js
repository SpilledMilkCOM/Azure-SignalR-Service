
// The following javascript is part of the "Quickstart" guide in the Azure SignalR Service.

function bindConnectionMessage(connection) {

	var messageCallback = function (name, message) {

		if (!message) return;

		// deal with the message
		alert(name + ": message received:" + message);
	};

	// Create a function that the hub can call to broadcast messages.

	connection.on('broadcastMessage', displayMessage);
	connection.on('echo', displayMessage);
}

// The '/bus' is defined in Startup.cs (  routes.MapHub<Bus>("/bus");  )

var connection = new signalR.HubConnectionBuilder()
	.withUrl('/bus')
	.build();

bindConnectionMessage(connection);

wireupClient(connection);				// <====== I added this.

connection.start()
	.then(function () {
		onConnected(connection);		// <====== Need to define this. (once there is a connection, then do stuff) 
	})
	.catch(function (error) {
		console.error(error.message);
	});

//=======================================================================================================

// The following code I added.

function clientName() {
	return $('#clientId').val();
}

function displayMessage(name, message) {
	$('#latestMessageId').append('<li>' + name + ': ' + message + '</li>');
}

function onConnected(connection) {
	// Not sure what to put here just yet.
}

function wireupClient(connection) {
	$('#broadcastMessageId').click(function () {
		// Call the hub's Broadcast() method (supplied by the default Quickstart code)

		connection.invoke("BroadcastMessage", clientName(), 'BROADCAST! From the named client.')
			.catch(function (err) {
				return console.error(err.toString());
			});
	});

	$('#echoId').click(function () {
		// Call the hub's Echo() method (supplied by the default Quickstart code)

		connection.invoke("Echo", clientName(), 'ECHO! From the named client.')
			.catch(function (err) {
				return console.error(err.toString());
			});
	});
}