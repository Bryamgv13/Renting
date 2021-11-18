using System;
using System.Runtime.Serialization;

namespace Renting.Domain.DomainException
{
    [Serializable]
    public class VehiculoEnPicoYPlacaException : Exception
    {
        public VehiculoEnPicoYPlacaException()
        {
        }

        public VehiculoEnPicoYPlacaException(string mensaje) : base(mensaje)
        {
        }

        public VehiculoEnPicoYPlacaException(string mensaje, System.Exception innerException) : base(mensaje, innerException)
        {
        }

        protected VehiculoEnPicoYPlacaException(SerializationInfo info, StreamingContext contexto) : base(info, contexto)
        {
        }
    }
}
