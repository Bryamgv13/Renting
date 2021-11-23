using Renting.Domain.Entities;
using Renting.Domain.Enum;
using Renting.Domain.Ports;
using System;

namespace Renting.Domain.Services
{
    [DomainService]
    public class ServicioCalcularValorAPagar
    {
        private readonly IProveedorConstantes ProveedorConstantes;

        public ServicioCalcularValorAPagar(IProveedorConstantes proveedorConstantes)
        {
            ProveedorConstantes = proveedorConstantes;
        }

        public double CalcularValor(Vehiculo vehiculo)
        {
            if (vehiculo.Tipo.Equals(TipoVehiculo.Carro))
            {
                return CalcularValorCarro(vehiculo);
            }
            else
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
                return ProveedorConstantes.ValorCarroHora;
            }
            int dias = diferencia.Days;
            int horas = diferencia.Hours;
            if (dias == 0 && horas > 9)
            {
                return ProveedorConstantes.ValorCarroDia;
            }
            var total = (dias * ProveedorConstantes.ValorCarroDia) + (horas * ProveedorConstantes.ValorCarroHora);
            return total;
        }

        private double CalcularValorMoto(Vehiculo vehiculo)
        {
            double total;

            var diferencia = (DateTime.Now - vehiculo.Ingreso);
            double tiempoEnMinutos = diferencia.TotalMinutes;
            if (tiempoEnMinutos <= 60)
            {
                total = ProveedorConstantes.ValorMotoHora;
            }
            else
            {
                int dias = diferencia.Days;
                int horas = diferencia.Hours;
                if (dias == 0 && horas > 9)
                {
                    total = ProveedorConstantes.ValorMotoDia;
                }
                else
                {
                    total = (dias * ProveedorConstantes.ValorMotoDia) + (horas * ProveedorConstantes.ValorMotoHora);
                }
            }
            if (vehiculo.Cilindraje > ProveedorConstantes.CilindrajeRecargoMoto)
            {
                total += ProveedorConstantes.ValorRecargoMoto;
            }
            return total;
        }
    }
}
