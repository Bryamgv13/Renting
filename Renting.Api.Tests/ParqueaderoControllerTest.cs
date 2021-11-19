using Api.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace Renting.Api.Tests
{
    [TestClass]
    public class ParqueaderoControllerTest : IntegrationTestBuilder
    {
        [TestInitialize]
        public void Initialize()
        {
            BootstrapTestingSuite();
        }

        [TestMethod]
        public void PostIngresoSuccess()
        {
            var postContent = new Renting.Application.Parqueadero.IngresoVehiculoCommand
            {
                Placa = "AAA57E",
                Tipo = Domain.Enum.TipoVehiculo.Carro
            };

            var c = this.TestClient.PostAsync("api/Parqueadero/Ingreso", postContent, new JsonMediaTypeFormatter()).Result;
            
            c.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, c.StatusCode);
        }

        [TestMethod]
        public void PostSalidaSuccess()
        {
            var content = new Renting.Application.Parqueadero.IngresoVehiculoCommand
            {
                Placa = "AAA57E",
                Tipo = Domain.Enum.TipoVehiculo.Carro
            };
            _ = this.TestClient.PostAsync("api/Parqueadero/Ingreso", content, new JsonMediaTypeFormatter()).Result;
            var postContent = new Renting.Application.Parqueadero.SalidaVehiculoCommand
            {
                Placa = "AAA57E",
            };

            var c = this.TestClient.PostAsync("api/Parqueadero/Salida", postContent, new JsonMediaTypeFormatter()).Result;

            c.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, c.StatusCode);
        }
    }
}
