using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Renting.Application.Parqueadero
{
    public class SalidaVehiculoCommand : IRequest<InformacionSalidaDto>
    {
        public SalidaVehiculoCommand()
        {

        }

        public SalidaVehiculoCommand(string placa)
        {
            Placa = placa;
        }

        [Required]
        public string Placa { get; set; }
    }
}
