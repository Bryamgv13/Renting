using Renting.Domain.Enum;

namespace Renting.Domain.Entities
{
    public class PicoYPlaca
    {
        public int Dia { get; set; }
        public TipoVehiculo Tipo { get; set; }
        public string Placas { get; set; }

        public string[] ObtenerPlacas()
        {
            return this.Placas.Split('-');
        }
    }
}
