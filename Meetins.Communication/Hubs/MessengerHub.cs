using Meetins.Communication.Abstractions;
using Meetins.Communication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Meetins.Communication.Hubs
{
    /// <summary>
    /// Класс хаба для мессенджера.
    /// </summary>
    [Authorize]
    public class MessengerHub : Hub<IMessenger>
    {        
        public async Task Send(string mess)
        {       
            string userId = Context.User.FindFirst("userId").Value;
            await Clients.User(userId).ReceiveBroadcast(mess);
        }
    }
}
