using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Renting.Domain.Entities;
using Renting.Domain.Ports;
using Renting.Domain.Services;

namespace Renting.Domain.Tests
{
    [TestClass]
    public class ServicioCalcularValorAPagarTest
    {
        ServicioCalcularValorAPagar ServicioCalcularValorAPagar;
        IProveedorConstantes ProveedorConstantes;

        [TestInitialize]
        public void Initialize()
        {
            ProveedorConstantes = Substitute.For<IProveedorConstantes>();
            ServicioCalcularValorAPagar = new ServicioCalcularValorAPagar(ProveedorConstantes);
        }

        [TestMethod]
        public void CalcularValorCarro1Dia3Horas()
        {
            Vehiculo vehiculo = new Vehiculo
            {
                Tipo = Enum.TipoVehiculo.Carro,
                Ingreso = System.DateTime.Now.AddDays(-1).AddHours(-3)
            };
            ProveedorConstantes.ValorCarroDia.Returns(8000);
            ProveedorConstantes.ValorCarroHora.Returns(1000);

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
            ProveedorConstantes.ValorMotoDia.Returns(4000);
            ProveedorConstantes.ValorMotoHora.Returns(500);
            ProveedorConstantes.ValorRecargoMoto.Returns(2000);
            ProveedorConstantes.CilindrajeRecargoMoto.Returns(500);

            var resultado = ServicioCalcularValorAPagar.CalcularValor(vehiculo);

            Assert.AreEqual(6000, resultado);
        }
    }
}
