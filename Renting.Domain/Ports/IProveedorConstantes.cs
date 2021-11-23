namespace Renting.Domain.Ports
{
    public interface IProveedorConstantes
    {
        double ValorCarroHora { get; }
        double ValorCarroDia { get; }
        double ValorMotoHora { get; }
        double ValorMotoDia { get; }
        double ValorRecargoMoto { get; }
        int CilindrajeRecargoMoto { get; }
    }
}
