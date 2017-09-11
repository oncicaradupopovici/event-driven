using System;

namespace Charisma.SharedKernel.Application
{
    public class Command
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
