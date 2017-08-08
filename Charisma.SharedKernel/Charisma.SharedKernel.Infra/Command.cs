using System;

namespace Charisma.SharedKernel.Core
{
    public class Command : Message
    {
        public Guid Id { get; protected set; }
    }
}
