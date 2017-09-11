using System;
using Charisma.SharedKernel.Domain.Interfaces;

namespace Charisma.SharedKernel.Domain
{
    public abstract class ReadModelEntity : IEntity
    {
        public Guid Id { get; protected set; }
    }
}
