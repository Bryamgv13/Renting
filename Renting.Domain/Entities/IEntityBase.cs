using System;
using System.Collections.Generic;
using System.Text;

namespace Renting.Domain.Entities
{
    public interface IEntityBase<T>
    {
        T Id { get; set; }
    }
}
