
// The following javascript is part of the "Quickstart" guide in the Azure SignalR Service.

function bindConnectionMessage(connection) {

	var messageCallback = function (name, message) {

		if (!message) return;

		// deal with the message
		alert("message received:" + message);
	};

	// Create a function that the hub can call to broadcast messages.

	connection.on('broadcastMessage', messageCallback);
	connection.on('echo', messageCallback);
}

// The '/bus' is defined in Startup.cs (  routes.MapHub<Bus>("/bus");  )

var connection = new signalR.HubConnectionBuilder()
	.withUrl('/bus')
	.build();

bindConnectionMessage(connection);

wireupClient(connection.BusHub);		// <====== I added this.

connection.start()
	.then(function () {
		onConnected(connection);		// <====== Need to define this. (once there is a connection, then do stuff) 
	})
	.catch(function (error) {
		console.error(error.message);
	});