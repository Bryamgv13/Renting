using Renting.Domain.Entities;
using Renting.Domain.Enum;
using System;

namespace Renting.Domain.Services
{
    [DomainService]
    public class ServicioCalcularValorAPagar
    {
        private readonly int VALOR_CARRO_HORA = 1000;
        private readonly int VALOR_CARRO_DIA = 8000;
        private readonly int VALOR_MOTO_HORA = 500;
        private readonly int VALOR_MOTO_DIA = 4000;
        private readonly int RECARGO_MOTO = 2000;

        public ServicioCalcularValorAPagar() { }

        public double CalcularValor(Parqueadero vehiculo)
        {
            if (vehiculo.Tipo.Equals(TipoVehiculo.Carro))
            {
                return CalcularValorCarro(vehiculo);
            } else
            {
                return CalcularValorMoto(vehiculo);
            }
        }

        private double CalcularValorCarro(Parqueadero vehiculo)
        {
            var diferencia = (DateTime.Now - vehiculo.Ingreso);
            double tiempoEnMinutos = diferencia.TotalMinutes;
            if (tiempoEnMinutos <= 60)
            {
                return VALOR_CARRO_HORA;
            }
            int dias = diferencia.Days;
            int horas = diferencia.Hours;
            int minutos = diferencia.Minutes;
            var total = (dias * VALOR_CARRO_DIA) + (horas * VALOR_CARRO_HORA);
            return 0;
        }

        private double CalcularValorMoto(Parqueadero vehiculo)
        {
            return 0;
        }
    }
}
