using System;
using System.Collections.Generic;
using System.Text;

namespace Renting.Domain.Services
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DomainServiceAttribute : Attribute
    {
    }
}
