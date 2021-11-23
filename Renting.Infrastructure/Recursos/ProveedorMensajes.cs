using Renting.Domain.Ports;
using System.Resources;

namespace Renting.Infrastructure.Recursos
{
    public class ProveedorMensajes : IProveedorMensajes
    {
        private readonly ResourceManager _resourceManager;

        public ProveedorMensajes()
        {
            this._resourceManager = new ResourceManager("Renting.Infrastructure.Recursos.Mensajes", typeof(ProveedorMensajes).Assembly);
        }

        public string ParqueaderoLleno => _resourceManager.GetString("ParqueaderoLleno");
        public string PicoYPlaca => _resourceManager.GetString("PicoYPlaca");
    }
}
