using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Meetins.Communication.Hubs
{
    /// <summary>
    /// Класс хаба для мессенджера.
    /// </summary>
    public class MessengerHub : Hub
    {
        /// <summary>
        /// Метод отправит всем подключенным к хабу пользователям сообщение.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns></returns>
        public async Task SendBroadcastAsync(string message)
        {
            await this.Clients.All.SendAsync("ReceiveBroadcast", message);
        }

        public async Task SendMessageAsync(string message)
        {
            await this.Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
