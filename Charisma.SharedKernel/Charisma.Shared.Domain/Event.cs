using System;

namespace Charisma.SharedKernel.Domain
{
    public class Event : Message
    {
        public Guid Id { get; protected set; }
        public int Version;
    }
}
