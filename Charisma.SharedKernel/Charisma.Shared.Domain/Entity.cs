using Charisma.SharedKernel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Charisma.SharedKernel.Domain
{
    public class Entity : IEntity
    {
        public Guid Id { get; protected set; }
    }
}
