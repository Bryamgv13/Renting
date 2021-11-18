using MediatR;
using Microsoft.AspNetCore.Mvc;
using Renting.Application.Parqueadero;
using System.Threading.Tasks;

namespace Renting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParqueaderoController : ControllerBase
    {
        private readonly IMediator _Mediator;

        public ParqueaderoController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpPost("Ingreso")]
        public async Task IngresoVehiculo(IngresoVehiculoCommand vehiculo)
        {
            await _Mediator.Send(vehiculo);
        }

        [HttpPost("Salida")]
        public async Task SalidaVehiculo(SalidaVehiculoCommand vehiculo)
        {
            await _Mediator.Send(vehiculo);
        }
    }
}
