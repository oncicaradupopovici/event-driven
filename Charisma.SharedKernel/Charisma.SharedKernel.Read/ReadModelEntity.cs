using System;
using Charisma.SharedKernel.Core.Interfaces;

namespace Charisma.SharedKernel.ReadModel
{
    public abstract class ReadModelEntity : IEntity
    {
        public Guid Id { get; protected set; }
    }
}
