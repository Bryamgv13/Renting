using Renting.Domain.Entities;
using System.Collections.Generic;

namespace Renting.Domain.Ports
{
    public interface IRepositorioTable
    {
        IEnumerable<PicoYPlaca> ObtenerPicoYPlacas();
        IEnumerable<PicoYPlaca> ObtenerPicoYPlacaPorDiaYTipo(int dia, int tipo);
        void UpsertPicoYPlaca(PicoYPlaca picoYPlaca);
        void EliminarPicoYPlaca(int dia);
    }
}
