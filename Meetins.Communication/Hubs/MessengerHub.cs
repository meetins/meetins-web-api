using Meetins.Communication.Abstractions;
using Meetins.Communication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.Communication.Hubs
{
    /// <summary>
    /// Класс хаба для мессенджера.
    /// </summary>
    [Authorize]
    public class MessengerHub : Hub<IClients>
    {
        private readonly MessengerManager _chatManager;
        private const string _defaultGroupName = "General";

        public MessengerHub(MessengerManager chatManager)
            => _chatManager = chatManager;

        /// <summary>
        /// Called when a new connection is established with the hub.
        /// </summary>
        /// <returns>A that represents the asynchronous connect.</returns>
        public override async Task OnConnectedAsync()
        {
            var userName = Context.User?.FindFirst("userId").Value ?? "Anonymous";
            var connectionId = Context.ConnectionId;
            _chatManager.ConnectUser(userName, connectionId);
            await Groups.AddToGroupAsync(connectionId, _defaultGroupName);            
            await base.OnConnectedAsync();
            Console.WriteLine($"{userName} connected!");
        }

        /// <summary>Called when a connection with the hub is terminated.</summary>
        /// <returns>that represents the asynchronous disconnect.</returns>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var isUserRemoved = _chatManager.DisconnectUser(Context.ConnectionId);
            if (!isUserRemoved)
            {
                await base.OnDisconnectedAsync(exception);
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, _defaultGroupName);            
            await base.OnDisconnectedAsync(exception);
        }         
    }
}
