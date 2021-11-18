using System.Threading.Tasks;

namespace Renting.Application.Ports
{
    public interface IBusMessaging
    {
        Task SendMessageAsync(object o, string queue);
    }
}
