using System;

namespace Meetins.Communication.Hubs
{
    public class MessengerConnection
    {
        /// <summary>
        /// Registered at time
        /// </summary>
        public DateTime ConnectedAt { get; set; }

        /// <summary>
        /// Connection Id from client
        /// </summary>
        public string ConnectionId { get; set; } = null!;
    }
}
