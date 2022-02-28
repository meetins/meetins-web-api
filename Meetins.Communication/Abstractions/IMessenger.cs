using Meetins.Communication.Models;
using System.Threading.Tasks;

namespace Meetins.Communication.Abstractions
{
    public interface IMessenger
    {
        Task ReceiveBroadcast(string message);
    }
}
