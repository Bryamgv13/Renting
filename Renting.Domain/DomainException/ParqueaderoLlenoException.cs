using System;
using System.Runtime.Serialization;

namespace Renting.Domain.DomainException
{
    [Serializable]
    public class ParqueaderoLlenoException : Exception
    {
        public ParqueaderoLlenoException()
        {
        }

        public ParqueaderoLlenoException(string mensaje) : base(mensaje)
        {
        }

        public ParqueaderoLlenoException(string mensaje, System.Exception innerException) : base(mensaje, innerException)
        {
        }

        protected ParqueaderoLlenoException(SerializationInfo info, StreamingContext contexto) : base(info, contexto)
        {
        }
    }
}
