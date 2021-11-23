using System.Threading.Tasks;

namespace Renting.Domain.Ports
{
    public interface IRepositorioHub
    {
        Task InvocarHubAsync(string nombreMetodo, object mensaje);
    }
}
