using System;

namespace Charisma.SharedKernel.Domain
{
    public class Command : Message
    {
        public Guid Id { get; protected set; }
    }
}
