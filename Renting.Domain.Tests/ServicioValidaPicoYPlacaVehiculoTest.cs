using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Renting.Domain.DomainException;
using Renting.Domain.Entities;
using Renting.Domain.Ports;
using Renting.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Renting.Domain.Tests
{
    [TestClass]
    public class ServicioValidaPicoYPlacaVehiculoTest
    {
        ServicioValidaPicoYPlacaVehiculo ServicioValidaPicoYPlacaVehiculo;
        IRepositorioTable RepositorioPicoYPlaca;

        [TestInitialize]
        public void Initialize()
        {
            RepositorioPicoYPlaca = Substitute.For<IRepositorioTable>();
            ServicioValidaPicoYPlacaVehiculo = new ServicioValidaPicoYPlacaVehiculo(RepositorioPicoYPlaca);
        }

        [TestMethod]
        [ExpectedException(typeof(VehiculoEnPicoYPlacaException))]
        public void ValidarCarroConPicoYPlaca()
        {
            string placa = "AAA570";
            var tipo = Enum.TipoVehiculo.Carro;
            RepositorioPicoYPlaca.ObtenerPicoYPlacaPorDiaYTipo(Arg.Any<int>(), Arg.Any<int>()).Returns(new List<PicoYPlaca> { new PicoYPlaca { Placas = "0-1" } });

            ServicioValidaPicoYPlacaVehiculo.ValidarIngresoVehiculoAsync(placa, tipo);
        }

        [TestMethod]
        [ExpectedException(typeof(VehiculoEnPicoYPlacaException))]
        public void ValidarMotoConPicoYPlaca()
        {
            string placa = "AAA10D";
            var tipo = Enum.TipoVehiculo.Moto;
            RepositorioPicoYPlaca.ObtenerPicoYPlacaPorDiaYTipo(Arg.Any<int>(), Arg.Any<int>()).Returns(new List<PicoYPlaca> { new PicoYPlaca { Placas = "0-1" } });

            ServicioValidaPicoYPlacaVehiculo.ValidarIngresoVehiculoAsync(placa, tipo);
        }

        [TestMethod]
        public void ValidarCarroSinPicoYPlaca()
        {
            string placa = "AAA572";
            var tipo = Enum.TipoVehiculo.Carro;
            RepositorioPicoYPlaca.ObtenerPicoYPlacaPorDiaYTipo(Arg.Any<int>(), Arg.Any<int>()).Returns(new List<PicoYPlaca> { new PicoYPlaca { Placas = "0-1" } });

            ServicioValidaPicoYPlacaVehiculo.ValidarIngresoVehiculoAsync(placa, tipo);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ValidarMotoSinPicoYPlaca()
        {
            string placa = "AAA30D";
            var tipo = Enum.TipoVehiculo.Moto;
            RepositorioPicoYPlaca.ObtenerPicoYPlacaPorDiaYTipo(Arg.Any<int>(), Arg.Any<int>()).Returns(new List<PicoYPlaca> { new PicoYPlaca { Placas = "0-1" } });

            ServicioValidaPicoYPlacaVehiculo.ValidarIngresoVehiculoAsync(placa, tipo);

            Assert.IsTrue(true);
        }


    }
}
