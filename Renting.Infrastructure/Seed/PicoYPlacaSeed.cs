using Renting.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Renting.Infrastructure.Seed
{
    public static class PicoYPlacaSeed
    {
        public static IEnumerable<PicoYPlaca> ObtenerPicoYPlacaSeeds()
        {
            return new List<PicoYPlaca>
            {
                new PicoYPlaca
                {
                    Dia = (int) DayOfWeek.Monday,
                    Tipo = Domain.Enum.TipoVehiculo.Carro,
                    Placas = "2-3-4-5"
                },
                new PicoYPlaca
                {
                    Dia = (int) DayOfWeek.Tuesday,
                    Tipo = Domain.Enum.TipoVehiculo.Carro,
                    Placas = "6-7-8-9"
                },
                new PicoYPlaca
                {
                    Dia = (int) DayOfWeek.Wednesday,
                    Tipo = Domain.Enum.TipoVehiculo.Carro,
                    Placas = "0-1-2-3"
                },
                new PicoYPlaca
                {
                    Dia = (int) DayOfWeek.Thursday,
                    Tipo = Domain.Enum.TipoVehiculo.Carro,
                    Placas = "4-5-6-7"
                },
                new PicoYPlaca
                {
                    Dia = (int) DayOfWeek.Friday,
                    Tipo = Domain.Enum.TipoVehiculo.Carro,
                    Placas = "8-9-0-1"
                },
                new PicoYPlaca
                {
                    Dia = (int) DayOfWeek.Monday,
                    Tipo = Domain.Enum.TipoVehiculo.Moto,
                    Placas = "2-3"
                },
                new PicoYPlaca
                {
                    Dia = (int) DayOfWeek.Tuesday,
                    Tipo = Domain.Enum.TipoVehiculo.Moto,
                    Placas = "4-5"
                },
                new PicoYPlaca
                {
                    Dia = (int) DayOfWeek.Wednesday,
                    Tipo = Domain.Enum.TipoVehiculo.Moto,
                    Placas = "6-7"
                },
                new PicoYPlaca
                {
                    Dia = (int) DayOfWeek.Thursday,
                    Tipo = Domain.Enum.TipoVehiculo.Moto,
                    Placas = "8-9"
                },
                new PicoYPlaca
                {
                    Dia = (int) DayOfWeek.Friday,
                    Tipo = Domain.Enum.TipoVehiculo.Moto,
                    Placas = "0-1"
                }
            };
        }
    }
}
