using Renting.Domain.Ports;
using System.Resources;

namespace Renting.Infrastructure.Recursos
{
    public class ProveedorConstantes : IProveedorConstantes
    {
        private readonly ResourceManager _resourceManager;

        public ProveedorConstantes()
        {
            this._resourceManager = new ResourceManager("Renting.Infrastructure.Recursos.Constantes", typeof(ProveedorConstantes).Assembly);
        }

        public double ValorCarroHora => double.Parse(_resourceManager.GetString("ValorCarroHora"));
        public double ValorCarroDia => double.Parse(_resourceManager.GetString("ValorCarroDia"));
        public double ValorMotoHora => double.Parse(_resourceManager.GetString("ValorMotoHora"));
        public double ValorMotoDia => double.Parse(_resourceManager.GetString("ValorMotoDia"));
        public double ValorRecargoMoto => double.Parse(_resourceManager.GetString("ValorRecargoMoto"));
        public int CilindrajeRecargoMoto => int.Parse(_resourceManager.GetString("CilindrajeRecargoMoto"));
    }
}
