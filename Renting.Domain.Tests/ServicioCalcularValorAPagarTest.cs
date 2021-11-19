using Microsoft.VisualStudio.TestTools.UnitTesting;
using Renting.Domain.Entities;
using Renting.Domain.Services;

namespace Renting.Domain.Tests
{
    [TestClass]
    public class ServicioCalcularValorAPagarTest
    {
        ServicioCalcularValorAPagar ServicioCalcularValorAPagar;

        [TestInitialize]
        public void Initialize()
        {
            ServicioCalcularValorAPagar = new ServicioCalcularValorAPagar();
        }

        [TestMethod]
        public void CalcularValorCarro1Dia3Horas()
        {
            Vehiculo vehiculo = new Vehiculo
            {
                Tipo = Enum.TipoVehiculo.Carro,
                Ingreso = System.DateTime.Now.AddDays(-1).AddHours(-3)
            };

            var resultado = ServicioCalcularValorAPagar.CalcularValor(vehiculo);

            Assert.AreEqual(11000, resultado);
        }

        [TestMethod]
        public void CalcularValorMoto10HorasRecargo()
        {
            Vehiculo vehiculo = new Vehiculo
            {
                Tipo = Enum.TipoVehiculo.Moto,
                Cilindraje = 650,
                Ingreso = System.DateTime.Now.AddHours(-10)
            };

            var resultado = ServicioCalcularValorAPagar.CalcularValor(vehiculo);

            Assert.AreEqual(6000, resultado);
        }
    }
}
