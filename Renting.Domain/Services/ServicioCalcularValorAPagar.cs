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
        private readonly int VALOR_RECARGO_MOTO = 2000;
        private readonly int CILINDRAJE_RECARGO_MOTO = 500;

        public ServicioCalcularValorAPagar() { }

        public double CalcularValor(Vehiculo vehiculo)
        {
            if (vehiculo.Tipo.Equals(TipoVehiculo.Carro))
            {
                return CalcularValorCarro(vehiculo);
            } else
            {
                return CalcularValorMoto(vehiculo);
            }
        }

        private double CalcularValorCarro(Vehiculo vehiculo)
        {
            var diferencia = (DateTime.Now - vehiculo.Ingreso);
            double tiempoEnMinutos = diferencia.TotalMinutes;
            if (tiempoEnMinutos <= 60)
            {
                return VALOR_CARRO_HORA;
            }
            int dias = diferencia.Days;
            int horas = diferencia.Hours;
            if (dias == 0 && horas > 9)
            {
                return VALOR_CARRO_DIA;
            }
            var total = (dias * VALOR_CARRO_DIA) + (horas * VALOR_CARRO_HORA);
            return total;
        }

        private double CalcularValorMoto(Vehiculo vehiculo)
        {
            var diferencia = (DateTime.Now - vehiculo.Ingreso);
            double tiempoEnMinutos = diferencia.TotalMinutes;
            if (tiempoEnMinutos <= 60)
            {
                return VALOR_MOTO_HORA;
            }
            int dias = diferencia.Days;
            int horas = diferencia.Hours;
            double total;
            if (dias == 0 && horas > 9)
            {
                total = VALOR_MOTO_DIA;
            }
            else
            {
                total = (dias * VALOR_MOTO_DIA) + (horas * VALOR_MOTO_HORA);
            }            
            if (vehiculo.Cilindraje > CILINDRAJE_RECARGO_MOTO)
            {
                total += VALOR_RECARGO_MOTO;
            }
            return total;
        }
    }
}
