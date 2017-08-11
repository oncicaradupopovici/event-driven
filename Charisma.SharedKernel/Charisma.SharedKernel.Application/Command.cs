using System;
using Charisma.SharedKernel.Core;

namespace Charisma.SharedKernel.Application
{
    public class Command : Message
    {
        public Guid CommandId { get; }

        public DateTime CreationDate { get; }

        public Command()
        {
            CommandId = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
    }
}
