using System;
using Charisma.SharedKernel.Core;

namespace Charisma.SharedKernel.Application
{
    public class Command : Message
    {
        public Guid Id { get; protected set; }
    }
}
