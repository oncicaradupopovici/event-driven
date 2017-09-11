using System;
using System.Collections.Generic;
using System.Text;

namespace Charisma.SharedKernel.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}
