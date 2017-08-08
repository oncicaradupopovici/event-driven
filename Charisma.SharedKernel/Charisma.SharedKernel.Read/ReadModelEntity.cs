using System;

namespace Charisma.SharedKernel.ReadModel
{
    public abstract class ReadModelEntity
    {
        public Guid Id { get; protected set; }
    }
}
