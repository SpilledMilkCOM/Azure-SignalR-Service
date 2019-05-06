using Microsoft.AspNetCore.SignalR;

namespace SignalRService.Core.Hubs
{
	public class Bus : Hub
	{
		/// <summary>
		/// Every web client that is connected will receive this message.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="message"></param>
		public void BroadcastMessage(string name, string message)
		{
			Clients.All.SendAsync("broadcastMessage", name, message);
		}

		public void Echo(string name, string message)
		{
			Clients.Client(Context.ConnectionId).SendAsync("echo", name, message + " (echo from server)");
		}
	}
}
